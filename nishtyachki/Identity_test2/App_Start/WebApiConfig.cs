using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

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