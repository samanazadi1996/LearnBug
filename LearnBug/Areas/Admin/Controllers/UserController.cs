using Models;
using Models.Entities;
using NLog;
using PagedList;
using Services;
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
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index(int? page,string name = null, string username = null, string role = "نقش", string email = null)
        {

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var result = _userService.AllUsers(name, username, role, email);
            var model = result.ToPagedList(pageNumber, 5);
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
            if (_userService.ChangeProfilePicture(newPicture,type))
                return JavaScript("alert('عکس پروفایل شما با موفقیت بروزرسانی شد')");
            return JavaScript("alert('عکس پروفایل شما با موفقیت حذف شد')");
        }
        public ActionResult Avatar()
        {
            var user = _userService.Avatar();
            if (!user.IsActive)
                return RedirectToAction(actionName: "Logout", controllerName: "Account");
            return PartialView(user);
        }
        public ActionResult Blocked()
        {
            var model = _userService.GetBlockedUser();
            return View(model);
        }
        [HttpPost]
        public JsonResult UpdateBlock(int id)
        {
            var user = _userService.UpdateBlockUser(id);
            if (user.IsActive)
                return Json(new { str = "Block", script = "toastr.warning(' کاربر " + user.Name + " از لیست انسداد خارج شد ! ')" });
            return Json(new { str = "UnBlock", script = "toastr.info(' کاربر " + user.Name + " مسدود شد ! ')" });
        }

        public ActionResult ManagementUser(int id)
        {
            var user = _userService.GetRowSelectelById(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult ManagementUser(User user)
        {
            var result = _userService.ChangeUserByAdmin(user);
            return View(result);
        }
    }
}