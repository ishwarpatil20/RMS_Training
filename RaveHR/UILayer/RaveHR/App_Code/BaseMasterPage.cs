using System;
using System.Data;
using System.Configuration;
///------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           BaseMasterPage.cs       
//  Author:         Prashant Mala
//  Date written:   03/08/2009/ 6:51:30 PM
//  Description:    This class Log error in windows event log
//                  
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  03/08/2009 6:51:30 PM  Prashant Mala    n/a     Created    
//
//------------------------------------------------------------------------------

using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Common;
using System.Diagnostics;

/// <summary>
/// Summary description for BaseMasterPage
/// </summary>
public class BaseMasterPage : System.Web.UI.MasterPage
{
    public BaseMasterPage()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    
    /// <summary>
    /// Log error in windows event log
    /// </summary>
    /// <author>Prashant Mala</author>
    /// <CreatedDate>21st Sept 2009</CreatedDate>
    /// <LastModifiedDate>21st Sept 2009</LastModifiedDate>
    /// <param name="error">RaveHRException</param>
    /// <returns>Void</returns>
    protected void LogErrorMessage(RaveHRException error)
    {
        string strErrorMessage = "";
        strErrorMessage += "Error Source :- " + "\n";
        strErrorMessage += "Source : " + error.LayerName + "\n";
        strErrorMessage += "SourceClassName : " + error.ClassName + "\n";
        strErrorMessage += "MethodName : " + error.MethodName + "\n";
        strErrorMessage += "URL : " + HttpContext.Current.Request.Url.ToString() + "\n";
        strErrorMessage += "Account Name : " + Request.LogonUserIdentity.Name + "\n\n";
        strErrorMessage += "Exception Information :- " + "\n";
        strErrorMessage += "Error Message : " + error.Message + "\n";
        strErrorMessage += "Stack Trace : " + error.ErrorStackTrace + "\n\n";

        //--Check if the event log exits
        if (!EventLog.SourceExists(CommonConstants.APPLICATIONNAME))
        {
            EventLog.CreateEventSource(CommonConstants.APPLICATIONNAME, CommonConstants.LOGGERNAME);
        }

        //--Create instance of eventlog
        EventLog eventLog = new EventLog();
        eventLog.Source = CommonConstants.APPLICATIONNAME;

        //--Add to windows event log
        eventLog.WriteEntry(strErrorMessage, EventLogEntryType.Error, error.EventID);
        //EventLog.WriteEntry(CommonConstants.APPLICATIONNAME, strErrorMessage, EventLogEntryType.Error, error.EventID);

        //--Redirect to error page
        HttpContext.Current.Response.Redirect(CommonConstants.ERROR_PAGE, false);
    }
}
