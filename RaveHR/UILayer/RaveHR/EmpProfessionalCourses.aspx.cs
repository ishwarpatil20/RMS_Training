//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           EmpProfessionalCourses.aspx.cs    
//  Author:         Shrinivas.Dalavi
//  Date written:   21/8/2009/ 12:01:00 PM
//  Description:    This Page will be the entry page for adding professional details
//                  same page will use in View professional details & Edit professional details mode
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
using Common.AuthorizationManager;
using System.Collections;

public partial class EmpProfessionalCourses : BaseClass
{
    #region Private Field Members

    #region ViewState Constants

    private string PROFESSIONALDETAILS = "ProfessionalDetails";
    private string PROFESSIONALDETAILSDELETE = "ProfessionalDetailsDelete";
    private string DELETEROWINDEX = "DeleteRowIndex";
    private string ROWINDEX = "RowIndex";

    #endregion ViewState Constants

    private string IMGBTNDELETE ="imgBtnDelete";
    private string IMGBTNEDIT = "imgBtnEdit";
    private string CLASS_NAME = "EmpProfessionalCourses.aspx";
    private string PROFESSIONALID = "ProfessionalId";
    private string MODE = "Mode";
    Rave.HR.BusinessLayer.Employee.ProfessionalDetails ObjProfessionalDetailsBL = new Rave.HR.BusinessLayer.Employee.ProfessionalDetails();
    private int employeeID = 0;
    private BusinessEntities.Employee employee;
    string UserRaveDomainId;
    string UserMailId;

    AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
    // Get logged-in user's email id
    AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
    ArrayList arrRolesForUser = new ArrayList();

    /// <summary>
    /// Defines a constant error for no records found after applying filter criteria
    /// </summary>
    private const string NO_RECORDS_FOUND_MESSAGE = "No records found.";


    #endregion Private Field Members

    #region Local Properties

    /// <summary>
    /// Gets or sets the professional details collection.
    /// </summary>
    /// <value>The professional details collection.</value>
    private BusinessEntities.RaveHRCollection ProfessionalDetailsCollection
    {
        get
        {
            if (ViewState[PROFESSIONALDETAILS] != null)
                return ((BusinessEntities.RaveHRCollection)ViewState[PROFESSIONALDETAILS]);
            else
                return null;
        }
        set
        {
            ViewState[PROFESSIONALDETAILS] = value;
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

        txtCourseName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtCourseName.ClientID + "','" + imgCourseName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPACE + "');");
        imgCourseName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanCourseName.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
        imgCourseName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanCourseName.ClientID + "');");

        txtInstituteName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtInstituteName.ClientID + "','" + imgInstituteName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPECIALCHAR + "');");
        imgInstituteName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanInstituteName.ClientID + "','" + Common.CommonConstants.MSG_ALPHABET_SPECIALCHAR + "');");
        imgInstituteName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanInstituteName.ClientID + "');");

        txtYearofPassing.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtYearofPassing.ClientID + "','" + imgYearofPassing.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
        imgYearofPassing.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanYearofPassing.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgYearofPassing.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanYearofPassing.ClientID + "');");

        txtScore.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtScore.ClientID + "','" + imgScore.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
        imgScore.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanScore.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgScore.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanScore.ClientID + "');");

        txtOutOf.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtOutOf.ClientID + "','" + imgOutOf.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
        imgOutOf.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanOutOf.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgOutOf.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanOutOf.ClientID + "');");


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
                            Professionaldetails.Enabled = true;
                            btnCancel.Visible = true;
                            if (gvProfessionalCourses.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
                                btnSave.Visible = true;
                        }
                        else
                        {
                            Professionaldetails.Enabled = false;
                            btnSave.Visible = false;
                            btnCancel.Visible = false;

                        }
                    }
                }
                else
                {
                    if (PageMode == Common.MasterEnum.PageModeEnum.Edit.ToString() && arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
                    {
                        Professionaldetails.Enabled = true;
                        btnCancel.Visible = true;
                        if (gvProfessionalCourses.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
                            btnSave.Visible = true;
                    }
                    else
                    {
                        Professionaldetails.Enabled = false;
                        btnSave.Visible = false;
                        btnCancel.Visible = false;

                    }
                }
            }
        }
    }

    /// <summary>
    /// Handles the Click event of the btnAdd control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ValidateControls())
        {
            BusinessEntities.ProfessionalDetails objProfessionalDetails = new BusinessEntities.ProfessionalDetails();

            if (gvProfessionalCourses.Rows[0].Cells[0].Text == NO_RECORDS_FOUND_MESSAGE)
            {
                ProfessionalDetailsCollection.Clear();
            }

            objProfessionalDetails.CourseName = txtCourseName.Text;
            objProfessionalDetails.InstitutionName = txtInstituteName.Text;
            objProfessionalDetails.PassingYear = txtYearofPassing.Text;
            objProfessionalDetails.Score = txtScore.Text;
            objProfessionalDetails.Outof = txtOutOf.Text;
            objProfessionalDetails.Mode = 1;

            ProfessionalDetailsCollection.Add(objProfessionalDetails);

            this.DoDataBind();

            this.ClearControls();

            if (gvProfessionalCourses.Rows.Count != 0)
                btnSave.Visible = true;
        }
    }
  
    /// <summary>
    /// Handles the RowEditing event of the gvProfessionalCourses control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewEditEventArgs"/> instance containing the event data.</param>
    protected void gvProfessionalCourses_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int rowIndex = 0;

        txtCourseName.Text = gvProfessionalCourses.Rows[e.NewEditIndex].Cells[0].Text.Trim();
        txtInstituteName.Text = gvProfessionalCourses.Rows[e.NewEditIndex].Cells[1].Text.Trim();
        txtYearofPassing.Text = gvProfessionalCourses.Rows[e.NewEditIndex].Cells[2].Text;
        txtScore.Text = gvProfessionalCourses.Rows[e.NewEditIndex].Cells[3].Text;
        txtOutOf.Text = gvProfessionalCourses.Rows[e.NewEditIndex].Cells[4].Text;

        ImageButton btnImg = (ImageButton)gvProfessionalCourses.Rows[e.NewEditIndex].FindControl(IMGBTNDELETE);
        rowIndex = e.NewEditIndex;
        ViewState[ROWINDEX] = null;
        ViewState[ROWINDEX] = rowIndex;
        btnImg.Enabled = false;

        btnAddRow.Visible = false;
        btnUpdateRow.Visible = true;
        btnCancelRow.Visible = true;

        //Disabling all the edit buttons.
        for (int i = 0; i < gvProfessionalCourses.Rows.Count; i++)
        {
            ImageButton btnImgEdit = (ImageButton)gvProfessionalCourses.Rows[i].FindControl(IMGBTNEDIT);
            if (i != rowIndex)
                btnImgEdit.Enabled = false;
        }

    }

    /// <summary>
    /// Handles the RowDeleting event of the gvProfessionalCourses control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewDeleteEventArgs"/> instance containing the event data.</param>
    protected void gvProfessionalCourses_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int deleteRowIndex = 0;
        int rowIndex = -1;

        BusinessEntities.ProfessionalDetails objProfessionalDetails = new BusinessEntities.ProfessionalDetails();
            
        deleteRowIndex = e.RowIndex;
        
        objProfessionalDetails = (BusinessEntities.ProfessionalDetails)ProfessionalDetailsCollection.Item(deleteRowIndex);
        objProfessionalDetails.Mode = 3;

        if (ViewState[PROFESSIONALDETAILSDELETE] != null)
        {
            BusinessEntities.RaveHRCollection objDeleteProfessionalDetailsCollection = (BusinessEntities.RaveHRCollection)ViewState[PROFESSIONALDETAILSDELETE];
            objDeleteProfessionalDetailsCollection.Add(objProfessionalDetails);

            ViewState[PROFESSIONALDETAILSDELETE] = objDeleteProfessionalDetailsCollection;
        }
        else
        {
            BusinessEntities.RaveHRCollection objDeleteProfessionalDetailsCollection1 = new BusinessEntities.RaveHRCollection();

            objDeleteProfessionalDetailsCollection1.Add(objProfessionalDetails);

            ViewState[PROFESSIONALDETAILSDELETE] = objDeleteProfessionalDetailsCollection1;
        }
        
        ProfessionalDetailsCollection.RemoveAt(deleteRowIndex);

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

            ImageButton btnImg = (ImageButton)gvProfessionalCourses.Rows[rowIndex].FindControl(IMGBTNDELETE);
            btnImg.Enabled = false;

            //Disabling all the edit buttons.
            for (int i = 0; i < gvProfessionalCourses.Rows.Count; i++)
            {
                if (rowIndex != i)
                {
                    ImageButton btnImgEdit = (ImageButton)gvProfessionalCourses.Rows[i].FindControl(IMGBTNEDIT);
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
        Rave.HR.BusinessLayer.Employee.ProfessionalDetails objProfessionalDetailsBAL;
        
        BusinessEntities.ProfessionalDetails objProfessionalDetails;
        BusinessEntities.RaveHRCollection objSaveProfessionalDetailsCollection = new BusinessEntities.RaveHRCollection();
       
        try
        {
            objProfessionalDetailsBAL = new Rave.HR.BusinessLayer.Employee.ProfessionalDetails();

            if (gvProfessionalCourses.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
            {
                for (int i = 0; i < gvProfessionalCourses.Rows.Count; i++)
                {
                    objProfessionalDetails = new BusinessEntities.ProfessionalDetails();

                    Label ProfessionalId = (Label)gvProfessionalCourses.Rows[i].FindControl(PROFESSIONALID);
                    Label Mode = (Label)gvProfessionalCourses.Rows[i].FindControl(MODE);

                    objProfessionalDetails.ProfessionalId = int.Parse(ProfessionalId.Text);
                    objProfessionalDetails.EMPId = int.Parse(EMPId.Value);
                    objProfessionalDetails.CourseName = gvProfessionalCourses.Rows[i].Cells[0].Text;
                    objProfessionalDetails.InstitutionName = gvProfessionalCourses.Rows[i].Cells[1].Text;
                    objProfessionalDetails.PassingYear = gvProfessionalCourses.Rows[i].Cells[2].Text;
                    objProfessionalDetails.Score =gvProfessionalCourses.Rows[i].Cells[3].Text;
                    objProfessionalDetails.Outof = gvProfessionalCourses.Rows[i].Cells[4].Text;
                    objProfessionalDetails.Mode = int.Parse(Mode.Text);
                    objSaveProfessionalDetailsCollection.Add(objProfessionalDetails);
                }
            }
            BusinessEntities.RaveHRCollection objDeleteProfessionalDetailsCollection = (BusinessEntities.RaveHRCollection)ViewState[PROFESSIONALDETAILSDELETE];

            if (objDeleteProfessionalDetailsCollection != null)
            { 
                BusinessEntities.ProfessionalDetails obj = null;

                for (int i = 0; i < objDeleteProfessionalDetailsCollection.Count; i++)
                {
                    objProfessionalDetails = new BusinessEntities.ProfessionalDetails();
                    obj = (BusinessEntities.ProfessionalDetails)objDeleteProfessionalDetailsCollection.Item(i);

                    objProfessionalDetails.ProfessionalId = obj.ProfessionalId;
                    objProfessionalDetails.EMPId = obj.EMPId; 
                    objProfessionalDetails.CourseName = obj.CourseName; 
                    objProfessionalDetails.InstitutionName = obj.InstitutionName; 
                    objProfessionalDetails.PassingYear = obj.PassingYear; 
                    objProfessionalDetails.Score = obj.Score; 
                    objProfessionalDetails.Outof = obj.Outof;
                    objProfessionalDetails.Mode = obj.Mode;

                    objSaveProfessionalDetailsCollection.Add(objProfessionalDetails);
                }

            }
            objProfessionalDetailsBAL.Manipulation(objSaveProfessionalDetailsCollection);

            if (ViewState.Count > 0)
            {
                ViewState.Clear();
            }

            //Refresh the grip after saving
            //this.PopulateGrid();
            if (EMPId.Value != string.Empty)
            {
                int empID = Convert.ToInt32(EMPId.Value);
                //Refresh the grip after saving
                this.PopulateGrid(empID);
            }

            if (gvProfessionalCourses.Rows.Count == 0)
                btnSave.Visible = false;

            lblMessage.Text = "Prosessional Courses saved successfully.";
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
    /// Handles the Click event of the btnUpdate control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (ValidateControls())
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

                Label ProfessionalId = (Label)gvProfessionalCourses.Rows[rowIndex].FindControl(PROFESSIONALID);
                Label Mode = (Label)gvProfessionalCourses.Rows[rowIndex].FindControl(MODE);

                gvProfessionalCourses.Rows[rowIndex].Cells[0].Text = txtCourseName.Text;
                gvProfessionalCourses.Rows[rowIndex].Cells[1].Text = txtInstituteName.Text;
                gvProfessionalCourses.Rows[rowIndex].Cells[2].Text = txtYearofPassing.Text;
                gvProfessionalCourses.Rows[rowIndex].Cells[3].Text = txtScore.Text;
                gvProfessionalCourses.Rows[rowIndex].Cells[4].Text = txtOutOf.Text;

                if (int.Parse(ProfessionalId.Text) == 0)
                {
                    Mode.Text = "1";
                }
                else
                {
                    Mode.Text = "2";
                }

                ImageButton btnImg = (ImageButton)gvProfessionalCourses.Rows[rowIndex].FindControl(IMGBTNDELETE);
                btnImg.Enabled = true;
                ViewState[ROWINDEX] = null;
                ViewState[DELETEROWINDEX] = null;

            }

            for (int i = 0; i < ProfessionalDetailsCollection.Count; i++)
            {
                BusinessEntities.ProfessionalDetails objProfessionalDetails = new BusinessEntities.ProfessionalDetails();
                objProfessionalDetails = (BusinessEntities.ProfessionalDetails)ProfessionalDetailsCollection.Item(i);

                Label ProfessionalId = (Label)gvProfessionalCourses.Rows[i].FindControl(PROFESSIONALID);
                Label Mode = (Label)gvProfessionalCourses.Rows[rowIndex].FindControl(MODE);

                objProfessionalDetails.ProfessionalId = int.Parse(ProfessionalId.Text);
                objProfessionalDetails.EMPId = int.Parse(EMPId.Value);

                objProfessionalDetails.CourseName = gvProfessionalCourses.Rows[i].Cells[0].Text;
                objProfessionalDetails.InstitutionName = gvProfessionalCourses.Rows[i].Cells[1].Text;
                objProfessionalDetails.PassingYear = gvProfessionalCourses.Rows[i].Cells[2].Text;
                objProfessionalDetails.Score = gvProfessionalCourses.Rows[i].Cells[3].Text;
                objProfessionalDetails.Outof = gvProfessionalCourses.Rows[i].Cells[4].Text;
                objProfessionalDetails.Mode = int.Parse(Mode.Text);
            }

            //Clear all the fields after inserting row into gridview
            this.ClearControls();

            btnAddRow.Visible = true;
            btnUpdateRow.Visible = false;
            btnCancelRow.Visible = false;

            //Enabling all the edit buttons.
            for (int i = 0; i < gvProfessionalCourses.Rows.Count; i++)
            {
                ImageButton btnImgEdit = (ImageButton)gvProfessionalCourses.Rows[i].FindControl(IMGBTNEDIT);
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
            ImageButton btnImg = (ImageButton)gvProfessionalCourses.Rows[rowIndex].FindControl(IMGBTNDELETE);
            btnImg.Enabled = true;
            ViewState[ROWINDEX] = null;
            ViewState[DELETEROWINDEX] = null;
        }

        //Enabling all the edit buttons.
        for (int i = 0; i < gvProfessionalCourses.Rows.Count; i++)
        {
            ImageButton btnImgEdit = (ImageButton)gvProfessionalCourses.Rows[i].FindControl(IMGBTNEDIT);
            btnImgEdit.Enabled = true;
        }

        btnAddRow.Visible = true;
        btnUpdateRow.Visible = false;
        btnCancelRow.Visible = false;

    }

    /// <summary>
    /// Handles the Click event of the btnNext control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Response.Redirect(CommonConstants.PAGE_CERTIFICATIONDETAILS);
    }

    /// <summary>
    /// Handles the Click event of the btnPrevious control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnPrevious_Click(object sender, EventArgs e)
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
        this.ClearControls();
    }

    #endregion

    #region Private Member Functions

    /// <summary>
    /// Does the data bind.
    /// </summary>
    private void DoDataBind()
    {   
        gvProfessionalCourses.DataSource = ProfessionalDetailsCollection;
        gvProfessionalCourses.DataBind();

        //Displaying grid header with NO record found message.
        if (gvProfessionalCourses.Rows.Count == 0)
            ShowHeaderWhenEmptyGrid();

    }

    /// <summary>
    /// Clears the controls.
    /// </summary>
    private void ClearControls()
    {
        txtCourseName.Text = string.Empty;
        txtInstituteName.Text = string.Empty;
        txtYearofPassing.Text = string.Empty;
        txtScore.Text = string.Empty;
        txtOutOf.Text = string.Empty;
    }

    /// <summary>
    /// Populates the grid.
    /// </summary>
    private void PopulateGrid(int employeeID)
    {
        ProfessionalDetailsCollection = this.GetProfessionalDetails(employeeID);

        //Binding the datatable to grid
        gvProfessionalCourses.DataSource = ProfessionalDetailsCollection;
        gvProfessionalCourses.DataBind();

        //Displaying grid header with NO record found message.
        if (gvProfessionalCourses.Rows.Count == 0)
            ShowHeaderWhenEmptyGrid();

        //EMPId.Value = "14";
        EMPId.Value = employeeID.ToString().Trim();

    }

    /// <summary>
    /// Gets the professional details.
    /// </summary>
    /// <returns></returns>
    private BusinessEntities.RaveHRCollection GetProfessionalDetails(int employeeID)
    {
        Rave.HR.BusinessLayer.Employee.ProfessionalDetails objProfessionalDetailsBAL;
        BusinessEntities.ProfessionalDetails objProfessionalDetails;

        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            objProfessionalDetailsBAL = new Rave.HR.BusinessLayer.Employee.ProfessionalDetails();
            objProfessionalDetails = new BusinessEntities.ProfessionalDetails();
            
            //objProfessionalDetails.EMPId = 14;
            objProfessionalDetails.EMPId = employeeID;

            raveHRCollection = objProfessionalDetailsBAL.GetProfessionalDetails(objProfessionalDetails);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetProfessionalDetails", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
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

        int Score = Convert.ToInt32(txtScore.Text);
        int OutOf = Convert.ToInt32(txtOutOf.Text);
        int yearofPassing = Convert.ToInt32(txtYearofPassing.Text);
        
        if (OutOf < Score)
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.OUTOF_LESSTHAN_SCORE);
            flag = false;
        }

        int CurrentYear = DateTime.Now.Year;

        if (CurrentYear < yearofPassing)
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.YEAROF_PASSING_VALIDATION);
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
            gvProfessionalCourses.ShowHeader = true;

            //Create empty datasource for Grid view and bind
            ProfessionalDetailsCollection.Add(new BusinessEntities.ProfessionalDetails());
            gvProfessionalCourses.DataSource = ProfessionalDetailsCollection;
            gvProfessionalCourses.DataBind();

            //Calculate number of columns in Grid view used for column Span
            int columnsCount = gvProfessionalCourses.Columns.Count;

            //clear all the cells in the row
            gvProfessionalCourses.Rows[0].Cells.Clear();

            //add a new blank cell
            gvProfessionalCourses.Rows[0].Cells.Add(new TableCell());
            gvProfessionalCourses.Rows[0].Cells[0].Text = NO_RECORDS_FOUND_MESSAGE;
            gvProfessionalCourses.Rows[0].Cells[0].Wrap = false;
            gvProfessionalCourses.Rows[0].Cells[0].Width = Unit.Percentage(10);

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
