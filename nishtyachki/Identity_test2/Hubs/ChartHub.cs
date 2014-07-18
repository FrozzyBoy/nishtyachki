using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace AdminApp.Hubs
{
    [HubName("chart")]
    public class ChartHub : Hub
    {
        public void Update()
        {
            Clients.All.update();
        }
    }
}