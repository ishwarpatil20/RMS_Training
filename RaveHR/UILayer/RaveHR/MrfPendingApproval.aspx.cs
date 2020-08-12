//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           MrfPendingApproval.aspx.cs    
//  Author:         Gaurav.Thakkar
//  Date written:   24/8/2009/ 12:26:00 PM
//  Description:    This page lists the MRF Which are pending approval of finance
//                  
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  24/8/2009 12:26:00 PM  Gaurav.Thakkar    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Web.UI.WebControls;
using Common;
using System.Data.SqlClient;
using Common.AuthorizationManager;
using Common.Constants;
using System.Web.UI;
using System.Text;
using System.Collections;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class MrfPendingApproval : BaseClass
{

    #region Private Field Members

    /// <summary>
    /// All the parameters to be passed to SP
    /// </summary>
    BusinessEntities.ParameterCriteria objParameter = new BusinessEntities.ParameterCriteria();

    /// <summary>
    /// Defines the common Collection object.
    /// </summary>
    BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

    /// <summary>
    /// Defines the mrfDetail 
    /// </summary>
    private BusinessEntities.MRFDetail mrfDetail;

    /// <summary>
    /// Defines a constant for Page Count
    /// </summary>
    private int pageCount = 0;

    /// <summary>
    /// Defines default value for sorting expression 
    /// </summary>
    private static string sortExpression = string.Empty;

    /// <summary>
    /// Defines Ascending sorting order for grid
    /// </summary>
    private const string ASCENDING = " ASC";

    /// <summary>
    /// Defines Descending sorting order for grid
    /// </summary>
    private const string DESCENDING = " DESC";

    /// <summary>
    /// Defines the image direction either upwards or downwards
    /// </summary>
    private string imageDirection = string.Empty;

    /// <summary>
    /// Defines the command name Previous
    /// </summary>
    private const string PREVIOUS = "Previous";

    /// <summary>
    /// Defines the command name Next
    /// </summary>
    private const string NEXT = "Next";

    /// <summary>
    /// gets the Selected index of the grid view row
    /// </summary>
    private int selectedIndex;

    /// <summary>
    /// Gets the command name of the selected grid view row
    /// </summary>
    private string commandName = string.Empty;

    /// <summary>
    /// Command name Approve
    /// </summary>
    private const string APPROVE = "Approve";

    /// <summary>
    /// Command name Reject
    /// </summary>
    private const string REJECT = "Reject";

    /// <summary>
    /// Command name View
    /// </summary>
    private const string VIEW = "View";

    /// <summary>
    /// Defines the class name.
    /// </summary>
    private const string CLASS_NAME_MRFPENDINGAPPROVAL = "MrfPendingApproval.aspx";

    /// <summary>
    /// Command name sort
    /// </summary>
    private const string SORT = "Sort";

    /// <summary>
    /// Subject for Mail
    /// </summary>
    private string subject;

    /// <summary>
    /// Body for mail
    /// </summary>
    private string body;

    /// <summary>
    /// Authorization manager object
    /// </summary>
    AuthorizationManager objAuMan = new AuthorizationManager();

    /// <summary>
    /// Rave Domain
    /// </summary>
    /// 
    //Googleconfigurable
    //private const string RAVEDOMAIN = "@rave-tech.com";

    /// <summary>
    /// Defines default value EmployeeEmailId 
    /// </summary>
    private static string EmployeeEmailId = string.Empty;

    /// <summary>
    /// Defines default value RaisedByEmailId 
    /// </summary>
    private static string RaisedByEmailId = string.Empty;

    /// <summary>
    /// Store the Value of row count
    /// </summary>
    Hashtable hashTable = new Hashtable();

    /// <summary>
    /// Regards Name to be appended in mail
    /// </summary>
    string RegardsName;

    /// <summary>
    /// Business lyer initialisation.
    /// </summary>
    Rave.HR.BusinessLayer.MRF.MRFDetail mrfUser = new Rave.HR.BusinessLayer.MRF.MRFDetail();

    /// <summary>
    /// Defines default value ReportingToEmailId for Reporting to persons. 
    /// Defines default value for Count of reporting to persons.
    /// </summary>
    private string[] ReportingToByEmailId_PM;
    /// <summary>
    /// 
    /// </summary>
    int countOfReportingTo = 0;
    /// <summary>
    /// 
    /// </summary>
    private string ReportingToByEmailId;
    /// <summary>
    /// Defines default value MailPM
    /// </summary>
    private static string MailPM = string.Empty;

    /// <summary>
    /// Declare Rave.HR.BusinessLayer.Projects.ResourcePlan object
    /// </summary>
    Rave.HR.BusinessLayer.Projects.ResourcePlan objBalResourcePlan = null;

    /// <summary>
    /// Rave Email domain
    /// </summary>
    /// 
    //Googleconfigurable
    //private const string RAVE_DOMAIN = "@rave-tech.com";

    #endregion Private Field Members

    #region Protected Events

    /// <summary>
    /// PageLoad event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //Poonam : Issue : Disable Button : Starts
        btnSaveReject.Attributes.Add(CommonConstants.EVENT_ONCLICK, "javascript:return RejectValidate();");

        btnSaveApprove.OnClientClick = ClientScript.GetPostBackEventReference(btnSaveApprove, null);
        btnSaveReject.OnClientClick = "if(RejectValidate()){" + ClientScript.GetPostBackEventReference(btnSaveReject, null) + "}";
        //Poonam : Issue : Disable Button : Ends

        try
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

                if (!(arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM)||
                    arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEFM)))
                {
                    Response.Redirect(CommonConstants.PAGE_HOME, true);
                }
                //Siddharth 26th August 2015 End


                lblConfirmationMessage.Visible = false;

                if (!IsPostBack)
                {
                    if (Session[SessionNames.MRFVIEWINDEX] != null)
                    {
                        Session.Remove(SessionNames.MRFVIEWINDEX);
                    }

                    //By default on page load set current page index to 1 since 1st page is displayed at
                    //time of pageload.
                    Session[SessionNames.CURRENT_PAGE_INDEX_APPREJMRF] = 1;

                    // Setting the Default sortExpression as MrfCode otherwise the column name which is sorted
                    if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION_APPREJMRF] == null)
                    {
                        sortExpression = CommonConstants.MRF_CODE;
                        Session[SessionNames.PREVIOUS_SORT_EXPRESSION_APPREJMRF] = sortExpression;
                    }
                    else
                    {
                        sortExpression = Session[SessionNames.PREVIOUS_SORT_EXPRESSION_APPREJMRF].ToString();
                    }

                    //Bind the Approve/Reject MRF grid
                    BindGridApproveRejectMrf();
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFPENDINGAPPROVAL, "Page_Load", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// When MRF is approved its status is changed and comment is saved
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSaveApprove_Click(object sender, EventArgs e)
    {
        try
        {
            //Assign the session value to object
            mrfDetail = (BusinessEntities.MRFDetail)Session[SessionNames.MRFDETAIL_APPREJMRF];
            //Assign the reason of approval to object
            mrfDetail.CommentReason = txtComment.Text.ToString();
            //set the MRF status to Closed
            mrfDetail.Status = Common.CommonConstants.MRFStatus_Closed;

            //concurrency check STRAT Mahendra Issue 33860
            Rave.HR.BusinessLayer.MRF.MRFDetail objMRFDetails = new Rave.HR.BusinessLayer.MRF.MRFDetail();
            DataSet dsMRFAllocated = objMRFDetails.checkConcurrencyResourceAllocation(mrfDetail);
            // Rajan Kumar : Issue 46252: 12/02/2014 : Starts                        			 
            // Desc : In MRF history need to implemented in all cases in RMS.
            //Pass Email to know who is going to modified the data            
            AuthorizationManager authoriseduser = new AuthorizationManager();
            mrfDetail.LoggedInUserEmail = authoriseduser.getLoggedInUserEmailId();
            // Rajan Kumar : Issue 46252: 12/02/2014 : END


            if (dsMRFAllocated != null && dsMRFAllocated.Tables[0].Rows.Count == 0)
            {
                //concurrency check END Mahendra Issue 33860

                //Approve the MRF
                SetReasonOfApproveRejectMRF();

                ApprovalMail();
            }
            /*MRF Closure mail should not be sent as discussed on 21/01/2010 with sawita*/
            //MrfClosedMail();

            pnlApprove.Visible = false;

            lblMandatory.Visible = false;
            lblConfirmationMessage.Visible = true;
            lblConfirmationMessage.Text = CommonConstants.MSG_APPROVAL_OF_FINANCE;

        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFPENDINGAPPROVAL, "btnSaveApprove_Click", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// When MRF is rejected its status is changed and comment is saved
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSaveReject_Click(object sender, EventArgs e)
    {
        try
        {
            //Assign the session value to object
            mrfDetail = (BusinessEntities.MRFDetail)Session[SessionNames.MRFDETAIL_APPREJMRF];
            //Assign the reason of rejection to object
            mrfDetail.CommentReason = txtReject.Text.ToString();
            //set the MRF status to Rejected
            mrfDetail.Status = Common.CommonConstants.MRFStatus_Rejected;

            // Rajan Kumar : Issue 46252: 12/02/2014 : Starts                        			 
            // Desc : In MRF history need to implemented in all cases in RMS.
            //Pass Email to know who is going to modified the data            
            AuthorizationManager authoriseduser = new AuthorizationManager();
            mrfDetail.LoggedInUserEmail = authoriseduser.getLoggedInUserEmailId();
            // Rajan Kumar : Issue 46252: 12/02/2014 : END


            //Reject the MRF
            SetReasonOfApproveRejectMRF();

            RejectionMail();

            pnlReject.Visible = false;

            lblMandatory.Visible = false;
            lblConfirmationMessage.Visible = true;
            lblConfirmationMessage.Text = CommonConstants.MSG_REJECTION_OF_FINANCE;

        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFPENDINGAPPROVAL, "btnSaveReject_Click", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
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
            GridViewRow gvrPager = gvPendingApprovalOfMrf.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");

            switch (e.CommandName)
            {
                //Previous button is clicked
                case PREVIOUS:
                    Session[SessionNames.CURRENT_PAGE_INDEX_APPREJMRF] = Convert.ToInt32(txtPages.Text) - 1;
                    txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) - 1);
                    break;

                //Next button is clicked
                case NEXT:
                    Session[SessionNames.CURRENT_PAGE_INDEX_APPREJMRF] = Convert.ToInt32(txtPages.Text) + 1;
                    txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) + 1);
                    break;
            }

            // Bind the grid on paging.
            BindGridApproveRejectMrf();
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFPENDINGAPPROVAL, "ChangePage", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
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
            if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0 && Convert.ToInt32(txtPages.Text) <= Convert.ToInt32(Session[SessionNames.PAGE_COUNT_APPREJMRF].ToString()))
            {
                gvPendingApprovalOfMrf.PageIndex = Convert.ToInt32(txtPages.Text) - 1;
                Session[SessionNames.CURRENT_PAGE_INDEX_APPREJMRF] = txtPages.Text;
            }
            else
            {
                return;
            }

            //Bind the grid on paging
            BindGridApproveRejectMrf();

            //Assign the tetbox current page no.
            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_APPREJMRF].ToString();
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFPENDINGAPPROVAL, "txtPages_TextChanged", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// When data is bound to datarow in gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPendingApprovalOfMrf_DataBound(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrPager = gvPendingApprovalOfMrf.BottomPagerRow;

            if (gvrPager == null) return;

            //Get Text Box and Lable from the Grid View
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            Label lblPageCount = (Label)gvrPager.Cells[0].FindControl("lblPageCount");

            //Assign current page index to text box
            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_APPREJMRF].ToString();

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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFPENDINGAPPROVAL, "gvPendingApprovalOfMrf_DataBound", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Paging for Grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPendingApprovalOfMrf_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (e.NewPageIndex != -1)
            {
                // Assign the new page index
                gvPendingApprovalOfMrf.PageIndex = e.NewPageIndex;
            }

            // Bind the grid as per new page index.
            BindGridApproveRejectMrf();
        }

        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFPENDINGAPPROVAL, "gvPendingApprovalOfMrf_PageIndexChanging", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// When a particular Column is clicked for Sorting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPendingApprovalOfMrf_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            GridViewRow gvrPager = gvPendingApprovalOfMrf.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_APPREJMRF].ToString();

            //On sorting assign new sort expression
            sortExpression = e.SortExpression;

            if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION_APPREJMRF] == null)
            {
                Session[SessionNames.PREVIOUS_SORT_EXPRESSION_APPREJMRF] = sortExpression;
            }

            if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION_APPREJMRF].ToString() == sortExpression)
            {
                //Sort to opposite direction based upon previous sort direction
                if (Session[SessionNames.SORT_DIRECTION_APPREJMRF] == null || GridViewSortDirection == SortDirection.Descending)
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
            Session[SessionNames.PREVIOUS_SORT_EXPRESSION_APPREJMRF] = sortExpression;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFPENDINGAPPROVAL, "gvPendingApprovalOfMrf_Sorting", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// The RowCreated event is raised when each row in the GridView control is created
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPendingApprovalOfMrf_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (((Session[SessionNames.PAGE_COUNT_APPREJMRF] != null) && (int.Parse(Session[SessionNames.PAGE_COUNT_APPREJMRF].ToString()) > 1)) || ((raveHRCollection.Count > 1)))
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFPENDINGAPPROVAL, "gvPendingApprovalOfMrf_RowCreated", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// When image of either "Approve","Reject" or "View" is clicked this event is captured and
    /// the command associated with it is executed.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPendingApprovalOfMrf_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName != SORT)
            {
                GridView gridView = (GridView)sender;
                selectedIndex = int.Parse(e.CommandArgument.ToString());
                commandName = e.CommandName;

                gvPendingApprovalOfMrf.SelectedIndex = selectedIndex;
                mrfDetail = new BusinessEntities.MRFDetail();

                GridViewRow gvRow = gvPendingApprovalOfMrf.Rows[selectedIndex];
                Label lbl = (Label)gvRow.Cells[0].FindControl("lblMrfId");
                Label lblEmpId = (Label)gvRow.FindControl("lblEMPId");
                Label lblFunctionalManager = (Label)gvRow.FindControl("lblFunctionalManager");

                //Extract the MrfId and role From grid.
                //Role is extracted to check if role is PM/SPM/APM than it is handled in different manner.
                mrfDetail.MRFId = Convert.ToInt32(lbl.Text);
                mrfDetail.MRFCode = gvRow.Cells[1].Text;
                mrfDetail.EmployeeName = gvRow.Cells[2].Text;
                mrfDetail.ResourceOnBoard = DateTime.Parse(gvRow.Cells[3].Text);
                mrfDetail.DepartmentName = gvRow.Cells[6].Text;
                mrfDetail.ProjectName = gvRow.Cells[8].Text;
                mrfDetail.Role = gvRow.Cells[10].Text;
                mrfDetail.EmployeeId = Convert.ToInt32(lblEmpId.Text);
                mrfDetail.ClientName = gvRow.Cells[9].Text;
                mrfDetail.FunctionalManager = lblFunctionalManager.Text;

                Session[SessionNames.MRFDETAIL_APPREJMRF] = mrfDetail;

                //Depending upon the comand, perform the action.
                switch (commandName)
                {
                    case APPROVE:
                        pnlApprove.Visible = true;
                        pnlReject.Visible = false;
                        txtComment.Focus();
                        //This line of code is added since on click of "Approve" the Sort image used to get disappear.
                        //The "If" condition check is made since for One record sort image is not required.
                        if (gvPendingApprovalOfMrf.Rows.Count > 1)
                        {
                            AddSortImage(gvPendingApprovalOfMrf.HeaderRow);
                        }
                        //Mandatory label is made visible.
                        lblMandatory.Visible = true;
                        break;

                    case REJECT:
                        pnlApprove.Visible = false;
                        pnlReject.Visible = true;
                        txtReject.Focus();
                        //This line of code is added since on click of "Reject" the Sort image used to get disappear.
                        //The "If" condition check is made since for One record sort image is not required.
                        if (gvPendingApprovalOfMrf.Rows.Count > 1)
                        {
                            AddSortImage(gvPendingApprovalOfMrf.HeaderRow);
                        }
                        //Mandatory label is made visible.
                        lblMandatory.Visible = true;
                        break;

                    case VIEW:
                        string url = "MrfView.aspx?" + URLHelper.SecureParameters("MRFId", mrfDetail.MRFId.ToString()) + "&" + URLHelper.SecureParameters("index", gvRow.RowIndex.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "APPREJFINANCE") + "&" + URLHelper.CreateSignature(mrfDetail.MRFId.ToString(), gvRow.RowIndex.ToString(), "APPREJFINANCE");
                        Response.Redirect(url, false);
                        //Server.Transfer(url);
                        break;
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFPENDINGAPPROVAL, "gvPendingApprovalOfMrf_RowCommand", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    #endregion Protected Events

    #region Private Methods

    /// <summary>
    /// Binds the Approve/Reject MRF Grid
    /// </summary>
    private void BindGridApproveRejectMrf()
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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFPENDINGAPPROVAL, "BindGridApproveRejectMrf", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Get or set the GridViewSortDirection property
    /// </summary>
    private SortDirection GridViewSortDirection
    {
        get
        {
            if (Session[SessionNames.SORT_DIRECTION_APPREJMRF] == null)
                Session[SessionNames.SORT_DIRECTION_APPREJMRF] = SortDirection.Ascending;

            return (SortDirection)Session[SessionNames.SORT_DIRECTION_APPREJMRF];
        }

        set
        {
            Session[SessionNames.SORT_DIRECTION_APPREJMRF] = value;
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
                objParameter.SortExpressionAndDirection = sortExpression + direction;
            }

            raveHRCollection = GetApproveRejectMrf();

            if ((int.Parse(Session[SessionNames.PAGE_COUNT_APPREJMRF].ToString()) == 1) && (raveHRCollection.Count == 1))
            {
                gvPendingApprovalOfMrf.AllowSorting = false;
            }
            else
            {
                gvPendingApprovalOfMrf.AllowSorting = true;
            }

            if (raveHRCollection.Count == 0)
            {
                gvPendingApprovalOfMrf.DataSource = raveHRCollection;
                gvPendingApprovalOfMrf.DataBind();

                ShowHeaderWhenEmptyGrid(raveHRCollection);
            }
            else
            {
                gvPendingApprovalOfMrf.DataSource = raveHRCollection;
                gvPendingApprovalOfMrf.DataBind();
                raveHRCollection.Clear();

            }

            //Finds the Paging Text Box, Previous and Next Link Buttons in Grid Views Pagning Templet
            GridViewRow gvrPager = gvPendingApprovalOfMrf.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            LinkButton lbtnPrevious = (LinkButton)gvrPager.Cells[0].FindControl("lbtnPrevious");
            LinkButton lbtnNext = (LinkButton)gvrPager.Cells[0].FindControl("lbtnNext");

            if (pageCount > 1)
            {
                gvPendingApprovalOfMrf.BottomPagerRow.Visible = true;
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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFPENDINGAPPROVAL, "SortGridView", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Gets the MRF which are to be displayed in the grid
    /// </summary>
    /// <returns>Collection</returns>
    private BusinessEntities.RaveHRCollection GetApproveRejectMrf()
    {
        Rave.HR.BusinessLayer.MRF.MRFDetail mrfSummaryBL = new Rave.HR.BusinessLayer.MRF.MRFDetail();
        BusinessEntities.MRFDetail mrfDetail = new BusinessEntities.MRFDetail();
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();


        try
        {
            if (gvPendingApprovalOfMrf.BottomPagerRow != null)
            {
                GridViewRow gvrPager = gvPendingApprovalOfMrf.BottomPagerRow;
                TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
                if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0)
                {
                    objParameter.PageNumber = int.Parse(txtPages.Text);
                    objParameter.PageSize = 10;
                }
            }
            else
            {
                objParameter.PageNumber = 1;
                objParameter.PageSize = 10;
            }

            raveHRCollection = mrfSummaryBL.GetApproveRejectMrf(objParameter, ref pageCount);

            Session[SessionNames.PAGE_COUNT_APPREJMRF] = pageCount;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFPENDINGAPPROVAL, "GetApproveRejectMrf", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }

        return raveHRCollection;
    }

    /// <summary>
    /// Approve/Reject MRF
    /// </summary>
    private void SetReasonOfApproveRejectMRF()
    {
        try
        {
            // Mohamed : 12/02/2015 : Starts                        			  
            // Desc : Make a single code for normal employee and (PM,APM,SPM,AVP,PRM)

            
            // Rajan Kumar : Issue 48183 : 24/01/2014 : Starts                        			 
            // Desc : When employee(PM or AVP) assigned or released from project then project level PM access on project should get assigned or removed.
            //Added AVP flag in if condition           
            //if ((mrfDetail.Role != CommonConstants.APM) && (mrfDetail.Role != CommonConstants.PM) && (mrfDetail.Role != CommonConstants.SPM) && (mrfDetail.Role != CommonConstants.AVP) && (mrfDetail.Role != CommonConstants.PRM))
            //{
                //Updated the Status of MRF and REason
                if (Convert.ToBoolean(Rave.HR.BusinessLayer.MRF.MRFDetail.SetMRFApproveRejectReason(mrfDetail)))
                {
                    //Email Notification is sent.
                    //Response.Redirect(CommonConstants.Page_MrfPendingApproval, false);
                    BindGridApproveRejectMrf();
                    gvPendingApprovalOfMrf.SelectedIndex = -1;
                }
                else
                {

                    Response.Redirect(CommonConstants.Page_MrfPendingApproval, false);
                }
            //}
            //else
            //{
            //    //Updated the Status of MRF and REason
            //    if (Convert.ToBoolean(Rave.HR.BusinessLayer.MRF.MRFDetail.SetMRFApproveRejectReasonForPM(mrfDetail)))
            //    {
            //        //Email Notification is sent.
            //        //Response.Redirect(CommonConstants.Page_MrfPendingApproval, false);
            //        BindGridApproveRejectMrf();
            //        gvPendingApprovalOfMrf.SelectedIndex = -1;
            //    }
            //    else
            //    {

            //        Response.Redirect(CommonConstants.Page_MrfPendingApproval, false);
            //    }
            //}
            // Mohamed : 12/02/2015 : Ends
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFPENDINGAPPROVAL, "SetReasonOfApproveRejectMRF", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
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
            gvPendingApprovalOfMrf.ShowHeader = true;
            // Disable sorting
            gvPendingApprovalOfMrf.AllowSorting = false;

            //Create empty datasource for Grid view and bind
            raveHRCollection.Add(new BusinessEntities.MRFDetail());
            gvPendingApprovalOfMrf.DataSource = raveHRCollection;
            gvPendingApprovalOfMrf.DataBind();

            //Hide the column for images:Approve/Reject/View
            gvPendingApprovalOfMrf.Columns[13].Visible = false;
            gvPendingApprovalOfMrf.Columns[14].Visible = false;
            gvPendingApprovalOfMrf.Columns[15].Visible = false;

            //clear all the cells in the row
            gvPendingApprovalOfMrf.Rows[0].Cells.Clear();

            //add a new blank cell
            gvPendingApprovalOfMrf.Rows[0].Cells.Add(new TableCell());
            gvPendingApprovalOfMrf.Rows[0].Cells[0].Text = CommonConstants.NO_RECORDS_FOUND_MESSAGE;
            gvPendingApprovalOfMrf.Rows[0].Cells[0].Wrap = false;
            gvPendingApprovalOfMrf.Rows[0].Cells[0].Width = Unit.Percentage(10);

        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFPENDINGAPPROVAL, "ShowHeaderWhenEmptyGrid", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
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
                    sortImage.AlternateText = "Ascending";
                }
                else if (imageDirection == SortOrder.Descending.ToString())
                {
                    sortImage.ImageUrl = CommonConstants.IMAGE_DOWN_ARROW;
                    sortImage.AlternateText = "Descending";
                }

                // Add the image to the appropriate header cell
                switch (sortExpression)
                {
                    case CommonConstants.MRF_CODE:
                        headerRow.Cells[1].Controls.Add(sortImage);
                        break;

                    case CommonConstants.RESOURCE_NAME:
                        headerRow.Cells[2].Controls.Add(sortImage);
                        break;

                    case CommonConstants.START_DATE:
                        headerRow.Cells[3].Controls.Add(sortImage);
                        break;

                    case CommonConstants.END_DATE:
                        headerRow.Cells[4].Controls.Add(sortImage);
                        break;


                    case CommonConstants.BILLING:
                        headerRow.Cells[5].Controls.Add(sortImage);
                        break;

                    case CommonConstants.DEPARTMENT:
                        headerRow.Cells[6].Controls.Add(sortImage);
                        break;

                    case CommonConstants.RAISE_BY:
                        headerRow.Cells[7].Controls.Add(sortImage);
                        break;

                    case CommonConstants.PROJECT_NAME:
                        headerRow.Cells[8].Controls.Add(sortImage);
                        break;

                    case CommonConstants.CLIENT_NAME:
                        headerRow.Cells[9].Controls.Add(sortImage);
                        break;

                    case CommonConstants.ROLE:
                        headerRow.Cells[10].Controls.Add(sortImage);
                        break;

                    case CommonConstants.TARGET_CTC:
                        headerRow.Cells[11].Controls.Add(sortImage);
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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFPENDINGAPPROVAL, "AddSortImage", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Mail for Approval
    /// </summary>
    private void ApprovalMail()
    {
        mrfDetail = (BusinessEntities.MRFDetail)Session[SessionNames.MRFDETAIL_APPREJMRF];
        BusinessEntities.MRFDetail mailDetail = new BusinessEntities.MRFDetail();
        Rave.HR.BusinessLayer.MRF.MRFDetail mrfSummaryBL = new Rave.HR.BusinessLayer.MRF.MRFDetail();
        mailDetail = mrfSummaryBL.GetMailingDetails(mrfDetail);
        EmployeeEmailId = mailDetail.EmailId;
        RaisedByEmailId = mailDetail.RaisedBy;
        int[] IDs;
        Boolean B_ClientName = false;
        string ClientName = string.Empty;

        //Added by Kanchan for the requirment specified in the Discussion with Sawita Kamath and Gaurav Thakkar.
        // Requirment raised: When a mrf is approved the approval mail should go to the reporting to.
        if (mailDetail.ReportingToId != null)
        {
            if (mailDetail.ReportingToId.Contains(","))
            {
                // countOfReportingTo = mailDetail.ReportingToId.LastIndexOf(",");
                string[] allReportingTo;
                allReportingTo = mailDetail.ReportingToId.Split(',');
                countOfReportingTo = allReportingTo.Length;
                IDs = new int[countOfReportingTo];
                ReportingToByEmailId_PM = new string[countOfReportingTo];

                for (int i = 0; i < countOfReportingTo; i++)
                {
                    IDs[i] = Convert.ToInt32(allReportingTo[i].ToString());
                    ReportingToByEmailId_PM[i] = getMailID(IDs[i]);
                }
            }
            else
            {
                ReportingToByEmailId = getMailID(Convert.ToInt32(mailDetail.ReportingToId));
            }
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            raveHRCollection = mrfSummaryBL.GetProjectManagerByProjectId(mailDetail);
            BusinessEntities.MRFDetail forPm = new BusinessEntities.MRFDetail();
            string Mail = string.Empty;
            MailPM = string.Empty;
            foreach (BusinessEntities.MRFDetail mrf in raveHRCollection)
            {
                Mail = string.Empty;
                forPm = (BusinessEntities.MRFDetail)raveHRCollection.Item(0);
                if (!forPm.PmID.Contains(","))
                {
                    Mail = getMailID(Convert.ToInt32(forPm.EmployeeId));
                }
                else
                {
                    string[] MailsForPm = forPm.PmID.Split(',');
                    foreach (string id in MailsForPm)
                    {
                        Mail += getMailID(Convert.ToInt32(id)) + ",";
                    }
                }
                MailPM += "," + Mail;
                if (MailPM.EndsWith(","))
                {
                    MailPM = MailPM.TrimEnd(',');
                }
            }
        }

        //Siddharth 26-02-2015 Start
        ClientName = mrfDetail.ClientName;
        if (ClientName.ToUpper().Contains("NPS") || ClientName.ToUpper().Contains("NORTHGATE"))
            B_ClientName = true;
                //Siddharth 26-02-2015 End

        #region Coded By Sameer For New MailFunctionality

        Rave.HR.BusinessLayer.MRF.MRFDetail objBLMRF = new Rave.HR.BusinessLayer.MRF.MRFDetail();
        objBLMRF.SendMailToWhizibleForInternalResourceAllocation(mrfDetail, mailDetail, B_ClientName);

        #endregion Coded By Sameer For New MailFunctionality

    }

    /// <summary>
    /// Mail for Rejection of MRF
    /// </summary>
    private void RejectionMail()
    {
        mrfDetail = (BusinessEntities.MRFDetail)Session[SessionNames.MRFDETAIL_APPREJMRF];
        BusinessEntities.MRFDetail mailDetail = new BusinessEntities.MRFDetail();
        Rave.HR.BusinessLayer.MRF.MRFDetail mrfSummaryBL = new Rave.HR.BusinessLayer.MRF.MRFDetail();

        mailDetail = mrfSummaryBL.GetMailingDetailsOfFunctionalManagerAndReportingTo(mrfDetail);
        
        //mailDetail = mrfSummaryBL.GetMailingDetails(mrfDetail);
        RaisedByEmailId = mailDetail.RaisedBy;

        if (mrfDetail != null)
        {
            #region Code For New MailFunctionality

            Rave.HR.BusinessLayer.MRF.MRFDetail objBLMRF = new Rave.HR.BusinessLayer.MRF.MRFDetail();
            objBLMRF.SendMailToRPMForInternalResourceRejection(mrfDetail, mailDetail);

            #endregion Code For New MailFunctionality
        }

    }

    /// <summary>
    /// Link to be appended in mail
    /// </summary>
    /// <returns></returns>
    private string GetLink()
    {
        string sComp = Utility.GetUrl() + CommonConstants.Page_MrfSummary;
        return sComp;
    }

    /// <summary>
    /// Added by Kanchan for the requirment specified in the Discussion with Sawita Kamath and Gaurav Thakkar.
    /// Requirment raised:When a mrf is approved the approval mail should go to the reporting to.
    /// Give the emailId for the employee whose Employee id is supplied.
    /// </summary>
    /// <param name="empID"></param>
    /// <returns></returns>
    private string getMailID(int empID)
    {
        string ReportingToMailID = string.Empty;
        try
        {
            Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
            ReportingToMailID = master.GetEmailID(empID);
            return ReportingToMailID;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFPENDINGAPPROVAL, "getMailID", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    #endregion Private Methods

}
