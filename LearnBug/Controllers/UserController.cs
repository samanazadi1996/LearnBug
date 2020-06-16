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

namespace LearnBug.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        DatabaseContext db = new DatabaseContext();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(User user)
        {
            if (!ModelState.IsValid || db.Users.Any(p => p.Username == user.Username.ToLower().Trim()))
                return View(user);

            user.Status = 1;
            user.Roles = "User";
            user.Wallet = 0;
            user.Username = user.Username.ToLower().Trim();
            user.Password = user.Password.Encrypt();
            user.Dateofbirth = user.Dateofbirth.ToMiladiDate();
            db.Users.Add(user);
            if (db.SaveChanges() > 0)
                return RedirectToAction(actionName: "Login", controllerName: "Home");
            return View(user);

        }
        [AllowAnonymous]
        public ActionResult Profile(string id)
        {
            try
            {
                var user = db.Users.FirstOrDefault(p => p.Username.Trim() == id.Trim());
                var mutualFollower = user.Following.Select(p => p.Follower)
                    .Where(p => p.Following.Any(o => o.Follower.Username == User.Identity.Name));
                    
                    //.Where(p => p.Follower.Following.Any(o => o.Follower.Username == User.Identity.Name)).Take(5).Select(p => p.Following);
                ProfileViewModel model = new ProfileViewModel()
                {
                    User = user,
                    mutualFollower =mutualFollower,
                    Posts = user.Posts
                };

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
        [Authorize]
        public JsonResult ChangePassword(ChangePasswordViewModel model)
        {
            model.OldPassword = model.OldPassword.Encrypt();
            var myuser = db.Users.FirstOrDefault(p => p.Username == User.Identity.Name && p.Password == model.OldPassword);
            if (!ModelState.IsValid)
                return Json(new
                {
                    message = "toastr.error('مقادیر ورودی اشتباه است !')",
                    success = false
                });


            if (myuser == null)
            {

                return Json(new
                {
                    message = "toastr.error('رمز عبور شما اشتباه است')",
                    success = false
                });
            }
            else
            {
                myuser.Password = model.NewPassword.Encrypt();
                db.SaveChanges();
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
            ViewBag.pic = db.Users.Single(p => p.Username == User.Identity.Name).ImgProfile();
            return PartialView();
        }
        public ActionResult _editProfile()
        {
            var user = db.Users.Single(p => p.Username == User.Identity.Name);
            return PartialView(user);
        }
        [HttpPost]
        public ActionResult _editProfile([Bind(Exclude = "Wallet")] User user)
        {
            var myuser = db.Users.Single(p => p.Username == User.Identity.Name);
            //myuser.Username = user.Username.ToLower();
            myuser.Name = user.Name;
            myuser.Email = user.Email;
            myuser.Dateofbirth = user.Dateofbirth.ToMiladiDate();
            myuser.Gender = user.Gender;
            myuser.Biography = user.Biography;
            myuser.Phone = user.Phone;
            myuser.Location = user.Location;

            db.SaveChanges();
            return View("Edit");
        }
        [HttpPost]
        public JavaScriptResult changeProfilePicture(string newPicture, string type)
        {
            var user = db.Users.Single(p => p.Username == User.Identity.Name);
            if (type == "update")
            {
                var filename = "/Files/ProfilePicture/" + user.Username + ".jpg";
                Utility.ConvertBase64toFile.Convert_base64_url_Image(newPicture, filename);
                user.Image = filename;
                db.SaveChanges();
                return JavaScript("toastr.success('عکس پروفایل شما با موفقیت بروزرسانی شد')");
            }
            else
            {
                user.Image = null;
                db.SaveChanges();
                return JavaScript("toastr.success('عکس پروفایل شما با موفقیت حذف شد')");
            }
        }
        [AllowAnonymous]
        public ActionResult Avatar()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.Single(p => p.Username == User.Identity.Name);
                return PartialView(user);
            }
            return PartialView(null);
        }
        [AllowAnonymous]
        public ActionResult _UserFollow(int id)
        {
            var user = db.Users.Find(id);
            return PartialView(user);
        }
        [AllowAnonymous]
        [HttpPost]
        public JsonResult AutenticatorUseName(string Username)
        {
            Username = Username.ToLower();

            if (Username.Length < 5 || Username.Length > 30)
                return Json(false);

            if (db.Users.Any(p => p.Username == Username))
                return Json(false);

            var list = Username.ToCharArray();
            var firstChar = (int)list[0];

            if (firstChar <= 57 && firstChar >= 48)
                return Json(false);

            foreach (var item in list)
            {
                var asc = (int)item;

                if ((asc < 97 || asc > 122) && (asc < 48 || asc > 57) && item != '_' && item != '.' && item != '-' && item != '@')
                    return Json(false);
            }

            return Json(true);
        }
    }
}