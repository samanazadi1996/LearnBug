using Models.Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services;

namespace LearnBug.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FactorController : Controller
    {
        private readonly IFactorService _factorService;

        public FactorController(IFactorService factorService)
        {
            _factorService = factorService;
        }
        public ActionResult Index(string from = null, string to = null)
        {
            var model = _factorService.GetAllFactors(from, to);
            ViewBag.from = from;
            ViewBag.to = to;
            return View(model);
        }
    }
}