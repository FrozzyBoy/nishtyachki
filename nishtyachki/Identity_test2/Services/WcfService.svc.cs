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
            var client =_clients[_key];

            User usr = new User(_key, _clients[_key]);

            bool stay = UsersQueue.Instance.AddUserInQueue(usr);

            if (stay)
            {
                client.StandInQueue(); 
            }

            return stay;
        }

        public void LeaveQueue()
        {
            User usr = new User(_key, _clients[_key]);
            UsersQueue.Instance.DeleteFromTheQueue(usr);
        }

        public void AnswerForOfferToUse(bool willUse)
        {
            User usr = new User(_key, _clients[_key]);
            if (willUse)
            {
                UsersQueue.StartUseNishtiak(_key);
            }
            else
            {
                UsersQueue.Instance.DeleteFromTheQueue(usr);
            }
        }

        public void StopUseObj()
        {
            UsersQueue.EndUseNishtiak(_key);
        }
    }
}
