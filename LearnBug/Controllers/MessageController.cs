using Models.Entities;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using Services;

namespace LearnBug.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }   
        public ActionResult Index()
        {
            var result = _messageService.GetAllMessages();
                return View(result);
        }
        public ActionResult _Inbox()
        {
            var result = _messageService.Inbox();
            return PartialView(result.ToList());
        }
        public ActionResult _Sent()
        {
            var result = _messageService.Sent();
            return PartialView(result.ToList());
        }
        public JsonResult SendMessage(string text, int to)
        {
            var result = _messageService.SendMessage(text,to);
            return Json(new { msg = result });
        }
    }
}