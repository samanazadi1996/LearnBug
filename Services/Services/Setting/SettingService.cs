﻿using Models.Entities;
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
        private readonly ISettingRepository _settingRepository;
        public SettingService(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }
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
            var result = _settingRepository.Update(model);
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
        public Setting GetRowSelectelById(int id)
        {
            try
            {
                var Result = _settingRepository.Find(id);
                return Result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public IEnumerable<Setting> GetAllSetting()
        {
            try
            {
                var model = _settingRepository.Select().ToList();
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
