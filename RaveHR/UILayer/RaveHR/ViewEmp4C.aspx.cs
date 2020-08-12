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

public partial class ViewEmp4C : BaseClass
{
    const string CLASS_NAME = "FourCModule_View4CFeedback.aspx.cs";
    const string SEND_FOR_REVIEW = "SENDFORREVIEW";
    const string REVIEWED = "REVIEWED";

    int fromCurrentDate;
    string strEmpCode;
    string UserRaveDomainId;
    string UserMailId;
    List<string> lstRights;
    DataSet dsEmpDeatils;
    string NotApplicableStatus = "N/A";
    int dept = 1;
    int projectId = 0;
    public ASP._4clogin_aspx prev;

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
        
        //temp access
        string txtUser = null;
        AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
        if (ViewState["UserMailId"] == null)
        {
            if (PreviousPage != null)
            {
                prev = (ASP._4clogin_aspx)PreviousPage;
                txtUser = prev.UserName;

                if (txtUser != null && !string.IsNullOrEmpty(txtUser))
                {
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
                    //for all employees
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
        if (ViewState["dept"] != null)
        {
            dept = Convert.ToInt32( ViewState["dept"]);
        }
        if (!IsPostBack)
        {
            lstRights = CheckAccessRights(UserMailId);

            if (lstRights.Exists(o => o.ToString() == MasterEnum.FourCRole.ViewMy4C.ToString()))
            {
                ViewState["ClickCount"] = "1";
                //lblDate.Text = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Year;
                lblDate.Text = DateTime.Now.AddMonths(-1).ToString("MMMM yyyy");
                imgNext.Enabled = false;
                DateTime monthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));
                //Venkatesh : 4C_Support 26-Mar-2014 : Start 
                //Desc : Dept Support
                 DataSet dsDepartment = null; 
                Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
                try
                {
                    dsDepartment = fourCBAL.GetDepartmentName(UserMailId, "", "");
                    if (dsDepartment != null)
                    {
                        if (dsDepartment.Tables[1] != null)
                        {
                            if (dsDepartment.Tables[1].Rows.Count > 0)
                            {
                                dept =Convert.ToInt32(dsDepartment.Tables[1].Rows[0]["DepartmentID"].ToString());
                                ViewState["dept"] = dept;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetEmployeeDepartment", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
                    LogErrorMessage(objEx);
                } 

                //Venkatesh : 4C_Support 26-Mar-2014 : End
                BindProjectData(dept, projectId, monthYear.Month, monthYear.Year);    
            }
            else
            {
                Response.Redirect("Home.aspx");
            }
        }

    }

    protected void imgPrevious_Click(object sender, ImageClickEventArgs e)
    {
        //fromCurrentDate = fromCurrentDate + 1;
        if (lblDate.Text == DateTime.Now.AddMonths(-12).ToString("MMMM yyyy"))
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

        BindProjectData(dept, projectId, monthYear.Month, monthYear.Year);
      
    }

    protected void imgNext_Click(object sender, ImageClickEventArgs e)
    {
        if (lblDate.Text == DateTime.Now.AddMonths(-2).ToString("MMMM yyyy"))
        {
            imgNext.Enabled = false;
        }
        imgPrevious.Enabled = true;

        ViewState["ClickCount"] = Convert.ToInt32(ViewState["ClickCount"]) - 1;
        fromCurrentDate = Convert.ToInt32(ViewState["ClickCount"]);
        lblDate.Text = DateTime.Now.AddMonths(-fromCurrentDate).ToString("MMMM yyyy");

        DateTime monthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));
        //monthYear = monthYear.AddMonths(-1);
        //if (ddlDepartment.SelectedIndex > 0 || ddlEmployee.SelectedIndex > 0)
       // {
        BindProjectData(dept, projectId, monthYear.Month, monthYear.Year);
       // }

    }

    private List<string> CheckAccessRights(string emailId)
    {
        lstRights = new List<string> { };
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
    private void BindProjectData(int deptId, int projectId, int month, int year)
    {
        int empId = int.Parse(ViewState["LoginEmpId"].ToString());
        
        grdEmpDetails.Visible = true;
        dsEmpDeatils = new DataSet();
        Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();

        
        dsEmpDeatils = fourCBAL.Get4CViewFeedbackDeatils(deptId, projectId, month, year, empId, int.Parse(ViewState["LoginEmpId"].ToString()), "");
        grdEmpDetails.DataSource = dsEmpDeatils.Tables[0];
        grdEmpDetails.DataBind();


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
                divGridViewEmpDetails.Style.Add("height", "450px");
            }
        }
    }

    protected void grdEmpDetails_DataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[1].Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
            //e.Row.Cells[1].Attributes["onmouseout"] = "this.style.textDecoration='none';";

           // e.Row.Cells[1].Attributes.Add("style", "text-decoration:underline");
            //e.Row.Cells[1].Attributes["onmouseout"] = "this.style.textDecoration='none';";

            if (ViewState["AllowDirectSubmit"] == null)
                ViewState["AllowDirectSubmit"] = "";

            string strAllowedToEdit = "";

            DateTime monthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));
            //e.Row.Cells[1].Attributes["onclick"] =
            //e.Row.Attributes["onclick"] =    "javascript:Open4CDetailPopUp('" + DataBinder.Eval(e.Row.DataItem, "EmpId") +
            //                                                        "','" + DataBinder.Eval(e.Row.DataItem, "DepartmentId") +
            //                                                        "','" + DataBinder.Eval(e.Row.DataItem, "ProjectId") +
            //                                                        "','" + monthYear.Month +
            //                                                        "','" + monthYear.Year +
            //                                                        "','" + DataBinder.Eval(e.Row.DataItem, "FBID") +
            //                                                        "','" + ViewState["FourCRole"].ToString() +
            //                                                        "','" + UserMailId +
            //                                                         "','" + "" +
            //                                                         "','" + "" +
            //                                                        "','" + DataBinder.Eval(e.Row.DataItem, "EmployeeName") + "')";
            
            //Ishwar Patil 28012015 Start
            HyperLink hypName = (HyperLink)e.Row.FindControl("lnkName");
            //Ishwar Patil 28012015 End
            string createURLParameter = URLHelper.SecureParameters("EmpId", DataBinder.Eval(e.Row.DataItem, "EmpId").ToString()) + "&"
                                          + URLHelper.SecureParameters("departmentId", DataBinder.Eval(e.Row.DataItem, "DepartmentId").ToString()) + "&"
                                          + URLHelper.SecureParameters("projectId", DataBinder.Eval(e.Row.DataItem, "ProjectId").ToString()) + "&"
                                          + URLHelper.SecureParameters("month", monthYear.Month.ToString()) + "&"
                                          + URLHelper.SecureParameters("year", monthYear.Year.ToString()) + "&"
                                          + URLHelper.SecureParameters("FBID", DataBinder.Eval(e.Row.DataItem, "FBID").ToString()) + "&"
                                          + URLHelper.SecureParameters("loginEmailId", UserMailId) + "&"
                                          + URLHelper.SecureParameters("Mode", "1") + "&"
                                          + URLHelper.SecureParameters("EmployeeName", DataBinder.Eval(e.Row.DataItem, "EmployeeName").ToString()) + "&"
                                          + URLHelper.CreateSignature(DataBinder.Eval(e.Row.DataItem, "EmpId").ToString(),
                                                                   DataBinder.Eval(e.Row.DataItem, "DepartmentId").ToString(),
                                                                   DataBinder.Eval(e.Row.DataItem, "ProjectId").ToString(),
                                                                   monthYear.Month.ToString(),
                                                                   monthYear.Year.ToString(),
                                                                   DataBinder.Eval(e.Row.DataItem, "FBID").ToString(),
                                                                   UserMailId,
                                                                   "1",
                                                                   DataBinder.Eval(e.Row.DataItem, "EmployeeName").ToString()); //strAllowedToEdit;


            //Ishwar Patil 28012015 Start
            //e.Row.Cells[1].Attributes["onclick"] = "javascript:Open4CActionPopUp('" + createURLParameter + "')";
            hypName.Attributes["onclick"] = "javascript:Open4CActionPopUp('" + createURLParameter + "')";
            //Ishwar Patil 28012015 End

            if (!string.IsNullOrEmpty(dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Competency"].ToString()))
            {

                string strCompetencyColor = dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Competency"].ToString() != "Amber" ? dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Competency"].ToString() : "#FFCC00";
                if (strCompetencyColor.Trim() != NotApplicableStatus)
                {
                    e.Row.Cells[10].Attributes.Add("style", "background-color: " + strCompetencyColor);
                }

            }
            if (!string.IsNullOrEmpty(dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Communication"].ToString()))
            {
                string strCommunicationColor = dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Communication"].ToString() != "Amber" ? dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Communication"].ToString() : "#FFCC00";
                if (strCommunicationColor.Trim() != NotApplicableStatus)
                {
                    e.Row.Cells[11].Attributes.Add("style", "background-color: " + strCommunicationColor);
                }

            }

            if (!string.IsNullOrEmpty(dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Commitment"].ToString()))
            {
                string strCommitmentColor = dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Commitment"].ToString() != "Amber" ? dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Commitment"].ToString() : "#FFCC00";
                if (strCommitmentColor.Trim() != NotApplicableStatus)
                {
                    e.Row.Cells[12].Attributes.Add("style", "background-color: " + strCommitmentColor);
                }

            }
            if (!string.IsNullOrEmpty(dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Collaboration"].ToString()))
            {
                string strCollaboration = dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Collaboration"].ToString() != "Amber" ? dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Collaboration"].ToString() : "#FFCC00";
                if (strCollaboration.Trim() != NotApplicableStatus)
                {
                    e.Row.Cells[13].Attributes.Add("style", "background-color: " + strCollaboration);
                }

            }

            //if (!string.IsNullOrEmpty(dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Competency"].ToString()))
            //{
            //    string strCompetencyColor = dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Competency"].ToString() != "Amber" ? dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Competency"].ToString() : "#FFCC00";
            //    e.Row.Cells[10].Attributes.Add("style", "background-color: " + strCompetencyColor);
            //}
            //if (!string.IsNullOrEmpty(dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Communication"].ToString()))
            //{
            //    string strCommunicationColor = dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Communication"].ToString() != "Amber" ? dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Communication"].ToString() : "#FFCC00";
            //    e.Row.Cells[11].Attributes.Add("style", "background-color: " + strCommunicationColor);
            //}

            //if (!string.IsNullOrEmpty(dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Commitment"].ToString()))
            //{
            //    string strCommitmentColor = dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Commitment"].ToString() != "Amber" ? dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Commitment"].ToString() : "#FFCC00";
            //    e.Row.Cells[12].Attributes.Add("style", "background-color: " + strCommitmentColor);
            //}
            //if (!string.IsNullOrEmpty(dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Collaboration"].ToString()))
            //{
            //    string strCommitmentColor = dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Collaboration"].ToString() != "Amber" ? dsEmpDeatils.Tables[0].Rows[e.Row.RowIndex]["Collaboration"].ToString() : "#FFCC00";
            //    e.Row.Cells[13].Attributes.Add("style", "background-color: " + strCommitmentColor);
            //}
        }

        int[] deptId = { 1, 7, 16, 17, 18, 24, 26 };

        if (DataBinder.Eval(e.Row.DataItem, "DepartmentId") != null && !string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "DepartmentId").ToString()))
        {
            //if(int.Parse(DataBinder.Eval(e.Row.DataItem, "DepartmentId").ToString().Contains('1', '7'))
            if (deptId.Any(o => int.Parse(o.ToString()) == int.Parse(DataBinder.Eval(e.Row.DataItem, "DepartmentId").ToString())))
            {
                e.Row.Cells[4].Visible = true;
                e.Row.Cells[5].Visible = true;
                grdEmpDetails.HeaderRow.Cells[4].Visible = true;
                grdEmpDetails.HeaderRow.Cells[5].Visible = true;
            }
            else
            {
                e.Row.Cells[4].Visible = false;
                e.Row.Cells[5].Visible = false;
                grdEmpDetails.HeaderRow.Cells[4].Visible = false;
                grdEmpDetails.HeaderRow.Cells[5].Visible = false;
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/Home.aspx", false);
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
}
