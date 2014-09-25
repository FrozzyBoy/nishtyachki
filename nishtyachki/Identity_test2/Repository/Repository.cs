using AdminApp.AdminAppService;
using System.ServiceModel;

namespace AdminApp.Repository
{
    public class Repository : IRepository
    {
        private IAdminAppService _service;

        public NishtiakSet NishtiakSet { get; private set; }
        public QueueSet QueueSet { get; private set; }
        public UserSet UserSet { get; private set; }

        public Repository()
        {
            IAdminAppServiceCallback callback = new CallBackAdminApp();
            InstanceContext ic = new InstanceContext(callback);
            _service = new AdminAppServiceClient(ic);

            this.NishtiakSet = new NishtiakSet(_service);
            this.QueueSet = new QueueSet(_service);
            this.UserSet = new UserSet(_service);
        }

    }
}