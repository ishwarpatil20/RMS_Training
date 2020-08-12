
//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           ListOfProject.cs       
//  Class:          listOfProject
//  Author:         Yagendra Sharnagat
//  Date written:   17/09/2009 3:51:30 PM
//  Description:    This class display the existing projevt details in a grid 
//                  to view in pop up window.
//
//  Amendments
//  Date                  Who                    Ref     Description
//  ----                  -----------            ---     -----------
//  17/09/2009            yagendra sharnagat     n/a      Created 
//------------------------------------------------------------------------------

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
using System.Collections.Generic;
using System.Data.SqlClient;
//using System.DirectoryServices;

using Rave.HR.BusinessLayer;
using Common;
using Common.Constants;
using Common.AuthorizationManager;

public partial class listOfProject : BaseClass
{
    #region Private Members


    /// <summary>
    /// Defines a constant for default value of drop down
    /// </summary>
    private const string SELECT = "Select";

    private const int PAGENUMBER = 1;

    /// <summary>
    /// Defines an ArrayList for Roles
    /// </summary>
    ArrayList arrRolesForUser = new ArrayList();

    /// <summary>
    /// Defines a currentPageIndex
    /// </summary>
    private string currentPageIndex = "currentPageIndex";

    /// <summary>
    /// Defines a constant for Page Name used in each catch block 
    /// </summary>
    private const string CLASS_NAME = "listOfProject.aspx";

    BusinessEntities.ProjectCriteria objProjectCriteria = new BusinessEntities.ProjectCriteria();

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
    /// Defines a constant for Page Size
    /// </summary>
    private const int PAGE_SIZE = 10;

    /// <summary>
    /// Defines a constant for Page Count.
    /// </summary>
    private const string PAGE_COUNT = "pagecount";

    /// <summary>
    /// Sets the image direction either upwards or downwards
    /// </summary>
    private string imageDirection = string.Empty;

    /// <summary>
    /// Defines the class name.
    /// </summary>
    private const string CLASS_NAME_LISTOFPROJECT = "listOfProject.aspx";

    /// <summary>
    /// Private Property to Get and Set direction for for sorting
    /// </summary>
    private SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState[SessionNames.CONTRACT_SORT_DIRECTION] == null)
                ViewState[SessionNames.CONTRACT_SORT_DIRECTION] = SortDirection.Ascending;

            return (SortDirection)ViewState[SessionNames.CONTRACT_SORT_DIRECTION];
        }
        set
        {
            ViewState[SessionNames.CONTRACT_SORT_DIRECTION] = value;
        }
    }



    #endregion Private Members

    #region Protected Methods

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!ValidateURL())
        {
            Response.Redirect(CommonConstants.INVALIDURL, false);
        }
        else
        {
            Response.Expires = 0;
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");

            //Umesh: Issue 'Modal Popup issue in chrome' Starts
            //btnCancle.Attributes.Add(CommonConstants.EVENT_ONCLICK, "JavaScript:window.close(); return false;");
            //Umesh: Issue 'Modal Popup issue in chrome' Ends

            btnOK.Attributes.Add("onclick", "return RadioChecked1();");


            if (!IsPostBack)
            {
                Session[SessionNames.CLIENT_NAME] = null;
                Session[SessionNames.PROJECT_SUMMARY_PROJECT_NAME] = null;
                Session[SessionNames.STATUS] = null;
                Session[SessionNames.PAGENUMBER] = null;
                Session[SessionNames.FILERCLICKED] = null;

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

                FillClientDropDown();

                FillStatusDropDown();

                //Populate the Grid with project details.        
                populateGridWithPRojectDetails(1);
            }
        }
    }

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
    /// Select checked project & close the pop up window.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            string projectCode = Request.Form["CommonRow"];

            //get the project code in session to display data in add project page.
            Session[SessionNames.CONTRACT_SELECETED_PROJECT] = projectCode;

            //Close the window.
            //Umesh: Issue 'Modal Popup issue in chrome' Starts
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "CloseJS", "jQuery.modalDialog.getCurrent().close();", true);
            //Umesh: Issue 'Modal Popup issue in chrome' Ends
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_LISTOFPROJECT, "btnOK_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// CLose the current window .
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancle_Click(object sender, EventArgs e)
    {
        Session.Remove(SessionNames.CONTRACT_SELECETED_PROJECT);
        //Umesh: Issue 'Modal Popup issue in chrome' Starts
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "CancleJS", "jQuery.modalDialog.getCurrent().close();", true);
        //Umesh: Issue 'Modal Popup issue in chrome' Ends
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdvListofProjects_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {


    }

    /// <summary>
    /// SOrt the grid view according to sort expression & direction.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdvListofProjects_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

            sortExpression = e.SortExpression;

            //Check the session for previous sort expression and setting value according to that
            if (ViewState[SessionNames.CONTRACT_PREVIOUS_SORT_EXPRESSION] == null)
            {
                //Keep the Sorting direction in session
                ViewState[SessionNames.CONTRACT_PREVIOUS_SORT_EXPRESSION] = sortExpression;
            }
            //Setting the sort direction and fetch data accroding to that
            if (ViewState[SessionNames.CONTRACT_PREVIOUS_SORT_EXPRESSION].ToString() == sortExpression)
            {
                //Sort to opposite direction based upon previous sort direction
                if (ViewState[SessionNames.CONTRACT_SORT_DIRECTION] == null || GridViewSortDirection == SortDirection.Descending)
                {
                    GridViewSortDirection = SortDirection.Ascending;

                    //Keep the Sorting direction in session
                    ViewState[SessionNames.CONTRACT_SORTDIRECTIONFORBINDING] = ASCENDING;

                }
                else
                {
                    GridViewSortDirection = SortDirection.Descending;

                    //Keep the Sorting direction in session
                    ViewState[SessionNames.CONTRACT_SORTDIRECTIONFORBINDING] = DESCENDING;

                }
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;

                //Keep the Sorting direction in session
                ViewState[SessionNames.CONTRACT_SORTDIRECTIONFORBINDING] = ASCENDING;

            }
            ViewState[SessionNames.CONTRACT_PREVIOUS_SORT_EXPRESSION] = sortExpression;
            if (Session[SessionNames.FILERCLICKED] != null)
                BindGrid();
            else
                populateGridWithPRojectDetails(2);

        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_LISTOFPROJECT, "grdvListofProjects_Sorting", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Change the page previous or next.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ChangePage(object sender, CommandEventArgs e)
    {
        try
        {
            GridViewRow gvrPager = grdvListofProjects.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");

            switch (e.CommandName)
            {
                case "Previous":
                    ViewState[currentPageIndex] = Convert.ToInt32(txtPages.Text) - 1;
                    txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) - 1);
                    break;

                case "Next":
                    ViewState[currentPageIndex] = Convert.ToInt32(txtPages.Text) + 1;
                    txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) + 1);
                    break;
            }

            Session[SessionNames.PAGENUMBER] = Convert.ToInt32(txtPages.Text);

            if (Session[SessionNames.FILERCLICKED] != null)
            {
                if (Session[SessionNames.FILERCLICKED].ToString() == true.ToString())

                    BindGrid();
                else
                    populateGridWithPRojectDetails(2);
            }
            else
                populateGridWithPRojectDetails(2);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_LISTOFPROJECT, "ChangePage", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// get the data of text box page no. 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtPages_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txtPages = (TextBox)sender;

            //Check text entered in text box is numeric or not.
            txtPages.Attributes.Add(CommonConstants.EVENT_ONKEYPRESS, "return isNumberKey(event)");

            //Check for Paging text box is not empty, non-zero, and out of range paging no.
            if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0 && Convert.ToInt32(txtPages.Text) <= Convert.ToInt32(ViewState[PAGE_COUNT].ToString()))
            {
                grdvListofProjects.PageIndex = Convert.ToInt32(txtPages.Text) - 1;
                ViewState[currentPageIndex] = txtPages.Text;
            }
            else
            {
                txtPages.Text = ViewState[currentPageIndex].ToString();
                return;
            }

            //Bind the grid on paging
            populateGridWithPRojectDetails(2);

        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_LISTOFPROJECT, "txtPages_TextChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// Add the sorting image on header.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdvListofProjects_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (ViewState[PAGE_COUNT] != null)
                {
                    if (Convert.ToInt32(ViewState[PAGE_COUNT].ToString()) > 0 && grdvListofProjects.AllowSorting == true)
                    {
                        //Add sort Images to Grid View Header
                        AddSortImage(e.Row);
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_LISTOFPROJECT, "grdvListofProjects_RowCreated", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// Function is executed when button filter is clicked.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            #region Clearing Session Before Filter
            Session[SessionNames.CLIENT_NAME] = null;
            Session[SessionNames.PROJECT_SUMMARY_PROJECT_NAME] = null;
            Session[SessionNames.STATUS] = null;
            Session[SessionNames.PAGENUMBER] = null;
            Session[SessionNames.FILERCLICKED] = null;
            #endregion

            Session[SessionNames.FILERCLICKED] = true.ToString();
            //Set values of filtering criterias to session
            Session[SessionNames.PROJECT_SUMMARY_PROJECT_NAME] = txtProjectName.Text.Trim();

            if (ddlClientName.SelectedItem.Value != SELECT)
                Session[SessionNames.CLIENT_NAME] = ddlClientName.SelectedItem.Value;

            if (ddlStatus.SelectedItem.Value != SELECT)
                Session[SessionNames.STATUS] = ddlStatus.SelectedItem.Value;

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
                btnClear.Visible = true;
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

    protected void btnRemoveFilter_Click(object sender, EventArgs e)
    {
        Session[SessionNames.CLIENT_NAME] = null;
        Session[SessionNames.PROJECT_SUMMARY_PROJECT_NAME] = null;
        Session[SessionNames.STATUS] = null;
        Session[SessionNames.PAGENUMBER] = null;
        Session[SessionNames.FILERCLICKED] = null;
        populateGridWithPRojectDetails(1);
    }
    #endregion Protected Methods

    #region Private Methods

    /// <summary>
    /// Populate the grid according to sorting criteria.
    /// </summary>
    /// <param name="Mode"></param>
    private void populateGridWithPRojectDetails(int Mode)
    {

        try
        {
            BusinessEntities.ContractProject objGridDetail = new BusinessEntities.ContractProject();
            //Declare contractproject class object.
            Rave.HR.BusinessLayer.Contracts.ContractProject projectDetails = new Rave.HR.BusinessLayer.Contracts.ContractProject();
            BusinessEntities.RaveHRCollection CollectionGridDetail = new BusinessEntities.RaveHRCollection();
            //Declare list of contractproject class object.
            List<BusinessEntities.ContractProject> objListOfProjects = new List<BusinessEntities.ContractProject>();

            //On pageload get the data with default sorting.
            if (Mode == 1)
            {
                CollectionGridDetail = projectDetails.GetProjectsListDetails(GetCriteriaForPageload());
                for (int i = 0; i < CollectionGridDetail.Count; i++)
                {
                    foreach (List<BusinessEntities.ContractProject> con in CollectionGridDetail)
                    {
                        foreach (BusinessEntities.ContractProject c in con)
                        {
                            objGridDetail = (BusinessEntities.ContractProject)c;
                            objListOfProjects.Add(objGridDetail);
                        }
                    }

                }


            }
            else
            {
                //Get the data with paging & sorting.

                CollectionGridDetail = projectDetails.GetProjectsListDetails(GetCriteriaForPagingAndSorting());

                for (int i = 0; i < CollectionGridDetail.Count; i++)
                {
                    foreach (List<BusinessEntities.ContractProject> con in CollectionGridDetail)
                    {
                        foreach (BusinessEntities.ContractProject c in con)
                        {
                            objGridDetail = (BusinessEntities.ContractProject)c;
                            objListOfProjects.Add(objGridDetail);
                        }
                    }
                }
            }

            //Bind the grid with respective data.
            BindData(objListOfProjects);
        }


        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_LISTOFPROJECT, "populateGridWithPRojectDetails", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// Binds The Grid for Filter.
    /// </summary>
    /// <param name="Mode"></param>
    private void BindGrid()
    {
        BusinessEntities.ContractProject objGridDetail = new BusinessEntities.ContractProject();

        if (Session[SessionNames.PROJECT_SUMMARY_PROJECT_NAME] != null)

            objGridDetail.ProjectName = Session[SessionNames.PROJECT_SUMMARY_PROJECT_NAME].ToString();

        if (Session[SessionNames.CLIENT_NAME] != null)

            objGridDetail.ClientName = Session[SessionNames.CLIENT_NAME].ToString();

        if (Session[SessionNames.STATUS] != null)

            objGridDetail.StatusID = Convert.ToInt32(Session[SessionNames.STATUS].ToString());


        //Declare contractproject class object.
        Rave.HR.BusinessLayer.Contracts.ContractProject projectDetails = new Rave.HR.BusinessLayer.Contracts.ContractProject();
        BusinessEntities.RaveHRCollection CollectionGridDetail = new BusinessEntities.RaveHRCollection();
        //Declare list of contractproject class object.
        List<BusinessEntities.ContractProject> objListOfProjects = new List<BusinessEntities.ContractProject>();

        objListOfProjects = projectDetails.GetProjectsDetailsForFilter(objGridDetail, GetCriteriaForPageload());

        BindData(objListOfProjects);
    }

    /// <summary>
    /// Get the Criteria for page load.
    /// </summary>
    /// <returns></returns>
    private BusinessEntities.ContractCriteria GetCriteriaForPageload()
    {
        //Define the ContractCriteria object.
        BusinessEntities.ContractCriteria criteria = new BusinessEntities.ContractCriteria();

        //Set the all default criteria.
        if (Session[SessionNames.PAGENUMBER] == null)
            Session[SessionNames.PAGENUMBER] = 1;
        criteria.PageNumber = Convert.ToInt32(Session[SessionNames.PAGENUMBER].ToString());
        ViewState[currentPageIndex] = criteria.PageNumber;
        criteria.Direction = DESCENDING;
        //GridViewSortDirection = SortDirection.Descending;

        if ((sortExpression == string.Empty) || (sortExpression == null))
        {
            sortExpression = criteria.SortExpression;
            criteria.SortExpression = CommonConstants.CON_PROJECTCODE;
        }
        else
        {
            criteria.SortExpression = sortExpression;
        }

        if (ViewState[SessionNames.CONTRACT_PREVIOUS_SORT_EXPRESSION] == null)
        {
            criteria.SortExpression = CommonConstants.CON_PROJECTCODE;
        }
        else
        {
            criteria.SortExpression = ViewState[SessionNames.CONTRACT_PREVIOUS_SORT_EXPRESSION].ToString();
        }
        if (ViewState[currentPageIndex] != null)
        {
            criteria.PageNumber = Convert.ToInt32(ViewState[currentPageIndex].ToString());
        }
        else
        {
            criteria.PageNumber = 1;
            ViewState[currentPageIndex] = criteria.PageNumber;
        }
        if (ViewState[SessionNames.CONTRACT_SORTDIRECTIONFORBINDING] == null)
            criteria.Direction = DESCENDING;
        else
            criteria.Direction = ViewState[SessionNames.CONTRACT_SORTDIRECTIONFORBINDING].ToString();




        return criteria;
    }

    /// <summary>
    /// Get the criteria for  sorting & paging.
    /// </summary>
    /// <returns></returns>
    private BusinessEntities.ContractCriteria GetCriteriaForPagingAndSorting()
    {
        try
        {
            //Define the ContractCriteria object.
            BusinessEntities.ContractCriteria criteria = new BusinessEntities.ContractCriteria();

            //setting the Sort Expression ie on which field sort will happen.
            if (ViewState[SessionNames.CONTRACT_PREVIOUS_SORT_EXPRESSION] == null)
            {
                criteria.SortExpression = CommonConstants.CON_PROJECTCODE;
            }
            else
            {
                criteria.SortExpression = ViewState[SessionNames.CONTRACT_PREVIOUS_SORT_EXPRESSION].ToString();
            }


            // setting page number according to which record will fetch.
            if (ViewState[currentPageIndex] != null)
            {
                criteria.PageNumber = Convert.ToInt32(ViewState[currentPageIndex].ToString());
            }
            else
            {
                criteria.PageNumber = 1;
                ViewState[currentPageIndex] = criteria.PageNumber;
            }


            //Setting the sort direction.
            if (ViewState[SessionNames.CONTRACT_SORTDIRECTIONFORBINDING] == null)
                criteria.Direction = DESCENDING;
            else
                criteria.Direction = ViewState[SessionNames.CONTRACT_SORTDIRECTIONFORBINDING].ToString();

            return criteria;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_LISTOFPROJECT, "GetCriteriaForPagingAndSorting", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Bind  data to grid view.
    /// </summary>
    /// <param name="objListOfProjects"></param>
    private void BindData(List<BusinessEntities.ContractProject> objListOfProjects)
    {
        try
        {
            //Chech whether count is greater than zero or not.
            if (objListOfProjects.Count > 0)
            {
                //Define the dataset.
                DataSet dsProjectdetails = new DataSet();

                //Define the datatable & columns.
                DataTable dtProjectData = new DataTable();
                dtProjectData.Columns.Add(DbTableColumn.Con_ProjectID);
                dtProjectData.Columns.Add(DbTableColumn.Con_ProjectCode);
                dtProjectData.Columns.Add(DbTableColumn.Con_DocumentName);
                dtProjectData.Columns.Add(DbTableColumn.Con_ContractCode);
                dtProjectData.Columns.Add(DbTableColumn.Con_ProjectName);
                dtProjectData.Columns.Add(DbTableColumn.Con_ContractType);
                dtProjectData.Columns.Add(DbTableColumn.ClientName);

                //fill the table with data got from database.
                foreach (BusinessEntities.ContractProject proj in objListOfProjects)
                {
                    DataRow row = dtProjectData.NewRow();
                    row[DbTableColumn.Con_ProjectID] = (proj.ProjectID);
                    row[DbTableColumn.Con_ProjectCode] = (proj.ProjectCode).ToString();
                    row[DbTableColumn.Con_DocumentName] = (proj.DocumentName).ToString();
                    row[DbTableColumn.Con_ContractCode] = (proj.ContractCode).ToString();
                    row[DbTableColumn.Con_ProjectName] = (proj.ProjectName).ToString();
                    row[DbTableColumn.Con_ContractType] = (proj.ContractType).ToString();
                    row[DbTableColumn.ClientName] = (proj.ClientName).ToString();
                    dtProjectData.Rows.Add(row);
                }

                //Add table to dataset.
                dsProjectdetails.Tables.Add(dtProjectData);
                Session[SessionNames.CONTRACT_LISTOFPROJECT_DATA] = dtProjectData;

                int pageCount = 1;

                //Get the page count from contract which is assigned to contract 
                foreach (BusinessEntities.Contract ContractItem in objListOfProjects)
                {
                    pageCount = ContractItem.PageCount;
                    break;
                }
                //Assign pagecount to viewstate to get it next time.
                ViewState[PAGE_COUNT] = pageCount;

                if (pageCount == 1 && objListOfProjects.Count == 1)
                {
                    grdvListofProjects.AllowSorting = false;
                }
                else
                {
                    grdvListofProjects.AllowSorting = true;
                }

                //Bind datatable to data grid.
                grdvListofProjects.DataSource = dsProjectdetails;
                grdvListofProjects.DataBind();
                grdvListofProjects.AllowPaging = true;
                grdvListofProjects.PageSize = 10;

                //Display the paging according to data.
                DisplayPaging(objListOfProjects);
            }
            else
            {
                //Bind datatable to data grid.
                grdvListofProjects.DataSource = objListOfProjects;
                grdvListofProjects.DataBind();

                //Display Header When GridView Empty with proper message.
                ShowHeaderWhenEmptyGrid(objListOfProjects);

            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_LISTOFPROJECT, "BindData", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Display paging according to paging criteria.
    /// </summary>
    /// <param name="objListOfProjects"></param>
    private void DisplayPaging(List<BusinessEntities.ContractProject> objListOfProjects)
    {
        try
        {
            //int pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(objListOfProjects.Count) / 10));
            int pageCount = 1;

            //Get the page count from contract which is assigned to contract 
            foreach (BusinessEntities.Contract ContractItem in objListOfProjects)
            {
                pageCount = ContractItem.PageCount;
                break;
            }
            //Assign pagecount to viewstate to get it next time.
            ViewState[PAGE_COUNT] = pageCount;

            GridViewRow gvrPager = grdvListofProjects.BottomPagerRow;

            if (gvrPager != null)
            {

                //Get Text Box and Lable from the Grid View
                TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
                Label lblPageCount = (Label)gvrPager.Cells[0].FindControl("lblPageCount");
                LinkButton lbtnPrevious = (LinkButton)gvrPager.Cells[0].FindControl("lbtnPrevious");
                LinkButton lbtnNext = (LinkButton)gvrPager.Cells[0].FindControl("lbtnNext");

                //Check whether Page count is greater than one or not.
                if (Convert.ToInt32(ViewState[PAGE_COUNT].ToString()) > 1)
                {
                    grdvListofProjects.BottomPagerRow.Visible = true;
                }
                else
                {
                    grdvListofProjects.BottomPagerRow.Visible = false;
                }

                //Check whether Page count is greater than one or not.
                if (Convert.ToInt32(ViewState[PAGE_COUNT].ToString()) > 1)
                {
                    lblPageCount.Text = pageCount.ToString();
                    txtPages.Text = ViewState[currentPageIndex].ToString();
                    lbtnNext.Enabled = true;
                    //ViewState[currentPageIndex] = txtPages.Text;
                }
                //else
                //{
                //Check whether Page count is equal to one or not.
                if (Convert.ToInt32(ViewState[currentPageIndex].ToString()) == 1)
                {
                    lbtnNext.Enabled = true;
                    lbtnPrevious.Enabled = false;
                }
                else if (Convert.ToInt32(ViewState[currentPageIndex].ToString()) > 1 && Convert.ToInt32(ViewState[currentPageIndex].ToString()) < Convert.ToInt32(ViewState[PAGE_COUNT].ToString()))
                {
                    lbtnNext.Enabled = true;
                    lbtnPrevious.Enabled = true;
                }
                if (Convert.ToInt32(ViewState[currentPageIndex].ToString()) == Convert.ToInt32(ViewState[PAGE_COUNT].ToString()))
                {
                    lbtnNext.Enabled = false;
                    lbtnPrevious.Enabled = true;
                }
                //}
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_LISTOFPROJECT, "DisplayPaging", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
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
                    case CommonConstants.CON_PROJECTCODE:
                        headerRow.Cells[1].Controls.Add(sortImage);
                        break;

                    case CommonConstants.CON_PROJECTNAME:
                        headerRow.Cells[2].Controls.Add(sortImage);
                        break;

                    case CommonConstants.CLIENT_NAME:
                        headerRow.Cells[3].Controls.Add(sortImage);
                        break;

                    case CommonConstants.CON_CONTRACTCODE:
                        headerRow.Cells[4].Controls.Add(sortImage);
                        break;

                    case CommonConstants.CON_DOCUMENTNAME:
                        headerRow.Cells[5].Controls.Add(sortImage);
                        break;

                    case CommonConstants.CON_CONTRACTTYPENAME:
                        headerRow.Cells[6].Controls.Add(sortImage);
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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_LISTOFPROJECT, "AddSortImage", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Display Header When GridView Empty with proper message.
    /// </summary>
    /// <param name="raveHRCollection">EmptyCollection</param>
    private void ShowHeaderWhenEmptyGrid(List<BusinessEntities.ContractProject> objListOfProjects)
    {
        try
        {
            //set header visible
            grdvListofProjects.ShowHeader = true;
            // Disable sorting
            grdvListofProjects.AllowSorting = false;

            //Create empty datasource for Grid view and bind
            objListOfProjects.Add(new BusinessEntities.ContractProject());
            grdvListofProjects.DataSource = objListOfProjects;
            grdvListofProjects.DataBind();

            //clear all the cells in the row
            grdvListofProjects.Rows[0].Cells.Clear();

            //add a new blank cell
            grdvListofProjects.Rows[0].Cells.Add(new TableCell());
            grdvListofProjects.Rows[0].Cells[0].Text = CommonConstants.NO_RECORDS_FOUND_MESSAGE;
            grdvListofProjects.Rows[0].Cells[0].Wrap = false;
            grdvListofProjects.Rows[0].Cells[0].Width = Unit.Percentage(10);

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_LISTOFPROJECT, "ShowHeaderWhenEmptyGrid", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
        }
    }

    #endregion Private Methods

}
