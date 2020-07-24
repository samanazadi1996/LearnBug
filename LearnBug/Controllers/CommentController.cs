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
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public ActionResult _Comments(int Id)
        {
            var result = _commentService.GetAllCommentsByPost(Id);
            ViewBag.PostId = Id;
            return PartialView(result);
        }
        [HttpPost]
        [Authorize]
        public string deleteComment(int id)
        {
            var result = _commentService.Delete(id);
            return result;
        }

        [Authorize]
        public JsonResult SendComment(int id, string text)
        {
            try
            {
                var result = _commentService.Create(id, text);
                if (result.Any())
                    return Json(new { success = true, html = this.RenderPartialToString("_Comments", result), message = "toastr.success('کامنت شما ثبت شد')" });
                return Json(new { success = false, html = "", message = "toastr.error('کامنت شما ثبت نشد')" });
            }
            catch (Exception)
            {
                return Json(new { success = false, html = "", message = "toastr.error('خطایی رخ داد')" });
            }
        }
    }
}