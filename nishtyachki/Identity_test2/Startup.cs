using AdminApp.App_Start;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Web.Routing;

[assembly: OwinStartupAttribute(typeof(AdminApp.Startup))]
namespace AdminApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            ContainerConfig.Config(config);

            ConfigureAuth(app);
            WebApiConfig.RegisterRoutes(config);
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);
            app.MapSignalR();
        }                
    }
}