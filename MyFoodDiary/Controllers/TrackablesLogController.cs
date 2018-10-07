using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using MyFoodDiary.Domain;
using MyFoodDiary.Models;
using MyFoodDiary.Services.Abstract;

namespace MyFoodDiary.Controllers
{
    [Authorize]
    public class TrackablesLogController : Controller
    {
        private readonly ITrackableItemServices _trackableItemServices;
        private readonly IUserServices _userServices;

        public TrackablesLogController()
        { }

        public TrackablesLogController(ITrackableItemServices trackableItemServices, IUserServices userServices)
        {
            _trackableItemServices = trackableItemServices;
            _userServices = userServices;
        }

        public ActionResult Index()
        {
            return View("Index", new TrackableItemListViewModel());
        }

        public ActionResult Refresh(DateTime date)
        {
            User user = _userServices.GetUser(User.Identity.Name);
        
            List<TrackableItem> trackableItems = _trackableItemServices.GetTrackableItems(date, user.Id).OrderByDescending(x => x.Id).ToList();

            var viewModel = new TrackableItemListViewModel()
            {
                TrackableItems = Mapper.Map<IEnumerable<TrackableItem>, IEnumerable<TrackableItemViewModel>>(trackableItems),            
            };

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Save(int id, int quantity)
        {
          //  _trackableItemServices.UpdateFoodItem(id, quantity);

            return RedirectToAction("Index");
        }

    }
}
