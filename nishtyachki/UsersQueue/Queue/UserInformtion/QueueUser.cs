﻿using System;
using System.Collections.Generic;
using UsersQueue.Services.UserAppService;
using System.Linq;
using UsersQueue.Model;
using UsersQueue.Services.TransferObjects;
using UsersQueue.Queue.Statistics;
using System.Reflection;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace UsersQueue.Queue.UserInformtion
{
    public enum Role : int
    {
        standart = 0,
        premium = 1
    }

    public enum UserCurrentState : int
    {
        Offline, Online, InQueue, AcceptingOffer, UsingNishtiak
    }

    public class QueueUser
    {
        public string ID { get; private set; }
        public IUserAppServiceClient Client { get; set; }

        private UserCurrentState _state;

        public event Action ChangedState;

        public UserCurrentState State
        {
            get
            {
                return _state;
            }
            set
            {
                UserStats.GetUserStat(ID).UpdateInfo(value, _state);

                if (value != UserCurrentState.UsingNishtiak && value != UserCurrentState.AcceptingOffer)
                {
                    var nishtiak = Nishtiachki.Nishtiachok.GetNishtiakByUserId(this.ID);
                    if (nishtiak != null)
                    {
                        nishtiak.MakeFree();
                    }
                }
                                
                _state = value;

                if (ChangedState != null)
                {
                    ChangedState();
                }

                this.SaveChanges();
            }
        }

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

        private DateTime? _premiumEndDate;

        public DateTime PremiumEndDate
        {
            get
            {
                if (_premiumEndDate == null)
                {
                    _premiumEndDate = DefaultPremiumEndDate;
                }
                return (DateTime)_premiumEndDate;
            }
            private set
            {
                _premiumEndDate = value;
                this.SaveChanges();
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
                    return;
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
            if (_t != null)
            {
                _t.Stop();
            }
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
                else
                {
                    context.UsersInfo.Attach(info);
                    var entry = context.Entry<UserInfo>(info);

                    Type queueUser = typeof(UserInfo);

                    foreach (var propertyInfo in queueUser.GetProperties())
                    {
                        if (propertyInfo.GetCustomAttribute<KeyAttribute>() == null 
                            && propertyInfo.GetCustomAttribute<DataMemberAttribute>() != null)
                            entry.Property(propertyInfo.Name).IsModified = true;
                    }
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
                this.State = info.State;
            }
        }


        public QueueUserTransferObject GetQueueUserTransferObject()
        {
            QueueUserTransferObject result = new QueueUserTransferObject();

            result.ID = this.ID;
            result.PremiumEndDate = this.PremiumEndDate;
            result.Role = (int)this.Role;
            result.State = (int)this.State;

            return result;
        }

        public static DateTime DefaultPremiumEndDate
        {
            get
            {
                return new DateTime(2000, 1, 1, 1, 1, 1, 1);//если нет премиума, то он кончается в 2000-ом году
            }
        }
    }
}