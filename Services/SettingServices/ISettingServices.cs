using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services.SettingServices
{
   public interface ISettingServices
    {
        bool ChangeLogo(string Name);
        bool DeleteLogo(string Name);
        bool UploadLogo(HttpPostedFileBase File);
    }
}
