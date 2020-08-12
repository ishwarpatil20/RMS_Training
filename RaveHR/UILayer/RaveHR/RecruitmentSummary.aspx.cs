//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           RecruitmentSummary.aspx      
//  Author:         Gaurav Thakkar 
//  Date written:   17/09/2009/ 11:30:30 AM
//  Description:    Recruitment Summary 
//
//  Amendments
//  Date                    Who              Ref     Description
//  ----                    -----------      ---     -----------
//  17/09/2009/ 11:30:30 AM  Gaurav Thakkar    n/a     Created    
//
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
using Common;
using System.Data.SqlClient;
using BusinessEntities;
using Common.AuthorizationManager;

public partial class RecruitmentSummary : BaseClass
{

    private static string sortExpression = string.Empty;
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    private const string PREVIOUS = "Previous";
    private const string NEXT = "Next";
    private int pageCount = 0;
    private string imageDirection = string.Empty;
    BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
    BusinessEntities.ParameterCriteria objParameter = new BusinessEntities.ParameterCriteria();
    Hashtable hashTable = new Hashtable();
     
    // 27633 subhra start
    /// <summary>
    /// To store data for previous and next page for paging issue
    /// </summary>
    Hashtable temppreviousHashTable = new Hashtable();
    BusinessEntities.RaveHRCollection raveHRpreviousCollection = new BusinessEntities.RaveHRCollection();
    private int IntHashpageCount = 0;
    //End 27633
    private const string CLASS_NAME_RECRUITMENTSUMMARY = "RecruitmentSummary.aspx";
    private const string FILLSTATUSDROPDOWN = "FillStatusDropDown";

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
                ArrayList arrRolesForUser = new ArrayList();
                AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
                arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(RaveHRAuthorizationManager.getLoggedInUser());

                if (!(arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERECRUITMENT)))
                {
                    Response.Redirect(CommonConstants.PAGE_HOME, true);
                }
                //Siddharth 26th August 2015 End



                if (!IsPostBack)
                {
                    if (Session[SessionNames.CONFIRMATION_MESSAGE_CANDIDATE_ADDED] != null)
                    {
                        lblMessage.Text = Convert.ToString(Session[SessionNames.CONFIRMATION_MESSAGE_CANDIDATE_ADDED]);
                        lblMessage.Visible = true;
                        Session.Remove(SessionNames.CONFIRMATION_MESSAGE_CANDIDATE_ADDED);
                    }
                    else if (Session[SessionNames.CONFIRMATION_MESSAGE_CANDIDATE_DELETED] != null)
                    {
                        lblMessage.Text = Convert.ToString(Session[SessionNames.CONFIRMATION_MESSAGE_CANDIDATE_DELETED]);
                        lblMessage.Visible = true;
                        Session.Remove(SessionNames.CONFIRMATION_MESSAGE_CANDIDATE_DELETED);
                    }
                    else if (Session[SessionNames.CONFIRMATION_MESSAGE_CANDIDATE_UPDATED] != null)
                    {
                        lblMessage.Text = Convert.ToString(Session[SessionNames.CONFIRMATION_MESSAGE_CANDIDATE_UPDATED]);
                        lblMessage.Visible = true;
                        Session.Remove(SessionNames.CONFIRMATION_MESSAGE_CANDIDATE_UPDATED);
                    }
                    else if (Session[SessionNames.CONFIRMATION_MESSAGE_CANDIDATE_JOINED] != null)
                    {
                        lblMessage.Text = Convert.ToString(Session[SessionNames.CONFIRMATION_MESSAGE_CANDIDATE_JOINED]);
                        lblMessage.Visible = true;
                        Session.Remove(SessionNames.CONFIRMATION_MESSAGE_CANDIDATE_JOINED);
                    }
                    else
                    {
                        lblMessage.Text = "";
                    }
                    if (Session[SessionNames.CURRENT_PAGE_INDEX_RS] == null)
                        Session[SessionNames.CURRENT_PAGE_INDEX_RS] = 1;

                    // Setting the Default sortExpression as MrfCode
                    if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION_RS] == null)
                    {
                        sortExpression = CommonConstants.MRF_CODE;
                        Session[SessionNames.PREVIOUS_SORT_EXPRESSION_RS] = sortExpression;
                    }
                    else
                    {
                        sortExpression = Session[SessionNames.PREVIOUS_SORT_EXPRESSION_RS].ToString();
                    }

                    this.FillDepartmentDropDown();

                    this.FillStatusDropDown();

                    //On Pageload ProjectName & Role dropdowns are disabled and their default value is 
                    //set to "SELECT".
                    ddlProjectName.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
                    ddlProjectName.Enabled = false;
                    ddlRole.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
                    ddlRole.Enabled = false;

                    BindGridRecruitmentSummary();

                    if (Session[SessionNames.DEPARTMENT_ID_RS] != null)
                    {
                        ddlDepartment.SelectedValue = Session[SessionNames.DEPARTMENT_ID_RS].ToString();
                    }

                    if (Session[SessionNames.PROJECT_ID_RS] != null)
                    {
                        ddlProjectName.SelectedValue = Session[SessionNames.PROJECT_ID_RS].ToString();
                    }

                    if (Session[SessionNames.ROLE_RS] != null)
                    {
                        ddlRole.SelectedValue = Session[SessionNames.ROLE_RS].ToString();
                    }

                    if (Session[SessionNames.PROJECT_ID_RS] != null || Session[SessionNames.ROLE_RS] != null || Session[SessionNames.DEPARTMENT_ID_RS] != null)
                    {
                        btnRemoveFilter.Visible = true;
                    }

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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RECRUITMENTSUMMARY, "Page_Load", EventIDConstants.RAVE_HR_RECRUITMENT_PRESENTATION_LAYER);
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RECRUITMENTSUMMARY, "ddlDepartment_SelectedIndexChanged", EventIDConstants.RAVE_HR_RECRUITMENT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// When a particular Column is clicked for Sorting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvRecruitmentSummary_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            GridViewRow gvrPager = gvRecruitmentSummary.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_RS].ToString();

            //On sorting assign new sort expression
            sortExpression = e.SortExpression;

            if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION_RS] == null)
            {
                Session[SessionNames.PREVIOUS_SORT_EXPRESSION_RS] = sortExpression;
            }

            if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION_RS].ToString() == sortExpression)
            {
                //Sort to opposite direction based upon previous sort direction
                if (Session[SessionNames.SORT_DIRECTION_RS] == null || GridViewSortDirection == SortDirection.Descending)
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

            if (Session[SessionNames.PROJECT_ID_RS] != null || Session[SessionNames.DEPARTMENT_ID_RS] != null || Session[SessionNames.ROLE_RS] != null)
            {
                btnRemoveFilter.Visible = true;
            }

            //Set current sort expression as PreviousSortExpression
            Session[SessionNames.PREVIOUS_SORT_EXPRESSION_RS] = sortExpression;
        }

      //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RECRUITMENTSUMMARY, "gvRecruitmentSummary_Sorting", EventIDConstants.RAVE_HR_RECRUITMENT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Paging for Grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvRecruitmentSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (e.NewPageIndex != -1)
            {
                // Assign the new page index
                gvRecruitmentSummary.PageIndex = e.NewPageIndex;
            }

            // Bind the grid as per new page index.
            BindGridRecruitmentSummary();
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RECRUITMENTSUMMARY, "gvRecruitmentSummary_PageIndexChanging", EventIDConstants.RAVE_HR_RECRUITMENT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// When data is bound to datarow in gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvRecruitmentSummary_DataBound(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrPager = gvRecruitmentSummary.BottomPagerRow;

            if (gvrPager == null) return;

            //Get Text Box and Lable from the Grid View
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            Label lblPageCount = (Label)gvrPager.Cells[0].FindControl("lblPageCount");

            //Assign current page index to text box
            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_RS].ToString();

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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RECRUITMENTSUMMARY, "gvRecruitmentSummary_DataBound", EventIDConstants.RAVE_HR_RECRUITMENT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    /// <summary>
    /// The RowCreated event is raised when each row in the GridView control is created
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvRecruitmentSummary_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (((Session[SessionNames.PAGE_COUNT_RS] != null) && (int.Parse(Session[SessionNames.PAGE_COUNT_RS].ToString()) > 1)) || ((raveHRCollection.Count > 1)))
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RECRUITMENTSUMMARY, "gvRecruitmentSummary_RowCreated", EventIDConstants.RAVE_HR_RECRUITMENT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Row is bound to grid 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvRecruitmentSummary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {           

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool CandidateNotJoining = false;

             //Name: Sanju:Issue Id 50201  Changed cursor property to pointer so that it should display hand in IE10,9,Chrome and mozilla browser.
             //   e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
                // Mohamed : NIS-RMS : 07/01/2015 : Starts                        			  
                // Desc : Remove underline and click from remaining columns

                e.Row.Cells[1].Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                e.Row.Cells[1].Attributes["onmouseout"] = "this.style.textDecoration='none';";

                e.Row.Cells[1].Attributes["onclick"] = "location.href = 'RecruitmentPipelineDetails.aspx?" + 
                    URLHelper.SecureParameters("CandidateId", DataBinder.Eval(e.Row.DataItem, "CandidateId").ToString()) + 
                    "&" +
                    URLHelper.SecureParameters("CandidateNotJoining", CandidateNotJoining.ToString()) +                    
                    "&" + 
                    URLHelper.SecureParameters("index", e.Row.RowIndex.ToString()) +
                    "&" +
                    URLHelper.CreateSignature(DataBinder.Eval(e.Row.DataItem, "CandidateId").ToString(),CandidateNotJoining.ToString(), e.Row.RowIndex.ToString()) + "'";
                // Mohamed : NIS-RMS : 07/01/2015 : Ends
                ViewState["Recruitment"]  = e.Row.DataItem.ToString();
                //To solved the issue id : 20375
                //Start
                if (!hashTable.Contains(e.Row.RowIndex))
                hashTable.Add(e.Row.RowIndex, DataBinder.Eval(e.Row.DataItem, "CandidateId").ToString());
                //End
            }
            Session[SessionNames.RECRUITMENTVIEWINDEX] = hashTable;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RECRUITMENTSUMMARY, "gvRecruitmentSummary_RowDataBound", EventIDConstants.RAVE_HR_RECRUITMENT_PRESENTATION_LAYER);
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
            if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0 && Convert.ToInt32(txtPages.Text) <= Convert.ToInt32(Session[SessionNames.PAGE_COUNT_RS].ToString()))
            {
                gvRecruitmentSummary.PageIndex = Convert.ToInt32(txtPages.Text) - 1;
                Session[SessionNames.CURRENT_PAGE_INDEX_RS] = txtPages.Text;
            }
            else
            {
                txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_RS].ToString();
                return;
            }

            //Bind the grid on paging
            BindGridRecruitmentSummary();

            //Assign the tetbox current page no.
            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_RS].ToString();
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RECRUITMENTSUMMARY, "txtPages_TextChanged", EventIDConstants.RAVE_HR_RECRUITMENT_PRESENTATION_LAYER);
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
            GridViewRow gvrPager = gvRecruitmentSummary.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");

            switch (e.CommandName)
            {
                //Previous button is clicked
                case PREVIOUS:
                    //To solved the issue id : 20375
                    //Start
                    if ((Convert.ToInt32(txtPages.Text) - 1) == 0)
                    {
                        Session[SessionNames.CURRENT_PAGE_INDEX_RS] = 1;
                        txtPages.Text = "1";

                    }
                    else
                    {
                    //End
                        Session[SessionNames.CURRENT_PAGE_INDEX_RS] = Convert.ToInt32(txtPages.Text) - 1;
                        txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) - 1);
                    }
                    break;

                //Next button is clicked
                case NEXT:
                    Session[SessionNames.CURRENT_PAGE_INDEX_RS] = Convert.ToInt32(txtPages.Text) + 1;
                    txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) + 1);
                    break;
            }

            // Bind the grid on paging.
            BindGridRecruitmentSummary();
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RECRUITMENTSUMMARY, "ChangePage", EventIDConstants.RAVE_HR_RECRUITMENT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            // Assign DepartmentId to Session
            Session[SessionNames.DEPARTMENT_ID_RS] = ddlDepartment.SelectedItem.Value;
            // Assign ProjectId to Session
            Session[SessionNames.PROJECT_ID_RS] = ddlProjectName.SelectedItem.Value;
            // Assign RoleId to Session
            Session[SessionNames.ROLE_RS] = ddlRole.SelectedItem.Value;
            
            //// Assign StatusId to Session
            if (ddlStatus.SelectedItem.Text != CommonConstants.SELECT)              
                Session[SessionNames.STATUS] = ddlStatus.SelectedItem.Value;        
            else
                Session[SessionNames.STATUS] = Convert.ToInt32(MasterEnum.MRFStatus.PendingExpectedResourceJoin);

            if (gvRecruitmentSummary.BottomPagerRow != null)
            {
                GridViewRow gvrPager = gvRecruitmentSummary.BottomPagerRow;
                TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");


                if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0)
                {
                    txtPages.Text = 1.ToString();
                    Session[SessionNames.CURRENT_PAGE_INDEX_RS] = txtPages.Text;
                }


            }

            BindGridRecruitmentSummary();

            if (Session[SessionNames.PROJECT_ID_RS] != null || Session[SessionNames.DEPARTMENT_ID_RS] != null || Session[SessionNames.ROLE_RS] != null || Session[SessionNames.STATUS]!= null)
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RECRUITMENTSUMMARY, "btnFilter_Click", EventIDConstants.RAVE_HR_RECRUITMENT_PRESENTATION_LAYER);
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
            Session[SessionNames.DEPARTMENT_ID_RS] = null;
            // Assign ProjectId to Session
            Session[SessionNames.PROJECT_ID_RS] = null;
            // Assign RoleId to Session
            Session[SessionNames.ROLE_RS] = null;
           // Assign RoleId to Session
            Session[SessionNames.STATUS] = null;

            sortExpression = CommonConstants.MRF_CODE;
            Session[SessionNames.PREVIOUS_SORT_EXPRESSION_RS] = sortExpression;

            ClearFilteringFields();
            BindGridRecruitmentSummary();
            btnRemoveFilter.Visible = false;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RECRUITMENTSUMMARY, "btnRemoveFilter_Click", EventIDConstants.RAVE_HR_RECRUITMENT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void gvRecruitmentSummary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "CandidateNotJoiningCommand")
        {
            bool CandidateNotJoining = true;
            GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            ImageButton imgCandidateNotJoined = (ImageButton)grv.FindControl("imgCandidateNotJoining");          
            Response.Redirect(CommonConstants.Page_RecruitmentPipelineDetails +
                              "?" +
                              URLHelper.SecureParameters("CandidateId", imgCandidateNotJoined.CommandArgument.ToString()) +
                              "&" + 
                              URLHelper.SecureParameters("CandidateNotJoining",CandidateNotJoining.ToString())+
                              "&" + 
                              URLHelper.CreateSignature(imgCandidateNotJoined.CommandArgument.ToString(),CandidateNotJoining.ToString()),false);
            

 
        }


 
    }


    #endregion Protected Events

    #region Private Methods

    /// <summary>
    /// Binds the Recruitment Summary grid
    /// </summary>
    private void BindGridRecruitmentSummary()
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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RECRUITMENTSUMMARY, "BindGridRecruitmentSummary", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Get or set the GridViewSortDirection property
    /// </summary>
    private SortDirection GridViewSortDirection
    {
        get
        {
            if (Session[SessionNames.SORT_DIRECTION_RS] == null)
                Session[SessionNames.SORT_DIRECTION_RS] = SortDirection.Ascending;

            return (SortDirection)Session[SessionNames.SORT_DIRECTION_RS];
        }

        set
        {
            Session[SessionNames.SORT_DIRECTION_RS] = value;
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

            objParameter.PageNumber = Convert.ToInt32(Session[SessionNames.CURRENT_PAGE_INDEX_RS].ToString());
            objParameter.PageSize = 10;

            raveHRCollection = GetRecruitmentSummary();

            if ((int.Parse(Session[SessionNames.PAGE_COUNT_RS].ToString()) == 1) && (raveHRCollection.Count == 1))
            {
                gvRecruitmentSummary.AllowSorting = false;
            }
            else
            {
                gvRecruitmentSummary.AllowSorting = true;
            }

            if (raveHRCollection.Count == 0)
            {
                gvRecruitmentSummary.DataSource = raveHRCollection;
                gvRecruitmentSummary.DataBind();

                ShowHeaderWhenEmptyGrid(raveHRCollection);
            }
            else
            {
                gvRecruitmentSummary.DataSource = raveHRCollection;
                gvRecruitmentSummary.DataBind();

            }

            //Finds the Paging Text Box, Previous and Next Link Buttons in Grid Views Pagning Templet
            GridViewRow gvrPager = gvRecruitmentSummary.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            LinkButton lbtnPrevious = (LinkButton)gvrPager.Cells[0].FindControl("lbtnPrevious");
            LinkButton lbtnNext = (LinkButton)gvrPager.Cells[0].FindControl("lbtnNext");

            if (pageCount > 1)
            {
                gvRecruitmentSummary.BottomPagerRow.Visible = true;
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
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RECRUITMENTSUMMARY, "SortGridView", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Get the summary 
    /// </summary>
    /// <returns></returns>
    private BusinessEntities.RaveHRCollection GetRecruitmentSummary()
    {
        Rave.HR.BusinessLayer.Recruitment.Recruitment recruitmentSummaryBL = new Rave.HR.BusinessLayer.Recruitment.Recruitment();
        BusinessEntities.Recruitment recruitment = new BusinessEntities.Recruitment();
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            if (gvRecruitmentSummary.BottomPagerRow != null)
            {
                GridViewRow gvrPager = gvRecruitmentSummary.BottomPagerRow;
                TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
                if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0)
                {
                    objParameter.PageNumber = int.Parse(txtPages.Text);
                    objParameter.PageSize = 10;
                }
            }
            else
            {
                // venkatesh  : Issue 46698 : 4/12/2013 : Starts                 
                // Desc : proper Paging No  should display

                if (Session[SessionNames.CURRENT_PAGE_INDEX_RS] != null)
                {
                    objParameter.PageNumber = Convert.ToInt32(Session[SessionNames.CURRENT_PAGE_INDEX_RS].ToString());
                    objParameter.PageSize = 10;
                }
                else
                {
                    objParameter.PageNumber = 1;
                    objParameter.PageSize = 10;
                }
                // venkatesh  : Issue 46698 : 4/12/2013 : End 

            }

            if (Session[SessionNames.PROJECT_ID_RS] == null || Session[SessionNames.PROJECT_ID_RS].ToString() == CommonConstants.SELECT)
            {
                recruitment.ProjectId = 0;
            }
            else
            {
                recruitment.ProjectId = int.Parse(Session[SessionNames.PROJECT_ID_RS].ToString());
            }
            if (Session[SessionNames.ROLE_RS] == null || Session[SessionNames.ROLE_RS].ToString() == CommonConstants.SELECT)
            {
                recruitment.RoleId = 0;
            }
            else
            {
                recruitment.RoleId = int.Parse(Session[SessionNames.ROLE_RS].ToString());
            }
            if (Session[SessionNames.DEPARTMENT_ID_RS] == null || Session[SessionNames.DEPARTMENT_ID_RS].ToString() == CommonConstants.SELECT)
            {
                recruitment.DepartmentId = 0;
            }
            else
            {
                recruitment.DepartmentId = int.Parse(Session[SessionNames.DEPARTMENT_ID_RS].ToString());
            }

            if (Session[SessionNames.STATUS] == null || Session[SessionNames.STATUS].ToString() == CommonConstants.SELECT)
            {
                recruitment.MRFStatus = 0;
            }
            else
            {
                recruitment.MRFStatus = int.Parse(Session[SessionNames.STATUS].ToString());
            }

            if (Session[SessionNames.PROJECT_ID_RS] == null && Session[SessionNames.ROLE_RS] == null && Session[SessionNames.DEPARTMENT_ID_RS] == null && Session[SessionNames.STATUS] == null)
            {
                // Method for Pageload
                raveHRCollection = recruitmentSummaryBL.GetRecruitmentSummaryForPageLoad(objParameter, ref pageCount);
                // 27633-ambar-start
                objParameter.PageSize = 10000;
                int temppagenumber = 0;
                temppagenumber = objParameter.PageNumber;
                objParameter.PageNumber = 1;
                raveHRpreviousCollection = recruitmentSummaryBL.GetRecruitmentSummaryForPageLoad(objParameter, ref IntHashpageCount);
                objParameter.PageSize = 10;
                objParameter.PageNumber = temppagenumber;
                int k = 0;
                foreach (BusinessEntities.Recruitment collectionprevious in raveHRpreviousCollection)
                {
                    temppreviousHashTable.Add(k, collectionprevious.CandidateId.ToString());
                    k++;
                }
                Session[SessionNames.RECRUITMENTPREVIOUSHASHTABLE] = temppreviousHashTable;
                // 27633-ambar-End

            }
            else
            {
                #region  ////// 27633-ambar-start
                ////// Method for Filter
                ////raveHRCollection = recruitmentSummaryBL.GetRecruitmentSummaryForFilterAndPaging(objParameter, recruitment, ref pageCount);
                
                ////objParameter.PageSize = 10000;
                ////int temppagenumber = 0;
                ////temppagenumber = objParameter.PageNumber;
                ////objParameter.PageNumber = 1;
                ////raveHRpreviousCollection = recruitmentSummaryBL.GetRecruitmentSummaryForPageLoad(objParameter, ref IntHashpageCount);
                ////objParameter.PageSize = 10;
                ////objParameter.PageNumber = temppagenumber;
                ////int k = 0;
                ////foreach (BusinessEntities.Recruitment collectionprevious in raveHRpreviousCollection)
                ////{
                ////    temppreviousHashTable.Add(k, collectionprevious.MRFId.ToString());
                ////    k++;
                ////}
                ////Session[SessionNames.RECRUITMENTPREVIOUSHASHTABLE] = temppreviousHashTable;
                ////// 27633-ambar-End
                #endregion

                // Method for Filter
                // venkatesh  : Issue 46698 : 9/12/2013 : Starts                 
                // Desc : Filter -> Paging -> give error
                raveHRCollection = recruitmentSummaryBL.GetRecruitmentSummaryForFilterAndPaging(objParameter, recruitment, ref pageCount);
                objParameter.PageSize = 10000;
                int temppagenumber = 0;
                temppagenumber = objParameter.PageNumber;
                objParameter.PageNumber = 1;
                raveHRpreviousCollection = recruitmentSummaryBL.GetRecruitmentSummaryForFilterAndPaging(objParameter, recruitment, ref IntHashpageCount);
                objParameter.PageSize = 10;
                objParameter.PageNumber = temppagenumber;
                int k = 0;
                foreach (BusinessEntities.Recruitment collectionprevious in raveHRpreviousCollection)
                {                    
                    temppreviousHashTable.Add(k, collectionprevious.CandidateId.ToString());                    
                    k++;
                }
                Session[SessionNames.RECRUITMENTPREVIOUSHASHTABLE] = temppreviousHashTable;
                // venkatesh  : Issue 46698 : 9/12/2013 : End
            }

            Session[SessionNames.PAGE_COUNT_RS] = pageCount;

        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RECRUITMENTSUMMARY, "GetRecruitmentSummary", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
        return raveHRCollection;
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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RECRUITMENTSUMMARY, "FillDepartmentDropDown", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
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
                if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Admin))
                {
                    FillRoleDropDown(Convert.ToInt32(EnumsConstants.Category.AdminRole));
                }
                if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Finance))
                {
                    FillRoleDropDown(Convert.ToInt32(EnumsConstants.Category.FinanceRole));
                }
                if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.HR))
                {
                    FillRoleDropDown(Convert.ToInt32(EnumsConstants.Category.HRRole));
                }
                if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.ITS))
                {
                    FillRoleDropDown(Convert.ToInt32(EnumsConstants.Category.ITSRole));
                }
                if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Marketing))
                {
                    FillRoleDropDown(Convert.ToInt32(EnumsConstants.Category.MarketingRole));
                }
                if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.PMOQuality))
                {
                    FillRoleDropDown(Convert.ToInt32(EnumsConstants.Category.PMOQualityRole));
                }
                if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.PreSales))
                {
                    FillRoleDropDown(Convert.ToInt32(EnumsConstants.Category.PreSalesRole));
                }
                if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.RaveDevelopment))
                {
                    FillRoleDropDown(Convert.ToInt32(EnumsConstants.Category.RaveDevelopmentRole));
                }
                if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Support))
                {
                    ddlRole.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
                }
                if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Testing))
                {
                    FillRoleDropDown(Convert.ToInt32(EnumsConstants.Category.TestingRole));
                }

                ddlProjectName.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
                ddlProjectName.Enabled = false;

            }
            else
            {
                FillRoleDropDown(Convert.ToInt32(EnumsConstants.Category.ProjectRole));
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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RECRUITMENTSUMMARY, "FillRoleDropdownAsPerDepartment", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    ///  Fills the Role dropdown
    /// </summary>
    private void FillRoleDropDown(int categoryId)
    {
        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        // Initialise Business layer object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        try
        {
            // Call the Business layer method
            raveHRCollection = master.FillDropDownsBL(categoryId);

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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RECRUITMENTSUMMARY, "FillRoleDropDown", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
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
        Rave.HR.BusinessLayer.Recruitment.Recruitment projectName = new Rave.HR.BusinessLayer.Recruitment.Recruitment();
        try
        {
            // Call the Business layer method
            raveHRCollection = projectName.GetProjectName();

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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RECRUITMENTSUMMARY, "FillProjectNameDropDown", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
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
                    case CommonConstants.PROJECT_NAME:
                        headerRow.Cells[2].Controls.Add(sortImage);
                        break;

                    case CommonConstants.CLIENT_NAME:
                        headerRow.Cells[3].Controls.Add(sortImage);
                        break;

                    case CommonConstants.ROLE:
                        headerRow.Cells[4].Controls.Add(sortImage);
                        break;

                    case CommonConstants.RESOURCE_NAME:
                        headerRow.Cells[5].Controls.Add(sortImage);
                        break;


                    case CommonConstants.EXPECTED_DATE_OF_JOINING:
                        headerRow.Cells[6].Controls.Add(sortImage);
                        break;

                    case CommonConstants.RECRUITMENT_MANAGER:
                        headerRow.Cells[7].Controls.Add(sortImage);
                        break;

                    case CommonConstants.DEPARTMENT:
                        headerRow.Cells[8].Controls.Add(sortImage);
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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RECRUITMENTSUMMARY, "AddSortImage", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }

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
            gvRecruitmentSummary.ShowHeader = true;
            // Disable sorting
            gvRecruitmentSummary.AllowSorting = false;

            //Create empty datasource for Grid view and bind
            raveHRCollection.Add(new BusinessEntities.Recruitment());
            gvRecruitmentSummary.DataSource = raveHRCollection;
            gvRecruitmentSummary.DataBind();

            //clear all the cells in the row
            gvRecruitmentSummary.Rows[0].Cells.Clear();

            //add a new blank cell
            gvRecruitmentSummary.Rows[0].Cells.Add(new TableCell());
            gvRecruitmentSummary.Rows[0].Cells[0].Text = CommonConstants.NO_RECORDS_FOUND_MESSAGE;
            gvRecruitmentSummary.Rows[0].Cells[0].Wrap = false;
            gvRecruitmentSummary.Rows[0].Cells[0].Width = Unit.Percentage(10);
            gvRecruitmentSummary.Rows[0].Attributes.Clear();

        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RECRUITMENTSUMMARY, "ShowHeaderWhenEmptyGrid", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Sets the default value "Select" of all the dropdown in filter panel
    /// </summary>
    private void ClearFilteringFields()
    {
        try
        {
            ddlDepartment.SelectedIndex = CommonConstants.ZERO;
            ddlProjectName.SelectedIndex = CommonConstants.ZERO;
            ddlRole.SelectedIndex = CommonConstants.ZERO;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RECRUITMENTSUMMARY, "ClearFilteringFields", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    ///  Fills the Status dropdown
    /// </summary>
    private void FillStatusDropDown()
    {
        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
        BusinessEntities.RaveHRCollection newRaveHRCollection = new BusinessEntities.RaveHRCollection();

        // Initialise Business layer object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        try
        {
            // Call the Business layer method
            raveHRCollection = master.FillDropDownsBL(Convert.ToInt32(EnumsConstants.Category.MRFStatus));//PendingAllocationStatus


            foreach (KeyValue<string> keyValue in raveHRCollection)
            {
                if (keyValue.KeyName == "141" || keyValue.KeyName == "263" || keyValue.KeyName == "733" || keyValue.KeyName == "SELECT")
                {
                    newRaveHRCollection.Add(keyValue);
                }
            }

            if (newRaveHRCollection != null)
            {
                // Assign the data source to dropdown
                ddlStatus.DataSource = newRaveHRCollection;

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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RECRUITMENTSUMMARY, FILLSTATUSDROPDOWN, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    #endregion Private Methods

}
