using Microsoft.Owin;
using Owin;
using System.Web.Http;


[assembly: OwinStartupAttribute(typeof(AdminApp.Startup))]
namespace AdminApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var config = new HttpConfiguration();
            WebApiConfig.RegisterRoutes(config);
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);

            app.MapSignalR();
        }
    }
}