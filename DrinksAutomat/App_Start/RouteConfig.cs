using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DrinksAutomat
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Admin",
                url: "admineditor",
                defaults: new { controller = "Admin", action = "Adminstrate" }
            );

            routes.MapRoute(
                name: "User",
                url: "{action}/{id}",
                defaults: new {controller ="User", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}

