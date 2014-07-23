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
        opened, locked
    }
    public class UsersQueue
    {
        private static List<User> _queue = new List<User>();
        private static UsersQueue _instance;
        public event EventHandler QueueChanged;
        static Object LockObj = new Object();
        public QueueState _QueueState { get; private set; }

        private UsersQueue()
        {
            _QueueState = QueueState.opened;
        }
        public static void Lock_Unlock_Queue()
        {
            if (Instance._QueueState == QueueState.locked)
            {
                Instance._QueueState = QueueState.opened;
            }
            else
            {
                Instance._QueueState = QueueState.locked;
            }
        }
        public static UsersQueue Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new UsersQueue();

                        }
                    }
                }
                return _instance;
            }
        }

        public bool AddUserInQueue(User user)
        {
            lock (LockObj)
            {
                if (Instance._QueueState == QueueState.opened)
                {
                    try
                    {
                        user.iClient.StandInQueue();
                    }
                    catch (Exception ex)
                    {
                        user.iClient.ShowMessage("callback exception stand in queue: " + ex.Message);
                    }
                    this.AddUser(user);
                    UserInfo.Instance.CheckUser(user.ID);
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
        public User GetUser(string id)
        {
            return _queue.Find(m => m.ID == id);
        }

        public void AddUser(User user)
        {
            var oldUser = Instance.GetUser(user.ID);

            if (oldUser == null)
            {
                _queue.Add(user);
            }
            else
            {
                _queue.Remove(oldUser);
                _queue.Add(user);
            }
        }
        public void DeleteUserByAdmin(User user)
        {
            user.iClient.DroppedByServer("you're dropped by Admin");
            DeleteFromTheQueue(user);

        }
        public void DeleteFromTheQueue(User user)
        {
            lock (LockObj)
            {
                switch (user.State)
                {
                    case UserState.InQueue:
                        UserInfo.Instance.GetUser(user.ID).UpdateInfo(TypeOfUpdate.leftQueueBeforeUsedNishtyak);
                        break;
                    case UserState.WaitingForAccept:
                        UserInfo.Instance.GetUser(user.ID).UpdateInfo(TypeOfUpdate.leftQueueBeforeUsedNishtyak);
                        break;
                    case UserState.UsingNishtiak:
                        UserInfo.Instance.GetUser(user.ID).UpdateInfo(TypeOfUpdate.endedToUseNishtyak);
                        user.Abort();
                        break;
                }
                user.State = UserState.Offline;
                _queue.Remove(user);
                QueueArgs args = new QueueArgs(TypeOfChanges.delete);
                OnQueueChanged(user, args);
                AlertQueue();
            }
        }

        public void ChangeRoleByAdmin(User user, Role needed_role)
        {
            lock (LockObj)
            {
                //если роль юзера изменилась на премиум, то надо добавить ему 3 дня
                if (needed_role == Role.premium)
                { UserInfo.Instance.GetUser(user.ID).AddPremium(); }

                for (int i = 0; i < _queue.Count; i++)
                {
                    if (_queue[i].Equals(user))
                    {
                        _queue[i].Role = needed_role;
                        QueueArgs args = new QueueArgs(TypeOfChanges.change, needed_role);
                        OnQueueChanged(user, args);
                        UpdateQueue(i, needed_role);
                        break;
                    }
                }
            }

        }

        //оповещение пользователей
        static public void AlertQueue()
        {
                int i = 0;
                for (; i < Nishtiachok.GetNumOfFreeResources(); i++)
                {
                    if (i < _queue.Count)
                    {
                        if ((_queue[i].State == UserState.InQueue))
                        {
                            _queue[i].CheckTimeForAcess();

                            try
                            {
                                _queue[i].iClient.OfferToUseObj();
                            }
                            catch (Exception ex)
                            {
                                _queue[i].iClient.ShowMessage("callback exception offer to use: " + ex.Message);
                            }

                            _queue[i].State = UserState.WaitingForAccept;
                        }
                    }
                }
                if (i + 1 < _queue.Count && (_queue[i + 1].State == UserState.InQueue))
                {
                    _queue[i + 1].iClient.ShowMessage("You'r next to USE");
                }                     

        }
        public void StartUseNishtiak(string id)
        {
            try
            {
                Instance.GetUser(id).iClient.NotifyToUseObj();
            }
            catch (Exception ex)
            {
                Instance.GetUser(id).iClient.ShowMessage("callback exception notify to use: " + ex.Message);
            }

            UserInfo.Instance.GetUser(id).UpdateInfo(TypeOfUpdate.beganToUseNishtyak);
            Instance.GetUser(id).Abort();
            Instance.GetUser(id).CheckTimeForUsing();
            Nishtiachok.GetFreeNishtiachok().owner = Instance.GetUser(id);
            Instance.GetUser(id).State = UserState.UsingNishtiak;
        }
        public void EndUseNishtiak(string id)
        {
            Instance.GetUser(id).Abort();
            Instance.GetUser(id).iClient.ShowMessage("EndUseNishtiak for user");
            UserInfo.Instance.GetUser(id).UpdateInfo(TypeOfUpdate.endedToUseNishtyak);          
            Nishtiachok.GetNishtiakByUserId(id).State = Nishtiachok_State.free;
            Nishtiachok.GetNishtiakByUserId(id).owner = null;
            Instance.GetUser(id).State = UserState.Offline;
            Instance.DeleteFromTheQueue(Instance.GetUser(id));
        }
        //сортировка,вызываемая при изменении роли пользователя
        static void UpdateQueue(int i, Role changedRole)
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
        public int GetCount
        {
            get { return _queue.Count; }
        }

        void OnQueueChanged(object obj, QueueArgs args)
        {
            if (QueueChanged != null)
            {
                QueueChanged(obj, args);
            }
        }
        static public List<User> Queue
        {
            get
            {
                return _queue;
            }
        }

    }
}