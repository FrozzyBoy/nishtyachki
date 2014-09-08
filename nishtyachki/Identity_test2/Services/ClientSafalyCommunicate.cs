using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Threading.Tasks;

namespace AdminApp.Services
{
    public class ClientSafalyCommunicate : IClient
    {
        private IClient _client;
        private static readonly ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Task SafeAsk(Func<Task> act, string msgIfError)
        {
            Task returnTask = null;
            
            try
            {
                returnTask = act();
            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                log.Error(msgIfError, ex);
            }

            return returnTask;
        }

        public ClientSafalyCommunicate(IClient client)
        {
            _client = client;
            log.Info("test");
        }

        public System.Threading.Tasks.Task NotifyServerReady()
        {
            return SafeAsk(() => { return _client.NotifyServerReady(); }, "");
        }

        public System.Threading.Tasks.Task ShowMessage(string text)
        {
            return SafeAsk(() => { return _client.ShowMessage(text); }, "");
        }

        public System.Threading.Tasks.Task StandInQueue()
        {
            return SafeAsk(() => { return _client.StandInQueue(); }, "");
        }

        public System.Threading.Tasks.Task ShowPosition(int position)
        {
            return SafeAsk(() => { return _client.ShowPosition(position); }, "");
        }

        public System.Threading.Tasks.Task OfferToUseObj()
        {
            return SafeAsk(() => { return _client.OfferToUseObj(); }, "");
        }

        public System.Threading.Tasks.Task NotifyToUseObj()
        {
            return SafeAsk(() => { return _client.NotifyToUseObj(); }, "");
        }

        public System.Threading.Tasks.Task DroppedByServer(string text)
        {
            return SafeAsk(() => { return _client.DroppedByServer(text); }, "");
        }
    }
}