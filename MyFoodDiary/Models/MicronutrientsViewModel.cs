using System.Collections.Generic;
using System.Web.Mvc;
using MyFoodDiary.Domain;

namespace MyFoodDiary.Models
{
    public class MicronutrientsViewModel
    {        
        public int SelectedNutrientId { get; set; }

        //private Micronutrients micronutrients = new Micronutrients();
        public IEnumerable<SelectListItem> Nutrients
        {
            get { return new SelectList(Micronutrients.Nutrients, "Id", "Name"); }
        }
    }
}