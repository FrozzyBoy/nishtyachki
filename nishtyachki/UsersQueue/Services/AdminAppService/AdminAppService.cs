using System.ServiceModel;
using UsersQueue.Queue.Nishtiachki;
using UsersQueue.Queue;
using UsersQueue.Model;
using System.Linq;
using UsersQueue.Services.TransferObjects;
using UsersQueue.Queue.UserInformtion;
using UsersQueue.Queue.Statistics;

namespace UsersQueue.Services.AdminAppService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AdminAppService" in both code and config file together.
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class AdminAppService : IAdminAppService
    {
        public void AddNishtiak()
        {
            Nishtiachok.AddNistiachokByAdmin();
        }

        public void DeleteNishtiak(string id)
        {
            Nishtiachok.DeleteNishtiachok(id);
        }

        public void ChangeNishtiakState(string id, int state)
        {
            if (state > (int)Nishtiachok_State.locked)
            {
                state = (int)Nishtiachok_State.locked;
            }

            var changeTo = (Nishtiachok_State)state;

            var nishtiak = Nishtiachok.GetNishtiachokByName(id);
            nishtiak.ChangeNishtState(changeTo);
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

            if (user == null)
            {
                var nisht = Nishtiachok.GetNishtiakByUserId(id);
                if (nisht != null)
                {
                    user = nisht.Owner;
                }
            }

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

        public NishtiakTransferObject[] GetAllNishtiachoks()
        {
            var recive = Nishtiachok.GetAllNishtiakTransferObjects.ToArray();
            return recive;
        }

        public QueueUserTransferObject GetUserInQueueByID(string id)
        {
            return UsersQueueInstance.Instance.GetUser(id).GetQueueUserTransferObject();
        }

        public QueueUserTransferObject[] GetAllUsersInQueue()
        {
            return UsersQueueInstance.Instance.GetTransferQueueUsers.ToArray();
        }

        public UserInfo GetUserInfoByID(string id)
        {
            UserInfo info = null;

            using (var context = new AppDbContext())
            {
                info = context.UsersInfo.SingleOrDefault<UserInfo>((x) => x.UserName == id);
            }

            return info;
        }

        public UserInfo[] GetInfoForAllUsers()
        {
            UserInfo[] result = null;
            using (var context = new AppDbContext())
            {
                result = (from ui in context.UsersInfo
                          select ui).ToArray<UserInfo>();
            }
            return result;
        }

        public void Init()
        {
            IAdminAppServiceCallback callbacks = OperationContext.Current.GetCallbackChannel<IAdminAppServiceCallback>();
            Nishtiachok.EventChangeNisht += callbacks.UpdateNishtiachok;
            UsersQueueInstance.QueueChanged += callbacks.UpdateQueue;
        }

        public int GetQueueState()
        {
            return (int)UsersQueueInstance.Instance.QueueState;
        }

        public ChartValues GetStatisticsPersonal(string userId)
        {
            var result = StatisticComposer.ChartPersonalStatistic(userId);
            return result;
        }

        public ChartValues GetStatisticsGeneralWasMoreThenAthoresInState(int stat)
        {
            var parseStat = (UserCurrentState)stat;
            var result = StatisticComposer.GeneralWasMoreThenAthoresInState(parseStat);
            return result;
        }


        public NishtiakTransferObject GetNishtiakById(string nishtiakId)
        {
            var nisht = Nishtiachok.GetNishtiachokByName(nishtiakId);
            NishtiakTransferObject result = null;
            if (nisht != null)
            {
                result = nisht.GetNishtiakTransferObject();
            }

            return result;

        }

        public ChartValues GetStatisticsForNishtiak(string nishtiakID)
        {
            ChartValues result = StatisticComposer.GetStatisticsForNishtiak(nishtiakID);
            return result;
        }
    }
}