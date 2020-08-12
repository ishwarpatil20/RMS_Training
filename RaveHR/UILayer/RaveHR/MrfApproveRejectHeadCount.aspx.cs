//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           MrfApproveRejectHeadCount.aspx      
//  Author:         Chhaya Gunjal 
//  Date written:   03/09/2009/ 10:58:30 AM
//  Description:    The MrfApproveRejectHeadCount page summarises MRF details whose status is 
//                  Pending Approval of Head Count.
//
//  Amendments
//  Date                    Who              Ref     Description
//  ----                    -----------      ---     -----------
//  03/09/2009 10:58:30 AM  Chhaya Gunjal    n/a     Created    
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Rave.HR.BusinessLayer.MRF;
using BusinessEntities;
using Common;
using Common.AuthorizationManager;
using System.Text;
using System.Collections.Generic;

public partial class MrfApproveRejectHeadCount : BaseClass
{

    #region Variable
    private BusinessEntities.MRFDetail mrfDetail = new BusinessEntities.MRFDetail();
    private static string sortExpression = string.Empty;
    RaveHRCollection objListSort = new RaveHRCollection();
    string subject;
    string body;
    AuthorizationManager objAuMan = new AuthorizationManager();
    //Googleconfigurable
    //const string RAVEDOMAIN = "@rave-tech.com";
    private const string PROJECT = "Projects";
    //Declaring Master Class Object
    Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

    /// <summary>
    /// Store the Value of row count
    /// </summary>
    Hashtable hashTable = new Hashtable();


    #endregion

    #region Constants
    private const string APPROVE = "Approve";
    private const string REJECT = "Reject";
    private const string VIEW = "View";
    private const string NO_RECORDS_FOUND_MESSAGE = "No records found.";
    BusinessEntities.ParameterCriteria objParameterCriteria = new BusinessEntities.ParameterCriteria();
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    private const string CLASS_NAME = "MrfApproveRejectHeadCount.aspx";
    private const string CURRENT_PAGE_INDEX = "Current Page Index of Approve REject Head Count";
    private const string PREVIOUS_SORT_EXPRESSION = "Previous_Sort_Expression of ARHC";
    private const string TXT_PAGES = "txtPages";
    private const string PREVIOUS = "Previous";
    private const string NEXT = "Next";
    private int pageCount = 0;
    private const string PAGE_COUNT = "Page Count of ARHC";
    private const string LBL_PAGE_COUNT = "lblpagecount";
    private const string SORT = "Sort";
    private const string LBL_BTN_PREVIOUS = "lbtnPrevious";
    private const string LBL_BTN_NEXT = "lbtnNext";
    private const string SORT_DIRECTION = "sortDirection";
    private const string MRFCode = "MRFCode";
    private const string MRFRaisedBy = "MRFRaisedBy";
    private const string PROJECT_NAME = "ProjectName";
    private const string MRFCTC = "MRFCTC";
    private const string EXPERIENCE = "Experience";
    private const string RECRUITMENTMANAGER = "RecruitmentManager";
    private const string STATUSNAME = "StatusName";
    #region EventNameConstants
    private const string PAGE_LOAD = "Page_Load";
    private const string BTN_CANCEL_CLICK = "btnCancel_Click";
    private const string CHANGE_PAGE = "ChangePage";
    private const string TXT_PAGES_TEXTCHANGED = "txtPages_TextChanged";
    private const string BTN_SAVE_REJECT_CLICK = "btnSaveReject_Click";
    private const string BTN_SAVEAPPROVE_CLICK = "btnSaveApprove_Click";
    private const string GRID_ROWCOMMAND = "grdvListofPendingApproveRejectHeadCount_RowCommand";
    private const string GRID_DATABOUND = "grdvListofPendingApproveRejectHeadCount_DataBound";
    private const string GRID_PAGE_INDEX_CHANGING = "grdvListofPendingApproveRejectHeadCount_PageIndexChanging";
    private const string GRID_ROWCREATED = "grdvListofPendingApproveRejectHeadCount_RowCreated";
    private const string GRID_ROWDATABOUND = "grdvListofPendingApproveRejectHeadCount_RowDataBound";
    private const string MRFPURPOSE = "MRFPurpose";
    #endregion

    #region MethodNameConstants
    private const string BIND_GRID = "BindGrid";
    private const string SORT_GRID_VIEW = "SortGridView";
    private const string SHOW_HEADER_WHEN_EMPTY_GRID = "ShowHeaderWhenEmptyGrid";
    private const string GET_MRF_DETAILS = "GetMRFDetailsForApprovalOfHeadCount";
    private const string SET_REASON = "SetReasonOfApproveRejectMRF";
    private const string GRID_VIEW_SORT_DIRECTION = "GridViewSortDirection";
    private const string ADD_SORT_IMAGE = "AddSortImage";
    private const string GRID_SORTING = "grdvListofPendingApproveRejectHeadCount_Sorting";

    #endregion

    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {

        

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

            if (!(arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM) ||
                 arrRolesForUser.Contains(AuthorizationManagerConstants.ROLECOO)))
            {
                Response.Redirect(CommonConstants.PAGE_HOME, true);
            }
            //Siddharth 26th August 2015 End



            btnSaveReject.Attributes.Add(CommonConstants.EVENT_ONCLICK, "return ButtonRejectClickValidate();");
            //Poonam : Issue : Disable Button : Starts
            btnSaveApprove.OnClientClick = ClientScript.GetPostBackEventReference(btnSaveApprove, null);
            btnSaveReject.OnClientClick = "if(ButtonRejectClickValidate()){" + ClientScript.GetPostBackEventReference(btnSaveReject, null) + "}";
            //Poonam : Issue : Disable Button : Ends

            /*Changes made by Gaurav as per discussion that Comment textbox is not mandatory.*/
            //btnSaveApprove.Attributes.Add(CommonConstants.EVENT_ONCLICK, "return ButtonApproveClickValidate();");
            
            //Clear Message
            lblMessage.Text = string.Empty;

            if (!IsPostBack)
            {
                if (Session[SessionNames.MRFVIEWINDEX] != null)
                {
                    Session.Remove(SessionNames.MRFVIEWINDEX);
                }

                try
                {
                    //if (MRFRoles.GetRolesApproveRejectHeadCount() == AuthorizationManagerConstants.ROLECEO)
                    //{
                    //    grdvListofPendingApproveRejectHeadCount.Columns[6].Visible = false;
                    //    grdvListofPendingApproveRejectHeadCount.Columns[9].Visible = false;
                    //}
                    Session[CURRENT_PAGE_INDEX] = 1;

                    // 30302-Ambar-Start
                    // IF Session[SessionNames.CURRENT_PAGE_INDEX_MRF] is not initiated on MRF Summary Page then Initiate it here
                    if (Session[SessionNames.CURRENT_PAGE_INDEX_MRF] == null)
                    {
                      Session[SessionNames.CURRENT_PAGE_INDEX_MRF] = 1;
                    }
                    // 30302-Ambar-End

                    if (Session[PREVIOUS_SORT_EXPRESSION] == null)
                    {
                        sortExpression = MRFCode;
                        Session[PREVIOUS_SORT_EXPRESSION] = sortExpression;
                    }
                    else
                    {
                        sortExpression = Session[PREVIOUS_SORT_EXPRESSION].ToString();
                    }
                    //Bind the grid while Loading the page
                    BindGrid();

                }
                //catches RaveHRException exception
                catch (RaveHRException ex)
                {
                    LogErrorMessage(ex);
                }
                catch (Exception ex)
                {
                    RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, PAGE_LOAD, EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
                    LogErrorMessage(objEx);
                }
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(AuthorizationManagerConstants.PAGEHOME, false);
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, BTN_CANCEL_CLICK, EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void ChangePage(object sender, CommandEventArgs e)
    {
        try
        {
            GridViewRow gvrPager = grdvListofPendingApproveRejectHeadCount.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl(TXT_PAGES);
            switch (e.CommandName)
            {
                case PREVIOUS:
                    Session[CURRENT_PAGE_INDEX] = Convert.ToInt32(txtPages.Text) - 1;
                    txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) - 1);
                    grdvListofPendingApproveRejectHeadCount.SelectedIndex = -1;
                    break;

                case NEXT:
                    Session[CURRENT_PAGE_INDEX] = Convert.ToInt32(txtPages.Text) + 1;
                    txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) + 1);
                    grdvListofPendingApproveRejectHeadCount.SelectedIndex = -1;
                    break;
            }

            BindGrid();
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, CHANGE_PAGE, EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void txtPages_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txtPages = (TextBox)sender;
            //Check for Paging text box is not empty, non-zero, and out of range paging no.
            if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0 && Convert.ToInt32(txtPages.Text) <= Convert.ToInt32(Session[PAGE_COUNT].ToString()))
            {
                grdvListofPendingApproveRejectHeadCount.PageIndex = Convert.ToInt32(txtPages.Text) - 1;
                Session[CURRENT_PAGE_INDEX] = txtPages.Text;
            }
            //Bind the grid on paging
            BindGrid();
            txtPages.Text = Session[CURRENT_PAGE_INDEX].ToString();
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, TXT_PAGES_TEXTCHANGED, EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnSaveReject_Click(object sender, EventArgs e)
    {
        try
        {
            mrfDetail = (BusinessEntities.MRFDetail)Session[SessionNames.MRFDetail];
            mrfDetail.CommentReason = txtReject.Text.ToString();
            //set the MRF status to Reject when head count Reject it.
            mrfDetail.Status = Common.CommonConstants.MRFStatus_Rejected;
            mrfDetail.IsApproved = false;
            string ConfirmMsg = string.Empty;
            // Rajan Kumar : Issue 46252: 10/02/2014 : Starts                        			 
            // Desc : In MRF history need to implemented in all cases in RMS.
            //Pass Email to know who is going to modified the data            
            AuthorizationManager authoriseduser = new AuthorizationManager();
            mrfDetail.LoggedInUserEmail = authoriseduser.getLoggedInUserEmailId();
            // Rajan Kumar : Issue 46252: 10/02/2014 : END
            SetReasonOfApproveRejectMRF(ref ConfirmMsg);

            lblMessage.Text = Common.CommonConstants.MSG_MRF_REJECTION_OF_HEADCOUNT;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, BTN_SAVE_REJECT_CLICK, EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnSaveApprove_Click(object sender, EventArgs e)
    {

        try
        {
            mrfDetail = (BusinessEntities.MRFDetail)Session[SessionNames.MRFDetail];
            mrfDetail.CommentReason = txtComment.Text.ToString();
            mrfDetail.IsApproved = true;
            string ConfirmMasg = string.Empty;

            //Get logged in user email Id.
            string userEmailId = objAuMan.getLoggedInUserEmailId();
            //Get Head Count approver EmailId.
            string ApproverMailId = GetEmailIdForDeptWiseHeadCountApprovalMailTo(mrfDetail.DepartmentName);
            // Rajan Kumar : Issue 46252: 10/02/2014 : Starts                        			 
            // Desc : In MRF history need to implemented in all cases in RMS.
            //Pass Email to know who is going to modified the data
            mrfDetail.LoggedInUserEmail = userEmailId;
            // Rajan Kumar : Issue 46252: 10/02/2014 : END
            SetReasonOfApproveRejectMRF(ref ConfirmMasg);

            lblMessage.Text = ConfirmMasg;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, BTN_SAVEAPPROVE_CLICK, EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdvListofPendingApproveRejectHeadCount_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            // --Add sort image in header
            if (Session[PAGE_COUNT] != null)
                if (int.Parse(Session[PAGE_COUNT].ToString()) == 1)
                {
                    AddSortImage(grdvListofPendingApproveRejectHeadCount.HeaderRow);
                }
            if (e.CommandName != SORT)
            {
                if (e.CommandName == APPROVE || e.CommandName == REJECT || e.CommandName == VIEW)
                {

                    GridView _gridView = (GridView)sender;
                    // Get the selected index and the command name
                    int _selectedIndex = int.Parse(e.CommandArgument.ToString());
                    string _commandName = e.CommandName;
                    grdvListofPendingApproveRejectHeadCount.SelectedIndex = _selectedIndex;
                    mrfDetail = new BusinessEntities.MRFDetail();
                    GridViewRow dr = grdvListofPendingApproveRejectHeadCount.Rows[_selectedIndex];
                    Label lbl = (Label)dr.Cells[0].FindControl("lbl");
                    HiddenField hdfRecruitmentMgrEmailId = (HiddenField)dr.Cells[16].FindControl("hdfRecruiterEmailId");
                    HiddenField hdfMRFStatusId = (HiddenField)dr.Cells[16].FindControl("hdfMRFStatusId");
                    HiddenField HfExpectedClosureDate = (HiddenField)dr.Cells[16].FindControl("HfExpectedClosureDate");

                    mrfDetail.MRFId = Convert.ToInt32(lbl.Text);
                    mrfDetail.MRFCode = dr.Cells[1].Text.ToString();
                    mrfDetail.DepartmentName = dr.Cells[5].Text.ToString();
                    mrfDetail.ProjectName = dr.Cells[6].Text.ToString();
                    mrfDetail.Role = dr.Cells[10].Text.ToString();
                    mrfDetail.RecruitmentManager = dr.Cells[13].Text.ToString();


                    //Recruiter Email Id.
                    mrfDetail.EmailId = hdfRecruitmentMgrEmailId.Value.ToString();
                    //current MRF status.
                    mrfDetail.StatusId = Convert.ToInt32(hdfMRFStatusId.Value.ToString());
                    mrfDetail.ExpectedClosureDate = HfExpectedClosureDate.Value.ToString();

                    lblMessage.Text = "";
                    Session[SessionNames.MRFDetail] = mrfDetail;
                    switch (_commandName)
                    {
                        case (APPROVE):
                            pnlApprove.Visible = true;
                            pnlReject.Visible = false;
                            break;
                        case (REJECT):
                            pnlReject.Visible = true;
                            pnlApprove.Visible = false;
                            break;
                        case (VIEW):
                            string url = "MrfView.aspx?" + URLHelper.SecureParameters("MRFId", mrfDetail.MRFId.ToString()) + "&" + URLHelper.SecureParameters("index", dr.RowIndex.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "APPREJHEADCOUNT") + "&" + URLHelper.CreateSignature(mrfDetail.MRFId.ToString(), dr.RowIndex.ToString(), "APPREJHEADCOUNT");
                            Response.Redirect(url, false);
                            //Server.Transfer(url);
                            break;
                    }
                }

                //mrfDetail.MRFId = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "MRFId").ToString());
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, GRID_ROWCOMMAND, EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdvListofPendingApproveRejectHeadCount_DataBound(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrPager = grdvListofPendingApproveRejectHeadCount.BottomPagerRow;

            if (gvrPager == null) return;

            //Get Text Box and Lable from the Grid View
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl(TXT_PAGES);
            Label lblPageCount = (Label)gvrPager.Cells[0].FindControl(LBL_PAGE_COUNT);

            //Assign current page index to text box
            txtPages.Text = Session[CURRENT_PAGE_INDEX].ToString();

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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, GRID_DATABOUND, EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdvListofPendingApproveRejectHeadCount_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //Change the page index on page index changing event and bind the grid
            if (e.NewPageIndex != -1)
            {
                grdvListofPendingApproveRejectHeadCount.PageIndex = e.NewPageIndex;
            }

            BindGrid();

        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, GRID_PAGE_INDEX_CHANGING, EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdvListofPendingApproveRejectHeadCount_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (((Session[PAGE_COUNT] != null) && (int.Parse(Session[PAGE_COUNT].ToString()) > 1)) || ((objListSort.Count > 1)))
                {
                    //Add sort Images to Grid View Header
                    AddSortImage(e.Row);
                }
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.DataItem != null)
                {
                    hashTable.Add(e.Row.RowIndex, DataBinder.Eval(e.Row.DataItem, "MRFID").ToString());
                }
            }

            if (hashTable.Keys.Count != 0)
            {
                Session[SessionNames.MRFVIEWINDEX] = hashTable;
            }

        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, GRID_ROWCREATED, EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdvListofPendingApproveRejectHeadCount_RowDataBound(object sender, GridViewRowEventArgs e)
    { }

    #endregion

    #region Methods
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
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, BIND_GRID, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
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

            if (sortExpression == CommonConstants.MRF_CODE)
            {
                objParameterCriteria.SortExpressionAndDirection = sortExpression + direction;
            }
            else
            {
                objParameterCriteria.SortExpressionAndDirection = sortExpression + direction;
            }
            objListSort = GetMRFDetailsForApprovalOfHeadCount();

            if ((int.Parse(Session[PAGE_COUNT].ToString()) == 1) && (objListSort.Count == 1))
            {
                grdvListofPendingApproveRejectHeadCount.AllowSorting = false;
            }
            else
            {
                grdvListofPendingApproveRejectHeadCount.AllowSorting = true;
            }

            if (objListSort.Count == 0)
            {
                grdvListofPendingApproveRejectHeadCount.DataSource = objListSort;
                grdvListofPendingApproveRejectHeadCount.DataBind();

                //grdvListofPendingApproveRejectHeadCount.Columns[0].Visible = false;
                ShowHeaderWhenEmptyGrid(objListSort);
            }
            else
            {
                //Bind the Grid View in Sorted order
                grdvListofPendingApproveRejectHeadCount.DataSource = objListSort;
                grdvListofPendingApproveRejectHeadCount.DataBind();
                //grdvListofPendingApproveRejectHeadCount.Columns[0].Visible = false;

            }

            //Finds the Paging Text Box, Previous and Next Link Buttons in Grid Views Pagning Templet
            GridViewRow gvrPager = grdvListofPendingApproveRejectHeadCount.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl(TXT_PAGES);

            LinkButton lbtnPrevious = (LinkButton)gvrPager.Cells[0].FindControl(LBL_BTN_PREVIOUS);
            LinkButton lbtnNext = (LinkButton)gvrPager.Cells[0].FindControl(LBL_BTN_NEXT);

            if (pageCount > 1)
            {
                grdvListofPendingApproveRejectHeadCount.BottomPagerRow.Visible = true;
            }

            //Don't allow any character other than Number in paging text box
            txtPages.Attributes.Add(CommonConstants.EVENT_ONKEYPRESS, "return isNumberKey(event)");

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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, SORT_GRID_VIEW, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Display Header When GridView Empty with proper message
    /// </summary>
    /// <param name="objListSort">EmptyList</param>
    private void ShowHeaderWhenEmptyGrid(RaveHRCollection objListSort)
    {
        try
        {
            //set header visible
            grdvListofPendingApproveRejectHeadCount.ShowHeader = true;
            grdvListofPendingApproveRejectHeadCount.AllowSorting = false;

            //Create empty datasource for Grid view and bind
            objListSort.Add(new BusinessEntities.MRFDetail());
            grdvListofPendingApproveRejectHeadCount.DataSource = objListSort;
            grdvListofPendingApproveRejectHeadCount.DataBind();
            grdvListofPendingApproveRejectHeadCount.Columns[0].Visible = false;

            //Calculate number of columns in Grid view used for column Span
            int columnsCount = grdvListofPendingApproveRejectHeadCount.Columns.Count;

            //clear all the cells in the row
            grdvListofPendingApproveRejectHeadCount.Rows[0].Cells.Clear();

            //add a new blank cell
            grdvListofPendingApproveRejectHeadCount.Rows[0].Cells.Add(new TableCell());
            grdvListofPendingApproveRejectHeadCount.Rows[0].Cells[0].Text = NO_RECORDS_FOUND_MESSAGE;
            grdvListofPendingApproveRejectHeadCount.Rows[0].Cells[0].Wrap = false;
            grdvListofPendingApproveRejectHeadCount.Rows[0].Cells[0].Width = Unit.Percentage(10);
            grdvListofPendingApproveRejectHeadCount.Rows[0].Attributes.Remove(CommonConstants.EVENT_ONCLICK);
            grdvListofPendingApproveRejectHeadCount.Rows[0].Attributes.Remove(CommonConstants.EVENT_ONMOUSEOVER);
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, SHOW_HEADER_WHEN_EMPTY_GRID, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Returns MRF Details whose status is pending approval of Head Count
    /// </summary>
    /// <returns>List</returns> GetMRFDetailsForApprovalOfHeadCount()
    private RaveHRCollection GetMRFDetailsForApprovalOfHeadCount()
    {
        RaveHRCollection objListMRFDetails = new RaveHRCollection();
        AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
        string UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
        string UserMailId = UserRaveDomainId.Replace("co.in", "com");
        try
        {
            if (grdvListofPendingApproveRejectHeadCount.BottomPagerRow != null)
            {
                GridViewRow gvrPager = grdvListofPendingApproveRejectHeadCount.BottomPagerRow;
                TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl(TXT_PAGES);
                if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0 && Convert.ToInt32(txtPages.Text) <= Convert.ToInt32(Session[PAGE_COUNT].ToString()))
                {
                    objParameterCriteria.PageNumber = int.Parse(txtPages.Text);
                }
            }
            else
            {
                objParameterCriteria.PageNumber = 1;
            }
            objParameterCriteria.PageSize = 10;
            //objListMRFDetails = Rave.HR.BusinessLayer.MRF.MRFDetail.GetMRFDetailsForApprocalOfMRFByHeadCount(MRFRoles.GetRolesApproveRejectHeadCount(),objParameterCriteria, ref pageCount);
            objListMRFDetails = Rave.HR.BusinessLayer.MRF.MRFDetail.GetMRFDetailsForApprocalOfMRFByHeadCount(UserMailId, objParameterCriteria, ref pageCount);
            Session[PAGE_COUNT] = pageCount;

        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, GET_MRF_DETAILS, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
        return objListMRFDetails;
    }

    /// <summary>
    /// Change the status of MRF and also reson.
    /// </summary>
    private void GetApproveRejectMRFPage()
    {
        try
        {
            BindGrid();
            pnlApprove.Visible = false;
            pnlReject.Visible = false;
            txtComment.Text = string.Empty;
            txtReject.Text = string.Empty;
            grdvListofPendingApproveRejectHeadCount.SelectedIndex = -1;

        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, SET_REASON, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// Change the status of MRF and also reson.
    /// </summary>
    private void SetReasonOfApproveRejectMRF(ref string ConfirmMasg)
    {
        try
        {
            Rave.HR.BusinessLayer.MRF.MRFDetail MRFBL = new Rave.HR.BusinessLayer.MRF.MRFDetail();
            int MRFStatusID;

            //Call the data layer method  & return updated MRF status id.
            MRFStatusID = MRFBL.SetMRFSatusAfterApproval(mrfDetail,ref ConfirmMasg);

            //Refresh the grid
            GetApproveRejectMRFPage();

        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, SET_REASON, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Private Property to Get and Set direction for for sorting
    /// </summary>
    public SortDirection GridViewSortDirection
    {
        get
        {
            if (Session[SORT_DIRECTION] == null)
                Session[SORT_DIRECTION] = SortDirection.Ascending;
            return (SortDirection)Session[SORT_DIRECTION];
        }
        set
        {
            Session[SORT_DIRECTION] = value;
        }

    }

    /// <summary>
    /// To Show image of sorting on column
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
                    sortImage.ImageUrl = CommonConstants.IMAGE_UP_ARROW;
                    sortImage.AlternateText = CommonConstants.ASCENDING;
                }
                else if (_sortDirection == SortOrder.Descending.ToString())
                {
                    sortImage.ImageUrl = CommonConstants.IMAGE_DOWN_ARROW;
                    sortImage.AlternateText = CommonConstants.DESCENDING;
                }

                // Add the image to the appropriate header cell
                switch (sortExpression)
                {
                    case MRFCode:
                        headerRow.Cells[1].Controls.Add(sortImage);
                        break;
                    case CommonConstants.START_DATE:
                        headerRow.Cells[2].Controls.Add(sortImage);
                        break;
                    case CommonConstants.END_DATE:
                        headerRow.Cells[3].Controls.Add(sortImage);
                        break;
                    case MRFRaisedBy:
                        headerRow.Cells[4].Controls.Add(sortImage);
                        break;
                    case CommonConstants.DEPARTMENT:
                        headerRow.Cells[5].Controls.Add(sortImage);
                        break;
                    case CommonConstants.CLIENT_NAME:
                        headerRow.Cells[6].Controls.Add(sortImage);
                        break;
                    case PROJECT_NAME:
                        headerRow.Cells[7].Controls.Add(sortImage);
                        break;
                    case CommonConstants.UTILIZATION:
                        headerRow.Cells[8].Controls.Add(sortImage);
                        break;
                    case CommonConstants.BILLING:
                        headerRow.Cells[9].Controls.Add(sortImage);
                        break;
                    case CommonConstants.ROLE:
                        headerRow.Cells[10].Controls.Add(sortImage);
                        break;
                    case MRFCTC:
                        headerRow.Cells[11].Controls.Add(sortImage);
                        break;
                    case EXPERIENCE:
                        headerRow.Cells[12].Controls.Add(sortImage);
                        break;
                    case RECRUITMENTMANAGER:
                        headerRow.Cells[13].Controls.Add(sortImage);
                        break;
                    case STATUSNAME:
                        headerRow.Cells[14].Controls.Add(sortImage);
                        break;
                    case MRFPURPOSE:
                        headerRow.Cells[15].Controls.Add(sortImage);
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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, ADD_SORT_IMAGE, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// To sorting on grid
    /// </summary>   
    protected void grdvListofPendingApproveRejectHeadCount_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            GridViewRow gvrPager = grdvListofPendingApproveRejectHeadCount.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl(TXT_PAGES);
            txtPages.Text = Session[CURRENT_PAGE_INDEX].ToString();

            //On sorting assign new sort expression
            sortExpression = e.SortExpression;
            if (Session[PREVIOUS_SORT_EXPRESSION] == null)
            {
                Session[PREVIOUS_SORT_EXPRESSION] = sortExpression;
            }
            if (Session[PREVIOUS_SORT_EXPRESSION].ToString() == sortExpression)
            {
                //Sort to opposite direction based upon previous sort direction
                if (Session[SORT_DIRECTION] == null || GridViewSortDirection == SortDirection.Descending)
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
            //Set current sort expression as PreviousSortExpression
            Session[PREVIOUS_SORT_EXPRESSION] = sortExpression;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, GRID_SORTING, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// Get the Email ID of head count approver by department name for sending to approve mail.
    /// </summary>
    /// <param name="DepartmentName"></param>
    /// <returns></returns>
    private string GetEmailIdForDeptWiseHeadCountApprovalMailTo(string DepartmentName)
    {
        string mailTo = string.Empty;
        try
        {
            Rave.HR.BusinessLayer.MRF.MRFDetail MRFDetailsBL = new Rave.HR.BusinessLayer.MRF.MRFDetail();
            List<BusinessEntities.MRFDetail> listMRFDetail = new List<BusinessEntities.MRFDetail>();

            //Get all the data of department wise approver.
            listMRFDetail = MRFDetailsBL.GetEmailIdForHeadCountApproval();
            foreach (BusinessEntities.MRFDetail itemMRF in listMRFDetail)
            {
                //Check whether department name is same then assign approver email id to mailto.
                if (itemMRF.DepartmentName == DepartmentName)
                {
                    mailTo = itemMRF.EmailId;
                    break;
                }
            }
            return mailTo;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetEmailIdForDeptWiseHeadCountApprovalMailTo", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    #endregion
}
