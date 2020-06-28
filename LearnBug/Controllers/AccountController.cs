using Models;
using Models.Entities;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LearnBug.Controllers
{
    public class AccountController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #region login_logout
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Username, string Password, string Rememberme)
        {
            Password = Password.Encrypt();
            if (db.Users.Any(p => p.Username == Username.ToLower() && p.Password == Password))
            {
                logger.Info("Login User => " + Username.ToLower());
                FormsAuthentication.SetAuthCookie(Username.ToLower(), Convert.ToBoolean(Rememberme));
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.Message = "نام کاربری یا رمز عبور اشتباه است";
                return View();
            }
        }

        #endregion
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (!ModelState.IsValid || db.Users.Any(p => p.Username == user.Username.ToLower().Trim()))
                return View(user);

            user.Status = 1;
            user.Roles = "User";
            user.Wallet = 0;
            user.Username = user.Username.ToLower().Trim();
            user.Password = user.Password.Encrypt();
            user.Dateofbirth = user.PersianDateofbirth.ToMiladiDate();
            db.Users.Add(user);
            if (db.SaveChanges() > 0)
                return RedirectToAction(actionName: "Login", controllerName: "Home");
            return View(user);
        }

    }
}