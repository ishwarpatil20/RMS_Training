using System;
using System.Text;
using System.Web.UI;
using Common;
using System.DirectoryServices;
using Common.AuthorizationManager;
using System.Collections;
using System.Web.UI.WebControls;

public partial class EmployeeQualificationDetails : BaseClass
{
    #region Private Field Members

    #region ViewState Constants

    private string QUALIFICATIONDETAILS = "QualificationDetails";
    private string QUALIFICATIONDETAILSDELETE = "QualificationDetailsDelete";
    private string DELETEROWINDEX = "DeleteRowIndex";
    private string ROWINDEX = "RowIndex";

    #endregion ViewState Constants

    private string IMGBTNDELETE = "imgBtnDelete";
    private string IMGBTNEDIT = "imgBtnEdit";
    private string CLASS_NAME = "EmpQualificationDetails.aspx";
    private string QUALIFICATION = "Qualification";
    private string QUALIFICATIONID = "QualificationId";
    private string MODE = "Mode";
    string UserRaveDomainId;
    string UserMailId;
    private string EditMode = string.Empty;
    static int value = 0;
    string PageMode = string.Empty;
    const string PAGETYPE = "pagetype";
    const string PAGETYPEEMPLOYEESUMMERY = "EMPLOYEESUMMERY";

    Rave.HR.BusinessLayer.Employee.QualificationDetails ObjQualificationDetailsBL = new Rave.HR.BusinessLayer.Employee.QualificationDetails();
    AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
    // Get logged-in user's email id
    AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
    ArrayList arrRolesForUser = new ArrayList();

    /// <summary>
    /// Defines a constant error for no records found after applying filter criteria
    /// </summary>
    private const string NO_RECORDS_FOUND_MESSAGE = "No records found.";

    private int employeeID = 0;
    private BusinessEntities.Employee employee;

    protected EmployeeMenuUC BubbleControl;

    #endregion Private Field Members

    #region Local Properties

    /// <summary>
    /// Gets or sets the qualification details collection.
    /// </summary>
    /// <value>The qualification details collection.</value>
    private BusinessEntities.RaveHRCollection QualificationDetailsCollection
    {
        get
        {
            if (ViewState[QUALIFICATIONDETAILS] != null)
                return ((BusinessEntities.RaveHRCollection)ViewState[QUALIFICATIONDETAILS]);
            else
                return null;
        }
        set
        {
            ViewState[QUALIFICATIONDETAILS] = value;
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
        //Clearing the error label
        lblError.Text = string.Empty;
        lblMessage.Text = string.Empty;


        btnAddRow.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return ButtonClickValidate();");
        btnUpdateRow.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return ButtonClickValidate();");
        btnSave.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return SaveButtonClickValidate();");

        ddlQualification.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + spanzQualification.ClientID + "','','');");
        imgQualification.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanQualification.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgQualification.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanQualification.ClientID + "');");

        txtUniversityName.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return ValidateSpecialCharacters()");
        txtUniversityName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtUniversityName.ClientID + "','" + imgUniversityName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPECIALCHAR + "');");
        imgUniversityName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanUniversityName.ClientID + "','" + Common.CommonConstants.MSG_ALPHABET_SPECIALCHAR + "');");
        imgUniversityName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanUniversityName.ClientID + "');");

        txtInstituteName.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return ValidateSpecialCharacters()");
        txtInstituteName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtInstituteName.ClientID + "','" + imgInstituteName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPECIALCHAR + "');");
        imgInstituteName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanInstituteName.ClientID + "','" + Common.CommonConstants.MSG_ALPHABET_SPECIALCHAR + "');");
        imgInstituteName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanInstituteName.ClientID + "');");

        txtStream.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return ValidateSpecialCharacters()");

        txtYearOfPassing.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtYearOfPassing.ClientID + "','" + imgYearOfPassing.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
        txtYearOfPassing.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateYearOfPassing()");
        imgYearOfPassing.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanYearOfPassing.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgYearOfPassing.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanYearOfPassing.ClientID + "');");

        txtGPA.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtGPA.ClientID + "','" + imgGPA.ClientID + "','" + Common.CommonConstants.VALIDATE_DECIMAL_FUNCTION + "');");
        imgGPA.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanGPA.ClientID + "','" + Common.CommonConstants.MSG_DECIMAL + "');");
        imgGPA.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanGPA.ClientID + "');");

        txtOutOf.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtOutOf.ClientID + "','" + imgOutOf.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
        imgOutOf.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanOutOf.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgOutOf.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanOutOf.ClientID + "');");

        txtPercentage.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtPercentage.ClientID + "','" + imgPercentage.ClientID + "','" + Common.CommonConstants.VALIDATE_DECIMAL_FUNCTION + "');");
        imgPercentage.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanPercentage.ClientID + "','" + Common.CommonConstants.MSG_DECIMAL + "');");
        imgPercentage.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanPercentage.ClientID + "');");

        if (!ValidateURL())
        {
            Response.Redirect(CommonConstants.INVALIDURL, false);
        }

        if (Session[Common.SessionNames.EMPLOYEEDETAILS] != null)
        {
            employee = (BusinessEntities.Employee)Session[Common.SessionNames.EMPLOYEEDETAILS];
        }

        if (employee != null)
        {
            employeeID = employee.EMPId;
            lblempName.Text = employee.FirstName.ToUpper() + " " + employee.LastName.ToUpper();
        }

        if (!IsPostBack)
        {
            Session[SessionNames.PAGEMODE] = Common.MasterEnum.PageModeEnum.View;
            ViewState["isSaved"] = null;
            this.PopulateGrid(employeeID);
            this.GetQualifications();
        }

        if (gvQualification.Rows[0].Cells[0].Text == NO_RECORDS_FOUND_MESSAGE)
        {
            QualificationDetailsCollection.Clear();
            ShowHeaderWhenEmptyGrid();
        }

        // Get logged-in user's email id
        AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
        UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
        UserMailId = UserRaveDomainId.Replace("co.in", "com");

        arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());

        SavedControlVirtualPath = "~/EmployeeMenuUC.ascx";
        ReloadControl();

        if (UserMailId.ToLower() == employee.EmailId.ToLower() || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
        {
            if (Session[SessionNames.PAGEMODE] != null)
            {
                PageMode = Session[SessionNames.PAGEMODE].ToString();

                if (PageMode == Common.MasterEnum.PageModeEnum.View.ToString() && IsPostBack == false && gvQualification.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
                //if (PageMode == Common.MasterEnum.PageModeEnum.View.ToString() && (gvQualification.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE ||gvQualification.Rows.Count > 0) )
                {
                    Qualificationdetails.Enabled = false;
                    btnEdit.Visible = true;
                    btnEditCancel.Visible = true;
                    btnCancel.Visible = false;
                    btnAddRow.Visible = false;
                }
            }
        }
        else
        {
            Qualificationdetails.Enabled = false;

        }
        txtOutOf.Enabled = false;
       
      
        

    }

    /// <summary>
    /// Handles the Click event of the btnAddRow control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Rave.HR.BusinessLayer.Employee.QualificationDetails objQualificationDetailsBAL;

        if (ValidateControls())
        {
            BusinessEntities.QualificationDetails objQualificationDetails = new BusinessEntities.QualificationDetails();
            objQualificationDetailsBAL = new Rave.HR.BusinessLayer.Employee.QualificationDetails();

            if (gvQualification.Rows[0].Cells[0].Text == NO_RECORDS_FOUND_MESSAGE)
            {
                QualificationDetailsCollection.Clear();
            }

            objQualificationDetails.QualificationName = ddlQualification.SelectedItem.Text;
            if (!string.IsNullOrEmpty(txtStream.Text.Trim()))
                objQualificationDetails.Stream = txtStream.Text.Trim();
            else
                objQualificationDetails.Stream = "";
            objQualificationDetails.UniversityName = txtUniversityName.Text.Trim();
            objQualificationDetails.InstituteName = txtInstituteName.Text.Trim();
            objQualificationDetails.PassingYear = txtYearOfPassing.Text.Trim();
            if (!string.IsNullOrEmpty(txtGPA.Text))
                objQualificationDetails.GPA = float.Parse(txtGPA.Text);
            else
                objQualificationDetails.GPA = CommonConstants.ZERO;

            if (!string.IsNullOrEmpty(txtOutOf.Text))
                objQualificationDetails.Outof = float.Parse(txtOutOf.Text);
            else
                objQualificationDetails.Outof = CommonConstants.ZERO;
            if (!string.IsNullOrEmpty(txtPercentage.Text))
                objQualificationDetails.Percentage = float.Parse(txtPercentage.Text);
            else
                objQualificationDetails.Percentage = CommonConstants.ZERO;

            objQualificationDetails.Qualification = int.Parse(ddlQualification.SelectedItem.Value);
            objQualificationDetails.Mode = 1;
            objQualificationDetails.QualificationId = 0;
            objQualificationDetails.EMPId = int.Parse(EMPId.Value);

            QualificationDetailsCollection.Add(objQualificationDetails);

            objQualificationDetailsBAL.AddQualificationDetails(objQualificationDetails);

            this.PopulateGrid(objQualificationDetails.EMPId);

            this.ClearControls();

            lblMessage.Text = "Qualification details saved successfully.";
                
        }
    }

    /// <summary>
    /// Handles the RowEditing event of the gvQualification control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewEditEventArgs"/> instance containing the event data.</param>
    protected void gvQualification_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int rowIndex = 0;
        value = 1;

        Label QualificationValue = (Label)gvQualification.Rows[e.NewEditIndex].FindControl("Qualification");

        ddlQualification.SelectedValue = QualificationValue.Text;

        txtStream.Text = gvQualification.Rows[e.NewEditIndex].Cells[1].Text.Replace("&nbsp;", string.Empty).Trim();
        txtUniversityName.Text = gvQualification.Rows[e.NewEditIndex].Cells[2].Text.Replace("&nbsp;", string.Empty).Trim();
        txtInstituteName.Text = gvQualification.Rows[e.NewEditIndex].Cells[3].Text.Replace("&nbsp;", string.Empty).Trim();
        txtYearOfPassing.Text = gvQualification.Rows[e.NewEditIndex].Cells[4].Text;
        txtPercentage.Text = gvQualification.Rows[e.NewEditIndex].Cells[5].Text;
        txtGPA.Text = gvQualification.Rows[e.NewEditIndex].Cells[6].Text;
        txtOutOf.Text = gvQualification.Rows[e.NewEditIndex].Cells[7].Text;
        ddlQualification_OnSelectedIndexChanged(null, null);
        if (string.IsNullOrEmpty(txtPercentage.Text.Trim()))
        {
            txtGPA.Enabled = true;
        }
        else
        {
            txtGPA.Enabled = false;
        }
        if (!string.IsNullOrEmpty(txtOutOf.Text.Trim()))
        {
            txtOutOf.Enabled = true;
        }
        else
        {
            txtOutOf.Enabled = false;
        }
        
        ImageButton btnImg = (ImageButton)gvQualification.Rows[e.NewEditIndex].FindControl(IMGBTNDELETE);
        rowIndex = e.NewEditIndex;
        ViewState[ROWINDEX] = null;
        ViewState[ROWINDEX] = rowIndex;
        btnImg.Enabled = false;

        btnAddRow.Visible = false;
        btnUpdateRow.Visible = true;
        btnCancelRow.Visible = true;
        btnCancel.Visible = false;

        //Disabling all the edit buttons.
        for (int i = 0; i < gvQualification.Rows.Count; i++)
        {
            ImageButton btnImgEdit = (ImageButton)gvQualification.Rows[i].FindControl(IMGBTNEDIT);
            if (i != rowIndex)
                btnImgEdit.Enabled = false;
        }

        HfIsDataModified.Value = CommonConstants.YES;
    }

    /// <summary>
    /// Handles the RowDeleting event of the gvQualification control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewDeleteEventArgs"/> instance containing the event data.</param>
    protected void gvQualification_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int deleteRowIndex = 0;
        int rowIndex = -1;
        Rave.HR.BusinessLayer.Employee.QualificationDetails objQualificationDetailsBAL;
        //Boolean Flag = false;

        BusinessEntities.QualificationDetails objQualificationDetails = new BusinessEntities.QualificationDetails();   
        objQualificationDetailsBAL = new Rave.HR.BusinessLayer.Employee.QualificationDetails();
        deleteRowIndex = e.RowIndex;

        objQualificationDetails = (BusinessEntities.QualificationDetails)QualificationDetailsCollection.Item(deleteRowIndex);
        //if (objQualificationDetails.Mode == 1)
        //    Flag = true;
        objQualificationDetails.Mode = 3;

        if (ViewState[QUALIFICATIONDETAILSDELETE] != null)
        {
            BusinessEntities.RaveHRCollection objDeleteQualificationDetailsCollection = (BusinessEntities.RaveHRCollection)ViewState[QUALIFICATIONDETAILSDELETE];
            objDeleteQualificationDetailsCollection.Add(objQualificationDetails);

            ViewState[QUALIFICATIONDETAILSDELETE] = objDeleteQualificationDetailsCollection;
        }
        else
        {
            BusinessEntities.RaveHRCollection objDeleteQualificationDetailsCollection1 = new BusinessEntities.RaveHRCollection();

            objDeleteQualificationDetailsCollection1.Add(objQualificationDetails);

            ViewState[QUALIFICATIONDETAILSDELETE] = objDeleteQualificationDetailsCollection1;
        }

        QualificationDetailsCollection.RemoveAt(deleteRowIndex);

        ViewState[DELETEROWINDEX] = deleteRowIndex;

        DoDataBind();

        objQualificationDetailsBAL.DeleteQualificationDetails(objQualificationDetails);

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

            ImageButton btnImg = (ImageButton)gvQualification.Rows[rowIndex].FindControl(IMGBTNDELETE);
            btnImg.Enabled = false;

            //Disabling all the edit buttons.
            for (int i = 0; i < gvQualification.Rows.Count; i++)
            {
                if (rowIndex != i)
                {
                    ImageButton btnImgEdit = (ImageButton)gvQualification.Rows[i].FindControl(IMGBTNEDIT);
                    btnImgEdit.Enabled = false;
                }
            }
        }
        lblMessage.Text = "Qualification details deleted successfully.";
    }

    /// <summary>
    /// Handles the Click event of the btnSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Rave.HR.BusinessLayer.Employee.QualificationDetails objQualificationDetailsBAL;

        BusinessEntities.QualificationDetails objQualificationDetails;
        BusinessEntities.RaveHRCollection objSaveQualificationDetailsCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            objQualificationDetailsBAL = new Rave.HR.BusinessLayer.Employee.QualificationDetails();

            if (gvQualification.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
            {
                for (int i = 0; i < gvQualification.Rows.Count; i++)
                {
                    objQualificationDetails = new BusinessEntities.QualificationDetails();

                    Label QualificationValue = (Label)gvQualification.Rows[i].FindControl(QUALIFICATION);
                    Label QualificationId = (Label)gvQualification.Rows[i].FindControl(QUALIFICATIONID);
                    Label Mode = (Label)gvQualification.Rows[i].FindControl(MODE);

                    objQualificationDetails.QualificationId = int.Parse(QualificationId.Text);
                    objQualificationDetails.EMPId = int.Parse(EMPId.Value);
                    objQualificationDetails.QualificationName = gvQualification.Rows[i].Cells[0].Text;
                    objQualificationDetails.Stream = gvQualification.Rows[i].Cells[1].Text.Replace("&nbsp;", string.Empty);
                    objQualificationDetails.UniversityName = gvQualification.Rows[i].Cells[2].Text.Replace("&nbsp;", string.Empty);
                    objQualificationDetails.InstituteName = gvQualification.Rows[i].Cells[3].Text;
                    objQualificationDetails.PassingYear = gvQualification.Rows[i].Cells[4].Text;

                    if (!string.IsNullOrEmpty(gvQualification.Rows[i].Cells[5].Text))
                        objQualificationDetails.Percentage = float.Parse(gvQualification.Rows[i].Cells[5].Text);
                    else
                        objQualificationDetails.Percentage = float.Parse(CommonConstants.ZERO.ToString());

                    if (!string.IsNullOrEmpty(gvQualification.Rows[i].Cells[6].Text))
                        objQualificationDetails.GPA = float.Parse(gvQualification.Rows[i].Cells[6].Text);
                    else
                        objQualificationDetails.GPA = CommonConstants.ZERO;

                    if (!string.IsNullOrEmpty(gvQualification.Rows[i].Cells[7].Text))
                        objQualificationDetails.Outof = float.Parse(gvQualification.Rows[i].Cells[7].Text);
                    else
                        objQualificationDetails.Outof = CommonConstants.ZERO;
                    
                    objQualificationDetails.Mode = int.Parse(Mode.Text);
                    objQualificationDetails.Qualification = int.Parse(QualificationValue.Text);

                    objSaveQualificationDetailsCollection.Add(objQualificationDetails);
                }
            }
            BusinessEntities.RaveHRCollection objDeleteQualificationDetailsCollection = (BusinessEntities.RaveHRCollection)ViewState[QUALIFICATIONDETAILSDELETE];

            if (objDeleteQualificationDetailsCollection != null)
            {
                BusinessEntities.QualificationDetails obj = null;

                for (int i = 0; i < objDeleteQualificationDetailsCollection.Count; i++)
                {
                    objQualificationDetails = new BusinessEntities.QualificationDetails();
                    obj = (BusinessEntities.QualificationDetails)objDeleteQualificationDetailsCollection.Item(i);

                    objQualificationDetails.QualificationId = obj.QualificationId;
                    objQualificationDetails.EMPId = obj.EMPId;
                    objQualificationDetails.QualificationName = obj.QualificationName;
                    objQualificationDetails.UniversityName = obj.UniversityName;
                    objQualificationDetails.InstituteName = obj.InstituteName;
                    objQualificationDetails.PassingYear = obj.PassingYear;
                    objQualificationDetails.GPA = obj.GPA;
                    objQualificationDetails.Outof = obj.Outof;
                    objQualificationDetails.Percentage = obj.Percentage;
                    objQualificationDetails.Mode = obj.Mode;
                    objQualificationDetails.Qualification = obj.Qualification;

                    objSaveQualificationDetailsCollection.Add(objQualificationDetails);
                }

            }
            objQualificationDetailsBAL.Manipulation(objSaveQualificationDetailsCollection);

            if (ViewState.Count > 0)
            {
                ViewState.Clear();
            }

            if (EMPId.Value != string.Empty)
            {
                int empID = Convert.ToInt32(EMPId.Value);
                //Refresh the grip after saving
                this.PopulateGrid(empID);
            }

            lblMessage.Text = "Qualification details saved successfully.";

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
    /// Handles the Click event of the btnCancelRow control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnCancelRow_Click(object sender, EventArgs e)
    {
        int rowIndex = 0;
        int deleteRowIndex = -1;
        value = 0;

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
            ImageButton btnImg = (ImageButton)gvQualification.Rows[rowIndex].FindControl(IMGBTNDELETE);
            btnImg.Enabled = true;
            ViewState[ROWINDEX] = null;
            ViewState[DELETEROWINDEX] = null;
        }

        //Enabling all the edit buttons.
        for (int i = 0; i < gvQualification.Rows.Count; i++)
        {
            ImageButton btnImgEdit = (ImageButton)gvQualification.Rows[i].FindControl(IMGBTNEDIT);
            btnImgEdit.Enabled = true;
        }
        //To solved issue id 19221
        //Coded by Rahul P
        //Start
        btnAddRow.Visible = true;
        btnCancel.Visible = true;
        btnUpdateRow.Visible = false;
        btnCancelRow.Visible = false;
        btnCancel.Visible = true;
        //End

    }

    /// <summary>
    /// Handles the Click event of the btnUpdateRow control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnUpdateRow_Click(object sender, EventArgs e)
    {
        Rave.HR.BusinessLayer.Employee.QualificationDetails objQualificationDetailsBAL;

        if (ValidateControls())
        {
            objQualificationDetailsBAL = new Rave.HR.BusinessLayer.Employee.QualificationDetails();
            BusinessEntities.QualificationDetails objQualificationDetails = new BusinessEntities.QualificationDetails();
            int rowIndex = 0;
            int deleteRowIndex = -1;
            value = 0;

            if (ViewState[DELETEROWINDEX] != null)
            {
                deleteRowIndex = Convert.ToInt32(ViewState[DELETEROWINDEX].ToString());
            }

            //Update the grid view according the row, which is selected for editing.
            if (ViewState[ROWINDEX] != null)
            {
                rowIndex = Convert.ToInt32(ViewState[ROWINDEX].ToString());
                if (deleteRowIndex != -1 && deleteRowIndex < rowIndex) rowIndex--;

                Label QualificationValue = (Label)gvQualification.Rows[rowIndex].FindControl(QUALIFICATION);
                Label QualificationId = (Label)gvQualification.Rows[rowIndex].FindControl(QUALIFICATIONID);
                Label Mode = (Label)gvQualification.Rows[rowIndex].FindControl(MODE);

                QualificationValue.Text = ddlQualification.SelectedValue;

                gvQualification.Rows[rowIndex].Cells[0].Text = ddlQualification.SelectedItem.Text;
                gvQualification.Rows[rowIndex].Cells[1].Text = txtStream.Text;
                gvQualification.Rows[rowIndex].Cells[2].Text = txtUniversityName.Text;
                gvQualification.Rows[rowIndex].Cells[3].Text = txtInstituteName.Text;
                gvQualification.Rows[rowIndex].Cells[4].Text = txtYearOfPassing.Text;
                if (!string.IsNullOrEmpty(txtPercentage.Text))
                    gvQualification.Rows[rowIndex].Cells[5].Text = txtPercentage.Text;
                else
                    gvQualification.Rows[rowIndex].Cells[5].Text = CommonConstants.ZERO.ToString();
                if (!string.IsNullOrEmpty(txtGPA.Text))
                    gvQualification.Rows[rowIndex].Cells[6].Text = txtGPA.Text;
                else
                    gvQualification.Rows[rowIndex].Cells[6].Text = CommonConstants.ZERO.ToString();

                if (!string.IsNullOrEmpty(txtOutOf.Text))
                    gvQualification.Rows[rowIndex].Cells[7].Text = txtOutOf.Text;
                else
                    gvQualification.Rows[rowIndex].Cells[7].Text = CommonConstants.ZERO.ToString();
                
                if (int.Parse(QualificationId.Text) == 0)
                {
                    Mode.Text = "1";
                }
                else
                {
                    Mode.Text = "2";
                }
                ImageButton btnImg = (ImageButton)gvQualification.Rows[rowIndex].FindControl(IMGBTNDELETE);
                btnImg.Enabled = true;
                ViewState[ROWINDEX] = null;
                ViewState[DELETEROWINDEX] = null;

            }

            for (int i = 0; i < QualificationDetailsCollection.Count; i++)
            {
                
                objQualificationDetails = (BusinessEntities.QualificationDetails)QualificationDetailsCollection.Item(i);

                Label QualificationId = (Label)gvQualification.Rows[i].FindControl(QUALIFICATIONID);
                Label Mode = (Label)gvQualification.Rows[i].FindControl(MODE);

                objQualificationDetails.QualificationId = int.Parse(QualificationId.Text);
                objQualificationDetails.EMPId = int.Parse(EMPId.Value);
                objQualificationDetails.QualificationName = gvQualification.Rows[i].Cells[0].Text;
                objQualificationDetails.Stream = gvQualification.Rows[i].Cells[1].Text.Replace("&nbsp;", string.Empty).Trim();
                objQualificationDetails.UniversityName = gvQualification.Rows[i].Cells[2].Text.Replace("&nbsp;", string.Empty).Trim();
                objQualificationDetails.InstituteName = gvQualification.Rows[i].Cells[3].Text;
                objQualificationDetails.PassingYear = gvQualification.Rows[i].Cells[4].Text;

                if (!string.IsNullOrEmpty(gvQualification.Rows[i].Cells[5].Text))
                    objQualificationDetails.Percentage = float.Parse(gvQualification.Rows[i].Cells[5].Text);
                else
                    objQualificationDetails.Percentage = CommonConstants.ZERO;

                if (!string.IsNullOrEmpty(gvQualification.Rows[i].Cells[6].Text))
                {
                    objQualificationDetails.GPA = float.Parse(gvQualification.Rows[i].Cells[6].Text);
                }
                else
                {
                    objQualificationDetails.GPA = float.Parse(CommonConstants.ZERO.ToString());
                }

                if (!string.IsNullOrEmpty(gvQualification.Rows[i].Cells[7].Text))
                    objQualificationDetails.Outof = float.Parse(gvQualification.Rows[i].Cells[7].Text);
                else
                    objQualificationDetails.Outof = float.Parse(CommonConstants.ZERO.ToString());

                
                objQualificationDetails.Mode = int.Parse(Mode.Text);
            }

            this.DoDataBind();

            objQualificationDetailsBAL.UpdateQualificationDetails(objQualificationDetails);

            //Clear all the fields after inserting row into gridview
            this.ClearControls();

            btnAddRow.Visible = true;
            btnCancel.Visible = true;
            btnUpdateRow.Visible = false;
            btnCancelRow.Visible = false;

            //Enabling all the edit buttons.
            for (int i = 0; i < gvQualification.Rows.Count; i++)
            {
                ImageButton btnImgEdit = (ImageButton)gvQualification.Rows[i].FindControl(IMGBTNEDIT);
                btnImgEdit.Enabled = true;
            }

            HfIsDataModified.Value = string.Empty;
            lblMessage.Text = "Qualification details updated successfully.";
        }

        
    }

    /// <summary>
    /// Handles the Click event of the btnNext control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Response.Redirect(CommonConstants.PAGE_PROFESSIONALETAILS);
    }

    /// <summary>
    /// Handles the Click event of the btnPrevious control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Response.Redirect(CommonConstants.PAGE_PASSPORTDETAILS);
    }

    /// <summary>
    /// Handles the Click event of the btnCancel control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearControls();

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

    /// <summary>
    /// Handles the Click event of the lnkSaveBtn control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void lnkSaveBtn_Click(object sender, EventArgs e)
    {
        System.Windows.Forms.DialogResult dialougeResult = System.Windows.Forms.MessageBox.Show(CommonConstants.DATANOTSAVED, "", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation);
        if (dialougeResult.ToString() == System.Windows.Forms.DialogResult.Yes.ToString())
        {
            btnSave_Click(null, null);
        }
    }

    /// <summary>
    /// Handles the OnTextChanged event of the txtGPA control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void txtGPA_OnTextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtGPA.Text.Trim()))
        {
            txtOutOf.Enabled = true;
        }
        else
        {
            txtOutOf.Enabled = false;
        }
    }

    /// <summary>
    /// Handles the OnSelectedIndexChanged event of the ddlQualification control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlQualification_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlQualification.SelectedItem.Text == "CA" ||
        ddlQualification.SelectedItem.Text == "CS") 
        {
            mandatorymarkStream.Visible = false;
        }
        else
            mandatorymarkStream.Visible = true;
    }

    /// <summary>
    /// Handles the Click event of the btnEdit control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Session[SessionNames.PAGEMODE] = Common.MasterEnum.PageModeEnum.Edit;
        Qualificationdetails.Enabled = true;
        btnAddRow.Visible = true;
        btnCancel.Visible = true;
        btnEditCancel.Visible = false;
        btnEdit.Visible = false;
        //To solved issue id 19221
        //Coded by Rahul P
        //Start
        btnUpdateRow.Visible = false;
        btnCancelRow.Visible = false;
        //End
    }

    /// <summary>
    /// Handles the Click event of the btnCancel control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnEditCancel_Click(object sender, EventArgs e)
    {
        this.ClearControls();

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

    /// <summary>
    /// Handles the OnTextChanged event of the txtPercentage control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void txtPercentage_OnTextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtPercentage.Text.Trim()))
        {
            txtGPA.Enabled = true;
        }
        else
        {
            txtGPA.Enabled = false;
        }
    }

    #endregion Protected Events

    #region Private Member Functions

    /// <summary>
    /// Does the data bind.
    /// </summary>
    private void DoDataBind()
    {
        gvQualification.DataSource = QualificationDetailsCollection;
        gvQualification.DataBind();

        for (int i = 0; i < gvQualification.Rows.Count; i++)
        {
            if (gvQualification.Rows[i].Cells[5].Text == CommonConstants.ZERO.ToString())
                gvQualification.Rows[i].Cells[5].Text = string.Empty;

            if (gvQualification.Rows[i].Cells[6].Text == CommonConstants.ZERO.ToString())
                gvQualification.Rows[i].Cells[6].Text = string.Empty;

            if (gvQualification.Rows[i].Cells[7].Text == CommonConstants.ZERO.ToString())
                gvQualification.Rows[i].Cells[7].Text = string.Empty;
        }

        if (gvQualification.Rows.Count == 0)
        {
            ShowHeaderWhenEmptyGrid();
        }

        txtOutOf.Enabled = false;
    }

    /// <summary>
    /// Clears the controls.
    /// </summary>
    private void ClearControls()
    {
        ddlQualification.SelectedIndex = 0;
        txtGPA.Text = string.Empty;
        txtUniversityName.Text = string.Empty;
        txtInstituteName.Text = string.Empty;
        txtYearOfPassing.Text = string.Empty;
        txtGPA.Text = string.Empty;
        txtOutOf.Text = string.Empty;
        txtPercentage.Text = string.Empty;
        txtStream.Text = string.Empty;
    }

    /// <summary>
    /// Populates the grid.
    /// </summary>
    private int PopulateGrid(int employeeID)
    {

        int j = 0;
        QualificationDetailsCollection = this.GetQualificationDetails(employeeID);

        //Binding the datatable to grid
        gvQualification.DataSource = QualificationDetailsCollection;
        gvQualification.DataBind();

        for (int i = 0; i < gvQualification.Rows.Count; i++)
        {
            if (gvQualification.Rows[i].Cells[5].Text == CommonConstants.ZERO.ToString())
                gvQualification.Rows[i].Cells[5].Text = string.Empty;

            if (gvQualification.Rows[i].Cells[6].Text == CommonConstants.ZERO.ToString())
                gvQualification.Rows[i].Cells[6].Text = string.Empty;

            if (gvQualification.Rows[i].Cells[7].Text == CommonConstants.ZERO.ToString())
                gvQualification.Rows[i].Cells[7].Text = string.Empty;
        }
        //To solved issue id 19221
        //Coded by Rahul P
        //Start
        j = gvQualification.Rows.Count;
        //End
        if (gvQualification.Rows.Count == 0)
            ShowHeaderWhenEmptyGrid();
        //else
        //    btnAddRow.Text = CommonConstants.BTN_AddRow;
        

        //EMPId.Value = "14";
        EMPId.Value = employeeID.ToString().Trim();
        return j;

    }

    /// <summary>
    /// Gets the qualification details.
    /// </summary>
    /// <returns></returns>
    private BusinessEntities.RaveHRCollection GetQualificationDetails(int employeeID)
    {
        Rave.HR.BusinessLayer.Employee.QualificationDetails objQualificationDetailsBAL;
        BusinessEntities.QualificationDetails objQualificationDetails;

        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            objQualificationDetailsBAL = new Rave.HR.BusinessLayer.Employee.QualificationDetails();
            objQualificationDetails = new BusinessEntities.QualificationDetails();

            //objQualificationDetails.EMPId = 14;
            objQualificationDetails.EMPId = employeeID;

            raveHRCollection = objQualificationDetailsBAL.GetQualificationDetails(objQualificationDetails);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetQualificationDetails", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

        return raveHRCollection;
    }

    ///// <summary>
    ///// Gets the domain users.
    ///// </summary>
    ///// <param name="strUserName">Name of the STR user.</param>
    ///// <returns></returns>
    //public string GetDomainUsers(string strUserName)
    //{
    //    //strUserName = strUserName.Replace("@rave-tech.co.in", "");

    //    string strUserEmail = string.Empty;
    //    AuthorizationManager obj = new AuthorizationManager();
    //    //Google change point to northgate
    //    if (strUserName.ToLower().Contains("@rave-tech.co.in"))
    //    {
    //        strUserName = strUserName.Replace("@rave-tech.co.in", "");
    //        //strUserEmail = strUserName + "@" + AuthorizationManagerConstants.RAVEDOMAINEMAIL;
    //        //strUserName = obj.GetWindowsUsernameAsPerNorthgate(strUserName);
    //        if (Session["WindowsUsername"] == null)
    //        {
    //            strUserName = objRaveHRAuthorizationManager.GetWindowsUsernameAsPerNorthgate(strUserName);
    //        }
    //        else
    //        {
    //            strUserName = Session["WindowsUsername"].ToString().Trim();
    //        }


    //        strUserEmail = strUserName + "@" + AuthorizationManagerConstants.RAVEDOMAINEMAIL;
    //    }
    //    else
    //    {
    //        strUserName = strUserName.Replace(AuthorizationManagerConstants.NORTHGATEDOMAIN, "");
    //        //strUserEmail = strUserName + "@" + AuthorizationManagerConstants.NORTHGATEDOMAINEMAIL;
    //        //strUserName = obj.GetWindowsUsernameAsPerNorthgate(strUserName);
    //        if (Session["WindowsUsername"] == null)
    //        {
    //            strUserName = objRaveHRAuthorizationManager.GetWindowsUsernameAsPerNorthgate(strUserName);
    //        }
    //        else
    //        {
    //            strUserName = Session["WindowsUsername"].ToString().Trim();
    //        }


    //        strUserEmail = strUserName + "@" + AuthorizationManagerConstants.NORTHGATEDOMAINEMAIL;
    //    }

        

    //    //DirectoryEntry searchRoot = new DirectoryEntry("LDAP://RAVE-TECH.CO.IN");

    //    //DirectorySearcher search = new DirectorySearcher(searchRoot);

    //    //SearchResult result;

    //    //SearchResultCollection resultCol = search.FindAll();

    //    //if (resultCol != null)
    //    //{

    //    //    for (int counter = 0; counter < resultCol.Count; counter++)
    //    //    {

    //    //        result = resultCol[counter];

    //    //        if (result.Properties.Contains("samaccountname"))
    //    //        {
    //    //            {

    //    //                strUserEmail = result.Properties["mail"][0].ToString();

    //    //            }

    //    //        }

    //    //    }

    //    //}

    //    return strUserEmail;
    //}

    /// <summary>
    /// Gets the location.
    /// </summary>
    private void GetQualifications()
    {
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        raveHrColl = master.FillDropDownsBL((int)Common.EnumsConstants.Category.Qualification);

        ddlQualification.DataSource = raveHrColl;
        ddlQualification.DataTextField = Common.CommonConstants.DDL_DataTextField;
        ddlQualification.DataValueField = Common.CommonConstants.DDL_DataValueField;
        ddlQualification.DataBind();
        ddlQualification.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

    }

    /// <summary>
    /// Validates the controls.
    /// </summary>
    /// <returns></returns>
    private bool ValidateControls()
    {
        StringBuilder errMessage = new StringBuilder();
        bool flag = true;


        if (string.IsNullOrEmpty(txtUniversityName.Text.Trim()) && string.IsNullOrEmpty(txtInstituteName.Text.Trim()))
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.UNIVERSITYINSTITUTE_VALIDATION);
            flag = false;
        }

        int yearofPassing = Convert.ToInt32(txtYearOfPassing.Text);

        if (!string.IsNullOrEmpty(txtGPA.Text.Trim()) && string.IsNullOrEmpty(txtOutOf.Text.Trim()))
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.GPA_VALIDATION);
            flag = false;
        }

        if (!string.IsNullOrEmpty(txtGPA.Text.Trim()) && !string.IsNullOrEmpty(txtOutOf.Text.Trim()))
        {
            float GpaValue = float.Parse(txtGPA.Text);
            float OutOf = float.Parse(txtOutOf.Text);

            if (OutOf < GpaValue)
            {
                errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.OUTOF_LESSTHAN_GPA);
                flag = false;
            }
        }

        if (string.IsNullOrEmpty(txtGPA.Text.Trim()) && string.IsNullOrEmpty(txtPercentage.Text.Trim()))
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.GPAPercentage_VALIDATION);
            flag = false;
        }

        //if (string.IsNullOrEmpty(txtInstituteName.Text.Trim()))
        //{
        //    errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.SPACES_VALIDATION);
        //    flag = false;
        //}

        int CurrentYear = DateTime.Now.Year;

        if (CurrentYear < yearofPassing)
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.YEAROF_PASSING_VALIDATION);
            flag = false;
        }

        if (!string.IsNullOrEmpty(txtPercentage.Text))
        {
            if (!IsValueNonZero(txtPercentage.Text))
            {
                errMessage.Append(CommonConstants.NEW_LINE + "Percentage Value" + " cannot be zero.");
                flag = false;
            }

            double Percentage = Convert.ToDouble(txtPercentage.Text);

            if (Percentage > 100.00)
            {
                errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.PERCENTAGE_VALIDATION);
                flag = false;
            }
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
            gvQualification.ShowHeader = true;

            //Create empty datasource for Grid view and bind
            QualificationDetailsCollection.Add(new BusinessEntities.QualificationDetails());
            gvQualification.DataSource = QualificationDetailsCollection;
            gvQualification.DataBind();

            //Calculate number of columns in Grid view used for column Span
            int columnsCount = gvQualification.Columns.Count;

            //clear all the cells in the row
            gvQualification.Rows[0].Cells.Clear();

            //add a new blank cell
            gvQualification.Rows[0].Cells.Add(new TableCell());
            gvQualification.Rows[0].Cells[0].Text = NO_RECORDS_FOUND_MESSAGE;
            gvQualification.Rows[0].Cells[0].Wrap = false;
            gvQualification.Rows[0].Cells[0].Width = Unit.Percentage(10);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ShowHeaderWhenEmptyGrid", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
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
        Int32 decVal = Convert.ToInt32(Convert.ToDecimal(value));
        if (value.ToString().Trim().Equals("0") || value.ToString().Trim().Equals("0.0") || value.ToString().Trim().Equals("0.00") || decVal == 0)
        {
            return false;
        }

        return true;
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
        int i = 0;
        //this.PopulateControls();
        if (Session[SessionNames.EMPLOYEEDETAILS] != null)
        {
            
            employee = (BusinessEntities.Employee)Session[SessionNames.EMPLOYEEDETAILS];
            employee = this.GetEmployee(employee);

            lblempName.Text = employee.FirstName.ToUpper() + " " + employee.LastName.ToUpper();
            this.ClearControls();
            //To solved issue id 19221
            //Coded by Rahul P
            //Start
            i = this.PopulateGrid(employee.EMPId);
            //this.GetQualifications();
        }

        if (i == 0)
        {
            Qualificationdetails.Enabled = true;
            btnEdit.Visible = false;
            btnEditCancel.Visible = false;
            btnCancel.Visible = true;
            btnAddRow.Visible = true;
            btnUpdateRow.Visible = false;
            btnCancelRow.Visible = false;

        }
        else
        {
            Qualificationdetails.Enabled = false;
            btnEdit.Visible = true;
            btnEditCancel.Visible = true;
            btnCancel.Visible = false;
            btnAddRow.Visible = false;
            btnUpdateRow.Visible = false;
            btnCancelRow.Visible = false;
        }
        //End

        //Ishwar: Issue Id - 54410 : 31122014 Start
        if (UserMailId.ToLower() != employee.EmailId.ToLower() && arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEPM))
        {
            btnEdit.Visible = false;
            btnCancel.Visible = false;
            btnEditCancel.Visible = true;
            btnAddRow.Visible = false;
            Qualificationdetails.Enabled = false;
        }
        //Ishwar: Issue Id - 54410 : 31122014 Ends

        Session[Common.SessionNames.EMPLOYEEDETAILS] = employee;
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

    #endregion Private Member Functions

}
