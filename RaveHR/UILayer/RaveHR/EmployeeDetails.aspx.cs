using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Common;
using Common.AuthorizationManager;
using System.IO;
using System.Text;
using System.Data;
using System.Configuration;
using System.Runtime.Serialization.Formatters.Binary;
using Common.Constants;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using BusinessEntities;
using System.Linq;


/// <summary>
/// 
/// </summary>
public partial class EmployeeDetails : BaseClass
{
    #region Private Field Members

    BusinessEntities.Employee employee = new BusinessEntities.Employee();
    Common.AuthorizationManager.AuthorizationManager objAuth;
    ArrayList arrRolesForUser = new ArrayList();


    const string IMAGEURLPATH = "ImagePathURL";
    string UserRaveDomainId;
    string UserMailId;
    const string ReadOnly = "readonly";
    char[] SPILITER_SLASH = { '/' };
    //const string imagePath = @"~\Images";
    string imagePath = Utility.GetUrl() + ConfigurationSettings.AppSettings[IMAGEURLPATH];
    string imagePhysicalPath = ConfigurationSettings.AppSettings["ImagePhysicalPath"];
    //const string BackSlash = @"\";
    const string IMAGES = "Images";
    const string NoImage = "NoImage.jpg";

    DataTable dtEmployeeProjectCodtCodes;

    /// <summary>
    /// 
    /// </summary>
    const string INDEX = "index";

    const string CLASS_NAME = "Employee.aspx.cs";

    /// <summary>
    /// Rave Email domain
    /// </summary>
    /// 
    //Googleconfigurable
    //private const string RAVE_DOMAIN = "@rave-tech.com";
    string PageMode = string.Empty;
    private string EMERGENCYDETAILS = "EmergencyDetails";
    const string PAGETYPE = "pagetype";
    const string PAGETYPEEMPLOYEESUMMERY = "EMPLOYEESUMMERY";

    protected EmployeeMenuUC BubbleControl;
    private string SelectedIndex = "";

    #endregion

    #region Properties

    /// <summary>
    /// Property for EMPLOYEEPREVIOUSCOUNT
    /// </summary>
    public int EMPLOYEEPREVIOUSCOUNT
    {
        get
        {
            if (ViewState["EMPLOYEEPREVIOUSCOUNT"] != null)
            {
                return Convert.ToInt32(ViewState["EMPLOYEEPREVIOUSCOUNT"]);
            }
            else
            {
                return -1;
            }
        }
        set
        {
            ViewState["EMPLOYEEPREVIOUSCOUNT"] = value;
        }
    }

    /// <summary>
    /// Property for EMPLOYEENEXTCOUNT
    /// </summary>
    public int EMPLOYEENEXTCOUNT
    {
        get
        {
            if (ViewState["EMPLOYEENEXTCOUNT"] != null)
            {
                return Convert.ToInt32(ViewState["EMPLOYEENEXTCOUNT"]);
            }
            else
            {
                return -1;
            }
        }
        set
        {
            ViewState["EMPLOYEENEXTCOUNT"] = value;
        }
    }

    /// <summary>
    ///  Property for EMPLOYEECURRENTCOUNT
    /// </summary>
    public int EMPLOYEECURRENTCOUNT
    {
        get
        {
            if (ViewState["EMPLOYEECURRENTCOUNT"] != null)
            {
                return Convert.ToInt32(ViewState["EMPLOYEECURRENTCOUNT"]);
            }
            else
            {
                return -1;
            }
        }
        set
        {
            ViewState["EMPLOYEECURRENTCOUNT"] = value;
        }
    }

    /// <summary>
    /// Gets or sets the obj emergency details collection.
    /// </summary>
    /// <value>The obj emergency details collection.</value>
    private BusinessEntities.RaveHRCollection objEmergencyDetailsCollection
    {
        get
        {
            if (ViewState[EMERGENCYDETAILS] != null)
                return ((BusinessEntities.RaveHRCollection)ViewState[EMERGENCYDETAILS]);
            else
                return null;
        }
        set
        {
            ViewState[EMERGENCYDETAILS] = value;
        }
    }

    /// <summary>
    /// For saving the path of user control
    /// </summary>
    private string SavedControlVirtualPath
    {
        get
        {
            if (ViewState["saved"] == null ||
                (string)ViewState["saved"] == string.Empty)
                return null;
            return (string)ViewState["saved"];
        }
        set
        {
            ViewState["saved"] = value;
        }
    }
    // Ishwar - NISRMS - 20112014 Start
    string strUserIdentity = string.Empty;
    BusinessEntities.Employee Employee;
    Rave.HR.BusinessLayer.Employee.Employee employeeBL;
    // Ishwar - NISRMS - 20112014 End
    #endregion Properties

    #region Protected Methods

    /// <summary>
    /// Handles the Init event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Init(object sender, EventArgs e)
    {
        //googing google
        //txtEmailID.Attributes.Add(ReadOnly, ReadOnly);
        txtReportingTo.Attributes.Add(ReadOnly, ReadOnly);
        txtReportingFM.Attributes.Add(ReadOnly, ReadOnly);
        // Sanju:Issue Id 50201
        // onclick events are added on click of an edit button(Chrome issue: onclick is not getting disabled even if the img tag is disabled)

        //imgEmpEmailPopulate.Attributes.Add("onclick", "return popUpEmployeeEmailPopulate();");
        //imgReportingToPopulate.Attributes.Add("onclick", "return popUpEmployeeSearch();");
        //imgReportingFM.Attributes.Add("onclick", "return popUpFunctionalManagerSearch();");
    }

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!ValidateURL())
            {
                Response.Redirect(CommonConstants.INVALIDURL);

            }
            else
            {

                //Clearing the error label
                lblMessage.Text = string.Empty;
                lblConfirmMsg.Text = string.Empty;

                //To solved issue id 21329
                //Code Added by Rahul P
                //Start
                ucDatePickerJoiningDate.TextBox.Attributes.Add("onKeyUp", "return GetTrueKeyCode(event);");
                ucDatePickerDOB.TextBox.Attributes.Add("onKeyUp", "return GetTrueKeyCode(event);");

                //End

                //Mohamed : Issue 39509/41062 : 06/03/2013 : Starts                        			  
                //Desc :  Adding new Columns date for Designation and Departement
                ucDatePickerDepartmentChangeDate.TextBox.Attributes.Add("OnTextChanged", "return fnChange();");
                ucDatePickerDepartmentChangeDate.TextBox.Attributes.Add("onKeyUp", "return GetTrueKeyCode(event);");
                ucDatePickerDesignationChangeDate.TextBox.Attributes.Add("onKeyUp", "return GetTrueKeyCode(event);");
                ucDatePickerConfirmedDate.TextBox.Attributes.Add("onKeyUp", "return GetTrueKeyCode(event);");
                //Mohamed : Issue 39509/41062 : 06/03/2013 : Ends

                //To solved issue id 21330
                //Code Added by Rahul P
                //Start
                btnUpdate.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return ButtonClickValidate();");
                //End
                txtFirstName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtFirstName.ClientID + "','" + imgFirstName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPACE + "');");
                imgFirstName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanFirstName.ClientID + "','" + Common.CommonConstants.MSG_ALPHABET + "');");
                imgFirstName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanFirstName.ClientID + "');");

                txtMiddleName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtMiddleName.ClientID + "','" + imgMiddleName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPACE + "');");
                imgMiddleName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanMiddleName.ClientID + "','" + Common.CommonConstants.MSG_ALPHABET + "');");
                imgMiddleName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanMiddleName.ClientID + "');");

                txtLastName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtLastName.ClientID + "','" + imgLastName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPACE + "');");
                imgLastName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanLastName.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
                imgLastName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanLastName.ClientID + "');");

                txtSpouseName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtSpouseName.ClientID + "','" + imgSpouseName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPACE + "');");
                imgSpouseName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanSpouseName.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
                imgSpouseName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanSpouseName.ClientID + "');");

                txtReportingFM.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtReportingFM.ClientID + "','" + imgcReportingFM.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_WITDSPACE + "');");
                imgcReportingFM.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanzReportingFM.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
                imgcReportingFM.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanzReportingFM.ClientID + "');");

                Rave.HR.BusinessLayer.MRF.MRFRoles mrfRoles = new Rave.HR.BusinessLayer.MRF.MRFRoles();
                // Get logged-in user's email id
                AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
                UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();

                //UserMailId = UserRaveDomainId.Replace("co.in", "com");
                //Googleconfigurable
                string domainName = string.Empty;
                //if (UserRaveDomainId.ToLower().Contains("@rave-tech.co.in"))
                //{
                //    UserRaveDomainId = UserRaveDomainId.Replace("@rave-tech.co.in", "");
                //    //strUserEmail = strUserName + "@" + AuthorizationManagerConstants.RAVEDOMAINEMAIL;
                //    if (Session["WindowsUsername"] == null)
                //    {


                //        UserMailId = objRaveHRAuthorizationManager.GetWindowsUsernameAsPerNorthgate(UserRaveDomainId, out domainName);
                //    }
                //    else
                //    {
                //        UserMailId = Session["WindowsUsername"].ToString().Trim();
                //    }
                //    UserMailId = UserMailId + "@" + AuthorizationManagerConstants.RAVEDOMAINEMAIL;

                //}
                //else
                //{
                //    UserRaveDomainId = UserRaveDomainId.ToLower().Replace("@" + AuthorizationManagerConstants.NORTHGATEDOMAIN, "");
                //    //strUserEmail = strUserName + "@" + AuthorizationManagerConstants.NORTHGATEDOMAINEMAIL;
                //    if (Session["WindowsUsername"] == null)
                //    {
                //        UserMailId = objRaveHRAuthorizationManager.GetWindowsUsernameAsPerNorthgate(UserRaveDomainId, out domainName);
                //    }
                //    else
                //    {
                //        UserMailId = Session["WindowsUsername"].ToString().Trim();
                //    }
                //    UserMailId = UserMailId + "@" + AuthorizationManagerConstants.NORTHGATEDOMAINEMAIL;
                //}

                if (Session["WindowsUsername"] == null || Session["domainName"] == null)
                {
                    UserMailId = objRaveHRAuthorizationManager.GetWindowsUsernameAsPerNorthgate(UserRaveDomainId, out domainName);
                    UserMailId = UserMailId.ToLower().Trim() + "@" + domainName.ToLower().Trim();
                }
                else
                {
                    UserMailId = Session["WindowsUsername"].ToString().ToLower().Trim() + "@" + Session["domainName"].ToString().ToLower().Trim();
                }

                //AuthorizationManager objAuthorizationManager = new AuthorizationManager();
                //UserMailId = objAuthorizationManager.GetDomainUsers(UserRaveDomainId);


                AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();

                arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());

                if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM))
                {
                    //btnRelease.Visible = true;
                    ddlResourceBussinesUnit.Enabled = true;
                    trResourceBusinessUnit.Attributes.Remove("style");
                }
                else
                {
                    ddlResourceBussinesUnit.Enabled = false;
                    trResourceBusinessUnit.Attributes.Add("style", "display:none");
                }
                //Venkatesh : Issue 57883 : 29/04/2016 : Starts
                //Desc :  Department and designation change date should show to manager in readonly
                if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR) || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM) || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEPM))
                {
                //Venkatesh : Issue 57883 : 29/04/2016 : End
                    btnUpdate.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return ButtonClickValidate(true);");
                    lblMandatory_DOB.Visible = false;
                    lblMandatory_Fresher.Visible = false;
                    lblMandatory_Gender.Visible = false;
                    lblMandatory_BloodGroup.Visible = false;
                    lblMandatory_Marital.Visible = false;
                }

                else
                {
                    btnUpdate.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return ButtonClickValidate(false);");
                    lblMandatory_DOB.Visible = true;
                    lblMandatory_Fresher.Visible = true;
                    lblMandatory_Gender.Visible = true;
                    lblMandatory_BloodGroup.Visible = true;
                    lblMandatory_Marital.Visible = true;

                    //Mohamed : Issue 39509/41062 : 06/03/2013 : Starts
                    //Desc :  Adding new Columns date for Probation
                    Page.ClientScript.RegisterStartupScript(GetType(), "Valid", "ControlAsPerUser();", true);
                    //Mohamed : Issue 39509/41062 : 06/03/2013 : Ends
                }




                if (!IsPostBack)
                {
                    Session[SessionNames.PAGEMODE] = Common.MasterEnum.PageModeEnum.View;

                    this.PopulateDropdownControls();

                    //IF EMPId is not null
                    if (Request.QueryString[CommonConstants.EMP_ID] != null)
                    {
                        Session[SessionNames.EMPLOYEEDETAILS] = null;
                        employee.EMPId = Convert.ToInt32(DecryptQueryString(QueryStringConstants.EMPID).ToString());
                        Session[SessionNames.EMPLOYEEDETAILS] = employee;
                        //btnRelease.Attributes["onclick"] = "popUpEmployeeReleasePopulate('" + employee.EMPId + "')";
                        Hidden_EmployeeID.Value = Convert.ToString(employee.EMPId);
                        //ViewState["EmployeeProjectCodeCode"] = dtEmployeeProjectCodtCodes;
                    }

                    //To solved issue id 19221
                    //Code Added by Rahul P
                    //Start
                    if (Request.QueryString["index"] != null)
                    {
                        //Session[SessionNames.EMPLOYEEDETAILS] = null;
                        SelectedIndex = Convert.ToString(DecryptQueryString("index").ToString());
                        Session["SelectedRow"] = SelectedIndex.ToString();
                    }
                    //End
                    //else
                    //{
                    //    btnRelease.Visible = false;
                    //}
                    if (Request.QueryString[QueryStringConstants.FLAG] != null)
                    {
                        string page = DecryptQueryString(QueryStringConstants.FLAG).ToString();
                        if (page == "_detailspage")
                        {
                            Session[SessionNames.EMPLOYEEDETAILS] = null;
                        }
                    }
                    this.PopulateControls();

                    //Declare a session paramete to hold the previous emp id
                    Session[SessionNames.PREVIOUS_EMP] = employee.EMPId;
                    Hidden_EmployeeID.Value = Convert.ToString(employee.EMPId);
                    //ViewState["EmployeeProjectCodeCode"] = dtEmployeeProjectCodtCodes;
                }

                SavedControlVirtualPath = "~/EmployeeMenuUC.ascx";
                ReloadControl();

                if (Session[SessionNames.PAGEMODE] != null)
                {
                    PageMode = Session[SessionNames.PAGEMODE].ToString();

                    if (PageMode == Common.MasterEnum.PageModeEnum.View.ToString())
                    {
                        this.EnableControl();
                        btnUpdate.Visible = false;
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "Page_Load", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the Click event of the btnEdit control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            Session[SessionNames.PAGEMODE] = Common.MasterEnum.PageModeEnum.Edit;
            btnUpdate.Visible = true;
            btnCancel.Visible = true;
            btnEdit.Visible = false;
            this.EnableDisableControls();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnEdit_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the Click event of the btnUpdate control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            //if (ValidateControls())// && ValidateCompleteCostCodeGrid(gvCostCode)==true)
            //{

            Rave.HR.BusinessLayer.Employee.EmergencyContact objEmergencyContactBL = new Rave.HR.BusinessLayer.Employee.EmergencyContact();
            BusinessEntities.RaveHRCollection objSaveEmergencyDetailsCollection = new BusinessEntities.RaveHRCollection();
            Boolean IsEmployeeDetailsModified = false;

            employee = (BusinessEntities.Employee)Session[Common.SessionNames.EMPLOYEEDETAILS];
            BusinessEntities.Employee prevEmployeeDetails = new BusinessEntities.Employee();
            prevEmployeeDetails = DeepCopy<BusinessEntities.Employee>(employee);
            prevEmployeeDetails.PrefixName = ddlPrefix.Items.FindByValue(prevEmployeeDetails.Prefix.ToString()).Text.ToString();
            prevEmployeeDetails.EmployeeType = ddlEmployeeType.Items.FindByValue(prevEmployeeDetails.Type.ToString()).Text.ToString();
            prevEmployeeDetails.BandName = ddlBand.Items.FindByValue(prevEmployeeDetails.Band.ToString()).Text.ToString();

            if (employee != null)
            {
                Rave.HR.BusinessLayer.Employee.Employee addEmployeeBAL = new Rave.HR.BusinessLayer.Employee.Employee();
                employee.Prefix = Convert.ToInt32(ddlPrefix.SelectedItem.Value);
                employee.FirstName = txtFirstName.Text.Trim();
                employee.LastName = txtLastName.Text.Trim();
                employee.EMPCode = txtEmployeeCode.Text.Trim();
                employee.EmailId = txtEmailID.Text.Trim();
                employee.GroupId = Convert.ToInt32(ddlDepartment.SelectedItem.Value);
                employee.Department = Convert.ToString(ddlDepartment.SelectedItem);
                employee.Band = Convert.ToInt32(ddlBand.SelectedItem.Value);
                employee.BandName = Convert.ToString(ddlBand.SelectedItem);
                employee.JoiningDate = Convert.ToDateTime(ucDatePickerJoiningDate.Text.Trim());
                employee.StatusId = (int)MasterEnum.EmployeeStatus.Active;
                employee.DesignationId = Convert.ToInt32(ddlDesignation.SelectedItem.Value);
                employee.Designation = Convert.ToString(ddlDesignation.SelectedItem);
                employee.ReportingToId = hidReportingTo.Value.ToString();
                employee.ReportingTo = HfReportingToName.Value;
                employee.LastModifiedByMailId = UserMailId;
                employee.LastModifiedDate = DateTime.Today;
                employee.Type = Convert.ToInt32(ddlEmployeeType.SelectedItem.Value);
                employee.EmployeeType = Convert.ToString(ddlEmployeeType.SelectedItem);
                employee.DOB = ucDatePickerDOB.Text.Trim() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(ucDatePickerDOB.Text);
                employee.Gender = ddlGender.SelectedItem.Value.Trim();
                employee.MaritalStatus = ddlMaritalStatus.SelectedItem.Value.Trim();
                employee.MiddleName = txtMiddleName.Text.Trim();
                employee.ReportingToFMId = Convert.ToInt32(hidReportingFM.Value.ToString());
                employee.ReportingToFM = HfReportingToFM.Value.ToString();
                employee.EmpLocation = ddlEmpLocation.SelectedItem.Text.Trim();
                employee.PrefixName = ddlPrefix.SelectedItem.Text.ToString();

                //Mohamed : Issue 39509/41062 : 07/03/2013 : Starts                        			  
                //Desc :  Adding new Columns date for Probation,Designation and Departement

                employee.DesignationChangeDate = ucDatePickerDesignationChangeDate.Text.Trim();
                employee.DepartementChangeDate = ucDatePickerDepartmentChangeDate.Text.Trim();
                employee.ConfirmedDate = ucDatePickerConfirmedDate.Text.Trim();
                if (!string.IsNullOrEmpty(ucDatePickerConfirmedDate.Text.Trim()))
                {
                    employee.ProbationFlag = true;
                }
                //Mohamed : Issue 50844 : 18/08/2014 : Starts                        			  
                //Desc :  Probation flag updating while updating employees data
                else if (employee.ProbationFlag && string.IsNullOrEmpty(ucDatePickerConfirmedDate.Text.Trim()))
                {
                    employee.ProbationFlag = true;
                }
                //Mohamed : Issue 50844 : 18/08/2014 : Ends
                else
                {
                    employee.ProbationFlag = false;
                }


                //Mohamed : Issue 39509/41062 : 07/03/2013 : Ends


                //Siddharth 9th June 2015 Start
                employee.BPSSVersion = ddlBPSSVersion.SelectedItem.Value.Trim();

                if (!string.IsNullOrEmpty(ucdatepickerCompletionDate.Text.Trim()))
                {
                    employee.BPSSCompletionDate = Convert.ToString(ucdatepickerCompletionDate.Text.Trim());// == string.Empty ? DateTime.MinValue : Convert.ToDateTime(ucdatepickerCompletionDate.Text);
                }
                else
                {
                    employee.BPSSCompletionDate = "";
                }
                //Siddharth 9th June 2015 End

                //Siddharth 25th August 2015 Start
                employee.ResourceBussinessUnit = Convert.ToInt32(ddlResourceBussinesUnit.SelectedValue);
                //Siddharth 25th August 2015 Start

                employee.BloodGroup = txtBloodGroup.Text.Trim();

                if (rblIsFresher.SelectedItem.Text == CommonConstants.YES)

                    employee.IsFresher = 1;
                else
                    employee.IsFresher = 2;

                if (ddlMaritalStatus.SelectedItem.Text == "Married")
                {
                    employee.SpouseName = txtSpouseName.Text.Trim();
                }
                else
                {
                    employee.SpouseName = string.Empty;
                }
                string filePath = string.Empty;
                filePath = Path.GetFileName(imgEmp.ImageUrl);
                employee.FileName = filePath;

                int dataCount = addEmployeeBAL.IsEmployeeDataExists(employee);

                if (dataCount == 4)
                {
                    lblMessage.Text = "Emailid " + employee.EmailId + " and Employee Code " + employee.EMPCode + " already exists in the database.";
                    return;
                }
                if (dataCount == 5)
                {
                    lblMessage.Text = "Emailid " + employee.EmailId + " already exists in the database.";
                    return;
                }
                if (dataCount == 6)
                {
                    lblMessage.Text = "Employee Code " + employee.EMPCode + " already exists in the database.";
                    return;
                }

                // 29501-Ambar-Start
                if ((((ddlPrefix.SelectedValue == "155") || (ddlPrefix.SelectedValue == "158")) && (ddlGender.SelectedValue == "M"))
                   || (((ddlPrefix.SelectedValue == "156") || (ddlPrefix.SelectedValue == "157")) && (ddlGender.SelectedValue == "F"))
                  || ((ddlPrefix.SelectedValue == "SELECT") && (ddlGender.SelectedValue == "SELECT"))
                  )
                {
                    ;//Do nothing
                }
                else
                {
                    lblMessage.Text = "Prefix and Gender values does not match.";
                    return;
                }
                // 29501-Ambar-End

                //check if employee details has modified if yes then maintain history of changes in database.
                if (employee.Prefix != prevEmployeeDetails.Prefix || employee.FirstName != prevEmployeeDetails.FirstName
                    || employee.LastName != prevEmployeeDetails.LastName || employee.EMPCode.Trim() != prevEmployeeDetails.EMPCode.Trim()
                    || employee.EmailId != prevEmployeeDetails.EmailId || employee.GroupId != prevEmployeeDetails.GroupId
                    || employee.Band != prevEmployeeDetails.Band || employee.EmpLocation != prevEmployeeDetails.EmpLocation
                    || employee.DesignationId != prevEmployeeDetails.DesignationId || employee.ReportingToId != prevEmployeeDetails.ReportingToId
                    || employee.Type != prevEmployeeDetails.Type || employee.ReportingToFMId != prevEmployeeDetails.ReportingToFMId
                    || employee.Gender != prevEmployeeDetails.Gender //24070-Ambar-Added condition
                  )
                {
                    IsEmployeeDetailsModified = true;
                }
                //Ishwar Patil 20112014 For NIS : Start
                //Desc: CostCode for NIS Employee.
                //if ((ConfigurationManager.AppSettings["NISReportsAccess"] != null) || (ConfigurationManager.AppSettings["RMOGroupName"] != null))
                //{
                //    string NewUserMailId = (UserMailId.Remove(UserMailId.Length - 3) + "co.in").ToUpper();

                //    if (((ConfigurationManager.AppSettings["NISReportsAccess"].ToString().ToUpper()).Contains(NewUserMailId)) || ((ConfigurationManager.AppSettings["RMOGroupName"].ToString().ToUpper()).Contains(NewUserMailId)))
                //    {


                //Siddharth 3 April 2015 Start
                //for (int i = 0; i < gvCostCode.Rows.Count; i++)
                //{
                //    if (i == 0)
                //        employee.CostCode = ((TextBox)gvCostCode.Rows[i].FindControl("txtCostCode")).Text;
                //    else
                //        employee.CostCode = employee.CostCode + " , " + ((TextBox)gvCostCode.Rows[i].FindControl("txtCostCode")).Text;
                //}
                //Siddharth 3 April 2015 End



                //employee.CostCode = txtCostCode.Text.Trim();
                //    }
                //    else
                //    {
                //        employee.CostCode = Session["CostCode"].ToString();
                //    }
                //}
                //Ishwar Patil 20112014 For NIS : End

                //Siddhesh Arekar 08/07/2015 Start
                if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
                {
                    employee.LoginRole = AuthorizationManagerConstants.ROLEHR;
                }
                else if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM))
                {
                    employee.LoginRole = AuthorizationManagerConstants.ROLERPM;
                }
                else
                    employee.LoginRole = string.Empty;
                //Siddhesh Arekar 08/07/2015 End

                addEmployeeBAL.UpdateEmployee(employee, IsEmployeeDetailsModified);

                //Siddharth 3 April 2015 Start
                //DataTable dt = new DataTable();
                //dt.Columns.Add(new DataColumn("EmpId", typeof(string)));
                //dt.Columns.Add(new DataColumn("ProjectNo", typeof(string)));
                //dt.Columns.Add(new DataColumn("ProjectName", typeof(string)));
                //dt.Columns.Add(new DataColumn("CostCode", typeof(string)));
                //dt.Columns.Add(new DataColumn("Billing", typeof(string)));
                //DataRow dr;

                //// dt = (DataTable) ViewState["EmployeeProjectCodeCode"];

                //for (int i = 0; i < gvCostCode.Rows.Count; i++)
                //{
                //    dr = dt.NewRow();
                //    dr["EmpId"] = Hidden_EmployeeID.Value;
                //    dr["ProjectName"] = ((DropDownList)gvCostCode.Rows[i].FindControl("ddlProject")).SelectedValue;
                //    dr["CostCode"] = ((TextBox)gvCostCode.Rows[i].FindControl("txtCostCode")).Text;
                //    dr["Billing"] = ((TextBox)gvCostCode.Rows[i].FindControl("txtBilling")).Text;
                //    dt.Rows.Add(dr);
                //}
                //addEmployeeBAL.UpdateEmployeeCostCode(dt, employee);
                //Siddharth 3 April 2015 End

                PopulateControls();

                addEmployeeBAL.SendMailForEditEmployee(employee, prevEmployeeDetails);



                btnEdit.Visible = false;

                lblConfirmMsg.Text = "Employee Details updated successfully.";
            }
        }

        //}
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnUpdate_Click", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// Handles the Click event of the imgbtnUpload control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.ImageClickEventArgs"/> instance containing the event data.</param>
    protected void imgbtnUpload_Click(object sender, ImageClickEventArgs e)
    {
        StringBuilder errMessage = new StringBuilder();
        string extension;
        string fullFileName;

        //Checks if any Image is selected
        if (FileUpload1.PostedFile.FileName == string.Empty)
        {
            //errorLbl.Text = errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.resmgr.GetString(ErrorMessageConstants.ERR_PRODUCT_UPLOAD_FILE)).ToString();
            return;
        }
        else
        {
            try
            {
                extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                //Generates a new name for the image using GUID
                string fileName = Guid.NewGuid() + extension;
                //saves the image file
                string appPath = ConfigurationSettings.AppSettings["ImagePhysicalPath"];
                //Makes a Full name of the image with the path where the image will be saved
                fullFileName = appPath + fileName;
                FileUpload1.PostedFile.SaveAs(fullFileName);

                //The image is shown in the image control on the page
                FileInfo sFile = new FileInfo(fullFileName);
                if (sFile.Length == 0)
                {
                    //errorLbl.Text = errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.resmgr.GetString(ErrorMessageConstants.ERR_PRODUCT_UPLOAD_FILEPATH)).ToString(); ;
                    imgEmp.ImageUrl = string.Empty;
                    imgEmp.Visible = false;
                }
                else
                {
                    imgEmp.ImageUrl = imagePath + fileName;
                    imgEmp.Visible = true;
                }

            }
            catch (RaveHRException ex)
            {
                LogErrorMessage(ex);
            }
            catch (Exception ex)
            {
                RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "imgbtnUpload_Click", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
                LogErrorMessage(objEx);
            }
        }
    }

    /// <summary>
    /// Handles the Click event of the btnResume control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnResume_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(CommonConstants.PAGE_GENERATERESUME);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnResume_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Preview Button Click
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnPrevious_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Next Button CLick
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnNext_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlDepartment control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartment.SelectedValue != CommonConstants.SELECT)
            {
                FillRoleDropdownAsPerDepartment();
                ucDatePickerDepartmentChangeDate.Text = "";
                ucDatePickerDesignationChangeDate.Text = "";
            }
            else
            {
                ddlDesignation.Items.Clear();
                ddlDesignation.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlDepartment_SelectedIndexChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    //Mohamed : Issue 39509/41062 : 07/03/2013 : Starts                        			  
    //Desc :  Adding new Columns date for Probation,Designation and Departement

    protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDesignation.SelectedValue != CommonConstants.SELECT)
            {
                ucDatePickerDesignationChangeDate.Text = "";
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlDesignation_SelectedIndexChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }
    //Mohamed : Issue 39509/41062 : 07/03/2013 : Ends

    /// <summary>
    /// Handles the Click event of the btnCancel control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //To solved issue id 19221
            //Code Added by Rahul P
            //Start
            if (Request.QueryString[PAGETYPE] != null)
            {
                if (DecryptQueryString(PAGETYPE) == PAGETYPEEMPLOYEESUMMERY)
                {
                    Response.Redirect(CommonConstants.PAGE_EMPLOYEESUMMARY, false);
                }
            }
            //End
            else
            {
                Response.Redirect(CommonConstants.PAGE_HOME, false);
            }

            // 29884-Ambar-Start-09062012
            Session[SessionNames.REFRESHPAGE] = null;
            // 29884-Ambar-End-09062012
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnCancel_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlMaritalStatus control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlMaritalStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlMaritalStatus.SelectedItem.Text == "Married")
            {
                txtSpouseName.Visible = true;
                lblSpouseName.Visible = true;
                lblmandatorymark.Visible = true;
                txtSpouseName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtSpouseName.ClientID + "','" + imgSpouseName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPACE + "');");
                imgSpouseName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanSpouseName.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
                imgSpouseName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanSpouseName.ClientID + "');");
            }
            else
            {
                txtSpouseName.Visible = false;
                lblSpouseName.Visible = false;
                lblmandatorymark.Visible = false;
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlMaritalStatus_SelectedIndexChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


  

    /// <summary>
    /// Handles the Click event of the btnDelete control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        BusinessEntities.Employee emp = null;
        string empCode = string.Empty;
        string fullName = string.Empty;

        try
        {
            if (Session[SessionNames.EMPLOYEEDETAILS] != null)
            {
                emp = (BusinessEntities.Employee)Session[SessionNames.EMPLOYEEDETAILS];
            }

            if (emp != null)
            {
                if (emp.EmailId.ToLower() == UserMailId.ToLower()) return;

                Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
                employeeBL.DeleteEmployee(emp);
                empCode = emp.EMPCode;
                fullName = emp.FirstName + " " + emp.LastName;
                //--Get mailIds
                AuthorizationManager authoriseduser = new AuthorizationManager();
                string strCurrentUser = authoriseduser.getLoggedInUserEmailId();

                Response.Redirect(CommonConstants.PAGE_HOME, false);


                lblConfirmMsg.Text = "Employee Deleted successfully.";
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnDelete_Click", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the OnTextChanged event of the txtDOB control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void txtDOB_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            string[] DateofBirthArr;

            if (!string.IsNullOrEmpty(ucDatePickerDOB.Text))
            {
                int MinYear = 0;
                int MaxYear = 0;

                MinYear = System.DateTime.Now.AddYears(-70).Year;
                MaxYear = System.DateTime.Now.AddYears(-18).Year;

                DateofBirthArr = Convert.ToString(ucDatePickerDOB.Text).Split(SPILITER_SLASH);
                DateTime DateofBirth = new DateTime(Convert.ToInt32(DateofBirthArr[2]), Convert.ToInt32(DateofBirthArr[1]), Convert.ToInt32(DateofBirthArr[0]));

                if (DateofBirth.Year < MinYear || DateofBirth.Year > MaxYear)
                {
                    lblMessage.Text = "Birth date should be between 1-Jan-" + MinYear + " and 31-Dec-" + MaxYear + ".";
                    return;
                }

            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "txtDOB_OnTextChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the Click event of the lnkSaveBtn control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void lnkSaveBtn_Click(object sender, EventArgs e)
    {
        try
        {
            System.Windows.Forms.DialogResult dialougeResult = System.Windows.Forms.MessageBox.Show(CommonConstants.DATANOTSAVED, "", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation);
            if (dialougeResult.ToString() == System.Windows.Forms.DialogResult.Yes.ToString())
            {
                btnUpdate_Click(null, null);
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "lnkSaveBtn_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    #endregion Protected Methods

    #region Private Method

    /// <summary>
    /// Fucntion will Set the Index of Page
    /// </summary>
    private void SetEmployeeIndex()
    {
        Hashtable htnew = new Hashtable();

        if (Request.QueryString[INDEX] != null)
        {
            int countIndex = Convert.ToInt32(DecryptQueryString(INDEX));

            if (Session[SessionNames.EMPLOYEEVIEWINDEX] != null)
            {
                htnew = (Hashtable)Session[SessionNames.EMPLOYEEVIEWINDEX];
            }

            EMPLOYEECURRENTCOUNT = countIndex;
            EMPLOYEEPREVIOUSCOUNT = EMPLOYEECURRENTCOUNT - 1;
            EMPLOYEENEXTCOUNT = (htnew.Keys.Count - 2) - countIndex;
            //if (countIndex == 0)
            //{
            //    btnNext.Enabled = false;
            //    btnPrevious.Enabled = false;
            //}
            //if (htnew.Keys.Count == 1)
            //{
            //    btnNext.Visible = false;
            //    btnPrevious.Visible = false;
            //}
            //else if (EMPLOYEEPREVIOUSCOUNT == -1)
            //{
            //    btnNext.Visible = true;
            //    btnPrevious.Visible = false;
            //}
            //else if (EMPLOYEENEXTCOUNT == -1)
            //{
            //    btnNext.Visible = false;
            //    btnPrevious.Visible = true;
            //}

            if (htnew.Keys.Count == 1)
            {
                btnNext.Enabled = false;
                btnPrevious.Enabled = false;
            }
            else if (EMPLOYEEPREVIOUSCOUNT == -1)
            {
                btnNext.Enabled = true;
                btnPrevious.Enabled = false;
            }
            else if (EMPLOYEENEXTCOUNT == -1)
            {
                btnNext.Enabled = false;
                btnPrevious.Enabled = true;
            }
        }
        else
        {
            btnNext.Enabled = false;
            btnPrevious.Enabled = false;
        }
    }

    /// <summary>
    /// Gets the employee.
    /// </summary>
    /// <param name="mailID">The mail ID.</param>
    /// <returns></returns>
    private BusinessEntities.Employee GetEmployee(string mailID)
    {
        BusinessEntities.Employee empPar = null;
        empPar = new BusinessEntities.Employee();
        empPar.EmailId = mailID;
        BusinessEntities.Employee employee = new BusinessEntities.Employee();
        Rave.HR.BusinessLayer.Employee.Employee emp = new Rave.HR.BusinessLayer.Employee.Employee();

        try
        {
            employee = emp.GetEmployeeByEmpId(empPar);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetEmployee", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

        return employee;
    }

    /// <summary>
    /// Gets the employee.
    /// </summary>
    /// <param name="employeeObj">The employee obj.</param>
    /// <returns></returns>
    private BusinessEntities.Employee GetEmployee(BusinessEntities.Employee employeeObj)
    {
        BusinessEntities.Employee empPar = null;
        empPar = new BusinessEntities.Employee();
        BusinessEntities.Employee employee = new BusinessEntities.Employee();
        Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();

        try
        {
            employee = employeeBL.GetEmployee(employeeObj);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetEmployee", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

        return employee;
    }



    //Siddharth 6 April 2015 Start
    /// <summary>
    /// Gets the employee CostCode By EmpID.
    /// </summary>
    /// <param name="employeeObj">The employee obj.</param>
    /// <returns></returns>
    private DataTable GetEmployeeCostCodeByEmpID(int EmpID)
    {
        BusinessEntities.Employee empPar = null;
        empPar = new BusinessEntities.Employee();
        BusinessEntities.Employee employee = new BusinessEntities.Employee();
        Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
        DataTable dt = null;
        try
        {
            dt = new DataTable();
            dt = employeeBL.Employee_GetEmployeeCostCodeByEmpID(EmpID);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetEmployeeCostCodeByEmpID", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

        return dt;
    }

    /// <summary>
    /// Gets the employee CostCode By EmpID.
    /// </summary>
    /// <param name="employeeObj">The employee obj.</param>
    /// <returns></returns>
    private string Employee_GetEmployeeCostCodeByEmpIDandPrjID(int EmpID, int ProjectID)
    {
        BusinessEntities.Employee empPar = null;
        empPar = new BusinessEntities.Employee();
        BusinessEntities.Employee employee = new BusinessEntities.Employee();
        Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
        string str = string.Empty;
        try
        {
            str = employeeBL.Employee_GetEmployeeCostCodeByEmpIDandPrjID(EmpID, ProjectID);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetEmployeeCostCodeByEmpID", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
        return str;
    }

        /// <summary>
    /// Gets the employee CostCode By EmpID.
    /// </summary>
    /// <param name="employeeObj">The employee obj.</param>
    /// <returns></returns>
    private string Employee_GetEmployeeCostCodeByEmpIDandNoPrjID(int EmpID)
    {
        BusinessEntities.Employee empPar = null;
        empPar = new BusinessEntities.Employee();
        BusinessEntities.Employee employee = new BusinessEntities.Employee();
        Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
        string str = string.Empty;
        try
        {
            str = employeeBL.Employee_GetEmployeeCostCodeByEmpIDandNoPrjID(EmpID);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetEmployeeCostCodeByEmpID", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
        return str;
    }

    //Siddharth 6 April 2015 End


    //Siddharth 25th August 2015 Start
    /// <summary>
    /// Gets the employee business unit.
    /// </summary>
    private void GetEmployeeBusinessUnit()
    {
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        try
        {
            raveHrColl = master.FillDropDownsBL((int)Common.EnumsConstants.Category.ResourceBusinessunit);

            ddlResourceBussinesUnit.Items.Clear();
            ddlResourceBussinesUnit.DataSource = raveHrColl;
            ddlResourceBussinesUnit.DataTextField = Common.CommonConstants.DDL_DataTextField;
            ddlResourceBussinesUnit.DataValueField = Common.CommonConstants.DDL_DataValueField;
            ddlResourceBussinesUnit.DataBind();
            ddlResourceBussinesUnit.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetEmployeeBusinessUnit", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    //Siddharth 25th August 2015 Start


    /// <summary>
    /// Gets the employee designations.
    /// </summary>
    private void GetEmployeeDesignations()
    {
        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        //Declaring COllection class object
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        //Calling Fill dropdown Business layer method to fill 
        //the dropdown from Master class.
        raveHrColl = master.FillDropDownsBL((int)Common.EnumsConstants.Category.Designations);

        ddlDesignation.DataSource = raveHrColl;
        ddlDesignation.DataTextField = Common.CommonConstants.DDL_DataTextField;
        ddlDesignation.DataValueField = Common.CommonConstants.DDL_DataValueField;
        ddlDesignation.DataBind();
        ddlDesignation.Items.Insert(0, CommonConstants.SELECT);

    }

    /// <summary>
    /// Gets the employee band.
    /// </summary>
    private void GetEmployeeBand()
    {
        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        //Declaring COllection class object
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        //Calling Fill dropdown Business layer method to fill 
        //the dropdown from Master class.
        raveHrColl = master.FillDropDownsBL((int)Common.EnumsConstants.Category.EmployeeBand);

        ddlBand.DataSource = raveHrColl;
        ddlBand.DataTextField = Common.CommonConstants.DDL_DataTextField;
        ddlBand.DataValueField = Common.CommonConstants.DDL_DataValueField;
        ddlBand.DataBind();
        ddlBand.Items.Insert(0, CommonConstants.SELECT);

    }

    /// <summary>
    /// Gets the type of the employee.
    /// </summary>
    private void GetEmployeeType()
    {
        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        //Declaring COllection class object
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        //Calling Fill dropdown Business layer method to fill 
        //the dropdown from Master class.
        raveHrColl = master.FillDropDownsBL((int)Common.EnumsConstants.Category.EmployeeType);

        ddlEmployeeType.DataSource = raveHrColl;
        ddlEmployeeType.DataTextField = Common.CommonConstants.DDL_DataTextField;
        ddlEmployeeType.DataValueField = Common.CommonConstants.DDL_DataValueField;
        ddlEmployeeType.DataBind();
        ddlEmployeeType.Items.Insert(0, CommonConstants.SELECT);

    }

    /// <summary>
    /// Gets the employee status.
    /// </summary>
    private void GetEmployeeStatus()
    {
        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        //Declaring COllection class object
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        //Calling Fill dropdown Business layer method to fill 
        //the dropdown from Master class.
        raveHrColl = master.FillDropDownsBL((int)Common.EnumsConstants.Category.EmployeeStatus);

        //ddlStatus.DataSource = raveHrColl;
        //ddlStatus.DataTextField = Common.CommonConstants.DDL_DataTextField;
        //ddlStatus.DataValueField = Common.CommonConstants.DDL_DataValueField;
        //ddlStatus.DataBind();
        //ddlStatus.Items.Insert(0, CommonConstants.SELECT);

    }

    /// <summary>
    /// Gets the employee department.
    /// </summary>
    private void GetEmployeeDepartment()
    {
        //Declaring COllection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        //Calling Fill dropdown Business layer method to fill 
        //the dropdown from Master class.
        raveHRCollection = master.FillDepartmentDropDownBL();

        ddlDepartment.DataSource = raveHRCollection;
        ddlDepartment.DataTextField = Common.CommonConstants.DDL_DataTextField;
        ddlDepartment.DataValueField = Common.CommonConstants.DDL_DataValueField;
        ddlDepartment.DataBind();
        ddlDepartment.Items.Insert(0, CommonConstants.SELECT);
    }

    /// <summary>
    /// Gets the employee prefix.
    /// </summary>
    private void GetEmployeePrefix()
    {
        //Declaring COllection class object
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        //Calling Fill dropdown Business layer method to fill 
        //the dropdown from Master class.
        raveHrColl = master.FillDropDownsBL((int)Common.EnumsConstants.Category.Prefix);

        ddlPrefix.DataSource = raveHrColl;
        ddlPrefix.DataTextField = Common.CommonConstants.DDL_DataTextField;
        ddlPrefix.DataValueField = Common.CommonConstants.DDL_DataValueField;
        ddlPrefix.DataBind();
        ddlPrefix.Items.Insert(0, CommonConstants.SELECT);
    }

    /// <summary>
    /// Gets the employee location.
    /// </summary>
    private void GetEmployeeLocation()
    {
        //Declaring COllection class object
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        //Calling Fill dropdown Business layer method to fill 
        //the dropdown from Master class.
        raveHrColl = master.FillDropDownsBL((int)Common.EnumsConstants.Category.CandidateLocation);

        ddlEmpLocation.ClearSelection();
        //ddlEmpLocation.Items.Clear();
        ddlEmpLocation.DataSource = raveHrColl;
        ddlEmpLocation.DataTextField = Common.CommonConstants.DDL_DataTextField;
        ddlEmpLocation.DataValueField = Common.CommonConstants.DDL_DataValueField;
        ddlEmpLocation.DataBind();
        ddlEmpLocation.Items.Insert(0, CommonConstants.SELECT);
    }

    //Siddharth 9th June 2015 Start
    /// <summary>
    /// Gets the employee BPSS Version.
    /// </summary>
    private void GetBPSSVersion()
    {
        //Declaring COllection class object
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        //Calling Fill dropdown Business layer method to fill 
        //the dropdown from Master class.
        raveHrColl = master.FillDropDownsBL((int)Common.EnumsConstants.Category.BPSSVersion);

        ddlBPSSVersion.ClearSelection();
        //ddlEmpLocation.Items.Clear();
        ddlBPSSVersion.DataSource = raveHrColl;
        ddlBPSSVersion.DataTextField = Common.CommonConstants.DDL_DataTextField;
        ddlBPSSVersion.DataValueField = Common.CommonConstants.DDL_DataValueField;
        ddlBPSSVersion.DataBind();
        ddlBPSSVersion.Items.Insert(0, CommonConstants.SELECT);
    }
    //Siddharth 9th June 2015 Start


    /// <summary>
    /// Populates the controls.
    /// </summary>
    private void PopulateControls()
    {
        BusinessEntities.Employee employee = new BusinessEntities.Employee();

        try
        {
            //DataTable dtGetEmpCostCodes = new DataTable();
            if (Session[SessionNames.EMPLOYEEDETAILS] != null)
            {
                employee = (BusinessEntities.Employee)Session[SessionNames.EMPLOYEEDETAILS];
                employee = this.GetEmployee(employee);
                //Siddharth 6 April 2015 Start
                //  dtGetEmpCostCodes = this.GetEmployeeCostCodeByEmpID(employee.EMPId);
                //Siddharth 6 April 2015 ENd
            }
            else
            {
                if (UserMailId != string.Empty && UserMailId != null)
                    employee = this.GetEmployee(UserMailId);
                //Siddharth 6 April 2015 Start
                // dtGetEmpCostCodes = this.GetEmployeeCostCodeByEmpID(employee.EMPId);
                //Siddharth 6 April 2015 Start
            }


            //Siddharth 2 April 2015 Start
            //Bind grid Here
            // DefaultGridView();
            //Siddharth 2 April 2015 End

            Session[Common.SessionNames.EMPLOYEEDETAILS] = employee;

            // Ishwar - NISRMS - 27112014 Start
            int pCount;
            strUserIdentity = HttpContext.Current.ApplicationInstance.Session["WindowsUsername"].ToString().Trim();
            employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();

            //Siddharth 7 April 2015 Start
            //pCount = employeeBL.EDCEmployeeCount(strUserIdentity, Convert.ToString(employee.EMPId));
            //Siddharth 7 April 2015 End
            objAuth = new Common.AuthorizationManager.AuthorizationManager();


            //if (pCount >= CommonConstants.ONE && Convert.ToString(ConfigurationManager.AppSettings["NISReportsAccess"]).Contains(objAuth.getLoggedInUser()))
            //Siddharth 7 April 2015 Start
            //if (Convert.ToString(ConfigurationManager.AppSettings["NISReportsAccess"]).Contains(objAuth.getLoggedInUser()))
            //{
            //    pnlCostCode.Visible = true;
            //}
            //else if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR) || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM))// && pCount >= CommonConstants.ONE)
            //{
            //    if (UserMailId.ToLower() != employee.EmailId.ToLower())
            //    {
            //        pnlCostCode.Visible = true;
            //        pnlCostCode.Enabled = false;
            //    }
            //}
            //else
            //{
            //    pnlCostCode.Visible = false;
            //}
            //Siddharth 7 April 2015 End
            // Ishwar - NISRMS - 27112014 End

            //Logic:- RMO



            if (employee == null)
            {
                Response.Redirect(CommonConstants.PAGE_HOME, false);
                return;
            }
            //BusinessEntities.Employee employee = new BusinessEntities.Employee();
            //employee = (BusinessEntities.Employee)Session[SessionNames.EMPLOYEEDETAILS];
            //To solved issue id 19221
            //Code Added by Rahul P
            //Start
            if ((employee.FileName == null) || (employee.LastName == null))
            {
                lblempName.Text = string.Empty;
            }
            //End
            else
            {
                lblempName.Text = employee.FirstName.ToUpper() + " " + employee.LastName.ToUpper();
            }

            //Umesh: Issue Id - 54410 : 'To check whether user has access to edit Employee's details on Employee Summary page' starts
            string strNISUsers = string.Empty;
            if (ConfigurationManager.AppSettings["NISReportsAccess"] != null)
            {
                strNISUsers = ConfigurationManager.AppSettings["NISReportsAccess"].ToString();
            }
            if (UserMailId.ToLower() == employee.EmailId.ToLower() || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR)
                || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM))
            {
                btnEdit.Visible = true;
            }
            else
            {
                objAuth = new Common.AuthorizationManager.AuthorizationManager();
                if (strNISUsers.Contains(objAuth.getLoggedInUser()))
                {
                    btnEdit.Visible = true;
                }
            }
            //Umesh: Issue Id - 54410 : 'To check whether user has access to edit Employee's details on Employee Summary page' ends

            //To solved issue id 19221
            //Code Added by Rahul P
            //Start
            if (Session[SessionNames.PAGEMODE] != null)
            {
                PageMode = Session[SessionNames.PAGEMODE].ToString();

                if (PageMode == Common.MasterEnum.PageModeEnum.View.ToString())
                {
                    this.EnableControl();
                    btnUpdate.Visible = false;
                }

            }
            //End
            if (employee != null)
            {
                ddlPrefix.SelectedValue = employee.Prefix.ToString();
                txtEmployeeCode.Text = employee.EMPCode;
                txtFirstName.Text = employee.FirstName;
                txtLastName.Text = employee.LastName;
                txtEmailID.Text = employee.EmailId;
                //Issue Id : 36961 Mahendra 18 Mar 2013 Start
                hdtxtEmail.Value = employee.EmailId;
                //Issue Id : 36961 Mahendra 18 Mar 2013 End
                ddlDepartment.SelectedValue = employee.GroupId.ToString();
                Session["DeptId"] = ddlDepartment.SelectedValue.ToString();
                Session["EmpId"] = employee.EMPId;


                //Rakesh : Business Vertical Wise Resume Template Download 08/07/2016 Begin 
                employee.BusinessVertical = employeeBL.GetEmployeBusinessVerticalID(employee.EMPId);


                //Rakesh : Business Vertical Wise Resume Template Download 08/07/2016 End

                Session["BusinessVertical"] = employee.BusinessVertical;
                FillRoleDropdownAsPerDepartment();
                ddlEmployeeType.SelectedValue = employee.Type.ToString();
                ddlBand.SelectedValue = employee.Band.ToString();
                ddlDesignation.SelectedValue = employee.DesignationId.ToString();
                ucDatePickerJoiningDate.Text = employee.JoiningDate == DateTime.MinValue ? string.Empty : employee.JoiningDate.ToShortDateString();
                hidReportingTo.Value = employee.ReportingToId;
                txtReportingTo.Text = employee.ReportingTo;
                hidReportingFM.Value = employee.ReportingToFMId.ToString();
                txtReportingFM.Text = employee.ReportingToFM;
                ////ddlEmpLocation.SelectedValue = employee.EmpLocation.ToString().Trim();
                //ddlEmpLocation.SelectedItem.Text = employee.EmpLocation.ToString().Trim();


                //Siddharth 9th June 2015 Start
                DateTime dtBPSSVersionDate = new DateTime(2015, 6, 15);
                if (employee.JoiningDate >= dtBPSSVersionDate)
                    ddlBPSSVersion.SelectedIndex = ddlBPSSVersion.Items.IndexOf(ddlBPSSVersion.Items.FindByText("2.0"));//2.0
                else
                    ddlBPSSVersion.SelectedIndex = ddlBPSSVersion.Items.IndexOf(ddlBPSSVersion.Items.FindByValue(employee.BPSSVersion.Trim()));

                if (Convert.ToString(employee.BPSSCompletionDate) == "01/01/1901 00:00:00")
                {
                    ucdatepickerCompletionDate.Text = string.Empty;
                }
                else
                {
                    ucdatepickerCompletionDate.Text = Convert.ToString(employee.BPSSCompletionDate) == string.Empty ? string.Empty : Convert.ToDateTime(employee.BPSSCompletionDate.ToString()).ToShortDateString();
                }
                //Convert.ToDateTime(employee.BPSSCompletionDate.ToString().Trim()).ToShortDateString(); //
                //Siddharth 9th June 2015 End


                //Siddharth 25th August 2015 Start
                ddlResourceBussinesUnit.SelectedValue = employee.ResourceBussinessUnit == 0 ? CommonConstants.SELECT : employee.ResourceBussinessUnit.ToString();
                //Siddharth 25th August 2015 Start


                //Mohamed : Issue 39509/41062 : 07/03/2013 : Starts                        			  
                //Desc :  Adding new Columns date for Probation,Designation and Departement

                ucDatePickerDepartmentChangeDate.Text = employee.DepartementChangeDate == string.Empty ? string.Empty : Convert.ToDateTime(employee.DepartementChangeDate.ToString()).ToShortDateString();
                ucDatePickerDesignationChangeDate.Text = employee.DesignationChangeDate == string.Empty ? string.Empty : Convert.ToDateTime(employee.DesignationChangeDate.ToString()).ToShortDateString();
                ucDatePickerConfirmedDate.Text = employee.ConfirmedDate == string.Empty ? string.Empty : Convert.ToDateTime(employee.ConfirmedDate.ToString()).ToShortDateString();
                if (employee.StatusId == 143) // Employees who left the Company
                {
                    lblStatus.Text = "Inactive";
                }
                else if (employee.ResignationDate == DateTime.MinValue) // Employee who are currently working
                {
                    if (employee.Type == 144 || employee.Type == 683) // Employee who are "Permanent" and "ASE"
                    {
                        if (employee.ProbationFlag)
                        {
                            lblStatus.Text = "Confirmed";
                        }
                        else
                        {
                            lblStatus.Text = "On Probation";
                        }
                    }
                    if (employee.Type == 145 || employee.Type == 684) // Employees who are Consultant
                    {
                        lblStatus.Text = "Consultant";
                    }
                }
                else //Employees who are currently working and resigned the job
                {
                    lblStatus.Text = "Serving Notice Period";
                }
                hfConfirmedDate.Value = employee.ConfirmedDate == string.Empty ? string.Empty : Convert.ToDateTime(employee.ConfirmedDate.ToString()).ToShortDateString();
                hfDesignationChangeDate.Value = employee.DesignationChangeDate == string.Empty ? string.Empty : Convert.ToDateTime(employee.DesignationChangeDate.ToString()).ToShortDateString();
                hfDepartmentChangeDate.Value = employee.DepartementChangeDate == string.Empty ? string.Empty : Convert.ToDateTime(employee.DepartementChangeDate.ToString()).ToShortDateString();
                hfDepartmentChange.Value = employee.GroupId.ToString();
                hfDesignationChange.Value = employee.DesignationId.ToString();
                //Mohamed : Issue 39509/41062 : 07/03/2013 : Ends

                ddlEmpLocation.ClearSelection();
                ddlEmpLocation.Items.FindByText(employee.EmpLocation.ToString().Trim()).Selected = true;
                string image1 = employee.FileName;
                string completeImagePhysicalPath = imagePhysicalPath + image1;
                if (!File.Exists(completeImagePhysicalPath))
                {
                    imgEmp.ImageUrl = imagePath + NoImage;

                }
                else
                {
                    imgEmp.ImageUrl = imagePath + image1;
                }
                ucDatePickerDOB.Text = employee.DOB == DateTime.MinValue ? string.Empty : employee.DOB.ToShortDateString();
                ddlGender.SelectedValue = employee.Gender.Trim() == string.Empty ? CommonConstants.SELECT : employee.Gender.Trim();
                ddlMaritalStatus.SelectedValue = employee.MaritalStatus.Trim() == string.Empty ? CommonConstants.SELECT : employee.MaritalStatus.Trim();
                txtMiddleName.Text = employee.MiddleName;
                txtSpouseName.Text = employee.SpouseName;
                txtBloodGroup.Text = employee.BloodGroup;
                ddlMaritalStatus_SelectedIndexChanged(null, null);

                if (employee.IsFresher == CommonConstants.ONE)
                    rblIsFresher.SelectedIndex = CommonConstants.ZERO;
                else
                    rblIsFresher.SelectedIndex = CommonConstants.ONE;

                //Rave.HR.BusinessLayer.Employee.EmergencyContact objEmergencyContactBL = new Rave.HR.BusinessLayer.Employee.EmergencyContact();

                //Ishwar 24112014 For NIS : Start
                //txtCostCode.Text = employee.CostCode;

                //Siddharth 6 April 2015 Start
                //gvCostCode.DataSource = dtGetEmpCostCodes;
                //gvCostCode.DataBind();
                //Siddharth 6 April 2015 End

                Session["CostCode"] = employee.CostCode;
                //Ishwar 24112014 For NIS : End
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "PopulateControls", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Enables the disable controls.
    /// </summary>
    private void EnableDisableControls()
    {
        BusinessEntities.Employee employee = new BusinessEntities.Employee();
        employee = (BusinessEntities.Employee)Session[SessionNames.EMPLOYEEDETAILS];
        //Ishwar 24112014 For NIS : Start
        objAuth = new Common.AuthorizationManager.AuthorizationManager();
        string strRMOGroupName = string.Empty;
        string strNISUsers = string.Empty;
        if (ConfigurationManager.AppSettings["RMOGroupName"] != null)
        {
            strRMOGroupName = ConfigurationManager.AppSettings["RMOGroupName"].ToString();
        }
        if (ConfigurationManager.AppSettings["NISReportsAccess"] != null)
        {
            strNISUsers = ConfigurationManager.AppSettings["NISReportsAccess"].ToString();
        }
        //Ishwar 24112014 For NIS : End
        if (UserMailId.ToLower() == employee.EmailId.ToLower())
        {
            //if (arrRolesForUser.Count > 0)
            //{
            pnlEmployeeDetails.Enabled = false;
            pnlEmpPersonalDetails.Enabled = true;
            //pnlEmpSecurityDetails.Enabled = true;
            imgEmpEmailPopulate.Disabled = true;
            imgReportingToPopulate.Disabled = true;
            //30936-Subhra-Start-01112011
            imgReportingFM.Disabled = true;
            //30936-Subhra-end-01112011
            FileUpload1.Enabled = false;
            
            //Siddharth 25th August 2015 Start
            if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM))//Chanchal and Kedar's names r there in Azman
            {
                ddlResourceBussinesUnit.Visible = true;
                trResourceBusinessUnit.Attributes.Remove("style");
            }
            else
            {
                ddlResourceBussinesUnit.Enabled = false;
                trResourceBusinessUnit.Attributes.Add("style", "display:none");
            }
            //Siddharth 25th August 2015 End

            if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR) || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM))
            {
                pnlEmployeeDetails.Enabled = true;
                imgEmpEmailPopulate.Disabled = false;
                imgReportingToPopulate.Disabled = false;
                //30936-Subhra-Start-01112011
                imgReportingFM.Disabled = false;
                //30936-Subhra-end-01112011
                FileUpload1.Enabled = true;
                //sanju 
                //Chrome and FF issue(onclick events get added only to the authorized user)
                imgEmpEmailPopulate.Attributes.Add("onclick", "return popUpEmployeeEmailPopulate();");
                imgReportingToPopulate.Attributes.Add("onclick", "return popUpEmployeeSearch();");
                imgReportingFM.Attributes.Add("onclick", "return popUpFunctionalManagerSearch();");
            }

            //}
            txtEmailID.Enabled = false;
        }
        if (UserMailId.ToLower() != employee.EmailId.ToLower())
        {
            if (arrRolesForUser.Count > 0)
            {
                //foreach (string STR in arrRolesForUser)
                //{
                //    if (STR != AuthorizationManagerConstants.ROLEHR)
                //    {
                //        pnlEmployeeDetails.Enabled = false;
                //        pnlEmpPersonalDetails.Enabled = false;
                //    }
                //}

               

                if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR) || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM))
                {
                    pnlEmployeeDetails.Enabled = true;
                    pnlEmpPersonalDetails.Enabled = true;
                    pnlEmpSecurityDetails.Enabled = true;
                    imgEmpEmailPopulate.Disabled = false;
                    imgReportingToPopulate.Disabled = false;
                    //30936-Subhra-Start-01112011
                    imgReportingFM.Disabled = false;
                    //30936-Subhra-end-01112011
                    FileUpload1.Enabled = true;
                    txtEmailID.Enabled = true;
                    //sanju 
                    //Chrome and FF issue(onclick events get added only to the authorized user)
                    imgEmpEmailPopulate.Attributes.Add("onclick", "return popUpEmployeeEmailPopulate();");
                    imgReportingToPopulate.Attributes.Add("onclick", "return popUpEmployeeSearch();");
                    imgReportingFM.Attributes.Add("onclick", "return popUpFunctionalManagerSearch();");

                    //Ishwar 24112014 For NIS : Start
                    //pnlCostCode.Visible = true;
                    //pnlCostCode.Enabled = false;
                    //Ishwar 24112014 For NIS : End
                }
                else
                {
                    if (strNISUsers.Contains(objAuth.getLoggedInUser()) || strRMOGroupName.Contains(objAuth.getLoggedInUser()))
                    {
                        btnUpdate.Visible = true;
                        btnEdit.Visible = false;
                    }
                    else
                    {
                        btnUpdate.Visible = false;
                        btnEdit.Visible = true;
                    }
                }
            }

        }


        //Issue ID:-20776
        //Coded by Anuj Govil
        //START
        txtReportingTo.ReadOnly = true;
        txtReportingFM.ReadOnly = true;
        txtReportingTo.ForeColor = Color.FromArgb(174, 174, 174);
        txtReportingFM.ForeColor = Color.FromArgb(174, 174, 174);
        //END

        //Ishwar 27112014 For NIS : Start
        strUserIdentity = HttpContext.Current.ApplicationInstance.Session["WindowsUsername"].ToString().Trim();
        Employee = new BusinessEntities.Employee();
        employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
        Employee = employeeBL.GetNISEmployeeList(strUserIdentity);

        int pCount;
        employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
        pCount = employeeBL.EDCEmployeeCount(strUserIdentity, Convert.ToString(employee.EMPId));


        





        //if (!String.IsNullOrEmpty(Employee.WindowsUserName))
        //{
        //if (Employee.WindowsUserName.ToUpper() == strUserIdentity.ToUpper())
        //{
        //if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR) && pCount >= CommonConstants.ONE)
        //{
        //    if (UserMailId.ToLower() != employee.EmailId.ToLower())
        //    {
        //        pnlCostCode.Visible = true;
        //        pnlCostCode.Enabled = false;
        //    }
        //}
        //if (pCount == CommonConstants.ZERO)
        //{
        //    pnlCostCode.Visible = false;
        //    pnlCostCode.Enabled = false;
        //}

        //if (pCount >= CommonConstants.ONE && strNISUsers.Contains(objAuth.getLoggedInUser()))
        //{
        //    //if (pCount >= CommonConstants.ONE)
        //    //{
        //    pnlCostCode.Visible = true;
        //    pnlCostCode.Enabled = true;
        //    //}
        //}

        //else
        //{
        //    if (pCount >= CommonConstants.ONE && Convert.ToString(ConfigurationManager.AppSettings["NISReportsAccess"]).Contains(objAuth.getLoggedInUser()))
        //    {
        //        pnlCostCode.Visible = true;
        //    }
        //}
        //}
        //}
        //else
        //{
        //   if (pCount == CommonConstants.ZERO)
        //    {
        //        pnlCostCode.Visible = false;
        //        pnlCostCode.Enabled = false;
        //    }

        //   if (strRMOGroupName.Contains(objAuth.getLoggedInUser()) || strNISUsers.Contains(objAuth.getLoggedInUser()))
        //   {
        //       if (pCount >= CommonConstants.ONE)
        //       {
        //           //For RMO Group Employees
        //           pnlCostCode.Visible = true;
        //           pnlCostCode.Enabled = true;
        //       }
        //   }
        //else
        //{
        //    if (pCount >= CommonConstants.ONE)
        //    {
        //        //For RMO Group Employees
        //        pnlCostCode.Visible = true;
        //    }
        //}
        //}
        //Ishwar 27112014 For NIS : End
    }

    /// <summary>
    /// Enables the control.
    /// </summary>
    private void EnableControl()
    {
        pnlEmployeeDetails.Enabled = false;
        pnlEmpPersonalDetails.Enabled = false;
        pnlEmpSecurityDetails.Enabled = false;
        imgEmpEmailPopulate.Disabled = true;
        imgReportingToPopulate.Disabled = true;
        //30936-Subhra-Start-01112011
        imgReportingFM.Disabled = true;
        //Subhra-end-01112011
        FileUpload1.Enabled = false;

        //Ishwar 20112014 For NIS : Start
        //pnlCostCode.Enabled = false;
        //Ishwar 20112014 For NIS : End

        //pnlPermanenetAddr.Enabled = false;
        //divEmergencyDetails.Disabled = true;
    }

    /// <summary>
    /// Function will call on Privious Click
    /// </summary>
    private void PreviousClick()
    {
        EMPLOYEECURRENTCOUNT = EMPLOYEECURRENTCOUNT - 1;
        EMPLOYEEPREVIOUSCOUNT = EMPLOYEEPREVIOUSCOUNT - 1;
        EMPLOYEENEXTCOUNT = EMPLOYEENEXTCOUNT + 1;

        EnableDisableButtons(EMPLOYEECURRENTCOUNT, EMPLOYEEPREVIOUSCOUNT, EMPLOYEENEXTCOUNT);
    }

    /// <summary>
    /// Function will call on Next Click
    /// </summary>
    private void NextClick()
    {
        EMPLOYEECURRENTCOUNT = EMPLOYEECURRENTCOUNT + 1;
        EMPLOYEEPREVIOUSCOUNT = EMPLOYEEPREVIOUSCOUNT + 1;
        EMPLOYEENEXTCOUNT = EMPLOYEENEXTCOUNT - 1;

        EnableDisableButtons(EMPLOYEECURRENTCOUNT, EMPLOYEEPREVIOUSCOUNT, EMPLOYEENEXTCOUNT);
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
            if (Session[SessionNames.EMPLOYEEVIEWINDEX] != null)
            {
                ht = (Hashtable)Session[SessionNames.EMPLOYEEVIEWINDEX];

                if (ht.Contains(PreviousIndex) == true)
                {
                    btnPrevious.Enabled = true;
                }
                else
                {
                    btnPrevious.Enabled = false;
                }

                if (ht.Contains(NextIndex) == true)
                {
                    btnNext.Enabled = true;
                }
                else
                {
                    btnNext.Enabled = false;
                }

                if (ht.Contains(currentIndex) == true)
                {
                    string empID = Convert.ToString(ht[currentIndex]);
                    BusinessEntities.Employee emp = new BusinessEntities.Employee();
                    emp.EMPId = Convert.ToInt32(empID);
                    Session[SessionNames.EMPLOYEEDETAILS] = emp;
                    //this.GetEmployee(emp);
                    this.PopulateControls();

                    if (PageMode == Common.MasterEnum.PageModeEnum.Edit.ToString())
                    {
                        btnEdit.Visible = false;
                    }

                    //CopyMRFDetail(Convert.ToInt32(ht[currentIndex]));
                }
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "EnableDisableButtons", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Validates the controls.
    /// </summary>
    /// <returns></returns>
    private bool ValidateControls()
    {
        StringBuilder errMessage = new StringBuilder();
        bool flag = true;
        string[] joiningDateArr;
        string[] ResignationDateArr;
        string[] LastWorkDayArr;
        string[] DateofBirthArr;
        string[] BPSSCompletionDate;

        //if (!string.IsNullOrEmpty(ucDatePickerJoiningDate.Text) && !string.IsNullOrEmpty(ucDatePickerDOB.Text))
        //{
        //    joiningDateArr = Convert.ToString(ucDatePickerJoiningDate.Text).Split(SPILITER_SLASH);
        //    DateTime joiningDate = new DateTime(Convert.ToInt32(joiningDateArr[2]), Convert.ToInt32(joiningDateArr[1]), Convert.ToInt32(joiningDateArr[0]));

        //    DateofBirthArr = Convert.ToString(ucDatePickerDOB.Text).Split(SPILITER_SLASH);
        //    DateTime DateofBirth = new DateTime(Convert.ToInt32(DateofBirthArr[2]), Convert.ToInt32(DateofBirthArr[1]), Convert.ToInt32(DateofBirthArr[0]));

        //    if (joiningDate > DateTime.Now.Date)
        //    {
        //        errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.JOINING_DATE_VALIDATION);
        //        flag = false;
        //    }

        //    if (DateofBirth > DateTime.Now.Date)
        //    {
        //        errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.BIRTH_DATE_VALIDATION);
        //        flag = false;
        //    }
        //}

        ////Siddharth 22 June 2015 Start
        //if (!string.IsNullOrEmpty(ucDatePickerJoiningDate.Text) && !string.IsNullOrEmpty(ucdatepickerCompletionDate.Text))
        //{
        //    joiningDateArr = Convert.ToString(ucDatePickerJoiningDate.Text).Split(SPILITER_SLASH);
        //    DateTime joiningDate = new DateTime(Convert.ToInt32(joiningDateArr[2]), Convert.ToInt32(joiningDateArr[1]), Convert.ToInt32(joiningDateArr[0]));

        //    BPSSCompletionDate = Convert.ToString(ucdatepickerCompletionDate.Text).Split(SPILITER_SLASH);
        //    DateTime DateofBPSSCompletion = new DateTime(Convert.ToInt32(BPSSCompletionDate[2]), Convert.ToInt32(BPSSCompletionDate[1]), Convert.ToInt32(BPSSCompletionDate[0]));

        //    if (joiningDate >= DateofBPSSCompletion)
        //    {
        //        errMessage.Append(CommonConstants.NEW_LINE + "BPSS Completion Date cannot be less than Employee Joining Date");
        //        flag = false;
        //    }

        //    if (DateofBPSSCompletion > DateTime.Now.Date)
        //    {
        //        errMessage.Append(CommonConstants.NEW_LINE + "BPSS Completion Date cannot be set to future date");
        //        flag = false;
        //    }


        //}
        ////Siddharth 22 June 2015 End





        //if (!string.IsNullOrEmpty(txtResignationDate.Text) && !string.IsNullOrEmpty(txtLastWorkDay.Text))
        //{
        //    ResignationDateArr = Convert.ToString(txtResignationDate.Text).Split(SPILITER_SLASH);
        //    DateTime ResignationDate = new DateTime(Convert.ToInt32(ResignationDateArr[2]), Convert.ToInt32(ResignationDateArr[1]), Convert.ToInt32(ResignationDateArr[0]));

        //    LastWorkDayArr = Convert.ToString(txtLastWorkDay.Text).Split(SPILITER_SLASH);
        //    DateTime LastWorkDay = new DateTime(Convert.ToInt32(LastWorkDayArr[2]), Convert.ToInt32(LastWorkDayArr[1]), Convert.ToInt32(LastWorkDayArr[0]));

        //    if (ResignationDate > DateTime.Now.Date)
        //    {
        //        errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.RESIGNATION_DATE_VALIDATION);
        //        flag = false;
        //    }

        //    if (LastWorkDay > DateTime.Now.Date)
        //    {
        //        errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.LASTWORKING_DAY_VALIDATION);
        //        flag = false;
        //    }
        //}

        //if (string.IsNullOrEmpty(txtPhone.Text.Trim()) && string.IsNullOrEmpty(txtMobile.Text.Trim()))
        //{

        //    errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.PHONEMOBILE_VALIDATION);
        //    flag = false;
        //}

        lblMessage.Text = errMessage.ToString();
        return flag;
    }

    /// <summary>
    /// Gets the employee designations.
    /// </summary>
    /// <param name="categoryID">The category ID.</param>
    private void GetEmployeeDesignations(int categoryID)
    {
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();

        //if (categoryID != (int)EnumsConstants.Category.ProjectRole)
        //{
        //    raveHrColl = master.FillDropDownsBL(categoryID);
        //}
        //else
        //{
        raveHrColl = employeeBL.GetEmployeesDesignations(categoryID);
        //}

        ddlDesignation.Items.Clear();
        ddlDesignation.DataSource = raveHrColl;
        ddlDesignation.DataTextField = Common.CommonConstants.DDL_DataTextField;
        ddlDesignation.DataValueField = Common.CommonConstants.DDL_DataValueField;
        ddlDesignation.DataBind();
        ddlDesignation.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

    }

    /// <summary>
    /// Based on the Department selected, fill the Role 
    /// </summary>
    private void FillRoleDropdownAsPerDepartment()
    {
        //if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Projects))
        //{
        //    GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.ProjectRole));
        //}
        //if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Admin))
        //{
        //    GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.AdminRole));
        //}
        //if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Finance))
        //{
        //    GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.FinanceRole));
        //}
        //if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.HR))
        //{
        //    GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.HRRole));
        //}
        //if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.ITS))
        //{
        //    GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.ITSRole));
        //}
        //if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Marketing))
        //{
        //    GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.MarketingRole));
        //}
        //if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.PMOQuality))
        //{
        //    GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.PMOQualityRole));
        //}
        //if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.PreSales))
        //{
        //    GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.PreSalesRole));
        //}
        //if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.RaveDevelopment))
        //{
        //    GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.RaveDevelopmentRole));
        //}
        //if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Support))
        //{
        //    ddlDesignation.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
        //}
        //if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Testing))
        //{
        //    GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.TestingRole));
        //}
        //if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Recruitment))
        //{
        //    GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.RecruitmentRole));
        //}

        GetEmployeeDesignations(int.Parse(ddlDepartment.SelectedValue));
    }

    /// <summary>
    /// Populates the dropdown controls.
    /// </summary>
    private void PopulateDropdownControls()
    {
        GetEmployeeDesignations(0);
        GetEmployeeBand();
        GetEmployeeType();
        GetEmployeeStatus();
        GetEmployeePrefix();
        GetEmployeeDepartment();
        GetEmployeeLocation();
        //Siddharth 9th June 2015 Start
        GetBPSSVersion();
        //Siddharth 9th June 2015 End

        //Siddharth 25th August 2015 Start
        GetEmployeeBusinessUnit();
        //Siddharth 25th August 2015 End
    }

    /// <summary>
    /// Get message body.
    /// </summary>
    private string GetMessageBody(string strToUser, string strFromUser, string departmentname, string empname)
    {
        HtmlForm htmlFormBody = new HtmlForm();
        try
        {
            StringBuilder strMessageBody = new StringBuilder();

            //Google
            string strUser = "";
            //Googleconfigurable
            AuthorizationManager obj = new AuthorizationManager();
            strUser = obj.GetUsernameBasedOnEmail(strFromUser);
            //if (strFromUser.ToLower().Trim().Contains("@rave-tech.com"))
            //{
            //    strUser = strFromUser.Replace("@rave-tech.com", "");
            //}
            //else
            //{
            //    strUser = strFromUser.Replace("@" + AuthorizationManagerConstants.NORTHGATEDOMAINEMAIL, "");
            //}

            strMessageBody.Append("Hello," + "</br>"
                + "This is to bring to your notice that " + empname + " from department " + departmentname + " is deleted from" + "</br>"
                + "the Resource Management System. " + "</br>" +
                // "</br>" + "</br>" + "Regards," + "</br>" + strFromUser.Replace(RAVE_DOMAIN, "") + "</br>");
                "</br>" + "</br>" + "Regards," + "</br>" + strUser + "</br>");

            htmlFormBody.Style.Add(HtmlTextWriterStyle.FontFamily, "5");
            htmlFormBody.Style.Add(HtmlTextWriterStyle.FontWeight, "10");
            htmlFormBody.InnerText = strMessageBody.ToString();
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetMessageBody", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

        return htmlFormBody.InnerText.ToString();
    }

    /// <summary>
    /// Creates the data table.
    /// </summary>
    /// <returns></returns>
    private DataTable CreateDataTable()
    {
        DataTable myDataTable = new DataTable();

        DataColumn myDataColumn;

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "ContactName";
        myDataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "Relation";
        myDataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.Int32");
        myDataColumn.ColumnName = "ContactNumber";
        myDataTable.Columns.Add(myDataColumn);

        DataRow row;

        row = myDataTable.NewRow();
        row["ContactName"] = string.Empty;
        row["Relation"] = string.Empty;
        row["ContactNumber"] = 0;
        myDataTable.Rows.Add(row);

        row = myDataTable.NewRow();
        row["ContactName"] = string.Empty;
        row["Relation"] = string.Empty;
        row["ContactNumber"] = 0;
        myDataTable.Rows.Add(row);

        row = myDataTable.NewRow();
        row["ContactName"] = string.Empty;
        row["Relation"] = string.Empty;
        row["ContactNumber"] = 0;
        myDataTable.Rows.Add(row);

        return myDataTable;
    }

    /// <summary>
    /// Reloads the control.
    /// </summary>
    private void ReloadControl()
    {
        PlaceHolder1.Controls.Clear();
        if (SavedControlVirtualPath != null)
        {
            Control control = this.LoadControl(SavedControlVirtualPath);
            if (control != null)
            {
                // Gives the control a unique ID. It is important to ensure
                // the page working properly. Here we use control.GetType().Name
                // as the ID.
                control.ID = control.GetType().Name;
                PlaceHolder1.Controls.Add(control);

                EmployeeMenuUC uc = control as EmployeeMenuUC;
                uc.BubbleClick += new EventHandler(BubbleControl_BubbleClick);
                //BubbleControl.BubbleClick += new EventHandler(BubbleControl_BubbleClick);
            }
        }
    }

    /// <summary>
    /// Enables a server control to perform final clean up before it is released from memory.
    /// </summary>
    public override void Dispose()
    {
        for (int i = 0; i < PlaceHolder1.Controls.Count; i++)
        {
            Control ctrl = PlaceHolder1.Controls[i];
            if (ctrl != null)
                ctrl.Dispose();
        }
        base.Dispose();
    }

    /// <summary>
    /// Handles the BubbleClick event of the BubbleControl control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    private void BubbleControl_BubbleClick(object sender, EventArgs e)
    {
        try
        {
            //Response.Write("WebForm1 :: WebForm1_BubbleClick from " +
            //               sender.GetType().ToString() + "<BR>");

            this.PopulateControls();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "BubbleControl_BubbleClick", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Deeps the copy.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj">The obj.</param>
    /// <returns></returns>
    public T DeepCopy<T>(T obj)
    {
        object result = null;

        using (var ms = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            ms.Position = 0;

            result = (T)formatter.Deserialize(ms);
            ms.Close();
        }

        return (T)result;


    }




    #endregion Private Method

    // 29501-Ambar-Start
    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlPrefix control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlPrefix_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPrefix.SelectedValue == CommonConstants.SELECT)
            {
                ddlGender.SelectedValue = "SELECT";
            }
            else if ((ddlPrefix.SelectedValue == "155") || (ddlPrefix.SelectedValue == "158"))
            {
                ddlGender.SelectedValue = "M";
            }
            else
            {
                ddlGender.SelectedValue = "F";
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlPrefix_SelectedIndexChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    // 29501-Ambar-End



    #region "Multiple Cost Codes"
    ////Siddharth 2 April 2015 Start
    //protected void btnAddRow_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (gvCostCode.Rows.Count > 0)
    //        {
    //            if (ValidateCostCodeGrid(gvCostCode) == false)
    //            {
    //                //if (ddlSkill.SelectedItem.Text == CommonConstants.SELECT)
    //                //{
    //                //   // lblError.Text = "Please select a Skill.";
    //                return;
    //            }
    //            else
    //            {
    //                DefaultGridView();
    //            }
    //        }
    //        else
    //        {
    //            DefaultGridView();

    //            TextBox txtCostCode = (TextBox)gvCostCode.Rows[0].FindControl("txtCostCode");
    //            TextBox txtBilling = (TextBox)gvCostCode.Rows[0].FindControl("txtBilling");
    //            HtmlGenericControl lblmandatorymarkCostCode = (HtmlGenericControl)gvCostCode.Rows[0].FindControl("lblmandatorymarkCostCode");
    //            HtmlGenericControl lblmandatorymarkBilling = (HtmlGenericControl)gvCostCode.Rows[0].FindControl("lblmandatorymarkBilling");
    //            if (Session[SessionNames.EMPLOYEEDETAILS] != null)
    //            {
    //                employee = (BusinessEntities.Employee)Session[SessionNames.EMPLOYEEDETAILS];
    //                string values = this.Employee_GetEmployeeCostCodeByEmpIDandNoPrjID(employee.EMPId);
    //                if (!string.IsNullOrEmpty(values))
    //                {
    //                    string[] arrValues = values.Split('~');
    //                    txtCostCode.Text = arrValues[0].ToString().Trim();
    //                    if (string.IsNullOrEmpty(arrValues[1].ToString().Trim()))
    //                        txtBilling.Text = "0";
    //                    else
    //                        txtBilling.Text = arrValues[1].ToString().Trim();
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnAddRow_Click", EventIDConstants.RAVE_EMP_SKILL_SEARCH_PRESENTATION_LAYER);
    //        LogErrorMessage(objEx);
    //    }
    //}


    ////Siddharth 7 April 2015 Start
    //protected bool ValidateCostCodeGrid(GridView gv)
    //{
    //    bool isValid = true;

    //    DropDownList ddlSkill = (DropDownList)gvCostCode.Rows[(gv.Rows.Count - 1)].FindControl("ddlProject");
    //    TextBox txtCostCode = (TextBox)gvCostCode.Rows[(gv.Rows.Count - 1)].FindControl("txtCostCode");
    //    TextBox txtBilling = (TextBox)gvCostCode.Rows[(gv.Rows.Count - 1)].FindControl("txtBilling");


    //    if (ddlSkill.SelectedItem.Text == CommonConstants.SELECT)
    //    {//Project is not selected
    //        if (String.IsNullOrEmpty(txtCostCode.Text.Trim()) && String.IsNullOrEmpty(txtBilling.Text.Trim()))
    //        {// Cost Code and Billing is Empty
    //            lblMessage.Text = "Please fill all details";
    //            isValid = false;
    //        }
    //        else if (!String.IsNullOrEmpty(txtCostCode.Text.Trim()) && String.IsNullOrEmpty(txtBilling.Text.Trim()))
    //        {//Cost Code is missing
    //            lblMessage.Text = "Please fill Billing";
    //            isValid = false;
    //        }

    //    }
    //    else
    //    {//Project is selected
    //        if (String.IsNullOrEmpty(txtCostCode.Text.Trim()))
    //        {
    //            lblMessage.Text = "Project is selected but Cost Code is not entered";
    //            isValid = false;
    //        }
    //    }

    //    return isValid;
    //}

    //protected bool ValidateCompleteCostCodeGrid(GridView gv)
    //{
    //    bool isValid = true;

    //    DataTable dtCheckDuplicates = new DataTable();
    //    dtCheckDuplicates= GridToDataTable(gvCostCode);

    //    //First Check if Duplicates Exist
    //    if (CheckDuplicatesRecords(dtCheckDuplicates) == 0)
    //    {//No Duplicates

    //        for (int i = 0; i < gv.Rows.Count; i++)
    //        {
    //            DropDownList ddlSkill = (DropDownList)gvCostCode.Rows[i].FindControl("ddlProject");
    //            TextBox txtCostCode = (TextBox)gvCostCode.Rows[i].FindControl("txtCostCode");
    //            TextBox txtBilling = (TextBox)gvCostCode.Rows[i].FindControl("txtBilling");

    //            if (ddlSkill.SelectedItem.Text == CommonConstants.SELECT)
    //            {//Project is not selected
    //                if (String.IsNullOrEmpty(txtCostCode.Text.Trim()) && String.IsNullOrEmpty(txtBilling.Text.Trim()))
    //                {// Cost Code and Billing is Empty
    //                    lblMessage.Text = "Please fill all details";
    //                    isValid = false;
    //                }
    //                else if (!String.IsNullOrEmpty(txtCostCode.Text.Trim()) && String.IsNullOrEmpty(txtBilling.Text.Trim()))
    //                {//Cost Code is missing
    //                    lblMessage.Text = "Please fill Billing";
    //                    isValid = false;
    //                }

    //            }
    //            else
    //            {//Project is selected
    //                if (String.IsNullOrEmpty(txtCostCode.Text.Trim()))
    //                {
    //                    lblMessage.Text = "Project is selected but Cost Code is not entered";
    //                    isValid = false;
    //                }
    //            }
    //        }
    //    }
    //    else
    //    {
    //        lblMessage.Text = "Duplicate Values entered in Cost Code Grid";
    //        isValid = false;
    //    }
    //    return isValid;
    //}

    //private int CheckDuplicatesRecords(DataTable dt)
    //{
    //    int isDuplicate = 0;
    //    var UniqueRows = dt.AsEnumerable().Distinct(DataRowComparer.Default);
    //    DataTable dt2 = UniqueRows.CopyToDataTable();
    //    isDuplicate = dt.Rows.Count - dt2.Rows.Count;
    //    return isDuplicate;
    //}

    //private DataTable GridToDataTable(GridView gv)
    //{
    //     DataTable dtGridToDataTable = new DataTable();

    //        //dtGridToDataTable.Columns.Add(new DataColumn("ProjectNo", typeof(string)));
    //        dtGridToDataTable.Columns.Add(new DataColumn("ProjectName", typeof(string)));
    //        dtGridToDataTable.Columns.Add(new DataColumn("CostCode", typeof(string)));
    //        dtGridToDataTable.Columns.Add(new DataColumn("Billing", typeof(string)));
    //        //int i = 1;

    //        foreach (GridViewRow gdr in gv.Rows)
    //        {
    //            DataRow dr = dtGridToDataTable.NewRow();
    //           // dr["ProjectNo"] = i;
    //            dr["ProjectName"] = ((DropDownList)gdr.FindControl("ddlProject")).SelectedValue;
    //            dr["CostCode"] = ((TextBox)gdr.FindControl("txtCostCode")).Text;
    //            dr["Billing"] = ((TextBox)gdr.FindControl("txtBilling")).Text;
    //            dtGridToDataTable.Rows.Add(dr);
    //            //i++;
    //        }
    //        return dtGridToDataTable;
    //}

    //Siddharth 7 April 2015 End

    //protected void DefaultGridView()
    //{
    //    try
    //    {
    //        dtEmployeeProjectCodtCodes = new DataTable();

    //        dtEmployeeProjectCodtCodes.Columns.Add(new DataColumn("ProjectNo", typeof(string)));
    //        dtEmployeeProjectCodtCodes.Columns.Add(new DataColumn("ProjectName", typeof(string)));
    //        dtEmployeeProjectCodtCodes.Columns.Add(new DataColumn("CostCode", typeof(string)));
    //        dtEmployeeProjectCodtCodes.Columns.Add(new DataColumn("Billing", typeof(string)));
    //        int i = 1;

    //        foreach (GridViewRow gdr in gvCostCode.Rows)
    //        {
    //            DataRow dr = dtEmployeeProjectCodtCodes.NewRow();
    //            dr["ProjectNo"] = i;
    //            dr["ProjectName"] = ((DropDownList)gdr.FindControl("ddlProject")).SelectedValue;
    //            dr["CostCode"] = ((TextBox)gdr.FindControl("txtCostCode")).Text;
    //            dr["Billing"] = ((TextBox)gdr.FindControl("txtBilling")).Text;
    //            dtEmployeeProjectCodtCodes.Rows.Add(dr);
    //            i++;
    //        }

    //        DataRow defualtdr = dtEmployeeProjectCodtCodes.NewRow();
    //        defualtdr["ProjectNo"] = i;
    //        dtEmployeeProjectCodtCodes.Rows.Add(defualtdr);
    //        //ViewState["EmployeeProjectCodeCode"] = dtEmployeeProjectCodtCodes;
    //        BindGridView(dtEmployeeProjectCodtCodes);
    //    }
    //    catch (Exception ex)
    //    {
    //        RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "DefaultGridView", EventIDConstants.RAVE_EMP_SKILL_SEARCH_PRESENTATION_LAYER);
    //        LogErrorMessage(objEx);
    //    }
    //}



    //protected void BindGridView(DataTable dt)
    //{
    //    try
    //    {
    //        if (dt.Rows.Count > 0)
    //        {
    //            gvCostCode.DataSource = dt;
    //            gvCostCode.DataBind();

    //            //Unlimited Rows can be added
    //            btnAddRow.Visible = true;
             
    //            dt.Clear();
    //        }
    //        else
    //        {
    //            dt = new DataTable();

    //            dt.Columns.Add(new DataColumn("ProjectNo", typeof(string)));
    //            dt.Columns.Add(new DataColumn("ProjectName", typeof(string)));
    //            dt.Columns.Add(new DataColumn("CostCode", typeof(string)));
    //            dt.Columns.Add(new DataColumn("Billing", typeof(string)));

    //            DataRow defualtdr = dt.NewRow();
    //            defualtdr["ProjectNo"] = 1;
    //            dt.Rows.Add(defualtdr);

    //            gvCostCode.DataSource = dt;
    //            gvCostCode.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "BindGridView", EventIDConstants.RAVE_EMP_SKILL_SEARCH_PRESENTATION_LAYER);
    //        LogErrorMessage(objEx);
    //    }
    //}



    //protected void gvCostCode_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    try
    //    {
    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            DropDownList ddlProject = (DropDownList)e.Row.FindControl("ddlProject");
    //            HiddenField HFSkillNo = (HiddenField)e.Row.FindControl("HFSkillName");
              
    //            ddlProject.DataSource = this.GetProjectCostCodes();
    //            ddlProject.DataTextField = Common.CommonConstants.DDL_DataValueField;
    //            ddlProject.DataValueField = Common.CommonConstants.DDL_DataTextField;
    //            ddlProject.DataBind();
    //            ddlProject.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
    //            if (HFSkillNo.Value !="")
    //                ddlProject.SelectedValue = HFSkillNo.Value.ToString();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "gvSkillCriteria_RowDataBound", EventIDConstants.RAVE_EMP_SKILL_SEARCH_PRESENTATION_LAYER);
    //        LogErrorMessage(objEx);
    //    }
    //}
    
    
    //protected void gvCostCode_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    try
    //    {
    //        //int RowID = int.Parse(e.CommandArgument.ToString());
    //        GridViewRow row = (GridViewRow)(((System.Web.UI.WebControls.Image)e.CommandSource).NamingContainer);
    //        int RowID = row.RowIndex;
    //        //Add 1 to RowID to maintain index
    //        RowID = RowID + 1;
    //        if (e.CommandName == "DeleteRow")
    //        {
    //            if (RowID > 1)
    //            {
    //                dtEmployeeProjectCodtCodes = new DataTable();

    //                dtEmployeeProjectCodtCodes.Columns.Add(new DataColumn("ProjectNo", typeof(string)));
    //                dtEmployeeProjectCodtCodes.Columns.Add(new DataColumn("ProjectName", typeof(string)));
    //                dtEmployeeProjectCodtCodes.Columns.Add(new DataColumn("CostCode", typeof(string)));
    //                dtEmployeeProjectCodtCodes.Columns.Add(new DataColumn("Billing", typeof(string)));
    //                int i = 1;
    //                foreach (GridViewRow gdr in gvCostCode.Rows)
    //                {
    //                    HiddenField hfSkillNo = (HiddenField)gdr.FindControl("HFSkillNo");
    //                    if (!string.IsNullOrEmpty(hfSkillNo.Value.ToString().Trim()) && !hfSkillNo.Value.ToString().Equals(CommonConstants.SELECT))
    //                    {
    //                        if (RowID != Convert.ToInt32(hfSkillNo.Value))
    //                        {
    //                            DataRow dr = dtEmployeeProjectCodtCodes.NewRow();
    //                            dr["ProjectNo"] = i;
    //                            dr["ProjectName"] = ((DropDownList)gdr.FindControl("ddlProject")).SelectedValue;
    //                            dr["CostCode"] = ((TextBox)gdr.FindControl("txtCostCode")).Text;
    //                            dr["Billing"] = ((TextBox)gdr.FindControl("txtBilling")).Text;
    //                            dtEmployeeProjectCodtCodes.Rows.Add(dr);
    //                            i++;
    //                        }
    //                    }
    //                }
    //                //ViewState["EmployeeProjectCodeCode"] = dtEmployeeProjectCodtCodes;
    //                BindGridView(dtEmployeeProjectCodtCodes);
    //            }
    //            else
    //            {
    //                lblMessage.Text = "Cannot delete the first row";
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "gvSkillCriteria_RowCommand", EventIDConstants.RAVE_EMP_SKILL_SEARCH_PRESENTATION_LAYER);
    //        LogErrorMessage(objEx);
    //    }
    //}



    /// <summary>
    /// Gets the skills.
    /// </summary>
    //private RaveHRCollection GetProjectCostCodes()
    //{
    //    Rave.HR.BusinessLayer.Employee.Employee objEmployeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
    //    BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

    //    raveHrColl = objEmployeeBL.GetProjectNameForEmpByEmpID(Convert.ToInt16(Hidden_EmployeeID.Value));

    //    return raveHrColl;
    //}




    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlProject control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    //protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    DropDownList ddlProject = (DropDownList)sender;
    //    GridViewRow GRow = (GridViewRow)ddlProject.NamingContainer;

    //    TextBox txtCostCode = (TextBox)GRow.FindControl("txtCostCode");
    //    TextBox txtBilling = (TextBox)GRow.FindControl("txtBilling");
    //    HtmlGenericControl lblmandatorymarkCostCode = (HtmlGenericControl)GRow.FindControl("lblmandatorymarkCostCode");
    //    HtmlGenericControl lblmandatorymarkBilling = (HtmlGenericControl)GRow.FindControl("lblmandatorymarkBilling");


    //    if (ddlProject.SelectedItem.Text == CommonConstants.SELECT)
    //    {
    //        //txtCostCode.Enabled = false;
    //        txtCostCode.Text = "";
    //        lblmandatorymarkCostCode.Visible = false;
    //        txtBilling.Enabled = true;
    //        lblmandatorymarkBilling.Visible = true;

    //        if (Session[SessionNames.EMPLOYEEDETAILS] != null)
    //        {
    //            employee = (BusinessEntities.Employee)Session[SessionNames.EMPLOYEEDETAILS];
    //            string values = this.Employee_GetEmployeeCostCodeByEmpIDandNoPrjID(employee.EMPId);
    //            if (!string.IsNullOrEmpty(values))
    //            {
    //                string[] arrValues = values.Split('~');
    //                txtCostCode.Text = arrValues[0].ToString().Trim();
    //                if (string.IsNullOrEmpty(arrValues[1].ToString().Trim()))
    //                    txtBilling.Text = "0";
    //                else
    //                    txtBilling.Text = arrValues[1].ToString().Trim();
    //            }
    //        }

    //    }
    //    else
    //    {
    //        txtCostCode.Enabled = true;
    //        txtCostCode.Text = "";
    //        lblmandatorymarkCostCode.Visible = true;
    //        txtBilling.Enabled = false;
    //        lblmandatorymarkBilling.Visible = false;
    //        if (Session[SessionNames.EMPLOYEEDETAILS] != null)
    //        {
    //            employee = (BusinessEntities.Employee)Session[SessionNames.EMPLOYEEDETAILS];

    //            string values = this.Employee_GetEmployeeCostCodeByEmpIDandPrjID(employee.EMPId, Convert.ToInt16(ddlProject.SelectedValue.ToString().Trim()));
    //            string[] arrValues = values.Split('~');
    //            txtCostCode.Text = arrValues[0].ToString().Trim();
    //            if (string.IsNullOrEmpty(arrValues[1].ToString().Trim()))
    //                txtBilling.Text = "0";
    //            else
    //                txtBilling.Text = arrValues[1].ToString().Trim();
    //        }
    //    }
    //}

    //Siddharth 2 April 2015 End
    #endregion "Multiple Cost Codes"






}
