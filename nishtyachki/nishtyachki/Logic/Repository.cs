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
            if (_service != null)
            {
                _service.DisconnectAsync();
            }

            AdminApp.IWcfServiceCallback callback = new CallBackClass(window, notify);
            InstanceContext ic = new InstanceContext(callback);
            _service = new AdminApp.WcfServiceClient(ic);
            _service.InitUserAsync();
        }

        public void StayInQueue()
        {
            _service.TryStandInQueueAsync();        
        }

        public void AnswerForOfferToUse(bool willUse)
        {
            _service.AnswerForOfferToUseAsync(willUse);
        }

        public void Disconnect()
        {
            _service.DisconnectAsync();
        }

        public void LeaveQueue()
        {
            _service.LeaveQueueAsync();
        }

        public void StopUse()
        {
            _service.StopUseObjAsync();
        }

        ~Repository()
        {
            Disconnect();
        }
    }
}
