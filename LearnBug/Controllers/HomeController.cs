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
using System.Web.UI;
using PagedList;

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
        public ActionResult Index(int? page, string search = "")
        {
            var result = _homeService.Index(search);
            if (!string.IsNullOrEmpty(search))
                ViewBag.Srch = search.Trim();
            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var model = result.ToPagedList(pageNumber, 12);
            return View(model);
        }
    }
}