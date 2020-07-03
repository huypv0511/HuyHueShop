using System.Web.Mvc;
using System.Web.Routing;

namespace MVCDemo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
             name: "Add Cart",
             url: "them-gio-hang",
             defaults: new { controller = "Product", action = "AddToCart", id = UrlParameter.Optional },
             namespaces: new[] { "MVCDemo.Controllers" }
            ); 
            routes.MapRoute(
             name: "Test",
             url: "them-gio",
             defaults: new { controller = "Product", action = "Cart", id = UrlParameter.Optional },
             namespaces: new[] { "MVCDemo.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MVCDemo.Controllers" }
            );
         
        }
    }
}
