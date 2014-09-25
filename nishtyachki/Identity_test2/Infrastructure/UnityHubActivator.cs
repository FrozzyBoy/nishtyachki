using System.Diagnostics;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Practices.Unity;

namespace AdminApp.Infrastructure
{
    public sealed class UnityHubActivator : IHubActivator
    {
        private readonly IUnityContainer _container;

        public UnityHubActivator(IUnityContainer container)
        {
            Debug.Assert(container != null, "container == null");

            _container = container;
        }

        public IHub Create(HubDescriptor descriptor)
        {
            var hubType = descriptor.HubType;
            return hubType != null ? _container.Resolve(hubType) as IHub : null;
        }
    }
}