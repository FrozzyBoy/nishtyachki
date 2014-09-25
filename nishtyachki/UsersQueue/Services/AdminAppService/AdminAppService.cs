using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace UsersQueue.Services.AdminAppService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AdminAppService" in both code and config file together.
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class AdminAppService : IAdminAppService
    {
        public void AddNishtiak(string id)
        {
            throw new NotImplementedException();
        }

        public void DeleteNishtiak(string id)
        {
            throw new NotImplementedException();
        }

        public void ChangeNishtiakState(string id, int state)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserByAdmin(string id)
        {
            throw new NotImplementedException();
        }

        public void SwitchQueueState()
        {
            throw new NotImplementedException();
        }

        public void UpdateUsersInQueue(string[] userNames)
        {
            throw new NotImplementedException();
        }

        public void SendMsg(string msg, string id)
        {
            throw new NotImplementedException();
        }

        public void ChangeUserRole(string id, int role)
        {
            throw new NotImplementedException();
        }

    }
}
