using System.Web.Mvc;
using System.Web.Routing;

namespace SCCL.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            );

            routes.MapRoute(
                "404-PageNotFound",
                "{*url}",
                new {controller = "StaticContent", action = "PageNotFound"}
            );
        }
    }
}