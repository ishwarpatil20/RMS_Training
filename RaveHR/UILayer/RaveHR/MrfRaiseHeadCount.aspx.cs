//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           MrfSummary.aspx.cs    
//  Author:         Gaurav.Thakkar
//  Date written:   24/8/2009/ 12:26:00 PM
//  Description:    This page Raises Head Count
//                  
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  24/8/2009 12:26:00 PM  Gaurav.Thakkar    n/a     Created    
//
//------------------------------------------------------------------------------
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
using System.Text;
using System.Collections.Generic;
using Common.AuthorizationManager;
using Common.Constants;
using BusinessEntities;
using Rave.HR.BusinessLayer;
public partial class MrfRaiseHeadCount : BaseClass
{

    #region Private Members

    /// <summary>
    /// Gets The MRF Id
    /// </summary>
    private string MrfId = string.Empty;

    /// <summary>
    /// Gets The Project Name
    /// </summary>
    private string ProjectName = string.Empty;

    /// <summary>
    ///  Gets The Role
    /// </summary>
    private string Role = string.Empty;

    /// <summary>
    ///  Gets The Experience
    /// </summary>
    private string Experience = string.Empty;

    /// <summary>
    ///  Gets The Target CTC
    /// </summary>
    private string TargetCTC = string.Empty;

    /// <summary>
    /// Gets The Department
    /// </summary>
    private string Department = string.Empty;

    /// <summary>
    /// Gets The Department
    /// </summary>
    private string MrfCode = string.Empty;

    /// <summary>
    /// Defines the class name.
    /// </summary>
    private const string CLASS_NAME_MRFRAISEHEADCOUNT = "MrfRaiseHeadCount.aspx";

    /// <summary>
    /// Subject for mail
    /// </summary>
    string subject;

    /// <summary>
    /// Body for mail
    /// </summary>
    string body;

    /// <summary>
    /// Rave Domain
    /// </summary>
    /// 
    //Googleconfigurable
    //const string RAVEDOMAIN = "@rave-tech.com";

    /// <summary>
    /// Authorisation manager 
    /// </summary>
    AuthorizationManager objAuMan = new AuthorizationManager();

    /// <summary>
    /// Name to be appended in mail at "Regards".
    /// </summary>
    private string RegardsName;


    public bool IsCostCodeValidation { get; set; } 

    /// <summary>
    /// Business lyer initialisation.
    /// </summary>
    Rave.HR.BusinessLayer.MRF.MRFDetail mrfUser = new Rave.HR.BusinessLayer.MRF.MRFDetail();

    /// <summary>
    /// Gets The Target Closure Date To Recruiter
    /// </summary>
    private string TargetClosureDateToRecruiter = string.Empty;

    /// <summary>
    /// Gets The SLA Days To Recruiter
    /// </summary>
    private string SLADays = string.Empty;

    /// <summary>
    /// Gets The Default SLA Days TO Recruiter
    /// </summary>
    private int _defaultSLADays = 60;

    /// <summary>
    /// Gets The ProjectId
    /// </summary>
    private string ProjectId = string.Empty;
    /// <summary>
    /// Declaring COllection class object
    /// </summary>    
    BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

    #endregion Private Members

    #region Protected Method

    /// <summary>
    /// Page Load Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Javascript Function Call
        //Umesh: Issue 'Modal Popup issue in chrome' Starts
        //imgPurpose.Attributes.Add("onclick", "return popUpEmployeeName();");
        //Umesh: Issue 'Modal Popup issue in chrome' Ends
        #endregion 

        lblPurpose.Text = string.Empty;
        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.AppendHeader("Pragma", "no-cache");
        
        if (!IsPostBack)
        {
            this.GetPurposeDetails();
        }
        if (!ValidateURL())
        {
            Response.Redirect(CommonConstants.INVALIDURL);
        }
        else
        {
            try
            {                
                btnOK.Attributes.Add("onclick", "return ButtonClickValidate();");

                //Poonam : Issue : Disable Button : Starts
                btnOK.OnClientClick = "if(ButtonClickValidate()){" + ClientScript.GetPostBackEventReference(btnOK, null) + "}";
                //Poonam : Issue : Disable Button : Ends

                ddlRecruitmentManager.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzddlRecruitmentManager.ClientID + "','','');");

                ddlCostCode.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzddlCostCode.ClientID + "','','');");


                //ddlPurpose.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateProject();");
                //ddlPurpose.Attributes.Add(CommonConstants.EVENT_ONBLUR, "javascript:ValidateProject();");
                //imgTargetDate.Attributes.Add(CommonConstants.EVENT_ONMOUSEOVER, "javascript:ValidateControl('" + txtTargetDate.ClientID + "','','');");
                //Extract the parameters from Query String
                MrfId = DecryptQueryString(QueryStringConstants.MRFID).ToString();
                ProjectName = DecryptQueryString(QueryStringConstants.PROJECTNAME).ToString();
                Role = DecryptQueryString(QueryStringConstants.ROLE).ToString();
                Experience = DecryptQueryString(QueryStringConstants.EXP).ToString();
                TargetCTC = DecryptQueryString(QueryStringConstants.TARGETCTC).ToString();
                Department = DecryptQueryString(QueryStringConstants.DEPT).ToString();
                MrfCode = DecryptQueryString(QueryStringConstants.MRFCODE).ToString();

                //get SLA Days for recruiter
                SLADays = DecryptQueryString(QueryStringConstants.SLADAYS).ToString();
                ProjectId = DecryptQueryString(QueryStringConstants.PROJECTID).ToString();

                hidDepartment.Value = Department;
                hidMrfCode.Value = MrfCode;


                if (!IsPostBack)
                {
                    if (ProjectId != CommonConstants.SELECT)
                    {
                        //Rakesh : Actual vs Budget 06/06/2016 Begin
                        NPS_Validation objNPS_Validation = Rave.HR.BusinessLayer.MRF.MRFDetail.Is_NIS_NorthgateProject(Convert.ToInt32(ProjectId));

                        if (objNPS_Validation.IsNPS_Project)
                        {
                            trCostCode.Visible = true;
                            Rave.HR.BusinessLayer.Common.Master objMaster = new Rave.HR.BusinessLayer.Common.Master();
                            raveHRCollection = objMaster.FillDropDownsBL(Common.EnumsConstants.Category.CostCode.CastToInt32());
                            ddlCostCode.BindDropdown(raveHRCollection);

                            if (objNPS_Validation.IsDisableValidation)
                            {
                                IsCostCodeValidation= false;
                                lblCostCodeValidation.Visible = false;                               
                            }
                            else
                            {
                                IsCostCodeValidation= true;
                            }
                        }
                        else
                        {
                            trCostCode.Visible = false;
                        }
                    }
                    else
                    {
                        trCostCode.Visible = false;
                    }
                    // End


                    //Fill Recritment manager dopdown
                    FillRecruitmentManagerDropDown();

                    //If ProjectName is "SELECT" than disable the ProjectName textbox
                    if (ProjectName != CommonConstants.SELECT)
                    {
                        txtProjectName.Text = ProjectName;
                    }
                    else
                    {
                        txtProjectName.Enabled = false;
                    }

                    //Assign the values from querystring to respective textboxes
                    txtRole.Text = Role;
                    txtExperience.Text = Experience;
                    txtTargetCTC.Text = TargetCTC;
                    
                   
                    #region Coded by Sameer For SLA Days

                    //checks if SLA days is null
                    //It is calculating here but in database we are saving calulated date from sp.
                    if (!string.IsNullOrEmpty(SLADays))
                        txtTargetDate.Text = DateTime.Today.AddDays(double.Parse(SLADays)).ToString(CommonConstants.DATE_FORMAT);
                    else
                        //Default SLA Days
                        txtTargetDate.Text = DateTime.Today.AddDays(_defaultSLADays).ToString(CommonConstants.DATE_FORMAT);

                    #endregion Coded by Sameer For SLA Days

                    // 57877-Venkatesh-  29042016 : Start 
                    // Add sai email if while raising headcount for nis projects
                    HfldProjectId.Value= ProjectId;
                    // 57877-Venkatesh-  29042016 : End
                    hidMrfId.Value = MrfId;
                }
                //If Session is not null Means multiple values selected from pending allocation page.
                //If multiple mrf are not selected from pending allocation page 
                //then experience should not be visible.

                if (Session[SessionNames.MRFRaiseHeadCOuntGroup] != null )
                {
                    lblExperience.Visible = false;
                    txtExperience.Visible = false;
                }
                else
                {
                    lblExperience.Visible = true;
                    txtExperience.Visible = true;
                }
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                LogErrorMessage(ex);
            }
            catch (Exception ex)
            {
                RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFRAISEHEADCOUNT, "Page_Load", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
                LogErrorMessage(objEx);
            }
        }
    }

    
    //Rakesh : 18/05/2016 Begin
    void BindDepartmentDropdown()
    {
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        try
        {  // Initialise Collection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            // Initialise Business layer object
            

            // Call the Business layer method
            raveHRCollection = master.FillDepartmentDropDownBL();

            if (raveHRCollection != null)
            {
                // Assign the data source to dropdown
                ddlDepartment.DataSource = raveHRCollection;

                ddlDepartment.DataTextField = CommonConstants.DDL_DataTextField;
                ddlDepartment.DataValueField = CommonConstants.DDL_DataValueField;

                // Bind the data to dropdown
                ddlDepartment.DataBind();

                // Default value of dropdown is "Select"
                ddlDepartment.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
             
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFRAISEHEADCOUNT, "FillDropDown", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    //End


    /// <summary>
    /// Handles the SelectedIndexChanged event of the ddlPurpose control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void ddlPurpose_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtPurpose.Text = string.Empty;
        if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.HiringForNewRole))
        {
            ddlDepartment.Visible = false;
            txtPurpose.Visible = true;
//            lblPurpose.Text = "New Role";  // RB  New Role Is Used in Place of New Position
            lblPurpose.Text = "Position Description";
            imgPurpose.Visible = false;
            txtPurpose.ReadOnly = false;
            lblmandatorymarkPurpose.Visible = true;            
        }
        else if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.HiringForProject))
        {
            ddlDepartment.Visible = false;
            txtPurpose.Visible = true;
            lblPurpose.Text = "Project Name";
            imgPurpose.Visible = false;
            txtPurpose.ReadOnly = false;
            lblmandatorymarkPurpose.Visible = true;
            //Venkatesh : Issue 35089 : 24/12/2013 : Start
            //Desc : Remove validation for Project, except Raveforecasted
            txtPurpose.Text = txtProjectName.Text;
            //Venkatesh : Issue 35089 : 24/12/2013 : End
        }
        else if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.Replacement))
        {
            ddlDepartment.Visible = false;
            txtPurpose.Visible = true;
            txtPurpose.ReadOnly = true;
            lblPurpose.Text = "Employee Name";
            imgPurpose.Visible = true;
            lblmandatorymarkPurpose.Visible = true;
        }
        // Rakesh B 57942 10-5-2016 Start 
        else if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.SubstituteForMaternityLeave))
        {
            ddlDepartment.Visible = false;
            txtPurpose.Visible = true;
            txtPurpose.ReadOnly = true;
            lblPurpose.Text = "Employee Name";
            imgPurpose.Visible = true;
            lblmandatorymarkPurpose.Visible = true;
        }
        else if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.Others))
        {
            txtPurpose.Visible = true;
            lblPurpose.Text = "Description";
            imgPurpose.Visible = false;
            txtPurpose.ReadOnly = false;
            lblmandatorymarkPurpose.Visible = true;
            //Venkatesh : Issue 35089 : 24/12/2013 : Start
            //Desc : Remove validation for Project, except Raveforecasted
          //  txtPurpose.Text = txtProjectName.Text;
            //Venkatesh : Issue 35089 : 24/12/2013 : End
        }
        //End
        else if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.MarketResearchfeasibility))
        {
            txtPurpose.Visible = true;
            lblPurpose.Text = "Comment";
            imgPurpose.Visible = false;
            txtPurpose.ReadOnly = false;
            lblmandatorymarkPurpose.Visible = false;
            ddlDepartment.Visible = false;
        }
        else if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.HiringForFutureBusiness))
        {
            txtPurpose.Visible = true;
            lblPurpose.Text = "Comment";
            imgPurpose.Visible = false;
            txtPurpose.ReadOnly = false;
            lblmandatorymarkPurpose.Visible = false;
            ddlDepartment.Visible = false;
        }
        else if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.HiringForInternalProject))
        {
            txtPurpose.Visible = true;
            lblPurpose.Text = "Comment";
            imgPurpose.Visible = false;
            txtPurpose.ReadOnly = false;
            lblmandatorymarkPurpose.Visible = true;
            ddlDepartment.Visible = false;
        }
        else if (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.HiringforDepartment))
        {
           // txtPurpose.Visible = true;
            lblPurpose.Text = "Department";
            imgPurpose.Visible = false;
            txtPurpose.ReadOnly = false;
            ddlDepartment.Visible = true;
            txtPurpose.Visible = false;
            BindDepartmentDropdown();
            lblmandatorymarkPurpose.Visible = true;
        }
        else
        {
            lblPurpose.Text = string.Empty;
            txtPurpose.Visible = false;
            imgPurpose.Visible = false;
            txtPurpose.ReadOnly = false;
            ddlDepartment.Visible = false;
            lblmandatorymarkPurpose.Visible = false;
        }
    }

    /// <summary>
    /// Fill DropDown Method
    /// </summary>
    private void GetPurposeDetails()
    {
        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        try
        {
            //Calling Business layer FillDropDown Method
            raveHRCollection = master.FillDropDownsBL(Convert.ToInt32(Common.EnumsConstants.Category.MRFPurpose));

            //Check Collection is null or not
            if (raveHRCollection != null)
            {
                //Assign DataSOurce to Collection
                ddlPurpose.DataSource = raveHRCollection;

                //Assign DataText Filed to DropDown
                ddlPurpose.DataTextField = CommonConstants.DDL_DataTextField;

                //Assign DataValue Field to Dropdown
                ddlPurpose.DataValueField = CommonConstants.DDL_DataValueField;

                //Bind Dropdown
                ddlPurpose.DataBind();

                //Insert Select as a item for dropdown
                ddlPurpose.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                raveHRCollection.Clear();
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFRAISEHEADCOUNT, "FillDropDown", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }

    }
        
    /// <summary>
    /// Raise the Head count for MRF
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            string mrfIdGroup = string.Empty;
            //Get the Recruitment Manager Id
            string recruitmentManagerId = ddlRecruitmentManager.SelectedValue;
            //Aarohi : Issue 30885 : 14/12/2011 : Start
            if ((Session[SessionNames.PURPOSE] != null) && (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.Replacement)) || (ddlPurpose.SelectedValue == Convert.ToString((int)Common.MasterEnum.MRFPurpose.SubstituteForMaternityLeave)))
            {
                //Issue Id : 34331 START : Mahendra
                //commented following and added new code to map Arraylist elements
                //txtPurpose.Text = Session[SessionNames.PURPOSE].ToString();
                System.Collections.ArrayList name = (System.Collections.ArrayList)Session[SessionNames.PURPOSE];
                foreach (var item in name)
                {
                    txtPurpose.Text = item.ToString();
                }
                //Issue Id : 34331 END
            }
            //Aarohi : Issue 30885 : 14/12/2011 : End

            //On Click, Change the status of respetive MRF
            if (recruitmentManagerId != CommonConstants.SELECT)
            {
                BusinessEntities.MRFDetail mrfDetail = new BusinessEntities.MRFDetail();
                Rave.HR.BusinessLayer.MRF.MRFDetail objMrf = new Rave.HR.BusinessLayer.MRF.MRFDetail();

                //Check if wheather any need of approval of head count.means approver id for this deprtment is 0.
                string mailTo = GetEmailIdForDeptWiseHeadCountApprovalMailTo(hidDepartment.Value);

                //if no approver id found then directly goes for pending external allocation.
                //For Rave consultant department no approval is required.mail is directly sent to Recruiment.
                if (string.IsNullOrEmpty(mailTo))
                {
                    mrfDetail.Status = CommonConstants.MRFStatus_PendingExternalAllocation;
                }
                else
                {
                    //Sent For Finance to approve the head count.
                    mrfDetail.Status = CommonConstants.MRFStatus_PendingHeadCountApprovalOfFinance;

                    //Goes for head count approval & make status Peding Approval Of HeadCount.
                    // mrfDetail.Status = CommonConstants.MRFStatus_PedingApprovalOfHeadCount;
                }
                mrfDetail.EmployeeId = int.Parse(recruitmentManagerId);
                mrfDetail.ExpectedClosureDate = txtTargetDate.Text;
                mrfDetail.RecruitmentManager = ddlRecruitmentManager.SelectedItem.Text;
                mrfDetail.MRFCode = hidMrfCode.Value;
                mrfDetail.ProjectName = txtProjectName.Text;
                mrfDetail.DepartmentName = hidDepartment.Value;
                mrfDetail.Role = txtRole.Text;
                mrfDetail.ExpectedClosureDate = txtTargetDate.Text;
                mrfDetail.RecruitersId = ddlRecruitmentManager.SelectedValue;
                // 57877-Venkatesh-  29042016 : Start 
                // Add sai email if while raising headcount for nis projects
                if (HfldProjectId.Value !="SELECT")
                    mrfDetail.ProjectId = Convert.ToInt32 (HfldProjectId.Value);
                // 57877-Venkatesh-  29042016 : End
                //Adding Purpose and PurposeDescription fileds

                if (ddlPurpose.SelectedItem.Text != Common.CommonConstants.SELECT)
                    mrfDetail.MRFPurposeId = Convert.ToInt32(ddlPurpose.SelectedValue);
                else
                    mrfDetail.MRFPurposeId = CommonConstants.ZERO;

                if (mrfDetail.MRFPurposeId == Convert.ToInt32(MasterEnum.MRFPurpose.MarketResearchfeasibility))
                {
                    mrfDetail.Status = CommonConstants.MRFStatus_PendingExternalAllocation;
                }

                if (txtPurpose.Visible == true && !string.IsNullOrEmpty(txtPurpose.Text))
                    mrfDetail.MRFPurposeDescription = txtPurpose.Text.Trim();
                if (ddlDepartment.Visible == true && ddlDepartment.SelectedIndex >0)
                    mrfDetail.MRFPurposeDescription = ddlDepartment.SelectedItem.Text;

                // Rakesh -  Issue : 57942  to Set Dropdown Value to Object
                if(ddlPurpose.SelectedItem!=null)
                mrfDetail.MRFPurpose  = ddlPurpose.SelectedItem.Text ;
                //end

                if (Session[SessionNames.MRFRaiseHeadCOuntGroup] != null)
                {
                    mrfIdGroup = Session[SessionNames.MRFRaiseHeadCOuntGroup].ToString();
                }
                string[] newMrfId = mrfIdGroup.Split(',');

                // Rajan Kumar : Issue 46252: 10/02/2014 : Starts                        			 
                // Desc : In MRF history need to implemented in all cases in RMS.
                //Pass Email to know who is going to modified the data            
                AuthorizationManager authoriseduser = new AuthorizationManager();
                mrfDetail.LoggedInUserEmail = authoriseduser.getLoggedInUserEmailId();
                // Rajan Kumar : Issue 46252: 10/02/2014 : END



                //Rakesh : Actual vs Budget 06/06/2016 Begin
                if (!string.IsNullOrEmpty(ddlCostCode.SelectedValue) && ddlCostCode.SelectedValue !=CommonConstants.SELECT)
                mrfDetail.CostCodeId = ddlCostCode.SelectedValue.CastToInt32();
                //End


                //If Session is not null Means multiple values selected from pending allocation page.
                if (Session[SessionNames.MRFRaiseHeadCOuntGroup] != null)
                {

                    for (int i = 0; i < newMrfId.Length; i++)
                    {
                        bool isMailInGroup = false;
                        if (newMrfId.Length == 1)
                        {
                            isMailInGroup = true;
                        }

                        mrfDetail.MRFId = Convert.ToInt32(newMrfId[i]);

                        BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();
                        raveHRCollection = objMrf.CopyMRFBL(Convert.ToInt32(newMrfId[i]));
                        foreach (BusinessEntities.MRFDetail currentMrfDetails in raveHRCollection)
                        {
                            mrfDetail.MRFCode = currentMrfDetails.MRFCode;
                        }

                        
                        //update MRF Status
                        //Last parameter set to mail should not send for group. it would be in shot.
                        //for that code is written below.
                        objMrf.UpdateMrfStatus(mrfDetail, mailTo, newMrfId, isMailInGroup);
                    } 

                    //For rave consultant send mail in one shot for all selected MRF.
                    if (string.IsNullOrEmpty(mailTo) && newMrfId.Length > 1)
                    {
                        objMrf.SendMailForRaiseHeadCountWithouApprovalInGroup(mrfDetail, newMrfId);
                    }

                }
                else
                {
                    mrfDetail.MRFId = int.Parse(hidMrfId.Value);
                    objMrf.UpdateMrfStatus(mrfDetail, mailTo, newMrfId, true);
                }

                Session[SessionNames.CONFIRMATION_MESSAGE] = "Request for head count is raised successfully, email notification is sent for approval.";
                Session[SessionNames.RAISE_HEAD_COUNT] = "True";

                if (Session[SessionNames.MRFRaiseHeadCOuntGroup] != null)
                {
                    Session.Remove(SessionNames.MRFRaiseHeadCOuntGroup);
                    //Close the window                
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "close", " window.opener.location.href='MrfPendingAllocation.aspx'; window.close();", true);
                    //Umesh: Issue 'Modal Popup issue in chrome' Starts
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "CloseJS", "jQuery.modalDialog.getCurrent().close();", true);
                    //Umesh: Issue 'Modal Popup issue in chrome' Ends
                }
                else
                {
                    //Umesh: Issue 'Modal Popup issue in chrome' Starts
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "CloseJS", "jQuery.modalDialog.getCurrent().close();", true);
                    //Umesh: Issue 'Modal Popup issue in chrome' Ends
                }
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFRAISEHEADCOUNT, "btnOK_Click", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Close the pop-up window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //Umesh: Issue 'Modal Popup issue in chrome' Starts
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "CloseJS", "jQuery.modalDialog.getCurrent().close();", true);
            //Umesh: Issue 'Modal Popup issue in chrome' Ends
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFRAISEHEADCOUNT, "btnCancel_Click", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    #endregion Protected Method

    #region Private Methods

    /// <summary>
    /// Fill Recruitment Manager dropdown
    /// </summary>
    private void FillRecruitmentManagerDropDown()
    {
        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        // Initialise Business layer object
        Rave.HR.BusinessLayer.MRF.MRFDetail employeeNameBL = new Rave.HR.BusinessLayer.MRF.MRFDetail();

        try
        {
            raveHRCollection = employeeNameBL.GetRecruitmentManager();

            if (raveHRCollection != null)
            {
                // Assign the data source to dropdown
                ddlRecruitmentManager.DataSource = raveHRCollection;

                ddlRecruitmentManager.DataTextField = CommonConstants.DDL_DataTextField;
                ddlRecruitmentManager.DataValueField = CommonConstants.DDL_DataValueField;

                // Bind the data to dropdown
                ddlRecruitmentManager.DataBind();

                // Default value of dropdown is "Select"
                ddlRecruitmentManager.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
            }
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFRAISEHEADCOUNT, "FillRecruitmentManagerDropDown", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Gets the subject for mail
    /// </summary>
    /// <param name="mrfCode"></param>
    /// <param name="ProjectName"></param>
    /// <returns>string</returns>
    private string GetProjectSubject(string mrfCode, string ProjectName)
    {
        try
        {
            string Projectsubject;
            Projectsubject = "Approval required for Head Count request. [MRF Code: <" + mrfCode + ">,Project Name<" + ProjectName + ">]";
            return Projectsubject;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFRAISEHEADCOUNT, "GetProjectSubject", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Gets the subject for mail
    /// </summary>
    /// <param name="mrfCode"></param>
    /// <param name="Department"></param>
    /// <returns>string</returns>
    private string GetDepartmentSubject(string mrfCode, string Department)
    {
        try
        {
            string Projectsubject;
            Projectsubject = "Approval required for Head Count request. [MRF Code: <" + mrfCode + ">,Department Name<" + Department + ">]";
            return Projectsubject;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFRAISEHEADCOUNT, "GetDepartmentSubject", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Gets the link to be appended in mail
    /// </summary>
    /// <returns>string</returns>
    private string GetLink()
    {
        try
        {
            //String sComp = System.Net.Dns.GetHostEntry(Request.ServerVariables["Local_Addr"]).HostName.ToString();
            //sComp = sComp.Substring(0, sComp.IndexOf(@"."));
            //string strApproveheadCount = sComp + Request.ServerVariables["PATH_INFO"];
            //strApproveheadCount = "http://" + strApproveheadCount.Replace(Common.AuthorizationManager.AuthorizationManager.GetCurrentPageName(), CommonConstants.Page_MrfApproveRejectHeadCount);
            //return strApproveheadCount;

            string sComp = Utility.GetUrl() + CommonConstants.Page_MrfApproveRejectHeadCount;

            return sComp;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFRAISEHEADCOUNT, "GetLink", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Get the Email ID of head count approver by department name for sending to approve mail.
    /// </summary>
    /// <param name="DepartmentName"></param>
    /// <returns></returns>
    private string GetEmailIdForDeptWiseHeadCountApprovalMailTo(string DepartmentName)
    {
        string mailTo = string.Empty;
        try
        {
            Rave.HR.BusinessLayer.MRF.MRFDetail MRFDetailsBL = new Rave.HR.BusinessLayer.MRF.MRFDetail();
            List<BusinessEntities.MRFDetail> listMRFDetail = new List<BusinessEntities.MRFDetail>();

            //Get all the data of department wise approver.
            listMRFDetail = MRFDetailsBL.GetEmailIdForHeadCountApproval();
            foreach (BusinessEntities.MRFDetail itemMRF in listMRFDetail)
            {
                //Check whether department name is same then assign approver email id to mailto.
                if (itemMRF.DepartmentName == DepartmentName)
                {
                    //mail should sent only to high priority person, not to all approver of department.
                    //we can give rights to multiple resource bt mail should go to only one.
                    mailTo = itemMRF.EmailId;
                    break;
                }
            }
            return mailTo;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFRAISEHEADCOUNT, "GetEmailIdForMailTo", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Get the link of MRF summary page for mail to recruiter.
    /// </summary>
    /// <returns></returns>
    private string GetLinkWithoutApproval()
    {
        try
        {
            string sComp = Utility.GetUrl() + CommonConstants.Page_MrfSummary;

            return sComp;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFRAISEHEADCOUNT, "GetLinkWithoutApproval", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Get the Employee email Id by empId.
    /// </summary>
    /// <param name="EmpId"></param>
    /// <returns></returns>
    private string GetEmailIdByEmpId(int EmpId)
    {
        try
        {

            Rave.HR.BusinessLayer.Contracts.Contract contractBL = new Rave.HR.BusinessLayer.Contracts.Contract();
            return contractBL.GetEmailIdByEmpId(EmpId);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_MRFRAISEHEADCOUNT, "GetEmailIdByEmpId", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }
    }

    #endregion Private Methods




    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
