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
    public class FactorController : Controller
    {
        DatabaseContext db = new DatabaseContext();


        public ActionResult Index(int Page = 1)
        {
            var factors = db.Factors.Where(p => p.User.Username == User.Identity.Name).OrderByDescending(p => p.InsertDateTime).Select(p=>p.postId).AsQueryable();

            PostsViewModel model = new PostsViewModel
            {
                postId = factors,
                CurrentPage = Page,
                TotalItemCount = factors.Count()
            };
            return View(model);
        }

        public ActionResult _SellContent(int Id)
        {
            var model = db.Factors.Where(p => p.Post.User.Username == User.Identity.Name).OrderByDescending(p=>p.InsertDateTime).AsQueryable();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult CreateFactor(int Id)
        {
            var me = db.Users.Single(p => p.Username == User.Identity.Name);

            var content = db.Posts.Find(Id);
            if (!content.Factors.Any(p => p.User.Username == me.Username))
            {
                if (me.Wallet >= content.Price)
                {
                    Factor factor = new Factor()
                    {
                        postId = Id,
                        Price = content.Price.Value,
                        Commission = content.User.Commission
                    };
                    me.Factors.Add(factor);
                    me.Wallet -= content.Price.Value;
                    content.User.Wallet += (content.Price.Value - (content.Price.Value / 100 * content.User.Commission));
                    db.SaveChanges();
                    return Json(new { Success = true, Html = content.Content, Script = "alert('خرید با موفقیت انجام شد')" });

                }
                else
                {
                    return Json(new { Success = false, Html = "", Script = "alert('کیف پول خود را شارژ کنید')" });
                }
            }
            else
            {
                return Json(new { Success = false, Html = "", Script = "alert('error');" });

            }
        }

    }
}