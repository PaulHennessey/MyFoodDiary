using System.Collections.Generic;
using System.Web.Mvc;

namespace MyFoodDiary.Models
{
    public class Nutrient
    {       
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public enum Nutrients
    {
        Protein,
        Fat
    }

    public class BarChartViewModel
    {
        public BarChartViewModel()
        {
            _nutrients.Add(new Nutrient { Id = 1, Name = "Fat" });
            _nutrients.Add(new Nutrient { Id = 2, Name = "Protein" });
        }

        private readonly List<Nutrient> _nutrients = new List<Nutrient>();
        public int SelectedNutrientId { get; set; }

        public string ChartTitle { get; set; }
        public List<string> BarNames { get; set; }
        public List<decimal> BarData { get; set; }
        public Nutrients Nutrient { get; set; }

        public IEnumerable<SelectListItem> Nutrients
        {
            get { return new SelectList(_nutrients, "Id", "Name"); }
        }
    }
}