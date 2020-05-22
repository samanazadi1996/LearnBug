using LearnBug.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnBug.Controllers
{
    [Authorize]
    public class FactorController : Controller
    {
        LearnBugDBEntities1 db = new LearnBugDBEntities1();



        public ActionResult _Index()
        {
            var model = db.Factors.Where(p => p.User.Username == User.Identity.Name).OrderByDescending(p => p.Datetime).AsQueryable();
            return PartialView(model);
        }
        public ActionResult _SellContent()
        {
            var model = db.Factors.Where(p => p.Content.User.Username == User.Identity.Name).OrderByDescending(p=>p.Datetime).AsQueryable();
            return PartialView(model);
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
                    return Json(new { Success = true, Html = content.Description, Script = "alert('خرید با موفقیت انجام شد')" });

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


        [Authorize(Roles ="Admin")]
        public ActionResult Factors( string from=null,string to=null)
        {
            var model = db.Factors.AsQueryable();

            if (!string.IsNullOrEmpty(from))
            {
                DateTime datefrom =Convert.ToDateTime(from).ToMiladiDate();
                model = model.Where(p => p.Datetime >= datefrom);
            }

            if (!string.IsNullOrEmpty(to))
            {
                DateTime dateto = Convert.ToDateTime(to).ToMiladiDate();
                model = model.Where(p => p.Datetime <= dateto);
            }
            ViewBag.from = from;
            ViewBag.to = to;

            return View(model.OrderByDescending(p=>p.Datetime));
        }






    }
}