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

public partial class _4CEmpDetailsPopup : BaseClass
{
    int fromCurrentDate;
    string strEmpCode;
    private const string CLASS_NAME = "_4CEmpDetailsPopup.aspx";

    string CTypeId = "87";
    string CActionStatusId = "86";
    string OpenStatusId = "1038";
    string CarryForwardId = "1041";

    const string ActionINSERT = "INSERT";
    const string ActionUPDATE = "UPDATE";
    const string ActionDELETE = "DELETE";

    const string ActComeFromPreviousMonth = "PREVIOUSMONTH";

    DataTable dtActionData;
    string MasterName = "MasterName";
    string MasterID = "MasterID";
    //Define the zero as string.
    private string ZERO = "0";
    //Define the select as string.
    private string SELECTONE = "SELECT";
    string UserMailId;
    string UserRaveDomainId;

    FourCFeedback objFeedBack;


    const string Competency = "Competency";
    const string Communication = "Communication";
    const string Commitment = "Commitment";
    const string Collaboration = "Collaboration";


    protected void Page_Load(object sender, EventArgs e)
    {

        HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
        HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();


        //AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
        //UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
        //UserMailId = UserRaveDomainId.Replace("co.in", "com");


        if(!string.IsNullOrEmpty(Request.QueryString["LoginEmailId"].ToString()))
             UserMailId = Request.QueryString["LoginEmailId"].ToString();


        if (!IsPostBack)
        {
            int empId = 0;
            int projectId = 0;
            int departmentId = 0;
            int month = 0;
            int year = 0;
            string fourCRole = "";
            string empName = "";


            //if (!string.IsNullOrEmpty(Request.QueryString["EmpId"].ToString()))
            empId = int.Parse(DecryptQueryString("EmpId").ToString());            
            departmentId = int.Parse(DecryptQueryString("departmentId").ToString());

            //Venkatesh : 4C_Support : 26/2/2014 : Start 
            if (departmentId == 1)         //if (!Utility.IsSupportDept(departmentId))
                projectId = int.Parse(DecryptQueryString("projectId").ToString());
            else
                projectId = 0; 
            //Venkatesh : 4C_Support : 26/2/2014 : End

            month = int.Parse(DecryptQueryString("month").ToString()); 
            year = int.Parse(DecryptQueryString("year").ToString());
            empName = DecryptQueryString("EmpName").ToString();


            //commented Mahendra To view 4C for all employees
            //if(!string.IsNullOrEmpty(Request.QueryString["EmpId"].ToString()))
            //    empId = int.Parse(Request.QueryString["EmpId"].ToString());

            //if(!string.IsNullOrEmpty(Request.QueryString["projectId"].ToString()))
            // projectId = int.Parse(Request.QueryString["projectId"].ToString());

            //if(!string.IsNullOrEmpty(Request.QueryString["departmentId"].ToString()))
            //    departmentId = int.Parse(Request.QueryString["departmentId"].ToString());

            //if(!string.IsNullOrEmpty(Request.QueryString["month"].ToString()))
            //    month = int.Parse(Request.QueryString["month"].ToString());

            //if(!string.IsNullOrEmpty(Request.QueryString["year"].ToString()))
            //    year = int.Parse(Request.QueryString["year"].ToString());

            //if(!string.IsNullOrEmpty(Request.QueryString["EmpName"].ToString()))
            //  empName = Request.QueryString["EmpName"].ToString();
            //commented Mahendra To view 4C for all employees


            //if(!string.IsNullOrEmpty(Request.QueryString["AllowDirectSubmit"]))
            //{
            //    ViewState["AllowDirectSubmit"] = Request.QueryString["AllowDirectSubmit"].ToString();
            //}
            //else
            //{
            //    ViewState["AllowDirectSubmit"] = "";
            //}

            hdEmpId.Value = empId.ToString();
            hdMonth.Value = month.ToString();
            hdProjectId.Value = projectId.ToString();
            hdRole.Value = fourCRole.ToString();
            hdYear.Value = year.ToString();
            ViewState["FourCRole"] = fourCRole;

            //For All employees
            //hdFBId.Value = Request.QueryString["FBID"].ToString();
            hdFBId.Value = DecryptQueryString("FBID").ToString();

            //lblDate.Text = string.Concat(month.ToString(), " ", year.ToString());
            //DateTime dtMonthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));
            DateTime dtMonthYear = DateTime.Parse(string.Concat("01 ", month.ToString(), " ", year.ToString()));
           // Request.QueryString["MyArgs"].ToString();
            lblDate.Text = dtMonthYear.ToString("MMMM") + " " + dtMonthYear.Year;
            //As per month selection add ViewState["ClickCount"] value so that next and previous icon work properly.
            //if (lblDate.Text != DateTime.Now.AddMonths(-1).ToString("MMMM yyyy"))
            //{
            //    for (int iCount = 1; iCount <= 7; iCount++)
            //    {
            //        if (lblDate.Text == DateTime.Now.AddMonths(-iCount).ToString("MMMM yyyy"))
            //        {
            //            ViewState["ClickCount"] = iCount;
            //            break;
            //        }
            //    }
            //}
            //else
            //{
            //    ViewState["ClickCount"] = 1;
            //}



            //if (lblDate.Text == DateTime.Now.AddMonths(-7).ToString("MMMM yyyy"))
            //{
            //    imgPrevious.Enabled = false;
            //}
            ////if (lblDate.Text == DateTime.Now.AddMonths(-1).ToString("MMMM yyyy"))
            //if (lblDate.Text == DateTime.Now.ToString("MMMM yyyy"))
            //{
            //    imgNext.Enabled = false;
            //}

            //fromCurrentDate = 2;



            lblEmpName.Text = empName;
            //FillCType();

            FillDepartment(departmentId);
            fillProjectDropdown(projectId, dtMonthYear.Month, dtMonthYear.Year);
            //lblDate.Text = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Year;
            //lblActionInitiatedDate.Text = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Year;
            //lblActionInitiatedDate1.Text = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Year;
            //lblDate.Text = "March 2012";
            //imgNext.Enabled = false;

          
            ddlProjectList.SelectedValue = hdProjectId.Value;
            
            //BindData();
            //BindActionData(int.Parse(hdProjectId.Value), int.Parse(hdMonth.Value), int.Parse(hdYear.Value), fourCRole);
            BindActionData(int.Parse(hdProjectId.Value), dtMonthYear.Month, dtMonthYear.Year, ViewState["FourCRole"].ToString());

            //make all page read only.
            makeReadOnly(this.Page, false);

        }
        fillDropdownColor();
    }

    private void fillProjectDropdown(int projectId, int month, int year)
    {
        if (projectId != 0)
        {
            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
            //DataSet dsProject = fourCBAL.FillProjectDropdownOnEmp(int.Parse(hdEmpId.Value), int.Parse(hdMonth.Value), int.Parse(hdYear.Value));
            //DataSet dsProject = fourCBAL.FillProjectDropdownOnEmp(int.Parse(hdEmpId.Value), month, year, ViewState["AllowDirectSubmit"].ToString());
            DataSet dsProject = fourCBAL.FillProjectDropdownOnEmp(int.Parse(hdEmpId.Value), month, year);
            ddlProjectList.DataSource = dsProject;
            ddlProjectList.DataValueField = dsProject.Tables[0].Columns[0].ToString();
            ddlProjectList.DataTextField = dsProject.Tables[0].Columns[1].ToString();
            ddlProjectList.DataBind();
            ddlProjectList.Enabled = true;
        }
        else
        {
            ddlProjectList.Enabled = false;
        }
        
        ddlProjectList.Items.Insert(0, new ListItem(SELECTONE, ZERO));
    }

    private void FillDepartment(int departmentId)
    {
        //Declaring COllection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        //Calling Fill dropdown Business layer method to fill 
        //the dropdown from Master class.
        raveHRCollection = master.FillDepartmentDropDownBL();

        ddlDepartment.Items.Clear();
        ddlDepartment.DataSource = raveHRCollection;
        ddlDepartment.DataTextField = Common.CommonConstants.DDL_DataTextField;
        ddlDepartment.DataValueField = Common.CommonConstants.DDL_DataValueField;
        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, new ListItem(CommonConstants.SELECT, CommonConstants.ZERO.ToString()));
        //remove the Dept Name 
        ddlDepartment.Items.Remove(ddlDepartment.Items.FindByValue(CommonConstants.DeptId_RaveDevelopment.ToString()));
        ddlDepartment.Items.Remove(ddlDepartment.Items.FindByText(CommonConstants.PRESALES_USA.ToString()));
        ddlDepartment.Items.Remove(ddlDepartment.Items.FindByText(CommonConstants.PRESALES_UK.ToString()));
        ddlDepartment.Items.Remove(ddlDepartment.Items.FindByText(CommonConstants.PRESALES_INDIA.ToString()));
        ddlDepartment.Items.Remove(ddlDepartment.Items.FindByText(CommonConstants.RAVECONSULTANT_USA.ToString()));
        ddlDepartment.Items.Remove(ddlDepartment.Items.FindByText(CommonConstants.RAVECONSULTANT_UK.ToString()));
        ddlDepartment.Items.Remove(ddlDepartment.Items.FindByText(CommonConstants.RAVEFORCASTEDPROJECT.ToString()));
        ddlDepartment.Items.Remove(ddlDepartment.Items.FindByText(CommonConstants.SALES_DEPARTMENT.ToString()));
        ddlDepartment.Items.Remove(ddlDepartment.Items.FindByText(CommonConstants.Senior_Mgt_DEPARTMENT.ToString()));
        ddlDepartment.Items.Remove(ddlDepartment.Items.FindByText(CommonConstants.Project_Mentee2010_DEPARTMENT.ToString()));

        ddlDepartment.SelectedValue = departmentId.ToString();
        ddlDepartment.Enabled = false;
    }


    private void BindActionData(int projectId, int month, int year, string fourCRole)
    {
        Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();

        DataSet dsEmpDeatils = new DataSet();

        //DataSet dsDetails = fourCBAL.Get4CActionDetails(int.Parse(ddlDepartment.SelectedValue), int.Parse(hdEmpId.Value), projectId, month, year, UserMailId, int.Parse(DecryptQueryString("Mode").ToString()));
        //4C for employees
        //DataSet dsDetails = fourCBAL.Get4CActionDetailsForDashboard(int.Parse(ddlDepartment.SelectedValue), int.Parse(hdEmpId.Value), int.Parse(Request.QueryString["FBID"].ToString()), projectId, month, year, UserMailId, int.Parse(DecryptQueryString("Mode").ToString()));
        DataSet dsDetails = fourCBAL.Get4CActionDetailsForDashboard(int.Parse(ddlDepartment.SelectedValue), int.Parse(hdEmpId.Value), int.Parse(DecryptQueryString("FBID").ToString()), projectId, month, year, UserMailId, int.Parse(DecryptQueryString("Mode").ToString()));
        dtActionData = new DataTable();

        //dtActionData = dsDetails.Tables[0];

        //SET Manager Name and Reviewer Name
        //if (dsDetails != null && dsDetails.Tables[2].Rows.Count > 0)
        //{

        //    if (dsDetails.Tables[2].Rows.Count > 0 && !string.IsNullOrEmpty(dsDetails.Tables[2].Rows[0][0].ToString()))
        //        lblManager.Text = dsDetails.Tables[2].Rows[0][0].ToString();
        //    else
        //        lblManager.Text = "";

        //    if (dsDetails.Tables[2].Rows.Count > 0 && !string.IsNullOrEmpty(dsDetails.Tables[2].Rows[0][1].ToString()))
        //        lblReviewerName.Text = dsDetails.Tables[2].Rows[0][1].ToString();
        //    else
        //        lblReviewerName.Text = "";
        //}


        ////SEt send for review enable or disable
        //if (dsDetails != null && dsDetails.Tables[3].Rows.Count > 0)
        //{
        //    if (!string.IsNullOrEmpty(dsDetails.Tables[3].Rows[0][0].ToString()))
        //    {
        //        hdSendForReviewEnable.Value = dsDetails.Tables[3].Rows[0][0].ToString();
        //    }
        //    else
        //    {
        //        hdSendForReviewEnable.Value = "";
        //    }

        //    if (!string.IsNullOrEmpty(dsDetails.Tables[3].Rows[0][1].ToString()))
        //    {
        //        hdSubmitratingEnable.Value = dsDetails.Tables[3].Rows[0][1].ToString();
        //    }
        //    else
        //    {
        //        hdSubmitratingEnable.Value = "";
        //    }

        //    if (!string.IsNullOrEmpty(dsDetails.Tables[3].Rows[0][2].ToString()))
        //    {
        //        if (dsDetails.Tables[3].Rows[0][2].ToString().Trim() == "TRUE")
        //        {
        //            divFinalRating.Visible = false;
        //            divAddNewRow.Visible = false;
        //            divActionDetails.Visible = false;
        //            divParameters.Visible = false;
        //            //btnSave.Visible = false;
        //            divNoDataFound.Visible = true;
        //        }
        //        else
        //        {
        //            divFinalRating.Visible = true;
        //            divAddNewRow.Visible = true;
        //            divActionDetails.Visible = true;
        //            divParameters.Visible = true;
        //            //btnSave.Visible = true;
        //            divNoDataFound.Visible = false;
        //        }
        //    }

        //}


        //AssignFinalRatingColor(dsDetails);

        //IsAllowToFillActionData(projectId, UserMailId);

        //if (dsDetails.Tables[0].Rows.Count == 0)
        //{
        //    dtActionData.Columns.Add(DbTableColumn.FourCROWId);
        //    dtActionData.Columns.Add(DbTableColumn.FourCActionId);
        //    dtActionData.Columns.Add(DbTableColumn.FourCId);
        //    dtActionData.Columns.Add(DbTableColumn.FourCType);
        //    dtActionData.Columns.Add(DbTableColumn.FourCParameterType);
        //    dtActionData.Columns.Add(DbTableColumn.FourCDescription);
        //    dtActionData.Columns.Add(DbTableColumn.FourCAction);
        //    dtActionData.Columns.Add(DbTableColumn.FourCActionOwner);
        //    dtActionData.Columns.Add(DbTableColumn.FourCActionOwnerId);
        //    dtActionData.Columns.Add(DbTableColumn.FourCActionCreatedDate);
        //    dtActionData.Columns.Add(DbTableColumn.FourCTargetClosureDate);
        //    dtActionData.Columns.Add(DbTableColumn.FourCActualClosureDate);
        //    dtActionData.Columns.Add(DbTableColumn.FourCRemarks);
        //    dtActionData.Columns.Add(DbTableColumn.FourCActionStatus);
        //    dtActionData.Columns.Add(DbTableColumn.ActionDML);
        //    dtActionData.Columns.Add(DbTableColumn.ActionFrom);

        //    dtActionData = AddNewRow(dtActionData);
        //    Session[SessionNames.FOURC_ACTION_DATA] = dtActionData;

        //    grdEmpActionDetails.DataSource = dtActionData;
        //    grdEmpActionDetails.DataBind();

            
        //}
        //else
        //{
            dtActionData = dsDetails.Tables[0];

            //Session[SessionNames.FOURC_ACTION_DATA] = dtActionData;

            grdEmpActionDetails.DataSource = dtActionData;
            grdEmpActionDetails.DataBind();

        //}

        //IsAllowToFillActionData(projectId, UserMailId);
    }


    //private void AssignFinalRatingColor(DataSet dsColor)
    //{
    //    if (dsColor.Tables[1].Rows.Count > 0)
    //    {
    //        if (dsColor.Tables[1].Rows[0][0] != null && !string.IsNullOrEmpty(dsColor.Tables[1].Rows[0][0].ToString()))
    //            ddlRAGCompetency.SelectedValue = dsColor.Tables[1].Rows[0][0].ToString();
    //        else
    //            ddlRAGCompetency.SelectedIndex = 0;


    //        if (dsColor.Tables[1].Rows[0][1] != null && !string.IsNullOrEmpty(dsColor.Tables[1].Rows[0][1].ToString()))
    //            ddlRAGCommunication.SelectedValue = dsColor.Tables[1].Rows[0][1].ToString();
    //        else
    //            ddlRAGCommunication.SelectedIndex = 0;

    //        if (dsColor.Tables[1].Rows[0][2] != null && !string.IsNullOrEmpty(dsColor.Tables[1].Rows[0][2].ToString()))
    //            ddlRAGCommitment.SelectedValue = dsColor.Tables[1].Rows[0][2].ToString();
    //        else
    //            ddlRAGCommitment.SelectedIndex = 0;

    //        if (dsColor.Tables[1].Rows[0][3] != null && !string.IsNullOrEmpty(dsColor.Tables[1].Rows[0][3].ToString()))
    //            ddlRAGCollaboration.SelectedValue = dsColor.Tables[1].Rows[0][3].ToString();
    //        else
    //            ddlRAGCollaboration.SelectedIndex = 0;
    //    }
    //    else
    //    {
    //        ddlRAGCompetency.SelectedIndex = 0;
    //        ddlRAGCommunication.SelectedIndex = 0;
    //        ddlRAGCommitment.SelectedIndex = 0;
    //        ddlRAGCollaboration.SelectedIndex = 0;
    //    }
    //}

   

    private DataTable AddNewRow(DataTable dtActionData)
    {
        DateTime dtMonthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));

        DataRow dr = dtActionData.NewRow();
        dr[DbTableColumn.FourCROWId] = grdEmpActionDetails.Rows.Count;
        dr[DbTableColumn.FourCActionId] = 0;

        if(dtActionData != null && dtActionData.Rows.Count >= 1)
        {
            //var dValue=  from row in dtActionData.Rows[0]["FBID"].ToString(); 
            //             where row.Field("ROWID") == 1
            //             select row.Field("D"); 
            
            hdCurrentFBId.Value = dtActionData.Rows[0]["FBID"].ToString(); 
                //dtActionData.Select("FBID = 'INSERT' ")
        }

        if (!string.IsNullOrEmpty(hdFBId.Value) && (int.Parse(hdMonth.Value) == dtMonthYear.Month))
        {
            dr[DbTableColumn.FourCId] = int.Parse(hdFBId.Value);
        }
        else if (!string.IsNullOrEmpty(hdCurrentFBId.Value) && hdCurrentFBId.Value != "0")
        {
            dr[DbTableColumn.FourCId] = int.Parse(hdCurrentFBId.Value);
        }
        else
        {
            dr[DbTableColumn.FourCId] = 0;
        }

        dr[DbTableColumn.FourCType] = 0;
        dr[DbTableColumn.FourCParameterType] = 0;
        dr[DbTableColumn.FourCDescription] = "";
        dr[DbTableColumn.FourCAction] = "";
        dr[DbTableColumn.FourCActionOwner] = "";
        dr[DbTableColumn.FourCActionOwnerId] = 0;
        dr[DbTableColumn.FourCActionCreatedDate] = DateTime.Now.ToString("dd MMM yyyy");
        dr[DbTableColumn.FourCTargetClosureDate] = DBNull.Value;
        dr[DbTableColumn.FourCActualClosureDate] = DBNull.Value;
        dr[DbTableColumn.FourCRemarks] = "";
        dr[DbTableColumn.FourCActionStatus] = 1;
        dr[DbTableColumn.ActionDML] = ActionINSERT;
        dr[DbTableColumn.ActionFrom] = "";

        dtActionData.Rows.Add(dr);

        return dtActionData;
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

    private List<BusinessEntities.Master> FillParameter(string CId)
    {
        try
        {
            List<BusinessEntities.Master> objCTypeStatus = new List<BusinessEntities.Master>();

            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
            objCTypeStatus = fourCBAL.GetParameter(CId);
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

    private List<BusinessEntities.Master> FillActionOwner(int projectId)
    {
        try
        {
            List<BusinessEntities.Master> objCTypeStatus = new List<BusinessEntities.Master>();

            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
            objCTypeStatus = fourCBAL.GetActionOwner(projectId);
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


    private void fillDropdownColor()
    {
        // Sanju:Issue Id 50201:Added color on dropdown on pageload if the option is selected
        string competencySelectedValue = ddlRAGCompetency.SelectedItem.Value == "Amber" ? "#FFCC00" : ddlRAGCompetency.SelectedItem.Value;
        string collaborationSelectedValue = ddlRAGCollaboration.SelectedItem.Value == "Amber" ? "#FFCC00" : ddlRAGCollaboration.SelectedItem.Value;
        string communicationSelectedValue = ddlRAGCommunication.SelectedItem.Value == "Amber" ? "#FFCC00" : ddlRAGCommunication.SelectedItem.Value;
        string commitmentSelectedValue = ddlRAGCommitment.SelectedItem.Value == "Amber" ? "#FFCC00" : ddlRAGCommitment.SelectedItem.Value;

        ddlRAGCompetency.Items[1].Attributes.Add("style", "background-color: #008000");
        ddlRAGCompetency.Items[2].Attributes.Add("style", "background-color: #FFCC00");
        ddlRAGCompetency.Items[3].Attributes.Add("style", "background-color: #FF0000");
        ddlRAGCompetency.Attributes.Add("style", "background:" + competencySelectedValue);

        ddlRAGCollaboration.Items[1].Attributes.Add("style", "background-color: #008000");
        ddlRAGCollaboration.Items[2].Attributes.Add("style", "background-color: #FFCC00");
        ddlRAGCollaboration.Items[3].Attributes.Add("style", "background-color: #FF0000");
        ddlRAGCollaboration.Attributes.Add("style", "background:" + collaborationSelectedValue);

        ddlRAGCommunication.Items[1].Attributes.Add("style", "background-color: #008000");
        ddlRAGCommunication.Items[2].Attributes.Add("style", "background-color: #FFCC00");
        ddlRAGCommunication.Items[3].Attributes.Add("style", "background-color: #FF0000");
        ddlRAGCommunication.Attributes.Add("style", "background:" + communicationSelectedValue);

        ddlRAGCommitment.Items[1].Attributes.Add("style", "background-color: #008000");
        ddlRAGCommitment.Items[2].Attributes.Add("style", "background-color: #FFCC00");
        ddlRAGCommitment.Items[3].Attributes.Add("style", "background-color: #FF0000");
        ddlRAGCommitment.Attributes.Add("style", "background:" + commitmentSelectedValue);
        // Sanju:Issue Id 50201:End
    }

    protected void imgPrevious_Click(object sender, ImageClickEventArgs e)
    {

        if (lblDate.Text == DateTime.Now.AddMonths(-6).ToString("MMMM yyyy"))
        {
            imgPrevious.Enabled = false;
        }

        imgNext.Enabled = true;
        ViewState["ClickCount"] = Convert.ToInt32(ViewState["ClickCount"]) + 1;
        fromCurrentDate = Convert.ToInt32(ViewState["ClickCount"]);
        lblDate.Text = DateTime.Now.AddMonths(-fromCurrentDate).ToString("MMMM yyyy");// + " " + DateTime.Now.Year;
        DateTime dtMonthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));
  
        BindActionData(int.Parse(ddlProjectList.SelectedItem.Value), dtMonthYear.Month, dtMonthYear.Year, ViewState["FourCRole"].ToString());

    }
    protected void imgNext_Click(object sender, ImageClickEventArgs e)
    {
        
        if (lblDate.Text == DateTime.Now.ToString("MMMM yyyy"))
        {
            imgNext.Enabled = false;
        }
       
            imgPrevious.Enabled = true;

            ViewState["ClickCount"] = Convert.ToInt32(ViewState["ClickCount"]) - 1;
            fromCurrentDate = Convert.ToInt32(ViewState["ClickCount"]);

            //if(fromCurrentDate != 0)
            if (DateTime.Now.AddMonths(-1).ToString("MMMM yyyy") != DateTime.Now.AddMonths(-fromCurrentDate).ToString("MMMM yyyy"))
                lblDate.Text = DateTime.Now.AddMonths(-fromCurrentDate).ToString("MMMM yyyy");
            else
                lblDate.Text = DateTime.Now.AddMonths(-1).ToString("MMMM yyyy");

            //if (lblDate.Text == DateTime.Now.AddMonths(-1).ToString("MMMM yyyy"))
            if (lblDate.Text == DateTime.Now.ToString("MMMM yyyy"))
            {
                imgNext.Enabled = false;
            }

            //Bind Grid
            DateTime dtMonthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));
            BindActionData(int.Parse(ddlProjectList.SelectedItem.Value), dtMonthYear.Month, dtMonthYear.Year, ViewState["FourCRole"].ToString());
        //}
    }




    protected void grdEmpActionDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblRowId = (Label)e.Row.FindControl("lblRowId");
            HiddenField hdActionId = (HiddenField)e.Row.FindControl("hdActionId");
            DropDownList ddlCType = (DropDownList)e.Row.FindControl("ddlCType");
            DropDownList ddlParameter = (DropDownList)e.Row.FindControl("ddlCParameter");
            //TextBox txtDescription = (TextBox)e.Row.FindControl("txtDescription");
            //TextBox txtAction = (TextBox)e.Row.FindControl("txtAction");
            //TextBox txtActionOwner = (TextBox)e.Row.FindControl("txtActionOwner");
            ////DropDownList ddlActionOwner = (DropDownList)e.Row.FindControl("ddlActionOwner");
            Label lblActionCreatedDate = (Label)e.Row.FindControl("lblActionCreatedDate");
            ////TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");
            //Image imgActionOwner = (Image)e.Row.FindControl("imgActionOwner");

            UIControls_DatePickerControl ucDatePickerTragetClosureDate = (UIControls_DatePickerControl)e.Row.FindControl("ucDatePickerTragetClosureDate");
            UIControls_DatePickerControl ucDatePickerActualClosureDate = (UIControls_DatePickerControl)e.Row.FindControl("ucDatePickerActualClosureDate");

            DropDownList ddlActionStatus = (DropDownList)e.Row.FindControl("ddlActionStatus");
            int grdRow = int.Parse(lblRowId.Text);

            //ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
            //imgDelete.Attributes.Add("onClick", "return func_AskUser()");
            //imgActionOwner.Attributes["onclick"] = "popUpEmployeeSearch('" + e.Row.DataItemIndex + "');";
          
           

            //if (Session[SessionNames.FOURC_ACTION_DATA] != null)
            //{
            //    dtActionData = (DataTable)Session[SessionNames.FOURC_ACTION_DATA];

                LoadCType(sender, e);
       

                LoadActionStatus(sender, e);


                if (!string.IsNullOrEmpty(dtActionData.Rows[grdRow][DbTableColumn.FourCType].ToString()))
                {
                    ddlCType.SelectedValue = dtActionData.Rows[grdRow][DbTableColumn.FourCType].ToString();
                    LoadParameter(ddlCType.SelectedItem.Value, ddlParameter);
                }

                if(!string.IsNullOrEmpty(dtActionData.Rows[grdRow][DbTableColumn.FourCParameterType].ToString()))
                    ddlParameter.SelectedValue = dtActionData.Rows[grdRow][DbTableColumn.FourCParameterType].ToString();

                //if (!string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "ActionComeFrom").ToString()) || (string.IsNullOrEmpty(Request.QueryString["AllowToEdit"].ToString())))
                //{
                //    DateTime dtMonthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));

                //    if (int.Parse(hdMonth.Value) == dtMonthYear.Month)
                //    {
                //        ddlCType.Enabled = false;
                //        ddlParameter.Enabled = false;
                //        txtAction.Enabled = false;
                //        txtDescription.Enabled = false;
                //        ucDatePickerTragetClosureDate.IsEnable = false;
                //    }
                //}
                //else
                //{
                //    ddlCType.Enabled = true;
                //    ddlParameter.Enabled = true;
                //    txtAction.Enabled = true;
                //    txtDescription.Enabled = true;
                //    ucDatePickerTragetClosureDate.IsEnable = true;
                //}

                if (!string.IsNullOrEmpty(dtActionData.Rows[grdRow][DbTableColumn.FourCActionStatus].ToString()))
                {
                    
                    ddlActionStatus.SelectedValue = dtActionData.Rows[grdRow][DbTableColumn.FourCActionStatus].ToString();
                    
                    //if (ddlActionStatus.SelectedItem.Text == "Closed")
                    //{
                    //    ucDatePickerActualClosureDate.IsEnable = true;
                    //}
                }

                ////All carry forward action are in disable mode.
                //if (ddlActionStatus.SelectedValue == CarryForwardId.Trim() || ddlActionStatus.SelectedItem.Text == "Closed" || ddlActionStatus.SelectedItem.Text == "On Hold")
                //{
                //    ddlCType.Enabled = false;
                //    ddlParameter.Enabled = false;
                //    txtAction.Enabled = false;
                //    txtDescription.Enabled = false;
                //    ucDatePickerTragetClosureDate.IsEnable = false;
                //}




                //if(!string.IsNullOrEmpty(dtActionData.Rows[grdRow][DbTableColumn.FourCActionOwnerId].ToString()))
                //    ddlActionOwner.SelectedValue = dtActionData.Rows[grdRow][DbTableColumn.FourCActionOwnerId].ToString();

                DateTime dtTemp;
                if (!string.IsNullOrEmpty(dtActionData.Rows[grdRow][DbTableColumn.FourCActionCreatedDate].ToString()))
                {
                    dtTemp = DateTime.Parse(dtActionData.Rows[grdRow][DbTableColumn.FourCActionCreatedDate].ToString());
                    lblActionCreatedDate.Text = dtTemp.ToString("dd MMM yyyy");
                }

                if (!string.IsNullOrEmpty(dtActionData.Rows[grdRow][DbTableColumn.FourCTargetClosureDate].ToString()))
                {
                    dtTemp = DateTime.Parse(dtActionData.Rows[grdRow][DbTableColumn.FourCTargetClosureDate].ToString());
                    ucDatePickerTragetClosureDate.Text = dtTemp.ToString("dd/MM/yyyy");
                }

                if (!string.IsNullOrEmpty(dtActionData.Rows[grdRow][DbTableColumn.FourCActualClosureDate].ToString()))
                {
                    dtTemp = DateTime.Parse(dtActionData.Rows[grdRow][DbTableColumn.FourCActualClosureDate].ToString());
                    ucDatePickerActualClosureDate.Text = dtTemp.ToString("dd/MM/yyyy");
                }

                if (!string.IsNullOrEmpty(dtActionData.Rows[grdRow][DbTableColumn.FourCActualClosureDate].ToString()))
                {
                    dtTemp = DateTime.Parse(dtActionData.Rows[grdRow][DbTableColumn.FourCActualClosureDate].ToString());
                    ucDatePickerActualClosureDate.Text = dtTemp.ToString("dd/MM/yyyy");
                }

                if (!string.IsNullOrEmpty(dtActionData.Rows[grdRow][DbTableColumn.Competency].ToString()))
                    ddlRAGCompetency.SelectedValue = dtActionData.Rows[grdRow][DbTableColumn.Competency].ToString();
                 else
                     ddlRAGCompetency.SelectedIndex = 0;


                if (!string.IsNullOrEmpty(dtActionData.Rows[grdRow][DbTableColumn.Communication].ToString()))
                    ddlRAGCommunication.SelectedValue = dtActionData.Rows[grdRow][DbTableColumn.Communication].ToString();
                 else
                     ddlRAGCommunication.SelectedIndex = 0;

                if (!string.IsNullOrEmpty(dtActionData.Rows[grdRow][DbTableColumn.Commitment].ToString()))
                    ddlRAGCommitment.SelectedValue = dtActionData.Rows[grdRow][DbTableColumn.Commitment].ToString();
                 else
                     ddlRAGCommitment.SelectedIndex = 0;

                if (!string.IsNullOrEmpty(dtActionData.Rows[grdRow][DbTableColumn.Collaboration].ToString()))
                    ddlRAGCollaboration.SelectedValue = dtActionData.Rows[grdRow][DbTableColumn.Collaboration].ToString();
                 else
                     ddlRAGCollaboration.SelectedIndex = 0;

                if (!string.IsNullOrEmpty(dtActionData.Rows[grdRow][DbTableColumn.Creator].ToString()))
                    lblManager.Text = dtActionData.Rows[grdRow][DbTableColumn.Creator].ToString();

                if (!string.IsNullOrEmpty(dtActionData.Rows[grdRow][DbTableColumn.Reviewer].ToString()))
                    lblReviewerName.Text = dtActionData.Rows[grdRow][DbTableColumn.Reviewer].ToString();

            }

            //If Action present then do not show delete button
            //(btnSave.Enabled == false && ddlActionStatus.SelectedItem.Text != "Open")
            //if ((!string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "FBAID").ToString()) && DataBinder.Eval(e.Row.DataItem, "FBAID").ToString() != "0" && ddlActionStatus.SelectedItem.Text != "Open") || (!string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "ActionComeFrom").ToString())))
            //{
            //    imgDelete.Visible = false;
            //}
            //else
            //{
            //    imgDelete.Visible = true;
            //}


           

            //if ((!string.IsNullOrEmpty(hdSendForReviewEnable.Value) && hdSendForReviewEnable.Value == "False") || (!string.IsNullOrEmpty(hdSubmitratingEnable.Value) && hdSubmitratingEnable.Value == "False"))
            //{
            //    makeReadOnly(this.Page, false);
            //}
        //}

        //Hide serial no column
        e.Row.Cells[0].Visible = false;

        
        
       
    }

    protected void ddlCType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow clickedRow = ((DropDownList)sender).NamingContainer as GridViewRow;
        DropDownList ddlCType = (DropDownList)clickedRow.FindControl("ddlCType");
        DropDownList ddlParameter = (DropDownList)clickedRow.FindControl("ddlCParameter");
        LoadParameter(ddlCType.SelectedItem.Value, ddlParameter);
    }

    protected void ddlActionStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow clickedRow = ((DropDownList)sender).NamingContainer as GridViewRow;
        DropDownList ddlActionStatus = (DropDownList)clickedRow.FindControl("ddlActionStatus");
        //DropDownList ddlCType = (DropDownList)clickedRow.FindControl("ddlCType");
       // DropDownList ddlCParameter = (DropDownList)clickedRow.FindControl("ddlCParameter");

        UIControls_DatePickerControl ucDatePickerActualClosureDate = (UIControls_DatePickerControl)clickedRow.FindControl("ucDatePickerActualClosureDate");

        if (ddlActionStatus.SelectedItem.Text == "Closed")
        {
            ucDatePickerActualClosureDate.IsEnable = true;
        }
        else
        {
            ucDatePickerActualClosureDate.IsEnable = false;
            ucDatePickerActualClosureDate.Text = "";
        }

        
        
    }


    protected void ddlProjectList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProjectList.SelectedIndex > 0)
        {
            //Session[SessionNames.FOURC_ACTION_DATA] = null;
            hdProjectId.Value = ddlProjectList.SelectedItem.Value;
            DateTime dtMonthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));
            BindActionData(int.Parse(hdProjectId.Value), dtMonthYear.Month, dtMonthYear.Year, ViewState["FourCRole"].ToString());
            //IsAllowToFillActionData(int.Parse(hdProjectId.Value), UserMailId);
        }
    }


    //protected void imgDelete_Click(object sender, ImageClickEventArgs e)
    //{
    //    GridViewRow clickedRow = ((ImageButton)sender).NamingContainer as GridViewRow;
    //    HiddenField hdActionId = (HiddenField)clickedRow.FindControl("hdActionId");
    //    Label lblRowId = (Label)clickedRow.FindControl("lblRowId");
    //    int grdRow = int.Parse(lblRowId.Text);

    //    if (grdEmpActionDetails.Rows.Count == 1)
    //    {
    //        Page page = HttpContext.Current.Handler as Page;
    //        ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Cannot delete row.!!!');", true);
    //        return;
    //    }

    //    //if (string.IsNullOrEmpty(hdActionId.Value))
    //    //{
    //        UpdateDataTable();

    //        if (Session[SessionNames.FOURC_ACTION_DATA] != null)
    //        {
    //            dtActionData = (DataTable)Session[SessionNames.FOURC_ACTION_DATA];

    //            dtActionData.Rows.RemoveAt(grdRow);

    //            grdEmpActionDetails.DataSource = dtActionData;
    //            grdEmpActionDetails.DataBind();

    //            Session[SessionNames.FOURC_ACTION_DATA] = dtActionData;
    //        }
    //    //}
    //}


    private void IsAllowToFillActionData(int projectId, string userMailID)
    {
        //2 level of enable disable first project level access second Month and Year level
        bool flag = false;

        string dateValue = DateTime.Now.AddMonths(-1).ToString("MMMM yyyy");

        if (ViewState["FourCRole"] != null && ViewState["FourCRole"].ToString().Trim() == MasterEnum.FourCRole.FOURCADMIN.ToString())
        {
            btnSave.Enabled = true;
        }
        else
        {
            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
            flag = fourCBAL.IsAllowToFillActionData(int.Parse(ddlDepartment.SelectedValue), projectId, userMailID);
            //dateValue.ToString().Trim() == lblDate.Text.ToString().Trim() &&
            //if (flag && ViewState["FourCRole"] != null && ViewState["FourCRole"].ToString().Trim() != MasterEnum.FourCRole.VIEWACCESSRIGHTS.ToString().Trim() && (!string.IsNullOrEmpty(Request.QueryString["AllowToEdit"].ToString()) || ((string.IsNullOrEmpty(hdSendForReviewEnable.Value) && !string.IsNullOrEmpty(hdSubmitratingEnable.Value) && hdSubmitratingEnable.Value != "False") || (string.IsNullOrEmpty(hdSubmitratingEnable.Value) && !string.IsNullOrEmpty(hdSendForReviewEnable.Value) && hdSendForReviewEnable.Value != "False")))
            if (flag && ViewState["FourCRole"] != null && ViewState["FourCRole"].ToString().Trim() != MasterEnum.FourCRole.VIEWACCESSRIGHTS.ToString().Trim()
                && (((!string.IsNullOrEmpty(hdSendForReviewEnable.Value) && hdSendForReviewEnable.Value != "False") || (!string.IsNullOrEmpty(hdSubmitratingEnable.Value) && hdSubmitratingEnable.Value != "False")) || (string.IsNullOrEmpty(hdSendForReviewEnable.Value) && string.IsNullOrEmpty(hdSubmitratingEnable.Value)))
                )
            {
                //btnSave.Enabled = true;
                makeReadOnly(this.Page, true);
            }
            else
            {
                //btnSave.Enabled = false;
                makeReadOnly(this.Page, false);

                if(ddlDepartment.SelectedItem.Text == "Projects")
                    ddlProjectList.Enabled = true;

                //btnCancel.Enabled = true;
            }
        }
    }

    public void makeReadOnly(Control c, bool flag)
    {
        foreach (Control childControl in c.Controls)
        {
            if (childControl.GetType() == typeof(TextBox))
            {
                ((TextBox)childControl).Enabled = flag;
            }
            else if (childControl.GetType() == typeof(CheckBox))
            {
                ((CheckBox)childControl).Enabled = flag;
            }
            else if (childControl.GetType() == typeof(DropDownList))
            {
                if(((DropDownList)childControl).ID.ToString() != "ddlDepartment")
                    ((DropDownList)childControl).Enabled = flag;
            }
            if (childControl.GetType() == typeof(Button))
            {
                if (((Button)childControl).ID.ToString() != "btnCancel" && ((Button)childControl).ID.ToString() != "btnClose")
                    ((Button)childControl).Enabled = flag;
            }

            if (childControl.GetType() == typeof(ImageButton))
            {
                if (((ImageButton)childControl).ID.ToString() != "lnkDesc" && ((ImageButton)childControl).ID.ToString() != "lnkAction" && ((ImageButton)childControl).ID.ToString() != "lnkRemarks")
                    ((ImageButton)childControl).Enabled = flag;
            }

            if (childControl.GetType() == typeof(Image))
            {
                ((Image)childControl).Enabled = flag;
            }

            if (childControl.Controls.Count > 0)
            {
                makeReadOnly(childControl, flag);
            }
        }
    }



    private void LoadParameter(string CType, DropDownList ddlParameter)
    {
        ddlParameter.Items.Clear();

        List<BusinessEntities.Master> objCTypeStatus = new List<BusinessEntities.Master>();
        objCTypeStatus = FillParameter(CType);

        ddlParameter.DataSource = objCTypeStatus;
        ddlParameter.DataTextField = MasterName;
        ddlParameter.DataValueField = MasterID;
        ddlParameter.DataBind();
        ddlParameter.Items.Insert(0, new ListItem(SELECTONE, ZERO));
    }

    


    private void LoadCType(object sender, GridViewRowEventArgs e)
    {
        DropDownList ddlCType = (DropDownList)e.Row.FindControl("ddlCType");
        DropDownList ddlParameter = (DropDownList)e.Row.FindControl("ddlCParameter");

        List<BusinessEntities.Master> objCTypeStatus = new List<BusinessEntities.Master>();
        objCTypeStatus = FillMasterData(CTypeId);

        ddlCType.DataSource = objCTypeStatus.OrderBy(o=>o.MasterId);
        ddlCType.DataTextField = MasterName;
        ddlCType.DataValueField = MasterID;
        ddlCType.DataBind();
        ddlCType.Items.Insert(0, new ListItem(SELECTONE, ZERO));

        ddlParameter.Items.Insert(0, new ListItem(SELECTONE, ZERO));
    }

    private void LoadActionStatus(object sender, GridViewRowEventArgs e)
    {
        DropDownList ddlActionStatus = (DropDownList)e.Row.FindControl("ddlActionStatus");
        

        List<BusinessEntities.Master> objCTypeStatus = new List<BusinessEntities.Master>();
        objCTypeStatus = FillMasterData(CActionStatusId);

        ddlActionStatus.DataSource = objCTypeStatus;
        ddlActionStatus.DataTextField = MasterName;
        ddlActionStatus.DataValueField = MasterID;
        ddlActionStatus.DataBind();
        ddlActionStatus.Items.Insert(0, new ListItem(SELECTONE, ZERO));

        ddlActionStatus.SelectedValue  = OpenStatusId;
    }

    //private void LoadActionOwner(object sender, GridViewRowEventArgs e)
    //{
    //    DropDownList ddlActionOwner = (DropDownList)e.Row.FindControl("ddlActionOwner");
    //    List<BusinessEntities.Master> objCTypeStatus = new List<BusinessEntities.Master>();
    //    objCTypeStatus = FillActionOwner(int.Parse(hdProjectId.Value));

    //    ddlActionOwner.DataSource = objCTypeStatus;
    //    ddlActionOwner.DataTextField = MasterName;
    //    ddlActionOwner.DataValueField = MasterID;
    //    ddlActionOwner.DataBind();

    //    ddlActionOwner.Items.Insert(0, new ListItem(SELECTONE, ZERO));
    //}

    protected void btnAddNewRow_Click(object sender, EventArgs e)
    {
        UpdateDataTable();

        dtActionData = AddNewRow(dtActionData);

        grdEmpActionDetails.DataSource = dtActionData;
        grdEmpActionDetails.DataBind();

        Session[SessionNames.FOURC_ACTION_DATA] = dtActionData;

    }

    private void UpdateDataTable()
    {
        if (Session[SessionNames.FOURC_ACTION_DATA] != null)
        {
            dtActionData = (DataTable)Session[SessionNames.FOURC_ACTION_DATA];
        }

        foreach (GridViewRow gvRow in grdEmpActionDetails.Rows)
        {
            Label lblRowId = (Label)gvRow.FindControl("lblRowId");
            int grdRow = int.Parse(lblRowId.Text);
            HiddenField hdActionId = (HiddenField)gvRow.FindControl("hdActionId");
            DropDownList ddlCType = (DropDownList)gvRow.FindControl("ddlCType");
            DropDownList ddlParameter = (DropDownList)gvRow.FindControl("ddlCParameter");
            TextBox txtDescription = (TextBox)gvRow.FindControl("txtDescription");
            TextBox txtAction = (TextBox)gvRow.FindControl("txtAction");
            //DropDownList ddlActionOwner = (DropDownList)gvRow.FindControl("ddlActionOwner");
            TextBox txtActionOwner = (TextBox)gvRow.FindControl("txtActionOwner");
            HiddenField hdActionOwnerId = (HiddenField)gvRow.FindControl("HfActionOwner");
            Label lblActionCreatedDate = (Label)gvRow.FindControl("lblActionCreatedDate");
            TextBox txtRemarks = (TextBox)gvRow.FindControl("txtRemarks");
            DropDownList ddlActionStatus = (DropDownList)gvRow.FindControl("ddlActionStatus");

            UIControls_DatePickerControl ucDatePickerTragetClosureDate = (UIControls_DatePickerControl)gvRow.FindControl("ucDatePickerTragetClosureDate");
            UIControls_DatePickerControl ucDatePickerActualClosureDate = (UIControls_DatePickerControl)gvRow.FindControl("ucDatePickerActualClosureDate");
            
            

            //if (string.IsNullOrEmpty(hdActionId.Value))
            //{

                dtActionData.Rows[grdRow][DbTableColumn.FourCType] = ddlCType.SelectedItem.Value;
                dtActionData.Rows[grdRow][DbTableColumn.FourCParameterType] = int.Parse(ddlParameter.SelectedItem.Value);
                dtActionData.Rows[grdRow][DbTableColumn.FourCDescription] = txtDescription.Text;
                dtActionData.Rows[grdRow][DbTableColumn.FourCAction] = txtAction.Text;
                //if (ddlActionOwner.SelectedIndex > 0)
                //{
                //    dtActionData.Rows[grdRow][DbTableColumn.FourCActionOwnerId] = int.Parse(ddlActionOwner.SelectedItem.Value);
                //}
                //else
                //{
                //    dtActionData.Rows[grdRow][DbTableColumn.FourCActionOwnerId] = 0;
                //}

                dtActionData.Rows[grdRow][DbTableColumn.FourCActionOwner] = txtActionOwner.Text;

                dtActionData.Rows[grdRow][DbTableColumn.FourCActionOwnerId] = hdActionOwnerId.Value;

                dtActionData.Rows[grdRow][DbTableColumn.FourCActionCreatedDate] = lblActionCreatedDate.Text;

                if(string.IsNullOrEmpty(ucDatePickerTragetClosureDate.Text))
                    dtActionData.Rows[grdRow][DbTableColumn.FourCTargetClosureDate] = DBNull.Value;
                else
                    dtActionData.Rows[grdRow][DbTableColumn.FourCTargetClosureDate] = ucDatePickerTragetClosureDate.Text;

                if (string.IsNullOrEmpty(ucDatePickerActualClosureDate.Text))
                    dtActionData.Rows[grdRow][DbTableColumn.FourCActualClosureDate] = DBNull.Value;
                else
                    dtActionData.Rows[grdRow][DbTableColumn.FourCActualClosureDate] = ucDatePickerActualClosureDate.Text;

                dtActionData.Rows[grdRow][DbTableColumn.FourCRemarks] = txtRemarks.Text;

                dtActionData.Rows[grdRow][DbTableColumn.FourCActionStatus] = ddlActionStatus.SelectedItem.Value;    

            //}
            
        }

        Session[SessionNames.FOURC_ACTION_DATA] = dtActionData;

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        
        UpdateDataTable();

        bool flag = false;

        DateTime dtMonthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));

               

        Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
        

        int countCompetency = 0, countCommunication = 0, countCommitment = 0, countCollaboration = 0;

        string valResult = "";
        bool minValReq = false;
        bool minAtLeastOneRow = false;
        bool closedvalidation = false;
        bool carryforwardValidation = false;
        //bool closedValidation_First = false;
        bool actionStatusValidation = false;


        foreach (GridViewRow gvRow in grdEmpActionDetails.Rows)
        {
            Label lblRowId = (Label)gvRow.FindControl("lblRowId");
            int grdRow = int.Parse(lblRowId.Text);
            HiddenField hdActionId = (HiddenField)gvRow.FindControl("hdActionId");
            DropDownList ddlCType = (DropDownList)gvRow.FindControl("ddlCType");
            DropDownList ddlParameter = (DropDownList)gvRow.FindControl("ddlCParameter");
            TextBox txtDescription = (TextBox)gvRow.FindControl("txtDescription");
            TextBox txtAction = (TextBox)gvRow.FindControl("txtAction");
            //DropDownList ddlActionOwner = (DropDownList)gvRow.FindControl("ddlActionOwner");
            TextBox txtActionOwner = (TextBox)gvRow.FindControl("txtActionOwner");
            HiddenField hdActionOwnerId = (HiddenField)gvRow.FindControl("HfActionOwner");
            Label lblActionCreatedDate = (Label)gvRow.FindControl("lblActionCreatedDate");
            TextBox txtRemarks = (TextBox)gvRow.FindControl("txtRemarks");
            DropDownList ddlActionStatus = (DropDownList)gvRow.FindControl("ddlActionStatus");
            HiddenField hdActionFrom = (HiddenField)gvRow.FindControl("hdActionFrom");

            UIControls_DatePickerControl ucDatePickerTragetClosureDate = (UIControls_DatePickerControl)gvRow.FindControl("ucDatePickerTragetClosureDate");
            UIControls_DatePickerControl ucDatePickerActualClosureDate = (UIControls_DatePickerControl)gvRow.FindControl("ucDatePickerActualClosureDate");

            if (ddlCType.SelectedItem.Text.Trim() == Competency)
            {
                countCompetency = countCompetency + 1;
            }
            else if (ddlCType.SelectedItem.Text.Trim() == Communication)
            {
                countCommunication = countCommunication + 1;
            }
            else if (ddlCType.SelectedItem.Text.Trim() == Commitment)
            {
                countCommitment = countCommitment + 1;
            }
            else if (ddlCType.SelectedItem.Text.Trim() == Collaboration)
            {
                countCollaboration = countCollaboration + 1;
            }

            //gvRow.Row.Cells[1].ForeColor = System.Drawing.Color.Red; 

            //gvRow.BorderColor = System.Drawing.Color.Red;

            Unit borderWidth = Unit.Pixel(2);
            Unit zeroBorderWidth = Unit.Pixel(0);

            if (ddlCType.SelectedIndex > 0 || ddlParameter.SelectedIndex > 0 || !string.IsNullOrEmpty(txtAction.Text))
            {
                if (!string.IsNullOrEmpty(hdActionFrom.Value) && ddlActionStatus.SelectedItem.Text == "Open")
                {
                    carryforwardValidation = true;
                }
                
                
                if (string.IsNullOrEmpty(txtDescription.Text))
                {
                    //minValReq = "Please Enter Description.";
                    minValReq = true;
                    gvRow.Cells[3].BorderColor = System.Drawing.Color.Red;
                    gvRow.Cells[3].BorderWidth = borderWidth;
                }
                else
                {
                    gvRow.Cells[3].BorderColor = System.Drawing.Color.White;
                    gvRow.Cells[3].BorderWidth = zeroBorderWidth;
                }

                if (ddlCType.SelectedIndex == 0)
                {
                    //if (string.IsNullOrEmpty(minValReq))
                    //    minValReq = "Please Select C.";
                    //else
                    //    minValReq = minValReq + "<br>" + "Please Select C.";

                    minValReq = true;

                    gvRow.Cells[1].BorderColor = System.Drawing.Color.Red;
                    gvRow.Cells[1].BorderWidth = borderWidth;
                }
                else
                {
                    gvRow.Cells[1].BorderColor = System.Drawing.Color.White;
                    gvRow.Cells[1].BorderWidth = zeroBorderWidth;
                }
                


                if (ddlParameter.SelectedIndex == 0)
                {
                    minValReq = true;
                    gvRow.Cells[2].BorderColor = System.Drawing.Color.Red;
                    gvRow.Cells[2].BorderWidth = borderWidth;
                    //gvRow.Cells[2].BackColor = System.Drawing.Color.Red;

                    //if (string.IsNullOrEmpty(minValReq))
                    //    minValReq = "Please Select C's Parameter.";
                    //else
                    //    minValReq = minValReq + "<br>" + "Please Select C's Parameter.";
                }
                else
                {
                    gvRow.Cells[2].BorderColor = System.Drawing.Color.White;
                    gvRow.Cells[2].BorderWidth = zeroBorderWidth;
                }

                if (string.IsNullOrEmpty(txtAction.Text))
                {
                    //if (string.IsNullOrEmpty(minValReq))
                    //    minValReq = "Please Enter Action.";
                    //else
                    //    minValReq = minValReq + "<br>" + "Please Enter Action.";

                    minValReq = true;
                    gvRow.Cells[4].BorderColor = System.Drawing.Color.Red;
                    gvRow.Cells[4].BorderWidth = borderWidth;
                }
                else
                {
                    gvRow.Cells[4].BorderColor = System.Drawing.Color.White;
                    gvRow.Cells[4].BorderWidth = zeroBorderWidth;
                }

                if (string.IsNullOrEmpty(txtActionOwner.Text))
                {
                    //if (string.IsNullOrEmpty(minValReq))
                    //    minValReq = "Please enter Action Owner.";
                    //else
                    //    minValReq = minValReq + "<br>" + "Please Enter Action Owner.";

                    minValReq = true;
                    gvRow.Cells[5].BorderColor = System.Drawing.Color.Red;
                    gvRow.Cells[5].BorderWidth = borderWidth;
                }
                else
                {
                    gvRow.Cells[5].BorderColor = System.Drawing.Color.White;
                    gvRow.Cells[5].BorderWidth = zeroBorderWidth;
                }
              
                if(string.IsNullOrEmpty(ucDatePickerTragetClosureDate.Text))
                {
                    //valResult = "Please enter Target Closure Date.";
                    //if (string.IsNullOrEmpty(minValReq))
                    //    minValReq = "Please enter Action Owner.";
                    //else
                    //    minValReq = minValReq + "<br>" + "Please enter Target Closure Date.";

                    minValReq = true;
                    gvRow.Cells[7].BorderColor = System.Drawing.Color.Red;
                    gvRow.Cells[7].BorderWidth = borderWidth;
                }
                else
                {
                    gvRow.Cells[7].BorderColor = System.Drawing.Color.White;
                    gvRow.Cells[7].BorderWidth = zeroBorderWidth;
                }

                if (ddlActionStatus.SelectedItem.Text == "Closed" && string.IsNullOrEmpty(ucDatePickerActualClosureDate.Text))
                {
                    //if (string.IsNullOrEmpty(minValReq))
                    //    minValReq = "Please enter Actual Closure date.";
                    //else
                    //    minValReq = minValReq + "<br>" + "Please enter Actual Closure date.";

                    minValReq = true;
                    gvRow.Cells[8].BorderColor = System.Drawing.Color.Red;
                    gvRow.Cells[8].BorderWidth = borderWidth;
                }
                else
                {
                    gvRow.Cells[8].BorderColor = System.Drawing.Color.White;
                    gvRow.Cells[8].BorderWidth = zeroBorderWidth;
                }

                if (ddlActionStatus.SelectedItem.Text == "Closed" && !string.IsNullOrEmpty(ucDatePickerActualClosureDate.Text) && (Convert.ToDateTime(ucDatePickerActualClosureDate.Text.ToString()) > DateTime.Now))
                {
                    closedvalidation = true;
                }

                if ((ddlActionStatus.SelectedItem.Text == "Closed" || ddlActionStatus.SelectedItem.Text == "On Hold") && string.IsNullOrEmpty(txtRemarks.Text))
                {
                    minValReq = true;
                    gvRow.Cells[9].BorderColor = System.Drawing.Color.Red;
                    gvRow.Cells[9].BorderWidth = borderWidth;
                }

                //if (ddlActionStatus.SelectedItem.Text != "Closed" &&  ddlActionStatus.SelectedIndex > 0 && !string.IsNullOrEmpty(ucDatePickerActualClosureDate.Text))
                //{
                //    closedValidation_First = true;
                //    gvRow.Cells[8].BorderColor = System.Drawing.Color.Red;
                //    gvRow.Cells[8].BorderWidth = borderWidth;
                //}


                if (ddlActionStatus.SelectedItem.Text == SELECTONE)
                {
                    //if (string.IsNullOrEmpty(minValReq))
                    //    minValReq = "Please Action Status.";
                    //else
                    //    minValReq = minValReq + "<br>" + "Please Action Status.";

                    minValReq = true;
                    gvRow.Cells[10].BorderColor = System.Drawing.Color.Red;
                    gvRow.Cells[10].BorderWidth = borderWidth;
                }
                else
                {
                    gvRow.Cells[10].BorderColor = System.Drawing.Color.White;
                    gvRow.Cells[10].BorderWidth = zeroBorderWidth;
                }

                // Onhold carry forward action, closed action cannot mark in open state.
                if (ddlCType.Enabled == false && ddlActionStatus.SelectedItem.Text == "Open")
                {
                    actionStatusValidation = true;

                    gvRow.Cells[10].BorderColor = System.Drawing.Color.Red;
                    gvRow.Cells[10].BorderWidth = borderWidth;
                }
            }

            if (ddlCType.SelectedIndex == 0 && ddlParameter.SelectedIndex == 0 && dtActionData.Rows.Count == 1)
            {
                minAtLeastOneRow = true;
            }
        }

        if (minAtLeastOneRow && ddlRAGCollaboration.SelectedIndex == 0 && ddlRAGCommitment.SelectedIndex == 0 && ddlRAGCommunication.SelectedIndex == 0 && ddlRAGCompetency.SelectedIndex == 0)
        {
            minAtLeastOneRow = true;
        }
        else
        {
            minAtLeastOneRow = false;
        }


        //if (!string.IsNullOrEmpty(valResult) || !string.IsNullOrEmpty(minValReq))
        //{
        //    lblMessage.Visible = true;
        //    lblMessage.ForeColor = System.Drawing.Color.Red;
        //    lblMessage.Text = minValReq + "<br />" + valResult;
        //    return;
        //}


        string strVal = "";

        if ((ddlRAGCompetency.SelectedItem.Text == "Red" || ddlRAGCompetency.SelectedItem.Text == "Amber") && countCompetency == 0)
        {
            strVal = Competency;
        }
        if ((ddlRAGCommunication.SelectedItem.Text == "Red" || ddlRAGCommunication.SelectedItem.Text == "Amber") && countCommunication == 0)
        {
            if(string.IsNullOrEmpty(strVal))
                strVal = Communication;
            else
                strVal = strVal + ", " + Communication;
        }
        if ((ddlRAGCommitment.SelectedItem.Text == "Red" || ddlRAGCommitment.SelectedItem.Text == "Amber") && countCommitment == 0)
        {
            if (string.IsNullOrEmpty(strVal))
                strVal = Commitment;
            else
                strVal = strVal + ", " + Commitment;
        }
        if ((ddlRAGCollaboration.SelectedItem.Text == "Red" || ddlRAGCollaboration.SelectedItem.Text == "Amber") && countCollaboration == 0)
        {
            if (string.IsNullOrEmpty(strVal))
                strVal = Collaboration;
            else
                strVal = strVal + ", " + Collaboration;
        }


        if (!string.IsNullOrEmpty(strVal) || minValReq || minAtLeastOneRow || closedvalidation || carryforwardValidation || actionStatusValidation)// || closedValidation_First)
        {
            lblMessage.Text = "";
            lblMessage.Visible = true;
            lblMessage.ForeColor = System.Drawing.Color.Red;
            if(!string.IsNullOrEmpty(strVal))
                lblMessage.Text = "Please Add Action for " + strVal + " .";
            if (minValReq)
                lblMessage.Text = lblMessage.Text + "<br />" + "Please enter relevant information in highlighted field/s.";
            if(minAtLeastOneRow)
                lblMessage.Text = lblMessage.Text + "<br />" + "Please enter rating details.";
            if(closedvalidation)
                lblMessage.Text = lblMessage.Text + "<br />" + "Please enter Actual Closure Date Less than or equal to current date.";
            if(carryforwardValidation)
                lblMessage.Text = lblMessage.Text + "<br />" + "Action Status cannot be changed from carry forward to Open.";
            if (actionStatusValidation)
                lblMessage.Text = lblMessage.Text + "<br />" + "Carry Forward, On Hold, Closed action Status cannot be changed to Open.";
            //if (closedValidation_First)
            //    lblMessage.Text = lblMessage.Text + "<br />" + "Please remove actual closure date."; 
            return;
        }


        objFeedBack = fourCBAL.Get4CIndividualEmployeeDeatils(int.Parse(ddlDepartment.SelectedItem.Value), int.Parse(ddlProjectList.SelectedItem.Value), dtMonthYear.Month, dtMonthYear.Year, int.Parse(hdEmpId.Value));

        if (ddlRAGCollaboration.SelectedIndex > 0)
            objFeedBack.Collaboration = ddlRAGCollaboration.SelectedItem.Value;
        else
            objFeedBack.Collaboration = null;

        if(ddlRAGCommitment.SelectedIndex > 0)
            objFeedBack.Commitment = ddlRAGCommitment.SelectedItem.Value;
        else
            objFeedBack.Commitment = null;

        if (ddlRAGCommunication.SelectedIndex > 0)
            objFeedBack.Communication = ddlRAGCommunication.SelectedItem.Value;
        else
            objFeedBack.Communication = null;

        if (ddlRAGCompetency.SelectedIndex > 0)
            objFeedBack.Competency = ddlRAGCompetency.SelectedItem.Value;
        else
            objFeedBack.Competency = null;

        objFeedBack.ModifiedById = UserMailId;


        //UPDATE the FB Id in action if anyone has filled all green and next time try to add action for the same month.
        if (objFeedBack.FBID > 0)
        {
            foreach (DataRow dr in dtActionData.Rows)
            {
                if ((!string.IsNullOrEmpty(dr[DbTableColumn.FourCType].ToString()) && dr[DbTableColumn.FourCType].ToString() != "0") && (!string.IsNullOrEmpty(dr[DbTableColumn.FourCParameterType].ToString()) && dr[DbTableColumn.FourCParameterType].ToString() != "0"))
                {
                    dr[DbTableColumn.FourCId] = objFeedBack.FBID;
                }
            }
        }



        try
        {
            flag = fourCBAL.InsertActionDetails(dtActionData, objFeedBack);

            if (flag)
            {
                Session[SessionNames.FOURC_ACTION_DATA] = null;
                //Response.Write("<script>returnValue='" + ddlProjectList.SelectedItem.Value + "_" + dtMonthYear.Month + "_" + dtMonthYear.Year + "';</script>");
                Response.Write("<script>returnValue='" + ddlProjectList.SelectedItem.Value + "_" + hdMonth.Value + "_" + hdYear.Value + "';</script>");
                Response.Write("<script language='javascript'>alert('Data Saved Successfully'); window.close();</script>");
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnSave_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }

    }

    //When There is change project name dropdown then change FBId also  //////need to be done.
    //When dropdown change fill the FourCFeedback entity


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Session[SessionNames.FOURC_ACTION_DATA] = null;
        dtActionData = null;

        //Ishwar 09012015 Start
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "CloseJS",
            "jQuery.modalDialog.getCurrent().close();jQuery.modalDialog.getCurrent().postMessageToParent('" + dtActionData + "');", true);
        //Response.Write("<script language='javascript'>window.close();</script>");
        //Ishwar 09012015 End
    }

    protected void lnk_Click(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();

        txtDesc.Text = "";
       // btnDescSave.Enabled = true;
        btnClose.Enabled = true;

        ImageButton btnDetails = sender as ImageButton;
        GridViewRow row = (GridViewRow)btnDetails.NamingContainer;
        DropDownList ddl = (DropDownList)row.FindControl("ddlCParameter");
        //ViewEmp4C
        if (!string.IsNullOrEmpty(id.ToString()) && int.Parse(id) > 0)
        {
            hdRowId.Value = id;
        }
        else
        {
            Label lblNo = (Label)row.FindControl("lblRowId");

            hdRowId.Value = lblNo.Text;
        }

        //Page page = HttpContext.Current.Handler as Page;
        //if (!ddl.Enabled)
        //{
        //    if (e.CommandName != "Remarks")
        //    {
        //        ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "document.getElementById('" + btnDescSave.ClientID + "').style.visibility='hidden';", true);
        //    }
        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "document.getElementById('" + btnDescSave.ClientID + "').style.visibility='visible';", true);
        //}

        if (e.CommandName == "Desc")
        {
            TextBox txtDescription = (TextBox)row.FindControl("txtDescription");
            txtDesc.Text = txtDescription.Text;
            ViewState["ImageButtonType"] = "Desc";
            lblModelPopupHeader.Text = "Description";
        }
        else if (e.CommandName == "Action")
        {
            TextBox txtAction = (TextBox)row.FindControl("txtAction");
            txtDesc.Text = txtAction.Text;
            ViewState["ImageButtonType"] = "Action";
            lblModelPopupHeader.Text = "Action";
        }
        else if (e.CommandName == "Remarks")
        {
            TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
            txtDesc.Text = txtRemarks.Text;
            ViewState["ImageButtonType"] = "Remarks";
            lblModelPopupHeader.Text = "Remarks";
        }


        updPnlCustomerDetail.Update();

        this.mdlPopup.Show();

    }
}
