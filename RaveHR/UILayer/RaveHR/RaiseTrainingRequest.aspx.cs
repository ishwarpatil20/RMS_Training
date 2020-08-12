//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           RaiseTrainingRequest.aspx.cs       
//  Author:         Ishwar Patil
//  Date written:   07/04/2014
//  Description:    The Raise Training Request page you can raise new training. It is also used to View,Update,Delete.   
//  Date                        Who             Ref     Description
//  ----                        -----------     ---     -----------
//  07/04/2014                  Ishwar.Patil    n/a     Created    
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Common.AuthorizationManager;
using Common.Constants;
using System.Globalization;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using Rave.HR.BusinessLayer.Interface;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using BusinessEntities;
using Rave.HR.BusinessLayer;


public partial class RaiseTrainingRequest : BaseClass
{

    #region Function Names

    const string FunctionPage_Load = "Page_Load";
    const string FunctionGetMasterData_TrainingTypeDropDown = "GetMasterData_TrainingTypeDropDown";
    const string FunctionGetMasterData_TrainingNameDropDown = "GetMasterData_TechnicalNameDropDown";
    const string FunctionGetMasterData_TrainingCategoryDropDown = "GetMasterData_TrainingCategoryDropDown";
    const string FunctionGetMasterData_TrainingQuarterDropDown = "GetMasterData_TrainingQuarterDropDown";
    const string FunctionGetMasterData_TrainingNoOfParticipantsDropDown = "GetMasterData_TrainingNoOfParticipantsDropDown";
    const string FunctionGetMasterData_RequestByTechnicalDropDown = "GetMasterData_RequestByTechnicalDropDown";
    const string FunctionGetMasterData_SoftSkillsNameDropDown = "GetMasterData_SoftSkillsNameDropDown";
    const string FunctionClearTechnicalControls = "ClearTechnicalControls";
    const string FunctionbtnSave_Click = "btnSave_Click";
    const string FunctionGetMasterData_KSSTrainingTypeDropDown = "GetMasterData_KSSTrainingTypeDropDown";
    const string FunctionbtnSaveKSS_Click = "btnSaveKSS_Click";
    const string FunctionClearKSSControls = "ClearKSSControls";
    const string FunctionbtnSaveSeminars_Click = "btnSaveSeminars_Click";
    const string FunctionClearSeminarsControls = "ClearSeminarsControls";
    const string FunctionGetMasterData_RequestBySeminarsDropDown = "GetMasterData_RequestBySeminarsDropDown";
    const string FunctionddlTrainingType_SelectedIndexChanged = "ddlTrainingType_SelectedIndexChanged";
    const string FunctionTechnicalTrainingGridView = "TechnicalTrainingGridView";
    const string FunctiongvTechnicalTrainingSummary_RowCommand = "gvTechnicalTrainingSummary_RowCommand";
    const string FunctiongvSoftSkillsTrainingSummary_RowCommand = "gvSoftSkillsTrainingSummary_RowCommand";
    const string FunctiongvKSSTrainingSummary_RowCommand = "gvKSSTrainingSummary_RowCommand";
    const string FunctionViewTechnicalTriningSummary = "ViewTechnicalTriningSummary";
    const string FunctionGetTechnicalSoftSkillsTraining = "GetTechnicalSoftSkillsTraining";
    const string FunctionDeleteTechnicalSoftSkillsTraining = "DeleteTechnicalSoftSkillsTraining";
    const string FunctionViewSoftSkillsTriningSummary = "ViewSoftSkillsTriningSummary";
    const string FunctionViewKSSTraining = "ViewKSSTraining";
    const string FunctionKSSTrainingGridView = "KSSTrainingGridView";
    const string FunctionGetKSSTraining = "GetKSSTraining";
    const string FunctionDeleteDeleteKSSTraining = "DeleteDeleteKSSTraining";
    const string FunctiongvSeminarsSummary_RowCommand = "gvSeminarsSummary_RowCommand";
    const string FunctionViewSeminarsTraining = "ViewSeminarsTraining";
    const string FunctionSeminarsTrainingGridView = "SeminarsTrainingGridView";
    const string FunctionGetSeminarsTraining = "GetSeminarsTraining";
    const string FunctionDeleteSeminarsTraining = "DeleteSeminarsTraining";
    const string FunctiongvSeminarsSummary_OnRowDataBound = "gvSeminarsSummary_OnRowDataBound";
    const string FunctiongvKSSTrainingSummary_OnRowDataBound = "gvKSSTrainingSummary_OnRowDataBound";
    const string FunctiongvTechnicalTrainingSummary_OnRowDataBound = "gvTechnicalTrainingSummary_OnRowDataBound";
    const string FunctiongvSoftSkillsTrainingSummary_OnRowDataBound = "gvSoftSkillsTrainingSummary_OnRowDataBound";
    const string FunctionEditSeminarsDetails = "EditSeminarsDetails";
    
    #endregion

    Rave.HR.BusinessLayer.Training.RaiseTrainingRequest saveTrainingBL = null;
    Rave.HR.BusinessLayer.Employee.Employee addEmployeeBAL = null;
    Rave.HR.BusinessLayer.Common.Master objRaveHRMasterBAL = null;
    private static BusinessEntities.RaiseTrainingRequest RaiseTrainingCollection;
    List<BusinessEntities.Master> objRaveHRMaster = null;
    BusinessEntities.ParameterCriteria objParameter = new BusinessEntities.ParameterCriteria();
    BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

    private static string sortExpression = string.Empty;

    
    private int selectedIndex;
    private string commandName = string.Empty;
    private string RaiseID = string.Empty;
    private string Action = string.Empty;
    private string Page = string.Empty;
    private string TrainingTypeID = string.Empty;
    //string KSSDocumentPath = "~/Resumes/";
    private int RaiseTrainingId;
    private string UserMailId;
    private int UserEmpID;
    private int AccessRightID;
    bool result = false;
    private int EditCount = 0;
    DataSet ds;

    private const string CLASS_NAME = "RaiseTrainingRequest.aspx";

    /// <summary>
    /// Handles the Init event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Init(object sender, EventArgs e)
    {
        imgNameofParticipant.Attributes.Add("onclick", "return popUpNameOfParticipantSearch();");
        imgPresenter.Attributes.Add("onclick", "return popUpPresenterSearch();");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString[QueryStringConstants.TrainingType] != null)
            TrainingTypeID = DecryptQueryString(QueryStringConstants.TrainingType).ToString();

        if (Request.QueryString[QueryStringConstants.RaiseID] != null)
            RaiseID = DecryptQueryString(QueryStringConstants.RaiseID).ToString();

        if (Request.QueryString[QueryStringConstants.Action] != null)
            Action = DecryptQueryString(QueryStringConstants.Action).ToString();

        if (Request.QueryString[QueryStringConstants.Page] != null)
            Page = DecryptQueryString(QueryStringConstants.Page).ToString();

        AuthorizationManager authoriseduser = new AuthorizationManager();
        UserMailId = authoriseduser.getLoggedInUserEmailId();
        try
        {
            objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            UserEmpID = objRaveHRMasterBAL.GetLoggedInUserID(UserMailId);
            lblMandatory.Text = string.Empty;
            lblConfirmMessage.Text = string.Empty;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "RaiseTrainingRequest.aspx", "FunctionPage_Load", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
        if (!IsPostBack)
        {
            saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
            AccessRightID = saveTrainingBL.AccessForTrainingModule(UserEmpID);

            if (AccessRightID == 0)
            {
                Response.Redirect("Home.aspx");
            }
            else
            {
                GetMasterData_TrainingTypeDropDown();
                //GetMasterData_TechnicalNameDropDown();
                ddlTrainingNameTech.Items.Insert(0, "Select");
                GetMasterData_TrainingCategoryDropDown();
                GetMasterData_TrainingQuarterDropDown();
                GetMasterData_TrainingNoOfParticipantsDropDown();
                GetMasterData_RequestByTechnicalDropDown();

                saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
                DataTable dt = saveTrainingBL.GetEditKSSTrainingGroup(UserEmpID, CommonConstants.DefaultFlagZero.ToString());
                EditCount = Convert.ToInt32(dt.Rows.Count);
                if (EditCount == CommonConstants.DefaultFlagZero)
                {
                    ddlRequestByTechnical.Enabled = false;
                    ddlRequestedBySeminars.Enabled = false;
                }

                if (Session[SessionNames.CURRENT_PAGE_INDEX_TRAINING] == null)
                    Session[SessionNames.CURRENT_PAGE_INDEX_TRAINING] = 1;

                if (!string.IsNullOrEmpty(RaiseID))
                {
                    UpdateAndViewTrainingDetails();
                }
            }
        }

        

        ddlTrainingNameTech.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzTrainingNameTech.ClientID + "','','');");
        ddlQuarter.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzQuarter.ClientID + "','','');");
        ddlNoofParticipants.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzNoofParticipants.ClientID + "','','');");
        ddlCategoryTech.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzCategory.ClientID + "','','');");
        ddlRequestByTechnical.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzRequestBy.ClientID + "','','');");
        ddlpriority.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzpriority.ClientID + "','','');");
        txtTrainingTypeOtherTech.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + txtTrainingTypeOtherTech.ClientID + "','','');");
        txtSeminarsName.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + txtSeminarsName.ClientID + "','','');");
        ucSeminarsDate.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + ucSeminarsDate.ClientID + "','','');");
        ddlRequestedBySeminars.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzRequestedBySeminars.ClientID + "','','');");
        ddlType.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + spanzType.ClientID + "','','');");
        txtTopic.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + txtTopic.ClientID + "','','');");
        txtAgenda.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + txtAgenda.ClientID + "','','');");
        ucDate.Attributes.Add(Common.CommonConstants.EVENT_ONBLUR, "javascript:ValidateControl('" + ucDate.ClientID + "','','');");
    }

    /// <summary>
    ///  Method to get values of Training Type
    /// </summary>
    private void GetMasterData_TrainingTypeDropDown()
    {
        try
        {
            objRaveHRMaster = new List<BusinessEntities.Master>();
            objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            objRaveHRMaster = objRaveHRMasterBAL.GetTraining_MasterData(CommonConstants.TrainingType);
            ddlTrainingType.DataSource = objRaveHRMaster;
            ddlTrainingType.DataTextField = "MasterName";
            ddlTrainingType.DataValueField = "MasterID";
            ddlTrainingType.DataBind();
            ddlTrainingType.Items.Insert(0, "Select");

            saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
            DataTable dt = saveTrainingBL.GetEditKSSTrainingGroup(UserEmpID, CommonConstants.DefaultFlagOne.ToString());
            EditCount = Convert.ToInt32(dt.Rows.Count);
            if (EditCount == CommonConstants.DefaultFlagZero)
            {
                ddlTrainingType.Items.Remove(ddlTrainingType.Items.FindByValue(CommonConstants.KSSID.ToString()));
            }
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "RaiseTrainingRequest.aspx", "FunctionGetMasterData_TrainingTypeDropDown", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    ///  Method to get values of Training Name
    /// </summary>
    private void GetMasterData_TechnicalNameDropDown()
    {
        try
        {
            objRaveHRMaster = new List<BusinessEntities.Master>();
            objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            objRaveHRMaster = objRaveHRMasterBAL.GetTraining_MasterData(CommonConstants.TrainingName);
            ddlTrainingNameTech.DataSource = objRaveHRMaster;
            ddlTrainingNameTech.DataTextField = "MasterName";
            ddlTrainingNameTech.DataValueField = "MasterID";
            ddlTrainingNameTech.DataBind();
            ddlTrainingNameTech.Items.Insert(0, "Select");
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "RaiseTrainingRequest.aspx", "FunctionGetMasterData_TrainingNameDropDown", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    ///  Method to get values of Training Category
    /// </summary>
    private void GetMasterData_TrainingCategoryDropDown()
    {
        try
        {
            objRaveHRMaster = new List<BusinessEntities.Master>();
            objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            objRaveHRMaster = objRaveHRMasterBAL.GetTraining_MasterData(CommonConstants.TrainingCategory);
            ddlCategoryTech.DataSource = objRaveHRMaster;
            ddlCategoryTech.DataTextField = "MasterName";
            ddlCategoryTech.DataValueField = "MasterID";
            ddlCategoryTech.DataBind();
            ddlCategoryTech.Items.Insert(0, "Select");
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "RaiseTrainingRequest.aspx", "FunctionGetMasterData_TrainingCategoryDropDown", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    ///  Method to get values of Training Quarter
    /// </summary>
    private void GetMasterData_TrainingQuarterDropDown()
    {
        try
        {
            int mth = DateTime.Now.Month;
            if (mth <= 3)
            {
                ddlQuarter.Items.Insert(0, new ListItem("Select", "0"));
                ddlQuarter.Items.Insert(1, new ListItem("Jan - Mar", "1"));
                ddlQuarter.Items.Insert(2, new ListItem("Apr - Jun", "2"));
            }
            else if (mth > 3 && mth <= 6)
            {
                ddlQuarter.Items.Insert(0, new ListItem("Select", "0"));
                ddlQuarter.Items.Insert(1, new ListItem("Apr - Jun", "2"));
                ddlQuarter.Items.Insert(2, new ListItem("Jul - Sep", "3"));
            }
            else if (mth > 6 && mth <= 9)
            {
                ddlQuarter.Items.Insert(0, new ListItem("Select", "0"));
                ddlQuarter.Items.Insert(1, new ListItem("Jul - Sep", "3"));
                ddlQuarter.Items.Insert(2, new ListItem("Oct - Dec", "4"));
            }
            else if (mth > 9 && mth <= 12)
            {
                ddlQuarter.Items.Insert(0, new ListItem("Select", "0"));
                ddlQuarter.Items.Insert(1, new ListItem("Oct - Dec", "4"));
                ddlQuarter.Items.Insert(2, new ListItem("Jan - Mar", "1"));
            }

            /*objRaveHRMaster = new List<BusinessEntities.Master>();
            objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            objRaveHRMaster = objRaveHRMasterBAL.GetTraining_MasterData(CommonConstants.TrainingQuarter);
            ddlQuarter.DataSource = objRaveHRMaster;
            ddlQuarter.DataTextField = "MasterName";
            ddlQuarter.DataValueField = "MasterID";
            ddlQuarter.DataBind();
            ddlQuarter.Items.Insert(0, "Select");*/
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "RaiseTrainingRequest.aspx", "FunctionGetMasterData_TrainingQuarterDropDown", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    ///  Method to get values of Training No Of Participants
    /// </summary>
    private void GetMasterData_TrainingNoOfParticipantsDropDown()
    {
        try
        {
            objRaveHRMaster = new List<BusinessEntities.Master>();
            objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            objRaveHRMaster = objRaveHRMasterBAL.GetTraining_MasterData(CommonConstants.TrainingParticipants);
            ddlNoofParticipants.DataSource = objRaveHRMaster;
            ddlNoofParticipants.DataTextField = "MasterName";
            ddlNoofParticipants.DataValueField = "MasterID";
            ddlNoofParticipants.DataBind();
            ddlNoofParticipants.Items.Insert(0, "Select");
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "RaiseTrainingRequest.aspx", "FunctionGetMasterData_TrainingNoOfParticipantsDropDown", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    ///  Method to get values of KSS Training Type
    /// </summary>
    private void GetMasterData_KSSTrainingTypeDropDown()
    {
        try
        {
            objRaveHRMaster = new List<BusinessEntities.Master>();
            objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            objRaveHRMaster = objRaveHRMasterBAL.GetTraining_MasterData(CommonConstants.KSSType);
            ddlType.DataSource = objRaveHRMaster;
            ddlType.DataTextField = "MasterName";
            ddlType.DataValueField = "MasterID";
            ddlType.DataBind();
            ddlType.Items.Insert(0, "Select");
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "RaiseTrainingRequest.aspx", "FunctionGetMasterData_KSSTrainingTypeDropDown", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    ///  Method to get values of RequestBy(Technical Training)
    /// </summary>
    private void GetMasterData_RequestByTechnicalDropDown()
    {
        try
        {
            ds = new DataSet();
            addEmployeeBAL = new Rave.HR.BusinessLayer.Employee.Employee();
            ds = addEmployeeBAL.GetActiveEmployeeList();
            ddlRequestByTechnical.DataSource = ds.Tables[0];
            ddlRequestByTechnical.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlRequestByTechnical.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlRequestByTechnical.DataBind();
            ddlRequestByTechnical.Items.Insert(0, "Select");
            ddlRequestByTechnical.Items.FindByValue(UserEmpID.ToString()).Selected = true;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "RaiseTrainingRequest.aspx", "FunctionGetMasterData_RequestByTechnicalDropDown", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    ///  Method to get values of RequestBy(Seminars Training)
    /// </summary>
    private void GetMasterData_RequestBySeminarsDropDown()
    {
        try
        {
            ds = new DataSet();
            addEmployeeBAL = new Rave.HR.BusinessLayer.Employee.Employee();
            ds = addEmployeeBAL.GetActiveEmployeeList();
            ddlRequestedBySeminars.DataSource = ds.Tables[0];
            ddlRequestedBySeminars.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlRequestedBySeminars.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlRequestedBySeminars.DataBind();
            ddlRequestedBySeminars.Items.Insert(0, "Select");
            ddlRequestedBySeminars.Items.FindByValue(UserEmpID.ToString()).Selected = true;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "RaiseTrainingRequest.aspx", "FunctionGetMasterData_RequestBySeminarsDropDown", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    ///// <summary>
    /////  Method to get values of Name of Participant(Seminars Training)
    ///// </summary>
    //private void GetMasterData_NameOfParticipantsSeminarsDropDown()
    //{
    //    try
    //    {
    //        ds = new DataSet();
    //        addEmployeeBAL = new Rave.HR.BusinessLayer.Employee.Employee();
    //        ds = addEmployeeBAL.GetActiveEmployeeList();
    //        ddlNameofParticipant.DataSource = ds.Tables[0];
    //        ddlNameofParticipant.DataTextField = ds.Tables[0].Columns[1].ToString();
    //        ddlNameofParticipant.DataValueField = ds.Tables[0].Columns[0].ToString();
    //        ddlNameofParticipant.DataBind();
    //        ddlNameofParticipant.Items.Insert(0, "Select");
    //    }
    //    catch (RaveHRException ex)
    //    {
    //        throw ex;
    //    }
    //    catch (Exception ex)
    //    {
    //        RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "RaiseTrainingRequest.aspx", "FunctionGetMasterData_NameOfParticipantsSeminarsDropDown", EventIDConstants.TRAINING_PRESENTATION_LAYER);
    //        LogErrorMessage(objEx);
    //    }
    //}

    /// <summary>
    ///  Method to get values of Presenter(KSS Training)
    /// </summary>
    //private void GetMasterData_PresenterDropDown()
    //{
    //    try
    //    {
    //        ds = new DataSet();
    //        addEmployeeBAL = new Rave.HR.BusinessLayer.Employee.Employee();
    //        ds = addEmployeeBAL.GetActiveEmployeeList();
    //        ddlPresenter.DataSource = ds.Tables[0];
    //        ddlPresenter.DataTextField = ds.Tables[0].Columns[1].ToString();
    //        ddlPresenter.DataValueField = ds.Tables[0].Columns[0].ToString();
    //        ddlPresenter.DataBind();
    //        ddlPresenter.Items.Insert(0, "Select");
    //    }
    //    catch (RaveHRException ex)
    //    {
    //        throw ex;
    //    }
    //    catch (Exception ex)
    //    {
    //        RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "RaiseTrainingRequest.aspx", "FunctionGetMasterData_PresenterDropDown", EventIDConstants.TRAINING_PRESENTATION_LAYER);
    //        LogErrorMessage(objEx);
    //    }
    //}

    /// <summary>
    ///  Method to get values of SoftSkills
    /// </summary>
    private void GetMasterData_SoftSkillsNameDropDown()
    {
        try
        {
            objRaveHRMaster = new List<BusinessEntities.Master>();
            objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            objRaveHRMaster = objRaveHRMasterBAL.GetTraining_MasterData(CommonConstants.SoftSkills);
            ddlTrainingNameTech.DataSource = objRaveHRMaster;
            ddlTrainingNameTech.DataTextField = "MasterName";
            ddlTrainingNameTech.DataValueField = "MasterID";
            ddlTrainingNameTech.DataBind();
            ddlTrainingNameTech.Items.Insert(0, "Select");
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "RaiseTrainingRequest.aspx", "FunctionGetMasterData_SoftSkillsNameDropDown", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// btnSave_Click event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text.ToLower() == CommonConstants.Back)
            {
                if (Page == CommonConstants.ApproveRejectSummaryPage)
                {
                    if (Convert.ToInt32(ddlTrainingType.SelectedValue) == CommonConstants.TechnicalTrainingID)
                    {
                        Response.Redirect("~/ApproveRejectTrainingRequest.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.TechnicalTrainingID.ToString()), false);
                    }
                    else if (Convert.ToInt32(ddlTrainingType.SelectedValue) == CommonConstants.SoftSkillsTrainingID)
                    {
                        Response.Redirect("~/ApproveRejectTrainingRequest.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.SoftSkillsTrainingID.ToString()), false);
                    }
                }
                else if (Page == CommonConstants.SummaryPage)
                {

                    if (Convert.ToInt32(ddlTrainingType.SelectedValue) == CommonConstants.TechnicalTrainingID)
                    {
                        Response.Redirect("~/RaiseTrainingSummary.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.TechnicalTrainingID.ToString()), false);
                    }
                    else if (Convert.ToInt32(ddlTrainingType.SelectedValue) == CommonConstants.SoftSkillsTrainingID)
                    {
                        Response.Redirect("~/RaiseTrainingSummary.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.SoftSkillsTrainingID.ToString()), false);
                    }
                }
            }
            else
            {
                RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
                if (!String.IsNullOrEmpty(hfRaiseID.Value))
                {
                    RaiseTrainingCollection.RaiseID = Convert.ToInt32(hfRaiseID.Value);
                }
                else
                {
                    RaiseTrainingCollection.RaiseID = 0;
                }
                RaiseTrainingCollection.TrainingType = ddlTrainingType.SelectedValue;
                RaiseTrainingCollection.TrainingStatus = CommonConstants.TrainingStatusOpen.ToString();
                if (Convert.ToInt32(ddlTrainingType.SelectedValue) == CommonConstants.TechnicalTrainingID)
                {
                    RaiseTrainingCollection.TrainingName = ddlTrainingNameTech.SelectedValue;
                }
                else if (Convert.ToInt32(ddlTrainingType.SelectedValue) == CommonConstants.SoftSkillsTrainingID)
                {
                    RaiseTrainingCollection.TrainingName = ddlTrainingNameTech.SelectedValue;
                }
                RaiseTrainingCollection.TrainingNameOther = txtTrainingTypeOtherTech.Text;
                RaiseTrainingCollection.Quarter = ddlQuarter.SelectedValue;
                RaiseTrainingCollection.NoOfParticipant = ddlNoofParticipants.SelectedValue;
                RaiseTrainingCollection.Category = ddlCategoryTech.SelectedValue;
                RaiseTrainingCollection.RequestedBy = ddlRequestByTechnical.SelectedValue;
                RaiseTrainingCollection.Priority = ddlpriority.SelectedValue;
                RaiseTrainingCollection.BusinessImpact = txtBusinessImpact.Text;
                RaiseTrainingCollection.Comments = txtCommentsTechnical.Text;
                RaiseTrainingCollection.CreatedByEmailId = UserMailId;

                saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
                RaiseTrainingId = saveTrainingBL.Save(RaiseTrainingCollection, CommonConstants.TechnicalTrainingID, CommonConstants.SoftSkillsTrainingID);

                if (RaiseTrainingId != 0)
                    result = true;

                if (result == true)
                {
                    if (Convert.ToInt32(ddlTrainingType.SelectedValue) == CommonConstants.TechnicalTrainingID)
                    {
                        if (btnSave.Text.ToLower() == CommonConstants.Update)
                        {
                            string msg = ddlTrainingNameTech.SelectedItem.Text.ToLower() == CommonConstants.TrainingTypeOther ? txtTrainingTypeOtherTech.Text.ToString() : ddlTrainingNameTech.SelectedItem.ToString();
                            msg += " training details edited.";
                            //Ishwar Patil : Training Module : 27/08/2014 Start
                            SendMailForTechSoftSkillEdit(Convert.ToInt32(hfRaiseID.Value));
                            //Ishwar Patil : Training Module : 27/08/2014 End
                            System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                            string scriptKey = "ConfirmationScript";
                            javaScript.Append("var userConfirmation = window.alert('" + msg + "');\n");
                            javaScript.Append("location.href='RaiseTrainingSummary.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.TechnicalTrainingID.ToString()) + "';");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), scriptKey, javaScript.ToString(), true);
                        }
                        else
                        {
                            lblConfirmMessage.Text = "Training requisition submitted.";
                            //Ishwar Patil : Training Module : 19/08/2014 Start
                            SendMailForTechSoftSkill();
                            //Ishwar Patil : Training Module : 19/08/2014 End
                            ClearTechnicalControls();
                        }
                    }
                    else if (Convert.ToInt32(ddlTrainingType.SelectedValue) == CommonConstants.SoftSkillsTrainingID)
                    {
                        if (btnSave.Text.ToLower() == CommonConstants.Update)
                        {
                            string msg = ddlTrainingNameTech.SelectedItem.Text.ToLower() == CommonConstants.TrainingTypeOther ? txtTrainingTypeOtherTech.Text.ToString() : ddlTrainingNameTech.SelectedItem.ToString();
                            msg += " training details edited.";
                            //Ishwar Patil : Training Module : 27/08/2014 Start
                            SendMailForTechSoftSkillEdit(Convert.ToInt32(hfRaiseID.Value));
                            //Ishwar Patil : Training Module : 27/08/2014 End
                            System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                            string scriptKey = "ConfirmationScript";
                            javaScript.Append("var userConfirmation = window.alert('" + msg + "');\n");
                            //javaScript.Append("location.href='RaiseTrainingSummary.aspx';");
                            javaScript.Append("location.href='RaiseTrainingSummary.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.SoftSkillsTrainingID.ToString()) + "';");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), scriptKey, javaScript.ToString(), true);
                        }
                        else
                        {
                            lblConfirmMessage.Text = "Training requisition submitted.";
                            //Ishwar Patil : Training Module : 19/08/2014 Start
                            SendMailForTechSoftSkill();
                            //Ishwar Patil : Training Module : 19/08/2014 End
                            ClearTechnicalControls();
                        }
                    }
                    if (ddlTrainingNameTech.SelectedItem.Text.ToLower() != CommonConstants.TrainingTypeOther)
                    {
                        td1OtherTech.Style.Add("display", "none");
                        td2OtherTech.Style.Add("display", "none");
                    }
                }
                if (btnSave.Text.ToLower() == CommonConstants.Submit)
                {
                    if (Convert.ToInt32(ddlTrainingType.SelectedValue) == CommonConstants.TechnicalTrainingID)
                    {
                        ddlTrainingNameTech.ClearSelection();
                        GetMasterData_TechnicalNameDropDown();
                        ddlTrainingNameTech.SelectedIndex = 0;
                    }
                    else if (Convert.ToInt32(ddlTrainingType.SelectedValue) == CommonConstants.SoftSkillsTrainingID)
                    {
                        ddlTrainingNameTech.ClearSelection();
                        GetMasterData_SoftSkillsNameDropDown();
                        ddlTrainingNameTech.SelectedIndex = 0;
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionbtnSave_Click, EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// method to get html data
    /// </summary>
//    private string GetHTMLForTableData(BusinessEntities.MRFDetail mrfDetail)
    
    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (btnReset.Text == CommonConstants.Cancel)
            {
                if (Convert.ToInt32(ddlTrainingType.SelectedValue) == CommonConstants.TechnicalTrainingID)
                {
                    Response.Redirect("~/RaiseTrainingSummary.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.TechnicalTrainingID.ToString()), false);
                }
                else if (Convert.ToInt32(ddlTrainingType.SelectedValue) == CommonConstants.SoftSkillsTrainingID)
                {
                    Response.Redirect("~/RaiseTrainingSummary.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.SoftSkillsTrainingID.ToString()), false);
                }
            }
            else
            {
                GetMasterData_RequestByTechnicalDropDown();
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "FunctionbtnReset_OnClick", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnResetKSS_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (btnResetKSS.Text == CommonConstants.Cancel)
            {
                Response.Redirect("~/RaiseTrainingSummary.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.KSSID.ToString()), false);
            }
            else
            {
                txtPresenter.Text = string.Empty;
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "FunctionbtnResetKSS_OnClick", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnResetSeminars_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (btnResetSeminars.Text == CommonConstants.Cancel)
            {
                Response.Redirect("~/RaiseTrainingSummary.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.SeminarsID.ToString()), false);
            }
            else
            {
                txtNameofParticipant.Text = string.Empty;
                GetMasterData_RequestBySeminarsDropDown();
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "FunctionbtnResetSeminars_OnClick", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    
    /// <summary>
    /// btnSaveKSS_Click event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSaveKSS_Click(object sender, EventArgs e)
    {
        try
        {
            bool SaveFlag = true;
            if (btnSaveKSS.Text.ToLower() == CommonConstants.Back)
            {
                if (Page == CommonConstants.ApproveRejectSummaryPage)
                {
                    Response.Redirect("~/ApproveRejectTrainingRequest.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.KSSID.ToString()), false);
                }
                else if (Page == CommonConstants.SummaryPage)
                {
                    Response.Redirect("~/RaiseTrainingSummary.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.KSSID.ToString()), false);
                }
            }
            else
            {
                saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
                DataTable dt = saveTrainingBL.GetEditKSSTrainingGroup(UserEmpID, CommonConstants.DefaultFlagZero.ToString());
                EditCount = Convert.ToInt32(dt.Rows.Count);
                if (EditCount == CommonConstants.DefaultFlagZero)
                {
                    DateTime sysdate = DateTime.Today;
                    DateTime Seminarsdate = Convert.ToDateTime(ucDate.Text);
                    if (Seminarsdate < sysdate)
                    {
                        SaveFlag = false;
                        lblMandatory.Text = "Date should be greater than or equal to today's date.";
                        txtPresenter.Text = hfPresenter.Value;
                        this.divPresenterDate.Attributes.Add("style", "width:230px; border-color:Red; border-style:solid; border-width:1px");
                    }
                }
                if (SaveFlag == true)
                {
                    this.divPresenterDate.Attributes.Add("style", "width:230px; border-color:White;");
                    RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
                    if (!String.IsNullOrEmpty(hfRaiseID.Value))
                    {
                        RaiseTrainingCollection.RaiseID = Convert.ToInt32(hfRaiseID.Value);
                    }
                    else
                    {
                        RaiseTrainingCollection.RaiseID = 0;
                    }
                    RaiseTrainingCollection.TrainingType = ddlTrainingType.SelectedValue;
                    RaiseTrainingCollection.TrainingStatus = CommonConstants.TrainingStatusOpen.ToString();
                    RaiseTrainingCollection.Type = ddlType.SelectedValue;
                    RaiseTrainingCollection.Topic = txtTopic.Text;
                    RaiseTrainingCollection.Agenda = txtAgenda.Text;
                    RaiseTrainingCollection.PresenterID = hfID.Value;
                    RaiseTrainingCollection.Date = Convert.ToDateTime(ucDate.Text);
                    RaiseTrainingCollection.Comments = txtCommentsKSS.Text;
                    RaiseTrainingCollection.CreatedByEmailId = UserMailId;

                    saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
                    RaiseTrainingId = saveTrainingBL.SaveKSS(RaiseTrainingCollection);

                    if (RaiseTrainingId != 0)
                        result = true;

                    if (result == true)
                    {
                        if (btnSaveKSS.Text.ToLower() == CommonConstants.Update)
                        {
                            string msg = "Knowledge Sharing Session(" + ddlType.SelectedItem + ") details edited.";
                            SendMailForKSSEdit(Convert.ToInt32(hfRaiseID.Value));
                            //lblConfirmMessage.Text = msg;
                            System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                            string scriptKey = "ConfirmationScript";
                            javaScript.Append("var userConfirmation = window.alert('" + msg + "');\n");
                            //javaScript.Append("location.href='RaiseTrainingSummary.aspx';");
                            javaScript.Append("location.href='RaiseTrainingSummary.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.KSSID.ToString()) + "';");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), scriptKey, javaScript.ToString(), true);
                        }
                        else
                        {
                            lblConfirmMessage.Text = "Knowledge Sharing Session(" + ddlType.SelectedItem + ") details submitted.";
                            //Ishwar Patil : Training Module : 21/08/2014 Start
                            SendMailForKSS(RaiseTrainingCollection);
                            //Ishwar Patil : Training Module : 21/08/2014 End
                            ClearKSSControls();
                        }
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionbtnSaveKSS_Click, EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    
    /// <summary>
    /// btnSaveSeminars_Click event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSaveSeminars_Click(object sender, EventArgs e)
    {
        try
        {
            bool SaveFlag = true;
            if (btnSaveSeminars.Text.ToLower() == CommonConstants.Back)
            {
                if (Page == CommonConstants.ApproveRejectSummaryPage)
                {
                    Response.Redirect("~/ApproveRejectTrainingRequest.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.SeminarsID.ToString()), false);
                }
                else if (Page == CommonConstants.SummaryPage)
                {
                    Response.Redirect("~/RaiseTrainingSummary.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.SeminarsID.ToString()), false);
                }
            }
            else
            {
                saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
                DataTable dt = saveTrainingBL.GetEditKSSTrainingGroup(UserEmpID, CommonConstants.DefaultFlagZero.ToString());
                EditCount = Convert.ToInt32(dt.Rows.Count);
                if (EditCount == CommonConstants.DefaultFlagZero)
                {
                    DateTime sysdate = DateTime.Today;
                    DateTime Seminarsdate = Convert.ToDateTime(ucSeminarsDate.Text);
                    if (Seminarsdate < sysdate)
                    {
                        SaveFlag = false;
                        lblMandatory.Text = "Date should be greater than or equal to today's date.";
                        txtNameofParticipant.Text = hfNameofParticipant.Value;
                        this.divdate.Attributes.Add("style", "width:230px; border-color:Red; border-style:solid; border-width:1px");
                    }
                }
                if (SaveFlag == true)
                {
                    this.divdate.Attributes.Add("style", "width:230px; border-color:White;");
                    string NameOfParticipantsIds = string.Empty;
                    int i = 0;
                    RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();

                    if (!String.IsNullOrEmpty(hfRaiseID.Value))
                    {
                        RaiseTrainingCollection.RaiseID = Convert.ToInt32(hfRaiseID.Value);
                    }
                    else
                    {
                        RaiseTrainingCollection.RaiseID = 0;
                    }
                    RaiseTrainingCollection.TrainingType = ddlTrainingType.SelectedValue;
                    RaiseTrainingCollection.TrainingStatus = CommonConstants.TrainingStatusOpen.ToString();
                    RaiseTrainingCollection.SeminarsName = txtSeminarsName.Text;
                    RaiseTrainingCollection.Date = Convert.ToDateTime(ucSeminarsDate.Text);
                    RaiseTrainingCollection.RequestedBy = ddlRequestedBySeminars.SelectedValue;
                    RaiseTrainingCollection.NameOfParticipantID = hfID.Value;
                    RaiseTrainingCollection.URL = txtURL.Text;
                    RaiseTrainingCollection.Comments = txtCommentSeminars.Text;
                    RaiseTrainingCollection.CreatedByEmailId = UserMailId;

                    saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
                    RaiseTrainingId = saveTrainingBL.SaveSeminars(RaiseTrainingCollection);

                    if (RaiseTrainingId != 0)
                        result = true;

                    if (result == true)
                    {
                        if (btnSaveSeminars.Text.ToLower() == CommonConstants.Update)
                        {
                            string msg = txtSeminarsName.Text + " details edited.";
                            //Ishwar Patil : Training Module : 03/09/2014 Start
                            SendMailForSeminarEdit(Convert.ToInt32(hfRaiseID.Value));
                            //Ishwar Patil : Training Module : 03/09/2014 End
                            //lblConfirmMessage.Text = msg;
                            System.Text.StringBuilder javaScript = new System.Text.StringBuilder();
                            string scriptKey = "ConfirmationScript";
                            javaScript.Append("var userConfirmation = window.alert('" + msg + "');\n");
                            //javaScript.Append("location.href='RaiseTrainingSummary.aspx';");
                            javaScript.Append("location.href='RaiseTrainingSummary.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.SeminarsID.ToString()) + "';");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), scriptKey, javaScript.ToString(), true);
                        }
                        else
                        {
                            lblConfirmMessage.Text = "Seminar request submitted.";

                            //Ishwar Patil : Training Module : 21/08/2014 Start
                            SendMailForSeminar(RaiseTrainingCollection);
                            //Ishwar Patil : Training Module : 21/08/2014 End

                            ClearSeminarsControls();
                        }
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionbtnSaveSeminars_Click, EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Clears all the controls.
    /// </summary>
    private void ClearTechnicalControls()
    {
        try
        {
            //ddlTrainingType.SelectedIndex = 0;
            ddlTrainingNameTech.SelectedIndex = 0;
            txtTrainingTypeOtherTech.Text = "Name of the Training";
            ddlQuarter.SelectedIndex = 0;
            ddlNoofParticipants.SelectedIndex = 0;
            ddlCategoryTech.SelectedIndex = 0;
            ddlpriority.SelectedIndex = 0;
            txtBusinessImpact.Text = string.Empty;
            txtCommentsTechnical.Text = string.Empty;
            btnSave.Text = "Submit";
            hfRaiseID.Value = string.Empty;

            //Fetched WindowLogin Name
            GetMasterData_RequestByTechnicalDropDown();
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionClearTechnicalControls, EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Clears all KSS Training controls.
    /// </summary>
    private void ClearKSSControls()
    {
        try
        {
            //ddlTrainingType.SelectedIndex = 0;
            ddlType.SelectedIndex = 0;
            txtTopic.Text = string.Empty;
            txtAgenda.Text = string.Empty;
            ucDate.Text = string.Empty;
            txtPresenter.Text = string.Empty;
            txtCommentsKSS.Text = string.Empty;
            hfRaiseID.Value = string.Empty;
            btnSaveKSS.Text = "Submit";
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionClearKSSControls, EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Clears all Seminars Training controls.
    /// </summary>
    private void ClearSeminarsControls()
    {
        try
        {
            //ddlTrainingType.SelectedIndex = 0;
            txtSeminarsName.Text = string.Empty;
            ucSeminarsDate.Text = string.Empty;
            txtNameofParticipant.Text = string.Empty;
            txtURL.Text = string.Empty;
            txtCommentSeminars.Text = string.Empty;
            hfRaiseID.Value = string.Empty;
            btnSaveSeminars.Text = "Submit";
            //Fetch WindowLogin User Name after clear a data
            GetMasterData_RequestBySeminarsDropDown();
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionClearSeminarsControls, EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    protected void txtTrainingTypeOtherTech_OnTextChanged(object sender, EventArgs e)
    {
        saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
        RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
        string strname = txtTrainingTypeOtherTech.Text;
        if (ddlTrainingType.SelectedValue == CommonConstants.TechnicalTrainingID.ToString())
        {
            RaiseTrainingCollection.Category = CommonConstants.TrainingName;
        }
        else if (ddlTrainingType.SelectedValue == CommonConstants.SoftSkillsTrainingID.ToString())
        {
            RaiseTrainingCollection.Category = CommonConstants.SoftSkills;
        }
        RaiseTrainingCollection.TrainingNameOther = txtTrainingTypeOtherTech.Text;

        RaiseTrainingId = saveTrainingBL.CheckDuplication(RaiseTrainingCollection);
        if (RaiseTrainingId == 1)
        {
            td1OtherTech.Style.Add("display", "block");
            td2OtherTech.Style.Add("display", "block");
            txtTrainingTypeOtherTech.Text = "Name of the Training";
            lblMandatory.Text = strname + " training already exist in the training list.";
        }
        else
        {
            td1OtherTech.Style.Add("display", "block");
            td2OtherTech.Style.Add("display", "block");
            txtTrainingTypeOtherTech.Text = RaiseTrainingCollection.TrainingNameOther;
        }

    }

    protected void ddlTrainingType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTrainingType.SelectedValue != CommonConstants.SELECT)
            {
                if (Convert.ToInt32(ddlTrainingType.SelectedValue) == CommonConstants.TechnicalTrainingID)
                {
                    divTechnicalSkills.Visible = true;
                    divKSS.Visible = false;
                    divSeminars.Visible = false;

                    ddlTrainingNameTech.ClearSelection();
                    GetMasterData_TechnicalNameDropDown();

                    ClearTechnicalControls();
                }
                else if (Convert.ToInt32(ddlTrainingType.SelectedValue) == CommonConstants.SoftSkillsTrainingID)
                {
                    divTechnicalSkills.Visible = true;
                    divKSS.Visible = false;
                    divSeminars.Visible = false;

                    ddlTrainingNameTech.ClearSelection();
                    GetMasterData_SoftSkillsNameDropDown();
                    ClearTechnicalControls();
                }
                else if (Convert.ToInt32(ddlTrainingType.SelectedValue) == CommonConstants.KSSID)
                {
                    divTechnicalSkills.Visible = false;
                    divKSS.Visible = true;
                    divSeminars.Visible = false;

                    GetMasterData_KSSTrainingTypeDropDown();
                    //GetMasterData_PresenterDropDown();
                    ClearKSSControls();
                }
                else if (Convert.ToInt32(ddlTrainingType.SelectedValue) == CommonConstants.SeminarsID)
                {
                    divTechnicalSkills.Visible = false;
                    divKSS.Visible = false;
                    divSeminars.Visible = true;

                    ClearSeminarsControls();
                    GetMasterData_RequestBySeminarsDropDown();
                    //GetMasterData_NameOfParticipantsSeminarsDropDown();

                }
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctionddlTrainingType_SelectedIndexChanged, EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Get the Seminars Training Summary
    /// </summary>
    /// <returns></returns>
    private void GetSeminarsTraining(int RaiseID, string Action)
    {
        try
        {
            RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
            saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
            RaiseTrainingCollection = saveTrainingBL.GetSeminarsTraining(RaiseID);

            ddlTrainingType.SelectedValue = RaiseTrainingCollection.TrainingType;
            txtSeminarsName.Text = RaiseTrainingCollection.SeminarsName;
            ucSeminarsDate.Text = RaiseTrainingCollection.Date.ToString("dd/MM/yyyy");
            ddlRequestedBySeminars.SelectedValue = RaiseTrainingCollection.RequestedBy;
            txtURL.Text = RaiseTrainingCollection.URL;
            txtCommentSeminars.Text = RaiseTrainingCollection.Comments;
            txtNameofParticipant.Text = RaiseTrainingCollection.NameOfParticipant;
            hfNameofParticipant.Value = RaiseTrainingCollection.NameOfParticipant;
            hfID.Value = RaiseTrainingCollection.NameOfParticipantID;
            hfRaiseID.Value = RaiseTrainingCollection.RaiseID.ToString();
            btnSaveSeminars.Text = "Update";
            btnResetSeminars.Text = CommonConstants.Cancel;
            if (Action == CommonConstants.View)
            {
                //ddlTrainingType.Enabled = false;
                txtSeminarsName.Enabled = false;
                ucSeminarsDate.IsEnable = false;
                ddlRequestedBySeminars.Enabled = false;
                txtURL.Enabled = false;
                txtNameofParticipant.Enabled = false;
                txtCommentSeminars.Enabled = false;
                imgNameofParticipant.Disabled = true;
                img1.Disabled = true;
                btnSaveSeminars.Text = "Back";
                btnResetSeminars.Visible = false;
            }
            if (Action == CommonConstants.Update && RaiseTrainingCollection.StatusName == CommonConstants.Approved)
            {
                txtSeminarsName.Enabled = false;
            }
            ddlTrainingType.Enabled = false;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "FunctionGetSeminarsTraining", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Get the KSS Training Summary
    /// </summary>
    /// <returns></returns>
    private void GetKSSTraining(int RaiseID, string Action)
    {
        try
        {
            RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
            saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
            RaiseTrainingCollection = saveTrainingBL.GetKSSTraining(RaiseID);

            ddlTrainingType.SelectedValue = RaiseTrainingCollection.TrainingType;
            ddlType.SelectedValue = RaiseTrainingCollection.Type;
            txtTopic.Text = RaiseTrainingCollection.Topic;
            txtAgenda.Text = RaiseTrainingCollection.Agenda;
            txtPresenter.Text = RaiseTrainingCollection.Presenter;
            hfPresenter.Value = RaiseTrainingCollection.Presenter;
            hfID.Value = RaiseTrainingCollection.PresenterID;
            ucDate.Text = RaiseTrainingCollection.Date.ToString("dd/MM/yyyy");
            txtCommentsKSS.Text = RaiseTrainingCollection.Comments;
            hfRaiseID.Value = RaiseTrainingCollection.RaiseID.ToString();
            btnSaveKSS.Text = "Update";
            btnResetKSS.Text = CommonConstants.Cancel;
            if (Action == CommonConstants.View)
            {
                ddlType.Enabled = false;
                txtTopic.Enabled = false;
                txtAgenda.Enabled = false;
                txtPresenter.Enabled = false;
                ucDate.IsEnable = false;
                txtCommentsKSS.Enabled = false;
                btnSaveKSS.Text = "Back";
                btnResetKSS.Visible = false;
                imgPresenter.Disabled = true;
            }
            ddlTrainingType.Enabled = false;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "FunctionGetKSSTraining", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Get the Technical Training Summary
    /// </summary>
    /// <returns></returns>
    private void GetTechnicalSoftSkillsTraining(int RaiseID, int TrainingTypeID, string Action)
    {
        try
        {
            RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
            saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
            RaiseTrainingCollection = saveTrainingBL.GetTechnicalSoftSkills(RaiseID, TrainingTypeID);

            if (RaiseTrainingCollection.TrainingType == CommonConstants.TechnicalTrainingID.ToString())
            {
                ddlTrainingNameTech.ClearSelection();
                GetMasterData_TechnicalNameDropDown();
            }
            else if (RaiseTrainingCollection.TrainingType == CommonConstants.SoftSkillsTrainingID.ToString())
            {
                ddlTrainingNameTech.ClearSelection();
                GetMasterData_SoftSkillsNameDropDown();
            }

            ddlTrainingType.SelectedValue = RaiseTrainingCollection.TrainingType;
            ddlTrainingNameTech.SelectedValue = RaiseTrainingCollection.TrainingName;
            ddlQuarter.SelectedValue = RaiseTrainingCollection.Quarter;
            ddlNoofParticipants.SelectedValue = RaiseTrainingCollection.NoOfParticipant;
            ddlCategoryTech.SelectedValue = RaiseTrainingCollection.Category;
            ddlRequestByTechnical.SelectedValue = RaiseTrainingCollection.RequestedBy;
            ddlpriority.SelectedValue = RaiseTrainingCollection.Priority;
            txtBusinessImpact.Text = RaiseTrainingCollection.BusinessImpact;
            txtCommentsTechnical.Text = RaiseTrainingCollection.Comments;

            hfRaiseID.Value = RaiseTrainingCollection.RaiseID.ToString();
            btnSave.Text = "Update";
            btnReset.Text = CommonConstants.Cancel;
            if (ddlTrainingNameTech.SelectedItem.Text.ToLower() == CommonConstants.TrainingTypeOther)
            {
                td1OtherTech.Style.Add("display", "block");
                td2OtherTech.Style.Add("display", "block");
                txtTrainingTypeOtherTech.Text = RaiseTrainingCollection.TrainingNameOther;
            }
            else
            {
                td1OtherTech.Style.Add("display", "none");
                td2OtherTech.Style.Add("display", "none");
            }
            if (Action == CommonConstants.View)
            {
                //ddlTrainingType.Enabled = false;
                ddlTrainingNameTech.Enabled = false;
                ddlQuarter.Enabled = false;
                ddlNoofParticipants.Enabled = false;
                ddlCategoryTech.Enabled = false;
                ddlRequestByTechnical.Enabled = false;
                ddlpriority.Enabled = false;
                txtBusinessImpact.Enabled = false;
                txtCommentsTechnical.Enabled = false;
                btnSave.Text = "Back";
                btnReset.Visible = false;
            }
            if (Action == CommonConstants.Update && RaiseTrainingCollection.StatusName == CommonConstants.Approved)
            {
                //ddlTrainingType.Enabled = false;
                ddlTrainingNameTech.Enabled = false;
            }
            ddlTrainingType.Enabled = false;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "FunctionGetTechnicalSoftSkillsTraining", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Edit Training Details
    /// </summary>
    /// <returns></returns>
    private void UpdateAndViewTrainingDetails()
    {
        try
        {
            int pRaiseID = Convert.ToInt32(RaiseID);
            if (TrainingTypeID == CommonConstants.SeminarsID.ToString())
            {
                divTechnicalSkills.Visible = false;
                divKSS.Visible = false;
                divSeminars.Visible = true;

                GetMasterData_RequestBySeminarsDropDown();

                GetSeminarsTraining(pRaiseID, Action);
            }
            else if (TrainingTypeID == CommonConstants.KSSID.ToString())
            {
                divTechnicalSkills.Visible = false;
                divKSS.Visible = true;
                divSeminars.Visible = false;

                GetMasterData_KSSTrainingTypeDropDown();

                GetKSSTraining(pRaiseID, Action);
            }
            else if (TrainingTypeID == CommonConstants.TechnicalTrainingID.ToString())
            {
                divTechnicalSkills.Visible = true;
                divKSS.Visible = false;
                divSeminars.Visible = false;

                ddlTrainingNameTech.ClearSelection();
                GetMasterData_TechnicalNameDropDown();

                GetTechnicalSoftSkillsTraining(pRaiseID, CommonConstants.TechnicalTrainingID, Action);
            }
            else if (TrainingTypeID == CommonConstants.SoftSkillsTrainingID.ToString())
            {
                divTechnicalSkills.Visible = true;
                divKSS.Visible = false;
                divSeminars.Visible = false;

                ddlTrainingNameTech.ClearSelection();
                GetMasterData_SoftSkillsNameDropDown();

                GetTechnicalSoftSkillsTraining(pRaiseID, CommonConstants.SoftSkillsTrainingID, Action);
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "FunctionEditSeminarsDetails", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }
    //Ishwar Patil : Training Module : 19/08/2014 Start

    private void SendMailForTechSoftSkillEdit(int Raiseid)
    {
        try
        {
            IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                   Convert.ToInt16(EnumsConstants.EmailFunctionality.TechSoftSkillRequestEdit));
            string URL = string.Empty;
            if (Convert.ToInt32(ddlTrainingType.SelectedValue) == CommonConstants.TechnicalTrainingID)
            {
                URL = "http://RMS/RaiseTrainingSummary.aspx?TrainingType=MTIwNw~~";
            }
            else if (Convert.ToInt32(ddlTrainingType.SelectedValue) == CommonConstants.SoftSkillsTrainingID)
            {
                URL = "http://RMS/RaiseTrainingSummary.aspx?TrainingType=MTIwOA~~";
            }

            DataSet ds = saveTrainingBL.GetEmailIDDetails(ddlRequestByTechnical.SelectedValue.ToString(), Raiseid);

            if (ds.Tables[1].Rows[0]["Name"].ToString() == CommonConstants.Open.ToUpper().ToString())     //If Training Status is Open
            {
                if (ds.Tables[0].Rows.Count != 0)
                {
                    //For TO
                    obj.To.Add(CommonConstants.EmailIdOfRMOGroup);

                    //For CC
                    string EmailID = string.Empty;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        EmailID += ds.Tables[0].Rows[i]["LineManagerEMailID"].ToString() + ',';
                    }
                    EmailID += ds.Tables[0].Rows[0]["RequestRaiserEMailID"].ToString();
                    obj.CC.Add(EmailID);

                    string msg = ddlTrainingNameTech.SelectedItem.Text.ToLower() == CommonConstants.TrainingTypeOther ? txtTrainingTypeOtherTech.Text.ToString() : ddlTrainingNameTech.SelectedItem.ToString();

                    //For Body
                    //obj.Body = string.Format(obj.Body, CommonConstants.RMGroupName, GetHTMLForTableData(), ddlRequestByTechnical.SelectedItem.Text);
                    obj.Body = string.Format(obj.Body, CommonConstants.RMGroupName, msg, ddlRequestByTechnical.SelectedItem.Text, URL);

                    obj.SendEmail(obj);
                }
            }
            else if (ds.Tables[1].Rows[0]["Name"].ToString() == CommonConstants.Approved.ToUpper().ToString()) //RMGroup should update training when training stutus is approved.
            {
                if (ds.Tables[0].Rows.Count != 0)
                {
                    //For TO
                    obj.To.Add(ds.Tables[0].Rows[0]["RequestRaiserEMailID"].ToString());

                    //For CC
                    string EmailID = string.Empty;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        EmailID += ds.Tables[0].Rows[i]["LineManagerEMailID"].ToString() + ',';
                    }
                    EmailID += CommonConstants.EmailIdOfRMOGroup;
                    obj.CC.Add(EmailID);

                    string msg = ddlTrainingNameTech.SelectedItem.Text.ToLower() == CommonConstants.TrainingTypeOther ? txtTrainingTypeOtherTech.Text.ToString() : ddlTrainingNameTech.SelectedItem.ToString();
                    //For Body
                    //obj.Body = string.Format(obj.Body, ddlRequestByTechnical.SelectedItem.Text, GetHTMLForTableData(), CommonConstants.RMGroupName);
                    obj.Body = string.Format(obj.Body, ddlRequestByTechnical.SelectedItem.Text, msg, CommonConstants.RMGroupName, URL);

                    obj.SendEmail(obj);
                }
            }
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "SendMailForTechSoftSkill", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }
    private void SendMailForTechSoftSkill()
    {
        try
        {
            //If Line/Function Manager requesting a training
            if (UserEmpID == Convert.ToInt32(ddlRequestByTechnical.SelectedValue))
            {
                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                   Convert.ToInt16(EnumsConstants.EmailFunctionality.TechSoftSkillRequest));

                //obj.Subject = string.Format(obj.Subject);

                DataSet ds = saveTrainingBL.GetEmailIDDetails(ddlRequestByTechnical.SelectedValue.ToString(),CommonConstants.DefaultRaiseID);

                if (ds.Tables[0].Rows.Count != 0)
                {
                    //For TO
                    obj.To.Add(CommonConstants.EmailIdOfRMOGroup);

                    //For CC
                    string EmailID = string.Empty;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        EmailID += ds.Tables[0].Rows[i]["LineManagerEMailID"].ToString() + ',';
                    }
                    EmailID += ds.Tables[0].Rows[0]["RequestRaiserEMailID"].ToString();
                    obj.CC.Add(EmailID);

                    //For Body
                    obj.Body = string.Format(obj.Body, CommonConstants.RMGroupName, GetHTMLForTableData(), ddlRequestByTechnical.SelectedItem.Text);

                    obj.SendEmail(obj);
                }
            }
            //Behalf of Line/Function Manager RMGroup requesting a training.
            else
            {
                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                   Convert.ToInt16(EnumsConstants.EmailFunctionality.TechSoftSkillRequestbehalfManager));

                //obj.Subject = string.Format(obj.Subject);

                DataSet ds = saveTrainingBL.GetEmailIDDetails(ddlRequestByTechnical.SelectedValue.ToString(),CommonConstants.DefaultRaiseID);

                if (ds.Tables[0].Rows.Count != 0)
                {
                    //For TO
                    obj.To.Add(ds.Tables[0].Rows[0]["RequestRaiserEMailID"].ToString());

                    //For CC
                    string EmailID = string.Empty;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        EmailID += ds.Tables[0].Rows[i]["LineManagerEMailID"].ToString() + ',';
                    }
                    EmailID += CommonConstants.EmailIdOfRMOGroup;
                    obj.CC.Add(EmailID);

                    //For Body
                    obj.Body = string.Format(obj.Body, ds.Tables[0].Rows[0]["RequestRaiserName"].ToString(), GetHTMLForTableData(), CommonConstants.RMGroupName);

                    obj.SendEmail(obj);
                }
            }
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "SendMailForTechSoftSkill", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    private void SendMailForKSSEdit(int Raiseid)
    {
        try
        {
            IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
               Convert.ToInt16(EnumsConstants.EmailFunctionality.KSSRequestEdit));
            string URL = string.Empty;
            URL = "http://RMS/RaiseTrainingSummary.aspx?TrainingType=MTIxMA~~";

            DataTable dt = saveTrainingBL.GetEmailIDDetailsForKSS(RaiseTrainingCollection.PresenterID);

            if (dt.Rows.Count != 0)
            {

                //For TO
                obj.To.Add(CommonConstants.EmailIdOfRMOGroup);

                //For CC
                string EmailID = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    EmailID += dt.Rows[i]["RequestRaiserEMailID"].ToString() + ',';
                }
                EmailID += UserMailId + "," + CommonConstants.EmailIdForKSS;

                obj.CC.Add(EmailID);

                //For Regards 
                string[] UserName;
                string RegardsUserName = string.Empty;
                char separator = Convert.ToChar(System.Configuration.ConfigurationManager.AppSettings["SplitCharacter"]);
                UserName = UserMailId.Split('@');
                if (UserName[0].Contains(separator.ToString()))
                {
                    UserName = UserName[0].Split(separator);
                    for (int i = 0; i < UserName.Length; i++)
                    {
                        RegardsUserName = RegardsUserName + " " + UserName[i];
                    }
                }

                //For Body
                obj.Body = string.Format(obj.Body, CommonConstants.RMGroupName, ddlType.SelectedItem.Text, RegardsUserName, URL);

                obj.SendEmail(obj);
            }
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "FunctionSendMailForKSSEdit", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }
    private void SendMailForKSS(BusinessEntities.RaiseTrainingRequest RaiseTrainingCollection)
    {
        try
        {
            IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
               Convert.ToInt16(EnumsConstants.EmailFunctionality.KSSRequest));

            DataTable dt = saveTrainingBL.GetEmailIDDetailsForKSS(RaiseTrainingCollection.PresenterID);

            if (dt.Rows.Count != 0)
            {

                //For TO
                obj.To.Add(CommonConstants.EmailIdOfRMOGroup);

                //For CC
                string EmailID = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    EmailID += dt.Rows[i]["RequestRaiserEMailID"].ToString() + ',';
                }
                EmailID += UserMailId + "," + CommonConstants.EmailIdForKSS;

                obj.CC.Add(EmailID);

                //For Regards 
                string[] UserName;
                string RegardsUserName = string.Empty;
                char separator = Convert.ToChar(System.Configuration.ConfigurationManager.AppSettings["SplitCharacter"]);
                UserName = UserMailId.Split('@');
                if (UserName[0].Contains(separator.ToString()))
                {
                    UserName = UserName[0].Split(separator);
                    for (int i = 0; i < UserName.Length; i++)
                    {
                        RegardsUserName = RegardsUserName + " " + UserName[i];
                    }
                }

                //For Body
                obj.Body = string.Format(obj.Body, CommonConstants.RMGroupName, GetHTMLForTableDataForKSS(), RegardsUserName);

                obj.SendEmail(obj);
            }
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "FunctionSendMailForKSS", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    private void SendMailForSeminarEdit(int Raiseid)
    {
        try
        {
            IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                   Convert.ToInt16(EnumsConstants.EmailFunctionality.SeminarsRequestEdit));

            string URL = string.Empty;
            URL = "http://RMS/RaiseTrainingSummary.aspx?TrainingType=MTIwOQ~~";

            DataSet ds = saveTrainingBL.GetEmailIDDetails(ddlRequestedBySeminars.SelectedValue, Raiseid);

            if (ds.Tables[1].Rows[0]["Name"].ToString() == CommonConstants.Open.ToUpper().ToString())     //If Training Status is Open
            {
                if (ds.Tables[0].Rows.Count != 0)
                {
                    //For TO
                    obj.To.Add(CommonConstants.EmailIdOfRMOGroup);

                    //For CC
                    string EmailID = string.Empty;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        EmailID += ds.Tables[0].Rows[i]["LineManagerEMailID"].ToString() + ',';
                    }
                    EmailID += ds.Tables[0].Rows[0]["RequestRaiserEMailID"].ToString();
                    obj.CC.Add(EmailID);
                    
                    //For Body
                    obj.Body = string.Format(obj.Body, CommonConstants.RMGroupName, txtSeminarsName.Text, ddlRequestedBySeminars.SelectedItem.Text, URL);

                    obj.SendEmail(obj);
                }
            }
            else if (ds.Tables[1].Rows[0]["Name"].ToString() == CommonConstants.Approved.ToUpper().ToString()) //RMGroup should update training when training stutus is approved.
            {
                if (ds.Tables[0].Rows.Count != 0)
                {
                    //For TO
                    obj.To.Add(ds.Tables[0].Rows[0]["RequestRaiserEMailID"].ToString());

                    //For CC
                    string EmailID = string.Empty;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        EmailID += ds.Tables[0].Rows[i]["LineManagerEMailID"].ToString() + ',';
                    }
                    EmailID += CommonConstants.EmailIdOfRMOGroup;
                    obj.CC.Add(EmailID);

                    //For Body
                    obj.Body = string.Format(obj.Body, ds.Tables[0].Rows[0]["RequestRaiserName"].ToString(), txtSeminarsName.Text, CommonConstants.RMGroupName, URL);

                    obj.SendEmail(obj);
                }
            }
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "FunctionSendMailForSeminarEdit", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }
    private void SendMailForSeminar(BusinessEntities.RaiseTrainingRequest RaiseTrainingCollection)
    {
        try
        {
            IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
               Convert.ToInt16(EnumsConstants.EmailFunctionality.SeminarRequest));

            DataSet ds = saveTrainingBL.GetEmailIDDetails(ddlRequestedBySeminars.SelectedValue,CommonConstants.DefaultRaiseID);

            if (ds.Tables[0].Rows.Count != 0)
            {

                //For TO--RMGroup
                obj.To.Add(CommonConstants.EmailIdOfRMOGroup);

                //For CC--Line manager of Request Raiser, Request Raiser
                string EmailID = string.Empty;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    EmailID += ds.Tables[0].Rows[i]["LineManagerEMailID"].ToString() + ',';
                }
                EmailID += ds.Tables[0].Rows[0]["RequestRaiserEMailID"].ToString();

                obj.CC.Add(EmailID);

                //For Body
                obj.Body = string.Format(obj.Body, CommonConstants.RMGroupName, GetHTMLForTableDataForSeminar(), ddlRequestedBySeminars.SelectedItem.Text);

                obj.SendEmail(obj);
            }
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "FunctionSendMailForSeminar", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    private string GetHTMLForTableData()
    {

        try
        {
            //list for table values
            List<string> objListTrainingDetail = new List<string>();
            if (ddlTrainingNameTech.SelectedItem.Text.ToLower() == CommonConstants.TrainingTypeOther)
            {
                objListTrainingDetail.Add(txtTrainingTypeOtherTech.Text);
            }
            else
            {
                objListTrainingDetail.Add(ddlTrainingNameTech.SelectedItem.Text);
            }
            objListTrainingDetail.Add(ddlQuarter.SelectedItem.Text);
            objListTrainingDetail.Add(ddlpriority.SelectedItem.Text);

            string[,] arrayData = new string[3, 2];

            if (objListTrainingDetail.Count > 0)
            {
                //Header Values
                arrayData[0, 0] = "Name of Training";
                arrayData[1, 0] = "Quarter";
                arrayData[2, 0] = "Priority";

                //Row Details
                for (int i = 0; i <= objListTrainingDetail.Count - 1; i++)
                {
                    arrayData[i, 1] = objListTrainingDetail[i];
                }

            }

            IEmailTableData objEmailTableData = new EmailTableData();
            objEmailTableData.RowDetail = arrayData;
            objEmailTableData.RowCount = objListTrainingDetail.Count;
            return objEmailTableData.GetVerticalHeaderTableData(objEmailTableData);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "FunctionGetHTMLForTableData", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    private string GetHTMLForTableDataForKSS()
    {

        try
        {
            //list for table values
            List<string> objListTrainingDetail = new List<string>();
            objListTrainingDetail.Add(ddlType.SelectedItem.Text);
            objListTrainingDetail.Add(txtTopic.Text);
            objListTrainingDetail.Add(ucDate.Text);

            string[,] arrayData = new string[3, 2];

            if (objListTrainingDetail.Count > 0)
            {
                //Header Values
                arrayData[0, 0] = "Type";
                arrayData[1, 0] = "Topic";
                arrayData[2, 0] = "Date";

                //Row Details
                for (int i = 0; i <= objListTrainingDetail.Count - 1; i++)
                {
                    arrayData[i, 1] = objListTrainingDetail[i];
                }

            }

            IEmailTableData objEmailTableData = new EmailTableData();
            objEmailTableData.RowDetail = arrayData;
            objEmailTableData.RowCount = objListTrainingDetail.Count;
            return objEmailTableData.GetVerticalHeaderTableData(objEmailTableData);
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "FunctionGetHTMLForTableDataForKSS", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    private string GetHTMLForTableDataForSeminar()
    {
        try
        {
            //list for table values
            List<string> objListTrainingDetail = new List<string>();
            objListTrainingDetail.Add(txtSeminarsName.Text);
            objListTrainingDetail.Add(ucSeminarsDate.Text);
            objListTrainingDetail.Add("Open");

            string[,] arrayData = new string[3, 2];

            if (objListTrainingDetail.Count > 0)
            {
                //Header Values
                arrayData[0, 0] = "Name of Seminar";
                arrayData[1, 0] = "Date";
                arrayData[2, 0] = "Status";

                //Row Details
                for (int i = 0; i <= objListTrainingDetail.Count - 1; i++)
                {
                    arrayData[i, 1] = objListTrainingDetail[i];
                }

            }

            IEmailTableData objEmailTableData = new EmailTableData();
            objEmailTableData.RowDetail = arrayData;
            objEmailTableData.RowCount = objListTrainingDetail.Count;
            return objEmailTableData.GetVerticalHeaderTableData(objEmailTableData);
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "FunctionGetHTMLForTableDataForSeminar", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }


    //Ishwar Patil : Training Module : 19/08/2014 End

    ///// <summary>
    ///// Validates the controls.
    ///// </summary>
    ///// <returns></returns>
    //private Boolean ValidateControls()
    //{
    //    Boolean Flag = false;
    //    Page page = HttpContext.Current.Handler as Page;

    //    if (FUFileUpload.HasFile)
    //    {
    //        if (FUFileUpload.FileBytes.Length > 2096927)
    //        {
    //            if (page != null)
    //            {
    //                Flag = false;
    //                ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Please Upload the File whose size is Less than 2MB.');", true);
    //            }
    //        }
    //        else
    //        {
    //            string fileExtension = System.IO.Path.GetExtension(FUFileUpload.FileName).ToLower();
    //            string[] allowedExtension = { ".doc", ".docx" };
    //            for (int i = 0; i < allowedExtension.Length; i++)
    //            {
    //                if (fileExtension == allowedExtension[i])
    //                {
    //                    Flag = true;
    //                }
    //            }
    //        }
    //    }
    //    if (!Flag)
    //    {
    //        if (page != null)
    //        {
    //            Flag = false;
    //            ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('Please Upload the valid File only in Doc and Docx format.');", true);
    //        }
    //    }
    //    return Flag;
    //}
}


