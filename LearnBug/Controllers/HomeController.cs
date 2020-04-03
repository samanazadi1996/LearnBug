using System;
using System.Collections.Generic;
using System.Linq;
using LearnBug.Models.DomainModels;
using System.Web;
using System.Web.Mvc;
using LearnBug.ViewModels;
using System.Web.Security;

namespace LearnBug.Controllers
{
    public class HomeController : Controller
    {
        LearnBug.Models.DomainModels.LearnBugDBEntities1 db = new Models.DomainModels.LearnBugDBEntities1();
        [Authorize(Roles = "Admin")]
        public ActionResult AdminPanel()
        {
            return View();
        }
        public ActionResult Index(string search = null, int Page = 1)
        {
            var contents = db.Contents.OrderByDescending(p => p.Datetime).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                contents = contents.Where(p => p.Subject.Contains(search) || p.Group.Name.Contains(search) || p.User.name.Contains(search) || p.User.Username.Contains(search));
                ViewBag.Srch = search.Trim();
            }
            ContentViewModel model = new ContentViewModel
            {
                Contents = contents.Skip((Page - 1) * 12).Take(12),
                CurrentPage = Page,
                TotalItemCount = contents.Count()
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Username, string Password, string Rememberme)
        {

            if (db.Users.Where(p => p.Username == Username.ToLower() && p.Password == Password).Any())
            {
                FormsAuthentication.SetAuthCookie(Username.ToLower(), Convert.ToBoolean(Rememberme));
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "نام کاربری یا رمز عبور اشتباه است";
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}