using LearnBug.Models.DomainModels;
using LearnBug.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Utilty;

namespace LearnBug.Controllers
{
    public class CommentController : Controller
    {
        LearnBug.Models.DomainModels.LearnBugDBEntities1 db = new Models.DomainModels.LearnBugDBEntities1();
        public ActionResult _SeeComments(int Id)
        {
            var model = db.Contents.Find(Id);
            return PartialView(model);
        }
        [HttpPost]
        [Authorize]
        public ActionResult deleteComment(int id)
        {
            var cmnt = db.Comments.Find(id);
            var user = db.Users.Single(p => p.Username.ToLower() == User.Identity.Name.ToLower());
            if (cmnt.userId==user.Id || cmnt.Content.userId==user.Id || User.IsInRole("Admin"))
            {
                db.Comments.Remove(cmnt);
                if (db.SaveChanges() > 0)
                {
                    return Json(new JsonData()
                    {
                        Html = this.RenderPartialToString("_SeeComments", cmnt.Content),
                        Success = true,
                        Script = "alert('!کامنت شما حذف شد')"

                    });
                }
                else
                {
                    return Json(new JsonData()
                    {
                        Html = "",
                        Success = false,
                        Script = "alert('!کامنت شما حذف نشد')"
                    });

                }
            }
            else
            {
                return Json(new JsonData()
                {
                    Html = "",
                    Success = false,
                    Script = "alert('!کامنت شما حذف نشد')"
                });
            }






        }

        [HttpPost]
        [Authorize]
        public ActionResult SendComment(int id, string text)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(text))
                {
                    Comment comment = new Comment();
                    comment.Text = text;
                    comment.userId =db.Users.Single(p=>p.Username.ToLower()==User.Identity.Name.ToLower()).Id;
                    comment.Datetime = DateTime.Now;
                    db.Contents.Find(id).Comments.Add(comment);
                    if (db.SaveChanges() > 0)
                    {
                        var model = db.Contents.Find(id);
                        return Json(new JsonData()
                        {
                            Html = this.RenderPartialToString("_SeeComments", model),
                            Success = true,
                            Script = "alert('!کامنت شما ثبت شد')"
                        });
                    }
                    else
                    {
                        return Json(new JsonData()
                        {
                            Html = "",
                            Success = false,
                            Script = "alert('!کامنت شما ثبت نشد')"
                        });

                    }
                }
                else
                {
                    return Json(new JsonData()
                    {
                        Html = "",
                        Success = false,
                        Script = "alert('نظر خود را بنویسید')"
                    });
                }
            }
            else
            {
                return Json(new JsonData()
                {
                    Html = "",
                    Success = false,
                    Script = "alert('لطفا در سایت ما ثبت نام کنید!')"
                });
            }
        }
        //public ActionResult SetparialviewComment(int id)
        //{
        //    viewcontentViewModel a = new viewcontentViewModel();
        //    a.Content = db.Contents.Find(id);
        //    a.Users = db.Users.ToList();
        //    a.Group = db.Groups.Find(a.Content.groupId);
        //    a.Comments = db.Comments.Where(p => p.contentId == id).OrderByDescending(p => p.Id);

        //    return Json(new JsonData()
        //    {
        //        Html = this.RenderPartialToString("_SeeComments", a),
        //    });
        //}
    }
}