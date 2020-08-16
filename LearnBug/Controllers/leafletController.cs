using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ViewModels;
using System.Web.Mvc;
using Services;

namespace LearnBug.Controllers
{
    public class leafletController : Controller
    {
        private readonly ILeafletService _leafletService;

        public leafletController(ILeafletService leafletService)
        {
            _leafletService = leafletService;
        }
        // GET: leaflet
        public ActionResult Create()
        {
            return PartialView();
        }    
        [Authorize]
        public ActionResult Edit()
        {
            var model = _leafletService.GetLocation();
            return PartialView(model);
        }
        [HttpPost]
        [Authorize]
        public JsonResult Edit(LeafletViewModel leaflet)
        {
            if (_leafletService.Edit(leaflet))
                return Json("مکان شما با موفقیت ثبت شد");
            return Json("خطا در ویرایش مکان شما");
        }
    }
}