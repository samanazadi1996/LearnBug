using Hangfire;
using Hangfire.SqlServer;
using LearnBug.Model.Configurations;
using Microsoft.Owin;
using NLog;
using Owin;

[assembly: OwinStartup(typeof(LearnBug.Startup))]

namespace LearnBug
{
    public class Startup
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public void Configuration(IAppBuilder app)
        {
            AutofacConfigurationExtensions.RegisterDependencies();
            HanfireConfigurationExtensions.AddHangfireServices(app);
        }
    }
}
