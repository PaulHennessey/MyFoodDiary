using System.ComponentModel.DataAnnotations;

namespace MyFoodDiary.Models
{
    public class DietFoodItemViewModel
    {
        public DietFoodItemViewModel()
        {
            Calories = 23;
        }
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Calories { get; set; }
    }
}