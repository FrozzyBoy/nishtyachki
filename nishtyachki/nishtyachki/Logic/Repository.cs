using nishtyachki.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace nishtyachki.Logic
{
    public class Repository : IRepository
    {
        private AdminApp.IWcfService _service;

        public Repository(IClientWindow window, INotifyWindow notify)
        {
            AdminApp.IWcfServiceCallback callback = new CallBackClass(window, notify);
            InstanceContext ic = new InstanceContext(callback);
            _service = new AdminApp.WcfServiceClient(ic);
            _service.InitUserAsync();
        }

        public void StayInQueue()
        {
            _service.TryStandInQueueAsync();
        
        }

        ~Repository()
        {
            _service.LeaveQueueAsync();
        }
    }
}
