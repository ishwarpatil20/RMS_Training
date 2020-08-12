//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           AddProject.aspx.cs       
//  Author:         gaurav.thakkar
//  Date written:   04/01/2009 10:00:00 AM
//  Description:    The Add Project page adds a new project to the system. It is also used to view,update,   //                    delete,approve or reject a project.
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  04/01/2009 10:00:00 AM  gaurav.thakkar    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Mail;
using Common;
using System.Xml;
using System.DirectoryServices;
using Common.AuthorizationManager;
using System.Text;
using Common.Constants;
using System.Text.RegularExpressions;


public partial class AddProject : BaseClass
{

    #region Private Field Members

    /// <summary>
    /// event name to capture event from query string
    /// </summary>
    string mode = string.Empty;

    /// <summary>
    /// capture projectid from query string
    /// </summary>
    int projectId;

    /// <summary>
    /// the dateformat of Date.
    /// </summary>
    private const string dateFormat = "dd/MM/yyyy";

    private const string SELECT = "Select";

    private const string CLASS_NAME = "AddProject.aspx";

    string SortExpression;
    string SortDir;

    string strContentProjectUpdated;
    string strSubjectProjectUpdated;

    string[] UserName;
    string FinalUserName;
    string UserMailId;
    string UserRaveDomainId;
    string UserDisplayName;
    string RPMEmailId;
    string[] COOUser;
    string COOEmailId;
    string[] RPMUser;

    DataSet dsProjDetails = new DataSet();
    string[] COOFirstName;
    string COODisplayName;
    string CurrentDateTime;

    string ProjectName;

    public string PMRole;
    public string PresalesRole;
    public string COORole;
    public string RPMRole;
    public string Action;
    public string[] RPMDisplayName;
    public string[] RPMDisplay;
    public string RPMFinalDisplayName;
    ArrayList arrRolesForUser = new ArrayList();

    public string EMailIdOfPM;
    string projectsummaryprojectname;

    int I;

    XmlDocument xml = new XmlDocument();
    string xmlPath = HttpContext.Current.Request.PhysicalApplicationPath + System.Configuration.ConfigurationSettings.AppSettings["MailXml"];

    string clientNm;
    int projectStatusId;

    int pageCount = 0;

    /// <summary>
    /// Rave Email domain
    /// </summary>
    /// 
    //Googleconfigurable
    //private const string RAVE_DOMAIN = "@rave-tech.com";


    private const int PAGE_SIZE = 10;

    string TempURL;
    string[] ArrTempURL;

    /// <summary>
    /// Define Constants for new Domain,SubDomain,Category and Technolgy
    /// </summary>
    private int _domainId = 0;
    private int _subDomainId = 0;
    private int _categoryId = 0;
    private int _techNologyId = 0;
    private string _numericExpression = "^[0-9]*$";

    #endregion Private Field Members

    #region Publec Field Members

    public enum Mode
    {
        View,
        Update
    }

    /// <summary>
    /// Defines enum for Project Status
    /// </summary>
    public enum ProjectStatus
    {
        Delivery,
        Closed,
        PreSales,

        //Deleted
    }

    /// <summary>
    /// Defines enum for ResourcePlanMode 
    /// </summary>
    public enum ResourcePlanMode
    {
        CreateResourcePlan,
        ViewResourcePlan,
        EditResourcePlan,
    }

    public string IsFiltered;

    #endregion Publec Field Members

    #region Protected Events

    /// <summary>
    /// On Page load, dropdownlist is filled
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    protected void Page_Load(object sender, EventArgs e)
    {
        ucDatePickerStartDate.TextBox.Width = 150;
        ucDatePickerEndDate.TextBox.Width = 150;


        if (!ValidateURL())
        {
            Response.Redirect(CommonConstants.INVALIDURL, false);
        }
        else
        {
            char separator = Convert.ToChar(System.Configuration.ConfigurationManager.AppSettings["SplitCharacter"]);

            CurrentDateTime = DateTime.Now.ToString("dd-MMM-yyyy");
            if (Request.QueryString[QueryStringConstants.CLIENTNAME] != null)
                clientNm = DecryptQueryString(QueryStringConstants.CLIENTNAME);

            if (Request.QueryString[QueryStringConstants.PROJECTSUMMARYPROJECTNAME] != null)
                projectsummaryprojectname = DecryptQueryString(QueryStringConstants.PROJECTSUMMARYPROJECTNAME);

            if (Request.QueryString[QueryStringConstants.PROJECTSTATUSID] != null)
                projectStatusId = Convert.ToInt32(DecryptQueryString(QueryStringConstants.PROJECTSTATUSID));
            try
            {

                mode = DecryptQueryString(QueryStringConstants.MODE);
                if (DecryptQueryString(QueryStringConstants.FILTERAPPLIED) != null)
                {
                    IsFiltered = DecryptQueryString(QueryStringConstants.FILTERAPPLIED);
                }
                else
                {
                    IsFiltered = "";
                }

                // Get logged-in user's email id
                AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
                UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
                UserMailId = UserRaveDomainId.Replace("co.in", "com");
                UserName = UserMailId.Split('@');
                if (UserName[0].Contains(separator.ToString()))
                {
                    UserName = UserName[0].Split(separator);
                    for (int i = 0; i < UserName.Length; i++)
                    {
                        UserName[i] = ConvertToUpper(UserName[i]);
                        UserDisplayName += UserName[i];

                        if (i < UserName.Length - 1)
                            UserDisplayName += " ";
                    }
                }
                else
                {
                    FinalUserName = ConvertToUpper(UserName[0]);
                    UserDisplayName = FinalUserName;
                }
                AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
                arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());

                foreach (string STR in arrRolesForUser)
                {
                    switch (STR)
                    {
                        case AuthorizationManagerConstants.ROLEPRESALES:
                            PresalesRole = AuthorizationManagerConstants.ROLEPRESALES;
                            break;
                        case AuthorizationManagerConstants.ROLEPROJECTMANAGER:
                            PMRole = AuthorizationManagerConstants.ROLEPROJECTMANAGER;
                            break;
                        case AuthorizationManagerConstants.ROLECOO:
                            COORole = AuthorizationManagerConstants.ROLECOO;
                            break;
                        case AuthorizationManagerConstants.ROLERPM:
                            RPMRole = AuthorizationManagerConstants.ROLERPM;
                            break;
                        default:
                            break;
                    }
                }

                //Find RPM's mailid
                RPMEmailId = "";
                RPMUser = Roles.GetUsersInRole(AuthorizationManagerConstants.ROLERPM);
                if (RPMUser.Length != 0)
                {
                    for (I = 0; I < RPMUser.Length; I++)
                    {
                        if (RPMEmailId == "")
                        {
                            RPMEmailId = GetDomainUsers(RPMUser[I]);
                        }
                        else
                        {
                            RPMEmailId = RPMEmailId + "," + GetDomainUsers(RPMUser[I]);
                        }
                    }

                    RPMDisplay = RPMEmailId.Split('.');
                    RPMDisplayName = RPMDisplay[1].Split('@');
                    RPMFinalDisplayName = ConvertToUpper(RPMDisplay[0]) + " " + ConvertToUpper(RPMDisplayName[0]);
                }

                //Find COO's mailid
                COOUser = Roles.GetUsersInRole(AuthorizationManagerConstants.ROLECOO);
                if (COOUser.Length != 0)
                {
                    COOFirstName = COOUser[0].Split('.');
                    COODisplayName = ConvertToUpper(COOFirstName[0]);
                    //COODisplayName = ConvertToUpper(COOFirstName[0]);
                    COOEmailId = GetDomainUsers(COOUser[0]);
                }

                XmlDocument xmlTest = new XmlDocument();
                xmlTest.Load(xmlPath);

                XmlNode nodeContentProjectUpdated = xmlTest.SelectSingleNode("MailMessage//Project_Updated//Content");
                strContentProjectUpdated = nodeContentProjectUpdated.InnerXml;
                XmlNode nodeSubjectProjectUpdated = xmlTest.SelectSingleNode("MailMessage//Project_Updated//Subject");
                strSubjectProjectUpdated = nodeSubjectProjectUpdated.InnerXml;

                if (!IsPostBack)
                {
                    SessionClear();
                    GetMasterData_StatusDropDown();
                    ddlStatus.Items.FindByText(ProjectStatus.Closed.ToString()).Enabled = false;
                    //ddlStatus.Items.FindByText(ProjectStatus.Deleted.ToString()).Enabled = false;
                    if ((Roles.IsUserInRole(AuthorizationManagerConstants.ROLEPRESALES) && Roles.IsUserInRole(AuthorizationManagerConstants.ROLEPROJECTMANAGER)) || (Roles.IsUserInRole(AuthorizationManagerConstants.ROLEPRESALES) && Roles.IsUserInRole(AuthorizationManagerConstants.ROLERPM)))
                    {
                        ddlStatus.Items.FindByText(ProjectStatus.Delivery.ToString()).Enabled = true;
                    }
                    else if (Roles.IsUserInRole(AuthorizationManagerConstants.ROLEPRESALES))
                    {
                        ddlStatus.Items.FindByText(ProjectStatus.Delivery.ToString()).Enabled = false;
                    }
                    GetMasterData_LocationDropDown();

                    //Siddharth 12th March 2015 Start
                    GetMasterData_ProjectModelDropDown();
                    //Siddharth 12th March 2015 End

                    //Siddharth 3rd Aug 2015 Start
                    GetMasterData_BusinessVerticalDropDown();
                    //Siddharth 3rd Aug 2015 End

                    GetMasterData_ProjectGroupDropDown();
                    GetMasterData_ProjectStdHoursDropDown();
                    // Mohamed : Issue  : 29/09/2014 : Starts                        			  
                    // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
                    GetMasterData_ProjectDivisionDropDown();
                    GetMasterData_ProjectBussinessAreaDropDown();
                    GetMasterData_ProjectBussinessSegmentDropDown();
                    // Mohamed : Issue  : 29/09/2014 : Ends
                    categoryDropDown();
                    domainDropDown();

                    //Rakesh : HOD for Employees 11/07/2016 Begin   
                    BindProject_Head_Dropdown();
                    //Rakesh : HOD for Employees 11/07/2016 End   

                    ddlTechnology.Items.Insert(0, "Select");
                    ddlSubDomain.Items.Insert(0, "Select");
                    GetOnGoingProjectStatus_Dropdown();

                    //Code added by kanchan to handle maxlength property for Summary and Project Description textarea
                    txtDescription.Attributes.Add("onKeypress", "return fnValidateProjDesc('" + txtDescription.ClientID + "',500);");

                    txtProjectName.Focus();

                    //Code added by Gaurav to handle the previous URL on Click of the Cancel Button 
                    if (Request.UrlReferrer != null)
                    {
                        mode = DecryptQueryString(QueryStringConstants.MODE);
                        if (mode == Mode.Update.ToString())
                        {
                            string previousURL = Request.UrlReferrer.OriginalString;
                            string previousProjectID = Request.UrlReferrer.Query.Substring(11, Request.UrlReferrer.Query.IndexOf("&") - 11);
                            string currentProjectId = DecryptQueryString(QueryStringConstants.PROJECTID);

                            ViewState["PreviousURL"] = previousURL.Replace(String.Format("ProjectID={0}", previousProjectID), String.Format("ProjectID={0}", currentProjectId));
                        }
                    }
                }

                if (mode == Mode.View.ToString())
                {
                    Action = Mode.View.ToString();
                    Page.Title = "View Project";

                    pnlTechnologyControls.Visible = false;
                    pnlDomainControls.Visible = false;

                    lblMandatory.Visible = false;

                    //method to get projectId,sort Expression and Direction
                    GetProjectIdSortExpAndDirection();

                    if (!IsPostBack)
                    {
                        ViewProjectDetails(projectId, SortDir, SortExpression, UserMailId, PresalesRole, PMRole, COORole, RPMRole, Action, clientNm, projectStatusId, projectsummaryprojectname);
                        GetProjectSummaryList(projectId);
                    }

                    btnSave.Visible = false;

                    if (Roles.IsUserInRole(AuthorizationManagerConstants.ROLECOO))
                    {
                        /*mode = Request.QueryString["Mode"];
                        if (mode == Mode.ApproveReject.ToString())
                        {
                            btnApprove.Visible = true;
                            btnReject.Visible = true;
                        }*/
                    }

                    if ((Roles.IsUserInRole(AuthorizationManagerConstants.ROLEPROJECTMANAGER)) || (Roles.IsUserInRole(AuthorizationManagerConstants.ROLEPRESALES)))
                    {
                        /*if (hidWorkflowStatus.Value == "Pending Approval Of COO")
                        {
                            btnEdit.Visible = false;
                        }
                        else
                        {
                            btnEdit.Visible = true;
                        }*/
                    }

                    if (Roles.IsUserInRole(AuthorizationManagerConstants.ROLECOO))
                    {
                        btnEdit.Visible = false;
                    }

                    if (Roles.IsUserInRole(AuthorizationManagerConstants.ROLERPM))
                    {
                        btnEdit.Visible = true;
                    }
                    else if (Roles.IsUserInRole(AuthorizationManagerConstants.ROLEPM))
                    {
                        btnEdit.Visible = true;

                    }
                    else if (Roles.IsUserInRole(AuthorizationManagerConstants.ROLEPRESALES))
                    {
                        btnEdit.Visible = true;
                    }

                    ViewResourcePlan(mode, projectId);

                    /*if (Roles.IsUserInRole(AuthorizationManagerConstants.ROLERPM))
                    {
                        if (hidWorkflowStatus.Value == "Pending Approval Of COO")
                        {
                            btnEdit.Visible = false;
                            btnDelete.Visible = false;
                            btnApprove.Visible = false;
                            btnReject.Visible = false;
                        }
                        else
                        {
                            btnEdit.Visible = true;
                            if (txtStatus.Text == ProjectStatus.Deleted.ToString())
                            {
                                btnDelete.Visible = false;
                            }
                            else
                            {
                                btnDelete.Visible = true;
                            }
                        }
                        if (hidWorkflowStatus.Value == WorkflowStatus.Approved.ToString() || hidWorkflowStatus.Value == WorkflowStatus.Rejected.ToString())
                        {
                            btnApprove.Visible = false;
                            btnReject.Visible = false;
                        }
                    }*/

                }

                if (mode == Mode.Update.ToString())
                {
                    Action = Mode.Update.ToString();
                    Page.Title = "Update Project";
                    txtProjectID.Enabled = false;
                    txtDescription.ReadOnly = false;

                    //txtEndDate.Attributes.Add("readonly", "readonly");


                    if (Roles.IsUserInRole(AuthorizationManagerConstants.ROLERPM))
                    {
                        ddlStatus.Items.FindByText(ProjectStatus.Closed.ToString()).Enabled = true;

                    }
                    if (Roles.IsUserInRole(AuthorizationManagerConstants.ROLEPRESALES) && Roles.IsUserInRole(AuthorizationManagerConstants.ROLEPROJECTMANAGER))
                    {
                        ddlStatus.Items.FindByText(ProjectStatus.Delivery.ToString()).Enabled = true;
                    }

                    //method to get projectId,sort Expression and Direction
                    GetProjectIdSortExpAndDirection();

                    if (!IsPostBack)
                    {
                        UpdateProjectDetails(projectId, SortDir, SortExpression, UserMailId, PresalesRole, PMRole, COORole, RPMRole);
                    }

                    CreateResourcePlan(mode, projectId);

                    txtClientName.Enabled = false;
                    txtProjectCode.Enabled = false;
                    txtProjectName.Enabled = false;

                    //txtStartDate.Enabled = false;
                    //ucDatePickerStartDate.IsEnable = false;
                    //ucDatePickerEndDate.IsEnable = false;
                    txtLocation.Enabled = false;
                }
            }
            catch (RaveHRException ex)
            {
                LogErrorMessage(ex);
            }
            catch (Exception ex)
            {
                RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "Page_Load", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
                LogErrorMessage(objEx);
            }
        }
    }

    // Mohamed : Issue  : 29/09/2014 : Starts                        			  
    // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
    /// <summary>
    ///  method to get value of ProjectDivisionDropDown
    /// </summary>
    private void GetMasterData_ProjectDivisionDropDown()
    {
        try
        {
            List<BusinessEntities.Master> objRaveHRMaster = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            objRaveHRMaster = objRaveHRMasterBAL.GetMasterData((Convert.ToInt32(EnumsConstants.Category.ProjectDivision)).ToString());
            ddlPrjDivision.DataSource = objRaveHRMaster;
            ddlPrjDivision.DataTextField = "MasterName";
            ddlPrjDivision.DataValueField = "MasterID";
            ddlPrjDivision.DataBind();
            ddlPrjDivision.Items.Insert(0, "Select");
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "GetMasterData_ProjectDivisionDropDown", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    ///  method to get value of ProjectBussinessAreaDropDown
    /// </summary>
    private void GetMasterData_ProjectBussinessAreaDropDown()
    {
        try
        {
            List<BusinessEntities.Master> objRaveHRMaster = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            objRaveHRMaster = objRaveHRMasterBAL.GetMasterData((Convert.ToInt32(EnumsConstants.Category.ProjectBussinessArea)).ToString());
            ddlProjecBusinessArea.DataSource = objRaveHRMaster;
            ddlProjecBusinessArea.DataTextField = "MasterName";
            ddlProjecBusinessArea.DataValueField = "MasterID";
            ddlProjecBusinessArea.DataBind();
            ddlProjecBusinessArea.Items.Insert(0, "Select");
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "GetMasterData_ProjectBussinessAreaDropDown", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    ///  method to get value of ProjectBussinessSegmentDropDown
    /// </summary>
    private void GetMasterData_ProjectBussinessSegmentDropDown()
    {
        try
        {
            List<BusinessEntities.Master> objRaveHRMaster = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            objRaveHRMaster = objRaveHRMasterBAL.GetMasterData((Convert.ToInt32(EnumsConstants.Category.ProjectBussinessSegment)).ToString());
            ddlProjectBusinessSegment.DataSource = objRaveHRMaster;
            ddlProjectBusinessSegment.DataTextField = "MasterName";
            ddlProjectBusinessSegment.DataValueField = "MasterID";
            ddlProjectBusinessSegment.DataBind();
            ddlProjectBusinessSegment.Items.Insert(0, "Select");
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "GetMasterData_ProjectBussinessSegmentDropDown", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    // Mohamed : Issue  : 29/09/2014 : Ends

    /// <summary>
    /// Get resourcePlanId by projectId
    /// </summary>
    private BusinessEntities.RaveHRCollection GetResourcePlanIdByProjectId(string mode, int projectId)
    {
        //declare BusinessEntities.ResourcePlan object
        BusinessEntities.ResourcePlan objBEResourcePlan = new BusinessEntities.ResourcePlan();

        if (projectId != 0)
        {
            //assign projectId 
            objBEResourcePlan.ProjectId = projectId;
        }
        else
        {
            //assign projectId for querystring.
            objBEResourcePlan.ProjectId = int.Parse(DecryptQueryString(QueryStringConstants.PROJECTID));
        }
        objBEResourcePlan.Mode = mode;
        //declare Rave.HR.BusinessLayer.Projects.ResourcePlan object
        Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLResourcePlan = new Rave.HR.BusinessLayer.Projects.ResourcePlan();

        //call method to get resource plan count by project id
        return (objBLLResourcePlan.GetResourcePlanForProjectId(objBEResourcePlan));

    }

    /// <summary>
    /// method to get project summary list
    /// </summary>
    private void GetProjectSummaryList(int currentProjectID)
    {
        if (Session["pageCount"] != null)
        {
            pageCount = Convert.ToInt32(Session["pageCount"].ToString());
        }
        Rave.HR.BusinessLayer.Projects.Projects objProjectSummaryBAL = new Rave.HR.BusinessLayer.Projects.Projects();
        DataTable dt = new DataTable();

        BusinessEntities.Master objGetProjectStatus = new BusinessEntities.Master();
        BusinessEntities.Projects objGetProjectSummary = new BusinessEntities.Projects();

        try
        {
            //If any of the session is null or Contains text "Select" get the page load data
            if ((Session["ProjectSummaryProjectName"] == null || Session["ProjectSummaryProjectName"].ToString() == string.Empty) && (Session["ClientName"] == null || Session["ClientName"].ToString() == SELECT) && (Session["ProjectStatus"] == null || Session["ProjectStatus"].ToString() == SELECT) && (Session["WorkFlowStatus"] == null || Session["WorkFlowStatus"].ToString() == SELECT))
            {
                dt = objProjectSummaryBAL.GetUnfilteredProjectSummaryList(UserMailId, COORole, PresalesRole, PMRole, RPMRole);
            }
            //Else get the filtered data based upon filter critera selection
            else
            {
                if (Session["ProjectSummaryProjectName"] == null || Session["ProjectSummaryProjectName"].ToString() == string.Empty)
                {
                    objGetProjectSummary.ProjectName = null;
                }
                else
                {
                    objGetProjectSummary.ProjectName = Session["ProjectSummaryProjectName"].ToString();
                }
                if (Session["ClientName"] == null || Session["ClientName"].ToString() == SELECT)
                {
                    objGetProjectSummary.ClientName = null;
                }
                else
                {
                    objGetProjectSummary.ClientName = Session["ClientName"].ToString();
                }

                if (Session["ProjectStatus"] == null || Session["ProjectStatus"].ToString() == SELECT || Session["ProjectStatus"].ToString() == "")
                {
                    objGetProjectStatus.MasterId = 0;
                }
                else
                {
                    objGetProjectStatus.MasterId = Convert.ToInt32(Session["ProjectStatus"].ToString());
                }

                dt = objProjectSummaryBAL.GetFilteredProjectSummaryList(objGetProjectSummary, objGetProjectStatus, UserMailId, COORole, PresalesRole, PMRole, RPMRole);
            }

            DataView dv = new DataView(dt);

            //string sortExpr = string.Empty;
            //switch (hidSortExpr.Value)
            //{
            //    case "iProjectID":
            //        sortExpr = "ProjectID";
            //        break;
            //    case "strProjectCode":
            //        sortExpr = "ProjectCode";
            //        break;
            //    case "strProjectName":
            //        sortExpr = "ProjectName";
            //        break;
            //    case "strClientName":
            //        sortExpr = "ClientName";
            //        break;
            //    case "strProjectStatus":
            //        sortExpr = "ProjectStatus";
            //        break;
            //}

            dv.Sort = hidSortExpr.Value + " " + hidSortDir.Value + ", ProjectID " + hidSortDir.Value;
            dt = dv.ToTable("dt");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int projectID = Convert.ToInt32(dt.Rows[i]["ProjectID"]);
                if (projectID.Equals(currentProjectID))
                {
                    if (i < dt.Rows.Count - 1)
                    {
                        hidNextProjectId.Value = dt.Rows[i + 1]["ProjectID"].ToString();
                        btnNext.Visible = true;
                    }
                    else
                        btnNext.Visible = false;

                    if (i > 0)
                    {
                        hidPrevProjectId.Value = dt.Rows[i - 1]["ProjectID"].ToString();
                        btnPrevious.Visible = true;
                    }
                    else
                    {
                        btnPrevious.Visible = false;
                    }
                }
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetProjectSummaryList", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// method to get domain users
    /// </summary>
    public string GetDomainUsers(string strUserName)
    {
        //strUserName = strUserName.Replace("@rave-tech.co.in", "");

        string strUserEmail = string.Empty;
        AuthorizationManager obj = new AuthorizationManager();

        //string strExcludeList = string.Empty;
        //strExcludeList = ConfigurationManager.AppSettings["QualityTeamReportsAccess"].ToString();
        //GoogleMail
        string domainName = string.Empty;
        obj.GetWindowsUsernameAsPerNorthgate(strUserName, out domainName);

        strUserEmail = strUserName + "@" + domainName;

        //Google change point to northgate
        //if (strUserName.ToLower().Contains("@rave-tech.co.in"))
        //{
        //    strUserName = strUserName.Replace("@rave-tech.co.in", "");
        //    //strUserEmail = strUserName + "@" + AuthorizationManagerConstants.RAVEDOMAINEMAIL;
        //    //strUserName = obj.GetWindowsUsernameAsPerNorthgate(strUserName);
        //    //if (Session["WindowsUsername"] == null)
        //    //{
        //    UserMailId = obj.GetWindowsUsernameAsPerNorthgate(strUserName);
        //    //}
        //    //else
        //    //{
        //     //   UserMailId = Session["WindowsUsername"].ToString().Trim();
        //    //}
        //    strUserEmail = strUserName + "@" + AuthorizationManagerConstants.RAVEDOMAINEMAIL;

        //}
        //else
        //{
        //    strUserName = strUserName.Replace(AuthorizationManagerConstants.NORTHGATEDOMAIN, "");
        //    //strUserEmail = strUserName + "@" + AuthorizationManagerConstants.NORTHGATEDOMAINEMAIL;
        //    //strUserName = obj.GetWindowsUsernameAsPerNorthgate(strUserName);
        //    //if (Session["WindowsUsername"] == null)
        //    //{
        //    UserMailId = obj.GetWindowsUsernameAsPerNorthgate(strUserName);
        //    //}
        //    //else
        //    //{
        //    //    UserMailId = Session["WindowsUsername"].ToString().Trim();
        //    //}

        //    strUserEmail = strUserName + "@" + AuthorizationManagerConstants.NORTHGATEDOMAINEMAIL;
        //}

        //DirectoryEntry searchRoot = new DirectoryEntry("LDAP://RAVE-TECH.CO.IN");

        //DirectorySearcher search = new DirectorySearcher(searchRoot);

        ////string query = "(|(objectCategory=group)(objectCategory=user)) ";
        //string query = "(SAMAccountName=" + strUserName + ")";

        //search.Filter = query;

        //SearchResult result;

        //SearchResultCollection resultCol = search.FindAll();

        //if (resultCol != null)
        //{

        //    for (int counter = 0; counter < resultCol.Count; counter++)
        //    {

        //        result = resultCol[counter];

        //        if (result.Properties.Contains("samaccountname"))
        //        {

        //            //string strAccountName = result.Properties["samaccountname"][0].ToString();

        //            //if (strAccountName.ToLower() == strUserName.ToLower())
        //            {

        //                strUserEmail = result.Properties["mail"][0].ToString();

        //            }

        //        }

        //    }

        //}

        return strUserEmail;
    }

    /// <summary>
    /// method to convert to upper case
    /// </summary>
    public string ConvertToUpper(string InputString)
    {
        InputString = InputString.Substring(0, 1).ToUpper() + InputString.Substring(1, InputString.Length - 1);
        return InputString;
    }

    /// <summary>
    /// This function Views the next Project in series on "View Project" screen
    /// </summary>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            int projectId = Convert.ToInt32(hidNextProjectId.Value);
            ViewProjectDetails(Convert.ToInt32(hidNextProjectId.Value), hidSortDir.Value, hidSortExpr.Value, UserMailId, PresalesRole, PMRole, COORole, RPMRole, Action, clientNm, projectStatusId, projectsummaryprojectname);
            GetProjectSummaryList(projectId);
            ViewResourcePlan(Mode.View.ToString(), projectId);
            ViewState["projectId"] = projectId;
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "lbtnNext_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// This function Views the previous Project in series on "View Project" screen
    /// </summary>
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            int projectId = Convert.ToInt32(hidPrevProjectId.Value);
            ViewProjectDetails(Convert.ToInt32(hidPrevProjectId.Value), hidSortDir.Value, hidSortExpr.Value, UserMailId, PresalesRole, PMRole, COORole, RPMRole, Action, clientNm, projectStatusId, projectsummaryprojectname);
            GetProjectSummaryList(projectId);
            ViewResourcePlan(Mode.View.ToString(), projectId);
            ViewState["projectId"] = projectId;
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "lbtnPrevious_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// this function is used to either add,view,update,delete,approve or reject the project.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Rave.HR.BusinessLayer.Projects.Projects objAddProjectBAL;
            //Rave.HR.BusinessLayer.Projects.Projects objGetProjectCountBAL;
            BusinessEntities.Projects objAddProject;
            BusinessEntities.Projects objCurrentProjectDetail;
            //BusinessEntities.Projects objChkForDuplicateProjectName;
            List<BusinessEntities.Projects> lstCurrentProjectDetail;

            System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo(((System.Web.Configuration.GlobalizationSection)(ConfigurationSettings.GetConfig("system.web/globalization"))).Culture);

            if (mode == Mode.Update.ToString())
            {
                bool IsUpdated = false;
                bool IsTechnologyDetailsGridUpdated = true;
                bool IsDomainDetailsGridUpdated = true;
                string Updated = null;
                bool IsMailSentStatus = false;
                int count = 0;
                int subDomainCount = 0;
                string ProjectStatusId = null;


                //string ProjectStatusId = Session[SessionNames.PROJECT_STATUS].ToString();
                if (Session[SessionNames.PROJECT_STATUS] == null ||
                    Session[SessionNames.PROJECT_STATUS].ToString() == "Select")
                {
                    ProjectStatusId = "0";
                }
                else
                {
                    ProjectStatusId = Session[SessionNames.PROJECT_STATUS].ToString();
                }
                objAddProject = new BusinessEntities.Projects();
                objCurrentProjectDetail = new BusinessEntities.Projects();
                objAddProjectBAL = new Rave.HR.BusinessLayer.Projects.Projects();

                objCurrentProjectDetail.ProjectId = int.Parse(txtProjectID.Text);
                lstCurrentProjectDetail = new List<BusinessEntities.Projects>();

                //lstCurrentProjectDetail = objAddProjectBAL.GetProjectDetails(objCurrentProjectDetail, null, null, null, PresalesRole, PMRole, COORole, RPMRole, Mode.View.ToString(), null, Convert.ToInt32(ProjectStatusId), null);
                lstCurrentProjectDetail = objAddProjectBAL.GetProjectDetails(objCurrentProjectDetail, null, null, UserMailId, PresalesRole, PMRole, COORole, RPMRole, Mode.View.ToString(), null, Convert.ToInt32(ProjectStatusId), null);

                if (lstCurrentProjectDetail.Count > 0)
                    objCurrentProjectDetail = lstCurrentProjectDetail[0];

                //checks Project StandardHours is updated
                if (objCurrentProjectDetail.StandardHours != ddlStandardHours.SelectedItem.Text)
                {
                    if (objCurrentProjectDetail.StandardHours == "" &&
                        ddlStandardHours.SelectedItem.Text == "Select")
                    {
                        IsUpdated = false;
                    }
                    else
                    {
                        IsUpdated = true;
                        //IsMailSentStatus = false;
                    }
                }

                //Siddharth 12 march 2015 Start
                //checks Project StandardHours is updated
                if (objCurrentProjectDetail.ProjectModel != ddlProjectModel.SelectedItem.Text)
                {
                    if (objCurrentProjectDetail.ProjectModel == "" &&
                        ddlProjectModel.SelectedValue == "Select")
                    {
                        IsUpdated = false;
                    }
                    else
                    {
                        IsUpdated = true;
                        //IsMailSentStatus = false;
                    }
                }
                //Siddharth 12 march 2015 End


                //Siddharth 3 August 2015 Start
                //checks Project StandardHours is updated
                if (objCurrentProjectDetail.BusinessVertical != ddlBusinessVertical.SelectedItem.Text)
                {
                    if (objCurrentProjectDetail.BusinessVertical == "" &&
                        ddlBusinessVertical.SelectedValue == "Select")
                    {
                        IsUpdated = false;
                    }
                    else
                    {
                        IsUpdated = true;
                        //IsMailSentStatus = false;
                    }
                }
                //Siddharth 3 August 2015 End

                //checks Project Description is updated
                if (objCurrentProjectDetail.Description != txtDescription.Text)
                {
                    IsUpdated = true;
                    objAddProject.Description = objCurrentProjectDetail.Description;
                    //IsMailSentStatus = false;
                }

                objCurrentProjectDetail.CreatedBy = UserMailId;

                //checks ProjectGroup is updated
                if (objCurrentProjectDetail.ProjectGroup != ddlProjectGroup.SelectedItem.Text)
                {
                    IsUpdated = true;
                    //IsMailSentStatus = false;
                }

                //checks ProjectStatus is updated
                if (objCurrentProjectDetail.ProjectStatus != ddlStatus.SelectedItem.Value)
                {
                    IsUpdated = true;
                    //IsMailSentStatus = false;
                }

                //if (objCurrentProjectDetail.EndDate.ToShortDateString() != txtEndDate.Text)
                if (objCurrentProjectDetail.EndDate.ToShortDateString() != ucDatePickerEndDate.Text)
                {
                    IsUpdated = true;
                    IsMailSentStatus = true;
                }

                //checks OnGoingProjectStatus is updated
                if (objCurrentProjectDetail.OnGoingProjectStatusID != ddlOnGoingProjectStatus.SelectedItem.Value)
                {
                    IsUpdated = true;
                    //IsMailSentStatus = false;
                }

                objAddProject.ProjectName = txtProjectName.Text;
                objAddProject.ProjectCode = txtProjectCode.Text;

                // Mohamed : Issue  : 26/09/2014 : Starts                        			  
                // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
                objAddProject.ProjectDivision = Convert.ToInt32(ddlPrjDivision.SelectedValue);
                objAddProject.ProjectBussinessArea = ddlProjecBusinessArea.Enabled ? Convert.ToInt32(ddlProjecBusinessArea.SelectedValue) : 0;
                objAddProject.ProjectBussinessSegment = ddlProjectBusinessSegment.Enabled ? Convert.ToInt32(ddlProjectBusinessSegment.SelectedValue) : 0;
                //objAddProject.ProjectAlias = txtProjectAlias.Text.Trim();

                objCurrentProjectDetail.ProjectDivision = Convert.ToInt32(ddlPrjDivision.SelectedValue);
                objCurrentProjectDetail.ProjectBussinessArea = ddlProjecBusinessArea.Enabled ? Convert.ToInt32(ddlProjecBusinessArea.SelectedValue) : 0;
                objCurrentProjectDetail.ProjectBussinessSegment = ddlProjectBusinessSegment.Enabled ? Convert.ToInt32(ddlProjectBusinessSegment.SelectedValue) : 0;
                //objCurrentProjectDetail.ProjectAlias = txtProjectAlias.Text.Trim();
                // Mohamed : Issue  : 23/09/2014 : Ends


                //Rakesh : HOD for Employees 12/07/2016 Begin   
                objCurrentProjectDetail.ProjectHeadId = ddlProjectHead.Enabled ? Convert.ToInt32(ddlProjectHead.SelectedValue) : 0;
                objAddProject.ProjectHeadId = ddlProjectHead.Enabled ? Convert.ToInt32(ddlProjectHead.SelectedValue) : 0;
                
                //Rakesh : HOD for Employees 12/07/2016 End  

                string strProjectStatus = ddlStatus.SelectedItem.Text;
                objAddProject.Description = objCurrentProjectDetail.Description;
                objAddProject.OnGoingProjectStatusID = ddlOnGoingProjectStatus.SelectedValue;

                ////for project edited details
                objAddProject.ClientName = txtClientName.Text;
                //objAddProject.StartDate = Convert.ToDateTime(txtStartDate.Text);
                //string strEndDate = txtEndDate.Text;
                objAddProject.StartDate = Convert.ToDateTime(ucDatePickerStartDate.Text);
                string strEndDate = ucDatePickerEndDate.Text;

                /*If Projects Stamdered Hrs,Group,Description,Status,Endate is not chnaged*/
                if (IsUpdated == false)
                {
                    List<BusinessEntities.Technology> lstTechnologyID = new List<BusinessEntities.Technology>();
                    for (int a = 0; a < objCurrentProjectDetail.Categories.Count; a++)
                    {
                        count += objCurrentProjectDetail.Categories[a].Technologies.Count;
                    }
                    if (count == 0)
                    {
                        IsTechnologyDetailsGridUpdated = false;
                    }

                    //START
                    //Created By:Sameer Chornele
                    //Created ON:20-MAY-2010
                    //ISSUE ID: 18496
                    AddCateGoryAndTechnologyForProjects(lstTechnologyID, objCurrentProjectDetail, IsTechnologyDetailsGridUpdated);
                    //END

                    objCurrentProjectDetail.Technologies = lstTechnologyID;

                    List<BusinessEntities.SubDomain> lstSubDomainID = new List<BusinessEntities.SubDomain>();

                    for (int a = 0; a < objCurrentProjectDetail.LstDomain.Count; a++)
                    {
                        subDomainCount += objCurrentProjectDetail.LstDomain[a].lstSubDomain.Count;
                    }

                    if (subDomainCount == 0)
                    {
                        IsDomainDetailsGridUpdated = false;
                    }

                    //START
                    //Created By:Sameer Chornele
                    //Created ON:20-MAY-2010
                    //ISSUE ID: 18496
                    AddDomainAndSubDomainForProjects(lstSubDomainID, objCurrentProjectDetail, IsDomainDetailsGridUpdated);
                    //END

                    objCurrentProjectDetail.LstSubDomain = lstSubDomainID;
                    Updated = "False";
                    objAddProjectBAL = new Rave.HR.BusinessLayer.Projects.Projects();


                    if (strProjectStatus == ProjectStatus.Closed.ToString())
                    {
                        //checks if project can be closed or not.
                        if (ValidateProjectStatus())
                        {
                            objAddProjectBAL.UpdateProject(objCurrentProjectDetail, Updated, Request.QueryString.ToString(), IsMailSentStatus);
                            IsUpdated = true;
                            // IsMailSentStatus = false;
                        }
                        else
                        {
                            IsUpdated = false;
                        }
                    }
                    else
                    {
                        objAddProjectBAL.UpdateProject(objCurrentProjectDetail, Updated, Request.QueryString.ToString(), IsMailSentStatus);
                        //IsUpdated = true;
                    }

                    if (IsTechnologyDetailsGridUpdated == true ||
                        IsDomainDetailsGridUpdated == true ||
                        IsUpdated == true)
                    {
                        //MailMessage(Mode.Update.ToString());

                        //when project Status is closed
                        if (strProjectStatus == ProjectStatus.Closed.ToString())
                        {
                            Session["ConfirmationMessage"] = "Project " + txtProjectName.Text + " is closed, email notification is sent for the same.";
                        }
                        else
                        {
                            if (objAddProject.EndDate.ToString() != hfProjectEndtDts.Value)
                            {
                                Session["ConfirmationMessage"] = "Project " + txtProjectName.Text
                                    + " is updated successfully.";
                            }
                            else
                            {
                                Session["ConfirmationMessage"] = "Project " + txtProjectName.Text
                                    + " is edited, email notification is sent for the same.";
                            }
                        }
                    }


                    Response.Redirect(CommonConstants.PROJECTSUMMARY_PAGE, false);

                }
                else
                { //IsUpdated == true
                    objAddProject.ProjectId = int.Parse(DecryptQueryString(QueryStringConstants.PROJECTID));
                    objAddProject.ProjectStatus = ddlStatus.SelectedItem.Value;
                    if (ddlStandardHours.SelectedItem.Text == "Select")
                    {
                        objAddProject.StandardHours = "";
                    }
                    else
                    {
                        objAddProject.StandardHours = ddlStandardHours.SelectedItem.Text;
                    }

                    //Siddharth 12 March 2015 Start
                    if (ddlProjectModel.SelectedItem.Text == "Select")
                    {
                        objAddProject.ProjectModel = "";
                    }
                    else
                    {
                        objAddProject.ProjectModel = ddlProjectModel.SelectedValue;
                    }
                    //Siddharth 12 March 2015 End

                    //Siddharth 3 August 2015 Start
                    if (ddlBusinessVertical.SelectedItem.Text == "Select")
                    {
                        objAddProject.BusinessVertical = "";
                    }
                    else
                    {
                        objAddProject.BusinessVertical = ddlBusinessVertical.SelectedValue;
                    }
                    //Siddharth 3 August 2015 End

                    objAddProject.Description = txtDescription.Text;
                    objAddProject.CreatedBy = UserMailId;
                    //objAddProject.EndDate = Convert.ToDateTime(txtEndDate.Text);
                    objAddProject.EndDate = Convert.ToDateTime(ucDatePickerEndDate.Text);

                    // Make txtEndDate readonly
                    //txtEndDate.ReadOnly = true;

                    List<BusinessEntities.Technology> lstTechnologyID = new List<BusinessEntities.Technology>();
                    for (int a = 0; a < objCurrentProjectDetail.Categories.Count; a++)
                    {
                        count += objCurrentProjectDetail.Categories[a].Technologies.Count;
                    }

                    if (count == 0)
                    {
                        IsTechnologyDetailsGridUpdated = false;
                    }

                    //START
                    //Created By:Sameer Chornele
                    //Created ON:20-MAY-2010
                    //ISSUE ID: 18496
                    AddCateGoryAndTechnologyForProjects(lstTechnologyID, objCurrentProjectDetail, IsTechnologyDetailsGridUpdated);
                    //END


                    objAddProject.Technologies = lstTechnologyID;

                    List<BusinessEntities.SubDomain> lstSubDomainID = new List<BusinessEntities.SubDomain>();

                    for (int a = 0; a < objCurrentProjectDetail.LstDomain.Count; a++)
                    {
                        subDomainCount += objCurrentProjectDetail.LstDomain[a].lstSubDomain.Count;
                    }

                    if (subDomainCount == 0)
                    {
                        IsDomainDetailsGridUpdated = false;
                    }

                    //START
                    //Created By:Sameer Chornele
                    //Created ON:20-MAY-2010
                    //ISSUE ID: 18496
                    AddDomainAndSubDomainForProjects(lstSubDomainID, objCurrentProjectDetail, IsDomainDetailsGridUpdated);
                    //END

                    objAddProject.LstSubDomain = lstSubDomainID;
                    Updated = "True";

                    //Add projectGroup during updation.
                    objAddProject.ProjectGroup = ddlProjectGroup.SelectedItem.Text;

                    objAddProjectBAL = new Rave.HR.BusinessLayer.Projects.Projects();


                    //when project Status is closed
                    if (strProjectStatus == ProjectStatus.Closed.ToString())
                    {
                        if (ValidateProjectStatus())
                        {
                            objAddProjectBAL.UpdateProject(objAddProject, Updated, Request.QueryString.ToString(), IsMailSentStatus);
                            IsUpdated = true;
                        }
                        else
                        {
                            IsUpdated = false;
                        }

                    }
                    else
                    {
                        if (objAddProject.EndDate.ToString() != hfProjectEndtDts.Value)
                        {
                            objAddProjectBAL.UpdateProject(objAddProject, Updated, Request.QueryString.ToString(), IsMailSentStatus);
                            IsUpdated = true;
                            IsMailSentStatus = true;
                        }
                        else
                        {
                            IsUpdated = false;
                            //IsMailSentStatus = false;
                        }
                    }

                    if (IsUpdated == true)
                        SessionClear();

                    projectId = int.Parse(DecryptQueryString(QueryStringConstants.PROJECTID));
                    SortExpression = DecryptQueryString(QueryStringConstants.SORTEXPRESSION);
                    SortDir = DecryptQueryString(QueryStringConstants.SORTDIRECTION);

                    if (IsUpdated == true)
                    {

                        Response.Redirect(CommonConstants.PROJECTSUMMARY_PAGE, false);
                    }
                }
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "lbtnSubmit_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Get Allocated Resource By ProjectId
    /// </summary>
    private BusinessEntities.RaveHRCollection GetAllocatedResourceByProjectId()
    {
        BusinessEntities.ResourcePlan objBEResourcePlan = new BusinessEntities.ResourcePlan();
        objBEResourcePlan.ProjectId = int.Parse(DecryptQueryString(QueryStringConstants.PROJECTID));
        Rave.HR.BusinessLayer.Projects.ResourcePlan ObjBLLResourcePlan = new Rave.HR.BusinessLayer.Projects.ResourcePlan();

        BusinessEntities.RaveHRCollection ObjAllocatedResourceByProjectId = new BusinessEntities.RaveHRCollection();

        ObjAllocatedResourceByProjectId = ObjBLLResourcePlan.GetAllocatedResourceByProjectId(objBEResourcePlan);

        return ObjAllocatedResourceByProjectId;
    }

    /// <summary>
    /// ddlCategory_SelectedIndexChanged event handler
    /// </summary>
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string CategoryID = ddlCategory.SelectedItem.Value.ToString();

            if (CategoryID != "Select")
            {

                List<BusinessEntities.Technology> lstTechnology = new List<BusinessEntities.Technology>();
                Rave.HR.BusinessLayer.Projects.Projects objCategoryBAL = new Rave.HR.BusinessLayer.Projects.Projects();

                lstTechnology = objCategoryBAL.Technology(Convert.ToInt32(CategoryID));

                // 34914-Ambar-Start-21062012
                // Added condition to populate data only when necessary
                if (lstTechnology.Count > 0)
                {
                    //Get the Hidden selected index field of the Combo
                    System.Web.UI.WebControls.HiddenField hfSelctedIndex = ddlTechnology.FindControl("HiddenField") as System.Web.UI.WebControls.HiddenField;

                    //Clear the selected index's
                    hfSelctedIndex.Value = (0).ToString();
                    ddlTechnology.ClearSelection();
                    ddlTechnology.DataSource = lstTechnology;
                    ddlTechnology.DataTextField = "TechnolgoyName";
                    ddlTechnology.DataValueField = "TechnologyID";
                    ddlTechnology.DataBind();
                    ddlTechnology.Items.Insert(0, "Select");
                    ddlTechnology.SelectedIndex = 0;
                }
                else
                {
                    ddlTechnology.ClearSelection();
                    ddlTechnology.Items.Insert(0, "Select");
                    ddlTechnology.SelectedIndex = 0;
                }
                // 34914-Ambar-End-21062012

            }
            else
            {
                ddlTechnology.ClearSelection();
                ddlTechnology.Items.Insert(0, "Select");

                // 34914-Ambar-Start-21062012
                ddlTechnology.SelectedIndex = 0;
                // 34914-Ambar-End-21062012
            }

            ScriptManager.GetCurrent(Page).SetFocus(ddlCategory);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "lbtnSubmit_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// btnAdd_Click event handler
    /// </summary>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            gvCategoryTechnology.Visible = true;

            DataTable dtTechnologies = new DataTable();
            if (Session["TechnologiesTable"] != null)
            {

                dtTechnologies = (DataTable)Session["TechnologiesTable"];
            }
            else
            {
                dtTechnologies.Columns.Add("category");
                dtTechnologies.Columns.Add("categoryId");
                dtTechnologies.Columns.Add("technologyName");
                dtTechnologies.Columns.Add("technologyID");
            }

            string category = null;

            if (mode == Mode.Update.ToString())
            {
                lbCategorylError.Text = "";
                category = ddlCategory.SelectedItem.Text;
                if (category != "Select")
                {
                    if (ddlTechnology.SelectedItem.Text != "Select")
                    {
                        if (dtTechnologies.Select("technologyID='" + ddlTechnology.SelectedItem.Value + "'").Length == 0)
                        {
                            DataRow dr = dtTechnologies.NewRow();
                            dr["category"] = ddlCategory.SelectedItem.Text;
                            dr["categoryId"] = ddlCategory.SelectedItem.Value;
                            dr["technologyName"] = ddlTechnology.SelectedItem.Text;
                            dr["technologyID"] = ddlTechnology.SelectedItem.Value;
                            dtTechnologies.Rows.Add(dr);

                            Session["TechnologiesTable"] = dtTechnologies;
                            gvCategoryTechnology.DataSource = dtTechnologies;
                            gvCategoryTechnology.DataBind();
                        }
                        else
                        {
                            lbCategorylError.Text = "Duplicate technology is not allowed kindly select another.";
                        }
                    }
                    else
                    {
                        lbCategorylError.Text = "Please Select a Technology";
                    }
                }
                else
                {
                    lbCategorylError.Text = "Please Select a Category";
                }
            }
            else
            {
                lbCategorylError.Text = "";
                category = ddlCategory.SelectedItem.Text;
                if (category != "Select")
                {
                    string technology = ddlTechnology.SelectedItem.Text;
                    if (technology != "Select")
                    {
                        if (dtTechnologies.Select("technologyID='" + ddlTechnology.SelectedItem.Value + "'").Length == 0)
                        {
                            DataRow dr = dtTechnologies.NewRow();
                            dr["category"] = ddlCategory.SelectedItem.Text;
                            dr["categoryId"] = ddlCategory.SelectedItem.Value;
                            dr["technologyName"] = ddlTechnology.SelectedItem.Text;
                            dr["technologyID"] = ddlTechnology.SelectedItem.Value;
                            dtTechnologies.Rows.Add(dr);

                            Session["TechnologiesTable"] = dtTechnologies;
                            gvCategoryTechnology.DataSource = dtTechnologies;
                            gvCategoryTechnology.DataBind();
                        }
                        else
                        {
                            lbCategorylError.Text = "Duplicate technology is not allowed kindly select another.";
                        }
                    }
                    else
                    {
                        lbCategorylError.Text = "Please Select a Technology";
                    }

                }
                else
                {
                    lbCategorylError.Text = "Please Select a Category";
                }
            }
            ScriptManager.GetCurrent(Page).SetFocus(btnAdd);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "lbtnSubmit_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// gvCategoryTechnology_RowDeleting event handler
    /// </summary>
    protected void gvCategoryTechnology_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dtTechnologies = new DataTable();
        if (Session["TechnologiesTable"] != null)
        {
            dtTechnologies = (DataTable)Session["TechnologiesTable"];
        }
        dtTechnologies.Rows[e.RowIndex].Delete();
        Session["TechnologiesTable"] = dtTechnologies;

        gvCategoryTechnology.DataSource = dtTechnologies;
        gvCategoryTechnology.DataBind();
    }

    /// <summary>
    /// lbtnRemove_Click event handler
    /// </summary>
    protected void lbtnRemove_Click(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// ddlDomain_SelectedIndexChanged event handler
    /// </summary>>
    protected void ddlDomain_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string DomainID = ddlDomain.SelectedItem.Value.ToString();
            if (DomainID != "Select")
            {
                List<BusinessEntities.SubDomain> lstSubDomain = new List<BusinessEntities.SubDomain>();
                Rave.HR.BusinessLayer.Projects.Projects objSubDomainBAL = new Rave.HR.BusinessLayer.Projects.Projects();

                lstSubDomain = objSubDomainBAL.GetSubDomain(Convert.ToInt32(DomainID));

                // 34914-Ambar-Start-21062012
                // Added condition to populate data only when necessary
                if (lstSubDomain.Count > 0)
                {
                    //Get the Hidden selected index field of the Combo
                    System.Web.UI.WebControls.HiddenField hfSelctedIndex = ddlSubDomain.FindControl("HiddenField") as System.Web.UI.WebControls.HiddenField;

                    //Clear the selected index's
                    hfSelctedIndex.Value = (0).ToString();
                    ddlSubDomain.ClearSelection();
                    ddlSubDomain.DataSource = lstSubDomain;
                    ddlSubDomain.DataTextField = "SubDomainName";
                    ddlSubDomain.DataValueField = "SubDomainID";
                    ddlSubDomain.DataBind();
                    ddlSubDomain.Items.Insert(0, "Select");
                    ddlSubDomain.SelectedIndex = 0;
                }
                else
                {
                    ddlSubDomain.ClearSelection();
                    ddlSubDomain.Items.Insert(0, "Select");
                    ddlSubDomain.SelectedIndex = 0;
                }
                // 34914-Ambar-End-21062012
            }
            else
            {
                ddlSubDomain.ClearSelection();
                ddlSubDomain.Items.Insert(0, "Select");

                // 34914-Ambar-Start-21062012
                ddlSubDomain.SelectedIndex = 0;
                // 34914-Ambar-End-21062012

            }
            ScriptManager.GetCurrent(Page).SetFocus(ddlDomain);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "lbtnSubmit_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// gvDomainSubDomain_RowDeleting event handler
    /// </summary>
    protected void gvDomainSubDomain_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dtSubDomains = new DataTable();
        if (Session["SubDomainsTable"] != null)
        {
            dtSubDomains = (DataTable)Session["SubDomainsTable"];
        }
        dtSubDomains.Rows[e.RowIndex].Delete();
        Session["SubDomainsTable"] = dtSubDomains;

        gvDomainSubDomain.DataSource = dtSubDomains;
        gvDomainSubDomain.DataBind();
    }

    /// <summary>
    /// btnAddDomain_Click event handler
    /// </summary>
    protected void btnAddDomain_Click(object sender, EventArgs e)
    {
        try
        {
            gvDomainSubDomain.Visible = true;

            DataTable dtSubDomains = new DataTable();
            if (Session["SubDomainsTable"] != null)
            {
                dtSubDomains = (DataTable)Session["SubDomainsTable"];
            }
            else
            {

                dtSubDomains.Columns.Add("ID");
                dtSubDomains.Columns.Add("domain");
                dtSubDomains.Columns.Add("subDomain");
                dtSubDomains.Columns.Add("subDomainId");
            }
            string domain = null;

            if (mode == Mode.Update.ToString())
            {
                lblDomainError.Text = "";
                domain = ddlDomain.SelectedItem.Text;
                if (domain != "Select")
                {
                    string subDomain = ddlSubDomain.SelectedItem.Text;

                    //get subdomain value if user has not selected 
                    if (subDomain == "Select")
                        ddlSubDomain.SelectedItem.Value = Convert.ToString(GetSubDomainIdByDomainId());

                    //checks if choosen subdomain is already selected
                    if (dtSubDomains.Select("subDomainId='" + ddlSubDomain.SelectedItem.Value + "'").Length == 0)
                    {
                        DataRow dr = dtSubDomains.NewRow();
                        dr["ID"] = ddlDomain.SelectedItem.Value;
                        dr["domain"] = ddlDomain.SelectedItem.Text;
                        if (subDomain == "Select")
                        {
                            dr["subDomain"] = "None";
                        }
                        else
                        {
                            dr["subDomain"] = ddlSubDomain.SelectedItem.Text;
                        }

                        dr["subDomainId"] = ddlSubDomain.SelectedItem.Value;
                        dtSubDomains.Rows.Add(dr);

                        Session["SubDomainsTable"] = dtSubDomains;
                        gvDomainSubDomain.DataSource = dtSubDomains;
                        gvDomainSubDomain.DataBind();
                    }
                    else
                    {
                        lblDomainError.Text = "Duplicate SubDomain is not allowed kindly select another.";
                    }
                }
                else
                {
                    lblDomainError.Text = "Please Select a Domain";
                }
            }
            else
            {
                lblDomainError.Text = "";
                domain = ddlDomain.SelectedItem.Text;
                if (domain != "Select")
                {
                    lblDomainError.Text = "";
                    string subDomain = ddlSubDomain.SelectedItem.Text;
                    if (subDomain != "Select")
                    {
                        if (dtSubDomains.Select("subDomainId='" + ddlSubDomain.SelectedItem.Value + "'").Length == 0)
                        {
                            DataRow dr = dtSubDomains.NewRow();
                            dr["ID"] = ddlDomain.SelectedItem.Text;
                            dr["domain"] = ddlDomain.SelectedItem.Text;
                            dr["subDomain"] = ddlSubDomain.SelectedItem.Text;
                            dr["subDomainId"] = ddlSubDomain.SelectedItem.Value;
                            dtSubDomains.Rows.Add(dr);

                            Session["SubDomainsTable"] = dtSubDomains;
                            gvDomainSubDomain.DataSource = dtSubDomains;
                            gvDomainSubDomain.DataBind();
                        }
                        else
                        {
                            lblDomainError.Text = "Duplicate SubDomain is not allowed kindly select another.";
                        }
                    }
                    else
                    {
                        lblDomainError.Text = "Please Select a SubDomain";
                    }
                }
                else
                {
                    lblDomainError.Text = "Please Select a Domain";
                }
            }
            ScriptManager.GetCurrent(Page).SetFocus(btnAddDomain);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "lbtnSubmit_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// btnCancel_Click event handler
    /// </summary>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            SessionClear();
            mode = DecryptQueryString(QueryStringConstants.MODE);
            if ((mode == Mode.Update.ToString() && ViewState["PreviousURL"] != null))
            {
                //Siddhesh : Issue  : 28/07/2015 : Start
                //Description: Code changed to handle runtime error on cancel button click
                Response.Redirect(ViewState["PreviousURL"].ToString(), false);
                //Siddhesh : Issue  : 28/07/2015 : End
            }
            else
            {
                Response.Redirect(CommonConstants.PROJECTSUMMARY_PAGE.ToString(), false);
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "lbtnSubmit_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// btnEdit_Click event handler
    /// </summary>
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            //ucDatePickerEndDate.IsEnable = true;
            //ucDatePickerStartDate.IsEnable = true;
            //method to get projectId,sort Expression and Direction
            GetProjectIdSortExpAndDirection();
            //ucDatePickerEndDate.IsEnable = true;
            //ucDatePickerStartDate.IsEnable = true;

            Response.Redirect(CommonConstants.ADDPROJECT_PAGE.ToString() + "?" +
                URLHelper.SecureParameters("ProjectID", txtProjectID.Text) + "&" +
                URLHelper.SecureParameters("Mode", Mode.Update.ToString()) + "&" +
                URLHelper.SecureParameters("sortExpression", SortExpression) + "&" +
                URLHelper.SecureParameters("sortDirection", SortDir) + "&" +
                URLHelper.CreateSignature(txtProjectID.Text, Mode.Update.ToString(), SortExpression, SortDir), false);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "lbtnSubmit_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    #region For Edit Resource Plan

    /// <summary>
    /// Edit and Create resource plan button click eventhandler.
    /// </summary>
    protected void btnEditResourcePlan_Click(object sender, EventArgs e)
    {
        try
        {
            projectId = int.Parse(DecryptQueryString(QueryStringConstants.PROJECTID));
            mode = DecryptQueryString(QueryStringConstants.MODE);

            //in view mode 
            if ((mode == Mode.View.ToString()))
            {
                if (btnEditResourcePlan.CommandName == "CreateResourcePlan")
                {
                    Response.Redirect(CommonConstants.CREATERP_PAGE.ToString() + "?" +
                        URLHelper.SecureParameters("ProjectId", txtProjectID.Text) + "&" +
                        URLHelper.CreateSignature(txtProjectID.Text), false);
                }
                else if (btnEditResourcePlan.CommandName == "ViewResourcePlan")
                {
                    Response.Redirect(CommonConstants.VIEWRP_PAGE.ToString() + "?" +
                        URLHelper.SecureParameters("ProjectId", txtProjectID.Text) + "&" +
                        URLHelper.CreateSignature(txtProjectID.Text), false);
                }
            }

            //in edit mode
            if ((mode == Mode.Update.ToString()))
            {
                if (btnEditResourcePlan.CommandName == "CreateResourcePlan")
                {
                    Response.Redirect(CommonConstants.CREATERP_PAGE.ToString() + "?" +
                        URLHelper.SecureParameters("ProjectId", txtProjectID.Text) + "&" +
                        URLHelper.CreateSignature(txtProjectID.Text), false);
                }
                else if (btnEditResourcePlan.CommandName == "EditResourcePlan")
                {
                    Response.Redirect(CommonConstants.EDITRPOPTIONS_PAGE.ToString() + "?" +
                        URLHelper.SecureParameters("ProjectId", txtProjectID.Text) + "&" +
                        URLHelper.CreateSignature(txtProjectID.Text), false);
                }
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "lbtnSubmit_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    #endregion For Edit Resource Plan

    #endregion Protected Events

    #region Private Member Functions

    /// <summary>
    /// method to get value of StatusDropDown
    /// </summary>
    private void GetMasterData_StatusDropDown()
    {
        try
        {
            List<BusinessEntities.Master> objRaveHRMaster = new List<BusinessEntities.Master>();
            List<BusinessEntities.ContractProject> objContractType = new List<BusinessEntities.ContractProject>();
            Rave.HR.BusinessLayer.Common.Master objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            objRaveHRMaster = objRaveHRMasterBAL.GetMasterData((Convert.ToInt32(EnumsConstants.Category.ProjectStatus)).ToString());

            ddlStatus.DataSource = objRaveHRMaster;
            ddlStatus.DataTextField = "MasterName";
            ddlStatus.DataValueField = "MasterID";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, "Select");

            #region project status by contract type

            objContractType = GetContractType();

            foreach (BusinessEntities.ContractProject objContract in objContractType)
            {
                //Ishwar : Issue 49176 : 20/02/2014 : Starts
                hidContractEndDt.Value = objContract.ContractEndDate.ToString(dateFormat);
                //Ishwar : Issue 49176 : 20/02/2014 : End

                if (objContract.ContractType != "Rave Internal")
                {

                    //if (ObjMaster.MasterName == ProjectStatus.Delivery.ToString())
                    //{
                    //    ddlStatus.Items.Remove(ddlStatus.Items.FindByText("Pre-Sales"));
                    //    break;
                    //}
                    //else if (ObjMaster.MasterName == "Pre-Sales")
                    //{
                    //    ddlStatus.Items.Remove(ddlStatus.Items.FindByText(ProjectStatus.Delivery.ToString()));
                    //    break;
                    //}

                }
            }

            #endregion project status by contract type
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "GetMasterData_StatusDropDown", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// method to get value of LocationDropDown
    /// </summary>
    private void GetMasterData_LocationDropDown()
    {
        try
        {
            List<BusinessEntities.Master> objRaveHRMaster = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();

            objRaveHRMaster = objRaveHRMasterBAL.GetMasterData((Convert.ToInt32(EnumsConstants.Category.Location)).ToString());
            ddlLocation.DataSource = objRaveHRMaster;
            ddlLocation.DataTextField = "MasterName";
            ddlLocation.DataValueField = "MasterID";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, "Select");
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "GetMasterData_LocationDropDown", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    ///  method to get value of ProjectGroupDropDown
    /// </summary>
    private void GetMasterData_ProjectGroupDropDown()
    {
        try
        {
            List<BusinessEntities.Master> objRaveHRMaster = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            objRaveHRMaster = objRaveHRMasterBAL.GetMasterData((Convert.ToInt32(EnumsConstants.Category.ProjectGroup)).ToString());
            ddlProjectGroup.DataSource = objRaveHRMaster;
            ddlProjectGroup.DataTextField = "MasterName";
            ddlProjectGroup.DataValueField = "MasterID";
            ddlProjectGroup.DataBind();
            ddlProjectGroup.Items.Insert(0, "Select");
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "GetMasterData_ProjectGroupDropDown", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    //Vandana
    private void GetOnGoingProjectStatus_Dropdown()
    {
        try
        {
            List<BusinessEntities.Master> objRaveHRMaster = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master objRaveHRMasterBL = new Rave.HR.BusinessLayer.Common.Master();
            objRaveHRMaster = objRaveHRMasterBL.GetMasterData((Convert.ToInt32(EnumsConstants.Category.OnGoingProjectStauts)).ToString());
            ddlOnGoingProjectStatus.DataSource = objRaveHRMaster;
            ddlOnGoingProjectStatus.DataTextField = "MasterName";
            ddlOnGoingProjectStatus.DataValueField = "MasterID";
            ddlOnGoingProjectStatus.DataBind();
            ddlOnGoingProjectStatus.Items.Insert(0, "Select");

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex,
                Sources.PresentationLayer, "AddProject.aspx", "GetOnGoingProjectStatus_Dropdown",
                EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    //Vandana

    /// <summary>
    ///  method to get value of ProjectStdHoursDropDown
    /// </summary>
    private void GetMasterData_ProjectStdHoursDropDown()
    {
        try
        {
            List<BusinessEntities.Master> objRaveHRMaster = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            objRaveHRMaster = objRaveHRMasterBAL.GetMasterData((Convert.ToInt32(EnumsConstants.Category.ProjectStandardHours)).ToString());
            ddlStandardHours.DataSource = objRaveHRMaster;
            ddlStandardHours.DataTextField = "MasterName";
            ddlStandardHours.DataValueField = "MasterID";
            ddlStandardHours.DataBind();
            ddlStandardHours.Items.Insert(0, "Select");
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "GetMasterData_ProjectStdHoursDropDown", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    ///  method to get value of categoryDropDown
    /// </summary>
    private void categoryDropDown()
    {
        try
        {
            List<BusinessEntities.Category> lstCategory = new List<BusinessEntities.Category>();
            Rave.HR.BusinessLayer.Projects.Projects objTechnologyBAL = new Rave.HR.BusinessLayer.Projects.Projects();

            lstCategory = objTechnologyBAL.TechnologyCategory();
            ddlCategory.DataSource = lstCategory;
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryId";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, "Select");
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "categoryDropDown", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    ///  method to get value of domainDropDown
    /// </summary>
    private void domainDropDown()
    {
        try
        {
            List<BusinessEntities.Domain> lstDomain = new List<BusinessEntities.Domain>();
            Rave.HR.BusinessLayer.Projects.Projects objDomainBAL = new Rave.HR.BusinessLayer.Projects.Projects();

            lstDomain = objDomainBAL.GetDomainName();
            ddlDomain.DataSource = lstDomain;
            ddlDomain.DataTextField = "DomainName";
            ddlDomain.DataValueField = "DomainID";
            ddlDomain.DataBind();
            ddlDomain.Items.Insert(0, "Select");
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "domainDropDown", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }



    //Siddharth 13 march 2015 Start
    /// <summary>
    /// method to get value of ProjectModel
    /// </summary>
    private void GetMasterData_ProjectModelDropDown()
    {
        try
        {
            List<BusinessEntities.Master> objRaveHRMaster = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();

            objRaveHRMaster = objRaveHRMasterBAL.GetMasterData((Convert.ToInt32(EnumsConstants.Category.ProjectModel)).ToString());
            ddlProjectModel.DataSource = objRaveHRMaster;
            ddlProjectModel.DataTextField = "MasterName";
            ddlProjectModel.DataValueField = "MasterID";
            ddlProjectModel.DataBind();
            ddlProjectModel.Items.Insert(0, "Select");
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "GetMasterData_ProjectModelDropDown", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    //Siddharth 13 march 2015 End


    //Siddharth 3 August 2015 Start
    /// <summary>
    /// method to get value of ProjectModel
    /// </summary>
    private void GetMasterData_BusinessVerticalDropDown()
    {
        try
        {
            List<BusinessEntities.Master> objRaveHRMaster = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();

            objRaveHRMaster = objRaveHRMasterBAL.GetMasterData((Convert.ToInt32(EnumsConstants.Category.BusinessVertical)).ToString());
            ddlBusinessVertical.DataSource = objRaveHRMaster;
            ddlBusinessVertical.DataTextField = "MasterName";
            ddlBusinessVertical.DataValueField = "MasterID";
            ddlBusinessVertical.DataBind();
            ddlBusinessVertical.Items.Insert(0, "Select");
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "GetMasterData_ProjectModelDropDown", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    //Siddharth 3 August 2015 End





    /// <summary>
    /// method to view projectdetails
    /// </summary>
    private void ViewProjectDetails(int projId)
    {
        ViewProjectDetails(projId, null, null, null, null, null, null, null, null, null, 0, null);
    }

    /// <summary>
    /// method to view projectdetails
    /// </summary>
    private void ViewProjectDetails(int projId, string SortDir, string SortExpression, string UserMailId,
                                    string PresalesRole, string PMRole, string COORole, string RPMRole,
                                    string Action, string clientNm, int projectStatusId,
                                    string projectsummaryprojectname)
    {
        try
        {
            lblAddProject.Text = "View Project";

            BusinessEntities.Projects objViewProject = new BusinessEntities.Projects();
            objViewProject.ProjectId = projId;

            List<BusinessEntities.Projects> lstRaveHRViewProject = new List<BusinessEntities.Projects>();

            Rave.HR.BusinessLayer.Projects.Projects objViewProjectBAL = new Rave.HR.BusinessLayer.Projects.Projects();

            lstRaveHRViewProject = objViewProjectBAL.GetProjectDetails(objViewProject, SortDir, SortExpression, UserMailId, PresalesRole, PMRole, COORole, RPMRole, Action, clientNm, projectStatusId, projectsummaryprojectname);

            BusinessEntities.Projects RaveHRViewProject = lstRaveHRViewProject[0];

            // Making all the fields read-only on "View Project"
            ReadOnlyFields();
            ddlStatus.Visible = false;
            ddlProjectGroup.Visible = false;
            ddlStandardHours.Visible = false;
            ddlLocation.Visible = false;

            txtStatus.Visible = true;
            txtProjectGroup.Visible = true;
            txtStandardHours.Visible = true;
            txtLocation.Visible = true;

            ddlOnGoingProjectStatus.Visible = false;
            txtOnGoingProjectStatus.Visible = true;


            ucDatePickerStartDate.IsEnable = false;
            ucDatePickerEndDate.IsEnable = false;

            txtProjectID.Text = RaveHRViewProject.ProjectId.ToString();

            //Aarohi : Issue 31838(CR) : 28/12/2011 : Start
            Session[SessionNames.PROJECT_ID] = txtProjectID.Text;
            //Aarohi : Issue 31838(CR) : 28/12/2011 : End

            txtProjectCode.Text = RaveHRViewProject.ProjectCode;
            //hidPrevProjectId.Value = RaveHRViewProject.iPrevProjectId.ToString();
            hidSortDir.Value = SortDir;
            hidSortExpr.Value = SortExpression;
            //hidNextProjectId.Value = RaveHRViewProject.iNextProjectId.ToString();            
            txtClientName.Text = RaveHRViewProject.ClientName;
            txtProjectName.Text = RaveHRViewProject.ProjectName;

            // Mohamed : Issue  : 26/09/2014 : Starts                        			  
            // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
            ddlPrjDivision.ClearSelection();
            if (ddlPrjDivision.Items.FindByValue(RaveHRViewProject.ProjectDivision.ToString()) != null)
            {
                ddlPrjDivision.Items.FindByValue(RaveHRViewProject.ProjectDivision.ToString()).Selected = true;
            }

            ddlProjecBusinessArea.ClearSelection();
            if (ddlProjecBusinessArea.Items.FindByValue(RaveHRViewProject.ProjectBussinessArea.ToString()) != null)
            {
                ddlProjecBusinessArea.Items.FindByValue(RaveHRViewProject.ProjectBussinessArea.ToString()).Selected = true;
            }
            //if (RaveHRViewProject.ProjectDivision.ToString().Equals(CommonConstants.Project_Division_PublicService))
            //{
            //    ddlProjecBusinessArea.Enabled = true;
            //}

            //Rakesh : HOD for Employees 12/July/2016 Begin
            ddlProjectHead.SelectedValue = Convert.ToString(RaveHRViewProject.ProjectHeadId);
            //Rakesh : HOD for Employees 12/July/2016 End

            //Siddharth 12 March 2015 Start
            ddlProjectModel.ClearSelection();
            if (!string.IsNullOrEmpty(RaveHRViewProject.ProjectModel.ToString()))
            {
                if (ddlProjectModel.Items.FindByValue(RaveHRViewProject.ProjectModel.ToString()) != null)
                {
                    ddlProjectModel.Items.FindByValue(RaveHRViewProject.ProjectModel.ToString()).Selected = true;
                }
            }
            else
            {
                //ddlProjectModel.Items.FindByValue(CommonConstants.SELECT).Selected = true;
                ddlProjectModel.Items[0].Selected = true;
            }
            //Siddharth 12 March 2015 End

            //Siddharth 3 August 2015 Start
            ddlBusinessVertical.ClearSelection();
            if (!string.IsNullOrEmpty(RaveHRViewProject.BusinessVertical.ToString()))
            {
                if (ddlBusinessVertical.Items.FindByValue(RaveHRViewProject.BusinessVertical.ToString()) != null)
                {
                    ddlBusinessVertical.Items.FindByValue(RaveHRViewProject.BusinessVertical.ToString()).Selected = true;
                }
            }
            else
            {
                //ddlProjectModel.Items.FindByValue(CommonConstants.SELECT).Selected = true;
                ddlBusinessVertical.Items[0].Selected = true;
            }
            //Siddharth 3 August 2015 End

            ddlProjectBusinessSegment.ClearSelection();
            if (ddlProjectBusinessSegment.Items.FindByValue(RaveHRViewProject.ProjectBussinessSegment.ToString()) != null)
            {
                ddlProjectBusinessSegment.Items.FindByValue(RaveHRViewProject.ProjectBussinessSegment.ToString()).Selected = true;
            }
            //if (RaveHRViewProject.ProjectBussinessArea.ToString().Equals(CommonConstants.Project_BussinessArea_Solutions))
            //{
            //    ddlProjectBusinessSegment.Enabled = true;
            //}
            //txtProjectAlias.ReadOnly = false;
            //txtProjectAlias.Enabled = true;
            //txtProjectAlias.Text = RaveHRViewProject.ProjectAlias.ToString();
            // Mohamed : Issue  : 23/09/2014 : Ends

            ddlStatus.ClearSelection();
            ddlStatus.SelectedValue = RaveHRViewProject.ProjectStatus;
            txtStatus.Text = ddlStatus.SelectedItem.Text;
            ddlLocation.ClearSelection();
            ddlLocation.SelectedItem.Value = RaveHRViewProject.Location;
            txtLocation.Text = ddlLocation.SelectedItem.Value;
            ddlStandardHours.ClearSelection();
            ddlStandardHours.SelectedItem.Text = RaveHRViewProject.StandardHours.ToString();
            txtStandardHours.Text = ddlStandardHours.SelectedItem.Text;
            ddlProjectGroup.ClearSelection();
            ddlProjectGroup.SelectedItem.Value = RaveHRViewProject.ProjectGroup;
            txtProjectGroup.Text = ddlProjectGroup.SelectedItem.Value;
            txtDescription.Text = RaveHRViewProject.Description;

            // Mohamed : Issue 36710 : 24/04/2013 : Starts                        			  
            // Desc : Check the projectstatusid value and display the proper value
            ddlOnGoingProjectStatus.ClearSelection();
            if (RaveHRViewProject.OnGoingProjectStatusID == null || RaveHRViewProject.OnGoingProjectStatusID == "")
            {
                ddlOnGoingProjectStatus.SelectedIndex = 0;
                txtOnGoingProjectStatus.Text = ddlOnGoingProjectStatus.SelectedItem.Text;
            }
            else
            {
                ddlOnGoingProjectStatus.SelectedValue = RaveHRViewProject.OnGoingProjectStatusID;
                txtOnGoingProjectStatus.Text = ddlOnGoingProjectStatus.SelectedItem.Text;
            }
            // Mohamed : Issue 36710 : 24/04/2013 : Ends
            //txtProjectManager.Text = RaveHRViewProject.fullName;
            /*if (RaveHRViewProject.fullName == null)
            {
                txtProjectManager.Enabled = false;
                txtProjectManager.Text = "N/A";
            }
            else
            {
                txtProjectManager.Text = RaveHRViewProject.fullName;
            }*/

            hidProjectManagerName.Value = RaveHRViewProject.FullName;
            hidProjectManagerEmail.Value = RaveHRViewProject.EmailIdOfPM;

            //txtStartDate.Text = RaveHRViewProject.StartDate.ToString(dateFormat);
            //txtEndDate.Text = RaveHRViewProject.EndDate.ToString(dateFormat);

            ucDatePickerStartDate.Text = RaveHRViewProject.StartDate.ToString(dateFormat);

            hfProjectEndtDts.Value = RaveHRViewProject.EndDate.ToString(dateFormat);

            ucDatePickerEndDate.Text = RaveHRViewProject.EndDate.ToString(dateFormat);

            ddlCategory.Visible = false;
            ddlTechnology.Visible = false;
            ddlDomain.Visible = false;
            ddlSubDomain.Visible = false;

            List<BusinessEntities.Projects> lstGridView = new List<BusinessEntities.Projects>();

            BusinessEntities.Projects objCategoryTechnology = null;

            int count;

            if (RaveHRViewProject.Categories.Count != 0)
            {
                pnlNoTechnologyRecords.Visible = false;
                divTechnologyDetails.Visible = true;

                DataTable dtTechnologies = new DataTable();
                dtTechnologies.Columns.Add("category");
                dtTechnologies.Columns.Add("categoryId");
                dtTechnologies.Columns.Add("technologyName");
                dtTechnologies.Columns.Add("technologyID");

                for (count = 0; count < RaveHRViewProject.Categories.Count; count++)
                {
                    for (int i = 0; i < RaveHRViewProject.Categories[count].Technologies.Count; i++)
                    {
                        DataRow dr = dtTechnologies.NewRow();
                        dr["category"] = RaveHRViewProject.Categories[count].CategoryName;
                        dr["categoryId"] = RaveHRViewProject.Categories[count].CategoryId;
                        dr["technologyName"] = RaveHRViewProject.Categories[count].Technologies[i].TechnolgoyName;
                        dr["technologyID"] = RaveHRViewProject.Categories[count].Technologies[i].TechnologyID;
                        dtTechnologies.Rows.Add(dr);
                    }
                }

                gvCategoryTechnology.Visible = true;
                btnAdd.Visible = false;
                lblCategories.Visible = false;
                lblTechnologies.Visible = false;
                gvCategoryTechnology.Columns[4].Visible = false;
                gvCategoryTechnology.DataSource = dtTechnologies;
                gvCategoryTechnology.DataBind();

                Session["TechnologiesTable"] = dtTechnologies;
            }
            else
            {
                divTechnologyDetails.Visible = false;
                pnlNoTechnologyRecords.Visible = true;
            }

            List<BusinessEntities.Projects> lstgvDomainSubDomain = new List<BusinessEntities.Projects>();

            if (RaveHRViewProject.LstDomain.Count != 0)
            {
                pnlNoDomainRecords.Visible = false;
                divDomainDetails.Visible = true;

                DataTable dtDomain = new DataTable();
                dtDomain.Columns.Add("ID");
                dtDomain.Columns.Add("domain");
                dtDomain.Columns.Add("subDomain");
                dtDomain.Columns.Add("subDomainId");

                for (count = 0; count < RaveHRViewProject.LstDomain.Count; count++)
                {
                    for (int i = 0; i < RaveHRViewProject.LstDomain[count].lstSubDomain.Count; i++)
                    {
                        DataRow dr = dtDomain.NewRow();
                        dr["ID"] = RaveHRViewProject.LstDomain[count].DomainId;
                        dr["domain"] = RaveHRViewProject.LstDomain[count].DomainName;
                        dr["subDomain"] = RaveHRViewProject.LstDomain[count].lstSubDomain[i].SubDomainName;
                        dr["subDomainId"] = RaveHRViewProject.LstDomain[count].lstSubDomain[i].SubDomainId;
                        dtDomain.Rows.Add(dr);
                    }
                }

                gvDomainSubDomain.Visible = true;
                btnAddDomain.Visible = false;
                lblDomains.Visible = false;
                lblSubDomains.Visible = false;
                gvDomainSubDomain.Columns[4].Visible = false;
                gvDomainSubDomain.DataSource = dtDomain;
                gvDomainSubDomain.DataBind();

                Session["SubDomainsTable"] = dtDomain;
            }
            else
            {
                divDomainDetails.Visible = false;
                pnlNoDomainRecords.Visible = true;
            }
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "ViewProjectDetails", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Update Project details.
    /// </summary>    
    private void UpdateProjectDetails(int projId, string SortDir, string SortExpression, string UserMailId, string PresalesRole, string PMRole, string COORole, string RPMRole)
    {
        try
        {
            lblAddProject.Text = "Update Project";

            BusinessEntities.Projects objUpdateProject = new BusinessEntities.Projects();
            objUpdateProject.ProjectId = int.Parse(DecryptQueryString(QueryStringConstants.PROJECTID));

            Rave.HR.BusinessLayer.Projects.Projects objUpdateProjectBAL = new Rave.HR.BusinessLayer.Projects.Projects();

            List<BusinessEntities.Projects> lstRaveHRUpdateProject = new List<BusinessEntities.Projects>();

            lstRaveHRUpdateProject = objUpdateProjectBAL.GetProjectDetails(objUpdateProject, SortDir, SortExpression, UserMailId, PresalesRole, PMRole, COORole, RPMRole, Action, clientNm, projectStatusId, projectsummaryprojectname);
            BusinessEntities.Projects RaveHRUpdateProject = lstRaveHRUpdateProject[0];

            //Making only the required fields read-only on "View Project" 
            ReadOnlyFields();

            //Only the Required fields are editable.            
            if (Roles.IsUserInRole(AuthorizationManagerConstants.ROLERPM))
            {
                ddlStatus.Enabled = true;
                ddlStandardHours.Enabled = true;
                //Siddharth 12th March 2015 --Make Project Model Dropdown enabled when Edit Button is clicked -- Start
                ddlProjectModel.Enabled = true;
                //Siddharth 12th March 2015 --Make Project Model Dropdown enabled when Edit Button is clicked -- End

                //Siddharth 3rd August 2015 --Make Business Vertical Dropdown enabled when Edit Button is clicked -- Start
                ddlBusinessVertical.Enabled = true;
                //Siddharth 3rd August 2015 --Make Business Vertical Dropdown enabled when Edit Button is clicked -- End

                //imgCalStartDate.Enabled = false;
                //imgCalEndDate.Enabled = true;
                //txtEndDate.ReadOnly = false;
                //txtDescription.ReadOnly = false;
                txtDescription.Enabled = true;

                //Rakesh : HOD for Employees 12/07/2016 Begin   
                ddlProjectHead.Enabled = true;
                //Rakesh : HOD for Employees 12/07/2016 End
            }
            else if (Roles.IsUserInRole(AuthorizationManagerConstants.ROLEPRESALES))
            {
                ddlStandardHours.Enabled = true;
                //imgCalStartDate.Visible = false;
                //imgCalEndDate.Visible = false;
                //imgCalStartDate.Enabled = false;
                //imgCalEndDate.Enabled = false;
                //txtDescription.ReadOnly = false;
                txtDescription.Enabled = true;
            }

            ddlProjectGroup.Enabled = true;
            ddlProjectGroup.Visible = true;
            txtProjectGroup.Visible = false;
            ddlLocation.Visible = false;
            txtLocation.Visible = true;
            txtDescription.ReadOnly = false;
            ddlOnGoingProjectStatus.Enabled = true;

            txtProjectID.Text = RaveHRUpdateProject.ProjectId.ToString();
            txtProjectCode.Text = RaveHRUpdateProject.ProjectCode;
            txtClientName.Text = RaveHRUpdateProject.ClientName;
            txtProjectName.Text = RaveHRUpdateProject.ProjectName;
            txtLocation.Text = RaveHRUpdateProject.Location;

            // Mohamed : Issue  : 26/09/2014 : Starts                        			  
            // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
            ddlPrjDivision.Enabled = true;
            ddlPrjDivision.Visible = true;
            ddlPrjDivision.ClearSelection();
            if (ddlPrjDivision.Items.FindByValue(RaveHRUpdateProject.ProjectDivision.ToString()) != null)
            {
                ddlPrjDivision.Items.FindByValue(RaveHRUpdateProject.ProjectDivision.ToString()).Selected = true;
            }
            else
            {
                ddlPrjDivision.SelectedIndex = 0;
            }


            //Siddharth 12 March 2015 Start
            ddlProjectModel.ClearSelection();
            if (ddlProjectModel.Items.FindByValue(RaveHRUpdateProject.ProjectModel.ToString()) != null)
            {
                ddlProjectModel.Items.FindByValue(RaveHRUpdateProject.ProjectModel.ToString()).Selected = true;
            }
            //Siddharth 12 March 2015 End

            //Siddharth 3 August 2015 Start
            ddlBusinessVertical.ClearSelection();
            if (ddlBusinessVertical.Items.FindByValue(RaveHRUpdateProject.BusinessVertical.ToString()) != null)
            {
                ddlBusinessVertical.Items.FindByValue(RaveHRUpdateProject.BusinessVertical.ToString()).Selected = true;
            }
            //Siddharth 3 August 2015 End

            ddlProjecBusinessArea.Visible = true;
            ddlProjecBusinessArea.ClearSelection();
            if (ddlProjecBusinessArea.Items.FindByValue(RaveHRUpdateProject.ProjectBussinessArea.ToString()) != null)
            {
                ddlProjecBusinessArea.Items.FindByValue(RaveHRUpdateProject.ProjectBussinessArea.ToString()).Selected = true;
            }
            else
            {
                ddlProjecBusinessArea.SelectedIndex = 0;
            }
            if (RaveHRUpdateProject.ProjectDivision.ToString().Equals(CommonConstants.Project_Division_PublicService))
            {
                ddlProjecBusinessArea.Enabled = true;
            }

            ddlProjectBusinessSegment.Visible = true;
            ddlProjectBusinessSegment.ClearSelection();
            if (ddlProjectBusinessSegment.Items.FindByValue(RaveHRUpdateProject.ProjectBussinessSegment.ToString()) != null)
            {
                ddlProjectBusinessSegment.Items.FindByValue(RaveHRUpdateProject.ProjectBussinessSegment.ToString()).Selected = true;
            }
            else
            {
                ddlProjectBusinessSegment.SelectedIndex = 0;
            }
            if (RaveHRUpdateProject.ProjectBussinessArea.ToString().Equals(CommonConstants.Project_BussinessArea_Solutions))
            {
                ddlProjectBusinessSegment.Enabled = true;
            }

            //txtProjectAlias.ReadOnly = false;
            //txtProjectAlias.Visible = true;
            //txtProjectAlias.Text = RaveHRUpdateProject.ProjectAlias.ToString();
            // Mohamed : Issue  : 23/09/2014 : Ends

            ddlStatus.SelectedValue = RaveHRUpdateProject.ProjectStatus;

            if (RaveHRUpdateProject.StandardHours == "")
            {
                ddlStandardHours.SelectedIndex = 0;
            }
            else
            {
                ddlStandardHours.Items.FindByText(RaveHRUpdateProject.StandardHours).Selected = true;
            }

            if (RaveHRUpdateProject.ProjectGroup == "")
            {
                ddlProjectGroup.SelectedIndex = 0;
            }
            else
            {
                ddlProjectGroup.Items.FindByText(RaveHRUpdateProject.ProjectGroup).Selected = true;
            }

            txtProjectGroup.Text = ddlProjectGroup.SelectedItem.Text;
            txtDescription.Text = RaveHRUpdateProject.Description;
            txtProjectManager.Text = RaveHRUpdateProject.FullName;


            ddlProjectHead.SelectedValue = Convert.ToString(RaveHRUpdateProject.ProjectHeadId);

            ddlOnGoingProjectStatus.SelectedValue = RaveHRUpdateProject.OnGoingProjectStatusID;

            txtOnGoingProjectStatus.Text = ddlOnGoingProjectStatus.SelectedItem.Text;

            if (txtProjectManager.Text != String.Empty)
                imgClearPM.Style.Add("display", "inline");

            hidUpdateProjectManagerID.Value = RaveHRUpdateProject.ResourceId.ToString();

            //txtStartDate.Text = RaveHRUpdateProject.StartDate.ToString(dateFormat);
            //txtEndDate.Text = RaveHRUpdateProject.EndDate.ToString(dateFormat);

            ucDatePickerStartDate.Text = RaveHRUpdateProject.StartDate.ToString(dateFormat);
            ucDatePickerEndDate.Text = RaveHRUpdateProject.EndDate.ToString(dateFormat);

            int count = 0;

            DataTable dtTechnologies = new DataTable();
            dtTechnologies.Columns.Add("category");
            dtTechnologies.Columns.Add("categoryId");
            dtTechnologies.Columns.Add("technologyName");
            dtTechnologies.Columns.Add("technologyID");

            for (count = 0; count < RaveHRUpdateProject.Categories.Count; count++)
            {
                for (int i = 0; i < RaveHRUpdateProject.Categories[count].Technologies.Count; i++)
                {
                    DataRow dr = dtTechnologies.NewRow();
                    dr["category"] = RaveHRUpdateProject.Categories[count].CategoryName;
                    dr["categoryId"] = RaveHRUpdateProject.Categories[count].CategoryId;
                    dr["technologyName"] = RaveHRUpdateProject.Categories[count].Technologies[i].TechnolgoyName;
                    dr["technologyID"] = RaveHRUpdateProject.Categories[count].Technologies[i].TechnologyID;
                    dtTechnologies.Rows.Add(dr);
                }
            }

            gvCategoryTechnology.Visible = true;
            gvCategoryTechnology.DataSource = dtTechnologies;
            gvCategoryTechnology.DataBind();
            Session["TechnologiesTable"] = dtTechnologies;

            DataTable dtDomain = new DataTable();
            dtDomain.Columns.Add("ID");
            dtDomain.Columns.Add("domain");
            dtDomain.Columns.Add("subDomain");
            dtDomain.Columns.Add("subDomainId");

            for (count = 0; count < RaveHRUpdateProject.LstDomain.Count; count++)
            {
                for (int i = 0; i < RaveHRUpdateProject.LstDomain[count].lstSubDomain.Count; i++)
                {
                    DataRow dr = dtDomain.NewRow();
                    dr["ID"] = RaveHRUpdateProject.LstDomain[count].DomainId;
                    dr["domain"] = RaveHRUpdateProject.LstDomain[count].DomainName;
                    dr["subDomain"] = RaveHRUpdateProject.LstDomain[count].lstSubDomain[i].SubDomainName;
                    dr["subDomainId"] = RaveHRUpdateProject.LstDomain[count].lstSubDomain[i].SubDomainId;
                    dtDomain.Rows.Add(dr);
                }
            }

            gvDomainSubDomain.Visible = true;
            gvDomainSubDomain.DataSource = dtDomain;
            gvDomainSubDomain.DataBind();
            Session["SubDomainsTable"] = dtDomain;

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "UpdateProjectDetails", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    #region For Create Resource Plan

    /// <summary>
    /// method for create resource plan
    /// </summary>
    private void CreateResourcePlan(string mode, int projectId)
    {
        //assign resourcePlanId
        BusinessEntities.RaveHRCollection objListRPIdByProjectId = new BusinessEntities.RaveHRCollection();
        objListRPIdByProjectId = GetResourcePlanIdByProjectId(Mode.Update.ToString(), projectId);

        //check if resourcePlanId contains some value. 
        if (objListRPIdByProjectId.Count > 0)
        {
            //checks authorization
            bool bCheckAccess = AuthorizeUserForPageOperations(new object[] { AuthorizationManagerConstants.OPERATION_PAGE_EDITMAINRP_VIEW });

            if (bCheckAccess)
            {
                btnEditResourcePlan.CommandName = ResourcePlanMode.EditResourcePlan.ToString();
                //enables visibility of edit resource plan button
                btnEditResourcePlan.Visible = true;
            }
            else
            {
                //enables visibility of create resource plan button
                btnEditResourcePlan.Visible = false;
            }
        }
        else
        {
            //checks authorization
            bool bCheckAccess = AuthorizeUserForPageOperations(new object[] { AuthorizationManagerConstants.OPERATION_PAGE_CREATERP_VIEW });

            if (bCheckAccess)
            {
                //replace edit by create
                btnEditResourcePlan.Text = "Create Resource Plan";
                btnEditResourcePlan.CommandName = ResourcePlanMode.CreateResourcePlan.ToString();
                //enables visibility of create resource plan button
                btnEditResourcePlan.Visible = true;
            }
            else
            {
                //enables visibility of create resource plan button
                btnEditResourcePlan.Visible = false;
            }
        }
    }

    #endregion For Create Resource Plan

    #region For View Resource Plan

    /// <summary>
    /// method for view resource plan
    /// </summary>
    private void ViewResourcePlan(string mode, int projectId)
    {
        //assign resourcePlanId
        BusinessEntities.RaveHRCollection objListRPIdByProjectId = GetResourcePlanIdByProjectId(Mode.View.ToString(), projectId);

        //check if resourcePlanId contains some value. 
        if (objListRPIdByProjectId.Count == 0)
        {
            bool bCheckAccess = AuthorizeUserForPageOperations(new object[] { AuthorizationManagerConstants.OPERATION_PAGE_CREATERP_VIEW });
            if (bCheckAccess)
            {
                //replace edit by create
                btnEditResourcePlan.Text = "Create Resource Plan";
                btnEditResourcePlan.CommandName = ResourcePlanMode.CreateResourcePlan.ToString();
                //enables visibility of edit resource plan button
                btnEditResourcePlan.Visible = true;
            }
            else
            {
                btnEditResourcePlan.Visible = false;
            }
        }
        else
        {
            bool bCheckAccess = AuthorizeUserForPageOperations(new object[] { AuthorizationManagerConstants.OPERATION_PAGE_VIEWRP_VIEW });
            if (bCheckAccess)
            {
                //replace edit by view
                btnEditResourcePlan.Text = "View Resource Plan";
                btnEditResourcePlan.CommandName = ResourcePlanMode.ViewResourcePlan.ToString();
                //enables visibility of view resource plan button
                //Ishwar Patil 40019 08092014 Start
                //btnEditResourcePlan.Visible = true;
                btnEditResourcePlan.Visible = false;
                //Ishwar Patil 40019 08092014 End
            }
            else
            {
                btnEditResourcePlan.Visible = false;
            }
        }
    }

    #endregion For Edit Resource Plan

    /// <summary>
    /// Method to get ProjectId,SortExpression and SortDirection
    /// </summary>
    private void GetProjectIdSortExpAndDirection()
    {
        if (Convert.ToInt32(ViewState["projectId"]) == 0)
            projectId = int.Parse(DecryptQueryString(QueryStringConstants.PROJECTID));
        else
            projectId = Convert.ToInt32(ViewState["projectId"]);

        if ((hidSortExpr.Value.ToString()) == "")
            SortExpression = DecryptQueryString(QueryStringConstants.SORTEXPRESSION);
        else
            SortExpression = hidSortExpr.Value.ToString();

        if ((hidSortDir.Value.ToString()) == "")
            SortDir = DecryptQueryString(QueryStringConstants.SORTDIRECTION);
        else
            SortDir = hidSortDir.Value.ToString();

    }

    /// <summary>
    /// get contractType for project
    /// </summary>
    private List<BusinessEntities.ContractProject> GetContractType()
    {
        //--Set entity
        BusinessEntities.ContractProject objBEGetContractsForProject = new BusinessEntities.ContractProject();
        objBEGetContractsForProject.ProjectID = int.Parse(DecryptQueryString(QueryStringConstants.PROJECTID)); ;

        //--Get contracts for project                
        Rave.HR.BusinessLayer.Contracts.ContractProject objBLLGetContractsForProject = new Rave.HR.BusinessLayer.Contracts.ContractProject();
        List<BusinessEntities.ContractProject> objListGetContractsForProject = new List<BusinessEntities.ContractProject>();

        objListGetContractsForProject = objBLLGetContractsForProject.GetContractsForProject(objBEGetContractsForProject);
        return objListGetContractsForProject;
    }

    /// <summary>
    /// Get message body.
    /// </summary>
    private string GetMessageBody(string strToUser, string strFromUser, string projectName, string strProjectSummaryLink, string strProjectStatus, string strStartDate, string strEndDate, string strClientName)
    {
        HtmlForm htmlFormBody = new HtmlForm();
        try
        {
            StringBuilder strMessageBody = new StringBuilder();
            if (strProjectStatus == ProjectStatus.Closed.ToString())
            {
                strMessageBody.Append("Dear All," + "<br/>"
                    + "This is to bring to your notice that the project [" + projectName + "] has been closed." + "<br/><br/>" +
                       "Project Start Date :" + strStartDate + "<br/><br/>" +
                       "Project End Date  :" + strEndDate + "<br/><br/>" +
                       "Client Name :" + strClientName + "<br/><br/>" + "Regards," + "<br/>RMO Team<br/>");
            }
            else
            {
                strMessageBody.Append("Dear All," + "<br/>"
                       + "This is to bring to your notice that the project [" + projectName + "] has been edited." + "<br/><br/>" +
                       "Project Start Date :" + strStartDate + "<br/><br/>" +
                       "Project End Date  :" + strEndDate + "<br/><br/>" +
                       "Client Name :" + strClientName + "<br/><br/>" +
                       "To view the edited project, kindly click on the link provided" + "<br/><a href=" + strProjectSummaryLink + ">" + strProjectSummaryLink + "</a><br/><br/>" + "" + "If you face problem opening this link, please copy and paste the given URL into your browser or contact the RMS Helpdesk." +
                        "<br/>" + "<br/>" + "Regards," + "<br/>RMO Team<br/>");
            }
            htmlFormBody.Style.Add(HtmlTextWriterStyle.FontFamily, "5");
            htmlFormBody.Style.Add(HtmlTextWriterStyle.FontWeight, "10");
            htmlFormBody.InnerText = strMessageBody.ToString();
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetMessageBody", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }

        return htmlFormBody.InnerText.ToString();
    }

    /// <summary>
    /// Validate Project can be closed or not.
    /// </summary>
    private bool ValidateProjectStatus()
    {
        bool Flag = true;
        lblMandatory.Text = "";

        BusinessEntities.RaveHRCollection objValidateProjectStatus = new BusinessEntities.RaveHRCollection();
        objValidateProjectStatus = GetAllocatedResourceByProjectId();

        //Project cannot be closed if resource is already allocated to this project
        if (objValidateProjectStatus.Count > 0)
        {
            lblMandatory.Text = CommonConstants.ProjectErrorMsg_SaveProject;
            Flag = false;
        }

        #region Code Commented for new CR

        /* Code Commented for new CR in which
           Project can be closed whether Resource Plan is active or not
         */

        //if Resource Plan is active project cannot be closed.
        //objValidateProjectStatus = new BusinessEntities.RaveHRCollection();
        //objValidateProjectStatus = GetResourcePlanIdByProjectId(string.Empty, 0);
        //if (objValidateProjectStatus.Count > 0)
        //{
        //    lblMandatory.Text = CommonConstants.ProjectErrorMsg_ResourcePlanIsActive;
        //    Flag = false;
        //}

        #endregion Code Commented for new CR

        lblMandatory.Style.Add(HtmlTextWriterStyle.Color, "Red");

        return Flag;
    }

    //Get SubDomain Id By DomainId
    private int GetSubDomainIdByDomainId()
    {
        string DomainID = ddlDomain.SelectedItem.Value.ToString();
        int Id = 0;
        if (DomainID != "Select")
        {
            //ddlSubDomain.Enabled = true;

            List<BusinessEntities.SubDomain> lstSubDomain = new List<BusinessEntities.SubDomain>();
            Rave.HR.BusinessLayer.Projects.Projects objSubDomainBAL = new Rave.HR.BusinessLayer.Projects.Projects();

            lstSubDomain = objSubDomainBAL.GetSubDomain(Convert.ToInt32(DomainID));

            //loops to get subdomainId value for corressponding domain
            foreach (BusinessEntities.SubDomain objList in lstSubDomain)
            {
                if (objList.SubDomainName == "None")
                {
                    Id = objList.SubDomainId;
                }
            }
            return Id;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// Method To Technology And Category.
    /// </summary>
    private void AddCategoryTechnology(BusinessEntities.Category category, BusinessEntities.Technology technology, ref int CategoryId, ref int TechNologyId)
    {
        Rave.HR.BusinessLayer.Projects.Projects objSubDomainBAL = new Rave.HR.BusinessLayer.Projects.Projects();
        objSubDomainBAL.AddCategoryTechnology(category, technology, ref CategoryId, ref TechNologyId);
    }

    /// <summary>
    /// Method To Add New Technology And Category.
    /// </summary>>
    private void AddCateGoryAndTechnologyForProjects(List<BusinessEntities.Technology> lstTechnologyID, BusinessEntities.Projects objCurrentProjectDetail,
        bool IsTechnologyDetailsGridUpdated)
    {
        for (int i = 0; i < gvCategoryTechnology.Rows.Count; i++)
        {

            Label lblTechNoID = (Label)gvCategoryTechnology.Rows[i].FindControl("lblTechnologyID");
            Label lblTechnology = (Label)gvCategoryTechnology.Rows[i].FindControl("lblTechnology");

            BusinessEntities.Technology objTechnology = new BusinessEntities.Technology(0, null, 0);

            Label lblCategoryID = (Label)gvCategoryTechnology.Rows[i].FindControl("lblCategoryID");
            Label lblCategory = (Label)gvCategoryTechnology.Rows[i].FindControl("lblCategory");

            BusinessEntities.Category objCategory = new BusinessEntities.Category();

            /*checks if Category And Technology is already their
            * if not than update it in master*/
            if ((Regex.IsMatch(lblCategoryID.Text, _numericExpression)))
            {
                objCategory.CategoryId = int.Parse(lblCategoryID.Text);
                objCategory.CategoryName = lblCategory.Text;
            }
            else
            {
                objCategory.CategoryName = lblCategory.Text;
            }


            if ((Regex.IsMatch(lblTechNoID.Text, _numericExpression)))
            {
                // Name does not match schema
                objTechnology.TechnologyID = int.Parse(lblTechNoID.Text);
                objTechnology.TechnolgoyName = lblTechnology.Text;
                lstTechnologyID.Add(objTechnology);
            }
            else
            {
                objTechnology.TechnolgoyName = lblTechnology.Text;
                AddCategoryTechnology(objCategory, objTechnology, ref _categoryId, ref _techNologyId);
                objTechnology.TechnologyID = _techNologyId;

                //checks if category is also new
                if (_categoryId == 0)
                    objTechnology.CategoryID = objCategory.CategoryId;
                else
                    objTechnology.CategoryID = _categoryId;

                lstTechnologyID.Add(objTechnology);
            }

            CheckTechnoLogyGridUpdated(objCurrentProjectDetail, IsTechnologyDetailsGridUpdated, objTechnology);

        }
    }


    /// <summary>
    /// Method To Add New Domain And SubDomain.
    /// </summary>>
    private void AddDomainAndSubDomainForProjects(List<BusinessEntities.SubDomain> lstSubDomainID, BusinessEntities.Projects objCurrentProjectDetail,
        bool IsDomainDetailsGridUpdated)
    {

        for (int i = 0; i < gvDomainSubDomain.Rows.Count; i++)
        {

            Label lblDomainId = (Label)gvDomainSubDomain.Rows[i].FindControl("hdnDomainId");
            Label lblDomain = (Label)gvDomainSubDomain.Rows[i].FindControl("lblDomain");

            BusinessEntities.Domain objDomain = new BusinessEntities.Domain();

            Label lblSubDomainID = (Label)gvDomainSubDomain.Rows[i].FindControl("lblSubDomainID");
            Label lblSubDomain = (Label)gvDomainSubDomain.Rows[i].FindControl("lblSubDomain");

            BusinessEntities.SubDomain objSubDomain = new BusinessEntities.SubDomain();

            /*checks if Domain And SubDomain is already their
            * if not than update it in master*/
            if ((Regex.IsMatch(lblDomainId.Text, _numericExpression)))
            {
                objDomain.DomainId = int.Parse(lblDomainId.Text);
                objDomain.DomainName = lblDomain.Text;
            }
            else
            {
                objDomain.DomainName = lblDomain.Text;
            }


            if ((Regex.IsMatch(lblSubDomainID.Text, _numericExpression)))
            {
                // Name does not match schema
                objSubDomain.SubDomainId = int.Parse(lblSubDomainID.Text);
                objSubDomain.SubDomainName = lblSubDomain.Text;
                lstSubDomainID.Add(objSubDomain);
            }
            else
            {
                objSubDomain.SubDomainName = lblSubDomain.Text;
                AddDomainAndSubDomain(objDomain, objSubDomain, ref _domainId, ref _subDomainId);
                objSubDomain.SubDomainId = _subDomainId;

                //checks if domain is also new
                if (_domainId == 0)
                    objSubDomain.DomainId = objDomain.DomainId;
                else
                    objSubDomain.DomainId = _domainId;

                lstSubDomainID.Add(objSubDomain);

            }

            ChecksDomainGridUpdated(objCurrentProjectDetail, IsDomainDetailsGridUpdated, objSubDomain);
        }
    }

    /// <summary>
    /// Method To Add Domain And SubDomain.
    /// </summary>
    private void AddDomainAndSubDomain(BusinessEntities.Domain objDomain, BusinessEntities.SubDomain objSubDomain, ref int DomainId, ref int SubDomainId)
    {
        Rave.HR.BusinessLayer.Projects.Projects objSubDomainBAL = new Rave.HR.BusinessLayer.Projects.Projects();
        objSubDomainBAL.AddDomainAndSubDomain(objDomain, objSubDomain, ref  DomainId, ref  SubDomainId);
    }

    /// <summary>
    /// Checks Technology Grid Updated Or Not
    /// </summary>
    private void CheckTechnoLogyGridUpdated(BusinessEntities.Projects objCurrentProjectDetail, bool IsTechnologyDetailsGridUpdated,
        BusinessEntities.Technology objTechnology)
    {
        IsTechnologyDetailsGridUpdated = true;
        for (int j = 0; j < objCurrentProjectDetail.Categories.Count; j++)
        {
            if (IsTechnologyDetailsGridUpdated == false)
            {
                break;
            }
            for (int k = 0; k < objCurrentProjectDetail.Categories[j].Technologies.Count; k++)
            {
                if (objTechnology.TechnologyID == objCurrentProjectDetail.Categories[j].Technologies[k].TechnologyID)
                {
                    IsTechnologyDetailsGridUpdated = false;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Checks Domain Grid Updated Or Not
    /// </summary>
    private void ChecksDomainGridUpdated(BusinessEntities.Projects objCurrentProjectDetail, bool IsDomainDetailsGridUpdated, BusinessEntities.SubDomain objSubDomain)
    {
        for (int j = 0; j < objCurrentProjectDetail.LstDomain.Count; j++)
        {
            if (IsDomainDetailsGridUpdated == false)
            {
                break;
            }
            for (int k = 0; k < objCurrentProjectDetail.LstDomain[j].lstSubDomain.Count; k++)
            {
                if (objSubDomain.SubDomainId == objCurrentProjectDetail.LstDomain[j].lstSubDomain[k].SubDomainId)
                {
                    IsDomainDetailsGridUpdated = false;
                    break;
                }
            }
        }
    }

    #endregion Private Member Functions

    #region Public Member Functions
    //Rakesh : HOD for Employees 11/07/2016 Begin   
    void BindProject_Head_Dropdown()
    {
        Rave.HR.BusinessLayer.Employee.Employee objEmployee = new Rave.HR.BusinessLayer.Employee.Employee();
        ddlProjectHead.BindDropdown(objEmployee.Get_HOD_Employees(), "FullName", "EmpId");
    }
    //Rakesh : HOD for Employees 11/07/2016 End   

    /// <summary>
    /// metho for ReadOnlyFields
    /// </summary>
    public void ReadOnlyFields()
    {
        try
        {
            lblProjectID.Visible = true;
            //txtProjectID.Visible = true;
            txtProjectCode.Visible = true;
            txtProjectID.Enabled = true;
            txtProjectID.ReadOnly = true;
            txtProjectCode.ReadOnly = true;
            txtClientName.ReadOnly = true;
            txtClientName.Enabled = true;
            txtProjectName.ReadOnly = true;
            txtProjectName.Enabled = true;

            // Mohamed : Issue  : 26/09/2014 : Starts                        			  
            // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
            ddlPrjDivision.Enabled = false;
            ddlProjecBusinessArea.Enabled = false;
            ddlProjectBusinessSegment.Enabled = false;
            //txtProjectAlias.ReadOnly = true;
            //txtProjectAlias.Enabled = true;
            // Mohamed : Issue  : 23/09/2014 : Ends

            ddlStatus.Enabled = false;
            txtStatus.ReadOnly = true;
            ddlLocation.Enabled = false;
            txtLocation.ReadOnly = true;
            ddlStandardHours.Enabled = false;
            txtStandardHours.ReadOnly = true;
            ddlProjectGroup.Enabled = false;
            txtProjectGroup.ReadOnly = true;
            txtDescription.ReadOnly = true;
            txtDescription.Enabled = true;
            ucDatePickerStartDate.IsEnable = false;
            ucDatePickerEndDate.IsEnable = true;
            ddlOnGoingProjectStatus.Enabled = false;
            txtOnGoingProjectStatus.ReadOnly = true;

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex,
                Sources.PresentationLayer, "AddProject.aspx", "ReadOnlyFields", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    //Code Commented as new method is used for mailing.
    ///// <summary>
    ///// sends mail when either of them occurs : Add Project,Approve Project,Reject Project.
    ///// </summary>
    //public void MailMessage(string mode)
    //{
    //    try
    //    {
    //        if (mode == Mode.Update.ToString())
    //        {
    //            MailMessage message = new MailMessage();
    //            MailAddress fromMailAddres = new MailAddress(UserMailId, UserDisplayName);

    //            if (fromMailAddres != null)
    //                message.From = fromMailAddres;

    //            //1) Logged-in user is RPM. MailTo is PM if assigned and self-cc.
    //            if (RPMRole == AuthorizationManagerConstants.ROLERPM)
    //            {
    //                if (hidProjectManagerEmail.Value != "")
    //                {
    //                    message.To.Add(hidProjectManagerEmail.Value);
    //                    strContentProjectUpdated = strContentProjectUpdated.Replace("[User]", hidProjectManagerName.Value);
    //                }
    //                else
    //                {
    //                    message.To.Add(UserMailId);
    //                    strContentProjectUpdated = strContentProjectUpdated.Replace("[User]", UserDisplayName);
    //                }

    //                if (hidProjectManagerEmail.Value != "")
    //                {
    //                    message.CC.Add(UserMailId);
    //                    strContentProjectUpdated = strContentProjectUpdated.Replace("[User]", UserDisplayName);
    //                }
    //            }

    //            //2) Logged-in user is Presales. MailTo is RPM and CC to PM and self-cc.
    //            if (PresalesRole == AuthorizationManagerConstants.ROLEPRESALES)
    //            {
    //                if (RPMEmailId != "")
    //                {
    //                    message.To.Add(RPMEmailId);
    //                    strContentProjectUpdated = strContentProjectUpdated.Replace("[User]", RPMFinalDisplayName);
    //                }

    //                if (RPMEmailId == "")
    //                {
    //                    if (hidProjectManagerEmail.Value != "")
    //                    {
    //                        message.To.Add(hidProjectManagerEmail.Value);
    //                        strContentProjectUpdated = strContentProjectUpdated.Replace("[User]", hidProjectManagerName.Value);
    //                    }
    //                }
    //                else
    //                {
    //                    if (hidProjectManagerEmail.Value != "")
    //                    {
    //                        message.CC.Add(hidProjectManagerEmail.Value);
    //                        strContentProjectUpdated = strContentProjectUpdated.Replace("[User]", hidProjectManagerName.Value);
    //                    }
    //                }

    //                if (RPMEmailId == "")
    //                {
    //                    if (hidProjectManagerEmail.Value == "")
    //                    {
    //                        message.To.Add(UserMailId);
    //                        strContentProjectUpdated = strContentProjectUpdated.Replace("[User]", UserDisplayName);
    //                    }
    //                    else
    //                    {
    //                        message.CC.Add(UserMailId);
    //                        strContentProjectUpdated = strContentProjectUpdated.Replace("[User]", UserDisplayName);
    //                    }
    //                }
    //                else
    //                {
    //                    message.CC.Add(UserMailId);
    //                    strContentProjectUpdated = strContentProjectUpdated.Replace("[User]", UserDisplayName);
    //                }
    //            }

    //            //3) Logged-in user is PM. MailTo is RPM if assigned and self-cc.
    //            if (PMRole == AuthorizationManagerConstants.ROLEPROJECTMANAGER)
    //            {
    //                if (RPMEmailId != "")
    //                {
    //                    message.To.Add(RPMEmailId);
    //                    strContentProjectUpdated = strContentProjectUpdated.Replace("[User]", RPMFinalDisplayName);
    //                }

    //                if (RPMEmailId == "")
    //                {
    //                    message.To.Add(UserMailId);
    //                    strContentProjectUpdated = strContentProjectUpdated.Replace("[User]", UserDisplayName);
    //                }
    //                else
    //                {
    //                    message.CC.Add(UserMailId);
    //                    strContentProjectUpdated = strContentProjectUpdated.Replace("[User]", UserDisplayName);
    //                }
    //            }

    //            strSubjectProjectUpdated = strSubjectProjectUpdated.Replace("(ProjectId)", txtProjectID.Text);
    //            strSubjectProjectUpdated = strSubjectProjectUpdated.Replace("(ClientName)", txtClientName.Text);
    //            strSubjectProjectUpdated = strSubjectProjectUpdated.Replace("(ProjectName)", txtProjectName.Text);

    //            message.Subject = strSubjectProjectUpdated;

    //            //Following is name of person from message body after word Hello,
    //            //strContentProjectUpdated = strContentProjectUpdated.Replace("[COO]", COODisplayName);

    //            ProjectName = txtProjectName.Text;

    //            strContentProjectUpdated = strContentProjectUpdated.Replace("[ProjectName]", txtProjectName.Text);
    //            strContentProjectUpdated = strContentProjectUpdated.Replace("[ApproveRejectProjectLink]", "<a href=" + URLForMailing + ">" + URLForMailing + "</a>");

    //            //Following is name of person from message body after word Regards,
    //            strContentProjectUpdated = strContentProjectUpdated.Replace("[LoggedInUser]", UserDisplayName);

    //            message.Body = strContentProjectUpdated;

    //            message.IsBodyHtml = true;

    //            Utility Mail = new Utility();
    //            //BusinessEntities.RaveHRMail raveHRMail = new BusinessEntities.RaveHRMail();
    //            //if (COOEmailId != null)
    //            Mail.SendMail(message);
    //        }
    //    }
    //    catch (RaveHRException ex)
    //    {
    //        throw ex;
    //    }
    //    catch (Exception ex)
    //    {
    //        RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "MailMessage", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
    //        //LogErrorMessage(objEx, lblMessage, CommonConstants.MAILMESSAGE);
    //        LogMailErrorMessage(objEx, ProjectName, mode);
    //    }
    //}

    /// <summary>
    /// method to clear sessin variables
    /// </summary>
    public void SessionClear()
    {
        Session.Remove("TechnologiesTable");
        Session.Remove("SubDomainsTable");
    }

    /// <summary>
    /// Authorize User
    /// </summary>
    public bool AuthorizeUserForPageOperations(object[] operationsId)
    {
        Common.AuthorizationManager.AuthorizationManager objAuthorizationManager = new Common.AuthorizationManager.AuthorizationManager();
        return objAuthorizationManager.AuthorizeUserForPageOperations(operationsId);
    }

    #endregion Public Member Functions


    protected void ddlPrjDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPrjDivision.SelectedValue == CommonConstants.Project_Division_PublicService)
            {
                ddlProjecBusinessArea.Enabled = true;
                ddlProjecBusinessArea.SelectedIndex = 0;
            }
            else
            {
                ddlProjecBusinessArea.Enabled = false;
                ddlProjecBusinessArea.SelectedIndex = 0;

                ddlProjectBusinessSegment.Enabled = false;
                ddlProjectBusinessSegment.SelectedIndex = 0;
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "lbtnSubmit_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    protected void ddlProjecBusinessArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProjecBusinessArea.SelectedValue == CommonConstants.Project_BussinessArea_Solutions)
            {
                ddlProjectBusinessSegment.Enabled = true;
                ddlProjectBusinessSegment.SelectedIndex = 0;
            }
            else
            {
                ddlProjectBusinessSegment.Enabled = false;
                ddlProjectBusinessSegment.SelectedIndex = 0;
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "lbtnSubmit_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
}
