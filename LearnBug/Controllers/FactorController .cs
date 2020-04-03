using LearnBug.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Utilty;

namespace LearnBug.Controllers
{
    [Authorize]
    public class FactorController : Controller
    {
        LearnBug.Models.DomainModels.LearnBugDBEntities1 db = new Models.DomainModels.LearnBugDBEntities1();
        // GET: Bookmark
        public ActionResult Index()
        {
            var model = db.Factors.Where(p => p.User.Username == User.Identity.Name);
            return View(model);
        }
        public ActionResult SellContent()
        {
            var model = db.Factors.Where(p => p.Content.User.Username == User.Identity.Name);
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateFactor(int Id)
        {
            var me = db.Users.Single(p => p.Username == User.Identity.Name);

            var content = db.Contents.Find(Id);
            if (!content.Factors.Any(p => p.User.Username == me.Username))
            {
                if (me.Wallet >= content.Price)
                {
                    Factor factor = new Factor()
                    {
                        Datetime = DateTime.Now,
                        contentId = Id,
                        Price = content.Price.Value,
                        Commission = content.User.Commission
                    };
                    me.Factors.Add(factor);
                    me.Wallet -= content.Price.Value;
                    content.User.Wallet += (content.Price.Value - (content.Price.Value / 100 * content.User.Commission));
                    db.SaveChanges();
                    return Json(new JsonData { Success = true, Html = content.Description, Script = "alert('خرید با موفقیت انجام شد')" });

                }
                else
                {
                    return Json(new JsonData { Success = false, Html = "", Script = "alert('کیف پول خود را شارژ کنید')" });
                }
            }
            else
            {
                return Json(new JsonData { Success = false, Html = "", Script = "alert('error');" });

            }
        }

    }
}