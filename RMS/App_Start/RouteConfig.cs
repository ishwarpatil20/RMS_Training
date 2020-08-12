using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
                      
            //routes.MapRoute(
            //    name: "Result",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Assessment", action = "Result", id = "1207" }//ApproveRejectTrainingRequest

            //);
          //  routes.MapRoute(
          //    name: "TrainingPlanned",
          //    url: "{controller}/{action}/{id}",
          //    defaults: new { controller = "TrainingPlan", action = "ViewTrainingPlanned", id = UrlParameter.Optional }//ApproveRejectTrainingRequest
          //);

            //routes.MapRoute(
            //    name: "ViewEmployeeTillBilled",
            //    url: "{controller}/{action}/{id}",
            //      defaults: new { controller = "Employee", action = "ViewEmployeeTillBilled", id = UrlParameter.Optional }//ApproveRejectTrainingRequest
            //);

            routes.MapRoute(
              name: "TrainingModule",
              url: "{controller}/{action}/{id}",
              defaults: new { controller = "training", action = "TrainingModule", id = UrlParameter.Optional }
          );

            routes.MapRoute(
            "Default1",
            "Special/{controller}/abc/{action}/{id}",
            new { controller = "training", action = "ViewTechnicalTrainingRequest", id = UrlParameter.Optional },
            new[] { "UrlRoutingDemo.training" }
           );


            //routes.MapRoute(
            //    name: "raisetraining",
            //    url: "abc/{controller}/{action}/{id}",
            //    defaults: new { controller = "training", action = "ViewTechnicalTrainingRequest", id = "1207" }//ApproveRejectTrainingRequest
            //);


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Nomination", action = "MailToAdmin", id = UrlParameter.Optional }
            );

         //   routes.MapRoute(
         //    name: "training",
         //    url: "{controller}/{action}/{RaiseId}/{trainingtypeID}/{operation}",
         //    defaults: new
         //    {
         //        controller = "training",
         //        action = "EditRaiseTrainingRequest",
         //        RaiseId = UrlParameter.Optional,
         //        trainingtypeid = UrlParameter.Optional,
         //        operation = UrlParameter.Optional
         //    }
         //);

            routes.MapRoute(
             name: "Trainingcourse",
             url: "{controller}/{action}/{courseId}",
             defaults: new { controller = "Trainingcourse", action = "EditCourse", courseId = UrlParameter.Optional }
             );

            routes.MapRoute(
                name: "LoadSemiKSSName",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Training", action = "LoadSemiKSSName", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}