using Models.Entities;
using Models.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services
{
    public class SettingService : ISettingService
    {
        SettingRepository settingRepository = new SettingRepository();
        public bool ChangeLogo(string Name)
        {
            try
            {
                if (Name != "Logo.png")
                {
                    string filenameLogo = HttpContext.Current.Server.MapPath("~") + "Files\\Picture\\Logo\\Logo.png";
                    string newName = HttpContext.Current.Server.MapPath("~") + "Files\\Picture\\Logo\\" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".png";
                    string newLogo = HttpContext.Current.Server.MapPath("~") + "Files\\Picture\\Logo\\" + Name;
                    File.Move(filenameLogo, newName);
                    File.Move(newLogo, filenameLogo);
                }
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool Edit(Setting model)
        {
            var result=  settingRepository.Update(model);
            return result;
        }

        public bool DeleteLogo(string Name)
        {
            if (Name == "Logo.png")
            {
                return false;
            }
            File.Delete(HttpContext.Current.Server.MapPath("~") + "Files\\Picture\\Logo\\" + Name);
            return true;
        }

        public bool UploadLogo(HttpPostedFileBase File)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath("~") + "Files\\Picture\\Logo\\" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".png";
                File.SaveAs(path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
