//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           EmpProjectDetails.aspx.cs    
//  Author:         Shrinivas.Dalavi
//  Date written:   9/7/2009/ 12:01:00 PM
//  Description:    This Page will be the entry page for adding Project details,
//                  same page will use in View Project details & Edit Project details mode
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  9/7/2009/ 12:01:00 PM  Shrinivas.Dalavi    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Text;
using System.Web.UI.WebControls;
using Common;
using Common.AuthorizationManager;
using System.Collections;

public partial class EmpProjectDetails : BaseClass
{
    #region Private Field Members

    #region ViewState Constants

    private string PROJECTDETAILS = "ProjectDetails";
    private string PROJECTDETAILSDELETE = "ProjectDetailsDelete";
    private string DELETEROWINDEX = "DeleteRowIndex";
    private string ROWINDEX = "RowIndex";

    #endregion ViewState Constants

    private string IMGBTNDELETE = "imgBtnDelete";
    private string IMGBTNEDIT = "imgBtnEdit";
    private string CLASS_NAME = "EmpProjectDetails.aspx";
    private string PROJECTID = "ProjectId";
    private string MODE = "Mode";
    private string ProjectLocationId = "LocationId";
    private string ProjectDoneStatus = "ProjectDoneStatus";
    private string CompanyName = "Rave Technologies";
    private string RaveProjects = "Projects In Rave";
    Rave.HR.BusinessLayer.Employee.ProjectDetails ObjProjectDetailsBL = new Rave.HR.BusinessLayer.Employee.ProjectDetails();
    private int employeeID = 0;
    private BusinessEntities.Employee employee;
    string UserRaveDomainId;
    string UserMailId;

    /// <summary>
    /// Defines a constant error for no records found after applying filter criteria
    /// </summary>
    private const string NO_RECORDS_FOUND_MESSAGE = "No records found.";

    AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
    // Get logged-in user's email id
    AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
    ArrayList arrRolesForUser = new ArrayList();

    #endregion Private Field Members

    #region Local Properties

    /// <summary>
    /// Gets or sets the project details collection.
    /// </summary>
    /// <value>The project details collection.</value>
    private BusinessEntities.RaveHRCollection ProjectDetailsCollection
    {
        get
        {
            if (ViewState[PROJECTDETAILS] != null)
                return ((BusinessEntities.RaveHRCollection)ViewState[PROJECTDETAILS]);
            else
                return null;
        }
        set
        {
            ViewState[PROJECTDETAILS] = value;
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
        btnUpdate.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return ButtonClickValidate();");
        btnSave.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return SaveButtonClickValidate();");

        ddlLocation.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + spanzLocation.ClientID + "','','');");
        imgLocation.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanLocation.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgLocation.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanLocation.ClientID + "');");

        txtCompanyName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtCompanyName.ClientID + "','" + imgCompanyName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPECIALCHAR + "');");
        imgCompanyName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanCompanyName.ClientID + "','" + Common.CommonConstants.MSG_ALPHABET_SPECIALCHAR + "');");
        imgCompanyName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanCompanyName.ClientID + "');");

        txtProjectName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtProjectName.ClientID + "','" + imgProjectName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPECIALCHAR + "');");
        imgProjectName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanProjectName.ClientID + "','" + Common.CommonConstants.MSG_ALPHABET_SPECIALCHAR + "');");
        imgProjectName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanProjectName.ClientID + "');");

        txtClientName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtClientName.ClientID + "','" + imgClientName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPECIALCHAR + "');");
        imgClientName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanClientName.ClientID + "','" + Common.CommonConstants.MSG_ALPHABET_SPECIALCHAR + "');");
        imgClientName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanClientName.ClientID + "');");

        txtDuration.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtDuration.ClientID + "','" + imgDuration.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
        imgDuration.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanDuration.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgDuration.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanDuration.ClientID + "');");

        txtProjectSiZe.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtProjectSiZe.ClientID + "','" + imgProjectSize.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
        imgProjectSize.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanProjectSize.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgProjectSize.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanProjectSize.ClientID + "');");

        txtProjectDescription.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return MultiLineTextBox('" + txtProjectDescription.ClientID + "','" + txtProjectDescription.MaxLength + "','" + imgProjectDescription.ClientID + "');");
        imgProjectDescription.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanProjectDescription.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgProjectDescription.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanProjectDescription.ClientID + "');");

        txtRole.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtRole.ClientID + "','" + imgRole.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPACE + "');");
        imgRole.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanRole.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
        imgRole.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanRole.ClientID + "');");

        txtResponsibility.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return MultiLineTextBox('" + txtResponsibility.ClientID + "','" + txtResponsibility.MaxLength + "','" + imgResponsibility.ClientID + "');");
        imgResponsibility.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanResponsibility.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgResponsibility.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanResponsibility.ClientID + "');");

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
                this.GetLocation();
                this.GetBifurcation();
            }

            if (gvProjectDetails.Rows[0].Cells[0].Text == NO_RECORDS_FOUND_MESSAGE)
            {
                ProjectDetailsCollection.Clear();
                ShowHeaderWhenEmptyGrid();
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
                            Projectdetails.Enabled = true;
                            btnCancel.Visible = true;
                            if (gvProjectDetails.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
                                btnSave.Visible = true;
                        }
                        else
                        {
                            Projectdetails.Enabled = false;
                            btnSave.Visible = false;
                            btnCancel.Visible = false;

                        }
                    }
                }
                else
                {
                    if (PageMode == Common.MasterEnum.PageModeEnum.Edit.ToString() && arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
                    {
                        Projectdetails.Enabled = true;
                        btnCancel.Visible = true;
                        if (gvProjectDetails.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
                            btnSave.Visible = true;
                    }
                    else
                    {
                        Projectdetails.Enabled = false;
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
        BusinessEntities.ProjectDetails objProjectDetails = new BusinessEntities.ProjectDetails();

        if (gvProjectDetails.Rows[0].Cells[0].Text == NO_RECORDS_FOUND_MESSAGE)
        {
            ProjectDetailsCollection.Clear();
        }
        objProjectDetails.ProjectName = txtProjectName.Text;
        objProjectDetails.Organisation = txtCompanyName.Text;
        objProjectDetails.Role = txtRole.Text;
        objProjectDetails.ProjectLocation = ddlLocation.SelectedItem.Text;
        objProjectDetails.OnsiteDuration = txtDuration.Text;
        objProjectDetails.Description = txtProjectDescription.Text;
        objProjectDetails.LocationId = Convert.ToInt32(ddlLocation.SelectedValue);
        objProjectDetails.ClientName = txtClientName.Text;
        objProjectDetails.ProjectSize = Convert.ToInt32(txtProjectSiZe.Text);
        objProjectDetails.Resposibility = txtResponsibility.Text;
        objProjectDetails.ProjectDoneName = ddlBifurcation.SelectedItem.Text;
        objProjectDetails.ProjectDone = Convert.ToInt32(ddlBifurcation.SelectedValue);

        objProjectDetails.Mode = 1;

        ProjectDetailsCollection.Add(objProjectDetails);

        this.DoDataBind();

        this.ClearControls();

        if (gvProjectDetails.Rows.Count != 0)
            btnSave.Visible = true;
    }

    /// <summary>
    /// Handles the Click event of the btnSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Rave.HR.BusinessLayer.Employee.ProjectDetails objProjectDetailsBAL;

        BusinessEntities.ProjectDetails objProjectDetails;
        BusinessEntities.RaveHRCollection objSaveProjectDetailsCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            objProjectDetailsBAL = new Rave.HR.BusinessLayer.Employee.ProjectDetails();

            if (gvProjectDetails.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
            {
                for (int i = 0; i < gvProjectDetails.Rows.Count; i++)
                {
                    objProjectDetails = new BusinessEntities.ProjectDetails();
                    Label ProjectId = (Label)gvProjectDetails.Rows[i].FindControl(PROJECTID);
                    Label Mode = (Label)gvProjectDetails.Rows[i].FindControl(MODE);

                    objProjectDetails.ProjectId = int.Parse(ProjectId.Text);
                    objProjectDetails.EMPId = int.Parse(EMPId.Value);
                    objProjectDetails.Organisation = gvProjectDetails.Rows[i].Cells[2].Text;
                    objProjectDetails.ProjectName = gvProjectDetails.Rows[i].Cells[1].Text;
                    objProjectDetails.ProjectLocation = gvProjectDetails.Rows[i].Cells[3].Text;
                    objProjectDetails.ClientName = gvProjectDetails.Rows[i].Cells[4].Text;
                    objProjectDetails.ProjectSize = int.Parse(gvProjectDetails.Rows[i].Cells[5].Text);
                    objProjectDetails.OnsiteDuration = gvProjectDetails.Rows[i].Cells[6].Text;
                    objProjectDetails.Role = gvProjectDetails.Rows[i].Cells[7].Text;
                    objProjectDetails.Description = gvProjectDetails.Rows[i].Cells[8].Text;
                    objProjectDetails.Resposibility = gvProjectDetails.Rows[i].Cells[9].Text;
                    objProjectDetails.Mode = int.Parse(Mode.Text);

                    Label LocationId = (Label)gvProjectDetails.Rows[i].FindControl(ProjectLocationId);
                    objProjectDetails.LocationId = Convert.ToInt32(LocationId.Text);

                    Label ProjectDoneID = (Label)gvProjectDetails.Rows[i].FindControl(ProjectDoneStatus);
                    objProjectDetails.ProjectDone = Convert.ToInt32(ProjectDoneID.Text);

                    objSaveProjectDetailsCollection.Add(objProjectDetails);
                }
            }
            BusinessEntities.RaveHRCollection objDeleteProjectDetailsCollection = (BusinessEntities.RaveHRCollection)ViewState[PROJECTDETAILSDELETE];

            if (objDeleteProjectDetailsCollection != null)
            {
                BusinessEntities.ProjectDetails obj = null;

                for (int i = 0; i < objDeleteProjectDetailsCollection.Count; i++)
                {
                    objProjectDetails = new BusinessEntities.ProjectDetails();
                    obj = (BusinessEntities.ProjectDetails)objDeleteProjectDetailsCollection.Item(i);

                    objProjectDetails.ProjectId = obj.ProjectId;
                    objProjectDetails.EMPId = obj.EMPId;
                    objProjectDetails.ProjectName = obj.ProjectName;
                    objProjectDetails.Organisation = obj.Organisation;
                    objProjectDetails.ClientName = obj.ClientName;
                    objProjectDetails.ProjectSize = obj.ProjectSize;
                    objProjectDetails.Role = obj.Role;
                    objProjectDetails.ProjectLocation = obj.ProjectLocation;
                    objProjectDetails.OnsiteDuration = obj.OnsiteDuration;
                    objProjectDetails.Description = obj.Description;
                    objProjectDetails.Resposibility = obj.Resposibility;
                    objProjectDetails.ProjectSize = obj.ProjectSize;
                    objProjectDetails.Mode = obj.Mode;
                    objProjectDetails.LocationId = objProjectDetails.LocationId;
                    objProjectDetails.ProjectDone = obj.ProjectDone;
                    objProjectDetails.ProjectDoneName = obj.ProjectDoneName;
                    objSaveProjectDetailsCollection.Add(objProjectDetails);
                }

            }
            objProjectDetailsBAL.Manipulation(objSaveProjectDetailsCollection);

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

            if (gvProjectDetails.Rows.Count == 0)
                btnSave.Visible = false;

            lblMessage.Text = "Project details saved successfully.";

        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnSave_Click", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// Handles the RowDeleting event of the gvProjectDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewDeleteEventArgs"/> instance containing the event data.</param>
    protected void gvProjectDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int deleteRowIndex = 0;
        int rowIndex = -1;

        BusinessEntities.ProjectDetails objProjectDetails = new BusinessEntities.ProjectDetails();

        deleteRowIndex = e.RowIndex;

        objProjectDetails = (BusinessEntities.ProjectDetails)ProjectDetailsCollection.Item(deleteRowIndex);
        objProjectDetails.Mode = 3;

        if (ViewState[PROJECTDETAILSDELETE] != null)
        {
            BusinessEntities.RaveHRCollection objDeleteProjectDetailsCollection = (BusinessEntities.RaveHRCollection)ViewState[PROJECTDETAILSDELETE];
            objDeleteProjectDetailsCollection.Add(objProjectDetails);

            ViewState[PROJECTDETAILSDELETE] = objDeleteProjectDetailsCollection;
        }
        else
        {
            BusinessEntities.RaveHRCollection objDeleteProjectDetailsCollection1 = new BusinessEntities.RaveHRCollection();

            objDeleteProjectDetailsCollection1.Add(objProjectDetails);

            ViewState[PROJECTDETAILSDELETE] = objDeleteProjectDetailsCollection1;
        }

        ProjectDetailsCollection.RemoveAt(deleteRowIndex);

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

            ImageButton btnImg = (ImageButton)gvProjectDetails.Rows[rowIndex].FindControl(IMGBTNDELETE);
            btnImg.Enabled = false;

            //Disabling all the edit buttons.
            for (int i = 0; i < gvProjectDetails.Rows.Count; i++)
            {
                if (rowIndex != i)
                {
                    ImageButton btnImgEdit = (ImageButton)gvProjectDetails.Rows[i].FindControl(IMGBTNEDIT);
                    btnImgEdit.Enabled = false;
                }
            }
        }

    }

    /// <summary>
    /// Handles the RowEditing event of the gvProjectDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewEditEventArgs"/> instance containing the event data.</param>
    protected void gvProjectDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int rowIndex = 0;

        txtProjectName.Text = gvProjectDetails.Rows[e.NewEditIndex].Cells[1].Text.Trim();
        txtCompanyName.Text = gvProjectDetails.Rows[e.NewEditIndex].Cells[2].Text.Trim();
        txtClientName.Text = gvProjectDetails.Rows[e.NewEditIndex].Cells[4].Text.Trim();
        txtProjectSiZe.Text = gvProjectDetails.Rows[e.NewEditIndex].Cells[5].Text.Trim();
        txtDuration.Text = gvProjectDetails.Rows[e.NewEditIndex].Cells[6].Text.Trim();
        txtRole.Text = gvProjectDetails.Rows[e.NewEditIndex].Cells[7].Text.Trim();
        txtProjectDescription.Text = gvProjectDetails.Rows[e.NewEditIndex].Cells[8].Text.Trim();
        txtResponsibility.Text = gvProjectDetails.Rows[e.NewEditIndex].Cells[9].Text.Trim();

        Label LocationId = (Label)gvProjectDetails.Rows[e.NewEditIndex].FindControl(ProjectLocationId);
        ddlLocation.SelectedValue = LocationId.Text;

        Label ProjectDoneID = (Label)gvProjectDetails.Rows[e.NewEditIndex].FindControl(ProjectDoneStatus);
        ddlBifurcation.SelectedValue = ProjectDoneID.Text;

        ImageButton btnImg = (ImageButton)gvProjectDetails.Rows[e.NewEditIndex].FindControl(IMGBTNDELETE);
        rowIndex = e.NewEditIndex;
        ViewState[ROWINDEX] = null;
        ViewState[ROWINDEX] = rowIndex;
        btnImg.Enabled = false;

        btnAddRow.Visible = false;
        btnUpdate.Visible = true;
        btnCancelRow.Visible = true;

        //Disabling all the edit buttons.
        for (int i = 0; i < gvProjectDetails.Rows.Count; i++)
        {
            ImageButton btnImgEdit = (ImageButton)gvProjectDetails.Rows[i].FindControl(IMGBTNEDIT);
            if (i != rowIndex)
                btnImgEdit.Enabled = false;
        }

    }

    /// <summary>
    /// Handles the Click event of the btnUpdate control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnUpdate_Click(object sender, EventArgs e)
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

            Label ProjectId = (Label)gvProjectDetails.Rows[rowIndex].FindControl(PROJECTID);
            Label Mode = (Label)gvProjectDetails.Rows[rowIndex].FindControl(MODE);

            gvProjectDetails.Rows[rowIndex].Cells[0].Text = ddlBifurcation.SelectedItem.Text;
            gvProjectDetails.Rows[rowIndex].Cells[1].Text = txtProjectName.Text;
            gvProjectDetails.Rows[rowIndex].Cells[2].Text = txtCompanyName.Text;
            gvProjectDetails.Rows[rowIndex].Cells[3].Text = ddlLocation.SelectedItem.Text;
            gvProjectDetails.Rows[rowIndex].Cells[4].Text = txtClientName.Text;
            gvProjectDetails.Rows[rowIndex].Cells[5].Text = txtProjectSiZe.Text;
            gvProjectDetails.Rows[rowIndex].Cells[6].Text = txtDuration.Text;
            gvProjectDetails.Rows[rowIndex].Cells[7].Text = txtRole.Text;
            gvProjectDetails.Rows[rowIndex].Cells[8].Text = txtProjectDescription.Text;
            gvProjectDetails.Rows[rowIndex].Cells[9].Text = txtResponsibility.Text;


            if (int.Parse(ProjectId.Text) == 0)
            {
                Mode.Text = "1";
            }
            else
            {
                Mode.Text = "2";
            }

            Label LocationId = (Label)gvProjectDetails.Rows[rowIndex].FindControl(ProjectLocationId);
            LocationId.Text = ddlLocation.SelectedValue;

            Label ProjectDoneID = (Label)gvProjectDetails.Rows[rowIndex].FindControl(ProjectDoneStatus);
            ProjectDoneID.Text = ddlBifurcation.SelectedValue;

            ImageButton btnImg = (ImageButton)gvProjectDetails.Rows[rowIndex].FindControl(IMGBTNDELETE);
            btnImg.Enabled = true;
            ViewState[ROWINDEX] = null;
            ViewState[DELETEROWINDEX] = null;

        }

        for (int i = 0; i < ProjectDetailsCollection.Count; i++)
        {
            BusinessEntities.ProjectDetails objProjectDetails = new BusinessEntities.ProjectDetails();
            objProjectDetails = (BusinessEntities.ProjectDetails)ProjectDetailsCollection.Item(i);

            Label ProjectId = (Label)gvProjectDetails.Rows[i].FindControl(PROJECTID);
            Label Mode = (Label)gvProjectDetails.Rows[rowIndex].FindControl(MODE);

            objProjectDetails.ProjectId = int.Parse(ProjectId.Text);
            objProjectDetails.EMPId = int.Parse(EMPId.Value);
            objProjectDetails.ProjectDoneName = gvProjectDetails.Rows[i].Cells[0].Text;
            objProjectDetails.ProjectName = gvProjectDetails.Rows[i].Cells[1].Text;
            objProjectDetails.Organisation = gvProjectDetails.Rows[i].Cells[2].Text;
            objProjectDetails.ProjectLocation = gvProjectDetails.Rows[i].Cells[3].Text;
            objProjectDetails.ClientName = gvProjectDetails.Rows[i].Cells[4].Text;
            objProjectDetails.ProjectSize = Convert.ToInt32(gvProjectDetails.Rows[i].Cells[5].Text);
            objProjectDetails.OnsiteDuration = gvProjectDetails.Rows[i].Cells[6].Text;
            objProjectDetails.Role = gvProjectDetails.Rows[i].Cells[7].Text;
            objProjectDetails.Description = gvProjectDetails.Rows[i].Cells[8].Text;
            objProjectDetails.Resposibility = gvProjectDetails.Rows[i].Cells[9].Text;
            objProjectDetails.Mode = int.Parse(Mode.Text);
        }

        //Clear all the fields after inserting row into gridview
        this.ClearControls();

        btnAddRow.Visible = true;
        btnUpdate.Visible = false;
        btnCancelRow.Visible = false;

        //Enabling all the edit buttons.
        for (int i = 0; i < gvProjectDetails.Rows.Count; i++)
        {
            ImageButton btnImgEdit = (ImageButton)gvProjectDetails.Rows[i].FindControl(IMGBTNEDIT);
            btnImgEdit.Enabled = true;
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
            ImageButton btnImg = (ImageButton)gvProjectDetails.Rows[rowIndex].FindControl(IMGBTNDELETE);
            btnImg.Enabled = true;
            ViewState[ROWINDEX] = null;
            ViewState[DELETEROWINDEX] = null;
        }

        //Enabling all the edit buttons.
        for (int i = 0; i < gvProjectDetails.Rows.Count; i++)
        {
            ImageButton btnImgEdit = (ImageButton)gvProjectDetails.Rows[i].FindControl(IMGBTNEDIT);
            btnImgEdit.Enabled = true;
        }

        btnAddRow.Visible = true;
        btnUpdate.Visible = false;
        btnCancelRow.Visible = false;
    }

    /// <summary>
    /// Handles the Click event of the btnNext control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Response.Redirect(CommonConstants.PAGE_OTHERDETAILS);
    }

    /// <summary>
    /// Handles the Click event of the btnPrevious control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Response.Redirect(CommonConstants.PAGE_SKILLSDETAILS);
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlBifurcation control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlBifurcation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBifurcation.SelectedItem.Text == RaveProjects)
        {
            txtCompanyName.Enabled = false;
            txtCompanyName.Text = CompanyName;
        }
        else
        {
            txtCompanyName.Enabled = true;
            txtCompanyName.Text = string.Empty;
        }
    }

    #endregion

    #region Private Member Functions

    /// <summary>
    /// Does the data bind.
    /// </summary>
    private void DoDataBind()
    {
        gvProjectDetails.DataSource = ProjectDetailsCollection;
        gvProjectDetails.DataBind();

        //Displaying grid header with NO record found message.
        if (gvProjectDetails.Rows.Count == 0)
            ShowHeaderWhenEmptyGrid();

    }

    /// <summary>
    /// Clears the controls.
    /// </summary>
    private void ClearControls()
    {
        txtProjectName.Text = string.Empty;
        txtCompanyName.Text = string.Empty;
        txtProjectDescription.Text = string.Empty;
        txtRole.Text = string.Empty;
        txtDuration.Text = string.Empty;
        ddlLocation.SelectedIndex = 0;
        txtClientName.Text = string.Empty;
        txtResponsibility.Text = string.Empty;
        txtProjectSiZe.Text = string.Empty;
        ddlBifurcation.SelectedIndex = 0;

    }

    /// <summary>
    /// Populates the grid.
    /// </summary>
    private void PopulateGrid(int employeeID)
    {
        ProjectDetailsCollection = this.GetProjectDetails(employeeID);

        //Binding the datatable to grid
        gvProjectDetails.DataSource = ProjectDetailsCollection;
        gvProjectDetails.DataBind();

        //Displaying grid header with NO record found message.
        if (gvProjectDetails.Rows.Count == 0)
            ShowHeaderWhenEmptyGrid();

        EMPId.Value = employeeID.ToString().Trim();
    }

    /// <summary>
    /// Gets the project details.
    /// </summary>
    /// <returns></returns>
    private BusinessEntities.RaveHRCollection GetProjectDetails(int employeeID)
    {
        Rave.HR.BusinessLayer.Employee.ProjectDetails objProjectDetailsBAL;
        BusinessEntities.ProjectDetails objProjectDetails;

        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            objProjectDetailsBAL = new Rave.HR.BusinessLayer.Employee.ProjectDetails();
            objProjectDetails = new BusinessEntities.ProjectDetails();

            objProjectDetails.EMPId = employeeID;

            raveHRCollection = objProjectDetailsBAL.GetProjectDetails(objProjectDetails);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetProjectDetails", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

        return raveHRCollection;
    }

    /// <summary>
    /// Gets the location.
    /// </summary>
    private void GetLocation()
    {
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        raveHrColl = master.FillDropDownsBL((int)Common.EnumsConstants.Category.ResourceLocation);

        ddlLocation.DataSource = raveHrColl;
        ddlLocation.DataTextField = Common.CommonConstants.DDL_DataTextField;
        ddlLocation.DataValueField = Common.CommonConstants.DDL_DataValueField;
        ddlLocation.DataBind();
        ddlLocation.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

    }

    /// <summary>
    /// Validates the controls.
    /// </summary>
    /// <returns></returns>
    private bool ValidateControls()
    {
        StringBuilder errMessage = new StringBuilder();
        bool flag = true;

        if (string.IsNullOrEmpty(txtCompanyName.Text.Trim()))
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.SPACES_VALIDATION);
            flag = false;
        }

        if (string.IsNullOrEmpty(txtProjectName.Text.Trim()))
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.SPACES_VALIDATION);
            flag = false;
        }

        if (string.IsNullOrEmpty(txtClientName.Text.Trim()))
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.SPACES_VALIDATION);
            flag = false;
        }

        lblError.Text = errMessage.ToString();
        return flag;
    }

    /// <summary>
    /// Gets the bifurcation.
    /// </summary>
    private void GetBifurcation()
    {
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        raveHrColl = master.FillDropDownsBL((int)Common.EnumsConstants.Category.ProjectDoneStatus);

        ddlBifurcation.DataSource = raveHrColl;
        ddlBifurcation.DataTextField = Common.CommonConstants.DDL_DataTextField;
        ddlBifurcation.DataValueField = Common.CommonConstants.DDL_DataValueField;
        ddlBifurcation.DataBind();
        ddlBifurcation.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

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
            gvProjectDetails.ShowHeader = true;

            //Create empty datasource for Grid view and bind
            ProjectDetailsCollection.Add(new BusinessEntities.ProjectDetails());
            gvProjectDetails.DataSource = ProjectDetailsCollection;
            gvProjectDetails.DataBind();

            //Calculate number of columns in Grid view used for column Span
            int columnsCount = gvProjectDetails.Columns.Count;

            //clear all the cells in the row
            gvProjectDetails.Rows[0].Cells.Clear();

            //add a new blank cell
            gvProjectDetails.Rows[0].Cells.Add(new TableCell());
            gvProjectDetails.Rows[0].Cells[0].Text = NO_RECORDS_FOUND_MESSAGE;
            gvProjectDetails.Rows[0].Cells[0].Wrap = false;
            gvProjectDetails.Rows[0].Cells[0].Width = Unit.Percentage(10);

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
