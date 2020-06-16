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
                    return "toastr.success(''!کامنت شما حذف شد')";
                }
                else
                {
                    return "toastr.error('!کامنت شما حذف نشد')";

                }
            }
            else
            {
                return "toastr.error('!کامنت شما حذف نشد')";
            }
        }

        [Authorize]
        public JsonResult SendComment(int id, string text)
        {
            try
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
                if (db.SaveChanges() > 0)
                {
                    var model = post.Comments;
                    ViewBag.PostId = post.Id;

                    return Json(new {success=true, html = this.RenderPartialToString("_Comments", model), message ="toastr.success('کامنت شما ثبت شد')" });
                }
                else
                {
                    return Json(new { success = false,html="", message = "toastr.error('کامنت شما ثبت نشد')" });
                }
            }
            catch (Exception)
            {
                return Json(new { success = false,html="", message = "toastr.error('خطایی رخ داد')" });
            }
        }
    }
}