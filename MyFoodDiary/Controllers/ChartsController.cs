using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MyFoodDiary.Domain;
using MyFoodDiary.Models;
using MyFoodDiary.Services.Abstract;

namespace MyFoodDiary.Controllers
{
    [Authorize]
    public class ChartsController : Controller
    {
        private readonly IFoodItemServices _foodItemServices;
        private readonly IProductServices _productServices;
        private readonly IChartServices _chartServices;
        private readonly IUserServices _userServices;

        public ChartsController()
        { }

        public ChartsController(IChartServices chartServices, IFoodItemServices foodItemServices, IProductServices productServices, IUserServices userServices)
        {
            _productServices = productServices;
            _foodItemServices = foodItemServices;
            _chartServices = chartServices;
            _userServices = userServices;
        }

        public ActionResult Index()
        {
            return View("Index", new BarChartViewModel());
        }

        public ActionResult RefreshBarChart(DateTime start, DateTime end, string nutrient)
        {
            User user = _userServices.GetUser(User.Identity.Name);

            //start = start.AddDays(-6);

            List<Day> days = _foodItemServices.GetDays(start, end, user.Id).ToList();
            List<Product> products = _productServices.GetProducts(days).ToList();
            var viewModel = new BarChartViewModel();

            if (nutrient.ToLower() == "totalenergy")
            {
                viewModel.BarNames = _chartServices.GetTotalEnergyCategories();
                viewModel.ChartTitle = _chartServices.GetTitle(nutrient);
                viewModel.BarData = _chartServices.CalculateTotalEnergyData(days, products);
            }
            else
            {
                viewModel.BarNames = _chartServices.GetBarNames(days);
                viewModel.ChartTitle = _chartServices.GetTitle(nutrient);
                viewModel.BarData = _chartServices.CalculateNutrientByProduct(days, products, nutrient);
            }

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }


        public ActionResult RefreshLineChart(DateTime start, DateTime end, string nutrient)
        {
            start = end.AddDays(-6);

            User user = _userServices.GetUser(User.Identity.Name);

            List<Day> days = _foodItemServices.GetDays(start, end, user.Id).ToList();
            List<Product> products = _productServices.GetProducts(days).ToList();
            var viewModel = new LineChartViewModel
            {
                BarNames = _chartServices.GetDates(days),
                ChartTitle = _chartServices.GetTitle(nutrient),
                BarData = _chartServices.CalculateNutrientByDay(days, products, nutrient)
            };

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
    }
}
