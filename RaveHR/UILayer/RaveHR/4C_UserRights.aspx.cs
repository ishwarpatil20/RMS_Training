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


public partial class FourCModule_4C_UserRights : BaseClass
{

    const string CLASS_NAME = "FourCModule_4C_UserRights.aspx.cs";
    BusinessEntities.Employee employee = new BusinessEntities.Employee();
    string UserRaveDomainId;
    string UserMailId;
    ArrayList arrRolesForUser = new ArrayList();
    List<string> lstRights;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!ValidateURL())
        {
            Response.Redirect(CommonConstants.INVALIDURL, false);
        }
        else
        {

            AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
            UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
            UserMailId = UserRaveDomainId.Replace("co.in", "com");

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
                    //bool flagAceess = false;
                    lstRights = CheckAccessRights(UserMailId);

                    //if (!flagAceess)
                    //Google
                    //if (!lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.FOURCADMIN.ToString()) && UserMailId != "sawita.kamat@rave-tech.com")
                    if (!lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.FOURCADMIN.ToString()) && (UserMailId != "sawita.kamat@northgateps.com"))
                    {
                        lblMessage.Visible = true;
                        divDetail.Visible = false;
                        tdFilter.Visible = false;
                        divHeaderLabel.Visible = false;

                        lblMessage.Text = "You don’t have enough permission to access this system. Please contact to the website administrator.";
                        lblMessage.Style["color"] = "red";
                        return;
                    }
                    else
                    {

                        GetEmployeeDepartment();
                        FillProjectNameDropDown();
                        FillCreatorApproverData();
                        CheckCreatorReviewerSetForAll();
                        imgCreator.Attributes["onclick"] = "popUpEmployeeSearch('Creator');";
                        imgReviewer.Attributes["onclick"] = "popUpEmployeeSearch('Reviewer');";
                        imgSearchEmployee.Attributes["onclick"] = "popUpEmployeeSearch('SearchEmployee');";

                    }
                }
            }
        }
    }

    //private bool CheckAccessRights(string emailId)
    //{
    //    bool flag = false;
    //    try
    //    {
            
    //        Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
    //        flag = fourCBAL.Check4CAccessRights(emailId);
    //        return flag;
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
    //    return flag;
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

    private void FillCreatorApproverData()
    {
        try
        {
            DataSet dsCreatorReviewer = new DataSet();
            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
            dsCreatorReviewer = fourCBAL.GetCreatorApproverDetails();

            grdvCreatorApprover.DataSource = dsCreatorReviewer;
            grdvCreatorApprover.DataBind();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "FillCreatorApproverData", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    private void CheckCreatorReviewerSetForAll()
    {
        try
        {
            bool flag = false;
            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
            flag = fourCBAL.CheckCreatorReviewerSetForAll();

            if (flag)
            {
                ViewState["btnAddEnable"] = false;
                btnAdd.Enabled = false;
            }
            else
            {
                btnAdd.Enabled = true;
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "CheckCreatorReviewerSetForAll", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    protected void grdvCreatorApprover_PageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdvCreatorApprover.PageIndex = e.NewPageIndex;
            if (rblAdminSelectionOption.SelectedIndex == 0)
            {
                FillCreatorApproverData();
            }
            else
            {
                getEmployeeData();
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvCreatorApprover_PageIndexChanging", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdvCreatorApprover_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgDeleteRPDuration = (ImageButton)e.Row.FindControl("imgDelete");
                imgDeleteRPDuration.Attributes.Add("onClick", "return func_AskUser()");

                if (e.Row.Cells[0].Text == "Projects" && string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "ProjectId").ToString()))
                {
                    e.Row.Cells[1].Text = "Bench";
                }
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvCreatorApprover_RowDataBound", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    protected void grdvCreatorApprover_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblMessage.Visible = false;

        

        if (e.CommandName == "Edt")
        {
            GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
            HiddenField HfProjectId = (HiddenField)row.FindControl("hfProjectId");
            HiddenField HfDepartmentId = (HiddenField)row.FindControl("HfDepartmentId");
            HiddenField HfCreatorGrd = (HiddenField)row.FindControl("HfCreatorGrd");
            HiddenField HfReviewerGrd = (HiddenField)row.FindControl("HfReviewerGrd");
           
            btnAdd.Enabled = false;
            btnUpdate.Enabled = true;

            HfCreator.Value = HfCreatorGrd.Value;
            HfReviewer.Value = HfReviewerGrd.Value;

            txtCreator.Text = row.Cells[2].Text;
            txtReviewer.Text = row.Cells[3].Text;

            //Declaring COllection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
            Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
            raveHRCollection = master.FillDepartmentDropDownBL();

            ddlDepartment.Items.Clear();
            ddlDepartment.DataSource = raveHRCollection;
            ddlDepartment.DataTextField = Common.CommonConstants.DDL_DataTextField;
            ddlDepartment.DataValueField = Common.CommonConstants.DDL_DataValueField;
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);


            ddlDepartment.SelectedValue = HfDepartmentId.Value;
            
            if (HfDepartmentId.Value == "1")
            {
                ddlProject.Enabled = true;

                Rave.HR.BusinessLayer.MRF.MRFDetail mrfProjectName = new Rave.HR.BusinessLayer.MRF.MRFDetail();
                raveHRCollection = mrfProjectName.GetProjectName();

                ddlProject.Items.Clear();
                ddlProject.DataSource = raveHRCollection;
                ddlProject.DataTextField = CommonConstants.DDL_DataTextField;
                ddlProject.DataValueField = CommonConstants.DDL_DataValueField;

                // Bind the data to dropdown
                ddlProject.DataBind();
                // Default value of dropdown is "Select"
                ddlProject.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);


                if(!string.IsNullOrEmpty(HfProjectId.Value))
                    ddlProject.SelectedValue = HfProjectId.Value;
            }
            else
            {
                ddlProject.SelectedItem.Text = "SELECT";
            }

            ddlDepartment.Enabled = false;
            ddlProject.Enabled = false;
        }
        else if (e.CommandName == "Del")
            {
                try
                {
                    if (rblAdminSelectionOption.SelectedIndex == 0)
                    {

                        GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                        HiddenField HfProjectId = (HiddenField)row.FindControl("hfProjectId");
                        HiddenField HfDepartmentId = (HiddenField)row.FindControl("HfDepartmentId");
                        HiddenField HfCreatorGrd = (HiddenField)row.FindControl("HfCreatorGrd");
                        HiddenField HfReviewerGrd = (HiddenField)row.FindControl("HfReviewerGrd");


                        Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
                        int depId = int.Parse(HfDepartmentId.Value);
                        int projId = 0;

                        if (depId == 1)
                            projId = int.Parse(HfProjectId.Value);


                        fourCBAL.AddUpdateDeleteCreatorReviewer(depId, projId, HfCreator.Value, HfReviewer.Value, UserMailId, "Delete");
                        FillCreatorApproverData();

                        ClearControl();

                        lblMessage.Visible = true;
                        lblMessage.Text = "Data Deleted Successfully!!!.";
                        lblMessage.Style["color"] = "blue";
                    }
                    else
                    {
                        GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                        HiddenField HfCreatorGrd = (HiddenField)row.FindControl("HfCreatorGrd");
                        List<string> ls = new List<string> { };
                        ls.Add(HfCreatorGrd.Value);

                        if (ls.Count > 0 && !string.IsNullOrEmpty(HfCreatorGrd.Value))
                        {
                            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
                            fourCBAL.AddDeleteViewAccessRights(ls, UserMailId, "Delete");

                            getEmployeeData();

                            lblMessage.Visible = true;
                            lblMessage.Text = "Data Added Successfully!!!.";
                            lblMessage.Style["color"] = "blue";
                        }

                    }

                }
                catch (RaveHRException ex)
                {
                    LogErrorMessage(ex);
                }
                catch (Exception ex)
                {
                    RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvCreatorApprover_RowCommand", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
                    LogErrorMessage(objEx);
                }
            }
    }


    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            int DeptId = Convert.ToInt32(ddlDepartmentFilter.SelectedItem.Value);
            int ProjectId = 0;
            if (ddlDepartmentFilter.SelectedItem.Text == MasterEnum.Departments.Projects.ToString())
            {
                ProjectId = Convert.ToInt32(ddlProjectNameFilter.SelectedItem.Value);
            }

            DataSet dsCreatorReviewer = new DataSet();
            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
            dsCreatorReviewer = fourCBAL.GetFilteredCreatorApproverDetails(DeptId, ProjectId);

            grdvCreatorApprover.DataSource = dsCreatorReviewer;
            grdvCreatorApprover.DataBind();

          
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnFilter_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearControl();
        ddlProject.Enabled = false;
        ddlDepartment.Enabled = true;
    }


    private void ClearControl()
    {
        try
        {
            rblAdminSelectionOption.SelectedIndex = 0;
            //ddlDepartment.SelectedIndex = 0;
            //ddlProject.SelectedIndex = 0;
            txtCreator.Text = "";
            txtReviewer.Text = "";
            grdvCreatorApprover.PageIndex = 0;
            FillCreatorApproverData();
            //ddlProject.Enabled = false;
            //ddlDepartment.Enabled = true;
            if (ViewState["btnAddEnable"] != null && !Convert.ToBoolean(ViewState["btnAddEnable"].ToString()))
                btnAdd.Enabled = false;
            else
                btnAdd.Enabled = true;

            btnUpdate.Enabled = false;
            //ddlProjectNameFilter.SelectedIndex = 0;
            //ddlDepartmentFilter.SelectedIndex = 0;
            //ddlProjectNameFilter.Enabled = false;
            lblMessage.Visible = false;

            GetEmployeeDepartment();
            FillProjectNameDropDown();

            upFilter.Update();

        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnReset_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    { 
        try
        {
            int DeptId = Convert.ToInt32(ddlDepartment.SelectedItem.Value);
            int ProjectId = 0;
            if (ddlDepartment.SelectedItem.Text == MasterEnum.Departments.Projects.ToString())
            {
                if (ddlProject.SelectedIndex > 0)
                {
                    ProjectId = Convert.ToInt32(ddlProject.SelectedItem.Value);
                }
            }

            bool flag = false;
            bool isAllowCreator = false;
            bool isAllowReviewer = false;

            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();

            fourCBAL.CheckReviewerIsAllowForDepartment(DeptId,ProjectId, HfCreator.Value, HfReviewer.Value, ref isAllowCreator, ref isAllowReviewer);

            if (!isAllowCreator)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Creator is not part of project or department. creator is present in Functional Manager or Line Manager of Employee.!!!.";
                return;
            }
            if (!isAllowReviewer)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Reviewer is not part of project or department. Reviewer is present in Functional Manager or Line Manager of Employee.!!!.";
                return;
            }

            for (int iRow = 0; iRow <= grdvCreatorApprover.Rows.Count - 1; iRow++)
            {
                HiddenField HfProjectId = (HiddenField)grdvCreatorApprover.Rows[iRow].FindControl("hfProjectId");
                HiddenField HfDepartmentId = (HiddenField)grdvCreatorApprover.Rows[iRow].FindControl("HfDepartmentId");

                if (ddlDepartment.SelectedItem.Text == MasterEnum.Departments.Projects.ToString())
                {
                    if (HfDepartmentId.Value == "1" && HfProjectId.Value == ddlProject.SelectedItem.Value)
                    {
                        flag = true;
                        break;
                    }
                }
                else
                {
                    if (HfDepartmentId.Value == ddlDepartment.SelectedItem.Value)
                    {
                        flag = true;
                        break;
                    }
                }   
            }

            if (flag)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Creator and Reviewer already set for selected record. please select different record.!!!.";
                lblMessage.Style["color"] = "red";
                ddlDepartment.SelectedIndex = 0;
                ddlProject.SelectedIndex = 0;
                ddlProject.Enabled = false;
            }
            else
            {
                if (!string.IsNullOrEmpty(HfCreator.Value) && !string.IsNullOrEmpty(HfReviewer.Value))
                {
                    
                    fourCBAL.AddUpdateDeleteCreatorReviewer(DeptId, ProjectId, HfCreator.Value, HfReviewer.Value, UserMailId, "Add");

                    FillCreatorApproverData();

                    ClearControl();

                    lblMessage.Visible = true;
                    lblMessage.Text = "Data Added Successfully!!!.";
                    lblMessage.Style["color"] = "blue";
                }
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnAdd_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    protected void btnAddEmployee_Click(object sender, EventArgs e)
    {
        try
        {
            string newEmpId = HfViewRights.Value;

            List<string> lstDuplicate = new List<string> {};
            List<string> lstNewEmpId = new List<string> { };
            string strDuplicateName = "";

            if (!string.IsNullOrEmpty(newEmpId))
            {
                string[] empidarray = newEmpId.Split(',');

                for (int i = 0; i < empidarray.Length; i++)
                {
                    string tempEmpId = empidarray[i];
                    bool flag = true;
                    for (int iRow = 0; iRow <= grdvCreatorApprover.Rows.Count - 1; iRow++)
                    {
                        HiddenField HfEmpIdInGrd = (HiddenField)grdvCreatorApprover.Rows[iRow].FindControl("HfCreatorGrd");
                        if (HfEmpIdInGrd.Value == tempEmpId)
                        {
                            lstDuplicate.Add(tempEmpId);
                            if (strDuplicateName == "")
                            {
                                strDuplicateName = strDuplicateName + grdvCreatorApprover.Rows[iRow].Cells[2].Text.ToString();
                            }
                            else
                            {
                                strDuplicateName = " " + strDuplicateName + " , " + grdvCreatorApprover.Rows[iRow].Cells[2].Text.ToString();
                            }
                            flag = false;
                            break;
                        }
                    }

                    if(flag)
                        lstNewEmpId.Add(tempEmpId);
                }

                if (!string.IsNullOrEmpty(strDuplicateName))
                {
                    Page page = HttpContext.Current.Handler as Page;
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Employee " + strDuplicateName + " has already assigned view access rights.');", true);
                }

                if (lstNewEmpId.Count > 0)
                {
                    Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
                    fourCBAL.AddDeleteViewAccessRights(lstNewEmpId, UserMailId, "Add");
                    
                    getEmployeeData();

                    lblMessage.Visible = true;
                    lblMessage.Text = "Data Added Successfully!!!.";
                    lblMessage.Style["color"] = "blue";
                }


            }

        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnAddEmployee_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }



    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartment.SelectedIndex == 0)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Please select department!!!.";
                lblMessage.Style["color"] = "red";
                return;
            }
            //if (ddlDepartment.SelectedItem.Text == MasterEnum.Departments.Projects.ToString() && ddlProject.SelectedIndex == 0)
            //{
            //    lblMessage.Visible = true;
            //    lblMessage.Text = "Please select Project!!!.";
            //    lblMessage.Style["color"] = "red";
            //    return;
            //}
            if (txtCreator.Text.Trim() == "")
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Please select Creator!!!.";
                lblMessage.Style["color"] = "red";
                return;
            }
            if (txtReviewer.Text.Trim() == "")
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Please select Reviewer!!!.";
                lblMessage.Style["color"] = "red";
                return;
            }


                int DeptId = Convert.ToInt32(ddlDepartment.SelectedItem.Value);
                int ProjectId = 0;
                if (ddlDepartment.SelectedItem.Text == MasterEnum.Departments.Projects.ToString())
                {
                    if(ddlProject.SelectedIndex > 0)
                        ProjectId = Convert.ToInt32(ddlProject.SelectedItem.Value);
                }

                bool isAllowCreator = false;
                bool isAllowReviewer = false;

                Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();

                fourCBAL.CheckReviewerIsAllowForDepartment(DeptId, ProjectId, HfCreator.Value, HfReviewer.Value, ref isAllowCreator, ref isAllowReviewer);

                if (!isAllowCreator)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Creator is not part of project or department. creator is present in Functional Manager or Line Manager of Employee.!!!.";
                    return;
                }
                if (!isAllowReviewer)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Reviewer is not part of project or department. Reviewer is present in Functional Manager or Line Manager of Employee.!!!.";
                    return;
                }

                DataSet dsCreatorReviewer = new DataSet();
                
                //fourCBAL.UpdateCreatorReviewer(DeptId, ProjectId, HfCreator.Value, HfReviewer.Value, UserMailId);
                fourCBAL.AddUpdateDeleteCreatorReviewer(DeptId, ProjectId, HfCreator.Value, HfReviewer.Value, UserMailId, "Update");

                FillCreatorApproverData();

                ClearControl();

                lblMessage.Visible = true;
                lblMessage.Text = "Data Updated Successfully!!!.";
                lblMessage.Style["color"] = "blue";

        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnUpdate_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }



    protected void rblAdminSelectionOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblAdminSelectionOption.SelectedIndex == 0)
        {
            divCreatorReviewer.Visible = true;
            grdvCreatorApprover.Visible = true;
            btnUpdate.Visible = true;
            btnReset.Visible = true;
            btnAdd.Visible = true;
            btnAddEmployee.Visible = false;
            divViewRights.Visible = false;

            tdFilter.Visible = true;
            feildLabel.Text = @"Creator / Reviewer";
            
            grdvCreatorApprover.Columns[2].HeaderText = "Creator";
            grdvCreatorApprover.Columns[0].Visible = true;
            grdvCreatorApprover.Columns[1].Visible = true;
            grdvCreatorApprover.Columns[3].Visible = true;
            grdvCreatorApprover.Columns[4].Visible = true;
            grdvCreatorApprover.Width = Unit.Percentage(100);
            grdvCreatorApprover.PageIndex = 0;

            FillCreatorApproverData();
            lblSummary.Text = @"Creator / Reviewer Summary";
            lblMessage.Visible = false;

        }
        else
        {
            tdFilter.Visible = false;
            //tblFilter.Visible = false;
            //grdvCreatorApprover.Visible = false;
            divCreatorReviewer.Visible = false;
            btnUpdate.Visible = false;
            btnReset.Visible = false;
            btnAdd.Visible = false;
            btnAddEmployee.Visible = true;
            divViewRights.Visible = true;
            feildLabel.Text = "View Rights";
            grdvCreatorApprover.PageIndex = 0;
            getEmployeeData();
            lblSummary.Text = "View Rights Summary";
            lblMessage.Visible = false;
           
        }
    }


    private void getEmployeeData()
    {
        try
        {
            DataSet dsCreatorReviewer = new DataSet();
            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
            dsCreatorReviewer = fourCBAL.GetViewAccessRightsData();

            grdvCreatorApprover.DataSource = dsCreatorReviewer;

            grdvCreatorApprover.Columns[2].HeaderText = "Employee Name";
            grdvCreatorApprover.Columns[0].Visible = false;
            grdvCreatorApprover.Columns[1].Visible = false;
            grdvCreatorApprover.Columns[3].Visible = false;
            grdvCreatorApprover.Columns[4].Visible = false;
            
            grdvCreatorApprover.DataBind();

            

            grdvCreatorApprover.Width=Unit.Percentage(50); 
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "FillCreatorApproverData", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }



    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedItem.Value == "1")
        {
            ddlProject.Enabled = true;
        }
        else
        {
            ddlProject.Enabled = false;
        }
    }


    private void GetEmployeeDepartment()
    {
        //Declaring COllection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        try
        {
            //Calling Fill dropdown Business layer method to fill 
            //the dropdown from Master class.
            raveHRCollection = master.FillDepartmentDropDownBL();

            DataSet dsDepartment = null;
            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
            dsDepartment = fourCBAL.GetDepartmentForCreatorApprover();

            ddlDepartment.Items.Clear();
            //ddlDepartment.DataSource = raveHRCollection;
            //ddlDepartment.DataTextField = Common.CommonConstants.DDL_DataTextField;
            //ddlDepartment.DataValueField = Common.CommonConstants.DDL_DataValueField;
            ddlDepartment.DataSource = dsDepartment;
            ddlDepartment.DataTextField = dsDepartment.Tables[0].Columns[1].ToString();
            ddlDepartment.DataValueField = dsDepartment.Tables[0].Columns[0].ToString();
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

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

            ddlDepartmentFilter.DataSource = raveHRCollection;
            ddlDepartmentFilter.DataTextField = Common.CommonConstants.DDL_DataTextField;
            ddlDepartmentFilter.DataValueField = Common.CommonConstants.DDL_DataValueField;
            ddlDepartmentFilter.DataBind();
            ddlDepartmentFilter.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

            //remove the Dept Name 
            ddlDepartmentFilter.Items.Remove(ddlDepartmentFilter.Items.FindByValue(CommonConstants.DeptId_RaveDevelopment.ToString()));
            ddlDepartmentFilter.Items.Remove(ddlDepartmentFilter.Items.FindByText(CommonConstants.PRESALES_USA.ToString()));
            ddlDepartmentFilter.Items.Remove(ddlDepartmentFilter.Items.FindByText(CommonConstants.PRESALES_UK.ToString()));
            ddlDepartmentFilter.Items.Remove(ddlDepartmentFilter.Items.FindByText(CommonConstants.PRESALES_INDIA.ToString()));
            ddlDepartmentFilter.Items.Remove(ddlDepartmentFilter.Items.FindByText(CommonConstants.RAVECONSULTANT_USA.ToString()));
            ddlDepartmentFilter.Items.Remove(ddlDepartmentFilter.Items.FindByText(CommonConstants.RAVECONSULTANT_UK.ToString()));
            ddlDepartmentFilter.Items.Remove(ddlDepartmentFilter.Items.FindByText(CommonConstants.RAVEFORCASTEDPROJECT.ToString()));
            ddlDepartmentFilter.Items.Remove(ddlDepartmentFilter.Items.FindByText(CommonConstants.SALES_DEPARTMENT.ToString()));
            ddlDepartmentFilter.Items.Remove(ddlDepartmentFilter.Items.FindByText(CommonConstants.Senior_Mgt_DEPARTMENT.ToString()));
            ddlDepartmentFilter.Items.Remove(ddlDepartmentFilter.Items.FindByText(CommonConstants.Project_Mentee2010_DEPARTMENT.ToString()));

            ddlDepartment.Enabled = true;

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
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        // Initialise Business layer object
        Rave.HR.BusinessLayer.MRF.MRFDetail mrfProjectName = new Rave.HR.BusinessLayer.MRF.MRFDetail();
        try
        {
            // Call the Business layer method
            raveHRCollection = mrfProjectName.GetProjectName();

            DataSet dsProject = null;
            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
            dsProject = fourCBAL.GetProjectNameAddCreatorReviewer();

            if (raveHRCollection != null)
            {
                // Assign the data source to dropdown

                ddlProject.DataSource = dsProject;

                //ddlProject.DataTextField = CommonConstants.DDL_DataTextField;
                //ddlProject.DataValueField = CommonConstants.DDL_DataValueField;
                ddlProject.DataTextField = dsProject.Tables[0].Columns[1].ToString();
                ddlProject.DataValueField = dsProject.Tables[0].Columns[0].ToString();


                // Bind the data to dropdown
                ddlProject.DataBind();

                // Default value of dropdown is "Select"
                ddlProject.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);


                ddlProjectNameFilter.DataSource = raveHRCollection;
                ddlProjectNameFilter.DataTextField = CommonConstants.DDL_DataTextField;
                ddlProjectNameFilter.DataValueField = CommonConstants.DDL_DataValueField;

                // Bind the data to dropdown
                ddlProjectNameFilter.DataBind();
                // Default value of dropdown is "Select"
                ddlProjectNameFilter.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                ddlProjectNameFilter.Enabled = false;

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

    protected void ddlDepartmentFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartmentFilter.SelectedValue != "SELECT")
            {
                if (int.Parse(ddlDepartmentFilter.SelectedValue) != Convert.ToInt32(MasterEnum.Departments.Projects))
                {
                    ddlProjectNameFilter.SelectedIndex = 0;
                    ddlProjectNameFilter.Enabled = false;

                }
                else
                {
                    ddlProjectNameFilter.Enabled = true;
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
