using LearnBug.App_Start;
using System.Web;
using System.Web.Mvc;

namespace LearnBug
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {      
            filters.Add(new CustomErrorHandler());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
