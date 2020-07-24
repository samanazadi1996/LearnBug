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
    public class BookmarkController : Controller
    {
        private readonly IBookmarkService _bookmarkService;
        public BookmarkController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }
        // GET: Bookmark    
        [Authorize]

        public JavaScriptResult CreateOrDeleteBookmark(int postId)
        {
            var result = _bookmarkService.CreateOrDeleteBookmark(postId);
            return JavaScript(result);
        }

        public ActionResult Index(int Page = 1, string search = null)
        {
            var model = _bookmarkService.GetAllBookmarks(Page, search);
            if (!string.IsNullOrEmpty(search))
                ViewBag.Srch = search.Trim();
            return View(model);
        }

    }
}