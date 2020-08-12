//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           ProejctSummary.aspx.cs       
//  Author:         vineet.kulkarni 
//  Date written:   4/3/2009/ 10:58:30 AM
//  Description:    The Project Summary page summarises project details and Contract Details. 
//                  User can View, Update project details navigating from here.
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  4/3/2009 10:58:30 AM  vineet.kulkarni    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Reflection;
using Common;
using Common.AuthorizationManager;

public partial class ProjectSummary : BaseClass
{
    #region Private Field Members

    /// <summary>
    /// Defines Ascending sorting order for grid
    /// </summary>
    private const string ASCENDING = " ASC";

    /// <summary>
    /// Defines Descending sorting order for grid
    /// </summary>
    private const string DESCENDING = " DESC";

    /// <summary>
    /// Defines default value for sorting expression 
    /// </summary>
    private static string sortExpression = string.Empty;

    /// <summary>
    /// Defines a constant error for no records found after applying filter criteria
    /// </summary>
    private const string NO_RECORDS_FOUND_MESSAGE = "No records found.";

    /// <summary>
    /// Defines a constant for default value of drop down
    /// </summary>
    private const string SELECT = "Select";

    /// <summary>
    /// Defines a constant for Page Name used in each catch block 
    /// </summary>
    private const string CLASS_NAME = "ProjectSummary.aspx";

    /// <summary>
    /// Defines an ArrayList for Roles
    /// </summary>
    ArrayList arrRolesForUser = new ArrayList();

    /// <summary>
    /// Defines a constant for Page Size
    /// </summary>
    private const int PAGE_SIZE = 10;

    /// <summary>
    /// Defines a constant for Page Count
    /// </summary>
    private int pageCount = 0;

    BusinessEntities.ProjectCriteria objProjectCriteria = new BusinessEntities.ProjectCriteria();

    /// <summary>
    /// Defines a sessionPageCount
    /// </summary>
    private const string sessionPageCount = "pageCount";

    /// <summary>
    /// Defines the command name Previous
    /// </summary>
    private const string PREVIOUS = "Previous";

    /// <summary>
    /// Defines the command name Next
    /// </summary>
    private const string NEXT = "Next";

    /// <summary>
    /// Defines an ArrayList for ProjectId
    /// </summary>
    ArrayList arrProjectId = new ArrayList();

    #endregion Private Field Members

    #region Public Field Members

    /// <summary>
    /// Defines enum for Mode
    /// </summary>
    public enum Mode
    {
        View
    }

    /// <summary>
    /// Defines enum for Project Status
    /// </summary>
    public enum ProjectStatus
    {
        Closed,
        Deleted
    }

    /// <summary>
    /// Defines Logged in User Mail Id
    /// </summary>
    public string UserMailId;

    /// <summary>
    /// Defines Generic List for Proejct Summary Data
    /// </summary>
    List<BusinessEntities.Projects> objListSort = new List<BusinessEntities.Projects>();

    #endregion Public Field Members

    #region Protected Events

    /// <summary>
    /// page load event handler
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Make filter button as a default button.
            Page.Form.DefaultButton = btnFilter.UniqueID;

            if (!ValidateURL())
            {
                Response.Redirect(CommonConstants.INVALIDURL);
            }
            else
            {

                //Siddharth 26th August 2015 Start
                //Task ID:- 56487 Hide the pages access for normal employees
                AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
                arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(RaveHRAuthorizationManager.getLoggedInUser());

                if (!(arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM) ||
                     arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEPM) ||
                     arrRolesForUser.Contains(AuthorizationManagerConstants.ROLECOO)))
                {
                    Response.Redirect(CommonConstants.PAGE_HOME, true);
                }
                //Siddharth 26th August 2015 End



                //Make Error Message label invisible for each page load event trigger
                lblMessage.Visible = false;

                if (Session[SessionNames.CONFIRMATION_MESSAGE] != null)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = Session[SessionNames.CONFIRMATION_MESSAGE].ToString();
                    Session[SessionNames.CONFIRMATION_MESSAGE] = null;
                }
                if (Session[SessionNames.EMAIL_MESSAGE] != null)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = Session[SessionNames.EMAIL_MESSAGE].ToString();
                    Session[SessionNames.EMAIL_MESSAGE] = null;
                }

                //Get the currently logged-in user
                AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
                arrRolesForUser = objRaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());
                objProjectCriteria.UserMailId = objRaveHRAuthorizationManager.getLoggedInUser();
                objProjectCriteria.UserMailId = objProjectCriteria.UserMailId.Replace("co.in", "com");

                foreach (string STR in arrRolesForUser)
                {
                    switch (STR)
                    {
                        case AuthorizationManagerConstants.ROLECOO:
                            objProjectCriteria.RoleCOO = AuthorizationManagerConstants.ROLECOO;
                            break;

                        case AuthorizationManagerConstants.ROLEPRESALES:
                            objProjectCriteria.RolePresales = AuthorizationManagerConstants.ROLEPRESALES;
                            break;

                        case AuthorizationManagerConstants.ROLEPROJECTMANAGER:
                            objProjectCriteria.RolePM = AuthorizationManagerConstants.ROLEPROJECTMANAGER;
                            break;

                        case AuthorizationManagerConstants.ROLERPM:
                            objProjectCriteria.RoleRPM = AuthorizationManagerConstants.ROLERPM;
                            break;

                        default:
                            break;
                    }
                }

                if (!IsPostBack)
                {
                    if (Session[SessionNames.CURRENT_PAGE_INDEX_PROJECT] == null)

                        Session[SessionNames.CURRENT_PAGE_INDEX_PROJECT] = 1;

                    //check the session for sort expression previously Set otherwise set default sort expresion
                    if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION] == null)
                    {
                        sortExpression = Common.Constants.DbTableColumn.ProjectCode;
                        Session[SessionNames.PREVIOUS_SORT_EXPRESSION] = sortExpression;
                    }
                    else
                    {
                        sortExpression = Session[SessionNames.PREVIOUS_SORT_EXPRESSION].ToString();
                    }

                    FillClientDropDown();

                    //Fill Project Status Dropdown and Show <tr> of Project Status only to PM, COO and RPM.
                    if (objProjectCriteria.RolePM == AuthorizationManagerConstants.ROLEPROJECTMANAGER || objProjectCriteria.RoleRPM == AuthorizationManagerConstants.ROLERPM || objProjectCriteria.RoleCOO == AuthorizationManagerConstants.ROLECOO)
                    {
                        FillStatusDropDown();
                        trlblStatus.Visible = true;
                        trddlStatus.Visible = true;
                    }
                    else
                    {
                        trlblStatus.Visible = false;
                        trddlStatus.Visible = false;
                    }

                    //Bind the grid while Loading the page
                    BindGrid();

                    //Set the session values to respective fields
                    if (Session[SessionNames.PROJECT_SUMMARY_PROJECT_NAME] != null)
                    {
                        txtProjectName.Text = Session[SessionNames.PROJECT_SUMMARY_PROJECT_NAME].ToString();
                    }
                    if (Session[SessionNames.CLIENT_NAME] != null)
                    {
                        ddlClientName.SelectedValue = Session[SessionNames.CLIENT_NAME].ToString();
                    }
                    if (Session[SessionNames.PROJECT_STATUS] != null)
                    {
                        if ((objProjectCriteria.RolePM == AuthorizationManagerConstants.ROLEPROJECTMANAGER) || (objProjectCriteria.RoleRPM == AuthorizationManagerConstants.ROLERPM) || (objProjectCriteria.RoleCOO == AuthorizationManagerConstants.ROLECOO) || (objProjectCriteria.RolePresales == AuthorizationManagerConstants.ROLEPRESALES))
                        {
                            ddlStatus.SelectedValue = Session[SessionNames.PROJECT_STATUS].ToString();
                        }
                    }
                    if (Session[SessionNames.PROJECT_SUMMARY_PROJECT_NAME] != null || Session[SessionNames.CLIENT_NAME] != null || Session[SessionNames.PROJECT_STATUS] != null)
                    {
                        btnRemoveFilter.Visible = true;
                    }

                }
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "Page_Load", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// gridview pageIndexChanging event handler
    /// </summary>
    protected void grdvListofProjects_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //Change the page index on page index changing event and bind the grid
            if (e.NewPageIndex != -1)
            {
                grdvListofProjects.PageIndex = e.NewPageIndex;
            }

            BindGrid();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvListofProjects_PageIndexChanging", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// gridview rowCreated event handler
    /// </summary>
    protected void grdvListofProjects_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (((Session["pagecount"] != null) && (int.Parse(Session["pagecount"].ToString()) > 1)) || ((objListSort.Count > 1)))
                {
                    //Add sort Images to Grid View Header
                    AddSortImage(e.Row);
                }
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvListofProjects_RowCreated", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// gridvtew sorting event handler
    /// </summary>
    protected void grdvListofProjects_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            GridViewRow gvrPager = grdvListofProjects.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_PROJECT].ToString();

            //On sorting assign new sort expression
            sortExpression = e.SortExpression;

            if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION] == null)
            {
                Session[SessionNames.PREVIOUS_SORT_EXPRESSION] = sortExpression;
            }

            if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION].ToString() == sortExpression)
            {
                //Sort to opposite direction based upon previous sort direction
                if (Session[SessionNames.SORT_DIRECTION] == null || GridViewSortDirection == SortDirection.Descending)
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
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, ASCENDING);
            }

            if (Session[SessionNames.PROJECT_SUMMARY_PROJECT_NAME] != null || Session[SessionNames.CLIENT_NAME] != null || Session[SessionNames.PROJECT_STATUS] != null)
            {
                btnRemoveFilter.Visible = true;
            }

            //Set current sort expression as PreviousSortExpression
            Session[SessionNames.PREVIOUS_SORT_EXPRESSION] = sortExpression;
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvListofProjects_Sorting", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// btnFilter_Click event handler
    /// </summary>
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            //Set values of filtering criterias to session
            Session[SessionNames.PROJECT_SUMMARY_PROJECT_NAME] = txtProjectName.Text.Trim();
            Session[SessionNames.CLIENT_NAME] = ddlClientName.SelectedItem.Value;

            if (objProjectCriteria.RolePM == AuthorizationManagerConstants.ROLEPROJECTMANAGER || objProjectCriteria.RoleRPM == AuthorizationManagerConstants.ROLERPM || objProjectCriteria.RoleCOO == AuthorizationManagerConstants.ROLECOO)
            {
                Session[SessionNames.PROJECT_STATUS] = ddlStatus.SelectedValue;
            }
            else
            {
                Session[SessionNames.PROJECT_STATUS] = null;
            }

            if (grdvListofProjects.BottomPagerRow != null)
            {
                GridViewRow gvrPager = grdvListofProjects.BottomPagerRow;
                TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");


                if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0)
                {
                    txtPages.Text = 1.ToString();
                    Session[SessionNames.CURRENT_PAGE_INDEX_PROJECT] = txtPages.Text;
                }
            }

            BindGrid();

            if (Session[SessionNames.PROJECT_SUMMARY_PROJECT_NAME] != null || Session[SessionNames.CLIENT_NAME] != null || Session[SessionNames.PROJECT_STATUS] != null)
            {
                btnRemoveFilter.Visible = true;
            }
            
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

    /// <summary>
    /// txtPages_TextChanged event handler
    /// </summary>
    protected void txtPages_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txtPages = (TextBox)sender;

            //Check for Paging text box is not empty, non-zero, and out of range paging no.
            if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0 && Convert.ToInt32(txtPages.Text) <= Convert.ToInt32(Session[sessionPageCount].ToString()))
            {
                grdvListofProjects.PageIndex = Convert.ToInt32(txtPages.Text) - 1;
                Session[SessionNames.CURRENT_PAGE_INDEX_PROJECT] = txtPages.Text;
            }
            else
            {
                txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_PROJECT].ToString();
                return;
            }

            //Bind the grid on paging
            BindGrid();
            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_PROJECT].ToString();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "txtPages_TextChanged", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// grdview rowDataBound event handler
    /// </summary>
    protected void grdvListofProjects_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //User clicks on the projects and redirects to view mode with project details   

            string clientName = "Select";
            if (Session[SessionNames.CLIENT_NAME] != null)
                clientName = Session[SessionNames.CLIENT_NAME].ToString();

            string projectname = "";
            if (Session[SessionNames.PROJECT_SUMMARY_PROJECT_NAME] != null)
                projectname = Session[SessionNames.PROJECT_SUMMARY_PROJECT_NAME].ToString();

            int projectStatusId = 0;
            if ((Session[SessionNames.PROJECT_STATUS] == null) || (Session[SessionNames.PROJECT_STATUS].ToString() == "Select"))
            {
                projectStatusId = 0;
            }
            else
            {
                projectStatusId = Convert.ToInt32(Session[SessionNames.PROJECT_STATUS]);
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Name: Sanju:Issue Id 50201  Changed cursor property to pointer so that it should display hand in IE10,9,Chrome and mozilla browser.
                // Mohamed : NIS-RMS : 07/01/2015 : Starts                        			  
                // Desc : Remove underline and click from remaining columns

                e.Row.Cells[1].Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                e.Row.Cells[1].Attributes["onmouseout"] = "this.style.textDecoration='none';";

                if (GridViewSortDirection == SortDirection.Ascending)
                {
                    e.Row.Cells[1].Attributes.Add("onclick", "sendWindow('AddProject.aspx?" + URLHelper.SecureParameters("ProjectID", DataBinder.Eval(e.Row.DataItem, "ProjectID").ToString()) + "&" + URLHelper.SecureParameters("Mode", Mode.View.ToString()) + "&" + URLHelper.SecureParameters("sortExpression", sortExpression) + "&" + URLHelper.SecureParameters("sortDirection", "ASC") + "&" + URLHelper.SecureParameters("clientname", clientName) + "&" + URLHelper.SecureParameters("projectstatusid", projectStatusId.ToString()) + "&" + URLHelper.SecureParameters("projectsummaryprojectname", projectname) + "&" + URLHelper.CreateSignature(DataBinder.Eval(e.Row.DataItem, "ProjectID").ToString(), Mode.View.ToString(), sortExpression, "ASC", clientName, projectStatusId.ToString(), projectname) + "')");
                }
                else
                {
                    e.Row.Cells[1].Attributes.Add("onclick", "sendWindow('AddProject.aspx?" + URLHelper.SecureParameters("ProjectID", DataBinder.Eval(e.Row.DataItem, "ProjectID").ToString()) + "&" + URLHelper.SecureParameters("Mode", Mode.View.ToString()) + "&" + URLHelper.SecureParameters("sortExpression", sortExpression) + "&" + URLHelper.SecureParameters("sortDirection", "DESC") + "&" + URLHelper.SecureParameters("clientname", clientName) + "&" + URLHelper.SecureParameters("projectstatusid", projectStatusId.ToString()) + "&" + URLHelper.SecureParameters("projectsummaryprojectname", projectname) + "&" + URLHelper.CreateSignature(DataBinder.Eval(e.Row.DataItem, "ProjectID").ToString(), Mode.View.ToString(), sortExpression, "DESC", clientName, projectStatusId.ToString(), projectname) + "')");
                }

                //--Add style to expand image
                e.Row.Cells[0].Attributes["onmouseover"] = "this.style.cursor='';";
                e.Row.Cells[0].Attributes.Add("onClick", "return buClick();");
                // Mohamed : NIS-RMS : 07/01/2015 : Ends
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvListofProjects_RowDataBound", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// grdvListofProjects_DataBound event handler
    /// </summary>
    protected void grdvListofProjects_DataBound(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrPager = grdvListofProjects.BottomPagerRow;

            if (gvrPager == null) return;

            //Get Text Box and Lable from the Grid View
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            Label lblPageCount = (Label)gvrPager.Cells[0].FindControl("lblPageCount");

            //Assign current page index to text box
            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_PROJECT].ToString();

            //Assign total no of pages to label
            if (lblPageCount != null)
                lblPageCount.Text = pageCount.ToString();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvListofProjects_DataBound", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// btnRemoveFilter_Click event handler
    /// </summary>
    protected void btnRemoveFilter_Click(object sender, EventArgs e)
    {
        try
        {
            //clear all the session value
            Session[SessionNames.PROJECT_SUMMARY_PROJECT_NAME] = null;
            Session[SessionNames.CLIENT_NAME] = null;
            Session[SessionNames.PROJECT_STATUS] = null;
            Session[SessionNames.SORT_DIRECTION] = null;
            Session[SessionNames.CURRENT_PAGE_INDEX_PROJECT] = 1;

            //Assign default sortExpression on Project ID
            sortExpression = Common.Constants.DbTableColumn.ProjectCode;
            Session[SessionNames.PREVIOUS_SORT_EXPRESSION] = sortExpression;


            //On Remove Filter clear filtering fields, Bind the grid and hide Remove Filter Button
            ClearFilteringFields();
            BindGrid();
            btnRemoveFilter.Visible = false;
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnRemoveFilter_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// grdvListofProjects_RowCommand event handler
    /// </summary>
    protected void grdvListofProjects_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //--Add sort image in header
            if ((int.Parse(Session[SessionNames.PageCount].ToString()) == 1) && (grdvListofProjects.Rows.Count > PAGE_SIZE))
            {
                AddSortImage(grdvListofProjects.HeaderRow);
            }

            if (e.CommandName == "ChildGridContractsForProject")
            {
                GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                ImageButton imgbtnExpandCollaspeChildGrid = (ImageButton)grv.FindControl("imgbtnExpandCollaspeChildGrid");
                GridView grdContractGrid = (GridView)grv.FindControl("grdContractGrid");
                HtmlTableRow tr_ContractGrid = (HtmlTableRow)grv.FindControl("tr_ContractGrid");

                //--Collaspe
                if ((imgbtnExpandCollaspeChildGrid != null) && (tr_ContractGrid != null))
                {
                    if (imgbtnExpandCollaspeChildGrid.ImageUrl == Common.CommonConstants.IMAGE_MINUSSIGNPATH)
                    {
                        tr_ContractGrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        imgbtnExpandCollaspeChildGrid.ImageUrl = Common.CommonConstants.IMAGE_PLUSSIGNPATH;
                        return;
                    }
                }

                //--Set entity
                BusinessEntities.ContractProject objBEGetContractsForProject = new BusinessEntities.ContractProject();
                objBEGetContractsForProject.ProjectID = int.Parse(e.CommandArgument.ToString());

                //--Get contracts for project                
                Rave.HR.BusinessLayer.Contracts.ContractProject objBLLGetContractsForProject = new Rave.HR.BusinessLayer.Contracts.ContractProject();
                List<BusinessEntities.ContractProject> objListGetContractsForProject = new List<BusinessEntities.ContractProject>();

                objListGetContractsForProject = objBLLGetContractsForProject.GetContractsForProject(objBEGetContractsForProject);

                //--Collaspe the child grids
                foreach (GridViewRow grvRow in grdvListofProjects.Rows)
                {
                    //create imagebutton and assign imagebutton value as "+" icon of gridviewrow.
                    ImageButton imgbtn = (ImageButton)grvRow.FindControl("imgbtnExpandCollaspeChildGrid");

                    //create htmltablerow and assign child resource plan row value to it.
                    HtmlTableRow tr_RP = (HtmlTableRow)grvRow.FindControl("tr_ContractGrid");

                    //display none when user clicks "-" icon
                    tr_RP.Style.Add(HtmlTextWriterStyle.Display, "none");

                    //replace "-" icon with "+" when user clicks on collapse("-")image button.
                    imgbtn.ImageUrl = Common.CommonConstants.IMAGE_PLUSSIGNPATH;

                }

                //--Display child grid
                if (grdContractGrid != null)
                {
                    //--Bind
                    grdContractGrid.DataSource = objListGetContractsForProject;
                    grdContractGrid.DataBind();
                }

                //--Expand child grid
                if ((imgbtnExpandCollaspeChildGrid != null) && (tr_ContractGrid != null))
                {
                    imgbtnExpandCollaspeChildGrid.ImageUrl = Common.CommonConstants.IMAGE_MINUSSIGNPATH;
                  //Name: Sanju:Issue Id 50201  Removed display property so that it should display grid properly in IE10,Chrome and mozilla browser.
                  //  tr_ContractGrid.Style.Add(HtmlTextWriterStyle.Display, "block");
                    tr_ContractGrid.Style.Add(HtmlTextWriterStyle.Display, "");
                    imgbtnExpandCollaspeChildGrid.ToolTip = "Collapse";
                }
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvListofProjects_RowCommand", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    #endregion Protected Events

    #region Private Member Functions

    /// <summary>
    /// Clears filtering criteria fields
    /// </summary>
    private void ClearFilteringFields()
    {
        try
        {
            txtProjectName.Text = string.Empty;
            // 27631-Subhra-Start 
            // Fill Client status drop down box for Delivery & Presales type Project only
            FillClientDropDown();
            // 27631-Subhra-End 
            ddlClientName.SelectedIndex = 0;

            if ((objProjectCriteria.RolePM == AuthorizationManagerConstants.ROLEPROJECTMANAGER) || (objProjectCriteria.RoleRPM == AuthorizationManagerConstants.ROLERPM) || (objProjectCriteria.RoleCOO == AuthorizationManagerConstants.ROLECOO))
            {
                ddlStatus.SelectedIndex = 0;
            }

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ClearFilteringFields", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Binds grid in sorted order in given direction
    /// </summary>
    private void BindGrid()
    {
        try
        {
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                SortGridView(sortExpression, ASCENDING);
            }
            else
            {
                SortGridView(sortExpression, DESCENDING);
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "BindGrid", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Private Property to Get and Set direction for for sorting
    /// </summary>
    private SortDirection GridViewSortDirection
    {
        get
        {
            if (Session[SessionNames.SORT_DIRECTION] == null)
                Session[SessionNames.SORT_DIRECTION] = SortDirection.Ascending;

            return (SortDirection)Session[SessionNames.SORT_DIRECTION];
        }
        set
        {
            Session[SessionNames.SORT_DIRECTION] = value;
        }
    }

    /// <summary>
    /// Sorts grid view
    /// </summary>
    /// <param name="sortExpression">Sort expression</param>
    /// <param name="direction">Sorts in Ascending or Descending order</param>
    private void SortGridView(string sortExpression, string direction)
    {
        try
        {
            if (sortExpression == Common.Constants.DbTableColumn.ProjectCode)
            {
                objProjectCriteria.SortExpressionAndDirection = sortExpression + direction;
            }
            else
            {
                objProjectCriteria.SortExpressionAndDirection = sortExpression + direction;
            }

            objListSort = GetProjectSummaryData(false);

            if ((int.Parse(Session[sessionPageCount].ToString()) == 1) && (objListSort.Count == 1))
            {
                grdvListofProjects.AllowSorting = false;
            }
            else
            {
                grdvListofProjects.AllowSorting = true;
            }

            if (objListSort.Count == 0)
            {
                grdvListofProjects.DataSource = objListSort;
                grdvListofProjects.DataBind();

                ShowHeaderWhenEmptyGrid(objListSort);
            }
            else
            {
                //Bind the Grid View in Sorted order
                grdvListofProjects.DataSource = objListSort;
                grdvListofProjects.DataBind();

                //Get total projects 
                objListSort = new List<BusinessEntities.Projects>();
                objListSort = GetProjectSummaryData(true);

                //loops through objListSort list
                foreach (BusinessEntities.Projects objList in objListSort)
                {
                    arrProjectId.Add(objList.ProjectId);
                }

                //assign array value to session
                Session[SessionNames.PROJECTSUMMARY_PROJECT_ID] = arrProjectId;
            }

            //Finds the Paging Text Box, Previous and Next Link Buttons in Grid Views Pagning Templet
            GridViewRow gvrPager = grdvListofProjects.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            LinkButton lbtnPrevious = (LinkButton)gvrPager.Cells[0].FindControl("lbtnPrevious");
            LinkButton lbtnNext = (LinkButton)gvrPager.Cells[0].FindControl("lbtnNext");

            if (pageCount > 1)
            {
                grdvListofProjects.BottomPagerRow.Visible = true;
            }

            //Don't allow any character other than Number in paging text box
            txtPages.Attributes.Add("onkeypress", "return isNumberKey(event)");

            //Enable Prvious and Disable Next when Paging Text box contains last paging number
            if (Convert.ToInt32(txtPages.Text) == pageCount)
            {
                lbtnPrevious.Enabled = true;
                lbtnNext.Enabled = false;
            }
            //Enable Next and Disable Previous when Paging Text box contains First paging number i.e. 1
            if (Convert.ToInt32(txtPages.Text) == 1)
            {
                lbtnPrevious.Enabled = false;
                lbtnNext.Enabled = true;
            }
            //Enable both Next and Previous when Paging Text box contains paging number between 1 and Last page number
            if ((Convert.ToInt32(txtPages.Text) > 1) && (Convert.ToInt32(txtPages.Text) < pageCount))
            {
                lbtnPrevious.Enabled = true;
                lbtnNext.Enabled = true; ;
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "SortGridView", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Display Header When GridView Empty with proper message
    /// </summary>
    /// <param name="objListSort">EmptyList</param>
    private void ShowHeaderWhenEmptyGrid(List<BusinessEntities.Projects> objListSort)
    {
        try
        {
            //set header visible
            grdvListofProjects.ShowHeader = true;
            grdvListofProjects.AllowSorting = false;

            //Create empty datasource for Grid view and bind
            objListSort.Add(new BusinessEntities.Projects());
            grdvListofProjects.DataSource = objListSort;
            grdvListofProjects.DataBind();

            //Calculate number of columns in Grid view used for column Span
            int columnsCount = grdvListofProjects.Columns.Count;

            //clear all the cells in the row
            grdvListofProjects.Rows[0].Cells.Clear();

            //add a new blank cell
            grdvListofProjects.Rows[0].Cells.Add(new TableCell());
            grdvListofProjects.Rows[0].Cells[0].Text = NO_RECORDS_FOUND_MESSAGE;
            grdvListofProjects.Rows[0].Cells[0].Wrap = false;
            grdvListofProjects.Rows[0].Cells[0].Width = Unit.Percentage(10);
            grdvListofProjects.Rows[0].Attributes.Remove("onclick");
            grdvListofProjects.Rows[0].Attributes.Remove("onmouseover");
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ShowHeaderWhenEmptyGrid", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Fills Client drop down
    /// </summary>
    private void FillClientDropDown()
    {
        List<BusinessEntities.Projects> objRaveHRProjects = new List<BusinessEntities.Projects>();
        Rave.HR.BusinessLayer.Projects.Projects objRaveHRProjctsBAL = new Rave.HR.BusinessLayer.Projects.Projects();

        try
        {
            objRaveHRProjects = objRaveHRProjctsBAL.GetClientName(objProjectCriteria);
            ddlClientName.DataSource = objRaveHRProjects;
            ddlClientName.DataTextField = "ClientName";
            ddlClientName.DataValueField = "ClientId";
            ddlClientName.DataBind();
            ddlClientName.Items.Insert(0, SELECT);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "FillClientDropDown", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Fills Status drop down
    /// </summary>
    private void FillStatusDropDown()
    {
        List<BusinessEntities.Master> objRaveHRMaster = new List<BusinessEntities.Master>();
        Rave.HR.BusinessLayer.Common.Master objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();

        try
        {
            objRaveHRMaster = objRaveHRMasterBAL.GetMasterData(Convert.ToInt32(EnumsConstants.Category.ProjectStatus).ToString());
            ddlStatus.DataSource = objRaveHRMaster;
            ddlStatus.DataTextField = "MasterName";
            ddlStatus.DataValueField = "MasterID";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, SELECT);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "FillStatusDropDown", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Returns Project Summary data for GridView
    /// </summary>
    /// <returns>List</returns>
    private List<BusinessEntities.Projects> GetProjectSummaryData(bool setPageing)
    {
        if (grdvListofProjects.BottomPagerRow != null)
        {
            GridViewRow gvrPager = grdvListofProjects.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0 && Convert.ToInt32(txtPages.Text) <= Convert.ToInt32(Session[sessionPageCount].ToString()))
            {
                objProjectCriteria.PageNumber = int.Parse(txtPages.Text);
            }
        }
        else
        {
            objProjectCriteria.PageNumber = Convert.ToInt32(Session[SessionNames.CURRENT_PAGE_INDEX_PROJECT].ToString());
        }

        //checks if PageNumber=0 then assign 1 to it
        if (objProjectCriteria.PageNumber == 0)
        {
            objProjectCriteria.PageNumber = 1;
        }

        Rave.HR.BusinessLayer.Projects.Projects objProjectSummaryBAL = new Rave.HR.BusinessLayer.Projects.Projects();

        BusinessEntities.Master objGetProjectStatus = new BusinessEntities.Master();
        BusinessEntities.Projects objGetProjectSummary = new BusinessEntities.Projects();

        List<BusinessEntities.Projects> objListProjectSummary = new List<BusinessEntities.Projects>();

        try
        {
            if (Session[SessionNames.PROJECT_SUMMARY_PROJECT_NAME] == null || Session[SessionNames.PROJECT_SUMMARY_PROJECT_NAME].ToString() == string.Empty)
            {
                objGetProjectSummary.ProjectName = null;
            }
            else
            {
                objGetProjectSummary.ProjectName = Session[SessionNames.PROJECT_SUMMARY_PROJECT_NAME].ToString();
            }
            if (Session[SessionNames.CLIENT_NAME] == null || Session[SessionNames.CLIENT_NAME].ToString() == SELECT)
            {
                objGetProjectSummary.ClientName = null;
            }
            else
            {
                objGetProjectSummary.ClientName = Session[SessionNames.CLIENT_NAME].ToString();
            }
            if ((objProjectCriteria.RolePM == AuthorizationManagerConstants.ROLEPROJECTMANAGER) || (objProjectCriteria.RoleRPM == AuthorizationManagerConstants.ROLERPM) || (objProjectCriteria.RoleCOO == AuthorizationManagerConstants.ROLECOO))
            {
                if (Session[SessionNames.PROJECT_STATUS] == null || Session[SessionNames.PROJECT_STATUS].ToString() == SELECT)
                {
                    objGetProjectStatus.MasterId = 0;
                }
                else
                {
                    objGetProjectStatus.MasterId = Convert.ToInt32(Session[SessionNames.PROJECT_STATUS].ToString());
                }
            }
            else
            {
                objGetProjectStatus.MasterId = 0;
            }

            //If any of the session is null or Contains text "Select" get the page load data

            if ((Session[SessionNames.PROJECT_SUMMARY_PROJECT_NAME] == null || Session[SessionNames.PROJECT_SUMMARY_PROJECT_NAME].ToString() == string.Empty) && (Session[SessionNames.CLIENT_NAME] == null || Session[SessionNames.CLIENT_NAME].ToString() == SELECT) && (Session[SessionNames.PROJECT_STATUS] == null || Session[SessionNames.PROJECT_STATUS].ToString() == SELECT))
            {
                objListProjectSummary = objProjectSummaryBAL.GetProjectSummaryForPageLoad(objProjectCriteria, PAGE_SIZE, ref pageCount, setPageing);
            }
            //Else get the filtered data based upon filter critera selection
            else
            {
                objListProjectSummary = objProjectSummaryBAL.GetProjectSummaryForFilter(objGetProjectSummary, objGetProjectStatus, objProjectCriteria, PAGE_SIZE, ref pageCount, setPageing);
            }
            Session[sessionPageCount] = pageCount;

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetDataProjectSummaryApplyFilter", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
        return objListProjectSummary;
    }

    /// <summary>
    /// method to Add SortImage 
    /// </summary>
    private void AddSortImage(GridViewRow headerRow)
    {
        try
        {
            string _sortDirection = GridViewSortDirection.ToString();

            if (!_sortDirection.Equals(String.Empty))
            {
                // Create the sorting image based on the sort direction
                Image sortImage = new Image();

                if (_sortDirection == SortOrder.Ascending.ToString())
                {
                    sortImage.ImageUrl = "Images/arrow_up.gif";
                    sortImage.AlternateText = "Ascending";
                }
                else if (_sortDirection == SortOrder.Descending.ToString())
                {
                    sortImage.ImageUrl = "Images/arrow_down.gif";
                    sortImage.AlternateText = "Descending";
                }

                // Add the image to the appropriate header cell
                switch (sortExpression)
                {
                    case "ProjectCode":
                        headerRow.Cells[1].Controls.Add(sortImage);
                        break;
                    case "ClientName":
                        headerRow.Cells[2].Controls.Add(sortImage);
                        break;
                    case "ProjectName":
                        headerRow.Cells[3].Controls.Add(sortImage);
                        break;
                    case "ProjectStatus":
                        headerRow.Cells[4].Controls.Add(sortImage);
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "AddSortImage", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// ChangePage event handler
    /// </summary>
    protected void ChangePage(object sender, CommandEventArgs e)
    {
        GridViewRow gvrPager = grdvListofProjects.BottomPagerRow;
        TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");

        switch (e.CommandName)
        {
            case PREVIOUS:
                //To solved the issue id : 20375
                //Start
                if ((Convert.ToInt32(txtPages.Text) - 1) == 0)
                {
                    Session[SessionNames.CURRENT_PAGE_INDEX_PROJECT] = 1;
                    txtPages.Text = "1";
                }
                else
                {
                //End
                    Session[SessionNames.CURRENT_PAGE_INDEX_PROJECT] = Convert.ToInt32(txtPages.Text) - 1;
                    txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) - 1);
                }

                break;

            case NEXT:
                Session[SessionNames.CURRENT_PAGE_INDEX_PROJECT] = Convert.ToInt32(txtPages.Text) + 1;
                txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) + 1);
                break;
        }

        BindGrid();
    }

    // 27631-Subhra- Start
    //Added by Subhra for Issue Id:27631(CR)
    /// <summary>
    /// To populate the Client name dropdown depending on the selected Project Status 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            // 27631-Subhra- Added If condition to Fill Client status drop down box as per status
            if (Convert.ToInt32(ddlStatus.SelectedValue) == Convert.ToInt32(MasterEnum.ProjectStatus.Closed))
                FillClientDropDownAsPerStatus();
            else
                FillClientDropDown();

            //if (ddlStatus.SelectedValue != CommonConstants.SELECT)
            //{
            //    objProjectCriteria.StatusId = Convert.ToInt32(ddlStatus.SelectedValue);
            //}
            //else
            //    objProjectCriteria.StatusId = 0;

            //FillClientDropDownAsPerStatus();
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objex = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlStatus_SelectedIndexChanged", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objex);
        }
    }
    // 27631-Subhra- End
    

    private void FillClientDropDownAsPerStatus()
    {
        try
        {
            ddlClientName.Enabled = true;
            FillClosedClientDropDown();

        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
             RaveHRException objex = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "FillClientDropDownAsPerStatus", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
             LogErrorMessage(objex);
        }
        

    }

     //<summary>
     //To fill the Client dropdown as per the Status selected
     //</summary>
     //<param name="StatusId"></param>
    private void FillClosedClientDropDown()
    {
        List<BusinessEntities.Projects> objRaveHRProjects = new List<BusinessEntities.Projects>();
        Rave.HR.BusinessLayer.Projects.Projects objRaveHRProjctsBAL = new Rave.HR.BusinessLayer.Projects.Projects();
        try
        {
            objRaveHRProjects = objRaveHRProjctsBAL.GetClientNameAsPerStatus(objProjectCriteria);
            if (objRaveHRProjctsBAL != null)
            {
                ddlClientName.DataSource = objRaveHRProjects;
                ddlClientName.DataTextField = "ClientName";
                ddlClientName.DataValueField = "ClientId";
                ddlClientName.DataBind();
                ddlClientName.Items.Insert(0, SELECT);
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objex = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "FillClientDropDown", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }

    }


    #endregion Private Member Functions
    
}
