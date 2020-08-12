//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2015 all rights reserved.
//
//  File:           CheckAccessAttribute.cs       
//  Author:         jagmohan.rawat
//  Date written:   13/07/2015/ 10:58:30 AM
//  Description:    CheckAccessAttribute page is use for checking authorization of user for particular page
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
using System.Configuration;
using System.Web.Mvc;
using Domain.Entities;
using Domain.Interfaces;
using RMS.Common;
using RMS.Common.Constants;
using RMS.Common.ExceptionHandling;
using Infrastructure.Data;

namespace RMS.Helpers
{
    /// <summary>
    /// Check whether user has access to particular module or not
    /// </summary>
    /// <returns>Collection</returns>
    public class CheckAccessAttribute : FilterAttribute, IActionFilter
    {

        private readonly ITrainingModel _service;
        int accessRightID, userEmpID;
        Master objMaster = new Master();
        
        public CheckAccessAttribute(){
            ITrainingModel service= new TrainingRepository();
            _service = service;
        }


        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {           
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                //string controller = filterContext.RouteData.Values["controller"].ToString();
                //string action = filterContext.RouteData.Values["action"].ToString();
                //this.userEmpID = objMaster.GetEmployeeIDByEmailID();                
                //accessRightID = _service.AccessForTrainingModule(userEmpID);                
                
                //if (accessRightID == 0)
                //{
                //    HttpContext.Current.Response.Redirect(ConfigurationManager.AppSettings[CommonConstants.BaseUrl]);
                //}

                string controllername = filterContext.RequestContext.RouteData.Values["controller"].ToString();
                string actionName = filterContext.RequestContext.RouteData.Values["action"].ToString();
                ObjectParameter objdisabled = new ObjectParameter("Disabled", false);

                var result = db.USP_RPL_CheckUserAccess(username, controllername, actionName, objdisabled).FirstOrDefault();


                authorize = result.Value >= 1 ? true : false;
                if (!authorize)
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }

                filterContext.ActionParameters["isDisable"] = objdisabled.Value;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.UILayer, CommonConstants.CheckAccessAttribute, CommonConstants.OnResultExecuting, EventIDConstants.ERROR_UI_LAYER);
            }
        }
        
    }
}