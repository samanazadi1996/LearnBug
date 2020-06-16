using Models.Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace LearnBug.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        public ActionResult Index()
        {
            var myUserName = User.Identity.Name;
            var message = db.Messages.Where(p => p.sender.Username == myUserName || p.Reciver.Username == myUserName);
            var model = message.Select(p => new MessageViewModel {
                User =(p.Reciver.Username== myUserName ? p.sender:p.Reciver)
                });
            return View(model.Distinct());
        }
        public ActionResult _Inbox()
        {
            var me = db.Users.Single(p => p.Username == User.Identity.Name);
            var model = me.Inbox.OrderByDescending(p=>p.InsertDateTime);
            return PartialView(model);
        }
        public ActionResult _Sent()
        {
            var me = db.Users.Single(p => p.Username == User.Identity.Name);
            var model = me.Sent.OrderByDescending(p => p.InsertDateTime);
            return PartialView(model);
        }
        public JsonResult SendMessage(string text, int to)
        {
            var message = new Message
            {
                Text = text,
                reciverId = to,
                Status = 0

            };
            if (ModelState.IsValid)
            {
                db.Users.Single(p => p.Username == User.Identity.Name).Sent.Add(message);
                if (db.SaveChanges() > 0)
                {
                    return Json(new {msg= "toastr.success('پیغام ارسال شد')" });
                }
                else
                {
                    return Json(new { msg= "toastr.error('پیغام ارسال نشد')" });
                }
            }
            else
            {
                return Json(new { msg = "toastr.warning('مقادیر نامعتبر')" });

            }
        }
        public JsonResult Viewmessage(int id)
        {
            var msg = db.Messages.Find(id);
            int user = db.Users.Single(p => p.Username == User.Identity.Name).Id;
            if (msg.reciverId == user || msg.senderId == user)
            {
                if (msg.Status < 2 && msg.reciverId == user)
                {
                    msg.Status = 2;
                    db.SaveChanges();
                }
                return Json(new
                {
                    success = true,
                    text = msg.Text,
                    username = User.Identity.Name,
                });

            }
            else
            {
                return Json(new { success = false });
            }

        }

    }
}