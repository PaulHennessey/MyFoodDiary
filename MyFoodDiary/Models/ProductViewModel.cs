using System.Collections.Generic;

namespace MyFoodDiary.Models
{
    public class ProductViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public Dictionary<string, double> Nutrients { get; set; }
    }
}