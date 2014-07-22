using AdminApp.Queue;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace AdminApp.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfService.svc or WcfService.svc.cs at the Solution Explorer and start debugging.
    public class WcfService : IWcfService
    {
        private static ConcurrentDictionary<string, IClient> _clients = new ConcurrentDictionary<string, IClient>();
        private string _key;

        public void InitUser()
        {
            IClient client = OperationContext.Current.GetCallbackChannel<IClient>();
            _key = Thread.CurrentPrincipal.Identity.Name;
            
            _clients[_key] = client;

            client.NotifyServerReady();
        }

        public bool TryStandInQueue()
        {
            //стать в очередь и вернуть, стал ли он в одном потоке
            throw new NotImplementedException();
        }

        public void LeaveQueue()
        {
            throw new NotImplementedException();
        }

        public void AnsweForOfferToUse(bool willUse)
        {
            //true если пользователь согласен
            throw new NotImplementedException();
        }

        public void StopUseObj()
        {
            throw new NotImplementedException();
        }
    }
}
