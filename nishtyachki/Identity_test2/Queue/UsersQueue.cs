using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using AdminApp.Services;
using AdminApp.Nishtiachki;
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
               QueueArgs args = new QueueArgs(TypeOfChanges.add);
              OnQueueChanged(user,args);
           }

       }
       
        public static void DeleteFromTheQueue(User user)
        {
            lock (LockObj)
            {                
                _queue.Remove(user);
                QueueArgs args = new QueueArgs(TypeOfChanges.add);
                OnQueueChanged(user, args);
                AlertQueue();
            }
        }
       
        public static void ChangeRole(User user, Role needed_role)
        {
            lock (LockObj)
            {
                for (int i = 0; i < _queue.Count; i++)
                {
                    if (_queue[i].Equals(user))
                    {
                        _queue[i].Role = needed_role;
                        QueueArgs args = new QueueArgs(TypeOfChanges.change, needed_role);
                        OnQueueChanged(user, args);
                        UpdateQueue(i,needed_role);
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
        static void UpdateQueue(int i,Role changedRole)
        {
            if (changedRole == Role.premium)
            {
                try
                {
                    while (_queue[i - 1].Role != Role.premium)
                    {
                        User temp = _queue[i];
                        _queue[i] = _queue[i - 1];
                        _queue[i - 1] = temp;
                        i--;
                    }

                }
                catch (IndexOutOfRangeException)
                {

                }
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