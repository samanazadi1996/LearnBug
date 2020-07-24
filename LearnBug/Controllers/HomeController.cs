using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NLog;
using Models;
using ViewModels;
using Services;

namespace LearnBug.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;
        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }
        public ActionResult About()
        {
            var model = _homeService.About();
            return View(model);
        }
        public ActionResult Index(string search = null, int Page = 1)
        {
            var model = _homeService.Index(search, Page);
            if (!string.IsNullOrEmpty(search))
                ViewBag.Srch = search.Trim();
            return View(model);
        }
    }
}