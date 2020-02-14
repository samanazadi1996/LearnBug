using LearnBug.Models.DomainModels;
using LearnBug.Models.Repositories;
using LearnBug.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnBug.Controllers
{
    public class UserController : Controller
    {
        LearnBug.Models.DomainModels.LearnBugDBEntities1 db = new Models.DomainModels.LearnBugDBEntities1();

        [HttpGet]
        public ActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(User user)
        {
            user.Status = 1;
            user.Roles = "user";
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                if (db.SaveChanges()>0)
                {
                    ViewBag.message = "ثبت نام انجام شد";
                    ViewBag.style = "green";
                    return View();
                }
                else
                {
                    ViewBag.message = "ثبت نام انجام نشد";
                    ViewBag.style = "red";
                    return View();
                }

            }
            else
            {
                ViewBag.message = "مقادیر ورودی صحیح نمی باشد";
                ViewBag.style = "blue";
                return View();

            }
        }
        public ActionResult Profile(string username)
        {
            var u = db.Users.FirstOrDefault(p => p.Username.Trim().ToLower() == username.Trim().ToLower());
            var model = new IndexViewModel
            {
                Comments = db.Comments.ToList(),
                User = u,
                Groups = db.Groups.ToList(),
                Contents = db.Contents.Where(p => p.userId == u.Id).ToList()
            };
            return View(model);
        }
        [Authorize]
        public ActionResult SendMessage(string text, int to)
        {
            var message = new Message
            {
                Datetime = DateTime.Now.ToPersianDate().ToString(),
                FromuserId = Convert.ToInt32(Session["Log"]),
                Text = text,
                TouserId = to,
                Status = 0

            };
            if (ModelState.IsValid)
            {
                db.Messages.Add(message);
                if (db.SaveChanges() > 0)
                {
                    return JavaScript("alert('پیغام ارسال شد')");
                }
                else
                {
                    return JavaScript("alert('پیغام ارسال نشد')");
                }
            }
            else
            {
                return JavaScript("alert('مقادیر نامعتبر')");

            }
        }



    }
}