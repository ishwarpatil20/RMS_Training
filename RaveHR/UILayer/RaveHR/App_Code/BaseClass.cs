///------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           BaseClass.cs       
//  Author:         Prashant Mala
//  Date written:   28/04/2009/ 5:51:00 PM
//  Description:    This class Provides the base class methods
//                  
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  28/04/2009/ 5:51:00 PM  Prashant Mala    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using Common;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Common.Constants;
using Rave.HR.DataAccessLayer.Common;
/// <summary>
/// Summary description for BaseClass
/// </summary>
public class BaseClass : System.Web.UI.Page
{
    ArrayList URLCollection = new ArrayList();
    public BaseClass()
    {
        //
        // TODO: Add constructor logic here
        //
    }



    /// <summary>
    /// Log error in windows event log
    /// </summary>
    /// <author>Prashant Mala</author>
    /// <CreatedDate>14th Sept 2009</CreatedDate>
    /// <LastModifiedDate>14th Sept 2009</LastModifiedDate>
    /// <param name="error">RaveHRException</param>
    /// <returns>Void</returns>
    protected void LogErrorMessage(RaveHRException error)
    {

        // Rakesh : Issue 58027 : 19/May/2016 : Starts  

        //string strErrorMessage = "";
        //strErrorMessage += "Error Source :- " + "\n";
        //strErrorMessage += "Source : " + error.LayerName + "\n";
        //strErrorMessage += "SourceClassName : " + error.ClassName + "\n";
        //strErrorMessage += "MethodName : " + error.MethodName + "\n";
        //strErrorMessage += "URL : " + HttpContext.Current.Request.Url.ToString() + "\n";
        //strErrorMessage += "Account Name : " + Request.LogonUserIdentity.Name + "\n\n";
        //strErrorMessage += "Exception Information :- " + "\n";
        //strErrorMessage += "Error Message : " + error.Message + "\n";
        //strErrorMessage += "Stack Trace : " + error.ErrorStackTrace + "\n\n";
        
        
        //Rakesh Commented Storing Error in Event Logs Begin 18-05-2016
        
        //--Check if the event log exits
        //if (!EventLog.SourceExists(CommonConstants.APPLICATIONNAME))
        //{
          //  EventLog.CreateEventSource(CommonConstants.APPLICATIONNAME, CommonConstants.LOGGERNAME);
       // }
        
     
        //--Create instance of eventlog
       // EventLog eventLog = new EventLog();
        //eventLog.Source = CommonConstants.APPLICATIONNAME;

        //--Add to windows event log
        //eventLog.WriteEntry(strErrorMessage, EventLogEntryType.Error, error.EventID);
        //EventLog.WriteEntry(CommonConstants.APPLICATIONNAME, strErrorMessage, EventLogEntryType.Error, error.EventID);

        //End

        Master objMaster = new Master();
        objMaster.SaveError(error.Message, error.LayerName, error.ClassName, error.MethodName, error.EventID.ToString(), error.ErrorStackTrace, CommonConstants.APPLICATIONNAME);

        //End

        //--Redirect to error page
        HttpContext.Current.Response.Redirect(CommonConstants.ERROR_PAGE, false);
    }

    /// <summary>
    /// Log error in windows event log
    /// </summary>
    /// <author>Kanchan P</author>
    /// <CreatedDate>3rd July 2009</CreatedDate>
    /// <LastModifiedDate>3rd July 2009</LastModifiedDate>
    /// <param name="error">RaveHRException</param>
    /// <param name="lblMessage">label control on which error message to be displayed</param>
    /// <param name="strMessage">error message to be displayed</param>
    /// <returns>Void</returns>
    protected void LogMailErrorMessage(RaveHRException error, string ProjectName, string mode)
    {
        string strErrorMessage = "";
        //strErrorMessage += "Event ID : " + System.Guid.NewGuid().ToString() + "\n\n";
        strErrorMessage += "Error Source :- " + "\n";
        strErrorMessage += "Source : " + error.LayerName + "\n";
        strErrorMessage += "SourceClassName : " + error.ClassName + "\n";
        strErrorMessage += "MethodName : " + error.MethodName + "\n";
        strErrorMessage += "URL : " + HttpContext.Current.Request.Url.ToString() + "\n";
        strErrorMessage += "Account Name : " + Request.LogonUserIdentity.Name + "\n\n";
        strErrorMessage += "Exception Information :- " + "\n";
        strErrorMessage += "Error Message : " + error.Message + "\n";
        strErrorMessage += "Stack Trace : " + error.StackTrace + "\n\n";
        //--Add to windows event log

        if (!EventLog.SourceExists(CommonConstants.APPLICATIONNAME))
        {
            EventLog.CreateEventSource(CommonConstants.APPLICATIONNAME, CommonConstants.LOGGERNAME);
        }

        //--Create instance of eventlog
        EventLog eventLog = new EventLog();
        eventLog.Source = CommonConstants.APPLICATIONNAME;

        //--
        eventLog.WriteEntry(strErrorMessage, EventLogEntryType.Error, error.EventID);
        //EventLog.WriteEntry(CommonConstants.APPLICATIONNAME, strErrorMessage, EventLogEntryType.Error, error.EventID);
        switch (mode)
        {
            case "Update":
                Session["EmailMessage"] = "Project " + ProjectName + " is updated successfully, but a error is occurred while sending a email notification. Kindly note once the server is up, email will be sent.";
                break;

            case "Delete":
                Session["EmailMessage"] = "Project " + ProjectName + " is deleted successfully, but a error is occurred while sending a email notification. Kindly note once the server is up, email will be sent.";
                break;

            case "Reject":
                Session["EmailMessage"] = "Project " + ProjectName + " is rejected successfully, but a error is occurred while sending a email notification. Kindly note once the server is up, email will be sent.";
                break;

            case "Approve":
                Session["EmailMessage"] = "Project " + ProjectName + " is approved successfully, but a error is occurred while sending a email notification. Kindly note once the server is up, email will be sent.";
                break;

            default:
                Session["EmailMessage"] = "Project " + ProjectName + " is created successfully, but a error is occurred while sending a email notification. Kindly note once the server is up, email will be sent.";
                break;
        }

        //Redirect to Project Summary
        HttpContext.Current.Response.Redirect(CommonConstants.PROJECTSUMMARY_PAGE, false);
    }

    protected void Page_Init(object Sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BackURLCollection();
        }
    }

    /// <summary>
    /// This method collect the Back page URL.
    /// </summary>
    public void BackURLCollection()
    {
        try
        {
            string URLRefrences = string.Empty;
            if (Request.UrlReferrer != null)
            {
                URLRefrences = Convert.ToString(Request.UrlReferrer);
                string[] URLColection = URLRefrences.Split('?');
                string PreviousPageNameURL = Convert.ToString(URLColection[0]);
                string BackURLStatus = null;

                if (Request.QueryString[QueryStringConstants.BACK] != null)
                {
                    BackURLStatus = Convert.ToString(DecryptQueryString(QueryStringConstants.BACK));
                }
                bool BackURLStatusReferrer = Regex.IsMatch(URLRefrences, "&Back=true");
                if (BackURLStatusReferrer)
                {
                    string[] URLRefrencesBackCollection = Regex.Split(URLRefrences, "&Back=true");
                    URLRefrences = Convert.ToString(URLRefrencesBackCollection[0]);
                }


                if (BackURLStatus == null)
                {
                    if (Session["URLCOLLECTION"] != null)
                    {
                        URLCollection = (ArrayList)Session["URLCOLLECTION"];
                        int URLCount = URLCollection.Count - 1;

                        if (URLCount != -1)
                        {
                            string LastURL = (Convert.ToString(URLCollection[URLCount]));
                            string[] LastURLColection = LastURL.Split('?');
                            string LastURLAbsolute = Convert.ToString(LastURLColection[0]);
                            if (PreviousPageNameURL != LastURLAbsolute)
                            {
                                URLCollection.Add(URLRefrences);
                                Session["URLCOLLECTION"] = URLCollection;
                            }
                        }
                        else
                        {
                            URLCollection.Add(URLRefrences);
                            Session["URLCOLLECTION"] = URLCollection;
                        }
                    }
                    else
                    {
                        URLCollection.Add(URLRefrences);
                        Session["URLCOLLECTION"] = URLCollection;
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    /// <summary>
    /// This method redirect to back page.
    /// </summary>
    public void BackPageRedirection()
    {
        try
        {
            if (Session["URLCOLLECTION"] != null)
            {
                URLCollection = (ArrayList)Session["URLCOLLECTION"];
                int URLCount = URLCollection.Count - 1;
                string RedirectBackURL = (Convert.ToString(URLCollection[URLCount]));
                string[] RedirectBackURLSplit = RedirectBackURL.Split('?');
                if (RedirectBackURLSplit.Length == 1)
                {
                    Response.Redirect(RedirectBackURL + "?Back=true", false);
                }
                else
                {
                    Response.Redirect(RedirectBackURL + "&Back=true", false);
                }

                URLCollection.RemoveAt(URLCount);
                Session["URLCOLLECTION"] = URLCollection;
            }

        }
        catch (Exception ex)
        {

        }
    }

    private int GenerateId()
    {
        byte[] buffer = Guid.NewGuid().ToByteArray();
        return BitConverter.ToUInt16(buffer, 0);
    }

    protected bool ValidateURL()
    {
        int lengthOfParts;

        if (Request.QueryString.Count != 0)

            lengthOfParts = Request.QueryString.Count - 1;

        else

            lengthOfParts = Request.QueryString.Count;

        string[] parts = new string[lengthOfParts];

        int counter = 0;

        if (parts.Length > 0)
        {
            foreach (string qval in Request.QueryString)
            {
                if (qval != CommonConstants.SIGNATURE)
                {
                    parts[counter] = URLHelper.Clarify(Request.QueryString[qval]);

                    counter++;
                }
            }
            return URLHelper.ValidateSignature(Request.QueryString[CommonConstants.SIGNATURE], parts);
        }
        else
        {
            return true;
        }
    }

    protected List<BusinessEntities.URLHelperEntity> DecryptQueryString()
    {
        List<BusinessEntities.URLHelperEntity> lstURLHelperEntity = new List<BusinessEntities.URLHelperEntity>();

        foreach (string qVal in Request.QueryString)
        {
            BusinessEntities.URLHelperEntity objEntity = new BusinessEntities.URLHelperEntity();
            //This will qive the QueryString Name
            objEntity.QueryStringName = qVal;

            //This will qive the QueryString Decrypted Value
            objEntity.QueryStringValue = URLHelper.Clarify(Convert.ToString(Request.QueryString[objEntity.QueryStringName]));

            lstURLHelperEntity.Add(objEntity);
        }
        return lstURLHelperEntity;
    }

    public string DecryptQueryString(string queryValue)
    {
        string returnValue = string.Empty;

        returnValue = URLHelper.Clarify(Convert.ToString(Request.QueryString[queryValue]));

        return returnValue;
    }
}
