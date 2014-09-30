using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace UsersQueue.Services.AdminAppService
{
    public interface IAdminAppServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void UpdateQueue();
        [OperationContract(IsOneWay = true)]
        void UpdateNishtiachok();
    }
}