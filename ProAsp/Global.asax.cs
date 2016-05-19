using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using SimpleInjector.Integration.Web.Mvc;
using Container = SimpleInjector.Container;

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
