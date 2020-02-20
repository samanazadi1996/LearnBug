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
            return View();
        }
        public ActionResult _viewfullMessage(string type)
        {
            int a = Convert.ToInt32(Session["Log"]);
            myMessageViewModel model = new myMessageViewModel();
            if (type== "From")
            {
                model.messages = db.Messages.OrderByDescending(p => p.Id).Where(p => p.FromuserId == a).ToList();
                ViewBag.Typemessage = "صندوق ارسالی";
                ViewBag.Typ = "From";

            }
            else
            {
                model.messages = db.Messages.OrderByDescending(p => p.Id).Where(p =>  p.TouserId == a).ToList();
                ViewBag.Typemessage = "صندوق ورودی";
                ViewBag.Typ = "To";
            }
            model.Users = db.Users.ToList();
            foreach (var item in model.messages)
            {
                if (item.Status == 0 && item.TouserId == a) item.Status = 1;
            }
            db.SaveChanges();
            return PartialView(model);
        }
        public JsonResult Viewmessage(int id)
        {
            var msg = db.Messages.Find(id);
            int user = Convert.ToInt32(Session["Log"]);
            if (msg.TouserId == user || msg.FromuserId == user)
            {
                if (msg.Status <2 && msg.TouserId == user)
                {
                    msg.Status = 2;
                    db.SaveChanges();
                }
                return Json(new {
                    success = true,
                    datetime = msg.Datetime,
                    text = msg.Text,
                    username = db.Users.Find(user).Username,
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