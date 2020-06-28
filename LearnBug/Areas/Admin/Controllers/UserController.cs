using Models;
using Models.Entities;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LearnBug.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        DatabaseContext db = new DatabaseContext();

        public ActionResult Index(string name = null, string username = null, string role = "نقش", string email = null)
        {
            var model = db.Users.AsQueryable();

            //name = name.Trim();
            //username = username.Trim();
            //email = email.Trim();
            if (!string.IsNullOrEmpty(name))
                model = model.Where(p => p.Name.Contains(name));

            if (!string.IsNullOrEmpty(username))
                model = model.Where(p => p.Username.Contains(username));

            if (!string.IsNullOrEmpty(email))
                model = model.Where(p => p.Email.Contains(email));

            if (role != "نقش")
                model = model.Where(p => p.Roles == role);

            ViewBag.name = name;
            ViewBag.username = username;
            ViewBag.email = email;
            ViewBag.role = role;
            return View(model);
        }

        public ActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public JavaScriptResult changeProfilePicture(string newPicture, string type)
        {
            var user = db.Users.Single(p => p.Username == User.Identity.Name);
            if (type == "update")
            {
                user.Image = newPicture;
                db.SaveChanges();
                return JavaScript("alert('عکس پروفایل شما با موفقیت بروزرسانی شد')");
            }
            else
            {
                user.Image = null;
                db.SaveChanges();
                return JavaScript("alert('عکس پروفایل شما با موفقیت حذف شد')");
            }
        }
        public ActionResult Avatar()
        {
            var user = db.Users.Single(p => p.Username == User.Identity.Name);
            if (!user.IsActive)
                return RedirectToAction(actionName: "Logout", controllerName: "Account");
            return PartialView(user);
        }
        public ActionResult Blocked()
        {
            var model = db.Users.Where(curent => !curent.IsActive);
            return View(model);
        }
        [HttpPost]
        public JsonResult UpdateBlock(int id)
        {
            var user = db.Users.Single(p => p.Id == id);
            user.IsActive = !user.IsActive;
            db.SaveChanges();
            if (user.IsActive)
                return Json(new { str = "Block", script = "toastr.warning(' کاربر "+ user.Name+" از لیست انسداد خارج شد ! ')" });
            return Json(new { str = "UnBlock", script = "toastr.info(' کاربر "+ user.Name+" مسدود شد ! ')" });
        }

        public ActionResult ManagementUser(int id)
        {
            var user = db.Users.Find(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult ManagementUser(User user)
        {
            user.Dateofbirth = user.PersianDateofbirth.ToMiladiDate();
            user.Username = user.Username.ToLower().Trim();
            user.Password = user.Password.Encrypt();
            if (ModelState.IsValid)
            {
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return View(db.Users.Single(p => p.Id == user.Id));
        }
    }
}