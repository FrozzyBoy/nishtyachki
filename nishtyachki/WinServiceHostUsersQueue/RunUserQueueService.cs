using System;
using System.ServiceModel;
using System.ServiceProcess;

namespace WinServiceHostUsersQueue
{
    public partial class RunUserQueueService : ServiceBase
    {
        private ServiceHost _hostUserApp;
        private ServiceHost _hostAdminApp;

        public RunUserQueueService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                _hostUserApp = new ServiceHost(typeof(UsersQueue.Services.UserAppService.UserAppService));
                _hostUserApp.Open();

                _hostAdminApp = new ServiceHost(typeof(UsersQueue.Services.AdminAppService.AdminAppService));
                _hostAdminApp.Open();
            }
            catch (Exception exc)
            {
                if (Environment.UserInteractive)
                {
                    Console.WriteLine(exc.Message);
                }
                else
                {
                    throw;
                }
            }
            
        }

        protected override void OnStop()
        {
            if (_hostUserApp != null && _hostUserApp.State == CommunicationState.Opened)
            {
                _hostUserApp.Close();
            }
            _hostUserApp = null;

            if (_hostAdminApp != null && _hostAdminApp.State == CommunicationState.Opened)
            {
                _hostAdminApp.Close();
            }
            _hostAdminApp = null;

        }

        public void Start(string[] args)
        {
            this.OnStart(args);
        }

        public new void Stop()
        {
            this.OnStop();
        }
    }
}
