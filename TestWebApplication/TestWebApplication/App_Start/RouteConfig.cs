﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TestWebApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Files",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "FilePage", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Account",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "PersonalPage", id = UrlParameter.Optional }
            );
        }
    }
}
