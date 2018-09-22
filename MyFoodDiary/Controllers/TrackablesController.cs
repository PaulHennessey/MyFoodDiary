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
    public class TrackablesController : Controller
    {
        private readonly ITrackablesServices _trackablesServices;
        private readonly IUserServices _userServices;

        public TrackablesController()
        { }

        public TrackablesController(ITrackablesServices trackablesServices, IUserServices userServices)
        {
            _trackablesServices = trackablesServices;
            _userServices = userServices;
        }

        public ActionResult Index()
        {
            User user = _userServices.GetUser(User.Identity.Name);

            List<Trackable> items = _trackablesServices.GetTrackables(user.Id).OrderBy(x => x.Name).ToList();

            var viewModel = new TrackablesViewModel()
            {
                Trackables = items
            };

            return View("Index", viewModel);
        }


        [HttpGet]
        public ViewResult Create()
        {
            Trackable trackable = new Trackable()
            {
                Name = String.Empty,
            };

            TrackableViewModel trackableViewModel = Mapper.Map<Trackable, TrackableViewModel>(trackable);

            return View(trackableViewModel);
        }


        [HttpPost]
        public ActionResult Create(TrackableViewModel trackableViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = _userServices.GetUser(User.Identity.Name);

                Trackable trackable = Mapper.Map<TrackableViewModel, Trackable>(trackableViewModel);

                _trackablesServices.CreateTrackable(trackable, user.Id);

                return RedirectToAction("Index");
            }
            else
            {
                return View(trackableViewModel);
            }
        }


        //[HttpGet]
        //public ActionResult Edit(string code)
        //{
        //    Product product = _productServices.GetProduct(code);

        //    ProductViewModel productViewModel = Mapper.Map<Product, ProductViewModel>(product);

        //    productViewModel.Nutrients = _productServices.GetNutrients(product);

        //    return View(productViewModel);
        //}


        //[HttpPost]
        //public ActionResult Edit(ProductViewModel productViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Product product = Mapper.Map<ProductViewModel, Product>(productViewModel);
        //        product.ProductMacronutrients = _productServices.UpdateProductMacronutrients(productViewModel.Nutrients);
        //        product.ProductMicronutrients = _productServices.UpdateProductMicronutrients(productViewModel.Nutrients);

        //        _productServices.UpdateProduct(product);

        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return View(productViewModel);
        //    }
        //}


        //public ActionResult Delete(string code)
        //{
        //    _productServices.DeleteProduct(code);

        //    return RedirectToAction("Index");
        //}
    }
}
