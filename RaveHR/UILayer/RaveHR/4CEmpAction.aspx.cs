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

public partial class _4CEmpAction : BaseClass
{
    int fromCurrentDate;
    string strEmpCode;
    private const string CLASS_NAME = "_4CEmpAction.aspx";

    string CTypeId = "87";
    string CActionStatusId = "86";
    string OpenStatusId = "1065";
    string CarryForwardId = "1068";
    string CommentId = "1107";

    string NotApplicableStatus = "N/A";
    const string ActionINSERT = "INSERT";
    const string ActionUPDATE = "UPDATE";
    const string ActionDELETE = "DELETE";

    const string ActComeFromPreviousMonth = "PREVIOUSMONTH";

    DataTable dtActionData;
    DataTable dtActionDataDelete;
    string MasterName = "MasterName";
    string MasterID = "MasterID";
    //Define the zero as string.
    private string ZERO = "0";
    //Define the select as string.
    private string SELECTONE = "SELECT";
    string UserMailId;
    string UserRaveDomainId;

    FourCFeedback objFeedBack;
    DataSet dsEmpDashboard;
    DataTable oldDBActionData;

    const string Competency = "Competency";
    const string Communication = "Communication";
    const string Commitment = "Commitment";
    const string Collaboration = "Collaboration";
    const string CommentActionStatus = "Comment";


    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
        HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();

        //Poonam : Issue : Disable Button : Starts
        //btnSubmitRating.Attributes.Add(CommonConstants.EVENT_ONCLICK, "javascript:return CheckIfRowChecked();");
        btnSave.OnClientClick = ClientScript.GetPostBackEventReference(btnSave, null);
        //Poonam : Issue : Disable Button : Ends


        if (!string.IsNullOrEmpty(DecryptQueryString("LoginEmailId").ToString()))
            UserMailId = DecryptQueryString("LoginEmailId").ToString();

        if (!IsPostBack)
        {
            //check access rights security
            bool flag = Check4CAccess();
            if (!flag)
            {
                Response.Redirect("Home.aspx");
            }
            else
            {

                int empId = 0;
                int projectId = 0;
                int departmentId = 0;
                int month = 0;
                int year = 0;
                string fourCRole = "";
                string empName = "";

                if (!string.IsNullOrEmpty(DecryptQueryString("EmpId").ToString()))
                    empId = int.Parse(DecryptQueryString("EmpId").ToString());

                if (!string.IsNullOrEmpty(DecryptQueryString("projectId").ToString()))
                    projectId = int.Parse(DecryptQueryString("projectId").ToString());

                if (!string.IsNullOrEmpty(DecryptQueryString("departmentId").ToString()))
                    departmentId = int.Parse(DecryptQueryString("departmentId").ToString());

                if (!string.IsNullOrEmpty(DecryptQueryString("month").ToString()))
                    month = int.Parse(DecryptQueryString("month").ToString());

                if (!string.IsNullOrEmpty(DecryptQueryString("year").ToString()))
                    year = int.Parse(DecryptQueryString("year").ToString());

                if (!string.IsNullOrEmpty(DecryptQueryString("FourCRole").ToString()))
                    fourCRole = DecryptQueryString("FourCRole").ToString();

                if (!string.IsNullOrEmpty(DecryptQueryString("EmpName").ToString()))
                    empName = DecryptQueryString("EmpName").ToString();

                Session["HistoryMonth"] = 3;
                btnDashboardHistory.Enabled = true;

                if (!string.IsNullOrEmpty(DecryptQueryString("AllowDirectSubmit")))
                {
                    ViewState["AllowDirectSubmit"] = DecryptQueryString("AllowDirectSubmit").ToString();
                }
                else
                {
                    ViewState["AllowDirectSubmit"] = "";
                }

                hdEmpId.Value = empId.ToString();
                hdMonth.Value = month.ToString();
                hdProjectId.Value = projectId.ToString();
                hdRole.Value = fourCRole.ToString();
                hdYear.Value = year.ToString();
                ViewState["FourCRole"] = fourCRole;
                hdFBId.Value = DecryptQueryString("FBID").ToString();

                //lblDate.Text = string.Concat(month.ToString(), " ", year.ToString());
                //DateTime dtMonthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));
                DateTime dtMonthYear = DateTime.Parse(string.Concat("01 ", month.ToString(), " ", year.ToString()));
                // Request.QueryString["MyArgs"].ToString();
                lblDate.Text = dtMonthYear.ToString("MMMM") + " " + dtMonthYear.Year;
                //As per month selection add ViewState["ClickCount"] value so that next and previous icon work properly.
                if (lblDate.Text != DateTime.Now.AddMonths(-1).ToString("MMMM yyyy"))
                {
                    for (int iCount = 1; iCount <= 7; iCount++)
                    {
                        if (lblDate.Text == DateTime.Now.AddMonths(-iCount).ToString("MMMM yyyy"))
                        {
                            ViewState["ClickCount"] = iCount;
                            break;
                        }
                    }
                }
                else
                {
                    ViewState["ClickCount"] = 1;
                }



                if (lblDate.Text == DateTime.Now.AddMonths(-7).ToString("MMMM yyyy"))
                {
                    imgPrevious.Enabled = false;
                }
                //if (lblDate.Text == DateTime.Now.AddMonths(-1).ToString("MMMM yyyy"))
                if (lblDate.Text == DateTime.Now.ToString("MMMM yyyy"))
                {
                    imgNext.Enabled = false;
                }

                //fromCurrentDate = 2;

                //calculate last day of month and which is not holiday
                Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();

                DateTime? dtCurrentLastDayOfMonth = fourCBAL.CurrentMonthLastDay();
                //ViewState["CurrentMonthLastDay"] = dtCurrentLastDayOfMonth;
                if (lblDate.Text == DateTime.Now.ToString("MMMM yyyy"))
                {
                    ViewState["CarryForwardCommentDate"] = dtCurrentLastDayOfMonth;
                }
                else
                {
                    if (dtCurrentLastDayOfMonth != null)
                    {
                        DateTime dtLastMonthLastDate = (DateTime)dtCurrentLastDayOfMonth;
                        dtLastMonthLastDate = dtLastMonthLastDate.AddMonths(-1);
                        ViewState["CarryForwardCommentDate"] = dtLastMonthLastDate;
                    }
                }




                lblEmpName.Text = empName;
                //FillCType();

                FillDepartment(departmentId);
                fillProjectDropdown(projectId, dtMonthYear.Month, dtMonthYear.Year);

                ddlProjectList.SelectedValue = hdProjectId.Value;

                //BindData();
                //BindActionData(int.Parse(hdProjectId.Value), int.Parse(hdMonth.Value), int.Parse(hdYear.Value), fourCRole);
                BindActionData(int.Parse(hdProjectId.Value), dtMonthYear.Month, dtMonthYear.Year, ViewState["FourCRole"].ToString());

                LoadDashboardData();

                //Allow reviewer remarks for only reviewer
                if (!string.IsNullOrEmpty(DecryptQueryString("Mode")) && DecryptQueryString("Mode").ToString().Trim() == "0")
                {
                    txtRemarksReviewer.Enabled = false;
                    txtRemarksReviewer.Visible = false;
                    tblReviewerRemarks.Visible = false;
                }
                else
                {
                    txtRemarksReviewer.Visible = true;
                    txtRemarksReviewer.Enabled = true;
                    tblReviewerRemarks.Visible = true;
                }
            }
        }
        fillDropdownColor();
    }


    private void fillProjectDropdown(int projectId, int month, int year)
    {
        try
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
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "fillProjectDropdown", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    private void FillDepartment(int departmentId)
    {
        try
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
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "FillDepartment", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    private void BindActionData(int projectId, int month, int year, string fourCRole)
    {
        try
        {
            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();

            DataSet dsEmpDeatils = new DataSet();

            DataSet dsDetails = fourCBAL.Get4CActionDetails(int.Parse(ddlDepartment.SelectedValue), int.Parse(hdEmpId.Value), projectId, month, year, UserMailId, int.Parse(DecryptQueryString("Mode").ToString())); //0 Add 1 Review
            dtActionData = new DataTable();

            //dtActionData = dsDetails.Tables[0];

            //SET Manager Name and Reviewer Name
            if (dsDetails != null && dsDetails.Tables[2].Rows.Count > 0)
            {

                if (dsDetails.Tables[2].Rows.Count > 0 && !string.IsNullOrEmpty(dsDetails.Tables[2].Rows[0][0].ToString()))
                    lblManager.Text = dsDetails.Tables[2].Rows[0][0].ToString();
                else
                    lblManager.Text = "";

                if (dsDetails.Tables[2].Rows.Count > 0 && !string.IsNullOrEmpty(dsDetails.Tables[2].Rows[0][1].ToString()))
                    lblReviewerName.Text = dsDetails.Tables[2].Rows[0][1].ToString();
                else
                    lblReviewerName.Text = "";
            }

            //SEt send for review enable or disable
            if (dsDetails != null && dsDetails.Tables[3].Rows.Count > 0)
            {
                //if (!string.IsNullOrEmpty(dsDetails.Tables[3].Rows[0][0].ToString()))
                //{
                //    hdSendForReviewEnable.Value = dsDetails.Tables[3].Rows[0][0].ToString();
                //}
                //else
                //{
                //    hdSendForReviewEnable.Value = "";
                //}

                //if (!string.IsNullOrEmpty(dsDetails.Tables[3].Rows[0][1].ToString()))
                //{
                //    hdSubmitratingEnable.Value = dsDetails.Tables[3].Rows[0][1].ToString();
                //}
                //else
                //{
                //    hdSubmitratingEnable.Value = "";
                //}

                //Allow to fill action or not check
                //if (!string.IsNullOrEmpty(dsDetails.Tables[3].Rows[0][0].ToString()))
                if (dsDetails.Tables[3].Rows.Count > 0)
                {
                    hdSendForReviewEnable.Value = dsDetails.Tables[3].Rows[0][0].ToString();
                    hdSubmitratingEnable.Value = dsDetails.Tables[3].Rows[0][1].ToString();
                }

                //set emp Name
                if (!string.IsNullOrEmpty(dsDetails.Tables[3].Rows[0][3].ToString()))
                {
                    lblEmpName.Text = "4C Feedback For -" + dsDetails.Tables[3].Rows[0][3].ToString();
                }

                if (!string.IsNullOrEmpty(dsDetails.Tables[3].Rows[0][2].ToString()))
                {
                    if (dsDetails.Tables[3].Rows[0][2].ToString().Trim() == "TRUE")
                    {
                        divFinalRating.Visible = false;
                        //divAddNewRow.Visible = false;
                        divActionDetails.Visible = false;
                        divParameters.Visible = false;
                        btnSave.Visible = false;
                        divNoDataFound.Visible = true;
                    }
                    else
                    {
                        divFinalRating.Visible = true;
                        //divAddNewRow.Visible = true;
                        divActionDetails.Visible = true;
                        divParameters.Visible = true;
                        btnSave.Visible = true;
                        divNoDataFound.Visible = false;
                    }
                }

            }


            AssignFinalRatingColor(dsDetails);

            IsAllowToFillActionData(projectId, UserMailId);

            if (dsDetails.Tables[0].Rows.Count == 0)
            {
                dtActionData.Columns.Add(DbTableColumn.FourCROWId);
                dtActionData.Columns.Add(DbTableColumn.FourCActionId);
                dtActionData.Columns.Add(DbTableColumn.FourCId);
                dtActionData.Columns.Add(DbTableColumn.FourCType);
                dtActionData.Columns.Add(DbTableColumn.FourCParameterType);
                dtActionData.Columns.Add(DbTableColumn.FourCDescription);
                dtActionData.Columns.Add(DbTableColumn.FourCAction);
                dtActionData.Columns.Add(DbTableColumn.FourCActionOwner);
                dtActionData.Columns.Add(DbTableColumn.FourCActionOwnerId);
                dtActionData.Columns.Add(DbTableColumn.FourCActionCreatedDate);
                dtActionData.Columns.Add(DbTableColumn.FourCTargetClosureDate);
                dtActionData.Columns.Add(DbTableColumn.FourCActualClosureDate);
                dtActionData.Columns.Add(DbTableColumn.FourCRemarks);
                dtActionData.Columns.Add(DbTableColumn.FourCActionStatus);
                dtActionData.Columns.Add(DbTableColumn.ActionDML);
                dtActionData.Columns.Add(DbTableColumn.ActionFrom);

                //Ishwar : 08072015 Start
                dtActionData.Columns.Add(DbTableColumn.LastMonthDescription);
                dtActionData.Columns.Add(DbTableColumn.LastMonthAction);
                dtActionData.Columns.Add(DbTableColumn.LastMonthActionOwnerId);
                dtActionData.Columns.Add(DbTableColumn.LastMonthTargetClosureDate);
                dtActionData.Columns.Add(DbTableColumn.LastMonthActualClosuredate);
                dtActionData.Columns.Add(DbTableColumn.LastMonthRemarks);
                dtActionData.Columns.Add(DbTableColumn.LastMonthActionStatus);
                //Ishwar : 08072015 End
                dtActionData.Columns.Add(DbTableColumn.ParentFBAID);
                
                dtActionData = AddNewRow(dtActionData);
                //Session[SessionNames.FOURC_ACTION_DATA] = dtActionData;
                ViewState["dtActionData"] = dtActionData;

                grdEmpActionDetails.DataSource = dtActionData;
                grdEmpActionDetails.DataBind();


            }
            else
            {
                dtActionData = dsDetails.Tables[0];

                //Session[SessionNames.FOURC_ACTION_DATA] = dtActionData;
                ViewState["dtActionData"] = dtActionData;

                grdEmpActionDetails.DataSource = dtActionData;
                grdEmpActionDetails.DataBind();


            }

            if (!IsPostBack)
            {
                //foreach (GridViewRow gvRow in grdEmpActionDetails.Rows)
                //{
                //    Label lblRowId = (Label)gvRow.FindControl("lblRowId");
                //    int grdRow = int.Parse(lblRowId.Text);
                //    HiddenField HfActionStatus = (HiddenField)gvRow.FindControl("HfActionStatus");
                //    DropDownList ddlActionStatus = (DropDownList)gvRow.FindControl("ddlActionStatus");

                //    HfActionStatus.Value = ddlActionStatus.SelectedValue;

                //}

                oldDBActionData = dtActionData;
                ViewState["dtOldDBActionData"] = oldDBActionData;
            }

            //IsAllowToFillActionData(projectId, UserMailId);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "BindActionData", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    private void AssignFinalRatingColor(DataSet dsColor)
    {
        try
        {
            if (dsColor.Tables[1].Rows.Count > 0)
            {
                if (dsColor.Tables[1].Rows[0][0] != null && !string.IsNullOrEmpty(dsColor.Tables[1].Rows[0][0].ToString()))
                {
                    ddlRAGCompetency.SelectedValue = dsColor.Tables[1].Rows[0][0].ToString();
                    ViewState["CompetencyRatingByCreator"] = dsColor.Tables[1].Rows[0][0].ToString();
                }
                else
                    ddlRAGCompetency.SelectedIndex = 0;


                if (dsColor.Tables[1].Rows[0][1] != null && !string.IsNullOrEmpty(dsColor.Tables[1].Rows[0][1].ToString()))
                {
                    ddlRAGCommunication.SelectedValue = dsColor.Tables[1].Rows[0][1].ToString();
                    ViewState["CommunicationRatingByCreator"] = dsColor.Tables[1].Rows[0][1].ToString();
                }
                else
                    ddlRAGCommunication.SelectedIndex = 0;

                if (dsColor.Tables[1].Rows[0][2] != null && !string.IsNullOrEmpty(dsColor.Tables[1].Rows[0][2].ToString()))
                {
                    ddlRAGCommitment.SelectedValue = dsColor.Tables[1].Rows[0][2].ToString();
                    ViewState["CommitmentRatingByCreator"] = dsColor.Tables[1].Rows[0][2].ToString();
                }
                else
                    ddlRAGCommitment.SelectedIndex = 0;

                if (dsColor.Tables[1].Rows[0][3] != null && !string.IsNullOrEmpty(dsColor.Tables[1].Rows[0][3].ToString()))
                {
                    ddlRAGCollaboration.SelectedValue = dsColor.Tables[1].Rows[0][3].ToString();
                    ViewState["CollaborationRatingByCreator"] = dsColor.Tables[1].Rows[0][3].ToString();
                }
                else
                    ddlRAGCollaboration.SelectedIndex = 0;

                //4 count is for 4 C color
                if (dsColor.Tables[1].Columns.Count > 4 && dsColor.Tables[1].Rows[0][4] != null && !string.IsNullOrEmpty(dsColor.Tables[1].Rows[0][4].ToString()))
                {
                    txtRemarksReviewer.Text = dsColor.Tables[1].Rows[0][4].ToString();
                }

                //Ishwar : 02072015 Start
                //string competencySelectedValue = ddlRAGCompetency.SelectedItem.Value == "Amber" ? "#FFCC00" : ddlRAGCompetency.SelectedItem.Value;
                //string collaborationSelectedValue = ddlRAGCollaboration.SelectedItem.Value == "Amber" ? "#FFCC00" : ddlRAGCollaboration.SelectedItem.Value;
                //string communicationSelectedValue = ddlRAGCommunication.SelectedItem.Value == "Amber" ? "#FFCC00" : ddlRAGCommunication.SelectedItem.Value;
                //string commitmentSelectedValue = ddlRAGCommitment.SelectedItem.Value == "Amber" ? "#FFCC00" : ddlRAGCommitment.SelectedItem.Value;

                //ddlRAGCompetency.Items[1].Attributes.Add("style", "background-color: #008000");
                //ddlRAGCompetency.Items[2].Attributes.Add("style", "background-color: #FFCC00");
                //ddlRAGCompetency.Items[3].Attributes.Add("style", "background-color: #FF0000");

                //ddlRAGCompetency.Attributes.Add("style", "background:" + competencySelectedValue);

                //ddlRAGCollaboration.Items[1].Attributes.Add("style", "background-color: #008000");
                //ddlRAGCollaboration.Items[2].Attributes.Add("style", "background-color: #FFCC00");
                //ddlRAGCollaboration.Items[3].Attributes.Add("style", "background-color: #FF0000");

                //ddlRAGCollaboration.Attributes.Add("style", "background:" + collaborationSelectedValue);

                //ddlRAGCommunication.Items[1].Attributes.Add("style", "background-color: #008000");
                //ddlRAGCommunication.Items[2].Attributes.Add("style", "background-color: #FFCC00");
                //ddlRAGCommunication.Items[3].Attributes.Add("style", "background-color: #FF0000");

                //ddlRAGCommunication.Attributes.Add("style", "background:" + communicationSelectedValue);

                //ddlRAGCommitment.Items[1].Attributes.Add("style", "background-color: #008000");
                //ddlRAGCommitment.Items[2].Attributes.Add("style", "background-color: #FFCC00");
                //ddlRAGCommitment.Items[3].Attributes.Add("style", "background-color: #FF0000");

                //ddlRAGCommitment.Attributes.Add("style", "background:" + commitmentSelectedValue);
                //Desc : Only for review login : Start 
                //if (!string.IsNullOrEmpty(DecryptQueryString("Mode")) && DecryptQueryString("Mode").ToString().Trim() == "1")
                //{
                //    if (Convert.ToString(dsColor.Tables[1].Rows[0][DbTableColumn.Competency]) != Convert.ToString(dsColor.Tables[1].Rows[0][DbTableColumn.LastMonthCompetency]))
                //    {
                //        ddlRAGCompetency.Attributes.Add("style", "border: solid 3px Blue; background:" + competencySelectedValue);
                //        ViewState["CompetencyFlag"] = CommonConstants.ONE;
                //    }
                //    if (Convert.ToString(dsColor.Tables[1].Rows[0][DbTableColumn.Collaboration]) != Convert.ToString(dsColor.Tables[1].Rows[0][DbTableColumn.LastMonthCollaboration]))
                //    {
                //        ddlRAGCollaboration.Attributes.Add("style", "border: solid 3px Blue; background:" + collaborationSelectedValue);
                //        ViewState["CollaborationFlag"] = CommonConstants.ONE;
                //    }
                //    if (Convert.ToString(dsColor.Tables[1].Rows[0][DbTableColumn.Communication]) != Convert.ToString(dsColor.Tables[1].Rows[0][DbTableColumn.LastMonthCommunication]))
                //    {
                //        ddlRAGCommunication.Attributes.Add("style", "border: solid 3px Blue; background:" + communicationSelectedValue);
                //        ViewState["CommunicationFlag"] = CommonConstants.ONE;
                //    }
                //    if (Convert.ToString(dsColor.Tables[1].Rows[0][DbTableColumn.Commitment]) != Convert.ToString(dsColor.Tables[1].Rows[0][DbTableColumn.LastMonthCommitment]))
                //    {
                //        ddlRAGCommitment.Attributes.Add("style", "border: solid 3px Blue; background:" + commitmentSelectedValue);
                //        ViewState["CommitmentFlag"] = CommonConstants.ONE;
                //    }
                //}
                //Desc : Only for review login : End
                ////Ishwar : 02072015 End
            }
            else
            {
                ddlRAGCompetency.SelectedIndex = 0;
                ddlRAGCommunication.SelectedIndex = 0;
                ddlRAGCommitment.SelectedIndex = 0;
                ddlRAGCollaboration.SelectedIndex = 0;
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "AssignFinalRatingColor", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }



    private DataTable AddNewRow(DataTable dtActionData)
    {

        DateTime dtMonthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));

        DataRow dr = dtActionData.NewRow();
        dr[DbTableColumn.FourCROWId] = grdEmpActionDetails.Rows.Count;
        dr[DbTableColumn.FourCActionId] = 0;

        if (dtActionData != null && dtActionData.Rows.Count >= 1)
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

        dr[DbTableColumn.ParentFBAID] = 0;

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
        try
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

            //Ishwar : 13072015 Start : For 4C hightlight CR
            if (!string.IsNullOrEmpty(DecryptQueryString("Mode")) && DecryptQueryString("Mode").ToString().Trim() == "1")
            {
                if (Convert.ToInt32(ViewState["CompetencyFlag"]) == CommonConstants.ONE)
                {
                    ddlRAGCompetency.Attributes.Add("style", "border: solid 3px Blue; background:" + competencySelectedValue);
                }
                if (Convert.ToInt32(ViewState["CollaborationFlag"]) == CommonConstants.ONE)
                {
                    ddlRAGCollaboration.Attributes.Add("style", "border: solid 3px Blue; background:" + collaborationSelectedValue);
                }
                if (Convert.ToInt32(ViewState["CommunicationFlag"]) == CommonConstants.ONE)
                {
                    ddlRAGCommunication.Attributes.Add("style", "border: solid 3px Blue; background:" + communicationSelectedValue);
                }
                if (Convert.ToInt32(ViewState["CommitmentFlag"]) == CommonConstants.ONE)
                {
                    ddlRAGCommitment.Attributes.Add("style", "border: solid 3px Blue; background:" + commitmentSelectedValue);
                }
            }
            //Ishwar : 13072015 End
            // Sanju:Issue Id 50201:End
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "fillDropdownColor", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    protected void imgPrevious_Click(object sender, ImageClickEventArgs e)
    {
        try
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
            //Bind Grid
            //if (lblDate.Text == DateTime.Now.AddMonths(-6).ToString("MMMM yyyy"))
            //{
            //    imgPrevious.Enabled = false;
            //}

            BindActionData(int.Parse(ddlProjectList.SelectedItem.Value), dtMonthYear.Month, dtMonthYear.Year, ViewState["FourCRole"].ToString());

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "imgPrevious_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }

    }
    protected void imgNext_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //if (lblDate.Text == DateTime.Now.AddMonths(-1).ToString("MMMM yyyy"))
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
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "imgPrevious_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    //protected void grdEmpActionDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    try
    //    {
    //        //if (e. == DataControlRowType.DataRow)
    //        //{
    //            DropDownList ddlActionStatus = (DropDownList)grdEmpActionDetails.Rows[e.RowIndex].FindControl("ddlActionStatus");
    //            HiddenField HfActionStatus = (HiddenField)grdEmpActionDetails.Rows[e.RowIndex].FindControl("HfActionStatus");
    //            //HiddenField HfActionStatus = (HiddenField)e.Row.FindControl("HfActionStatus");
    //            HfActionStatus.Value = ddlActionStatus.SelectedValue;
    //        //}
    //    }
    //    catch (RaveHRException ex)
    //    {
    //        throw ex;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdEmpActionDetails_RowDataBound", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
    //    }
    //}
    protected void grdEmpActionDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        int value = int.Parse(grdEmpActionDetails.SelectedDataKey.Value.ToString());
    }


    protected void grdEmpActionDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                Label lblRowId = (Label)e.Row.FindControl("lblRowId");
                HiddenField hdActionId = (HiddenField)e.Row.FindControl("hdActionId");
                DropDownList ddlCType = (DropDownList)e.Row.FindControl("ddlCType");
                DropDownList ddlParameter = (DropDownList)e.Row.FindControl("ddlCParameter");
                TextBox txtDescription = (TextBox)e.Row.FindControl("txtDescription");
                TextBox txtAction = (TextBox)e.Row.FindControl("txtAction");
                TextBox txtActionOwner = (TextBox)e.Row.FindControl("txtActionOwner");
                HiddenField HfActionOwnerName = (HiddenField)e.Row.FindControl("HfActionOwnerName");
                HiddenField HfActionStatus = (HiddenField)e.Row.FindControl("HfActionStatus");

                //DropDownList ddlActionOwner = (DropDownList)e.Row.FindControl("ddlActionOwner");
                Label lblActionCreatedDate = (Label)e.Row.FindControl("lblActionCreatedDate");
                //TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");
                Image imgActionOwner = (Image)e.Row.FindControl("imgActionOwner");

                UIControls_DatePickerControl ucDatePickerTragetClosureDate = (UIControls_DatePickerControl)e.Row.FindControl("ucDatePickerTragetClosureDate");
                UIControls_DatePickerControl ucDatePickerActualClosureDate = (UIControls_DatePickerControl)e.Row.FindControl("ucDatePickerActualClosureDate");

                DropDownList ddlActionStatus = (DropDownList)e.Row.FindControl("ddlActionStatus");
                int grdRow = int.Parse(lblRowId.Text);

                ImageButton imgDelete = (ImageButton)e.Row.FindControl("imgDelete");
                imgDelete.Attributes.Add("onClick", "return func_AskUser()");
                imgActionOwner.Attributes["onclick"] = "popUpEmployeeSearch('" + e.Row.DataItemIndex + "');";
                //ddlActionStatus.Attributes["onchange"] = "EnableActulaClosureDate('" + e.Row.DataItemIndex + "');";
                //txtActionOwner.Attributes["onblur"] = "readonlyTextBox('" + e.Row.DataItemIndex + "');";
                // txtActionOwner.Attributes["onkeyup"] = "readonlyTextBox('" + e.Row.DataItemIndex + "');";


                //txtActionOwner.ReadOnly = true; 



                TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");
                //TextBox txtRemarksReviewer = (TextBox)e.Row.FindControl("txtRemarksReviewer");


                //if (Session[SessionNames.FOURC_ACTION_DATA] != null)
                if (ViewState["dtActionData"] != null)
                {
                    //dtActionData = (DataTable)Session[SessionNames.FOURC_ACTION_DATA];
                    dtActionData = (DataTable)ViewState["dtActionData"];

                    LoadCType(sender, e);
                    //ddlCType.SelectedValue = dtActionData[

                    LoadActionStatus(sender, e);
                    //LoadActionOwner(sender, e);

                    //if (string.IsNullOrEmpty(hdActionId.Value))

                    if (!string.IsNullOrEmpty(dtActionData.Rows[grdRow][DbTableColumn.FourCType].ToString()))
                    {
                        ddlCType.SelectedValue = dtActionData.Rows[grdRow][DbTableColumn.FourCType].ToString();
                        LoadParameter(ddlCType.SelectedItem.Value, ddlParameter);
                    }

                    if (!string.IsNullOrEmpty(dtActionData.Rows[grdRow][DbTableColumn.FourCParameterType].ToString()))
                        ddlParameter.SelectedValue = dtActionData.Rows[grdRow][DbTableColumn.FourCParameterType].ToString();

                    //if (!string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "ActionComeFrom").ToString()) || (string.IsNullOrEmpty(DecryptQueryString("AllowToEdit").ToString())))
                    //if (!string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "ActionComeFrom").ToString()) || ((string.IsNullOrEmpty(hdSendForReviewEnable.Value)) || string.IsNullOrEmpty(hdSubmitratingEnable.Value)))
                    if (!string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "ActionComeFrom").ToString()) || ((string.IsNullOrEmpty(hdSendForReviewEnable.Value) && hdSendForReviewEnable.Value.ToString().Trim() == "False") || string.IsNullOrEmpty(hdSubmitratingEnable.Value) && hdSubmitratingEnable.Value.ToString().Trim() == "False"))
                    {
                        DateTime dtMonthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));

                        if (int.Parse(hdMonth.Value) == dtMonthYear.Month)
                        {
                            ddlCType.Enabled = false;
                            ddlParameter.Enabled = false;
                            //action can be edit
                            //txtAction.Enabled = false;
                            //txtDescription.Enabled = false;
                            //ucDatePickerTragetClosureDate.IsEnable = false;
                        }
                    }
                    else
                    {
                        ddlCType.Enabled = true;
                        ddlParameter.Enabled = true;
                        txtAction.Enabled = true;
                        txtDescription.Enabled = true;
                        //ucDatePickerTragetClosureDate.IsEnable = true;
                    }

                    if (!string.IsNullOrEmpty(dtActionData.Rows[grdRow][DbTableColumn.FourCActionStatus].ToString()))
                    {
                        if (!string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "ActionComeFrom").ToString()) && DataBinder.Eval(e.Row.DataItem, "ActionComeFrom").ToString().Trim() == ActComeFromPreviousMonth.Trim() && OpenStatusId == dtActionData.Rows[grdRow][DbTableColumn.FourCActionStatus].ToString().Trim())
                        {
                            ddlActionStatus.SelectedValue = CarryForwardId.Trim();
                        }
                        else
                        {
                            ddlActionStatus.SelectedValue = dtActionData.Rows[grdRow][DbTableColumn.FourCActionStatus].ToString();
                        }

                        ddlActionStatus.Attributes["onChange"] = "check('" + e.Row.DataItemIndex + "');";

                        if (ddlActionStatus.SelectedItem.Text == "Closed")
                        {
                            ucDatePickerActualClosureDate.IsEnable = true;
                            ucDatePickerActualClosureDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            ucDatePickerTragetClosureDate.IsEnable = false;
                        }
                        else
                        {
                            ucDatePickerActualClosureDate.Text = "";
                            ucDatePickerActualClosureDate.IsEnable = false;
                            ucDatePickerTragetClosureDate.IsEnable = true;
                        }
                        string statusVal = "";

                        if (ViewState["dtOldDBActionData"] != null)
                        {
                            oldDBActionData = (DataTable)ViewState["dtOldDBActionData"];
                            if (grdRow <= oldDBActionData.Rows.Count - 1)
                            {
                                statusVal = oldDBActionData.Rows[grdRow]["ActionStatus"].ToString();
                            }
                        }

                        //if action come from previous month then 
                        if (!string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "ActionComeFrom").ToString()) && DataBinder.Eval(e.Row.DataItem, "ActionComeFrom").ToString().Trim() == ActComeFromPreviousMonth.Trim())
                        {


                            //if comment then remove carry forward and open
                            if (ddlActionStatus.SelectedItem.Text == "Comment") // action is comment and from previous month then remove carry forward and open status.
                            {
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByValue(CarryForwardId.Trim()));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Open"));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText(CommonConstants.Achievement));
                            }
                            else if (ddlActionStatus.SelectedValue == CarryForwardId.Trim()) //if carry forward then remove open and comment.
                            {
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Open"));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Comment"));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText(CommonConstants.Achievement));
                            }
                            //if closed and orignal value comment then remove open and carry forward
                            //else if (ddlActionStatus.SelectedItem.Text == "Closed" && !string.IsNullOrEmpty(dtActionData.Rows[grdRow][DbTableColumn.FourCActionStatus].ToString()) && dtActionData.Rows[grdRow][DbTableColumn.FourCActionStatus].ToString().Trim() == CommentId.Trim())
                            else if (ddlActionStatus.SelectedItem.Text == "Closed" && statusVal == CarryForwardId.Trim())
                            {
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Open"));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Comment"));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText(CommonConstants.Achievement));
                            }
                            //if closed and orignal value comment then remove open and comment
                            //else if (ddlActionStatus.SelectedItem.Text == "Closed" && !string.IsNullOrEmpty(dtActionData.Rows[grdRow][DbTableColumn.FourCActionStatus].ToString()) && dtActionData.Rows[grdRow][DbTableColumn.FourCActionStatus].ToString().Trim() == CarryForwardId.Trim())
                            else if (ddlActionStatus.SelectedItem.Text == "Closed" && statusVal == CommentId.Trim())
                            {
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Open"));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText(CommonConstants.Achievement));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByValue(CarryForwardId.Trim()));
                            }
                            //Ishwar 15022016 Desc : added new status(Achievement) --- Start
                            else if (ddlActionStatus.SelectedItem.Text == CommonConstants.Achievement) 
                            {
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByValue(CarryForwardId.Trim()));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Open"));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Comment"));
                            }
                            //Ishwar 15022016 Desc : added new status(Achievement) --- End
                        }
                        else
                        {

                            if (ViewState["dtOldDBActionData"] != null)
                            {

                                oldDBActionData = (DataTable)ViewState["dtOldDBActionData"];
                                if (hdActionId.Value.ToString().Trim() != "0")
                                {
                                    DataColumn[] prmk = new DataColumn[1];
                                    prmk[0] = oldDBActionData.Columns["FBAId"];
                                    oldDBActionData.PrimaryKey = prmk;

                                    //int rowSelected = int.Parse(grdEmpActionDetails.SelectedDataKey.Value.ToString());
                                    int rowSelected = 0;
                                    DataRow[] result = null;
                                    //result = oldDBActionData.Rows.Find("FBAId =" + hdActionId.Value.ToString().Trim() +"");
                                    if (ViewState["SelectedRow"] != null)
                                    {
                                        //result = oldDBActionData.Select("FBAId =" + ViewState["SelectedRow"].ToString().Trim() + "");

                                        if (oldDBActionData != null && oldDBActionData.Rows.Count > 0)
                                        {
                                            DataView dv = new DataView(oldDBActionData);
                                            //dv.RowFilter = "FBAId =" + ViewState["SelectedRow"].ToString().Trim() + "";
                                            dv.RowFilter = "FBAId =" + hdActionId.Value.ToString().Trim() + "";
                                            if (dv != null && dv.ToTable().Rows.Count > 0)
                                            {
                                                statusVal = dv.ToTable().Rows[0]["ActionStatus"].ToString();
                                            }
                                        }
                                    }
                                }
                            }

                            if (ddlActionStatus.SelectedItem.Text == "Open" && hdActionId.Value.ToString().Trim() == "0") //if new action then remove closed and carry forward
                            {
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Closed"));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByValue(CarryForwardId.Trim()));
                            }
                            else if (ddlActionStatus.SelectedItem.Text == "Open" && hdActionId.Value.ToString().Trim() != "0") //if new action then remove closed and carry forward
                            {
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Closed"));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByValue(CarryForwardId.Trim()));
                            }
                            else if (ddlActionStatus.SelectedValue == CarryForwardId.Trim()) //if carry forward then remove open and comment.
                            {
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Open"));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Comment"));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText(CommonConstants.Achievement));
                            }
                            else if (ddlActionStatus.SelectedItem.Text == "Comment" 
                                && hdActionId.Value.ToString().Trim() != "0"
                                && DataBinder.Eval(e.Row.DataItem, "ParentFBAID").ToString() != CommonConstants.ZERO.ToString()) // action is comment and from previous month then remove carry forward and open status.
                            {
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByValue(CarryForwardId.Trim()));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Open"));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText(CommonConstants.Achievement));
                            }
                            else if (ddlActionStatus.SelectedItem.Text == "Comment"
                                && hdActionId.Value.ToString().Trim() != "0"
                                && DataBinder.Eval(e.Row.DataItem, "ParentFBAID").ToString() == CommonConstants.ZERO.ToString()) 
                            {
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByValue(CarryForwardId.Trim()));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Open"));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText(CommonConstants.Achievement));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Closed"));
                            }
                            else if (ddlActionStatus.SelectedItem.Text == "Comment"
                                && hdActionId.Value.ToString().Trim() == "0"
                                && DataBinder.Eval(e.Row.DataItem, "ParentFBAID").ToString() != CommonConstants.ZERO.ToString()) // action is comment and from previous month then remove carry forward and open status.
                            {
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByValue(CarryForwardId.Trim()));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Closed"));
                            }
                            else if (!String.IsNullOrEmpty(HfActionStatus.Value) && ddlActionStatus.SelectedItem.Text == "Closed" && statusVal == CarryForwardId.Trim())
                            {
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Open"));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Comment"));
                            }
                            else if (!String.IsNullOrEmpty(HfActionStatus.Value) && ddlActionStatus.SelectedItem.Text == "Closed" && statusVal == CommentId.Trim())
                            {
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Open"));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByValue(CarryForwardId.Trim()));
                            }
                            else if (!String.IsNullOrEmpty(HfActionStatus.Value) && ddlActionStatus.SelectedItem.Text == "Closed" && !string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "FBAID").ToString()) && DataBinder.Eval(e.Row.DataItem, "FBAID").ToString() != "0")
                            {
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Open"));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByValue(CarryForwardId.Trim()));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Comment"));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText(CommonConstants.Achievement));
                            }
                            else if (ddlActionStatus.SelectedItem.Text == CommonConstants.Achievement && hdActionId.Value.ToString().Trim() == "0")
                            {
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Closed"));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByValue(CarryForwardId.Trim()));
                            }
                            else if (ddlActionStatus.SelectedItem.Text == CommonConstants.Achievement && hdActionId.Value.ToString().Trim() != "0")
                            {
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByValue(CarryForwardId.Trim()));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Open"));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Comment"));
                                ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Closed"));
                            }
                        }
                    }

                    //DateTime 

                    //All carry forward action are in disable mode.
                    if (ddlActionStatus.SelectedValue == CarryForwardId.Trim() || ddlActionStatus.SelectedItem.Text == "Closed" || ddlActionStatus.SelectedItem.Text == "On Hold" || ddlActionStatus.SelectedItem.Text == "Comment")
                    {
                        if (ddlActionStatus.SelectedItem.Text == "Comment" && DataBinder.Eval(e.Row.DataItem, "ActionComeFrom").ToString().ToUpper() != ActComeFromPreviousMonth.Trim().ToUpper())
                        {
                            bool CommentCarryforward = false;

                            //if (ddlActionStatus.SelectedItem.Text == "Comment" && ddlCType.SelectedIndex > 0 && ddlParameter.SelectedIndex > 0 && (!string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "FBAID").ToString()) && DataBinder.Eval(e.Row.DataItem, "FBAID").ToString() != "0"))
                            if (!string.IsNullOrEmpty(dtActionData.Rows[grdRow][DbTableColumn.FourCActionCreatedDate].ToString()))
                            {
                                DateTime dtCarryForwardCommentDt = DateTime.Parse(ViewState["CarryForwardCommentDate"].ToString());
                                //DateTime dtCommentRaisedDt = DateTime.Parse(lblActionCreatedDate.Text);
                                DateTime dtCommentRaisedDt = DateTime.Parse(dtActionData.Rows[grdRow][DbTableColumn.FourCActionCreatedDate].ToString());

                                //compare action created 
                                if (dtCommentRaisedDt < dtCarryForwardCommentDt)
                                {
                                    CommentCarryforward = true;
                                }
                                else
                                {
                                    CommentCarryforward = false;

                                    //remove open and closed and carryforwarded action if comment save.
                                    if (!string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "FBAID").ToString()) && DataBinder.Eval(e.Row.DataItem, "FBAID").ToString() != "0")
                                    {
                                        //ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Closed"));
                                        ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByValue(CarryForwardId.Trim()));
                                        ddlActionStatus.Items.Remove(ddlActionStatus.Items.FindByText("Open"));
                                    }
                                }
                            }
                            //if (ddlActionStatus.SelectedItem.Text == "Comment" && ddlCType.SelectedIndex > 0 && ddlParameter.SelectedIndex > 0 && (!string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "FBAID").ToString()) && DataBinder.Eval(e.Row.DataItem, "FBAID").ToString() != "0"))
                            if (ddlActionStatus.SelectedItem.Text == "Comment" && CommentCarryforward)
                            {
                                ddlCType.Enabled = false;
                                ddlParameter.Enabled = false;
                            }
                            else
                            {
                                ddlCType.Enabled = true;
                                ddlParameter.Enabled = true;
                            }
                        }
                        else
                        {
                            ddlCType.Enabled = false;
                            ddlParameter.Enabled = false;

                            //txtAction.Enabled = false;
                            //txtDescription.Enabled = false;
                            //ucDatePickerTragetClosureDate.IsEnable = false;
                        }
                    }




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


                    //DateTime.Now.ToString("dd MMM yyyy")
                }
                //If Action present then do not show delete button
                //(btnSave.Enabled == false && ddlActionStatus.SelectedItem.Text != "Open")
                //if ((!string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "FBAID").ToString()) && DataBinder.Eval(e.Row.DataItem, "FBAID").ToString() != "0" 
                    //&& ddlActionStatus.SelectedItem.Text != "Open") || (!string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "ActionComeFrom").ToString())))
                
                //if ((!string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "FBAID").ToString())) && (DataBinder.Eval(e.Row.DataItem, "FBAID").ToString() != "0")
                //    && (Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ParentFBAID")) > Convert.ToInt32(CommonConstants.ZERO))
                //    )
                //{
                //    imgDelete.Visible = false;
                //}
                //else
                //{
                //    imgDelete.Visible = true;
                //}

                if ((Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ParentFBAID")) == 0)
                    && (DataBinder.Eval(e.Row.DataItem, "ActionComeFrom").ToString() == ""))
                {
                    imgDelete.Visible = true;
                }
                else
                {
                    imgDelete.Visible = false;
                }
                //else
                //{
                //    LoadCType(sender, e);
                //    LoadActionStatus(sender, e);
                //    LoadActionOwner(sender, e);

                //    lblActionCreatedDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                //}

                if ((!string.IsNullOrEmpty(hdSendForReviewEnable.Value) && hdSendForReviewEnable.Value == "False") || (!string.IsNullOrEmpty(hdSubmitratingEnable.Value) && hdSubmitratingEnable.Value == "False"))
                {
                    makeReadOnly(this.Page, false);
                }

                //Ishwar 07072015 Start
                if (!string.IsNullOrEmpty(DecryptQueryString("Mode")) && DecryptQueryString("Mode").ToString().Trim() == "1")
                {
                    if (Convert.ToString(dtActionData.Rows[grdRow][DbTableColumn.FourCDescription]).Replace("\r", "").Replace("\n", "") !=
                        Convert.ToString(dtActionData.Rows[grdRow][DbTableColumn.LastMonthDescription]).Replace("\r", "").Replace("\n", ""))
                    {
                        e.Row.Cells[3].Attributes.Add("style", "border: solid 3px Blue;");
                        e.Row.Cells[3].Font.Bold = true;
                    }
                    if (Convert.ToString(dtActionData.Rows[grdRow][DbTableColumn.FourCAction]).Replace("\r", "").Replace("\n", "")
                        != Convert.ToString(dtActionData.Rows[grdRow][DbTableColumn.LastMonthAction]).Replace("\r", "").Replace("\n", ""))
                    {
                        e.Row.Cells[4].Attributes.Add("style", "border: solid 3px Blue;");
                        e.Row.Cells[4].Font.Bold = true;
                    }
                    if (Convert.ToString(dtActionData.Rows[grdRow][DbTableColumn.FourCActionOwnerId]) != Convert.ToString(CommonConstants.ZERO))
                    {
                        if (Convert.ToString(dtActionData.Rows[grdRow][DbTableColumn.FourCActionOwnerId]) != Convert.ToString(dtActionData.Rows[grdRow][DbTableColumn.LastMonthActionOwnerId]))
                        {
                            e.Row.Cells[5].Attributes.Add("style", "border: solid 3px Blue;");
                            e.Row.Cells[5].Font.Bold = true;
                        }
                    }
                    if (Convert.ToString(dtActionData.Rows[grdRow][DbTableColumn.FourCTargetClosureDate]) != Convert.ToString(dtActionData.Rows[grdRow][DbTableColumn.LastMonthTargetClosureDate]))
                    {
                        e.Row.Cells[7].Attributes.Add("style", "border: solid 3px Blue;");
                        e.Row.Cells[7].Font.Bold = true;
                    }
                    if (Convert.ToString(dtActionData.Rows[grdRow][DbTableColumn.FourCActualClosureDate]) != Convert.ToString(dtActionData.Rows[grdRow][DbTableColumn.LastMonthActualClosuredate]))
                    {
                        e.Row.Cells[8].Attributes.Add("style", "border: solid 3px Blue;");
                        e.Row.Cells[8].Font.Bold = true;
                    }
                    if (Convert.ToString(dtActionData.Rows[grdRow][DbTableColumn.FourCRemarks]).Replace("\r", "").Replace("\n", "")
                        != Convert.ToString(dtActionData.Rows[grdRow][DbTableColumn.LastMonthRemarks]).Replace("\r", "").Replace("\n", ""))
                    {
                        e.Row.Cells[9].Attributes.Add("style", "border: solid 3px Blue;");
                        e.Row.Cells[9].Font.Bold = true;
                    }
                    if (Convert.ToString(dtActionData.Rows[grdRow][DbTableColumn.FourCType]) != Convert.ToString(CommonConstants.ZERO)
                        && Convert.ToString(dtActionData.Rows[grdRow][DbTableColumn.FourCParameterType]) != Convert.ToString(CommonConstants.ZERO))
                    {
                        if ((Convert.ToString(dtActionData.Rows[grdRow][DbTableColumn.FourCActionStatus]) == CommonConstants.FOURCACTIONSTATUSCARRYFORWARD)
                            && (Convert.ToString(dtActionData.Rows[grdRow][DbTableColumn.LastMonthActionStatus]) == CommonConstants.FOURCACTIONSTATUSOPEN))
                        { }
                        else
                        {
                            if (Convert.ToString(dtActionData.Rows[grdRow][DbTableColumn.FourCActionStatus])
                                != Convert.ToString(dtActionData.Rows[grdRow][DbTableColumn.LastMonthActionStatus]))
                            {
                                e.Row.Cells[10].Attributes.Add("style", "border: solid 3px Blue;");
                                e.Row.Cells[10].Font.Bold = true;
                            }
                        }
                    }
                }
                //Ishwar 07072015 End
            }




            //Hide serial no column
            e.Row.Cells[0].Visible = false;

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdEmpActionDetails_RowDataBound", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }

    }

    protected void ddlCType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            UpdateDataTable();

            grdEmpActionDetails.DataSource = dtActionData;
            grdEmpActionDetails.DataBind();

            GridViewRow clickedRow = ((DropDownList)sender).NamingContainer as GridViewRow;
            DropDownList ddlCType = (DropDownList)clickedRow.FindControl("ddlCType");
            DropDownList ddlParameter = (DropDownList)clickedRow.FindControl("ddlCParameter");
            LoadParameter(ddlCType.SelectedItem.Value, ddlParameter);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlCType_SelectedIndexChanged", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    protected void ddlActionStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            GridViewRow clickedRow = ((DropDownList)sender).NamingContainer as GridViewRow;
            DropDownList ddlActionStatus = (DropDownList)clickedRow.FindControl("ddlActionStatus");
            HiddenField hdActionId = (HiddenField)clickedRow.FindControl("hdActionId");

            ViewState["SelectedRow"] = hdActionId.Value;

            UpdateDataTable();

            grdEmpActionDetails.DataSource = dtActionData;
            grdEmpActionDetails.DataBind();

            upDetails.Update();

            UIControls_DatePickerControl ucDatePickerActualClosureDate = (UIControls_DatePickerControl)clickedRow.FindControl("ucDatePickerActualClosureDate");
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlActionStatus_SelectedIndexChanged", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }

    }


    protected void ddlProjectList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProjectList.SelectedIndex == 0)
            {
                ddlProjectList.SelectedValue = hdProjectId.Value;
            }

            if (ddlProjectList.SelectedIndex > 0)
            {
                //Session[SessionNames.FOURC_ACTION_DATA] = null;
                ViewState["dtActionData"] = null;
                hdProjectId.Value = ddlProjectList.SelectedItem.Value;
                DateTime dtMonthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));
                BindActionData(int.Parse(hdProjectId.Value), dtMonthYear.Month, dtMonthYear.Year, ViewState["FourCRole"].ToString());
                //IsAllowToFillActionData(int.Parse(hdProjectId.Value), UserMailId);
            }

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlProjectList_SelectedIndexChanged", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }

    }


    protected void imgDelete_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            GridViewRow clickedRow = ((ImageButton)sender).NamingContainer as GridViewRow;
            HiddenField hdActionId = (HiddenField)clickedRow.FindControl("hdActionId");

            DropDownList ddlCType = (DropDownList)clickedRow.FindControl("ddlCType");
            DropDownList ddlCParameter = (DropDownList)clickedRow.FindControl("ddlCParameter");

            Label lblRowId = (Label)clickedRow.FindControl("lblRowId");
            int grdRow = int.Parse(lblRowId.Text);


            if (grdEmpActionDetails.Rows.Count == 1 && ddlCType.SelectedIndex == 0 && ddlCParameter.SelectedIndex == 0)
            {
                Page page = HttpContext.Current.Handler as Page;
                ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Cannot delete row.!!!');", true);
                return;
            }

            //if (string.IsNullOrEmpty(hdActionId.Value))
            //{
            UpdateDataTable();

            //if (Session[SessionNames.FOURC_ACTION_DATA] != null)
            if (ViewState["dtActionData"] != null)
            {


                //dtActionDataDelete.ImportRow(dtActionData.Rows[grdRow].ItemArray);
                if (!string.IsNullOrEmpty(hdActionId.Value) && (int.Parse(hdActionId.Value) > 0))
                {
                    dtActionDataDelete = dtActionData.Clone();

                    if (Session[SessionNames.FOURC_ACTION_DeletedData] != null)
                    {
                        dtActionDataDelete = (DataTable)Session[SessionNames.FOURC_ACTION_DeletedData];
                    }

                    dtActionDataDelete.Rows.Add(dtActionData.Rows[grdRow].ItemArray);

                    Session[SessionNames.FOURC_ACTION_DeletedData] = dtActionDataDelete;
                }

                dtActionData.Rows.RemoveAt(grdRow);

                //if (!string.IsNullOrEmpty(hdActionId.Value) && (int.Parse(hdActionId.Value) > 0))
                //{
                //    dtActionData.Rows[int.Parse(lblRowId.Text)]["ActionDML"] = "DELETE";
                //    //Update the data table as row is already saved in database
                //    //dtActionData.Rows[0]["AllowAccess"] 
                //}
                //else
                //{
                //    dtActionData.Rows.RemoveAt(grdRow);
                //}


                if (grdEmpActionDetails.Rows.Count == 1 && ddlCType.SelectedIndex > 0 && ddlCParameter.SelectedIndex > 0)
                {
                    dtActionData = AddNewRow(dtActionData);
                }

                DataView dv = new DataView(dtActionData);
                dv.RowFilter = "ActionDML IN ('UPDATE','INSERT')";

                //DataTable dtTemp = null;
                //DataTable dtTemp = dtActionData.Clone();   
                //foreach (DataRow dr in dtActionData.Select("ActionDML = 'UPDATE' OR ActionDML = 'INSERT'"))
                //{
                //    //DataRow drNew = new DataRow();
                //    DataRow drNew = dtTemp.NewRow();
                //    drNew[0] = dr[0];
                //    drNew[1] = dr[1];
                //    drNew[2] = dr[2];
                //    drNew[3] = dr[3];
                //    drNew[4] = dr[4];
                //    drNew[5] = dr[5];
                //    drNew[6] = dr[6];
                //    drNew[7] = dr[7];
                //    drNew[8] = dr[8];
                //    drNew[9] = dr[9];
                //    drNew[10] = dr[10];
                //    drNew[11] = dr[11];
                //    drNew[12] = dr[12];
                //    drNew[13] = dr[13];
                //    drNew[14] = dr[14];
                //    drNew[15] = dr[15];
                //    drNew[16] = dr[16];
                //    dtTemp.Rows.Add(drNew);
                //}
                //DataTable dtTemp = dtActionData.Select("ActionDML = 'UPDATE' OR ActionDML = 'INSERT'");

                grdEmpActionDetails.DataSource = dv;
                grdEmpActionDetails.DataBind();

                //Session[SessionNames.FOURC_ACTION_DATA] = dtActionData;
                ViewState["dtActionData"] = dtActionData;
            }
            //}
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlProjectList_SelectedIndexChanged", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }


    private void IsAllowToFillActionData(int projectId, string userMailID)
    {
        try
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
                    // && (((!string.IsNullOrEmpty(hdSendForReviewEnable.Value) && hdSendForReviewEnable.Value != "False") || (!string.IsNullOrEmpty(hdSubmitratingEnable.Value) && hdSubmitratingEnable.Value != "False")) || (string.IsNullOrEmpty(hdSendForReviewEnable.Value) && string.IsNullOrEmpty(hdSubmitratingEnable.Value)))
                     && (((string.IsNullOrEmpty(hdSendForReviewEnable.Value)) || (string.IsNullOrEmpty(hdSubmitratingEnable.Value))))
                    )
                {
                    //btnSave.Enabled = true;
                    makeReadOnly(this.Page, true);
                }
                else
                {
                    //btnSave.Enabled = false;
                    makeReadOnly(this.Page, false);

                    if (ddlDepartment.SelectedItem.Text == "Projects")
                        ddlProjectList.Enabled = true;

                    //btnCancel.Enabled = true;
                }
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlProjectList_SelectedIndexChanged", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
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
                if (((DropDownList)childControl).ID.ToString() != "ddlDepartment")
                    ((DropDownList)childControl).Enabled = flag;
            }
            if (childControl.GetType() == typeof(Button))
            {
                if (((Button)childControl).ID.ToString() != "btnCancel")
                {
                    if (((Button)childControl).ID.ToString() != "btnDashboardHistory" && ((Button)childControl).ID.ToString() != "btnClose")
                    {
                        ((Button)childControl).Enabled = flag;
                    }
                }
            }

            if (childControl.GetType() == typeof(ImageButton))
            {
                if (((ImageButton)childControl).ID.ToString() == "imgDelete")
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
        try
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
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "LoadParameter", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }




    private void LoadCType(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DropDownList ddlCType = (DropDownList)e.Row.FindControl("ddlCType");
            DropDownList ddlParameter = (DropDownList)e.Row.FindControl("ddlCParameter");

            List<BusinessEntities.Master> objCTypeStatus = new List<BusinessEntities.Master>();
            objCTypeStatus = FillMasterData(CTypeId);

            ddlCType.DataSource = objCTypeStatus.OrderBy(o => o.MasterId);
            ddlCType.DataTextField = MasterName;
            ddlCType.DataValueField = MasterID;
            ddlCType.DataBind();
            ddlCType.Items.Insert(0, new ListItem(SELECTONE, ZERO));

            ddlParameter.Items.Insert(0, new ListItem(SELECTONE, ZERO));
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

    private void LoadActionStatus(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DropDownList ddlActionStatus = (DropDownList)e.Row.FindControl("ddlActionStatus");


            List<BusinessEntities.Master> objCTypeStatus = new List<BusinessEntities.Master>();
            objCTypeStatus = FillMasterData(CActionStatusId);

            ddlActionStatus.DataSource = objCTypeStatus;
            ddlActionStatus.DataTextField = MasterName;
            ddlActionStatus.DataValueField = MasterID;
            ddlActionStatus.DataBind();
            //ddlActionStatus.Items.Insert(0, new ListItem(SELECTONE, ZERO));

            ddlActionStatus.SelectedValue = OpenStatusId;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "LoadActionStatus", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
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
        try
        {
            UpdateDataTable();

            dtActionData = AddNewRow(dtActionData);

            grdEmpActionDetails.DataSource = dtActionData;
            grdEmpActionDetails.DataBind();

            //Session[SessionNames.FOURC_ACTION_DATA] = dtActionData;
            //ViewState["dtActionData"] = dtActionData; 
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnAddNewRow_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    private void UpdateDataTable()
    {
        try
        {
            if (ViewState["dtActionData"] != null)
            {
                dtActionData = (DataTable)ViewState["dtActionData"];
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
                HiddenField HfActionOwnerName = (HiddenField)gvRow.FindControl("HfActionOwnerName");
                Label lblActionCreatedDate = (Label)gvRow.FindControl("lblActionCreatedDate");
                TextBox txtRemarks = (TextBox)gvRow.FindControl("txtRemarks");
                DropDownList ddlActionStatus = (DropDownList)gvRow.FindControl("ddlActionStatus");

                HiddenField hdActionFrom = (HiddenField)gvRow.FindControl("hdActionFrom");
                HiddenField hdParentFBAID = (HiddenField)gvRow.FindControl("hdParentFBAID");
                
                UIControls_DatePickerControl ucDatePickerTragetClosureDate = (UIControls_DatePickerControl)gvRow.FindControl("ucDatePickerTragetClosureDate");
                UIControls_DatePickerControl ucDatePickerActualClosureDate = (UIControls_DatePickerControl)gvRow.FindControl("ucDatePickerActualClosureDate");
        
                //if (string.IsNullOrEmpty(hdActionId.Value))
                //{
                dtActionData.Rows[grdRow][DbTableColumn.FourCROWId] = grdRow;

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

                //dtActionData.Rows[grdRow][DbTableColumn.FourCActionOwner] = txtActionOwner.Text;
                dtActionData.Rows[grdRow][DbTableColumn.FourCActionOwner] = HfActionOwnerName.Value;

                dtActionData.Rows[grdRow][DbTableColumn.FourCActionOwnerId] = hdActionOwnerId.Value;

                dtActionData.Rows[grdRow][DbTableColumn.FourCActionCreatedDate] = lblActionCreatedDate.Text;

                if (string.IsNullOrEmpty(ucDatePickerTragetClosureDate.Text))
                    dtActionData.Rows[grdRow][DbTableColumn.FourCTargetClosureDate] = DBNull.Value;
                else
                    dtActionData.Rows[grdRow][DbTableColumn.FourCTargetClosureDate] = ucDatePickerTragetClosureDate.Text;

                if (string.IsNullOrEmpty(ucDatePickerActualClosureDate.Text))
                    dtActionData.Rows[grdRow][DbTableColumn.FourCActualClosureDate] = DBNull.Value;
                else
                    dtActionData.Rows[grdRow][DbTableColumn.FourCActualClosureDate] = ucDatePickerActualClosureDate.Text;

                dtActionData.Rows[grdRow][DbTableColumn.FourCRemarks] = txtRemarks.Text;

                dtActionData.Rows[grdRow][DbTableColumn.FourCActionStatus] = ddlActionStatus.SelectedItem.Value;

                dtActionData.Rows[grdRow][DbTableColumn.ParentFBAID] = hdParentFBAID.Value;

                dtActionData.Rows[grdRow][DbTableColumn.ActionComeFrom] = hdActionFrom.Value;
                //}

            }

            //Session[SessionNames.FOURC_ACTION_DATA] = dtActionData;
            ViewState["dtActionData"] = dtActionData;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "UpdateDataTable", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            UpdateDataTable();

            grdEmpActionDetails.DataSource = dtActionData;
            grdEmpActionDetails.DataBind();


            bool flag = false;

            DateTime dtMonthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));



            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();


            int countCompetency = 0, countCommunication = 0, countCommitment = 0, countCollaboration = 0, countCompetencyAchievement = 0, countCommunicationAchievement = 0, countCommitmentAchievement = 0, countCollaborationAchievement = 0;

            string valResult = "";
            bool minValReq = false;
            bool minAtLeastOneRow = false;
            bool closedvalidation = false;
            bool carryforwardValidation = false;
            //bool closedValidation_First = false;
            bool actionStatusValidation = false;
            bool duplicateAction = false;
            bool TragetClosureDate = false;

            //Ishwar Patil 08092015 Start
            bool duplicationActionCommand = false;
            //Ishwar Patil 08092015 End
            bool ValAchievement = false;

            DataTable dtDuplicateAction = new DataTable();
            dtDuplicateAction.Columns.Add(DbTableColumn.FourCType);
            dtDuplicateAction.Columns.Add(DbTableColumn.FourCParameterType);
            //dtDuplicateAction.Columns.Add(DbTableColumn.FourCActionOwner);
            dtDuplicateAction.Columns.Add(DbTableColumn.FourCActionOwnerId);

            //Ishwar Patil 01092015 Start
            dtDuplicateAction.Columns.Add(DbTableColumn.FourCActionStatus);
            //Ishwar Patil 01092015 End


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

                if (ddlActionStatus.SelectedItem.Text == CommonConstants.Achievement && ddlCType.SelectedItem.Text.Trim() == Competency)
                {
                    countCompetencyAchievement = countCompetencyAchievement + 1;
                }
                if (ddlActionStatus.SelectedItem.Text == CommonConstants.Achievement && ddlCType.SelectedItem.Text.Trim() == Communication)
                {
                    countCommunicationAchievement = countCommunicationAchievement + 1;
                }
                if (ddlActionStatus.SelectedItem.Text == CommonConstants.Achievement && ddlCType.SelectedItem.Text.Trim() == Commitment)
                {
                    countCommitmentAchievement = countCommitmentAchievement + 1;
                }
                if (ddlActionStatus.SelectedItem.Text == CommonConstants.Achievement && ddlCType.SelectedItem.Text.Trim() == Collaboration)
                {
                    countCollaborationAchievement = countCollaborationAchievement + 1;
                }

                //gvRow.Row.Cells[1].ForeColor = System.Drawing.Color.Red; 

                //gvRow.BorderColor = System.Drawing.Color.Red;

                Unit borderWidth = Unit.Pixel(2);
                Unit zeroBorderWidth = Unit.Pixel(0);

                if ((ddlCType.SelectedIndex > 0 || ddlParameter.SelectedIndex > 0 || !string.IsNullOrEmpty(txtAction.Text)) && (ddlActionStatus.SelectedItem.Text != CommentActionStatus && ddlActionStatus.SelectedItem.Text != CommonConstants.Achievement))
                {
                    DataRow dr = dtDuplicateAction.NewRow();

                    dr[DbTableColumn.FourCType] = ddlCType.SelectedItem.Value;
                    dr[DbTableColumn.FourCParameterType] = ddlParameter.SelectedItem.Value;
                    dr[DbTableColumn.FourCActionOwnerId] = hdActionOwnerId.Value;

                    //Ishwar Patil 01092015 Start
                    dr[DbTableColumn.FourCActionStatus] = ddlActionStatus.SelectedItem.Value;
                    //Ishwar Patil 01092015 End

                    dtDuplicateAction.Rows.Add(dr);

                    //int grdRow = int.Parse(lblRowId.Text);
                    string statusVal = "";
                    if (ViewState["dtOldDBActionData"] != null)
                    {
                        oldDBActionData = (DataTable)ViewState["dtOldDBActionData"];
                        if (grdRow <= oldDBActionData.Rows.Count - 1)
                        {
                            statusVal = oldDBActionData.Rows[grdRow]["ActionStatus"].ToString();
                        }
                    }

                    if (string.IsNullOrEmpty(hdActionFrom.Value) && ddlActionStatus.SelectedItem.Text == "Closed" && statusVal == CommentId.Trim())
                    {
                        //if (ddlActionStatus.SelectedItem.Text == "Closed" && string.IsNullOrEmpty(ucDatePickerActualClosureDate.Text))
                        if (ddlActionStatus.SelectedItem.Text == "Closed" && string.IsNullOrEmpty(dtActionData.Rows[grdRow]["ActualClosureDate"].ToString()))
                        {
                            //if (string.IsNullOrEmpty(minValReq))
                            //    minValReq = "Please enter Actual Closure date.";
                            //else
                            //    minValReq = minValReq + "<br>" + "Please enter Actual Closure date.";
                            ucDatePickerActualClosureDate.Text = "";
                            minValReq = true;
                            gvRow.Cells[8].BorderColor = System.Drawing.Color.Red;
                            gvRow.Cells[8].BorderWidth = borderWidth;
                        }
                        else
                        {
                            gvRow.Cells[8].BorderColor = System.Drawing.Color.White;
                            gvRow.Cells[8].BorderWidth = zeroBorderWidth;
                        }
                    }
                    else
                    {
                        if (ddlActionStatus.SelectedItem.Text == "Closed")
                        {

                            //check the first what is action status
                            if (ViewState["dtOldDBActionData"] != null)
                            {
                                oldDBActionData = (DataTable)ViewState["dtOldDBActionData"];
                                if (grdRow <= oldDBActionData.Rows.Count - 1)
                                {
                                    statusVal = oldDBActionData.Rows[grdRow]["ActionStatus"].ToString();
                                }
                            }

                            //this is first time if carry-forawrd action set to close then req validation
                            if (statusVal == CarryForwardId.Trim())
                            {
                                if (string.IsNullOrEmpty(txtRemarks.Text))
                                {
                                    minValReq = true;
                                    gvRow.Cells[9].BorderColor = System.Drawing.Color.Red;
                                    gvRow.Cells[9].BorderWidth = borderWidth;
                                }
                                else
                                {
                                    gvRow.Cells[9].BorderColor = System.Drawing.Color.White;
                                    gvRow.Cells[9].BorderWidth = zeroBorderWidth;
                                }


                                if (string.IsNullOrEmpty(ucDatePickerActualClosureDate.Text))
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

                                if (!string.IsNullOrEmpty(ucDatePickerActualClosureDate.Text) && (Convert.ToDateTime(ucDatePickerActualClosureDate.Text.ToString()) > DateTime.Now))
                                {
                                    closedvalidation = true;
                                    gvRow.Cells[8].BorderColor = System.Drawing.Color.Red;
                                    gvRow.Cells[8].BorderWidth = borderWidth;
                                }

                            }
                            //end


                        }
                        else
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
                            }
                            else
                            {
                                gvRow.Cells[2].BorderColor = System.Drawing.Color.White;
                                gvRow.Cells[2].BorderWidth = zeroBorderWidth;
                            }

                            if (string.IsNullOrEmpty(txtAction.Text))
                            {
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
                                minValReq = true;
                                gvRow.Cells[5].BorderColor = System.Drawing.Color.Red;
                                gvRow.Cells[5].BorderWidth = borderWidth;
                            }
                            else
                            {
                                gvRow.Cells[5].BorderColor = System.Drawing.Color.White;
                                gvRow.Cells[5].BorderWidth = zeroBorderWidth;
                            }

                            if (string.IsNullOrEmpty(ucDatePickerTragetClosureDate.Text))
                            {
                                minValReq = true;
                                gvRow.Cells[7].BorderColor = System.Drawing.Color.Red;
                                gvRow.Cells[7].BorderWidth = borderWidth;
                            }
                            else
                            {
                                gvRow.Cells[7].BorderColor = System.Drawing.Color.White;
                                gvRow.Cells[7].BorderWidth = zeroBorderWidth;
                            }
                            if (ddlActionStatus.SelectedItem.Text == SELECTONE)
                            {
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
                    }
                    //Ishwar Patil 09092015 Start
                    if (!string.IsNullOrEmpty(ucDatePickerTragetClosureDate.Text) && (Convert.ToDateTime(ucDatePickerTragetClosureDate.Text.ToString()) < DateTime.Now)
                        && ddlActionStatus.SelectedItem.Text != "Closed")
                    {
                        TragetClosureDate = true;
                        gvRow.Cells[7].BorderColor = System.Drawing.Color.Red;
                        gvRow.Cells[7].BorderWidth = borderWidth;
                        break;
                    }
                  
                    //Ishwar Patil 09092015 End
                }

                //comment Action status validation
                if (ddlActionStatus.SelectedItem.Text == CommentActionStatus || ddlActionStatus.SelectedItem.Text==CommonConstants.Achievement)
                {
                    //Ishwar Patil 08092015 Start
                    DataRow dr = dtDuplicateAction.NewRow();
                    dr[DbTableColumn.FourCType] = ddlCType.SelectedItem.Value;
                    dr[DbTableColumn.FourCParameterType] = ddlParameter.SelectedItem.Value;
                    dr[DbTableColumn.FourCActionOwnerId] = String.IsNullOrEmpty(hdActionOwnerId.Value) ? 0 : Convert.ToInt32(hdActionOwnerId.Value);
                    dr[DbTableColumn.FourCActionStatus] = ddlActionStatus.SelectedItem.Value;

                    dtDuplicateAction.Rows.Add(dr);
                    //Ishwar Patil 08092015 End

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
                    }
                    else
                    {
                        gvRow.Cells[2].BorderColor = System.Drawing.Color.White;
                        gvRow.Cells[2].BorderWidth = zeroBorderWidth;
                    }
                    //Ishwar Patil 09092015 Start
                    if (!string.IsNullOrEmpty(ucDatePickerTragetClosureDate.Text))
                    {
                        if (Convert.ToDateTime(ucDatePickerTragetClosureDate.Text.ToString()) < DateTime.Now)
                        {
                            TragetClosureDate = true;
                            gvRow.Cells[7].BorderColor = System.Drawing.Color.Red;
                            gvRow.Cells[7].BorderWidth = borderWidth;
                            break;
                        }
                    }
                    //Ishwar Patil 09092015 End
                }


                if (ddlCType.SelectedIndex == 0 && ddlParameter.SelectedIndex == 0 && dtActionData.Rows.Count == 1)
                {
                    minAtLeastOneRow = true;
                }

            }

            //same C, parameter, and action owner not allowed validation
            if (dtDuplicateAction != null && dtDuplicateAction.Rows.Count > 1)
            {

                DataTable dtTable = new DataTable();
                dtTable = dtDuplicateAction.Clone();
                //bool chkDuplicateAction = false;
                foreach (DataRow row in dtDuplicateAction.Rows)
                {
                    if (dtTable.Rows.Count == 0)
                    {
                        dtTable.Rows.Add(dtDuplicateAction.Rows[0].ItemArray);
                        //dtTable.Rows.Add(row);
                    }
                    else
                    {
                        foreach (DataRow r in dtTable.Rows)
                        {
                            //if (row.ItemArray[0].ToString() == r.ItemArray[0].ToString() && 
                            //    row.ItemArray[1].ToString() == r.ItemArray[1].ToString() &&
                            //    row.ItemArray[2].ToString() == r.ItemArray[2].ToString())

                            //Ishwar Patil 08092015 Start DESC: for "Comment" action status validation
                            if (row.ItemArray[3].ToString() == CommonConstants.FOURCACTIONSTATUSCOMMENT || r.ItemArray[3].ToString() == CommonConstants.FOURCACTIONSTATUSCOMMENT)
                            {
                                if (row.ItemArray[0].ToString() == r.ItemArray[0].ToString() &&
                                    row.ItemArray[1].ToString() == r.ItemArray[1].ToString() &&
                                    row.ItemArray[3].ToString() == r.ItemArray[3].ToString())
                                {
                                    duplicationActionCommand = true;
                                    break;
                                }
                            }
                            else if (row.ItemArray[0].ToString() == r.ItemArray[0].ToString() &&
                                row.ItemArray[1].ToString() == r.ItemArray[1].ToString() &&
                                row.ItemArray[2].ToString() == r.ItemArray[2].ToString() &&
                                row.ItemArray[3].ToString() == r.ItemArray[3].ToString())
                            {
                                duplicateAction = true;
                                break;
                            }
                            //Ishwar Patil 08092015 End
                        }
                        dtTable.Rows.Add(row.ItemArray);
                    }
                }
            }



            if ((!string.IsNullOrEmpty(DecryptQueryString("Mode")) && DecryptQueryString("Mode").ToString().Trim() != "0") &&
                 (ViewState["CompetencyRatingByCreator"].ToString().Trim() != ddlRAGCompetency.SelectedItem.Value.ToString().Trim() ||
                 ViewState["CommunicationRatingByCreator"].ToString().Trim() != ddlRAGCommunication.SelectedItem.Value.ToString().Trim() ||
                 ViewState["CommitmentRatingByCreator"].ToString().Trim() != ddlRAGCommitment.SelectedItem.Value.ToString().Trim() ||
                 ViewState["CollaborationRatingByCreator"].ToString().Trim() != ddlRAGCollaboration.SelectedItem.Value.ToString().Trim()))
            {
                //tdReviewerRemarks.BorderColor = System.Drawing.Color.Red;
                if (string.IsNullOrEmpty(txtRemarksReviewer.Text))
                {
                    txtRemarksReviewer.BorderColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    txtRemarksReviewer.BorderColor = System.Drawing.Color.White;
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

            string strVal = "";

            if ((ddlRAGCompetency.SelectedItem.Text == "Red" || ddlRAGCompetency.SelectedItem.Text == "Amber") && countCompetency == 0)
            {
                strVal = Competency;
            }
            if ((ddlRAGCommunication.SelectedItem.Text == "Red" || ddlRAGCommunication.SelectedItem.Text == "Amber") && countCommunication == 0)
            {
                if (string.IsNullOrEmpty(strVal))
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

            if ((ddlRAGCompetency.SelectedItem.Text == "Red" || ddlRAGCompetency.SelectedItem.Text == "Amber") && (countCompetencyAchievement != 0))
            {
                ValAchievement = true;
            }
            if ((ddlRAGCommunication.SelectedItem.Text == "Red" || ddlRAGCommunication.SelectedItem.Text == "Amber") && (countCommunicationAchievement != 0))
            {
                ValAchievement = true;
            }
            if ((ddlRAGCommitment.SelectedItem.Text == "Red" || ddlRAGCommitment.SelectedItem.Text == "Amber") && (countCommitmentAchievement != 0))
            {
                ValAchievement = true;
            }
            if ((ddlRAGCollaboration.SelectedItem.Text == "Red" || ddlRAGCollaboration.SelectedItem.Text == "Amber") && (countCollaborationAchievement != 0))
            {
                ValAchievement = true;
            }

            if (!string.IsNullOrEmpty(strVal) || minValReq || minAtLeastOneRow || closedvalidation || carryforwardValidation || actionStatusValidation || duplicateAction || duplicationActionCommand || TragetClosureDate || ValAchievement)// || closedValidation_First)
            {
                lblMessage.Text = "";
                lblMessage.Visible = true;
                lblMessage.ForeColor = System.Drawing.Color.Red;
                if (!string.IsNullOrEmpty(strVal))
                    lblMessage.Text = "Please Add Action for " + strVal + " .";
                if (minValReq)
                    lblMessage.Text = lblMessage.Text + "<br />" + "Please enter relevant information in highlighted field/s.";
                if (minAtLeastOneRow)
                    lblMessage.Text = lblMessage.Text + "<br />" + "Please enter rating details.";
                if (closedvalidation)
                    lblMessage.Text = lblMessage.Text + "<br />" + "Please enter Actual Closure Date Less than or equal to current date.";
                if (carryforwardValidation)
                    lblMessage.Text = lblMessage.Text + "<br />" + "Action Status cannot be changed from carry forward to Open.";
                if (actionStatusValidation)
                    lblMessage.Text = lblMessage.Text + "<br />" + "Carry Forward, Closed action Status cannot be changed to Open.";
                if (duplicateAction)
                    lblMessage.Text = lblMessage.Text + "<br />" + "Duplicate Action with same 'C', 'Parameter' and 'Action Owner' cannot be created.";
                //Ishwar Patil 08092015 Start
                if (duplicationActionCommand)
                    lblMessage.Text = lblMessage.Text + "<br />" + "Duplicate Action with same 'C' and 'Parameter' cannot be created.";
                if (TragetClosureDate)
                    lblMessage.Text = lblMessage.Text + "<br />" + "Target Closure Date should be greater than today`s date.";
                if (ValAchievement)
                    lblMessage.Text = lblMessage.Text + "<br />" + "'Achievement' can be added only when a 4C parameter has been marked as 'Green'";

                //Ishwar Patil 08092015 End
                //if (closedValidation_First)
                //    lblMessage.Text = lblMessage.Text + "<br />" + "Please remove actual closure date."; 
                return;
            }


            objFeedBack = fourCBAL.Get4CIndividualEmployeeDeatils(int.Parse(ddlDepartment.SelectedItem.Value), int.Parse(ddlProjectList.SelectedItem.Value), dtMonthYear.Month, dtMonthYear.Year, int.Parse(hdEmpId.Value));

            if (ddlRAGCollaboration.SelectedIndex > 0)
                objFeedBack.Collaboration = ddlRAGCollaboration.SelectedItem.Value;
            else
                objFeedBack.Collaboration = null;

            if (ddlRAGCommitment.SelectedIndex > 0)
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

            if (!string.IsNullOrEmpty(DecryptQueryString("Mode")) && DecryptQueryString("Mode").ToString().Trim() == "1")  //0 Add 1 review
            {
                objFeedBack.ReviewerRemarks = txtRemarksReviewer.Text;
                objFeedBack.RatingOption = int.Parse(DecryptQueryString("Mode"));
            }
            else
            {
                objFeedBack.RatingOption = int.Parse(DecryptQueryString("Mode"));
            }


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


            //Update the delete row in dtActionData from dtActionDataDelete
            //if (Session[SessionNames.FOURC_ACTION_DATA] != null)
            if (ViewState["dtActionData"] != null)
            {
                dtActionDataDelete = (DataTable)Session[SessionNames.FOURC_ACTION_DeletedData];
            }

            if (dtActionDataDelete != null && dtActionDataDelete.Rows.Count > 0)
            {
                foreach (DataRow dr in dtActionDataDelete.Rows)
                {
                    dr["ActionDML"] = "DELETE";
                    dtActionData.Rows.Add(dr.ItemArray);
                }
                Session[SessionNames.FOURC_ACTION_DeletedData] = null;
                dtActionDataDelete = null;
            }

            flag = fourCBAL.InsertActionDetails(dtActionData, objFeedBack);

            if (flag)
            {
                //Session[SessionNames.FOURC_ACTION_DATA] = null;
                //Response.Write("<script>returnValue='" + ddlProjectList.SelectedItem.Value + "_" + hdMonth.Value + "_" + hdYear.Value + "';</script>");
                //Response.Write("<script language='javascript'>alert('Data Saved Successfully'); window.close();</script>");

                //Session[SessionNames.FOURC_ACTION_DATA] = null;
                //dtActionData = null;
                Session["HistoryMonth"] = null;

                Response.Redirect("4C_Add4CFeedback.aspx?" + URLHelper.SecureParameters("departmentId", DecryptQueryString("departmentId").ToString()) + "&" + URLHelper.SecureParameters("projectId", DecryptQueryString("projectId").ToString()) + "&"
                                                       + URLHelper.SecureParameters("month", dtMonthYear.Month.ToString()) + "&" + URLHelper.SecureParameters("year", dtMonthYear.Year.ToString()) + "&"
                                                       + URLHelper.SecureParameters("LoginEmailId", DecryptQueryString("LoginEmailId").ToString()) + "&"
                                                       + URLHelper.SecureParameters("Mode", DecryptQueryString("Mode").ToString()) + "&"
                                                       + URLHelper.CreateSignature(DecryptQueryString("projectId").ToString(), dtMonthYear.Month.ToString(), dtMonthYear.Year.ToString(), DecryptQueryString("LoginEmailId").ToString(), DecryptQueryString("Mode").ToString()));

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
        try
        {
            //Session[SessionNames.FOURC_ACTION_DATA] = null;
            ViewState["dtActionData"] = null;
            dtActionData = null;
            Session["HistoryMonth"] = null;
            //Response.Write("<script language='javascript'>window.close();</script>");

            DateTime dtMonthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));

            Response.Redirect("4C_Add4CFeedback.aspx?" + URLHelper.SecureParameters("departmentId", DecryptQueryString("departmentId").ToString()) + "&" + URLHelper.SecureParameters("projectId", DecryptQueryString("projectId").ToString()) + "&"
                                                           + URLHelper.SecureParameters("month", dtMonthYear.Month.ToString()) + "&" + URLHelper.SecureParameters("year", dtMonthYear.Year.ToString()) + "&"
                                                           + URLHelper.SecureParameters("LoginEmailId", DecryptQueryString("LoginEmailId").ToString()) + "&"
                                                           + URLHelper.SecureParameters("Mode", DecryptQueryString("Mode").ToString()) + "&"
                                                           + URLHelper.CreateSignature(DecryptQueryString("projectId").ToString(), dtMonthYear.Month.ToString(), dtMonthYear.Year.ToString(), DecryptQueryString("LoginEmailId").ToString(), DecryptQueryString("Mode").ToString())
            );
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            //throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnCancel_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    protected void btnDashboardHistory_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (Session["HistoryMonth"] != null)
            {
                if (int.Parse(Session["HistoryMonth"].ToString()) == 3)
                {
                    Session["HistoryMonth"] = 6;
                }
                else if (int.Parse(Session["HistoryMonth"].ToString()) == 6)
                {
                    Session["HistoryMonth"] = 9;
                }
                else if (int.Parse(Session["HistoryMonth"].ToString()) == 9)
                {
                    btnDashboardHistory.Enabled = false;
                    Session["HistoryMonth"] = 12;
                }
                else
                {
                    Session["HistoryMonth"] = 3;
                }
            }

            LoadDashboardData();
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnDashboardHistory_OnClick", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }

    }



    private void LoadDashboardData()
    {
        try
        {
            dsEmpDashboard = new DataSet();
            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();

            if (Session["HistoryMonth"] == null)
            {
                Session["HistoryMonth"] = 3;
            }

            dsEmpDashboard = fourCBAL.GetEmpDashboardData(int.Parse(hdEmpId.Value), int.Parse(Session["HistoryMonth"].ToString())); //0 Add 1 Review

            grdEmpDashboard.DataSource = dsEmpDashboard.Tables[0];
            grdEmpDashboard.DataBind();

            if (dsEmpDashboard.Tables[0].Rows.Count > 0)
            {
                btnDashboardHistory.Visible = true;
            }
            else
            {
                btnDashboardHistory.Visible = false;
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "LoadDashboardData", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }

    }

    protected void grdEmpDashboard_DataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //if (!string.IsNullOrEmpty(dsEmpDashboard.Tables[0].Rows[e.Row.RowIndex]["Competency"].ToString()))
                //{
                //    string strCompetencyColor = dsEmpDashboard.Tables[0].Rows[e.Row.RowIndex]["Competency"].ToString() != "Amber" ? dsEmpDashboard.Tables[0].Rows[e.Row.RowIndex]["Competency"].ToString() : "#FFCC00";
                //    e.Row.Cells[6].Attributes.Add("style", "background-color: " + strCompetencyColor);
                //}
                //if (!string.IsNullOrEmpty(dsEmpDashboard.Tables[0].Rows[e.Row.RowIndex]["Communication"].ToString()))
                //{
                //    string strCommunicationColor = dsEmpDashboard.Tables[0].Rows[e.Row.RowIndex]["Communication"].ToString() != "Amber" ? dsEmpDashboard.Tables[0].Rows[e.Row.RowIndex]["Communication"].ToString() : "#FFCC00";
                //    e.Row.Cells[7].Attributes.Add("style", "background-color: " + strCommunicationColor);
                //}

                //if (!string.IsNullOrEmpty(dsEmpDashboard.Tables[0].Rows[e.Row.RowIndex]["Commitment"].ToString()))
                //{
                //    string strCommitmentColor = dsEmpDashboard.Tables[0].Rows[e.Row.RowIndex]["Commitment"].ToString() != "Amber" ? dsEmpDashboard.Tables[0].Rows[e.Row.RowIndex]["Commitment"].ToString() : "#FFCC00";
                //    e.Row.Cells[8].Attributes.Add("style", "background-color: " + strCommitmentColor);
                //}
                //if (!string.IsNullOrEmpty(dsEmpDashboard.Tables[0].Rows[e.Row.RowIndex]["Collaboration"].ToString()))
                //{
                //    string strCommitmentColor = dsEmpDashboard.Tables[0].Rows[e.Row.RowIndex]["Collaboration"].ToString() != "Amber" ? dsEmpDashboard.Tables[0].Rows[e.Row.RowIndex]["Collaboration"].ToString() : "#FFCC00";
                //    e.Row.Cells[9].Attributes.Add("style", "background-color: " + strCommitmentColor);
                //}

                HyperLink hypRatingMonth = (HyperLink)e.Row.FindControl("hypRatingMonth");

                bool NAOption = false;

                if (!string.IsNullOrEmpty(dsEmpDashboard.Tables[0].Rows[e.Row.RowIndex]["Competency"].ToString()))
                {

                    string strCompetencyColor = dsEmpDashboard.Tables[0].Rows[e.Row.RowIndex]["Competency"].ToString() != "Amber" ? dsEmpDashboard.Tables[0].Rows[e.Row.RowIndex]["Competency"].ToString() : "#FFCC00";
                    if (strCompetencyColor.Trim() != NotApplicableStatus)
                    {
                        e.Row.Cells[6].Attributes.Add("style", "background-color: " + strCompetencyColor);
                    }
                    else
                    {
                        hypRatingMonth.Enabled = false;
                        NAOption = true;
                    }
                }
                if (!string.IsNullOrEmpty(dsEmpDashboard.Tables[0].Rows[e.Row.RowIndex]["Communication"].ToString()))
                {
                    string strCommunicationColor = dsEmpDashboard.Tables[0].Rows[e.Row.RowIndex]["Communication"].ToString() != "Amber" ? dsEmpDashboard.Tables[0].Rows[e.Row.RowIndex]["Communication"].ToString() : "#FFCC00";
                    if (strCommunicationColor.Trim() != NotApplicableStatus)
                    {
                        e.Row.Cells[7].Attributes.Add("style", "background-color: " + strCommunicationColor);
                    }
                    else
                    {
                        //e.Row.Cells[10].Text = NotApplicableStatus;
                    }
                }

                if (!string.IsNullOrEmpty(dsEmpDashboard.Tables[0].Rows[e.Row.RowIndex]["Commitment"].ToString()))
                {
                    string strCommitmentColor = dsEmpDashboard.Tables[0].Rows[e.Row.RowIndex]["Commitment"].ToString() != "Amber" ? dsEmpDashboard.Tables[0].Rows[e.Row.RowIndex]["Commitment"].ToString() : "#FFCC00";
                    if (strCommitmentColor.Trim() != NotApplicableStatus)
                    {
                        e.Row.Cells[8].Attributes.Add("style", "background-color: " + strCommitmentColor);
                    }
                    else
                    {
                        //e.Row.Cells[11].Text = NotApplicableStatus;
                    }
                }
                if (!string.IsNullOrEmpty(dsEmpDashboard.Tables[0].Rows[e.Row.RowIndex]["Collaboration"].ToString()))
                {
                    string strCollaboration = dsEmpDashboard.Tables[0].Rows[e.Row.RowIndex]["Collaboration"].ToString() != "Amber" ? dsEmpDashboard.Tables[0].Rows[e.Row.RowIndex]["Collaboration"].ToString() : "#FFCC00";
                    if (strCollaboration.Trim() != NotApplicableStatus)
                    {
                        e.Row.Cells[9].Attributes.Add("style", "background-color: " + strCollaboration);
                    }
                    else
                    {
                        //e.Row.Cells[12].Text = NotApplicableStatus;
                    }
                }


                if (!NAOption)
                {
                    string createURLParameter = URLHelper.SecureParameters("EmpId", DataBinder.Eval(e.Row.DataItem, "EmpId").ToString()) + "&"
                              + URLHelper.SecureParameters("departmentId", DataBinder.Eval(e.Row.DataItem, "DepartmentId").ToString()) + "&"
                              + URLHelper.SecureParameters("projectId", DataBinder.Eval(e.Row.DataItem, "ProjectId").ToString()) + "&"
                              + URLHelper.SecureParameters("month", DataBinder.Eval(e.Row.DataItem, "Month").ToString()) + "&"
                              + URLHelper.SecureParameters("year", DataBinder.Eval(e.Row.DataItem, "Year").ToString()) + "&"
                              + URLHelper.SecureParameters("FBID", DataBinder.Eval(e.Row.DataItem, "FBID").ToString()) + "&"
                              + URLHelper.SecureParameters("loginEmailId", UserMailId) + "&"
                              + URLHelper.SecureParameters("Mode", DecryptQueryString("Mode").ToString()) + "&"
                              + URLHelper.SecureParameters("EmployeeName", DataBinder.Eval(e.Row.DataItem, "EmployeeName").ToString()) + "&"
                              + URLHelper.CreateSignature(DataBinder.Eval(e.Row.DataItem, "EmpId").ToString(),
                                                       DataBinder.Eval(e.Row.DataItem, "DepartmentId").ToString(),
                                                       DataBinder.Eval(e.Row.DataItem, "ProjectId").ToString(),
                                                       DataBinder.Eval(e.Row.DataItem, "Month").ToString(),
                                                       DataBinder.Eval(e.Row.DataItem, "Year").ToString(),
                                                       DataBinder.Eval(e.Row.DataItem, "FBID").ToString(),
                                                       UserMailId,
                                                       DecryptQueryString("Mode").ToString(),
                                                       DataBinder.Eval(e.Row.DataItem, "EmployeeName").ToString()); //strAllowedToEdit;

                    //Ishwar 09012015 Start
                    hypRatingMonth.Attributes["onclick"] = "javascript:Open4CActionPopUp('" + createURLParameter + "')";
                    //Ishwar 09012015 End
                }

                // Thiso who are working on project show them project name and project joining date column.
                int[] deptId = { 1, 7, 16, 17, 18, 24, 26 };

                HiddenField hdDepartmentId = (HiddenField)e.Row.FindControl("hdDepartmentId");

                if (!string.IsNullOrEmpty(hdDepartmentId.Value))
                {
                    //if(int.Parse(DataBinder.Eval(e.Row.DataItem, "DepartmentId").ToString().Contains('1', '7'))
                    if (deptId.Any(o => int.Parse(o.ToString()) == int.Parse(hdDepartmentId.Value)))
                    {
                        e.Row.Cells[3].Visible = true;
                        grdEmpDashboard.HeaderRow.Cells[3].Visible = true;
                    }
                    else
                    {
                        e.Row.Cells[3].Visible = false;
                        grdEmpDashboard.HeaderRow.Cells[3].Visible = false;
                        //if (DataBinder.Eval(e.Row.DataItem, "ProjectName").ToString().Trim().ToUpper() == "BENCH")
                        //{
                        //    e.Row.Cells[3].Text = "";
                        //}
                    }
                }


                e.Row.Cells[0].Visible = false;
                grdEmpDashboard.HeaderRow.Cells[0].Visible = false;

                e.Row.Cells[1].Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
                e.Row.Cells[1].Attributes["onmouseout"] = "this.style.textDecoration='none';";

            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdEmpDashboard_DataBound", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }

    }

    protected void lnk_Click(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();

        txtDesc.Text = "";
        btnDescSave.Enabled = true;
        btnClose.Enabled = true;

        ImageButton btnDetails = sender as ImageButton;
        GridViewRow row = (GridViewRow)btnDetails.NamingContainer;
        DropDownList ddl = (DropDownList)row.FindControl("ddlCParameter");

        if (int.Parse(id) > 0)
        {
            hdRowId.Value = id;
        }
        else
        {
            Label lblNo = (Label)row.FindControl("lblRowId");

            hdRowId.Value = lblNo.Text;
        }

        Page page = HttpContext.Current.Handler as Page;
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





    protected void btnDescSave_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grdEmpActionDetails.Rows)
        {
            HiddenField hdActionId = (HiddenField)row.FindControl("hdActionId");
            Label lblRoWNo = (Label)row.FindControl("lblRowId");

            if (hdActionId.Value == hdRowId.Value && hdActionId.Value != "0")
            {
                if (ViewState["ImageButtonType"].ToString() == "Desc")
                {
                    TextBox txtDescGV = (TextBox)row.FindControl("txtDescription");
                    txtDescGV.Text = txtDesc.Text;
                }
                else if (ViewState["ImageButtonType"].ToString() == "Action")
                {
                    TextBox txtActionGV = (TextBox)row.FindControl("txtAction");
                    txtActionGV.Text = txtDesc.Text;
                }
                else if (ViewState["ImageButtonType"].ToString() == "Remarks")
                {
                    TextBox txtRemarksGV = (TextBox)row.FindControl("txtRemarks");
                    txtRemarksGV.Text = txtDesc.Text;
                }
                break;
            }
            else if (lblRoWNo.Text == hdRowId.Value)
            {
                if (ViewState["ImageButtonType"].ToString() == "Desc")
                {
                    TextBox txtDescGV = (TextBox)row.FindControl("txtDescription");
                    txtDescGV.Text = txtDesc.Text;
                }
                else if (ViewState["ImageButtonType"].ToString() == "Action")
                {
                    TextBox txtActionGV = (TextBox)row.FindControl("txtAction");
                    txtActionGV.Text = txtDesc.Text;
                }
                else if (ViewState["ImageButtonType"].ToString() == "Remarks")
                {
                    TextBox txtRemarksGV = (TextBox)row.FindControl("txtRemarks");
                    txtRemarksGV.Text = txtDesc.Text;
                }
                break;
            }
        }

        UpdateDataTable();

        grdEmpActionDetails.DataSource = dtActionData;
        grdEmpActionDetails.DataBind();

        ViewState["dtActionData"] = dtActionData;
        //Session[SessionNames.FOURC_ACTION_DATA] = dtActionData;

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
            )
        {
            flag = true;
        }

        return flag;
    }
}
