using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImitateUserApp.UserAppService;
using System.ServiceModel;
using System.Threading;

namespace ImitateUserApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var callback = new CallBackClass();
            InstanceContext context = new InstanceContext(callback);
            var service = new UserAppServiceClient(context);

            callback.Service = service;
            service.InitUser();

            Console.WriteLine("write q to out");

            while (Console.ReadLine().ToLower() != "q")
            {
                service.Close();
                return;
            }
        }

        public class CallBackClass : IUserAppServiceCallback
        {
            public UserAppServiceClient Service { get; set; }

            public void NotifyServerReady()
            {
                Console.WriteLine("NotifyServerReady");
                new Thread(() =>
                {
                    Console.WriteLine("sleep for second");
                    Thread.Sleep(1000);
                    Service.TryStandInQueueAsync();
                }).Start();
            }

            public void ShowMessage(string text)
            {
                Console.WriteLine("msg: {0}", text);
            }

            public void StandInQueue()
            {
                Console.WriteLine("StandInQueue");
            }

            public void ShowPosition(int position)
            {
                Console.WriteLine("position: {0}", position);
            }

            public void OfferToUseObj()
            {
                Console.WriteLine("OfferToUseObj");

                new Thread(() => 
                {
                    Console.WriteLine("sleep for second");
                    Thread.Sleep(1000);
                    Service.AnswerForOfferToUseAsync(true);
                }).Start();

            }

            public void NotifyToUseObj()
            {
                Console.WriteLine("NotifyToUseObj");

                new Thread(() =>
                {
                    Console.WriteLine("sleep for 5 seconds");
                    Thread.Sleep(5000);
                    Service.LeaveQueueAsync();
                }).Start();

            }

            public void DroppedByServer(string text)
            {
                Console.WriteLine("DroppedByServer: {0}", text);
            }
        }

    }
}
