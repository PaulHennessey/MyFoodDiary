using System.ComponentModel.DataAnnotations;

namespace MyFoodDiary.Models
{
    public class TrackableItemViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Quantity { get; set; }
    }
}