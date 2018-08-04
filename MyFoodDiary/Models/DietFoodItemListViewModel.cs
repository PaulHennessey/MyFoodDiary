using System.Collections.Generic;

namespace MyFoodDiary.Models
{
    public class DietFoodItemListViewModel
    {
        public IEnumerable<DietFoodItemViewModel> FoodItems { get; set; }
        public IEnumerable<FavouriteViewModel> Favourites { get; set; }      
    }
}