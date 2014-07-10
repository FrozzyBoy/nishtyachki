using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace nishtyachki.Logic.Service
{
    public class WcfService : IWcfService
    {
        public Registration Register(int userId)
        {
            Thread.Sleep(1000);
            return new Registration(userId + 59);
        }
    }
}
