using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdminApp.Queue;
using AdminApp.Models.Infrastructure;
using System.Collections.Concurrent;
namespace AdminApp.Models
{
    public enum TypeOfUpdate
    {
        standInQueue, leftQueueBeforeUsedNishtyak, beganToUseNishtyak, endedToUseNishtyak
    }
    public class UserInfo : IUserInfo
    {
        private static ConcurrentDictionary<string, UserInfo> _info = new ConcurrentDictionary<string, UserInfo>();
        private static Object LockObj = new Object();
        private DateTime _premiumEndDate;
        private UserStat _statistic;

        private static UserInfo _instance;

        public static UserInfo Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new UserInfo();
                        }
                    }
                }
                return _instance;
            }
        }

        //getter for user
        public UserInfo GetUser(string key)
        {
            lock (LockObj)
            {
                return _info[key];
            }
        }
        public void AddPremium()
        {
            _premiumEndDate = DateTime.Now.AddDays(3);
        }
        private UserInfo()
        {
            _premiumEndDate = DateTime.MinValue;
            _statistic = new UserStat();
        }
        public void UsersUseTimeCount(out DateTime[] time, out int[] count)
        {
            throw new NotImplementedException();
        }
        public bool IsPrime
        {
            get { return DateTime.Now < _premiumEndDate; }
        }
        public void CheckUser(string key)
        {
            if (_info.ContainsKey(key))
            {
                _info[key].UpdateInfo(TypeOfUpdate.standInQueue);
                if (!_info[key].IsPrime)
                {
                    var user = UsersQueue.Instance.GetUser(key);
                    if (user != null)
                    {
                        user.Role = Role.standart;
                    }
                }

            }
            else
            {
                _info.TryAdd(key, new UserInfo());
                _info[key].UpdateInfo(TypeOfUpdate.standInQueue);

            }
        }
        public void UpdateInfo(TypeOfUpdate type)
        {
            _statistic.UpdateInfo(type);
        }

    }
}