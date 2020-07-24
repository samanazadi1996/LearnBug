using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace LearnBug.Areas.Admin.Controllers
{
    public class CommonController : Controller
    {
        private readonly ICommonService _commonService;

        public CommonController(ICommonService commonService)
        {
            _commonService = commonService;
        }
        // GET: Admin/Common
        public ActionResult Notification()
        {
            var model = _commonService.Notification();
            return PartialView(model);
        }
        public ActionResult Footer()
        {
            var model = _commonService.Footer();
            return PartialView(model);
        }

    }
}