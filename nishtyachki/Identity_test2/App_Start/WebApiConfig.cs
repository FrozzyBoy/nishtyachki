using Microsoft.Practices.Unity;
using System.Web.Http;
using AdminApp.App_Start;
using AdminApp.QueueChannel;

namespace AdminApp
{
    public class WebApiConfig
    {
        public static void RegisterRoutes(HttpConfiguration config)
        {
            /*
            config.Routes.MapHttpRoute(
                "API Default",
                "api/{controller}/{id}",
                new {id = RouteParameter.Optional }
                );
             */
        }

        public static void Register(HttpConfiguration configuration)
        {
            configuration.Routes.MapHttpRoute("API Default", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

        }

    }
}