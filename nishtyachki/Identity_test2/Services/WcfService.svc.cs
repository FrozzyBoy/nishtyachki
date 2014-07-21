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
        private readonly TimeSpan DelayToKickUser = new TimeSpan(0, 10, 0);


        public bool TryStandInQueue()
        {
            if (_key != null)
            {
                new Thread(() =>
                {
                    IClient res;
                    if (_clients.TryGetValue(_key, out res))
                    {
                        User user = new User(_key, this.SayUserUseObj, this.SayUserHisPosition);
                        UsersQueue.Instance.AddUserInQueue(user);
                        //res.StandInQueue(UsersQueue.Instance.GetCount);
                    }

                }).Start();
            }

            return true;
        }

        private void SayUserUseObj()
        {
            _clients[_key].NotifyToUseObj();
        }
    
        private void SayUserHisPosition(int pos)
        {
            _clients[_key].ShowPosition(pos);
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
            
            _clients[_key] = client;

            client.NotifyServerReady();
        }

    }
}
