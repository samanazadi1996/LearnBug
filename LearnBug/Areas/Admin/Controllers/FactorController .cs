using Models.Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnBug.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FactorController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        public ActionResult Index( string from=null,string to=null)
        {
            var model = db.Factors.AsQueryable();

            if (!string.IsNullOrEmpty(from))
            {
                DateTime datefrom =from.ToMiladiDate();
                model = model.Where(p => p.InsertDateTime >= datefrom);
            }

            if (!string.IsNullOrEmpty(to))
            {
                DateTime dateto = to.ToMiladiDate();
                model = model.Where(p => p.InsertDateTime <= dateto);
            }
            ViewBag.from = from;
            ViewBag.to = to;

            return View(model.OrderByDescending(p=>p.InsertDateTime));
        }
    }
}