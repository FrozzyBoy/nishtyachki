using AdminApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Timers;
using AdminApp.Services;

namespace AdminApp.Queue
{
    public enum Role
    {
        standart = 0,
        premium = 1
    }
    public enum UserState
    {
        Offline, Online, InQueue, WaitingForAccept, UsingNishtiak
    }
    public enum TypeOfUpdate
    {        
        StandInQueue, 
        LeftQueueBeforeUsedNishtyak, 
        BeganToUseNishtyak, 
        EndedToUseNishtyak,
        WaitingForAccept
    }
    
    public class User
    {        
        public string ID { get; private set; }
        public IClient Client { get; set; }
        public UserStat Statistic { get; private set; }
        public UserState State { get; set; }

        public Role Role
        {
            get
            {
                if (_premiumEndDate > DateTime.Now)
                {
                    return Queue.Role.premium;
                }
                return Queue.Role.standart;
            }
        }

        private DateTime _premiumEndDate;

        public static UserInfo GetUserInfo(string id)
        {
            UserInfo oldUser = AppDbContext.Instance.UsersInfo.SingleOrDefault<UserInfo>(u => u.UserName == id);
            return oldUser;
        }

        public static IEnumerable<UserInfo> GetUserInfo()
        {
            return AppDbContext.Instance.UsersInfo.ToArray<UserInfo>();
        }

        public User(string id,IClient Client )
        {
            this.ID = id;
            this.Client = Client;
            this.Statistic = new UserStat();
            this.State = UserState.Online;

            LoadChanges();

            this.State = UserState.Online;
        }

        public void UpdateInfo(TypeOfUpdate type)
        {
            Statistic.UpdateInfo(type);
            SaveChanges();
        }

        public void AddPremium(int days = 3)
        {
            _premiumEndDate = DateTime.Now.AddDays(days);
            SaveChanges();
        }

        private System.Timers.Timer _t;
        public override bool Equals(Object obj)
        {
            User user = obj as User;

            if (user == null)
            {
                return false;
            }

            if (this.ID == user.ID)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        internal void CheckTimeForAcess()
        {
            _t = new System.Timers.Timer(120000);
            _t.Elapsed += t_Elapsed;
            _t.Start();
            
        }
        internal void CheckTimeForUsing()
        {
            _t = new System.Timers.Timer(600000);
            _t.Elapsed += t_Elapsed;
            _t.Start();

        }    
        internal void Abort()
        {
            _t.Stop();
        }
        void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _t.Stop();
            Client.DroppedByServer("you are dropepd");
            UsersQueue.Instance.DeleteFromTheQueue(this);
        }

        internal void DeletePremium()
        {
            _premiumEndDate = DateTime.MinValue;
            SaveChanges();
        }

        private void SaveChanges()
        {
            var info = GetUserInfo(this.ID);

            bool addNewUser = false;

            if (info == null)
            {
                addNewUser = true;
                info = new UserInfo();
            }

            info.UserName = this.ID;
            info.State = this.State;
            info.Stats = this.Statistic._stats;
            info.UserName = this.ID;
            info.PremiumEndDate = this._premiumEndDate;

            if (addNewUser)
            {
                AppDbContext.Instance.UsersInfo.Add(info);
            }
            AppDbContext.Instance.SaveChanges();
        }

        private void LoadChanges()
        {
            var info = GetUserInfo(this.ID);

            if (info == null)
            {
                SaveChanges();
            }
            else
            {
                this.State = info.State;
                this.Statistic = new UserStat(info.Stats);
                _premiumEndDate = info.PremiumEndDate;
            }
        }

    }
}