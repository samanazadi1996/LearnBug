using Models.Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace LearnBug.Controllers
{
    public class PostController : Controller
    {
        DatabaseContext db = new DatabaseContext();

        public ActionResult _SinglePost(int id)
        {
            var post = db.Posts.Find(id);
            return PartialView(post);
        }

        public ActionResult ViewPost(int id)
        {
            var Content = db.Posts.Find(id);
            return View(Content);
        }

        [Authorize]
        public ActionResult Index(int Page = 1)
        {
            var posts = db.Posts.Where(p => p.User.Username == User.Identity.Name).OrderByDescending(o => o.InsertDateTime);

            PostsViewModel model = new PostsViewModel
            {
                PostId = posts.Skip((Page - 1) * 12).Take(12).Select(p => p.Id),
                CurrentPage = Page,
                TotalItemCount = posts.Count()
            };
            return View(model);
        }
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Groups = new SelectList(db.Groups.ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Create(Post post)
        {
            string path = Server.MapPath("~/Files/Posts/");

            var result = Utility.ConvertBase64toFile.Convert_Htmlbase64_url_Image(post.Content, path, "/Files/Posts/");

            var user = db.Users.Single(p => p.Username == User.Identity.Name);
            post.Status = 0;
            if (User.IsInRole("User"))
                post.Price = null;
            post.Content = result.ToString();
            user.Posts.Add(post);
            if (db.SaveChanges() > 0)
            {
                return RedirectToAction("ViewPost", "Post", new { id = user.Posts.LastOrDefault().Id });

            }
            else
            {
                ViewBag.Groups = new SelectList(db.Groups.ToList(), "Id", "Name");
                ViewBag.message = "ثبت مطلب انجام نشد";
                ViewBag.style = "red";
                return View(post);
            }


        }

        //[Authorize]

        //public ActionResult ContentOfFollowing(int Page = 1)
        //{
        //    var user = db.Users.Single(p => p.Username == User.Identity.Name);
        //    var contents = user.Following.SelectMany(p => p.Following.Posts).OrderByDescending(O => O.Datetime).AsQueryable();
        //    ContentViewModel model = new ContentViewModel
        //    {
        //        posts = contents.Skip((Page - 1) * 12).Take(12),
        //        CurrentPage = Page,
        //        TotalItemCount = contents.Count()
        //    };


        //    return View(model);
        //}

        [Authorize]
        public ActionResult Edit(int id)
        {
            var model = db.Posts.Find(id);

            if (User.IsInRole("Admin") || model.User.Username == User.Identity.Name)
            {
                ViewBag.Groups = new SelectList(db.Groups.ToList(), "Id", "Name");
                return View(model);
            }
            return View("AddContent");

        }
        [HttpPost]
        [Authorize]
        public ActionResult Edit(Post post)
        {
            string path = Server.MapPath("~/Files/Posts/");

            var result = Utility.ConvertBase64toFile.Convert_Htmlbase64_url_Image(post.Content, path, "/Files/Posts/");

            var cntnt = db.Posts.Find(post.Id);
            if (cntnt.User.Username == User.Identity.Name || User.IsInRole("Admin"))
            {
                if (!User.IsInRole("User"))
                    cntnt.Price = post.Price;

                cntnt.groupId = post.groupId;
                cntnt.Subject = post.Subject;
                cntnt.Content = result;

                if (db.SaveChanges() > 0)
                {
                    return RedirectToAction("ViewPost", "Post", new { id = post.Id });
                }
            }
            ViewBag.Groups = new SelectList(db.Groups.ToList(), "Id", "Name");
            ViewBag.message = "ثبت مطلب انجام نشد";
            ViewBag.style = "red";
            return View(post);

        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            var model = db.Posts.Find(id);
            if (User.IsInRole("Admin") || model.User.Username == User.Identity.Name)
                return View(model);
            return HttpNotFound();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Delete(Post post)
        {
            var model = db.Posts.Find(post.Id);

            if (User.IsInRole("Admin") || model.User.Username == User.Identity.Name && !model.Factors.Any())
            {
                foreach (var item in model.Comments) { model.Comments.Remove(item); }
                foreach (var item in model.Bookmarks) { model.Bookmarks.Remove(item); }
                db.Posts.Remove(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return HttpNotFound();

        }

    }
}