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
    [Authorize]
    public class BookmarkController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        [Authorize]
        // GET: Bookmark
        public JavaScriptResult CreateOrDeleteBookmark(int postId)
        {
            var myUser = db.Users.Single(p => p.Username == User.Identity.Name);
            if (!myUser.Bookmarks.Any(p => p.postId == postId))
            {
                myUser.Bookmarks.Add(new Bookmark { postId = postId });
                db.SaveChanges();
                return JavaScript("toastr.success('مطلب نشانه گذاری شد .')");

            }
            else
            {
                var bookmark = myUser.Bookmarks.FirstOrDefault(p => p.postId == postId);
                db.Bookmarks.Remove(bookmark);
                db.SaveChanges();
                return JavaScript("toastr.success('مطلب از لیست نشانه گذاری ها حذف شد .')");
            }
        }

        public ActionResult Index( int Page = 1)
        {
            var Posts = db.Users.Single(p => p.Username == User.Identity.Name).Bookmarks.Select(o => o.Post).OrderByDescending(i => i.InsertDateTime);
            PostsViewModel model = new PostsViewModel
            {
                postId = Posts.Skip((Page - 1) * 12).Take(12).Select(p => p.Id).AsQueryable(),
                CurrentPage = Page,
                TotalItemCount = Posts.Count()
            };
            return View(model);
        }

    }
}