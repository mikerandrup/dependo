using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PrototypeWebsite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            // Pages
            routes.MapRoute(
                name: "treepage",
                url: "trees",
                defaults: new { controller = "Page", action = "Trees" }
            );
            routes.MapRoute(
                name: "fruitpage",
                url: "fruits",
                defaults: new { controller = "Page", action = "Fruits" }
            );

            // Asset Routes
            routes.MapRoute(
                name: "fetchjavascript",
                url: "fetch/javascript",
                defaults: new { controller = "AssetDelivery", action = "JavaScript" }
            );
        }
    }
}