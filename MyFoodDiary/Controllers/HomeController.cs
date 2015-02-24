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
    public class HomeController : Controller
    {
        private readonly IFoodItemServices _foodItemServices;
        private readonly IProductServices _productServices;
        private readonly IUserServices _userServices;

        public HomeController()
        { }

        public HomeController(IFoodItemServices foodItemServices, IProductServices productServices, IUserServices userServices)
        {
            _productServices = productServices;
            _foodItemServices = foodItemServices;
            _userServices = userServices;
            Mapper.CreateMap<FoodItem, FoodItemViewModel>();
            Mapper.CreateMap<Favourite, FavouriteViewModel>();
            Mapper.CreateMap<Product, ProductAutocompleteViewModel>()
                .ForMember(dest => dest.label, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.value, opt => opt.MapFrom(src => src.Code));
        }

        public ActionResult Index()
        {
            return View("Index", new FoodItemListViewModel());
        }

        public ActionResult Refresh(DateTime date)
        {
            User user = _userServices.GetUser(User.Identity.Name);

            // Order by Id, so the most recent item is at the top of the list.
            List<FoodItem> foodItems = _foodItemServices.GetFoodItems(date, user.Id).OrderByDescending(x => x.Id).ToList();
            List<Favourite> favourites = _foodItemServices.GetFavourites(user.Id).OrderByDescending(x => x.Name).ToList();

            var viewModel = new FoodItemListViewModel()
            {
                FoodItems = Mapper.Map<IEnumerable<FoodItem>, IEnumerable<FoodItemViewModel>>(foodItems),
                Favourites = Mapper.Map<IEnumerable<Favourite>, IEnumerable<FavouriteViewModel>>(favourites)
            };

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Save(int id, int quantity)
        {
            _foodItemServices.UpdateFoodItem(id, quantity);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _foodItemServices.DeleteFoodItem(id);

            return RedirectToAction("Index");
        }
    
        /// <summary>
        /// When we favourite something, we also want to save it - otherwise it is possible to edit a quantity, then
        /// update the Favourites table without updating the FoodItems.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public ActionResult Favourite(int id, int quantity)
        {
            User user = _userServices.GetUser(User.Identity.Name);

            _foodItemServices.UpdateFoodItem(id, quantity);
            _foodItemServices.MergeFavourite(user.Id, id, quantity);

            return RedirectToAction("Index");
        }


        public ActionResult UseFavourite(string Code, DateTime date)
        {
            User user = _userServices.GetUser(User.Identity.Name);

            Favourite favourite = _foodItemServices.GetFavourites(user.Id).Where(f => f.Code == Code).First();                
            _foodItemServices.InsertFoodItem(Code, favourite.Quantity, date, user.Id);

            return RedirectToAction("Index");
        }


        public ActionResult DeleteFavourite(string Code)
        {
            User user = _userServices.GetUser(User.Identity.Name);
            
            _foodItemServices.DeleteFavourite(user.Id, Code);

            return RedirectToAction("Index");
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
        public ActionResult SelectFood(string Code, DateTime date)
        {
            User user = _userServices.GetUser(User.Identity.Name);

            Product product = _productServices.GetProduct(Code);

            _foodItemServices.InsertFoodItem(Code, product.ServingSize > 0 ? 1 : 0, date, user.Id);

            return RedirectToAction("Index");
        }

    }
}
