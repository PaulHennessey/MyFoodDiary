using System;
using System.Data;

namespace MyFoodDiary.Data.Abstract
{
    public interface ITrackableItemRepository
    {
        DataTable GetTrackableItems(DateTime dt, int userId);
        //void InsertTrackableItem(string code, int quantity, DateTime dt, int userId);
        //void UpdateTrackableItem(int id, int quantity);
        //void DeleteTrackableItem(int id);
    }
}
