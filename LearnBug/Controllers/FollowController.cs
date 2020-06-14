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
        [HttpPost]
        public string AddOrDeleteFollow(int id)
        {
            var myUser = db.Users.Single(p => p.Username == User.Identity.Name);

            if (myUser.Follower.Any(p => p.followingId == id))
            {
                var follow = myUser.Follower.FirstOrDefault(p => p.followingId == id);
                db.Follows.Remove(follow);
                db.SaveChanges();
                return "follow";
            }
            else
            {
                
                myUser.Follower.Add(new Follow { followingId = id });
                db.SaveChanges();
                return "Unfollow";
            }
        }


        public ActionResult Followers(string id)
        {
            var model = db.Users.Single(p => p.Username == id).Following.Select(p => p.Follower).AsQueryable();
            return View(model);
        }
        public ActionResult Following(string id)
        {
            var model = db.Users.Single(p => p.Username == id).Follower.Select(p => p.Following).AsQueryable();
            return View(model);
        }
    }
}