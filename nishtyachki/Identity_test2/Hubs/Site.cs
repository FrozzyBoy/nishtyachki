using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace AdminApp.Hubs
{
    [HubName("site")] 
    public class SiteHub : Hub
    {
        public void Site()
        {
            Clients.All.refresh();
        }
    }
}