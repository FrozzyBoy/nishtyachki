using System;
using System.ServiceModel;
using AdminApp.AdminAppService;
using System.Collections.Generic;

namespace AdminApp.QueueChannel
{
    public class QueueProxy : IQueueChannel
    {
        private static object _lockService = new object();
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
                        _service.Ping();
                    }
                }
            }
        }

        public QueueProxy()
        {

        }

        public void AddNishtiak(string id)
        {
            Service.AddNishtiakAsync(id);
        }

        public void DeleteNishtiak(string id)
        {
            Service.DeleteNishtiakAsync(id);
        }

        public void ChangeNishtiakState(string id, int state)
        {
            Service.ChangeNishtiakStateAsync(id, state);
        }

        public void DeleteUserByAdmin(string id)
        {
            Service.DeleteUserByAdminAsync(id);
        }

        public void SwitchQueueState()
        {
            Service.SwitchQueueStateAsync();
        }

        public void UpdateUsersInQueue(string[] userNames)
        {
            Service.UpdateUsersInQueueAsync(userNames);
        }

        public void SendMsg(string msg, string id)
        {
            Service.SendMsgAsync(msg, id);
        }

        public void ChangeUserRole(string id, int role)
        {
            Service.ChangeUserRoleAsync(id, role);
        }

        private List<T> ConvertArrayToList<T>(T[] arr)
        {
            List<T> list = new List<T>();
            list.AddRange(arr);
            return list;
        }

        public List<Nishtiachok> GetAllNishtiaks()
        {
            var recive = Service.GetAllNishtiachoks();
            return ConvertArrayToList<Nishtiachok>(recive);
        }

        public QueueUser GetUserInQueueByID(string id)
        {
            var data = Service.GetUserInQueueByID(id);
            return data;
        }

        public List<QueueUser> GetAllUsersInQueue()
        {
            var recive = Service.GetAllUsersInQueue();
            return ConvertArrayToList<QueueUser>(recive);
        }

        public UserInfo GetUserInfoByID(string id)
        {
            var recive = Service.GetUserInfoByID(id);
            return recive;
        }

        public List<UserInfo> GetAllUsersInfo()
        {
            var recive = Service.GetInfoForAllUsers();
            return ConvertArrayToList<UserInfo>(recive);
        }
    }
}