using System;
using System.ServiceModel;
using UsersQueue.Queue.Nishtiachki;
using UsersQueue.Queue;
using UsersQueue.Model;
using System.Linq;

namespace UsersQueue.Services.AdminAppService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AdminAppService" in both code and config file together.
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class AdminAppService : IAdminAppService
    {
        public void AddNishtiak(string id)
        {
            Nishtiachok.AddNistiachokByAdmin(id);
        }

        public void DeleteNishtiak(string id)
        {
            Nishtiachok.DeleteNishtiachok(id);
        }

        public void ChangeNishtiakState(string id, int state)
        {
            var nishtiak = Nishtiachok.GetNishtiachokByNamme(id);
            nishtiak.ChangeNishtState((Nishtiachok_State)state);
        }

        public void DeleteUserByAdmin(string id)
        {
            UsersQueueInstance.Instance.DeleteUserByAdmin(id);
        }

        public void SwitchQueueState()
        {
            UsersQueueInstance.SwitchQueueState();
        }

        public void UpdateUsersInQueue(string[] userNames)
        {
            UsersQueueInstance.Instance.UpdateQueue(userNames);
        }

        public void SendMsg(string msg, string id)
        {
            var user = UsersQueueInstance.Instance.GetUser(id);
            if (user != null)
            {
                user.Client.ShowMessage(msg);
            }
        }

        public void ChangeUserRole(string id, int role)
        {
            var user = UsersQueueInstance.Instance.GetUser(id);
            user.ChangeRole((Queue.UserInformtion.Role)role);
        }

        public bool Ping()
        {
            return true;
        }

        public Queue.Nishtiachki.Nishtiachok[] GetAllNishtiachoks()
        {
            return Nishtiachok.Nishtiachki.ToArray();
        }

        public Queue.UserInformtion.QueueUser GetUserInQueueByID(string id)
        {
            return UsersQueueInstance.Instance.GetUser(id);
        }

        public Queue.UserInformtion.QueueUser[] GetAllUsersInQueue()
        {
            return UsersQueueInstance.Instance.Queue.ToArray();
        }

        public Queue.UserInformtion.UserInfo GetUserInfoByID(string id)
        {
            Queue.UserInformtion.UserInfo info = null;

            using (var context = new AppDbContext())
            {
                info = context.UsersInfo.SingleOrDefault<Queue.UserInformtion.UserInfo>((x) => x.UserName == id);
            }

            return info;
        }

        public Queue.UserInformtion.UserInfo[] GetInfoForAllUsers()
        {
            Queue.UserInformtion.UserInfo[] result = null;
            using (var context = new AppDbContext())
            {
                result = (from ui in context.UsersInfo
                          select ui).ToArray<Queue.UserInformtion.UserInfo>();
            }
            return result;
        }
    }
}