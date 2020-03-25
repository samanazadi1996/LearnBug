using LearnBug.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Utilty;

namespace LearnBug.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GroupController : Controller
    {
        LearnBug.Models.DomainModels.LearnBugDBEntities1 db = new Models.DomainModels.LearnBugDBEntities1();
        [HttpGet]
        public ActionResult ManagementGroups()
        {
            return View(db.Groups);
        }
        [HttpPost]
        public ActionResult ManagementGroups(Group group)
        {

            if (ModelState.IsValid && !string.IsNullOrEmpty(group.Name.Trim()))
            {
                db.Groups.Add(group);
                if (db.SaveChanges() > 0)
                {
                    return Json(new JsonData()
                    {
                        Html = this.RenderPartialToString("_GroupList", db.Groups),
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

                Group group = db.Groups.Find(Id);
                db.Groups.Remove(group);
                if (db.SaveChanges() > 0)
                {
                    return Json(new JsonData()
                    {
                        Html = this.RenderPartialToString("_GroupList", db.Groups),
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