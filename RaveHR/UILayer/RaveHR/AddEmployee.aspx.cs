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
using System.Xml;
using System.DirectoryServices;
using Common.AuthorizationManager;
using Rave.HR.BusinessLayer;
using System.Text;
using System.IO;
using Rave.HR.BusinessLayer.Interface;

public partial class AddEmployee : BaseClass
{
    #region Constants

    const string ReadOnly = "readonly";
    const string CLASS_NAME = "AddEmployee.aspx.cs";
    const string IMAGEPATHURL = "ImagePathURL";
    string imagePath = Utility.GetUrl() + ConfigurationSettings.AppSettings[IMAGEPATHURL];
    const string BackSlash = @"\";
    const string IMAGES = "Images";
    const string NoImage = "NoImage.jpg";

    /// <summary>
    /// Rave Email domain
    /// </summary>
    /// 
    //Googleconfigurable
    //private const string RAVE_DOMAIN = "@rave-tech.com";
    private const string ONSITECONTRACT = "Onsite Contract";

    #endregion Constants

    #region Members Variables

    string UserRaveDomainId;
    string UserMailId;

    /// <summary>
    /// Contains the list of roles
    /// </summary>
    ArrayList arrRolesForUser = new ArrayList();

    #endregion Members Variables

    #region Protected Events

    protected void Page_Init(object sender, EventArgs e)
    {
        //txtJoiningDate.Attributes.Add(ReadOnly, ReadOnly);
        txtReportingTo.Attributes.Add(ReadOnly, ReadOnly);
        //txtEmailID.Attributes.Add(ReadOnly,ReadOnly);
        txtWindowsUsername.Attributes.Add(ReadOnly, ReadOnly);

        txtReportingFM.Attributes.Add(ReadOnly, ReadOnly);
        //Umesh: Issue 'Modal Popup issue in chrome' Starts
        //imgEmpEmailPopulate.Attributes.Add("onclick", "return popUpEmployeeEmailPopulate();");
        //Umesh: Issue 'Modal Popup issue in chrome' Ends

        //imgReportingToPopulate.Attributes.Add("onclick", "return popUpEmployeeSearch();");
        //imgReportingFM.Attributes.Add("onclick", "return popUpFunctionalManagerSearch();");
        //imgWinowsUsername.Attributes.Add("onclick", "return popUpEmployeeWindowsUsernamePopulate();");
        #region JavaScript Function Call

        btnAdd.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return ButtonClickValidate();");

        txtEmployeeCode.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtEmployeeCode.ClientID + "','" + imgEmployeeCode.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_WITDSPACE + "');");
        imgEmployeeCode.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanzEmployeeCode.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgEmployeeCode.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanzEmployeeCode.ClientID + "');");

        txtFirstName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtFirstName.ClientID + "','" + imgFirstName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_WITDSPACE + "');");
        imgFirstName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanzFirstName.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
        imgFirstName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanzFirstName.ClientID + "');");

        // Ambar-26755-Start : Commented following lines
        // txtMiddleName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtMiddleName.ClientID + "','" + imgMiddleName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_WITDSPACE + "');");
        // imgMiddleName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanMiddleName.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        // imgMiddleName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanMiddleName.ClientID + "');");
        // Ambar-26755-End

        txtLastName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtLastName.ClientID + "','" + imgLastName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_WITDSPACE + "');");
        imgLastName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanzLastName.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
        imgLastName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanzLastName.ClientID + "');");
        // 26755-Ambar-Start
        // on focus clear the clipboard
        txtFirstName.Attributes.Add(Common.CommonConstants.EVENT_ONFOCUS, "javascript:window.clipboardData.clearData()");
        txtMiddleName.Attributes.Add(Common.CommonConstants.EVENT_ONFOCUS, "javascript:window.clipboardData.clearData()");  
        txtLastName.Attributes.Add(Common.CommonConstants.EVENT_ONFOCUS, "javascript:window.clipboardData.clearData()");
        // 26755-Ambar-End

        //txtEmailID.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtEmailID.ClientID + "','" + imgEmailIDspan.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_WITDSPACE + "');");
        imgEmailIDspan.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanzEmailID.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgEmailIDspan.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanzEmailID.ClientID + "');");

        //txtJoiningDate.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtJoiningDate.ClientID + "','" + imgJoiningDateError.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_WITDSPACE + "');");
        imgEmailIDspan.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanzJoiningDate.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgEmailIDspan.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanzJoiningDate.ClientID + "');");

        txtReportingTo.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtReportingTo.ClientID + "','" + imgReportingTo.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_WITDSPACE + "');");
        imgEmailIDspan.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanzReportingTo.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgEmailIDspan.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanzReportingTo.ClientID + "');");

        txtReportingFM.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtReportingFM.ClientID + "','" + imgcReportingFM.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_WITDSPACE + "');");
        imgcReportingFM.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanzReportingFM.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgcReportingFM.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanzReportingFM.ClientID + "');");


        ddlPrefix.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzPrefix.ClientID + "','','');");

        ddlMRFCode.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzMRFCode.ClientID + "','','');");

        ddlDepartment.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzDepartment.ClientID + "','','');");

        ddlBand.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzBand.ClientID + "','','');");

        ddlEmployeeType.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzEmployeeType.ClientID + "','','');");

        ddlDesignation.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzDesignation.ClientID + "','','');");

        #endregion
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!ValidateURL())
        {
            Response.Redirect(CommonConstants.INVALIDURL, false);
        }
        else
        {
            AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
            UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
            UserMailId = UserRaveDomainId.Replace("co.in", "com");
            AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
            //ArrayList arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());
            arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());

            if (!arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
            {
                Response.Redirect(CommonConstants.PAGE_HOME, true);
            }

            if (Page.IsPostBack != true)
            {
                imgEmp.ImageUrl = imagePath + BackSlash + NoImage;

                this.PopulateDropdownControls();
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddEmployeeData();
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedValue != CommonConstants.SELECT)
        {
            FillRoleDropdownAsPerDepartment();
        }
        else
        {
            ddlDesignation.Items.Clear();
            ddlDesignation.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
        }
    }

    protected void ddlMRFCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        string mrfid = string.Empty;
        Rave.HR.BusinessLayer.Recruitment.Recruitment recruitmentBL = new Rave.HR.BusinessLayer.Recruitment.Recruitment();
        BusinessEntities.Recruitment recruitment = null;
        mrfid = ddlMRFCode.SelectedItem.Value.ToString();
        if (mrfid != CommonConstants.SELECT)
        {
            recruitment = recruitmentBL.GetMrfDetailForEmployee(Convert.ToInt32(mrfid));

            if (recruitment != null)
            {
                ddlPrefix.SelectedValue = recruitment.PrefixId.ToString();
                txtFirstName.Text = recruitment.FirstName;
                txtMiddleName.Text = recruitment.MiddleName;
                txtLastName.Text = recruitment.LastName;
                ddlDepartment.SelectedValue = recruitment.DepartmentId.ToString();
                FillRoleDropdownAsPerDepartment();
                hidReportingTo.Value = recruitment.ReportingId;
                txtReportingTo.Text = recruitment.ReportingTo;
                ddlDesignation.SelectedValue = recruitment.DesignationId.ToString();
                //txtJoiningDate.Text = recruitment.ResourceJoinedDate == DateTime.MinValue ? string.Empty : recruitment.ResourceJoinedDate.ToShortDateString();
                ucDatePicker.Text = recruitment.ResourceJoinedDate == DateTime.MinValue ? string.Empty : recruitment.ResourceJoinedDate.ToShortDateString();
                ddlEmployeeType.SelectedValue = recruitment.EmployeeTypeId.ToString();
                ddlBand.SelectedValue = recruitment.BandId.ToString();
                ddlResourceBussinesUnit.SelectedValue = recruitment.ResourceBussinessUnit == 0 ? CommonConstants.SELECT : recruitment.ResourceBussinessUnit.ToString();
                txtReleventYears.Text = recruitment.RelevantExperienceYear.ToString();
                txtReleventMonths.Text = recruitment.RelavantExperienceMonth.ToString();
                //ddlLocation.SelectedItem.Text = recruitment.Location.ToString();
                ddlLocation.ClearSelection();
                ddlLocation.Items.FindByText(recruitment.Location.ToString().Trim()).Selected = true;

                if (ddlEmployeeType.SelectedItem.Text == ONSITECONTRACT)
                {
                    txtEmailID.Text = recruitment.CandidateEmailID.ToString();
                    //txtEmailID.Attributes.Add(ReadOnly, ReadOnly);
                    imgEmpEmailPopulate.Visible = false;
                }
                else
                {
                    txtEmailID.Text = "";
                    //txtEmailID.Attributes.Add(ReadOnly, ReadOnly);
                    imgEmpEmailPopulate.Visible = true;
                }
            }
        }
        else
        {
            ddlPrefix.SelectedValue = CommonConstants.SELECT;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            ddlDepartment.SelectedValue = CommonConstants.SELECT;
            GetEmployeeDesignations(0);
            hidReportingTo.Value = string.Empty;
            txtReportingTo.Text = string.Empty;
            ddlDesignation.SelectedValue = CommonConstants.SELECT;
            //txtJoiningDate.Text = string.Empty;
            ucDatePicker.Text = string.Empty;
            ddlEmployeeType.SelectedValue = CommonConstants.SELECT;
            ddlBand.SelectedValue = CommonConstants.SELECT;
            txtReleventMonths.Text = string.Empty;
            txtReleventYears.Text = string.Empty;
            ddlResourceBussinesUnit.SelectedValue = CommonConstants.SELECT;
            ddlLocation.SelectedValue = CommonConstants.SELECT;
        }

    }

    /// <summary>
    /// To upload the foto of an employee
    /// </summary>
    protected void imgbtnUpload_Click(object sender, ImageClickEventArgs e)
    {
        StringBuilder errMessage = new StringBuilder();
        string extension;
        string fullFileName;
        string appPath = ConfigurationSettings.AppSettings["ImagePhysicalPath"];
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
                throw ex;
            }
            catch (Exception ex)
            {
                RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "imgbtnUpload_Click", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
                LogErrorMessage(objEx);
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(CommonConstants.PAGE_HOME, false);
    }

    #endregion Protected Method

    #region Private Functions

    private void GetEmployeeBand()
    {
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        try
        {
            raveHrColl = master.FillDropDownsBL((int)Common.EnumsConstants.Category.EmployeeBand);

            ddlBand.Items.Clear();
            ddlBand.DataSource = raveHrColl;
            ddlBand.DataTextField = Common.CommonConstants.DDL_DataTextField;
            ddlBand.DataValueField = Common.CommonConstants.DDL_DataValueField;
            ddlBand.DataBind();
            ddlBand.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetEmployeeBand", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    private void GetEmployeeType()
    {
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        try
        {
            raveHrColl = master.FillDropDownsBL((int)Common.EnumsConstants.Category.EmployeeType);

            ddlEmployeeType.Items.Clear();
            ddlEmployeeType.DataSource = raveHrColl;
            ddlEmployeeType.DataTextField = Common.CommonConstants.DDL_DataTextField;
            ddlEmployeeType.DataValueField = Common.CommonConstants.DDL_DataValueField;
            ddlEmployeeType.DataBind();
            ddlEmployeeType.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetEmployeeType", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    private void GetEmployeeDepartment()
    {
        //Declaring COllection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        try
        {
            //Calling Fill dropdown Business layer method to fill 
            //the dropdown from Master class.
            raveHRCollection = master.FillDepartmentDropDownBL();

            ddlDepartment.Items.Clear();
            ddlDepartment.DataSource = raveHRCollection;
            ddlDepartment.DataTextField = Common.CommonConstants.DDL_DataTextField;
            ddlDepartment.DataValueField = Common.CommonConstants.DDL_DataValueField;
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

            //remove the Dept Name called RaveDevelopment from Dropdown -Vandna
            ddlDepartment.Items.Remove(ddlDepartment.Items.FindByValue(CommonConstants.DeptId_RaveDevelopment.ToString()));

            // 36837-Ambar-Start-28082012
            ddlDepartment.Items.Remove(ddlDepartment.Items.FindByValue(CommonConstants.DeptId_PRESALES_INDIA.ToString()));
            ddlDepartment.Items.Remove(ddlDepartment.Items.FindByValue(CommonConstants.DeptId_PRESALES_UK.ToString()));
            ddlDepartment.Items.Remove(ddlDepartment.Items.FindByValue(CommonConstants.DeptId_PRESALES_USA.ToString()));
            ddlDepartment.Items.Remove(ddlDepartment.Items.FindByValue(CommonConstants.DeptId_RaveForecastedProject.ToString()));
            // 36837-Ambar-End


        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetEmployeeDepartment", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    private void GetEmployeePrefix()
    {
        //Declaring COllection class object
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        try
        {
            //Calling Fill dropdown Business layer method to fill 
            //the dropdown from Master class.
            raveHrColl = master.FillDropDownsBL((int)Common.EnumsConstants.Category.Prefix);

            ddlPrefix.Items.Clear();
            ddlPrefix.DataSource = raveHrColl;
            ddlPrefix.DataTextField = Common.CommonConstants.DDL_DataTextField;
            ddlPrefix.DataValueField = Common.CommonConstants.DDL_DataValueField;
            ddlPrefix.DataBind();
            ddlPrefix.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetEmployeePrefix", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    private void GetMRFCode()
    {
        //Declaring COllection class object
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Employee.Employee employee = new Rave.HR.BusinessLayer.Employee.Employee();

        try
        {
            //Calling Fill dropdown Business layer method to fill 
            //the dropdown from Employee Business class.
            raveHrColl = employee.FillMRFCodeDropDowns();

            ddlMRFCode.Items.Clear();
            ddlMRFCode.DataSource = raveHrColl;
            ddlMRFCode.DataTextField = Common.CommonConstants.DDL_DataTextField;
            ddlMRFCode.DataValueField = Common.CommonConstants.DDL_DataValueField;
            ddlMRFCode.DataBind();
            ddlMRFCode.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetMRFCode", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    private void PopulateDropdownControls()
    {
        GetEmployeeBand();
        GetEmployeeType();
        //GetEmployeeStatus();
        GetEmployeeDepartment();
        GetEmployeeDesignations(0);
        GetEmployeePrefix();
        GetMRFCode();
        GetEmployeeBusinessUnit();
        GetEmployeeLocation();

        imgEmp.ImageUrl = imagePath + BackSlash + NoImage;

    }

    private void AddEmployeeData()
    {
        try
        {
            int empID = 0;
            bool isExist = false;
            string empCode = string.Empty;
            string fullName = string.Empty;
            string _userMailId = string.Empty;

            Rave.HR.BusinessLayer.Recruitment.Recruitment recruitmentBL = new Rave.HR.BusinessLayer.Recruitment.Recruitment();
            BusinessEntities.Recruitment recruitment = null;
            BusinessEntities.Employee addEmployee = new BusinessEntities.Employee();
            Rave.HR.BusinessLayer.Employee.Employee addEmployeeBAL = new Rave.HR.BusinessLayer.Employee.Employee();

            addEmployee.Prefix = Convert.ToInt32(ddlPrefix.SelectedItem.Value);
            addEmployee.EMPCode = txtEmployeeCode.Text.Trim();
            addEmployee.FirstName = txtFirstName.Text.Trim();
            addEmployee.MiddleName = txtMiddleName.Text.Trim();
            addEmployee.LastName = txtLastName.Text.Trim();
            fullName = txtFirstName.Text.Trim() + " " + txtLastName.Text.Trim();
            addEmployee.EmailId = txtEmailID.Text.Trim();
            addEmployee.GroupId = Convert.ToInt32(ddlDepartment.SelectedItem.Value);
            addEmployee.Band = Convert.ToInt32(ddlBand.SelectedItem.Value);
            //addEmployee.JoiningDate = Convert.ToDateTime(txtJoiningDate.Text.Trim());
            addEmployee.JoiningDate = Convert.ToDateTime(ucDatePicker.Text.Trim());
            addEmployee.StatusId = (int)MasterEnum.EmployeeStatus.Active;
            addEmployee.DesignationId = Convert.ToInt32(ddlDesignation.SelectedItem.Value);
            addEmployee.EmailId = txtEmailID.Text.Trim().ToLower();
            addEmployee.ReportingToId = hidReportingTo.Value.Trim().ToString();
            AuthorizationManager authoriseduser = new AuthorizationManager();
            string LoggedInUserMailId = authoriseduser.getLoggedInUser();
            _userMailId = LoggedInUserMailId.Replace("co.in", "com");
            addEmployee.CreatedByMailId = _userMailId;
            addEmployee.CreatedDate = DateTime.Today;
            addEmployee.LastModifiedByMailId = _userMailId;
            addEmployee.LastModifiedDate = DateTime.Today;
            addEmployee.Type = Convert.ToInt32(ddlEmployeeType.SelectedItem.Value);
            addEmployee.CreatedDate = DateTime.Now;
            addEmployee.MRFId = Convert.ToInt32(ddlMRFCode.SelectedItem.Value);
            string imgName = Path.GetFileName(imgEmp.ImageUrl);
            addEmployee.FileName = imgName;
            addEmployee.RelavantExperienceMonth = Convert.ToInt32(txtReleventMonths.Text);
            addEmployee.RelevantExperienceYear = Convert.ToInt32(txtReleventYears.Text);
            addEmployee.ResourceBussinessUnit = Convert.ToInt32(ddlResourceBussinesUnit.SelectedValue);
            addEmployee.ReportingToFMId = Convert.ToInt32(hidReportingToFM.Value.Trim().ToString());
            addEmployee.Department = Convert.ToString(ddlDepartment.SelectedItem);
            addEmployee.Designation = Convert.ToString(ddlDesignation.SelectedItem);
            addEmployee.EmployeeType = Convert.ToString(ddlEmployeeType.SelectedItem);
            addEmployee.MRFcode = ddlMRFCode.SelectedItem.Text;
            recruitment = recruitmentBL.GetMrfDetailForEmployee(Convert.ToInt32(addEmployee.MRFId));
            addEmployee.Location = recruitment.Location;
            addEmployee.EmpLocation = (ddlLocation.SelectedItem.Text).Trim().ToString();

            //going google Mahendra 26-Jun-2013
            addEmployee.WindowsUserName = txtWindowsUsername.Text.Trim().ToString();
            


            int dataCount = addEmployeeBAL.IsEmployeeDataExists(addEmployee);

            //Mohamed : Issue 41065 : 05/04/2013 : Starts                        			  
            //Desc :  "When X-Employee rejoin then its previous email id should be change and the same email id should be inserted" -- Code Changes in Sp 
            //if (dataCount == 1)
            //{
            //    lblMessage.Text = "Emailid " + addEmployee.EmailId + " and Emplyee Code " + addEmployee.EMPCode + " already exists in the database.";
            //    return;
            //}
            //if (dataCount == 2)
            //{
            //    lblMessage.Text = "Emailid " + addEmployee.EmailId + " already exists in the database.";
            //    return;
            //}
            if (dataCount == 4)
            {
                lblMessage.Text = "Emailid " + addEmployee.EmailId + " is already assign to existing user.";
                return;
            }
            //Mohamed : Issue 41065 : 05/04/2013 : Ends
            if (dataCount == 3)
            {
                lblMessage.Text = "Employee Code " + addEmployee.EMPCode + " already exists in the database.";
                return;
            }

            empID = addEmployeeBAL.AddEmployee(addEmployee, ref empCode);
            if (empID > 0)
            {
                addEmployee.EMPId = empID;
                addEmployeeBAL.UpdateEmployeeMRFCode(addEmployee, (int)Common.MasterEnum.MRFStatus.PendingNewEmployeeAllocation);
            }
            else
            {
                return;
            }
            //--Get mailIds
            addEmployeeBAL.SendMailForAddEmployee(addEmployee);

            //for sending mail to the added employee to update his/her details
            //Only Rave domain employee should get this mail.
            //if (addEmployee.EmailId.Contains(RAVE_DOMAIN))
            if (addEmployee.EmployeeType != ONSITECONTRACT)
            {
                addEmployeeBAL.SendMailForAddEmployeeToUpdateDetails(addEmployee);
            }

            //Refreshing the data after inserting it.
            this.RefreshControls();

            lblConfirmationMessage.Text = "Employee: " + fullName + " added successfully with Employee Code: " + empCode;
            lblMessage.Text = string.Empty;
            imgEmp.ImageUrl = imagePath + BackSlash + NoImage;
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "AddEmployee", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }
    private void RefreshControls()
    {
        this.PopulateDropdownControls();

        txtEmployeeCode.Text = string.Empty;
        txtFirstName.Text = string.Empty;
        txtMiddleName.Text = string.Empty;
        txtLastName.Text = string.Empty;
        hidReportingTo.Value = string.Empty;
        txtReportingTo.Text = string.Empty;
        //txtJoiningDate.Text = string.Empty;
        ucDatePicker.Text = string.Empty;
        txtEmailID.Text = string.Empty;
        imgEmp.ImageUrl = string.Empty;
        txtReleventYears.Text = string.Empty;
        txtReleventMonths.Text = string.Empty;
        txtReportingFM.Text = string.Empty;
        txtWindowsUsername.Text = string.Empty;
    }

    /// <summary>
    /// Based on the Department selected, fill the Role 
    /// </summary>
    private void FillRoleDropdownAsPerDepartment()
    {
        GetEmployeeDesignations(int.Parse(ddlDepartment.SelectedValue));
    }

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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetEmployeeType", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }


    private void GetEmployeeLocation()
    {
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        try
        {
            raveHrColl = master.FillDropDownsBL((int)Common.EnumsConstants.Category.CandidateLocation);

            ddlLocation.ClearSelection();
            //ddlLocation.Items.Clear();
            ddlLocation.DataSource = raveHrColl;
            ddlLocation.DataTextField = Common.CommonConstants.DDL_DataTextField;
            ddlLocation.DataValueField = Common.CommonConstants.DDL_DataValueField;
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetEmployeeLocation", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    protected void ddlEmployeeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedItem.Text.Contains("RaveConsultant-UK")
           || ddlDepartment.SelectedItem.Text.Contains("RaveConsultant-USA")
           || ddlDepartment.SelectedItem.Text.Contains("RaveConsultant-India"))
        {
            if (ddlEmployeeType.SelectedValue == Convert.ToInt32(MasterEnum.EmployeeType.ASE).ToString()
              || ddlEmployeeType.SelectedValue == Convert.ToInt32(MasterEnum.EmployeeType.Permanent).ToString()
              || ddlEmployeeType.SelectedValue == Convert.ToInt32(MasterEnum.EmployeeType.Contract).ToString())
            {
                imgEmpEmailPopulate.Visible = true;
            }
            else
            {
                imgEmpEmailPopulate.Visible = false;
            }
        }
    }

    #endregion Private Functions


}
