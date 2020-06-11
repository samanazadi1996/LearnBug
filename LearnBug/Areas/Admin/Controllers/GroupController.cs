using Models;
using Models.Entities;
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
        DatabaseContext db = new DatabaseContext();
        [HttpGet]
        public ActionResult ManagementGroups()
        {
            return View(db.Groups);
        }
        public ActionResult _GroupList()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult AddGroup(string Name ,string Image)
        {

            if (!db.Groups.Any(p=>p.Name.ToLower()==Name))
            {
                var filename = "/Files/GroupPicture/Group_" + (db.Groups.OrderByDescending(p=>p.Id).FirstOrDefault().Id+1).ToString() + ".jpg";
                Utility.ConvertBase64toFile.Convert_base64_url_Image(Image, filename);

                Group group = new Group { 
                    Name = Name ,
                    Image= filename
                };
                db.Groups.Add(group);
                if (db.SaveChanges() > 0)
                {
                    return Json(new {
                        Html = this.RenderPartialToString("_GroupList", db.Groups),
                        Success = true,
                        Script = "alert('گروه اضافه شد')"
                    });
                }
                else
                {
                    return Json(new {
                        Html = "",
                        Success = false,
                        Script = "alert('گروه اضافه نشد')"
                    });
                }

            }
            else
            {
                return Json(new {
                    Html = "",
                    Success = false,
                    Script = "alert('گروه از قبل وجود دارد')"

                });
            }
        }
        [HttpPost]
        public ActionResult DeleteGroup(int Id)
        {

            if (!string.IsNullOrEmpty(Id.ToString()))
            {

                Group group = db.Groups.Find(Id);
                db.Groups.Remove(group);
                if (db.SaveChanges() > 0)
                {
                    return Json(new {
                        Html = this.RenderPartialToString("_GroupList", db.Groups),
                        Success = true,
                        Script = "alert('گروه حذف شد')"
                    });
                }
                else
                {
                    return Json(new {
                        Html = "",
                        Success = false,
                        Script = "alert('گروه حذف نشد')"
                    });
                }

            }
            else
            {
                return Json(new {
                    Html = "",
                    Success = false,
                    Script = "alert('گروه انتخواب نشده است')"

                });
            }
        }
        [HttpPost]
        public ActionResult EditGroup(int Id ,string Name, string Image)
        {

            if (!string.IsNullOrEmpty(Id.ToString()))
            {
                var group = db.Groups.Find(Id);
                var filename = "/Files/GroupPicture/Group_" + group.Id + ".jpg";
                Utility.ConvertBase64toFile.Convert_base64_url_Image(Image, filename);

                group.Name = Name;
                group.Image = filename;
                if (db.SaveChanges() > 0)
                {
                    return Json(new {
                        Html = this.RenderPartialToString("_GroupList", db.Groups),
                        Success = true,
                        Script = "alert('گروه ویرایش شد')"
                    });
                }
                else
                {
                    return Json(new {
                        Html = "",
                        Success = false,
                        Script = "alert('گروه ویرایش نشد')"
                    });
                }

            }
            else
            {
                return Json(new {
                    Html = "",
                    Success = false,
                    Script = "alert('گروه انتخواب نشده است')"

                });
            }
        }


    }
}