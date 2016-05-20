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
using ProAsp.Data.DatabaseContext;
using ProAsp.Data.Repository;
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
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            container.Register(typeof(IDbContext), typeof(ProAspDbContext), Lifestyle.Scoped);
            container.Register(typeof(IRepository<>), typeof(GenericRepository<>), Lifestyle.Transient);

            (from type in typeof(IUserService).Assembly.GetExportedTypes()
                where type.Namespace == "ProAsp.Core.Services"
                where type.GetInterfaces().Any()
                select new {Service = type.GetInterfaces().Single(), Implementation = type})
                    .ForEach(obj => container.Register(obj.Service, obj.Implementation));

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
