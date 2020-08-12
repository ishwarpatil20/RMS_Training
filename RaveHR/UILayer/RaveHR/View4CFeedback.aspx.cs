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

public partial class FourCModule_View4CFeedback : BaseClass
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

    protected void Page_Load(object sender, EventArgs e)
    {

        HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
        HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();

        AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
        UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
        UserMailId = UserRaveDomainId.Replace("co.in", "com");
       

        if (!IsPostBack)
        {
            lstRights = CheckAccessRights(UserMailId);
            ViewState["lstRights"] = lstRights;
            //DataSet ds = CheckAccessRights(UserMailId);
            //DataTable dtRoles = ds.Tables[0];

            ////ViewState["FourCEmpRole"] = ds;
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

            //ViewState["FourCEmpRole"] = lstRights;

            if (lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.VIEWACCESSRIGHTS.ToString()) || lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.FOURCADMIN.ToString()))
            {
                ViewState["FourCRole"] = MasterEnum.FourCRole.VIEWACCESSRIGHTS.ToString();
            }
            else if (lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.RMS_FUNCTIONALMANAGER.ToString()))
            {
                ViewState["FourCRole"] = MasterEnum.FourCRole.RMS_FUNCTIONALMANAGER.ToString();
            }
            else
            {
                ViewState["FourCRole"] = null;
            }


            //if (lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.VIEWACCESSRIGHTS.ToString()) || lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.FOURCADMIN.ToString()) || lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.RMS_FUNCTIONALMANAGER.ToString()))
            if(ViewState["FourCRole"] != null)
            //if (dtRoles.Select("Role = '"+ CommonConstants.FOURCROLE14 +"'").Any())
            {
                ViewState["ClickCount"] = "1";
                //lblDate.Text = DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Year;
                lblDate.Text = DateTime.Now.AddMonths(-1).ToString("MMMM yyyy");
                imgNext.Enabled = false;


                FillDepartment(ViewState["FourCRole"].ToString());
                //FillProjectNameDropDown(lstRights, ViewState["LoginEmpId"].ToString());
                FillProjectNameDropDown(lstRights, int.Parse(ViewState["LoginEmpId"].ToString()));
                
                FillEmployeeDDL(lstRights);
                
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "You don’t have enough permission to access this system. Please contact to the website administrator.";
                lblMessage.Style["color"] = "red";
                return;
            }
        }
    }

    private List<string> CheckAccessRights(string emailId)
    {
        lstRights = new List<string> { };
        try
        {
            int loginEmpId=0;
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

    protected void imgPrevious_Click(object sender, ImageClickEventArgs e)
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
         //if (ddlDepartment.SelectedIndex > 0 || ddlEmployee.SelectedIndex > 0 || (ddlDepartment.SelectedIndex > 0 && int.Parse(ddlDepartment.SelectedValue) != Convert.ToInt32(MasterEnum.Departments.Projects)) || (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Projects) && ddlProjectList.SelectedIndex > 0))
         if (ddlProjectList.SelectedIndex > 0 || ddlEmployee.SelectedIndex > 0 || (ddlDepartment.SelectedIndex > 0 && int.Parse(ddlDepartment.SelectedValue) != Convert.ToInt32(MasterEnum.Departments.Projects)) || (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Projects) && ddlProjectList.SelectedIndex > 0))
         {
             BindProjectData(int.Parse(ddlDepartment.SelectedItem.Value), int.Parse(ddlProjectList.SelectedItem.Value), monthYear.Month, monthYear.Year);
         }
         else
         {
             grdEmpDetails.Visible = false;
             lblMessage.Visible = false;
         }
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
        if (ddlDepartment.SelectedIndex > 0 || ddlEmployee.SelectedIndex > 0)
        {
            BindProjectData(int.Parse(ddlDepartment.SelectedItem.Value), int.Parse(ddlProjectList.SelectedItem.Value), monthYear.Month, monthYear.Year); 
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
            if (ddlProjectList.SelectedIndex > 0)
            {
                DateTime monthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));
                ddlEmployee.Items.Clear();
                BindProjectData(int.Parse(ddlDepartment.SelectedItem.Value), int.Parse(ddlProjectList.SelectedItem.Value), monthYear.Month, monthYear.Year); 

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
    /// Employee Dropdown
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime monthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));

            //if (ddlEmployee.SelectedIndex > 0)
            //{
                //check if project and department dropdown not selected
                BindProjectData(int.Parse(ddlDepartment.SelectedItem.Value), int.Parse(ddlProjectList.SelectedItem.Value), monthYear.Month, monthYear.Year);
           // }
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

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime monthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));

            if (ddlDepartment.SelectedValue != "SELECT")
            {
                if (ddlDepartment.SelectedIndex > 0 && int.Parse(ddlDepartment.SelectedValue) != Convert.ToInt32(MasterEnum.Departments.Projects))
                {
                    ddlProjectList.SelectedIndex = 0;
                    ddlProjectList.Enabled = false;

                    ddlEmployee.Items.Clear();

                    BindProjectData(int.Parse(ddlDepartment.SelectedItem.Value), int.Parse(ddlProjectList.SelectedItem.Value), monthYear.Month, monthYear.Year); 
                }
                else
                {
                    ddlProjectList.Enabled = true;
                    ddlEmployee.Items.Clear();
                    ddlEmployee.Items.Insert(0, new ListItem(CommonConstants.SELECT, CommonConstants.ZERO.ToString()));
                    ddlProjectList.SelectedValue = "0";

                    if (ddlDepartment.SelectedItem.Text != MasterEnum.Departments.Projects.ToString())
                        ddlProjectList.Enabled = false;

                    //lblManagerName.Text = "";
                    //lblReviewerName.Text = "";

                    grdEmpDetails.Visible = false;
                    lblMessage.Visible = false;

                }    
            }
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


    private void FillDepartment(string role)
    {
        //Declaring COllection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
        DataSet dsDepartment = null;
        Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();

        if (role == MasterEnum.FourCRole.RMS_FUNCTIONALMANAGER.ToString())
        {
            dsDepartment = fourCBAL.GetFunctionalManagerDeptName(int.Parse(ViewState["LoginEmpId"].ToString()));
        }
        else
        {
            ////Declaring Master Class Object
            //Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

            ////Calling Fill dropdown Business layer method to fill 
            ////the dropdown from Master class.
            //raveHRCollection = master.FillDepartmentDropDownBL();

            dsDepartment = fourCBAL.GetDepartmentName("");
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
        }

        //ddlDepartment.Items.Clear();
        //ddlDepartment.DataSource = raveHRCollection;
        //ddlDepartment.DataTextField = Common.CommonConstants.DDL_DataTextField;
        //ddlDepartment.DataValueField = Common.CommonConstants.DDL_DataValueField;
        //ddlDepartment.DataBind();
        //ddlDepartment.Items.Insert(0, new ListItem(CommonConstants.SELECT, CommonConstants.ZERO.ToString()));
        ////remove the Dept Name called RaveDevelopment from Dropdown -Vandna
        //ddlDepartment.Items.Remove(ddlDepartment.Items.FindByValue(CommonConstants.DeptId_RaveDevelopment.ToString()));
    }


    /// <summary>
    /// Fills the Project Name dropdown
    /// </summary>
    private void FillProjectNameDropDown(List<string> lstRights,  int loginEmpId)
    {
        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        // Initialise Business layer object
        Rave.HR.BusinessLayer.MRF.MRFDetail mrfProjectName = new Rave.HR.BusinessLayer.MRF.MRFDetail();
        try
        {
            DataSet dsProject = null;
            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();

            if (lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.FOURCADMIN.ToString()) || lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.VIEWACCESSRIGHTS.ToString()))
            {
                // Call the Business layer method
                //raveHRCollection = mrfProjectName.GetProjectName();
                dsProject = fourCBAL.GetProjectName();

            }
            else
            {
                dsProject = fourCBAL.GetFunctionalManagerProjectName(int.Parse(ViewState["LoginEmpId"].ToString()));
            }
            
            
            if (raveHRCollection != null)
            {
                // Assign the data source to dropdown
                ddlProjectList.Items.Clear();
                ddlProjectList.DataSource = dsProject;

                //ddlProjectList.DataTextField = CommonConstants.DDL_DataTextField;
               // ddlProjectList.DataValueField = CommonConstants.DDL_DataValueField;

                ddlProjectList.DataTextField = dsProject.Tables[0].Columns[1].ToString();
                ddlProjectList.DataValueField = dsProject.Tables[0].Columns[0].ToString();

                // Bind the data to dropdown
                ddlProjectList.DataBind();

                // Default value of dropdown is "Select"
                //ddlProjectList.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
                ddlProjectList.Items.Insert(0, new ListItem(CommonConstants.SELECT, CommonConstants.ZERO.ToString()));
                ddlProjectList.Items.Add(new ListItem(CommonConstants.BENCH_PROJECT, CommonConstants.BENCH_VALUE.ToString()));
                //ddlProjectList.Items.Add(new ListItem(CommonConstants.SELECT, CommonConstants.ZERO.ToString()));

                //DateTime monthYear = DateTime.Parse(string.Concat("01 ", lblDate.Text));

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

                ddlProjectList.Enabled = false;
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
    /// Fill Employee Name 
    /// </summary>
    private void FillEmployeeDDL(List<string> lstRights)
    {
        DataSet ds = new DataSet();
        Rave.HR.BusinessLayer.Employee.Employee addEmployeeBAL = new Rave.HR.BusinessLayer.Employee.Employee();
        Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();

        if (lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.FOURCADMIN.ToString()) || lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.VIEWACCESSRIGHTS.ToString()))
        //if (ViewState["FourCRole"].ToString() != MasterEnum.FourCRole.RMS_FUNCTIONALMANAGER.ToString())
        {
            ds = addEmployeeBAL.GetActiveEmployeeList();
        }
        else
        {
            ds = fourCBAL.GetFunctionalManagerEmployeeName(int.Parse(ViewState["LoginEmpId"].ToString()));
        }

        ddlEmployee.Items.Clear();

        ddlEmployee.DataSource = ds.Tables[0];
        ddlEmployee.DataTextField = ds.Tables[0].Columns[1].ToString();
        ddlEmployee.DataValueField = ds.Tables[0].Columns[0].ToString();
        ddlEmployee.DataBind();

        ddlEmployee.Items.Insert(0, new ListItem(CommonConstants.SELECT, CommonConstants.ZERO.ToString()));
    }


    private void BindProjectData(int deptId, int projectId, int month, int year)
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
        //    dsEmpDeatils = fourCBAL.Get4CEmployeeDeatils(projectId, month, year, fourCRole, empId);
        //}
        
        dsEmpDeatils = fourCBAL.Get4CViewFeedbackDeatils(deptId, projectId, month, year, empId, int.Parse(ViewState["LoginEmpId"].ToString()), ViewState["FourCRole"].ToString());
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
                divGridViewEmpDetails.Style.Add("height", "280px");
            }
        }


        if (empId == 0)
        {
            ddlEmployee.ClearSelection();
            ddlEmployee.Items.Clear();
            //ddlEmployee.DataSource = dsEmpDeatils.Tables[0];

            //ddlEmployee.DataTextField = dsEmpDeatils.Tables[0].Columns[0].ToString();
            //ddlEmployee.DataValueField = dsEmpDeatils.Tables[0].Columns[2].ToString();
            //ddlEmployee.DataBind();
            int funmanagerLoginId = 0;

            if (ViewState["FourCRole"].ToString().Trim() == MasterEnum.FourCRole.RMS_FUNCTIONALMANAGER.ToString())
            {
                funmanagerLoginId = int.Parse(ViewState["LoginEmpId"].ToString());
            }

            DataSet dsEmp = fourCBAL.Get4CViewEmployeeFromRMS(deptId, projectId, month, year, funmanagerLoginId);
            ddlEmployee.DataSource = dsEmp;
            ddlEmployee.DataTextField = dsEmp.Tables[0].Columns[1].ToString();
            ddlEmployee.DataValueField = dsEmp.Tables[0].Columns[0].ToString();
            ddlEmployee.DataBind();

            ddlEmployee.Items.Insert(0, new ListItem(CommonConstants.SELECT, CommonConstants.ZERO.ToString()));
        }
    }

    protected void grdEmpDetails_DataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
            e.Row.Cells[1].Attributes["onmouseout"] = "this.style.textDecoration='none';";

            if (ViewState["AllowDirectSubmit"] == null)
                ViewState["AllowDirectSubmit"] = "";

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


            //e.Row.Cells[1].Attributes["onclick"] = "javascript:Open4CDetailPopUp('" + DataBinder.Eval(e.Row.DataItem, "EmpId") +
            //                                                        "','" + DataBinder.Eval(e.Row.DataItem, "DepartmentId") +
            //                                                        "','" + DataBinder.Eval(e.Row.DataItem, "ProjectId") +
            //                                                        "','" + monthYear.Month +
            //                                                        "','" + monthYear.Year +
            //                                                        "','" + DataBinder.Eval(e.Row.DataItem, "FBID") +
            //                                                        "','" + 1 + //("Mode", rbAVPView.SelectedIndex.ToString())

            //    //"','" + ViewState["FourCRole"].ToString() +

            //                                                        "','" + UserMailId +

            //                                                        "','" + DataBinder.Eval(e.Row.DataItem, "EmployeeName") + "')";

           
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

            HyperLink lbEmpName = (HyperLink)e.Row.FindControl("lnkName");

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
            //Ishwar 12012015 Start
            lbEmpName.Attributes["onclick"] = "javascript:Open4CActionPopUp('" + createURLParameter + "')";
            //Ishwar 12012015 End

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

    protected void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            ddlDepartment.SelectedIndex = 0;
            ddlProjectList.SelectedIndex = 0;
            
            ddlProjectList.Enabled = false;
            grdEmpDetails.Visible = false;

            FillEmployeeDDL((List<string>)ViewState["lstRights"]);

            ViewState["ClickCount"] = "1";
            lblDate.Text = DateTime.Now.AddMonths(-1).ToString("MMMM yyyy");
            imgNext.Enabled = false;
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnReset_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

}
