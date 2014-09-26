using System;
using System.Collections.Generic;
using UsersQueue.Services.UserAppService;
using System.Linq;
using UsersQueue;
using System.Runtime.Serialization;
using UsersQueue.Model;

namespace UsersQueue.Queue.UserInformtion
{
    public enum Role
    {
        standart = 0,
        premium = 1
    }
    public enum UserCurrentState
    {
        Offline, Online, InQueue, AcceptingOffer, UsingNishtiak
    }

    [DataContract]
    public class QueueUser
    {
        [DataMember]
        public string ID { get; private set; }
        public IUserAppServiceClient Client { get; set; }

        [DataMember]
        private UserCurrentState _state;
        public UserCurrentState State
        {
            get
            {
                return _state;
            }
            set
            {
                UserStats.GetUserStat(ID).UpdateInfo(value, _state);
                _state = value;
            }
        }

        [DataMember]
        public Role Role
        {
            get
            {
                if (PremiumEndDate > DateTime.Now)
                {
                    return Role.premium;
                }
                return Role.standart;
            }
        }

        [DataMember]
        private DateTime? _premiumEndDate;
        public DateTime PremiumEndDate
        {
            get
            {
                if (_premiumEndDate == null)
                {
                    _premiumEndDate = new DateTime(2000, 1, 1, 1, 1, 1, 1);//если нет премиума, то он кончается в 2000-ом году
                }
                return (DateTime)_premiumEndDate;
            }
            private set
            {
                _premiumEndDate = value;
            }

        }

        public void ChangeRole(Role newRole)
        {
            switch (newRole)
            {
                case Role.standart:
                    this.DeletePremium();
                    break;
                case Role.premium:
                    this.AddPremium();
                    break;
                default:
                    break;
            }
        }

        public static UserInfo GetUserInfo(string id)
        {
            using (var context = new AppDbContext())
            {
                UserInfo oldUser = context.UsersInfo.SingleOrDefault<UserInfo>(u => u.UserName == id);
                return oldUser;
            }
        }

        public static IEnumerable<UserInfo> GetUserInfo()
        {
            using (var context = new AppDbContext())
            {
                return context.UsersInfo.ToArray<UserInfo>();
            }
        }

        public QueueUser(string id, IUserAppServiceClient Client)
        {
            this.ID = id;
            this.Client = Client;

            LoadChanges();

            this.State = UserCurrentState.Online;
        }

        public void AddPremium(int days = 3)
        {
            PremiumEndDate = DateTime.Now.AddDays(days);
            SaveChanges();
        }

        private System.Timers.Timer _t;
        public override bool Equals(Object obj)
        {
            QueueUser user = obj as QueueUser;

            var result = false;

            if (user != null)
            {
                result = (this.ID == user.ID);
            }

            return result;
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
            UsersQueueInstance.Instance.DeleteFromTheQueue(this);
        }

        internal void DeletePremium()
        {
            _premiumEndDate = null;
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
            info.UserName = this.ID;
            info.PremiumEndDate = this.PremiumEndDate;

            using (var context = new AppDbContext())
            {
                if (addNewUser)
                {
                    context.UsersInfo.Add(info);
                }
                context.SaveChanges();
            }
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
                this.PremiumEndDate = (DateTime)info.PremiumEndDate;
            }
        }

    }
}