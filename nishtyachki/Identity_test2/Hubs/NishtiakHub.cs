using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using AdminApp.QueueChannel;

namespace AdminApp.Hubs
{
    [HubName("nishtiak")]
    public class NishtiakHub : Hub
    {
        public override System.Threading.Tasks.Task OnConnected()
        {
            CallBackAdminApp.eventUpdateNishtiachok += Nishtiachok_EventChangeNisht;
            return base.OnConnected();
        }

        void Nishtiachok_EventChangeNisht()
        {
            Clients.All.update();
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool fl)
        {
            CallBackAdminApp.eventUpdateNishtiachok -= Nishtiachok_EventChangeNisht;
            return base.OnDisconnected(fl);
        }
    }
}