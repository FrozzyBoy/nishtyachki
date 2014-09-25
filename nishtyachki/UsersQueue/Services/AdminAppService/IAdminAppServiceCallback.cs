using System.ServiceModel;
using System.Threading.Tasks;

namespace UsersQueue.Services.AdminAppService
{
    public interface IAdminAppServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        Task UpdateQueue();
        [OperationContract(IsOneWay = true)]
        Task UpdateNishtiachok();
    }
}