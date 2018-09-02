using System.Collections.Generic;
using System.Data;
using System.Linq;
using MyFoodDiary.Data.Abstract;
using MyFoodDiary.Domain;
using MyFoodDiary.Services.Abstract;

namespace MyFoodDiary.Services.Concrete
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductMapper _productMapper;

        public ProductServices()
        { }

        public ProductServices(IProductRepository productRepository, IProductMapper productMapper)
        {
            _productRepository = productRepository;
            _productMapper = productMapper;
        }

        public IEnumerable<Product> GetProducts(int userId)
        {
            DataTable dataTable = _productRepository.GetProducts(userId);
            return _productMapper.HydrateProducts(dataTable);
        }

        public IEnumerable<Product> GetProducts(List<FoodItem> foodItems)
        {
            DataTable dataTable = _productRepository.GetProducts(foodItems);
            return _productMapper.HydrateProducts(dataTable);
        }

        public IEnumerable<Product> GetProducts(List<Day> days)
        {
            IEnumerable<FoodItem> foodItems = new List<FoodItem>();

            foreach (var day in days)
            {
                foodItems = foodItems.Concat(day.Food);
            }

            DataTable dataTable = _productRepository.GetProducts(foodItems);
            return _productMapper.HydrateProducts(dataTable);
        }

        public void CreateProduct(Product product, int userId)
        {
            _productRepository.CreateProduct(product, userId);
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.UpdateProduct(product);
        }

        public Product GetProduct(string code)
        {
            DataTable dataTable = _productRepository.GetProduct(code);
            return _productMapper.HydrateProducts(dataTable).FirstOrDefault();
        }

        public List<Product> GetCustomProducts(int userId)
        {
            DataTable dataTable = _productRepository.GetCustomProducts(userId);
            return _productMapper.HydrateProducts(dataTable).ToList();
        }

        public void DeleteProduct(string code)
        {
            _productRepository.DeleteProduct(code);
        }

    }
}
