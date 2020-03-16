using System;
using System.Collections.Generic;
using System.Linq;
using LearnBug.Models.DomainModels;
using System.Web;
using System.Web.Mvc;
using LearnBug.ViewModels;
using LearnBug.Models.Repositories;
using System.Web.Security;

namespace LearnBug.Controllers
{
    public class HomeController : Controller
    {
        LearnBug.Models.DomainModels.LearnBugDBEntities1 db = new Models.DomainModels.LearnBugDBEntities1();

        public ActionResult Index(int page = 1)
        {
            var a = db.Contents;
            var model = new IndexViewModel
            {
                Contents = a.OrderByDescending(p => p.Id).Skip((page - 1) * 10).Take(10),
                UsersCount = db.Users.Count(),
                CurrentPage = page,
                TotalItemCount = a.Count()
            };
            if (User.Identity.IsAuthenticated)
                model.User = db.Users.Single(p => p.Username == User.Identity.Name);

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