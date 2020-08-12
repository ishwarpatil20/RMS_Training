//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2012 all rights reserved.
//
//  File:           ReportingOrFunctionalManager.aspx.cs
//  Author:         
//  Date written:   29 May 2012
//  Description:    To Resolve issue Id 28572 (CR). This is pop up window where user can set line manager and functional manager.

//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Collections;
using System.Data;
using Common.Constants;
using Common.AuthorizationManager;
using System.Windows.Forms;
using System.Web.Services;

public partial class ReportingOrFunctionalManager : BaseClass
{
    BusinessEntities.Employee employee = new BusinessEntities.Employee();
    string empId;
    private string SELECTONE = "Select";
    private string ZERO = "0";
    private static string sortExpression = string.Empty;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    BusinessEntities.ParameterCriteria objParameter = new BusinessEntities.ParameterCriteria();
    // Sets the image direction either upwards or downwards.
    private string imageDirection = string.Empty;

    protected void Page_Load(object sender, EventArgs e) 
     {
        HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
        HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();

        if (!ValidateURL())
        {
            Response.Redirect(CommonConstants.INVALIDURL, false);
        }
        else
        {
            employee = (BusinessEntities.Employee)Session[Common.SessionNames.EMPLOYEEDETAILS];
            imgFMSelectAll.Attributes.Add("onclick", "return popUpFunctionalManagerSearch();");
            imgPMSelectAll.Attributes.Add("onclick", "return popUpEmployeeSearch();");


            if (!Page.IsPostBack)
            {

                if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION_UPManger] == null)
                {
                    sortExpression = CommonConstants.EMPLOYEE_NAME;
                    Session[SessionNames.PREVIOUS_SORT_EXPRESSION_UPManger] = sortExpression;
                }
                else
                {
                    sortExpression = Session[SessionNames.PREVIOUS_SORT_EXPRESSION_UPManger].ToString();
                }

                if (Request.QueryString["page"] != null && Request.QueryString["page"] == "employeesummary")
                {
                    dataView.Visible = false;
                    ddlEmployeeList.Visible = true;
                    lblSelectEmployee.Visible = true;
                    lblSelectEmployee.Visible = true;
                    //btnReset.Visible = false;
                    //btnSave.Visible = false;
                }
                else
                {
                    dataView.Visible = true;
                    ddlEmployeeList.Visible = false;
                    lblSelectEmployee.Visible = false;
                    lblSelectEmployee.Visible = false;
                    SortGridView("EmployeeName", ASCENDING);
                    //btnReset.Visible = true;
                    //btnSave.Visible = true;
                }
                fillEmployeeDDL();
            }
        }
        
        //hidEMPId.Value = URL;

    }


    private void fillEmployeeDDL()
    {
        DataSet ds = new DataSet();
        Rave.HR.BusinessLayer.Employee.Employee addEmployeeBAL = new Rave.HR.BusinessLayer.Employee.Employee();
        //sanju kushwaha Start
        //issue 49187 Added new SP USP_Project_GetManagerList to load the manager list instead of all employees
        ds = addEmployeeBAL.GetManagersList();
        //End
        ddlEmployeeList.DataSource = ds.Tables[0];
        ddlEmployeeList.DataTextField = ds.Tables[0].Columns[1].ToString();
        ddlEmployeeList.DataValueField = ds.Tables[0].Columns[0].ToString();
        ddlEmployeeList.DataBind();

        ddlEmployeeList.Items.Insert(0, new ListItem(SELECTONE, ZERO));
    }


    protected void ddlEmployeeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        empId = ddlEmployeeList.SelectedItem.Value;
        dataView.Visible = true;
        //PopulateData();
        SortGridView("EmployeeName", ASCENDING);
    }



    protected void grdvListofReportingEmployees_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            sortExpression = e.SortExpression;

            if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION_UPManger] == null)
            {
                Session[SessionNames.PREVIOUS_SORT_EXPRESSION_UPManger] = sortExpression;
            }

            
                //Sort to opposite direction based upon previous sort direction
                if (Session[SessionNames.SORT_DIRECTION_UPManger] == null || GridViewSortDirection == SortDirection.Descending)
                {
                    GridViewSortDirection = SortDirection.Ascending;
                    SortGridView(sortExpression, ASCENDING);
                }
                else
                {
                    GridViewSortDirection = SortDirection.Descending;
                    SortGridView(sortExpression, DESCENDING);
                }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "grdvListofReportingEmployees_Sorting", "grdvListofEmployees_RowDataBound", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            throw objEx;
        }
    }


    /// <summary>
    /// Add the Sort Image to gridview header row
    /// </summary>
    /// <param name="headerRow"></param>
    private void AddSortImage(GridViewRow headerRow)
    {
        try
        {
            //Assign the sort direction of gridview to image
            imageDirection = GridViewSortDirection.ToString();

            if (!imageDirection.Equals(string.Empty))
            {
                // Create the sorting image based on the sort direction
                Image sortImage = new Image();

                if (imageDirection == SortOrder.Ascending.ToString())
                {
                    sortImage.ImageUrl = CommonConstants.IMAGE_UP_ARROW;
                    sortImage.AlternateText = CommonConstants.ASCENDING;
                }
                else if (imageDirection == SortOrder.Descending.ToString())
                {
                    sortImage.ImageUrl = CommonConstants.IMAGE_DOWN_ARROW;
                    sortImage.AlternateText = CommonConstants.DESCENDING;
                }

                // Add the image to the appropriate header cell
                switch (sortExpression)
                {
                    case "EmployeeName":
                        headerRow.Cells[2].Controls.Add(sortImage);
                        break;

                    case "ProjectName":
                        headerRow.Cells[3].Controls.Add(sortImage);
                        break;
                    
                    // 36732-Ambar-Start
                    case "DepartmentName":
                        headerRow.Cells[4].Controls.Add(sortImage);
                        break;

                    case "Designation":
                        headerRow.Cells[5].Controls.Add(sortImage);
                        break;
                    // 36732-Ambar-End
                }
            }
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "ReportingOrFunctionalManager.aspx", "AddSortImage", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
        }
    }


    /// <summary>
    /// Add the sorting image in the haeder of grid.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdvListofReportingEmployees_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //Check whether row is header or not.
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //if (Session[SessionNames.CONTRACT_ACTUALPAGECOUNT] != null)
               // {
                    if (grdvListofReportingEmployees.AllowSorting == true)
                    {
                        //Add sort Images to Grid View Header
                        AddSortImage(e.Row);
                    }
                //}
            }
        }

        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "ReportingOrFunctionalManager.aspx", "grdvListofContract_RowCreated", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }



    /// <summary>
    /// Get or set the GridViewSortDirection property
    /// </summary>
    private SortDirection GridViewSortDirection
    {
        get
        {
            if (Session[SessionNames.SORT_DIRECTION_UPManger] == null)
                Session[SessionNames.SORT_DIRECTION_UPManger] = SortDirection.Ascending;

            return (SortDirection)Session[SessionNames.SORT_DIRECTION_UPManger];
        }

        set
        {
            Session[SessionNames.SORT_DIRECTION_UPManger] = value;
        }
    }


    protected void grdvListofReportingEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // 36732-Ambar-Start--Corrected Index due to addtion of columns
                System.Web.UI.WebControls.TextBox txtFM = (System.Web.UI.WebControls.TextBox)e.Row.Cells[8].Controls[1];

                System.Web.UI.WebControls.Image imgFM = (System.Web.UI.WebControls.Image)e.Row.Cells[9].Controls[1];
                System.Web.UI.WebControls.TextBox txtRM = (System.Web.UI.WebControls.TextBox)e.Row.Cells[6].Controls[1];
                System.Web.UI.WebControls.Image imgRM = (System.Web.UI.WebControls.Image)e.Row.Cells[7].Controls[1];

                if (txtFM.Visible && txtFM.Enabled == false)
                {
                    e.Row.Cells[8].Attributes.Add("title", txtFM.Text);
                }
                if (txtRM.Visible && txtRM.Enabled == false)
                {
                    e.Row.Cells[6].Attributes.Add("title", txtRM.Text);
                }

                // 36732-Ambar-End

                if (txtFM.Visible)
                {
                    imgFM.Attributes["onclick"] = "popUpEmployeeSearchFMIndv('" + e.Row.DataItemIndex + "');";
                }
                else
                {
                    imgFM.Visible = false;
                }
                
                imgRM.Attributes["onclick"] = "popUpEmployeeSearchRMIndv('" + e.Row.DataItemIndex + "');";
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "ReportingOrFunctionalManager.aspx", "grdvListofEmployees_RowDataBound", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            throw objEx;
        }
    }

     /// <summary>
    /// Sort the gridview as per SortExpression and SortDirection
    /// </summary>
    /// <param name="sortExpression"></param>
    /// <param name="direction"></param>
    private void SortGridView(string sortExpression, string direction)
    {
        try
        {
             objParameter.SortExpressionAndDirection = sortExpression + direction;
             PopulateData();
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "ReportingOrFunctionalManager", "SortGridView", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
        }
    }




    private void PopulateData()
    {
        try
        {
            BusinessEntities.Employee employee = new BusinessEntities.Employee();

            if (Request.QueryString["page"] != null && Request.QueryString["page"] == "employeesummary")
            {
                empId = ddlEmployeeList.SelectedItem.Value;
            }
            else
            {
                employee = (BusinessEntities.Employee)Session[Common.SessionNames.EMPLOYEEDETAILS];
                empId = employee.EMPId.ToString();
            }

            grdvListofReportingEmployees.Visible = true;
            Rave.HR.BusinessLayer.Employee.Employee addEmployeeBAL = new Rave.HR.BusinessLayer.Employee.Employee();
            AuthorizationManager authoriseduser = new AuthorizationManager();
            DataSet ds = new DataSet();
            ds = addEmployeeBAL.GetReportingFunctionalManagerIds(empId, objParameter);
            grdvListofReportingEmployees.DataSource = ds;
            grdvListofReportingEmployees.DataBind();

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count == 0)
                {
                    //btnSave.Visible = false;
                    imgFMSelectAll.Visible = false;
                    imgPMSelectAll.Visible = false;
                    lblFMName.Visible = false;
                    lblPMName.Visible = false;
                    txtFMName.Visible = false;
                    txtRMName.Visible = false;
                    //btnSave.Visible = false;
                    //btnReset.Visible = false;
                }
                else
                {
                    imgFMSelectAll.Visible = true;
                    imgPMSelectAll.Visible = true;
                    lblFMName.Visible = true;
                    lblPMName.Visible = true;
                    txtFMName.Visible = true;
                    txtRMName.Visible = true;
                    txtFMName.Enabled = false;
                    txtRMName.Enabled = false;
                    //btnSave.Visible = true;
                    //btnSave.Visible = true;
                    //btnReset.Visible = true;
                }
                
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "ReportingOrFunctionalManager.aspx", "PopulateData", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            throw objEx;
        }
    }


    protected void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlEmployeeList.Visible == true)
            {
                if (ddlEmployeeList.SelectedIndex > 0)
                {
                    if (grdvListofReportingEmployees.Rows.Count > 0)
                    {
                        SortGridView("EmployeeName", ASCENDING);
                    }
                    else
                    {
                        grdvListofReportingEmployees.Visible = false;
                        ddlEmployeeList.SelectedIndex = 0;
                    }
                }
                else
                {
                    Page page = HttpContext.Current.Handler as Page;
                    ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Please select employee.');", true);
                }
            }
            if (ddlEmployeeList.Visible == false)
            {
                SortGridView("EmployeeName", ASCENDING);
            }

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, "ReportingOrFunctionalManager.aspx", "btnReset_Click", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Rave.HR.BusinessLayer.Employee.Employee addEmployeeBAL = new Rave.HR.BusinessLayer.Employee.Employee();
            string chkUpdateEmpNo = "";

            if (Request.QueryString["page"] != null && Request.QueryString["page"] == "employeesummary")
            {
                empId = ddlEmployeeList.SelectedItem.Value;
            }
            else
            {
                employee = (BusinessEntities.Employee)Session[Common.SessionNames.EMPLOYEEDETAILS];
                empId = employee.EMPId.ToString();
            }

            foreach (GridViewRow gvRow in grdvListofReportingEmployees.Rows)
            {
                System.Web.UI.WebControls.CheckBox chkSel = (System.Web.UI.WebControls.CheckBox)gvRow.FindControl("chkSelect");
                System.Web.UI.WebControls.HiddenField hdEmpNo = (System.Web.UI.WebControls.HiddenField)gvRow.FindControl("HfEmpId");
                System.Web.UI.WebControls.HiddenField hdProjectId = (System.Web.UI.WebControls.HiddenField)gvRow.FindControl("HfProjectId");
                System.Web.UI.WebControls.HiddenField hdReportingToName = (System.Web.UI.WebControls.HiddenField)gvRow.FindControl("HfReportingToName");
                System.Web.UI.WebControls.HiddenField HdReportingToCommonName = (System.Web.UI.WebControls.HiddenField)gvRow.FindControl("HfReportingToCommonName");
                System.Web.UI.WebControls.HiddenField HdFunctionalToName = (System.Web.UI.WebControls.HiddenField)gvRow.FindControl("HfFunctionalToName");

                if (chkSel != null && chkSel.Checked)
                {
                        if (hdProjectId.Value != "0")
                        {
                            //Update Project Allocation table T_ProjectAllocation
                            addEmployeeBAL.UpdateEmployeeFMRMForProjectAllocation(Convert.ToInt32(hdEmpNo.Value), hdReportingToName.Value, Convert.ToInt32(hdProjectId.Value));
                        }

                        if (!string.Equals(chkUpdateEmpNo.Trim(), hdEmpNo.Value.Trim()))
                        {
                            chkUpdateEmpNo = hdEmpNo.Value;
                            //Siddhesh Arekar : Reporting Manager not updating : 17/06/2015 : Starts
                            //Description - Added new parameter in ReportingToCommonName function for passing changed FunctionalToName
                            string HdReportingCommonNew = ReportingToCommonName(empId, hdEmpNo.Value, HdReportingToCommonName.Value, HdFunctionalToName.Value);
                            //Siddhesh Arekar : Reporting Manager not updating : 17/06/2015 : Ends

                            if (HdReportingCommonNew.Equals("DuplicateFunctionalManager"))
                            {
                                Page page = HttpContext.Current.Handler as Page;
                                ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Two different fucntional managers selected for same employee.');", true);
                                return;
                            }
                            
                            //update common functional and reporting to T_Employees
                            if (HdReportingCommonNew != "")
                            {
                                addEmployeeBAL.UpdateEmployeeFMRM(Convert.ToInt32(hdEmpNo.Value), HdReportingCommonNew, Convert.ToInt32(HdFunctionalToName.Value));
                            }
                        }
                }
            }

            //Umesh: Issue 'Modal Popup issue in chrome' Starts
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "SaveMessage", "alert('Data Saved Successfully'); jQuery.modalDialog.getCurrent().close();", true);
            //Response.Write("<script type='text/javascript'>alert('Data Saved Successfully'); window.close();</script>");
            //Umesh: Issue 'Modal Popup issue in chrome' Ends
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, "ReportingOrFunctionalManager.aspx", "btnSave_Click", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
        }
    }

    private string ReportingToCommonName(string removeEmpId, string empId, string hdCommonReportingToNameDB, string hdFunctionalToNameDB)
    {
        string strReturnVal = "";

        //if (removeEmpId.Trim() != hdCommonReportingToNameDB.Trim())
        //{
            //OLD Reporting ID
            List<string> allReportingTo = hdCommonReportingToNameDB.Split(',').ToList();
            List<string> lstChkDifferentFunctionalM = new List<string> { };
            List<string> lstFinalFunctionalM = new List<string> { };

            //REMOVE the resigned manager Id
            allReportingTo.Remove(removeEmpId);

            foreach (GridViewRow gvRow in grdvListofReportingEmployees.Rows)
            {
                System.Web.UI.WebControls.HiddenField hdEmpNo = (System.Web.UI.WebControls.HiddenField)gvRow.FindControl("HfEmpId");
                System.Web.UI.WebControls.HiddenField hdReportingToName = (System.Web.UI.WebControls.HiddenField)gvRow.FindControl("HfReportingToName");
                System.Web.UI.WebControls.HiddenField hdProjectId = (System.Web.UI.WebControls.HiddenField)gvRow.FindControl("HfProjectId");
                System.Web.UI.WebControls.CheckBox chkSel = (System.Web.UI.WebControls.CheckBox)gvRow.FindControl("chkSelect");
                System.Web.UI.WebControls.HiddenField HdFunctionalToName = (System.Web.UI.WebControls.HiddenField)gvRow.FindControl("HfFunctionalToName");

                if (string.Equals(hdEmpNo.Value.Trim(), empId.Trim()) && chkSel.Checked)
                {
                    //new functional manager list created
                    lstChkDifferentFunctionalM.Add(HdFunctionalToName.Value);

                    if (hdProjectId.Value != "0")
                    {
                        if (string.Equals(hdEmpNo.Value.Trim(), empId.Trim()))
                        {
                            allReportingTo.Add(hdReportingToName.Value);
                        }
                    }
                    else
                    {
                        List<string> newReportingTo = hdReportingToName.Value.Split(',').ToList();
                        //Siddhesh Arekar : Reporting Manager not updating : 17/06/2015 : Starts
                        if(hdCommonReportingToNameDB == hdFunctionalToNameDB)
                        //Siddhesh Arekar : Reporting Manager not updating : 17/06/2015 : End
                            newReportingTo.Remove(removeEmpId);

                        foreach (var itmR in newReportingTo)
                        {
                            allReportingTo.Add(itmR);
                        }
                    }
                }
            }

            //select distinct reportingTo from list
            foreach (var itm in allReportingTo.Distinct())
            {
                if (strReturnVal == "")
                {
                    strReturnVal = itm;
                }
                else
                {
                    strReturnVal = strReturnVal + "," + itm;
                }
            }

            //if two different functional manager exists for the employee then return error message.
            lstFinalFunctionalM = lstChkDifferentFunctionalM.Distinct().ToList();
            if (lstFinalFunctionalM.Count > 1)
            {
                return "DuplicateFunctionalManager";
            }
       // }

        return strReturnVal;
   }
}
