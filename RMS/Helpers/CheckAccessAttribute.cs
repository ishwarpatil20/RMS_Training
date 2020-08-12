using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Mvc;
using Domain.Entities;
using Infrastructure.Interfaces;
using Services.Interfaces;
using Services;
using RMS.Common;
using RMS.Common.Constants;
using RMS.Common.ExceptionHandling;
using Infrastructure;
using RMS.Common.BusinessEntities.Menu;
using System.Collections;

namespace RMS.Helpers
{
    /// <summary>
    /// Check whether user has access to particular module or not
    /// </summary>
    /// <returns>Collection</returns>
    public class CheckAccessAttribute : FilterAttribute, IActionFilter
    {

        private readonly ITrainingRepository _service;
        private ICommonService _commonservice;
        private int roleID;
        //Master objMaster = new Master();       

        public CheckAccessAttribute(){
            ITrainingRepository service = new TrainingRepository();
            ICommonService commonservice = new CommonService();
            _service = service;
            _commonservice = commonservice;
        }


        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {           
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                HttpContext context = HttpContext.Current;
                if (Convert.ToString(ConfigurationManager.AppSettings["ResetRoleYesNo"]).ToLower() == "yes")
                    context.Session[AuthorizationManagerConstants.AZMAN_ROLES] = null; // VRP hardcode

                if (String.IsNullOrEmpty(Convert.ToString(context.Session["UserEmailID"])))
                {
                    context.Session["UserEmailID"] = _commonservice.getLoggedInUserEmailId();
                }

                if (String.IsNullOrEmpty(Convert.ToString(context.Session["EmpID"])))
                {
                    context.Session["EmpID"] = _commonservice.GetEmployeeID();
                }

                //if (String.IsNullOrEmpty(Convert.ToString(context.Session["_RoleName"])))
                if (context.Session[AuthorizationManagerConstants.AZMAN_ROLES] == null)
                {
                    ArrayList arrRolesForUser = new ArrayList();
                    arrRolesForUser = _commonservice.GetEmployeeRole(Convert.ToInt32(context.Session["EmpID"]));
                    arrRolesForUser = Common.Utility.ConvertArrayListLowerCase(arrRolesForUser);
                    context.Session[AuthorizationManagerConstants.AZMAN_ROLES] = arrRolesForUser;
                    //context.Session["_RoleName"] = _commonservice.GetEmployeeRole(Convert.ToInt32(context.Session["EmpID"]), out roleID);
                    //context.Session["_RoleID"] = roleID;
                }

                if (context.Session[AuthorizationManagerConstants.AZMAN_ROLES] == null)               
                {
                    HttpContext.Current.Response.Redirect(ConfigurationManager.AppSettings[CommonConstants.BaseUrl]);
                }
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

        public static string Encode(string encodeMe)
        {
            byte[] encoded = System.Text.Encoding.UTF8.GetBytes(encodeMe);
            return Convert.ToBase64String(encoded);
        }

        public static string Decode(string decodeMe)
        {
            byte[] encoded = Convert.FromBase64String(decodeMe);
            return System.Text.Encoding.UTF8.GetString(encoded);
        }
    }
}
