//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           EmpSkillsDetails.aspx.cs    
//  Author:         Shrinivas.Dalavi
//  Date written:   21/8/2009/ 12:01:00 PM
//  Description:    This Page will be the entry page for adding skills details
//                  same page will use in View skills details & Edit skills details 
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  21/8/2009/ 12:01:00 PM  Shrinivas.Dalavi    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Text;
using System.Collections;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Common;
using Common.AuthorizationManager;

public partial class EmpSkillsDetails : BaseClass
{
    #region Private Field Members

    /// <summary>
    /// private variable for Class Name used in each catch block
    /// </summary>
    private string CLASS_NAME = "EmpSkillsDetails.aspx";

    /// <summary>
    /// private string variable for Image Button Edit
    /// </summary>
    private string IBTNEDIT = "imgBtnEdit";

    /// <summary>
    /// private string variable for Image Button Delete
    /// </summary>
    private string IBTNDELETE = "imgBtnDelete";
    private string SKILL = "Skill";
    private string SKILLID = "SkillsId";
    private string PROFICIENCY = "Proficiency";
    private string MODE = "Mode";
    private string SKILLTYPE = "SkillType";
    private int employeeID = 0;
    private BusinessEntities.Employee employee;
    char[] SPILITER_SLASH = { '/' };
    const string ReadOnly = "readonly";
    int SkillType = 0;
    string UserRaveDomainId;
    string UserMailId;
    AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
    // Get logged-in user's email id
    AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
    ArrayList arrRolesForUser = new ArrayList();
    AuthorizationManager authoriseduser = new AuthorizationManager();
    //Googleconfigurable
    //private const string RAVE_DOMAIN = "@rave-tech.com";

    /// <summary>
    /// Defines a constant error for no records found after applying filter criteria
    /// </summary>
    private const string NO_RECORDS_FOUND_MESSAGE = "No records found.";

    #region ViewState Constants

    /// <summary>
    /// private string variable for View State
    /// </summary>
    private string SKILLDETAILS = "SkillDetails";
    private string SKILLDETAILSDELETE = "SkillDetailsDelete";
    private string DELETEROWINDEX = "DeleteRowIndex";
    private string ROWINDEX = "RowIndex";

    #endregion ViewState Constants

    #endregion Private Field Members

    #region Private Properties

    /// <summary>
    /// Gets or sets the skill details collection.
    /// </summary>
    /// <value>The skill details collection.</value>
    private BusinessEntities.RaveHRCollection SkillDetailsCollection
    {
        get
        {
            if (ViewState[SKILLDETAILS] != null)
                return ((BusinessEntities.RaveHRCollection)ViewState[SKILLDETAILS]);
            else
                return null;
        }
        set
        {
            ViewState[SKILLDETAILS] = value;
        }
    }

    #endregion Private Field Members
     
    #region Protected Events

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
                Response.Redirect(CommonConstants.INVALIDURL, false);
            }
            else
            {
                //Clearing the error label
                lblError.Text = string.Empty;
                lblMessage.Text = string.Empty;

                btnAddRow.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return ButtonClickValidate();");
                btnUpdate.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return ButtonClickValidate();");
                btnSave.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return SaveButtonClickValidate();");

                txtYearsOfExperience.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtYearsOfExperience.ClientID + "','" + imgYearsOfExperience.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
                imgYearsOfExperience.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanYearsOfExperience.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
                imgYearsOfExperience.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanYearsOfExperience.ClientID + "');");

                txtLastUsedDate.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + txtLastUsedDate.ClientID + "','','');");
                txtLastUsedDate.Attributes.Add(ReadOnly, ReadOnly);

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
                    this.GetSkillTypes();
                    this.GetSkillsByType(0);
                    this.GetProficiencyLevel();
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
                                Skillsdetails.Enabled = true;
                                btnCancel.Visible = true;
                                SkillTypeRow.Disabled = false;
                                if (gvSkills.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
                                    btnSave.Visible = true;

                            }
                            else
                            {
                                Skillsdetails.Enabled = false;
                                btnSave.Visible = false;
                                btnCancel.Visible = false;
                                SkillTypeRow.Disabled = true;

                            }
                        }
                    }
                    else
                    {
                        if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
                        {
                            if (PageMode == Common.MasterEnum.PageModeEnum.Edit.ToString())
                            {
                                Skillsdetails.Enabled = true;
                                btnCancel.Visible = true;
                                SkillTypeRow.Disabled = false;
                                if (gvSkills.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
                                    btnSave.Visible = true;

                            }
                            else
                            {
                                Skillsdetails.Enabled = false;
                                btnSave.Visible = false;
                                btnCancel.Visible = false;
                                SkillTypeRow.Disabled = true;

                            }
                        }
                        else
                        {
                            if (PageMode == Common.MasterEnum.PageModeEnum.Edit.ToString())
                            {
                                Skillsdetails.Enabled = true;
                                btnCancel.Visible = true;
                                SkillTypeRow.Disabled = true;
                                ddlSkills.Enabled = false;
                                txtLastUsedDate.Enabled = false;
                                txtYearsOfExperience.Enabled = false;
                                imgLastUsedDate.Enabled = false;

                                for (int i = 0; i < gvSkills.Rows.Count; i++)
                                {
                                    ImageButton ibtnDelete = (ImageButton)gvSkills.Rows[i].FindControl(IBTNDELETE);
                                    ibtnDelete.Enabled = false;
                                }
                                if (gvSkills.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
                                    btnSave.Visible = true;

                            }
                            else
                            {
                                Skillsdetails.Enabled = false;
                                btnSave.Visible = false;
                                btnCancel.Visible = false;
                                SkillTypeRow.Disabled = true;

                            }
                        }

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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "Page_Load", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the Click event of the btnAdd control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateControls())
            {

                BusinessEntities.SkillsDetails objSkillsDetails = new BusinessEntities.SkillsDetails();

                if (gvSkills.Rows[0].Cells[0].Text == NO_RECORDS_FOUND_MESSAGE)
                {
                    SkillDetailsCollection.Clear();
                }
                objSkillsDetails.SkillName = ddlSkills.SelectedItem.Text;
                objSkillsDetails.Experience = txtYearsOfExperience.Text;
                objSkillsDetails.ProficiencyLevel = ddlProficiencyLevel.SelectedItem.Text;
                objSkillsDetails.LastUsedDate = Convert.ToDateTime(txtLastUsedDate.Text);
                objSkillsDetails.Mode = 1;
                objSkillsDetails.Proficiency = int.Parse(ddlProficiencyLevel.SelectedValue);
                objSkillsDetails.Skill = int.Parse(ddlSkills.SelectedValue);
                objSkillsDetails.SkillType = int.Parse(ddlSkillsType.SelectedValue);

                SkillDetailsCollection.Add(objSkillsDetails);

                this.BindGrid();

                this.ClearControls();

                if (gvSkills.Rows.Count != 0)
                    btnSave.Visible = true;
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnAddRow_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
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

                    Label SkillValue = (Label)gvSkills.Rows[rowIndex].FindControl(SKILL);
                    Label ProficiencyValue = (Label)gvSkills.Rows[rowIndex].FindControl(PROFICIENCY);
                    Label SkillId = (Label)gvSkills.Rows[rowIndex].FindControl(SKILLID);
                    Label Mode = (Label)gvSkills.Rows[rowIndex].FindControl(MODE);

                    SkillValue.Text = ddlSkills.SelectedValue;
                    ProficiencyValue.Text = ddlProficiencyLevel.SelectedValue;
                    gvSkills.Rows[rowIndex].Cells[0].Text = ddlSkills.SelectedItem.Text;
                    gvSkills.Rows[rowIndex].Cells[1].Text = txtYearsOfExperience.Text;
                    gvSkills.Rows[rowIndex].Cells[2].Text = ddlProficiencyLevel.SelectedItem.Text;
                    gvSkills.Rows[rowIndex].Cells[3].Text = txtLastUsedDate.Text;

                    if (int.Parse(SkillId.Text) == 0)
                    {
                        Mode.Text = "1";
                    }
                    else
                    {
                        Mode.Text = "2";
                    }
                    ImageButton ibtnDelete = (ImageButton)gvSkills.Rows[rowIndex].FindControl(IBTNDELETE);
                    ibtnDelete.Enabled = true;
                    ViewState[ROWINDEX] = null;
                    ViewState[DELETEROWINDEX] = null;

                }

                for (int i = 0; i < SkillDetailsCollection.Count; i++)
                {
                    BusinessEntities.SkillsDetails objSkillsDetails = new BusinessEntities.SkillsDetails();
                    objSkillsDetails = (BusinessEntities.SkillsDetails)SkillDetailsCollection.Item(i);

                    Label SkillId = (Label)gvSkills.Rows[rowIndex].FindControl(SKILLID);
                    Label Mode = (Label)gvSkills.Rows[rowIndex].FindControl(MODE);

                    objSkillsDetails.SkillsId = int.Parse(SkillId.Text);
                    objSkillsDetails.EMPId = int.Parse(EMPId.Value);
                    objSkillsDetails.SkillName = gvSkills.Rows[i].Cells[0].Text;
                    objSkillsDetails.Experience = gvSkills.Rows[i].Cells[1].Text;
                    objSkillsDetails.ProficiencyLevel = gvSkills.Rows[i].Cells[2].Text;
                    objSkillsDetails.LastUsedDate = Convert.ToDateTime(gvSkills.Rows[i].Cells[3].Text);
                    objSkillsDetails.Mode = int.Parse(Mode.Text);
                }


                //Clear all the fields after inserting row into gridview
                this.ClearControls();

                btnAddRow.Visible = true;
                btnUpdate.Visible = false;
                btnCancelRow.Visible = false;

                //Enabling all the edit buttons.
                for (int i = 0; i < gvSkills.Rows.Count; i++)
                {
                    ImageButton ibtnEdit = (ImageButton)gvSkills.Rows[i].FindControl(IBTNEDIT);
                    ibtnEdit.Enabled = true;
                }
            }

        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnUpdate_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
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

    /// <summary>
    /// Handles the Click event of the btnSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Rave.HR.BusinessLayer.Employee.SkillsDetails objSkillsDetailsBAL;
        Rave.HR.BusinessLayer.Employee.Employee objEmployeeSkillsBAL;

        BusinessEntities.SkillsDetails objSkillsDetails;
        BusinessEntities.RaveHRCollection objSaveSkillsDetailsCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            objSkillsDetailsBAL = new Rave.HR.BusinessLayer.Employee.SkillsDetails();
            objEmployeeSkillsBAL = new Rave.HR.BusinessLayer.Employee.Employee();

            if (gvSkills.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
            {
                for (int i = 0; i < gvSkills.Rows.Count; i++)
                {
                    objSkillsDetails = new BusinessEntities.SkillsDetails();

                    Label SkillValue = (Label)gvSkills.Rows[i].FindControl(SKILL);
                    Label ProficiencyValue = (Label)gvSkills.Rows[i].FindControl(PROFICIENCY);
                    Label SkillId = (Label)gvSkills.Rows[i].FindControl(SKILLID);
                    Label Mode = (Label)gvSkills.Rows[i].FindControl(MODE);

                    objSkillsDetails.SkillsId = int.Parse(SkillId.Text);
                    objSkillsDetails.EMPId = int.Parse(EMPId.Value);
                    objSkillsDetails.SkillName = gvSkills.Rows[i].Cells[0].Text;
                    objSkillsDetails.Experience = gvSkills.Rows[i].Cells[1].Text;
                    objSkillsDetails.ProficiencyLevel = gvSkills.Rows[i].Cells[2].Text;
                    objSkillsDetails.LastUsedDate = Convert.ToDateTime(gvSkills.Rows[i].Cells[3].Text);
                    objSkillsDetails.Mode = int.Parse(Mode.Text);
                    objSkillsDetails.Skill = int.Parse(SkillValue.Text);
                    objSkillsDetails.Proficiency = int.Parse(ProficiencyValue.Text);
                    objSaveSkillsDetailsCollection.Add(objSkillsDetails);
                }
            }
            BusinessEntities.RaveHRCollection objDeleteSkillsDetailsCollection = (BusinessEntities.RaveHRCollection)ViewState[SKILLDETAILSDELETE];

            if (objDeleteSkillsDetailsCollection != null)
            {
                BusinessEntities.SkillsDetails obj = null;

                for (int i = 0; i < objDeleteSkillsDetailsCollection.Count; i++)
                {
                    objSkillsDetails = new BusinessEntities.SkillsDetails();
                    obj = (BusinessEntities.SkillsDetails)objDeleteSkillsDetailsCollection.Item(i);

                    objSkillsDetails.SkillsId = obj.SkillsId;
                    objSkillsDetails.EMPId = obj.EMPId;
                    objSkillsDetails.Skill = obj.Skill;
                    objSkillsDetails.Experience = obj.Experience;
                    objSkillsDetails.Proficiency = obj.Proficiency;
                    objSkillsDetails.Mode = obj.Mode;
                    objSkillsDetails.Skill = obj.Skill;
                    objSkillsDetails.Proficiency = obj.Proficiency;

                    objSaveSkillsDetailsCollection.Add(objSkillsDetails);
                }
            }
            objSkillsDetailsBAL.Manipulation(objSaveSkillsDetailsCollection);

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
            if (gvSkills.Rows.Count == 0)
                btnSave.Visible = false;

            objEmployeeSkillsBAL.SendMailApprovalOfSkillsRating(employee);

            lblMessage.Text = "Skills details saved successfully.";

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
    /// Handles the RowEditing event of the gvSkills control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewEditEventArgs"/> instance containing the event data.</param>
    protected void gvSkills_RowEditing(object sender, GridViewEditEventArgs e)  
    {
        try
        {
            int rowIndex = 0;

            Label SkillValue = (Label)gvSkills.Rows[e.NewEditIndex].FindControl(SKILL);
            Label ProficiencyValue = (Label)gvSkills.Rows[e.NewEditIndex].FindControl(PROFICIENCY);
            Label SkillType = (Label)gvSkills.Rows[e.NewEditIndex].FindControl(SKILLTYPE);

            ddlSkillsType.SelectedValue = SkillType.Text;
            this.GetSkillsByType(Convert.ToInt32(ddlSkillsType.SelectedValue));
            this.GetProficiencyLevel();

            ddlSkills.SelectedValue = SkillValue.Text;
            txtYearsOfExperience.Text = gvSkills.Rows[e.NewEditIndex].Cells[1].Text.Trim();
            ddlProficiencyLevel.SelectedValue = ProficiencyValue.Text;
            txtLastUsedDate.Text = gvSkills.Rows[e.NewEditIndex].Cells[3].Text;

            ImageButton ibtnDelete = (ImageButton)gvSkills.Rows[e.NewEditIndex].FindControl(IBTNDELETE);
            rowIndex = e.NewEditIndex;
            ViewState[ROWINDEX] = null;
            ViewState[ROWINDEX] = rowIndex;
            ibtnDelete.Enabled = false;

            btnAddRow.Visible = false;
            btnUpdate.Visible = true;
            btnCancelRow.Visible = true;

            //Disabling all the edit buttons.
            for (int i = 0; i < gvSkills.Rows.Count; i++)
            {
                ImageButton ibtnEdit = (ImageButton)gvSkills.Rows[i].FindControl(IBTNEDIT);
                if (i != rowIndex)
                    ibtnEdit.Enabled = false;
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "gvSkills_RowEditing", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the RowDeleting event of the gvSkills control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewDeleteEventArgs"/> instance containing the event data.</param>
    protected void gvSkills_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int deleteRowIndex = 0;
            int rowIndex = -1;

            BusinessEntities.SkillsDetails objSkillsDetails = new BusinessEntities.SkillsDetails();

            deleteRowIndex = e.RowIndex;

            objSkillsDetails = (BusinessEntities.SkillsDetails)SkillDetailsCollection.Item(deleteRowIndex);
            objSkillsDetails.Mode = 3;

            if (ViewState[SKILLDETAILSDELETE] != null)
            {
                BusinessEntities.RaveHRCollection objDeleteSkillDetailsCollection = (BusinessEntities.RaveHRCollection)ViewState[SKILLDETAILSDELETE];
                objDeleteSkillDetailsCollection.Add(objSkillsDetails);

                ViewState[SKILLDETAILSDELETE] = objDeleteSkillDetailsCollection;
            }
            else
            {
                BusinessEntities.RaveHRCollection objDeleteSkillDetailsCollection1 = new BusinessEntities.RaveHRCollection();

                objDeleteSkillDetailsCollection1.Add(objSkillsDetails);

                ViewState[SKILLDETAILSDELETE] = objDeleteSkillDetailsCollection1;
            }

            SkillDetailsCollection.RemoveAt(deleteRowIndex);

            ViewState[DELETEROWINDEX] = deleteRowIndex;

            BindGrid();

            if (ViewState[ROWINDEX] != null)
            {
                rowIndex = Convert.ToInt32(ViewState[ROWINDEX].ToString());

                //Check edit index with deleted index if edit index is 
                //greater than or equal to delete index then rowindex decremented.
                if (rowIndex != -1 && deleteRowIndex <= rowIndex)
                {
                    rowIndex--;
                    //store the rowindex in viewstate.
                    ViewState[ROWINDEX] = rowIndex;
                }

                ImageButton ibtnDelete = (ImageButton)gvSkills.Rows[rowIndex].FindControl(IBTNDELETE);
                ibtnDelete.Enabled = false;

                //Disabling all the edit buttons.
                for (int i = 0; i < gvSkills.Rows.Count; i++)
                {
                    if (rowIndex != i)
                    {
                        ImageButton ibtnEdit = (ImageButton)gvSkills.Rows[i].FindControl(IBTNEDIT);
                        ibtnEdit.Enabled = false;
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "gvSkills_RowDeleting", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlSkillsType control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlSkillsType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSkillsType.SelectedValue != CommonConstants.SELECT)
        {
            SkillType = Convert.ToInt32(ddlSkillsType.SelectedValue);
            this.GetSkillsByType(SkillType);

            if (gvSkills.Rows[0].Cells[0].Text == NO_RECORDS_FOUND_MESSAGE)
            {
                SkillDetailsCollection.Clear();
                ShowHeaderWhenEmptyGrid();
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
        try
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
                ImageButton ibtnDelete = (ImageButton)gvSkills.Rows[rowIndex].FindControl(IBTNDELETE);
                ibtnDelete.Enabled = true;
                ViewState[ROWINDEX] = null;
                ViewState[DELETEROWINDEX] = null;
            }

            //Enabling all the edit buttons.
            for (int i = 0; i < gvSkills.Rows.Count; i++)
            {
                ImageButton ibtnEdit = (ImageButton)gvSkills.Rows[i].FindControl(IBTNEDIT);
                ibtnEdit.Enabled = true;
            }

            btnAddRow.Visible = true;
            btnUpdate.Visible = false;
            btnCancelRow.Visible = false;
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnCancel_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the Click event of the btnNext control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Response.Redirect(CommonConstants.PAGE_PROJECTDETAILS);
    }

    /// <summary>
    /// Handles the Click event of the btnPrevious control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Response.Redirect(CommonConstants.PAGE_ORGANIZATIONDETAILS);
    }

    #endregion Protected Events

    #region Private Member Functions

    /// <summary>
    /// Populates the grid.
    /// </summary>
    private void PopulateGrid(int employeeID)
    {
        SkillDetailsCollection = this.GetSkillDetails(employeeID);

        //Binding the datatable to grid
        gvSkills.DataSource = SkillDetailsCollection;
        gvSkills.DataBind();

        //Displaying grid header with NO record found message.
        if (gvSkills.Rows.Count == 0)
            ShowHeaderWhenEmptyGrid();

        EMPId.Value = employeeID.ToString().Trim();

    }

    /// <summary>
    /// Gets the skill details.
    /// </summary>
    /// <param name="employeeID">The employee ID.</param>
    /// <returns></returns>
    private BusinessEntities.RaveHRCollection GetSkillDetails(int employeeID)
    {
        Rave.HR.BusinessLayer.Employee.SkillsDetails objSkillsDetailsBAL;
        BusinessEntities.SkillsDetails objSkillsDetails;

        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            objSkillsDetailsBAL = new Rave.HR.BusinessLayer.Employee.SkillsDetails();
            objSkillsDetails = new BusinessEntities.SkillsDetails();

            objSkillsDetails.EMPId =employeeID;

            raveHRCollection = objSkillsDetailsBAL.GetSkillsDetails(objSkillsDetails);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetSkillDetails", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

        return raveHRCollection;
    }

    /// <summary>
    /// Does the data bind.
    /// </summary>
    private void BindGrid()
    {
        try
        {
            gvSkills.DataSource = SkillDetailsCollection;
            gvSkills.DataBind();

            //Displaying grid header with NO record found message.
            if (gvSkills.Rows.Count == 0)
                ShowHeaderWhenEmptyGrid();
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "BindGrid", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Clears the controls.
    /// </summary>
    private void ClearControls()
    {
        ddlSkills.SelectedIndex = 0;
        ddlProficiencyLevel.SelectedIndex = 0;
        txtYearsOfExperience.Text = string.Empty;
        txtLastUsedDate.Text = string.Empty;
    }

    /// <summary>
    /// Gets the skill types.
    /// </summary>
    private void GetSkillTypes()
    {
        ddlSkillsType.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
        
        ddlSkillsType.Items.Insert(1, new ListItem(Common.EnumsConstants.Category.Databases.ToString(),
                                                Convert.ToString((int)Common.EnumsConstants.Category.Databases)));
        ddlSkillsType.Items.Insert(2, new ListItem(Common.EnumsConstants.Category.InternetTechnologies.ToString(),
                                                Convert.ToString((int)Common.EnumsConstants.Category.InternetTechnologies)));
        ddlSkillsType.Items.Insert(3, new ListItem(Common.EnumsConstants.Category.Languages.ToString(),
                                                Convert.ToString((int)Common.EnumsConstants.Category.Languages)));
        ddlSkillsType.Items.Insert(4, new ListItem(Common.EnumsConstants.Category.OperatingSystems.ToString(),
                                                    Convert.ToString((int)Common.EnumsConstants.Category.OperatingSystems)));
        ddlSkillsType.Items.Insert(5, new ListItem(Common.EnumsConstants.Category.Others.ToString(),
                                                Convert.ToString((int)Common.EnumsConstants.Category.Others)));
    }

    /// <summary>
    /// Gets the type of the skills.
    /// </summary>
    /// <param name="SkillType">Type of the skill.</param>
    private void GetSkillsByType(int SkillType)
    {
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        raveHrColl = master.FillDropDownsBL(SkillType);

        ddlSkills.DataSource = raveHrColl;
        ddlSkills.DataTextField = Common.CommonConstants.DDL_DataTextField;
        ddlSkills.DataValueField = Common.CommonConstants.DDL_DataValueField;
        ddlSkills.DataBind();
        ddlSkills.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

    }

    /// <summary>
    /// Gets the proficiency level.
    /// </summary>
    private void GetProficiencyLevel()
    {
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        raveHrColl = master.FillDropDownsBL((int)Common.EnumsConstants.Category.ProficiencyLevel);

        ddlProficiencyLevel.DataSource = raveHrColl;
        ddlProficiencyLevel.DataTextField = Common.CommonConstants.DDL_DataTextField;
        ddlProficiencyLevel.DataValueField = Common.CommonConstants.DDL_DataValueField;
        ddlProficiencyLevel.DataBind();
        ddlProficiencyLevel.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

    }

    /// <summary>
    /// Validates the controls.
    /// </summary>
    /// <returns></returns>
    private bool ValidateControls()
    {
        StringBuilder errMessage = new StringBuilder();
        bool flag = true;
        string[] LastUsedDateArr;

        LastUsedDateArr = Convert.ToString(txtLastUsedDate.Text).Split(SPILITER_SLASH);
        DateTime LastUsedDate = new DateTime(Convert.ToInt32(LastUsedDateArr[2]), Convert.ToInt32(LastUsedDateArr[1]), Convert.ToInt32(LastUsedDateArr[0]));

        if (LastUsedDate > DateTime.Now.Date)
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.LAST_USED_DATE_VALIDATION);
            flag = false;
        }

        lblError.Text = errMessage.ToString();
        return flag;

    }

    /// <summary>
    /// Get message body.
    /// </summary>
    private string GetMessageBody(string strToUser, string strFromUser, string projectName, string strApproveRejectLink,string Fullname)
    {
        HtmlForm htmlFormBody = new HtmlForm();
        try
        {
            StringBuilder strMessageBody = new StringBuilder();

            //Googleconfigurable
            string strUser = "";
            AuthorizationManager obj = new AuthorizationManager();
            strUser = obj.GetUsernameBasedOnEmail(strFromUser);
            //if (strFromUser.ToLower().Trim().Contains("@rave-tech.com"))
            //{
            //    strUser = strFromUser.ToLower().Replace("@rave-tech.com", "");
            //}
            //else
            //{
            //    strUser = strFromUser.ToLower().Replace("@" + AuthorizationManagerConstants.NORTHGATEDOMAINEMAIL, "");
            //}

            strMessageBody.Append("Hello," + "</br>"
                + "This is to bring to your notice that " + Fullname + "</br>" +
                "has modified certain details in the “Resource Management” system," + "</br>" +
                "His skills details have also been modified, kindly approve his proficiency level." + "</br>" + "</br>" +
                "Please click on the link below to approve the skills set" + "</br>" +
                "</br><a href=" + strApproveRejectLink + ">" + strApproveRejectLink + "</a></br>" + "" + "If you face problem opening this link, please copy and paste the given URL into your browser." +
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
    /// Display Header When GridView Empty with proper message
    /// </summary>
    /// <param name="objListSort">EmptyList</param>
    private void ShowHeaderWhenEmptyGrid()
    {
        try
        {
            //set header visible
            gvSkills.ShowHeader = true;

            //Create empty datasource for Grid view and bind
            SkillDetailsCollection.Add(new BusinessEntities.SkillsDetails());
            gvSkills.DataSource = SkillDetailsCollection;
            gvSkills.DataBind();

            //Calculate number of columns in Grid view used for column Span
            int columnsCount = gvSkills.Columns.Count;

            //clear all the cells in the row
            gvSkills.Rows[0].Cells.Clear();

            //add a new blank cell
            gvSkills.Rows[0].Cells.Add(new TableCell());
            gvSkills.Rows[0].Cells[0].Text = NO_RECORDS_FOUND_MESSAGE;
            gvSkills.Rows[0].Cells[0].Wrap = false;
            gvSkills.Rows[0].Cells[0].Width = Unit.Percentage(10);

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
