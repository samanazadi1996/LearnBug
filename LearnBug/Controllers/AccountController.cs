using Common.ReCaptcha;
using Models;
using Models.Entities;
using NLog;
using Services;
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
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [Authorize]
        public ActionResult Logout()
        {
            _accountService.Logout();
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
            var result = _accountService.Login(Username, Password, Rememberme);

            if (result)
                return RedirectToAction("Index", "Home");

            ViewBag.Message = "نام کاربری یا رمز عبور اشتباه است";
            return View();
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
        //[ReCaptcha]
        public ActionResult Register(User user)
        {
            if (!ModelState.IsValid)
                return View(user);

            if (_accountService.Register(user))
                return RedirectToAction(actionName: "Login", controllerName: "Account");
            return View(user);
        }

    }
}