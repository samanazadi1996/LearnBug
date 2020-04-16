using LearnBug.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnBug.Controllers
{
    [Authorize(Roles = "Admin")]
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
        [HttpGet]
        public ActionResult UploadLogo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadLogo(HttpPostedFileBase UploadImage)
        {
            string path = Server.MapPath("~") + "Files\\Picture\\Logo\\" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".png";

            UploadImage.SaveAs(path);
            return View();
        }

        [HttpPost]
        public JavaScriptResult SetLogo(string pathfile)
        {
            return JavaScript("alert("+pathfile+")");
        }
    }
}