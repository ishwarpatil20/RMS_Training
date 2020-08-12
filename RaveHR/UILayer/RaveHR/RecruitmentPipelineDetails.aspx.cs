//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           RecruitmentPipelineDetails.aspx      
//  Author:         Chhaya Gunjal 
//  Date written:   16/09/2009/ 10:58:30 AM
//  Description:    The RecruitmentPipelineDetails added recruitment record ,also view ,update and delete the recruitment record.
//  Amendments
//  Date                    Who              Ref     Description
//  ----                    -----------      ---     -----------
//  07/10/2009 10:58:30 AM  Chhaya Gunjal    n/a     Created    
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
using Common.Constants;
using Common.AuthorizationManager;
using System.DirectoryServices;
using BusinessEntities;
using System.Text;
using Common;

public partial class RecruitmentPipelineDetails : BaseClass
{

    #region Variable
    RaveHRCollection raveHRCollection;
    BusinessEntities.Recruitment recruitment = new Recruitment();
    const string INDEX = "index";
    string subject;
    string body;
    AuthorizationManager objAuMan = new AuthorizationManager();
    //Googleconfigurable
    //const string RAVEDOMAIN = "@rave-tech.co.in";
    private const string PROJECT = "Projects";
    private const string OTHER_LOCATION = "Other Location";
    string message = "";
    /// <summary>
    /// variable to hold the previous MrfCode
    /// </summary>
    private string previousMrfCode = string.Empty;
    #endregion

    #region Constants
    private const string CLASS_NAME = "RecruitmentPipelineDetails.aspx";
    private const string PAGE_LOAD = "Page_Load";
    private const string BTN_SUBMIT_CLICK = "btnSubmit_Click";
    private const string RECRUITMENTCURRENTCOUNT = "RecruitmentCurrentCount";
    private const string BTN_CONFIRM_DELETE = "btnConfirmDelete_Click";
    private const string BTN_DELETE_CLICK = "btnDelete_Click";
    private const string BTN_PREVIOUS_CLICK = "btnPrevious_Click";
    private const string BTN_NEXT_CLICK = "btnNext_Click";
    private const string BTN_SAVE_CLICK = "btnSave_Click";
    private const string BTN_EDIT_CLICK = "btnEdit_Click";
    private const string CHKSELECT_JOINEDDATE = "chkSelect_JoinedDate";
    private const string BTN_CANCEL_CLICK = "btnCancel_Click";
    private const string PREVIOUS_CLICK = "PreviousClick";
    private const string ENABLE_DISABLE_BUTTONS = "EnableDisableButtons";
    private const string NEXT_CLICK = "NextClick";
    private const string SET_RECRUITMENT_INDEX = "SetRecruitmentIndex";
    //private const string VIEW_PIPELINE_DETAILS="ViewPipeLineDetails";
    private const string FILL_CONTROL_FOR_VIEW = "FillControlForViewPipeLineDetails";
    private const string SET_CONTROL_FOR_VIEW = "SetControlForViewPipeLineDetails";
    private const string RESET_CONTROL = "ResetControl";
    private const string GET_CONTTROL_VALUE = "GetControlValue";
    private const string GET_RECRUITMNET_DETAIL = "GetRecruitmentDetail";
    private const string FILL_DROP_DOWNS = "FillDropDowns";
    private const string FILL_MRF_CODE_DROP_DOWNS = "FillMRFCodeDropDowns";
    private const string FILL_VIEW_MRF_CODE_DROPDOWNS = "FillViewMRFCodeDropDowns";
    private const string FILL_PREFIX_DROP_DOWN = "FillPrefixDropDown";
    private const string FILL_EMPLOYEE_BANDDROPDOWN = "FillEmployeeBandDropDown";
    private const string FILL_EMPLOYEE_TYPE_DROP_DOWN = "FillEmployeeTypeDropDown";
    private const string DDLMRFCODE_SELECTEDCHANGED = "ddlMRFCode_SelectedIndexChanged";
    private const string FILL_ROLE_DROP_DOWN = "FillRoleDropDown";
    private const string FILL_DESIGNATION_AS_PER_DEPARTMENT = "FillDesignationDropdownAsPerDepartment";
    private const string FILL_ROLE_DEPARTMENTWISE = "FillRoleDepartmentWise";
    private const string REMOVE_RECRUITMNET_RECORD = "RemoveRecruitmentRecord";
    private const string EDIT_PIPELINEDETAILS = "EditPipeLineDetails";
    private const string GET_EDIT_PIPELINEDETAILS = "GetEditPipeLineDetails";
    private const string SET_CONTROL_FOR_EDIT_PIPELINEDDETAILS = "SetControlForEditPipeLineDetails";
    private const string FILL_MRF_DETAILS = "FillMRFDetails";
    private const string FILL_LOCATION_DROP_DOWN = "FillLocationDropDown";
    private const string DDL_LOCATION_SELECTED_INDEX_CHANGED = "ddlLocation_SelectedIndexChanged";
    private const string DELETE_PIPELINE_DETAILS = "Delete Pipeline Details";
    private const string EDIT_PIPELINE_DETAILS = "Edit Pipeline Details";
    private const string VIEW_PIPELINE_DETAILS = "View Pipeline Details";
    private const string ADD_PIPELINE_DETAILS = "Add Pipeline Details";
    private const string FILL_RESOURCEBUSSINESSUNIT_DROPDOWN = "FillResourceBussinesUnitDropDown";
    private const string MESSAGE_FOR_FUTURE_DATE = "DateOfJoining should not be future date,please change the DateofJoining.";
    private const string TRANING_SUBJECT = "TRANING SUBJECT";
    //Date Format
    const string DATEFORMAT = "dd/MM/yyyy";

    //const string CurrentDate = DateTime.Now.ToString(DATEFORMAT);
    // Issue Id : 34230 STRAT CONCURRENCY HANDLED Mahendra
    private Random random = new Random();
    // Issue Id : 34230 END CONCURRENCY HANDLED Mahendra

    #endregion

    #region Property
    /// <summary>
    ///  Property for Recruitment Current count
    /// </summary>
    public int RecruitmentCurrentCount
    {
        get
        {
            if (ViewState["RecruitmentCurrentCount"] != null)
            {
                return Convert.ToInt32(ViewState["RecruitmentCurrentCount"]);
            }
            else
            {
                return -1;
            }
        }
        set
        {
            ViewState["RecruitmentCurrentCount"] = value;
        }
    }

    /// <summary>
    /// Property for Recruitment Previous Count
    /// </summary>
    public int RecruitmentPreviousCount
    {
        get
        {
            if (ViewState["RecruitmentPreviousCount"] != null)
            {
                return Convert.ToInt32(ViewState["RecruitmentPreviousCount"]);
            }
            else
            {
                return -1;
            }
        }
        set
        {
            ViewState["RecruitmentPreviousCount"] = value;
        }
    }

    /// <summary>
    /// Property for RecruitmentNextCount
    /// </summary>
    public int RecruitmentNextCount
    {
        get
        {
            if (ViewState["RecruitmentNextCount"] != null)
            {
                return Convert.ToInt32(ViewState["RecruitmentNextCount"]);
            }
            else
            {
                return -1;
            }
        }
        set
        {
            ViewState["RecruitmentNextCount"] = value;
        }
    }

    #endregion

    //Rajan Kumar : Issue 39508: 05/02/2014 : Starts                        			 
    // Desc : Traninig for new joining employee. (Training Gaps).    
    #region ENUM
    public enum TraningSubjectChanged
    {
        Add,
        Edit,
        Delete
    }
    #endregion
    // Rajan Kumar : Issue 39508: 05/02/2014 : END
    #region Event
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Jave Script
        // Issue Id : 34230 STRAT CONCURRENCY HANDLED Mahendra
        btnSubmit.Attributes.Add(CommonConstants.EVENT_ONCLICK, "javascript:return ButtonClickValidate();");
        // Issue Id : 34230 END CONCURRENCY HANDLED Mahendra
        btnSave.Attributes.Add(CommonConstants.EVENT_ONCLICK, "javascript:return ButtonClickValidate();");

        //Poonam : Issue : Disable Button : Starts
        btnSave.OnClientClick = "if(ButtonClickValidate()){" + ClientScript.GetPostBackEventReference(btnSave, null) + "}";
        btnSubmit.OnClientClick = "if(ButtonClickValidate()){" + ClientScript.GetPostBackEventReference(btnSubmit, null) + "}";
        //Poonam : Issue : Disable Button : Ends

        ddlMRFCode.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzMRFCode.ClientID + "','','');");
        ddlPrefix.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzPrefix.ClientID + "','','');");
        ddlEmpType.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzEmpType.ClientID + "','','');");
        ddlDesignation.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzDesignation.ClientID + "','','');");
        ddlBand.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzBand.ClientID + "','','');");
        //txtExpectedDateOfJoining.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + txtExpectedDateOfJoining.ClientID + "','','');");
        DatePickerCandidateJoined.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + DatePickerCandidateJoined.ClientID + "','','');");
        imgExpectedDateOfJoiningError.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanExpectedDateOfJoining.ClientID + "','" + Common.CommonConstants.MSG_DATE + "');");
        imgExpectedDateOfJoiningError.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanExpectedDateOfJoining.ClientID + "');");

        //txtActualCTC.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateCTCControlValue('" + //txtActualCTC.ClientID + "','" + //imgActualCTC.ClientID + "','" + "DecimalActualCTC" + "');");
        //imgActualCTC.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanzActualCTC.ClientID + "','" + Common.CommonConstants.MSG_DECIMAL + "');");
        //imgActualCTC.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanzActualCTC.ClientID + "');");

        txtFirstName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtFirstName.ClientID + "','" + imgFirstName.ClientID + "','" + Common.CommonConstants.VALIDATE_ISALPHABET + "');");
        imgFirstName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanFirtstName.ClientID + "','" + Common.CommonConstants.MSG_ALPHABET + "');");
        imgFirstName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanFirtstName.ClientID + "');");

        txtLastName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtLastName.ClientID + "','" + imgLastName.ClientID + "','" + Common.CommonConstants.VALIDATE_ISALPHABET + "');");
        imgLastName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanLastName.ClientID + "','" + Common.CommonConstants.MSG_ALPHABET + "');");
        imgLastName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanLastName.ClientID + "');");
        //Poonam : Issue : 54340: Starts (Added Condition)
        if (txtMiddleName.Text != "")
        {
            txtMiddleName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtMiddleName.ClientID + "','" + imgMiddleName.ClientID + "','" + Common.CommonConstants.VALIDATE_ISALPHABET + "');");
            imgMiddleName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanMiddleName.ClientID + "','" + Common.CommonConstants.MSG_ALPHABET + "');");
            imgMiddleName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanMiddleName.ClientID + "');");
        }
        //Poonam : Issue : 54340: Ends
        imgResponsiblePersonSearch.Attributes.Add("onclick", "return popUpEmployeeSearch();");
        txtReportingTo.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return MultiLineTextBox('" + txtReportingTo.ClientID + "','" + txtReportingTo.MaxLength + "','" + imgReportingTo.ClientID + "');");
        txtReportingTo.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtReportingTo.ClientID + "','" + imgReportingTo.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_WITDSPACE + "');");
        imgReportingTo.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanReportingTo.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgReportingTo.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanReportingTo.ClientID + "');");
        //txtExpectedDateOfJoining.Attributes.Add("readonly", "readonly");

        txtReason.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtReason.ClientID + "','" + imgReason.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_WITDSPACE + "');");
        imgReason.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanReason.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgReason.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanReason.ClientID + "');");

        btnConfirmDelete.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return ButtonDeleteClickValidate();");

        ddlDepartment.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzDepartment.ClientID + "','','');");
        // Rajan Kumar : Issue 39508: 31/01/2014 : Starts                        			 
        // Desc : Traninig for new joining employee. (Training Gaps).
        chkTrainingRequired.Attributes.Add(CommonConstants.EVENT_ONCLICK, "TrainingRequired();");
        // Rajan Kumar : Issue 39508: 31/01/2014 : END

        #endregion
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

                if (!(arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERECRUITMENT) ))
                   // arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM)))
                {
                    Response.Redirect(CommonConstants.PAGE_HOME, true);
                }
                //Siddharth 26th August 2015 End


                // Issue Id : 34230 STRAT CONCURRENCY HANDLED Mahendra
                SetToken();
                // Issue Id : 34230 END CONCURRENCY HANDLED Mahendra

                if (!IsPostBack)
                {
                    FillDropDowns();
                    SetRecruitmentIndex();
                    if (Request.QueryString[QueryStringConstants.CANDIDATEID] != null)
                    {
                        recruitment = new Recruitment();
                        recruitment.CandidateId = Convert.ToInt32(DecryptQueryString(QueryStringConstants.CANDIDATEID));

                        // Mohamed : Issue 48476 : 29/01/2014 : Starts                        			  
                        // Desc : Assigned 2 Resource for 1 MRF _ Vinayak Narkar -- for Edit Pipeline details

                        hidCandidateId.Value = recruitment.CandidateId.ToString();

                        // Mohamed : Issue 48476 : 29/01/2014 : Ends

                        //recruitment.IsCandidateJoining = Convert.ToBoolean(DecryptQueryString(QueryStringConstants.CANDIDATENOTJOINING));
                        if (Convert.ToBoolean(DecryptQueryString(QueryStringConstants.CANDIDATENOTJOINING)))
                        {
                            ViewPipeLineDetails(recruitment.CandidateId);
                            pnlViewRecruitment.Visible = false;
                            lblHeaderPipelineDetails.Text = DELETE_PIPELINE_DETAILS;
                            pnlDeleteReason.Visible = true;
                            pnlDelete.Visible = true;

                        }
                        else
                        {
                            ViewPipeLineDetails(recruitment.CandidateId);
                        }

                    }
                    else
                    {
                        FillMRFCodeDropDowns();
                        //Default focus on MRF Code
                        ddlMRFCode.Focus();
                        lblHeaderPipelineDetails.Text = ADD_PIPELINE_DETAILS;
                    }
                }
                // Rajan Kumar : Issue 39508: 31/01/2014 : Starts                        			 
                // Desc : Traninig for new joining employee. (Training Gaps).
                Page.ClientScript.RegisterStartupScript(GetType(), "chkTrainingRequired", "TrainingRequired();", true);
                // Rajan Kumar : Issue 39508: 31/01/2014 : END
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, PAGE_LOAD, EventIDConstants.RAVE_HR_RECRUITMENT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnReportingTo_Click(object sender, EventArgs e)
    {
        try
        {
            if (hidResponsiblePersonName.Value != null)
            {
                txtReportingTo.Text = Rave.HR.BusinessLayer.Recruitment.Recruitment.GetMRfResponsiblePersonName(hidResponsiblePersonName.Value);
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnReportingTo_Click", EventIDConstants.RAVE_HR_RECRUITMENT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            // Issue Id : 34230 STRAT CONCURRENCY HANDLED Mahendra
            if (IsTokenValid())
            {
                // Issue Id : 34230 END CONCURRENCY HANDLED Mahendra
                GetRecruitmentDetail();
            }
            ResetControl();
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, BTN_SUBMIT_CLICK, EventIDConstants.RAVE_HR_RECRUITMENT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnConfirmDelete_Click(object sender, EventArgs e)
    {
        try
        {
            RemoveRecruitmentRecord();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, BTN_CONFIRM_DELETE, EventIDConstants.RAVE_HR_RECRUITMENT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, BTN_PREVIOUS_CLICK, EventIDConstants.RAVE_HR_RECRUITMENT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, BTN_NEXT_CLICK, EventIDConstants.RAVE_HR_RECRUITMENT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            //Date of joining shuld not be future date. validation
            System.DateTime TodayDate = Convert.ToDateTime(DateTime.Now.ToString(DATEFORMAT));
            if (DatePickerCandidateJoined.Text.Trim() != "")
            {
                if (Convert.ToDateTime(DatePickerCandidateJoined.Text).Date > TodayDate)
                {
                    lblMessage.Text = MESSAGE_FOR_FUTURE_DATE;
                    return;
                }
            }
            GetEditPipeLineDetails();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, BTN_SAVE_CLICK, EventIDConstants.RAVE_HR_RECRUITMENT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            EditPipeLineDetails();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, BTN_EDIT_CLICK, EventIDConstants.RAVE_HR_RECRUITMENT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    protected void chkSelect_JoinedDate(object sender, EventArgs e)
    {
        try
        {
            if (chkSelect.Checked)
            {
                pnlJoiningDate.Visible = true;
                //btnSaveReject.Attributes.Add(CommonConstants.EVENT_ONCLICK, "return ButtonRejectClickValidate();");
                btnSave.Attributes.Add(CommonConstants.EVENT_ONCLICK, "return ButtonJoiningDateValidate();");

                //Assign textbox with current Datetime.
                DatePickerCandidateJoined.Text = DateTime.Now.ToString(DATEFORMAT);

            }
            else
                pnlJoiningDate.Visible = false;
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, BTN_SAVE_CLICK, EventIDConstants.RAVE_HR_RECRUITMENT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("RecruitmentSummary.aspx", false);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, BTN_SAVE_CLICK, EventIDConstants.RAVE_HR_RECRUITMENT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    protected void btnSubmitCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("Home.aspx", false);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, BTN_SAVE_CLICK, EventIDConstants.RAVE_HR_RECRUITMENT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlLocation.SelectedItem.Text == OTHER_LOCATION)
            {
                txtLocation.Visible = true;
                txtLocation.Focus();
            }
            else
            {
                txtLocation.Visible = false;
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, DDL_LOCATION_SELECTED_INDEX_CHANGED, EventIDConstants.RAVE_HR_RECRUITMENT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void ddlMRFCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlMRFCode.SelectedItem.Text != Common.CommonConstants.SELECT)
            {
                int mrfId = Convert.ToInt32(ddlMRFCode.SelectedItem.Value);
                recruitment.MRFCode = Convert.ToString(ddlMRFCode.SelectedItem.Text);

                FillMRFDetails(mrfId);
                //Amita raised issue- when swap candidate from testing to PEEF project it throw error. beacuse here candidate Id need to reassign to recruitment object.
                //START 18-DEC-2012
                if (Request.QueryString[QueryStringConstants.CANDIDATEID] != null)
                {
                    recruitment.CandidateId = Convert.ToInt32(DecryptQueryString(QueryStringConstants.CANDIDATEID));
                }
                //END 18-DEC-2012
                ddlPrefix.Focus();
            }
            else
            {
                txtProjectName.Text = string.Empty;
                txtXClientName.Text = string.Empty;
                txtReportingTo.Text = string.Empty;
                txtPurpose.Text = string.Empty;
            }
            ddlDepartment.SelectedIndex = -1;
            ddlBand.SelectedIndex = -1;
            ddlResourceBussinesUnit.SelectedIndex = -1;
            ddlLocation.SelectedIndex = -1;
            ddlPrefix.SelectedIndex = -1;
            txtFirstName.Text = string.Empty;
            txtMiddleName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            ucDatePicker.Text = string.Empty;
            tbPhoneNo.Text = string.Empty;
            tbAdress.Text = string.Empty;
            txtReleventMonths.Text = string.Empty;
            txtReleventYears.Text = string.Empty;
            txtLandlineNo.Text = string.Empty;
            txtEmailId.Text = string.Empty;
            chkTrainingRequired.Checked = false;
            chkSelect.Checked = false;

            //txtDepartment.Text = recruitment.Department;
            // recruitment.Department = ddlDepartment.SelectedItem.Value;
            lblMessage.Text = "";


            if (ddlMRFCode.SelectedItem.Text == Common.CommonConstants.SELECT)
            {
                rowProjClientDetails.Visible = true;
                lblPrjName.Visible = true;
                txtProjectName.Visible = true;
                txtXClientName.Visible = true;
                lblClientName.Visible = true;
                //MandatoryProjectName.Visible = true;
                //MandatoryClientName.Visible = true;
                txtContractDuration.Visible = false;
                LblContractDuration.Visible = false;

            }


            //textbox shoudl be madatory for Consultant Depts
            if (ddlMRFCode.SelectedItem.Text.Contains("Consultant"))
            {
                CandidateEmailIdMandatoryMark.Visible = true;
                txtContractDuration.Visible = true;
                LblContractDuration.Visible = true;
            }
            else
            {
                CandidateEmailIdMandatoryMark.Visible = false;
                txtContractDuration.Visible = false;
                LblContractDuration.Visible = false;
            }

            //Assign textbox with current Datetime.
            DatePickerOfferAcceptedDt.Text = DateTime.Now.ToString(DATEFORMAT);


        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, DDLMRFCODE_SELECTEDCHANGED, EventIDConstants.RAVE_HR_RECRUITMENT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// ddlDepartmen SelectedIndexChanged event handler
    /// </summary>
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedValue != CommonConstants.SELECT)
        {
            FillRoleDropdownAsPerDepartment(Convert.ToInt32(ddlDepartment.SelectedValue));
        }
        else
        {
            ddlDesignation.Items.Clear();
            ddlDesignation.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
        }

        //textbox shoudl be madatory for Consultant Depts
        if ((ddlDepartment.SelectedValue == CommonConstants.RAVECONSULTANT_INDIA_ID)
            || (ddlDepartment.SelectedValue == CommonConstants.RAVECONSULTANT_USA_ID)
            || (ddlDepartment.SelectedValue == CommonConstants.RAVECONSULTANT_UK_ID))
        {
            CandidateEmailIdMandatoryMark.Visible = true;
        }
        else
        {
            CandidateEmailIdMandatoryMark.Visible = false;
        }

        if (ddlDepartment.SelectedValue == CommonConstants.SELECT)
        {
            CandidateEmailIdMandatoryMark.Visible = false;
        }

        // recruitment.Department = Convert.ToString(ddlDepartment.SelectedItem);
        if (ddlDepartment.SelectedValue != CommonConstants.SELECT)
        {
            int Deptid = Convert.ToInt32(ddlDepartment.SelectedItem.Value);

            FillResourceBussinesUnitAsperDept(Deptid);
        }
        else
        {
            ddlResourceBussinesUnit.SelectedIndex = -1;
        }



    }

    /// <summary>
    /// ddlDepartmen SelectedIndexChanged event handler
    /// </summary>
    protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillEmployeeTypeDropDown();
    }

    #endregion

    #region Method

    /// <summary>
    /// Function will call on Privious Click
    /// </summary>
    private void PreviousClick()
    {
        try
        {
            RecruitmentCurrentCount = RecruitmentCurrentCount - 1;
            RecruitmentPreviousCount = RecruitmentPreviousCount - 1;
            RecruitmentNextCount = RecruitmentNextCount + 1;

            EnableDisableButtons(RecruitmentCurrentCount, RecruitmentPreviousCount, RecruitmentNextCount);
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, PREVIOUS_CLICK, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
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
            if (Session[SessionNames.RECRUITMENTVIEWINDEX] != null)
            {
                // 27633-Ambar-Start
                //ht = (Hashtable)Session[SessionNames.MRFVIEWINDEX];
                ht = (Hashtable)Session[SessionNames.RECRUITMENTPREVIOUSHASHTABLE];
                // 27633-Ambar-End
                if (ht.Contains(PreviousIndex) == true)
                {
                    btnPrevious.Visible = true;
                }
                else
                {
                    btnPrevious.Visible = false;
                }

                if (ht.Contains(NextIndex) == true)
                {
                    btnNext.Visible = true;
                }
                else
                {
                    btnNext.Visible = false;
                }

                if (ht.Contains(currentIndex) == true)
                {
                    //CopyMRFDetail(Convert.ToInt32(ht[currentIndex]));
                    recruitment = new Recruitment();
                    recruitment.CandidateId = Convert.ToInt32(ht[currentIndex]);
                    ViewPipeLineDetails(recruitment.CandidateId);
                }
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, ENABLE_DISABLE_BUTTONS, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Function will call on Next Click
    /// </summary>
    private void NextClick()
    {
        try
        {
            RecruitmentCurrentCount = RecruitmentCurrentCount + 1;
            RecruitmentPreviousCount = RecruitmentPreviousCount + 1;
            RecruitmentNextCount = RecruitmentNextCount - 1;
            EnableDisableButtons(RecruitmentCurrentCount, RecruitmentPreviousCount, RecruitmentNextCount);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, NEXT_CLICK, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Fucntion will Set the Index of Page
    /// </summary>
    private void SetRecruitmentIndex()
    {
        try
        {
            Hashtable htnew = new Hashtable();
            if (Request.QueryString[INDEX] != null)
            {
                int countIndex = Convert.ToInt32(DecryptQueryString(INDEX));
                if (Session[SessionNames.RECRUITMENTVIEWINDEX] != null)
                {
                    //27633 
                    //htnew = (Hashtable)Session[SessionNames.MRFVIEWINDEX]; 
                    htnew = (Hashtable)Session[SessionNames.RECRUITMENTPREVIOUSHASHTABLE];
                }
                RecruitmentCurrentCount = ((Convert.ToInt16(Session[SessionNames.CURRENT_PAGE_INDEX_RS].ToString()) - 1) * 10) + countIndex;
                RecruitmentPreviousCount = RecruitmentCurrentCount - 1;
                RecruitmentNextCount = (htnew.Keys.Count - 2) - (((Convert.ToInt16(Session[SessionNames.CURRENT_PAGE_INDEX_RS].ToString()) - 1) * 10) + countIndex);



                if (htnew.Keys.Count == 1)
                {
                    btnNext.Visible = false;
                    btnPrevious.Visible = false;
                }
                else if (RecruitmentPreviousCount == -1)
                {
                    btnNext.Visible = true;
                    btnPrevious.Visible = false;
                }
                else if (RecruitmentNextCount == -1)
                {
                    btnNext.Visible = false;
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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, SET_RECRUITMENT_INDEX, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// View the pipeline details set all control in readonly mode.
    /// </summary>
    private void ViewPipeLineDetails(int CandidateId)
    {
        try
        {
            recruitment = Rave.HR.BusinessLayer.Recruitment.Recruitment.GetRecruitmentDetail(CandidateId);
            FillControlForViewPipeLineDetails(recruitment);
            SetControlForViewPipeLineDetails(recruitment);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, VIEW_PIPELINE_DETAILS, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Fill Candidate details in respective control
    /// </summary>
    private void FillControlForViewPipeLineDetails(BusinessEntities.Recruitment recruitment)
    {
        try
        {

            if (recruitment.BandId != 0)
            {
                ddlBand.ClearSelection();
                ddlBand.SelectedValue = Convert.ToString(recruitment.BandId);
            }
            if (recruitment.DesignationId != 0)
            {
                FillDesignationDropdownAsPerDepartment(Convert.ToInt32(recruitment.DepartmentId));
                //FillRoleDropDown(recruitment.DepartmentId);
                ddlDesignation.ClearSelection();
                ddlDesignation.Items.FindByValue(Convert.ToString(recruitment.DesignationId)).Selected = true;
                //ddlDesignation.SelectedItem.Value = Convert.ToString(recruitment.DesignationId);
                //ddlDesignation.SelectedIndex = 169;
            }
            if (recruitment.EmployeeTypeId != 0)
            {
                ddlEmpType.ClearSelection();
                ddlEmpType.SelectedValue = Convert.ToString(recruitment.EmployeeTypeId);
                recruitment.EmployeeType = ddlEmpType.SelectedItem.Text;
            }
            //Modified By kanchan.
            //Description:To resolve issue ID :16409
            //*
            if (recruitment.MRFId != 0)
            {
                ListItem list = new ListItem(recruitment.MRFCode.ToString(), recruitment.MRFId.ToString(), true);

                ///called the function to fill the mrf code drop down box.
                FillMRFCodeDropDowns();

                if (ddlMRFCode.Items.FindByValue(recruitment.MRFId.ToString()) == null)
                {
                    ddlMRFCode.Items.Insert(1, list);
                }
                if (ddlMRFCode.SelectedItem.Value != recruitment.MRFId.ToString())
                {
                    ddlMRFCode.ClearSelection();
                    ddlMRFCode.Items.FindByValue(Convert.ToString(recruitment.MRFId)).Selected = true;
                }
            }
            //*
            if (recruitment.PrefixId != 0)
            {
                ddlPrefix.ClearSelection();
                ddlPrefix.SelectedValue = Convert.ToString(recruitment.PrefixId);
                recruitment.Prefix = ddlPrefix.SelectedItem.Text;
            }
            //if (recruitment.ActualCTC != 0)
            //txtActualCTC.Text = Convert.ToString(recruitment.ActualCTC);

            //venkatesh  : Issue 40244 : 10/12/2013 : Starts  
            // Desc : Validation
            if (recruitment.Department != null)
            {
                txtDepartment.Text = recruitment.Department;
                ddlDepartment.ClearSelection();
                ddlDepartment.Items.FindByText(Convert.ToString(recruitment.Department)).Selected = true;
            }

            hfExpectectedDOJ.Value = recruitment.ExpectedJoiningDate.ToString("dd/MM/yyyy");
            recruitment.Prev_ExpectedJoiningDate = Convert.ToDateTime(hfExpectectedDOJ.Value);

            if (recruitment.Designation != null)
                hfDesignationId.Value = recruitment.Designation.ToString();
            //venkatesh  : Issue 40244 : 10/12/2013 : End  

            recruitment.Prev_Designation = hfDesignationId.Value;

            if (recruitment.ExpectedJoiningDate != null)
                ucDatePicker.Text = recruitment.ExpectedJoiningDate.ToString("dd/MM/yyyy");
            if (recruitment.FirstName != null)
                txtFirstName.Text = recruitment.FirstName;
            if (recruitment.LastName != null)
                txtLastName.Text = recruitment.LastName;
            if (recruitment.MiddleName != null)
                txtMiddleName.Text = recruitment.MiddleName;
            if (recruitment.ProjectName != null)
                txtProjectName.Text = recruitment.ProjectName;
            if (recruitment.ClientName != null)
                txtXClientName.Text = recruitment.ClientName;
            if (recruitment.ReportingTo != null)
            {

                txtReportingTo.Text = Rave.HR.BusinessLayer.Recruitment.Recruitment.GetMRfResponsiblePersonName(recruitment.ReportingTo);
                hidResponsiblePerson.Value = recruitment.ReportingTo;
            }
            string str = "-";
            //venkatesh  : Issue 40244 : 10/12/2013 : Starts  
            // Desc : Validation
            if (recruitment.Location != null)
            {
                if (!recruitment.Location.Equals(str))
                {
                    int flag = 1;

                    for (int i = 0; i < ddlLocation.Items.Count; i++)
                    {
                        string location = ddlLocation.Items[i].ToString();
                        if (location.Equals(recruitment.Location))
                        {
                            //ddlLocation.SelectedValue =Convert.ToString(281);
                            ddlLocation.SelectedIndex = i;
                            flag = 0;
                            break;
                        }
                    }
                    if (flag == 1)
                    {
                        ddlLocation.SelectedIndex = ddlLocation.Items.Count - 1;
                        txtLocation.Visible = true;
                        txtLocation.Text = recruitment.Location;
                    }
                    else
                    {
                        txtLocation.Visible = false;
                    }
                }
            }
            //venkatesh  : Issue 40244 : 10/12/2013 : End

            txtReleventYears.Text = recruitment.RelevantExperienceYear.ToString();
            txtReleventMonths.Text = recruitment.RelavantExperienceMonth.ToString();

            if (recruitment.ResourceBussinessUnit != 0)
            {
                ddlResourceBussinesUnit.ClearSelection();
                ddlResourceBussinesUnit.Items.FindByValue(recruitment.ResourceBussinessUnit.ToString()).Selected = true;
            }

            //Code added by kanchan for Issue ID 16457.
            //* Hiding of the text boxes when department is other then project.
            //if (!ddlMRFCode.SelectedItem.Text.Contains(CommonConstants.RAVEFORCASTEDPROJECT) || recruitment.ProjectId == 0 )
            if (recruitment.ProjectId == 0)
            {
                txtProjectName.Visible = false;
                lblPrjName.Visible = false;
                //MandatoryProjectName.Visible = false;

                txtXClientName.Visible = false;
                lblClientName.Visible = false;
                //MandatoryClientName.Visible = false;
            }
            else
            {
                txtProjectName.Visible = true;
                lblPrjName.Visible = true;
                //MandatoryProjectName.Visible = true;

                txtXClientName.Visible = true;
                lblClientName.Visible = true;
                //MandatoryClientName.Visible = true;
            }

            txtDepartment.Text = recruitment.Department;
            if (txtDepartment.Text == CommonConstants.RAVECONSULTANT_INDIA ||
                txtDepartment.Text == CommonConstants.RAVECONSULTANT_UK ||
                txtDepartment.Text == CommonConstants.RAVECONSULTANT_USA ||
                txtDepartment.Text == CommonConstants.RAVEFORCASTEDPROJECT
                )
            {
                lblClientName.Visible = true;
                txtXClientName.Visible = true;
            }

            //Addition of two new fields.
            if (recruitment.Address != null)
            {
                tbAdress.Text = recruitment.Address.ToString();
            }
            if (recruitment.PhoneNo != null)
            {
                tbPhoneNo.Text = recruitment.PhoneNo.ToString();
            }
            //Added to get the Landline No.
            if (recruitment.LandlineNo != null)
            {
                txtLandlineNo.Text = recruitment.LandlineNo.ToString();
            }

            //added to get emailid of candidate
            if (recruitment.CandidateEmailID != null)
            {
                txtEmailId.Text = recruitment.CandidateEmailID.ToString();
            }
            txtContractDuration.Text = recruitment.ContractDuration.ToString();

            if (recruitment.CandidateOfferAcceptedDate != null)
            {
                DatePickerOfferAcceptedDt.Text = recruitment.CandidateOfferAcceptedDate.ToString("dd/MM/yyyy");
            }
            if (!string.IsNullOrEmpty(recruitment.MRFPurpose))
                txtPurpose.Text = recruitment.MRFPurpose;

            //Rajan Kumar : Issue 39508: 31/01/2014 : Starts                        			 
            // Desc : Traninig for new joining employee. (Training Gaps).
            chkTrainingRequired.Checked = recruitment.IsTrainingRequired;
            txtTrainingSubject.Text = recruitment.TrainingSubject;
            ViewState[TRANING_SUBJECT] = recruitment.TrainingSubject;
            if (chkTrainingRequired.Checked)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "chkTrainingRequired", "TrainingRequired();", true);
            }
            // Rajan Kumar : Issue 39508: 31/01/2014 : END
            hidMrfStatus.Value = recruitment.MRFStatus.ToString();
            Session[SessionNames.RECRUITMENT] = recruitment;

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FILL_CONTROL_FOR_VIEW, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// Set all the control in view mode
    /// </summary>
    private void SetControlForViewPipeLineDetails(BusinessEntities.Recruitment recruitment)
    {
        try
        {

            lblHeaderPipelineDetails.Text = VIEW_PIPELINE_DETAILS;
            pnlAdd.Visible = false;
            pnlViewRecruitment.Visible = true;
            ddlBand.Enabled = false;
            ddlDesignation.Enabled = false;
            ddlEmpType.Enabled = false;
            ddlMRFCode.Enabled = false;
            ddlPrefix.Enabled = false;
            //txtActualCTC.Enabled = false;
            txtDepartment.Enabled = false;
            ucDatePicker.IsEnable = false;
            txtFirstName.Enabled = false;
            txtLastName.Enabled = false;
            txtMiddleName.Enabled = false;
            txtProjectName.Enabled = false;
            txtReportingTo.Enabled = false;
            ddlLocation.Enabled = false;
            txtLocation.Enabled = false;
            imgResponsiblePersonSearch.Visible = false;
            ddlDepartment.Enabled = false;
            ddlResourceBussinesUnit.Enabled = false;
            tbPhoneNo.Enabled = false;
            tbAdress.Enabled = false;
            txtLandlineNo.Enabled = false;
            txtEmailId.Enabled = false;
            txtContractDuration.Enabled = false;
            DatePickerOfferAcceptedDt.IsEnable = false;
            txtPurpose.Enabled = false;
            txtReleventMonths.Enabled = false;
            txtReleventYears.Enabled = false;
            //Rajan Kumar : Issue 39508: 31/01/2014 : Starts                        			 
            // Desc : Traninig for new joining employee. (Training Gaps).
            chkTrainingRequired.Enabled = false;
            txtTrainingSubject.Enabled = false;
            // Rajan Kumar : Issue 39508: 31/01/2014 : END

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, SET_CONTROL_FOR_VIEW, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// After adding the pipeline details reset all control, so that we can add new candidate record.
    /// </summary>
    private void ResetControl()
    {
        try
        {
            ddlMRFCode.SelectedIndex = -1;
            ddlBand.SelectedIndex = 0;
            ddlDesignation.SelectedIndex = 0;
            ddlEmpType.SelectedIndex = 0;
            ddlPrefix.SelectedIndex = 0;
            //txtActualCTC.Text = string.Empty;
            txtDepartment.Text = string.Empty;
            ucDatePicker.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtMiddleName.Text = string.Empty;
            txtProjectName.Text = string.Empty;
            txtReportingTo.Text = string.Empty;
            txtXClientName.Text = string.Empty;
            FillMRFCodeDropDowns();
            ddlDepartment.SelectedIndex = -1;
            ddlLocation.SelectedIndex = -1;
            ddlResourceBussinesUnit.SelectedIndex = -1;
            txtReleventMonths.Text = string.Empty;
            txtReleventYears.Text = string.Empty;
            tbAdress.Text = string.Empty;
            tbPhoneNo.Text = string.Empty;
            txtEmailId.Text = string.Empty;
            txtLandlineNo.Text = string.Empty;
            CandidateEmailIdMandatoryMark.Visible = false;
            ddlDepartment.SelectedItem.Text = CommonConstants.SELECT;
            txtContractDuration.Text = string.Empty;
            LblContractDuration.Visible = false;
            txtContractDuration.Visible = false;
            //DatePickerOfferAcceptedDt.Text = DateTime.Now.ToString(DATEFORMAT);
            DatePickerOfferAcceptedDt.Text = string.Empty;
            //Rajan Kumar : Issue 39508: 31/01/2014 : Starts                        			 
            // Desc : Traninig for new joining employee. (Training Gaps).
            chkTrainingRequired.Checked = false;
            txtTrainingSubject.Text = string.Empty;
            // Rajan Kumar : Issue 39508: 31/01/2014 : END
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, RESET_CONTROL, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// for insertion of record take the control's value.
    /// </summary>
    private void GetControlValue()
    {
        try
        {
            AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
            // Rajan Kumar : Issue 46252: 12/02/2014 : Starts                        			 
            // Desc : In MRF history need to implemented in all cases in RMS.
            //Pass Email to know who is going to modified the data 
            //string UserMailId = objRaveHRAuthorizationManager.getLoggedInUser();
            string UserMailId = objRaveHRAuthorizationManager.getLoggedInUserEmailId();
            // Rajan Kumar : Issue 46252: 12/02/2014 : END           
            recruitment = (BusinessEntities.Recruitment)Session["Recruitment"];
            recruitment.EmailId = UserMailId;
            recruitment.Designation = ddlDesignation.SelectedItem.Text;
            // Rajan Kumar : Issue 46252: 10/02/2014 : Starts                        			 
            // Desc : In MRF history need to implemented in all cases in RMS.
            // Prev_Designation is not set after page post back,again set Prev_Designation so that in  email Prev_Designation and Designation
            // use to coampare
            if (hfDesignationId.Value != null)
            {
                recruitment.Prev_Designation = hfDesignationId.Value;
            }
            // Rajan Kumar : Issue 46252: 10/02/2014 : END  
            // 29516-Ambar-Start-05092011
            recruitment.DesignationId = Convert.ToInt32(ddlDesignation.SelectedItem.Value);
            // 29516-Ambar-End-05092011
            recruitment.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedItem.Value);
            recruitment.Department = ddlDepartment.SelectedItem.Text;
            recruitment.EmployeeType = ddlEmpType.SelectedItem.Text;
            recruitment.EmployeeTypeId = Convert.ToInt32(ddlEmpType.SelectedItem.Value);
            recruitment.Prefix = ddlPrefix.SelectedItem.Text;
            recruitment.PrefixId = Convert.ToInt32(ddlPrefix.SelectedValue);
            recruitment.FirstName = txtFirstName.Text.Trim();
            recruitment.LastName = txtLastName.Text.Trim();
            recruitment.Band = ddlBand.SelectedItem.Value;

            // Mohamed : Issue 48476 : 29/01/2014 : Starts                        			  
            // Desc : Assigned 2 Resource for 1 MRF _ Vinayak Narkar -- for Edit Pipeline details

            recruitment.CandidateId = Convert.ToInt32(hidCandidateId.Value);
            BusinessEntities.Recruitment Rec_MRF_Project_Id = new BusinessEntities.Recruitment();
            Rec_MRF_Project_Id = Rave.HR.BusinessLayer.Recruitment.Recruitment.GetRecruitmentDetail(recruitment.CandidateId);
            recruitment.ProjectId = Rec_MRF_Project_Id.ProjectId;

            // Mohamed : Issue 48476 : 29/01/2014 : Ends

            //if (//txtActualCTC.Text != "")
            //    recruitment.ActualCTC = Convert.ToDecimal(//txtActualCTC.Text.Trim());
            //else
            //    recruitment.ActualCTC = 0;

            if (txtMiddleName.Text != null)
                recruitment.MiddleName = txtMiddleName.Text.Trim();
            recruitment.ExpectedJoiningDate = Convert.ToDateTime(ucDatePicker.Text);
            recruitment.ReportingTo = hidResponsiblePerson.Value.ToString();
            if (ddlLocation.SelectedItem.Text == OTHER_LOCATION)
            {
                if (txtLocation.Text.Trim().Length > 0)
                    recruitment.Location = txtLocation.Text.Trim();
            }
            else if (ddlLocation.SelectedItem.Text == CommonConstants.SELECT)
            {
                recruitment.Location = null;
            }
            else
            {
                recruitment.Location = ddlLocation.SelectedItem.Text;
            }
            recruitment.RelavantExperienceMonth = Convert.ToInt32(txtReleventMonths.Text);
            recruitment.RelevantExperienceYear = Convert.ToInt32(txtReleventYears.Text);

            if (ddlResourceBussinesUnit.SelectedItem.Text != CommonConstants.SELECT)
            {
                recruitment.ResourceBussinessUnit = Convert.ToInt32(ddlResourceBussinesUnit.SelectedItem.Value);
            }
            if (tbAdress.Text != null)
            {
                recruitment.Address = tbAdress.Text;
            }
            if (tbPhoneNo.Text != null)
            {
                recruitment.PhoneNo = tbPhoneNo.Text;
            }
            if (!string.IsNullOrEmpty(txtLandlineNo.Text))
            {
                recruitment.LandlineNo = txtLandlineNo.Text;
            }
            recruitment.CandidateEmailID = txtEmailId.Text.Trim();

            recruitment.CandidateOfferAcceptedDate = Convert.ToDateTime(DatePickerOfferAcceptedDt.Text);

            recruitment.MRFPurpose = txtPurpose.Text.Trim();

            //Modified to fix the error in the mail.
            recruitment.MRFCode = ddlMRFCode.SelectedItem.Text;

            if (Session[SessionNames.STATUS] != null)
                recruitment.MRFStatus = int.Parse(Session[SessionNames.STATUS].ToString());
            else
                recruitment.MRFStatus = Convert.ToInt32(MasterEnum.MRFStatus.PendingExpectedResourceJoin);
            //Rajan Kumar : Issue 39508: 03/02/2014 : Starts                        			 
            // Desc : Traninig for new joining employee. (Training Gaps).
            recruitment.IsTrainingRequired = chkTrainingRequired.Checked;
            recruitment.TrainingSubject = txtTrainingSubject.Text;
            // Rajan Kumar : Issue 39508: 03/02/2014 : END
            //Mohamed : Issue 50306 : 09/09/2014 : Starts                        			  
            //Desc : Expected Joinee's details edited[MRF Code: MRF_Testing_SrTstA_0387] old  Joining date is default date - Mail for de-linking MRF.            
            recruitment.Prev_MRFId = Convert.ToInt32(hdOldMRFId.Value);
            recruitment.MRFId = Convert.ToInt32(ddlMRFCode.SelectedValue.ToString());
            //Mohamed : Issue 50306 : 09/09/2014 : Ends

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, GET_CONTTROL_VALUE, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Get all candidate record for view purpose.
    /// </summary>
    private void GetRecruitmentDetail()
    {
        try
        {
            recruitment = (BusinessEntities.Recruitment)Session["Recruitment"];
            AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
            string UserMailId = objRaveHRAuthorizationManager.getLoggedInUserEmailId();

            #region Modified By Mohamed Dangra
            // Mohamed : Issue 48476 : 29/01/2014 : Starts                        			  
            // Desc : Assigned 2 Resource for 1 MRF _ Vinayak Narkar

            recruitment.MRFId = Convert.ToInt32(ddlMRFCode.SelectedValue.ToString());
            BusinessEntities.Recruitment Rec_MRF_Project_Id = new BusinessEntities.Recruitment();
            Rec_MRF_Project_Id = Rave.HR.BusinessLayer.Recruitment.Recruitment.GetMrfDetail(recruitment.MRFId);
            recruitment.ProjectId = Rec_MRF_Project_Id.ProjectId;
            recruitment.ActualCTC = 0;

            // Mohamed : Issue 48476 : 29/01/2014 : Ends
            #endregion Modified By Mohamed Dangra

            recruitment.EmailId = UserMailId;
            recruitment.DesignationId = Convert.ToInt32(ddlDesignation.SelectedValue);
            recruitment.Designation = ddlDesignation.SelectedItem.Text;
            recruitment.EmployeeType = ddlEmpType.SelectedItem.Text;
            recruitment.EmployeeTypeId = Convert.ToInt32(ddlEmpType.SelectedValue);
            recruitment.PrefixId = Convert.ToInt32(ddlPrefix.SelectedItem.Value);
            recruitment.Prefix = ddlPrefix.SelectedItem.Text;

            recruitment.FirstName = txtFirstName.Text.Trim();
            recruitment.LastName = txtLastName.Text.Trim();
            recruitment.Band = ddlBand.SelectedItem.Value;
            recruitment.Department = ddlDepartment.SelectedItem.Text;

            #region MyRegion
            //if (txtActualCTC.Text != "")
            //{
            //    int splitLength = 0;
            //    int intActualCTC = 10;
            //    int intCTC = 0;
            //    int actualCTCint = 0;
            //    string[] actualCTC = txtActualCTC.Text.Split(',');
            //    int count = actualCTC.Count();
            //    if (count == 1)
            //        recruitment.ActualCTC = Convert.ToDecimal(txtActualCTC.Text);
            //    else
            //    {
            //        int counterValue = 0;
            //        foreach (string ctc in actualCTC)
            //        {
            //            counterValue += 1;
            //            intCTC = Convert.ToInt32(ctc);
            //            actualCTCint += intCTC;
            //            if (counterValue < actualCTC.Length)
            //            {
            //                intActualCTC = 10;
            //                splitLength = actualCTC[counterValue].Length;
            //                for (int counter = 1; counter < splitLength; counter++)
            //                {
            //                    intActualCTC = intActualCTC * 10;
            //                }
            //                actualCTCint = actualCTCint * intActualCTC;
            //            }
            //        }
            //        recruitment.ActualCTC = Convert.ToDecimal(actualCTCint);
            //    }
            //}
            //else
            //    recruitment.ActualCTC = 0; 
            #endregion

            if (ddlLocation.SelectedItem.Text == OTHER_LOCATION)
            {
                if (txtLocation.Text.Trim().Length > 0)
                    recruitment.Location = txtLocation.Text.Trim();
            }
            else if (ddlLocation.SelectedItem.Text == CommonConstants.SELECT)
            {
                recruitment.Location = null;
            }
            else
            {
                recruitment.Location = ddlLocation.SelectedItem.Text;
            }
            if (txtMiddleName.Text != null)
                recruitment.MiddleName = txtMiddleName.Text.Trim();
            recruitment.ExpectedJoiningDate = Convert.ToDateTime(ucDatePicker.Text);
            recruitment.ReportingTo = hidResponsiblePerson.Value.ToString();

            if (!string.IsNullOrEmpty(txtReleventYears.Text))
                recruitment.RelevantExperienceYear = Convert.ToInt32(txtReleventYears.Text);

            if (!string.IsNullOrEmpty(txtReleventMonths.Text))
                recruitment.RelavantExperienceMonth = Convert.ToInt32(txtReleventMonths.Text);

            if (ddlResourceBussinesUnit.SelectedItem.Text != CommonConstants.SELECT)
            {
                recruitment.ResourceBussinessUnit = Convert.ToInt32(ddlResourceBussinesUnit.SelectedItem.Value);
            }
            if (ddlDepartment.SelectedItem.Text != CommonConstants.SELECT)
            {
                recruitment.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedItem.Value);
            }
            if (tbPhoneNo.Text != null)
            {
                recruitment.PhoneNo = tbPhoneNo.Text.ToString();
            }
            if (tbAdress.Text != null)
            {
                recruitment.Address = tbAdress.Text;
            }
            if (!string.IsNullOrEmpty(txtLandlineNo.Text))
            {
                recruitment.LandlineNo = txtLandlineNo.Text;
            }
            if (txtEmailId.Text != null)
            {
                recruitment.CandidateEmailID = txtEmailId.Text;
            }
            if (txtContractDuration.Text != null)
            {
                recruitment.ContractDuration = Convert.ToInt32(txtContractDuration.Text);
            }
            if (DatePickerOfferAcceptedDt.Text != null)
            {
                recruitment.CandidateOfferAcceptedDate = Convert.ToDateTime(DatePickerOfferAcceptedDt.Text);
            }
            recruitment.MRFPurpose = txtPurpose.Text.Trim();

            //Modified to fix the error in the mail.
            recruitment.MRFCode = ddlMRFCode.SelectedItem.Text.ToString();
            //Rajan Kumar : Issue 39508: 31/01/2014 : Starts                        			 
            // Desc : Traninig for new joining employee. (Training Gaps).
            if (chkTrainingRequired.Checked == true)
            {
                recruitment.IsTrainingRequired = true;
                recruitment.TrainingSubject = txtTrainingSubject.Text.Trim();
            }
            // Rajan Kumar : Issue 39508: 31/01/2014 : END
            int result = Rave.HR.BusinessLayer.Recruitment.Recruitment.AddPipelineDetails(recruitment);
            //Mohamed Dangra : Issue 52488: 14/08/2014 : Starts                        			 
            // Desc : Validate Dublicate candidate entry on in add pipeline
            if (result == -1)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Pipeline details for these MRF already exists";
            }
            else if (result == -2)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Provided Candidate is already assigned.";
            }
            // Mohamed Dangra : Issue 52488: 14/08/2014 : END
            else if (result != 0)
            {
                lblMessage.Visible = true;
                //Rave.HR.BusinessLayer.Recruitment.Recruitment.SendMail(recruitment);
                lblMessage.Text = "Pipeline details for raised MRF is added successfully,email notification is sent";
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, GET_RECRUITMNET_DETAIL, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Fill all drop downs:-MRFCode,Designation,Band,Prefix etc.
    /// </summary>
    private void FillDropDowns()
    {
        try
        {
            if (Request.QueryString[QueryStringConstants.CANDIDATEID] != null)
            {
                //to get those MRFCode whose status is pending expected resource join.
                FillViewMRFCodeDropDowns();
                ddlPrefix.Focus();
            }
            else
            {
                //to get those MRFCode whose status is pending external allocation.
                FillMRFCodeDropDowns();

            }
            FillPrefixDropDown();
            FillEmployeeBandDropDown();
            FillEmployeeTypeDropDown();
            FillLocationDropDown();
            FillDepartmentDropDown();

            //Function called to fill ddlResourceBussinesUnit.
            FillResourceBussinesUnitDropDown();


        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FILL_DROP_DOWNS, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Show the Location of Candidate.
    /// </summary>
    private void FillLocationDropDown()
    {

        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        // Initialise Business layer object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        try
        {
            raveHRCollection = master.FillDropDownsBL((int)Common.EnumsConstants.Category.CandidateLocation);
            //Check Collection is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSOurce to Collection
                ddlLocation.DataSource = raveHRCollection;

                //Assign DataText Filed to DropDown
                ddlLocation.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign DataValue Field to Dropdown
                ddlLocation.DataValueField = CommonConstants.DDL_DataValueField;

                //Bind Dropdown
                ddlLocation.DataBind();

                //Insert Select as a item for dropdown
                ddlLocation.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
                ddlLocation.Items.Insert(6, "Other Location");

                raveHRCollection.Clear();
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FILL_MRF_CODE_DROP_DOWNS, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// Show the MRFCode.
    /// </summary>
    private void FillMRFCodeDropDowns()
    {
        try
        {

            //Calling Business layer FillDropDown Method
            raveHRCollection = Rave.HR.BusinessLayer.Recruitment.Recruitment.GetMrfCode();

            //Check Collection is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSOurce to Collection
                ddlMRFCode.DataSource = raveHRCollection;

                //Assign DataText Filed to DropDown
                ddlMRFCode.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign DataValue Field to Dropdown
                ddlMRFCode.DataValueField = CommonConstants.DDL_DataValueField;

                //Bind Dropdown
                ddlMRFCode.DataBind();

                //Insert Select as a item for dropdown
                ddlMRFCode.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                raveHRCollection.Clear();
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FILL_MRF_CODE_DROP_DOWNS, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Show the MRFCode.
    /// </summary>
    private void FillViewMRFCodeDropDowns()
    {
        try
        {

            //Calling Business layer FillDropDown Method
            raveHRCollection = Rave.HR.BusinessLayer.Recruitment.Recruitment.ViewMrfCode();

            //Check Collection is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSOurce to Collection
                ddlMRFCode.DataSource = raveHRCollection;

                //Assign DataText Filed to DropDown
                ddlMRFCode.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign DataValue Field to Dropdown
                ddlMRFCode.DataValueField = CommonConstants.DDL_DataValueField;

                //Bind Dropdown
                ddlMRFCode.DataBind();

                //Insert Select as a item for dropdown
                ddlMRFCode.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                raveHRCollection.Clear();
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FILL_VIEW_MRF_CODE_DROPDOWNS, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    ///  Fills the Prefix dropdown
    /// </summary>
    private void FillPrefixDropDown()
    {
        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        // Initialise Business layer object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        try
        {
            // Call the Business layer method
            raveHRCollection = master.FillDropDownsBL(Convert.ToInt32(EnumsConstants.Category.Prefix));

            if (raveHRCollection != null)
            {
                // Assign the data source to dropdown
                ddlPrefix.DataSource = raveHRCollection;

                ddlPrefix.DataTextField = CommonConstants.DDL_DataTextField;
                ddlPrefix.DataValueField = CommonConstants.DDL_DataValueField;

                // Bind the data to dropdown
                ddlPrefix.DataBind();

                // Default value of dropdown is "Select"
                ddlPrefix.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
            }
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FILL_PREFIX_DROP_DOWN, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    ///  Fills the Employee Band dropdown
    /// </summary>
    private void FillEmployeeBandDropDown()
    {

        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        // Initialise Business layer object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        try
        {
            // Call the Business layer method
            raveHRCollection = master.FillDropDownsBL(Convert.ToInt32(EnumsConstants.Category.EmployeeBand));

            if (raveHRCollection != null)
            {
                // Assign the data source to dropdown
                ddlBand.DataSource = raveHRCollection;

                ddlBand.DataTextField = CommonConstants.DDL_DataTextField;
                ddlBand.DataValueField = CommonConstants.DDL_DataValueField;

                // Bind the data to dropdown
                ddlBand.DataBind();

                // Default value of dropdown is "Select"
                ddlBand.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
            }
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FILL_EMPLOYEE_BANDDROPDOWN, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    ///  Fills the Employee Type dropdown
    /// </summary>
    private void FillEmployeeTypeDropDown()
    {
        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        // Initialise Business layer object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        try
        {
            // Call the Business layer method
            raveHRCollection = master.FillDropDownsBL(Convert.ToInt32(EnumsConstants.Category.EmployeeType));

            if (raveHRCollection != null)
            {
                // Assign the data source to dropdown
                ddlEmpType.DataSource = raveHRCollection;

                ddlEmpType.DataTextField = CommonConstants.DDL_DataTextField;
                ddlEmpType.DataValueField = CommonConstants.DDL_DataValueField;

                // Bind the data to dropdown
                ddlEmpType.DataBind();

                // Default value of dropdown is "Select"
                ddlEmpType.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                if (ddlDepartment.SelectedIndex > 0 && ((ddlDepartment.SelectedItem.Text == CommonConstants.RAVECONSULTANT_INDIA ||
                ddlDepartment.SelectedItem.Text == CommonConstants.RAVECONSULTANT_UK ||
                ddlDepartment.SelectedItem.Text == CommonConstants.RAVECONSULTANT_USA)))
                {
                    ddlEmpType.Items.Remove(ddlEmpType.Items.FindByText(CommonConstants.Employee_Type_ASE.ToString()));
                    ddlEmpType.Items.Remove(ddlEmpType.Items.FindByText(CommonConstants.Employee_Type_Permanent.ToString()));
                }

                if (ddlDesignation.SelectedIndex > 0 && ((ddlDesignation.SelectedItem.Text.Trim().ToUpper() == CommonConstants.Consultant.Trim().ToUpper() ||
                ddlDesignation.SelectedItem.Text.Trim().ToUpper() == CommonConstants.Consultant_Tester.Trim().ToUpper())))
                {
                    ddlEmpType.Items.Remove(ddlEmpType.Items.FindByText(CommonConstants.Employee_Type_ASE.ToString()));
                    ddlEmpType.Items.Remove(ddlEmpType.Items.FindByText(CommonConstants.Employee_Type_Permanent.ToString()));
                }

            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FILL_EMPLOYEE_TYPE_DROP_DOWN, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    private void FillMRFDetails(int mrfId)
    {
        try
        {
            recruitment = Rave.HR.BusinessLayer.Recruitment.Recruitment.GetMrfDetail(mrfId);
            recruitment.MRFId = mrfId;
            if (recruitment.Department != null)
            {
                txtDepartment.Text = recruitment.Department;

                #region Changes made by Gaurav Thakkar
                /*Changes made as per discussion that when department is other than "Projects"
                  than Project Name and Client Name will not be visible*/

                //vandana
                if (txtDepartment.Text == CommonConstants.RAVECONSULTANT_INDIA.ToString() ||
                    txtDepartment.Text == CommonConstants.RAVECONSULTANT_UK.ToString() ||
                    txtDepartment.Text == CommonConstants.RAVECONSULTANT_USA.ToString() ||
                    txtDepartment.Text == CommonConstants.RAVEFORCASTEDPROJECT
                    )
                {
                    rowProjClientDetails.Visible = true;
                    lblClientName.Visible = true;
                    txtXClientName.Visible = true;
                    //MandatoryClientName.Visible = true;

                    lblPrjName.Visible = false;
                    txtProjectName.Visible = false;
                    //MandatoryProjectName.Visible = false;

                    LblContractDuration.Visible = true;
                    txtContractDuration.Visible = true;

                }
                else if (txtDepartment.Text == MasterEnum.Departments.Projects.ToString())
                {
                    rowProjClientDetails.Visible = true;
                    lblClientName.Visible = true;
                    txtXClientName.Visible = true;
                    lblPrjName.Visible = true;
                    txtProjectName.Visible = true;
                    //MandatoryProjectName.Visible = true;

                }
                else
                {
                    rowProjClientDetails.Visible = false;
                    lblClientName.Visible = false;
                    txtXClientName.Visible = false;
                    lblPrjName.Visible = false;
                    txtProjectName.Visible = false;
                }

                #endregion

                if (recruitment.DepartmentId > 0)
                    FillDesignationDropdownAsPerDepartment(recruitment.DepartmentId);
            }
            if (recruitment.ClientName != null)
                txtXClientName.Text = recruitment.ClientName;
            if (recruitment.ProjectName != null)
                txtProjectName.Text = recruitment.ProjectName;
            if (recruitment.ReportingTo != null)
            {
                txtReportingTo.Text = Rave.HR.BusinessLayer.Recruitment.Recruitment.GetMRfResponsiblePersonName(recruitment.ReportingTo);
                hidResponsiblePerson.Value = recruitment.ReportingTo;
            }
            txtContractDuration.Text = recruitment.ContractDuration.ToString();

            txtPurpose.Text = recruitment.MRFPurpose;

            Session[SessionNames.RECRUITMENT] = recruitment;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FILL_MRF_DETAILS, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
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
                ddlDesignation.DataSource = raveHRCollection;

                ddlDesignation.DataTextField = CommonConstants.DDL_DataTextField;
                ddlDesignation.DataValueField = CommonConstants.DDL_DataValueField;

                // Bind the data to dropdown
                ddlDesignation.DataBind();

                // Default value of dropdown is "Select"
                ddlDesignation.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);



            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FILL_ROLE_DROP_DOWN, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Based on the Department selected, fill the Role 
    /// </summary>
    private void FillDesignationDropdownAsPerDepartment(int deptId)
    {
        try
        {
            //FillRoleDepartmentWise(deptId);
            FillRoleDropdownAsPerDepartment(deptId);
            ddlDesignation.Enabled = true;
            //ddlDepartment_SelectedIndexChanged(null, null);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FILL_DESIGNATION_AS_PER_DEPARTMENT, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// Based on the Department , fill the Designation.
    /// </summary>
    private void FillRoleDepartmentWise(int DepartmentId)
    {
        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        Rave.HR.BusinessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.BusinessLayer.MRF.MRFDetail();
        try
        {
            raveHRCollection = new RaveHRCollection();
            //Calling Business layer FillDropDown Method
            raveHRCollection = mRFDetail.GetRoleDepartmentWiseBL(DepartmentId);

            //Check Collection is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSOurce to Collection
                ddlDesignation.DataSource = raveHRCollection;

                //Assign DataText Filed to DropDown
                ddlDesignation.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign DataValue Field to Dropdown
                ddlDesignation.DataValueField = CommonConstants.DDL_DataValueField;

                //Bind Dropdown
                ddlDesignation.DataBind();

                //Insert Select as a item for dropdown
                ddlDesignation.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                raveHRCollection.Clear();
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FILL_ROLE_DEPARTMENTWISE, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Remove the Recruitment Record.
    /// </summary>
    private void RemoveRecruitmentRecord()
    {
        try
        {
            recruitment = (BusinessEntities.Recruitment)Session[SessionNames.RECRUITMENT];
            recruitment.Reason = txtReason.Text.ToString();

            // Rajan Kumar : Issue 46252: 12/02/2014 : Starts                        			 
            // Desc : In MRF history need to implemented in all cases in RMS.
            //Pass Email to know who is going to modified the data            
            AuthorizationManager authoriseduser = new AuthorizationManager();
            recruitment.EmailId = authoriseduser.getLoggedInUserEmailId();
            // Rajan Kumar : Issue 46252: 12/02/2014 : END
            int removeRecruitmentRecord = Rave.HR.BusinessLayer.Recruitment.Recruitment.RemovePipelineDetails(recruitment);
            //if (removeRecruitmentRecord == 1)
            //{            
            //}
            Response.Redirect("RecruitmentSummary.aspx", false);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, REMOVE_RECRUITMNET_RECORD, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Set the control for Edit.
    /// </summary>
    private void EditPipeLineDetails()
    {
        try
        {
            SetControlForEditPipeLineDetails();
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, EDIT_PIPELINEDETAILS, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Get the control for Edit.
    /// </summary>
    private void GetEditPipeLineDetails()
    {
        try
        {
            bool sendMail = false;
            GetControlValue();

            if (chkSelect.Checked)
            {
                recruitment.IsResourceJoined = 1;
                if (DatePickerCandidateJoined.Text != null)
                    //28568-Subhra-start
                    //recruitment.ResourceJoinedDate = Convert.ToDateTime(ucDatePicker.Text);
                    recruitment.ResourceJoinedDate = Convert.ToDateTime(DatePickerCandidateJoined.Text);
                //28568-subhra-end                
            }
            else
            {
                recruitment.IsResourceJoined = 0;
                recruitment.ResourceJoinedDate = System.DateTime.MinValue;
            }

            previousMrfCode = hdMrfCode.Value;

            bool IsMailSentStatus = false;
            if (ucDatePicker.Text == hfExpectectedDOJ.Value)
            {
                IsMailSentStatus = false;
            }
            else
            {
                IsMailSentStatus = true;
            }

            // Jignyasa : Issue 42211 : 6/06/2013 : Starts 
            // Desc : Check if current effective joining date is same as previous effective joining date and current designation id is same as previous designation id      
            bool chkDesignationAndJoiningDate = true;

            if (recruitment.Prev_ExpectedJoiningDate != recruitment.ExpectedJoiningDate || recruitment.Prev_Designation != recruitment.Designation)
            {
                chkDesignationAndJoiningDate = false;
            }
            else if (recruitment.IsResourceJoined == 1)
            {
                chkDesignationAndJoiningDate = false;
            }
            //Rajan Kumar : Issue 39508: 05/02/2014 : Starts                        			 
            //Desc : Traninig for new joining employee. (Training Gaps).
            string mode = TrainingSubjectMode();
            // Rajan Kumar : Issue 39508: 05/02/2014 : END
            int flag = Rave.HR.BusinessLayer.Recruitment.Recruitment.EditPipelineDetails(recruitment, previousMrfCode, IsMailSentStatus, true, chkDesignationAndJoiningDate, mode);
            // Jignyasa : Issue 42211 : 6/06/2013 : Ends 

            Response.Redirect("RecruitmentSummary.aspx", false);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, GET_EDIT_PIPELINEDETAILS, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// set the control for Edit.
    /// </summary>
    private void SetControlForEditPipeLineDetails()
    {
        try
        {
            ddlBand.Enabled = true;
            ddlDesignation.Enabled = true;
            ddlEmpType.Enabled = true;
            //ddlMRFCode.Enabled = true;
            ddlPrefix.Enabled = true;
            //txtActualCTC.Enabled = true;
            txtDepartment.Enabled = false;
            ucDatePicker.IsEnable = true;
            txtFirstName.Enabled = true;
            txtLastName.Enabled = true;
            txtMiddleName.Enabled = true;
            txtProjectName.Enabled = false;
            txtReportingTo.Enabled = true;
            imgResponsiblePersonSearch.Visible = true;
            pnlViewRecruitment.Visible = false;
            pnlUpdate.Visible = true;
            txtLandlineNo.Enabled = true;
            txtEmailId.Enabled = true;
            DatePickerOfferAcceptedDt.IsEnable = true;
            lblHeaderPipelineDetails.Text = EDIT_PIPELINE_DETAILS;

            ddlPrefix.Focus();
            ddlLocation.Enabled = true;
            txtLocation.Enabled = true;
            ddlDepartment.Enabled = true;
            ddlResourceBussinesUnit.Enabled = true;
            txtReleventYears.Enabled = true;
            txtReleventMonths.Enabled = true;
            tbAdress.Enabled = true;
            tbPhoneNo.Enabled = true;

            if (ddlDepartment.SelectedItem.Text == CommonConstants.RAVECONSULTANT_INDIA ||
                ddlDepartment.SelectedItem.Text == CommonConstants.RAVECONSULTANT_UK ||
                ddlDepartment.SelectedItem.Text == CommonConstants.RAVECONSULTANT_USA)
            {
                txtContractDuration.Visible = true;
                LblContractDuration.Visible = true;
            }
            else
            {
                txtContractDuration.Visible = false;
                LblContractDuration.Visible = false;
            }

            if (Convert.ToInt32(hidMrfStatus.Value) == Convert.ToInt32(MasterEnum.MRFStatus.ResourceJoin) || Convert.ToInt32(hidMrfStatus.Value) == Convert.ToInt32(MasterEnum.MRFStatus.PendingNewEmployeeAllocation))
            {
                pnlJoiningDetails.Visible = false;
            }
            else
            {
                pnlJoiningDetails.Visible = true;
            }

            btnEditMRFCode.Visible = true;

            hdMrfCode.Value = ddlMRFCode.SelectedItem.Text.ToString();
            //Mohamed : Issue 50306 : 09/09/2014 : Starts                        			  
            //Desc : Expected Joinee's details edited[MRF Code: MRF_Testing_SrTstA_0387] old  Joining date is default date - Mail for de-linking MRF.
            hdOldMRFId.Value = ddlMRFCode.SelectedItem.Value.ToString();
            //Mohamed : Issue 50306 : 09/09/2014 : Ends
            //Rajan Kumar : Issue 39508: 31/01/2014 : Starts                        			 
            // Desc : Traninig for new joining employee. (Training Gaps).
            chkTrainingRequired.Enabled = true;
            txtTrainingSubject.Enabled = true;
            if (chkTrainingRequired.Checked)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "chkTrainingRequired", "TrainingRequired();", true);
            }
            // Rajan Kumar : Issue 39508: 31/01/2014 : END
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "BindGridRecruitmentSummary", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }


    /// <summary>
    /// Get Department Dropdown values
    /// </summary>
    private void FillDepartmentDropDown()
    {
        try
        {
            //Declaring Master Class Object
            Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

            //Calling Fill dropdown Business layer method to fill the dropdown
            raveHRCollection = master.FillDepartmentDropDownBL();


            //Check Collection object is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSource to dropdown
                ddlDepartment.DataSource = raveHRCollection;

                //Assign DataText Field to dropdown
                ddlDepartment.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign Data Value field to dropdown
                ddlDepartment.DataValueField = CommonConstants.DDL_DataValueField;

                //Bind Dropdown
                ddlDepartment.DataBind();

                //Insert Select as a Item for Dropdown
                ddlDepartment.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                //remove the Dept Name called RaveDevelopment from Dropdown -Vandna
                ddlDepartment.Items.Remove(ddlDepartment.Items.FindByValue(CommonConstants.DeptId_RaveDevelopment.ToString()));
                ddlDepartment.Items.Remove(ddlDepartment.Items.FindByValue(CommonConstants.DeptId_RaveForecastedProject.ToString()));

                //Issue Id : Mahendra remove below dept
                ddlDepartment.Items.Remove(ddlDepartment.Items.FindByValue(CommonConstants.DeptId_PRESALES_USA.ToString()));
                ddlDepartment.Items.Remove(ddlDepartment.Items.FindByValue(CommonConstants.DeptId_PRESALES_UK.ToString()));
                ddlDepartment.Items.Remove(ddlDepartment.Items.FindByValue(CommonConstants.DeptId_PRESALES_INDIA.ToString()));
                ddlDepartment.Items.Remove(ddlDepartment.Items.FindByText(CommonConstants.Project_Mentee2010_DEPARTMENT.ToString()));

                raveHRCollection.Clear();
            }

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "FillDropDowns", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// Based on the Department selected, fill the Role 
    /// </summary>
    private void FillRoleDropdownAsPerDepartment(int deptId)
    {
        if (deptId == 0 && (ddlDepartment.SelectedItem.Text == CommonConstants.SELECT))
        {
            GetEmployeeDesignations(0);
        }
        else
        {
            GetEmployeeDesignations(deptId);
        }

        FillEmployeeTypeDropDown();
    }

    /// <summary>
    /// get designation by department id
    /// </summary>
    private void GetEmployeeDesignations(int categoryID)
    {
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();
        Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();

        raveHrColl = employeeBL.GetEmployeesDesignations(categoryID);

        ddlDesignation.Items.Clear();
        ddlDesignation.DataSource = raveHrColl;
        ddlDesignation.DataTextField = Common.CommonConstants.DDL_DataTextField;
        ddlDesignation.DataValueField = Common.CommonConstants.DDL_DataValueField;
        ddlDesignation.DataBind();
        ddlDesignation.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

    }


    /// <summary>
    /// Show the Location of Candidate.
    /// </summary>
    private void FillResourceBussinesUnitDropDown()
    {
        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        // Initialise Business layer object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        try
        {
            raveHRCollection = master.FillDropDownsBL((int)Common.EnumsConstants.Category.ResourceBusinessunit);
            //Check Collection is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSOurce to Collection
                ddlResourceBussinesUnit.DataSource = raveHRCollection;

                //Assign DataText Filed to DropDown
                ddlResourceBussinesUnit.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign DataValue Field to Dropdown
                ddlResourceBussinesUnit.DataValueField = CommonConstants.DDL_DataValueField;

                //Bind Dropdown
                ddlResourceBussinesUnit.DataBind();

                //Insert Select as a item for dropdown
                ddlResourceBussinesUnit.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                raveHRCollection.Clear();
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FILL_RESOURCEBUSSINESSUNIT_DROPDOWN, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }


    private void FillResourceBussinesUnitAsperDept(int Deptid)
    {
        try
        {
            recruitment = Rave.HR.BusinessLayer.Recruitment.Recruitment.ResourceBussinesUnitAsperDept(Deptid);

            ddlResourceBussinesUnit.ClearSelection();
            ddlResourceBussinesUnit.Items.FindByText(recruitment.ResourceBussinessUnitName.ToString().Trim()).Selected = true;

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FILL_MRF_DETAILS, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }


    protected void ddlBand_SelectedIndexChanged(object sender, EventArgs e)
    {

        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
        BusinessEntities.RaveHRCollection newRaveHRCollection = new BusinessEntities.RaveHRCollection();
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        try
        {
            if (ddlBand.SelectedItem.Value != CommonConstants.SELECT)
            {
                raveHRCollection = master.FillDropDownsBL(Convert.ToInt32(EnumsConstants.Category.EmployeeType));
                foreach (KeyValue<string> keyValue in raveHRCollection)
                {
                    if (ddlBand.SelectedItem.ToString() != CommonConstants.EmployeeBandC.ToString())
                    {
                        if (keyValue.KeyName == CommonConstants.EmployeePermanentID.ToString())
                        {
                            newRaveHRCollection.Add(keyValue);
                        }
                    }
                    else
                    {
                        if (keyValue.KeyName != CommonConstants.EmployeePermanentID.ToString())
                        {
                            newRaveHRCollection.Add(keyValue);
                        }
                    }

                }

                if (newRaveHRCollection != null)
                {
                    // Assign the data source to dropdown
                    ddlEmpType.DataSource = newRaveHRCollection;

                    ddlEmpType.DataTextField = CommonConstants.DDL_DataTextField;
                    ddlEmpType.DataValueField = CommonConstants.DDL_DataValueField;

                    // Bind the data to dropdown
                    ddlEmpType.DataBind();

                    // Default value of dropdown is "Select"
                    ddlEmpType.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
                }

            }
            else
            { FillEmployeeTypeDropDown(); }
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FILL_EMPLOYEE_TYPE_DROP_DOWN, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }

    }

    protected void btnEditMRFCode_Click(object sender, EventArgs e)
    {
        ddlMRFCode.Enabled = true;
    }

    // Issue Id : 34230 STRAT CONCURRENCY HANDLED Mahendra
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
    // Issue Id : 34230 END CONCURRENCY HANDLED Mahendra
    //Rajan Kumar : Issue 39508: 05/02/2014 : Starts                        			 
    //Desc : Traninig for new joining employee. (Training Gaps).  
    private string TrainingSubjectMode()
    {
        string traningSubject = (string)ViewState[TRANING_SUBJECT];
        string mode = string.Empty;
        if (chkTrainingRequired.Checked)
        {
            if (!string.IsNullOrEmpty(traningSubject))
            {
                if (!string.IsNullOrEmpty(txtTrainingSubject.Text.Trim()))
                {
                    if (!traningSubject.Equals(txtTrainingSubject.Text))
                    {
                        mode = TraningSubjectChanged.Edit.ToString();
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(txtTrainingSubject.Text.Trim()))
                    mode = TraningSubjectChanged.Add.ToString();
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(traningSubject))
            {
                if (string.IsNullOrEmpty(txtTrainingSubject.Text.Trim()))
                {
                    mode = TraningSubjectChanged.Edit.ToString();
                }
            }
        }
        return mode;
    }
    #endregion
}
