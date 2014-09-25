﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace UsersQueue.Services.AdminAppService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAdminAppService" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(IAdminAppServiceCallback),
        SessionMode = SessionMode.Required)]
    public interface IAdminAppService
    {
        #region impliments_for_nishtiak
        [OperationContract]
        void AddNishtiak(string id);
        [OperationContract]
        void DeleteNishtiak(string id);
        [OperationContract]
        void ChangeNishtiakState(string id, int state);
        #endregion

        #region impliments_for_queue
        [OperationContract]
        void DeleteUserByAdmin(string id);
        [OperationContract]
        void SwitchQueueState();
        [OperationContract]
        void UpdateUsersInQueue(string[] userNames);
        #endregion

        #region impliments_for_user
        [OperationContract]
        void SendMsg(string msg, string id);
        [OperationContract]
        void ChangeUserRole(string id, int role);
        #endregion
    }
}