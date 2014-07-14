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

        public bool TryStandInQueue(string passkey)
        {
            bool operationOk = false;

            if (passkey != null)
            {
                new Thread(() =>
                {
                    Thread.Sleep(5000);

                    IClient res;
                    if (_clients.TryGetValue(passkey, out res))
                    {
                        res.NotifyToUseObj("yahoo! U can Use nishtiak!");
                        operationOk = true;
                    }

                }).Start();
            }

            return operationOk;
        }

        public void LeaveQueue(string passkey)
        {
            IClient client;
            _clients.TryRemove(passkey, out client);
        }

        public string GetPasskey()
        {
            IClient client = OperationContext.Current.GetCallbackChannel<IClient>();
            string key = OperationContext.Current.SessionId;

            if (!_clients.TryAdd(key, client))
            {
                key = null;
            }

            client.NotifyServerReady();

            return key;

        }

    }
}
