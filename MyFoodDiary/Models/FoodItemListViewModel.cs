﻿using System.Collections.Generic;

namespace MyFoodDiary.Models
{
    public class FoodItemListViewModel
    {
        public IEnumerable<FoodItemViewModel> FoodItems { get; set; }         
    }
}