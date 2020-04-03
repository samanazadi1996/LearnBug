using LearnBug.Models.DomainModels;
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
        LearnBug.Models.DomainModels.LearnBugDBEntities1 db = new Models.DomainModels.LearnBugDBEntities1();
        // GET: Transaction
        public ActionResult Add(double price)
        {
            var user = db.Users.Single(p => p.Username == User.Identity.Name);
            Transaction transaction = new Transaction
            {
                Datetime = DateTime.Now,
                Price = price,
                Charge = true,
            };

            user.Transactions.Add(transaction);
            user.Wallet += price;
            db.SaveChanges();
            return View();
        }
        public ActionResult Index()
        {
            var transactions = db.Users.Single(p => p.Username == User.Identity.Name).Transactions;
            return View(transactions);
        }
    }
}