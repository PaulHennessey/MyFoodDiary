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
    public class MealsController : Controller
    {
        private readonly IMealServices _mealServices;
        private readonly IUserServices _userServices;

        public MealsController()
        { }

        public MealsController(IMealServices mealServices, IUserServices userServices)
        {
            _mealServices = mealServices;
            _userServices = userServices;
        }

        public ActionResult Index()
        {
            User user = _userServices.GetUser(User.Identity.Name);

            List<Meal> items = _mealServices.GetMeals(user.Id).OrderBy(x => x.Name).ToList();

            var viewModel = new MealsViewModel()
            {
                Meals = items
            };

            return View("Index", viewModel);
        }


        //[HttpGet]
        //public ViewResult Create()
        //{
        //    Product product = new Product()
        //    {
        //        Code = String.Empty,
        //        ProductMacronutrients = new ProductMacronutrients().InitialiseList(),
        //        ProductMicronutrients = new ProductMicronutrients().InitialiseList(),
        //    };

        //    ProductViewModel productViewModel = Mapper.Map<Product, ProductViewModel>(product);

        //    productViewModel.MacroNutrients = _mealServices.GetMacroNutrients(product);
        //    productViewModel.MicroNutrients = _mealServices.GetMicroNutrients(product);

        //    return View(productViewModel);
        //}


        //[HttpPost]
        //public ActionResult Create(ProductViewModel productViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        User user = _userServices.GetUser(User.Identity.Name);

        //        Product product = Mapper.Map<ProductViewModel, Product>(productViewModel);
        //        product.ProductMacronutrients = _mealServices.UpdateProductMacronutrients(productViewModel.MacroNutrients);
        //        product.ProductMicronutrients = _mealServices.UpdateProductMicronutrients(productViewModel.MicroNutrients);

        //        _mealServices.CreateProduct(product, user.Id);

        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return View(productViewModel);
        //    }
        //}


        //[HttpGet]
        //public ActionResult Edit(string code)
        //{
        //    Product product = _mealServices.GetProduct(code);

        //    ProductViewModel productViewModel = Mapper.Map<Product, ProductViewModel>(product);

        //    productViewModel.MacroNutrients = _mealServices.GetMacroNutrients(product);
        //    productViewModel.MicroNutrients = _mealServices.GetMicroNutrients(product);

        //    return View(productViewModel);
        //}


        //[HttpPost]
        //public ActionResult Edit(ProductViewModel productViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Product product = Mapper.Map<ProductViewModel, Product>(productViewModel);
        //        product.ProductMacronutrients = _mealServices.UpdateProductMacronutrients(productViewModel.MacroNutrients);
        //        product.ProductMicronutrients = _mealServices.UpdateProductMicronutrients(productViewModel.MicroNutrients);

        //        _mealServices.UpdateProduct(product);

        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return View(productViewModel);
        //    }
        //}


        //public ActionResult Delete(string code)
        //{
        //    _mealServices.DeleteProduct(code);

        //    return RedirectToAction("Index");
        //}
    }
}
