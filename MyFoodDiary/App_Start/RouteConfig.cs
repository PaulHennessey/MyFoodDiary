using System.Web.Mvc;
using System.Web.Routing;

namespace MyFoodDiary
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "RefreshHome",
                url: "Home/Refresh/{date}",
                defaults: new { controller = "Home", action = "Refresh", date = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "RefreshBarChart",
                url: "Charts/RefreshBarChart/{start}/{end}/{nutrient}",
                defaults: new { controller = "Charts", action = "RefreshBarChart", start = UrlParameter.Optional, end = UrlParameter.Optional, nutrient = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "RefreshLineChart",
                url: "Charts/RefreshLineChart/{start}/{end}/{nutrient}",
                defaults: new { controller = "Charts", action = "RefreshLineChart", start = UrlParameter.Optional, end = UrlParameter.Optional, nutrient = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Search",
                url: "Home/Search/{term}",
                defaults: new { controller = "Home", action = "Search", term = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Delete",
                url: "Home/Delete/{id}",
                defaults: new { controller = "Home", action = "Delete", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Save",
                url: "Home/Save/{id}/{quantity}",
                defaults: new { controller = "Home", action = "Save", id = UrlParameter.Optional, quantity = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Favourite",
                url: "Home/Favourite/{id}/{quantity}",
                defaults: new { controller = "Home", action = "Favourite", id = UrlParameter.Optional, quantity = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "SelectFood",
                url: "Home/SelectFood/{Code}/{date}",
                defaults: new { controller = "Home", action = "SelectFood", Code = UrlParameter.Optional, date = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "CreateProduct",
                url: "Products/Create",
                defaults: new { controller = "Products", action = "Create" }
            );

            routes.MapRoute(
                name: "EditProduct",
                url: "Products/Edit/{code}",
                defaults: new { controller = "Products", action = "Edit" }
            );

            routes.MapRoute(
                name: "DeleteProduct",
                url: "Products/Delete/{code}",
                defaults: new { controller = "Products", action = "Delete" }
            );

            routes.MapRoute(
                name: "Products",
                url: "Products/{date}",
                defaults: new { controller = "Products", action = "Index", date = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Charts",
                url: "Charts/{date}",
                defaults: new { controller = "Charts", action = "Index", date = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Home",
                url: "Home/{date}",
                defaults: new { controller = "Home", action = "Index", date = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}