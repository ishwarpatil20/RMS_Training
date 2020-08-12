//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           MrfRaisePrevious.aspx.cs    
//  Author:         Sunil.Mishra
//  Date written:   27/8/2009/ 12:01:00 PM
//  Description:    This Page will be the entry page for Raising the MRF
//                  same page will use in View MRF & Edit MRF mode
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  27/8/2009/ 12:01:00 PM  Sunil.Mishra    n/a     Created     
//
//------------------------------------------------------------------------------
using System;
using Common;
using System.Collections;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common.Constants;
using System.Data;
using Common.AuthorizationManager;


public partial class MrfRaisePrevious : BaseClass
{
    #region Private Field Members
    //Declaring COllection class object
    BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

    //Declaring rave HR Collection class object
    BusinessEntities.RaveHRCollection raveHRCollectionCopy = new BusinessEntities.RaveHRCollection();

    //Create Object for mrfDetail for Business Layer
    Rave.HR.BusinessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.BusinessLayer.MRF.MRFDetail();

    //Create Object for mrfDetail for Business entity layer
    BusinessEntities.MRFDetail entitymRFDetail = new BusinessEntities.MRFDetail();

    //Split character by dash
    char[] SPILITER_DASH = { '-' };

    //Split character by Slash
    char[] SPILITER_SLASH = { '/' };

    //Define const ReadOnly
    const string ReadOnly = "readonly";

    //bool variable
    bool flag;
    ImageButton btn;
    //error message string builder.
    StringBuilder errMessage;

    //Date Format
    const string DATEFORMAT = "dd/MM/yyyy";

    //DateTime Variable
    DateTime ProjectStartDate;

    //DateTime Variable
    DateTime ProjectEndDate;

    //DateTime Variable
    DateTime RPStartDate;

    //DateTime Variable
    DateTime RPEndDate;

    //DataRow Variable
    DataRow dr = null;

    //string const.
    const string SYSTEMBOOLEAN = "System.Boolean";

    //string const.
    const string SYSTEMINT32 = "System.Int32";

    //string const.
    const string SYSTEMDATETIME = "System.DateTime";
    const string SYSTEMSTRING = "System.String";

    const string mode = "mode";

    const string CLASS_NAME_RAISEMRF_PREVIOUS = "MrfRaisePrevious";

    //declare department 
    private enum Department
    {
        Others,
        Projects
    }

    const string MoveMRF = "MoveMRF";    

    const string PAGETYPE = "pagetype";
    #endregion

    #region Page Property
    /// <summary>
    /// Role Property
    /// </summary>
    public string ROLE
    {
        get
        {
            if (ViewState["ROLE"] != null)
            {
                return Convert.ToString(ViewState["ROLE"]);
            }
            else
            {
                return string.Empty;
            }
        }
        set
        {
            ViewState["ROLE"] = value;
        }
    }

    /// <summary>
    /// PageMode Property
    /// </summary>
    private string PAGEMODE
    {
        get;
        set;
    }

    #endregion

    #region Protected Events
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        #region JavaScript

       // btnCopy.Attributes.Add(CommonConstants.EVENT_ONCLICK, "javascript:return CopyButtonClick();");
        btnNext.Attributes.Add(CommonConstants.EVENT_ONCLICK, "javascript:return ButtonClickValidate();");

        //Poonam : Issue : Disable Button : Starts
        btnNext.OnClientClick = "if(ButtonClickValidate()){" + ClientScript.GetPostBackEventReference(btnNext, null) + "}";
        //Poonam : Issue : Disable Button : Ends

        //btnNext.Attributes.Add(CommonConstants.EVENT_ONCLICK, "javascrript: return ValidateDateOfRequsition();");
        txtNoOfResources.Attributes.Add(CommonConstants.EVENT_ONBLUR, "ValidatedateforotherDepartments();");
        imgNoOfResources.Attributes.Add(CommonConstants.EVENT_ONMOUSEOVER, "javascript:ShowTooltip('" + spanNoOfResources.ClientID + "','" + Common.CommonConstants.MSG_NUMERIC + "');");
        imgNoOfResources.Attributes.Add(CommonConstants.EVENT_ONMOUSEOUT, "javascript:HideTooltip('" + spanNoOfResources.ClientID + "');");

        ddlMRFType.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzMRFType.ClientID + "','','');");

        //ddlPreviousMRF.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzddlPreviousMRF.ClientID + "','','');");
        GroupDropDownList2.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzddlPreviousMRF.ClientID + "','','');");

        txtClientName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "return ValidateControl('" + txtClientName.ClientID + "','" + imgClientName.ClientID + "','" + Common.CommonConstants.VALIDATE_ALPHA_NUMERIC_WITDSPACE + "');");
        imgClientName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOVER, "ShowTooltip('" + spanClientName.ClientID + "','" + Common.CommonConstants.MSG_ALPHA_NUMERIC + "');");
        imgClientName.Attributes.Add(Common.CommonConstants.EVENT_ONMOUSEOUT, "HideTooltip('" + spanClientName.ClientID + "');");

        //setting the width of DateControl
        ucDatePicker.Width = 221;
        ucDatePicker1.Width = 221;
        ucDatePicker2.Width = 221;



        #endregion
        try
        {
            if (!ValidateURL())
            {
                Response.Redirect(CommonConstants.INVALIDURL);
            }
            else
            {
                //Siddharth 26th August 2015 Start
                //Task ID:- 56487 Hide the pages access for normal employees
                ArrayList arrRolesForUser1 = new ArrayList();
                AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
                arrRolesForUser1 = RaveHRAuthorizationManager.getRolesForUser(RaveHRAuthorizationManager.getLoggedInUser());

                if (!(arrRolesForUser1.Contains(AuthorizationManagerConstants.ROLERPM) ||
                    arrRolesForUser1.Contains(AuthorizationManagerConstants.ROLEPM)))
                {
                    Response.Redirect(CommonConstants.PAGE_HOME, true);
                }
                //Siddharth 26th August 2015 End

                
                if (Session[SessionNames.CONFIRMATION_MESSAGE] != null)
                {
                    lblMessage.Text = Convert.ToString(Session[SessionNames.CONFIRMATION_MESSAGE]);
                    Session.Remove(SessionNames.CONFIRMATION_MESSAGE);
                }
                else
                {
                    lblMessage.Text = "";
                }

                if (!IsPostBack)
                {

                    //32911-Ambar-09012012-Start
                    // Initialise to null
                    raveHRCollectionCopy = null;
                    //32911-Ambar-09012012-End
                    
                    //Default focus on MRF Type
                    ddlDepartment.Focus();
                    

                    //Function will fill all the necessary dropdown on page load.
                    FillDropDowns();

                    // 28344-Ambar-Start
                    if (Session[SessionNames.MRFPREVIOUSVALUE] != null)
                    {
                      BusinessEntities.MRFDetail mRfDetailtemp;
                      mRfDetailtemp = (BusinessEntities.MRFDetail)Session[SessionNames.MRFPREVIOUSVALUE];

                      SetControlValues(mRfDetailtemp);

                      if (mRfDetailtemp.ProjectId != 0)
                      {
                        //--Get logged in user
                        Common.AuthorizationManager.AuthorizationManager objAuMan = new Common.AuthorizationManager.AuthorizationManager();
                        string strCurrentUser = objAuMan.getLoggedInUserEmailId();

                        BusinessEntities.MRFDetail objBEMRFDetail = new BusinessEntities.MRFDetail();
                        objBEMRFDetail.EmailId = strCurrentUser;

                        //--Get department list
                        Rave.HR.BusinessLayer.MRF.MRFDetail objBLLMRFDetail = new Rave.HR.BusinessLayer.MRF.MRFDetail();

                        ddlDepartment.Items.Clear();
                        ddlDepartment.DataSource = objBLLMRFDetail.GetMRFRaiseAccesForDepartmentByEmpId(objBEMRFDetail);
                        ddlDepartment.DataTextField = "DepartmentName";
                        ddlDepartment.DataValueField = "DepartmentId";
                        ddlDepartment.DataBind();

                        if (ddlDepartment.Items.Count > 0)
                        {
                          ddlDepartment.Items.Insert(0, new ListItem(CommonConstants.SELECT));
                        }
                        else
                        {
                          ddlDepartment.Items.Add(new ListItem(CommonConstants.SELECT, "0"));
                        }

                        ddlDepartment.SelectedItem.Value = mRfDetailtemp.DepartmentId.ToString();
                        ddlDepartment.SelectedItem.Text = mRfDetailtemp.DepartmentName;

                        ddlProjectName.SelectedItem.Value = mRfDetailtemp.ProjectId.ToString();
                        ddlProjectName.SelectedItem.Text = mRfDetailtemp.ProjectName;
                        FillResourcePlanDropDown(Convert.ToInt32(ddlProjectName.SelectedItem.Value));
                        txtProjectDesc.Text = mRfDetailtemp.ProjectDescription.ToString();

                        ddlResourcePlanCode.SelectedItem.Value = mRfDetailtemp.ResourcePlanId.ToString();
                        ddlResourcePlanCode.SelectedItem.Text = mRfDetailtemp.RPCode;

                        ddlRole.SelectedItem.Value = mRfDetailtemp.RoleId.ToString();
                        ddlRole.SelectedItem.Text = mRfDetailtemp.RoleName;
                        ddlRole.Enabled = true;
                        ddlrole_selectedindexchanged();

                        ucDatePicker.Text = DateTime.Now.ToString(DATEFORMAT);
                        ucDatePicker2.Text = mRfDetailtemp.ResourceOnBoard.ToString(DATEFORMAT);
                        ucDatePicker1.Text = mRfDetailtemp.ResourceReleased.ToString(DATEFORMAT);

                        txtNoOfResources.Text = "";
                      }
                      Session.Remove(SessionNames.MRFPREVIOUSVALUE);
                    }
                    // 28344-Ambar-End

                    #region RoleCheck
                    //Array list store logged in user role.
                    ArrayList arrRolesForUser = new ArrayList();

                    //Check Session Null value
                    if (Session[SessionNames.AZMAN_ROLES] != null)
                    {
                        //Assign variable from the session 
                        arrRolesForUser = (ArrayList)Session[SessionNames.AZMAN_ROLES];

                        //check arraylist count
                        if (arrRolesForUser.Count != 0)
                        {
                            //Check Role For RPM
                            if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM))
                            {
                                //Department dropdown will enable true
                                //--ddlDepartment.Enabled = true;

                                //Current role will be roleRPM
                                ROLE = AuthorizationManagerConstants.ROLERPM;
                            }
                            //Check role for PM,APM,GPM
                            else if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEPM) || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEAPM) || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEGPM))
                            {
                                //Check Role For PM,APM,GPM
                                ROLE = AuthorizationManagerConstants.ROLEPM;

                                int ProjectID = Convert.ToInt32(MasterEnum.Departments.Projects);

                                //--if (ddlDepartment.Items.Contains(new ListItem(Convert.ToString(MasterEnum.Departments.Projects), Convert.ToString(ProjectID))))
                                //--{
                                //--    ddlDepartment.SelectedValue = Convert.ToString(ProjectID);
                                //--    ddlDepartment_SelectedIndexChanged(null, null);
                                //--}

                                //--ddlDepartment.Enabled = false;
                                //--ddlProjectName.Enabled = true;
                                //--ddlResourcePlanCode.Enabled = true;
                                //Function will fill ProjectName DropDown
                                //--FillProjectNameDropDown();
                            }
                            else if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEFINANCE) || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEFM) || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLECFM))
                            {
                                //Check Role For FM, CFM

                                ROLE = AuthorizationManagerConstants.ROLECFM;

                                int Finance = Convert.ToInt32(MasterEnum.Departments.Finance);

                                //--if (ddlDepartment.Items.Contains(new ListItem(Convert.ToString(MasterEnum.Departments.Finance), Convert.ToString(Finance))))
                                //--{
                                //--    ddlDepartment.SelectedValue = Convert.ToString(Finance);
                                //--    DepartmentWiseRoleDispaly(Finance);
                                //--    ddlDepartment.Enabled = false;
                                //--    ddlProjectName.Enabled = false;
                                //--    ddlResourcePlanCode.Enabled = false;
                                //--}
                            }
                            else if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEPRESALES))
                            {
                                ROLE = AuthorizationManagerConstants.ROLEPRESALES;

                                //--
                                /* *
                                ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.Admin.ToString(), Convert.ToInt32(MasterEnum.Departments.Admin).ToString()));
                                ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.Finance.ToString(), Convert.ToInt32(MasterEnum.Departments.Finance).ToString()));
                                ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.HR.ToString(), Convert.ToInt32(MasterEnum.Departments.HR).ToString()));
                                ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.ITS.ToString(), Convert.ToInt32(MasterEnum.Departments.ITS).ToString()));
                                ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.Marketing.ToString(), Convert.ToInt32(MasterEnum.Departments.Marketing).ToString()));
                                ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.PMOQuality.ToString(), Convert.ToInt32(MasterEnum.Departments.PMOQuality).ToString()));
                                ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.Projects.ToString(), Convert.ToInt32(MasterEnum.Departments.Projects).ToString()));
                                ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.RaveDevelopment.ToString(), Convert.ToInt32(MasterEnum.Departments.RaveDevelopment).ToString()));
                                ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.Recruitment.ToString(), Convert.ToInt32(MasterEnum.Departments.Recruitment).ToString()));
                                ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.Support.ToString(), Convert.ToInt32(MasterEnum.Departments.Support).ToString()));
                                ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.Testing.ToString(), Convert.ToInt32(MasterEnum.Departments.Testing).ToString()));
                                ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.BA.ToString(), Convert.ToInt32(MasterEnum.Departments.BA).ToString()));
                                ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.Usability.ToString(), Convert.ToInt32(MasterEnum.Departments.Usability).ToString()));
                                ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.ATG.ToString(), Convert.ToInt32(MasterEnum.Departments.ATG).ToString()));
                                ddlDepartment.Items.Remove(new ListItem(MasterEnum.Departments.RPG.ToString(), Convert.ToInt32(MasterEnum.Departments.RPG).ToString()));
                                 * */

                                //--ddlProjectName.Enabled = false;
                                //--ddlResourcePlanCode.Enabled = false;
                                // Commented By Gaurav Thakkar as per requirement.
                                /*
                                int PreSales = Convert.ToInt32(Convert.ToInt32(MasterEnum.Departments.PreSales));
                                //Check Role For PreSales
                            
                                if (ddlDepartment.Items.Contains(new ListItem(Convert.ToString(MasterEnum.Departments.PreSales), Convert.ToString(PreSales))))
                                {
                                    ddlDepartment.SelectedValue = Convert.ToString(PreSales);
                                    DepartmentWiseRoleDispaly(PreSales);
                                    ddlDepartment.Enabled = false;
                                    ddlProjectName.Enabled = false;
                                    ddlResourcePlanCode.Enabled = false;
                                }
                                */
                            }
                            else if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERECRUITMENT))
                            {
                                ROLE = AuthorizationManagerConstants.ROLERECRUITMENT;

                                int Recruitment = Convert.ToInt32(MasterEnum.Departments.Recruitment);
                                /* *
                                if (ddlDepartment.Items.Contains(new ListItem(Convert.ToString(MasterEnum.Departments.Recruitment), Convert.ToString(Recruitment))))
                                {
                                    ddlDepartment.SelectedValue = Convert.ToString(Recruitment);
                                    DepartmentWiseRoleDispaly(Recruitment);
                                    ddlDepartment.Enabled = false;
                                    ddlProjectName.Enabled = false;
                                    ddlResourcePlanCode.Enabled = false;
                                }
                                 * */
                            }
                            else if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
                            {
                                ROLE = AuthorizationManagerConstants.ROLEHR;

                                int Hr = Convert.ToInt32(MasterEnum.Departments.HR);
                                /* *
                                if (ddlDepartment.Items.Contains(new ListItem(Convert.ToString(MasterEnum.Departments.HR), Convert.ToString(Hr))))
                                {
                                    ddlDepartment.SelectedValue = Convert.ToString(Hr);
                                    DepartmentWiseRoleDispaly(Hr);
                                    ddlDepartment.Enabled = false;
                                    ddlProjectName.Enabled = false;
                                    ddlResourcePlanCode.Enabled = false;
                                }
                                 * */
                            }
                            else if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLETESTING))
                            {
                                ROLE = AuthorizationManagerConstants.ROLETESTING;

                                int Testing = Convert.ToInt32(MasterEnum.Departments.Testing);

                                /* *
                                if (ddlDepartment.Items.Contains(new ListItem(Convert.ToString(MasterEnum.Departments.Testing), Convert.ToString(Testing))))
                                {
                                    ddlDepartment.SelectedValue = Convert.ToString(Testing);
                                    DepartmentWiseRoleDispaly(Testing);
                                    ddlDepartment.Enabled = false;
                                    ddlProjectName.Enabled = false;
                                    ddlResourcePlanCode.Enabled = false;
                                }
                                 * */
                            }
                            else if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEQUALITY))
                            {
                                ROLE = AuthorizationManagerConstants.ROLEQUALITY;

                                int Quality = Convert.ToInt32(MasterEnum.Departments.PMOQuality);
                                /* *
                                if (ddlDepartment.Items.Contains(new ListItem(Convert.ToString(MasterEnum.Departments.PMOQuality), Convert.ToString(Quality))))
                                {
                                    ddlDepartment.SelectedValue = Convert.ToString(Quality);
                                    DepartmentWiseRoleDispaly(Quality);
                                    ddlDepartment.Enabled = false;
                                    ddlProjectName.Enabled = false;
                                    ddlResourcePlanCode.Enabled = false;
                                }
                                 * */
                            }
                            else if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEMH))
                            {
                                ROLE = AuthorizationManagerConstants.ROLEMH;

                                int Marketing = Convert.ToInt32(MasterEnum.Departments.Marketing);
                                /* *
                                 if (ddlDepartment.Items.Contains(new ListItem(Convert.ToString(MasterEnum.Departments.Marketing), Convert.ToString(Marketing))))
                                 {
                                     ddlDepartment.SelectedValue = Convert.ToString(Marketing);
                                     DepartmentWiseRoleDispaly(Marketing);
                                     ddlDepartment.Enabled = false;
                                     ddlProjectName.Enabled = false;
                                     ddlResourcePlanCode.Enabled = false;
                                 }
                                  * */
                            }
                            else if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERAVECONSULTANT))
                            {
                                ROLE = AuthorizationManagerConstants.ROLERAVECONSULTANT;

                                int RaveConsultant = Convert.ToInt32(MasterEnum.Departments.RaveConsultant_India);
                                /* *
                                if (ddlDepartment.Items.Contains(new ListItem(Convert.ToString(MasterEnum.Departments.RaveConsultant), Convert.ToString(RaveConsultant))))
                                {
                                    ddlDepartment.SelectedValue = Convert.ToString(RaveConsultant);
                                    DepartmentWiseRoleDispaly(RaveConsultant);
                                    ddlDepartment.Enabled = false;
                                    ddlProjectName.Enabled = false;
                                    ddlResourcePlanCode.Enabled = false;
                                }
                                 * */
                            }
                        }
                        //If department is selected is disabled & not a project.
                        if (ddlDepartment.Enabled == false && Convert.ToInt32(ddlDepartment.SelectedItem.Value) != Convert.ToInt32(Common.MasterEnum.Departments.Projects))
                        {
                            /* *
                            ProjectDetails.Visible = false;
                            lblProjDescription.Visible = false;
                            txtProjectDesc.Visible = false;
                            labProjectDescription.Visible = false;
                             * */
                        }
                    }
                    #endregion

                    //25988-Ambar-Start
                    FillDPStatusDropDown();
                    string strnull = null;  
                    //25988-Ambar-Start

                    //Function will fill copy MRF Dropdown
                    FillCopyMRFdLL();


                    //vandana
                    //string String_DeptProjectNamev ;//= ddlMRFDeptCopy.SelectedItem.Text.ToString();
                    //int int_DeptProjectMRFRoleIDv ;//= Convert.ToInt32(ddlMRFFilterRoleCopy.SelectedValue);

                    string String_DeptProjectNamev = null;
                    int int_DeptProjectMRFRoleIDv = -1;



                    FillMRFCodeDropDowns(String_DeptProjectNamev, int_DeptProjectMRFRoleIDv, strnull);//25988-Ambar
                    //vandana
                    FillDepartmentProjectNamesDropDown(strnull);//25988-Ambar
                    

                    //Assign textbox with current Datetime.
                    ucDatePicker.Text = DateTime.Now.ToString(DATEFORMAT);

                    //if page is calling from Pending Allocation Screen.
                    if (Request.QueryString[MoveMRF] != null)
                    {
                        Boolean allocateResource = Convert.ToBoolean(DecryptQueryString(MoveMRF).ToString());
                        if (Request.QueryString[PAGETYPE] != null)
                        {

                            if (allocateResource && DecryptQueryString(PAGETYPE) == MoveMRF)
                            {
                                PAGEMODE = Convert.ToString(MasterEnum.PageMode.MOVE);

                                //Function to populate data in the controls and set the visiblity.
                                int mrfId = Convert.ToInt32(DecryptQueryString(QueryStringConstants.MRF_ID).ToString()); ;
                                PopulateControlForMoveMRF(mrfId);
                            }

                        }
                    }
                    //Check the Mode of Query string
                    if (Request.QueryString[mode] != null)
                    {
                        if (DecryptQueryString(mode).ToString() == Convert.ToString(Common.MasterEnum.PageMode.PREVIOUS))
                        {
                            if (Session[SessionNames.MRFGRIDFILL] != null)
                            {
                                //If Grid with Session Object
                                BindGridWithSession();
                            }

                          // 28344-Ambar-Start
                          // Commented out below code
                          ////check session is null
                          //  if (Session[SessionNames.MRFPREVIOUSVALUE] != null)
                          //  {
                          //      BusinessEntities.MRFDetail mrfDetailobj = new BusinessEntities.MRFDetail();
                          //      mrfDetailobj = (BusinessEntities.MRFDetail)Session[SessionNames.MRFPREVIOUSVALUE];

                          //      //Set Control Values
                          //      SetControlValues(mrfDetailobj);
                          //  }
                          //  else
                          //  {
                          //      //Function will reset controls
                          //      ResetColtrol();
                          //  }
                          // 28344-Ambar-End
                        }
                    }

                    if (ddlDepartment.SelectedItem.Text != Common.CommonConstants.SELECT)
                    {
                        if (Convert.ToInt32(ddlDepartment.SelectedItem.Value) == Convert.ToInt32(Common.MasterEnum.Departments.Projects))
                        {
                            txtNoOfResources.ReadOnly = true;
                        }
                        else
                        {
                            txtNoOfResources.ReadOnly = false;
                        }
                    }
                    // 28344-ambar-End
                    // Rajan Kumar : Issue 36749 : 07/01/2014 : Starts                        			 
                    // Desc : Required Resource checkbox is unchecked after coming back using Previous button
                    if (Request.QueryString[mode] != null)
                    {
                        if (DecryptQueryString(mode).ToString() == Convert.ToString(Common.MasterEnum.PageMode.PREVIOUS))
                        {
                            if (Session[SessionNames.MRFGRIDFILL] != null)
                            {
                                bool status=txtNoOfResources.ReadOnly;
                                txtNoOfResources.ReadOnly = false;
                                txtNoOfResources.Text = SelectedCheckBoxCount();                                
                                txtNoOfResources.ReadOnly = status;
                            }
                        }
                    }
                    // Rajan Kumar : Issue 36749 : 07/01/2014 : Ends 
                }                
            } 
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, 
                CLASS_NAME_RAISEMRF_PREVIOUS, "Page_Load", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnNext_Click(object sender, EventArgs e)
    {
        try

        {
            //Vandana
            // Issue Id 18773. Validation for Date of Requistion in MRF for Future Dates
            DateTime selecteddate = Convert.ToDateTime(ucDatePicker.Text);
            DateTime CurrentDate = Convert.ToDateTime(DateTime.Now.ToString(DATEFORMAT));
            if (selecteddate > Convert.ToDateTime(CurrentDate))
            {
                lblMessage.Text = "The Date of Requisition cannot be greater than today's date.";
                return;
            }
            //

            if (ValidateControl() == true)
            {
                bool validationStatus = false;
                //Modified By Rajesh; Issue Id 20516. Validations for Billing Date
                if (Convert.ToInt32(ddlDepartment.SelectedItem.Value) == Convert.ToInt32(Common.MasterEnum.Departments.Projects))
                {
                    for (int i = 0; i < grdresource.Rows.Count; i++)
                    {
                        CheckBox chkRPDuId = (CheckBox)grdresource.Rows[i].Cells[0].FindControl("chk");
                        Label lblBilling = (Label)grdresource.Rows[i].FindControl("lblBilling");
                        int billingAmount = Convert.ToInt32(lblBilling.Text);

                        if (chkRPDuId.Checked)
                        {
                            if (billingAmount > 0)
                            {
                                DateTime RPStartDate = Convert.ToDateTime(grdresource.Rows[i].Cells[2].Text);
                                DateTime RPEndDate = Convert.ToDateTime(grdresource.Rows[i].Cells[3].Text);
                                UIControls_DatePickerControl billingDatePicker = (UIControls_DatePickerControl)grdresource.Rows[i].Cells[5].FindControl("billingDatePicker");

                                if (billingDatePicker.Text != null && billingDatePicker.Text != "")
                                {
                                    DateTime billingDate = Convert.ToDateTime(billingDatePicker.Text);
                                    if ((billingDate < RPStartDate) || (billingDate > RPEndDate))
                                    {                                        
                                        lblMessage.Text = "Billing Date should be between Starting date and Ending date";
                                        lblMessage.ForeColor = System.Drawing.Color.Red;
                                        billingDatePicker.TextBox.BorderStyle = BorderStyle.Solid;
                                        billingDatePicker.TextBox.BorderColor = System.Drawing.Color.Red;
                                        if (hidResourcesCount.Value != string.Empty)
                                        {
                                            txtNoOfResources.Text = hidResourcesCount.Value;
                                        }
                                        return;
                                    }
                                    else
                                    {
                                        validationStatus = true;
                                    }
                                }
                                else
                                {
                                    lblMessage.Text = "Billing date Can't be empty";
                                    lblMessage.ForeColor = System.Drawing.Color.Red;
                                    billingDatePicker.TextBox.BorderStyle = BorderStyle.Solid;
                                    billingDatePicker.TextBox.BorderColor = System.Drawing.Color.Red;
                                    if (hidResourcesCount.Value != string.Empty)
                                    {
                                        txtNoOfResources.Text = hidResourcesCount.Value;
                                    }
                                    return;
                                }
                            }
                            else
                            {
                                validationStatus = true;
                            }
                        }
                        else
                        {
                            validationStatus = true;
                        }
                    }                    
                }
                else
                {
                    validationStatus = true;
                }
                if (validationStatus)
                {
                GetControlValue();
                //Aarohi : Issue 28735(CR) : 26/12/2011 : Start
                BusinessEntities.MRFDetail mRfDetail = new BusinessEntities.MRFDetail();
                if (Session[SessionNames.MRFPREVIOUSVALUE] != null)
                {
                    mRfDetail = (BusinessEntities.MRFDetail)Session[SessionNames.MRFPREVIOUSVALUE];
                }
                Session[SessionNames.PROJECT_NAME] = mRfDetail.ProjectName;
                Session[SessionNames.ROLE] = mRfDetail.RoleName;
                Session[SessionNames.DEPARTMENT_NAME] = mRfDetail.DepartmentName;
                //Aarohi : Issue 28735(CR) : 26/12/2011 : End
                Server.Transfer("MrfRaiseNext.aspx", false);
                }               
            }
            else
            {
                CheckCheckBox();
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (System.Threading.ThreadAbortException IException)
        {

        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, 
                CLASS_NAME_RAISEMRF_PREVIOUS, "btnNext_Click", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// Dropdown Selected Index Changed Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlProjectName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            /*Clear the grid session when department changes -- Gaurav Thakkar*/
            // Rajan Kumar : Issue 36749 : 07/01/2014 : Starts                        			 
            // Desc : Required Resource checkbox is unchecked after coming back using Previous button
            if (Request.QueryString[mode] == null)
            {
                Session[SessionNames.MRFGRIDFILL] = null;
            }
            // Rajan Kumar : Issue 36749 : 07/01/2014 : Ends 
            /*Clear the Copy session when Project Name changes -- Gaurav Thakkar*/
            // 28513-Ambar-Start-05092011
            // Commented out following to preserve the MRFCOPY session value till cancel
            // Session[SessionNames.MRFCOPY] = null;
            // 28513-Ambar-End-05092011
            /*Clear the Previous session when department changes -- Gaurav Thakkar*/
            Session[SessionNames.MRFPREVIOUSVALUE] = null;
            if (Session[SessionNames.MRFGRIDFILL] == null)
            {
                grdresource.Visible = false;
            }
            if (ddlProjectName.SelectedItem.Text != CommonConstants.SELECT)
            {
                FillResourcePlanDropDown(Convert.ToInt32(ddlProjectName.SelectedItem.Value));
                GetProjectDescription(ddlProjectName.SelectedIndex - 1);
                txtNoOfResources.Text = "";
                txtNoOfResources.ReadOnly = true;
                ddlRole.ClearSelection();
                ddlRole.Enabled = false;
            }
            #region Modified By Mohamed Dangra
            // Project description should be blank after reselecting the default value of Project "Select"
            else
            {
                txtProjectDesc.Text = "";
            }
            #endregion Modified By Mohamed Dangra
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_PREVIOUS, "ddlProjectName_SelectedIndexChanged", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Dropdown Selected Index Changed Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlResourcePlanCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlResourcePlanCode.SelectedItem.Text != CommonConstants.SELECT)
            {
                ddlRole.Enabled = true;

                FillRoleDropDown(Convert.ToInt32(ddlResourcePlanCode.SelectedValue), Convert.ToInt32(MasterEnum.RPDurationEditionStatus.Deleted));
                txtNoOfResources.ReadOnly = true;
                txtNoOfResources.Text = "";
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_PREVIOUS, "ddlResourcePlanCode_SelectedIndexChanged", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Dropdown Selected Index Changed Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProjectName.Enabled == true)
            {
                if (ddlRole.SelectedItem.Text != CommonConstants.SELECT)
                {
                    grdresource.Visible = true;
                    FillResourceGrid();
                }
                else
                {
                    grdresource.Visible = false;
                }
                txtNoOfResources.ReadOnly = true;
                txtNoOfResources.Text = "";
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_PREVIOUS, "ddlRole_SelectedIndexChanged", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Copy Button CLick Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCopy_Click(object sender, EventArgs e)
    {

        try
        {
          // 28343-Ambar-Start-15072011
          if (ddlMRFFilterCopy.SelectedValue == CommonConstants.SELECT)
          {
            lblMessage.Text = "<font color=RED>" + "Please select a MRF Code to copy" + "</font>";
            return;                                        
          }  
          // 28343-Ambar-End-15072011

          PAGEMODE = Convert.ToString(MasterEnum.PageMode.COPY);

            //FillMRFCodeDropDowns(ddlMRFDeptCopy.SelectedItem.Text.ToString());
            //raveHRCollectionCopy = mRFDetail.CopyMRFBL(Convert.ToInt32(ddlPreviousMRF.SelectedItem.Value));
          // raveHRCollectionCopy = mRFDetail.CopyMRFBL(Convert.ToInt32(GroupDropDownList2.SelectedItem.Value));
           raveHRCollectionCopy = mRFDetail.CopyMRFBL(Convert.ToInt32(ddlMRFFilterCopy.SelectedItem.Value));
          
          

            if (raveHRCollectionCopy != null)
            {
                for (int i = 0; i < raveHRCollectionCopy.Count; i++)
                {
                    entitymRFDetail = (BusinessEntities.MRFDetail)raveHRCollectionCopy.Item(i);
                    SetControlValues(entitymRFDetail);
                    grdresource.Visible = false;
                    Session[SessionNames.MRFCOPY] = entitymRFDetail;
                }
            }



           
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, 
                CLASS_NAME_RAISEMRF_PREVIOUS, "btnCopy_Click", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// Cancel Button CLick Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (Session[SessionNames.MRFPREVIOUSVALUE] != null)
        {
            Session.Remove(SessionNames.MRFPREVIOUSVALUE);
        }
        if (Session[SessionNames.MRFCOPY] != null)
        {
            Session.Remove(SessionNames.MRFCOPY);
        }

        Response.Redirect("Home.aspx");
    }

    /// <summary>
    /// Grid Row DataBound Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdresource_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chk = (CheckBox)e.Row.FindControl("chk");
            btn = (ImageButton)e.Row.FindControl("imgView");
            Label lblRPDurationId = (Label)e.Row.FindControl("lblRPDuId");

            Label lblBilling = (Label)e.Row.FindControl("lblBilling");

            //Modified By Rajesh; Issue Id 20516. Code for enabling and disabling Billing Date field

            if (Convert.ToInt32(ddlDepartment.SelectedItem.Value) == Convert.ToInt32(Common.MasterEnum.Departments.Projects)
             || Convert.ToInt32(ddlDepartment.SelectedItem.Value) == Convert.ToInt32(Common.MasterEnum.Departments.RaveForecastedProjects))
            {
                UIControls_DatePickerControl billingDatePicker = (UIControls_DatePickerControl)e.Row.FindControl("billingDatePicker");
                //int billingAmount = ((BusinessEntities.MRFDetail)(e.Row.DataItem)).Billing;
                // Rajan Kumar : Issue 36749 : 07/01/2014 : Starts                        			 
                // Desc : When MRF is raised against department then MRF end date is not mandatory (remove validation). 
                if (Session[SessionNames.MRFGRIDFILL] != null)
                {
                    DataTable RPGriddt = (DataTable)Session[SessionNames.MRFGRIDFILL];
                    if (RPGriddt != null)
                    {
                        if (RPGriddt.Rows.Count > 0)
                        {                           
                            foreach (DataRow row in RPGriddt.Rows)
                            {
                                if (row[CommonConstants.RESOURCEPLANDURATIONID].ToString() == lblRPDurationId.Text)
                                {

                                    if (!string.IsNullOrEmpty(row[CommonConstants.RESOURCEBILLINGDATE].ToString()))
                                    {                                       
                                       
                                        if (Convert.ToDateTime(row[CommonConstants.RESOURCEBILLINGDATE]) != new DateTime())
                                        {
                                            billingDatePicker.Text = Convert.ToDateTime(row[CommonConstants.RESOURCEBILLINGDATE]).ToString(DATEFORMAT);
                                        }
                                        else
                                        {
                                            billingDatePicker.Text = string.Empty;
                                        }
                                      
                                    }                                    
                                }
                            }  
                        }
                    }
                }
               
                int billingAmount = Convert.ToInt32(lblBilling.Text);
                // Rajan Kumar : Issue 45752 : 03/01/2013: Ends  
                if (billingAmount > 0)
                {
                    billingDatePicker.IsEnable = true;
                }
                else
                {
                    billingDatePicker.IsEnable = false;
                    billingDatePicker.Text = "";
                }
            }
            
            string RPStartDate = e.Row.Cells[2].Text;
            string RPEndDate = e.Row.Cells[3].Text;

            //chk.Attributes.Add("onclick", "ValidateCheckBox('" + chk.ClientID + "','" + RPStartDate + "','" + RPEndDate + "')");
            chk.Attributes.Add("onclick", "ValidateCheckBox('" + chk.ClientID + "')");            
            btn.Attributes.Add("onclick", "return popUpResourceSplitDuration('" + lblRPDurationId.Text + "');");
            string URL = string.Empty;
            URL += "ResourcePlanLocationList.aspx?" + URLHelper.SecureParameters("RPDuId", lblRPDurationId.Text) + "&" + URLHelper.CreateSignature(lblRPDurationId.Text);
            hidEncryptedQueryString.Value = URL;
        }
    }

    protected void grdresource_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "View")
            {
                GridView _gridView = (GridView)sender;
                // Get the selected index and the command name
                int _selectedIndex = int.Parse(e.CommandArgument.ToString());
                string _commandName = e.CommandName;
                grdresource.SelectedIndex = _selectedIndex;
                GridViewRow dr = grdresource.Rows[_selectedIndex];
                Label lbl = (Label)dr.Cells[0].FindControl("lblRPDuId");
                hidRPId.Value = lbl.Text;
                btn = (ImageButton)dr.Cells[0].FindControl("imgView");
                btn.Attributes.Add("onclick", "return popUpResourceSplitDuration();");
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "MrfRaisePrevious", "grdresource_RowCommand", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    /// <summary>
    /// Dropdown Selected Index Changed Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            /*Clear the grid session when department changes -- Gaurav Thakkar*/
            // Rajan Kumar : Issue 36749 : 07/01/2014 : Starts                        			 
            // Desc : Required Resource checkbox is unchecked after coming back using Previous button
            if (Request.QueryString[mode] == null)
            {
                Session[SessionNames.MRFGRIDFILL] = null;
            }
            // Rajan Kumar : Issue 36749 : 07/01/2014 : Ends 
            /*Clear the Copy session when department changes -- Gaurav Thakkar*/
            // 28513-Ambar-Start-05092011
            // Commented out following to preserve the MRFCOPY session value till cancel            
            // Session[SessionNames.MRFCOPY] = null;
            // 28513-Ambar-End-05092011
            /*Clear the Previous session when department changes -- Gaurav Thakkar*/
            Session[SessionNames.MRFPREVIOUSVALUE] = null;

            if (ddlDepartment.SelectedItem.Text != Common.CommonConstants.SELECT)
            {
                FillProjectNameDropDown();
                hidDepartment.Value = ddlDepartment.SelectedItem.Value;

                if (Convert.ToInt32(ddlDepartment.SelectedItem.Value) == Convert.ToInt32(Common.MasterEnum.Departments.Projects))
                {
                    ddlProjectName.Enabled = true;
                    ddlResourcePlanCode.Enabled = true;
                    txtProjectDesc.Enabled = true;
                    txtNoOfResources.ReadOnly = true;
                    ddlRole.Enabled = false;
                    ddlRole.SelectedIndex = -1;
                    txtNoOfResources.Text = "";
                    //If department is "Projects" than hide the row which has MRF date range
                    // Since in this case Dates come from the Grid                                    
                    MrfDateRange.Visible = false;
                    // Rajan Kumar : Issue 45752 : 03/01/2014 : Starts                        			 
                    // Desc : When MRF is raised against department then MRF end date is not mandatory (remove validation).
                    lblRequiredFrom.Visible = false;
                    RequiredFrommandatorymark.Visible = false;
                    imgRequiredFromError.Visible = false;
                    ucDatePicker2.Visible = false;
                    // Rajan Kumar : Issue 45752 : 03/01/2014: Ends 
                   
                    
         
                    //Project Details fields are : "Project Name" and "Resource Plan Code"
                    ProjectDetails.Visible = true;                    
                    //Project Description field is made visible
                    //ProjDesc1.Visible = true;
                    //ProjDesc2.Visible = true;
                    //ProjDesc3.Visible = true;
                    lblProjDescription.Visible = true;
                    labProjectDescription.Visible = true;
                    txtProjectDesc.Visible = true;
                    lblClientName.Visible = false;
                    txtClientName.Visible = false;

                    lblSOWEndDt.Visible = false;
                    DatePickerSOWEndDate.Visible = false;
                    lblSOWStDt.Visible = false;
                    DtPckrSOWStartDate.Visible = false;
                    lblSOWNo.Visible = false;
                    txtSOWNo.Visible = false;
                    SOWEndDatemandatorymark.Visible = false;
                    SOWStartDatemandatorymark.Visible = false; 
                }
                else
                {
                    ddlProjectName.Enabled = false;
                    ddlResourcePlanCode.Enabled = false;
                    txtProjectDesc.Enabled = false;
                    txtProjectDesc.Text = "";
                    txtNoOfResources.ReadOnly = false;
                    txtNoOfResources.Text = string.Empty;
                    ddlProjectName.SelectedIndex = -1;
                    ddlResourcePlanCode.SelectedIndex = -1;
                    grdresource.Visible = false;
                    ddlRole.Enabled = true;
                    //MAKE THE MRF DATE RANGE ROW VISIBLE for departments other than "Projects".                   
                    // Rajan Kumar : Issue 45752 : 03/01/2014 : Starts                        			 
                    // Desc : When MRF is raised against department then MRF end date is not mandatory (remove validation).
                    //Now from date move to another <tr> so no need to show MrfDateRange
                    //MrfDateRange.Visible = true;
                    MrfDateRange.Visible = false;
                    // Rajan Kumar : Issue 45752 : 03/01/2014: Ends                    
                    ucDatePicker2.Text = string.Empty;
                    ucDatePicker1.Text = string.Empty;
                    //Project Details fields are : "Project Name" and "Resource Plan Code"
                    ProjectDetails.Visible = false;
                    //Project Description field is made invisible
                    //ProjDesc1.Visible = false;
                    //ProjDesc2.Visible = false;
                    //ProjDesc3.Visible = false;
                    lblProjDescription.Visible = false;
                    labProjectDescription.Visible = false;
                    txtProjectDesc.Visible = false;

                    // Rajan Kumar : Issue 45752 : 03/01/2014 : Starts                        			 
                    // Desc : When MRF is raised against department then MRF end date is not mandatory (remove validation).
                    lblRequiredFrom.Visible = true;
                    RequiredFrommandatorymark.Visible = true;
                    imgRequiredFromError.Visible = true;
                    ucDatePicker2.Visible = true;
                    // Rajan Kumar : Issue 45752 : 03/01/2014: Ends                   

                    #region Modified By Gaurav Thakkar

                    /*Changes made to show/hide the client details*/
                    if (ddlDepartment.SelectedValue == ((int)MasterEnum.MRFDepartment.PreSales_UK).ToString()
                        || ddlDepartment.SelectedValue == ((int)MasterEnum.MRFDepartment.PreSales).ToString()
                        || ddlDepartment.SelectedValue == ((int)MasterEnum.MRFDepartment.PreSales_US).ToString()
                        || ddlDepartment.SelectedValue == ((int)MasterEnum.MRFDepartment.RaveConsultant_India).ToString()
                        //|| ddlDepartment.SelectedValue == ((int)MasterEnum.MRFDepartment.PreSales_India).ToString()
                        //|| ddlDepartment.SelectedValue == ((int)MasterEnum.MRFDepartment.RaveConsultant_USA).ToString()
                        //|| ddlDepartment.SelectedValue == ((int)MasterEnum.MRFDepartment.RaveConsultant_UK).ToString()
                        || ddlDepartment.SelectedValue == ((int)MasterEnum.MRFDepartment.RaveForecastedProject).ToString()
                        )
                    {
                        lblClientName.Visible = true;
                        labProjectDescription.Visible = true;
                        txtClientName.Visible = true;
                    }
                    else
                    {
                        lblClientName.Visible = false;
                        labProjectDescription.Visible = false;
                        txtClientName.Visible = false;
                       
                    }

                    #endregion Modified By Gaurav Thakkar

                    //Fill the role value as per Department
                    DepartmentWiseRoleDispaly(Convert.ToInt32(ddlDepartment.SelectedItem.Value));


                    #region Modified By Vandana Mestri

                    if (ddlDepartment.SelectedValue == ((int)MasterEnum.MRFDepartment.RaveConsultant_USA).ToString()
                        || ddlDepartment.SelectedValue == ((int)MasterEnum.MRFDepartment.RaveConsultant_UK).ToString()
                        || ddlDepartment.SelectedValue == ((int)MasterEnum.MRFDepartment.RaveConsultant_India).ToString()
                       )
                    {

                        lblClientName.Visible = true;
                        labProjectDescription.Visible = true;
                        txtClientName.Visible = true;

                        lblSOWEndDt.Visible = true;
                        lblSOWStDt.Visible = true;
                        lblSOWNo.Visible = true;
                        DtPckrSOWStartDate.Visible = true;
                        DatePickerSOWEndDate.Visible = true;
                        txtSOWNo.Visible = true;
                        DtPckrSOWStartDate.Width = 221;
                        DatePickerSOWEndDate.Width = 221;
                    }
                    else
                    {
                        //lblProjDescription.Visible = true;
                        //labProjectDescription.Visible = true;
                        //txtProjectDesc.Visible = true;

                        //lblClientName.Visible = false;
                        //labProjectDescription.Visible = false;
                        //txtClientName.Visible = false;

                        lblSOWEndDt.Visible = false;
                        DatePickerSOWEndDate.Visible = false;
                        lblSOWStDt.Visible = false;
                        DtPckrSOWStartDate.Visible = false;
                        lblSOWNo.Visible = false; 
                        txtSOWNo.Visible = false;
                        SOWEndDatemandatorymark.Visible = false;
                        SOWStartDatemandatorymark.Visible = false;  
                    }

                    #endregion Modified By Vandana Mestri

                }
            }
            else
            {
                //clear items in role dropdown and Insert Select as a Item for Dropdown
                ddlRole.Items.Clear();
                ddlRole.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                //clear items in projectName dropdown and Insert Select as a Item for Dropdown
                ddlProjectName.Items.Clear();
                ddlProjectName.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                //clear items in resourcePlanCode dropdown and Insert Select as a Item for Dropdown
                ddlResourcePlanCode.Items.Clear();
                ddlResourcePlanCode.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                //Enable fields
                ProjectDetails.Visible = true;
                ddlProjectName.Enabled = true;
                ddlResourcePlanCode.Enabled = true;
                lblProjDescription.Visible = true;
                labProjectDescription.Visible = true;
                txtProjectDesc.Visible = true;
                #region Modified By Mohamed Dangra
                // Mohamed : Issue 37011 : 29/04/2013 : Starts                        			  
                // Desc : Project description should be blank after reselecting the default value of Department "Select"
                txtProjectDesc.Text = "";
                // Mohamed : Issue 37011 : 29/04/2013 : Ends
                #endregion Modified By Mohamed Dangra
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_PREVIOUS, "ddlDepartment_SelectedIndexChanged", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    #endregion

    #region Private Member Functions

    /// <summary>
    /// Function will Bind Grid with Session Object
    /// </summary>
    private void BindGridWithSession()
    {
        try
        {
            DataTable RPGriddt = (DataTable)Session[SessionNames.MRFGRIDFILL];
            grdresource.DataSource = RPGriddt;
            grdresource.DataBind();
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_PREVIOUS, "BindGridWithSession", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Fucntion will Set Control Values
    /// </summary>
    /// <param name="mrfDetailobj"></param>
    private void SetControlValues(BusinessEntities.MRFDetail mrfDetailobj)
    {
        try
        {
            //Check MRF Type contrins or not
            if (ddlMRFType.Items.Contains(new ListItem(CommonConstants.SELECT)))
            {
                ddlMRFType.SelectedValue = Convert.ToString(mrfDetailobj.MRfType);
            }

            //Umesh: Runtime exception on clicking of 'Copy' button Starts
            if (ddlDepartment.Items.FindByValue(mrfDetailobj.DepartmentId.ToString()) == null)
            {
                lblMessage.Text = "You are not authorised to copy this MRF.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            //Umesh: Runtime exception on clicking of 'Copy' button Ends

            ddlDepartment.SelectedValue = Convert.ToString(mrfDetailobj.DepartmentId);

            //Function will fill Project Name DropDown
            FillProjectNameDropDown();

            //Check if Project is Zero or not
            if (mrfDetailobj.ProjectId != 0)
            {
                //checks if Department is of Projects type
                if (mrfDetailobj.DepartmentId == Convert.ToInt32(Department.Projects))
                {
                    ProjectDetails.Visible = true;
                    lblProjDescription.Visible = true;
                    labProjectDescription.Visible = true;
                    txtProjectDesc.Visible = true;
                    ddlProjectName.Enabled = true;
                    ddlResourcePlanCode.Enabled = true;
                    ddlResourcePlanCode.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
                }
                else
                {
                    MrfDateRange.Visible = true;
                }

                if (ddlProjectName.Items.Contains(new ListItem(mrfDetailobj.ProjectName, Convert.ToString(mrfDetailobj.ProjectId))) == true)
                {
                    ddlProjectName.SelectedValue = Convert.ToString(mrfDetailobj.ProjectId);
                    ddlProjectName.Enabled = true;
                    ddlResourcePlanCode.Enabled = true;
                    GetProjectDescription(ddlProjectName.SelectedIndex - 1);
                    FillResourcePlanDropDown(Convert.ToInt32(ddlProjectName.SelectedValue));
                    if (ddlResourcePlanCode.Items.Contains(new ListItem(mrfDetailobj.RPCode, Convert.ToString(mrfDetailobj.ResourcePlanId))) == true)
                    {
                        ddlResourcePlanCode.SelectedValue = Convert.ToString(mrfDetailobj.ResourcePlanId);
                        FillRoleDropDown(Convert.ToInt32(ddlResourcePlanCode.SelectedValue), Convert.ToInt32(MasterEnum.RPDurationEditionStatus.Deleted));
                    }

                    txtNoOfResources.ReadOnly = true;
                    MrfDateRange.Visible = false;
                    lblClientName.Visible = false;
                    txtClientName.Visible = false;
                }
            }
            else if (mrfDetailobj.DepartmentName.Contains("RaveConsultant") || mrfDetailobj.DepartmentName.Contains("Presales -"))
            {
                 if (mrfDetailobj.ClientName != "")
                {
                    lblClientName.Visible = true;
                    labProjectDescription.Visible = false;
                    lblProjDescription.Visible = false;
                    txtProjectDesc.Visible = false;
                    txtClientName.Visible = true;
                    txtClientName.Text = mrfDetailobj.ClientName;
                }
            }
            else
            {
                ddlProjectName.Enabled = false;
                ddlResourcePlanCode.Items.Clear();
                ddlResourcePlanCode.Enabled = false;
                DepartmentWiseRoleDispaly(mrfDetailobj.DepartmentId);
                txtNoOfResources.ReadOnly = false;
                txtProjectDesc.Text = string.Empty;
                /*Modified by Gaurav Thakkar; Set the required fields visibility to false.*/
                ProjectDetails.Visible = false;
                //ProjDesc1.Visible = false;
                //ProjDesc2.Visible = false;
                //ProjDesc3.Visible = false;
                lblProjDescription.Visible = false;
                labProjectDescription.Visible = false;
                txtProjectDesc.Visible = false;
                MrfDateRange.Visible = true;

            }

            ddlDepartment_SelectedIndexChanged(null, null);

            //Check Role Dropdown 
            if (PAGEMODE == Convert.ToString(MasterEnum.PageMode.COPY))
            {
                if (ddlRole.Items.Contains(new ListItem(mrfDetailobj.Role, Convert.ToString(mrfDetailobj.RoleId))))
                {
                    ddlRole.SelectedValue = Convert.ToString(mrfDetailobj.RoleId);
                    ddlRole.SelectedIndex = -1;
                }
            }
            else
            {
                if (ddlRole.Items.Contains(new ListItem(mrfDetailobj.RoleName, Convert.ToString(mrfDetailobj.RoleId))))
                {
                    ddlRole.SelectedValue = Convert.ToString(mrfDetailobj.RoleId);
                    // Rajan Kumar : Issue 45752 : 16/01/2014 : Starts                        			 
                    // Desc : When MRF is raised against department then MRF end date is not mandatory (remove validation).   
                    //Commented after click on previous button role is not set 
                    //ddlRole.SelectedIndex = -1;
                    // Rajan Kumar : Issue 45752 : 03/01/2014: Ends 
                }
            }

            if (mrfDetailobj.DateOfRequisition != DateTime.MinValue)
            {   
                //Start
                //Comment by Rahul P on 20 Apr 2010
                //For Issue No. 
                //ucDatePicker.Text = mrfDetailobj.DateOfRequisition.ToString(DATEFORMAT);
                ucDatePicker.Text = DateTime.Now.ToString(DATEFORMAT);
                //End
            }
            else
            {
                ucDatePicker.Text = DateTime.Now.ToString(DATEFORMAT);
            }
            ucDatePicker2.Text = mrfDetailobj.ResourceOnBoard.ToString(DATEFORMAT);            
            // Rajan Kumar : Issue 45752 : 03/01/2014 : Starts                        			 
            // Desc : When MRF is raised against department then MRF end date is not mandatory (remove validation).
            if (Convert.ToDateTime(mrfDetailobj.ResourceReleased.ToString(DATEFORMAT)) != new DateTime())
            {
                ucDatePicker1.Text = mrfDetailobj.ResourceReleased.ToString(DATEFORMAT);
            }
            else
            {
                ucDatePicker1.Text = string.Empty;              
            }            
            // Rajan Kumar : Issue 45752 : 03/01/2014: Ends 
            txtNoOfResources.Text = Convert.ToString(mrfDetailobj.NoOfResourceRequired);
            if (txtNoOfResources.Text == "0")
            {
                txtNoOfResources.Text = "";
            }
            
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_PREVIOUS, "SetControlValues", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Fucntion will Get Control Values
    /// </summary>
    private void GetControlValue()
    {
        try
        {
            //Create Object for Projects for Business Layer
            Rave.HR.BusinessLayer.Projects.Projects projectsDetailBL = new Rave.HR.BusinessLayer.Projects.Projects();
            BusinessEntities.Projects projectsDetail;
            BusinessEntities.MRFDetail mRfDetail;

            //Check Session is null
            if (Session[SessionNames.MRFPREVIOUSVALUE] != null)
            {
                mRfDetail = (BusinessEntities.MRFDetail)Session[SessionNames.MRFPREVIOUSVALUE];
            }
            else
            {
                mRfDetail = new BusinessEntities.MRFDetail();
            }

            mRfDetail.MRfType = Convert.ToInt32(ddlMRFType.SelectedItem.Value);
            mRfDetail.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedItem.Value);
            mRfDetail.DepartmentName = ddlDepartment.SelectedItem.Text;
            

            if (ddlProjectName.Visible == true)
            {
                mRfDetail.ProjectId = Convert.ToInt32(ddlProjectName.SelectedItem.Value);
                mRfDetail.ProjectName = ddlProjectName.SelectedItem.Text;
                mRfDetail.Domain = GetDomainName(mRfDetail.ProjectId);
                
                //Get Project Details for projectID
                projectsDetail = projectsDetailBL.RetrieveProjectDetails(mRfDetail.ProjectId);
                mRfDetail.ClientName = projectsDetail.ClientName;
            }
            else
            {
                mRfDetail.ProjectId = 0;
                mRfDetail.ProjectName = "";
                mRfDetail.Domain = "";
            }
            

            string[] rolearr = ddlRole.SelectedItem.Text.Split(SPILITER_DASH);

            mRfDetail.RoleName = ddlRole.SelectedItem.Text;//rolearr[0].ToString();
            mRfDetail.Role = rolearr[1].ToString();

            if (ddlResourcePlanCode.Enabled == true)
            {
                mRfDetail.ResourcePlanId = Convert.ToInt32(ddlResourcePlanCode.SelectedItem.Value);
                mRfDetail.RPCode = ddlResourcePlanCode.SelectedItem.Text;
            }
            else
            {
                mRfDetail.ResourcePlanId = 0;
            }

            mRfDetail.RoleId = Convert.ToInt32(ddlRole.SelectedItem.Value);
            mRfDetail.DateOfRequisition = Convert.ToDateTime(ucDatePicker.Text);
            /*Changes Made by gaurav */
            //mRfDetail.ResourceOnBoard = Convert.ToDateTime(txtRequiredFrom.Text);
            //mRfDetail.ResourceReleased = Convert.ToDateTime(ucDatePicker1.Text);
            mRfDetail.ProjectDescription = txtProjectDesc.Text;
            if (txtNoOfResources.ReadOnly == true)
            {
                mRfDetail.NoOfResourceRequired = FillGridValueInToDataTable();
            }
            else
            {
                mRfDetail.NoOfResourceRequired = Convert.ToInt32(txtNoOfResources.Text);
                mRfDetail.ResourceOnBoard = Convert.ToDateTime(ucDatePicker2.Text);
                // Rajan Kumar : Issue 45752 : 03/01/2014 : Starts                        			 
                // Desc : When MRF is raised against department then MRF end date is not mandatory (remove validation).
                //mRfDetail.ResourceReleased = Convert.ToDateTime(ucDatePicker1.Text);
                if (!string.IsNullOrEmpty(ucDatePicker1.Text))
                {
                    mRfDetail.ResourceReleased = Convert.ToDateTime(ucDatePicker1.Text);
                }  
                // Rajan Kumar : Issue 45752 : 03/01/2014: Ends 
                             
            }

            if (!string.IsNullOrEmpty(txtClientName.Text))
            {
                mRfDetail.ClientName = txtClientName.Text;
            }
            
            if (!string.IsNullOrEmpty(DtPckrSOWStartDate.Text))
            {
                mRfDetail.SOWStartDate = Convert.ToDateTime(DtPckrSOWStartDate.Text);
            }
            
            if (!string.IsNullOrEmpty(DatePickerSOWEndDate.Text))
            {
                mRfDetail.SOWEndDate = Convert.ToDateTime(DatePickerSOWEndDate.Text);
            }
            
            if (!string.IsNullOrEmpty(txtSOWNo.Text))
            {
                mRfDetail.SOWNo = txtSOWNo.Text;
            }

            Session[SessionNames.MRFPREVIOUSVALUE] = mRfDetail;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, 
                CLASS_NAME_RAISEMRF_PREVIOUS, "GetControlValue", 
                EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }

    }

    /// <summary>
    /// Get Domain Name as per Selected Project
    /// </summary>
    /// <param name="ProjectID"></param>
    /// <returns></returns>
    private string GetDomainName(int ProjectID)
    {
        StringBuilder sb = new StringBuilder(); ;
        BusinessEntities.RaveHRCollection raveHRCollection;
        raveHRCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            raveHRCollection = mRFDetail.GetProjectDomainBL(ProjectID);

            BusinessEntities.KeyValue<string> obj = null;

            if (raveHRCollection.Count != 0)
            {

                for (int i = 0; i < raveHRCollection.Count; i++)
                {
                    obj = (BusinessEntities.KeyValue<string>)raveHRCollection.Item(i);
                    sb.Append(obj.Val);
                    sb.Append(",");
                }

                sb.Remove(sb.Length - 1, 1);
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_PREVIOUS, "GetDomainName", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }

        return sb.ToString();
    }

    /// <summary>
    /// Function will Get Table Structure
    /// </summary>
    /// <returns></returns>
    private DataTable GetTableStructure()
    {
        DataTable dt = new DataTable();
        DataColumn RPColumn = null;
        RPColumn = new DataColumn(CommonConstants.CHECKGRIDVALUE, Type.GetType(SYSTEMBOOLEAN));
        dt.Columns.Add(RPColumn);
        RPColumn = new DataColumn(CommonConstants.RESOURCEPLANDURATIONID, Type.GetType(SYSTEMINT32));
        dt.Columns.Add(RPColumn);
        RPColumn = new DataColumn(CommonConstants.RESOURCEPLANSTARTDATE, Type.GetType(SYSTEMDATETIME));
        dt.Columns.Add(RPColumn);
        RPColumn = new DataColumn(CommonConstants.RESOURCEPLANENDDATE, Type.GetType(SYSTEMDATETIME));
        dt.Columns.Add(RPColumn);
        RPColumn = new DataColumn(CommonConstants.RESOURCEPLANENDLOCATION, Type.GetType(SYSTEMSTRING));
        dt.Columns.Add(RPColumn);
        RPColumn = new DataColumn(CommonConstants.RESOURCEPLANBILLING, Type.GetType(SYSTEMINT32));
        dt.Columns.Add(RPColumn);
        RPColumn = new DataColumn(CommonConstants.RESOURCEPLANUTILIZATION, Type.GetType(SYSTEMINT32));
        dt.Columns.Add(RPColumn);
        RPColumn = new DataColumn(CommonConstants.RESOURCEBILLINGDATE, Type.GetType(SYSTEMDATETIME));
        dt.Columns.Add(RPColumn);
        return dt;
    }

    /// <summary>
    /// Function will Fill Grid Value from DataTable
    /// </summary>
    /// <returns></returns>
    private int FillGridValueInToDataTable()
    {
        DataTable GridDt = GetTableStructure();
        int counter = 0;

        for (int i = 0; i < grdresource.Rows.Count; i++)
        {
            dr = GridDt.NewRow();

            CheckBox chkRPDuId = (CheckBox)grdresource.Rows[i].Cells[0].FindControl("chk");

            if (chkRPDuId.Checked)
            {
                dr[CommonConstants.CHECKGRIDVALUE] = 1;
                counter += 1;
            }
            else
            {
                dr[CommonConstants.CHECKGRIDVALUE] = 0;
            }
            Label lblRPDurationId = (Label)grdresource.Rows[i].FindControl("lblRPDuId");
            Label lblBilling = (Label)grdresource.Rows[i].FindControl("lblBilling");
            Label lblUtilization = (Label)grdresource.Rows[i].FindControl("lblUtilization");                         

            UIControls_DatePickerControl billingDatePicker = (UIControls_DatePickerControl)grdresource.Rows[i].Cells[5].FindControl("billingDatePicker");           

            dr[CommonConstants.RESOURCEPLANDURATIONID] = lblRPDurationId.Text;
            dr[CommonConstants.RESOURCEPLANSTARTDATE] = grdresource.Rows[i].Cells[2].Text;
            dr[CommonConstants.RESOURCEPLANENDDATE] = grdresource.Rows[i].Cells[3].Text;
            dr[CommonConstants.RESOURCEPLANENDLOCATION] = grdresource.Rows[i].Cells[4].Text;
            dr[CommonConstants.RESOURCEPLANBILLING] = lblBilling.Text;
            dr[CommonConstants.RESOURCEPLANUTILIZATION] = lblUtilization.Text;

            //Modified By Rajesh; Issue Id 20516. geting the value of Billing Date

            if (billingDatePicker.Text != null && billingDatePicker.Text != "")
            {
                dr[CommonConstants.RESOURCEBILLINGDATE] = billingDatePicker.Text;
            }
            else
            {
                dr[CommonConstants.RESOURCEBILLINGDATE] = new DateTime();
            }

            GridDt.Rows.Add(dr);
        }
        Session[SessionNames.MRFGRIDFILL] = GridDt;
        return counter;
    }

    /// <summary>
    /// Function will Fill Dropdowns of all the Master Data
    /// </summary>
    private void FillDropDowns1()
    {
        try
        {
            //Declaring Master Class Object
            Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

            //Calling Business layer FillDropDown Method
            raveHRCollection = master.FillDropDownsBL(Convert.ToInt32(Common.EnumsConstants.Category.MRFType));

            //Check Collection is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSOurce to Collection
                ddlMRFType.DataSource = raveHRCollection;

                //Assign DataText Filed to DropDown
                ddlMRFType.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign DataValue Field to Dropdown
                ddlMRFType.DataValueField = CommonConstants.DDL_DataValueField;

                //Bind Dropdown
                ddlMRFType.DataBind();

                //Insert Select as a item for dropdown
                ddlMRFType.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                raveHRCollection.Clear();
            }


            //Calling Fill dropdown Business layer method to fill the dropdown
            raveHRCollection = master.FillDepartmentDropDownBL();

            //Check Collection object is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSource to dropdown
                ddlDepartment.DataSource = raveHRCollection;

                //Assign DataText Field to dropdown
                ddlDepartment.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign Data Value field to dropdown
                ddlDepartment.DataValueField = CommonConstants.DDL_DataValueField;

                //Bind Dropdown
                ddlDepartment.DataBind();

                //Insert Select as a Item for Dropdown
                ddlDepartment.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                raveHRCollection.Clear();
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_PREVIOUS, "FillDropDowns", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }
    private void FillDropDowns()
    {
        try
        {
            //Declaring Master Class Object
            Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

            //Calling Business layer FillDropDown Method
            raveHRCollection = master.FillDropDownsBL(Convert.ToInt32(Common.EnumsConstants.Category.MRFType));

            //Check Collection is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSOurce to Collection
                ddlMRFType.DataSource = raveHRCollection;

                //Assign DataText Filed to DropDown
                ddlMRFType.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign DataValue Field to Dropdown
                ddlMRFType.DataValueField = CommonConstants.DDL_DataValueField;

                //Bind Dropdown
                ddlMRFType.DataBind();

                // Venkatesh : Issue : 42905  03-Jan-14: Starts                    
                // Desc : Removed select option 
                //Insert Select as a item for dropdown
                //ddlMRFType.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
                // Venkatesh : Issue : 42905  03-Jan-14: End
                raveHRCollection.Clear();
            }

            //--Get logged in user
            Common.AuthorizationManager.AuthorizationManager objAuMan = new Common.AuthorizationManager.AuthorizationManager();
            string strCurrentUser = objAuMan.getLoggedInUserEmailId();

            BusinessEntities.MRFDetail objBEMRFDetail = new BusinessEntities.MRFDetail();
            objBEMRFDetail.EmailId = strCurrentUser;

            //--Get department list
            Rave.HR.BusinessLayer.MRF.MRFDetail objBLLMRFDetail = new Rave.HR.BusinessLayer.MRF.MRFDetail();

            ddlDepartment.Items.Clear();
            ddlDepartment.DataSource = objBLLMRFDetail.GetMRFRaiseAccesForDepartmentByEmpId(objBEMRFDetail);
            ddlDepartment.DataTextField = "DepartmentName";
            ddlDepartment.DataValueField = "DepartmentId";
            ddlDepartment.DataBind();

            if (ddlDepartment.Items.Count > 0)
            {
                ddlDepartment.Items.Insert(0, new ListItem(CommonConstants.SELECT));
            }
            else
            {
                ddlDepartment.Items.Add(new ListItem(CommonConstants.SELECT, "0"));
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_PREVIOUS, "FillDropDowns", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Function will use to fill Control as per selected MRF from Copy MRF Button
    /// </summary>
    private void FillCopyMRFdLL1()
    {
        //Declare AuthorizationManager class Object
        AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();

        try
        {
            //Get Current user Email Id
            string UserMailId = objRaveHRAuthorizationManager.getLoggedInUserEmailId();

            //Create object of ParameterCriteria calss.
            BusinessEntities.ParameterCriteria parametercopy = new BusinessEntities.ParameterCriteria();

            parametercopy.EMailID = UserMailId;
            parametercopy.RoleRPM = ROLE;

            //Call Business Layer Copy MRF Function
            raveHRCollection = mRFDetail.GetCopyMRFBL(parametercopy);

            //Check Collection object is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSource to dropdown
                //ddlPreviousMRF.DataSource = raveHRCollection;
                GroupDropDownList2.DataSource = raveHRCollection;

                //Assign DataText Field to dropdown
                //ddlPreviousMRF.DataTextField = CommonConstants.DDL_DataTextField;
                GroupDropDownList2.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign Data Value field to dropdown
                //ddlPreviousMRF.DataValueField = CommonConstants.DDL_DataValueField;
                GroupDropDownList2.DataValueField = CommonConstants.DDL_DataValueField;

                GroupDropDownList2.DataGroupField = CommonConstants.DDL_DataGroupField;


                //Bind Dropdown
                //ddlPreviousMRF.DataBind();
                GroupDropDownList2.DataBind();
                //GroupDropDownList2.SelectedIndex = 0;

                //Insert Select as a Item for Dropdown
                //ddlPreviousMRF.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT); 

                GroupDropDownList2.Items.Insert(0, new ListItem(CommonConstants.SELECT, CommonConstants.SELECT, true));
                //GroupDropDownList2.Items[0].Enabled = true;
                //GroupDropDownList2.Items[0].Selected = false;

                raveHRCollection.Clear();
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_PREVIOUS, "FillCopyMRFdLL", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Function will Fill Project Name DropDown
    /// </summary>
    private void FillProjectNameDropDown()
    {

        AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
        try
        {
            string UserMailId = objRaveHRAuthorizationManager.getLoggedInUserEmailId();

            BusinessEntities.ParameterCriteria parameterCriteria = new BusinessEntities.ParameterCriteria();

            parameterCriteria.EMailID = UserMailId;
            parameterCriteria.RoleRPM = ROLE;
            //Calling Fill dropdown Business layer method to fill the dropdown
            raveHRCollection = mRFDetail.GetProjectNameRoleWiseBL(parameterCriteria);

            Session[Common.SessionNames.MRFPROJECTDETAIL_COLLECTION] = raveHRCollection;

            //Check Collection object is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSource to dropdown
                ddlProjectName.DataSource = raveHRCollection;

                //Assign DataText Field to dropdown
                ddlProjectName.DataTextField = DbTableColumn.ProjectName;

                //Assign Data Value field to dropdown
                ddlProjectName.DataValueField = DbTableColumn.ProjectId;

                //Bind Dropdown
                ddlProjectName.DataBind();

                //Insert Select as a Item for Dropdown
                ddlProjectName.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_PREVIOUS, "FillProjectNameDropDown", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Function will Fill ResourcePlan DropDown as per Selected ProjectID
    /// </summary>
    private void FillResourcePlanDropDown(int projectId)
    {
        lblMessage.Text = "";

        try
        {
            //Calling Fill dropdown Business layer method to fill the dropdown
            raveHRCollection = mRFDetail.GetResourcePlanProjectWiseBL(projectId);

            //Check Collection object is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSource to dropdown
                ddlResourcePlanCode.DataSource = raveHRCollection;

                //Assign DataText Field to dropdown
                ddlResourcePlanCode.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign Data Value field to dropdown
                ddlResourcePlanCode.DataValueField = CommonConstants.DDL_DataValueField;

                //Bind Dropdown
                ddlResourcePlanCode.DataBind();

                //Insert Select as a Item for Dropdown
                ddlResourcePlanCode.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

            }

            if (raveHRCollection.Count == 0)
            {
                lblMessage.Text = "RP Code for this Project is pending for approval and hence MRF can't be raised.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_PREVIOUS, "FillResourcePlanDropDown", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Function will Fill Role Dropworn as per Selected Resource Plan
    /// </summary>
    private void FillRoleDropDown(int ResourcePlanCode, int ResourcePlanDurationStatus)
    {
        lblMessage.Text = "";

        try
        {
            //Calling Fill dropdown Business layer method to fill the dropdown
            raveHRCollection = mRFDetail.GetRoleResourcePlanWiseBL(ResourcePlanCode, ResourcePlanDurationStatus);

            //Check Collection object is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSource to dropdown
                ddlRole.DataSource = raveHRCollection;

                //Assign DataText Field to dropdown
                ddlRole.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign Data Value field to dropdown
                ddlRole.DataValueField = CommonConstants.DDL_DataValueField;

                //Bind Dropdown
                ddlRole.DataBind();

                //Insert Select as a Item for Dropdown
                ddlRole.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

            }

            /*This section is added by Gaurav Thakkar to fix the issue id 15085 as per discussion
              that a message will ne displayed when no roles are present for RP*/
            if (raveHRCollection.Count == 0)
            {
                lblMessage.Text = "All the roles are already allocated for selected RP.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_PREVIOUS, "FillRoleDropDown", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Function Will Fill Grid
    /// </summary>
    private void FillResourceGrid()
    {
        try
        {
            //Calling Fill dropdown Business layer method to fill the dropdown
            if (ddlResourcePlanCode.SelectedItem.Text != Common.CommonConstants.SELECT)
            {
                raveHRCollection = mRFDetail.GetResourceGridRoleWiseBL(Convert.ToInt32(ddlRole.SelectedValue), Convert.ToInt32(ddlResourcePlanCode.SelectedValue));

                //Check Collection object is null or not
                if (raveHRCollection != null)
                {
                    //Assign DataSource
                    grdresource.DataSource = raveHRCollection;

                    //Bind Dropdown
                    grdresource.DataBind();
                }
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_PREVIOUS, "FillResourceGrid", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Fucntion will Dispaly Role as Per Selected Department
    /// </summary>
    /// <param name="DepartmentId"></param>
    private void DepartmentWiseRoleDispaly(int DepartmentId)
    {
        FillRoleDepartmentWise(DepartmentId);
    }

    /// <summary>
    /// Function will the Project Description
    /// </summary>
    private void GetProjectDescription(int index)
    {
        BusinessEntities.RaveHRCollection raveHRCollection;
        raveHRCollection = new BusinessEntities.RaveHRCollection();
        try
        {
            raveHRCollection = (BusinessEntities.RaveHRCollection)Session[Common.SessionNames.MRFPROJECTDETAIL_COLLECTION];

            if (raveHRCollection != null)
            {
                BusinessEntities.MRFDetail mRFDetail = new BusinessEntities.MRFDetail();
                mRFDetail = (BusinessEntities.MRFDetail)raveHRCollection.Item(index);
                Session[Common.SessionNames.MRFPROJECTDETAIL] = mRFDetail;
                txtProjectDesc.Text = mRFDetail.ProjectDescription;
                hidRequiredFrom.Value = mRFDetail.ProjectStartDate;
                hidRequiredTill.Value = mRFDetail.ProjectEndDate;

            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_PREVIOUS, "GetProjectDescription", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Function will Validate Controls
    /// </summary>
    /// <returns></returns>
    private bool ValidateControl()
    {
        try
        {
            string[] projectStartDateArr;
            string[] projectEndDateArr;

            string[] RequestFromArr;
            string[] RequestTillArr;
            errMessage = new StringBuilder();

            flag = true;

            if (ddlProjectName.Enabled == true)
            {
                /*if (!string.IsNullOrEmpty(hidRequiredFrom.Value))
                {
                    projectStartDateArr = Convert.ToString(hidRequiredFrom.Value).Split(SPILITER_SLASH);
                    ProjectStartDate = new DateTime(Convert.ToInt32(projectStartDateArr[2]), Convert.ToInt32(projectStartDateArr[1]), Convert.ToInt32(projectStartDateArr[0]));
                }
                if (!string.IsNullOrEmpty(hidRequiredTill.Value))
                {
                    projectEndDateArr = Convert.ToString(hidRequiredTill.Value).Split(SPILITER_SLASH);
                    ProjectEndDate = new DateTime(Convert.ToInt32(projectEndDateArr[2]), Convert.ToInt32(projectEndDateArr[1]), Convert.ToInt32(projectEndDateArr[0]));
                }

                if (!string.IsNullOrEmpty(txtRequiredFrom.Text))
                {
                    RequestFromArr = txtRequiredFrom.Text.Split(SPILITER_SLASH);
                    DateTime RequestFromDate = new DateTime(Convert.ToInt32(RequestFromArr[2]), Convert.ToInt32(RequestFromArr[1]), Convert.ToInt32(RequestFromArr[0]));
                    if (RequestFromDate < ProjectStartDate || RequestFromDate > ProjectEndDate)
                    {
                        errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.MSG_MRFREQUIREDFROM + ProjectStartDate.ToString(DATEFORMAT) + CommonConstants.MSG_MRFREQUIREDFROM_Date + ProjectEndDate.ToString(DATEFORMAT));
                        flag = false;
                    }
                }
                if (!string.IsNullOrEmpty(ucDatePicker1.Text))
                {
                    RequestTillArr = ucDatePicker1.Text.Split(SPILITER_SLASH);
                    DateTime RequestTillDate = new DateTime(Convert.ToInt32(RequestTillArr[2]), Convert.ToInt32(RequestTillArr[1]), Convert.ToInt32(RequestTillArr[0]));
                    if (RequestTillDate > ProjectEndDate || RequestTillDate < ProjectStartDate)
                    {
                        errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.MSG_MRFREQUIREDTILL + ProjectStartDate.ToString(DATEFORMAT) + CommonConstants.MSG_MRFREQUIREDTILL_Date + ProjectEndDate.ToString(DATEFORMAT));
                        flag = false;
                    }
                }

                if (!string.IsNullOrEmpty(txtRequiredFrom.Text) && !string.IsNullOrEmpty(ucDatePicker1.Text))
                {
                    RequestFromArr = txtRequiredFrom.Text.Split(SPILITER_SLASH);
                    DateTime RequestFromDate = new DateTime(Convert.ToInt32(RequestFromArr[2]), Convert.ToInt32(RequestFromArr[1]), Convert.ToInt32(RequestFromArr[0]));

                    RequestTillArr = ucDatePicker1.Text.Split(SPILITER_SLASH);
                    DateTime RequestTillDate = new DateTime(Convert.ToInt32(RequestTillArr[2]), Convert.ToInt32(RequestTillArr[1]), Convert.ToInt32(RequestTillArr[0]));

                    if (RequestFromDate >= RequestTillDate)
                    {
                        errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.MSG_MRFREQUIRED_FROM_GREATEREQUAL);
                        flag = false;
                    }
                    else if (RequestTillDate <= RequestFromDate)
                    {
                        errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.MSG_MRFREQUIRED_TILL_LESSEQUAL);
                        flag = false;
                    }
                }*/
            }
            else
            {
                // Rajan Kumar : Issue 36749 : 27/02/2014 : Starts                        			 
                // Desc : When MRF is raised against department then MRF end date is not mandatory (remove validation). 
                //Comment the code because we dont required till date so no need to validate
                ////if (!string.IsNullOrEmpty(ucDatePicker2.Text) && !string.IsNullOrEmpty(ucDatePicker1.Text))
                ////{
                ////    RequestFromArr = ucDatePicker2.Text.Split(SPILITER_SLASH);
                ////    DateTime RequestFromDate = new DateTime(Convert.ToInt32(RequestFromArr[2]), Convert.ToInt32(RequestFromArr[1]), Convert.ToInt32(RequestFromArr[0]));

                ////    RequestTillArr = ucDatePicker1.Text.Split(SPILITER_SLASH);
                ////    DateTime RequestTillDate = new DateTime(Convert.ToInt32(RequestTillArr[2]), Convert.ToInt32(RequestTillArr[1]), Convert.ToInt32(RequestTillArr[0]));

                ////    if (RequestFromDate > RequestTillDate)
                ////    {
                ////        errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.MSG_MRFREQUIRED_FROM_GREATEREQUAL);
                ////        flag = false;
                ////    }
                ////    else if (RequestTillDate <= RequestFromDate)
                ////    {
                ////        errMessage.Append(CommonConstants.NEW_LINE + CommonConstants.MSG_MRFREQUIRED_TILL_LESSEQUAL);
                ////        flag = false;
                ////    }
                ////}
                // Rajan Kumar : Issue 45752 : 03/01/2013: Ends  
            }

            lblMessage.Text = errMessage.ToString();
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_PREVIOUS, "ValidateControl", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
        return flag;
    }

    /// <summary>
    /// Fucntion will Call on CheckCheckBox
    /// </summary>
    private void CheckCheckBox()
    {
        for (int i = 0; i < grdresource.Rows.Count; i++)
        {
            CheckBox chkRPDuId = (CheckBox)grdresource.Rows[i].Cells[0].FindControl("chk");

            if (chkRPDuId.Checked)
            {
                chkRPDuId.Checked = false;
                txtNoOfResources.Text = "";
            }
        }

    }

    /// <summary>
    /// Fucntion will call Fill Role as per Selected Department Wise
    /// </summary>
    /// <param name="DepartmentId"></param>
    private void FillRoleDepartmentWise(int DepartmentId)
    {
        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        try
        {
            //Calling Business layer FillDropDown Method
            raveHRCollection = mRFDetail.GetRoleDepartmentWiseBL(DepartmentId);

            //Check Collection is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSOurce to Collection
                ddlRole.DataSource = raveHRCollection;

                //Assign DataText Filed to DropDown
                ddlRole.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign DataValue Field to Dropdown
                ddlRole.DataValueField = CommonConstants.DDL_DataValueField;

                //Bind Dropdown
                ddlRole.DataBind();

                //Insert Select as a item for dropdown
                ddlRole.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                raveHRCollection.Clear();
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_PREVIOUS, "FillRoleDepartmentWise", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Function will Call to Reset Controls
    /// </summary>
    private void ResetColtrol()
    {
        ddlMRFType.SelectedIndex = -1;
        ddlDepartment.SelectedIndex = -1;
        ddlProjectName.SelectedIndex = -1;
        ddlRole.SelectedIndex = -1;
        ucDatePicker2.Text = string.Empty;
        ucDatePicker1.Text = string.Empty;
        txtProjectDesc.Text = string.Empty;
        txtNoOfResources.Text = string.Empty;

    }

    /// <summary>
    /// Function will use to fill Control as per selected MRF from Copy MRF Button
    /// </summary>
    private void FillCopyMRFdLL()
    {
        //Declare AuthorizationManager class Object
        AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();

        try
        {
            //Get Current user Email Id
            string UserMailId = objRaveHRAuthorizationManager.getLoggedInUserEmailId();

            //Create object of ParameterCriteria calss.
            BusinessEntities.ParameterCriteria parametercopy = new BusinessEntities.ParameterCriteria();

            parametercopy.EMailID = UserMailId;

            //Call Business Layer Copy MRF Function
            raveHRCollection = mRFDetail.GetCopyMRFList(parametercopy);

            //Check Collection object is null or not
            if (raveHRCollection != null)
            {
                GroupDropDownList2.DataSource = raveHRCollection;
                GroupDropDownList2.DataTextField = CommonConstants.DDL_DataTextField;
                GroupDropDownList2.DataValueField = CommonConstants.DDL_DataValueField;
                GroupDropDownList2.DataGroupField = CommonConstants.DDL_DataGroupField;
                GroupDropDownList2.DataBind();
                GroupDropDownList2.Items.Insert(0, new ListItem(CommonConstants.SELECT, CommonConstants.SELECT, true));
                raveHRCollection.Clear();
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_PREVIOUS, "FillCopyMRFdLL", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Populates Control for Move MRF
    /// </summary>
    private void PopulateControlForMoveMRF( int MrfID)
    {
        try
        {
            //HideControlForMoveMRF(false);            
            //mRFDetail = new Rave.HR.BusinessLayer.MRF.MRFDetail();
            //BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
            //raveHRCollection = mRFDetail.GetMrfDetail(MrfID);
            //Session[SessionNames.MoveMRFDetails] = raveHRCollection;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_PREVIOUS, "PopulateControlForMoveMRF", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Hides Control for Move MRF
    /// </summary>
    private void HideControlForMoveMRF(bool flag)
    {
        lblPrevMrf.Visible = flag;
        GroupDropDownList2.Visible = flag;
        btnCopy.Visible = flag;
    }




    private void FillDepartmentProjectNamesDropDown(string ProjectStaus)
    {
        //Declare AuthorizationManager class Object
        AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
        
        try
        {

            //Get Current user Email Id
            string UserMailId = objRaveHRAuthorizationManager.getLoggedInUserEmailId();

            //Create object of ParameterCriteria calss.
            BusinessEntities.ParameterCriteria parametercopy = new BusinessEntities.ParameterCriteria();

            parametercopy.EMailID = UserMailId;
            parametercopy.RoleRPM = ROLE;
            parametercopy.ProjectStatus = ProjectStaus;////25988-Ambar


            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
            Rave.HR.BusinessLayer.MRF.MRFDetail DeptDetails = new Rave.HR.BusinessLayer.MRF.MRFDetail();

           
            //Calling Business layer FillDropDown Method
            raveHRCollection = DeptDetails.FillDepartmentProjectNamesDropDownBL(parametercopy);               

            //Check Collection is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSOurce to Collection
                ddlMRFDeptCopy.DataSource = raveHRCollection;

                //Assign DataText Filed to DropDown
                ddlMRFDeptCopy.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign DataValue Field to Dropdown
                ddlMRFDeptCopy.DataValueField = CommonConstants.DDL_DataValueField;

                //Bind Dropdown
                ddlMRFDeptCopy.DataBind();

                //Insert Select as a item for dropdown
                ddlMRFDeptCopy.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                raveHRCollection.Clear();
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_PREVIOUS, "FillDepartmentProjectNamesDropDown", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }


   

    private void FillMRFRoleDropDown(string DeptProjectName)
    {
        try
        {
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
            Rave.HR.BusinessLayer.MRF.MRFDetail DeptMRF = new Rave.HR.BusinessLayer.MRF.MRFDetail();
            //Calling Business layer FillDropDown Method
            raveHRCollection = DeptMRF.FillMRFRoleBL(DeptProjectName);



            //Calling Business layer FillDropDown Method
            //raveHRCollection = Rave.HR.BusinessLayer.Recruitment.Recruitment.GetMrfCode();

            //Check Collection is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSOurce to Collection
                ddlMRFFilterRoleCopy.DataSource = raveHRCollection;

                //Assign DataText Filed to DropDown
                ddlMRFFilterRoleCopy.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign DataValue Field to Dropdown
                ddlMRFFilterRoleCopy.DataValueField = CommonConstants.DDL_DataValueField;

                //Bind Dropdown
                ddlMRFFilterRoleCopy.DataBind();

                //Insert Select as a item for dropdown
                ddlMRFFilterRoleCopy.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

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
                CLASS_NAME_RAISEMRF_PREVIOUS, "FillMRFRoleDropDowns",
                EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }


    private void FillMRFCodeDropDowns(string DeptProjectName, int DeptProjectMRFRoleID, string ProjectStatus)
    {
        //Declare AuthorizationManager class Object
        AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();

        try
        {
            //Get Current user Email Id
            string UserMailId = objRaveHRAuthorizationManager.getLoggedInUserEmailId();

            //Create object of ParameterCriteria calss.
            BusinessEntities.ParameterCriteria parametercopy = new BusinessEntities.ParameterCriteria();

            parametercopy.EMailID = UserMailId;
            parametercopy.RoleRPM = ROLE;
            parametercopy.ProjectStatus = ProjectStatus; ////25988-Ambar

            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
            Rave.HR.BusinessLayer.MRF.MRFDetail DeptMRF = new Rave.HR.BusinessLayer.MRF.MRFDetail();
            //Calling Business layer FillDropDown Method
            raveHRCollection = DeptMRF.FillMRFCodeBL(DeptProjectName, DeptProjectMRFRoleID, parametercopy);



            //Calling Business layer FillDropDown Method
            //raveHRCollection = Rave.HR.BusinessLayer.Recruitment.Recruitment.GetMrfCode();

            //Check Collection is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSOurce to Collection
                ddlMRFFilterCopy.DataSource = raveHRCollection;

                //Assign DataText Filed to DropDown
                ddlMRFFilterCopy.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign DataValue Field to Dropdown
                ddlMRFFilterCopy.DataValueField = CommonConstants.DDL_DataValueField;

                //Bind Dropdown
                ddlMRFFilterCopy.DataBind();

                //Insert Select as a item for dropdown
                ddlMRFFilterCopy.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

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
                CLASS_NAME_RAISEMRF_PREVIOUS, "FillMRFCodeDropDowns",
                EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    protected void ddlMRFDeptCopy_SelectedIndexChanged(object sender, EventArgs e)
    {

        LblMRFRole.Visible = true;
        ddlMRFFilterRoleCopy.Visible = true;
        FillMRFRoleDropDown(ddlMRFDeptCopy.SelectedItem.Text.ToString());


        string String_DeptProjectNamev = ddlMRFDeptCopy.SelectedItem.Text.ToString();
        int int_DeptProjectMRFRoleIDv = -1;

        //25988-Ambar-Start
        string strnull = null;
        //25988-Ambar-Start

        FillMRFCodeDropDowns(String_DeptProjectNamev, int_DeptProjectMRFRoleIDv, strnull);

       
    }


    protected void ddlMRFFilterRoleCopy_SelectedIndexChanged(object sender, EventArgs e)
    {
     

       // ddlMRFFilterCopy.Visible = true;
       // lblPrevMrf.Visible = true;
        string String_DeptProjectNamev = ddlMRFDeptCopy.SelectedItem.Text.ToString();
        int int_DeptProjectMRFRoleIDv = Convert.ToInt32(ddlMRFFilterRoleCopy.SelectedValue);

        if (ddlMRFFilterRoleCopy.SelectedItem.Text != CommonConstants.SELECT)
        {
            int_DeptProjectMRFRoleIDv = Convert.ToInt32(ddlMRFFilterRoleCopy.SelectedValue);
        }
        else
        {
            int_DeptProjectMRFRoleIDv = -1;
        }

        if (ddlMRFDeptCopy.SelectedItem.Text != CommonConstants.SELECT)
        {
            String_DeptProjectNamev = ddlMRFDeptCopy.SelectedItem.Text.ToString();
        }
        else
        {
            String_DeptProjectNamev = CommonConstants.SELECT;
        }

        //25988-Ambar-Start
        string strnull = null;
        //25988-Ambar-Start

        FillMRFCodeDropDowns(String_DeptProjectNamev, int_DeptProjectMRFRoleIDv, strnull);
    }

    protected void txtSOWNo_TextChanged(object sender, EventArgs e)
        {

        if (txtSOWNo.Text.Length != 0)
        {
            SOWStartDatemandatorymark.Visible = true;
            SOWEndDatemandatorymark.Visible = true;
        }
        else
        {
            SOWStartDatemandatorymark.Visible = false;
            SOWEndDatemandatorymark.Visible = false;
        }

    }

    ////25988-Ambar-Start

    private void FillDPStatusDropDown()
    {
      //Declare AuthorizationManager class Object
      AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();

      try
      {
        // Initialise Business layer object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
        //Rave.HR.BusinessLayer.MRF.MRFDetail DeptDetails = new Rave.HR.BusinessLayer.MRF.MRFDetail();

        //Calling Business layer FillDropDown Method
        raveHRCollection = master.FillDropDownsBL(Convert.ToInt32(EnumsConstants.Category.ProjectStatus));
        
        //Check Collection is null or not
        if (raveHRCollection != null)
        {
          //Assign DataSOurce to Collection
          ddldpstatus.DataSource = raveHRCollection;

          //Assign DataText Filed to DropDown
          ddldpstatus.DataTextField = CommonConstants.DDL_DataTextField;

          //Assign DataValue Field to Dropdown
          ddldpstatus.DataValueField = CommonConstants.DDL_DataValueField;

          //Bind Dropdown
          ddldpstatus.DataBind();

          //Insert Select as a item for dropdown
          ddldpstatus.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

          raveHRCollection.Clear();
        }
      }
      catch (RaveHRException ex)
      {
        throw ex;
      }
      catch (Exception ex)
      {
        throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_PREVIOUS, "FillDPStatusDropDown", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
      }
    }

    protected void ddlDPStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
      LblMRFRole.Visible = false;
      ddlMRFFilterRoleCopy.Visible = false;

      string String_projectstatus = ddldpstatus.SelectedItem.Text.ToString();

      string String_Projectstatus = null;

      if (ddldpstatus.SelectedItem.Text.ToString() != "SELECT")
      {
        String_Projectstatus = ddldpstatus.SelectedItem.Text.ToString();
      }
      else
      {
        String_Projectstatus = null;
      }

      string String_DeptProjectNamev = null;
      int int_DeptProjectMRFRoleIDv = -1;

      FillMRFCodeDropDowns(String_DeptProjectNamev, int_DeptProjectMRFRoleIDv, String_Projectstatus);
      FillDepartmentProjectNamesDropDown(String_Projectstatus);
    }

    ////25988-Ambar-End



    // 28344-ambar-start
    public void ddlrole_selectedindexchanged()
    {
      try
      {
        if (ddlProjectName.Enabled == true)
        {
          if (ddlRole.SelectedItem.Text != CommonConstants.SELECT)
          {
            grdresource.Visible = true;
            FillResourceGrid();
          }
          else
          {
            grdresource.Visible = false;
          }
          txtNoOfResources.ReadOnly = true;
          txtNoOfResources.Text = "";
        }
      }
      catch (RaveHRException ex)
      {
        LogErrorMessage(ex);
      }
      catch (Exception ex)
      {
        RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RAISEMRF_PREVIOUS, "ddlRole_SelectedIndexChanged", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        LogErrorMessage(objEx);
      }
    }
    // 28344-ambar-End
    // Rajan Kumar : Issue 36749 : 07/01/2014 : Starts                        			 
    // Desc : Required Resource checkbox is unchecked after coming back using Previous button
    public string SelectedCheckBoxCount()
    {
        int counter=0;
        for (int i = 0; i < grdresource.Rows.Count; i++)
        {
            CheckBox chkRPDuId = (CheckBox)grdresource.Rows[i].Cells[0].FindControl("chk");

            if (chkRPDuId.Checked)
            {               
                counter += 1;
            }           
        }
        if (counter == 0)
        {
            return string.Empty;
        }
        return counter.ToString(); ;   
    }
    // Rajan Kumar : Issue 36749 : 07/01/2014 : Ends 

    #endregion

    
}
