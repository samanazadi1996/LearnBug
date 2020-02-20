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
            int u = Convert.ToInt32(Session["Log"]);
            var a = db.Contents.ToList();
            var model = new IndexViewModel
            {
                User = db.Users.Find(u),
                Users = db.Users.ToList(),
                Contents = a.OrderByDescending(p => p.Id).Skip((page - 1) * 10).Take(10).ToList(),
                Groups = db.Groups.ToList(),
                CurrentPage = page,
                TotalItemCount = a.Count(),
                MyContent = a.Count(p => p.userId == u)
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

            if (db.Users.Where(p => p.Username == Username && p.Password == Password).Any())
            {
                FormsAuthentication.SetAuthCookie(Username,Convert.ToBoolean(Rememberme));
                Session["Log"] = db.Users.FirstOrDefault(p=>p.Username==Username && p.Password==Password).Id;

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "نام کاربری یا پسورد اشتباه است";  
                return View();
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["log"] = null;
            return RedirectToAction("Index");
        }


    }
}