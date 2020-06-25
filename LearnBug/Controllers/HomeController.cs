using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using NLog;
using Models;
using ViewModels;

namespace LearnBug.Controllers
{
    public class HomeController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ActionResult About()
        {
            var model = db.Settings.Single(p => p.Name == "About");
            return View(model);
        }
        public ActionResult Index(string search = null, int Page = 1)
        {
            var contents = db.Posts.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                contents = contents.Where(p => p.Subject.Contains(search) || p.Group.Name.Contains(search) || p.User.Name.Contains(search) || p.User.Username.Contains(search) || p.Price.ToString().Contains(search)|| p.KeyWords.Contains(search)) ;
                ViewBag.Srch = search.Trim();
            }
            PostsViewModel model = new PostsViewModel
            {
                PostId = contents.OrderByDescending(p => p.InsertDateTime).Skip((Page - 1) * 12).Take(12).Select(p => p.Id),
                CurrentPage = Page,
                TotalItemCount = contents.Count(),
                Groups=db.Groups    
            };
            return View(model);
        }
    }
}