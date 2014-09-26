using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using AdminApp.Queue;
using AdminApp.QueueChannel;

namespace AdminApp.Hubs
{
    [HubName("queue")]
    public class UserQueueHub : Hub
    {       
        public override System.Threading.Tasks.Task OnConnected()
        {
            CallBackAdminApp.eventUpdateQueue += Instance_QueueChanged;            
            return base.OnConnected();
        }
        
        public override System.Threading.Tasks.Task OnDisconnected(bool fl)
        {
            CallBackAdminApp.eventUpdateQueue -= Instance_QueueChanged;
            return base.OnDisconnected(fl);
        }

        void Instance_QueueChanged()
        {
            Clients.All.update();
        }

    }
}