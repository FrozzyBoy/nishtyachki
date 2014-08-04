using AdminApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Timers;
using AdminApp.Services;
using System.Collections.Concurrent;

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
        private static ConcurrentDictionary<string, User> _allUsers = new ConcurrentDictionary<string, User>();

        public IClient iClient { get; set; }
        public string ID { get; private set; }
        public string UserName { get; private set; }
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

        public static User GetUser(string id)
        {
            User oldUser;
            _allUsers.TryGetValue(id, out oldUser);

            return oldUser;
        }

        public static IEnumerable<User> GetAllUsers()
        {
            return _allUsers.Values;
        }

        public User(string id,IClient IClient )
        {
            User oldUser;
            if (_allUsers.TryGetValue(id, out oldUser))
            {
                _premiumEndDate = oldUser._premiumEndDate;
                Statistic = oldUser.Statistic;
            }
            else
            {
                _premiumEndDate = DateTime.MinValue;
                Statistic = new UserStat();
            }

            this.ID = id;
            this.iClient = IClient;
            this.State = UserState.Online;

            _allUsers.AddOrUpdate(id, this, (key, user) => user);
        }

        public void UpdateInfo(TypeOfUpdate type)
        {
            Statistic.UpdateInfo(type);
        }

        public void AddPremium(int days = 3)
        {
            _premiumEndDate = DateTime.Now.AddDays(days);
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
        }
    }
}