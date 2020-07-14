using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Drawing;

namespace Utility
{
    public static class ConvertBase64toFile
    {
        public static string Convert_Htmlbase64_url_Image(string htmlString, string savetourl, string url)
        {
            Random rnd = new Random();
            string pattern = "<img.*?src=[\"'](.+?)[\"'].*?>";
            string a = "", filename = "";
            int o = 0;
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = rgx.Matches(htmlString);

            for (int i = 0, l = matches.Count; i < l; i++)
            {
                a = matches[i].Groups[1].ToString();
                {
                    if (a.Substring(0, 5).ToString().ToLower() == "data:")
                    {
                        Thread.Sleep(200);
                        filename = DateTime.Now.ToString("yyyyMMddhhmmss") + ((int)rnd.Next(0, 1000000000)) + (o++).ToString() + ".jpg";
                        #region Convert base64 to image
                        byte[] imageBytes = Convert.FromBase64String(a.Substring(a.LastIndexOf(",") + 1));
                        MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                        ms.Write(imageBytes, 0, imageBytes.Length);
                        Image img = Image.FromStream(ms, true);
                        #endregion
                        img.Save(savetourl + filename);
                        htmlString = htmlString.Replace(a, url + filename);
                    }
                }
            }
            return htmlString;

        }
        public static void Convert_base64_url_Image(string image, string filename)
        {
            filename = filename.Substring(1);
            var path = HttpContext.Current.Server.MapPath("~").Replace("\\","/");

            if (image.Substring(0, 5).ToString().ToLower() == "data:")
            {
                #region Convert base64 to image
                byte[] imageBytes = Convert.FromBase64String(image.Substring(image.LastIndexOf(",") + 1));
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);
                Image img = Image.FromStream(ms, true);
                #endregion
                img.Save(path + filename);
            }   
        }

    }
}
