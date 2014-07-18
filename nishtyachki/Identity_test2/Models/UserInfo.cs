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
        standInQueue,leftQueue,beganToUseNishtyak,endedToUseNishtyak
    }
    public class UserInfo:IUserInfo
    {
        static ConcurrentDictionary<string,UserInfo> _info = new ConcurrentDictionary<string,UserInfo>();       
        static Object LockObj = new Object();
        DateTime _premiumEndDate;
        UserStat _statistic;       
      //getter for user
       public static UserInfo GetUser(string key)
        {
            lock (LockObj)
            {
                
                    return _info[key];
                                
            }
        }
        public void AddPremium()
        {
            _premiumEndDate=DateTime.Now.AddDays(3);
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
          public static void CheckUser(string key)
       {
           if(_info.ContainsKey(key))
           {
               _info[key].UpdateInfo(TypeOfUpdate.beganToUseNishtyak);
               if (!_info[key].IsPrime)
               {
                   UsersQueue.GetUser(key).Role = Role.standart;
               }

           }
           else
           {            
               _info.TryAdd(key, new UserInfo());
               _info[key].UpdateInfo(TypeOfUpdate.beganToUseNishtyak);
              
           }
       }
       public  void UpdateInfo(TypeOfUpdate type)
          {
              _statistic.UpdateInfo(type);
          }
        
    }
}