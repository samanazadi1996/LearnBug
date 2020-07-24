using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services
{
   public interface ISettingService
    {
        bool Edit(Setting model);
        bool ChangeLogo(string Name);
        bool DeleteLogo(string Name);
        bool UploadLogo(HttpPostedFileBase File);
        Setting GetRowSelectelById(int id);
        IEnumerable<Setting> GetAllSetting();
    }
}
