using System.ServiceModel;
using UsersQueue.Queue.Nishtiachki;
using UsersQueue.Queue.UserInformtion;
using UsersQueue.Services.TransferObjects;

namespace UsersQueue.Services.AdminAppService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAdminAppService" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(IAdminAppServiceCallback),
        SessionMode = SessionMode.Required)]
    public interface IAdminAppService
    {
        #region impliments_for_nishtiak
        [OperationContract]
        void AddNishtiak();
        [OperationContract]
        void DeleteNishtiak(string id);
        [OperationContract]
        void ChangeNishtiakState(string id, int state);
        [OperationContract]
        NishtiakTransferObject[] GetAllNishtiachoks();
        #endregion

        #region impliments_for_queue
        [OperationContract]
        void DeleteUserByAdmin(string id);
        [OperationContract]
        void SwitchQueueState();
        [OperationContract]
        void UpdateUsersInQueue(string[] userNames);
        [OperationContract]
        QueueUserTransferObject GetUserInQueueByID(string id);
        [OperationContract]
        QueueUserTransferObject[] GetAllUsersInQueue();
        [OperationContract]
        int GetQueueState();

        #endregion

        #region impliments_for_user
        [OperationContract]
        void SendMsg(string msg, string id);
        [OperationContract]
        void ChangeUserRole(string id, int role);
        [OperationContract]
        UserInfo GetUserInfoByID(string id);
        [OperationContract]
        UserInfo[] GetInfoForAllUsers();
        #endregion

        #region impliments_for_chart
        [OperationContract]
        ChartValues GetStatisticsPersonal(string userId);
        [OperationContract]
        ChartValues GetStatisticsGeneralWasMoreThenAthoresInState(int stat);
        #endregion

        #region service_configs
        [OperationContract]
        bool Ping();
        [OperationContract(IsInitiating = true)]
        void Init();
        #endregion service_configs
    }
}