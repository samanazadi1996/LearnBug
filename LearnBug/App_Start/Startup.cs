using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using NLog;
using Owin;
using System.IO;

[assembly: OwinStartup(typeof(LearnBug.Startup))]

namespace LearnBug
{
    public class Startup
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration
    .UseSqlServerStorage(@"data source=.;initial catalog=LearnBugDatabase;integrated security=True;");

            app.UseHangfireDashboard();
            app.UseHangfireServer();
            //RecurringJob.AddOrUpdate(() =>
            //File.Delete(System.Web.HttpContext.Current.Server.MapPath("~") + "logs/app-log.txt"), Cron.DayInterval(1));

            //RecurringJob.AddOrUpdate(() => MailSender.SendMail("adasdasd", "sadasdasdsad", "samanazadi1996@gmail.com"), Cron.MinuteInterval(50));
        }
    }
}
