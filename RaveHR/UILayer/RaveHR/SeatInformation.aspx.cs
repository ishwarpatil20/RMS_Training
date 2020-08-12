
//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           SeatInformation.aspx.cs       
//  Author:         Kanchan.Singh
//  Date written:   19/11/2009 2:30:00 PM
//  Description:    The Seat Information page adds the information about the person seating on that location. It is also used to view or
//                  update the details of the location(Seat).
//
//  Amendments
//  Date                   Who               Ref      Description
//  ----                   -----------       ---      -----------
//  19/11/2009 2:30:00 PM  Kanchan.Singh     n/a      Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Common;
using Common.AuthorizationManager;
using System.Text;
using Common.Constants;

public partial class SeatInformation : BaseClass
{
    #region Local member Declaration

    //Define the zero as string.
    private string ZERO = "0";

    //Define the select as string.
    private string SELECTONE = "Select";

    string MasterName = "MasterName";

    string MasterID = "MasterID";

    const string CLASS_NAME = "SeatInformation.aspx.cs";

    const string EMPLOYEESEATDETAILS = "EmployeeDetailsAtSeat";

    BusinessEntities.SeatAllocation SeatDetails = null;

    Rave.HR.BusinessLayer.SeatAllocation.SeatAllocation BLSeatAllocation = new Rave.HR.BusinessLayer.SeatAllocation.SeatAllocation();

    bool result = false;

    const string VIEWINFORMATION = "ViewInformation";

    const string GETCONTROLDATA = "getControldata";

    const string CHECKSELFALLOCATION = "CheckSelfAllocation";

    const string DATAFORVIEW = "BindDataForView";

    const string SeatAllocationCategory = "59";

    string subject;

    string body;

    //Googleconfigurable
    //private const string RAVE_DOMAIN = "@rave-tech.com";

    const char Splitter = '/';

    #endregion Local member Declaration

    #region Protected Methodes

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
                //Remove Caches to get updated data from database.
                Response.Expires = 0;
                Response.Cache.SetNoStore();
                Response.AppendHeader("Pragma", "no-cache");

                if (!IsPostBack)
                {
                    //GetMasterData_EmployeeCode();
                    GetMasterData_EmployeeAsset();

                    string ID = null;
                    if (Request.QueryString[0] != null)
                    {
                        ID = Request.QueryString[0];
                        hfSeatID.Value = ID.ToString();
                        ViewInformation(Convert.ToInt32(ID));
                    }
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "PageLoad", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlEmpAsset_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GetMasterData_EmployeeCode();
            //Hide the dropdown for asset or selection.
            btnAllocate.Visible = true;

            hideSelection();

            //Condition is written to display controls as per the selection of the drop down.
            if (Convert.ToInt32(ddlEmpAsset.SelectedValue) == 301)
            {
                UnhideControlsForAsset();
                HideControlforEmployee();
            }
            else
            {
                HideControlsForAsset();
                unhideControlforEmployee();
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlEmpAsset_SelectedIndexChanged", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Allocate the employee to related(clicked )seat.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAllocate_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ddlEmpAsset.SelectedValue) != 301)
            {
                if (string.IsNullOrEmpty(hfEmpID.Value))
                {
                    hfEmpID.Value = "0";
                }

                if (Convert.ToInt32(hfEmpID.Value) != 0 && Convert.ToInt32(hfSeatID.Value) != 0)
                {
                    if (CheckEmployeeLocation(Convert.ToInt32(hfEmpID.Value)))
                    {
                        SeatDetails = new BusinessEntities.SeatAllocation();
                        BLSeatAllocation = new Rave.HR.BusinessLayer.SeatAllocation.SeatAllocation();

                        SeatDetails.EmployeeID = Convert.ToInt32(hfEmpID.Value);
                        SeatDetails.SeatID = Convert.ToInt32(hfSeatID.Value);

                        result = BLSeatAllocation.Allocate(SeatDetails);
                        if (result)
                        {
                            lblConfirmMessage.Visible = true;
                            lblConfirmMessage.Text = "Details Updated sucessfully";
                            btnAllocate.Visible = false;
                            btnOK.Visible = true;
                        }
                    }
                }
                else
                {
                    lblConfirmMessage.Text = "<font color=RED >" + "Select a Proper Employee Code." + "</font>";
                }
            }
            else
            {
                HideControlsForAsset();
                btnAllocate.Visible = false;
                lblConfirmMessage.Text = "<font color=RED >" + "MODULE UNDER DEVLOPMENT" + "</font>";
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnAllocate_Click", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Save the employee details.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text != "Close")
            {
                BusinessEntities.SeatAllocation SeatInfo = new BusinessEntities.SeatAllocation();
                BLSeatAllocation = new Rave.HR.BusinessLayer.SeatAllocation.SeatAllocation();
                SeatInfo = getControldata();
                result = BLSeatAllocation.SaveEmpDetails(SeatInfo);
                if (result)
                {
                    lblConfirmMessage.Visible = true;
                    lblConfirmMessage.Text = "Details Updated sucessfully";
                    btnSave.Text = "Close";
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "window.close();", true);
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnSave_Click", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Close the current window and redirect to parent page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            string seatId = Request.QueryString[0]; 
            string branchId = Request.QueryString[QueryStringConstants.SEATALNBRANCHID];
            string sectionId = Request.QueryString[QueryStringConstants.SEATALNSECTIONID];
            //Close the window.
            string script = "window.opener.location.href='SeatAllocation.aspx?AllocateSeatID=" + seatId + "&BranchID=" + branchId + "&SectionID=" + sectionId + "'; window.close();";

            Page.ClientScript.RegisterStartupScript(this.GetType(), "close", script, true);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnOK_Click", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }


    }

    /// <summary>
    /// Get the employee name on the basis of selected employee code.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlEmployeeCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetEmployeeName();

    }

    #endregion Protected Methodes

    #region Public Methodes

    /// <summary>
    /// Make controls visible for view mode.
    /// </summary>
    public void makeViewControlsVisible()
    {
        try
        {
            //Make labels Visible.        
            lblDepartmentName.Visible = true;
            lblEmployeeCode.Visible = true;
            lblEmpName.Visible = true;
            lblExtnsion.Visible = true;
            lblprojectName.Visible = true;
            lblSeatInformation.Visible = true;
            lblLandmark.Visible = true;


            //Make Textboxes Visible.
            tbEmpCode.Visible = true;
            tbExtension.Visible = true;
            tbLandmark.Visible = true;
            tbDepartmentName.Visible = true;
            tbProjectName.Visible = true;
            tbEmpName.Visible = true;

            //makes Control read only in view mode.
            tbEmpCode.ReadOnly = true;
            tbExtension.ReadOnly = true;
            tbLandmark.ReadOnly = true;
            tbProjectName.ReadOnly = true;
            tbDepartmentName.ReadOnly = true;
            tbEmpName.ReadOnly = true;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "makeViewControlsVisible", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Make controls Invisible .
    /// </summary>
    public void HideViewControls()
    {
        try
        {
            //Make labels InVisible.

            lblDepartmentName.Visible = false;
            lblEmployeeCode.Visible = false;
            lblExtnsion.Visible = false;
            lblprojectName.Visible = false;
            lblSeatInformation.Visible = false;
            lblLandmark.Visible = false;
            lblEmpName.Visible = false;

            //Make Textboxes InVisible.
            tbEmpCode.Visible = false;
            tbExtension.Visible = false;
            tbLandmark.Visible = false;
            tbProjectName.Visible = false;
            tbDepartmentName.Visible = false;
            tbEmpName.Visible = false;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "HideViewControls", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Unhides the Asset or employee dropdown.
    /// </summary>
    public void UnhideSelection()
    {
        try
        {
            lblEmpAsset.Visible = true;
            ddlEmpAsset.Visible = true;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "UnhideSelection", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Hides the Asset or employee dropdown.
    /// </summary>
    public void hideSelection()
    {
        try
        {
            lblEmpAsset.Visible = false;

            ddlEmpAsset.DataSource = null;
            ddlEmpAsset.DataBind();
            ddlEmpAsset.Visible = false;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "hideSelection", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Hides Controls related to Asset.
    /// </summary>
    public void HideControlsForAsset()
    {
        try
        {
            //Make labels Visible.
            lblAssetCode.Visible = false;
            lblAssetDesc.Visible = false;
            lblLandmark.Visible = false;

            //Make Dropdowns Visible.
            ddlAssetCode.Visible = false;
            ddlAssetCode.DataSource = null;
            ddlAssetCode.DataBind();


            tbLandmark.Visible = false;
            tbAssetDescription.Visible = false;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "hideSelection", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Unhides the controls related to the Asset.
    /// </summary>
    public void UnhideControlsForAsset()
    {
        try
        {
            //Make labels Visible.
            lblAssetCode.Visible = true;
            lblAssetDesc.Visible = true;
            lblLandmark.Visible = true;

            //Make Dropdowns Visible.
            ddlAssetCode.Visible = true;
            tbAssetDescription.Visible = true;
            tbLandmark.Visible = true;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "UnhideControlsForAsset", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Hide controls related to employee.
    /// </summary>
    public void HideControlforEmployee()
    {
        try
        {
            lblEmployeeCode.Visible = false;
            tbEmpCode.Visible = false;
            ddlEmployeeCode.DataSource = null;
            ddlEmployeeCode.DataBind();
            ddlEmployeeCode.Visible = false;
            lblEmpName.Visible = false;
            tbEmpName.Visible = false;
            tbEmpName.ReadOnly = false;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "HideControlforEmployee", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Unhide controls related to employee.
    /// </summary>
    public void unhideControlforEmployee()
    {
        try
        {
            lblEmployeeCode.Visible = true;
            //tbEmpCode.Visible = true;
            ddlEmployeeCode.Visible = true;
            lblEmpName.Visible = true;
            tbEmpName.Visible = true;
            tbEmpName.ReadOnly = true;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "unhideControlforEmployee", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
        }


    }

    /// <summary>
    /// Get the employee details at a particular seat.
    /// </summary>
    /// <param name="Seat"></param>
    /// <returns></returns>
    public BusinessEntities.SeatAllocation EmployeeDetailsAtSeat(BusinessEntities.SeatAllocation Seat)
    {
        try
        {
            SeatDetails = new BusinessEntities.SeatAllocation();
            BLSeatAllocation = new Rave.HR.BusinessLayer.SeatAllocation.SeatAllocation();
            SeatDetails = BLSeatAllocation.GetEmployeeDetailsForSeat(Seat);

        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, EMPLOYEESEATDETAILS, EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
        return SeatDetails;

    }

    /// <summary>
    /// View the Seat details of respective employee.
    /// </summary>
    /// <param name="ID"></param>
    public void ViewInformation(int ID)
    {
        try
        {
            BusinessEntities.SeatAllocation SeatID = new BusinessEntities.SeatAllocation();
            SeatID.SeatID = ID;
            int EmpID = 0;
            if (hfEmpID.Value != string.Empty)
            {
                EmpID = Convert.ToInt32(hfEmpID.Value);
            }
            SeatID.EmployeeID = EmpID;

            SeatDetails = new BusinessEntities.SeatAllocation();
            SeatDetails = EmployeeDetailsAtSeat(SeatID);

            //If user has clicked on green seat.means want to view a deatils.
            if (SeatDetails.EmployeeName != null)
            {
                makeViewControlsVisible();
                BindDataForView(SeatDetails);

                //If department is project then project textbox should be visible, else department should be display.
                if (SeatDetails.DepartmentName != "Projects")
                {
                    tbProjectName.Visible = false;
                    lblprojectName.Visible = false;
                }
                //check the roles for self & admin. only this guys can change extension and landmark of seat.
                if (CheckSelfAllocation(SeatDetails) || Rave.HR.BusinessLayer.SeatAllocation.SeatAllocationRoles.CheckRolesSeatAllocation())
                {
                    tbExtension.ReadOnly = false;
                    tbLandmark.ReadOnly = false;
                    btnSave.Visible = true;
                }
                else
                {
                    tbExtension.ReadOnly = true;
                    tbLandmark.ReadOnly = true;
                    btnSave.Visible = false;
                }

                hfEmpID.Value = SeatDetails.EmployeeID.ToString();
            }
            //for allocate condition.
            else
            {
                //Check the roles.
                if (Rave.HR.BusinessLayer.SeatAllocation.SeatAllocationRoles.CheckRolesSeatAllocation())
                {
                    UnhideSelection();
                }
                else
                {
                    hideSelection();
                    lblConfirmMessage.Visible = true;
                    lblConfirmMessage.Text = "You are not authorised to Allocate a Employee.";
                }
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, VIEWINFORMATION, EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Bind data to their respective controls. 
    /// </summary>
    /// <param name="SeatData"></param>
    public void BindDataForView(BusinessEntities.SeatAllocation SeatData)
    {
        try
        {
            if (SeatData.EmployeeName != string.Empty)
            {
                tbEmpName.Text = SeatData.EmployeeName;
            }
            if (SeatData.EmployeeCode != string.Empty)
            {
                tbEmpCode.Text = SeatData.EmployeeCode;
            }
            if (SeatData.DepartmentName != string.Empty)
            {
                tbDepartmentName.Text = SeatData.DepartmentName;
            }
            if (SeatData.ProjectName != string.Empty)
            {
                tbProjectName.Text = SeatData.ProjectName;
            }
            else
            {
                tbProjectName.Text = "No project Assigned";
            }

            if (SeatData.ExtensionNo != 0)
            {
                tbExtension.Text = SeatData.ExtensionNo.ToString();
            }
            else
            {
                tbExtension.Text = "No Extension Provided";
            }

            if (SeatData.SeatLandmark != string.Empty)
            {
                tbLandmark.Text = SeatData.SeatLandmark;
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "BindDataForView", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// Check whether seat is allocated to self(LOGIN USER).
    /// </summary>
    /// <param name="seat"></param>
    /// <returns></returns>
    public bool CheckSelfAllocation(BusinessEntities.SeatAllocation seat)
    {
        try
        {
            BLSeatAllocation = new Rave.HR.BusinessLayer.SeatAllocation.SeatAllocation();

            SeatDetails = new BusinessEntities.SeatAllocation();

            //Get logged in user
            Common.AuthorizationManager.AuthorizationManager objAuMan = new Common.AuthorizationManager.AuthorizationManager();

            string LoggedInuserEmailId = objAuMan.getLoggedInUserEmailId();

            if (LoggedInuserEmailId == seat.EmployeeEmailID)
            {
                result = true;
            }
            return result;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, CHECKSELFALLOCATION, EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
        }
    }

    public BusinessEntities.SeatAllocation getControldata()
    {
        try
        {
            SeatDetails = new BusinessEntities.SeatAllocation();
            SeatDetails.SeatID = Convert.ToInt32(hfSeatID.Value);
            SeatDetails.EmployeeID = Convert.ToInt32(hfEmpID.Value);
            SeatDetails.SeatLandmark = tbLandmark.Text;
            if (tbExtension.Text != "No Extension Provided")
            {
                SeatDetails.ExtensionNo = Convert.ToInt32(tbExtension.Text);
            }
            return SeatDetails;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "getControldata", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// Get the employee name by employee code.
    /// </summary>
    public void GetEmployeeName()
    {
        try
        {
            SeatDetails = new BusinessEntities.SeatAllocation();
            BusinessEntities.SeatAllocation Seat = new BusinessEntities.SeatAllocation();

            BLSeatAllocation = new Rave.HR.BusinessLayer.SeatAllocation.SeatAllocation();
            // SeatDetails.EmployeeCode = tbEmpCode.Text.Trim();
            SeatDetails.EmployeeCode = ddlEmployeeCode.SelectedItem.Text;
            Seat = BLSeatAllocation.GetEmployeeName(SeatDetails);

            hfEmpID.Value = Seat.EmployeeID.ToString();
            tbEmpName.Text = Seat.EmployeeName;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetEmployeeName", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Close the window of current work.
    /// </summary>
    public void Close()
    {
        //Close the window.
        Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "window.close();", true);
    }

    /// <summary>
    /// Check whether employee allocated to any seat.
    /// </summary>
    /// <param name="employeeId"></param>
    /// <returns></returns>
    public bool CheckEmployeeLocation(int employeeId)
    {
        bool flag = true;
        try
        {
            SeatDetails = new BusinessEntities.SeatAllocation();

            BLSeatAllocation = new Rave.HR.BusinessLayer.SeatAllocation.SeatAllocation();
            SeatDetails = BLSeatAllocation.CheckEmployeeLocation(employeeId);

            if (SeatDetails != null)
            {
                flag = false;
                lblConfirmMessage.Text = "<font color=RED >'" + SeatDetails.EmployeeName + "' has been already allocated at Seat No. '" + SeatDetails.SeatName + "'</font>";
            }
            else
            {
                flag = true;
            }
            return flag;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "CheckEmployeeLocation", EventIDConstants.RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER);
        }

    }

    #endregion Public methodes

    #region Private Methodes

    /// <summary>
    /// Gets the master data for the Asset or employee dropdown.
    /// </summary>
    private void GetMasterData_EmployeeAsset()
    {
        try
        {
            List<BusinessEntities.Master> ddlObj = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master BLobject = new Rave.HR.BusinessLayer.Common.Master();

            //This region will fill the client location drop down with the valid location types.
            ddlObj = BLobject.GetMasterData(Convert.ToString(SeatAllocationCategory));

            ddlEmpAsset.DataSource = ddlObj;
            ddlEmpAsset.DataValueField = MasterID;
            ddlEmpAsset.DataTextField = MasterName;
            ddlEmpAsset.DataBind();
            ddlEmpAsset.Items.Insert(0, new ListItem(SELECTONE, ZERO));
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetMasterData_LocationDropDown", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Gets  employees those are not allocated to any seat.
    /// </summary>
    private void GetMasterData_EmployeeCode()
    {
        try
        {
            List<BusinessEntities.SeatAllocation> ddlObj = new List<BusinessEntities.SeatAllocation>();
            Rave.HR.BusinessLayer.SeatAllocation.SeatAllocation BLobject = new Rave.HR.BusinessLayer.SeatAllocation.SeatAllocation();

            //This region will fill the unallocated Employee Code in the employee code drop down.
            ddlObj = BLobject.GetUnAllocatedEmployee();

            ddlEmployeeCode.DataSource = ddlObj;
            ddlEmployeeCode.DataValueField = DbTableColumn.Seat_EmployeeID;
            ddlEmployeeCode.DataTextField = DbTableColumn.Seat_EmployeeCode;
            ddlEmployeeCode.DataBind();
            ddlEmployeeCode.Items.Insert(0, new ListItem(SELECTONE, ZERO));
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetMasterData_LocationDropDown", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
        }
    }

    #endregion Private Methodes


}
