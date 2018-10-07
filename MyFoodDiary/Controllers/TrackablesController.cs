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


        [HttpGet]
        public ActionResult Edit(int id)
        {
            Trackable trackable = _trackablesServices.GetTrackable(id);

            TrackableViewModel trackableViewModel = Mapper.Map<Trackable, TrackableViewModel>(trackable);

            return View(trackableViewModel);
        }


        [HttpPost]
        public ActionResult Edit(TrackableViewModel trackableViewModel)
        {
            if (ModelState.IsValid)
            {
                Trackable trackable = Mapper.Map<TrackableViewModel, Trackable>(trackableViewModel);

                _trackablesServices.UpdateTrackable(trackable);

                return RedirectToAction("Index");
            }
            else
            {
                return View(trackableViewModel);
            }
        }


        public ActionResult Delete(int id)
        {
            _trackablesServices.DeleteTrackable(id);

            return RedirectToAction("Index");
        }
    }
}