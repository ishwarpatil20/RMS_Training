using System;
using System.Text;
using System.Web.UI.WebControls;
using Common;
using Common.AuthorizationManager;
using System.Collections;
using System.Web.UI;
using System.Text.RegularExpressions;

public partial class EmployeeProfessionalCourses : BaseClass
{
    #region Private Field Members

    #region ViewState Constants

    private string PROFESSIONALDETAILS = "ProfessionalDetails";
    private string PROFESSIONALDETAILSDELETE = "ProfessionalDetailsDelete";
    private string DELETEROWINDEX = "DeleteRowIndex";
    private string ROWINDEX = "RowIndex";

    #endregion ViewState Constants

    private string IMGBTNDELETE = "imgBtnDelete";
    private string IMGBTNEDIT = "imgBtnEdit";
    private string CLASS_NAME = "EmpProfessionalCourses.aspx";
    private string PROFESSIONALID = "ProfessionalId";
    private string MODE = "Mode";
    Rave.HR.BusinessLayer.Employee.ProfessionalDetails ObjProfessionalDetailsBL = new Rave.HR.BusinessLayer.Employee.ProfessionalDetails();
    private int employeeID = 0;
    private BusinessEntities.Employee employee;
    string UserRaveDomainId;
    string UserMailId;
    Regex regexObj = null;
    Regex regexObjString = null;
    string PageMode = string.Empty;
    const string PAGETYPE = "pagetype";
    const string PAGETYPEEMPLOYEESUMMERY = "EMPLOYEESUMMERY";

    AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
    // Get logged-in user's email id
    AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
    ArrayList arrRolesForUser = new ArrayList();

    /// <summary>
    /// Defines a constant error for no records found after applying filter criteria
    /// </summary>
    private const string NO_RECORDS_FOUND_MESSAGE = "No records found.";

    protected EmployeeMenuUC BubbleControl;
  
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

        txtCourseName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtCourseName.ClientID + "','" + imgCourseName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPACE + "');");
        imgCourseName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanCourseName.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
        imgCourseName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanCourseName.ClientID + "');");

        txtInstituteName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtInstituteName.ClientID + "','" + imgInstituteName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPECIALCHAR + "');");
        imgInstituteName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanInstituteName.ClientID + "','" + Common.CommonConstants.MSG_ALPHABET_SPECIALCHAR + "');");
        imgInstituteName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanInstituteName.ClientID + "');");

        txtYearofPassing.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtYearofPassing.ClientID + "','" + imgYearofPassing.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
        imgYearofPassing.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanYearofPassing.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgYearofPassing.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanYearofPassing.ClientID + "');");

        imgScore.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanScore.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgScore.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanScore.ClientID + "');");

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
                this.PopulateGrid(employeeID);
            }

            if (gvProfessionalCourses.Rows[0].Cells[0].Text == NO_RECORDS_FOUND_MESSAGE)
            {
                ProfessionalDetailsCollection.Clear();
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

                    if (PageMode == Common.MasterEnum.PageModeEnum.View.ToString() && IsPostBack == false && gvProfessionalCourses.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
                    {
                        Professionaldetails.Enabled = false;
                        btnEdit.Visible = true;
                        btnEditCancel.Visible = true;
                        btnCancel.Visible = false;
                        btnAddRow.Visible = false;

                        if (gvProfessionalCourses.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
                        {
                            //Enabling all the edit buttons.
                            for (int i = 0; i < gvProfessionalCourses.Rows.Count; i++)
                            {
                                ImageButton btnImgEdit = (ImageButton)gvProfessionalCourses.Rows[i].FindControl(IMGBTNEDIT);
                                btnImgEdit.Enabled = false;
                                ImageButton btnImgDelete = (ImageButton)gvProfessionalCourses.Rows[i].FindControl(IMGBTNDELETE);
                                btnImgDelete.Enabled = false;
                            }
                        }
                    }
                }
            }
            else
            {
                Professionaldetails.Enabled = false;
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
        Rave.HR.BusinessLayer.Employee.ProfessionalDetails objProfessionalDetailsBAL;

        if (ValidateControls())
        {
            BusinessEntities.ProfessionalDetails objProfessionalDetails = new BusinessEntities.ProfessionalDetails();
            objProfessionalDetailsBAL = new Rave.HR.BusinessLayer.Employee.ProfessionalDetails();

            if (gvProfessionalCourses.Rows[0].Cells[0].Text == NO_RECORDS_FOUND_MESSAGE)
            {
                ProfessionalDetailsCollection.Clear();
            }

            // 26864-Ambar-Start
            // Code to accept only one decimal point
            bool is_decimal_point = false;
            string str_current_char = null, strScore = txtScore.Text;
            
            if ( (strScore != string.Empty) || (strScore != null) || (strScore != "") )
            {
              for (int i = 0; i < txtScore.Text.Length; i++)
              {
                str_current_char = strScore.Substring(i, 1);
                if ((str_current_char == "."))
                {
                  if (is_decimal_point)
                  {
                    lblMessage.Text = "<font color=RED>" + "Percentage can contain only one decimal point." + "</font>";
                    return;
                  }
                  else
                  {
                    is_decimal_point = true;
                  }
                }                
              }
            }
            // 26864-Ambar-End

            objProfessionalDetails.CourseName = txtCourseName.Text;
            objProfessionalDetails.InstitutionName = txtInstituteName.Text;
            objProfessionalDetails.PassingYear = txtYearofPassing.Text;
            objProfessionalDetails.Score = txtScore.Text;
            objProfessionalDetails.EMPId = int.Parse(EMPId.Value);

            objProfessionalDetails.Mode = 1;

            ProfessionalDetailsCollection.Add(objProfessionalDetails);

            objProfessionalDetailsBAL.AddProfessionalDetails(objProfessionalDetails);

            this.PopulateGrid(objProfessionalDetails.EMPId);

            this.ClearControls();

            lblMessage.Text = "Professional Courses saved successfully.";

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
        txtScore.Text = gvProfessionalCourses.Rows[e.NewEditIndex].Cells[3].Text.Trim();

        ImageButton btnImg = (ImageButton)gvProfessionalCourses.Rows[e.NewEditIndex].FindControl(IMGBTNDELETE);
        rowIndex = e.NewEditIndex;
        ViewState[ROWINDEX] = null;
        ViewState[ROWINDEX] = rowIndex;
        btnImg.Enabled = false;

        btnAddRow.Visible = false;
        btnCancel.Visible = false;
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
        Boolean Flag = false;
        Rave.HR.BusinessLayer.Employee.ProfessionalDetails objProfessionalDetailsBAL;
        BusinessEntities.ProfessionalDetails objProfessionalDetails = new BusinessEntities.ProfessionalDetails();
        objProfessionalDetailsBAL = new Rave.HR.BusinessLayer.Employee.ProfessionalDetails();

        deleteRowIndex = e.RowIndex;

        objProfessionalDetails = (BusinessEntities.ProfessionalDetails)ProfessionalDetailsCollection.Item(deleteRowIndex);
        if (objProfessionalDetails.Mode == 1)
            Flag = true;
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

        objProfessionalDetailsBAL.DeleteProfessionalDetails(objProfessionalDetails);

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
        lblMessage.Text = "Professional Courses deleted successfully.";

        HfIsDataModified.Value = string.Empty;
    }

    /// <summary>
    /// Handles the Click event of the btnUpdate control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        Rave.HR.BusinessLayer.Employee.ProfessionalDetails objProfessionalDetailsBAL;

        if (ValidateControls())
        {
            int rowIndex = 0;
            int deleteRowIndex = -1;
            objProfessionalDetailsBAL = new Rave.HR.BusinessLayer.Employee.ProfessionalDetails();
            BusinessEntities.ProfessionalDetails objProfessionalDetails = new BusinessEntities.ProfessionalDetails();

            // 26864-Ambar-Start
            // Code to accept only one decimal point
            bool is_decimal_point = false;
            string str_current_char = null, strScore = txtScore.Text;

            if ((strScore != string.Empty) || (strScore != null) || (strScore != ""))
            {
              for (int i = 0; i < txtScore.Text.Length; i++)
              {
                str_current_char = strScore.Substring(i, 1);
                if ((str_current_char == "."))
                {
                  if (is_decimal_point)
                  {
                    lblMessage.Text = "<font color=RED>" + "Percentage can contain only one decimal point." + "</font>";
                    return;
                  }
                  else
                  {
                    is_decimal_point = true;
                  }
                }
              }
            }
            // 26864-Ambar-End
            
            

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
                
                objProfessionalDetails = (BusinessEntities.ProfessionalDetails)ProfessionalDetailsCollection.Item(i);

                Label ProfessionalId = (Label)gvProfessionalCourses.Rows[i].FindControl(PROFESSIONALID);
                Label Mode = (Label)gvProfessionalCourses.Rows[i].FindControl(MODE);

                objProfessionalDetails.ProfessionalId = int.Parse(ProfessionalId.Text);
                objProfessionalDetails.EMPId = int.Parse(EMPId.Value);

                objProfessionalDetails.CourseName = gvProfessionalCourses.Rows[i].Cells[0].Text;
                objProfessionalDetails.InstitutionName = gvProfessionalCourses.Rows[i].Cells[1].Text;
                objProfessionalDetails.PassingYear = gvProfessionalCourses.Rows[i].Cells[2].Text;
                objProfessionalDetails.Score = gvProfessionalCourses.Rows[i].Cells[3].Text;
                objProfessionalDetails.Mode = int.Parse(Mode.Text);
            }
            
            this.DoDataBind();

            objProfessionalDetailsBAL.UpdateProfessionalDetails(objProfessionalDetails);

            //Clear all the fields after inserting row into gridview
            this.ClearControls();

            btnAddRow.Visible = true;
            btnCancel.Visible = true;
            btnUpdateRow.Visible = false;
            btnCancelRow.Visible = false;

            //Enabling all the edit buttons.
            for (int i = 0; i < gvProfessionalCourses.Rows.Count; i++)
            {
                ImageButton btnImgEdit = (ImageButton)gvProfessionalCourses.Rows[i].FindControl(IMGBTNEDIT);
                btnImgEdit.Enabled = true;
            }

            lblMessage.Text = "Professional Courses updated successfully.";
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
        btnCancel.Visible = true;
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
    /// Handles the Click event of the btnEdit control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Session[SessionNames.PAGEMODE] = Common.MasterEnum.PageModeEnum.Edit;
        //To solved issue id 19221
        //Coded by Rahul P
        //Start
        //Professionaldetails.Visible = true;
        Professionaldetails.Enabled = true;
        btnAddRow.Visible = true;
        btnCancel.Visible = true;
        btnEditCancel.Visible = false;
        btnEdit.Visible = false;
        btnUpdateRow.Visible = false;
        btnCancelRow.Visible = false;
        //End
        if (gvProfessionalCourses.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
        {
            //Enabling all the edit buttons.
            for (int i = 0; i < gvProfessionalCourses.Rows.Count; i++)
            {
                ImageButton btnImgEdit = (ImageButton)gvProfessionalCourses.Rows[i].FindControl(IMGBTNEDIT);
                btnImgEdit.Enabled = true;
                ImageButton btnImgDelete = (ImageButton)gvProfessionalCourses.Rows[i].FindControl(IMGBTNDELETE);
                btnImgDelete.Enabled = true;
            }
        }
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
        //txtOutOf.Text = string.Empty;
    }

    /// <summary>
    /// Populates the grid.
    /// </summary>
    private int PopulateGrid(int employeeID)
    {
        int i = 0;
        ProfessionalDetailsCollection = this.GetProfessionalDetails(employeeID);

        //Binding the datatable to grid
        gvProfessionalCourses.DataSource = ProfessionalDetailsCollection;
        gvProfessionalCourses.DataBind();
        //To solved issue id 19221
        //Coded by Rahul P
        //Start
        i = gvProfessionalCourses.Rows.Count;
        //End
        //Displaying grid header with NO record found message.
        if (gvProfessionalCourses.Rows.Count == 0)
            ShowHeaderWhenEmptyGrid();
        
        EMPId.Value = employeeID.ToString().Trim();
        return i;
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
            LogErrorMessage(ex);
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

        int yearofPassing =  Convert.ToInt32(txtYearofPassing.Text);

        // 26416-Ambar-Start
        // Added following validation
        if (yearofPassing.ToString().Length != 4)
        {
          errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.YEAROF_PASSING_FORMAT_VALIDATION);
          flag = false;
        }
        // 26416-Ambar-End

        regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
     
        int CurrentYear = DateTime.Now.Year;

        if (CurrentYear < yearofPassing)
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.YEAROF_PASSING_VALIDATION);
            flag = false;
        }

        if (regexObj.IsMatch(txtScore.Text))
        {
            double Percentage = Convert.ToDouble(txtScore.Text);

            // 26416-Ambar-Start
            // Edited If condition to check for 0 value
            if (Percentage > 100.00 || Percentage <= 0.0)
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
        }

        if (i == 0)
        {
            Professionaldetails.Enabled = true;
            btnEdit.Visible = false;
            btnEditCancel.Visible = false;
            btnCancel.Visible = true;
            btnAddRow.Visible = true;
            btnUpdateRow.Visible = false;
            btnCancelRow.Visible = false;
        }
        else
        {
            Professionaldetails.Enabled = false;
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
            btnAddRow.Visible = false;
            btnEditCancel.Visible = true;
            Professionaldetails.Enabled = false;
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
