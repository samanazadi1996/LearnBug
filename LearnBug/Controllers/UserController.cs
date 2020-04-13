using LearnBug.Models.DomainModels;
using LearnBug.ViewModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LearnBug.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private LearnBugDBEntities1 db = new LearnBugDBEntities1();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult RegisterUser(User user)
        {
            if (db.Users.Any(p => p.Username == user.Username.ToLower().Trim()))
            {
                ViewBag.message = "کاربر با این نام از قبل وجود دارد";
                ViewBag.style = "red";
                return View(user);
            }
            user.Status = 1;
            user.Roles = "User";
            user.Wallet = 0;
            user.Username = user.Username.ToLower().Trim();
            user.Password = user.Password.Encrypt();
            user.Dateofbirth = user.Dateofbirth.ToMiladiDate();
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                if (db.SaveChanges() > 0)
                {
                    logger.Info("RegisterUser " + user.Username);
                    return RedirectToAction(actionName: "Login", controllerName: "Home");
                }
                else
                {
                    ViewBag.message = "ثبت نام انجام نشد";
                    ViewBag.style = "red";
                    return View(user);
                }

            }
            else
            {
                ViewBag.message = "مقادیر ورودی صحیح نمی باشد";
                ViewBag.style = "blue";
                return View(user);

            }
        }
        [AllowAnonymous]
        public ActionResult Profile(string username)
        {
            try
            {
                var user = db.Users.FirstOrDefault(p => p.Username.Trim() == username.Trim());
                return View(user);

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return View();
            }

        }
        [Authorize(Roles = "Admin")][HttpGet]
        public ActionResult AllUsers(string name=null, string username=null, string role="نقش", string email=null)
        {
            var model = db.Users.AsQueryable();

            //name = name.Trim();
            //username = username.Trim();
            //email = email.Trim();
            if (!string.IsNullOrEmpty(name))
                model = model.Where(p => p.name.Contains(name));

            if (!string.IsNullOrEmpty(username))
                model = model.Where(p => p.Username.Contains(username));

            if (!string.IsNullOrEmpty(email))
                model = model.Where(p => p.Email.Contains(email));

            if (role!="نقش")
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
        public ActionResult ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            var myuser = db.Users.FirstOrDefault(p => p.Username == User.Identity.Name && p.Password == oldPassword.Encrypt());
            if (myuser == null)
            {

                return Json(new
                {
                    js = "document.getElementById('oldPassword').focus();document.querySelector('#oldPassword').value=''",
                    msg = "رمز عبور شما اشتباه است",
                    ss = false
                });
            }
            else
            {
                if (confirmPassword == newPassword)
                {
                    myuser.Password = newPassword.Encrypt();
                    db.SaveChanges();
                    return Json(new
                    {
                        ss = true
                    });
                }
                else
                {
                    return Json(new
                    {
                        js = "document.getElementById('confirmPassword').focus();document.querySelector('#confirmPassword').value=''",
                        msg = "تکرار رمز عبور اشتباه است",
                        ss = false
                    });
                }
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
            myuser.name = user.name;
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
            return PartialView(user);
        }





        public ActionResult ManagementUser(int id)
        {
            var user = db.Users.Find(id);
            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult ManagementUser(User user)
        {
            user.Dateofbirth = user.Dateofbirth.ToMiladiDate();
            user.Username = user.Username.ToLower().Trim();
            user.Password = user.Password.Encrypt();
            if (ModelState.IsValid)
            {
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return View(db.Users.Single(p=>p.Id==user.Id));
        }
    }
}