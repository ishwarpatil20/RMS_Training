using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.AuthorizationManager;
using Common;
using System.Text;
using System.Text.RegularExpressions;

public partial class EmployeeOrganization : BaseClass
{
    #region Private Field Members

    #region ViewState Constants

    private string ORGANIZATIONDETAILS = "OrganizationDetails";
    private string NONRELEVANTORGANIZATIONDETAILS = "NonRelevantOrganizationDetails";
    private string ORGANIZATIONDETAILSDELETE = "OrganizationDetailsDelete";
    private string ORGANIZATIONDETAILSNONDELETE = "OrganizationDetailsNonDelete";
    private string DELETEROWINDEX = "DeleteRowIndex";
    private string DELETENONRELEVANTROWINDEX = "DeleteNonRelevantRowIndex";
    private string ROWINDEX = "RowIndex";
    private string NONRELEVANTROWINDEX = "NonRelevantRowIndex";

    #endregion ViewState Constants

    private string IMGBTNDELETE = "imgBtnDelete";
    private string IMGBTNEDIT = "imgBtnEdit";
    private string CLASS_NAME = "EmpOrganization.aspx";
    private string ORGANIZATIONID = "OrganisationId";
    private string MODE = "Mode";
    const string ReadOnly = "readonly";
    char[] SPILITER_SLASH = { '/' };
    Rave.HR.BusinessLayer.Employee.OrganisationDetails ObjOrganisationDetailsBL = new Rave.HR.BusinessLayer.Employee.OrganisationDetails();
    private int employeeID = 0;
    private BusinessEntities.Employee employee;
    private static int ExperienceValue = 0;
    private static int NonExperienceValue = 0;
    string UserRaveDomainId;
    string UserMailId;
    string jScript;
    string PreviousPage = string.Empty;
    AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
    // Get logged-in user's email id
    AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
    ArrayList arrRolesForUser = new ArrayList();
    string MONTH = "Month";
    string YEAR = "Year";
    string MONTHS = "Months";
    string YEARS = "Years";
    private string MONTHSINCE = "MonthSince";
    private string YEARSINCE = "YearSince";
    private string MONTHTILL = "MonthTill";
    private string YEARTILL = "YearTill";
    private string EXPERIENCEMONTH = "ExperienceMonth";
    private string EXPERIENCEYEAR = "ExperienceYear";
    private string TRUE = "True";
    string PageMode = string.Empty;
    const string PAGETYPE = "pagetype";
    const string PAGETYPEEMPLOYEESUMMERY = "EMPLOYEESUMMERY";
    //28109-Ambar-Start    
    //public static int ITotalMonths ;
    //public static int ITotalYears;
    //public static int IReleventMonths;
    //public static int IReleventYears;

    //Mahendra Temp Fix 28109 STRAT
    public  int ITotalMonths;
    public  int ITotalYears;
    public  int IReleventMonths;
    public  int IReleventYears;
    //Mahendra Temp Fix 28109 END

    Regex regexObj = null;
    //28109-Ambar-End
    /// <summary>
    /// Defines a constant error for no records found after applying filter criteria
    /// </summary>
    private const string NO_RECORDS_FOUND_MESSAGE = "No records found.";

    protected EmployeeMenuUC BubbleControl;

    #endregion Private Field Members

    #region Local Properties

    /// <summary>
    /// Gets or sets the organisation details collection.
    /// </summary>
    /// <value>The organisation details collection.</value>
    private BusinessEntities.RaveHRCollection OrganisationDetailsCollection
    {
        get
        {
            if (ViewState[ORGANIZATIONDETAILS] != null)
                return ((BusinessEntities.RaveHRCollection)ViewState[ORGANIZATIONDETAILS]);
            else
                return null;
        }
        set
        {
            ViewState[ORGANIZATIONDETAILS] = value;
        }
    }

    /// <summary>
    /// Gets or sets the non relevant organisation details collection.
    /// </summary>
    /// <value>The non relevant organisation details collection.</value>
    private BusinessEntities.RaveHRCollection NonRelevantOrganisationDetailsCollection
    {
        get
        {
            if (ViewState[NONRELEVANTORGANIZATIONDETAILS] != null)
                return ((BusinessEntities.RaveHRCollection)ViewState[NONRELEVANTORGANIZATIONDETAILS]);
            else
                return null;
        }
        set
        {
            ViewState[NONRELEVANTORGANIZATIONDETAILS] = value;
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

    ////Aarohi : Issue 31584 : 16/01/2012 : Start
    //string[] arrOrganisationSince = new string[50];
    //string[] arrOrganisationTill = new string[50];
    ////Aarohi : Issue 31584 : 16/01/2012 : End

    #endregion



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
            //Clearing the error label
            lblError.Text = string.Empty;
            lblMessage.Text = string.Empty;

            btnAdd.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return ButtonClickValidate();");
            btnUpdate.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return ButtonClickValidate();");
            btnAddRow.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return RowButtonClickValidate();");
            btnUpdateRow.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return RowButtonClickValidate();");
            btnSave.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return SaveButtonClickValidate();");

            txtCompanyName.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return ValidateSpecialCharacters()");
            txtCompanyName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtCompanyName.ClientID + "','" + imgCompanyName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPECIALCHAR + "');");
            imgCompanyName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanCompanyName.ClientID + "','" + Common.CommonConstants.MSG_ALPHABET_SPECIALCHAR + "');");
            imgCompanyName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanCompanyName.ClientID + "');");

            txtPositionHeld.Attributes.Add(Common.CommonConstants.EVENT_ONKEYPRESS, "return ValidateSpecialCharacters()");
            txtPositionHeld.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtPositionHeld.ClientID + "','" + imgPositionHeld.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPECIALCHAR + "');");
            imgPositionHeld.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanPositionHeld.ClientID + "','" + Common.CommonConstants.MSG_ALPHABET_SPECIALCHAR + "');");
            imgPositionHeld.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanPositionHeld.ClientID + "');");

            txtRCompanyName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtRCompanyName.ClientID + "','" + imgRCompanyName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPECIALCHAR + "');");
            imgRCompanyName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanRCompanyName.ClientID + "','" + Common.CommonConstants.MSG_ALPHABET_SPECIALCHAR + "');");
            imgRCompanyName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanRCompanyName.ClientID + "');");

            txtRPositionHeld.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtRPositionHeld.ClientID + "','" + imgRPositionHeld.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPECIALCHAR + "');");
            imgRPositionHeld.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanRPositionHeld.ClientID + "','" + Common.CommonConstants.MSG_ALPHABET_SPECIALCHAR + "');");
            imgRPositionHeld.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanRPositionHeld.ClientID + "');");

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

                //Javascript for checking employee is fresher or not while loading page.
                //If employee is fresher then Page will redirected to previous page requested.
                if (Context.Request.UrlReferrer != null)
                {
                    PreviousPage = Context.Request.UrlReferrer.ToString();//gets previous page url

                    jScript = "<script language='javascript'>javascript:JavScriptFn('" + employee.IsFresher + "','" + PreviousPage + "')</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "Startup", jScript);
                }

                Session[SessionNames.PAGEMODE] = Common.MasterEnum.PageModeEnum.View;
                this.GetMonths(ddlMonthSince);
                this.GetMonths(ddlMonthTill);
                this.GetMonths(ddlRMonthSince);
                this.GetMonths(ddlRMonthTill);
                this.GetYears(ddlYearSince);
                this.GetYears(ddlYearTill);
                this.GetYears(ddlRYearSince);
                this.GetYears(ddlRYearTill);
                this.GetRelevantExperience(employeeID);
                this.PopulateGrid(employeeID);
                this.PopulateNonRelevantGrid(employeeID);
                //Mahendra Temp Fix 28109 STRAT
                this.CalculateRaveExperience(employee);
                //Mahendra Temp Fix 28109 End
            }

            if (gvOrganisation.Rows[0].Cells[0].Text == NO_RECORDS_FOUND_MESSAGE)
            {
                OrganisationDetailsCollection.Clear();
                ShowHeaderWhenEmptyRelevantGrid();
            }

            if (gvOrgNonReleventDetails.Rows[0].Cells[0].Text == NO_RECORDS_FOUND_MESSAGE)
            {
                NonRelevantOrganisationDetailsCollection.Clear();
                ShowHeaderWhenEmptyGrid();
            }



            // Get logged-in user's email id
            AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
            UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
            UserMailId = UserRaveDomainId.Replace("co.in", "com");

            arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());

            //Mahendra Temp Fix 28109 STRAT
            if (Session[SessionNames.PAGEMODE] != null)
                PageMode = Session[SessionNames.PAGEMODE].ToString();
            //Mahendra Temp Fix 28109 END

            if (employee != null)
            {
                if (UserMailId.ToLower() == employee.EmailId.ToLower() || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
                {

                    if (Session[SessionNames.PAGEMODE] != null)
                    {
                        //Mahendra Temp Fix 28109 STRAT
                        //PageMode = Session[SessionNames.PAGEMODE].ToString();
                        //Mahendra Temp Fix 28109 END

                        //if (PageMode == Common.MasterEnum.PageModeEnum.View.ToString() && IsPostBack == false && gvOrganisation.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
                        if (PageMode == Common.MasterEnum.PageModeEnum.View.ToString() && IsPostBack == false)
                        {
                            //To solved the issue no 19221
                            //Comment by Rahul P 
                            //Start
                            //Releventdetails.Visible = false;
                            //NonReleventDetails.Visible = false;
                            Releventdetails.Enabled = false;
                            NonReleventDetails.Enabled = false;
                            btnSave.Visible = false;
                            btnCancel1.Visible = false;
                            RelevantRow.Visible = false;
                            //Ishwar: Issue Id - 54410 30122014 Start
                            rblNonReleventExperience.Enabled = false;
                            //Ishwar: Issue Id - 54410 30122014 End
                            btnEdit.Visible = true;
                            //End
                            //if (gvOrganisation.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
                            //{
                            //    //Enabling all the edit buttons.
                            //    for (int i = 0; i < gvOrganisation.Rows.Count; i++)
                            //    {
                            //        ImageButton btnImgEdit = (ImageButton)gvOrganisation.Rows[i].FindControl(IMGBTNEDIT);
                            //        btnImgEdit.Enabled = false;
                            //        ImageButton btnImgDelete = (ImageButton)gvOrganisation.Rows[i].FindControl(IMGBTNDELETE);
                            //        btnImgDelete.Enabled = false;
                            //    }
                            //}

                            //if (gvOrgNonReleventDetails.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
                            //{
                            //    //Enabling all the edit buttons.
                            //    for (int i = 0; i < gvOrgNonReleventDetails.Rows.Count; i++)
                            //    {
                            //        ImageButton btnImgEdit = (ImageButton)gvOrgNonReleventDetails.Rows[i].FindControl(IMGBTNEDIT);
                            //        btnImgEdit.Enabled = false;
                            //        ImageButton btnImgDelete = (ImageButton)gvOrgNonReleventDetails.Rows[i].FindControl(IMGBTNDELETE);
                            //        btnImgDelete.Enabled = false;
                            //    }
                            //}
                        }
                        else
                        {

                            btnEdit.Visible = false;

                            //To solved the issue no 19221
                            //Comment by Rahul P 
                            //Start
                            btnSave.Visible = true;
                            //End


                        }
                    }
                }
                else
                {
                    Releventdetails.Enabled = false;
                    NonReleventDetails.Enabled = false;
                    btnSave.Visible = false;
                    btnCancel1.Visible = false;
                    //Ishwar: Issue Id - 54410 30122014 Start
                    rblNonReleventExperience.Enabled = false;
                    //Ishwar: Issue Id - 54410 30122014 End
                    //28109-Ambar-Start
                    if (!arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM))
                        ExperienceRow.Disabled = true;
                    //28109-Ambar-End
                }
            }

            //Mahendra Temp Fix 28109 STRAT
            //txtTotalMonths.Enabled = false;
            //txtTotalYears.Enabled = false;
            //txtReleventMonths.Enabled = false;
            //txtReleventYears.Enabled = false;
            if (PageMode == Common.MasterEnum.PageModeEnum.View.ToString())
            {
                txtTotalMonths.Enabled = false;
                txtTotalYears.Enabled = false;
                txtReleventMonths.Enabled = false;
                txtReleventYears.Enabled = false;
            }
            else
            {//Edit

                //txtTotalMonths.Enabled = true;
                // txtTotalYears.Enabled = true;

                //Siddharth 1 April 2015 Start
                //Logic:- Relevant Years and Months should not be Editable.
                //txtReleventMonths.Enabled = true;
                //txtReleventYears.Enabled = true;
                //Siddharth 1 April 2015 Start
            }
            //Mahendra Temp Fix 28109 END
            txtReleventMonths.Enabled = false;
            txtReleventYears.Enabled = false;
            txtExperienceMonth.Enabled = false;
            txtExperienceYear.Enabled = false;
            txtRExperienceMonth.Enabled = false;
            txtRExperienceYear.Enabled = false;

            //Ishwar: Issue Id - 54410 : 'To check whether user has access to edit Employee's details on Employee Summary page' starts
            if (UserMailId.ToLower() != employee.EmailId.ToLower() && arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEPM))
            {
                btnEdit.Visible = false;
            }
            //Ishwar: Issue Id - 54410 : 'To check whether user has access to edit Employee's details on Employee Summary page' ends

            SavedControlVirtualPath = "~/EmployeeMenuUC.ascx";
            ReloadControl();

            //Mahendra Temp Fix 28109 STRAT
            //28109-Ambar-Start
            //if (  ( arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR) 
            //        || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM) )
            //      && ( employee.EmailId != UserMailId )
            //    )
            //  btnEdit.Visible = true;
            //28109-Ambar-End
            //Mahendra Temp Fix 28109 END


            ////Aarohi : Issue 31584 : 16/01/2012 : Start
            //if (gvOrganisation.Rows.Count > 1)
            //{
            //    for (int i = 0; i < gvOrganisation.Rows.Count; i++)
            //    {
            //        arrOrganisationSince[i] = gvOrganisation.Rows[i].Cells[2].Text;
            //        arrOrganisationTill[i] = gvOrganisation.Rows[i].Cells[3].Text;
            //    }
            //}
            ////Aarohi : Issue 31584 : 16/01/2012 : End
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "Page_Load", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
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
                    //Label OrganisationId = (Label)gvOrganisation.Rows[rowIndex].FindControl(ORGANIZATIONID);
                    //Label Mode = (Label)gvOrganisation.Rows[rowIndex].FindControl(MODE);
                    //Label MonthSince = (Label)gvOrganisation.Rows[rowIndex].FindControl(MONTHSINCE);
                    //Label YearSince = (Label)gvOrganisation.Rows[rowIndex].FindControl(YEARSINCE);
                    //Label MonthTill = (Label)gvOrganisation.Rows[rowIndex].FindControl(MONTHTILL);
                    //Label YearTill = (Label)gvOrganisation.Rows[rowIndex].FindControl(YEARTILL);
                    //Label ExperienceMonth = (Label)gvOrganisation.Rows[e.NewEditIndex].FindControl(EXPERIENCEMONTH);
                    //Label ExperienceYear = (Label)gvOrganisation.Rows[e.NewEditIndex].FindControl(EXPERIENCEYEAR);

                    //MonthSince.Text = ddlMonthSince.SelectedValue;
                    //YearSince.Text = ddlYearSince.SelectedValue;
                    //MonthTill.Text = ddlMonthTill.SelectedValue;
                    //YearTill.Text = ddlYearTill.SelectedValue;
                    //ExperienceMonth.Text = ddlExperienceMonth.SelectedValue;
                    //ExperienceYear.Text = ddlExperienceYear.SelectedValue;
                    //gvOrganisation.Rows[rowIndex].Cells[0].Text = txtCompanyName.Text;
                    //gvOrganisation.Rows[rowIndex].Cells[1].Text = ddlMonthSince.SelectedItem.Text + "-" + ddlYearSince.SelectedItem.Text;
                    //gvOrganisation.Rows[rowIndex].Cells[2].Text = ddlMonthTill.SelectedItem.Text + "-" + ddlYearTill.SelectedItem.Text;
                    //gvOrganisation.Rows[rowIndex].Cells[3].Text = txtPositionHeld.Text;
                    ////gvOrganisation.Rows[rowIndex].Cells[4].Text = txtExperience.Text;

                    //if (int.Parse(OrganisationId.Text) == 0)
                    //{
                    //    Mode.Text = "1";
                    //}
                    //else
                    //{
                    //    Mode.Text = "2";
                    //}

                    ImageButton btnImg = (ImageButton)gvOrganisation.Rows[rowIndex].FindControl(IMGBTNDELETE);
                    btnImg.Enabled = true;
                    ViewState[ROWINDEX] = null;
                    ViewState[DELETEROWINDEX] = null;

                    //}
                    int total = 0;
                    //for (int i = 0; i < OrganisationDetailsCollection.Count; i++)
                    //{
                    BusinessEntities.OrganisationDetails objOrganisationDetails = new BusinessEntities.OrganisationDetails();
                    objOrganisationDetails = (BusinessEntities.OrganisationDetails)OrganisationDetailsCollection.Item(rowIndex);

                    Label OrganisationId = (Label)gvOrganisation.Rows[rowIndex].FindControl(ORGANIZATIONID);
                    Label Mode = (Label)gvOrganisation.Rows[rowIndex].FindControl(MODE);
                    //Label MonthSince = (Label)gvOrganisation.Rows[i].FindControl(MONTHSINCE);
                    //Label YearSince = (Label)gvOrganisation.Rows[i].FindControl(YEARSINCE);
                    //Label MonthTill = (Label)gvOrganisation.Rows[i].FindControl(MONTHTILL);
                    //Label YearTill = (Label)gvOrganisation.Rows[i].FindControl(YEARTILL);
                    //Label ExperienceMonth = (Label)gvOrganisation.Rows[i].FindControl(EXPERIENCEMONTH);
                    //Label ExperienceYear = (Label)gvOrganisation.Rows[i].FindControl(EXPERIENCEYEAR);

                    //MonthSince.Text = ddlMonthSince.SelectedValue;
                    //YearSince.Text = ddlYearSince.SelectedValue;
                    //MonthTill.Text = ddlMonthTill.SelectedValue;
                    //YearTill.Text = ddlYearTill.SelectedValue;
                    //ExperienceMonth.Text = txtExperienceMonth.Text;
                    //ExperienceYear.Text = txtExperienceYear.Text;
                    if (int.Parse(OrganisationId.Text) == 0)
                    {
                        Mode.Text = "1";
                    }
                    else
                    {
                        Mode.Text = "2";
                    }

                    objOrganisationDetails.OrganisationId = int.Parse(OrganisationId.Text);
                    objOrganisationDetails.EMPId = int.Parse(EMPId.Value);
                    objOrganisationDetails.CompanyName = txtCompanyName.Text.Trim();
                    objOrganisationDetails.MonthSince = int.Parse(ddlMonthSince.SelectedValue);
                    objOrganisationDetails.YearSince = int.Parse(ddlYearSince.SelectedValue);
                    objOrganisationDetails.MonthTill = int.Parse(ddlMonthTill.SelectedValue);
                    objOrganisationDetails.YearTill = int.Parse(ddlYearTill.SelectedValue);
                    objOrganisationDetails.Designation = txtPositionHeld.Text.Trim();
                    objOrganisationDetails.WorkingSince = ddlMonthSince.SelectedItem.Text + "-" + ddlYearSince.SelectedItem.Text;
                    objOrganisationDetails.WorkingTill = ddlMonthTill.SelectedItem.Text + "-" + ddlYearTill.SelectedItem.Text;
                    objOrganisationDetails.Experience = txtExperienceYear.Text + " " + YEARS + "-" + txtExperienceMonth.Text + " " + MONTHS;
                    objOrganisationDetails.ExperienceMonth = int.Parse(txtExperienceMonth.Text);
                    objOrganisationDetails.ExperienceYear = int.Parse(txtExperienceYear.Text);
                    objOrganisationDetails.Mode = int.Parse(Mode.Text);

                    //total = total + Convert.ToInt32(gvOrganisation.Rows[i].Cells[4].Text);

                    //}

                    //hfTotalExperience.Value = total.ToString();

                    this.DoDataBind();

                    //Clear all the fields after inserting row into gridview
                    this.ClearControls();

                    btnAdd.Visible = true;
                    btnUpdate.Visible = false;
                    btnCancel1.Visible = false;

                    //Enabling all the edit buttons.
                    for (int i = 0; i < gvOrganisation.Rows.Count; i++)
                    {
                        ImageButton btnImgEdit = (ImageButton)gvOrganisation.Rows[i].FindControl(IMGBTNEDIT);
                        btnImgEdit.Enabled = true;
                    }
                    //HfIsDataModified.Value = CommonConstants.YES;
                }
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnUpdate_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
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
                ImageButton btnImg = (ImageButton)gvOrganisation.Rows[rowIndex].FindControl(IMGBTNDELETE);
                btnImg.Enabled = true;
                ViewState[ROWINDEX] = null;
                ViewState[DELETEROWINDEX] = null;
            }

            //Enabling all the edit buttons.
            for (int i = 0; i < gvOrganisation.Rows.Count; i++)
            {
                ImageButton btnImgEdit = (ImageButton)gvOrganisation.Rows[i].FindControl(IMGBTNEDIT);
                btnImgEdit.Enabled = true;
            }

            btnAdd.Visible = true;
            btnUpdate.Visible = false;
            btnCancel1.Visible = false;

            //int Total = Convert.ToInt32(hfTotalExperience.Value) + ExperienceValue;
            //hfTotalExperience.Value = Total.ToString();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnCancel_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    /// <summary>
    /// Handles the Click event of the btnSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Rave.HR.BusinessLayer.Employee.OrganisationDetails objOrganisationDetailsBAL;
        int NonExperienceMonthTotal = 0;
        int NonExperienceYearTotal = 0;
        int ExperienceMonthTotal = 0;
        int ExperienceYearTotal = 0;

        BusinessEntities.OrganisationDetails objOrganisationDetails;
        BusinessEntities.RaveHRCollection objSaveOrganisationDetailsCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            objOrganisationDetailsBAL = new Rave.HR.BusinessLayer.Employee.OrganisationDetails();

            //28109-Ambar-Start
            if (  ( arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR) 
                    || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM) )
                  && (employee.EmailId.ToLower() != UserMailId.ToLower())
                )
            {
              regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
              if (txtTotalYears.Text != string.Empty)
              {
                if (!regexObj.IsMatch(txtTotalYears.Text))
                {
                  lblMessage.Text = "Total Years" + " should be numeric only.";
                  txtTotalMonths.Text = ITotalMonths.ToString();
                  txtTotalYears.Text = ITotalYears.ToString();
                  return;
                }
              }

              regexObj = new Regex(CommonConstants.INTEGER_NUMBER_REGEX);
              if (txtTotalMonths.Text != string.Empty)
              {
                if (!regexObj.IsMatch(txtTotalMonths.Text))
                {
                  lblMessage.Text = "Total Months" + " should be numeric only.";
                  txtTotalMonths.Text = ITotalMonths.ToString();
                  txtTotalYears.Text = ITotalYears.ToString();
                  return;
                }
              }

              Boolean update_flag = false;

              if ((ITotalMonths != Convert.ToInt32(txtTotalMonths.Text.ToString()))
                    || (ITotalYears != Convert.ToInt32(txtTotalYears.Text.ToString()))
                 )
              {
                update_flag = true;
              }

              if (update_flag)
              {
                  //Mahendra Temp Fix 28109 STRAT
                  objOrganisationDetailsBAL.UpdateTotalReleventExp(employeeID,
                                                                  Convert.ToInt32(txtReleventMonths.Text.ToString()),
                                                                  Convert.ToInt32(txtReleventYears.Text.ToString())
                      //,Convert.ToInt32(txtReleventMonths.Text.ToString()),
                      //Convert.ToInt32(txtReleventYears.Text.ToString())
                                                                  );

                //objOrganisationDetailsBAL.UpdateTotalReleventExp(employeeID,
                //                                                  Convert.ToInt32(txtTotalMonths.Text.ToString()),
                //                                                  Convert.ToInt32(txtTotalYears.Text.ToString())
                //  //,Convert.ToInt32(txtReleventMonths.Text.ToString()),
                //  //Convert.ToInt32(txtReleventYears.Text.ToString())
                //                                                  );

                  //Mahendra Temp Fix 28109 END
                ITotalMonths = Convert.ToInt32(txtTotalMonths.Text.ToString());
                ITotalYears = Convert.ToInt32(txtTotalYears.Text.ToString());

                //Mahendra Temp Fix 28109 STRAT
                txtTotalMonths.Text = txtReleventMonths.Text;
                txtTotalYears.Text = txtReleventYears.Text;

                this.CalculateRaveExperience(employee);
                //Mahendra Temp Fix 28109 END
                lblMessage.Text = "Experience details saved successfully.";
              }
            }            
            else
            { 
             // txtTotalMonths.Text = ITotalMonths.ToString();
             // txtTotalYears.Text = ITotalYears.ToString();

                ITotalMonths = Convert.ToInt32(txtTotalMonths.Text.ToString());
                ITotalYears = Convert.ToInt32(txtTotalYears.Text.ToString());


              //28109-Ambar-End
              if (!string.IsNullOrEmpty(txtReleventMonths.Text) && !string.IsNullOrEmpty(txtReleventYears.Text))
              {
                int ReleventMonths = Convert.ToInt32(txtReleventMonths.Text);
                int ReleventYears = Convert.ToInt32(txtReleventYears.Text);


                if (gvOrganisation.Rows[0].Cells[0].Text == NO_RECORDS_FOUND_MESSAGE && gvOrgNonReleventDetails.Rows[0].Cells[0].Text == NO_RECORDS_FOUND_MESSAGE
                    && ViewState[ORGANIZATIONDETAILSDELETE] == null && ViewState[ORGANIZATIONDETAILSNONDELETE] == null)
                {
                  lblError.Text = "Please add experience summary to save the details.";
                  return;
                }
                if (gvOrganisation.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
                {
                  for (int i = 0; i < gvOrganisation.Rows.Count; i++)
                  {
                    objOrganisationDetails = new BusinessEntities.OrganisationDetails();

                    Label OrganisationId = (Label)gvOrganisation.Rows[i].FindControl(ORGANIZATIONID);
                    Label Mode = (Label)gvOrganisation.Rows[i].FindControl(MODE);
                    Label MonthSince = (Label)gvOrganisation.Rows[i].FindControl(MONTHSINCE);
                    Label YearSince = (Label)gvOrganisation.Rows[i].FindControl(YEARSINCE);
                    Label MonthTill = (Label)gvOrganisation.Rows[i].FindControl(MONTHTILL);
                    Label YearTill = (Label)gvOrganisation.Rows[i].FindControl(YEARTILL);
                    Label ExperienceMonth = (Label)gvOrganisation.Rows[i].FindControl(EXPERIENCEMONTH);
                    Label ExperienceYear = (Label)gvOrganisation.Rows[i].FindControl(EXPERIENCEYEAR);

                    objOrganisationDetails.OrganisationId = int.Parse(OrganisationId.Text);
                    objOrganisationDetails.EMPId = int.Parse(EMPId.Value);
                    objOrganisationDetails.CompanyName = gvOrganisation.Rows[i].Cells[0].Text;
                    objOrganisationDetails.MonthSince = int.Parse(MonthSince.Text);
                    objOrganisationDetails.YearSince = int.Parse(YearSince.Text);
                    objOrganisationDetails.MonthTill = int.Parse(MonthTill.Text);
                    objOrganisationDetails.YearTill = int.Parse(YearTill.Text);
                    objOrganisationDetails.Designation = gvOrganisation.Rows[i].Cells[1].Text;
                    objOrganisationDetails.Experience = gvOrganisation.Rows[i].Cells[4].Text;
                    objOrganisationDetails.ExperienceMonth = Convert.ToInt32(ExperienceMonth.Text);
                    objOrganisationDetails.ExperienceYear = Convert.ToInt32(ExperienceYear.Text);
                    ExperienceMonthTotal = ExperienceMonthTotal + Convert.ToInt32(ExperienceMonth.Text);
                    ExperienceYearTotal = ExperienceYearTotal + Convert.ToInt32(ExperienceYear.Text);
                    objOrganisationDetails.Mode = int.Parse(Mode.Text);
                    objOrganisationDetails.ExperienceType = Convert.ToInt32(MasterEnum.ExperienceType.Relevent);
                    objSaveOrganisationDetailsCollection.Add(objOrganisationDetails);
                  }
                }

                // 27637-Ambar-Start
                // Changed If condition
                // if (ExperienceMonthTotal != ReleventMonths || ReleventYears != ExperienceYearTotal)
                  //Mahendra commented temp fix
                //if (((ExperienceYearTotal * 12) + ExperienceMonthTotal) != (((ReleventYears * 12) + ReleventMonths)))
                if (((ExperienceYearTotal * 12) + ExperienceMonthTotal) > (((ReleventYears * 12) + ReleventMonths)))
                // 27637-Ambar-End
                {
                  lblError.Text = CommonConstants.EXPERIENCE_EQUAL_RELEVENTEXPERIENCE;
                  return;
                }

                BusinessEntities.RaveHRCollection objDeleteOrganisationDetailsCollection = (BusinessEntities.RaveHRCollection)ViewState[ORGANIZATIONDETAILSDELETE];

                if (objDeleteOrganisationDetailsCollection != null)
                {
                  BusinessEntities.OrganisationDetails obj = null;

                  for (int i = 0; i < objDeleteOrganisationDetailsCollection.Count; i++)
                  {
                    objOrganisationDetails = new BusinessEntities.OrganisationDetails();
                    obj = (BusinessEntities.OrganisationDetails)objDeleteOrganisationDetailsCollection.Item(i);

                    objOrganisationDetails.OrganisationId = obj.OrganisationId;
                    objOrganisationDetails.EMPId = obj.EMPId;
                    objOrganisationDetails.Mode = obj.Mode;

                    objSaveOrganisationDetailsCollection.Add(objOrganisationDetails);
                  }

                }

                if (rblNonReleventExperience.SelectedIndex == CommonConstants.ZERO)
                {
                  //int NonReleventExperience = TotalExperience - ReleventExperience;
                  if (gvOrgNonReleventDetails.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
                  {
                    for (int i = 0; i < gvOrgNonReleventDetails.Rows.Count; i++)
                    {
                      objOrganisationDetails = new BusinessEntities.OrganisationDetails();

                      Label OrganisationId = (Label)gvOrgNonReleventDetails.Rows[i].FindControl(ORGANIZATIONID);
                      Label Mode = (Label)gvOrgNonReleventDetails.Rows[i].FindControl(MODE);
                      Label MonthSince = (Label)gvOrgNonReleventDetails.Rows[i].FindControl(MONTHSINCE);
                      Label YearSince = (Label)gvOrgNonReleventDetails.Rows[i].FindControl(YEARSINCE);
                      Label MonthTill = (Label)gvOrgNonReleventDetails.Rows[i].FindControl(MONTHTILL);
                      Label YearTill = (Label)gvOrgNonReleventDetails.Rows[i].FindControl(YEARTILL);
                      Label ExperienceMonth = (Label)gvOrgNonReleventDetails.Rows[i].FindControl(EXPERIENCEMONTH);
                      Label ExperienceYear = (Label)gvOrgNonReleventDetails.Rows[i].FindControl(EXPERIENCEYEAR);

                      objOrganisationDetails.OrganisationId = int.Parse(OrganisationId.Text);
                      objOrganisationDetails.EMPId = int.Parse(EMPId.Value);
                      objOrganisationDetails.MonthSince = int.Parse(MonthSince.Text);
                      objOrganisationDetails.YearSince = int.Parse(YearSince.Text);
                      objOrganisationDetails.MonthTill = int.Parse(MonthTill.Text);
                      objOrganisationDetails.YearTill = int.Parse(YearTill.Text);
                      objOrganisationDetails.ExperienceMonth = Convert.ToInt32(ExperienceMonth.Text);
                      objOrganisationDetails.ExperienceYear = Convert.ToInt32(ExperienceYear.Text);
                      objOrganisationDetails.Mode = int.Parse(Mode.Text);
                      objOrganisationDetails.CompanyName = gvOrgNonReleventDetails.Rows[i].Cells[0].Text;
                      objOrganisationDetails.Designation = gvOrgNonReleventDetails.Rows[i].Cells[1].Text;
                      objOrganisationDetails.ExperienceType = Convert.ToInt32(MasterEnum.ExperienceType.NonRelevent);
                      NonExperienceMonthTotal = NonExperienceMonthTotal + Convert.ToInt32(ExperienceMonth.Text);
                      NonExperienceYearTotal = NonExperienceYearTotal + Convert.ToInt32(ExperienceYear.Text);

                      objSaveOrganisationDetailsCollection.Add(objOrganisationDetails);
                    }
                  }

                  BusinessEntities.RaveHRCollection objDeleteNonOrganisationDetailsCollection = (BusinessEntities.RaveHRCollection)ViewState[ORGANIZATIONDETAILSNONDELETE];

                  if (objDeleteNonOrganisationDetailsCollection != null)
                  {
                    BusinessEntities.OrganisationDetails obj = null;

                    for (int i = 0; i < objDeleteNonOrganisationDetailsCollection.Count; i++)
                    {
                      objOrganisationDetails = new BusinessEntities.OrganisationDetails();
                      obj = (BusinessEntities.OrganisationDetails)objDeleteNonOrganisationDetailsCollection.Item(i);

                      objOrganisationDetails.OrganisationId = obj.OrganisationId;
                      objOrganisationDetails.EMPId = obj.EMPId;
                      objOrganisationDetails.Mode = obj.Mode;

                      objSaveOrganisationDetailsCollection.Add(objOrganisationDetails);
                    }

                  }
                }
                else
                {
                  objOrganisationDetails = new BusinessEntities.OrganisationDetails();
                  objOrganisationDetails.EMPId = Convert.ToInt32(EMPId.Value);
                  objOrganisationDetails.ExperienceType = Convert.ToInt32(MasterEnum.ExperienceType.NonRelevent);

                  objOrganisationDetailsBAL.DeleteNonOrganisationDetails(objOrganisationDetails);
                }
                objOrganisationDetailsBAL.Manipulation(objSaveOrganisationDetailsCollection);

                if (ViewState.Count > 0)
                {
                  ViewState.Clear();
                }
                if (EMPId.Value != string.Empty)
                {
                  int empID = Convert.ToInt32(EMPId.Value);
                  //Refresh the grip after saving
                  this.PopulateGrid(empID);
                  this.PopulateNonRelevantGrid(empID);

                  if (!string.IsNullOrEmpty(txtReleventYears.Text) && !string.IsNullOrEmpty(txtReleventMonths.Text))
                  {
                    //Calucate the total experience in Months and Years i.e Relevant experience + Non relevant Experience.
                    int TotalYear = NonExperienceYearTotal + Convert.ToInt32(txtReleventYears.Text);
                    int TotalMonth = NonExperienceMonthTotal + Convert.ToInt32(txtReleventMonths.Text);

                     // int TotalYear = NonExperienceYearTotal + ExperienceYearTotal;
                     // int TotalMonth = NonExperienceMonthTotal + ExperienceMonthTotal;

                    TotalMonth = TotalYear * 12 + TotalMonth;
                    decimal months = TotalMonth / 12;
                    TotalYear = Convert.ToInt32(Math.Round(months));
                    TotalMonth = TotalMonth % 12;

                    txtTotalMonths.Text = TotalMonth.ToString();
                    txtTotalYears.Text = TotalYear.ToString();
                  }
                }

                if (gvOrganisation.Rows.Count == 0 && gvOrgNonReleventDetails.Rows.Count == 0)
                  btnSave.Visible = false;

                lblMessage.Text = "Organisation details saved successfully.";

                //HfIsDataModified.Value = string.Empty;
              }
            }
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
    /// Handles the RowDeleting event of the gvOrganisation control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewDeleteEventArgs"/> instance containing the event data.</param>
    protected void gvOrganisation_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int deleteRowIndex = 0;
            int rowIndex = -1;
            Boolean Flag = false;

            BusinessEntities.OrganisationDetails objOrganisationDetails = new BusinessEntities.OrganisationDetails();

            deleteRowIndex = e.RowIndex;

            objOrganisationDetails = (BusinessEntities.OrganisationDetails)OrganisationDetailsCollection.Item(deleteRowIndex);
            if (objOrganisationDetails.Mode == 1)
                Flag = true;
            objOrganisationDetails.Mode = 3;

            if (ViewState[ORGANIZATIONDETAILSDELETE] != null)
            {
                BusinessEntities.RaveHRCollection objDeleteOrganisationDetailsCollection = (BusinessEntities.RaveHRCollection)ViewState[ORGANIZATIONDETAILSDELETE];
                objDeleteOrganisationDetailsCollection.Add(objOrganisationDetails);

                ViewState[ORGANIZATIONDETAILSDELETE] = objDeleteOrganisationDetailsCollection;
            }
            else
            {
                BusinessEntities.RaveHRCollection objDeleteOrganisationDetailsCollection1 = new BusinessEntities.RaveHRCollection();

                objDeleteOrganisationDetailsCollection1.Add(objOrganisationDetails);

                ViewState[ORGANIZATIONDETAILSDELETE] = objDeleteOrganisationDetailsCollection1;
            }


            OrganisationDetailsCollection.RemoveAt(deleteRowIndex);

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

                ImageButton btnImg = (ImageButton)gvOrganisation.Rows[rowIndex].FindControl(IMGBTNDELETE);
                btnImg.Enabled = false;

                //Disabling all the edit buttons.
                for (int i = 0; i < gvOrganisation.Rows.Count; i++)
                {
                    if (rowIndex != i)
                    {
                        ImageButton btnImgEdit = (ImageButton)gvOrganisation.Rows[i].FindControl(IMGBTNEDIT);
                        btnImgEdit.Enabled = false;
                    }
                }
            }
            if (gvOrganisation.Rows[0].Cells[0].Text == NO_RECORDS_FOUND_MESSAGE && Flag == true)
            {
                btnAdd.Text = CommonConstants.BTN_Save;
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "gvOrganisation_RowDeleting", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the RowEditing event of the gvOrganisation control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewEditEventArgs"/> instance containing the event data.</param>
    protected void gvOrganisation_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            int rowIndex = 0;
            Label MonthSince = (Label)gvOrganisation.Rows[e.NewEditIndex].FindControl(MONTHSINCE);
            Label YearSince = (Label)gvOrganisation.Rows[e.NewEditIndex].FindControl(YEARSINCE);
            Label MonthTill = (Label)gvOrganisation.Rows[e.NewEditIndex].FindControl(MONTHTILL);
            Label YearTill = (Label)gvOrganisation.Rows[e.NewEditIndex].FindControl(YEARTILL);
            Label ExperienceMonth = (Label)gvOrganisation.Rows[e.NewEditIndex].FindControl(EXPERIENCEMONTH);
            Label ExperienceYear = (Label)gvOrganisation.Rows[e.NewEditIndex].FindControl(EXPERIENCEYEAR);

            ddlMonthSince.SelectedValue = MonthSince.Text;
            ddlYearSince.SelectedValue = YearSince.Text;
            ddlMonthTill.SelectedValue = MonthTill.Text;
            ddlYearTill.SelectedValue = YearTill.Text;
            txtExperienceMonth.Text = ExperienceMonth.Text;
            txtExperienceYear.Text = ExperienceYear.Text;

            txtCompanyName.Text = gvOrganisation.Rows[e.NewEditIndex].Cells[0].Text.Trim();
            txtPositionHeld.Text = gvOrganisation.Rows[e.NewEditIndex].Cells[1].Text.Trim();
            //txtExperience.Text = gvOrganisation.Rows[e.NewEditIndex].Cells[4].Text.Trim();
            //ExperienceValue = Convert.ToInt32(gvOrganisation.Rows[e.NewEditIndex].Cells[4].Text);


            ImageButton btnImg = (ImageButton)gvOrganisation.Rows[e.NewEditIndex].FindControl(IMGBTNDELETE);
            rowIndex = e.NewEditIndex;
            ViewState[ROWINDEX] = null;
            ViewState[ROWINDEX] = rowIndex;
            btnImg.Enabled = false;

            btnAdd.Visible = false;
            btnUpdate.Visible = true;
            btnCancel1.Visible = true;

            //Disabling all the edit buttons.
            for (int i = 0; i < gvOrganisation.Rows.Count; i++)
            {
                ImageButton btnImgEdit = (ImageButton)gvOrganisation.Rows[i].FindControl(IMGBTNEDIT);
                if (i != rowIndex)
                    btnImgEdit.Enabled = false;
            }

            //int Total = Convert.ToInt32(hfTotalExperience.Value) - ExperienceValue;
            //hfTotalExperience.Value = Total.ToString();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "gvOrganisation_RowEditing", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
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
                BusinessEntities.OrganisationDetails objOrganisationDetails = new BusinessEntities.OrganisationDetails();

                if (gvOrganisation.Rows[0].Cells[0].Text == NO_RECORDS_FOUND_MESSAGE)
                {
                    OrganisationDetailsCollection.Clear();
                }

                objOrganisationDetails.CompanyName = txtCompanyName.Text;
                objOrganisationDetails.Designation = txtPositionHeld.Text;
                objOrganisationDetails.WorkingSince = ddlMonthSince.SelectedItem.Text + "-" + ddlYearSince.SelectedItem.Text;
                objOrganisationDetails.WorkingTill = ddlMonthTill.SelectedItem.Text + "-" + ddlYearTill.SelectedItem.Text;
                objOrganisationDetails.MonthSince = Convert.ToInt32(ddlMonthSince.SelectedValue);
                objOrganisationDetails.YearSince = Convert.ToInt32(ddlYearSince.SelectedValue);
                objOrganisationDetails.MonthTill = Convert.ToInt32(ddlMonthTill.SelectedValue);
                objOrganisationDetails.YearTill = Convert.ToInt32(ddlYearTill.SelectedValue);
                objOrganisationDetails.Experience = txtExperienceYear.Text + " " + YEARS + "-" + txtExperienceMonth.Text + " " + MONTHS;
                objOrganisationDetails.ExperienceMonth = Convert.ToInt32(txtExperienceMonth.Text);
                objOrganisationDetails.ExperienceYear = Convert.ToInt32(txtExperienceYear.Text);

                objOrganisationDetails.Mode = 1;

                OrganisationDetailsCollection.Add(objOrganisationDetails);

                this.DoDataBind();

                this.ClearControls();

                if (gvOrganisation.Rows.Count != 0)
                    btnSave.Visible = true;

                //btnAdd.Text = CommonConstants.BTN_AddRow;
                //HfIsDataModified.Value = CommonConstants.YES;
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnAdd_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// Handles the Click event of the btnAddRow control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateNonReleventControls())
            {
                BusinessEntities.OrganisationDetails objOrganisationDetails = new BusinessEntities.OrganisationDetails();

                if (gvOrgNonReleventDetails.Rows[0].Cells[0].Text == NO_RECORDS_FOUND_MESSAGE)
                {
                    NonRelevantOrganisationDetailsCollection.Clear();
                }

                objOrganisationDetails.CompanyName = txtRCompanyName.Text;
                objOrganisationDetails.Designation = txtRPositionHeld.Text;
                objOrganisationDetails.WorkingSince = ddlRMonthSince.SelectedItem.Text + "-" + ddlRYearSince.SelectedItem.Text;
                objOrganisationDetails.WorkingTill = ddlRMonthTill.SelectedItem.Text + "-" + ddlRYearTill.SelectedItem.Text;
                objOrganisationDetails.MonthSince = Convert.ToInt32(ddlRMonthSince.SelectedValue);
                objOrganisationDetails.YearSince = Convert.ToInt32(ddlRYearSince.SelectedValue);
                objOrganisationDetails.MonthTill = Convert.ToInt32(ddlRMonthTill.SelectedValue);
                objOrganisationDetails.YearTill = Convert.ToInt32(ddlRYearTill.SelectedValue);
                objOrganisationDetails.Experience = txtRExperienceYear.Text + " " + YEARS + "-" + txtRExperienceMonth.Text + " " + MONTHS;
                objOrganisationDetails.ExperienceMonth = Convert.ToInt32(txtRExperienceMonth.Text);
                objOrganisationDetails.ExperienceYear = Convert.ToInt32(txtRExperienceYear.Text);
                objOrganisationDetails.Mode = 1;

                NonRelevantOrganisationDetailsCollection.Add(objOrganisationDetails);

                this.DoNonReleventDataBind();

                this.ClearNonReleventControls();

                if (gvOrgNonReleventDetails.Rows.Count != 0)
                    btnSave.Visible = true;

                //btnAddRow.Text = CommonConstants.BTN_AddRow;
                //HfIsDataModified.Value = CommonConstants.YES;
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnAddRow_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// Handles the Click event of the btnUpdateRow control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnUpdateRow_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateNonReleventControls())
            {
                int rowIndex = 0;
                int deleteRowIndex = -1;
                int total = 0;

                if (ViewState[DELETENONRELEVANTROWINDEX] != null)
                {
                    deleteRowIndex = Convert.ToInt32(ViewState[DELETENONRELEVANTROWINDEX].ToString());
                }

                //Update the grid view according the row, which is selected for editing.
                if (ViewState[NONRELEVANTROWINDEX] != null)
                {
                    rowIndex = Convert.ToInt32(ViewState[NONRELEVANTROWINDEX].ToString());
                    if (deleteRowIndex != -1 && deleteRowIndex < rowIndex) rowIndex--;

                    //Label OrganisationId = (Label)gvOrgNonReleventDetails.Rows[rowIndex].FindControl(ORGANIZATIONID);
                    //Label Mode = (Label)gvOrgNonReleventDetails.Rows[rowIndex].FindControl(MODE);
                    //Label MonthSince = (Label)gvOrgNonReleventDetails.Rows[rowIndex].FindControl(MONTHSINCE);
                    //Label YearSince = (Label)gvOrgNonReleventDetails.Rows[rowIndex].FindControl(YEARSINCE);
                    //Label MonthTill = (Label)gvOrgNonReleventDetails.Rows[rowIndex].FindControl(MONTHTILL);
                    //Label YearTill = (Label)gvOrgNonReleventDetails.Rows[rowIndex].FindControl(YEARTILL);


                    //MonthSince.Text = ddlRMonthSince.SelectedValue;
                    //YearSince.Text = ddlRYearSince.SelectedValue;
                    //MonthTill.Text = ddlRMonthTill.SelectedValue;
                    //YearTill.Text = ddlRYearTill.SelectedValue;
                    //ExperienceMonth.Text = txtExperienceMonth.Text;
                    //ExperienceYear.Text = txtExperienceYear.Text;
                    //gvOrgNonReleventDetails.Rows[rowIndex].Cells[0].Text = txtRCompanyName.Text;
                    //gvOrgNonReleventDetails.Rows[rowIndex].Cells[1].Text = ddlRMonthSince.SelectedItem.Text + "-" + ddlRYearSince.SelectedItem.Text;
                    //gvOrgNonReleventDetails.Rows[rowIndex].Cells[2].Text = ddlRMonthTill.SelectedItem.Text + "-" + ddlRYearTill.SelectedItem.Text;
                    //gvOrgNonReleventDetails.Rows[rowIndex].Cells[3].Text = txtRPositionHeld.Text;
                    //gvOrgNonReleventDetails.Rows[rowIndex].Cells[4].Text = txtRExperience.Text;

                    //if (int.Parse(OrganisationId.Text) == 0)
                    //{
                    //    Mode.Text = "1";
                    //}
                    //else
                    //{
                    //    Mode.Text = "2";
                    //}

                    ImageButton btnImg = (ImageButton)gvOrgNonReleventDetails.Rows[rowIndex].FindControl(IMGBTNDELETE);
                    btnImg.Enabled = true;
                    ViewState[NONRELEVANTROWINDEX] = null;
                    ViewState[DELETENONRELEVANTROWINDEX] = null;

                }

                for (int i = 0; i < NonRelevantOrganisationDetailsCollection.Count; i++)
                {
                    BusinessEntities.OrganisationDetails objOrganisationDetails = new BusinessEntities.OrganisationDetails();
                    objOrganisationDetails = (BusinessEntities.OrganisationDetails)NonRelevantOrganisationDetailsCollection.Item(i);

                    Label OrganisationId = (Label)gvOrgNonReleventDetails.Rows[i].FindControl(ORGANIZATIONID);
                    Label Mode = (Label)gvOrgNonReleventDetails.Rows[i].FindControl(MODE);
                    //Label MonthSince = (Label)gvOrgNonReleventDetails.Rows[i].FindControl(MONTHSINCE);
                    //Label YearSince = (Label)gvOrgNonReleventDetails.Rows[i].FindControl(YEARSINCE);
                    //Label MonthTill = (Label)gvOrgNonReleventDetails.Rows[i].FindControl(MONTHTILL);
                    //Label YearTill = (Label)gvOrgNonReleventDetails.Rows[i].FindControl(YEARTILL);
                    //Label ExperienceMonth = (Label)gvOrgNonReleventDetails.Rows[i].FindControl(EXPERIENCEMONTH);
                    //Label ExperienceYear = (Label)gvOrgNonReleventDetails.Rows[i].FindControl(EXPERIENCEYEAR);

                    //MonthSince.Text = ddlRMonthSince.SelectedValue;
                    //YearSince.Text = ddlRYearSince.SelectedValue;
                    //MonthTill.Text = ddlRMonthTill.SelectedValue;
                    //YearTill.Text = ddlRYearTill.SelectedValue;
                    //ExperienceMonth.Text = txtRExperienceMonth.Text;
                    //ExperienceYear.Text = txtRExperienceYear.Text;

                    if (int.Parse(OrganisationId.Text) == 0)
                    {
                        Mode.Text = "1";
                    }
                    else
                    {
                        Mode.Text = "2";
                    }
                    objOrganisationDetails.OrganisationId = int.Parse(OrganisationId.Text);
                    objOrganisationDetails.EMPId = int.Parse(EMPId.Value);
                    objOrganisationDetails.MonthSince = int.Parse(ddlRMonthSince.SelectedValue);
                    objOrganisationDetails.YearSince = int.Parse(ddlRYearSince.SelectedValue);
                    objOrganisationDetails.MonthTill = int.Parse(ddlRMonthTill.SelectedValue);
                    objOrganisationDetails.YearTill = int.Parse(ddlRYearTill.SelectedValue);
                    objOrganisationDetails.CompanyName = txtRCompanyName.Text.Trim();
                    objOrganisationDetails.Designation = txtRPositionHeld.Text.Trim();
                    objOrganisationDetails.Mode = int.Parse(Mode.Text);
                    objOrganisationDetails.WorkingSince = ddlRMonthSince.SelectedItem.Text + "-" + ddlRYearSince.SelectedItem.Text;
                    objOrganisationDetails.WorkingTill = ddlRMonthTill.SelectedItem.Text + "-" + ddlRYearTill.SelectedItem.Text;
                    objOrganisationDetails.Experience = txtRExperienceYear.Text + " " + YEARS + "-" + txtRExperienceMonth.Text + " " + MONTHS;
                    objOrganisationDetails.ExperienceMonth = int.Parse(txtRExperienceMonth.Text);
                    objOrganisationDetails.ExperienceYear = int.Parse(txtRExperienceYear.Text);
                    //total = total + Convert.ToInt32(gvOrgNonReleventDetails.Rows[i].Cells[4].Text);

                }

                hfTotalNonExperience.Value = total.ToString();

                this.DoNonReleventDataBind();

                //Clear all the fields after inserting row into gridview
                this.ClearNonReleventControls();

                btnAddRow.Visible = true;
                btnUpdateRow.Visible = false;
                btnCancelRow.Visible = false;

                //Enabling all the edit buttons.
                for (int i = 0; i < gvOrgNonReleventDetails.Rows.Count; i++)
                {
                    ImageButton btnImgEdit = (ImageButton)gvOrgNonReleventDetails.Rows[i].FindControl(IMGBTNEDIT);
                    btnImgEdit.Enabled = true;
                }
                //HfIsDataModified.Value = CommonConstants.YES;
            }
        }

        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnUpdateRow_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
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
        try
        {
            int rowIndex = 0;
            int deleteRowIndex = -1;

            //Clear all the fields after inserting row into gridview
            this.ClearNonReleventControls();

            if (ViewState[DELETENONRELEVANTROWINDEX] != null)
            {
                deleteRowIndex = Convert.ToInt32(ViewState[DELETENONRELEVANTROWINDEX].ToString());
            }

            if (ViewState[NONRELEVANTROWINDEX] != null)
            {
                rowIndex = Convert.ToInt32(ViewState[NONRELEVANTROWINDEX].ToString());
                if (deleteRowIndex != -1 && deleteRowIndex < rowIndex) rowIndex--;
                ImageButton btnImg = (ImageButton)gvOrgNonReleventDetails.Rows[rowIndex].FindControl(IMGBTNDELETE);
                btnImg.Enabled = true;
                ViewState[NONRELEVANTROWINDEX] = null;
                ViewState[DELETENONRELEVANTROWINDEX] = null;
            }

            //Enabling all the edit buttons.
            for (int i = 0; i < gvOrgNonReleventDetails.Rows.Count; i++)
            {
                ImageButton btnImgEdit = (ImageButton)gvOrgNonReleventDetails.Rows[i].FindControl(IMGBTNEDIT);
                btnImgEdit.Enabled = true;
            }

            btnAddRow.Visible = true;
            btnCancel.Visible = true;
            btnUpdateRow.Visible = false;
            btnCancelRow.Visible = false;

            int Total = Convert.ToInt32(hfTotalNonExperience.Value) + NonExperienceValue;
            hfTotalNonExperience.Value = Total.ToString();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnCancelRow_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the RowDeleting event of the gvOrgNonReleventDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewDeleteEventArgs"/> instance containing the event data.</param>
    protected void gvOrgNonReleventDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int deleteRowIndex = 0;
            int rowIndex = -1;
            Boolean Flag = false;

            BusinessEntities.OrganisationDetails objOrganisationDetails = new BusinessEntities.OrganisationDetails();

            deleteRowIndex = e.RowIndex;

            objOrganisationDetails = (BusinessEntities.OrganisationDetails)NonRelevantOrganisationDetailsCollection.Item(deleteRowIndex);
            if (objOrganisationDetails.Mode == 1)
                Flag = true;
            objOrganisationDetails.Mode = 3;

            if (ViewState[ORGANIZATIONDETAILSNONDELETE] != null)
            {
                BusinessEntities.RaveHRCollection objDeleteOrganisationDetailsCollection = (BusinessEntities.RaveHRCollection)ViewState[ORGANIZATIONDETAILSNONDELETE];
                objDeleteOrganisationDetailsCollection.Add(objOrganisationDetails);

                ViewState[ORGANIZATIONDETAILSNONDELETE] = objDeleteOrganisationDetailsCollection;
            }
            else
            {
                BusinessEntities.RaveHRCollection objDeleteOrganisationDetailsCollection1 = new BusinessEntities.RaveHRCollection();

                objDeleteOrganisationDetailsCollection1.Add(objOrganisationDetails);

                ViewState[ORGANIZATIONDETAILSNONDELETE] = objDeleteOrganisationDetailsCollection1;
            }

            NonRelevantOrganisationDetailsCollection.RemoveAt(deleteRowIndex);

            ViewState[DELETENONRELEVANTROWINDEX] = deleteRowIndex;

            DoNonReleventDataBind();

            if (ViewState[NONRELEVANTROWINDEX] != null)
            {
                rowIndex = Convert.ToInt32(ViewState[NONRELEVANTROWINDEX].ToString());
                //check edit index with deleted index if edit index is greater than or equal to delete index then rowindex decremented.
                if (rowIndex != -1 && deleteRowIndex <= rowIndex)
                {
                    rowIndex--;
                    //store the rowindex in viewstate.
                    ViewState[NONRELEVANTROWINDEX] = rowIndex;
                }

                ImageButton btnImg = (ImageButton)gvOrgNonReleventDetails.Rows[rowIndex].FindControl(IMGBTNDELETE);
                btnImg.Enabled = false;

                //Disabling all the edit buttons.
                for (int i = 0; i < gvOrgNonReleventDetails.Rows.Count; i++)
                {
                    if (rowIndex != i)
                    {
                        ImageButton btnImgEdit = (ImageButton)gvOrgNonReleventDetails.Rows[i].FindControl(IMGBTNEDIT);
                        btnImgEdit.Enabled = false;
                    }
                }
            }
            //if (gvOrgNonReleventDetails.Rows[0].Cells[0].Text == NO_RECORDS_FOUND_MESSAGE && Flag == true)
            //{
            //    btnAddRow.Text = CommonConstants.BTN_Save;
            //}
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "gvOrgNonReleventDetails_RowDeleting", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the RowEditing event of the gvOrgNonReleventDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewEditEventArgs"/> instance containing the event data.</param>
    protected void gvOrgNonReleventDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            int rowIndex = 0;
            Label MonthSince = (Label)gvOrgNonReleventDetails.Rows[e.NewEditIndex].FindControl(MONTHSINCE);
            Label YearSince = (Label)gvOrgNonReleventDetails.Rows[e.NewEditIndex].FindControl(YEARSINCE);
            Label MonthTill = (Label)gvOrgNonReleventDetails.Rows[e.NewEditIndex].FindControl(MONTHTILL);
            Label YearTill = (Label)gvOrgNonReleventDetails.Rows[e.NewEditIndex].FindControl(YEARTILL);
            Label ExperienceMonth = (Label)gvOrgNonReleventDetails.Rows[e.NewEditIndex].FindControl(EXPERIENCEMONTH);
            Label ExperienceYear = (Label)gvOrgNonReleventDetails.Rows[e.NewEditIndex].FindControl(EXPERIENCEYEAR);

            ddlRMonthSince.SelectedValue = MonthSince.Text;
            ddlRYearSince.SelectedValue = YearSince.Text;
            ddlRMonthTill.SelectedValue = MonthTill.Text;
            ddlRYearTill.SelectedValue = YearTill.Text;
            txtRExperienceMonth.Text = ExperienceMonth.Text;
            txtRExperienceYear.Text = ExperienceYear.Text;
            txtRCompanyName.Text = gvOrgNonReleventDetails.Rows[e.NewEditIndex].Cells[0].Text.Trim();
            txtRPositionHeld.Text = gvOrgNonReleventDetails.Rows[e.NewEditIndex].Cells[1].Text.Trim();
            //NonExperienceValue = Convert.ToInt32(gvOrgNonReleventDetails.Rows[e.NewEditIndex].Cells[4].Text);

            ImageButton btnImg = (ImageButton)gvOrgNonReleventDetails.Rows[e.NewEditIndex].FindControl(IMGBTNDELETE);
            rowIndex = e.NewEditIndex;
            ViewState[NONRELEVANTROWINDEX] = null;
            ViewState[NONRELEVANTROWINDEX] = rowIndex;
            btnImg.Enabled = false;

            btnAddRow.Visible = false;
            btnUpdateRow.Visible = true;
            btnCancelRow.Visible = true;

            //Disabling all the edit buttons.
            for (int i = 0; i < gvOrgNonReleventDetails.Rows.Count; i++)
            {
                ImageButton btnImgEdit = (ImageButton)gvOrgNonReleventDetails.Rows[i].FindControl(IMGBTNEDIT);
                if (i != rowIndex)
                    btnImgEdit.Enabled = false;
            }

            ////int Total = Convert.ToInt32(hfTotalNonExperience.Value) - NonExperienceValue;
            ////hfTotalNonExperience.Value = Total.ToString();
            HfIsDataModified.Value = CommonConstants.YES;
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "gvOrgNonReleventDetails_RowEditing", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
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
        try
        {
            Response.Redirect(CommonConstants.PAGE_SKILLSDETAILS);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnNext_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the Click event of the btnPrevious control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(CommonConstants.PAGE_CERTIFICATIONDETAILS);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnPrevious_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the Click event of the CancelDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void CancelDetails_Click(object sender, EventArgs e)
    {
        try
        {
            this.ClearControls();
            this.ClearNonReleventControls();
            if (Request.QueryString[PAGETYPE] != null)
            {
                if (DecryptQueryString(PAGETYPE) == PAGETYPEEMPLOYEESUMMERY)
                {
                    //Siddhesh - Handle error on cancel button click
                    //Response.Redirect("EmployeeDetails.aspx?" + URLHelper.SecureParameters("EmpId", employeeID.ToString()) + "&" + URLHelper.SecureParameters("index", Session["SelectedRow"].ToString()) + "&" + URLHelper.SecureParameters("pagetype", "EMPLOYEESUMMERY") + "&" + "&" + URLHelper.CreateSignature(employeeID.ToString(), Session["SelectedRow"].ToString(), "EMPLOYEESUMMERY"));
                    string strRedirect = "EmployeeDetails.aspx?" + URLHelper.SecureParameters("EmpId", employeeID.ToString()) + "&" + URLHelper.SecureParameters("index", Session["SelectedRow"].ToString()) + "&" + URLHelper.SecureParameters("pagetype", "EMPLOYEESUMMERY") + "&" + "&" + URLHelper.CreateSignature(employeeID.ToString(), Session["SelectedRow"].ToString(), "EMPLOYEESUMMERY");
                    Response.Redirect(strRedirect, false);
                    //Siddhesh - Handle error on cancel button click
                }
            }
            else
            {
                Response.Redirect(CommonConstants.PAGE_EMPLOYEEDETAILS, false);
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "CancelDetails_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlMonthSince control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlMonthSince_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Aarohi : Issue 31584 : 16/01/2012 : Start
        //if (ddlMonthSince.SelectedValue != MONTH && ddlYearSince.SelectedValue != YEAR && ddlMonthTill.SelectedValue != MONTH && ddlYearTill.SelectedValue != YEAR)
        //{
        //    arrOrganisationSince[gvOrganisation.Rows.Count] = ddlMonthSince.SelectedItem.Text + "-" + ddlYearSince.SelectedItem.Text;
        //    arrOrganisationTill[gvOrganisation.Rows.Count] = ddlMonthTill.SelectedItem.Text + "-" + ddlYearTill.SelectedItem.Text;
        //}
        //Aarohi : Issue 31584 : 16/01/2012 : End
        try
        {
            CalculateExperience(txtExperienceYear, txtExperienceMonth, ddlMonthSince, ddlYearSince, ddlMonthTill, ddlYearTill);
            ddlMonthSince.Focus();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlMonthSince_SelectedIndexChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlYearSince control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlYearSince_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Aarohi : Issue 31584 : 16/01/2012 : Start
        //if (ddlMonthSince.SelectedValue != MONTH && ddlYearSince.SelectedValue != YEAR && ddlMonthTill.SelectedValue != MONTH && ddlYearTill.SelectedValue != YEAR)
        //{
        //    arrOrganisationSince[gvOrganisation.Rows.Count] = ddlMonthSince.SelectedItem.Text + "-" + ddlYearSince.SelectedItem.Text;
        //    arrOrganisationTill[gvOrganisation.Rows.Count] = ddlMonthTill.SelectedItem.Text + "-" + ddlYearTill.SelectedItem.Text;
        //}
        //Aarohi : Issue 31584 : 16/01/2012 : End
        try
        {
            CalculateExperience(txtExperienceYear, txtExperienceMonth, ddlMonthSince, ddlYearSince, ddlMonthTill, ddlYearTill);
            ddlYearSince.Focus();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlYearSince_SelectedIndexChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlMonthTill control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlMonthTill_SelectedIndexChanged(object sender, EventArgs e)
    {
        ////Aarohi : Issue 31584 : 16/01/2012 : Start
        //if (ddlMonthSince.SelectedValue != MONTH && ddlYearSince.SelectedValue != YEAR && ddlMonthTill.SelectedValue != MONTH && ddlYearTill.SelectedValue != YEAR)
        //{
        //    arrOrganisationSince[gvOrganisation.Rows.Count] = ddlMonthSince.SelectedItem.Text + "-" + ddlYearSince.SelectedItem.Text;
        //    arrOrganisationTill[gvOrganisation.Rows.Count] = ddlMonthTill.SelectedItem.Text + "-" + ddlYearTill.SelectedItem.Text;
        //}
        ////Aarohi : Issue 31584 : 16/01/2012 : End
        try
        {
            CalculateExperience(txtExperienceYear, txtExperienceMonth, ddlMonthSince, ddlYearSince, ddlMonthTill, ddlYearTill);
            ddlMonthTill.Focus();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlMonthTill_SelectedIndexChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlYearTill control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlYearTill_SelectedIndexChanged(object sender, EventArgs e)
    {
        ////Aarohi : Issue 31584 : 16/01/2012 : Start
        //if (ddlMonthSince.SelectedValue != MONTH && ddlYearSince.SelectedValue != YEAR && ddlMonthTill.SelectedValue != MONTH && ddlYearTill.SelectedValue != YEAR)
        //{
        //    arrOrganisationSince[gvOrganisation.Rows.Count] = ddlMonthSince.SelectedItem.Text + "-" + ddlYearSince.SelectedItem.Text;
        //    arrOrganisationTill[gvOrganisation.Rows.Count] = ddlMonthTill.SelectedItem.Text + "-" + ddlYearTill.SelectedItem.Text;
        //}
        ////Aarohi : Issue 31584 : 16/01/2012 : End
        try
        {
            CalculateExperience(txtExperienceYear, txtExperienceMonth, ddlMonthSince, ddlYearSince, ddlMonthTill, ddlYearTill);
            ddlYearTill.Focus();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlYearTill_SelectedIndexChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlRMonthTill control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlRMonthTill_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            CalculateExperience(txtRExperienceYear, txtRExperienceMonth, ddlRMonthSince, ddlRYearSince, ddlRMonthTill, ddlRYearTill);
            ddlRMonthTill.Focus();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlRMonthTill_SelectedIndexChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlRYearTill control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlRYearTill_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            CalculateExperience(txtRExperienceYear, txtRExperienceMonth, ddlRMonthSince, ddlRYearSince, ddlRMonthTill, ddlRYearTill);
            ddlRYearTill.Focus();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlRYearTill_SelectedIndexChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlRMonthSince control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlRMonthSince_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            CalculateExperience(txtRExperienceYear, txtRExperienceMonth, ddlRMonthSince, ddlRYearSince, ddlRMonthTill, ddlRYearTill);
            ddlRMonthSince.Focus();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlRMonthSince_SelectedIndexChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlRYearSince control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlRYearSince_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            CalculateExperience(txtRExperienceYear, txtRExperienceMonth, ddlRMonthSince, ddlRYearSince, ddlRMonthTill, ddlRYearTill);
            ddlRYearSince.Focus();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlRYearSince_SelectedIndexChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the rblNonReleventExperience control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void rblNonReleventExperience_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rblNonReleventExperience.SelectedValue == TRUE)
                divNonReleventDetail.Visible = true;
            else
                divNonReleventDetail.Visible = false;

            //To solved the issue no 19221
            //Comment by Rahul P 
            //Start
            if (Session[SessionNames.PAGEMODE] != null)
            {
                PageMode = Session[SessionNames.PAGEMODE].ToString();

                if (PageMode == Common.MasterEnum.PageModeEnum.View.ToString())
                {
                    btnEdit.Visible = true;
                    btnSave.Visible = false;
                }
                else
                {
                    btnSave.Visible = true;
                    btnEdit.Visible = false;
                }
            }
        }  //End
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "rblNonReleventExperience_SelectedIndexChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
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
        try
        {
            Session[SessionNames.PAGEMODE] = Common.MasterEnum.PageModeEnum.Edit;
            btnEdit.Visible = false;
            btnSave.Visible = true;
            RelevantRow.Visible = true;
            Releventdetails.Visible = true;
            NonReleventDetails.Visible = true;
            //To solved the issue no 19221
            //Comment by Rahul P 
            //Start
            Releventdetails.Enabled = true;
            NonReleventDetails.Enabled = true;
            //Ishwar: Issue Id - 54410 30122014 Start
            rblNonReleventExperience.Enabled = true;
            //Ishwar: Issue Id - 54410 30122014 End
            //End
            if (gvOrganisation.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
            {
                //Enabling all the edit buttons.
                for (int i = 0; i < gvOrganisation.Rows.Count; i++)
                {
                    ImageButton btnImgEdit = (ImageButton)gvOrganisation.Rows[i].FindControl(IMGBTNEDIT);
                    btnImgEdit.Enabled = true;
                    ImageButton btnImgDelete = (ImageButton)gvOrganisation.Rows[i].FindControl(IMGBTNDELETE);
                    btnImgDelete.Enabled = true;
                }
            }

            if (gvOrgNonReleventDetails.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
            {
                //Enabling all the edit buttons.
                for (int i = 0; i < gvOrgNonReleventDetails.Rows.Count; i++)
                {
                    ImageButton btnImgEdit = (ImageButton)gvOrgNonReleventDetails.Rows[i].FindControl(IMGBTNEDIT);
                    btnImgEdit.Enabled = true;
                    ImageButton btnImgDelete = (ImageButton)gvOrgNonReleventDetails.Rows[i].FindControl(IMGBTNDELETE);
                    btnImgDelete.Enabled = true;
                }
            }

            //28109-Ambar-Start
            //if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR) || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM))

            //Siddharth 1st April 2015
            //Only HR should be able to update Relevant Months and Years
            if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
            {
                //Mahendra Temp Fix 28109 STRAT
                //txtTotalMonths.Enabled = true;
                //txtTotalYears.Enabled = true;
                //Mahendra Temp Fix 28109 END
                txtReleventMonths.Enabled = true;
                txtReleventYears.Enabled = true;

                ITotalMonths = Convert.ToInt32(txtTotalMonths.Text.ToString());
                ITotalYears = Convert.ToInt32(txtTotalYears.Text.ToString());
                IReleventMonths = Convert.ToInt32(txtReleventMonths.Text.ToString());
                IReleventYears = Convert.ToInt32(txtReleventYears.Text.ToString());
            }
            //28109-Ambar-End
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnEdit_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    #endregion

    #region Private Member Functions

    //Mahendra Temp Fix 28109 STRAT
    /// <summary>
    /// calculate Rave Experience
    /// </summary>
    /// <param name="employee"></param>
    private void CalculateRaveExperience(BusinessEntities.Employee employee)
    {
        DateDifference dateDifference;

        if (employee.StatusId == 143)
        {
            dateDifference = new DateDifference(employee.LastWorkingDay, employee.JoiningDate);
        }
        else
        {
            dateDifference = new DateDifference(DateTime.Now, employee.JoiningDate);
        }
        txtRaveExperienceYear.Text = dateDifference.Years.ToString();
        txtRaveExperienceMonths.Text = dateDifference.Months.ToString();
        int totalMonth = 0;
        int totalYear = 0;

        totalMonth = (Convert.ToInt32(txtReleventMonths.Text) + Convert.ToInt32(txtRaveExperienceMonths.Text));
        totalYear = (Convert.ToInt32(txtReleventYears.Text) + Convert.ToInt32(txtRaveExperienceYear.Text));

        totalMonth = totalYear * 12 + totalMonth;
        decimal months = totalMonth / 12;
        totalYear = Convert.ToInt32(Math.Round(months));
        totalMonth = totalMonth % 12;

        txtTotalRaveExternalRelevantMonths.Text = totalMonth.ToString();
        txtTotalRaveExternalRelevantYear.Text = totalYear.ToString();
    }
    //Mahendra Temp Fix 28109 END

    /// <summary>
    /// Does the data bind.
    /// </summary>
    private void DoDataBind()
    {
        gvOrganisation.DataSource = OrganisationDetailsCollection;
        gvOrganisation.DataBind();

        //Displaying grid header with NO record found message.
        if (gvOrganisation.Rows.Count == 0)
            ShowHeaderWhenEmptyRelevantGrid();

        int total = 0;
        //if (gvOrganisation.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
        //{
        //    for (int i = 0; i < gvOrganisation.Rows.Count; i++)
        //    {
        //        total = total + Convert.ToInt32(gvOrganisation.Rows[i].Cells[4].Text);

        //    }
        //}
        //hfTotalExperience.Value = total.ToString();

    }

    /// <summary>
    /// Does the data bind.
    /// </summary>
    private void DoNonReleventDataBind()
    {
        gvOrgNonReleventDetails.DataSource = NonRelevantOrganisationDetailsCollection;
        gvOrgNonReleventDetails.DataBind();

        //Displaying grid header with NO record found message.
        if (gvOrgNonReleventDetails.Rows.Count == 0)
            ShowHeaderWhenEmptyGrid();
       
    }

    /// <summary>
    /// Clears the controls.
    /// </summary>
    private void ClearControls()
    {
        txtCompanyName.Text = string.Empty;
        //txtExperience.Text = string.Empty;
        txtPositionHeld.Text = string.Empty;
        ddlMonthSince.SelectedIndex = 0;
        ddlYearSince.SelectedIndex = 0;
        ddlMonthTill.SelectedIndex = 0;
        ddlYearTill.SelectedIndex = 0;
        txtExperienceMonth.Text = string.Empty;
        txtExperienceYear.Text = string.Empty;
    }

    /// <summary>
    /// Clears the Non Relevent Grid controls.
    /// </summary>
    private void ClearNonReleventControls()
    {
        txtRCompanyName.Text = string.Empty;
        txtRExperienceYear.Text = string.Empty;
        txtRExperienceMonth.Text = string.Empty;
        txtRPositionHeld.Text = string.Empty;
        ddlRMonthSince.SelectedIndex = 0;
        ddlRYearSince.SelectedIndex = 0;
        ddlRMonthTill.SelectedIndex = 0;
        ddlRYearTill.SelectedIndex = 0;
    }

    /// <summary>
    /// Populates the grid.
    /// </summary>
    private int PopulateGrid(int employeeID)
    {
        int i = 0;
        OrganisationDetailsCollection = this.GetOrganisationDetails(Convert.ToInt32(MasterEnum.ExperienceType.Relevent), employeeID);
        //Binding the datatable to grid
        gvOrganisation.DataSource = OrganisationDetailsCollection;
        gvOrganisation.DataBind();
        //To solved the issue no 19221
        //Comment by Rahul P 
        //Start
        i = gvOrganisation.Rows.Count;
        //End
        //Displaying grid header with NO record found message.
        if (gvOrganisation.Rows.Count == 0)
        {
            ShowHeaderWhenEmptyRelevantGrid();
            
        }
        else
        {
            btnAdd.Text = CommonConstants.BTN_AddRow;
        }

        EMPId.Value = employeeID.ToString().Trim();

        //int total = 0;
        //if (gvOrganisation.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
        //{

        //    for (int i = 0; i < gvOrganisation.Rows.Count; i++)
        //    {
        //        total = total + Convert.ToInt32(gvOrganisation.Rows[i].Cells[4].Text);

        //    }
        //}
        //hfTotalExperience.Value = total.ToString();
        
        return i;
    }

    /// <summary>
    /// Populates the non relevant grid.
    /// </summary>
    /// <param name="employeeID">The employee ID.</param>
    private void PopulateNonRelevantGrid(int employeeID)
    {
        NonRelevantOrganisationDetailsCollection = this.GetOrganisationDetails(Convert.ToInt32(MasterEnum.ExperienceType.NonRelevent), employeeID);

        //Binding the datatable to grid
        gvOrgNonReleventDetails.DataSource = NonRelevantOrganisationDetailsCollection;
        gvOrgNonReleventDetails.DataBind();

        //Displaying grid header with NO record found message.
        if (gvOrgNonReleventDetails.Rows.Count == 0)
        {
            ShowHeaderWhenEmptyGrid();
            rblNonReleventExperience.SelectedIndex = 1;
            rblNonReleventExperience_SelectedIndexChanged(null, null);
        }
        else
        {
            rblNonReleventExperience.SelectedIndex = 0;
            rblNonReleventExperience_SelectedIndexChanged(null, null);
        }
        int totalMonths = 0;
        int totalYears = 0;
        //int ExperienceMonthTotal = 0;
        //int ExperienceYearTotal = 0;

        //if (gvOrganisation.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
        //{
        //    for (int i = 0; i < gvOrganisation.Rows.Count; i++)
        //    {
        //        Label ExperienceMonth = (Label)gvOrganisation.Rows[i].FindControl(EXPERIENCEMONTH);
        //        Label ExperienceYear = (Label)gvOrganisation.Rows[i].FindControl(EXPERIENCEYEAR);
        //        ExperienceMonthTotal = ExperienceMonthTotal + Convert.ToInt32(ExperienceMonth.Text);
        //        ExperienceYearTotal = ExperienceYearTotal + Convert.ToInt32(ExperienceYear.Text);
        //    }
        //    totalMonths = totalMonths + ExperienceMonthTotal;
        //    totalYears = totalYears + ExperienceYearTotal;
        //}


        if (gvOrgNonReleventDetails.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
        {
            for (int i = 0; i < gvOrgNonReleventDetails.Rows.Count; i++)
            {
                Label ExperienceMonth = (Label)gvOrgNonReleventDetails.Rows[i].FindControl(EXPERIENCEMONTH);
                Label ExperienceYear = (Label)gvOrgNonReleventDetails.Rows[i].FindControl(EXPERIENCEYEAR);
                totalMonths = totalMonths + Convert.ToInt32(ExperienceMonth.Text);
                totalYears = totalYears + Convert.ToInt32(ExperienceYear.Text);
            }
        }
        if (!string.IsNullOrEmpty(txtReleventYears.Text) && !string.IsNullOrEmpty(txtReleventMonths.Text))
        {
            totalYears = totalYears + Convert.ToInt32(txtReleventYears.Text);
            totalMonths = totalMonths + Convert.ToInt32(txtReleventMonths.Text);

            totalMonths = totalYears * 12 + totalMonths;
            decimal months = totalMonths / 12;
            totalYears = Convert.ToInt32(Math.Round(months));
            totalMonths = totalMonths % 12;
        }
        txtTotalMonths.Text = totalMonths.ToString();
        txtTotalYears.Text = totalYears.ToString();

    }

    /// <summary>
    /// Gets the professional details.
    /// </summary>
    /// <returns></returns>
    private BusinessEntities.RaveHRCollection GetOrganisationDetails(int ExperienceType, int employeeID)
    {
        Rave.HR.BusinessLayer.Employee.OrganisationDetails objOrganisationDetailsBAL;
        BusinessEntities.OrganisationDetails objOrganisationDetails;

        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            objOrganisationDetailsBAL = new Rave.HR.BusinessLayer.Employee.OrganisationDetails();
            objOrganisationDetails = new BusinessEntities.OrganisationDetails();

            objOrganisationDetails.EMPId = employeeID;
            objOrganisationDetails.ExperienceType = ExperienceType;

            raveHRCollection = objOrganisationDetailsBAL.GetOrganisationDetails(objOrganisationDetails);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetOrganisationDetails", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
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

        DateTime WorkingSinceDate = new DateTime(Convert.ToInt32(ddlYearSince.SelectedValue), Convert.ToInt32(ddlMonthSince.SelectedValue), 1);
        DateTime WorkingTillDate = new DateTime(Convert.ToInt32(ddlYearTill.SelectedValue), Convert.ToInt32(ddlMonthTill.SelectedValue), 1);

        if (WorkingSinceDate > DateTime.Now.Date)
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.REL_WORKINGSINCE_VALIDATION);
            flag = false;
        }

        if (WorkingTillDate > DateTime.Now.Date)
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.REL_WORKINGTILL_VALIDATION);
            flag = false;
        }

        if (string.IsNullOrEmpty(txtCompanyName.Text.Trim()))
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.SPACES_VALIDATION);
            flag = false;
        }

        if (string.IsNullOrEmpty(txtCompanyName.Text.Trim()))
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.SPACES_VALIDATION);
            flag = false;
        }

        if (WorkingTillDate < WorkingSinceDate)
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.RELWORKINGTILL_LESSTHAN_WORKINGSINCE);
            flag = false;
        }

        //if (txtReleventMonths.Text != string.Empty && txtReleventYears.Text != string.Empty)
        //{
        //    int ReleventMonthExperience = Convert.ToInt32(txtReleventMonths.Text);
        //    int ReleventYearExperience = Convert.ToInt32(txtReleventYears.Text);
        //    int ExperienceYear = Convert.ToInt32(txtExperienceYear.Text);
        //    int ExperienceMonth = Convert.ToInt32(txtExperienceMonth.Text);
        //    int TotalMonthExperience = 0;
        //    int TotalYearExperience = 0;

        //    if (ExperienceYear > ReleventYearExperience)
        //    {
        //        errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.RELEVENTEXPERIENCE_VALIDATION);
        //        flag = false;
        //        lblError.Text = CommonConstants.RELEVENTEXPERIENCE_VALIDATION;
        //    }
        //    if (ExperienceMonth > ReleventMonthExperience)
        //    {
        //        errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.RELEVENTEXPERIENCE_VALIDATION);
        //        flag = false;
        //        lblError.Text = CommonConstants.RELEVENTEXPERIENCE_VALIDATION;
        //    }

            //if (flag)
            //{
            //    if (hfExperienceTotalMonths.Value == string.Empty && hfExperienceTotalYears.Value == string.Empty)
            //    {
            //        TotalMonthExperience = ExperienceMonth;
            //        TotalYearExperience = ExperienceYear;
            //        hfExperienceTotalMonths.Value = TotalMonthExperience.ToString();
            //        hfExperienceTotalYears.Value = TotalYearExperience.ToString();
            //    }
            //    else
            //    {
            //        TotalMonthExperience = Convert.ToInt32(hfExperienceTotalMonths.Value) + ExperienceMonth;
            //        TotalYearExperience = Convert.ToInt32(hfExperienceTotalYears.Value) + ExperienceYear;
            //        hfExperienceTotalMonths.Value = TotalMonthExperience.ToString();
            //    }
            //    if (TotalMonthExperience > ReleventMonthExperience)
            //    {
            //        //lblError.Text = CommonConstants.RELEVENTEXPERIENCE_GRID_VALIDATION;
            //        errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.RELEVENTEXPERIENCE_GRID_VALIDATION);
            //        flag = false;
            //    }
            //    if (TotalYearExperience > ReleventYearExperience)
            //    {
            //        //lblError.Text = CommonConstants.RELEVENTEXPERIENCE_GRID_VALIDATION;
            //        errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.RELEVENTEXPERIENCE_GRID_VALIDATION);
            //        flag = false;
            //    }

            //    if (!flag)
            //    {
            //        hfExperienceTotalMonths.Value = Convert.ToString(TotalMonthExperience - ExperienceMonth);
            //        hfExperienceTotalYears.Value = Convert.ToString(TotalYearExperience - ExperienceYear);
            //    }
            //}
        //}
        lblError.Text = errMessage.ToString();
        return flag;
    }

    /// <summary>
    /// Validates the controls.
    /// </summary>
    /// <returns></returns>
    private bool ValidateNonReleventControls()
    {
        StringBuilder errMessage = new StringBuilder();
        bool flag = true;

        DateTime WorkingSinceDate = new DateTime(Convert.ToInt32(ddlRYearSince.SelectedValue), Convert.ToInt32(ddlRMonthSince.SelectedValue), 1);
        DateTime WorkingTillDate = new DateTime(Convert.ToInt32(ddlRYearTill.SelectedValue), Convert.ToInt32(ddlRMonthTill.SelectedValue), 1);


        if (WorkingSinceDate > DateTime.Now.Date)
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.NONREL_WORKINGSINCE_VALIDATION);
            flag = false;
        }

        if (WorkingTillDate > DateTime.Now.Date)
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.NONREL_WORKINGTILL_VALIDATION);
            flag = false;
        }

        if (WorkingTillDate < WorkingSinceDate)
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.NONRELWORKINGTILL_LESSTHAN_WORKINGSINCE);
            flag = false;
        }

        lblError.Text = errMessage.ToString();
        return flag;

    }

    /// <summary>
    /// Gets the months.
    /// </summary>
    private void GetMonths(DropDownList ddlMonthValue)
    {

        ddlMonthValue.Items.Insert(CommonConstants.ZERO, MONTH);

        ddlMonthValue.Items.Insert(1, new ListItem(Common.MasterEnum.Months.January.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.January)));
        ddlMonthValue.Items.Insert(2, new ListItem(Common.MasterEnum.Months.February.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.February)));
        ddlMonthValue.Items.Insert(3, new ListItem(Common.MasterEnum.Months.March.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.March)));
        ddlMonthValue.Items.Insert(4, new ListItem(Common.MasterEnum.Months.April.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.April)));
        ddlMonthValue.Items.Insert(5, new ListItem(Common.MasterEnum.Months.May.ToString(),
                                                 Convert.ToString((int)Common.MasterEnum.Months.May)));
        ddlMonthValue.Items.Insert(6, new ListItem(Common.MasterEnum.Months.June.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.June)));
        ddlMonthValue.Items.Insert(7, new ListItem(Common.MasterEnum.Months.July.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.July)));
        ddlMonthValue.Items.Insert(8, new ListItem(Common.MasterEnum.Months.August.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.August)));
        ddlMonthValue.Items.Insert(9, new ListItem(Common.MasterEnum.Months.September.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.September)));
        ddlMonthValue.Items.Insert(10, new ListItem(Common.MasterEnum.Months.October.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.October)));
        ddlMonthValue.Items.Insert(11, new ListItem(Common.MasterEnum.Months.November.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.November)));
        ddlMonthValue.Items.Insert(12, new ListItem(Common.MasterEnum.Months.December.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.December)));

    }

    /// <summary>
    /// Gets the years.
    /// </summary>
    private void GetYears(DropDownList ddlYearValue)
    {
        ddlYearValue.Items.Insert(CommonConstants.ZERO, YEAR);

        int startIndex = 1;
        for (int i = DateTime.Now.Year; i >= DateTime.Now.Year-60; i--)
        {
            ddlYearValue.Items.Insert(startIndex, new ListItem(i.ToString(), i.ToString()));

            startIndex++;
        }
    }

    /// <summary>
    /// Sorts the DDL.
    /// </summary>
    /// <param name="objDDL">The obj DDL.</param>
    private void SortDDL(ref DropDownList objDDL)
    {
        ArrayList textList = new ArrayList();
        ArrayList valueList = new ArrayList();


        foreach (ListItem li in objDDL.Items)
        {
            valueList.Add(li.Value);
        }

        valueList.Sort();


        foreach (object item in valueList)
        {
            string Text = objDDL.Items.FindByValue(item.ToString()).Text;
            textList.Add(Text);
        }
        objDDL.Items.Clear();

        for (int i = 0; i < valueList.Count; i++)
        {
            ListItem objItem = new ListItem(textList[i].ToString(), valueList[i].ToString());
            objDDL.Items.Add(objItem);
        }
    }

    /// <summary>
    /// Display Header When GridView Empty with proper message
    /// </summary>
    /// <param name="objListSort">EmptyList</param>
    private void ShowHeaderWhenEmptyRelevantGrid()
    {
        try
        {
            //set header visible
            gvOrganisation.ShowHeader = true;

            //Create empty datasource for Grid view and bind
            OrganisationDetailsCollection.Add(new BusinessEntities.OrganisationDetails());
            gvOrganisation.DataSource = OrganisationDetailsCollection;
            gvOrganisation.DataBind();

            //Calculate number of columns in Grid view used for column Span
            int columnsCount = gvOrganisation.Columns.Count;

            //clear all the cells in the row
            gvOrganisation.Rows[0].Cells.Clear();

            //add a new blank cell
            gvOrganisation.Rows[0].Cells.Add(new TableCell());
            gvOrganisation.Rows[0].Cells[0].Text = NO_RECORDS_FOUND_MESSAGE;
            gvOrganisation.Rows[0].Cells[0].Wrap = false;
            gvOrganisation.Rows[0].Cells[0].Width = Unit.Percentage(10);

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
    /// Display Header When GridView Empty with proper message
    /// </summary>
    /// <param name="objListSort">EmptyList</param>
    private void ShowHeaderWhenEmptyGrid()
    {
        try
        {
            //set header visible
            gvOrgNonReleventDetails.ShowHeader = true;

            //Create empty datasource for Grid view and bind
            NonRelevantOrganisationDetailsCollection.Add(new BusinessEntities.OrganisationDetails());
            gvOrgNonReleventDetails.DataSource = NonRelevantOrganisationDetailsCollection;
            gvOrgNonReleventDetails.DataBind();

            //Calculate number of columns in Grid view used for column Span
            int columnsCount = gvOrgNonReleventDetails.Columns.Count;

            //clear all the cells in the row
            gvOrgNonReleventDetails.Rows[0].Cells.Clear();

            //add a new blank cell
            gvOrgNonReleventDetails.Rows[0].Cells.Add(new TableCell());
            gvOrgNonReleventDetails.Rows[0].Cells[0].Text = NO_RECORDS_FOUND_MESSAGE;
            gvOrgNonReleventDetails.Rows[0].Cells[0].Wrap = false;
            gvOrgNonReleventDetails.Rows[0].Cells[0].Width = Unit.Percentage(10);

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
        try
        {
            int i = 0;
            //this.PopulateControls();
            if (Session[SessionNames.EMPLOYEEDETAILS] != null)
            {
                employee = (BusinessEntities.Employee)Session[SessionNames.EMPLOYEEDETAILS];
                employee = this.GetEmployee(employee);

                lblempName.Text = employee.FirstName.ToUpper() + " " + employee.LastName.ToUpper();

                this.GetMonths(ddlMonthSince);
                this.GetMonths(ddlMonthTill);
                this.GetMonths(ddlRMonthSince);
                this.GetMonths(ddlRMonthTill);
                this.GetYears(ddlYearSince);
                this.GetYears(ddlYearTill);
                this.GetYears(ddlRYearSince);
                this.GetYears(ddlRYearTill);
                this.GetRelevantExperience(employee.EMPId);
                //To solved the issue no 19221
                //Comment by Rahul P 
                //Start
                i = this.PopulateGrid(employee.EMPId);
                //End
                this.PopulateNonRelevantGrid(employee.EMPId);
                //this.PopulateControls();

                // Mohamed : NIS-RMS : 05/01/2015 : Starts                        			  
                // Desc : Calculate Rave Experience
                this.CalculateRaveExperience(employee);
                // Mohamed : NIS-RMS : 05/01/2015 : Ends
            }
            //To solved the issue no 19221
            //Comment by Rahul P 
            //Start
            if (i == 0)
            {
                Releventdetails.Enabled = true;
                NonReleventDetails.Enabled = true;
                //Ishwar: Issue Id - 54410 30122014 Start
                rblNonReleventExperience.Enabled = true;
                //Ishwar: Issue Id - 54410 30122014 End
                btnSave.Visible = true;
                //btnCancel1.Visible = true;
                btnEdit.Visible = false;
                RelevantRow.Visible = true;
            }
            else
            {
                btnSave.Visible = false;
                //btnCancel1.Visible = true;
                btnEdit.Visible = true;
                Releventdetails.Enabled = false;
                NonReleventDetails.Enabled = false;
                RelevantRow.Visible = false;
                //Ishwar: Issue Id - 54410 30122014 Start
                rblNonReleventExperience.Enabled = false;
                //Ishwar: Issue Id - 54410 30122014 End
            }
            //End

            //Ishwar: Issue Id - 54410 : 31122014 Start
            if (UserMailId.ToLower() != employee.EmailId.ToLower() && arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEPM))
            {
                btnEdit.Visible = false;
                btnCancel.Visible = true;
                btnSave.Visible = false;
                btnAddRow.Visible = false;
                Releventdetails.Enabled = false;
                NonReleventDetails.Enabled = false;
                rblNonReleventExperience.Enabled = false;
            }
            //Ishwar: Issue Id - 54410 : 31122014 Ends

            Session[Common.SessionNames.EMPLOYEEDETAILS] = employee;
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "BubbleControl_BubbleClick", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
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

    /// <summary>
    /// Calculates the experience.
    /// </summary>
    public void CalculateExperience(TextBox ExperienceYear, TextBox ExperienceMonth, DropDownList ddlMonthSince, DropDownList ddlYearSince, DropDownList ddlMonthTill, DropDownList ddlYearTill)
    {
        if (ddlMonthSince.SelectedValue != MONTH && ddlYearSince.SelectedValue != YEAR && ddlMonthTill.SelectedValue != MONTH && ddlYearTill.SelectedValue != YEAR)
        {
            int MonthSince = Convert.ToInt32(ddlMonthSince.SelectedValue);
            int YearSince = Convert.ToInt32(ddlYearSince.SelectedValue);
            int MonthTill = Convert.ToInt32(ddlMonthTill.SelectedValue);
            int YearTill = Convert.ToInt32(ddlYearTill.SelectedValue);
            int Months = 0;

            int Years = Math.Abs(YearSince - YearTill);

            ////Aarohi : Issue 31584 : 20/01/2012 : Start
            //// Commented out the earlier logic to calculate experience and implemented new one
            if (MonthTill < MonthSince)
            {
              Years = Math.Abs(Years - 1);
              Months = 12 - Math.Abs(MonthTill - MonthSince) + 1;

            }
            else if (MonthTill >= MonthSince)
            {
              Months = Math.Abs(MonthSince - MonthTill) + 1;
            }

            if (Months == 12)
            {
              Years = Years + 1;
              Months = 0;
            }
            
            //int count = 0;
            //for (int i = 0; i < arrOrganisationSince.Length; i++)
            //{
            //    if (arrOrganisationSince[i] == ddlMonthSince.SelectedItem.Text + "-" + ddlYearSince.SelectedItem.Text ||
            //        arrOrganisationSince[i] == ddlMonthTill.SelectedItem.Text + "-" + ddlYearTill.SelectedItem.Text ||
            //        arrOrganisationTill[i] == ddlMonthSince.SelectedItem.Text + "-" + ddlYearSince.SelectedItem.Text ||
            //        arrOrganisationTill[i] == ddlMonthTill.SelectedItem.Text + "-" + ddlYearTill.SelectedItem.Text)
            //    {
            //        count++;
            //    }
            //}
            //if (MonthTill < MonthSince)
            //{
            //    if (count >= 1)
            //    {
            //        Years = Math.Abs(Years - 1);
            //        Months = 12 - Math.Abs(MonthTill - MonthSince);
            //    }
            //    else
            //    {
            //        Years = Math.Abs(Years - 1);
            //        Months = 12 - Math.Abs(MonthTill - MonthSince) + 1;
            //    }
            //}
            //else if (MonthTill == MonthSince)
            //{
            //    if (count == 1)
            //    {
            //        Months = Math.Abs(MonthSince - MonthTill) + 1;
            //    }               
            //}
            //else if (MonthTill > MonthSince)
            //{
            //    if (count >= 1)
            //    {
            //        Months = Math.Abs(MonthSince - MonthTill);
            //    }
            //    else
            //    {
            //        Months = Math.Abs(MonthSince - MonthTill) + 1;
            //    }
            //}
            //if (Months == 12)
            //{
            //    if (count >= 1)
            //    {
            //        Months = 0;
            //    }
            //    else
            //    {
            //        Years = Years + 1;
            //        Months = 0;
            //    }
            //}
            ////Aarohi : Issue 31584 : 16/01/2012 : End
            ExperienceYear.Text = Years.ToString();
            ExperienceMonth.Text = Months.ToString();
        }
    }

    /// <summary>
    /// Gets the relevant experience.
    /// </summary>
    /// <param name="employeeID">The employee ID.</param>
    /// <returns></returns>
    private void GetRelevantExperience(int employeeID)
    {
        Rave.HR.BusinessLayer.Employee.OrganisationDetails objOrganisationDetailsBAL;
        BusinessEntities.OrganisationDetails objOrganisationDetails;

        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            objOrganisationDetailsBAL = new Rave.HR.BusinessLayer.Employee.OrganisationDetails();
            objOrganisationDetails = new BusinessEntities.OrganisationDetails();

            objOrganisationDetails.EMPId = employeeID;

            raveHRCollection = objOrganisationDetailsBAL.GetRelevantExperience(objOrganisationDetails);

            foreach (BusinessEntities.OrganisationDetails OrganisationDetails in raveHRCollection)
            {
                txtReleventYears.Text = OrganisationDetails.ExperienceYear.ToString();
                txtReleventMonths.Text = OrganisationDetails.ExperienceMonth.ToString();
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetRelevantExperience", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    #endregion

    
}
