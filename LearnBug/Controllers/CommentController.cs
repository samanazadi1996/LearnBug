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
        public ActionResult _SeeComments()
        {
            return PartialView();
        }
        [HttpPost]
        [Authorize]
        public ActionResult deleteCmnt(int id)
        {
            var cmnt = db.Comments.Find(id);
            var cntnt = db.Contents.Find(cmnt.contentId);
            if (cmnt.userId == Convert.ToInt32(Session["Log"]) || cntnt.userId == Convert.ToInt32(Session["Log"]))
            {
                db.Comments.Remove(cmnt);
                if (db.SaveChanges() > 0)
                {
                    viewcontentViewModel a = new viewcontentViewModel();
                    var cid = cmnt.contentId;
                    a.Content = db.Contents.Find(cid);
                    a.Users = db.Users.ToList();
                    a.Group = db.Groups.Find(a.Content.groupId);
                    a.Comments = db.Comments.Where(p => p.contentId == cid).OrderByDescending(p => p.Id);
                    return Json(new JsonData()
                    {
                        Html = this.RenderPartialToString("_SeeComments", a),
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
            if (!string.IsNullOrEmpty(Session["Log"].ToString()))
            {
                if (!string.IsNullOrEmpty(text))
                {
                    Comment comment = new Comment();
                    comment.contentId = id;
                    comment.Text = text;
                    comment.userId = Convert.ToInt32(Session["Log"]);
                    comment.Datetime = DateTime.Now.ToPersianDate().ToString();
                    db.Comments.Add(comment);
                    if (db.SaveChanges() > 0)
                    {
                        viewcontentViewModel a = new viewcontentViewModel();
                        a.Content = db.Contents.Find(id);
                        a.Users = db.Users.ToList();
                        a.Group = db.Groups.Find(a.Content.groupId);
                        a.Comments = db.Comments.Where(p => p.contentId == id).OrderByDescending(p => p.Id);

                        return Json(new JsonData()
                        {
                            Html = this.RenderPartialToString("_SeeComments", a),
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
        public ActionResult SetparialviewComment(int id)
        {
            viewcontentViewModel a = new viewcontentViewModel();
            a.Content = db.Contents.Find(id);
            a.Users = db.Users.ToList();
            a.Group = db.Groups.Find(a.Content.groupId);
            a.Comments = db.Comments.Where(p => p.contentId == id).OrderByDescending(p => p.Id);

            return Json(new JsonData()
            {
                Html = this.RenderPartialToString("_SeeComments", a),
            });
        }
    }
}