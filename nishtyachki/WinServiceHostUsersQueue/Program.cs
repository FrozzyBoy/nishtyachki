using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WinServiceHostUsersQueue
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (RunUserQueueService service = new RunUserQueueService())
            {
                if (Environment.UserInteractive)
                {
                    service.Start(args);
                    Console.WriteLine("Press ENTER to stop service...");
                    Console.ReadLine();
                    service.Stop();
                }
                else
                {
                    ServiceBase.Run(service);
                }
            }
        }
    }
}
