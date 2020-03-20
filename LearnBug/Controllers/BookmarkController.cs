using LearnBug.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnBug.Controllers
{
    [Authorize]

    public class BookmarkController : Controller
    {
        LearnBug.Models.DomainModels.LearnBugDBEntities1 db = new Models.DomainModels.LearnBugDBEntities1();
        [Authorize]
        // GET: Bookmark
        public void AddBookmark(int ContentId, bool type)
        {
                var myUser = db.Users.Single(p => p.Username == User.Identity.Name);
                if (type)
                {
                    if (!myUser.Bookmarks.Any(p => p.contentId == ContentId))
                    {
                        myUser.Bookmarks.Add(new Bookmark { contentId = ContentId, Datetime = DateTime.Now });
                    }
                }
                else
                {
                    if (myUser.Bookmarks.Any(p => p.contentId == ContentId))
                    {
                        var bookmark = myUser.Bookmarks.FirstOrDefault(p => p.contentId == ContentId);
                        db.Bookmarks.Remove(bookmark) ;
                    }
                }
                db.SaveChanges();
        }
        public ActionResult MyBookMarks()
        {
            var contents = db.Users.Single(p => p.Username == User.Identity.Name).Bookmarks.Select(o => o.Content).OrderByDescending(i=>i.Datetime);
            return View(contents);
        }
    }
}