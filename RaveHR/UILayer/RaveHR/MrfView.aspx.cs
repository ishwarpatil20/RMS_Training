//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           MrfView.aspx.cs    
//  Author:         Sunil.Mishra
//  Date written:   22/9/2009/ 12:01:00 PM
//  Description:    This Page will be the entry page for View the MRF
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  22/9/2009/ 12:01:00 PM  Sunil.Mishra    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Web.UI.WebControls;
using BusinessEntities;
using Common.Constants;
using Common;
using Common.AuthorizationManager;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Web.UI;
using System.Drawing;

public partial class MrfView : BaseClass
{
    #region Variable
    //Declare RaveHRCollection Object
    RaveHRCollection raveHRCollectionCopy = new RaveHRCollection();

    //Declare RaveHRCollection Object
    RaveHRCollection raveHRCollection = new RaveHRCollection();

    private const string PENDINGEXTERNALALLOCATION = "PendingExternalAllocation";

    //Rakesh : Actual vs Budget 10/06/2016 Begin  
    private bool IsPendingAllocation = false;
    //End

    //Rakesh : Change Purpose on  MRF Move 10/06/2016 Begin   
    private bool IsMoveMRF = false;

    //End

    ////Declare Business Layer MRFDetail Object
    Rave.HR.BusinessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.BusinessLayer.MRF.MRFDetail();

    //Declare BusinessEntities Layer MRFDetail Object
    BusinessEntities.MRFDetail entitymRFDetail = new MRFDetail();

    //Const for DateFormat
    const string DATEFORMAT = "dd/MM/yyyy";

    //Declare BusinessLayer master Object
    Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

    //Const ReadOnly
    const string ReadOnly = "readonly";

    ImageButton btn;

    //
    private MRFDetail mrfDetail = new MRFDetail();

    //
    const string AllocateResourec = "AllocateResourec";

    /// <summary>
    /// 
    /// </summary>
    const string AbortMRF = "Abort MRF";

    const string ABORTED = "Abort";

    /// <summary>
    /// 
    /// </summary>
    const string EditMRF = "Edit MRF";

    /// <summary>
    /// 
    /// </summary>
    const string MoveMRF = "Move MRF";

    /// <summary>
    /// 
    /// </summary>
    const string ReplaceMRF = "Replace MRF";

    /// <summary>
    /// 
    /// </summary>
    const string ViewMRF = "View MRF";

    /// <summary>
    /// 
    /// </summary>
    const string INDEX = "index";

    char[] SPILITER_DASH = { '-' };

    const string CLASS_NAME_VIEWMRF = "MrfView";

    //Declare Rave Domain
    //Googleconfigurable
    //const string RAVEDOMAIN = "@rave-tech.com";

    //Declare Subject
    string subject;

    //Declare string varibale 
    string body;

    const string PAGETYPEMRFSUMMERY = "MRFSUMMERY";

    const string PAGETYPE_PENDING_ALLOCATION = "MRFPendingAllocation";

    const string PAGETYPE = "pagetype";

    const string PAGETYPEAPPREJHEADCOUNT = "APPREJHEADCOUNT";

    const string PAGETYPEAPPREJFINANCE = "APPREJFINANCE";

    const string MRFID = "MRFId";
    //Aarohi : Issue 31838(CR) : 28/12/2011 : Start
    const string PAGETYPE_EDITMAINRP = "EditMainRP";
    //Aarohi : Issue 31838(CR) : 28/12/2011 : End




    DateTime CurrentDate = Convert.ToDateTime(DateTime.Now.ToString(DATEFORMAT));

    //const string LBL_MESSAGE_FOR_FUTUREALLOCATION = "Please select the AllocationType as FutureAllocation can not be happen backdated";
    //const string LBL_MESSAGE_FOR_CURRENTALLOCATION = "Please select the AllocationType as CurretAllocation can not be happen futuredated";
    const string LBL_MESSAGE_FOR_FUTUREALLOCATION = "Type of Allocation cannot be Future when current date is selected.";

    const string LBL_MESSAGE_FOR_CURRENTALLOCATION = "Future date cannot be selected for Current Allocation Type.";

    const string LBL_MESSAGE_FOR_CURRENTALLOCATION2 = "Allocation Date should lie between Required From  and Required Till dates.";

    //33243-Subhra - start Added this string for error message
    const string LBL_MESSAGE_FOR_PROJECTALLOCATION = "Project allocation date should be greater than or equal to employee joining date";
    //33243-Subhra-end
    // Mohamed Dangra : Issue 50873 : 19/05/2014 : Starts                        			 
    // Desc : Any resource allocation start date should not be less than project start date.

    const string LBL_MESSAGE_FOR_PROJECTALLOCATION_LESSTHAN_PROJECT_STARTDATE = "Project allocation date should be greater than or equal to Project start date";
    // Mohamed Dangra : Issue 50873 : 19/05/2014: Ends 

    const string LBL_MESSAGE_FOR_FINALCURRENTALLOCATION = "Request for allocation is done succesfully, email notification is sent to finance for approval.";

    string message = "";

    //Declare AuthorizationManager Class Object
    AuthorizationManager objAuMan = new AuthorizationManager();

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
    /// Defines default value EmployeeEmailId 
    /// </summary>
    private static string EmployeeEmailId = string.Empty;

    /// <summary>
    /// Defines default value RaisedByEmailId 
    /// </summary>
    private static string RaisedByEmailId = string.Empty;

    /// <summary>
    /// Rave Email domain
    /// </summary>
    /// 
    //Googleconfigurable
    //private const string RAVE_DOMAIN = "@rave-tech.com";

    /// <summary>
    /// Define CompanyName
    /// </summary>
    private string CompanyName = "Rave Technologies";

    // Issue Id : 34229 STRAT CONCURRENCY HANDLED Mahendra
    private Random random = new Random();
    // Issue Id : 34229 END CONCURRENCY HANDLED Mahendra

    /// <summary>
    /// ArrayList for Roles For User
    /// </summary>
    ArrayList arrRolesForUser = new ArrayList();

    //Siddhesh Arekar Issue ID : 55884 Closure Type
    KeyValue<string> typeOfAllocation;
    //Siddhesh Arekar Issue ID : 55884 Closure Type

    #endregion

    #region Property
    /// <summary>
    /// Property for Role
    /// </summary>
    public string ROLE
    {
        get
        {
            if (ViewState["ROLE"] != null)
            {
                return Convert.ToString(ViewState["ROLE"]);
            }
            else
            {
                return string.Empty;
            }
        }
        set
        {
            ViewState["ROLE"] = value;
        }
    }

    /// <summary>
    /// Property for MRFSTATUS
    /// </summary>
    public int MRFSTATUS
    {
        get
        {
            if (ViewState["MRFSTATUS"] != null)
            {
                return Convert.ToInt32(ViewState["MRFSTATUS"]);
            }
            else
            {
                return 0;
            }
        }
        set
        {
            ViewState["MRFSTATUS"] = value;
        }
    }

    /// <summary>
    /// Property for MRFPREVIOUSCOUNT
    /// </summary>
    public int MRFPREVIOUSCOUNT
    {
        get
        {
            if (ViewState["MRFPREVIOUSCOUNT"] != null)
            {
                return Convert.ToInt32(ViewState["MRFPREVIOUSCOUNT"]);
            }
            else
            {
                return -1;
            }
        }
        set
        {
            ViewState["MRFPREVIOUSCOUNT"] = value;
        }
    }

    /// <summary>
    /// Property for MRFNEXTCOUNT
    /// </summary>
    public int MRFNEXTCOUNT
    {
        get
        {
            if (ViewState["MRFNEXTCOUNT"] != null)
            {
                return Convert.ToInt32(ViewState["MRFNEXTCOUNT"]);
            }
            else
            {
                return -1;
            }
        }
        set
        {
            ViewState["MRFNEXTCOUNT"] = value;
        }
    }

    /// <summary>
    ///  Property for MRFCURRENTCOUNT
    /// </summary>
    public int MRFCURRENTCOUNT
    {
        get
        {
            if (ViewState["MRFCURRENTCOUNT"] != null)
            {
                return Convert.ToInt32(ViewState["MRFCURRENTCOUNT"]);
            }
            else
            {
                return -1;
            }
        }
        set
        {
            ViewState["MRFCURRENTCOUNT"] = value;
        }
    }

    #endregion

    #region PageEvents
    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!ValidateURL())
        {
            Response.Redirect(CommonConstants.INVALIDURL, false);
        }
        else
        {
            #region Javascript Function Call

            //Check if user is coming from "raiseHeadCount pop-up
            if (Session[SessionNames.RAISE_HEAD_COUNT] != null)
            {
                //Redirect the user to "Page_MrfPendingAllocation" page
                Response.Redirect(CommonConstants.Page_MrfPendingAllocation, false);
                Session[SessionNames.RAISE_HEAD_COUNT] = null;
            }

            //checkResourceSelected()
            // Issue Id : 34229 STRAT CONCURRENCY HANDLED Mahendra
            btnAllocate.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return checkResourceSelected();");
            // Issue Id : 34229 END CONCURRENCY HANDLED Mahendra
            //btnAllocate.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return checkResourceSelected();");

            //Poonam : Issue : Disable Button : Starts
            btnAllocate.OnClientClick = "if(checkResourceSelected()){" + ClientScript.GetPostBackEventReference(btnAllocate, null) + "}";
            //Poonam : Issue : Disable Button : Ends

            btnDelete.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return ConfirmForDeletingMRF();");
            //txtAllocationDate.Attributes.Add(CommonConstants.EVENT_ONMOUSEOVER, "javascript:ValidateControl('" + txtAllocationDate.ClientID + "','','');");
            btnSelectInternalResource.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return fnInternalResourcePopup();");

            //Umesh: Issue 'Modal Popup issue in chrome' Starts
            btnRaiseHeadCount.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return fnRaiseHeadCountPopup(this);");
            //Umesh: Issue 'Modal Popup issue in chrome' Ends

            btnRaiseMRF.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return ButtonClickValidate();");


            //txtAllocationDate.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtAllocationDate.ClientID + "','" + imgAllocationDateError.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_WITDSPACE + "');");
            //imgAllocationDateError.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanzAllocationDate.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
            //imgAllocationDateError.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanzAllocationDate.ClientID + "');");

            //txtMustHaveSkills.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return MultiLineTextBox('" + txtMustHaveSkills.ClientID + "','" + txtMustHaveSkills.MaxLength + "','" + imgMustHaveSkills.ClientID + "');");
            //txtGoodSkills.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return MultiLineTextBox('" + txtGoodSkills.ClientID + "','" + txtGoodSkills.MaxLength + "','" + imgGoodSkills.ClientID + "');");
            //txtTools.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return MultiLineTextBox('" + txtTools.ClientID + "','" + txtTools.MaxLength + "','" + imgTools.ClientID + "');");
            //txtSoftSkills.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return MultiLineTextBox('" + txtSoftSkills.ClientID + "','" + txtSoftSkills.MaxLength + "','" + imgSoftSkill.ClientID + "');");
            //txtRemarks.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return MultiLineTextBox('" + txtRemarks.ClientID + "','" + txtRemarks.MaxLength + "','" + imgRemarks.ClientID + "');");
            //txtResourceResponsibility.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return MultiLineTextBox('" + txtResourceResponsibility.ClientID + "','" + txtResourceResponsibility.MaxLength + "','" + imgResourceResponsibility.ClientID + "');");
            //txtReason.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return MultiLineTextBox('" + txtReason.ClientID + "','" + txtReason.MaxLength + "','" + imgReason.ClientID + "');");

            txtMustHaveSkills.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return MultiLineTextBoxCheckResponsibility('" + txtMustHaveSkills.ClientID + "','" + txtMustHaveSkills.MaxLength + "','" + imgMustHaveSkills.ClientID + "');");
            imgMustHaveSkills.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanMustHaveSkills.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
            imgMustHaveSkills.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanMustHaveSkills.ClientID + "');");

            txtGoodSkills.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return MultiLineTextBoxCheckResponsibility('" + txtGoodSkills.ClientID + "','" + txtGoodSkills.MaxLength + "','" + imgGoodSkills.ClientID + "');");
            imgGoodSkills.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanGoodSkills.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
            imgGoodSkills.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanGoodSkills.ClientID + "');");

            txtTools.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return MultiLineTextBoxCheckResponsibility('" + txtTools.ClientID + "','" + txtTools.MaxLength + "','" + imgTools.ClientID + "');");
            imgTools.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanTools.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
            imgTools.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanTools.ClientID + "');");

            ddlSkillsCategory.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzSkillCategory.ClientID + "','','');");

            ddlMRFType.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzMRFType.ClientID + "','','');");

            txtExperince.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtExperince.ClientID + "','" + imgExperience.ClientID + "','" + "Decimal" + "');");
            imgExperience.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanExperience.ClientID + "','" + Common.CommonConstants.MSG_DECIMAL + "');");
            imgExperience.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanExperience.ClientID + "');");

            txtExperince1.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return checkExperience1();");
            //txtExperience1.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtExperience1.ClientID + "','" + imgExperience.ClientID + "','" + "Decimal" + "');");
            imgExperience.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanExperience.ClientID + "','" + Common.CommonConstants.MSG_DECIMAL + "');");
            imgExperience.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanExperience.ClientID + "');");


            txtHeighestQualification.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtHeighestQualification.ClientID + "','" + imgHeightQualification.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_WITDSPACE + "');");
            imgHeightQualification.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanHeightQualification.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
            imgHeightQualification.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanHeightQualification.ClientID + "');");

            txtSoftSkills.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return MultiLineTextBoxCheckResponsibility('" + txtSoftSkills.ClientID + "','" + txtSoftSkills.MaxLength + "','" + imgSoftSkill.ClientID + "');");
            imgSoftSkill.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanSoftSkill.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
            imgSoftSkill.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanSoftSkill.ClientID + "');");

            //txtUtilijation.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtUtilijation.ClientID + "','" + imgUtilization.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
            txtUtilijation.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return checkUtilization();");
            imgUtilization.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanUtilization.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
            imgUtilization.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanUtilization.ClientID + "');");

            //txtBilling.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtBilling.ClientID + "','" + imgBilling.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
            txtBilling.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return checkBilling();");
            imgBilling.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanBilling.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
            imgBilling.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanBilling.ClientID + "');");

            txtTargetCTC.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtTargetCTC.ClientID + "','" + imgCTC.ClientID + "','" + "Decimal" + "');");
            imgCTC.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanCTC.ClientID + "','" + Common.CommonConstants.MSG_DECIMAL + "');");
            imgCTC.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanCTC.ClientID + "');");

            txtTargetCTC1.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return checkCTC1();");

            //32182-Ambar-Start-30122011
            txtTargetCTC.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return checkCTC2();");
            //32182-Ambar-End-30122011

            //txtCTC1.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtCTC1.ClientID + "','" + imgCTC.ClientID + "','" + "Decimal" + "');");
            imgCTC.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanCTC.ClientID + "','" + Common.CommonConstants.MSG_DECIMAL + "');");
            imgCTC.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanCTC.ClientID + "');");

            txtRemarks.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return MultiLineTextBoxCheckResponsibility('" + txtRemarks.ClientID + "','" + txtRemarks.MaxLength + "','" + imgRemarks.ClientID + "');");
            imgRemarks.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanRemarks.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
            imgRemarks.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanRemarks.ClientID + "');");

            txtResourceResponsibility.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return MultiLineTextBoxCheckResponsibility('" + txtResourceResponsibility.ClientID + "','" + txtResourceResponsibility.MaxLength + "','" + imgResourceResponsibility.ClientID + "');");
            imgResourceResponsibility.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanResourceResponsibility.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
            imgResourceResponsibility.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanResourceResponsibility.ClientID + "');");

            //txtReason.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtReason.ClientID + "','" + imgReason.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_WITDSPACE + "');");
            txtReason.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return MultiLineTextBoxCheckReason('" + txtReason.ClientID + "','" + txtReason.MaxLength + "','" + imgReason.ClientID + "');");

            imgReason.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanReason.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
            imgReason.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanReason.ClientID + "');");

            imgResponsiblePersonSearch.Attributes.Add("onclick", "return popUpEmployeeSearch();");
            imgPurpose.Attributes.Add("onclick", "return popUpEmployeeName();");
            //Ishwar Patil 20042015 Start
            imgSkillsSearch.Attributes.Add("onclick", "return popUpSkillSearch();");
            //Ishwar Patil 20042015 End

            //Readonly TestBoxs
            txtRequiredFrom.Attributes.Add(ReadOnly, ReadOnly);

            //Readonly TestBoxs
            txtRequiredTill.Attributes.Add(ReadOnly, ReadOnly);

            //Readonly TestBoxs
            txtDateOfRequisition.Attributes.Add(ReadOnly, ReadOnly);

            DeleteImage.Attributes.Add("onclick", "Javascript:return fnConfirmDelete();");

            txtClientName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtClientName.ClientID + "','" + imgClientName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_WITDSPACE + "');");
            imgClientName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanClientName.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
            imgClientName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanClientName.ClientID + "');");



            #endregion


            try
            {
                lblMessage.Text = string.Empty;
                // Issue Id : 34229 STRAT CONCURRENCY HANDLED Mahendra
                SetToken();
                // Issue Id : 34229 END CONCURRENCY HANDLED Mahendra
                //Siddhesh Arekar Issue ID : 55884 Closure Type
                SetTypeOfAllocation();
                //Siddhesh Arekar Issue ID : 55884 Closure Type
                if (!IsPostBack)
                {
                    //Function will Set MRF Index
                    SetMRFIndex();

                    //Save button visibe false
                    btnSavebtn.Visible = false;

                    //Role Related Coding
                    #region Page Role

                    AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
                    arrRolesForUser = objRaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());

                    if (arrRolesForUser.Count != 0)
                    {
                        if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM))
                        {
                            ddlDepartment.Enabled = true;
                            ROLE = AuthorizationManagerConstants.ROLERPM;
                        }
                        else if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEPM) || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEAPM) || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEGPM))
                        {
                            ROLE = AuthorizationManagerConstants.ROLEPM;
                            ddlDepartment.SelectedIndex = 1;
                            ddlDepartment.Enabled = false;
                            FillProjectNameDropDown();
                        }
                        else if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEPRESALES))
                        {
                            ROLE = AuthorizationManagerConstants.ROLEPRESALES;
                        }
                        else if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERECRUITMENT))
                        {
                            ROLE = AuthorizationManagerConstants.ROLERECRUITMENT;
                        }
                        else if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEFINANCE))
                        {
                            ROLE = AuthorizationManagerConstants.ROLEFINANCE;
                        }
                        else if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLECFM))
                        {
                            ROLE = AuthorizationManagerConstants.ROLECFM;
                        }
                        else if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEFM))
                        {
                            ROLE = AuthorizationManagerConstants.ROLEFM;
                        }
                    }
                    #endregion Page Role

                    //if page is calling from Pending Allocation Screen.
                    if (Request.QueryString[AllocateResourec] != null)
                    {
                        Boolean allocateResource = Convert.ToBoolean(DecryptQueryString(AllocateResourec).ToString());
                        if (Request.QueryString[PAGETYPE] != null)
                        {

                            if (allocateResource && DecryptQueryString(PAGETYPE) == PAGETYPE_PENDING_ALLOCATION)
                            {
                                //Function will visible controls
                                IsPendingAllocation = true;
                                SetVisibleAllocateResourceControl(true);

                            }

                        }
                    }
                    //IF Page is call from MRF Summery Page.
                    else
                    {
                        pnlViewMRF.Visible = true;
                        pnlHeaderAllocateResource.Visible = false;
                        pnlHeaderViewMRF.Visible = true;
                        lblHeaderViewEdit.Text = ViewMRF;
                    }

                    //IF MRF ID is not null
                    int LocalMRFID = 0;
                    if (Request.QueryString[CommonConstants.MRF_ID] != null)
                    {
                        mrfDetail.MRFId = Convert.ToInt32(DecryptQueryString(QueryStringConstants.MRF_ID).ToString());
                        //VRP: 09Jan2015_Start: Check MRF history
                        LocalMRFID = mrfDetail.MRFId;
                        //VRP: 09Jan2015_End: Check MRF history
                        //Umesh: Issue 'Internal Allocation is not working by modal popup changes' Starts
                        string employeeName = string.Empty;
                        if (Session[SessionNames.MRFDetail] != null)
                            employeeName = ((MRFDetail)Session[SessionNames.MRFDetail]).EmployeeName;

                        if (string.IsNullOrEmpty(employeeName))
                            Session[SessionNames.MRFDetail] = mrfDetail;
                        //Umesh: Issue 'Internal Allocation is not working by modal popup changes' Ends
                    }
                    if (Session[SessionNames.MRFDetail] != null) //IF MRF Detail Session is not null
                    {
                        mrfDetail = (MRFDetail)Session[SessionNames.MRFDetail];
                        //VRP: 09Jan2015_Start: Check MRF history
                        if (LocalMRFID == mrfDetail.MRFId)
                        { //showing the internal Resource selected
                            if (Session[SessionNames.InternalResource] != null)
                            {
                                if (mrfDetail.EmployeeName != null && mrfDetail.EmployeeId != 0)
                                {
                                    SetVisibleAllocateResourceControl(true);
                                    BindInternalResourceDetailsGrid();
                                }
                            }
                        }
                        else
                        {
                            mrfDetail.MRFId = LocalMRFID;
                            Session[SessionNames.MRFDetail] = mrfDetail;
                        }
                        //VRP: 09Jan2015_End: Check MRF history
                    }

                    FillMRFDetail();

                    //MRF Detail will Fill as per selected MRF ID
                    if (DecryptQueryString(PAGETYPE) == PAGETYPE_PENDING_ALLOCATION)
                    {
                        //method to get SLA days
                        GetSLADaysByMrfId(int.Parse(hidMRFID.Value));
                        btnDelete.Visible = false;
                        btnEdit.Visible = true;
                    }

                    hidInternalResourceId.Value = "yes";

                    //Modified By Rajesh; Issue Id 20969. Disabling MRFStatus dropdown if status is Aborted or Closed.
                    if (txtMRFStatus.Value.Trim() == ABORTED
                        || txtMRFStatus.Value.Trim() == MasterEnum.MRFStatus.MarketResearchCompleteAndClosed.ToString())
                    {
                        btnEdit.Enabled = false;
                        txtReason.Enabled = false;
                    }
                    else
                    {
                        btnEdit.Enabled = true;
                    }
                    if (ROLE == AuthorizationManagerConstants.ROLERPM)
                    {
                        mrfDetail = (MRFDetail)Session[SessionNames.MRFDetail];
                        if (mrfDetail.StatusId != Convert.ToInt32(MasterEnum.MRFStatus.Abort)
                           && mrfDetail.StatusId != Convert.ToInt32(MasterEnum.MRFStatus.MarketResearchCompleteAndClosed)
                           && mrfDetail.StatusId != Convert.ToInt32(MasterEnum.MRFStatus.Closed)
                           && mrfDetail.StatusId != Convert.ToInt32(MasterEnum.MRFStatus.Rejected)
                           && mrfDetail.StatusId != Convert.ToInt32(MasterEnum.MRFStatus.Replaced))
                        {
                            btnMoveMRF.Visible = true;
                            btnMoveMRF.Enabled = true;
                        }
                        else
                        {
                            btnMoveMRF.Enabled = false;
                            btnMoveMRF.Visible = false;
                        }
                    }
                    else
                    {
                        lblReasonMoveMRF.Visible = false;
                        lblReasonMoveMRFMandatory.Visible = false;
                        txtReasonMoveMRF.Visible = false;
                        btnMoveMRF.Enabled = false;
                        btnMoveMRF.Visible = false;
                    }
                }

                //Aarohi : Issue 31826 : 16/12/2011 : Start
                if (Session[SessionNames.RESOURCE_JOINED] != null)
                {
                    txtResourceJoined.Text = Session[SessionNames.RESOURCE_JOINED].ToString();
                    //Mahendra Issue Id : 33861 STRAT
                    Session[SessionNames.RESOURCE_JOINED] = null;
                    //Mahendra Issue Id : 33861 END

                }
                //Aarohi : Issue 31826 : 16/12/2011 : End

                if (Request.QueryString[PAGETYPE] != null)
                {
                    if (DecryptQueryString(PAGETYPE) != PAGETYPEMRFSUMMERY && DecryptQueryString(PAGETYPE) != PAGETYPE_PENDING_ALLOCATION)
                    {
                        pnlViewMRF.Visible = true;
                        pnlHeaderAllocateResource.Visible = false;
                        pnlHeaderViewMRF.Visible = true;
                        lblHeaderViewEdit.Text = ViewMRF;
                        btnDelete.Visible = false;
                    }
                }

                Rave.HR.BusinessLayer.MRF.MRFDetail mrfDetailsObj = new Rave.HR.BusinessLayer.MRF.MRFDetail();

                mrfDetail = (MRFDetail)Session[SessionNames.MRFDetail];
                //Poonam : 54324 : Starts

                if (mrfDetail == null)
                {
                    return;
                }
                //Poonam : 54324 : Ends

                hidTypeOfAllocation.Value = mrfDetail.TypeOfAllocationName;
                //Assign Department Selected Value
                //ddlDepartment.SelectedValue = Convert.ToString(mrfDetail.DepartmentId);


                /*Show Internal Resource Allocated Grid For 
                PendingAllocation,PendingExternalAllocation And PendingNewEmployeeAllocation */
                // 27642- Ambar - Added Rejected status in IF condition.
                if (txtMRFStatus.Value == MasterEnum.MRFStatus.PendingNewEmployeeAllocation.ToString()
                    || txtMRFStatus.Value == MasterEnum.MRFStatus.PendingExternalAllocation.ToString()
                    || txtMRFStatus.Value == MasterEnum.MRFStatus.PendingAllocation.ToString()
                    || txtMRFStatus.Value == MasterEnum.MRFStatus.Rejected.ToString())
                {
                    raveHRCollection = new RaveHRCollection();
                    raveHRCollection = mrfDetailsObj.GetEmployeeByMRFID(mrfDetail);

                    foreach (BusinessEntities.Employee objListEmpLoyeeDetails in raveHRCollection)
                    {
                        txtResourceName.Text = objListEmpLoyeeDetails.FirstName + " " +
                                                objListEmpLoyeeDetails.LastName;
                    }
                    if (txtResourceName.Text.Trim() != "" &&
                        txtMRFStatus.Value != MasterEnum.MRFStatus.Closed.ToString() &&
                        txtMRFStatus.Value != MasterEnum.MRFStatus.PendingNewEmployeeAllocation.ToString())
                    {

                        //Rakesh : Actual vs Budget 22/06/2016 Begin  
                        if (txtMRFStatus.Value == MasterEnum.MRFStatus.PendingAllocation.ToString())
                            IsPendingAllocation = true;

                        txtResourceName.Text = mrfDetail.EmployeeName.ToString();
                        hidFutureEmpID.Value = Convert.ToString(mrfDetail.EmployeeId);
                        string strCostCode = "";
                        Rave.HR.BusinessLayer.MRF.MRFDetail mrfDetailsobj = new Rave.HR.BusinessLayer.MRF.MRFDetail();

                        if (ddlProjectName.SelectedValue != null && ddlProjectName.SelectedValue != CommonConstants.SELECT && ddlCostCode.SelectedValue != null)
                            strCostCode = mrfDetailsobj.GET_EmployeeAllocation_CostCode(hidFutureEmpID.Value.CastToInt32(), ddlProjectName.SelectedValue.CastToInt32(),
                                0);
                        //ddlCostCode.SelectedValue.CastToInt32()
                        if (!string.IsNullOrEmpty(strCostCode))
                        {
                            rfCostCodeValidator.Visible = false;
                            lblCostCodeRequired.Visible = false;
                        }
                        else
                        {

                            rfCostCodeValidator.Visible = true;
                            lblCostCodeRequired.Visible = true;
                        }
                        //Rakesh : Actual vs Budget 22/06/2016 End

                        SetVisibleAllocateResourceControl(false);
                        pnlInternalResourceDetails.Visible = true;
                        txtResourceName.Text = mrfDetail.EmployeeName;
                        hidFutureEmpID.Value = Convert.ToString(mrfDetail.EmployeeId);
                    }
                    else if (txtMRFStatus.Value == MasterEnum.MRFStatus.PendingAllocation.ToString() &&
                        hidTypeOfAllocation.Value == CommonConstants.CurrentAllocation.ToString())
                    {


                    }
                    else if (txtMRFStatus.Value == MasterEnum.MRFStatus.PendingNewEmployeeAllocation.ToString())
                    {
                        pnlInternalResourceDetails.Visible = true;
                    }
                    else if (txtMRFStatus.Value == MasterEnum.MRFStatus.PendingAllocation.ToString() &&
                            hidTypeOfAllocation.Value == CommonConstants.FutureAllocation.ToString())
                    {
                        pnlInternalResourceDetails.Visible = true;
                        txtResourceName.Text = mrfDetail.FutureAllocateResourceName.ToString();
                        txtAllocationDate.Text = mrfDetail.FutureAllocationDate;
                    }


                    else
                    {
                        //IF MRF Detail Session is not null
                        if (Session[SessionNames.MRFDetail] != null)
                        {
                            mrfDetail = (MRFDetail)Session[SessionNames.MRFDetail];

                            //showing the internal Resource selected
                            if (Session[SessionNames.InternalResource] != null)
                            {
                                SetVisibleAllocateResourceControl(false);
                                BindInternalResourceDetailsGrid();
                            }
                        }
                    }
                }

                //Displaying Resource Name when status is Resource Join
                if (Session[SessionNames.MRFDetail] != null)
                {
                    mrfDetail = (MRFDetail)Session[SessionNames.MRFDetail];
                }
                if (Session[SessionNames.MRF_STATUS_NAME] != null)
                {
                    if (Session[SessionNames.MRF_STATUS_NAME].ToString() == CommonConstants.RESOURCE_JOIN)
                    {
                        if (Session[SessionNames.MRFDetail] != null)
                        {
                            mrfDetail = (MRFDetail)Session[SessionNames.MRFDetail];

                        }
                        string resourceName = mrfDetailsObj.GetCandidateByMRFID(mrfDetail);
                        lblResourceName.Visible = true;
                        txtResourceJoined.Visible = true;
                        txtResourceJoined.Text = resourceName;
                        txtResourceJoined.Enabled = false;
                    }

                    if (txtMRFStatus.Value.Trim() == PENDINGEXTERNALALLOCATION)
                    {
                        btnRaiseHeadCount.Enabled = false;
                    }
                    else
                    {
                        btnRaiseHeadCount.Enabled = true;
                    }

                    if (DecryptQueryString(PAGETYPE) == PAGETYPEMRFSUMMERY)
                    {
                        FutureAllocationResetControl();
                    }


                }
                //Aarohi : Issue 31838(CR) : 28/12/2011 : Start
                if (Request.QueryString[PAGETYPE] != null)
                {
                    if (DecryptQueryString(PAGETYPE) == PAGETYPE_EDITMAINRP)
                    {
                        btnNext.Visible = false;
                        btnReplaceMRF.Visible = false;
                        btnMoveMRF.Visible = false;
                        btnEdit.Visible = false;
                        // Mahendra : Issue 31838(CR) Start
                        btnPrevious.Visible = false;
                        // Mahendra : Issue 31838(CR) End
                    }
                }
                //Aarohi : Issue 31838(CR) : 28/12/2011 : End      

                // 34825-Ambar-Start-20062012
                // Disable Raise head count and select internal resource if MRF is Pending External Allocation
                if (txtMRFStatus.Value == MasterEnum.MRFStatus.PendingAllocation.ToString())
                {
                    btnRaiseHeadCount.Enabled = true;
                    btnSelectInternalResource.Enabled = true;
                }
                else
                {
                    btnRaiseHeadCount.Enabled = false;
                    btnSelectInternalResource.Enabled = false;
                }
                // 34825-Ambar-End-20062012

            }
            catch (RaveHRException ex)
            {
                LogErrorMessage(ex);
            }
            catch (Exception ex)
            {
                RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                    CLASS_NAME_VIEWMRF, "Page_Load", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
                LogErrorMessage(objEx);
            }

            // string URL = Utility.GetUrl() + "MrfRaiseHeadCount.aspx?" + URLHelper.SecureParameters("MrfId", hidMRFID.Value.ToString()) + "&" + URLHelper.SecureParameters("ProjectName", hidprojectName.Value.ToString()) + "&" + URLHelper.SecureParameters("Role", hidrole.Value.ToString()) + "&" + URLHelper.SecureParameters("Exp", txtExperince.Text + "-" + txtExperince1.Text) + "&" + URLHelper.SecureParameters("TargetCTC", txtTargetCTC.Text + "-" + txtTargetCTC1.Text) + "&" + URLHelper.SecureParameters("Dept", hidDepartment.Value.ToString()) + "&" + URLHelper.SecureParameters("MrfCode", txtMRFCode.Text) + "&" + URLHelper.SecureParameters("SLADays", hidSLADays.Value.ToString()) + "&" + URLHelper.CreateSignature(hidMRFID.Value.ToString(), hidprojectName.Value.ToString(), hidrole.Value.ToString(), txtExperince.Text + "-" + txtExperince1.Text, txtTargetCTC.Text + "-" + txtTargetCTC1.Text, hidDepartment.Value.ToString(), txtMRFCode.Text, hidSLADays.Value.ToString());
            // 57877-Venkatesh-  29042016 : Start 
            // Add sai email if while raising headcount for nis projects
            string URL = "MrfRaiseHeadCount.aspx?" +
                        URLHelper.SecureParameters("MrfId", hidMRFID.Value.ToString()) + "&" +
                        URLHelper.SecureParameters("ProjectName", hidprojectName.Value.ToString()) + "&" +
                        URLHelper.SecureParameters("Role", hidrole.Value.ToString()) + "&" +
                        URLHelper.SecureParameters("Exp", txtExperince.Text + "-" + txtExperince1.Text) + "&" +
                        URLHelper.SecureParameters("TargetCTC", txtTargetCTC.Text + "-" + txtTargetCTC1.Text) + "&" +
                        URLHelper.SecureParameters("Dept", hidDepartment.Value.ToString()) + "&" +
                        URLHelper.SecureParameters("MrfCode", txtMRFCode.Text) + "&" +
                        URLHelper.SecureParameters("SLADays", hidSLADays.Value.ToString()) + "&" +
                        URLHelper.SecureParameters("ProjectId", hidprojectId.Value.ToString()) + "&" +
                        URLHelper.CreateSignature(hidMRFID.Value.ToString(), hidprojectName.Value.ToString(),
                        hidrole.Value.ToString(), txtExperince.Text + "-" + txtExperince1.Text,
                        txtTargetCTC.Text + "-" + txtTargetCTC1.Text, hidDepartment.Value.ToString(),
                        txtMRFCode.Text, hidSLADays.Value.ToString(), hidprojectId.Value.ToString());
            hidEncryptedQueryString.Value = URL;
            // 57877-Venkatesh-29042016 : End
        }
        Session["MovedStatus"] = null;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ChangePage(object sender, CommandEventArgs e)
    {
    }

    /// <summary>
    /// Event fired when Status value is changed
    /// </summary>    
    protected void ddlMRFStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ROLE == AuthorizationManagerConstants.ROLERPM)
        {
            if (ddlMRFStatus.SelectedValue == Convert.ToInt32(MasterEnum.MRFStatus.PendingExternalAllocation).ToString())
            {
                lblExpectedClosureDate.Visible = true;
                lblExpectedClosureDatemandatory.Visible = true;
                uclExpectedClosureDate.Visible = true;
                uclExpectedClosureDate.Text = hidExpectedClosedDate.Value;
                uclExpectedClosureDate.IsEnable = true;
                lblRForExtendingEClDates.Visible = true;
                lblRForExtendingEClDatesmandatory.Visible = true;
                txtReasonExpDate.Visible = true;
                txtReasonExpDate.Text = hidReasonExpectedCloserDate.Value;
                txtReasonExpDate.Enabled = true;
            }
            else
            {
                lblExpectedClosureDate.Visible = true;
                lblExpectedClosureDatemandatory.Visible = true;
                uclExpectedClosureDate.Visible = true;
                uclExpectedClosureDate.IsEnable = false;
                lblRForExtendingEClDates.Visible = true;
                lblRForExtendingEClDatesmandatory.Visible = true;
                txtReasonExpDate.Visible = true;
                txtReasonExpDate.Enabled = false;
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtPages_TextChanged(object sender, EventArgs e)
    { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void btnDelete_Click(object sender, EventArgs e)
    //{
    //  pnlDelete.Visible = true;
    //  btnPrevious.Visible = false;
    //  btnNext.Visible = false;
    //  lblHeaderViewEdit.Text = AbortMRF;
    //  lblReson.Text = "Abort Reason";
    //  txtReason.Text = string.Empty;
    //  btnSavebtn.Visible = true;
    //  btnDelete.Visible = false;
    //  btnEdit.Visible = false;
    //  txtReason.ReadOnly = false;
    //  btnEdit.Enabled = false;
    //  //        FutureAllocationResetControl();

    //}

    /// <summary>
    /// Preview Button Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        PreviousClick();

    }

    /// <summary>
    /// Next Button CLick
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        NextClick();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DeleteMRFDetail();
        Response.Redirect("MRFSummary.aspx");
    }
    /// <summary>
    /// Edit Button CLick
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        string selectedMRFStatus = ddlMRFStatus.SelectedValue;
        FillStatusDropDown("Edit_MRF");
        if (selectedMRFStatus != null)
        {
            ddlMRFStatus.SelectedValue = selectedMRFStatus;
        }
        if (ROLE == AuthorizationManagerConstants.ROLERPM)
        {
            ControlEditEnable(true);
        }
        else
        {
            ControlEditEnable(false);
        }

        btnSavebtn.Visible = true;
        btnEdit.Visible = false;
        btnPrevious.Visible = false;
        btnNext.Visible = false;
        btnDelete.Visible = false;
        lblHeaderViewEdit.Text = EditMRF;
        pnlDelete.Visible = false;
        txtMustHaveSkills.Focus();
        ChangeModeOfMultilineTextBox(false);

        #region Modified By Mohamed Dangra
        // Mohamed : Issue 50791 : 12/05/2014 : Starts                        			  
        // Desc : IN Mrf Details ,GroupId need to implement
        chkGroupId.Visible = true;
        // Mohamed : Issue 50791 : 12/05/2014 : Ends
        #endregion Modified By Mohamed Dangra
        //Aarohi : Issue 31173(CR) : 26/12/2011 : Start
        if (ROLE == AuthorizationManagerConstants.ROLERPM)
        {
            txtRequiredFrom.Enabled = true;
            //Rajan : Issue 40243 : 22/01/2014 : Start
            //MRF end date changed in resource plan should reflect in MRF and Vice versa. 
            //commented the code,till date required as read only
            //txtRequiredTill.Enabled = true;
            txtRequiredTill.Enabled = false;
            //Rajan : Issue 40243 : 22/01/2014 : End
            txtDateOfRequisition.Enabled = true;
        }
        //Aarohi : Issue 31173(CR) : 26/12/2011 : End

        //Ishwar : Issue 49746 : 12/03/2014 : Starts
        //MRF status is enabled true when the MRF status is Pending External Allocation
        if (!string.Equals(ddlMRFStatus.SelectedItem.Text, CommonConstants.MRFStatus_PendingExternalAllocation))
        {
            ddlMRFStatus.Enabled = false;
        }
        //Ishwar : Issue 49746 : 12/03/2014 : End
    }
    /// <summary>
    /// Move MRF Button CLick
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnMoveMRF_Click(object sender, EventArgs e)
    {
        //Rakesh : Change Purpose on  MRF Move 10/06/2016 Begin  
        IsMoveMRF = true;

        ddlPurpose.Enabled = true;
        //   txtPurposeDescription.Enabled = true;
        txtPurposeDescription.Enabled = true;
        ddlPurpose.SelectedValue = CommonConstants.SELECT;
        txtPurposeDescription.Text = "";
        hidEmployeeName.Value = "";
        ddlCostCode.SelectedValue = CommonConstants.SELECT;
        //End
        ddlPuposeDepartment.Visible = false;
        ddlPuposeDepartment.SelectedValue = CommonConstants.SELECT;

        ddlPurpose_SelectedIndexChanged(null, null);
        PopulateDetailsForMoveMrf();
        btnSavebtn.Visible = true;
        btnEdit.Visible = false;
        btnPrevious.Visible = false;
        btnNext.Visible = false;
        btnDelete.Visible = false;
        lblHeaderViewEdit.Text = MoveMRF;
        btnMoveMRF.Visible = false;
        pnlDelete.Visible = false;
        ChangeModeOfMultilineTextBox(false);
        #region Modified By Mohamed Dangra
        // Mohamed : Issue 50791 : 12/05/2014 : Starts                        			  
        // Desc : IN Mrf Details ,GroupId need to implement
        chkGroupId.Visible = true;
        // Mohamed : Issue 50791 : 12/05/2014 : Ends
        #endregion Modified By Mohamed Dangra
    }

    /// <summary>
    /// Edit Button CLick
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReplaceMRF_Click(object sender, EventArgs e)
    {
        //ControlEditEnable(true);

        ddlSkillsCategory.Enabled = true;
        txtHeighestQualification.Enabled = true;
        txtUtilijation.Enabled = true;
        txtBilling.Enabled = true;
        txtTargetCTC.Enabled = true;
        txtTargetCTC1.Enabled = true;
        txtExperince.Enabled = true;
        txtExperince1.Enabled = true;
        ddlMRFType.Enabled = true;
        imgResponsiblePersonSearch.Visible = true;
        //Ishwar Patil 22/04/2015 Start
        imgSkillsSearch.Visible = true;
        //Ishwar Patil 22/04/2015 End
        txtRemarks.Enabled = true;
        txtDomain.Enabled = true;
        txtSoftSkills.ReadOnly = false;
        txtResourceResponsibility.ReadOnly = false;
        txtGoodSkills.ReadOnly = false;
        txtMustHaveSkills.ReadOnly = false;
        txtTools.ReadOnly = false;
        btnReplaceMRF.Visible = false;

        btnSavebtn.Visible = false;
        btnEdit.Visible = false;
        btnPrevious.Visible = false;
        btnNext.Visible = false;
        btnDelete.Visible = false;
        lblHeaderViewEdit.Text = ReplaceMRF;
        pnlDelete.Visible = false;
        txtMustHaveSkills.Focus();
        btnRaiseMRF.Visible = true;
        imgRequiredFrom.Visible = true;
        imgRequiredTill.Visible = true;
        imgRequiredFrom.Enabled = true;
        imgRequiredTill.Enabled = true;
        txtRequiredFrom.Enabled = true;
        //Rajan : Issue 40243 : 22/01/2014 : Start
        //MRF end date changed in resource plan should reflect in MRF and Vice versa. 
        //commented the code,till date required as read only
        //txtRequiredTill.Enabled = true;
        txtRequiredTill.Enabled = false;
        //Rajan : Issue 40243 : 22/01/2014 : End
        pnlDelete.Visible = true;
        txtReason.Text = string.Empty;
        txtReason.Enabled = true;
        txtReason.ReadOnly = false;
        lblReson.Text = "Replace MRF Reason";

        //Added to made date of requisition editable while replacing MRF.
        txtDateOfRequisition.Enabled = true;
        imgDateOfRequisition.Visible = true;
        imgDateOfRequisition.Enabled = true;

        txtDateOfRequisition.Text = DateTime.Now.ToString(DATEFORMAT);

        //Resolve Issue ID:18778
        //Done By Sameer
        ddlRecruiterName.Enabled = false;
    }

    /// <summary>
    /// Cancel Button CLick
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Session[SessionNames.InternalResource] != null)
        {
            Session.Remove(SessionNames.InternalResource);
        }

        //Umesh: Issue Id:54324 - 'To check whether able to allocate internal resource from Pending Allocation' starts
        if (Session[SessionNames.MRFDetail] != null)
        {
            Session.Remove(SessionNames.MRFDetail);
        }
        //Umesh: Issue Id:54324 - 'To check whether able to allocate internal resource from Pending Allocation' ends

        if (Session[SessionNames.CLIENT_NAME] != null)
        {
            Session.Remove(SessionNames.CLIENT_NAME);
        }

        FillMRFDetail();
        #region Modified By Mohamed Dangra
        // Mohamed : Issue 50791 : 12/05/2014 : Starts                        			  
        // Desc : IN Mrf Details ,GroupId need to implement
        chkGroupId.Visible = false;
        // Mohamed : Issue 50791 : 12/05/2014 : Ends
        #endregion Modified By Mohamed Dangra
        if (lblHeaderViewEdit.Text == EditMRF)
        {
            lblHeaderViewEdit.Text = ViewMRF;
            ControlEditEnable(false);
            //Ambar - 21618 - Start
            ddlRecruiterName.Enabled = false;
            ddlMRFStatus.Enabled = false;
            //Ambar - 21618 - End
            //Modified By Rajesh; Issue Id 20516. enable/disable Billing Date
            if (Convert.ToInt32(ddlDepartment.SelectedItem.Value) == Convert.ToInt32(Common.MasterEnum.Departments.Projects)
               || Convert.ToInt32(ddlDepartment.SelectedItem.Value) == Convert.ToInt32(Common.MasterEnum.Departments.RaveForecastedProjects))
            {
                txtBillingDate.IsEnable = false;
                txtBillingDate.TextBox.BorderColor = System.Drawing.Color.Gray;
                txtBillingDate.TextBox.BorderWidth = 1;
            }

            uclExpectedClosureDate.IsEnable = false;
            txtReasonExpDate.Enabled = false;
            uclExpectedClosureDate.Text = hidExpectedClosedDate.Value;
            txtReasonExpDate.Text = hidReasonExpectedCloserDate.Value;

            uclExpectedClosureDate.TextBox.BorderWidth = 1;
            uclExpectedClosureDate.TextBox.BorderColor = System.Drawing.Color.Gray;

            txtReasonExpDate.BorderWidth = 1;
            txtReasonExpDate.BorderColor = System.Drawing.Color.Gray;


            btnSavebtn.Visible = false;
            btnEdit.Visible = true;
            btnPrevious.Visible = true;
            btnNext.Visible = true;

            if (txtMRFStatus.Value == Convert.ToString(MasterEnum.MRFStatus.Abort))
            {
                btnDelete.Visible = false;
                pnlDelete.Visible = true;
            }
            else
            {
                btnDelete.Visible = true;
                pnlDelete.Visible = false;
            }
            //Disable Fields For Recruiters
            DisableFieldsForRecruiters(false);
        }
        else if (lblHeaderViewEdit.Text == AbortMRF)
        {
            lblHeaderViewEdit.Text = ViewMRF;
            ControlEditEnable(false);
            btnSavebtn.Visible = false;

            btnPrevious.Visible = true;
            btnNext.Visible = true;
            btnDelete.Visible = true;
            pnlDelete.Visible = false;
            //if (txtMRFStatus.Value == Convert.ToString(MasterEnum.MRFStatus.PendingAllocation))
            if (txtMRFStatus.Value != Convert.ToString(MasterEnum.MRFStatus.Closed))
            {
                if (txtMRFStatus.Value == MasterEnum.MRFStatus.PendingAllocation.ToString()
                 || txtMRFStatus.Value == MasterEnum.MRFStatus.PendingNewEmployeeAllocation.ToString()
                 || txtMRFStatus.Value == MasterEnum.MRFStatus.PendingExternalAllocation.ToString())
                {
                    btnEdit.Visible = true;
                    btnEdit.Enabled = true;
                }
            }
            //else if (txtMRFStatus.Value == MasterEnum.MRFStatus.PendingAllocation.ToString())
            //{
            //    btnCancel.Visible = true;
            //}
            else
            {
                btnEdit.Visible = false;
                btnEdit.Enabled = false;
            }

            //Disable Fields For Recruiters
            DisableFieldsForRecruiters(false);
        }
        else
        {
            if (Request.QueryString[PAGETYPE] != null)
            {
                if (DecryptQueryString(PAGETYPE) == PAGETYPEAPPREJHEADCOUNT)
                {
                    Response.Redirect(CommonConstants.Page_MrfApproveRejectHeadCount, false);
                }
                else if (DecryptQueryString(PAGETYPE) == PAGETYPEAPPREJFINANCE)
                {
                    Response.Redirect(CommonConstants.Page_MrfPendingApproval, false);
                }
                else if (DecryptQueryString(PAGETYPE) == PAGETYPEMRFSUMMERY)
                {
                    Response.Redirect(CommonConstants.Page_MrfSummary, false);
                }
                else if (DecryptQueryString(PAGETYPE) == PAGETYPE_PENDING_ALLOCATION)
                {
                    Response.Redirect(CommonConstants.Page_MrfPendingAllocation, false);
                }

            }
            if (Request.QueryString[AllocateResourec] != null)
            {
                Response.Redirect(CommonConstants.Page_MrfPendingAllocation, false);
                // Response.Redirect(CommonConstants.Page_MrfSummary, false);
            }
        }
        //Aarohi : Issue 31838(CR) : 28/12/2011 : Start
        if (DecryptQueryString(PAGETYPE) == PAGETYPE_EDITMAINRP)
        {
            Response.Redirect((CommonConstants.EDITMAINRP_PAGE.ToString() + "?" +
            URLHelper.SecureParameters("RPId", Session[SessionNames.RP_ID].ToString()) + "&" +
            URLHelper.SecureParameters("ProjectId", Session[SessionNames.PROJECT_ID].ToString()) + "&" +
            URLHelper.CreateSignature(Session[SessionNames.RP_ID].ToString(), Session[SessionNames.PROJECT_ID].ToString())), false);
        }
        //Aarohi : Issue 31838(CR) : 28/12/2011 : End

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSavebtn_Click(object sender, EventArgs e)
    {
        try
        {
            if (lblHeaderViewEdit.Text == EditMRF)
            {
                EDITMRFDetail();
                //FutureAllocationResetControl();
            }
            else if (lblHeaderViewEdit.Text == MoveMRF)
            {
                MOVEMRFDetails();
            }
            else
            {
                AbortMRFDetail();

            }
            if (ddlMRFStatus.SelectedValue == Convert.ToInt32(MasterEnum.MRFStatus.MarketResearchCompleteAndClosed).ToString())
            {
                btnEdit.Enabled = false;
                txtReason.Enabled = false;
            }

        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                CLASS_NAME_VIEWMRF, "btnSavebtn_Click", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRaiseMRF_Click(object sender, EventArgs e)
    {
        BusinessEntities.MRFDetail MRFDetailobject = new BusinessEntities.MRFDetail();
        try
        {

            if (lblHeaderViewEdit.Text == ReplaceMRF)
            {

                MRFDetailobject = GetControlValuesForReplaceMRF();
                mRFDetail.SendEmailForReplaceMRF(MRFDetailobject);
                Response.Redirect(CommonConstants.Page_MrfSummary, false);
            }


            //resetControl();


        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer,

      CLASS_NAME_VIEWMRF, "btnRaiseMRF_Click", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlPurpose control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    //protected void ddlPurpose_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.HiringForNewRole))
    //    {
    //        txtPurposeDescription.Visible = true;
    //        // Rakesh : Issue 57942 : 10/May/2016 : Starts 

    //        //            lblPurposedescription.Text = "New Role";

    //        //End

    //        lblPurposedescription.Text = "Position Description";

    //        imgPurpose.Visible = false;
    //        txtPurposeDescription.ReadOnly = false;
    //        lblmandatorymark.Visible = true;
    //    }
    //    else if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.HiringForProject))
    //    {
    //        txtPurposeDescription.Visible = true;
    //        lblPurposedescription.Text = "Project Name";
    //        imgPurpose.Visible = false;
    //        txtPurposeDescription.ReadOnly = false;
    //        lblmandatorymark.Visible = true;
    //    }
    //    else if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.Replacement))
    //    {
    //        txtPurposeDescription.Visible = true;
    //        txtPurposeDescription.ReadOnly = true;
    //        lblPurposedescription.Text = "Employee Name";
    //        imgPurpose.Visible = true;
    //        lblmandatorymark.Visible = true;
    //    }

    //        // Rakesh : Issue 57942 : 10/May/2016 : Starts 

    //    else if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.SubstituteForMaternityLeave))
    //    {
    //        txtPurposeDescription.Visible = true;
    //        txtPurposeDescription.ReadOnly = true;
    //        lblPurposedescription.Text = "Employee Name";
    //        imgPurpose.Visible = true;
    //        lblmandatorymark.Visible = true;
    //    }

    //    else if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.HiringforDepartment))
    //    {
    //        txtPurposeDescription.Visible = true;
    //        txtPurposeDescription.ReadOnly = true;
    //        lblPurposedescription.Text = "Department Name";

    //        lblmandatorymark.Visible = true;
    //    }
    //    else if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.Others))
    //    {
    //        txtPurposeDescription.Visible = true;
    //        lblPurposedescription.Text = "Description";
    //        imgPurpose.Visible = false;

    //        txtPurposeDescription.ReadOnly = false;
    //        lblmandatorymark.Visible = true;
    //    }
    //    //End
    //    else if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.HiringForFutureBusiness) || ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.MarketResearchfeasibility))
    //    {
    //        txtPurposeDescription.Visible = true;
    //        lblPurposedescription.Text = "Comment";
    //        imgPurpose.Visible = false;
    //        txtPurposeDescription.ReadOnly = false;
    //        lblmandatorymark.Visible = false;
    //    }
    //    else
    //    {
    //        txtPurposeDescription.Visible = false;
    //        lblPurposedescription.Text = string.Empty;
    //        imgPurpose.Visible = false;
    //        txtPurposeDescription.ReadOnly = false;
    //        lblmandatorymark.Visible = false;
    //    }
    //    txtPurposeDescription.Text = string.Empty;
    //    hidEmployeeName.Value = string.Empty;

    //}


    private void SetPuropseVisibility(bool IsDepartment, bool IsPurposeDescription, bool IsImgPurpose, bool IsPurposeDescriptionReadOnly, bool Islbl, string PurposeDescriptionText)
    {
        ddlPuposeDepartment.Visible = IsDepartment;
        txtPurposeDescription.Visible = IsPurposeDescription;
        imgPurpose.Visible = IsImgPurpose;
        txtPurposeDescription.ReadOnly = IsPurposeDescriptionReadOnly;
        lblmandatorymarkPurpose.Visible = Islbl;
        lblPurposedescription.Text = PurposeDescriptionText;

    }


    protected void ddlPurpose_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtPurposeDescription.Text = string.Empty;
        if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.HiringForNewRole))
        {
            // IsDepartment|IsPurposeDescription|IsImgPurpose|IsPurposeDescriptionReadOnly|Islbl|PurposeDescriptionText
            // SetPuropseVisibility(false, true, false, false, true, "Position Description");

            ddlPuposeDepartment.Visible = false;
            txtPurposeDescription.Visible = true;
            lblPurposedescription.Text = "Position Description";
            imgPurpose.Visible = false;
            txtPurposeDescription.ReadOnly = false;
            lblmandatorymarkPurpose.Visible = true;
        }
        else if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.HiringForProject))
        {

            // IsDepartment|IsPurposeDescription|IsImgPurpose|IsPurposeDescriptionReadOnly|Islbl|PurposeDescriptionText
            //  SetPuropseVisibility(false, true, false, false, true, "Project Name");

            ddlPuposeDepartment.Visible = false;
            txtPurposeDescription.Visible = true;
            lblPurposedescription.Text = "Project Name";
            imgPurpose.Visible = false;
            txtPurposeDescription.ReadOnly = false;
            lblmandatorymarkPurpose.Visible = true;

            //Rakesh : Actual vs Budget 10/06/2016 Begin              
            string strProjectName = (ddlProjectName.SelectedItem != null) ? ddlProjectName.SelectedItem.Text : "";
            txtPurposeDescription.Text = strProjectName;
            //Rakesh : MRF Move Edit Purpose :  10/06/2016 : End

        }
        else if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.Replacement))
        {
            // IsDepartment|IsPurposeDescription|IsImgPurpose|IsPurposeDescriptionReadOnly|Islbl|PurposeDescriptionText
            //SetPuropseVisibility(false, true, false, false, true, "Project Name");


            ddlPuposeDepartment.Visible = false;
            txtPurposeDescription.Visible = true;
            txtPurposeDescription.ReadOnly = true;
            lblPurposedescription.Text = "Employee Name";
            imgPurpose.Visible = true;
            lblmandatorymarkPurpose.Visible = true;
        }

        // Rakesh B 57942 10-5-2016 Start 
        else if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.SubstituteForMaternityLeave))
        {
            ddlPuposeDepartment.Visible = false;
            txtPurposeDescription.Visible = true;
            txtPurposeDescription.ReadOnly = true;
            lblPurposedescription.Text = "Employee Name";
            imgPurpose.Visible = true;
            lblmandatorymarkPurpose.Visible = true;
        }
        else if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.Others))
        {
            txtPurposeDescription.Visible = true;
            lblPurposedescription.Text = "Description";
            imgPurpose.Visible = false;
            txtPurposeDescription.ReadOnly = false;
            lblmandatorymarkPurpose.Visible = true;


            //Venkatesh : Issue 35089 : 24/12/2013 : Start
            //Desc : Remove validation for Project, except Raveforecasted
            //  txtPurpose.Text = txtProjectName.Text;
            //Venkatesh : Issue 35089 : 24/12/2013 : End
        }
        //End
        else if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.MarketResearchfeasibility))
        {
            txtPurposeDescription.Visible = true;
            lblPurposedescription.Text = "Comment";
            imgPurpose.Visible = false;
            txtPurposeDescription.ReadOnly = false;
            lblmandatorymarkPurpose.Visible = false;
            ddlPuposeDepartment.Visible = false;
        }
        else if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.HiringForFutureBusiness))
        {
            txtPurposeDescription.Visible = true;
            lblPurposedescription.Text = "Comment";
            imgPurpose.Visible = false;
            txtPurposeDescription.ReadOnly = false;
            lblmandatorymarkPurpose.Visible = false;
            ddlPuposeDepartment.Visible = false;
        }
        else if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.HiringForInternalProject))
        {
            txtPurposeDescription.Visible = true;
            lblPurposedescription.Text = "Comment";
            imgPurpose.Visible = false;
            txtPurposeDescription.ReadOnly = false;
            lblmandatorymarkPurpose.Visible = true;
            ddlPuposeDepartment.Visible = false;
        }
        else if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.HiringforDepartment))
        {
            // txtPurpose.Visible = true;
            lblPurposedescription.Text = "Department";
            imgPurpose.Visible = false;
            txtPurposeDescription.ReadOnly = false;
            ddlPuposeDepartment.Visible = true;
            txtPurposeDescription.Visible = false;
            ddlPuposeDepartment.Enabled = true;
            lblmandatorymarkPurpose.Visible = true;
        }
        else if (ddlPurpose.SelectedItem.Text == CommonConstants.SELECT)
        {
            lblPurposedescription.Text = string.Empty;
            txtPurposeDescription.Visible = false;
            imgPurpose.Visible = false;
            txtPurposeDescription.ReadOnly = false;
            ddlPuposeDepartment.Visible = false;
            lblmandatorymarkPurpose.Visible = false;
        }
    }

    protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProjectName.SelectedItem.Text != CommonConstants.SELECT)
            {
                FillResourcePlanDropDown(Convert.ToInt32(ddlProjectName.SelectedItem.Value));
                GetProjectDescription(ddlProjectName.SelectedIndex - 1);
                ddlRole.ClearSelection();
                ddlRole.Enabled = false;
                ddlResourcePlanCode.Enabled = true;
                BindCostCode(Convert.ToInt32(ddlProjectName.SelectedItem.Value));

                //Aarohi : Issue 31826 : 16/12/2011 : Start
                btnSavebtn.Visible = true;
                //Aarohi : Issue 31826 : 16/12/2011 : End

            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                CLASS_NAME_VIEWMRF, "ddlProjectName_SelectedIndexChanged", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    protected void ddlResourcePlanCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlResourcePlanCode.SelectedItem.Text != CommonConstants.SELECT)
            {
                ddlRole.Enabled = true;
                FillRoleDropDown(Convert.ToInt32(ddlResourcePlanCode.SelectedValue), Convert.ToInt32(MasterEnum.RPDurationEditionStatus.Deleted));
                if (lblHeaderViewEdit.Text == MoveMRF)
                {
                    if (ddlRole.Items.FindByText(hidMoveMrfRoleName.Value) == null)
                    {
                        lblMessage.Text = "Role " + hidMoveMrfRoleName.Value + " is not present in the Resource Plan (" + ddlResourcePlanCode.SelectedItem.Text + ").";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        ddlRole.Enabled = false;
                        return;
                    }
                    else
                    {
                        ddlRole.Enabled = true;
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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                CLASS_NAME_VIEWMRF, "ddlResourcePlanCode_SelectedIndexChanged", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartment.SelectedItem.Text != Common.CommonConstants.SELECT)
            {
                FillProjectNameDropDown();
                hidDepartment.Value = ddlDepartment.SelectedItem.Value;

                if (Convert.ToInt32(ddlDepartment.SelectedItem.Value) == Convert.ToInt32(Common.MasterEnum.Departments.Projects))
                {
                    ddlProjectName.Enabled = true;
                    ddlResourcePlanCode.Enabled = true;
                    ddlRole.Enabled = false;
                    ddlRole.SelectedIndex = -1;
                    ddlResourcePlanCode.SelectedIndex = -1;
                }
                else
                {
                    if (ddlCostCode.SelectedValue != CommonConstants.SELECT)
                        ddlCostCode.SelectedValue = CommonConstants.SELECT;
                    trCostCode.Visible = false;
                    ddlProjectName.Enabled = false;
                    ddlResourcePlanCode.Enabled = false;
                    ddlProjectName.SelectedIndex = -1;
                    ddlResourcePlanCode.SelectedIndex = -1;
                    ddlRole.Enabled = true;
                    //Fill the role value as per Department
                    DepartmentWiseRoleDispaly(Convert.ToInt32(ddlDepartment.SelectedItem.Value));
                    if (lblHeaderViewEdit.Text == MoveMRF)
                    {
                        if (ddlRole.Items.FindByText(hidMoveMrfRoleName.Value) == null)
                        {
                            //Poonam : Issue : 54911 : Starts
                            if (ddlDepartment.SelectedItem.Text == "Projects")
                            {
                                lblMessage.Text = "Role " + hidMoveMrfRoleName.Value + " not present in the Resource Plan (" + ddlResourcePlanCode.SelectedItem.Text + ")";
                            }
                            //Poonam : Issue : 54911 : Ends
                            else
                            {
                                lblMessage.Text = "Role " + hidMoveMrfRoleName.Value + " not present in the Department (" + ddlDepartment.SelectedItem.Text + ")";
                            }
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            ddlRole.Enabled = false;
                            return;
                        }
                        else
                        {
                            ddlRole.Enabled = true;
                        }
                    }
                }
            }
            else
            {
                //clear items in role dropdown and Insert Select as a Item for Dropdown
                ddlRole.Items.Clear();
                ddlRole.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                //clear items in projectName dropdown and Insert Select as a Item for Dropdown
                ddlProjectName.Items.Clear();
                ddlProjectName.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                //clear items in resourcePlanCode dropdown and Insert Select as a Item for Dropdown
                ddlResourcePlanCode.Items.Clear();
                ddlResourcePlanCode.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                //Enable fields               
                ddlProjectName.Enabled = true;
                ddlResourcePlanCode.Enabled = true;
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                CLASS_NAME_VIEWMRF, "ddlDepartment_SelectedIndexChanged", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {   //Poonam : Issue : RMS : Starts (Added Condition)
            if (ddlProjectName.Enabled == true && ddlProjectName.SelectedValue != "SELECT")
            {
                //Poonam : Issue : RMS : Ends (Added Condition)
                if (ddlRole.SelectedItem.Text != CommonConstants.SELECT)
                {
                    grdresource.Visible = true;
                    FillResourceGrid();
                    hidGridEnabled.Value = "true";
                }
                else
                {
                    grdresource.Visible = false;
                    hidGridEnabled.Value = "false";
                }
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                CLASS_NAME_VIEWMRF, "ddlRole_SelectedIndexChanged", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }


    /// <summary>
    /// Grid Row DataBound Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdresource_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chk = (CheckBox)e.Row.FindControl("chk");
            //btn = (ImageButton)e.Row.FindControl("imgView");
            Label lblRPDurationId = (Label)e.Row.FindControl("lblRPDuId");

            //Modified By Rajesh; Issue Id 20516. Code for enabling and disabling Billing Date field

            if (Convert.ToInt32(ddlDepartment.SelectedItem.Value) == Convert.ToInt32(Common.MasterEnum.Departments.Projects)
             || Convert.ToInt32(ddlDepartment.SelectedItem.Value) == Convert.ToInt32(Common.MasterEnum.Departments.RaveForecastedProjects))
            {
                UIControls_DatePickerControl billingDatePicker = (UIControls_DatePickerControl)e.Row.FindControl("billingDatePicker");
                int billingAmount = ((BusinessEntities.MRFDetail)(e.Row.DataItem)).Billing;
                if (billingAmount > 0)
                {
                    billingDatePicker.IsEnable = true;
                }
                else
                {
                    billingDatePicker.IsEnable = false;
                    billingDatePicker.Text = "";
                }
            }

            string RPStartDate = e.Row.Cells[2].Text;
            string RPEndDate = e.Row.Cells[3].Text;

            //chk.Attributes.Add("onclick", "ValidateCheckBox('" + chk.ClientID + "','" + RPStartDate + "','" + RPEndDate + "')");
            chk.Attributes.Add("onclick", "ValidateCheckBox('" + chk.ClientID + "')");
            //btn.Attributes.Add("onclick", "return popUpResourceSplitDuration('" + lblRPDurationId.Text + "');");
            string URL = string.Empty;
            URL += "ResourcePlanLocationList.aspx?" + URLHelper.SecureParameters("RPDuId", lblRPDurationId.Text) + "&" + URLHelper.CreateSignature(lblRPDurationId.Text);
            hidEncryptedQueryString.Value = URL;
        }
    }

    protected void grdresource_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "View")
            {
                GridView _gridView = (GridView)sender;
                // Get the selected index and the command name
                int _selectedIndex = int.Parse(e.CommandArgument.ToString());
                string _commandName = e.CommandName;
                grdresource.SelectedIndex = _selectedIndex;
                GridViewRow dr = grdresource.Rows[_selectedIndex];
                Label lbl = (Label)dr.Cells[0].FindControl("lblRPDuId");
                hidRPId.Value = lbl.Text;
                btn = (ImageButton)dr.Cells[0].FindControl("imgView");
                btn.Attributes.Add("onclick", "return popUpResourceSplitDuration();");
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "MrfRaisePrevious", "grdresource_RowCommand", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    #endregion

    #region Internal Resource Allocation

    #region Click Event

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelAllocateResource_Click(object sender, EventArgs e)
    {
        //Venkatesh Start: Session null
        Session[SessionNames.InternalResource] = null;
        //Venkatesh End: Session null
        string url = CommonConstants.Page_MrfPendingAllocation + "?" + URLHelper.SecureParameters("AllocateResourec", "True") + "&" + URLHelper.CreateSignature("True");
        Response.Redirect(url, false);
    }

    /// <summary>
    /// Method To Allocate Internal Resource
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAllocate_Click(object sender, EventArgs e)
    {
        try
        {
            //vandana

            //validation of dates
            //txtAllocationDate.Text = mrfDetail.FutureAllocationDate;
            //DateTime Validationdate = Convert.ToDateTime(mrfDetail.FutureAllocationDate);

            //Poonam : 54324 : Starts
            if (mrfDetail == null)
            {
                lblMessage.Text = "MRF is already closed by another user please raise another MRF.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
            //Poonam : 54324 : Ends
            else
            {
                DateTime selecteddate = Convert.ToDateTime(txtAllocationDate.Text);
                string employName = Convert.ToString(txtResourceName.Text);
                hidEmployeeName.Value = employName;

                DateTime fromDate = Convert.ToDateTime(txtRequiredFrom.Text);
                // Rajan Kumar : Issue 45752 : 03/01/2014 : Starts                        			 
                // Desc : When MRF is raised against department then MRF end date is not mandatory (remove validation).
                DateTime toDate = DateTime.MaxValue;
                if (!string.IsNullOrEmpty(txtRequiredTill.Text))
                {
                    toDate = Convert.ToDateTime(txtRequiredTill.Text);
                }
                // Rajan Kumar : Issue 45752 : 03/01/2014: Ends                  

                //33243-Subhra-Start- Added to check whether project allocation date is greater than joining date of employee
                //Commented by mahendra on 16/02/2012 mrfDetail.EmployeeJoiningDate not set properly which is set now in MRFInternalResource.CS 
                //if (selecteddate >= mrfDetail.EmployeeJoiningDate)
                mrfDetail = (MRFDetail)Session[SessionNames.MRFDetail];
                if (selecteddate < mrfDetail.EmployeeJoiningDate)
                {
                    lblMessage.Text = LBL_MESSAGE_FOR_PROJECTALLOCATION;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    txtAllocationDate.BorderColor = System.Drawing.Color.Red;
                    return;
                }
                //33243-Subhra-end

                // Mohamed Dangra : Issue 50873 : 19/05/2014 : Starts                        			 
                // Desc : Any resource allocation start date should not be less than project start date.

                if (selecteddate < mrfDetail.ProjStartDate)
                {
                    lblMessage.Text = LBL_MESSAGE_FOR_PROJECTALLOCATION_LESSTHAN_PROJECT_STARTDATE + " (" + mrfDetail.ProjStartDate.ToString("dd/MM/yyyy") + ").";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    txtAllocationDate.BorderColor = System.Drawing.Color.Red;
                    return;
                }
                // Mohamed Dangra : Issue 50873 : 19/05/2014: Ends 

                if (selecteddate > toDate || selecteddate < fromDate)
                {
                    lblMessage.Text = LBL_MESSAGE_FOR_CURRENTALLOCATION2;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    txtAllocationDate.BorderColor = System.Drawing.Color.Red;
                    return;
                }
                //30926-Subhra-start
                if (selecteddate > Convert.ToDateTime(CurrentDate))
                {
                    lblMessage.Text = LBL_MESSAGE_FOR_CURRENTALLOCATION;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    txtAllocationDate.BorderColor = System.Drawing.Color.Red;
                    return;
                }
                //30926-Subhra-end

                //Siddhesh Arekar Issue ID : 55884 Closure Type
                if (ddlClosureType.SelectedValue == CommonConstants.SELECT)
                {
                    lblMessage.Text = "Please select a Type of Closure.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                //Siddhesh Arekar Issue ID : 55884 Closure Type
                Rave.HR.BusinessLayer.MRF.MRFDetail objMrfDetail = new Rave.HR.BusinessLayer.MRF.MRFDetail();
                // Issue Id : 34229 STRAT CONCURRENCY HANDLED Mahendra
                if (IsTokenValid())
                {
                    // Issue Id : 34229 END CONCURRENCY HANDLED Mahendra
                    if (Convert.ToInt32(hidContractTypeID.Value) != Convert.ToInt32(MasterEnum.ContractType.RaveInternal) &&
                        (txtMRFStatus.Value != MasterEnum.MRFStatus.PendingNewEmployeeAllocation.ToString())
                            && hidDepartment.Value != MasterEnum.Departments.PreSales.ToString()
                            && hidDepartment.Value != CommonConstants.PRESALES_UK
                            && hidDepartment.Value != CommonConstants.PRESALES_USA
                            && hidDepartment.Value != CommonConstants.PRESALES_INDIA
                            )
                    {
                        if (Session[SessionNames.CLIENT_NAME] != null)
                        {
                            if (Session[SessionNames.CLIENT_NAME] != CompanyName)
                            {
                                hidInternalResourceId.Value = "yes";
                                AllocateResource();
                            }
                            else
                            {
                                AllocateResourceForInternalProject();
                            }
                        }
                        else
                        {
                            hidInternalResourceId.Value = "yes";
                            AllocateResource();
                        }
                    }
                    else
                    {
                        AllocateResourceForInternalProject();
                    }
                    // Issue Id : 34229 STRAT CONCURRENCY HANDLED Mahendra
                }
                else
                {
                    Response.Redirect(CommonConstants.Page_MrfPendingAllocation, false);
                }
                // Issue Id : 34229 END CONCURRENCY HANDLED Mahendra
            }

        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                CLASS_NAME_VIEWMRF, "btnAllocate_Click", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }





    //Delete the Selected Internal Resource
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DeleteResource_Click(object sender, EventArgs e)
    {
        hidInternalResourceId.Value = "yes";
        pnlInternalResourceDetails.Visible = false;
        mrfDetail.EmployeeId = 0;
        mrfDetail.EmployeeName = string.Empty;
        mrfDetail.Billing = 0;
        mrfDetail.EmployeeId = 0;
        mrfDetail.Utilization = 0;
        mrfDetail.FutureAllocationDate = string.Empty;
        mrfDetail.FutureTypeOfAllocationID = 0;
        mrfDetail.FutureTypeOfSupplyID = 0;
        ddlClosureType.SelectedItem.Text = "Internal";
        //txtResourceName.Text = string.Empty;


        Session[SessionNames.MRFDetail] = mrfDetail;
        Session.Remove(SessionNames.InternalResource);

        //Umesh: Issue Id:54324 - 'To check whether able to allocate internal resource from Pending Allocation' starts
        if (Session[SessionNames.MRFDetail] != null)
        {
            Session.Remove(SessionNames.MRFDetail);
        }
        //Umesh: Issue Id:54324 - 'To check whether able to allocate internal resource from Pending Allocation' ends

        DeleteUIFutureAllocateEmployee(mrfDetail.MRFId);


        string url = CommonConstants.Page_MrfPendingAllocation + "?" +
         URLHelper.SecureParameters("AllocateResourec", "True") + "&" +
         URLHelper.CreateSignature("True");
        Response.Redirect(url, false);
    }




    #endregion

    #region Method

    /// <summary>
    /// Function will Allocate Resource
    /// </summary>
    private void AllocateResource()
    {
        mrfDetail.Status = CommonConstants.MRFStatus_PendingApprovalOfFinance;
        mrfDetail.MRFId = Convert.ToInt32(hidMRFID.Value);
        mrfDetail.AllocationDate = txtAllocationDate.Text;

        mrfDetail.EmployeeId = Convert.ToInt32(hidFutureEmpID.Value);
        mrfDetail.TypeOfAllocation = Convert.ToInt32(typeOfAllocation.KeyName);
        mrfDetail.TypeOfClosure = Convert.ToInt32(ddlClosureType.SelectedValue);
        mrfDetail.EmployeeName = hidEmployeeName.Value;

        if (ddlCostCode.SelectedValue != CommonConstants.SELECT && !String.IsNullOrEmpty(ddlCostCode.SelectedValue))
            mrfDetail.CostCodeId = ddlCostCode.SelectedValue.CastToInt32();

        if (divOverride.Visible == true)
            mrfDetail.IsOverride = chkOverride.Checked;

        // Rajan Kumar : Issue 46252: 10/02/2014 : Starts                        			 
        // Desc : In MRF history need to implemented in all cases in RMS.
        //Pass Email to know who is going to modified the data            
        AuthorizationManager authoriseduser = new AuthorizationManager();
        mrfDetail.LoggedInUserEmail = authoriseduser.getLoggedInUserEmailId();
        // Rajan Kumar : Issue 46252: 10/02/2014 : END

        try
        {
            int setMRFStatus = Rave.HR.BusinessLayer.MRF.MRFDetail.SetMRFStatus(mrfDetail);
            if (setMRFStatus == 1)
            {
                if (hidDepartment.Value == CommonConstants.RAVECONSULTANT_USA || hidDepartment.Value == CommonConstants.RAVECONSULTANT_UK || hidDepartment.Value == CommonConstants.RAVECONSULTANT_INDIA)
                {
                    int employeeId = Convert.ToInt32(hidFutureEmpID.Value);
                    Rave.HR.BusinessLayer.Employee.Employee employee = new Rave.HR.BusinessLayer.Employee.Employee();
                    BusinessEntities.Employee objUpdateEmployee = new BusinessEntities.Employee();
                    objUpdateEmployee.EMPId = employeeId;
                    objUpdateEmployee = employee.GetEmployee(objUpdateEmployee);
                    if (objUpdateEmployee.Type == Convert.ToInt32(MasterEnum.EmployeeType.OnsiteContract))
                    {
                        AllocateResourceForInternalProject();
                    }
                    else
                    {
                        SendApprovalToFinance();
                    }
                }
                else
                {
                    SendApprovalToFinance();
                }
                if (Session[SessionNames.InternalResource] != null)
                {
                    Session.Remove(SessionNames.InternalResource);

                }

                //Umesh: Issue Id:54324 - 'To check whether able to allocate internal resource from Pending Allocation' starts
                if (Session[SessionNames.MRFDetail] != null)
                {
                    Session.Remove(SessionNames.MRFDetail);
                }
                //Umesh: Issue Id:54324 - 'To check whether able to allocate internal resource from Pending Allocation' ends

                Response.Redirect(CommonConstants.Page_MrfPendingAllocation, false);
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                CLASS_NAME_VIEWMRF, "AllocateResource", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }

    }

    private void SendApprovalToFinance()
    {
        try
        {
            #region Code For New mail fuctionality

            Rave.HR.BusinessLayer.MRF.MRFDetail objBLMRF = new Rave.HR.BusinessLayer.MRF.MRFDetail();
            mrfDetail.Role = hidrole.Value;
            mrfDetail.DepartmentName = hidDepartment.Value;
            //Jignyasa Issue id : 42400,42315
            if (Session[SessionNames.CLIENT_NAME] != null)
            {
                mrfDetail.ClientName = Session[SessionNames.CLIENT_NAME].ToString();
            }
            else if (mrfDetail.DepartmentName == "Projects")
            {
                BusinessEntities.RaveHRCollection raveCollection = new BusinessEntities.RaveHRCollection();
                Rave.HR.BusinessLayer.MRF.MRFDetail mrfClientName = new Rave.HR.BusinessLayer.MRF.MRFDetail();
                raveHRCollection = mrfClientName.GetClientNameFromProjectID(mrfDetail.ProjectId);
                if (raveHRCollection != null)
                {
                    if (raveHRCollection.Count > 0)
                    {
                        mrfDetail.ClientName = ((BusinessEntities.KeyValue<string>)(raveHRCollection.Item(0))).KeyName;
                    }
                }
            }
            //Jignyasa Issue id : 42400,42315
            mrfDetail.EmployeeName = hidEmployeeName.Value;
            objBLMRF.SendMailToFinanceForInternalResourceAllocation(mrfDetail);

            #endregion Code For New mail fuctionality
            Session[SessionNames.CONFIRMATION_MESSAGE] = LBL_MESSAGE_FOR_FINALCURRENTALLOCATION;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                CLASS_NAME_VIEWMRF, "AllocateResource", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void SetVisibleAllocateResourceControl(bool Flag)
    {
        pnlHeaderAllocateResource.Visible = true;
        pnlHeaderViewMRF.Visible = false;
        pnlPendingAllocation.Visible = true;
        pnlViewMRF.Visible = false;

        //To disable MRFSumaary related buttons
        //pnlViewMRF.Visible = true;
        btnRaiseMRF.Visible = false;
        btnReplaceMRF.Visible = false;
        btnEdit.Visible = false;
        btnCancel.Visible = false;
        btnSavebtn.Visible = false;
        btnDelete.Visible = false;

        //Done By Sameer For Issue Id:18395
        lblRecruiterName.Visible = false;
        ddlRecruiterName.Visible = false;
        ddlRecruiterName.Enabled = false;

        if (IsPendingAllocation)
            ddlCostCode.Enabled = IsPendingAllocation;
        else
            ddlCostCode.Enabled = Flag;
    }

    //Geting the internal Resource Allocated
    private void BindInternalResourceDetailsGrid()
    {
        if (mrfDetail.EmployeeName != null && mrfDetail.EmployeeId != 0)
        {
            pnlInternalResourceDetails.Visible = true;
            //txtBilling_ISR.Text = mrfDetail.Billing.ToString();
            //txtUtilization_ISR.Text = mrfDetail.Utilization.ToString();
            txtResourceName.Text = mrfDetail.EmployeeName;
            hidFutureEmpID.Value = Convert.ToString(mrfDetail.EmployeeId);
            //



        }
    }

    /// <summary>
    /// This is called when a Resource is allocated to an Internal Project.
    /// Thus, after execution of this function, resource is directly allocated to project, it is not 
    /// sent to finance department for approval.
    /// </summary>
    private void AllocateResourceForInternalProject()
    {
        try
        {
            mrfDetail.MRFId = Convert.ToInt32(hidMRFID.Value);
            mrfDetail.CommentReason = string.Empty;
            mrfDetail.AllocationDate = txtAllocationDate.Text;

            mrfDetail.Status = CommonConstants.MRFStatus_PendingAllocation;
            mrfDetail.EmployeeId = Convert.ToInt32(hidFutureEmpID.Value);
            //Siddhesh Arekar Issue ID : 55884 Closure Type
            mrfDetail.TypeOfAllocation = Convert.ToInt32(typeOfAllocation.KeyName);
            mrfDetail.TypeOfClosure = Convert.ToInt32(ddlClosureType.SelectedValue);
            //Siddhesh Arekar Issue ID : 55884 Closure Type
            mrfDetail.EmployeeName = hidEmployeeName.Value;


            //Siddharth: 23-02-2015
            //Check status of B_ClientName variable. Set it to True if client Name contains NIS or Northgate. 
            //Set it to False Otherwise

            bool B_ClientName = false;

            // venkatesh  : Issue 47525 : 4/12/2013 : Starts 
            // Desc : Client Name in Mail Allocation "

            //  if (Session[SessionNames.CLIENT_NAME] != null)
            //{
            //    mrfDetail.ClientName = Session[SessionNames.CLIENT_NAME].ToString();
            //}
            //else
            if (mrfDetail.DepartmentName == "Projects")
            {
                try
                {
                    BusinessEntities.RaveHRCollection raveCollection = new BusinessEntities.RaveHRCollection();
                    Rave.HR.BusinessLayer.MRF.MRFDetail mrfClientName = new Rave.HR.BusinessLayer.MRF.MRFDetail();
                    raveHRCollection = mrfClientName.GetClientNameFromProjectID(mrfDetail.ProjectId);
                    if (raveHRCollection != null)
                    {
                        if (raveHRCollection.Count > 0)
                        {
                            mrfDetail.ClientName = ((BusinessEntities.KeyValue<string>)(raveHRCollection.Item(0))).KeyName;
                        }
                    }
                }
                catch (Exception ex)
                {
                    mrfDetail.ClientName = "";
                }
            }
            else
            {
                mrfDetail.ClientName = "";
            }
            // venkatesh  : Issue 47525 : 4/12/2013 : End

            Rave.HR.BusinessLayer.MRF.MRFDetail mrfDetailsObj = new Rave.HR.BusinessLayer.MRF.MRFDetail();

            raveHRCollection = new BusinessEntities.RaveHRCollection();
            raveHRCollection = mrfDetailsObj.GetEmployeeByMRFID(mrfDetail);

            foreach (BusinessEntities.Employee objList in raveHRCollection)
            {
                mrfDetail.EmployeeName = (objList.FirstName + " " + objList.LastName);
                mrfDetail.EmployeeId = objList.EMPId;
                mrfDetail.EmployeeTypeId = objList.Type;

                //33243-Subhra-03022012- start
                mrfDetail.EmployeeJoiningDate = objList.JoiningDate;
                //33243-Subhra- end 
            }
            if (ddlCostCode.SelectedValue != CommonConstants.SELECT && !String.IsNullOrEmpty(ddlCostCode.SelectedValue))
                mrfDetail.CostCodeId = ddlCostCode.SelectedValue.CastToInt32();

            if (divOverride.Visible == true)
                mrfDetail.IsOverride = chkOverride.Checked;
            //If EmployeeTypeId is zero then get type of employee table.
            if (mrfDetail.EmployeeTypeId == 0)
                mrfDetail.EmployeeTypeId = mrfDetailsObj.GetEmployeeTypeId(mrfDetail.EmployeeId);

            if (mrfDetail.BillingDate > Convert.ToDateTime(mrfDetail.AllocationDate))
            {
                mrfDetail.Billing = 0;
            }
            // Rajan Kumar : Issue 46252: 10/02/2014 : Starts                        			 
            // Desc : In MRF history need to implemented in all cases in RMS.
            //Pass Email to know who is going to modified the data            
            AuthorizationManager authoriseduser = new AuthorizationManager();
            mrfDetail.LoggedInUserEmail = authoriseduser.getLoggedInUserEmailId();
            // Rajan Kumar : Issue 46252: 10/02/2014 : END

            //New function added to associate an employee to a particular MRF
            //I didn't add it in SetReasonOfApproveMRF() function because didn't want to
            //break the logic of SetReasonOfApproveMRF() which further uses the logic of BL and DL.
            Rave.HR.BusinessLayer.MRF.MRFDetail.SetMRFStatus(mrfDetail);

            mrfDetail.Status = Common.CommonConstants.MRFStatus_Closed;

            //Afterwards set the status of mrf as closed and allocate the resource 
            SetReasonOfApproveMRF();

            if (mrfDetail.ClientName.ToUpper().Contains("NPS") || mrfDetail.ClientName.ToUpper().Contains("NORTHGATE"))
                B_ClientName = true;
            else
                B_ClientName = false;
            //Send the mail after DB process.
            ApprovalMail(B_ClientName);

            //Set the Confirmation message.
            Session[SessionNames.CONFIRMATION_MESSAGE] = CommonConstants.MSG_APPROVAL_OF_FINANCE;

            //Redirect it to Pending Allocation Page.
            Response.Redirect(CommonConstants.Page_MrfPendingAllocation, false);
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                CLASS_NAME_VIEWMRF, "AllocateResourceForInternalProject", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    private void SetReasonOfApproveMRF()
    {
        try
        {
            // Rajan Kumar : Issue 48183 : 24/01/2014 : Starts                        			 
            // Desc : When employee(PM or AVP) assigned or released from project then project level PM access on project should get assigned or removed.
            // Added AVP flag in if condition 
            if ((mrfDetail.Role != CommonConstants.APM) && (mrfDetail.Role != CommonConstants.PM) && (mrfDetail.Role != CommonConstants.SPM) && (mrfDetail.Role != CommonConstants.AVP) && (mrfDetail.Role != CommonConstants.PRM))
            {
                //Updated the Status of MRF and REason
                if (Convert.ToBoolean(Rave.HR.BusinessLayer.MRF.MRFDetail.SetMRFApproveRejectReason(mrfDetail)))
                {
                    return;
                }
            }
            else
            {
                //Updated the Status of MRF and REason
                if (Convert.ToBoolean(Rave.HR.BusinessLayer.MRF.MRFDetail.SetMRFApproveRejectReasonForPM(mrfDetail)))
                {
                    return;
                }

            }
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                CLASS_NAME_VIEWMRF, "SetReasonOfApproveMRF", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Details of mail to be sent after approval.
    /// </summary>
    private void ApprovalMail(bool B_ClientName)
    {
        try
        {
            BusinessEntities.MRFDetail mailDetail = new BusinessEntities.MRFDetail();
            Rave.HR.BusinessLayer.MRF.MRFDetail mrfSummaryBL = new Rave.HR.BusinessLayer.MRF.MRFDetail();
            mailDetail = mrfSummaryBL.GetMailingDetails(mrfDetail);
            EmployeeEmailId = mailDetail.EmailId;
            RaisedByEmailId = mailDetail.RaisedBy;
            int[] IDs;

            //26569-Ambar-Start-04072011
            if (mrfDetail.FunctionalManager == null || mrfDetail.FunctionalManager == "" || mrfDetail.FunctionalManager == string.Empty)
                mrfDetail.FunctionalManager = mailDetail.FunctionalManager;
            //26569-Ambar-End

            #region Logic for Reporting To's

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

            #endregion Logic for Reporting To's


            #region Code For New MailFunctionality

            Rave.HR.BusinessLayer.MRF.MRFDetail objBLMRF = new Rave.HR.BusinessLayer.MRF.MRFDetail();
            objBLMRF.SendMailToWhizibleForInternalResourceAllocation(mrfDetail, mailDetail, B_ClientName);

            #endregion Code For New MailFunctionality
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                CLASS_NAME_VIEWMRF, "ApprovalMail", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }

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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_VIEWMRF, "getMailID", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    #endregion

    #endregion

    #region Function



    /// <summary>
    /// Function will GetControlValues
    /// </summary>
    /// <returns></returns>
    private BusinessEntities.MRFDetail GetControlValuesForReplaceMRF()
    {
        BusinessEntities.MRFDetail mrfDetailobj = new BusinessEntities.MRFDetail();
        AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
        try
        {
            mrfDetailobj.MRFId = Convert.ToInt32(hidMRFID.Value);
            if (hidResourcePlanDurationId.Value != "")
                mrfDetailobj.ResourcePlanDurationId = Convert.ToInt32(hidResourcePlanDurationId.Value);
            string UserMailId = objRaveHRAuthorizationManager.getLoggedInUserEmailId();
            mrfDetailobj.MustToHaveSkills = txtMustHaveSkills.Text.Trim().ToString();
            mrfDetailobj.GoodToHaveSkills = txtGoodSkills.Text.Trim().ToString();
            mrfDetailobj.Tools = txtTools.Text.Trim().ToString();
            if (ddlSkillsCategory.SelectedItem.Text != Common.CommonConstants.SELECT)
            {
                mrfDetailobj.SkillCategoryId = Convert.ToInt32(ddlSkillsCategory.SelectedItem.Value);
            }
            else
            {
                mrfDetailobj.SkillCategoryId = 0;
            }
            string Experience = txtExperince.Text.Trim().ToString() + "-" + txtExperince1.Text.Trim().ToString();
            if (Experience == "")
            {
                mrfDetailobj.Experience = Convert.ToDecimal(0);
            }
            else
            {
                mrfDetailobj.ExperienceString = Experience;
            }
            mrfDetailobj.Qualification = txtHeighestQualification.Text.Trim().ToString();
            mrfDetailobj.SoftSkills = txtSoftSkills.Text.Trim().ToString();
            mrfDetailobj.Utilization = Convert.ToInt32(txtUtilijation.Text.Trim().ToString());
            mrfDetailobj.Billing = Convert.ToInt32(txtBilling.Text.Trim().ToString());
            string TargetCTC = txtTargetCTC.Text.Trim().ToString() + "-" + txtTargetCTC1.Text.Trim().ToString();
            if (TargetCTC == "")
            {
                mrfDetailobj.MRFCTC = Convert.ToDecimal(0);
            }
            else
            {
                mrfDetailobj.MRFCTCString = TargetCTC;
            }
            mrfDetailobj.Remarks = txtRemarks.Text.Trim().ToString();
            mrfDetailobj.ReportingToId = hidResponsiblePersonName.Value.ToString();
            mrfDetailobj.ResourceResponsibility = txtResourceResponsibility.Text.Trim().ToString();
            mrfDetailobj.LoggedInUserEmail = UserMailId;
            mrfDetailobj.MRfType = Convert.ToInt32(ddlMRFType.SelectedItem.Value);
            mrfDetailobj.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedItem.Value);
            mrfDetailobj.DepartmentName = ddlDepartment.SelectedItem.Text;
            if (mrfDetailobj.DepartmentId == 1)
            {
                mrfDetailobj.ProjectId = Convert.ToInt32(ddlProjectName.SelectedItem.Value);
                mrfDetailobj.ProjectName = ddlProjectName.SelectedItem.Text;
                mrfDetailobj.Domain = mRFDetail.GetDomainName(mrfDetailobj.ProjectId);
                mrfDetailobj.ProjectDescription = txtProjectDescription.Text.Trim();
                mrfDetailobj.ResourcePlanId = Convert.ToInt32(ddlResourcePlanCode.SelectedItem.Value);
                mrfDetailobj.RPCode = ddlResourcePlanCode.SelectedItem.Text;
            }
            else
            {
                mrfDetailobj.ProjectId = 0;
                mrfDetailobj.ProjectName = "";
                mrfDetailobj.Domain = "";
                mrfDetailobj.ProjectDescription = "";
                mrfDetailobj.ResourcePlanId = 0;
            }
            string[] rolearr = ddlRole.SelectedItem.Text.Split(SPILITER_DASH);

            mrfDetailobj.RoleName = ddlRole.SelectedItem.Text;//rolearr[0].ToString();
            mrfDetailobj.Role = rolearr[1].ToString();

            mrfDetailobj.RoleId = Convert.ToInt32(ddlRole.SelectedItem.Value);
            mrfDetailobj.DateOfRequisition = Convert.ToDateTime(txtDateOfRequisition.Text.Trim());
            mrfDetailobj.NoOfResourceRequired = 1;
            mrfDetailobj.ResourceOnBoard = Convert.ToDateTime(txtRequiredFrom.Text);
            if (!string.IsNullOrEmpty(txtRequiredTill.Text))
            {
                mrfDetailobj.ResourceReleased = Convert.ToDateTime(txtRequiredTill.Text);
            }
            if (txtReason.Text != null)
                mrfDetailobj.CommentReason = txtReason.Text.ToString().Trim();

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_VIEWMRF, "GetControlValues", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }

        return mrfDetailobj;
    }

    /// <summary>
    /// Fucntion will Set the Index of Page
    /// </summary>
    private void SetMRFIndex()
    {
        Hashtable htnew = new Hashtable();

        if (Request.QueryString[INDEX] != null)
        {
            int countIndex = Convert.ToInt32(DecryptQueryString(INDEX));

            if (Session[SessionNames.MRFVIEWINDEX] != null)
            {
                //27633 
                //htnew = (Hashtable)Session[SessionNames.MRFVIEWINDEX]; 
                htnew = (Hashtable)Session[SessionNames.MRFPreviousHashTable];

            }
            //27633

            // 30315-Ambar-Start
            // IF Session[SessionNames.CURRENT_PAGE_INDEX_MRF] is not initiated then Initiate it here
            if (Session[SessionNames.CURRENT_PAGE_INDEX_MRF] == null)
            {
                Session[SessionNames.CURRENT_PAGE_INDEX_MRF] = 1;
            }
            // 30315-Ambar-End

            MRFCURRENTCOUNT = ((Convert.ToInt16(Session[SessionNames.CURRENT_PAGE_INDEX_MRF].ToString()) - 1) * 10) + countIndex;
            MRFPREVIOUSCOUNT = MRFCURRENTCOUNT - 1;
            // 29621-Ambar-Start-30082011
            if (htnew == null)
                MRFNEXTCOUNT = (((Convert.ToInt16(Session[SessionNames.CURRENT_PAGE_INDEX_MRF].ToString()) - 1) * 10) + countIndex);
            else
                MRFNEXTCOUNT = (htnew.Keys.Count - 2) - (((Convert.ToInt16(Session[SessionNames.CURRENT_PAGE_INDEX_MRF].ToString()) - 1) * 10) + countIndex);
            // 29621-Ambar-End-30082011

            if (htnew == null || htnew.Keys.Count == 1 || Request.QueryString[CommonConstants._isAccessUrl] != null) // 29621-Ambar-30082011
            {
                btnNext.Visible = false;
                btnPrevious.Visible = false;
            }
            else if (MRFPREVIOUSCOUNT == -1)
            {
                btnNext.Visible = true;
                btnPrevious.Visible = false;
            }
            else if (MRFNEXTCOUNT == -1)
            {
                btnNext.Visible = false;
                btnPrevious.Visible = true;
            }
        }
    }

    /// <summary>
    /// Function will Call to FIll MRFDetail
    /// </summary>
    private void FillMRFDetail()
    {
        try
        {
            if (Session[SessionNames.MRFDetail] != null)
                mrfDetail = (MRFDetail)Session[SessionNames.MRFDetail];

            if (mrfDetail.MRFId != 0)
            {
                hidMRFID.Value = Convert.ToString(mrfDetail.MRFId);
            }
            else
            {
                if (Request.QueryString[MRFID] != null)
                {
                    hidMRFID.Value = DecryptQueryString(MRFID).ToString();
                }
            }
            //hidMRFID.Value = "87";
            CopyMRFDetail(Convert.ToInt32(hidMRFID.Value));
            ControlEnable(false);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                CLASS_NAME_VIEWMRF, "FillMRFDetail", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// Function will Populate MRF Details based on Passed MRFId
    /// </summary>
    /// <param name="MRFId"></param>
    private void CopyMRFDetail(int MRFId)
    {
        try
        {
            raveHRCollectionCopy = mRFDetail.CopyMRFBL(MRFId);

            if (raveHRCollectionCopy != null)
            {
                for (int i = 0; i < raveHRCollectionCopy.Count; i++)
                {
                    entitymRFDetail = (BusinessEntities.MRFDetail)raveHRCollectionCopy.Item(i);
                    SetControlValues(entitymRFDetail);
                }
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                CLASS_NAME_VIEWMRF, "CopyMRFDetail", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Function will Set Control Values at View Mode
    /// </summary>
    /// <param name="mrfDetailobj">The MRF detailobj.</param>
    private void SetControlValues(BusinessEntities.MRFDetail mrfDetailobj)
    {
        try
        {
            // 35093-Ambar-Start-04072012
            // Get the role of current logged in user and make billing date textbox blank.
            AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
            arrRolesForUser = objRaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());
            txtBillingDate.Text = "";

            //Rakesh : Actual vs Budget 07/06/2016 Begin  

            BindCostCode(mrfDetailobj.ProjectId);

            if (mrfDetailobj.CostCodeId != 0)
                ddlCostCode.SelectedValue = mrfDetailobj.CostCodeId.ToString();

            //Rakesh : Actual vs Budget 07/06/2016 End


            // 35093-Ambar-End

            //Function to fill MRF Type DropDowns
            FillMRFTypeDll();

            //Fucntion to FIll Department DropDown
            FillDepartmentDropDown();

            //Function to fill Skill Category DropDown
            BindSkillCategorydLL();

            txtMRFCode.Text = mrfDetailobj.MRFCode;

            //Assign RecruitmentManager to MRFCode
            FillRecruitmentManagerDropDown();

            //Fill MRF Status
            FillStatusDropDown("Page_Load");
            FillClosureTypeDropDown();
            Rave.HR.BusinessLayer.MRF.MRFDetail mrfDetailsBussObj = new Rave.HR.BusinessLayer.MRF.MRFDetail();
            string resourceName = mrfDetailsBussObj.GetCandidateByMRFID(mrfDetailobj);
            lblResourceName.Visible = true;
            txtResourceJoined.Visible = true;
            txtResourceJoined.Text = resourceName;
            txtResourceJoined.Enabled = false;

            #region Added by Gaurav Thakkar
            /*If the MRF is replaced one than show the MRF details i.e the NEW MRF code which has replaced 
              the old one*/
            if (mrfDetailobj.NewMRFCode != "")
            {
                lblNewMRFCode.Visible = true;
                txtNewMRFCode.Visible = true;
                txtNewMRFCode.Text = mrfDetailobj.NewMRFCode;
                txtNewMRFCode.Enabled = false;
            }
            else
            {
                lblNewMRFCode.Visible = false;
                txtNewMRFCode.Visible = false;
            }

            #endregion Added by Gaurav Thakkar

            //Check MRF Type dropdown contains mrfType or Not
            if (ddlMRFType.Items.FindByValue(mrfDetailobj.MRfType.ToString()) != null)
            {
                ddlMRFType.SelectedValue = Convert.ToString(mrfDetailobj.MRfType);
            }

            //Assign Department Selected Value
            ddlDepartment.SelectedValue = Convert.ToString(mrfDetailobj.DepartmentId);

            //Function will show role as per selected Department wise.
            DepartmentWiseRoleDispaly(Convert.ToInt32(ddlDepartment.SelectedItem.Value));

            //Fill Project Name DropDown
            FillProjectNameDropDown();


            //Fill Purpose dropdown
            GetPurposeDetails();
            if (mrfDetailobj.StatusId > 0)
            {
                ddlMRFStatus.SelectedValue = mrfDetailobj.StatusId.ToString();
                previousMRFStatus.Value = ddlMRFStatus.SelectedValue;
            }
            ddlMRFStatus.Enabled = false;

            if (mrfDetailobj.StatusId == Convert.ToInt32(MasterEnum.MRFStatus.Abort)
                || mrfDetailobj.StatusId == Convert.ToInt32(MasterEnum.MRFStatus.Closed))
            {
                lblClosedDate.Visible = true;
                txtClosedDate.Visible = true;
                if (mrfDetailobj.AbortedOrClosedDate != "")
                {
                    txtClosedDate.Text = mrfDetailobj.AbortedOrClosedDate;
                }
                else
                {
                    txtClosedDate.Text = " ";
                }
                if (mrfDetailobj.StatusId == Convert.ToInt32(MasterEnum.MRFStatus.Abort))
                {
                    lblClosedDate.Text = "Aborted Date";
                }
                else
                {
                    lblClosedDate.Text = "Closed Date";
                }
            }
            else
            {
                lblClosedDate.Visible = false;
                txtClosedDate.Visible = false;
            }

            uclExpectedClosureDate.TextBox.BorderWidth = 1;
            uclExpectedClosureDate.TextBox.BorderColor = System.Drawing.Color.Gray;

            txtReasonExpDate.BorderWidth = 1;
            txtReasonExpDate.BorderColor = System.Drawing.Color.Gray;

            if (Convert.ToDateTime(mrfDetailobj.ExpectedClosureDate) != DateTime.MinValue)
            {
                uclExpectedClosureDate.Text = mrfDetailobj.ExpectedClosureDate;
                hidExpectedClosedDate.Value = mrfDetailobj.ExpectedClosureDate;

                txtReasonExpDate.Text = mrfDetailobj.ReasonForExtendingExpectedClosureDate;
                hidReasonExpectedCloserDate.Value = mrfDetailobj.ReasonForExtendingExpectedClosureDate;
                //storing the value for doing validation of Expected Closed Date.
                hidRequestForRecruitment.Value = Convert.ToString(mrfDetailobj.RequestForRecruitment);
            }
            else
            {
                uclExpectedClosureDate.Text = "";
            }

            //Mofied By Rajesh: issueId: 20083. Showing the Expected ClosureDate
            if (mrfDetailobj.StatusId == Convert.ToInt32(MasterEnum.MRFStatus.PendingExternalAllocation))
            {
                if (ROLE == AuthorizationManagerConstants.ROLERPM || ROLE == AuthorizationManagerConstants.ROLERECRUITMENT)
                {
                    lblExpectedClosureDate.Visible = true;
                    lblExpectedClosureDatemandatory.Visible = true;
                    uclExpectedClosureDate.Visible = true;
                    uclExpectedClosureDate.IsEnable = false;
                    lblRForExtendingEClDates.Visible = true;
                    lblRForExtendingEClDatesmandatory.Visible = true;
                    txtReasonExpDate.Visible = true;
                    txtReasonExpDate.Enabled = false;


                }
                //else if (ROLE == AuthorizationManagerConstants.ROLERECRUITMENT)
                //{
                //    lblExpectedClosureDate.Visible = true;
                //    lblExpectedClosureDatemandatory.Visible = true;
                //    uclExpectedClosureDate.Visible = true;
                //    uclExpectedClosureDate.IsEnable = false;
                //    lblRForExtendingEClDates.Visible = true;
                //    lblRForExtendingEClDatesmandatory.Visible = true;
                //    txtReasonExpDate.Visible = true;
                //    txtReasonExpDate.Enabled = false;

              //}
                else
                {
                    lblExpectedClosureDate.Visible = false;
                    lblExpectedClosureDatemandatory.Visible = false;
                    uclExpectedClosureDate.Visible = false;
                    lblRForExtendingEClDates.Visible = false;
                    lblRForExtendingEClDatesmandatory.Visible = false;
                    txtReasonExpDate.Visible = false;
                }
            }
            else
            {
                lblExpectedClosureDate.Visible = false;
                lblExpectedClosureDatemandatory.Visible = false;
                uclExpectedClosureDate.Visible = false;
                lblRForExtendingEClDates.Visible = false;
                lblRForExtendingEClDatesmandatory.Visible = false;
                txtReasonExpDate.Visible = false;
            }

            txtBillingDate.IsEnable = false;
            //Modified By Rajesh; Issue Id 20516. enable/disable of Billing Date
            if (mrfDetailobj.Billing > 0)
            {
                if (Convert.ToInt32(ddlDepartment.SelectedItem.Value) == Convert.ToInt32(Common.MasterEnum.Departments.Projects))
                {
                    // 35093-Ambar-Added PM role condition to following.
                    if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM) || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERECRUITMENT) || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEPM))
                    {
                        if (mrfDetailobj.BillingDate != DateTime.MinValue)
                        {
                            // 31103-Subhra-Start-14122011
                            // Commented following and Changed format of blling date
                            // txtBillingDate.Text = mrfDetailobj.BillingDate.ToShortDateString();
                            txtBillingDate.Text = mrfDetailobj.BillingDate.ToString(DATEFORMAT);
                            // 31103-Subhra-End-14122011
                        }
                        else
                        {
                            txtBillingDate.Text = " ";
                        }
                    }
                    else
                    {
                        txtBillingDate.Visible = true;
                        lblBillingDate.Visible = true;
                        lblBillingDatemandatory.Visible = true;
                    }
                }
                else
                {
                    txtBillingDate.Visible = false;
                    lblBillingDate.Visible = false;
                    lblBillingDatemandatory.Visible = false;
                }
            }
            else
            {
                txtBillingDate.Visible = false;
                lblBillingDate.Visible = false;
                lblBillingDatemandatory.Visible = false;
            }


            //Check Project id 0 or not
            if (mrfDetailobj.ProjectId != 0)
            {
                if (ddlProjectName.Items.Contains(new ListItem(mrfDetailobj.ProjectName, Convert.ToString(mrfDetailobj.ProjectId))) == true)
                {
                    ddlProjectName.SelectedValue = Convert.ToString(mrfDetailobj.ProjectId);
                    ddlProjectName.Enabled = true;
                    hidProjectEndDate.Value = mrfDetailobj.ProjEndDate.ToShortDateString();
                    hidProjectStartDate.Value = mrfDetailobj.ProjStartDate.ToShortDateString();
                    ddlResourcePlanCode.Enabled = true;

                    GetProjectDescription(ddlProjectName.SelectedIndex - 1);
                    FillResourcePlanDropDown(Convert.ToInt32(ddlProjectName.SelectedValue));
                }
                else
                {
                    ddlProjectName.ClearSelection();
                    //ddlProjectName.SelectedValue = Convert.ToString(Common.CommonConstants.ZERO);
                    ddlProjectName.Items.FindByText(CommonConstants.SELECT).Selected = true;
                }

                ddlResourcePlanCode.Items.Clear();
                ddlResourcePlanCode.Items.Add(new ListItem(mrfDetailobj.RPCode, Convert.ToString(mrfDetailobj.ResourcePlanId)));
                if (ddlResourcePlanCode.Items.Contains(new ListItem("Select", "0")) == true)
                {
                    ddlResourcePlanCode.SelectedIndex = 1;
                }
                else
                {
                    ddlResourcePlanCode.SelectedIndex = 0;
                }

            }
            else
            {
                ddlProjectName.Enabled = false;
                ddlResourcePlanCode.Items.Clear();
                ddlResourcePlanCode.Enabled = false;

            }

            // venkatesh  : Issue 39795 : 21/11/2013 : Starts
            // Desc : Role Editable
            if (ddlDepartment.SelectedItem.Text.ToString().ToLower() == "projects" && (ddlMRFStatus.SelectedValue == Convert.ToInt32(MasterEnum.MRFStatus.PendingExternalAllocation).ToString() || ddlMRFStatus.SelectedValue == Convert.ToInt32(MasterEnum.MRFStatus.PendingAllocation).ToString()))
            {
                ddlRole.Items.Clear();
                ddlRole.Items.Add(new ListItem(mrfDetailobj.Role, Convert.ToString(mrfDetailobj.RoleId)));
                if (ddlRole.Items.Contains(new ListItem("Select", "0")) == true)
                {
                    ddlRole.SelectedIndex = 1;
                }
                else
                {
                    ddlRole.SelectedIndex = 0;
                }
            }
            else
                ddlRole.SelectedValue = Convert.ToString(mrfDetailobj.RoleId);
            // Desc : Role Editable


            // Assign Date of Requisition
            if (mrfDetailobj.DateOfRequisition != DateTime.MinValue)
            {
                txtDateOfRequisition.Text = mrfDetailobj.DateOfRequisition.ToString(DATEFORMAT);
            }
            else
            {
                txtDateOfRequisition.Text = DateTime.Now.ToString(DATEFORMAT);
            }

            //Assign txtRequired From TextBox
            txtRequiredFrom.Text = mrfDetailobj.ResourceOnBoard.ToString(DATEFORMAT);
            hidOldRequiredFromDate.Value = mrfDetailobj.ResourceOnBoard.ToShortDateString();

            //Assign txtRequired Till TextBox
            // Rajan Kumar : Issue 45752 : 03/01/2014 : Starts                        			 
            // Desc : When MRF is raised against department then MRF end date is not mandatory (remove validation).
            //txtRequiredTill.Text = mrfDetailobj.ResourceReleased.ToString(DATEFORMAT);
            if (!string.IsNullOrEmpty(mrfDetailobj.ResourceReleased.ToString(DATEFORMAT)) // Umesh : Issue 52816 : 03/09/2014 "Null or empty check for string" 
                && Convert.ToDateTime(mrfDetailobj.ResourceReleased.ToString(DATEFORMAT)) != new DateTime())
            {
                txtRequiredTill.Text = mrfDetailobj.ResourceReleased.ToString(DATEFORMAT);
            }
            else
            {
                txtRequiredTill.Text = string.Empty;
            }
            // Rajan Kumar : Issue 45752 : 03/01/2014: Ends 

            hidOldRequiredTillDate.Value = string.IsNullOrEmpty(mrfDetailobj.ResourceReleased.ToShortDateString()) // Umesh : Issue 52816 : 03/09/2014 "Null or empty check for string"
                                            ? string.Empty
                                            : mrfDetailobj.ResourceReleased.ToShortDateString();

            //Assign txtDomain From TextBox
            txtDomain.Text = mrfDetailobj.Domain;

            //Assign txtMustHaveSkills From TextBox
            txtMustHaveSkills.Text = mrfDetailobj.MustToHaveSkills;

            //Assign txtGoodSkills From TextBox
            txtGoodSkills.Text = mrfDetailobj.GoodToHaveSkills;

            //Assign txtTools From TextBox
            txtTools.Text = mrfDetailobj.Tools;

            if (ddlSkillsCategory.Items.Contains(new ListItem(mrfDetailobj.SkillCategoryName, Convert.ToString(mrfDetailobj.SkillCategoryId))) == true)
            {
                ddlSkillsCategory.SelectedValue = Convert.ToString(mrfDetailobj.SkillCategoryId);
            }

            //Assign txtDomain From TextBox
            txtDomain.Text = mrfDetailobj.Domain;

            string[] Exparr = Convert.ToString(mrfDetailobj.ExperienceString).Split(SPILITER_DASH);

            if (Exparr.Length == 2)
            {
                //Assign txtExperince From TextBox
                txtExperince.Text = Exparr[0].ToString();
                txtExperince1.Text = Exparr[1].ToString();
            }
            else if (Exparr.Length == 1)
            {
                txtExperince.Text = Exparr[0].ToString();
            }
            //Assign txtHeighestQualification From TextBox
            txtHeighestQualification.Text = mrfDetailobj.Qualification;

            //Assign txtSoftSkills From TextBox
            txtSoftSkills.Text = mrfDetailobj.SoftSkills;

            //Assign txtUtilijation From TextBox
            txtUtilijation.Text = Convert.ToString(mrfDetailobj.Utilization);

            //Assign txtBilling From TextBox
            txtBilling.Text = Convert.ToString(mrfDetailobj.Billing);

            string[] CTCarr = Convert.ToString(mrfDetailobj.MRFCTCString).Split(SPILITER_DASH);

            if (CTCarr.Length == 2)
            {
                //Assign txtTargetCTC From TextBox
                txtTargetCTC.Text = CTCarr[0].ToString();
                txtTargetCTC1.Text = CTCarr[1].ToString();
            }
            else if (CTCarr.Length == 1)
            {
                txtTargetCTC.Text = CTCarr[0].ToString();
            }

            //Assign txtRemarks From TextBox
            txtRemarks.Text = mrfDetailobj.Remarks;

            //Assign txtResponsiblePerson From TextBox
            txtResponsiblePerson.Text = mrfDetailobj.ReportingToEmployee;

            //Ishwar Patil 22/04/2015 Start
            TxtSkills.Text = mrfDetailobj.MandatorySkills;
            hidSkills.Value = mrfDetailobj.MandatorySkillsID;
            //Ishwar Patil 22/04/2015 End

            //Assign hidResponsiblePersonName
            hidResponsiblePersonName.Value = mrfDetailobj.ReportingToId;

            hidReportingToName.Value = mrfDetailobj.ReportingToEmployee;

            //Assign txtResourceResponsibility
            txtResourceResponsibility.Text = mrfDetailobj.ResourceResponsibility;

            hidContractTypeID.Value = mrfDetailobj.ContractTypeID.ToString();

            if (mrfDetailobj.DepartmentId == (int)MasterEnum.Departments.RaveConsultant_India
                || mrfDetailobj.DepartmentId == (int)MasterEnum.Departments.RaveConsultant_UK
                || mrfDetailobj.DepartmentId == (int)MasterEnum.Departments.RaveConsultant_USA
                || mrfDetailobj.DepartmentId == (int)MasterEnum.Departments.PreSales
                || mrfDetailobj.DepartmentId == (int)MasterEnum.Departments.RaveForecastedProjects)
            {
                if (mrfDetailobj.ClientName != "")
                {
                    lblClientName.Visible = true;
                    txtClientName.Visible = true;
                    txtClientName.Text = mrfDetailobj.ClientName;
                    mandatorymarkClientName.Visible = true;
                }
                else
                {
                    lblClientName.Visible = false;
                    txtClientName.Visible = false;
                    mandatorymarkClientName.Visible = false;
                }
            }
            else
            {
                lblClientName.Visible = false;
                txtClientName.Visible = false;
                mandatorymarkClientName.Visible = false;
            }

            if (mrfDetailobj.DepartmentId == (int)MasterEnum.Departments.RaveConsultant_India
              || mrfDetailobj.DepartmentId == (int)MasterEnum.Departments.RaveConsultant_UK
              || mrfDetailobj.DepartmentId == (int)MasterEnum.Departments.RaveConsultant_USA)
            {
                lblSOWNo.Visible = true;
                txtSOWNo.Visible = true;
                lblSowStartDt.Visible = true;
                DtPckerSOWStartDt.Visible = true;
                lblSOWEndDt.Visible = true;
                DtPckSOWEndDt.Visible = true;
            }

            else
            {
                lblSOWNo.Visible = false;
                txtSOWNo.Visible = false;
                lblSowStartDt.Visible = false;
                DtPckerSOWStartDt.Visible = false;
                lblSOWEndDt.Visible = false;
                DtPckSOWEndDt.Visible = false;
            }


            //Assign MRFSTATUS
            MRFSTATUS = mrfDetailobj.StatusId;

            //Function will Check MRF Status
            CheckMRFStatus(mrfDetailobj.StatusId, mrfDetailobj.CommentReason);

            hidprojectName.Value = ddlProjectName.SelectedItem.Text;
            // 57877-Venkatesh-  29042016 : Start 
            // Add sai email if while raising headcount for nis projects
            hidprojectId.Value = ddlProjectName.SelectedValue;
            // 57877-Venkatesh-  29042016 : End
            hidrole.Value = ddlRole.SelectedItem.Text;
            hidExp.Value = txtExperince.Text;
            hidCTC.Value = txtTargetCTC.Text;
            hidDepartment.Value = ddlDepartment.SelectedItem.Text;
            if (mrfDetailobj.ResourcePlanDurationId != 0)
                hidResourcePlanDurationId.Value = mrfDetailobj.ResourcePlanDurationId.ToString();
            else
                hidResourcePlanDurationId.Value = "0";
            //Session[SessionNames.MRFDetail] = mrfDetailobj;

            if (!string.IsNullOrEmpty(mrfDetailobj.MRFColorCode))
            {
                ddlColorCode.SelectedValue = mrfDetailobj.MRFColorCode;
            }
            else
            {
                ddlColorCode.SelectedValue = CommonConstants.ColorCodeAmber;
            }

            if (!string.IsNullOrEmpty(mrfDetailobj.RecruitersId))
            {
                if (ddlRecruiterName.Items.FindByValue(mrfDetailobj.RecruitersId) != null)
                {
                    ddlRecruiterName.SelectedValue = mrfDetailobj.RecruitersId;
                    hidRecruiterId.Value = mrfDetailobj.RecruitersId;
                }
            }

            //if (DecryptQueryString(PAGETYPE) == PAGETYPE_PENDING_ALLOCATION)
            //{
            //    txtPurpose.Visible = true;
            //    ddlPurpose.Visible = false;
            //    if (!string.IsNullOrEmpty(mrfDetailobj.MRFPurpose))
            //        txtPurpose.Text = mrfDetailobj.MRFPurpose.Trim();
            //    lblmandatorymark.Visible = false;
            //}
            //else
            //{
            txtPurposeDescription.Visible = false;
            ddlPurpose.Visible = true;

            if (mrfDetailobj.MRFPurposeId != 0)
                ddlPurpose.SelectedValue = Convert.ToInt32(mrfDetailobj.MRFPurposeId).ToString();
            else
                ddlPurpose.SelectedValue = CommonConstants.SELECT;


            if (ddlPurpose.SelectedValue == CommonConstants.SELECT)
            {
                mrfDetailobj.MRFPurposeId = 0;
            }
            else
            {
                mrfDetailobj.MRFPurposeId = Convert.ToInt32(ddlPurpose.SelectedValue);
            }

            ddlPurpose_SelectedIndexChanged(null, null);


            if (!string.IsNullOrEmpty(mrfDetailobj.MRFPurposeDescription))
            {

                if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.HiringforDepartment))
                    ddlPuposeDepartment.Items.FindByText(mrfDetailobj.MRFPurposeDescription).Selected = true;
                else
                    txtPurposeDescription.Text = mrfDetailobj.MRFPurposeDescription;
            }
            //}
            //vandnaa
            hidTypeOfAllocation.Value = mrfDetailobj.TypeOfAllocationName;
            hidFutureEmpID.Value = Convert.ToInt32(mrfDetailobj.FutureEmpID).ToString();
            hidMoveMrfRoleID.Value = mrfDetailobj.RoleId.ToString();
            hidMoveMrfRoleName.Value = ddlRole.SelectedItem.Text;
            hidNoOfResources.Value = "";
            //vandnaa

            txtSOWNo.Text = mrfDetailobj.SOWNo;


            if (mrfDetailobj.SOWStartDate != DateTime.MinValue)
            {


                DtPckerSOWStartDt.Text = mrfDetailobj.SOWStartDate.ToString(DATEFORMAT);
            }
            else
            {
                DtPckerSOWStartDt.Text = " ";
            }

            if (mrfDetailobj.SOWEndDate != DateTime.MinValue)
            {
                DtPckSOWEndDt.Text = mrfDetailobj.SOWEndDate.ToString(DATEFORMAT);
            }
            else
            {
                DtPckSOWEndDt.Text = " ";
            }

            //Umesh: Issue 'Internal Allocation is not working by modal popup changes' Starts
            if (mrfDetailobj.EmployeeId == 0 || string.IsNullOrEmpty(mrfDetailobj.EmployeeName))
            {
                MRFDetail mrfDetail = (MRFDetail)Session[SessionNames.MRFDetail];
                //NISRMS Issue : 54363 Ishwar 18122014 Start
                if (mrfDetail != null)
                {
                    //NISRMS Issue : 54363 Ishwar 18122014 End
                    mrfDetailobj.EmployeeId = mrfDetail.EmployeeId;
                    mrfDetailobj.EmployeeName = mrfDetail.EmployeeName;
                    mrfDetailobj.EmployeeJoiningDate = mrfDetail.EmployeeJoiningDate;
                }
            }
            //Umesh: Issue 'Internal Allocation is not working by modal popup changes' Ends

            Session[SessionNames.MRFDetail] = mrfDetailobj;

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                CLASS_NAME_VIEWMRF, "SetControlValues", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="MRFStatusID"></param>
    /// <param name="Reason"></param>
    private void CheckMRFStatus(int MRFStatusID, string Reason)
    {
        if (MRFStatusID == Convert.ToInt32(MasterEnum.MRFStatus.PendingAllocation))
        {
            btnDelete.Visible = true;
            btnDelete.Enabled = true;
            VIsibleEditBtn();
            // VIsibleAbortBtn();
            pnlDelete.Visible = false;
            txtMRFStatus.Value = Convert.ToString(MasterEnum.MRFStatus.PendingAllocation);
            ddlMRFStatus.SelectedValue = Convert.ToInt32(MasterEnum.MRFStatus.PendingAllocation).ToString();
        }
        if (MRFStatusID == Convert.ToInt32(MasterEnum.MRFStatus.MarketResearchCompleteAndClosed))
        {
            VIsibleEditBtn();
            // VIsibleAbortBtn();
            pnlDelete.Visible = false;
            txtMRFStatus.Value = Convert.ToString(MasterEnum.MRFStatus.MarketResearchCompleteAndClosed);
            ddlMRFStatus.SelectedValue = Convert.ToInt32(MasterEnum.MRFStatus.MarketResearchCompleteAndClosed).ToString();
        }
        else if (MRFStatusID == Convert.ToInt32(MasterEnum.MRFStatus.PendingApprovalOfFinance))
        {
            //VIsibleAbortBtn();
            btnEdit.Visible = false;
            pnlDelete.Visible = false;
            txtMRFStatus.Value = Convert.ToString(MasterEnum.MRFStatus.PendingApprovalOfFinance);
            ddlMRFStatus.SelectedValue = Convert.ToInt32(MasterEnum.MRFStatus.PendingApprovalOfFinance).ToString();
        }
        else if (MRFStatusID == Convert.ToInt32(MasterEnum.MRFStatus.PendingHeadCountApprovalOfCOO))
        {
            //VIsibleAbortBtn();
            btnEdit.Visible = false;
            pnlDelete.Visible = false;
            txtMRFStatus.Value = Convert.ToString(MasterEnum.MRFStatus.PendingHeadCountApprovalOfCOO);
            ddlMRFStatus.SelectedValue = Convert.ToInt32(MasterEnum.MRFStatus.PendingHeadCountApprovalOfCOO).ToString();
        }
        else if (MRFStatusID == Convert.ToInt32(MasterEnum.MRFStatus.PendingHeadCountApprovalOfFinance))
        {
            //VIsibleAbortBtn();
            btnEdit.Visible = false;
            pnlDelete.Visible = false;
            txtMRFStatus.Value = Convert.ToString(MasterEnum.MRFStatus.PendingHeadCountApprovalOfFinance);
            ddlMRFStatus.SelectedValue = Convert.ToInt32(MasterEnum.MRFStatus.PendingHeadCountApprovalOfFinance).ToString();
        }
        else if (MRFStatusID == Convert.ToInt32(MasterEnum.MRFStatus.PendingExpectedResourceJoin))
        {
            // VIsibleAbortBtn();
            btnEdit.Visible = false;
            pnlDelete.Visible = false;
            txtMRFStatus.Value = Convert.ToString(MasterEnum.MRFStatus.PendingExpectedResourceJoin);
            ddlMRFStatus.SelectedValue = Convert.ToInt32(MasterEnum.MRFStatus.PendingExpectedResourceJoin).ToString();
        }
        else if (MRFStatusID == Convert.ToInt32(MasterEnum.MRFStatus.PendingExternalAllocation))
        {
            btnDelete.Visible = true;
            btnDelete.Enabled = true;
            // VIsibleAbortBtn();
            VIsibleEditBtn();
            //btnEdit.Visible = false;
            pnlDelete.Visible = false;
            txtMRFStatus.Value = Convert.ToString(MasterEnum.MRFStatus.PendingExternalAllocation);
            ddlMRFStatus.SelectedValue = Convert.ToInt32(MasterEnum.MRFStatus.PendingExternalAllocation).ToString();
        }
        else if (MRFStatusID == Convert.ToInt32(MasterEnum.MRFStatus.Rejected))
        {
            btnEdit.Visible = true;
            txtMRFStatus.Value = Convert.ToString(MasterEnum.MRFStatus.Rejected);
            ddlMRFStatus.SelectedValue = Convert.ToInt32(MasterEnum.MRFStatus.Rejected).ToString();
            pnlDelete.Visible = true;
            btnDelete.Visible = true;
            lblReson.Text = "Reject Reason";
            txtReason.Text = Reason;
            txtReason.ReadOnly = true;
            txtReason.Enabled = false;
        }
        else if (MRFStatusID == Convert.ToInt32(MasterEnum.MRFStatus.Closed))
        {
            // VIsibleAbortBtn();
            // Aarohi : Issue 31838(CR) : 28/12/2011 : Start
            // Added condition to make replace button to make the ReplaceMRF button invisible for the EditMainRP page
            // btnReplaceMRF.Visible = true;
            if (DecryptQueryString(PAGETYPE) == PAGETYPE_EDITMAINRP)
            {
                btnReplaceMRF.Visible = false;
            }
            else
            {
                btnReplaceMRF.Visible = true;
            }
            //Aarohi : Issue 31838(CR) : 28/12/2011 : End
            btnEdit.Visible = false;
            pnlDelete.Visible = true;
            btnDelete.Visible = false;
            txtReason.Enabled = false;
            txtReason.ReadOnly = true;
            txtReason.Text = Reason;
            lblReson.Text = "Approve Reason";
            txtMRFStatus.Value = Convert.ToString(MasterEnum.MRFStatus.Closed);
            ddlMRFStatus.SelectedValue = Convert.ToInt32(MasterEnum.MRFStatus.Closed).ToString();
        }
        else if (MRFStatusID == Convert.ToInt32(MasterEnum.MRFStatus.ResourceJoin))
        {
            //VIsibleAbortBtn();
            btnEdit.Visible = false;
            pnlDelete.Visible = false;
            txtMRFStatus.Value = Convert.ToString(MasterEnum.MRFStatus.ResourceJoin);
            ddlMRFStatus.SelectedValue = Convert.ToInt32(MasterEnum.MRFStatus.ResourceJoin).ToString();
        }
        else if (MRFStatusID == Convert.ToInt32(MasterEnum.MRFStatus.Abort))
        {
            txtMRFStatus.Value = Convert.ToString(MasterEnum.MRFStatus.Abort);
            ddlMRFStatus.SelectedValue = Convert.ToInt32(MasterEnum.MRFStatus.Abort).ToString();

            //panel is used to show Reason Label & Textbox

            pnlDelete.Visible = true;

            if (ROLE == AuthorizationManagerConstants.ROLERPM)
            {
                btnEdit.Visible = true;
                btnDelete.Visible = false;
            }
            else if (ROLE == AuthorizationManagerConstants.ROLERECRUITMENT)
            {
                btnEdit.Visible = true;
            }
            else
            {
                btnEdit.Visible = false;
                btnDelete.Visible = false;
                pnlDelete.Visible = false;
            }

            lblReson.Text = "Abort Reason";
            txtReason.Text = Reason;
            txtReason.ReadOnly = true;
            txtReason.Enabled = false;
            previousMRFStatus.Value = ddlMRFStatus.SelectedValue;
        }

        #region Changes made by Gaurav Thakkar
        /*New status "Reploced" is been added to replace the existing MRF*/
        else if (MRFStatusID == Convert.ToInt32(MasterEnum.MRFStatus.Replaced))
        {
            btnEdit.Visible = false;
            btnDelete.Visible = false;
            txtMRFStatus.Value = Convert.ToString(MasterEnum.MRFStatus.Replaced);
            ddlMRFStatus.SelectedValue = Convert.ToInt32(MasterEnum.MRFStatus.Replaced).ToString();
        }

        #endregion Changes made by Gaurav Thakkar

        #region Changes made by Shrinivas Dalavi
        /*New status "Pending New Employee Allocation" is been added to MRF*/
        else if (MRFStatusID == Convert.ToInt32(MasterEnum.MRFStatus.PendingNewEmployeeAllocation))
        {
            VIsibleEditBtn();
            //VIsibleAbortBtn();
            pnlDelete.Visible = false;
            txtMRFStatus.Value = Convert.ToString(MasterEnum.MRFStatus.PendingNewEmployeeAllocation);
            ddlMRFStatus.SelectedValue = Convert.ToInt32(MasterEnum.MRFStatus.PendingNewEmployeeAllocation).ToString();
        }
        #endregion Changes made by Shrinivas Dalavi

    }

    /// <summary>
    /// Function will visible Edit Button
    /// </summary>
    private void VIsibleEditBtn()
    {
        if (ROLE == AuthorizationManagerConstants.ROLERPM || ROLE == AuthorizationManagerConstants.ROLEPM
            || ROLE == AuthorizationManagerConstants.ROLEGPM || ROLE == AuthorizationManagerConstants.ROLEAPM
            || ROLE == AuthorizationManagerConstants.ROLEPRESALES || ROLE == AuthorizationManagerConstants.ROLEFINANCE
            || ROLE == AuthorizationManagerConstants.ROLEFM || ROLE == AuthorizationManagerConstants.ROLECFM
            || ROLE == AuthorizationManagerConstants.ROLERECRUITMENT)
        {
            btnEdit.Visible = true;
            btnEdit.Enabled = true;
        }
        else
        {
            btnEdit.Visible = false;
            btnEdit.Enabled = false;
        }
    }

    /// <summary>
    /// Fucntion will visible Abort Button
    /// </summary>
    private void VIsibleAbortBtn()
    {
        if (ROLE == AuthorizationManagerConstants.ROLERPM)
        {
            btnDelete.Visible = true;
            btnDelete.Enabled = true;
        }
        else
        {
            btnDelete.Visible = false;
            btnDelete.Enabled = false;
        }
    }

    /// <summary>
    /// FUnction will Call to Fill Project Name DropDwon
    /// </summary>
    private void FillProjectNameDropDown()
    {
        try
        {
            AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
            string UserMailId = objRaveHRAuthorizationManager.getLoggedInUserEmailId();

            BusinessEntities.ParameterCriteria parameterCriteria = new BusinessEntities.ParameterCriteria();

            parameterCriteria.EMailID = UserMailId;
            parameterCriteria.RoleRPM = ROLE;
            //Calling Fill dropdown Business layer method to fill the dropdown
            raveHRCollection = mRFDetail.GetProjectNameRoleWiseBL(parameterCriteria);

            Session[Common.SessionNames.MRFPROJECTDETAIL_COLLECTION] = raveHRCollection;

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
                ddlResourcePlanCode.Items.Clear();
                ddlResourcePlanCode.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);


            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_VIEWMRF, "FillProjectNameDropDown", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Function will Call TO Get Project Description.
    /// </summary>
    /// <param name="index"></param>
    private void GetProjectDescription(int index)
    {
        try
        {
            BusinessEntities.RaveHRCollection raveHRCollection;
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            raveHRCollection = (BusinessEntities.RaveHRCollection)Session[Common.SessionNames.MRFPROJECTDETAIL_COLLECTION];

            if (raveHRCollection != null)
            {
                BusinessEntities.MRFDetail mRFDetail = new BusinessEntities.MRFDetail();
                mRFDetail = (BusinessEntities.MRFDetail)raveHRCollection.Item(index);
                Session[Common.SessionNames.MRFPROJECTDETAIL] = mRFDetail;
                txtProjectDescription.Text = mRFDetail.ProjectDescription;
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_VIEWMRF, "GetProjectDescription", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// FUnction will Call to FIll ResourcePlan DropDwon
    /// </summary>
    /// <param name="projectId"></param>
    private void FillResourcePlanDropDown(int projectId)
    {
        try
        {
            //Calling Fill dropdown Business layer method to fill the dropdown
            raveHRCollection = mRFDetail.GetResourcePlanProjectWiseBL(projectId);

            //Check Collection object is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSource to dropdown
                ddlResourcePlanCode.DataSource = raveHRCollection;

                //Assign DataText Field to dropdown
                ddlResourcePlanCode.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign Data Value field to dropdown
                ddlResourcePlanCode.DataValueField = CommonConstants.DDL_DataValueField;

                //Bind Dropdown
                ddlResourcePlanCode.DataBind();

                //Insert Select as a Item for Dropdown
                ddlResourcePlanCode.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_VIEWMRF, "FillResourcePlanDropDown", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Function will Call to FIll Role DropDown
    /// </summary>
    /// <param name="ResourcePlanCode"></param>
    private void FillRoleDropDown(int ResourcePlanCode)
    {
        try
        {
            //Calling Fill dropdown Business layer method to fill the dropdown
            raveHRCollection = mRFDetail.GetRoleResourcePlanWiseBL(ResourcePlanCode, Convert.ToInt32(MasterEnum.RPDurationEditionStatus.Deleted));

            //Check Collection object is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSource to dropdown
                ddlRole.DataSource = raveHRCollection;

                //Assign DataText Field to dropdown
                ddlRole.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign Data Value field to dropdown
                ddlRole.DataValueField = CommonConstants.DDL_DataValueField;

                //Bind Dropdown
                ddlRole.DataBind();

                //Insert Select as a Item for Dropdown
                ddlRole.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_VIEWMRF, "FillRoleDropDown", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// FUnction will call to FIll Department DropDwon
    /// </summary>
    private void FillDepartmentDropDown()
    {
        try
        {
            //Calling Fill dropdown Business layer method to fill the dropdown
            raveHRCollection = master.FillDepartmentDropDownBL();

            //Check Collection object is null or not
            if (raveHRCollection != null)
            {
                ////Assign DataSource to dropdown
                //ddlDepartment.DataSource = raveHRCollection;

                ////Assign DataText Field to dropdown
                //ddlDepartment.DataTextField = CommonConstants.DDL_DataTextField;

                ////Assign Data Value field to dropdown
                //ddlDepartment.DataValueField = CommonConstants.DDL_DataValueField;

                ////Bind Dropdown
                //ddlDepartment.DataBind();


                ////Insert Select as a Item for Dropdown
                //ddlDepartment.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                ddlDepartment.BindDropdown(raveHRCollection);
                ddlPuposeDepartment.BindDropdown(raveHRCollection);
                raveHRCollection.Clear();
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_VIEWMRF, "FillDepartmentDropDown", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }



    //Rakesh : Actual vs Budget 07/06/2016 Begin
    void BindCostCode(int ProjectId)
    {
        NPS_Validation objNPS_Validation = Rave.HR.BusinessLayer.MRF.MRFDetail.Is_NIS_NorthgateProject(Convert.ToInt32(ProjectId));
     
        if (objNPS_Validation.IsNPS_Project)
        {
            if (ddlCostCode.Items.Count <= 0)
            {
                trCostCode.Visible = true;
                Rave.HR.BusinessLayer.Common.Master objMaster = new Rave.HR.BusinessLayer.Common.Master();
                raveHRCollection = objMaster.FillDropDownsBL(Common.EnumsConstants.Category.CostCode.CastToInt32());
                ddlCostCode.BindDropdown(raveHRCollection);
            }

            if (objNPS_Validation.IsDisableValidation)
            {
                rfCostCodeValidator.Visible = false;
                rfCostCodeValidator.ValidationGroup = "SAVED";   // Disable Validation
                lblCostCodeRequired.Visible = false;
            }
            else
            {
                rfCostCodeValidator.Visible = true;
                rfCostCodeValidator.ValidationGroup = "Save";  // Enable Validation
                lblCostCodeRequired.Visible = true;
            }

        }
        else
        {
            if (ddlCostCode.SelectedValue != CommonConstants.SELECT)
                ddlCostCode.SelectedValue = CommonConstants.SELECT;
            trCostCode.Visible = false;
        }



    }
    // End



    /// <summary>
    /// FUnction will Call to Fill Skill CategoryDropDown
    /// </summary>
    private void BindSkillCategorydLL()
    {
        try
        {
            //Declaring Master Class Object
            Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

            //Calling Business layer FillDropDown Method
            raveHRCollection = master.FillDropDownsBL(Convert.ToInt32(Common.EnumsConstants.Category.PrimarySkills));

            //Check Collection is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSOurce to Collection
                ddlSkillsCategory.DataSource = raveHRCollection;

                //Assign DataText Filed to DropDown
                ddlSkillsCategory.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign DataValue Field to Dropdown
                ddlSkillsCategory.DataValueField = CommonConstants.DDL_DataValueField;

                //Bind Dropdown
                ddlSkillsCategory.DataBind();

                //Insert Select as a item for dropdown
                ddlSkillsCategory.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                raveHRCollection.Clear();
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                CLASS_NAME_VIEWMRF, "BindSkillCategorydLL", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// Function will call to FIll MRF yep dropdown
    /// </summary>
    private void FillMRFTypeDll()
    {
        try
        {
            //Calling Business layer FillDropDown Method
            raveHRCollection = master.FillDropDownsBL(Convert.ToInt32(Common.EnumsConstants.Category.MRFType));

            //Check Collection is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSOurce to Collection
                ddlMRFType.DataSource = raveHRCollection;

                //Assign DataText Filed to DropDown
                ddlMRFType.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign DataValue Field to Dropdown
                ddlMRFType.DataValueField = CommonConstants.DDL_DataValueField;

                //Bind Dropdown
                ddlMRFType.DataBind();

                //Insert Select as a item for dropdown
                ddlMRFType.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                raveHRCollection.Clear();
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_VIEWMRF, "FillMRFTypeDll", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// Function will call to enable or disable control as View mode.
    /// </summary>
    /// <param name="flag"></param>
    private void ControlEnable(bool flag)
    {
        ddlMRFType.Enabled = flag;
        txtMRFCode.Enabled = flag;
        ddlDepartment.Enabled = flag;
        ddlProjectName.Enabled = flag;
        ddlResourcePlanCode.Enabled = flag;
        ddlRole.Enabled = flag;
        txtProjectDescription.Enabled = flag;
        //txtMustHaveSkills.Enabled = flag;
        //txtGoodSkills.Enabled = flag;
        //txtTools.Enabled = flag;

        //Removing read only for Scroll bar.        
        ChangeModeOfMultilineTextBox(!flag);

        ddlSkillsCategory.Enabled = flag;
        txtDomain.Enabled = flag;
        txtExperince.Enabled = flag;
        txtExperince1.Enabled = flag;
        txtHeighestQualification.Enabled = flag;
        //txtSoftSkills.Enabled = flag;
        txtUtilijation.Enabled = flag;
        txtBilling.Enabled = flag;
        txtTargetCTC.Enabled = flag;
        txtTargetCTC1.Enabled = flag;
        txtRemarks.Enabled = flag;
        txtResponsiblePerson.Enabled = flag;
        //Ishwar Patil 22/04/2015 Start
        TxtSkills.Enabled = flag;
        //Ishwar Patil 22/04/2015 End
        //txtResourceResponsibility.Enabled = flag;
        imgDateOfRequisition.Enabled = flag;
        imgRequiredFrom.Enabled = flag;
        imgRequiredTill.Enabled = flag;
        imgResponsiblePersonSearch.Visible = flag;
        //Ishwar Patil 22/04/2015 Start
        imgSkillsSearch.Visible = flag;
        //Ishwar Patil 22/04/2015 End
        txtDateOfRequisition.Enabled = flag;
        txtRequiredFrom.Enabled = flag;
        //Rajan : Issue 40243 : 22/01/2014 : Start
        //MRF end date changed in resource plan should reflect in MRF and Vice versa. 
        //commented the code,till date required as read only
        //txtRequiredTill.Enabled = flag;
        txtRequiredTill.Enabled = false;
        //Rajan : Issue 40243 : 22/01/2014 : End
        txtClientName.Enabled = flag;
        ddlPuposeDepartment.Enabled = flag;
        //Rakesh : MRF Move Edit Purpose :  10/06/2016 : End

        ddlPurpose.Enabled = flag;
        txtPurposeDescription.Enabled = flag;
        txtPurposeDescription.Enabled = flag;

        // End

        //Rakesh : Actual vs Budget 07/06/2016 Begin 
        if (IsPendingAllocation)
            ddlCostCode.Enabled = IsPendingAllocation;
        else
            ddlCostCode.Enabled = flag;
        //End

        //Done By Sameer For Issue Id:18395
        if (ROLE == AuthorizationManagerConstants.ROLERPM
            && !arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERECRUITMENT))
        {
            ddlColorCode.Enabled = flag;
        }
        else
        {
            ddlColorCode.Enabled = false;
        }

        if (Request.QueryString[PAGETYPE] != null)
        {
            if (DecryptQueryString(PAGETYPE) == PAGETYPEMRFSUMMERY)
            {
                txtReason.Enabled = true;

                //Done By Sameer For Issue Id:18395
                if (DecryptQueryString(PAGETYPE) == PAGETYPEMRFSUMMERY)
                {
                    lblRecruiterName.Visible = true;
                    ddlRecruiterName.Visible = true;
                    ddlRecruiterName.Enabled = flag;
                }
                else
                {
                    lblRecruiterName.Visible = false;
                    ddlRecruiterName.Visible = false;
                    ddlRecruiterName.Enabled = false;
                }
            }
            else
            {
                txtReason.Enabled = flag;
                ddlRecruiterName.Enabled = flag;
            }
        }
        else
        {
            txtReason.Enabled = flag;
        }

        txtSOWNo.Enabled = flag;
        DtPckerSOWStartDt.IsEnable = flag;
        DtPckSOWEndDt.IsEnable = flag;
    }

    /*As per the discussion with Niharika this function is written
    to make the Multi line text boxes scrollable in view mode*/
    /// <summary>
    /// Changes the mode of the Multi line text boxes.
    /// </summary>
    private void ChangeModeOfMultilineTextBox(bool flag)
    {
        txtMustHaveSkills.ReadOnly = flag;
        txtGoodSkills.ReadOnly = flag;
        txtSoftSkills.ReadOnly = flag;
        txtResourceResponsibility.ReadOnly = flag;
        txtTools.ReadOnly = flag;
        if (flag == true)
        {
            txtMustHaveSkills.ForeColor = Color.FromArgb(174, 174, 174);
            txtGoodSkills.ForeColor = Color.FromArgb(174, 174, 174);
            txtSoftSkills.ForeColor = Color.FromArgb(174, 174, 174);
            txtResourceResponsibility.ForeColor = Color.FromArgb(174, 174, 174);
            txtTools.ForeColor = Color.FromArgb(174, 174, 174);
        }
        else
        {
            txtMustHaveSkills.ForeColor = Color.Black;
            txtGoodSkills.ForeColor = Color.Black;
            txtSoftSkills.ForeColor = Color.Black;
            txtResourceResponsibility.ForeColor = Color.Black;
            txtTools.ForeColor = Color.Black;
        }
    }

    /// <summary>
    /// Function will Populate role as per selected Department.
    /// </summary>
    /// <param name="DepartmentId"></param>
    private void DepartmentWiseRoleDispaly(int DepartmentId)
    {
        FillRoleDepartmentWise(DepartmentId);
    }

    /// <summary>
    /// Fuction will call to fill Department wise Role
    /// </summary>
    /// <param name="DepartmentId"></param>
    private void FillRoleDepartmentWise(int DepartmentId)
    {
        try
        {
            //Declaring Master Class Object
            Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

            //Calling Business layer FillDropDown Method
            raveHRCollection = mRFDetail.GetRoleDepartmentWiseBL(DepartmentId);

            //Check Collection is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSOurce to Collection
                ddlRole.DataSource = raveHRCollection;

                //Assign DataText Filed to DropDown
                ddlRole.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign DataValue Field to Dropdown
                ddlRole.DataValueField = CommonConstants.DDL_DataValueField;

                //Bind Dropdown
                ddlRole.DataBind();

                //Insert Select as a item for dropdown
                ddlRole.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                raveHRCollection.Clear();
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                CLASS_NAME_VIEWMRF, "FillRoleDepartmentWise", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Function will Call to Enable or Disable controls
    /// </summary>
    /// <param name="flag"></param>
    private void ControlEditEnable(bool flag)
    {
        ddlMRFType.Enabled = flag;
        txtMustHaveSkills.Enabled = flag;
        txtGoodSkills.Enabled = flag;
        txtTools.Enabled = flag;
        ddlSkillsCategory.Enabled = flag;
        txtExperince.Enabled = flag;
        txtExperince1.Enabled = flag;
        txtHeighestQualification.Enabled = flag;
        txtSoftSkills.Enabled = flag;
        txtUtilijation.Enabled = flag;
        txtBilling.Enabled = flag;
        txtTargetCTC.Enabled = flag;
        txtTargetCTC1.Enabled = flag;
        txtRemarks.Enabled = flag;
        txtResourceResponsibility.Enabled = flag;
        imgResponsiblePersonSearch.Visible = flag;
        //Ishwar Patil 22/04/2015 Start
        imgSkillsSearch.Visible = flag;
        //Ishwar Patil 22/04/2015 End
        txtClientName.Enabled = flag;
        txtDomain.Enabled = flag;
        ddlPurpose.Enabled = flag;
        txtPurposeDescription.Enabled = flag;
        txtBillingDate.IsEnable = false;
        ddlMRFStatus.Enabled = false;
        ddlColorCode.Enabled = flag;

        //Rakesh : Actual vs Budget 07/06/2016 Begin  
        if (IsPendingAllocation)
            ddlCostCode.Enabled = IsPendingAllocation;
        else
            ddlCostCode.Enabled = flag;
        //End



        txtSOWNo.Enabled = flag;
        DtPckerSOWStartDt.IsEnable = flag;
        DtPckSOWEndDt.IsEnable = flag;

        // venkatesh  : Issue 39795 : 21/11/2013 : Starts
        // Desc : Role Editable
        if (ddlDepartment.SelectedItem.Text.ToString().ToLower() != "projects" && flag == true)
            ddlRole.Enabled = flag;
        else
            ddlRole.Enabled = false;
        // venkatesh  : Issue 39795 : 21/11/2013 : End

        //Modified By Rajesh; Issue Id 20969.
        if (ROLE == AuthorizationManagerConstants.ROLERPM)
        {
            if (MRFSTATUS == Convert.ToInt32(MasterEnum.MRFStatus.PendingExternalAllocation))
            {
                if (flag == true)
                {
                    ddlMRFStatus.SelectedValue = Convert.ToInt32(MasterEnum.MRFStatus.PendingExternalAllocation).ToString();
                    ddlMRFStatus.Enabled = true;
                    btnEdit.Enabled = true;
                }
                else
                {
                    btnEdit.Enabled = true;
                    ddlMRFStatus.Enabled = false;
                }
            }
            else
            {
                btnEdit.Enabled = false;
            }

            //Modified By Rajesh; Issue Id 20969. Enabling/Disabling Purpose dropdown in Editing

            if (txtMRFStatus.Value == MasterEnum.MRFStatus.PendingAllocation.ToString() && flag == true)
            {
                ddlPurpose.Enabled = false;
                ddlMRFStatus.SelectedValue = Convert.ToInt32(MasterEnum.MRFStatus.PendingAllocation).ToString();
            }
            else if (txtMRFStatus.Value == MasterEnum.MRFStatus.PendingExternalAllocation.ToString() && flag == true)
            {
                if (ddlPurpose.SelectedValue == Convert.ToInt32(MasterEnum.MRFPurpose.MarketResearchfeasibility).ToString())
                {
                    ddlPurpose.Enabled = false;
                }
                else
                {
                    ddlPurpose.Enabled = true;
                }
                ddlMRFStatus.SelectedValue = Convert.ToInt32(MasterEnum.MRFStatus.PendingExternalAllocation).ToString();
            }
            else if (txtMRFStatus.Value == MasterEnum.MRFStatus.MarketResearchCompleteAndClosed.ToString() && flag == true)
            {
                ddlPurpose.Enabled = false;
                ddlMRFStatus.SelectedValue = Convert.ToInt32(MasterEnum.MRFStatus.MarketResearchCompleteAndClosed).ToString();
            }
            else
            {
                ddlMRFStatus.Enabled = false;

            }
            //Modified By Rajesh- Ended; Issue Id 20969.

            //Modified By Rajesh; Issue Id 20516. Validations for Billing Date
            if (Convert.ToInt32(ddlDepartment.SelectedItem.Value) == Convert.ToInt32(Common.MasterEnum.Departments.Projects)
                || Convert.ToInt32(ddlDepartment.SelectedItem.Value) == Convert.ToInt32(Common.MasterEnum.Departments.RaveForecastedProjects))
            {
                txtBillingDate.IsEnable = true;
            }

            uclExpectedClosureDate.IsEnable = true;
            txtReasonExpDate.Enabled = true;
            // Rajan Kumar : Issue 41677: 20/01/2014 : Starts                        			 
            // Desc : When MRF is in Pending New Employee Allocation then Purpose and project name should able to change.
            if (txtMRFStatus.Value == MasterEnum.MRFStatus.PendingNewEmployeeAllocation.ToString())
            {
                txtPurposeDescription.ReadOnly = false;
                ddlPurpose.Enabled = true;
            }

            // Rajan Kumar : Issue 41677 : 20/01/2014: Ends

        }
        //Modified By Rajesh Ended; Issue Id 20969.
        //Issue Id :48204 Added RMGroup so that once they become recruiter head they will have rights to change recruiter   Mahendra

        //Start :  venkatesh 58052 
        //Desc : Added PM if he\she also Recruitment so that once they become recruiter head they will have rights to change recruiter
        //Date :  20- May- 2016
        AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
        arrRolesForUser = objRaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());
        bool PM_Recruitment = false;
        if (arrRolesForUser.Count != 0)
        {
            if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEPM) && arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERECRUITMENT))
            {
                PM_Recruitment = true;
            }
        }

        if ((ROLE == AuthorizationManagerConstants.ROLERECRUITMENT) || (ROLE == AuthorizationManagerConstants.ROLERPM) || (PM_Recruitment))
        {
            if (DecryptQueryString(PAGETYPE) == PAGETYPEMRFSUMMERY)
            {
                //Check wether loged in user is department head
                if (IsDepartmentHead())
                {
                    ddlRecruiterName.Enabled = true;
                    ddlMRFStatus.Enabled = true;
                }
                else
                {
                    ddlRecruiterName.Enabled = false;
                    ddlMRFStatus.Enabled = false;
                }
            }
        }
        //End :  venkatesh 58052 
        if (ROLE == AuthorizationManagerConstants.ROLERPM)
        {
            if (DecryptQueryString(PAGETYPE) == PAGETYPEMRFSUMMERY)
            {
                ddlMRFStatus.Enabled = true;
            }
        }
    }

    /// <summary>
    /// Function will Call to Enable
    /// </summary>
    /// <param name="flag"></param>
    private void PopulateDetailsForMoveMrf()
    {
        lblMessage.Text = "Please change Responsible Person if required.";

        bool flag = false;
        ddlMRFType.Enabled = false;
        txtMustHaveSkills.Enabled = false;
        txtGoodSkills.Enabled = false;
        txtTools.Enabled = false;
        ddlSkillsCategory.Enabled = false;
        txtExperince.Enabled = false;
        txtExperince1.Enabled = false;
        txtHeighestQualification.Enabled = false;
        txtSoftSkills.Enabled = false;
        txtUtilijation.Enabled = false;
        txtBilling.Enabled = false;
        txtTargetCTC.Enabled = false;
        txtTargetCTC1.Enabled = false;
        txtRemarks.Enabled = false;
        txtResourceResponsibility.Enabled = false;
        imgResponsiblePersonSearch.Visible = false;
        txtClientName.Visible = false;
        txtClientName.Enabled = false;
        txtDomain.Enabled = false;
        ddlColorCode.Enabled = false;

        ddlProjectName.Enabled = true;
        ddlDepartment.Enabled = true;
        ddlResourcePlanCode.Enabled = true;
        ddlRole.Enabled = true;
        ddlCostCode.Enabled = true;
        txtResponsiblePerson.Enabled = true;
        imgResponsiblePersonSearch.Visible = true;
        //Ishwar Patil 22/04/2015 Start
        TxtSkills.Enabled = true;
        imgSkillsSearch.Visible = true;
        //Ishwar Patil 22/04/2015 End

        txtBillingDate.Visible = false;
        lblBillingDate.Visible = false;
        lblBillingDatemandatory.Visible = false;

        ddlRecruiterName.Enabled = flag;
        ddlMRFStatus.Enabled = false;

        imgPurpose.Visible = false;

        //if (txtMRFStatus.Value == MasterEnum.MRFStatus.PendingAllocation.ToString())
        //{
        //    ddlMRFStatus.SelectedValue = Convert.ToInt32(MasterEnum.MRFStatus.PendingAllocation).ToString();
        //}
        //if (txtMRFStatus.Value == MasterEnum.MRFStatus.PendingExternalAllocation.ToString())
        //{
        //    ddlMRFStatus.SelectedValue = Convert.ToInt32(MasterEnum.MRFStatus.PendingExternalAllocation).ToString();
        //}
        //if (txtMRFStatus.Value == MasterEnum.MRFStatus.MarketResearchCompleteAndClosed.ToString() && flag == true)
        //{
        //    ddlMRFStatus.SelectedValue = Convert.ToInt32(MasterEnum.MRFStatus.MarketResearchCompleteAndClosed).ToString();
        //}
        //if (txtMRFStatus.Value == MasterEnum.MRFStatus.PendingExternalAllocation.ToString())
        //{
        //    uclExpectedClosureDate.IsEnable = false;
        //    txtReasonExpDate.Enabled = false;
        //}

        lblReasonMoveMRF.Visible = true;
        lblReasonMoveMRFMandatory.Visible = true;
        txtReasonMoveMRF.Visible = true;
    }

    /// <summary>
    /// Method checks whether login user is department head or not
    /// </summary>
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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_VIEWMRF, "IsDepartmentHead", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Function will call for edit MRF Detail
    /// </summary>
    private void EDITMRFDetail()
    {
        int EditMRFId;
        Boolean IsRecruiterReassigned = false;
        Boolean IsMrfStatusChanged = false;
        BusinessEntities.MRFDetail MRFDetailobject = new BusinessEntities.MRFDetail();
        txtBillingDate.IsEnable = false;

        try
        {
            MRFDetailobject = GetControlValues();

            //Modified By Rajesh; Issue Id 20516. Validations for Billing Date
            if (Convert.ToInt32(ddlDepartment.SelectedItem.Value) == Convert.ToInt32(Common.MasterEnum.Departments.Projects))
            {
                if (ROLE == AuthorizationManagerConstants.ROLERPM)
                {
                    DateTime fromDate = Convert.ToDateTime(txtRequiredFrom.Text);
                    //Rajan : Issue 40243 : 22/01/2014 : Start
                    //MRF end date changed in resource plan should reflect in MRF and Vice versa. 
                    //Handle for null value
                    DateTime toDate = DateTime.MaxValue;
                    if (!string.IsNullOrEmpty(txtRequiredTill.Text))
                    {
                        toDate = Convert.ToDateTime(txtRequiredTill.Text);
                    }
                    //Rajan : Issue 40243 : 22/01/2014 : End         

                    // 27951-Ambar-Start
                    // Added following code for checking billing data only in case when billing is > 0
                    int billingAmount = Convert.ToInt32(txtBilling.Text);
                    if (billingAmount > 0)
                    {
                        // 27951-Ambar-End
                        if (txtBillingDate.Text != null && txtBillingDate.Text != "" && txtBillingDate.Text.Trim() != string.Empty)
                        {
                            if (Convert.ToDateTime(txtBillingDate.Text) < fromDate
                                || Convert.ToDateTime(txtBillingDate.Text) > toDate)
                            {
                                lblMessage.Text = "Billing date should lies between Required From and Required Till.";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                txtBillingDate.Text = "";
                                txtBillingDate.TextBox.BorderColor = System.Drawing.Color.Red;
                                txtBillingDate.TextBox.BorderWidth = 1;
                                txtBillingDate.IsEnable = true;
                                return;
                            }
                            else
                            {
                                MRFDetailobject.BillingDate = Convert.ToDateTime(txtBillingDate.Text);
                                txtBillingDate.TextBox.BorderColor = System.Drawing.Color.Gray;
                                txtBillingDate.TextBox.BorderWidth = 1;
                                lblMessage.ForeColor = System.Drawing.Color.Blue;
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Billing date Can't be empty.";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            txtBillingDate.Text = "";
                            txtBillingDate.TextBox.BorderColor = System.Drawing.Color.Red;
                            txtBillingDate.TextBox.BorderWidth = 1;
                            txtBillingDate.IsEnable = true;
                            // 35093-Ambar-Start
                            txtBillingDate.Visible = true;
                            lblBillingDate.Visible = true;
                            lblBillingDatemandatory.Visible = true;
                            // 35093-Ambar-End
                            return;
                        }
                    }// 27951-Ambar
                }
            }
            //Modified By Rajesh Ended; Issue Id 20516.

            //Modified By Rajesh Start ; Issue Id 20083.
            if (ROLE == AuthorizationManagerConstants.ROLERPM)
            {
                if (ddlMRFStatus.SelectedValue == Convert.ToInt32(MasterEnum.MRFStatus.PendingExternalAllocation).ToString())
                {
                    if (uclExpectedClosureDate.Text != " " && uclExpectedClosureDate.Text != null
                        && uclExpectedClosureDate.Text != string.Empty)
                    {
                        if (hidRequestForRecruitment.Value == "" || hidRequestForRecruitment.Value == null)
                        {
                            MRFDetailobject.ExpectedClosureDate = uclExpectedClosureDate.Text;
                            MRFDetailobject.ReasonForExtendingExpectedClosureDate = txtReasonExpDate.Text;
                        }
                        else if (Convert.ToDateTime(uclExpectedClosureDate.Text) >= Convert.ToDateTime(hidRequestForRecruitment.Value))
                        {
                            MRFDetailobject.ExpectedClosureDate = uclExpectedClosureDate.Text;
                            if (hidExpectedClosedDate.Value != uclExpectedClosureDate.Text)
                            {
                                if (txtReasonExpDate.Text != null && txtReasonExpDate.Text.Trim() != string.Empty && txtReasonExpDate.Text != "" && hidExpectedClosedDate.Value != uclExpectedClosureDate.Text)
                                {
                                    MRFDetailobject.ReasonForExtendingExpectedClosureDate = txtReasonExpDate.Text;
                                }
                                else
                                {
                                    lblMessage.Text = "Please enter the reason for extending expected closure date.";
                                    lblMessage.ForeColor = System.Drawing.Color.Red;
                                    txtReasonExpDate.Text = "";
                                    txtReasonExpDate.BorderColor = System.Drawing.Color.Red;
                                    txtReasonExpDate.BorderWidth = 1;
                                    txtReasonExpDate.Enabled = true;
                                    return;
                                }
                            }
                            else
                            {
                                MRFDetailobject.ReasonForExtendingExpectedClosureDate = txtReasonExpDate.Text;
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Expected Closure Date should be greater than RequestforRecruitement date ( " + hidRequestForRecruitment.Value + ")";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            uclExpectedClosureDate.Text = "";
                            uclExpectedClosureDate.TextBox.BorderColor = System.Drawing.Color.Red;
                            uclExpectedClosureDate.TextBox.BorderWidth = 1;
                            uclExpectedClosureDate.IsEnable = true;
                            return;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Please enter the Expected Closure Date";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        uclExpectedClosureDate.Text = "";
                        uclExpectedClosureDate.TextBox.BorderColor = System.Drawing.Color.Red;
                        uclExpectedClosureDate.TextBox.BorderWidth = 1;
                        uclExpectedClosureDate.IsEnable = true;
                        return;
                    }
                }
                else
                {
                    if (hidRequestForRecruitment.Value != "" && hidRequestForRecruitment.Value != "")
                    {
                        MRFDetailobject.ExpectedClosureDate = uclExpectedClosureDate.Text;
                        MRFDetailobject.ReasonForExtendingExpectedClosureDate = txtReasonExpDate.Text;
                    }
                }
            }

            if (!string.IsNullOrEmpty(hidRecruiterId.Value))
            {
                // 35093-Ambar-Start
                // While Recruiter Reassignment set the values
                if (hidRecruiterId.Value != MRFDetailobject.RecruitersId)
                {
                    IsRecruiterReassigned = true;
                    MRFDetailobject.ExpectedClosureDate = uclExpectedClosureDate.Text;
                    if ((txtReasonExpDate.Text.Trim() != "") && (txtReasonExpDate.Text.Trim() != string.Empty) && (txtReasonExpDate.Text != null))
                    {
                        MRFDetailobject.ReasonForExtendingExpectedClosureDate = txtReasonExpDate.Text;
                    }
                    if ((txtBillingDate.Text.Trim() != "") && (txtBillingDate.Text.Trim() != string.Empty) && (txtBillingDate.Text != null))
                    {
                        MRFDetailobject.BillingDate = Convert.ToDateTime(txtBillingDate.Text);
                    }

                }
                // 35093-Ambar-End
            }

            if (ddlMRFStatus.Enabled)
            {
                if (previousMRFStatus.Value != "")
                {
                    //Siddhesh : Issue 56245 : 28/07/2015 : Start
                    if (ddlMRFStatus.SelectedValue != previousMRFStatus.Value)
                    //Siddhesh : Issue 56245 : 28/07/2015 : End
                    {
                        IsMrfStatusChanged = true;
                        MRFDetailobject.StatusId = Convert.ToInt32(ddlMRFStatus.SelectedValue);
                    }
                }
            }

            // 35093-Ambar-Start
            if (ROLE != AuthorizationManagerConstants.ROLERPM)
            {
                if (ROLE == AuthorizationManagerConstants.ROLEPM)
                {
                    // set the values if Role is PM
                    MRFDetailobject.ExpectedClosureDate = uclExpectedClosureDate.Text;
                    if ((txtReasonExpDate.Text != "") && (txtReasonExpDate.Text != string.Empty) && (txtReasonExpDate.Text != null))
                    {
                        MRFDetailobject.ReasonForExtendingExpectedClosureDate = txtReasonExpDate.Text;
                    }
                    if ((txtBillingDate.Text.Trim() != "") && (txtBillingDate.Text.Trim() != string.Empty) && (txtBillingDate.Text != null))
                    {
                        MRFDetailobject.BillingDate = Convert.ToDateTime(txtBillingDate.Text);
                    }
                }
            }
            // 35093-Ambar-End

            #region Modified By Mohamed Dangra
            // Mohamed : Issue 50791 : 12/05/2014 : Starts                        			  
            // Desc : IN Mrf Details ,GroupId need to implement
            if (chkGroupId.Checked)
            {
                MRFDetailobject.GroupId = mRFDetail.GetMRFDetailMaxGroupIdBL() + 1;
            }

            // Mohamed : Issue 50791 : 12/05/2014 : Ends
            #endregion Modified By Mohamed Dangra

            EditMRFId = mRFDetail.EditMRFBL(MRFDetailobject, IsRecruiterReassigned, IsMrfStatusChanged);

            #region Modified By Mohamed Dangra
            // Mohamed : Issue 51824  : 19/08/2014 : Starts                        			  
            // Desc : Provision in RMS system for a notification to be sent when an MRf is assigned  from one recruiter to another.
            if (ddlRecruiterName.Enabled)
            {
                if (hidRecruiterId.Value != ddlRecruiterName.SelectedValue)
                {
                    Utility objUtility = new Utility();
                    string MRFCode = txtMRFCode.Text;
                    string NewRecruiterEmailId = mRFDetail.GetEmailIdByEmpId(Convert.ToInt32(ddlRecruiterName.SelectedValue));
                    string OldRecruiterEmailId = mRFDetail.GetEmailIdByEmpId(Convert.ToInt32(hidRecruiterId.Value));
                    string NewRecruiterName = objUtility.GetEmployeeFirstName(NewRecruiterEmailId);
                    string OldRecruiterName = objUtility.GetEmployeeFirstName(OldRecruiterEmailId);
                    string Role = ddlRole.SelectedItem.Text;
                    string RequiredDate = mrfDetail.ExpectedClosureDate;
                    string ProjectName = ddlProjectName.SelectedItem.Text;
                    mRFDetail.SendMailMRFAssignedFromOneRecruiterToAnother(NewRecruiterEmailId, OldRecruiterEmailId, NewRecruiterName, MRFCode, Role, OldRecruiterName, RequiredDate, ProjectName);
                }
            }

            // Mohamed : Issue 51824  : 19/08/2014 : Ends
            #endregion Modified By Mohamed Dangra

            if (IsRecruiterReassigned)
                lblMessage.Text = CommonConstants.MSG_MRF_REASSIGNED;
            else
                lblMessage.Text = "MRF Code [" + txtMRFCode.Text + "] Edited Successfully";
            lblMessage.ForeColor = System.Drawing.Color.Blue;
            btnPrevious.Visible = true;
            btnNext.Visible = true;
            lblHeaderViewEdit.Text = ViewMRF;
            btnSavebtn.Visible = false;
            btnEdit.Visible = true;
            btnDelete.Visible = true;
            ControlEditEnable(false);
            EnableDisableButtons(MRFCURRENTCOUNT, MRFPREVIOUSCOUNT, MRFNEXTCOUNT);
            //}
            #region Modified By Mohamed Dangra
            // Mohamed : Issue 50791 : 12/05/2014 : Starts                        			  
            // Desc : IN Mrf Details ,GroupId need to implement
            chkGroupId.Visible = false;
            // Mohamed : Issue 50791 : 12/05/2014 : Ends
            #endregion Modified By Mohamed Dangra


        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                CLASS_NAME_VIEWMRF, "EDITMRFDetail", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }
    /// <summary>
    /// Function will call for move MRF Detail
    /// </summary>
    private void MOVEMRFDetails()
    {
        string NewMRFCode = string.Empty;
        txtBillingDate.IsEnable = false;
        BusinessEntities.MRFDetail MRFDetailobject = new BusinessEntities.MRFDetail();
        try
        {
            BusinessEntities.MRFDetail MRFDetailobjForDeleteOldMRF = new BusinessEntities.MRFDetail();
            MRFDetailobjForDeleteOldMRF = GetAbortControlValues();
            Rave.HR.BusinessLayer.MRF.MRFDetail mrfDetailsBll = new Rave.HR.BusinessLayer.MRF.MRFDetail();
            MRFDetailobject = mrfDetailsBll.GetMrfMoveDetail(Convert.ToInt32(hidMRFID.Value));

            if (hidGridEnabled.Value == "true" && hidNoOfResources.Value == "")
            {
                lblMessage.Text = "Please select a role in the grid.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                hidNoOfResources.Value = "";
                return;
            }
            if (ddlRole.SelectedItem.Text != hidMoveMrfRoleName.Value)
            {
                lblMessage.Text = "Mrf can be moved only to " + hidMoveMrfRoleName.Value + " role.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                hidNoOfResources.Value = "";
                return;
            }


            //Handling validation from server side. Client side validation have some risk's in case of Edit.
            if (Convert.ToInt32(ddlDepartment.SelectedItem.Value) == Convert.ToInt32(Common.MasterEnum.Departments.Projects)
                   || Convert.ToInt32(ddlDepartment.SelectedItem.Value) == Convert.ToInt32(Common.MasterEnum.Departments.RaveForecastedProjects))
            {
                for (int i = 0; i < grdresource.Rows.Count; i++)
                {
                    CheckBox chkRPDuId = (CheckBox)grdresource.Rows[i].Cells[0].FindControl("chk");
                    Label lblBilling = (Label)grdresource.Rows[i].FindControl("lblBilling");
                    int billingAmount = Convert.ToInt32(lblBilling.Text);

                    if (chkRPDuId.Checked)
                    {
                        if (billingAmount > 0)
                        {
                            DateTime RPStartDate = Convert.ToDateTime(grdresource.Rows[i].Cells[2].Text);
                            DateTime RPEndDate = Convert.ToDateTime(grdresource.Rows[i].Cells[3].Text);
                            UIControls_DatePickerControl billingDatePicker = (UIControls_DatePickerControl)grdresource.Rows[i].Cells[5].FindControl("billingDatePicker");

                            if (billingDatePicker.Text != null && billingDatePicker.Text != "")
                            {
                                DateTime billingDate = Convert.ToDateTime(billingDatePicker.Text);
                                if ((billingDate < RPStartDate) || (billingDate > RPEndDate))
                                {
                                    lblMessage.Text = "Billing Date should be between Start date and End date";
                                    lblMessage.ForeColor = System.Drawing.Color.Red;
                                    billingDatePicker.TextBox.BorderStyle = BorderStyle.Solid;
                                    billingDatePicker.TextBox.BorderColor = System.Drawing.Color.Red;
                                    return;
                                }
                            }
                            else
                            {
                                lblMessage.Text = "Billing date Can't be empty";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                billingDatePicker.TextBox.BorderStyle = BorderStyle.Solid;
                                billingDatePicker.TextBox.BorderColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                    }
                }
            }

            if (ddlDepartment.SelectedItem.Text == CommonConstants.PROJECTS)
            {
                if (ddlProjectName.SelectedItem.Text != CommonConstants.SELECT)
                {
                    ddlProjectName.BorderColor = System.Drawing.Color.Gray;
                    if (ddlResourcePlanCode.SelectedItem.Text != CommonConstants.SELECT)
                    {
                        ddlResourcePlanCode.BorderColor = System.Drawing.Color.Gray;
                        if (ddlRole.SelectedValue != CommonConstants.SELECT)
                        {
                            ddlRole.BorderColor = System.Drawing.Color.Gray;
                            if (MRFDetailobject.ProjectId != Convert.ToInt32(ddlProjectName.SelectedValue))
                            {
                                if (txtReasonMoveMRF.Text != null && txtReasonMoveMRF.Text.Trim() != "" && txtReasonMoveMRF.Text != string.Empty)
                                {
                                    txtReasonMoveMRF.BorderColor = System.Drawing.Color.Gray;
                                    MRFDetailobject = GetMoveMRFControlValues(MRFDetailobject);
                                }
                                else
                                {
                                    lblMessage.Text = "Please enter the Reason for moving MRF.";
                                    lblMessage.ForeColor = System.Drawing.Color.Red;
                                    txtReasonMoveMRF.BorderColor = System.Drawing.Color.Red;
                                    txtReasonMoveMRF.BorderWidth = 1;
                                    return;
                                }
                            }
                            else
                            {
                                lblMessage.Text = "MRF can't be moved to same project.";
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                return;
                            }
                        }
                        else
                        {

                            lblMessage.Text = "Please select a Role.";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            ddlRole.BorderColor = System.Drawing.Color.Red;
                            ddlRole.BorderWidth = 1;
                            return;

                        }
                    }
                    else
                    {

                        lblMessage.Text = "Please select a Resource Plan Code.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        ddlResourcePlanCode.BorderColor = System.Drawing.Color.Red;
                        ddlResourcePlanCode.BorderWidth = 1;
                        return;

                    }
                }
                else
                {

                    lblMessage.Text = "Please select a Project.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    ddlProjectName.BorderColor = System.Drawing.Color.Red;
                    ddlProjectName.BorderWidth = 1;
                    return;

                }
            }
            else if (MRFDetailobject.DepartmentName != ddlDepartment.SelectedItem.Text)
            {
                if (ddlRole.SelectedValue != CommonConstants.SELECT)
                {
                    MRFDetailobject = GetMoveMRFControlValues(MRFDetailobject);
                }
                else
                {
                    lblMessage.Text = "Please select a Role.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    ddlRole.BorderColor = System.Drawing.Color.Red;
                    ddlRole.BorderWidth = 1;
                    return;
                }
            }
            else
            {
                lblMessage.Text = "MRF can't be moved to same deparment.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            hidOldMRFCode.Value = MRFDetailobject.MRFCode;

            #region Modified By Mohamed Dangra
            // Mohamed : Issue 50791 : 12/05/2014 : Starts                        			  
            // Desc : IN Mrf Details ,GroupId need to implement
            if (chkGroupId.Checked)
            {
                MRFDetailobject.GroupId = mRFDetail.GetMRFDetailMaxGroupIdBL() + 1;
            }

            // Mohamed : Issue 50791 : 12/05/2014 : Ends
            #endregion Modified By Mohamed Dangra

            //     if (ddlCostCode.SelectedItem!=null)

            if ((!string.IsNullOrEmpty(ddlCostCode.SelectedValue)) && ddlCostCode.SelectedValue != CommonConstants.SELECT)
                MRFDetailobject.CostCodeId = ddlCostCode.SelectedValue.CastToInt32();
            else
                MRFDetailobject.CostCodeId = null;

            //Rakesh : MRF Move Edit Purpose :  10/06/2016 : 

            if (ddlPurpose.SelectedItem.Text != Common.CommonConstants.SELECT)
                MRFDetailobject.MRFPurposeId = Convert.ToInt32(ddlPurpose.SelectedValue);
            else
                MRFDetailobject.MRFPurposeId = CommonConstants.ZERO;



            if (txtPurposeDescription.Visible == true && !string.IsNullOrEmpty(hidEmployeeName.Value))
                MRFDetailobject.MRFPurposeDescription = hidEmployeeName.Value.Trim();

            if (txtPurposeDescription.Visible == true && !string.IsNullOrEmpty(txtPurposeDescription.Text))
                MRFDetailobject.MRFPurposeDescription = txtPurposeDescription.Text.Trim();



            if (ddlPuposeDepartment.Visible == true && ddlPuposeDepartment.SelectedIndex > 0)
                MRFDetailobject.MRFPurposeDescription = ddlPuposeDepartment.SelectedItem.Text;

            // Rakesh -  Issue : 57942  to Set Dropdown Value to Object
            if (ddlPurpose.SelectedItem != null)
                MRFDetailobject.MRFPurposeId = ddlPurpose.SelectedValue.CastToInt32();

            //Rakesh : MRF Move Edit Purpose :  End

            NewMRFCode = mRFDetail.MoveMRFBL(MRFDetailobject, MRFDetailobjForDeleteOldMRF);
            MRFDetailobject.MRFCode = NewMRFCode;

            if (NewMRFCode != string.Empty)
            {
                string msg = "MRF Code [" + txtMRFCode.Text + "] Moved Successfully to New MRF Code [" + NewMRFCode + "]";
                Session["MovedStatus"] = msg;
                Response.Redirect(CommonConstants.Page_MrfSummary, false);
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                CLASS_NAME_VIEWMRF, "MOVEMRFDetails", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// Function will Get Control Values for Update Records.
    /// </summary>
    /// <returns></returns>
    private BusinessEntities.MRFDetail GetControlValues()
    {
        BusinessEntities.MRFDetail mrfDetailobj = new BusinessEntities.MRFDetail();
        try
        {

            AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
            string UserMailId = objRaveHRAuthorizationManager.getLoggedInUserEmailId();

            mrfDetailobj.MRFId = Convert.ToInt32(hidMRFID.Value);


            //if (MRFSTATUS != 0 && txtMRFStatus.Value != Convert.ToString(MasterEnum.MRFStatus.PendingExternalAllocation))
            //{
            //    mrfDetailobj.StatusId = MRFSTATUS;
            //}
            //else
            //{
            mrfDetailobj.StatusId = Convert.ToInt32(ddlMRFStatus.SelectedValue);
            MRFSTATUS = mrfDetail.StatusId;
            //}

            //Rakesh : Actual vs Budget 07/06/2016 Begin  
            if (ddlCostCode.SelectedItem != null && ddlCostCode.SelectedItem.Text != Common.CommonConstants.SELECT)
            {
                mrfDetailobj.CostCodeId = ddlCostCode.SelectedValue.CastToInt32();
            }
            else
            {
                mrfDetailobj.CostCodeId = 0;
            }
            //End

            if (ddlMRFType.SelectedItem.Text != Common.CommonConstants.SELECT)
            {
                mrfDetailobj.MRfType = Convert.ToInt32(ddlMRFType.SelectedItem.Value);
            }
            else
            {
                mrfDetailobj.MRfType = 0;
            }

            mrfDetailobj.MustToHaveSkills = txtMustHaveSkills.Text.Trim().ToString();
            mrfDetailobj.GoodToHaveSkills = txtGoodSkills.Text.Trim().ToString();
            mrfDetailobj.Tools = txtTools.Text.Trim().ToString();
            if (ddlSkillsCategory.SelectedItem.Text != Common.CommonConstants.SELECT)
            {
                mrfDetailobj.SkillCategoryId = Convert.ToInt32(ddlSkillsCategory.SelectedItem.Value);
            }
            else
            {
                mrfDetailobj.SkillCategoryId = 0;
            }
            string Experience = txtExperince.Text.Trim().ToString() + "-" + txtExperince1.Text.Trim().ToString();
            if (Experience == "")
            {
                mrfDetailobj.Experience = Convert.ToDecimal(0);
            }
            else
            {
                mrfDetailobj.ExperienceString = Experience;
            }

            mrfDetailobj.Qualification = txtHeighestQualification.Text.Trim().ToString();
            mrfDetailobj.SoftSkills = txtSoftSkills.Text.Trim().ToString();
            mrfDetailobj.Utilization = Convert.ToInt32(txtUtilijation.Text.Trim().ToString());
            mrfDetailobj.Billing = Convert.ToInt32(txtBilling.Text.Trim().ToString());

            string TargetCTC = txtTargetCTC.Text.Trim().ToString() + "-" + txtTargetCTC1.Text.Trim().ToString();
            if (txtTargetCTC.Text == "")
            {
                mrfDetailobj.MRFCTC = Convert.ToDecimal(0);
            }
            else
            {
                mrfDetailobj.MRFCTCString = TargetCTC;
            }

            mrfDetailobj.Remarks = txtRemarks.Text.Trim().ToString();
            mrfDetailobj.ReportingToId = hidResponsiblePersonName.Value.ToString();
            if (txtResponsiblePerson.Text != "" && txtResponsiblePerson.Text != null && txtResponsiblePerson.Text.Trim() != string.Empty)
            {
                mrfDetailobj.ReportingToEmployee = hidReportingToName.Value;
            }
            mrfDetailobj.ResourceResponsibility = txtResourceResponsibility.Text.Trim().ToString();
            mrfDetailobj.ClientName = txtClientName.Text;
            mrfDetailobj.LoggedInUserEmail = UserMailId;
            mrfDetailobj.MRFColorCode = ddlColorCode.SelectedValue;
            mrfDetailobj.Domain = txtDomain.Text.Trim();

            //checks is Recruiter Exist Or Not
            if (ddlRecruiterName.SelectedValue == CommonConstants.SELECT)
            {
                mrfDetailobj.RecruitersId = "0";
            }
            else
            {
                mrfDetailobj.RecruitersId = ddlRecruiterName.SelectedValue;
            }
            mrfDetailobj.Role = ddlRole.SelectedItem.Text;
            mrfDetailobj.RoleId = Convert.ToInt32(ddlRole.SelectedValue.ToString());
            mrfDetailobj.DepartmentName = ddlDepartment.SelectedItem.Text;


            //Venkatesh: Issue 39795 : 24/12/2013 : Start
            // Desc : Update MRFCOde
            mrfDetailobj.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);

            if (ddlDepartment.SelectedItem.Text == "Projects")
            {
                mrfDetailobj.ProjectId = Convert.ToInt32(ddlProjectName.SelectedValue);
                mrfDetailobj.ProjectName = ddlProjectName.SelectedItem.Text;
            }
            else
            {
                mrfDetailobj.ProjectId = 0;
                mrfDetailobj.ProjectName = "SELECT";
            }

            string[] rolearr = mrfDetailobj.Role.Split(SPILITER_DASH);
            mrfDetailobj.RoleName = mrfDetailobj.Role;//rolearr[0].ToString();
            mrfDetailobj.Role = rolearr[1].ToString();

            string MRFCode = "";
            if (mrfDetailobj.ProjectId != 0)
            {
                string[] mrfcode = txtMRFCode.Text.Trim().Split('_');
                MRFCode = "MRF_" + mrfDetailobj.ProjectName + "_" + mrfDetailobj.Role + "_" + Convert.ToString(mrfcode[mrfcode.Length - 1]);
                mrfDetailobj.MRFCode = MRFCode;
            }
            else
            {
                string[] mrfcode = txtMRFCode.Text.Trim().Split('_');
                MRFCode = "MRF_" + mrfDetailobj.DepartmentName + "_" + mrfDetailobj.Role + "_" + Convert.ToString(mrfcode[mrfcode.Length - 1]);
                mrfDetailobj.MRFCode = MRFCode;
            }

            txtMRFCode.Text = MRFCode;
            ///mrfDetailobj.MRFCode = //Venkatesh: Issue 39795 : 24/12/2013 : End

            if (ddlPurpose.SelectedValue != CommonConstants.SELECT)
            {
                mrfDetailobj.MRFPurposeId = Convert.ToInt32(ddlPurpose.SelectedValue);
            }
            else
            {
                mrfDetailobj.MRFPurposeId = Convert.ToInt32(CommonConstants.ZERO);
            }

            if (!String.IsNullOrEmpty(hidEmployeeName.Value))
                mrfDetailobj.MRFPurposeDescription = hidEmployeeName.Value;
            else
                mrfDetailobj.MRFPurposeDescription = txtPurposeDescription.Text.Trim();

            mrfDetailobj.ProjectName = ddlProjectName.SelectedItem.Text;


            if (mrfDetailobj.MRFPurposeId == Convert.ToInt32(MasterEnum.MRFPurpose.MarketResearchfeasibility))
            {
                mrfDetailobj.MRfType = Convert.ToInt32(MasterEnum.MRFType.Shortlist);
            }
            // 28495-Ambar-Start
            // Commented out following code
            //else
            //{
            //    mrfDetailobj.MRfType = Convert.ToInt32(MasterEnum.MRFType.Shortlist_and_make_anoffer);
            //}
            // 28495-Ambar-End

            if (!string.IsNullOrEmpty(txtSOWNo.Text))
            {
                mrfDetailobj.SOWNo = txtSOWNo.Text;
            }
            else
            {
                mrfDetailobj.SOWNo = "";
            }


            if (!string.IsNullOrEmpty(DtPckerSOWStartDt.Text.Trim()))
            {
                mrfDetailobj.SOWStartDate = Convert.ToDateTime(DtPckerSOWStartDt.Text);
            }
            else
            {
                mrfDetailobj.SOWStartDate = DateTime.MinValue;
            }

            if (!string.IsNullOrEmpty(DtPckSOWEndDt.Text.Trim()))
            {
                mrfDetailobj.SOWEndDate = Convert.ToDateTime(DtPckSOWEndDt.Text);
            }
            else
            {
                mrfDetailobj.SOWEndDate = DateTime.MinValue;
            }
            //Aarohi : Issue 31173 : 06/02/2012 : Start
            if (!string.IsNullOrEmpty(txtRequiredFrom.Text))
            {
                mrfDetailobj.ResourceOnBoard = Convert.ToDateTime(txtRequiredFrom.Text);
            }

            //Remove Till date validation 
            if (!string.IsNullOrEmpty(txtRequiredTill.Text))
            {
                mrfDetailobj.ResourceReleased = Convert.ToDateTime(txtRequiredTill.Text);
            }
            if (!string.IsNullOrEmpty(txtDateOfRequisition.Text))
            {
                mrfDetailobj.DateOfRequisition = Convert.ToDateTime(txtDateOfRequisition.Text);
            }
            //Aarohi : Issue 31173 : 06/02/2012 : End
            //Ishwar Patil 22/04/2015 Start
            TxtSkills.Text = hidSkillsName.Value;
            mrfDetailobj.MandatorySkillsID = hidSkills.Value;
            //Ishwar Patil 22/04/2015 End
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                CLASS_NAME_VIEWMRF, "GetControlValues", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }

        return mrfDetailobj;
    }
    /// <summary>
    /// Function will Get Move MRF Control Values for inserting new Record.
    /// </summary>
    /// <returns></returns>
    private BusinessEntities.MRFDetail GetMoveMRFControlValues(BusinessEntities.MRFDetail mrfDetailobj)
    {
        try
        {
            MRFSTATUS = mrfDetail.StatusId;
            txtMRFStatus.Value = ddlMRFStatus.SelectedItem.Text;
            //29998-Ambar-start-20092011
            string[] rolearr = ddlRole.SelectedItem.Text.Split(SPILITER_DASH);

            mrfDetailobj.RoleName = ddlRole.SelectedItem.Text;//rolearr[0].ToString();
            mrfDetailobj.Role = rolearr[1].ToString();
            //29998-Ambar-end-20092011     
            //mrfDetailobj.Role = ddlRole.SelectedItem.Text;
            mrfDetailobj.RoleId = Convert.ToInt32(ddlRole.SelectedValue);
            mrfDetailobj.DepartmentName = ddlDepartment.SelectedItem.Text;
            mrfDetailobj.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
            mrfDetailobj.MRFCode = txtMRFCode.Text.Trim();

            if (ddlDepartment.SelectedItem.Text == "Projects")
            {
                mrfDetailobj.ProjectId = Convert.ToInt32(ddlProjectName.SelectedValue);
                mrfDetailobj.ProjectName = ddlProjectName.SelectedItem.Text;
                mrfDetailobj.ResourcePlanId = Convert.ToInt32(ddlResourcePlanCode.SelectedValue);
            }
            else
            {
                mrfDetailobj.ProjectId = 0;
                mrfDetailobj.ProjectName = "SELECT";
                mrfDetailobj.ResourcePlanId = 0;
                //Poonam : Issue : Remove replica of MRF from grid while moving MRF : Starts
                mrfDetailobj.ResourcePlanDurationId = 0;
                //Poonam : Issue : End
            }
            mrfDetailobj.CommentMoveMRF = txtReasonMoveMRF.Text;

            if (mrfDetailobj.ProjectId > 0)
            {
                for (int i = 0; i < grdresource.Rows.Count; i++)
                {
                    CheckBox chkRPDuId = (CheckBox)grdresource.Rows[i].Cells[0].FindControl("chk");
                    if (chkRPDuId.Checked)
                    {
                        Label lblRPDurationId = (Label)grdresource.Rows[i].FindControl("lblRPDuId");
                        Label lblBilling = (Label)grdresource.Rows[i].FindControl("lblBilling");
                        Label lblUtilization = (Label)grdresource.Rows[i].FindControl("lblUtilization");
                        UIControls_DatePickerControl billingDatePicker = (UIControls_DatePickerControl)grdresource.Rows[i].Cells[5].FindControl("billingDatePicker");

                        mrfDetailobj.ResourcePlanDurationId = Convert.ToInt32(lblRPDurationId.Text);
                        mrfDetailobj.ResourceOnBoard = Convert.ToDateTime(grdresource.Rows[i].Cells[2].Text);
                        mrfDetailobj.ResourceReleased = Convert.ToDateTime(grdresource.Rows[i].Cells[3].Text);
                        mrfDetailobj.Billing = Convert.ToInt32(lblBilling.Text);
                        mrfDetailobj.Utilization = Convert.ToInt32(lblUtilization.Text);

                        if (billingDatePicker.Text != null && billingDatePicker.Text != "")
                        {
                            mrfDetailobj.BillingDate = Convert.ToDateTime(billingDatePicker.Text);
                        }
                        else
                        {
                            mrfDetailobj.BillingDate = new DateTime();
                        }
                    }
                }
            }

            mrfDetailobj.ReportingToId = hidResponsiblePersonName.Value.ToString();
            if (txtResponsiblePerson.Text != "" && txtResponsiblePerson.Text != null && txtResponsiblePerson.Text.Trim() != string.Empty)
            {
                mrfDetailobj.ReportingToEmployee = hidReportingToName.Value;
            }
            //Ishwar Patil 22/04/2015 Start
            mrfDetailobj.MandatorySkills = hidSkillsName.Value;
            //Ishwar Patil 22/04/2015 End
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_VIEWMRF, "GetControlValues", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }

        return mrfDetailobj;
    }

    /// <summary>
    /// Function will Get Abort Control Values
    /// </summary>
    /// <returns></returns>
    private BusinessEntities.MRFDetail GetAbortControlValues()
    {
        BusinessEntities.MRFDetail mrfDetailobj = new BusinessEntities.MRFDetail();
        #region Coded By Anuj
        //Done By Anuj
        //For Issue ID:20374
        //START
        mrfDetailobj.Role = hidrole.Value;
        //dept id for "projects is 1"
        if (hidDepartment.Value.Trim() == "Projects")
        {
            mrfDetailobj.ResourcePlanDurationId = Convert.ToInt32(hidResourcePlanDurationId.Value);
            mrfDetailobj.ResourcePlanId = Convert.ToInt32(ddlResourcePlanCode.SelectedItem.Value);
        }
        else
        {
            mrfDetailobj.ResourcePlanDurationId = 0;
            mrfDetailobj.ResourcePlanId = 0;
        }
        mrfDetailobj.MRFId = Convert.ToInt32(hidMRFID.Value);
        //END
        #endregion Coded by Anuj
        // 27632-Ambar-Start
        // Uncommented following to capture the reason and user
        // Move code here to get current user
        AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
        string UserMailId = objRaveHRAuthorizationManager.getLoggedInUserEmailId();
        mrfDetailobj.AbortReason = txtReasonMoveMRF.Text.Trim();
        mrfDetailobj.LoggedInUserEmail = UserMailId;
        // 27632-Ambar-End
        //mrfDetailobj.StatusId = Convert.ToInt32(MasterEnum.MRFStatus.Abort);
        return mrfDetailobj;
    }

    /// <summary>
    /// Function will use to Abort MRF Detail
    /// </summary>
    private void AbortMRFDetail()
    {
        int AbortMRFId;
        BusinessEntities.MRFDetail MRFDetailabortobj = new BusinessEntities.MRFDetail();

        try
        {
            //Declare the bool variable to find the MRF Type:
            Boolean isResourseRelease_E_MRF = new Boolean();
            string currStatusName;
            currStatusName = "";
            currStatusName = mrfDetail.StatusId.ToString();
            MRFDetailabortobj = GetAbortControlValues();
            //currStatusName = MRFDetailabortobj.StatusName.ToString();
            if (currStatusName != null)
            {
                if ((currStatusName.ToString().Equals("141")) || (currStatusName.ToString().Equals("98")))
                {
                    isResourseRelease_E_MRF = true;
                }
                else
                {
                    isResourseRelease_E_MRF = false;
                }
            }


            AbortMRFId = mRFDetail.AbortMRFBL(MRFDetailabortobj, Request.QueryString.ToString(), isResourseRelease_E_MRF);

            if (AbortMRFId == 0)
            {
                lblMessage.Text = "MRF Code [" + txtMRFCode.Text + "] Aborted Successfully";
                btnPrevious.Visible = true;
                btnNext.Visible = true;
                lblHeaderViewEdit.Text = ViewMRF;
                btnSavebtn.Visible = false;

                FillMRFDetail();
                txtReason.Enabled = false;
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_VIEWMRF, "AbortMRFDetail", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }

    }


    /// <summary>
    /// Function will use to Delete MRF Detail
    /// </summary>
    private void DeleteMRFDetail()
    {
        //int AbortMRFId;
        BusinessEntities.MRFDetail MRFDetaildeleteobj = new BusinessEntities.MRFDetail();

        try
        {
            //Done By Anuj
            //For Issue ID:20374
            //START
            MRFDetaildeleteobj = GetAbortControlValues();
            Rave.HR.BusinessLayer.MRF.MRFDetail DeleteMrf = new Rave.HR.BusinessLayer.MRF.MRFDetail();

            DeleteMrf.DeleteMRFBL(MRFDetaildeleteobj, (Convert.ToInt32(previousMRFStatus.Value)));
            //END
        }

           /*try
         {
           //Declare the bool variable to find the MRF Type:
           Boolean isResourseRelease_E_MRF = new Boolean();
           string currStatusName;
           currStatusName = "";
           currStatusName = mrfDetail.StatusId.ToString();
           MRFDetailabortobj = GetAbortControlValues();
           //currStatusName = MRFDetailabortobj.StatusName.ToString();
           if (currStatusName != null)
           {
             if ((currStatusName.ToString().Equals("141")) || (currStatusName.ToString().Equals("98")))
             {
               isResourseRelease_E_MRF = true;
             }
             else
             {
               isResourseRelease_E_MRF = false;
             }
           }


           AbortMRFId = mRFDetail.AbortMRFBL(MRFDetailabortobj, Request.QueryString.ToString(), isResourseRelease_E_MRF);

           if (AbortMRFId == 0)
           {
               Response.Redirect("MrfSummary.aspx");
             lblMessage.Text = "MRF Code [" + txtMRFCode.Text + "] Aborted Successfully";
             btnPrevious.Visible = true;
             btnNext.Visible = true;
             lblHeaderViewEdit.Text = ViewMRF;
             btnSavebtn.Visible = false;

            FillMRFDetail();
               txtReason.Enabled = false;
         
           }
         }*/
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_VIEWMRF, "DeleteMRFDetail", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// Function will use to enable or disable previous or next Buttons
    /// </summary>
    /// <param name="currentIndex"></param>
    /// <param name="PreviousIndex"></param>
    /// <param name="NextIndex"></param>
    private void EnableDisableButtons(int currentIndex, int PreviousIndex, int NextIndex)
    {
        try
        {
            Hashtable ht = new Hashtable();
            if (Session[SessionNames.MRFVIEWINDEX] != null)
            {

                // 27633-Ambar-Start
                //ht = (Hashtable)Session[SessionNames.MRFVIEWINDEX];
                ht = (Hashtable)Session[SessionNames.MRFPreviousHashTable];
                // 27633-Ambar-End

                if (ht.Contains(PreviousIndex) == true && Request.QueryString[CommonConstants._isAccessUrl] == null)
                {
                    btnPrevious.Visible = true;
                }
                else
                {
                    btnPrevious.Visible = false;
                }

                if (ht.Contains(NextIndex) == true && Request.QueryString[CommonConstants._isAccessUrl] == null)
                {
                    btnNext.Visible = true;

                }
                else
                {
                    btnNext.Visible = false;
                }

                if (ht.Contains(currentIndex) == true)
                {
                    if (Request.QueryString[CommonConstants._isAccessUrl] != null)
                    {
                        //IF MRF ID is not null
                        if (Request.QueryString[CommonConstants.MRF_ID] != null)
                        {
                            mrfDetail.MRFId = Convert.ToInt32(DecryptQueryString(QueryStringConstants.MRF_ID).ToString());
                            CopyMRFDetail(mrfDetail.MRFId);
                            ControlEnable(false);
                        }
                    }
                    else
                    {
                        hidMRFID.Value = Convert.ToString(ht[currentIndex]);
                        CopyMRFDetail(Convert.ToInt32(ht[currentIndex]));
                        ControlEnable(false);
                    }
                }
            }
            if (Request.QueryString[CommonConstants._isAccessUrl] != null)
            {
                btnPrevious.Visible = false;
                btnNext.Visible = false;
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_VIEWMRF, "EnableDisableButtons", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }
    /// <summary>
    /// Function will call on Privious Click
    /// </summary>
    private void PreviousClick()
    {
        MRFCURRENTCOUNT = MRFCURRENTCOUNT - 1;
        MRFPREVIOUSCOUNT = MRFPREVIOUSCOUNT - 1;
        MRFNEXTCOUNT = MRFNEXTCOUNT + 1;

        EnableDisableButtons(MRFCURRENTCOUNT, MRFPREVIOUSCOUNT, MRFNEXTCOUNT);
        btnDelete.Visible = false;
        if (Request.QueryString[PAGETYPE] != null)
        {
            if (DecryptQueryString(PAGETYPE) == PAGETYPE_PENDING_ALLOCATION)
            {
                btnDelete.Visible = true;
                btnEdit.Visible = false;
            }
            else
            {
                pnlViewMRF.Visible = true;
                pnlHeaderAllocateResource.Visible = false;
                pnlHeaderViewMRF.Visible = true;
                lblHeaderViewEdit.Text = ViewMRF;
            }
            #region Coded By Anuj
            //Done By Anuj
            //For Issue ID:20374
            //START
            if (txtMRFStatus.Value == MasterEnum.MRFStatus.PendingExternalAllocation.ToString() || txtMRFStatus.Value == MasterEnum.MRFStatus.PendingAllocation.ToString() || txtMRFStatus.Value == "Pending Allocation")
            {//Only Accessible To RPM Group
                if (ROLE == AuthorizationManagerConstants.ROLERPM)
                {
                    btnDelete.Visible = true;
                    btnDelete.Enabled = true;
                }
                else
                    btnDelete.Visible = false;
            }
            else
                btnDelete.Visible = false;

            //END
            #endregion Coded by Anuj

            /*
        #region Coded By Sameer
        //Done By Sameer
        //For Issue ID:18589
        //START
        if (txtMRFStatus.Value == CommonConstants.MRFStatus_Closed
           || txtMRFStatus.Value == CommonConstants.MRFStatus_Rejected
           || txtMRFStatus.Value == ABORTED
           || txtMRFStatus.Value == MasterEnum.MRFStatus.Replaced.ToString())
        {
          btnDelete.Visible = false;
        }
      /* else
       {
          //Only Accessible To RPM Group
          if (ROLE == AuthorizationManagerConstants.ROLERPM)
            btnDelete.Visible = true;
        }
       * 

        if (txtMRFStatus.Value.Trim() == ABORTED)
        {
          btnEdit.Enabled = false;
        }
        //END

        #endregion Coded By Sameer

      }*/
            //removed due to issue id 20374
        }

    }

    /// <summary>
    /// Function will call on Next Click
    /// </summary>
    private void NextClick()
    {
        btnDelete.Visible = false;
        MRFCURRENTCOUNT = MRFCURRENTCOUNT + 1;
        MRFPREVIOUSCOUNT = MRFPREVIOUSCOUNT + 1;
        MRFNEXTCOUNT = MRFNEXTCOUNT - 1;

        EnableDisableButtons(MRFCURRENTCOUNT, MRFPREVIOUSCOUNT, MRFNEXTCOUNT);

        if (Request.QueryString[PAGETYPE] != null)
        {
            if (DecryptQueryString(PAGETYPE) == PAGETYPE_PENDING_ALLOCATION)
            {

                btnEdit.Visible = false;

            }
            else
            {
                pnlViewMRF.Visible = true;
                pnlHeaderAllocateResource.Visible = false;
                pnlHeaderViewMRF.Visible = true;
                lblHeaderViewEdit.Text = ViewMRF;
            }

            #region Coded By Anuj
            //Done By Anuj
            //For Issue ID:20374
            //START
            if (txtMRFStatus.Value == MasterEnum.MRFStatus.PendingExternalAllocation.ToString() || txtMRFStatus.Value == MasterEnum.MRFStatus.PendingAllocation.ToString() || txtMRFStatus.Value == "Pending Allocation")
            {//Only Accessible To RPM Group
                if (ROLE == AuthorizationManagerConstants.ROLERPM)
                {
                    btnDelete.Visible = true;
                    btnDelete.Enabled = true;
                }
                else
                    btnDelete.Visible = false;
            }
            else
                btnDelete.Visible = false;

            //END
            #endregion Coded by Anuj


            /*
        #region Coded By Sameer
        //Done By Sameer
        //For Issue ID:18589
        //START
          

        if (txtMRFStatus.Value == CommonConstants.MRFStatus_Closed
           || txtMRFStatus.Value == CommonConstants.MRFStatus_Rejected
           || txtMRFStatus.Value == ABORTED
           || txtMRFStatus.Value == MasterEnum.MRFStatus.Replaced.ToString())
        {

          btnDelete.Visible = false;
        }
        else
        {
          //Only Accessible To RPM Group
          if (ROLE == AuthorizationManagerConstants.ROLERPM)
            btnDelete.Visible = true;
          btnDelete.Enabled = true;
        }

        if (txtMRFStatus.Value.Trim() == ABORTED)
        {
          btnEdit.Enabled = false;
        }
        //END
        #endregion Coded By Sameer
        */

        }

    }

    /// <summary>
    /// Function will GetLink
    /// </summary>
    /// <returns></returns>
    private string GetLink()
    {
        //Get Server URL
        string strPendingAlocationLink = Utility.GetUrl() + CommonConstants.Page_MrfPendingApproval;
        return strPendingAlocationLink;
    }

    /// <summary>
    /// Method to get SLA days for the recuriter by employee designation
    /// </summary>
    private string GetSLADaysByMrfId(int MRFID)
    {
        // Initialise Business layer object
        Rave.HR.BusinessLayer.MRF.MRFDetail objBLMrf = new Rave.HR.BusinessLayer.MRF.MRFDetail();

        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            BusinessEntities.MRFDetail mrfDetail = new MRFDetail();
            //mrfDetail.MRFId = int.Parse(hidMRFID.Value);
            mrfDetail.MRFId = MRFID;
            raveHRCollection = objBLMrf.GetSLADaysByMrfId(mrfDetail);

            //checks if object is not null
            if (raveHRCollection != null)
            {
                foreach (BusinessEntities.MRFDetail objMrfDetails in raveHRCollection)
                {
                    hidSLADays.Value = objMrfDetails.SLADays.ToString();
                }
            }
            return hidSLADays.Value;
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_VIEWMRF, "GetTargetClosureDateByMrfId", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// Fill Recruitment Manager dropdown
    /// </summary>
    private void FillRecruitmentManagerDropDown()
    {
        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        // Initialise Business layer object
        Rave.HR.BusinessLayer.MRF.MRFDetail employeeNameBL = new Rave.HR.BusinessLayer.MRF.MRFDetail();

        try
        {
            raveHRCollection = employeeNameBL.GetRecruitmentManager();

            if (raveHRCollection != null)
            {
                // Assign the data source to dropdown
                ddlRecruiterName.DataSource = raveHRCollection;

                ddlRecruiterName.DataTextField = CommonConstants.DDL_DataTextField;
                ddlRecruiterName.DataValueField = CommonConstants.DDL_DataValueField;

                // Bind the data to dropdown
                ddlRecruiterName.DataBind();

                // Default value of dropdown is "Select"
                ddlRecruiterName.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
            }
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_VIEWMRF, "FillRecruitmentManagerDropDown", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Method To Enable Fields Other Than Recruiters
    /// </summary>
    private void EnableFieldsExceptRescruiters(bool flag)
    {
        ddlMRFType.Enabled = flag;
        txtMustHaveSkills.Enabled = flag;
        txtGoodSkills.Enabled = flag;
        txtTools.Enabled = flag;
        txtExperince.Enabled = flag;
        ddlSkillsCategory.Enabled = flag;
        txtExperince1.Enabled = flag;
        txtHeighestQualification.Enabled = flag;
        txtSoftSkills.Enabled = flag;
        txtUtilijation.Enabled = flag;
        txtBilling.Enabled = flag;
        txtTargetCTC.Enabled = flag;
        txtTargetCTC1.Enabled = flag;
        txtRemarks.Enabled = flag;
        txtResourceResponsibility.Enabled = flag;
        imgResponsiblePersonSearch.Visible = flag;
        //Ishwar Patil 22/04/2015 Start
        imgSkillsSearch.Visible = flag;
        //Ishwar Patil 22/04/2015 End
        txtClientName.Enabled = flag;
        txtDomain.Enabled = flag;
        ddlColorCode.Enabled = flag;
    }

    /// <summary>
    /// Method To Diasable Fields For Recruiters
    /// </summary>
    private void DisableFieldsForRecruiters(bool flag)
    {
        ddlMRFType.Enabled = flag;
        txtMustHaveSkills.Enabled = flag;
        txtGoodSkills.Enabled = flag;
        txtTools.Enabled = flag;
        txtExperince.Enabled = flag;
        ddlSkillsCategory.Enabled = flag;
        txtExperince1.Enabled = flag;
        txtHeighestQualification.Enabled = flag;
        txtSoftSkills.Enabled = flag;
        txtUtilijation.Enabled = flag;
        txtBilling.Enabled = flag;
        txtTargetCTC.Enabled = flag;
        txtTargetCTC1.Enabled = flag;
        txtRemarks.Enabled = flag;
        txtResourceResponsibility.Enabled = flag;
        imgResponsiblePersonSearch.Visible = flag;
        //Ishwar Patil 22/04/2015 Start
        imgSkillsSearch.Visible = flag;
        //Ishwar Patil 22/04/2015 End
        txtClientName.Enabled = flag;
        txtDomain.Enabled = flag;
        ddlColorCode.Enabled = flag;

    }

    /// <summary>
    /// Fill DropDown Method
    /// </summary>
    private void GetPurposeDetails()
    {
        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        try
        {
            //Calling Business layer FillDropDown Method
            raveHRCollection = master.FillDropDownsBL(Convert.ToInt32(Common.EnumsConstants.Category.MRFPurpose));
            //Check Collection is null or not
            if (raveHRCollection != null)
            {
                ddlPurpose.BindDropdown(raveHRCollection);
                raveHRCollection.Clear();
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_VIEWMRF, "FillDropDown", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// Fills the status drop down.
    /// </summary>
    private void FillStatusDropDown(string CallingFrom)
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
                if (CallingFrom == "Edit_MRF" && ddlMRFStatus.SelectedValue == Convert.ToInt32(MasterEnum.MRFStatus.PendingExternalAllocation).ToString())
                {
                    // pending External Allocation allowed for all roles
                    if (Convert.ToInt32(keyValue.KeyName) == (int)MasterEnum.MRFStatus.PendingExternalAllocation)
                    {
                        newRaveHRCollection.Add(keyValue);
                    }
                    if (Convert.ToInt32(keyValue.KeyName) == (int)MasterEnum.MRFStatus.PendingAllocation)
                    {
                        // pending Allocation not Allowed for all RECRUITMENT
                        if (ROLE == AuthorizationManagerConstants.ROLERPM)
                        {
                            newRaveHRCollection.Add(keyValue);
                        }
                    }
                    if (Convert.ToInt32(keyValue.KeyName) == (int)MasterEnum.MRFStatus.MarketResearchCompleteAndClosed)
                    {
                        // MarketResearchCompleteAndClosed  Allowed only for all RECRUITMENT and ROLERPM
                        if ((ROLE == AuthorizationManagerConstants.ROLERPM) || (ROLE == AuthorizationManagerConstants.ROLERECRUITMENT))
                        {
                            newRaveHRCollection.Add(keyValue);
                        }
                    }

                    ddlMRFStatus.Enabled = true;
                }
                else
                {
                    newRaveHRCollection.Add(keyValue);
                    ddlMRFStatus.Enabled = false;
                }
            }

            if (newRaveHRCollection != null)
            {
                // Assign the data source to dropdown
                ddlMRFStatus.DataSource = newRaveHRCollection;

                ddlMRFStatus.DataTextField = CommonConstants.DDL_DataTextField;
                ddlMRFStatus.DataValueField = CommonConstants.DDL_DataValueField;

                // Bind the data to dropdown
                ddlMRFStatus.DataBind();
            }

        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_VIEWMRF, "FillStatusDropDown", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }
    //Siddhesh Arekar Issue ID : 55884 Closure Type
    private void SetTypeOfAllocation()
    {
        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        try
        {
            typeOfAllocation = master.GetMasterTypeDetails(Convert.ToInt32(Common.EnumsConstants.Category.TypeOfAllocation), "Current");
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                CLASS_NAME_VIEWMRF, "SetTypeOfAllocation", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }

    }

    // Bind data to Closure type DropDown
    private void FillClosureTypeDropDown()
    {
        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        try
        {
            //Calling Business layer FillDropDown Method
            raveHRCollection = master.FillDropDownsBL(Convert.ToInt32(Common.EnumsConstants.Category.TypeOfClosure));

            //Check Collection is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSOurce to Collection
                ddlClosureType.DataSource = raveHRCollection;

                //Assign DataText Filed to DropDown
                ddlClosureType.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign DataValue Field to Dropdown
                ddlClosureType.DataValueField = CommonConstants.DDL_DataValueField;

                //Bind Dropdown
                ddlClosureType.DataBind();

                //Insert Select as a item for dropdown
                ddlClosureType.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                raveHRCollection.Clear();
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                CLASS_NAME_VIEWMRF, "FillDropDown", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }
    //Siddhesh Arekar Issue ID : 55884 Closure Type

    //Vandana-Start
    public void DeleteUIFutureAllocateEmployee(int mrfid)
    {
        try
        {
            Rave.HR.BusinessLayer.MRF.MRFDetail objUIDeleteFutureAllocateEmployee =
                new Rave.HR.BusinessLayer.MRF.MRFDetail();

            objUIDeleteFutureAllocateEmployee.DeleteBLFutureAllocateEmployee(mrfid);

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer,
                CLASS_NAME_VIEWMRF, "DeleteDLFutureAllocateEmployee",
                EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
        }
    }
    //Vandana-End

    //Vandana-Start
    public void EditUIFutureAllocationEmployee(int mrftempid, string lint_type_of_allocation, string lint_type_of_supply, DateTime ldt_date_of_Allocation, int lstr_employeeeID)
    {
        try
        {
            Rave.HR.BusinessLayer.MRF.MRFDetail objUIEditFutureAllocateEmployee =
                new Rave.HR.BusinessLayer.MRF.MRFDetail();

            objUIEditFutureAllocateEmployee.EditBLFutureAllocationEmployee(mrftempid,
                                                                        lint_type_of_allocation,
                                                                        lint_type_of_supply,
                                                                        ldt_date_of_Allocation,
                                                                        lstr_employeeeID);

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer,
                CLASS_NAME_VIEWMRF, "EditFutureAllocateEmployee", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
        }
    }

    //Vandana-End

    //Vandana-start
    private void FutureAllocationResetControl()
    {
        pnlPendingAllocation.Visible = false;
        pnlInternalResourceDetails.Visible = false;
        pnlViewMRF.Visible = true;
        btnCancel.Visible = true;
        pnlHeaderAllocateResource.Visible = false;
        pnlHeaderViewMRF.Visible = true;
        //txtResourceName.Visible = false;
        //txtAllocationDate.Enabled = false;
        //btnSaveFutureAllocation.Visible = false;
        //btnAllocate.Visible = false;
        //btnRaiseHeadCount.Visible = false;
        //btnSelectInternalResource.Visible = false;
        //btnCancelAllocateResource.Visible = false;
        //btnSavebtn.Visible = true;
    }
    //Vandana-End

    /// <summary>
    /// Function will Fill Role Dropworn as per Selected Resource Plan
    /// </summary>
    private void FillRoleDropDown(int ResourcePlanCode, int ResourcePlanDurationStatus)
    {
        lblMessage.Text = "";

        try
        {
            //Calling Fill dropdown Business layer method to fill the dropdown
            raveHRCollection = mRFDetail.GetRoleResourcePlanWiseBL(ResourcePlanCode, ResourcePlanDurationStatus);

            //Check Collection object is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSource to dropdown
                ddlRole.DataSource = raveHRCollection;

                //Assign DataText Field to dropdown
                ddlRole.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign Data Value field to dropdown
                ddlRole.DataValueField = CommonConstants.DDL_DataValueField;

                //Bind Dropdown
                ddlRole.DataBind();

                //Insert Select as a Item for Dropdown
                ddlRole.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

            }

            /*This section is added by Gaurav Thakkar to fix the issue id 15085 as per discussion
              that a message will ne displayed when no roles are present for RP*/
            if (raveHRCollection.Count == 0)
            {
                lblMessage.Text = "All the roles are already allocated for selected RP.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                CLASS_NAME_VIEWMRF, "FillRoleDropDown(int ResourcePlanCode, int ResourcePlanDurationStatus)", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Function Will Fill Grid
    /// </summary>
    private void FillResourceGrid()
    {
        try
        {
            //Calling Fill dropdown Business layer method to fill the dropdown
            if (ddlResourcePlanCode.SelectedItem.Text != Common.CommonConstants.SELECT)
            {
                raveHRCollection = mRFDetail.GetResourceGridRoleWiseBL(Convert.ToInt32(ddlRole.SelectedValue), Convert.ToInt32(ddlResourcePlanCode.SelectedValue));

                //Check Collection object is null or not
                if (raveHRCollection != null)
                {
                    //Assign DataSource
                    grdresource.DataSource = raveHRCollection;

                    //Bind Dropdown
                    grdresource.DataBind();
                }
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                CLASS_NAME_VIEWMRF, "FillResourceGrid", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }


    }


    protected void txtSOWNo_TextChanged(object sender, EventArgs e)
    {

        if (txtSOWNo.Text.Length != 0)
        {
            mandmarkSowStartDt.Visible = true;
            mandmrkSOWEndDt.Visible = true;
        }
        else
        {
            mandmarkSowStartDt.Visible = false;
            mandmrkSOWEndDt.Visible = false;
        }

    }

    // Issue Id : 34229 STRAT CONCURRENCY HANDLED Mahendra
    private bool IsTokenValid()
    {
        bool result = double.Parse(hidToken.Value) == ((double)Session["NextToken"]);
        hidToken.Value = "";
        //SetToken(); 
        return result;
    }

    private void SetToken()
    {
        double next = random.Next();
        hidToken.Value = next + "";
        Session["NextToken"] = next;
    }
    // Issue Id : 34229 END CONCURRENCY HANDLED Mahendra

    #endregion

    // 35093-Ambar-Start
    #region 35093
    protected void TxtBilling_TextChanged(object sender, EventArgs e)
    {
        btnSavebtn.Enabled = false;
        if (txtBilling.Text != null && txtBilling.Text != "" && txtBilling.Text.Trim() != string.Empty)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtBilling.Text, "^[0-9]*$"))
            {
                if (Convert.ToInt16(txtBilling.Text) > 0)
                {
                    txtBillingDate.Visible = true;
                    lblBillingDate.Visible = true;
                    lblBillingDatemandatory.Visible = true;
                    txtBillingDate.TextBox.BorderColor = System.Drawing.Color.Gray;
                    txtBillingDate.TextBox.BorderWidth = 1;
                    txtBillingDate.IsEnable = true;
                }
                else
                {
                    txtBillingDate.Visible = false;
                    lblBillingDate.Visible = false;
                    lblBillingDatemandatory.Visible = false;
                    txtBillingDate.TextBox.BorderColor = System.Drawing.Color.Gray;
                    txtBillingDate.TextBox.BorderWidth = 1;
                    txtBillingDate.IsEnable = false;
                }
                //imgBilling.Visible = false;
                //txtBilling.BorderColor = System.Drawing.Color.Gray;
                //txtBilling.BorderWidth = 1;
            }
            //else
            //{
            //  imgBilling.Visible = true;
            //  txtBilling.BorderColor = System.Drawing.Color.Red;
            //  txtBilling.BorderWidth = 1;
            //}
        }
        //else
        //{
        //  imgBilling.Visible = true;
        //  txtBilling.BorderColor = System.Drawing.Color.Red;
        //  txtBilling.BorderWidth = 1;

        //}

        btnSavebtn.Enabled = true;
    }
    #endregion 35093
    // 35093-Ambar-End
    protected void ddlCostCode_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (Session[SessionNames.InternalResource] != null)
        {
            string strCostCode = "";

            //Rakesh : Actual vs Budget 22/06/2016 Begin  

            Rave.HR.BusinessLayer.MRF.MRFDetail mrfDetailsobj = new Rave.HR.BusinessLayer.MRF.MRFDetail();

            if (ddlProjectName.SelectedValue != null && ddlProjectName.SelectedValue != CommonConstants.SELECT && ddlCostCode.SelectedValue != null && ddlCostCode.SelectedValue != CommonConstants.SELECT)
                strCostCode = mrfDetailsobj.GET_EmployeeAllocation_CostCode(hidFutureEmpID.Value.CastToInt32(), ddlProjectName.SelectedValue.CastToInt32(),
                    0);
            //ddlCostCode.SelectedValue.CastToInt32()
            if (!string.IsNullOrEmpty(strCostCode))
            {
                rfCostCodeValidator.Visible = false;
                chkOverride.Text = string.Format("Employee Has Already Mapped with Cost Code '{0}' Do you want to Override Cost Code ?", strCostCode);
                divOverride.Visible = true;
            }
            else
            {
                rfCostCodeValidator.Visible = true;
                chkOverride.Text = "";
                divOverride.Visible = false;
            }

            //Rakesh : Actual vs Budget 22/06/2016 End
        }

    }
}
