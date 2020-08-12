using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Common;
using Common.AuthorizationManager;
using System.IO;
using System.Text;
using System.Data;
using Common.Constants;

public partial class Employee : BaseClass
{
    #region Private Field Members

    BusinessEntities.Employee employee = new BusinessEntities.Employee();
    ArrayList arrRolesForUser = new ArrayList();

    string UserRaveDomainId;
    string UserMailId;
    const string ReadOnly = "readonly";
    char[] SPILITER_SLASH = { '/' };
    const string imagePath = @"~\Images";
    const string BackSlash = @"\";
    const string IMAGES = "Images";
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

    #endregion Properties

    #region Protected Methods

    protected void Page_Init(object sender, EventArgs e)
    {
        txtJoiningDate.Attributes.Add(ReadOnly, ReadOnly);
        txtResignationDate.Attributes.Add(ReadOnly, ReadOnly);
        txtLastWorkDay.Attributes.Add(ReadOnly, ReadOnly);
        txtEmployeeCode.Attributes.Add(ReadOnly, ReadOnly);
        txtDOB.Attributes.Add(ReadOnly, ReadOnly);
        txtEmailID.Attributes.Add(ReadOnly, ReadOnly);
        txtReportingTo.Attributes.Add(ReadOnly, ReadOnly);
        imgEmpEmailPopulate.Attributes.Add("onclick", "return popUpEmployeeEmailPopulate();");
        imgReportingToPopulate.Attributes.Add("onclick", "return popUpEmployeeSearch();");
    }

    protected void Page_Load(object sender, EventArgs e)
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

            btnUpdate.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return ButtonClickValidate();");

            txtFirstName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtFirstName.ClientID + "','" + imgFirstName.ClientID + "','" + Common.CommonConstants.VALIDATE_ISALPHABET + "');");
            imgFirstName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanFirstName.ClientID + "','" + Common.CommonConstants.MSG_ALPHABET + "');");
            imgFirstName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanFirstName.ClientID + "');");

            txtLastName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtLastName.ClientID + "','" + imgLastName.ClientID + "','" + Common.CommonConstants.VALIDATE_ISALPHABET + "');");
            imgLastName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanLastName.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
            imgLastName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanLastName.ClientID + "');");

            txtResignationReason.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtResignationReason.ClientID + "','" + imgResignationReason.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_FUNCTION + "');");
            imgResignationReason.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanResignationReason.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
            imgResignationReason.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanResignationReason.ClientID + "');");

            txtFatherName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtFatherName.ClientID + "','" + imgFatherName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPACE + "');");
            imgFatherName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanFatherName.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
            imgFatherName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanFatherName.ClientID + "');");


            txtSpouseName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtSpouseName.ClientID + "','" + imgSpouseName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPACE + "');");
            imgSpouseName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanSpouseName.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
            imgSpouseName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanSpouseName.ClientID + "');");


            txtCurrentAddress.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtCurrentAddress.ClientID + "','" + imgCurrentAddress.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_WITDSPACE + "');");
            imgCurrentAddress.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanCurrentAddress.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
            imgCurrentAddress.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanCurrentAddress.ClientID + "');");

            txtCStreetName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtCStreetName.ClientID + "','" + imgCStreetName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_FUNCTION + "');");
            imgCStreetName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanCStreetName.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
            imgCStreetName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanCStreetName.ClientID + "');");

            txtCCity.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtCCity.ClientID + "','" + imgCCity.ClientID + "','" + Common.CommonConstants.VALIDATE_ISALPHABET + "');");
            imgCCity.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanCCity.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
            imgCCity.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanCCity.ClientID + "');");

            txtCPinCode.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtCPinCode.ClientID + "','" + imgCPinCode.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_FUNCTION + "');");
            imgCPinCode.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanCPinCode.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
            imgCPinCode.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanCPinCode.ClientID + "');");


            txtPhone.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtPhone.ClientID + "','" + imgPhone.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
            imgPhone.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanPhone.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
            imgPhone.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanPhone.ClientID + "');");

            txtMobile.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtMobile.ClientID + "','" + imgMobile.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
            imgMobile.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanMobile.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
            imgMobile.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanMobile.ClientID + "');");

            txtEmergencyContactNo.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtEmergencyContactNo.ClientID + "','" + imgEmergencyContactNo.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
            imgEmergencyContactNo.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanEmergencyContactNo.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
            imgEmergencyContactNo.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanEmergencyContactNo.ClientID + "');");

            txtPAddress.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtPAddress.ClientID + "','" + imgPAddress.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_WITDSPACE + "');");
            imgPAddress.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanPAddress.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
            imgPAddress.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanPAddress.ClientID + "');");

            txtPStreetName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtPStreetName.ClientID + "','" + imgPStreetName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_FUNCTION + "');");
            imgPStreetName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanPStreetName.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
            imgPStreetName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanPStreetName.ClientID + "');");

            txtPCity.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtPCity.ClientID + "','" + imgPCity.ClientID + "','" + Common.CommonConstants.VALIDATE_ISALPHABET + "');");
            imgPCity.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanPCity.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
            imgPCity.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanPCity.ClientID + "');");

            txtPPinCode.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtPPinCode.ClientID + "','" + imgPPinCode.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_FUNCTION + "');");
            imgPPinCode.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanPPinCode.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
            imgPPinCode.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanPPinCode.ClientID + "');");

            Rave.HR.BusinessLayer.MRF.MRFRoles mrfRoles = new Rave.HR.BusinessLayer.MRF.MRFRoles();
            // Get logged-in user's email id
            AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
            UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
            UserMailId = UserRaveDomainId.Replace("co.in", "com");
            AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
            //ArrayList arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());
            arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());

            if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM))
            {
                btnRelease.Visible = true;
                btnDelete.Visible = true;
            }

            if (!IsPostBack)
            {
                //Function will Set MRF Index
                this.SetEmployeeIndex();

                Session[SessionNames.PAGEMODE] = Common.MasterEnum.PageModeEnum.View;

                this.PopulateDropdownControls();
                Session[SessionNames.EMPLOYEEDETAILS] = null;

                //IF EMPId is not null
                if (Request.QueryString[CommonConstants.EMP_ID] != null)
                {
                    employee.EMPId = Convert.ToInt32(DecryptQueryString(QueryStringConstants.EMPID).ToString());
                    Session[SessionNames.EMPLOYEEDETAILS] = employee;
                    btnRelease.Attributes["onclick"] = "popUpEmployeeReleasePopulate('" + employee.EMPId + "')";

                }
                else
                {
                    btnRelease.Visible = false;
                }
                this.PopulateControls();
            }



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

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Session[SessionNames.PAGEMODE] = Common.MasterEnum.PageModeEnum.Edit;
        btnUpdate.Visible = true;
        btnCancel.Visible = true;
        btnEdit.Visible = false;

        //if (objEmergencyDetailsCollection.Count != 0)
        //{
        //    gvEmergencyDetails.DataSource = objEmergencyDetailsCollection;
        //    gvEmergencyDetails.DataBind();
        //}
        this.EnableDisableControls();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateControls())
            {
                Rave.HR.BusinessLayer.Employee.EmergencyContact objEmergencyContactBL = new Rave.HR.BusinessLayer.Employee.EmergencyContact();
                BusinessEntities.EmergencyContact objEmergencyContact = null;
                BusinessEntities.RaveHRCollection objSaveEmergencyDetailsCollection = new BusinessEntities.RaveHRCollection();

                employee = (BusinessEntities.Employee)Session[Common.SessionNames.EMPLOYEEDETAILS];

                if (employee != null)
                {
                    Rave.HR.BusinessLayer.Employee.Employee addEmployeeBAL = new Rave.HR.BusinessLayer.Employee.Employee();
                    employee.Prefix = Convert.ToInt32(ddlPrefix.SelectedItem.Value);
                    employee.FirstName = txtFirstName.Text.Trim();
                    employee.LastName = txtLastName.Text.Trim();
                    employee.EmailId = txtEmailID.Text.Trim();
                    employee.GroupId = Convert.ToInt32(ddlDepartment.SelectedItem.Value);
                    employee.Band = Convert.ToInt32(ddlBand.SelectedItem.Value);
                    employee.JoiningDate = Convert.ToDateTime(txtJoiningDate.Text.Trim());
                    employee.StatusId = (int)MasterEnum.EmployeeStatus.Active;
                    employee.DesignationId = Convert.ToInt32(ddlDesignation.SelectedItem.Value);
                    employee.EmailId = txtEmailID.Text.Trim();
                    employee.ReportingToId = hidReportingTo.Value.ToString();
                    AuthorizationManager authoriseduser = new AuthorizationManager();
                    string LoggedInUserMailId = authoriseduser.getLoggedInUser();
                    employee.Type = Convert.ToInt32(ddlEmployeeType.SelectedItem.Value);
                    employee.LastWorkingDay = txtLastWorkDay.Text.Trim() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(txtLastWorkDay.Text);
                    employee.ResignationDate = txtResignationDate.Text.Trim() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(txtResignationDate.Text);
                    employee.ResignationReason = txtResignationReason.Text.Trim();

                    employee.DOB = txtDOB.Text.Trim() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(txtDOB.Text);
                    employee.Gender = ddlGender.SelectedItem.Value.Trim();
                    employee.MaritalStatus = ddlMaritalStatus.SelectedItem.Value.Trim();
                    employee.FatherName = txtFatherName.Text.Trim();

                    employee.BloodGroup = txtBloodGroup.Text.Trim();
                    employee.CAddress = txtCurrentAddress.Text.Trim();
                    employee.CStreet = txtCStreetName.Text.Trim();
                    employee.CCity = txtCCity.Text.Trim();
                    employee.CPinCode = txtCPinCode.Text.Trim();
                    employee.ResidencePhone = txtPhone.Text;
                    employee.EmergencyContactNo = txtEmergencyContactNo.Text.Trim();
                    employee.MobileNo = txtMobile.Text;
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

                    //If perment & current address differs
                    if (rblSamePerAddress.SelectedIndex == CommonConstants.ONE)
                    {
                        employee.PAddress = txtPAddress.Text.Trim();
                        employee.PStreet = txtPStreetName.Text.Trim();
                        employee.PCity = txtPCity.Text.Trim();
                        employee.PPinCode = txtPPinCode.Text.Trim();
                    }
                    else
                    {
                        employee.PAddress = string.Empty;
                        employee.PStreet = string.Empty;
                        employee.PCity = string.Empty;
                        employee.PPinCode = string.Empty;

                    }
                    string filePath = string.Empty;
                    filePath = Path.GetFileName(imgEmp.ImageUrl);
                    employee.FileName = filePath;
                    Boolean flag=false;

                    addEmployeeBAL.UpdateEmployee(employee, flag);

                    #region Commented code

                    //for (int i = 0; i < gvEmergencyDetails.Rows.Count; i++)
                    //{
                    //    TextBox PersonName = (TextBox)gvEmergencyDetails.Rows[i].FindControl("txtName");
                    //    TextBox Relation = (TextBox)gvEmergencyDetails.Rows[i].FindControl("txtRelation");
                    //    TextBox ContactNo = (TextBox)gvEmergencyDetails.Rows[i].FindControl("txtContactNo");
                    //    objEmergencyContact = new BusinessEntities.EmergencyContact();

                    //    if (employee != null)
                    //    {
                    //        objEmergencyContact.EMPId = employee.EMPId;
                    //    }
                    //    if (!string.IsNullOrEmpty(PersonName.Text.Trim()))
                    //    {
                    //        objEmergencyContact.ContactName = PersonName.Text.Trim();
                    //    }
                    //    if (!string.IsNullOrEmpty(Relation.Text.Trim()))
                    //    {
                    //        objEmergencyContact.Relation = Relation.Text.Trim();
                    //    }
                    //    if (ContactNo.Text.Trim()!="0")
                    //    {
                    //        objEmergencyContact.ContactNumber = ContactNo.Text.Trim();
                    //    }
                    //    //objSaveEmergencyDetailsCollection.Add(objEmergencyContact);

                    //    objEmergencyContactBL.AddEmergencyContact(objEmergencyContact);

                    //}
                    #endregion

                    lblConfirmMsg.Text = "Employee Details updated successfully.";
                }
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnUpdate_Click", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    protected void rblSamePerAddress_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblSamePerAddress.SelectedItem.Value == "True")
        {
            pnlPermanenetAddr.Visible = false;
        }
        else
        {
            pnlPermanenetAddr.Visible = true;
            pnlPermanenetAddr.Enabled = true;

            if (txtPAddress.Text != string.Empty && txtPStreetName.Text != string.Empty && txtPPinCode.Text != string.Empty && txtPCity.Text != string.Empty && !arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
            {
                txtPAddress.Enabled = false;
                txtPStreetName.Enabled = false;
                txtPCity.Enabled = false;
                txtPPinCode.Enabled = false;
            }
        }
    }

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
                string fileName = BackSlash + Guid.NewGuid() + extension;
                //Makes a Full name of the image with the path where the image will be saved
                fullFileName = Server.MapPath(IMAGES) + fileName;
                //saves the image file
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

    protected void btnResume_Click(object sender, EventArgs e)
    {
        Response.Redirect(CommonConstants.PAGE_GENERATERESUME);
    }

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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(CommonConstants.PAGE_HOME);
    }

    protected void ddlMaritalStatus_SelectedIndexChanged(object sender, EventArgs e)
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
                string strUserEamilIdsInRole = authoriseduser.getUserEmailIdInRoles(AuthorizationManagerConstants.ROLECOO);
                strUserEamilIdsInRole += authoriseduser.getUserEmailIdInRoles(AuthorizationManagerConstants.ROLERPM);
                Response.Redirect(CommonConstants.PAGE_HOME, false);
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnDelete_Click", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
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
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetEmployee", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

        return employee;
    }

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
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetEmployee", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

        return employee;
    }

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

    private void GetEmployeeStatus()
    {
        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        //Declaring COllection class object
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        //Calling Fill dropdown Business layer method to fill 
        //the dropdown from Master class.
        raveHrColl = master.FillDropDownsBL((int)Common.EnumsConstants.Category.EmployeeStatus);

        ddlStatus.DataSource = raveHrColl;
        ddlStatus.DataTextField = Common.CommonConstants.DDL_DataTextField;
        ddlStatus.DataValueField = Common.CommonConstants.DDL_DataValueField;
        ddlStatus.DataBind();
        ddlStatus.Items.Insert(0, CommonConstants.SELECT);

    }

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

    private void PopulateControls()
    {
        BusinessEntities.Employee employee = new BusinessEntities.Employee();

        try
        {
            if (Session[SessionNames.EMPLOYEEDETAILS] != null)
            {
                employee = (BusinessEntities.Employee)Session[SessionNames.EMPLOYEEDETAILS];
                employee = this.GetEmployee(employee);
            }
            else
            {
                if (UserMailId != string.Empty && UserMailId != null)
                    employee = this.GetEmployee(UserMailId);
            }
            Session[Common.SessionNames.EMPLOYEEDETAILS] = employee;

            if (employee == null)
            {
                Response.Redirect(CommonConstants.PAGE_HOME, false);
                return;
            }
            //BusinessEntities.Employee employee = new BusinessEntities.Employee();
            //employee = (BusinessEntities.Employee)Session[SessionNames.EMPLOYEEDETAILS];


            if (UserMailId.ToLower() == employee.EmailId.ToLower())
            {
                if (arrRolesForUser.Count > 0)
                {
                    btnEdit.Visible = true;
                }
            }
            else
            {
                if (arrRolesForUser.Count > 0)
                {
                    //if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
                    //{
                    btnEdit.Visible = true;
                    //}
                }
            }

            if (employee != null)
            {
                ddlPrefix.SelectedValue = employee.Prefix.ToString();
                txtEmployeeCode.Text = employee.EMPCode;
                txtFirstName.Text = employee.FirstName;
                txtLastName.Text = employee.LastName;
                txtEmailID.Text = employee.EmailId;
                ddlDepartment.SelectedValue = employee.GroupId.ToString();
                FillRoleDropdownAsPerDepartment();
                ddlEmployeeType.SelectedValue = employee.Type.ToString();
                ddlBand.SelectedValue = employee.Band.ToString();
                ddlDesignation.SelectedValue = employee.DesignationId.ToString();
                txtJoiningDate.Text = employee.JoiningDate == DateTime.MinValue ? string.Empty : employee.JoiningDate.ToShortDateString();
                hidReportingTo.Value = employee.ReportingToId;
                txtReportingTo.Text = employee.ReportingTo;
                ddlStatus.SelectedValue = employee.StatusId.ToString();
                txtLastWorkDay.Text = employee.LastWorkingDay == DateTime.MinValue ? string.Empty : employee.LastWorkingDay.ToShortDateString();
                txtResignationDate.Text = employee.ResignationDate == DateTime.MinValue ? string.Empty : employee.ResignationDate.ToShortDateString();
                txtResignationReason.Text = employee.ResignationReason;
                //image
                string image1 = employee.FileName;

                imgEmp.ImageUrl = imagePath + BackSlash + image1;

                txtDOB.Text = employee.DOB == DateTime.MinValue ? string.Empty : employee.DOB.ToShortDateString();
                ddlGender.SelectedValue = employee.Gender.Trim() == string.Empty ? CommonConstants.SELECT : employee.Gender.Trim();
                ddlMaritalStatus.SelectedValue = employee.MaritalStatus.Trim() == string.Empty ? CommonConstants.SELECT : employee.MaritalStatus.Trim();
                txtFatherName.Text = employee.FatherName;
                txtSpouseName.Text = employee.SpouseName;
                txtBloodGroup.Text = employee.BloodGroup;
                txtCurrentAddress.Text = employee.CAddress;
                txtCStreetName.Text = employee.CStreet;
                txtCCity.Text = employee.CCity;
                txtCPinCode.Text = employee.CPinCode;
                txtPhone.Text = employee.ResidencePhone;
                txtMobile.Text = employee.MobileNo;
                txtEmergencyContactNo.Text = employee.EmergencyContactNo;
                ddlMaritalStatus_SelectedIndexChanged(null, null);

                if (employee.PAddress != string.Empty)
                {
                    rblSamePerAddress.SelectedIndex = CommonConstants.ONE;
                    txtPAddress.Text = employee.PAddress;
                    txtPCity.Text = employee.PCity;
                    txtPPinCode.Text = employee.PPinCode;
                    txtPStreetName.Text = employee.PStreet;
                    pnlPermanenetAddr.Visible = true;
                }
                else
                {
                    rblSamePerAddress.SelectedIndex = CommonConstants.ZERO;
                    pnlPermanenetAddr.Visible = false;
                }

                if (employee.IsFresher == CommonConstants.ONE)
                    rblIsFresher.SelectedIndex = CommonConstants.ZERO;
                else
                    rblIsFresher.SelectedIndex = CommonConstants.ONE;

                Rave.HR.BusinessLayer.Employee.EmergencyContact objEmergencyContactBL = new Rave.HR.BusinessLayer.Employee.EmergencyContact();

                //if (objEmergencyDetailsCollection.Count != 0)
                //{
                //    gvEmergencyDetails.DataSource = objEmergencyDetailsCollection;
                //    gvEmergencyDetails.DataBind();
                //}
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "PopulateControls", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    private void EnableDisableControls()
    {
        BusinessEntities.Employee employee = new BusinessEntities.Employee();
        employee = (BusinessEntities.Employee)Session[SessionNames.EMPLOYEEDETAILS];
        if (UserMailId.ToLower() == employee.EmailId.ToLower())
        {
            if (arrRolesForUser.Count > 0)
            {
                pnlEmployeeDetails.Enabled = false;
                pnlEmpPersonalDetails.Enabled = true;
                imgEmpEmailPopulate.Disabled = true;
                imgReportingToPopulate.Disabled = true;
                //divEmergencyDetails.Disabled = false;

                if (txtCurrentAddress.Text != string.Empty && txtCStreetName.Text != string.Empty && txtCPinCode.Text != string.Empty && txtCCity.Text != string.Empty && !arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
                {
                    txtCurrentAddress.Enabled = false;
                    txtCStreetName.Enabled = false;
                    txtCCity.Enabled = false;
                    txtCPinCode.Enabled = false;
                }

                if (rblSamePerAddress.SelectedIndex == CommonConstants.ONE)
                {
                    if (txtPAddress.Text != string.Empty && txtPStreetName.Text != string.Empty && txtPPinCode.Text != string.Empty && txtPCity.Text != string.Empty && !arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
                    {
                        pnlPermanenetAddr.Enabled = true;
                        txtPAddress.Enabled = false;
                        txtPStreetName.Enabled = false;
                        txtPCity.Enabled = false;
                        txtPPinCode.Enabled = false;
                    }
                    else
                    {
                        pnlPermanenetAddr.Enabled = true;
                    }
                }
                if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
                {
                    pnlEmployeeDetails.Enabled = true;
                    //pnlEmpPersonalDetails.Enabled = true;
                    imgEmpEmailPopulate.Disabled = false;
                    imgReportingToPopulate.Disabled = false;

                }

            }
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

                if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
                {
                    pnlEmployeeDetails.Enabled = true;
                    pnlEmpPersonalDetails.Enabled = true;
                    imgEmpEmailPopulate.Disabled = false;
                    imgReportingToPopulate.Disabled = false;

                    if (rblSamePerAddress.SelectedIndex == CommonConstants.ONE)
                        pnlPermanenetAddr.Enabled = true;
                }
                else
                {
                    btnUpdate.Visible = false;
                }
            }
        }
    }

    private void EnableControl()
    {
        pnlEmployeeDetails.Enabled = false;
        pnlEmpPersonalDetails.Enabled = false;
        imgEmpEmailPopulate.Disabled = true;
        imgReportingToPopulate.Disabled = true;
        pnlPermanenetAddr.Enabled = false;
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
            throw ex;
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

        if (!string.IsNullOrEmpty(txtJoiningDate.Text) && !string.IsNullOrEmpty(txtDOB.Text))
        {
            joiningDateArr = Convert.ToString(txtJoiningDate.Text).Split(SPILITER_SLASH);
            DateTime joiningDate = new DateTime(Convert.ToInt32(joiningDateArr[2]), Convert.ToInt32(joiningDateArr[1]), Convert.ToInt32(joiningDateArr[0]));

            DateofBirthArr = Convert.ToString(txtDOB.Text).Split(SPILITER_SLASH);
            DateTime DateofBirth = new DateTime(Convert.ToInt32(DateofBirthArr[2]), Convert.ToInt32(DateofBirthArr[1]), Convert.ToInt32(DateofBirthArr[0]));

            if (joiningDate > DateTime.Now.Date)
            {
                errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.JOINING_DATE_VALIDATION);
                flag = false;
            }

            if (DateofBirth > DateTime.Now.Date)
            {
                errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.BIRTH_DATE_VALIDATION);
                flag = false;
            }
        }

        if (!string.IsNullOrEmpty(txtResignationDate.Text) && !string.IsNullOrEmpty(txtLastWorkDay.Text))
        {
            ResignationDateArr = Convert.ToString(txtResignationDate.Text).Split(SPILITER_SLASH);
            DateTime ResignationDate = new DateTime(Convert.ToInt32(ResignationDateArr[2]), Convert.ToInt32(ResignationDateArr[1]), Convert.ToInt32(ResignationDateArr[0]));

            LastWorkDayArr = Convert.ToString(txtLastWorkDay.Text).Split(SPILITER_SLASH);
            DateTime LastWorkDay = new DateTime(Convert.ToInt32(LastWorkDayArr[2]), Convert.ToInt32(LastWorkDayArr[1]), Convert.ToInt32(LastWorkDayArr[0]));

            if (ResignationDate > DateTime.Now.Date)
            {
                errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.RESIGNATION_DATE_VALIDATION);
                flag = false;
            }

            if (LastWorkDay > DateTime.Now.Date)
            {
                errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.LASTWORKING_DAY_VALIDATION);
                flag = false;
            }
        }

        if (string.IsNullOrEmpty(txtPhone.Text.Trim()) && string.IsNullOrEmpty(txtMobile.Text.Trim()))
        {

            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.PHONEMOBILE_VALIDATION);
            flag = false;
        }

        lblMessage.Text = errMessage.ToString();
        return flag;
    }


    private void GetEmployeeDesignations(int categoryID)
    {
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        raveHrColl = master.FillDropDownsBL(categoryID);

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
        if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Projects))
        {
            GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.ProjectRole));
        }
        if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Admin))
        {
            GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.AdminRole));
        }
        if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Finance))
        {
            GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.FinanceRole));
        }
        if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.HR))
        {
            GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.HRRole));
        }
        if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.ITS))
        {
            GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.ITSRole));
        }
        if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Marketing))
        {
            GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.MarketingRole));
        }
        if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.PMOQuality))
        {
            GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.PMOQualityRole));
        }
        if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.PreSales))
        {
            GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.PreSalesRole));
        }
        if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.RaveDevelopment))
        {
            GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.RaveDevelopmentRole));
        }
        if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Support))
        {
            ddlDesignation.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
        }
        if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Testing))
        {
            GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.TestingRole));
        }
        if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Recruitment))
        {
            GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.RecruitmentRole));
        }
    }

    private void PopulateDropdownControls()
    {
        GetEmployeeDesignations(0);
        GetEmployeeBand();
        GetEmployeeType();
        GetEmployeeStatus();
        GetEmployeePrefix();
        GetEmployeeDepartment();
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
            //Googleconfigurable
            string strUser = "";
            //if (strFromUser.ToLower().Trim().Contains("@rave-tech.com"))
            //{
            //    strUser = strFromUser.ToLower().Replace("@rave-tech.com", "");
            //}
            //else
            //{
            //    strUser = strFromUser.ToLower().Replace("@" + AuthorizationManagerConstants.NORTHGATEDOMAINEMAIL, "");
            //}

            AuthorizationManager obj = new AuthorizationManager();
            strUser = obj.GetUsernameBasedOnEmail(strFromUser);
            

            strMessageBody.Append("Hello," + "</br>"
                + "This is to bring to your notice that " + empname + " from department" + departmentname + " is deleted from" + "</br>"
                + "the Resource Management System. " + "</br>" +
                //"</br>" + "</br>" + "Regards," + "</br>" + strFromUser.Replace(RAVE_DOMAIN, "") + "</br>");
                "</br>" + "</br>" + "Regards," + "</br>" + strUser + "</br>");

            htmlFormBody.Style.Add(HtmlTextWriterStyle.FontFamily, "5");
            htmlFormBody.Style.Add(HtmlTextWriterStyle.FontWeight, "10");
            htmlFormBody.InnerText = strMessageBody.ToString();
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
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

    #endregion Private Method


}
