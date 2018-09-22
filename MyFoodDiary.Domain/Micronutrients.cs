using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFoodDiary.Domain
{
    public static class Micronutrients
    {
        public static List<Nutrient> Nutrients = new List<Nutrient>
        {
            new Nutrient { Name = "Calcium", MeasurementUnit = "mg", RDA = 1000 },
            new Nutrient { Name = "Vitamin D", MeasurementUnit = "µg", RDA = 10 },
            new Nutrient { Name = "Vitamin C", MeasurementUnit = "mg", RDA = 100 }
        };

        public static Nutrient Nutrient(string name)
        {
            return Nutrients.Where(m => m.Name == name).First();
        }
    }
}
