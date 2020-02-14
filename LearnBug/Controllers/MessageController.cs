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
            int a = Convert.ToInt32(Session["Log"]);
            myMessageViewModel model = new myMessageViewModel();
            model.messages = db.Messages.OrderByDescending(p=>p.Id).Where(p => p.FromuserId == a || p.TouserId == a).ToList();
            model.Users = db.Users.ToList();
            return View(model);
        }
    }
}