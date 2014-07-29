using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using AdminApp.Queue;
using System.Threading;

namespace AdminApp.Hubs
{
    [HubName("queue")]
    public class UserQueueHub : Hub
    {       
        public override System.Threading.Tasks.Task OnConnected()
        {
            UsersQueue.Instance.QueueChanged += Instance_QueueChanged;
            return base.OnConnected();
        }
        
        public override System.Threading.Tasks.Task OnDisconnected()
        {
            UsersQueue.Instance.QueueChanged -= Instance_QueueChanged;
            return base.OnDisconnected();
        }

        void Instance_QueueChanged(object sender, EventArgs e)
        {
            Clients.All.update();
        }

        public void Test()
        {
            Clients.All.update();
        }
    }
}