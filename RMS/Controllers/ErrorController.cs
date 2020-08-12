using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Common;
using RMS.Common.ExceptionHandling;
using RMS.Common.Constants;

namespace RMS.Controllers
{
    /// <summary>
    /// Show Error page in case of error occured
    /// </summary>
    /// <value>TrainingTypeID</value>       
    public class ErrorController : Controller
    {
        
        protected override void  OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                string controller = filterContext.RouteData.Values["controller"].ToString();
                string action = filterContext.RouteData.Values["action"].ToString();
                Exception ex = filterContext.Exception;
                filterContext.ExceptionHandled = true;
                RaveHRException exception = new RaveHRException(filterContext.Exception.Message, filterContext.Exception, Sources.ControllerLayer, controller, action, EventIDConstants.TRAINING_CONTROLLER_LAYER);
                Response.Redirect(Url.Action("showerror", "error"));
                //RedirectToAction("showerror", "error");
                //throw new RaveHRException(filterContext.Exception.Message, filterContext.Exception, Sources.ControllerLayer, controller, action, EventIDConstants.TRAINING_CONTROLLER_LAYER);                                
            }
        }


        public ActionResult ShowError()
        {
            return View("error");
        }
    }

           
}
