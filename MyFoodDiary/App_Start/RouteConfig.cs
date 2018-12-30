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

            routes.MapRoute(
                name: "SaveTrackableItem",
                url: "TrackablesLog/Save/{id}/{trackableId}/{quantity}/{date}",
                defaults: new { controller = "TrackablesLog", action = "Save", id = UrlParameter.Optional, trackableId = UrlParameter.Optional, quantity = UrlParameter.Optional, date = UrlParameter.Optional }
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


            routes.MapRoute(
                name: "UseMealWeightFirst",
                url: "DataEntry/UseMeal/{id}/{date}",
                defaults: new { controller = "DataEntry", action = "UseMeal", id = UrlParameter.Optional, date = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "CreateMeal",
                url: "Meals/Create",
                defaults: new { controller = "Meals", action = "Create" }
            );

            routes.MapRoute(
                name: "DeleteMeal",
                url: "Meals/DeleteMeal/{mealId}",
                defaults: new { controller = "Meals", action = "DeleteMeal", mealId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "DeleteIngredient",
                url: "Meals/DeleteIngredient/{id}/{mealId}",
                defaults: new { controller = "Meals", action = "DeleteIngredient",  id = UrlParameter.Optional, mealId = UrlParameter.Optional}
            );

            routes.MapRoute(
                name: "SaveIngredient",
                url: "Meals/SaveIngredient/{id}/{mealId}/{quantity}",
                defaults: new { controller = "Meals", action = "SaveIngredient", id = UrlParameter.Optional, mealId = UrlParameter.Optional, quantity = UrlParameter.Optional }
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
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "DataEntry", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}