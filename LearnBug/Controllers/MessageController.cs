using LearnBug.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnBug.Controllers
{
    public class MessageController : Controller
    {
        LearnBug.Models.DomainModels.LearnBugDBEntities1 db = new Models.DomainModels.LearnBugDBEntities1();
        [Authorize]
        public ActionResult myMessage()
        {
            int userId = db.Users.Single(P=>P.Username==User.Identity.Name).Id;
            var model = db.Users.OrderByDescending(p => p.Id).Where(p => p.Messages.Any(o => o.FromuserId == userId || o.TouserId == userId)).OrderByDescending(p => p.Id);
            var modelstatus = db.Messages.Where(p => p.TouserId == userId && p.Status == 0);
            foreach (var item in modelstatus)
            {
                item.Status = 1;
            }
            db.SaveChanges();
            return View(model);
        }
        public ActionResult _viewfullMessage(int id)
        {
            int a = db.Users.Single(p=>p.Username==User.Identity.Name).Id;
            var user = db.Users.Find(id);
            ViewBag.username = user.Username;
            ViewBag.id = user.Id;
            ViewBag.image = user.Image;
            ViewBag.name = user.name;
            var model = db.Messages.Where(p => (p.FromuserId == a && p.TouserId == id) || (p.FromuserId == id && p.TouserId == a));
            var modelstatus = model.Where(p => p.TouserId == a && p.Status != 2);
            foreach (var item in modelstatus) item.Status = 2;
            db.SaveChanges();

            return PartialView(model);
        }
        public JsonResult Viewmessage(int id)
        {
            var msg = db.Messages.Find(id);
            int user = db.Users.Single(p => p.Username == User.Identity.Name).Id;
            if (msg.TouserId == user || msg.FromuserId == user)
            {
                if (msg.Status < 2 && msg.TouserId == user)
                {
                    msg.Status = 2;
                    db.SaveChanges();
                }
                return Json(new
                {
                    success = true,
                    datetime = msg.Datetime,
                    text = msg.Text,
                    username = User.Identity.Name,
                });

            }
            else
            {
                return Json(new { success = false });
            }

        }
        //public JsonResult likemsg(int id)
        //{
        //    var msg = db.Messages.Find(id);
        //    var user = Convert.ToInt32(Session["Log"]);
        //    if (user == msg.TouserId)
        //    {
        //        if (msg.Status == 2)
        //            msg.Status = 1;
        //        else
        //            msg.Status = 2;

        //        db.SaveChanges();

        //        return Json(new {heart = "red" });
        //     }
        //    else
        //    {
        //        return Json(new { heart = "black" });
        //    }

        //}


    }

}