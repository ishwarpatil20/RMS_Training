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
using Common.AuthorizationManager;
using Rave.HR.BusinessLayer;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.DirectoryServices;
using Common.Constants;


public partial class ConsolidatedSummary : BaseClass
{
    #region Members Variables

    BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
    Rave.HR.BusinessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.BusinessLayer.MRF.MRFDetail();
    BusinessEntities.Employee employee = new BusinessEntities.Employee();

    /// <summary>
    /// Default value for DropDown 
    /// </summary>
    private const string SELECT = "Select";

    /// <summary>
    /// Defines Ascending sorting order for grid
    /// </summary>
    private const string ASCENDING = " ASC";

    /// <summary>
    /// Defines Descending sorting order for grid
    /// </summary>
    private const string DESCENDING = " DESC";

    /// <summary>
    /// Defines the command name Previous
    /// </summary>
    private const string PREVIOUS = "Previous";

    /// <summary>
    /// Defines the command name Next
    /// </summary>
    private const string NEXT = "Next";

    /// <summary>
    /// Defines a constant for Page Count
    /// </summary>
    private int pageCount = 0;

    private string ZERO = "0";
    private const string RELEASECOMMAND = "ReleaseImageBtn";
    private const string RELEASEBUTTONID = "imgRelease";
    private const string EMPID = "EmpID";
    //Ishwar Patil 01102014 For NIS : Start
    private const string _RMOGroupName = "RMOGroupName";
    //Ishwar Patil 01102014 For NIS : End

    
    /// <summary>
    /// All the parameters to be passed to SP
    /// </summary>
    BusinessEntities.ParameterCriteria objParameter = new BusinessEntities.ParameterCriteria();

    /// <summary>
    /// Determines the total no. roles for user.
    /// </summary>
    ArrayList rolesForUser = new ArrayList();

    /// <summary>
    /// Get or set the GridViewSortDirection property
    /// </summary>
    private System.Web.UI.WebControls.SortDirection GridViewSortDirection
    {
        get
        {
            if (Session[SessionNames.SORT_DIRECTION_EMP] == null)
                Session[SessionNames.SORT_DIRECTION_EMP] = System.Web.UI.WebControls.SortDirection.Ascending;

            return (System.Web.UI.WebControls.SortDirection)Session[SessionNames.SORT_DIRECTION_EMP];
        }

        set
        {
            Session[SessionNames.SORT_DIRECTION_EMP] = value;
        }
    }

    /// <summary>
    /// Defines default value for sorting expression 
    /// </summary>
    private static string sortExpression = string.Empty;

    /// <summary>
    /// Sets the image direction either upwards or downwards
    /// </summary>
    private string imageDirection = string.Empty;

    /// <summary>
    /// Store the Value of row count
    /// </summary>
    Hashtable hashTable = new Hashtable();

    //27633-Subhra-start
    /// <summary>
    /// To store data for previous and next page for paging issue
    /// </summary>
    Hashtable temppreviousHashTable = new Hashtable();
    BusinessEntities.RaveHRCollection raveHRpreviousCollection = new BusinessEntities.RaveHRCollection();
    private int IntHashpageCount = 0;
    //end-27633

    private const string CLASS_NAME = "ConsolidatedSummary.aspx.cs";

    string UserRaveDomainId;
    string UserMailId;

    /// <summary>
    /// Contains the list of roles
    /// </summary>
    ArrayList arrRolesForUser = new ArrayList();

    #endregion Members Variables

    #region Protected Events

    protected void Page_Load(object sender, EventArgs e)
    {
        //Make filter button as a default button.
        Page.Form.DefaultButton = btnFilter.UniqueID;

        if (!ValidateURL())
        {
            Response.Redirect(CommonConstants.INVALIDURL, false);
        }
        else
        {
            //GVResourcesOnboard.RowCommand += new GridViewCommandEventHandler(GVResourcesOnboard_RowCommand);

            // Get logged-in user's email id
            AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
            UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
            UserMailId = UserRaveDomainId.Replace("co.in", "com");
            AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
            arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(UserRaveDomainId);

            if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEEMPLOYEE) && arrRolesForUser.Count == 1)
            {
                Response.Redirect(CommonConstants.PAGE_HOME, true);
            }



            if (!Page.IsPostBack)
            {
                this.GetRolesforUser();
                this.GetProjectNameNIS();
                ddlProject.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                TROnBoard.Visible = false;
                TRRelease.Visible = false;
                Session[SessionNames.SORT_DIRECTION_EMP] = null;
            }
            sortExpression = "[Resource Type]";
            imageDirection = SortOrder.Ascending.ToString();
            Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] = null;
        }
    }
   
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            //Siddharth 26 march 2015 Start
            //Pass Default Sort Expression
            imageDirection = SortOrder.Ascending.ToString();
            Session[SessionNames.SORT_DIRECTION_EMP] = null;
            this.GetConsolidated("[Resource Type] ASC");
            //Siddharth 26 march 2015 Start
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetConsolidated", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    /// <summary>
    /// Change the page on click of Previous or Next linkbutton and bind grid.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ChangePage(object sender, CommandEventArgs e)
    {

    }

    /// <summary>
    /// Text change of pager textbox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtPages_TextChanged(object sender, EventArgs e)
    {

    }

    #endregion Protected Events

    #region Private Methods

    /// <summary>
    /// Gets the Role for Logged in User
    /// </summary>
    private void GetRolesforUser()
    {
        try
        {
            // Get the roles of user which is stored in the session variable
            rolesForUser = this.GetUserAndRoles();

            //Parse through the array and assign individual role to object
            foreach (string STR in rolesForUser)
            {
                switch (STR)
                {
                    case AuthorizationManagerConstants.ROLERPM:
                        objParameter.Role = AuthorizationManagerConstants.ROLERPM;
                        break;

                    case AuthorizationManagerConstants.ROLEPROJECTMANAGER:
                        objParameter.Role = AuthorizationManagerConstants.ROLEPROJECTMANAGER;
                        break;

                    case AuthorizationManagerConstants.ROLEHR:
                        objParameter.Role = AuthorizationManagerConstants.ROLEHR;
                        break;

                    default:
                        break;
                }
            }

            if ((objParameter.RoleRPM == AuthorizationManagerConstants.ROLERPM) || (objParameter.RoleCEO == AuthorizationManagerConstants.ROLECEO) || (objParameter.RoleCOO == AuthorizationManagerConstants.ROLECOO))
            {
                objParameter.RoleRPM = AuthorizationManagerConstants.ROLERPM;
                objParameter.RoleCEO = AuthorizationManagerConstants.ROLERPM;
                objParameter.RoleCOO = AuthorizationManagerConstants.ROLERPM;
            }

            if ((objParameter.RoleCFM == AuthorizationManagerConstants.ROLECFM) || (objParameter.RoleFM == AuthorizationManagerConstants.ROLEFM))
            {
                objParameter.RoleCFM = AuthorizationManagerConstants.ROLECFM;
                objParameter.RoleFM = AuthorizationManagerConstants.ROLECFM;
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetConsolidated", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    private void GetProjectNameNIS()
    {
        try
        {
            AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
            UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
            UserRaveDomainId = UserRaveDomainId.Replace("co.in", "com");

            //Declaring COllection class object
            BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

            //Declaring Master Class Object
            Rave.HR.BusinessLayer.Employee.Employee employee = new Rave.HR.BusinessLayer.Employee.Employee();

            //Calling Fill dropdown Business layer method to fill 
            //the dropdown from Employee Business class.
            raveHrColl = employee.FillProjectNameNISDropDowns(UserRaveDomainId, objParameter.Role);

            ddlProject.Items.Clear();
            ddlProject.DataSource = raveHrColl;
            ddlProject.DataTextField = Common.CommonConstants.DDL_DataTextField;
            ddlProject.DataValueField = Common.CommonConstants.DDL_DataValueField;
            ddlProject.DataBind();
            //ddlProject.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetConsolidated", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }



    private void GetConsolidated(string SortExpressionAndDirection)
    {
        BusinessEntities.Employee employee = new BusinessEntities.Employee();
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
        Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
        try
        {
            DataSet ds = new DataSet();

            ds = employeeBL.GetConsolidated(Convert.ToInt32(ddlProject.SelectedValue), SortExpressionAndDirection);
            GVResourcesOnboard.DataSource = ds.Tables[0];
            Session["GVResourcesOnboard"] = Convert.ToInt32(ds.Tables[0].Rows.Count);
            GVResourcesOnboard.DataBind();

            GVResourcesReleased.DataSource = ds.Tables[1];
            Session["GVResourcesReleased"] = Convert.ToInt32(ds.Tables[1].Rows.Count);
            GVResourcesReleased.DataBind();
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    btnExport.Visible = false;
                else
                    btnExport.Visible = true;

                if (ds.Tables[0].Rows.Count == 0)
                    TROnBoard.Visible = false;
                else
                    TROnBoard.Visible = true;

                if (ds.Tables[1].Rows.Count == 0)
                    TRRelease.Visible = false;
                else
                    TRRelease.Visible = true;
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetConsolidated", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }  

    private ArrayList GetUserAndRoles()
    {
        ArrayList usersRole = new ArrayList();
        AuthorizationManager athManager = new AuthorizationManager();
        string emailID = athManager.getLoggedInUser();
        usersRole = athManager.getRolesForUser(emailID);

        return usersRole;
    }

    #region Export to excel 1
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            GridView[] gvList = null;
            gvList = new GridView[] { GVResourcesOnboard, GVResourcesReleased };
            ExportAv("Consolidated Report by Project.xls", gvList);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnExport_Click", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    public static void ExportAv(string fileName, GridView[] gvs)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=\"" + fileName + "\"");
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        foreach (GridView gv in gvs)
        {
            gv.AllowPaging = false;

            if (gv.Rows.Count > 0)
            {
                //   Create a form to contain the grid
                Table table = new Table();

                table.GridLines = gv.GridLines;
                //   add the header row to the table
                if (!(gv.Caption == null))
                {
                    TableCell cellCaption = new TableCell();
                    cellCaption.Text = "";

                    if (gv.ID == "GVResourcesOnboard")
                    {
                        cellCaption.ColumnSpan = 7;
                    }
                    else if (gv.ID == "GVResourcesReleased")
                    {
                        cellCaption.ColumnSpan = 8;
                        TableRow trCaption = new TableRow();
                        trCaption.Controls.Add(cellCaption);
                        table.Rows.Add(trCaption);
                    }

                    TableCell cell = new TableCell();
                    if (gv.ID == "GVResourcesOnboard")
                        cell.Text = "Resources On-board";
                    else if (gv.ID == "GVResourcesReleased")
                        cell.Text = "Resources in the past";
                    cell.Font.Bold = true;
                    //cell.HorizontalAlign = HorizontalAlign.Right;
                    cell.Style.Add("text-Decoration", "bold");
                    if (gv.ID == "GVResourcesOnboard")
                        cell.ColumnSpan = 7;
                    else if (gv.ID == "GVResourcesReleased")
                        cell.ColumnSpan = 8;
                    TableRow tr = new TableRow();
                    tr.Controls.Add(cell);
                    table.Rows.Add(tr);
                }

                if (!(gv.HeaderRow == null))
                {
                    table.Rows.Add(gv.HeaderRow);
                }
                //   add each of the data rows to the table
                foreach (GridViewRow row in gv.Rows)
                {
                    row.Cells[0].HorizontalAlign = HorizontalAlign.Center;      //Sr.No.
                    row.Cells[1].HorizontalAlign = HorizontalAlign.Center;      //Emp Code
                    table.Rows.Add(row);
                }
                //   add the footer row to the table
                if (!(gv.FooterRow == null))
                {
                    table.Rows.Add(gv.FooterRow);
                }
                //   render the table into the htmlwriter
                //table.RenderControl("ds");
                table.RenderControl(htw);
            }
        }
        //   render the htmlwriter into the response

        //string headerTable = @"<table width='100%' class='TestCssStyle'><tr><td><h4>Report </h4> </td><td></td><td><h4>" + DateTime.Now.ToString("d") + "</h4></td></tr></table>";
        //HttpContext.Current.Response.Write(headerTable);
        HttpContext.Current.Response.Write(sw.ToString());

        HttpContext.Current.Response.Flush();// Sends all currently buffered output to the client.
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest();// Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
        //HttpContext.Current.Response.End();
    }
    #endregion

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }


    /// <summary>
    /// Sort the gridview as per SortExpression and SortDirection
    /// </summary>
    /// <param name="sortExpression"></param>
    /// <param name="direction"></param>
    
    //Siddharth 26 March 2015 Start
    private void SortGridView(string sortExpression, string direction)
    {
        try
        {
            if (sortExpression == CommonConstants.EMP_FIRSTNAME)
            {
                objParameter.SortExpressionAndDirection = sortExpression + direction;
            }
            else
            {
                objParameter.SortExpressionAndDirection = sortExpression + direction;
            }

            //This will call the bind function and pass sort details
            this.GetConsolidated(objParameter.SortExpressionAndDirection);

            //if (raveHRCollection.Count == 0)
            //{
            //    GVResourcesOnboard.DataSource = raveHRCollection;
            //    GVResourcesOnboard.DataBind();

            //   // ShowHeaderWhenEmptyGrid(raveHRCollection);
            //}
            //else
            //{
            //    GVResourcesOnboard.DataSource = raveHRCollection;
            //    GVResourcesOnboard.DataBind();

            //}
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "SortGridView", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    //Siddharth 26 March 2015 Start




    //Siddharth 30 March 2015 Start


    protected void GVResourcesReleased_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            // GridViewRow gvrPager = GVResourcesOnboard.BottomPagerRow;
            // TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            // txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_EMP].ToString();

            //On sorting assign new sort expression
            sortExpression = e.SortExpression;

            if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] == null)
            {
                Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] = sortExpression;
            }

            if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP].ToString() == sortExpression)
            {
                //Sort to opposite direction based upon previous sort direction
                if (Session[SessionNames.SORT_DIRECTION_EMP] == null || GridViewSortDirection == System.Web.UI.WebControls.SortDirection.Descending)
                {
                    GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Ascending;
                    SortGridView(sortExpression, ASCENDING);
                }
                else
                {
                    GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Descending;
                    SortGridView(sortExpression, DESCENDING);
                }
            }
            else
            {
                GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Ascending;
                SortGridView(sortExpression, ASCENDING);
            }

            if (Session[SessionNames.EMP_PROJECTID] != null || Session[SessionNames.EMP_DEPARTMENTID] != null || Session[SessionNames.EMP_ROLE] != null || Session[SessionNames.EMP_STATUSID] != null || Session[SessionNames.EMP_NAME] != null)
            {
                //Change here 26 March 2015
                //btnRemoveFilter.Visible = true;
            }

            //Set current sort expression as PreviousSortExpression
            Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] = sortExpression;
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetConsolidated", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    protected void GVResourcesOnboard_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            // GridViewRow gvrPager = GVResourcesOnboard.BottomPagerRow;
            // TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            // txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_EMP].ToString();

            //On sorting assign new sort expression
            sortExpression = e.SortExpression;

            if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] == null)
            {
                Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] = sortExpression;
            }

            if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP].ToString() == sortExpression)
            {
                //Sort to opposite direction based upon previous sort direction
                if (Session[SessionNames.SORT_DIRECTION_EMP] == null || GridViewSortDirection == System.Web.UI.WebControls.SortDirection.Descending)
                {
                    GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Ascending;
                    SortGridView(sortExpression, ASCENDING);
                }
                else
                {
                    GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Descending;
                    SortGridView(sortExpression, DESCENDING);
                }
            }
            else
            {
                GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Ascending;
                SortGridView(sortExpression, ASCENDING);
            }

            if (Session[SessionNames.EMP_PROJECTID] != null || Session[SessionNames.EMP_DEPARTMENTID] != null || Session[SessionNames.EMP_ROLE] != null || Session[SessionNames.EMP_STATUSID] != null || Session[SessionNames.EMP_NAME] != null)
            {
                //Change here 26 March 2015
                //btnRemoveFilter.Visible = true;
            }

            //Set current sort expression as PreviousSortExpression
            Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] = sortExpression;
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetConsolidated", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    protected void GVResourcesOnboard_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (((Session["GVResourcesOnboard"] != null) && (int.Parse(Session["GVResourcesOnboard"].ToString()) > 1)))
                {
                    //Add sort Images to Grid View Header
                    AddSortImageforGVResourcesOnboard(e.Row);
                }
            }
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetConsolidated", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }





    protected void GVResourcesReleased_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (((Session["GVResourcesReleased"] != null) && (int.Parse(Session["GVResourcesReleased"].ToString()) > 1)))
                {
                    //Add sort Images to Grid View Header
                    AddSortImageforGVResourcesReleased(e.Row);
                }
            }
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetConsolidated", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Add the Sort Image to gridview header row
    /// </summary>
    /// <param name="headerRow"></param>
    private void AddSortImageforGVResourcesOnboard(GridViewRow headerRow)
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
                    case CommonConstants.EMP_CODE:
                        headerRow.Cells[1].Controls.Add(sortImage);
                        break;
                    case CommonConstants.EMP_NAME:
                        headerRow.Cells[2].Controls.Add(sortImage);
                        break;
                    case CommonConstants.JOB_TITLE_SKILL_CONSOLIDATED_SUMMARY_RPT:
                        headerRow.Cells[3].Controls.Add(sortImage);
                        break;
                    case CommonConstants.SKILLS_CONSOLIDATED_SUMMARY_RPT:
                        headerRow.Cells[4].Controls.Add(sortImage);
                        break;
                    case CommonConstants.PROJECT_START_DATE_SKILL_CONSOLIDATED_SUMMARY_RPT:
                        headerRow.Cells[5].Controls.Add(sortImage);
                        break;
                    case CommonConstants.RESOURCE_TYPE_SKILL_CONSOLIDATED_SUMMARY_RPT:
                        headerRow.Cells[6].Controls.Add(sortImage);
                        break;
                    case CommonConstants.COST_CODE_SKILL_CONSOLIDATED_SUMMARY_RPT:
                        headerRow.Cells[7].Controls.Add(sortImage);
                        break;   
                }
            }
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetConsolidated", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    private void AddSortImageforGVResourcesReleased(GridViewRow headerRow)
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
                    case CommonConstants.EMP_CODE:
                        headerRow.Cells[1].Controls.Add(sortImage);
                        break;
                    case CommonConstants.EMP_NAME:
                        headerRow.Cells[2].Controls.Add(sortImage);
                        break;
                    case CommonConstants.JOB_TITLE_SKILL_CONSOLIDATED_SUMMARY_RPT:
                        headerRow.Cells[3].Controls.Add(sortImage);
                        break;
                    case CommonConstants.SKILLS_CONSOLIDATED_SUMMARY_RPT:
                        headerRow.Cells[4].Controls.Add(sortImage);
                        break;
                    case CommonConstants.PROJECT_START_DATE_SKILL_CONSOLIDATED_SUMMARY_RPT:
                        headerRow.Cells[5].Controls.Add(sortImage);
                        break;
                    case CommonConstants.PROJECT_END_DATE_SKILL_CONSOLIDATED_SUMMARY_RPT:
                        headerRow.Cells[6].Controls.Add(sortImage);
                        break;
                    case CommonConstants.RESOURCE_TYPE_SKILL_CONSOLIDATED_SUMMARY_RPT:
                        headerRow.Cells[7].Controls.Add(sortImage);
                        break;
                }
            }
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetConsolidated", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
       
    }

    //Siddharth 30 March 2015 End


    #endregion Private Methods

}
