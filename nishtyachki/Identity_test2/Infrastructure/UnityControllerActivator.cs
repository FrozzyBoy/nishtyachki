using System;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;

namespace AdminApp.Infrastructure
{
    public sealed class UnityControllerActivator : IControllerActivator
    {
        private readonly IUnityContainer _container;

        public UnityControllerActivator(IUnityContainer container)
        {
            _container = container;
        }

        public IController Create(RequestContext requestContext, Type controllerType)
        {
            return (IController)_container.Resolve(controllerType);
        }
    }
}