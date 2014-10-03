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
        #region PutActions

        #region nishtiak
        void AddNishtiak();
        void DeleteNishtiak(string id);
        void ChangeNishtiakState(string id, int state);
        #endregion nishtiak

        #region queue
        void DeleteUserByAdmin(string id);
        void SwitchQueueState();
        void UpdateUsersInQueue(string[] userNames);
        #endregion queue

        #region user
        void SendMsg(string msg, string id);
        void ChangeUserRole(string id, int role);
        #endregion user

        #endregion PutActions

        #region GetActions
        #region nishtiak
        List<Nishtiachok> GetAllNishtiaks();
        Nishtiachok GetNishtiakById(string nishtiakId);
        #endregion nishtiak

        #region queue
        QueueUser GetUserInQueueByID(string id);
        List<QueueUser> GetAllUsersInQueue();
        object GetQueueInstance();
        #endregion queue

        #region user
        UserInfo GetUserInfoByID(string id);
        List<UserInfo> GetAllUsersInfo();
        #endregion user

        #region statistics
        ChartValues GetStatisticsGeneralWasMoreThenAthoresInState(int state);
        ChartValues GetStatisticsPersonal(string userID);
        ChartValues GetStatisticsForNishtiak(string nishtiakID);
        #endregion

        #endregion GetActions        
    }
}
