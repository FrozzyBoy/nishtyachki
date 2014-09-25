using AdminApp.AdminAppService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminApp.QueueChannel
{
    public interface IQueueChannel
    {
        void AddNishtiak(string id);
        void DeleteNishtiak(string id);
        void ChangeNishtiakState(string id, int state);
        void DeleteUserByAdmin(string id);
        void SwitchQueueState();
        void UpdateUsersInQueue(string[] userNames);
        void SendMsg(string msg, string id);
        void ChangeUserRole(string id, int role);
    }
}
