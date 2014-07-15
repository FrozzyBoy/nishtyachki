using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using AdminApp.Services;

namespace AdminApp.Queue
{
    public static class UsersQueue
    {
       public static List<User> _queue= new List<User>();
       static Object LockObj = new Object();
        public static void AddUser(User user)
        {                       
           lock( LockObj)
            {
            _queue.Add(user);
        }
        }
       
        public static void DeleteFromTheQueue(string id)
        {
            lock (LockObj)
            {
                _queue.RemoveAll(m => m.ID == id);
                UpdateQueue();
            }
        }
       
        public static void ChangeRole(string id, Role needed_role)
        {
            lock (LockObj)
            {
                foreach (User user in _queue)
                {
                    if (user.ID == id)
                    {
                        user.Role = needed_role;
                    }
                }
            }
        }
        //оповещение пацыков
        public static void UpdateQueue()
        {
            try
            {
                _queue[0].TellToUse();
                _queue[1].TellPossition(2);
            }
            catch (IndexOutOfRangeException)
            {
            }
            
        }
        public static int GetCount
        {
            get { return _queue.Count; }
        }

    }
}