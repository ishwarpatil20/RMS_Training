using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Common;
using Common.AuthorizationManager;
using System.IO;
using System.Text;
using System.Data;
using Rave.HR.BusinessLayer.Interface;
using Rave.HR.BusinessLayer;
using BusinessEntities;
using Common.Constants;
using System.Web;

public partial class EmployeeResignationDetails : BaseClass
{
    #region Private Field Members

    BusinessEntities.Employee employee = new BusinessEntities.Employee();
    ArrayList arrRolesForUser = new ArrayList();

    string UserRaveDomainId;
    string UserMailId;
    const string ReadOnly = "readonly";
    char[] SPILITER_SLASH = { '/' };
    const string imagePath = @"~\Images";
    const string BackSlash = @"\";
    const string IMAGES = "Images";
    /// <summary>
    /// 
    /// </summary>
    const string INDEX = "index";

    const string CLASS_NAME = "Employee.aspx.cs";

    /// <summary>
    /// Rave Email domain
    /// </summary>
    /// 
    //Googleconfigurable
    //private const string RAVE_DOMAIN = "@rave-tech.com";
    string PageMode = string.Empty;
    private string EMERGENCYDETAILS = "EmergencyDetails";
    const string PAGETYPE = "pagetype";
    const string PAGETYPEEMPLOYEESUMMERY = "EMPLOYEESUMMERY";
    protected EmployeeMenuUC BubbleControl;
    private int employeeID = 0;

    const string ROLLBACK_RESIGNATION_MESSAGE = "Employee Resignation Details Rolled back successfully and email notification is sent.";
    BusinessEntities.ParameterCriteria objParameter = new BusinessEntities.ParameterCriteria();

    #endregion

    #region Properties

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

    #endregion Properties

    #region Protected Methods

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
            lblMessage.Text = string.Empty;
            lblConfirmMsg.Text = string.Empty;

            btnSave.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return ButtonClickValidate();");
            //26137-Ambar-Start
            //btnRollBack.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return ConfirmForRollBackResign();");
            //26137-Ambar-End
            //Poonam : Issue : Disable Button : Starts
            btnSave.OnClientClick = "if(ButtonClickValidate()){" + ClientScript.GetPostBackEventReference(btnSave, null) + "}";
            btnRollBack.OnClientClick = "if(ConfirmForRollBackResign()){" + ClientScript.GetPostBackEventReference(btnRollBack, null) + "}";
            //Poonam : Issue : Disable Button : Ends
            //btnRMFM.Attributes.Add(CommonConstants.EVENT_ONCLICK, "return SelectProjectDetails();");

            Rave.HR.BusinessLayer.MRF.MRFRoles mrfRoles = new Rave.HR.BusinessLayer.MRF.MRFRoles();
            // Get logged-in user's email id
            AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
            UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
            UserMailId = UserRaveDomainId.Replace("co.in", "com");
            AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
            //ArrayList arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());
            arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());

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
                #region Modified By Mohamed Dangra
                // Mohamed : Issue 50959 : 16/05/2014 : Starts                        			  
                // Desc : Roll back button should invisible for Inactive employee

                if (employee.StatusId == 142)
                {
                    //31145-Subhra-Start
                    if (employee.ResignationReason != string.Empty)
                        btnRollBack.Visible = true;
                    //31145-Subhra-end
                }

                // Mohamed : Issue 50959 : 16/05/2014 : Ends
                #endregion Modified By Mohamed Dangra
                this.GetEmployeeStatus();
                this.PopulateControls();

                //Ishwar 20022015 : NIS RMS : Start
                string strUserIdentity = string.Empty;
                strUserIdentity = HttpContext.Current.ApplicationInstance.Session["WindowsUsername"].ToString().Trim();
                BusinessEntities.Employee Employee = new BusinessEntities.Employee();
                Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
                Employee = employeeBL.GetNISEmployeeList(strUserIdentity);
                if (!String.IsNullOrEmpty(Employee.WindowsUserName))
                {
                    pnlResignationDetails.Enabled = false;
                    btnRMFMHTML.Visible = false;
                    btnSave.Visible = false;
                    btnRollBack.Visible = false;
                }
                //Ishwar 20022015 : NIS RMS : End
            }

            SavedControlVirtualPath = "~/EmployeeMenuUC.ascx";
            ReloadControl();
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
    /// Handles the Click event of the btnSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateControls())
            {
                Boolean Flag, B_ClientName = false;
                string ClientName = string.Empty;
                employee = (BusinessEntities.Employee)Session[Common.SessionNames.EMPLOYEEDETAILS];

                if (Request.QueryString[QueryStringConstants.EMPID] != null)
                    employee.EMPId = Convert.ToInt32(DecryptQueryString(QueryStringConstants.EMPID).ToString());
                if (employee != null)
                {
                    Rave.HR.BusinessLayer.Employee.Employee addEmployeeBAL = new Rave.HR.BusinessLayer.Employee.Employee();
                    AuthorizationManager authoriseduser = new AuthorizationManager();

                    if (ucDatePickerLastWorkDay.Visible)
                    {
                        if (employee.StatusId != Convert.ToInt32(ddlStatus.SelectedItem.Value.ToString()) || employee.ResignationDate.ToString("dd/MM/yyyy") != ucDatePickerResignationDate.Text || employee.ResignationReason != txtResignationReason.Text || employee.LastWorkingDay.ToString("dd/MM/yyyy") != ucDatePickerLastWorkDay.Text)
                            Flag = true;
                    }
                    else if (employee.StatusId != Convert.ToInt32(ddlStatus.SelectedItem.Value.ToString()) || employee.ResignationDate.ToString("dd/MM/yyyy") != ucDatePickerResignationDate.Text || employee.ResignationReason != txtResignationReason.Text)
                    {
                        Flag = true;
                    }
                    Flag = true;

                    if (Flag)
                    {
                        objParameter.EMailID = UserMailId;
                        employee.StatusId = Convert.ToInt32(ddlStatus.SelectedItem.Value.ToString());
                        employee.LastWorkingDay = ucDatePickerLastWorkDay.Text.Trim() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(ucDatePickerLastWorkDay.Text);
                        employee.ResignationDate = ucDatePickerResignationDate.Text.Trim() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(ucDatePickerResignationDate.Text);
                        employee.ResignationReason = txtResignationReason.Text.Trim();
                        employee.LastModifiedByMailId = UserMailId;
                        employee.LastModifiedDate = DateTime.Now.Date;
                        employee.FullName = employee.FirstName.Trim() + " " + employee.LastName.Trim();

                        //update the session values for an employee.
                        Session[SessionNames.EMPLOYEEDETAILS] = employee;

                        //Siddharth 23-02-2015 
                        //Get Projects allocated of resigned Employee
                        RaveHRCollection raveHRCollection = new RaveHRCollection();
                        Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
                        //Get the allocated project details of employee
                        raveHRCollection = employeeBL.GetEmployeesAllocation(employee);
                        B_ClientName = false;

                        if (raveHRCollection != null)
                        {
                            if (raveHRCollection.Count > 0)
                            {
                                for (int i = 0; i < raveHRCollection.Count; i++)
                                {
                                    ClientName = ((BusinessEntities.Employee)(raveHRCollection.Item(i))).ClientName;
                                    if (ClientName.ToUpper().Contains("NPS") || ClientName.ToUpper().Contains("NORTHGATE"))
                                        B_ClientName = true;
                                }
                            }
                        }

                        //--Get mailIds
                        if (ddlStatus.SelectedItem.Text == "Active")
                        {
                            addEmployeeBAL.UpdateEmployeeResignationDetails(employee);

                            addEmployeeBAL.SendMailEmployeeSeperationFromCompany(employee, B_ClientName);

                            lblConfirmMsg.Text = "Employee Resignation Details saved successfully and email notification is sent.";

                            lblMessage.Visible = true;
                        }
                        if (ddlStatus.SelectedItem.Text == "InActive")
                        {
                            ExitEmployee(employee, B_ClientName);
                        }
                        btnRollBack.Visible = true;
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnUpdate_Click", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnCancel_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    //Aarohi : Issue 28572(CR) : 05/01/2012 : Start
    /// <summary>
    /// Handles the Click event of the btnRMFM control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    //protected void btnRMFM_Click(object sender, EventArgs e)
    //{

    //    try
    //    {
    //        string url = "ReportingOrFunctionalManager.aspx?" + URLHelper.SecureParameters("EMPId", employee.EMPId.ToString()) + "&" + URLHelper.CreateSignature(employee.EMPId.ToString());

    //        // string script = "<script type='text/javascript'> window.open('" + url + "', null, 'location=center,toolbar=no,menubar=no,directories=yes,status=yes,resizable=yes,scrollbars=yes,height=600,width=1000, top=0,left=250'); </script>";
    //        //comment by mahendra
    //        string script = "<script type='text/javascript'> window.showModalDialog('" + url + "', null, 'dialogHeight:400px; dialogWidth:1000px; center:yes'); </script>";

    //        this.ClientScript.RegisterStartupScript(this.GetType(), "clientScript", script);
    //    }
    //    //catches RaveHRException exception
    //    catch (RaveHRException ex)
    //    {
    //        LogErrorMessage(ex);
    //    }
    //    catch (Exception ex)
    //    {
    //        RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnRMFM_Click", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
    //        LogErrorMessage(objEx);
    //    }

    //}
    //Aarohi : Issue 28572(CR) : 05/01/2012 : End

    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlStatus control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlStatus.SelectedItem.Text == "Active")
            {
                ucDatePickerLastWorkDay.Visible = false;
                lbLstWrkDay.Visible = false;
                lblmandatoryLstWrkDay.Visible = false;
                ucDatePickerLastWorkDay.Visible = false;
            }
            else
            {
                ucDatePickerLastWorkDay.Visible = true;
                lbLstWrkDay.Visible = true;
                lblmandatoryLstWrkDay.Visible = true;
                ucDatePickerLastWorkDay.Visible = true;
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlStatus_SelectedIndexChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the Click event of the lnkSaveBtn control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void lnkSaveBtn_Click(object sender, EventArgs e)
    {
        try
        {
            System.Windows.Forms.DialogResult dialougeResult = System.Windows.Forms.MessageBox.Show(CommonConstants.DATANOTSAVED, "", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation);
            if (dialougeResult.ToString() == System.Windows.Forms.DialogResult.Yes.ToString())
            {
                btnSave_Click(null, null);
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "lnkSaveBtn_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    ///// <summary>
    ///// Handles the Click event of the lnkSaveBtn control.
    ///// </summary>
    ///// <param name="sender">The source of the event.</param>
    ///// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnRollBack_Click(object sender, EventArgs e)
    {
        try
        {
            Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();

            if (txtResignationReason.Text.Trim() == null || txtResignationReason.Text.Trim() == ""
                || txtResignationReason.Text.Trim() == string.Empty)
            {
                lblMessage.Text = "Plese enter details";
                return;
            }
            else
            {
                //Mohamed : Issue 41564 : 04/04/2013 : Starts
                //Desc : Employee’s name is missing in subject line. It should have been “Employee Name : Kalpesh Hodar”. Another same case attached
                employee.FullName = employee.FirstName.Trim() + " " + employee.LastName.Trim();
                //Mohamed : Issue 41564 : 04/04/2013 : Ends

                // Roll back Resign of employee from company (Cancel).
                employeeBL.RollBackEmployeeResignationDetailsBL(employee);

                //Send mail for Roll back Resign.
                employeeBL.SendMailEmployeeRollBackResignFromCompany(employee);

                lblConfirmMsg.Text = ROLLBACK_RESIGNATION_MESSAGE;

                lblMessage.Visible = true;
                txtResignationReason.Text = string.Empty;
                ucDatePickerResignationDate.Text = string.Empty;

                //Mohamed : Issue 41715 : 04/04/2013 : Starts
                //Desc : Employee Detail/Resignation Detail :- after Rollback, if we click on "Resignation Detail" from left panel it display the detail even after Rollback
                employee.LastWorkingDay = DateTime.MinValue;
                employee.ResignationDate = DateTime.MinValue;
                employee.ResignationReason = string.Empty;

                //Mohamed : Issue 41715 : 04/04/2013 : Ends
                //Poonam : Issue : 56656 : Starts
                btnSave.Visible = true;
                //Poonam : Issue : 56656 : Ends
                btnRollBack.Visible = false;
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnRollBack_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    #endregion

    #region Private Method

    /// <summary>
    /// Gets the employee status.
    /// </summary>
    private void GetEmployeeStatus()
    {
        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        //Declaring COllection class object
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        //Calling Fill dropdown Business layer method to fill 
        //the dropdown from Master class.
        raveHrColl = master.FillDropDownsBL((int)Common.EnumsConstants.Category.EmployeeStatus);

        ddlStatus.DataSource = raveHrColl;
        ddlStatus.DataTextField = Common.CommonConstants.DDL_DataTextField;
        ddlStatus.DataValueField = Common.CommonConstants.DDL_DataValueField;
        ddlStatus.DataBind();
        ddlStatus.Items.Insert(0, CommonConstants.SELECT);

    }

    /// <summary>
    /// Populates the controls.
    /// </summary>
    private void PopulateControls()
    {
        try
        {

            if (employee == null)
            {
                Response.Redirect(CommonConstants.PAGE_HOME, false);
                return;
            }

            if (employee != null)
            {
                ddlStatus.SelectedValue = employee.StatusId.ToString();
                if (ddlStatus.SelectedItem.Text == "Active")
                {
                    ucDatePickerLastWorkDay.Visible = false;
                    lbLstWrkDay.Visible = false;
                    lblmandatoryLstWrkDay.Visible = false;
                    ucDatePickerLastWorkDay.Visible = false;
                }
                if (ddlStatus.SelectedItem.Text == "InActive")
                {
                    ucDatePickerLastWorkDay.Visible = true;
                    lbLstWrkDay.Visible = true;
                    lblmandatoryLstWrkDay.Visible = true;
                    ucDatePickerLastWorkDay.Visible = true;
                    ucDatePickerLastWorkDay.Text = employee.LastWorkingDay == DateTime.MinValue ? string.Empty : employee.LastWorkingDay.ToShortDateString();
                }
                ucDatePickerResignationDate.Text = employee.ResignationDate == DateTime.MinValue ? string.Empty : employee.ResignationDate.ToShortDateString();
                txtResignationReason.Text = employee.ResignationReason;
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "PopulateControls", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Validates the controls.
    /// </summary>
    /// <returns></returns>
    private bool ValidateControls()
    {
        StringBuilder errMessage = new StringBuilder();
        bool flag = true;
        string[] ResignationDateArr;
        string[] LastWorkDayArr;

        //Siddharth 03rd Dec 2015 Start
        if (!string.IsNullOrEmpty(ucDatePickerResignationDate.Text))
        {
            ResignationDateArr = Convert.ToString(ucDatePickerResignationDate.Text).Split(SPILITER_SLASH);
            DateTime ResignationDate = new DateTime(Convert.ToInt32(ResignationDateArr[2]), Convert.ToInt32(ResignationDateArr[1]), Convert.ToInt32(ResignationDateArr[0]));

            if (ResignationDate > DateTime.Now.Date)
            {
                errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.RESIGNATION_DATE_VALIDATION);
                flag = false;
            }
        }
        //Siddharth 03rd Dec 2015 End

        if (!string.IsNullOrEmpty(ucDatePickerResignationDate.Text) && !string.IsNullOrEmpty(ucDatePickerLastWorkDay.Text))
        {
            //ResignationDateArr = Convert.ToString(ucDatePickerResignationDate.Text).Split(SPILITER_SLASH);
            //DateTime ResignationDate = new DateTime(Convert.ToInt32(ResignationDateArr[2]), Convert.ToInt32(ResignationDateArr[1]), Convert.ToInt32(ResignationDateArr[0]));

            LastWorkDayArr = Convert.ToString(ucDatePickerLastWorkDay.Text).Split(SPILITER_SLASH);
            DateTime LastWorkDay = new DateTime(Convert.ToInt32(LastWorkDayArr[2]), Convert.ToInt32(LastWorkDayArr[1]), Convert.ToInt32(LastWorkDayArr[0]));

            if (LastWorkDay > DateTime.Now.Date)
            {
                errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.LASTWORKING_DAY_VALIDATION);
                flag = false;
            }
        }


        lblMessage.Text = errMessage.ToString();
        return flag;
    }

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
                //BubbleControl.BubbleClick += new EventHandler(BubbleControl_BubbleClick);
            }
        }
    }

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

    private void BubbleControl_BubbleClick(object sender, EventArgs e)
    {
        try
        {
            //this.PopulateControls();

            if (Session[SessionNames.EMPLOYEEDETAILS] != null)
            {
                employee = (BusinessEntities.Employee)Session[SessionNames.EMPLOYEEDETAILS];
                employee = this.GetEmployee(employee);

                lblempName.Text = employee.FirstName.ToUpper() + " " + employee.LastName.ToUpper();

                this.PopulateControls();
            }

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
    /// release the employee from project & then Exit the employee.
    /// If employee is last resource on project then project should get closed.
    /// Mail should be send for all above activities.
    /// </summary>
    /// <param name="employee">Employee Details</param>
    /// <created By>Yagendra Sharnagat </created By>
    private void ExitEmployee(BusinessEntities.Employee employee, bool B_ClientName)
    {

        RaveHRCollection raveHRCollection = new RaveHRCollection();

        Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();

        //Get the allocated project details of employee
        raveHRCollection = employeeBL.IsAllocatedToProject(employee);

        //Issue Id : 28572 Mahendra START
        //Check if any employee's line manager or functional manager is resigned employee.
        RaveHRCollection raveHRCollectionReportingEmployee = new RaveHRCollection();
        raveHRCollectionReportingEmployee = employeeBL.IsRepotingManager(employee);
        if (raveHRCollectionReportingEmployee.Count != 0)
        {
            lblMessage.Visible = true;
            lblMessage.Text = "Resigned Employee is a Line Manager or Functional Manager of some of employees. Please change Manager for that employees.";
            return;
        }
        //Issue Id : 28572 Mahendra END


        int ProjectCount = 0;
        Boolean IsProjectClosed = false;
        string projectAllocation = string.Empty;
        Boolean IsRoleHR = false;

        // Check whether employee is allocated to project.
        if (raveHRCollection.Count != 0)
        {
            RaveHRCollection newRaveHRCollection = new RaveHRCollection();
            BusinessEntities.ParameterCriteria newObjParameter = new BusinessEntities.ParameterCriteria();

            //As this parameter required for the method.
            newObjParameter.PageNumber = 1;
            newObjParameter.PageSize = 10;
            newObjParameter.SortExpressionAndDirection = "EMPId ASC";
            IsRoleHR = true;

            employee.StatusId = 142;//active emp

            //Get the employee details of allocated projects.
            newRaveHRCollection = employeeBL.GetEmployeesProjectDetails(newObjParameter, employee, ref ProjectCount);

            //To release Employee project wise.
            foreach (BusinessEntities.Employee empDetails in newRaveHRCollection)
            {
                empDetails.EmpReleasedStatus = 1;

                empDetails.StatusId = Convert.ToInt32(ddlStatus.SelectedItem.Value.ToString());

                //For mail get current date for release
                //30920-Subhra-Start
                //empDetails.ProjectReleaseDate = System.DateTime.Now;
                empDetails.ProjectReleaseDate = ucDatePickerLastWorkDay.Text.Trim() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(ucDatePickerLastWorkDay.Text);
                //30920-Subhra-End

                //Release the employee from project.
                employeeBL.UpdateEmployeeReleaseStatus(empDetails, ref IsProjectClosed);

                //Siddharth 7 April 2015 Start
                //Set isActive =0 for projects of resigned employees
                employeeBL.Employee_UpdateEmpCostCodeProjRelease(empDetails);
                //Siddharth 7 April 2015 End

                if (empDetails.ProjectId != 0)
                {   //Send mail for project release.
                    employeeBL.SendMailToEmployeeReleasedFromProject(empDetails, IsRoleHR);

                    projectAllocation += empDetails.ProjectName + ", ";
                }
                else
                {
                    //Send mail for department release.
                    employeeBL.SendMailToEmployeeReleasedFromDepartment(empDetails, IsRoleHR);
                }

                //If last employee then close the project.
                if (IsProjectClosed)
                {
                    //then send Email for closed project
                    int ProjectId = empDetails.ProjectId;

                    Rave.HR.BusinessLayer.Projects.Projects objProjectsBAL = new Rave.HR.BusinessLayer.Projects.Projects(); ;
                    BusinessEntities.Projects objRaveHR = new BusinessEntities.Projects(); ;
                    //Get the prooject details for email functionality.
                    objRaveHR = objProjectsBAL.RetrieveProjectDetails(ProjectId);
                    //--Call the BL method to send Emails.
                    employeeBL.SendMailToEmployeeForClosedProject(objRaveHR);
                }
            }
        }

        //Inactive employee Id.
        employee.StatusId = 143;

        //Resign the employee from company (exit).
        employeeBL.UpdateEmployeeResignationDetails(employee);

        //Remove last appended comma.
        if (projectAllocation.EndsWith(", "))
            projectAllocation = projectAllocation.Substring(0, projectAllocation.Length - 2);

        //Also get project name with department.
        if (projectAllocation.Trim() != string.Empty)
            employee.Department = employee.Department + " (" + projectAllocation.Trim() + ") ";


        //Send mail for exit.
        employeeBL.SendMailEmployeeResignFromCompany(employee, B_ClientName);

        lblConfirmMsg.Text = "Employee Resignation Details saved successfully and email notification is sent.";

        lblMessage.Visible = true;
    }





    #endregion

}
