using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace nishtyachki.Logic.Service
{
    [ServiceContract]
    public interface IWcfService
    {
        [OperationContract]
        Registration Register(int userId);
    }
}
