//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           MasterPage.aspx.cs       
//  Author:         vineet.kulkarni
//  Date written:   3/16/2009/ 10:00:00 AM
//  Description:    The Master page handles common functionality for all RaveHR System pages. 
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  3/16/2009/ 10:00:00 AM  Prashant Mala    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.DirectoryServices;
using BusinessEntities;
using Common;
using Common.AuthorizationManager;
using System.Web.Security;
using Rave.HR.BusinessLayer.MRF;
using Rave.HR.BusinessLayer.Contract;
using Rave.HR.BusinessLayer.Recruitment;
using Rave.HR.BusinessLayer.Employee;


public partial class MasterPage : BaseMasterPage
{
    #region Public Variables
    public string Temp;
    public string PMRole;
    public string COORole;
    public string RPMRole;
    public string PresalesRole;
    private const string _rpgReportAccess = "RPGReportsAccess";
    private const string _qualityReportAccess = "QualityTeamReportsAccess";
    private const string _recruitmentReportAccess = "RecruitmentTeamReportsAccess";
    private const string _hrReportAccess = "HRTeamReportsAccess";
    //27622-Ambar
    //Ishwar 26022015 : Start --Desc : Report is not required
    //private const string _SpecialReportAccess = "SpecialReport";
    //Ishwar 26022015 : End

    //36581-Ambar
    private const string _OrgChartDetailsReportAccess = "OrgChartDetailsReport";

    /// <summary>
    /// Defines a constant for Page Name used in each catch block 
    /// </summary>
    private const string CLASS_NAME = "ProjectSummary.aspx";
    BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
    AuthorizationManager objAuMan = new AuthorizationManager();

    ArrayList arrRolesForUser = new ArrayList();
    #endregion Public Variables

    #region Protected Methods
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string AppPath = HttpContext.Current.Request.Url.AbsoluteUri.Substring(0, HttpContext.Current.Request.Url.AbsoluteUri.LastIndexOf('/')) + "/LogOut.htm";
            lbtnLogout.Attributes.Add("onclick", "javascript:logout(" + AppPath + ")");
            if (!IsPostBack)
            {

                AuthorizeUser();

                if (AuthorizationManager.GetCurrentPageName() != AuthorizationManagerConstants.PAGEHOME)
                {
                    //--Authorize user for project menu tab
                    //Please do'nt uncomment the code -- sunil
                    //AuthorizeUserForProjectMenu();
                }

                //ApplyCssToSubMenu();

                //Make visible to tab if user have access right.
                //AccessRightForContract();


                //--Authorize user for project menu tab
                AuthorizeUserForProjectMenu();
                //--Css for project tabs
                ApplyCssToSubMenu();
                //Make visible to tab if user have access right.Contract
                AccessRightForContract();

                //Make visible to tab if user have access right.MRF
                AccessRightForMRF();

                //Make visible to tab if user have access right.Recruitment
                AccessRightForRecrutment();

                AccessRightForEmployee();

                //Make visible to tab if user have access right.Reports
                AcessForRMSReports();

                AccessRightFor4C();

                //Ishwar Patil : Trainging Module 12/05/2014 : Starts
                AccessTrainingModule();
                //Ishwar Patil : Trainging Module 12/05/2014 : End


            }

            //////--Authorize user for project menu tab
            //AuthorizeUserForProjectMenu();
            ////--Css for project tabs
            //ApplyCssToSubMenu();
            ////Make visible to tab if user have access right.Contract
            //AccessRightForContract();

            ////Make visible to tab if user have access right.MRF
            //AccessRightForMRF();

            ////Make visible to tab if user have access right.Recruitment
            //AccessRightForRecrutment();

            //AccessRightForEmployee();

            ////Make visible to tab if user have access right.Reports
            //AcessForRMSReports();

            //AccessRightFor4C();


        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "Page_Load", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void lbtnLogOut_Click(object sender, EventArgs e)
    {
        string strScript = "<script>window.open('','_self');window.close();</script>";
        Response.Write(strScript);
    }

    #region Projects
    protected void fnRedirectToProjSummary(object sender, EventArgs e)
    {
        //clear all the session value
        Session["ProjectSummaryProjectName"] = null;
        Session["ClientName"] = null;
        Session["ProjectStatus"] = null;
        Session["WorkFlowStatus"] = null;
        Session["sortDirection"] = null;
        Session["PreviousSortExpression"] = null;

        Response.Redirect(CommonConstants.PROJECTSUMMARY_PAGE, false);
    }

    protected void fnRedirectToReports(object sender, EventArgs e)
    {
        Response.Redirect(CommonConstants.Reports_PAGE, false);
    }
    #endregion Projects

    #region MRF
    protected void fnRedirectToRaiseMRF(object sender, EventArgs e)
    {
        //clear all the session value
        if (MRFRoles.CheckRolesRaiseMrf())
            Response.Redirect("MrfRaisePrevious.aspx", false);
        else
            Response.Redirect("Home.aspx", false);

    }
    protected void fnRedirectMrfPendingApproval(object sender, EventArgs e)
    {
        //clear all the session value
        if (MRFRoles.CheckRolesPendingApproval())
        {
            //All the sessions are cleared before redirecting to page.
            Session[SessionNames.PREVIOUS_SORT_EXPRESSION_APPREJMRF] = null;
            Session[SessionNames.SORT_DIRECTION_APPREJMRF] = null;

            Response.Redirect("MrfPendingApproval.aspx", false);
        }
        else
        {
            Response.Redirect("Home.aspx", false);
        }
    }

    protected void fnRedirectMrfSummary(object sender, EventArgs e)
    {
        //clear all the session value
        if (MRFRoles.CheckRolesMrfSummary())
        {
            //All the sessions are cleared before redirecting to page.
            Session[SessionNames.DEPARTMENT_ID] = null;
            Session[SessionNames.PROJECT_ID_MRF] = null;
            Session[SessionNames.ROLE] = null;
            Session[SessionNames.MRF_STATUS_ID] = null;
            Session[SessionNames.SORT_DIRECTION_MRF] = null;
            Session[SessionNames.CURRENT_PAGE_INDEX_MRF] = null;
            Session[SessionNames.PREVIOUS_SORT_EXPRESSION_MRF] = null;
            Session[SessionNames.CURRENT_PAGE_INDEX_PENDING_ALLOCATION] = null;
            //Venkatesh Start: Session null
            Session[SessionNames.MRFDetail] = null;
            //Venkatesh End: session null
            Response.Redirect("MrfSummary.aspx", false);
        }
        else
        {
            Response.Redirect("Home.aspx", false);
        }
    }

    protected void fnRedirectMrfApproveRejectHeadCount(object sender, EventArgs e)
    {
        //clear all the session value
        if (MRFRoles.CheckRolesApproveRejectHeadCount())
        {
            Response.Redirect("MrfApproveRejectHeadCount.aspx", false);
        }
        else
        {
            Response.Redirect("Home.aspx", false);
        }
    }

    protected void fnRedirectMrfPendingAllocation(object sender, EventArgs e)
    {
        //Venkatesh Start: Session null
        Session[SessionNames.InternalResource] = null;
        //Venkatesh End: Session null
        //clear all the session value
        if (MRFRoles.CheckRolesPendingAllocation())
            Response.Redirect("MrfPendingAllocation.aspx", false);
        else
            Response.Redirect("Home.aspx", false);

    }
    #endregion

    #region Employee

    protected void fnRedirectToAddEmployee(object sender, EventArgs e)
    {
        Response.Redirect("AddEmployee.aspx", false);
    }

    protected void fnRedirectTo4CEmployee(object sender, EventArgs e)
    {
        Response.Redirect("ViewEmp4C.aspx", false);
    }
    // NIS RMS-Venkatesh-Start-29102014
    protected void fnRedirectToConsolidated(object sender, EventArgs e)
    {
        Response.Redirect("ConsolidatedSummary.aspx", false);
    }
    // NIS RMS-Venkatesh-Start-29102014

    // NIS RMS-Siddharth-Start-27th April 2015
    protected void fnRedirectToConsolidatedbyCostcode(object sender, EventArgs e)
    {
        Response.Redirect("ConsolidatedSummarybyCostcode.aspx", false);
    }
    // NIS RMS-Siddharth-End-27th April 2015

    // NIS RMS-Mohamed-Start-08122014
    protected void fnRedirectToSkillReport(object sender, EventArgs e)
    {
        Response.Redirect("SkillReport.aspx", false);
    }
    // NIS RMS-Mohamed-Ends-08122014

    // NIS RMS-Ishwar-Start-16022015
    protected void fnRedirectToMRFAgingReport(object sender, EventArgs e)
    {
        Response.Redirect("MRFAgingReport.aspx", false);
    }
    protected void fnRedirectToMRFAgingForOpenPositionReport(object sender, EventArgs e)
    {
        Response.Redirect("MRFAgingForOpenPositionReport.aspx", false);
    }
    // NIS RMS-Ishwar-Ends-16022015
    
    // NIS RMS-Ishwar-Start-11112014
    protected void fnRedirectToSkillSearchReport(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeSkillSearch.aspx", false);
    }
    // NIS RMS-Ishwar-Start-11112014

    protected void fnRedirectToEmpSummary(object sender, EventArgs e)
    {
        Session[SessionNames.EMP_DEPARTMENTID] = null;
        Session[SessionNames.EMP_PROJECTID] = null;
        Session[SessionNames.EMP_ROLEID] = null;
        Session[SessionNames.EMP_STATUSID] = null;
        Session[SessionNames.EMP_NAME] = null;
        Session[SessionNames.SORT_DIRECTION_EMP] = null;
        Session[SessionNames.CURRENT_PAGE_INDEX_EMP] = null;
        Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] = null;

        // 29884-Ambar-Start-09062012
        Session[SessionNames.REFRESHPAGE] = null;
        // 29884-Ambar-End-09062012

        //Venkatesh Start: Session null 29122014
        Session[SessionNames.CURRENT_PAGE_SIZE_EMP] = null;
        //Venkatesh End: Session null 29122014

        Response.Redirect("EmployeeSummary.aspx", false);
    }

    protected void fnRedirectToEmpDetails(object sender, EventArgs e)
    {
        Response.Redirect("Employee.aspx", false);
    }

    protected void fnRedirectToEmpDetailsProfile(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeDetails.aspx?flag=_detailspage", false);
    }

    #endregion


    protected void fnRedirectToApproveRejectResourcePlan(object sender, EventArgs e)
    {
        Response.Redirect("ApproveRejectRP.aspx", false);
    }

    protected void lbtnLogout_Click(object sender, EventArgs e)
    {
        //Mohamed --Start
        //HttpContext.Current.ApplicationInstance.Session["WindowsUsername"] = null;
        //string strUser = HttpContext.Current.Request.LogonUserIdentity.Name;

        // if (strUser.Contains("olduser"))
        //{

        //    Response.StatusCode = 401;

        //}
        //Mohamed --
        string strScript = "<script>window.open('','_self');window.close();</script>";
        Response.Redirect("LogOut.htm");

        Response.Write(strScript);
    }


    #region Contract

    ///<summary>
    ///This function redirects to the add contract page
    ///</summary>
    protected void fnRedirectToAddContract(object sender, EventArgs e)
    {
        //Remove the session of contract summary page.
        Session.Remove(SessionNames.CONTRACT_CONTRACTTYPE);
        Session.Remove(SessionNames.CONTRACT_CONTRACTSTATUS);
        Session.Remove(SessionNames.CONTRACT_DOCUMENTNAME);
        Session.Remove(SessionNames.CONTRACT_PREVIOUS_SORT_EXPRESSION);
        Session.Remove(SessionNames.CONTRACT_CURRENTPAGEINDEX);
        Session.Remove(SessionNames.CONTRACT_SORTDIRECTIONFORBINDING);
        Session.Remove(SessionNames.CONTRACT_SORT_DIRECTION);
        Session.Remove(SessionNames.CONTRACT_ACTUALPAGECOUNT);
        Session.Remove(SessionNames.CONTRACT_SORTDIRECTIONFORBINDING);

        //Check roles for contract summary page.
        if (ContractRoles.CheckRolesContractSummary())
            Response.Redirect("AddContract.aspx", false);
        else
            Response.Redirect("Home.aspx", false);

    }

    ///<summary>
    ///This function redirects to the Contract Summary page
    ///</summary>
    protected void fnRedirectToContractSummary(object sender, EventArgs e)
    {
        if (ContractRoles.CheckRolesContractSummary())
            Response.Redirect("ContractSummary.aspx", false);
        else
        {
            //Response.Redirect("Home.aspx", false);
            spanContractSummary.Visible = false;
            spanContract.Visible = false;
        }
    }

    #endregion Contract

    #region Recruitment

    protected void fnRedirectToRecruitmentSummary(object sender, EventArgs e)
    {
        if (RecruitmentRoles.CheckRolesForRecruitmentTab())
        {
            Session[SessionNames.DEPARTMENT_ID_RS] = null;
            Session[SessionNames.PROJECT_ID_RS] = null;
            Session[SessionNames.ROLE_RS] = null;
            Session[SessionNames.PREVIOUS_SORT_EXPRESSION_RS] = null;
            Session[SessionNames.SORT_DIRECTION_RS] = null;

            Response.Redirect("RecruitmentSummary.aspx", false);
        }
        else
        {
            Response.Redirect("Home.aspx", false);
        }

    }

    protected void fnRedirectToPipelineDetails(object sender, EventArgs e)
    {
        if (RecruitmentRoles.CheckRolesForRecruitmentTab())
        {
            Response.Redirect("RecruitmentPipelineDetails.aspx", false);
        }
        else
        {
            Response.Redirect("Home.aspx", false);
        }
    }

    #endregion Recruitment


    #region Seat Allocation
    ///<summary>
    ///This function redirects to the Seat Allocation page
    ///</summary>
    protected void fnRedirectToSeatAllocation(object sender, EventArgs e)
    {
        // if (ContractRoles.CheckRolesContractSummary())
        Response.Redirect("SeatAllocation.aspx", false);
        //else
        //{
        //    //Response.Redirect("Home.aspx", false);
        //    Span19.Visible = false;
        //    Span21.Visible = false;
        //}
    }
    #endregion

    #region RMS Reports
    ///<summary>
    ///This function redirects to the Seat Allocation page
    ///</summary>
    protected void fnRedirectToRMSReports(object sender, EventArgs e)
    {
        Response.Redirect("Reports.aspx", false);
    }

    ///<summary>
    ///This function redirects to the head count report page
    ///</summary>
    protected void fnRedirectToHeadCountReport(object sender, EventArgs e)
    {
        Response.Redirect("HeadCountReport.aspx", false);
    }

    #endregion RMS Reports

    //Ishwar Patil : Trainging Module 23/04/2014 : Starts
    #region Raise Training

    ///<summary>
    ///This function redirects to the Raise Training page
    ///</summary>
    //protected void fnRedirectToRaiseTraining(object sender, EventArgs e)
    //{
    //    Response.Redirect("RaiseTrainingRequest.aspx", false);
    //}

    //protected void OnClick_btnTrainingModule(object sender, EventArgs e)
    //{
    //    Response.Redirect("http://rav-vm-srv-096:8024/", false);
    //}

    ///<summary>
    ///This function redirects to the Raise Training Summary page
    ///</summary>
    //protected void fnRedirectToRaiseTrainingSummary(object sender, EventArgs e)
    //{
    //    Response.Redirect("RaiseTrainingSummary.aspx", false);
    //}
    ///<summary>
    ///This function redirects to the Raise Training Summary page
    ///</summary>
    //protected void fnRedirectToApproveReject(object sender, EventArgs e)
    //{
    //    Response.Redirect("ApproveRejectTrainingRequest.aspx", false);
    //}
    #endregion Raise Training
    //Ishwar Patil : Trainging Module 23/04/2014 : End

    #region 4C
    ///<summary>
    ///This function redirects to 4C Feedback
    ///</summary>
    protected void fnRedirectToFourCFeedback(object sender, EventArgs e)
    {
        //Response.Redirect("FourCModule/4C_Add4CFeedback.aspx", false);
        //ViewState["UserMailId"] = null;
        Response.Redirect("4C_Add4CFeedback.aspx", false);
    }

    protected void fnRedirectToFourCAdmin(object sender, EventArgs e)
    {
        //Response.Redirect("FourCModule/4C_UserRights.aspx", false);
        Response.Redirect("4C_UserRights.aspx", false);
    }

    protected void fnRedirectToFourCView(object sender, EventArgs e)
    {
        Response.Redirect("View4CFeedback.aspx", false);
        //Response.Redirect("~/FourCModule/View4CFeedback.aspx", false);
    }

    protected void fnRedirectToFourCReports(object sender, EventArgs e)
    {
        //Response.Redirect("~/FourCModule/4C_Reports.aspx", false);
        Response.Redirect("4C_Reports.aspx", false);
    }

    protected void fnRedirectToLoginPageOfAnotherUser(object sender, EventArgs e)
    {
        //Response.Redirect("~/FourCModule/4C_Reports.aspx", false);
        Response.Redirect("4CLogin.aspx", false);
    }

    #endregion 4C


    #endregion Protected Methods

    #region Private Methods

    /// <summary>
    /// Authorize user for upper tab
    /// </summary>
    /// <returns>void</returns>
    private void AuthorizeUser()
    {
        try
        {
            ArrayList arrRolesForUser = new ArrayList();
            AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
            arrRolesForUser = objRaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());

            //--Add to session 
            Session[SessionNames.AZMAN_ROLES] = arrRolesForUser;

            string UserMailId = objRaveHRAuthorizationManager.getLoggedInUser();
            string[] UserName;
            string UserDisplayName = null;
            string FinalUserName = null;
            char separator = Convert.ToChar(System.Configuration.ConfigurationManager.AppSettings["SplitCharacter"]);

            if (arrRolesForUser.Count == 0)
            {
                ////Response.Redirect(CommonConstants.UNAUTHORISEDUSER);
            }
            UserName = UserMailId.Split('@');
            if (UserName[0].Contains(separator.ToString()))
            {
                UserName = UserName[0].Split(separator);
                for (int i = 0; i < UserName.Length; i++)
                {
                    UserName[i] = ConvertToUpper(UserName[i]);
                    UserDisplayName += UserName[i];

                    if (i < UserName.Length - 1)
                        UserDisplayName += ".";
                }
            }
            else
            {
                FinalUserName = ConvertToUpper(UserName[0]);
                UserDisplayName = FinalUserName;
            }
            lblUserName.Text = lblUserName.Text + " " + UserDisplayName;
            if (arrRolesForUser.Count > 1)
            {
                foreach (string STR in arrRolesForUser)
                {
                    switch (STR)
                    {
                        case AuthorizationManagerConstants.ROLECOO:
                            COORole = AuthorizationManagerConstants.ROLECOO;
                            break;
                        case AuthorizationManagerConstants.ROLERPM:
                            RPMRole = AuthorizationManagerConstants.ROLERPM;
                            break;
                        case AuthorizationManagerConstants.ROLEPRESALES:
                            PresalesRole = AuthorizationManagerConstants.ROLEPRESALES;
                            break;
                        case AuthorizationManagerConstants.ROLEPROJECTMANAGER:
                            PMRole = AuthorizationManagerConstants.ROLEPROJECTMANAGER;
                            break;
                        default:
                            break;
                    }

                }
                if (RPMRole == AuthorizationManagerConstants.ROLERPM)
                {
                    lblRole.Text = AuthorizationManagerConstants.ROLERPM;
                }
                else if (COORole == AuthorizationManagerConstants.ROLECOO)
                {
                    lblRole.Text = AuthorizationManagerConstants.ROLECOO;
                }
                else if (PresalesRole == AuthorizationManagerConstants.ROLEPRESALES)
                {
                    lblRole.Text = AuthorizationManagerConstants.ROLEPRESALES;
                }
                else
                {
                    lblRole.Text = "Project Manager";
                }

            }
            else
            {
                lblUserName.Text = lblUserName.Text;
            }

            //Get the upper tab menu for user based on authorization
            ArrayList arrMainMenuTab = new ArrayList();
            arrMainMenuTab = objRaveHRAuthorizationManager.getUpperTabMenuForUser();

            //foreach (string str in arrRolesForUser)
            //{
            //    Response.Write(str + "<br>");
            //}

            //if (arrMainMenuTab.Contains(CommonConstants.TABPROJECT))
            //{
            //    tabProject.Visible = true;
            //    tabProject1.Visible = false;
            //}
            //else
            //{
            //    tabProject.Visible = false;
            //    tabProject1.Visible = true;
            //}

            if (!arrMainMenuTab.Contains(CommonConstants.TABPROJECT))
            {
                ApplyCssToAccedDeniedMenu(tabProject);
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "AuthorizeUser", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Authorize user for project tab
    /// </summary>
    /// <returns>void</returns>
    private void AuthorizeUserForProjectMenu()
    {
        try
        {
            ArrayList arrPagesAccessForUser = new ArrayList();
            AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
            arrPagesAccessForUser = objRaveHRAuthorizationManager.getProjectTabMenuForUser();

            //--Project summary Sub menu access
            if (arrPagesAccessForUser.Contains(CommonConstants.PROJECTSUMMARY_PAGE)) { divProjectSummary.Visible = true; }
            else { divProjectSummary.Visible = false; }

            //--Approve Reject RPSub menu access
            if (arrPagesAccessForUser.Contains(CommonConstants.APPROVEREJECTRP_PAGE)) { divApproveRejectResourcePlan.Visible = true; }
            else { divApproveRejectResourcePlan.Visible = false; }

            //--Reports menu access
            if (arrPagesAccessForUser.Contains(CommonConstants.Reports_PAGE)) { divReports.Visible = true; }
            else { divReports.Visible = false; spApproveRejectResourcePlan.Visible = false; }

            //--Redirect if not authorized
            //objRaveHRAuthorizationManager.IsUserAuthorizedToPage(arrPagesAccessForUser);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "AuthorizeUserForProjectMenu", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    private void ApplyCssToSubMenu()
    {
        if (((btnApproveRejectResourcePlan.Visible == true) && (divProjectSummary.Visible == true)))
        {
            spProjectSummary.Visible = true;
        }
        else
        {
            spProjectSummary.Visible = false;
        }
    }

    /// <summary>
    /// Apply Css to Main Menus for access denied users
    /// </summary>
    private void ApplyCssToAccedDeniedMenu(HtmlAnchor tab)
    {
        tab.Style[HtmlTextWriterStyle.Display] = "none";
        //tab.Attributes.Remove("onmouseover");
        //tab.Style[HtmlTextWriterStyle.Cursor] = "text";
        //tab.Style[HtmlTextWriterStyle.BackgroundColor] = "#dbffff";
        hhdAmenus.Value = hhdAmenus.Value + "," + tab.ClientID;
    }
    #endregion Private Methods

    #region Public Methods
    public string ConvertToUpper(string InputString)
    {
        InputString = InputString.Substring(0, 1).ToUpper() + InputString.Substring(1, InputString.Length - 1);
        return InputString;
    }

    /* google google
    /// <summary>
    /// Gets the logged in user emailid
    /// </summary>
    /// <returns>string</returns>        
    public string GetDomainUsers(string strUserName)
    {
        //strUserName = strUserName.Replace("@rave-tech.co.in", "");
        //Google change point to northgate
        if (strUserName.ToLower().Contains("@rave-tech.co.in"))
        {
            strUserName = strUserName.Replace("@rave-tech.co.in", "");
        }
        else
        {
            strUserName = strUserName.Replace(AuthorizationManagerConstants.NORTHGATEDOMAIN, "");
        }

        string strUserEmail = string.Empty;

        DirectoryEntry searchRoot = new DirectoryEntry("LDAP://RAVE-TECH.CO.IN");

        DirectorySearcher search = new DirectorySearcher(searchRoot);

        //string query = "(|(objectCategory=group)(objectCategory=user)) ";
        string query = "(SAMAccountName=" + strUserName + ")";

        search.Filter = query;

        SearchResult result;

        SearchResultCollection resultCol = search.FindAll();

        if (resultCol != null)
        {

            for (int counter = 0; counter < resultCol.Count; counter++)
            {

                result = resultCol[counter];

                if (result.Properties.Contains("samaccountname"))
                {

                    strUserEmail = result.Properties["mail"][0].ToString();

                }

            }

        }

        return strUserEmail;
    }
    
     * 
     */

    /// <summary>
    /// Make visible to tab of contracts if user have access rights.
    /// </summary>
    public void AccessRightForContract()
    {
        if (!ContractRoles.CheckRolesContractSummary())
        {
            spanContractSummary.Visible = false;
            spanContract.Visible = false;
            ApplyCssToAccedDeniedMenu(tabContract);
        }
        if ((ContractRoles.CheckRolesAddInternalContract()) || (ContractRoles.CheckRolesAddExternalContract()))
        { }
        else
        {
            spanAddContract.Visible = false;
            spanContract.Visible = false;
        }
    }

    /// <summary>
    /// Make visible to tab of MRF if user have access rights.
    /// </summary>
    public void AccessRightForMRF()
    {
        //TO solved issue id 21336
        //Start 
        ArrayList arrRolesPreSale = new ArrayList();
        arrRolesPreSale = (System.Collections.ArrayList)Session[SessionNames.AZMAN_ROLES];

        if (!MRFRoles.CheckRolesMrfSummary() && !MRFRoles.CheckRolesRaiseMrf() && !MRFRoles.CheckRolesPendingAllocation() && !MRFRoles.CheckRolesPendingApproval() && !MRFRoles.CheckRolesApproveRejectHeadCount())
        {
            ApplyCssToAccedDeniedMenu(tabMRF);
        }


        foreach (string STR in arrRolesPreSale)
        {
            switch (STR)
            {
                case "rolePreSales":
                    spanAppRejHeadCount.Visible = false;
                    spanMRFPendingApproval.Visible = false;
                    spanMRFPendingAllocation.Visible = false;
                    break;
                default:
                    break;
            }

        }
        //End

        //43639-- Jignyasa--Start
        //var chkFunctionalManager = CheckIfFunctionalManager(arrRolesPreSale);

        //if (!chkFunctionalManager && !MRFRoles.CheckRolesMrfSummary() && !MRFRoles.CheckRolesRaiseMrf() && !MRFRoles.CheckRolesPendingAllocation() && !MRFRoles.CheckRolesPendingApproval() && !MRFRoles.CheckRolesApproveRejectHeadCount())
        //{
        //    ApplyCssToAccedDeniedMenu(tabMRF);
        //}

        //if (chkFunctionalManager && !MRFRoles.CheckRolesMrfSummary()) //Those who are only functional manager and not PM they should see only mrfsummary page.
        //{
        //    spanMRFSummary.Visible = true;
        //    spanRaiseMRF.Visible = false;
        //    spanMRFPendingApproval.Visible = false;
        //    spanAppRejHeadCount.Visible = false;
        //    spanMRFPendingAllocation.Visible = false;
        //}
        //43639--Jignyasa--End
    }

    //43639--Jignyasa--Start
    //private bool CheckIfFunctionalManager(ArrayList ArrRoles)
    //{
    //    AuthorizationManager authorizationManager = new AuthorizationManager();
    //    bool functionalManager = authorizationManager.CheckIfFunctionalManager();

    //    Session["IsFunctionalManager"] = functionalManager;

    //    if (functionalManager && !ArrRoles.Contains(AuthorizationManagerConstants.ROLEPROJECTMANAGER))
    //    {
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}
    //43639--Jignyasa--End


    //43639--Jignyasa--End

    //Ishwar Patil : Trainging Module 12/05/2014 : Starts
    private void AccessTrainingModule()
    {
        int AccessRightID = 0;
        string UserMailId;
        int UserEmpID;        
        AuthorizationManager authoriseduser = new AuthorizationManager();
        Rave.HR.BusinessLayer.Training.RaiseTrainingRequest saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
        Rave.HR.BusinessLayer.Common.Master objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();

        UserMailId = authoriseduser.getLoggedInUserEmailId();
        UserEmpID = objRaveHRMasterBAL.GetLoggedInUserID(UserMailId);

        AccessRightID = saveTrainingBL.AccessForTrainingModule(UserEmpID);

        if (AccessRightID == CommonConstants.DefaultFlagMinus)
        {
            //spanRaiseTraining.Visible = true;
            //spanRaiseTrainingSummary.Visible = true;
            //spanApprovRejectRequest.Visible = true;
            tabTraining.Visible = true;

            //spanTrainingModule.Visible = true;
        }
        else
        {
            bool ShowTrainingMenu = Convert.ToBoolean(ConfigurationSettings.AppSettings[CommonConstants.ShowTrainingMenu]);
            if (ShowTrainingMenu)
                tabTraining.Visible = true;
        }
        //else
        //{
        //    if (AccessRightID == CommonConstants.DefaultFlagZero)
        //    {
        //        spanRaiseTraining.Visible = false;
        //        spanRaiseTrainingSummary.Visible = false;
        //        spanApprovRejectRequest.Visible = false;
        //        tabTraining.Visible = false;
        //    }
        //    else
        //    {
        //        spanRaiseTraining.Visible = true;
        //        spanRaiseTrainingSummary.Visible = true;
        //        tabTraining.Visible = true;
        //        //For Approve Reject screen only
        //        if (CommonConstants.ApproveRejectID == UserEmpID)
        //        {
        //            spanApprovRejectRequest.Visible = true;
        //        }
        //        else
        //        {
        //            spanApprovRejectRequest.Visible = false;
        //        }
        //    }
        //}
    }
    //Ishwar Patil : Trainging Module 12/05/2014 : End


    private void AccessRightFor4C()
    {
        int loginEmpId = 0;
        List<string> lstRights;
        string UserRaveDomainId;
        AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
        UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();

        Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
        lstRights = fourCBAL.Check4CLoginRights(UserRaveDomainId.Replace("co.in", "com"), ref loginEmpId);

        tab4C.Visible = false;
        spanAddFeedback.Visible = false;
        span4CAdmin.Visible = false;
        spanViewFourC.Visible = false;
        span4CReports.Visible = false;
        span4CLogin.Visible = false;


        if (lstRights.Exists(o => o.ToString() == MasterEnum.FourCRole.FOURCADMIN.ToString()) || (UserRaveDomainId.ToLower() == "sawita.kamat@rave-tech.co.in" || UserRaveDomainId.ToLower() == "sawita.kamat@northgateps.co.in"))
        {
            tab4C.Visible = true;
            spanAddFeedback.Visible = true;
            span4CAdmin.Visible = true;
            spanViewFourC.Visible = true;
            span4CReports.Visible = true;
            span4CLogin.Visible = true;
        }
        else if (lstRights.Exists(o => o.ToString() == MasterEnum.FourCRole.RMS_FUNCTIONALMANAGER.ToString()) || lstRights.Exists(o => o.ToString() == MasterEnum.FourCRole.VIEWACCESSRIGHTS.ToString()))
        {
            tab4C.Visible = true;
            span4CAdmin.Visible = false;
            Span2.Visible = false;
            spanViewFourC.Visible = true;
            span4CReports.Visible = false;
            Span1.Visible = false;
            span4CLogin.Visible = false;
            Span4.Visible = false;
            Span12.Visible = false;

            if (lstRights.Exists(o => o.ToString() == MasterEnum.FourCRole.CREATOR.ToString()) || lstRights.Exists(o => o.ToString() == MasterEnum.FourCRole.REVIEWER.ToString()))
            {
                spanAddFeedback.Visible = true;
            }
            else
            {
                spanAddFeedback.Visible = false;
            }
            // Mohamed : 13/02/2015 : Starts                        			  
            // Desc : 4C access rights
            if (lstRights.Exists(o => o.ToString() == MasterEnum.FourCRole.REPORTACCESS.ToString()))
            {
                Span1.Visible = true;
                span4CReports.Visible = true;                
            }
            else
            {
                span4CReports.Visible = false;
            }

            // Mohamed : 13/02/2015 : Ends
        }
        else if (lstRights.Exists(o => o.ToString() == MasterEnum.FourCRole.CREATOR.ToString()) || lstRights.Exists(o => o.ToString() == MasterEnum.FourCRole.REVIEWER.ToString()))
        {
            tab4C.Visible = true;
            span4CAdmin.Visible = false;
            Span2.Visible = false;
            spanViewFourC.Visible = false;
            Span12.Visible = false;
            span4CReports.Visible = false;
            Span1.Visible = false;
            span4CLogin.Visible = false;
            Span4.Visible = false;
            spanAddFeedback.Visible = true;
            // Mohamed : 13/02/2015 : Starts                        			  
            // Desc : 4C access rights
            if (lstRights.Exists(o => o.ToString() == MasterEnum.FourCRole.REPORTACCESS.ToString()))
            {
                span4CReports.Visible = true;
            }
            else
            {
                span4CReports.Visible = false;
            }

            // Mohamed : 13/02/2015 : Ends
        }
        #region Modified By Mohamed Dangra
        // Mohamed : 13/02/2015 : Starts                        			  
        // Desc : 4C access rights
        else if (lstRights.Exists(o => o.ToString() == MasterEnum.FourCRole.REPORTACCESS.ToString()))
        {
            tab4C.Visible = true;
            span4CAdmin.Visible = false;
            Span2.Visible = false;
            spanViewFourC.Visible = false;
            Span12.Visible = false;
            span4CReports.Visible = true;
            Span1.Visible = false;
            span4CLogin.Visible = false;
            Span4.Visible = false;
            spanAddFeedback.Visible = false;
        }

        // Mohamed : 13/02/2015 : Ends
        #endregion Modified By Mohamed Dangra
        //My4CView Start
        if (lstRights.Exists(o => o.ToString() == MasterEnum.FourCRole.ViewMy4C.ToString()))
        {
            spanViewMy4C.Visible = true;
        }
        else
        {
            spanViewMy4C.Visible = false;
        }
        //My4CView End



    }

    /// <summary>
    /// Make visible to tab of Recrutment if user have access rights.
    /// </summary>
    public void AccessRightForRecrutment()
    {
        if (!RecruitmentRoles.CheckRolesForRecruitmentTab())
        {
            ApplyCssToAccedDeniedMenu(tabRecruitment);
        }
    }

    public void AccessRightForEmployee()
    {
        AuthorizationManager objAuthMgr = new AuthorizationManager();



        if ((!Rave.HR.BusinessLayer.Employee.Employee.CheckDepartmentHeadbyEmailId(objAuthMgr.getLoggedInUserEmailId())) && (!EmployeeRoles.CheckRolesEmployeeSummaryAndProfile()))
        {
            SpanEmpSummary.Visible = false;
        }

        if (!EmployeeRoles.CheckRolesEmployee())
        {
            SpanAddEmployee.Visible = false;
        }

        // Ishwar - NISRMS - 30102014 Start
        string strUserIdentity = string.Empty;
        strUserIdentity = HttpContext.Current.ApplicationInstance.Session["WindowsUsername"].ToString().Trim();

        BusinessEntities.Employee Employee = new BusinessEntities.Employee();
        Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
        Employee = employeeBL.GetNISEmployeeList(strUserIdentity);
        if (!String.IsNullOrEmpty(Employee.WindowsUserName))
        {
            if (Employee.WindowsUserName.ToUpper() == strUserIdentity.ToUpper())
            {
                //Employee Profile false for EDC team members
                SpanEmployeeProfile.Visible = false;
            }
        }
        else
        {
            string strNISUsers = string.Empty;
            if (ConfigurationManager.AppSettings["NISReportsAccess"] != null)
            {
                strNISUsers = ConfigurationManager.AppSettings["NISReportsAccess"].ToString();
            }
            Common.AuthorizationManager.AuthorizationManager objAuth = new Common.AuthorizationManager.AuthorizationManager();
            if (!strNISUsers.Contains(objAuth.getLoggedInUser()))
            {
                spanSkillSearchReport.Visible = false;
                spanHeadCountReport.Visible = false;
                spanSkillSearchReport.Visible = false;
                spanSkillReport.Visible = false;
                spanConsolidated.Visible = false;
                spanConsolidatedByCostCode.Visible = false;
                // Ishwar - NISRMS - 16022015 Start
                spanMRFAgingReport.Visible = false;
                spanMRFAgingForOpenPositionReport.Visible = false;
                // Ishwar - NISRMS - 16022015 End
            }
        }
        // Ishwar - NISRMS - 30102014 End
    }

    /// <summary>
    /// Check Users Acess Rights For Reports
    /// </summary>
    public void AcessForRMSReports()
    {
        //string strUsers = ConfigurationManager.AppSettings[_reportAccess].ToString();
        string strQualityTeamUsers = string.Empty;
        if (ConfigurationManager.AppSettings[_qualityReportAccess] != null)
        {
            strQualityTeamUsers = ConfigurationManager.AppSettings[_qualityReportAccess].ToString();
        }
        string strRMPTeamUsers = string.Empty;
        if (ConfigurationManager.AppSettings[_rpgReportAccess] != null)
        {
            strRMPTeamUsers = ConfigurationManager.AppSettings[_rpgReportAccess].ToString();
        }

        string strRecruitementTeamUsers = string.Empty;
        if (ConfigurationManager.AppSettings[_recruitmentReportAccess] != null)
        {
            strRecruitementTeamUsers = ConfigurationManager.AppSettings[_recruitmentReportAccess].ToString();
        }

        string strHRTeamUsers = string.Empty;
        if (ConfigurationManager.AppSettings[_hrReportAccess] != null)
        {
            strHRTeamUsers = ConfigurationManager.AppSettings[_hrReportAccess].ToString();
        }
        
        //Ishwar 26022015 : Start --Desc : Report is not required
        ////27622-Ambar-Start
        //string strSpecialUsers = string.Empty;
        //if (ConfigurationManager.AppSettings[_SpecialReportAccess] != null)
        //{
        //  strSpecialUsers = ConfigurationManager.AppSettings[_SpecialReportAccess].ToString();
        //}
        ////27622-Ambar-End
        //Ishwar 26022015 : End


        //36581-Ambar-Start
        string strOrgChartDetailsUsers = string.Empty;
        if (ConfigurationManager.AppSettings[_OrgChartDetailsReportAccess] != null)
        {
            strOrgChartDetailsUsers = ConfigurationManager.AppSettings[_OrgChartDetailsReportAccess].ToString();
        }
        //36581-Ambar-End

        //Ishwar Patil NIS RMS : 31/10/2014 : Start
        string strNISUsers = string.Empty;
        if (ConfigurationManager.AppSettings["NISReportsAccess"] != null)
        {
            strNISUsers = ConfigurationManager.AppSettings["NISReportsAccess"].ToString();
        }
        //Ishwar Patil NIS RMS : 31/10/2014 : End

        Common.AuthorizationManager.AuthorizationManager objAuth =
                                                new Common.AuthorizationManager.AuthorizationManager();


        //36581-Ambar-Edited IF Condiction for strOrgChartDetailsUsers (Org Chart Details Report)
        //27622-Ambar-Edited IF Condiction for strSpecialUsers
        if (!strQualityTeamUsers.Contains(objAuth.getLoggedInUser()) &&
            !strRMPTeamUsers.Contains(objAuth.getLoggedInUser()) &&
            !strRecruitementTeamUsers.Contains(objAuth.getLoggedInUser()) &&
            !strHRTeamUsers.Contains(objAuth.getLoggedInUser()) &&
            //!strSpecialUsers.Contains(objAuth.getLoggedInUser()) &&
            !strOrgChartDetailsUsers.Contains(objAuth.getLoggedInUser()) &&
            !strNISUsers.Contains(objAuth.getLoggedInUser())
          )
        {
            tabRMSReports.Visible = false;
        }

        //Ishwar NISRMS : 24022015 Start
        if (strNISUsers.Contains(objAuth.getLoggedInUser()) &&
            !strQualityTeamUsers.Contains(objAuth.getLoggedInUser()) &&
            !strRMPTeamUsers.Contains(objAuth.getLoggedInUser()) &&
            !strRecruitementTeamUsers.Contains(objAuth.getLoggedInUser()) &&
            !strHRTeamUsers.Contains(objAuth.getLoggedInUser()) &&
            //!strSpecialUsers.Contains(objAuth.getLoggedInUser()) &&
            !strOrgChartDetailsUsers.Contains(objAuth.getLoggedInUser())
          )
        {
            spanRMSReports.Visible = false;
            btnRMSReports.Visible =false;
        }
        //Ishwar NISRMS : 24022015 End
    }


    //Access rights security
    public bool Check4CAccess()
    {
        int loginEmpId = 0;
        List<string> lstRights;
        string UserRaveDomainId;
        AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
        UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();

        Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
        lstRights = fourCBAL.Check4CLoginRights(UserRaveDomainId.Replace("co.in", "com"), ref loginEmpId);

        bool flag = false;


        if (lstRights.Exists(o => o.ToString() == MasterEnum.FourCRole.FOURCADMIN.ToString()) ||
            (UserRaveDomainId.ToLower() == "sawita.kamat@rave-tech.co.in" || UserRaveDomainId.ToLower() == "sawita.kamat@northgateps.co.in") ||
            lstRights.Exists(o => o.ToString() == MasterEnum.FourCRole.RMS_FUNCTIONALMANAGER.ToString()) ||
            lstRights.Exists(o => o.ToString() == MasterEnum.FourCRole.VIEWACCESSRIGHTS.ToString()) ||
            lstRights.Exists(o => o.ToString() == MasterEnum.FourCRole.CREATOR.ToString()) ||
            lstRights.Exists(o => o.ToString() == MasterEnum.FourCRole.REVIEWER.ToString())
            // Mohamed : 13/02/2015 : Starts                        			  
            // Desc : 4C access rights
            || (lstRights.Exists(o => o.ToString() == MasterEnum.FourCRole.REPORTACCESS.ToString()))
            // Mohamed : 13/02/2015 : Ends
            )
        {
            flag = true;
        }

        return flag;
    }


    #endregion Public Methods

}
