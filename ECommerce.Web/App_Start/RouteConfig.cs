using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ECommerce.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                    "Category", "Category/{seo}/{id}",
                    new { controller = "Category", action = "CategoryDetail" },
                    new { id = @"\d+" },
                    namespaces: new string[] { "ECommerce.Web.Controllers" }
                    );

            routes.MapRoute(
                    "Product", "Product/{seo}/{id}",
                    new { controller = "Product", action = "ProductDetail" },
                    new { id = @"\d+" },
                    namespaces: new string[] { "ECommerce.Web.Controllers" }
                    );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "ECommerce.Web.Controllers" }
            );
        }
    }
}
