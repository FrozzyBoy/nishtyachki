using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UsersQueue.Queue.Nishtiachki;
using UsersQueue.Queue.UserInformtion;

namespace UsersQueue.Queue
{
    [DataContract]
    public enum QueueState:int
    {
        opened, locked
    }

    public class UsersQueueInstance
    {
        private List<QueueUser> _queue = new List<QueueUser>();
        private static UsersQueueInstance _instance;
        public static event Action QueueChanged;
        static Object LockObj = new Object();
        public QueueState QueueState { get; private set; }

        private static bool _onqueueChange;

        private UsersQueueInstance()
        {
            QueueState = QueueState.opened;
            QueueChanged += UsersQueue_QueueChanged;
            _onqueueChange = false;
            Nishtiachok.EventChangeNisht += UsersQueue_QueueChanged;
        }

        private static void UsersQueue_QueueChanged()
        {
            AlertQueue();
        }

        public static void SwitchQueueState()
        {
            QueueArgs queueChange = null;

            if (Instance.QueueState == QueueState.locked)
            {
                Instance.QueueState = QueueState.opened;
                queueChange = new QueueArgs(TypeOfChanges.opened);
            }
            else
            {
                Instance.QueueState = QueueState.locked;
                queueChange = new QueueArgs(TypeOfChanges.blocked);
            }

            OnQueueChanged();

        }
        
        public static UsersQueueInstance Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (LockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new UsersQueueInstance();
                        }
                    }
                }
                return _instance;
            }
        }

        public bool AddUserInQueue(QueueUser user)
        {
            bool operationResult = false;

            if (user.State == UserCurrentState.Online)
            {
                lock (_queue)
                {
                    if (QueueState == QueueState.opened)
                    {
                        this.AddUser(user);
                        QueueArgs args = new QueueArgs(TypeOfChanges.add);
                        user.Client.StandInQueue();
                        user.State = UserCurrentState.InQueue;
                        OnQueueChanged();
                        operationResult = true;
                    }
                }
            }

            return operationResult;
        }
        public QueueUser GetUser(string id)
        {
            QueueUser user = null;
            lock (_queue)
            {
                user = _queue.Find(m => m.ID == id);
            }
            return user;
        }

        public void AddUser(QueueUser user)
        {
            var oldUser = Instance.GetUser(user.ID);

            lock (_queue)
            {
                if (oldUser == null)
                {
                    _queue.Add(user);
                }
                else
                {
                    _queue.Remove(oldUser);
                    _queue.Add(user);
                }
                user.State = UserCurrentState.InQueue;
            }

        }

        public void DeleteUserByAdmin(string id)
        {
            var user = this.GetUser(id);
            DeleteFromTheQueue(user);
            user.Client.DroppedByServer("you're dropped by Admin");
        }

        public void DeleteFromTheQueue(QueueUser user)
        {
            lock (_queue)
            {
                switch (user.State)
                {
                    case UserCurrentState.AcceptingOffer:
                        Nishtiachok.GetNishtiakByUserId(user.ID).MakeFree();
                        user.Abort();
                        break;
                    case UserCurrentState.UsingNishtiak:
                        Nishtiachok.GetNishtiakByUserId(user.ID).MakeFree();
                        user.Abort();
                        break;
                }
                user.State = UserCurrentState.Online;
                _queue.Remove(user);
                QueueArgs args = new QueueArgs(TypeOfChanges.delete);
                OnQueueChanged();
            }
        }

        public void ChangeRoleByAdmin(QueueUser user, Role needed_role)
        {
            lock (_queue)
            {
                if (needed_role == Role.premium)
                {
                    user.AddPremium();
                }
                if (needed_role == Role.standart)
                {
                    user.DeletePremium();
                }

                QueueArgs args = new QueueArgs(TypeOfChanges.change, needed_role);
                OnQueueChanged();
                UpdateQueue();

            }

        }

        //оповещение пользователей
        private static void AlertQueue()
        {
            foreach (var nishtiak in Nishtiachok.Nishtiachki)
            {
                if (nishtiak.State == Nishtiachok_State.free)
                {
                    lock (Instance.Queue)
                    {
                        for (int i = 0; i < Instance.Queue.Count; i++)
                        {
                            var user = Instance.Queue[i];
                            if (user.State == UserCurrentState.InQueue)
                            {
                                Instance.DeleteFromTheQueue(user);
                                nishtiak.SetOwner(user);
                                nishtiak.ChangeNishtState(Nishtiachok_State.wait_for_user);
                                user.CheckTimeForAcess();
                                user.State = UserCurrentState.AcceptingOffer;
                                user.Client.OfferToUseObj();
                                i--;
                            }
                        }
                    }
                }
            }
        }

        public void StartUseNishtiak(QueueUser user)
        {
            user.Client.NotifyToUseObj();

            user.Abort();
            user.CheckTimeForUsing();
            var nishtiak = Nishtiachok.GetNishtiakByUserId(user.ID);
            nishtiak.SetOwner(user);
            nishtiak.ChangeNishtState(Nishtiachok_State.in_using);
            user.State = UserCurrentState.UsingNishtiak;
        }

        public void EndUseNishtiak(QueueUser user)
        {
            user.Abort();
            var nishtiak = Nishtiachok.GetNishtiakByUserId(user.ID);

            if (nishtiak != null)
            {
                nishtiak.MakeFree();
            }

            user.State = UserCurrentState.Online;
            Instance.DeleteFromTheQueue(user);
        }

        //сортировка,вызываемая при изменении роли пользователя
        static void UpdateQueue()
        {
            lock (Instance.Queue)
            {
                for (int i = 0; i < Instance._queue.Count; i++)
                {
                    int j = i;
                    while (j - 1 > 0 && (int)Instance.Queue[j].Role > (int)Instance.Queue[j - 1].Role)
                    {
                        var temp = Instance.Queue[j];
                        Instance._queue[j] = Instance._queue[j - 1];
                        Instance._queue[j - 1] = Instance._queue[j];
                        j--;
                    }
                }

            }
        }

        public int GetCount
        {
            get { return _instance.Queue.Count; }
        }

        private static void OnQueueChanged()
        {
            if (!_onqueueChange)
            {
                _onqueueChange = true;
                try
                {
                    if (QueueChanged != null)
                    {
                        QueueChanged();
                    }
                }
                finally
                {
                    _onqueueChange = false;
                }
            }
        }
        
        public List<QueueUser> Queue
        {
            get
            {
                return _queue;
            }
        }

        internal void UpdateQueue(string[] userNames)
        {
            if (userNames != null && userNames.Length > 0)
            {
                var newUsers = from name in userNames
                               join user in _queue on name equals user.ID
                               select user;

                lock (_queue)
                {
                    _queue.Clear();
                    _queue.AddRange(newUsers.ToList<QueueUser>());
                }

            }
        }

    }
}
