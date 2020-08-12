//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           MrfRaiseNext.aspx.cs    
//  Author:         Sunil.Mishra
//  Date written:   27/8/2009/ 12:01:00 PM
//  Description:    This Page will be the entry page for Raising the MRF
//                  same page will use in View MRF & Edit MRF mode
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  27/8/2009/ 12:01:00 PM  Sunil.Mishra    n/a     Created    
//
//-----------------------------------------------------------------------------
using System;
using Common;
using System.Text;
using System.Data;
using Common.AuthorizationManager;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Drawing;
using System.Collections;

public partial class MrfRaiseNext : BaseClass
{
    #region Private Field Members
    //Declaring COllection class object
    BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

    //Declaring COllection class object
    Rave.HR.BusinessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.BusinessLayer.MRF.MRFDetail();

    Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

    //Integer Variable
    int MRFID;

    //Declare DataTable.
    DataTable dtResourceGrid = new DataTable();

    //Declare Split dash Variable
    char[] SPILITER_DASH = { '-' };

    //Declare Subject
    string subject;

    //Declare string varibale 
    string body;

    //Declare Class Name
    const string CLASS_NAME_RAISEMRF_NEXT = "MrfRaiseNext";

    //Declare AuthorizationManager Class Object
    AuthorizationManager objAuMan = new AuthorizationManager();

    //Declare Rave Domain
    //Googleconfigurable
    //const string RAVEDOMAIN = "@rave-tech.com";

    string message = "";

    char[] SPILITER_DOT = { '.' };

    //Aarohi : Issue 28735(CR) : 13/01/2012 : Start
    public const string DEPARTMENT = "Department";
    public const string PROJECT = "Project";
    //Aarohi : Issue 28735(CR) : 13/01/2012 : End

    #endregion

    #region Protected Events

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
    /// ArrayList for Roles For User
    /// </summary>
    ArrayList arrRolesForUser = new ArrayList();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Javascript Function Call

        btnRaiseMRF.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return ButtonClickValidate();");

        //Poonam : Issue : Disable Button : Starts
        btnRaiseMRF.OnClientClick = "if(ButtonClickValidate()){" + ClientScript.GetPostBackEventReference(btnRaiseMRF, null) + "}";
        //Poonam : Issue : Disable Button : Ends

       // btnRaiseMRF.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return checkExperience1();");
       // txtExperience1.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return checkExperience1();");

        txtMusthaveskill.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return MultiLineTextBoxCheckResponsibility('" + txtMusthaveskill.ClientID + "','" + txtMusthaveskill.MaxLength + "','" + imgMusthaveskill.ClientID + "');");
        imgMusthaveskill.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanMusthaveskill.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgMusthaveskill.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanMusthaveskill.ClientID + "');");

        txtGoodtohaveskill.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return MultiLineTextBoxCheckResponsibility('" + txtGoodtohaveskill.ClientID + "','" + txtGoodtohaveskill.MaxLength + "','" + imgGoodtohaveskill.ClientID + "');");
        imgGoodtohaveskill.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanGoodtohaveskill.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgGoodtohaveskill.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanGoodtohaveskill.ClientID + "');");

        txtTools.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return MultiLineTextBoxCheckResponsibility('" + txtTools.ClientID + "','" + txtTools.MaxLength + "','" + imgTools.ClientID + "');");
        imgTools.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanTools.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgTools.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanTools.ClientID + "');");

        ddlSkillCategory.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzSkillCategory.ClientID + "','','');");

        txtExperience.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtExperience.ClientID + "','" + imgExperience.ClientID + "','" + "Decimal" + "');");
        imgExperience.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanExperience.ClientID + "','" + Common.CommonConstants.MSG_DECIMAL + "');");
        imgExperience.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanExperience.ClientID + "');");

        txtExperience1.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return checkExperience1();");
        //txtExperience1.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtExperience1.ClientID + "','" + imgExperience.ClientID + "','" + "Decimal" + "');");
        imgExperience.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanExperience.ClientID + "','" + Common.CommonConstants.MSG_DECIMAL + "');");
        imgExperience.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanExperience.ClientID + "');");


        txtHeightQualification.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtHeightQualification.ClientID + "','" + imgHeightQualification.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_WITDSPACE + "');");
        imgHeightQualification.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanHeightQualification.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgHeightQualification.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanHeightQualification.ClientID + "');");

        txtSoftSkill.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return MultiLineTextBoxCheckResponsibility('" + txtSoftSkill.ClientID + "','" + txtSoftSkill.MaxLength + "','" + imgSoftSkill.ClientID + "');");
        imgSoftSkill.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanSoftSkill.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgSoftSkill.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanSoftSkill.ClientID + "');");


        //txtUtilijation.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return checkUtilization();");

        txtUtilijation.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return checkUtilization();");
        imgUtilization.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanUtilization.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgUtilization.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanUtilization.ClientID + "');");

        //txtBilling.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtBilling.ClientID + "','" + imgBilling.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
        txtBilling.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return checkBilling();");

        imgBilling.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanBilling.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgBilling.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanBilling.ClientID + "');");

        
        txtCTC.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtCTC.ClientID + "','" + imgCTC.ClientID + "','" + "Decimal" + "');");
        imgCTC.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanCTC.ClientID + "','" + Common.CommonConstants.MSG_DECIMAL + "');");
        imgCTC.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanCTC.ClientID + "');");
        
        

        //txtCTC1.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return checkCTC1();");
        txtCTC1.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtCTC1.ClientID + "','" + imgCTC.ClientID + "','" + "Decimal" + "');");
        imgCTC.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanCTC.ClientID + "','" + Common.CommonConstants.MSG_DECIMAL + "');");
        imgCTC.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanCTC.ClientID + "');");
        
        txtRemarks.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return MultiLineTextBoxCheckResponsibility('" + txtRemarks.ClientID + "','" + txtRemarks.MaxLength + "','" + imgRemarks.ClientID + "');");
        imgRemarks.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanRemarks.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgRemarks.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanRemarks.ClientID + "');");

        //txtResponsiblePerson.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtResponsiblePerson.ClientID + "','" + imgResponsiblePerson.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_WITDSPACE + "');");
        //imgResponsiblePerson.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanResponsiblePerson.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        //imgResponsiblePerson.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanResponsiblePerson.ClientID + "');");

        txtResourceResponsibility.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return MultiLineTextBoxCheckResponsibility('" + txtResourceResponsibility.ClientID + "','" + txtResourceResponsibility.MaxLength + "','" + imgResourceResponsibility.ClientID + "');");
        imgResourceResponsibility.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanResourceResponsibility.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgResourceResponsibility.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanResourceResponsibility.ClientID + "');");


        //Ishwar Patil 20042015 Start
        imgSkillsSearch.Attributes.Add("onclick", "return popUpSkillSearch();");
        //Ishwar Patil 20042015 End

        imgResponsiblePersonSearch.Attributes.Add("onclick", "return popUpEmployeeSearch();");

        //imgPurpose.Attributes.Add("onclick", "return popUpEmployeeName();");

        #endregion

        //lblPurpose.Text = string.Empty;

        if (!ValidateURL())
        {
            Response.Redirect(CommonConstants.INVALIDURL);
        }
        else
        {
            

            try
            {
                if (!IsPostBack)
                {
                    AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
                    arrRolesForUser = objRaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());

                    if (arrRolesForUser.Count != 0)
                    {
                        if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM))
                        {
                            ROLE = AuthorizationManagerConstants.ROLERPM;
                        }
                    }     

                    //Default Focus
                    txtMusthaveskill.Focus();

                    //Fill Dropdown Method
                    FillDropDown();
                    this.GetPurposeDetails();

                    if (Session[SessionNames.MRFPREVIOUSVALUE] != null)
                    {
                        if (Session[SessionNames.MRFCOPY] != null)
                        {
                            BusinessEntities.MRFDetail mrfDetailcopyObj = new BusinessEntities.MRFDetail();
                            mrfDetailcopyObj = (BusinessEntities.MRFDetail)Session[SessionNames.MRFCOPY];

                            SetControlValues(mrfDetailcopyObj);
                            if (mrfDetailcopyObj.DepartmentName != Common.MasterEnum.Departments.Projects.ToString())
                            {
                                rowDomainAndSkill.Visible = true;
                                MandatoryDomain.Visible = false;
                                rowUtilizationAndBilling.Visible = true;
                            }
                            else
                            {
                                rowUtilizationAndBilling.Visible = false;
                            }
                        }
                        else
                        {
                            BusinessEntities.MRFDetail mrfDetailobj = new BusinessEntities.MRFDetail();
                            mrfDetailobj = (BusinessEntities.MRFDetail)Session[SessionNames.MRFPREVIOUSVALUE];

                            SetControlValues(mrfDetailobj);
                            //Check if Department selected was not "Projects" than hide the details of 
                            //"Domain" and "Skill Category".
                            if (mrfDetailobj.DepartmentName != Common.MasterEnum.Departments.Projects.ToString())
                            {
                                rowDomainAndSkill.Visible = true;
                                MandatoryDomain.Visible = false;
                                rowUtilizationAndBilling.Visible = true;
                            }
                            else
                            {
                                rowUtilizationAndBilling.Visible = false;
                            }
                        }
                    }                                 
                }
                txtResponsiblePerson.Text = hidResponsiblePersonName.Value;
                //txtResponsiblePerson.Text = mRFDetail.ReportingToEmployee;
                //Aarohi : Issue 28735(CR) : 26/12/2011 : Start
                //Coded to display Project/Dept Name on the second page of raise Mrf
                if (Session[SessionNames.PROJECT_NAME] == null || (Session[SessionNames.PROJECT_NAME].ToString()) == string.Empty)
                {
                    if (Session[SessionNames.DEPARTMENT_NAME] != null)
                    {
                        lblProjectName.Text = Session[SessionNames.DEPARTMENT_NAME].ToString();
                        lblTitle.Text = DEPARTMENT;
                    }
                }
                else
                {
                    lblProjectName.Text = Session[SessionNames.PROJECT_NAME].ToString();
                    lblTitle.Text = PROJECT;
                }
                lblRole.Text = Session[SessionNames.ROLE].ToString();
                //Aarohi : Issue 28735(CR) : 26/12/2011 : End
            }
            catch (RaveHRException ex)
            {
                LogErrorMessage(ex);
            }
            catch (Exception ex)
            {
                RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, 
                    CLASS_NAME_RAISEMRF_NEXT, "Page_Load", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
                LogErrorMessage(objEx);
            }
        }
    }

    /// <summary>
    /// Previous Button CLick
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            BusinessEntities.MRFDetail mRfDetail = new BusinessEntities.MRFDetail();
            mRfDetail = GetControlValues();

            Server.Transfer("MrfRaisePrevious.aspx?" + URLHelper.SecureParameters("mode", Common.MasterEnum.PageMode.PREVIOUS.ToString()) + "&" + URLHelper.CreateSignature(Common.MasterEnum.PageMode.PREVIOUS.ToString()));
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (System.Threading.ThreadAbortException IException)
        {

        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_NEXT, "btnPrevious_Click", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Raise MRF CLick 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRaiseMRF_Click(object sender, EventArgs e)
    {

        btnRaiseMRF.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return ButtonClickValidate();");
        
        BusinessEntities.MRFDetail MRFDetailobject = new BusinessEntities.MRFDetail();

        try
        {
            MRFDetailobject = GetControlValues();

            if (Session[SessionNames.MRFGRIDFILL] != null)
            {
                dtResourceGrid = (DataTable)Session[SessionNames.MRFGRIDFILL];

                raveHRCollection = mRFDetail.RaiseMRFBL(MRFDetailobject, dtResourceGrid);
            }
            else
            {
                raveHRCollection = mRFDetail.RaiseMRFBL(MRFDetailobject, null);
            }

            Response.Redirect(CommonConstants.Page_MrfRaisePrevious, false);

            if (Session[SessionNames.MRFPREVIOUSVALUE] != null)
            {
                Session.Remove(SessionNames.MRFPREVIOUSVALUE);
            }
            if (Session[SessionNames.MRFGRIDFILL] != null)
            {
                Session.Remove(SessionNames.MRFGRIDFILL);
            }
            if (Session[SessionNames.MRFCOPY] != null)
            {
                Session.Remove(SessionNames.MRFCOPY);
            }

            //if (txtResponsiblePerson.Text == null || txtResponsiblePerson.Text=="")
            //{
            //    lblMessage.Text = "Please fill Responsible Person";
            //    lblMessage.ForeColor = Color.Red;
            //    txtResponsiblePerson.BorderColor = Color.Red;
            //    txtResponsiblePerson.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
            //    txtResponsiblePerson.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(2);
            //    return;
            //}

            //if (ddlSkillCategory.SelectedItem.Text == "SELECT")
            //{
            //    lblMessage.Text = "Please select Skill Category";
            //    lblMessage.ForeColor = Color.Red;
            //    ddlSkillCategory.BorderColor = Color.Red;
            //    ddlSkillCategory.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
            //    ddlSkillCategory.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(2);
            //    return;
            //}

            resetControl();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                CLASS_NAME_RAISEMRF_NEXT, "btnRaiseMRF_Click", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Cncel Button CLick
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session[SessionNames.MRFPREVIOUSVALUE] != null)
            {
                Session.Remove(SessionNames.MRFPREVIOUSVALUE);
            }
            if (Session[SessionNames.MRFGRIDFILL] != null)
            {
                Session.Remove(SessionNames.MRFGRIDFILL);
            }
            if (Session[SessionNames.MRFCOPY] != null)
            {
                Session.Remove(SessionNames.MRFCOPY);
            }
            Response.Redirect("Home.aspx", false);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_NEXT, "btnCancel_Click", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlPurpose control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlPurpose_SelectedIndexChanged(object sender, EventArgs e)
    {
        //txtPurpose.Text = string.Empty;
        //if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.HiringForNewRole))
        //{
        //    txtPurpose.Visible = true;
        //    lblPurpose.Text = "New Role";
        //    imgPurpose.Visible = false;
        //    txtPurpose.ReadOnly = false;
        //    lblmandatorymarkPurpose.Visible = true;
        //}
        //else if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.HiringForProject))
        //{
        //    txtPurpose.Visible = true;
        //    lblPurpose.Text = "Project Name";
        //    imgPurpose.Visible = false;
        //    txtPurpose.ReadOnly = false;
        //    lblmandatorymarkPurpose.Visible = true;
        //}
        //else if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.Replacement))
        //{
        //    txtPurpose.Visible = true;
        //    txtPurpose.ReadOnly = true;
        //    lblPurpose.Text = "Employee Name";
        //    imgPurpose.Visible = true;
        //    lblmandatorymarkPurpose.Visible = true;
        //}
        //else if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.HiringForFutureBusiness) || ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.MarketResearchfeasibility))
        //{
        //    txtPurpose.Visible = true;
        //    lblPurpose.Text = "Comment";
        //    imgPurpose.Visible = false;
        //    txtPurpose.ReadOnly = false;
        //    lblmandatorymarkPurpose.Visible = false;
        //}
        //else
        //{
        //    lblPurpose.Text = string.Empty;
        //    txtPurpose.Visible = false;
        //    imgPurpose.Visible = false;
        //    txtPurpose.ReadOnly = false;
        //    lblmandatorymarkPurpose.Visible = false;
        //}
    }

    #endregion

    #region Private Member Functions
    /// <summary>
    /// Function will use to set the control values
    /// </summary>
    /// <param name="mrfDetailobj"></param>
    private void SetControlValues(BusinessEntities.MRFDetail mrfDetailobj)
    {
        lblMessage.Text = "";

        try
        {
            txtDomain.Text = mrfDetailobj.Domain;
            hidDepartmentName.Value = mrfDetailobj.DepartmentName;
            txtMusthaveskill.Text = mrfDetailobj.MustToHaveSkills;
            txtGoodtohaveskill.Text = mrfDetailobj.GoodToHaveSkills;
            txtTools.Text = mrfDetailobj.Tools;
            ddlSkillCategory.SelectedValue = Convert.ToString(mrfDetailobj.SkillCategoryId);
            txtDomain.Text = mrfDetailobj.Domain;
            if (mrfDetailobj.DepartmentName == MasterEnum.Departments.Projects.ToString() && txtDomain.Text == "")
            {
                lblMessage.Text = "Please provide Domain details";
                lblMessage.ForeColor = Color.Red;
                txtDomain.BorderColor = Color.Red;
                txtDomain.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
                txtDomain.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(2);
            }
            if (mrfDetailobj.ExperienceString != null)
            {
                string[] Exparr = Convert.ToString(mrfDetailobj.ExperienceString).Split(SPILITER_DASH);

                txtExperience.Text = Exparr[0].ToString();
                txtExperience1.Text = Exparr[1].ToString();
            }
            txtHeightQualification.Text = mrfDetailobj.Qualification;
            txtSoftSkill.Text = mrfDetailobj.SoftSkills;
            txtUtilijation.Text = Convert.ToString(mrfDetailobj.Utilization);
            txtBilling.Text = Convert.ToString(mrfDetailobj.Billing);

            //Venkatesh : XXX 26-Feb-2014 : Start 
            //Desc : Mrf CTC error handling when it Null
            if (mrfDetailobj.MRFCTCString != null && mrfDetailobj.MRFCTCString != "")
            {
            //Venkatesh : XXX 26-Feb-2014 : End
                string[] CTCarr = Convert.ToString(mrfDetailobj.MRFCTCString).Split(SPILITER_DASH);

                txtCTC.Text = CTCarr[0].ToString();
                txtCTC1.Text = CTCarr[1].ToString();
            }
            else
            {
                txtCTC.Text = "0";
                txtCTC1.Text = "0";
            }

            txtRemarks.Text = mrfDetailobj.Remarks;
            txtResponsiblePerson.Text = mrfDetailobj.EmployeeName;
            hidResponsiblePerson.Value = mrfDetailobj.ReportingToId;
            txtResourceResponsibility.Text = mrfDetailobj.ResourceResponsibility;


            //if (ddlSkillCategory.SelectedItem.Text == "SELECT")
            //{
            //    lblMessage.Text = "Please select Skill Category";
            //    lblMessage.ForeColor = Color.Red;
            //    ddlSkillCategory.BorderColor = Color.Red;
            //    ddlSkillCategory.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
            //    ddlSkillCategory.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(2);
            //    // return;
            //}

            //if (txtResponsiblePerson.Text == null ||txtResponsiblePerson.Text == "Mrf Admin")
            //{
            //    lblMessage.Text = "Please provide Responsible Person";
            //    lblMessage.ForeColor = Color.Red;
            //    txtResponsiblePerson.BorderColor = Color.Red;
            //    txtResponsiblePerson.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
            //    txtResponsiblePerson.BorderWidth = System.Web.UI.WebControls.Unit.Pixel(2);
            //   // return;
            //}

           

            
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, 
                CLASS_NAME_RAISEMRF_NEXT, "SetControlValues", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }


    }

    /// <summary>
    /// Function will GetControlValues
    /// </summary>
    /// <returns></returns>
    private BusinessEntities.MRFDetail GetControlValues()
    {
        BusinessEntities.MRFDetail mrfDetailobj = new BusinessEntities.MRFDetail();
        AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
        try
        {
            string UserMailId = objRaveHRAuthorizationManager.getLoggedInUserEmailId();

            if (Session[SessionNames.MRFPREVIOUSVALUE] != null)
            {
                mrfDetailobj = (BusinessEntities.MRFDetail)Session[SessionNames.MRFPREVIOUSVALUE];

                mrfDetailobj.MustToHaveSkills = txtMusthaveskill.Text.Trim().ToString();
                mrfDetailobj.GoodToHaveSkills = txtGoodtohaveskill.Text.Trim().ToString();
                mrfDetailobj.Tools = txtTools.Text.Trim().ToString();
                if (ddlSkillCategory.SelectedItem.Text != Common.CommonConstants.SELECT)
                {
                    mrfDetailobj.SkillCategoryId = Convert.ToInt32(ddlSkillCategory.SelectedItem.Value);
                }
                else
                {
                    mrfDetailobj.SkillCategoryId = 0;
                }
                string Experience = txtExperience.Text.Trim().ToString() + "-" + txtExperience1.Text.Trim().ToString();
                if (Experience == "")
                {
                    mrfDetailobj.Experience = Convert.ToDecimal(0);
                }
                else
                {
                    mrfDetailobj.ExperienceString = Experience;
                }

                mrfDetailobj.Qualification = txtHeightQualification.Text.Trim().ToString();
                mrfDetailobj.SoftSkills = txtSoftSkill.Text.Trim().ToString();

                if (mrfDetailobj.ProjectId == 0 && mrfDetailobj.DepartmentName != MasterEnum.Departments.Projects.ToString())
                {
                    if (txtUtilijation.Text != "")
                    {
                        mrfDetailobj.Utilization = Convert.ToInt32(txtUtilijation.Text.Trim().ToString());
                    }
                    if (txtBilling.Text != "")
                    {
                        mrfDetailobj.Billing = Convert.ToInt32(txtBilling.Text.Trim().ToString());
                    }
                }
                string TargetCTC = txtCTC.Text.Trim().ToString() + "-" + txtCTC1.Text.Trim().ToString();

                if (TargetCTC == "")
                {
                    mrfDetailobj.MRFCTC = Convert.ToDecimal(0);
                }
                else
                {
                    mrfDetailobj.MRFCTCString = TargetCTC;
                }

                mrfDetailobj.Remarks = txtRemarks.Text.Trim().ToString();
                mrfDetailobj.ReportingToId = hidResponsiblePerson.Value.ToString();
                mrfDetailobj.ResourceResponsibility = txtResourceResponsibility.Text.Trim().ToString();
                mrfDetailobj.LoggedInUserEmail = UserMailId;
                mrfDetailobj.Domain = txtDomain.Text.Trim();

                if(mrfDetailobj.MRFPurposeId == Convert.ToInt32(MasterEnum.MRFPurpose.MarketResearchfeasibility))
                {
                    mrfDetailobj.MRfType=Convert.ToInt32(MasterEnum.MRFType.Shortlist);
                }
                else
                {
                    mrfDetailobj.MRfType=Convert.ToInt32(MasterEnum.MRFType.Shortlist_and_make_anoffer);
                }

                //Ishwar Patil 22/04/2015 Start
                mrfDetailobj.MandatorySkills = Convert.ToString(hidSkills.Value);
                //Ishwar Patil 22/04/2015 End

                Session[SessionNames.MRFPREVIOUSVALUE] = mrfDetailobj;
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, 
                CLASS_NAME_RAISEMRF_NEXT, "GetControlValues", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }

        return mrfDetailobj;
    }

    /// <summary>
    /// Fill DropDown Method
    /// </summary>
    private void FillDropDown()
    {
        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        try
        {
            BusinessEntities.MRFDetail mrfDetailobj = new BusinessEntities.MRFDetail();

            if (Session[SessionNames.MRFPREVIOUSVALUE] != null)
            {
                mrfDetailobj = (BusinessEntities.MRFDetail)Session[SessionNames.MRFPREVIOUSVALUE];
            }
            int testingDeparmentId = mrfDetailobj.DepartmentId;

            //Calling Business layer FillDropDown Method
            if (testingDeparmentId == (int)MasterEnum.MRFDepartment.Testing)
            {
                raveHRCollection = master.FillDropDownsBL(testingDeparmentId);
            }
            else
            {
                //Calling Business layer FillDropDown Method
                raveHRCollection = master.FillDropDownsBL(Convert.ToInt32(Common.EnumsConstants.Category.PrimarySkills));
            }

            //Check Collection is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSOurce to Collection
                ddlSkillCategory.DataSource = raveHRCollection;

                //Assign DataText Filed to DropDown
                ddlSkillCategory.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign DataValue Field to Dropdown
                ddlSkillCategory.DataValueField = CommonConstants.DDL_DataValueField;

                //Bind Dropdown
                ddlSkillCategory.DataBind();

                //Insert Select as a item for dropdown
                ddlSkillCategory.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

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
                CLASS_NAME_RAISEMRF_NEXT, "FillDropDown", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// Function will reset control
    /// </summary>
    private void resetControl()
    {
        try
        {
            txtDomain.Text = string.Empty;
            txtMusthaveskill.Text = string.Empty;
            txtGoodtohaveskill.Text = string.Empty;
            txtTools.Text = string.Empty;
            ddlSkillCategory.SelectedIndex = 0;
            txtDomain.Text = string.Empty;
            txtExperience.Text = string.Empty;
            txtExperience1.Text = string.Empty;
            txtHeightQualification.Text = string.Empty;
            txtSoftSkill.Text = string.Empty;
            txtUtilijation.Text = string.Empty;
            txtBilling.Text = string.Empty;
            txtCTC.Text = string.Empty;
            txtCTC1.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtResponsiblePerson.Text = string.Empty;
            txtResourceResponsibility.Text = string.Empty;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, 
                CLASS_NAME_RAISEMRF_NEXT, "FillDropDown", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Function will GetLink
    /// </summary>
    /// <returns></returns>
    private string GetLink()
    {
        try
        {
            string sComp = Utility.GetUrl() + CommonConstants.Page_MrfPendingAllocation;

            return sComp;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_NEXT, "FillDropDown", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Fill DropDown Method
    /// </summary>
    private void GetPurposeDetails()
    {
        ////Declaring Master Class Object
        //Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        //try
        //{
        //    //Calling Business layer FillDropDown Method
        //    raveHRCollection = master.FillDropDownsBL(Convert.ToInt32(Common.EnumsConstants.Category.MRFPurpose));

        //    //Check Collection is null or not
        //    if (raveHRCollection != null)
        //    {
        //        //Assign DataSOurce to Collection
        //        ddlPurpose.DataSource = raveHRCollection;

        //        //Assign DataText Filed to DropDown
        //        ddlPurpose.DataTextField = CommonConstants.DDL_DataTextField;

        //        //Assign DataValue Field to Dropdown
        //        ddlPurpose.DataValueField = CommonConstants.DDL_DataValueField;

        //        //Bind Dropdown
        //        ddlPurpose.DataBind();

        //        //Insert Select as a item for dropdown
        //        ddlPurpose.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

        //        raveHRCollection.Clear();
        //    }
        //}
        //catch (RaveHRException ex)
        //{
        //    throw ex;
        //}
        //catch (Exception ex)
        //{
        //    throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_NEXT, "FillDropDown", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        //}

    }



    #endregion

    
}
