using System;
using System.Collections;
using System.Web.UI.WebControls;
using Common;
using Common.AuthorizationManager;
using System.Text.RegularExpressions;

public partial class EmpContactDetails : BaseClass
{

    #region Private Field Members

    private string CONTACTTYPE = "contacttype";
    private int employeeID = 0;
    private BusinessEntities.Employee employee;
    private string CLASS_NAME = "EmpContactDetails";
    private string IMGBTNDELETE = "imgBtnDelete";
    private string IMGBTNEDIT = "imgBtnEdit";
    private string MODE = "Mode";
    string PageMode = string.Empty;
    string UserRaveDomainId=string.Empty;
    string UserMailId=string.Empty;
    ArrayList arrRolesForUser = new ArrayList();
    private string EMPRELATION = "EmployeeRelation";
    Regex regexObj = null;
    private static bool Flag = false;

    #region ViewState Constants

    private string CONTACTDETAILS = "ContactDetails";
    private string CONTACTDETAILSDELETE = "ContactDetailsDelete";
    private string DELETEROWINDEX = "DeleteRowIndex";
    private string ROWINDEX = "RowIndex";
    private string EMERGENCYDETAILS = "EmergencyDetails";
    private string EMERGENCYCONTACTDELETE = "EmergencyContactDelete";
    private string EMRDELETEROWINDEX = "EmrDeleteRowIndex";
    private string EMRROWINDEX = "EmrRowIndex";

    #endregion ViewState Constants

    #endregion

    #region Local Properties

    /// <summary>
    /// Gets or sets the contact details collection.
    /// </summary>
    /// <value>The contact details collection.</value>
    private BusinessEntities.RaveHRCollection ContactdetailsCollection
    {
        get
        {
            if (ViewState[CONTACTDETAILS] != null)
                return ((BusinessEntities.RaveHRCollection)ViewState[CONTACTDETAILS]);
            else
                return null;
        }
        set
        {
            ViewState[CONTACTDETAILS] = value;
        }
    }

    /// <summary>
    /// Gets or sets the contact type collection.
    /// </summary>
    /// <value>The contact type collection.</value>
    private BusinessEntities.RaveHRCollection ContactTypeCollection
    {
        get
        {
            if (ViewState[CONTACTTYPE] != null)
                return ((BusinessEntities.RaveHRCollection)ViewState[CONTACTTYPE]);
            else
                return null;
        }
        set
        {
            ViewState[CONTACTTYPE] = value;
        }
    }

    /// <summary>
    /// Gets or sets the emergencydetails collection.
    /// </summary>
    /// <value>The emergencydetails collection.</value>
    private BusinessEntities.RaveHRCollection EmergencydetailsCollection
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
    /// Gets or sets the employee relation collection.
    /// </summary>
    /// <value>The employee relation collection.</value>
    private BusinessEntities.RaveHRCollection EmployeeRelationCollection
    {
        get
        {
            if (ViewState[EMPRELATION] != null)
                return ((BusinessEntities.RaveHRCollection)ViewState[EMPRELATION]);
            else
                return null;
        }
        set
        {
            ViewState[EMPRELATION] = value;
        }
    }


    #endregion

    #region Protected Events

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //Resetting the error label
        lblMessage.Text = string.Empty;
        lblEmrgencyError.Text = string.Empty;
        lblContactError.Text = string.Empty;
        lblConfirmMsg.Text = string.Empty;

        btnUpdate.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return ButtonClickValidate();");

        txtCHouseNo.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtCHouseNo.ClientID + "','" + imgCHouseNo.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
        imgCHouseNo.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanCHouseNo.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgCHouseNo.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanCHouseNo.ClientID + "');");

        txtCApartment.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtCApartment.ClientID + "','" + imgCApartment.ClientID + "','" + Common.CommonConstants.VALIDATE_ISALPHABET + "');");
        imgCApartment.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanCApartment.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
        imgCApartment.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanCApartment.ClientID + "');");

        txtCStreetName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtCStreetName.ClientID + "','" + imgCStreetName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_FUNCTION + "');");
        imgCStreetName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanCStreetName.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgCStreetName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanCStreetName.ClientID + "');");

        txtCLandmark.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtCLandmark.ClientID + "','" + imgCLandmark.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_FUNCTION + "');");
        imgCLandmark.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanCLandmark.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgCLandmark.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanCLandmark.ClientID + "');");

        txtCCity.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtCCity.ClientID + "','" + imgCCity.ClientID + "','" + Common.CommonConstants.VALIDATE_ISALPHABET + "');");
        imgCCity.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanCCity.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
        imgCCity.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanCCity.ClientID + "');");

        txtCState.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtCState.ClientID + "','" + imgCState.ClientID + "','" + Common.CommonConstants.VALIDATE_ISALPHABET + "');");
        imgCState.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanCState.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
        imgCState.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanCState.ClientID + "');");

        txtCCountry.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtCCountry.ClientID + "','" + imgCCountry.ClientID + "','" + Common.CommonConstants.VALIDATE_ISALPHABET + "');");
        imgCCountry.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanCCountry.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
        imgCCountry.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanCCountry.ClientID + "');");

        txtCPinCode.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtCPinCode.ClientID + "','" + imgCPinCode.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
        imgCPinCode.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanCPinCode.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgCPinCode.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanCPinCode.ClientID + "');");

        txtPHouseNo.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtPHouseNo.ClientID + "','" + imgPHouseNo.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
        imgPHouseNo.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanPHouseNo.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgPHouseNo.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanPHouseNo.ClientID + "');");

        txtPApartment.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtCApartment.ClientID + "','" + imgPApartment.ClientID + "','" + Common.CommonConstants.VALIDATE_ISALPHABET + "');");
        imgPApartment.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanPApartment.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
        imgPApartment.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanPApartment.ClientID + "');");

        txtPStreetName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtPStreetName.ClientID + "','" + imgPStreetName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_FUNCTION + "');");
        imgPStreetName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanPStreetName.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgPStreetName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanPStreetName.ClientID + "');");

        txtPLandmark.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtPLandmark.ClientID + "','" + imgPLandmark.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_FUNCTION + "');");
        imgPLandmark.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanPLandmark.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgPLandmark.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanPLandmark.ClientID + "');");

        txtPCity.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtPCity.ClientID + "','" + imgPCity.ClientID + "','" + Common.CommonConstants.VALIDATE_ISALPHABET + "');");
        imgPCity.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanPCity.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
        imgPCity.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanPCity.ClientID + "');");

        txtPState.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtPState.ClientID + "','" + imgPState.ClientID + "','" + Common.CommonConstants.VALIDATE_ISALPHABET + "');");
        imgPState.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanPState.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
        imgPState.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanPState.ClientID + "');");

        txtPCountry.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtPCountry.ClientID + "','" + imgPCountry.ClientID + "','" + Common.CommonConstants.VALIDATE_ISALPHABET + "');");
        imgPCountry.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanPCountry.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
        imgPCountry.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanPCountry.ClientID + "');");

        txtPPincode.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtPPincode.ClientID + "','" + imgPPincode.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
        imgPPincode.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanPPincode.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgPPincode.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanPPincode.ClientID + "');");
        if (!ValidateURL())
        {
            Response.Redirect(CommonConstants.INVALIDURL, false);
        }
        else
        {
            if (Session[Common.SessionNames.EMPLOYEEDETAILS] != null)
            {
                employee = (BusinessEntities.Employee)Session[Common.SessionNames.EMPLOYEEDETAILS];
            }

            if (employee != null)
            {
                employeeID = employee.EMPId;
            }

            // Get logged-in user's email id
            AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
            UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
            UserMailId = UserRaveDomainId.Replace("co.in", "com");
            AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
            arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());



            if (!IsPostBack)
            {
                //setting Pagemode & default value for hiddenfield.
                Session[SessionNames.PAGEMODE] = Common.MasterEnum.PageModeEnum.View;
                EMPAddressId.Value = "0";

                this.PopulateGrid(employeeID);
                this.PopulateEmergencyGrid(employeeID);
                this.PopulateControls();
            }

            if (Session[SessionNames.PAGEMODE] != null)
            {
                PageMode = Session[SessionNames.PAGEMODE].ToString();

                if (PageMode == Common.MasterEnum.PageModeEnum.View.ToString())
                {
                    this.EnableDisableControl();
                    btnUpdate.Visible = false;
                }
            }
        }
        
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the rblSamePerAddress control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
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

            if (txtPHouseNo.Text != string.Empty && txtPStreetName.Text != string.Empty && txtPPincode.Text != string.Empty && txtPCity.Text != string.Empty && !arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
            {
                txtPHouseNo.Enabled = false;
                txtPStreetName.Enabled = false;
                txtPCity.Enabled = false;
                txtPPincode.Enabled = false;
                txtPApartment.Enabled = false;
                txtPCountry.Enabled = false;
                txtPHouseNo.Enabled = false;
                txtPLandmark.Enabled = false;
                txtPState.Enabled = false;
            }
        }

    }

    /// <summary>
    /// Handles the Click event of the btnEdit control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Session[SessionNames.PAGEMODE] = Common.MasterEnum.PageModeEnum.Edit;
        btnUpdate.Visible = true;
        //btnCancel.Visible = true;
        btnEdit.Visible = false;
        this.EnableControls();
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the gvEmergencyDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void gvEmergencyDetails_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Handles the RowDataBound event of the gvContactDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
    protected void gvContactDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ContactTypeCollection = this.GetContactTypes();

            DropDownList ContactTypeddl = (DropDownList)e.Row.Cells[0].FindControl("ddlContactType");
            Label ContactTypeHf = (Label)e.Row.Cells[7].FindControl("HfContactType");
            Label ContactTypeValue = (Label)e.Row.Cells[0].FindControl("lblContactType");
          
            ContactTypeValue.Visible = true;
            ContactTypeddl.Visible = false;

            ContactTypeddl.DataSource = ContactTypeCollection;
            ContactTypeddl.DataTextField = Common.CommonConstants.DDL_DataTextField;
            ContactTypeddl.DataValueField = Common.CommonConstants.DDL_DataValueField;
            ContactTypeddl.DataBind();

            ContactTypeddl.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

            //set the dropdownlist value
            ContactTypeddl.SelectedValue = ContactTypeHf.Text;

        }
        if (e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
            DropDownList ddlContactType = (DropDownList)gvContactDetails.Controls[0].Controls[0].FindControl("ddlEmptyContactType");

            ContactTypeCollection = this.GetContactTypes();

            ddlContactType.DataSource = ContactTypeCollection;
            ddlContactType.DataTextField = Common.CommonConstants.DDL_DataTextField;
            ddlContactType.DataValueField = Common.CommonConstants.DDL_DataValueField;
            ddlContactType.DataBind();

            ddlContactType.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

        }
    }

    /// <summary>
    /// Handles the RowCommand event of the gvContactDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
    protected void gvContactDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EmptyAdd")
        {
            DropDownList ddlContactType = (DropDownList)gvContactDetails.Controls[0].Controls[0].FindControl("ddlEmptyContactType");
            TextBox CityCode = (TextBox)gvContactDetails.Controls[0].Controls[0].FindControl("txtEmptyCityCode");
            TextBox CountryCode = (TextBox)gvContactDetails.Controls[0].Controls[0].FindControl("txtEmptyCountryCode");
            TextBox ContactNo = (TextBox)gvContactDetails.Controls[0].Controls[0].FindControl("txtEmptyContactNo");
            TextBox Extension = (TextBox)gvContactDetails.Controls[0].Controls[0].FindControl("txtEmptyExtension");
            TextBox AvailabilityTime = (TextBox)gvContactDetails.Controls[0].Controls[0].FindControl("txtEmptyAvailabilityTime");

            if (ddlContactType.SelectedItem.Text == CommonConstants.SELECT)
            {
                lblContactError.Text = "Please select a Contact Type.";
                return;
            }

            if (CityCode.Text == string.Empty)
            {
                lblContactError.Text = "City Code" + " is mandatory.";
                return;
            }

            if (CountryCode.Text == string.Empty)
            {
                lblContactError.Text = "Country Code" + " is mandatory.";
                return;
            }

            if (ContactNo.Text == string.Empty)
            {
                lblContactError.Text = "ContactNo" + " is mandatory.";
                return;
            }

            if (Extension.Text == string.Empty)
            {
                lblContactError.Text = "Extension" + " is mandatory.";
                return;
            }

            if (AvailabilityTime.Text == string.Empty)
            {
                lblContactError.Text = "Availability Time" + " is mandatory.";
                return;
            }

            regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
            if (CityCode.Text != string.Empty)
            {
                if (!regexObj.IsMatch(CityCode.Text))
                {
                    lblContactError.Text = "City Code" + " should have numeric only.";
                    return;
                }
            }
            regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
            if (CountryCode.Text != string.Empty)
            {
                if (!regexObj.IsMatch(CountryCode.Text))
                {
                    lblContactError.Text = "Country Code" + " should have numeric only.";
                    return;
                }
            }

            regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
            if (ContactNo.Text != string.Empty)
            {
                if (!regexObj.IsMatch(ContactNo.Text))
                {
                    lblContactError.Text = "Contact No" + " should have numeric only.";
                    return;
                }
            }

            regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
            if (Extension.Text != string.Empty)
            {
                if (!regexObj.IsMatch(Extension.Text))
                {
                    lblContactError.Text = "Extension" + " should have numeric only.";
                    return;
                }
            }

            regexObj = new Regex(CommonConstants.ALPHANUMERIC_WITHSPACE);
            if (AvailabilityTime.Text != string.Empty)
            {
                if (!regexObj.IsMatch(AvailabilityTime.Text))
                {
                    lblContactError.Text = "Availability Time" + " should have alphanumeric only.";
                    return;
                }
            }

            BusinessEntities.ContactDetails objContactDetails = new BusinessEntities.ContactDetails();

            objContactDetails.ContactType = int.Parse(ddlContactType.SelectedValue);
            objContactDetails.CityCode = int.Parse(CityCode.Text.Trim());
            objContactDetails.CountryCode = int.Parse(CountryCode.Text.Trim());
            objContactDetails.ContactNo = ContactNo.Text.Trim();
            objContactDetails.Extension = int.Parse(Extension.Text.Trim());
            objContactDetails.AvalibilityTime = AvailabilityTime.Text.Trim();
            objContactDetails.ContactTypeName = ddlContactType.SelectedItem.Text.Trim();
            objContactDetails.Mode = 1;

            ContactdetailsCollection.Add(objContactDetails);

            this.DoDataBind();
        }

        if (e.CommandName == "Add")
        {

            DropDownList ddlContactType = (DropDownList)gvContactDetails.FooterRow.FindControl("ddlAddContactType");
            TextBox CityCode = (TextBox)gvContactDetails.FooterRow.FindControl("txtAddCityCode");
            TextBox CountryCode = (TextBox)gvContactDetails.FooterRow.FindControl("txtAddCountryCode");
            TextBox ContactNo = (TextBox)gvContactDetails.FooterRow.FindControl("txtAddContactNo");
            TextBox Extension = (TextBox)gvContactDetails.FooterRow.FindControl("txtAddExtension");
            TextBox AvailabilityTime = (TextBox)gvContactDetails.FooterRow.FindControl("txtAddAvailabilityTime");

            if (ddlContactType.SelectedItem.Text == CommonConstants.SELECT)
            {
                lblContactError.Text = "Please select a Contact Type.";
                return;
            }

            if (CityCode.Text == string.Empty)
            {
                lblContactError.Text = "City Code" + " is mandatory.";
                return;
            }

            if (CountryCode.Text == string.Empty)
            {
                lblContactError.Text = "Country Code" + " is mandatory.";
                return;
            }

            if (ContactNo.Text == string.Empty)
            {
                lblContactError.Text = "ContactNo" + " is mandatory.";
                return;
            }

            if (Extension.Text == string.Empty)
            {
                lblContactError.Text = "Extension" + " is mandatory.";
                return;
            }

            if (AvailabilityTime.Text == string.Empty)
            {
                lblContactError.Text = "Availability Time" + " is mandatory.";
                return;
            }

            regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
            if (CityCode.Text != string.Empty)
            {
                if (!regexObj.IsMatch(CityCode.Text))
                {
                    lblContactError.Text = "City Code" + " should have numeric only.";
                    return;
                }
            }
            regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
            if (CountryCode.Text != string.Empty)
            {
                if (!regexObj.IsMatch(CountryCode.Text))
                {
                    lblContactError.Text = "Country Code" + " should have numeric only.";
                    return;
                }
            }

            regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
            if (ContactNo.Text != string.Empty)
            {
                if (!regexObj.IsMatch(ContactNo.Text))
                {
                    lblContactError.Text = "Contact No" + " should have numeric only.";
                    return;
                }
            }

            regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
            if (Extension.Text != string.Empty)
            {
                if (!regexObj.IsMatch(Extension.Text))
                {
                    lblContactError.Text = "Extension" + " should have numeric only.";
                    return;
                }
            }

            regexObj = new Regex(CommonConstants.ALPHANUMERIC_WITHSPACE);
            if (AvailabilityTime.Text != string.Empty)
            {
                if (!regexObj.IsMatch(AvailabilityTime.Text))
                {
                    lblContactError.Text = "Availability Time" + " should have alphanumeric only.";
                    return;
                }
            }

            BusinessEntities.ContactDetails objContactDetails = new BusinessEntities.ContactDetails();

            objContactDetails.ContactType = int.Parse(ddlContactType.SelectedValue);
            objContactDetails.CityCode = int.Parse(CityCode.Text.Trim());
            objContactDetails.CountryCode = int.Parse(CountryCode.Text.Trim());
            objContactDetails.ContactNo = ContactNo.Text.Trim();
            objContactDetails.Extension = int.Parse(Extension.Text.Trim());
            objContactDetails.AvalibilityTime = AvailabilityTime.Text.Trim();
            objContactDetails.ContactTypeName = ddlContactType.SelectedItem.Text.Trim();
            objContactDetails.Mode = 1;

            ContactdetailsCollection.Add(objContactDetails);

            this.DoDataBind();
        }

        
        //Populating Dropdownlist in the FooterRow of the grid
        DropDownList AddContactType = (DropDownList)gvContactDetails.FooterRow.FindControl("ddlAddContactType");

        AddContactType.DataSource = ContactTypeCollection;
        AddContactType.DataTextField = Common.CommonConstants.DDL_DataTextField;
        AddContactType.DataValueField = Common.CommonConstants.DDL_DataValueField;
        AddContactType.DataBind();

        AddContactType.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
    }

    /// <summary>
    /// Handles the RowEditing event of the gvContactDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewEditEventArgs"/> instance containing the event data.</param>
    protected void gvContactDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvContactDetails.EditIndex = e.NewEditIndex;
        DoDataBind();

        Label ContactType = (Label)gvContactDetails.Rows[e.NewEditIndex].FindControl("lblContactType");
        DropDownList ContactTypeddl = (DropDownList)gvContactDetails.Rows[e.NewEditIndex].FindControl("ddlContactType");
        Label ContactTypeHf = (Label)gvContactDetails.Rows[e.NewEditIndex].FindControl("HfContactType");

        ContactTypeddl.Visible = true;
        ContactType.Visible = false;
        ContactTypeddl.SelectedValue = ContactTypeHf.Text;

        //Populating Dropdownlist in the FooterRow of the grid
        DropDownList AddContactType = (DropDownList)gvContactDetails.FooterRow.FindControl("ddlAddContactType");

        AddContactType.DataSource = ContactTypeCollection;
        AddContactType.DataTextField = Common.CommonConstants.DDL_DataTextField;
        AddContactType.DataValueField = Common.CommonConstants.DDL_DataValueField;
        AddContactType.DataBind();

        AddContactType.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
    }

    /// <summary>
    /// Handles the RowDeleting event of the gvContactDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewDeleteEventArgs"/> instance containing the event data.</param>
    protected void gvContactDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int deleteRowIndex = 0;
        int rowIndex = -1;

        BusinessEntities.ContactDetails objContactDetails = new BusinessEntities.ContactDetails();

        deleteRowIndex = e.RowIndex;

        objContactDetails = (BusinessEntities.ContactDetails)ContactdetailsCollection.Item(deleteRowIndex);
        objContactDetails.Mode = 3;

        if (ViewState[CONTACTDETAILSDELETE] != null)
        {
            BusinessEntities.RaveHRCollection objDeleteContactDetailsCollection = (BusinessEntities.RaveHRCollection)ViewState[CONTACTDETAILSDELETE];
            objDeleteContactDetailsCollection.Add(objContactDetails);

            ViewState[CONTACTDETAILSDELETE] = objDeleteContactDetailsCollection;
        }
        else
        {
            BusinessEntities.RaveHRCollection objDeleteContactDetailsCollection1 = new BusinessEntities.RaveHRCollection();

            objDeleteContactDetailsCollection1.Add(objContactDetails);

            ViewState[CONTACTDETAILSDELETE] = objDeleteContactDetailsCollection1;
        }

        ContactdetailsCollection.RemoveAt(deleteRowIndex);

        ViewState[DELETEROWINDEX] = deleteRowIndex;

        DoDataBind();

        //Populating Dropdownlist in the FooterRow of the grid
        DropDownList AddContactType = (DropDownList)gvContactDetails.FooterRow.FindControl("ddlAddContactType");

        AddContactType.DataSource = ContactTypeCollection;
        AddContactType.DataTextField = Common.CommonConstants.DDL_DataTextField;
        AddContactType.DataValueField = Common.CommonConstants.DDL_DataValueField;
        AddContactType.DataBind();

        AddContactType.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

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

            ImageButton btnImg = (ImageButton)gvContactDetails.Rows[rowIndex].FindControl(IMGBTNDELETE);
            btnImg.Enabled = false;

            //Disabling all the edit buttons.
            for (int i = 0; i < gvContactDetails.Rows.Count; i++)
            {
                if (rowIndex != i)
                {
                    ImageButton btnImgEdit = (ImageButton)gvContactDetails.Rows[i].FindControl(IMGBTNEDIT);
                    btnImgEdit.Enabled = false;
                }
            }
        }
    }

    /// <summary>
    /// Handles the RowCancelingEdit event of the gvContactDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCancelEditEventArgs"/> instance containing the event data.</param>
    protected void gvContactDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvContactDetails.EditIndex = -1;
        DoDataBind();

        //Populating Dropdownlist in the FooterRow of the grid
        DropDownList AddContactType = (DropDownList)gvContactDetails.FooterRow.FindControl("ddlAddContactType");

        AddContactType.DataSource = ContactTypeCollection;
        AddContactType.DataTextField = Common.CommonConstants.DDL_DataTextField;
        AddContactType.DataValueField = Common.CommonConstants.DDL_DataValueField;
        AddContactType.DataBind();

        AddContactType.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

    }

    /// <summary>
    /// Handles the RowUpdating event of the gvContactDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewUpdateEventArgs"/> instance containing the event data.</param>
    protected void gvContactDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label ContactTypeHf = (Label)gvContactDetails.Rows[e.RowIndex].FindControl("HfContactType");
        DropDownList ddlContactType = (DropDownList)gvContactDetails.Rows[e.RowIndex].FindControl("ddlContactType");
        TextBox CityCode = (TextBox)gvContactDetails.Rows[e.RowIndex].FindControl("txtCityCode");
        TextBox CountryCode = (TextBox)gvContactDetails.Rows[e.RowIndex].FindControl("txtCountryCode");
        TextBox ContactNo = (TextBox)gvContactDetails.Rows[e.RowIndex].FindControl("txtContactNo");
        TextBox Extension = (TextBox)gvContactDetails.Rows[e.RowIndex].FindControl("txtExtension");
        TextBox AvailabilityTime = (TextBox)gvContactDetails.Rows[e.RowIndex].FindControl("txtAvailabilityTime");
        Label ContactId = (Label)gvContactDetails.Rows[e.RowIndex].FindControl("ContactId");
        ImageButton BtnUpdate = (ImageButton)gvContactDetails.Rows[e.RowIndex].FindControl("imgBtnUpdate");
        ImageButton BtnCancel = (ImageButton)gvContactDetails.Rows[e.RowIndex].FindControl("imgBtnCancel");
        Label Mode = (Label)gvContactDetails.Rows[e.RowIndex].FindControl(MODE);

        if (ddlContactType.SelectedItem.Text == CommonConstants.SELECT)
        {
            lblContactError.Text = "Please select a Contact Type.";
            return;
        }

        if (CityCode.Text == string.Empty)
        {
            lblContactError.Text = "City Code" + " is mandatory.";
            return;
        }

        if (CountryCode.Text == string.Empty)
        {
            lblContactError.Text = "Country Code" + " is mandatory.";
            return;
        }

        if (ContactNo.Text == string.Empty)
        {
            lblContactError.Text = "ContactNo" + " is mandatory.";
            return;
        }

        if (Extension.Text == string.Empty)
        {
            lblContactError.Text = "Extension" + " is mandatory.";
            return;
        }

        if (AvailabilityTime.Text == string.Empty)
        {
            lblContactError.Text = "Availability Time" + " is mandatory.";
            return;
        }

        regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
        if (CityCode.Text != string.Empty)
        {
            if (!regexObj.IsMatch(CityCode.Text))
            {
                lblContactError.Text = "City Code" + " should have numeric only.";
                return;
            }
        }
        regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
        if (CountryCode.Text != string.Empty)
        {
            if (!regexObj.IsMatch(CountryCode.Text))
            {
                lblContactError.Text = "Country Code" + " should have numeric only.";
                return;
            }
        }

        regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
        if (ContactNo.Text != string.Empty)
        {
            if (!regexObj.IsMatch(ContactNo.Text))
            {
                lblContactError.Text = "Contact No" + " should have numeric only.";
                return;
            }
        }

        regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
        if (Extension.Text != string.Empty)
        {
            if (!regexObj.IsMatch(Extension.Text))
            {
                lblContactError.Text = "Extension" + " should have numeric only.";
                return;
            }
        }

        regexObj = new Regex(CommonConstants.ALPHANUMERIC_WITHSPACE);
        if (AvailabilityTime.Text != string.Empty)
        {
            if (!regexObj.IsMatch(AvailabilityTime.Text))
            {
                lblContactError.Text = "Availability Time" + " should have alphanumeric only.";
                return;
            }
        }

        BtnUpdate.Visible = false;
        BtnCancel.Visible = false;

        if (int.Parse(ContactId.Text) == 0)
        {
            Mode.Text = "1";
        }
        else
        {
            Mode.Text = "2";
        }

        BusinessEntities.ContactDetails objContactDetails = new BusinessEntities.ContactDetails();
        objContactDetails = (BusinessEntities.ContactDetails)ContactdetailsCollection.Item(e.RowIndex);

        objContactDetails.ContactType = int.Parse(ddlContactType.SelectedValue);
        //objContactDetails.EMPId = int.Parse(EMPId.Value);
        objContactDetails.EMPId = 4;
        objContactDetails.ContactTypeName = ddlContactType.SelectedItem.Text;
        objContactDetails.CityCode = int.Parse(CityCode.Text);
        objContactDetails.CountryCode = int.Parse(CountryCode.Text);
        objContactDetails.ContactNo = ContactNo.Text;
        objContactDetails.Extension = int.Parse(Extension.Text);
        objContactDetails.AvalibilityTime = AvailabilityTime.Text;
        objContactDetails.Mode = int.Parse(Mode.Text);

        gvContactDetails.EditIndex = -1;
        DoDataBind();  

        //Populating Dropdownlist in the FooterRow of the grid.
        DropDownList AddContactType = (DropDownList)gvContactDetails.FooterRow.FindControl("ddlAddContactType");
        AddContactType.DataSource = ContactTypeCollection;
        AddContactType.DataTextField = Common.CommonConstants.DDL_DataTextField;
        AddContactType.DataValueField = Common.CommonConstants.DDL_DataValueField;
        AddContactType.DataBind();
        AddContactType.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

        ImageButton BtnEdit = (ImageButton)gvContactDetails.Rows[e.RowIndex].FindControl(IMGBTNEDIT);
        ImageButton BtnDelete = (ImageButton)gvContactDetails.Rows[e.RowIndex].FindControl(IMGBTNDELETE);
        BtnEdit.Visible = true;
        BtnDelete.Visible = true;
    }

    /// <summary>
    /// Handles the Click event of the btnUpdate control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Rave.HR.BusinessLayer.Employee.Address objAddressBAL;

        BusinessEntities.Address objAddress;
        BusinessEntities.RaveHRCollection objSaveAddressCollection = new BusinessEntities.RaveHRCollection();

        Rave.HR.BusinessLayer.Employee.ContactDetails objContactDetailsBAL;

        BusinessEntities.ContactDetails objContactDetails;
        BusinessEntities.RaveHRCollection objSaveContactDetailsCollection = new BusinessEntities.RaveHRCollection();

        Rave.HR.BusinessLayer.Employee.EmergencyContact objEmergencyContactBAL;

        BusinessEntities.EmergencyContact objEmergencyContact;
        BusinessEntities.RaveHRCollection objSaveEmergencyContactCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            if (gvEmergencyDetails.Rows.Count == 0)
            {
                lblMessage.Text = "Please provide atleast one Emergency Contact Number.";
                return;
            }

            objContactDetailsBAL = new Rave.HR.BusinessLayer.Employee.ContactDetails();

            int count = 0;
            for (int i = 0; i < gvContactDetails.Rows.Count; i++)
            {
                objContactDetails = new BusinessEntities.ContactDetails();

                Label ContactType = (Label)gvContactDetails.Rows[i].FindControl("lblContactType");
                Label ContactId = (Label)gvContactDetails.Rows[i].FindControl("ContactId");
                Label Mode = (Label)gvContactDetails.Rows[i].FindControl(MODE);
                Label CityCode = (Label)gvContactDetails.Rows[i].FindControl("lblCityCode");
                Label CountryCode = (Label)gvContactDetails.Rows[i].FindControl("lblCountryCode");
                Label ContactNo = (Label)gvContactDetails.Rows[i].FindControl("lblContactNo");
                Label Extension = (Label)gvContactDetails.Rows[i].FindControl("lblExtension");
                Label AvailabilityTime = (Label)gvContactDetails.Rows[i].FindControl("lblAvailabilityTime");
                Label ContactTypeHf = (Label)gvContactDetails.Rows[i].FindControl("HfContactType");

                objContactDetails.EmployeeContactId = int.Parse(ContactId.Text.Trim());
                //objContactDetails.EMPId = int.Parse(EMPId.Value);
                objContactDetails.EMPId = 4;
                objContactDetails.ContactTypeName = ContactType.Text.Trim();
                objContactDetails.CityCode = int.Parse(CityCode.Text.Trim());
                objContactDetails.CountryCode = int.Parse(CountryCode.Text.Trim());
                objContactDetails.ContactNo = ContactNo.Text.Trim();
                objContactDetails.Extension = int.Parse(Extension.Text.Trim());
                objContactDetails.AvalibilityTime = AvailabilityTime.Text.Trim();
                objContactDetails.ContactType = int.Parse(ContactTypeHf.Text.Trim());
                objContactDetails.Mode = int.Parse(Mode.Text.Trim());
                objContactDetails.CreatedById = UserMailId;
                objContactDetails.CreatedDate = DateTime.Today.Date;
                objContactDetails.LastModifiedById = UserMailId;
                objContactDetails.LastModifiedDate = DateTime.Today.Date;

                if (ContactType.Text.Trim() == MasterEnum.ContactType.Home.ToString() || ContactType.Text.Trim() == MasterEnum.ContactType.Mobile.ToString())
                    count++;


                objSaveContactDetailsCollection.Add(objContactDetails);
            }

            if (count < 2)
            {
                lblMessage.Text = "Please provide atleast one contact number of type Home and Mobile.";
                return;
            }

            BusinessEntities.RaveHRCollection objDeleteContactDetailsCollection = (BusinessEntities.RaveHRCollection)ViewState[CONTACTDETAILSDELETE];

            if (objDeleteContactDetailsCollection != null)
            {
                BusinessEntities.ContactDetails obj = null;

                for (int i = 0; i < objDeleteContactDetailsCollection.Count; i++)
                {
                    objContactDetails = new BusinessEntities.ContactDetails();
                    obj = (BusinessEntities.ContactDetails)objDeleteContactDetailsCollection.Item(i);

                    objContactDetails.EmployeeContactId = obj.EmployeeContactId;
                    objContactDetails.EMPId = obj.EMPId;
                    objContactDetails.ContactTypeName = obj.ContactTypeName;
                    objContactDetails.CityCode = obj.CityCode;
                    objContactDetails.CountryCode = obj.CountryCode;
                    objContactDetails.ContactNo = obj.ContactNo;
                    objContactDetails.Extension = obj.Extension;
                    objContactDetails.AvalibilityTime = obj.AvalibilityTime;
                    objContactDetails.ContactType = obj.ContactType;
                    objContactDetails.Mode = obj.Mode;

                    objSaveContactDetailsCollection.Add(objContactDetails);
                }

            }
            objContactDetailsBAL.Manipulation(objSaveContactDetailsCollection);


            objAddressBAL = new Rave.HR.BusinessLayer.Employee.Address();
            objAddress = new BusinessEntities.Address();

            objAddress.FlatNo = txtCHouseNo.Text.Trim();
            objAddress.BuildingName = txtCApartment.Text.Trim();
            objAddress.Street = txtCStreetName.Text.Trim();
            objAddress.Landmark = txtCLandmark.Text.Trim();
            objAddress.City = txtCCity.Text.Trim();
            objAddress.State = txtCState.Text.Trim();
            objAddress.Country = txtCCountry.Text.Trim();
            objAddress.Pincode = txtCPinCode.Text.Trim();
            objAddress.AddressType = (int)MasterEnum.AddressType.Current;
            objAddress.EMPId = 4;
            objAddress.CreatedById = UserMailId;
            objAddress.LastModifiedById = UserMailId;
            objAddress.EmployeeAddressId = int.Parse(EMPAddressId.Value);

            if (!Flag)
            {
                objAddressBAL.ManipulateAddress(objAddress);

            }
            else
            {
                objAddressBAL.AddAddress(objAddress);
                Flag = false;
            }
            //If perment & current address differs
            if (rblSamePerAddress.SelectedIndex == CommonConstants.ONE)
            {
                objAddress = new BusinessEntities.Address();

                objAddress.FlatNo = txtPHouseNo.Text.Trim();
                objAddress.BuildingName = txtPApartment.Text.Trim();
                objAddress.Street = txtPStreetName.Text.Trim();
                objAddress.Landmark = txtPLandmark.Text.Trim();
                objAddress.City = txtPCity.Text.Trim();
                objAddress.State = txtPState.Text.Trim();
                objAddress.Country = txtPCountry.Text.Trim();
                objAddress.Pincode = txtPPincode.Text.Trim();
                objAddress.AddressType = (int)MasterEnum.AddressType.Permanent;
                objAddress.EMPId = 4;
                objAddress.CreatedById = UserMailId;
                objAddress.LastModifiedById = UserMailId;

                objAddressBAL.ManipulateAddress(objAddress);

            }

            objEmergencyContactBAL = new Rave.HR.BusinessLayer.Employee.EmergencyContact();

            for (int i = 0; i < gvEmergencyDetails.Rows.Count; i++)
            {
                objEmergencyContact = new BusinessEntities.EmergencyContact();

                Label Name = (Label)gvEmergencyDetails.Rows[i].FindControl("lblName");
                Label ContactId = (Label)gvEmergencyDetails.Rows[i].FindControl("EmrContactId");
                Label Mode = (Label)gvEmergencyDetails.Rows[i].FindControl(MODE);
                Label Relation = (Label)gvEmergencyDetails.Rows[i].FindControl("lblRelation");
                Label ContactNo = (Label)gvEmergencyDetails.Rows[i].FindControl("lblContactNo");

                Label RelationTypeHf = (Label)gvEmergencyDetails.Rows[i].FindControl("HfRelationType");

                objEmergencyContact.EmergencyContactId = int.Parse(ContactId.Text.Trim());
                //objEmergencyContact.EMPId = int.Parse(EMPId.Value);
                objEmergencyContact.EMPId = 4;
                objEmergencyContact.ContactName = Name.Text.Trim();
                objEmergencyContact.ContactNumber = ContactNo.Text.Trim();
                objEmergencyContact.Relation = Relation.Text.Trim();
                objEmergencyContact.RelationType = int.Parse(RelationTypeHf.Text.Trim());
                objEmergencyContact.Mode = int.Parse(Mode.Text.Trim());


                objSaveEmergencyContactCollection.Add(objEmergencyContact);
            }

            BusinessEntities.RaveHRCollection objDeleteEmergencyContactCollection = (BusinessEntities.RaveHRCollection)ViewState[EMERGENCYCONTACTDELETE];

            if (objDeleteEmergencyContactCollection != null)
            {
                BusinessEntities.EmergencyContact obj = null;

                for (int i = 0; i < objDeleteEmergencyContactCollection.Count; i++)
                {
                    objEmergencyContact = new BusinessEntities.EmergencyContact();
                    obj = (BusinessEntities.EmergencyContact)objDeleteEmergencyContactCollection.Item(i);

                    objEmergencyContact.EmergencyContactId = obj.EmergencyContactId;
                    objEmergencyContact.EMPId = obj.EMPId;
                    objEmergencyContact.ContactName = obj.ContactName;
                    objEmergencyContact.ContactNumber = obj.ContactNumber;
                    objEmergencyContact.Relation = obj.Relation;
                    objEmergencyContact.RelationType = obj.RelationType;
                    objEmergencyContact.Mode = obj.Mode;

                    objSaveEmergencyContactCollection.Add(objEmergencyContact);
                }

            }
            objEmergencyContactBAL.Manipulation(objSaveEmergencyContactCollection);

            if (ViewState.Count > 0)
            {
                ViewState.Clear();
            }

            if (EMPId.Value != string.Empty)
            {
                int empID = Convert.ToInt32(EMPId.Value);
                //Refresh the grip after saving
                this.PopulateGrid(empID);
                this.PopulateEmergencyGrid(empID);
                this.PopulateControls();
            }

            lblConfirmMsg.Text = "Contact details saved successfully.";
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnSave_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the RowEditing event of the gvEmergencyDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewEditEventArgs"/> instance containing the event data.</param>
    protected void gvEmergencyDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvEmergencyDetails.EditIndex = e.NewEditIndex;
        DoEmergencyDataBind();

        DropDownList Relationddl = (DropDownList)gvEmergencyDetails.Rows[e.NewEditIndex].FindControl("ddlRelation");
        Label RelationTypeHf = (Label)gvEmergencyDetails.Rows[e.NewEditIndex].FindControl("HfRelationType");

        EmployeeRelationCollection = this.GetEmployeeRelation();
        Relationddl.DataSource = EmployeeRelationCollection;
        Relationddl.DataTextField = Common.CommonConstants.DDL_DataTextField;
        Relationddl.DataValueField = Common.CommonConstants.DDL_DataValueField;
        Relationddl.DataBind();

        Relationddl.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

        Relationddl.SelectedValue = RelationTypeHf.Text;

    }

    /// <summary>
    /// Handles the RowDeleting event of the gvEmergencyDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewDeleteEventArgs"/> instance containing the event data.</param>
    protected void gvEmergencyDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int deleteRowIndex = 0;
        int rowIndex = -1;

        BusinessEntities.EmergencyContact objEmergencyContact = new BusinessEntities.EmergencyContact();

        deleteRowIndex = e.RowIndex;

        objEmergencyContact = (BusinessEntities.EmergencyContact)EmergencydetailsCollection.Item(deleteRowIndex);
        objEmergencyContact.Mode = 3;

        if (ViewState[EMERGENCYCONTACTDELETE] != null)
        {
            BusinessEntities.RaveHRCollection objDeleteEmergencyContactCollection = (BusinessEntities.RaveHRCollection)ViewState[EMERGENCYCONTACTDELETE];
            objDeleteEmergencyContactCollection.Add(objEmergencyContact);

            ViewState[EMERGENCYCONTACTDELETE] = objDeleteEmergencyContactCollection;
        }
        else
        {
            BusinessEntities.RaveHRCollection objDeleteEmergencyContactCollection1 = new BusinessEntities.RaveHRCollection();

            objDeleteEmergencyContactCollection1.Add(objEmergencyContact);

            ViewState[EMERGENCYCONTACTDELETE] = objDeleteEmergencyContactCollection1;
        }

        EmergencydetailsCollection.RemoveAt(deleteRowIndex);

        ViewState[EMRDELETEROWINDEX] = deleteRowIndex;

        DoEmergencyDataBind();

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

            ImageButton btnImg = (ImageButton)gvEmergencyDetails.Rows[rowIndex].FindControl(IMGBTNDELETE);
            btnImg.Enabled = false;

            //Disabling all the edit buttons.
            for (int i = 0; i < gvEmergencyDetails.Rows.Count; i++)
            {
                if (rowIndex != i)
                {
                    ImageButton btnImgEdit = (ImageButton)gvEmergencyDetails.Rows[i].FindControl(IMGBTNEDIT);
                    btnImgEdit.Enabled = false;
                }
            }
        }
    }

    /// <summary>
    /// Handles the RowCommand event of the gvEmergencyDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
    protected void gvEmergencyDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EmptyAdd")
        {
            TextBox PersonName = (TextBox)gvEmergencyDetails.Controls[0].Controls[0].FindControl("txtEmptyName");
            DropDownList Relation = (DropDownList)gvEmergencyDetails.Controls[0].Controls[0].FindControl("ddlEmptyRelation");
            TextBox ContactNo = (TextBox)gvEmergencyDetails.Controls[0].Controls[0].FindControl("txtEmptyContactNo");

            if (PersonName.Text == string.Empty)
            {
                lblEmrgencyError.Text = "Person Name" + " is mandatory.";
                return;
            }

            if (Relation.SelectedItem.Text == CommonConstants.SELECT)
            {
                lblEmrgencyError.Text = "Please select a Relation.";
                return;
            }

            if (ContactNo.Text == string.Empty)
            {
                lblEmrgencyError.Text = "Contact No" + " is mandatory.";
                return;
            }

            regexObj = new Regex(CommonConstants.ALPHABET_WITHSPACE);
            if (PersonName.Text != string.Empty)
            {
                if (!regexObj.IsMatch(PersonName.Text))
                {
                    lblEmrgencyError.Text = "Person Name" + " should have alphabets only.";
                    return;
                }
            }

            regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
            if (ContactNo.Text != string.Empty)
            {
                if (!regexObj.IsMatch(ContactNo.Text))
                {
                    lblEmrgencyError.Text = "Contact No" + " should have numeric only.";
                    return;
                }
            }

            BusinessEntities.EmergencyContact objEmergencyContact = new BusinessEntities.EmergencyContact();

            objEmergencyContact.ContactName = PersonName.Text.Trim();
            objEmergencyContact.Relation = Relation.SelectedItem.Text.Trim();
            objEmergencyContact.ContactNumber = ContactNo.Text.Trim();
            objEmergencyContact.RelationType = int.Parse(Relation.SelectedValue);
            objEmergencyContact.Mode = 1;

            EmergencydetailsCollection.Add(objEmergencyContact);

            this.DoEmergencyDataBind();
        }

        if (e.CommandName == "Add")
        {
            TextBox PersonName = (TextBox)gvEmergencyDetails.FooterRow.FindControl("txtAddName");
            DropDownList Relation = (DropDownList)gvEmergencyDetails.FooterRow.FindControl("ddlAddRelation");
            TextBox ContactNo = (TextBox)gvEmergencyDetails.FooterRow.FindControl("txtAddContactNo");

            if (PersonName.Text == string.Empty)
            {
                lblEmrgencyError.Text = "Person Name" + " is mandatory.";
                return;
            }

            if (Relation.SelectedItem.Text == CommonConstants.SELECT)
            {
                lblEmrgencyError.Text = "Please select a Relation.";
                return;
            }

            if (ContactNo.Text == string.Empty)
            {
                lblEmrgencyError.Text = "Contact No" + " is mandatory.";
                return;
            }

            regexObj = new Regex(CommonConstants.ALPHABET_WITHSPACE);
            if (PersonName.Text != string.Empty)
            {
                if (!regexObj.IsMatch(PersonName.Text))
                {
                    lblEmrgencyError.Text = "Person Name" + " should have alphabets only.";
                    return;
                }
            }

            regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
            if (ContactNo.Text != string.Empty)
            {
                if (!regexObj.IsMatch(ContactNo.Text))
                {
                    lblEmrgencyError.Text = "Contact No" + " should have numeric only.";
                    return;
                }
            }

            BusinessEntities.EmergencyContact objEmergencyContact = new BusinessEntities.EmergencyContact();


            objEmergencyContact.ContactName = PersonName.Text.Trim();
            objEmergencyContact.Relation = Relation.SelectedItem.Text.Trim();
            objEmergencyContact.ContactNumber = ContactNo.Text.Trim();
            objEmergencyContact.RelationType = int.Parse(Relation.SelectedValue);
            objEmergencyContact.Mode = 1;

            EmergencydetailsCollection.Add(objEmergencyContact);

            this.DoEmergencyDataBind();
        }
    }

    /// <summary>
    /// Handles the RowUpdating event of the gvEmergencyDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewUpdateEventArgs"/> instance containing the event data.</param>
    protected void gvEmergencyDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        Label EmrContactId = (Label)gvEmergencyDetails.Rows[e.RowIndex].FindControl("EmrContactId");
        TextBox PersonName = (TextBox)gvEmergencyDetails.Rows[e.RowIndex].FindControl("txtName");
        //TextBox Relation = (TextBox)gvEmergencyDetails.Rows[e.RowIndex].FindControl("txtRelation");
        TextBox ContactNo = (TextBox)gvEmergencyDetails.Rows[e.RowIndex].FindControl("txtContactNo");
        ImageButton BtnUpdate = (ImageButton)gvEmergencyDetails.Rows[e.RowIndex].FindControl("imgBtnUpdate");
        ImageButton BtnCancel = (ImageButton)gvEmergencyDetails.Rows[e.RowIndex].FindControl("imgBtnCancel");
        Label Mode = (Label)gvEmergencyDetails.Rows[e.RowIndex].FindControl(MODE);
        DropDownList ddlRelationType = (DropDownList)gvEmergencyDetails.Rows[e.RowIndex].FindControl("ddlRelation");

        if (PersonName.Text == string.Empty)
        {
            lblEmrgencyError.Text = "Person Name" + " is mandatory.";
            return;
        }

        if (ddlRelationType.SelectedItem.Text == CommonConstants.SELECT)
        {
            lblEmrgencyError.Text = "Please select a Relation.";
            return;
        }

        if (ContactNo.Text == string.Empty)
        {
            lblEmrgencyError.Text = "Contact No" + " is mandatory.";
            return;
        }

        regexObj = new Regex(CommonConstants.ALPHABET_WITHSPACE);
        if (PersonName.Text != string.Empty)
        {
            if (!regexObj.IsMatch(PersonName.Text))
            {
                lblEmrgencyError.Text = "Person Name" + " should have alphabets only.";
                return;
            }
        }

        regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
        if (ContactNo.Text != string.Empty)
        {
            if (!regexObj.IsMatch(ContactNo.Text))
            {
                lblEmrgencyError.Text = "Contact Number" + " should have numeric only.";
                return;
            }
        }

        BtnUpdate.Visible = false;
        BtnCancel.Visible = false;

        if (int.Parse(EmrContactId.Text) == 0)
        {
            Mode.Text = "1";
        }
        else
        {
            Mode.Text = "2";
        }

        BusinessEntities.EmergencyContact objEmergencyContact = new BusinessEntities.EmergencyContact();
        objEmergencyContact = (BusinessEntities.EmergencyContact)EmergencydetailsCollection.Item(e.RowIndex);

        //objEmergencyContact.EMPId = int.Parse(EMPId.Value);
        objEmergencyContact.EMPId = 4;
        objEmergencyContact.ContactName = PersonName.Text;
        objEmergencyContact.Relation = ddlRelationType.SelectedItem.Text;
        objEmergencyContact.ContactNumber = ContactNo.Text;
        objEmergencyContact.Mode = int.Parse(Mode.Text);
        objEmergencyContact.RelationType = int.Parse(ddlRelationType.SelectedValue);

        gvEmergencyDetails.EditIndex = -1;
        DoEmergencyDataBind();

        ImageButton BtnEdit = (ImageButton)gvEmergencyDetails.Rows[e.RowIndex].FindControl(IMGBTNEDIT);
        ImageButton BtnDelete = (ImageButton)gvEmergencyDetails.Rows[e.RowIndex].FindControl(IMGBTNDELETE);
        BtnEdit.Visible = true;
        BtnDelete.Visible = true;
    }

    /// <summary>
    /// Handles the RowCancelingEdit event of the gvEmergencyDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCancelEditEventArgs"/> instance containing the event data.</param>
    protected void gvEmergencyDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvEmergencyDetails.EditIndex = -1;
        DoEmergencyDataBind();
    }

    /// <summary>
    /// Handles the RowDataBound event of the gvEmergencyDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
    protected void gvEmergencyDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //EmployeeRelationCollection = this.GetEmployeeRelation();

            //DropDownList ddlRelation = (DropDownList)e.Row.Cells[1].FindControl("ddlRelation");
            //HiddenField RelationTypeHf = (HiddenField)e.Row.Cells[5].FindControl("HfRelationType");
            //Label ContactTypeValue = (Label)e.Row.Cells[0].FindControl("lblContactType");

            //ContactTypeValue.Visible = true;
            //ContactTypeddl.Visible = false;

            //ddlRelation.DataSource = EmployeeRelationCollection;
            //ddlRelation.DataTextField = Common.CommonConstants.DDL_DataTextField;
            //ddlRelation.DataValueField = Common.CommonConstants.DDL_DataValueField;
            //ddlRelation.DataBind();

            //ddlRelation.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

            ////set the dropdownlist value
            //ddlRelation.SelectedValue = RelationTypeHf.Value;

        }
        if (e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
            DropDownList ddlRelation = (DropDownList)gvEmergencyDetails.Controls[0].Controls[0].FindControl("ddlEmptyRelation");

            EmployeeRelationCollection = this.GetEmployeeRelation();

            ddlRelation.DataSource = EmployeeRelationCollection;
            ddlRelation.DataTextField = Common.CommonConstants.DDL_DataTextField;
            ddlRelation.DataValueField = Common.CommonConstants.DDL_DataValueField;
            ddlRelation.DataBind();

            ddlRelation.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

        }
    }

    /// <summary>
    /// Handles the Click event of the btnChangeAddress control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnChangeAddress_Click(object sender, EventArgs e)
    {
        this.ClearControls();

        Flag = true;
    }

    /// <summary>
    /// Handles the Click event of the btnCanelChange control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnCanelChange_Click(object sender, EventArgs e)
    {
        Flag = false;
        this.PopulateControls();
    }

    /// <summary>
    /// Handles the Click event of the btnCancel control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(CommonConstants.PAGE_HOME);
    }

    #endregion

    #region Private Member Functions

    /// <summary>
    /// Does the data bind.
    /// </summary>
    private void DoDataBind()
    {
        //gvContactDetails.EditIndex = -1;

        gvContactDetails.DataSource = ContactdetailsCollection;
        gvContactDetails.DataBind();


    }

    /// <summary>
    /// Populates the grid.
    /// </summary>
    private void PopulateGrid(int employeeID)
    {
        ContactdetailsCollection = this.GetContactDetails(employeeID);

        //Binding the datatable to grid
        gvContactDetails.DataSource = ContactdetailsCollection;
        gvContactDetails.DataBind();

         EMPId.Value = "4";
        //EMPId.Value = employeeID.ToString().Trim();

         if (gvContactDetails.FooterRow != null)
         {
             //Populating Dropdownlist in the FooterRow of the grid
             DropDownList AddContactType = (DropDownList)gvContactDetails.FooterRow.FindControl("ddlAddContactType");

             AddContactType.DataSource = ContactTypeCollection;
             AddContactType.DataTextField = Common.CommonConstants.DDL_DataTextField;
             AddContactType.DataValueField = Common.CommonConstants.DDL_DataValueField;
             AddContactType.DataBind();

             AddContactType.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
         }
    }

    /// <summary>
    /// Populates the emergency grid.
    /// </summary>
    /// <param name="employeeID">The employee ID.</param>
    private void PopulateEmergencyGrid(int employeeID)
    {
        EmergencydetailsCollection = this.GetEmergencyDetails(employeeID);

        //Binding the datatable to grid
        gvEmergencyDetails.DataSource = EmergencydetailsCollection;
        gvEmergencyDetails.DataBind();

        EMPId.Value = "4";
        //EMPId.Value = employeeID.ToString().Trim();

        if (gvEmergencyDetails.FooterRow != null)
        {
            EmployeeRelationCollection = this.GetEmployeeRelation();

            //Populating Dropdownlist in the FooterRow of the grid
            DropDownList ddlRelation = (DropDownList)gvEmergencyDetails.FooterRow.FindControl("ddlAddRelation");

            ddlRelation.DataSource = EmployeeRelationCollection;
            ddlRelation.DataTextField = Common.CommonConstants.DDL_DataTextField;
            ddlRelation.DataValueField = Common.CommonConstants.DDL_DataValueField;
            ddlRelation.DataBind();

            ddlRelation.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
        }
    }

    /// <summary>
    /// Gets the location.
    /// </summary>
    private BusinessEntities.RaveHRCollection GetContactTypes()
    {
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        return raveHrColl = master.FillDropDownsBL((int)Common.EnumsConstants.Category.ContactType);

        //ddlQualification.DataSource = raveHrColl;
        //ddlQualification.DataTextField = Common.CommonConstants.DDL_DataTextField;
        //ddlQualification.DataValueField = Common.CommonConstants.DDL_DataValueField;
        //ddlQualification.DataBind();
        //ddlQualification.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

    }

    /// <summary>
    /// Gets the contact details.
    /// </summary>
    /// <param name="employeeID">The employee ID.</param>
    /// <returns></returns>
    private BusinessEntities.RaveHRCollection GetContactDetails(int employeeID)
    {
        Rave.HR.BusinessLayer.Employee.ContactDetails objContactDetailsBAL;
        BusinessEntities.ContactDetails objContactDetails;

        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            objContactDetailsBAL = new Rave.HR.BusinessLayer.Employee.ContactDetails();
            objContactDetails = new BusinessEntities.ContactDetails();

            //objContactDetails.EMPId = employeeID;
            objContactDetails.EMPId = 4;

            raveHRCollection = objContactDetailsBAL.GetContactDetails(objContactDetails);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetContactDetails", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

        return raveHRCollection;
    }

    /// <summary>
    /// Enables the disable control.
    /// </summary>
    private void EnableDisableControl()
    {
        pnlEmpCurrentAddress.Enabled = false;
        pnlPermanenetAddr.Enabled = false;
        //divContactDetails.Disabled = true;
        gvContactDetails.Enabled = false;
        divEmergencyDetails.Disabled = true;
    }

    /// <summary>
    /// Enables the disable control.
    /// </summary>
    private void EnableControls()
    {
        pnlEmpCurrentAddress.Enabled = true;
        pnlPermanenetAddr.Enabled = true;
        //divContactDetails.Disabled = false;
        gvContactDetails.Enabled = true;
        divEmergencyDetails.Disabled = false;
    }

    /// <summary>
    /// Does the data bind.
    /// </summary>
    private void DoEmergencyDataBind()
    {
        gvEmergencyDetails.DataSource = EmergencydetailsCollection;
        gvEmergencyDetails.DataBind();

        if (gvEmergencyDetails.Rows.Count == 5)
        {
            gvEmergencyDetails.FooterRow.Visible = false;
        }

        //Populating Dropdownlist in the FooterRow of the grid
        DropDownList ddlRelation = (DropDownList)gvEmergencyDetails.FooterRow.FindControl("ddlAddRelation");

        ddlRelation.DataSource = EmployeeRelationCollection;
        ddlRelation.DataTextField = Common.CommonConstants.DDL_DataTextField;
        ddlRelation.DataValueField = Common.CommonConstants.DDL_DataValueField;
        ddlRelation.DataBind();

        ddlRelation.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
    }

    /// <summary>
    /// Gets the emergency details.
    /// </summary>
    /// <param name="employeeID">The employee ID.</param>
    /// <returns></returns>
    private BusinessEntities.RaveHRCollection GetEmergencyDetails(int employeeID)
    {
        Rave.HR.BusinessLayer.Employee.EmergencyContact objEmergencyContactBAL;
        BusinessEntities.EmergencyContact objEmergencyContact;

        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            objEmergencyContactBAL = new Rave.HR.BusinessLayer.Employee.EmergencyContact();
            objEmergencyContact = new BusinessEntities.EmergencyContact();

            //objContactDetails.EMPId = employeeID;
            objEmergencyContact.EMPId = 4;

            raveHRCollection = objEmergencyContactBAL.GetEmergencyContact(objEmergencyContact);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetContactDetails", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

        return raveHRCollection;
    }

    /// <summary>
    /// Gets the address.
    /// </summary>
    /// <param name="employeeObj">The employee obj.</param>
    /// <returns></returns>
    private BusinessEntities.RaveHRCollection GetAddress(BusinessEntities.Employee employeeObj)
    {
        Rave.HR.BusinessLayer.Employee.Address objAddressBAL;
        //BusinessEntities.Employee objEmployee;

        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            objAddressBAL = new Rave.HR.BusinessLayer.Employee.Address();
            //objEmployee = new BusinessEntities.Address();

            //objAddress.EMPId = employeeID;
            employeeObj.EMPId = 4;


            raveHRCollection = objAddressBAL.GetAddress(employeeObj);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetContactDetails", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

        return raveHRCollection;
    }

    /// <summary>
    /// Populates the controls.
    /// </summary>
    private void PopulateControls()
    {
        BusinessEntities.Employee employee = new BusinessEntities.Employee();
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            //if (Session[SessionNames.EMPLOYEEDETAILS] != null)
            //{
            //    employee = (BusinessEntities.Employee)Session[SessionNames.EMPLOYEEDETAILS];
                 raveHRCollection = this.GetAddress(employee);
            //}
            //else
            //{
            //    //if (UserMailId != string.Empty && UserMailId != null)
            //    //    raveHRCollection = this.GetAddress(UserMailId);
            //}
            //Session[Common.SessionNames.EMPLOYEEDETAILS] = employee;

            //if (employee == null)
            //{
            //    Response.Redirect(CommonConstants.PAGE_HOME, false);
            //    return;
            //}
           
            //if (UserMailId.ToLower() == employee.EmailId.ToLower())
            //{
            //    if (arrRolesForUser.Count > 0)
            //    {
            //        btnEdit.Visible = true;
            //    }
            //}
            //else
            //{
            //    if (arrRolesForUser.Count > 0)
            //    {
            //        //if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
            //        //{
            //        btnEdit.Visible = true;
            //        //}
            //    }
            //}
            pnlPermanenetAddr.Visible = false;

            if (raveHRCollection != null)
            {

                BusinessEntities.Address obj = null;

                for (int i = 0; i < raveHRCollection.Count; i++)
                {
                    obj = (BusinessEntities.Address)raveHRCollection.Item(i);

                    if (obj.AddressType == (int)MasterEnum.AddressType.Current)
                    {
                        txtCHouseNo.Text = obj.FlatNo;
                        txtCApartment.Text = obj.BuildingName;
                        txtCStreetName.Text = obj.Street;
                        txtCLandmark.Text = obj.Landmark;
                        txtCCity.Text = obj.City;
                        txtCState.Text = obj.State;
                        txtCCountry.Text = obj.Country;
                        txtCPinCode.Text = obj.Pincode;

                        EMPAddressId.Value = obj.EmployeeAddressId.ToString();
                    }

                    if (obj.AddressType == (int)MasterEnum.AddressType.Permanent)
                    {
                        pnlPermanenetAddr.Visible = true;
                        rblSamePerAddress.SelectedIndex = 1;
                        txtPHouseNo.Text = obj.FlatNo;
                        txtPApartment.Text = obj.BuildingName;
                        txtPStreetName.Text = obj.Street;
                        txtPLandmark.Text = obj.Landmark;
                        txtPCity.Text = obj.City;
                        txtPState.Text = obj.State;
                        txtPCountry.Text = obj.Country;
                        txtPPincode.Text = obj.Pincode;
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "PopulateControls", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Gets the employee relation.
    /// </summary>
    /// <returns></returns>
    private BusinessEntities.RaveHRCollection GetEmployeeRelation()
    {
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        return raveHrColl = master.FillDropDownsBL((int)Common.EnumsConstants.Category.EmployeeRelation);

    }

    /// <summary>
    /// Clears the controls.
    /// </summary>
    private void ClearControls()
    {
        txtCHouseNo.Text = string.Empty;
        txtCApartment.Text = string.Empty;
        txtCCity.Text = string.Empty;
        txtCCountry.Text = string.Empty;
        txtCLandmark.Text = string.Empty;
        txtCPinCode.Text = string.Empty;
        txtCState.Text = string.Empty;
        txtCStreetName.Text = string.Empty;
        txtCHouseNo.Focus();
        rblSamePerAddress.SelectedIndex = 0;

        txtPHouseNo.Text = string.Empty;
        txtPApartment.Text = string.Empty;
        txtPCity.Text = string.Empty;
        txtPCountry.Text = string.Empty;
        txtPLandmark.Text = string.Empty;
        txtPPincode.Text = string.Empty;
        txtPState.Text = string.Empty;
        txtPStreetName.Text = string.Empty;
    }

    #endregion

    
}



