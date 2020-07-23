using Models.Entities;
using Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ViewModels;
using Services;

namespace LearnBug.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        public ActionResult Profile(string id)
        {
            try
            {
                var model = _userService.Profile(id);
                return View(model);

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return View();
            }

        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AllUsers(string name = null, string username = null, string role = "نقش", string email = null)
        {
            var model = _userService.AllUsers(name, username, role, email);
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
        [Authorize]
        public JsonResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return Json(new
                {
                    message = "toastr.error('مقادیر ورودی اشتباه است !')",
                    success = false
                });


            if (_userService.ChangePassword(model))
            {

                return Json(new
                {
                    message = "toastr.error('رمز عبور شما اشتباه است')",
                    success = false
                });
            }
            else
            {
                return Json(new
                {
                    message = "toastr.success('رمز عبور شما تغیر کرد !') ",
                    success = true
                });
            }
        }
        public ActionResult _editPassword()
        {
            return PartialView();
        }
        public ActionResult _editPicture()
        {
            ViewBag.pic = _userService.GetCurrentUser().ImgProfile();
            return PartialView();
        }
        public ActionResult _editProfile()
        {
            var user = _userService.GetCurrentUser();
            return PartialView(user);
        }
        [HttpPost]
        public ActionResult _editProfile([Bind(Exclude = "Wallet")] User user)
        {
            _userService.EditProfile(user);
            return View("Edit");
        }
        [HttpPost]
        public JavaScriptResult changeProfilePicture(string newPicture, string type)
        {
            if (_userService.ChangeProfilePicture(newPicture, type))
                return JavaScript("toastr.success('عکس پروفایل شما با موفقیت بروزرسانی شد')");
            return JavaScript("toastr.success('عکس پروفایل شما با موفقیت حذف شد')");
        }
        [AllowAnonymous]
        public ActionResult Avatar()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _userService.Avatar();
                if (user.IsActive)
                    return PartialView(user);
                return RedirectToAction(actionName: "Logout", controllerName: "Account");
            }
            return PartialView(null);
        }
        [AllowAnonymous]
        public ActionResult _UserFollow(int id)
        {
            var user = _userService.GetRowSelectelById(id);
            return PartialView(user);
        }
        [AllowAnonymous]
        [HttpPost]
        public JsonResult AutenticatorUseName(string Username)
        {
            return Json(_userService.AutenticatorUseName(Username));
        }

    }
}