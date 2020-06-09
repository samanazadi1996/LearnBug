using Models.Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnBug.Controllers
{
    public class CommentController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        public ActionResult _Comments(int Id)
        {
            var post = db.Posts.Find(Id);
            var model = post.Comments;
            ViewBag.PostId = post.Id;
            return PartialView(model);
        }
        [HttpPost]
        [Authorize]
        public string deleteComment(int id)
        {
            var cmnt = db.Comments.Find(id);
            var user = db.Users.Single(p => p.Username.ToLower() == User.Identity.Name.ToLower());
            if (cmnt.userId == user.Id || cmnt.Post.userId == user.Id || User.IsInRole("Admin"))
            {
                db.Comments.Remove(cmnt);
                if (db.SaveChanges() > 0)
                {
                    return "alert('!کامنت شما حذف شد')";
                }
                else
                {
                    return "alert('!کامنت شما حذف نشد')";

                }
            }
            else
            {
                return "alert('!کامنت شما حذف نشد')";
            }
        }

        [Authorize]
        public ActionResult SendComment(int id, string text)
        {
            var post = db.Posts.Find(id);
            Comment comment = new Comment()
            {
                Text = text,
                postId = id
            };
            comment.Text = text;
            var me = db.Users.Single(p => p.Username.ToLower() == User.Identity.Name.ToLower());
            me.Comments.Add(comment);
            db.SaveChanges();
            return PartialView("_Comments", post.Id);


        }
        //public ActionResult SetparialviewComment(int id)
        //{
        //    viewcontentViewModel a = new viewcontentViewModel();
        //    a.Content = db.Contents.Find(id);
        //    a.Users = db.Users.ToList();
        //    a.Group = db.Groups.Find(a.Content.groupId);
        //    a.Comments = db.Comments.Where(p => p.contentId == id).OrderByDescending(p => p.Id);

        //    return Json(new 
        //    {
        //        Html = this.RenderPartialToString("_SeeComments", a),
        //    });
        //}
    }
}