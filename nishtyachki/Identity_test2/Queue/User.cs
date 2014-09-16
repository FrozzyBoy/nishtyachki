﻿using AdminApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using AdminApp.Services;

namespace AdminApp.Queue
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
    
    public class User
    {        
        public string ID { get; private set; }
        public IClient Client { get; set; }
        private UserCurrentState _state;
        public UserCurrentState State 
        {
            get
            {
                return _state;
            }
            set
            {
                UserStats.GetUserStat(ID).UpdateInfo(value,_state);
                _state = value;                
            }
        }

        public Role Role
        {
            get
            {
                if (PremiumEndDate > DateTime.Now)
                {
                    return Queue.Role.premium;
                }
                return Queue.Role.standart;
            }
        }

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

        public User(string id,IClient Client )
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