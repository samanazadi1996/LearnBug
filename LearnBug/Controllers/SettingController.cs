using LearnBug.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnBug.Controllers
{
    public class SettingController : Controller
    {
        Models.DomainModels.LearnBugDBEntities1 db = new Models.DomainModels.LearnBugDBEntities1();
        public ActionResult Index()
        {
            var model = db.Settings.AsQueryable();
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var model = db.Settings.Find(id);
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Setting setting)
        {
            var model = db.Settings.Find(setting.Id);
            model.Value = setting.Value;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}