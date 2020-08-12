using System;
using System.Collections;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Common.AuthorizationManager;
using System.Text;
using System.Collections.Generic;

public partial class EmployeeProjectDetails : BaseClass
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
    private const string hdfRankOrder = "hdfRankOrder";
    string PageMode = string.Empty;
    const string PAGETYPE = "pagetype";
    const string PAGETYPEEMPLOYEESUMMERY = "EMPLOYEESUMMERY";

    /// <summary>
    /// Defines a constant error for no records found after applying filter criteria
    /// </summary>
    private const string NO_RECORDS_FOUND_MESSAGE = "No records found.";

    AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
    // Get logged-in user's email id
    AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
    ArrayList arrRolesForUser = new ArrayList();

    protected EmployeeMenuUC BubbleControl;

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
        btnUpdate.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return ButtonClickValidate();");

        ddlLocation.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + spanzLocation.ClientID + "','','');");
        imgLocation.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanLocation.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgLocation.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanLocation.ClientID + "');");

        txtCompanyName.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return ValidateSpecialCharacters()");
        txtCompanyName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtCompanyName.ClientID + "','" + imgCompanyName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPECIALCHAR + "');");
        imgCompanyName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanCompanyName.ClientID + "','" + Common.CommonConstants.MSG_ALPHABET_SPECIALCHAR + "');");
        imgCompanyName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanCompanyName.ClientID + "');");

        txtProjectName.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return ValidateSpecialCharacters()");
        txtProjectName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtProjectName.ClientID + "','" + imgProjectName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPECIALCHAR + "');");
        imgProjectName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanProjectName.ClientID + "','" + Common.CommonConstants.MSG_ALPHABET_SPECIALCHAR + "');");
        imgProjectName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanProjectName.ClientID + "');");

        txtClientName.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return ValidateSpecialCharacters()");
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

        if (Session[Common.SessionNames.EMPLOYEEDETAILS] != null)
        {
            employee = (BusinessEntities.Employee)Session[Common.SessionNames.EMPLOYEEDETAILS];
        }
        else
        {
            Response.Redirect("EmployeeDetails.aspx");
        }

        if (employee != null)
        {
            employeeID = employee.EMPId;
            lblempName.Text = employee.FirstName.ToUpper() + " " + employee.LastName.ToUpper();
        }

        if (!IsPostBack)
        {
            Session[SessionNames.PAGEMODE] = Common.MasterEnum.PageModeEnum.View;
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

        arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());

        SavedControlVirtualPath = "~/EmployeeMenuUC.ascx";
        ReloadControl();

        if (UserMailId.ToLower() == employee.EmailId.ToLower() || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
        {
            if (Session[SessionNames.PAGEMODE] != null)
            {
                PageMode = Session[SessionNames.PAGEMODE].ToString();

                if (PageMode == Common.MasterEnum.PageModeEnum.View.ToString() && IsPostBack == false && gvProjectDetails.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
                {
                    Projectdetails.Enabled = false;
                    btnEdit.Visible = true;
                    btnEditCancel.Visible = true;
                    btnCancel.Visible = false;
                    btnAddRow.Visible = false;
                }
            }
        }
        else
        {
            Projectdetails.Enabled = false;
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
        Rave.HR.BusinessLayer.Employee.ProjectDetails objProjectDetailsBAL;

        if (ValidateControls())
        {
            objProjectDetailsBAL = new Rave.HR.BusinessLayer.Employee.ProjectDetails();

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
            objProjectDetails.EMPId = int.Parse(EMPId.Value);

            objProjectDetails.Mode = 1;

            ProjectDetailsCollection.Add(objProjectDetails);

            objProjectDetailsBAL.AddProjectDetails(objProjectDetails);

            this.PopulateGrid(objProjectDetails.EMPId);

            this.ClearControls();

            HfIsDataModified.Value = CommonConstants.YES;

            lblMessage.Text = "Project details saved successfully.";
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
        Boolean Flag = false;
        Rave.HR.BusinessLayer.Employee.ProjectDetails objProjectDetailsBAL;
        BusinessEntities.ProjectDetails objProjectDetails = new BusinessEntities.ProjectDetails();
        objProjectDetailsBAL = new Rave.HR.BusinessLayer.Employee.ProjectDetails();

        deleteRowIndex = e.RowIndex;

        objProjectDetails = (BusinessEntities.ProjectDetails)ProjectDetailsCollection.Item(deleteRowIndex);
        if (objProjectDetails.Mode == 1)
            Flag = true;
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

        objProjectDetailsBAL.DeleteProjectDetails(objProjectDetails);


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

        lblMessage.Text = "Project details deleted successfully.";
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

        //Mohamed : Issue 41355 : 06/03/2013 : Starts
        //Desc :  when an employee clicks on project details and try to edit the same, the responsibility upper text box is shown blank though the responsibilities exist in below textbox

        //txtProjectDescription.Text = gvProjectDetails.Rows[e.NewEditIndex].Cells[8].Text.Trim();
        //txtResponsibility.Text = gvProjectDetails.Rows[e.NewEditIndex].Cells[9].Text.Trim();
        TextBox TxtProjDesc = (TextBox)gvProjectDetails.Rows[e.NewEditIndex].Cells[8].FindControl("ProjectDescription"); //.Text.Trim();//
        txtProjectDescription.Text = TxtProjDesc.Text;
        txtResponsibility.Text = gvProjectDetails.Rows[e.NewEditIndex].Cells[10].Text.Trim();

        //Mohamed : Issue 41355 : 06/03/2013 : Ends

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
        btnCancel.Visible = false;
        btnUpdate.Visible = true;
        btnCancelRow.Visible = true;

        //Disabling all the edit buttons.
        for (int i = 0; i < gvProjectDetails.Rows.Count; i++)
        {
            ImageButton btnImgEdit = (ImageButton)gvProjectDetails.Rows[i].FindControl(IMGBTNEDIT);
            if (i != rowIndex)
                btnImgEdit.Enabled = false;
        }
        HfIsDataModified.Value = CommonConstants.YES;
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
        Rave.HR.BusinessLayer.Employee.ProjectDetails objProjectDetailsBAL;

        if (ValidateControls())
        {
            BusinessEntities.ProjectDetails objProjectDetails = new BusinessEntities.ProjectDetails();
            objProjectDetailsBAL = new Rave.HR.BusinessLayer.Employee.ProjectDetails();
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
                //Mohamed : Issue 41442 : 28/03/2013 : Starts
                //Desc :  when an employee clicks on project details and try to edit the same, the responsibility upper text box is shown blank though the responsibilities exist in below textbox

                //gvProjectDetails.Rows[rowIndex].Cells[8].Text = txtProjectDescription.Text;
                //gvProjectDetails.Rows[rowIndex].Cells[9].Text = txtResponsibility.Text;
                TextBox TxtProjDesc = (TextBox)gvProjectDetails.Rows[rowIndex].Cells[8].FindControl("ProjectDescription");
                TxtProjDesc.Text = txtProjectDescription.Text;
                gvProjectDetails.Rows[rowIndex].Cells[10].Text = txtResponsibility.Text;

                //Mohamed : Issue 41442 : 28/03/2013 : Ends




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

            //Mohamed : Issue 50200 : 28/03/2014 : Starts
            //Desc :  Aryabhatta has raised 2 issues in RMS

            //for (int i = 0; i < ProjectDetailsCollection.Count; i++)
            //{
            objProjectDetails = (BusinessEntities.ProjectDetails)ProjectDetailsCollection.Item(rowIndex);

            Label Grid_ProjectId = (Label)gvProjectDetails.Rows[rowIndex].FindControl(PROJECTID);
            Label Grid_Mode = (Label)gvProjectDetails.Rows[rowIndex].FindControl(MODE);

            objProjectDetails.ProjectId = int.Parse(Grid_ProjectId.Text);
            objProjectDetails.EMPId = int.Parse(EMPId.Value);
            objProjectDetails.ProjectDoneName = gvProjectDetails.Rows[rowIndex].Cells[0].Text;
            objProjectDetails.ProjectName = gvProjectDetails.Rows[rowIndex].Cells[1].Text;
            objProjectDetails.Organisation = gvProjectDetails.Rows[rowIndex].Cells[2].Text;
            objProjectDetails.ProjectLocation = gvProjectDetails.Rows[rowIndex].Cells[3].Text;
            objProjectDetails.ClientName = gvProjectDetails.Rows[rowIndex].Cells[4].Text;
            objProjectDetails.ProjectSize = Convert.ToInt32(gvProjectDetails.Rows[rowIndex].Cells[5].Text);
            objProjectDetails.OnsiteDuration = gvProjectDetails.Rows[rowIndex].Cells[6].Text;
            objProjectDetails.Role = gvProjectDetails.Rows[rowIndex].Cells[7].Text;

            //Mohamed : Issue 41442 : 28/03/2013 : Starts
            //Desc :  when an employee clicks on project details and try to edit the same, the responsibility upper text box is shown blank though the responsibilities exist in below textbox

            //objProjectDetails.Description = gvProjectDetails.Rows[i].Cells[8].Text;
            //objProjectDetails.Resposibility = gvProjectDetails.Rows[i].Cells[9].Text;
            TextBox Grid_TxtProjDesc = (TextBox)gvProjectDetails.Rows[rowIndex].Cells[8].FindControl("ProjectDescription");
            objProjectDetails.Description = Grid_TxtProjDesc.Text;
            objProjectDetails.Resposibility = gvProjectDetails.Rows[rowIndex].Cells[10].Text;

            //Mohamed : Issue 41442 : 28/03/2013 : Ends

            objProjectDetails.Mode = int.Parse(Grid_Mode.Text);
            //}

            this.DoDataBind();

            objProjectDetailsBAL.UpdateProjectDetails(objProjectDetails);

            //Mohamed : Issue 50200 : 28/03/2014 : Ends

            //Clear all the fields after inserting row into gridview
            this.ClearControls();

            btnAddRow.Visible = true;
            btnCancel.Visible = true;
            btnUpdate.Visible = false;
            btnCancelRow.Visible = false;

            //Enabling all the edit buttons.
            for (int i = 0; i < gvProjectDetails.Rows.Count; i++)
            {
                ImageButton btnImgEdit = (ImageButton)gvProjectDetails.Rows[i].FindControl(IMGBTNEDIT);
                btnImgEdit.Enabled = true;
            }
            HfIsDataModified.Value = string.Empty;
            lblMessage.Text = "Project details updated successfully.";
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
        btnCancel.Visible = true;
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

    /// <summary>
    /// Shift the row position down.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgBtnRankDown_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            List<BusinessEntities.ProjectDetails> objProjectDetailsCollection = new List<BusinessEntities.ProjectDetails>();

            ImageButton thisbutton = (ImageButton)sender;
            GridViewRow thisrow = (GridViewRow)thisbutton.Parent.Parent;
            GridView thisgrid = (GridView)thisrow.Parent.Parent;

            int maxrows = thisgrid.Rows.Count;
            int row = (thisrow.RowIndex) + 1;

            int nextRowNumber = row + 1;

            //Check wheather row is last row or not,
            if (maxrows > row)
            {
                for (int i = 0; i < gvProjectDetails.Rows.Count; i++)
                {
                    BusinessEntities.ProjectDetails objProjectDetails = new BusinessEntities.ProjectDetails();
                    Label ProjectId = (Label)gvProjectDetails.Rows[i].FindControl(PROJECTID);
                    Label Mode = (Label)gvProjectDetails.Rows[i].FindControl(MODE);

                    int rank = 1;
                    //Get the hidden field which store value for order.
                    HiddenField hdfRank = (HiddenField)gvProjectDetails.Rows[i].FindControl(hdfRankOrder);

                    //Made Next row Up And current row down.
                    if ((i + 1) == nextRowNumber)
                    {
                        rank = int.Parse(hdfRank.Value) - 1;
                    }
                    else if ((i + 1) == row)
                    {
                        rank = int.Parse(hdfRank.Value) + 1;
                    }

                    objProjectDetails.ProjectId = int.Parse(ProjectId.Text);
                    objProjectDetails.EMPId = int.Parse(EMPId.Value);
                    objProjectDetails.ProjectDoneName = gvProjectDetails.Rows[i].Cells[0].Text;
                    objProjectDetails.ProjectName = gvProjectDetails.Rows[i].Cells[1].Text;
                    objProjectDetails.Organisation = gvProjectDetails.Rows[i].Cells[2].Text;
                    objProjectDetails.ProjectLocation = gvProjectDetails.Rows[i].Cells[3].Text;
                    objProjectDetails.ClientName = gvProjectDetails.Rows[i].Cells[4].Text;
                    objProjectDetails.ProjectSize = int.Parse(gvProjectDetails.Rows[i].Cells[5].Text);
                    objProjectDetails.OnsiteDuration = gvProjectDetails.Rows[i].Cells[6].Text;
                    objProjectDetails.Role = gvProjectDetails.Rows[i].Cells[7].Text;

                    //Mohamed : Issue 50200 : 28/03/2014 : Starts
                    //Desc :  Aryabhatta has raised 2 issues in RMS

                    //objProjectDetails.Description = gvProjectDetails.Rows[i].Cells[8].Text;
                    //objProjectDetails.Resposibility = gvProjectDetails.Rows[i].Cells[9].Text;

                    TextBox Grid_TxtProjDesc = (TextBox)gvProjectDetails.Rows[i].Cells[8].FindControl("ProjectDescription");
                    objProjectDetails.Description = Grid_TxtProjDesc.Text;
                    objProjectDetails.Resposibility = gvProjectDetails.Rows[i].Cells[10].Text;
                                        
                    //Mohamed : Issue 50200 : 28/03/2014 : Ends

                    objProjectDetails.Mode = 2;
                    objProjectDetails.RankOrder = rank;

                    Label LocationId = (Label)gvProjectDetails.Rows[i].FindControl(ProjectLocationId);
                    objProjectDetails.LocationId = Convert.ToInt32(LocationId.Text);

                    Label ProjectDoneID = (Label)gvProjectDetails.Rows[i].FindControl(ProjectDoneStatus);
                    objProjectDetails.ProjectDone = Convert.ToInt32(ProjectDoneID.Text);

                    objProjectDetailsCollection.Add(objProjectDetails);
                }

                //Make the data sorted on the basis of rank.
                if (objProjectDetailsCollection != null)
                {
                    var orderedEmpList = from emp in objProjectDetailsCollection
                                         orderby emp.RankOrder
                                         select emp;

                    //Convert it into collection and display in grid.
                    objProjectDetailsCollection = orderedEmpList.ToList();
                    gvProjectDetails.DataSource = objProjectDetailsCollection;
                    gvProjectDetails.DataBind();
                }
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "imgBtnRankDown_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }


    }

    /// <summary>
    ///  Shift row one position above.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgBtnRankUp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            List<BusinessEntities.ProjectDetails> objProjectDetailsCollection = new List<BusinessEntities.ProjectDetails>();

            ImageButton thisbutton = (ImageButton)sender;
            GridViewRow thisrow = (GridViewRow)thisbutton.Parent.Parent;
            GridView thisgrid = (GridView)thisrow.Parent.Parent;

            int maxrows = thisgrid.Rows.Count;
            int row = (thisrow.RowIndex) + 1;

            int previousrownumber = row - 1;

            //If row is a top row.
            if (previousrownumber > 0)
            {
                for (int i = 0; i < gvProjectDetails.Rows.Count; i++)
                {
                    BusinessEntities.ProjectDetails objProjectDetails = new BusinessEntities.ProjectDetails();
                    Label ProjectId = (Label)gvProjectDetails.Rows[i].FindControl(PROJECTID);
                    Label Mode = (Label)gvProjectDetails.Rows[i].FindControl(MODE);

                    int rank = 1;
                    HiddenField hdfRank = (HiddenField)gvProjectDetails.Rows[i].FindControl(hdfRankOrder);

                    //Make current row up and previous row down.
                    if ((i + 1) == previousrownumber)
                    {
                        rank = int.Parse(hdfRank.Value) + 1;
                    }
                    else if ((i + 1) == row)
                    {
                        rank = int.Parse(hdfRank.Value) - 1;
                    }

                    objProjectDetails.ProjectId = int.Parse(ProjectId.Text);
                    objProjectDetails.EMPId = int.Parse(EMPId.Value);
                    objProjectDetails.ProjectDoneName = gvProjectDetails.Rows[i].Cells[0].Text;
                    objProjectDetails.ProjectName = gvProjectDetails.Rows[i].Cells[1].Text;
                    objProjectDetails.Organisation = gvProjectDetails.Rows[i].Cells[2].Text;
                    objProjectDetails.ProjectLocation = gvProjectDetails.Rows[i].Cells[3].Text;
                    objProjectDetails.ClientName = gvProjectDetails.Rows[i].Cells[4].Text;
                    objProjectDetails.ProjectSize = int.Parse(gvProjectDetails.Rows[i].Cells[5].Text);
                    objProjectDetails.OnsiteDuration = gvProjectDetails.Rows[i].Cells[6].Text;
                    objProjectDetails.Role = gvProjectDetails.Rows[i].Cells[7].Text;

                    //Mohamed : Issue 50200 : 28/03/2014 : Starts
                    //Desc :  Aryabhatta has raised 2 issues in RMS

                    //objProjectDetails.Description = gvProjectDetails.Rows[i].Cells[8].Text;
                    //objProjectDetails.Resposibility = gvProjectDetails.Rows[i].Cells[9].Text;

                    TextBox Grid_TxtProjDesc = (TextBox)gvProjectDetails.Rows[i].Cells[8].FindControl("ProjectDescription");
                    objProjectDetails.Description = Grid_TxtProjDesc.Text;
                    objProjectDetails.Resposibility = gvProjectDetails.Rows[i].Cells[10].Text;

                    //Mohamed : Issue 50200 : 28/03/2014 : Ends
                                        
                    objProjectDetails.Mode = 2;
                    objProjectDetails.RankOrder = rank;

                    Label LocationId = (Label)gvProjectDetails.Rows[i].FindControl(ProjectLocationId);
                    objProjectDetails.LocationId = Convert.ToInt32(LocationId.Text);

                    Label ProjectDoneID = (Label)gvProjectDetails.Rows[i].FindControl(ProjectDoneStatus);
                    objProjectDetails.ProjectDone = Convert.ToInt32(ProjectDoneID.Text);

                    objProjectDetailsCollection.Add(objProjectDetails);
                }

                //Make the data sorted on the basis of rank.
                if (objProjectDetailsCollection != null)
                {
                    var orderedEmpList = from emp in objProjectDetailsCollection
                                         orderby emp.RankOrder
                                         select emp;

                    //Convert it into collection and display in grid.
                    objProjectDetailsCollection = orderedEmpList.ToList();
                    gvProjectDetails.DataSource = objProjectDetailsCollection;
                    gvProjectDetails.DataBind();


                }
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "imgBtnRankUp_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
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
        Projectdetails.Enabled = true;
        btnAddRow.Visible = true;
        btnCancel.Visible = true;
        btnEditCancel.Visible = false;
        btnEdit.Visible = false;
        //To solved issue id 19221
        //Coded by Rahul P
        //Start
        btnUpdate.Visible = false;
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
        //To solved issue id 19221
        //Coded by Rahul P
        //Start
        //ddlLocation.SelectedIndex = 0;
        ddlLocation.SelectedIndex = -1;
        //end
        txtClientName.Text = string.Empty;
        txtResponsibility.Text = string.Empty;
        txtProjectSiZe.Text = string.Empty;
        //To solved issue id 19221
        //Coded by Rahul P
        //Start
        //ddlBifurcation.SelectedIndex = 0;
        ddlBifurcation.SelectedIndex = -1;
        //End

    }

    /// <summary>
    /// Populates the grid.
    /// </summary>
    private int PopulateGrid(int employeeID)
    {
        int i = 0;
        ProjectDetailsCollection = this.GetProjectDetails(employeeID);

        //Binding the datatable to grid
        gvProjectDetails.DataSource = ProjectDetailsCollection;
        gvProjectDetails.DataBind();
        //To solved issue id 19221
        //Coded by Rahul P
        //Start
        i = gvProjectDetails.Rows.Count;
        //End
        //Displaying grid header with NO record found message.
        if (gvProjectDetails.Rows.Count == 0)
            ShowHeaderWhenEmptyGrid();

        EMPId.Value = employeeID.ToString().Trim();
        return i;
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
            LogErrorMessage(ex);
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
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ShowHeaderWhenEmptyGrid", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
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
            //End
            this.GetLocation();
            this.GetBifurcation();
        }
        //To solved issue id 19221
        //Coded by Rahul P
        //Start
        if (i == 0)
        {
            Projectdetails.Enabled = true;
            btnEdit.Visible = false;
            btnEditCancel.Visible = false;
            btnCancel.Visible = true;
            btnAddRow.Visible = true;
            btnUpdate.Visible = false;
            btnCancelRow.Visible = false;
        }
        else
        {
            Projectdetails.Enabled = false;
            btnEdit.Visible = true;
            btnEditCancel.Visible = true;
            btnCancel.Visible = false;
            btnAddRow.Visible = false;
            btnUpdate.Visible = false;
            btnCancelRow.Visible = false;
        }
        //End

        //Ishwar: Issue Id - 54410 : 31122014 Start
        if (UserMailId.ToLower() != employee.EmailId.ToLower() && arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEPM))
        {
            btnEdit.Visible = false;
            btnCancel.Visible = false;
            btnAddRow.Visible = false;
            btnEditCancel.Visible = true;
            Projectdetails.Enabled = false;
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

    #endregion

}
