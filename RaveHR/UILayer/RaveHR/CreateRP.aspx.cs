//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           CreateRP.aspx.cs       
//  Author:         prashant.mala
//  Date written:   8/24/2009/ 10:58:30 AM
//  Description:    CreateRP page is used to create resource plan for the project.
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  8/24/2009/ 10:58:30 AM  prashant.mala    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Common;
using BusinessEntities;
using System.Net.Mail;
using System.Text;
using Common.Constants;
using System.Collections;
using System.Collections.Generic;
using Common.AuthorizationManager;

public partial class CreateRP : BaseClass
{
    #region Private Field Members

    /// <summary>
    /// Define Resource plan id
    /// </summary>
    private int ResourcePlanID = 0;

    /// <summary>
    /// Define Resource plan duration id
    /// </summary>
    private int ResourcePlanDurationID = 0;

    /// <summary>
    /// Define project id
    /// </summary>
    private int ProjectID = 0;

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
    /// Defines Generic List for Resource Plan Data
    /// </summary>
    RaveHRCollection objListResourcePlan = new RaveHRCollection();

    /// <summary>
    /// Error Message for dates
    /// </summary>
    private string dateErrorMsg = "Dates must match with project dates";

    /// <summary>
    /// Error Message for Add duration(onshore/offshore)
    /// </summary>
    private string strRPDurationErrorMsg = "Add details for offshore/onsite";

    /// <summary>
    /// define CLASS_NAME_RP
    /// </summary>
    private const string CLASS_NAME_RP = CommonConstants.CREATERP_PAGE;

    /// <summary>
    /// Rave Email domain
    /// </summary>
    /// 
    //Googleconfigurable
    //private const string RAVE_DOMAIN = "@rave-tech.com";

    /// <summary>
    /// Resource Location
    /// </summary>
    private const string Onsite_Offshore = "Onsite/Offshore";

    /// <summary>
    /// Define ResourceNo
    /// </summary>
    private const double ResourceNo = 0.0;

    /// <summary>
    /// Define RESOURCEBILLING
    /// </summary>
    private string RESOURCEBILLING = "Billing";

    /// <summary>
    /// Define totalBilling
    /// </summary>
    private int totalBilling = 0;

    /// <summary>
    /// Error Message for Billing
    /// </summary>
    private string billingErrorMsg = "Total billing cannot be greater than billability of a project";

    //Issue ID : 33940 Mahendra Strat
    public List<DateTime> lstEndDateRPDuration;
    //Issue ID : 33940 Mahendra END

    #endregion Private Field Members

    #region Protected Events

    /// <summary>
    /// page load eventhandler.
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
                ArrayList arrRolesForUser = new ArrayList();
                //AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
                AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
                //ArrayList arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());
                arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(RaveHRAuthorizationManager.getLoggedInUser());

                //only RMO team has access to create the RP
                if (!(arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM)))
                {
                    Response.Redirect(CommonConstants.PAGE_HOME, true);
                }
                //Siddharth 26th August 2015 End



                //--
                lblMessage.Style["color"] = "";
                lblMessage.Text = "<span >Fields marked with <span class='mandatorymark'>*</span> are mandatory.</span>";
                //--Get project id from url
                ProjectID = int.Parse(DecryptQueryString(QueryStringConstants.PROJECTID).ToString());

                if (!IsPostBack)
                {
                    //--Authorise User
                    AuthorizeUserForPageView();

                    //--Set current page index
                    ViewState["currentPageIndex"] = 1;

                    //--Default sort expression
                    if (ViewState[SessionNames.PREVIOUS_SORT_EXPRESSION] == null)
                    {
                        sortExpression = "RPDuRowNo";
                        ViewState[SessionNames.PREVIOUS_SORT_EXPRESSION] = sortExpression;
                    }
                    else
                    {
                        sortExpression = ViewState[SessionNames.PREVIOUS_SORT_EXPRESSION].ToString();
                    }

                    //--Get Project Details
                    GetProjectDetails();
                    //--Get Roles
                    GetRoles();
                    //--Get Location
                    GetLocation();
                    //--Get Project Location
                    GetProjectLocation();
                    //--Get Actual Location
                    GetActualLocation();

                    //--Check RPDuratonDetail still inactive
                    GetInactiveRPDurationDetail();
                    //--Get resource plan duration detail
                    GridRPDurationDetail();
                    //--Get resource plan 
                    GridResourcePlan();

                    //--Disable dates fields
                    //DisableDateFields();

                    //--Add JS to controls
                    AddJsToControls();
                }

                //--Add Sort Image
                if (IsPostBack)
                {
                    if (grdResourcePlan.Rows.Count > 1)
                    {
                        if ((Session[SessionNames.PageCount] != null) && (int.Parse(Session[SessionNames.PageCount].ToString()) == 1))
                        {
                            AddSortImage(grdResourcePlan.HeaderRow);
                        }
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "Page_Load", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }


    }

    /// <summary>
    /// add duration button click event handler
    /// </summary>
    protected void btnAddDuration_Click(object sender, EventArgs e)
    {
        try
        {
            
            if (ddlLocation.SelectedValue == Convert.ToInt32(MasterEnum.ResourceLocation.OnsiteOffshore).ToString())
            {
                if (grdRPDurationDetail.Rows.Count > 0)
                {
                    btnAddResourceDuration_Click(sender, e);
                    tr_SplitDurationOffshoreOnsite.Visible = false;
                    
                    td_StartDate.Visible = true;
                    ucDatePickerStartDate.Visible = true;
                    //imgCalResourceDurationStartDate.Visible = true;

                    td_EndDate.Visible = true;
                    ucDatePickerEndDate.Visible = true;
                    //imgCalResourceDurationEndDate.Visible = true;
                    ddlLocation.Enabled = true;
                }
                else
                {
                    lblMessage.Text = strRPDurationErrorMsg;
                    lblMessage.Style["color"] = "red";
                }

                return;
            }

            //--Date Validations with project dates
            if (!DateValWithProjectDates(ucDatePickerStartDate.Text, ucDatePickerEndDate.Text))
            {
                return;
            }


            ////Issue ID : 33940 Mahendra Strat
            if (Session["RPEndDateForSameRole"] != null && ddlRole.Enabled == false)
            {
                DateTime nextDate, maxDate;
                lstEndDateRPDuration = (List<DateTime>)Session["RPEndDateForSameRole"];

                maxDate = Convert.ToDateTime("01/01/1900");

                foreach (var item in lstEndDateRPDuration)
                {
                    if (item > maxDate)
                        maxDate = item;
                }

                nextDate = maxDate.AddDays(1);

                if (Convert.ToDateTime(ucDatePickerStartDate.Text) != nextDate)
                {
                    lblMessage.Text = "Please select the start date greater than one day of previous end date duration.";
                    lblMessage.Style["color"] = "red";
                    return;
                }
            }
            //Issue ID : 33940 Mahendra end

            //--Check RPDuratonDetail still inactive
            GetInactiveRPDurationDetail();

            //Issue ID : 33940 Mahendra Strat
            ViewState["FromAddRow"] = "FromAddRow";
            //Issue ID : 33940 Mahendra END

            //--Add resource plan 
            if (ResourcePlanID > 0)
            {
                AddRPDurationDetail();
            }
            else
            {
                AddResourcePlan();
            }

            //--Display duration grid
            GridRPDurationDetail();

            //--Display resource plan grid
            GridResourcePlan();

            //Issue ID : 33940 Mahendra Strat
            btnUpdateDuration.Visible = false;
            //Issue ID : 33940 Mahendra END
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "btnAddDuration_Click", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// rowcommand eventhandler for RPDurationDetail grid.
    /// </summary>
    protected void grdRPDurationDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RemoveRPDurationDetail")
            {
                BusinessEntities.ResourcePlan objBEResourcePlan = new BusinessEntities.ResourcePlan();
                objBEResourcePlan.RPDId = int.Parse(e.CommandArgument.ToString());

                Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLResourcePlan = new Rave.HR.BusinessLayer.Projects.ResourcePlan();
                objBLLResourcePlan.DeleteRPDetail(objBEResourcePlan);

                //--Check RPDuratonDetail still inactive
                GetInactiveRPDurationDetail();
                //--Display duration grid
                GridRPDurationDetail();
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "grdRPDurationDetail_RowCommand", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// AddResourceDuration buttoun click event handler.
    /// </summary>
    protected void btnAddResourceDuration_Click(object sender, EventArgs e)
    {
        try
        {
            //--Check RPDuratonDetail still inactive
            GetInactiveRPDurationDetail();

            //--Add Resource Duration
            BusinessEntities.ResourcePlan objBECreateResourcePlanDuration = new BusinessEntities.ResourcePlan();
            objBECreateResourcePlanDuration.ResourcePlanDurationId = ResourcePlanDurationID;
            objBECreateResourcePlanDuration.StartDate = GetMinDate(grdRPDurationDetail, 0);
            objBECreateResourcePlanDuration.EndDate = GetMaxDate(grdRPDurationDetail, 0);
            objBECreateResourcePlanDuration.NumberOfResources = int.Parse(txtNumberOfResources.Text.Trim()) - 1;

            Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLCreateResourcePlanDuration = new Rave.HR.BusinessLayer.Projects.ResourcePlan();

            //validates billing for RP does not exceed total billing of project
            if (ValidateBilling(int.Parse(txtNumberOfResources.Text), int.Parse(txtBilling.Text)))
            {

                objBLLCreateResourcePlanDuration.CreateRPDuration(objBECreateResourcePlanDuration);

                //--reset value
                ResourcePlanDurationID = 0;

                //--Display resource plan duration grid
                GridRPDurationDetail();

                //--Display resource plan grid
                GridResourcePlan();

                ddlRole.SelectedValue = "";
                ddlRole.Enabled = true;
                txtUtilization.Text = "";
                txtBilling.Text = "";
                ucDatePickerStartDate.Text = "";
                ucDatePickerEndDate.Text = "";
                ucDatePickerActualStartDate.Text = "";
                ucDatePickerActualEndDate.Text = "";
                ddlLocation.SelectedValue = "";
                ddlProjectLocation.SelectedValue = "";
                ddlActualLocation.SelectedValue = "";
                txtNumberOfResources.Text = "";

                /*--Changes due to onsite/offhoer part---*/
                tr_SplitDurationOffshoreOnsite.Visible = false;
            }
            else
            {
                //reset ddlLocation
                ddlLocation.SelectedIndex = -1;

                //assign error message for RP billing cannot be greater than project billing.
                lblMessage.Text = billingErrorMsg;
                lblMessage.Style["color"] = "red";
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "btnAddResourceDuration_Click", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// SaveResourcePlan button click event handler.
    /// </summary>
    protected void btnSaveResourcePlan_Click(object sender, EventArgs e)
    {
        try
        {
            //--Check RPDuratonDetail still inactive
            GetInactiveRPDurationDetail();

            //--Get logged in user
            Common.AuthorizationManager.AuthorizationManager objAuMan = new Common.AuthorizationManager.AuthorizationManager();
            
            string strCurrentUser = objAuMan.getLoggedInUserEmailId();

            //--Add Resource Duration
            BusinessEntities.ResourcePlan objBECreateResourcePlan = new BusinessEntities.ResourcePlan();

            objBECreateResourcePlan.RPStatusId = Convert.ToInt32(MasterEnum.ResourcePlanStatus.Active);
            objBECreateResourcePlan.RPId = ResourcePlanID;
            objBECreateResourcePlan.ResourcePlanCreated = true; //--true-RP created false-not created
            objBECreateResourcePlan.CreatedById = strCurrentUser;
            objBECreateResourcePlan.CreatedDate = DateTime.Now;
            objBECreateResourcePlan.LastModifiedById = strCurrentUser;
            objBECreateResourcePlan.LastModifiedDate = DateTime.Now;

            Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLCreateResourcePlan = new Rave.HR.BusinessLayer.Projects.ResourcePlan();

            //--Create Resource Plan
            objBLLCreateResourcePlan.CreateResourcePlan(objBECreateResourcePlan);

            //--Display Error Message
            string strSaveMessage = "<script>alert('" + CommonConstants.RPStatus_SaveCreatedResourcePlan + "');</script>";
            Response.Write(strSaveMessage);

            //--Bind Resource Plan
            GridResourcePlan();

        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "btnSaveResourcePlan_Click", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// rowcommand eventhandler for ResourcePlan grid
    /// </summary>
    protected void grdResourcePlan_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //--Delete RP Duration
            if (e.CommandName == "DeleteRPDuration")
            {
                //--Remove RPDuration
                RemoveRPDuration(int.Parse(e.CommandArgument.ToString()));

                //--Check RPDuratonDetail still inactive
                GetInactiveRPDurationDetail();
                //--Get resource plan duration detail
                GridRPDurationDetail();
                //--Get resource plan 
                GridResourcePlan();

                //--Get Location
                GetLocation();

                //--enable buttons
                btnUpdateDuration.Visible = false;
                btnAddDuration.Visible = true;

                //--Enable the fields
                ddlRole.SelectedValue = "";
                ddlRole.Enabled = true;
                ddlLocation.Enabled = true;
                ddlProjectLocation.Enabled = true;

                //--Enable the other fields
                txtUtilization.Text = "";
                txtBilling.Text = "";
                txtNumberOfResources.Text = "";
                ucDatePickerStartDate.Text = "";
                ucDatePickerEndDate.Text = "";
                ddlLocation.SelectedValue = "";
                ddlProjectLocation.SelectedValue = "";

                //--Enable/Disable fields
                lblNumberOfResources.Visible = true;
                txtNumberOfResources.Visible = true;
                txtUtilization.Enabled = true;
                txtBilling.Enabled = true;
                ucDatePickerStartDate.IsEnable = true;
                ucDatePickerEndDate.IsEnable = true;
                //imgCalResourceDurationStartDate.Enabled = true;
                //imgCalResourceDurationEndDate.Enabled = true;
            }

            //--Expand Collapse Child Grid
            if (e.CommandName == "ChildGridRPDuraiton")
            {
                //--Get controls
                GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                ImageButton imgbtnExpandCollaspeChildGrid = (ImageButton)grv.FindControl("imgbtnExpandCollaspeChildGrid");
                GridView grdChildRPDurationDetail = (GridView)grv.FindControl("grdChildRPDurationDetail");
                HtmlTableRow tr_ChildRPDuration = (HtmlTableRow)grv.FindControl("tr_ChildRPDuration");

                //--Collaspe
                if (imgbtnExpandCollaspeChildGrid.ImageUrl == Common.CommonConstants.IMAGE_MINUSSIGNPATH)
                {
                    tr_ChildRPDuration.Style.Add(HtmlTextWriterStyle.Display, "none");
                    imgbtnExpandCollaspeChildGrid.ImageUrl = Common.CommonConstants.IMAGE_PLUSSIGNPATH;
                    //reset child grid expand status
                    ViewState["Row_RPDurationId"] = null;
                    return;
                }

                //--Collaspe the child grids
                foreach (GridViewRow grvRow in grdResourcePlan.Rows)
                {
                    ImageButton imgbtnExpandCollaspe = (ImageButton)grvRow.FindControl("imgbtnExpandCollaspeChildGrid");
                    HtmlTableRow tr_ChildGrid = (HtmlTableRow)grvRow.FindControl("tr_ChildRPDuration");

                    tr_ChildGrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                    imgbtnExpandCollaspe.ImageUrl = Common.CommonConstants.IMAGE_PLUSSIGNPATH;

                }

                //--Bind Grid
                BindChildGridRPDurationDetail(int.Parse(e.CommandArgument.ToString()), grdChildRPDurationDetail);

                //--Expand child grid
                if ((imgbtnExpandCollaspeChildGrid != null) && (tr_ChildRPDuration != null))
                {
                    imgbtnExpandCollaspeChildGrid.ImageUrl = Common.CommonConstants.IMAGE_MINUSSIGNPATH;
                //Sanju:Issue Id 50201: RemovedControl display block property
                    tr_ChildRPDuration.Style.Add(HtmlTextWriterStyle.Display, "");
                    //Sanju:Issue Id 50201:end
                    imgbtnExpandCollaspeChildGrid.ToolTip = "Collapse";

                    //--maintain child grid expand status
                    ViewState["Row_RPDurationId"] = imgbtnExpandCollaspeChildGrid.CommandArgument.ToString();
                }
            }

            //--Edit Resource Plan Duration 
            if (e.CommandName == "EditRPDuration")
            {
                //--Get data and populate the fields
                BusinessEntities.ResourcePlan objBEResourcePlan = new BusinessEntities.ResourcePlan();
                objBEResourcePlan.ResourcePlanDurationId = int.Parse(e.CommandArgument.ToString());

                Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLResourcePlan = new Rave.HR.BusinessLayer.Projects.ResourcePlan();
                BusinessEntities.ResourcePlan objBEResourcePlanById = new BusinessEntities.ResourcePlan();
                // Rajan Kumar : Issue 46252: 12/02/2014 : Starts                        			 
                // Desc : In MRF history need to implemented in all cases in RMS.
                //Pass Email to know who is going to modified the data            
                AuthorizationManager authoriseduser = new AuthorizationManager();
                objBEResourcePlan.ApproverId = authoriseduser.getLoggedInUserEmailId();
                // Rajan Kumar : Issue 46252: 12/02/2014 : END

                objBEResourcePlanById = objBLLResourcePlan.RPDurationByID(objBEResourcePlan, "SELECT");

                //--populate fields
                ddlRole.SelectedValue = objBEResourcePlanById.Role;
                ddlRole.Enabled = true;
                btnUpdateDuration.Visible = true;
                btnUpdateDuration.CommandArgument = objBEResourcePlanById.ResourcePlanDurationId.ToString();
                btnUpdateDuration.CommandName = "UpdateRPDuration";
                btnAddDuration.Visible = false;

                //--Disable other fields
                ucDatePickerStartDate.IsEnable = false;
                ucDatePickerEndDate.IsEnable = false;
                //imgCalResourceDurationStartDate.Enabled = false;
                ucDatePickerStartDate.Text = "";
                //imgCalResourceDurationEndDate.Enabled = false;
                ucDatePickerEndDate.Text = "";
                txtUtilization.Enabled = false;
                txtUtilization.Text = "";
                txtBilling.Enabled = false;
                txtBilling.Text = "";
                ddlLocation.Enabled = false;
                ddlLocation.SelectedValue = "";
                ddlProjectLocation.Enabled = false;
                ddlProjectLocation.SelectedValue = "";
                txtNumberOfResources.Visible = false;
                lblNumberOfResources.Visible = false;

                //--Add Duration button JS
                string txtBoxControlIds = "";
                //string txtBoxControlIds = txtResourceStartDate.ClientID + "," + txtResourceEndDate.ClientID;

                string ddlControlIds = ddlRole.ClientID;
                string ddlSpanControlIds = "spanRole";

                btnUpdateDuration.Attributes.Add("onClick", "return Validate('" + txtBoxControlIds + "', '" + ddlControlIds + "', '" + ddlSpanControlIds + "');");
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "grdResourcePlan_RowCommand", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// rowcommand eventhandler for ChildRPDurationDetail grid
    /// </summary>
    protected void grdChildRPDurationDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //--Add sort image in header
            AddSortImageOnEvent();

            if (e.CommandName == "ChildDeleteRPDetail")
            {
                GridViewRow grvRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                GridView grdChildRPDurationDetail = (GridView)grvRow.NamingContainer;
                GridViewRow grvParent = (GridViewRow)grdChildRPDurationDetail.NamingContainer;
                GridView grdResourcePlan = (GridView)grvParent.NamingContainer;
                ImageButton imgDeleteRPDuration = (ImageButton)grvParent.FindControl("imgDeleteRPDuration");

                //--Fill entity and delete record
                BusinessEntities.ResourcePlan objBEResourcePlan = new BusinessEntities.ResourcePlan();
                objBEResourcePlan.RPDId = int.Parse(e.CommandArgument.ToString());

                Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLResourcePlan = new Rave.HR.BusinessLayer.Projects.ResourcePlan();
                objBLLResourcePlan.DeleteRPDetail(objBEResourcePlan);

                //--Bind RP grid
                GridResourcePlan();

                //--Get Location
                GetLocation();

                //--enable buttons
                btnUpdateDuration.Visible = false;
                btnAddDuration.Visible = true;

                //--Enable the fields
                ddlRole.SelectedValue = "";
                ddlRole.Enabled = true;
                ddlLocation.Enabled = true;
                ddlProjectLocation.Enabled = true;

                //--Enable the other fields
                txtUtilization.Text = "";
                txtBilling.Text = "";
                txtNumberOfResources.Text = "";
                ucDatePickerStartDate.Text = "";
                ucDatePickerEndDate.Text = "";
                ddlLocation.SelectedValue = "";
                ddlProjectLocation.SelectedValue = "";

                //--Enable/Disable fields
                lblNumberOfResources.Visible = true;
                txtNumberOfResources.Visible = true;
                txtUtilization.Enabled = true;
                txtBilling.Enabled = true;
                ucDatePickerStartDate.IsEnable = true;
                ucDatePickerEndDate.IsEnable = true;
                //imgCalResourceDurationStartDate.Enabled = true;
                //imgCalResourceDurationEndDate.Enabled = true;
            }

            //--Edit Resource Plan detail
            if (e.CommandName == "EditChildRPDetail")
            {
                //--Get data and populate the fields
                BusinessEntities.ResourcePlan objBEResourcePlan = new BusinessEntities.ResourcePlan();
                objBEResourcePlan.RPDId = int.Parse(e.CommandArgument.ToString());

                Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLResourcePlan = new Rave.HR.BusinessLayer.Projects.ResourcePlan();
                BusinessEntities.ResourcePlan objBEResourcePlanById = new BusinessEntities.ResourcePlan();
                objBEResourcePlanById = objBLLResourcePlan.RPDetailByID(objBEResourcePlan, "SELECT");

                //--populate fields
                txtUtilization.Text = objBEResourcePlanById.Utilization.ToString();
                txtUtilization.Enabled = true;
                txtBilling.Text = objBEResourcePlanById.Billing.ToString();
                txtBilling.Enabled = true;
                ddlLocation.SelectedValue = objBEResourcePlanById.ResourceLocation;
                ddlLocation.Enabled = true;
                ucDatePickerStartDate.Text = objBEResourcePlanById.ResourceStartDate.ToString("dd/MM/yyyy");
                ucDatePickerStartDate.IsEnable = true;
                //CalResourceDurationStartDate.Enabled = true;
                ucDatePickerEndDate.Text = objBEResourcePlanById.ResourceEndDate.ToString("dd/MM/yyyy");
                ucDatePickerEndDate.IsEnable = true;
                //CalResourceDurationEndDate.Enabled = true;
                ddlProjectLocation.SelectedValue = objBEResourcePlanById.ProjectLocation;
                ddlProjectLocation.Enabled = true;

                //--Disable other fields
                ddlRole.SelectedValue = objBEResourcePlanById.Role;
                ddlRole.Enabled = false;

                //--Enable/Disable buttons
                btnUpdateDuration.Visible = true;
                btnUpdateDuration.CommandArgument = objBEResourcePlan.RPDId.ToString();
                btnUpdateDuration.CommandName = "UpdateRPDetail";
                //Issue ID : 33940 Mahendra Strat
                btnAddDuration.Visible = true;
                //Issue ID : 33940 Mahendra END
                lblNumberOfResources.Visible = false;
                txtNumberOfResources.Visible = false;

                //--Add JS to update button
                string txtBoxControlIds = txtUtilization.ClientID + "," + txtBilling.ClientID + "," + ucDatePickerStartDate.ClientID + "," + ucDatePickerEndDate.ClientID;
                string ddlControlIds = ddlLocation.ClientID + "," + ddlProjectLocation.ClientID;
                string ddlSpanControlIds = "spanLocation,spanProjectLocation";
                string errorImgIDs = imgUtilization.ClientID + "," + imgBilling.ClientID;

                //--Remove Onsite_Offshore item
                ddlLocation.Items.Remove(new ListItem(Onsite_Offshore, Convert.ToInt32(MasterEnum.ResourceLocation.OnsiteOffshore).ToString()));

                //validate billing by ProjectId
                if (ViewState[RESOURCEBILLING] != null)
                    ViewState[RESOURCEBILLING] = Convert.ToDecimal(ViewState[RESOURCEBILLING].ToString()) - Convert.ToDecimal(txtBilling.Text);
                ViewState["editClicked"] = true;
                //Issue ID : 33940 Mahendra Strat
                ViewState["RPduId"] = objBEResourcePlanById.ResourcePlanDurationId.ToString();
                //Issue ID : 33940 Mahendra ENd

                btnUpdateDuration.Attributes.Add("onClick", "return Validate('" + txtBoxControlIds + "', '" + ddlControlIds + "', '" + ddlSpanControlIds + "', '" + errorImgIDs + "');");
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "grdChildRPDurationDetail_RowCommand", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// rowdatabound eventhandler for ResourcePlan grid.
    /// </summary>
    protected void grdResourcePlan_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlTableRow tr_ChildRPDuration = (HtmlTableRow)e.Row.FindControl("tr_ChildRPDuration");
                ImageButton imgbtnExpandCollaspeChildGrid = (ImageButton)e.Row.FindControl("imgbtnExpandCollaspeChildGrid");
                GridView grdChildRPDurationDetail = (GridView)e.Row.FindControl("grdChildRPDurationDetail");

                //--Check if any child grid expand and bind child grid
                if ((ViewState["Row_RPDurationId"] != null) && (ViewState["Row_RPDurationId"].ToString() == imgbtnExpandCollaspeChildGrid.CommandArgument.ToString()))
                {
                    imgbtnExpandCollaspeChildGrid.ImageUrl = Common.CommonConstants.IMAGE_MINUSSIGNPATH;
                    //Sanju:Issue Id 50201 : Removed display block property
                    tr_ChildRPDuration.Style.Add(HtmlTextWriterStyle.Display, "");
                    imgbtnExpandCollaspeChildGrid.ToolTip = "Collapse";

                    //--Bind Child Grid
                    BindChildGridRPDurationDetail(int.Parse(imgbtnExpandCollaspeChildGrid.CommandArgument.ToString()), grdChildRPDurationDetail);
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "grdResourcePlan_RowDataBound", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// UpdateDuration button click eventhandler.
    /// </summary>
    protected void btnUpdateDuration_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnUpdateDuration.CommandName == "UpdateRPDuration")
            {
                BusinessEntities.ResourcePlan objBEResourcePlan = new BusinessEntities.ResourcePlan();
                objBEResourcePlan.ResourcePlanDurationId = int.Parse(btnUpdateDuration.CommandArgument.ToString());
                objBEResourcePlan.StartDate = DateTime.Now; //--default since txtResourceStartDate is removed
                objBEResourcePlan.EndDate = DateTime.Now;//--default since txtResourceEndDate is removed
                objBEResourcePlan.Role = ddlRole.SelectedValue;

                Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLResourcePlan = new Rave.HR.BusinessLayer.Projects.ResourcePlan();

                // Rajan Kumar : Issue 46252: 12/02/2014 : Starts                        			 
                // Desc : In MRF history need to implemented in all cases in RMS.
                //Pass Email to know who is going to modified the data            
                AuthorizationManager authoriseduser = new AuthorizationManager();
                objBEResourcePlan.ApproverId = authoriseduser.getLoggedInUserEmailId();
                // Rajan Kumar : Issue 46252: 12/02/2014 : END

                //--Update record
                objBLLResourcePlan.UpdateRPDurationByID(objBEResourcePlan, "UPDATE");

                //--Get Location
                GetLocation();

                //--enable buttons
                btnUpdateDuration.Visible = false;
                btnAddDuration.Visible = true;

                //--Enable the fields
                ddlRole.SelectedValue = "";
                ddlRole.Enabled = true;

                //--Enable the other fields
                txtUtilization.Enabled = true;
                txtBilling.Enabled = true;
                ucDatePickerStartDate.IsEnable = true;
                ucDatePickerEndDate.IsEnable = true;
                //imgCalResourceDurationStartDate.Enabled = true;
                //imgCalResourceDurationEndDate.Enabled = true;
                ddlLocation.Enabled = true;
                ddlProjectLocation.Enabled = true;

                //--Enable/Disable fields
                lblNumberOfResources.Visible = true;
                txtNumberOfResources.Visible = true;

                //--Bind Resource Plan Grid
                GridResourcePlan();

            }

            if (btnUpdateDuration.CommandName == "UpdateRPDetail")
            {
                BusinessEntities.ResourcePlan objBEResourcePlan = new BusinessEntities.ResourcePlan();
                objBEResourcePlan.RPDId = int.Parse(btnUpdateDuration.CommandArgument.ToString());
                objBEResourcePlan.Utilization = int.Parse(txtUtilization.Text);
                objBEResourcePlan.Billing = int.Parse(txtBilling.Text);
                objBEResourcePlan.ResourceLocation = ddlLocation.SelectedValue;
                objBEResourcePlan.ResourceStartDate = DateTime.Parse(ucDatePickerStartDate.Text);
                objBEResourcePlan.ResourceEndDate = DateTime.Parse(ucDatePickerEndDate.Text);
                objBEResourcePlan.ProjectLocation = ddlProjectLocation.SelectedValue;

                Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLResourcePlan = new Rave.HR.BusinessLayer.Projects.ResourcePlan();

                //validates billing for RP does not exceed total billing of project
                if (ValidateBilling(1, Convert.ToInt32(objBEResourcePlan.Billing)))
                {
                    //--Update record
                    objBLLResourcePlan.UpdateRPDetailByID(objBEResourcePlan, "UPDATE");

                    //--Get Location
                    GetLocation();

                    //--enable buttons
                    btnUpdateDuration.Visible = false;
                    btnAddDuration.Visible = true;

                    //--Enable the fields
                    ddlRole.SelectedValue = "";
                    ddlRole.Enabled = true;

                    //--Enable the other fields
                    txtUtilization.Text = "";
                    txtBilling.Text = "";
                    ucDatePickerStartDate.Text = "";
                    ucDatePickerEndDate.Text = "";
                    ddlLocation.SelectedValue = "";
                    ddlProjectLocation.SelectedValue = "";

                    //--Enable/Disable fields
                    lblNumberOfResources.Visible = true;
                    txtNumberOfResources.Visible = true;

                    //--Bind Resource Plan Grid
                    GridResourcePlan();
                }
                else
                {
                    lblMessage.Text = billingErrorMsg;
                    lblMessage.Style["color"] = "red";
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "btnUpdateDuration_Click", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// CancelRPDetails button click eventhandler.
    /// </summary>
    protected void btnCancelRPDetails_Click(object sender, EventArgs e)
    {
        try
        {
            //--Check RPDuratonDetail still inactive
            GetInactiveRPDurationDetail();

            //assign value when RP edition is canceled
            if (ViewState["editClicked"] != null && Convert.ToBoolean(ViewState["editClicked"]) == true)
                ViewState[RESOURCEBILLING] = Convert.ToInt32(ViewState[RESOURCEBILLING]) + Convert.ToInt32(txtBilling.Text);

            //--Remove RPDuration
            RemoveRPDuration(ResourcePlanDurationID);

            //--Bind RP Duration Details grid
            GridRPDurationDetail();

            //--Get Location
            GetLocation();

            /*-- For Onsite/Offshore details --*/
            txtNumberOfResources.Text = "";
            ucDatePickerStartDate.Visible = true;
            ucDatePickerStartDate.Text = "";
            ucDatePickerEndDate.Text = "";
            ucDatePickerEndDate.Visible = true;

            txtUtilization.Text = "";
            txtUtilization.Enabled = true;
            txtBilling.Text = "";
            txtBilling.Enabled = true;
            ucDatePickerActualStartDate.Text = "";
            ucDatePickerActualEndDate.Text = "";
            td_StartDate.Visible = true;
            td_EndDate.Visible = true;
            ucDatePickerStartDate.IsEnable = true;
            ucDatePickerEndDate.IsEnable = true;
            ucDatePickerStartDate.Visible = true;
            ucDatePickerEndDate.Visible = true;
            //imgCalResourceDurationStartDate.Visible = true;
            //imgCalResourceDurationStartDate.Enabled = true;
            //imgCalResourceDurationEndDate.Visible = true;
            //imgCalResourceDurationEndDate.Enabled = true;

            ddlRole.SelectedValue = "";
            ddlActualLocation.SelectedValue = "";
            ddlLocation.SelectedValue = "";
            ddlLocation.Enabled = true;
            ddlProjectLocation.SelectedValue = "";
            ddlProjectLocation.Enabled = true;
            tr_SplitDurationOffshoreOnsite.Visible = false;

            //--Enable/Disable buttons
            btnAddDuration.Visible = true;
            btnUpdateDuration.Visible = false;

            //--Enable/Disable fields
            lblNumberOfResources.Visible = true;
            txtNumberOfResources.Visible = true;

            //Issue ID : 33940 Mahendra Strat
            if (Session["RPEndDateForSameRole"] != null)
                Session["RPEndDateForSameRole"] = null;

            //Issue ID : 33940 Mahendra End
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "btnCancelRPDetails_Click", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// CancelResourcePlan button click eventhandler.
    /// </summary>
    protected void btnCancelResourcePlan_Click(object sender, EventArgs e)
    {
        try
        {
            //--
            GetInactiveRPDurationDetail();

            BusinessEntities.ResourcePlan objBEResourcePlan = new BusinessEntities.ResourcePlan();
            objBEResourcePlan.RPId = ResourcePlanID;

            Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLResourcePlan = new Rave.HR.BusinessLayer.Projects.ResourcePlan();
            objBLLResourcePlan.DeleteResourcePlan(objBEResourcePlan);

            //--Redirect to project summary page
            Response.Redirect(CommonConstants.PROJECTSUMMARY_PAGE, false);
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "btnCancelResourcePlan_Click", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// txtPages textbox text change event handler
    /// </summary>
    protected void txtPages_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //--Add sort image to header
            AddSortImageOnEvent();

            TextBox txtPages = (TextBox)sender;

            //Check for Paging text box is not empty, non-zero, and out of range paging no.
            if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0 && Convert.ToInt32(txtPages.Text) <= Convert.ToInt32(Session[SessionNames.PageCount].ToString()))
            {
                grdResourcePlan.PageIndex = Convert.ToInt32(txtPages.Text) - 1;
                ViewState["currentPageIndex"] = txtPages.Text;
            }
            else
            {
                txtPages.Text = ViewState["currentPageIndex"].ToString();
                return;
            }

            //Bind the grid on paging
            GridResourcePlan();
            txtPages.Text = ViewState["currentPageIndex"].ToString();
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
    /// Page change eventhandler
    /// </summary>
    protected void ChangePage(object sender, CommandEventArgs e)
    {
        try
        {
            GridViewRow gvrPager = grdResourcePlan.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");

            switch (e.CommandName)
            {
                case "Previous":
                    ViewState["currentPageIndex"] = Convert.ToInt32(txtPages.Text) - 1;
                    txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) - 1);
                    break;

                case "Next":
                    ViewState["currentPageIndex"] = Convert.ToInt32(txtPages.Text) + 1;
                    txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) + 1);
                    break;
            }

            //--Bind Grid;
            GridResourcePlan();
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
    /// ResourcePlan grid databound event handler.
    /// </summary>
    protected void grdResourcePlan_DataBound(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrPager = grdResourcePlan.BottomPagerRow;

            if (gvrPager == null) return;

            //Get Text Box and Lable from the Grid View
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            Label lblPageCount = (Label)gvrPager.Cells[0].FindControl("lblPageCount");

            //Assign current page index to text box
            txtPages.Text = ViewState["currentPageIndex"].ToString();

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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "grdResourcePlan_DataBound", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// ResourcePlan rowcreated event handler.
    /// </summary>
    protected void grdResourcePlan_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (((Session[SessionNames.PageCount] != null) && (int.Parse(Session[SessionNames.PageCount].ToString()) > 1)) || ((objListResourcePlan.Count > 1)))
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "grdResourcePlan_RowCreated", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// ResourcePlan grid onsorting eventhandler
    /// </summary>
    protected void grdResourcePlan_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            //--Pager txtbox
            GridViewRow gvrPager = grdResourcePlan.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            txtPages.Text = ViewState["currentPageIndex"].ToString();

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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "grdResourcePlan_Sorting", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// location ddl change
    /// </summary>
    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlLocation.SelectedValue == Convert.ToInt32(MasterEnum.ResourceLocation.OnsiteOffshore).ToString())
            {
                //--
                td_StartDate.Visible = false;
                ucDatePickerStartDate.Visible = false;
                //ucDatePickerStartDate.Text = DateTime.Now.ToString();    //--Set Default value;
                //imgCalResourceDurationStartDate.Visible = false;

                //--disable fields
                td_EndDate.Visible = false;
                ucDatePickerEndDate.Visible = false;
                //ucDatePickerEndDate.Text = DateTime.Now.ToString(); //-- Set Default value;
                //imgCalResourceDurationEndDate.Visible = false;

                //--
                tr_SplitDurationOffshoreOnsite.Visible = true;

                //--Add Duration button JS
                string txtBoxControlIds = txtNumberOfResources.ClientID + "," + txtUtilization.ClientID + "," + txtBilling.ClientID;
                string ddlControlIds = ddlRole.ClientID + "," + ddlLocation.ClientID + "," + ddlProjectLocation.ClientID;
                string ddlSpanControlIds = "spanRole,spanLocation,spanProjectLocation";
                string errorImgIDs = imgErrorNoOfResources.ClientID + "," + imgUtilization.ClientID + "," + imgBilling.ClientID;

                btnAddDuration.Attributes.Add("onClick", "return Validate('" + txtBoxControlIds + "', '" + ddlControlIds + "', '" + ddlSpanControlIds + "', '" + errorImgIDs + "');");
            }
            else
            {
                //--
                td_StartDate.Visible = true;
                ucDatePickerStartDate.Visible = true;
                //txtResourceDurationStartDate.Text = "";
                //imgCalResourceDurationStartDate.Visible = true;

                //--
                td_EndDate.Visible = true;
                ucDatePickerEndDate.Visible = true;
                //ucDatePickerEndDate.Text = "";
                //imgCalResourceDurationEndDate.Visible = true;

                //--
                tr_SplitDurationOffshoreOnsite.Visible = false;

                //--Add Duration button JS
                string txtBoxControlIds = txtNumberOfResources.ClientID + "," + txtUtilization.ClientID + "," + txtBilling.ClientID + "," + ucDatePickerStartDate.ClientID + "," + ucDatePickerEndDate.ClientID;
                string ddlControlIds = ddlRole.ClientID + "," + ddlLocation.ClientID + "," + ddlProjectLocation.ClientID;
                string ddlSpanControlIds = "spanRole,spanLocation,spanProjectLocation";
                string errorImgIDs = imgErrorNoOfResources.ClientID + "," + imgUtilization.ClientID + "," + imgBilling.ClientID;

                btnAddDuration.Attributes.Add("onClick", "return Validate('" + txtBoxControlIds + "', '" + ddlControlIds + "', '" + ddlSpanControlIds + "', '" + errorImgIDs + "');");
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "btnCancelRPDetails_Click", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Add Row for onsite/offshore
    /// </summary>
    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateRPDuration())
            {
                //--Date Validations with project dates
                if (!DateValWithProjectDates(ucDatePickerActualStartDate.Text, ucDatePickerActualEndDate.Text))
                {
                    return;
                }

                //--Check RPDuratonDetail still inactive
                GetInactiveRPDurationDetail();

                //--Add resource plan 
                if (ResourcePlanID > 0)
                {
                    AddRPDurationDetail();
                }
                else
                {
                    AddResourcePlan();
                }

                //--Display duration grid
                GridRPDurationDetail();

                //--Diable fields
                ddlLocation.Enabled = false;
            }
            else
            {
                lblMessage.Text = CommonConstants.RPErrorMsg_AddResourcePlanDuration;
                lblMessage.Style.Add(HtmlTextWriterStyle.Color, "Red");
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "btnCancelRPDetails_Click", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    #endregion Protected Events

    #region Private Member Functions

    /// <summary>
    /// Get roles from master
    /// </summary>
    private void GetRoles()
    {
        try
        {
            BindDropDown(ddlRole, Convert.ToInt32(EnumsConstants.Category.ProjectRole));
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "GetRoles", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);

        }
    }

    /// <summary>
    /// Get location from master
    /// </summary>
    private void GetLocation()
    {
        try
        {
            BindDropDown(ddlLocation, Convert.ToInt32(EnumsConstants.Category.ResourceLocation));
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "GetLocation", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Get actual location from master
    /// </summary>
    private void GetActualLocation()
    {
        try
        {
            BindDropDown(ddlActualLocation, Convert.ToInt32(EnumsConstants.Category.ResourceLocation));

            //--Remove Onsite_Offshore item
            ddlActualLocation.Items.Remove(new ListItem(Onsite_Offshore, Convert.ToInt32(MasterEnum.ResourceLocation.OnsiteOffshore).ToString()));
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "GetLocation", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Get projectlocation from master
    /// </summary>
    private void GetProjectLocation()
    {
        try
        {
            BindDropDown(ddlProjectLocation, Convert.ToInt32(EnumsConstants.Category.ProjectLocation));
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "GetProjectLocation", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Get master data
    /// </summary>
    private BusinessEntities.RaveHRCollection GetMaster(int CategoryId)
    {

        BusinessEntities.RaveHRCollection objListMaster = new BusinessEntities.RaveHRCollection();
        try
        {
            Rave.HR.BusinessLayer.Common.Master objRaveMaster = new Rave.HR.BusinessLayer.Common.Master();
            objListMaster = objRaveMaster.FillDropDownsBL(CategoryId);

        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "GetMaster", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
        return objListMaster;
    }

    /// <summary>
    /// Generic method for dropdown bind
    /// </summary>
    private void BindDropDown(DropDownList ddl, int CategoryId)
    {
        try
        {
            ddl.Items.Clear();
            ddl.DataSource = GetMaster(CategoryId);
            ddl.DataTextField = "Val";
            ddl.DataValueField = "KeyName";
            ddl.DataBind();

            if (ddl.Items.Count > 0)
            {
                ddl.Items.Insert(0, new ListItem("Select", ""));
            }
            else
            {
                ddl.Items.Add(new ListItem("Select", ""));
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "BindDropDown", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Add resource plan
    /// </summary>
    private void AddResourcePlan()
    {
        try
        {
            BusinessEntities.ResourcePlan objBEAddResourcePlan = new BusinessEntities.ResourcePlan();
            objBEAddResourcePlan.ProjectId = ProjectID;
            objBEAddResourcePlan.Role = ddlRole.SelectedValue;
            objBEAddResourcePlan.RPStatusId = Convert.ToInt32(MasterEnum.ResourcePlanStatus.Active);
            objBEAddResourcePlan.ResourcePlanCreated = false;  //-- 1 = active, 0 = inactive
            objBEAddResourcePlan.ProjectLocation = ddlProjectLocation.SelectedValue;
            objBEAddResourcePlan.ResourceLocation = ddlLocation.SelectedValue;
            objBEAddResourcePlan.Utilization = int.Parse(txtUtilization.Text.Trim());
            objBEAddResourcePlan.Billing = int.Parse(txtBilling.Text.Trim());

            if (ddlLocation.SelectedValue == Convert.ToInt32(MasterEnum.ResourceLocation.OnsiteOffshore).ToString())
            {
                objBEAddResourcePlan.ResourceStartDate = DateTime.Parse(ucDatePickerActualStartDate.Text.Trim());
                objBEAddResourcePlan.ResourceEndDate = DateTime.Parse(ucDatePickerActualEndDate.Text.Trim());
                objBEAddResourcePlan.ResourceLocation = ddlActualLocation.SelectedValue;
                objBEAddResourcePlan.ResourcePlanDurationCreated = false;
                objBEAddResourcePlan.Location = int.Parse(ddlLocation.SelectedValue);
                objBEAddResourcePlan.StartDate = DateTime.Parse(ucDatePickerActualStartDate.Text.Trim());
                objBEAddResourcePlan.EndDate = DateTime.Parse(ucDatePickerActualEndDate.Text.Trim());
                objBEAddResourcePlan.NumberOfResources = 1;

                //--Reset fields
                ucDatePickerActualEndDate.Text = "";
                ucDatePickerActualStartDate.Text = "";
                ddlActualLocation.SelectedValue = "";
            }
            else
            {
                objBEAddResourcePlan.ResourceStartDate = DateTime.Parse(ucDatePickerStartDate.Text.Trim());
                objBEAddResourcePlan.ResourceEndDate = DateTime.Parse(ucDatePickerEndDate.Text.Trim());
                objBEAddResourcePlan.ResourcePlanDurationCreated = true;
                objBEAddResourcePlan.Location = int.Parse(ddlLocation.SelectedValue);
                objBEAddResourcePlan.ResourceLocation = ddlLocation.SelectedValue;
                objBEAddResourcePlan.StartDate = DateTime.Parse(ucDatePickerStartDate.Text.Trim());
                objBEAddResourcePlan.EndDate = DateTime.Parse(ucDatePickerEndDate.Text.Trim());
                objBEAddResourcePlan.NumberOfResources = int.Parse(txtNumberOfResources.Text.Trim());

                //--Reset fields
                ddlRole.SelectedValue = "";
                ddlRole.Enabled = true;
                txtUtilization.Text = "";
                txtBilling.Text = "";
                ucDatePickerStartDate.Text = "";
                ucDatePickerEndDate.Text = "";
                ddlLocation.SelectedValue = "";
                ddlProjectLocation.SelectedValue = "";
                txtNumberOfResources.Text = "";
            }
            Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLAddResourcePlan = new Rave.HR.BusinessLayer.Projects.ResourcePlan();

            //validates billing for RP does not exceed total billing of project
            if (ValidateBilling(Convert.ToInt32(objBEAddResourcePlan.NumberOfResources), Convert.ToInt32(objBEAddResourcePlan.Billing)))
            {
                //method to add new Resource Plan
                objBLLAddResourcePlan.AddResourcePlan(objBEAddResourcePlan, ref ResourcePlanID);
            }
            else
            {
                //assign error message for RP billing cannot be greater than project billing.
                lblMessage.Text = billingErrorMsg;
                lblMessage.Style["color"] = "red";
            }

        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "AddResourcePlan", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// Add resource plan duration detail for RP
    /// </summary>
    private void AddRPDurationDetail()
    {
        try
        {
            BusinessEntities.ResourcePlan objBEAddResourcePlan = new BusinessEntities.ResourcePlan();
            objBEAddResourcePlan.RPId = ResourcePlanID;
            objBEAddResourcePlan.Role = ddlRole.SelectedValue;
            objBEAddResourcePlan.ResourcePlanDurationId = ResourcePlanDurationID;

            //Issue ID : 33940 Mahendra Strat
            if (ViewState["RPduId"] != null && ViewState["FromAddRow"] != null)
            //if (ViewState["RPduId"] != null)
            {
                objBEAddResourcePlan.ResourcePlanDurationId = int.Parse(ViewState["RPduId"].ToString());
                ResourcePlanDurationID = int.Parse(ViewState["RPduId"].ToString());
                ViewState["RPduId"] = null;
                ViewState["FromAddRow"] = null;
            }
            //Issue ID : 33940 Mahendra END

            objBEAddResourcePlan.ProjectLocation = ddlProjectLocation.SelectedValue;
            objBEAddResourcePlan.ResourceLocation = ddlLocation.SelectedValue;
            objBEAddResourcePlan.Utilization = int.Parse(txtUtilization.Text.Trim());
            objBEAddResourcePlan.Billing = int.Parse(txtBilling.Text.Trim());
            if (ddlLocation.SelectedValue == Convert.ToInt32(MasterEnum.ResourceLocation.OnsiteOffshore).ToString())
            {
                objBEAddResourcePlan.StartDate = DateTime.Parse(ucDatePickerActualStartDate.Text.Trim());
                objBEAddResourcePlan.EndDate = DateTime.Parse(ucDatePickerActualEndDate.Text.Trim());
                objBEAddResourcePlan.ResourceStartDate = DateTime.Parse(ucDatePickerActualStartDate.Text.Trim());
                objBEAddResourcePlan.ResourceEndDate = DateTime.Parse(ucDatePickerActualEndDate.Text.Trim());
                objBEAddResourcePlan.ResourcePlanDurationCreated = false;
                objBEAddResourcePlan.ResourceLocation = ddlActualLocation.SelectedValue;
                objBEAddResourcePlan.Location = int.Parse(ddlLocation.SelectedValue);
                ucDatePickerActualStartDate.Text = "";
                ucDatePickerActualEndDate.Text = "";
                ddlActualLocation.SelectedValue = "";
                objBEAddResourcePlan.NumberOfResources = 1;
            }
            else
            {
                objBEAddResourcePlan.ResourceStartDate = DateTime.Parse(ucDatePickerStartDate.Text.Trim());
                objBEAddResourcePlan.ResourceEndDate = DateTime.Parse(ucDatePickerEndDate.Text.Trim());
                objBEAddResourcePlan.ResourcePlanDurationCreated = true;
                objBEAddResourcePlan.ResourceLocation = ddlLocation.SelectedValue;
                objBEAddResourcePlan.Location = int.Parse(ddlLocation.SelectedValue);
                objBEAddResourcePlan.StartDate = DateTime.Parse(ucDatePickerStartDate.Text.Trim());
                objBEAddResourcePlan.EndDate = DateTime.Parse(ucDatePickerEndDate.Text.Trim());
                //Issue ID : 33940 Mahendra Strat
                //objBEAddResourcePlan.NumberOfResources = int.Parse(txtNumberOfResources.Text.Trim());

                if (txtNumberOfResources.Text.Trim() != "")
                    objBEAddResourcePlan.NumberOfResources = int.Parse(txtNumberOfResources.Text.Trim());
                else
                    objBEAddResourcePlan.NumberOfResources = 1;
                //Issue ID : 33940 Mahendra END

                //--reset fields
                ddlRole.SelectedValue = "";
                ddlRole.Enabled = true;
                txtUtilization.Text = "";
                txtBilling.Text = "";
                ucDatePickerStartDate.Text = "";
                ucDatePickerEndDate.Text = "";
                ddlLocation.SelectedValue = "";
                ddlProjectLocation.SelectedValue = "";
                txtNumberOfResources.Text = "";
            }
            Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLAddResourcePlan = new Rave.HR.BusinessLayer.Projects.ResourcePlan();
            //--
            if (ResourcePlanDurationID > 0)
            {
                //Issue ID : 33940 Mahendra Strat
                //--Get the min & max date 
                //objBEAddResourcePlan.StartDate = GetMinDate(grdRPDurationDetail, 0);
                //objBEAddResourcePlan.EndDate = GetMaxDate(grdRPDurationDetail, 0);
                //Issue ID : 33940 Mahendra END

                //validates billing for RP does not exceed total billing of project
                if (ValidateBilling(Convert.ToInt32(objBEAddResourcePlan.NumberOfResources), Convert.ToInt32(objBEAddResourcePlan.Billing)))
                {
                    objBLLAddResourcePlan.AddRPDurationDetail(objBEAddResourcePlan);
                }
                else
                {
                    //assign error message for RP billing cannot be greater than project billing.
                    lblMessage.Text = billingErrorMsg;
                    lblMessage.Style["color"] = "red";
                }
            }
            else
            {
                //validates billing for RP does not exceed total billing of project
                if (ValidateBilling(Convert.ToInt32(objBEAddResourcePlan.NumberOfResources), Convert.ToInt32(objBEAddResourcePlan.Billing)))
                {
                    objBLLAddResourcePlan.AddRPDuration(objBEAddResourcePlan);
                }
                else
                {
                    //assign error message for RP billing cannot be greater than project billing.
                    lblMessage.Text = billingErrorMsg;
                    lblMessage.Style["color"] = "red";
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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "AddRPDurationDetail", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// bind resource plan duration detail grid
    /// </summary>
    private void GridRPDurationDetail()
    {
        try
        {
            BusinessEntities.ResourcePlan objBERPDurationDetail = new BusinessEntities.ResourcePlan();
            objBERPDurationDetail.RPId = ResourcePlanID;
            objBERPDurationDetail.ResourcePlanDurationCreated = false;  //-- 1 = created, 0 = not created

            Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLRPDurationDetail = new Rave.HR.BusinessLayer.Projects.ResourcePlan();

            RaveHRCollection objListRPDurationDetail = new RaveHRCollection();
            objListRPDurationDetail = objBLLRPDurationDetail.GetRPDuration(objBERPDurationDetail);

            //--Bind grid
            grdRPDurationDetail.DataSource = objListRPDurationDetail;
            grdRPDurationDetail.DataBind();

            //--Display grid
            if (grdRPDurationDetail.Rows.Count > 0)
            {
                //--Get entity
                BusinessEntities.ResourcePlan objBEListRPDurationDetail = (BusinessEntities.ResourcePlan)objListRPDurationDetail.Item(0);

                tr_RPDurationDetail.Visible = true;
                ddlRole.SelectedValue = objBEListRPDurationDetail.Role;
                ddlRole.Enabled = false;
            }
            else
            {
                tr_RPDurationDetail.Visible = false;
                ddlRole.SelectedValue = "";
                ddlRole.Enabled = true;
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "GridRPDurationDetail", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Get inactive rp created for the project
    /// </summary>
    private void GetInactiveRPDurationDetail()
    {
        try
        {
            //--Get Inactive ResourcePlan ID
            BusinessEntities.ResourcePlan objBEResourcePlan = new BusinessEntities.ResourcePlan();
            objBEResourcePlan.ProjectId = ProjectID;
            objBEResourcePlan.ResourcePlanCreated = false;  //-- 1 = created, 0 = not created

            Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLResourcePlan = new Rave.HR.BusinessLayer.Projects.ResourcePlan();

            RaveHRCollection objListResourcePlan = new RaveHRCollection();
            objListResourcePlan = objBLLResourcePlan.GetInactiveResourcePlan(objBEResourcePlan);

            if (objListResourcePlan.Count > 0)
            {
                //--Get entity
                BusinessEntities.ResourcePlan objBEListResourcePlan = (BusinessEntities.ResourcePlan)objListResourcePlan.Item(0);

                ResourcePlanID = objBEListResourcePlan.RPId;

                //--Get Inactive RP duration id 
                objBEResourcePlan.RPId = ResourcePlanID;
                objBEResourcePlan.ResourcePlanDurationCreated = false; //-- 1 = created, 0 = not created

                objListResourcePlan = new RaveHRCollection();
                objListResourcePlan = objBLLResourcePlan.GetInactiveRPDurationDetail(objBEResourcePlan);

                //--Check count
                if (objListResourcePlan.Count > 0)
                {
                    //--get entity
                    objBEListResourcePlan = (BusinessEntities.ResourcePlan)objListResourcePlan.Item(0);

                    ResourcePlanDurationID = objBEListResourcePlan.ResourcePlanDurationId;
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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "GetInactiveRPDurationDetail", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Bind child grid displaying resource plan duration detail
    /// </summary>
    private void BindChildGridRPDurationDetail(int RPDurationId, GridView grdChildRPDurationDetail)
    {
        try
        {
            BusinessEntities.ResourcePlan objBERPDurationDetail = new BusinessEntities.ResourcePlan();
            objBERPDurationDetail.ResourcePlanDurationId = RPDurationId;
            objBERPDurationDetail.Mode = "SELECT";

            Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLRPDurationDetail = new Rave.HR.BusinessLayer.Projects.ResourcePlan();

            RaveHRCollection objListRPDurationDetail = new RaveHRCollection();
            objListRPDurationDetail = objBLLRPDurationDetail.GetRPDurationDetail(objBERPDurationDetail);

            //--Bind grid
            grdChildRPDurationDetail.DataSource = objListRPDurationDetail;
            grdChildRPDurationDetail.DataBind();

            ////Issue ID : 33940 Mahendra Strat
            lstEndDateRPDuration = new List<DateTime> { };
            foreach (GridViewRow grv in grdChildRPDurationDetail.Rows)
            {
                //Label lblEndDt = grv.FindControl("");
                lstEndDateRPDuration.Add(Convert.ToDateTime(grv.Cells[7].Text.ToString()));
            }
            Session["RPEndDateForSameRole"] = null;
            Session["RPEndDateForSameRole"] = lstEndDateRPDuration;
            //Issue ID : 33940 Mahendra End
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }

        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "BindChildGridRPDurationDetail", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Remove resource plan duration method
    /// </summary>
    private void RemoveRPDuration(int RPDurationId)
    {
        try
        {
            BusinessEntities.ResourcePlan objBEResourcePlan = new BusinessEntities.ResourcePlan();
            objBEResourcePlan.ResourcePlanDurationId = RPDurationId;

            Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLResourcePlan = new Rave.HR.BusinessLayer.Projects.ResourcePlan();
            objBLLResourcePlan.DeleteRPDuration(objBEResourcePlan);
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "RemoveRPDuration", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Diable date fields method
    /// </summary>
    private void DisableDateFields()
    {
        try
        {
            //object[] txtBoxArr = { txtResourceStartDate, txtResourceEndDate, ucDatePickerStartDate, ucDatePickerEndDate };
            object[] txtBoxArr = { ucDatePickerStartDate, ucDatePickerEndDate, ucDatePickerActualStartDate, ucDatePickerActualEndDate };
            foreach (TextBox txtBox in txtBoxArr)
            {
                txtBox.Attributes.Add("onkeypress", "return Block()");
                txtBox.Attributes.Add("onkeyup", "return Block()");
                txtBox.Attributes.Add("onkeydown", "return Block()");
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "DisableDateFields", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
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
            if ((int.Parse(Session[SessionNames.PageCount].ToString()) == 1) && (grdResourcePlan.Rows.Count > PAGE_SIZE))
            {
                AddSortImage(grdResourcePlan.HeaderRow);
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
    private void SortGridView(string sortExpression, string direction)
    {
        try
        {
            //--Get RP data
            objListResourcePlan = GetResourcePlanList(direction);

            //Method to get billing value
            GetTotalBilling();

            //--Bind Grid
            grdResourcePlan.DataSource = objListResourcePlan;
            grdResourcePlan.DataBind();

            //--table row
            if (grdResourcePlan.Rows.Count > 0)
            {
                tr_ResourcePlan.Visible = true;
            }
            else
            {
                tr_ResourcePlan.Visible = false;
            }

            //--Disable/enable sorting
            if ((int.Parse(Session[SessionNames.PageCount].ToString()) == 1) && (objListResourcePlan.Count == 1))
            {
                grdResourcePlan.AllowSorting = false;
            }
            else
            {
                grdResourcePlan.AllowSorting = true;
            }

            //Finds the Paging Text Box, Previous and Next Link Buttons in Grid Views Pagning Templet
            GridViewRow gvrPager = grdResourcePlan.BottomPagerRow;
            if (gvrPager == null)
            {
                return;
            }
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            LinkButton lbtnPrevious = (LinkButton)gvrPager.Cells[0].FindControl("lbtnPrevious");
            LinkButton lbtnNext = (LinkButton)gvrPager.Cells[0].FindControl("lbtnNext");

            if (pageCount > 1)
            {
                grdResourcePlan.BottomPagerRow.Visible = true;
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
    /// Bind resource plan grid
    /// </summary>
    private void GridResourcePlan()
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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "GridResourcePlan", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Returns Resource Plan data for GridView
    /// </summary>
    private RaveHRCollection GetResourcePlanList(string direction)
    {
        RaveHRCollection objListGetResourcePlan = null;
        try
        {
            //--Fill entiry
            BusinessEntities.ResourcePlan objBEResourcePlan = new BusinessEntities.ResourcePlan();
            objBEResourcePlan.ProjectId = ProjectID;
            objBEResourcePlan.ResourcePlanCreated = false;  //- true-Created, false-Not Created
            objBEResourcePlan.ResourcePlanDurationCreated = true;   //- true-Created, false-Not Created
            objBEResourcePlan.PageSize = PAGE_SIZE;

            //--
            if (grdResourcePlan.BottomPagerRow != null)
            {
                GridViewRow gvrPager = grdResourcePlan.BottomPagerRow;
                TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
                if ((txtPages.Text.Trim() != "") && (int.Parse(txtPages.Text.Trim()) != 0) && (int.Parse(txtPages.Text.Trim()) <= int.Parse(Session[SessionNames.PageCount].ToString())))
                {
                    objBEResourcePlan.PageNumber = int.Parse(txtPages.Text.Trim());
                }
            }
            else
            {
                objBEResourcePlan.PageNumber = 1;
            }

            //--Sort Expression
            objBEResourcePlan.SortExpression = sortExpression;
            objBEResourcePlan.SortDirection = direction;

            //--Get data
            Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLGetResourcePlan = new Rave.HR.BusinessLayer.Projects.ResourcePlan();
            objListGetResourcePlan = new RaveHRCollection();

            objListGetResourcePlan = objBLLGetResourcePlan.GetResourcePlan(objBEResourcePlan, ref pageCount);

            //--Get pagecount in viewstate
            Session[SessionNames.PageCount] = pageCount;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "GetResourcePlanList", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
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
                    case "RPDuRowNo":
                        headerRow.Cells[1].Controls.Add(sortImage);
                        break;
                    case "Role":
                        headerRow.Cells[2].Controls.Add(sortImage);
                        break;
                    case "StartDate":
                        headerRow.Cells[3].Controls.Add(sortImage);
                        break;
                    case "EndDate":
                        headerRow.Cells[4].Controls.Add(sortImage);
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
    /// Add js functions to page controls.
    /// </summary>
    private void AddJsToControls()
    {
        try
        {
            //--Number of Resource Validation
            txtNumberOfResources.Attributes.Add("onblur", "ValidateTextBoxControl('" + txtNumberOfResources.ClientID + "', '" + imgErrorNoOfResources.ClientID + "', '" + CommonConstants.VALIDATE_NUMERIC_FUNCTION + "' );checkTxtBoxRange('" + txtNumberOfResources.ClientID + "', '" + imgErrorNoOfResources.ClientID + "' ,'" + CommonConstants.MSG_MAXVALUE + "')");
            //Sanju:Issue Id 50201:Added event(backspace and delete were not working in firefox)
            txtNumberOfResources.Attributes.Add("onKeyPress", "return MultiLineTextBox('" + txtNumberOfResources.ClientID + "', '" + txtNumberOfResources.MaxLength + "', '" + imgErrorNoOfResources.ClientID + "',event);");
            //Sanju:Issue Id 50201 End
            imgErrorNoOfResources.Attributes.Add("onmouseover", "return ShowTooltip('" + spanNoOfResourcesTooltip.ClientID + "', '" + CommonConstants.MSG_MAXVALUE + "')");
            imgErrorNoOfResources.Attributes.Add("onmouseout", "return HideTooltip('" + spanNoOfResourcesTooltip.ClientID + "')");

            //--ResourceDurationStartDate Validation
            ucDatePickerStartDate.TextBox.Attributes.Add("onchange", "ValidateTextBoxControl('" + ucDatePickerStartDate.ClientID + "' ); CheckDates('" + ucDatePickerStartDate.ClientID + "', '" + ucDatePickerEndDate.ClientID + "', '" + imgErrorResourceDurationStartDate.ClientID + "', '" + imgErrorResourceDurationEndDate.ClientID + "')");
            imgErrorResourceDurationStartDate.Attributes.Add("onmouseover", "return ShowTooltip('" + spanResourceDurationStartDateTooltip.ClientID + "', '" + CommonConstants.MSG_DATERANGE + "')");
            imgErrorResourceDurationStartDate.Attributes.Add("onmouseout", "return HideTooltip('" + spanResourceDurationStartDateTooltip.ClientID + "')");

            //--ResourceDurationEndDate Validation
            ucDatePickerEndDate.TextBox.Attributes.Add("onchange", "ValidateTextBoxControl('" + ucDatePickerEndDate.ClientID + "'); CheckDates('" + ucDatePickerStartDate.ClientID + "', '" + ucDatePickerEndDate.ClientID + "', '" + imgErrorResourceDurationStartDate.ClientID + "', '" + imgErrorResourceDurationEndDate.ClientID + "')");
            imgErrorResourceDurationEndDate.Attributes.Add("onmouseover", "return ShowTooltip('" + spanResourceDurationEndDateTooltip.ClientID + "', '" + CommonConstants.MSG_DATERANGE + "')");
            imgErrorResourceDurationEndDate.Attributes.Add("onmouseout", "return HideTooltip('" + spanResourceDurationEndDateTooltip.ClientID + "')");

            //--ResourceActualStartDate Validation
            ucDatePickerActualStartDate.TextBox.Attributes.Add("onchange", "ValidateTextBoxControl('" + ucDatePickerActualStartDate.ClientID + "' ); CheckDates('" + ucDatePickerActualStartDate.ClientID + "', '" + ucDatePickerActualEndDate.ClientID + "', '" + imgErrorActualResourceStartDate.ClientID + "', '" + imgErrorActualResourceEndDate.ClientID + "')");
            imgErrorActualResourceStartDate.Attributes.Add("onmouseover", "return ShowTooltip('" + spanActualResourcesStartDate.ClientID + "', '" + CommonConstants.MSG_DATERANGE + "')");
            imgErrorActualResourceStartDate.Attributes.Add("onmouseout", "return HideTooltip('" + spanActualResourcesStartDate.ClientID + "')");

            //--Resource ActualEndDate Validation
            ucDatePickerActualEndDate.TextBox.Attributes.Add("onchange", "ValidateTextBoxControl('" + ucDatePickerActualEndDate.ClientID + "'); CheckDates('" + ucDatePickerActualStartDate.ClientID + "', '" + ucDatePickerActualEndDate.ClientID + "', '" + imgErrorActualResourceStartDate.ClientID + "', '" + imgErrorActualResourceEndDate.ClientID + "')");
            imgErrorActualResourceEndDate.Attributes.Add("onmouseover", "return ShowTooltip('" + spanActualResourceEndDate.ClientID + "', '" + CommonConstants.MSG_DATERANGE + "')");
            imgErrorActualResourceEndDate.Attributes.Add("onmouseout", "return HideTooltip('" + spanActualResourceEndDate.ClientID + "')");

            ddlRole.Attributes.Add("onchange", "return CheckRequiredDropDownFields('" + ddlRole.ClientID + "', 'spanRole')");
            //ddlLocation.Attributes.Add("onchange", "return CheckRequiredDropDownFields('" + ddlLocation.ClientID + "', 'spanLocation')");
            ddlActualLocation.Attributes.Add("onchange", "return CheckRequiredDropDownFields('" + ddlActualLocation.ClientID + "', 'spanActualLocation')");
            ddlProjectLocation.Attributes.Add("onchange", "return CheckRequiredDropDownFields('" + ddlProjectLocation.ClientID + "', 'spanProjectLocation')");

            //--Utilization Validation
            txtUtilization.Attributes.Add("onblur", "ValidateTextBoxControl('" + txtUtilization.ClientID + "', '" + imgUtilization.ClientID + "', '" + CommonConstants.VALIDATE_NUMERIC_FUNCTION + "' );checkTxtBoxRange('" + txtUtilization.ClientID + "', '" + imgUtilization.ClientID + "' ,'" + CommonConstants.MSG_MAXVALUE + "')");
            //Sanju:Issue Id 50201:Added event(backspace and delete were not working in firefox)
            txtUtilization.Attributes.Add("onKeyPress", "return MultiLineTextBox('" + txtUtilization.ClientID + "', '" + txtUtilization.MaxLength + "', '" + imgUtilization.ClientID + "',event);");
            //Sanju:Issue Id 50201 End
            imgUtilization.Attributes.Add("onmouseover", "return ShowTooltip('" + spanUtilizationTooltip.ClientID + "', '" + CommonConstants.MSG_MAXVALUE + "')");
            imgUtilization.Attributes.Add("onmouseout", "return HideTooltip('" + spanUtilizationTooltip.ClientID + "')");
            //--Billing Validation
            txtBilling.Attributes.Add("onblur", "ValidateTextBoxControl('" + txtBilling.ClientID + "', '" + imgBilling.ClientID + "', '" + CommonConstants.VALIDATE_BILLINGNUMERIC_FUNCTION + "' );checkTxtBoxRange('" + txtBilling.ClientID + "', '" + imgBilling.ClientID + "' ,'" + CommonConstants.MSG_MAXVALUE + "')");
            //Sanju:Issue Id 50201:Added event(backspace and delete were not working in firefox)
            txtBilling.Attributes.Add("onKeyPress", "return MultiLineTextBox('" + txtBilling.ClientID + "', '" + txtBilling.MaxLength + "', '" + imgBilling.ClientID + "',event);");
            //Sanju:Issue Id 50201 End
            imgBilling.Attributes.Add("onmouseover", "return ShowTooltip('" + spanBillingTooltip.ClientID + "', '" + CommonConstants.MSG_MAXVALUE + "')");
            imgBilling.Attributes.Add("onmouseout", "return HideTooltip('" + spanBillingTooltip.ClientID + "')");

            //--Add Duration button JS
            string txtBoxControlIds = txtNumberOfResources.ClientID + "," + txtUtilization.ClientID + "," + txtBilling.ClientID + "," + ucDatePickerStartDate.ClientID + "," + ucDatePickerEndDate.ClientID;
            //string txtBoxControlIds = txtResourceStartDate.ClientID + "," + txtResourceEndDate.ClientID + "," + txtUtilization.ClientID + "," + txtBilling.ClientID + "," + ucDatePickerStartDate.ClientID + "," + ucDatePickerEndDate.ClientID;

            string ddlControlIds = ddlRole.ClientID + "," + ddlLocation.ClientID + "," + ddlProjectLocation.ClientID;
            string ddlSpanControlIds = "spanRole,spanLocation,spanProjectLocation";
            string errorImgIDs = imgErrorNoOfResources.ClientID + "," + imgUtilization.ClientID + "," + imgBilling.ClientID;

            btnAddDuration.Attributes.Add("onClick", "return Validate('" + txtBoxControlIds + "', '" + ddlControlIds + "', '" + ddlSpanControlIds + "', '" + errorImgIDs + "');");

            //--Add validaiton to AddRow button 
            string txtBoxAddRowControlIds = txtNumberOfResources.ClientID + "," + txtUtilization.ClientID + "," + txtBilling.ClientID + "," + ucDatePickerActualStartDate.ClientID + "," + ucDatePickerActualEndDate.ClientID;
            string ddlAddRowControlIds = ddlRole.ClientID + "," + ddlLocation.ClientID + "," + ddlProjectLocation.ClientID + "," + ddlActualLocation.ClientID;
            string ddlAddRowSpanControlIds = "spanRole,spanLocation,spanProjectLocation,spanActualLocation";
            string errorAddRowImgIDs = imgErrorNoOfResources.ClientID + "," + imgUtilization.ClientID + "," + imgBilling.ClientID + "," + imgErrorActualResourceStartDate.ClientID + "," + imgErrorActualResourceEndDate.ClientID;

            btnAddRow.Attributes.Add("onClick", "return Validate('" + txtBoxAddRowControlIds + "', '" + ddlAddRowControlIds + "', '" + ddlAddRowSpanControlIds + "', '" + errorAddRowImgIDs + "');");
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "AddJsToControls", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Get min date.
    /// </summary>
    private DateTime GetMinDate(GridView grdView, int iDeleteRow)
    {
        try
        {
            DateTime dtMinDate;
            Label lblMinStartDate = (Label)grdView.Rows[0].FindControl("lblResourceStartDate");
            dtMinDate = DateTime.Parse(lblMinStartDate.Text);
            for (int i = 1; i < grdView.Rows.Count; i++)
            {
                lblMinStartDate = (Label)grdView.Rows[i].FindControl("lblResourceStartDate");
                if (dtMinDate > DateTime.Parse(lblMinStartDate.Text))
                {
                    dtMinDate = DateTime.Parse(lblMinStartDate.Text);
                }
            }
            //--
            return dtMinDate;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "GetMessageBody", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Get max date.
    /// </summary>
    private DateTime GetMaxDate(GridView grdView, int iDeleteRow)
    {
        try
        {
            DateTime dtMaxDate;
            Label lblMaxStartDate = (Label)grdView.Rows[0].FindControl("lblResourceEndDate");
            dtMaxDate = DateTime.Parse(lblMaxStartDate.Text);
            for (int i = 1; i < grdView.Rows.Count; i++)
            {
                lblMaxStartDate = (Label)grdView.Rows[i].FindControl("lblResourceEndDate");
                if (dtMaxDate < DateTime.Parse(lblMaxStartDate.Text))
                {
                    dtMaxDate = DateTime.Parse(lblMaxStartDate.Text);
                }
            }

            //--
            return dtMaxDate;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "GetMessageBody", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// date val with project dates.
    /// </summary>
    private bool DateValWithProjectDates(string txtStartDate, string txtEndDate)
    {
        try
        {
            //--StartDate & EndDate should be within project start and enddate
            DateTime dtProjectStartDate, dtProjectEndDate;
            BusinessEntities.ResourcePlan objBEResourcePlan = new BusinessEntities.ResourcePlan();
            Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLResourcePlan = new Rave.HR.BusinessLayer.Projects.ResourcePlan();
            objBEResourcePlan.ProjectId = ProjectID;
            objBEResourcePlan = objBLLResourcePlan.GetProjectDetails(objBEResourcePlan);

            //--add to temp var
            dtProjectStartDate = objBEResourcePlan.StartDate;
            dtProjectEndDate = objBEResourcePlan.EndDate;


            //--Start Date Validation
            if ((DateTime.Parse(txtStartDate.Trim()) > dtProjectEndDate) || (DateTime.Parse(txtStartDate.Trim()) < dtProjectStartDate))
            {
                lblMessage.Text = dateErrorMsg + " i.e between " + dtProjectStartDate.ToString(CommonConstants.DATE_FORMAT) + " & " + dtProjectEndDate.ToString(CommonConstants.DATE_FORMAT);
                lblMessage.Style["color"] = "red";
                return false;
            }

            //--End Date Validation
            if ((DateTime.Parse(txtEndDate.Trim()) > dtProjectEndDate) || (DateTime.Parse(txtEndDate.Trim()) < dtProjectStartDate))
            {
                lblMessage.Text = dateErrorMsg + " i.e between " + dtProjectStartDate.ToString(CommonConstants.DATE_FORMAT) + " & " + dtProjectEndDate.ToString(CommonConstants.DATE_FORMAT);
                lblMessage.Style["color"] = "red";
                return false;
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "GetMessageBody", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }

        //--default result
        return true;
    }

    /// <summary>
    /// checks if duration dates are not repeated.
    /// Created Date:18-Nov-2009.
    /// </summary>
    private bool ValidateRPDuration()
    {

        //declare and define Flag
        bool Flag = true;

        try
        {
            //loops through gridview
            foreach (GridViewRow grv in grdRPDurationDetail.Rows)
            {
                //checks if duration dates are not repeated. 
                if (((DateTime.Parse(ucDatePickerActualStartDate.Text) < DateTime.Parse(((Label)grv.FindControl("lblResourceStartDate")).Text))
                    && (DateTime.Parse(ucDatePickerActualEndDate.Text) < DateTime.Parse(((Label)grv.FindControl("lblResourceStartDate")).Text)))
                                                                ||
                    ((DateTime.Parse(ucDatePickerActualStartDate.Text) > DateTime.Parse(((Label)grv.FindControl("lblResourceEndDate")).Text))
                    && (DateTime.Parse(ucDatePickerActualEndDate.Text) > DateTime.Parse(((Label)grv.FindControl("lblResourceEndDate")).Text)))
                    )
                {
                    //assign true when duration dates are valid
                    Flag = true;
                }
                else
                {
                    //assign false when duration dates are invalid
                    Flag = false;
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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "ValidateRPDuration", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
        return Flag;
    }

    ///// <summary>ss
    ///// Validate Billing for Project by no. of resource
    ///// </summary>
    private bool ValidateBilling(int noOfResource, int billing)
    {
        //--Return default 
        return true;
        
        //--Validation code
        //declare and assign Flag vaue
        bool Flag = false;

        //declare BusinessEntities.ResourcePlan object
        BusinessEntities.ResourcePlan objBEResourcePlan = new BusinessEntities.ResourcePlan();

        //declare Rave.HR.BusinessLayer.Projects.ResourcePlan object
        Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLResourcePlan = new Rave.HR.BusinessLayer.Projects.ResourcePlan();

        //assign projectId value
        objBEResourcePlan.ProjectId = ProjectID;

        //get No. of resource for T&M projects
        objBEResourcePlan = objBLLResourcePlan.GetNoOfResouceByProjectId(objBEResourcePlan);

        //Checks collection object is not null
        if (objBEResourcePlan != null)
        {
            //checks if newly created resource plan does not exceed total  billing of project
            if (Convert.ToInt32(ViewState[RESOURCEBILLING]) == 0)
            {
                if (Convert.ToDecimal(objBEResourcePlan.ResourceNo) * 100 >= noOfResource * billing)
                {
                    //return value
                    Flag = true;
                }
            }
            else if (Convert.ToDecimal(objBEResourcePlan.ResourceNo) * 100 >= Convert.ToInt32(ViewState[RESOURCEBILLING]) + (noOfResource * billing))
            {
                //return value    
                Flag = true;
            }
        }
        else
        {
            //for projects other than T&M
            Flag = true;
        }

        //return value
        return Flag;
    }

    /// <summary>
    /// Method to get total billing
    /// </summary>
    private void GetTotalBilling()
    {
        //loops through gridview to get totalbilling.
        foreach (BusinessEntities.ResourcePlanDetail objlist in objListResourcePlan)
        {
            //assign rp billing to totalBilling variable
            if (totalBilling != 0)
            {
                totalBilling = totalBilling + Convert.ToInt32(objlist.Billing);
            }
            else
            {
                totalBilling = Convert.ToInt32(objlist.Billing);
            }
        }

        ViewState[RESOURCEBILLING] = totalBilling;
    }

    /// <summary>
    /// method to get project details
    /// </summary>
    private void GetProjectDetails()
    {
        try
        {
            //declare Rave.HR.BusinessLayer.Projects.ResourcePlan object
            Rave.HR.BusinessLayer.Projects.Projects objBLLProjects = new Rave.HR.BusinessLayer.Projects.Projects();
            BusinessEntities.Projects objBEProjects = objBLLProjects.RetrieveProjectDetails(ProjectID);

            //--Assign project name
            if (objBEProjects != null)
            {
                lblProjectName.Text = objBEProjects.ClientName + "-" + objBEProjects.ProjectName;
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "GetProjectDetails", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    #endregion Private Member Functions

    #region Public Member Functions

    /// <summary>
    /// Authorize User
    /// </summary>
    public void AuthorizeUserForPageView()
    {
        Common.AuthorizationManager.AuthorizationManager objAuthorizationManager = new Common.AuthorizationManager.AuthorizationManager();
        objAuthorizationManager.AuthorizeUserForPageView(new object[] { AuthorizationManagerConstants.OPERATION_PAGE_CREATERP_VIEW });
    }

    #endregion Public Member Functions
}
