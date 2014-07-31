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
        private  List<User> _queue = new List<User>();
        private static UsersQueue _instance;
        public event EventHandler QueueChanged;
        static Object LockObj = new Object();
        public QueueState _QueueState { get; private set; }

        private UsersQueue()
        {
            _QueueState = QueueState.opened;
            QueueChanged += UsersQueue_QueueChanged;
        }

        void UsersQueue_QueueChanged(object sender, EventArgs e)
        {
            AlertQueue();
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
            bool operationResult = false;

            if (user.State == UserState.Online)
            {
                lock (LockObj)
                {
                    if (Instance._QueueState == QueueState.opened)
                    {
                        this.AddUser(user);
                        user.UpdateInfo(TypeOfUpdate.StandInQueue);
                        QueueArgs args = new QueueArgs(TypeOfChanges.add);
                        OnQueueChanged(user, args);
                        operationResult = true;
                    }
                }
            }

            return operationResult;
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
                        user.UpdateInfo(TypeOfUpdate.LeftQueueBeforeUsedNishtyak);
                        break;
                    case UserState.WaitingForAccept:
                        user.UpdateInfo(TypeOfUpdate.LeftQueueBeforeUsedNishtyak);
                        break;
                    case UserState.UsingNishtiak:
                        user.UpdateInfo(TypeOfUpdate.EndedToUseNishtyak);
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
            lock (LockObj)
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
        static public void AlertQueue()
        {

            foreach (var nishtiak in Nishtiachok.Nishtiachki)
            {
                if (nishtiak.State == Nishtiachok_State.free)
                {
                    foreach (var user in Instance._queue)
                    {
                        user.CheckTimeForAcess();
                        user.iClient.OfferToUseObj();
                        user.Statistic.UpdateInfo(TypeOfUpdate.WaitingForAccept);
                    }
                }
            }
        }

        public void StartUseNishtiak(string id)
        {
            Instance.GetUser(id).iClient.NotifyToUseObj();

            User.GetUser(id).UpdateInfo(TypeOfUpdate.BeganToUseNishtyak);
            Instance.GetUser(id).Abort();
            Instance.GetUser(id).CheckTimeForUsing();
            Nishtiachok.GetFreeNishtiachok().owner = Instance.GetUser(id);
            Instance.GetUser(id).State = UserState.UsingNishtiak;

            //DeleteFromTheQueue(GetUser(id));
        }
        public void EndUseNishtiak(string id)
        {
            Instance.GetUser(id).Abort();
            User.GetUser(id).UpdateInfo(TypeOfUpdate.EndedToUseNishtyak);          
            Nishtiachok.GetNishtiakByUserId(id).State = Nishtiachok_State.free;
            Nishtiachok.GetNishtiakByUserId(id).owner = null;
            Instance.GetUser(id).State = UserState.Online;
            Instance.DeleteFromTheQueue(Instance.GetUser(id));
        }
        //сортировка,вызываемая при изменении роли пользователя
        static void UpdateQueue()
        {
            for (int i = 0; i < Instance._queue.Count; i++)
            {
                int j = i;
                while (j - 1 > 0 && (int)Instance._queue[j].Role > (int)Instance._queue[j-1].Role)
                {
                    var temp = Instance._queue[j];
                    Instance._queue[j] = Instance._queue[j - 1];
                    Instance._queue[j - 1] = Instance._queue[j];
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

    }
}