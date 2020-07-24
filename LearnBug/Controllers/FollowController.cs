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
    public class FollowController : Controller
    {
        private readonly IFollowService _followService;

        public FollowController(IFollowService followService)
        {
            _followService = followService;
        }        // GET: Follow
        [HttpPost]
        public string AddOrDeleteFollow(int id)
        {
            var result = _followService.AddOrDeleteFollow(id);
            return result;
        }
        public ActionResult Followers(string id)
        {
            var model = _followService.Followers(id);
            return View(model);
        }
        public ActionResult Following(string id)
        {
            var model = _followService.Following(id);
            return View(model);
        }
    }
}