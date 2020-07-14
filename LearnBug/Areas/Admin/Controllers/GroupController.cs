using Models;
using Models.Entities;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnBug.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        [HttpGet]
        public ActionResult ManagementGroups()
        {
            return View(_groupService.GetAllGroups());
        }
        public ActionResult _GroupList()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult AddGroup(string Name, string Image)
        {
            if (_groupService.AddGroup(Name, Image))
            {
                return Json(new
                {
                    Html = this.RenderPartialToString("_GroupList", _groupService.GetAllGroups()),
                    Success = true,
                    Script = "toastr.success('گروه اضافه شد')"
                });
            }
            return Json(new
            {
                Html = "",
                Success = false,
                Script = "toastr.error('گروه از قبل وجود دارد')"
            });
        }
        [HttpPost]
        public ActionResult DeleteGroup(int Id)
        {
            if (_groupService.DeleteGroup(Id))
            {
                return Json(new
                {
                    Html = this.RenderPartialToString("_GroupList", _groupService.GetAllGroups()),
                    Success = true,
                    Script = "toastr.success('گروه حذف شد')"
                });
            }
            return Json(new
            {
                Html = "",
                Success = false,
                Script = "toastr.error('گروه حذف نشد')"
            });
        }
        [HttpPost]
        public ActionResult EditGroup(int Id, string Name, string Image)
        {
            if (_groupService.EditGroup(Id, Name, Image))
            {
                return Json(new
                {
                    Html = this.RenderPartialToString("_GroupList", _groupService.GetAllGroups()),
                    Success = true,
                    Script = "toastr.success('گروه ویرایش شد')"
                });
            }
            return Json(new
            {
                Html = "",
                Success = false,
                Script = "toastr.error('گروه ویرایش نشد')"
            });


        }
    }
}