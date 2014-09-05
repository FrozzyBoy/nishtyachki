﻿using System;
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
        private List<User> _queue = new List<User>();
        private static UsersQueue _instance;
        public event EventHandler QueueChanged;
        static Object LockObj = new Object();
        public QueueState QueueState { get; private set; }

        private UsersQueue()
        {
            QueueState = QueueState.opened;
            QueueChanged += UsersQueue_QueueChanged;
        }

        private void UsersQueue_QueueChanged(object sender, EventArgs e)
        {
            AlertQueue();
        }

        public static void Lock_Unlock_Queue()
        {
            if (Instance.QueueState == QueueState.locked)
            {
                Instance.QueueState = QueueState.opened;
            }
            else
            {
                Instance.QueueState = QueueState.locked;
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
            bool operationResult = false;

            if (user.State == UserState.Online)
            {
                lock (_queue)
                {
                    if (QueueState == QueueState.opened)
                    {
                        this.AddUser(user);
                        user.UpdateInfo(TypeOfUpdate.StandInQueue);
                        QueueArgs args = new QueueArgs(TypeOfChanges.add);
                        user.Client.StandInQueue();
                        OnQueueChanged(user, args);
                        operationResult = true;                        
                    }
                }
            }

            return operationResult;
        }
        public User GetUser(string id)
        {
            User user = null;
            lock (_queue)
            {
                user = _queue.Find(m => m.ID == id);
            }
            return user;
        }

        public void AddUser(User user)
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
                user.State = UserState.InQueue;
            }

        }
        public void DeleteUserByAdmin(User user)
        {
            DeleteFromTheQueue(user);
            user.Client.DroppedByServer("you're dropped by Admin");
        }

        public void DeleteFromTheQueue(User user)
        {
            lock (_queue)
            {
                switch (user.State)
                {
                    case UserState.InQueue:
                        user.UpdateInfo(TypeOfUpdate.LeftQueueBeforeUsedNishtyak);
                        break;
                    case UserState.AcceptingOffer:
                        user.UpdateInfo(TypeOfUpdate.LeftQueueBeforeUsedNishtyak);
                        Nishtiachok.GetNishtiakByUserId(user.ID).MakeFree();
                        user.Abort();
                        break;
                    case UserState.UsingNishtiak:
                        user.UpdateInfo(TypeOfUpdate.EndedToUseNishtyak);
                        Nishtiachok.GetNishtiakByUserId(user.ID).MakeFree();
                        user.Abort();
                        break;
                }
                user.State = UserState.Online;
                _queue.Remove(user);
                QueueArgs args = new QueueArgs(TypeOfChanges.delete);
                OnQueueChanged(user, args);
            }
        }

        public void ChangeRoleByAdmin(User user, Role needed_role)
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
                OnQueueChanged(user, args);
                UpdateQueue();

            }

        }

        //оповещение пользователей
        public static void AlertQueue()
        {
            foreach (var nishtiak in Nishtiachok.Nishtiachki)
            {
                if (nishtiak.State == Nishtiachok_State.free )
                {
                    lock (Instance._queue)
                    {
                        if (Instance._queue.Count > 0)
                        {
                            foreach (var user in Instance._queue)
                            {
                                if (user.State == UserState.InQueue)
                                {
                                    Instance.DeleteFromTheQueue(user);
                                    nishtiak.owner = user;
                                    nishtiak.State = Nishtiachok_State.wait_for_user;
                                    user.CheckTimeForAcess();
                                    user.State = UserState.AcceptingOffer;
                                    user.Client.OfferToUseObj();
                                    user.Statistic.UpdateInfo(TypeOfUpdate.WaitingForAccept);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void StartUseNishtiak(User user)
        {
            user.Client.NotifyToUseObj();

            user.UpdateInfo(TypeOfUpdate.BeganToUseNishtyak);
            user.Abort();
            user.CheckTimeForUsing();
            var nishtiak = Nishtiachok.GetNishtiakByUserId(user.ID);
            nishtiak.owner = user;
            nishtiak.State = Nishtiachok_State.in_using;
            user.State = UserState.UsingNishtiak;
        }
        public void EndUseNishtiak(User user)
        {
            user.Abort();
            user.UpdateInfo(TypeOfUpdate.EndedToUseNishtyak);
            Nishtiachok.GetNishtiakByUserId(user.ID).State = Nishtiachok_State.free;
            Nishtiachok.GetNishtiakByUserId(user.ID).owner = null;
            user.State = UserState.Online;
            Instance.DeleteFromTheQueue(user);
        }
        //сортировка,вызываемая при изменении роли пользователя
        static void UpdateQueue()
        {
            lock (Instance._queue)
            {
                for (int i = 0; i < Instance._queue.Count; i++)
                {
                    int j = i;
                    while (j - 1 > 0 && (int)Instance._queue[j].Role > (int)Instance._queue[j - 1].Role)
                    {
                        var temp = Instance._queue[j];
                        Instance._queue[j] = Instance._queue[j - 1];
                        Instance._queue[j - 1] = Instance._queue[j];
                        j--;
                    }
                }

            }
        }
        public int GetCount
        {
            get { return _instance._queue.Count; }
        }

        void OnQueueChanged(object obj, QueueArgs args)
        {
            if (QueueChanged != null)
            {
                QueueChanged(obj, args);
            }
        }

        public List<User> Queue
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
                    _queue.AddRange(newUsers.ToList<User>());
                }

            }
        }
    }
}