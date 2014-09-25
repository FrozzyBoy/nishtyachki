using System;
using System.ServiceModel;
using AdminApp.AdminAppService;

namespace AdminApp.QueueChannel
{
    public class QueueProxy : IQueueChannel
    {
        private static object _lockService = new object();
        private static IAdminAppService _service;

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
                        IAdminAppServiceCallback callback = new CallBackAdminApp();
                        InstanceContext ic = new InstanceContext(callback);
                        _service = new AdminAppServiceClient(ic);
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
    }
}