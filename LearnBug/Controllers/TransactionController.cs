using Models.Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnBug.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        // GET: Transaction

        public ActionResult _Index()
        {
            var transactions = db.Users.Single(p => p.Username == User.Identity.Name).Transactions.OrderByDescending(p => p.InsertDateTime).AsQueryable();
            return PartialView(transactions);
        }


        public ActionResult _Add()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult _Add(double price)
        {
            var user = db.Users.Single(p => p.Username == User.Identity.Name);
            Transaction transaction = new Transaction
            {
                Price = price,
                Charge = true,
            };

            user.Transactions.Add(transaction);
            user.Wallet += price;
            db.SaveChanges();
            return View();
        }








    }
}