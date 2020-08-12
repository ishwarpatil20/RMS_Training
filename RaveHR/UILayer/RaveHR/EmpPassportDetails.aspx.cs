using System;
using System.Text;
using System.Web.UI.WebControls;
using Common;
using Common.AuthorizationManager;
using System.Collections;

public partial class EmpPassportDetails : BaseClass
{
    #region Private Field Members

    #region ViewState Constants

    private string VISADETAILS = "VisaDetails";
    private string VISADETAILSDELETE = "VisaDetailsDelete";
    private string DELETEROWINDEX = "DeleteRowIndex";
    private string ROWINDEX = "RowIndex";

    #endregion ViewState Constants

    private string IMGBTNDELETE = "imgBtnDelete";
    private string IMGBTNEDIT = "imgBtnEdit";
    private string CLASS_NAME = "EmpPassportDetails.aspx";
    private string VISAID = "VisaId";
    private string MODE = "Mode";
    const string ReadOnly = "readonly";
    const string DateFormat = "dd/MM/yyyy";
    char[] SPILITER_SLASH = { '/' };
    string UserRaveDomainId;
    string UserMailId;
    string PageMode; 
    Rave.HR.BusinessLayer.Employee.VisaDetails ObjVisaDetailsBL = new Rave.HR.BusinessLayer.Employee.VisaDetails();
    Rave.HR.BusinessLayer.Employee.Employee objEmployeeBAL = new Rave.HR.BusinessLayer.Employee.Employee();
    AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
    // Get logged-in user's email id
    AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
    ArrayList arrRolesForUser = new ArrayList();

    private int employeeID = 0;
    private BusinessEntities.Employee employee;

    /// <summary>
    /// Defines a constant error for no records found after applying filter criteria
    /// </summary>
    private const string NO_RECORDS_FOUND_MESSAGE = "No records found.";

    #endregion Private Field Members

    #region Local Properties

    private BusinessEntities.RaveHRCollection VisaDetailsCollection
    {
        get
        {
            if (ViewState[VISADETAILS] != null)
                return ((BusinessEntities.RaveHRCollection)ViewState[VISADETAILS]);
            else
                return null;
        }
        set
        {
            ViewState[VISADETAILS] = value;
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
        //Clearing the error label
        lblError.Text = string.Empty;
        lblMessage.Text = string.Empty;

        btnAddRow.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return ButtonClickValidate();");

        btnSave.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return SaveButtonClickValidate();");


        txtCountryName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtCountryName.ClientID + "','" + imgCountryName.ClientID + "','" + Common.CommonConstants.VALIDATE_ISALPHABET + "');");
        imgCountryName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanCountryName.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
        imgCountryName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanCountryName.ClientID + "');");

        txtVisaType.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtVisaType.ClientID + "','" + imgVisaType.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPACE + "');");
        imgVisaType.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanVisaType.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
        imgVisaType.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanVisaType.ClientID + "');");

        txtExpiryDate.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + txtExpiryDate.ClientID + "','','');");
        txtExpiryDate.Attributes.Add(ReadOnly, ReadOnly);
        txtVisaExpiryDate.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + txtVisaExpiryDate.ClientID + "','','');");
        txtVisaExpiryDate.Attributes.Add(ReadOnly, ReadOnly);
        txtIssueDate.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + txtIssueDate.ClientID + "','','');");
        txtIssueDate.Attributes.Add(ReadOnly, ReadOnly);

        txtPassportNo.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtPassportNo.ClientID + "','" + imgPassportNo.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_FUNCTION + "');");
        imgPassportNo.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanPassportNo.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgPassportNo.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanPassportNo.ClientID + "');");

        txtIssuePlace.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtIssuePlace.ClientID + "','" + imgIssuePlace.ClientID + "','" + Common.CommonConstants.VALIDATE_ISALPHABET + "');");
        imgIssuePlace.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanIssuePlace.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
        imgIssuePlace.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanIssuePlace.ClientID + "');");

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

            if (!IsPostBack)
            {
                this.PopulateControl();
                this.PopulateGrid(employeeID);
            }

            // Get logged-in user's email id
            AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
            UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
            UserMailId = UserRaveDomainId.Replace("co.in", "com");


            if (Session[SessionNames.PAGEMODE] != null)
            {
                PageMode = Session[SessionNames.PAGEMODE].ToString();
                arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());

                if (UserMailId.ToLower() == employee.EmailId.ToLower())
                {
                    if (arrRolesForUser.Count > 0)
                    {
                        if (PageMode == Common.MasterEnum.PageModeEnum.Edit.ToString())
                        {
                            passportdetails.Enabled = true;
                            visaDetails.Enabled = true;
                            rbtnValidPassport.Enabled = true;
                            lblValidPassport.Enabled = true;
                            if (rbtnValidPassport.SelectedIndex == CommonConstants.ZERO)
                            {
                                btnSave.Visible = true;
                                btnCancel.Visible = true;
                            }
                        }
                        else
                        {
                            passportdetails.Enabled = false;
                            visaDetails.Enabled = false;
                            rbtnValidPassport.Enabled = false;
                            lblValidPassport.Enabled = false;
                        }
                    }
                }
                else
                {
                    if (PageMode == Common.MasterEnum.PageModeEnum.Edit.ToString() && arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
                    {
                        passportdetails.Enabled = true;
                        visaDetails.Enabled = true;
                        rbtnValidPassport.Enabled = true;
                        lblValidPassport.Enabled = true;
                        if (rbtnValidPassport.SelectedIndex == CommonConstants.ZERO)
                        {
                            btnSave.Visible = true;
                            btnCancel.Visible = true;
                        }
                    }
                    else
                    {
                        passportdetails.Enabled = false;
                        visaDetails.Enabled = false;
                        rbtnValidPassport.Enabled = false;
                        lblValidPassport.Enabled = false;
                    }
                }
            }
        }

    }

    /// <summary>
    /// Handles the Click event of the btnUpdateRow control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnUpdateRow_Click(object sender, EventArgs e)
    {
        if (ValidateVisaControls())
        {

            int rowIndex = 0;
            int deleteRowIndex = -1;

            if (ViewState[DELETEROWINDEX] != null)
            {
                deleteRowIndex = Convert.ToInt32(ViewState[DELETEROWINDEX].ToString());
            }

            //Update the grid view according the row, which is selected for editing.
            if (ViewState[ROWINDEX] != null)
            {
                rowIndex = Convert.ToInt32(ViewState[ROWINDEX].ToString());
                if (deleteRowIndex != -1 && deleteRowIndex < rowIndex) rowIndex--;

                Label VisaId = (Label)gvVisaDetails.Rows[rowIndex].FindControl(VISAID);
                Label Mode = (Label)gvVisaDetails.Rows[rowIndex].FindControl(MODE);

                gvVisaDetails.Rows[rowIndex].Cells[0].Text = txtCountryName.Text;
                gvVisaDetails.Rows[rowIndex].Cells[1].Text = txtVisaType.Text;
                gvVisaDetails.Rows[rowIndex].Cells[2].Text = txtVisaExpiryDate.Text;

                if (int.Parse(VisaId.Text) == 0)
                {
                    Mode.Text = "1";
                }
                else
                {
                    Mode.Text = "2";
                }

                ImageButton btnImg = (ImageButton)gvVisaDetails.Rows[rowIndex].FindControl(IMGBTNDELETE);
                btnImg.Enabled = true;
                ViewState[ROWINDEX] = null;
                ViewState[DELETEROWINDEX] = null;

            }

            for (int i = 0; i < VisaDetailsCollection.Count; i++)
            {
                BusinessEntities.VisaDetails objVisaDetails = new BusinessEntities.VisaDetails();
                objVisaDetails = (BusinessEntities.VisaDetails)VisaDetailsCollection.Item(i);

                Label VisaId = (Label)gvVisaDetails.Rows[i].FindControl(VISAID);
                Label Mode = (Label)gvVisaDetails.Rows[rowIndex].FindControl(MODE);

                objVisaDetails.VisaId = int.Parse(VisaId.Text);
                objVisaDetails.EMPId = int.Parse(EMPId.Value);

                objVisaDetails.CountryName = gvVisaDetails.Rows[i].Cells[0].Text;
                objVisaDetails.VisaType = gvVisaDetails.Rows[i].Cells[1].Text;
                objVisaDetails.ExpiryDate = Convert.ToDateTime(gvVisaDetails.Rows[i].Cells[2].Text);
                objVisaDetails.Mode = int.Parse(Mode.Text);
            }

            //Clear all the fields after inserting row into gridview
            this.ClearControls();

            btnAddRow.Visible = true;
            btnUpdateRow.Visible = false;
            btnCancelRow.Visible = false;

            //Enabling all the edit buttons.
            for (int i = 0; i < gvVisaDetails.Rows.Count; i++)
            {
                ImageButton btnImgEdit = (ImageButton)gvVisaDetails.Rows[i].FindControl(IMGBTNEDIT);
                btnImgEdit.Enabled = true;
            }
        }
    }

    /// <summary>
    /// Handles the Click event of the btnCancelRow control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnCancelRow_Click(object sender, EventArgs e)
    {
        int rowIndex = 0;
        int deleteRowIndex = -1;

        //Clear all the fields after inserting row into gridview
        this.ClearControls();

        if (ViewState[DELETEROWINDEX] != null)
        {
            deleteRowIndex = Convert.ToInt32(ViewState[DELETEROWINDEX].ToString());
        }

        if (ViewState[ROWINDEX] != null)
        {
            rowIndex = Convert.ToInt32(ViewState[ROWINDEX].ToString());
            if (deleteRowIndex != -1 && deleteRowIndex < rowIndex) rowIndex--;
            ImageButton btnImg = (ImageButton)gvVisaDetails.Rows[rowIndex].FindControl(IMGBTNDELETE);
            btnImg.Enabled = true;
            ViewState[ROWINDEX] = null;
            ViewState[DELETEROWINDEX] = null;
        }

        //Enabling all the edit buttons.
        for (int i = 0; i < gvVisaDetails.Rows.Count; i++)
        {
            ImageButton btnImgEdit = (ImageButton)gvVisaDetails.Rows[i].FindControl(IMGBTNEDIT);
            btnImgEdit.Enabled = true;
        }

        btnAddRow.Visible = true;
        btnUpdateRow.Visible = false;
        btnCancelRow.Visible = false;
    }

    /// <summary>
    /// Handles the Click event of the btnAdd control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ValidateVisaControls())
        {
            BusinessEntities.VisaDetails objVisaDetails = new BusinessEntities.VisaDetails();

            if (gvVisaDetails.Rows[0].Cells[0].Text == NO_RECORDS_FOUND_MESSAGE)
            {
                VisaDetailsCollection.Clear();
            }
            objVisaDetails.CountryName = txtCountryName.Text;
            objVisaDetails.VisaType = txtVisaType.Text;
            objVisaDetails.ExpiryDate = Convert.ToDateTime(txtVisaExpiryDate.Text);

            objVisaDetails.Mode = 1;

            VisaDetailsCollection.Add(objVisaDetails);

            this.DoDataBind();

            this.ClearControls();

        }
    }

    /// <summary>
    /// Handles the Click event of the btnSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidateControls())
        {
            Rave.HR.BusinessLayer.Employee.VisaDetails objVisaDetailsBAL;
            //Rave.HR.BusinessLayer.Employee.Employee objEmployeeBAL;

            BusinessEntities.VisaDetails objVisaDetails;
            BusinessEntities.RaveHRCollection objSaveVisaDetailsCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                objEmployeeBAL = new Rave.HR.BusinessLayer.Employee.Employee();

                if (rbtnValidPassport.SelectedIndex == CommonConstants.ZERO)
                {
                    employee.PassportNo = txtPassportNo.Text;
                    employee.PassportIssuePlace = txtIssuePlace.Text;
                    employee.PassportIssueDate = Convert.ToDateTime(txtIssueDate.Text);
                    employee.PassportExpireDate = Convert.ToDateTime(txtExpiryDate.Text);

                    if (rbtnValidVisa.SelectedIndex == CommonConstants.ZERO)
                    {
                        objVisaDetailsBAL = new Rave.HR.BusinessLayer.Employee.VisaDetails();

                        if (gvVisaDetails.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
                        {
                            for (int i = 0; i < gvVisaDetails.Rows.Count; i++)
                            {
                                objVisaDetails = new BusinessEntities.VisaDetails();

                                Label VisaId = (Label)gvVisaDetails.Rows[i].FindControl(VISAID);
                                Label Mode = (Label)gvVisaDetails.Rows[i].FindControl(MODE);

                                objVisaDetails.VisaId = int.Parse(VisaId.Text);
                                objVisaDetails.EMPId = int.Parse(EMPId.Value);
                                objVisaDetails.CountryName = gvVisaDetails.Rows[i].Cells[0].Text;
                                objVisaDetails.VisaType = gvVisaDetails.Rows[i].Cells[1].Text;
                                objVisaDetails.ExpiryDate = Convert.ToDateTime(gvVisaDetails.Rows[i].Cells[2].Text);
                                objVisaDetails.Mode = int.Parse(Mode.Text);
                                objSaveVisaDetailsCollection.Add(objVisaDetails);
                            }
                        }
                        BusinessEntities.RaveHRCollection objDeleteVisaDetailsCollection = (BusinessEntities.RaveHRCollection)ViewState[VISADETAILSDELETE];

                        if (objDeleteVisaDetailsCollection != null)
                        {
                            BusinessEntities.VisaDetails obj = null;

                            for (int i = 0; i < objDeleteVisaDetailsCollection.Count; i++)
                            {
                                objVisaDetails = new BusinessEntities.VisaDetails();
                                obj = (BusinessEntities.VisaDetails)objDeleteVisaDetailsCollection.Item(i);

                                objVisaDetails.VisaId = obj.VisaId;
                                objVisaDetails.EMPId = obj.EMPId;
                                objVisaDetails.CountryName = obj.CountryName;
                                objVisaDetails.VisaType = obj.VisaType;
                                objVisaDetails.ExpiryDate = obj.ExpiryDate;
                                objVisaDetails.Mode = obj.Mode;

                                objSaveVisaDetailsCollection.Add(objVisaDetails);
                            }

                        }
                    }
                    else
                    {
                        //delete visa details if exists.
                        objVisaDetailsBAL = new Rave.HR.BusinessLayer.Employee.VisaDetails();
                        objVisaDetailsBAL.DeleteVisaDetailsByEmpId(employeeID);
                    }
                }
                else
                {
                    employee.PassportNo = string.Empty;
                    employee.PassportIssuePlace = string.Empty;
                    employee.PassportIssueDate = DateTime.MinValue;
                    employee.PassportExpireDate = DateTime.MinValue;

                    //delete visa details if exists.
                    objVisaDetailsBAL = new Rave.HR.BusinessLayer.Employee.VisaDetails();
                    objVisaDetailsBAL.DeleteVisaDetailsByEmpId(employeeID);
                }
                Boolean Flag = false;
                objEmployeeBAL.UpdateEmployee(employee,Flag);

                objVisaDetailsBAL.Manipulation(objSaveVisaDetailsCollection);

                if (ViewState.Count > 0)
                {
                    ViewState.Clear();
                }

                //Refresh the grip after saving
                if (EMPId.Value != string.Empty)
                {
                    int empID = Convert.ToInt32(EMPId.Value);
                    //Refresh the grip after saving
                    this.PopulateGrid(empID);
                }

                lblMessage.Text = "Passport details saved successfully.";

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
    }

    /// <summary>
    /// Handles the Click event of the btnNext control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Response.Redirect(CommonConstants.PAGE_QUALIFICATIONDETAILS);
    }

    /// <summary>
    /// Handles the Click event of the btnCancel control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtPassportNo.Text = string.Empty;
        txtIssuePlace.Text = string.Empty;
        txtIssueDate.Text = string.Empty;
        txtExpiryDate.Text = string.Empty;

        this.ClearControls();
    }

    /// <summary>
    /// Handles the RowEditing event of the gvVisaDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewEditEventArgs"/> instance containing the event data.</param>
    protected void gvVisaDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int rowIndex = 0;

        txtCountryName.Text = gvVisaDetails.Rows[e.NewEditIndex].Cells[0].Text;
        txtVisaType.Text = gvVisaDetails.Rows[e.NewEditIndex].Cells[1].Text.Trim();
        txtVisaExpiryDate.Text = gvVisaDetails.Rows[e.NewEditIndex].Cells[2].Text;

        ImageButton btnImg = (ImageButton)gvVisaDetails.Rows[e.NewEditIndex].FindControl(IMGBTNDELETE);
        rowIndex = e.NewEditIndex;
        ViewState[ROWINDEX] = null;
        ViewState[ROWINDEX] = rowIndex;
        btnImg.Enabled = false;

        btnAddRow.Visible = false;
        btnUpdateRow.Visible = true;
        btnCancelRow.Visible = true;

        //Disabling all the edit buttons.
        for (int i = 0; i < gvVisaDetails.Rows.Count; i++)
        {
            ImageButton btnImgEdit = (ImageButton)gvVisaDetails.Rows[i].FindControl(IMGBTNEDIT);
            if (i != rowIndex)
                btnImgEdit.Enabled = false;
        }
    }

    /// <summary>
    /// Handles the RowDeleting event of the gvVisaDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewDeleteEventArgs"/> instance containing the event data.</param>
    protected void gvVisaDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int deleteRowIndex = 0;
        int rowIndex = -1;

        BusinessEntities.VisaDetails objVisaDetails = new BusinessEntities.VisaDetails();

        deleteRowIndex = e.RowIndex;

        objVisaDetails = (BusinessEntities.VisaDetails)VisaDetailsCollection.Item(deleteRowIndex);
        objVisaDetails.Mode = 3;

        if (ViewState[VISADETAILSDELETE] != null)
        {
            BusinessEntities.RaveHRCollection objDeleteVisaDetailsCollection = (BusinessEntities.RaveHRCollection)ViewState[VISADETAILSDELETE];
            objDeleteVisaDetailsCollection.Add(objVisaDetails);

            ViewState[VISADETAILSDELETE] = objDeleteVisaDetailsCollection;
        }
        else
        {
            BusinessEntities.RaveHRCollection objDeleteVisaDetailsCollection1 = new BusinessEntities.RaveHRCollection();

            objDeleteVisaDetailsCollection1.Add(objVisaDetails);

            ViewState[VISADETAILSDELETE] = objDeleteVisaDetailsCollection1;
        }

        VisaDetailsCollection.RemoveAt(deleteRowIndex);

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

            ImageButton btnImg = (ImageButton)gvVisaDetails.Rows[rowIndex].FindControl(IMGBTNDELETE);
            btnImg.Enabled = false;

            //Disabling all the edit buttons.
            for (int i = 0; i < gvVisaDetails.Rows.Count; i++)
            {
                if (rowIndex != i)
                {
                    ImageButton btnImgEdit = (ImageButton)gvVisaDetails.Rows[i].FindControl(IMGBTNEDIT);
                    btnImgEdit.Enabled = false;
                }
            }
        }
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the rbtnValidPassport control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void rbtnValidPassport_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnValidPassport.SelectedItem.Text == CommonConstants.YES)
        {
            divPassportDetails.Visible = true;
        }
        else
        {
            divPassportDetails.Visible = false;
            divVisaDetails.Visible = false;
            //btnSave.Visible = false;
            //btnCancel.Visible = false;
        }

    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the rbtnValidVisa control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void rbtnValidVisa_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnValidVisa.SelectedItem.Text == CommonConstants.YES)
        {
            divVisaDetails.Visible = true;
            if (gvVisaDetails.Rows[0].Cells[0].Text == NO_RECORDS_FOUND_MESSAGE)
            {
                VisaDetailsCollection.Clear();
                ShowHeaderWhenEmptyGrid();
            }
        }
        else
            divVisaDetails.Visible = false;
    }

    #endregion

    #region Private Member Functions

    /// <summary>
    /// Does the data bind.
    /// </summary>
    private void DoDataBind()
    {
        gvVisaDetails.DataSource = VisaDetailsCollection;
        gvVisaDetails.DataBind();

        //Displaying grid header with NO record found message.
        if (gvVisaDetails.Rows.Count == 0)
            ShowHeaderWhenEmptyGrid();

    }

    /// <summary>
    /// Clears the controls.
    /// </summary>
    private void ClearControls()
    {
        txtCountryName.Text = string.Empty;
        txtVisaType.Text = string.Empty;
        txtVisaExpiryDate.Text = string.Empty;

    }

    /// <summary>
    /// Populates the grid.
    /// </summary>
    private void PopulateGrid(int employeeID)
    {
        VisaDetailsCollection = this.GetVisaDetails(employeeID);

        //Binding the datatable to grid
        gvVisaDetails.DataSource = VisaDetailsCollection;
        gvVisaDetails.DataBind();

        //Displaying grid header with NO record found message.
        if (gvVisaDetails.Rows.Count == 0)
            ShowHeaderWhenEmptyGrid();

        if (gvVisaDetails.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
        {
            rbtnValidVisa.SelectedIndex = 0;
            divVisaDetails.Visible = true;
        }
        else
        {
            divVisaDetails.Visible = false;
        }
        EMPId.Value = employeeID.ToString().Trim();

        if (Session[SessionNames.PAGEMODE] != null)
        {
            PageMode = Session[SessionNames.PAGEMODE].ToString();

            //if (PageMode == Common.MasterEnum.PageModeEnum.View.ToString() && IsPostBack == false && gvQualification.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
            if (PageMode == Common.MasterEnum.PageModeEnum.View.ToString() && gvVisaDetails.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
            {
                    passportdetails.Enabled = false;
                    visaDetails.Enabled = true;
                    rbtnValidPassport.Enabled = true;
                    lblValidPassport.Enabled = true;
                    if (rbtnValidPassport.SelectedIndex == CommonConstants.ZERO)
                    {
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                    }
                
            }
        }

    }

    /// <summary>
    /// Populates the control.
    /// </summary>
    private void PopulateControl()
    {
        if (employee != null)
        {
            if (employee.PassportNo != null && employee.PassportNo != string.Empty)
            {
                txtPassportNo.Text = employee.PassportNo;
                txtIssuePlace.Text = employee.PassportIssuePlace.Trim();
                txtIssueDate.Text = employee.PassportIssueDate.ToString(DateFormat);
                txtExpiryDate.Text = employee.PassportExpireDate.ToString(DateFormat);

            }
        }
    }

    /// <summary>
    /// Gets the professional details.
    /// </summary>
    /// <returns></returns>
    private BusinessEntities.RaveHRCollection GetVisaDetails(int employeeID)
    {
        Rave.HR.BusinessLayer.Employee.VisaDetails objVisaDetailsBAL;
        BusinessEntities.VisaDetails objVisaDetails;

        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            objVisaDetailsBAL = new Rave.HR.BusinessLayer.Employee.VisaDetails();
            objVisaDetails = new BusinessEntities.VisaDetails();

            //objVisaDetails.EMPId = 14;
            objVisaDetails.EMPId = employeeID;

            raveHRCollection = objVisaDetailsBAL.GetVisaDetails(objVisaDetails);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetVisaDetails", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

        return raveHRCollection;
    }

    /// <summary>
    /// Validates the controls.
    /// </summary>
    /// <returns></returns>
    private bool ValidateControls()
    {
        StringBuilder errMessage = new StringBuilder();
        bool flag = true;
        string[] issueDateArr;
        string[] expiryDateArr;

        if (!string.IsNullOrEmpty(txtIssueDate.Text) && !string.IsNullOrEmpty(txtExpiryDate.Text))
        {
            issueDateArr = Convert.ToString(txtIssueDate.Text).Split(SPILITER_SLASH);
            DateTime issueDate = new DateTime(Convert.ToInt32(issueDateArr[2]), Convert.ToInt32(issueDateArr[1]), Convert.ToInt32(issueDateArr[0]));

            expiryDateArr = Convert.ToString(txtExpiryDate.Text).Split(SPILITER_SLASH);
            DateTime expiryDate = new DateTime(Convert.ToInt32(expiryDateArr[2]), Convert.ToInt32(expiryDateArr[1]), Convert.ToInt32(expiryDateArr[0]));

            if (issueDate > DateTime.Now.Date)
            {
                errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.DATEOFISSUE_VALIDATION);
                flag = false;
            }

            if (issueDate > expiryDate)
            {
                errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.EXPIRYDATE_LESSTHAN_DATEOFISSUE);
                flag = false;
            }

            if (expiryDate < DateTime.Now.Date)
            {
                errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.EXPIRYDATE_LESSTHAN_CURRENTDATE);
                flag = false;
            }
        }
        lblError.Text = errMessage.ToString();
        return flag;

    }

    /// <summary>
    /// Validates the controls.
    /// </summary>
    /// <returns></returns>
    private bool ValidateVisaControls()
    {
        StringBuilder errMessage = new StringBuilder();
        bool flag = true;

        string[] visaExpiryDateArr;

        visaExpiryDateArr = Convert.ToString(txtVisaExpiryDate.Text).Split(SPILITER_SLASH);
        DateTime visaExpiryDate = new DateTime(Convert.ToInt32(visaExpiryDateArr[2]), Convert.ToInt32(visaExpiryDateArr[1]), Convert.ToInt32(visaExpiryDateArr[0]));

        if (visaExpiryDate < DateTime.Now.Date)
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.VISAEXPIRYDATE_LESSTHAN_CURRENTDATE);
            flag = false;
        }

        lblError.Text = errMessage.ToString();
        return flag;

    }

    /// <summary>
    /// Display Header When GridView Empty with proper message
    /// </summary>
    /// <param name="objListSort">EmptyList</param>
    private void ShowHeaderWhenEmptyGrid()
    {
        try
        {
            //set header visible
            gvVisaDetails.ShowHeader = true;

            //Create empty datasource for Grid view and bind
            VisaDetailsCollection.Add(new BusinessEntities.VisaDetails());
            gvVisaDetails.DataSource = VisaDetailsCollection;
            gvVisaDetails.DataBind();

            //Calculate number of columns in Grid view used for column Span
            int columnsCount = gvVisaDetails.Columns.Count;

            //clear all the cells in the row
            gvVisaDetails.Rows[0].Cells.Clear();

            //add a new blank cell
            gvVisaDetails.Rows[0].Cells.Add(new TableCell());
            gvVisaDetails.Rows[0].Cells[0].Text = NO_RECORDS_FOUND_MESSAGE;
            gvVisaDetails.Rows[0].Cells[0].Wrap = false;
            gvVisaDetails.Rows[0].Cells[0].Width = Unit.Percentage(10);

            if (Session[SessionNames.PAGEMODE] != null)
            {
                PageMode = Session[SessionNames.PAGEMODE].ToString();
                if (PageMode == Common.MasterEnum.PageModeEnum.View.ToString())
                {
                    passportdetails.Enabled = true;
                    visaDetails.Enabled = true;
                    rbtnValidPassport.Enabled = true;
                    lblValidPassport.Enabled = true;
                    if (rbtnValidPassport.SelectedIndex == CommonConstants.ZERO)
                    {
                        btnSave.Visible = true;
                        btnCancel.Visible = true;
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ShowHeaderWhenEmptyGrid", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    #endregion
}