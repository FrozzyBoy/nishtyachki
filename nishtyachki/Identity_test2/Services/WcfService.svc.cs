﻿using System;
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

        public bool TryStandInQueue()
        {
            bool operationOk = false;

            if (_key != null)
            {
                new Thread(() =>
                {
                    Thread.Sleep(5000);

                    IClient res;
                    if (_clients.TryGetValue(_key, out res))
                    {
                        res.NotifyToUseObj("yahoo! U can Use nishtiak!");
                        operationOk = true;
                    }

                }).Start();
            }

            return operationOk;
        }

        public void LeaveQueue()
        {
            IClient client;
            _clients.TryRemove(_key, out client);
        }

        public void InitUser()
        {
            IClient client = OperationContext.Current.GetCallbackChannel<IClient>();
            _key = Thread.CurrentPrincipal.Identity.Name;

            if (!_clients.TryAdd(_key, client))
            {
                _key = null;
            }

            client.NotifyServerReady();
        }

    }
}
