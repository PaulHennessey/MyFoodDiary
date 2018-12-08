using System.Collections.Generic;
using MyFoodDiary.Domain;

namespace MyFoodDiary.Services.Abstract
{
    public interface IMealServices
    {
        IEnumerable<Meal> GetMeals(int userId);
        Meal GetMeal(int id);
        void CreateMeal(Meal meal, int userId);
        void UpdateMeal(Meal meal);
        //void DeleteProduct(string code);
    }
}
