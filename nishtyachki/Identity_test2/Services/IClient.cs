using System.ServiceModel;
using System.Threading.Tasks;

namespace AdminApp.Services
{
    public interface IClient
    {
        [OperationContract(IsOneWay = true)]
        Task NotifyServerReady();
        [OperationContract(IsOneWay = true)]
        Task ShowMessage(string text);
        [OperationContract(IsOneWay = true)]
        Task StandInQueue();
        [OperationContract(IsOneWay = true)]
        Task ShowPosition(int position);
        [OperationContract]
        Task OfferToUseObj();
        [OperationContract]
        Task NotifyToUseObj();
        [OperationContract]
        Task DroppedByServer(string text);
    }
}
