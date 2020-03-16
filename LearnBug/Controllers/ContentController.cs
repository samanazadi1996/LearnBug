using LearnBug.Models.DomainModels;
using LearnBug.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Utilty;

namespace LearnBug.Controllers
{
    public class ContentController : Controller
    {
        LearnBug.Models.DomainModels.LearnBugDBEntities1 db = new Models.DomainModels.LearnBugDBEntities1();

        [Authorize]
        public ActionResult myContents()
        {
            var contents = db.Contents.Where(p => p.User.Username == User.Identity.Name).OrderByDescending(o=>o.Datetime);
            return View(contents);
        }
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult ViewContent(int id)
        {
            var Content = db.Contents.Find(id);
            return View(Content);
        }


        [HttpPost]
        [Authorize]

        public ActionResult DeleteContent(int id)
        {
            var cntnt = db.Contents.Find(id);
            foreach (var item in cntnt.Comments)
            {
                db.Comments.Remove(item);
            }
            db.Contents.Remove(cntnt);
            db.SaveChanges();
            return Json(new JsonData()
            {
                Html = "",
                Success = true,
                Script = "alert('!مطلب شما حذف شد')"

            });

        }
        [HttpGet]
        [Authorize]

        public ActionResult AddContent()
        {
            ViewBag.Groups = new SelectList(db.Groups.ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [Authorize]

        public ActionResult AddContent(Content content)
        {
            content.Datetime = DateTime.Now.ToPersianDate().ToString();
            content.Status = 0;
            db.Users.Single(p => p.Username == User.Identity.Name).Contents.Add(content);
            if (db.SaveChanges() > 0)
            {
                return RedirectToAction("myContents");
            }
            else
            {
                ViewBag.Groups = new SelectList(db.Groups.ToList(), "Id", "Name");
                ViewBag.message = "ثبت مطلب انجام نشد";
                ViewBag.style = "red";
                return View(content);
            }


        }










    }
}