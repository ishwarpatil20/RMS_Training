using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Objects;
using System.Data.SqlClient;
using RMS.Common;
namespace RMS.Common.AuthorizationManager
{
    public class AuthorizeFilter : ActionFilterAttribute
    {
        //string username = "noaccess";
        //string username = "jagmohan.rawat";
        string username = "venkatesh.patange";
        //string username = "Ishwar.Patil";
        //string username = "Siddhesh.Arekar";
        bool authorize = false;                

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controllername = filterContext.RequestContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.RequestContext.RouteData.Values["action"].ToString();
            bool isDisabled = false;
            bool result = Master.CheckAccess(username, controllername, actionName,out isDisabled);
            
            if (!result)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }

            filterContext.ActionParameters["isDisable"] = isDisabled;
        }
    }
}
