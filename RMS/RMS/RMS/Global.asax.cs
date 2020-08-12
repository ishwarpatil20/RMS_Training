using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject.Web.Common;
using Ninject;
using Ninject.Web.Mvc;
using RMS;
using System.Reflection;
using Infrastructure.Data;
using Domain.Interfaces;

namespace RMS
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override IKernel CreateKernel()
        {
           var kernel = new StandardKernel();
           kernel.Load(Assembly.GetExecutingAssembly());
           kernel.Bind<ITrainingModel>().To<TrainingRepository>();
           return kernel;
        }

        protected override void OnApplicationStarted()
        {

            base.OnApplicationStarted();
            
            AreaRegistration.RegisterAllAreas();
            
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
        }
    }
}