//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2015 all rights reserved.
//
//  File:           ErrorController.cs       
//  Author:         jagmohan.rawat
//  Date written:   13/07/2015/ 10:58:30 AM
//  Description:    Error controller page is use for displaying error page
//
//  Amendments
//  Date                        Who                 Ref     Description
//  ----                        -----------         ---     -----------
//  13/07/2015/ 10:58:30 AM     jagmohan.rawat      n/a     Created    
//
//------------------------------------------------------------------------------

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
                RedirectToAction("showerror", "error");
                throw new RaveHRException(filterContext.Exception.Message, filterContext.Exception, Sources.ControllerLayer, controller, action, EventIDConstants.TRAINING_CONTROLLER_LAYER);                                
            }
        }


        public ActionResult ShowError()
        {
            return View("error");
        }
    }

           
}
