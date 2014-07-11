using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Identity_test2.Startup))]
namespace Identity_test2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
