using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Formater
{
    public class Formater
    {

        public static string formatter(string text)
        {
            text = "تولدتان مبارک آقای {1} سال خوبی داشته باشید";
            text = string.Format(text,
                GetUserNane(),
                GetNane(),
                GetDateofbirth()            
                );
                return text;
        }

        public static string GetUserNane()
        {
                return "saman";
        }
        public static string GetNane()
        {
                return "aaaa";
        }
        public static string GetDateofbirth()
        {
                return "daaaaaa";
        }
    }
}
