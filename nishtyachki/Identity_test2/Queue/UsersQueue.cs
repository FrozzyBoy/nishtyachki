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
       private static List<User> _queue= new List<User>();
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
               user.State = UserState.InQueue;
               _queue.Add(user);
               QueueArgs args = new QueueArgs(TypeOfChanges.add);
              OnQueueChanged(user,args);
           }

       }
       public static User GetUser(string id)
       {
           return _queue.Find(m => m.ID == id);
       }

        public  void DeleteFromTheQueueByAdmin(User user)
        {
            lock (LockObj)
            {
                user.State = UserState.Offline;
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
        static void DeleteUser(User user)
        {
            user.State = UserState.Offline;
            _queue.Remove(user);
            UsersQueue.AlertQueue();
        }
        //оповещение пользователей
       static void AlertQueue()
        {
            try
            {
                int i = 0;
                for (; i < Nishtiachok.GetNumOfFreeResources(); i++)
                {
                    if (_queue[i].State == UserState.InQueue)
                    {
                        _queue[i].TellToUse();
                        _queue[i].State = UserState.WaitingForAccept;
                    }
                }
               
                _queue[i+1].TellPossition(i+1);
            }

            catch (IndexOutOfRangeException)
            {
            }
            
        }
       public static void StartUseNishtiak(string id)
       {
            Nishtiachok.GetFreeNishtiachok().owner=UsersQueue.GetUser(id);
            GetUser(id).State = UserState.UsingNishtiak;
       }
        public static void EndUseNishtiak(string id)
       {
           Nishtiachok.GetNishtiakByUserId(id).State = Nishtiachok_State.free;
           GetUser(id).State = UserState.Offline;
           DeleteUser(GetUser(id));
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