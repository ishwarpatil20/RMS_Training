//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           EmpQualificationDetails.aspx.cs    
//  Author:         Shrinivas.Dalavi
//  Date written:   21/8/2009/ 12:01:00 PM
//  Description:    This Page will be the entry page for adding Qualification details
//                  same page will use in View Qualification details & Edit Qualification details mode
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  21/8/2009/ 12:01:00 PM  Shrinivas.Dalavi    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Text;
using System.Web.UI.WebControls;
using Common;
using System.DirectoryServices;
using Common.AuthorizationManager;
using System.Collections;

public partial class EmpQualificationDetails : BaseClass
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

        txtUniversityName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtUniversityName.ClientID + "','" + imgUniversityName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPECIALCHAR + "');");
        imgUniversityName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanUniversityName.ClientID + "','" + Common.CommonConstants.MSG_ALPHABET_SPECIALCHAR + "');");
        imgUniversityName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanUniversityName.ClientID + "');");

        txtInstituteName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtInstituteName.ClientID + "','" + imgInstituteName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPECIALCHAR + "');");
        imgInstituteName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanInstituteName.ClientID + "','" + Common.CommonConstants.MSG_ALPHABET_SPECIALCHAR + "');");
        imgInstituteName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanInstituteName.ClientID + "');");

        txtYearOfPassing.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtYearOfPassing.ClientID + "','" + imgYearOfPassing.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
        imgYearOfPassing.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanYearOfPassing.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgYearOfPassing.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanYearOfPassing.ClientID + "');");

        txtGPA.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtGPA.ClientID + "','" + imgGPA.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
        imgGPA.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanGPA.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgGPA.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanGPA.ClientID + "');");

        txtOutOf.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtOutOf.ClientID + "','" + imgOutOf.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
        imgOutOf.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanOutOf.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgOutOf.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanOutOf.ClientID + "');");

        txtPercentage.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtPercentage.ClientID + "','" + imgPercentage.ClientID + "','" + "Decimal" + "');");
        imgPercentage.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanPercentage.ClientID + "','" + Common.CommonConstants.MSG_DECIMAL + "');");
        imgPercentage.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanPercentage.ClientID + "');");

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
                this.PopulateGrid(employeeID);
                this.GetQualifications();
            }

            // Get logged-in user's email id
            AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
            UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
            UserMailId = UserRaveDomainId.Replace("co.in", "com");


            if (Session[SessionNames.PAGEMODE] != null)
            {
                string PageMode = Session[SessionNames.PAGEMODE].ToString();
                arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());

                if (UserMailId.ToLower() == employee.EmailId.ToLower())
                {
                    if (arrRolesForUser.Count > 0)
                    {
                        if (PageMode == Common.MasterEnum.PageModeEnum.Edit.ToString())
                        {
                            Qualificationdetails.Enabled = true;
                            btnCancel.Visible = true;
                            if (gvQualification.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
                                btnSave.Visible = true;
                        }
                        else
                        {
                            Qualificationdetails.Enabled = false;
                            btnSave.Visible = false;
                            btnCancel.Visible = false;

                        }
                    }
                }
                else
                {
                    if (PageMode == Common.MasterEnum.PageModeEnum.Edit.ToString() && arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
                    {
                        Qualificationdetails.Enabled = true;
                        btnCancel.Visible = true;
                        if (gvQualification.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
                            btnSave.Visible = true;
                    }
                    else
                    {
                        Qualificationdetails.Enabled = false;
                        btnSave.Visible = false;
                        btnCancel.Visible = false;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Handles the Click event of the btnAddRow control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ValidateControls())
        {
            BusinessEntities.QualificationDetails objQualificationDetails = new BusinessEntities.QualificationDetails();

            if (gvQualification.Rows[0].Cells[0].Text == NO_RECORDS_FOUND_MESSAGE)
            {
                QualificationDetailsCollection.Clear();
            }

            objQualificationDetails.QualificationName = ddlQualification.SelectedItem.Text;
            objQualificationDetails.UniversityName = txtUniversityName.Text;
            objQualificationDetails.InstituteName = txtInstituteName.Text;
            objQualificationDetails.PassingYear = txtYearOfPassing.Text;
            if (!string.IsNullOrEmpty(txtGPA.Text))
            objQualificationDetails.GPA = float.Parse(txtGPA.Text);
            objQualificationDetails.Outof = float.Parse(txtOutOf.Text);
            objQualificationDetails.Percentage = float.Parse(txtPercentage.Text);
            objQualificationDetails.Qualification = int.Parse(ddlQualification.SelectedItem.Value);
            objQualificationDetails.Mode = 1;
            objQualificationDetails.QualificationId = 0;

            QualificationDetailsCollection.Add(objQualificationDetails);

            this.DoDataBind();

            this.ClearControls();

            if (gvQualification.Rows.Count != 0)
                btnSave.Visible = true;
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
        txtUniversityName.Text = gvQualification.Rows[e.NewEditIndex].Cells[1].Text.Trim();
        txtInstituteName.Text = gvQualification.Rows[e.NewEditIndex].Cells[2].Text.Trim();
        txtYearOfPassing.Text = gvQualification.Rows[e.NewEditIndex].Cells[3].Text;
        txtGPA.Text = gvQualification.Rows[e.NewEditIndex].Cells[4].Text;
        txtOutOf.Text = gvQualification.Rows[e.NewEditIndex].Cells[5].Text;
        txtPercentage.Text = gvQualification.Rows[e.NewEditIndex].Cells[6].Text;

        ImageButton btnImg = (ImageButton)gvQualification.Rows[e.NewEditIndex].FindControl(IMGBTNDELETE);
        rowIndex = e.NewEditIndex;
        ViewState[ROWINDEX] = null;
        ViewState[ROWINDEX] = rowIndex;
        btnImg.Enabled = false;

        btnAddRow.Visible = false;
        btnUpdateRow.Visible = true;
        btnCancelRow.Visible = true;

        //Disabling all the edit buttons.
        for (int i = 0; i < gvQualification.Rows.Count; i++)
        {
            ImageButton btnImgEdit = (ImageButton)gvQualification.Rows[i].FindControl(IMGBTNEDIT);
            if (i != rowIndex)
                btnImgEdit.Enabled = false;
        }
        
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

        BusinessEntities.QualificationDetails objQualificationDetails = new BusinessEntities.QualificationDetails();

        deleteRowIndex = e.RowIndex;

        objQualificationDetails = (BusinessEntities.QualificationDetails)QualificationDetailsCollection.Item(deleteRowIndex);
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
                        objQualificationDetails.UniversityName = gvQualification.Rows[i].Cells[1].Text;
                        objQualificationDetails.InstituteName = gvQualification.Rows[i].Cells[2].Text;
                        objQualificationDetails.PassingYear = gvQualification.Rows[i].Cells[3].Text;
                        objQualificationDetails.GPA = float.Parse(gvQualification.Rows[i].Cells[4].Text);
                        objQualificationDetails.Outof = float.Parse(gvQualification.Rows[i].Cells[5].Text);
                        objQualificationDetails.Percentage = float.Parse(gvQualification.Rows[i].Cells[6].Text);
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

                 if (gvQualification.Rows[0].Cells[0].Text == NO_RECORDS_FOUND_MESSAGE)
                     btnSave.Visible = false;


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

        btnAddRow.Visible = true;
        btnUpdateRow.Visible = false;
        btnCancelRow.Visible = false;

    }

    /// <summary>
    /// Handles the Click event of the btnUpdateRow control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnUpdateRow_Click(object sender, EventArgs e)
    {
        if (ValidateControls())
        {

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
                gvQualification.Rows[rowIndex].Cells[1].Text = txtUniversityName.Text;
                gvQualification.Rows[rowIndex].Cells[2].Text = txtInstituteName.Text;
                gvQualification.Rows[rowIndex].Cells[3].Text = txtYearOfPassing.Text;
                gvQualification.Rows[rowIndex].Cells[4].Text = txtGPA.Text;

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
                BusinessEntities.QualificationDetails objQualificationDetails = new BusinessEntities.QualificationDetails();
                objQualificationDetails = (BusinessEntities.QualificationDetails)QualificationDetailsCollection.Item(i);

                Label QualificationId = (Label)gvQualification.Rows[i].FindControl(QUALIFICATIONID);
                Label Mode = (Label)gvQualification.Rows[rowIndex].FindControl(MODE);

                objQualificationDetails.QualificationId = int.Parse(QualificationId.Text);
                objQualificationDetails.EMPId = int.Parse(EMPId.Value);
                objQualificationDetails.QualificationName = gvQualification.Rows[i].Cells[0].Text;
                objQualificationDetails.UniversityName = gvQualification.Rows[i].Cells[1].Text;
                objQualificationDetails.InstituteName = gvQualification.Rows[i].Cells[2].Text;
                objQualificationDetails.PassingYear = gvQualification.Rows[i].Cells[3].Text;
                if (!string.IsNullOrEmpty(gvQualification.Rows[i].Cells[4].Text))
                {
                    objQualificationDetails.GPA = float.Parse(gvQualification.Rows[i].Cells[4].Text);
                }
                else
                {
                    objQualificationDetails.GPA = CommonConstants.ZERO;
                }
                objQualificationDetails.Outof = float.Parse(gvQualification.Rows[i].Cells[5].Text);
                objQualificationDetails.Percentage = float.Parse(gvQualification.Rows[i].Cells[6].Text);
                objQualificationDetails.Mode = int.Parse(Mode.Text);
            }

            //Clear all the fields after inserting row into gridview
            this.ClearControls();

            btnAddRow.Visible = true;
            btnUpdateRow.Visible = false;
            btnCancelRow.Visible = false;

            //Enabling all the edit buttons.
            for (int i = 0; i < gvQualification.Rows.Count; i++)
            {
                ImageButton btnImgEdit = (ImageButton)gvQualification.Rows[i].FindControl(IMGBTNEDIT);
                btnImgEdit.Enabled = true;
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

        if (gvQualification.Rows.Count == 0)
            ShowHeaderWhenEmptyGrid();

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
    }

    /// <summary>
    /// Populates the grid.
    /// </summary>
    private void PopulateGrid(int employeeID)
    {
        QualificationDetailsCollection = this.GetQualificationDetails(employeeID);

        //Binding the datatable to grid
        gvQualification.DataSource = QualificationDetailsCollection;
        gvQualification.DataBind();

        if (gvQualification.Rows.Count == 0)
            ShowHeaderWhenEmptyGrid();

        //EMPId.Value = "14";
        EMPId.Value = employeeID.ToString().Trim();

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
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetQualificationDetails", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

        return raveHRCollection;
    }

    //[System.Web.Services.WebMethod]
    //[System.Web.Script.Services.ScriptMethod]
    //public static string[] GetCountryNames(string prefixText, int count)
    //{
    //    return Search(prefixText);
    //}

    //private static ArrayList CountryList()
    //{
    //    ArrayList list = new ArrayList();
    //    list.Add("Afghanistan");
    //    list.Add("Albania");
    //    list.Add("Algeria");
    //    list.Add("Argentina");
    //    list.Add("Australia");
    //    list.Add("Bahrain");
    //    list.Add("Bangladesh");
    //    list.Add("Belgium");
    //    list.Add("Bhutan");
    //    list.Add("Bolivia");
    //    list.Add("Brazil");
    //    list.Add("British Virgin Islands");
    //    list.Add("Cambodia");
    //    list.Add("Canada");
    //    list.Add("Chile");
    //    list.Add("China");
    //    list.Add("Colombia");
    //    list.Add("Cuba");
    //    list.Add("Czech Republic");
    //    list.Add("Dominica");
    //    list.Add("Denmark");
    //    list.Add("Egypt");
    //    list.Add("Estonia");
    //    list.Add("Ethiopia");
    //    list.Add("Europa Island");
    //    list.Add("Fiji");
    //    list.Add("Finland");
    //    list.Add("France");
    //    list.Add("Germany");
    //    list.Add("Ghana");
    //    list.Add("Greece");
    //    list.Add("Hong Kong");
    //    list.Add("Hungary");
    //    list.Add("Iceland");
    //    list.Add("India");
    //    list.Add("Indonesia");
    //    list.Add("Iran");
    //    list.Add("Iraq");
    //    list.Add("Ireland");
    //    list.Add("Italy");
    //    list.Add("Japan");
    //    list.Add("Jordan");
    //    list.Add("Kenya");
    //    list.Add("Kuwait");
    //    list.Add("Libya");
    //    list.Add("Malaysia");
    //    list.Add("Mexico");
    //    list.Add("Myanmar");
    //    list.Add("Nepal");
    //    list.Add("Netherlands");
    //    list.Add("New Zealand");
    //    list.Add("Norway");
    //    list.Add("Oman");
    //    list.Add("Pakistan");
    //    list.Add("Palestine");
    //    list.Add("Peru");
    //    list.Add("Philippines");
    //    list.Add("Poland");
    //    list.Add("Qatar");
    //    list.Add("Romania");
    //    list.Add("Russia");
    //    list.Add("Saint Lucia");
    //    list.Add("Saudi Arabia");
    //    list.Add("Scotland");
    //    list.Add("Singapore");
    //    list.Add("South Africa");
    //    list.Add("Spain");
    //    list.Add("Sri Lanka");
    //    list.Add("Switzerland");
    //    list.Add("Taiwan");
    //    list.Add("Thailand");
    //    list.Add("Turkey");
    //    list.Add("Uganda");
    //    list.Add("Ukraine");
    //    list.Add("United Arab Emirates");
    //    list.Add("United Kingdom");
    //    list.Add("Vatican City");
    //    list.Add("Vietnam");
    //    list.Add("Wake Island");
    //    list.Add("Wales");
    //    list.Add("Yemen");
    //    list.Add("Yugoslavia");
    //    list.Add("Zambia");
    //    list.Add("Zimbabwe");

    //    return list;
    //}

    //private static string[] Search(string prefix)
    //{
    //    ArrayList list = CountryList();
    //    ArrayList searchList = new ArrayList();

    //    foreach (string search in list)
    //    {
    //        if (search.ToLower().StartsWith(prefix.ToLower()))
    //            searchList.Add(search);
    //    }

    //    return (string[])searchList.ToArray(typeof(string));
    //}

    /* going google
    public string GetDomainUsers(string strUserName)
    {
        //strUserName = strUserName.Replace("@rave-tech.co.in", "");
        //Google change point to northgate
        if (strUserName.ToLower().Contains("@rave-tech.co.in"))
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
        //string query = "(SAMAccountName=" + strUserName + ")";

        //search.Filter = query;

        SearchResult result;

        SearchResultCollection resultCol = search.FindAll();

        if (resultCol != null)
        {

            for (int counter = 0; counter < resultCol.Count; counter++)
            {

                result = resultCol[counter];

                if (result.Properties.Contains("samaccountname"))
                {

                    //string strAccountName = result.Properties["samaccountname"][0].ToString();

                    //if (strAccountName.ToLower() == strUserName.ToLower())
                    {

                        strUserEmail = result.Properties["mail"][0].ToString();

                    }

                }

            }

        }

        return strUserEmail;
    }
     
     */
     

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
        ddlQualification.Items.Insert(CommonConstants.ZERO,CommonConstants.SELECT);

    }

    /// <summary>
    /// Validates the controls.
    /// </summary>
    /// <returns></returns>
    private bool ValidateControls()
    {
       StringBuilder errMessage = new StringBuilder();
       bool flag=true;

        int yearofPassing= Convert.ToInt32(txtYearOfPassing.Text);
        double Percentage = Convert.ToDouble(txtPercentage.Text);

        if (string.IsNullOrEmpty(txtGPA.Text.Trim()) && string.IsNullOrEmpty(txtOutOf.Text.Trim()))
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.GPAOUTOF_VALIDATION);
            flag = false;
        }
        else
        {
            if (!string.IsNullOrEmpty(txtGPA.Text.Trim()) && string.IsNullOrEmpty(txtOutOf.Text.Trim()))
            {
                errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.GPA_VALIDATION);
                flag = false;
            }
            
        }

        if (!string.IsNullOrEmpty(txtGPA.Text.Trim()) && !string.IsNullOrEmpty(txtOutOf.Text.Trim()))
        {
            int GpaValue = Convert.ToInt32(txtGPA.Text);
            int OutOf = Convert.ToInt32(txtOutOf.Text);

            if (OutOf < GpaValue)
            {
                errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.OUTOF_LESSTHAN_GPA);
                flag = false;
            }
        }
        if (string.IsNullOrEmpty(txtUniversityName.Text.Trim()))
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.SPACES_VALIDATION);
            flag = false;
        }

        if (string.IsNullOrEmpty(txtInstituteName.Text.Trim()))
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.SPACES_VALIDATION);
            flag = false;
        }

        int CurrentYear = DateTime.Now.Year;

        if (CurrentYear < yearofPassing)
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.YEAROF_PASSING_VALIDATION);
            flag = false;
        }

        if (Percentage > 100.00)
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.PERCENTAGE_VALIDATION);
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
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ShowHeaderWhenEmptyGrid", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }   

    #endregion Private Member Functions



}
