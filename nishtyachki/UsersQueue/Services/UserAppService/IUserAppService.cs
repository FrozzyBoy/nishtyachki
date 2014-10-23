using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace UsersQueue.Services.UserAppService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserAppService" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(IUserAppServiceClient))]
    public interface IUserAppService
    {
        [OperationContract(IsInitiating = true)]
        void InitUser();
        [OperationContract]
        bool TryStandInQueue();
        [OperationContract]
        void LeaveQueue();
        [OperationContract(IsOneWay = true)]
        void AnswerForOfferToUse(bool willUse);
        [OperationContract]
        void StopUseObj();
        [OperationContract(IsTerminating = false)]
        void Disconnect();
    }
}
