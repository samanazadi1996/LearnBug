using LearnBug.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnBug.Controllers
{
    public class FollowController : Controller
    {
        LearnBug.Models.DomainModels.LearnBugDBEntities1 db = new Models.DomainModels.LearnBugDBEntities1();

        // GET: Follow
        public void Follow(int id, bool type)
        {
            var myUser = db.Users.Single(p => p.Username == User.Identity.Name);
            if (type)
            {
                if (!myUser.Follows1.Any(p => p.followUserId == id))
                {
                    myUser.Follows1.Add(new Follow { followUserId = id, DateTime = DateTime.Now });
                }
            }
            else
            {
                if (myUser.Follows1.Any(p => p.followUserId == id))
                {
                    var follow = myUser.Follows1.FirstOrDefault(p => p.followUserId == id);
                    db.Follows.Remove(follow);
                }
            }
            db.SaveChanges();

        }


        public ActionResult Followers(string id)
        {
            var model = db.Users.Single(p => p.Username == id).Follows.AsQueryable();
            return View(model);
        }
        public ActionResult Following(string id)
        {
            var model = db.Users.Single(p => p.Username == id).Follows1.AsQueryable();
            return View(model);
        }
    }
}