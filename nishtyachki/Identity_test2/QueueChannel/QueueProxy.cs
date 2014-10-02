using System;
using System.ServiceModel;
using AdminApp.AdminAppService;
using System.Collections.Generic;

namespace AdminApp.QueueChannel
{
    public class QueueProxy : IQueueChannel
    {
        private static object _lockService = new object();
        private static object _lockCommunicate = new object();
        private static IAdminAppService _service;

        private static IAdminAppServiceCallback _callback;

        private static IAdminAppService Service
        {
            get
            {
                InitService();

                try
                {
                    _service.Ping();
                }
                catch (Exception)
                {
                    _service = null;
                    InitService();
                    _service.Ping();
                }

                return _service;
            }
        }

        private static void InitService()
        {
            if (_service == null)
            {
                lock (_lockService)
                {
                    if (_service == null)
                    {
                        _callback = new CallBackAdminApp();
                        InstanceContext ic = new InstanceContext(_callback);
                        _service = new AdminAppServiceClient(ic);
                        _service.Init();
                    }
                }
            }
        }

        public QueueProxy()
        {
        }

        public void AddNishtiak(string id)
        {
            lock (_lockCommunicate)
            {
                Service.AddNishtiak(id);
            }
        }

        public void DeleteNishtiak(string id)
        {
            lock (_lockCommunicate)
            {
                Service.DeleteNishtiak(id);
            }
        }

        public void ChangeNishtiakState(string id, int state)
        {
            lock (_lockCommunicate)
            {
                Service.ChangeNishtiakState(id, state);
            }
        }

        public void DeleteUserByAdmin(string id)
        {
            lock (_lockCommunicate)
            {
                Service.DeleteUserByAdmin(id);
            }
        }

        public void SwitchQueueState()
        {
            lock (_lockCommunicate)
            {
                Service.SwitchQueueState();
            }
        }

        public void UpdateUsersInQueue(string[] userNames)
        {
            lock (_lockCommunicate)
            {
                Service.UpdateUsersInQueue(userNames);
            }
        }

        public void SendMsg(string msg, string id)
        {
            lock (_lockCommunicate)
            {
                Service.SendMsg(msg, id);
            }
        }

        public void ChangeUserRole(string id, int role)
        {
            lock (_lockCommunicate)
            {
                Service.ChangeUserRole(id, role);
            }
        }

        private List<T> ConvertArrayToList<T>(T[] arr)
        {
            List<T> list = new List<T>();
            if (arr != null)
            {
                list.AddRange(arr);
            }
            return list;
        }

        public List<Nishtiachok> GetAllNishtiaks()
        {
            lock (_lockCommunicate)
            {
                var recive = Service.GetAllNishtiachoks();
                return ConvertArrayToList<Nishtiachok>(recive);
            }
        }

        public QueueUser GetUserInQueueByID(string id)
        {
            lock (_lockCommunicate)
            {
                var data = Service.GetUserInQueueByID(id);
                return data;
            }
        }

        public List<QueueUser> GetAllUsersInQueue()
        {
            lock (_lockCommunicate)
            {
                var recive = Service.GetAllUsersInQueue();
                return ConvertArrayToList<QueueUser>(recive);
            }
        }

        public UserInfo GetUserInfoByID(string id)
        {
            lock (_lockCommunicate)
            {
                var recive = Service.GetUserInfoByID(id);
                return recive;
            }
        }

        public List<UserInfo> GetAllUsersInfo()
        {
            lock (_lockCommunicate)
            {
                var recive = Service.GetInfoForAllUsers();
                return ConvertArrayToList<UserInfo>(recive);
            }
        }

        public int GetQueueState()
        {
            int recive = 0;
            lock (_lockCommunicate)
            {
                recive = Service.GetQueueState();
            }
            return recive;
        }

        public object GetQueueInstance()
        {
            List<QueueUser> queue = GetAllUsersInQueue();
            int queueState = GetQueueState();
            
            var recive = new { QueueState = queueState, Queue = queue };
            return recive;
        }

        public ChartValues GetStatisticsGeneralWasMoreThenAthoresInState(int state)
        {
            var result = Service.GetStatisticsGeneralWasMoreThenAthoresInState(state);
            return result;
        }

        public ChartValues GetStatisticsPersonal(string userID)
        {
            var result = Service.GetStatisticsPersonal(userID);
            return result;
        }

    }
}