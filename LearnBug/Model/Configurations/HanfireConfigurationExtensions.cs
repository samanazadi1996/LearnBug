﻿using Hangfire;
using Hangfire.Dashboard;
using Microsoft.Owin;
using Owin;

namespace LearnBug.Model.Configurations
{


    public static class HanfireConfigurationExtensions
    {
        public static void AddHangfireServices(IAppBuilder app,string connectionStringName)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage(connectionStringName);
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorizationFilter() }
            });
            app.UseHangfireDashboard();

            app.UseHangfireServer();
            //RecurringJob.AddOrUpdate(() =>
            //File.Delete(System.Web.HttpContext.Current.Server.MapPath("~") + "logs/app-log.txt"), Cron.DayInterval(1));

            //RecurringJob.AddOrUpdate(() => MailSender.SendMail("adasdasd", "sadasdasdsad", "samanazadi1996@gmail.com"), Cron.MinuteInterval(50));

        }

    }

    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            // In case you need an OWIN context, use the next line, `OwinContext` class
            // is the part of the `Microsoft.Owin` package.
            var owinContext = new OwinContext(context.GetOwinEnvironment());

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return owinContext.Authentication.User.Identity.IsAuthenticated;
        }
    }

}
