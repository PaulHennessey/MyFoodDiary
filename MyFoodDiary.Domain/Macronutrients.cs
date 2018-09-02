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
            new Nutrient { Name = "Calories", MeasurementUnit = "kcal" },
            new Nutrient { Name = "Protein", MeasurementUnit = "grams" },
            new Nutrient { Name = "Carbohydrates", MeasurementUnit = "grams" },
            new Nutrient { Name = "Total Sugars", MeasurementUnit = "grams" },
            new Nutrient { Name = "Fat", MeasurementUnit = "grams" },
            new Nutrient { Name = "Alcohol", MeasurementUnit = "units" }
            //new Nutrient { Name = "Macronutrient Ratios", MeasurementUnit = "%" }
        };
    }
}
