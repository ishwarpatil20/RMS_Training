//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           EmpOtherDetails.aspx.cs    
//  Author:         Shrinivas.Dalavi
//  Date written:   21/8/2009/ 12:01:00 PM
//  Description:    This Page will be the entry page for adding Other details with employee
//                  same page will use in View Other details & Edit Other details mode
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  21/8/2009/ 12:01:00 PM  Shrinivas.Dalavi    n/a     Created     
//
//------------------------------------------------------------------------------

using System;
using System.Web.UI.WebControls;
using Common;
using Common.AuthorizationManager;
using System.Collections;

public partial class EmpOtherDetails : BaseClass
{
    #region Private Field Members
    
    Rave.HR.BusinessLayer.Employee.Employee objEmployeeBAL = new Rave.HR.BusinessLayer.Employee.Employee();
    private int employeeID = 0;
    private BusinessEntities.Employee employee;
    string UserRaveDomainId;
    string UserMailId;

    AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
    // Get logged-in user's email id
    AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
    ArrayList arrRolesForUser = new ArrayList();

    #endregion

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
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

            txtNoRelocationIndiaReason.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return MultiLineTextBox('" + txtNoRelocationIndiaReason.ClientID + "','" + txtNoRelocationIndiaReason.MaxLength + "','" + imgNoRelocationIndiaReason.ClientID + "');");
            imgNoRelocationIndiaReason.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanNoRelocationIndiaReason.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
            imgNoRelocationIndiaReason.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanNoRelocationIndiaReason.ClientID + "');");

            txtNoRelocationOtherCountryReason.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return MultiLineTextBox('" + txtNoRelocationOtherCountryReason.ClientID + "','" + txtNoRelocationOtherCountryReason.MaxLength + "','" + imgNoRelocationOtherCountryReason.ClientID + "');");
            imgNoRelocationOtherCountryReason.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanNoRelocationOtherCountryReason.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
            imgNoRelocationOtherCountryReason.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanNoRelocationOtherCountryReason.ClientID + "');");

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
                this.PopulateControl();

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
                            Otherdetails.Enabled = true;
                            btnSave.Visible = true;
                            btnCancel.Visible = true;
                        }
                        else
                        {
                            Otherdetails.Enabled = false;
                            btnSave.Visible = false;
                            btnCancel.Visible = false;

                        }
                    }
                }
                else
                {
                    if (PageMode == Common.MasterEnum.PageModeEnum.Edit.ToString() && arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
                    {
                        Otherdetails.Enabled = true;
                        btnSave.Visible = true;
                        btnCancel.Visible = true;
                    }
                    else
                    {
                        Otherdetails.Enabled = false;
                        btnSave.Visible = false;
                        btnCancel.Visible = false;

                    }
                }
            }
        }
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
                lblError.Text = CommonConstants.SELECT_DURATION;
                txtNoRelocationOtherCountryReason.Focus();
                return; 
            }
            employee.ReadyToRelocate = false;    
            employee.Duration = string.Empty;
            rbtnDuration.SelectedIndex = -1;
        }
        Boolean Flag = true;

        objEmployeeBAL.UpdateEmployee(employee, Flag);

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
            //rbtnDuration.SelectedValue.=string.Empty;
        }

    }

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
        }

        if (employee.ReadyToRelocate)
        {
            lblReasonRelocationOtherCountry.Visible = false;
            txtNoRelocationOtherCountryReason.Visible = false;
            lblDuration.Visible = true;
            rbtnDuration.Visible = true;
            rbtnRelocateOther.SelectedIndex = 0;
            rbtnDuration.SelectedValue = employee.Duration;
        }
        else
        {
            lblReasonRelocationOtherCountry.Visible = true;
            txtNoRelocationOtherCountryReason.Visible = true;
            lblDuration.Visible = false;
            rbtnDuration.Visible = false;
            rbtnRelocateOther.SelectedIndex = 1;
            //rbtnDuration.SelectedValue = string.Empty;
            txtNoRelocationOtherCountryReason.Text = employee.ReasonNotToRelocateIndia;
        }
    }

    #endregion
}
