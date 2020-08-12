using System;
using System.Collections;
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
using Common.Constants;
using Common.AuthorizationManager;
using System.Collections.Generic;
using System.Reflection;
using BusinessEntities;

public partial class FourCModule_4C_Add4CFeedback : BaseClass
{
    const string CLASS_NAME = "FourCModule_4C_Add4CFeedback.aspx.cs";
    const string SEND_FOR_REVIEW = "SENDFORREVIEW";
    const string REVIEWED = "REVIEWED";
    const string ADD_REVIEWRATING = "ADDREVIEWRATING";
    const string SUBMIT_REVIEW = "SUBMIT_REVIEW";

    int fromCurrentDate;
    string strEmpCode;
    string UserRaveDomainId;
    string UserMailId;
    List<string> lstRights;
    DataSet dsEmpDeatils;
    DataTable dsEmpDeatilsCarryForward;
    DataSet dsCretorReviewerDeatils;
    public ASP._4clogin_aspx prev;

    string Open4CStatusId = "1062";
    string Reviewed4CStatusId = "1064";
    string SendForReview4CStatusId = "1063";
    string NotApplicableStatus = "N/A";


    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
        HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();

        //Poonam : Issue : Disable Button : Starts

        btnSubmitRating.OnClientClick = ClientScript.GetPostBackEventReference(btnSubmitRating, null);
        btnSendForReview.OnClientClick = ClientScript.GetPostBackEventReference(btnSendForReview, null);
        //Poonam : Issue : Disable Button : Ends

        AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();

        string txtUser = null;

        if (ViewState["UserMailId"] == null)
        {
            if (PreviousPage != null)
            {                
                prev = (ASP._4clogin_aspx)PreviousPage;                
                txtUser = prev.UserName;                

                if (txtUser != null && !string.IsNullOrEmpty(txtUser))
                {
                    //GoogleMail
                    //ViewState["UserMailId"] = txtUser.ToString().Trim() + "@rave-tech.com";
                    //NorthgateChange keep only northgate idf
                    //if (txtUser.ToUpper().Trim() == "SAWITA.KAMAT")
                    //{
                    //    //Google
                    //    txtUser = txtUser + "@" + AuthorizationManagerConstants.NORTHGATEDOMAIN;
                    //}
                    //else
                    //{
                    //    txtUser = txtUser + "@" + AuthorizationManagerConstants.RAVEDOMAIN;
                    //}
                    //ViewState["UserMailId"] = txtUser.Replace("co.in", "com");

                    ViewState["UserMailId"] = objRaveHRAuthorizationManager.GetDomainUsers(txtUser.ToUpper().Trim());
                }                                                                                                                                                                                                                                                                                                                                                     
            }
            else
            {
                if (!string.IsNullOrEmpty(DecryptQueryString("LoginEmailId").ToString()))
                {
                    ViewState["UserMailId"] = DecryptQueryString("LoginEmailId").ToString().Trim();
                }
                else
                {
                    
                    UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
                    ViewState["UserMailId"] = UserRaveDomainId.Replace("co.in", "com");
                }
            }

            UserMailId = ViewState["UserMailId"].ToString();
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

                //tblAVP.Visible = false;

                lstRights = CheckAccessRights(UserMailId);
                // DataSet ds = CheckAccessRights(UserMailId);
                //DataTable dtRoles = ds.Tables[0];

                //ViewState["FourCEmpRole"] = ds;
                //ViewState["FourCEmpRole"] = dtRoles;
                //ViewState["LoginEmpId"] = dtRoles.Rows[0]["EmpId"].ToString();

                //if (dtRoles.Select("Role = '" + CommonConstants.FOURCROLE14 + "'").Any())
                //{
                //    ViewState["Role"] = CommonConstants.FOURCROLE14;
                //}
                //else
                //{
                //    ViewState["Role"] = dtRoles.Select("Role = '" + CommonConstants.FOURCROLE14 + "'");
                //}

                if (!lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.ONLYCREATOR.ToString()))
                {
                    tblAVP.Visible = true;
                }
                else
                {
                    tblAVP.Visible = false;
                }

                if (lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.FOURCADMIN.ToString()))
                {
                    ViewState["Role"] = MasterEnum.FourCRole.FOURCADMIN.ToString();

                    //divMonthYearDisplay.Visible = true;
                }
                else
                {
                    ViewState["Role"] = "";

                    //divMonthYearDisplay.Visible = false;
                }

                //if (divMonthYearDisplay.Visible)
                //{
                //ViewState["ClickCount"] = "1";
                //lblDate.Text = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Year;
                //lblDate.Text = DateTime.Now.AddMonths(-1).ToString("MMMM yyyy");
                //imgNext.Enabled = false;
                //}

                if (string.IsNullOrEmpty(DecryptQueryString("month").ToString()))
                {
                    ViewState["ClickCount"] = "1";
                    //lblDate.Text = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Year;
                    lblDate.Text = DateTime.Now.AddMonths(-1).ToString("MMMM yyyy");
                    //Show current Month
                    //imgNext.Enabled = false;
                }

                #region "COMMENT"
                //if (lstRights.Any(o=>o.ToString() == MasterEnum.FourCRole.FOURCADMIN.ToString()))
                //{
                //    tdEmployee.Visible = true;
                //    ViewState["FourCRole"] = MasterEnum.FourCRole.FOURCADMIN.ToString();
                //}
                //else if (lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.CREATORANDREVIEWER.ToString()))
                //{
                //    tdEmployee.Visible = false;
                //    ViewState["FourCRole"] = MasterEnum.FourCRole.CREATORANDREVIEWER.ToString();

                //}
                //else if (lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.REVIEWER.ToString()))
                //{
                //    tdEmployee.Visible = false;
                //    ViewState["FourCRole"] = MasterEnum.FourCRole.REVIEWER.ToString();
                //}
                //else if (lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.CREATOR.ToString()))
                //{
                //    tdEmployee.Visible = false;
                //    ViewState["FourCRole"] = MasterEnum.FourCRole.CREATOR.ToString();
                //}


                ////show Add review option (Admin + reviewer), Reviewer, creator + reviewer show
                //if ((lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.FOURCADMIN.ToString()) && lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.REVIEWER.ToString())) || lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.CREATORANDREVIEWER.ToString()) || lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.REVIEWER.ToString()))
                //{
                //    tblAVP.Visible = true;
                //}

                #endregion "COMMENT"

                // creator + reviewer
                //if (lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.CREATORANDREVIEWER.ToString()) || lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.REVIEWER.ToString()))
                //{
                //    ViewState["AllowDirectSubmit"] = "Allowed";
                //}

                //for (int iRow = 0; iRow < ds.Tables[0].Rows.Count; iRow++)
                //{
                //    if(
                //    ViewState["Role"] = 
                //}

                FillDepartment();
                FillProjectNameDropDown();
                FillEmployeeDDL();
                // BindData();



                //Below code is to refresh page when pop up window closed then assign projectId, month, year value in javascript and pass this to function.
                //if (!string.IsNullOrEmpty(hdPopUpReturnProjectId.Value))
                //if (!string.IsNullOrEmpty(DecryptQueryString("ProjectId").ToString()) && !string.IsNullOrEmpty(DecryptQueryString("month").ToString()) && !string.IsNullOrEmpty(DecryptQueryString("year").ToString()))
                if (!string.IsNullOrEmpty(DecryptQueryString("month").ToString()) && !string.IsNullOrEmpty(DecryptQueryString("year").ToString()))
                {
                    hdPopUpReturnDepartmentId.Value = DecryptQueryString("departmentId").ToString();
                    hdPopUpReturnProjectId.Value = DecryptQueryString("ProjectId").ToString();
                    hdPopupReturnMonth.Value = DecryptQueryString("month").ToString();
                    hdPopupReturnYear.Value = DecryptQueryString("year").ToString();

                    //ddlDepartment.SelectedItem.Value == hdPopUpReturnDepartmentId.Value;
                    //ddlProjectList.SelectedItem.Value == hdPopUpReturnProjectId.Value;

                    ddlDepartment.SelectedValue = hdPopUpReturnDepartmentId.Value;
                    ddlProjectList.SelectedValue = hdPopUpReturnProjectId.Value;
                    rbAVPView.SelectedIndex = int.Parse(DecryptQueryString("Mode").ToString());


                    if (int.Parse(hdPopUpReturnDepartmentId.Value) == 1 && !string.IsNullOrEmpty(hdPopUpReturnProjectId.Value) && int.Parse(hdPopUpReturnProjectId.Value) > 0)
                    {
                        ddlProjectList.Enabled = true;
                    }
                    else
                    {
                        ddlProjectList.Enabled = false;
                    }


                    //if (ddlProjectList.SelectedIndex > 0)
                    //{
                    //    BindProjectData(int.Parse(hdPopUpReturnProjectId.Value), int.Parse(hdPopupReturnMonth.Value), int.Parse(hdPopupReturnYear.Value), ViewState["Role"].ToString());
                    //}
                    //else
                    //{
                    //    BindProjectData(int.Parse(ddlProjectList.SelectedItem.Value), int.Parse(hdPopupReturnMonth.Value), int.Parse(hdPopupReturnYear.Value), ViewState["Role"].ToString());
                    //}

                    if (!string.IsNullOrEmpty(hdPopUpReturnProjectId.Value) || int.Parse(hdPopUpReturnDepartmentId.Value) != 1)
                    {

                        //SetMonthandYear(int.Parse(hdPopupReturnMonth.Value), int.Parse(hdPopupReturnYear.Value));

                        //DateTime dtMonthYear = DateTime.Parse(string.Concat("01 ", hdPopupReturnMonth.Value, " ", hdPopupReturnYear.Value));
                        //lblDate.Text = dtMonthYear.ToString("MMMM") + " " + dtMonthYear.Year;

                        ////As per month selection add ViewState["ClickCount"] value so that next and previous icon work properly.
                        ////Show Curreny month data
                        ////if (lblDate.Text != DateTime.Now.AddMonths(-1).ToString("MMMM yyyy"))
                        //if (lblDate.Text != DateTime.Now.ToString("MMMM yyyy"))
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
                        //    //Show Current Month
                        //    //ViewState["ClickCount"] = 1;
                        //    ViewState["ClickCount"] = 0; // 0 for current month
                        //    imgNext.Enabled = false;
                        //}

                        int prId = hdPopUpReturnProjectId.Value == "" ? 0 : int.Parse(hdPopUpReturnProjectId.Value);

                        BindProjectData(prId, int.Parse(hdPopupReturnMonth.Value), int.Parse(hdPopupReturnYear.Value), ViewState["Role"].ToString());
                        hdPopUpReturnProjectId.Value = string.Empty;
                        hdPopupReturnMonth.Value = string.Empty;
                        hdPopupReturnYear.Value = string.Empty;

                        //Request.QueryString.Clear();
                        PropertyInfo isreadonly = typeof(System.Collections.Specialized.NameValueCollection).GetProperty("IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
                        // make collection editable
                        isreadonly.SetValue(this.Request.QueryString, false, null);

                        this.Request.QueryString.Remove("departmentId");
                        this.Request.QueryString.Remove("ProjectId");
                        this.Request.QueryString.Remove("month");
                        this.Request.QueryString.Remove("year");
                        this.Request.QueryString.Remove("LoginEmailId");

                        this.Request.QueryString.Clear();
                    }
                }
            }

        }

        lblMessage.Visible = false;

        if (ViewState["CurrentMonthLastDay"] != null)
        {
            DateTime dtSelectedDate = DateTime.Parse(string.Concat("01 ", lblDate.Text));
            DateTime dtCurrentDate = DateTime.Parse(DateTime.Now.ToString("dd MMMM yyyy"));

            //For Current month do not allow manager to send data for review
            //if (DateTime.Now.ToString("dd MMMM yyyy") != DateTime.Parse(ViewState["CurrentMonthLastDay"].ToString()).ToString("dd MMMM yyyy") && lblDate.Text == DateTime.Now.ToString("MMMM yyyy"))
            //if (DateTime.Now.ToString("dd MMMM yyyy") != DateTime.Parse(ViewState["CurrentMonthLastDay"].ToString()).ToString("dd MMMM yyyy") && dtCurrentDate.Date > dtSelectedDate.Date)
            //if (lblDate.Text == DateTime.Now.ToString("MMMM yyyy"))
            //{
            //    btnSendForReview.Enabled = false;
            //    btnSubmitRating.Enabled = false;
            //    btnNotApplicable.Enabled = false;
            //}
       
            if (DateTime.Now.ToString("dd MMMM yyyy") != DateTime.Parse(ViewState["CurrentMonthLastDay"].ToString()).ToString("dd MMMM yyyy") && lblDate.Text == DateTime.Now.ToString("MMMM yyyy"))
            {
                btnSendForReview.Enabled = false;
                btnSubmitRating.Enabled = false;
                btnNotApplicable.Enabled = false;
            }
            else if (dtSelectedDate.Date > dtCurrentDate.Date)
            {
                btnSendForReview.Enabled = false;
                btnSubmitRating.Enabled = false;
                btnNotApplicable.Enabled = false;
            }
        }

        if (rbAVPView.SelectedIndex == 1)
        {
            btnNotApplicable.Visible = false;
        }
        else
        {
            if (btnSendForReview.Visible || btnSubmitRating.Visible)
            {
                btnNotApplicable.Visible = true;
            }
            else
            {
                btnNotApplicable.Visible = false;
            }
        }
    }

    private void SetMonthandYear(int month, int year)
    {
        DateTime dtMonthYear = DateTime.Parse(string.Concat("01 ", month, " ", year));
        lblDate.Text = dtMonthYear.ToString("MMMM") + " " + dtMonthYear.Year;
        string maxRedirectedDate = lblDate.Text;

        if (rbAVPView.SelectedIndex == 0)
        {
            DateTime maxdate = DateTime.Parse(string.Concat("01 ", int.Parse(ViewState["MaxMonth"].ToString()), " ", int.Parse(ViewState["MaxYear"].ToString())));
            maxRedirectedDate = maxdate.ToString("MMMM") + " " + maxdate.Year;
        }

        //calculate selected month date
         DateTime dtSelectedDate = DateTime.Parse(string.Concat("01 ", lblDate.Text));
        //calculate current day of month and compare if selected month greter than current month then imgNext should get disable.
         DateTime dtCurrentDate = DateTime.Parse(DateTime.Now.ToString("dd MMMM yyyy"));


        //As per month selection add ViewState["ClickCount"] value so that next and previous icon work properly.
        //Show Curreny month data
        //if (lblDate.Text != DateTime.Now.AddMonths(-1).ToString("MMMM yyyy"))
        if (lblDate.Text != DateTime.Now.ToString("MMMM yyyy"))
        {
            for (int iCount = 1; iCount <= 7; iCount++)
            {
                if (lblDate.Text == DateTime.Now.AddMonths(-iCount).ToString("MMMM yyyy"))
                {
                    ViewState["ClickCount"] = iCount;

                    //this will allow user to enter data in max rating month.
                    imgNext.Enabled = true;

                    //do not allow user for fill entry future unless previous entry filled. added if condition.
                    if ((maxRedirectedDate == lblDate.Text && rbAVPView.SelectedIndex == 0))
                    {
                         imgNext.Enabled = false;
                    }
                    break;
                }
            }
        }
        else
        {

            //Show Current Month
            //ViewState["ClickCount"] = 1;
            ViewState["ClickCount"] = 0; // 0 for current month
            //imgNext.Enabled = false;

            if ((maxRedirectedDate == lblDate.Text && rbAVPView.SelectedIndex == 0))
            {
                imgNext.Enabled = false;
            }
        }

        if (dtSelectedDate.Date > dtCurrentDate.Date && rbAVPView.SelectedIndex == 0)
        {
            for (int iCount = 1; iCount <= 2; iCount++)
            {
                if (lblDate.Text == DateTime.Now.AddMonths(iCount).ToString("MMMM yyyy"))
                {
                    //int.Parse(ViewState["ClickCount"].ToString())
                    int valueCount =  -iCount;
                    ViewState["ClickCount"] = valueCount;
                }
            }
            imgNext.Enabled = false;
        }

        SetExportToExcelSentForReviewLink();

        
    }


    //private DataSet CheckAccessRights(string emailId)
    //{
    //    //lstRights = new List<string> { };
    //    DataSet ds = null;
    //    try
    //    {

    //        Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
    //        ds = fourCBAL.Check4CLoginRights(emailId);
    //        return ds;
    //    }
    //    catch (RaveHRException ex)
    //    {
    //        LogErrorMessage(ex);
    //    }
    //    catch (Exception ex)
    //    {
    //        RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "CheckAccessRights", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
    //        LogErrorMessage(objEx);
    //    }
    //    return ds;
    //}

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

            //calculate last day of month and which is not holiday
            DateTime? dtCurrentLastDayOfMonth = fourCBAL.CurrentMonthLastDay();
            ViewState["CurrentMonthLastDay"] = dtCurrentLastDayOfMonth;

            //var lastDayOfMonth = DateTime.DaysInMonth(date.Year, date.Month);



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


    protected void imgPrevious_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //fromCurrentDate = fromCurrentDate + 1;
            if (lblDate.Text == DateTime.Now.AddMonths(-6).ToString("MMMM yyyy"))
            {
                imgPrevious.Enabled = false;
            }

            //if (ViewState["ClickCount"] == null)
            //{
            //    ViewState["ClickCount"] = 1;
            //}

            imgNext.Enabled = true;
            ViewState["ClickCount"] = Convert.ToInt32(ViewState["ClickCount"]) + 1;
            fromCurrentDate = Convert.ToInt32(ViewState["ClickCount"]);
            lblDate.Text = DateTime.Now.AddMonths(-fromCurrentDate).ToString("MMMM yyyy");// + " " + DateTime.Now.Year;

            DateTime monthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));
            //monthYear = monthYear.AddMonths(-1);

            //Venkatesh : 4C_Support 26-Feb-2014 : Start 
            //Desc : Dept Support
            if (Utility.IsSupportDept(int.Parse(ddlDepartment.SelectedItem.Value)))
            {
                if (monthYear.Month <= Common.CommonConstants.SupportDeptStartMonth && monthYear.Year  == CommonConstants.SupportDeptStartYear)
                    imgPrevious.Enabled = false;
                else
                    imgPrevious.Enabled = true;
            }
            //Venkatesh : 4C_Support 26-Feb-2014 : End

            if (ddlProjectList.SelectedIndex > 0 || ddlEmployee.SelectedIndex > 0 || (ddlDepartment.SelectedIndex > 0 && int.Parse(ddlDepartment.SelectedValue) != Convert.ToInt32(MasterEnum.Departments.Projects)) || (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Projects) && ddlProjectList.SelectedIndex > 0))
            {
                BindProjectData(int.Parse(ddlProjectList.SelectedItem.Value), monthYear.Month, monthYear.Year, ViewState["Role"].ToString());
            }
            else
            {
                // do not show previous state
                divReviewerNoData.Visible = false;
                lblMessage.Visible = false;
            }


            SetExportToExcelSentForReviewLink();

        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "imgPrevious_Click", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void imgNext_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //if (lblDate.Text == DateTime.Now.AddMonths(-1).ToString("MMMM yyyy"))
            //{
            //    imgNext.Enabled = false;
            //}
            imgPrevious.Enabled = true;

            ViewState["ClickCount"] = Convert.ToInt32(ViewState["ClickCount"]) - 1;
            fromCurrentDate = Convert.ToInt32(ViewState["ClickCount"]);
            lblDate.Text = DateTime.Now.AddMonths(-fromCurrentDate).ToString("MMMM yyyy");

            DateTime monthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));
            //monthYear = monthYear.AddMonths(-1);
            if (ddlProjectList.SelectedIndex > 0 || ddlEmployee.SelectedIndex > 0 || (ddlDepartment.SelectedIndex > 0 && int.Parse(ddlDepartment.SelectedValue) != Convert.ToInt32(MasterEnum.Departments.Projects)) || (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Projects) && ddlProjectList.SelectedIndex > 0))
            {

                BindProjectData(int.Parse(ddlProjectList.SelectedItem.Value), monthYear.Month, monthYear.Year, ViewState["Role"].ToString());
            }
            else
            {
                // do not show previous state
                divReviewerNoData.Visible = false;
                lblMessage.Visible = false;
            }


            SetExportToExcelSentForReviewLink();

        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "imgPrevious_Click", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdEmpDetails_DataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[2].Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
            e.Row.Cells[2].Attributes["onmouseout"] = "this.style.textDecoration='none';";

            if (ViewState["AllowDirectSubmit"] == null)
                ViewState["AllowDirectSubmit"] = "";

             DateTime monthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));

             CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
            

             //strAllowedToEdit = "True"; // Allow to edit so Save button enable on action page.
             if (rbAVPView.SelectedIndex == 0 && !string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "Employee4CStatus").ToString()) && int.Parse(DataBinder.Eval(e.Row.DataItem, "Employee4CStatus").ToString()) != int.Parse(Open4CStatusId.ToString()))
             {
                 chkSelect.Checked = true;
                 chkSelect.Enabled = false;
                // strAllowedToEdit = "";
             }
             if (rbAVPView.SelectedIndex == 1 && !string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "Employee4CStatus").ToString()) && int.Parse(DataBinder.Eval(e.Row.DataItem, "Employee4CStatus").ToString()) == int.Parse(Reviewed4CStatusId.ToString()))
             {
                 chkSelect.Checked = true;
                 chkSelect.Enabled = false;
                // strAllowedToEdit = "";
             }

            //Ishwar Patil 30062015 Start
             if (DataBinder.Eval(e.Row.DataItem, "Employee4CStatus").ToString() == Reviewed4CStatusId)
             {
                 chkSelect.Enabled = false;
             }
             //Ishwar Patil 30062015 End

            ImageButton imgRejectRating = (ImageButton)e.Row.FindControl("imgReject");


            if (rbAVPView.SelectedIndex == 1 && chkSelect.Enabled == true)
            {
                grdEmpDetails.HeaderRow.Cells[13].Visible = true;
                imgRejectRating.Visible = true;
            }
            else
            {

                if (rbAVPView.SelectedIndex == 0)
                {
                    grdEmpDetails.HeaderRow.Cells[13].Visible = false;
                    imgRejectRating.Visible = false;
                }
                else
                {
                    imgRejectRating.Enabled = false;
                    imgRejectRating.ImageUrl = "~/Images/rejectDisable.jpg";
                    imgRejectRating.CssClass = "button";
                }
            }

             HyperLink hypEmpName = (HyperLink)e.Row.FindControl("hypEmpName");

             hypEmpName.NavigateUrl = "4CEmpAction.aspx?" + URLHelper.SecureParameters("EmpId", DataBinder.Eval(e.Row.DataItem, "EmpId").ToString()) + "&" + URLHelper.SecureParameters("departmentId", DataBinder.Eval(e.Row.DataItem, "DepartmentId").ToString()) + "&" + URLHelper.SecureParameters("projectId", DataBinder.Eval(e.Row.DataItem, "ProjectId").ToString()) + "&"
                                                       + URLHelper.SecureParameters("month", monthYear.Month.ToString()) + "&" + URLHelper.SecureParameters("year", monthYear.Year.ToString()) + "&" + URLHelper.SecureParameters("FBID", DataBinder.Eval(e.Row.DataItem, "FBID").ToString()) + "&" + URLHelper.SecureParameters("loginEmailId", UserMailId) + "&" + URLHelper.SecureParameters("Mode", rbAVPView.SelectedIndex.ToString()) + "&"  //Mode 0 = Add, 1 = Review   //URLHelper.SecureParameters("AllowToEdit", strAllowedToEdit) + "&" 
                                                       + URLHelper.CreateSignature(DataBinder.Eval(e.Row.DataItem, "EmpId").ToString(), DataBinder.Eval(e.Row.DataItem, "DepartmentId").ToString(), DataBinder.Eval(e.Row.DataItem, "ProjectId").ToString(), monthYear.Month.ToString(), monthYear.Year.ToString(), DataBinder.Eval(e.Row.DataItem, "FBID").ToString(), UserMailId, rbAVPView.SelectedIndex.ToString()); //strAllowedToEdit

             //Mahendra Start 4C Redirection 
            HiddenField hdEmpId = (HiddenField)e.Row.FindControl("hdEmpId");
            Label lblGrdCompetency = (Label)e.Row.FindControl("lblGrdCompetency");
            HiddenField hdCompetency = (HiddenField)e.Row.FindControl("hdCompetency");
            Label lblGrdCommunication = (Label)e.Row.FindControl("lblGrdCommunication");
            HiddenField hdCommunication = (HiddenField)e.Row.FindControl("hdCommunication");
            Label lblGrdCommitment = (Label)e.Row.FindControl("lblGrdCommitment");
            HiddenField hdCommitment = (HiddenField)e.Row.FindControl("hdCommitment");
            Label lblGrdCollaboration = (Label)e.Row.FindControl("lblGrdCollaboration");
            HiddenField hdCollaboration = (HiddenField)e.Row.FindControl("hdCollaboration");
            HiddenField hdFlag = (HiddenField)e.Row.FindControl("hdFlag");

            DataRow foundRow = null;
            if (dsEmpDeatilsCarryForward != null && dsEmpDeatilsCarryForward.Rows.Count > 0)
            {
                foundRow = dsEmpDeatilsCarryForward.Rows.Find(int.Parse(hdEmpId.Value));
            }
            //Mahendra End

            if (!string.IsNullOrEmpty(dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Competency"].ToString())
                || (foundRow != null && !string.IsNullOrEmpty(foundRow["Competency"].ToString())))   //Mahendra Start End
            {

                string strCompetencyColor = dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Competency"].ToString() != "Amber" ? dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Competency"].ToString() : "#FFCC00";

                //Mahendra Start
                if (string.IsNullOrEmpty(strCompetencyColor) || strCompetencyColor == "")
                {
                    strCompetencyColor = foundRow["Competency"].ToString() != "Amber" ? foundRow["Competency"].ToString() : "#FFCC00";
                    lblGrdCompetency.Text = foundRow["Competency"].ToString();
                    hdCompetency.Value = foundRow["Competency"].ToString();
                    hdFlag.Value = foundRow["Flag"].ToString();
                }
                //Mahendra End

                if (strCompetencyColor.Trim() != NotApplicableStatus)
                {
                    e.Row.Cells[9].Attributes.Add("style", "background-color: " + strCompetencyColor);
                }
                else
                {
                    chkSelect.Checked = true;
                    hypEmpName.Enabled = false;
                }

                //Ishwar : 10072015 Start
                if (rbAVPView.SelectedIndex == 1)
                {
                    if (dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex][DbTableColumn.Competency].ToString() != dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex][DbTableColumn.LastMonthCompetency].ToString())
                    {
                        e.Row.Cells[9].Attributes.Add("style", "border: solid 3px Blue; background-color: " + strCompetencyColor);
                        e.Row.Cells[9].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[9].Font.Bold = true;
                    }
                }
                //Ishwar : 10072015 End
            }
            if (!string.IsNullOrEmpty(dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Communication"].ToString())
                || (foundRow != null && !string.IsNullOrEmpty(foundRow["Communication"].ToString())))   //Mahendra Start End
            {
                string strCommunicationColor = dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Communication"].ToString() != "Amber" ? dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Communication"].ToString() : "#FFCC00";

                //Mahendra Start
                if (string.IsNullOrEmpty(strCommunicationColor) || strCommunicationColor == "")
                {
                    strCommunicationColor = foundRow["Communication"].ToString() != "Amber" ? foundRow["Communication"].ToString() : "#FFCC00";
                    lblGrdCommunication.Text = foundRow["Communication"].ToString();
                    hdCommunication.Value = foundRow["Communication"].ToString();
                }
                //Mahendra End

                if (strCommunicationColor.Trim() != NotApplicableStatus)
                {
                    e.Row.Cells[10].Attributes.Add("style", "background-color: " + strCommunicationColor);
                }

                //Ishwar : 10072015 Start
                if (rbAVPView.SelectedIndex == 1)
                {
                    if (dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex][DbTableColumn.Communication].ToString() != dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex][DbTableColumn.LastMonthCommunication].ToString())
                    {
                        e.Row.Cells[10].Attributes.Add("style", "border: solid 3px Blue; background-color: " + strCommunicationColor);
                        e.Row.Cells[10].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[10].Font.Bold = true;
                    }
                }
                //Ishwar : 10072015 End
            }

            if (!string.IsNullOrEmpty(dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Commitment"].ToString())
                || (foundRow != null && !string.IsNullOrEmpty(foundRow["Commitment"].ToString())))   //Mahendra Start End
            {
                string strCommitmentColor = dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Commitment"].ToString() != "Amber" ? dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Commitment"].ToString() : "#FFCC00";

                //Mahendra Start
                if (string.IsNullOrEmpty(strCommitmentColor) || strCommitmentColor == "")
                {
                    strCommitmentColor = foundRow["Commitment"].ToString() != "Amber" ? foundRow["Commitment"].ToString() : "#FFCC00";
                    lblGrdCommitment.Text = foundRow["Commitment"].ToString();
                    hdCommitment.Value = foundRow["Commitment"].ToString();
                }
                //Mahendra End

                if (strCommitmentColor.Trim() != NotApplicableStatus)
                {
                    e.Row.Cells[11].Attributes.Add("style", "background-color: " + strCommitmentColor);
                }

                //Ishwar : 10072015 Start
                if (rbAVPView.SelectedIndex == 1)
                {
                    if (dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex][DbTableColumn.Commitment].ToString() != dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex][DbTableColumn.LastMonthCommitment].ToString())
                    {
                        e.Row.Cells[11].Attributes.Add("style", "border: solid 3px Blue; background-color: " + strCommitmentColor);
                        e.Row.Cells[11].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[11].Font.Bold = true;
                    }
                }
                //Ishwar : 10072015 End
            }
            if (!string.IsNullOrEmpty(dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Collaboration"].ToString())
                || (foundRow != null && !string.IsNullOrEmpty(foundRow["Commitment"].ToString())))   //Mahendra Start End
            {
                string strCollaboration = dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Collaboration"].ToString() != "Amber" ? dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Collaboration"].ToString() : "#FFCC00";

                //Mahendra Start
                if (string.IsNullOrEmpty(strCollaboration) || strCollaboration == "")
                {
                    strCollaboration = foundRow["Collaboration"].ToString() != "Amber" ? foundRow["Collaboration"].ToString() : "#FFCC00";
                    lblGrdCollaboration.Text = foundRow["Collaboration"].ToString();
                    hdCollaboration.Value = foundRow["Collaboration"].ToString();
                }
                //Mahendra End

                if (strCollaboration.Trim() != NotApplicableStatus)
                {
                    e.Row.Cells[12].Attributes.Add("style", "background-color: " + strCollaboration);
                }
                //Ishwar : 10072015 Start
                if (rbAVPView.SelectedIndex == 1)
                {
                    if (dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex][DbTableColumn.Collaboration].ToString() != dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex][DbTableColumn.LastMonthCollaboration].ToString())
                    {
                        e.Row.Cells[12].Attributes.Add("style", "border: solid 3px Blue; background-color: " + strCollaboration);
                        e.Row.Cells[12].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[12].Font.Bold = true;
                    }
                }
                //Ishwar : 10072015 End
            }

            //Ishwar 25082015 Start
            if (rbAVPView.SelectedIndex == 1)
            {
                Rave.HR.BusinessLayer.FourC.FourC objHighlightBL = new Rave.HR.BusinessLayer.FourC.FourC();
                DataTable dthightdetails = objHighlightBL.Get4C_ActionDetailsHighlightForDashboard(
                        Convert.ToInt32(hdEmpId.Value),
                        Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "DepartmentId")),
                        DataBinder.Eval(e.Row.DataItem, "ProjectId").ToString() == string.Empty ? 0 : Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ProjectId")),
                        Convert.ToInt32(monthYear.Month.ToString()),
                        Convert.ToInt32(monthYear.Year.ToString())
                        );
                for (int i = 0; i < dthightdetails.Rows.Count; i++)
                {
                    if (dthightdetails.Rows[i]["ParameterName"].ToString() == DbTableColumn.Competency.ToString())
                    {
                        string strCompetencyColor = dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Competency"].ToString() != "Amber" ? dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Competency"].ToString() : "#FFCC00";

                        e.Row.Cells[9].Attributes.Add("style", "border: solid 3px Blue; background-color: " + strCompetencyColor);
                        e.Row.Cells[9].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[9].Font.Bold = true;
                    }
                    if (dthightdetails.Rows[i]["ParameterName"].ToString() == DbTableColumn.Communication.ToString())
                    {
                        string strCommunicationColor = dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Communication"].ToString() != "Amber" ? dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Communication"].ToString() : "#FFCC00";

                        e.Row.Cells[10].Attributes.Add("style", "border: solid 3px Blue; background-color: " + strCommunicationColor);
                        e.Row.Cells[10].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[10].Font.Bold = true;
                    }
                    if (dthightdetails.Rows[i]["ParameterName"].ToString() == DbTableColumn.Commitment.ToString())
                    {
                        string strCommitmentColor = dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Commitment"].ToString() != "Amber" ? dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Commitment"].ToString() : "#FFCC00";

                        e.Row.Cells[11].Attributes.Add("style", "border: solid 3px Blue; background-color: " + strCommitmentColor);
                        e.Row.Cells[11].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[11].Font.Bold = true;
                    }
                    if (dthightdetails.Rows[i]["ParameterName"].ToString() == DbTableColumn.Collaboration.ToString())
                    {
                        string strCollaboration = dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Collaboration"].ToString() != "Amber" ? dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Collaboration"].ToString() : "#FFCC00";

                        e.Row.Cells[12].Attributes.Add("style", "border: solid 3px Blue; background-color: " + strCollaboration);
                        e.Row.Cells[12].ForeColor = System.Drawing.Color.Black;
                        e.Row.Cells[12].Font.Bold = true;
                    }
                }
            }
            //Ishwar 25082015 End

            // Thiso who are working on project show them project name and project joining date column.
            int[] deptId = { 1, 7, 16, 17, 18, 24, 26 };

            if (DataBinder.Eval(e.Row.DataItem, "DepartmentId") != null && !string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "DepartmentId").ToString()))
            {
                //if(int.Parse(DataBinder.Eval(e.Row.DataItem, "DepartmentId").ToString().Contains('1', '7'))
                if (deptId.Any(o => int.Parse(o.ToString()) == int.Parse(DataBinder.Eval(e.Row.DataItem, "DepartmentId").ToString())))
                {
                    e.Row.Cells[5].Visible = true;
                    e.Row.Cells[6].Visible = true;
                    grdEmpDetails.HeaderRow.Cells[5].Visible = true;
                    grdEmpDetails.HeaderRow.Cells[6].Visible = true;
                }
                else
                {
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    grdEmpDetails.HeaderRow.Cells[5].Visible = false;
                    grdEmpDetails.HeaderRow.Cells[6].Visible = false;
                }
            }

            e.Row.Cells[1].Visible = false;
            grdEmpDetails.HeaderRow.Cells[1].Visible = false;
        }
    }

    protected void grdEmpDetails_PageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdEmpDetails.PageIndex = e.NewPageIndex;
            DateTime monthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));
            BindProjectData(int.Parse(ddlProjectList.SelectedItem.Value), monthYear.Month, monthYear.Year, ViewState["Role"].ToString());
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdEmpDetails_PageIndexChanging", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void MyEvent(Object sender, EventArgs e)
    {
        string id = grdEmpDetails.SelectedDataKey.Value.ToString();
        strEmpCode = id;
        ViewState["GridRowSelected"] = grdEmpDetails.SelectedIndex;
        //string script = "<script type='text/javascript'> window.open('4CEmpDetailsPopup.aspx', null, 'scrollbars=1,height=500,width=1350,left=170,top=150'); </script>";
        //string script = "<script type='text/javascript'> window.showModalDialog('4CEmpDetailsPopup.aspx', null, 'dialogHeight:450px; dialogWidth:1270px; center:yes;'); </script>";
        //this.ClientScript.RegisterClientScriptBlock(this.GetType(), "clientScript", script);
    }


    //private void BindProjectData(int projectId, int month, int year, string fourCRole, int empId)
    private void BindProjectData(int projectId, int month, int year, string fourCRole)
    {
        try
        {   
            int empId = 0;
            if (ddlEmployee.SelectedIndex > 0)
            {
                empId = int.Parse(ddlEmployee.SelectedItem.Value);
            }
            grdEmpDetails.Visible = true;
            dsEmpDeatils = new DataSet();
            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();

            //if (ViewState["AllowDirectSubmit"] != null && !string.IsNullOrEmpty(ViewState["AllowDirectSubmit"].ToString()) && projectId == 0)
            //{
            //    dsEmpDeatils = fourCBAL.Get4CCreatorDeatils(month, year, fourCRole, UserMailId);
            //}
            //else
            //{
            int redirectMonth = 0, redirectYear = 0;
            if (rbAVPView.SelectedIndex == 0)
            {
                fourCBAL.GetRedirectMonth(int.Parse(ViewState["LoginEmpId"].ToString()), int.Parse(ddlDepartment.SelectedItem.Value), projectId, month, year, 0, ref redirectMonth, ref redirectYear);

                //for bench do not show redirection
                if (ddlDepartment.SelectedItem.Text == "Projects" && ddlProjectList.SelectedItem.Text.ToUpper().Trim() == "BENCH")
                {
                    if (ViewState["MaxMonth"] == null)
                    {
                        redirectMonth = month;
                        redirectYear = year;
                    }
                    else
                    {
                        redirectMonth = int.Parse(ViewState["MaxMonth"].ToString());
                        redirectYear = int.Parse(ViewState["MaxYear"].ToString());
                    }
                }

                //below condition is for redirection.
                if (((month < redirectMonth && year == redirectYear) || (month > redirectMonth && year < redirectYear)) && (ViewState["MaxMonth"] != null || !string.IsNullOrEmpty(hdPopupReturnMonth.Value)))
                {
                    dsEmpDeatils = fourCBAL.Get4CEmployeeDeatils(int.Parse(ViewState["LoginEmpId"].ToString()), int.Parse(ddlDepartment.SelectedItem.Value), projectId, month, year, fourCRole, empId);

                    ViewState["MaxMonth"] = redirectMonth;
                    ViewState["MaxYear"] = redirectYear;

                    SetMonthandYear(month, year);
                    //Venkatesh : 4C_Support 26-Feb-2014 : Start 
                    //Desc : Dept Support
                    if (Utility.IsSupportDept(int.Parse(ddlDepartment.SelectedItem.Value)))
                    {
                        if (month <= Common.CommonConstants.SupportDeptStartMonth && year == CommonConstants.SupportDeptStartYear)
                        {
                            imgPrevious.Enabled = false;
                            //SetMonthandYear(redirectMonth, redirectYear);
                        }
                        else
                            imgPrevious.Enabled = true;
                    }
                    //Venkatesh : 4C_Support 26-Feb-2014 : End
                 
                }
                else
                {
                    dsEmpDeatils = fourCBAL.Get4CEmployeeDeatils(int.Parse(ViewState["LoginEmpId"].ToString()), int.Parse(ddlDepartment.SelectedItem.Value), projectId, redirectMonth, redirectYear, fourCRole, empId);

                    //Mahendra Start
                    String empIdsList;
                    empIdsList = "";
                    //call read method.
                    for (int iRow = 0; iRow <= dsEmpDeatils.Tables[0].Rows.Count - 1; iRow++)
                    {

                        empIdsList += (dsEmpDeatils.Tables[0].Rows[iRow]["EmpId"].ToString()) + ',';
                        // Add the object to Collection
                    }

                    if (empIdsList.Length != CommonConstants.ZERO)
                    {
                        empIdsList = empIdsList.Substring(0, empIdsList.Length - 1);
                    }
                    
                    //Mahendra 
                    if (ddlDepartment.SelectedItem.Text == "Projects" && ddlProjectList.SelectedItem.Text.ToUpper().Trim() != "BENCH")
                    {
                        dsEmpDeatilsCarryForward = fourCBAL.Get4CEmployeeDeatilsCarryForward(int.Parse(ViewState["LoginEmpId"].ToString()), int.Parse(ddlDepartment.SelectedItem.Value), projectId, redirectMonth, redirectYear, fourCRole, empIdsList);
                        dsEmpDeatilsCarryForward.PrimaryKey = new DataColumn[] { dsEmpDeatilsCarryForward.Columns["EmpId"] };
                        //dsEmpDeatilsCarryForward.pro
                    }


                    ViewState["MaxMonth"] = redirectMonth;
                    ViewState["MaxYear"] = redirectYear;

                    SetMonthandYear(redirectMonth, redirectYear);
                    //Venkatesh : 4C_Support 26-Feb-2014 : Start 
                    //Desc : Dept Support
                    if (Utility.IsSupportDept(int.Parse(ddlDepartment.SelectedItem.Value)))
                    {
                        if (redirectMonth <= Common.CommonConstants.SupportDeptStartMonth && redirectYear == CommonConstants.SupportDeptStartYear)
                            imgPrevious.Enabled = false;
                        else
                            imgPrevious.Enabled = true;
                    }
                    //Venkatesh : 4C_Support 26-Feb-2014 : End
                }

            }
            else
            {
                //get the redirected month and year
                fourCBAL.GetRedirectMonth(int.Parse(ViewState["LoginEmpId"].ToString()), int.Parse(ddlDepartment.SelectedItem.Value), projectId, month, year, 1, ref redirectMonth, ref redirectYear);

                //for bench do not show redirection
                if (ddlDepartment.SelectedItem.Text == "Projects" && ddlProjectList.SelectedItem.Text.ToUpper().Trim() == "BENCH")
                {
                    if (ViewState["ReviewerMaxMonth"] == null)
                    {
                        redirectMonth = month;
                        redirectYear = year;
                    }
                    else
                    {
                        redirectMonth = int.Parse(ViewState["ReviewerMaxMonth"].ToString());
                        redirectYear = int.Parse(ViewState["ReviewerMaxYear"].ToString());
                    }
                }

                //below condition is for redirection.
               // if ((month < redirectMonth && year == redirectYear) || (month > redirectMonth && year < redirectYear))
                if ((month != redirectMonth) && (ViewState["ReviewerMaxMonth"] != null || !string.IsNullOrEmpty(hdPopupReturnMonth.Value)))
                {
                    dsEmpDeatils = fourCBAL.Get4CReviewerEmployeeDeatils(int.Parse(ViewState["LoginEmpId"].ToString()), int.Parse(ddlDepartment.SelectedItem.Value), projectId, month, year, fourCRole, empId);

                    ViewState["ReviewerMaxMonth"] = redirectMonth;
                    ViewState["ReviewerMaxYear"] = redirectYear;

                    SetMonthandYear(month, year);
                    //Venkatesh : 4C_Support 26-Feb-2014 : Start 
                    //Desc : Dept Support
                    if (Utility.IsSupportDept(int.Parse(ddlDepartment.SelectedItem.Value)))
                    {
                        if (month  <= Common.CommonConstants.SupportDeptStartMonth && year == CommonConstants.SupportDeptStartYear)
                            imgPrevious.Enabled = false;
                        else
                            imgPrevious.Enabled = true;
                    }
                    //Venkatesh : 4C_Support 26-Feb-2014 : End
                }
                else
                {
                    dsEmpDeatils = fourCBAL.Get4CReviewerEmployeeDeatils(int.Parse(ViewState["LoginEmpId"].ToString()), int.Parse(ddlDepartment.SelectedItem.Value), projectId, redirectMonth, redirectYear, fourCRole, empId);

                    ViewState["ReviewerMaxMonth"] = redirectMonth;
                    ViewState["ReviewerMaxYear"] = redirectYear;

                    SetMonthandYear(redirectMonth, redirectYear);
                    //Venkatesh : 4C_Support 26-Feb-2014 : Start 
                    //Desc : Dept Support
                    if (Utility.IsSupportDept(int.Parse(ddlDepartment.SelectedItem.Value)))
                    {
                        if (redirectMonth <= Common.CommonConstants.SupportDeptStartMonth && redirectYear == CommonConstants.SupportDeptStartYear)
                            imgPrevious.Enabled = false;
                        else
                            imgPrevious.Enabled = true;
                    }
                    //Venkatesh : 4C_Support 26-Feb-2014 : End
                }
            }

            dsCretorReviewerDeatils = fourCBAL.GetCreatorReviewerDetails(int.Parse(ddlDepartment.SelectedItem.Value), projectId);

            //}

            if (rbAVPView.SelectedIndex == 0 || (rbAVPView.SelectedIndex == 1 && dsEmpDeatils.Tables[0].Rows.Count > 0) && !string.IsNullOrEmpty(dsEmpDeatils.Tables[0].Rows[0]["EMPId"].ToString()))
            {
                grdEmpDetails.DataSource = dsEmpDeatils.Tables[0];
                grdEmpDetails.DataBind();

                divReviewerNoData.Visible = false;
            }
            else if (rbAVPView.SelectedIndex == 1 && (ddlDepartment.SelectedIndex > 0 || (ddlDepartment.SelectedItem.Text == "Projects" && ddlProjectList.SelectedIndex > 0)))
            {
                //lblMessage.Visible = true;
                //lblMessage.Text = "You have not yet created and submitted 4C rating.";
                grdEmpDetails.Visible = false;
                divReviewerNoData.Visible = true;
            }

            if (rbAVPView.SelectedIndex == 0)
            {
                month = redirectMonth;
                year = redirectYear;
            }
            
            ControlEnableDisable(dsEmpDeatils, dsCretorReviewerDeatils, projectId, month, year);
           
            //List<string> lst = (List<string>)ViewState["FourCEmpRole"];

            //if (dsEmpDeatils.Tables[0].Rows.Count > 0 && lst.Any(o => o.ToString() == MasterEnum.FourCRole.CREATOR.ToString()))
            //{
            //    if (ViewState["AllowDirectSubmit"] != null && !string.IsNullOrEmpty(ViewState["AllowDirectSubmit"].ToString()))
            //    {
            //        btnSubmitRating.Visible = true;
            //    }
            //    else
            //    {
            //        if(ddlProjectList.SelectedIndex > 0)
            //            btnSendForReview.Visible = true;
            //    }

            //}
            //else
            //{
            //    btnSendForReview.Visible = false;
            //}

            //DataTable dtRoles = (DataTable)ViewState["FourCEmpRole"];
            //if (dsEmpDeatils.Tables[0].Rows.Count > 0 && (dtRoles.Select("Role = '"+ CommonConstants.FOURCROLE1 +"'").Any() || dtRoles.Select("Role = '"+ CommonConstants.FOURCROLE2 +"'").Any()))
            //{
            //    btnSendForReview.Visible = true;
            //    btnSubmitRating.Visible = false;
            //}
            //if (dtRoles.Select("Role = '"+ CommonConstants.FOURCROLE14 +"'").Any())
            //{
            //    btnSendForReview.Visible = false;
            //    btnSubmitRating.Visible = true;
            //}

            //if (fourCRole == MasterEnum.FourCRole.FOURCADMIN.ToString() && empId == 0)
            if (empId == 0)
            {
                ddlEmployee.ClearSelection();
                ddlEmployee.Items.Clear();

                if (dsEmpDeatils.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(dsEmpDeatils.Tables[0].Rows[0]["EMPId"].ToString()))
                {
                    ddlEmployee.DataSource = dsEmpDeatils.Tables[0];
                    ddlEmployee.DataTextField = dsEmpDeatils.Tables[0].Columns[0].ToString();
                    ddlEmployee.DataValueField = dsEmpDeatils.Tables[0].Columns[2].ToString();
                    ddlEmployee.DataBind();
                }

                ddlEmployee.Items.Insert(0, new ListItem(CommonConstants.SELECT, CommonConstants.ZERO.ToString()));
            }

        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "BindProjectData", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    private void ControlEnableDisable(DataSet dsEmpDeatils, DataSet dsCretorReviewerDeatils, int projectId, int month, int year)
    {
        try
        {

            //SET Manager Name and Reviewer Name
            if (dsCretorReviewerDeatils != null && dsCretorReviewerDeatils.Tables[0].Rows.Count > 0)
            {

                if (dsCretorReviewerDeatils.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(dsCretorReviewerDeatils.Tables[0].Rows[0][0].ToString()))
                    lblManagerName.Text = dsCretorReviewerDeatils.Tables[0].Rows[0][0].ToString();
                else
                    lblManagerName.Text = "";

                if (dsCretorReviewerDeatils.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(dsCretorReviewerDeatils.Tables[0].Rows[0][1].ToString()))
                    lblReviewerName.Text = dsCretorReviewerDeatils.Tables[0].Rows[0][1].ToString();
                else
                    lblReviewerName.Text = "";
            }


            //Set if all employee rating is filled or not 
            ViewState["RatingFillForAllEmployee"] = "";
            bool btnEnableStatus = true;

            for (int iRow = 0; iRow < dsEmpDeatils.Tables[0].Rows.Count; iRow++)
            {
                if (string.IsNullOrEmpty(dsEmpDeatils.Tables[0].Rows[iRow]["Competency"].ToString()) || string.IsNullOrEmpty(dsEmpDeatils.Tables[0].Rows[iRow]["Communication"].ToString()) || string.IsNullOrEmpty(dsEmpDeatils.Tables[0].Rows[iRow]["Commitment"].ToString()) || string.IsNullOrEmpty(dsEmpDeatils.Tables[0].Rows[iRow]["Collaboration"].ToString()))
                {
                    ViewState["RatingFillForAllEmployee"] = "False";
                    break;
                }
                else
                {
                    ViewState["RatingFillForAllEmployee"] = "True";
                }

            }


            //SET SendForReview and SubmitRating Button Enable and Disable
            if (dsEmpDeatils != null && dsEmpDeatils.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dsEmpDeatils.Tables[0].Rows[0]["SendForReview"].ToString()))
                {
                    btnSendForReview.Visible = Convert.ToBoolean(dsEmpDeatils.Tables[0].Rows[0]["SendForReview"].ToString());
                    //tblAVP.Visible = Convert.ToBoolean(dsEmpDeatils.Tables[0].Rows[0]["AddReviewRadioButton"].ToString());
                    //if (btnSendForReview.Visible)
                    //{
                    //    btnSendForReview.Enabled = Convert.ToBoolean(dsEmpDeatils.Tables[0].Rows[0]["SentForReviewEnable"].ToString());
                    //}


                }
                if (!string.IsNullOrEmpty(dsEmpDeatils.Tables[0].Rows[0]["SubmitRating"].ToString()))
                {
                    btnSubmitRating.Visible = Convert.ToBoolean(dsEmpDeatils.Tables[0].Rows[0]["SubmitRating"].ToString());
                    //if (btnSubmitRating.Visible)
                    //{
                    //    btnSubmitRating.Enabled = Convert.ToBoolean(dsEmpDeatils.Tables[0].Rows[0]["RatingSubmittedEnable"].ToString());
                    //}


                }


                for (int iRow = 0; iRow < dsEmpDeatils.Tables[0].Rows.Count; iRow++)
                {
                    HiddenField hdEmployee4CStatus = null;
                    HiddenField hdProjectStatus = null;
                    CheckBox chkSelect = null;

                    if (!string.IsNullOrEmpty(dsEmpDeatils.Tables[0].Rows[0]["EMPId"].ToString()))
                    {
                        chkSelect = (CheckBox)grdEmpDetails.Rows[iRow].FindControl("chkSelect");
                        hdEmployee4CStatus = (HiddenField)grdEmpDetails.Rows[iRow].FindControl("hdEmp4CStatus");
                        hdProjectStatus = (HiddenField)grdEmpDetails.Rows[iRow].FindControl("hdProjectStatus");
                    }
                    if (btnSendForReview.Visible)
                    {
                        if (!string.IsNullOrEmpty(dsEmpDeatils.Tables[0].Rows[iRow]["Employee4CStatus"].ToString()) && int.Parse(hdProjectStatus.Value) != int.Parse(Open4CStatusId) && (int.Parse(hdEmployee4CStatus.Value) == int.Parse(SendForReview4CStatusId.ToString()) || int.Parse(hdEmployee4CStatus.Value) == int.Parse(Reviewed4CStatusId.ToString())))
                        {
                            btnEnableStatus = false;
                        }
                        else
                        {
                            btnEnableStatus = true;
                            break;
                        }
                    }

                    if (btnSubmitRating.Visible)
                    {
                        if (!string.IsNullOrEmpty(dsEmpDeatils.Tables[0].Rows[iRow]["Employee4CStatus"].ToString()) && int.Parse(hdProjectStatus.Value) == int.Parse(Reviewed4CStatusId.ToString()) && int.Parse(hdEmployee4CStatus.Value) == int.Parse(Reviewed4CStatusId.ToString()))
                        {
                            btnEnableStatus = false;
                        }
                        else
                        {
                            btnEnableStatus = true;
                            break;
                        }
                    }
                    //Ishwar Patil 30062015 Start
                    //if (!String.IsNullOrEmpty(hdEmployee4CStatus.Value))
                    //{
                    //    if (Convert.ToString(hdEmployee4CStatus.Value) == Reviewed4CStatusId)
                    //    {
                    //        chkSelect.Enabled = false;
                    //        btnSendForReview.Enabled = false;
                    //        btnSubmitRating.Enabled = false;
                    //        lblMessage.Text = "Please note : The 4C for " + month + "-" + year + " has already been filled.";
                    //    }
                    //}
                    //Ishwar Patil 30062015 End
                }


                if (rbAVPView.SelectedIndex == 1)
                {
                    btnNotApplicable.Visible = false;
                }
                else
                {
                    if (btnSendForReview.Visible || btnSubmitRating.Visible)
                    {
                        btnNotApplicable.Visible = true;
                    }
                    else
                    {
                        btnNotApplicable.Visible = false;
                    }
                }


                if (btnSendForReview.Visible)
                {
                    if (btnEnableStatus)
                    {
                        btnSendForReview.Enabled = true;
                        btnNotApplicable.Enabled = true;
                    }
                    else
                    {
                        btnSendForReview.Enabled = false;
                        btnNotApplicable.Enabled = false;
                    }
                }
                if (btnSubmitRating.Visible)
                {
                    if (btnEnableStatus)
                    {
                        btnSubmitRating.Enabled = true;
                        btnNotApplicable.Enabled = true;
                    }
                    else
                    {
                        btnSubmitRating.Enabled = false;
                        btnNotApplicable.Enabled = false;
                    }
                }


                // to fetch the total emp count, those manager who are in creator + reviewer and in Add 4C mode then call the reviewer SP and take the count.
                if (btnSubmitRating.Visible && rbAVPView.SelectedIndex == 0)
                {
                    Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
                    DataSet dsReviewer = fourCBAL.Get4CReviewerEmployeeDeatils(int.Parse(ViewState["LoginEmpId"].ToString()), int.Parse(ddlDepartment.SelectedItem.Value), projectId, month, year, ViewState["Role"].ToString(), 0); //bcz we are not fetching any seprate employee record.

                    if (!string.IsNullOrEmpty(dsReviewer.Tables[0].Rows[0]["COUNT_TotalReviewEmp"].ToString()))
                    {
                        ViewState["Total_No_Emp"] = int.Parse(dsReviewer.Tables[0].Rows[0]["COUNT_TotalReviewEmp"].ToString());
                        ViewState["Total_Reviewed_Emp"] = int.Parse(dsReviewer.Tables[0].Rows[0]["COUNT_ReviewedEmp"].ToString());
                    }
                }
                else if (btnSubmitRating.Visible)//this is for reviewer where we already fected count.
                {
                    if (!string.IsNullOrEmpty(dsEmpDeatils.Tables[0].Rows[0]["COUNT_TotalReviewEmp"].ToString()))
                    {
                        ViewState["Total_No_Emp"] = int.Parse(dsEmpDeatils.Tables[0].Rows[0]["COUNT_TotalReviewEmp"].ToString());
                        ViewState["Total_Reviewed_Emp"] = int.Parse(dsEmpDeatils.Tables[0].Rows[0]["COUNT_ReviewedEmp"].ToString());
                    }
                }


                if (ViewState["CurrentMonthLastDay"] != null)
                {

                    DateTime dtSelectedDate = DateTime.Parse(string.Concat("01 ", lblDate.Text));
                    DateTime dtCurrentDate = DateTime.Parse(DateTime.Now.ToString("dd MMMM yyyy"));

                    //For Current month do not allow manager to send data for review
                    //if (DateTime.Now != (DateTime)ViewState["CurrentMonthLastDay"] && lblDate.Text == DateTime.Now.ToString("MMMM yyyy"))
                    //if (DateTime.Now.ToString("dd MMMM yyyy") != DateTime.Parse(ViewState["CurrentMonthLastDay"].ToString()).ToString("dd MMMM yyyy") && lblDate.Text == DateTime.Now.ToString("MMMM yyyy"))

                    if (DateTime.Now.ToString("dd MMMM yyyy") != DateTime.Parse(ViewState["CurrentMonthLastDay"].ToString()).ToString("dd MMMM yyyy") && lblDate.Text == DateTime.Now.ToString("MMMM yyyy"))
                    {
                        btnSendForReview.Enabled = false;
                        btnSubmitRating.Enabled = false;
                        btnNotApplicable.Enabled = false;
                    }
                    else if (dtSelectedDate.Date > dtCurrentDate.Date)
                    {
                        btnSendForReview.Enabled = false;
                        btnSubmitRating.Enabled = false;
                        btnNotApplicable.Enabled = false;
                    }
                }

                //set the DIV height
                //divGridViewEmpDetails.Style.Add(
                if (dsEmpDeatils != null && dsEmpDeatils.Tables[0].Rows.Count > 0)
                {
                    int count = dsEmpDeatils.Tables[0].Rows.Count;
                    if (count <= 2)
                    {
                        divGridViewEmpDetails.Style.Add("height", "120px");
                    }
                    else if (count <= 5)
                    {
                        divGridViewEmpDetails.Style.Add("height", "220px");
                    }
                    else if (count <= 7)
                    {
                        divGridViewEmpDetails.Style.Add("height", "240px");
                    }
                    else if (count >= 8)
                    {
                        divGridViewEmpDetails.Style.Add("height", "280px");
                    }
                }


            }
            else
            {

                //Do not show button if No Records found
                btnSendForReview.Visible = false;
                btnSubmitRating.Visible = false;
                //btnNotApplicable.Visible = false;
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ControlEnableDisable", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }



    #region "COMMENT"
    //private void BindData()
    //{

    //    DataTable dt = new DataTable();
    //    dt.Columns.Add("FullName");
    //    dt.Columns.Add("Designation");
    //    dt.Columns.Add("Code");
    //    dt.Columns.Add("Projectname");
    //    dt.Columns.Add("ManagerName");
    //    dt.Columns.Add("compJoiningDate");
    //    dt.Columns.Add("projJoiningDate");

    //    DataRow drow = dt.NewRow();
    //    drow["FullName"] = "Mahendra Bharambe";
    //    drow["Designation"] = "Senior Software Engineer";
    //    drow["Code"] = "1";
    //    drow["Projectname"] = "Resource Management System (RMS)";
    //    drow["ManagerName"] = "Sawita Kamat";
    //    drow["compJoiningDate"] = "17 May 2000";
    //    drow["projJoiningDate"] = "15 Apr 2012";

    //    dt.Rows.Add(drow);

    //    DataRow drow1 = dt.NewRow();
    //    drow1["FullName"] = "Aarohi Sharma";
    //    drow1["Designation"] = "Associate Software Engineer";
    //    drow1["Code"] = "C0236";
    //    drow1["Projectname"] = "Resource Management System (RMS)";
    //    drow1["ManagerName"] = "Sawita Kamat";
    //    drow1["compJoiningDate"] = "17 May 2000";
    //    drow1["projJoiningDate"] = "15 Apr 2012";
    //    dt.Rows.Add(drow1);

    //    DataRow drow2 = dt.NewRow();
    //    drow2["FullName"] = "Abhay Zutshi";
    //    drow2["Designation"] = "Business Analyst";
    //    drow2["Code"] = "P1186";
    //    drow2["Projectname"] = "Resource Management System (RMS)";
    //    drow2["ManagerName"] = "Sawita Kamat";
    //    drow2["compJoiningDate"] = "17 May 2000";
    //    drow2["projJoiningDate"] = "15 Apr 2012";
    //    dt.Rows.Add(drow2);

    //    DataRow drow3 = dt.NewRow();
    //    drow3["FullName"] = "Abhishek Lohiya";
    //    drow3["Designation"] = "Senior Software Engineer";
    //    drow3["Code"] = "C0236";
    //    drow3["Projectname"] = "Resource Management System (RMS)";
    //    drow3["ManagerName"] = "Sawita Kamat";
    //    drow3["compJoiningDate"] = "17 May 2000";
    //    drow3["projJoiningDate"] = "15 Apr 2012";
    //    dt.Rows.Add(drow3);

    //    grdEmpDetails.DataSource = dt;
    //    grdEmpDetails.DataBind();


    //}

    #endregion "COMMENT"



    //protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlDepartment.SelectedItem.Value == "1")
    //    {
    //        ddlProjectList.Enabled = true;
    //    }
    //    else
    //    {
    //        ddlProjectList.Enabled = false;
    //    }
    //}



    protected void rbAVPView_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rbAVPView.SelectedIndex == 0)
            {
                divReviewerNoData.Visible = false;
                lnkExportToExcel.Visible = false;
            }
            else
            {
                lnkExportToExcel.Visible = true;
            }
            

            //Reset the controls
            grdEmpDetails.Visible = false;
            btnSendForReview.Visible = false;
            btnSubmitRating.Visible = false;
            btnNotApplicable.Visible = false;

            lblManagerName.Text = "";
            lblReviewerName.Text = "";

            ViewState["ClickCount"] = "1";
            lblDate.Text = DateTime.Now.AddMonths(-1).ToString("MMMM yyyy");
            //Show Current Month data
            //imgNext.Enabled = false;
            imgNext.Enabled = true;
            //Reset the controls

            //Fill the department and project dropdown
            FillDepartment();
            FillProjectNameDropDown();

            ddlProjectList.Enabled = false;
            ddlEmployee.Items.Clear();
            ddlEmployee.Items.Insert(0, new ListItem(CommonConstants.SELECT, CommonConstants.ZERO.ToString()));
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlDepartmentFilter_SelectedIndexChanged", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            

            //if (ddlDepartment.SelectedValue != "0")
            //{
                if (ddlDepartment.SelectedValue != "0" && int.Parse(ddlDepartment.SelectedValue) != Convert.ToInt32(MasterEnum.Departments.Projects))
                {

                    //if (!ddlProjectList.Items.Contains(new ListItem(CommonConstants.BENCH_DEPARTMENT, CommonConstants.BENCH_VALUE.ToString())))
                    //{

                    //    ddlProjectList.Items.Remove(new ListItem(CommonConstants.BENCH_PROJECT, CommonConstants.BENCH_VALUE.ToString()));
                    //    ddlProjectList.Items.Add(new ListItem(CommonConstants.BENCH_DEPARTMENT, CommonConstants.BENCH_VALUE.ToString()));
                    //}
                    //ddlProjectList.SelectedIndex = 0;
                    //ddlProjectList.SelectedItem.Text = CommonConstants.BENCH_DEPARTMENT;


                    ddlProjectList.SelectedValue = "0";
                    ddlProjectList.Enabled = false;
                    ddlEmployee.Items.Clear();

                    ViewState["ReviewerMaxMonth"] = null;
                    ViewState["ReviewerMaxYear"] = null;
                    ViewState["MaxMonth"] = null;
                    ViewState["MaxYear"] = null;

                    DateTime monthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));
                    //BindProjectData(int.Parse(ddlDepartment.SelectedItem.Value), int.Parse(ddlProjectList.SelectedItem.Value), monthYear.Month, monthYear.Year);

                    //Venkatesh : 4C_Support 26-Feb-2014 : Start 
                    //Desc : Dept Support
                    if (Utility.IsSupportDept(int.Parse(ddlDepartment.SelectedItem.Value)))
                    {
                        if (monthYear.Month <= Common.CommonConstants.SupportDeptStartMonth && monthYear.Year == CommonConstants.SupportDeptStartYear)
                            imgPrevious.Enabled = false;
                        else
                            imgPrevious.Enabled = true;
                    }
                    //Venkatesh : 4C_Support 26-Feb-2014 : End
                    BindProjectData(int.Parse(ddlProjectList.SelectedItem.Value), monthYear.Month, monthYear.Year, ViewState["Role"].ToString());
                }
                else
                {
                    imgPrevious.Enabled = true;
                    ddlProjectList.Enabled = true;
                    ddlEmployee.Items.Clear();
                    
                    //if (!ddlProjectList.Items.Contains(new ListItem(CommonConstants.SELECT, CommonConstants.ZERO.ToString())))
                    //{
                        ddlEmployee.Items.Insert(0, new ListItem(CommonConstants.SELECT, CommonConstants.ZERO.ToString()));
                    //}

                    //if(!ddlProjectList.Items.Contains(new ListItem(CommonConstants.BENCH_PROJECT, CommonConstants.BENCH_VALUE.ToString())))
                    //{
                    //    //ddlProjectList.Items.Remove(new ListItem(CommonConstants.BENCH_DEPARTMENT, CommonConstants.BENCH_VALUE.ToString()));
                    //    //ddlProjectList.Items.Add(new ListItem(CommonConstants.BENCH_PROJECT, CommonConstants.BENCH_VALUE.ToString()));
                    //}
                    //ddlProjectList.SelectedIndex = 0;

                    //ddlProjectList.SelectedItem.Text = CommonConstants.SELECT;
                     ddlProjectList.SelectedValue = "0";

                     if (ddlDepartment.SelectedItem.Text != MasterEnum.Departments.Projects.ToString())
                         ddlProjectList.Enabled = false;

                     lblManagerName.Text = "";
                     lblReviewerName.Text = "";

                    grdEmpDetails.Visible = false;

                    // do not show previous state
                    divReviewerNoData.Visible = false;
                    lblMessage.Visible = false;

                    //
                    if (ddlDepartment.SelectedIndex == 0 && ddlProjectList.SelectedIndex == 0)
                    {
                        
                        
                        FillEmployeeDDL();
                    }

                }
            //}
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlDepartmentFilter_SelectedIndexChanged", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    private void FillDepartment()
    {
        //Declaring COllection class object
        //BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        //Declaring Master Class Object
        //Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();


        DataSet dsDepartment = null; 
        Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
        try
        {

            if (ViewState["Role"].ToString().Trim() == MasterEnum.FourCRole.FOURCADMIN.ToString())
            {
                //the dropdown from Master class.
          //      raveHRCollection = master.FillDepartmentDropDownBL();

                dsDepartment = fourCBAL.GetDepartmentName("");
            }
            else
            {
                if (rbAVPView.SelectedIndex == 0)
                {
                    dsDepartment = fourCBAL.GetDepartmentName(UserMailId, ViewState["Role"] == null ? null : ViewState["Role"].ToString(), CommonConstants.FOURCTypeADD);
                }
                else
                {
                    // for review
                    dsDepartment = fourCBAL.GetDepartmentName(UserMailId, ViewState["Role"] == null ? null : ViewState["Role"].ToString(), CommonConstants.FOURCTypeReview);
                }

                //ddlDepartment.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
                ddlDepartment.Items.Insert(0, new ListItem(CommonConstants.SELECT, CommonConstants.ZERO.ToString()));
            }

            if (dsDepartment != null)
            {
                ddlDepartment.Items.Clear();

                ddlDepartment.DataSource = dsDepartment;
                ddlDepartment.DataTextField = dsDepartment.Tables[0].Columns[1].ToString();
                ddlDepartment.DataValueField = dsDepartment.Tables[0].Columns[0].ToString();

                ddlDepartment.DataBind();
                //ddlDepartment.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
                ddlDepartment.Items.Insert(0, new ListItem(CommonConstants.SELECT, CommonConstants.ZERO.ToString()));

                //remove the Dept 
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

            }

        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetEmployeeDepartment", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }



    /// <summary>
    /// Fills the Project Name dropdown
    /// </summary>
    private void FillProjectNameDropDown()
    {
        // Initialise Collection class object
        //BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        // Initialise Business layer object
        //Rave.HR.BusinessLayer.MRF.MRFDetail mrfProjectName = new Rave.HR.BusinessLayer.MRF.MRFDetail();
        try
        {
            DataSet dsProject = null;
            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
            //if (ViewState["FourCRole"].ToString() == MasterEnum.FourCRole.FOURCADMIN.ToString())
            if (ViewState["Role"].ToString().Trim() == MasterEnum.FourCRole.FOURCADMIN.ToString())
            {
                // Call the Business layer method
                //raveHRCollection = mrfProjectName.GetProjectName();
                dsProject = fourCBAL.GetProjectName();
                //DataSet ds = (DataSet)raveHRCollection;
            }
            else
            {
                //raveHRCollection = fourCBAL.GetProjectName(UserMailId, ViewState["AllowDirectSubmit"] == null ? null : ViewState["AllowDirectSubmit"].ToString());
                if (rbAVPView.SelectedIndex == 0)
                {
                    dsProject = fourCBAL.GetProjectName(UserMailId, ViewState["Role"] == null ? null : ViewState["Role"].ToString(), CommonConstants.FOURCTypeADD);
                }
                else
                {
                    dsProject = fourCBAL.GetProjectName(UserMailId, ViewState["Role"] == null ? null : ViewState["Role"].ToString(), CommonConstants.FOURCTypeReview);   
                }
            }

            if (dsProject != null)
            {
                // Assign the data source to dropdown
                ddlProjectList.Items.Clear();
                //ddlProjectList.DataSource = raveHRCollection;
                //ddlProjectList.DataTextField = CommonConstants.DDL_DataTextField;
                //ddlProjectList.DataValueField = CommonConstants.DDL_DataValueField;
                ddlProjectList.DataSource = dsProject;
                ddlProjectList.DataTextField = dsProject.Tables[0].Columns[1].ToString();
                ddlProjectList.DataValueField = dsProject.Tables[0].Columns[0].ToString();

                // Bind the data to dropdown
                ddlProjectList.DataBind();

                // Default value of dropdown is "Select"
                //ddlProjectList.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
                ddlProjectList.Items.Insert(0, new ListItem(CommonConstants.SELECT, CommonConstants.ZERO.ToString()));
                //ddlProjectList.Items.Add(new ListItem(CommonConstants.SELECT, CommonConstants.ZERO.ToString()));

                if (!IsPostBack)
                {
                    if (ViewState["Role"].ToString().Trim() == MasterEnum.FourCRole.FOURCADMIN.ToString())
                    {
                        ddlProjectList.Items.Add(new ListItem(CommonConstants.BENCH_PROJECT, CommonConstants.BENCH_VALUE.ToString()));
                    }
                }

                //ddlProjectList.Enabled = false;

                //if(!string.IsNullOrEmpty(lblDate.Text))
                //    DateTime monthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));

                //List<string> lst = (List<string>)ViewState["FourCEmpRole"];
                ////If login person is not admin then by default one project get selected
                ////if ((ViewState["FourCRole"].ToString() != MasterEnum.FourCRole.FOURCADMIN.ToString()) && (lst.Any(o => o.ToString() != MasterEnum.FourCRole.CREATORANDREVIEWER.ToString()) && lst.Any(o => o.ToString() == MasterEnum.FourCRole.CREATOR.ToString())))
                //if (lst.Any(o => o.ToString() == MasterEnum.FourCRole.CREATOR.ToString()) && string.IsNullOrEmpty(ViewState["AllowDirectSubmit"].ToString()))
                //{
                //    ddlProjectList.SelectedIndex = 1;
                //    BindProjectData(int.Parse(ddlProjectList.SelectedItem.Value), monthYear.Month, monthYear.Year, ViewState["FourCRole"].ToString());
                //}
                //else
                //{
                //    BindProjectData(int.Parse(ddlProjectList.SelectedItem.Value), monthYear.Month, monthYear.Year, ViewState["FourCRole"].ToString());
                //}
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "FillProjectNameDropDown", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }


    /// <summary>
    /// ddlContractType's Selected Index Changed event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlProjectList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime monthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));
            int projectId = int.Parse(ddlProjectList.SelectedItem.Value);

            //if (ddlProjectList.SelectedIndex > 0)
            //{
            //    BindProjectData(projectId, monthYear.Month, monthYear.Year, ViewState["FourCRole"].ToString());
            //}

            //if (ddlProjectList.SelectedIndex == 0 && ddlEmployee.SelectedIndex > 0)
            //{
            //    BindProjectData(projectId, monthYear.Month, monthYear.Year, ViewState["FourCRole"].ToString());
            //}

            if (ddlDepartment.SelectedIndex == 0 && ddlProjectList.SelectedIndex == 0 && ddlEmployee.SelectedIndex == 0)
            {
                grdEmpDetails.Visible = false;
                FillProjectNameDropDown();
                FillEmployeeDDL();
                lblManagerName.Text = "";
                lblReviewerName.Text = "";

                if (btnSendForReview.Visible)
                {
                    btnSendForReview.Visible = false;
                }
                if (btnSubmitRating.Visible)
                {
                    btnSubmitRating.Visible = false;
                }
                if (btnNotApplicable.Visible)
                {
                    btnNotApplicable.Visible = false;
                }
                //if (((btnSendForReview.Visible && !btnSubmitRating.Visible) || (!btnSendForReview.Visible && btnSubmitRating.Visible)))
                //{
                //    btnNotApplicable.Visible = true;
                //}

            }
            else
            {
                ddlEmployee.Items.Clear();
                grdEmpDetails.Visible = false;
                lblManagerName.Text = "";
                lblReviewerName.Text = "";

                if (!ddlEmployee.Items.Contains(new ListItem(CommonConstants.SELECT, CommonConstants.ZERO.ToString())))
                {
                    ddlEmployee.Items.Insert(0, new ListItem(CommonConstants.SELECT, CommonConstants.ZERO.ToString()));
                }

                if (btnSendForReview.Visible)
                {
                    btnSendForReview.Visible = false;
                }
                if (btnSubmitRating.Visible)
                {
                    btnSubmitRating.Visible = false;
                }
                if (btnNotApplicable.Visible)
                {
                    btnNotApplicable.Visible = false;
                }

                if (ddlDepartment.SelectedIndex != 0 && ddlProjectList.SelectedIndex != 0)
                {
                    //Redirecttion to specific month
                    Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();

                    ViewState["ReviewerMaxMonth"] = null;
                    ViewState["ReviewerMaxYear"] = null;
                    ViewState["MaxMonth"] = null;
                    ViewState["MaxYear"] = null;

                    //fourCBAL. (int.Parse(ViewState["LoginEmpId"].ToString()), int.Parse(ddlDepartment.SelectedItem.Value), projectId, month, year, fourCRole, empId);
                    //fourCBAL.GetRedirectMonth(int.Parse(ViewState["LoginEmpId"].ToString()), int.Parse(ddlDepartment.SelectedItem.Value), projectId, monthYear.Month, monthYear.Year, ref redirectMonth, ref redirectYear);
                    
                    BindProjectData(projectId, monthYear.Month, monthYear.Year, ViewState["Role"].ToString());
                }
            }

        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
            {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlProjectList_SelectedIndexChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// ddlContractType's Selected Index Changed event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime monthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));

            if (ddlProjectList.SelectedIndex > 0 || ddlDepartment.SelectedIndex > 0)
            {
                // Project first selected and then employee selected
                //if (ddlEmployee.SelectedIndex > 0)
                //{
                BindProjectData(int.Parse(ddlProjectList.SelectedItem.Value), monthYear.Month, monthYear.Year, ViewState["Role"].ToString());
                //}
            }
            else
            {
                Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
                //DataSet dsProject = fourCBAL.FillProjectDropdownOnEmp(int.Parse(ddlEmployee.SelectedItem.Value), monthYear.Month, monthYear.Year, "");
                DataSet dsProject = fourCBAL.FillProjectDropdownOnEmp(int.Parse(ddlEmployee.SelectedItem.Value), monthYear.Month, monthYear.Year);
                
                //if Admin select first employoee
                if (ddlProjectList.SelectedIndex == 0 && ddlDepartment.SelectedIndex == 0 && ViewState["Role"] != null && ViewState["Role"].ToString().Trim() == MasterEnum.FourCRole.FOURCADMIN.ToString())
                {
                    if (dsProject != null && dsProject.Tables[1].Rows.Count > 0)
                    {
                        ddlDepartment.Items.Clear();

                        ddlDepartment.DataSource = dsProject.Tables[1];
                        ddlDepartment.DataTextField = dsProject.Tables[1].Columns[1].ToString();
                        ddlDepartment.DataValueField = dsProject.Tables[1].Columns[0].ToString();

                        ddlDepartment.DataBind();
                        //ddlDepartment.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
                        ddlDepartment.Items.Insert(0, new ListItem(CommonConstants.SELECT, CommonConstants.ZERO.ToString()));

                        //remove the Dept Name called RaveDevelopment from Dropdown -Vandna
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
                    }
                }

                //Fill project dropdown
                ddlProjectList.Items.Clear();
                
                
                ddlProjectList.DataSource = dsProject;
                ddlProjectList.DataValueField = dsProject.Tables[0].Columns[0].ToString();
                ddlProjectList.DataTextField = dsProject.Tables[0].Columns[1].ToString();
                ddlProjectList.DataBind();

                ddlProjectList.Items.Insert(0, new ListItem(CommonConstants.SELECT, CommonConstants.ZERO.ToString()));
                //ddlProjectList.Items.Add(new ListItem(CommonConstants.SELECT, CommonConstants.ZERO.ToString()));

            }

            if (ddlProjectList.SelectedIndex == 0 && ddlEmployee.SelectedIndex == 0 && ddlDepartment.SelectedIndex == 0)
            {
                grdEmpDetails.Visible = false;
                FillProjectNameDropDown();
                FillEmployeeDDL();
                lblManagerName.Text = "";
                if (btnSendForReview.Visible)
                {
                    btnSendForReview.Visible = false;
                }
                if (btnSubmitRating.Visible)
                {
                    btnSubmitRating.Visible = false;
                }

                //if (((btnSendForReview.Visible && !btnSubmitRating.Visible) || (!btnSendForReview.Visible && btnSubmitRating.Visible)) && rbAVPView.SelectedIndex == 0)
                //{
                //    btnNotApplicable.Visible = true;
                //}
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlEmployee_SelectedIndexChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    


    /// <summary>
    /// Fill Employee Name 
    /// </summary>
    private void FillEmployeeDDL()
    {
        try
        {
            ddlEmployee.Items.Clear();

            //if (ViewState["FourCRole"] != null && ViewState["FourCRole"].ToString().Trim() == MasterEnum.FourCRole.FOURCADMIN.ToString())
            //if (ViewState["Role"].ToString().Trim() == CommonConstants.FOURCROLE14)
            if (ViewState["Role"] != null && ViewState["Role"].ToString().Trim() == MasterEnum.FourCRole.FOURCADMIN.ToString())
            {
                DataSet ds = new DataSet();
                Rave.HR.BusinessLayer.Employee.Employee addEmployeeBAL = new Rave.HR.BusinessLayer.Employee.Employee();
                ds = addEmployeeBAL.GetActiveEmployeeList();
                ddlEmployee.DataSource = ds.Tables[0];
                ddlEmployee.DataTextField = ds.Tables[0].Columns[1].ToString();
                ddlEmployee.DataValueField = ds.Tables[0].Columns[0].ToString();
                ddlEmployee.DataBind();
            }
            ddlEmployee.Items.Insert(0, new ListItem(CommonConstants.SELECT, CommonConstants.ZERO.ToString()));
        }
        
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "FillEmployeeDDL", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    private void SetExportToExcelSentForReviewLink()
    {
        //btn export to excel Sent for review ratings
        //if last day of month and 4C current month then export to excel should show visible
        if (rbAVPView.SelectedIndex == 1)
        {
            if (lblDate.Text == DateTime.Now.AddMonths(-1).ToString("MMMM yyyy") || DateTime.Now.ToString("dd MMMM yyyy") == DateTime.Parse(ViewState["CurrentMonthLastDay"].ToString()).ToString("dd MMMM yyyy"))
            {
                lnkExportToExcel.Visible = true;
            }
            else
            {
                lnkExportToExcel.Visible = false;
            }
        }
    }

    //mahendra start
    private void CheckFinalSubmit()
    {
        string checkSubmit = null;
        foreach (GridViewRow gvRow in grdEmpDetails.Rows)
        {
            CheckBox chkSelect = (CheckBox)gvRow.FindControl("chkSelect");
            if (chkSelect.Checked)
            {
                checkSubmit = "YES";
            }
            else
            {
                checkSubmit = "NO";
                break;
            }
        }
        if (checkSubmit.Equals("YES"))
        {
            ViewState["RatingFillForAllEmployee"] = "True";
        }
    }
    //mahendra end

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //if (ViewState["Role"].ToString() == MasterEnum.FourCRole.FOURCADMIN.ToString())
            //{               
                grdEmpDetails.Visible = false;
                if (ddlEmployee.SelectedIndex > 0 || ddlProjectList.SelectedIndex > 0 || ddlDepartment.SelectedIndex > 0 || (lblDate.Text != DateTime.Now.AddMonths(-1).ToString("MMMM yyyy")))
                {
                    //Reset the controls
                    grdEmpDetails.Visible = false;
                    btnSendForReview.Visible = false;
                    btnSubmitRating.Visible = false;
                    btnNotApplicable.Visible = false;
                    lblManagerName.Text = "";
                    lblReviewerName.Text = "";

                    ViewState["ClickCount"] = "1";
                    lblDate.Text = DateTime.Now.AddMonths(-1).ToString("MMMM yyyy");
                    //Show Current month data
                    //imgNext.Enabled = false;
                    //Reset the controls

                    //Fill the department and project dropdown
                    FillDepartment();
                    FillProjectNameDropDown();
                    ddlProjectList.Enabled = false;
                    FillEmployeeDDL();
                    lblManagerName.Text = "";
                    divReviewerNoData.Visible = false;
                }
                else
                {
                    Response.Redirect("~/Home.aspx", false);
                }
            //}
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnCancel_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    protected void btnSubmitRating_Click(object sender, EventArgs e)
    {
        try
        {
            //Check if all record filled 
            bool flag = false;
            bool flagRatingSubmit = false;
            DataTable dtFinalRating= null;

            //string ratingOption = "";

            string ratingOption = REVIEWED;
            string ratingType = "";

            if (rbAVPView.SelectedIndex == 0)
            {
                ratingType = ADD_REVIEWRATING;
            }
            else
            {
                ratingType = SUBMIT_REVIEW;
            }

            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
            DateTime dtMonthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));

            //flag = fourCBAL.CheckRatingFillForAll(ratingOption, ratingType, int.Parse(ddlDepartment.SelectedItem.Value), int.Parse(ddlProjectList.SelectedItem.Value), dtMonthYear.Month, dtMonthYear.Year, int.Parse(ViewState["LoginEmpId"].ToString()));

                  //submit rating for this month

            //if (ViewState["RatingFillForAllEmployee"] != null && ViewState["RatingFillForAllEmployee"].ToString() != "True")
            //{
            ////if (flag)
            ////{
            //    //Validation Message
            //    lblMessage.Visible = true;
            //    //lblMessage.CssClass = "messagetext";
            //    lblMessage.ForeColor = System.Drawing.Color.Red;
            //    lblMessage.Text = "Please Enter 4C Rating For All Employees.";
            //}
            //else
            //{

            string finalSubmit = "NO";

            //mahendra start
            CheckFinalSubmit();
            //mahendra end

            if (ViewState["RatingFillForAllEmployee"] != null && ViewState["RatingFillForAllEmployee"].ToString() == "True")
            {
                finalSubmit = "YES";
            }
            bool ratingFillAllCheckedRecords = true;
            int Reviewer_Submit_rating_Count = 0;

            dtFinalRating = new DataTable();
            dtFinalRating.Columns.Add(DbTableColumn.FourCId);
            //mahendra start
            dtFinalRating.Columns.Add(DbTableColumn.EMPId);
            dtFinalRating.Columns.Add(DbTableColumn.Flag);
            //mahendra end

            foreach (GridViewRow gvRow in grdEmpDetails.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvRow.FindControl("chkSelect");
                HiddenField hdFBID = (HiddenField)gvRow.FindControl("hdFBID");
                HiddenField hdCompetency = (HiddenField)gvRow.FindControl("hdCompetency");
                HiddenField hdCommunication = (HiddenField)gvRow.FindControl("hdCommunication");
                HiddenField hdCommitment = (HiddenField)gvRow.FindControl("hdCommitment");
                HiddenField hdCollaboration = (HiddenField)gvRow.FindControl("hdCollaboration");

                //mahendra start
                HiddenField hdEmpId = (HiddenField)gvRow.FindControl("hdEmpId");
                HiddenField hdFlag = (HiddenField)gvRow.FindControl("hdFlag");
                //mahendra end

                if (chkSelect.Enabled && chkSelect.Checked)
                {
                    if (string.IsNullOrEmpty(hdCompetency.Value) || string.IsNullOrEmpty(hdCommunication.Value) || string.IsNullOrEmpty(hdCommitment.Value) || string.IsNullOrEmpty(hdCollaboration.Value))
                    {
                        ratingFillAllCheckedRecords = false;
                        break;
                    }

                    DataRow dr = dtFinalRating.NewRow();
                    dr[DbTableColumn.FourCId] = hdFBID.Value;
                    //mahendra start
                    dr[DbTableColumn.EMPId] = hdEmpId.Value;
                    dr[DbTableColumn.Flag] = hdFlag.Value;
                    //mahendra end
                    dtFinalRating.Rows.Add(dr);

                    Reviewer_Submit_rating_Count = Reviewer_Submit_rating_Count + 1;
                }
                

                if (!chkSelect.Checked) // THis is for reviewer as well as creator to marks project status. // specially who are only creator and checked all record and do send for review.
                {
                    finalSubmit = "NO";
                }
            }

            if (ratingFillAllCheckedRecords == false)
            {
                lblMessage.Visible = true;
                //lblMessage.CssClass = "messagetext";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Please enter 4C rating of the selected employee for each C.";
                return;
            }

            //Reviewer calculate final submit when in Add 4C Mode.
            //when mode 0 i.e is Add just checked all data is filled or not. but when in review page then need to calculate total no of emoplyee to review and reviewed employee.
            //final submit = Total No of Employee To Review - Total Emp Reviewed (only which creator sent) - Total employee To Review (Reviewer_Submit_rating_Count) == 0 then final submit = Yes
            if (finalSubmit == "YES" && rbAVPView.SelectedIndex == 1)
            {
                if (ViewState["Total_No_Emp"] != null && !string.IsNullOrEmpty(ViewState["Total_No_Emp"].ToString()))
                {
                    if ((int.Parse(ViewState["Total_No_Emp"].ToString()) - int.Parse(ViewState["Total_Reviewed_Emp"].ToString()) - Reviewer_Submit_rating_Count) == 0)
                    {
                        finalSubmit = "YES";
                    }
                    else
                    {
                        finalSubmit = "NO";
                    }
                }
            }


            //flagRatingSubmit = fourCBAL.SubmitReviewRating(UserMailId, ratingOption, dtFinalRating);   

            if (dtFinalRating.Rows.Count > 0)
            {
                flagRatingSubmit = fourCBAL.SubmitReviewRating(ratingOption, finalSubmit, int.Parse(ddlDepartment.SelectedItem.Value), int.Parse(ddlProjectList.SelectedItem.Value), dtMonthYear.Month, dtMonthYear.Year, int.Parse(ViewState["LoginEmpId"].ToString()), dtFinalRating);
            }
            BindProjectData(int.Parse(ddlProjectList.SelectedItem.Value), dtMonthYear.Month, dtMonthYear.Year, ViewState["Role"].ToString());

                //fourCBAL.SendMailForSendForReview();

                //if (flagRatingSubmit)
                //{
                //    btnSubmitRating.Enabled = false;
                //    //send mail
                //}
            //}
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnCancel_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnSendForReview_Click(object sender, EventArgs e)
    {
        try
        {
            bool flag = false;
            bool flagRatingSubmit = false;
            DataTable dtFinalRating = null;
            string ratingOption = SEND_FOR_REVIEW;

            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
            DateTime dtMonthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));
            

            //Final Submit
            string finalSubmit = "NO";

            //mahendra start
            CheckFinalSubmit();
            //mahendra end

            if (ViewState["RatingFillForAllEmployee"] != null && ViewState["RatingFillForAllEmployee"].ToString() == "True")
            {
                finalSubmit = "YES"; // below checked if all checkboxes are checked or not for final submit.
            }

            bool ratingFillAllCheckedRecords = true;
            


            dtFinalRating = new DataTable();
            dtFinalRating.Columns.Add(DbTableColumn.FourCId);
            //dtFinalRating.Columns.Add(DbTableColumn.FourCId);
            //mahendra start
            dtFinalRating.Columns.Add(DbTableColumn.EMPId);
            dtFinalRating.Columns.Add(DbTableColumn.Flag);
            //mahendra end

            foreach (GridViewRow gvRow in grdEmpDetails.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvRow.FindControl("chkSelect");
                HiddenField hdFBID = (HiddenField)gvRow.FindControl("hdFBID");
                HiddenField hdCompetency = (HiddenField)gvRow.FindControl("hdCompetency");
                HiddenField hdCommunication = (HiddenField)gvRow.FindControl("hdCommunication");
                HiddenField hdCommitment = (HiddenField)gvRow.FindControl("hdCommitment");
                HiddenField hdCollaboration = (HiddenField)gvRow.FindControl("hdCollaboration");

                //mahendra start
                HiddenField hdEmpId = (HiddenField)gvRow.FindControl("hdEmpId");
                HiddenField hdFlag = (HiddenField)gvRow.FindControl("hdFlag");
                //mahendra end

                if (chkSelect.Enabled && chkSelect.Checked)
                {
                    if (string.IsNullOrEmpty(hdCompetency.Value) || string.IsNullOrEmpty(hdCommunication.Value) || string.IsNullOrEmpty(hdCommitment.Value) || string.IsNullOrEmpty(hdCollaboration.Value))
                    {
                        ratingFillAllCheckedRecords = false;
                        break;
                    }
                    
                    DataRow dr = dtFinalRating.NewRow();
                    dr[DbTableColumn.FourCId] = hdFBID.Value;
                    //mahendra start
                    dr[DbTableColumn.EMPId] = hdEmpId.Value;
                    dr[DbTableColumn.Flag] = hdFlag.Value;
                    //mahendra end
                    dtFinalRating.Rows.Add(dr);

                    //Reviewer_Submit_rating_Count = Reviewer_Submit_rating_Count + 1;
                }

                if (!chkSelect.Checked) // THis is for reviewer as well as creator to marks project status. // specially who are only creator and checked all record and do send for review.
                {
                    finalSubmit = "NO";
                }
            }

            if (ratingFillAllCheckedRecords == false)
            {
                lblMessage.Visible = true;
                //lblMessage.CssClass = "messagetext";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Please enter 4C rating of the selected employee for each C.";
                return;
            }

            if (dtFinalRating.Rows.Count > 0)
            {
                //flagRatingSubmit = fourCBAL.SubmitReviewRating(UserMailId, ratingOption, dtFinalRating);
                flagRatingSubmit = fourCBAL.SubmitReviewRating(ratingOption, finalSubmit, int.Parse(ddlDepartment.SelectedItem.Value), int.Parse(ddlProjectList.SelectedItem.Value), dtMonthYear.Month, dtMonthYear.Year, int.Parse(ViewState["LoginEmpId"].ToString()), dtFinalRating);

                // Mail to the Reviewer
                if (finalSubmit == "YES" && int.Parse(ddlDepartment.SelectedItem.Value) == 1 && int.Parse(ddlProjectList.SelectedItem.Value) != 0 && int.Parse(ddlProjectList.SelectedItem.Value) != int.Parse(CommonConstants.BENCH_VALUE.ToString()))
                {
                    fourCBAL.SendMailForSendForReview(int.Parse(ddlDepartment.SelectedItem.Value), int.Parse(ddlProjectList.SelectedItem.Value), lblDate.Text, int.Parse(ViewState["LoginEmpId"].ToString()));
                }
                else if (finalSubmit == "YES" && int.Parse(ddlDepartment.SelectedItem.Value) != 1 && int.Parse(ddlProjectList.SelectedItem.Value) == 0)
                {
                    fourCBAL.SendMailForSendForReviewSupport(int.Parse(ddlDepartment.SelectedItem.Value), int.Parse(ddlProjectList.SelectedItem.Value), lblDate.Text, int.Parse(ViewState["LoginEmpId"].ToString()));
                }

            }

            BindProjectData(int.Parse(ddlProjectList.SelectedItem.Value), dtMonthYear.Month, dtMonthYear.Year, ViewState["Role"].ToString());
                //if (flagRatingSubmit)
                //{
                //     btnSendForReview.Enabled = false;
                //    //send mail
                //}
            //}
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnSendForReview_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnNotApplicable_Click(object sender, EventArgs e)
    {
        try
        {
            FourCFeedback objFeedBack;
            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
            DateTime dtMonthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));
            string strEmployeeNameNotValidFor_NA = "";        

            foreach (GridViewRow gvRow in grdEmpDetails.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvRow.FindControl("chkSelect");

                HiddenField hdFBID = (HiddenField)gvRow.FindControl("hdFBID");
                HiddenField hdEmpId = (HiddenField)gvRow.FindControl("hdEmpId");
                
                HiddenField hdCompetency = (HiddenField)gvRow.FindControl("hdCompetency");
                HiddenField hdCommunication = (HiddenField)gvRow.FindControl("hdCommunication");
                HiddenField hdCommitment = (HiddenField)gvRow.FindControl("hdCommitment");
                HiddenField hdCollaboration = (HiddenField)gvRow.FindControl("hdCollaboration");
                HyperLink hypEmpName = (HyperLink)gvRow.FindControl("hypEmpName");

                if (chkSelect.Enabled && chkSelect.Checked)
                {
                    bool flagVal = false;
                    flagVal = fourCBAL.IsValidForNotApplication(int.Parse(ddlDepartment.SelectedItem.Value), int.Parse(ddlProjectList.SelectedItem.Value), dtMonthYear.Month, dtMonthYear.Year, int.Parse(hdEmpId.Value));

                    if (flagVal)
                    {

                        objFeedBack = fourCBAL.Get4CIndividualEmployeeDeatils(int.Parse(ddlDepartment.SelectedItem.Value), int.Parse(ddlProjectList.SelectedItem.Value), dtMonthYear.Month, dtMonthYear.Year, int.Parse(hdEmpId.Value));
                        if (!string.IsNullOrEmpty(hdFBID.Value) && int.Parse(hdFBID.Value) != 0)
                        {
                            objFeedBack.FBID = int.Parse(hdFBID.Value);
                        }
                        else
                        {
                            objFeedBack.FBID = 0;
                        }
                        objFeedBack.Collaboration = NotApplicableStatus;
                        objFeedBack.Commitment = NotApplicableStatus;
                        objFeedBack.Communication = NotApplicableStatus;
                        objFeedBack.Competency = NotApplicableStatus;
                        objFeedBack.ModifiedById = UserMailId;

                        DataTable dtActionData = null;
                        //if (string.IsNullOrEmpty(hdCompetency.Value) && string.IsNullOrEmpty(hdCommunication.Value) && string.IsNullOrEmpty(hdCommitment.Value) && string.IsNullOrEmpty(hdCollaboration.Value))
                        //{
                        fourCBAL.InsertActionDetails(dtActionData, objFeedBack);
                        //}
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(strEmployeeNameNotValidFor_NA))
                        {
                            strEmployeeNameNotValidFor_NA = hypEmpName.Text;
                        }
                        else
                        {
                            strEmployeeNameNotValidFor_NA = strEmployeeNameNotValidFor_NA + ", " + hypEmpName.Text;
                        }
                    }
                }

                BindProjectData(int.Parse(ddlProjectList.SelectedItem.Value), dtMonthYear.Month, dtMonthYear.Year, ViewState["Role"].ToString());

                if (!string.IsNullOrEmpty(strEmployeeNameNotValidFor_NA))
                {
                    lblMessage.Visible = true;
                    //lblMessage.CssClass = "messagetext";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    //lblMessage.Text = strEmployeeNameNotValidFor_NA + " Employee/s selected cannot be termed as Not Applicable.";
                    lblMessage.Text = strEmployeeNameNotValidFor_NA + " cannot be termed as Not Applicable.";
                    return;
                }

            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnNotApplicable_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            GridViewRow clickedRow = ((CheckBox)sender).NamingContainer as GridViewRow;
            CheckBox chkSelect = (CheckBox)clickedRow.FindControl("chkSelect");
            Label lblGrdCompetency = (Label)clickedRow.FindControl("lblGrdCompetency");

            HyperLink hypEmpName = (HyperLink)clickedRow.FindControl("hypEmpName");
            

            if (lblGrdCompetency.Text.Trim() == NotApplicableStatus)
            {
                if (!chkSelect.Checked)
                {
                    hypEmpName.Enabled = true;
                }
                else
                {
                    hypEmpName.Enabled = false;
                }
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "chkSelect_CheckedChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    protected void lnkRejectRating_Click(object sender, CommandEventArgs e)
    {
        string id = e.CommandArgument.ToString();
        ViewState["FBID"] = id;
        string empName = "";
        foreach (GridViewRow row in grdEmpDetails.Rows)
        {
            HiddenField hdFBID = (HiddenField)row.FindControl("hdFBID");
            if (hdFBID.Value.Trim() == id.Trim())
            {
                HyperLink hypEmpName = (HyperLink)row.FindControl("hypEmpName");
                empName = hypEmpName.Text;
                break;
            }
        }

        
        lblModelPopupHeader.Text = "Reject Rating Remarks : " + empName;
        hdEmpName.Value = "";
        hdEmpName.Value = empName;
        lblValidate.Visible = false;
        txtDesc.Text = "";

        updPnlCustomerDetail.Update();

        this.mdlPopup.Show();

    }

    protected void btnDescSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtDesc.Text))
        {
            lblValidate.Visible = true;
            updPnlCustomerDetail.Update();
            this.mdlPopup.Show();
        }
        else
        {

            if (ViewState["FBID"] != null && !string.IsNullOrEmpty(ViewState["FBID"].ToString()))
            {
                int fbId = int.Parse(ViewState["FBID"].ToString());
                string rejectText = txtDesc.Text;

                bool flagRatingSubmit = false;
                //DBUpdeate
                Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
                flagRatingSubmit = fourCBAL.Reject4CRating(fbId, txtDesc.Text, int.Parse(ViewState["LoginEmpId"].ToString()), hdEmpName.Value, ddlProjectList.SelectedItem.Text, lblDate.Text);

                DateTime dtMonthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));
                BindProjectData(int.Parse(ddlProjectList.SelectedItem.Value), dtMonthYear.Month, dtMonthYear.Year, ViewState["Role"].ToString());
            }
        }
    }

    protected void lnkExportToExcel_Click(object sender, EventArgs e)
    {
        Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
        DataSet dsExpoertToExcelSentForReview = fourCBAL.ExportToExcelSentForReviewRatings(int.Parse(ViewState["LoginEmpId"].ToString()));

        if (dsExpoertToExcelSentForReview != null && dsExpoertToExcelSentForReview.Tables[0].Rows.Count > 0)
        {
            #region "Action"
            lnkExportToExcel.Enabled = true;
            string strDept = string.Empty;
            string strProject = string.Empty;
            string strCType = string.Empty;
            string strColorRating = string.Empty;

            Microsoft.Office.Interop.Excel.ApplicationClass ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            xlWorkBook = ExcelApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet Sheet1 = null;

            DateTime dtDate = DateTime.Now;
            //Sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Sheets[iRow];
            Sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)ExcelApp.Worksheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //Siddharth 11th May 2015 Start
            //Display previous month as SheetName and Title
            Sheet1.Name = dtDate.AddMonths(-1).ToString("MMMM yyyy");
            Sheet1.Cells[1, 1] = "Action Report - " + dtDate.AddMonths(-1).ToString("MMMM yyyy");
            //Siddharth 11th May 2015 End

            Microsoft.Office.Interop.Excel.Range titleRange = ExcelApp.get_Range(Sheet1.Cells[1, "A"], Sheet1.Cells[1, dsExpoertToExcelSentForReview.Tables[0].Columns.Count]);
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

            for (int i = 0; i < dsExpoertToExcelSentForReview.Tables[0].Columns.Count; i++)
            {
                Sheet1.Cells[2, i + 1] = dsExpoertToExcelSentForReview.Tables[0].Columns[i].ColumnName;
                ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[2, i + 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                ((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[2, i + 1]).Font.Bold = true;

            }


            for (int i = 0; i < dsExpoertToExcelSentForReview.Tables[0].Rows.Count; i++)
            {
                for (int j = 0; j < dsExpoertToExcelSentForReview.Tables[0].Columns.Count; j++)
                {
                    //Sheet1.Cells[i + 2, j + 1] = dtActionReport.Rows[i][j].ToString();


                    if (dsExpoertToExcelSentForReview.Tables[0].Columns.ToString() == "Action Created Date" || dsExpoertToExcelSentForReview.Tables[0].Columns.ToString() == "Target Closure Date" || dsExpoertToExcelSentForReview.Tables[0].Columns.ToString() == "Actual Closure Date")
                    {
                        //((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[i + 3, j + 1]).EntireColumn.NumberFormat = "D";
                        //((Microsoft.Office.Interop.Excel.Range)Sheet1.Cells[i + 3, j + 1]).EntireRow.NumberFormat = "D";

                        //Sheet1.Cells[i + 3, j + 1] = string.Format("{D}", dsActionReport.Tables[0].Rows[i][j].ToString());
                        Sheet1.Cells[i + 3, j + 1] = dsExpoertToExcelSentForReview.Tables[0].Rows[i][j].ToString();

                    }
                    else
                    {
                        Sheet1.Cells[i + 3, j + 1] = dsExpoertToExcelSentForReview.Tables[0].Rows[i][j].ToString();
                    }



                }
            }

            Microsoft.Office.Interop.Excel.Range headerRange = Sheet1.get_Range(Sheet1.Cells[2, 0 + 1], Sheet1.Cells[2, dsExpoertToExcelSentForReview.Tables[0].Columns.Count]);
            headerRange.Font.Bold = true;
            headerRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(132, 91, 173));


            Microsoft.Office.Interop.Excel.Range entireDataRange = Sheet1.get_Range(Sheet1.Cells[1, 1], Sheet1.Cells[2 + dsExpoertToExcelSentForReview.Tables[0].Rows.Count, 21]);
            entireDataRange.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Color = System.Drawing.Color.Black.ToArgb();
            entireDataRange.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeLeft].Color = System.Drawing.Color.Black.ToArgb();
            entireDataRange.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].Color = System.Drawing.Color.Black.ToArgb();
            entireDataRange.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Color = System.Drawing.Color.Black.ToArgb();
            entireDataRange.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].Color = System.Drawing.Color.Black.ToArgb();
            entireDataRange.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].Color = System.Drawing.Color.Black.ToArgb();
            entireDataRange.EntireColumn.AutoFit();
            entireDataRange.EntireRow.AutoFit();

            Microsoft.Office.Interop.Excel.Range entireCRange = Sheet1.get_Range(Sheet1.Cells[3, 5], Sheet1.Cells[5 + dsExpoertToExcelSentForReview.Tables[0].Rows.Count, 11]);

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
            Microsoft.Office.Interop.Excel.Range columnWidthRange = Sheet1.get_Range(Sheet1.Cells[2, 12], Sheet1.Cells[dsExpoertToExcelSentForReview.Tables[0].Rows.Count, 13]);
            Microsoft.Office.Interop.Excel.Range columnWidthRangeRemarks = Sheet1.get_Range(Sheet1.Cells[2, 18], Sheet1.Cells[dsExpoertToExcelSentForReview.Tables[0].Rows.Count, 18]);

            columnWidthRange.UseStandardWidth = 50;
            columnWidthRangeRemarks.UseStandardWidth = 50;
            columnWidthRange.ColumnWidth = 50;
            columnWidthRangeRemarks.ColumnWidth = 50;
            columnWidthRange.WrapText = true;
            columnWidthRangeRemarks.WrapText = true;



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
        else
        {
            lnkExportToExcel.Enabled = false;
            Page page = HttpContext.Current.Handler as Page;
            ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('No Data Found. !!!!');", true);
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

