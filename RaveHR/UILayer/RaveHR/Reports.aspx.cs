//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           Reports.aspx.cs       
//  Author:         prashant.mala
//  Date written:   12/8/2009/ 10:58:30 AM
//  Description:    Reports page is used to view reports.
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  12/8/2009/ 10:58:30 AM  prashant.mala    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Configuration;
using System.Web;
using System.Text;
using BusinessEntities;
using System.Data.SqlClient;
using Common;
using System.Collections;
using Common.Constants;

public partial class Reports : System.Web.UI.Page
{

    #region Protected Events
    /// <summary>
    /// page load eventhandler.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        string strQualityTeamUsers = string.Empty;
        if (ConfigurationManager.AppSettings["QualityTeamReportsAccess"] != null)
        {
            strQualityTeamUsers = ConfigurationManager.AppSettings["QualityTeamReportsAccess"].ToString();
        }
        string strRMPTeamUsers = string.Empty;
        if (ConfigurationManager.AppSettings["RPGReportsAccess"] != null)
        {
            strRMPTeamUsers = ConfigurationManager.AppSettings["RPGReportsAccess"].ToString();
        }

        string strRecruitmentTeamUsers = string.Empty;
        if (ConfigurationManager.AppSettings["RecruitmentTeamReportsAccess"] != null)
        {
            strRecruitmentTeamUsers = ConfigurationManager.AppSettings["RecruitmentTeamReportsAccess"].ToString();
        }

        string strHRTeamUsers = string.Empty;
        if (ConfigurationManager.AppSettings["HRTeamReportsAccess"] != null)
        {
            strHRTeamUsers = ConfigurationManager.AppSettings["HRTeamReportsAccess"].ToString();
        }
        //Siddhesh Arekar for Task ID is 55884 : 06/01/2015 : Start
        string strRMOGroupName = string.Empty;
        if (ConfigurationManager.AppSettings["HRTeamReportsAccess"] != null)
        {
            strRMOGroupName = ConfigurationManager.AppSettings["RMOGroupName"].ToString();
        }
        //Siddhesh Arekar for Task ID is 55884 : 06/01/2015 : End

        //Ishwar 26022015 : Start --Desc : Report is not required
        ////27622-Ambar-Start
        //string strSpecialUsers = string.Empty;
        //if (ConfigurationManager.AppSettings["SpecialReport"] != null)
        //{
        //    strSpecialUsers = ConfigurationManager.AppSettings["SpecialReport"].ToString();
        //}
        ////27622-Ambar-End
        //Ishwar 26022015 : End


        //36581-Ambar-Start
        string strOrgChartDetailsUsers = string.Empty;
        if (ConfigurationManager.AppSettings["OrgChartDetailsReport"] != null)
        {
          strOrgChartDetailsUsers = ConfigurationManager.AppSettings["OrgChartDetailsReport"].ToString();
        }
        //36581-Ambar-End

        //Ishwar Patil NIS RMS : 29/10/2014 : Start
        string strNISUsers = string.Empty;
        if (ConfigurationManager.AppSettings["NISReportsAccess"] != null)
        {
            strNISUsers = ConfigurationManager.AppSettings["NISReportsAccess"].ToString();
        }
        //Ishwar Patil NIS RMS : 29/10/2014 : End

        //--Authorise User
        //AuthorizeUserForPageView(); 
        bool bCheckAccess = false;
        Common.AuthorizationManager.AuthorizationManager objAuth = new Common.AuthorizationManager.AuthorizationManager();
        if ((strQualityTeamUsers != null) && (strQualityTeamUsers.Contains(objAuth.getLoggedInUser())))
        {
            tr_QuailityTeamReport.Visible = true;
            bCheckAccess = true;
        }

        //--
        if ((strRMPTeamUsers != null) && (strRMPTeamUsers.Contains(objAuth.getLoggedInUser())))
        {
            tr_RPMReports.Visible = true;
            bCheckAccess = true;
        }

        if ((strRecruitmentTeamUsers != null) && (strRecruitmentTeamUsers.Contains(objAuth.getLoggedInUser())))
        {
            tr_RecruitmentTeamReport.Visible = true;
            bCheckAccess = true;
        }


        if ((strHRTeamUsers != null) && (strHRTeamUsers.Contains(objAuth.getLoggedInUser())))
        {
            tr_HRTeamReport.Visible = true;
            bCheckAccess = true;
        }
      
        //Ishwar 26022015 : Start --Desc : Report is not required
        ////27622-Ambar-Start
        //if ((strSpecialUsers != null) && (strSpecialUsers.Contains(objAuth.getLoggedInUser())))
        //{
        //    tr_SpecialReport.Visible = true;
        //    bCheckAccess = true;
        //}        
        ////27622-Ambar-End
        //Ishwar 26022015 : End


        //36581-Ambar-Start
        if ((strOrgChartDetailsUsers != null) && (strOrgChartDetailsUsers.Contains(objAuth.getLoggedInUser())))
        {
          tr_OrgChartDetailsReport.Visible = true;
          bCheckAccess = true;
        }
        //36581-Ambar-End

        //Ishwar Patil NIS RMS : 29/10/2014 : Start
        //if (((strRMPTeamUsers != null) && (strRMPTeamUsers.Contains(objAuth.getLoggedInUser()))) || ((strNISUsers != null) && (strNISUsers.Contains(objAuth.getLoggedInUser()))))
        //{
        //    tr_NISRMSReport.Visible = true;
        //    bCheckAccess = true;
        //}
        //Ishwar Patil NIS RMS : 29/10/2014 : End

        //--

        //Siddhesh Arekar for Task ID is 55884 : 06/01/2015 : Start
        if ((strRMOGroupName != null) && (strRMOGroupName.Contains(objAuth.getLoggedInUser())))
        {
          tr_MRFClosureStatus.Visible = true;
          bCheckAccess = true;
        }
        //Siddhesh Arekar for Task ID is 55884 : 06/01/2015 : End

        if (!bCheckAccess)
        {
            Response.Redirect("UnAuthorisedUser.htm", false);
        }
    }

    /// <summary>
    /// project details
    /// </summary>
    protected void btnProjectsDetails_Click(object sender, EventArgs e)
    {
        GetReport("Projects Details");
    }

    #region Modified By Mohamed Dangra
    // Mohamed :  27/03/2014 : Starts                        			  
    // Desc : Report's added

    /// <summary>
    /// UtilAndBillingDayWise
    /// </summary>
    protected void BtnUtilAndBillingDayWise_Click(object sender, EventArgs e)
    {
        GetReport("UtilAndBillingDayWise");
    }

    /// <summary>
    /// UtilAndBillingDayWise
    /// </summary>
    protected void BtnUtilAndBillingDayWiseTesting_Click(object sender, EventArgs e)
    {
        GetReport("UtilAndBillingDayWiseTesting");
    }


    /// <summary>
    /// UtilBillingGreaterthan100
    /// </summary>
    protected void BtnUtilBillingGreaterthan100_Click(object sender, EventArgs e)
    {
        GetReport("UtilBillingGreaterthan100");
    }

    /// <summary>
    /// RecruitmentMRF
    /// </summary>
    protected void BtnRecruitmentMRF_Click(object sender, EventArgs e)
    {
        GetReport("RecruitmentMRF");
    }

    // Mohamed : 27/03/2014 : Ends
    #endregion Modified By Mohamed Dangra

    // Mohamed : Issue : 51365 : 05/09/2014 : Starts                        			  
    // Desc : Average Util billing for given period Report added

    /// <summary>
    /// UtilAndBillingDayWise
    /// </summary>
    protected void BtnAvgUtilBilling_Click(object sender, EventArgs e)
    {
        GetReport("Avg Util Billing for Given Period");
    }
    // Mohamed : 05/09/2014 : Ends

    // Ishwar Patil NIS RMS : 09/10/2014 : Start
    protected void LBSkillsReport_Click(object sender, EventArgs e)
    {
        GetReport("Skills Report");
    }
    // Ishwar Patil NIS RMS : 09/10/2014 : End
    
    /// <summary>
    /// current plus forecast bench
    /// </summary>
    protected void btnCurrentPlusForeCastBench_Click(object sender, EventArgs e)
    {
        GetReport("Current Plus ForeCasted Bench");
    }

    /// <summary>
    /// current plus forecast bench
    /// </summary>
    //Ishwar 26022015 : Start --Desc : Report is not required
    //protected void btnCurrentPlusForeCaseBenchResourceGroupWise_Click(object sender, EventArgs e)
    //{
    //    GetReport("Current Plus Forecasted Bench ResourceGroupWise");
    //}
    //Ishwar 26022015 : End

    /// <summary>
    /// add duration button click event handler
    /// </summary>
    protected void btnAllocationBillingReport_Click(object sender, EventArgs e)
    {
        GetReport("Allocation and Billing Bench");
    }

    /// <summary>
    /// add duration button click event handler
    /// </summary>
    protected void btnMRFSummary_Click(object sender, EventArgs e)
    {
        GetReport("Summary of MRFs");
    }



    //Siddharth 26th August 2015 Start
    protected void btnEmployeeReportingManagerReport_Click(object sender, EventArgs e)
    {
        GetReport("EmployeeReportingManagerReport");
    }
    //Siddharth 26th August 2015 End



    /// <summary>
    /// add duration button click event handler
    /// </summary>
    protected void btnResourceAllocationAgainstMRF_Click(object sender, EventArgs e)
    {
        GetReport("Resource Allocation against MRF");
    }

    // Mohamed :  14/10/2014 : Starts                        			  
    // Desc : Head Count Report added
    /// <summary>
    /// add Head Count button click event handler
    /// </summary>
    protected void BtnHeadCountReport_Click(object sender, EventArgs e)
    {
        GetReport("Head Count");
    }
    // Mohamed :  14/10/2014 : Ends

    /// <summary>
    /// add duration button click event handler
    /// </summary>
    protected void btnResourceDetails_Click(object sender, EventArgs e)
    {
        GetReport("Resource_Details");
    }

    /// <summary>
    /// add button click event handler
    /// </summary>
    protected void BtnSkillsDetails_Click(object sender, EventArgs e)
    {
        GetReport("Employee Skills Details");
    }



    protected void BtnResignedEmpDetails_Click(object sender, EventArgs e)
    {
        GetReport("ResignedEmployeeDetails");
    }
    //Ishwar 26022015 : Start --Desc : Report is not required
    //protected void BtnContractDetails_Click(object sender, EventArgs e)
    //{
    //    GetReport("ContractDetails");
    //}
    //Ishwar 26022015 : End

    protected void btnActiveEmployeesDetails_Click(object sender, EventArgs e)
    {
        GetReport("ActiveEmployeesDetails");
    }
    
    //Ishwar 26022015 : Start --Desc : Report is not required
    #region Note : implementation format to be changed
    //protected void btnPractiseTeamReport_Click(object sender, EventArgs e)
    //{
    //    StringBuilder strbHTML = new StringBuilder("");
    //    strbHTML.Append("<table border = '0' cellpadding = '0' cellspacing = '0' style='border-color:Black;' width = '100%'>");
    //    strbHTML.Append("<tr><td>&nbsp;</td></tr>");
    //    #region Header Content
    //    strbHTML.Append("<tr>");
    //    strbHTML.Append("<td width = '80%'>");

    //    strbHTML.Append("<table border = '2' cellpadding = '0' cellspacing = '0' style='border-color:Black;' width = '100%'>");
    //    strbHTML.Append("<tr>");
    //    strbHTML.Append("<td style='" + strHeaderStyle + "' align = 'center' width = '10%'>Dept</td>");
    //    strbHTML.Append("<td style='" + strHeaderStyle + "' align = 'center' width = '10%'>Designation</td>");
    //    strbHTML.Append("<td width = '80%'>");

    //    #region Weekly Content

    //    strbHTML.Append("<table border = '2' cellpadding = '0' cellspacing = '0' style='border-color:Black;' width = '80%'>");
    //    strbHTML.Append("<tr><td style='" + strHeaderStyle + "' align = 'center'>" + GetFirdayDateForCurrentWeek() + "</td></tr>");
    //    strbHTML.Append("<tr>");
    //    strbHTML.Append("<td align = 'center'>");

    //    #region Allocation Details

    //    strbHTML.Append("<table border = '2' cellpadding = '0' cellspacing = '0' style='border-color:Black;' width = '80%'>");
    //    strbHTML.Append("<tr>");
    //    strbHTML.Append("<td align = 'center' style='" + strHeaderStyle + "' width = '40%'>Project Name</td>");
    //    strbHTML.Append("<td align = 'center' style='" + strHeaderStyle + "' width = '30%'>No of days utilized</td>");
    //    strbHTML.Append("<td align = 'center' style='" + strHeaderStyle + "' width = '30%'>No of days billed</td>");
    //    strbHTML.Append("</tr>");
    //    strbHTML.Append("</table>");

    //    #endregion Allocation Details

    //    strbHTML.Append("</td>");
    //    strbHTML.Append("</tr>");
    //    strbHTML.Append("</table>");

    //    #endregion Weekly Content

    //    strbHTML.Append("</td>");

    //    #region *****************Total

    //    //strbHTML.Append("<td>");

    //    //#region Weekly Content

    //    //strbHTML.Append("<table border = '2' cellpadding = '0' cellspacing = '0' style='border-color:Black;' width = '100%'>");
    //    //strbHTML.Append("<tr><td style='" + strHeaderStyle + "' align = 'center'>Total</td></tr>");
    //    //strbHTML.Append("<tr>");
    //    //strbHTML.Append("<td style='" + strHeaderStyle + "' align = 'center'>");

    //    //#region Allocation Details

    //    //strbHTML.Append("<table border = '2' cellpadding = '0' cellspacing = '0' style='border-color:Black;' width = '100%'>");
    //    //strbHTML.Append("<tr>");
    //    //strbHTML.Append("<td align = 'center' style='" + strHeaderStyle + "'>Total No of days utilized</td>");
    //    //strbHTML.Append("<td align = 'center' style='" + strHeaderStyle + "'>Total No of days billed</td>");
    //    //strbHTML.Append("</tr>");
    //    //strbHTML.Append("</table>");

    //    //#endregion Allocation Details

    //    //strbHTML.Append("</td>");
    //    //strbHTML.Append("</tr>");
    //    //strbHTML.Append("</table>");

    //    //#endregion Weekly Content

    //    //strbHTML.Append("</td>");

    //    #endregion *************Total

    //    strbHTML.Append("</tr>");
    //    strbHTML.Append("</table>");

    //    strbHTML.Append("</td>");
    //    strbHTML.Append("</tr>");
    //    #endregion Header Content

    //    #region Main Content

    //    //-----------------Row Details

    //    strbHTML = getDetails(strbHTML);
    //    //-----------------Row Details

    //    #endregion Main Content

    //    strbHTML.Append("</table>");

    //    //--
    //    GetExcel("Practice_Team", strbHTML.ToString());
    //}
    //Ishwar 26022015 : End

    private StringBuilder getDetails(StringBuilder strbHTML)
    {

        #region Get the allocation details

        RaveHRCollection objEmployeeAllocation = new RaveHRCollection();
        objEmployeeAllocation = GetAllocatedResourceByProjectId();

        ArrayList objDept = new ArrayList();
        foreach (BusinessEntities.Employee objEmployee in objEmployeeAllocation)
        {
            if (!objDept.Contains(objEmployee.Department))
            {
                //--Add dept to arr
                objDept.Add(objEmployee.Department);

                strbHTML.Append("<tr>");
                strbHTML.Append("<td style='" + strRowStyle + "' align = 'left' colspan = '3'><b>" + objEmployee.Department + "</b></td>");
                strbHTML.Append("</tr>");
            }


            #region Header Content
            strbHTML.Append("<tr>");
            strbHTML.Append("<td width = '100%'>");

            strbHTML.Append("<table border = '2' cellpadding = '0' cellspacing = '0' style='border-color:Black;' width = '100%'>");
            strbHTML.Append("<tr>");
            strbHTML.Append("<td style='" + strRowStyle + "' align = 'left' width = '10%'>" + objEmployee.FullName + "</td>");
            strbHTML.Append("<td style='" + strRowStyle + "' align = 'left' width = '10%'>" + objEmployee.Designation + "</td>");
            strbHTML.Append("<td width = '80%'>");

            #region Weekly Content

            strbHTML.Append("<table border = '2' cellpadding = '0' cellspacing = '0' style='border-color:Black;' width = '100%'>");
            strbHTML.Append("<tr>");
            strbHTML.Append("<td  align = 'center'>");

            #region Allocation Details

            strbHTML.Append("<table border = '2' cellpadding = '0' cellspacing = '0' style='border-color:Black;' width = '100%'>");
            strbHTML.Append("<tr>");
            strbHTML.Append("<td align = 'left' style='" + strRowStyle + "'>" + objEmployee.ProjectName + "</td>");
            strbHTML.Append("<td align = 'right' style='" + strRowStyle + "'>" + objEmployee.NoOfDaysUtilised + "</td>");
            strbHTML.Append("<td align = 'right' style='" + strRowStyle + "'>" + objEmployee.NoOfDaysBilled + "</td>");
            strbHTML.Append("</tr>");
            strbHTML.Append("</table>");

            #endregion Allocation Details

            strbHTML.Append("</td>");
            strbHTML.Append("</tr>");
            strbHTML.Append("</table>");

            #endregion Weekly Content

            strbHTML.Append("</td>");

            #region *****************Total

            //strbHTML.Append("<td>");

            //#region Weekly Content

            //strbHTML.Append("<table border = '2' cellpadding = '0' cellspacing = '0' style='border-color:Black;' width = '100%'>");
            //strbHTML.Append("<tr>");
            //strbHTML.Append("<td align = 'center'>");

            //#region Allocation Details

            //strbHTML.Append("<table border = '2' cellpadding = '0' cellspacing = '0' style='border-color:Black;' width = '100%'>");
            //strbHTML.Append("<tr>");
            //strbHTML.Append("<td align = 'right' style='" + strRowStyle + "'>" + objEmployee.TotalNoOfDaysUtilised + "</td>");
            //strbHTML.Append("<td align = 'right' style='" + strRowStyle + "'>" + objEmployee.TotalNoOfDaysBilled + "</td>");
            //strbHTML.Append("</tr>");
            //strbHTML.Append("</table>");

            //#endregion Allocation Details

            //strbHTML.Append("</td>");
            //strbHTML.Append("</tr>");
            //strbHTML.Append("</table>");

            //#endregion Weekly Content

            //strbHTML.Append("</td>");

            #endregion *************Total

            strbHTML.Append("</tr>");
            strbHTML.Append("</table>");

            strbHTML.Append("</td>");
            strbHTML.Append("</tr>");

            #endregion Header Content
        }

        #endregion Get the allocation details

        return strbHTML;
    }

    #region

    /// <summary>
    /// Header Style
    /// </summary>
    private const string strHeaderStyle = "background-color: #BFBFBF;font-size: 9pt;font-weight: bold;padding-left: 5px;font-family:Verdana;vertical-align:top;";

    /// <summary>
    /// Row Style
    /// </summary>
    private const string strRowStyle = "font-family: Verdana;	font-size: 9pt;	padding-left: 5px;vertical-align:top;";

    /// <summary>
    /// Data reader object
    /// </summary>
    SqlDataReader objReader = null;

    /// <summary>
    /// Data access class 
    /// </summary>
    DataAccessClass objDAResourcePlan = null;

    private static SqlParameter[] objSqlParameter;

    #endregion

    private void GetExcel(string strFileName, string strbHTML)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".xls");
        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";

        HttpContext.Current.Response.Write(strbHTML);
        HttpContext.Current.Response.End();
    }

    /// <summary>
    /// get allocated resource by projectId
    /// </summary>
    public RaveHRCollection GetAllocatedResourceByProjectId()
    {
        try
        {
            objDAResourcePlan = new DataAccessClass();
            objDAResourcePlan.OpenConnection(ConfigurationManager.ConnectionStrings["RaveHRConnectionString"].ConnectionString);
            objReader = objDAResourcePlan.ExecuteReaderSP("USP_ResourcePlan_GetPracticeTeamAllocationDetails");
            RaveHRCollection objListGetResourcePlan = new RaveHRCollection();

            BusinessEntities.Employee objBEEmployee = null;
            while (objReader.Read())
            {
                objBEEmployee = new BusinessEntities.Employee();
                objBEEmployee.FullName = objReader["EmployeeName"].ToString();
                objBEEmployee.Designation = objReader["Designation"].ToString();
                objBEEmployee.Department = objReader["ResourceBU"].ToString();
                objBEEmployee.ProjectName = objReader["ProjectName"].ToString();
                string str = objReader["NoOfDaysBilled"].ToString();
                string str1 = objReader["NoOfDaysUtilized"].ToString();
                objBEEmployee.NoOfDaysBilled = double.Parse(objReader["NoOfDaysBilled"].ToString());
                objBEEmployee.NoOfDaysUtilised = double.Parse(objReader["NoOfDaysUtilized"].ToString());
                objBEEmployee.TotalNoOfDaysBilled = double.Parse(objReader["TotalNoOfDaysBilled"].ToString());
                objBEEmployee.TotalNoOfDaysUtilised = double.Parse(objReader["TotalNoOfDaysUtilised"].ToString());

                objListGetResourcePlan.Add(objBEEmployee);
            }

            //--
            return objListGetResourcePlan;

        }
        //catches genral exception
        catch (Exception ex)
        {
            throw ex;
        }
        //close datareader and connection
        finally
        {
            //checks if datareader is null
            if (objReader != null)
            {
                //close datareader
                objReader.Close();
            }

            //close connection
            objDAResourcePlan.CloseConncetion();
        }
    }

    #endregion Note : implementation format to be changed


    /// <summary>
    /// Get Recruitment SLA Report
    /// Done By Sameer For Issue ID:18641
    /// </summary>
    
    //Ishwar 26022015 : Start --Desc : Report is not required
    //protected void btnRecruitMentSLAReport_Click(object sender, EventArgs e)
    //{
    //    GetReport("Recuitment_SLA");
    //}
    
    //protected void btnDemandSupplyNettingReport_Click(object sender, EventArgs e)
    //{
    //    GetReport("Demand Supply Netting");
    //}
    //Ishwar 26022015 : End


    protected void btnResourcesHistoryProjectDetails_Click(object sender, EventArgs e)
    {
        GetReport("Resources_History_Project_Details");
    }


    protected void BtnQualityReport_Click(object sender, EventArgs e)
    {
        GetReport("ProposedDataforQuality");
    }


    protected void BtnAllProjectDetails_Click(object sender, EventArgs e)
    {
        GetReport("AllProjectDetails");
    }
    //Ishwar 26022015 : Start --Desc : Report is not required
    //protected void BtnWeeklyBenchReport_Click(object sender, EventArgs e)
    //{
    //    GetReport("Weekly Bench Report");
    //}
    ////Ishwar 26022015 : End

    //protected void btnMRFAging_Click(object sender, EventArgs e)
    //{
    //    GetReport("MrfAgingByPrimarySkillAndDesignation");
    //}

    //protected void btnMRFAgingByOpenPosition_Click(object sender, EventArgs e)
    //{
    //    GetReport("MrfAgingByPrimarySkillAndDesignation_RecruitmentAgingReport");
    //}
    #endregion Protected Events

    #region Private Methods
    /// <summary>
    /// Authorize User
    /// </summary>
    public void AuthorizeUserForPageView()
    {
        Common.AuthorizationManager.AuthorizationManager objAuthorizationManager = new Common.AuthorizationManager.AuthorizationManager();
        objAuthorizationManager.AuthorizeUserForPageView(new object[] { Common.AuthorizationManagerConstants.OPERATION_PAGE_CREATERP_VIEW });
    }

    #endregion

    #region Report Implementation

    private void GetReport(string strReport)
    {

        //--
        rpvReports.ShowCredentialPrompts = false;

        //--Credentials
        //rpvReports.ServerReport.ReportServerCredentials = new ReportCredentials("username", "passwd", "domain");

        //--Processing mode
        rpvReports.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;

        //--Report Server
        rpvReports.ServerReport.ReportServerUrl = new System.Uri("http://localhost/ReportServer/");
        //rpvReports.ServerReport.ReportServerUrl = new System.Uri("http://cu-489:8080/ReportServer/");
        
        //--Report dir
        rpvReports.ServerReport.ReportPath = "/RMSReports/" + strReport;

        //System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter> param = new System.Collections.Generic.List<Microsoft.Reporting.WebForms.ReportParameter>();
        //param.Add(new Microsoft.Reporting.WebForms.ReportParameter("@IsDept", "1"));
        //rpvReports.ServerReport.SetParameters(param);

        //--report
        rpvReports.ServerReport.Refresh();

        //--
        //string strReportWindow = "<script>window.open('" + ConfigurationManager.AppSettings["RMSUrl"].ToString() + "/ReportServer/Pages/ReportViewer.aspx?%2fRMSReports%2f" + strReport + "&rs:Command=Render', null ,'toolbar=no,height=600,scrollbars=yes,resizable=yes','mywindow');</script>";
        //Page.RegisterStartupScript("reportScript", strReportWindow);
    }

    public class ReportCredentials : Microsoft.Reporting.WebForms.IReportServerCredentials
    {

        string _userName, _password, _domain;

        public ReportCredentials(string userName, string password, string domain)
        {

            _userName = userName;

            _password = password;

            _domain = domain;

        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {

            get
            {
                return null;
            }
        }

        public System.Net.ICredentials NetworkCredentials
        {
            get
            {
                return new System.Net.NetworkCredential(_userName, _password, _domain);
            }

        }

        public bool GetFormsCredentials(out System.Net.Cookie authCoki, out string userName, out string password, out string authority)
        {
            userName = _userName;
            password = _password;
            authority = _domain;
            authCoki = new System.Net.Cookie(".ASPXAUTH", ".ASPXAUTH", "/", "Domain");
            return true;
        }
    }

    private string GetFirdayDateForCurrentWeek()
    {
        int days = 0;
        switch (DateTime.Now.DayOfWeek.ToString())
        {
            case "Monday":
                days = 4;
                break;
            case "Tuesday":
                days = 3;
                break;
            case "Wednesday":
                days = 2;
                break;
            case "Thursday":
                days = 1;
                break;
            case "Saturday":
                days = -1;
                break;
            case "Sunday":
                days = -2;
                break;
        }

        return string.Format("{0:D}", DateTime.Now.AddDays(days));
    }

    #endregion Report Implementation end

    protected void btnOrgChartDetails_Click(object sender, EventArgs e)
    {
      GetReport("Org Chart");
    }

    protected void btnMRFClosureStatus_Click(object sender, EventArgs e)
    {
        GetReport("MRF Closure Status");
    }
}
