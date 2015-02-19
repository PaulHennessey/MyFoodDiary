using System;
using System.Collections.Generic;
using System.Linq;
using MyFoodDiary.Domain;
using MyFoodDiary.Services.Abstract;

namespace MyFoodDiary.Services.Concrete
{
    public class ChartServices : IChartServices
    {
        // The Calorie Value of the different nutrients expressed as kcal/g
        const double CalorieValueProtein = 4;
        const double CalorieValueCarbohydrate = 4;
        const double CalorieValueFat = 9;
        const double CalorieValueAlcohol = 7;

        private readonly Dictionary<string, string> _titles = new Dictionary<string, string>();

        public ChartServices()
        {
            _titles.Add("Protein", "Protein in grams");
            _titles.Add("Carbohydrates", "Carbohydrates in grams");
            _titles.Add("Fat", "Fat in grams");
            _titles.Add("Calories", "Calories");
            _titles.Add("Alcohol", "Alcohol in units");
            _titles.Add("TotalEnergy", "% Total Energy");
        }


        /// <summary>
        /// This gives you the amount of nutrient per product. I expect that the list of days will normally just contain one,
        /// but it will work the same if there are multiple days.
        /// </summary>
        /// <param name="days"></param>
        /// <param name="products"></param>
        /// <param name="nutrient"></param>
        /// <returns></returns>
        public List<double> CalculateNutrientByProduct(List<Day> days, List<Product> products, string nutrient)
        {
            // Make a big list of all the fooditems from each day.
            List<FoodItem> foodItems = new List<FoodItem>();
            foreach (Day day in days)
            {
                foodItems.AddRange(day.Food);
            }

            // Now group the fooditems to get rid of repeats.
            var groupedFoodItems = foodItems.
                                   GroupBy(f => f.FoodCode).
                                   Select(fg => new { Code = fg.Key, Total = fg.Sum(f => f.Quantity) }).ToList();

            // If ServingSize == 0, we assume the data is per 100g. Otherwise it's per serving.
            var actualNutrientValues = from g in groupedFoodItems
                                       join p in products
                                       on g.Code equals p.Code
                                       select new
                                       {
                                           TotalNutrient = p.ServingSize == 0 ? Math.Round(g.Total * (p.Nutrients[nutrient] / 100), 1) :
                                                                                Math.Round(g.Total * (p.Nutrients[nutrient]), 1)

                                       };
            
            // Now convert the anonymous types to a list of objects for the Highchart. 
            var nutrients = new List<double>();
            double total = 0;
            foreach (var product in actualNutrientValues)
            {
                nutrients.Add(product.TotalNutrient);
                total += product.TotalNutrient;
            }

            nutrients.Add(total);
            if (nutrient.ToLower() == "alcohol")
            {
                ConvertAlcoholToUnits(ref nutrients);
            }

            return nutrients;
        }


        /// <summary>
        /// This is used for the line charts.
        /// </summary>
        /// <param name="days"></param>
        /// <param name="products"></param>
        /// <param name="nutrient"></param>
        /// <returns></returns>
        public List<double> CalculateNutrientByDay(List<Day> days, List<Product> products, string nutrient)
        {
            var nutrients = new List<double>();

            foreach (Day day in days)
            {
                // Now group the fooditems to get rid of repeats.
                var groupedFoodItems = day.Food.
                                       GroupBy(f => f.FoodCode).
                                       Select(fg => new { Code = fg.Key, Total = fg.Sum(f => f.Quantity) }).ToList();

                var actualNutrientValues = from g in groupedFoodItems
                                           join p in products
                                           on g.Code equals p.Code
                                           select new
                                           {
                                               TotalNutrient = p.ServingSize == 0 ? Math.Round(g.Total * (p.Nutrients[nutrient] / 100), 1) :
                                                                                    Math.Round(g.Total * (p.Nutrients[nutrient]), 1)                                                                                    
                                           };

                double totalNutrientByDay = actualNutrientValues.Sum(product => product.TotalNutrient);
                nutrients.Add(totalNutrientByDay);
            }

            // Alcohol is different because even when we do want to set the serving size (e.g. 568ml), we 
            // still want the data to be entered per 100ml - i.e. we want to use the ABV because 
            // that's what it says on the label.
            if (nutrient.ToLower() == "alcohol")
            {
                ConvertAlcoholToUnits(ref nutrients);
            }

            return nutrients;
        }


        public List<double> CalculateTotalEnergyData(List<Day> days, List<Product> products)
        {
            // Make a big list of all the fooditems from each day.
            List<FoodItem> foodItems = new List<FoodItem>();
            foreach (Day day in days)
            {
                foodItems.AddRange(day.Food);
            }

            // First group the fooditems in case there are repeats, e.g. two apples.
            var groupedFoodItems = foodItems.
                                   GroupBy(f => f.FoodCode).
                                   Select(fg => new { Code = fg.Key, Total = fg.Sum(f => f.Quantity) }).ToList();


            var actualCarbohydrateValues = from g in groupedFoodItems
                                           join p in products
                                           on g.Code equals p.Code
                                           select new
                                           {
                                               TotalNutrient = p.ValuesArePerItem ? Math.Round(g.Total * (p.Nutrients["Carbohydrates"]), 1) : Math.Round(g.Total * (p.Nutrients["Carbohydrates"] / 100), 1)
                                           };

            var actualProteinValues = from g in groupedFoodItems
                                      join p in products
                                      on g.Code equals p.Code
                                      select new
                                      {
                                          TotalNutrient = p.ValuesArePerItem ? Math.Round(g.Total * (p.Nutrients["Protein"]), 1) : Math.Round(g.Total * (p.Nutrients["Protein"] / 100), 1)
                                      };

            var actualFatValues = from g in groupedFoodItems
                                  join p in products
                                  on g.Code equals p.Code
                                  select new
                                  {
                                      TotalNutrient = p.ValuesArePerItem ? Math.Round(g.Total * (p.Nutrients["Fat"]), 1) : Math.Round(g.Total * (p.Nutrients["Fat"] / 100), 1)
                                  };

            // Now convert the anonymous types to a list of objects for the Highchart.
            var nutrients = new List<double>();

            double carbohydrates = actualCarbohydrateValues.Sum(product => product.TotalNutrient);
            double protein = actualProteinValues.Sum(product => product.TotalNutrient);
            double fat = actualFatValues.Sum(product => product.TotalNutrient);

            double carbohydrateCalories = carbohydrates * CalorieValueCarbohydrate;
            double proteinCalories = protein * CalorieValueProtein;
            double fatCalories = fat * CalorieValueFat;
            double totalCalories = carbohydrateCalories + proteinCalories + fatCalories;

            nutrients.Add(Math.Round((carbohydrateCalories / totalCalories) * 100, 1));
            nutrients.Add(Math.Round((proteinCalories / totalCalories) * 100, 1));
            nutrients.Add(Math.Round((fatCalories / totalCalories) * 100, 1));

            return nutrients;
        }


        private void ConvertAlcoholToUnits(ref List<double> nutrients)
        {
            for (int i = 0; i < nutrients.Count; i++)
            {
                nutrients[i] = nutrients[i] / CalorieValueAlcohol;
            }
        }


        public List<string> GetBarNames(IEnumerable<FoodItem> foodItems)
        {
            // First group the fooditems in case there are repeats, e.g. two apples.
            var groupedFoodItems = foodItems.
                                   GroupBy(f => f.FoodCode).
                                   Select(fg => new { fg.First().Name });

            var names = groupedFoodItems.Select(g => g.Name).ToList();
            names.Add("Total");

            return names;
        }


        public List<string> GetBarNames(IEnumerable<Day> days)
        {
            // Make a big list of all the fooditems from each day.
            List<FoodItem> foodItems = new List<FoodItem>();
            foreach (Day day in days)
            {
                foodItems.AddRange(day.Food);
            }

            // Now group the fooditems to get rid of repeats.
            List<string> names = foodItems.
                                   GroupBy(f => f.FoodCode).
                                   Select(fg => fg.First().Name).ToList();

            names.Add("Total");
            return names;
        }


        public List<string> GetDates(IEnumerable<Day> days)
        {
            return days.Select(d => d.Date.ToShortDateString()).ToList();
        }


        public List<string> GetTotalEnergyCategories()
        {
            return new List<string> { "Carbohydrates", "Protein", "Fat" };
        }


        public string GetTitle(string nutrient)
        {
            return _titles[nutrient];
        }
    }
}
