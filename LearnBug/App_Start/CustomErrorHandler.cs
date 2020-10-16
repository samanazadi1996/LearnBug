using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Web;
using System.Web.Mvc;

namespace LearnBug.App_Start
{
    public class CustomErrorHandler : HandleErrorAttribute, IExceptionFilter
    {
		public void OnException(ExceptionContext filterContext)
		{
			Logger logger = LogManager.GetCurrentClassLogger();
			Exception exception = filterContext.Exception;
			if (exception.GetType().Name == "HttpAntiForgeryException")
				return;
			if (exception.GetType().Name != "HttpException")
			{
				string str1 = "";
				if (exception is DbEntityValidationException)
				{
					foreach (DbEntityValidationResult entityValidationError in ((DbEntityValidationException)exception).EntityValidationErrors)
					{
						foreach (DbValidationError validationError in (IEnumerable<DbValidationError>)entityValidationError.ValidationErrors)
							str1 = str1 + "خطای اطلاعات ورودی برای " + validationError.PropertyName + " <br/> " + validationError.ErrorMessage + " <br/>";
					}
				}
				string str2 = ", Url:" + (object)filterContext.HttpContext.Request.Url + ", UrlReferrer:" + (object)filterContext.HttpContext.Request.UrlReferrer;
				string str3 = "App_Error[ip:" + (HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : "") + " , user:" + (HttpContext.Current?.User != null ? HttpContext.Current.User.Identity.Name : "") + "] " + str2;
				if (exception is InfoException)
				logger.Error((object)(str3 + exception.Message));
				else
				logger.Error((object)(str3 + str1));
			}
			else
			{
				string str1 = HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : "";
				string str2 = HttpContext.Current?.User != null ? HttpContext.Current.User.Identity.Name : "";
				logger.Error((object)("App_Error[ip:" + str1 + " , user:" + str2 + "]"));
			}
		}
	}
}