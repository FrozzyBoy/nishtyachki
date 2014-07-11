using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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

        public string DoWork(string value)
        {
            Thread.Sleep(3000);
            return "Welcome, " + value;
        }

        public string DoAnotherWork(string value)
        {
            Thread.Sleep(1000);
            return "Another Welcome, " + value;
        }

        public void OpenSession()
        {
            IClient client = OperationContext.Current.GetCallbackChannel<IClient>();
            string key = OperationContext.Current.SessionId;
            _clients.AddOrUpdate(key, client, (k, v) => v);


            new Thread(() =>
            {
                Thread.Sleep(5000);

                IClient res;
                if (_clients.TryGetValue(key, out res))
                {
                    res.ShowMessage("yahoo!");
                }

            }).Start();
        }

        public void MyCallBack()
        {
            throw new NotImplementedException();
        }
    }
}
