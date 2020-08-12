//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           MrfSummary.aspx.cs    
//  Author:         Gaurav.Thakkar
//  Date written:   24/8/2009/ 13:26:00 PM
//  Description:    This page lists the MRF Summary
//                  
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  24/8/2009        Gaurav.Thakkar       n/a     Created    
//  28/05/2008       Yagendra Sharnagat           Modified(Added Export to excel functinality) 
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
using BusinessEntities;
using Common;
using Common.AuthorizationManager;
using System.Data.SqlClient;
using Common.Constants;
using System.IO;

public partial class MrfSummary : BaseClass
{
    #region Private Members

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
    /// Defines default value for sorting expression 
    /// </summary>
    private static string sortExpression = string.Empty;

    /// <summary>
    /// Defines a constant for Page Count
    /// </summary>
    private int pageCount = 0;

    /// <summary>
    /// Defines the command name Previous
    /// </summary>
    private const string PREVIOUS = "Previous";

    /// <summary>
    /// Defines the command name Next
    /// </summary>
    private const string NEXT = "Next";

    /// <summary>
    /// Sets the image direction either upwards or downwards
    /// </summary>
    private string imageDirection = string.Empty;

    /// <summary>
    /// All the parameters to be passed to SP
    /// </summary>
    BusinessEntities.ParameterCriteria objParameter = new BusinessEntities.ParameterCriteria();

    /// <summary>
    /// Determines the total no. roles for user.
    /// </summary>
    ArrayList rolesForUser = new ArrayList();

    /// <summary>
    /// Store the Value of row count
    /// </summary>
    Hashtable hashTable = new Hashtable();

    //Start 
    //IssueId 27633 subhra
    /// <summary>
    /// To store data for previous and next page for paging issue
    /// </summary>
    Hashtable temppreviousHashTable = new Hashtable();
    BusinessEntities.RaveHRCollection raveHRpreviousCollection = new BusinessEntities.RaveHRCollection();
    private int IntHashpageCount = 0;
    //End 27633

    /// <summary>
    /// Defines the class name.
    /// </summary>
    private const string CLASS_NAME_MRFSUMMARY = "MrfSummary.aspx";

    /// <summary>
    /// Status : PendingExternalAllocation
    /// </summary>
    private const string PENDINGEXTERNALALLOCATION = "Pending External Allocation";

    //Ishwar Patil 01102014 For NIS : Start
    private const string _RMOGroupName = "RMOGroupName";
    //Ishwar Patil 01102014 For NIS : End

    AuthorizationManager objAuMan = new AuthorizationManager();

    ArrayList arrRolesForUser = new ArrayList();



    #endregion Private Members

    #region Public Field Members

    /// <summary>
    /// Collection
    /// </summary>
    BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

    #endregion

    #region Protected Events

    /// <summary>
    /// PageLoad event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
                     arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERECRUITMENT) ||
                     arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR) ||
                     arrRolesForUser.Contains(AuthorizationManagerConstants.ROLECOO) ||
                     arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEFM) ||
                     arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEQUALITY) ||
                     arrRolesForUser.Contains(AuthorizationManagerConstants.ROLETESTING)))
                {
                    Response.Redirect(CommonConstants.PAGE_HOME, true);
                }
                //Siddharth 26th August 2015 End



                if (Session[SessionNames.CONFIRMATION_MESSAGE] != null)
                {
                    lblMessage.Text = Convert.ToString(Session[SessionNames.CONFIRMATION_MESSAGE]);
                    Session.Remove(SessionNames.CONFIRMATION_MESSAGE);
                    lblMessage.Visible = true;
                }
                else
                {
                    lblMessage.Text = "";
                }
                //Determine the role of Logged-in user.
                GetRolesforUser();

                if (!IsPostBack)
                {
                    if (Session[SessionNames.MRFVIEWINDEX] != null)
                    {
                        Session.Remove(SessionNames.MRFVIEWINDEX);
                    }

                    if (Session[SessionNames.CURRENT_PAGE_INDEX_MRF] == null)
                    {
                        Session[SessionNames.CURRENT_PAGE_INDEX_MRF] = 1;
                    }

                    // Setting the Default sortExpression as MrfCode
                    if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION_MRF] == null)
                    {
                        sortExpression = CommonConstants.MRF_CODE;
                        Session[SessionNames.PREVIOUS_SORT_EXPRESSION_MRF] = sortExpression;
                    }
                    else
                    {
                        sortExpression = Session[SessionNames.PREVIOUS_SORT_EXPRESSION_MRF].ToString();
                    }

                    //Fill all the dropdowns in Filter Panel
                    this.FillDropDowns();

                    //Bind the MRF Summary Grid
                    BindGridMrfSummary();

                    gvListOfMrf.Columns[0].Visible = false;
                    //gvListOfMrf.Columns[9].Visible = false;

                    if (Session[SessionNames.DEPARTMENT_ID] != null)
                    {
                        ddlDepartment.SelectedValue = Session[SessionNames.DEPARTMENT_ID].ToString();
                        // 24736-Ambar-Start
                        // Fill drop downs as per role so the session value can be restored.
                        filldropdownasperrole();
                        // 24736-Ambar-End
                    }

                    if (Session[SessionNames.PROJECT_ID_MRF] != null)
                    {
                        ddlProjectName.SelectedValue = Session[SessionNames.PROJECT_ID_MRF].ToString();
                    }

                    if (Session[SessionNames.ROLE] != null)
                    {
                        ddlRole.SelectedValue = Session[SessionNames.ROLE].ToString();
                    }

                    if (Session[SessionNames.MRF_STATUS_ID] != null)
                    {
                        ddlStatus.SelectedValue = Session[SessionNames.MRF_STATUS_ID].ToString();
                        if (ddlStatus.SelectedItem.Text == MasterEnum.MRFStatus.Closed.ToString()
                            || ddlStatus.SelectedItem.Text == MasterEnum.MRFStatus.Abort.ToString())
                        {
                            gvListOfMrf.Columns[11].Visible = true;
                            if (ddlStatus.SelectedItem.Text == MasterEnum.MRFStatus.Closed.ToString())
                            {
                                gvListOfMrf.Columns[12].Visible = true;
                                gvListOfMrf.Columns[13].Visible = true;
                            }
                            else
                            {
                                gvListOfMrf.Columns[12].Visible = false;
                                gvListOfMrf.Columns[13].Visible = false;
                            }
                        }
                        else
                        {
                            gvListOfMrf.Columns[11].Visible = false;
                            gvListOfMrf.Columns[12].Visible = false;
                            gvListOfMrf.Columns[13].Visible = false;
                        }
                        if (ddlStatus.SelectedItem.Text == PENDINGEXTERNALALLOCATION)
                        {
                            gvListOfMrf.Columns[6].Visible = true;
                            //gvListOfMrf.Columns[5].Visible = false;
                        }
                    }

                    if (Session[SessionNames.PROJECT_ID_MRF] != null || Session[SessionNames.ROLE] != null || Session[SessionNames.MRF_STATUS_ID] != null || Session[SessionNames.DEPARTMENT_ID] != null)
                    {
                        btnRemoveFilter.Visible = true;
                    }

                    if (objParameter.RoleRecruitment == AuthorizationManagerConstants.ROLERECRUITMENT)
                    {
                        gvListOfMrf.Columns[6].Visible = true;
                    }
                    //Ishwar Patil 30092014 For NIS : Start
                    string strRMOGroupName = string.Empty;
                    if (ConfigurationManager.AppSettings[_RMOGroupName] != null)
                    {
                        strRMOGroupName = ConfigurationManager.AppSettings[_RMOGroupName].ToString();
                    }
                    Common.AuthorizationManager.AuthorizationManager objAuth =
                                                        new Common.AuthorizationManager.AuthorizationManager();
                    if (!strRMOGroupName.Contains(objAuth.getLoggedInUser()))
                    {
                        trRMSMRF.Visible = false;
                    }
                    else
                    {
                        trRMSMRF.Visible = true;
                    }
                    //Ishwar Patil 30092014 For NIS : End
                }
            }
            if (Session["MovedStatus"] != null)
            {
                lblMessage.Text = Session["MovedStatus"].ToString();
                lblMessage.Visible = true;
                Session["MovedStatus"] = null;
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "Page_Load", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// When a particular Column is clicked for Sorting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvListOfMrf_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            GridViewRow gvrPager = gvListOfMrf.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_MRF].ToString();

            //On sorting assign new sort expression
            sortExpression = e.SortExpression;

            if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION_MRF] == null)
            {
                Session[SessionNames.PREVIOUS_SORT_EXPRESSION_MRF] = sortExpression;
            }

            if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION_MRF].ToString() == sortExpression)
            {
                //Sort to opposite direction based upon previous sort direction
                if (Session[SessionNames.SORT_DIRECTION_MRF] == null || GridViewSortDirection == SortDirection.Descending)
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

            if (Session[SessionNames.PROJECT_ID_MRF] != null || Session[SessionNames.DEPARTMENT_ID] != null || Session[SessionNames.ROLE] != null || Session[SessionNames.MRF_STATUS_ID] != null)
            {
                btnRemoveFilter.Visible = true;
            }

            //Set current sort expression as PreviousSortExpression
            Session[SessionNames.PREVIOUS_SORT_EXPRESSION_MRF] = sortExpression;
        }

        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "gvListOfMrf_Sorting", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Paging for Grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvListOfMrf_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (e.NewPageIndex != -1)
            {
                // Assign the new page index
                gvListOfMrf.PageIndex = e.NewPageIndex;
            }

            // Bind the grid as per new page index.
            BindGridMrfSummary();
        }

       //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "gvListOfMrf_PageIndexChanging", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// The RowCreated event is raised when each row in the GridView control is created
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvListOfMrf_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (((Session[SessionNames.PAGE_COUNT_MRF] != null) && (int.Parse(Session[SessionNames.PAGE_COUNT_MRF].ToString()) > 1)) || ((raveHRCollection.Count > 1)))
                {
                    //Add sort Images to Grid View Header
                    AddSortImage(e.Row);
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "gvListOfMrf_RowCreated", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// When data is bound to datarow in gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvListOfMrf_DataBound(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrPager = gvListOfMrf.BottomPagerRow;

            if (gvrPager == null) return;

            //Get Text Box and Lable from the Grid View
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            Label lblPageCount = (Label)gvrPager.Cells[0].FindControl("lblPageCount");

            //Assign current page index to text box
            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_MRF].ToString();

            //Assign total no of pages to label
            if (lblPageCount != null)
                lblPageCount.Text = pageCount.ToString();
        }

        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "gvListOfMrf_DataBound", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Row is bound to grid 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvListOfMrf_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
              //Name: Sanju:Issue Id 50201  Changed cursor property to pointer so that it should display hand in IE10,9,Chrome and mozilla browser.
              //  e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";

                // Mohamed : NIS-RMS : 07/01/2015 : Starts                        			  
                // Desc : Remove underline and click from remaining columns

                e.Row.Cells[1].Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                e.Row.Cells[1].Attributes["onmouseout"] = "this.style.textDecoration='none';";

                e.Row.Cells[1].Attributes["onclick"] = "location.href = 'MrfView.aspx?" + URLHelper.SecureParameters("MRFId", DataBinder.Eval(e.Row.DataItem, "MRFID").ToString()) + "&" + URLHelper.SecureParameters("index", e.Row.RowIndex.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "MRFSUMMERY") + "&" + URLHelper.CreateSignature(DataBinder.Eval(e.Row.DataItem, "MRFID").ToString(), e.Row.RowIndex.ToString(), "MRFSUMMERY") + "'";

                // Mohamed : NIS-RMS : 07/01/2015 : Ends

                //To solved the issue id : 20365
                //Start
                if (!hashTable.Contains(e.Row.RowIndex))
                    hashTable.Add(e.Row.RowIndex, DataBinder.Eval(e.Row.DataItem, "MRFID").ToString());
                //End    
            }
                Session[SessionNames.MRFVIEWINDEX] = hashTable;
            //hashTable.Clear();
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "gvListOfMrf_RowDataBound", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Filter is applied to grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            // Assign DepartmentId to Session
            Session[SessionNames.DEPARTMENT_ID] = ddlDepartment.SelectedItem.Value;
            // Assign ProjectId to Session
            Session[SessionNames.PROJECT_ID_MRF] = ddlProjectName.SelectedItem.Value;
            // Assign RoleId to Session
            Session[SessionNames.ROLE] = ddlRole.SelectedItem.Value;
            // Assign MrfStatusId to Session
            if (ddlStatus.SelectedItem.Value != CommonConstants.SELECT)
                Session[SessionNames.MRF_STATUS_ID] = ddlStatus.SelectedItem.Value;
            else
                Session[SessionNames.MRF_STATUS_ID] = 0;

            // Assign MrfStatusName to Session
            Session[SessionNames.MRF_STATUS_NAME] = ddlStatus.SelectedItem.Text;

            if (gvListOfMrf.BottomPagerRow != null)
            {
                GridViewRow gvrPager = gvListOfMrf.BottomPagerRow;
                TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");


                if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0)
                {
                    txtPages.Text = 1.ToString();
                    Session[SessionNames.CURRENT_PAGE_INDEX_MRF] = txtPages.Text;
                }
            }

            BindGridMrfSummary();

            if ((objParameter.RoleRPM == AuthorizationManagerConstants.ROLERPM) || (objParameter.RolePM == AuthorizationManagerConstants.ROLEPROJECTMANAGER) || (objParameter.RoleAPM == AuthorizationManagerConstants.ROLEAPM) || (objParameter.RoleGPM == AuthorizationManagerConstants.ROLEGPM))
            {
                if (ddlStatus.SelectedItem.Text == MasterEnum.MRFStatus.Closed.ToString()
                    || ddlStatus.SelectedItem.Text == MasterEnum.MRFStatus.Abort.ToString())
                {
                    gvListOfMrf.Columns[11].Visible = true;
                    if (ddlStatus.SelectedItem.Text == MasterEnum.MRFStatus.Closed.ToString())
                    {
                        gvListOfMrf.Columns[13].Visible = true;
                    }
                    else
                    {
                        gvListOfMrf.Columns[13].Visible = false;
                    }
                }
                else
                {
                    gvListOfMrf.Columns[11].Visible = false;
                    gvListOfMrf.Columns[13].Visible = false;
                }
                if (ddlStatus.SelectedItem.Text == PENDINGEXTERNALALLOCATION)
                {
                    gvListOfMrf.Columns[6].Visible = true;
                }
                else
                {
                    gvListOfMrf.Columns[6].Visible = false;
                }
            }

            gvListOfMrf.Columns[0].Visible = false;

            if (Session[SessionNames.PROJECT_ID_MRF] != null || Session[SessionNames.DEPARTMENT_ID] != null || Session[SessionNames.ROLE] != null || Session[SessionNames.MRF_STATUS_ID] != null)
            {
                btnRemoveFilter.Visible = true;
            }
        }

        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "btnFilter_Click", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Text change of pager textbox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtPages_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txtPages = (TextBox)sender;

            //Check for Paging text box is not empty, non-zero, and out of range paging no.
            if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0 && Convert.ToInt32(txtPages.Text) <= Convert.ToInt32(Session[SessionNames.PAGE_COUNT_MRF].ToString()))
            {
                gvListOfMrf.PageIndex = Convert.ToInt32(txtPages.Text) - 1;
                Session[SessionNames.CURRENT_PAGE_INDEX_MRF] = txtPages.Text;
            }
            else
            {
                txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_MRF].ToString();
                return;
            }

            //Bind the grid on paging
            BindGridMrfSummary();

            //Assign the tetbox current page no.
            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_MRF].ToString();
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "txtPages_TextChanged", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
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
        try
        {
            GridViewRow gvrPager = gvListOfMrf.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");

            switch (e.CommandName)
            {
                //Previous button is clicked
                case PREVIOUS:
                    //To solved the issue id : 20365
                    //Start
                    if ((Convert.ToInt32(txtPages.Text) - 1) == 0)
                    {
                        Session[SessionNames.CURRENT_PAGE_INDEX_MRF] = 1;
                        txtPages.Text = "1";
                    }
                    else
                    {
                    //End
                        Session[SessionNames.CURRENT_PAGE_INDEX_MRF] = Convert.ToInt32(txtPages.Text) - 1;
                        txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) - 1);
                    }
                    break;

                //Next button is clicked
                case NEXT:
                    Session[SessionNames.CURRENT_PAGE_INDEX_MRF] = Convert.ToInt32(txtPages.Text) + 1;
                    txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) + 1);
                    break;
            }

            // Bind the grid on paging.
            BindGridMrfSummary();
        }

        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "ChangePage", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// On remove Filter Clear all the Filtering criteria and once again bind the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRemoveFilter_Click(object sender, EventArgs e)
    {
        try
        {
            // Assign DepartmentId to Session
            Session[SessionNames.DEPARTMENT_ID] = null;
            // Assign ProjectId to Session
            Session[SessionNames.PROJECT_ID_MRF] = null;
            // Assign RoleId to Session
            Session[SessionNames.ROLE] = null;
            // Assign MrfStatusId to Session
            Session[SessionNames.MRF_STATUS_ID] = null;
            Session[SessionNames.MRF_STATUS_NAME] = null;
            //User is directed to 1st page
            Session[SessionNames.CURRENT_PAGE_INDEX_MRF] = 1;

            sortExpression = CommonConstants.MRF_CODE;
            Session[SessionNames.PREVIOUS_SORT_EXPRESSION_APPREJMRF] = sortExpression;

            ClearFilteringFields();
            BindGridMrfSummary();
            gvListOfMrf.Columns[11].Visible = false;
            gvListOfMrf.Columns[6].Visible = false;
            //gvListOfMrf.Columns[5].Visible = true;
            btnRemoveFilter.Visible = false;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "btnRemoveFilter_Click", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Depending upon the selected Department, fill the Role dropdown
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlProjectName.ClearSelection();
            if (ddlDepartment.SelectedValue != CommonConstants.SELECT)
            {
                FillRoleDropdownAsPerDepartment();
            }
            else
            {
                ddlProjectName.Enabled = false;
                ddlRole.Enabled = false;
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "ddlDepartment_SelectedIndexChanged", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Export TO excel the MRf details.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
             string direction = "  ASC";

            // Setting the Default sortExpression as MrfCode
            if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION_MRF] == null)
            {
                sortExpression = CommonConstants.MRF_CODE;
                Session[SessionNames.PREVIOUS_SORT_EXPRESSION_MRF] = sortExpression;
            }
            else
            {
                sortExpression = Session[SessionNames.PREVIOUS_SORT_EXPRESSION_MRF].ToString();
            }


            if (sortExpression == CommonConstants.MRF_CODE)
            {
                objParameter.SortExpressionAndDirection = sortExpression + direction;
            }
            else
            {
                objParameter.SortExpressionAndDirection = sortExpression + direction + "," + CommonConstants.MRF_CODE + direction;
            }


            //Define object parameter as we required whole data so i provided page size 11000.
            objParameter.PageNumber = 1;
            objParameter.PageSize = 10000;
            //Ishwar Patil 29092014 For NIS : Start
            objParameter.IsRMSMRF = RBRMSMRF.SelectedValue;
            //Ishwar Patil 29092014 For NIS : End

            //Get MRF details
            raveHRCollection = GetMrfSummary();

            ExportToExcel(raveHRCollection);
            
        }
        catch (System.Threading.ThreadAbortException ex) { }

        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "btnExportToExcel_Click", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void gvListOfMrf_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == CommonConstants.MOVEMRF)
        {
        }
    }

    //Ishwar Patil 29092014 For NIS : Start
    protected void OnSelectedIndexChanged_RBRMSMRF(object sender, EventArgs e)
    {
        BindGridMrfSummary();
    }
    //Ishwar Patil 29092014 For NIS : End

    #endregion

    #region Private Method

    /// <summary>
    /// Fills all the dropdown in Filter panel.
    /// </summary>
    private void FillDropDowns()
    {
        try
        {
            //Fill Department dropdown
            FillDepartmentDropDown();

            //Fill Status dropdown
            FillStatusDropDown();

            //On Pageload ProjectName & Role dropdowns are disabled and their default value is 
            //set to "SELECT".
            ddlProjectName.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
            ddlProjectName.Enabled = false;
            ddlRole.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
            ddlRole.Enabled = false;

            //Check if user is not RPM (or COO or CEO)
            if (objParameter.RoleRPM != AuthorizationManagerConstants.ROLERPM)
            {
                //If User is PM than By default Department is "Projects" and is disabled.
                //ProjectName dropdown will only have those projects which are alloted to PM
                if (objParameter.RolePM == AuthorizationManagerConstants.ROLEPM || objParameter.RoleGPM == AuthorizationManagerConstants.ROLEPM || objParameter.RoleAPM == AuthorizationManagerConstants.ROLEPM)
                {
                    ddlDepartment.ClearSelection();
                    ddlDepartment.Items.FindByText(MasterEnum.Departments.Projects.ToString()).Selected = true;
                    ddlDepartment.Enabled = false;

                    ddlRole.Enabled = true;
                    FillRoleDropDown(int.Parse(ddlDepartment.SelectedItem.Value));

                    ddlProjectName.Enabled = true;
                    FillProjectNameDropdownForPM();
                }

                //If user is CFM/FM than Department is "Finance"
                if (objParameter.RoleCFM == AuthorizationManagerConstants.ROLECFM)
                {
                    ddlDepartment.ClearSelection();
                    ddlDepartment.Items.FindByText(MasterEnum.Departments.Finance.ToString()).Selected = true;
                    ddlDepartment.Enabled = false;

                    ddlRole.Enabled = true;
                    FillRoleDropDown(int.Parse(ddlDepartment.SelectedItem.Value));

                }

                //If user is HR than Status selected is "Pending External Allocation"
                if (objParameter.RoleRecruitment == AuthorizationManagerConstants.ROLERECRUITMENT)
                {
                    ddlStatus.Items.FindByText(CommonConstants.MRFStatus_PendingExternalAllocation).Selected = true;
                    ddlStatus.Enabled = false;

                }

                //If user is PreSales than the departments enabled are "Projects" and "PreSales"
                if (objParameter.RolePreSales == AuthorizationManagerConstants.ROLEPRESALES)
                {
                    ddlDepartment.ClearSelection();
                    //ddlDepartment.Items.FindByText(MasterEnum.Departments.PreSales.ToString()).Selected = true;
                    //ddlDepartment.Enabled = false;

                    RemoveDepartmentValuesForPresalesUser();
                    //FillRoleDropDown(int.Parse(ddlDepartment.SelectedItem.Value));                    

                }

                //If user is HR than Department is "HR"
                if (objParameter.RoleHR == AuthorizationManagerConstants.ROLEHR)
                {
                    ddlDepartment.ClearSelection();
                    ddlDepartment.Items.FindByText(MasterEnum.Departments.HR.ToString()).Selected = true;
                    ddlDepartment.Enabled = false;

                    ddlRole.Enabled = true;
                    FillRoleDropDown(int.Parse(ddlDepartment.SelectedItem.Value));

                }

                //If user is Testing than Department is "Testing"
                if (objParameter.RoleTesting == AuthorizationManagerConstants.ROLETESTING)
                {
                    ddlDepartment.ClearSelection();
                    ddlDepartment.Items.FindByText(MasterEnum.Departments.Testing.ToString()).Selected = true;
                    ddlDepartment.Enabled = false;

                    ddlRole.Enabled = true;
                    FillRoleDropDown(int.Parse(ddlDepartment.SelectedItem.Value));

                }

                //If user is Quality than Department is "PMOQuality"
                if (objParameter.RoleQuality == AuthorizationManagerConstants.ROLEQUALITY)
                {
                    ddlDepartment.ClearSelection();
                    ddlDepartment.Items.FindByText(MasterEnum.Departments.PMOQuality.ToString()).Selected = true;
                    ddlDepartment.Enabled = false;

                    ddlRole.Enabled = true;
                    FillRoleDropDown(int.Parse(ddlDepartment.SelectedItem.Value));

                }

                //If user is Marketing than Department is "Marketing"
                if (objParameter.RoleMarketing == AuthorizationManagerConstants.ROLEMH)
                {
                    ddlDepartment.ClearSelection();
                    ddlDepartment.Items.FindByText(MasterEnum.Departments.Marketing.ToString()).Selected = true;
                    ddlDepartment.Enabled = false;

                    ddlRole.Enabled = true;
                    FillRoleDropDown(int.Parse(ddlDepartment.SelectedItem.Value));

                }
                //If user is rave consultant than Department is "rave consultant"
                if (objParameter.RoleRaveConsultant == AuthorizationManagerConstants.ROLERAVECONSULTANT)
                {
                    ddlDepartment.ClearSelection();
                    ddlDepartment.SelectedValue = Convert.ToInt16(MasterEnum.Departments.RaveConsultant_India).ToString();
                    //ddlDepartment.Items.FindByText(MasterEnum.Departments.RaveConsultant_India.ToString()).Selected = true;
                    ddlDepartment.Enabled = false;

                    ddlRole.Enabled = true;
                    FillRoleDropDown(int.Parse(ddlDepartment.SelectedItem.Value));

                }
            }

        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "FillDropDowns", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
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

            if (raveHRCollection != null)
            {
                // Assign the data source to dropdown
                ddlProjectName.DataSource = raveHRCollection;

                ddlProjectName.DataTextField = CommonConstants.DDL_DataTextField;
                ddlProjectName.DataValueField = CommonConstants.DDL_DataValueField;

                // Bind the data to dropdown
                ddlProjectName.DataBind();

                // Default value of dropdown is "Select"
                ddlProjectName.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "FillProjectNameDropDown", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    ///  Fills the Role dropdown
    /// </summary>
    private void FillRoleDropDown(int DepartmentId)
    {
        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        // Initialise Business layer object
        Rave.HR.BusinessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.BusinessLayer.MRF.MRFDetail();

        try
        {
            // Call the Business layer method
            raveHRCollection = mRFDetail.GetRoleDepartmentWiseBL(DepartmentId);

            if (raveHRCollection != null)
            {
                // Assign the data source to dropdown
                ddlRole.DataSource = raveHRCollection;

                ddlRole.DataTextField = CommonConstants.DDL_DataTextField;
                ddlRole.DataValueField = CommonConstants.DDL_DataValueField;

                // Bind the data to dropdown
                ddlRole.DataBind();

                // Default value of dropdown is "Select"
                ddlRole.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "FillRoleDropDown", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    ///  Fills the Status dropdown
    /// </summary>
    private void FillStatusDropDown()
    {
        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        // Initialise Business layer object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        try
        {
            // Call the Business layer method
            raveHRCollection = master.FillDropDownsBL(Convert.ToInt32(EnumsConstants.Category.MRFStatus));

            if (raveHRCollection != null)
            {
                // Assign the data source to dropdown
                ddlStatus.DataSource = raveHRCollection;

                ddlStatus.DataTextField = CommonConstants.DDL_DataTextField;
                ddlStatus.DataValueField = CommonConstants.DDL_DataValueField;

                // Bind the data to dropdown
                ddlStatus.DataBind();

                // Default value of dropdown is "Select"
                ddlStatus.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "FillStatusDropDown", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    ///  Fills the Department dropdown
    /// </summary>
    private void FillDepartmentDropDown()
    {
        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        // Initialise Business layer object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        try
        {
            // Call the Business layer method
            raveHRCollection = master.FillDepartmentDropDownBL();

            if (raveHRCollection != null)
            {
                // Assign the data source to dropdown
                ddlDepartment.DataSource = raveHRCollection;

                ddlDepartment.DataTextField = CommonConstants.DDL_DataTextField;
                ddlDepartment.DataValueField = CommonConstants.DDL_DataValueField;

                // Bind the data to dropdown
                ddlDepartment.DataBind();

                // Default value of dropdown is "Select"
                ddlDepartment.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "FillDepartmentDropDown", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Binds the List Of MRF Grid
    /// </summary>
    private void BindGridMrfSummary()
    {
        try
        {
            // By default sortDirection is Ascending
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                // Sort GridView in Ascending direction
                SortGridView(sortExpression, ASCENDING);
            }
            else
            {
                // Sort GridView in Descending direction
                SortGridView(sortExpression, DESCENDING);
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "BindGridMrfSummary", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Get or set the GridViewSortDirection property
    /// </summary>
    private SortDirection GridViewSortDirection
    {
        get
        {
            if (Session[SessionNames.SORT_DIRECTION_MRF] == null)
                Session[SessionNames.SORT_DIRECTION_MRF] = SortDirection.Ascending;

            return (SortDirection)Session[SessionNames.SORT_DIRECTION_MRF];
        }

        set
        {
            Session[SessionNames.SORT_DIRECTION_MRF] = value;
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
            if (sortExpression == CommonConstants.MRF_CODE)
            {
                objParameter.SortExpressionAndDirection = sortExpression + direction;
            }
            else
            {
                objParameter.SortExpressionAndDirection = sortExpression + direction + "," + CommonConstants.MRF_CODE + direction;
            }

            //if (gvListOfMrf.BottomPagerRow != null)
            //{
            //    GridViewRow gvrBottomPager = gvListOfMrf.BottomPagerRow;
            //    TextBox txtPages = (TextBox)gvrBottomPager.Cells[0].FindControl("txtPages");
            //    if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0)
            //    {
            //objParameter.PageNumber = int.Parse(txtPages.Text);
            objParameter.PageNumber = Convert.ToInt32(Session[SessionNames.CURRENT_PAGE_INDEX_MRF].ToString());
            objParameter.PageSize = 10;
            //Ishwar Patil 29092014 For NIS : Start
            objParameter.IsRMSMRF = RBRMSMRF.SelectedValue;
            //Ishwar Patil 29092014 For NIS : End

            //    }
            //}
            //else
            //{
            //    //objParameter.PageNumber = 1;
            //    objParameter.PageNumber = Convert.ToInt32(Session[SessionNames.CURRENT_PAGE_INDEX_MRF].ToString());
            //    objParameter.PageSize = 11;
            //}
            raveHRCollection = GetMrfSummary();

            if ((int.Parse(Session[SessionNames.PAGE_COUNT_MRF].ToString()) == 1) && (raveHRCollection.Count == 1))
            {
                gvListOfMrf.AllowSorting = false;
            }
            else
            {
                gvListOfMrf.AllowSorting = true;
            }

            if (raveHRCollection.Count == 0)
            {
                gvListOfMrf.DataSource = raveHRCollection;
                gvListOfMrf.DataBind();

                ShowHeaderWhenEmptyGrid(raveHRCollection);
            }
            else
            {
                if (Session[SessionNames.MRF_STATUS_ID] != null)
                {
                    if (int.Parse(Session[SessionNames.MRF_STATUS_ID].ToString()) == Convert.ToInt32(MasterEnum.MRFStatus.Abort))
                    {
                        gvListOfMrf.Columns[11].HeaderText = "Aborted Date";
                        gvListOfMrf.Columns[11].Visible = true;
                        gvListOfMrf.Columns[12].Visible = false;
                    }
                    else if (int.Parse(Session[SessionNames.MRF_STATUS_ID].ToString()) == Convert.ToInt32(MasterEnum.MRFStatus.Closed))
                    {
                        gvListOfMrf.Columns[11].HeaderText = "Closed Date";
                        gvListOfMrf.Columns[12].HeaderText = "Resource Allocated Date";
                        gvListOfMrf.Columns[11].Visible = true;
                        gvListOfMrf.Columns[12].Visible = true;  
                    }
                }
                else
                {
                    gvListOfMrf.Columns[11].Visible = false;
                    gvListOfMrf.Columns[12].Visible = false;                    
                }
                gvListOfMrf.DataSource = raveHRCollection;
                gvListOfMrf.DataBind();

            }

            //Finds the Paging Text Box, Previous and Next Link Buttons in Grid Views Pagning Templet
            GridViewRow gvrPager = gvListOfMrf.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            LinkButton lbtnPrevious = (LinkButton)gvrPager.Cells[0].FindControl("lbtnPrevious");
            LinkButton lbtnNext = (LinkButton)gvrPager.Cells[0].FindControl("lbtnNext");

            if (pageCount > 1)
            {
                gvListOfMrf.BottomPagerRow.Visible = true;
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

            if (raveHRCollection.Count <= 1)
            {
                btnExportToExcel.Visible = false;
            }
            else
            {
                btnExportToExcel.Visible = true;
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "SortGridView", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Gets the MRF Summary on List of MRF grid
    /// </summary>
    /// <returns>Collection</returns>
    private BusinessEntities.RaveHRCollection GetMrfSummary()
    {
        Rave.HR.BusinessLayer.MRF.MRFDetail mrfSummaryBL = new Rave.HR.BusinessLayer.MRF.MRFDetail();
        BusinessEntities.MRFDetail mrfDetail = new BusinessEntities.MRFDetail();
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
        


        try
        {


            if (Session[SessionNames.PROJECT_ID_MRF] == null || Session[SessionNames.PROJECT_ID_MRF].ToString() == CommonConstants.SELECT)
            {
                mrfDetail.ProjectId = 0;
            }
            else
            {
                mrfDetail.ProjectId = int.Parse(Session[SessionNames.PROJECT_ID_MRF].ToString());
            }
            if (Session[SessionNames.ROLE] == null || Session[SessionNames.ROLE].ToString() == CommonConstants.SELECT)
            {
                mrfDetail.RoleId = 0;
            }
            else
            {
                mrfDetail.RoleId = int.Parse(Session[SessionNames.ROLE].ToString());
            }
            if (Session[SessionNames.MRF_STATUS_ID] == null || Session[SessionNames.MRF_STATUS_ID].ToString() == CommonConstants.SELECT)
            {
                mrfDetail.StatusId = 0;
            }
            else
            {
                mrfDetail.StatusId = int.Parse(Session[SessionNames.MRF_STATUS_ID].ToString());
            }
            if (Session[SessionNames.DEPARTMENT_ID] == null || Session[SessionNames.DEPARTMENT_ID].ToString() == CommonConstants.SELECT)
            {
                mrfDetail.DepartmentId = 0;
            }
            else
            {
                mrfDetail.DepartmentId = int.Parse(Session[SessionNames.DEPARTMENT_ID].ToString());
            }

            if (Session[SessionNames.PROJECT_ID_MRF] == null && Session[SessionNames.ROLE] == null && Session[SessionNames.MRF_STATUS_ID] == null && Session[SessionNames.DEPARTMENT_ID] == null)
            {
                // Method for Pageload
                raveHRCollection = mrfSummaryBL.GetMrfSummaryForPageLoad(objParameter, ref pageCount);

                // 27633-ambar-start
                objParameter.PageSize = 10000;
                int temppagenumber = 0;
                temppagenumber = objParameter.PageNumber;
                objParameter.PageNumber = 1;
                raveHRpreviousCollection = mrfSummaryBL.GetMrfSummaryForPageLoad(objParameter, ref IntHashpageCount);
                objParameter.PageSize = 10;
                objParameter.PageNumber = temppagenumber;
                int k = 0;
                foreach (BusinessEntities.MRFDetail collectionprevious in raveHRpreviousCollection)
                {
                    temppreviousHashTable.Add(k, collectionprevious.MRFId.ToString());
                    k++;
                }
                Session[SessionNames.MRFPreviousHashTable] = temppreviousHashTable;
                // 27633-ambar-End
            }
            else
            {
                // Method for Filter
                raveHRCollection = mrfSummaryBL.GetMrfSummary(objParameter, mrfDetail, ref pageCount);
                
                // 27633-ambar-start
                objParameter.PageSize = 10000;
                int temppagenumber = 0;
                temppagenumber = objParameter.PageNumber;
                objParameter.PageNumber = 1;
                raveHRpreviousCollection = mrfSummaryBL.GetMrfSummary(objParameter, mrfDetail, ref IntHashpageCount);
                objParameter.PageSize = 10;
                objParameter.PageNumber = temppagenumber;
                int k = 0;
                foreach (BusinessEntities.MRFDetail collectionprevious in raveHRpreviousCollection)
                {
                    temppreviousHashTable.Add(k, collectionprevious.MRFId.ToString());
                    k++;
                }
                Session[SessionNames.MRFPreviousHashTable] = temppreviousHashTable;
                // 27633-ambar-End
            }

            Session[SessionNames.PAGE_COUNT_MRF] = pageCount;

        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "GetMrfSummary", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }

        return raveHRCollection;
    }

    /// <summary>
    /// Display Header When GridView Empty with proper message
    /// </summary>
    /// <param name="raveHRCollection">EmptyCollection</param>
    private void ShowHeaderWhenEmptyGrid(BusinessEntities.RaveHRCollection raveHRCollection)
    {
        try
        {
            //set header visible
            gvListOfMrf.ShowHeader = true;
            // Disable sorting
            gvListOfMrf.AllowSorting = false;

            //Create empty datasource for Grid view and bind
            raveHRCollection.Add(new BusinessEntities.MRFDetail());
            gvListOfMrf.DataSource = raveHRCollection;
            gvListOfMrf.DataBind();

            //clear all the cells in the row
            gvListOfMrf.Rows[0].Cells.Clear();

            //add a new blank cell
            gvListOfMrf.Rows[0].Cells.Add(new TableCell());
            gvListOfMrf.Rows[0].Cells[0].Text = CommonConstants.NO_RECORDS_FOUND_MESSAGE;
            gvListOfMrf.Rows[0].Cells[0].Wrap = false;
            gvListOfMrf.Rows[0].Cells[0].Width = Unit.Percentage(11);

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "ShowHeaderWhenEmptyGrid", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
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
                    case CommonConstants.MRF_CODE:
                        headerRow.Cells[1].Controls.Add(sortImage);
                        break;

                    //case CommonConstants.RP_CODE:
                    //    headerRow.Cells[2].Controls.Add(sortImage);
                    //    break;

                    case CommonConstants.ROLE:
                        headerRow.Cells[2].Controls.Add(sortImage);
                        break;

                    case CommonConstants.CLIENT_NAME:
                        headerRow.Cells[3].Controls.Add(sortImage);
                        break;

                    case CommonConstants.PROJECT_NAME:
                        headerRow.Cells[4].Controls.Add(sortImage);
                        break;

                    case CommonConstants.RESOURCE_ON_BOARD:
                        headerRow.Cells[5].Controls.Add(sortImage);
                        break;

                    case CommonConstants.EXPECTED_CLOSURE_DATE:
                        headerRow.Cells[6].Controls.Add(sortImage);
                        break;

                    //case CommonConstants.RAISED_BY:
                    //    headerRow.Cells[8].Controls.Add(sortImage);
                    //    break;

                    case CommonConstants.STATUS:
                        headerRow.Cells[7].Controls.Add(sortImage);
                        break;

                    case CommonConstants.DEPARTMENT_NAME:
                        headerRow.Cells[8].Controls.Add(sortImage);
                        break;

                    case CommonConstants.RECRUITMENT_MANAGER:
                        headerRow.Cells[9].Controls.Add(sortImage);
                        break;

                    case CommonConstants.RECRUITMENTASSIGNDATE:
                        headerRow.Cells[10].Controls.Add(sortImage);
                        break;

                    case CommonConstants.ABORTEDORCLOSEDDATE:
                        headerRow.Cells[11].Controls.Add(sortImage);
                        break;

                    case CommonConstants.ALLOCATIONDATE:
                        headerRow.Cells[12].Controls.Add(sortImage);
                        break;

                    case CommonConstants.EMPLOYEE_NAME:
                        headerRow.Cells[13].Controls.Add(sortImage);
                        break;
                }
            }
        }

        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "AddSortImage", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Based on the Department selected, fill the Role 
    /// </summary>
    private void FillRoleDropdownAsPerDepartment()
    {
        try
        {
            ddlRole.Enabled = true;

            if (int.Parse(ddlDepartment.SelectedValue) != Convert.ToInt32(MasterEnum.Departments.Projects))
            {
                FillRoleDropDown(Convert.ToInt32(ddlDepartment.SelectedItem.Value));

                ddlProjectName.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
                ddlProjectName.Enabled = false;

            }
            else
            {
                FillRoleDropDown(Convert.ToInt32(ddlDepartment.SelectedItem.Value));
                FillProjectNameDropDown();
                ddlProjectName.Enabled = true;
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "FillRoleDropdownAsPerDepartment", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Fills the project dropdown as per Projects alloted to PM
    /// </summary>
    private void FillProjectNameDropdownForPM()
    {
        try
        {
            AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
            string UserMailId = objRaveHRAuthorizationManager.getLoggedInUserEmailId();

            //Initialise the ParameterCriteria object
            BusinessEntities.ParameterCriteria parameterCriteria = new BusinessEntities.ParameterCriteria();
            // Initialise Business layer object
            Rave.HR.BusinessLayer.MRF.MRFDetail mrfProjectName = new Rave.HR.BusinessLayer.MRF.MRFDetail();

            parameterCriteria.EMailID = UserMailId;//"gaurav.thakkar@rave-tech.com";
            if (objParameter.RolePM != null || objParameter.RoleAPM != null || objParameter.RoleGPM != null)
            {
                parameterCriteria.RoleRPM = AuthorizationManagerConstants.ROLEPROJECTMANAGER;
            }
            //parameterCriteria.EMailID = "gaurav.thakkar@rave-tech.com";

            //Calling Fill dropdown Business layer method to fill the dropdown
            raveHRCollection = mrfProjectName.GetProjectNameRoleWiseBL(parameterCriteria);

            //Check Collection object is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSource to dropdown
                ddlProjectName.DataSource = raveHRCollection;

                //Assign DataText Field to dropdown
                ddlProjectName.DataTextField = DbTableColumn.ProjectName;

                //Assign Data Value field to dropdown
                ddlProjectName.DataValueField = DbTableColumn.ProjectId;

                //Bind Dropdown
                ddlProjectName.DataBind();

                //Insert Select as a Item for Dropdown
                ddlProjectName.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "FillProjectNameDropdownForPM", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Sets the default value "Select" of all the dropdown in filter panel
    /// </summary>
    private void ClearFilteringFields()
    {
        try
        {
            //Check if user is not RPM (or COO or CEO)
            if (objParameter.RoleRPM != AuthorizationManagerConstants.ROLERPM)
            {
                //If User is PM than By default Department is "Projects" and is disabled.
                //ProjectName dropdown will only have those projects which are alloted to PM
                if (objParameter.RolePM == AuthorizationManagerConstants.ROLEPM)
                {
                    ddlDepartment.Items.FindByText(MasterEnum.Departments.Projects.ToString()).Selected = true;
                    ddlDepartment.Enabled = false;

                    ddlProjectName.SelectedIndex = CommonConstants.ZERO;
                    ddlRole.SelectedIndex = CommonConstants.ZERO;
                    ddlStatus.SelectedIndex = CommonConstants.ZERO;

                }

                //If user is CFM/FM than Department is "Finance"
                if (objParameter.RoleCFM == AuthorizationManagerConstants.ROLECFM)
                {
                    ddlDepartment.Items.FindByText(MasterEnum.Departments.Finance.ToString()).Selected = true;
                    ddlDepartment.Enabled = false;

                    ddlProjectName.SelectedIndex = CommonConstants.ZERO;
                    ddlProjectName.Enabled = false;
                    ddlRole.SelectedIndex = CommonConstants.ZERO;
                    ddlStatus.SelectedIndex = CommonConstants.ZERO;

                }

                //If user is HR than Status selected is "Pending External Allocation"
                if (objParameter.RoleRecruitment == AuthorizationManagerConstants.ROLERECRUITMENT)
                {
                    ddlStatus.Items.FindByText(CommonConstants.MRFStatus_PendingExternalAllocation).Selected = true;
                    ddlStatus.Enabled = false;

                    ddlDepartment.SelectedIndex = CommonConstants.ZERO;
                    ddlProjectName.SelectedIndex = CommonConstants.ZERO;
                    ddlProjectName.Enabled = false;
                    ddlRole.SelectedIndex = CommonConstants.ZERO;
                }

                //If user is PreSales than the departments enabled are "Projects" and "PreSales"
                if (objParameter.RolePreSales == AuthorizationManagerConstants.ROLEPRESALES)
                {
                    //ddlDepartment.Items.FindByText(MasterEnum.Departments.PreSales.ToString()).Enabled = true;
                    //ddlDepartment.Enabled = false;
                    RemoveDepartmentValuesForPresalesUser();
                    ddlDepartment.SelectedIndex = CommonConstants.ZERO;
                    ddlProjectName.SelectedIndex = CommonConstants.ZERO;
                    ddlProjectName.Enabled = false;
                    ddlRole.SelectedIndex = CommonConstants.ZERO;
                    ddlRole.Enabled = false;
                    ddlStatus.SelectedIndex = CommonConstants.ZERO;
                }

                //If user is HR than Department is "HR"
                if (objParameter.RoleHR == AuthorizationManagerConstants.ROLEHR)
                {
                    ddlDepartment.Items.FindByText(MasterEnum.Departments.HR.ToString()).Selected = true;
                    ddlDepartment.Enabled = false;

                    ddlProjectName.SelectedIndex = CommonConstants.ZERO;
                    ddlProjectName.Enabled = false;
                    ddlRole.SelectedIndex = CommonConstants.ZERO;
                    ddlStatus.SelectedIndex = CommonConstants.ZERO;

                }

                //If user is Testingthan Department is "Testing"
                if (objParameter.RoleTesting == AuthorizationManagerConstants.ROLETESTING)
                {
                    ddlDepartment.Items.FindByText(MasterEnum.Departments.Testing.ToString()).Selected = true;
                    ddlDepartment.Enabled = false;

                    ddlProjectName.SelectedIndex = CommonConstants.ZERO;
                    ddlProjectName.Enabled = false;
                    ddlRole.SelectedIndex = CommonConstants.ZERO;
                    ddlStatus.SelectedIndex = CommonConstants.ZERO;

                }

                //If user is Quality than Department is "PMOQuality"
                if (objParameter.RoleQuality == AuthorizationManagerConstants.ROLEQUALITY)
                {
                    ddlDepartment.Items.FindByText(MasterEnum.Departments.PMOQuality.ToString()).Selected = true;
                    ddlDepartment.Enabled = false;

                    ddlProjectName.SelectedIndex = CommonConstants.ZERO;
                    ddlProjectName.Enabled = false;
                    ddlRole.SelectedIndex = CommonConstants.ZERO;
                    ddlStatus.SelectedIndex = CommonConstants.ZERO;

                }

                //If user is Marketing than Department is "Marketing"
                if (objParameter.RoleMarketing == AuthorizationManagerConstants.ROLEMH)
                {
                    ddlDepartment.Items.FindByText(MasterEnum.Departments.Marketing.ToString()).Selected = true;
                    ddlDepartment.Enabled = false;

                    ddlProjectName.SelectedIndex = CommonConstants.ZERO;
                    ddlProjectName.Enabled = false;
                    ddlRole.SelectedIndex = CommonConstants.ZERO;
                    ddlStatus.SelectedIndex = CommonConstants.ZERO;

                }
            }
            else
            {
                ddlDepartment.SelectedIndex = CommonConstants.ZERO;
                ddlProjectName.SelectedIndex = CommonConstants.ZERO;
                ddlRole.SelectedIndex = CommonConstants.ZERO;
                ddlStatus.SelectedIndex = CommonConstants.ZERO;

                //On pageload or remove filter these dropdown are disabled.
                ddlProjectName.Enabled = false;
                ddlRole.Enabled = false;
            }


        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "ClearFilteringFields", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Gets the Role for Logged in User
    /// </summary>
    private void GetRolesforUser()
    {
        try
        {
            AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
            objParameter.EMailID = objRaveHRAuthorizationManager.getLoggedInUserEmailId();

            // Get the roles of user which is stored in the session variable

            //--Add to session 
            Session[SessionNames.AZMAN_ROLES] = objRaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser()); ;
            rolesForUser = (ArrayList)Session[SessionNames.AZMAN_ROLES];

            //Parse through the array and assign individual role to object
            foreach (string STR in rolesForUser)
            {
                switch (STR)
                {
                    case AuthorizationManagerConstants.ROLERPM:
                        objParameter.RoleRPM = AuthorizationManagerConstants.ROLERPM;
                        break;

                    case AuthorizationManagerConstants.ROLECEO:
                        objParameter.RoleCEO = AuthorizationManagerConstants.ROLECEO;
                        break;

                    case AuthorizationManagerConstants.ROLECOO:
                        objParameter.RoleCOO = AuthorizationManagerConstants.ROLECOO;
                        break;

                    case AuthorizationManagerConstants.ROLECFM:
                        objParameter.RoleCFM = AuthorizationManagerConstants.ROLECFM;
                        break;

                    case AuthorizationManagerConstants.ROLEFM:
                        objParameter.RoleFM = AuthorizationManagerConstants.ROLEFM;
                        break;

                    case AuthorizationManagerConstants.ROLEPRESALES:
                        objParameter.RolePreSales = AuthorizationManagerConstants.ROLEPRESALES;
                        break;

                    case AuthorizationManagerConstants.ROLEPROJECTMANAGER:
                        objParameter.RolePM = AuthorizationManagerConstants.ROLEPROJECTMANAGER;
                        break;

                    case AuthorizationManagerConstants.ROLEAPM:
                        objParameter.RoleAPM = AuthorizationManagerConstants.ROLEAPM;
                        break;

                    case AuthorizationManagerConstants.ROLEGPM:
                        objParameter.RoleGPM = AuthorizationManagerConstants.ROLEGPM;
                        break;

                    /* Changed by Gaurav, As discussed with Megha.. All rights of HR is give to Recruitment*/
                    case AuthorizationManagerConstants.ROLERECRUITMENT:
                        objParameter.RoleRecruitment = AuthorizationManagerConstants.ROLERECRUITMENT;
                        break;

                    case AuthorizationManagerConstants.ROLEHR:
                        objParameter.RoleHR = AuthorizationManagerConstants.ROLEHR;
                        break;

                    case AuthorizationManagerConstants.ROLETESTING:
                        objParameter.RoleTesting = AuthorizationManagerConstants.ROLETESTING;
                        break;

                    case AuthorizationManagerConstants.ROLEQUALITY:
                        objParameter.RoleQuality = AuthorizationManagerConstants.ROLEQUALITY;
                        break;

                    case AuthorizationManagerConstants.ROLEMH:
                        objParameter.RoleMarketing = AuthorizationManagerConstants.ROLEMH;
                        break;

                    case AuthorizationManagerConstants.ROLERAVECONSULTANT:
                        objParameter.RoleRaveConsultant = AuthorizationManagerConstants.ROLERAVECONSULTANT;
                        break;
                    default:
                        break;
                }
            }

            //Assigning 3 roles to 1 role since all 3 have same rights.
            if ((objParameter.RoleRPM == AuthorizationManagerConstants.ROLERPM) || (objParameter.RoleCEO == AuthorizationManagerConstants.ROLECEO) || (objParameter.RoleCOO == AuthorizationManagerConstants.ROLECOO))
            {
                objParameter.RoleRPM = AuthorizationManagerConstants.ROLERPM;
                objParameter.RoleCEO = AuthorizationManagerConstants.ROLERPM;
                objParameter.RoleCOO = AuthorizationManagerConstants.ROLERPM;
            }

            //Assigning 2 roles to 1 role since all 3 have same rights.
            if ((objParameter.RoleCFM == AuthorizationManagerConstants.ROLECFM) || (objParameter.RoleFM == AuthorizationManagerConstants.ROLEFM))
            {
                objParameter.RoleCFM = AuthorizationManagerConstants.ROLECFM;
                objParameter.RoleFM = AuthorizationManagerConstants.ROLECFM;
            }

            //Assigning 3 roles to 1 role since all 3 have same rights.
            if ((objParameter.RolePM == AuthorizationManagerConstants.ROLEPROJECTMANAGER) || (objParameter.RoleAPM == AuthorizationManagerConstants.ROLEAPM) || (objParameter.RoleGPM == AuthorizationManagerConstants.ROLEGPM))
            {
                objParameter.RolePM = AuthorizationManagerConstants.ROLEPROJECTMANAGER;
                objParameter.RoleAPM = AuthorizationManagerConstants.ROLEPROJECTMANAGER;
                objParameter.RoleGPM = AuthorizationManagerConstants.ROLEPROJECTMANAGER;
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "GetRolesforUser", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    private bool IsDepartmentHead()
    {
        //Create Businees Object of MRF
        Rave.HR.BusinessLayer.MRF.MRFDetail objDetails = new Rave.HR.BusinessLayer.MRF.MRFDetail();

        //create Collection Object
        raveHRCollection = new RaveHRCollection();

        //create AuthorizationManager Object
        objAuMan = new AuthorizationManager();

        try
        {

            //Get Roles of Loged Users
            arrRolesForUser = objAuMan.getRolesForUser(objAuMan.getLoggedInUser());

            //Method to Get DepartmentHead By DeptId
            raveHRCollection = objDetails.GetDepartmentHeadByEmaiIdAndDeptId(objAuMan.getLoggedInUserEmailId());

            if (raveHRCollection.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "IsDepartmentHead", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }   

    /// <summary>
    /// Disables the required fields for presales role
    /// </summary>
    private void RemoveDepartmentValuesForPresalesUser()
    {
        try
        {
            ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.Admin.ToString(), Convert.ToInt32(MasterEnum.Departments.Admin).ToString()));
            ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.Finance.ToString(), Convert.ToInt32(MasterEnum.Departments.Finance).ToString()));
            ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.HR.ToString(), Convert.ToInt32(MasterEnum.Departments.HR).ToString()));
            ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.ITS.ToString(), Convert.ToInt32(MasterEnum.Departments.ITS).ToString()));
            ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.Marketing.ToString(), Convert.ToInt32(MasterEnum.Departments.Marketing).ToString()));
            ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.PMOQuality.ToString(), Convert.ToInt32(MasterEnum.Departments.PMOQuality).ToString()));
            ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.Projects.ToString(), Convert.ToInt32(MasterEnum.Departments.Projects).ToString()));
            ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.RaveDevelopment.ToString(), Convert.ToInt32(MasterEnum.Departments.RaveDevelopment).ToString()));
            ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.Recruitment.ToString(), Convert.ToInt32(MasterEnum.Departments.Recruitment).ToString()));
            ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.Support.ToString(), Convert.ToInt32(MasterEnum.Departments.Support).ToString()));
            ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.Testing.ToString(), Convert.ToInt32(MasterEnum.Departments.Testing).ToString()));
            ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.BA.ToString(), Convert.ToInt32(MasterEnum.Departments.BA).ToString()));
            ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.Usability.ToString(), Convert.ToInt32(MasterEnum.Departments.Usability).ToString()));
            ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.ATG.ToString(), Convert.ToInt32(MasterEnum.Departments.ATG).ToString()));
            ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.RPG.ToString(), Convert.ToInt32(MasterEnum.Departments.RPG).ToString()));
        }

         //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "RemoveDepartmentValuesForPresalesUser", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Export data to excel.
    /// </summary>
    /// <param name="raveHRCollection"></param>
    private void ExportToExcel(BusinessEntities.RaveHRCollection raveHRCollection)
    {

        try
        {
            //define the label to dispaly header text.
            Label titleLbl = new Label();
            titleLbl.Text = "MRF Summary Report On " + DateTime.Now.ToString();

            //make bold to header text.
            titleLbl.Font.Bold = true;

            GridView gv = new GridView();
            gv.DataSource = GetRequiredColumnOfCollection(raveHRCollection);
            gv.DataBind();

            //give name to excel sheet.
            string attachment = "attachment; filename=MRFSummaryReport.xls";
            Response.ClearContent();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", attachment);

            // Remove the charset from the Content-Type header.
            Response.Charset = string.Empty;
            // Turn off the view state.
            this.EnableViewState = false;

            StringWriter posSW = new StringWriter();
            HtmlTextWriter posHW = new HtmlTextWriter(posSW);

            //' Get the HTML for the header control.
            titleLbl.RenderControl(posHW);
            //' Get the HTML for the control.
            gv.RenderControl(posHW);

            //' Write the HTML back to the browser.
            Response.Write(posSW.ToString());

            //' End the response.
            Response.End();
        }
        catch (System.Threading.ThreadAbortException ex) { }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFSUMMARY, "ExportToExcel", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Get The only required column to export to excel details.
    /// </summary>
    /// <param name="raveHRCollection"></param>
    /// <returns></returns>
    private DataTable GetRequiredColumnOfCollection(BusinessEntities.RaveHRCollection raveHRCollection)
    {
        DataTable dt = new DataTable();
        //dt.Columns.Add(DbTableColumn.MRFID);
        dt.Columns.Add(DbTableColumn.MRFCode);
        //dt.Columns.Add(DbTableColumn.RPCode);
        dt.Columns.Add(DbTableColumn.Role);
        dt.Columns.Add(DbTableColumn.ClientName);
        dt.Columns.Add(DbTableColumn.ProjectName);
        // Mohamed : Issue 41902 : 24/04/2013 : Starts                        			  
        // Desc : Mrf Summary - In export to excel heading is deiplayed as "ResourceOnBoard" and it should be changed to "RequiredForm"
        //dt.Columns.Add(DbTableColumn.ResourceOnBoard);
        dt.Columns.Add(DbTableColumn.RequestFrom);
        // Mohamed : Issue 41902 : 24/04/2013 : Ends
        dt.Columns.Add(DbTableColumn.Status);
        dt.Columns.Add(DbTableColumn.Department);
        dt.Columns.Add(DbTableColumn.EmployeeName);

        // venkatesh  : Issue 46380 : 19/11/2013 : Starts
        // Desc : Mrf Summary - In export to excel "
        dt.Columns.Add(DbTableColumn.JoiningDate);
        dt.Columns.Add(DbTableColumn.Designation);
        // venkatesh  : Issue 46380 : 19/11/2013 : End
        

        dt.Columns.Add(DbTableColumn.RecruitmentManager);
        // 29173-Ambar-Start-05092011
        //dt.Columns.Add(DbTableColumn.TypeOFMRF);
        // 29173-Ambar-End-05092011

        //Rajnikant: Issue 45708 : 18/09/2014 : Starts                        			  
        //Desc :  Get Skill of MRF
        dt.Columns.Add(DbTableColumn.Skill);
        //Rajnikant: Issue 45708 : 18/09/2014 : Ends

        foreach (BusinessEntities.MRFDetail mrfDetails in raveHRCollection)
        {
            DataRow dr = dt.NewRow();

            //dr[DbTableColumn.MRFID] = mrfDetails.MRFId;

            dr[DbTableColumn.MRFCode] = mrfDetails.MRFCode;

           //dr[DbTableColumn.RPCode] = mrfDetails.RPCode;

            dr[DbTableColumn.Role] = mrfDetails.Role;

            dr[DbTableColumn.ClientName] = mrfDetails.ClientName;

            dr[DbTableColumn.ProjectName] = mrfDetails.ProjectName;

            // Mohamed : Issue 41902 : 24/04/2013 : Starts                        			  
            // Desc : Mrf Summary - In export to excel heading is deiplayed as "ResourceOnBoard" and it should be changed to "RequiredForm"
            //dr[DbTableColumn.ResourceOnBoard] = mrfDetails.ResourceOnBoard;
            dr[DbTableColumn.RequestFrom] = mrfDetails.ResourceOnBoard;
            // Mohamed : Issue 41902 : 24/04/2013 : Ends

            //dr[DbTableColumn.ExpectedClosureDate] = objMrfDetail.ExpectedClosureDate;

            //dr[DbTableColumn.RaisedBy]= objMrfDetail.RaisedBy;

            dr[DbTableColumn.Status] = mrfDetails.Status;

            dr[DbTableColumn.Department] = mrfDetails.DepartmentName;

            dr[DbTableColumn.EmployeeName] = mrfDetails.EmployeeName;

            // venkatesh  : Issue 46380 : 19/11/2013 : Starts
            // Desc : Mrf Summary - In export to excel "
            dr[DbTableColumn.JoiningDate] = mrfDetails.DateOfJoining;
            dr[DbTableColumn.Designation] = mrfDetails.Designation;
            // venkatesh  : Issue 46380 : 19/11/2013 : End

            dr[DbTableColumn.RecruitmentManager] = mrfDetails.RecruitmentManager;

            // 29173-Ambar-Start-05092011
            //dr[DbTableColumn.TypeOFMRF] = mrfDetails.TypeOFMRF;//31759-Subhra-Changed the data table column name from MRFType toTypeOfMRF
            // 29173-Ambar-End-05092011

            //Rajnikant: Issue 45708 : 18/09/2014 : Starts                        			  
            //Desc :  Get Skill of MRF
            dr[DbTableColumn.Skill] = mrfDetails.Skill;
            //Rajnikant: Issue 45708 : 18/09/2014 : End

            dt.Rows.Add(dr);
        }

        return dt;
    }


  #endregion Private Method

  // 24736-Ambar-Start
  // Function to fill drop downs as per role
  public void filldropdownasperrole()
  {    
    if (objParameter.RoleRPM == AuthorizationManagerConstants.ROLERPM)
    {
      ddlRole.Enabled = true;
        //31933 -Subhra - Start
      if ( ddlDepartment.SelectedItem.Value.ToUpper() != "SELECT" )
          //31933 -Subhra - End
      FillRoleDropDown(int.Parse(ddlDepartment.SelectedItem.Value));

      //FillRoleDropDown(ddlDepartment.SelectedIndex);

      if (ddlDepartment.SelectedItem.Text == MasterEnum.Departments.Projects.ToString())
      {
        ddlProjectName.Enabled = true;
        FillProjectNameDropDown();
      }
    }
    else
    {
      //If User is PM than By default Department is "Projects" and is disabled.
      //ProjectName dropdown will only have those projects which are alloted to PM
      if (objParameter.RolePM == AuthorizationManagerConstants.ROLEPM || objParameter.RoleGPM == AuthorizationManagerConstants.ROLEPM || objParameter.RoleAPM == AuthorizationManagerConstants.ROLEPM)
      {
        ddlDepartment.ClearSelection();
        ddlDepartment.Items.FindByText(MasterEnum.Departments.Projects.ToString()).Selected = true;
        ddlDepartment.Enabled = false;

        ddlRole.Enabled = true;
        //31933 -Subhra - Start
        if (ddlDepartment.SelectedItem.Value.ToUpper() != "SELECT")
            //31933 -Subhra - End
        FillRoleDropDown(int.Parse(ddlDepartment.SelectedItem.Value));

        ddlProjectName.Enabled = true;
        FillProjectNameDropdownForPM();
      }

      //If user is CFM/FM than Department is "Finance"
      if (objParameter.RoleCFM == AuthorizationManagerConstants.ROLECFM)
      {
        ddlDepartment.ClearSelection();
        ddlDepartment.Items.FindByText(MasterEnum.Departments.Finance.ToString()).Selected = true;
        ddlDepartment.Enabled = false;

        ddlRole.Enabled = true;
        //31933 -Subhra - Start
        if (ddlDepartment.SelectedItem.Value.ToUpper() != "SELECT")
            //31933 -Subhra - End
        FillRoleDropDown(int.Parse(ddlDepartment.SelectedItem.Value));

      }

      //If user is HR than Status selected is "Pending External Allocation"
      if (objParameter.RoleRecruitment == AuthorizationManagerConstants.ROLERECRUITMENT)
      {
        ddlRole.Enabled = true;
        FillRoleDropDown(int.Parse(ddlDepartment.SelectedItem.Value));

        ddlProjectName.Enabled = true;
        FillProjectNameDropDown();

        ddlStatus.Items.FindByText(CommonConstants.MRFStatus_PendingExternalAllocation).Selected = true;
        ddlStatus.Enabled = false;

      }

      //If user is PreSales than the departments enabled are "Projects" and "PreSales"
      if (objParameter.RolePreSales == AuthorizationManagerConstants.ROLEPRESALES)
      {
        //ddlDepartment.ClearSelection();
        //ddlDepartment.Items.FindByText(MasterEnum.Departments.PreSales.ToString()).Selected = true;
        //ddlDepartment.Enabled = false;

        //RemoveDepartmentValuesForPresalesUser();
        ddlRole.Enabled = true;
        //31933 -Subhra - Start
        if (ddlDepartment.SelectedItem.Value.ToUpper() != "SELECT")
            //31933 -Subhra - End
        FillRoleDropDown(int.Parse(ddlDepartment.SelectedItem.Value));                    

      }

      //If user is HR than Department is "HR"
      if (objParameter.RoleHR == AuthorizationManagerConstants.ROLEHR)
      {
        ddlDepartment.ClearSelection();
        ddlDepartment.Items.FindByText(MasterEnum.Departments.HR.ToString()).Selected = true;
        ddlDepartment.Enabled = false;

        ddlRole.Enabled = true;
        //31933 -Subhra - Start
        if (ddlDepartment.SelectedItem.Value.ToUpper() != "SELECT")
            //31933 -Subhra - End
        FillRoleDropDown(int.Parse(ddlDepartment.SelectedItem.Value));

      }

      //If user is Testing than Department is "Testing"
      if (objParameter.RoleTesting == AuthorizationManagerConstants.ROLETESTING)
      {
        ddlDepartment.ClearSelection();
        ddlDepartment.Items.FindByText(MasterEnum.Departments.Testing.ToString()).Selected = true;
        ddlDepartment.Enabled = false;

        ddlRole.Enabled = true;
        //31933 -Subhra - Start
        if (ddlDepartment.SelectedItem.Value.ToUpper() != "SELECT")
            //31933 -Subhra - End
        FillRoleDropDown(int.Parse(ddlDepartment.SelectedItem.Value));

      }

      //If user is Quality than Department is "PMOQuality"
      if (objParameter.RoleQuality == AuthorizationManagerConstants.ROLEQUALITY)
      {
        ddlDepartment.ClearSelection();
        ddlDepartment.Items.FindByText(MasterEnum.Departments.PMOQuality.ToString()).Selected = true;
        ddlDepartment.Enabled = false;

        ddlRole.Enabled = true;
        //31933 -Subhra - Start
        if (ddlDepartment.SelectedItem.Value.ToUpper() != "SELECT")
            //31933 -Subhra - End
        FillRoleDropDown(int.Parse(ddlDepartment.SelectedItem.Value));

      }

      //If user is Marketing than Department is "Marketing"
      if (objParameter.RoleMarketing == AuthorizationManagerConstants.ROLEMH)
      {
        ddlDepartment.ClearSelection();
        ddlDepartment.Items.FindByText(MasterEnum.Departments.Marketing.ToString()).Selected = true;
        ddlDepartment.Enabled = false;

        ddlRole.Enabled = true;
        //31933 -Subhra - Start
        if (ddlDepartment.SelectedItem.Value.ToUpper() != "SELECT")
            //31933 -Subhra - End
        FillRoleDropDown(int.Parse(ddlDepartment.SelectedItem.Value));

      }
      //If user is rave consultant than Department is "rave consultant"
      if (objParameter.RoleRaveConsultant == AuthorizationManagerConstants.ROLERAVECONSULTANT)
      {
        ddlDepartment.ClearSelection();
        ddlDepartment.SelectedValue = Convert.ToInt16(MasterEnum.Departments.RaveConsultant_India).ToString();
        //ddlDepartment.Items.FindByText(MasterEnum.Departments.RaveConsultant_India.ToString()).Selected = true;
        ddlDepartment.Enabled = false;

        ddlRole.Enabled = true;
        //31933 -Subhra - Start
        if (ddlDepartment.SelectedItem.Value.ToUpper() != "SELECT")
            //31933 -Subhra - End
        FillRoleDropDown(int.Parse(ddlDepartment.SelectedItem.Value));

      }
    }
    //24736-Ambar-End
  }

}
