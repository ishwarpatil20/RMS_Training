//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           ApproveRejectRP.aspx.cs    
//  Author:         Sameer.Chornele
//  Date written:   27/8/2009/ 12:01:00 PM
//  Description:    This Page will be the entry page for Approve and Reject Resource Plan
//  Amendments
//  Date                        Who           Ref     Description
//  ----                      -----------     ---     -----------
//  27/8/2009/ 12:01:00 PM  Sameer.Chornele   n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Common;
using Common.Constants;
using BusinessEntities;
using System.Text;
using System.Collections;
using Common.AuthorizationManager;

public partial class ApproveRejectRP : BaseClass
{

    #region Private Field Members

    /// <summary>
    /// define block
    /// </summary>
    private const string block = "block";

    /// <summary>
    /// define collapse
    /// </summary>
    private const string collapse = "Collapse";

    /// <summary>
    /// define none
    /// </summary>
    private const string none = "none";

    /// <summary>
    /// define childGridApproveRejectResourcePlan
    /// </summary>
    private const string childGridApproveRejectResourcePlan = "ChildGridApproveRejectResourcePlan";

    /// <summary>
    /// define imgbtnExpandCollaspeChildGrid
    /// </summary>
    private const string imagebuttonExpandCollaspeChildGrid = "imgbtnExpandCollaspeChildGrid";

    /// <summary>
    /// define grdResourcePlan
    /// </summary>
    private const string gridResourcePlan = "grdResourcePlan";

    /// <summary>
    /// define tr_ResourcePlan
    /// </summary>
    private const string childRowResourcePlan = "tr_ResourcePlan";

    /// <summary>
    /// define hdnFAppRej
    /// </summary>
    private const string hiddenFieldAppRej = "hdnFAppRej";

    /// <summary>
    /// define hdnFRPID
    /// </summary>
    private const string hiddenFieldRPID = "hdnFRPID";

    /// <summary>
    /// define dateFormat
    /// </summary>
    private const string dateFormat = "dd/MM/yyyy";

    /// <summary>
    /// define emptyString
    /// </summary>
    private const string emptyString = " ";

    /// <summary>
    /// define buttonClickValidate
    /// </summary>
    private const string buttonClickValidate = "return ButtonClickValidate();";

    /// <summary>
    /// define validateControl
    /// </summary>
    private const string validateControl = "return ValidateControl('";

    /// <summary>
    /// define commaSepration
    /// </summary>
    private const string commaSepration = "','";

    /// <summary>
    /// define closeBracket
    /// </summary>
    private const string closeBracket = "');";

    /// <summary>
    /// define showTooltip
    /// </summary>
    private const string showTooltip = "ShowTooltip('";

    /// <summary>
    /// define hideTooltip
    /// </summary>
    private const string hideTooltip = "HideTooltip('";

    /// <summary>
    /// define approveImageBtn
    /// </summary>
    private const string approveImageBtn = "ApproveImageBtn";

    /// <summary>
    /// define rejectImageBtn
    /// </summary>
    private const string rejectImageBtn = "RejectImageBtn";

    /// <summary>
    /// define viewImageBtn
    /// </summary>
    private const string viewImageBtn = "ViewImageBtn";

    /// <summary>
    /// define reasonForApproval
    /// </summary>
    private const string reasonForApproval = "Reason For Approval";

    /// <summary>
    /// define reasonForRejection
    /// </summary>
    private const string reasonForRejection = "Reason For Rejection";

    /// <summary>
    /// define resorcePlanApprovalStatus
    /// </summary>
    private const string resorcePlanApprovalStatus = "ResorcePlanApprovalStatus";

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
    /// Defines a constant for Page Count
    /// </summary>
    private int pageCount = 0;

    /// <summary>
    /// Defines a currentPageIndex
    /// </summary>
    private string currentPageIndex = "currentPageIndex";

    /// <summary>
    /// Defines a lblProjectName
    /// </summary>
    private string lblProjectName = "lblProjectName";

    /// <summary>
    /// Defines a lblchildProjectName
    /// </summary>
    private string lblchildProjectName = "lblchildProjectName";

    /// <summary>
    /// Defines Generic List for Resource Plan Data
    /// </summary>
    RaveHRCollection objListResourcePlan = new RaveHRCollection();

    /// <summary>
    /// define CLASS_NAME_RP
    /// </summary>
    private const string CLASS_NAME_RP = CommonConstants.APPROVEREJECTRP_PAGE;

    /// <summary>
    /// Rave Email domain
    /// </summary>
    /// 
    //Googleconfigurable
    //private const string RAVE_DOMAIN = "@rave-tech.com";

    /// <summary>
    /// declare NO_RECORDS_FOUND_MESSAGE
    /// </summary>
    private const string NO_RECORDS_FOUND_MESSAGE = "No records found.";

    /// <summary>
    /// declare SHOW_HEADER_WHEN_EMPTY_GRID
    /// </summary>
    private const string SHOW_HEADER_WHEN_EMPTY_GRID = "ShowHeaderWhenEmptyGrid";

    /// <summary>
    /// declare Row_ProjectId
    /// </summary>
    private const string Row_ProjectId = "Row_ProjectId";

    /// <summary>
    /// declare LabelRPApprovalStatusId
    /// </summary>
    private const string LabelRPApprovalStatusId = "lblRPApprovalStatusId";

    /// <summary>
    /// declare Pending_For_Approval
    /// </summary>
    private const string Pending_For_Approval = "Pending For Approval";

    /// <summary>
    /// declare ApproveImageButton
    /// </summary>
    private const string ApproveImageButton = "imgApprove";

    /// <summary>
    /// declare RejectImageButton
    /// </summary>
    private const string RejectImageButton = "imgReject";

    /// <summary>
    /// declare ApproveOrReject
    /// </summary>
    private const string ApproveOrReject = "ApproveOrReject";

    /// <summary>
    /// declare RPApproved
    /// </summary>
    private const string RPApproved = "approved";

    /// <summary>
    /// declare RPRejected
    /// </summary>
    private const string RPRejected = "rejected";

    /// <summary>
    /// declare MessagetextCss
    /// </summary>
    private const string MessagetextCss = "Messagetext";

    /// <summary>
    /// declare Local_Address
    /// </summary>
    private const string Local_Address = "Local_Addr";

    /// <summary>
    /// declare Path_Information
    /// </summary>
    private const string Path_Information = "PATH_INFO";

    string UserRaveDomainId;
    string UserMailId;
    ArrayList arrRolesForUser = new ArrayList();

    #endregion Private Field Members

    #region Protected Methods

    /// <summary>
    /// Page Load Event Handler.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!ValidateURL())
            {
                Response.Redirect(CommonConstants.INVALIDURL, false);
            }
            else
            {
                //Siddharth 26th August 2015 Start
                //Task ID:- 56487 Hide the pages access for normal employees
                AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
                UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
                UserMailId = UserRaveDomainId.Replace("co.in", "com");
                arrRolesForUser = objRaveHRAuthorizationManager.getRolesForUser(UserRaveDomainId);

                if (!(arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM) ||
                      arrRolesForUser.Contains(AuthorizationManagerConstants.ROLECOO)))
                {
                    Response.Redirect(CommonConstants.PAGE_HOME, true);
                }
                //Siddharth 26th August 2015 End


                lblMandatory.Text = "Fields marked with <span class='mandatorymark'>*</span> are mandatory.";
                lblMandatory.Visible = false;

                //if page loads first time.
                if (!IsPostBack)
                {
                    //--Authorise User
                    AuthorizeUserForPageView();

                    //--Set current page index.
                    ViewState[currentPageIndex] = 1;

                    //--Default sort expression.
                    if (ViewState[SessionNames.PREVIOUS_SORT_EXPRESSION] == null)
                    {
                        //sorting on rownumber.
                        sortExpression = DbTableColumn.Con_ProjectCodeAbbrivation;

                        //assign to viewstate.
                        ViewState[SessionNames.PREVIOUS_SORT_EXPRESSION] = sortExpression;
                    }
                    else
                    {
                        //assign previuous sort expression.
                        sortExpression = ViewState[SessionNames.PREVIOUS_SORT_EXPRESSION].ToString();
                    }

                    //display project details for approve/reject resource plan grid
                    GridApproveRejectRP();

                }

                //when user click on save button
                saveBtn.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, buttonClickValidate);
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "Page_Load", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// this method is for expanding resouce plan child grid if project parent grid contains resouce plan
    /// </summary>
    protected void grdPendingResourcePlan_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //if commandname is child grid for approve/reject resource plan.
            if (e.CommandName == childGridApproveRejectResourcePlan)
            {
                //create gridviewrow object of approve/reject resource plan.
                GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                //create imagebutton and assign imagebutton value as "+" icon of gridviewrow.
                ImageButton imgbtnExpandCollaspeChildGrid = (ImageButton)grv.FindControl(imagebuttonExpandCollaspeChildGrid);

                //create gridview and assign child resource plan grid value to it.
                GridView grdResourcePlan = (GridView)grv.FindControl(gridResourcePlan);

                //create htmltablerow and assign child resource plan row value to it.
                HtmlTableRow tr_ResourcePlan = (HtmlTableRow)grv.FindControl(childRowResourcePlan);

                //create hiddenfield and assign child resource plan hiddenfield value to it.
                HiddenField hdnFAppRej = (HiddenField)grv.Cells[0].FindControl(hiddenFieldAppRej);

                //create Label and assign resource plan project Name value to it.
                Label labelProjectName = (Label)grv.Cells[0].FindControl(lblProjectName);

                //create label to get list of resource plan for particular project.
                Label labelchildProjectName = (Label)grv.Cells[0].FindControl(lblchildProjectName);

                //assign value to lblchildProjectName label to display project name with list of resource plan.
                labelchildProjectName.Text = labelProjectName.Text;

                ViewState["ProjectName"] = labelProjectName.Text;

                //collapse resource plan grid.
                //if image url is equal to "-" icon image.
                if (imgbtnExpandCollaspeChildGrid.ImageUrl == Common.CommonConstants.IMAGE_MINUSSIGNPATH)
                {
                    //display none when user clicks "-" icon
                    tr_ResourcePlan.Style.Add(HtmlTextWriterStyle.Display, none);

                    //replace "-" icon with "+" when user clicks on collapse("-")image button.
                    imgbtnExpandCollaspeChildGrid.ImageUrl = Common.CommonConstants.IMAGE_PLUSSIGNPATH;

                    //reset child grid expand status
                    ViewState[Row_ProjectId] = null;

                    if (CommentPanel.Visible)
                        CommentPanel.Visible = false;

                    return;
                }


                //--Collaspe the child grids
                foreach (GridViewRow grvRow in grdPendingResourcePlan.Rows)
                {
                    //create imagebutton and assign imagebutton value as "+" icon of gridviewrow.
                    ImageButton imgbtn = (ImageButton)grvRow.FindControl(imagebuttonExpandCollaspeChildGrid);

                    //create htmltablerow and assign child resource plan row value to it.
                    HtmlTableRow tr_RP = (HtmlTableRow)grvRow.FindControl(childRowResourcePlan);

                    //display none when user clicks "-" icon
                    tr_RP.Style.Add(HtmlTextWriterStyle.Display, none);

                    //replace "-" icon with "+" when user clicks on collapse("-")image button.
                    imgbtn.ImageUrl = Common.CommonConstants.IMAGE_PLUSSIGNPATH;

                }

                //--Bind Grid
                BindChildGridResourcePlanDetail(int.Parse(e.CommandArgument.ToString()), grdResourcePlan);

                //--Checks if Resource Plan is Pending For Approval/Rejection then only approve/reject image buttons are visible.
                foreach (GridViewRow objResourcePlan in grdResourcePlan.Rows)
                {
                    if (((Label)objResourcePlan.FindControl(LabelRPApprovalStatusId)).Text != Pending_For_Approval)
                    {
                        ((ImageButton)objResourcePlan.FindControl(ApproveImageButton)).Enabled = false;
                        ((ImageButton)objResourcePlan.FindControl(ApproveImageButton)).Visible = false;
                        ((ImageButton)objResourcePlan.FindControl(RejectImageButton)).Enabled = false;
                        ((ImageButton)objResourcePlan.FindControl(RejectImageButton)).Visible = false;
                    }
                    else
                    {
                        ((ImageButton)objResourcePlan.FindControl(ApproveImageButton)).Enabled = true;
                        ((ImageButton)objResourcePlan.FindControl(ApproveImageButton)).Visible = true;
                        ((ImageButton)objResourcePlan.FindControl(RejectImageButton)).Enabled = true;
                        ((ImageButton)objResourcePlan.FindControl(RejectImageButton)).Visible = true;
                    }
                }

                //expand resource plan child grid
                if ((imgbtnExpandCollaspeChildGrid != null) && (tr_ResourcePlan != null))
                {
                    //replace "+" icon with "-" when user clicks on collapse("+")image button.
                    imgbtnExpandCollaspeChildGrid.ImageUrl = Common.CommonConstants.IMAGE_MINUSSIGNPATH;

                    //display block when user clicks "+" icon
                  //Name: Sanju Kushwaha. Removed display property  so that it should display grid properly in IE10,Chrome and mozilla browser.
                    tr_ResourcePlan.Style.Add(HtmlTextWriterStyle.Display, "");

                    //assign tooltip tip to image button.
                    imgbtnExpandCollaspeChildGrid.ToolTip = collapse;

                    //--maintain child grid expand status
                    ViewState[Row_ProjectId] = imgbtnExpandCollaspeChildGrid.CommandArgument.ToString();
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "grdPendingResourcePlan_RowCommand", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// This Method fires on Approve/Reject Image button ClickEventHanler for Adding comments for selected 
    /// Resource Plan.
    /// </summary>
    protected void grdChildRowPendingResourcePlan_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            //create GridViewRow object of resource plan grid and assign value to it.
            GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            Label RPCode = (Label)grv.FindControl("RPCode");

            //assign seleted row index value
            int _selectedIndex = int.Parse(e.CommandArgument.ToString());

            //create gridview object
            GridView grvObj = (GridView)((grv.Parent).Parent);

            //assgin selected index
            grvObj.SelectedIndex = _selectedIndex;

            //if commandname approveimage button of resource plan.
            if (e.CommandName == approveImageBtn)
            {
                ViewState[ApproveOrReject] = RPApproved;
                hdnFieldApproveOrReject.Value = RPApproved;

                ViewState[resorcePlanApprovalStatus] = Convert.ToInt32(MasterEnum.ResourcePlanApprovalStatus.Approved);
                lblMandatory.Visible = true;
            }

            //if commandname approveimage button of resource plan.
            if (e.CommandName == rejectImageBtn)
            {
                ViewState[ApproveOrReject] = RPRejected;
                hdnFieldApproveOrReject.Value = RPRejected;

                ViewState[resorcePlanApprovalStatus] = Convert.ToInt32(MasterEnum.ResourcePlanApprovalStatus.Rejected);
                lblMandatory.Visible = true;
            }

            //Makes CommentPanel visible
            CommentPanel.Visible = true;

            //create hiddenfield object and assign value to it.
            HiddenField hdnFRPID = (HiddenField)grv.Cells[0].FindControl(hiddenFieldRPID);

            //Assign ResourcePlanId value to viewstate
            ViewState[hiddenFieldRPID] = hdnFRPID.Value;


            //if commandname viewimage button of resource plan.
            if (e.CommandName == viewImageBtn)
            {
                //Makes CommentPanel visibility false
                CommentPanel.Visible = false;

                Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLViewApproveRejectRP = new Rave.HR.BusinessLayer.Projects.ResourcePlan();
                
                //RPIssue : 33126 Start
                //objBLLViewApproveRejectRP.ViewRPInExcel(int.Parse(hdnFRPID.Value), RPCode.Text);
                objBLLViewApproveRejectRP.ViewRPInExcel(int.Parse(hdnFRPID.Value), RPCode.Text, "BeforeApproveMode");
                //RPIssue : 33126 End
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (System.Threading.ThreadAbortException ex) { }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "grdChildRowPendingResourcePlan_RowCommand", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// Save Commnets For Approve/Reject Resource Plan
    /// </summary>
    protected void btnSaveComment_ClickEvtHandler(object sender, EventArgs e)
    {
        try
        {
            //validate comment textbox value
            if (ValidateApproveRejectControl())
            {
                //create object of BusinessEntities.ResourcePlan
                BusinessEntities.ResourcePlan objBEApproveRejectRP = new BusinessEntities.ResourcePlan();

                //assign comment entered by user.
                objBEApproveRejectRP.ReasonForApproval = txtComments.Text;

                //Get logged in user
                Common.AuthorizationManager.AuthorizationManager objAuMan = new Common.AuthorizationManager.AuthorizationManager();

                //method to get logged in user.
                string strCurrentUser = objAuMan.getLoggedInUserEmailId();

                //assign logged in user id as approverid.
                objBEApproveRejectRP.ApproverId = strCurrentUser;

                //assign approval date.
                objBEApproveRejectRP.ResourcePlanApprovalDate = Convert.ToDateTime(DateTime.Now.ToString(dateFormat));

                //assign resource plan id value.
                objBEApproveRejectRP.RPId = Convert.ToInt32(ViewState[hiddenFieldRPID]);

                //assign resource plan id value.
                objBEApproveRejectRP.RPApprovalStatusId = Convert.ToInt32(ViewState[resorcePlanApprovalStatus]);

                //create object of businesslayer of resourceplan
                Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLApproveRejectRP = new Rave.HR.BusinessLayer.Projects.ResourcePlan();

                // Initialise the Collection class object
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

                //Concurrency Handled RP 33126
                int? RPApprovalstatus = objBLLApproveRejectRP.GetRPApprovalStatus(objBEApproveRejectRP.RPId);
                // Added If condition
                if (RPApprovalstatus == null)
                {
                    //END Concurrency Handled RP 33126

                    //Check if Resource Plan is rejected if not get Resouce Plan details for Mails. 
                    if (objBEApproveRejectRP.RPApprovalStatusId != Convert.ToInt32(MasterEnum.ResourcePlanApprovalStatus.Rejected))
                    {
                        //method to get resourceplan for ApproveRejectRP
                        raveHRCollection = objBLLApproveRejectRP.GetRPEditedDetailsForMail(objBEApproveRejectRP);
                    }
                    //method to add reason for resource plan during approval or rejection.
                    string objListApproveRejectRP = objBLLApproveRejectRP.AddReasonForApproveRejectRP(objBEApproveRejectRP);

                    //if user has not entered any comments.
                    if (!string.IsNullOrEmpty(objListApproveRejectRP))
                    {
                        //makes comment panel visibility false.
                        CommentPanel.Visible = false;

                        //assign null value to comment text.
                        txtComments.Text = emptyString;

                        //assign css
                        lblMandatory.CssClass = MessagetextCss;

                        //for approval
                        if (Convert.ToInt32(ViewState[resorcePlanApprovalStatus].ToString()) == Convert.ToInt32(MasterEnum.ResourcePlanApprovalStatus.Approved))
                        {
                            lblMandatory.Text = CommonConstants.RPStatus_SaveApprovedResourcePlan.Replace("<<Project name>>", ViewState["ProjectName"].ToString());

                            lblMandatory.Visible = true;
                        }

                        //for rejection
                        if (Convert.ToInt32(ViewState[resorcePlanApprovalStatus].ToString()) == Convert.ToInt32(MasterEnum.ResourcePlanApprovalStatus.Rejected))
                        {
                            lblMandatory.Text = CommonConstants.RPStatus_SaveRejectedResourcePlan.Replace("<<Project Name>>", ViewState["ProjectName"].ToString());

                            lblMandatory.Visible = true;
                        }

                    }

                    string strApproveOrReject = ConvertToUpper(ViewState[ApproveOrReject].ToString());

                    //--Get the project manager email id 
                    RaveHRCollection objProjectDetailsByRPId = new RaveHRCollection();

                    string fileName = Server.MapPath(".") + @"\" + DecryptQueryString(QueryStringConstants.TXTFILELOCATION);

                    objProjectDetailsByRPId = objBLLApproveRejectRP.GetProjectByRPId(objBEApproveRejectRP, strApproveOrReject, fileName, raveHRCollection);

                    //Call Method to Update Employee ProjectAllocation
                    if (Convert.ToInt32(ViewState[resorcePlanApprovalStatus].ToString()) == Convert.ToInt32(MasterEnum.ResourcePlanApprovalStatus.Approved))
                    {
                        UpdateEmployeeProjectAllocation(objBEApproveRejectRP);
                    }
                }
                else // //RPIssue : 33126 Start - Added Else Part
                {
                    Response.Redirect(CommonConstants.APPROVEREJECTRP_PAGE, false);
                }
                //RPIssue : 33126 End
                //display project details for approve/reject resource plan grid
                GridApproveRejectRP();

            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "SaveCommentBtn_ClickEvtHandler", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// Cancel Commnets For Approve/Reject Resource Plan
    /// </summary>
    protected void btnCancelComment_ClickEvtHandler(object sender, EventArgs e)
    {
        try
        {
            //makes comment panel visibility as false.
            CommentPanel.Visible = false;

            //assign null value to comment text.
            txtComments.Text = " ";

            //BindGrid.
            GridApproveRejectRP();
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "CancelCommentBtn_ClickEvtHandler", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// Row DataBound EventHandler
    /// </summary>
    protected void grdPendingResourcePlan_DataBound(object sender, EventArgs e)
    {
        try
        {
            //create object for bottompager row.
            GridViewRow gvrPager = grdPendingResourcePlan.BottomPagerRow;

            //if grid pager row is null
            if (gvrPager == null) return;

            //Get Text Box and Lable from the Grid View
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            Label lblPageCount = (Label)gvrPager.Cells[0].FindControl("lblPageCount");

            //Assign current page index to text box
            txtPages.Text = ViewState[currentPageIndex].ToString();

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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "grdPendingResourcePlan_DataBound", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// On Rowcreated eventHandler
    /// </summary>
    protected void grdPendingResourcePlan_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (((Session["pagecount"] != null) && (int.Parse(Session["pagecount"].ToString()) > 1)) || ((objListResourcePlan.Count > 1)))
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "grdPendingResourcePlan_RowCreated", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// On Row sorting eventHandler.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdPendingResourcePlan_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            //--Pager txtbox
            GridViewRow gvrPager = grdPendingResourcePlan.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            txtPages.Text = ViewState[currentPageIndex].ToString();

            //On sorting assign new sort expression
            sortExpression = e.SortExpression;

            if (ViewState[SessionNames.PREVIOUS_SORT_EXPRESSION] == null)
            {
                ViewState[SessionNames.PREVIOUS_SORT_EXPRESSION] = sortExpression;
            }

            if (ViewState[SessionNames.PREVIOUS_SORT_EXPRESSION].ToString() == sortExpression)
            {
                //Sort to opposite direction based upon previous sort direction
                if (ViewState[SessionNames.SORT_DIRECTION] == null || GridViewSortDirection == SortDirection.Descending)
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
            ViewState[SessionNames.PREVIOUS_SORT_EXPRESSION] = sortExpression;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "grdPendingResourcePlan_Sorting", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// text changed page eventhandler.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtPages_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //--Add sort image to header
            AddSortImageOnEvent();

            TextBox txtPages = (TextBox)sender;

            //Check for Paging text box is not empty, non-zero, and out of range paging no.
            if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0 && Convert.ToInt32(txtPages.Text) <= Convert.ToInt32(Session["pagecount"].ToString()))
            {
                grdPendingResourcePlan.PageIndex = Convert.ToInt32(txtPages.Text) - 1;
                ViewState[currentPageIndex] = txtPages.Text;
            }
            else
            {
                txtPages.Text = ViewState[currentPageIndex].ToString();
                return;
            }

            //Bind the grid on paging
            GridApproveRejectRP();
            txtPages.Text = ViewState[currentPageIndex].ToString();
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "txtPages_TextChanged", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// next and previous button click eventhandler.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ChangePage(object sender, CommandEventArgs e)
    {
        try
        {
            GridViewRow gvrPager = grdPendingResourcePlan.BottomPagerRow;
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

            //--Bind Grid;
            GridApproveRejectRP();
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "ChangePage", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// eventhandler for pending resource plan.
    /// </summary>
    protected void grdPendingResourcePlan_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            HiddenField hdFProjectdId = null;
            GridView grdChildRPDurationDetail = null;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlTableRow tr_ChildRPDuration = (HtmlTableRow)e.Row.FindControl("tr_ResourcePlan");
                ImageButton imgbtnExpandCollaspeChildGrid = (ImageButton)e.Row.FindControl("imgbtnExpandCollaspeChildGrid");
                grdChildRPDurationDetail = (GridView)e.Row.FindControl("grdResourcePlan");

                hdFProjectdId = (HiddenField)(e.Row.FindControl(hiddenFieldAppRej));
                Label lblProjectManger = (Label)e.Row.FindControl("lblProjectManager");

                //create Label and assign resource plan project Name value to it.
                Label labelProjectName = (Label)e.Row.FindControl("lblchildProjectName");

                BusinessEntities.ResourcePlan objBEGetProjectManagerByProjectId = new BusinessEntities.ResourcePlan();
                objBEGetProjectManagerByProjectId.ProjectId = int.Parse(hdFProjectdId.Value);

                if (objBEGetProjectManagerByProjectId.ProjectId.ToString() == DecryptQueryString(QueryStringConstants.PROJECTID))
                {
                    ImageButton imgbtnExpandCollaspeChildGrid1 = (ImageButton)e.Row.FindControl(imagebuttonExpandCollaspeChildGrid);

                    //create gridview and assign child resource plan grid value to it.
                    GridView grdResourcePlan = (GridView)e.Row.FindControl(gridResourcePlan);

                    //create htmltablerow and assign child resource plan row value to it.
                    HtmlTableRow tr_ResourcePlan = (HtmlTableRow)e.Row.FindControl(childRowResourcePlan);

                    //create hiddenfield and assign child resource plan hiddenfield value to it.
                    HiddenField hdnFAppRej = (HiddenField)e.Row.Cells[0].FindControl(hiddenFieldAppRej);

                    //create Label and assign resource plan project Name value to it.
                    Label labelProjectName1 = (Label)e.Row.Cells[0].FindControl(lblProjectName);

                    //create label to get list of resource plan for particular project.
                    Label labelchildProjectName = (Label)e.Row.Cells[0].FindControl(lblchildProjectName);

                    //assign value to lblchildProjectName label to display project name with list of resource plan.
                    labelchildProjectName.Text = labelProjectName1.Text;

                    ViewState["ProjectName"] = labelProjectName1.Text;

                    //collapse resource plan grid.
                    //if image url is equal to "-" icon image.
                    if (imgbtnExpandCollaspeChildGrid1.ImageUrl == Common.CommonConstants.IMAGE_MINUSSIGNPATH)
                    {
                        //display none when user clicks "-" icon
                        tr_ResourcePlan.Style.Add(HtmlTextWriterStyle.Display, none);

                        //replace "-" icon with "+" when user clicks on collapse("-")image button.
                        imgbtnExpandCollaspeChildGrid1.ImageUrl = Common.CommonConstants.IMAGE_PLUSSIGNPATH;

                        //reset child grid expand status
                        ViewState[Row_ProjectId] = null;

                        if (CommentPanel.Visible)
                            CommentPanel.Visible = false;

                        return;
                    }


                    //--Collaspe the child grids
                    foreach (GridViewRow grvRow in grdPendingResourcePlan.Rows)
                    {
                        //create imagebutton and assign imagebutton value as "+" icon of gridviewrow.
                        ImageButton imgbtn = (ImageButton)grvRow.FindControl(imagebuttonExpandCollaspeChildGrid);

                        //create htmltablerow and assign child resource plan row value to it.
                        HtmlTableRow tr_RP = (HtmlTableRow)grvRow.FindControl(childRowResourcePlan);

                        //display none when user clicks "-" icon
                        tr_RP.Style.Add(HtmlTextWriterStyle.Display, none);

                        //replace "-" icon with "+" when user clicks on collapse("-")image button.
                        imgbtn.ImageUrl = Common.CommonConstants.IMAGE_PLUSSIGNPATH;

                    }

                    //--Bind Grid
                    BindChildGridResourcePlanDetail(int.Parse(DecryptQueryString(QueryStringConstants.PROJECTID)), grdResourcePlan);

                    //--Checks if Resource Plan is Pending For Approval/Rejection then only approve/reject image buttons are visible.
                    foreach (GridViewRow objResourcePlan in grdResourcePlan.Rows)
                    {
                        if (((Label)objResourcePlan.FindControl(LabelRPApprovalStatusId)).Text != Pending_For_Approval)
                        {
                            ((ImageButton)objResourcePlan.FindControl(ApproveImageButton)).Enabled = false;
                            ((ImageButton)objResourcePlan.FindControl(ApproveImageButton)).Visible = false;
                            ((ImageButton)objResourcePlan.FindControl(RejectImageButton)).Enabled = false;
                            ((ImageButton)objResourcePlan.FindControl(RejectImageButton)).Visible = false;
                        }
                        else
                        {
                            ((ImageButton)objResourcePlan.FindControl(ApproveImageButton)).Enabled = true;
                            ((ImageButton)objResourcePlan.FindControl(ApproveImageButton)).Visible = true;
                            ((ImageButton)objResourcePlan.FindControl(RejectImageButton)).Enabled = true;
                            ((ImageButton)objResourcePlan.FindControl(RejectImageButton)).Visible = true;
                        }
                    }

                    //expand resource plan child grid
                    if ((imgbtnExpandCollaspeChildGrid1 != null) && (tr_ResourcePlan != null))
                    {
                        //replace "+" icon with "-" when user clicks on collapse("+")image button.
                        imgbtnExpandCollaspeChildGrid1.ImageUrl = Common.CommonConstants.IMAGE_MINUSSIGNPATH;

                        //display block when user clicks "+" icon
                        tr_ResourcePlan.Style.Add(HtmlTextWriterStyle.Display, block);

                        //assign tooltip tip to image button.
                        imgbtnExpandCollaspeChildGrid1.ToolTip = collapse;

                        //--maintain child grid expand status
                        ViewState[Row_ProjectId] = imgbtnExpandCollaspeChildGrid1.CommandArgument.ToString();
                    }
                }

                RaveHRCollection objListProjectManagerByProjectId = new RaveHRCollection();

                objListProjectManagerByProjectId = GetProjectManagerByProjectId(objBEGetProjectManagerByProjectId);



                //checks if ProjectManager is allocated to this project or not
                if (objListProjectManagerByProjectId.Count > 0)
                {
                    BusinessEntities.Projects objBEResourcePlan = (BusinessEntities.Projects)objListProjectManagerByProjectId.Item(0);
                    lblProjectManger.Text = objBEResourcePlan.CreatedByFullName;
                }
                else
                {
                    lblProjectManger.Text = "-";
                }

                //--Check if any child grid expand and bind child grid
                if ((ViewState["Row_ProjectId"] != null) && (ViewState["Row_ProjectId"].ToString() == imgbtnExpandCollaspeChildGrid.CommandArgument.ToString()))
                {
                    imgbtnExpandCollaspeChildGrid.ImageUrl = Common.CommonConstants.IMAGE_MINUSSIGNPATH;
                    //Name: Sanju Kushwaha Removed display property so that it should display grid properly in IE10,Chrome and mozilla browser.
                    //  tr_ChildRPDuration.Style.Add(HtmlTextWriterStyle.Display, "");
                    tr_ChildRPDuration.Style.Add(HtmlTextWriterStyle.Display, "");
                    imgbtnExpandCollaspeChildGrid.ToolTip = "Collapse";

                    //--Bind Child Grid
                    BindChildGridResourcePlanDetail(int.Parse(imgbtnExpandCollaspeChildGrid.CommandArgument.ToString()), grdChildRPDurationDetail);

                    labelProjectName.Text = ViewState["ProjectName"].ToString();

                    //--Checks if Resource Plan is Pending For Approval/Rejection then only approve/reject image buttons are visible.
                    foreach (GridViewRow objResourcePlan in grdChildRPDurationDetail.Rows)
                    {
                        if (((Label)objResourcePlan.FindControl(LabelRPApprovalStatusId)).Text != Pending_For_Approval)
                        {
                            ((ImageButton)objResourcePlan.FindControl(ApproveImageButton)).Enabled = false;
                            ((ImageButton)objResourcePlan.FindControl(ApproveImageButton)).Visible = false;
                            ((ImageButton)objResourcePlan.FindControl(RejectImageButton)).Enabled = false;
                            ((ImageButton)objResourcePlan.FindControl(RejectImageButton)).Visible = false;
                        }
                        else
                        {
                            ((ImageButton)objResourcePlan.FindControl(ApproveImageButton)).Enabled = true;
                            ((ImageButton)objResourcePlan.FindControl(ApproveImageButton)).Visible = true;
                            ((ImageButton)objResourcePlan.FindControl(RejectImageButton)).Enabled = true;
                            ((ImageButton)objResourcePlan.FindControl(RejectImageButton)).Visible = true;
                        }
                    }
                }
                else
                {
                    imgbtnExpandCollaspeChildGrid.ImageUrl = Common.CommonConstants.IMAGE_PLUSSIGNPATH;
                    tr_ChildRPDuration.Style.Add(HtmlTextWriterStyle.Display, "none");
                    imgbtnExpandCollaspeChildGrid.ToolTip = "Expand";
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "grdPendingResourcePlan_RowDataBound", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    #endregion Protected Methods

    #region Private Member Functions

    /// <summary>
    /// Gets Projects For Approve/Reject Resource Plan.
    /// </summary>
    private void GridApproveRejectRP()
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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "GridApproveRejectRP", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);

        }

    }

    /// <summary>
    /// Private Property to Get and Set direction for for sorting
    /// </summary>
    private SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState[Common.SessionNames.SORT_DIRECTION] == null)
                ViewState[Common.SessionNames.SORT_DIRECTION] = SortDirection.Ascending;

            return (SortDirection)ViewState[Common.SessionNames.SORT_DIRECTION];
        }
        set
        {
            ViewState[Common.SessionNames.SORT_DIRECTION] = value;
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
            //--Get RP data
            objListResourcePlan = GetProjectListForResourcePlan(direction);

            //--Bind Grid
            grdPendingResourcePlan.DataSource = objListResourcePlan;
            grdPendingResourcePlan.DataBind();

            //--table row
            if (objListResourcePlan.Count == 0)
            {
                //grdvListofPendingApproveRejectHeadCount.Columns[0].Visible = false;
                ShowHeaderWhenEmptyGrid(objListResourcePlan);
                //tr_PendingResourcePlan.Visible = false;
            }

            //--Disable/enable sorting
            if ((int.Parse(Session["pagecount"].ToString()) == 1) && (objListResourcePlan.Count == 1))
            {
                grdPendingResourcePlan.AllowSorting = false;
            }
            else
            {
                grdPendingResourcePlan.AllowSorting = true;
            }

            //Finds the Paging Text Box, Previous and Next Link Buttons in Grid Views Paging Templet
            GridViewRow gvrPager = grdPendingResourcePlan.BottomPagerRow;
            if (gvrPager == null)
            {
                return;
            }

            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            LinkButton lbtnPrevious = (LinkButton)gvrPager.Cells[0].FindControl("lbtnPrevious");
            LinkButton lbtnNext = (LinkButton)gvrPager.Cells[0].FindControl("lbtnNext");

            if (pageCount > 1)
            {
                grdPendingResourcePlan.BottomPagerRow.Visible = true;
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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "SortGridView", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);

        }
    }

    /// <summary>
    /// Returns Resource Plan data for GridView
    /// </summary>
    /// <returns>List</returns>
    private RaveHRCollection GetProjectListForResourcePlan(string direction)
    {
        RaveHRCollection objListGetResourcePlan = null;
        try
        {
            //--Fill entiry
            //BusinessEntities.ResourcePlan objBEResourcePlan = new BusinessEntities.ResourcePlan();
            BusinessEntities.Projects objBEApproveRejectRP = new BusinessEntities.Projects();

            ////create object of businesslayer of resourceplan 
            Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLApproveRejectRP = new Rave.HR.BusinessLayer.Projects.ResourcePlan();

            objBEApproveRejectRP.PageSize = PAGE_SIZE;

            //--
            if (grdPendingResourcePlan.BottomPagerRow != null)
            {
                GridViewRow gvrPager = grdPendingResourcePlan.BottomPagerRow;
                TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
                if ((txtPages.Text.Trim() != "") && (int.Parse(txtPages.Text.Trim()) != 0) && (int.Parse(txtPages.Text.Trim()) <= int.Parse(Session["pagecount"].ToString())))
                {
                    objBEApproveRejectRP.PageNumber = int.Parse(txtPages.Text.Trim());
                }
            }
            else
            {
                objBEApproveRejectRP.PageNumber = 1;
            }

            //--Sort Expression
            objBEApproveRejectRP.SortExpression = sortExpression;
            objBEApproveRejectRP.SortDirection = direction;

            //--Get data
            Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLGetResourcePlan = new Rave.HR.BusinessLayer.Projects.ResourcePlan();
            objListGetResourcePlan = new RaveHRCollection();

            //method to get project details for approve/reject resource plan.
            objListGetResourcePlan = objBLLApproveRejectRP.GetProjectDetailsForApproveRejectRP(objBEApproveRejectRP, ref pageCount);

            //--Get pagecount in viewstate
            Session["pagecount"] = pageCount;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "GetProjectListForResourcePlan", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);

        }
        //--
        return objListGetResourcePlan;
    }

    /// <summary>
    /// Sort image method
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
                    case DbTableColumn.Con_ProjectCodeAbbrivation:
                        headerRow.Cells[1].Controls.Add(sortImage);
                        break;
                    case DbTableColumn.ProjectName:
                        headerRow.Cells[2].Controls.Add(sortImage);
                        break;
                    case DbTableColumn.Status:
                        headerRow.Cells[3].Controls.Add(sortImage);
                        break;
                    case "PM":
                        headerRow.Cells[4].Controls.Add(sortImage);
                        break;
                    case DbTableColumn.CreatedBy:
                        headerRow.Cells[5].Controls.Add(sortImage);
                        break;
                    case DbTableColumn.StartDate:
                        headerRow.Cells[6].Controls.Add(sortImage);
                        break;
                    case DbTableColumn.EndDate:
                        headerRow.Cells[7].Controls.Add(sortImage);
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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "AddSortImage", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Add sort image method on event 
    /// </summary>
    private void AddSortImageOnEvent()
    {
        try
        {
            //--Add sort image in header
            if (int.Parse(Session["pagecount"].ToString()) == 1)
            {
                AddSortImage(grdPendingResourcePlan.HeaderRow);
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "AddSortImageOnEvent", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Bind child grid displaying resource plan duration detail
    /// </summary>
    private void BindChildGridResourcePlanDetail(int projectId, GridView grdResourcePlan)
    {
        try
        {
            //create object of businessentities.resourceplan
            BusinessEntities.ResourcePlan objBEApproveRejectRP = new BusinessEntities.ResourcePlan();

            //assign projectid property value from the retrieved hiidenfield for projectid
            objBEApproveRejectRP.ProjectId = projectId;

            //assign RPApprovalStatusId property value for approved resourceplan.
            objBEApproveRejectRP.RPApprovalStatusId = Convert.ToInt32(MasterEnum.ResourcePlanApprovalStatus.Approved);

            //create businesslayer object of ResourcePlan
            Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLApproveRejectRP = new Rave.HR.BusinessLayer.Projects.ResourcePlan();

            //initialise collection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            //method to get projects for approve/reject resource plan and assign it to list object of BusinessEntities.ResourcePlan 
            raveHRCollection = objBLLApproveRejectRP.GetResourcePlanForApproveRejectRP(objBEApproveRejectRP);

            //assign datasource to grid.
            grdResourcePlan.DataSource = raveHRCollection;

            //bind grid.
            grdResourcePlan.DataBind();

        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "BindChildGridResourcePlanDetail", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// validate text content of comment during approval or rejection.
    /// </summary>
    /// <returns></returns>
    private bool ValidateApproveRejectControl()
    {

        //declare bool varaible.
        bool Flag = false;

        try
        {
            //when user has not entered proper value for comments
            if (!string.IsNullOrEmpty(txtComments.Text))
            {
                //assign Flag value as false
                Flag = true;
            }

        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "ValidateApproveRejectControl", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }

        //return bool value
        return Flag;
    }

    /// <summary>
    /// get project manager by project Id.
    /// </summary>
    /// <returns></returns>
    private RaveHRCollection GetProjectManagerByProjectId(BusinessEntities.ResourcePlan objBEGetProjectManagerByProjectId)
    {
        Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLGetProjectManagerByProjectId = new Rave.HR.BusinessLayer.Projects.ResourcePlan();
        return objBLLGetProjectManagerByProjectId.GetProjectManagerByProjectId(objBEGetProjectManagerByProjectId);
    }

    /// <summary>
    /// Authorize User
    /// </summary>
    public void AuthorizeUserForPageView()
    {
        Common.AuthorizationManager.AuthorizationManager objAuthorizationManager = new Common.AuthorizationManager.AuthorizationManager();
        objAuthorizationManager.AuthorizeUserForPageView(new object[] { AuthorizationManagerConstants.OPERATION_PAGE_APPROVEREJECTRP_VIEW });
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
            grdPendingResourcePlan.ShowHeader = true;
            grdPendingResourcePlan.AllowSorting = false;

            //Create empty datasource for Grid view and bind
            objListSort.Add(new BusinessEntities.Projects());
            grdPendingResourcePlan.DataSource = objListSort;
            grdPendingResourcePlan.DataBind();
            grdPendingResourcePlan.Columns[0].Visible = false;

            //Calculate number of columns in Grid view used for column Span
            int columnsCount = grdPendingResourcePlan.Columns.Count;

            //clear all the cells in the row
            grdPendingResourcePlan.Rows[0].Cells.Clear();

            //add a new blank cell
            grdPendingResourcePlan.Rows[0].Cells.Add(new TableCell());
            grdPendingResourcePlan.Rows[0].Cells[0].Text = NO_RECORDS_FOUND_MESSAGE;
            grdPendingResourcePlan.Rows[0].Cells[0].Wrap = false;
            grdPendingResourcePlan.Rows[0].Cells[0].Width = Unit.Percentage(10);
            grdPendingResourcePlan.Rows[0].Attributes.Remove(CommonConstants.EVENT_ONCLICK);
            grdPendingResourcePlan.Rows[0].Attributes.Remove(CommonConstants.EVENT_ONMOUSEOVER);
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, SHOW_HEADER_WHEN_EMPTY_GRID, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Method to Update Employee ProjectAllocation By Resource Plan
    /// </summary>
    private void UpdateEmployeeProjectAllocation(BusinessEntities.ResourcePlan objBEGetResourcePlanId)
    {
        Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLGetResourcePlanId = new Rave.HR.BusinessLayer.Projects.ResourcePlan();
        objBLLGetResourcePlanId.UpdateEmployeeProjectAllocation(objBEGetResourcePlanId);

    }

    #endregion Private Member Functions

    #region Public Method

    /// <summary>
    /// convert first letter to upper case.
    /// </summary> 
    public string ConvertToUpper(string InputString)
    {
        InputString = InputString.Substring(0, 1).ToUpper() + InputString.Substring(1, InputString.Length - 1);
        return InputString;
    }
    #endregion Public Method
}
