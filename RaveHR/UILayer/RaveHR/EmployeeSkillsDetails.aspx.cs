using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Common;
using Common.AuthorizationManager;
using System.Text;
using BusinessEntities;
using Common.Constants;
using AjaxControlToolkit;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public partial class EmployeeSkillsDetails : BaseClass
{
    #region Private Field Members

    /// <summary>h
    /// private variable for Class Name used in each catch block
    /// </summary>
    private string CLASS_NAME = "EmployeeSkillsDetails.aspx";
    private int employeeID = 0;
    private BusinessEntities.Employee employee;
    char[] SPILITER_SLASH = { '/' };
    const string ReadOnly = "readonly";
    string UserRaveDomainId;
    string UserMailId;
    string MONTHS = "Months";
    string YEARS = "Years";
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
    private string _numericExpression = "^[0-9]*$";
    string PageMode = string.Empty;
    private string IMGBTNADD = "imgBtnAdd";
    private string IMGBTNDELETE = "imgBtnDelete";
    const string PAGETYPE = "pagetype";
    const string PAGETYPEEMPLOYEESUMMERY = "EMPLOYEESUMMERY";
    private string IMGBTNADDDOMAIN = "imgBtnAddDomain";
    private string IMGBTNDELETEDOMAIN = "imgBtnDeleteDomain";
    #region ViewState Constants

    /// <summary>
    /// private string variable for View State
    /// </summary>
    private string SKILLDETAILS = "SkillDetails";
    private string SKILLDETAILSDELETE = "SkillDetailsDelete";
    private string DELETEROWINDEX = "DeleteRowIndex";
    private string ROWINDEX = "RowIndex";
    private string PROFICIENCYDETAILS = "ProficiencyDetails";
    private string SKILLS = "Skills";
    private string EMPLOYEEDOMAIN = "EmployeeDomain";
    private string EMPLOYEEDOMAINCOLLECTION = "EmployeeDomainCollection";
    private string EMPLOYEEDOMAINDELETE = "EmployeeDomainDelete";

    #endregion ViewState Constants

    protected EmployeeMenuUC BubbleControl;

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

    //Siddhesh Arekar Domain Details 09032015 Start
    /// <summary>
    /// Gets or sets the skill details collection.
    /// </summary>
    /// <value>The skill details collection.</value>
    private BusinessEntities.RaveHRCollection EmployeeDomainCollection
    {
        get
        {
            if (ViewState[EMPLOYEEDOMAIN] != null)
                return ((BusinessEntities.RaveHRCollection)ViewState[EMPLOYEEDOMAIN]);
            else
                return null;
        }
        set
        {
            ViewState[EMPLOYEEDOMAIN] = value;
        }
    }

    /// <summary>
    /// Gets or sets the skill details collection.
    /// </summary>
    /// <value>The skill details collection.</value>
    private BusinessEntities.RaveHRCollection objEmployeeDomainCollection
    {
        get
        {
            if (ViewState[EMPLOYEEDOMAINCOLLECTION] != null)
                return ((BusinessEntities.RaveHRCollection)ViewState[EMPLOYEEDOMAINCOLLECTION]);
            else
                return null;
        }
        set
        {
            ViewState[EMPLOYEEDOMAINCOLLECTION] = value;
        }
    }
    //Siddhesh Arekar Domain Details 09032015 End

    /// <summary>
    /// Gets or sets the proficiency collection.
    /// </summary>
    /// <value>The proficiency collection.</value>
    private BusinessEntities.RaveHRCollection ProficiencyCollection
    {
        get
        {
            if (ViewState[PROFICIENCYDETAILS] != null)
                return ((BusinessEntities.RaveHRCollection)ViewState[PROFICIENCYDETAILS]);
            else
                return null;
        }
        set
        {
            ViewState[PROFICIENCYDETAILS] = value;
        }
    }

    /// <summary>
    /// Gets or sets the skill collection.
    /// </summary>
    /// <value>The skill collection.</value>
    private BusinessEntities.RaveHRCollection SkillCollection
    {
        get
        {
            if (ViewState[SKILLS] != null)
                return ((BusinessEntities.RaveHRCollection)ViewState[SKILLS]);
            else
                return null;
        }
        set
        {
            ViewState[SKILLS] = value;
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
            //Clearing the error label
            lblError.Text = string.Empty;
            lblMessage.Text = string.Empty;

            if (!ValidateURL())
            {
                Response.Redirect(CommonConstants.INVALIDURL, false);
            }
            else
            {
                btnSavePrimarySkills.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return SavePrimaySkillsButtonClickValidate();");

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
                    if (employee == null)
                        employee = new BusinessEntities.Employee();

                    Rave.HR.BusinessLayer.Employee.Employee objEmployee = new Rave.HR.BusinessLayer.Employee.Employee();
                    if (Request.QueryString[QueryStringConstants.EMPID] != null)
                        employee.EMPId = Convert.ToInt32(DecryptQueryString(QueryStringConstants.EMPID));

                    Session[SessionNames.PAGEMODE] = Common.MasterEnum.PageModeEnum.View;

                    employee = objEmployee.GetEmployee(employee);
                    this.PopulateGrid(employeeID);
                    this.GetPrimarySkillDetails(0);
                    this.PopulateContols();

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


                        if (PageMode == Common.MasterEnum.PageModeEnum.View.ToString() && IsPostBack == false && gvSkillsdetails.Rows.Count != CommonConstants.ZERO)
                        {
                            btnSavePrimarySkills.Enabled = false;

                            //To solved issue id 19221
                            //Coded added by Rahul P
                            lbxPrimaryskills.Enabled = false;

                            //29326-Subhra-start  Added this line to disable the skills at page load
                            gvSkillsdetails.Enabled = false;
                            gvEmployeeDomain.Enabled = false;

                            //Added this line and for loop to disable combo box at pageload
                            ComboBox ddlAddSkill = (ComboBox)gvSkillsdetails.FooterRow.FindControl("ddlAddSkill");
                            ComboBox ddlAddEmployeeDomain = (ComboBox)gvEmployeeDomain.FooterRow.FindControl("ddlAddEmployeeDomain");

                            ddlAddSkill.Enabled = false;
                            ddlAddEmployeeDomain.Enabled = false;

                            for (int i = 0; i < gvSkillsdetails.Rows.Count; i++)
                            {
                                ComboBox cmbSkill1 = (ComboBox)gvSkillsdetails.Rows[i].FindControl("ddlSkill");
                                cmbSkill1.Enabled = false;
                            }

                            for (int i = 0; i < gvEmployeeDomain.Rows.Count; i++)
                            {
                                ComboBox cmbEmployeeDomain = (ComboBox)gvEmployeeDomain.Rows[i].FindControl("ddlEmployeeDomain");
                                cmbEmployeeDomain.Enabled = false;
                            }

                            //29326-Subhra-end

                            if (Session[SessionNames.DefaultRow].ToString() != "Yes")
                            {
                                btnEdit.Visible = true;

                                btnSave.Visible = false;

                            }
                            //29326-Subhra-Start
                            //else
                            //{
                            //    gvSkillsdetails.Enabled = true;
                            // btnEdit.Visible = false;


                            //btnSave.Visible = true;
                            //}
                            //29326-Subhra-end

                            //Skillsdetails.Enabled = false;
                            //Enabling all the edit buttons.
                            for (int i = 0; i < gvSkillsdetails.Rows.Count; i++)
                            {
                                ImageButton btnImgDelete = (ImageButton)gvSkillsdetails.Rows[i].FindControl(IMGBTNDELETE);
                                btnImgDelete.Enabled = false;
                                //To solved issue id 19221
                                //Coded added by Rahul P
                                if (Session["DefaultRow"].ToString() != "Yes")
                                {
                                    Button btnImgAdd = (Button)gvSkillsdetails.FooterRow.FindControl(IMGBTNADD);
                                    btnImgAdd.Enabled = false;
                                }
                                else
                                {
                                    Button btnImgAdd = (Button)gvSkillsdetails.FooterRow.FindControl(IMGBTNADD);
                                    btnImgAdd.Enabled = true;
                                }
                                //End
                            }

                            //Siddhesh Arekar Domain Details 09032015 Start
                            for (int i = 0; i < gvEmployeeDomain.Rows.Count; i++)
                            {
                                ImageButton btnImgDeleteDomain = (ImageButton)gvEmployeeDomain.Rows[i].FindControl(IMGBTNDELETEDOMAIN);
                                btnImgDeleteDomain.Enabled = false;
                                //To solved issue id 19221
                                //Coded added by Rahul P
                                if (Session["DefaultRow"].ToString() != "Yes")
                                {
                                    Button btnImgAddDomain = (Button)gvEmployeeDomain.FooterRow.FindControl(IMGBTNADDDOMAIN);
                                    btnImgAddDomain.Enabled = false;
                                }
                                else
                                {
                                    Button btnImgAddDomain = (Button)gvEmployeeDomain.FooterRow.FindControl(IMGBTNADDDOMAIN);
                                    btnImgAddDomain.Enabled = true;
                                }
                            }
                            //Siddhesh Arekar Domain Details 09032015 End

                        }
                        else
                        {
                            //To solved issue id 19221
                            //Coded added by Rahul P
                            btnSavePrimarySkills.Enabled = true;
                            lbxPrimaryskills.Enabled = true;
                            //End
                            btnEdit.Visible = false;
                            //Skillsdetails.Enabled = true;
                        }
                    }
                }
                else
                {
                    Skillsdetails.Enabled = false;
                    pnlDomainDetails.Enabled = false;
                }

                if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR) || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM))
                {
                    lbxPrimaryskills.Visible = true;
                    lblPrimaryskills.Visible = true;
                    txtPrimarySkills.Enabled = false;
                    lblSkills.Visible = true;
                    lblText.Visible = true;

                    //pnlPrimarysSkills.Enabled = true;
                }
                else
                {
                    #region Coded By Sameer To View Primary Skill of employees

                    if (lbxPrimaryskills.Items.Count > 0)
                    {
                        foreach (ListItem objList in lbxPrimaryskills.Items)
                        {
                            if (objList.Selected == true)
                            {
                                if (string.IsNullOrEmpty(txtPrimarySkills.Text))
                                {
                                    txtPrimarySkills.Text = objList.Text;
                                }
                                else
                                {
                                    if (!txtPrimarySkills.Text.Contains(objList.Text))
                                        txtPrimarySkills.Text = txtPrimarySkills.Text + "," + objList.Text;
                                }
                                //pnlPrimarysSkills.Enabled = true;
                            }
                        }

                    }

                    EnableDisablePrimarySkillControls();

                    #endregion Coded By Sameer To View Primary Skill of employees

                }


                SavedControlVirtualPath = "~/EmployeeMenuUC.ascx";
                ReloadControl();

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
    /// Handles the Click event of the btnCancel control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
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
    /// Handles the Click event of the btnSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //Mohamed : Issue 41678 : 10/04/2013 : Starts
        //Desc : If only one skill added in an Employee skill page then its not getting saved. (Do not click Add Row) Saving on multiple skills, if Add row not click on last record then it's not getting saved in the system.

        DropDownList ddlYearFoot = (DropDownList)gvSkillsdetails.FooterRow.FindControl("ddlAddYear");
        DropDownList ddlMonthFoot = (DropDownList)gvSkillsdetails.FooterRow.FindControl("ddlAddMonth");
        DropDownList ddlProficiencyFoot = (DropDownList)gvSkillsdetails.FooterRow.FindControl("ddlAddProficiency");
        DropDownList ddlLastUsedFoot = (DropDownList)gvSkillsdetails.FooterRow.FindControl("ddlAddLastUsed");
        ComboBox ddlSkillFoot = (ComboBox)gvSkillsdetails.FooterRow.FindControl("ddlAddSkill");
        TextBox txtVersionFoot = (TextBox)gvSkillsdetails.FooterRow.FindControl("txtAddVersion");
        TextBox txtSkillFoot = (TextBox)gvSkillsdetails.FooterRow.FindControl("txtAddSkill");

        if (ddlSkillFoot.SelectedItem.Text != CommonConstants.SELECT || ddlYearFoot.SelectedItem.Text != YEARS || ddlMonthFoot.SelectedItem.Text != MONTHS || ddlProficiencyFoot.SelectedItem.Text != CommonConstants.SELECT || ddlLastUsedFoot.SelectedItem.Text != YEARS)
        {
            if (ddlSkillFoot.SelectedItem.Text == CommonConstants.SELECT)
            {
                lblError.Text = "Please select a Skill.";
                return;
            }
            if (ddlYearFoot.SelectedItem.Text == YEARS)
            {
                lblError.Text = "Please select a Years.";
                return;
            }
            if (ddlMonthFoot.SelectedItem.Text == MONTHS)
            {
                lblError.Text = "Please select a Months.";
                return;
            }
            if (ddlProficiencyFoot.SelectedItem.Text == CommonConstants.SELECT)
            {
                lblError.Text = "Please select a Proficiency.";
                return;
            }

            if (ddlLastUsedFoot.SelectedItem.Text == YEARS)
            {
                lblError.Text = "Please select a Last Used.";
                return;
            }
            if (ddlYearFoot.SelectedValue == CommonConstants.ZERO.ToString() && ddlMonthFoot.SelectedValue == CommonConstants.ZERO.ToString())
            {
                lblError.Text = "Experience can not be zero.";
                return;
            }

            if (ddlSkillFoot.SelectedItem.Text == "Others")
            {
                //Siddhesh Arekar : Issue - validation  :  28/09/2015 : Starts
                if (string.IsNullOrEmpty(txtSkillFoot.Text))
                {
                    lblError.Text = "Please specify other skill";
                    return;
                }
                else if (txtSkillFoot.Text.Trim().ToLower() == "other" || txtSkillFoot.Text.Trim().ToLower() == "others")
                {
                    lblError.Text = "Please specify valid other skill";
                    return;
                }
                //Siddhesh Arekar : Issue - validation  : 28/09/2015 : End
            }
            for (int i = 0; i < gvSkillsdetails.Rows.Count; i++)
            {
                ComboBox cmbSkill = (ComboBox)gvSkillsdetails.Rows[i].FindControl("ddlSkill");
                TextBox txtVersionNo = (TextBox)gvSkillsdetails.Rows[i].FindControl("txtVersion");
                if (cmbSkill.SelectedItem.Text != "Others" || ddlSkillFoot.SelectedItem.Text != "Others")
                {
                    if (cmbSkill.SelectedItem.Text == ddlSkillFoot.SelectedItem.Text)
                    {
                        //To solved issue id 19221
                        //Coded by Rahul P
                        if (txtVersionNo.Text == txtVersionFoot.Text)
                        {
                            lblError.Text = "Skill " + ddlSkillFoot.SelectedItem.Text + " With Version No " + txtVersionFoot.Text + " is already added.";
                            return;
                        }
                        //End
                    }
                }
            }
            BusinessEntities.SkillsDetails objSkillsDetailsFoot = new BusinessEntities.SkillsDetails();
            if ((Regex.IsMatch(ddlSkillFoot.SelectedValue, _numericExpression)))
            {
                objSkillsDetailsFoot.Skill = Convert.ToInt32(ddlSkillFoot.SelectedValue);
            }
            if (ddlSkillFoot.SelectedItem.Text != "Others")
            {
                objSkillsDetailsFoot.SkillName = ddlSkillFoot.SelectedItem.Text.Trim().ToString();
            }
            else
            {
                objSkillsDetailsFoot.SkillName = txtSkillFoot.Text.Trim();
                HfOtherskill.Value = txtSkillFoot.Text.Trim();
            }
            objSkillsDetailsFoot.Year = Convert.ToInt32(ddlYearFoot.SelectedValue);
            objSkillsDetailsFoot.Month = Convert.ToInt32(ddlMonthFoot.SelectedValue);
            objSkillsDetailsFoot.Proficiency = Convert.ToInt32(ddlProficiencyFoot.SelectedValue);
            objSkillsDetailsFoot.ProficiencyLevel = ddlProficiencyFoot.SelectedItem.Text.Trim().ToString();
            objSkillsDetailsFoot.LastUsed = Convert.ToInt32(ddlLastUsedFoot.SelectedValue);
            objSkillsDetailsFoot.SkillVersion = txtVersionFoot.Text.Trim().ToString();
            objSkillsDetailsFoot.SkillsId = CommonConstants.ZERO;
            objSkillsDetailsFoot.Mode = CommonConstants.ONE;

            SkillDetailsCollection.Add(objSkillsDetailsFoot);

            this.DoDatabind();

            HfIsDataModified.Value = CommonConstants.YES;
        }


        //Siddhesh Arekar Domain Details 09032015 Start
        BusinessEntities.Employee objEmployee;
        BusinessEntities.RaveHRCollection objSaveEmpDomainCollection = new BusinessEntities.RaveHRCollection();
        Rave.HR.BusinessLayer.Employee.Employee objEmpDomainBAL = new Rave.HR.BusinessLayer.Employee.Employee();
        BusinessEntities.RaveHRCollection objDeleteEmpDomainCollection = new BusinessEntities.RaveHRCollection();
        //Siddhesh Arekar Domain Details 09032015 End

        //Mohamed : Issue 41678 : 10/04/2013 : Ends
        BusinessEntities.SkillsDetails objSkillsDetails;
        BusinessEntities.RaveHRCollection objSaveSkillDetailsCollection = new BusinessEntities.RaveHRCollection();
        Rave.HR.BusinessLayer.Employee.SkillsDetails objSkillsDetailsBAL = new Rave.HR.BusinessLayer.Employee.SkillsDetails();
        BusinessEntities.RaveHRCollection objDeleteSkillsdetailsCollection = new BusinessEntities.RaveHRCollection();


        if (ViewState[SKILLDETAILSDELETE] != null)
            objDeleteSkillsdetailsCollection = (BusinessEntities.RaveHRCollection)ViewState[SKILLDETAILSDELETE];

        if (gvSkillsdetails.Rows.Count == 0 && objDeleteSkillsdetailsCollection.Count == 0)
        {
            lblError.Text = "Please add skill details to Save.";
            return;
        }
        // Mohamed : As it is not needed commented for performance : Start
        //for (int i = 0; i < gvSkillsdetails.Rows.Count; i++)
        //{
        //    //int count = 0;
        //    ComboBox cmbSkill1 = (ComboBox)gvSkillsdetails.Rows[i].FindControl("ddlSkill");

        //    for (int j = 0; j < gvSkillsdetails.Rows.Count; j++)
        //    {
        //        ComboBox cmbSkill2 = (ComboBox)gvSkillsdetails.Rows[j].FindControl("ddlSkill");
        //        if (cmbSkill1.SelectedItem.Text != "Others" || cmbSkill2.SelectedItem.Text != "Others")
        //        {
        //            //if (cmbSkill1.SelectedItem.Text == cmbSkill2.SelectedItem.Text)
        //            //{
        //            //    count++;
        //            //    if (count > 1)
        //            //    {
        //            //        lblError.Text = "Skill " + cmbSkill2.SelectedItem.Text + " is already present.";
        //            //        return;
        //            //    }
        //            //}
        //        }
        //    }
        //}
        // Mohamed : End
        for (int i = 0; i < gvSkillsdetails.Rows.Count; i++)
        {
            if (gvSkillsdetails.Rows[i].Visible == true)
            {
            objSkillsDetails = new BusinessEntities.SkillsDetails();

            DropDownList ddlYear = (DropDownList)gvSkillsdetails.Rows[i].FindControl("ddlYear");
            DropDownList ddlMonth = (DropDownList)gvSkillsdetails.Rows[i].FindControl("ddlMonth");
            DropDownList ddlProficiency = (DropDownList)gvSkillsdetails.Rows[i].FindControl("ddlProficiency");
            DropDownList ddlLastUsed = (DropDownList)gvSkillsdetails.Rows[i].FindControl("ddlLastUsed");
            ComboBox ddlSkill = (ComboBox)gvSkillsdetails.Rows[i].FindControl("ddlSkill");
            Label lblMonth = (Label)gvSkillsdetails.Rows[i].FindControl("lblMonth");
            Label lblYear = (Label)gvSkillsdetails.Rows[i].FindControl("lblYear");
            Label lblProficiency = (Label)gvSkillsdetails.Rows[i].FindControl("lblProficiency");
            Label lblLastUsed = (Label)gvSkillsdetails.Rows[i].FindControl("lblLastUsed");
            Label lblSkillId = (Label)gvSkillsdetails.Rows[i].FindControl("SkillId");
            TextBox txtVersion = (TextBox)gvSkillsdetails.Rows[i].FindControl("txtVersion");
            Label Mode = (Label)gvSkillsdetails.Rows[i].FindControl("lblMode");
            TextBox txtSkill = (TextBox)gvSkillsdetails.Rows[i].FindControl("txtSkill");

            if (ddlYear.SelectedValue == CommonConstants.ZERO.ToString() && ddlMonth.SelectedValue == CommonConstants.ZERO.ToString())
            {
                lblError.Text = "Experience can not be zero.";
                return;
            }

            if (ddlSkill.SelectedItem.Text == CommonConstants.SELECT)
            {
                lblError.Text = "Please select a Skill.";
                return;
            }
            if (ddlYear.SelectedItem.Text == YEARS)
            {
                lblError.Text = "Please select a Years.";
                return;
            }
            if (ddlMonth.SelectedItem.Text == MONTHS)
            {
                lblError.Text = "Please select a Months.";
                return;
            }
            if (ddlProficiency.SelectedItem.Text == CommonConstants.SELECT)
            {
                lblError.Text = "Please select a Proficiency.";
                return;
            }
            if (ddlLastUsed.SelectedItem.Text == YEARS)
            {
                lblError.Text = "Please select a Last Used.";
                return;
            }
            if (ddlYear.SelectedValue == CommonConstants.ZERO.ToString() && ddlMonth.SelectedValue == CommonConstants.ZERO.ToString())
            {
                lblError.Text = "Experience can not be zero.";
                return;
            }
            if (ddlSkill.SelectedItem.Text == "Others")
            {
                //Siddhesh Arekar : Issue - Validation Defect : 28/09/2015 : Starts
                if (string.IsNullOrEmpty(txtSkill.Text))
                {
                    lblError.Text = "Please specify other skill.";
                    return;
                }
                else if (txtSkill.Text.Trim().ToLower() == "other" || txtSkill.Text.Trim().ToLower() == "others")
                {
                    lblError.Text = "Please specify valid other skill.";
                    return;
                }
                //Siddhesh Arekar : Issue - Validation Defect : 28/09/2015 : End
            }

            //Mohamed : Issue 41678 : 10/04/2013 : Starts
            //Desc : If only one skill added in an Employee skill page then its not getting saved. (Do not click Add Row) Saving on multiple skills, if Add row not click on last record then it's not getting saved in the system.
            for (int j = i + 1; j < gvSkillsdetails.Rows.Count; j++)
            {
                ComboBox cmbSkillJ = (ComboBox)gvSkillsdetails.Rows[j].FindControl("ddlSkill");
                TextBox txtSkillJ = (TextBox)gvSkillsdetails.Rows[j].FindControl("txtSkill");
                TextBox txtVersionNoJ = (TextBox)gvSkillsdetails.Rows[j].FindControl("txtVersion");

                string valCmbSkillI = ddlSkill.SelectedItem.Text.ToLower().Trim();
                string valSkillOtherI = txtSkill.Text.ToLower().Trim();
                string valSkillVersionI = txtVersion.Text.ToLower().Trim();
                string valCmbSkillJ = cmbSkillJ.SelectedItem.Text.ToLower().Trim();
                string valSkillOtherJ = txtSkillJ.Text.ToLower().Trim();
                string valSkillVersionJ = txtVersionNoJ.Text.ToLower().Trim();

                if (cmbSkillJ.SelectedItem.Text == CommonConstants.SELECT)
                {
                    lblError.Text = "Please select a Skill.";
                    return;
                }
                //Siddhesh Arekar Domain Details 09032015 Start
                if (cmbSkillJ.SelectedItem.Text == "Others")
                {
                    if (string.IsNullOrEmpty(txtSkillJ.Text))
                    {
                        lblError.Text = "Please specify other skill";
                        return;
                    }
                }
                
                if (valCmbSkillJ != "others")
                {
                    if (valCmbSkillI != "others")
                    {
                        if (valCmbSkillI == valCmbSkillJ)
                        {
                            if (valSkillVersionI == valSkillVersionJ)
                            {
                                lblError.Text = "Skill " + cmbSkillJ.SelectedItem.Text + " With Version No " + txtVersionNoJ.Text + " is already added.";
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (valSkillOtherI == valSkillOtherJ)
                        {
                            if (valSkillVersionI == valSkillVersionJ)
                            {
                                lblError.Text = "Skill " + cmbSkillJ.SelectedItem.Text + " With Version No " + txtVersionNoJ.Text + " is already added.";
                                return;
                            }
                        }
                    }
                }
                else
                {
                    if (valCmbSkillI != "others")
                    {
                        if (valCmbSkillI == valSkillOtherJ)
                        {
                            if (valSkillVersionI == valSkillVersionJ)
                            {
                                lblError.Text = "Skill " + txtSkillJ.Text + " With Version No " + txtVersionNoJ.Text + " is already added.";
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (valSkillOtherI == valSkillOtherJ)
                        {
                            if (valSkillVersionI == valSkillVersionJ)
                            {
                                lblError.Text = "Skill " + txtSkillJ.Text + " With Version No " + txtVersionNoJ.Text + " is already added.";
                                return;
                            }
                        }
                    }
                    //Siddhesh Arekar Domain Details 09032015 End
                }
            }
            //Mohamed : Issue 41678 : 10/04/2013 : Ends
            if (ddlSkill.SelectedItem.Text.Trim() == "Others")
            {
                if (objSkillsDetailsBAL.Check_SkillCategory_Exists(txtSkill.Text.Trim()))
                {
                    lblError.Text = "Skill " + txtSkill.Text + " is already exists.";
                    return;
                }
            }

            if (txtSkill.Text.Trim() != string.Empty)
                objSkillsDetails.SkillName = txtSkill.Text.Trim().ToString();
            else
                objSkillsDetails.SkillName = ddlSkill.SelectedItem.Text.Trim().ToString();

            objSkillsDetails.Skill = Convert.ToInt32(ddlSkill.SelectedValue);
            objSkillsDetails.Year = Convert.ToInt32(ddlYear.SelectedValue);
            objSkillsDetails.Month = Convert.ToInt32(ddlMonth.SelectedValue);
            objSkillsDetails.Proficiency = Convert.ToInt32(ddlProficiency.SelectedValue);
            objSkillsDetails.ProficiencyLevel = ddlProficiency.SelectedItem.Text.Trim().ToString();
            objSkillsDetails.LastUsed = Convert.ToInt32(ddlLastUsed.SelectedValue);
            objSkillsDetails.SkillVersion = txtVersion.Text.Trim().ToString();
            objSkillsDetails.SkillsId = Convert.ToInt32(lblSkillId.Text.Trim().ToString());
            if (EMPId.Value == "" || EMPId.Value == string.Empty) return;
            objSkillsDetails.EMPId = int.Parse(EMPId.Value);
            if (objSkillsDetails.SkillsId != 0)
                objSkillsDetails.Mode = 2;
            else
                objSkillsDetails.Mode = CommonConstants.ONE;
            objSaveSkillDetailsCollection.Add(objSkillsDetails);
            }
        }


        //Siddhesh Arekar Domain Details 09032015 Start
        ComboBox ddlEmployeeDomain = (ComboBox)gvEmployeeDomain.FooterRow.FindControl("ddlAddEmployeeDomain");
        TextBox txtEmployeeDomain = (TextBox)gvEmployeeDomain.FooterRow.FindControl("txtAddEmployeedomain");

        for (int i = 0; i < gvEmployeeDomain.Rows.Count; i++)
        {
            if (gvEmployeeDomain.Rows[i].Visible == true)
            {
                ComboBox cmbDomainI = (ComboBox)gvEmployeeDomain.Rows[i].FindControl("ddlEmployeeDomain");
                TextBox txtDomainI = (TextBox)gvEmployeeDomain.Rows[i].FindControl("txtEmployeedomain");
                string valCmbDomainI = cmbDomainI.SelectedItem.Text.ToLower().Trim();
                string valtxtDomainI = txtDomainI.Text.ToLower().Trim();

                if (cmbDomainI.SelectedItem.Text == CommonConstants.SELECT)
                {
                    lblError.Text = "Please select a Employee Domain.";
                    return;
                }

                if (cmbDomainI.SelectedItem.Text == "Others")
                {
                    //Siddhesh Arekar : Issue - Validation Defect : 28/09/2015 : Starts
                    if (string.IsNullOrEmpty(txtDomainI.Text))
                    {
                        lblError.Text = "Please specify other Domain.";
                        return;
                    }
                    else if (txtDomainI.Text.Trim().ToLower() == "other" || txtDomainI.Text.Trim().ToLower() == "others")
                    {
                        lblError.Text = "Please specify valid other Domain.";
                        return;
                    }
                    //Siddhesh Arekar : Issue - Validation Defect : 28/09/2015 : End
                }

                for (int j = i + 1; j < gvEmployeeDomain.Rows.Count; j++)
                {
                    ComboBox cmbDomainJ = (ComboBox)gvEmployeeDomain.Rows[j].FindControl("ddlEmployeeDomain");
                    TextBox txtDomainJ = (TextBox)gvEmployeeDomain.Rows[j].FindControl("txtEmployeedomain");
                    string valCmbDomainJ = cmbDomainJ.SelectedItem.Text.ToLower().Trim();
                    string valtxtDomainJ = txtDomainJ.Text.ToLower().Trim();

                    if (cmbDomainJ.SelectedItem.Text == CommonConstants.SELECT)
                    {
                        lblError.Text = "Please select a Employee Domain.";
                        return;
                    }
                    if (cmbDomainJ.SelectedItem.Text == "Others")
                    {
                        if (string.IsNullOrEmpty(txtDomainJ.Text))
                        {
                            lblError.Text = "Please specify other Domain.";
                            return;
                        }
                        //Siddhesh Arekar : Issue - Validation Defect : 28/09/2015 : Starts
                        if (txtDomainJ.Text.Trim().ToLower() == "other" || txtDomainJ.Text.Trim().ToLower() == "others")
                        {
                            lblError.Text = "Please specify valid other Domain.";
                            return;
                        }
                        //Siddhesh Arekar : Issue - Validation Defect : 28/09/2015 : Starts
                    }

                    if (valCmbDomainJ != "others")
                    {
                        if (valCmbDomainI != "others")
                        {
                            if (valCmbDomainJ == valCmbDomainI)
                            {
                                lblError.Text = "Employee Domain " + cmbDomainI.SelectedItem.Text.Trim() + " is already added.";
                                return;
                            }
                        }
                        else
                        {
                            if (valCmbDomainJ == valtxtDomainI)
                            {
                                lblError.Text = "Employee Domain " + txtDomainI.Text.Trim() + " is already added.";
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (valCmbDomainI != "others")
                        {
                            if (valtxtDomainJ == valCmbDomainI)
                            {
                                lblError.Text = "Employee Domain " + cmbDomainI.SelectedItem.Text.Trim() + " is already added.";
                                return;
                            }
                        }
                        else
                        {
                            if (valtxtDomainJ == valtxtDomainI)
                            {
                                lblError.Text = "Employee Domain " + txtDomainI.Text.Trim() + " is already added.";
                                return;
                            }
                        }
                    }
                }
            }
        }
        
        for (int i = 0; i < gvEmployeeDomain.Rows.Count; i++)
        {
            if (gvEmployeeDomain.Rows[i].Visible == true)
            {
                objEmployee = new BusinessEntities.Employee();

                ComboBox ddlEmpDomain = (ComboBox)gvEmployeeDomain.Rows[i].FindControl("ddlEmployeeDomain");
                TextBox txtEmpDomain = (TextBox)gvEmployeeDomain.Rows[i].FindControl("txtEmployeedomain");
                if (ddlEmpDomain.SelectedItem.Text == CommonConstants.SELECT)
                {
                    lblError.Text = "Please select a Employee domain.";
                    return;
                }
                if (ddlEmpDomain.SelectedItem.Text == "Others")
                {
                    if (string.IsNullOrEmpty(txtEmpDomain.Text))
                    {
                        lblError.Text = "Please specify other Employee Domain";
                        return;
                    }
                }

                if (ddlEmpDomain.SelectedItem.Text == "Others")
                {
                    Common.AuthorizationManager.AuthorizationManager objAuMan = new Common.AuthorizationManager.AuthorizationManager();
                    string strCurrentUser = objAuMan.getLoggedInUserEmailId();
                    //Rave.HR.BusinessLayer.Employee.Employee objEmpDomainBAL = new Rave.HR.BusinessLayer.Employee.Employee();
                    int newDomainID = 0;
                    int insertStatus = objEmpDomainBAL.Employee_Add_Domain_Master(txtEmpDomain.Text.Trim(), (int)Common.EnumsConstants.Category.EmployeeDomain, strCurrentUser, out newDomainID);

                    if (insertStatus == 1)
                    {
                        objEmployee.EmployeeDomain = Convert.ToString(newDomainID);
                        objEmployee.EmployeeDomainName = txtEmpDomain.Text.Trim();
                    }
                    else if (insertStatus == 2)
                    {
                        lblError.Text = "Employee Domain " + txtEmpDomain.Text + " is already added.";
                        return;
                    }
                    else
                    {
                        lblError.Text = "Error occured. Please contact Administrator.";
                        return;
                    }
                }
                else
                {
                    objEmployee.EmployeeDomain = ddlEmpDomain.SelectedValue;
                }
                if (EMPId.Value == "" || EMPId.Value == string.Empty) return;
                objEmployee.EMPId = int.Parse(EMPId.Value);
                objSaveEmpDomainCollection.Add(objEmployee);
            }
        }

        //Siddhesh Arekar Domain Details 09032015 Start
        if (objDeleteSkillsdetailsCollection != null)
        {
            BusinessEntities.SkillsDetails obj = null;

            for (int i = 0; i < objDeleteSkillsdetailsCollection.Count; i++)
            {
                objSkillsDetails = new BusinessEntities.SkillsDetails();
                obj = (BusinessEntities.SkillsDetails)objDeleteSkillsdetailsCollection.Item(i);

                objSkillsDetails.SkillsId = obj.SkillsId;
                if (EMPId.Value == "" || EMPId.Value == string.Empty) return;
                objSkillsDetails.EMPId = int.Parse(EMPId.Value);
                objSkillsDetails.Mode = obj.Mode;

                objSaveSkillDetailsCollection.Add(objSkillsDetails);                
            }

        }
        objSkillsDetailsBAL.Manipulation(objSaveSkillDetailsCollection);
        objEmpDomainBAL.Manipulation(objSaveEmpDomainCollection, Convert.ToInt32(EMPId.Value));
        if (EMPId.Value != string.Empty)
        {
            int empID = Convert.ToInt32(EMPId.Value);
            this.PopulateGrid(empID);
            this.PopulateContols();
        }


        lblMessage.Text = "Skills details saved successfully.";
        HfIsDataModified.Value = string.Empty;
    }

    /// <summary>
    /// Handles the Click event of the btnSavePrimarySkills control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSavePrimarySkills_ClickEvtHandler(object sender, EventArgs e)
    {
        Rave.HR.BusinessLayer.Employee.SkillsDetails objSkillsDetailsBAL;
        Rave.HR.BusinessLayer.Employee.Employee objEmployeeSkillsBAL;
        try
        {
            if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR) || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM))
            {
                objSkillsDetailsBAL = new Rave.HR.BusinessLayer.Employee.SkillsDetails();
                objEmployeeSkillsBAL = new Rave.HR.BusinessLayer.Employee.Employee();
                string PrimarySkills = string.Empty;

                for (int i = 0; i < lbxPrimaryskills.Items.Count; i++)
                {
                    if (lbxPrimaryskills.Items[i].Selected)
                    {
                        PrimarySkills = PrimarySkills + lbxPrimaryskills.Items[i].Value;
                        PrimarySkills = PrimarySkills + ",";
                    }
                }

                objSkillsDetailsBAL.AddPrimarySkillDetails(employeeID, PrimarySkills);

                lblMessage.Text = "Primary Skills details saved successfully.";
            }

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
    /// Handles the RowDataBound event of the gvSkillsdetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
    protected void gvSkillsdetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlYear = (DropDownList)e.Row.Cells[2].FindControl("ddlYear");
            DropDownList ddlMonth = (DropDownList)e.Row.Cells[2].FindControl("ddlMonth");
            DropDownList ddlProficiency = (DropDownList)e.Row.Cells[3].FindControl("ddlProficiency");
            DropDownList ddlLastUsed = (DropDownList)e.Row.Cells[4].FindControl("ddlLastUsed");
            ComboBox ddlSkill = (ComboBox)e.Row.Cells[0].FindControl("ddlSkill");
            Label lblMonth = (Label)e.Row.Cells[5].FindControl("lblMonth");
            Label lblYear = (Label)e.Row.Cells[6].FindControl("lblYear");
            Label lblProficiency = (Label)e.Row.Cells[7].FindControl("lblProficiency");
            Label lblLastUsed = (Label)e.Row.Cells[8].FindControl("lblLastUsed");
            Label lblSkill = (Label)e.Row.Cells[9].FindControl("lblSkill");
            TextBox txtSkill = (TextBox)e.Row.Cells[0].FindControl("txtSkill");
            //HtmlTableRow tr_Skill = (HtmlTableRow)e.Row.Cells[0].FindControl("tr_Skill");

            SkillCollection = this.GetSkills();

            ddlSkill.DataSource = SkillCollection;
            ddlSkill.DataTextField = Common.CommonConstants.DDL_DataTextField;
            ddlSkill.DataValueField = Common.CommonConstants.DDL_DataValueField;
            ddlSkill.DataBind();

            ddlSkill.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
            ddlSkill.SelectedValue = lblSkill.Text;

            ddlYear.Items.Insert(CommonConstants.ZERO, "Years");

            if (ddlSkill.SelectedItem.Text == "Others")
            {
                ddlSkill.Visible = true;
                //txtSkill.Visible = true;
                txtSkill.Style["display"] = "";
                //txtSkill.Text = HfOtherskill.Value;
            }
            int startYearIndex = 1;
            for (int i = 0; i <= 40; i++)
            {
                ddlYear.Items.Insert(startYearIndex, new ListItem(i.ToString(), i.ToString()));
                startYearIndex++;
            };
            ddlYear.SelectedValue = lblYear.Text;

            ddlMonth.Items.Insert(CommonConstants.ZERO, "Months");
            int startMonthIndex = 1;
            for (int i = 0; i <= 12; i++)
            {
                ddlMonth.Items.Insert(startMonthIndex, new ListItem(i.ToString(), i.ToString()));
                startMonthIndex++;
            }

            ddlMonth.SelectedValue = lblMonth.Text;

            ProficiencyCollection = this.GetProficiencyLevel();
            //EmployeeDomainCollection = this.GetEmployeeDomains();

            ddlProficiency.DataSource = ProficiencyCollection;
            ddlProficiency.DataTextField = Common.CommonConstants.DDL_DataTextField;
            ddlProficiency.DataValueField = Common.CommonConstants.DDL_DataValueField;
            ddlProficiency.DataBind();

            ddlProficiency.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
            ddlProficiency.SelectedValue = lblProficiency.Text;

            ddlLastUsed.Items.Insert(CommonConstants.ZERO, YEARS);

            int startIndex = 1;
            for (int i = DateTime.Now.Year; i >= DateTime.Now.Year - 60; i--)
            {
                ddlLastUsed.Items.Insert(startIndex, new ListItem(i.ToString(), i.ToString()));
                startIndex++;
            }
            ddlLastUsed.SelectedValue = lblLastUsed.Text;

        }

        if (e.Row.RowType == DataControlRowType.EmptyDataRow)
        {

            DropDownList ddlYear = (DropDownList)gvSkillsdetails.Controls[0].Controls[0].FindControl("ddlEmptyYear");
            DropDownList ddlMonth = (DropDownList)gvSkillsdetails.Controls[0].Controls[0].FindControl("ddlEmptyMonth");
            DropDownList ddlProficiency = (DropDownList)gvSkillsdetails.Controls[0].Controls[0].FindControl("ddlEmptyProficiency");
            DropDownList ddlLastUsed = (DropDownList)gvSkillsdetails.Controls[0].Controls[0].FindControl("ddlEmptyLastUsed");
            ComboBox ddlSkill = (ComboBox)gvSkillsdetails.Controls[0].Controls[0].FindControl("ddlEmptySkill");

            SkillCollection = this.GetSkills();

            ddlSkill.DataSource = SkillCollection;
            ddlSkill.DataTextField = Common.CommonConstants.DDL_DataTextField;
            ddlSkill.DataValueField = Common.CommonConstants.DDL_DataValueField;
            ddlSkill.DataBind();

            ddlSkill.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);


            ddlYear.Items.Insert(CommonConstants.ZERO, "Years");
            int startYearIndex = 1;
            for (int i = 0; i <= 40; i++)
            {
                ddlYear.Items.Insert(startYearIndex, new ListItem(i.ToString(), i.ToString()));
                startYearIndex++;
            }


            ddlMonth.Items.Insert(CommonConstants.ZERO, "Months");
            int startMonthIndex = 1;
            for (int i = 0; i <= 12; i++)
            {
                ddlMonth.Items.Insert(startMonthIndex, new ListItem(i.ToString(), i.ToString()));
                startMonthIndex++;
            }


            ProficiencyCollection = this.GetProficiencyLevel();

            ddlProficiency.DataSource = ProficiencyCollection;
            ddlProficiency.DataTextField = Common.CommonConstants.DDL_DataTextField;
            ddlProficiency.DataValueField = Common.CommonConstants.DDL_DataValueField;
            ddlProficiency.DataBind();

            ddlProficiency.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);


            ddlLastUsed.Items.Insert(CommonConstants.ZERO, YEARS);

            int startIndex = 1;
            for (int i = DateTime.Now.Year; i >= DateTime.Now.Year - 60; i--)
            {
                ddlLastUsed.Items.Insert(startIndex, new ListItem(i.ToString(), i.ToString()));
                startIndex++;
            }

        }

    }

    /// <summary>
    /// Handles the RowCommand event of the gvSkillsdetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
    protected void gvSkillsdetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EmptyAdd")
        {
            DropDownList ddlYear = (DropDownList)gvSkillsdetails.Controls[0].Controls[0].FindControl("ddlEmptyYear");
            DropDownList ddlMonth = (DropDownList)gvSkillsdetails.Controls[0].Controls[0].FindControl("ddlEmptyMonth");
            DropDownList ddlProficiency = (DropDownList)gvSkillsdetails.Controls[0].Controls[0].FindControl("ddlEmptyProficiency");
            DropDownList ddlLastUsed = (DropDownList)gvSkillsdetails.Controls[0].Controls[0].FindControl("ddlEmptyLastUsed");
            ComboBox ddlSkill = (ComboBox)gvSkillsdetails.Controls[0].Controls[0].FindControl("ddlEmptySkill");
            TextBox txtVersion = (TextBox)gvSkillsdetails.Controls[0].Controls[0].FindControl("txtEmptyVersion");
            TextBox txtSkill = (TextBox)gvSkillsdetails.Controls[0].Controls[0].FindControl("txtEmptySkill");

            if (ddlSkill.SelectedItem.Text == CommonConstants.SELECT)
            {
                lblError.Text = "Please select a Skill.";
                return;
            }
            if (ddlYear.SelectedItem.Text == YEARS)
            {
                lblError.Text = "Please select a Years.";
                return;
            }
            if (ddlMonth.SelectedItem.Text == MONTHS)
            {
                lblError.Text = "Please select a Months.";
                return;
            }
            if (ddlProficiency.SelectedItem.Text == CommonConstants.SELECT)
            {
                lblError.Text = "Please select a Proficiency.";
                return;
            }
            if (ddlLastUsed.SelectedItem.Text == YEARS)
            {
                lblError.Text = "Please select a Last Used.";
                return;
            }
            if (ddlYear.SelectedValue == CommonConstants.ZERO.ToString() && ddlMonth.SelectedValue == CommonConstants.ZERO.ToString())
            {
                lblError.Text = "Experience can not be zero.";
                return;
            }

            if (ddlSkill.SelectedItem.Text == "Others")
            {
                if (string.IsNullOrEmpty(txtSkill.Text))
                {
                    lblError.Text = "Please specify other skill";
                    return;
                }

            }

            BusinessEntities.SkillsDetails objSkillsDetails = new BusinessEntities.SkillsDetails();

            if ((Regex.IsMatch(ddlSkill.SelectedValue, _numericExpression)))
            {
                objSkillsDetails.Skill = Convert.ToInt32(ddlSkill.SelectedValue);
            }

            if (ddlSkill.SelectedItem.Text != "Others")
            {
                objSkillsDetails.SkillName = ddlSkill.SelectedItem.Text.Trim().ToString();
            }
            else
            {
                objSkillsDetails.SkillName = txtSkill.Text.Trim();
                HfOtherskill.Value = txtSkill.Text.Trim();
            }

            objSkillsDetails.Year = Convert.ToInt32(ddlYear.SelectedValue);
            objSkillsDetails.Month = Convert.ToInt32(ddlMonth.SelectedValue);
            objSkillsDetails.Proficiency = Convert.ToInt32(ddlProficiency.SelectedValue);
            objSkillsDetails.ProficiencyLevel = ddlProficiency.SelectedItem.Text.Trim().ToString();
            objSkillsDetails.LastUsed = Convert.ToInt32(ddlLastUsed.SelectedValue);
            objSkillsDetails.SkillVersion = txtVersion.Text.Trim().ToString();
            objSkillsDetails.SkillsId = CommonConstants.ZERO;
            objSkillsDetails.Mode = CommonConstants.ONE;

            SkillDetailsCollection.Add(objSkillsDetails);

            this.DoDatabind();

            HfIsDataModified.Value = CommonConstants.YES;
        }
        if (e.CommandName == "Add")
        {
            DropDownList ddlYear = (DropDownList)gvSkillsdetails.FooterRow.FindControl("ddlAddYear");
            DropDownList ddlMonth = (DropDownList)gvSkillsdetails.FooterRow.FindControl("ddlAddMonth");
            DropDownList ddlProficiency = (DropDownList)gvSkillsdetails.FooterRow.FindControl("ddlAddProficiency");
            DropDownList ddlLastUsed = (DropDownList)gvSkillsdetails.FooterRow.FindControl("ddlAddLastUsed");
            ComboBox ddlSkill = (ComboBox)gvSkillsdetails.FooterRow.FindControl("ddlAddSkill");
            TextBox txtAddVersion = (TextBox)gvSkillsdetails.FooterRow.FindControl("txtAddVersion");
            TextBox txtAddSkill = (TextBox)gvSkillsdetails.FooterRow.FindControl("txtAddSkill");

            if (ddlSkill.SelectedItem.Text == CommonConstants.SELECT)
            {
                lblError.Text = "Please select a Skill.";
                return;
            }
            if (ddlYear.SelectedItem.Text == YEARS)
            {
                lblError.Text = "Please select a Years.";
                return;
            }
            if (ddlMonth.SelectedItem.Text == MONTHS)
            {
                lblError.Text = "Please select a Months.";
                return;
            }
            if (ddlProficiency.SelectedItem.Text == CommonConstants.SELECT)
            {
                lblError.Text = "Please select a Proficiency.";
                return;
            }
            if (ddlLastUsed.SelectedItem.Text == YEARS)
            {
                lblError.Text = "Please select a Last Used.";
                return;
            }
            if (ddlYear.SelectedValue == CommonConstants.ZERO.ToString() && ddlMonth.SelectedValue == CommonConstants.ZERO.ToString())
            {
                lblError.Text = "Experience can not be zero.";
                return;
            }

            if (ddlSkill.SelectedItem.Text == "Others")
            {
                //Siddhesh Arekar : Issue - Validation Defect : 28/09/2015 : Starts
                if (string.IsNullOrEmpty(txtAddSkill.Text))
                {
                    lblError.Text = "Please specify other skill.";
                    return;
                }
                else if (txtAddSkill.Text.Trim().ToLower() == "other" || txtAddSkill.Text.Trim().ToLower() == "others")
                {
                    lblError.Text = "Please specify valid other skill.";
                    return;
                }
                //Siddhesh Arekar : Issue - Validation Defect : 28/09/2015 : End
            }
            for (int i = 0; i < gvSkillsdetails.Rows.Count; i++)
            {
                ComboBox cmbSkill = (ComboBox)gvSkillsdetails.Rows[i].FindControl("ddlSkill");
                TextBox txtSkill = (TextBox)gvSkillsdetails.Rows[i].FindControl("txtSkill");
                TextBox txtVersionNo = (TextBox)gvSkillsdetails.Rows[i].FindControl("txtVersion");

                string strNewSkill = ddlSkill.SelectedItem.Text.ToLower().Trim();
                string strNewSkillOther = txtAddSkill.Text.ToLower().Trim();
                string strNewVersion = txtAddVersion.Text.ToLower().Trim();

                string strOldSkill = cmbSkill.SelectedItem.Text.ToLower().Trim();
                string strOldSkillOther = txtSkill.Text.ToLower().Trim();
                string strOldVersion = txtVersionNo.Text.ToLower().Trim();

                if (strNewSkill != "others")
                {
                    if (strOldSkill != "others")
                    {
                        if (strOldSkill == strNewSkill)
                        {
                            if (strNewVersion == strOldVersion)
                            {
                                lblError.Text = "Skill " + ddlSkill.SelectedItem.Text + " With Version No " + txtAddVersion.Text + " is already added.";
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (strOldSkillOther == strNewSkill)
                        {
                            if (strNewVersion == strOldVersion)
                            {
                                lblError.Text = "Skill " + ddlSkill.SelectedItem.Text + " With Version No " + txtAddVersion.Text + " is already added.";
                                return;
                            }
                        }
                    }
                }
                else
                {
                    if (strOldSkill != "others")
                    {
                        if (strOldSkill == strNewSkillOther)
                        {
                            if (strNewVersion == strOldVersion)
                            {
                                lblError.Text = "Skill " + txtSkill.Text + " With Version No " + txtAddVersion.Text + " is already added.";
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (strOldSkillOther == strNewSkillOther)
                        {
                            if (strNewVersion == strOldVersion)
                            {
                                lblError.Text = "Skill " + txtSkill.Text + " With Version No " + txtAddVersion.Text + " is already added.";
                                return;
                            }
                        }
                    }
                }
                //if (cmbSkill.SelectedItem.Text != "Others" || ddlSkill.SelectedItem.Text != "Others")
                //{
                //    if (cmbSkill.SelectedItem.Text == ddlSkill.SelectedItem.Text)
                //    {
                //        //To solved issue id 19221
                //        //Coded by Rahul P
                //        if (txtVersionNo.Text == txtAddVersion.Text)
                //        {
                //            lblError.Text = "Skill " + ddlSkill.SelectedItem.Text + " With Version No " + txtAddVersion.Text + " is already added.";
                //            return;
                //        }
                //        //End
                //    }
                //}
            }
            BusinessEntities.SkillsDetails objSkillsDetails = new BusinessEntities.SkillsDetails();
            if ((Regex.IsMatch(ddlSkill.SelectedValue, _numericExpression)))
            {
                objSkillsDetails.Skill = Convert.ToInt32(ddlSkill.SelectedValue);
            }
            if (ddlSkill.SelectedItem.Text != "Others")
            {
                objSkillsDetails.SkillName = ddlSkill.SelectedItem.Text.Trim().ToString();
            }
            else
            {
                objSkillsDetails.SkillName = txtAddSkill.Text.Trim();
                HfOtherskill.Value = txtAddSkill.Text.Trim();
            }
            objSkillsDetails.Year = Convert.ToInt32(ddlYear.SelectedValue);
            objSkillsDetails.Month = Convert.ToInt32(ddlMonth.SelectedValue);
            objSkillsDetails.Proficiency = Convert.ToInt32(ddlProficiency.SelectedValue);
            objSkillsDetails.ProficiencyLevel = ddlProficiency.SelectedItem.Text.Trim().ToString();
            objSkillsDetails.LastUsed = Convert.ToInt32(ddlLastUsed.SelectedValue);
            objSkillsDetails.SkillVersion = txtAddVersion.Text.Trim().ToString();
            objSkillsDetails.SkillsId = CommonConstants.ZERO;
            objSkillsDetails.Mode = CommonConstants.ONE;

            SkillDetailsCollection.Add(objSkillsDetails);

            this.DoDatabind();

            HfIsDataModified.Value = CommonConstants.YES;
        }

        DropDownList ddlAddYear = (DropDownList)gvSkillsdetails.FooterRow.FindControl("ddlAddYear");
        DropDownList ddlAddMonth = (DropDownList)gvSkillsdetails.FooterRow.FindControl("ddlAddMonth");
        DropDownList ddlAddProficiency = (DropDownList)gvSkillsdetails.FooterRow.FindControl("ddlAddProficiency");
        DropDownList ddlAddLastUsed = (DropDownList)gvSkillsdetails.FooterRow.FindControl("ddlAddLastUsed");
        ComboBox ddlAddSkill = (ComboBox)gvSkillsdetails.FooterRow.FindControl("ddlAddSkill");

        SkillCollection = this.GetSkills();

        ddlAddSkill.DataSource = SkillCollection;
        ddlAddSkill.DataTextField = Common.CommonConstants.DDL_DataTextField;
        ddlAddSkill.DataValueField = Common.CommonConstants.DDL_DataValueField;
        ddlAddSkill.DataBind();

        ddlAddSkill.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

        ddlAddYear.Items.Insert(CommonConstants.ZERO, "Years");
        int startYearIndex = 1;
        for (int i = 0; i <= 40; i++)
        {
            ddlAddYear.Items.Insert(startYearIndex, new ListItem(i.ToString(), i.ToString()));
            startYearIndex++;
        }

        ddlAddMonth.Items.Insert(CommonConstants.ZERO, "Months");
        int startMonthIndex = 1;
        for (int i = 0; i <= 12; i++)
        {
            ddlAddMonth.Items.Insert(startMonthIndex, new ListItem(i.ToString(), i.ToString()));
            startMonthIndex++;
        }


        ProficiencyCollection = this.GetProficiencyLevel();

        ddlAddProficiency.DataSource = ProficiencyCollection;
        ddlAddProficiency.DataTextField = Common.CommonConstants.DDL_DataTextField;
        ddlAddProficiency.DataValueField = Common.CommonConstants.DDL_DataValueField;
        ddlAddProficiency.DataBind();

        ddlAddProficiency.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

        ddlAddLastUsed.Items.Insert(CommonConstants.ZERO, YEARS);

        int startIndex = 1;
        for (int i = DateTime.Now.Year; i >= DateTime.Now.Year - 60; i--)
        {
            ddlAddLastUsed.Items.Insert(startIndex, new ListItem(i.ToString(), i.ToString()));
            startIndex++;
        }
    }

    /// <summary>
    /// Handles the RowDeleting event of the gvSkillsdetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewDeleteEventArgs"/> instance containing the event data.</param>
    protected void gvSkillsdetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int deleteRowIndex = 0;

        BusinessEntities.SkillsDetails objSkillsDetails = new BusinessEntities.SkillsDetails();

        deleteRowIndex = e.RowIndex;

        objSkillsDetails = (BusinessEntities.SkillsDetails)SkillDetailsCollection.Item(deleteRowIndex);
        objSkillsDetails.Mode = 3;

        if (ViewState[SKILLDETAILSDELETE] != null)
        {
            BusinessEntities.RaveHRCollection objDeleteSkillsDetailsCollection = (BusinessEntities.RaveHRCollection)ViewState[SKILLDETAILSDELETE];
            objDeleteSkillsDetailsCollection.Add(objSkillsDetails);

            ViewState[SKILLDETAILSDELETE] = objDeleteSkillsDetailsCollection;
        }
        else
        {
            BusinessEntities.RaveHRCollection objDeleteSkillsDetailCollection1 = new BusinessEntities.RaveHRCollection();

            objDeleteSkillsDetailCollection1.Add(objSkillsDetails);

            ViewState[SKILLDETAILSDELETE] = objDeleteSkillsDetailCollection1;
        }

        SkillDetailsCollection.RemoveAt(deleteRowIndex);

        this.DoDatabind();

        #region Modified By Mohamed Dangra
        // Mohamed : Issue 54923 : 10/03/2015 : Starts                        			  
        // Desc :  On Employee profile page-->Employee skills,When all the skills are deleted,  there is no option to add a new one
        if (SkillDetailsCollection.Count == 0)
        {
            gvSkillsdetails.DataSource = DefaultGridRow();
            gvSkillsdetails.DataBind();
            if (gvSkillsdetails.Rows[0] != null)
            {
                gvSkillsdetails.Rows[0].Visible = false;
            }
            Session[SessionNames.DefaultRow] = "Yes";
        }
        // Mohamed : Issue 54923 : 10/03/2015 : Ends
        #endregion Modified By Mohamed Dangra

        DropDownList ddlAddYear = (DropDownList)gvSkillsdetails.FooterRow.FindControl("ddlAddYear");
        DropDownList ddlAddMonth = (DropDownList)gvSkillsdetails.FooterRow.FindControl("ddlAddMonth");
        DropDownList ddlAddProficiency = (DropDownList)gvSkillsdetails.FooterRow.FindControl("ddlAddProficiency");
        DropDownList ddlAddLastUsed = (DropDownList)gvSkillsdetails.FooterRow.FindControl("ddlAddLastUsed");
        ComboBox ddlAddSkill = (ComboBox)gvSkillsdetails.FooterRow.FindControl("ddlAddSkill");

        SkillCollection = this.GetSkills();

        ddlAddSkill.DataSource = SkillCollection;
        ddlAddSkill.DataTextField = Common.CommonConstants.DDL_DataTextField;
        ddlAddSkill.DataValueField = Common.CommonConstants.DDL_DataValueField;
        ddlAddSkill.DataBind();

        ddlAddSkill.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

        ddlAddYear.Items.Insert(CommonConstants.ZERO, "Years");
        int startYearIndex = 1;
        for (int i = 0; i <= 40; i++)
        {
            ddlAddYear.Items.Insert(startYearIndex, new ListItem(i.ToString(), i.ToString()));
            startYearIndex++;
        }

        ddlAddMonth.Items.Insert(CommonConstants.ZERO, "Months");
        int startMonthIndex = 1;
        for (int i = 0; i <= 12; i++)
        {
            ddlAddMonth.Items.Insert(startMonthIndex, new ListItem(i.ToString(), i.ToString()));
            startMonthIndex++;
        }


        ProficiencyCollection = this.GetProficiencyLevel();

        ddlAddProficiency.DataSource = ProficiencyCollection;
        ddlAddProficiency.DataTextField = Common.CommonConstants.DDL_DataTextField;
        ddlAddProficiency.DataValueField = Common.CommonConstants.DDL_DataValueField;
        ddlAddProficiency.DataBind();

        ddlAddProficiency.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

        ddlAddLastUsed.Items.Insert(CommonConstants.ZERO, YEARS);

        int startIndex = 1;
        for (int i = DateTime.Now.Year - 60; i <= DateTime.Now.Year; i++)
        {
            ddlAddLastUsed.Items.Insert(startIndex, new ListItem(i.ToString(), i.ToString()));
            startIndex++;
        }

        HfIsDataModified.Value = CommonConstants.YES;
    }

    /// <summary>
    /// Handles the Click event of the btnEdit control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnEdit_Click(object sender, EventArgs e)
    {

        Session[SessionNames.PAGEMODE] = Common.MasterEnum.PageModeEnum.Edit;
        btnEdit.Visible = false;
        btnSave.Visible = true;
        //To solved issue id 19221
        //Coded by Rahul P
        gvSkillsdetails.Enabled = true;
        gvEmployeeDomain.Enabled = true;
        PageMode = Common.MasterEnum.PageModeEnum.Edit.ToString();
        employee = (BusinessEntities.Employee)Session[SessionNames.EMPLOYEEDETAILS];
        employee = this.GetEmployee(employee);
        PopulateGrid(employee.EMPId);
        pnlPrimarysSkills.Enabled = true;
        //End
        //Enabling all the edit buttons.
        for (int i = 0; i < gvSkillsdetails.Rows.Count; i++)
        {
            ImageButton btnImgDelete = (ImageButton)gvSkillsdetails.Rows[i].FindControl(IMGBTNDELETE);
            btnImgDelete.Enabled = true;
            Button btnImgAdd = (Button)gvSkillsdetails.FooterRow.FindControl(IMGBTNADD);
            btnImgAdd.Enabled = true;
        }

        for (int i = 0; i < gvEmployeeDomain.Rows.Count; i++)
        {
            ImageButton btnImgDeleteDomain = (ImageButton)gvEmployeeDomain.Rows[i].FindControl(IMGBTNDELETEDOMAIN);
            btnImgDeleteDomain.Enabled = true;
            Button btnImgAddDomain = (Button)gvEmployeeDomain.FooterRow.FindControl(IMGBTNADDDOMAIN);
            btnImgAddDomain.Enabled = true;
        }
    }

    /// <summary>
    /// Handles the ClickEvtHandler event of the lbxPrimarySkills control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void lbxPrimarySkills_ClickEvtHandler(object sender, EventArgs e)
    {
        lblSkills.Text = string.Empty;
        foreach (ListItem item in lbxPrimaryskills.Items)
        {
            if (item.Selected)
            {
                lblSkills.Text = lblSkills.Text + item.Text;
                lblSkills.Text += ", ";
            }
        }
        lblSkills.Text = lblSkills.Text.Remove(lblSkills.Text.Length - 2);
    }

    /// <summary>
    /// Handles the ClickEvtHandler event of the ddlEmptySkill control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlEmptySkill_ClickEvtHandler(object sender, EventArgs e)
    {
        TextBox txtSkill = (TextBox)gvSkillsdetails.Controls[0].Controls[0].FindControl("txtEmptySkill");
        ComboBox ddlSkill = (ComboBox)gvSkillsdetails.Controls[0].Controls[0].FindControl("ddlEmptySkill");
        txtSkill.Text = "";
        if (ddlSkill.SelectedItem.Text == "Others")
        {
            txtSkill.Visible = true;
        }
        else
        {
            txtSkill.Visible = false;
        }
    }

    /// <summary>
    /// Handles the ClickEvtHandler event of the ddlAddSkill control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlAddSkill_ClickEvtHandler(object sender, EventArgs e)
    {
        TextBox txtSkill = (TextBox)gvSkillsdetails.FooterRow.FindControl("txtAddSkill");
        ComboBox ddlSkill = (ComboBox)gvSkillsdetails.FooterRow.FindControl("ddlAddSkill");
        
        if (ddlSkill.SelectedItem.Text == "Others")
        {
            txtSkill.Text = "";
            txtSkill.Style["display"] = "";
        }
        else
        {
            txtSkill.Style["display"] = "none";
        }
    }

    /// <summary>
    /// Handles the ClickEvtHandler event of the ddlSkill control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlSkill_ClickEvtHandler(object sender, EventArgs e)
    {
        for (int i = 0; i < gvSkillsdetails.Rows.Count; i++)
        {
            ComboBox cmbSkill1 = (ComboBox)gvSkillsdetails.Rows[i].FindControl("ddlSkill");
            TextBox txtSkill = (TextBox)gvSkillsdetails.Rows[i].FindControl("txtSkill");
            
            if (cmbSkill1.SelectedItem.Text == "Others")
            {
                txtSkill.Text = "";
                txtSkill.Style["display"] = "";
            }
            else
            {
                txtSkill.Style["display"] = "none";
            }
        }

    }
    //Siddhesh Arekar Domain Details 09032015 Start
    /// <summary>
    /// Handles the RowDataBound event of the gvSkillsdetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
    protected void gvEmployeeDomain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ComboBox ddlEmployeeDomain = (ComboBox)e.Row.Cells[0].FindControl("ddlEmployeeDomain");
            Label lblEmployeeDomain = (Label)e.Row.Cells[1].FindControl("lblEmployeeDomain");
            TextBox txtEmployeeDomain = (TextBox)e.Row.Cells[0].FindControl("txtEmployeeDomain");
            //HtmlTableRow tr_EmployeeDomain = (HtmlTableRow)e.Row.Cells[0].FindControl("tr_EmployeeDomain");


            EmployeeDomainCollection = this.GetEmployeeDomains();

            ddlEmployeeDomain.DataSource = EmployeeDomainCollection;
            ddlEmployeeDomain.DataTextField = Common.CommonConstants.DDL_DataTextField;
            ddlEmployeeDomain.DataValueField = Common.CommonConstants.DDL_DataValueField;
            ddlEmployeeDomain.DataBind();

            ddlEmployeeDomain.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
            ddlEmployeeDomain.SelectedValue = lblEmployeeDomain.Text;

            if (ddlEmployeeDomain.SelectedItem.Text == "Others")
            {
                ddlEmployeeDomain.Visible = true;
                //tr_EmployeeDomain.Style["display"] = "";
                txtEmployeeDomain.Style["display"] = "";
            }
        }

        if (e.Row.RowType == DataControlRowType.EmptyDataRow)
        {
            ComboBox ddlEmployeeDomain = (ComboBox)gvEmployeeDomain.Controls[0].Controls[0].FindControl("ddlEmployeeDomain");

            EmployeeDomainCollection = this.GetEmployeeDomains();

            ddlEmployeeDomain.DataSource = EmployeeDomainCollection;
            ddlEmployeeDomain.DataTextField = Common.CommonConstants.DDL_DataTextField;
            ddlEmployeeDomain.DataValueField = Common.CommonConstants.DDL_DataValueField;
            ddlEmployeeDomain.DataBind();

            ddlEmployeeDomain.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
        }

        

    }

    /// <summary>
    /// Handles the RowCommand event of the gvEmployeeDomain control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewCommandEventArgs"/> instance containing the event data.</param>
    protected void gvEmployeeDomain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EmptyAdd")
        {
            ComboBox ddlEmployeeDomain = (ComboBox)gvEmployeeDomain.Controls[0].Controls[0].FindControl("ddlEmployeeDomain");
            TextBox txtEmployeeDomain = (TextBox)gvEmployeeDomain.Controls[0].Controls[0].FindControl("txtEmployeeDomain");

            if (ddlEmployeeDomain.SelectedItem.Text == CommonConstants.SELECT)
            {
                lblError.Text = "Please select a employee domain.";
                return;
            }

            if (ddlEmployeeDomain.SelectedItem.Text == "Others")
            {
                if (string.IsNullOrEmpty(txtEmployeeDomain.Text))
                {
                    lblError.Text = "Please specify other employee domain";
                    return;
                }
            }

            BusinessEntities.Employee objEmployeeDomain = new BusinessEntities.Employee();

            if ((Regex.IsMatch(ddlEmployeeDomain.SelectedValue, _numericExpression)))
            {
                objEmployeeDomain.EmployeeDomain = Convert.ToString(ddlEmployeeDomain.SelectedValue);
            }

            if (ddlEmployeeDomain.SelectedItem.Text != "Others")
            {
                objEmployeeDomain.EmployeeDomainName = ddlEmployeeDomain.SelectedItem.Text.Trim().ToString();
            }
            else
            {
                objEmployeeDomain.EmployeeDomainName = txtEmployeeDomain.Text.Trim();
                HfOtherEmpDomain.Value = txtEmployeeDomain.Text.Trim();
            }

            
            EmployeeDomainCollection.Add(objEmployeeDomain);

            this.DoDatabind();

            HfIsDataModified.Value = CommonConstants.YES;
        }
        if (e.CommandName == "Add")
        {
            ComboBox ddlEmployeeDomain = (ComboBox)gvEmployeeDomain.FooterRow.FindControl("ddlAddEmployeeDomain");
            TextBox txtEmployeeDomain = (TextBox)gvEmployeeDomain.FooterRow.FindControl("txtAddEmployeeDomain");

            if (ddlEmployeeDomain.SelectedItem.Text == CommonConstants.SELECT)
            {
                lblError.Text = "Please select a employee domain.";
                return;
            }

            if (ddlEmployeeDomain.SelectedItem.Text == "Others")
            {
                if (string.IsNullOrEmpty(txtEmployeeDomain.Text))
                {
                    lblError.Text = "Please specify other employee domain.";
                    return;
                }
                //Siddhesh Arekar : Issue - Validation Defect : 28/09/2015 : Starts
                else if (txtEmployeeDomain.Text.Trim().ToLower() == "other" || txtEmployeeDomain.Text.Trim().ToLower() == "others")
                {
                    lblError.Text = "Please specify valid other employee domain.";
                    return;
                }
                //Siddhesh Arekar : Issue - Validation Defect : 28/09/2015 : End
            }

            for (int i = 0; i < gvEmployeeDomain.Rows.Count; i++)
            {
                if (gvEmployeeDomain.Rows[i].Visible == true && gvEmployeeDomain.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    ComboBox cmbDomain = (ComboBox)gvEmployeeDomain.Rows[i].FindControl("ddlEmployeeDomain");
                    TextBox txtDomain = (TextBox)gvEmployeeDomain.Rows[i].FindControl("txtEmployeedomain");

                    if (cmbDomain.SelectedItem.Text == CommonConstants.SELECT)
                    {
                        lblError.Text = "Please select a Employee Domain.";
                        return;
                    }
                    if (cmbDomain.SelectedItem.Text == "Others")
                    {
                        if (string.IsNullOrEmpty(txtDomain.Text))
                        {
                            lblError.Text = "Please specify other Domain.";
                            return;
                        }
                    }

                    string strNewDomain = ddlEmployeeDomain.SelectedItem.Text.ToLower().Trim();
                    string strNewDomainOther = txtEmployeeDomain.Text.ToLower().Trim();
                    string valCmbDomain = cmbDomain.SelectedItem.Text.ToLower().Trim();
                    string valtxtDomain = txtDomain.Text.ToLower().Trim();
                    if (strNewDomain == "others")
                    {
                        if (valCmbDomain != "others")
                        {
                            if (valCmbDomain == strNewDomainOther)
                            {
                                lblError.Text = "Domain " + txtEmployeeDomain.Text.Trim() + " is already added.";
                                return;
                            }
                        }
                        else
                        {
                            if (valtxtDomain == strNewDomainOther)
                            {
                                lblError.Text = "Domain " + txtEmployeeDomain.Text.Trim() + " is already added.";
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (valCmbDomain != "others")
                        {
                            if (valCmbDomain == strNewDomain)
                            {
                                lblError.Text = "Domain " + ddlEmployeeDomain.SelectedItem.Text.Trim() + " is already added.";
                                return;
                            }
                        }
                        else
                        {
                            if (valtxtDomain == strNewDomain)
                            {
                                lblError.Text = "Domain " + ddlEmployeeDomain.SelectedItem.Text.Trim() + " is already added.";
                                return;
                            }
                        }
                    }
                }
            }

            BusinessEntities.Employee objEmployeeDomain = new BusinessEntities.Employee();
            if ((Regex.IsMatch(ddlEmployeeDomain.SelectedValue, _numericExpression)))
            {
                objEmployeeDomain.EmployeeDomain = Convert.ToString(ddlEmployeeDomain.SelectedValue);
            }
            if (ddlEmployeeDomain.SelectedItem.Text != "Others")
            {
                objEmployeeDomain.EmployeeDomainName = ddlEmployeeDomain.SelectedItem.Text.Trim().ToString();
            }
            else
            {
                objEmployeeDomain.EmployeeDomainName = txtEmployeeDomain.Text.Trim();
                HfOtherEmpDomain.Value = txtEmployeeDomain.Text.Trim();
            }

            objEmployeeDomainCollection.Add(objEmployeeDomain);

            this.DoDatabindEmployeeDomain();

            HfIsDataModified.Value = CommonConstants.YES;
        }

        ComboBox ddlAddEmployeeDomain = (ComboBox)gvEmployeeDomain.FooterRow.FindControl("ddlAddEmployeeDomain");

        EmployeeDomainCollection = this.GetEmployeeDomains();

        ddlAddEmployeeDomain.DataSource = EmployeeDomainCollection;
        ddlAddEmployeeDomain.DataTextField = Common.CommonConstants.DDL_DataTextField;
        ddlAddEmployeeDomain.DataValueField = Common.CommonConstants.DDL_DataValueField;
        ddlAddEmployeeDomain.DataBind();

        ddlAddEmployeeDomain.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

    }
    

    /// <summary>
    /// Handles the RowDeleting event of the gvSkillsdetails control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewDeleteEventArgs"/> instance containing the event data.</param>
    protected void gvEmployeeDomain_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int deleteRowIndex = 0;

        BusinessEntities.Employee objEmpDomain = new BusinessEntities.Employee();

        deleteRowIndex = e.RowIndex;

        objEmpDomain = (BusinessEntities.Employee)objEmployeeDomainCollection.Item(deleteRowIndex);
        //objEmpDomain.Mode = 3;

        if (ViewState[EMPLOYEEDOMAINDELETE] != null)
        {
            BusinessEntities.RaveHRCollection objDeleteEmployeeDomainCollection = (BusinessEntities.RaveHRCollection)ViewState[EMPLOYEEDOMAINDELETE];
            objDeleteEmployeeDomainCollection.Add(objEmpDomain);

            ViewState[EMPLOYEEDOMAINDELETE] = objDeleteEmployeeDomainCollection;
        }
        else
        {
            BusinessEntities.RaveHRCollection objDeleteEmployeeDomainCollection1 = new BusinessEntities.RaveHRCollection();

            objDeleteEmployeeDomainCollection1.Add(objEmpDomain);

            ViewState[EMPLOYEEDOMAINDELETE] = objDeleteEmployeeDomainCollection1;
        }

        objEmployeeDomainCollection.RemoveAt(deleteRowIndex);

        this.DoDatabindEmployeeDomain();
        if (objEmployeeDomainCollection.Count == 0)
        {
            gvEmployeeDomain.DataSource = DefaultGridRowForDomain();
            gvEmployeeDomain.DataBind();
            if (gvEmployeeDomain.Rows[0] != null)
            {
                gvEmployeeDomain.Rows[0].Visible = false;
            }
            Session[SessionNames.DefaultRow] = "Yes";
        }

        ComboBox ddlEmployeeDomain = (ComboBox)gvEmployeeDomain.FooterRow.FindControl("ddlAddEmployeeDomain");

        EmployeeDomainCollection = this.GetEmployeeDomains();

        ddlEmployeeDomain.DataSource = EmployeeDomainCollection;
        ddlEmployeeDomain.DataTextField = Common.CommonConstants.DDL_DataTextField;
        ddlEmployeeDomain.DataValueField = Common.CommonConstants.DDL_DataValueField;
        ddlEmployeeDomain.DataBind();

        ddlEmployeeDomain.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

        HfIsDataModified.Value = CommonConstants.YES;
    }
    /// <summary>
    /// Handles the ClickEvtHandler event of the ddlEmployeeDomain control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlEmployeeDomain_ClickEvtHandler(object sender, EventArgs e)
    {
        for (int i = 0; i < gvEmployeeDomain.Rows.Count; i++)
        {
            ComboBox cmbEmployeeDomain = (ComboBox)gvEmployeeDomain.Rows[i].FindControl("ddlEmployeeDomain");
            TextBox txtEmployeeDomain = (TextBox)gvEmployeeDomain.Rows[i].FindControl("txtEmployeeDomain");

            if (txtEmployeeDomain != null)
            {
                if (cmbEmployeeDomain.SelectedItem.Text == "Others")
                {
                    txtEmployeeDomain.Text = "";
                    txtEmployeeDomain.Style["display"] = "";
                }
                else
                {
                    txtEmployeeDomain.Style["display"] = "none";
                }
            }
        }

    }

    /// <summary>
    /// Handles the ClickEvtHandler event of the ddlAddEmployeeDomain control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlAddEmployeeDomain_ClickEvtHandler(object sender, EventArgs e)
    {
        TextBox txtEmployeeDomain = (TextBox)gvEmployeeDomain.FooterRow.FindControl("txtAddEmployeeDomain");
        ComboBox ddlgvEmployeeDomain = (ComboBox)gvEmployeeDomain.FooterRow.FindControl("ddlAddEmployeeDomain");

        if (ddlgvEmployeeDomain.SelectedItem.Text == "Others")
        {
            txtEmployeeDomain.Text = "";
            txtEmployeeDomain.Style["display"] = "";
        }
        else
        {
            txtEmployeeDomain.Style["display"] = "none";
        }
    }
    //Siddhesh Arekar Domain Details 09032015 End
    #endregion Protected Events

    //To solved issue id 19221
    //Coded by Rahul P
    #region Private Member Functions

    /// <summary>
    /// Creating the Empty grid when there is no data.
    /// </summary>
    private BusinessEntities.RaveHRCollection DefaultGridRow()
    {
        //Rave.HR.BusinessLayer.Employee.SkillsDetails objSkillsDetailsBAL;
        BusinessEntities.SkillsDetails objSkillsDetails = null;
        objSkillsDetails = new SkillsDetails();
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
        //objSkillsDetails.LastUsed = 0;
        //objSkillsDetails.Year = 0;
        //objSkillsDetails.Month = 0;
        //objSkillsDetails.ProficiencyLevel = "SELECT";
        //objSkillsDetails.Skill = 0;
        //objSkillsDetails.SkillVersion = "";

        raveHRCollection.Add(objSkillsDetails);

        return raveHRCollection;
    }
    //End

    /// <summary>
    /// Creating the Empty grid when there is no data.
    /// </summary>
    private BusinessEntities.RaveHRCollection DefaultGridRowForDomain()
    {
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
        raveHRCollection.Add(new BusinessEntities.Employee());
        return raveHRCollection;
    }

    /// <summary>
    /// Populates the grid.
    /// </summary>
    private void PopulateGrid(int employeeID)
    {
        SkillDetailsCollection = this.GetSkillDetails(employeeID);
        objEmployeeDomainCollection = this.GetEmployeeDomainsByEmpId(employeeID);
        //To solved issue id 19221
        //Coded by Rahul P
        if (SkillDetailsCollection.Count != 0)
        {
            //Binding the datatable to grid
            PageMode = "View";
            //pnlPrimarysSkills.Enabled = false;
            gvSkillsdetails.DataSource = SkillDetailsCollection;
            gvSkillsdetails.DataBind();
            Session[SessionNames.DefaultRow] = "No";
        }
        else
        {
            // gvSkillsdetails.Rows[0].Enabled = false;
            PageMode = "Edit";
            //pnlPrimarysSkills.Enabled = true;
            gvSkillsdetails.DataSource = DefaultGridRow();
            gvSkillsdetails.DataBind();
            if (gvSkillsdetails.Rows[0] != null)
            {
                gvSkillsdetails.Rows[0].Visible = false;
            }
            Session[SessionNames.DefaultRow] = "Yes";
        }
        //End


        if (objEmployeeDomainCollection.Count != 0)
        {
            //Binding the datatable to grid
            PageMode = "View";
            gvEmployeeDomain.DataSource = objEmployeeDomainCollection;
            gvEmployeeDomain.DataBind();
            Session[SessionNames.DefaultRow] = "No";
        }
        else
        {
            PageMode = "Edit";
            gvEmployeeDomain.DataSource = DefaultGridRowForDomain();
            gvEmployeeDomain.DataBind();
            if (gvEmployeeDomain.Rows[0] != null)
            {
                gvEmployeeDomain.Rows[0].Visible = false;
            }
            Session[SessionNames.DefaultRow] = "Yes";
        }


        EMPId.Value = employeeID.ToString().Trim();



        if (gvSkillsdetails.FooterRow != null)
        {
            DropDownList ddlAddYear = (DropDownList)gvSkillsdetails.FooterRow.FindControl("ddlAddYear");
            DropDownList ddlAddMonth = (DropDownList)gvSkillsdetails.FooterRow.FindControl("ddlAddMonth");
            DropDownList ddlAddProficiency = (DropDownList)gvSkillsdetails.FooterRow.FindControl("ddlAddProficiency");
            DropDownList ddlAddLastUsed = (DropDownList)gvSkillsdetails.FooterRow.FindControl("ddlAddLastUsed");
            ComboBox ddlAddSkill = (ComboBox)gvSkillsdetails.FooterRow.FindControl("ddlAddSkill");

            SkillCollection = this.GetSkills();

            ddlAddSkill.DataSource = SkillCollection;
            ddlAddSkill.DataTextField = Common.CommonConstants.DDL_DataTextField;
            ddlAddSkill.DataValueField = Common.CommonConstants.DDL_DataValueField;
            ddlAddSkill.DataBind();

            ddlAddSkill.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

            ddlAddYear.Items.Insert(CommonConstants.ZERO, "Years");
            int startYearIndex = 1;
            for (int i = 0; i <= 40; i++)
            {
                ddlAddYear.Items.Insert(startYearIndex, new ListItem(i.ToString(), i.ToString()));
                startYearIndex++;
            }

            ddlAddMonth.Items.Insert(CommonConstants.ZERO, "Months");
            int startMonthIndex = 1;
            for (int i = 0; i <= 12; i++)
            {
                ddlAddMonth.Items.Insert(startMonthIndex, new ListItem(i.ToString(), i.ToString()));
                startMonthIndex++;
            }

            ProficiencyCollection = this.GetProficiencyLevel();

            ddlAddProficiency.DataSource = ProficiencyCollection;
            ddlAddProficiency.DataTextField = Common.CommonConstants.DDL_DataTextField;
            ddlAddProficiency.DataValueField = Common.CommonConstants.DDL_DataValueField;
            ddlAddProficiency.DataBind();

            ddlAddProficiency.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

            ddlAddLastUsed.Items.Insert(CommonConstants.ZERO, YEARS);

            int startIndex = 1;
            for (int i = DateTime.Now.Year; i >= DateTime.Now.Year - 60; i--)
            {
                ddlAddLastUsed.Items.Insert(startIndex, new ListItem(i.ToString(), i.ToString()));
                startIndex++;
            }
        }


        if (gvEmployeeDomain.FooterRow != null)
        {
            ComboBox ddlAddEmployeeDomain = (ComboBox)gvEmployeeDomain.FooterRow.FindControl("ddlAddEmployeeDomain");

            EmployeeDomainCollection = this.GetEmployeeDomains();

            ddlAddEmployeeDomain.DataSource = EmployeeDomainCollection;
            ddlAddEmployeeDomain.DataTextField = Common.CommonConstants.DDL_DataTextField;
            ddlAddEmployeeDomain.DataValueField = Common.CommonConstants.DDL_DataValueField;
            ddlAddEmployeeDomain.DataBind();
            ddlAddEmployeeDomain.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
        }


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

            objSkillsDetails.EMPId = employeeID;

            raveHRCollection = objSkillsDetailsBAL.GetSkillsDetails(objSkillsDetails);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
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
    private void DoDatabind()
    {
        try
        {
            gvSkillsdetails.DataSource = SkillDetailsCollection;
            gvSkillsdetails.DataBind();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "BindGrid", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    private void DoDatabindEmployeeDomain()
    {
        try
        {
            gvEmployeeDomain.DataSource = objEmployeeDomainCollection;
            gvEmployeeDomain.DataBind();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "BindGrid", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Gets the skills.
    /// </summary>
    private RaveHRCollection GetSkills()
    {
        //Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        Rave.HR.BusinessLayer.Employee.SkillsDetails objSkillTypeBL = new Rave.HR.BusinessLayer.Employee.SkillsDetails();
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        raveHrColl = objSkillTypeBL.GetSkillTypesCategory();
        return raveHrColl;
    }

    /// <summary>
    /// Gets the proficiency level.
    /// </summary>
    private RaveHRCollection GetProficiencyLevel()
    {
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        raveHrColl = master.FillDropDownsBL((int)Common.EnumsConstants.Category.ProficiencyLevel);

        return raveHrColl;
    }

    /// <summary>
    /// Gets the Employee Domain.
    /// </summary>
    private RaveHRCollection GetEmployeeDomains()
    {
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        raveHrColl = master.FillDropDownsBL((int)Common.EnumsConstants.Category.EmployeeDomain);

        return raveHrColl;
    }

    /// <summary>
    /// Gets the Employee Domain.
    /// </summary>
    private RaveHRCollection GetEmployeeDomainsByEmpId(int employeeId)
    {
        //Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        //BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        //raveHrColl = master.FillDropDownsBL((int)Common.EnumsConstants.Category.EmployeeDomain);

        //return raveHrColl;

        BusinessEntities.Employee objEmployee;
        Rave.HR.BusinessLayer.Employee.Employee objEmployeeBL;


        //Rave.HR.BusinessLayer.Employee.SkillsDetails objSkillsDetailsBAL;
        //BusinessEntities.SkillsDetails objSkillsDetails;

        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            string empDomain = "";
            objEmployeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
            objEmployee = new BusinessEntities.Employee();

            objEmployee.EMPId = employeeID;

            raveHRCollection = objEmployeeBL.Employee_GetEmployeeDomain(objEmployee);          

            
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetSkillDetails", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

        return raveHRCollection;

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
        //this.PopulateControls();
        if (Session[SessionNames.EMPLOYEEDETAILS] != null)
        {
            employee = (BusinessEntities.Employee)Session[SessionNames.EMPLOYEEDETAILS];
            employee = this.GetEmployee(employee);
            ////To solved issue id 19221
            //Coded by Rahul P
            //Start
            lbxPrimaryskills.SelectedIndex = 0;
            lblempName.Text = employee.FirstName.ToUpper() + " " + employee.LastName.ToUpper();
            this.PopulateGrid(employee.EMPId);
            this.PopulateContols(employee.EMPId);

        }

        if (SkillDetailsCollection.Count != 0)
        {
            //gvSkillsdetails.Enabled = false;
            //Skillsdetails.Enabled = false;
            //gvSkillsdetails.Enabled = false;
            btnEdit.Visible = true;
            btnSave.Visible = false;
            btnSavePrimarySkills.Enabled = false;
            lbxPrimaryskills.Enabled = false;

            for (int i = 0; i < gvSkillsdetails.Rows.Count; i++)
            {
                ImageButton btnImgDelete = (ImageButton)gvSkillsdetails.Rows[i].FindControl(IMGBTNDELETE);
                btnImgDelete.Enabled = false;
                if (Session["DefaultRow"].ToString() != "Yes")
                {
                    Button btnImgAdd = (Button)gvSkillsdetails.FooterRow.FindControl(IMGBTNADD);
                    btnImgAdd.Enabled = false;
                }
                else
                {
                    Button btnImgAdd = (Button)gvSkillsdetails.FooterRow.FindControl(IMGBTNADD);
                    btnImgAdd.Enabled = true;
                }
            }
            //PageMode = "View";
        }
        else
        {
            //gvSkillsdetails.Enabled = true;
            //Skillsdetails.Enabled = true;
            btnEdit.Visible = false;
            btnSave.Visible = true;
            //PageMode = "Edit";


            btnSavePrimarySkills.Enabled = true;
            lbxPrimaryskills.Enabled = true;
        }
        //End


        if (EmployeeDomainCollection.Count != 0)
        {
            for (int i = 0; i < gvEmployeeDomain.Rows.Count; i++)
            {
                ImageButton btnImgDeleteDomain = (ImageButton)gvSkillsdetails.Rows[i].FindControl(IMGBTNDELETEDOMAIN);
                btnImgDeleteDomain.Enabled = false;
                if (Session["DefaultRow"].ToString() != "Yes")
                {
                    Button btnImgAddDomain = (Button)gvSkillsdetails.FooterRow.FindControl(IMGBTNADDDOMAIN);
                    btnImgAddDomain.Enabled = false;
                }
                else
                {
                    Button btnImgAddDomain = (Button)gvSkillsdetails.FooterRow.FindControl(IMGBTNADDDOMAIN);
                    btnImgAddDomain.Enabled = true;
                }
            }            
        }

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

    /// <summary>
    /// Gets the primary skill details.
    /// </summary>
    /// <param name="employeeID">The employee ID.</param>
    /// <returns></returns>
    private void GetPrimarySkillDetails(int employeeID)
    {
        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            //Calling Business layer FillDropDown Method
            raveHRCollection = master.FillDropDownsBL(Convert.ToInt32(Common.EnumsConstants.Category.PrimarySkills));

            //Check Collection is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSOurce to Collection
                lbxPrimaryskills.DataSource = raveHRCollection;

                //Assign DataText Filed to DropDown
                lbxPrimaryskills.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign DataValue Field to Dropdown
                lbxPrimaryskills.DataValueField = CommonConstants.DDL_DataValueField;

                //Bind Dropdown
                lbxPrimaryskills.DataBind();

                //Insert Select as a item for dropdown
                lbxPrimaryskills.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                raveHRCollection.Clear();
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                CLASS_NAME, "GetPrimarySkillDetails", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// Populates the contols.
    /// </summary>
    private void PopulateContols()
    {
        Rave.HR.BusinessLayer.Employee.SkillsDetails objSkillsDetailsBAL;
        BusinessEntities.SkillsDetails objSkillsDetails;

        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            objSkillsDetailsBAL = new Rave.HR.BusinessLayer.Employee.SkillsDetails();
            objSkillsDetails = new BusinessEntities.SkillsDetails();

            raveHRCollection = objSkillsDetailsBAL.FillDropDownsBL(employeeID);

            lblSkills.Text = string.Empty;
            foreach (KeyValue<string> keyvalue in raveHRCollection)
            {
                lbxPrimaryskills.Items.FindByValue(keyvalue.KeyName).Selected = true;
                lblSkills.Text = lblSkills.Text + keyvalue.Val;
                lblSkills.Text += ", ";

            }

            if (lblSkills.Text.Length != 0)

                lblSkills.Text = lblSkills.Text.Remove(lblSkills.Text.Length - 2);


        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetPrimarySkillDetails", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    private void PopulateContols(int empId)
    {
        Rave.HR.BusinessLayer.Employee.SkillsDetails objSkillsDetailsBAL;
        BusinessEntities.SkillsDetails objSkillsDetails;

        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            objSkillsDetailsBAL = new Rave.HR.BusinessLayer.Employee.SkillsDetails();
            objSkillsDetails = new BusinessEntities.SkillsDetails();

            raveHRCollection = objSkillsDetailsBAL.FillDropDownsBL(empId);

            lblSkills.Text = string.Empty;
            //lbxPrimaryskills.SelectedIndex = 0;
            if (lbxPrimaryskills.Items.Count > 0)
            {
                foreach (ListItem objList in lbxPrimaryskills.Items)
                {
                    if (objList.Selected == true)
                    {
                        objList.Selected = false;
                    }
                }
            }
            foreach (KeyValue<string> keyvalue in raveHRCollection)
            {
                lbxPrimaryskills.Items.FindByValue(keyvalue.KeyName).Selected = true;
                lblSkills.Text = lblSkills.Text + keyvalue.Val;
                lblSkills.Text += ", ";


            }

            if (lblSkills.Text.Length != 0)
                lblSkills.Text = lblSkills.Text.Remove(lblSkills.Text.Length - 2);


        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetPrimarySkillDetails", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// Method to Enable/Disable PrimarySkill Controls
    /// </summary>
    private void EnableDisablePrimarySkillControls()
    {
        lbxPrimaryskills.Visible = false;
        lbxPrimaryskills.Enabled = false;
        lblMsg.Visible = false;
        mandPriSkills.Visible = false;
        btnSavePrimarySkills.Visible = false;
        btnSavePrimarySkills.Enabled = false;
        txtPrimarySkills.Visible = true;
        lblSkills.Visible = false;
        lblText.Visible = false;
    }

    #endregion Private Member Functions


}
