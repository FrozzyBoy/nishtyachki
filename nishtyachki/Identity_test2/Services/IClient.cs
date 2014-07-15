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
        void NotifyToUseObj(string text);
        [OperationContract(IsOneWay = true)]
        void StandInQueue(int numberPeopleInfront);
    }
}
