using System.Collections.Generic;
using MyFoodDiary.Domain;

namespace MyFoodDiary.Services.Abstract
{
    public interface IChartServices
    {
        List<double> CalculateNutrientByProduct(List<Day> days, List<Product> products, string nutrient);
        List<double> CalculateNutrientByDay(List<Day> days, List<Product> products, string nutrient);
        List<double> CalculateTotalEnergyData(List<Day> days, List<Product> products);
        List<double> CalculateAlcoholByProduct(List<Day> days, List<Product> products);
        List<double> CalculateAlcoholByDay(List<Day> days, List<Product> products);
        List<string> GetBarNames(IEnumerable<Day> days);
        List<string> GetDates(IEnumerable<Day> days);
        List<string> GetBarNames(IEnumerable<MealItem> foodItems);
        List<string> GetTotalEnergyCategories();
        string GetTitle(string nutrient);
    }
}
