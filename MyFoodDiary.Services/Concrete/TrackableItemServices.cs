using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MyFoodDiary.Data.Abstract;
using MyFoodDiary.Domain;
using MyFoodDiary.Services.Abstract;

namespace MyFoodDiary.Services.Concrete
{
    public class TrackableItemServices : ITrackableItemServices
    {
        private readonly ITrackableItemRepository _trackableItemRepository;
        private readonly ITrackableItemMapper _trackableItemMapper;
        private readonly ITrackablesRepository _trackablesRepository;
        private readonly ITrackablesMapper _trackablesMapper;

        public TrackableItemServices()
        { }


        public TrackableItemServices(ITrackableItemRepository trackableItemRepository, ITrackableItemMapper trackableItemMapper, ITrackablesRepository productRepository, ITrackablesMapper productMapper)
        {
            _trackableItemRepository = trackableItemRepository;
            _trackableItemMapper = trackableItemMapper;
            _trackablesRepository = productRepository;
            _trackablesMapper = productMapper;
        }


        public IEnumerable<TrackableItem> GetTrackableItems(DateTime dt, int userId)
        {
            //DataTable trackablesTable = _trackablesRepository.GetTrackables(userId);
            //IEnumerable<Trackable> trackables = _trackablesMapper.HydrateTrackables(trackablesTable);


            DataTable trackableItems = _trackableItemRepository.GetTrackableItems(dt, userId);
            return _trackableItemMapper.HydrateTrackableItems(trackableItems);
        }

        //public Day GetDay(DateTime dt, int userId)
        //{
        //    return new Day()
        //    {
        //        Date = dt,
        //        Food = _foodItemMapper.HydrateFoodItems(_foodItemRepository.GetFoodItems(dt, userId)).ToList()
        //    };
        //}

        //public IEnumerable<Day> GetDays(DateTime start, DateTime end, int userId)
        //{
        //    var days = new List<Day>();

        //    while (start <= end)
        //    {
        //        days.Add(GetDay(start, userId));
        //        start = start.AddDays(1);
        //    }
        //    return days;
        //}

        //public void InsertFoodItem(string code, int quantity, DateTime dt, int userId)
        //{
        //    _foodItemRepository.InsertFoodItem(code, quantity, dt, userId);
        //}


        //public void UpdateFoodItem(int id, int quantity)
        //{
        //    _foodItemRepository.UpdateFoodItem(id, quantity);
        //}


        //public void DeleteFoodItem(int id)
        //{
        //    _foodItemRepository.DeleteFoodItem(id);
        //}
    }
}
