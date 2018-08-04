using System.Collections.Generic;
using MyFoodDiary.Domain;

namespace MyFoodDiary.Services.Abstract
{
    public interface IProductServices
    {
        IEnumerable<Product> GetProducts(int userId);
        IEnumerable<Product> GetProducts(List<FoodItem> foodItems);
        IEnumerable<Product> GetProducts(List<Day> days);
        List<Product> GetCustomProducts(int userId);
        Product GetProduct(string code);
        void CreateProduct(Product product, int userId);
        void UpdateProduct(Product product);
        void DeleteProduct(string code);
    }
}
