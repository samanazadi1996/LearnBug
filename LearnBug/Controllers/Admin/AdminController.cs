using LearnBug.Models.Repositories;
using LearnBug.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Utilty;

namespace LearnBug.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {

        public ActionResult AdminPanel()
        {

            return View();
        }
        [HttpGet]
        public ActionResult ManagementGroups()
        {
            GroupRepository db = new GroupRepository();

            return View(db.Select());
        }
        [HttpPost]
        public ActionResult ManagementGroups(Group group)
        {
         
            if (ModelState.IsValid && !string.IsNullOrEmpty(group.Name.Trim()))
            {
                
            GroupRepository db = new GroupRepository();
                if (db.Add(group))
                {
                    return Json(new JsonData()
                    {
                        Html = this.RenderPartialToString("_GroupList", db.Select()),
                        Success = true,
                        Script = "alert('گروه اضافه شد')"
                    });
                }
                else
                {
                    return Json(new JsonData()
                    {
                        Html = "",
                        Success = false,
                        Script = "alert('گروه اضافه نشد')"
                    });
                }

            }
            else
            {
                return Json(new JsonData()
                {
                    Html = "",
                    Success = false,
                    Script = "alert('گروه اضافه نشد')"

                });
            }
        }
        public ActionResult DeleteGroup(int Id)
        {

            if (!string.IsNullOrEmpty(Id.ToString()))
            {

                GroupRepository db = new GroupRepository();
                Group group = db.Find(Id);
                if (db.Delete(group))
                {
                    return Json(new JsonData()
                    {
                        Html = this.RenderPartialToString("_GroupList", db.Select()),
                        Success = true,
                        Script = "alert('گروه حذف شد')"
                    });
                }
                else
                {
                    return Json(new JsonData()
                    {
                        Html = "",
                        Success = false,
                        Script = "alert('گروه حذف نشد')"
                    });
                }

            }
            else
            {
                return Json(new JsonData()
                {
                    Html = "",
                    Success = false,
                    Script = "alert('گروه انتخواب نشده است')"

                });
            }
        }

        public ActionResult _GroupList()
        {
            return PartialView();
        }

    }
}