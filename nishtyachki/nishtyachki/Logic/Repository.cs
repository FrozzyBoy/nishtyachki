using nishtyachki.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace nishtyachki.Logic
{
    public class Repository : IRepository, IDisposable
    {
        private AdminApp.IWcfService _service;
        private IClientWindow _window;

        public Repository(IClientWindow window)
        {
            _window = window;
            ConnectToServer();
        }

        public void ConnectToServer()        
        {
            AdminApp.IWcfServiceCallback callback = new CallBackClass(_window);
            InstanceContext ic = new InstanceContext(callback);
            _service = new AdminApp.WcfServiceClient(ic);
            _service.InitUserAsync();
        }

        public void StayInQueue()
        {
            try
            {
                _service.TryStandInQueueAsync();
            }
            catch (Exception)
            {
                ConnectToServer();
                throw;
            }
        }

        public void AnswerForOfferToUse(bool willUse)
        {
            try
            {
                _service.AnswerForOfferToUseAsync(willUse);
            }
            catch (Exception)
            {
                ConnectToServer();                
                throw;
            }
        }

        private void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                _service.DisconnectAsync();
            }
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
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
