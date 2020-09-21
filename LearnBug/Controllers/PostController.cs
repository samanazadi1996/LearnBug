using Models.Entities;
using Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using Services;
using PagedList;

namespace LearnBug.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IGroupService _groupService;

        public PostController(IPostService postService,IGroupService groupService)
        {
            _postService = postService;
            _groupService = groupService;
        }
        public ActionResult _SinglePost(int id)
        {
            var post = _postService.GetSinglePostById(id);
            return PartialView(post);
        }

        public ActionResult ViewPost(int id)
        {
            var Content = _postService.GetRowById(id);
            return View(Content);
        }

        [Authorize]
        public ActionResult Index(int? page)
        {
            var result = _postService.GetMyPosts();
            var pageNumber = page ?? 1;
            var model = result.ToPagedList(pageNumber, 12);

            return View(model);
        }
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Groups = new SelectList(_groupService.GetAllGroups(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Create(Post post)
        {
            if (_postService.Create(post))
            {
                return RedirectToAction("ViewPost", "Post", new { id = _postService.GetLastPost() });

            }
            else
            {
                ViewBag.Groups = new SelectList(_groupService.GetAllGroups(), "Id", "Name");
                ViewBag.message = "ثبت مطلب انجام نشد";
                ViewBag.style = "red";
                return View(post);
            }


        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            var model = _postService.GetRowById(id);

            if (User.IsInRole("Admin") || model.User.Username == User.Identity.Name)
            {
                ViewBag.Groups = new SelectList(_groupService.GetAllGroups(), "Id", "Name");
                return View(model);
            }
            return View("AddContent");

        }
        [HttpPost]
        [Authorize]
        public ActionResult Edit(Post post)
        {
            if (_postService.IsMainPost(post.Id))
            {
                if (_postService.Edit(post))
                {
                    return RedirectToAction("ViewPost", "Post", new { id = post.Id });
                }
            }
            ViewBag.Groups = new SelectList(_groupService.GetAllGroups(), "Id", "Name");
            ViewBag.message = "ثبت مطلب انجام نشد";
            ViewBag.style = "red";
            return View(post);

        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            var model = _postService.GetRowById(id);
            if (_postService.IsMainPost(id))
                return View(model);
            return HttpNotFound();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Delete(Post post)
        {
            _postService.Delete(post);
            return HttpNotFound();
        }

    }
}