using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MyFoodDiary.Data.Abstract;
using MyFoodDiary.Domain;
using MyFoodDiary.Services.Abstract;

namespace MyFoodDiary.Services.Concrete
{
    public class FoodItemServices : IFoodItemServices
    {
        private readonly IFoodItemRepository _foodItemRepository;
        private readonly IFoodItemMapper _foodItemMapper;


        public FoodItemServices()
        { }


        public FoodItemServices(IFoodItemRepository foodItemRepository, IFoodItemMapper foodItemMapper)
        {
            _foodItemRepository = foodItemRepository;
            _foodItemMapper = foodItemMapper;
        }


        public IEnumerable<FoodItem> GetFoodItems(DateTime dt, int userId)
        {
            DataTable foodItems = _foodItemRepository.GetFoodItems(dt, userId);
            return _foodItemMapper.HydrateFoodItems(foodItems);
        }

        public Day GetDay(DateTime dt, int userId)
        {
            return new Day()
            {
                Date = dt,
                Food = _foodItemMapper.HydrateFoodItems(_foodItemRepository.GetFoodItems(dt, userId)).ToList()
            };
        }

        public IEnumerable<Day> GetDays(DateTime start, DateTime end, int userId)
        {
            var days = new List<Day>();

            while (start <= end)
            {
                days.Add(GetDay(start, userId));
                start = start.AddDays(1);
            }
            return days;
        }

        public void InsertFoodItem(string code, int quantity, DateTime dt, int userId)
        {
            _foodItemRepository.InsertFoodItem(code, quantity, dt, userId);
        }


        public void UpdateFoodItem(int id, int quantity)
        {
            _foodItemRepository.UpdateFoodItem(id, quantity);
        }


        public void DeleteFoodItem(int id)
        {
            _foodItemRepository.DeleteFoodItem(id);
        }

    }
}
