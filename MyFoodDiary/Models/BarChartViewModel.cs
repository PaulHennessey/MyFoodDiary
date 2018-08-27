using System.Collections.Generic;
using System.Web.Mvc;
using MyFoodDiary.Domain;

namespace MyFoodDiary.Models
{
    public class BarChartViewModel
    {
        public BarChartViewModel()
        {
            _nutrients.Add(new Nutrient { Id = 1, Name = "Calories" });
            _nutrients.Add(new Nutrient { Id = 2, Name = "Protein" });
            _nutrients.Add(new Nutrient { Id = 3, Name = "Carbohydrates" });
            _nutrients.Add(new Nutrient { Id = 4, Name = "TotalSugars" });
            _nutrients.Add(new Nutrient { Id = 5, Name = "Fat" });            
            _nutrients.Add(new Nutrient { Id = 6, Name = "Alcohol" });
            _nutrients.Add(new Nutrient { Id = 7, Name = "Cholesterol" });
            _nutrients.Add(new Nutrient { Id = 8, Name = "MacronutrientRatio" });
            SelectedNutrientId = 9;
        }

        private readonly List<Nutrient> _nutrients = new List<Nutrient>();
        public int SelectedNutrientId { get; set; }

        public List<string> ChartTitle { get; set; }
        public List<string> BarNames { get; set; }
        public List<List<decimal>> BarData { get; set; }

        public IEnumerable<SelectListItem> Nutrients
        {
            get { return new SelectList(_nutrients, "Id", "Name"); }
        }
    }
}