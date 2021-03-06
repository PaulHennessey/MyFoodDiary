﻿using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using MyFoodDiary.Data.Abstract;
using MyFoodDiary.Data.Concrete;
using MyFoodDiary.Services.Abstract;
using MyFoodDiary.Services.Concrete;
using Ninject;

namespace MyFoodDiary.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IChartServices>().To<ChartServices>();
            ninjectKernel.Bind<IFoodItemServices>().To<FoodItemServices>();
            ninjectKernel.Bind<IProductServices>().To<ProductServices>();
            ninjectKernel.Bind<IUserServices>().To<UserServices>();
            ninjectKernel.Bind<IProductMapper>().To<ProductMapper>();
            ninjectKernel.Bind<IFoodItemMapper>().To<FoodItemMapper>();
            ninjectKernel.Bind<IFavouriteMapper>().To<FavouriteMapper>();
            ninjectKernel.Bind<IUserMapper>().To<UserMapper>();
            ninjectKernel.Bind<IProductRepository>().To<ProductRepository>().WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["CoFIDS_ConnectionString"].ConnectionString);
            ninjectKernel.Bind<IFoodItemRepository>().To<FoodItemRepository>().WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["MyFoodDiary_ConnectionString"].ConnectionString);
            ninjectKernel.Bind<IUserRepository>().To<UserRepository>().WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["MyFoodDiary_ConnectionString"].ConnectionString);
            ninjectKernel.Bind<IFavouriteRepository>().To<FavouriteRepository>().WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["MyFoodDiary_ConnectionString"].ConnectionString);
        }
    }
}