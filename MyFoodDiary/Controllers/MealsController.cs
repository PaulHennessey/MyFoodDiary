﻿using System;
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
        private readonly IIngredientServices _ingredientServices;
        private readonly IUserServices _userServices;
        private readonly IProductServices _productServices;

        public MealsController()
        { }

        public MealsController(IMealServices mealServices, IUserServices userServices, IProductServices productServices, IIngredientServices ingredientServices)
        {
            _mealServices = mealServices;
            _userServices = userServices;
            _productServices = productServices;
            _ingredientServices = ingredientServices;
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


        [HttpGet]
        public ViewResult Create()
        {
            Meal meal = new Meal()
            {
                Name = String.Empty,
                Ingredients = new List<Ingredient>
                {
                    new Ingredient{ Id = 1, MealId = 1, Code = "aaa", Quantity = 10 },
                    new Ingredient{ Id = 2, MealId = 1, Code = "bbb", Quantity = 20 },
                    new Ingredient{ Id = 3, MealId = 1, Code = "ccc", Quantity = 30 }
                }
            };

        MealViewModel mealViewModel = Mapper.Map<Meal, MealViewModel>(meal);

            return View(mealViewModel);
        }


        [HttpPost]
        public ActionResult Create(MealViewModel mealViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = _userServices.GetUser(User.Identity.Name);

                Meal meal = Mapper.Map<MealViewModel, Meal>(mealViewModel);

                _mealServices.CreateMeal(meal, user.Id);

                return RedirectToAction("Index");
            }
            else
            {
                return View(mealViewModel);
            }
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            Meal meal = _mealServices.GetMeal(id);

            MealViewModel mealViewModel = Mapper.Map<Meal, MealViewModel>(meal);
            
            return View(mealViewModel);
        }


        [HttpPost]
        public ActionResult Edit(MealViewModel mealViewModel)
        {
            if (ModelState.IsValid)
            {
                Meal meal = Mapper.Map<MealViewModel, Meal>(mealViewModel);

                _mealServices.UpdateMeal(meal);

                return RedirectToAction("Index");
            }
            else
            {
                return View(mealViewModel);
            }
        }

        /// <summary>
        /// This gives the autocomplete field a list of products.
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public ActionResult Search(string term)
        {
            User user = _userServices.GetUser(User.Identity.Name);

            List<Product> items = _productServices.GetProducts(user.Id).ToList();

            var filteredItems = items.Where(item => item.Name.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0);

            IEnumerable<ProductAutocompleteViewModel> viewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductAutocompleteViewModel>>(filteredItems);

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This is called when you select a product from the autocomplete list.
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public ActionResult SelectFood(string code, int mealId)
        {
            //User user = _userServices.GetUser(User.Identity.Name);

            //Product product = _productServices.GetProduct(Code);

            _ingredientServices.CreateIngredient(code, mealId);
            //_foodItemServices.InsertFoodItem(Code, 0, date, user.Id);

            return RedirectToAction("Index");
        }



        //public ActionResult Delete(string code)
        //{
        //    _mealServices.DeleteProduct(code);

        //    return RedirectToAction("Index");
        //}
    }
}