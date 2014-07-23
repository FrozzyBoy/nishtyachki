using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Web;
using AdminApp.Services;
using AdminApp.Nishtiachki;
using AdminApp.Models;
using System.Threading;

namespace AdminApp.Queue
{
   public enum QueueState
    {
        opened,locked
    }
    public  class UsersQueue
    {
       public static List<User> _queue= new List<User>();
       private static UsersQueue _instance ;
       public  event EventHandler QueueChanged;
       static Object LockObj = new Object();
       public QueueState _QueueState {get;private set;} 

         private UsersQueue()
       {
           _QueueState = QueueState.opened;
       }
        public static void Lock_Unlock_Queue()
         {
            if(_instance._QueueState==QueueState.locked)
            {
                _instance._QueueState = QueueState.opened;
            }
            else
            {
                _instance._QueueState = QueueState.locked;
            }
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

       public  bool AddUserInQueue(User user)
       {
           lock (LockObj)
           {
               if (Instance._QueueState==QueueState.opened)
               {
                   _queue.Add(user);
                   UserInfo.CheckUser(user.ID);
                   user.State = UserState.InQueue;
                   QueueArgs args = new QueueArgs(TypeOfChanges.add);
                   OnQueueChanged(user, args);
                   AlertQueue();
                   return true;
               }
               else
               {
                   return false;
               }
           }

       }
       public static User GetUser(string id)
       {
           return _queue.Find(m => m.ID == id);
       }

       

       public  void DeleteFromTheQueue(User user)
        {
            lock (LockObj)
            {
                switch (user.State)
                {
                    case UserState.InQueue:
                        UserInfo.GetUser(user.ID).UpdateInfo(TypeOfUpdate.leftQueueBeforeUsedNishtyak);
                        break;
                    case UserState.WaitingForAccept:
                        UserInfo.GetUser(user.ID).UpdateInfo(TypeOfUpdate.leftQueueBeforeUsedNishtyak);
                        break;
                    case UserState.UsingNishtiak:
                        UserInfo.GetUser(user.ID).UpdateInfo(TypeOfUpdate.endedToUseNishtyak);
                        break; 
                }
                user.State = UserState.Offline;
                _queue.Remove(user);
                QueueArgs args = new QueueArgs(TypeOfChanges.delete);
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
       static public void AlertQueue()
        {
            try
            {
                int i = 0;
                for (; i < Nishtiachok.GetNumOfFreeResources(); i++)
                {
                    if (_queue[i].State == UserState.InQueue)
                    {                                                                       
                        _queue[i].ThreadForCheckAnswerTime = new Thread(new ThreadStart(_queue[i].CheckTimeForAcess));
                        _queue[i].ThreadForCheckAnswerTime.Start();
                        _queue[i].iClient.NotifyToUseObj();
                        _queue[i].State = UserState.WaitingForAccept;
                    }
                }
                if (_queue[i+1].State == UserState.InQueue)
                {
                    _queue[i + 1].iClient.ShowMessage("You'r next to USE");
                }
            }

            catch (Exception)
            {
                
            }
            
        }
       public static void StartUseNishtiak(string id)
       {
           UserInfo.GetUser(id).UpdateInfo(TypeOfUpdate.beganToUseNishtyak);
           GetUser(id).ThreadForCheckAnswerTime.Abort();
           GetUser(id).ThreadForCheckUsingTime = new Thread(new ThreadStart(GetUser(id).CheckTimeForUsing));
           GetUser(id).ThreadForCheckUsingTime.Start();
           Nishtiachok.GetFreeNishtiachok().owner=UsersQueue.GetUser(id);
            GetUser(id).State = UserState.UsingNishtiak;
       }
        public static void EndUseNishtiak(string id)
       {
           UserInfo.GetUser(id).UpdateInfo(TypeOfUpdate.endedToUseNishtyak);
           GetUser(id).ThreadForCheckUsingTime.Abort();
           Nishtiachok.GetNishtiakByUserId(id).State = Nishtiachok_State.free;
           Nishtiachok.GetNishtiakByUserId(id).owner = null;
           GetUser(id).State = UserState.Offline;
           Instance.DeleteFromTheQueue(GetUser(id));
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