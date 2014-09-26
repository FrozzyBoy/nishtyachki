using AdminApp.AdminAppService;
using Antlr.Runtime.Misc;

namespace AdminApp.QueueChannel
{
    public class CallBackAdminApp : IAdminAppServiceCallback
    {
        public static event Action eventUpdateQueue;
        public static event Action eventUpdateNishtiachok;

        public void UpdateQueue(object sender, AdminApp.AdminAppService.EventArgs e)
        {
            if(eventUpdateQueue != null)
            {
                eventUpdateQueue();
            }
        }

        public void UpdateNishtiachok(object sender, AdminApp.AdminAppService.EventArgs e)
        {
            if (eventUpdateNishtiachok != null)
            {
                eventUpdateNishtiachok();
            }
        }
    }
}
