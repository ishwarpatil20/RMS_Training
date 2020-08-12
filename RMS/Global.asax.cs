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
using Infrastructure;
using Infrastructure.Interfaces;
using Services;
using Services.Interfaces;
using RMS.ModelBinder;
using Domain.Entities;

namespace RMS
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            kernel.Bind<ITrainingRepository>().To<TrainingRepository>();
            kernel.Bind<ITrainingService>().To<TrainingService>();
            kernel.Bind<ICommonService>().To<CommonService>();
            kernel.Bind<ICommonRepository>().To<CommonRepository>();
            kernel.Bind<IMasterService>().To<MasterService>();
            kernel.Bind<IMasterRepository<T_Master>>().To<MasterRepository>();

            kernel.Bind<IAssessmentService>().To<AssessmentService>();
            kernel.Bind<IAssessmentRepository>().To<AssessmentRepository>();
            
            
            //Rakesh : Actual vs Budget  Begin 
            kernel.Bind<IOtherMasterService>().To<OtherMasterService>();
            kernel.Bind<IOtherMasterRepository>().To<OtherMasterRepository>();
            kernel.Bind<IBudgetService>().To<BudgetService>();
            kernel.Bind<IBudgetRepository>().To<BudgetRepository>();
            //Rakesh : Actual vs Budget  End

            //Venkatesh : Interview Panel Begin
            kernel.Bind<IInterviewPanelService>().To<InterviewPanelService>();
            kernel.Bind<IInterviewPanelRepository<T_InterviewPanel>>().To<InterviewPanelRepository>();
            kernel.Bind<IEmployeeRepository>().To<EmployeeRepository>();
            kernel.Bind<IEmployeeService>().To<EmployeeService>();
            kernel.Bind<IIPanelLevelRepository<T_IP_PanelLevel>>().To<IPanelLevelRepository>();
            kernel.Bind<IIPanelDesignationRepository<T_IP_PanelDesignation>>().To<IPanelDesignationRepository>();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            //Venkatesh : Interview Panel End


            //Rakesh : Insert Daily Util Billing
            kernel.Bind<ISubDomainRepository<T_SubDomain>>().To<SubDomainRepository>();
            kernel.Bind<IDomainRepository<T_Domain>>().To<DomainRepository>();
            //End

            kernel.Bind<IDailyUtilBillingService>().To<DailyUtilBillingService>();

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

        protected void Application_Start()
        {
            ModelBinders.Binders.Add(typeof(TrainingCourseModel), new EditCourseModelBinder());
            ModelBinders.Binders.Add(typeof(TrainingCourseModel), new CoursePaymentModelBinder());


            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
         //   routes.MapRoute(
         //     name: "raisetraining",
         //     url: "{controller}/{action}/{id}",
         //     defaults: new { controller = "training", action = "ViewTechnicalTrainingRequest", id = "1207" }//ApproveRejectTrainingRequest
         // );

         //   routes.MapRoute(
         //      name: "training",
         //      url: "{controller}/{action}/{RaiseId}/{trainingtypeID}/{operation}",
         //      defaults: new
         //      {
         //          controller = "training",
         //          action = "EditRaiseTrainingRequest",
         //          RaiseId = UrlParameter.Optional,
         //          trainingtypeID = UrlParameter.Optional,
         //          operation = UrlParameter.Optional
         //      }
         //  );

         //   routes.MapRoute(
         //    name: "Trainingcourse",
         //    url: "{controller}/{action}/{courseId}",
         //    defaults: new { controller = "Trainingcourse", action = "EditCourse", courseId = UrlParameter.Optional }
         //);
        }
    }
}