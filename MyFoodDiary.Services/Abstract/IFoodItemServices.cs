using System;
using System.Collections.Generic;
using MyFoodDiary.Domain;

namespace MyFoodDiary.Services.Abstract
{
    public interface IFoodItemServices
    {
        IEnumerable<FoodItem> GetFoodItems(DateTime dt, int userId);
        IEnumerable<Day> GetDays(DateTime start, DateTime end, int userId);
        void InsertFoodItem(string code, int quantity, DateTime dt, int userId);
        void UpdateFoodItem(int id, int quantity);
        void DeleteFoodItem(int id);
    }
}
