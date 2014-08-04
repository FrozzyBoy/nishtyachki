using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace AdminApp.Hubs
{
    [HubName("nishtiak")]
    public class NishtiakHub : Hub
    {
        public override System.Threading.Tasks.Task OnConnected()
        {
            Nishtiachki.Nishtiachok.EventChangeNisht += Nishtiachok_EventChangeNisht;
            return base.OnConnected();
        }

        void Nishtiachok_EventChangeNisht(object sender, EventArgs e)
        {
            Clients.All.update();
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool fl)
        {
            Nishtiachki.Nishtiachok.EventChangeNisht -= Nishtiachok_EventChangeNisht;
            return base.OnDisconnected(fl);
        }
    }
}