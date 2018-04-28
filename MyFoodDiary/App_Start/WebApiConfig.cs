﻿using AutoMapper;
using MyFoodDiary.Domain;
using MyFoodDiary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MyFoodDiary
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductViewModel>();
                cfg.CreateMap<ProductViewModel, Product>();
                cfg.CreateMap<FoodItem, FoodItemViewModel>();
                cfg.CreateMap<Favourite, FavouriteViewModel>();
                cfg.CreateMap<Product, ProductAutocompleteViewModel>()
                    .ForMember(dest => dest.label, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.value, opt => opt.MapFrom(src => src.Code));
            });

        }
    }
}