using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Common;
using Common.AuthorizationManager;
using Common.Constants;
using BusinessEntities;
using System.Text;
using System.IO;


public partial class FourCModule_4C_Reports : BaseClass
{
    string CTypeId = "87";
    private string ZERO = "0";
    //Define the select as string.
    private string SELECTONE = "SELECT";
    string MasterName = "MasterName";
    string UserRaveDomainId;
    string UserMailId;
    List<string> lstRights;
    string MasterID = "MasterID";
    private const string CLASS_NAME = "FourCModule_4C_Reports.aspx";
    BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
    public ASP._4clogin_aspx prev;

    protected void Page_Load(object sender, EventArgs e)
    {
        AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
        UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
        UserMailId = UserRaveDomainId.Replace("co.in", "com");

        string txtUser = null;

        if (ViewState["UserMailId"] == null)
        {
            if (PreviousPage != null)
            {
                prev = (ASP._4clogin_aspx)PreviousPage;
                txtUser = prev.UserName;

                if (txtUser != null && !string.IsNullOrEmpty(txtUser))
                {
                    ViewState["UserMailId"] = objRaveHRAuthorizationManager.GetDomainUsers(txtUser.ToUpper().Trim());
                    UserMailId = ViewState["UserMailId"].ToString();
                    //lstRights = new List<string> { };
                }
            }
            else
            {
                UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
                ViewState["UserMailId"] = UserRaveDomainId.Replace("co.in", "com");
                UserMailId = ViewState["UserMailId"].ToString();
                //lstRights = CheckAccessRights(UserMailId);
            }
        }
        else
        {
            UserMailId = ViewState["UserMailId"].ToString();
        }


        if (!IsPostBack)
        {

            //check access rights security
            MasterPage objMaster = new MasterPage();
            bool flag = objMaster.Check4CAccess();
            if (!flag)
            {
                Response.Redirect("Home.aspx");
            }
            else
            {

                lstRights = CheckAccessRights(UserMailId);
                ViewState["lstRights"] = lstRights;
                LoadCType();
                LoadDepartment(lstRights);
                LoadProjects(lstRights);
                LoadDesignation(lstRights);
                lnkConsolidated.Font.Bold = true;
                LoadInitialValue();
            }
        }
        LoadColor();

        //imgConsolidatedFind.Attributes.Add
        string strParameter = "";
        if (ViewState["lstRights"] != null)
        {
            lstRights = (List<string>)ViewState["lstRights"];
        }
        if (lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.FOURCADMIN.ToString())
            || lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.REPORTACCESS.ToString())
                    || ViewState["UserMailId"].ToString() == "sawita.kamat@northgateps.com")
        {
            strParameter = ""; //no parameter
        }
        else
        {
            strParameter = "" + URLHelper.SecureParameters("EmpId", ViewState["LoginEmpId"].ToString()) + "&" + URLHelper.SecureParameters("VIEW", "SUB-ORDINATE-VIEW") + "&" + URLHelper.CreateSignature(ViewState["LoginEmpId"].ToString(), "SUB-ORDINATE-VIEW");
        }
        
        //Ishwar : 12062015 : Start
        //Desc : 4C consolidated report access to PMO group
        ArrayList arrRoles = new ArrayList();
        arrRoles = (System.Collections.ArrayList)Session[SessionNames.AZMAN_ROLES];
        foreach (string STR in arrRoles)
        {
            switch (STR)
            {
                case "PMO":
                    tdmenulist.Visible = false;
                    break;
                default:
                    break;
            }
        }
        //Ishwar : 12062015 : End


        imgConsolidatedFind.Attributes["onclick"] = "popUpEmployeeSearch('" + strParameter + "','CONSOLIDATED')";
        imgActionFind.Attributes["onclick"] = "popUpEmployeeSearch('" + strParameter + "','ACTION')";
        imgActionOwner.Attributes["onclick"] = "popUpEmployeeSearch('" + strParameter + "','ACTION-ACTIONOWNER')";
        lblMessage.Visible = false;
    }


    private List<string> CheckAccessRights(string emailId)
    {
        lstRights = new List<string> { };
        //DataSet ds = null;
        try
        {
            int loginEmpId = 0;
            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
            lstRights = fourCBAL.Check4CLoginRights(emailId, ref loginEmpId);
            ViewState["LoginEmpId"] = loginEmpId;
            return lstRights;
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "CheckAccessRights", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
        return lstRights;
    }

    private void LoadInitialValue()
    {
        if (lnkConsolidated.Font.Bold == true)
        {

            lblReport.Text = "Consolidated Report";
            lnkConsolidated.Font.Bold = true;
            lnkAnalysis.Font.Bold = false;
            lnkAction.Font.Bold = false;
            lnkStatus.Font.Bold = false;
            lnkMovement.Font.Bold = false;
            lnkCountReport.Font.Bold = false;

            tblConsolidated.Visible = true;
            tblAction.Visible = false;
            tblAnalysis.Visible = false;
            tblMovement.Visible = false;
            tblStatus.Visible = false;
            tblCountReport.Visible = false;

            if (ViewState["Department"] != null)
            {
                //RaveHRCollection raveHRCollection = (RaveHRCollection)ViewState["Department"];
                //lstConsolidatedDepartment.DataSource = raveHRCollection;
                //lstConsolidatedDepartment.DataTextField = Common.CommonConstants.DDL_DataTextField;
                //lstConsolidatedDepartment.DataValueField = Common.CommonConstants.DDL_DataValueField;
                //lstConsolidatedDepartment.DataBind();
                DataSet dsDept = (DataSet)ViewState["Department"];
                lstConsolidatedDepartment.DataSource = dsDept;
                lstConsolidatedDepartment.DataTextField = dsDept.Tables[0].Columns[1].ToString();
                lstConsolidatedDepartment.DataValueField = dsDept.Tables[0].Columns[0].ToString();
                lstConsolidatedDepartment.DataBind();

            }
            if (ViewState["Projects"] != null)
            {
                //RaveHRCollection raveHRCollection = (RaveHRCollection)ViewState["Projects"];
                DataSet dsProj = (DataSet)ViewState["Projects"];
                lstConsolidatedProject.DataSource = dsProj;
                lstConsolidatedProject.DataTextField = dsProj.Tables[0].Columns[1].ToString();
                lstConsolidatedProject.DataValueField = dsProj.Tables[0].Columns[0].ToString();
                lstConsolidatedProject.DataBind();
            }

            if (ViewState["Designation"] != null)
            {
                RaveHRCollection raveHRCollection = (RaveHRCollection)ViewState["Designation"];
                lstConsolidatedEmpDesignation.DataSource = raveHRCollection;
                lstConsolidatedEmpDesignation.DataTextField = Common.CommonConstants.DDL_DataTextField;
                lstConsolidatedEmpDesignation.DataValueField = Common.CommonConstants.DDL_DataValueField;
                lstConsolidatedEmpDesignation.DataBind();
            }
        }
        else if (lnkAnalysis.Font.Bold == true)
        {
            lblReport.Text = "Analysis Report";
            lnkConsolidated.Font.Bold = false;
            lnkAnalysis.Font.Bold = true;
            lnkAction.Font.Bold = false;
            lnkStatus.Font.Bold = false;
            lnkMovement.Font.Bold = false;
            lnkCountReport.Font.Bold = false;

            tblConsolidated.Visible = false;
            tblAction.Visible = false;
            tblAnalysis.Visible = true;
            tblMovement.Visible = false;
            tblStatus.Visible = false;
            tblCountReport.Visible = false;


            if (ViewState["Department"] != null)
            {
                //RaveHRCollection raveHRCollection = (RaveHRCollection)ViewState["Department"];
                //lstConsolidatedDepartment.DataSource = raveHRCollection;
                //lstConsolidatedDepartment.DataTextField = Common.CommonConstants.DDL_DataTextField;
                //lstConsolidatedDepartment.DataValueField = Common.CommonConstants.DDL_DataValueField;
                //lstConsolidatedDepartment.DataBind();
                DataSet dsDept = (DataSet)ViewState["Department"];
                lstAnalysisDepartment.DataSource = dsDept;
                lstAnalysisDepartment.DataTextField = dsDept.Tables[0].Columns[1].ToString();
                lstAnalysisDepartment.DataValueField = dsDept.Tables[0].Columns[0].ToString();
                lstAnalysisDepartment.DataBind();

            }
            if (ViewState["Projects"] != null)
            {
                //RaveHRCollection raveHRCollection = (RaveHRCollection)ViewState["Projects"];
                DataSet dsProj = (DataSet)ViewState["Projects"];
                lstAnalysisProject.DataSource = dsProj;
                lstAnalysisProject.DataTextField = dsProj.Tables[0].Columns[1].ToString();
                lstAnalysisProject.DataValueField = dsProj.Tables[0].Columns[0].ToString();
                lstAnalysisProject.DataBind();
            }

            if (ViewState["Designation"] != null)
            {
                RaveHRCollection raveHRCollection = (RaveHRCollection)ViewState["Designation"];
                lstAnalysisEmpDesignation.DataSource = raveHRCollection;
                lstAnalysisEmpDesignation.DataTextField = Common.CommonConstants.DDL_DataTextField;
                lstAnalysisEmpDesignation.DataValueField = Common.CommonConstants.DDL_DataValueField;
                lstAnalysisEmpDesignation.DataBind();
            }
        }
        else if (lnkAction.Font.Bold == true)
        {
            if (ViewState["Department"] != null)
            {
                //RaveHRCollection raveHRCollection = (RaveHRCollection)ViewState["Department"];
                //lstConsolidatedDepartment.DataSource = raveHRCollection;
                //lstConsolidatedDepartment.DataTextField = Common.CommonConstants.DDL_DataTextField;
                //lstConsolidatedDepartment.DataValueField = Common.CommonConstants.DDL_DataValueField;
                //lstConsolidatedDepartment.DataBind();
                DataSet dsDept = (DataSet)ViewState["Department"];
                lstActionDepartment.DataSource = dsDept;
                lstActionDepartment.DataTextField = dsDept.Tables[0].Columns[1].ToString();
                lstActionDepartment.DataValueField = dsDept.Tables[0].Columns[0].ToString();
                lstActionDepartment.DataBind();

            }
            if (ViewState["Projects"] != null)
            {
                //RaveHRCollection raveHRCollection = (RaveHRCollection)ViewState["Projects"];
                DataSet dsProj = (DataSet)ViewState["Projects"];
                lstActionProject.DataSource = dsProj;
                lstActionProject.DataTextField = dsProj.Tables[0].Columns[1].ToString();
                lstActionProject.DataValueField = dsProj.Tables[0].Columns[0].ToString();
                lstActionProject.DataBind();
            }
        }
        if (lnkStatus.Font.Bold == true)
        {
            ddlStatusYear.Items.Clear();
            ddlStatusMonth.Items.Clear();

            int startYearIndex = 1;
            ddlStatusYear.Items.Insert(CommonConstants.ZERO, "Year");
            for (int i = 0; i <= 1; i++)
            {
                int year = int.Parse(DateTime.Now.AddYears(-i).ToString("yyyy"));

                ddlStatusYear.Items.Insert(startYearIndex, new ListItem(year.ToString(), year.ToString()));
                startYearIndex++;
            }

            ddlStatusMonth.Items.Insert(CommonConstants.ZERO, "Months");
            int startMonthIndex = 1;
            DateTime stDate = DateTime.Parse("12/12/" + DateTime.Now.ToString("yyyy"));

            for (int i = 1; i <= 12; i++)
            {
                ddlStatusMonth.Items.Insert(startMonthIndex, new ListItem(stDate.AddMonths(i).ToString("MMMM").ToString(), i.ToString()));
                startMonthIndex++;
            }
        }

    }

    private void LoadDepartment(List<string> lstRights)
    {
        try
        {

            //Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
            //raveHRCollection = master.FillDepartmentDropDownBL();
            //ViewState["Department"] = raveHRCollection;
            DataSet dsDepartment = null;
            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();

            if (lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.FOURCADMIN.ToString()) 
                    || lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.REPORTACCESS.ToString())
                    || ViewState["UserMailId"].ToString() == "sawita.kamat@northgateps.com")
            {
                dsDepartment = fourCBAL.GetDepartmentName(UserMailId);
            }
            else
            {
                dsDepartment = fourCBAL.GetDepartmentName(UserMailId, ViewState["Role"] == null ? null : ViewState["Role"].ToString(), CommonConstants.FOURCTypeADD);
            }
            ViewState["Department"] = dsDepartment;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "LoadCType", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    private void LoadProjects(List<string> lstRights)
    {
        try
        {
            //Rave.HR.BusinessLayer.MRF.MRFDetail mrfProjectName = new Rave.HR.BusinessLayer.MRF.MRFDetail();
            //raveHRCollection = mrfProjectName.GetProjectName();
            //ViewState["Projects"] = raveHRCollection;
            DataSet dsProject = null;
            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();

            if (lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.FOURCADMIN.ToString())
                    || lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.REPORTACCESS.ToString())
                    || ViewState["UserMailId"].ToString() == "sawita.kamat@northgateps.com")
            {
                dsProject = fourCBAL.GetProjectName();
            }
            else
            {
                dsProject = fourCBAL.GetProjectName(UserMailId, ViewState["Role"] == null ? null : ViewState["Role"].ToString(), CommonConstants.FOURCTypeADD);
            }
            ViewState["Projects"] = dsProject;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "LoadProjects", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    private void LoadCType()
    {
        try
        {
            List<BusinessEntities.Master> objCTypeStatus = new List<BusinessEntities.Master>();
            objCTypeStatus = FillMasterData(CTypeId);

            if (lnkAnalysis.Font.Bold == true)
            {
                chkCTypeAnalysis.DataSource = objCTypeStatus.OrderBy(o => o.MasterId);
                chkCTypeAnalysis.DataTextField = MasterName;
                chkCTypeAnalysis.DataValueField = MasterID;
                chkCTypeAnalysis.DataBind();
            }
            else if (lnkAction.Font.Bold == true)
            {
                chkActionCType.DataSource = objCTypeStatus.OrderBy(o => o.MasterId);
                chkActionCType.DataTextField = MasterName;
                chkActionCType.DataValueField = MasterID;
                chkActionCType.DataBind();
            }
            //chkCType.Items.Insert(0, new ListItem(SELECTONE, ZERO));

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "LoadCType", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }

    }

    private void LoadDesignation(List<string> lstRights)
    {
        try
        {
            //Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
            //raveHRCollection = employeeBL.GetEmployeesDesignations(0);
            //if (lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.FOURCADMIN.ToString()))
            //{
            //    Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
            //    raveHRCollection = master.FillDropDownsBL((int)Common.EnumsConstants.Category.Designations);
            //    ViewState["Designation"] = raveHRCollection;
            //}
            //else
            //{
            string deptval = "";
            if (lnkConsolidated.Font.Bold == true)
            {
                foreach (ListItem item in lstConsolidatedDepartment.Items)
                {
                    if (item.Selected)
                    {
                        deptval = deptval + item.Value + ",";
                    }
                }
            }
            else if (lnkAnalysis.Font.Bold == true)
            {
                foreach (ListItem item in lstAnalysisDepartment.Items)
                {
                    if (item.Selected)
                    {
                        deptval = deptval + item.Value + ",";
                    }
                }
            }


            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
            raveHRCollection = fourCBAL.Fill4CReportDesignation(int.Parse(ViewState["LoginEmpId"].ToString()), deptval);
            ViewState["Designation"] = raveHRCollection;
            //}
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "LoadCType", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }


    private List<BusinessEntities.Master> FillMasterData(string id)
    {
        try
        {
            List<BusinessEntities.Master> objCTypeStatus = new List<BusinessEntities.Master>();

            Rave.HR.BusinessLayer.Common.Master objMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            objCTypeStatus = objMasterBAL.GetMasterData(id);
            return objCTypeStatus;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "FillCType", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    private void LoadColor()
    {
        chkActionColorRating.Items[0].Attributes.Add("style", "background-color: #008000");
        chkActionColorRating.Items[1].Attributes.Add("style", "background-color: #FFCC00");
        chkActionColorRating.Items[2].Attributes.Add("style", "background-color: #FF0000");
    }

    protected void lnkConsolidated_Click(object sender, EventArgs e)
    {
        lblReport.Text = "Consolidated Report";
        lnkConsolidated.Font.Bold = true;
        lnkAnalysis.Font.Bold = false;
        lnkAction.Font.Bold = false;
        lnkStatus.Font.Bold = false;
        lnkMovement.Font.Bold = false;
        lnkCountReport.Font.Bold = false;

        tblConsolidated.Visible = true;
        tblAction.Visible = false;
        tblAnalysis.Visible = false;
        tblMovement.Visible = false;
        tblStatus.Visible = false;
        tblCountReport.Visible = false;

    }
    protected void lnkAnalysis_Click(object sender, EventArgs e)
    {
        lblReport.Text = "Analysis Report";
        lnkConsolidated.Font.Bold = false;
        lnkAnalysis.Font.Bold = true;
        lnkAction.Font.Bold = false;
        lnkStatus.Font.Bold = false;
        lnkMovement.Font.Bold = false;
        lnkCountReport.Font.Bold = false;

        tblConsolidated.Visible = false;
        tblAction.Visible = false;
        tblAnalysis.Visible = true;
        tblMovement.Visible = false;
        tblStatus.Visible = false;
        tblCountReport.Visible = false;

        LoadCType();
        LoadInitialValue();
    }
    protected void lnkAction_Click(object sender, EventArgs e)
    {
        lblReport.Text = "Action Report";
        lnkConsolidated.Font.Bold = false;
        lnkAnalysis.Font.Bold = false;
        lnkAction.Font.Bold = true;
        lnkStatus.Font.Bold = false;
        lnkMovement.Font.Bold = false;
        lnkCountReport.Font.Bold = false;

        tblConsolidated.Visible = false;
        tblAction.Visible = true;
        tblAnalysis.Visible = false;
        tblMovement.Visible = false;
        tblStatus.Visible = false;
        tblCountReport.Visible = false;

        //Load Data
        LoadCType();
        LoadInitialValue();

    }
    protected void lnkStatus_Click(object sender, EventArgs e)
    {
        lblReport.Text = "Status Report";
        lnkConsolidated.Font.Bold = false;
        lnkAnalysis.Font.Bold = false;
        lnkAction.Font.Bold = false;
        lnkStatus.Font.Bold = true;
        lnkMovement.Font.Bold = false;
        lnkCountReport.Font.Bold = false;

        tblConsolidated.Visible = false;
        tblAction.Visible = false;
        tblAnalysis.Visible = false;
        tblMovement.Visible = false;
        tblStatus.Visible = true;
        tblCountReport.Visible = false;

        LoadInitialValue();
    }
    protected void lnkMovement_Click(object sender, EventArgs e)
    {
        lblReport.Text = "Movement Report";
        lnkConsolidated.Font.Bold = false;
        lnkAnalysis.Font.Bold = false;
        lnkAction.Font.Bold = false;
        lnkStatus.Font.Bold = false;
        lnkMovement.Font.Bold = true;
        lnkCountReport.Font.Bold = false;

        tblConsolidated.Visible = false;
        tblAction.Visible = false;
        tblAnalysis.Visible = false;
        tblMovement.Visible = true;
        tblStatus.Visible = false;
        tblCountReport.Visible = false;
    }

    protected void lnkCountReport_Click(object sender, EventArgs e)
    {
        lblReport.Text = "Count Report";
        lnkConsolidated.Font.Bold = false;
        lnkAnalysis.Font.Bold = false;
        lnkAction.Font.Bold = false;
        lnkStatus.Font.Bold = false;
        lnkMovement.Font.Bold = false;
        lnkCountReport.Font.Bold = true;

        tblConsolidated.Visible = false;
        tblAction.Visible = false;
        tblAnalysis.Visible = false;
        tblMovement.Visible = false;
        tblStatus.Visible = false;
        tblCountReport.Visible = true;
    }

    private void doClearControl()
    {
        if (lnkConsolidated.Font.Bold == true)
        {
            lstConsolidatedProject.ClearSelection();
            lstConsolidatedDepartment.ClearSelection();
            lstConsolidatedEmpDesignation.ClearSelection();
            ddlConsolidatedPeriod.SelectedIndex = 0;
            HfConsolidatedEmpId.Value = "";
            HfConsolidatedEmpName.Value = "";
            txtConsolidatedEmp.Text = "";

            lstConsolidatedProject.Enabled = false;
            lstConsolidatedEmpDesignation.Enabled = false;
            lstConsolidatedEmpDesignation.Items.Clear();
            //Umesh: Issue 'Modal Popup issue in chrome' Starts
            //ChkIsSupportConsolidate.Checked = false; 
            //Umesh: Issue 'Modal Popup issue in chrome' Ends
        }
        else if (lnkAnalysis.Font.Bold == true)
        {
            lstAnalysisProject.ClearSelection();
            lstAnalysisDepartment.ClearSelection();
            lstConsolidatedEmpDesignation.ClearSelection();
            ddlAnalysisPeriod.SelectedIndex = 0;

            lstAnalysisProject.Enabled = false;
            lstAnalysisEmpDesignation.Enabled = false;
            lstAnalysisEmpDesignation.Items.Clear();
            chkCTypeAnalysis.ClearSelection();
            //Umesh: Issue 'Modal Popup issue in chrome' Starts
            //ChkIsSupportAnalysis.Checked = false;
            //Umesh: Issue 'Modal Popup issue in chrome' Ends
        }
        else if (lnkAction.Font.Bold == true)
        {
            txtActionEmp.Text = "";
            txtActionOwnerEmp.Text = "";
            HfActionEmpId.Value = "";
            HfActionEmpName.Value = "";
            HfActionOwnerEmpName.Value = "";
            HfActionOwnerEmpId.Value = "";

            lstActionDepartment.ClearSelection();
            lstActionProject.ClearSelection();
            ddlActionPeriod.SelectedIndex = 0;
            chkActionCType.ClearSelection();
            chkActionColorRating.ClearSelection();

            lstActionProject.Enabled = false;
            //Umesh: Issue 'Modal Popup issue in chrome' Starts
            //ChkIsSupportAction.Checked = false;
            //Umesh: Issue 'Modal Popup issue in chrome' Ends
        }
        else if (lnkStatus.Font.Bold == true)
        {
            ddlStatusMonth.SelectedIndex = 0;
            ddlStatusYear.SelectedIndex = 0;
            //Umesh: Issue 'Modal Popup issue in chrome' Starts
            //ChkIsSupportStatus.Checked = false;
            //Umesh: Issue 'Modal Popup issue in chrome' Ends
        }
        else if (lnkMovement.Font.Bold == true)
        {
            ddlMovementDuration.SelectedIndex = 0;
            //Umesh: Issue 'Modal Popup issue in chrome' Starts
            //ChkIsSupportMovement.Checked = false;
            //Umesh: Issue 'Modal Popup issue in chrome' Ends
        }
        else if (lnkCountReport.Font.Bold == true)
        {
            ddlCountReport.SelectedIndex = 0;
            //Umesh: Issue 'Modal Popup issue in chrome' Starts
            //ChkIsSupportCount  .Checked = false;
            //Umesh: Issue 'Modal Popup issue in chrome' Ends
        }
    }


    protected void lstConsolidatedDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (lstConsolidatedDepartment.SelectedItem.Value == "1")
            {
                lstConsolidatedProject.Enabled = true;
            }
            else
            {
                lstConsolidatedProject.Enabled = false;
            }

            LoadDesignation((List<string>)ViewState["lstRights"]);

            RaveHRCollection raveHRCollection = (RaveHRCollection)ViewState["Designation"];
            lstConsolidatedEmpDesignation.DataSource = raveHRCollection;
            lstConsolidatedEmpDesignation.DataTextField = Common.CommonConstants.DDL_DataTextField;
            lstConsolidatedEmpDesignation.DataValueField = Common.CommonConstants.DDL_DataValueField;
            lstConsolidatedEmpDesignation.DataBind();

            lstConsolidatedEmpDesignation.Enabled = true;

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "lstConsolidatedDepartment_SelectedIndexChanged", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }


    protected void lstAnalysisDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (lstAnalysisDepartment.SelectedItem.Value == "1")
            {
                lstAnalysisProject.Enabled = true;
            }
            else
            {
                lstAnalysisProject.Enabled = false;
            }

            LoadDesignation((List<string>)ViewState["lstRights"]);

            RaveHRCollection raveHRCollection = (RaveHRCollection)ViewState["Designation"];
            lstAnalysisEmpDesignation.DataSource = raveHRCollection;
            lstAnalysisEmpDesignation.DataTextField = Common.CommonConstants.DDL_DataTextField;
            lstAnalysisEmpDesignation.DataValueField = Common.CommonConstants.DDL_DataValueField;
            lstAnalysisEmpDesignation.DataBind();

            lstAnalysisEmpDesignation.Enabled = true;

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "lstConsolidatedDepartment_SelectedIndexChanged", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }


    protected void lstActionDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (lstActionDepartment.SelectedItem.Value == "1")
            {
                lstActionProject.Enabled = true;
            }
            else
            {
                //Venkatesh : 4C_Support Report 26-Feb-2014 : Start 
                lstActionProject.ClearSelection();
                //Venkatesh : 4C_Support Report 26-Feb-2014 : End 
                lstActionProject.Enabled = false;
            }

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "lstConsolidatedDepartment_SelectedIndexChanged", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }


    protected void btnClear_Click(object sender, EventArgs e)
    {
        doClearControl();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }





    protected void btnReports_Click(object sender, EventArgs e)
    {
        Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();

        #region "Filedelete"
        //string fileName_Consolidated = Server.MapPath("~/Consolidated.xls");
        if (lnkConsolidated.Font.Bold == true)
        {
            if (File.Exists(Server.MapPath("~/Consolidated.xls")))
            {
                File.Delete(Server.MapPath("~/Consolidated.xls"));
            }
        }
        if (lnkAnalysis.Font.Bold == true)
        {
            if (File.Exists(Server.MapPath("~/Analysis.xls")))
            {
                File.Delete(Server.MapPath("~/Analysis.xls"));
            }
        }
        if (lnkAction.Font.Bold == true)
        {
            if (File.Exists(Server.MapPath("~/ActionReport.xls")))
            {
                File.Delete(Server.MapPath("~/ActionReport.xls"));
            }
        }
        if (lnkStatus.Font.Bold == true)
        {
            if (File.Exists(Server.MapPath("~/StatusReport.xls")))
            {
                File.Delete(Server.MapPath("~/StatusReport.xls"));
            }
        }
        if (lnkMovement.Font.Bold == true)
        {
            if (File.Exists(Server.MapPath("~/MovementReport.xls")))
            {
                File.Delete(Server.MapPath("~/MovementReport.xls"));
            }
        }
        if (lnkCountReport.Font.Bold == true)
        {
            if (File.Exists(Server.MapPath("~/CountReport.xls")))
            {
                File.Delete(Server.MapPath("~/CountReport.xls"));
            }
        }
        #endregion "Filedelete"

        if (lnkConsolidated.Font.Bold == true)
        {
            #region "Consolidated"
            string strProjectId = "";

            string strDept = string.Empty;
            string strProject = string.Empty;
            string strDesignationId = string.Empty;

            foreach (ListItem item in lstConsolidatedDepartment.Items)
            {
                if (item.Selected)
                {
                    strDept = strDept + item.Value + ",";
                }
            }
            foreach (ListItem item in lstConsolidatedProject.Items)
            {
                if (item.Selected)
                {
                    strProject = strProject + item.Value + ",";
                }
            }
            foreach (ListItem item in lstConsolidatedEmpDesignation.Items)
            {
                if (item.Selected)
                {
                    strDesignationId = strDesignationId + item.Value + ",";
                }
            }

            //Umesh: Issue 'Modal Popup issue in chrome' Starts
            DataSet dsConsolidatedReport = fourCBAL.GetConsolidatedReport(int.Parse(ViewState["LoginEmpId"].ToString()), HfConsolidatedEmpId.Value, strDesignationId, strDept, strProject, int.Parse(ddlConsolidatedPeriod.SelectedItem.Value));//,ChkIsSupportConsolidate.Checked);
            //Umesh: Issue 'Modal Popup issue in chrome' Ends

            int period = int.Parse(ddlConsolidatedPeriod.SelectedItem.Value);

            DataTable dtPeriod = new DataTable();
            dtPeriod.Columns.Add("Id");
            dtPeriod.Columns.Add("MonthYear");

            for (int iPeriod = period; iPeriod > 0; iPeriod--)
            {

                //DateTime dtDate = DateTime.Now.AddMonths(-iPeriod);
                DateTime dtDate = DateTime.Now.AddMonths(-iPeriod);

                DataRow dr = dtPeriod.NewRow();
                dr["Id"] = iPeriod;
                dr["MonthYear"] = dtDate.Month + "-" + dtDate.Year;
                dtPeriod.Rows.Add(dr);
            }

            //Excel Object creation
            Microsoft.Office.Interop.Excel.ApplicationClass ExcelAppConsolidated = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            xlWorkBook = ExcelAppConsolidated.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet Sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)ExcelAppConsolidated.Sheets[1];

            //int titleMonthYear = 5; //start from E column 
            int titleMonthYear = 7; //start from E column 

            //Loop over period
            for (int iRow = 1; iRow <= dtPeriod.Rows.Count; iRow++)
            {

                string monthYr = dtPeriod.Rows[iRow - 1]["MonthYear"].ToString();

                //create header
                if (iRow == 1)
                {
                    Microsoft.Office.Interop.Excel.Range headerRange = Sheet1.get_Range("A1", "F1");
                    Microsoft.Office.Interop.Excel.Range headerRangeMerge1 = Sheet1.get_Range("A1", "A2");
                    Microsoft.Office.Interop.Excel.Range headerRangeMerge2 = Sheet1.get_Range("B1", "B2");
                    Microsoft.Office.Interop.Excel.Range headerRangeMerge3 = Sheet1.get_Range("C1", "C2");
                    Microsoft.Office.Interop.Excel.Range headerRangeMerge4 = Sheet1.get_Range("D1", "D2");
                    Microsoft.Office.Interop.Excel.Range headerRangeMerge5 = Sheet1.get_Range("E1", "E2");
                    Microsoft.Office.Interop.Excel.Range headerRangeMerge6 = Sheet1.get_Range("F1", "F2");

                    headerRangeMerge1.Merge(Type.Missing);
                    headerRangeMerge2.Merge(Type.Missing);
                    headerRangeMerge3.Merge(Type.Missing);
                    headerRangeMerge4.Merge(Type.Missing);
                    headerRangeMerge5.Merge(Type.Missing);
                    headerRangeMerge6.Merge(Type.Missing);

                    headerRange.Font.Bold = true;
                    headerRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    //headerRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                    //                 Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium,
                    //                 Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                    //                 Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);

                    headerRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(132, 91, 173));
                    headerRange.EntireColumn.AutoFit();
                    headerRange.EntireRow.AutoFit();

                    //Assign Column Name
                    //for (int i = 0; i <= 3; i++) // as 4 fixed header column name
                    for (int i = 0; i <= 5; i++) // as 4 fixed header column name
                    {
                        Sheet1.Cells[1, i + 1] = dsConsolidatedReport.Tables[0].Columns[i].ColumnName;
                        Microsoft.Office.Interop.Excel.Range headerDataRange = Sheet1.get_Range(Sheet1.Cells[1, i + 1], Sheet1.Cells[1, i + 1]);
                        headerDataRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                        headerDataRange.Font.Bold = true;
                        headerDataRange.EntireRow.AutoFit();
                        headerDataRange.EntireColumn.AutoFit();
                    }

                    //Assign Data
                    for (int i = 0; i < dsConsolidatedReport.Tables[0].Rows.Count; i++)
                    {
                        for (int j = 0; j < dsConsolidatedReport.Tables[0].Columns.Count - 1; j++) //we don't want project column heance count - 1
                        {
                            Sheet1.Cells[i + 3, j + 1] = dsConsolidatedReport.Tables[0].Rows[i][j].ToString();

                            Microsoft.Office.Interop.Excel.Range rowRange = Sheet1.get_Range(Sheet1.Cells[i + 3, j + 1], Sheet1.Cells[1, 3]);
                            rowRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                            rowRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                                                 Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium,
                                                 Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                                                 Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);

                            rowRange.EntireRow.AutoFit();
                            rowRange.EntireColumn.AutoFit();
                        }
                    }

                }

                Microsoft.Office.Interop.Excel.Range titleRange = ExcelAppConsolidated.get_Range(Sheet1.Cells[1, titleMonthYear], Sheet1.Cells[1, titleMonthYear + 5]);

                Sheet1.Cells[1, titleMonthYear] = monthYr;

                titleRange.Merge(Type.Missing);      //Center the title horizontally then vertically at the above defined range     
                titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                titleRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                //Increase the font-size of the title     
                titleRange.Font.Size = 12;
                //Make the title bold     
                titleRange.Font.Bold = true;
                //Give the title background color     
                //titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(132, 91, 173));
                titleRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                //Set the title row height     
                titleRange.RowHeight = 35;

                Microsoft.Office.Interop.Excel.Range headerRangeSecond = Sheet1.get_Range(Sheet1.Cells[2, titleMonthYear], Sheet1.Cells[2, titleMonthYear + 5]);
                headerRangeSecond.Font.Bold = true;
                headerRangeSecond.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                headerRangeSecond.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(132, 91, 173));
                //headerRangeSecond.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                //                         Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium,
                //                         Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                //                         Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);

                //Assign Column Name
                //for (int i = 5; i < 11; i++) // as 4 fixed header column name
                for (int i = 7; i < 13; i++) // as 4 fixed header column name
                {
                    //Sheet1.Cells[2, titleMonthYear + i - 5] = dsConsolidatedReport.Tables[1].Columns[i].ColumnName;
                    Sheet1.Cells[2, titleMonthYear + i - 7] = dsConsolidatedReport.Tables[1].Columns[i].ColumnName;

                    ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[2, titleMonthYear + i - 7]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[2, titleMonthYear + i - 7]).Font.Bold = true;
                    ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[2, titleMonthYear + i - 7]).EntireColumn.AutoFit();
                    ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[2, titleMonthYear + i - 7]).EntireRow.AutoFit();
                }

                for (int i = 0; i <= dsConsolidatedReport.Tables[0].Rows.Count - 1; i++)
                {
                    string empName = dsConsolidatedReport.Tables[0].Rows[i]["EmployeeName"].ToString();
                    string projectName = dsConsolidatedReport.Tables[0].Rows[i]["ProjectName"] == null ? null : dsConsolidatedReport.Tables[0].Rows[i]["ProjectName"].ToString();

                    int nRemoveSpecialChar = empName.IndexOf("'");
                    if (nRemoveSpecialChar != -1)
                    {
                        empName = empName.Replace("'", "");
                    }

                    DataRow[] result = dsConsolidatedReport.Tables[1].Select("EmployeeName = '" + empName + "' and ProjectName = '" + projectName + "' and MonthYear = '" + monthYr + "'");

                    Microsoft.Office.Interop.Excel.Range rowRange = Sheet1.get_Range(Sheet1.Cells[i + 3, titleMonthYear], Sheet1.Cells[i + 3, titleMonthYear + 5]);
                    if (result.Take(1).Any())
                    {
                        rowRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                        //rowRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                        //                     Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium,
                        //                     Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                        //                     Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);


                        rowRange.EntireColumn.AutoFit();
                        rowRange.EntireRow.AutoFit();
                    }


                    //for (int j = 0; j <= dsConsolidatedReport.Tables[1].Rows.Count; j++)
                    //{

                    foreach (DataRow dr in result)
                    {
                        Sheet1.Cells[i + 3, titleMonthYear] = dr["ProjectName"].ToString();
                        Sheet1.Cells[i + 3, titleMonthYear + 1] = dr["Manager Name"].ToString();

                        Sheet1.Cells[i + 3, titleMonthYear + 2] = dr["Competency"].ToString();
                        Microsoft.Office.Interop.Excel.Range rowRangeCompetency = Sheet1.get_Range(Sheet1.Cells[i + 3, titleMonthYear + 2], Sheet1.Cells[i + 3, titleMonthYear + 2]);
                        if (dr["Competency"].ToString().Trim() == "Green")
                        {
                            rowRangeCompetency.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
                        }
                        else if (dr["Competency"].ToString().Trim() == "Red")
                        {
                            rowRangeCompetency.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                        }
                        else if (dr["Competency"].ToString().Trim() == "Amber")
                        {
                            //rowRangeCompetency.Interior.Color = System.Drawing.Color.FromArgb(255,204,102).ToArgb();
                            rowRangeCompetency.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 204, 0));
                        }


                        Sheet1.Cells[i + 3, titleMonthYear + 3] = dr["Communication"].ToString();
                        Microsoft.Office.Interop.Excel.Range rowRangeCommunication = Sheet1.get_Range(Sheet1.Cells[i + 3, titleMonthYear + 3], Sheet1.Cells[i + 3, titleMonthYear + 3]);
                        if (dr["Communication"].ToString().Trim() == "Green")
                        {
                            rowRangeCommunication.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
                        }
                        else if (dr["Communication"].ToString().Trim() == "Red")
                        {
                            rowRangeCommunication.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                        }
                        else if (dr["Communication"].ToString().Trim() == "Amber")
                        {
                            //rowRangeCommunication.Interior.Color = System.Drawing.Color.FromArgb(255, 204, 102).ToArgb();
                            rowRangeCommunication.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 204, 0));
                        }


                        Sheet1.Cells[i + 3, titleMonthYear + 4] = dr["Commitment"].ToString();
                        Microsoft.Office.Interop.Excel.Range rowRangeCommitment = Sheet1.get_Range(Sheet1.Cells[i + 3, titleMonthYear + 4], Sheet1.Cells[i + 3, titleMonthYear + 4]);
                        if (dr["Commitment"].ToString().Trim() == "Green")
                        {
                            rowRangeCommitment.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
                        }
                        else if (dr["Commitment"].ToString().Trim() == "Red")
                        {
                            rowRangeCommitment.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                        }
                        else if (dr["Commitment"].ToString().Trim() == "Amber")
                        {
                            //rowRangeCommitment.Interior.Color = System.Drawing.Color.FromArgb(255, 204, 0).ToArgb();
                            rowRangeCommitment.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 204, 0));

                        }


                        Sheet1.Cells[i + 3, titleMonthYear + 5] = dr["Collaboration"].ToString();
                        Microsoft.Office.Interop.Excel.Range rowRangeCollaboration = Sheet1.get_Range(Sheet1.Cells[i + 3, titleMonthYear + 5], Sheet1.Cells[i + 3, titleMonthYear + 5]);
                        if (dr["Collaboration"].ToString().Trim() == "Green")
                        {
                            rowRangeCollaboration.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
                        }
                        else if (dr["Collaboration"].ToString().Trim() == "Red")
                        {
                            rowRangeCollaboration.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                        }
                        else if (dr["Collaboration"].ToString().Trim() == "Amber")
                        {
                            //rowRangeCollaboration.Interior.Color = System.Drawing.Color.FromArgb(255, 204, 102).ToArgb();
                            rowRangeCollaboration.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 204, 0));

                        }



                    }
                    //}
                }

                int blankRow = 0;
                int rowCount = dsConsolidatedReport.Tables[0].Rows.Count;
                if (titleMonthYear == 5)
                {
                    blankRow = titleMonthYear + 5; // on 11th column it is require
                }
                else
                {
                    blankRow = (titleMonthYear + 5); // 5 intially five column fixed then 6 column which are chnage as per month and year and 6 are the columns.
                }

                //Microsoft.Office.Interop.Excel.Range blankExcelRow = Sheet1.get_Range(Sheet1.Cells[blankRow + 1, blankRow + 1], Sheet1.Cells[blankRow + 1, blankRow + 1]);  //, (rowCount + 2)); // + 2 is header row
                Microsoft.Office.Interop.Excel.Range blankExcelRow = Sheet1.get_Range(Sheet1.Cells[1, blankRow + 1], Sheet1.Cells[rowCount + 2, blankRow + 1]);  //, (rowCount + 2)); // + 2 is header row

                blankExcelRow.ColumnWidth = 0.5;
                //blankExcelRow.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                //                             Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium,
                //                             Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                //                             Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
                blankExcelRow.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(102, 51, 153));
                blankExcelRow.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);





                titleMonthYear = titleMonthYear + 7;

            }

            Microsoft.Office.Interop.Excel.Range borderRange = Sheet1.get_Range(Sheet1.Cells[1, 1], Sheet1.Cells[dsConsolidatedReport.Tables[0].Rows.Count + 2, titleMonthYear - 1]);  //, (rowCount + 2)); // + 2 is header row
            borderRange.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.Color.Black.ToArgb();
            borderRange.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].Color = System.Drawing.Color.Black.ToArgb();
            borderRange.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].Color = System.Drawing.Color.Black.ToArgb();
            borderRange.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.Color.Black.ToArgb();
            borderRange.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].Color = System.Drawing.Color.Black.ToArgb();
            borderRange.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].Color = System.Drawing.Color.Black.ToArgb();


            string fileName = Server.MapPath("~/Consolidated.xls");
            //string fileName = "Consolidated.xls";
            string misValue = null;
            ExcelAppConsolidated.DisplayAlerts = false; //Supress overwrite request     
            xlWorkBook.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            ExcelAppConsolidated.Quit();

            releaseObject(Sheet1);
            //releaseObject(Sheet2);
            releaseObject(xlWorkBook);
            releaseObject(ExcelAppConsolidated);


            String FilePath = Server.MapPath("~/Consolidated.xls");
            //String FilePath = "Consolidated.xls";
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "text/plain";
            response.AddHeader("Content-Disposition", "attachment; filename=ConsolidatedReport-" + DateTime.Now.ToShortDateString() + ".xls;");
            response.TransmitFile(FilePath);
            response.Flush();
            response.Close();
            #endregion "Consolidated"
        }
        else if (lnkAnalysis.Font.Bold == true)
        {
            #region "Analysis"

            string strProjectId = "";

            string strDept = string.Empty;
            string strProject = string.Empty;
            string strDesignationId = string.Empty;
            string strCType = string.Empty;

            foreach (ListItem item in lstAnalysisDepartment.Items)
            {
                if (item.Selected)
                {
                    strDept = strDept + item.Value + ",";
                }
            }
            foreach (ListItem item in lstAnalysisProject.Items)
            {
                if (item.Selected)
                {
                    strProject = strProject + item.Value + ",";
                }
            }
            foreach (ListItem item in lstAnalysisEmpDesignation.Items)
            {
                if (item.Selected)
                {
                    strDesignationId = strDesignationId + item.Value + ",";
                }
            }

            foreach (ListItem item in chkCTypeAnalysis.Items)
            {
                if (item.Selected)
                {
                    strCType = strCType + item.Value + ",";
                }
            }

            //Umesh: Issue 'Modal Popup issue in chrome' Starts
            DataSet dsAnalysisReport = fourCBAL.GetAnalysisReport(int.Parse(ViewState["LoginEmpId"].ToString()), int.Parse(ddlAnalysisPeriod.SelectedItem.Value), strDept, strProject, strDesignationId, strCType);//, ChkIsSupportAnalysis .Checked);
            //Umesh: Issue 'Modal Popup issue in chrome' Ends

            int period = int.Parse(ddlAnalysisPeriod.SelectedItem.Value);

            DataTable dtPeriod = new DataTable();
            dtPeriod.Columns.Add("Id");
            dtPeriod.Columns.Add("MonthYear");

            for (int iPeriod = period  ; iPeriod > 0; iPeriod--)
            {

                //DateTime dtDate = DateTime.Now.AddMonths(-iPeriod);
                DateTime dtDate = DateTime.Now.AddMonths(-(iPeriod));

                DataRow dr = dtPeriod.NewRow();
                dr["Id"] = iPeriod;
                dr["MonthYear"] = dtDate.Month + "-" + dtDate.Year;
                dtPeriod.Rows.Add(dr);
            }


            bool blCompetency = false;
            bool blCommunication = false;
            bool blCommitment = false;
            bool blCollaboration = false;
            bool noItemSelected = false;

            foreach (ListItem item in chkCTypeAnalysis.Items)
            {
                if (item.Selected)
                {
                    noItemSelected = true;

                    if (item.Text == "Competency")
                    {
                        blCompetency = true;
                    }
                    else if (item.Text == "Communication")
                    {
                        blCommunication = true;
                    }
                    else if (item.Text == "Commitment")
                    {
                        blCommitment = true;
                    }
                    else if (item.Text == "Collaboration")
                    {
                        blCollaboration = true;
                    }

                }
            }

            if (noItemSelected == false)
            {
                blCompetency = true;
                blCommunication = true;
                blCommitment = true;
                blCollaboration = true;
            }


            int loopStatus = 4;
            //int loopStatus = 6;




            Microsoft.Office.Interop.Excel.ApplicationClass ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            xlWorkBook = ExcelApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet Sheet1 = null;

            //int titleMonthYear = 4;
            int titleMonthYear = 6;

            for (int iRow = 1; iRow <= loopStatus; iRow++)
            {
                //   Sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.ThisWorkbook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);



                string CType = string.Empty;
                string excelSheetName = string.Empty;

                if (iRow == 1 && blCollaboration)
                {
                    excelSheetName = "Collaboration";
                    CType = "Collaboration";
                }
                else if (iRow == 2 && blCommitment)
                {
                    excelSheetName = "Commitment";
                    CType = "Commitment";
                }
                else if (iRow == 3 && blCommunication)
                {
                    excelSheetName = "Communication";
                    CType = "Communication";
                }
                else if (iRow == 4 && blCompetency)
                {
                    excelSheetName = "Competency";
                    CType = "Competency";
                }

                if (!string.IsNullOrEmpty(excelSheetName))
                {

                    Sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    Sheet1.Name = excelSheetName;

                    //Assign Column Name
                    //for (int i = 0; i <= 3; i++) // as 4 fixed header column name
                    //{

                    Microsoft.Office.Interop.Excel.Range headerPositive = Sheet1.get_Range(Sheet1.Cells[1, 1], Sheet1.Cells[1, titleMonthYear + dtPeriod.Rows.Count]);
                    headerPositive.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    headerPositive.Font.Bold = true;
                    headerPositive.Merge(Type.Missing);      //Center the title horizontally then vertically at the above defined range     
                    headerPositive.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    headerPositive.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                    headerPositive.RowHeight = 35;
                    headerPositive.EntireRow.AutoFit();
                    headerPositive.EntireColumn.AutoFit();
                    headerPositive.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

                    Sheet1.Cells[1, 1] = "Analysis Report";



                    //Assign Column Name - poitive movement
                    Sheet1.Cells[2, 1] = dsAnalysisReport.Tables[1].Columns["EmployeeName"].ColumnName;
                    Sheet1.Cells[2, 2] = dsAnalysisReport.Tables[1].Columns["EmpCode"].ColumnName;
                    Sheet1.Cells[2, 3] = dsAnalysisReport.Tables[1].Columns["ConfirmationStatus"].ColumnName;
                    Sheet1.Cells[2, 4] = dsAnalysisReport.Tables[1].Columns["Designation"].ColumnName;
                    Sheet1.Cells[2, 5] = dsAnalysisReport.Tables[1].Columns["Department"].ColumnName;
                    Sheet1.Cells[2, 6] = dsAnalysisReport.Tables[1].Columns["ProjectName"].ColumnName;

                    //Microsoft.Office.Interop.Excel.Range headerDataRange = Sheet1.get_Range(Sheet1.Cells[2, 1], Sheet1.Cells[2, 4]);
                    Microsoft.Office.Interop.Excel.Range headerDataRange = Sheet1.get_Range(Sheet1.Cells[2, 1], Sheet1.Cells[2, 6]);
                    headerDataRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    headerDataRange.Font.Bold = true;
                    headerDataRange.EntireRow.AutoFit();
                    headerDataRange.EntireColumn.AutoFit();
                    headerDataRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(132, 91, 173));

                    int compVariableEmpData = 0;
                    int commVariableEmpData = 0;
                    int commitmentVariableEmpData = 0;
                    int collaVariableEmpData = 0;

                    //Assign Data -- positive
                    for (int i = 0; i <= dsAnalysisReport.Tables[0].Rows.Count - 1; i++)
                    {
                        if (dsAnalysisReport.Tables[0].Rows[i]["CType"].ToString() == CType)
                        {
                            string monthYr = dtPeriod.Rows[dtPeriod.Rows.Count - 1]["MonthYear"].ToString();

                            int empId = int.Parse(dsAnalysisReport.Tables[0].Rows[i]["EmpId"].ToString());
                            int empDept = int.Parse(dsAnalysisReport.Tables[0].Rows[i]["DepartmentId"].ToString());
                            int projectId = 0;

                            if (!string.IsNullOrEmpty(dsAnalysisReport.Tables[0].Rows[i]["ProjectId"].ToString()))
                            {
                                projectId = int.Parse(dsAnalysisReport.Tables[0].Rows[i]["ProjectId"].ToString());
                            }


                            DataRow[] result = null;
                            if (projectId > 0)
                            {
                                result = dsAnalysisReport.Tables[1].Select("EmpId = '" + empId + "' and ProjectId = '" + projectId + "' and MonthYear = '" + monthYr + "' and CType = '" + CType + "'");
                            }
                            else
                            {
                                result = dsAnalysisReport.Tables[1].Select("EmpId = '" + empId + "' and ProjectId = '" + projectId + "' and MonthYear = '" + monthYr + "' and CType = '" + CType + "'");
                            }

                            foreach (DataRow drEmp in result)
                            {
                                //Sheet1.Cells[i + 3, 1] = drEmp["EmployeeName"].ToString();
                                //Sheet1.Cells[i + 3, 2] = drEmp["Designation"].ToString();
                                //Sheet1.Cells[i + 3, 3] = drEmp["Department"].ToString();
                                //Sheet1.Cells[i + 3, 4] = drEmp["ProjectName"].ToString();



                                if (CType == "Competency")
                                {
                                    Sheet1.Cells[compVariableEmpData + 3, 1] = drEmp["EmployeeName"].ToString();
                                    Sheet1.Cells[compVariableEmpData + 3, 2] = drEmp["EmpCode"].ToString();
                                    Sheet1.Cells[compVariableEmpData + 3, 3] = drEmp["ConfirmationStatus"].ToString();
                                    Sheet1.Cells[compVariableEmpData + 3, 4] = drEmp["Designation"].ToString();
                                    Sheet1.Cells[compVariableEmpData + 3, 5] = drEmp["Department"].ToString();
                                    Sheet1.Cells[compVariableEmpData + 3, 6] = drEmp["ProjectName"].ToString();

                                    compVariableEmpData = compVariableEmpData + 1;
                                }
                                else if (CType == "Communication")
                                {
                                    Sheet1.Cells[commVariableEmpData + 3, 1] = drEmp["EmployeeName"].ToString();
                                    Sheet1.Cells[commVariableEmpData + 3, 2] = drEmp["EmpCode"].ToString();
                                    Sheet1.Cells[commVariableEmpData + 3, 3] = drEmp["ConfirmationStatus"].ToString();
                                    Sheet1.Cells[commVariableEmpData + 3, 4] = drEmp["Designation"].ToString();
                                    Sheet1.Cells[commVariableEmpData + 3, 5] = drEmp["Department"].ToString();
                                    Sheet1.Cells[commVariableEmpData + 3, 6] = drEmp["ProjectName"].ToString();

                                    commVariableEmpData = commVariableEmpData + 1;
                                }
                                else if (CType == "Commitment")
                                {
                                    Sheet1.Cells[commitmentVariableEmpData + 3, 1] = drEmp["EmployeeName"].ToString();
                                    Sheet1.Cells[commitmentVariableEmpData + 3, 2] = drEmp["EmpCode"].ToString();
                                    Sheet1.Cells[commitmentVariableEmpData + 3, 3] = drEmp["ConfirmationStatus"].ToString();
                                    Sheet1.Cells[commitmentVariableEmpData + 3, 4] = drEmp["Designation"].ToString();
                                    Sheet1.Cells[commitmentVariableEmpData + 3, 5] = drEmp["Department"].ToString();
                                    Sheet1.Cells[commitmentVariableEmpData + 3, 6] = drEmp["ProjectName"].ToString();

                                    commitmentVariableEmpData = commitmentVariableEmpData + 1;
                                }
                                else if (CType == "Collaboration")
                                {
                                    Sheet1.Cells[collaVariableEmpData + 3, 1] = drEmp["EmployeeName"].ToString();
                                    Sheet1.Cells[collaVariableEmpData + 3, 2] = drEmp["EmpCode"].ToString();
                                    Sheet1.Cells[collaVariableEmpData + 3, 3] = drEmp["ConfirmationStatus"].ToString();
                                    Sheet1.Cells[collaVariableEmpData + 3, 4] = drEmp["Designation"].ToString();
                                    Sheet1.Cells[collaVariableEmpData + 3, 5] = drEmp["Department"].ToString();
                                    Sheet1.Cells[collaVariableEmpData + 3, 6] = drEmp["ProjectName"].ToString();

                                    collaVariableEmpData = collaVariableEmpData + 1;
                                }
                            }

                        }
                    }



                    //Loop over period
                    for (int iRowP = 1; iRowP <= dtPeriod.Rows.Count; iRowP++)
                    {
                        string monthYr = dtPeriod.Rows[iRowP - 1]["MonthYear"].ToString();

                        Microsoft.Office.Interop.Excel.Range headerSecond = Sheet1.get_Range(Sheet1.Cells[2, titleMonthYear + iRowP], Sheet1.Cells[2, titleMonthYear + iRowP]);
                        headerSecond.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                        headerSecond.Font.Bold = true;
                        headerSecond.EntireRow.AutoFit();
                        headerSecond.EntireColumn.AutoFit();
                        headerSecond.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(132, 91, 173));

                        Sheet1.Cells[2, titleMonthYear + iRowP] = monthYr.ToString();

                        int compVariable = 0;
                        int commVariable = 0;
                        int commitVariable = 0;
                        int collVariable = 0;

                        // Positive Action
                        for (int i = 0; i <= dsAnalysisReport.Tables[0].Rows.Count - 1; i++)
                        {
                            if (dsAnalysisReport.Tables[0].Rows[i]["CType"].ToString() == CType)
                            {
                                int empId = int.Parse(dsAnalysisReport.Tables[0].Rows[i]["EmpId"].ToString());
                                int empDept = int.Parse(dsAnalysisReport.Tables[0].Rows[i]["DepartmentId"].ToString());
                                int projectId = 0;

                                if (!string.IsNullOrEmpty(dsAnalysisReport.Tables[0].Rows[i]["ProjectId"].ToString()))
                                {
                                    projectId = int.Parse(dsAnalysisReport.Tables[0].Rows[i]["ProjectId"].ToString());
                                }

                                DataRow[] result = null;
                                if (projectId > 0)
                                {
                                    result = dsAnalysisReport.Tables[1].Select("EmpId = '" + empId + "' and ProjectId = '" + projectId + "' and MonthYear = '" + monthYr + "' and CType = '" + CType + "'");
                                    //result = dsAnalysisReport.Tables[1].Select("EmpId = 1035 and MonthYear = '" + monthYr + "' and CType = '" + CType + "'");
                                }
                                else
                                {
                                    result = dsAnalysisReport.Tables[1].Select("EmpId = '" + empId + "' and ProjectId = '" + projectId + "' and MonthYear = '" + monthYr + "' and CType = '" + CType + "'");
                                }

                               

                                //if no data then add blank row
                                if (result.Any() == false)
                                {
                                    if (CType == "Competency")
                                    {
                                        compVariable = compVariable + 1;
                                    }
                                    else if (CType == "Communication")
                                    {
                                        commVariable = commVariable + 1;
                                    }
                                    else if (CType == "Commitment")
                                    {
                                        commitVariable = commitVariable + 1;
                                    }
                                    else if (CType == "Collaboration")
                                    {
                                        collVariable = collVariable + 1;
                                    }
                                }

                                foreach (DataRow dr in result)
                                {
                                    //Sheet1.Cells[i + 3, titleMonthYear] = dr["ProjectName"].ToString();

                                    //Sheet1.Cells[i + 1, titleMonthYear + 1] = dr["Manager Name"].ToString();



                                    if (CType == "Competency")
                                    {

                                        //Sheet1.Cells[i + 3, titleMonthYear + iRowP] = dr["Competency"].ToString();
                                        Sheet1.Cells[compVariable + 3, titleMonthYear + iRowP] = dr["Competency"].ToString();

                                        
                                        //Microsoft.Office.Interop.Excel.Range rowRangeCompetency = Sheet1.get_Range(Sheet1.Cells[i + 3, titleMonthYear + iRowP], Sheet1.Cells[i + 3, titleMonthYear + iRowP]);
                                        Microsoft.Office.Interop.Excel.Range rowRangeCompetency = Sheet1.get_Range(Sheet1.Cells[compVariable + 3, titleMonthYear + iRowP], Sheet1.Cells[compVariable + 3, titleMonthYear + iRowP]);

                                        if (dr["Competency"].ToString().Trim() == "Green")
                                        {
                                            rowRangeCompetency.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
                                        }
                                        else if (dr["Competency"].ToString().Trim() == "Red")
                                        {
                                            rowRangeCompetency.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                        }
                                        else if (dr["Competency"].ToString().Trim() == "Amber")
                                        {
                                            //rowRangeCompetency.Interior.Color = System.Drawing.Color.FromArgb(255,204,102).ToArgb();
                                            rowRangeCompetency.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 204, 0));
                                        }

                                        rowRangeCompetency.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                                                 Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);

                                        compVariable = compVariable + 1;

                                    }
                                    else if (CType == "Communication")
                                    {
                                        //Sheet1.Cells[i + 3, titleMonthYear + iRowP] = dr["Communication"].ToString();
                                        Sheet1.Cells[commVariable + 3, titleMonthYear + iRowP] = dr["Communication"].ToString();
                                        //Microsoft.Office.Interop.Excel.Range rowRangeCommunication = Sheet1.get_Range(Sheet1.Cells[i + 3, titleMonthYear + iRowP], Sheet1.Cells[i + 3, titleMonthYear + iRowP]);
                                        Microsoft.Office.Interop.Excel.Range rowRangeCommunication = Sheet1.get_Range(Sheet1.Cells[commVariable + 3, titleMonthYear + iRowP], Sheet1.Cells[commVariable + 3, titleMonthYear + iRowP]);
                                        if (dr["Communication"].ToString().Trim() == "Green")
                                        {
                                            rowRangeCommunication.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
                                        }
                                        else if (dr["Communication"].ToString().Trim() == "Red")
                                        {
                                            rowRangeCommunication.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                        }
                                        else if (dr["Communication"].ToString().Trim() == "Amber")
                                        {
                                            //rowRangeCommunication.Interior.Color = System.Drawing.Color.FromArgb(255, 204, 102).ToArgb();
                                            rowRangeCommunication.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 204, 0));
                                        }

                                        rowRangeCommunication.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                                                 Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
                                        commVariable = commVariable + 1;

                                    }
                                    else if (CType == "Commitment")
                                    {

                                        //Sheet1.Cells[i + 3, titleMonthYear + iRowP] = dr["Commitment"].ToString();
                                        Sheet1.Cells[commitVariable + 3, titleMonthYear + iRowP] = dr["Commitment"].ToString();

                                        //Microsoft.Office.Interop.Excel.Range rowRangeCommitment = Sheet1.get_Range(Sheet1.Cells[i + 3, titleMonthYear + iRowP], Sheet1.Cells[i + 3, titleMonthYear + iRowP]);
                                        Microsoft.Office.Interop.Excel.Range rowRangeCommitment = Sheet1.get_Range(Sheet1.Cells[commitVariable + 3, titleMonthYear + iRowP], Sheet1.Cells[commitVariable + 3, titleMonthYear + iRowP]);

                                        if (dr["Commitment"].ToString().Trim() == "Green")
                                        {
                                            rowRangeCommitment.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
                                        }
                                        else if (dr["Commitment"].ToString().Trim() == "Red")
                                        {
                                            rowRangeCommitment.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                        }
                                        else if (dr["Commitment"].ToString().Trim() == "Amber")
                                        {
                                            //rowRangeCommitment.Interior.Color = System.Drawing.Color.FromArgb(255, 204, 0).ToArgb();
                                            rowRangeCommitment.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 204, 0));

                                        }
                                        rowRangeCommitment.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                                                 Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);

                                        commitVariable = commitVariable + 1;
                                    }
                                    else if (CType == "Collaboration")
                                    {
                                        //Sheet1.Cells[i + 3, titleMonthYear + iRowP] = dr["Collaboration"].ToString();
                                        Sheet1.Cells[collVariable + 3, titleMonthYear + iRowP] = dr["Collaboration"].ToString();

                                        //Microsoft.Office.Interop.Excel.Range rowRangeCollaboration = Sheet1.get_Range(Sheet1.Cells[i + 3, titleMonthYear + iRowP], Sheet1.Cells[i + 3, titleMonthYear + iRowP]);
                                        Microsoft.Office.Interop.Excel.Range rowRangeCollaboration = Sheet1.get_Range(Sheet1.Cells[collVariable + 3, titleMonthYear + iRowP], Sheet1.Cells[collVariable + 3, titleMonthYear + iRowP]);
                                        if (dr["Collaboration"].ToString().Trim() == "Green")
                                        {
                                            rowRangeCollaboration.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
                                        }
                                        else if (dr["Collaboration"].ToString().Trim() == "Red")
                                        {
                                            rowRangeCollaboration.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                        }
                                        else if (dr["Collaboration"].ToString().Trim() == "Amber")
                                        {
                                            //rowRangeCollaboration.Interior.Color = System.Drawing.Color.FromArgb(255, 204, 102).ToArgb();
                                            rowRangeCollaboration.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 204, 0));

                                        }
                                        rowRangeCollaboration.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                                                 Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);

                                        collVariable = collVariable + 1;

                                    }

                                }

                            }

                        }

                        //Microsoft.Office.Interop.Excel.Range positiveBorder = Sheet1.get_Range(Sheet1.Cells[1, 1], Sheet1.Cells[dsAnalysisReport.Tables[0].Rows.Count + 2, period + 4]); //2 header
                        Microsoft.Office.Interop.Excel.Range positiveBorder = Sheet1.get_Range(Sheet1.Cells[1, 1], Sheet1.Cells[dsAnalysisReport.Tables[0].Rows.Count + 2, period + 6]); //2 header
                        positiveBorder.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.Color.Black.ToArgb();
                        positiveBorder.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].Color = System.Drawing.Color.Black.ToArgb();
                        positiveBorder.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].Color = System.Drawing.Color.Black.ToArgb();
                        positiveBorder.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.Color.Black.ToArgb();
                        positiveBorder.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].Color = System.Drawing.Color.Black.ToArgb();
                        positiveBorder.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].Color = System.Drawing.Color.Black.ToArgb();

                        positiveBorder.EntireColumn.AutoFit();
                        positiveBorder.EntireRow.AutoFit();

                    }

                }
            }



            string fileName = Server.MapPath("~/AnalysisReport.xls");
            //string fileName = "Consolidated.xls";
            string misValue = null;
            ExcelApp.DisplayAlerts = false; //Supress overwrite request     
            xlWorkBook.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            ExcelApp.Quit();

            releaseObject(Sheet1);
            //releaseObject(Sheet2);
            releaseObject(xlWorkBook);
            releaseObject(ExcelApp);


            String FilePath = Server.MapPath("~/AnalysisReport.xls");
            //String FilePath = "Consolidated.xls";
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "text/plain";
            response.AddHeader("Content-Disposition", "attachment; filename=AnalysisReport-" + DateTime.Now.ToShortDateString() + ".xls;");
            response.TransmitFile(FilePath);
            response.Flush();
            response.Close();

            #endregion "Analysis"
        }
        else if (lnkAction.Font.Bold == true)
        {
            #region "Action"

            string strProjectId = "";

            string strDept = string.Empty;
            string strProject = string.Empty;
            string strCType = string.Empty;
            string strColorRating = string.Empty;

            foreach (ListItem item in lstActionDepartment.Items)
            {
                if (item.Selected)
                {
                    strDept = strDept + item.Value + ",";
                }
            }
            foreach (ListItem item in lstActionProject.Items)
            {
                if (item.Selected)
                {
                    strProject = strProject + item.Value + ",";
                }
            }
            foreach (ListItem item in chkActionCType.Items)
            {
                if (item.Selected)
                {
                    strCType = strCType + item.Value + ",";
                }
            }
            foreach (ListItem item in chkActionColorRating.Items)
            {
                if (item.Selected)
                {
                    strColorRating = strColorRating + item.Value + ",";
                }
            }

            //DataSet dsActionReport = fourCBAL.GetActionReport(int.Parse(ViewState["LoginEmpId"].ToString()), HfActionEmpId.Value, HfActionOwnerEmpId.Value, lstActionDepartment.SelectedItem.Value, lstActionProject.SelectedItem.Value, chkActionCType.SelectedItem.Value, chkActionColorRating.SelectedItem.Value, int.Parse(ddlActionPeriod.SelectedItem.Value));
            //Umesh: Issue 'Modal Popup issue in chrome' Starts
            DataSet dsActionReport = fourCBAL.GetActionReport(int.Parse(ViewState["LoginEmpId"].ToString()), HfActionEmpId.Value, HfActionOwnerEmpId.Value, strDept, strProject, strCType, strColorRating, int.Parse(ddlActionPeriod.SelectedItem.Value));//, ChkIsSupportAction.Checked);
            //Umesh: Issue 'Modal Popup issue in chrome' Ends

            int period = int.Parse(ddlActionPeriod.SelectedItem.Value);
            int monthDuration = period;

            Microsoft.Office.Interop.Excel.ApplicationClass ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            xlWorkBook = ExcelApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet Sheet1 = null;

            for (int iRow = 1; iRow <= period; iRow++)
            {
                DateTime dtDate = DateTime.Now.AddMonths(-monthDuration);
                //string monthVal = dtDate.Month + "-" + dtDate.Year;

                //Sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[iRow];
                Sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                Sheet1.Name = dtDate.ToString("MMMM yyyy");
                Sheet1.Cells[1, 1] = "Action Report - " + dtDate.ToString("MMMM yyyy");

                Microsoft.Office.Interop.Excel.Range titleRange = ExcelApp.get_Range(Sheet1.Cells[1, "A"], Sheet1.Cells[1, dsActionReport.Tables[iRow - 1].Columns.Count]);
                titleRange.Merge(Type.Missing);      //Center the title horizontally then vertically at the above defined range     
                titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                titleRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                //Increase the font-size of the title     
                titleRange.Font.Size = 12;
                //Make the title bold     
                titleRange.Font.Bold = true;
                //Give the title background color     
                titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(132, 91, 173));
                //Set the title row height     
                titleRange.RowHeight = 35;
                titleRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

                for (int i = 0; i < dsActionReport.Tables[iRow - 1].Columns.Count; i++)
                {
                    Sheet1.Cells[2, i + 1] = dsActionReport.Tables[iRow - 1].Columns[i].ColumnName;
                    ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[2, i + 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[2, i + 1]).Font.Bold = true;

                }
                for (int i = 0; i < dsActionReport.Tables[iRow - 1].Rows.Count; i++)
                {
                    for (int j = 0; j < dsActionReport.Tables[iRow - 1].Columns.Count; j++)
                    {
                        //Sheet1.Cells[i + 2, j + 1] = dtActionReport.Rows[i][j].ToString();


                        if (dsActionReport.Tables[iRow - 1].Columns.ToString() == "Action Created Date" || dsActionReport.Tables[iRow - 1].Columns.ToString() == "Target Closure Date" || dsActionReport.Tables[iRow - 1].Columns.ToString() == "Actual Closure Date")
                        {
                            //((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[i + 3, j + 1]).EntireColumn.NumberFormat = "D";
                            //((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[i + 3, j + 1]).EntireRow.NumberFormat = "D";

                            //Sheet1.Cells[i + 3, j + 1] = string.Format("{D}", dsActionReport.Tables[iRow - 1].Rows[i][j].ToString());
                            Sheet1.Cells[i + 3, j + 1] = dsActionReport.Tables[iRow - 1].Rows[i][j].ToString();
                        }
                        else
                        {
                            Sheet1.Cells[i + 3, j + 1] = dsActionReport.Tables[iRow - 1].Rows[i][j].ToString();
                        }


                        //Microsoft.Office.Interop.Excel.Range rowRangeC = Sheet1.get_Range(Sheet1.Cells[i + 3, j + 1], Sheet1.Cells[i + 3, j + 1]);
                        ////if (dsActionReport.Tables[iRow - 1].Rows[i]["Competency"].ToString().Trim() == "Green" || dsActionReport.Tables[iRow - 1].Rows[i]["Communication"].ToString().Trim() == "Green" || dsActionReport.Tables[iRow - 1].Rows[i]["Commitment"].ToString().Trim() == "Green" || dsActionReport.Tables[iRow - 1].Rows[i]["Collaboration"].ToString().Trim() == "Green")
                        //if (dsActionReport.Tables[iRow - 1].Rows[i][j].ToString().Trim() == "Green") //|| dsActionReport.Tables[iRow - 1].Rows[i][j].ToString().Trim() == "Green" || dsActionReport.Tables[iRow - 1].Rows[i][j].ToString().Trim() == "Green" || dsActionReport.Tables[iRow - 1].Rows[i]["Collaboration"].ToString().Trim() == "Green")
                        //{
                        //    rowRangeC.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
                        //}
                        ////else if (dsActionReport.Tables[iRow - 1].Rows[i]["Competency"].ToString().Trim() == "Red" || dsActionReport.Tables[iRow - 1].Rows[i]["Communication"].ToString().Trim() == "Red" || dsActionReport.Tables[iRow - 1].Rows[i]["Commitment"].ToString().Trim() == "Red" || dsActionReport.Tables[iRow - 1].Rows[i]["Collaboration"].ToString().Trim() == "Red")
                        //else if (dsActionReport.Tables[iRow - 1].Rows[i][j].ToString().Trim() == "Red")
                        //{
                        //    rowRangeC.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                        //}
                        ////else if (dsActionReport.Tables[iRow - 1].Rows[i]["Competency"].ToString().Trim() == "Amber" || dsActionReport.Tables[iRow - 1].Rows[i]["Communication"].ToString().Trim() == "Amber" || dsActionReport.Tables[iRow - 1].Rows[i]["Commitment"].ToString().Trim() == "Amber" || dsActionReport.Tables[iRow - 1].Rows[i]["Collaboration"].ToString().Trim() == "Amber")
                        //else if (dsActionReport.Tables[iRow - 1].Rows[i][j].ToString().Trim() == "Amber")
                        //{
                        //    //rowRangeCompetency.Interior.Color = System.Drawing.Color.FromArgb(255,204,102).ToArgb();
                        //    rowRangeC.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 204, 0));
                        //}
                    }
                }

                Microsoft.Office.Interop.Excel.Range headerRange = Sheet1.get_Range(Sheet1.Cells[2, 0 + 1], Sheet1.Cells[2, dsActionReport.Tables[iRow - 1].Columns.Count]);
                headerRange.Font.Bold = true;
                headerRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(132, 91, 173));


                Microsoft.Office.Interop.Excel.Range entireDataRange = Sheet1.get_Range(Sheet1.Cells[1, 1], Sheet1.Cells[2 + dsActionReport.Tables[iRow - 1].Rows.Count, 20]);
                entireDataRange.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.Color.Black.ToArgb();
                entireDataRange.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].Color = System.Drawing.Color.Black.ToArgb();
                entireDataRange.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].Color = System.Drawing.Color.Black.ToArgb();
                entireDataRange.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.Color.Black.ToArgb();
                entireDataRange.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].Color = System.Drawing.Color.Black.ToArgb();
                entireDataRange.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].Color = System.Drawing.Color.Black.ToArgb();
                entireDataRange.EntireColumn.AutoFit();
                entireDataRange.EntireRow.AutoFit();

                Microsoft.Office.Interop.Excel.Range entireCRange = Sheet1.get_Range(Sheet1.Cells[3, 5], Sheet1.Cells[5 + dsActionReport.Tables[iRow - 1].Rows.Count, 8]);

                foreach (Microsoft.Office.Interop.Excel.Range li in entireCRange)
                {
                    if (li.Cells.Text.ToString() == "Green")
                    {
                        li.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
                    }
                    else if (li.Cells.Text.ToString() == "Red")
                    {
                        li.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    }
                    else if (li.Cells.Text.ToString() == "Amber")
                    {
                        li.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 204, 0));
                    }
                }



                //Microsoft.Office.Interop.Excel.Range columnWidthRange = Sheet1.get_Range(Sheet1.Cells[2, 9], Sheet1.Cells[2, 10]);
                //Microsoft.Office.Interop.Excel.Range columnWidthRangeRemarks = Sheet1.get_Range(Sheet1.Cells[2, 15], Sheet1.Cells[2, 15]);
                Microsoft.Office.Interop.Excel.Range columnWidthRange = Sheet1.get_Range(Sheet1.Cells[2, 11], Sheet1.Cells[2, 12]);
                Microsoft.Office.Interop.Excel.Range columnWidthRangeRemarks = Sheet1.get_Range(Sheet1.Cells[2, 17], Sheet1.Cells[2, 17]);

                columnWidthRange.UseStandardWidth = 50;
                columnWidthRangeRemarks.UseStandardWidth = 50;
                columnWidthRange.ColumnWidth = 50;
                columnWidthRangeRemarks.ColumnWidth = 50;



                monthDuration = monthDuration - 1;
            }

            string fileName = Server.MapPath("~/ActionReport.xls");
            string misValue = null;
            ExcelApp.DisplayAlerts = false; //Supress overwrite request     
            xlWorkBook.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            ExcelApp.Quit();

            releaseObject(Sheet1);
            // releaseObject(Sheet2);
            releaseObject(xlWorkBook);
            releaseObject(ExcelApp);


            String FilePath = Server.MapPath("~/ActionReport.xls");
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "text/plain";
            response.AddHeader("Content-Disposition", "attachment; filename=ActionReport-" + DateTime.Now.ToShortDateString() + ".xls;");
            response.TransmitFile(FilePath);
            response.Flush();
            response.Close();

            #endregion "Action"
        }
        else if (lnkStatus.Font.Bold == true)
        {
            #region "Status"
            if (ddlStatusMonth.SelectedIndex == 0 || ddlStatusYear.SelectedIndex == 0)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Please select month and year.";
                lblMessage.Style["color"] = "red";
                return;
            }
            else
            {

                lblMessage.Visible = false;
                //Umesh: Issue 'Modal Popup issue in chrome' Starts
                DataSet dsStatusReport = fourCBAL.GetStatusReport(int.Parse(ViewState["LoginEmpId"].ToString()), int.Parse(ddlStatusMonth.SelectedItem.Value), int.Parse(ddlStatusYear.SelectedItem.Value));//, ChkIsSupportStatus.Checked);
                //Umesh: Issue 'Modal Popup issue in chrome' Ends

                int loopStatus = 4;
                Microsoft.Office.Interop.Excel.ApplicationClass ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                xlWorkBook = ExcelApp.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel.Worksheet Sheet1 = null;

                for (int iRow = 1; iRow <= loopStatus; iRow++)
                {
                    //   Sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.ThisWorkbook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                    Sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                    int loopCount = 0;

                    if (iRow == 1)
                    {
                        Sheet1.Name = "Review Done";
                        loopCount = 3;
                    }
                    else if (iRow == 2)
                    {
                        Sheet1.Name = "Send For Review Done";
                        loopCount = 3;
                    }
                    else if (iRow == 3)
                    {
                        Sheet1.Name = "Pending Review";
                        loopCount = 2;
                    }
                    else if (iRow == 4)
                    {
                        Sheet1.Name = "Pending Send For Review";
                        loopCount = 2;
                    }
                    //Sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[iRow];

                    //for (int i = 0; i < dsStatusReport.Tables[loopStatus - iRow].Columns.Count; i++)
                    for (int i = 0; i < loopCount; i++)
                    {
                        Sheet1.Cells[1, i + 1] = dsStatusReport.Tables[loopStatus - iRow].Columns[i].ColumnName;

                        ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[1, i + 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                        ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[1, i + 1]).Font.Bold = true;
                        ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[1, i + 1]).EntireColumn.AutoFit();
                        ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[1, i + 1]).EntireRow.AutoFit();

                    }

                    int jLoopCount = loopCount;
                    for (int i = 0; i < dsStatusReport.Tables[loopStatus - iRow].Rows.Count; i++)
                    //for (int i = 0; i < loopCount; i++)
                    {
                        //for (int j = 0; j < dsStatusReport.Tables[loopStatus - iRow].Columns.Count; j++)
                        for (int j = 0; j < loopCount; j++)
                        {
                            Sheet1.Cells[i + 2, j + 1] = dsStatusReport.Tables[loopStatus - iRow].Rows[i][j].ToString();




                            Microsoft.Office.Interop.Excel.Range rowRange = Sheet1.get_Range(Sheet1.Cells[i + 2, j + 1], Sheet1.Cells[2, dsStatusReport.Tables[loopStatus - iRow].Columns.Count]);
                            rowRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                            rowRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                                                 Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium,
                                                 Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                                                 Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);

                            rowRange.EntireColumn.AutoFit();
                            rowRange.EntireRow.AutoFit();
                        }
                    }

                    //Microsoft.Office.Interop.Excel.Range headerRange = Sheet1.get_Range(Sheet1.Cells[1, 0 + 1], Sheet1.Cells[1, dsStatusReport.Tables[loopStatus - iRow].Columns.Count]);
                    Microsoft.Office.Interop.Excel.Range headerRange = Sheet1.get_Range(Sheet1.Cells[1, 0 + 1], Sheet1.Cells[1, loopCount]);

                    headerRange.Font.Bold = true;

                    headerRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);


                    headerRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                                             Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium,
                                             Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                                             Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);

                    headerRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(132, 91, 173));

                    headerRange.EntireColumn.AutoFit();
                    headerRange.EntireRow.AutoFit();
                }


                string fileName = Server.MapPath("~/StatusReport.xls");
                string misValue = null;
                ExcelApp.DisplayAlerts = false; //Supress overwrite request     
                xlWorkBook.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                ExcelApp.Quit();

                releaseObject(Sheet1);
                // releaseObject(Sheet2);
                releaseObject(xlWorkBook);
                releaseObject(ExcelApp);


                String FilePath = Server.MapPath("~/StatusReport.xls");
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "text/plain";
                response.AddHeader("Content-Disposition", "attachment; filename=StatusReport-" + ddlStatusMonth.SelectedItem.Text + "-" + ddlStatusYear.SelectedItem.Text + " On " + DateTime.Now.ToShortDateString() + ".xls;");
                response.TransmitFile(FilePath);
                response.Flush();
                response.Close();


            }
            #endregion "Status"
        }
        else if (lnkMovement.Font.Bold == true)
        {
            #region "Movement"
            //Umesh: Issue 'Modal Popup issue in chrome' Starts
            DataSet dsMovementReport = fourCBAL.GetMovementReport(int.Parse(ViewState["LoginEmpId"].ToString()), int.Parse(ddlMovementDuration.SelectedItem.Value));//, ChkIsSupportMovement.Checked);
            //Umesh: Issue 'Modal Popup issue in chrome' Ends

            int period = int.Parse(ddlMovementDuration.SelectedItem.Value);

            DataTable dtPeriod = new DataTable();
            dtPeriod.Columns.Add("Id");
            dtPeriod.Columns.Add("MonthYear");

            for (int iPeriod = period; iPeriod > 0; iPeriod--)
            {

                //DateTime dtDate = DateTime.Now.AddMonths(-iPeriod);
                DateTime dtDate = DateTime.Now.AddMonths(-iPeriod);

                DataRow dr = dtPeriod.NewRow();
                dr["Id"] = iPeriod;
                dr["MonthYear"] = dtDate.Month + "-" + dtDate.Year;
                dtPeriod.Rows.Add(dr);
            }

            int loopStatus = 4;
            Microsoft.Office.Interop.Excel.ApplicationClass ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            xlWorkBook = ExcelApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet Sheet1 = null;

            //int titleMonthYear = 4;
            int titleMonthYear = 6;

            for (int iRow = 1; iRow <= loopStatus; iRow++)
            {
                //   Sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.ThisWorkbook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                Sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                string CType = string.Empty;
                if (iRow == 1)
                {
                    Sheet1.Name = "Collaboration";
                    CType = "Collaboration";
                }
                else if (iRow == 2)
                {
                    Sheet1.Name = "Commitment";
                    CType = "Commitment";
                }
                else if (iRow == 3)
                {
                    Sheet1.Name = "Communication";
                    CType = "Communication";
                }
                else if (iRow == 4)
                {
                    Sheet1.Name = "Competency";
                    CType = "Competency";
                }

                //Assign Column Name
                //for (int i = 0; i <= 3; i++) // as 4 fixed header column name
                //{

                Microsoft.Office.Interop.Excel.Range headerPositive = Sheet1.get_Range(Sheet1.Cells[1, 1], Sheet1.Cells[1, titleMonthYear + dtPeriod.Rows.Count]);
                headerPositive.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                headerPositive.Font.Bold = true;
                headerPositive.Merge(Type.Missing);      //Center the title horizontally then vertically at the above defined range     
                headerPositive.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                headerPositive.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                headerPositive.RowHeight = 35;
                headerPositive.EntireRow.AutoFit();
                headerPositive.EntireColumn.AutoFit();
                headerPositive.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

                int negativeStarting = dsMovementReport.Tables[0].Rows.Count + 10;

                Microsoft.Office.Interop.Excel.Range headerNegative = Sheet1.get_Range(Sheet1.Cells[negativeStarting, 1], Sheet1.Cells[negativeStarting, titleMonthYear + dtPeriod.Rows.Count]);
                headerNegative.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                headerNegative.Font.Bold = true;
                headerNegative.Merge(Type.Missing);      //Center the title horizontally then vertically at the above defined range     
                headerNegative.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                headerNegative.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                headerNegative.RowHeight = 35;
                headerNegative.EntireRow.AutoFit();
                headerNegative.EntireColumn.AutoFit();
                headerNegative.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);



                Sheet1.Cells[1, 1] = "Positive Movement";
                Sheet1.Cells[negativeStarting, 1] = "Negative Movement";


                //Assign Column Name - poitive movement
                Sheet1.Cells[2, 1] = dsMovementReport.Tables[1].Columns["EmployeeName"].ColumnName;
                Sheet1.Cells[2, 2] = dsMovementReport.Tables[1].Columns["EmpCode"].ColumnName;
                Sheet1.Cells[2, 3] = dsMovementReport.Tables[1].Columns["ConfirmationStatus"].ColumnName;
                Sheet1.Cells[2, 4] = dsMovementReport.Tables[1].Columns["Designation"].ColumnName;
                Sheet1.Cells[2, 5] = dsMovementReport.Tables[1].Columns["Department"].ColumnName;
                Sheet1.Cells[2, 6] = dsMovementReport.Tables[1].Columns["ProjectName"].ColumnName;

                //Negative movement 
                Sheet1.Cells[negativeStarting + 1, 1] = dsMovementReport.Tables[3].Columns["EmployeeName"].ColumnName;
                Sheet1.Cells[negativeStarting + 1, 2] = dsMovementReport.Tables[3].Columns["EmpCode"].ColumnName;
                Sheet1.Cells[negativeStarting + 1, 3] = dsMovementReport.Tables[3].Columns["ConfirmationStatus"].ColumnName;
                Sheet1.Cells[negativeStarting + 1, 4] = dsMovementReport.Tables[3].Columns["Designation"].ColumnName;
                Sheet1.Cells[negativeStarting + 1, 5] = dsMovementReport.Tables[3].Columns["Department"].ColumnName;
                Sheet1.Cells[negativeStarting + 1, 6] = dsMovementReport.Tables[3].Columns["ProjectName"].ColumnName;

                //Microsoft.Office.Interop.Excel.Range headerDataRange = Sheet1.get_Range(Sheet1.Cells[2, 1], Sheet1.Cells[2, 4]);
                Microsoft.Office.Interop.Excel.Range headerDataRange = Sheet1.get_Range(Sheet1.Cells[2, 1], Sheet1.Cells[2, 6]);
                headerDataRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                headerDataRange.Font.Bold = true;
                headerDataRange.EntireRow.AutoFit();
                headerDataRange.EntireColumn.AutoFit();
                headerDataRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(132, 91, 173));

                //Microsoft.Office.Interop.Excel.Range headerDataRangeNegative = Sheet1.get_Range(Sheet1.Cells[negativeStarting + 1, 1], Sheet1.Cells[negativeStarting + 1, 4]);
                Microsoft.Office.Interop.Excel.Range headerDataRangeNegative = Sheet1.get_Range(Sheet1.Cells[negativeStarting + 1, 1], Sheet1.Cells[negativeStarting + 1, 6]);
                headerDataRangeNegative.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                headerDataRangeNegative.Font.Bold = true;
                headerDataRangeNegative.EntireRow.AutoFit();
                headerDataRangeNegative.EntireColumn.AutoFit();
                headerDataRangeNegative.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(132, 91, 173));

                int compVariableEmpDataPositive = 0;
                int commVariableEmpDataPositive = 0;
                int commitmentVariableEmpDataPositive = 0;
                int collaVariableEmpDataPositive = 0;
                int compVariableEmpDataNegative = 0;
                int commVariableEmpDataNegative = 0;
                int commitmentVariableEmpDataNegative = 0;
                int collaVariableEmpDataNegative = 0;

                //Assign Data -- positive
                for (int i = 0; i < dsMovementReport.Tables[0].Rows.Count; i++)
                {
                    string monthYr = dtPeriod.Rows[dtPeriod.Rows.Count - 1]["MonthYear"].ToString();

                    if (dsMovementReport.Tables[0].Rows[i]["CType"].ToString() == CType)
                    {

                        int empId = int.Parse(dsMovementReport.Tables[0].Rows[i]["EmpId"].ToString());
                        int empDept = int.Parse(dsMovementReport.Tables[0].Rows[i]["DepartmentId"].ToString());
                        int projectId = 0;

                        if (!string.IsNullOrEmpty(dsMovementReport.Tables[0].Rows[i]["ProjectId"].ToString()))
                        {
                            projectId = int.Parse(dsMovementReport.Tables[0].Rows[i]["ProjectId"].ToString());
                        }


                        DataRow[] result = null;
                        if (projectId > 0)
                        {
                            result = dsMovementReport.Tables[1].Select("EmpId = '" + empId + "' and ProjectId = '" + projectId + "' and MonthYear = '" + monthYr + "' and CType = '" + CType + "'");
                        }
                        else
                        {
                            result = dsMovementReport.Tables[1].Select("EmpId = '" + empId + "' and ProjectId = '" + projectId + "' and MonthYear = '" + monthYr + "' and CType = '" + CType + "'");
                        }


                        //if no data then add blank row
                        if (result.Any() == false)
                        {
                            if (CType == "Competency")
                            {
                                compVariableEmpDataPositive = compVariableEmpDataPositive + 1;
                            }
                            else if (CType == "Communication")
                            {
                                commVariableEmpDataPositive = commVariableEmpDataPositive + 1;
                            }
                            else if (CType == "Commitment")
                            {
                                commitmentVariableEmpDataPositive = commitmentVariableEmpDataPositive + 1;
                            }
                            else if (CType == "Collaboration")
                            {
                                collaVariableEmpDataPositive = collaVariableEmpDataPositive + 1;
                            }
                        }

                        foreach (DataRow drEmp in result)
                        {
                            //Sheet1.Cells[i + 3, 1] = drEmp["EmployeeName"].ToString();
                            //Sheet1.Cells[i + 3, 2] = drEmp["Designation"].ToString();
                            //Sheet1.Cells[i + 3, 3] = drEmp["Department"].ToString();
                            //Sheet1.Cells[i + 3, 4] = drEmp["ProjectName"].ToString();

                            if (CType == "Competency")
                            {
                                Sheet1.Cells[compVariableEmpDataPositive + 3, 1] = drEmp["EmployeeName"].ToString();
                                Sheet1.Cells[compVariableEmpDataPositive + 3, 2] = drEmp["EmpCode"].ToString();
                                Sheet1.Cells[compVariableEmpDataPositive + 3, 3] = drEmp["ConfirmationStatus"].ToString();
                                Sheet1.Cells[compVariableEmpDataPositive + 3, 4] = drEmp["Designation"].ToString();
                                Sheet1.Cells[compVariableEmpDataPositive + 3, 5] = drEmp["Department"].ToString();
                                Sheet1.Cells[compVariableEmpDataPositive + 3, 6] = drEmp["ProjectName"].ToString();

                                compVariableEmpDataPositive = compVariableEmpDataPositive + 1;
                            }
                            else if (CType == "Communication")
                            {
                                Sheet1.Cells[commVariableEmpDataPositive + 3, 1] = drEmp["EmployeeName"].ToString();
                                Sheet1.Cells[commVariableEmpDataPositive + 3, 2] = drEmp["EmpCode"].ToString();
                                Sheet1.Cells[commVariableEmpDataPositive + 3, 3] = drEmp["ConfirmationStatus"].ToString();
                                Sheet1.Cells[commVariableEmpDataPositive + 3, 4] = drEmp["Designation"].ToString();
                                Sheet1.Cells[commVariableEmpDataPositive + 3, 5] = drEmp["Department"].ToString();
                                Sheet1.Cells[commVariableEmpDataPositive + 3, 6] = drEmp["ProjectName"].ToString();

                                commVariableEmpDataPositive = commVariableEmpDataPositive + 1;
                            }
                            else if (CType == "Commitment")
                            {
                                Sheet1.Cells[commitmentVariableEmpDataPositive + 3, 1] = drEmp["EmployeeName"].ToString();
                                Sheet1.Cells[commitmentVariableEmpDataPositive + 3, 2] = drEmp["EmpCode"].ToString();
                                Sheet1.Cells[commitmentVariableEmpDataPositive + 3, 3] = drEmp["ConfirmationStatus"].ToString();
                                Sheet1.Cells[commitmentVariableEmpDataPositive + 3, 4] = drEmp["Designation"].ToString();
                                Sheet1.Cells[commitmentVariableEmpDataPositive + 3, 5] = drEmp["Department"].ToString();
                                Sheet1.Cells[commitmentVariableEmpDataPositive + 3, 6] = drEmp["ProjectName"].ToString();

                                commitmentVariableEmpDataPositive = commitmentVariableEmpDataPositive + 1;
                            }
                            else if (CType == "Collaboration")
                            {
                                Sheet1.Cells[collaVariableEmpDataPositive + 3, 1] = drEmp["EmployeeName"].ToString();
                                Sheet1.Cells[collaVariableEmpDataPositive + 3, 2] = drEmp["EmpCode"].ToString();
                                Sheet1.Cells[collaVariableEmpDataPositive + 3, 3] = drEmp["ConfirmationStatus"].ToString();
                                Sheet1.Cells[collaVariableEmpDataPositive + 3, 4] = drEmp["Designation"].ToString();
                                Sheet1.Cells[collaVariableEmpDataPositive + 3, 5] = drEmp["Department"].ToString();
                                Sheet1.Cells[collaVariableEmpDataPositive + 3, 6] = drEmp["ProjectName"].ToString();

                                collaVariableEmpDataPositive = collaVariableEmpDataPositive + 1;
                            }

                        }
                    }
                }

                // Assign Data Negative
                for (int i = 0; i < dsMovementReport.Tables[2].Rows.Count; i++)
                {
                    string monthYr = dtPeriod.Rows[dtPeriod.Rows.Count - 1]["MonthYear"].ToString();

                    if (dsMovementReport.Tables[2].Rows[i]["CType"].ToString() == CType)
                    {

                        int empId = int.Parse(dsMovementReport.Tables[2].Rows[i]["EmpId"].ToString());
                        int empDept = int.Parse(dsMovementReport.Tables[2].Rows[i]["DepartmentId"].ToString());
                        int projectId = 0;

                        if (!string.IsNullOrEmpty(dsMovementReport.Tables[2].Rows[i]["ProjectId"].ToString()))
                        {
                            projectId = int.Parse(dsMovementReport.Tables[2].Rows[i]["ProjectId"].ToString());
                        }


                        DataRow[] result = null;
                        if (projectId > 0)
                        {
                            result = dsMovementReport.Tables[3].Select("EmpId = '" + empId + "' and ProjectId = '" + projectId + "' and MonthYear = '" + monthYr + "' and CType = '" + CType + "'");
                        }
                        else
                        {
                            result = dsMovementReport.Tables[3].Select("EmpId = '" + empId + "' and ProjectId = '" + projectId + "' and MonthYear = '" + monthYr + "' and CType = '" + CType + "'");
                        }


                        //if no data then add blank row
                        if (result.Any() == false)
                        {
                            if (CType == "Competency")
                            {
                                compVariableEmpDataNegative = compVariableEmpDataNegative + 1;
                            }
                            else if (CType == "Communication")
                            {
                                commVariableEmpDataNegative = commVariableEmpDataNegative + 1;
                            }
                            else if (CType == "Commitment")
                            {
                                commitmentVariableEmpDataNegative = commitmentVariableEmpDataNegative + 1;
                            }
                            else if (CType == "Collaboration")
                            {
                                collaVariableEmpDataNegative = collaVariableEmpDataNegative + 1;
                            }
                        }


                        foreach (DataRow drEmp in result)
                        {
                            //Sheet1.Cells[negativeStarting + i + 2, 1] = drEmp["EmployeeName"].ToString();
                            //Sheet1.Cells[negativeStarting + i + 2, 2] = drEmp["Designation"].ToString();
                            //Sheet1.Cells[negativeStarting + i + 2, 3] = drEmp["Department"].ToString();
                            //Sheet1.Cells[negativeStarting + i + 2, 4] = drEmp["ProjectName"].ToString();

                            if (CType == "Competency")
                            {
                                Sheet1.Cells[negativeStarting + compVariableEmpDataNegative + 2, 1] = drEmp["EmployeeName"].ToString();
                                Sheet1.Cells[negativeStarting + compVariableEmpDataNegative + 2, 2] = drEmp["EmpCode"].ToString();
                                Sheet1.Cells[negativeStarting + compVariableEmpDataNegative + 2, 3] = drEmp["ConfirmationStatus"].ToString();
                                Sheet1.Cells[negativeStarting + compVariableEmpDataNegative + 2, 4] = drEmp["Designation"].ToString();
                                Sheet1.Cells[negativeStarting + compVariableEmpDataNegative + 2, 5] = drEmp["Department"].ToString();
                                Sheet1.Cells[negativeStarting + compVariableEmpDataNegative + 2, 6] = drEmp["ProjectName"].ToString();

                                compVariableEmpDataNegative = compVariableEmpDataNegative + 1;
                            }
                            else if (CType == "Communication")
                            {
                                Sheet1.Cells[negativeStarting + commVariableEmpDataNegative + 2, 1] = drEmp["EmployeeName"].ToString();
                                Sheet1.Cells[negativeStarting + commVariableEmpDataNegative + 2, 2] = drEmp["EmpCode"].ToString();
                                Sheet1.Cells[negativeStarting + commVariableEmpDataNegative + 2, 3] = drEmp["ConfirmationStatus"].ToString();
                                Sheet1.Cells[negativeStarting + commVariableEmpDataNegative + 2, 4] = drEmp["Designation"].ToString();
                                Sheet1.Cells[negativeStarting + commVariableEmpDataNegative + 2, 5] = drEmp["Department"].ToString();
                                Sheet1.Cells[negativeStarting + commVariableEmpDataNegative + 2, 6] = drEmp["ProjectName"].ToString();

                                commVariableEmpDataNegative = commVariableEmpDataNegative + 1;
                            }
                            else if (CType == "Commitment")
                            {
                                Sheet1.Cells[negativeStarting + commitmentVariableEmpDataNegative + 2, 1] = drEmp["EmployeeName"].ToString();
                                Sheet1.Cells[negativeStarting + commitmentVariableEmpDataNegative + 2, 2] = drEmp["EmpCode"].ToString();
                                Sheet1.Cells[negativeStarting + commitmentVariableEmpDataNegative + 2, 3] = drEmp["ConfirmationStatus"].ToString();
                                Sheet1.Cells[negativeStarting + commitmentVariableEmpDataNegative + 2, 4] = drEmp["Designation"].ToString();
                                Sheet1.Cells[negativeStarting + commitmentVariableEmpDataNegative + 2, 5] = drEmp["Department"].ToString();
                                Sheet1.Cells[negativeStarting + commitmentVariableEmpDataNegative + 2, 6] = drEmp["ProjectName"].ToString();

                                commitmentVariableEmpDataNegative = commitmentVariableEmpDataNegative + 1;
                            }
                            else if (CType == "Collaboration")
                            {
                                Sheet1.Cells[negativeStarting + collaVariableEmpDataNegative + 2, 1] = drEmp["EmployeeName"].ToString();
                                Sheet1.Cells[negativeStarting + collaVariableEmpDataNegative + 2, 2] = drEmp["EmpCode"].ToString();
                                Sheet1.Cells[negativeStarting + collaVariableEmpDataNegative + 2, 3] = drEmp["ConfirmationStatus"].ToString();
                                Sheet1.Cells[negativeStarting + collaVariableEmpDataNegative + 2, 4] = drEmp["Designation"].ToString();
                                Sheet1.Cells[negativeStarting + collaVariableEmpDataNegative + 2, 5] = drEmp["Department"].ToString();
                                Sheet1.Cells[negativeStarting + collaVariableEmpDataNegative + 2, 6] = drEmp["ProjectName"].ToString();

                                collaVariableEmpDataNegative = collaVariableEmpDataNegative + 1;
                            }

                        }
                    }
                }


                //}

                //Loop over period
                for (int iRowP = 1; iRowP <= dtPeriod.Rows.Count; iRowP++)
                {
                    string monthYr = dtPeriod.Rows[iRowP - 1]["MonthYear"].ToString();

                    Microsoft.Office.Interop.Excel.Range headerSecond = Sheet1.get_Range(Sheet1.Cells[2, titleMonthYear + iRowP], Sheet1.Cells[2, titleMonthYear + iRowP]);
                    headerSecond.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    headerSecond.Font.Bold = true;
                    headerSecond.EntireRow.AutoFit();
                    headerSecond.EntireColumn.AutoFit();
                    headerSecond.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(132, 91, 173));

                    Microsoft.Office.Interop.Excel.Range headerSecondNegative = Sheet1.get_Range(Sheet1.Cells[negativeStarting + 1, titleMonthYear + iRowP], Sheet1.Cells[negativeStarting + 1, titleMonthYear + iRowP]);
                    headerSecondNegative.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    headerSecondNegative.Font.Bold = true;
                    headerSecondNegative.EntireRow.AutoFit();
                    headerSecondNegative.EntireColumn.AutoFit();
                    headerSecondNegative.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(132, 91, 173));

                    Sheet1.Cells[2, titleMonthYear + iRowP] = monthYr.ToString();
                    Sheet1.Cells[negativeStarting + 1, titleMonthYear + iRowP] = monthYr.ToString();


                    int compVariablePos = 0;
                    int commVariablepos = 0;
                    int commitVariablePos = 0;
                    int collVariablePos = 0;
                    int compVariableNeg = 0;
                    int commVariableNeg = 0;
                    int commitVariableNeg = 0;
                    int collVariableNeg = 0;


                    // Positive Action
                    for (int i = 0; i <= dsMovementReport.Tables[0].Rows.Count - 1; i++)
                    {
                        if (dsMovementReport.Tables[0].Rows[i]["CType"].ToString() == CType)
                        {

                            int empId = int.Parse(dsMovementReport.Tables[0].Rows[i]["EmpId"].ToString());
                            int empDept = int.Parse(dsMovementReport.Tables[0].Rows[i]["DepartmentId"].ToString());
                            int projectId = 0;

                            if (!string.IsNullOrEmpty(dsMovementReport.Tables[0].Rows[i]["ProjectId"].ToString()))
                            {
                                projectId = int.Parse(dsMovementReport.Tables[0].Rows[i]["ProjectId"].ToString());
                            }

                            DataRow[] result = null;
                            if (projectId > 0)
                            {
                                result = dsMovementReport.Tables[1].Select("EmpId = '" + empId + "' and ProjectId = '" + projectId + "' and MonthYear = '" + monthYr + "' and CType = '" + CType + "'");
                            }
                            else
                            {
                                result = dsMovementReport.Tables[1].Select("EmpId = '" + empId + "' and ProjectId = '" + projectId + "' and MonthYear = '" + monthYr + "' and CType = '" + CType + "'");
                            }

                            //if no data then add blank row
                            if (result.Any() == false)
                            {
                                if (CType == "Competency")
                                {
                                    compVariablePos = compVariablePos + 1;
                                }
                                else if (CType == "Communication")
                                {
                                    commVariablepos = commVariablepos + 1;
                                }
                                else if (CType == "Commitment")
                                {
                                    commitVariablePos = commitVariablePos + 1;
                                }
                                else if (CType == "Collaboration")
                                {
                                    collVariablePos = collVariablePos + 1;
                                }
                            }

                            foreach (DataRow dr in result)
                            {
                                //Sheet1.Cells[i + 3, titleMonthYear] = dr["ProjectName"].ToString();
                                //Sheet1.Cells[i + 1, titleMonthYear + 1] = dr["Manager Name"].ToString();

                                if (CType == "Competency")
                                {
                                    //Sheet1.Cells[i + 3, titleMonthYear + iRowP] = dr["Competency"].ToString();
                                    //Microsoft.Office.Interop.Excel.Range rowRangeCompetency = Sheet1.get_Range(Sheet1.Cells[i + 3, titleMonthYear + iRowP], Sheet1.Cells[i + 3, titleMonthYear + iRowP]);

                                    Sheet1.Cells[compVariablePos + 3, titleMonthYear + iRowP] = dr["Competency"].ToString();
                                    Microsoft.Office.Interop.Excel.Range rowRangeCompetency = Sheet1.get_Range(Sheet1.Cells[compVariablePos + 3, titleMonthYear + iRowP], Sheet1.Cells[compVariablePos + 3, titleMonthYear + iRowP]);
                                    if (dr["Competency"].ToString().Trim() == "Green")
                                    {
                                        rowRangeCompetency.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
                                    }
                                    else if (dr["Competency"].ToString().Trim() == "Red")
                                    {
                                        rowRangeCompetency.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                    }
                                    else if (dr["Competency"].ToString().Trim() == "Amber")
                                    {
                                        //rowRangeCompetency.Interior.Color = System.Drawing.Color.FromArgb(255,204,102).ToArgb();
                                        rowRangeCompetency.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 204, 0));
                                    }

                                    rowRangeCompetency.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                                             Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
                                    compVariablePos = compVariablePos + 1;
                                }
                                else if (CType == "Communication")
                                {
                                    //    Sheet1.Cells[i + 3, titleMonthYear + iRowP] = dr["Communication"].ToString();
                                    //    Microsoft.Office.Interop.Excel.Range rowRangeCommunication = Sheet1.get_Range(Sheet1.Cells[i + 3, titleMonthYear + iRowP], Sheet1.Cells[i + 3, titleMonthYear + iRowP]);
                                    Sheet1.Cells[commVariablepos + 3, titleMonthYear + iRowP] = dr["Communication"].ToString();
                                    Microsoft.Office.Interop.Excel.Range rowRangeCommunication = Sheet1.get_Range(Sheet1.Cells[commVariablepos + 3, titleMonthYear + iRowP], Sheet1.Cells[commVariablepos + 3, titleMonthYear + iRowP]);
                                    if (dr["Communication"].ToString().Trim() == "Green")
                                    {
                                        rowRangeCommunication.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
                                    }
                                    else if (dr["Communication"].ToString().Trim() == "Red")
                                    {
                                        rowRangeCommunication.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                    }
                                    else if (dr["Communication"].ToString().Trim() == "Amber")
                                    {
                                        //rowRangeCommunication.Interior.Color = System.Drawing.Color.FromArgb(255, 204, 102).ToArgb();
                                        rowRangeCommunication.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 204, 0));
                                    }

                                    rowRangeCommunication.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                                             Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
                                    commVariablepos = commVariablepos + 1;

                                }
                                else if (CType == "Commitment")
                                {

                                    //Sheet1.Cells[i + 3, titleMonthYear + iRowP] = dr["Commitment"].ToString();
                                    //Microsoft.Office.Interop.Excel.Range rowRangeCommitment = Sheet1.get_Range(Sheet1.Cells[i + 3, titleMonthYear + iRowP], Sheet1.Cells[i + 3, titleMonthYear + iRowP]);
                                    Sheet1.Cells[commitVariablePos + 3, titleMonthYear + iRowP] = dr["Commitment"].ToString();
                                    Microsoft.Office.Interop.Excel.Range rowRangeCommitment = Sheet1.get_Range(Sheet1.Cells[commitVariablePos + 3, titleMonthYear + iRowP], Sheet1.Cells[commitVariablePos + 3, titleMonthYear + iRowP]);

                                    if (dr["Commitment"].ToString().Trim() == "Green")
                                    {
                                        rowRangeCommitment.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
                                    }
                                    else if (dr["Commitment"].ToString().Trim() == "Red")
                                    {
                                        rowRangeCommitment.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                    }
                                    else if (dr["Commitment"].ToString().Trim() == "Amber")
                                    {
                                        //rowRangeCommitment.Interior.Color = System.Drawing.Color.FromArgb(255, 204, 0).ToArgb();
                                        rowRangeCommitment.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 204, 0));

                                    }
                                    rowRangeCommitment.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                                             Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);

                                    commitVariablePos = commitVariablePos + 1;
                                }
                                else if (CType == "Collaboration")
                                {
                                    //Sheet1.Cells[i + 3, titleMonthYear + iRowP] = dr["Collaboration"].ToString();
                                    //Microsoft.Office.Interop.Excel.Range rowRangeCollaboration = Sheet1.get_Range(Sheet1.Cells[i + 3, titleMonthYear + iRowP], Sheet1.Cells[i + 3, titleMonthYear + iRowP]);
                                    Sheet1.Cells[collVariablePos + 3, titleMonthYear + iRowP] = dr["Collaboration"].ToString();
                                    Microsoft.Office.Interop.Excel.Range rowRangeCollaboration = Sheet1.get_Range(Sheet1.Cells[collVariablePos + 3, titleMonthYear + iRowP], Sheet1.Cells[collVariablePos + 3, titleMonthYear + iRowP]);

                                    if (dr["Collaboration"].ToString().Trim() == "Green")
                                    {
                                        rowRangeCollaboration.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
                                    }
                                    else if (dr["Collaboration"].ToString().Trim() == "Red")
                                    {
                                        rowRangeCollaboration.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                    }
                                    else if (dr["Collaboration"].ToString().Trim() == "Amber")
                                    {
                                        //rowRangeCollaboration.Interior.Color = System.Drawing.Color.FromArgb(255, 204, 102).ToArgb();
                                        rowRangeCollaboration.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 204, 0));

                                    }
                                    rowRangeCollaboration.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                                             Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
                                    collVariablePos = collVariablePos + 1;

                                }

                            }
                        }




                    }

                    //Microsoft.Office.Interop.Excel.Range positiveBorder = Sheet1.get_Range(Sheet1.Cells[1, 1], Sheet1.Cells[dsMovementReport.Tables[0].Rows.Count + 2, period + 4]); //2 header
                    Microsoft.Office.Interop.Excel.Range positiveBorder = Sheet1.get_Range(Sheet1.Cells[1, 1], Sheet1.Cells[dsMovementReport.Tables[0].Rows.Count + 2, period + 6]); //2 header
                    positiveBorder.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.Color.Black.ToArgb();
                    positiveBorder.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].Color = System.Drawing.Color.Black.ToArgb();
                    positiveBorder.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].Color = System.Drawing.Color.Black.ToArgb();
                    positiveBorder.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.Color.Black.ToArgb();
                    positiveBorder.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].Color = System.Drawing.Color.Black.ToArgb();
                    positiveBorder.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].Color = System.Drawing.Color.Black.ToArgb();

                    positiveBorder.EntireColumn.AutoFit();
                    positiveBorder.EntireRow.AutoFit();


                    //Negative movement
                    for (int i = 0; i <= dsMovementReport.Tables[2].Rows.Count - 1; i++)
                    {
                        if (dsMovementReport.Tables[2].Rows[i]["CType"].ToString() == CType)
                        {

                            int empId = int.Parse(dsMovementReport.Tables[2].Rows[i]["EmpId"].ToString());
                            int empDept = int.Parse(dsMovementReport.Tables[2].Rows[i]["DepartmentId"].ToString());
                            int projectId = 0;

                            if (!string.IsNullOrEmpty(dsMovementReport.Tables[2].Rows[i]["ProjectId"].ToString()))
                            {
                                projectId = int.Parse(dsMovementReport.Tables[2].Rows[i]["ProjectId"].ToString());
                            }

                            DataRow[] result = null;
                            if (projectId > 0)
                            {
                                result = dsMovementReport.Tables[3].Select("EmpId = '" + empId + "' and ProjectId = '" + projectId + "' and MonthYear = '" + monthYr + "' and CType = '" + CType + "'");
                            }
                            else
                            {
                                result = dsMovementReport.Tables[3].Select("EmpId = '" + empId + "' and ProjectId = '" + projectId + "' and MonthYear = '" + monthYr + "' and CType = '" + CType + "'");
                            }

                            //if no data then add blank row
                            if (result.Any() == false)
                            {
                                if (CType == "Competency")
                                {
                                    compVariableNeg = compVariableNeg + 1;
                                }
                                else if (CType == "Communication")
                                {
                                    commVariableNeg = commVariableNeg + 1;
                                }
                                else if (CType == "Commitment")
                                {
                                    commitVariableNeg = commitVariableNeg + 1;
                                }
                                else if (CType == "Collaboration")
                                {
                                    collVariableNeg = collVariableNeg + 1;
                                }
                            }

                            foreach (DataRow dr in result)
                            {
                                //Sheet1.Cells[negativeStarting + i + 2, titleMonthYear] = dr["ProjectName"].ToString();
                                //Sheet1.Cells[i + 1, titleMonthYear + 1] = dr["Manager Name"].ToString();

                                if (CType == "Competency")
                                {
                                    //Sheet1.Cells[negativeStarting + i + 2, titleMonthYear + iRowP] = dr["Competency"].ToString();
                                    //Microsoft.Office.Interop.Excel.Range rowRangeCompetency = Sheet1.get_Range(Sheet1.Cells[negativeStarting + i + 2, titleMonthYear + iRowP], Sheet1.Cells[negativeStarting + i + 2, titleMonthYear + iRowP]);
                                    Sheet1.Cells[negativeStarting + compVariableNeg + 2, titleMonthYear + iRowP] = dr["Competency"].ToString();
                                    Microsoft.Office.Interop.Excel.Range rowRangeCompetency = Sheet1.get_Range(Sheet1.Cells[negativeStarting + compVariableNeg + 2, titleMonthYear + iRowP], Sheet1.Cells[negativeStarting + compVariableNeg + 2, titleMonthYear + iRowP]);
                                    if (dr["Competency"].ToString().Trim() == "Green")
                                    {
                                        rowRangeCompetency.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
                                    }
                                    else if (dr["Competency"].ToString().Trim() == "Red")
                                    {
                                        rowRangeCompetency.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                    }
                                    else if (dr["Competency"].ToString().Trim() == "Amber")
                                    {
                                        //rowRangeCompetency.Interior.Color = System.Drawing.Color.FromArgb(255,204,102).ToArgb();
                                        rowRangeCompetency.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 204, 0));
                                    }

                                    rowRangeCompetency.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                                             Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
                                    compVariableNeg = compVariableNeg + 1;

                                }
                                else if (CType == "Communication")
                                {
                                    //Sheet1.Cells[negativeStarting + i + 2, titleMonthYear + iRowP] = dr["Communication"].ToString();
                                    //Microsoft.Office.Interop.Excel.Range rowRangeCommunication = Sheet1.get_Range(Sheet1.Cells[negativeStarting + i + 2, titleMonthYear + iRowP], Sheet1.Cells[negativeStarting + i + 2, titleMonthYear + iRowP]);
                                    Sheet1.Cells[negativeStarting + commVariableNeg + 2, titleMonthYear + iRowP] = dr["Communication"].ToString();
                                    Microsoft.Office.Interop.Excel.Range rowRangeCommunication = Sheet1.get_Range(Sheet1.Cells[negativeStarting + commVariableNeg + 2, titleMonthYear + iRowP], Sheet1.Cells[negativeStarting + commVariableNeg + 2, titleMonthYear + iRowP]);
                                    if (dr["Communication"].ToString().Trim() == "Green")
                                    {
                                        rowRangeCommunication.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
                                    }
                                    else if (dr["Communication"].ToString().Trim() == "Red")
                                    {
                                        rowRangeCommunication.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                    }
                                    else if (dr["Communication"].ToString().Trim() == "Amber")
                                    {
                                        //rowRangeCommunication.Interior.Color = System.Drawing.Color.FromArgb(255, 204, 102).ToArgb();
                                        rowRangeCommunication.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 204, 0));
                                    }

                                    rowRangeCommunication.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                                             Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
                                    commVariableNeg = commVariableNeg + 1;

                                }
                                else if (CType == "Commitment")
                                {

                                    //Sheet1.Cells[negativeStarting + i + 2, titleMonthYear + iRowP] = dr["Commitment"].ToString();
                                    //Microsoft.Office.Interop.Excel.Range rowRangeCommitment = Sheet1.get_Range(Sheet1.Cells[negativeStarting + i + 2, titleMonthYear + iRowP], Sheet1.Cells[negativeStarting + i + 2, titleMonthYear + iRowP]);
                                    Sheet1.Cells[negativeStarting + commitVariableNeg + 2, titleMonthYear + iRowP] = dr["Commitment"].ToString();
                                    Microsoft.Office.Interop.Excel.Range rowRangeCommitment = Sheet1.get_Range(Sheet1.Cells[negativeStarting + commitVariableNeg + 2, titleMonthYear + iRowP], Sheet1.Cells[negativeStarting + commitVariableNeg + 2, titleMonthYear + iRowP]);
                                    if (dr["Commitment"].ToString().Trim() == "Green")
                                    {
                                        rowRangeCommitment.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
                                    }
                                    else if (dr["Commitment"].ToString().Trim() == "Red")
                                    {
                                        rowRangeCommitment.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                    }
                                    else if (dr["Commitment"].ToString().Trim() == "Amber")
                                    {
                                        //rowRangeCommitment.Interior.Color = System.Drawing.Color.FromArgb(255, 204, 0).ToArgb();
                                        rowRangeCommitment.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 204, 0));

                                    }
                                    rowRangeCommitment.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                                             Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
                                    commitVariableNeg = commitVariableNeg + 1;
                                }
                                else if (CType == "Collaboration")
                                {
                                    //Sheet1.Cells[negativeStarting + i + 2, titleMonthYear + iRowP] = dr["Collaboration"].ToString();
                                    //Microsoft.Office.Interop.Excel.Range rowRangeCollaboration = Sheet1.get_Range(Sheet1.Cells[negativeStarting + i + 2, titleMonthYear + iRowP], Sheet1.Cells[negativeStarting + i + 2, titleMonthYear + iRowP]);
                                    Sheet1.Cells[negativeStarting + collVariableNeg + 2, titleMonthYear + iRowP] = dr["Collaboration"].ToString();
                                    Microsoft.Office.Interop.Excel.Range rowRangeCollaboration = Sheet1.get_Range(Sheet1.Cells[negativeStarting + collVariableNeg + 2, titleMonthYear + iRowP], Sheet1.Cells[negativeStarting + collVariableNeg + 2, titleMonthYear + iRowP]);
                                    if (dr["Collaboration"].ToString().Trim() == "Green")
                                    {
                                        rowRangeCollaboration.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);
                                    }
                                    else if (dr["Collaboration"].ToString().Trim() == "Red")
                                    {
                                        rowRangeCollaboration.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                                    }
                                    else if (dr["Collaboration"].ToString().Trim() == "Amber")
                                    {
                                        //rowRangeCollaboration.Interior.Color = System.Drawing.Color.FromArgb(255, 204, 102).ToArgb();
                                        rowRangeCollaboration.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 204, 0));

                                    }
                                    rowRangeCollaboration.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                                             Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);

                                    collVariableNeg = collVariableNeg + 1;
                                }

                            }
                        }
                    }


                    //Microsoft.Office.Interop.Excel.Range negativeBorder = Sheet1.get_Range(Sheet1.Cells[negativeStarting, 1], Sheet1.Cells[negativeStarting + dsMovementReport.Tables[2].Rows.Count + 1, period + 4]); //2 header
                    Microsoft.Office.Interop.Excel.Range negativeBorder = Sheet1.get_Range(Sheet1.Cells[negativeStarting, 1], Sheet1.Cells[negativeStarting + dsMovementReport.Tables[2].Rows.Count + 1, period + 6]); //2 header
                    negativeBorder.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.Color.Black.ToArgb();
                    negativeBorder.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].Color = System.Drawing.Color.Black.ToArgb();
                    negativeBorder.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].Color = System.Drawing.Color.Black.ToArgb();
                    negativeBorder.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.Color.Black.ToArgb();
                    negativeBorder.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].Color = System.Drawing.Color.Black.ToArgb();
                    negativeBorder.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].Color = System.Drawing.Color.Black.ToArgb();

                    negativeBorder.EntireRow.AutoFit();
                    negativeBorder.EntireColumn.AutoFit();

                }


            }



            string fileName = Server.MapPath("~/MovementReport.xls");
            //string fileName = "Consolidated.xls";
            string misValue = null;
            ExcelApp.DisplayAlerts = false; //Supress overwrite request     
            xlWorkBook.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            ExcelApp.Quit();

            releaseObject(Sheet1);
            //releaseObject(Sheet2);
            releaseObject(xlWorkBook);
            releaseObject(ExcelApp);


            String FilePath = Server.MapPath("~/MovementReport.xls");
            //String FilePath = "Consolidated.xls";
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "text/plain";
            response.AddHeader("Content-Disposition", "attachment; filename=MovementReport-" + DateTime.Now.ToShortDateString() + ".xls;");
            response.TransmitFile(FilePath);
            response.Flush();
            response.Close();

            #endregion "Movement"
        }
        else if (lnkCountReport.Font.Bold == true)
        {
            #region "Count"
            //Umesh: Issue 'Modal Popup issue in chrome' Starts
            DataSet dsCountReport = fourCBAL.GetCountReport(int.Parse(ViewState["LoginEmpId"].ToString()), int.Parse(ddlCountReport.SelectedItem.Value));//, ChkIsSupportCount.Checked);
            //Umesh: Issue 'Modal Popup issue in chrome' Ends

            int period = int.Parse(ddlCountReport.SelectedItem.Value);

            DataTable dtPeriod = new DataTable();
            dtPeriod.Columns.Add("Id");
            dtPeriod.Columns.Add("MonthYear");

            for (int iPeriod = period; iPeriod > 0; iPeriod--)
            {

                //DateTime dtDate = DateTime.Now.AddMonths(-iPeriod);
                DateTime dtDate = DateTime.Now.AddMonths(-iPeriod);

                DataRow dr = dtPeriod.NewRow();
                dr["Id"] = iPeriod;
                dr["MonthYear"] = dtDate.Month + "-" + dtDate.Year;
                dtPeriod.Rows.Add(dr);
            }

           
            Microsoft.Office.Interop.Excel.ApplicationClass ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            xlWorkBook = ExcelApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet Sheet1 = null;

            int loopStatus = 3; //1. Active employee // reviewer list // employees whose 4C not filled
            for (int iRow = 1; iRow <= loopStatus; iRow++)
            {
                Sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                string CType = string.Empty;
                if (iRow == 1)
                {
                    Sheet1.Name = "RMS_4C_Employee";
                    CType = "RMS 4C Employee";
                }
                else if (iRow == 2)
                {
                    Sheet1.Name = "4C_ReviewerList";
                    CType = "4C ReviewerList";
                }
                else if (iRow == 3)
                {
                    Sheet1.Name = "4C_Not_Filled";
                    CType = "Employees Whose 4C Not Filled";
                }


                int tblCount = 3; // 0 to 4 for each C
                for (int iRowP = 1; iRowP <= tblCount; iRowP++)
                {

                    if (iRow == 1 || iRow == 2)
                    {
                        for (int i = 0; i < dsCountReport.Tables[tblCount + loopStatus + 1 - iRow].Columns.Count; i++)
                        {
                            Sheet1.Cells[1, i + 1] = dsCountReport.Tables[tblCount + loopStatus + 1 - iRow].Columns[i].ColumnName;

                            ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[1, i + 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                            ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[1, i + 1]).Font.Bold = true;
                            ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[1, i + 1]).EntireColumn.AutoFit();
                            ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[1, i + 1]).EntireRow.AutoFit();
                            ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[1, i + 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(132, 91, 173));
                        }

                        for (int i = 0; i < dsCountReport.Tables[tblCount + loopStatus + 1 - iRow].Rows.Count; i++)
                        {
                            for (int j = 0; j < dsCountReport.Tables[tblCount + loopStatus + 1 - iRow].Columns.Count; j++)
                            {
                                Sheet1.Cells[i + 2, j + 1] = dsCountReport.Tables[tblCount + loopStatus + 1 - iRow].Rows[i][j].ToString();
                            }
                        }

                    }
                    else if (iRow == 3)
                    {
                        for (int i = 0; i < dsCountReport.Tables[tblCount + 1].Columns.Count - 1; i++)
                        {
                            Sheet1.Cells[2, i + 1] = dsCountReport.Tables[tblCount + 1].Columns[i].ColumnName;

                            ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[2, i + 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                            ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[2, i + 1]).Font.Bold = true;
                            ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[2, i + 1]).EntireColumn.AutoFit();
                            ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[2, i + 1]).EntireRow.AutoFit();
                            ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[2, i + 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(132, 91, 173));

                        }

                        int rowCount = 0;

                        for (int iMCount = 1; iMCount <= dtPeriod.Rows.Count; iMCount++)
                        {
                            string monthYr = dtPeriod.Rows[iMCount - 1]["MonthYear"].ToString();

                            Microsoft.Office.Interop.Excel.Range RangeMerge = Sheet1.get_Range(Sheet1.Cells[rowCount + 1, 1], Sheet1.Cells[rowCount + 1, 2]);
                            RangeMerge.Merge(Type.Missing);
                            RangeMerge.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                            RangeMerge.Font.Bold = true;
                            RangeMerge.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                                                Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium,
                                                Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                                                Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
                            RangeMerge.EntireRow.AutoFit();
                            RangeMerge.EntireColumn.AutoFit();
                            RangeMerge.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(132, 91, 173));
                            RangeMerge.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                            RangeMerge.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

                            Sheet1.Cells[rowCount + 1, 1] = monthYr;

                            DataRow[] result = null;
                            result = dsCountReport.Tables[tblCount + 1].Select("MonthYear = '" + monthYr + "'");

                            foreach (DataRow dr in result)
                            {
                                if (iMCount == 1)
                                {
                                    Sheet1.Cells[rowCount + 3, 1] = dr["EmpCode"].ToString();
                                    Sheet1.Cells[rowCount + 3, 2] = dr["EmployeeName"].ToString();
                                }
                                else
                                {
                                    Sheet1.Cells[rowCount + 2, 1] = dr["EmpCode"].ToString();
                                    Sheet1.Cells[rowCount + 2, 2] = dr["EmployeeName"].ToString();
                                }

                                rowCount = rowCount + 1;
                            }
                            rowCount = rowCount + 3;
                        }
                    }
                }
            }


            loopStatus = 4;
            // int titleMonthYear = 4;

            for (int iRow = 1; iRow <= loopStatus; iRow++)
            {
                //   Sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.ThisWorkbook.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                Sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                string CType = string.Empty;
                if (iRow == 1)
                {
                    Sheet1.Name = "Collaboration";
                    CType = "Collaboration";
                }
                else if (iRow == 2)
                {
                    Sheet1.Name = "Commitment";
                    CType = "Commitment";
                }
                else if (iRow == 3)
                {
                    Sheet1.Name = "Communication";
                    CType = "Communication";
                }
                else if (iRow == 4)
                {
                    Sheet1.Name = "Competency";
                    CType = "Competency";
                }

                Microsoft.Office.Interop.Excel.Range titleRange = ExcelApp.get_Range(Sheet1.Cells[1, 1], Sheet1.Cells[1, period + 1]);
                titleRange.Merge(Type.Missing);      //Center the title horizontally then vertically at the above defined range     
                titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                titleRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
                //Increase the font-size of the title     
                titleRange.Font.Size = 12;
                //Make the title bold     
                titleRange.Font.Bold = true;
                //Give the title background color     
                //titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(132, 91, 173));
                titleRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                //Set the title row height     
                titleRange.RowHeight = 35;

                Sheet1.Cells[1, 1] = CType + " Count Report";


                Microsoft.Office.Interop.Excel.Range header = ExcelApp.get_Range(Sheet1.Cells[2, 1], Sheet1.Cells[2, 1]);
                Sheet1.Cells[2, 1] = CType;


                header.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                header.Font.Bold = true;
                header.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                                    Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium,
                                    Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                                    Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
                header.EntireRow.AutoFit();
                header.EntireColumn.AutoFit();
                header.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(132, 91, 173));


                //Loop over period
                for (int iRowP = 1; iRowP <= dtPeriod.Rows.Count; iRowP++)
                {
                    string monthYr = dtPeriod.Rows[iRowP - 1]["MonthYear"].ToString();

                    Microsoft.Office.Interop.Excel.Range headerSecond = Sheet1.get_Range(Sheet1.Cells[2, iRowP + 1], Sheet1.Cells[2, iRowP + 1]);
                    headerSecond.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                    headerSecond.Font.Bold = true;
                    headerSecond.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                                        Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium,
                                        Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                                        Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
                    headerSecond.EntireRow.AutoFit();
                    headerSecond.EntireColumn.AutoFit();
                    headerSecond.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(132, 91, 173));
                    Sheet1.Cells[2, iRowP + 1] = monthYr.ToString();


                    for (int i = 0; i <= 7; i++) // 5 bcz 4 C + 1 N/A + 1 total row + 4C not filled + Active 4C employee
                    {
                        string strColor = "";

                        if (i == 0)
                        {
                            strColor = "Red";
                        }
                        else if (i == 1)
                        {
                            strColor = "Amber";
                        }
                        else if (i == 2)
                        {
                            strColor = "Green";
                        }
                        else if (i == 3)
                        {
                            strColor = "N/A";
                        }
                        else if (i == 4)
                        {
                            strColor = "CNotFilled";
                        }
                        else if (i == 5)
                        {
                            strColor = "Total";
                        }
                        else if (i == 6)
                        {
                            strColor = "TotalTechSupportDeptEmpCount";
                        }

                        DataRow[] result = null;
                        result = dsCountReport.Tables[loopStatus - iRow].Select("MonthYear = '" + monthYr + "' and RatingColor = '" + strColor + "'");

                        if (result.Take(1).Any())
                        {
                            Microsoft.Office.Interop.Excel.Range rowRange = Sheet1.get_Range(Sheet1.Cells[3 + i, 1], Sheet1.Cells[3 + i, 1]);
                            rowRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                            rowRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                                                 Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium,
                                                 Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                                                 Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);


                            rowRange.EntireColumn.AutoFit();
                            rowRange.EntireRow.AutoFit();

                            string valColor = strColor;

                            if (valColor == "CNotFilled")
                            {
                                valColor = "4C Not Filled";
                            }
                            else if (valColor == "TotalTechSupportDeptEmpCount")
                            {
                                valColor = "RMS 4C Emp Count";
                            }

                            Sheet1.Cells[3 + i, 1] = valColor;
                        }

                        foreach (DataRow dr in result)
                        {
                            Sheet1.Cells[3 + i, iRowP + 1] = dr["RatingColorCount"].ToString();

                            Microsoft.Office.Interop.Excel.Range rowDataRange = Sheet1.get_Range(Sheet1.Cells[3 + i, iRowP + 1], Sheet1.Cells[3 + i, iRowP + 1]);

                            rowDataRange.Borders.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                            rowDataRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                                                 Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium,
                                                 Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                                                 Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);


                            rowDataRange.EntireColumn.AutoFit();
                            rowDataRange.EntireRow.AutoFit();
                        }
                    }
                }

                if (period == 1)
                {
                    ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[2, 2]).UseStandardWidth = 30;
                    ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[2, 2]).ColumnWidth = 30;
                }
                ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[2, 1]).UseStandardWidth = 20;
                ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[2, 1]).ColumnWidth = 20;

            }


            


            string fileName = Server.MapPath("~/CountReport.xls");
            //string fileName = "Consolidated.xls";
            string misValue = null;
            ExcelApp.DisplayAlerts = false; //Supress overwrite request     
            xlWorkBook.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            ExcelApp.Quit();

            releaseObject(Sheet1);
            //releaseObject(Sheet2);
            releaseObject(xlWorkBook);
            releaseObject(ExcelApp);


            String FilePath = Server.MapPath("~/CountReport.xls");
            //String FilePath = "Consolidated.xls";
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "text/plain";
            response.AddHeader("Content-Disposition", "attachment; filename=CountReport-" + DateTime.Now.ToShortDateString() + ".xls;");
            response.TransmitFile(FilePath);
            response.Flush();
            response.Close();

            #endregion "Count"
        }
    }

    private void DeleteFile(string fileName)
    {
        if (File.Exists(fileName))
        {
            try
            {
                File.Delete(fileName);
            }
            catch (Exception ex)
            {             //Could not delete the file, wait and try again             
                try
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    File.Delete(fileName);
                }
                catch
                {                 //Could not delete the file still             
                }
            }
        }
    }

    private void releaseObject(object obj)
    {
        try
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            obj = null;
        }
        catch (Exception ex)
        {
            obj = null;
            Response.Write("Exception Occured while releasing object " + ex.ToString());
        }
        finally
        {
            GC.Collect();
        }
    } 
    
}
