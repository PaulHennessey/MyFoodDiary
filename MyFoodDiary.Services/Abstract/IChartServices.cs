using System.Collections.Generic;
using MyFoodDiary.Domain;

namespace MyFoodDiary.Services.Abstract
{
    public interface IChartServices
    {
        List<decimal> CalculateNutrientByProduct(List<Day> days, List<Product> products, string nutrient);
        List<decimal> CalculateNutrientByDay(List<Day> days, List<Product> products, string nutrient);
        List<decimal> CalculateTotalEnergyData(List<Day> days, List<Product> products);
        List<decimal> CalculateAlcoholByProduct(List<Day> days, List<Product> products);
        List<decimal> CalculateAlcoholByDay(List<Day> days, List<Product> products);
        List<string> GetBarNames(IEnumerable<Day> days);
        List<string> GetDates(IEnumerable<Day> days);
        List<string> GetBarNames(IEnumerable<MealItem> foodItems);
        List<string> GetTotalEnergyCategories();
        string GetTitle(string nutrient);
    }
}
