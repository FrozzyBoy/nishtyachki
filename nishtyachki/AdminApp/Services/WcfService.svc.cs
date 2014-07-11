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

        public void StandInQueue()
        {
            IClient client = OperationContext.Current.GetCallbackChannel<IClient>();
            string key = OperationContext.Current.SessionId;
            //_clients.AddOrUpdate(key, client, (k, v) => v);

            if (_clients.TryAdd(key, client))
            {
                new Thread(() =>
                {
                    Thread.Sleep(5000);

                    IClient res;
                    if (_clients.TryGetValue(key, out res))
                    {
                        res.NotifyToUseObj("yahoo! U can Use nishtiak!");
                    }

                }).Start();
            }
        }

        public void LeaveQueue()
        {
        }
    }
}
