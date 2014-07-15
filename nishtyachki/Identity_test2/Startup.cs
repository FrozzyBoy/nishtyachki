using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AdminApp.Startup))]
namespace AdminApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}