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
using System.Configuration;
using System.Web.UI.HtmlControls;
using BusinessEntities;

public partial class CostCodeUpdateManager : BaseClass
{

    BusinessEntities.Employee employee = new BusinessEntities.Employee();
    Common.AuthorizationManager.AuthorizationManager objAuth;
    ArrayList arrRolesForUser = new ArrayList();
    private const string CLASS_NAME = "CostCodeUpdateManager.aspx.cs";

    string empId;
    private string SELECTONE = "Select";
    private string ZERO = "0";
    private static string sortExpression = string.Empty;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    private static int Mode = 2; //1- Project Specific, 2- Employee Specific
    private int projectID = 0;
    DataTable dtEmployeeProjectCodtCodes;


    BusinessEntities.ParameterCriteria objParameter = new BusinessEntities.ParameterCriteria();
    // Sets the image direction either upwards or downwards.
    private string imageDirection = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
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
                //imgFMSelectAll.Attributes.Add("onclick", "return popUpFunctionalManagerSearch();");
                //imgPMSelectAll.Attributes.Add("onclick", "return popUpEmployeeSearch();");


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
                        dataViewForProject.Visible = false;
                        dataViewForEmployee.Visible = true;
                        ddlProjectList.Visible = true;
                        lblSelectProject.Visible = true;
                        lblSelectEmployee.Visible = true;
                        //btnReset.Visible = false;
                        //btnSave.Visible = false;
                    }
                    else
                    {
                        dataViewForProject.Visible = true;
                        dataViewForEmployee.Visible = false;
                        ddlProjectList.Visible = false;
                        lblSelectProject.Visible = false;
                        lblSelectEmployee.Visible = false;
                        SortGridView("EmployeeName", ASCENDING);
                        //btnReset.Visible = true;
                        //btnSave.Visible = true;
                    }
                    btnAddRow.Visible = false;
                    fillEmployeeProjectDDL();
                    ddlMode_SelectedIndexChanged(sender, e);
                    btnAddRow_Click(sender, e);
                    //Session["StatusMessage"] = null;
                }


                //if (Session["StatusMessage"] == null)
                //{
                //    lblMessage.Text = "";
                //}
                //else
                //{
                //    lblMessage.Text = Session["StatusMessage"].ToString();
                //}
            }

            //hidEMPId.Value = URL;
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "Page_Load", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }


    private void fillEmployeeProjectDDL()
    {
        try
        {
            DataSet ds = new DataSet();
            Rave.HR.BusinessLayer.Employee.Employee addEmployeeBAL = new Rave.HR.BusinessLayer.Employee.Employee();


            //Check the employee Role and show the values accordingly

            AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();

            arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(RaveHRAuthorizationManager.getLoggedInUser());
            int role = 0;

            //Siddharth 14 April 2015 Start
            string strNISReportsAccess = string.Empty;
            if (ConfigurationManager.AppSettings["NISReportsAccess"] != null)
            {
                strNISReportsAccess = ConfigurationManager.AppSettings["NISReportsAccess"].ToString();
            }

            if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM))
            {//RMO Team
                role = 0;
            }
            else if ((arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEPM) && strNISReportsAccess.Contains(RaveHRAuthorizationManager.getLoggedInUser())))
            {//NIS Manager
                role = 1;
            }

            //Siddharth 14 April 2015 End


            ds = addEmployeeBAL.GetEmployeeAndProjectForCCManager(role);

            ddlProjectList.DataSource = ds.Tables[0];
            ddlProjectList.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlProjectList.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlProjectList.DataBind();
            ddlProjectList.Items.Insert(0, new ListItem(SELECTONE, ZERO));


            ddlEmployeeList.DataSource = ds.Tables[1];
            ddlEmployeeList.DataTextField = ds.Tables[1].Columns[1].ToString();
            ddlEmployeeList.DataValueField = ds.Tables[1].Columns[0].ToString();
            ddlEmployeeList.DataBind();
            ddlEmployeeList.Items.Insert(0, new ListItem(SELECTONE, ZERO));

            DdlCostCode.DataSource = ds.Tables[2];
            DdlCostCode.DataTextField = ds.Tables[2].Columns[1].ToString();
            DdlCostCode.DataValueField = ds.Tables[2].Columns[0].ToString();
            DdlCostCode.DataBind();
            DdlCostCode.Items.Insert(0, new ListItem(SELECTONE, ZERO));
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "CostCodeUpdateManager", "GetConsolidated", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    protected void ddlProjectList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            projectID = Convert.ToInt16(ddlProjectList.SelectedItem.Value);
            dataViewForProject.Visible = true;
            //PopulateData();
            SortGridView("EmployeeName", ASCENDING);
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

    protected void ddlEmployeeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            empId = ddlProjectList.SelectedItem.Value;
            dataViewForEmployee.Visible = true;
            //PopulateData();

            //Siddharth 8th May 2015 Start
            //Issue:- 1 blank row is added every time the Partially Billed Employee is selected.
            //So we have to check if it is visible then set its datasource to null
            if (grdvListofPartiallyBilledEmployees.Visible == true)
            {
                if (grdvListofPartiallyBilledEmployees.Rows.Count > 0)
                {
                    grdvListofPartiallyBilledEmployees.DataSource = null;
                    grdvListofPartiallyBilledEmployees.DataBind();
                }
            }
            //Siddharth 8th May 2015 End

            SortGridView("EmployeeName", ASCENDING);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlEmployeeList_SelectedIndexChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdvListofProjects_Sorting(object sender, GridViewSortEventArgs e)
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "grdvListofProjects_Sorting", "grdvListofEmployees_RowDataBound", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            throw objEx;
        }
    }


    protected void grdvListofEmployees_Sorting(object sender, GridViewSortEventArgs e)
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "grdvListofProjects_Sorting", "grdvListofEmployees_RowDataBound", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
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

                    case "CostCode":
                        headerRow.Cells[3].Controls.Add(sortImage);
                        break;

                    // 36732-Ambar-Start
                    case "Billing":
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
    protected void grdvListofProjects_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //Check whether row is header or not.
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //if (Session[SessionNames.CONTRACT_ACTUALPAGECOUNT] != null)
                // {
                if (grdvListofProjects.AllowSorting == true)
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
    /// Add the sorting image in the haeder of grid.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdvListofEmployees_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //Check whether row is header or not.
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //if (Session[SessionNames.CONTRACT_ACTUALPAGECOUNT] != null)
                // {
                if (grdvListofProjects.AllowSorting == true)
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "CostCodeupdateManager.aspx", "grdvListofemployees_RowCreated", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
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


    protected void grdvListofProjects_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //// 36732-Ambar-Start--Corrected Index due to addtion of columns
                //System.Web.UI.WebControls.TextBox txtFM = (System.Web.UI.WebControls.TextBox)e.Row.Cells[8].Controls[1];

                //System.Web.UI.WebControls.Image imgFM = (System.Web.UI.WebControls.Image)e.Row.Cells[9].Controls[1];
                //System.Web.UI.WebControls.TextBox txtRM = (System.Web.UI.WebControls.TextBox)e.Row.Cells[6].Controls[1];
                //System.Web.UI.WebControls.Image imgRM = (System.Web.UI.WebControls.Image)e.Row.Cells[7].Controls[1];

                //if (txtFM.Visible && txtFM.Enabled == false)
                //{
                //    e.Row.Cells[8].Attributes.Add("title", txtFM.Text);
                //}
                //if (txtRM.Visible && txtRM.Enabled == false)
                //{
                //    e.Row.Cells[6].Attributes.Add("title", txtRM.Text);
                //}

                //// 36732-Ambar-End

                //if (txtFM.Visible)
                //{
                //    imgFM.Attributes["onclick"] = "popUpEmployeeSearchFMIndv('" + e.Row.DataItemIndex + "');";
                //}
                //else
                //{
                //    imgFM.Visible = false;
                //}

                //imgRM.Attributes["onclick"] = "popUpEmployeeSearchRMIndv('" + e.Row.DataItemIndex + "');";
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


    protected void grdvListofEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                //// 36732-Ambar-Start--Corrected Index due to addtion of columns
                System.Web.UI.WebControls.HiddenField hfldCostcodeId = (System.Web.UI.WebControls.HiddenField)e.Row.FindControl("hfldCostcodeId");
                System.Web.UI.WebControls.ImageButton imgDeleteCC = (System.Web.UI.WebControls.ImageButton)e.Row.FindControl("imgDeleteCC");
                if (hfldCostcodeId.Value == "")
                    imgDeleteCC.Visible = false;
                //System.Web.UI.WebControls.Image imgFM = (System.Web.UI.WebControls.Image)e.Row.Cells[9].Controls[1];
                //System.Web.UI.WebControls.TextBox txtRM = (System.Web.UI.WebControls.TextBox)e.Row.Cells[6].Controls[1];
                //System.Web.UI.WebControls.Image imgRM = (System.Web.UI.WebControls.Image)e.Row.Cells[7].Controls[1];

                //if (txtFM.Visible && txtFM.Enabled == false)
                //{
                //    e.Row.Cells[8].Attributes.Add("title", txtFM.Text);
                //}
                //if (txtRM.Visible && txtRM.Enabled == false)
                //{
                //    e.Row.Cells[6].Attributes.Add("title", txtRM.Text);
                //}

                //// 36732-Ambar-End

                //if (txtFM.Visible)
                //{
                //    imgFM.Attributes["onclick"] = "popUpEmployeeSearchFMIndv('" + e.Row.DataItemIndex + "');";
                //}
                //else
                //{
                //    imgFM.Visible = false;
                //}

                //imgRM.Attributes["onclick"] = "popUpEmployeeSearchRMIndv('" + e.Row.DataItemIndex + "');";
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

            //if (Request.QueryString["page"] != null && Request.QueryString["page"] == "employeesummary")
            //{
            //    empId = ddlProjectList.SelectedItem.Value;
            //}
            //else
            //{
            //    employee = (BusinessEntities.Employee)Session[Common.SessionNames.EMPLOYEEDETAILS];
            //    empId = employee.EMPId.ToString();
            //}


            if (CostCodeUpdateManager.Mode == 1)//1- Project Specific, 2- Employee Specific
            {
                projectID = Convert.ToInt16(ddlProjectList.SelectedItem.Value);
                empId = "0";
            }
            else
            {
                empId = ddlEmployeeList.SelectedItem.Value;
            }



            grdvListofProjects.Visible = true;
            Rave.HR.BusinessLayer.Employee.Employee addEmployeeBAL = new Rave.HR.BusinessLayer.Employee.Employee();
            AuthorizationManager authoriseduser = new AuthorizationManager();


            DataSet ds = new DataSet();
            //ds = addEmployeeBAL.GetReportingFunctionalManagerIds(empId, objParameter);
            ds = addEmployeeBAL.GetProjectWiseEmpCostCodeDetailsForCCManager(Convert.ToInt16(empId.ToString().Trim()), projectID, CostCodeUpdateManager.Mode);

            if (CostCodeUpdateManager.Mode == 1)//1- Project Specific, 2- Employee Specific
            {
                grdvListofProjects.DataSource = ds;
                grdvListofProjects.DataBind();
            }
            else
            {
                //Logic:- Check if the Employee is Project(Allocated to Project) or Partial(Not allocated to a Project but have CostCode and Billing)
                //If Project then show grdvListofEmployees
                //Else if Partial then show spanPartiallyBilledEmployees. Partial will have option to Add/Delete rows for the employees.
                //else the employee has no Cost Code no billing and no project. Show the empty grid here to fill 

                string flag = string.Empty;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    flag = "Project";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (ds.Tables[0].Rows[i]["flag"].ToString() == "Partial")
                        {
                            flag = "Partial";
                            break;
                        }
                    }
                }

                if(flag == "Project")
                {

                    /* Hide the partially Billed Grid */
                    grdvListofEmployees.Visible = true;
                    spanPartiallyBilledEmployees.Visible = false;
                    grdvListofPartiallyBilledEmployees.Visible = false;
                    btnAddRow.Visible = false;

                    grdvListofEmployees.DataSource = ds;
                    grdvListofEmployees.DataBind();

                    //Make the Billing Editable of project is not selected
                    if (grdvListofEmployees.Rows.Count > 0)
                    {
                        foreach (GridViewRow gvRow in grdvListofEmployees.Rows)
                        {
                            System.Web.UI.WebControls.HiddenField HfPrjID = (System.Web.UI.WebControls.HiddenField)gvRow.FindControl("HfPrjID");
                            //System.Web.UI.WebControls.TextBox Billing = (System.Web.UI.WebControls.TextBox)gvRow.FindControl("Billing");
                            //if (HfPrjID.Value == "" || string.IsNullOrEmpty(HfPrjID.Value.Trim()))
                            //    Billing.ReadOnly = false;
                            //else
                            //    Billing.ReadOnly = true;
                        }
                    }
                }
                else if(flag == "Partial")
                {
                    //Show the Grid of Partially Billed Employees here
                    grdvListofEmployees.Visible = false;
                    spanPartiallyBilledEmployees.Visible = true;
                    grdvListofPartiallyBilledEmployees.Visible = true;

                    //Add a datacolumn and then bind it to grid
                    ds.Tables[0].Columns.Add(new DataColumn("ProjectNo", typeof(string)));
                    for (int i = 0; i < ds.Tables[0].Rows.Count ; i++)
                    {
                        ds.Tables[0].Rows[i]["ProjectNo"] = i + 1;
                    }
                    grdvListofPartiallyBilledEmployees.DataSource = ds;
                    grdvListofPartiallyBilledEmployees.DataBind();
                    btnAddRow.Visible = true;
                }
                else
                {
                    //enable the other grid with add new functionality
                    grdvListofEmployees.Visible = false;
                    spanPartiallyBilledEmployees.Visible = true;
                    grdvListofPartiallyBilledEmployees.Visible = true;
                    DefaultGridView();
                    btnAddRow.Visible = true;
                    //btnAddRow_Click(sender, e);
                }
            }
            TrOverrideCostcode.Visible = false;
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
            DdlCostCode.SelectedIndex = 0;
            if (CostCodeUpdateManager.Mode == 1)
            {
                ddlProjectList.SelectedIndex = 0;
                grdvListofProjects.DataSource = null;
                grdvListofProjects.DataBind();
            }
            else
            {
                ddlEmployeeList.SelectedIndex = 0;                
                grdvListofEmployees.DataSource = null;
                grdvListofEmployees.DataBind();
                grdvListofPartiallyBilledEmployees.DataSource = null;
                grdvListofPartiallyBilledEmployees.DataBind();
                btnAddRow.Visible = false;
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

            //Get the identity of Logged on User

            string strUserIdentity = string.Empty;
            strUserIdentity = HttpContext.Current.ApplicationInstance.Session["WindowsUsername"].ToString().Trim();

            BusinessEntities.Employee Employee = new BusinessEntities.Employee();
            Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
            int LastModifiedByID = employeeBL.Employee_GetWindowsUsenameofLoggedInUserForCCManager(strUserIdentity);

            //Venkatesh : Start : 20-Jun-2016 : Actual vs Budget
            //Code will not execute as projectwise functionality is disabled
            if (CostCodeUpdateManager.Mode == 1)
            {
                if (DdlCostCode.SelectedItem.Text!="Select" && ddlProjectList.SelectedItem.Text != "Select")
                {
                    projectID = Convert.ToInt16(ddlProjectList.SelectedItem.Value);
                    empId = "0";

                    foreach (GridViewRow gvRow in grdvListofProjects.Rows)
                    {
                        System.Web.UI.WebControls.CheckBox chkSel = (System.Web.UI.WebControls.CheckBox)gvRow.FindControl("chkSelect");
                        if (chkSel.Checked == true)
                        {
                            System.Web.UI.WebControls.HiddenField HfEmpID = (System.Web.UI.WebControls.HiddenField)gvRow.FindControl("HfEmpID");
                            System.Web.UI.WebControls.Label Name = (System.Web.UI.WebControls.Label)gvRow.FindControl("Name");
                            System.Web.UI.WebControls.Label CostCode = (System.Web.UI.WebControls.Label)gvRow.FindControl("CostCode");
                            System.Web.UI.WebControls.Label Billing = (System.Web.UI.WebControls.Label)gvRow.FindControl("Billing");

                            addEmployeeBAL.UpdateEmpCostCodeProjReleaseForCCManager(HfEmpID.Value, Convert.ToString(projectID), Billing.Text, DdlCostCode.SelectedValue.ToString(), LastModifiedByID);
                        }
                    }

                    //Session["StatusMessage"] = "Successfully saved the Cost Code details for Project " + ddlProjectList.SelectedItem.Text;
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Cost Code Update Manager", "alert('Successfully saved the Cost Code details');", true);
                    //Refresh the Popup and display latest value
                    //fillEmployeeProjectDDL();
                    //ddlProjectList.SelectedItem.Value = Convert.ToString(projectID);
                    ////ddlProjectList_SelectedIndexChanged(sender, e);
                    PopulateData();                    
                }
                else
                {
                    //Session["StatusMessage"] = "Please fill all the details";
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Cost Code Update Manager", "alert('Please fill all the details');", true);
                }
            }
            else
            {

                empId = ddlEmployeeList.SelectedItem.Value;

                if (grdvListofEmployees.Rows.Count > 0 && grdvListofEmployees.Visible == true)
                {
                    if (DdlCostCode.SelectedItem.Text != "Select" && ddlEmployeeList.SelectedItem.Text != "Select")
                    {
                        //Venkatesh : Start : 20-Jun-2016 : Actual vs Budget
                        foreach (GridViewRow gvRow in grdvListofEmployees.Rows)
                        {
                            System.Web.UI.WebControls.CheckBox chkSel = (System.Web.UI.WebControls.CheckBox)gvRow.FindControl("chkSelect");
                            System.Web.UI.WebControls.HiddenField HfPrjID = (System.Web.UI.WebControls.HiddenField)gvRow.FindControl("HfPrjID");
                            System.Web.UI.WebControls.HiddenField hfldCostcodeId = (System.Web.UI.WebControls.HiddenField)gvRow.FindControl("hfldCostcodeId");
                            if (chkOverrideCostcode.Checked==false && chkSel.Checked == false )
                            {
                                if (DdlCostCode.SelectedValue.ToString() != hfldCostcodeId.Value.ToString() && hfldCostcodeId.Value.ToString()!="")
                                {
                                    TrOverrideCostcode.Visible = true;
                                    return;
                                }
                            }
                        }
                        //Venkatesh : End: 20-Jun-2016 : Actual vs Budget

                        foreach (GridViewRow gvRow in grdvListofEmployees.Rows)
                        {
                            System.Web.UI.WebControls.CheckBox chkSel = (System.Web.UI.WebControls.CheckBox)gvRow.FindControl("chkSelect");
                            if (chkSel.Checked == true)
                            {
                                System.Web.UI.WebControls.HiddenField HfPrjID = (System.Web.UI.WebControls.HiddenField)gvRow.FindControl("HfPrjID");
                                //System.Web.UI.WebControls.HiddenField HfEmpCCId = (System.Web.UI.WebControls.HiddenField)gvRow.FindControl("HfEmpCCId");
                                System.Web.UI.WebControls.Label Name = (System.Web.UI.WebControls.Label)gvRow.FindControl("Name");
                                System.Web.UI.WebControls.Label CostCode = (System.Web.UI.WebControls.Label)gvRow.FindControl("CostCode");
                                System.Web.UI.WebControls.Label Billing = (System.Web.UI.WebControls.Label)gvRow.FindControl("Billing");
                                //if (HfEmpCCId != null)
                                //{
                                //if (string.IsNullOrEmpty(HfEmpCCId.Value.Trim()))
                                //{
                                    addEmployeeBAL.UpdateEmpCostCodeProjReleaseForCCManager(empId, HfPrjID.Value, Billing.Text, DdlCostCode.SelectedValue.ToString(), LastModifiedByID);
                                //}
                                //else
                                //{
                                //    addEmployeeBAL.UpdateEmpCostCodeProjReleaseForCCManager(empId, HfPrjID.Value, Billing.Text, DdlCostCode.SelectedValue.ToString(), LastModifiedByID, Convert.ToInt16(HfEmpCCId.Value));
                                //}
                                // }
                            }
                        }
                        //Session["StatusMessage"] = "Successfully saved the Cost Code details for Employee " + ddlEmployeeList.SelectedItem.Text;
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Cost Code Update Manager", "alert('Successfully saved the Cost Code details');", true);
                        //Refresh the Popup and display latest value
                        //fillEmployeeProjectDDL();
                        //ddlEmployeeList.SelectedItem.Value = empId;
                        ////ddlEmployeeList_SelectedIndexChanged(sender, e);
                        PopulateData();                        
                    }
                    else
                    {
                        //Session["StatusMessage"] = "Please fill all the details";
                        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Cost Code Update Manager", "alert('Please fill all the details');", true);
                    }
                }
                else if (grdvListofPartiallyBilledEmployees.Rows.Count > 0 && grdvListofPartiallyBilledEmployees.Visible == true)
                {
                    //Siddharth 3 April 2015 Start
                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("EmpId", typeof(string)));
                    dt.Columns.Add(new DataColumn("ProjectNo", typeof(string)));
                    dt.Columns.Add(new DataColumn("ProjectName", typeof(string)));
                    dt.Columns.Add(new DataColumn("CostCode", typeof(string)));
                    dt.Columns.Add(new DataColumn("Billing", typeof(string)));
                    DataRow dr = null;
                    foreach (GridViewRow gvRow in grdvListofPartiallyBilledEmployees.Rows)
                    {
                        dr = dt.NewRow();
                        dr["EmpId"] = empId;
                        //dr["ProjectName"] = ((DropDownList)gvRow.FindControl("ddlProject")).SelectedValue;
                        dr["ProjectName"] = DBNull.Value;
                        dr["CostCode"] = ((System.Web.UI.WebControls.TextBox)gvRow.FindControl("txtCostCode")).Text;
                        dr["Billing"] = ((System.Web.UI.WebControls.TextBox)gvRow.FindControl("txtBilling")).Text;
                        dt.Rows.Add(dr);
                    }
                    addEmployeeBAL.UpdateEmployeeCostCode(dt, Convert.ToInt16(empId), LastModifiedByID);


                    grdvListofPartiallyBilledEmployees.DataSource = null;
                    grdvListofPartiallyBilledEmployees.DataBind();

                    //Session["StatusMessage"] = "Successfully saved the Cost Code details for Employee " + ddlEmployeeList.SelectedItem.Text;
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Saved Data Successfully", "alert('Successfully saved the Cost Code details');", true);
                    //Refresh the Popup and display latest value
                    PopulateData();
                    //fillEmployeeProjectDDL();
                    //ddlEmployeeList_SelectedIndexChanged(sender, e);
                    //ddlEmployeeList.SelectedItem.Value = empId;                    
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Data not Saved", "alert('No Data to Save! Please fill details then click Save');", true);
                }
            }

            //Finally call the Page Load Event again
           // Page_Load(sender, e);

            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "SaveMessage", "alert('Data Saved Successfully'); jQuery.modalDialog.getCurrent().close();", true);
            //ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "SaveMessage", "alert('Data Saved Successfully');", true);

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
    
       

    protected void ddlMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlMode.SelectedItem.Value.ToString() == "Project")
            {
                pnlProjectSpecificCostCode.Visible = true;
                pnlEmployeeSpecificCostCode.Visible = false;
                CostCodeUpdateManager.Mode = 1;
            }
            else if (ddlMode.SelectedItem.Value.ToString() == "Employee")
            {
                pnlProjectSpecificCostCode.Visible = false;
                pnlEmployeeSpecificCostCode.Visible = true;
                CostCodeUpdateManager.Mode = 2;
            }
            else
            {
                pnlProjectSpecificCostCode.Visible = false;
                pnlEmployeeSpecificCostCode.Visible = false;
                CostCodeUpdateManager.Mode = 0;
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlMode_SelectedIndexChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }





    protected void gvCostCode_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //DropDownList ddlProject = (DropDownList)e.Row.FindControl("ddlProject");
                //HiddenField HFSkillNo = (HiddenField)e.Row.FindControl("HFSkillName");
                
                //ddlProject.DataSource = this.GetProjectCostCodes();
                //ddlProject.DataTextField = Common.CommonConstants.DDL_DataValueField;
                //ddlProject.DataValueField = Common.CommonConstants.DDL_DataTextField;
                //ddlProject.DataBind();
                //ddlProject.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
                //if (HFSkillNo.Value != "")
                //    ddlProject.SelectedValue = HFSkillNo.Value.ToString();
            }
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "CostCodeUpdateManager", "gvSkillCriteria_RowDataBound", EventIDConstants.RAVE_EMP_SKILL_SEARCH_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    protected void gvCostCode_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //int RowID = int.Parse(e.CommandArgument.ToString());
            GridViewRow row = (GridViewRow)(((System.Web.UI.WebControls.Image)e.CommandSource).NamingContainer);
            int RowID = row.RowIndex;
            //Add 1 to RowID to maintain index
            RowID = RowID + 1;
            if (e.CommandName == "DeleteRow")
            {
                //if (RowID > 1)
                //{
                //above line is commented so that all rows can be deleted
                dtEmployeeProjectCodtCodes = new DataTable();

                dtEmployeeProjectCodtCodes.Columns.Add(new DataColumn("ProjectNo", typeof(string)));
                dtEmployeeProjectCodtCodes.Columns.Add(new DataColumn("ProjectName", typeof(string)));
                dtEmployeeProjectCodtCodes.Columns.Add(new DataColumn("CostCode", typeof(string)));
                dtEmployeeProjectCodtCodes.Columns.Add(new DataColumn("Billing", typeof(string)));
                int i = 1;
                foreach (GridViewRow gdr in grdvListofPartiallyBilledEmployees.Rows)
                {
                    HiddenField hfSkillNo = (HiddenField)gdr.FindControl("HFSkillNo");
                    if (!string.IsNullOrEmpty(hfSkillNo.Value.ToString().Trim()) && !hfSkillNo.Value.ToString().Equals(CommonConstants.SELECT))
                    {
                        if (RowID != Convert.ToInt32(hfSkillNo.Value))
                        {
                            DataRow dr = dtEmployeeProjectCodtCodes.NewRow();
                            dr["ProjectNo"] = i;
                            //dr["ProjectName"] = ((DropDownList)gdr.FindControl("ddlProject")).SelectedValue;
                            dr["ProjectName"] = DBNull.Value;
                            dr["CostCode"] = ((System.Web.UI.WebControls.TextBox)gdr.FindControl("txtCostCode")).Text;
                            dr["Billing"] = ((System.Web.UI.WebControls.TextBox)gdr.FindControl("txtBilling")).Text;
                            dtEmployeeProjectCodtCodes.Rows.Add(dr);
                            i++;
                        }
                    }
                }
                //ViewState["EmployeeProjectCodeCode"] = dtEmployeeProjectCodtCodes;
                BindGridView(dtEmployeeProjectCodtCodes);
                //}
                //else
                //{
                //    //Session["StatusMessage"] = "Cannot delete the first row";
                //    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Cost Code Update Manager", "alert('Cannot delete the first row');", true);
                //}
            }
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "CostCodeUpdateManager", "gvSkillCriteria_RowCommand", EventIDConstants.RAVE_EMP_SKILL_SEARCH_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Gets the skills.
    /// </summary>
    private RaveHRCollection GetProjectCostCodes()
    {
        Rave.HR.BusinessLayer.Employee.Employee objEmployeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        raveHrColl = objEmployeeBL.GetProjectNameForEmpByEmpID(Convert.ToInt16(empId));

        return raveHrColl;
    }



    protected void BindGridView(DataTable dt)
    {
        try
        {
            if (dt.Rows.Count > 0)
            {
                grdvListofPartiallyBilledEmployees.DataSource = dt;
                grdvListofPartiallyBilledEmployees.DataBind();

                //Unlimited Rows can be added
                btnAddRow.Visible = true;

                dt.Clear();
            }
            else
            {
                dt = new DataTable();

                dt.Columns.Add(new DataColumn("ProjectNo", typeof(string)));
                dt.Columns.Add(new DataColumn("ProjectName", typeof(string)));
                dt.Columns.Add(new DataColumn("CostCode", typeof(string)));
                dt.Columns.Add(new DataColumn("Billing", typeof(string)));

                DataRow defualtdr = dt.NewRow();
                defualtdr["ProjectNo"] = 1;
                dt.Rows.Add(defualtdr);

                grdvListofPartiallyBilledEmployees.DataSource = dt;
                grdvListofPartiallyBilledEmployees.DataBind();
            }
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "CostCodeUpdateManager", "BindGridView", EventIDConstants.RAVE_EMP_SKILL_SEARCH_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        try
        {
            if (grdvListofPartiallyBilledEmployees.Rows.Count > 0)
            {
                if (ValidateCostCodeGrid(grdvListofPartiallyBilledEmployees) == false)
                {
                    //if (ddlSkill.SelectedItem.Text == CommonConstants.SELECT)
                    //{
                    //   // lblError.Text = "Please select a Skill.";
                    return;
                }
                else
                {
                    DefaultGridView();
                }
            }
            else
            {
                DefaultGridView();

                System.Web.UI.WebControls.TextBox txtCostCode = (System.Web.UI.WebControls.TextBox)grdvListofPartiallyBilledEmployees.Rows[0].FindControl("txtCostCode");
                System.Web.UI.WebControls.TextBox txtBilling = (System.Web.UI.WebControls.TextBox)grdvListofPartiallyBilledEmployees.Rows[0].FindControl("txtBilling");
                HtmlGenericControl lblmandatorymarkCostCode = (HtmlGenericControl)grdvListofPartiallyBilledEmployees.Rows[0].FindControl("lblmandatorymarkCostCode");
                HtmlGenericControl lblmandatorymarkBilling = (HtmlGenericControl)grdvListofPartiallyBilledEmployees.Rows[0].FindControl("lblmandatorymarkBilling");
                if (Session[SessionNames.EMPLOYEEDETAILS] != null)
                {
                    employee = (BusinessEntities.Employee)Session[SessionNames.EMPLOYEEDETAILS];
                    string values = this.Employee_GetEmployeeCostCodeByEmpIDandNoPrjID(employee.EMPId);
                    if (!string.IsNullOrEmpty(values))
                    {
                        string[] arrValues = values.Split('~');
                        txtCostCode.Text = arrValues[0].ToString().Trim();
                        if (string.IsNullOrEmpty(arrValues[1].ToString().Trim()))
                            txtBilling.Text = "0";
                        else
                            txtBilling.Text = arrValues[1].ToString().Trim();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "CostCodeUpdateManager", "btnAddRow_Click", EventIDConstants.RAVE_EMP_SKILL_SEARCH_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    //Siddharth 7 April 2015 Start


    protected void DefaultGridView()
    {
        try
        {
            dtEmployeeProjectCodtCodes = new DataTable();

            dtEmployeeProjectCodtCodes.Columns.Add(new DataColumn("ProjectNo", typeof(string)));
            dtEmployeeProjectCodtCodes.Columns.Add(new DataColumn("ProjectName", typeof(string)));
            dtEmployeeProjectCodtCodes.Columns.Add(new DataColumn("CostCode", typeof(string)));
            dtEmployeeProjectCodtCodes.Columns.Add(new DataColumn("Billing", typeof(string)));
            int i = 1;

            foreach (GridViewRow gdr in grdvListofPartiallyBilledEmployees.Rows)
            {
                DataRow dr = dtEmployeeProjectCodtCodes.NewRow();
                dr["ProjectNo"] = i;
                //dr["ProjectName"] = ((DropDownList)gdr.FindControl("ddlProject")).SelectedValue;
                //Siddharth 8th May 2015 Start
                dr["ProjectName"] = DBNull.Value;
                //Siddharth 8th May 2015 End
                dr["CostCode"] = ((System.Web.UI.WebControls.TextBox)gdr.FindControl("txtCostCode")).Text;
                dr["Billing"] = ((System.Web.UI.WebControls.TextBox)gdr.FindControl("txtBilling")).Text;
                dtEmployeeProjectCodtCodes.Rows.Add(dr);
                i++;
            }

            DataRow defualtdr = dtEmployeeProjectCodtCodes.NewRow();
            defualtdr["ProjectNo"] = i;
            dtEmployeeProjectCodtCodes.Rows.Add(defualtdr);
            //ViewState["EmployeeProjectCodeCode"] = dtEmployeeProjectCodtCodes;
            BindGridView(dtEmployeeProjectCodtCodes);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "CostCodeUpdateManager", "DefaultGridView", EventIDConstants.RAVE_EMP_SKILL_SEARCH_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    protected bool ValidateCostCodeGrid(GridView gv)
    {
        bool isValid = true;

        //DropDownList ddlSkill = (DropDownList)grdvListofPartiallyBilledEmployees.Rows[(gv.Rows.Count - 1)].FindControl("ddlProject");
        System.Web.UI.WebControls.TextBox txtCostCode = (System.Web.UI.WebControls.TextBox)grdvListofPartiallyBilledEmployees.Rows[(gv.Rows.Count - 1)].FindControl("txtCostCode");
        System.Web.UI.WebControls.TextBox txtBilling = (System.Web.UI.WebControls.TextBox)grdvListofPartiallyBilledEmployees.Rows[(gv.Rows.Count - 1)].FindControl("txtBilling");


        //if (ddlSkill.SelectedItem.Text == CommonConstants.SELECT)
        //{//Project is not selected
        if (String.IsNullOrEmpty(txtCostCode.Text.Trim()) && String.IsNullOrEmpty(txtBilling.Text.Trim()))
        {// Cost Code and Billing is Empty
            //Session["StatusMessage"] = "Please fill all details";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Cost Code Update Manager", "alert('Please fill all details');", true);
            isValid = false;
        }
        else if (!String.IsNullOrEmpty(txtCostCode.Text.Trim()) && String.IsNullOrEmpty(txtBilling.Text.Trim()))
        {//Cost Code is missing
            //Session["StatusMessage"] = "Please fill Billing";
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Cost Code Update Manager", "alert('Please fill all details');", true);
            isValid = false;
        }

        //}
        //else
        //{//Project is selected
        //    if (String.IsNullOrEmpty(txtCostCode.Text.Trim()))
        //    {
        //        //Session["StatusMessage"] = "Project is selected but Cost Code is not entered";
        //        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "Cost Code Update Manager", "alert('Project is selected but Cost Code is not entered');", true);
        //        isValid = false;
        //    }
        //}

        return isValid;
    }
    
    private int CheckDuplicatesRecords(DataTable dt)
    {
        int isDuplicate = 0;
        var UniqueRows = dt.AsEnumerable().Distinct(DataRowComparer.Default);
        DataTable dt2 = UniqueRows.CopyToDataTable();
        isDuplicate = dt.Rows.Count - dt2.Rows.Count;
        return isDuplicate;
    }


    private DataTable GridToDataTable(GridView gv)
    {
        DataTable dtGridToDataTable = new DataTable();

        //dtGridToDataTable.Columns.Add(new DataColumn("ProjectNo", typeof(string)));
        dtGridToDataTable.Columns.Add(new DataColumn("ProjectName", typeof(string)));
        dtGridToDataTable.Columns.Add(new DataColumn("CostCode", typeof(string)));
        dtGridToDataTable.Columns.Add(new DataColumn("Billing", typeof(string)));
        //int i = 1;

        foreach (GridViewRow gdr in gv.Rows)
        {
            DataRow dr = dtGridToDataTable.NewRow();
            // dr["ProjectNo"] = i;
            dr["ProjectName"] = ((DropDownList)gdr.FindControl("ddlProject")).SelectedValue;
            dr["CostCode"] = ((System.Web.UI.WebControls.TextBox)gdr.FindControl("txtCostCode")).Text;
            dr["Billing"] = ((System.Web.UI.WebControls.TextBox)gdr.FindControl("txtBilling")).Text;
            dtGridToDataTable.Rows.Add(dr);
            //i++;
        }
        return dtGridToDataTable;
    }



    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlProject control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlProject = (DropDownList)sender;
            GridViewRow GRow = (GridViewRow)ddlProject.NamingContainer;

            System.Web.UI.WebControls.TextBox txtCostCode = (System.Web.UI.WebControls.TextBox)GRow.FindControl("txtCostCode");
            System.Web.UI.WebControls.TextBox txtBilling = (System.Web.UI.WebControls.TextBox)GRow.FindControl("txtBilling");
            HtmlGenericControl lblmandatorymarkCostCode = (HtmlGenericControl)GRow.FindControl("lblmandatorymarkCostCode");
            HtmlGenericControl lblmandatorymarkBilling = (HtmlGenericControl)GRow.FindControl("lblmandatorymarkBilling");


            if (ddlProject.SelectedItem.Text == CommonConstants.SELECT)
            {
                //txtCostCode.Enabled = false;
                txtCostCode.Text = "";
                lblmandatorymarkCostCode.Visible = false;
                txtBilling.Enabled = true;
                lblmandatorymarkBilling.Visible = true;

                if (Session[SessionNames.EMPLOYEEDETAILS] != null)
                {
                    employee = (BusinessEntities.Employee)Session[SessionNames.EMPLOYEEDETAILS];
                    string values = this.Employee_GetEmployeeCostCodeByEmpIDandNoPrjID(employee.EMPId);
                    if (!string.IsNullOrEmpty(values))
                    {
                        string[] arrValues = values.Split('~');
                        txtCostCode.Text = arrValues[0].ToString().Trim();
                        if (string.IsNullOrEmpty(arrValues[1].ToString().Trim()))
                            txtBilling.Text = "0";
                        else
                            txtBilling.Text = arrValues[1].ToString().Trim();
                    }
                }
            }
            else
            {
                txtCostCode.Enabled = true;
                txtCostCode.Text = "";
                lblmandatorymarkCostCode.Visible = true;
                txtBilling.Enabled = false;
                lblmandatorymarkBilling.Visible = false;
                if (Session[SessionNames.EMPLOYEEDETAILS] != null)
                {
                    employee = (BusinessEntities.Employee)Session[SessionNames.EMPLOYEEDETAILS];

                    string values = this.Employee_GetEmployeeCostCodeByEmpIDandPrjID(employee.EMPId, Convert.ToInt16(ddlProject.SelectedValue.ToString().Trim()));
                    string[] arrValues = values.Split('~');
                    txtCostCode.Text = arrValues[0].ToString().Trim();
                    if (string.IsNullOrEmpty(arrValues[1].ToString().Trim()))
                        txtBilling.Text = "0";
                    else
                        txtBilling.Text = arrValues[1].ToString().Trim();
                }
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlProject_SelectedIndexChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Gets the employee CostCode By EmpID.
    /// </summary>
    /// <param name="employeeObj">The employee obj.</param>
    /// <returns></returns>
    private string Employee_GetEmployeeCostCodeByEmpIDandPrjID(int EmpID, int ProjectID)
    {
        BusinessEntities.Employee empPar = null;
        empPar = new BusinessEntities.Employee();
        BusinessEntities.Employee employee = new BusinessEntities.Employee();
        Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
        string str = string.Empty;
        try
        {
            str = employeeBL.Employee_GetEmployeeCostCodeByEmpIDandPrjID(EmpID, ProjectID);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "CostCodeUpdateManager", "GetEmployeeCostCodeByEmpID", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
        return str;
    }

    //Venkatesh : Start: 03-Jun-2016 : Actual vs Budget
    /// <summary>
    /// Handles the SelectedIndexChanged event of the DdlCostCode control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void DdlCostCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlProject = (DropDownList)sender;
            GridViewRow GRow = (GridViewRow)ddlProject.NamingContainer;

            System.Web.UI.WebControls.TextBox txtCostCode = (System.Web.UI.WebControls.TextBox)GRow.FindControl("txtCostCode");
            System.Web.UI.WebControls.TextBox txtBilling = (System.Web.UI.WebControls.TextBox)GRow.FindControl("txtBilling");
            HtmlGenericControl lblmandatorymarkCostCode = (HtmlGenericControl)GRow.FindControl("lblmandatorymarkCostCode");
            HtmlGenericControl lblmandatorymarkBilling = (HtmlGenericControl)GRow.FindControl("lblmandatorymarkBilling");


            if (ddlProject.SelectedItem.Text == CommonConstants.SELECT)
            {
                //txtCostCode.Enabled = false;
                txtCostCode.Text = "";
                lblmandatorymarkCostCode.Visible = false;
                txtBilling.Enabled = true;
                lblmandatorymarkBilling.Visible = true;

                if (Session[SessionNames.EMPLOYEEDETAILS] != null)
                {
                    employee = (BusinessEntities.Employee)Session[SessionNames.EMPLOYEEDETAILS];
                    string values = this.Employee_GetEmployeeCostCodeByEmpIDandNoPrjID(employee.EMPId);
                    if (!string.IsNullOrEmpty(values))
                    {
                        string[] arrValues = values.Split('~');
                        txtCostCode.Text = arrValues[0].ToString().Trim();
                        if (string.IsNullOrEmpty(arrValues[1].ToString().Trim()))
                            txtBilling.Text = "0";
                        else
                            txtBilling.Text = arrValues[1].ToString().Trim();
                    }
                }
            }
            else
            {
                txtCostCode.Enabled = true;
                txtCostCode.Text = "";
                lblmandatorymarkCostCode.Visible = true;
                txtBilling.Enabled = false;
                lblmandatorymarkBilling.Visible = false;
                if (Session[SessionNames.EMPLOYEEDETAILS] != null)
                {
                    employee = (BusinessEntities.Employee)Session[SessionNames.EMPLOYEEDETAILS];

                    string values = this.Employee_GetEmployeeCostCodeByEmpIDandPrjID(employee.EMPId, Convert.ToInt16(ddlProject.SelectedValue.ToString().Trim()));
                    string[] arrValues = values.Split('~');
                    txtCostCode.Text = arrValues[0].ToString().Trim();
                    if (string.IsNullOrEmpty(arrValues[1].ToString().Trim()))
                        txtBilling.Text = "0";
                    else
                        txtBilling.Text = arrValues[1].ToString().Trim();
                }
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlProject_SelectedIndexChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    //Venkatesh : End: 03-Jun-2016 : Actual vs Budget

    /// <summary>
    /// Gets the employee CostCode By EmpID.
    /// </summary>
    /// <param name="employeeObj">The employee obj.</param>
    /// <returns></returns>
    private string Employee_GetEmployeeCostCodeByEmpIDandNoPrjID(int EmpID)
    {
        BusinessEntities.Employee empPar = null;
        empPar = new BusinessEntities.Employee();
        BusinessEntities.Employee employee = new BusinessEntities.Employee();
        Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
        string str = string.Empty;
        try
        {
            str = employeeBL.Employee_GetEmployeeCostCodeByEmpIDandNoPrjID(EmpID);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "CostCodeUpdateManager", "GetEmployeeCostCodeByEmpID", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
        return str;
    }

    protected void grdvListofEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteCostcode")
        {
            Rave.HR.BusinessLayer.Employee.Employee addEmployeeBAL = new Rave.HR.BusinessLayer.Employee.Employee();
            //int EmpCCId = Convert.ToInt32 (e.CommandArgument.ToString());
            
            string str = string.Empty;
            try
            {
                //Get the identity of Logged on User
                string strUserIdentity = string.Empty;
                strUserIdentity = HttpContext.Current.ApplicationInstance.Session["WindowsUsername"].ToString().Trim();

                GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                HiddenField hfldProjectId = (HiddenField)grv.FindControl("hfldProjectId");
                              

                BusinessEntities.Employee Employee = new BusinessEntities.Employee();
                Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
                int LastModifiedByID = employeeBL.Employee_GetWindowsUsenameofLoggedInUserForCCManager(strUserIdentity);
                addEmployeeBAL.DeleteEmployeeCostcode(ddlEmployeeList.SelectedValue.ToString(), Convert.ToString(hfldProjectId.Value.ToString()),  LastModifiedByID);

                PopulateData();                                                                                                                                         
            }
            catch (RaveHRException ex)
            {
                LogErrorMessage(ex);
            }
            catch (Exception ex)
            {
                RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "CostCodeUpdateManager", "GetEmployeeCostCodeByEmpID", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
                LogErrorMessage(objEx);
            }            
        }
    }
}
