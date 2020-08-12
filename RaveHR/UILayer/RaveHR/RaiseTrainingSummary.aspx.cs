//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           RaiseTrainingRequest.aspx.cs       
//  Author:         Ishwar Patil
//  Date written:   28/04/2014
//  Description:    The Raise Training Summary page summarises All Training Details. You can Updated/Deleted Raise Training Details
//  Date                        Who             Ref     Description
//  ----                        -----------     ---     -----------
//  28/04/2014                  Ishwar.Patil    n/a     Created    
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

public partial class RaiseTrainingSummary : BaseClass
{
    #region Function Names
    const string FunctionPage_Load = "Page_Load";
    const string FunctionIBTechnicalPlus_Click = "IBTechnicalPlus_Click";
    const string FunctionIBSoftPlus_Click = "IBSoftPlus_Click";
    const string FunctionIBKSS_Click = "IBKSS_Click";
    const string FunctionIBSeminars_Click = "IBSeminars_Click";
    const string FunctionSoftSkillsTrainingGridView = "SoftSkillsTrainingGridView";
    const string FunctionGetMasterData_FilterStatusDropDown = "GetMasterData_FilterStatusDropDown";
    const string FunctionGetMasterData_FilterTrainingTypeDropDown = "GetMasterData_FilterTrainingTypeDropDown";
    const string FunctionGetMasterData_FilterQuarterDropDown = "GetMasterData_FilterQuarterDropDown";
    const string FunctionGetMasterData_FilterRequestByDropDown = "GetMasterData_FilterRequestByDropDown";
    const string FunctionFilterDropdownBing = "FilterDropdownBing";
    const string FunctionbtnFilter_OnClick = "btnFilter_OnClick";
    #endregion

    private const string PAGENAME = "RaiseTrainingSummary.aspx";
    private const string Edit = "EditCommand";
    private const string View = "ViewCommand";
    private const string Delete = "DeleteCommand";
    private const string Close = "CloseCommand";
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    private const string PREVIOUS = "Previous";
    private const string NEXT = "Next";
    private const string RaiseID = "RaiseID";
    private const string StatusName = "StatusName";
    private const string PlusJPG = "plus.JPG";
    private const string MinusJPG = "minus.JPG";
    private const string sessionFlag = "sessionFlag";
    private const string sessionPageCount = "pageCount";
    //private const string SoftsessionCount = "SoftCount";
    private const string KSSsessionPageCount = "KSSpageCount";
    private const string SeminarsessionPageCount = "SeminarpageCount";
    private static string sortExpression = string.Empty;
    private string commandName = string.Empty;
    private string TrainingTypeID = string.Empty;
    private int pageCount = 0;
    private int CurrentIndexCount = 0;
    private int RaiseTrainingId;
    private string TrainingName = string.Empty;
    private string UserMailId;
    private int UserEmpID;
    private int EditCount = 0;
    private int AccessRightID;
    bool result = false;
    DataSet ds;

    Rave.HR.BusinessLayer.Training.RaiseTrainingRequest saveTrainingBL = null;
    Rave.HR.BusinessLayer.Employee.Employee addEmployeeBAL = null;
    Rave.HR.BusinessLayer.Common.Master objRaveHRMasterBAL = null;
    BusinessEntities.ParameterCriteria objParameter = new BusinessEntities.ParameterCriteria();
    BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
    List<BusinessEntities.Master> objRaveHRMaster = null;
    private static BusinessEntities.RaiseTrainingRequest RaiseTrainingCollection;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString[QueryStringConstants.TrainingType] != null)
            {
                TrainingTypeID = DecryptQueryString(QueryStringConstants.TrainingType).ToString();
            }
            else
            {
                Session["FilterTrainingType"] = null;
                Session["FilterPriority"] = null;
                Session["FilterStatus"] = null;
                Session["FilterRequestBy"] = null;
                Session["FilterQuarter"] = null;
            }
        }

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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionPage_Load", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

        if (Session[SessionNames.CURRENT_PAGE_INDEX_TRAINING] == null)
            Session[SessionNames.CURRENT_PAGE_INDEX_TRAINING] = CommonConstants.DefaultFlagOne;

        if (Session[SessionNames.CURRENT_PAGE_INDEX_SOFTTRAINING] == null)
            Session[SessionNames.CURRENT_PAGE_INDEX_SOFTTRAINING] = CommonConstants.DefaultFlagOne;

        if (Session[SessionNames.CURRENT_PAGE_INDEX_KSSTRAINING] == null)
            Session[SessionNames.CURRENT_PAGE_INDEX_KSSTRAINING] = CommonConstants.DefaultFlagOne;

        if (Session[SessionNames.CURRENT_PAGE_INDEX_SeminarTRAINING] == null)
            Session[SessionNames.CURRENT_PAGE_INDEX_SeminarTRAINING] = CommonConstants.DefaultFlagOne;

        //if (Session[SessionNames.CURRENT_PAGE_INDEX_SOFTTRAINING] == null)
        //    Session[SessionNames.CURRENT_PAGE_INDEX_SOFTTRAINING] = CommonConstants.DefaultFlagOne;
        if (!Page.IsPostBack)
        {
            Session[sessionFlag] = CommonConstants.DefaultFlagOne;
            saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
            AccessRightID = saveTrainingBL.AccessForTrainingModule(UserEmpID);

            if (AccessRightID == 0)
            {
                Response.Redirect("Home.aspx");
            }
            else
            {
                GetMasterData_TrainingTypeRadioButton();
                FilterDropdownBing();
                BindGridView();
            }
        }
    }

    private void BindGridView()
    {
        if (CommonConstants.TechnicalTrainingID.ToString() == TrainingTypeID || CommonConstants.SoftSkillsTrainingID.ToString() == TrainingTypeID || CommonConstants.KSSID.ToString() == TrainingTypeID || CommonConstants.SeminarsID.ToString() == TrainingTypeID)
        {
            RBLTrainingType.SelectedValue = TrainingTypeID;
        }
        string RblTrainingType = RBLTrainingType.SelectedValue;
        if (RblTrainingType == CommonConstants.TechnicalTrainingID.ToString())
        {
            HideShowTechnicalTraining();
            lbltechsoftsummary.Text = "Technical";
            TechnicalTrainingGridView(sortExpression, ASCENDING);
        }
        else if (RblTrainingType == CommonConstants.SoftSkillsTrainingID.ToString())
        {
            HideShowSoftSkillsTraining();
            lbltechsoftsummary.Text = "Soft Skill";
            TechnicalTrainingGridView(sortExpression, ASCENDING);
        }
        else if (RblTrainingType == CommonConstants.KSSID.ToString())
        {
            HideShowKSSTraining();
            KSSTrainingGridView(sortExpression, ASCENDING);
        }
        else if (RblTrainingType == CommonConstants.SeminarsID.ToString())
        {
            HideShowSeminarsTraining();
            SeminarsTrainingGridView(sortExpression, ASCENDING);
        }
    }

    protected void RBLTrainingType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Session[sessionFlag] = CommonConstants.DefaultFlagZero;//For filter control value

        ddlFilterTrainingType.SelectedValue = RBLTrainingType.SelectedValue;
        ddlFilterPriority.SelectedValue = CommonConstants.DefaultFlagZero.ToString();
        ddlFilterStatus.SelectedIndex = 0;
        GetMasterData_FilterRequestByDropDown();
        ddlFilterQuarter.SelectedValue = CommonConstants.DefaultFlagZero.ToString();
        BindGridView();
    }

    //protected void ddlFilterStatus_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlFilterStatus.SelectedItem.Text == CommonConstants.Rejected.ToString())
    //    {
    //        ds = new DataSet();
    //        addEmployeeBAL = new Rave.HR.BusinessLayer.Employee.Employee();
    //        ds = addEmployeeBAL.GetActiveEmployeeList();
    //        ddlFilterRequestBy.DataSource = ds.Tables[0];
    //        ddlFilterRequestBy.DataTextField = ds.Tables[0].Columns[1].ToString();
    //        ddlFilterRequestBy.DataValueField = ds.Tables[0].Columns[0].ToString();
    //        ddlFilterRequestBy.DataBind();
    //        ddlFilterRequestBy.Items.Insert(0, "Select");
    //        ddlFilterRequestBy.Items.FindByValue(UserEmpID.ToString()).Selected = true;
    //        ddlFilterRequestBy.Enabled = false;
    //    }
    //}

    //protected void IBTechnicalPlus_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        HideShowTechnicalTraining();

    //        TechnicalTrainingGridView(sortExpression, ASCENDING);
    //        //ClearTechnicalControls();
    //    }
    //    catch (RaveHRException ex)
    //    {
    //        throw ex;
    //    }
    //    catch (Exception ex)
    //    {
    //        RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionIBTechnicalPlus_Click", EventIDConstants.TRAINING_PRESENTATION_LAYER);
    //        LogErrorMessage(objEx);
    //    }
    //}

    //protected void IBSoftPlus_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        HideShowSoftSkillsTraining();

    //        SoftSkillsTrainingGridView(sortExpression, ASCENDING);
    //        //ClearTechnicalControls();
    //    }
    //    catch (RaveHRException ex)
    //    {
    //        throw ex;
    //    }
    //    catch (Exception ex)
    //    {
    //        RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionIBSoftPlus_Click", EventIDConstants.TRAINING_PRESENTATION_LAYER);
    //        LogErrorMessage(objEx);
    //    }
    //}

    //protected void IBKSS_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        HideShowKSSTraining();
    //        KSSTrainingGridView(sortExpression, ASCENDING);
    //    }
    //    catch (RaveHRException ex)
    //    {
    //        throw ex;
    //    }
    //    catch (Exception ex)
    //    {
    //        RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionIBKSS_Click", EventIDConstants.TRAINING_PRESENTATION_LAYER);
    //        LogErrorMessage(objEx);
    //    }
    //}

    //protected void IBSeminars_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        HideShowSeminarsTraining();
    //        SeminarsTrainingGridView(sortExpression, ASCENDING);
    //        //ClearSeminarsControls();
    //    }
    //    catch (RaveHRException ex)
    //    {
    //        throw ex;
    //    }
    //    catch (Exception ex)
    //    {
    //        RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionIBSeminars_Click", EventIDConstants.TRAINING_PRESENTATION_LAYER);
    //        LogErrorMessage(objEx);
    //    }
    //}

    /// <summary>
    /// SoftSkillsChangePage event handler
    /// </summary>
    //protected void SoftSkillsChangePage(object sender, CommandEventArgs e)
    //{
    //    GridViewRow gvrPager = gvSoftSkillsTrainingSummary.BottomPagerRow;
    //    TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");

    //    switch (e.CommandName)
    //    {
    //        case PREVIOUS:
    //            if ((Convert.ToInt32(txtPages.Text) - 1) == 0)
    //            {
    //                Session[SessionNames.CURRENT_PAGE_INDEX_SOFTTRAINING] = 1;
    //                txtPages.Text = "1";
    //            }
    //            else
    //            {
    //                Session[SessionNames.CURRENT_PAGE_INDEX_SOFTTRAINING] = Convert.ToInt32(txtPages.Text) - 1;
    //                txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) - 1);
    //            }

    //            break;

    //        case NEXT:
    //            Session[SessionNames.CURRENT_PAGE_INDEX_SOFTTRAINING] = Convert.ToInt32(txtPages.Text) + 1;
    //            txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) + 1);
    //            break;
    //    }

    //    SoftSkillsTrainingGridView(sortExpression, ASCENDING);
    //}

    /// <summary>
    /// SoftSkillstxtPages_TextChanged event handler
    /// </summary>
    //protected void SoftSkillstxtPages_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        TextBox txtPages = (TextBox)sender;

    //        if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0 && Convert.ToInt32(txtPages.Text) <= Convert.ToInt32(Session[SoftsessionCount].ToString()))
    //        {
    //            gvSoftSkillsTrainingSummary.PageIndex = Convert.ToInt32(txtPages.Text) - 1;
    //            Session[SessionNames.CURRENT_PAGE_INDEX_SOFTTRAINING] = txtPages.Text;
    //        }
    //        else
    //        {
    //            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_SOFTTRAINING].ToString();
    //            return;
    //        }

    //        SoftSkillsTrainingGridView(sortExpression, ASCENDING);
    //        txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_SOFTTRAINING].ToString();
    //    }
    //    catch (RaveHRException ex)
    //    {
    //        LogErrorMessage(ex);
    //    }
    //    catch (Exception ex)
    //    {
    //        RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "SoftSkillstxtPages_TextChanged", EventIDConstants.TRAINING_PRESENTATION_LAYER);
    //        LogErrorMessage(objEx);
    //    }
    //}

    /// <summary>
    /// gridview gvSoftSkillsTrainingSummary_OnPageIndexChanging event handler
    /// </summary>
    //protected void gvSoftSkillsTrainingSummary_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    try
    //    {
    //        if (e.NewPageIndex != -1)
    //        {
    //            gvSoftSkillsTrainingSummary.PageIndex = e.NewPageIndex;
    //        }

    //        SoftSkillsTrainingGridView(sortExpression, ASCENDING);
    //    }
    //    catch (RaveHRException ex)
    //    {
    //        LogErrorMessage(ex);
    //    }
    //    catch (Exception ex)
    //    {
    //        RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "gvSoftSkillsTrainingSummary_OnPageIndexChanging", EventIDConstants.TRAINING_PRESENTATION_LAYER);
    //        LogErrorMessage(objEx);
    //    }
    //}

    /// <summary>
    /// gvSoftSkillsTrainingSummary_OnDataBound event handler
    /// </summary>
    //protected void gvSoftSkillsTrainingSummary_OnDataBound(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        GridViewRow gvrPager = gvSoftSkillsTrainingSummary.BottomPagerRow;

    //        if (gvrPager == null) return;

    //        TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
    //        Label lblPageCount = (Label)gvrPager.Cells[0].FindControl("lblPageCount");

    //        txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_SOFTTRAINING].ToString();

    //        if (lblPageCount != null)
    //            lblPageCount.Text = pageCount.ToString();
    //    }
    //    catch (RaveHRException ex)
    //    {
    //        LogErrorMessage(ex);
    //    }
    //    catch (Exception ex)
    //    {
    //        RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "gvSoftSkillsTrainingSummary_OnDataBound", EventIDConstants.TRAINING_PRESENTATION_LAYER);
    //        LogErrorMessage(objEx);
    //    }
    //}

    /// <summary>
    /// Sort the SoftSkills gridview as per SortExpression and SortDirection
    /// </summary>
    /// <param name="sortExpression"></param>
    /// <param name="direction"></param>
    //private void SoftSkillsTrainingGridView(string sortExpression, string direction)
    //{
    //    try
    //    {
    //        RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();

    //        RaiseTrainingCollection = GetFilterValues(Convert.ToInt32(Session[sessionFlag]));

    //        if (sortExpression == CommonConstants.MRF_CODE)
    //        {
    //            objParameter.SortExpressionAndDirection = sortExpression + direction;
    //        }
    //        else
    //        {
    //            objParameter.SortExpressionAndDirection = CommonConstants.TrainingCreatedDate + direction;
    //        }

    //        objParameter.PageNumber = Convert.ToInt32(Session[SessionNames.CURRENT_PAGE_INDEX_SOFTTRAINING].ToString());
    //        objParameter.PageSize = 5;

    //        raveHRCollection = ViewSoftSkillsTriningSummary(RaiseTrainingCollection);

    //        if ((int.Parse(Session[SessionNames.PAGE_COUNT_TRAINING].ToString()) == 1) && (Convert.ToInt32(RaiseTrainingCollection.Count) == 1))
    //        {
    //            gvSoftSkillsTrainingSummary.AllowSorting = false;
    //        }
    //        else
    //        {
    //            gvSoftSkillsTrainingSummary.AllowSorting = true;
    //        }

    //        if (Convert.ToInt32(raveHRCollection.Count) == 0)
    //        {
    //            gvSoftSkillsTrainingSummary.DataSource = raveHRCollection;
    //            gvSoftSkillsTrainingSummary.DataBind();

    //            ShowHeaderWhenEmptyGrid(raveHRCollection, gvSoftSkillsTrainingSummary);
    //        }
    //        else
    //        {
    //            gvSoftSkillsTrainingSummary.DataSource = raveHRCollection;
    //            gvSoftSkillsTrainingSummary.DataBind();
    //        }
    //        //Finds the Paging Text Box, Previous and Next Link Buttons in Grid Views Pagning Templet
    //        GridViewRow gvrPager = gvSoftSkillsTrainingSummary.BottomPagerRow;
    //        TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
    //        LinkButton lbtnPrevious = (LinkButton)gvrPager.Cells[0].FindControl("lbtnPrevious");
    //        LinkButton lbtnNext = (LinkButton)gvrPager.Cells[0].FindControl("lbtnNext");

    //        if (pageCount > 1)
    //        {
    //            gvSoftSkillsTrainingSummary.BottomPagerRow.Visible = true;
    //        }

    //        //Don't allow any character other than Number in paging text box
    //        txtPages.Attributes.Add("onkeypress", "return isNumberKey(event)");

    //        //Enable Prvious and Disable Next when Paging Text box contains last paging number
    //        if (Convert.ToInt32(txtPages.Text) == pageCount)
    //        {
    //            lbtnPrevious.Enabled = true;
    //            lbtnNext.Enabled = false;
    //        }
    //        //Enable Next and Disable Previous when Paging Text box contains First paging number i.e. 1
    //        if (Convert.ToInt32(txtPages.Text) == 1)
    //        {
    //            lbtnPrevious.Enabled = false;
    //            lbtnNext.Enabled = true;
    //        }
    //        //Enable both Next and Previous when Paging Text box contains paging number between 1 and Last page number
    //        if ((Convert.ToInt32(txtPages.Text) > 1) && (Convert.ToInt32(txtPages.Text) < pageCount))
    //        {
    //            lbtnPrevious.Enabled = true;
    //            lbtnNext.Enabled = true; ;
    //        }
    //        Session[SoftsessionCount] = pageCount;
    //    }
    //    //catches RaveHRException exception
    //    catch (RaveHRException ex)
    //    {
    //        throw ex;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionSoftSkillsTrainingGridView", EventIDConstants.TRAINING_PRESENTATION_LAYER);
    //    }
    //}

    /// <summary>
    /// Sort the KSS gridview as per SortExpression and SortDirection
    /// </summary>
    /// <param name="sortExpression"></param>
    /// <param name="direction"></param>
    private void KSSTrainingGridView(string sortExpression, string direction)
    {
        try
        {
            RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();

            if (TrainingTypeID == string.Empty)
            {
                RaiseTrainingCollection = GetFilterValues(Convert.ToInt32(Session[sessionFlag]));
            }
            else
            {
                RaiseTrainingCollection = GetSessionFilterValues();
            }
            //RaiseTrainingCollection = GetFilterValues(Convert.ToInt32(Session[sessionFlag]));

            if (sortExpression == CommonConstants.MRF_CODE)
            {
                objParameter.SortExpressionAndDirection = sortExpression + direction;
            }
            else
            {
                objParameter.SortExpressionAndDirection = CommonConstants.TrainingCreatedDate + direction;
            }

            objParameter.PageNumber = Convert.ToInt32(Session[SessionNames.CURRENT_PAGE_INDEX_KSSTRAINING].ToString());
            objParameter.PageSize = 10;

            raveHRCollection = ViewKSSTraining(RaiseTrainingCollection);

            if ((int.Parse(Session[SessionNames.PAGE_COUNT_KSSTRAINING].ToString()) == 1) && (Convert.ToInt32(RaiseTrainingCollection.Count) == 1))
            {
                gvKSSTrainingSummary.AllowSorting = false;
            }
            else
            {
                gvKSSTrainingSummary.AllowSorting = true;
            }

            if (Convert.ToInt32(raveHRCollection.Count) == 0)
            {
                DataTable dt = new DataTable();
                dt.Clear();
                dt.Columns.Add("Type");
                dt.Columns.Add("Topic");
                dt.Columns.Add("Date");
                dt.Columns.Add("CreatedDate");
                dt.Columns.Add("RaiseID");
                DataRow dr = dt.NewRow();
                dr["Type"] = CommonConstants.NO_RECORDS_FOUND_MESSAGE;
                dt.Rows.Add(dr);
                gvKSSTrainingSummary.DataSource = dt;
                gvKSSTrainingSummary.DataBind();
                for (int i = 1; i < gvKSSTrainingSummary.Rows[0].Cells.Count; i++)
                {
                    gvKSSTrainingSummary.Rows[0].Cells[i].Visible = false;
                }
                //gvKSSTrainingSummary.DataSource = raveHRCollection;
                //ShowHeaderWhenEmptyGrid(raveHRCollection, gvKSSTrainingSummary);
            }
            else
            {
                gvKSSTrainingSummary.DataSource = raveHRCollection;
                gvKSSTrainingSummary.DataBind();

                //Finds the Paging Text Box, Previous and Next Link Buttons in Grid Views Pagning Templet
                GridViewRow gvrPager = gvKSSTrainingSummary.BottomPagerRow;
                TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
                LinkButton lbtnPrevious = (LinkButton)gvrPager.Cells[0].FindControl("lbtnPrevious");
                LinkButton lbtnNext = (LinkButton)gvrPager.Cells[0].FindControl("lbtnNext");

                if (pageCount > 1)
                {
                    gvKSSTrainingSummary.BottomPagerRow.Visible = true;
                }

                //Don't allow any character other than Number in paging text box
                txtPages.Attributes.Add("onkeypress", "return isNumberKey(event)");

                //Enable Prvious and Disable Next when Paging Text box contains last paging number
                if (Convert.ToInt32(txtPages.Text) == pageCount)
                {
                    lbtnPrevious.Enabled = true;
                    lbtnNext.Enabled = false;
                }
                //Enable Next and Disable Previous when Paging Text box contains First paging number i.e. 1
                if (Convert.ToInt32(txtPages.Text) == 1)
                {
                    lbtnPrevious.Enabled = false;
                    lbtnNext.Enabled = true;
                }
                //Enable both Next and Previous when Paging Text box contains paging number between 1 and Last page number
                if ((Convert.ToInt32(txtPages.Text) > 1) && (Convert.ToInt32(txtPages.Text) < pageCount))
                {
                    lbtnPrevious.Enabled = true;
                    lbtnNext.Enabled = true; ;
                }
            }
            Session[KSSsessionPageCount] = pageCount;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionKSSTrainingGridView", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    private BusinessEntities.RaiseTrainingRequest GetSessionFilterValues()
    {
        if (Session["FilterTrainingType"] != null)
        {
            ddlFilterTrainingType.SelectedValue = Session["FilterTrainingType"].ToString();
        }
        if (Session["FilterPriority"] != null)
        {
            ddlFilterPriority.SelectedValue = Session["FilterPriority"].ToString();
        }
        if (Session["FilterStatus"] != null)
        {
            ddlFilterStatus.SelectedValue = Session["FilterStatus"].ToString();
        }
        if (Session["FilterRequestBy"] != null)
        {
            ddlFilterRequestBy.SelectedValue = Session["FilterRequestBy"].ToString();
        }
        if (Session["FilterQuarter"] != null)
        {
        ddlFilterQuarter.SelectedValue = Session["FilterQuarter"].ToString();
        }
        if (Session["FilterTrainingType"] != null)
        {
            RBLTrainingType.SelectedValue = Session["FilterTrainingType"].ToString();
        }
        if (ddlFilterTrainingType.SelectedItem.Value == CommonConstants.DefaultFlagZero.ToString())
        {
            RaiseTrainingCollection.TrainingType = CommonConstants.DefaultFlagZero.ToString();
        }
        else
        {
            RaiseTrainingCollection.TrainingType = ddlFilterTrainingType.SelectedItem.Value;
        }
        if (ddlFilterPriority.SelectedItem.Value == CommonConstants.DefaultFlagZero.ToString())
        {
            RaiseTrainingCollection.Priority = CommonConstants.DefaultFlagZero.ToString();
        }
        else
        {
            RaiseTrainingCollection.Priority = ddlFilterPriority.SelectedItem.Value;
        }

        if (ddlFilterStatus.SelectedItem.Value.ToUpper() == CommonConstants.SELECT)
        {
            RaiseTrainingCollection.Status = CommonConstants.DefaultFlagZero.ToString();
        }
        else
        {
            RaiseTrainingCollection.Status = ddlFilterStatus.SelectedItem.Value;
        }

        if (ddlFilterRequestBy.SelectedItem.Value.ToUpper() == CommonConstants.SELECT)
        {
            RaiseTrainingCollection.RequestedBy = CommonConstants.DefaultFlagZero.ToString();
        }
        else
        {
            RaiseTrainingCollection.RequestedBy = ddlFilterRequestBy.SelectedItem.Value;
        }

        if (ddlFilterQuarter.SelectedItem.Value.ToUpper() == CommonConstants.SELECT)
        {
            RaiseTrainingCollection.Quarter = CommonConstants.DefaultFlagZero.ToString();
        }
        else
        {
            RaiseTrainingCollection.Quarter = ddlFilterQuarter.SelectedItem.Value;
        }

        Session["FilterTrainingType"] = null;
        Session["FilterPriority"] = null;
        Session["FilterStatus"] = null;
        Session["FilterRequestBy"] = null;
        Session["FilterQuarter"] = null;

        return RaiseTrainingCollection;
    }

    /// <summary>
    /// get filter option control values
    /// </summary>
    private BusinessEntities.RaiseTrainingRequest GetFilterValues(int flag)
    {
        //0 = Filter option
        //1 = Radio Button option
        RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
        RaiseTrainingCollection.RaiseID = CommonConstants.DefaultRaiseID;
        RaiseTrainingCollection.UserEmpId = UserEmpID;
        if (flag == CommonConstants.DefaultFlagZero)
        {

            if (RBLTrainingType.SelectedValue == CommonConstants.TechnicalTrainingID.ToString() || RBLTrainingType.SelectedValue == CommonConstants.SoftSkillsTrainingID.ToString())
            {
                RaiseTrainingCollection.TrainingType = RBLTrainingType.SelectedValue;
            }
            else
            {
                RaiseTrainingCollection.TrainingType = CommonConstants.DefaultFlagZero.ToString();
            }
            RaiseTrainingCollection.Priority = CommonConstants.DefaultFlagZero.ToString();
            RaiseTrainingCollection.Status = CommonConstants.DefaultFlagZero.ToString();
            RaiseTrainingCollection.RequestedBy = CommonConstants.DefaultFlagZero.ToString();
            RaiseTrainingCollection.Quarter = CommonConstants.DefaultFlagZero.ToString();
        }
        else
        {
            if (ddlFilterTrainingType.SelectedItem.Value.ToUpper() == CommonConstants.SELECT)
            {
                if (flag == CommonConstants.DefaultFlagOne)
                {
                    RaiseTrainingCollection.TrainingType = RBLTrainingType.SelectedValue;
                }
                else
                {
                    RaiseTrainingCollection.TrainingType = CommonConstants.DefaultFlagZero.ToString();
                }
            }
            else
            {
                if (flag == CommonConstants.DefaultFlagMinus)
                {
                    RaiseTrainingCollection.TrainingType = ddlFilterTrainingType.SelectedItem.Value;
                }
                else
                {
                    RaiseTrainingCollection.TrainingType = RBLTrainingType.SelectedItem.Value;
                }
            }

            if (ddlFilterPriority.SelectedItem.Value == CommonConstants.DefaultFlagZero.ToString())
            {
                RaiseTrainingCollection.Priority = CommonConstants.DefaultFlagZero.ToString();
            }
            else
            {
                RaiseTrainingCollection.Priority = ddlFilterPriority.SelectedItem.Value;
            }

            if (ddlFilterStatus.SelectedItem.Value.ToUpper() == CommonConstants.SELECT)
            {
                RaiseTrainingCollection.Status = CommonConstants.DefaultFlagZero.ToString();
            }
            else
            {
                RaiseTrainingCollection.Status = ddlFilterStatus.SelectedItem.Value;
            }

            if (ddlFilterRequestBy.SelectedItem.Value.ToUpper() == CommonConstants.SELECT)
            {
                RaiseTrainingCollection.RequestedBy = CommonConstants.DefaultFlagZero.ToString();
            }
            else
            {
                if (flag == CommonConstants.DefaultFlagOne)
                {
                    RaiseTrainingCollection.RequestedBy = CommonConstants.DefaultFlagZero.ToString();
                }
                else
                {
                    RaiseTrainingCollection.RequestedBy = ddlFilterRequestBy.SelectedItem.Value;
                }
            }

            if (ddlFilterQuarter.SelectedItem.Value.ToUpper() == CommonConstants.SELECT)
            {
                RaiseTrainingCollection.Quarter = CommonConstants.DefaultFlagZero.ToString();
            }
            else
            {
                RaiseTrainingCollection.Quarter = ddlFilterQuarter.SelectedItem.Value;
            }
        }
        return RaiseTrainingCollection;
    }

    /// <summary>
    /// Sort the gridview as per SortExpression and SortDirection
    /// </summary>
    /// <param name="sortExpression"></param>
    /// <param name="direction"></param>
    private void TechnicalTrainingGridView(string sortExpression, string direction)
    {
        try
        {
            RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
            //RaiseTrainingCollection = GetFilterValues(Convert.ToInt32(Session[sessionFlag]));
            if (TrainingTypeID == string.Empty)
            {
                RaiseTrainingCollection = GetFilterValues(Convert.ToInt32(Session[sessionFlag]));
            }
            else
            {
                RaiseTrainingCollection = GetSessionFilterValues();
            }

            if (sortExpression == CommonConstants.TrainingCreatedDate)
            {
                objParameter.SortExpressionAndDirection = sortExpression + direction;
            }
            else
            {
                objParameter.SortExpressionAndDirection = CommonConstants.TrainingCreatedDate + direction;
            }

            if (RBLTrainingType.SelectedValue == CommonConstants.TechnicalTrainingID.ToString() || ddlFilterTrainingType.SelectedItem.Value.ToUpper() == CommonConstants.SELECT)
            {
                objParameter.PageNumber = Convert.ToInt32(Session[SessionNames.CURRENT_PAGE_INDEX_TRAINING].ToString());
            }
            else if (RBLTrainingType.SelectedValue == CommonConstants.SoftSkillsTrainingID.ToString())
            {
                objParameter.PageNumber = Convert.ToInt32(Session[SessionNames.CURRENT_PAGE_INDEX_SOFTTRAINING].ToString());
            }

            objParameter.PageSize = 10;

            raveHRCollection = ViewTechnicalTriningSummary(RaiseTrainingCollection);

            if ((int.Parse(Session[SessionNames.PAGE_COUNT_TRAINING].ToString()) == 1) && (Convert.ToInt32(RaiseTrainingCollection.Count) == 1))
            {
                gvTechnicalTrainingSummary.AllowSorting = false;
            }
            else
            {
                gvTechnicalTrainingSummary.AllowSorting = true;
            }

            if (Convert.ToInt32(raveHRCollection.Count) == 0)
            {
                //gvTechnicalTrainingSummary.DataSource = raveHRCollection;
                //gvTechnicalTrainingSummary.DataBind();
                DataTable dt = new DataTable();
                dt.Clear();
                dt.Columns.Add("TrainingName");
                dt.Columns.Add("Quarter");
                dt.Columns.Add("Priority");
                dt.Columns.Add("Status");
                dt.Columns.Add("RequestedBy");
                dt.Columns.Add("CreatedDate");
                dt.Columns.Add("RaiseID");
                dt.Columns.Add("RequestedByID");

                DataRow dr = dt.NewRow();
                dr["TrainingName"] = CommonConstants.NO_RECORDS_FOUND_MESSAGE;
                dt.Rows.Add(dr);
                gvTechnicalTrainingSummary.DataSource = dt;
                gvTechnicalTrainingSummary.DataBind();

                for (int i = 1; i < gvTechnicalTrainingSummary.Rows[0].Cells.Count; i++)
                {
                    gvTechnicalTrainingSummary.Rows[0].Cells[i].Visible = false;
                }
                //ShowHeaderWhenEmptyGrid(raveHRCollection, gvTechnicalTrainingSummary);
            }
            else
            {
                gvTechnicalTrainingSummary.DataSource = raveHRCollection;
                gvTechnicalTrainingSummary.DataBind();

                //Finds the Paging Text Box, Previous and Next Link Buttons in Grid Views Pagning Templet
                GridViewRow gvrPager = gvTechnicalTrainingSummary.BottomPagerRow;
                TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
                LinkButton lbtnPrevious = (LinkButton)gvrPager.Cells[0].FindControl("lbtnPrevious");
                LinkButton lbtnNext = (LinkButton)gvrPager.Cells[0].FindControl("lbtnNext");

                if (pageCount > 1)
                {
                    gvTechnicalTrainingSummary.BottomPagerRow.Visible = true;
                }

                //Don't allow any character other than Number in paging text box
                txtPages.Attributes.Add("onkeypress", "return isNumberKey(event)");

                //Enable Prvious and Disable Next when Paging Text box contains last paging number
                if (Convert.ToInt32(txtPages.Text) == pageCount)
                {
                    lbtnPrevious.Enabled = true;
                    lbtnNext.Enabled = false;
                }
                //Enable Next and Disable Previous when Paging Text box contains First paging number i.e. 1
                if (Convert.ToInt32(txtPages.Text) == 1)
                {
                    lbtnPrevious.Enabled = false;
                    lbtnNext.Enabled = true;
                }
                //Enable both Next and Previous when Paging Text box contains paging number between 1 and Last page number
                if ((Convert.ToInt32(txtPages.Text) > 1) && (Convert.ToInt32(txtPages.Text) < pageCount))
                {
                    lbtnPrevious.Enabled = true;
                    lbtnNext.Enabled = true; ;
                }
            }
            Session[sessionPageCount] = pageCount;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionTechnicalTrainingGridView", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// View the Technical Training Summary
    /// </summary>
    /// <returns></returns>
    private BusinessEntities.RaveHRCollection ViewTechnicalTriningSummary(BusinessEntities.RaiseTrainingRequest RaiseTraining)
    {
        try
        {
            saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
            raveHRCollection = saveTrainingBL.ViewTechnicalTraining(RaiseTraining, objParameter, ref pageCount, ref CurrentIndexCount);

            Session[SessionNames.PAGE_COUNT_TRAINING] = pageCount;
            if (CurrentIndexCount != CommonConstants.DefaultFlagZero)
            {
                if (RBLTrainingType.SelectedValue == CommonConstants.TechnicalTrainingID.ToString())
                {
                    Session[SessionNames.CURRENT_PAGE_INDEX_TRAINING] = CurrentIndexCount;
                }
                else if (RBLTrainingType.SelectedValue == CommonConstants.SoftSkillsTrainingID.ToString())
                {
                    Session[SessionNames.CURRENT_PAGE_INDEX_SOFTTRAINING] = CurrentIndexCount;
                }
            }
            return raveHRCollection;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionViewTechnicalTriningSummary", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// View the SoftSkills Training Summary
    /// </summary>
    /// <returns></returns>
    private BusinessEntities.RaveHRCollection ViewSoftSkillsTriningSummary(BusinessEntities.RaiseTrainingRequest RaiseTraining)
    {
        try
        {
            saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
            raveHRCollection = saveTrainingBL.ViewSoftSkillsTraining(RaiseTraining, objParameter, ref pageCount);

            Session[SessionNames.PAGE_COUNT_TRAINING] = pageCount;
            return raveHRCollection;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionViewSoftSkillsTriningSummary", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// View the KSS Training Summary
    /// </summary>
    /// <returns></returns>
    private BusinessEntities.RaveHRCollection ViewKSSTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining)
    {
        try
        {
            saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
            raveHRCollection = saveTrainingBL.ViewKSSTraining(RaiseTraining, objParameter, ref pageCount, ref CurrentIndexCount);

            Session[SessionNames.PAGE_COUNT_KSSTRAINING] = pageCount;

            if (CurrentIndexCount != CommonConstants.DefaultFlagZero)
            {
                Session[SessionNames.CURRENT_PAGE_INDEX_KSSTRAINING] = CurrentIndexCount;
            }
            return raveHRCollection;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionViewKSSTraining", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    protected void gvSeminarsSummary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
            commandName = e.CommandName;
            if (e.CommandName == Delete)
            {
                RaiseTrainingCollection.RaiseID = int.Parse(e.CommandArgument.ToString());
                RaiseTrainingCollection.Status = "0";
                RaiseTrainingCollection.UserEmpId = UserEmpID;
                DeleteSeminarsTraining(RaiseTrainingCollection);
                //Ishwar Patil : Training Module : 02/09/2014 Start
                SendMailForSeminarDeleted(Convert.ToInt32(e.CommandArgument.ToString()));
                //Ishwar Patil : Training Module : 02/09/2014 End
            }
            else if (e.CommandName == Close)
            {
                //ViewState[RaiseTrainingId.ToString()] = CommonConstants.SeminarsID;
                //ViewState[RaiseID] = int.Parse(e.CommandArgument.ToString());
                //ViewState[StatusName] = CommonConstants.Closed;
                //CommentPanel.Visible = true;
                //lblMandatory.Text = string.Empty;
                RaiseTrainingCollection.RaiseID = int.Parse(e.CommandArgument.ToString());
                RaiseTrainingCollection.Status = CommonConstants.Closed;
                RaiseTrainingCollection.UserEmpId = UserEmpID;
                lblMandatory.Text = string.Empty;

                CloseTraining(RaiseTrainingCollection, Convert.ToInt32(RBLTrainingType.SelectedValue));
            }
            Session["FilterTrainingType"] = ddlFilterTrainingType.SelectedValue;
            Session["FilterPriority"] = ddlFilterPriority.SelectedValue;
            Session["FilterStatus"] = ddlFilterStatus.SelectedValue;
            if (ddlFilterRequestBy.SelectedItem.Value.ToUpper() == CommonConstants.SELECT)
            {
                Session["FilterRequestBy"] = CommonConstants.DefaultFlagZero.ToString();
            }
            else
            {
                Session["FilterRequestBy"] = ddlFilterRequestBy.SelectedItem.Value;
            }
            Session["FilterQuarter"] = ddlFilterQuarter.SelectedValue;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvSeminarsSummary_RowCommand", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    protected void gvSeminarsSummary_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
                DataTable dt = saveTrainingBL.GetEditKSSTrainingGroup(UserEmpID, CommonConstants.DefaultFlagZero.ToString());
                EditCount = Convert.ToInt32(dt.Rows.Count);
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("imgEdit");
                ImageButton btnView = (ImageButton)e.Row.FindControl("imgView");
                ImageButton btnDelete = (ImageButton)e.Row.FindControl("imgDelete");
                ImageButton btnClose = (ImageButton)e.Row.FindControl("imgClose");
                HiddenField hf = (HiddenField)e.Row.FindControl("hfgvRaiseID");
                HiddenField hfRequestby = (HiddenField)e.Row.FindControl("hfRequestByID");
                
                string StatusValue = string.Empty;
                if (hf.Value != "0" && hf.Value != string.Empty)
                {
                    btnEdit.Attributes[CommonConstants.EVENT_ONCLICK] = "location.href = 'RaiseTrainingRequest.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.SeminarsID.ToString()) + "&" + URLHelper.SecureParameters("RaiseID", hf.Value) + "&" + URLHelper.SecureParameters("Action", CommonConstants.Update) + "&" + URLHelper.SecureParameters("Page", CommonConstants.SummaryPage) + "'";
                    btnView.Attributes[CommonConstants.EVENT_ONCLICK] = "location.href = 'RaiseTrainingRequest.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.SeminarsID.ToString()) + "&" + URLHelper.SecureParameters("RaiseID", hf.Value) + "&" + URLHelper.SecureParameters("Action", CommonConstants.View) + "&" + URLHelper.SecureParameters("Page", CommonConstants.SummaryPage) + "'";

                    StatusValue = DataBinder.Eval(e.Row.DataItem, "Status").ToString();

                    if (EditCount == CommonConstants.DefaultFlagZero)
                    {
                        if (UserEmpID != Convert.ToInt32(hfRequestby.Value) || StatusValue != CommonConstants.Open)
                        {
                            btnEdit.ImageUrl = CommonConstants.ImageMinus;
                            btnDelete.ImageUrl = CommonConstants.ImageMinus;
                            btnEdit.Enabled = false;
                            btnDelete.Enabled = false;
                            btnEdit.ToolTip = string.Empty;
                            btnDelete.ToolTip = string.Empty;
                        }
                        gvSeminarsSummary.Columns[(gvSeminarsSummary.Columns.Count - 1)].Visible = false;
                    }
                    else
                    {
                        //if (StatusValue == CommonConstants.Rejected || StatusValue == CommonConstants.Cancelled)
                        if (StatusValue == CommonConstants.Rejected || StatusValue == CommonConstants.Closed)
                        {
                            btnEdit.ImageUrl = CommonConstants.ImageMinus;
                            btnDelete.ImageUrl = CommonConstants.ImageMinus;
                            btnClose.ImageUrl = CommonConstants.ImageMinus;
                            btnEdit.ToolTip = string.Empty;
                            btnDelete.ToolTip = string.Empty;
                            btnClose.ToolTip = string.Empty;
                            btnEdit.Enabled = false;
                            btnDelete.Enabled = false;
                            btnClose.Enabled = false;
                        }
                        else
                        {
                            if (StatusValue == CommonConstants.Open)
                            {
                                btnClose.ImageUrl = CommonConstants.ImageMinus;
                                btnClose.ToolTip = string.Empty;
                                btnClose.Enabled = false;
                            }
                            else
                            {
                                btnEdit.Enabled = true;
                                btnDelete.Enabled = true;
                            }
                        }
                    }
                    if (StatusValue == CommonConstants.Deleted)
                    {
                        btnEdit.ImageUrl = CommonConstants.ImageMinus;
                        btnDelete.ImageUrl = CommonConstants.ImageMinus;
                        btnClose.ImageUrl = CommonConstants.ImageMinus;
                        btnEdit.ToolTip = string.Empty;
                        btnDelete.ToolTip = string.Empty;
                        btnClose.ToolTip = string.Empty;
                        btnEdit.Enabled = false;
                        btnDelete.Enabled = false;
                        btnClose.Enabled = false;
                    }
                }
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvSeminarsSummary_OnRowDataBound", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    protected void gvKSSTrainingSummary_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
                DataTable dt = saveTrainingBL.GetEditKSSTrainingGroup(UserEmpID, CommonConstants.DefaultFlagOne.ToString());
                EditCount = Convert.ToInt32(dt.Rows.Count);
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnView = (ImageButton)e.Row.FindControl("imgView");
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("imgEdit");
                ImageButton btnDelete = (ImageButton)e.Row.FindControl("imgDelete");
                //ImageButton btnCancel = (ImageButton)e.Row.FindControl("imgCancel");
                HiddenField hf = (HiddenField)e.Row.FindControl("hfgvRaiseID");
                string StatusValue = string.Empty;

                if (hf.Value != "0" && hf.Value != string.Empty)
                {
                    btnEdit.Attributes[CommonConstants.EVENT_ONCLICK] = "location.href = 'RaiseTrainingRequest.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.KSSID.ToString()) + "&" + URLHelper.SecureParameters("RaiseID", hf.Value) + "&" + URLHelper.SecureParameters("Action", CommonConstants.Update) + "&" + URLHelper.SecureParameters("Page", CommonConstants.SummaryPage) + "'";
                    btnView.Attributes[CommonConstants.EVENT_ONCLICK] = "location.href = 'RaiseTrainingRequest.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.KSSID.ToString()) + "&" + URLHelper.SecureParameters("RaiseID", hf.Value) + "&" + URLHelper.SecureParameters("Action", CommonConstants.View) + "&" + URLHelper.SecureParameters("Page", CommonConstants.SummaryPage) + "'";

                    StatusValue = DataBinder.Eval(e.Row.DataItem, "Status").ToString();

                    if (EditCount == CommonConstants.DefaultFlagZero)
                    {
                        btnEdit.ImageUrl = CommonConstants.ImageMinus;
                        btnDelete.ImageUrl = CommonConstants.ImageMinus;
                        btnEdit.Enabled = false;
                        btnDelete.Enabled = false;
                        btnEdit.ToolTip = string.Empty;
                        btnDelete.ToolTip = string.Empty;

                        //gvKSSTrainingSummary.Columns[(gvKSSTrainingSummary.Columns.Count - 1)].Visible = false;
                    }
                    else
                    {
                        //if (StatusValue == CommonConstants.Cancelled)
                        if (StatusValue == CommonConstants.Closed)
                        {
                            btnEdit.ImageUrl = CommonConstants.ImageMinus;
                            btnDelete.ImageUrl = CommonConstants.ImageMinus;
                            //btnCancel.ImageUrl = CommonConstants.ImageMinus;
                            btnEdit.ToolTip = string.Empty;
                            btnDelete.ToolTip = string.Empty;
                            //btnCancel.ToolTip = string.Empty;
                            btnEdit.Enabled = false;
                            btnDelete.Enabled = false;
                            //btnCancel.Enabled = false;
                        }
                        else
                        {
                            btnEdit.Enabled = true;
                            btnDelete.Enabled = true;
                        }
                    }
                }
                if (StatusValue == CommonConstants.Deleted)
                {
                    btnEdit.ImageUrl = CommonConstants.ImageMinus;
                    btnDelete.ImageUrl = CommonConstants.ImageMinus;
                    //btnCancel.ImageUrl = CommonConstants.ImageMinus;
                    btnEdit.ToolTip = string.Empty;
                    btnDelete.ToolTip = string.Empty;
                    //btnCancel.ToolTip = string.Empty;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    //btnCancel.Enabled = false;
                }
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvKSSTrainingSummary_OnRowDataBound", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    protected void gvTechnicalTrainingSummary_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
                DataTable dt = saveTrainingBL.GetEditKSSTrainingGroup(UserEmpID, CommonConstants.DefaultFlagZero.ToString());
                EditCount = Convert.ToInt32(dt.Rows.Count);
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnView = (ImageButton)e.Row.FindControl("imgView");
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("imgEdit");
                ImageButton btnDelete = (ImageButton)e.Row.FindControl("imgDelete");
                ImageButton btnClose = (ImageButton)e.Row.FindControl("imgClose");
                HiddenField hf = (HiddenField)e.Row.FindControl("hfgvRaiseID");
                HiddenField hfRequestby = (HiddenField)e.Row.FindControl("hfRequestByID");
                
                string StatusValue = string.Empty;
                if (hf.Value != "0" && hf.Value != string.Empty)
                {
                    btnEdit.Attributes[CommonConstants.EVENT_ONCLICK] = "location.href = 'RaiseTrainingRequest.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.TechnicalTrainingID.ToString()) + "&" + URLHelper.SecureParameters("RaiseID", hf.Value) + "&" + URLHelper.SecureParameters("Action", CommonConstants.Update) + "&" + URLHelper.SecureParameters("Page", CommonConstants.SummaryPage) + "'";
                    btnView.Attributes[CommonConstants.EVENT_ONCLICK] = "location.href = 'RaiseTrainingRequest.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.TechnicalTrainingID.ToString()) + "&" + URLHelper.SecureParameters("RaiseID", hf.Value) + "&" + URLHelper.SecureParameters("Action", CommonConstants.View) + "&" + URLHelper.SecureParameters("Page", CommonConstants.SummaryPage) + "'";
                
                    StatusValue = DataBinder.Eval(e.Row.DataItem, "Status").ToString();

                    //For all Manager, ATG, STM users
                    if (EditCount == CommonConstants.DefaultFlagZero)
                    {
                        if (UserEmpID != Convert.ToInt32(hfRequestby.Value) || StatusValue != CommonConstants.Open)
                        {
                            btnEdit.ImageUrl = CommonConstants.ImageMinus;
                            btnDelete.ImageUrl = CommonConstants.ImageMinus;
                            btnEdit.ToolTip = string.Empty;
                            btnDelete.ToolTip = string.Empty;
                            btnEdit.Enabled = false;
                            btnDelete.Enabled = false;
                        }
                        gvTechnicalTrainingSummary.Columns[(gvTechnicalTrainingSummary.Columns.Count - 1)].Visible = false;
                    }
                    //For RMO Teams
                    else
                    {
                        //if (StatusValue == CommonConstants.Rejected || StatusValue == CommonConstants.Cancelled)
                        if (StatusValue == CommonConstants.Rejected || StatusValue == CommonConstants.Closed)
                        {
                            btnEdit.ImageUrl = CommonConstants.ImageMinus;
                            btnDelete.ImageUrl = CommonConstants.ImageMinus;
                            btnClose.ImageUrl = CommonConstants.ImageMinus;
                            btnEdit.ToolTip = string.Empty;
                            btnDelete.ToolTip = string.Empty;
                            btnClose.ToolTip = string.Empty;
                            btnEdit.Enabled = false;
                            btnDelete.Enabled = false;
                            btnClose.Enabled = false;
                        }
                        else
                        {
                            if (StatusValue == CommonConstants.Open)
                            {
                                btnClose.ImageUrl = CommonConstants.ImageMinus;
                                btnClose.ToolTip = string.Empty;
                                btnClose.Enabled = false;
                            }
                            else
                            {
                                btnEdit.Enabled = true;
                                btnDelete.Enabled = true;
                            }
                        }
                    }
                    //For all login users
                    if (StatusValue == CommonConstants.Deleted)
                    {
                        btnEdit.ImageUrl = CommonConstants.ImageMinus;
                        btnDelete.ImageUrl = CommonConstants.ImageMinus;
                        btnClose.ImageUrl = CommonConstants.ImageMinus;
                        btnEdit.ToolTip = string.Empty;
                        btnDelete.ToolTip = string.Empty;
                        btnClose.ToolTip = string.Empty;
                        btnEdit.Enabled = false;
                        btnDelete.Enabled = false;
                        btnClose.Enabled = false;
                    }
                }
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvTechnicalTrainingSummary_OnRowDataBound", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    //protected void gvSoftSkillsTrainingSummary_OnRowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    try
    //    {
    //        if (e.Row.RowType == DataControlRowType.Header)
    //        {
    //            saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
    //            DataTable dt = saveTrainingBL.GetEditKSSTrainingGroup(UserEmpID, CommonConstants.DefaultFlagZero.ToString());
    //            EditCount = Convert.ToInt32(dt.Rows.Count);
    //        }
    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            ImageButton btnView = (ImageButton)e.Row.FindControl("imgView");
    //            ImageButton btnEdit = (ImageButton)e.Row.FindControl("imgEdit");
    //            ImageButton btnDelete = (ImageButton)e.Row.FindControl("imgDelete");
    //            HiddenField hf = (HiddenField)e.Row.FindControl("hfgvRaiseID");
    //            HiddenField hfRequestby = (HiddenField)e.Row.FindControl("hfRequestByID");
    //            string StatusValue = string.Empty;
    //            if (hf.Value != "0" && hf.Value != string.Empty)
    //            {
    //                StatusValue = DataBinder.Eval(e.Row.DataItem, "Status").ToString();

    //                btnEdit.Attributes[CommonConstants.EVENT_ONCLICK] = "location.href = 'RaiseTrainingRequest.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.SoftSkillsTrainingID.ToString()) + "&" + URLHelper.SecureParameters("RaiseID", hf.Value) + "&" + URLHelper.SecureParameters("Action", CommonConstants.Update) + "&" + URLHelper.SecureParameters("Page", CommonConstants.SummaryPage) + "'";
    //                btnView.Attributes[CommonConstants.EVENT_ONCLICK] = "location.href = 'RaiseTrainingRequest.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.SoftSkillsTrainingID.ToString()) + "&" + URLHelper.SecureParameters("RaiseID", hf.Value) + "&" + URLHelper.SecureParameters("Action", CommonConstants.View) + "&" + URLHelper.SecureParameters("Page", CommonConstants.SummaryPage) + "'";

    //                if (EditCount == CommonConstants.DefaultFlagZero)
    //                {
    //                    if (UserEmpID != Convert.ToInt32(hfRequestby.Value) || StatusValue != CommonConstants.Open)
    //                    {
    //                        btnEdit.ImageUrl = CommonConstants.ImageMinus;
    //                        btnDelete.ImageUrl = CommonConstants.ImageMinus;
    //                        btnEdit.ToolTip = string.Empty;
    //                        btnDelete.ToolTip = string.Empty;
    //                        btnEdit.Enabled = false;
    //                        btnDelete.Enabled = false;
    //                    }
    //                }
    //                else
    //                {
    //                    if (StatusValue == CommonConstants.Rejected || StatusValue == CommonConstants.Cancelled)
    //                    {
    //                        btnEdit.ImageUrl = CommonConstants.ImageMinus;
    //                        btnDelete.ImageUrl = CommonConstants.ImageMinus;
    //                        btnEdit.ToolTip = string.Empty;
    //                        btnDelete.ToolTip = string.Empty;
    //                        btnEdit.Enabled = false;
    //                        btnDelete.Enabled = false;
    //                    }
    //                    else
    //                    {
    //                        btnEdit.Enabled = true;
    //                        btnDelete.Enabled = true;
    //                    }
    //                }
    //                if (StatusValue == CommonConstants.Deleted)
    //                {
    //                    btnEdit.ImageUrl = CommonConstants.ImageMinus;
    //                    btnDelete.ImageUrl = CommonConstants.ImageMinus;
    //                    btnEdit.ToolTip = string.Empty;
    //                    btnDelete.ToolTip = string.Empty;
    //                    btnEdit.Enabled = false;
    //                    btnDelete.Enabled = false;
    //                }
    //            }
    //        }
    //    }
    //    catch (RaveHRException ex)
    //    {
    //        throw ex;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvSoftSkillsTrainingSummary_OnRowDataBound", EventIDConstants.TRAINING_PRESENTATION_LAYER);
    //    }
    //}

    protected void gvTechnicalTrainingSummary_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            TechnicalTrainingGridView(sortExpression, ASCENDING);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvTechnicalTrainingSummary_Sorting", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    //protected void gvSoftSkillsTrainingSummary_Sorting(object sender, GridViewSortEventArgs e)
    //{
    //    try
    //    {
    //        SoftSkillsTrainingGridView(sortExpression, ASCENDING);
    //    }
    //    catch (RaveHRException ex)
    //    {
    //        throw ex;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvSoftSkillsTrainingSummary_Sorting", EventIDConstants.TRAINING_PRESENTATION_LAYER);
    //    }
    //}

    protected void gvKSSTrainingSummary_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            KSSTrainingGridView(sortExpression, ASCENDING);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvKSSTrainingSummary_Sorting", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    protected void gvSeminarsSummary_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            SeminarsTrainingGridView(sortExpression, ASCENDING);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvSeminarsSummary_Sorting", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// gvTechnicalTrainingSummary_OnDataBound event handler
    /// </summary>
    protected void gvTechnicalTrainingSummary_OnDataBound(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrPager = gvTechnicalTrainingSummary.BottomPagerRow;

            if (gvrPager == null) return;

            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            Label lblPageCount = (Label)gvrPager.Cells[0].FindControl("lblPageCount");

            if (RBLTrainingType.SelectedValue == CommonConstants.TechnicalTrainingID.ToString() || ddlFilterTrainingType.SelectedItem.Value.ToUpper() == CommonConstants.SELECT)
            {
                txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_TRAINING].ToString();
            }
            else if (RBLTrainingType.SelectedValue == CommonConstants.SoftSkillsTrainingID.ToString())
            {
                txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_SOFTTRAINING].ToString();
            }

            if (lblPageCount != null)
                lblPageCount.Text = pageCount.ToString();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "gvTechnicalTrainingSummary_OnDataBound", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// gridview pageIndexChanging event handler
    /// </summary>
    protected void gvTechnicalTrainingSummary_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //Change the page index on page index changing event and bind the grid
            if (e.NewPageIndex != -1)
            {
                gvTechnicalTrainingSummary.PageIndex = e.NewPageIndex;
            }

            TechnicalTrainingGridView(sortExpression, ASCENDING);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "gvTechnicalTrainingSummary_OnPageIndexChanging", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// txtPages_TextChanged event handler
    /// </summary>
    protected void txtPages_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txtPages = (TextBox)sender;

            if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0 && Convert.ToInt32(txtPages.Text) <= Convert.ToInt32(Session[sessionPageCount].ToString()))
            {
                gvTechnicalTrainingSummary.PageIndex = Convert.ToInt32(txtPages.Text) - 1;
                if (RBLTrainingType.SelectedValue == CommonConstants.TechnicalTrainingID.ToString() || ddlFilterTrainingType.SelectedItem.Value.ToUpper() == CommonConstants.SELECT)
                {
                    Session[SessionNames.CURRENT_PAGE_INDEX_TRAINING] = txtPages.Text;
                }
                else if (RBLTrainingType.SelectedValue == CommonConstants.SoftSkillsTrainingID.ToString())
                {
                    Session[SessionNames.CURRENT_PAGE_INDEX_SOFTTRAINING] = txtPages.Text;
                }
            }
            else
            {
                if (RBLTrainingType.SelectedValue == CommonConstants.TechnicalTrainingID.ToString() || ddlFilterTrainingType.SelectedItem.Value.ToUpper() == CommonConstants.SELECT)
                {
                    txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_TRAINING].ToString();
                }
                else if (RBLTrainingType.SelectedValue == CommonConstants.SoftSkillsTrainingID.ToString())
                {
                    txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_SOFTTRAINING].ToString();
                }
                return;
            }

            TechnicalTrainingGridView(sortExpression, ASCENDING);
            if (RBLTrainingType.SelectedValue == CommonConstants.TechnicalTrainingID.ToString() || ddlFilterTrainingType.SelectedItem.Value.ToUpper() == CommonConstants.SELECT)
            {
                txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_TRAINING].ToString();
            }
            else if (RBLTrainingType.SelectedValue == CommonConstants.SoftSkillsTrainingID.ToString())
            {
                txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_SOFTTRAINING].ToString();
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "txtPages_TextChanged", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// ChangePage event handler
    /// </summary>
    protected void ChangePage(object sender, CommandEventArgs e)
    {
        GridViewRow gvrPager = gvTechnicalTrainingSummary.BottomPagerRow;
        TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");

        switch (e.CommandName)
        {
            case PREVIOUS:
                if ((Convert.ToInt32(txtPages.Text) - 1) == 0)
                {
                    if (RBLTrainingType.SelectedValue == CommonConstants.TechnicalTrainingID.ToString() || ddlFilterTrainingType.SelectedItem.Value.ToUpper() == CommonConstants.SELECT)
                    {
                        Session[SessionNames.CURRENT_PAGE_INDEX_TRAINING] = 1;
                    }
                    else if (RBLTrainingType.SelectedValue == CommonConstants.SoftSkillsTrainingID.ToString())
                    {
                        Session[SessionNames.CURRENT_PAGE_INDEX_SOFTTRAINING] = 1;    
                    }
                    txtPages.Text = "1";
                }
                else
                {
                    if (RBLTrainingType.SelectedValue == CommonConstants.TechnicalTrainingID.ToString() || ddlFilterTrainingType.SelectedItem.Value.ToUpper() == CommonConstants.SELECT)
                    {
                        Session[SessionNames.CURRENT_PAGE_INDEX_TRAINING] = Convert.ToInt32(txtPages.Text) - 1;
                    }
                    else if (RBLTrainingType.SelectedValue == CommonConstants.SoftSkillsTrainingID.ToString())
                    {
                        Session[SessionNames.CURRENT_PAGE_INDEX_SOFTTRAINING] = Convert.ToInt32(txtPages.Text) - 1;
                    }
                    txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) - 1);
                }

                break;

            case NEXT:
                if (RBLTrainingType.SelectedValue == CommonConstants.TechnicalTrainingID.ToString() || ddlFilterTrainingType.SelectedItem.Value.ToUpper() == CommonConstants.SELECT)
                {
                    Session[SessionNames.CURRENT_PAGE_INDEX_TRAINING] = Convert.ToInt32(txtPages.Text) + 1;
                }
                else if (RBLTrainingType.SelectedValue == CommonConstants.SoftSkillsTrainingID.ToString())
                {
                    Session[SessionNames.CURRENT_PAGE_INDEX_SOFTTRAINING] = Convert.ToInt32(txtPages.Text) + 1;
                }
                txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) + 1);
                break;
        }

        TechnicalTrainingGridView(sortExpression, ASCENDING);
    }

    /// <summary>
    /// SeminarsChangePage event handler
    /// </summary>
    protected void SeminarsChangePage(object sender, CommandEventArgs e)
    {
        GridViewRow gvrPager = gvSeminarsSummary.BottomPagerRow;
        TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");

        switch (e.CommandName)
        {
            case PREVIOUS:
                if ((Convert.ToInt32(txtPages.Text) - 1) == 0)
                {
                    Session[SessionNames.CURRENT_PAGE_INDEX_SeminarTRAINING] = 1;
                    txtPages.Text = "1";
                }
                else
                {
                    Session[SessionNames.CURRENT_PAGE_INDEX_SeminarTRAINING] = Convert.ToInt32(txtPages.Text) - 1;
                    txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) - 1);
                }

                break;

            case NEXT:
                Session[SessionNames.CURRENT_PAGE_INDEX_SeminarTRAINING] = Convert.ToInt32(txtPages.Text) + 1;
                txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) + 1);
                break;
        }

        SeminarsTrainingGridView(sortExpression, ASCENDING);
    }

    /// <summary>
    /// SeminarstxtPages_TextChanged event handler
    /// </summary>
    protected void SeminarstxtPages_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txtPages = (TextBox)sender;

            if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0 && Convert.ToInt32(txtPages.Text) <= Convert.ToInt32(Session[SeminarsessionPageCount].ToString()))
            {
                gvSeminarsSummary.PageIndex = Convert.ToInt32(txtPages.Text) - 1;
                Session[SessionNames.CURRENT_PAGE_INDEX_SeminarTRAINING] = txtPages.Text;
            }
            else
            {
                txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_SeminarTRAINING].ToString();
                return;
            }

            SeminarsTrainingGridView(sortExpression, ASCENDING);
            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_SeminarTRAINING].ToString();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "SeminarstxtPages_TextChanged", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// gridview gvSeminarsSummary_OnPageIndexChanging event handler
    /// </summary>
    protected void gvSeminarsSummary_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //Change the page index on page index changing event and bind the grid
            if (e.NewPageIndex != -1)
            {
                gvSeminarsSummary.PageIndex = e.NewPageIndex;
            }

            SeminarsTrainingGridView(sortExpression, ASCENDING);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "gvSeminarsSummary_OnPageIndexChanging", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// gvSeminarsSummary_OnDataBound event handler
    /// </summary>
    protected void gvSeminarsSummary_OnDataBound(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrPager = gvSeminarsSummary.BottomPagerRow;

            if (gvrPager == null) return;

            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            Label lblPageCount = (Label)gvrPager.Cells[0].FindControl("lblPageCount");

            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_SeminarTRAINING].ToString();

            if (lblPageCount != null)
                lblPageCount.Text = pageCount.ToString();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "gvSeminarsSummary_OnDataBound", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// KSSChangePage event handler
    /// </summary>
    protected void KSSChangePage(object sender, CommandEventArgs e)
    {
        GridViewRow gvrPager = gvKSSTrainingSummary.BottomPagerRow;
        TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");

        switch (e.CommandName)
        {
            case PREVIOUS:
                if ((Convert.ToInt32(txtPages.Text) - 1) == 0)
                {
                    Session[SessionNames.CURRENT_PAGE_INDEX_KSSTRAINING] = 1;
                    txtPages.Text = "1";
                }
                else
                {
                    Session[SessionNames.CURRENT_PAGE_INDEX_KSSTRAINING] = Convert.ToInt32(txtPages.Text) - 1;
                    txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) - 1);
                }

                break;

            case NEXT:
                Session[SessionNames.CURRENT_PAGE_INDEX_KSSTRAINING] = Convert.ToInt32(txtPages.Text) + 1;
                txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) + 1);
                break;
        }

        KSSTrainingGridView(sortExpression, ASCENDING);
    }

    /// <summary>
    /// KSStxtPages_TextChanged event handler
    /// </summary>
    protected void KSStxtPages_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txtPages = (TextBox)sender;

            if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0 && Convert.ToInt32(txtPages.Text) <= Convert.ToInt32(Session[KSSsessionPageCount].ToString()))
            {
                gvKSSTrainingSummary.PageIndex = Convert.ToInt32(txtPages.Text) - 1;
                Session[SessionNames.CURRENT_PAGE_INDEX_KSSTRAINING] = txtPages.Text;
            }
            else
            {
                txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_KSSTRAINING].ToString();
                return;
            }

            KSSTrainingGridView(sortExpression, ASCENDING);
            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_KSSTRAINING].ToString();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "KSStxtPages_TextChanged", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// gridview gvKSSTrainingSummary_OnPageIndexChanging event handler
    /// </summary>
    protected void gvKSSTrainingSummary_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (e.NewPageIndex != -1)
            {
                gvKSSTrainingSummary.PageIndex = e.NewPageIndex;
            }

            KSSTrainingGridView(sortExpression, ASCENDING);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "gvKSSTrainingSummary_OnPageIndexChanging", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// gvgvKSSTrainingSummary_OnDataBound event handler
    /// </summary>
    protected void gvgvKSSTrainingSummary_OnDataBound(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrPager = gvKSSTrainingSummary.BottomPagerRow;

            if (gvrPager == null) return;

            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            Label lblPageCount = (Label)gvrPager.Cells[0].FindControl("lblPageCount");

            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_KSSTRAINING].ToString();

            if (lblPageCount != null)
                lblPageCount.Text = pageCount.ToString();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "gvgvKSSTrainingSummary_OnDataBound", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// View the Seminars Training Summary
    /// </summary>
    /// <returns>raveHRCollection</returns>
    private BusinessEntities.RaveHRCollection ViewSeminarsTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining)
    {
        try
        {
            saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
            raveHRCollection = saveTrainingBL.ViewSeminarsTraining(RaiseTraining, objParameter, ref pageCount, ref CurrentIndexCount);

            Session[SessionNames.PAGE_COUNT_SeminarTRAINING] = pageCount;
            if (CurrentIndexCount != CommonConstants.DefaultFlagZero)
            {
                Session[SessionNames.CURRENT_PAGE_INDEX_SeminarTRAINING] = CurrentIndexCount;
            }
            return raveHRCollection;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionViewSeminarsTraining", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    protected void gvTechnicalTrainingSummary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
            commandName = e.CommandName;
            if (e.CommandName == Delete)
            {
                RaiseTrainingCollection.RaiseID = int.Parse(e.CommandArgument.ToString());
                RaiseTrainingCollection.Status = "0";
                RaiseTrainingCollection.UserEmpId = UserEmpID;

                DeleteTechnicalSoftSkillsTraining(RaiseTrainingCollection, Convert.ToInt32(RBLTrainingType.SelectedValue));
                //Ishwar Patil : Training Module : 02/09/2014 Start
                SendMailForTechSoftSkillDeleted(Convert.ToInt32(e.CommandArgument.ToString()));
                //Ishwar Patil : Training Module : 02/09/2014 End
            }
            else if (e.CommandName == Close)
            {
                //ViewState[RaiseTrainingId.ToString()] = CommonConstants.TechnicalTrainingID;
                //ViewState[StatusName] = CommonConstants.Cancelled;
                //CommentPanel.Visible = true;

                RaiseTrainingCollection.RaiseID = int.Parse(e.CommandArgument.ToString());
                RaiseTrainingCollection.Status = CommonConstants.Closed;
                RaiseTrainingCollection.UserEmpId = UserEmpID;
                lblMandatory.Text = string.Empty;
                
                CloseTraining(RaiseTrainingCollection, Convert.ToInt32(RBLTrainingType.SelectedValue));
            }
            Session["FilterTrainingType"] = ddlFilterTrainingType.SelectedValue;
            Session["FilterPriority"] = ddlFilterPriority.SelectedValue;
            Session["FilterStatus"] = ddlFilterStatus.SelectedValue;
            if (ddlFilterRequestBy.SelectedItem.Value.ToUpper() == CommonConstants.SELECT)
            {
                Session["FilterRequestBy"] = CommonConstants.DefaultFlagZero.ToString();
            }
            else
            {
                Session["FilterRequestBy"] = ddlFilterRequestBy.SelectedItem.Value;
            }
            Session["FilterQuarter"] = ddlFilterQuarter.SelectedValue;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvTechnicalTrainingSummary_RowCommand", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    private void SendMailForTechSoftSkillDeleted(int Raiseid)
    {
        try
        {
            IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
               Convert.ToInt16(EnumsConstants.EmailFunctionality.TechSoftSkillRequestDeleted));

            DataSet dt = saveTrainingBL.GetEmailIDForAppRejTechSoftSkill(Raiseid);
            if (dt.Tables[0].Rows.Count != 0)
            {

                //For TO--RMGroup
                obj.To.Add(dt.Tables[0].Rows[0]["RequestRaiserEMailID"]);

                //For CC--Line manager of Request Raiser, Request Raiser
                string EmailID = string.Empty;
                for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                {
                    EmailID += dt.Tables[0].Rows[i]["LineManagerEMailID"].ToString() + ',';
                }
                EmailID += CommonConstants.EmailIdOfRMOGroup;
                obj.CC.Add(EmailID);

                //For Body
                obj.Body = string.Format(obj.Body, dt.Tables[1].Rows[0]["RequestRaiserName"], dt.Tables[0].Rows[0]["TrainingName"], CommonConstants.RMGroupName);

                obj.SendEmail(obj);
            }
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionSendMailForTechSoftSkillDeleted", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    private void SendMailForSeminarDeleted(int Raiseid)
    {
        try
        {
            IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
               Convert.ToInt16(EnumsConstants.EmailFunctionality.SeminarsRequestDeleted));

            DataSet dt = saveTrainingBL.GetEmailIDForAppRejTechSoftSkill(Raiseid);
            if (dt.Tables[0].Rows.Count != 0)
            {

                //For TO--RMGroup
                obj.To.Add(dt.Tables[0].Rows[0]["RequestRaiserEMailID"]);

                //For CC--Line manager of Request Raiser, Request Raiser
                string EmailID = string.Empty;
                for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                {
                    EmailID += dt.Tables[0].Rows[i]["LineManagerEMailID"].ToString() + ',';
                }
                EmailID += CommonConstants.EmailIdOfRMOGroup;
                obj.CC.Add(EmailID);

                //For Body
                obj.Body = string.Format(obj.Body, dt.Tables[1].Rows[0]["RequestRaiserName"], dt.Tables[0].Rows[0]["TrainingName"], CommonConstants.RMGroupName);

                obj.SendEmail(obj);
            }
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionSendMailForSeminarDeleted", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    //protected void gvSoftSkillsTrainingSummary_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    try
    //    {
    //        RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
    //        commandName = e.CommandName;
    //        if (e.CommandName == Delete)
    //        {
    //            RaiseTrainingCollection.RaiseID = int.Parse(e.CommandArgument.ToString());
    //            RaiseTrainingCollection.Status = "0";
    //            RaiseTrainingCollection.UserEmpId = UserEmpID;
    //            DeleteTechnicalSoftSkillsTraining(RaiseTrainingCollection, CommonConstants.SoftSkillsTrainingID);
    //        }
    //    }
    //    catch (RaveHRException ex)
    //    {
    //        throw ex;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvSoftSkillsTrainingSummary_RowCommand", EventIDConstants.TRAINING_PRESENTATION_LAYER);
    //    }
    //}

    protected void gvKSSTrainingSummary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
            commandName = e.CommandName;
            if (e.CommandName == Delete)
            {
                RaiseTrainingCollection.RaiseID = int.Parse(e.CommandArgument.ToString());
                RaiseTrainingCollection.Status = "0";
                RaiseTrainingCollection.UserEmpId = UserEmpID;
                DeleteKSSTraining(RaiseTrainingCollection);
            }
            //else if (e.CommandName == Cancel)
            //{
            //    ViewState[RaiseTrainingId.ToString()] = CommonConstants.KSSID;
            //    ViewState[RaiseID] = int.Parse(e.CommandArgument.ToString());
            //    ViewState[StatusName] = CommonConstants.Cancelled;
            //    lblMandatory.Text = string.Empty;

            //    CommentPanel.Visible = true;
            //}
            Session["FilterTrainingType"] = ddlFilterTrainingType.SelectedValue;
            Session["FilterPriority"] = ddlFilterPriority.SelectedValue;
            Session["FilterStatus"] = ddlFilterStatus.SelectedValue;
            if (ddlFilterRequestBy.SelectedItem.Value.ToUpper() == CommonConstants.SELECT)
            {
                Session["FilterRequestBy"] = CommonConstants.DefaultFlagZero.ToString();
            }
            else
            {
                Session["FilterRequestBy"] = ddlFilterRequestBy.SelectedItem.Value;
            }
            Session["FilterQuarter"] = ddlFilterQuarter.SelectedValue;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvKSSTrainingSummary_RowCommand", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Sort the Seminars gridview as per SortExpression and SortDirection
    /// </summary>
    /// <param name="sortExpression"></param>
    /// <param name="direction"></param>
    private void SeminarsTrainingGridView(string sortExpression, string direction)
    {
        try
        {
            RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();

            if (TrainingTypeID == string.Empty)
            {
                RaiseTrainingCollection = GetFilterValues(Convert.ToInt32(Session[sessionFlag]));
            }
            else
            {
                RaiseTrainingCollection = GetSessionFilterValues();
            }

            if (sortExpression == CommonConstants.MRF_CODE)
            {
                objParameter.SortExpressionAndDirection = sortExpression + direction;
            }
            else
            {
                objParameter.SortExpressionAndDirection = CommonConstants.TrainingCreatedDate + direction;
            }

            objParameter.PageNumber = Convert.ToInt32(Session[SessionNames.CURRENT_PAGE_INDEX_SeminarTRAINING].ToString());
            objParameter.PageSize = 10;

            raveHRCollection = ViewSeminarsTraining(RaiseTrainingCollection);

            if ((int.Parse(Session[SessionNames.PAGE_COUNT_SeminarTRAINING].ToString()) == 1) && (Convert.ToInt32(RaiseTrainingCollection.Count) == 1))
            {
                gvSeminarsSummary.AllowSorting = false;
            }
            else
            {
                gvSeminarsSummary.AllowSorting = true;
            }

            if (Convert.ToInt32(raveHRCollection.Count) == 0)
            {
                DataTable dt = new DataTable();
                dt.Clear();
                dt.Columns.Add("SeminarsName");
                dt.Columns.Add("Date");
                dt.Columns.Add("Status");
                dt.Columns.Add("RequestedBy");
                dt.Columns.Add("RequestedByID");
                dt.Columns.Add("CreatedDate");
                dt.Columns.Add("RaiseID");
                DataRow dr = dt.NewRow();
                dr["SeminarsName"] = CommonConstants.NO_RECORDS_FOUND_MESSAGE;
                dt.Rows.Add(dr);
                //gvSeminarsSummary.DataSource = raveHRCollection;
                gvSeminarsSummary.DataSource = dt;
                gvSeminarsSummary.DataBind();

                for (int i = 1; i < gvSeminarsSummary.Rows[0].Cells.Count; i++)
                {
                    gvSeminarsSummary.Rows[0].Cells[i].Visible = false;
                }
                //ShowHeaderWhenEmptyGrid(raveHRCollection, gvSeminarsSummary);
            }
            else
            {
                gvSeminarsSummary.DataSource = raveHRCollection;
                gvSeminarsSummary.DataBind();

                //Finds the Paging Text Box, Previous and Next Link Buttons in Grid Views Pagning Templet
                GridViewRow gvrPager = gvSeminarsSummary.BottomPagerRow;
                TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
                LinkButton lbtnPrevious = (LinkButton)gvrPager.Cells[0].FindControl("lbtnPrevious");
                LinkButton lbtnNext = (LinkButton)gvrPager.Cells[0].FindControl("lbtnNext");

                if (pageCount > 1)
                {
                    gvSeminarsSummary.BottomPagerRow.Visible = true;
                }

                //Don't allow any character other than Number in paging text box
                txtPages.Attributes.Add("onkeypress", "return isNumberKey(event)");

                //Enable Prvious and Disable Next when Paging Text box contains last paging number
                if (Convert.ToInt32(txtPages.Text) == pageCount)
                {
                    lbtnPrevious.Enabled = true;
                    lbtnNext.Enabled = false;
                }
                //Enable Next and Disable Previous when Paging Text box contains First paging number i.e. 1
                if (Convert.ToInt32(txtPages.Text) == 1)
                {
                    lbtnPrevious.Enabled = false;
                    lbtnNext.Enabled = true;
                }
                //Enable both Next and Previous when Paging Text box contains paging number between 1 and Last page number
                if ((Convert.ToInt32(txtPages.Text) > 1) && (Convert.ToInt32(txtPages.Text) < pageCount))
                {
                    lbtnPrevious.Enabled = true;
                    lbtnNext.Enabled = true; ;
                }
            }
            Session[SeminarsessionPageCount] = pageCount;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionSeminarsTrainingGridView", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Delete the Seminars Training Details
    /// </summary>
    /// <returns></returns>
    private void DeleteSeminarsTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining)
    {
        try
        {
            saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
            TrainingName = saveTrainingBL.DeleteSeminarsTraining(RaiseTraining);
            if (TrainingName != string.Empty)
            {
                Session[SessionNames.CURRENT_PAGE_INDEX_SeminarTRAINING] = 1;
                lblConfirmMessage.Text = TrainingName + " seminar deleted.";
                SeminarsTrainingGridView(sortExpression, ASCENDING);
                //BindGridView();
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionDeleteSeminarsTraining", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Delete the Technical Training Details
    /// </summary>
    /// <returns></returns>
    private void DeleteTechnicalSoftSkillsTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining, int TrainingTypeID)
    {
        try
        {
            saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
            TrainingName = saveTrainingBL.DeleteTechnicalSoftSkillsTraining(RaiseTraining, TrainingTypeID);

            if (TrainingName != string.Empty)
            {
                lblConfirmMessage.Text = TrainingName + " training deleted.";
                if (RBLTrainingType.SelectedValue == CommonConstants.TechnicalTrainingID.ToString() || ddlFilterTrainingType.SelectedItem.Value.ToUpper() == CommonConstants.SELECT)
                {
                    Session[SessionNames.CURRENT_PAGE_INDEX_TRAINING] = CommonConstants.DefaultFlagOne;
                    HideShowTechnicalTraining();
                    TechnicalTrainingGridView(sortExpression, ASCENDING);
                }
                else if (RBLTrainingType.SelectedValue == CommonConstants.SoftSkillsTrainingID.ToString())
                {
                    Session[SessionNames.CURRENT_PAGE_INDEX_SOFTTRAINING] = CommonConstants.DefaultFlagOne;
                    HideShowSoftSkillsTraining();
                    TechnicalTrainingGridView(sortExpression, ASCENDING);
                }
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionDeleteTechnicalSoftSkillsTraining", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Delete the KSS Training Details
    /// </summary>
    /// <returns></returns>
    private void DeleteKSSTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining)
    {
        try
        {
            saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
            TrainingName = saveTrainingBL.DeleteKSSTraining(RaiseTraining);

            if (TrainingName != string.Empty)
            {

                lblConfirmMessage.Text = "Knowledge Sharing Session (" + TrainingName + ") deleted.";
                Session[SessionNames.CURRENT_PAGE_INDEX_KSSTRAINING] = 1;
                KSSTrainingGridView(sortExpression, ASCENDING);
                //BindGridView();
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionDeleteDeleteKSSTraining", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    ///  Method to get values of Training Status(Filter).
    /// </summary>
    private void GetMasterData_FilterStatusDropDown()
    {
        try
        {
            objRaveHRMaster = new List<BusinessEntities.Master>();
            objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            objRaveHRMaster = objRaveHRMasterBAL.GetTraining_MasterData(CommonConstants.TrainingStatus);
            ddlFilterStatus.DataSource = objRaveHRMaster;
            ddlFilterStatus.DataTextField = "MasterName";
            ddlFilterStatus.DataValueField = "MasterID";
            ddlFilterStatus.DataBind();
            ddlFilterStatus.Items.Insert(0, "Select");
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionGetMasterData_FilterStatusDropDown", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    private void GetMasterData_TrainingTypeRadioButton()
    {
        try
        {
            objRaveHRMaster = new List<BusinessEntities.Master>();
            objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            objRaveHRMaster = objRaveHRMasterBAL.GetTraining_MasterData(CommonConstants.TrainingType);
            RBLTrainingType.DataSource = objRaveHRMaster;
            RBLTrainingType.DataTextField = "MasterName";
            RBLTrainingType.DataValueField = "MasterID";
            RBLTrainingType.DataBind();
            RBLTrainingType.SelectedValue = CommonConstants.TechnicalTrainingID.ToString();
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionGetMasterData_FilterTrainingTypeDropDown", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    ///  Method to get values of Training Type(Filter).
    /// </summary>
    private void GetMasterData_FilterTrainingTypeDropDown()
    {
        try
        {
            objRaveHRMaster = new List<BusinessEntities.Master>();
            objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            objRaveHRMaster = objRaveHRMasterBAL.GetTraining_MasterData(CommonConstants.TrainingType);
            ddlFilterTrainingType.DataSource = objRaveHRMaster;
            ddlFilterTrainingType.DataTextField = "MasterName";
            ddlFilterTrainingType.DataValueField = "MasterID";
            ddlFilterTrainingType.DataBind();
            //ddlFilterTrainingType.Items.Insert(0, "Select");
            ddlFilterTrainingType.SelectedValue = CommonConstants.TechnicalTrainingID.ToString();
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionGetMasterData_FilterTrainingTypeDropDown", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    ///  Method to get values of Training Quarter(Filter)
    /// </summary>
    private void GetMasterData_FilterQuarterDropDown()
    {
        try
        {
            int mth = DateTime.Now.Month;
            if (mth <= 3)
            {
                ddlFilterQuarter.Items.Insert(0, new ListItem("Select", "0"));
                ddlFilterQuarter.Items.Insert(1, new ListItem("Jan - Mar", "1"));
                ddlFilterQuarter.Items.Insert(2, new ListItem("Apr - Jun", "2"));
            }
            else if (mth > 3 && mth <= 6)
            {
                ddlFilterQuarter.Items.Insert(0, new ListItem("Select", "0"));
                ddlFilterQuarter.Items.Insert(1, new ListItem("Apr - Jun", "2"));
                ddlFilterQuarter.Items.Insert(2, new ListItem("Jul - Sep", "3"));
            }
            else if (mth > 6 && mth <= 9)
            {
                ddlFilterQuarter.Items.Insert(0, new ListItem("Select", "0"));
                ddlFilterQuarter.Items.Insert(1, new ListItem("Jul - Sep", "3"));
                ddlFilterQuarter.Items.Insert(2, new ListItem("Oct - Dec", "4"));
            }
            else if (mth > 9 && mth <= 12)
            {
                ddlFilterQuarter.Items.Insert(0, new ListItem("Select", "0"));
                ddlFilterQuarter.Items.Insert(1, new ListItem("Oct - Dec", "4"));
                ddlFilterQuarter.Items.Insert(2, new ListItem("Jan - Mar", "1"));
            }
            //objRaveHRMaster = new List<BusinessEntities.Master>();
            //objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            //objRaveHRMaster = objRaveHRMasterBAL.GetTraining_MasterData(CommonConstants.TrainingQuarter);
            //ddlFilterQuarter.DataSource = objRaveHRMaster;
            //ddlFilterQuarter.DataTextField = "MasterName";
            //ddlFilterQuarter.DataValueField = "MasterID";
            //ddlFilterQuarter.DataBind();
            //ddlFilterQuarter.Items.Insert(0, "Select");
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionGetMasterData_FilterQuarterDropDown", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    ///  Method to get values of RequestBy(Filter)
    /// </summary>
    private void GetMasterData_FilterRequestByDropDown()
    {
        try
        {
            saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
            DataTable dt = saveTrainingBL.GetEditKSSTrainingGroup(UserEmpID, CommonConstants.DefaultFlagZero.ToString());
            EditCount = Convert.ToInt32(dt.Rows.Count);

            ds = new DataSet();
            addEmployeeBAL = new Rave.HR.BusinessLayer.Employee.Employee();
            ds = addEmployeeBAL.GetActiveEmployeeList();
            ddlFilterRequestBy.DataSource = ds.Tables[0];
            ddlFilterRequestBy.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlFilterRequestBy.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlFilterRequestBy.DataBind();
            ddlFilterRequestBy.Items.Insert(0, "Select");
            if (EditCount == CommonConstants.DefaultFlagZero)
            {
                ddlFilterRequestBy.Items.FindByValue(UserEmpID.ToString()).Selected = true;
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionGetMasterData_FilterRequestByDropDown", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    
    /// <summary>
    ///  Bind Master data into Filter control
    /// </summary>
    private void FilterDropdownBing()
    {
        try
        {
            GetMasterData_FilterTrainingTypeDropDown();
            GetMasterData_FilterStatusDropDown();
            GetMasterData_FilterQuarterDropDown();
            GetMasterData_FilterRequestByDropDown();
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionFilterDropdownBing", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnClear_OnClick(object sender, EventArgs e)
    {
        GetMasterData_FilterRequestByDropDown();
    }

    protected void btnFilter_OnClick(object sender, EventArgs e)
    {
        try
        {
            int pTrainingType = 0;
            Session[sessionFlag] = CommonConstants.DefaultFlagMinus;//For filter control value
            if (ddlFilterTrainingType.SelectedValue.ToUpper() != CommonConstants.SELECT)
            {
                pTrainingType = Convert.ToInt32(ddlFilterTrainingType.SelectedValue);
            }

            if (pTrainingType == CommonConstants.TechnicalTrainingID)
            {
                HideShowTechnicalTraining();
                lbltechsoftsummary.Text = "Technical";
                RBLTrainingType.SelectedValue = ddlFilterTrainingType.SelectedValue;
                TechnicalTrainingGridView(sortExpression, ASCENDING);
            }
            else if (pTrainingType == CommonConstants.SoftSkillsTrainingID)
            {
                HideShowSoftSkillsTraining();
                lbltechsoftsummary.Text = "Soft Skill";
                RBLTrainingType.SelectedValue = ddlFilterTrainingType.SelectedValue;
                TechnicalTrainingGridView(sortExpression, ASCENDING);
            }
            else if (pTrainingType == CommonConstants.KSSID)
            {
                HideShowKSSTraining();
                RBLTrainingType.SelectedValue = ddlFilterTrainingType.SelectedValue;
                KSSTrainingGridView(sortExpression, ASCENDING);
            }
            else if (pTrainingType == CommonConstants.SeminarsID)
            {
                HideShowSeminarsTraining();
                RBLTrainingType.SelectedValue = ddlFilterTrainingType.SelectedValue;
                SeminarsTrainingGridView(sortExpression, ASCENDING);
            }
            else
            {
                TechnicalTrainingGridView(sortExpression, ASCENDING);
                KSSTrainingGridView(sortExpression, ASCENDING);
                SeminarsTrainingGridView(sortExpression, ASCENDING);

                tblTechTraining.Visible = true;
                tblTechTraininggv.Visible = true;
                tblKSS.Visible = true;
                tblKSSgv.Visible = true;
                tblSeminars.Visible = true;
                tblSeminarsgv.Visible = true;
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionbtnFilter_OnClick", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void HideShowTechnicalTraining()
    {
        tblTechTraining.Visible = true;
        tblTechTraininggv.Visible = true;
        tblKSS.Visible = false;
        tblKSSgv.Visible = false;
        tblSeminars.Visible = false;
        tblSeminarsgv.Visible = false;
        //tblSoftTraining.Visible = false;
        //tblSoftTraininggv.Visible = false;
        //string ImageNameURL = IBTechnicalPlus.ImageUrl;
        //string[] SplitImageName = ImageNameURL.Split('/');
        //int strLenght = (SplitImageName.Length - 1);
        //if (SplitImageName[strLenght] == PlusJPG)
        //{
        //    Tech1.Visible = true;
        //    //Tech2.Visible = false;
        //    Tab3.Visible = false;
        //    Tab4.Visible = false;
        //    IBTechnicalPlus.ImageUrl = CommonConstants.ImageMinus;
        //    //IBSoftPlus.ImageUrl = CommonConstants.ImagePlus; ;
        //    IBKSS.ImageUrl = CommonConstants.ImagePlus; ;
        //    IBSeminars.ImageUrl = CommonConstants.ImagePlus;
        //}
        //else if (SplitImageName[strLenght] == MinusJPG)
        //{
        //    Tech1.Visible = false;
        //    IBTechnicalPlus.ImageUrl = CommonConstants.ImagePlus;
        //}
    }

    protected void HideShowSoftSkillsTraining()
    {
        tblTechTraining.Visible = true;
        tblTechTraininggv.Visible = true;
        tblKSS.Visible = false;
        tblKSSgv.Visible = false;
        tblSeminars.Visible = false;
        tblSeminarsgv.Visible = false;
        //tblSoftTraining.Visible = true;
        //tblSoftTraininggv.Visible = true;
        //string ImageNameURL = IBSoftPlus.ImageUrl;
        //string[] SplitImageName = ImageNameURL.Split('/');
        //int strLenght = (SplitImageName.Length - 1);
        //if (SplitImageName[strLenght] == PlusJPG)
        //{
        //    Tech2.Visible = true;
        //    Tech1.Visible = false;
        //    Tab3.Visible = false;
        //    Tab4.Visible = false;
        //    IBSoftPlus.ImageUrl = CommonConstants.ImageMinus;
        //    IBTechnicalPlus.ImageUrl = CommonConstants.ImagePlus; ;
        //    IBKSS.ImageUrl = CommonConstants.ImagePlus; ;
        //    IBSeminars.ImageUrl = CommonConstants.ImagePlus;
        //}
        //else if (SplitImageName[strLenght] == MinusJPG)
        //{
        //    Tech2.Visible = false;
        //    IBSoftPlus.ImageUrl = CommonConstants.ImagePlus;
        //}
    }

    protected void HideShowKSSTraining()
    {
        tblTechTraining.Visible = false;
        tblTechTraininggv.Visible = false;
        tblKSS.Visible = true;
        tblKSSgv.Visible = true;
        tblSeminars.Visible = false;
        tblSeminarsgv.Visible = false;
        //tblSoftTraining.Visible = false;
        //tblSoftTraininggv.Visible = false;
        //string ImageNameURL = IBKSS.ImageUrl;
        //string[] SplitImageName = ImageNameURL.Split('/');
        //int strLenght = (SplitImageName.Length - 1);
        //if (SplitImageName[strLenght] == PlusJPG)
        //{
        //    Tech1.Visible = false;
        //    //Tech2.Visible = false;
        //    Tab3.Visible = true;
        //    Tab4.Visible = false;
        //    IBTechnicalPlus.ImageUrl = CommonConstants.ImagePlus;
        //    //IBSoftPlus.ImageUrl = CommonConstants.ImagePlus;
        //    IBKSS.ImageUrl = CommonConstants.ImageMinus;
        //    IBSeminars.ImageUrl = CommonConstants.ImagePlus;
        //}
        //else if (SplitImageName[strLenght] == MinusJPG)
        //{
        //    Tab3.Visible = false;
        //    IBKSS.ImageUrl = CommonConstants.ImagePlus;
        //}
    }

    protected void HideShowSeminarsTraining()
    {
        tblTechTraining.Visible = false;
        tblTechTraininggv.Visible = false;
        tblKSS.Visible = false;
        tblKSSgv.Visible = false;
        tblSeminars.Visible = true;
        tblSeminarsgv.Visible = true;
        //tblSoftTraining.Visible = false;
        //tblSoftTraininggv.Visible = false;
        //string ImageNameURL = IBSeminars.ImageUrl;
        //string[] SplitImageName = ImageNameURL.Split('/');
        //int strLenght = (SplitImageName.Length - 1);
        //if (SplitImageName[strLenght] == PlusJPG)
        //{
        //    Tech1.Visible = false;
        //    // Tech2.Visible = false;
        //    Tab3.Visible = false;
        //    Tab4.Visible = true;
        //    IBTechnicalPlus.ImageUrl = CommonConstants.ImagePlus;
        //    //IBSoftPlus.ImageUrl = CommonConstants.ImagePlus;
        //    IBKSS.ImageUrl = CommonConstants.ImagePlus;
        //    IBSeminars.ImageUrl = CommonConstants.ImageMinus;
        //}
        //else if (SplitImageName[strLenght] == MinusJPG)
        //{
        //    Tab4.Visible = false;
        //    IBSeminars.ImageUrl = CommonConstants.ImagePlus;
        //}
    }

    //protected void ShowTrainingGridView()
    //{
    //    Tech1.Visible = true;
    //    //Tech2.Visible = true;
    //    Tab3.Visible = true;
    //    Tab4.Visible = true;
    //    IBTechnicalPlus.ImageUrl = CommonConstants.ImageMinus;
    //    //IBSoftPlus.ImageUrl = CommonConstants.ImageMinus;
    //    IBKSS.ImageUrl = CommonConstants.ImageMinus;
    //    IBSeminars.ImageUrl = CommonConstants.ImageMinus;
    //}

    //protected void HideShowTrainingGridView(int TrainingType)
    //{
    //    if (TrainingType == CommonConstants.TechnicalTrainingID)
    //    {
    //        HideShowTechnicalTraining();
    //    }
    //    //else if (TrainingType == CommonConstants.SoftSkillsTrainingID)
    //    //{
    //    //    HideShowSoftSkillsTraining();
    //    //}
    //    else if (TrainingType == CommonConstants.KSSID)
    //    {
    //        HideShowKSSTraining();
    //    }
    //    else if (TrainingType == CommonConstants.SeminarsID)
    //    {
    //        HideShowSeminarsTraining();
    //    }
    //}

    private void ShowHeaderWhenEmptyGrid(BusinessEntities.RaveHRCollection raveHRCollection, GridView gv)
    {
        try
        {
            gv.ShowHeader = true;
            gv.AllowSorting = false;
            raveHRCollection.Add(new BusinessEntities.RaiseTrainingRequest());
            gv.DataSource = raveHRCollection;
            gv.DataBind();
            gv.Rows[0].Cells.Clear();
            gv.Rows[0].Cells.Add(new TableCell());
            gv.Rows[0].Cells[0].Text = CommonConstants.NO_RECORDS_FOUND_MESSAGE;
        }
        catch
        {

        }
    }

    private void CloseTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining, int TrainingTypeID)
    {
        try
        {
            string TrainingName = string.Empty;
            RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();

            RaiseTrainingCollection.RaiseID = RaiseTraining.RaiseID;
            RaiseTrainingCollection.Status = RaiseTraining.Status;
            RaiseTrainingCollection.UserEmpId = RaiseTraining.UserEmpId;
            RaiseTrainingCollection.Comments = string.Empty;

            saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
            TrainingName = saveTrainingBL.UpdateRaiseTrainingStatus(RaiseTrainingCollection);

            if (TrainingTypeID == CommonConstants.TechnicalTrainingID || TrainingTypeID == CommonConstants.SoftSkillsTrainingID)
            {
                lblConfirmMessage.Text = "Training requisition closed.";
                TechnicalTrainingGridView(sortExpression, ASCENDING);
            }
            else if (TrainingTypeID == CommonConstants.SeminarsID)
            {
                lblConfirmMessage.Text = "Seminar request closed.";
                SeminarsTrainingGridView(sortExpression, ASCENDING);
            }
            else if (TrainingTypeID == CommonConstants.KSSID)
            {
                lblConfirmMessage.Text = "Knowledge Sharing Session(" + TrainingName + ") details closed.";
                KSSTrainingGridView(sortExpression, ASCENDING);
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionbtnSaveComment_OnClick", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    //protected void btnCancelComment_OnClick(object sender, EventArgs e)
    //{
    //    CommentPanel.Visible = false;
    //    txtComments.Text = string.Empty;
    //    lblConfirmMessage.Text = string.Empty;
    //    lblMandatory.Text = string.Empty;
    //}
    
    //protected void ddlFilterTrainingType_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (Convert.ToInt32(ddlFilterTrainingType.SelectedValue) == CommonConstants.TechnicalTrainingID)
    //        {
    //        }
    //        else if (Convert.ToInt32(ddlFilterTrainingType.SelectedValue) == CommonConstants.SoftSkillsTrainingID)
    //        {
    //        }
    //        else if (Convert.ToInt32(ddlFilterTrainingType.SelectedValue) == CommonConstants.KSSID)
    //        {
    //        }
    //        else if (Convert.ToInt32(ddlFilterTrainingType.SelectedValue) == CommonConstants.SeminarsID)
    //        {
    //        }
    //    }
    //    catch (RaveHRException ex)
    //    {
    //        throw ex;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "ddlFilterTrainingType_OnSelectedIndexChanged", EventIDConstants.TRAINING_PRESENTATION_LAYER);
    //    }
    //}
}
 
    

