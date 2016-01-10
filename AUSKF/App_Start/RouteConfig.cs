using System.Web.Mvc;
using System.Web.Routing;

namespace AUSKF
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // routes.MapMvcAttributeRoutes();
            //routes.MapRoute(
            //    name: "About",
            //    url: "About",
            //    defaults: new {controller = "Home", action = "About"},
            //    namespaces: new[] { "AUSKF.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "AUSKF.Controllers" }
            );
        }
    }
}
