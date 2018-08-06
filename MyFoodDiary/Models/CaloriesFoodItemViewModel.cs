using System.ComponentModel.DataAnnotations;

namespace MyFoodDiary.Models
{
    public class CaloriesFoodItemViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Calories { get; set; }
    }
}