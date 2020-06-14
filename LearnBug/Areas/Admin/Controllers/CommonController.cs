using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace LearnBug.Areas.Admin.Controllers
{
    public class CommonController : Controller
    {
        DatabaseContext db = new DatabaseContext();

        // GET: Admin/Common
        public ActionResult Notification()
        {
            var me = db.Users.FirstOrDefault(p => p.Username == User.Identity.Name);
            var Message = db.Messages.Where(p => p.reciverId == me.Id).OrderByDescending(p => p.InsertDateTime).Take(30).Select(p =>
                     new NotificationViewModel
                     {
                         User = p.sender,
                         DateTime = p.InsertDateTime,
                         Text = " Msg : " + p.Text.Substring(0, 20)
                     });
            var Follow = db.Follows.Where(p => p.followingId == me.Id).OrderByDescending(p => p.InsertDateTime).Take(30).Select(p =>
                 new NotificationViewModel
                 {
                     User = p.Follower,
                     DateTime = p.InsertDateTime,
                     Text = p.Follower.Name + " شما را فالو کرد "
                 });
            var Comment = db.Comments.Where(p => p.Post.userId == me.Id).OrderByDescending(p => p.InsertDateTime).Take(30).Select(p =>
                 new NotificationViewModel
                 {
                     User = p.User,
                     DateTime = p.InsertDateTime,
                     Text = p.User.Name + " برای پست شما کامنت گذاشت "
                 });
            var buy = db.Comments.Where(p => p.Post.userId == me.Id).OrderByDescending(p => p.InsertDateTime).Take(30).Select(p =>
                 new NotificationViewModel
                 {
                     User = p.User,
                     DateTime = p.InsertDateTime,
                     Text = p.User.Name + " مطلب شما را خریداری کرد "
                 });
            var model = Follow.Concat(Message).Concat(Comment).Concat(buy);
            model = model.OrderByDescending(p => p.DateTime).Take(30);
            return PartialView(model);
        }
        public ActionResult Footer()
        {
            var model = db.Settings.Single(p => p.Name == "Footer");
            return PartialView(model);
        }

    }
}