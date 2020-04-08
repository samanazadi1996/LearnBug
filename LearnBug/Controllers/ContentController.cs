using LearnBug.Models.DomainModels;
using LearnBug.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnBug.Controllers
{
    public class ContentController : Controller
    {
        LearnBugDBEntities1 db = new LearnBugDBEntities1();

        [Authorize]
        public ActionResult myContents()
        {
            var contents = db.Contents.Where(p => p.User.Username == User.Identity.Name).OrderByDescending(o => o.Datetime);
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

        public JavaScriptResult DeleteContent(int id)
        {

            var cntnt = db.Contents.Find(id);
            if (cntnt.User.Username == User.Identity.Name || User.IsInRole("Admin"))
            {
                foreach (var item in cntnt.Comments) { db.Comments.Remove(item); }
                foreach (var item in cntnt.Bookmarks) { db.Bookmarks.Remove(item); }
                db.Contents.Remove(cntnt);
                db.SaveChanges();
                return JavaScript("alert('!مطلب شما حذف شد')");
            }
            else
            {
                return JavaScript("alert('!مطلب شما حذف نشد')");
            }
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
            var user = db.Users.Single(p => p.Username == User.Identity.Name);
            content.Datetime = DateTime.Now;
            content.Status = 0;
            if (User.IsInRole("User"))
                content.Price = null;

            user.Contents.Add(content);
            if (db.SaveChanges() > 0)
            {
                return RedirectToAction("ViewContent", "Content", new { id = user.Contents.LastOrDefault().Id });

            }
            else
            {
                ViewBag.Groups = new SelectList(db.Groups.ToList(), "Id", "Name");
                ViewBag.message = "ثبت مطلب انجام نشد";
                ViewBag.style = "red";
                return View(content);
            }


        }
        [Authorize]

        public ActionResult ContentOfFollowing(int Page = 1)
        {
            var user = db.Users.Single(p => p.Username == User.Identity.Name);
            var contents = user.Follows1.SelectMany(p => p.User.Contents).OrderByDescending(O => O.Datetime).AsQueryable();
            ContentViewModel model = new ContentViewModel
            {
                Contents = contents.Skip((Page - 1) * 12).Take(12),
                CurrentPage = Page,
                TotalItemCount = contents.Count()
            };


            return View(model);
        }

        [Authorize]
        public ActionResult EditContent(int id)
        {
            var model = db.Contents.Find(id);

            if (User.IsInRole("Admin") || model.User.Username == User.Identity.Name)
            {
                ViewBag.Groups = new SelectList(db.Groups.ToList(), "Id", "Name");
                return View(model);
            }
            return View("AddContent");

        }
        [HttpPost]
        [Authorize]
        public ActionResult EditContent(Content content)
        {
            var cntnt = db.Contents.Find(content.Id);
            if (cntnt.User.Username == User.Identity.Name || User.IsInRole("Admin"))
            {
                if (!User.IsInRole("User"))
                    cntnt.Price = content.Price;

                cntnt.groupId = content.groupId;
                cntnt.Subject = content.Subject;
                cntnt.Description = content.Description;
            }

            if (db.SaveChanges() > 0)
            {
                return RedirectToAction("ViewContent", "Content", new { id = content.Id });
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