using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnBug.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GroupController : Controller
    {
        //LearnBugDBEntities1 db = new LearnBugDBEntities1();
        //public ActionResult Index()
        //{
        //    return View();
        //}
    }
}