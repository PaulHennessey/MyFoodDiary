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
    public class ProductsController : Controller
    {
        private readonly IProductServices _productServices;
        private readonly IUserServices _userServices;

        public ProductsController()
        { }

        public ProductsController(IProductServices productServices, IUserServices userServices)
        {
            _productServices = productServices;
            _userServices = userServices;
        }

        public ActionResult Index()
        {
            User user = _userServices.GetUser(User.Identity.Name);

            List<Product> items = _productServices.GetCustomProducts(user.Id).OrderBy(x => x.Name).ToList();

            var viewModel = new ProductsViewModel()
            {
                Products = items
            };

            return View("Index", viewModel);
        }


        [HttpGet]
        public ViewResult Create()
        {
            Product product = new Product()
            {
                Code = String.Empty,
                ProductMacronutrients = new ProductMacronutrients().InitialiseList(),
                ProductMicronutrients = new ProductMicronutrients().InitialiseList(),
            };

            ProductViewModel productViewModel = Mapper.Map<Product, ProductViewModel>(product);

            productViewModel.Nutrients = _productServices.GetNutrients(product);

            return View(productViewModel);
        }


        [HttpPost]
        public ActionResult Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = _userServices.GetUser(User.Identity.Name);

                Product product = Mapper.Map<ProductViewModel, Product>(productViewModel);
                product.ProductMacronutrients = _productServices.UpdateProductMacronutrients(productViewModel.Nutrients);
                product.ProductMicronutrients = _productServices.UpdateProductMicronutrients(productViewModel.Nutrients);

                _productServices.CreateProduct(product, user.Id);

                return RedirectToAction("Index");
            }
            else
            {
                return View(productViewModel);
            }
        }


        [HttpGet]
        public ActionResult Edit(string code)
        {
            Product product = _productServices.GetProduct(code);

            ProductViewModel productViewModel = Mapper.Map<Product, ProductViewModel>(product);

            productViewModel.Nutrients = _productServices.GetNutrients(product);

            return View(productViewModel);
        }


        [HttpPost]
        public ActionResult Edit(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                Product product = Mapper.Map<ProductViewModel, Product>(productViewModel);
                product.ProductMacronutrients = _productServices.UpdateProductMacronutrients(productViewModel.Nutrients);
                product.ProductMicronutrients = _productServices.UpdateProductMicronutrients(productViewModel.Nutrients);

                _productServices.UpdateProduct(product);

                return RedirectToAction("Index");
            }
            else
            {
                return View(productViewModel);
            }
        }


        public ActionResult Delete(string code)
        {
            _productServices.DeleteProduct(code);

            return RedirectToAction("Index");
        }
    }
}
