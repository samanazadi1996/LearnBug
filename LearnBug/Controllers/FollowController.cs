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
        public void Follow(int userId,bool type)
        {
            var myUser = db.Users.Single(p => p.Username == User.Identity.Name);
            if (type)
            {
                if (!myUser.Follows1.Any(p => p.followUserId == userId))
                {
                    myUser.Follows1.Add(new Follow { followUserId = userId, DateTime = DateTime.Now });
                }
            }
            else
            {
                if (myUser.Follows1.Any(p => p.followUserId == userId))
                {
                    var follow = myUser.Follows1.FirstOrDefault(p => p.followUserId == userId);
                    db.Follows.Remove(follow);
                }
            }
            db.SaveChanges();

        }
    }
}