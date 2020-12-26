using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using NLog;
using Owin;
using WebFramework.Configurations;

[assembly: OwinStartup(typeof(LearnBug.Startup))]

namespace LearnBug
{
    public class Startup
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage(@"data source=.;initial catalog=LearnBugDatabase;integrated security=True;");
            app.AddHangfireServices();
        }
    }
}
