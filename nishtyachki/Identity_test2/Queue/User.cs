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
        public IClient iClient { get; set; }
        //public string UserName { get; private set; }
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
            int key = id.GetHashCode();
            UserInfo oldUser = AppDbContext.Instance.UsersInfo.SingleOrDefault<UserInfo>(u => u.ID == key);
            return oldUser;
        }

        public static IEnumerable<UserInfo> GetUserInfo()
        {
            return AppDbContext.Instance.UsersInfo.ToArray<UserInfo>();
        }

        public User(string id,IClient IClient )
        {
            UserInfo oldUser = GetUserInfo(id);

            if (oldUser != null)
            {
                AppDbContext.Instance.UsersInfo.Remove(oldUser);
                _premiumEndDate = oldUser.PremiumEndDate;
                Statistic = new UserStat(oldUser.Stats);
            }
            else
            {
                _premiumEndDate = DateTime.MinValue;
                Statistic = new UserStat();
                oldUser = new UserInfo();
                oldUser.ID = id.GetHashCode();
                oldUser.PremiumEndDate = _premiumEndDate;
                oldUser.Stats = new List<Stats>();
            }

            this.ID = id;
            this.iClient = IClient;
            this.State = UserState.Online;
            oldUser.State = UserState.Online;

            AppDbContext.Instance.UsersInfo.Add(oldUser);
            AppDbContext.Instance.SaveChanges();
        }

        public void UpdateInfo(TypeOfUpdate type)
        {
            Statistic.UpdateInfo(type);
            AppDbContext.Instance.SaveChanges();
        }

        public void AddPremium(int days = 3)
        {
            _premiumEndDate = DateTime.Now.AddDays(days);
            AppDbContext.Instance.SaveChanges();
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
            iClient.DroppedByServer("you are dropepd");
            UsersQueue.Instance.DeleteFromTheQueue(this);
        }

        internal void DeletePremium()
        {
            _premiumEndDate = DateTime.MinValue;
            AppDbContext.Instance.SaveChanges();
        }
    }
}