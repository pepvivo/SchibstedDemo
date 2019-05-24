using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

/*
            routes.MapRoute (
                name: "Users",
                url: "api/{controller}/{action}/{id}",
                defaults: new { controller = "Users", action = "Get", name = UrlParameter.Optional }
            );
*/

            //routes.MapRoute(
            //    name: "UserPage",
            //    url: "UserPage/List",
            //    defaults: new { controller = "UserPage", action = "List", name = UrlParameter.Optional }
            //);


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            
            
        }
    }
}
