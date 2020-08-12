
//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           AddContract.aspx.cs       
//  Author:         Kanchan.Singh
//  Date written:   10/08/2009 12:30:00 PM
//  Description:    The Add Contract page adds a new Contract to the system. It is also used to view,update,Delete.   //                    delete,approve or reject a project.
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  04/01/2009 10:00:00 AM   Kanchan.Singh    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Common.AuthorizationManager;
using Common.Constants;
using System.Globalization;

public partial class AddContract : BaseClass
{
    #region Private Field Members

    /// <summary>
    /// Rave Email domain
    /// </summary>
    /// 
    //Googleconfigurable
    //private const string RAVE_DOMAIN = "@rave-tech.com";

    //defined private field members.
    int contractId;

    string YES = "Yes";

    string NO = "No";

    string ACTIVE = "Active";

    string INACTIVE = "InActive";

    private const string SELECT = "Select";

    private const string DELETE = "Delete";

    private const string CONFIRM = "Confirm";

    private const string DELETECONTRACT = "Delete Contract";

    private const string SaveConst = "Save";

    private const string EDITCONTRACT = "Edit Contract";

    //Define the zero as string.
    private string ZERO = "0";

    //Define the select as string.
    private string SELECTONE = "Select";

    /// <summary>
    /// event name to capture event from query string
    /// </summary>
    string mode = Mode.Add.ToString();
    // string mode = null;    

    /// <summary>
    /// the dateformat of Date.
    /// </summary>
    const string dateFormat = "dd/MM/yyyy";

    private const string CLASS_NAME = "AddContract.aspx";

    DataSet dsProjDetails = new DataSet();

    public string EMailIdOfPM;

    private string CRDETAILSDELETE = "CRDetailsDelete";
    private string DELETEROWINDEX = "DeleteRowIndex";
    private string ROWINDEX = "RowIndex";
    private string IMGBTNDELETE = "imgDelete";
    private string IMGBTNEDIT = "imgEdit";

    public enum Mode
    {
        Add,
        View,
        Update,
        Delete
    }

    //Business entity of type contractProject is created.
    BusinessEntities.ContractProject ObjProject = null;

    Rave.HR.BusinessLayer.Contracts.ContractProject objContractProjectBL = null;

    Rave.HR.BusinessLayer.Contracts.Contract objContractBL = null;

    private static BusinessEntities.RaveHRCollection raveHRCollection;

    bool result = false;

    //Googleconfigurable
    //const string RAVEDOMAIN = "@rave-tech.com";

    const string CLASS_NAME_ADD_CONTRACT = "AddContract";

    const string MAILBODYINTERNAL = "GetContractMailBodyInternal";

    const string MAILBODYEXTERNAL = "GetContractMailBodyExternal";

    const string SENDMAIL = "SendMail";

    string SortExpression = null;

    string SortDir = null;

    bool isDatePassed = false;

    bool isEmployeesReleased = false;

    AuthorizationManager objAuMan = new AuthorizationManager();

    string clientName;

    string UserMailId;

    bool contractStatusChangeAllowed = false;

    string INDEX = "index";

    string ContractType = "5";

    string MasterName = "MasterName";

    string MasterID = "MasterID";

    string ContractStatus = "4";

    string ClientName = "3";

    string ProjectType = "21";

    string AccountManager = "1";

    string Currency = "61";

    string Location = "10";

    string ProjectLocation = "18";

    const string UPDATE = "Update";

    const string SAVE = "Save";

    const string ENGB = "en-GB";

    const string EDITCONTRACTCOMMAND = "EditContract";

    const string DELETECONTRACTCOMMAND = "DeleteContract";

    const string BACK = "Back";

    const string Contract = "Contract";

    const string QueryStringContractID = "ContractID";

    const string ADDCONTRACT = "Add Contract";

    const string ADDROW = "Add Row";

    string ProjectCategory = "16";

    const string ADDCR = "AddCR";


    #region Function Names

    const string FunctionPage_Load = "Page_Load";

    const string FunctionddlContractType_SelectedIndexChanged = "ddlContractType_SelectedIndexChanged";

    const string FunctionddlAccountManager_SelectedIndexChanged = "ddlAccountManager_SelectedIndexChanged";

    const string FunctionbtnAddNewPrj_Click = "btnAddNewPrj_Click";

    const string FunctionBtnSelectExistingPrj_Click = "BtnSelectExistingPrj_Click";

    const string FunctionbtnSave_Click = "btnSave_Click";

    const string FunctionbtnCancle_Click = "btnCancle_Click";

    const string FunctionbtnEdit_Click = "btnEdit_Click";

    const string FunctionbtnPrevious_Click = "btnPrevious_Click";

    const string FunctionbtnNext_Click = "btnNext_Click";

    const string FunctionbtnAddRow_Click = "btnAddRow_Click";

    const string FunctionbtnDelete_Click = "btnDelete_Click";

    //const string FunctionbtnAddRow_Click = "btnAddRow_Click";

    const string FunctionbtnPrjCancle_Click = "btnPrjCancle_Click";

    const string FunctiongvProductDetails_RowCommand = "gvProductDetails_RowCommand";

    const string FunctionddlTypeOfPrj_SelectedIndexChanged = "ddlTypeOfPrj_SelectedIndexChanged";

    const string FunctionGetMasterData_ContractTypeDropDown = "GetMasterData_ContractTypeDropDown";

    const string FunctionGetMasterData_LocationDropDown = "GetMasterData_LocationDropDown";

    const string FunctionGetMasterData_ContractStatus = "GetMasterData_ContractStatus";

    const string FunctionGetMasterData_ClientName = "GetMasterData_ClientName";

    const string FunctionGetMasterData_TypeOfProject = "GetMasterData_TypeOfProject";

    const string FunctionGetMasterData_AccountManager = "GetMasterData_AccountManager";

    const string FunctionGetContractControldata = "GetContractControldata";

    const string FunctionSave = "Save";

    const string FunctionViewContractDetails = "ViewContractDetails";

    const string FunctionBindDataToContract = "BindDataToContract";

    const string FunctionBindDataToProject = "BindDataToProject";

    const string FunctionClearControls = "ClearControls";

    const string FunctionReadOnlyControls = "ReadOnlyControls";

    const string FunctionRemoveReadOnlyControls = "RemoveReadOnlyControls";

    const string FunctionBindDataToProjectControls = "BindDataToProjectControls";

    const string FunctionUnhideProjectControls = "UnhideProjectControls";

    const string FunctionHideProjectControls = "HideProjectControls";

    const string FunctionEditContract = "EditContract";

    const string FunctionDeleteContract = "DeleteContract";

    const string FunctionGetContractSubject = "GetContractSubject";

    const string FunctionGetContractSubjectDelete = "GetContractSubjectDelete";

    const string FunctionGetContractSubjectInternal = "GetContractSubjectInternal";

    const string FunctionGetContractSubjectEdit = "GetContractSubjectEdit";

    const string FunctionGetContractMailBodyInternal = "GetContractMailBodyInternal";

    const string FunctionGetContractMailBodyExternal = "GetContractMailBodyExternal";

    const string FunctionGetContractMailBodyEdit = "GetContractMailBodyEdit";

    const string FunctionGetContractMailBodyDelete = "GetContractMailBodyDelete";

    const string FunctionGetLink = "GetLink";

    const string FunctionSendMailMesssage = "SendMailMesssage";

    const string FunctionCompairDateOfProjectEndDt = "CompairDateOfProjectEndDt";

    const string FunctionCheckEmpAllReleased = "CheckEmpAllReleased";

    const string FunctionEnableDisableButtons = "EnableDisableButtons";

    const string FunctionPreviousClick = "PreviousClick";

    const string FunctionNextClick = "NextClick";

    const string FunctionSetContractIndex = "SetContractIndex";

    const string FunctioncheckProjectName = "checkProjectName";

    const string FunctionGetMasterData_CurrencyType = "GetMasterData_CurrencyType";

    const string FunctionGetDomainUsers = "GetDomainUsers";

    const string FunctionConvertToUpper = "ConvertToUpper";

    const string FunctionClearProjectDetailsControls = "ClearProjectDetailsControls";

    const string FunctionConvertDatatableToList = "ConvertDatatableToList";

    const string FunctioncheckClientName = "checkClientName";

    const string FunctiongetClientNameByProjectCode = "getClientNameByProjectCode";

    const string FunctionGetProjectControldata = "GetProjectControldata";

    const char Splitter = '/';

    const string FunctionGetMasterData_ProjectCategory = "GetMasterData_ProjectCategory";

    const string FunctioncheckProjectCode = "checkProjectCode";

    #endregion

    #endregion

    #region Public variable

    ArrayList addProjectDetails = new ArrayList();

    //Business entity of type contract is created.
    BusinessEntities.Contract objViewContract = null;

    List<BusinessEntities.ContractProject> objContractProject = null;
    private string CRDETAILS = "CRDetails";

    private string dateErrorMsg = "Project dates must match with Contract dates";

    private static Rave.HR.BusinessLayer.Contracts.ContractProject contractprojectBL = null;

    private static Rave.HR.BusinessLayer.Contracts.Contract contractBL = null;

    /// <summary>
    /// Property for CONTRACTPREVIOUSCOUNT
    /// </summary>
    public int CONTRACTPREVIOUSCOUNT
    {
        get
        {
            if (ViewState["CONTRACTPREVIOUSCOUNT"] != null)
            {
                return Convert.ToInt32(ViewState["CONTRACTPREVIOUSCOUNT"]);
            }
            else
            {
                return -1;
            }
        }
        set
        {
            ViewState["CONTRACTPREVIOUSCOUNT"] = value;
        }
    }

    /// <summary>
    /// Property for CONTRACTNEXTCOUNT
    /// </summary>
    public int CONTRACTNEXTCOUNT
    {
        get
        {
            if (ViewState["CONTRACTNEXTCOUNT"] != null)
            {
                return Convert.ToInt32(ViewState["CONTRACTNEXTCOUNT"]);
            }
            else
            {
                return -1;
            }
        }
        set
        {
            ViewState["CONTRACTNEXTCOUNT"] = value;
        }
    }

    /// <summary>
    ///  Property for CONTRACTCURRENTCOUNT
    /// </summary>
    public int CONTRACTCURRENTCOUNT
    {
        get
        {
            if (ViewState["CONTRACTCURRENTCOUNT"] != null)
            {
                return Convert.ToInt32(ViewState["CONTRACTCURRENTCOUNT"]);
            }
            else
            {
                return -1;
            }
        }
        set
        {
            ViewState["CONTRACTCURRENTCOUNT"] = value;
        }
    }

    NumberStyles style;

    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");


    /// <summary>
    /// Gets or sets the CR details collection.
    /// </summary>
    /// <value>The CR details collection.</value>
    private BusinessEntities.RaveHRCollection CRDetailsCollection
    {
        get
        {
            if (ViewState[CRDETAILS] != null)
                return ((BusinessEntities.RaveHRCollection)ViewState[CRDETAILS]);
            else
                return null;
        }
        set
        {
            ViewState[CRDETAILS] = value;
        }
    }

    #endregion

    #region protected methode

    /// <summary>
    /// On Contract Page load, all the dropdownlists are filled.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Javascript Function Call


        btnDelete.Attributes.Add(CommonConstants.EVENT_ONCLICK, "javascript:return SureTODelete();");

        // opens the Popup for selecting Existing projects.
        //Umesh: Issue 'Modal Popup issue in chrome' Starts
        //BtnSelectExistingPrj.Attributes.Add(CommonConstants.EVENT_ONCLICK, "return SelectProjectDetails();");
        //Umesh: Issue 'Modal Popup issue in chrome' Ends

        //highlights the fied when no data is entered.
        btnSave.Attributes.Add(CommonConstants.EVENT_ONCLICK, "javascript:return ButtonClickValidateContract();");
        btnAddRow.Attributes.Add(CommonConstants.EVENT_ONCLICK, "javascript:return ButtonClickValidate();");
        btnAddCRRow.Attributes.Add(CommonConstants.EVENT_ONCLICK, "javascript:return CRButtonClickValidate();");
        btnUpdateCRRow.Attributes.Add(CommonConstants.EVENT_ONCLICK, "javascript:return CRButtonClickValidate();");

        //ucContractStartDate.TextBox.Attributes.Add("onchange", "ValidateTextBoxControl('" + ucContractStartDate.ClientID + "' ); CheckDates('" + ucContractStartDate.ClientID + "', '" + ucContractEndDate.ClientID + "', '" + imgErrorContractStartDate.ClientID + "', '" + imgErrorContractEndDate.ClientID + "')");
        //imgErrorContractStartDate.Attributes.Add("onmouseover", "return ShowTooltip('" + SpanErrorContractStartDate.ClientID + "', '" + CommonConstants.MSG_DATERANGE + "')");
        //imgErrorContractStartDate.Attributes.Add("onmouseout", "return HideTooltip('" + SpanErrorContractStartDate.ClientID + "')");

        //ucContractEndDate.TextBox.Attributes.Add("onchange", "ValidateTextBoxControl('" + ucContractEndDate.ClientID + "'); CheckDates('" + ucContractStartDate.ClientID + "', '" + ucContractEndDate.ClientID + "', '" + imgErrorContractStartDate.ClientID + "', '" + imgErrorContractEndDate.ClientID + "')");
        //imgErrorContractEndDate.Attributes.Add("onmouseover", "return ShowTooltip('" + SpanErrorContractEndDate.ClientID + "', '" + CommonConstants.MSG_DATERANGE + "')");
        //imgErrorContractEndDate.Attributes.Add("onmouseout", "return HideTooltip('" + SpanErrorContractEndDate.ClientID + "')");

        // txtNoOfResources.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + txtNoOfResources.ClientID + "','" + imgNoOfResources.ClientID + "','" + CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
        imgNoOfResources.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanzNoOFResource.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgNoOfResources.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanzNoOFResource.ClientID + "');");

        //txtContractrefId.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtContractrefId.ClientID + "','" + imgContractrefId.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_WITDSPACE + "');");
        imgContractrefId.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanContractrefId.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgContractrefId.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanContractrefId.ClientID + "');");

        txtDocumentName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + txtDocumentName.ClientID + "','','');");
        txtPrjName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + txtPrjName.ClientID + "','','');");
        //validation for all drop downs.
        ddlAccountManager.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzAccountManager.ClientID + "','','');");
        ddlContractType.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzContractType.ClientID + "','','');");
        ddlContractStatus.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzContractStatus.ClientID + "','','');");
        ddlClientName.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzClientName.ClientID + "','','');");
        ddlLocation.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzLocation.ClientID + "','','');");

        //Siddharth 13 March 2015 Start
        ddlContractType.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzProjectModel.ClientID + "','','');");
        //Siddharth 13 March 2015 End

        //Siddharth 8 Sept 2015 Start
        ddlBusinessVertical.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + SpanBusinessVertical.ClientID + "','','');");
        //Siddharth 8 Sept 2015 End


        tbReasonForDeletion.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return MultiLineTextBox('" + tbReasonForDeletion.ClientID + "','" + tbReasonForDeletion.MaxLength + "','" + imgReasonForDeletion.ClientID + "');");

        imgReasonForDeletion.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanReasonForDeletion.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgReasonForDeletion.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanReasonForDeletion.ClientID + "');");

        txtDescription.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return MultiLineTextBox('" + txtDescription.ClientID + "','" + txtDescription.MaxLength + "','" + imgDescription.ClientID + "');");
        imgDescription.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanimgDescription.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgDescription.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanimgDescription.ClientID + "');");

        #endregion Javascript Function Call

        if (!ValidateURL())
        {
            Response.Redirect(CommonConstants.INVALIDURL, false);
        }
        else
        {

            //Siddharth 26th August 2015 Start
            //Task ID:- 56487 Hide the pages access for normal employees
            string UserRaveDomainId;
            ArrayList arrRolesForUser = new ArrayList();
            AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
            UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
            UserMailId = UserRaveDomainId.Replace("co.in", "com");
            AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
            arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());

            if (!(arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM) ||
                 arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEFM)))
            {
                Response.Redirect(CommonConstants.PAGE_HOME, true);
            }
            //Siddharth 26th August 2015 End



            //gets the logged in user id.
            AuthorizationManager authoriseduser = new AuthorizationManager();
            UserMailId = authoriseduser.getLoggedInUserEmailId();
            IsProjectDetailsVisible.Value = NO;
            lblMandatory.Visible = true;
            lblMandatory.Text = "";
            MandatoryEmailId.Visible = false;
            lblProjectCode.Visible = false;
            txtProjectCode.Visible = false;
            if (lblAddContract.Text == ADDCONTRACT)
            {
                lblContractStatus.Visible = false;
                ddlContractStatus.Visible = false;
                lblContractCode.Visible = false;
                txtContractCode.Visible = false;
            }

            try
            {
                if (Session[SessionNames.CONTRACT_SELECETED_PROJECT] != null)
                {
                    string ProCodeFromListOfProject = Session[SessionNames.CONTRACT_SELECETED_PROJECT].ToString();
                    Session[SessionNames.CONTRACT_SELECETED_PROJECT] = null;

                    BusinessEntities.ContractProject ProjectDetails_ID = new BusinessEntities.ContractProject();

                    objContractProjectBL = new Rave.HR.BusinessLayer.Contracts.ContractProject();

                    ObjProject = new BusinessEntities.ContractProject();
                    ObjProject.ProjectCode = ProCodeFromListOfProject;

                    //Gets the project details for the selected record.
                    ProjectDetails_ID = objContractProjectBL.GetProjectDetailsByProjectID(ObjProject);

                    btnAddNewPrj_Click(null, null);
                    //here set no bcz i want to check this condition on add row btn for existing project or new project.
                    IsNewProject.Value = NO;
                    //Binds data to the project details controls.
                    BindDataToProjectControls(ProjectDetails_ID);

                }

                if (!IsPostBack)
                {
                    GetMasterData_ContractTypeDropDown();
                    GetMasterData_ContractStatus();
                    GetMasterData_LocationDropDown();
                    GetMasterData_ClientName();
                    GetMasterData_AccountManager();
                    GetMasterData_TypeOfProject();
                    GetMasterData_CurrencyType();
                    GetMasterData_ProjectCategory();
                    // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
                    // Desc : Add project group in Contract page
                    GetMasterData_ProjectGroupDropDown();
                    // Mohamed : Issue 49791 : 15/09/2014 : Ends
                    // Mohamed : Issue  : 23/09/2014 : Starts                        			  
                    // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
                    GetMasterData_ProjectDivisionDropDown();
                    GetMasterData_ProjectBussinessAreaDropDown();
                    GetMasterData_ProjectBussinessSegmentDropDown();
                    // Mohamed : Issue  : 23/09/2014 : Ends


                    //Siddharth 13th March 2015 Start
                    GetMasterData_ProjectModelDropDown();
                    //Siddharth 13th March 2015 End

                    //Siddharth 28th August 2015 Start
                    GetMasterData_BusinessVerticalDropDown();
                    //Siddharth 28th August 2015 End



                    //Rakesh : HOD for Employees 11/07/2016 Begin   
                    BindProject_Head_Dropdown();
                    //Rakesh : HOD for Employees 11/07/2016 End   



                    SetContractIndex();

                    //Clear the add project grid data from session to add new fresh project.
                    Session.Remove(SessionNames.CONTRACT_PROJECT_DATA);

                    if (Request.QueryString[QueryStringContractID] != null)
                    {
                        mode = Mode.View.ToString();
                        GetMasterData_ContractTypeDropDown();
                    }

                }

                if (mode == Mode.View.ToString())
                {
                    Page.Title = Contract;

                    gvProjectDetails.Visible = true;
                    contractId = int.Parse(DecryptQueryString(QueryStringContractID));

                    ViewContractDetails(contractId, SortDir, SortExpression);
                    HideUnHideCRDetails(false);
                    PopulateGrid(contractId);
                    gvCRDetails.Columns[6].Visible = false;
                    gvCRDetails.Columns[7].Visible = false;

                    ReadOnlyControls();

                    BtnSelectExistingPrj.Visible = false;
                    btnAddNewPrj.Visible = false;

                }

                if (mode == Mode.Update.ToString())
                {
                }
            }
            catch (RaveHRException ex)
            {
                LogErrorMessage(ex);
            }
            catch (Exception ex)
            {
                RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionPage_Load, EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
                LogErrorMessage(objEx);
            }
        }
    }

    /// <summary>
    /// ddlContractType's Selected Index Changed event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlContractType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblConfirmMessage.Text = string.Empty;
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionddlContractType_SelectedIndexChanged, EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// ddlAccountManager's Selected Index Changed event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void ddlAccountManager_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblConfirmMessage.Text = string.Empty;
            Rave.HR.BusinessLayer.Contracts.Contract ContractBL = new Rave.HR.BusinessLayer.Contracts.Contract();

            if (ddlAccountManager.SelectedIndex == 0)
            {
                txtEmailId.Text = string.Empty;
            }
            else
            {
                int empId = Convert.ToInt32(ddlAccountManager.SelectedValue);
                string emailID = ContractBL.GetEmailID(empId);
                txtEmailId.Text = emailID;
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionddlAccountManager_SelectedIndexChanged, EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// btnAddNewPrj_Click event 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void btnAddNewPrj_Click(object sender, EventArgs e)
    {
        try
        {

            lblConfirmMessage.Text = string.Empty;
            //to make Invisible to Project details on page load.

            PanelProjectDetails.Visible = true;

            //highlightes the border area.
            divProjectDetails.Style.Add("<border:black>", "Solid");
            IsProjectDetailsVisible.Value = YES;
            IsNewProject.Value = YES;
            lblTypeOfProj.Visible = true;
            lblPrjName.Visible = true;
            lblNoOfResources.Visible = true;
            // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
            // Desc : Add project group in Contract page
            lblPrjGroup.Visible = true;
            MandatoryProjGroup.Visible = true;
            // Mohamed : Issue 49791 : 15/09/2014 : Ends
            lblStartDate.Visible = true;
            lblEndDate.Visible = true;
            lblPrjLocation.Visible = true;
            LabelProjectDescription.Visible = true;
            lblProjectCategory.Visible = true;

            txtPrjName.Visible = true;

            txtNoOfResources.Visible = true;
            txtPrjName.Enabled = true;
            txtDescription.Visible = true;

            ucDatePickerEnd.Visible = true;
            ucDatePickerStart.Visible = true;
            ucDatePickerStart.IsEnable = true;
            ucDatePickerEnd.IsEnable = true;

            ddlPrjLocation.Visible = true;
            ddlTypeOfPrj.Visible = true;
            ddlPrjLocation.ClearSelection();
            ddlTypeOfPrj.ClearSelection();
            ddlPrjLocation.Enabled = true;
            ddlTypeOfPrj.Enabled = true;
            // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
            // Desc : Add project group in Contract page
            ddlPrjGroup.Visible = true;
            ddlPrjGroup.ClearSelection();
            ddlPrjGroup.Enabled = true;
            // Mohamed : Issue 49791 : 15/09/2014 : Ends

            // Mohamed : Issue  : 23/09/2014 : Starts                        			  
            // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page
            ddlPrjDivision.Visible = true;
            ddlPrjDivision.ClearSelection();
            ddlPrjDivision.Enabled = true;
            lblPrjDivision.Visible = true;
            MandatoryProjectjDivision.Visible = true;

            //txtPrjAlias.Visible = true;
            //txtPrjAlias.Enabled = true;
            lblPrjBusinessArea.Visible = true;
            MandatoryProjectjBusinessArea.Visible = true;

            ddlPrjBusinessArea.Visible = true;
            ddlPrjBusinessArea.ClearSelection();
            lblPrjBusinessSegment.Visible = true;
            MandatoryProjectBusinessSegment.Visible = true;

            ddlPrjBusinessSegment.Visible = true;
            ddlPrjBusinessSegment.ClearSelection();
            //lblPrjAlias.Visible = true;
            //MandatoryPrjAlias.Visible = true;
            // Mohamed : Issue  : 23/09/2014 : Ends

            ddlProjectCategory.Visible = true;
            ddlProjectCategory.ClearSelection();
            ddlProjectCategory.Enabled = true;

            MandatoryProjectCategory.Visible = true;

            //Siddharth 9th Sept 2015 Start
            LblBusinessVertical.Visible = true;
            ddlBusinessVertical.Visible = true;
            ddlBusinessVertical.ClearSelection();
            ddlBusinessVertical.Enabled = true;
            lblMandatorymarkBusinessVertical.Visible = true;
            LblProjectModel.Visible = true;
            ddlProjectModel.Visible = true;
            ddlProjectModel.ClearSelection();
            ddlProjectModel.Enabled = true;
            lblMandatorymarkProjectModel.Visible = true;
            //Siddharth 9th Sept 2015 End

            btnAddNewPrj.Visible = true;

            btnAddRow.Visible = true;
            btnPrjCancle.Visible = true;

            //Setting visiblity of all the project details mandatory mark to true.

            MandatoryProjectName.Visible = true;
            MandatoryTypeOfProj.Visible = true;
            MandatoryStartDate.Visible = true;
            MandatoryEndDate.Visible = true;
            //ControlForCFM();
            // 36233-Ambar-29062012-Start
            // Uncomment following 
            mandatoryProjectDescription.Visible = true;
            // 36233-Ambar-29062012-End

            txtPrjName.Text = string.Empty;
            ucDatePickerEnd.Text = string.Empty;
            ucDatePickerStart.Text = string.Empty;
            txtNoOfResources.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtProjectAbbr.Visible = true;
            lblProjectAbbr.Visible = true;
            lblMandatoryProjectAbbr.Visible = true;
            lblPhase.Visible = true;
            txtPhase.Visible = true;
            lblMandatorymarkPhase.Visible = true;

            //Rakesh : HOD for Employees 11/07/2016 Begin   
            trProjectHead.Visible = true;
            //Rakesh : HOD for Employees 11/07/2016 End  
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {

            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionbtnAddNewPrj_Click, EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);

            LogErrorMessage(objEx);
        }

    }
    /// <summary>
    /// BtnSelectExistingPrj_Click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void BtnSelectExistingPrj_Click(object sender, EventArgs e)
    {
        try
        {
            IsNewProject.Value = NO;
            lblConfirmMessage.Text = string.Empty;
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionBtnSelectExistingPrj_Click, EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// btnSave_Click event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //Checking for the modes.        
        if (btnSave.Text == DELETE)
        {
            btnDelete.Visible = true;
            btnDelete.Width = 118;
            btnDelete.Text = CONFIRM;
            btnSave.Visible = false;
            mode = Mode.Delete.ToString();
            btnEdit.Visible = false;
            btnPrevious.Visible = false;
            btnNext.Visible = false;

            lblReasonOfDeletion.Visible = true;
            tbReasonForDeletion.Visible = true;

            lblReasonOfDeletion.Visible = true;
            MandatoryReasonOfDeletion.Visible = true;

            lblAddContract.Text = DELETECONTRACT;

        }
        if (btnSave.Text == SAVE && lblAddContract.Text == EDITCONTRACT)
        {
            mode = Mode.Update.ToString();
        }
        string prjLocation = ddlPrjLocation.SelectedItem.Text.ToString();
        try
        {
            DataTable contractProject = new DataTable();
            BusinessEntities.Contract addContractDetails = new BusinessEntities.Contract();

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();
            contractProject = (DataTable)Session[SessionNames.CONTRACT_PROJECT_DATA];

            if (checkClientName(contractProject))
            {

                #region Add Mode
                if (mode == Mode.Add.ToString())
                {
                    addContractDetails = GetContractControldata();
                    addContractDetails.ContractStatus = (Convert.ToInt32(MasterEnum.ContractStatus.Active)).ToString();

                    int contractID = 0;
                    contractID = Save(addContractDetails, contractProject);
                    if (contractID != 0)
                        result = true;
                    gvProjectDetails.Dispose();
                    gvProjectDetails.Columns.Clear();
                    addProjectDetails = null;
                    contractProject = null;
                    Session[SessionNames.CONTRACT_PROJECT_DATA] = null;
                    if (result == true)
                    {
                        if (contractProject == null)
                            lblConfirmMessage.Text = "Contract is created successfully, email notification is sent.";
                        else
                            lblConfirmMessage.Text = "Contract is created along with project successfully, email notification is sent.";

                        lblConfirmMessage.Text = "<font color=Blue>" + lblConfirmMessage.Text + "</font>";
                    }
                }
                #endregion Add Mode

                #region Update Mode
                if (mode == Mode.Update.ToString())
                {
                    bool projectSave = true;
                    bool save = false;
                    //gets all the data for the contract details controls. 

                    addContractDetails = GetContractControldata();

                    //Declares a list object to hold the data grid value.

                    List<BusinessEntities.ContractProject> ProjectDetails = new List<BusinessEntities.ContractProject>();

                    //Stores the session data for the grid in the variable.

                    DataTable projectData = new DataTable();
                    projectData = (DataTable)Session[SessionNames.CONTRACT_PROJECT_DATA];

                    ProjectDetails = ConvertDatatableToList(projectData);

                    isDatePassed = CompairDateOfProjectEndDt(objContractProject);
                    isEmployeesReleased = CheckEmpAllReleased(objContractProject);

                    //addContractDetails.ProjectCodeAbbreviation = txtClientAbbr.Text.Trim() + "_" + txtProjectAbbr.Text.Trim() + "_" + txtPhase.Text.Trim();

                    #region Inactive Status

                    if (ddlContractStatus.SelectedItem.Text == INACTIVE)
                    {

                        if (isDatePassed == true || isEmployeesReleased == true)
                        {
                            //If any project details are disassociated then it will mbe disassociated from that project 
                            //from this block of code.

                            if (Session[SessionNames.CONTRACT_DISASSOCIATED_PROJECT] != null)
                            {
                                //declares an object of BusinessLayer. 
                                contractprojectBL = new Rave.HR.BusinessLayer.Contracts.ContractProject();

                                //Assigns the value from session the business entity variable.
                                ObjProject = (BusinessEntities.ContractProject)Session[SessionNames.CONTRACT_DISASSOCIATED_PROJECT];

                                //Calls the BL layer Function to disassociate the project from the contract.
                                contractprojectBL.DisassociateProject(ObjProject, addContractDetails);

                                //Clears the session.
                                Session[SessionNames.CONTRACT_DISASSOCIATED_PROJECT] = null;
                            }

                            //Saves the updated data for the project details.
                            foreach (BusinessEntities.ContractProject details in ProjectDetails)
                            {
                                //calls the function to edit the data For the Project details.
                                contractprojectBL = new Rave.HR.BusinessLayer.Contracts.ContractProject();
                                details.ContractID = addContractDetails.ContractID;
                                projectSave = contractprojectBL.Edit(details, prjLocation);
                            }
                            //calls the function to edit the data For the Contract details.
                            contractBL = new Rave.HR.BusinessLayer.Contracts.Contract();
                            save = contractBL.Edit(addContractDetails, projectData, CRDetailsCollection);


                            //Makes all the fields on edit contract page read only.                       
                            ReadOnlyControls();

                            addProjectDetails = null;
                            contractProject = null;
                            if (save == true && projectSave == true)
                            {
                                lblConfirmMessage.Text = "Contract and project details have been sucessfully updated, email notification is sent.";

                                // 36041-Ambar-Start-21062012
                                lblConfirmMessage.Text = "<font color=Blue>" + lblConfirmMessage.Text + "</font>";
                                // 36041-Ambar-End-21062012
                            }
                        }

                        else
                        {
                            lblMandatory.Visible = true;

                            ddlContractStatus.ClearSelection();
                            ddlContractStatus.Items.FindByText(ACTIVE).Selected = true;
                            lblMandatory.Text = "Contract Not Saved.Contract Status cannot be changed to InActive as projects associated with contract is not reached their end date or allocated employees are not released.";
                            lblMandatory.Text = "<font color=RED>" + lblMandatory.Text + "</font>";
                        }
                    }
                    #endregion Inactive Status

                    #region Active Status
                    else if (ddlContractStatus.SelectedItem.Text == ACTIVE)
                    {
                        //If any project details are disassociated then it will mbe disassociated from that project 
                        //from this block of code.

                        if (Session[SessionNames.CONTRACT_DISASSOCIATED_PROJECT] != null)
                        {
                            //declares an object of BusinessLayer. 
                            contractprojectBL = new Rave.HR.BusinessLayer.Contracts.ContractProject();

                            //Assigns the value from session the business entity variable.
                            ObjProject = (BusinessEntities.ContractProject)Session[SessionNames.CONTRACT_DISASSOCIATED_PROJECT];

                            //Calls the BL layer Function to disassociate the project from the contract.
                            contractprojectBL.DisassociateProject(ObjProject, addContractDetails);

                            //Clears the session.
                            Session[SessionNames.CONTRACT_DISASSOCIATED_PROJECT] = null;
                        }

                        //Saves the updated data for the project details.
                        foreach (BusinessEntities.ContractProject details in ProjectDetails)
                        {
                            //calls the function to edit the data For the Project details.
                            contractprojectBL = new Rave.HR.BusinessLayer.Contracts.ContractProject();
                            details.ContractID = addContractDetails.ContractID;
                            if (details.ProjectCode != "")
                            {
                                projectSave = contractprojectBL.Edit(details, prjLocation);
                            }
                            else
                            {
                                details.ClientName = addContractDetails.ClientName;
                                details.CreatedByEmailId = addContractDetails.CreatedByEmailId;
                                //Added line to implement the new project code
                                details.ProjectCodeAbbreviation = txtClientAbbr.Text.Trim() + "_" + txtProjectAbbr.Text.Trim() + "_" + txtPhase.Text.Trim();
                                string ProjectCodeAbbreviation = string.Empty;
                                projectSave = contractprojectBL.Save(details, ref ProjectCodeAbbreviation);
                            }
                        }
                        //calls the function to edit the data For the Contract details.
                        contractBL = new Rave.HR.BusinessLayer.Contracts.Contract();
                        save = EditContract(addContractDetails, projectData, CRDetailsCollection);
                        addProjectDetails = null;
                        contractProject = null;

                        BusinessEntities.RaveHRCollection objDeleteCRDetailsCollection = (BusinessEntities.RaveHRCollection)ViewState[CRDETAILSDELETE];

                        if (CRDetailsCollection != null && CRDetailsCollection.Count > 0)
                            contractBL.Manipulation(CRDetailsCollection);

                        if (objDeleteCRDetailsCollection != null && objDeleteCRDetailsCollection.Count > 0)
                            contractBL.Manipulation(objDeleteCRDetailsCollection);

                    }
                    #endregion Active Status

                    HideProjectControls();

                    HideUnHideCRDetails(false);
                    ClearCRControls();
                    gvCRDetails.DataSource = null;
                    gvCRDetails.DataBind();
                }
                #endregion Update Mode
            }

        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionbtnSave_Click, EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }


    /// <summary>
    /// btnCancle_Click event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void btnCancle_Click(object sender, EventArgs e)
    {
        try
        {
            //Clears the project details controls.
            ClearProjectDetailsControls();

            if (lblAddContract.Text != EDITCONTRACT)
            {
                Response.Redirect(CommonConstants.CONTRACTSUMMARY_PAGE + "?" + URLHelper.SecureParameters("CancleClick", 1.ToString()) + "&" + URLHelper.CreateSignature(1.ToString()), false);
            }
            else if (txtContractCode.Text.Trim() != string.Empty)
            {
                string[] cId = (txtContractCode.Text).Split('C');
                contractId = Convert.ToInt32(cId[1]);
                ViewContractDetails(contractId, SortDir, SortExpression);
                lblAddContract.Text = Contract;
                PanelProjectDetails.Visible = true;
                ReadOnlyControls();
                HideProjectControls();
                BtnSelectExistingPrj.Visible = false;
                btnAddNewPrj.Visible = false;
                btnPrevious.Visible = true;
                btnNext.Visible = true;
                btnSave.Visible = false;
                btnEdit.Visible = true;
            }
            else
            {
                Response.Redirect(CommonConstants.CONTRACTSUMMARY_PAGE + "?" + URLHelper.SecureParameters("CancleClick", 1.ToString()) + "&" + URLHelper.CreateSignature(1.ToString()), false);
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionbtnCancle_Click, EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }


    }

    /// <summary>
    /// Button edit event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            //Changes the text of the lable.
            lblAddContract.Text = EDITCONTRACT;

            lblContractStatus.Visible = true;
            ddlContractStatus.Visible = true;

            lblContractCode.Visible = true;
            txtContractCode.Visible = true;

            //Removes the read only property of the Controls.
            RemoveReadOnlyControls();

            //Unhides the project details controls.
            UnhideProjectControls();

            gvCRDetails.Columns[6].Visible = true;
            gvCRDetails.Columns[7].Visible = true;

            if (gvCRDetails.Rows.Count > 0)
                divCRDetails.Visible = true;


            btnAddNewPrj.Visible = true;

            BtnSelectExistingPrj.Visible = true;

            //Sets the Page mode.
            mode = Mode.Update.ToString();

            //Make visible to save button (update).
            btnSave.Visible = true;

            lblProjectCode.Visible = false;
            txtProjectCode.Visible = false;

            //Check if query string is null or not.

            if (Request.QueryString[QueryStringContractID] != null)
            {
                contractId = int.Parse(DecryptQueryString(QueryStringContractID));

                //Business entity of type contract is created.
                objViewContract = new BusinessEntities.Contract();

                //Business entity of type contract is created.
                BusinessEntities.Contract objContract = new BusinessEntities.Contract();

                //Business entity of type contract project is created.
                objContractProject = new List<BusinessEntities.ContractProject>();

                //Created a new object of "Rave.HR.BusinessLayer.Contracts.Contract" to acess its function.
                Rave.HR.BusinessLayer.Contracts.Contract objContractBL = new Rave.HR.BusinessLayer.Contracts.Contract();

                objViewContract.ContractID = contractId;

                //Call the BL layer Function to fetch the data.
                objContract = objContractBL.GetContractDetails(objViewContract, SortDir, SortExpression);


                objContractProject = objContractBL.GetContractProjectDetails(objViewContract.ContractID, SortDir, SortExpression);

                isDatePassed = CompairDateOfProjectEndDt(objContractProject);
                isEmployeesReleased = CheckEmpAllReleased(objContractProject);

                if (isDatePassed != true)
                {
                    if (isEmployeesReleased == true)
                    {
                        contractStatusChangeAllowed = true;
                    }
                }

                // checks if contract status change is allowed or not.
                if (gvProjectDetails.Rows.Count > 0)
                {
                    if (contractStatusChangeAllowed == true)
                    {
                        ddlContractStatus.Enabled = true;
                    }
                    else
                    {
                        ddlContractStatus.Enabled = false;
                    }
                }
                else
                {
                    ddlContractStatus.Enabled = true;
                }
            }
            // txtContractValue.Enabled = false;

            //Siddharth 13th March 2015 --Make Project Model Dropdown enabled when Edit Button is clicked -- Start
            //ddlProjectModel.Enabled = true;
            //Siddharth 13th March 2015 --Make Project Model Dropdown enabled when Edit Button is clicked -- End

            //Siddharth 8 Sept 2015 --Make Business Vertical Dropdown enabled when Edit Button is clicked -- Start
            //ddlBusinessVertical.Enabled = true;
            //Siddharth 8 Sept 2015 --Make Business Vertical Dropdown enabled when Edit Button is clicked -- End

        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionbtnEdit_Click, EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// btnPrevious_Click event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            PreviousClick();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionbtnPrevious_Click, EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// btnNext_Click event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            NextClick();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionbtnNext_Click, EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// btnAddRow_Click event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateProjectdates())
            {
                bool isProjectAdded = false;
                Decimal NoOfresources = 0M;
                string projectName = txtPrjName.Text.Trim();
                string projectCode = txtClientAbbr.Text.Trim() + "_" + txtProjectAbbr.Text.Trim() + "_" + txtPhase.Text.Trim();
                IsProjectDetailsVisible.Value = NO;

                //boolean variable defined to check wether project name available or not 
                //It is set to true if no such record is available.

                bool noPrjNameAvailable;

                if (txtProjectCode.Text.Trim() == "")
                {
                    noPrjNameAvailable = checkProjectName(projectName);
                }
                else
                {
                    noPrjNameAvailable = true;
                }

                if (checkProjectCode(projectCode) && IsNewProject.Value == NO)
                {
                    lblMandatory.Text = "Project Code, entered already exist, kindly enter a different code";
                    lblMandatory.Text = "<font color=RED>" + lblMandatory.Text + "</font>";

                }

                //If we add the existing project for a contract row should be added in a grid.
                else if ((noPrjNameAvailable == true) || (IsNewProject.Value == NO))
                {
                    BusinessEntities.ContractProject contractProjectData = new BusinessEntities.ContractProject();
                    contractProjectData = GetProjectControldata();
                    gvProjectDetails.Visible = true;

                    DataTable dtProjectData = new DataTable();

                    DataRow dr = dtProjectData.NewRow();
                    dtProjectData.Columns.Add(DbTableColumn.Con_ProjectCode);
                    dtProjectData.Columns.Add(DbTableColumn.Con_ProjectName);
                    dtProjectData.Columns.Add(DbTableColumn.ProjectType);
                    dtProjectData.Columns.Add(DbTableColumn.ProjectTypeID);
                    dtProjectData.Columns.Add(DbTableColumn.ProjectStartDate);
                    dtProjectData.Columns.Add(DbTableColumn.ProjectEndDate);
                    dtProjectData.Columns.Add(DbTableColumn.NoOfResources);
                    dtProjectData.Columns.Add(DbTableColumn.ProjectLocation);
                    dtProjectData.Columns.Add(DbTableColumn.Description);
                    dtProjectData.Columns.Add(DbTableColumn.Con_ProjectCategoryID);
                    dtProjectData.Columns.Add(DbTableColumn.Con_ProjectCategoryName);
                    dtProjectData.Columns.Add(DbTableColumn.Con_StatusID);
                    // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
                    // Desc : Add project group in Contract page
                    dtProjectData.Columns.Add(DbTableColumn.ProjectGroup);
                    // Mohamed : Issue 49791 : 15/09/2014 : Ends

                    // Mohamed : Issue  : 23/09/2014 : Starts                        			  
                    // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
                    dtProjectData.Columns.Add(DbTableColumn.ProjectDivision);
                    dtProjectData.Columns.Add(DbTableColumn.ProjectBusinessArea);
                    dtProjectData.Columns.Add(DbTableColumn.ProjectBusinessSegment);
                    //dtProjectData.Columns.Add(DbTableColumn.ProjectAlias);
                    // Mohamed : Issue  : 23/09/2014 : Ends


                    //Siddharth 9 Sept 2015 Start
                    dtProjectData.Columns.Add(DbTableColumn.ProjectModel);
                    dtProjectData.Columns.Add(DbTableColumn.BusinessVertical);
                    //Siddharth 9 Sept 2015 Start


                    //Rakesh : HOD for Employees 11/07/2016 Begin   
                    dtProjectData.Columns.Add(DbTableColumn.ProjectHeadName);
                    dtProjectData.Columns.Add(DbTableColumn.ProjectHeadId);

                    //Rakesh : HOD for Employees 11/07/2016 End

                    if (Session[SessionNames.CONTRACT_PROJECT_DATA] != null)
                    {
                        dtProjectData = (DataTable)Session[SessionNames.CONTRACT_PROJECT_DATA];

                    }
                    //For checking duplicate project in a contract.
                    foreach (DataRow CheckRow in dtProjectData.Rows)
                    {
                        if (CheckRow[DbTableColumn.Con_ProjectName].ToString() == txtPrjName.Text.Trim())
                        {
                            isProjectAdded = true;
                            break;
                        }
                    }
                    if (!isProjectAdded || IsGridEdited.Value == YES)
                    {
                        //Add when new project is added.
                        if (contractProjectData != null)
                        {
                            addProjectDetails.Add(contractProjectData);
                        }

                        dr = dtProjectData.NewRow();
                        dr[DbTableColumn.Con_ProjectCode] = txtProjectCode.Text.Trim();
                        dr[DbTableColumn.Con_ProjectName] = txtPrjName.Text.Trim();
                        dr[DbTableColumn.ProjectType] = ddlTypeOfPrj.SelectedItem.Text.Trim();
                        dr[DbTableColumn.ProjectTypeID] = Convert.ToInt32(ddlTypeOfPrj.SelectedValue);
                        dr[DbTableColumn.ProjectStartDate] = ucDatePickerStart.Text.Trim();
                        dr[DbTableColumn.ProjectEndDate] = ucDatePickerEnd.Text.Trim();
                        // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
                        // Desc : Add project group in Contract page
                        dr[DbTableColumn.ProjectGroup] = ddlPrjGroup.SelectedItem.Text.Trim();
                        // Mohamed : Issue 49791 : 15/09/2014 : Ends
                        // Mohamed : Issue  : 23/09/2014 : Starts                        			  
                        // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page
                        dr[DbTableColumn.ProjectDivision] = Convert.ToInt32(ddlPrjDivision.SelectedValue);

                        dr[DbTableColumn.ProjectBusinessArea] = ddlPrjBusinessArea.Enabled ? Convert.ToInt32(ddlPrjBusinessArea.SelectedValue) : 0;
                        dr[DbTableColumn.ProjectBusinessSegment] = ddlPrjBusinessSegment.Enabled ? Convert.ToInt32(ddlPrjBusinessSegment.SelectedValue) : 0;
                        //dr[DbTableColumn.ProjectAlias] = txtPrjAlias.Text.Trim();
                        // Mohamed : Issue  : 23/09/2014 : Ends


                        //Siddharth 9 Sept 2015 Start
                        dr[DbTableColumn.ProjectModel] = ddlProjectModel.Enabled ? Convert.ToInt32(ddlProjectModel.SelectedValue) : 0;
                        //dr[DbTableColumn.ProjectModel] = Convert.ToInt32(ddlProjectModel.SelectedValue);
                        dr[DbTableColumn.BusinessVertical] = ddlBusinessVertical.Enabled ? Convert.ToInt32(ddlBusinessVertical.SelectedValue) : 0;
                        //dr[DbTableColumn.BusinessVertical] = Convert.ToInt32(ddlBusinessVertical.SelectedValue); 
                        //Siddharth 9 Sept 2015 Start


                        if (!ddlPrjLocation.SelectedIndex.Equals(0))
                        {
                            dr[DbTableColumn.ProjectLocation] = ddlPrjLocation.SelectedValue;
                        }
                        else
                        {
                            dr[DbTableColumn.ProjectLocation] = "0";
                        }
                        if (txtNoOfResources.Text.Trim() != "")
                        {
                            NoOfresources = Convert.ToDecimal(txtNoOfResources.Text.Trim());
                        }
                        dr[DbTableColumn.NoOfResources] = Convert.ToDecimal(NoOfresources);

                        if (txtDescription.Text.Trim() != "")
                        {
                            dr[DbTableColumn.Description] = txtDescription.Text.Trim();
                        }

                        dr[DbTableColumn.Con_ProjectCategoryName] = ddlProjectCategory.SelectedItem.Text.Trim();

                        if (!ddlProjectCategory.SelectedIndex.Equals(0))
                        {
                            dr[DbTableColumn.Con_ProjectCategoryID] = ddlProjectCategory.SelectedValue;
                        }
                        else
                        {
                            dr[DbTableColumn.Con_ProjectCategoryID] = "0";
                        }

                        //Siddharth 9 Sept 2015 Start
                        if (!ddlProjectModel.SelectedIndex.Equals(0))
                        {
                            dr[DbTableColumn.ProjectModel] = ddlProjectModel.SelectedValue;
                        }
                        else
                        {
                            dr[DbTableColumn.ProjectModel] = "0";
                        }

                        if (!ddlBusinessVertical.SelectedIndex.Equals(0))
                        {
                            dr[DbTableColumn.BusinessVertical] = ddlBusinessVertical.SelectedValue;
                        }
                        else
                        {
                            dr[DbTableColumn.BusinessVertical] = "0";
                        }
                        //Siddharth 9 Sept 2015 End



                        //Rakesh : HOD for Employees 11/07/2016 Begin   
                        if (!ddlProjectHead.SelectedIndex.Equals(0))
                        {
                            dr[DbTableColumn.ProjectHeadId] = ddlProjectHead.SelectedValue;
                        }
                        else
                        {
                            dr[DbTableColumn.ProjectHeadId] = "0";
                        }

                        if (!ddlProjectHead.SelectedIndex.Equals(0))
                        {
                            dr[DbTableColumn.ProjectHeadName] = ddlProjectHead.SelectedItem.Text;
                        }
                        else
                        {
                            dr[DbTableColumn.ProjectHeadId] = "";
                        }
                        //Rakesh : HOD for Employees 11/07/2016 End   

                        if (IsGridEdited.Value != YES)
                        {
                            dtProjectData.Rows.Add(dr);
                            Session[SessionNames.CONTRACT_PROJECT_DATA] = dtProjectData;
                        }
                        else
                        {
                            foreach (DataRow row in dtProjectData.Rows)
                            {
                                if (dr[DbTableColumn.Con_ProjectName].ToString() == row[DbTableColumn.Con_ProjectName].ToString())
                                {

                                    if (!txtNoOfResources.Text.Trim().Equals("0.0") && !txtNoOfResources.Text.Trim().Equals("0") && !txtNoOfResources.Text.Trim().Equals(string.Empty))
                                    {
                                        row[DbTableColumn.NoOfResources] = Convert.ToDecimal(txtNoOfResources.Text.Trim());
                                    }
                                    else
                                    {
                                        row[DbTableColumn.NoOfResources] = "0.00";
                                    }

                                    row[DbTableColumn.Description] = txtDescription.Text.Trim();
                                    row[DbTableColumn.Con_ProjectCategoryID] = ddlProjectCategory.SelectedValue;
                                    row[DbTableColumn.Con_ProjectCategoryName] = ddlProjectCategory.SelectedItem.Text;
                                    // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
                                    // Desc : Add project group in Contract page                                    
                                    row[DbTableColumn.ProjectGroup] = ddlPrjGroup.SelectedItem.Text.Trim();
                                    // Mohamed : Issue 49791 : 15/09/2014 : Ends
                                    // Mohamed : Issue  : 23/09/2014 : Starts                        			  
                                    // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page                                                                       
                                    row[DbTableColumn.ProjectDivision] = ddlPrjDivision.SelectedValue;
                                    row[DbTableColumn.ProjectBusinessArea] = ddlPrjBusinessArea.Enabled ? ddlPrjBusinessArea.SelectedValue : "0";
                                    row[DbTableColumn.ProjectBusinessSegment] = ddlPrjBusinessSegment.Enabled ? ddlPrjBusinessSegment.SelectedValue : "0";
                                    //row[DbTableColumn.ProjectAlias] = txtPrjAlias.Text.Trim();
                                    // Mohamed : Issue  : 23/09/2014 : Ends

                                    //Edited here
                                    //Siddharth 9 Sept 2015 Start
                                    row[DbTableColumn.ProjectModel] = ddlProjectModel.Enabled ? ddlProjectModel.SelectedValue : "0";
                                    //row[DbTableColumn.ProjectModel] = ddlProjectModel.SelectedValue;
                                    row[DbTableColumn.BusinessVertical] = ddlBusinessVertical.Enabled ? ddlBusinessVertical.SelectedValue : "0";
                                    //row[DbTableColumn.BusinessVertical] = ddlBusinessVertical.SelectedValue;
                                    //Siddharth 9 Sept 2015 End


                                    IsGridEdited.Value = YES;
                                    break;
                                }
                            }
                            Session[SessionNames.CONTRACT_PROJECT_DATA] = dtProjectData;
                        }
                        IsGridEdited.Value = NO;
                        if ((btnAddRow.Text == ADDROW))
                        {
                            gvProjectDetails.DataSource = dtProjectData;
                            gvProjectDetails.DataBind();
                        }
                        else
                        {
                            if (Session[SessionNames.CONTRACT_EDIT_PROJECT_DETAILS] != null)
                            {
                                #region update
                                mode = Mode.Update.ToString();
                                //Creates a new data to store the old data for the project details.
                                BusinessEntities.ContractProject oldProjectDetails = new BusinessEntities.ContractProject();

                                //oldProjectDetails = null;
                                //Assigns the old data to the table from the session to the table.
                                oldProjectDetails = (BusinessEntities.ContractProject)Session[SessionNames.CONTRACT_EDIT_PROJECT_DETAILS];

                                BusinessEntities.ContractProject forlist = new BusinessEntities.ContractProject();

                                //creates a business entity of type contract project.
                                BusinessEntities.ContractProject editProjectDetails = new BusinessEntities.ContractProject();

                                List<BusinessEntities.ContractProject> lsteditProjectDetails = new List<BusinessEntities.ContractProject>();

                                List<BusinessEntities.ContractProject> lsteditProject = new List<BusinessEntities.ContractProject>();

                                //Compair the old data (before edit) to the new data (after edit) of the project details.
                                if (oldProjectDetails != null)
                                {
                                    foreach (DataRow row in dtProjectData.Rows)
                                    {
                                        editProjectDetails.ProjectCode = row[DbTableColumn.Con_ProjectCode].ToString();

                                        editProjectDetails.ProjectName = row[DbTableColumn.Con_ProjectName].ToString();

                                        if (oldProjectDetails.ProjectType != row[DbTableColumn.ProjectType].ToString())
                                        {
                                            editProjectDetails.ProjectType = row[DbTableColumn.ProjectType].ToString();
                                        }
                                        else
                                        {
                                            editProjectDetails.ProjectType = oldProjectDetails.ProjectType;
                                        }
                                        if (!string.IsNullOrEmpty(row[DbTableColumn.ProjectTypeID].ToString()))
                                        {
                                            editProjectDetails.ProjectTypeID = Convert.ToInt32(row[DbTableColumn.ProjectTypeID]);
                                        }

                                        if (oldProjectDetails.ProjectStartDate != Convert.ToDateTime(row[DbTableColumn.ProjectStartDate]))
                                        {
                                            editProjectDetails.ProjectStartDate = Convert.ToDateTime(row[DbTableColumn.ProjectStartDate]);
                                        }
                                        else
                                        {
                                            editProjectDetails.ProjectStartDate = oldProjectDetails.ProjectStartDate;
                                        }

                                        if (oldProjectDetails.ProjectEndDate != Convert.ToDateTime(row[DbTableColumn.ProjectEndDate]))
                                        {
                                            editProjectDetails.ProjectEndDate = Convert.ToDateTime(row[DbTableColumn.ProjectEndDate]);
                                        }
                                        else
                                        {
                                            editProjectDetails.ProjectEndDate = oldProjectDetails.ProjectEndDate;
                                        }

                                        if (oldProjectDetails.NoOfResources != Convert.ToDecimal(row[DbTableColumn.NoOfResources]))
                                        {
                                            editProjectDetails.NoOfResources = Convert.ToDecimal(row[DbTableColumn.NoOfResources]);
                                        }
                                        else
                                        {
                                            editProjectDetails.NoOfResources = Convert.ToDecimal(oldProjectDetails.NoOfResources);
                                        }
                                        if (oldProjectDetails.ProjectLocationName != row[DbTableColumn.ProjectLocation].ToString())
                                        {
                                            editProjectDetails.ProjectLocationName = row[DbTableColumn.ProjectLocation].ToString();
                                        }
                                        else
                                        {
                                            editProjectDetails.ProjectLocationName = oldProjectDetails.ProjectLocationName;
                                        }

                                        if (!string.IsNullOrEmpty(row[DbTableColumn.Con_ProjectCategoryID].ToString()))
                                        {
                                            editProjectDetails.ProjectCategoryID = Convert.ToInt32(row[DbTableColumn.Con_ProjectCategoryID]);
                                        }

                                        if (oldProjectDetails.ProjectCategoryName != row[DbTableColumn.Con_ProjectCategoryName].ToString())
                                        {
                                            editProjectDetails.ProjectCategoryName = row[DbTableColumn.Con_ProjectCategoryName].ToString();
                                        }
                                        else
                                        {
                                            editProjectDetails.ProjectCategoryName = oldProjectDetails.ProjectCategoryName;
                                        }
                                        // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
                                        // Desc : Add project group in Contract page                                    
                                        if (oldProjectDetails.ProjectGroup != row[DbTableColumn.ProjectGroup].ToString())
                                        {
                                            editProjectDetails.ProjectGroup = row[DbTableColumn.ProjectGroup].ToString();
                                        }
                                        else
                                        {
                                            editProjectDetails.ProjectGroup = oldProjectDetails.ProjectGroup;
                                        }
                                        // Mohamed : Issue 49791 : 15/09/2014 : Ends

                                        // Mohamed : Issue  : 23/09/2014 : Starts                        			  
                                        // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
                                        if (oldProjectDetails.ProjectDivision != Convert.ToInt32(row[DbTableColumn.ProjectDivision].ToString()))
                                        {
                                            editProjectDetails.ProjectDivision = Convert.ToInt32(row[DbTableColumn.ProjectDivision].ToString());
                                        }
                                        else
                                        {
                                            editProjectDetails.ProjectDivision = oldProjectDetails.ProjectDivision;
                                        }

                                        if (oldProjectDetails.ProjectBussinessArea != Convert.ToInt32(row[DbTableColumn.ProjectBusinessArea].ToString()))
                                        {
                                            editProjectDetails.ProjectBussinessArea = Convert.ToInt32(row[DbTableColumn.ProjectBusinessArea].ToString());
                                        }
                                        else
                                        {
                                            editProjectDetails.ProjectBussinessArea = oldProjectDetails.ProjectBussinessArea;
                                        }

                                        if (oldProjectDetails.ProjectBussinessSegment != Convert.ToInt32(row[DbTableColumn.ProjectBusinessSegment].ToString()))
                                        {
                                            editProjectDetails.ProjectBussinessSegment = Convert.ToInt32(row[DbTableColumn.ProjectBusinessSegment].ToString());
                                        }
                                        else
                                        {
                                            editProjectDetails.ProjectBussinessSegment = oldProjectDetails.ProjectBussinessSegment;
                                        }

                                        //if (oldProjectDetails.ProjectAlias != row[DbTableColumn.ProjectAlias].ToString())
                                        //{
                                        //    editProjectDetails.ProjectAlias = row[DbTableColumn.ProjectAlias].ToString();
                                        //}
                                        //else
                                        //{
                                        //    editProjectDetails.ProjectAlias = oldProjectDetails.ProjectAlias;
                                        //}
                                        // Mohamed : Issue  : 23/09/2014 : Ends
                                        //BindDataToProject(editProjectDetails, Mode.Update);


                                        //Siddharth 9 Sept 2015 Start
                                        if (oldProjectDetails.ProjectModel != row[DbTableColumn.ProjectModel].ToString())
                                        {
                                            editProjectDetails.ProjectModel = row[DbTableColumn.ProjectModel].ToString();
                                        }
                                        else
                                        {
                                            editProjectDetails.ProjectModel = oldProjectDetails.ProjectModel;
                                        }

                                        if (oldProjectDetails.BusinessVertical != row[DbTableColumn.BusinessVertical].ToString())
                                        {
                                            editProjectDetails.BusinessVertical = row[DbTableColumn.BusinessVertical].ToString();
                                        }
                                        else
                                        {
                                            editProjectDetails.BusinessVertical = oldProjectDetails.BusinessVertical;
                                        }

                                        //Siddharth 9 Sept 2015 End


                                    }
                                }
                                lsteditProjectDetails = (List<BusinessEntities.ContractProject>)Session[SessionNames.CONTRACT_GRIDDATA];
                                //Session[SessionNames.CONTRACT_GRIDDATA] = null;
                                if (lsteditProjectDetails.Count != 0)
                                {
                                    foreach (BusinessEntities.ContractProject prjDetails in lsteditProjectDetails)
                                    {
                                        //Checks wether the data match with the data of project edited.added second condition to check project code should not be blank.
                                        if (prjDetails.ProjectCode == editProjectDetails.ProjectCode || !string.IsNullOrEmpty(editProjectDetails.ProjectCode))
                                        {
                                            //Checks for each value wether the same or changed.

                                            if (prjDetails.ProjectCode != editProjectDetails.ProjectCode)
                                            {
                                                forlist.ProjectCode = editProjectDetails.ProjectCode;
                                            }
                                            else
                                            {
                                                forlist.ProjectCode = prjDetails.ProjectCode;
                                            }

                                            if (prjDetails.ProjectName != editProjectDetails.ProjectName)
                                            {
                                                forlist.ProjectName = editProjectDetails.ProjectName;
                                            }
                                            else
                                            {
                                                forlist.ProjectName = prjDetails.ProjectName;
                                            }

                                            if (prjDetails.ProjectType != editProjectDetails.ProjectType)
                                            {
                                                forlist.ProjectType = editProjectDetails.ProjectType;
                                            }
                                            else
                                            {
                                                forlist.ProjectType = prjDetails.ProjectType;
                                            }

                                            if (prjDetails.ProjectStartDate != editProjectDetails.ProjectStartDate)
                                            {
                                                forlist.ProjectStartDate = editProjectDetails.ProjectStartDate;
                                            }
                                            else
                                            {
                                                forlist.ProjectStartDate = prjDetails.ProjectStartDate;
                                            }

                                            if (prjDetails.ProjectEndDate != editProjectDetails.ProjectEndDate)
                                            {
                                                forlist.ProjectEndDate = editProjectDetails.ProjectEndDate;
                                            }
                                            else
                                            {
                                                forlist.ProjectEndDate = prjDetails.ProjectEndDate;
                                            }

                                            if (prjDetails.NoOfResources != editProjectDetails.NoOfResources)
                                            {
                                                forlist.NoOfResources = editProjectDetails.NoOfResources;
                                            }
                                            else
                                            {
                                                forlist.NoOfResources = prjDetails.NoOfResources;
                                            }

                                            if (prjDetails.ProjectCategoryName != editProjectDetails.ProjectCategoryName)
                                            {
                                                forlist.ProjectCategoryName = editProjectDetails.ProjectCategoryName;
                                            }
                                            else
                                            {
                                                forlist.ProjectCategoryName = prjDetails.ProjectCategoryName;
                                            }
                                            if (prjDetails.ProjectCategoryID != editProjectDetails.ProjectCategoryID)
                                            {
                                                forlist.ProjectCategoryID = editProjectDetails.ProjectCategoryID;
                                            }
                                            else
                                            {
                                                forlist.ProjectCategoryID = prjDetails.ProjectCategoryID;
                                            }
                                        }
                                        else
                                        {
                                            forlist = null;
                                        }
                                        //Removes the business entity from the list to be replaced by the newer one (edit version)                   
                                        if (forlist != null)
                                        {
                                            lsteditProject.Add(forlist);
                                        }
                                        else
                                        {
                                            lsteditProject.Add(prjDetails);
                                        }
                                    }
                                }
                                else
                                {

                                    //forlist = (BusinessEntities.ContractProject)Session[SessionNames.CONTRACT_PROJECT_DATA];
                                    foreach (DataRow da in dtProjectData.Rows)
                                    {
                                        forlist.ProjectCode = da[DbTableColumn.Con_ProjectCode].ToString();
                                        forlist.ProjectName = da[DbTableColumn.Con_ProjectName].ToString();
                                        forlist.ProjectType = da[DbTableColumn.ProjectType].ToString();
                                        forlist.ProjectTypeID = Convert.ToInt32(da[DbTableColumn.ProjectTypeID]);
                                        forlist.ProjectStartDate = Convert.ToDateTime(da[DbTableColumn.ProjectStartDate]);
                                        forlist.ProjectEndDate = Convert.ToDateTime(da[DbTableColumn.ProjectEndDate]);
                                        forlist.NoOfResources = Convert.ToDecimal(da[DbTableColumn.NoOfResources], culture);
                                        forlist.ProjectCategoryID = Convert.ToInt32(da[DbTableColumn.Con_ProjectCategoryID]);
                                        forlist.ProjectCategoryName = da[DbTableColumn.Con_ProjectCategoryName].ToString();
                                        // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
                                        // Desc : Add project group in Contract page                                    
                                        forlist.ProjectGroup = da[DbTableColumn.ProjectGroup].ToString();
                                        // Mohamed : Issue 49791 : 15/09/2014 : Ends
                                        // Mohamed : Issue  : 23/09/2014 : Starts                        			  
                                        // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
                                        forlist.ProjectDivision = Convert.ToInt32(da[DbTableColumn.ProjectDivision]);
                                        forlist.ProjectBussinessArea = Convert.ToInt32(da[DbTableColumn.ProjectBusinessArea]);
                                        forlist.ProjectBussinessSegment = Convert.ToInt32(da[DbTableColumn.ProjectBusinessSegment]);
                                        //forlist.ProjectAlias = da[DbTableColumn.ProjectTypeID].ToString();
                                        // Mohamed : Issue  : 23/09/2014 : Ends

                                        //Siddharth 9 Sept 2015 Start
                                        forlist.ProjectModel = da[DbTableColumn.ProjectModel].ToString();
                                        forlist.BusinessVertical = da[DbTableColumn.BusinessVertical].ToString();
                                        //Siddharth 9 Sept 2015 End
                                        lsteditProject.Add(forlist);
                                    }
                                }
                                //Calls the function to bind the updated data to the grid.
                                BindDataToProject(lsteditProject, Mode.Update);
                                #endregion update
                            }
                            else
                            {

                                BusinessEntities.ContractProject listPrj = new BusinessEntities.ContractProject();
                                List<BusinessEntities.ContractProject> editProject = new List<BusinessEntities.ContractProject>();
                                //string LoggedInUserMailId = authoriseduser.getLoggedInUserEmailId();
                                foreach (DataRow da in dtProjectData.Rows)
                                {
                                    listPrj.ProjectCode = da[DbTableColumn.Con_ProjectCode].ToString();
                                    listPrj.ProjectName = da[DbTableColumn.Con_ProjectName].ToString();
                                    listPrj.ProjectType = da[DbTableColumn.ProjectType].ToString();
                                    if (!string.IsNullOrEmpty(da[DbTableColumn.ProjectTypeID].ToString()))
                                    {
                                        listPrj.ProjectTypeID = Convert.ToInt32(da[DbTableColumn.ProjectTypeID]);
                                    }
                                    listPrj.ProjectStartDate = Convert.ToDateTime(da[DbTableColumn.ProjectStartDate]);
                                    listPrj.ProjectEndDate = Convert.ToDateTime(da[DbTableColumn.ProjectEndDate]);
                                    listPrj.NoOfResources = Convert.ToDecimal(da[DbTableColumn.NoOfResources]);
                                    listPrj.ProjectLocationName = da[DbTableColumn.ProjectLocation].ToString();
                                    // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
                                    // Desc : Add project group in Contract page                               
                                    listPrj.ProjectGroup = da[DbTableColumn.ProjectGroup].ToString();
                                    // Mohamed : Issue 49791 : 15/09/2014 : Ends
                                    // Mohamed : Issue  : 23/09/2014 : Starts                        			  
                                    // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
                                    listPrj.ProjectDivision = Convert.ToInt32(da[DbTableColumn.ProjectDivision]);
                                    listPrj.ProjectBussinessArea = Convert.ToInt32(da[DbTableColumn.ProjectBusinessArea]);
                                    listPrj.ProjectBussinessSegment = Convert.ToInt32(da[DbTableColumn.ProjectBusinessSegment]);
                                    //listPrj.ProjectAlias = da[DbTableColumn.ProjectTypeID].ToString();
                                    // Mohamed : Issue  : 23/09/2014 : Ends
                                    if (!string.IsNullOrEmpty(da[DbTableColumn.Con_ProjectCategoryID].ToString()))
                                    {
                                        listPrj.ProjectCategoryID = Convert.ToInt32(da[DbTableColumn.Con_ProjectCategoryID]);
                                    }
                                    listPrj.ProjectCategoryName = da[DbTableColumn.Con_ProjectCategoryName].ToString();

                                    //Siddharth 9 Sept 2015 Start
                                    listPrj.ProjectModel = da[DbTableColumn.ProjectModel].ToString();
                                    listPrj.BusinessVertical = da[DbTableColumn.BusinessVertical].ToString();
                                    //Siddharth 9 Sept 2015 End


                                    listPrj.CreatedByEmailId = UserMailId;
                                    editProject.Add(listPrj);
                                }

                                //Calls the function to bind the updated data to the grid.
                                BindDataToProject(editProject, Mode.Update);
                            }
                        }
                        //Clear the controls of project details.
                        ClearProjectDetailsControls();
                        lblMandatory.Text = string.Empty;
                        lblConfirmMessage.Text = string.Empty;
                        lblMandatory.Text = " Please click on the Save button to save the project details ";
                        lblMandatory.Text = "<font color=Blue>" + lblMandatory.Text + "</font>";
                    }
                    else
                    {
                        lblMandatory.Text = txtPrjName.Text.Trim() + " is already added for this Contract";
                        lblMandatory.Text = "<font color=RED>" + lblMandatory.Text + "</font>";
                    }
                    //Session[SessionNames.CONTRACT_GRIDDATA] = dtProjectData; 
                    gvProjectDetails.DataSource = dtProjectData;
                    gvProjectDetails.DataBind();

                }
                else
                {
                    lblMandatory.Text = "Project Name, entered already exist, kindly enter a different name";
                    lblMandatory.Text = "<font color=RED>" + lblMandatory.Text + "</font>";
                }
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionbtnAddRow_Click, EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// btnDelete_Click event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        bool delete = false;
        string projectNames = null;
        lblMandatory.Visible = true;
        try
        {
            mode = Mode.Delete.ToString();

            BusinessEntities.Contract ContractDetails = new BusinessEntities.Contract();
            //gets all the data for the contract details controls. 

            ContractDetails = GetContractControldata();
            ContractDetails.ReasonForDeletion = tbReasonForDeletion.Text;

            List<BusinessEntities.ContractProject> ProjectDetails = new List<BusinessEntities.ContractProject>();

            //Stores the session data for the grid in the variable.

            ProjectDetails = (List<BusinessEntities.ContractProject>)Session[SessionNames.CONTRACT_GRIDDATA];

            //Calls the function to delete the contract details.
            if (ProjectDetails.Count == 0)
            {
                delete = DeleteContract(ContractDetails);
                if (delete == true)
                {
                    lblMandatory.Text = "Contract '" + ContractDetails.ContractCode + "' has been sucessfully deleted for client '" + clientName + "', email notification is sent.";
                    lblMandatory.Text = "<font color=blue>" + lblMandatory.Text + "</font>";
                    tbReasonForDeletion.Text = "";
                    btnDelete.Visible = false;
                    btnCancle.Text = BACK;
                }
            }
            else
            {
                foreach (BusinessEntities.ContractProject prj in ProjectDetails)
                {
                    projectNames += prj.ProjectName + ",";
                }

                lblMandatory.Text = "Contract is associated with Projects :" + projectNames + "hence can not be deleted.";
                lblMandatory.Text = "<font color=RED>" + lblMandatory.Text + "</font>";
            }

        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionbtnDelete_Click, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// btnPrjCancle_Click event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPrjCancle_Click(object sender, EventArgs e)
    {
        try
        {
            //added to make clear & visible to all controls of project.
            ClearProjectDetailsControls();
            gvProjectDetails.Columns[7].Visible = true;
            gvProjectDetails.Columns[8].Visible = true;
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionbtnPrjCancle_Click, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// gvProductDetails_RowCommand click event used for image button click.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvProductDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string mod = mode.ToString();
            IsGridEdited.Value = YES;
            string projectName = e.CommandArgument.ToString();
            ObjProject = new BusinessEntities.ContractProject();
            BusinessEntities.ContractProject contractPrj = new BusinessEntities.ContractProject();
            if (e.CommandName == DELETECONTRACTCOMMAND)
            {
                //gets the project code of the selected row.
                ObjProject.ProjectName = projectName;

                //Declares the list object to store the Old grid data.
                // objContractProject = new List<BusinessEntities.ContractProject>();

                DataTable Project = new DataTable();

                //Assigns the grid data To the list Object.
                Project = (DataTable)Session[SessionNames.CONTRACT_PROJECT_DATA];

                //Declares the list object To hold the new grid data after removing
                //the deleted row of project data.
                List<BusinessEntities.ContractProject> newProjectData = new List<BusinessEntities.ContractProject>();

                //BusinessEntities.ContractProject ConProject = new BusinessEntities.ContractProject();

                //loop to check for the project code.
                foreach (DataRow dr in Project.Rows)
                {
                    if (dr[DbTableColumn.Con_StatusID].ToString() != string.Empty)
                    {
                        if (Convert.ToInt32(dr[DbTableColumn.Con_StatusID].ToString()) == 3)
                        {
                            if (dr[DbTableColumn.Con_ProjectName].ToString() == ObjProject.ProjectName)
                            {
                                //Remove the row which should be deleted.
                                Project.Rows.Remove(dr);
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (dr[DbTableColumn.Con_ProjectName].ToString() == ObjProject.ProjectName)
                        {
                            //Remove the row which should be deleted.
                            Project.Rows.Remove(dr);
                            break;
                        }
                    }
                }

                //Add the remaining project to list object.
                foreach (DataRow dr in Project.Rows)
                {
                    BusinessEntities.ContractProject ConProject = new BusinessEntities.ContractProject();

                    //Status ID is checked because if a existing project will have a status ID 
                    //but a new project will not have a status ID.
                    if (dr[DbTableColumn.Con_StatusID].ToString() != string.Empty)
                    {
                        if (dr[DbTableColumn.Con_ProjectName].ToString() != ObjProject.ProjectName)
                        {
                            ConProject.ProjectCode = dr[DbTableColumn.Con_ProjectCode].ToString();
                            ConProject.ProjectName = dr[DbTableColumn.Con_ProjectName].ToString();
                            ConProject.ProjectType = dr[DbTableColumn.ProjectType].ToString();
                            ConProject.NoOfResources = Convert.ToDecimal(dr[DbTableColumn.NoOfResources]);
                            ConProject.ProjectStartDate = Convert.ToDateTime(dr[DbTableColumn.ProjectStartDate].ToString());
                            ConProject.ProjectEndDate = Convert.ToDateTime(dr[DbTableColumn.ProjectEndDate].ToString());
                            ConProject.LocationName = dr[DbTableColumn.ProjectLocation].ToString();
                            ConProject.ProjectCategoryName = dr[DbTableColumn.Con_ProjectCategoryName].ToString();
                            // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
                            // Desc : Add project group in Contract page
                            ConProject.ProjectGroup = dr[DbTableColumn.ProjectGroup].ToString();
                            // Mohamed : Issue 49791 : 15/09/2014 : Ends
                            // Mohamed : Issue  : 23/09/2014 : Starts                        			  
                            // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
                            ConProject.ProjectDivision = Convert.ToInt32(dr[DbTableColumn.ProjectDivision]);
                            ConProject.ProjectBussinessArea = Convert.ToInt32(dr[DbTableColumn.ProjectBusinessArea]);
                            ConProject.ProjectBussinessSegment = Convert.ToInt32(dr[DbTableColumn.ProjectBusinessSegment]);
                            //ConProject.ProjectAlias = dr[DbTableColumn.ProjectTypeID].ToString();
                            // Mohamed : Issue  : 23/09/2014 : Ends

                            //Siddharth 9 Sept 2015 Start
                            ConProject.ProjectModel = dr[DbTableColumn.ProjectModel].ToString();
                            ConProject.BusinessVertical = dr[DbTableColumn.BusinessVertical].ToString();
                            //Siddharth 9 Sept 2015 End

                            newProjectData.Add(ConProject);
                        }
                        else
                        {
                            //Condition to check wether the Project has a status of Closed or not
                            //If not Closed then it should be added tho the data table.

                            if (Convert.ToInt32(dr[DbTableColumn.Con_StatusID].ToString()) != 3)
                            {
                                ConProject.ProjectCode = dr[DbTableColumn.Con_ProjectCode].ToString();
                                ConProject.ProjectName = dr[DbTableColumn.Con_ProjectName].ToString();
                                ConProject.ProjectType = dr[DbTableColumn.ProjectType].ToString();
                                ConProject.NoOfResources = Convert.ToDecimal(dr[DbTableColumn.NoOfResources]);
                                ConProject.ProjectStartDate = Convert.ToDateTime(dr[DbTableColumn.ProjectStartDate].ToString());
                                ConProject.ProjectEndDate = Convert.ToDateTime(dr[DbTableColumn.ProjectEndDate].ToString());
                                ConProject.LocationName = dr[DbTableColumn.ProjectLocation].ToString();
                                ConProject.ProjectCategoryName = dr[DbTableColumn.Con_ProjectCategoryName].ToString();
                                // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
                                // Desc : Add project group in Contract page
                                ConProject.ProjectGroup = dr[DbTableColumn.ProjectGroup].ToString();
                                // Mohamed : Issue 49791 : 15/09/2014 : Ends
                                // Mohamed : Issue  : 23/09/2014 : Starts                        			  
                                // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
                                ConProject.ProjectDivision = Convert.ToInt32(dr[DbTableColumn.ProjectDivision]);
                                ConProject.ProjectBussinessArea = Convert.ToInt32(dr[DbTableColumn.ProjectBusinessArea]);
                                ConProject.ProjectBussinessSegment = Convert.ToInt32(dr[DbTableColumn.ProjectBusinessSegment]);
                                //ConProject.ProjectAlias = dr[DbTableColumn.ProjectTypeID].ToString();
                                // Mohamed : Issue  : 23/09/2014 : Ends

                                //Siddharth 9 Sept 2015 Start
                                ConProject.ProjectModel = dr[DbTableColumn.ProjectModel].ToString();
                                ConProject.BusinessVertical = dr[DbTableColumn.BusinessVertical].ToString();
                                //Siddharth 9 Sept 2015 End

                                newProjectData.Add(ConProject);

                                lblMandatory.Text = "Project : " + ObjProject.ProjectName.ToString() + " status is not Closed hence can not be deleted.";
                                lblMandatory.Text = "<font color=RED>" + lblMandatory.Text + "</font>";
                            }
                            //else
                            //{                                
                            //        lblMandatory.Text = "Project : " + ObjProject.ProjectName.ToString() + " status is not Closed hence can not be deleted.";
                            //        lblMandatory.Text = "<font color=RED>" + lblMandatory.Text + "</font>";                                   

                            //}
                        }
                    }
                    else
                    {
                        if (dr[DbTableColumn.Con_ProjectName].ToString() != ObjProject.ProjectName)
                        {
                            ConProject.ProjectCode = dr[DbTableColumn.Con_ProjectCode].ToString();
                            ConProject.ProjectName = dr[DbTableColumn.Con_ProjectName].ToString();
                            ConProject.ProjectType = dr[DbTableColumn.ProjectType].ToString();
                            ConProject.NoOfResources = Convert.ToDecimal(dr[DbTableColumn.NoOfResources]);
                            ConProject.ProjectStartDate = Convert.ToDateTime(dr[DbTableColumn.ProjectStartDate].ToString());
                            ConProject.ProjectEndDate = Convert.ToDateTime(dr[DbTableColumn.ProjectEndDate].ToString());
                            ConProject.LocationName = dr[DbTableColumn.ProjectLocation].ToString();
                            ConProject.ProjectCategoryName = dr[DbTableColumn.Con_ProjectCategoryName].ToString();
                            // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
                            // Desc : Add project group in Contract page
                            ConProject.ProjectGroup = dr[DbTableColumn.ProjectGroup].ToString();
                            // Mohamed : Issue 49791 : 15/09/2014 : Ends
                            // Mohamed : Issue  : 23/09/2014 : Starts                        			  
                            // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
                            ConProject.ProjectDivision = Convert.ToInt32(dr[DbTableColumn.ProjectDivision]);
                            ConProject.ProjectBussinessArea = Convert.ToInt32(dr[DbTableColumn.ProjectBusinessArea]);
                            ConProject.ProjectBussinessSegment = Convert.ToInt32(dr[DbTableColumn.ProjectBusinessSegment]);
                            //ConProject.ProjectAlias = dr[DbTableColumn.ProjectTypeID].ToString();
                            // Mohamed : Issue  : 23/09/2014 : Ends

                            //Siddharth 9 Sept 2015 Start
                            ConProject.ProjectModel = dr[DbTableColumn.ProjectModel].ToString();
                            ConProject.BusinessVertical = dr[DbTableColumn.BusinessVertical].ToString();
                            //Siddharth 9 Sept 2015 End

                            newProjectData.Add(ConProject);
                        }
                    }
                }

                //binds data to the grid.
                BindDataToProject(newProjectData, Mode.Update);

                //Session to hold the added project for a contract.
                Session[SessionNames.CONTRACT_PROJECT_DATA] = Project;

                //Session to hold the dissociated project details;
                Session[SessionNames.CONTRACT_DISASSOCIATED_PROJECT] = ObjProject;

                IsGridEdited.Value = NO;
            }
            if (e.CommandName == EDITCONTRACTCOMMAND)
            {
                // mode = Mode.Update.ToString();
                if (mode == Mode.Update.ToString())
                {
                    lblAddContract.Text = EDITCONTRACT;
                }
                raveHRCollection = new BusinessEntities.RaveHRCollection();
                BusinessEntities.ContractProject ProjectDetails_ID = new BusinessEntities.ContractProject();

                ObjProject.ProjectName = projectName;

                objContractProjectBL = new Rave.HR.BusinessLayer.Contracts.ContractProject();

                DataTable dt = new DataTable();
                dt = (DataTable)Session[SessionNames.CONTRACT_PROJECT_DATA];
                foreach (DataRow dr in dt.Rows)
                {
                    if (projectName == dr[DbTableColumn.Con_ProjectName].ToString())
                    {
                        if (dr[DbTableColumn.Con_ProjectCode].ToString() != null)
                        {
                            contractPrj.ProjectCode = dr[DbTableColumn.Con_ProjectCode].ToString();
                        }
                        else
                        {
                            contractPrj.ProjectCode = "";
                        }
                        contractPrj.ProjectName = dr[DbTableColumn.Con_ProjectName].ToString();
                        contractPrj.ProjectType = dr[DbTableColumn.ProjectType].ToString();
                        contractPrj.NoOfResources = Convert.ToDecimal(dr[DbTableColumn.NoOfResources]);
                        contractPrj.ProjectStartDate = Convert.ToDateTime(dr[DbTableColumn.ProjectStartDate].ToString());
                        contractPrj.ProjectEndDate = Convert.ToDateTime(dr[DbTableColumn.ProjectEndDate].ToString());
                        contractPrj.ProjectLocationName = dr[DbTableColumn.Projectlocation].ToString();
                        contractPrj.ProjectsDescription = dr[DbTableColumn.Description].ToString();
                        contractPrj.ProjectCategoryID = Convert.ToInt32(dr[DbTableColumn.Con_ProjectCategoryID]);
                        contractPrj.ProjectCategoryName = dr[DbTableColumn.Con_ProjectCategoryName].ToString();
                        // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
                        // Desc : Add project group in Contract page
                        contractPrj.ProjectGroup = dr[DbTableColumn.ProjectGroup].ToString();
                        // Mohamed : Issue 49791 : 15/09/2014 : Ends
                        // Mohamed : Issue  : 23/09/2014 : Starts                        			  
                        // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
                        contractPrj.ProjectDivision = Convert.ToInt32(dr[DbTableColumn.ProjectDivision]);
                        contractPrj.ProjectBussinessArea = Convert.ToInt32(dr[DbTableColumn.ProjectBusinessArea]);
                        contractPrj.ProjectBussinessSegment = Convert.ToInt32(dr[DbTableColumn.ProjectBusinessSegment]);
                        //contractPrj.ProjectAlias = dr[DbTableColumn.ProjectAlias].ToString();
                        // Mohamed : Issue  : 23/09/2014 : Ends

                        //Siddharth 9 Sept 2015 Start
                        contractPrj.ProjectModel = dr[DbTableColumn.ProjectModel].ToString();
                        contractPrj.BusinessVertical = dr[DbTableColumn.BusinessVertical].ToString();
                        //Siddharth 9 Sept 2015 End

                    }
                }
                BindDataToProjectControls(contractPrj);

            }
            if (e.CommandName == ADDCR)
            {
                HdfProjectCode.Value = e.CommandArgument.ToString();
                HideUnHideCRDetails(true);
                btnUpdateCRRow.Visible = false;
                btnUpdateCancelCRRow.Visible = false;
            }

        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctiongvProductDetails_RowCommand, EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// ddlTypeOfPrj_SelectedIndexChanged event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    protected void ddlTypeOfPrj_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            IsNewProject.Value = YES;
            lblConfirmMessage.Text = string.Empty;
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionddlTypeOfPrj_SelectedIndexChanged, EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    protected void btnAddCRRow_Click(object sender, EventArgs e)
    {
        try
        {
            Rave.HR.BusinessLayer.Contracts.Contract objCRDetailsBAL;

            BusinessEntities.Contract objCRDetails = new BusinessEntities.Contract();
            objCRDetailsBAL = new Rave.HR.BusinessLayer.Contracts.Contract();

            objCRDetails.CRReferenceNo = txtCRReferenceNo.Text.Trim();
            objCRDetails.ContractID = Convert.ToInt32(HdfContractId.Value);
            objCRDetails.CRProjectCode = HdfProjectCode.Value.Trim();

            if (checkCRReferenceNo(objCRDetails))
            {
                objCRDetails.CRStartDate = Convert.ToDateTime(ucDatePickerCRStartDate.Text.Trim());
                objCRDetails.CREndDate = Convert.ToDateTime(ucDatePickerCREndDate.Text.Trim());
                // 26595-Ambar-Start
                DateTime ldt_contractstartdate;
                ldt_contractstartdate = Convert.ToDateTime(ucContractStartDate.Text.Trim());

                if ((objCRDetails.CRStartDate < ldt_contractstartdate))
                {
                    lblMandatory.Text = string.Empty;
                    lblMandatory.Text = " CR start date can not be less than contract start date";
                    lblMandatory.Text = "<font color=RED>" + lblMandatory.Text + "</font>";
                    return;
                }
                // 26595-Ambar-End
                objCRDetails.CRRemarks = txtRemarks.Text.Trim();
                objCRDetails.CRId = 0;
                objCRDetails.Mode = "1";

                CRDetailsCollection.Add(objCRDetails);

                DoDataBind();

                this.ClearCRControls();

                lblMandatory.Text = string.Empty;
                lblMandatory.Text = " Please click on the Save button to save the CR details ";
                lblMandatory.Text = "<font color=Blue>" + lblMandatory.Text + "</font>";
            }
            else
            {
                lblMandatory.Text = string.Empty;
                lblMandatory.Text = "CR Reference No, entered already exist for project, kindly enter a different CR Reference No";
                lblMandatory.Text = "<font color=RED>" + lblMandatory.Text + "</font>";
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddContract.aspx", "Page_Load", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnCancelCRRow_Click(object sender, EventArgs e)
    {
        try
        {
            ClearCRControls();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddContract.aspx", "Page_Load", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void ddlClientName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            this.Get_ClientAbbrivation();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddContract.aspx", "Page_Load", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    #endregion

    #region Private Mathods

    /// <summary>
    /// function to get and bind the data to ContractTypeDropDown.
    /// </summary>

    private void GetMasterData_ContractTypeDropDown()
    {
        try
        {
            List<BusinessEntities.Master> objContractStatus = new List<BusinessEntities.Master>();
            //List<BusinessEntities.Contract> objContractStatus = new List<BusinessEntities.Contract>();

            Rave.HR.BusinessLayer.Common.Master objMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            objContractStatus = objMasterBAL.GetMasterData(ContractType);
            ddlContractType.DataSource = objContractStatus;
            ddlContractType.DataTextField = MasterName;
            ddlContractType.DataValueField = MasterID;
            ddlContractType.DataBind();
            ddlContractType.Items.Insert(0, new ListItem(SELECTONE, ZERO));

            //Checks this condition FOr Add mode.
            if (mode == Mode.Add.ToString())
            {
                //Check if user belongs to  CFM/FM and  RPM/Presales ,can add internal & external contract type.
                if ((Rave.HR.BusinessLayer.Contract.ContractRoles.CheckRolesAddInternalContract()) && (Rave.HR.BusinessLayer.Contract.ContractRoles.CheckRolesAddExternalContract()))
                {
                    // no need to remove any item from dropdown.
                }

                //Check if user is CFM/FM than only rave internal contract type can be added.
                else if (Rave.HR.BusinessLayer.Contract.ContractRoles.CheckRolesAddExternalContract() && mode != Mode.View.ToString())
                {
                    ddlContractType.Items.Remove(new ListItem("Rave Internal", "116"));
                }

                //Check if user is RPM/Presales than only rave internal contract type can be added.
                else if (Rave.HR.BusinessLayer.Contract.ContractRoles.CheckRolesAddInternalContract() && mode != Mode.View.ToString())
                {
                    ddlContractType.Items.Remove(new ListItem("MSA", "107"));
                    ddlContractType.Items.Remove(new ListItem("PO", "108"));
                    ddlContractType.Items.Remove(new ListItem("SOW", "109"));
                    ddlContractType.Items.Remove(new ListItem("CR", "110"));
                    ddlContractType.Items.Remove(new ListItem("SORR", "111"));
                    ddlContractType.Items.Remove(new ListItem("Work Order", "112"));
                    ddlContractType.Items.Remove(new ListItem("Temporary", "113"));
                    ddlContractType.Items.Remove(new ListItem("Invoice", "114"));
                }
            }

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionGetMasterData_ContractTypeDropDown, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// function to get and bind the data to LocationDropDown.
    /// </summary>

    private void GetMasterData_LocationDropDown()
    {
        try
        {
            List<BusinessEntities.Master> objLocation = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master objContractLocation = new Rave.HR.BusinessLayer.Common.Master();

            //This region will fill the client location drop down with the valid location types.
            objLocation = objContractLocation.GetMasterData(Convert.ToString(Location));
            ddlLocation.DataSource = objLocation;
            ddlLocation.DataValueField = MasterID;
            ddlLocation.DataTextField = MasterName;
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem(SELECTONE, ZERO));

            //This region will fill the Project location drop down with the valid location types.
            objLocation = objContractLocation.GetMasterData(Convert.ToString(ProjectLocation));

            ddlPrjLocation.DataSource = objLocation;
            ddlPrjLocation.DataValueField = MasterID;
            ddlPrjLocation.DataTextField = MasterName;
            ddlPrjLocation.DataBind();
            ddlPrjLocation.Items.Insert(0, new ListItem(SELECTONE, ZERO));
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionGetMasterData_LocationDropDown, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// function to get and bind the data to ContractStatus.
    /// </summary>

    private void GetMasterData_ContractStatus()
    {
        try
        {
            List<BusinessEntities.Master> objStatus = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master objContractStatus = new Rave.HR.BusinessLayer.Common.Master();

            //This region will fill the Contract Status drop down with the valid location types.
            objStatus = objContractStatus.GetMasterData(ContractStatus);
            ddlContractStatus.DataSource = objStatus;
            ddlContractStatus.DataValueField = MasterID;
            ddlContractStatus.DataTextField = MasterName;
            ddlContractStatus.DataBind();
            ddlContractStatus.Items.Insert(0, new ListItem(SELECTONE, ZERO));
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionGetMasterData_ContractStatus, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// function to get and bind the data to ClientName.
    /// </summary>

    private void GetMasterData_ClientName()
    {
        try
        {
            List<BusinessEntities.Master> objName = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master objCientName = new Rave.HR.BusinessLayer.Common.Master();

            //This region will fill the Contract Status drop down with the valid location types.
            objName = objCientName.GetMasterData(ClientName);
            ddlClientName.DataSource = objName;
            ddlClientName.DataValueField = MasterID;
            ddlClientName.DataTextField = MasterName;
            ddlClientName.DataBind();
            ddlClientName.Items.Insert(0, new ListItem(SELECTONE, ZERO));
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionGetMasterData_ClientName, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// function to get and bind the data to TypeOfProject.
    /// </summary>

    private void GetMasterData_TypeOfProject()
    {
        try
        {
            List<BusinessEntities.Master> objType = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master objTypeOfProject = new Rave.HR.BusinessLayer.Common.Master();

            //This region will fill the Contract Status drop down with the valid location types.
            objType = objTypeOfProject.GetMasterData(ProjectType);
            ddlTypeOfPrj.DataSource = objType;
            ddlTypeOfPrj.DataValueField = MasterID;
            ddlTypeOfPrj.DataTextField = MasterName;
            ddlTypeOfPrj.DataBind();
            ddlTypeOfPrj.Items.Insert(0, new ListItem(SELECTONE, ZERO));
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionGetMasterData_TypeOfProject, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// function to get and bind the data to AccountManager.
    /// </summary>

    private void GetMasterData_AccountManager()
    {
        try
        {
            List<BusinessEntities.Master> objType = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master objTypeOfProject = new Rave.HR.BusinessLayer.Common.Master();

            //This region will fill the Contract Status drop down with the valid location types.
            objType = objTypeOfProject.GetEmployeeDesignationWise(AccountManager);
            ddlAccountManager.DataSource = objType;
            ddlAccountManager.DataValueField = MasterID;
            ddlAccountManager.DataTextField = MasterName;
            ddlAccountManager.DataBind();
            ddlAccountManager.Items.Insert(0, new ListItem(SELECTONE, ZERO));

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionGetMasterData_AccountManager, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Gets the contract control data.
    /// </summary>
    /// <returns></returns>

    private BusinessEntities.Contract GetContractControldata()
    {
        try
        {
            BusinessEntities.Contract addContract = new BusinessEntities.Contract();
            style = NumberStyles.Number | NumberStyles.AllowCurrencySymbol;
            culture = CultureInfo.CreateSpecificCulture(ENGB);

            if (!txtContractCode.Text.Equals(""))
            {
                string[] ContractID = (txtContractCode.Text).Split('C');
                addContract.ContractID = Convert.ToInt32(ContractID[1]);
            }
            if (string.IsNullOrEmpty(txtContractValue.Text.Trim()))
            {
                txtContractValue.Text = "0";
            }
            decimal ContractValue = Convert.ToDecimal(txtContractValue.Text, culture);
            addContract.ContractCode = txtContractCode.Text;
            addContract.ContractTypeName = ddlContractType.SelectedItem.Text;
            addContract.AccountManagerID = Convert.ToInt32(ddlAccountManager.SelectedValue);
            addContract.AccountManagerName = Convert.ToString(ddlAccountManager.Text);
            addContract.ContractStatus = ddlContractStatus.Text;
            addContract.ContractReferenceID = txtContractrefId.Text;
            addContract.DocumentName = txtDocumentName.Text;
            addContract.EmailID = txtEmailId.Text;
            addContract.ClientName = ddlClientName.SelectedValue;
            addContract.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
            addContract.CreatedByEmailId = UserMailId;
            addContract.ContractType = ddlContractType.SelectedValue;
            addContract.ContractValue = ContractValue;
            addContract.CurrencyType = Convert.ToInt32(ddlcurrencyType.SelectedValue);
            //Populating Division and sponsor.
            addContract.Division = txtDivision.Text.ToString();
            addContract.Sponsor = txtSponsor.Text.ToString();

            addContract.ClinetAbbrivation = txtClientAbbr.Text.ToString();
            addContract.ProjectAbbrivation = txtProjectAbbr.Text.ToString();
            addContract.Phase = txtPhase.Text.ToString();
            addContract.ContractStartDate = Convert.ToDateTime(ucContractStartDate.Text);
            addContract.ContractEndDate = Convert.ToDateTime(ucContractEndDate.Text);

            //Siddharth 13 March 2015 Start
            addContract.ProjectModel = ddlProjectModel.SelectedValue;
            //Siddharth 13 March 2015 End

            //Siddharth 8 Sept 2015 Start
            addContract.BusinessVertical = ddlBusinessVertical.SelectedValue;
            //Siddharth 8 Sept 2015 End


            // 26114-Subhra-Start
            if (mode == Mode.Update.ToString())
            {
                addContract.TempAccountManagerName = Convert.ToString(ddlAccountManager.SelectedItem.Text);
                addContract.TempCurrencyName = Convert.ToString(ddlcurrencyType.SelectedItem.Text);

                objViewContract = (BusinessEntities.Contract)ViewState["objViewContract"];

                if (objViewContract.PreviousAccountManagerName != ddlAccountManager.SelectedItem.Text)
                    addContract.PreviousAccountManagerName = objViewContract.PreviousAccountManagerName;

                if (objViewContract.PreviousContractEndDate != Convert.ToDateTime(ucContractEndDate.Text))
                    addContract.PreviousContractEndDate = Convert.ToDateTime(objViewContract.PreviousContractEndDate);

                if (objViewContract.PreviousContractReferenceID != txtContractrefId.Text)
                    addContract.PreviousContractReferenceID = objViewContract.PreviousContractReferenceID;

                if (objViewContract.PreviousContractStartDate != Convert.ToDateTime(ucContractStartDate.Text))
                    addContract.PreviousContractStartDate = objViewContract.PreviousContractStartDate;

                if (objViewContract.PreviousContractType != ddlContractType.SelectedItem.Text)
                    addContract.PreviousContractType = objViewContract.PreviousContractType;

                if (objViewContract.PreviousContractValue != Convert.ToDecimal(txtContractValue.Text))
                    addContract.PreviousContractValue = objViewContract.PreviousContractValue;

                if (objViewContract.PreviousCurrencyType != ddlcurrencyType.SelectedItem.Text)
                    addContract.PreviousCurrencyType = objViewContract.PreviousCurrencyType;

                if (objViewContract.PreviousDocumentName != txtDocumentName.Text)
                    addContract.PreviousDocumentName = objViewContract.PreviousDocumentName;
            }
            // 26114-Subhra-End


            return addContract;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionGetContractControldata, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Gets the project Control data.
    /// </summary>
    /// <returns></returns>

    private BusinessEntities.ContractProject GetProjectControldata()
    {
        try
        {
            BusinessEntities.ContractProject contractProject = new BusinessEntities.ContractProject();

            contractProject.ProjectCode = txtProjectCode.Text;
            contractProject.ProjectType = ddlTypeOfPrj.SelectedItem.Text;
            contractProject.ProjectName = txtPrjName.Text;
            if (ucDatePickerStart.Text.Trim() != "")
            {
                contractProject.ProjectStartDate = Convert.ToDateTime(ucDatePickerStart.Text.Trim());
            }
            if (ucDatePickerEnd.Text.Trim() != "")
            {
                contractProject.ProjectEndDate = Convert.ToDateTime(ucDatePickerEnd.Text.Trim());
            }
            if (txtNoOfResources.Text != "")
            {
                contractProject.NoOfResources = Convert.ToDecimal(txtNoOfResources.Text, culture);
            }
            else
            {
                contractProject.NoOfResources = 0M;
            }
            if (!ddlPrjLocation.SelectedIndex.Equals(0))
            {
                contractProject.ProjectLocationID = Convert.ToInt32(ddlPrjLocation.SelectedValue);
                contractProject.ProjectLocationName = ddlPrjLocation.SelectedItem.Text.ToString();
            }
            contractProject.ProjectCategoryID = Convert.ToInt32(ddlProjectCategory.SelectedValue);
            contractProject.ProjectCategoryName = ddlProjectCategory.SelectedItem.Text;
            // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
            // Desc : Add project group in Contract page
            contractProject.ProjectGroup = ddlPrjGroup.SelectedItem.Text;
            // Mohamed : Issue 49791 : 15/09/2014 : Ends
            // Mohamed : Issue  : 23/09/2014 : Starts                        			  
            // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page
            contractProject.ProjectDivision = Convert.ToInt32(ddlPrjDivision.SelectedValue);
            contractProject.ProjectBussinessArea = ddlPrjBusinessArea.Enabled ? Convert.ToInt32(ddlPrjBusinessArea.SelectedValue) : 0;
            contractProject.ProjectBussinessSegment = ddlPrjBusinessSegment.Enabled ? Convert.ToInt32(ddlPrjBusinessSegment.SelectedValue) : 0;

            contractProject.BusinessVertical = ddlBusinessVertical.Enabled ? ddlBusinessVertical.SelectedValue : "0";
            contractProject.ProjectModel = ddlProjectModel.Enabled ? ddlProjectModel.SelectedValue : "0";

            // contractProject.BusinessVertical =ddlBusinessVertical.SelectedValue ;
            // contractProject.ProjectModel = ddlProjectModel.SelectedValue ;

            //contractProject.ProjectAlias = txtPrjAlias.Text;
            //contractProject.ProjectAlias = string.Empty;
            // Mohamed : Issue  : 23/09/2014 : Ends
            return contractProject;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionGetProjectControldata, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }

    }


    /// <summary>
    /// function to save contract and its project.
    /// </summary>
    /// <param name="addContractDetails"></param>
    /// <param name="addProjectDetails"></param>
    /// <returns></returns>

    private int Save(BusinessEntities.Contract addContractDetails, DataTable addProjectDetails)
    {
        try
        {
            BusinessEntities.ContractProject ProjectData = new BusinessEntities.ContractProject();
            Rave.HR.BusinessLayer.Contracts.Contract saveContract = new Rave.HR.BusinessLayer.Contracts.Contract();
            addContractDetails.ContractClientName = ddlClientName.SelectedItem.Text;
            contractId = saveContract.Save(addContractDetails, addProjectDetails);

            ClearControls();
            ReadOnlyControls();
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionSave, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
        return contractId;
    }


    /// <summary>
    /// Function to bind data in view mode.
    /// </summary>
    /// <param name="contractId"></param>
    /// <param name="SortDir"></param>
    /// <param name="SortExpression"></param>

    private void ViewContractDetails(int contractId, string SortDir, string SortExpression)
    {
        //Hashtable hashContract = new Hashtable();
        try
        {
            //As in view mode lable text is changed to "View contract".
            lblAddContract.Text = "View Contract";

            PanelProjectDetails.Visible = true;
            lblContractStatus.Visible = true;
            lblContractStatus.Visible = true;
            ddlContractStatus.Visible = true;
            lblContractCode.Visible = true;
            txtContractCode.Visible = true;

            gvProjectDetails.Visible = true;

            //Business entity of type contract is created.
            objViewContract = new BusinessEntities.Contract();

            objContractProject = new List<BusinessEntities.ContractProject>();

            //ObjViewContract's contract Id is set equal to the contract id recived to the function .
            objViewContract.ContractID = contractId;

            //Created a List type object of "BusinessEntities.Contract".
            List<BusinessEntities.Contract> lstViewContract = new List<BusinessEntities.Contract>();

            //Created a new object of "Rave.HR.BusinessLayer.Contracts.Contract" to acess its function.
            Rave.HR.BusinessLayer.Contracts.Contract objViewContractBL = new Rave.HR.BusinessLayer.Contracts.Contract();

            //Call the BL layer Function to fetch the view data.
            objViewContract = objViewContractBL.GetContractDetails(objViewContract, SortDir, SortExpression);


            objContractProject = objViewContractBL.GetContractProjectDetails(objViewContract.ContractID, SortDir, SortExpression);

            BindDataToContract(objViewContract);

            BindDataToProject(objContractProject, Mode.Add);

            DataTable viewDatatable = new DataTable();
            viewDatatable.Columns.Add(DbTableColumn.Con_ProjectCode);
            viewDatatable.Columns.Add(DbTableColumn.Con_ProjectName);
            viewDatatable.Columns.Add(DbTableColumn.ProjectType);
            viewDatatable.Columns.Add(DbTableColumn.ProjectTypeID);
            viewDatatable.Columns.Add(DbTableColumn.ProjectStartDate);
            viewDatatable.Columns.Add(DbTableColumn.ProjectEndDate);
            viewDatatable.Columns.Add(DbTableColumn.NoOfResources);
            viewDatatable.Columns.Add(DbTableColumn.ProjectLocation);
            viewDatatable.Columns.Add(DbTableColumn.Description);
            viewDatatable.Columns.Add(DbTableColumn.Con_ProjectCategoryID);
            viewDatatable.Columns.Add(DbTableColumn.Con_ProjectCategoryName);
            viewDatatable.Columns.Add(DbTableColumn.Con_StatusID);
            // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
            // Desc : Add project group in Contract page            
            viewDatatable.Columns.Add(DbTableColumn.ProjectGroup);
            // Mohamed : Issue 49791 : 15/09/2014 : Ends
            // Mohamed : Issue  : 23/09/2014 : Starts                        			  
            // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
            viewDatatable.Columns.Add(DbTableColumn.ProjectDivision);
            viewDatatable.Columns.Add(DbTableColumn.ProjectBusinessArea);
            viewDatatable.Columns.Add(DbTableColumn.ProjectBusinessSegment);
            //viewDatatable.Columns.Add(DbTableColumn.ProjectAlias);
            // Mohamed : Issue  : 23/09/2014 : Ends

            //Siddharth 9 Sept 2015 Start
            viewDatatable.Columns.Add(DbTableColumn.ProjectModel);
            viewDatatable.Columns.Add(DbTableColumn.BusinessVertical);
            //Siddharth 9 Sept 2015 Start

            foreach (BusinessEntities.ContractProject itemContractProject in objContractProject)
            {
                DataRow viewdr = viewDatatable.NewRow();
                viewdr[DbTableColumn.Con_ProjectCode] = itemContractProject.ProjectCode;
                viewdr[DbTableColumn.Con_ProjectName] = itemContractProject.ProjectName;
                viewdr[DbTableColumn.ProjectType] = itemContractProject.ProjectType;
                //viewdr["ProjectTypeID"] = itemContractProject.ProjectTypeID;
                viewdr[DbTableColumn.ProjectStartDate] = itemContractProject.ProjectStartDate.ToString(dateFormat);
                viewdr[DbTableColumn.ProjectEndDate] = itemContractProject.ProjectEndDate.ToString(dateFormat);
                viewdr[DbTableColumn.ProjectLocation] = itemContractProject.ProjectLocationName;
                viewdr[DbTableColumn.NoOfResources] = itemContractProject.NoOfResources;
                viewdr[DbTableColumn.Description] = itemContractProject.ProjectsDescription;
                viewdr[DbTableColumn.Con_ProjectCategoryID] = itemContractProject.ProjectCategoryID;
                viewdr[DbTableColumn.Con_ProjectCategoryName] = itemContractProject.ProjectCategoryName;
                viewdr[DbTableColumn.Con_StatusID] = itemContractProject.StatusID;
                // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
                // Desc : Add project group in Contract page            
                viewdr[DbTableColumn.ProjectGroup] = itemContractProject.ProjectGroup;
                // Mohamed : Issue 49791 : 15/09/2014 : Ends
                // Mohamed : Issue  : 23/09/2014 : Starts                        			  
                // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
                viewdr[DbTableColumn.ProjectDivision] = itemContractProject.ProjectDivision;
                viewdr[DbTableColumn.ProjectBusinessArea] = itemContractProject.ProjectBussinessArea;
                viewdr[DbTableColumn.ProjectBusinessSegment] = itemContractProject.ProjectBussinessSegment;
                //viewdr[DbTableColumn.ProjectAlias] = itemContractProject.ProjectAlias;
                // Mohamed : Issue  : 23/09/2014 : Ends

                //Siddharth 9 Sept 2015 Start
                viewdr[DbTableColumn.ProjectModel] = itemContractProject.ProjectModel;
                viewdr[DbTableColumn.BusinessVertical] = itemContractProject.BusinessVertical;
                //Siddharth 9 Sept 2015 Start

                viewDatatable.Rows.Add(viewdr);
            }

            Session[SessionNames.CONTRACT_PROJECT_DATA] = viewDatatable;


            // Session[SessionNames.CONTRACT_EDIT_PROJECT_DETAILS] = objContractProject;

            isDatePassed = CompairDateOfProjectEndDt(objContractProject);
            isEmployeesReleased = CheckEmpAllReleased(objContractProject);

            if (isDatePassed != true)
            {
                if (isEmployeesReleased == true)
                {
                    contractStatusChangeAllowed = true;
                }
            }

            if (objViewContract.ContractType == CommonConstants.CONTRACT_INTERNAL && Rave.HR.BusinessLayer.Contract.ContractRoles.CheckRolesAddInternalContract())
            {
                btnSave.Visible = true;
                btnSave.Text = DELETE;
                btnEdit.Visible = true;
            }
            else if (objViewContract.ContractType != CommonConstants.CONTRACT_INTERNAL && Rave.HR.BusinessLayer.Contract.ContractRoles.CheckRolesAddExternalContract())
            {
                btnSave.Visible = true;
                btnSave.Text = DELETE;
                btnEdit.Visible = true;
            }
            else
            {
                btnEdit.Visible = false;
            }
            //If contracts does not contain any project then only delete button should be visible.
            if (objContractProject.Count > 0)
            {
                btnSave.Visible = false;
            }
            else
            {
                if ((objViewContract.ContractType == CommonConstants.CONTRACT_INTERNAL && Rave.HR.BusinessLayer.Contract.ContractRoles.CheckRolesAddInternalContract()) || (objViewContract.ContractType != CommonConstants.CONTRACT_INTERNAL && Rave.HR.BusinessLayer.Contract.ContractRoles.CheckRolesAddExternalContract()))
                {
                    btnSave.Visible = true;
                }
                else
                {
                    btnSave.Visible = false;
                }
                PanelProjectDetails.Visible = false;
            }
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionViewContractDetails, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// This function is used to bind data to contract details.
    /// </summary>
    /// <param name="objViewContract"></param>
    /// <returns></returns>

    private void BindDataToContract(BusinessEntities.Contract objViewContract)
    {
        try
        {
            if (objViewContract != null)
            {
                //lblMandatory.Visible = false;
                txtContractCode.Text = objViewContract.ContractCode;
                txtContractrefId.Text = objViewContract.ContractReferenceID;
                txtDocumentName.Text = objViewContract.DocumentName;
                txtEmailId.Text = objViewContract.EmailID;
                if (objViewContract.ContractValue > 0)
                    txtContractValue.Text = String.Format("{0:0.##}", objViewContract.ContractValue);
                //Populating Sponsor textbox
                if (objViewContract.Sponsor != null)
                {
                    if (!objViewContract.Sponsor.Equals(string.Empty))
                    {
                        txtSponsor.Text = objViewContract.Sponsor.ToString();
                    }
                }
                //Populating Division textbox
                if (objViewContract.Division != null)
                {
                    if (!objViewContract.Division.Equals(string.Empty))
                    {
                        txtDivision.Text = objViewContract.Division.ToString();
                    }
                }

                ddlClientName.ClearSelection();
                if (objViewContract.ClientName != null)
                {
                    ddlClientName.Items.FindByText(objViewContract.ClientName).Selected = true;
                }
                //Clears and sets the selection for all the dropdown.
                ddlLocation.ClearSelection();
                if (objViewContract.LocationName != null)
                {
                    ddlLocation.Items.FindByText(objViewContract.LocationName).Selected = true;
                }
                ddlAccountManager.ClearSelection();

                //if no account manager  exist in employee table.
                if (ddlAccountManager.Items.Count > 1)
                {
                    #region Modified By Mohamed Dangra
                    // Mohamed : Issue 50557 : 21/04/2014 : Starts                        			  
                    // Desc : Add Account Manager name in Drop down when the Account manger is other than default

                    if (ddlAccountManager.Items.FindByValue(objViewContract.AccountManagerID.ToString()) == null)
                    {
                        ddlAccountManager.Items.Insert(ddlAccountManager.Items.Count, new ListItem(objViewContract.AccountManagerName, objViewContract.AccountManagerID.ToString()));
                        ddlAccountManager.Items[ddlAccountManager.Items.Count - 1].Selected = true;
                    }
                    else
                    {
                        ddlAccountManager.Items.FindByValue(objViewContract.AccountManagerID.ToString()).Selected = true;
                    }
                    // Mohamed : Issue 50557 : 21/04/2014 : Ends
                    #endregion Modified By Mohamed Dangra
                }


                ddlContractType.ClearSelection();
                if (objViewContract.ContractType != null)
                {
                    ddlContractType.Items.FindByText(objViewContract.ContractType).Selected = true;
                }
                ddlContractStatus.ClearSelection();
                if (objViewContract.ContractStatus != null)
                {
                    ddlContractStatus.Items.FindByText(objViewContract.ContractStatus).Selected = true;
                }
                ddlcurrencyType.ClearSelection();
                ddlcurrencyType.Items.FindByValue(objViewContract.CurrencyType.ToString()).Selected = true;

                //Siddharth 12 March 2015 Start
                ddlProjectModel.ClearSelection();
                if (objViewContract.ProjectModel != null)
                {
                    if (!string.IsNullOrEmpty(objViewContract.ProjectModel.ToString()))
                    {
                        if (ddlProjectModel.Items.FindByValue(objViewContract.ProjectModel.ToString()) != null)
                        {
                            ddlProjectModel.Items.FindByValue(objViewContract.ProjectModel.ToString()).Selected = true;
                        }
                    }
                    else
                    {
                        //ddlProjectModel.Items.FindByValue(CommonConstants.SELECT).Selected = true;
                        ddlProjectModel.Items[0].Selected = true;
                    }
                }
                else
                {
                    //ddlProjectModel.Items.FindByValue(CommonConstants.SELECT).Selected = true;
                    ddlProjectModel.Items[0].Selected = true;
                }
                //Siddharth 12 March 2015 End


                //Siddharth 3 August 2015 Start
                ddlBusinessVertical.ClearSelection();
                if (!string.IsNullOrEmpty(objViewContract.BusinessVertical))
                {
                    if (ddlBusinessVertical.Items.FindByValue(objViewContract.BusinessVertical.ToString()) != null)
                    {
                        ddlBusinessVertical.Items.FindByValue(objViewContract.BusinessVertical.ToString()).Selected = true;
                    }
                }
                else
                {
                    //ddlProjectModel.Items.FindByValue(CommonConstants.SELECT).Selected = true;
                    ddlBusinessVertical.Items[0].Selected = true;
                }
                //Siddharth 3 August 2015 End

                ucContractStartDate.Text = objViewContract.ContractStartDate.ToString("dd/MM/yyyy");

                ucContractEndDate.Text = objViewContract.ContractEndDate.ToString("dd/MM/yyyy");

                txtClientAbbr.Text = objViewContract.ClinetAbbrivation;

                //26114-Subhra-Start
                objViewContract.PreviousContractReferenceID = objViewContract.ContractReferenceID;
                objViewContract.PreviousDocumentName = objViewContract.DocumentName;
                objViewContract.PreviousContractReferenceID = objViewContract.ContractReferenceID;
                objViewContract.PreviousDocumentName = objViewContract.DocumentName;
                objViewContract.PreviousContractValue = objViewContract.ContractValue;
                objViewContract.PreviousAccountManagerName = ddlAccountManager.SelectedItem.Text;
                objViewContract.PreviousContractType = objViewContract.ContractType;
                //objViewContract.PreviousCurrencyType = objViewContract.CurrencyType;
                objViewContract.PreviousCurrencyType = ddlcurrencyType.SelectedItem.Text;
                objViewContract.PreviousContractStartDate = objViewContract.ContractStartDate;
                objViewContract.PreviousContractEndDate = objViewContract.ContractEndDate;
                objViewContract.PreviousContractValue = objViewContract.ContractValue;
                ViewState["objViewContract"] = objViewContract;
                //26114-Subhra-End
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionBindDataToContract, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// This function is used to bind data to the project details.
    /// </summary>
    /// <param name="objContractProject"></param>
    /// <returns>void</returns>  
    private void BindDataToProject(List<BusinessEntities.ContractProject> objContractProject, Mode mode)
    {
        try
        {
            //Checks if objContractProject is null or not 
            if (objContractProject != null)
            {
                Session[SessionNames.CONTRACT_GRIDDATA] = objContractProject;
                gvProjectDetails.DataSource = null;
                gvProjectDetails.DataSource = objContractProject;
                gvProjectDetails.DataBind();
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionBindDataToProject, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Clears all the controls.
    /// </summary>
    private void ClearControls()
    {
        try
        {
            //Clears the text box controls to null.
            txtContractCode.Text = string.Empty;
            txtContractrefId.Text = string.Empty;
            txtDocumentName.Text = string.Empty;
            txtEmailId.Text = string.Empty;
            txtNoOfResources.Text = string.Empty;
            txtPrjName.Text = string.Empty;
            txtProjectCode.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtContractValue.Text = string.Empty;
            txtDivision.Text = string.Empty;
            txtSponsor.Text = string.Empty;
            ucDatePickerEnd.Text = string.Empty;
            ucDatePickerStart.Text = string.Empty;
            txtProjectAbbr.Text = string.Empty;
            txtClientAbbr.Text = string.Empty;
            ucContractStartDate.Text = string.Empty;
            ucContractEndDate.Text = string.Empty;
            txtPhase.Text = string.Empty;

            //clears the selection from the drop down controls.

            ddlAccountManager.ClearSelection();
            ddlClientName.ClearSelection();
            ddlContractStatus.ClearSelection();
            ddlContractType.ClearSelection();
            ddlLocation.ClearSelection();
            ddlPrjLocation.ClearSelection();
            ddlTypeOfPrj.ClearSelection();
            ddlcurrencyType.ClearSelection();
            ddlProjectCategory.ClearSelection();
            // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
            // Desc : Add project group in Contract page
            ddlPrjGroup.ClearSelection();
            // Mohamed : Issue 49791 : 15/09/2014 : Ends

            // Mohamed : Issue  : 23/09/2014 : Starts                        			  
            // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page
            ddlPrjDivision.ClearSelection();
            ddlPrjBusinessArea.ClearSelection();
            ddlPrjBusinessSegment.ClearSelection();
            //txtPrjAlias.Text = string.Empty;
            ddlPrjBusinessArea.Enabled = false;
            ddlPrjBusinessSegment.Enabled = false;
            // Mohamed : Issue  : 23/09/2014 : Ends

            //Siddharth 13 March 2015 Start
            ddlProjectModel.ClearSelection();
            ddlProjectModel.Enabled = false;
            //Siddharth 13 March 2015 End

            //Siddharth 9 Sept 2015 Start
            ddlBusinessVertical.ClearSelection();
            ddlBusinessVertical.Enabled = false;
            //Siddharth 9 Sept 2015 End

            //Clears the records from grid view control
            gvProjectDetails.DataSource = "";
            gvProjectDetails.DataBind();
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionClearControls, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Make all the fields read only.
    /// </summary>
    private void ReadOnlyControls()
    {
        try
        {
            txtContractCode.Enabled = false;
            txtContractrefId.Enabled = false;
            txtContractrefId.Enabled = false;
            //txtDocumentName.ReadOnly = true;
            txtDocumentName.Enabled = false;
            txtEmailId.Enabled = false;
            txtNoOfResources.Enabled = false;
            txtPrjName.Enabled = false;
            txtProjectCode.Enabled = false;
            txtContractValue.Enabled = false;
            txtSponsor.Enabled = false;
            txtDivision.Enabled = false;
            ucDatePickerStart.IsEnable = false;
            ucDatePickerEnd.IsEnable = false;
            ucContractStartDate.IsEnable = false;
            ucContractEndDate.IsEnable = false;
            txtClientAbbr.Enabled = false;

            //makes all the drop downs to read only.
            ddlAccountManager.Enabled = false;
            ddlClientName.Enabled = false;
            ddlContractStatus.Enabled = false;
            ddlContractType.Enabled = false;
            ddlLocation.Enabled = false;
            ddlPrjLocation.Enabled = false;
            ddlTypeOfPrj.Enabled = false;
            ddlcurrencyType.Enabled = false;
            ddlProjectCategory.Enabled = false;
            // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
            // Desc : Add project group in Contract page
            ddlPrjGroup.Enabled = false;
            // Mohamed : Issue 49791 : 15/09/2014 : Ends

            // Mohamed : Issue  : 23/09/2014 : Starts                        			  
            // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page
            ddlPrjDivision.Enabled = false;
            ddlPrjBusinessArea.Enabled = false;
            ddlPrjBusinessSegment.Enabled = false;
            //txtPrjAlias.Enabled = false;
            // Mohamed : Issue  : 23/09/2014 : Ends

            //Siddharth 13 March 2015 Start
            ddlProjectModel.Enabled = false;
            //Siddharth 13 March 2015 Ends

            //Siddharth 8 Sept 2015 Start
            ddlBusinessVertical.Enabled = false;
            //Siddharth 8 Sept 2015 Ends


            //hides the edit and delete columns of the project details grid view.
            //Please ensure after adding any Column to the Project details grid 
            //hide unhide the edit and delete columns.
            //gvProjectDetails.Columns[7].Visible = false;
            gvProjectDetails.Columns[8].Visible = false;
            gvProjectDetails.Columns[9].Visible = false;
            gvProjectDetails.Columns[10].Visible = false;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionReadOnlyControls, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// removes the read only property of the fields.
    /// </summary>
    private void RemoveReadOnlyControls()
    {
        try
        {
            //makes all the text boxes editable.

            txtContractrefId.Enabled = true;
            txtDocumentName.Enabled = true;
            txtNoOfResources.Enabled = true;
            txtPrjName.Enabled = true;
            txtProjectCode.Enabled = true;
            txtContractValue.Enabled = true;
            txtDivision.Enabled = true;
            txtSponsor.Enabled = true;
            ucDatePickerStart.IsEnable = true;
            ucDatePickerEnd.IsEnable = true;
            ucContractStartDate.IsEnable = true;
            ucContractEndDate.IsEnable = true;
            txtClientAbbr.Enabled = true;

            //makes all the drop downseditable.
            ddlAccountManager.Enabled = true;
            ddlClientName.Enabled = true;
            ddlContractType.Enabled = true;
            ddlLocation.Enabled = true;
            ddlPrjLocation.Enabled = true;
            ddlTypeOfPrj.Enabled = true;
            ddlcurrencyType.Enabled = true;
            // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
            // Desc : Add project group in Contract page
            ddlPrjGroup.Enabled = true;
            // Mohamed : Issue 49791 : 15/09/2014 : Ends

            // Mohamed : Issue  : 23/09/2014 : Starts                        			  
            // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page
            ddlPrjDivision.Enabled = true;
            //ddlPrjBusinessArea.Enabled = true;
            //ddlPrjBusinessSegment.Enabled = true;
            //txtPrjAlias.Enabled = true;
            // Mohamed : Issue  : 23/09/2014 : Ends

            //Siddharth 13 March 2015 Start
            ddlProjectModel.Enabled = true;
            //Siddharth 13 March 2015 Ends

            //Siddharth 9 Sept 2015 Start
            ddlBusinessVertical.Enabled = true;
            //Siddharth 9 Sept 2015 Ends

            lblProjectCategory.Enabled = true;
            ddlProjectCategory.Enabled = true;

            //hides the edit and delete columns of the project details grid view.
            //Please ensure after adding any Column to the Project details grid 
            //hide unhide the edit and delete columns.
            gvProjectDetails.Columns[7].Visible = true;
            gvProjectDetails.Columns[8].Visible = true;
            gvProjectDetails.Columns[9].Visible = true;
            gvProjectDetails.Columns[10].Visible = true;
            //gvProjectDetails.co


            //removes these buttons from the page.

            btnPrevious.Visible = false;
            btnNext.Visible = false;
            btnSave.Text = SaveConst;
            btnEdit.Visible = false;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionRemoveReadOnlyControls, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// binds data to project details Controls.
    /// </summary>
    /// <param name="ContractProject"></param>
    private void BindDataToProjectControls(BusinessEntities.ContractProject ContractProject)
    {
        try
        {
            txtPrjName.Text = ContractProject.ProjectName;
            txtProjectCode.Text = ContractProject.ProjectCode;
            HdfProjectCode.Value = ContractProject.ProjectCode;

            ucDatePickerStart.Text = ContractProject.ProjectStartDate.ToString(dateFormat);
            ucDatePickerEnd.Text = ContractProject.ProjectEndDate.ToString(dateFormat);

            txtNoOfResources.Text = ContractProject.NoOfResources.ToString();

            ddlPrjLocation.ClearSelection();

            if (ddlPrjLocation.Items.FindByValue(ContractProject.ProjectLocationName.ToString()) != null)
            {
                ddlPrjLocation.Items.FindByValue(ContractProject.ProjectLocationName.ToString()).Selected = true;
            }

            ddlTypeOfPrj.ClearSelection();

            if (ddlTypeOfPrj.Items.FindByValue(ContractProject.ProjectType) == null)
            {
                if (ddlTypeOfPrj.Items.FindByText(ContractProject.ProjectType) != null)
                {
                    ddlTypeOfPrj.Items.FindByText(ContractProject.ProjectType).Selected = true;
                }
            }
            else
            {
                ddlTypeOfPrj.Items.FindByValue(ContractProject.ProjectType).Selected = true;

            }

            // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
            // Desc : Add project group in Contract page
            ddlPrjGroup.ClearSelection();
            if (ddlPrjGroup.Items.FindByText(ContractProject.ProjectGroup) != null)
            {
                ddlPrjGroup.Items.FindByText(ContractProject.ProjectGroup).Selected = true;
            }
            // Mohamed : Issue 49791 : 15/09/2014 : Ends

            // Mohamed : Issue  : 23/09/2014 : Starts                        			  
            // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS

            ddlPrjDivision.ClearSelection();
            if (ddlPrjDivision.Items.FindByValue(ContractProject.ProjectDivision.ToString()) != null)
            {
                ddlPrjDivision.Items.FindByValue(ContractProject.ProjectDivision.ToString()).Selected = true;
            }

            ddlPrjBusinessArea.ClearSelection();
            if (ddlPrjBusinessArea.Items.FindByValue(ContractProject.ProjectBussinessArea.ToString()) != null)
            {
                ddlPrjBusinessArea.Items.FindByValue(ContractProject.ProjectBussinessArea.ToString()).Selected = true;
            }
            if (ContractProject.ProjectDivision.ToString().Equals(CommonConstants.Project_Division_PublicService))
            {
                ddlPrjBusinessArea.Enabled = true;
            }

            ddlPrjBusinessSegment.ClearSelection();
            if (ddlPrjBusinessSegment.Items.FindByValue(ContractProject.ProjectBussinessSegment.ToString()) != null)
            {
                ddlPrjBusinessSegment.Items.FindByValue(ContractProject.ProjectBussinessSegment.ToString()).Selected = true;
            }
            if (ContractProject.ProjectBussinessArea.ToString().Equals(CommonConstants.Project_BussinessArea_Solutions))
            {
                ddlPrjBusinessSegment.Enabled = true;
            }

            //txtPrjAlias.Text = ContractProject.ProjectAlias.ToString();
            // Mohamed : Issue  : 23/09/2014 : Ends

            //Siddharth 9 Sept 2015 Start
            ddlProjectModel.ClearSelection();
            if (ddlProjectModel.Items.FindByValue(ContractProject.ProjectModel.ToString()) != null)
            {
                ddlProjectModel.Items.FindByValue(ContractProject.ProjectModel.ToString()).Selected = true;
            }

            ddlBusinessVertical.ClearSelection();
            if (ddlBusinessVertical.Items.FindByValue(ContractProject.BusinessVertical.ToString()) != null)
            {
                ddlBusinessVertical.Items.FindByValue(ContractProject.BusinessVertical.ToString()).Selected = true;
            }
            //Siddharth 9 Sept 2015 End

            //vandana
            ddlProjectCategory.ClearSelection();
            //if (ddlProjectCategory.Items.FindByValue(ContractProject.ProjectCategoryName) == null)
            //{
            //    if (ddlProjectCategory.Items.FindByText(ContractProject.ProjectCategoryName) != null)
            //    {
            //        ddlProjectCategory.Items.FindByText(ContractProject.ProjectCategoryName).Selected = true;
            //    }
            //}
            //else
            //{
            //    ddlProjectCategory.Items.FindByValue(ContractProject.ProjectCategoryName).Selected = true;

            //}
            if (ddlProjectCategory.Items.FindByValue(ContractProject.ProjectCategoryID.ToString()) != null)
            {
                ddlProjectCategory.Items.FindByValue(ContractProject.ProjectCategoryID.ToString()).Selected = true;

            }
            //vandana
            if (ContractProject.ProjectsDescription.ToString() != null)
            {
                txtDescription.Text = ContractProject.ProjectsDescription.ToString();
            }



            Session[SessionNames.CONTRACT_EDIT_PROJECT_DETAILS] = ContractProject;

            //Make disable to controls if selected project is existing project or project is edited on click of grid edit button.
            //If new project is there so all controls should be enabled.
            if (IsNewProject.Value == NO || (IsGridEdited.Value == YES && ContractProject.ProjectCode != ""))
            {
                txtPrjName.Enabled = false;
                txtNoOfResources.Enabled = true;
                ddlPrjLocation.Enabled = false;
                ddlTypeOfPrj.Enabled = false;
                // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
                // Desc : Add project group in Contract page
                ddlPrjGroup.Enabled = true;
                // Mohamed : Issue 49791 : 15/09/2014 : Ends

                // Mohamed : Issue  : 23/09/2014 : Starts                        			  
                // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
                ddlPrjDivision.Enabled = true;
                //txtPrjAlias.Enabled = true;
                // Mohamed : Issue  : 23/09/2014 : Ends

                //Siddharth Changed here
                // ddlBusinessVertical.Enabled = true;
                // ddlProjectModel.Enabled = true;

                ucDatePickerEnd.IsEnable = false;
                ucDatePickerStart.IsEnable = false;
                //ddlProjectCategory.Enabled = false;


            }
            else
            {
                txtPrjName.Enabled = true;
                txtNoOfResources.Enabled = true;
                ddlPrjLocation.Enabled = true;
                ddlTypeOfPrj.Enabled = true;
                ucDatePickerEnd.IsEnable = true;
                ucDatePickerStart.IsEnable = true;
                // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
                // Desc : Add project group in Contract page
                ddlPrjGroup.Enabled = true;
                // Mohamed : Issue 49791 : 15/09/2014 : Ends

                // Mohamed : Issue  : 23/09/2014 : Starts                        			  
                // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
                ddlPrjDivision.Enabled = true;
                //txtPrjAlias.Enabled = true;
                // Mohamed : Issue  : 23/09/2014 : Ends

                ddlBusinessVertical.Enabled = true;
                ddlProjectModel.Enabled = true;

                ucDatePickerEnd.Visible = true;
                ucDatePickerStart.Visible = true;

            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionBindDataToProjectControls, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// makes the controls visible for Project details.
    /// </summary>
    private void UnhideProjectControls()
    {
        try
        {

            lblProjectCode.Visible = true;
            lblTypeOfProj.Visible = true;
            lblPrjName.Visible = true;
            lblNoOfResources.Visible = true;
            // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
            // Desc : Add project group in Contract page
            lblPrjGroup.Visible = true;
            MandatoryProjGroup.Visible = true;
            // Mohamed : Issue 49791 : 15/09/2014 : Ends
            lblStartDate.Visible = true;
            lblEndDate.Visible = true;
            lblPrjLocation.Visible = true;
            LabelProjectDescription.Visible = true;

            txtPrjName.Visible = true;
            txtProjectCode.Visible = true;
            ucDatePickerStart.Visible = true;
            ucDatePickerEnd.Visible = true;
            txtNoOfResources.Visible = true;
            txtDescription.Visible = true;

            ddlPrjLocation.Visible = true;
            ddlTypeOfPrj.Visible = true;
            // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
            // Desc : Add project group in Contract page
            ddlPrjGroup.Visible = true;
            // Mohamed : Issue 49791 : 15/09/2014 : Ends

            // Mohamed : Issue  : 23/09/2014 : Starts                        			  
            // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
            ddlPrjDivision.Visible = true;
            ddlPrjBusinessArea.Visible = true;
            ddlPrjBusinessSegment.Visible = true;
            //txtPrjAlias.Visible = true;
            lblPrjDivision.Visible = true;
            MandatoryProjectjDivision.Visible = true;
            lblPrjBusinessArea.Visible = true;
            MandatoryProjectjBusinessArea.Visible = true;
            lblPrjBusinessSegment.Visible = true;
            MandatoryProjectBusinessSegment.Visible = true;
            //lblPrjAlias.Visible = true;
            //MandatoryPrjAlias.Visible = true;
            // Mohamed : Issue  : 23/09/2014 : Ends

            //Siddharth 9th Sept 2015 Start
            ddlBusinessVertical.Visible = true;
            LblBusinessVertical.Visible = true;
            lblMandatorymarkBusinessVertical.Visible = true;
            ddlProjectModel.Visible = true;
            LblProjectModel.Visible = true;
            lblMandatorymarkProjectModel.Visible = true;
            //Siddharth 9th Sept 2015 End

            MandatoryProjectName.Visible = true;
            MandatoryStartDate.Visible = true;
            MandatoryTypeOfProj.Visible = true;
            MandatoryEndDate.Visible = true;
            //ControlForCFM();
            // 36233-Ambar-29062012-Start
            // Uncomment following             
            mandatoryProjectDescription.Visible = true;
            // 36233-Ambar-29062012-End



            ddlProjectCategory.Visible = true;
            MandatoryProjectCategory.Visible = true;
            lblProjectCategory.Visible = true;

            lblProjectAbbr.Visible = true;
            lblMandatoryProjectAbbr.Visible = true;
            txtProjectAbbr.Visible = true;

            lblPhase.Visible = true;
            lblMandatorymarkPhase.Visible = true;
            txtPhase.Visible = true;

            btnAddRow.Visible = true;
            btnPrjCancle.Visible = true;
            btnAddRow.Text = UPDATE;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionUnhideProjectControls, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// This function hides all the project Controls.
    /// </summary>
    private void HideProjectControls()
    {
        try
        {
            lblProjectCode.Visible = false;
            lblTypeOfProj.Visible = false;
            lblPrjName.Visible = false;
            lblNoOfResources.Visible = false;
            // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
            // Desc : Add project group in Contract page
            lblPrjGroup.Visible = false;
            MandatoryProjGroup.Visible = false;
            // Mohamed : Issue 49791 : 15/09/2014 : Ends
            lblStartDate.Visible = false;
            lblEndDate.Visible = false;
            lblPrjLocation.Visible = false;
            LabelProjectDescription.Visible = false;

            txtPrjName.Visible = false;
            txtProjectCode.Visible = false;

            txtNoOfResources.Visible = false;
            txtDescription.Visible = false;
            ucDatePickerStart.Visible = false;
            ucDatePickerEnd.Visible = false;

            ddlPrjLocation.Visible = false;
            ddlTypeOfPrj.Visible = false;
            ddlProjectCategory.Visible = false;
            // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
            // Desc : Add project group in Contract page
            ddlPrjGroup.Visible = false;
            // Mohamed : Issue 49791 : 15/09/2014 : Ends

            // Mohamed : Issue  : 23/09/2014 : Starts                        			  
            // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
            ddlPrjDivision.Visible = false;
            ddlPrjBusinessArea.Visible = false;
            ddlPrjBusinessSegment.Visible = false;
            //txtPrjAlias.Visible = false;
            lblPrjDivision.Visible = false;
            MandatoryProjectjDivision.Visible = false;
            lblPrjBusinessArea.Visible = false;
            MandatoryProjectjBusinessArea.Visible = false;
            lblPrjBusinessSegment.Visible = false;
            MandatoryProjectBusinessSegment.Visible = false;
            //lblPrjAlias.Visible = false;
            //MandatoryPrjAlias.Visible = false;
            // Mohamed : Issue  : 23/09/2014 : Ends

            //Siddharth 9th Sept 2015 Start
            LblBusinessVertical.Visible = false;
            ddlBusinessVertical.Visible = false;
            lblMandatorymarkBusinessVertical.Visible = false;
            LblProjectModel.Visible = false;
            ddlProjectModel.Visible = false;
            lblMandatorymarkProjectModel.Visible = false;
            //Siddharth 9th Sept 2015 End

            MandatoryProjectName.Visible = false;
            MandatoryStartDate.Visible = false;
            MandatoryTypeOfProj.Visible = false;
            MandatoryEndDate.Visible = false;
            mandatoryProjectDescription.Visible = false;

            btnAddRow.Visible = false;
            btnPrjCancle.Visible = false;
            PanelProjectDetails.Visible = false;

            lblProjectCategory.Visible = false;
            ddlProjectCategory.Visible = false;
            MandatoryProjectCategory.Visible = false;

            lblMandatoryProjectAbbr.Visible = false;
            lblProjectAbbr.Visible = false;
            txtProjectAbbr.Visible = false;

            lblMandatorymarkPhase.Visible = false;
            lblPhase.Visible = false;
            txtPhase.Visible = false;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionHideProjectControls, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// This function is called when a contract is edited.
    /// </summary>
    /// <param name="contractDetails"></param>
    /// <param name="contractProject"></param>
    private bool EditContract(BusinessEntities.Contract contractDetails, DataTable contractProject, BusinessEntities.RaveHRCollection CRDetailsCollection)
    {
        bool success = false;

        try
        {
            Rave.HR.BusinessLayer.Contracts.Contract editContract = new Rave.HR.BusinessLayer.Contracts.Contract();
            contractDetails.ContractClientName = ddlClientName.SelectedItem.Text;
            success = editContract.Edit(contractDetails, contractProject, CRDetailsCollection);

            if (success == true)
            {
                lblConfirmMessage.Text = "Contract and project details have been sucessfully updated, email notification is sent.";

                // 36041-Ambar-Start-21062012
                lblConfirmMessage.Text = "<font color=Blue>" + lblConfirmMessage.Text + "</font>";
                // 36041-Ambar-End-21062012

                lblContractStatus.Visible = false;
                ddlContractStatus.Visible = false;
            }
            ClearControls();
            return success;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionEditContract, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// This function is called to delete the contract.
    /// </summary>
    /// <param name="contractDetails"></param>
    /// <returns></returns>
    private bool DeleteContract(BusinessEntities.Contract contractDetails)//, List<BusinessEntities.ContractProject> contractProject)
    {
        //End date flag is set to true for the case when no project is associated with a 
        //contract that means no comparaison is required and default it should allow to delete.
        DateTime todaysDate = DateTime.Now;

        bool result = false;
        try
        {
            //Declared a BL object.
            objContractBL = new Rave.HR.BusinessLayer.Contracts.Contract();
            contractDetails.ContractClientName = ddlClientName.SelectedItem.Text; ;
            //Calls the bL layer function for deletion of the contract.
            result = objContractBL.Delete(contractDetails);

            ReadOnlyControls();
            tbReasonForDeletion.Enabled = false;
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionDeleteContract, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
        return result;
    }

    /// <summary>
    /// Compares the start date and end date.
    /// </summary>
    /// <param name="contractProject"></param>
    /// <returns></returns>
    private bool CompairDateOfProjectEndDt(List<BusinessEntities.ContractProject> contractProject)
    {
        try
        {
            bool endDateSmaller = true;
            //Checks for null contractProject.
            if (contractProject != null)
            {
                //Compaire for all projects associated with a contract.

                foreach (BusinessEntities.ContractProject project in contractProject)
                {
                    if (project.ProjectEndDate < DateTime.Now)
                    {
                        //when project End date is less then todays date then flage set to false 
                        //as Status change of contract is not allowed in this case.
                        endDateSmaller = false;
                    }
                }
            }

            //After compairing if all the projects that are associated with the contract
            //have their end date less then today date the contract status is set to inactive.

            return endDateSmaller;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionCompairDateOfProjectEndDt, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Checks if All employees associated to the Project are realeased or not.
    /// </summary>
    /// <param name="contractProject"></param>
    /// <returns></returns>
    private bool CheckEmpAllReleased(List<BusinessEntities.ContractProject> contractProject)
    {
        bool allEmpReleased = false;
        objContractProjectBL = new Rave.HR.BusinessLayer.Contracts.ContractProject();

        try
        {
            //Checks for null contractProject.
            if (contractProject != null)
            {
                //Compaire for all projects associated with a contract.

                foreach (BusinessEntities.ContractProject project in contractProject)
                {
                    allEmpReleased = objContractProjectBL.IsEmployeeAssociated(project);
                }
            }
            return allEmpReleased;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionCheckEmpAllReleased, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
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
            if (Session[SessionNames.CONTRACT_CONTRACTIDHASHTABLE] != null)
            {
                // 27633-Ambar-Start
                //ht = (Hashtable)Session[SessionNames.MRFVIEWINDEX];
                ht = (Hashtable)Session[SessionNames.CONTRACTPREVIOUSHASHTABLE];
                // 27633-Ambar-End


                if (ht.Contains(PreviousIndex) == true)
                {
                    btnPrevious.Visible = true;
                    btnPrevious.Enabled = true;
                }
                else
                {
                    btnPrevious.Visible = false;
                }

                if (ht.Contains(NextIndex) == true)
                {
                    btnNext.Visible = true;
                    btnNext.Enabled = true;
                }
                else
                {
                    btnNext.Visible = false;
                }

                if (ht.Contains(currentIndex) == true)
                {
                    //hidMRFID.Value = Convert.ToString(ht[currentIndex]);
                    ViewContractDetails(Convert.ToInt32(ht[currentIndex]), string.Empty, string.Empty);
                }
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionEnableDisableButtons, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    ///  Function will called on Privious Click
    /// </summary>
    private void PreviousClick()
    {
        try
        {
            CONTRACTCURRENTCOUNT = CONTRACTCURRENTCOUNT - 1;
            CONTRACTPREVIOUSCOUNT = CONTRACTPREVIOUSCOUNT - 1;
            CONTRACTNEXTCOUNT = CONTRACTNEXTCOUNT + 1;

            EnableDisableButtons(CONTRACTCURRENTCOUNT, CONTRACTPREVIOUSCOUNT, CONTRACTNEXTCOUNT);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionPreviousClick, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// Function will called on Next Click
    /// </summary>
    private void NextClick()
    {
        try
        {
            CONTRACTCURRENTCOUNT = CONTRACTCURRENTCOUNT + 1;
            CONTRACTPREVIOUSCOUNT = CONTRACTPREVIOUSCOUNT + 1;
            CONTRACTNEXTCOUNT = CONTRACTNEXTCOUNT - 1;

            EnableDisableButtons(CONTRACTCURRENTCOUNT, CONTRACTPREVIOUSCOUNT, CONTRACTNEXTCOUNT);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionNextClick, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// Fucntion will Set the Index of Page.
    /// </summary>
    private void SetContractIndex()
    {
        try
        {
            Hashtable htnew = new Hashtable();

            if (Request.QueryString[INDEX] != null)
            {
                int countIndex = Convert.ToInt32(DecryptQueryString(INDEX));

                if (Session[SessionNames.CONTRACT_CONTRACTIDHASHTABLE] != null)
                {
                    //27633 
                    //htnew = (Hashtable)Session[SessionNames.MRFVIEWINDEX]; 
                    htnew = (Hashtable)Session[SessionNames.CONTRACTPREVIOUSHASHTABLE];
                }

                CONTRACTCURRENTCOUNT = ((Convert.ToInt16(Session[SessionNames.CONTRACT_CURRENTPAGEINDEX].ToString()) - 1) * 10) + countIndex;
                CONTRACTPREVIOUSCOUNT = CONTRACTCURRENTCOUNT - 1;
                CONTRACTNEXTCOUNT = (htnew.Keys.Count - 2) - (((Convert.ToInt16(Session[SessionNames.CONTRACT_CURRENTPAGEINDEX].ToString()) - 1) * 10) + countIndex);

                if (htnew.Keys.Count == 1)
                {
                    btnNext.Visible = false;
                    btnPrevious.Visible = false;
                }
                else if (CONTRACTPREVIOUSCOUNT == -1)
                {
                    btnNext.Visible = true;
                    btnPrevious.Visible = false;
                }
                else if (CONTRACTNEXTCOUNT == -1)
                {
                    btnNext.Visible = false;
                    btnPrevious.Visible = true;
                }
                else
                {
                    btnNext.Visible = true;
                    btnPrevious.Visible = true;
                }

            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionSetContractIndex, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// This function checks if the project Name exists or not.
    /// </summary>
    /// <param name="projectName"></param>
    /// <returns></returns>
    private bool checkProjectName(string projectName)
    {
        try
        {
            bool sucess = true;
            objContractProjectBL = new Rave.HR.BusinessLayer.Contracts.ContractProject();

            //Checks for null projectName.
            if (projectName != null)
            {
                //for add new project.
                if (IsNewProject.Value == YES)
                {
                    sucess = objContractProjectBL.checkProjectName(projectName);
                }
            }
            return sucess;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctioncheckProjectName, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// This function gets the master data for Currency type drop down.
    /// </summary>
    private void GetMasterData_CurrencyType()
    {
        try
        {
            List<BusinessEntities.Master> objType = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master BLobj = new Rave.HR.BusinessLayer.Common.Master();

            //This region will fill the Contract Status drop down with the valid location types.
            objType = BLobj.GetMasterData(Currency);

            ddlcurrencyType.DataSource = objType;
            ddlcurrencyType.DataValueField = MasterID;
            ddlcurrencyType.DataTextField = MasterName;
            ddlcurrencyType.DataBind();
            ddlcurrencyType.Items.Insert(0, new ListItem(SELECTONE, ZERO));

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionGetMasterData_CurrencyType, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    #region Modified By Mohamed Dangra
    // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
    // Desc : Add project group in Contract page

    /// <summary>
    ///  method to get value of ProjectGroupDropDown
    /// </summary>
    private void GetMasterData_ProjectGroupDropDown()
    {
        try
        {
            List<BusinessEntities.Master> objRaveHRMaster = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            objRaveHRMaster = objRaveHRMasterBAL.GetMasterData((Convert.ToInt32(EnumsConstants.Category.ProjectGroup)).ToString());
            ddlPrjGroup.DataSource = objRaveHRMaster;
            ddlPrjGroup.DataTextField = "MasterName";
            ddlPrjGroup.DataValueField = "MasterID";
            ddlPrjGroup.DataBind();
            ddlPrjGroup.Items.Insert(0, "Select");
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddContract.aspx", "GetMasterData_ProjectGroupDropDown", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    // Mohamed : Issue 49791 : 15/09/2014 : Ends
    #endregion Modified By Mohamed Dangra

    // Mohamed : Issue  : 23/09/2014 : Starts                        			  
    // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS

    /// <summary>
    ///  method to get value of ProjectDivisionDropDown
    /// </summary>
    private void GetMasterData_ProjectDivisionDropDown()
    {
        try
        {
            List<BusinessEntities.Master> objRaveHRMaster = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            objRaveHRMaster = objRaveHRMasterBAL.GetMasterData((Convert.ToInt32(EnumsConstants.Category.ProjectDivision)).ToString());
            ddlPrjDivision.DataSource = objRaveHRMaster;
            ddlPrjDivision.DataTextField = "MasterName";
            ddlPrjDivision.DataValueField = "MasterID";
            ddlPrjDivision.DataBind();
            ddlPrjDivision.Items.Insert(0, "Select");
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddContract.aspx", "GetMasterData_ProjectDivisionDropDown", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    ///  method to get value of ProjectBussinessAreaDropDown
    /// </summary>
    private void GetMasterData_ProjectBussinessAreaDropDown()
    {
        try
        {
            List<BusinessEntities.Master> objRaveHRMaster = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            objRaveHRMaster = objRaveHRMasterBAL.GetMasterData((Convert.ToInt32(EnumsConstants.Category.ProjectBussinessArea)).ToString());
            ddlPrjBusinessArea.DataSource = objRaveHRMaster;
            ddlPrjBusinessArea.DataTextField = "MasterName";
            ddlPrjBusinessArea.DataValueField = "MasterID";
            ddlPrjBusinessArea.DataBind();
            ddlPrjBusinessArea.Items.Insert(0, "Select");
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddContract.aspx", "GetMasterData_ProjectBussinessAreaDropDown", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    ///  method to get value of ProjectBussinessSegmentDropDown
    /// </summary>
    private void GetMasterData_ProjectBussinessSegmentDropDown()
    {
        try
        {
            List<BusinessEntities.Master> objRaveHRMaster = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            objRaveHRMaster = objRaveHRMasterBAL.GetMasterData((Convert.ToInt32(EnumsConstants.Category.ProjectBussinessSegment)).ToString());
            ddlPrjBusinessSegment.DataSource = objRaveHRMaster;
            ddlPrjBusinessSegment.DataTextField = "MasterName";
            ddlPrjBusinessSegment.DataValueField = "MasterID";
            ddlPrjBusinessSegment.DataBind();
            ddlPrjBusinessSegment.Items.Insert(0, "Select");
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddContract.aspx", "GetMasterData_ProjectBussinessSegmentDropDown", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    // Mohamed : Issue  : 23/09/2014 : Ends

    //Siddharth 3 August 2015 Start
    /// <summary>
    /// method to get value of BusinessVertical
    /// </summary>
    private void GetMasterData_BusinessVerticalDropDown()
    {
        try
        {
            List<BusinessEntities.Master> objRaveHRMaster = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();

            objRaveHRMaster = objRaveHRMasterBAL.GetMasterData((Convert.ToInt32(EnumsConstants.Category.BusinessVertical)).ToString());
            ddlBusinessVertical.DataSource = objRaveHRMaster;
            ddlBusinessVertical.DataTextField = "MasterName";
            ddlBusinessVertical.DataValueField = "MasterID";
            ddlBusinessVertical.DataBind();
            ddlBusinessVertical.Items.Insert(0, "Select");
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddProject.aspx", "GetMasterData_ProjectModelDropDown", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    //Rakesh : HOD for Employees 11/07/2016 Begin   
    void BindProject_Head_Dropdown()
    {
        Rave.HR.BusinessLayer.Employee.Employee objEmployee = new Rave.HR.BusinessLayer.Employee.Employee();
        ddlProjectHead.BindDropdown(objEmployee.Get_HOD_Employees(), "FullName", "EmpId");
    }
    //Rakesh : HOD for Employees 11/07/2016 End   

    //Siddharth 3 August 2015 End

    //Siddharth 13 March 2015 Start
    /// <summary>
    /// method to get value of ProjectModel
    /// </summary>
    private void GetMasterData_ProjectModelDropDown()
    {
        try
        {
            List<BusinessEntities.Master> objRaveHRMaster = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();

            objRaveHRMaster = objRaveHRMasterBAL.GetMasterData((Convert.ToInt32(EnumsConstants.Category.ProjectModel)).ToString());
            ddlProjectModel.DataSource = objRaveHRMaster;
            ddlProjectModel.DataTextField = "MasterName";
            ddlProjectModel.DataValueField = "MasterID";
            ddlProjectModel.DataBind();
            ddlProjectModel.Items.Insert(0, "Select");
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddContract.aspx", "GetMasterData_ProjectModelDropDown", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    //Siddharth 13 March 2015 End






    private void GetMasterData_ProjectCategory()
    {
        try
        {
            List<BusinessEntities.Master> objType = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master BLobj = new Rave.HR.BusinessLayer.Common.Master();

            //This region will fill the ProjectCategory Status drop down.
            objType = BLobj.GetMasterData(ProjectCategory);

            ddlProjectCategory.DataSource = objType;
            ddlProjectCategory.DataValueField = MasterID;
            ddlProjectCategory.DataTextField = MasterName;
            ddlProjectCategory.DataBind();
            ddlProjectCategory.Items.Insert(0, new ListItem(SELECTONE, ZERO));

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex,
                Sources.PresentationLayer, CLASS_NAME, FunctionGetMasterData_ProjectCategory,
                EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    private void Get_ClientAbbrivation()
    {
        try
        {
            List<BusinessEntities.Master> objType = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master BLobj = new Rave.HR.BusinessLayer.Common.Master();
            string ClientAbbrivation = string.Empty;

            if (ddlClientName.SelectedValue != CommonConstants.SELECT)
            {
                int MasterId = Convert.ToInt32(ddlClientName.SelectedValue);

                //This region will fill the ProjectCategory Status drop down.
                ClientAbbrivation = BLobj.Get_ClientAbbrivation(MasterId);

                txtClientAbbr.Text = ClientAbbrivation;
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex,
                Sources.PresentationLayer, CLASS_NAME, FunctionGetMasterData_ProjectCategory,
                EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Does the data bind.
    /// </summary>
    private void DoDataBind()
    {
        gvCRDetails.DataSource = CRDetailsCollection;
        gvCRDetails.DataBind();

    }
    #endregion Private Mathods

    #region Public methods


    /* Goging Google Comment 
    /// <summary>
    /// Gets the logged in user emailid.
    /// </summary>
    /// <returns>string</returns>        
    public string GetDomainUsers(string strUserName)
    {
        try
        {
            //strUserName = strUserName.Replace("@rave-tech.co.in", "");

            //Google change point to northgate
            if (strUserName.Contains("@rave-tech.co.in"))
            {
                strUserName = strUserName.Replace("@rave-tech.co.in", "");
            }
            else
            {
                strUserName = strUserName.Replace(AuthorizationManagerConstants.NORTHGATEDOMAIN, "");
            }


            string strUserEmail = string.Empty;

            DirectoryEntry searchRoot = new DirectoryEntry("LDAP://RAVE-TECH.CO.IN");

            DirectorySearcher search = new DirectorySearcher(searchRoot);

            //string query = "(|(objectCategory=group)(objectCategory=user)) ";
            string query = "(SAMAccountName=" + strUserName + ")";

            search.Filter = query;

            SearchResult result;

            SearchResultCollection resultCol = search.FindAll();

            if (resultCol != null)
            {

                for (int counter = 0; counter < resultCol.Count; counter++)
                {

                    result = resultCol[counter];

                    if (result.Properties.Contains("samaccountname"))
                    {

                        strUserEmail = result.Properties["mail"][0].ToString();

                    }

                }

            }

            return strUserEmail;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionGetDomainUsers, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }
     
     //Goging Google Comment 
     
    */

    /// <summary>
    /// Converts the input string to upper case.
    /// </summary>
    /// <returns>string</returns>  
    public string ConvertToUpper(string InputString)
    {
        try
        {
            InputString = InputString.Substring(0, 1).ToUpper() + InputString.Substring(1, InputString.Length - 1);
            return InputString;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionConvertToUpper, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Clear the Controls after click on add row button.
    /// </summary>
    public void ClearProjectDetailsControls()
    {
        try
        {
            txtPrjName.Text = string.Empty;
            txtProjectCode.Text = string.Empty;

            ucDatePickerEnd.Text = string.Empty;
            ucDatePickerStart.Text = string.Empty;

            txtNoOfResources.Text = string.Empty;
            txtDescription.Text = string.Empty;
            ddlPrjLocation.SelectedIndex = -1;
            ddlTypeOfPrj.SelectedIndex = -1;
            ddlProjectCategory.SelectedIndex = -1;
            // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
            // Desc : Add project group in Contract page
            ddlPrjGroup.SelectedIndex = -1;
            // Mohamed : Issue 49791 : 15/09/2014 : Ends
            // Mohamed : Issue  : 23/09/2014 : Starts                        			  
            // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
            ddlPrjDivision.SelectedIndex = -1;
            ddlPrjBusinessArea.SelectedIndex = -1;
            ddlPrjBusinessSegment.SelectedIndex = -1;
            //txtPrjAlias.Text = string.Empty;
            ddlPrjBusinessArea.Enabled = false;
            ddlPrjBusinessSegment.Enabled = false;
            // Mohamed : Issue  : 23/09/2014 : Ends

            //Siddharth 9th Sept 2015 Start
            ddlBusinessVertical.SelectedIndex = -1;
            //ddlBusinessVertical.Enabled = false;
            ddlProjectModel.SelectedIndex = -1;
            //ddlProjectModel.Enabled = false;
            //Siddharth 9th Sept 2015 End

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionClearProjectDetailsControls, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }


    }

    /// <summary>
    /// Converts Datatable to list (Of type BusinessEntities.ContractProject).
    /// </summary>
    /// <param name="contractTable"></param>
    /// <returns></returns>
    public List<BusinessEntities.ContractProject> ConvertDatatableToList(DataTable contractTable)
    {
        List<BusinessEntities.ContractProject> contractList = new List<BusinessEntities.ContractProject>();
        try
        {

            foreach (DataRow contractRow in contractTable.Rows)
            {
                BusinessEntities.ContractProject projectData = new BusinessEntities.ContractProject();
                projectData.ProjectCode = contractRow[DbTableColumn.ProjectCode].ToString();
                projectData.ProjectName = contractRow[DbTableColumn.Con_ProjectName].ToString();
                projectData.ProjectType = contractRow[DbTableColumn.ProjectType].ToString();
                projectData.ProjectTypeID = Convert.ToInt32(ddlTypeOfPrj.Items.FindByText(projectData.ProjectType).Value);
                projectData.ProjectStartDate = Convert.ToDateTime(contractRow[DbTableColumn.ProjectStartDate]);
                projectData.ProjectEndDate = Convert.ToDateTime(contractRow[DbTableColumn.ProjectEndDate]);
                projectData.ProjectLocationName = contractRow[DbTableColumn.ProjectLocation].ToString();
                projectData.NoOfResources = Convert.ToDecimal(contractRow[DbTableColumn.NoOfResources]);
                projectData.ProjectsDescription = contractRow[DbTableColumn.Description].ToString();
                projectData.ProjectCategoryName = contractRow[DbTableColumn.Con_ProjectCategoryName].ToString();
                projectData.ProjectCategoryID = Convert.ToInt32(contractRow[DbTableColumn.Con_ProjectCategoryID]);
                // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
                // Desc : Add project group in Contract page            
                projectData.ProjectGroup = contractRow[DbTableColumn.ProjectGroup].ToString();
                // Mohamed : Issue 49791 : 15/09/2014 : Ends

                // Mohamed : Issue  : 23/09/2014 : Starts                        			  
                // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
                projectData.ProjectDivision = Convert.ToInt32(contractRow[DbTableColumn.ProjectDivision].ToString());
                projectData.ProjectBussinessArea = Convert.ToInt32(contractRow[DbTableColumn.ProjectBusinessArea].ToString());
                projectData.ProjectBussinessSegment = Convert.ToInt32(contractRow[DbTableColumn.ProjectBusinessSegment].ToString());
                //projectData.ProjectAlias = contractRow[DbTableColumn.ProjectAlias].ToString();
                // Mohamed : Issue  : 23/09/2014 : Ends

                //Siddharth 9 Sept 2015 Start
                projectData.ProjectModel = contractRow[DbTableColumn.ProjectModel].ToString();
                projectData.BusinessVertical = contractRow[DbTableColumn.BusinessVertical].ToString();
                //Siddharth 9 Sept 2015 End

                contractList.Add(projectData);
            }

        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionConvertDatatableToList, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
        return contractList;

    }

    /// <summary>
    /// Check whether client name are same for all associated projects with contract.
    /// </summary>
    /// <param name="contractDetails"></param>
    /// <param name="projectDetails"></param>
    /// <returns></returns>
    public bool checkClientName(DataTable projectDetails)
    {
        bool flag = true;
        try
        {
            string clientnameId;
            if (projectDetails != null)
            {

                foreach (DataRow projectDr in projectDetails.Rows)
                {
                    if (projectDr[DbTableColumn.Con_ProjectCode].ToString() != string.Empty)
                    {
                        clientnameId = getClientNameByProjectCode(projectDr[DbTableColumn.Con_ProjectCode].ToString());

                        if (clientnameId != ddlClientName.SelectedValue)
                        {
                            flag = false;
                            lblMandatory.Text = "Client Name of  Project '" + projectDr["ProjectName"].ToString() + "' does not match with the contract's client name.";
                            lblMandatory.Text = "<font color=RED>" + lblMandatory.Text + "</font>";

                            break;
                        }
                    }
                }
            }

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctioncheckClientName, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
        return flag;
    }

    /// <summary>
    /// Get the client name of project by project code .
    /// </summary>
    /// <param name="projectCode"></param>
    /// <returns></returns>
    public string getClientNameByProjectCode(string projectCode)
    {
        try
        {

            BusinessEntities.ProjectDetails projectDetails = new BusinessEntities.ProjectDetails();
            objContractProjectBL = new Rave.HR.BusinessLayer.Contracts.ContractProject();
            projectDetails = objContractProjectBL.getClientNameByProjectCode(projectCode);
            return projectDetails.ClientName;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctiongetClientNameByProjectCode, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Checks the project code.
    /// </summary>
    /// <param name="projectName">Name of the project.</param>
    /// <returns></returns>
    private bool checkProjectCode(string projectName)
    {
        try
        {
            bool sucess = true;
            objContractProjectBL = new Rave.HR.BusinessLayer.Contracts.ContractProject();

            //Checks for null projectName.
            if (projectName != null)
            {
                //for add new project.
                if (IsNewProject.Value == YES)
                {
                    sucess = objContractProjectBL.checkProjectCode(projectName);
                }
            }
            return sucess;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctioncheckProjectCode, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    private void ClearCRControls()
    {
        try
        {
            txtCRReferenceNo.Text = string.Empty;
            ucDatePickerCRStartDate.Text = string.Empty;
            ucDatePickerCREndDate.Text = string.Empty;
            txtRemarks.Text = string.Empty;


        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionClearControls, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Populates the grid.
    /// </summary>
    private void PopulateGrid(int ContractId)
    {
        CRDetailsCollection = this.GetCRDetails(ContractId);

        //Binding the datatable to grid
        gvCRDetails.DataSource = CRDetailsCollection;
        gvCRDetails.DataBind();

        HdfContractId.Value = contractId.ToString();

        if (gvCRDetails.Rows.Count > 0)
            divCRDetails.Visible = true;
    }

    /// <summary>
    /// Gets the CR details.
    /// </summary>
    /// <param name="CRId">The CR id.</param>
    /// <returns></returns>
    private BusinessEntities.RaveHRCollection GetCRDetails(int ContractId)
    {
        Rave.HR.BusinessLayer.Contracts.Contract objCRDetailsBAL;
        BusinessEntities.Contract objCRDetails;

        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            objCRDetailsBAL = new Rave.HR.BusinessLayer.Contracts.Contract();
            objCRDetails = new BusinessEntities.Contract();

            objCRDetails.ContractID = ContractId;

            raveHRCollection = objCRDetailsBAL.GetCRDetails(objCRDetails);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetCRDetails", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

        return raveHRCollection;
    }

    #endregion Public methods




    protected void gvCRDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int deleteRowIndex = 0;
            int rowIndex = -1;

            BusinessEntities.Contract objCRDetails = new BusinessEntities.Contract();

            deleteRowIndex = e.RowIndex;

            objCRDetails = (BusinessEntities.Contract)CRDetailsCollection.Item(deleteRowIndex);

            objCRDetails.Mode = "3";

            if (ViewState[CRDETAILSDELETE] != null)
            {
                BusinessEntities.RaveHRCollection objDeleteQualificationDetailsCollection = (BusinessEntities.RaveHRCollection)ViewState[CRDETAILSDELETE];
                objDeleteQualificationDetailsCollection.Add(objCRDetails);

                ViewState[CRDETAILSDELETE] = objDeleteQualificationDetailsCollection;
            }
            else
            {
                BusinessEntities.RaveHRCollection objDeleteQualificationDetailsCollection1 = new BusinessEntities.RaveHRCollection();

                objDeleteQualificationDetailsCollection1.Add(objCRDetails);

                ViewState[CRDETAILSDELETE] = objDeleteQualificationDetailsCollection1;
            }

            CRDetailsCollection.RemoveAt(deleteRowIndex);

            ViewState[DELETEROWINDEX] = deleteRowIndex;

            DoDataBind();

            if (ViewState[ROWINDEX] != null)
            {
                rowIndex = Convert.ToInt32(ViewState[ROWINDEX].ToString());
                //check edit index with deleted index if edit index is greater than or equal to delete index then rowindex decremented.
                if (rowIndex != -1 && deleteRowIndex <= rowIndex)
                {
                    rowIndex--;
                    //store the rowindex in viewstate.
                    ViewState[ROWINDEX] = rowIndex;
                }

                // 27871-Ambar-Start
                // Added if condition to avoid crashing of system when last or first row is deleted
                if (rowIndex != -1)
                {
                    ImageButton btnImg = (ImageButton)gvCRDetails.Rows[rowIndex].FindControl(IMGBTNDELETE);
                    btnImg.Enabled = false;
                }

                //Disabling all the edit buttons.
                for (int i = 0; i < gvCRDetails.Rows.Count; i++)
                {
                    if (rowIndex != i)
                    {
                        ImageButton btnImgEdit = (ImageButton)gvCRDetails.Rows[i].FindControl(IMGBTNEDIT);
                        btnImgEdit.Enabled = false;
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddContract.aspx", "Page_Load", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void gvCRDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            int rowIndex = 0;

            txtCRReferenceNo.Text = gvCRDetails.Rows[e.NewEditIndex].Cells[2].Text.Trim();
            ucDatePickerCRStartDate.Text = gvCRDetails.Rows[e.NewEditIndex].Cells[3].Text.Trim();
            ucDatePickerCREndDate.Text = gvCRDetails.Rows[e.NewEditIndex].Cells[4].Text.Trim();
            txtRemarks.Text = gvCRDetails.Rows[e.NewEditIndex].Cells[5].Text.Trim();
            // 27871-Ambar-Start
            // Added if condition to avoid blank space
            if ((txtRemarks.Text == null) || (txtRemarks.Text == string.Empty) || (txtRemarks.Text == "&nbsp;"))
            {
                txtRemarks.Text = "";
            }
            // 27871-Ambar-End
            HdfProjectCode.Value = gvCRDetails.Rows[e.NewEditIndex].Cells[1].Text.Trim();

            rowIndex = e.NewEditIndex;
            ViewState[ROWINDEX] = rowIndex;

            //Disabling all the edit buttons.
            for (int i = 0; i < gvCRDetails.Rows.Count; i++)
            {
                ImageButton btnImgEdit = (ImageButton)gvCRDetails.Rows[i].FindControl(IMGBTNEDIT);
                if (i != rowIndex)
                    btnImgEdit.Enabled = false;
            }
            HideUnHideCRDetails(true);

            btnAddCRRow.Visible = false;
            btnUpdateCRRow.Visible = true;
            btnUpdateCancelCRRow.Visible = true;
            btnCancelCRRow.Visible = false;
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddContract.aspx", "Page_Load", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    protected void btnUpdateCRRow_Click(object sender, EventArgs e)
    {
        try
        {
            BusinessEntities.Contract objCRDetails = new BusinessEntities.Contract();

            int rowIndex = 0;

            if (ViewState[ROWINDEX] != null)
            {
                rowIndex = Convert.ToInt32(ViewState[ROWINDEX].ToString());

                objCRDetails = (BusinessEntities.Contract)CRDetailsCollection.Item(rowIndex);

                Label CRId = (Label)gvCRDetails.Rows[rowIndex].FindControl("CRId");

                objCRDetails.CRReferenceNo = txtCRReferenceNo.Text.Trim();
                objCRDetails.CRStartDate = Convert.ToDateTime(ucDatePickerCRStartDate.Text.Trim());
                objCRDetails.CREndDate = Convert.ToDateTime(ucDatePickerCREndDate.Text.Trim());
                objCRDetails.CRRemarks = txtRemarks.Text.Trim();

                // 26595-Ambar-Start
                DateTime ldt_contractstartdate;
                ldt_contractstartdate = Convert.ToDateTime(ucContractStartDate.Text.Trim());

                if ((objCRDetails.CRStartDate < ldt_contractstartdate))
                {
                    lblMandatory.Text = string.Empty;
                    lblMandatory.Text = " CR start date can not be less than contract start date";
                    lblMandatory.Text = "<font color=RED>" + lblMandatory.Text + "</font>";
                    return;
                }
                // 26595-Ambar-End

                if (int.Parse(CRId.Text) == 0)
                {
                    objCRDetails.Mode = "1";
                }
                else
                {
                    objCRDetails.Mode = "2";
                }

                if (!string.IsNullOrEmpty(HdfProjectCode.Value))
                {
                    btnAddCRRow.Visible = true;
                    btnCancelCRRow.Visible = true;
                    btnUpdateCancelCRRow.Visible = false;
                    btnUpdateCRRow.Visible = false;
                }
                else
                {
                    btnAddCRRow.Visible = false;
                    btnCancelCRRow.Visible = false;
                }
            }
            DoDataBind();
            ClearCRControls();

            lblMandatory.Text = string.Empty;
            lblMandatory.Text = " Please click on the Save button to save the CR details ";
            lblMandatory.Text = "<font color=Blue>" + lblMandatory.Text + "</font>";
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddContract.aspx", "Page_Load", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnUpdateCancelCRRow_Click(object sender, EventArgs e)
    {
        try
        {
            ClearCRControls();

            if (!string.IsNullOrEmpty(HdfProjectCode.Value))
            {
                btnAddCRRow.Visible = true;
                btnCancelCRRow.Visible = true;
                HideUnHideCRDetails(true);
            }
            else
            {
                btnAddCRRow.Visible = false;
                btnCancelCRRow.Visible = false;
                HideUnHideCRDetails(false);
            }

            btnUpdateCancelCRRow.Visible = false;
            btnUpdateCRRow.Visible = false;

            //Enabling all the edit buttons.
            for (int i = 0; i < gvCRDetails.Rows.Count; i++)
            {
                ImageButton btnImgEdit = (ImageButton)gvCRDetails.Rows[i].FindControl(IMGBTNEDIT);
                btnImgEdit.Enabled = true;
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddContract.aspx", "Page_Load", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    private void HideUnHideCRDetails(Boolean Flag)
    {
        pnlCRDetails.Visible = Flag;
        btnAddCRRow.Visible = Flag;
        btnCancelCRRow.Visible = Flag;
        divCRDetails.Visible = Flag;
    }

    private bool ValidateProjectdates()
    {
        bool Flag = true;

        if ((DateTime.Parse(ucDatePickerStart.Text.Trim()) > DateTime.Parse(ucContractEndDate.Text.Trim())) || (DateTime.Parse(ucDatePickerStart.Text.Trim()) < DateTime.Parse(ucContractStartDate.Text.Trim())))
        {
            lblConfirmMessage.Text = dateErrorMsg + " i.e between " + ucContractStartDate.Text + " & " + ucContractEndDate.Text + ".";
            lblConfirmMessage.Style["color"] = "red";
            Flag = false;
        }

        //--End Date Validation
        if ((DateTime.Parse(ucDatePickerEnd.Text.Trim()) > DateTime.Parse(ucContractEndDate.Text.Trim())) || (DateTime.Parse(ucDatePickerEnd.Text.Trim()) < DateTime.Parse(ucContractStartDate.Text.Trim())))
        {
            lblConfirmMessage.Text = dateErrorMsg + " i.e between " + ucContractStartDate.Text + " & " + ucContractEndDate.Text + ".";
            lblConfirmMessage.Style["color"] = "red";
            Flag = false;
        }

        return Flag;
    }

    private bool checkCRReferenceNo(BusinessEntities.Contract objCRDetails)
    {
        try
        {
            bool sucess = true;
            //objContractBL = new Rave.HR.BusinessLayer.Contracts.Contract();

            ////Checks for null projectName.
            //if (objCRDetails != null)
            //{
            //    sucess = objContractBL.checkCRReferenceNo(objCRDetails);

            //}

            if (gvCRDetails.Rows.Count > 0)
            {
                for (int i = 0; gvCRDetails.Rows.Count > i; i++)
                {
                    if (txtCRReferenceNo.Text.Trim() == gvCRDetails.Rows[i].Cells[2].Text.Trim())
                    {
                        lblConfirmMessage.Text = "CR Reference No, entered already exist for Project Code i.e." + HdfProjectCode.Value + ", kindly enter a different CR Reference No.";
                        lblConfirmMessage.Style["color"] = "red";
                        sucess = false;
                    }
                }
            }
            return sucess;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctioncheckProjectCode, EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }
    // Mohamed : Issue  : 29/09/2014 : Starts                        			  
    // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
    protected void ddlPrjDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPrjDivision.SelectedValue == CommonConstants.Project_Division_PublicService)
            {
                ddlPrjBusinessArea.Enabled = true;
                ddlPrjBusinessArea.SelectedIndex = 0;
            }
            else
            {
                ddlPrjBusinessArea.Enabled = false;
                ddlPrjBusinessArea.SelectedIndex = 0;

                ddlPrjBusinessSegment.Enabled = false;
                ddlPrjBusinessSegment.SelectedIndex = 0;
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddContract.aspx", "Page_Load", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    protected void ddlPrjBusinessArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPrjBusinessArea.SelectedValue == CommonConstants.Project_BussinessArea_Solutions)
            {
                ddlPrjBusinessSegment.Enabled = true;
                ddlPrjBusinessSegment.SelectedIndex = 0;
            }
            else
            {
                ddlPrjBusinessSegment.Enabled = false;
                ddlPrjBusinessSegment.SelectedIndex = 0;
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "AddContract.aspx", "Page_Load", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    // Mohamed : Issue  : 29/09/2014 : Ends
}
