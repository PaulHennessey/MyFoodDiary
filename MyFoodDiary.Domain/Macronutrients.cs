using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodDiary.Domain
{
    public static class Macronutrients
    {
        public static List<Nutrient> Nutrients = new List<Nutrient>
        {
            new Nutrient { Id = 1, Name = "Calories", MeasurementUnit = "kcal" },
            new Nutrient { Id = 2, Name = "Protein", MeasurementUnit = "grams" },
            new Nutrient { Id = 3, Name = "Carbohydrates", MeasurementUnit = "grams" },
            new Nutrient { Id = 4, Name = "Total Sugars", MeasurementUnit = "grams" },
            new Nutrient { Id = 5, Name = "Fat", MeasurementUnit = "grams" },
            new Nutrient { Id = 6, Name = "Alcohol", MeasurementUnit = "units" }
            //new Nutrient { Name = "Macronutrient Ratios", MeasurementUnit = "%" }
        };
    }
}
