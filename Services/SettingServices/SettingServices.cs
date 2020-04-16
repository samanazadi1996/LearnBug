using Services.SettingServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services.SettingServices
{
    public class SettingServices : ISettingServices
    {
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
        
        public bool DeleteLogo(string Name)
        {
            if (Name == "Logo.png")
            {
                return false;
            }
            File.Delete(HttpContext.Current.Server.MapPath("~") + "Files\\Picture\\Logo\\" + Name);
            return true;
        }
    }
}
