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
                name: "RefreshBarChart",
                url: "Macronutrients/RefreshBarChart/{start}/{end}/{nutrient}",
                defaults: new { controller = "Macronutrients", action = "RefreshBarChart", start = UrlParameter.Optional, end = UrlParameter.Optional, nutrient = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "RefreshLineChart",
                url: "Macronutrients/RefreshLineChart/{start}/{end}/{nutrient}",
                defaults: new { controller = "Macronutrients", action = "RefreshLineChart", start = UrlParameter.Optional, end = UrlParameter.Optional, nutrient = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Macronutrients",
                url: "Macronutrients/{date}",
                defaults: new { controller = "Macronutrients", action = "Index", date = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "MicronutrientsRefreshBarChart",
                url: "Micronutrients/RefreshBarChart/{start}/{end}/{nutrient}",
                defaults: new { controller = "Micronutrients", action = "RefreshBarChart", start = UrlParameter.Optional, end = UrlParameter.Optional, nutrient = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "MicronutrientsRefreshLineChart",
                url: "Micronutrients/RefreshLineChart/{start}/{end}/{nutrient}",
                defaults: new { controller = "Micronutrients", action = "RefreshLineChart", start = UrlParameter.Optional, end = UrlParameter.Optional, nutrient = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Micronutrients",
                url: "Micronutrients/{date}",
                defaults: new { controller = "Micronutrients", action = "Index", date = UrlParameter.Optional }
            );


            //routes.MapRoute(
            //    name: "RefreshHome",
            //    url: "Home/Refresh/{date}",
            //    defaults: new { controller = "Home", action = "Refresh", date = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "Search",
            //    url: "Home/Search/{term}",
            //    defaults: new { controller = "Home", action = "Search", term = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "Delete",
            //    url: "Home/Delete/{id}",
            //    defaults: new { controller = "Home", action = "Delete", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "Save",
            //    url: "Home/Save/{id}/{quantity}",
            //    defaults: new { controller = "Home", action = "Save", id = UrlParameter.Optional, quantity = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "Favourite",
            //    url: "Home/Favourite/{id}/{quantity}",
            //    defaults: new { controller = "Home", action = "Favourite", id = UrlParameter.Optional, quantity = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "SelectFood",
            //    url: "Home/SelectFood/{Code}/{date}",
            //    defaults: new { controller = "Home", action = "SelectFood", Code = UrlParameter.Optional, date = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "UseFavourite",
            //    url: "Home/UseFavourite/{Code}/{date}",
            //    defaults: new { controller = "Home", action = "UseFavourite", Code = UrlParameter.Optional, date = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "DeleteFavourite",
            //    url: "Home/DeleteFavourite/{Code}",
            //    defaults: new { controller = "Home", action = "DeleteFavourite", Code = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "Home",
            //    url: "Home/{date}",
            //    defaults: new { controller = "Home", action = "Index", date = UrlParameter.Optional }
            //);


            routes.MapRoute(
                name: "SaveTrackableItem",
                url: "TrackablesLog/Save/{id}/{quantity}",
                defaults: new { controller = "TrackablesLog", action = "Save", id = UrlParameter.Optional, quantity = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "TrackablesLogRefresh",
               url: "TrackablesLog/Refresh/{date}",
               defaults: new { controller = "TrackablesLog", action = "Refresh", date = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "TrackablesLog",
               url: "TrackablesLog/{date}",
               defaults: new { controller = "TrackablesLog", action = "Index", date = UrlParameter.Optional }
           );


            


            routes.MapRoute(
                name: "SaveWeightFirst",
                url: "DataEntry/Save/{id}/{quantity}",
                defaults: new { controller = "DataEntry", action = "Save", id = UrlParameter.Optional, quantity = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "FavouriteWeightFirst",
                url: "DataEntry/Favourite/{id}/{quantity}",
                defaults: new { controller = "DataEntry", action = "Favourite", id = UrlParameter.Optional, quantity = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "UseFavouriteWeightFirst",
                url: "DataEntry/UseFavourite/{Code}/{date}",
                defaults: new { controller = "DataEntry", action = "UseFavourite", Code = UrlParameter.Optional, date = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DeleteFavouriteWeightFirst",
                url: "DataEntry/DeleteFavourite/{Code}",
                defaults: new { controller = "DataEntry", action = "DeleteFavourite", Code = UrlParameter.Optional }
            );



            //routes.MapRoute(
            //    name: "SaveCaloriesFirst",
            //    url: "CaloriesFirst/Save/{id}/{code}/{calories}",
            //    defaults: new { controller = "CaloriesFirst", action = "Save", id = UrlParameter.Optional, code = UrlParameter.Optional, calories = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "FavouriteCaloriesFirst",
            //    url: "CaloriesFirst/Favourite/{id}/{quantity}",
            //    defaults: new { controller = "CaloriesFirst", action = "Favourite", id = UrlParameter.Optional, quantity = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "UseFavouriteCaloriesFirst",
            //    url: "CaloriesFirst/UseFavourite/{Code}/{date}",
            //    defaults: new { controller = "CaloriesFirst", action = "UseFavourite", Code = UrlParameter.Optional, date = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "DeleteFavouriteCaloriesFirst",
            //    url: "CaloriesFirst/DeleteFavourite/{Code}",
            //    defaults: new { controller = "CaloriesFirst", action = "DeleteFavourite", Code = UrlParameter.Optional }
            //);


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
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "DataEntry", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}