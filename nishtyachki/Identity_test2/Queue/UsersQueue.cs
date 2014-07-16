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
       public static event EventHandler QueueChanged;
       static Object LockObj = new Object();


       public static void AddUser(User user)
       {
           lock (LockObj)
           {
               _queue.Add(user);
              
           }

       }
       
        public static void DeleteFromTheQueue(string id)
        {
            lock (LockObj)
            {
                _queue.RemoveAll(m => m.ID == id);
                AlertQueue();
            }
        }
       
        public static void ChangeRole(string id, Role needed_role)
        {
            lock (LockObj)
            {
                for (int i = 0; i < _queue.Count; i++)
                {
                    if (_queue[i].ID == id)
                    {
                        _queue[i].Role = needed_role;
                        UpdateQueue(i);
                        break;
                    }
                }
            }
            
        }
        //оповещение пользователей
        public static void AlertQueue()
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
        //сортировка,вызываемая при изменении роли пользователя
        static void UpdateQueue(int i)
        {

            try
            {
                while(_queue[i-1].Role!=Role.premium)
                {
                    User temp = _queue[i];
                    _queue[i] = _queue[i - 1];
                    _queue[i - 1] = temp;
                    i--;
                }

            }
            catch(IndexOutOfRangeException)
            {

            }
        }
        public static int GetCount
        {
            get { return _queue.Count; }
        }
       
        static void OnQueueChanged(object obj,QueueArgs args )
        {
             if(QueueChanged!=null)
             {
                 QueueChanged(obj, args);
             }
        }
    }
}