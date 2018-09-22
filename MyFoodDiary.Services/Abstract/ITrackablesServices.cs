using System.Collections.Generic;
using MyFoodDiary.Domain;

namespace MyFoodDiary.Services.Abstract
{
    public interface ITrackablesServices
    {
        IEnumerable<Trackable> GetTrackables(int userId);
        //IEnumerable<Product> GetProducts(List<FoodItem> foodItems);
        //IEnumerable<Product> GetProducts(List<Day> days);
        //List<Product> GetCustomProducts(int userId);
        //Product GetProduct(string code);
        void CreateTrackable(Trackable trackable, int userId);
        //void UpdateProduct(Product product);
        //void DeleteProduct(string code);
        //Dictionary<string, decimal> GetNutrients(Product product);
        //ProductMacronutrients UpdateProductMacronutrients(Dictionary<string, decimal> nutrients);
        //ProductMicronutrients UpdateProductMicronutrients(Dictionary<string, decimal> nutrients);
    }
}
