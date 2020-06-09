using Models.Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnBug.Controllers
{
    public class FollowController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: Follow
        public void Follow(int id, bool type)
        {
            var myUser = db.Users.Single(p => p.Username == User.Identity.Name);
            if (type)
            {
                if (!myUser.Follower.Any(p => p.followerId == id))
                {
                    myUser.Follower.Add(new Follow { followerId = id});
                }
            }
            else
            {
                if (myUser.Follower.Any(p => p.followerId == id))
                {
                    var follow = myUser.Follower.FirstOrDefault(p => p.followerId == id);
                    db.Follows.Remove(follow);
                }
            }
            db.SaveChanges();

        }


        public ActionResult Followers(string id)
        {
            var model = db.Users.Single(p => p.Username == id).Following.Select(p=>p.Follower).AsQueryable();
            return View(model);
        }
        public ActionResult Following(string id)
        {
            var model = db.Users.Single(p => p.Username == id).Follower.Select(p=>p.Following).AsQueryable();
            return View(model);
        }
    }
}