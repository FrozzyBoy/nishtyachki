using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using AdminApp.Services;
using AdminApp.Nishtiachki;
using AdminApp.Models;
namespace AdminApp.Queue
{
    public  class UsersQueue
    {
       public static List<User> _queue= new List<User>();
       private static UsersQueue _instance ;
       public  event EventHandler QueueChanged;
       static Object LockObj = new Object();


         private UsersQueue()
       {
       }
         public static UsersQueue Instance
         {
             get
             {
                 
                 lock (LockObj)
                 {
                    
                     if (_instance == null)
                     {
                         _instance = new UsersQueue();
                        
                     }
                 }
                 return _instance;
             }
         }

       public  void AddUserInQueue(User user)
       {
           lock (LockObj)
           {               
               UserInfo.CheckUser(user.ID);
               _queue.Add(user);
               QueueArgs args = new QueueArgs(TypeOfChanges.add);
              OnQueueChanged(user,args);
           }

       }
       public User GetUser(string id)
       {
           return _queue.Find(m => m.ID == id);
       }

        public  void DeleteFromTheQueue(User user)
        {
            lock (LockObj)
            {                
                _queue.Remove(user);
                QueueArgs args = new QueueArgs(TypeOfChanges.add);
                OnQueueChanged(user, args);
                AlertQueue();
            }
        }
       
        public  void ChangeRoleByAdmin(User user, Role needed_role)
        {
            lock (LockObj)
            {
                //если роль юзера изменилась на премиум, то надо добавить ему 3 дня
                if (needed_role==Role.premium)
                { UserInfo.GetUser(user.ID).AddPremium(); }

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
        public  void AlertQueue()
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
        public  int GetCount
        {
            get { return _queue.Count; }
        }
       
         void OnQueueChanged(object obj,QueueArgs args )
        {
             if(QueueChanged!=null)
             {
                 QueueChanged(obj, args);
             }
        }
    }
}