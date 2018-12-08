using System.Collections.Generic;
using System.Data;
using System.Linq;
using MyFoodDiary.Data.Abstract;
using MyFoodDiary.Domain;
using MyFoodDiary.Services.Abstract;

namespace MyFoodDiary.Services.Concrete
{
    public class MealServices : IMealServices
    {
        private readonly IMealRepository _mealRepository;
        private readonly IMealMapper _mealMapper;

        public MealServices()
        { }

        public MealServices(IMealRepository mealRepository, IMealMapper mealMapper)
        {
            _mealRepository = mealRepository;
            _mealMapper = mealMapper;
        }

        public IEnumerable<Meal> GetMeals(int userId)
        {
            DataTable dataTable = _mealRepository.GetMeals(userId);
            return _mealMapper.HydrateMeals(dataTable);
        }

        //public IEnumerable<Product> GetProducts(List<FoodItem> foodItems)
        //{
        //    DataTable dataTable = _productRepository.GetProducts(foodItems);
        //    return _productMapper.HydrateProducts(dataTable);
        //}

        //public IEnumerable<Product> GetProducts(List<Day> days)
        //{
        //    IEnumerable<FoodItem> foodItems = new List<FoodItem>();

        //    foreach (var day in days)
        //    {
        //        foodItems = foodItems.Concat(day.Food);
        //    }

        //    DataTable dataTable = _productRepository.GetProducts(foodItems);
        //    return _productMapper.HydrateProducts(dataTable);
        //}

        public void CreateMeal(Meal meal, int userId)
        {
            _mealRepository.CreateMeal(meal, userId);
        }

        public void UpdateMeal(Meal meal)
        {
            _mealRepository.UpdateMeal(meal);
        }

        public Meal GetMeal(int id)
        {
            DataTable dataTable = _mealRepository.GetMeal(id);
            return _mealMapper.HydrateMeals(dataTable).FirstOrDefault();
        }

        //public List<Product> GetCustomProducts(int userId)
        //{
        //    DataTable dataTable = _productRepository.GetCustomProducts(userId);
        //    return _productMapper.HydrateProducts(dataTable).ToList();
        //}

        //public void DeleteProduct(string code)
        //{
        //    _productRepository.DeleteProduct(code);
        //}

        //public Dictionary<string, decimal> GetNutrients(Product product)
        //{
        //    var nutrients = new Dictionary<string, decimal>();

        //    foreach(ProductNutrient productNutrient in product.ProductMacronutrients.ProductNutrients)
        //    {
        //        nutrients.Add(productNutrient.Name, productNutrient.Quantity);
        //    }

        //    foreach (ProductNutrient productNutrient in product.ProductMicronutrients.ProductNutrients)
        //    {
        //        nutrients.Add(productNutrient.Name, productNutrient.Quantity);
        //    }

        //    return nutrients;
        //}

        //public Dictionary<string, decimal> GetMacroNutrients(Product product)
        //{
        //    var nutrients = new Dictionary<string, decimal>();

        //    foreach (ProductNutrient productNutrient in product.ProductMacronutrients.ProductNutrients)
        //    {
        //        nutrients.Add(productNutrient.Name, productNutrient.Quantity);
        //    }

        //    return nutrients;
        //}

        //public Dictionary<string, decimal> GetMicroNutrients(Product product)
        //{
        //    var nutrients = new Dictionary<string, decimal>();

        //    foreach (ProductNutrient productNutrient in product.ProductMicronutrients.ProductNutrients)
        //    {
        //        nutrients.Add(productNutrient.Name, productNutrient.Quantity);
        //    }

        //    return nutrients;
        //}

        //public ProductMacronutrients UpdateProductMacronutrients(Dictionary<string, decimal> nutrients)
        //{
        //    var productMacronutrients = new ProductMacronutrients().InitialiseList();

        //    foreach (var key in nutrients.Keys)
        //    {               
        //        if (productMacronutrients.ProductNutrients.Exists(n => n.Name == key))
        //            productMacronutrients.Update(key, nutrients[key]);
        //    }

        //    return productMacronutrients;
        //}

        //public ProductMicronutrients UpdateProductMicronutrients(Dictionary<string, decimal> nutrients)
        //{
        //    var productMicronutrients = new ProductMicronutrients().InitialiseList();

        //    foreach (var key in nutrients.Keys)
        //    {
        //        if (productMicronutrients.ProductNutrients.Exists(n => n.Name == key))
        //            productMicronutrients.Update(key, nutrients[key]);
        //    }

        //    return productMicronutrients;
        //}
    }
}
