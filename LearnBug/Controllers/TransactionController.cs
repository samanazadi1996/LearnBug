using Models.Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services;

namespace LearnBug.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IUserService _userService;
        public TransactionController(ITransactionService transactionService,IUserService userService)
        {
            _transactionService = transactionService;
            _userService = userService;
        }

        // GET: Transaction

        public ActionResult _Index()
        {
            return PartialView(_transactionService.GetAllTransaction());
        }


        public ActionResult _Add()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult _Add(double price)
        {
            _userService.AddTransactionByUser(price);
            return View();
        }








    }
}