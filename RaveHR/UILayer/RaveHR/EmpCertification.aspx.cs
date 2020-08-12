//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           CertificationDetails.cs       
//  Author:         sudip.guha
//  Date written:   19/08/2009/ 12:27:33 PM
//  Description:    This class provides the UI Layer methods for CertificationDetails in  Employee module.
//                  
//
//  Amendments
//  Date                        Who             Ref     Description
//  ----                        -----------     ---     -----------
//  14/08/2009/ 06:17:30 PM     sudip.guha      n/a     Created    
//  03/09/2009/ 05:52:55 PM     vineet.kulkarni n/a     moved BE's List to collection, proper commenting
//   21/8/2009/ 12:01:00 PM   Shrinivas.Dalavi    n/a   added all manipulation(add,get,delete,update) functionality
//------------------------------------------------------------------------------

using System;
using System.Text;
using System.Web.UI.WebControls;
using Common;
using Common.AuthorizationManager;
using System.Collections;

public partial class EmpCertification : BaseClass
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
        btnSave.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return SaveButtonClickValidate();");

        txtName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtName.ClientID + "','" + imgName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHABET_WITHSPACE + "');");
        imgName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanName.ClientID + "','" + Common.CommonConstants.MSG_ONLY_ALPHABET + "');");
        imgName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanName.ClientID + "');");

        txtTotalScore.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtTotalScore.ClientID + "','" + imgTotalScore.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
        imgTotalScore.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanTotalScore.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgTotalScore.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanTotalScore.ClientID + "');");

        txtOutOf.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtOutOf.ClientID + "','" + imgOutOf.ClientID + "','" + Common.CommonConstants.VALIDATE_NUMERIC_FUNCTION + "');");
        imgOutOf.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanOutOf.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgOutOf.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanOutOf.ClientID + "');");

        txtCertificationDate.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + txtCertificationDate.ClientID + "','','');");
        txtCertificationDate.Attributes.Add(ReadOnly, ReadOnly);
        txtCertficationValidDate.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + txtCertficationValidDate.ClientID + "','','');");
        txtCertficationValidDate.Attributes.Add(ReadOnly, ReadOnly);
        //imgCertificationDate.Attributes.Add(CommonConstants.EVENT_ONMOUSEOVER, "javascript:ValidateControl('" + imgCertificationDate.ClientID + "','','');");
        //imgCertficationValidDate.Attributes.Add(CommonConstants.EVENT_ONMOUSEOVER, "javascript:ValidateControl('" + imgCertficationValidDate.ClientID + "','','');");

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
            }

            if (!IsPostBack)
            {
                this.PopulateGrid(employeeID);
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
                            Certificationdetails.Enabled = true;
                            btnCancel.Visible = true;
                            if (gvCertification.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
                                btnSave.Visible = true;
                        }
                        else
                        {
                            Certificationdetails.Enabled = false;
                            btnSave.Visible = false;
                            btnCancel.Visible = false;

                        }
                    }
                }
                else
                {
                    if (PageMode == Common.MasterEnum.PageModeEnum.Edit.ToString() && arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
                    {
                        Certificationdetails.Enabled = true;
                        btnCancel.Visible = true;
                        if (gvCertification.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
                            btnSave.Visible = true;
                    }
                    else
                    {
                        Certificationdetails.Enabled = false;
                        btnSave.Visible = false;
                        btnCancel.Visible = false;

                    }
                }
            }
        }
    }

    /// <summary>
    /// Handles the Click event of the btnAdd control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ValidateControls())
        {

            BusinessEntities.CertificationDetails objCertificationDetails = new BusinessEntities.CertificationDetails();

            if (gvCertification.Rows[0].Cells[0].Text == NO_RECORDS_FOUND_MESSAGE)
            {
                CertificationDetailsCollection.Clear();
            }

            objCertificationDetails.CertificationName = txtName.Text;
            objCertificationDetails.CertificateDate = Convert.ToDateTime(txtCertificationDate.Text);
            objCertificationDetails.CertificateValidDate = Convert.ToDateTime(txtCertficationValidDate.Text);
            objCertificationDetails.Score = float.Parse(txtTotalScore.Text);
            objCertificationDetails.OutOf = float.Parse(txtOutOf.Text);
            objCertificationDetails.Mode = 1;

            CertificationDetailsCollection.Add(objCertificationDetails);

            this.DoDataBind();

            this.ClearControls();

            if (gvCertification.Rows.Count != 0)
                btnSave.Visible = true;
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
        txtCertificationDate.Text = gvCertification.Rows[e.NewEditIndex].Cells[1].Text;
        txtCertficationValidDate.Text = gvCertification.Rows[e.NewEditIndex].Cells[2].Text;
        txtTotalScore.Text = gvCertification.Rows[e.NewEditIndex].Cells[3].Text;
        txtOutOf.Text = gvCertification.Rows[e.NewEditIndex].Cells[4].Text;

        ImageButton btnImg = (ImageButton)gvCertification.Rows[e.NewEditIndex].FindControl(IMGBTNDELETE);
        rowIndex = e.NewEditIndex;
        ViewState[ROWINDEX] = null;
        ViewState[ROWINDEX] = rowIndex;
        btnImg.Enabled = false;

        btnAddRow.Visible = false;
        btnUpdate.Visible = true;
        btnCancelRow.Visible = true;

        //Disabling all the edit buttons.
        for (int i = 0; i < gvCertification.Rows.Count; i++)
        {
            ImageButton btnImgEdit = (ImageButton)gvCertification.Rows[i].FindControl(IMGBTNEDIT);
            if (i != rowIndex)
                btnImgEdit.Enabled = false;
        }
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

        BusinessEntities.CertificationDetails objCertificationDetails = new BusinessEntities.CertificationDetails();

        deleteRowIndex = e.RowIndex;

        objCertificationDetails = (BusinessEntities.CertificationDetails)CertificationDetailsCollection.Item(deleteRowIndex);
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
    }

    /// <summary>
    /// Handles the Click event of the btnSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Rave.HR.BusinessLayer.Employee.CertificationDetails objCertificationDetailsBAL;

        BusinessEntities.CertificationDetails objCertificationDetails;
        BusinessEntities.RaveHRCollection objSaveCertificationDetailsCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            objCertificationDetailsBAL = new Rave.HR.BusinessLayer.Employee.CertificationDetails();

            if (gvCertification.Rows[0].Cells[0].Text != NO_RECORDS_FOUND_MESSAGE)
            {
                for (int i = 0; i < gvCertification.Rows.Count; i++)
                {
                    objCertificationDetails = new BusinessEntities.CertificationDetails();
                    Label CertificationId = (Label)gvCertification.Rows[i].FindControl(CERTIFICATIONID);
                    Label Mode = (Label)gvCertification.Rows[i].FindControl(MODE);

                    objCertificationDetails.CertificationId = int.Parse(CertificationId.Text);
                    objCertificationDetails.EMPId = int.Parse(EMPId.Value);
                    objCertificationDetails.CertificationName = gvCertification.Rows[i].Cells[0].Text;
                    objCertificationDetails.CertificateDate = Convert.ToDateTime(gvCertification.Rows[i].Cells[1].Text);
                    objCertificationDetails.CertificateValidDate = Convert.ToDateTime(gvCertification.Rows[i].Cells[2].Text);
                    objCertificationDetails.Score = float.Parse(gvCertification.Rows[i].Cells[3].Text);
                    objCertificationDetails.OutOf = float.Parse(gvCertification.Rows[i].Cells[4].Text);
                    objCertificationDetails.Mode = int.Parse(Mode.Text);
                    objSaveCertificationDetailsCollection.Add(objCertificationDetails);
                }
            }
            BusinessEntities.RaveHRCollection objDeleteCertificationDetailsCollection = (BusinessEntities.RaveHRCollection)ViewState[CERTIFICATIONDETAILSDELETE];

            if (objDeleteCertificationDetailsCollection != null)
            {
                BusinessEntities.CertificationDetails obj = null;

                for (int i = 0; i < objDeleteCertificationDetailsCollection.Count; i++)
                {
                    objCertificationDetails = new BusinessEntities.CertificationDetails();
                    obj = (BusinessEntities.CertificationDetails)objDeleteCertificationDetailsCollection.Item(i);

                    objCertificationDetails.CertificationId = obj.CertificationId;
                    objCertificationDetails.EMPId = obj.EMPId;
                    objCertificationDetails.CertificationName = obj.CertificationName;
                    objCertificationDetails.CertificateDate = obj.CertificateDate;
                    objCertificationDetails.CertificateValidDate = obj.CertificateValidDate;
                    objCertificationDetails.Score = obj.Score;
                    objCertificationDetails.OutOf = obj.OutOf;
                    objCertificationDetails.Mode = obj.Mode;

                    objSaveCertificationDetailsCollection.Add(objCertificationDetails);
                }

            }
            objCertificationDetailsBAL.Manipulation(objSaveCertificationDetailsCollection);

            if (ViewState.Count > 0)
            {
                ViewState.Clear();
            }

            if (EMPId.Value != string.Empty)
            {
                int empID = Convert.ToInt32(EMPId.Value);
                //Refresh the grip after saving
                this.PopulateGrid(empID);
            }

            if (gvCertification.Rows.Count == 0)
                btnSave.Visible = false;

            lblMessage.Text = "Certification details saved successfully.";
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
    /// Handles the Click event of the btnCancel control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.ClearControls();
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
                gvCertification.Rows[rowIndex].Cells[1].Text = txtCertificationDate.Text;
                gvCertification.Rows[rowIndex].Cells[2].Text = txtCertficationValidDate.Text;
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

            for (int i = 0; i < CertificationDetailsCollection.Count; i++)
            {
                BusinessEntities.CertificationDetails objCertificationDetails = new BusinessEntities.CertificationDetails();
                objCertificationDetails = (BusinessEntities.CertificationDetails)CertificationDetailsCollection.Item(i);

                Label CertificationId = (Label)gvCertification.Rows[i].FindControl(CERTIFICATIONID);
                Label Mode = (Label)gvCertification.Rows[rowIndex].FindControl(MODE);

                objCertificationDetails.CertificationId = int.Parse(CertificationId.Text);
                objCertificationDetails.EMPId = int.Parse(EMPId.Value);
                objCertificationDetails.CertificationName = gvCertification.Rows[i].Cells[0].Text;
                objCertificationDetails.CertificateDate = Convert.ToDateTime(gvCertification.Rows[i].Cells[1].Text);
                objCertificationDetails.CertificateValidDate = Convert.ToDateTime(gvCertification.Rows[i].Cells[2].Text);
                objCertificationDetails.Score = float.Parse(gvCertification.Rows[i].Cells[3].Text);
                objCertificationDetails.OutOf = float.Parse(gvCertification.Rows[i].Cells[4].Text);
                objCertificationDetails.Mode = int.Parse(Mode.Text);
            }

            //Clear all the fields after inserting row into gridview
            this.ClearControls();

            btnAddRow.Visible = true;
            btnUpdate.Visible = false;
            btnCancelRow.Visible = false;

            //Enabling all the edit buttons.
            for (int i = 0; i < gvCertification.Rows.Count; i++)
            {
                ImageButton btnImgEdit = (ImageButton)gvCertification.Rows[i].FindControl(IMGBTNEDIT);
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
        btnUpdate.Visible = false;
        btnCancelRow.Visible = false;
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
        txtCertificationDate.Text = string.Empty;
        txtCertficationValidDate.Text = string.Empty;
        txtTotalScore.Text = string.Empty;
        txtOutOf.Text = string.Empty;
    }

    /// <summary>
    /// Populates the grid.
    /// </summary>
    private void PopulateGrid(int employeeID)
    {
        CertificationDetailsCollection = this.GetCertificationDetails(employeeID);

        //Binding the datatable to grid
        gvCertification.DataSource = CertificationDetailsCollection;
        gvCertification.DataBind();

        //Displaying grid header with NO record found message.
        if (gvCertification.Rows.Count == 0)
            ShowHeaderWhenEmptyGrid();

        //EMPId.Value = "14";
        EMPId.Value = employeeID.ToString().Trim();

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
            throw ex;
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

        int TotalScore = Convert.ToInt32(txtTotalScore.Text);
        int OutOf = Convert.ToInt32(txtOutOf.Text);

        if (OutOf < TotalScore)
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.OUTOF_LESSTHAN_TOTALSCORE);
            flag = false;
        }

        certificateDateArr = Convert.ToString(txtCertificationDate.Text).Split(SPILITER_SLASH);
        DateTime certificateDate = new DateTime(Convert.ToInt32(certificateDateArr[2]), Convert.ToInt32(certificateDateArr[1]), Convert.ToInt32(certificateDateArr[0]));

        certificateValidEndDateArr = Convert.ToString(txtCertficationValidDate.Text).Split(SPILITER_SLASH);
        DateTime certificateValidEndDate = new DateTime(Convert.ToInt32(certificateValidEndDateArr[2]), Convert.ToInt32(certificateValidEndDateArr[1]), Convert.ToInt32(certificateValidEndDateArr[0]));

        if (certificateDate > DateTime.Now.Date)
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.CERTIFICATION_DATE_VALIDATION);
            flag = false;
        }

        if (certificateValidEndDate < DateTime.Now.Date)
        {
            errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.CERTIFICATION_VALIDDATE_VALIDATION);
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
