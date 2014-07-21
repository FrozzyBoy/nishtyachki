using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AdminApp.Services
{
    public interface IClient
    {
        [OperationContract(IsOneWay = true)]
        void NotifyServerReady();
        [OperationContract(IsOneWay = true)]
        void ShowMessage(string text);
        [OperationContract(IsOneWay = true)]
        void NotifyToUseObj();
        [OperationContract(IsOneWay = true)]
        void StandInQueue();
        [OperationContract(IsOneWay = true)]
        void ShowPosition(int position);
        [OperationContract(IsOneWay = false)]
        bool SuggestToUseObject();
    }
}
