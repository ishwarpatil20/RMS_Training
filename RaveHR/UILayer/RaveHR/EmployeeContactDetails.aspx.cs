using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using Common;
using Common.AuthorizationManager;

public partial class EmployeeContactDetails : BaseClass
{
    #region Private Field Members

    private string CONTACTTYPE = "contacttype";
    private int employeeID = 0;
    private BusinessEntities.Employee employee;
    private string CLASS_NAME = "EmployeeContactDetails";
    private string IMGBTNDELETE = "imgBtnDelete";
    private string IMGBTNEDIT = "imgBtnEdit";
    private string MODE = "Mode";
    string PageMode = string.Empty;
    string UserRaveDomainId = string.Empty;
    string UserMailId = string.Empty;
    ArrayList arrRolesForUser = new ArrayList();
    private string EMPRELATION = "EmployeeRelation";
    Regex regexObj = null;
    private static bool Flag = false;
    private static bool IsSeatAllocateFlag;
    const string PAGETYPE = "pagetype";
    const string PAGETYPEEMPLOYEESUMMERY = "EMPLOYEESUMMERY";

    protected EmployeeMenuUC BubbleControl;

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

        // 26878-Ambar-Start
        // Commented following lines
        //txtCFlatNo.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return ValidateSpecialCharacters();");
        //txtCApartment.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return ValidateSpecialCharacters();");
        //txtCStreetName.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return ValidateSpecialCharacters();");
        //txtCLandmark.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return ValidateSpecialCharacters();");
        //txtPFlatNo.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return ValidateSpecialCharacters();");
        //txtPApartment.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return ValidateSpecialCharacters();");
        //txtPStreetName.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return ValidateSpecialCharacters();");
        //txtPLandmark.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return ValidateSpecialCharacters();");
        // 26878-Ambar-End

        txtCPinCode.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtCPinCode.ClientID + "','" + imgCPinCode.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
        imgCPinCode.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanCPinCode.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgCPinCode.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanCPinCode.ClientID + "');");

        txtCPinCode.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return Max_Length();");

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
                lblempName.Text = employee.FirstName.ToUpper() + " " + employee.LastName.ToUpper();
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
                EMPPerAddressId.Value = "0";

                this.PopulateGrid(employeeID);
                this.PopulateEmergencyGrid(employeeID);
                //To solved the issue no 19221
                //Comment by Rahul P 
                //Start
                this.PopulateControls(employeeID);
                //End
            }

            if (Session[SessionNames.PAGEMODE] != null)
            {
                PageMode = Session[SessionNames.PAGEMODE].ToString();

                //if (PageMode == Common.MasterEnum.PageModeEnum.View.ToString())
                if (PageMode == Common.MasterEnum.PageModeEnum.View.ToString() && txtCApartment.Text != string.Empty && IsPostBack==false)
                {
                    this.EnableDisableControl();
                    btnUpdate.Visible = false;
                }
                else
                {
                    btnEdit.Visible = false;
                    btnUpdate.Visible = true;
                }

                SavedControlVirtualPath = "~/EmployeeMenuUC.ascx";
                ReloadControl();
            }

            //Ishwar: Issue Id - 54410 : 'To check whether user has access to edit Employee's details on Employee Summary page' starts
            if (UserMailId.ToLower() != employee.EmailId.ToLower() && arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEPM))
            {
                btnEdit.Visible = false;
            }
            //Ishwar: Issue Id - 54410 : 'To check whether user has access to edit Employee's details on Employee Summary page' ends
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

            if (txtPFlatNo.Text != string.Empty && txtPStreetName.Text != string.Empty && txtPPincode.Text != string.Empty && txtPCity.Text != string.Empty && !arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
            {
                txtPFlatNo.Enabled = false;
                txtPStreetName.Enabled = false;
                txtPCity.Enabled = false;
                txtPPincode.Enabled = false;
                txtPApartment.Enabled = false;
                txtPCountry.Enabled = false;
                txtPFlatNo.Enabled = false;
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
        btnEdit.Visible = false;
        this.EnableControls();
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

            if (ContactNo.Text == string.Empty)
            {
                lblContactError.Text = "Contact No" + " is mandatory.";
                return;
            }

            regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
            if (CountryCode.Text != string.Empty)
            {
                if (!regexObj.IsMatch(CountryCode.Text))
                {
                    lblContactError.Text = "Country Code" + " should have numeric only.";
                    return;
                }
                else if (!IsValueNonZero(CountryCode.Text))
                {
                    lblContactError.Text = "Country Code" + " cannot be zero.";
                    return;
                }

            }

            regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
            if (CityCode.Text != string.Empty)
            {
                if (!regexObj.IsMatch(CityCode.Text))
                {
                    lblContactError.Text = "City Code" + " should have numeric only.";
                    return;
                }
                else if (!IsValueNonZero(CityCode.Text))
                {
                    lblContactError.Text = "City Code" + " cannot be zero.";
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

                if (!IsValueNonZero(ContactNo.Text))
                {
                    lblContactError.Text = "Contact No" + " cannot be zero.";
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
                else if (!IsValueNonZero(Extension.Text))
                {
                    lblContactError.Text = "Extension" + " cannot be zero.";
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

            regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
            if (AvailabilityTime.Text != string.Empty)
            {
                if (regexObj.IsMatch(AvailabilityTime.Text))
                {
                    if (!IsValueNonZero(AvailabilityTime.Text))
                    {
                        lblContactError.Text = "Availability Time" + " cannot be zero.";
                        return;
                    }
                }
            }
            BusinessEntities.ContactDetails objContactDetails = new BusinessEntities.ContactDetails();

            objContactDetails.ContactType = int.Parse(ddlContactType.SelectedValue);
            if (!string.IsNullOrEmpty(CityCode.Text))
                objContactDetails.CityCode = int.Parse(CityCode.Text.Trim());
            else
                objContactDetails.CityCode = CommonConstants.ZERO;

            if (!string.IsNullOrEmpty(CountryCode.Text))
                objContactDetails.CountryCode = int.Parse(CountryCode.Text.Trim());
            else
                objContactDetails.CountryCode = CommonConstants.ZERO;

            objContactDetails.ContactNo = ContactNo.Text.Trim();

            if (!string.IsNullOrEmpty(Extension.Text))
                objContactDetails.Extension = int.Parse(Extension.Text.Trim());
            else
                objContactDetails.Extension = CommonConstants.ZERO;

            if (!string.IsNullOrEmpty(AvailabilityTime.Text))
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

            if (ContactNo.Text == string.Empty)
            {
                lblContactError.Text = "Contact No" + " is mandatory.";
                return;
            }

            regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
            if (CountryCode.Text != string.Empty)
            {
                if (!regexObj.IsMatch(CountryCode.Text))
                {
                    lblContactError.Text = "Country Code" + " should have numeric only.";
                    return;
                }
                else if (!IsValueNonZero(CountryCode.Text))
                {
                    lblContactError.Text = "Country Code" + " cannot be zero.";
                    return;
                }

            }

            regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
            if (CityCode.Text != string.Empty)
            {
                if (!regexObj.IsMatch(CityCode.Text))
                {
                    lblContactError.Text = "City Code" + " should have numeric only.";
                    return;
                }
                else if (!IsValueNonZero(CityCode.Text))
                {
                    lblContactError.Text = "City Code" + " cannot be zero.";
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
                if (!IsValueNonZero(ContactNo.Text))
                {
                    lblContactError.Text = "Contact No" + " cannot be zero.";
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
                else if (!IsValueNonZero(Extension.Text))
                {
                    lblContactError.Text = "Extension" + " cannot be zero.";
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

            regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
            if (AvailabilityTime.Text != string.Empty)
            {
                if (regexObj.IsMatch(AvailabilityTime.Text))
                {
                    if (!IsValueNonZero(AvailabilityTime.Text))
                    {
                        lblContactError.Text = "Availability Time" + " cannot be zero.";
                        return;
                    }
                }
            }

            BusinessEntities.ContactDetails objContactDetails = new BusinessEntities.ContactDetails();

            objContactDetails.ContactType = int.Parse(ddlContactType.SelectedValue);
            if (!string.IsNullOrEmpty(CityCode.Text))
                objContactDetails.CityCode = int.Parse(CityCode.Text.Trim());
            else
                objContactDetails.CityCode = CommonConstants.ZERO;

            if (!string.IsNullOrEmpty(CountryCode.Text))
                objContactDetails.CountryCode = int.Parse(CountryCode.Text.Trim());


            else
                objContactDetails.CountryCode = CommonConstants.ZERO;

            objContactDetails.ContactNo = ContactNo.Text.Trim();

            if (!string.IsNullOrEmpty(Extension.Text))
                objContactDetails.Extension = int.Parse(Extension.Text.Trim());
            else
                objContactDetails.Extension = CommonConstants.ZERO;

            if (!string.IsNullOrEmpty(AvailabilityTime.Text))
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

        if (ContactNo.Text == string.Empty)
        {
            lblContactError.Text = "Contact No" + " is mandatory.";
            return;
        }

        regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
        if (CountryCode.Text != string.Empty)
        {
            if (!regexObj.IsMatch(CountryCode.Text))
            {
                lblContactError.Text = "Country Code" + " should have numeric only.";
                return;
            }
            else if (!IsValueNonZero(CountryCode.Text))
            {
                lblContactError.Text = "Country Code" + " cannot be zero.";
                return;
            }

        }

        regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
        if (CityCode.Text != string.Empty)
        {
            if (!regexObj.IsMatch(CityCode.Text))
            {
                lblContactError.Text = "City Code" + " should have numeric only.";
                return;
            }
            else if (!IsValueNonZero(CityCode.Text))
            {
                lblContactError.Text = "City Code" + " cannot be zero.";
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

            if (!IsValueNonZero(ContactNo.Text))
            {
                lblContactError.Text = "Contact No" + " cannot be zero.";
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
            else if (!IsValueNonZero(Extension.Text))
            {
                lblContactError.Text = "Extension" + " cannot be zero.";
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

        regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
        if (AvailabilityTime.Text != string.Empty)
        {
            if (regexObj.IsMatch(AvailabilityTime.Text))
            {
                if (!IsValueNonZero(AvailabilityTime.Text))
                {
                    lblContactError.Text = "Availability Time" + " cannot be zero.";
                    return;
                }
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
        
        // 26859-Ambar-Start
        // Assign Value to EMPId.Value
        EMPId.Value = employee.EMPId.ToString();
        // 26859-Ambar-End

        objContactDetails.ContactType = int.Parse(ddlContactType.SelectedValue);
        //objContactDetails.EMPId = int.Parse(EMPId.Value);
        if (EMPId.Value == "" || EMPId.Value == string.Empty) return;
        objContactDetails.EMPId = int.Parse(EMPId.Value);
        objContactDetails.ContactTypeName = ddlContactType.SelectedItem.Text;

        if (!string.IsNullOrEmpty(CityCode.Text))
            objContactDetails.CityCode = int.Parse(CityCode.Text.Trim());
        else
            objContactDetails.CityCode = CommonConstants.ZERO;

        if (!string.IsNullOrEmpty(CountryCode.Text))
            objContactDetails.CountryCode = int.Parse(CountryCode.Text.Trim());
        else
            objContactDetails.CountryCode = CommonConstants.ZERO;

        objContactDetails.ContactNo = ContactNo.Text.Trim();

        if (!string.IsNullOrEmpty(Extension.Text))
            objContactDetails.Extension = int.Parse(Extension.Text.Trim());
        else
            objContactDetails.Extension = CommonConstants.ZERO;

        if (!string.IsNullOrEmpty(AvailabilityTime.Text))
            objContactDetails.AvalibilityTime = AvailabilityTime.Text.Trim();
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

            if (gvEmergencyDetails.EditIndex != -1)
            {
                lblMessage.Text = "Please update Emergency details.";
                return;
            }

            if (gvContactDetails.EditIndex != -1)
            {
                lblMessage.Text = "Please update Contact Number.";
                return;
            }
            //To solved the issue no 19221
            //Comment by Rahul P 
            //Start
            //if (gvEmergencyDetails.Rows.Count == 0)
            if (gvEmergencyDetails.EditIndex != -1)
            {
            //End
                lblMessage.Text = "Please provide atleast one Emergency Contact Number.";
                return;
            }

            objContactDetailsBAL = new Rave.HR.BusinessLayer.Employee.ContactDetails();
            EMPId.Value = employee.EMPId.ToString();
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
                if (EMPId.Value == "" || EMPId.Value == string.Empty) return;
                objContactDetails.EMPId = int.Parse(EMPId.Value);
                objContactDetails.ContactTypeName = ContactType.Text.Trim();
                if (!string.IsNullOrEmpty(CityCode.Text))
                    objContactDetails.CityCode = int.Parse(CityCode.Text.Trim());
                else
                    objContactDetails.CityCode = CommonConstants.ZERO;

                if (!string.IsNullOrEmpty(CountryCode.Text))
                    objContactDetails.CountryCode = int.Parse(CountryCode.Text.Trim());
                else
                    objContactDetails.CountryCode = CommonConstants.ZERO;

                objContactDetails.ContactNo = ContactNo.Text.Trim();

                if (!string.IsNullOrEmpty(Extension.Text))
                    objContactDetails.Extension = int.Parse(Extension.Text.Trim());
                else
                    objContactDetails.Extension = CommonConstants.ZERO;

                if (!string.IsNullOrEmpty(AvailabilityTime.Text))
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

            if (!IsSeatAllocateFlag && !string.IsNullOrEmpty(txtSeatNumber.Text.Trim()))
            {
                objContactDetails = new BusinessEntities.ContactDetails();

                objContactDetails.EMPId = int.Parse(EMPId.Value);
                objContactDetails.SeatName = txtSeatNumber.Text.Trim();

                int status = objContactDetailsBAL.AllocateSeat(objContactDetails);

                if (status == 1)
                {
                    lblMessage.Text = "Seat Number " + txtSeatNumber.Text + " is allocated for another Employee.";
                    return;
                }
                if (status == 2)
                {
                    lblMessage.Text = "Seat Number " + txtSeatNumber.Text + " is not present in database.";
                    return;
                }
            }

            objContactDetailsBAL.Manipulation(objSaveContactDetailsCollection);

            objAddressBAL = new Rave.HR.BusinessLayer.Employee.Address();
            objAddress = new BusinessEntities.Address();

            objAddress.FlatNo = txtCFlatNo.Text.Trim();
            objAddress.BuildingName = txtCApartment.Text.Trim();
            objAddress.Street = txtCStreetName.Text.Trim();
            objAddress.Landmark = txtCLandmark.Text.Trim();
            objAddress.City = txtCCity.Text.Trim();
            objAddress.State = txtCState.Text.Trim();
            objAddress.Country = txtCCountry.Text.Trim();
            objAddress.Pincode = txtCPinCode.Text.Trim();
            objAddress.AddressType = (int)MasterEnum.AddressType.Current;
            if (EMPId.Value == "" || EMPId.Value == string.Empty) return;
            objAddress.EMPId = int.Parse(EMPId.Value);
            objAddress.CreatedById = UserMailId;
            objAddress.LastModifiedById = UserMailId;
            ////To solved the issue no 19221
            //Comment Added by Rahul P 
            //Start
            if (EMPAddressId.Value.Length == 0)
                EMPAddressId.Value = "0";
            //End
            objAddress.EmployeeAddressId = int.Parse(EMPAddressId.Value);
            objAddress.IsActive = true;

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

                objAddress.FlatNo = txtPFlatNo.Text.Trim();
                objAddress.BuildingName = txtPApartment.Text.Trim();
                objAddress.Street = txtPStreetName.Text.Trim();
                objAddress.Landmark = txtPLandmark.Text.Trim();
                objAddress.City = txtPCity.Text.Trim();
                objAddress.State = txtPState.Text.Trim();
                objAddress.Country = txtPCountry.Text.Trim();
                objAddress.Pincode = txtPPincode.Text.Trim();
                objAddress.AddressType = (int)MasterEnum.AddressType.Permanent;
                if (EMPId.Value == "" || EMPId.Value == string.Empty) return;
                objAddress.EMPId = int.Parse(EMPId.Value);
                objAddress.CreatedById = UserMailId;
                objAddress.LastModifiedById = UserMailId;
                //To solved the issue no 19221
                //Comment by Rahul P 
                //Start
                if (EMPPerAddressId.Value.Length == 0)
                    EMPPerAddressId.Value = "0";
                //End
                objAddress.EmployeeAddressId = int.Parse(EMPPerAddressId.Value);
                objAddress.IsActive = true;

                objAddressBAL.ManipulateAddress(objAddress);

            }
            else if (EMPPerAddressId.Value != "0" && rblSamePerAddress.SelectedIndex != CommonConstants.ONE)
            {
                objAddress = new BusinessEntities.Address();

                objAddress.AddressType = (int)MasterEnum.AddressType.Permanent;
                if (EMPId.Value == "" || EMPId.Value == string.Empty) return;
                objAddress.EMPId = int.Parse(EMPId.Value);
                objAddress.CreatedById = UserMailId;
                objAddress.LastModifiedById = UserMailId;
                //To solved the issue no 19221
                //Comment by Rahul P 
                //Start
                if (EMPPerAddressId.Value.Length == 0)
                    EMPPerAddressId.Value = "0";
                //End
                objAddress.EmployeeAddressId = int.Parse(EMPPerAddressId.Value);

                objAddress.IsActive = false;
                objAddressBAL.ManipulateAddress(objAddress);

                this.ClearPermanentControls();
                EMPPerAddressId.Value = "0";

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
                if (EMPId.Value == "" || EMPId.Value == string.Empty) return;
                objEmergencyContact.EMPId = int.Parse(EMPId.Value);
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
                lblempName.Text = employee.FirstName.ToUpper() + " " + employee.LastName.ToUpper();
                this.PopulateGrid(empID);
                this.PopulateEmergencyGrid(empID);
                //To solved the issue no 19221
                //Comment by Rahul P 
                //Start
                this.PopulateControls(empID);
                //End
                this.EnableDisableControl();
                btnUpdate.Visible = false;
                btnEdit.Visible = true;
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
                lblEmrgencyError.Text = "Person Name is mandatory.";
                return;
            }

            if (Relation.SelectedItem.Text == CommonConstants.SELECT)
            {
                lblEmrgencyError.Text = "Please select a Relation.";
                return;
            }

            if (ContactNo.Text == string.Empty)
            {
                lblEmrgencyError.Text = "Contact No is mandatory.";
                return;
            }

            regexObj = new Regex(CommonConstants.ALPHABET_WITHSPACE);
            if (PersonName.Text != string.Empty)
            {
                if (!regexObj.IsMatch(PersonName.Text))
                {
                    lblEmrgencyError.Text = "Person Name should have alphabets only.";
                    return;
                }
            }

            regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
            if (ContactNo.Text != string.Empty)
            {
                if (!regexObj.IsMatch(ContactNo.Text))
                {
                    lblEmrgencyError.Text = "Contact No should have numeric only.";
                    return;
                }

                if (!IsValueNonZero(ContactNo.Text))
                {
                    lblEmrgencyError.Text = "Contact No" + " cannot be zero.";
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
                lblEmrgencyError.Text = "Person Name is mandatory.";
                return;
            }

            if (Relation.SelectedItem.Text == CommonConstants.SELECT)
            {
                lblEmrgencyError.Text = "Please select a Relation.";
                return;
            }

            if (ContactNo.Text == string.Empty)
            {
                lblEmrgencyError.Text = "Contact No is mandatory.";
                return;
            }

            regexObj = new Regex(CommonConstants.ALPHABET_WITHSPACE);
            if (PersonName.Text != string.Empty)
            {
                if (!regexObj.IsMatch(PersonName.Text))
                {
                    lblEmrgencyError.Text = "Person Name should have alphabets only.";
                    return;
                }
            }

            regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
            if (ContactNo.Text != string.Empty)
            {
                if (!regexObj.IsMatch(ContactNo.Text))
                {
                    lblEmrgencyError.Text = "Contact No should have numeric only.";
                    return;
                }

                if (!IsValueNonZero(ContactNo.Text))
                {
                    lblEmrgencyError.Text = "Contact No" + " cannot be zero.";
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
        TextBox ContactNo = (TextBox)gvEmergencyDetails.Rows[e.RowIndex].FindControl("txtContactNo");
        ImageButton BtnUpdate = (ImageButton)gvEmergencyDetails.Rows[e.RowIndex].FindControl("imgBtnUpdate");
        ImageButton BtnCancel = (ImageButton)gvEmergencyDetails.Rows[e.RowIndex].FindControl("imgBtnCancel");
        Label Mode = (Label)gvEmergencyDetails.Rows[e.RowIndex].FindControl(MODE);
        DropDownList ddlRelationType = (DropDownList)gvEmergencyDetails.Rows[e.RowIndex].FindControl("ddlRelation");

        if (PersonName.Text == string.Empty)
        {
            lblEmrgencyError.Text = "Person Name is mandatory.";
            return;
        }

        if (ddlRelationType.SelectedItem.Text == CommonConstants.SELECT)
        {
            lblEmrgencyError.Text = "Please select a Relation.";
            return;
        }

        if (ContactNo.Text == string.Empty)
        {
            lblEmrgencyError.Text = "Contact No is mandatory.";
            return;
        }

        regexObj = new Regex(CommonConstants.ALPHABET_WITHSPACE);
        if (PersonName.Text != string.Empty)
        {
            if (!regexObj.IsMatch(PersonName.Text))
            {
                lblEmrgencyError.Text = "Person Name should have alphabets only.";
                return;
            }
        }

        regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
        if (ContactNo.Text != string.Empty)
        {
            if (!regexObj.IsMatch(ContactNo.Text))
            {
                lblEmrgencyError.Text = "Contact Number should have numeric only.";
                return;
            }
            if (!IsValueNonZero(ContactNo.Text))
            {
                lblEmrgencyError.Text = "Contact No" + " cannot be zero.";
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

        // 26859-Ambar-Start
        // Assign Value to EMPId.Value
        EMPId.Value = employee.EMPId.ToString();
        // 26859-Ambar-End

        if (EMPId.Value == "" || EMPId.Value == string.Empty) return;
        objEmergencyContact.EMPId = int.Parse(EMPId.Value);
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
        //To solved the issue no 19221
        //Comment by Rahul P 
        //Start
        this.PopulateControls(employee.EMPId);
        //End
    }

    /// <summary>
    /// Handles the Click event of the btnCancel control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Request.QueryString[PAGETYPE] != null)
        {
            if (DecryptQueryString(PAGETYPE) == PAGETYPEEMPLOYEESUMMERY)
            {
                Response.Redirect("EmployeeDetails.aspx?" + URLHelper.SecureParameters("EmpId", employeeID.ToString()) + "&" + URLHelper.SecureParameters("index", Session["SelectedRow"].ToString()) + "&" + URLHelper.SecureParameters("pagetype", "EMPLOYEESUMMERY") + "&" + "&" + URLHelper.CreateSignature(employeeID.ToString(), Session["SelectedRow"].ToString(), "EMPLOYEESUMMERY"));
            }
        }
        else
        {
            Response.Redirect(CommonConstants.PAGE_EMPLOYEEDETAILS, false);
        }

    }

    #endregion

    #region Private Member Functions

    /// <summary>
    /// Does the data bind.
    /// </summary>
    private void DoDataBind()
    {
        gvContactDetails.DataSource = ContactdetailsCollection;
        gvContactDetails.DataBind();

        //if (gvContactDetails.EditIndex == -1)
        //{
        for (int i = 0; i < gvContactDetails.Rows.Count; i++)
        {
            if (gvContactDetails.EditIndex != i)
            {
                Label CityCode = (Label)gvContactDetails.Rows[i].FindControl("lblCityCode");
                Label CountryCode = (Label)gvContactDetails.Rows[i].FindControl("lblCountryCode");
                Label Extension = (Label)gvContactDetails.Rows[i].FindControl("lblExtension");

                if (CityCode.Text == CommonConstants.ZERO.ToString())
                    CityCode.Text = string.Empty;

                if (CountryCode.Text == CommonConstants.ZERO.ToString())
                    CountryCode.Text = string.Empty;

                if (Extension.Text == CommonConstants.ZERO.ToString())
                    Extension.Text = string.Empty;
            }
            else
            {
                TextBox CityCode = (TextBox)gvContactDetails.Rows[gvContactDetails.EditIndex].Cells[1].FindControl("txtCityCode");
                TextBox CountryCode = (TextBox)gvContactDetails.Rows[gvContactDetails.EditIndex].Cells[2].FindControl("txtCountryCode");
                //TextBox ContactNo = (TextBox)gvContactDetails.FindControl("txtAddContactNo");
                TextBox Extension = (TextBox)gvContactDetails.Rows[gvContactDetails.EditIndex].Cells[4].FindControl("txtExtension");

                if (CityCode.Text == CommonConstants.ZERO.ToString())
                    CityCode.Text = string.Empty;

                if (CountryCode.Text == CommonConstants.ZERO.ToString())
                    CountryCode.Text = string.Empty;

                if (Extension.Text == CommonConstants.ZERO.ToString())
                    Extension.Text = string.Empty;
                //}
            }

        }
        //}
        //else
        //{
        //    //for (int i = 0; i < gvContactDetails.Rows.Count; i++)
        //    //{
        //        TextBox CityCode = (TextBox)gvContactDetails.Rows[gvContactDetails.EditIndex].Cells[1].FindControl("txtCityCode");
        //        TextBox CountryCode = (TextBox)gvContactDetails.Rows[gvContactDetails.EditIndex].Cells[2].FindControl("txtCountryCode");
        //        //TextBox ContactNo = (TextBox)gvContactDetails.FindControl("txtAddContactNo");
        //        TextBox Extension = (TextBox)gvContactDetails.Rows[gvContactDetails.EditIndex].Cells[4].FindControl("txtExtension");

        //        if (CityCode.Text == CommonConstants.ZERO.ToString())
        //            CityCode.Text = string.Empty;

        //        if (CountryCode.Text == CommonConstants.ZERO.ToString())
        //            CountryCode.Text = string.Empty;

        //        if (Extension.Text == CommonConstants.ZERO.ToString())
        //            Extension.Text = string.Empty;
        //    //}
        //}
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


        for (int i = 0; i < gvContactDetails.Rows.Count; i++)
        {
            Label CityCode = (Label)gvContactDetails.Rows[i].FindControl("lblCityCode");
            Label CountryCode = (Label)gvContactDetails.Rows[i].FindControl("lblCountryCode");
            Label Extension = (Label)gvContactDetails.Rows[i].FindControl("lblExtension");

            if (CityCode.Text == CommonConstants.ZERO.ToString())
                CityCode.Text = string.Empty;

            if (CountryCode.Text == CommonConstants.ZERO.ToString())
                CountryCode.Text = string.Empty;

            if (Extension.Text == CommonConstants.ZERO.ToString())
                Extension.Text = string.Empty;

        }
        //EMPId.Value = "4";
        EMPId.Value = employeeID.ToString().Trim();

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

        //EMPId.Value = "4";
        EMPId.Value = employeeID.ToString().Trim();

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

            objContactDetails.EMPId = employeeID;

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
        pnlEmergencyDetails.Enabled = false;
        pnlSeatDetails.Enabled = false;
        //To solved the issue no 19221
        //Comment by Rahul P 
        //Start
        pnlContactDetails.Enabled = false;
        //End
    }

    /// <summary>
    /// Enables the disable control.
    /// </summary>
    private void EnableControls()
    {
        if (UserMailId.ToLower() == employee.EmailId.ToLower())
        {
            pnlEmpCurrentAddress.Enabled = true;
            pnlPermanenetAddr.Enabled = true;
            gvContactDetails.Enabled = true;
            pnlEmergencyDetails.Enabled = true;
            pnlSeatDetails.Enabled = true;
            //To solved the issue no 19221
            //Comment by Rahul P 
            //Start
            pnlContactDetails.Enabled = true;
            //End

        }

        if (UserMailId.ToLower() != employee.EmailId.ToLower())
        {
            if (arrRolesForUser.Count > 0)
            {
                if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
                {
                    pnlEmpCurrentAddress.Enabled = true;
                    pnlPermanenetAddr.Enabled = true;
                    gvContactDetails.Enabled = true;
                    pnlEmergencyDetails.Enabled = true;
                    pnlSeatDetails.Enabled = true;
                    //To solved the issue no 19221
                    //Comment by Rahul P 
                    //Start
                    pnlContactDetails.Enabled = true;
                    //End
                }
                else
                {
                    btnUpdate.Visible = false;
                    btnEdit.Visible = true;
                }
            }

        }
        if (txtSeatNumber.Text != "")

            txtSeatNumber.Enabled = false;
        else
            txtSeatNumber.Enabled = true;
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

            objEmergencyContact.EMPId = employeeID;
            //objEmergencyContact.EMPId = 4;

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

            //employeeObj.EMPId = employeeID;
            //employeeObj.EMPId = 4;



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
    private void PopulateControls(int EMPId)
    {
        BusinessEntities.Employee employee = new BusinessEntities.Employee();
        BusinessEntities.RaveHRCollection raveHRCollection; //= new BusinessEntities.RaveHRCollection();

        try
        {
            employee.EMPId = EMPId;
            raveHRCollection = this.GetAddress(employee);

            pnlPermanenetAddr.Visible = false;

            if (raveHRCollection != null)
            {

                BusinessEntities.Address obj = null;
                if (raveHRCollection.Count > 0)
                {
                    for (int i = 0; i < raveHRCollection.Count; i++)
                    {
                        obj = (BusinessEntities.Address)raveHRCollection.Item(i);

                        if (obj.AddressType == (int)MasterEnum.AddressType.Current)
                        {
                            if (obj.FlatNo.Trim() != "")
                                txtCFlatNo.Text = obj.FlatNo.Trim();
                            else
                                txtCFlatNo.Text = "";
                            if (obj.BuildingName.Trim() != "")
                                txtCApartment.Text = obj.BuildingName.Trim();
                            else
                                txtCApartment.Text = "";
                            if (obj.Street.Trim() != "")
                                txtCStreetName.Text = obj.Street.Trim();
                            else
                                txtCStreetName.Text = "";
                            txtCLandmark.Text = obj.Landmark.Trim();
                            txtCCity.Text = obj.City.Trim();
                            txtCState.Text = obj.State.Trim();
                            txtCCountry.Text = obj.Country.Trim();
                            txtCPinCode.Text = obj.Pincode.Trim();
                            btnChangeAddress.Visible = true;
                            btnCanelChange.Visible = true;
                            EMPAddressId.Value = obj.EmployeeAddressId.ToString();
                        }

                        if (obj.AddressType == (int)MasterEnum.AddressType.Permanent)
                        {
                            pnlPermanenetAddr.Visible = true;
                            rblSamePerAddress.SelectedIndex = 1;
                            txtPFlatNo.Text = obj.FlatNo.Trim();
                            txtPApartment.Text = obj.BuildingName.Trim();
                            txtPStreetName.Text = obj.Street.Trim();
                            txtPLandmark.Text = obj.Landmark.Trim();
                            txtPCity.Text = obj.City.Trim();
                            txtPState.Text = obj.State.Trim();
                            txtPCountry.Text = obj.Country.Trim();
                            txtPPincode.Text = obj.Pincode.Trim();

                            EMPPerAddressId.Value = obj.EmployeeAddressId.ToString();
                        }
                    }
                }
                else
                {
                    ClearPermanentControls();
                    ClearControls();
                }
                

                txtSeatNumber.Text = this.GetSeatDetails(employee.EMPId);
                if (!string.IsNullOrEmpty(txtSeatNumber.Text))
                {
                    txtSeatNumber.ReadOnly = true;
                    IsSeatAllocateFlag = true;
                }
                else
                {
                    txtSeatNumber.ReadOnly = false;
                    IsSeatAllocateFlag = false;
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
        txtCFlatNo.Text = string.Empty;
        txtCApartment.Text = string.Empty;
        txtCCity.Text = string.Empty;
        txtCCountry.Text = string.Empty;
        txtCLandmark.Text = string.Empty;
        txtCPinCode.Text = string.Empty;
        txtCState.Text = string.Empty;
        txtCStreetName.Text = string.Empty;
        txtCFlatNo.Focus();
        //rblSamePerAddress.SelectedIndex = 0;

        //txtPFlatNo.Text = string.Empty;
        //txtPApartment.Text = string.Empty;
        //txtPCity.Text = string.Empty;
        //txtPCountry.Text = string.Empty;
        //txtPLandmark.Text = string.Empty;
        //txtPPincode.Text = string.Empty;
        //txtPState.Text = string.Empty;
        //txtPStreetName.Text = string.Empty;
    }

    /// <summary>
    /// Clears the permanent controls.
    /// </summary>
    private void ClearPermanentControls()
    {
        txtPFlatNo.Text = string.Empty;
        txtPApartment.Text = string.Empty;
        txtPCity.Text = string.Empty;
        txtPCountry.Text = string.Empty;
        txtPLandmark.Text = string.Empty;
        txtPPincode.Text = string.Empty;
        txtPState.Text = string.Empty;
        txtPStreetName.Text = string.Empty;
    }

    /// <summary>
    /// Determines whether [is value non zero] [the specified value].
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>
    /// 	<c>true</c> if [is value non zero] [the specified value]; otherwise, <c>false</c>.
    /// </returns>
    private bool IsValueNonZero(string value)
    {
        Int64 decVal = Convert.ToInt64(Convert.ToDecimal(value));
        if (value.ToString().Trim().Equals("0") || value.ToString().Trim().Equals("0.0") || value.ToString().Trim().Equals("0.00") || decVal == 0)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// Gets the contact details.
    /// </summary>
    /// <param name="employeeID">The employee ID.</param>
    /// <returns></returns>
    private string GetSeatDetails(int employeeID)
    {
        Rave.HR.BusinessLayer.Employee.ContactDetails objContactDetailsBAL;
        BusinessEntities.ContactDetails objContactDetails;

        string seatName = string.Empty;

        try
        {
            objContactDetailsBAL = new Rave.HR.BusinessLayer.Employee.ContactDetails();
            objContactDetails = new BusinessEntities.ContactDetails();

            objContactDetails.EMPId = employeeID;

            seatName = objContactDetailsBAL.GetSeatDetails(objContactDetails);
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

        return seatName;
    }

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
            }
        }
    }

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

    private void BubbleControl_BubbleClick(object sender, EventArgs e)
    {
        //this.PopulateControls();
        if (Session[SessionNames.EMPLOYEEDETAILS] != null)
        {
            employee = (BusinessEntities.Employee)Session[SessionNames.EMPLOYEEDETAILS];
            employee = this.GetEmployee(employee);

            //setting Pagemode & default value for hiddenfield.
            Session[SessionNames.PAGEMODE] = Common.MasterEnum.PageModeEnum.View;
            EMPAddressId.Value = "0";
            EMPPerAddressId.Value = "0";

            lblempName.Text = employee.FirstName.ToUpper() + " " + employee.LastName.ToUpper();
            this.PopulateGrid(employee.EMPId);
            this.PopulateEmergencyGrid(employee.EMPId);
            this.PopulateControls(employee.EMPId);
        }
        if (txtCApartment.Text.ToString().Length != 0)
        {
            pnlEmergencyDetails.Enabled = false;
            pnlEmpCurrentAddress.Enabled = false;
            pnlPermanenetAddr.Enabled = false;
            pnlContactDetails.Enabled = false;
            btnEdit.Visible = true;
            btnUpdate.Visible = false;
            //divContactDetails.Visible = false;
        }
        else
        {
            pnlEmergencyDetails.Enabled = true;
            pnlEmpCurrentAddress.Enabled = true;
            pnlPermanenetAddr.Enabled = true;
            pnlContactDetails.Enabled = true;
            btnEdit.Visible = false;
            btnUpdate.Visible = true;
            gvContactDetails.Enabled = true;
            //divContactDetails.Visible = true;
        }

        //Ishwar: Issue Id - 54410 : 30122014 Start
        if (UserMailId.ToLower() != employee.EmailId.ToLower() && arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEPM))
        {
            btnEdit.Visible = false;
        }
        //Ishwar: Issue Id - 54410 : 30122014 End

        Session[Common.SessionNames.EMPLOYEEDETAILS] = employee;
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

    #endregion


}
