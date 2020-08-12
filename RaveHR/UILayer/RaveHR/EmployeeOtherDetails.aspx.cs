using System;
using System.Collections;
using System.Web.UI;
using Common;
using Common.AuthorizationManager;

public partial class EmployeeOtherDetails : BaseClass
{
    #region Private Field Members

    Rave.HR.BusinessLayer.Employee.Employee objEmployeeBAL = new Rave.HR.BusinessLayer.Employee.Employee();
    private int employeeID = 0;
    private BusinessEntities.Employee employee;
    string UserRaveDomainId;
    string UserMailId;
    Boolean IsDataSaved = false;
    AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
    // Get logged-in user's email id
    AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
    ArrayList arrRolesForUser = new ArrayList();
    string PageMode = string.Empty;

    protected EmployeeMenuUC BubbleControl;

    private string CLASS_NAME = "EmployeeOtherDetails.aspx.cs";
    const string PAGETYPE = "pagetype";
    const string PAGETYPEEMPLOYEESUMMERY = "EMPLOYEESUMMERY";

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

    #endregion

    #region Protected Methods

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

        txtNoRelocationIndiaReason.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return MultiLineTextBox('" + txtNoRelocationIndiaReason.ClientID + "','" + txtNoRelocationIndiaReason.MaxLength + "','" + imgNoRelocationIndiaReason.ClientID + "');");
        imgNoRelocationIndiaReason.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanNoRelocationIndiaReason.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgNoRelocationIndiaReason.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanNoRelocationIndiaReason.ClientID + "');");

        txtNoRelocationOtherCountryReason.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return MultiLineTextBox('" + txtNoRelocationOtherCountryReason.ClientID + "','" + txtNoRelocationOtherCountryReason.MaxLength + "','" + imgNoRelocationOtherCountryReason.ClientID + "');");
        imgNoRelocationOtherCountryReason.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanNoRelocationOtherCountryReason.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgNoRelocationOtherCountryReason.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanNoRelocationOtherCountryReason.ClientID + "');");

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
                this.PopulateControl();

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

                    if (PageMode == Common.MasterEnum.PageModeEnum.View.ToString() && IsPostBack == false && IsDataSaved==true)
                    {
                        Otherdetails.Enabled = false;
                        btnEdit.Visible = true;
                        btnSave.Visible = false;
                    }
                }

            }
            else
            {
                Otherdetails.Enabled = false;

                btnSave.Visible = false;

            }
            

            SavedControlVirtualPath = "~/EmployeeMenuUC.ascx";
            ReloadControl();
       
    }

    /// <summary>
    /// Handles the Click event of the btnSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (rbtnRelocateIndia.SelectedIndex == 0)
        {
            employee.ReadyToRelocateIndia = true;
            employee.ReasonNotToRelocateIndia = string.Empty;
            txtNoRelocationIndiaReason.Text = string.Empty;
        }
        else
        {
            if (txtNoRelocationIndiaReason.Text != string.Empty)
            {
                employee.ReadyToRelocateIndia = false;
                employee.ReasonNotToRelocateIndia = txtNoRelocationIndiaReason.Text;
            }
            else
            {
                lblError.Text = CommonConstants.OTHERDETAILS_VALIDATION;
                txtNoRelocationIndiaReason.Focus();
                return;
            }
        }
        if (rbtnRelocateOther.SelectedIndex == CommonConstants.ZERO)
        {
            if (rbtnDuration.SelectedValue != string.Empty)
                employee.Duration = rbtnDuration.SelectedItem.Text;
            else
            {
                lblError.Text = CommonConstants.SELECT_DURATION;
                return;
            }
            employee.ReadyToRelocate = true;
            employee.ReasonNotToRelocate = string.Empty;
            txtNoRelocationOtherCountryReason.Text = string.Empty;
        }
        else
        {
            if (txtNoRelocationOtherCountryReason.Text != string.Empty)
            {
                employee.ReasonNotToRelocate = txtNoRelocationOtherCountryReason.Text;
            }
            else
            {   
                //To solved issue id 19221
                //Coded by Rahul P
                //lblError.Text = CommonConstants.SELECT_DURATION;
                lblError.Text = CommonConstants.OTHERDETAILS_VALIDATION;
                //End
                txtNoRelocationOtherCountryReason.Focus();
                return;
            }
            employee.ReadyToRelocate = false;
            employee.Duration = string.Empty;
            rbtnDuration.SelectedIndex = -1;
        }
        employee.LastModifiedByMailId = UserMailId;
        employee.LastModifiedDate = DateTime.Today;
        
        Boolean Flag = false;
        objEmployeeBAL.UpdateEmployee(employee,Flag);

        lblMessage.Text = "Other details saved successfully.";
    }

    /// <summary>
    /// Handles the Click event of the btnCancel control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtNoRelocationIndiaReason.Text = string.Empty;
        txtNoRelocationOtherCountryReason.Text = string.Empty;
        rbtnRelocateIndia.SelectedIndex = 1;
        rbtnRelocateOther.SelectedIndex = 1;

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
    /// Handles the Click event of the btnPrevious control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        Response.Redirect(CommonConstants.PAGE_PROJECTDETAILS);
    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the rbtnRelocateIndia control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void rbtnRelocateIndia_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnRelocateIndia.SelectedItem.Text == CommonConstants.YES)
        {
            lblReasonRelocationIndia.Visible = false;
            txtNoRelocationIndiaReason.Visible = false;
        }
        else
        {
            lblReasonRelocationIndia.Visible = true;
            txtNoRelocationIndiaReason.Visible = true;
        }

    }

    /// <summary>
    /// Handles the SelectedIndexChanged event of the rbtnRelocateOther control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void rbtnRelocateOther_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnRelocateOther.SelectedItem.Text == CommonConstants.YES)
        {
            lblReasonRelocationOtherCountry.Visible = false;
            txtNoRelocationOtherCountryReason.Visible = false;
            lblDuration.Visible = true;
            rbtnDuration.Visible = true;
        }
        else
        {
            lblReasonRelocationOtherCountry.Visible = true;
            txtNoRelocationOtherCountryReason.Visible = true;
            lblDuration.Visible = false;
            rbtnDuration.Visible = false;
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
        Otherdetails.Enabled = true;
        btnEdit.Visible = false;
        btnSave.Visible = true;
    }

    #endregion

    #region Private Member Functions

    /// <summary>
    /// Populates the control.
    /// </summary>
    private void PopulateControl()
    {
        EMPId.Value = employeeID.ToString().Trim();

        if (employee.ReadyToRelocateIndia)
        {
            lblReasonRelocationIndia.Visible = false;
            txtNoRelocationIndiaReason.Visible = false;
            rbtnRelocateIndia.SelectedIndex = 0;
        }
        else
        {
            lblReasonRelocationIndia.Visible = true;
            txtNoRelocationIndiaReason.Visible = true;
            txtNoRelocationIndiaReason.Text = employee.ReasonNotToRelocateIndia;
            rbtnRelocateIndia.SelectedIndex = 1;
            if (!string.IsNullOrEmpty(employee.ReasonNotToRelocateIndia))
                IsDataSaved = true;
            else
                IsDataSaved = false;
        }

        if (employee.ReadyToRelocate)
        {
            lblReasonRelocationOtherCountry.Visible = false;
            txtNoRelocationOtherCountryReason.Visible = false;
            lblDuration.Visible = true;
            rbtnDuration.Visible = true;
            rbtnRelocateOther.SelectedIndex = 0;
            //To solved issue id 19221
            //Coded by Rahul P
            //Start
            //rbtnDuration.SelectedValue = employee.Duration;
            for (int count = 0; count < rbtnDuration.Items.Count; count++)
            {
                if (rbtnDuration.Items[count].ToString() == employee.Duration.ToString())
                {
                    rbtnDuration.SelectedValue = employee.Duration;
                }
            }
            //End
            if (!string.IsNullOrEmpty(employee.Duration))
                IsDataSaved = true;
            else
                IsDataSaved = false;
        }
        else
        {
            lblReasonRelocationOtherCountry.Visible = true;
            txtNoRelocationOtherCountryReason.Visible = true;
            lblDuration.Visible = false;
            rbtnDuration.Visible = false;
            rbtnRelocateOther.SelectedIndex = 1;
            //To solved issue id 19221
            //Coded by Rahul P
            //Start
            txtNoRelocationOtherCountryReason.Text = employee.ReasonNotToRelocate;
            //End
            if (!string.IsNullOrEmpty(employee.ReasonNotToRelocateIndia))
                IsDataSaved = true;
            else
                IsDataSaved = false;
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
        //this.PopulateControls();
        if (Session[SessionNames.EMPLOYEEDETAILS] != null)
        {
            employee = (BusinessEntities.Employee)Session[SessionNames.EMPLOYEEDETAILS];
            employee = this.GetEmployee(employee);

            lblempName.Text = employee.FirstName.ToUpper() + " " + employee.LastName.ToUpper();

            employeeID = employee.EMPId;
            EMPId.Value = employeeID.ToString().Trim();
            this.PopulateControl();
        }
        //To solved issue id 19221
        //Coded by Rahul P
        //Start
        if (!IsDataSaved)
        {
            Otherdetails.Enabled = true;
            btnEdit.Visible = false;
            btnSave.Visible = true;
        }
        else
        {
            Otherdetails.Enabled = false;
            btnEdit.Visible = true;
            btnSave.Visible = false;
        }
        //End

        //Ishwar: Issue Id - 54410 : 05012015 Start
        if (UserMailId.ToLower() != employee.EmailId.ToLower() && arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEPM))
        {
            btnEdit.Visible = false;
            btnCancel.Visible = true;
            btnSave.Visible = false;
            Otherdetails.Enabled = false;
        }
        //Ishwar: Issue Id - 54410 : 05012015 Ends

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
