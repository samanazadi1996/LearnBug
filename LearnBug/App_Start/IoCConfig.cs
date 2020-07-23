using Autofac;
using Autofac.Integration.Mvc;
using Models.Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace LearnBug
{
    public class IoCConfig
    {
        public static void RegisterDependencies()
        {
            #region Create the builder
            var builder = new ContainerBuilder();
            #endregion

            #region Services
            builder.RegisterAssemblyTypes(typeof(ISettingService).Assembly).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(IGroupService).Assembly).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(ITransactionService).Assembly).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(IUserService).Assembly).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerRequest();
            #endregion

            #region Repositories
            builder.RegisterAssemblyTypes(typeof(ISettingRepository).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(IGroupRepository).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(ITransactionRepository).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            builder.RegisterAssemblyTypes(typeof(IUserRepository).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();
            #endregion

            #region Register all controllers for the assembly
            builder.RegisterControllers(typeof(MvcApplication).Assembly).InstancePerHttpRequest();
            #endregion

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            #region Register modules
            builder.RegisterAssemblyModules(typeof(MvcApplication).Assembly);
            #endregion

            #region Inject HTTP Abstractions
            builder.RegisterModule<AutofacWebTypesModule>();
            #endregion

            #region Set the MVC dependency resolver to use Autofac
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            #endregion

        }

    }
}