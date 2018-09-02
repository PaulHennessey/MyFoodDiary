using System.Collections.Generic;
using System.Web.Mvc;
using MyFoodDiary.Domain;

namespace MyFoodDiary.Models
{
    public class MacronutrientsViewModel
    {        
        public int SelectedNutrientId { get; set; }

        //private Macronutrients macronutrients = new Macronutrients();
        public IEnumerable<SelectListItem> Nutrients
        {
            get { return new SelectList(Macronutrients.Nutrients, "Id", "Name"); }
        }
    }
}