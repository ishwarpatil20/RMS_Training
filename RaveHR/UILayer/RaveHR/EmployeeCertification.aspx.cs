using System;
using System.Text;
using System.Web.UI.WebControls;
using Common;
using Common.AuthorizationManager;
using System.Collections;
using System.Web.UI;

public partial class EmployeeCertification : BaseClass
{
    #region Private Field Members

    #region ViewState Constants

    private string CERTIFICATIONDETAILS = "CertificationDetails";
    private string CERTIFICATIONDETAILSDELETE = "CERTIFICATIONDETAILSDelete";
    private string DELETEROWINDEX = "DeleteRowIndex";
    private string ROWINDEX = "RowIndex";

    #endregion ViewState Constants

    private string IMGBTNDELETE = "imgBtnDelete";
    private string IMGBTNEDIT = "imgBtnEdit";
    private string CLASS_NAME = "EmpCertification.aspx";
    private string CERTIFICATIONID = "CertificationId";
    private string MODE = "Mode";
    const string ReadOnly = "readonly";
    char[] SPILITER_SLASH = { '/' };
    string UserRaveDomainId;
    string UserMailId;
    string PageMode = string.Empty;
    const string PAGETYPE = "pagetype";
    const string PAGETYPEEMPLOYEESUMMERY = "EMPLOYEESUMMERY";

    Rave.HR.BusinessLayer.Employee.CertificationDetails ObjCertificationDetailsBL = new Rave.HR.BusinessLayer.Employee.CertificationDetails();
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

    protected EmployeeMenuUC BubbleControl;

    #endregion Private Field Members

    #region Local Properties

    private BusinessEntities.RaveHRCollection CertificationDetailsCollection
    {
        get
        {
            if (ViewState[CERTIFICATIONDETAILS] != null)
                return ((BusinessEntities.RaveHRCollection)ViewState[CERTIFICATIONDETAILS]);
            else
                return null;
        }
        set
        {
            ViewState[CERTIFICATIONDETAILS] = value;
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

        txtName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtName.ClientID + "','" + imgName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPACE + "');");
        imgName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanName.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
        imgName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanName.ClientID + "');");

        txtTotalScore.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtTotalScore.ClientID + "','" + imgTotalScore.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
        imgTotalScore.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanTotalScore.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgTotalScore.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanTotalScore.ClientID + "');");

        txtOutOf.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtOutOf.ClientID + "','" + imgOutOf.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
        imgOutOf.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanOutOf.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgOutOf.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanOutOf.ClientID + "');");

        ucDatePickerCertificationDate.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + ucDatePickerCertificationDate.ClientID + "','','');");
        ucDatePickerCertificationDate.Attributes.Add(ReadOnly, ReadOnly);
        ucDatePickerCertficationValidDate.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + ucDatePickerCertficationValidDate.ClientID + "','','');");
        ucDatePickerCertficationValidDate.Attributes.Add(ReadOnly, ReadOnly);

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
            Session["isSaved"] = null;
            Session[SessionNames.PAGEMODE] = Common.MasterEnum.PageModeEnum.View;
            this.PopulateGrid(employeeID);
        }

        if (gvCertification.Rows[0].Cells[0].Text == NO_RECORDS_FOUND_MESSAGE)
        {
            CertificationDetailsCollection.Clear();
            ShowHeaderWhenEmptyGrid();
        }

        // Get logged-in user's email id
        AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
        UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
        UserMailId = UserRaveDomainId.Replace("co.in", "com");

        arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());

        if (UserMailId.ToLower() == employee.EmailId.ToLower() || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
        {
            if (Session[SessionNames.PAGEMODE] != null)
            {
                PageMode = Session[SessionNames.PAGEMODE].ToString();

                if (PageMode == Common.MasterEnum.PageModeEnum.View.ToString() && IsPostBack == false && gvCertification.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
                {
                    Certificationdetails.Enabled = false;
                    btnEdit.Visible = true;
                    btnEditCancel.Visible = true;
                    btnCancel.Visible = false;
                    btnAddRow.Visible = false;
                }
            }
        }
        else
        {
            Certificationdetails.Enabled = false;

        }


        SavedControlVirtualPath = "~/EmployeeMenuUC.ascx";
        ReloadControl();
    }

    /// <summary>
    /// Handles the Click event of the btnAdd control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Rave.HR.BusinessLayer.Employee.CertificationDetails objCertificationDetailsBAL;

        if (ValidateControls())
        {
            BusinessEntities.CertificationDetails objCertificationDetails = new BusinessEntities.CertificationDetails();
            objCertificationDetailsBAL = new Rave.HR.BusinessLayer.Employee.CertificationDetails();

            if (gvCertification.Rows[0].Cells[0].Text == NO_RECORDS_FOUND_MESSAGE)
            {
                CertificationDetailsCollection.Clear();
            }

            objCertificationDetails.CertificationName = txtName.Text;
            objCertificationDetails.CertificateDate = Convert.ToDateTime(ucDatePickerCertificationDate.Text);
            if (ucDatePickerCertficationValidDate.Text != "")
                objCertificationDetails.CertificateValidDate = Convert.ToDateTime(ucDatePickerCertficationValidDate.Text);
            // Mohamed :Issue 50440 : 10/04/2014 : Starts
            // Desc : Remove Mandatory validation from "TotalScore" and "OutOf" Fields
            objCertificationDetails.Score = float.Parse(txtTotalScore.Text == "" ? "0" : txtTotalScore.Text);
            objCertificationDetails.OutOf = float.Parse(txtOutOf.Text == "" ? "0" : txtOutOf.Text);
            // Mohamed :Issue 50440 : 10/04/2014 : Ends
            objCertificationDetails.Mode = 1;
            objCertificationDetails.EMPId = int.Parse(EMPId.Value);

            CertificationDetailsCollection.Add(objCertificationDetails);

            objCertificationDetailsBAL.AddCertificationDetails(objCertificationDetails);

            this.PopulateGrid(objCertificationDetails.EMPId);

            this.ClearControls();
            //To solved the issue no 19221
            //Comment by Rahul P 
            //Start
            //btnAddRow.Text = CommonConstants.BTN_AddRow;
            btnAddRow.Text = "Save";
            //End
            lblMessage.Text = "Certification details saved successfully.";
        }
    }

    /// <summary>
    /// Handles the RowEditing event of the gvCertification control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewEditEventArgs"/> instance containing the event data.</param>
    protected void gvCertification_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int rowIndex = 0;

        txtName.Text = gvCertification.Rows[e.NewEditIndex].Cells[0].Text.Trim();
        ucDatePickerCertificationDate.Text = gvCertification.Rows[e.NewEditIndex].Cells[1].Text;
        ucDatePickerCertficationValidDate.Text = gvCertification.Rows[e.NewEditIndex].Cells[2].Text;
        txtTotalScore.Text = gvCertification.Rows[e.NewEditIndex].Cells[3].Text;
        txtOutOf.Text = gvCertification.Rows[e.NewEditIndex].Cells[4].Text;

        ImageButton btnImg = (ImageButton)gvCertification.Rows[e.NewEditIndex].FindControl(IMGBTNDELETE);
        rowIndex = e.NewEditIndex;
        ViewState[ROWINDEX] = null;
        ViewState[ROWINDEX] = rowIndex;
        btnImg.Enabled = false;

        btnAddRow.Visible = false;
        btnCancel.Visible = false;
        btnUpdate.Visible = true;
        btnCancelRow.Visible = true;


        //Disabling all the edit buttons.
        for (int i = 0; i < gvCertification.Rows.Count; i++)
        {
            ImageButton btnImgEdit = (ImageButton)gvCertification.Rows[i].FindControl(IMGBTNEDIT);
            if (i != rowIndex)
                btnImgEdit.Enabled = false;
        }

        HfIsDataModified.Value = CommonConstants.YES;
    }

    /// <summary>
    /// Handles the RowDeleting event of the gvCertification control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewDeleteEventArgs"/> instance containing the event data.</param>
    protected void gvCertification_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int deleteRowIndex = 0;
        int rowIndex = -1;
        Boolean Flag = false;
        Rave.HR.BusinessLayer.Employee.CertificationDetails objCertificationDetailsBAL;
        BusinessEntities.CertificationDetails objCertificationDetails = new BusinessEntities.CertificationDetails();
        objCertificationDetailsBAL = new Rave.HR.BusinessLayer.Employee.CertificationDetails();

        deleteRowIndex = e.RowIndex;

        objCertificationDetails = (BusinessEntities.CertificationDetails)CertificationDetailsCollection.Item(deleteRowIndex);
        if (objCertificationDetails.Mode == 1)
            Flag = true;
        objCertificationDetails.Mode = 3;

        if (ViewState[CERTIFICATIONDETAILSDELETE] != null)
        {
            BusinessEntities.RaveHRCollection objDeleteCertificationDetailsCollection = (BusinessEntities.RaveHRCollection)ViewState[CERTIFICATIONDETAILSDELETE];
            objDeleteCertificationDetailsCollection.Add(objCertificationDetails);

            ViewState[CERTIFICATIONDETAILSDELETE] = objDeleteCertificationDetailsCollection;
        }
        else
        {
            BusinessEntities.RaveHRCollection objDeleteCertificationDetailsCollection1 = new BusinessEntities.RaveHRCollection();

            objDeleteCertificationDetailsCollection1.Add(objCertificationDetails);

            ViewState[CERTIFICATIONDETAILSDELETE] = objDeleteCertificationDetailsCollection1;
        }

        CertificationDetailsCollection.RemoveAt(deleteRowIndex);

        ViewState[DELETEROWINDEX] = deleteRowIndex;

        DoDataBind();

        objCertificationDetailsBAL.DeleteCertificationDetails(objCertificationDetails);

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

            ImageButton btnImg = (ImageButton)gvCertification.Rows[rowIndex].FindControl(IMGBTNDELETE);
            btnImg.Enabled = false;

            //Disabling all the edit buttons.
            for (int i = 0; i < gvCertification.Rows.Count; i++)
            {
                if (rowIndex != i)
                {
                    ImageButton btnImgEdit = (ImageButton)gvCertification.Rows[i].FindControl(IMGBTNEDIT);
                    btnImgEdit.Enabled = false;
                }
            }
        }

        lblMessage.Text = "Certification details deleted successfully.";
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
    /// Handles the Click event of the btnUpdate control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Rave.HR.BusinessLayer.Employee.CertificationDetails objCertificationDetailsBAL;
        BusinessEntities.CertificationDetails objCertificationDetails = new BusinessEntities.CertificationDetails();

        if (ValidateControls())
        {
            objCertificationDetailsBAL = new Rave.HR.BusinessLayer.Employee.CertificationDetails();
            int rowIndex = 0;
            int deleteRowIndex = -1;
            int temp = 0;

            if (ViewState[DELETEROWINDEX] != null)
            {
                deleteRowIndex = Convert.ToInt32(ViewState[DELETEROWINDEX].ToString());
            }

            //Update the grid view according the row, which is selected for editing.
            if (ViewState[ROWINDEX] != null)
            {
                rowIndex = Convert.ToInt32(ViewState[ROWINDEX].ToString());
                if (deleteRowIndex != -1 && deleteRowIndex < rowIndex) rowIndex--;
                Label CertificationId = (Label)gvCertification.Rows[rowIndex].FindControl(CERTIFICATIONID);
                Label Mode = (Label)gvCertification.Rows[rowIndex].FindControl(MODE);

                gvCertification.Rows[rowIndex].Cells[0].Text = txtName.Text;
                gvCertification.Rows[rowIndex].Cells[1].Text = ucDatePickerCertificationDate.Text;
                gvCertification.Rows[rowIndex].Cells[2].Text = ucDatePickerCertficationValidDate.Text;
                gvCertification.Rows[rowIndex].Cells[3].Text = txtTotalScore.Text;
                gvCertification.Rows[rowIndex].Cells[4].Text = txtOutOf.Text;

                if (int.Parse(CertificationId.Text) == 0)
                {
                    Mode.Text = "1";
                }
                else
                {
                    Mode.Text = "2";
                }
                ImageButton btnImg = (ImageButton)gvCertification.Rows[rowIndex].FindControl(IMGBTNDELETE);
                btnImg.Enabled = true;
                ViewState[ROWINDEX] = null;
                ViewState[DELETEROWINDEX] = null;

            }

            //Mohamed : Issue 50200 : 28/03/2014 : Starts
            //Desc :  Aryabhatta has raised 2 issues in RMS
            //for (int i = 0; i < CertificationDetailsCollection.Count; i++)
            //{

            objCertificationDetails = (BusinessEntities.CertificationDetails)CertificationDetailsCollection.Item(rowIndex);

            Label Grid_CertificationId = (Label)gvCertification.Rows[rowIndex].FindControl(CERTIFICATIONID);
            Label Grid_Mode = (Label)gvCertification.Rows[rowIndex].FindControl(MODE);

            objCertificationDetails.CertificationId = int.Parse(Grid_CertificationId.Text);
            objCertificationDetails.EMPId = int.Parse(EMPId.Value);
            objCertificationDetails.CertificationName = gvCertification.Rows[rowIndex].Cells[0].Text;
            objCertificationDetails.CertificateDate = Convert.ToDateTime(gvCertification.Rows[rowIndex].Cells[1].Text);
            if (gvCertification.Rows[rowIndex].Cells[2].Text != "")
                objCertificationDetails.CertificateValidDate = Convert.ToDateTime(gvCertification.Rows[rowIndex].Cells[2].Text);
            objCertificationDetails.Score = float.Parse(gvCertification.Rows[rowIndex].Cells[3].Text);
            objCertificationDetails.OutOf = float.Parse(gvCertification.Rows[rowIndex].Cells[4].Text);
            objCertificationDetails.Mode = int.Parse(Grid_Mode.Text);
            //}

            this.DoDataBind();

            objCertificationDetailsBAL.UpdateCertificationDetails(objCertificationDetails);

            //Mohamed : Issue 50200 : 28/03/2014 : Ends

            //Clear all the fields after inserting row into gridview
            this.ClearControls();

            btnAddRow.Visible = true;
            btnUpdate.Visible = false;
            btnCancelRow.Visible = false;
            //To solved the issue no 19221
            //Comment by Rahul P 
            //Start
            btnCancel.Visible = true;
            //End
            //Enabling all the edit buttons.
            for (int i = 0; i < gvCertification.Rows.Count; i++)
            {
                ImageButton btnImgEdit = (ImageButton)gvCertification.Rows[i].FindControl(IMGBTNEDIT);
                btnImgEdit.Enabled = true;
            }
            HfIsDataModified.Value = string.Empty;

            lblMessage.Text = "Certification details updated successfully.";
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
            ImageButton btnImg = (ImageButton)gvCertification.Rows[rowIndex].FindControl(IMGBTNDELETE);
            btnImg.Enabled = true;
            ViewState[ROWINDEX] = null;
            ViewState[DELETEROWINDEX] = null;
        }

        //Enabling all the edit buttons.
        for (int i = 0; i < gvCertification.Rows.Count; i++)
        {
            ImageButton btnImgEdit = (ImageButton)gvCertification.Rows[i].FindControl(IMGBTNEDIT);
            btnImgEdit.Enabled = true;
        }

        btnAddRow.Visible = true;
        btnCancel.Visible = true;
        btnUpdate.Visible = false;
        btnCancelRow.Visible = false;
        //To solved the issue no 19221
        //Comment by Rahul P 
        //Start
        btnCancel.Visible = true;
        //End
    }

    /// <summary>
    /// Handles the Click event of the btnNext control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Response.Redirect(CommonConstants.PAGE_ORGANIZATIONDETAILS);
    }

    /// <summary>
    /// Handles the Click event of the btnPrevious control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Response.Redirect(CommonConstants.PAGE_PROFESSIONALETAILS);
    }

    /// <summary>
    /// Handles the Click event of the btnEdit control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Session[SessionNames.PAGEMODE] = Common.MasterEnum.PageModeEnum.Edit;
        Certificationdetails.Enabled = true;
        btnAddRow.Visible = true;
        btnCancel.Visible = true;
        btnEditCancel.Visible = false;
        btnEdit.Visible = false;
        //To solved the issue no 19221
        //Comment by Rahul P 
        //Start
        btnUpdate.Visible = false;
        //End
        btnCancelRow.Visible = false;
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

    #endregion Protected Events

    #region Private Member Functions

    /// <summary>
    /// Does the data bind.
    /// </summary>
    private void DoDataBind()
    {
        gvCertification.DataSource = CertificationDetailsCollection;
        gvCertification.DataBind();

        //Displaying grid header with NO record found message.
        if (gvCertification.Rows.Count == 0)
            ShowHeaderWhenEmptyGrid();


    }

    /// <summary>
    /// Clears the controls.
    /// </summary>
    private void ClearControls()
    {
        txtName.Text = string.Empty;
        ucDatePickerCertificationDate.Text = string.Empty;
        ucDatePickerCertficationValidDate.Text = string.Empty;
        txtTotalScore.Text = string.Empty;
        txtOutOf.Text = string.Empty;
    }

    /// <summary>
    /// Populates the grid.
    /// </summary>
    private int PopulateGrid(int employeeID)
    {

        int i = 0;
        CertificationDetailsCollection = this.GetCertificationDetails(employeeID);

        //Binding the datatable to grid
        gvCertification.DataSource = CertificationDetailsCollection;
        gvCertification.DataBind();
        //To solved the issue no 19221
        //Comment by Rahul P 
        //Start
        i = gvCertification.Rows.Count;
        //End
        //Displaying grid header with NO record found message.
        if (gvCertification.Rows.Count == 0)
            ShowHeaderWhenEmptyGrid();
        else

            //btnAddRow.Text = CommonConstants.BTN_AddRow;
            btnAddRow.Text = "Save";

        //EMPId.Value = "14";
        EMPId.Value = employeeID.ToString().Trim();
        return i;
    }

    /// <summary>
    /// Handles the RowDataBound event of the gvCertification control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
    protected void gvCertification_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToDateTime(e.Row.Cells[2].Text) == DateTime.MinValue)
            {
                e.Row.Cells[2].Text = "";
            }
        }
    }

    /// <summary>
    /// Gets the certification details.
    /// </summary>
    /// <returns></returns>
    private BusinessEntities.RaveHRCollection GetCertificationDetails(int employeeID)
    {
        Rave.HR.BusinessLayer.Employee.CertificationDetails objCertificationDetailsBAL;
        BusinessEntities.CertificationDetails objCertificationDetails;

        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            objCertificationDetailsBAL = new Rave.HR.BusinessLayer.Employee.CertificationDetails();
            objCertificationDetails = new BusinessEntities.CertificationDetails();

            //objCertificationDetails.EMPId = 14;
            objCertificationDetails.EMPId = employeeID;

            raveHRCollection = objCertificationDetailsBAL.GetCertificationDetails(objCertificationDetails);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetCertificationDetails", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
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
        string[] certificateDateArr;
        string[] certificateValidEndDateArr;

        // Mohamed :Issue 50440 :  10/04/2014 : Starts
        // Desc : Remove Mandatory validation from "TotalScore" and "OutOf" Fields

        int TotalScore = Convert.ToInt32(txtTotalScore.Text == "" ? "0" : txtTotalScore.Text);
        int OutOf = Convert.ToInt32(txtOutOf.Text == "" ? "0" : txtOutOf.Text);

        // Mohamed :Issue 50440 : 10/04/2014 : Ends

        if (OutOf < TotalScore)
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.OUTOF_LESSTHAN_TOTALSCORE);
            flag = false;
        }

        certificateDateArr = Convert.ToString(ucDatePickerCertificationDate.Text).Split(SPILITER_SLASH);
        DateTime certificateDate = new DateTime(Convert.ToInt32(certificateDateArr[2]), Convert.ToInt32(certificateDateArr[1]), Convert.ToInt32(certificateDateArr[0]));

        if (ucDatePickerCertficationValidDate.Text != "")
        {
            certificateValidEndDateArr = Convert.ToString(ucDatePickerCertficationValidDate.Text).Split(SPILITER_SLASH);
            DateTime certificateValidEndDate = new DateTime(Convert.ToInt32(certificateValidEndDateArr[2]), Convert.ToInt32(certificateValidEndDateArr[1]), Convert.ToInt32(certificateValidEndDateArr[0]));


            if (certificateValidEndDate < DateTime.Now.Date)
            {
                errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.CERTIFICATION_VALIDDATE_VALIDATION);
                flag = false;
            }
        }

        if (certificateDate > DateTime.Now.Date)
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.CERTIFICATION_DATE_VALIDATION);
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
            gvCertification.ShowHeader = true;

            //Create empty datasource for Grid view and bind
            CertificationDetailsCollection.Add(new BusinessEntities.CertificationDetails());
            gvCertification.DataSource = CertificationDetailsCollection;
            gvCertification.DataBind();

            //Calculate number of columns in Grid view used for column Span
            int columnsCount = gvCertification.Columns.Count;

            //clear all the cells in the row
            gvCertification.Rows[0].Cells.Clear();

            //add a new blank cell
            gvCertification.Rows[0].Cells.Add(new TableCell());
            gvCertification.Rows[0].Cells[0].Text = NO_RECORDS_FOUND_MESSAGE;
            gvCertification.Rows[0].Cells[0].Wrap = false;
            gvCertification.Rows[0].Cells[0].Width = Unit.Percentage(10);
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
            //To solved the issue no 19221
            //Comment by Rahul P 
            //Start
            this.ClearControls();
            i = this.PopulateGrid(employee.EMPId);
        }

        if (i == 0)
        {
            Certificationdetails.Enabled = true;
            btnEdit.Visible = false;
            btnEditCancel.Visible = false;
            btnCancel.Visible = true;
            btnAddRow.Visible = true;
            btnUpdate.Visible = false;
            btnCancelRow.Visible = false;
        }
        else
        {
            Certificationdetails.Enabled = false;
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
            btnUpdate.Visible = false;
            btnCancel.Visible = false;
            btnAddRow.Visible = false;
            btnEditCancel.Visible = true;
            Certificationdetails.Enabled = false;
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
