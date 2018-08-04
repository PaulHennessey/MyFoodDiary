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
        private const decimal CalorieValueProtein = 4;
        private const decimal CalorieValueCarbohydrate = 4;
        private const decimal CalorieValueFat = 9;
        private const decimal CalorieValueAlcohol = 7;
        private const decimal SpecificGravityOfAlcohol = 0.789m;

        private readonly Dictionary<string, string> _titles = new Dictionary<string, string>();

        public ChartServices()
        {
            _titles.Add("Protein", "Protein in grams");
            _titles.Add("Carbohydrates", "Carbohydrates in grams");
            _titles.Add("TotalSugars", "Total sugars in grams");
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
        public List<decimal> CalculateNutrientByProduct(List<Day> days, List<Product> products, string nutrient)
        {
            // Make a big list of all the fooditems from each day.
            List<FoodItem> foodItems = new List<FoodItem>();
            foreach (Day day in days)
            {
                foodItems.AddRange(day.Food);
            }

            // Now group the fooditems to get rid of repeats.
            var groupedFoodItems = foodItems.
                                   GroupBy(f => f.Code).
                                   Select(fg => new { Code = fg.Key, Total = fg.Sum(f => f.Quantity) }).ToList();

            var actualNutrientValues = from g in groupedFoodItems
                                       join p in products
                                       on g.Code equals p.Code
                                       select new
                                       {
                                           TotalNutrient = GetTotalNutrient(g.Total, p, nutrient)
                                       };

            // Now convert the anonymous types to a list of objects for the Highchart. 
            var nutrients = new List<decimal>();
            decimal total = 0;
            foreach (var product in actualNutrientValues)
            {
                nutrients.Add(product.TotalNutrient);
                total += product.TotalNutrient;
            }

            nutrients.Add(total);
            return nutrients;
        }


        /// <summary>
        /// Note the alcohol data is stored as ABV, i.e. units of alcohol per 100ml. We want to display 'units', e.g. one 
        /// pint of Bass (4.2 ABV) is 2.4 units.
        /// </summary>
        /// <param name="days"></param>
        /// <param name="products"></param>
        /// <returns></returns>
        public List<decimal> CalculateAlcoholByProduct(List<Day> days, List<Product> products)
        {
            // Make a big list of all the fooditems from each day.
            List<FoodItem> foodItems = new List<FoodItem>();
            foreach (Day day in days)
            {
                foodItems.AddRange(day.Food);
            }

            // Now group the fooditems to get rid of repeats.
            var groupedFoodItems = foodItems.
                                   GroupBy(f => f.Code).
                                   Select(fg => new { Code = fg.Key, Total = fg.Sum(f => f.Quantity) }).ToList();
            
            var actualNutrientValues = from g in groupedFoodItems
                                       join p in products
                                       on g.Code equals p.Code
                                       select new
                                       {
                                           TotalNutrient = GetTotalAlcoholUnits(g.Total, p)
                                       };

            // Now convert the anonymous types to a list of objects for the Highchart. 
            var nutrients = new List<decimal>();
            decimal total = 0;
            foreach (var product in actualNutrientValues)
            {
                nutrients.Add(product.TotalNutrient);
                total += product.TotalNutrient;
            }

            nutrients.Add(Math.Round(total, 1));

            return nutrients;
        }



        public List<decimal> CalculateAlcoholByDay(List<Day> days, List<Product> products)
        {
            var nutrients = new List<decimal>();

            foreach (Day day in days)
            {
                // Now group the fooditems to get rid of repeats.
                var groupedFoodItems = day.Food.
                                       GroupBy(f => f.Code).
                                       Select(fg => new { Code = fg.Key, Total = fg.Sum(f => f.Quantity) }).ToList();

                var actualNutrientValues = from g in groupedFoodItems
                                           join p in products
                                           on g.Code equals p.Code
                                           select new
                                           {
                                               TotalNutrient = GetTotalAlcoholUnits(g.Total, p)
                                           };

                decimal totalNutrientByDay = actualNutrientValues.Sum(product => product.TotalNutrient);
                nutrients.Add(totalNutrientByDay);
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
        public List<decimal> CalculateNutrientByDay(List<Day> days, List<Product> products, string nutrient)
        {
            var nutrients = new List<decimal>();

            foreach (Day day in days)
            {
                // Now group the fooditems to get rid of repeats.
                var groupedFoodItems = day.Food.
                                       GroupBy(f => f.Code).
                                       Select(fg => new { Code = fg.Key, Total = fg.Sum(f => f.Quantity) }).ToList();

                var actualNutrientValues = from g in groupedFoodItems
                                           join p in products
                                           on g.Code equals p.Code
                                           select new
                                           {
                                               TotalNutrient = GetTotalNutrient(g.Total, p, nutrient)
                                           };

                decimal totalNutrientByDay = actualNutrientValues.Sum(product => product.TotalNutrient);
                nutrients.Add(totalNutrientByDay);
            }

            return nutrients;
        }


        public List<decimal> CalculateTotalEnergyData(List<Day> days, List<Product> products)
        {
            // Make a big list of all the fooditems from each day.
            List<FoodItem> foodItems = new List<FoodItem>();
            foreach (Day day in days)
            {
                foodItems.AddRange(day.Food);
            }

            // First group the fooditems in case there are repeats, e.g. two apples.
            var groupedFoodItems = foodItems.
                                   GroupBy(f => f.Code).
                                   Select(fg => new { Code = fg.Key, Total = fg.Sum(f => f.Quantity) }).ToList();


            var actualCarbohydrateValues = from g in groupedFoodItems
                                           join p in products
                                           on g.Code equals p.Code
                                           select new
                                           {
                                               TotalNutrient = GetTotalNutrient(g.Total, p, "Carbohydrates")
                                           };

            var actualProteinValues = from g in groupedFoodItems
                                      join p in products
                                      on g.Code equals p.Code
                                      select new
                                      {
                                          TotalNutrient = GetTotalNutrient(g.Total, p, "Protein")
                                      };

            var actualFatValues = from g in groupedFoodItems
                                  join p in products
                                  on g.Code equals p.Code
                                  select new
                                  {
                                      TotalNutrient = GetTotalNutrient(g.Total, p, "Fat")
                                  };

            // Now convert the anonymous types to a list of objects for the Highchart.
            var nutrients = new List<decimal>();

            decimal carbohydrates = actualCarbohydrateValues.Sum(product => product.TotalNutrient);
            decimal protein = actualProteinValues.Sum(product => product.TotalNutrient);
            decimal fat = actualFatValues.Sum(product => product.TotalNutrient);

            decimal carbohydrateCalories = carbohydrates * CalorieValueCarbohydrate;
            decimal proteinCalories = protein * CalorieValueProtein;
            decimal fatCalories = fat * CalorieValueFat;
            decimal totalCalories = carbohydrateCalories + proteinCalories + fatCalories;

            nutrients.Add(Math.Round((carbohydrateCalories / totalCalories) * 100, 1));
            nutrients.Add(Math.Round((proteinCalories / totalCalories) * 100, 1));
            nutrients.Add(Math.Round((fatCalories / totalCalories) * 100, 1));

            return nutrients;
        }


        public List<string> GetBarNames(IEnumerable<FoodItem> foodItems)
        {
            // First group the fooditems in case there are repeats, e.g. two apples.
            var groupedFoodItems = foodItems.
                                   GroupBy(f => f.Code).
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
                                   GroupBy(f => f.Code).
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


        private decimal GetTotalNutrient(int total, Product product, string nutrient)
        {
            return Math.Round(total * (product.Nutrients[nutrient] / 100), 1);
        }

        /// <summary>
        /// Alcohol (ALCO). Values are given as g/100 ml. Pure ethyl alcohol has a
        /// specific gravity of 0.789, dividing values by 0.789 converts them to alcohol by
        /// volume(ml/100 ml). 
        /// 
        /// So to calculate units, (ABV * vol(ml))/1000.
        /// </summary>
        /// <param name="total"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        private decimal GetTotalAlcoholUnits(int total, Product product)
        {
            decimal alcoholByWeight = product.Nutrients["Alcohol"];
            decimal alcoholByVolume = alcoholByWeight / SpecificGravityOfAlcohol;

            return Math.Round((alcoholByVolume * total) / 1000, 1);
        }

    }
}
