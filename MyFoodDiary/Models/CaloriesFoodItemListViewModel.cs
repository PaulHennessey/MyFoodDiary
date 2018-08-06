using System.Collections.Generic;

namespace MyFoodDiary.Models
{
    public class CaloriesFoodItemListViewModel
    {
        public IEnumerable<CaloriesFoodItemViewModel> FoodItems { get; set; }
        public IEnumerable<FavouriteViewModel> Favourites { get; set; }      
    }
}