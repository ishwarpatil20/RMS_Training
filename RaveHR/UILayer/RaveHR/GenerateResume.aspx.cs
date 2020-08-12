//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           EmpOtherDetails.aspx.cs    
//  Author:         Shrinivas.Dalavi
//  Date written:   21/9/2009/ 12:01:00 PM
//  Description:    This Page will be the entry page for adding Other details with employee
//                  same page will use in View Other details & Edit Other details mode
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  21/9/2009/ 12:01:00 PM  Shrinivas.Dalavi    n/a     Created     
//
//------------------------------------------------------------------------------

using System;
using System.Web.UI.WebControls;
using Common;

public partial class GenerateResume : BaseClass
{
    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Handles the CheckedChanged event of the rBtnRaveFormat control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void rBtnRaveFormat_CheckedChanged(object sender, EventArgs e)
    {
        if (rBtnRaveFormat.Checked)
            rBtnClientFormat.Checked = false;

    }

    /// <summary>
    /// Handles the CheckedChanged event of the rBtnClientFormat control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void rBtnClientFormat_CheckedChanged(object sender, EventArgs e)
    {
        
        if (rBtnClientFormat.Checked)
            rBtnRaveFormat.Checked = false;
    }

    /// <summary>
    /// Handles the Click event of the btnCancel control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(CommonConstants.PAGE_EMPLOYEEDETAILS);
    }
}
