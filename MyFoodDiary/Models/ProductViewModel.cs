using MyFoodDiary.Domain;
using System.Collections.Generic;

namespace MyFoodDiary.Models
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public ProductMacronutrients ProductMacronutrients { get; set; }
        public ProductMicronutrients ProductMicronutrients { get; set; }
    }
}