using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Ajax.Utilities;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using SimpleInjector.Integration.Web.Mvc;
using Container = SimpleInjector.Container;
using ProAsp.Core.Services;
using ProAsp.Data;
using SimpleInjector.Integration.Web;

namespace ProAsp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private const bool release = 
#if DEBUG
            false;
#else
            true;
#endif

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DependencyResolver.SetResolver(RegisterContainer());
        }

        private SimpleInjectorDependencyResolver RegisterContainer()
        {
            var container = new Container();
            //container.Register<IUserService, UserService>();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.Register(typeof(IDbContext), typeof(ProAspDbContext), Lifestyle.Scoped);
            container.Register(typeof(IRepository<>), typeof(GenericRepository<>), Lifestyle.Transient);

            var services = typeof(UserService).Assembly.GetExportedTypes().Where(x=>x.Namespace=="ProAsp.Core.Services" && x.GetInterfaces().Any()).Select(y=>new{Service = y.GetInterfaces().Single(), Implementation = y} );
            foreach (var service in services)
            {
                container.Register(service.Service, service.Implementation, Lifestyle.Transient);
            }

            

            if (release)
            {
                container.Verify(VerificationOption.VerifyOnly);
            }
            else
            {
                container.Verify(VerificationOption.VerifyAndDiagnose);
                var result = Analyzer.Analyze(container);
            }

            return new SimpleInjectorDependencyResolver(container);
        }
    }
}
