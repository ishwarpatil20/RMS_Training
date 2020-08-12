//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           EmpOrganization.aspx.cs    
//  Author:         Shrinivas.Dalavi
//  Date written:   21/8/2009/ 12:01:00 PM
//  Description:    This Page will be the entry page for adding Organization details
//                  same page will use in View Organization details & Edit Organization details mode
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
using System.Web.UI.HtmlControls;
using Common.AuthorizationManager;
using System.Collections;


public partial class EmpOrganization : BaseClass
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
    string PreviousPage;
    AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
    // Get logged-in user's email id
    AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
    ArrayList arrRolesForUser = new ArrayList();
    string MONTHS = "Months";
    string YEARS = "Years";
    private string MONTHSINCE = "MonthSince";
    private string YEARSINCE = "YearSince";
    private string MONTHTILL = "MonthTill";
    private string YEARTILL = "YearTill";
    /// <summary>
    /// Defines a constant error for no records found after applying filter criteria
    /// </summary>
    private const string NO_RECORDS_FOUND_MESSAGE = "No records found.";

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

        btnAdd.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return ButtonClickValidate();");
        btnUpdate.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return ButtonClickValidate();");
        btnAddRow.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return RowButtonClickValidate();");
        btnUpdateRow.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return RowButtonClickValidate();");
        btnSave.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return SaveButtonClickValidate();");
        
        txtTotalExperience.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtTotalExperience.ClientID + "','" + imgTotalExperience.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
        imgTotalExperience.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanTotalExperience.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgTotalExperience.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanTotalExperience.ClientID + "');");

        txtReleventExperience.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtReleventExperience.ClientID + "','" + imgReleventExperience.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
        imgReleventExperience.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanReleventExperience.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgReleventExperience.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanReleventExperience.ClientID + "');");

        txtCompanyName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtCompanyName.ClientID + "','" + imgCompanyName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPECIALCHAR + "');");
        imgCompanyName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanCompanyName.ClientID + "','" + Common.CommonConstants.MSG_ALPHABET_SPECIALCHAR + "');");
        imgCompanyName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanCompanyName.ClientID + "');");

        txtPositionHeld.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtPositionHeld.ClientID + "','" + imgPositionHeld.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPACE + "');");
        imgPositionHeld.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanPositionHeld.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgPositionHeld.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanPositionHeld.ClientID + "');");

        txtRCompanyName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtRCompanyName.ClientID + "','" + imgRCompanyName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPECIALCHAR + "');");
        imgRCompanyName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanRCompanyName.ClientID + "','" + Common.CommonConstants.MSG_ALPHABET_SPECIALCHAR + "');");
        imgRCompanyName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanRCompanyName.ClientID + "');");

        txtRPositionHeld.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtRPositionHeld.ClientID + "','" + imgRPositionHeld.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPACE + "');");
        imgRPositionHeld.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanRPositionHeld.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgRPositionHeld.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanRPositionHeld.ClientID + "');");

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

                //Javascript for checking employee is fresher or not while loading page.
                //If employee is fresher then Page will redirected to previous page requested.
                PreviousPage = Context.Request.UrlReferrer.ToString();//gets previous page url
                jScript = "<script language='javascript'>javascript:JavScriptFn('" + employee.IsFresher + "','" + PreviousPage + "')</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "Startup", jScript);

            }

            if (!IsPostBack)
            {
                this.GetMonths();
                this.GetYears();
                this.GetNonReleventdetailMonths();
                this.GetNonReleventdetailYears();
                this.PopulateGrid(employeeID);
                this.PopulateNonRelevantGrid(employeeID);
                this.PopulateControls();
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
                            Releventdetails.Enabled = true;
                            NonReleventDetails.Enabled = true;
                            btnCancel.Visible = true;
                            ExperienceRow.Disabled = false;
                            if (gvOrganisation.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE || gvOrgNonReleventDetails.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
                                btnSave.Visible = true;
                        }
                        else
                        {
                            Releventdetails.Enabled = false;
                            NonReleventDetails.Enabled = false;
                            btnSave.Visible = false;
                            btnCancel1.Visible = false;
                            ExperienceRow.Disabled = true;
                        }
                    }
                }
                else
                {
                    if (PageMode == Common.MasterEnum.PageModeEnum.Edit.ToString() && arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
                    {
                        Releventdetails.Enabled = true;
                        NonReleventDetails.Enabled = true;
                        btnCancel.Visible = true;
                        ExperienceRow.Disabled = false;
                        if (gvOrganisation.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE || gvOrgNonReleventDetails.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
                            btnSave.Visible = true;
                    }
                    else
                    {
                        Releventdetails.Enabled = false;
                        NonReleventDetails.Enabled = false;
                        btnSave.Visible = false;
                        btnCancel1.Visible = false;
                        ExperienceRow.Disabled = true;
                    }
                }
            }
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
                Label OrganisationId = (Label)gvOrganisation.Rows[rowIndex].FindControl(ORGANIZATIONID);
                Label Mode = (Label)gvOrganisation.Rows[rowIndex].FindControl(MODE);
                Label MonthSince = (Label)gvOrganisation.Rows[rowIndex].FindControl(MONTHSINCE);
                Label YearSince = (Label)gvOrganisation.Rows[rowIndex].FindControl(YEARSINCE);
                Label MonthTill = (Label)gvOrganisation.Rows[rowIndex].FindControl(MONTHTILL);
                Label YearTill = (Label)gvOrganisation.Rows[rowIndex].FindControl(YEARTILL);

                MonthSince.Text = ddlMonthSince.SelectedValue;
                YearSince.Text = ddlYearSince.SelectedValue;
                MonthTill.Text = ddlMonthTill.SelectedValue;
                YearTill.Text = ddlYearTill.SelectedValue;
                gvOrganisation.Rows[rowIndex].Cells[0].Text = txtCompanyName.Text;
                gvOrganisation.Rows[rowIndex].Cells[1].Text = ddlMonthSince.SelectedItem.Text + "-" + ddlYearSince.SelectedItem.Text;
                gvOrganisation.Rows[rowIndex].Cells[2].Text = ddlMonthTill.SelectedItem.Text + "-" + ddlYearTill.SelectedItem.Text;
                gvOrganisation.Rows[rowIndex].Cells[3].Text = txtPositionHeld.Text;
                gvOrganisation.Rows[rowIndex].Cells[4].Text = txtExperience.Text;

                if (int.Parse(OrganisationId.Text) == 0)
                {
                    Mode.Text = "1";
                }
                else
                {
                    Mode.Text = "2";
                }

                ImageButton btnImg = (ImageButton)gvOrganisation.Rows[rowIndex].FindControl(IMGBTNDELETE);
                btnImg.Enabled = true;
                ViewState[ROWINDEX] = null;
                ViewState[DELETEROWINDEX] = null;

            }
            int total = 0;
            for (int i = 0; i < OrganisationDetailsCollection.Count; i++)
            {
                BusinessEntities.OrganisationDetails objOrganisationDetails = new BusinessEntities.OrganisationDetails();
                objOrganisationDetails = (BusinessEntities.OrganisationDetails)OrganisationDetailsCollection.Item(i);

                Label OrganisationId = (Label)gvOrganisation.Rows[i].FindControl(ORGANIZATIONID);
                Label Mode = (Label)gvOrganisation.Rows[rowIndex].FindControl(MODE);
                Label MonthSince = (Label)gvOrganisation.Rows[rowIndex].FindControl(MONTHSINCE);
                Label YearSince = (Label)gvOrganisation.Rows[rowIndex].FindControl(YEARSINCE);
                Label MonthTill = (Label)gvOrganisation.Rows[rowIndex].FindControl(MONTHTILL);
                Label YearTill = (Label)gvOrganisation.Rows[rowIndex].FindControl(YEARTILL);

                objOrganisationDetails.OrganisationId = int.Parse(OrganisationId.Text);
                objOrganisationDetails.EMPId = int.Parse(EMPId.Value);
                objOrganisationDetails.CompanyName = gvOrganisation.Rows[i].Cells[0].Text;
                objOrganisationDetails.MonthSince = int.Parse(MonthSince.Text);
                objOrganisationDetails.YearSince = int.Parse(YearSince.Text);
                objOrganisationDetails.MonthTill = int.Parse(MonthTill.Text);
                objOrganisationDetails.YearTill = int.Parse(YearTill.Text);
                objOrganisationDetails.Designation = gvOrganisation.Rows[i].Cells[3].Text;
                objOrganisationDetails.Experience = gvOrganisation.Rows[i].Cells[4].Text;
                objOrganisationDetails.Mode = int.Parse(Mode.Text);
                total = total + Convert.ToInt32(gvOrganisation.Rows[i].Cells[4].Text);

            }

            hfTotalExperience.Value = total.ToString();


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
        }
    }

    /// <summary>
    /// Handles the Click event of the btnCancel control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnCancel_Click(object sender, EventArgs e)
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

        int Total = Convert.ToInt32(hfTotalExperience.Value) + ExperienceValue;
        hfTotalExperience.Value = Total.ToString();
        
    }

    /// <summary>
    /// Handles the Click event of the btnSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Rave.HR.BusinessLayer.Employee.OrganisationDetails objOrganisationDetailsBAL;

        BusinessEntities.OrganisationDetails objOrganisationDetails;
        BusinessEntities.RaveHRCollection objSaveOrganisationDetailsCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            objOrganisationDetailsBAL = new Rave.HR.BusinessLayer.Employee.OrganisationDetails();

            if (!string.IsNullOrEmpty(txtReleventExperience.Text) && !string.IsNullOrEmpty(txtTotalExperience.Text))
            {
                int ReleventExperience = Convert.ToInt32(txtReleventExperience.Text);
                int TotalExperience = Convert.ToInt32(txtTotalExperience.Text);
                int ExperienceTotal = 0;

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

                        objOrganisationDetails.OrganisationId = int.Parse(OrganisationId.Text);
                        objOrganisationDetails.EMPId = int.Parse(EMPId.Value);
                        objOrganisationDetails.CompanyName = gvOrganisation.Rows[i].Cells[0].Text;
                        objOrganisationDetails.MonthSince = int.Parse(MonthSince.Text);
                        objOrganisationDetails.YearSince = int.Parse(YearSince.Text);
                        objOrganisationDetails.MonthTill = int.Parse(MonthTill.Text);
                        objOrganisationDetails.YearTill = int.Parse(YearTill.Text);

                        objOrganisationDetails.Designation = gvOrganisation.Rows[i].Cells[3].Text;
                        objOrganisationDetails.Experience = gvOrganisation.Rows[i].Cells[4].Text;
                        ExperienceTotal = ExperienceTotal + Convert.ToInt32(gvOrganisation.Rows[i].Cells[4].Text);
                        objOrganisationDetails.Mode = int.Parse(Mode.Text);
                        objOrganisationDetails.ExperienceType = Convert.ToInt32(MasterEnum.ExperienceType.Relevent);
                        objSaveOrganisationDetailsCollection.Add(objOrganisationDetails);
                    }
                }
                if (ExperienceTotal != ReleventExperience)
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
                        objOrganisationDetails.CompanyName = obj.CompanyName;
                        objOrganisationDetails.MonthSince = obj.MonthSince;
                        objOrganisationDetails.YearSince = obj.YearSince;
                        objOrganisationDetails.MonthTill = obj.MonthTill;
                        objOrganisationDetails.YearTill = obj.YearTill;
                        objOrganisationDetails.Designation = obj.Designation;
                        objOrganisationDetails.Experience = obj.Experience;
                        objOrganisationDetails.Mode = obj.Mode;

                        objSaveOrganisationDetailsCollection.Add(objOrganisationDetails);
                    }

                }

                if (gvOrgNonReleventDetails.Visible)
                {
                    int NonReleventExperience = TotalExperience - ReleventExperience;
                    int NonExperienceTotal = 0;

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

                            objOrganisationDetails.OrganisationId = int.Parse(OrganisationId.Text);
                            objOrganisationDetails.EMPId = int.Parse(EMPId.Value);
                            objOrganisationDetails.CompanyName = gvOrgNonReleventDetails.Rows[i].Cells[0].Text;
                            objOrganisationDetails.MonthSince = int.Parse(MonthSince.Text);
                            objOrganisationDetails.YearSince = int.Parse(YearSince.Text);
                            objOrganisationDetails.MonthTill = int.Parse(MonthTill.Text);
                            objOrganisationDetails.YearTill = int.Parse(YearTill.Text);
                            objOrganisationDetails.Designation = gvOrgNonReleventDetails.Rows[i].Cells[3].Text;
                            objOrganisationDetails.Experience = gvOrgNonReleventDetails.Rows[i].Cells[4].Text;
                            NonExperienceTotal = NonExperienceTotal + Convert.ToInt32(gvOrgNonReleventDetails.Rows[i].Cells[4].Text);
                            objOrganisationDetails.Mode = int.Parse(Mode.Text);
                            objOrganisationDetails.ExperienceType = Convert.ToInt32(MasterEnum.ExperienceType.NonRelevent);
                            objSaveOrganisationDetailsCollection.Add(objOrganisationDetails);
                        }
                    }
                    if (NonExperienceTotal != NonReleventExperience)
                    {
                        lblError.Text = CommonConstants.EXPERIENCE_EQUAL_NONRELEVENTEXPERIENCE;
                        return;
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
                            objOrganisationDetails.CompanyName = obj.CompanyName;
                            objOrganisationDetails.MonthSince = obj.MonthSince;
                            objOrganisationDetails.YearSince = obj.YearSince;
                            objOrganisationDetails.MonthTill = obj.MonthTill;
                            objOrganisationDetails.YearTill = obj.YearTill;
                            objOrganisationDetails.Designation = obj.Designation;
                            objOrganisationDetails.Experience = obj.Experience;
                            objOrganisationDetails.Mode = obj.Mode;

                            objSaveOrganisationDetailsCollection.Add(objOrganisationDetails);
                        }

                    }
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
                }

                if (gvOrganisation.Rows.Count == 0 && gvOrgNonReleventDetails.Rows.Count == 0)
                    btnSave.Visible = false;

                lblMessage.Text = "Organisation details saved successfully.";
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
        int deleteRowIndex = 0;
        int rowIndex = -1;

        BusinessEntities.OrganisationDetails objOrganisationDetails = new BusinessEntities.OrganisationDetails();

        deleteRowIndex = e.RowIndex;
        
        objOrganisationDetails = (BusinessEntities.OrganisationDetails)OrganisationDetailsCollection.Item(deleteRowIndex);
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
    }

    /// <summary>
    /// Handles the RowEditing event of the gvOrganisation control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewEditEventArgs"/> instance containing the event data.</param>
    protected void gvOrganisation_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int rowIndex = 0;
        Label MonthSince = (Label)gvOrganisation.Rows[e.NewEditIndex].FindControl(MONTHSINCE);
        Label YearSince = (Label)gvOrganisation.Rows[e.NewEditIndex].FindControl(YEARSINCE);
        Label MonthTill = (Label)gvOrganisation.Rows[e.NewEditIndex].FindControl(MONTHTILL);
        Label YearTill = (Label)gvOrganisation.Rows[e.NewEditIndex].FindControl(YEARTILL);

        ddlMonthSince.SelectedValue = MonthSince.Text;
        ddlYearSince.SelectedValue = YearSince.Text;
        ddlMonthTill.SelectedValue = MonthTill.Text;
        ddlYearTill.SelectedValue = YearTill.Text;

        txtCompanyName.Text = gvOrganisation.Rows[e.NewEditIndex].Cells[0].Text.Trim();
        txtPositionHeld.Text = gvOrganisation.Rows[e.NewEditIndex].Cells[3].Text.Trim();
        txtExperience.Text = gvOrganisation.Rows[e.NewEditIndex].Cells[4].Text.Trim();
        ExperienceValue = Convert.ToInt32(gvOrganisation.Rows[e.NewEditIndex].Cells[4].Text);
        

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

        int Total = Convert.ToInt32(hfTotalExperience.Value) - ExperienceValue;
        hfTotalExperience.Value = Total.ToString();
    }

    /// <summary>
    /// Totals the experience text changed event handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void TotalExperienceTextChangedEventHandler(object sender, EventArgs e)
    {
        if (txtTotalExperience.Text != string.Empty && txtReleventExperience.Text != string.Empty)
        {
            int TotalExperience = Convert.ToInt32(txtTotalExperience.Text);
            int RelevantExperience = Convert.ToInt32(txtReleventExperience.Text);

            if (RelevantExperience > TotalExperience)
            {
                //divReleventDetail.Visible = false;
                divNonReleventDetail.Visible = false;
                lblError.Text = CommonConstants.TOTALEXPERIENCE_VALIDATION;
                return;
            }
            else
            {
                //divReleventDetail.Visible = true;
                divNonReleventDetail.Visible = true;
            }

            if (txtTotalExperience.Text.Trim().Equals(txtReleventExperience.Text.Trim()))
                divNonReleventDetail.Visible = false;
            else
                divNonReleventDetail.Visible = true;
        }
        else
        {
            divNonReleventDetail.Visible = false;
        }
    }

    /// <summary>
    /// Relevents the experience text changed event handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ReleventExperienceTextChangedEventHandler(object sender, EventArgs e)
    {
        if (txtTotalExperience.Text != string.Empty && txtReleventExperience.Text != string.Empty)
        {
            int TotalExperience = Convert.ToInt32(txtTotalExperience.Text);
            int RelevantExperience = Convert.ToInt32(txtReleventExperience.Text);

            if (RelevantExperience > TotalExperience)
            {
                //divReleventDetail.Visible = false;
                divNonReleventDetail.Visible = false;
                lblError.Text = CommonConstants.RELEVENT_VALIDATION;
                return;
            }
            else
            {
                //divReleventDetail.Visible = true;
                divNonReleventDetail.Visible = true;
            }
            if (txtTotalExperience.Text.Trim().Equals(txtReleventExperience.Text.Trim()))
                divNonReleventDetail.Visible = false;
            else
                divNonReleventDetail.Visible = true;
        }
        else
        {
            divNonReleventDetail.Visible = false;
            //divReleventDetail.Visible = false;
        }

    }

    /// <summary>
    /// Workingssince text changed event handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    //protected void WorkingSinceTextChangedEventHandler(object sender, EventArgs e)
    //{
    //    if(txtWorkingSince.Text!=string.Empty && txtWorkingTill.Text!=string.Empty)
    //    {
    //        DateTime sdate = Convert.ToDateTime(txtWorkingSince.Text);
    //        DateTime edate = Convert.ToDateTime(txtWorkingTill.Text);

    //        int Months =12*(sdate.Year-edate.Year)+ sdate.Month - edate.Month;

    //        txtExperience.Text = Math.Abs(Months).ToString();
    //    }

    //}

    /// <summary>
    /// Workingstill text changed event handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    //protected void WorkingTillTextChangedEventHandler(object sender, EventArgs e)
    //{
    //    if (txtWorkingSince.Text != string.Empty && txtWorkingTill.Text != string.Empty)
    //    {
    //        DateTime sdate = Convert.ToDateTime(txtWorkingSince.Text);
    //        DateTime edate = Convert.ToDateTime(txtWorkingTill.Text);

    //        int Months = 12 * (sdate.Year - edate.Year) + sdate.Month - edate.Month;

    //        txtExperience.Text = Math.Abs(Months).ToString();
    //    }
        
    //}

    /// <summary>
    /// Handles the Click event of the btnAdd control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ValidateControls())
        {
            BusinessEntities.OrganisationDetails objOrganisationDetails = new BusinessEntities.OrganisationDetails();

            if (gvOrganisation.Rows[0].Cells[0].Text == NO_RECORDS_FOUND_MESSAGE)
            {
                OrganisationDetailsCollection.Clear();
            }

            objOrganisationDetails.CompanyName = txtCompanyName.Text;
            //objOrganisationDetails.StartDate = Convert.ToDateTime(txtWorkingSince.Text);
            //objOrganisationDetails.EndDate = Convert.ToDateTime(txtWorkingTill.Text);
            objOrganisationDetails.Designation = txtPositionHeld.Text;
            objOrganisationDetails.Experience = txtExperience.Text;
            objOrganisationDetails.WorkingSince = ddlMonthSince.SelectedItem.Text + "-" + ddlYearSince.SelectedItem.Text;
            objOrganisationDetails.WorkingTill = ddlMonthTill.SelectedItem.Text + "-" + ddlYearTill.SelectedItem.Text;
            objOrganisationDetails.MonthSince = Convert.ToInt32(ddlMonthSince.SelectedValue);
            objOrganisationDetails.YearSince = Convert.ToInt32(ddlYearSince.SelectedValue);
            objOrganisationDetails.MonthTill = Convert.ToInt32(ddlMonthTill.SelectedValue);
            objOrganisationDetails.YearTill = Convert.ToInt32(ddlYearTill.SelectedValue);

            objOrganisationDetails.Mode = 1;

            OrganisationDetailsCollection.Add(objOrganisationDetails);

            this.DoDataBind();

            this.ClearControls();

            if (gvOrganisation.Rows.Count != 0)
                btnSave.Visible = true;

        }

    }

    /// <summary>
    /// Handles the Click event of the btnAddRow control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnAddRow_Click(object sender, EventArgs e)
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
            objOrganisationDetails.Experience = txtRExperience.Text;
            objOrganisationDetails.WorkingSince = ddlMonthSince.SelectedItem.Text + "-" + ddlYearSince.SelectedItem.Text;
            objOrganisationDetails.WorkingTill = ddlMonthTill.SelectedItem.Text + "-" + ddlYearTill.SelectedItem.Text;
            objOrganisationDetails.MonthSince = Convert.ToInt32(ddlRMonthSince.SelectedValue);
            objOrganisationDetails.YearSince = Convert.ToInt32(ddlRYearSince.SelectedValue);
            objOrganisationDetails.MonthTill = Convert.ToInt32(ddlRMonthTill.SelectedValue);
            objOrganisationDetails.YearTill = Convert.ToInt32(ddlRYearTill.SelectedValue);
            objOrganisationDetails.Mode = 1;

            NonRelevantOrganisationDetailsCollection.Add(objOrganisationDetails);

            this.DoNonReleventDataBind();

            this.ClearNonReleventControls();

            if (gvOrgNonReleventDetails.Rows.Count != 0)
                btnSave.Visible = true;
        }
    }

    /// <summary>
    /// Handles the Click event of the btnUpdateRow control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnUpdateRow_Click(object sender, EventArgs e)
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

                Label OrganisationId = (Label)gvOrgNonReleventDetails.Rows[rowIndex].FindControl(ORGANIZATIONID);
                Label Mode = (Label)gvOrgNonReleventDetails.Rows[rowIndex].FindControl(MODE);
                Label MonthSince = (Label)gvOrgNonReleventDetails.Rows[rowIndex].FindControl(MONTHSINCE);
                Label YearSince = (Label)gvOrgNonReleventDetails.Rows[rowIndex].FindControl(YEARSINCE);
                Label MonthTill = (Label)gvOrgNonReleventDetails.Rows[rowIndex].FindControl(MONTHTILL);
                Label YearTill = (Label)gvOrgNonReleventDetails.Rows[rowIndex].FindControl(YEARTILL);

                MonthSince.Text = ddlRMonthSince.SelectedValue;
                YearSince.Text = ddlRYearSince.SelectedValue;
                MonthTill.Text = ddlRMonthTill.SelectedValue;
                YearTill.Text = ddlRYearTill.SelectedValue;
                gvOrgNonReleventDetails.Rows[rowIndex].Cells[0].Text = txtRCompanyName.Text;
                gvOrgNonReleventDetails.Rows[rowIndex].Cells[1].Text = ddlRMonthSince.SelectedItem.Text + "-" + ddlRYearSince.SelectedItem.Text;
                gvOrgNonReleventDetails.Rows[rowIndex].Cells[2].Text = ddlRMonthTill.SelectedItem.Text + "-" + ddlRYearTill.SelectedItem.Text;
                gvOrgNonReleventDetails.Rows[rowIndex].Cells[3].Text = txtRPositionHeld.Text;
                gvOrgNonReleventDetails.Rows[rowIndex].Cells[4].Text = txtRExperience.Text;

                if (int.Parse(OrganisationId.Text) == 0)
                {
                    Mode.Text = "1";
                }
                else
                {
                    Mode.Text = "2";
                }

                ImageButton btnImg = (ImageButton)gvOrgNonReleventDetails.Rows[rowIndex].FindControl(IMGBTNDELETE);
                btnImg.Enabled = true;
                ViewState[NONRELEVANTROWINDEX] = null;
                ViewState[DELETENONRELEVANTROWINDEX] = null;

                //}

                for (int i = 0; i < NonRelevantOrganisationDetailsCollection.Count; i++)
                {
                    BusinessEntities.OrganisationDetails objOrganisationDetails = new BusinessEntities.OrganisationDetails();
                    objOrganisationDetails = (BusinessEntities.OrganisationDetails)NonRelevantOrganisationDetailsCollection.Item(i);

                    //Label OrganisationId = (Label)gvOrgNonReleventDetails.Rows[i].FindControl(ORGANIZATIONID);
                    //Label Mode = (Label)gvOrgNonReleventDetails.Rows[rowIndex].FindControl(MODE);
                    //Label MonthSince = (Label)gvOrgNonReleventDetails.Rows[rowIndex].FindControl("MonthSince");
                    //Label YearSince = (Label)gvOrgNonReleventDetails.Rows[rowIndex].FindControl("YearSince");
                    //Label MonthTill = (Label)gvOrgNonReleventDetails.Rows[rowIndex].FindControl("MonthTill");
                    //Label YearTill = (Label)gvOrgNonReleventDetails.Rows[rowIndex].FindControl("YearTill");

                    objOrganisationDetails.OrganisationId = int.Parse(OrganisationId.Text);
                    objOrganisationDetails.EMPId = int.Parse(EMPId.Value);
                    objOrganisationDetails.MonthSince = int.Parse(MonthSince.Text);
                    objOrganisationDetails.YearSince = int.Parse(YearSince.Text);
                    objOrganisationDetails.MonthTill = int.Parse(MonthTill.Text);
                    objOrganisationDetails.YearTill = int.Parse(YearTill.Text);
                    objOrganisationDetails.CompanyName = gvOrgNonReleventDetails.Rows[i].Cells[0].Text;
                    objOrganisationDetails.Designation = gvOrgNonReleventDetails.Rows[i].Cells[3].Text;
                    objOrganisationDetails.Experience = gvOrgNonReleventDetails.Rows[i].Cells[4].Text;
                    objOrganisationDetails.Mode = int.Parse(Mode.Text);
                    total = total + Convert.ToInt32(gvOrgNonReleventDetails.Rows[i].Cells[4].Text);

                }
            }
            hfTotalNonExperience.Value = total.ToString();

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
        btnUpdateRow.Visible = false;
        btnCancelRow.Visible = false;

        int Total = Convert.ToInt32(hfTotalNonExperience.Value) + NonExperienceValue;
        hfTotalNonExperience.Value = Total.ToString();
    }

    /// <summary>
    /// Handles the RowDeleting event of the gvOrgNonReleventDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewDeleteEventArgs"/> instance containing the event data.</param>
    protected void gvOrgNonReleventDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int deleteRowIndex = 0;
        int rowIndex = -1;

        BusinessEntities.OrganisationDetails objOrganisationDetails = new BusinessEntities.OrganisationDetails();

        deleteRowIndex = e.RowIndex;
        //int experience = Convert.ToInt32(gvOrgNonReleventDetails.Rows[deleteRowIndex].Cells[4].Text);
        //int value = Convert.ToInt32(hfTotalNonExperience.Value) - experience;
        //hfTotalNonExperience.Value = value.ToString();

        objOrganisationDetails = (BusinessEntities.OrganisationDetails)NonRelevantOrganisationDetailsCollection.Item(deleteRowIndex);
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

    }

    /// <summary>
    /// Handles the RowEditing event of the gvOrgNonReleventDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewEditEventArgs"/> instance containing the event data.</param>
    protected void gvOrgNonReleventDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int rowIndex = 0;
        Label MonthSince = (Label)gvOrgNonReleventDetails.Rows[e.NewEditIndex].FindControl(MONTHSINCE);
        Label YearSince = (Label)gvOrgNonReleventDetails.Rows[e.NewEditIndex].FindControl(YEARSINCE);
        Label MonthTill = (Label)gvOrgNonReleventDetails.Rows[e.NewEditIndex].FindControl(MONTHTILL);
        Label YearTill = (Label)gvOrgNonReleventDetails.Rows[e.NewEditIndex].FindControl(YEARTILL);

        ddlRMonthSince.SelectedValue = MonthSince.Text;
        ddlRYearSince.SelectedValue = YearSince.Text;
        ddlRMonthTill.SelectedValue = MonthTill.Text;
        ddlRYearTill.SelectedValue = YearTill.Text;
        txtRCompanyName.Text = gvOrgNonReleventDetails.Rows[e.NewEditIndex].Cells[0].Text.Trim();
        txtRPositionHeld.Text = gvOrgNonReleventDetails.Rows[e.NewEditIndex].Cells[3].Text.Trim();
        txtRExperience.Text = gvOrgNonReleventDetails.Rows[e.NewEditIndex].Cells[4].Text;
        NonExperienceValue = Convert.ToInt32(gvOrgNonReleventDetails.Rows[e.NewEditIndex].Cells[4].Text);

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

        int Total = Convert.ToInt32(hfTotalNonExperience.Value) - NonExperienceValue;
        hfTotalNonExperience.Value = Total.ToString();
    }

    /// <summary>
    /// Non ReleventWorking since text changed event handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    //protected void RWorkingSinceTextChangedEventHandler(object sender, EventArgs e)
    //{
    //    if (txtRWorkingSince.Text != string.Empty && txtRWorkingTill.Text != string.Empty)
    //    {
    //        DateTime sdate = Convert.ToDateTime(txtRWorkingSince.Text);
    //        DateTime edate = Convert.ToDateTime(txtRWorkingTill.Text);

    //        int Months = 12 * (sdate.Year - edate.Year) + sdate.Month - edate.Month;

    //        txtRExperience.Text = Math.Abs(Months).ToString();
    //    }

    //}

    /// <summary>
    ///Non ReleventWorking till text changed event handler.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    //protected void RWorkingTillTextChangedEventHandler(object sender, EventArgs e)
    //{
    //    if (txtRWorkingSince.Text != string.Empty && txtRWorkingTill.Text != string.Empty)
    //    {
    //        DateTime sdate = Convert.ToDateTime(txtRWorkingSince.Text);
    //        DateTime edate = Convert.ToDateTime(txtRWorkingTill.Text);

    //        int Months = 12 * (sdate.Year - edate.Year) + sdate.Month - edate.Month;

    //        txtRExperience.Text = Math.Abs(Months).ToString();
    //    }

    //}

    /// <summary>
    /// Handles the Click event of the btnNext control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        Response.Redirect(CommonConstants.PAGE_SKILLSDETAILS);
    }

    /// <summary>
    /// Handles the Click event of the btnPrevious control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Response.Redirect(CommonConstants.PAGE_CERTIFICATIONDETAILS);
    }

    /// <summary>
    /// Handles the Click event of the CancelDetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void CancelDetails_Click(object sender, EventArgs e)
    {
        this.ClearControls();
        this.ClearNonReleventControls();
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlMonthSince control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlMonthSince_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMonthSince.SelectedValue != MONTHS && ddlYearSince.SelectedValue != YEARS && ddlMonthTill.SelectedValue != MONTHS && ddlYearTill.SelectedValue != YEARS)
        {
            int MonthSince = Convert.ToInt32(ddlMonthSince.SelectedValue);
            int YearSince = Convert.ToInt32(ddlYearSince.SelectedValue);
            int MonthTill = Convert.ToInt32(ddlMonthTill.SelectedValue);
            int YearTill = Convert.ToInt32(ddlYearTill.SelectedValue);

            int Months = 12 * (YearSince - YearTill) + MonthSince - MonthTill;

            txtExperience.Text = (Math.Abs(Months) + 1).ToString();
        }


    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlYearSince control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlYearSince_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMonthSince.SelectedValue != MONTHS && ddlYearSince.SelectedValue != YEARS && ddlMonthTill.SelectedValue != MONTHS && ddlYearTill.SelectedValue != YEARS)
        {
            int MonthSince = Convert.ToInt32(ddlMonthSince.SelectedValue);
            int YearSince = Convert.ToInt32(ddlYearSince.SelectedValue);
            int MonthTill = Convert.ToInt32(ddlMonthTill.SelectedValue);
            int YearTill = Convert.ToInt32(ddlYearTill.SelectedValue);

            int Months = 12 * (YearSince - YearTill) + MonthSince - MonthTill;

            txtExperience.Text = (Math.Abs(Months) + 1).ToString();
        }
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlMonthTill control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlMonthTill_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMonthSince.SelectedValue != MONTHS && ddlYearSince.SelectedValue != YEARS && ddlMonthTill.SelectedValue != MONTHS && ddlYearTill.SelectedValue != YEARS)
        {
            int MonthSince = Convert.ToInt32(ddlMonthSince.SelectedValue);
            int YearSince = Convert.ToInt32(ddlYearSince.SelectedValue);
            int MonthTill = Convert.ToInt32(ddlMonthTill.SelectedValue);
            int YearTill = Convert.ToInt32(ddlYearTill.SelectedValue);

            int Months = 12 * (YearSince - YearTill) + MonthSince - MonthTill;

            txtExperience.Text = (Math.Abs(Months) + 1).ToString();
        }
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlYearTill control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlYearTill_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMonthSince.SelectedValue != MONTHS && ddlYearSince.SelectedValue != YEARS && ddlMonthTill.SelectedValue != MONTHS && ddlYearTill.SelectedValue != YEARS)
        {
            int MonthSince = Convert.ToInt32(ddlMonthSince.SelectedValue);
            int YearSince = Convert.ToInt32(ddlYearSince.SelectedValue);
            int MonthTill = Convert.ToInt32(ddlMonthTill.SelectedValue);
            int YearTill = Convert.ToInt32(ddlYearTill.SelectedValue);

            int Months = 12 * (YearSince - YearTill) + MonthSince - MonthTill;

            txtExperience.Text = (Math.Abs(Months) + 1).ToString();
        }
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlRMonthTill control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlRMonthTill_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRMonthSince.SelectedValue != MONTHS && ddlRYearSince.SelectedValue != YEARS && ddlRMonthTill.SelectedValue != MONTHS && ddlRYearTill.SelectedValue != YEARS)
        {
            int MonthSince = Convert.ToInt32(ddlRMonthSince.SelectedValue);
            int YearSince = Convert.ToInt32(ddlRYearSince.SelectedValue);
            int MonthTill = Convert.ToInt32(ddlRMonthTill.SelectedValue);
            int YearTill = Convert.ToInt32(ddlRYearTill.SelectedValue);

            int Months = 12 * (YearSince - YearTill) + MonthSince - MonthTill;

            txtRExperience.Text = (Math.Abs(Months) + 1).ToString();
        }
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlRYearTill control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlRYearTill_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRMonthSince.SelectedValue != MONTHS && ddlRYearSince.SelectedValue != YEARS && ddlRMonthTill.SelectedValue != MONTHS && ddlRYearTill.SelectedValue != YEARS)
        {
            int MonthSince = Convert.ToInt32(ddlRMonthSince.SelectedValue);
            int YearSince = Convert.ToInt32(ddlRYearSince.SelectedValue);
            int MonthTill = Convert.ToInt32(ddlRMonthTill.SelectedValue);
            int YearTill = Convert.ToInt32(ddlRYearTill.SelectedValue);

            int Months = 12 * (YearSince - YearTill) + MonthSince - MonthTill;

            txtRExperience.Text = (Math.Abs(Months) + 1).ToString();
        }
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlRMonthSince control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlRMonthSince_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRMonthSince.SelectedValue != MONTHS && ddlRYearSince.SelectedValue != YEARS && ddlRMonthTill.SelectedValue != MONTHS && ddlRYearTill.SelectedValue != YEARS)
        {
            int MonthSince = Convert.ToInt32(ddlRMonthSince.SelectedValue);
            int YearSince = Convert.ToInt32(ddlRYearSince.SelectedValue);
            int MonthTill = Convert.ToInt32(ddlRMonthTill.SelectedValue);
            int YearTill = Convert.ToInt32(ddlRYearTill.SelectedValue);

            int Months = 12 * (YearSince - YearTill) + MonthSince - MonthTill;

            txtRExperience.Text = (Math.Abs(Months) + 1).ToString();
        }
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlRYearSince control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlRYearSince_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRMonthSince.SelectedValue != MONTHS && ddlRYearSince.SelectedValue != YEARS && ddlRMonthTill.SelectedValue != MONTHS && ddlRYearTill.SelectedValue != YEARS)
        {
            int MonthSince = Convert.ToInt32(ddlRMonthSince.SelectedValue);
            int YearSince = Convert.ToInt32(ddlRYearSince.SelectedValue);
            int MonthTill = Convert.ToInt32(ddlRMonthTill.SelectedValue);
            int YearTill = Convert.ToInt32(ddlRYearTill.SelectedValue);

            int Months = 12 * (YearSince - YearTill) + MonthSince - MonthTill;

            txtRExperience.Text = (Math.Abs(Months) + 1).ToString();
        }
    }

    #endregion

    #region Private Member Functions

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
        if (gvOrganisation.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
        {
            for (int i = 0; i < gvOrganisation.Rows.Count; i++)
            {
                total = total + Convert.ToInt32(gvOrganisation.Rows[i].Cells[4].Text);

            }
        }
        hfTotalExperience.Value = total.ToString();

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

        int total = 0;
        if (gvOrgNonReleventDetails.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
        {
            for (int i = 0; i < gvOrgNonReleventDetails.Rows.Count; i++)
            {
                total = total + Convert.ToInt32(gvOrgNonReleventDetails.Rows[i].Cells[4].Text);

            }
        }
        hfTotalNonExperience.Value = total.ToString();
    }

    /// <summary>
    /// Clears the controls.
    /// </summary>
    private void ClearControls()
    {
        txtCompanyName.Text = string.Empty;
        txtExperience.Text = string.Empty;
        txtPositionHeld.Text = string.Empty;
        ddlMonthSince.SelectedIndex = 0;
        ddlYearSince.SelectedIndex = 0;
        ddlMonthTill.SelectedIndex = 0;
        ddlYearTill.SelectedIndex = 0;
    }

    /// <summary>
    /// Clears the Non Relevent Grid controls.
    /// </summary>
    private void ClearNonReleventControls()
    {
        txtRCompanyName.Text = string.Empty;
        txtRExperience.Text = string.Empty;
        txtRPositionHeld.Text = string.Empty;
        ddlRMonthSince.SelectedIndex = 0;
        ddlRYearSince.SelectedIndex = 0;
        ddlRMonthTill.SelectedIndex = 0;
        ddlRYearTill.SelectedIndex = 0;
    }

    /// <summary>
    /// Populates the grid.
    /// </summary>
    private void PopulateGrid(int employeeID)
    {
        OrganisationDetailsCollection = this.GetOrganisationDetails(Convert.ToInt32(MasterEnum.ExperienceType.Relevent), employeeID);

        //Binding the datatable to grid
        gvOrganisation.DataSource = OrganisationDetailsCollection;
        gvOrganisation.DataBind();

        //Displaying grid header with NO record found message.
        if (gvOrganisation.Rows.Count == 0)
            ShowHeaderWhenEmptyRelevantGrid();

        EMPId.Value = employeeID.ToString().Trim();

        int total = 0;
        if (gvOrganisation.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
        {
            
            for (int i = 0; i < gvOrganisation.Rows.Count; i++)
            {
                total = total + Convert.ToInt32(gvOrganisation.Rows[i].Cells[4].Text);

            }
        }
            hfTotalExperience.Value = total.ToString();
        
    }

    /// <summary>
    /// Populates the non relevant grid.
    /// </summary>
    /// <param name="employeeID">The employee ID.</param>
    private void PopulateNonRelevantGrid(int employeeID)
    {
        NonRelevantOrganisationDetailsCollection = this.GetOrganisationDetails(Convert.ToInt32(MasterEnum.ExperienceType.NonRelevent),employeeID);

        //Binding the datatable to grid
        gvOrgNonReleventDetails.DataSource = NonRelevantOrganisationDetailsCollection;
        gvOrgNonReleventDetails.DataBind();

        //Displaying grid header with NO record found message.
        if (gvOrgNonReleventDetails.Rows.Count == 0)
            ShowHeaderWhenEmptyGrid();
        
        int total = 0;
        if (gvOrgNonReleventDetails.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
        {
            for (int i = 0; i < gvOrgNonReleventDetails.Rows.Count; i++)
            {
                total = total + Convert.ToInt32(gvOrgNonReleventDetails.Rows[i].Cells[4].Text);

            }
        }
        hfTotalNonExperience.Value = total.ToString();
       
    }

    /// <summary>
    /// Populates the controls.
    /// </summary>
    private void PopulateControls()
    {
        if (hfTotalExperience.Value != "0" || hfTotalNonExperience.Value != "0")
        {
            txtTotalExperience.Text = Convert.ToString(Convert.ToInt32(hfTotalExperience.Value) + Convert.ToInt32(hfTotalNonExperience.Value));
            txtReleventExperience.Text = hfTotalExperience.Value;

            if (hfTotalNonExperience.Value != "0")
            {
                if (Convert.ToInt32(hfTotalExperience.Value) > Convert.ToInt32(hfTotalNonExperience.Value))
                {
                    divNonReleventDetail.Visible = true;
                }
            }
        }

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
            throw ex;
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
        //string[] WorkingSinceDateArr;
        //string[] WorkingTillDateArr;

        //WorkingSinceDateArr = Convert.ToString(txtWorkingSince.Text).Split(SPILITER_SLASH);
        DateTime WorkingSinceDate = new DateTime(Convert.ToInt32(ddlYearSince.SelectedValue), Convert.ToInt32(ddlMonthSince.SelectedValue), 1);

        //WorkingTillDateArr = Convert.ToString(txtWorkingTill.Text).Split(SPILITER_SLASH);
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

        if (txtExperience.Text != string.Empty && txtReleventExperience.Text != string.Empty)
        {
            int ReleventExperience = Convert.ToInt32(txtReleventExperience.Text);
            int Experience = Convert.ToInt32(txtExperience.Text);
            int TotalExperience = 0;

            if (Experience > ReleventExperience)
            {
                errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.RELEVENTEXPERIENCE_VALIDATION);
                flag = false;
                //lblError.Text = CommonConstants.RELEVENTEXPERIENCE_VALIDATION;
                //return;
            }


            if (flag)
            {
                if (hfTotalExperience.Value == string.Empty)
                {
                    TotalExperience = Experience;
                    hfTotalExperience.Value = TotalExperience.ToString();
                }
                else
                {
                    TotalExperience = Convert.ToInt32(hfTotalExperience.Value) + Experience;
                    hfTotalExperience.Value = TotalExperience.ToString();
                }
                if (TotalExperience > ReleventExperience)
                {
                    //lblError.Text = CommonConstants.RELEVENTEXPERIENCE_GRID_VALIDATION;
                    errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.RELEVENTEXPERIENCE_GRID_VALIDATION);
                    flag = false;
                }

                if (!flag)
                    hfTotalExperience.Value = Convert.ToString(TotalExperience - Experience);
            }
        }
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
        //string[] WorkingSinceDateArr;
        //string[] WorkingTillDateArr;

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

        if (txtRExperience.Text != string.Empty && txtReleventExperience.Text != string.Empty)
        {
            int ReleventExperience = Convert.ToInt32(txtReleventExperience.Text);
            int TotalExperience = Convert.ToInt32(txtTotalExperience.Text);
            int NonreleventExperience = TotalExperience - ReleventExperience;
            int Experience = Convert.ToInt32(txtRExperience.Text);
            int ExperienceTotal = 0;

            if (Experience > NonreleventExperience)
            {
                errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.NONRELEVENTEXPERIENCE_VALIDATION);
                flag = false;
               
            }

            if (flag)
            {
                if (hfTotalNonExperience.Value == string.Empty)
                {
                    ExperienceTotal = Experience;
                    hfTotalNonExperience.Value = ExperienceTotal.ToString();
                }
                else
                {
                    ExperienceTotal = Convert.ToInt32(hfTotalNonExperience.Value) + Experience;
                    hfTotalNonExperience.Value = ExperienceTotal.ToString();
                }
                if (ExperienceTotal > NonreleventExperience)
                {
                    //lblError.Text = CommonConstants.NONRELEVENTEXPERIENCE_GRID_VALIDATION;
                    errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.NONRELEVENTEXPERIENCE_GRID_VALIDATION);
                    flag = false;
                }

                if (!flag)
                    hfTotalNonExperience.Value = Convert.ToString(ExperienceTotal - Experience);
            }
        }
        lblError.Text = errMessage.ToString();
        return flag;

    }

    /// <summary>
    /// Gets the months.
    /// </summary>
    private void GetMonths()
    {
        #region commented code
        //Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        //BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        //raveHrColl = master.FillDropDownsBL((int)Common.EnumsConstants.Category.Months);

        //ddlMonthSince.DataSource = raveHrColl;
        //ddlMonthSince.DataTextField = Common.CommonConstants.DDL_DataTextField;
        //ddlMonthSince.DataValueField = Common.CommonConstants.DDL_DataValueField;
        //ddlMonthSince.DataBind();
        //SortDDL(ref ddlMonthSince);
        //ddlMonthSince.Items.Insert(CommonConstants.ZERO, MONTHS);

        //ddlMonthTill.DataSource = raveHrColl;
        //ddlMonthTill.DataTextField = Common.CommonConstants.DDL_DataTextField;
        //ddlMonthTill.DataValueField = Common.CommonConstants.DDL_DataValueField;
        //ddlMonthTill.DataBind();
        //SortDDL(ref ddlMonthTill);
        //ddlMonthTill.Items.Insert(CommonConstants.ZERO, MONTHS);
       #endregion

        ddlMonthSince.Items.Insert(CommonConstants.ZERO, MONTHS);

        ddlMonthSince.Items.Insert(1, new ListItem(Common.MasterEnum.Months.January.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.January)));
        ddlMonthSince.Items.Insert(2, new ListItem(Common.MasterEnum.Months.February.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.February)));
        ddlMonthSince.Items.Insert(3, new ListItem(Common.MasterEnum.Months.March.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.March)));
        ddlMonthSince.Items.Insert(4, new ListItem(Common.MasterEnum.Months.April.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.April)));
        ddlMonthSince.Items.Insert(5, new ListItem(Common.MasterEnum.Months.May.ToString(),
                                                 Convert.ToString((int)Common.MasterEnum.Months.May)));
        ddlMonthSince.Items.Insert(6, new ListItem(Common.MasterEnum.Months.June.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.June)));
        ddlMonthSince.Items.Insert(7, new ListItem(Common.MasterEnum.Months.July.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.July)));
        ddlMonthSince.Items.Insert(8, new ListItem(Common.MasterEnum.Months.August.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.August)));
        ddlMonthSince.Items.Insert(9, new ListItem(Common.MasterEnum.Months.September.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.September)));
        ddlMonthSince.Items.Insert(10, new ListItem(Common.MasterEnum.Months.October.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.October)));
        ddlMonthSince.Items.Insert(11, new ListItem(Common.MasterEnum.Months.November.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.November)));
        ddlMonthSince.Items.Insert(12, new ListItem(Common.MasterEnum.Months.December.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.December)));

        ddlMonthTill.Items.Insert(CommonConstants.ZERO, MONTHS);

        ddlMonthTill.Items.Insert(1, new ListItem(Common.MasterEnum.Months.January.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.January)));
        ddlMonthTill.Items.Insert(2, new ListItem(Common.MasterEnum.Months.February.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.February)));
        ddlMonthTill.Items.Insert(3, new ListItem(Common.MasterEnum.Months.March.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.March)));
        ddlMonthTill.Items.Insert(4, new ListItem(Common.MasterEnum.Months.April.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.April)));
        ddlMonthTill.Items.Insert(5, new ListItem(Common.MasterEnum.Months.May.ToString(),
                                                 Convert.ToString((int)Common.MasterEnum.Months.May)));
        ddlMonthTill.Items.Insert(6, new ListItem(Common.MasterEnum.Months.June.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.June)));
        ddlMonthTill.Items.Insert(7, new ListItem(Common.MasterEnum.Months.July.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.July)));
        ddlMonthTill.Items.Insert(8, new ListItem(Common.MasterEnum.Months.August.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.August)));
        ddlMonthTill.Items.Insert(9, new ListItem(Common.MasterEnum.Months.September.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.September)));
        ddlMonthTill.Items.Insert(10, new ListItem(Common.MasterEnum.Months.October.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.October)));
        ddlMonthTill.Items.Insert(11, new ListItem(Common.MasterEnum.Months.November.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.November)));
        ddlMonthTill.Items.Insert(12, new ListItem(Common.MasterEnum.Months.December.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.December)));
    }

    /// <summary>
    /// Gets the non releventdetail months.
    /// </summary>
    private void GetNonReleventdetailMonths()
    {
        #region commented code
        //Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        //BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        //raveHrColl = master.FillDropDownsBL((int)Common.EnumsConstants.Category.Months);

        //ddlRMonthSince.DataSource = raveHrColl;
        //ddlRMonthSince.DataTextField = Common.CommonConstants.DDL_DataTextField;
        //ddlRMonthSince.DataValueField = Common.CommonConstants.DDL_DataValueField;
        //ddlRMonthSince.DataBind();
        //SortDDL(ref ddlRMonthSince);
        //ddlRMonthSince.Items.Insert(CommonConstants.ZERO, MONTHS);

        //ddlRMonthTill.DataSource = raveHrColl;
        //ddlRMonthTill.DataTextField = Common.CommonConstants.DDL_DataTextField;
        //ddlRMonthTill.DataValueField = Common.CommonConstants.DDL_DataValueField;
        //ddlRMonthTill.DataBind();
        //SortDDL(ref ddlRMonthTill);
        //ddlRMonthTill.Items.Insert(CommonConstants.ZERO, MONTHS);
        #endregion

        ddlRMonthSince.Items.Insert(CommonConstants.ZERO, MONTHS);

        ddlRMonthSince.Items.Insert(1, new ListItem(Common.MasterEnum.Months.January.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.January)));
        ddlRMonthSince.Items.Insert(2, new ListItem(Common.MasterEnum.Months.February.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.February)));
        ddlRMonthSince.Items.Insert(3, new ListItem(Common.MasterEnum.Months.March.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.March)));
        ddlRMonthSince.Items.Insert(4, new ListItem(Common.MasterEnum.Months.April.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.April)));
        ddlRMonthSince.Items.Insert(5, new ListItem(Common.MasterEnum.Months.May.ToString(),
                                                 Convert.ToString((int)Common.MasterEnum.Months.May)));
        ddlRMonthSince.Items.Insert(6, new ListItem(Common.MasterEnum.Months.June.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.June)));
        ddlRMonthSince.Items.Insert(7, new ListItem(Common.MasterEnum.Months.July.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.July)));
        ddlRMonthSince.Items.Insert(8, new ListItem(Common.MasterEnum.Months.August.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.August)));
        ddlRMonthSince.Items.Insert(9, new ListItem(Common.MasterEnum.Months.September.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.September)));
        ddlRMonthSince.Items.Insert(10, new ListItem(Common.MasterEnum.Months.October.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.October)));
        ddlRMonthSince.Items.Insert(11, new ListItem(Common.MasterEnum.Months.November.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.November)));
        ddlRMonthSince.Items.Insert(12, new ListItem(Common.MasterEnum.Months.December.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.December)));

        ddlRMonthTill.Items.Insert(CommonConstants.ZERO, MONTHS);

        ddlRMonthTill.Items.Insert(1, new ListItem(Common.MasterEnum.Months.January.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.January)));
        ddlRMonthTill.Items.Insert(2, new ListItem(Common.MasterEnum.Months.February.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.February)));
        ddlRMonthTill.Items.Insert(3, new ListItem(Common.MasterEnum.Months.March.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.March)));
        ddlRMonthTill.Items.Insert(4, new ListItem(Common.MasterEnum.Months.April.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.April)));
        ddlRMonthTill.Items.Insert(5, new ListItem(Common.MasterEnum.Months.May.ToString(),
                                                 Convert.ToString((int)Common.MasterEnum.Months.May)));
        ddlRMonthTill.Items.Insert(6, new ListItem(Common.MasterEnum.Months.June.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.June)));
        ddlRMonthTill.Items.Insert(7, new ListItem(Common.MasterEnum.Months.July.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.July)));
        ddlRMonthTill.Items.Insert(8, new ListItem(Common.MasterEnum.Months.August.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.August)));
        ddlRMonthTill.Items.Insert(9, new ListItem(Common.MasterEnum.Months.September.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.September)));
        ddlRMonthTill.Items.Insert(10, new ListItem(Common.MasterEnum.Months.October.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.October)));
        ddlRMonthTill.Items.Insert(11, new ListItem(Common.MasterEnum.Months.November.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.November)));
        ddlRMonthTill.Items.Insert(12, new ListItem(Common.MasterEnum.Months.December.ToString(),
                                                Convert.ToString((int)Common.MasterEnum.Months.December)));
    }

    /// <summary>
    /// Gets the years.
    /// </summary>
    private void GetYears()
    {
        ddlYearSince.Items.Insert(CommonConstants.ZERO, YEARS);
        ddlYearTill.Items.Insert(CommonConstants.ZERO, YEARS);

        int startIndex = 1;
        for (int i = employee.DOB.Year + 18; i <= DateTime.Now.Year; i++)
        {
            ddlYearSince.Items.Insert(startIndex, new ListItem(i.ToString(), i.ToString()));
            ddlYearTill.Items.Insert(startIndex, new ListItem(i.ToString(), i.ToString()));
            
            startIndex++;
        }
    }

    /// <summary>
    /// Gets the non releventdetail years.
    /// </summary>
    private void GetNonReleventdetailYears()
    {
        ddlRYearSince.Items.Insert(CommonConstants.ZERO, YEARS);
        ddlRYearTill.Items.Insert(CommonConstants.ZERO, YEARS);

        int startIndex = 1;

        for (int i = employee.DOB.Year + 18; i <= DateTime.Now.Year; i++)
        {
            ddlRYearSince.Items.Insert(startIndex, new ListItem(i.ToString(), i.ToString()));
            ddlRYearTill.Items.Insert(startIndex, new ListItem(i.ToString(), i.ToString()));

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
            throw ex;
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
