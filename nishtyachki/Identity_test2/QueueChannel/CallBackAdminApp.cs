using AdminApp.AdminAppService;
using System;

namespace AdminApp.QueueChannel
{
    public class CallBackAdminApp : IAdminAppServiceCallback
    {
        public static event Action eventUpdateQueue;
        public static event Action eventUpdateNishtiachok;

        public void UpdateQueue()
        {
            if(eventUpdateQueue != null)
            {
                eventUpdateQueue();
            }
        }

        public void UpdateNishtiachok()
        {
            if (eventUpdateNishtiachok != null)
            {
                eventUpdateNishtiachok();
            }
        }
    }
}
