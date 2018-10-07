using System;
using System.Collections.Generic;
using MyFoodDiary.Domain;

namespace MyFoodDiary.Services.Abstract
{
    public interface ITrackableItemServices
    {
        IEnumerable<TrackableItem> GetTrackableItems(DateTime dt, int userId);
        //IEnumerable<Day> GetDays(DateTime start, DateTime end, int userId);
        //Day GetDay(DateTime date, int userId);
        //void InsertFoodItem(string code, int quantity, DateTime dt, int userId);
        //void UpdateFoodItem(int id, int quantity);
        //void DeleteFoodItem(int id);
    }
}
