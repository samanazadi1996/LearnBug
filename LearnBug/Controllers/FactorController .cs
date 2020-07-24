﻿using Models.Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using Services;

namespace LearnBug.Controllers
{
    [Authorize]
    public class FactorController : Controller
    {
        private readonly IFactorService _factorService;

        public FactorController(IFactorService factorService)
        {
            _factorService = factorService;
        }
       public ActionResult Index(int Page = 1)
        {
            var model = _factorService.GetMyBoughtPosts(Page);
            return View(model);
        }

        public ActionResult _SellPost()
        {
            var model = _factorService.GetMySoldPosts();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult CreateFactor(int Id)
        {
            return Json(_factorService.Create(Id));
        }

    }
}