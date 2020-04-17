using LearnBug.Models.DomainModels;
using Services.SettingServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnBug.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SettingController : Controller
    {
        Models.DomainModels.LearnBugDBEntities1 db = new Models.DomainModels.LearnBugDBEntities1();
        SettingServices settingServices = new SettingServices();
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
        public ActionResult UploadLogo(HttpPostedFileBase File)
        {
            settingServices.UploadLogo(File);
            return View();
        }
        [HttpPost]
        public JavaScriptResult SetLogo(string name)
        {
            if (settingServices.ChangeLogo(name))
                return JavaScript("alert('لوگو سایت با موفقیت تغیر کرد')");
            return JavaScript("alert('خطایی رخ داده است')");
        }
        [HttpPost]
        public JavaScriptResult DeleteLogo(string name)
        {
            if (settingServices.DeleteLogo(name))
                return JavaScript("alert('لوگو با موفقیت حذف شد !')");

            return JavaScript("alert('لوگو سایت فعال است و نمیتوان آن را حذف کرد!')");

        }
    }
}