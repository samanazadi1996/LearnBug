using Models.Entities;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnBug.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SettingController : Controller
    {
        DatabaseContext db = new DatabaseContext();

        readonly private ISettingService _settingServices;
        public SettingController(ISettingService settingServices)
        {
            _settingServices = settingServices;
        }
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
            _settingServices.Edit(setting);
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
            _settingServices.UploadLogo(File);
            return View();
        }
        [HttpPost]
        public JavaScriptResult SetLogo(string name)
        {
            if (_settingServices.ChangeLogo(name))
                return JavaScript("toastr.success('لوگو سایت با موفقیت تغیر کرد')");
            return JavaScript("toastr.error('خطایی رخ داده است')");
        }
        [HttpPost]
        public JavaScriptResult DeleteLogo(string name)
        {
            if (_settingServices.DeleteLogo(name))
                return JavaScript("toastr.success('لوگو با موفقیت حذف شد !')");
            return JavaScript("toastr.error('لوگو سایت فعال است و نمیتوان آن را حذف کرد!')");
        }
    }
}