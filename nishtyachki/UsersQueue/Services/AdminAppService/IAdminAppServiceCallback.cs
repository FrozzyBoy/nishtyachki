using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace UsersQueue.Services.AdminAppService
{
    public interface IAdminAppServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        Task UpdateQueue(object sender, EventArgs e);
        [OperationContract(IsOneWay = true)]
        Task UpdateNishtiachok(object sender, EventArgs e);
    }
}