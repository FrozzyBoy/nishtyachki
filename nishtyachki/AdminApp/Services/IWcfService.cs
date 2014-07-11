using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AdminApp.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWcfService" in both code and config file together.
    [ServiceContract(CallbackContract=typeof(IClient))]
    public interface IWcfService
    {        
        [OperationContract]
        string DoWork(string value);
        [OperationContract]
        string DoAnotherWork(string value);
        [OperationContract]
        void MyCallBack();
        [OperationContract(IsInitiating = true)]
        void OpenSession();
    }
}
