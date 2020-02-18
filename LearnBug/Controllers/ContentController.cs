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
    public class ContentController : Controller
    {
        LearnBug.Models.DomainModels.LearnBugDBEntities1 db = new Models.DomainModels.LearnBugDBEntities1();

        [Authorize]
        public ActionResult myContents()
        {
            int q = Convert.ToInt32(Session["Log"]);
            var model = new myContentsViewModel
            {
                contents = db.Contents.Where(p => p.userId == q).OrderByDescending(p => p.Id).ToList(),
                comments = db.Comments.OrderBy(p => p.Id).ToList(),
                groups = db.Groups.OrderBy(p => p.Id).ToList()
        };

            return View(model);
        }
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult ViewContent(int id)
        {
            viewcontentViewModel a = new viewcontentViewModel();
            a.Content = db.Contents.Find(id);
            a.Users = db.Users.ToList();
            a.Group = db.Groups.Find(a.Content.groupId);
            a.Comments = db.Comments.Where(p => p.contentId == id).OrderByDescending(p => p.Id);
            return View(a);
        }


        [HttpPost]
        [Authorize]

        public ActionResult DeleteContent(int id)
        {
            var cntnt = db.Contents.Find(id);
            var cmnt = db.Comments.Where(p => p.contentId == id).ToList();
            foreach (var item in cmnt)
            {
                db.Comments.Remove(item);
            }
            db.Contents.Remove(cntnt);
            db.SaveChanges();
            return Json(new JsonData()
            {
                Html = "",
                Success = true,
                Script = "alert('!مطلب شما حذف شد')"

            });

        }
        [HttpGet]
        [Authorize]

        public ActionResult AddContent()
        {
            AddContentViewModel a = new AddContentViewModel();
            Content content = new Content();
            a.Content = content;
            ViewBag.Groups  = new SelectList(db.Groups); 
            return View(a);
        }
        [HttpPost]
        [Authorize]

        public ActionResult AddContent(Content content, HttpPostedFileBase Myfile)
        {
            AddContentViewModel a = new AddContentViewModel();
            a.Groups = db.Groups.ToList();
            a.Content = content;

            content.userId = Convert.ToInt32(Session["Log"]);
            content.Datetime = DateTime.Now.ToPersianDate().ToString();
            content.Status = 0;

            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Myfile.FileName.ToString()))
                {
                    //UploadImage
                    content.Image = Myfile.FileName;
                    string path = Server.MapPath("~") + "Files\\UploadImage\\" + Myfile.FileName;
                    Myfile.SaveAs(path);
                    Myfile.InputStream.ResizeImageByWidth(700, path, Utilty.ImageComperssion.Normal);
                }
                db.Contents.Add(content);
                if (db.SaveChanges()>0)
                {
                    return RedirectToAction("myContents");
                }
                else
                {
                    ViewBag.message = "ثبت مطلب انجام نشد";
                    ViewBag.style = "red";
                    return View(a);
                }

            }
            else
            {

                ViewBag.message = "مقادیر ورودی صحیح نمی باشد";
                ViewBag.style = "blue";
                return View(a);
            }
        }










    }
}