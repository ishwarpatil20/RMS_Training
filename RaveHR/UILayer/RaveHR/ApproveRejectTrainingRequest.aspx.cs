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
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using BusinessEntities;
using Rave.HR.BusinessLayer;
using Rave.HR.BusinessLayer.Interface;

public partial class ApproveRejectTrainingRequest : BaseClass
{

    private const string Approve = "ApproveCommand";
    private const string View = "ViewCommand";
    private const string Reject = "RejectCommand";
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    private const string PREVIOUS = "Previous";
    private const string NEXT = "Next";
    private const string PlusJPG = "plus.JPG";
    private const string MinusJPG = "minus.JPG";
    private const string RaiseID = "RaiseID";
    private const string StatusName = "StatusName";
    private const string sessionPageCount = "pageCount";
    private const string KSSsessionPageCount = "KSSpageCount";
    private const string SeminarsessionPageCount = "SeminarpageCount";
    private const string FilterDefaultStatus = "Open";
    private const string PAGENAME = "ApproveRejectTrainingRequest.aspx";
    private static string sortExpression = string.Empty;
    private string commandName = string.Empty;
    private string TrainingTypeID = string.Empty;
    private int pageCount = 0;
    private int CurrentIndexCount = 0;
    private int RaiseTrainingId;
    private string UserMailId;
    private int UserEmpID;
    private int AccessRightID;
    private int EditCount = 0;
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
        if (Request.QueryString[QueryStringConstants.TrainingType] != null)
            TrainingTypeID = DecryptQueryString(QueryStringConstants.TrainingType).ToString();

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

        if (Session[SessionNames.CURRENT_PAGE_INDEX_APPREJTRAINING] == null)
            Session[SessionNames.CURRENT_PAGE_INDEX_APPREJTRAINING] = 1;

        if (Session[SessionNames.CURRENT_PAGE_INDEX_APPREJKSSTRAINING] == null)
            Session[SessionNames.CURRENT_PAGE_INDEX_APPREJKSSTRAINING] = 1;

        if (Session[SessionNames.CURRENT_PAGE_INDEX_APPREJSeminarTRAINING] == null)
            Session[SessionNames.CURRENT_PAGE_INDEX_APPREJSeminarTRAINING] = 1;

        if (!Page.IsPostBack)
        {
            saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
            AccessRightID = saveTrainingBL.AccessForTrainingModule(UserEmpID);

            if (AccessRightID == 0)
            {
                Response.Redirect("Home.aspx");
            }
            else
            {
                FilterDropdownBing();
                BindGridView();
            }
        }
    }

    /// <summary>
    /// ChangePage event handler
    /// </summary>
    protected void ChangePage(object sender, CommandEventArgs e)
    {
        GridViewRow gvrPager = gvApproveRejectTechnicalTraining.BottomPagerRow;
        TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");

        switch (e.CommandName)
        {
            case PREVIOUS:
                if ((Convert.ToInt32(txtPages.Text) - 1) == 0)
                {
                    Session[SessionNames.CURRENT_PAGE_INDEX_APPREJTRAINING] = 1;
                    txtPages.Text = "1";
                }
                else
                {
                    Session[SessionNames.CURRENT_PAGE_INDEX_APPREJTRAINING] = Convert.ToInt32(txtPages.Text) - 1;
                    txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) - 1);
                }

                break;

            case NEXT:
                Session[SessionNames.CURRENT_PAGE_INDEX_APPREJTRAINING] = Convert.ToInt32(txtPages.Text) + 1;
                txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) + 1);
                break;
        }

        ApproveRejectTechnicalTrainingGridView(sortExpression, ASCENDING);
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
                gvApproveRejectTechnicalTraining.PageIndex = Convert.ToInt32(txtPages.Text) - 1;
                Session[SessionNames.CURRENT_PAGE_INDEX_APPREJTRAINING] = txtPages.Text;
            }
            else
            {
                txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_APPREJTRAINING].ToString();
                return;
            }

            ApproveRejectTechnicalTrainingGridView(sortExpression, ASCENDING);
            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_APPREJTRAINING].ToString();
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
    /// get filter option control values
    /// </summary>
    private BusinessEntities.RaiseTrainingRequest GetFilterValues()
    {
        RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();

        RaiseTrainingCollection.RaiseID = CommonConstants.DefaultRaiseID;
        RaiseTrainingCollection.UserEmpId = UserEmpID;
        if (ddlFilterTrainingType.SelectedItem.Value.ToUpper() == CommonConstants.SELECT)
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
        return RaiseTrainingCollection;
    }

    /// <summary>
    /// Sort the gridview as per SortExpression and SortDirection
    /// </summary>
    /// <param name="sortExpression"></param>
    /// <param name="direction"></param>
    private void ApproveRejectTechnicalTrainingGridView(string sortExpression, string direction)
    {
        try
        {
            RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();

            RaiseTrainingCollection = GetFilterValues();

            if (sortExpression == CommonConstants.TrainingCreatedDate)
            {
                objParameter.SortExpressionAndDirection = sortExpression + direction;
            }
            else
            {
                objParameter.SortExpressionAndDirection = CommonConstants.TrainingCreatedDate + direction;
            }

            objParameter.PageNumber = Convert.ToInt32(Session[SessionNames.CURRENT_PAGE_INDEX_APPREJTRAINING].ToString());
            objParameter.PageSize = 10;

            raveHRCollection = ApproveRejectViewTechnicalTriningSummary(RaiseTrainingCollection);

            if ((int.Parse(Session[SessionNames.PAGE_COUNT_TRAINING].ToString()) == 1) && (Convert.ToInt32(RaiseTrainingCollection.Count) == 1))
            {
                gvApproveRejectTechnicalTraining.AllowSorting = false;
            }
            else
            {
                gvApproveRejectTechnicalTraining.AllowSorting = true;
            }

            if (Convert.ToInt32(raveHRCollection.Count) == 0)
            {
                //gvApproveRejectTechnicalTraining.DataSource = raveHRCollection;
                //gvApproveRejectTechnicalTraining.DataBind();

                //ShowHeaderWhenEmptyGrid(raveHRCollection, gvApproveRejectTechnicalTraining);

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
                gvApproveRejectTechnicalTraining.DataSource = dt;
                gvApproveRejectTechnicalTraining.DataBind();

                for (int i = 1; i < gvApproveRejectTechnicalTraining.Rows[0].Cells.Count; i++)
                {
                    gvApproveRejectTechnicalTraining.Rows[0].Cells[i].Visible = false;
                }
            }
            else
            {
                gvApproveRejectTechnicalTraining.DataSource = raveHRCollection;
                gvApproveRejectTechnicalTraining.DataBind();

                //Finds the Paging Text Box, Previous and Next Link Buttons in Grid Views Pagning Templet
                GridViewRow gvrPager = gvApproveRejectTechnicalTraining.BottomPagerRow;
                TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
                LinkButton lbtnPrevious = (LinkButton)gvrPager.Cells[0].FindControl("lbtnPrevious");
                LinkButton lbtnNext = (LinkButton)gvrPager.Cells[0].FindControl("lbtnNext");

                if (pageCount > 1)
                {
                    gvApproveRejectTechnicalTraining.BottomPagerRow.Visible = true;
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
            gv.Rows[0].Cells[0].Wrap = false;
            gv.Rows[0].Cells[0].Width = Unit.Percentage(10);
        }
        catch
        {

        }
    }

    //protected void HideShowTrainingGridView(int TrainingType)
    //{
    //    if (TrainingType == CommonConstants.TechnicalTrainingID)
    //    {
    //        HideShowTechnicalTraining();
    //    }
    //    else if (TrainingType == CommonConstants.SoftSkillsTrainingID)
    //    {
    //        HideShowSoftSkillsTraining();
    //    }
    //    else if (TrainingType == CommonConstants.KSSID)
    //    {
    //        HideShowKSSTraining();
    //    }
    //    else if (TrainingType == CommonConstants.SeminarsID)
    //    {
    //        HideShowSeminarsTraining();
    //    }
    //}

    //protected void HideShowTechnicalTraining()
    //{
    //    string ImageNameURL = IBTechnicalPlus.ImageUrl;
    //    string[] SplitImageName = ImageNameURL.Split('/');
    //    int strLenght = (SplitImageName.Length - 1);
    //    if (SplitImageName[strLenght] == PlusJPG)
    //    {
    //        tblTechnicalTraining.Visible = true;
    //        tblSoftSkillTraining.Visible = false;
    //        tblKSSTraining.Visible = false;
    //        tblSeminarsTraining.Visible = false;
    //        IBTechnicalPlus.ImageUrl = CommonConstants.ImageMinus;
    //        IBSoftPlus.ImageUrl = CommonConstants.ImagePlus; ;
    //        IBKSS.ImageUrl = CommonConstants.ImagePlus; ;
    //        IBSeminars.ImageUrl = CommonConstants.ImagePlus;
    //    }
    //    else if (SplitImageName[strLenght] == MinusJPG)
    //    {
    //        tblTechnicalTraining.Visible = false;
    //        IBTechnicalPlus.ImageUrl = CommonConstants.ImagePlus;
    //    }
    //}

    //protected void HideShowSoftSkillsTraining()
    //{
    //    string ImageNameURL = IBSoftPlus.ImageUrl;
    //    string[] SplitImageName = ImageNameURL.Split('/');
    //    int strLenght = (SplitImageName.Length - 1);
    //    if (SplitImageName[strLenght] == PlusJPG)
    //    {
    //        tblSoftSkillTraining.Visible = true;
    //        tblTechnicalTraining.Visible = false;
    //        tblKSSTraining.Visible = false;
    //        tblSeminarsTraining.Visible = false;
    //        IBSoftPlus.ImageUrl = CommonConstants.ImageMinus;
    //        IBTechnicalPlus.ImageUrl = CommonConstants.ImagePlus; ;
    //        IBKSS.ImageUrl = CommonConstants.ImagePlus; ;
    //        IBSeminars.ImageUrl = CommonConstants.ImagePlus;
    //    }
    //    else if (SplitImageName[strLenght] == MinusJPG)
    //    {
    //        tblSoftSkillTraining.Visible = false;
    //        IBSoftPlus.ImageUrl = CommonConstants.ImagePlus;
    //    }
    //}

    //protected void HideShowKSSTraining()
    //{
    //    string ImageNameURL = IBKSS.ImageUrl;
    //    string[] SplitImageName = ImageNameURL.Split('/');
    //    int strLenght = (SplitImageName.Length - 1);
    //    if (SplitImageName[strLenght] == PlusJPG)
    //    {
    //        tblTechnicalTraining.Visible = false;
    //        tblSoftSkillTraining.Visible = false;
    //        tblKSSTraining.Visible = true;
    //        tblSeminarsTraining.Visible = false;
    //        IBTechnicalPlus.ImageUrl = CommonConstants.ImagePlus;
    //        IBSoftPlus.ImageUrl = CommonConstants.ImagePlus;
    //        IBKSS.ImageUrl = CommonConstants.ImageMinus;
    //        IBSeminars.ImageUrl = CommonConstants.ImagePlus;
    //    }
    //    else if (SplitImageName[strLenght] == MinusJPG)
    //    {
    //        tblKSSTraining.Visible = false;
    //        IBKSS.ImageUrl = CommonConstants.ImagePlus;
    //    }
    //}

    //protected void HideShowSeminarsTraining()
    //{
    //    string ImageNameURL = IBSeminars.ImageUrl;
    //    string[] SplitImageName = ImageNameURL.Split('/');
    //    int strLenght = (SplitImageName.Length - 1);
    //    if (SplitImageName[strLenght] == PlusJPG)
    //    {
    //        tblTechnicalTraining.Visible = false;
    //        tblSoftSkillTraining.Visible = false;
    //        tblKSSTraining.Visible = false;
    //        tblSeminarsTraining.Visible = true;
    //        IBTechnicalPlus.ImageUrl = CommonConstants.ImagePlus;
    //        IBSoftPlus.ImageUrl = CommonConstants.ImagePlus;
    //        IBKSS.ImageUrl = CommonConstants.ImagePlus;
    //        IBSeminars.ImageUrl = CommonConstants.ImageMinus;
    //    }
    //    else if (SplitImageName[strLenght] == MinusJPG)
    //    {
    //        tblSeminarsTraining.Visible = false;
    //        IBSeminars.ImageUrl = CommonConstants.ImagePlus;
    //    }
    //}

    //protected void ShowTrainingGridView()
    //{
    //    tblTechnicalTraining.Visible = true;
    //    tblSoftSkillTraining.Visible = true;
    //    tblKSSTraining.Visible = true;
    //    tblSeminarsTraining.Visible = true;
    //    IBTechnicalPlus.ImageUrl = CommonConstants.ImageMinus;
    //    IBSoftPlus.ImageUrl = CommonConstants.ImageMinus;
    //    IBKSS.ImageUrl = CommonConstants.ImageMinus;
    //    IBSeminars.ImageUrl = CommonConstants.ImageMinus;
    //}

    protected void btnFilter_OnClick(object sender, EventArgs e)
    {
        try
        {
            int pTrainingType = 0;
            if (ddlFilterTrainingType.SelectedValue.ToUpper() != CommonConstants.SELECT)
            {
                pTrainingType = Convert.ToInt32(ddlFilterTrainingType.SelectedValue);
            }

            if (pTrainingType == CommonConstants.TechnicalTrainingID || pTrainingType == CommonConstants.SoftSkillsTrainingID)
            {
                Session[SessionNames.CURRENT_PAGE_INDEX_APPREJTRAINING] = 1;
                ApproveRejectTechnicalTrainingGridView(sortExpression, ASCENDING);

                tblTechSoftTraining.Visible = true;
                tblTechnicalTraininggv.Visible = true;
                tblSeminarsTraining.Visible = false;
                tblSeminarsTraininggv.Visible = false;
                tblKSSTraining.Visible = false;
                tblKSSTraininggv.Visible = false;
            }
            else if (pTrainingType == CommonConstants.KSSID)
            {
                Session[SessionNames.CURRENT_PAGE_INDEX_APPREJKSSTRAINING] = 1;
                ApproveRejectKSSTrainingGridView(sortExpression, ASCENDING);

                tblTechSoftTraining.Visible = false;
                tblTechnicalTraininggv.Visible = false;
                tblSeminarsTraining.Visible = false;
                tblSeminarsTraininggv.Visible = false;
                tblKSSTraining.Visible = true;
                tblKSSTraininggv.Visible = true;
            }
            else if (pTrainingType == CommonConstants.SeminarsID)
            {
                Session[SessionNames.CURRENT_PAGE_INDEX_APPREJSeminarTRAINING] = 1;
                ApproveRejectSeminarsTrainingGridView(sortExpression, ASCENDING);

                tblTechSoftTraining.Visible = false;
                tblTechnicalTraininggv.Visible = false;
                tblSeminarsTraining.Visible = true;
                tblSeminarsTraininggv.Visible = true;
                tblKSSTraining.Visible = false;
                tblKSSTraininggv.Visible = false;
            }
            else
            {
                Session[SessionNames.CURRENT_PAGE_INDEX_APPREJTRAINING] = 1;
                Session[SessionNames.CURRENT_PAGE_INDEX_APPREJKSSTRAINING] = 1;
                Session[SessionNames.CURRENT_PAGE_INDEX_APPREJSeminarTRAINING] = 1;

                ApproveRejectTechnicalTrainingGridView(sortExpression, ASCENDING);
                ApproveRejectKSSTrainingGridView(sortExpression, ASCENDING);
                ApproveRejectSeminarsTrainingGridView(sortExpression, ASCENDING);

                tblTechSoftTraining.Visible = true;
                tblTechnicalTraininggv.Visible = true;
                tblSeminarsTraining.Visible = true;
                tblSeminarsTraininggv.Visible = true;
                tblKSSTraining.Visible = true;
                tblKSSTraininggv.Visible = true;
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

    //protected void IBTechnicalPlus_Click(object sender, ImageClickEventArgs e)
    //{
    //    try
    //    {
    //        HideShowTechnicalTraining();

    //        ApproveRejectTechnicalTrainingGridView(sortExpression, ASCENDING);
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

    //        ApproveRejectSoftSkillsTrainingGridView(sortExpression, ASCENDING);
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
    //        ApproveRejectKSSTrainingGridView(sortExpression, ASCENDING);
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
    //        ApproveRejectSeminarsTrainingGridView(sortExpression, ASCENDING);
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
            ddlFilterStatus.Items.FindByText(FilterDefaultStatus.ToString()).Selected = true;
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
            ddlFilterTrainingType.Items.Insert(0, "Select");
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
            ds = new DataSet();
            addEmployeeBAL = new Rave.HR.BusinessLayer.Employee.Employee();
            ds = addEmployeeBAL.GetActiveEmployeeList();
            ddlFilterRequestBy.DataSource = ds.Tables[0];
            ddlFilterRequestBy.DataTextField = ds.Tables[0].Columns[1].ToString();
            ddlFilterRequestBy.DataValueField = ds.Tables[0].Columns[0].ToString();
            ddlFilterRequestBy.DataBind();
            ddlFilterRequestBy.Items.Insert(0, "Select");
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

    protected void gvApproveRejectTechnicalTraining_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
            commandName = e.CommandName;
            ViewState[RaiseTrainingId.ToString()] = CommonConstants.TechnicalTrainingID;
            if (e.CommandName == Approve)
            {
                ViewState[RaiseID] = int.Parse(e.CommandArgument.ToString());
                ViewState[StatusName] = CommonConstants.Approved;
                lblMandatory.Text = string.Empty;
                lblComments.Text = "Comments (if any) :";
                spanComments.Style.Add("color", "#D4DEFA");
            }
            else if (e.CommandName == Reject)
            {
                ViewState[RaiseID] = int.Parse(e.CommandArgument.ToString());
                ViewState[StatusName] = CommonConstants.Rejected;
                lblComments.Text = CommonConstants.Reasonforrejection;
                spanComments.Style.Add("color", "Red");
                lblMandatory.Text = string.Empty;
            }
            CommentPanel.Visible = true;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvApproveRejectTechnicalTraining_RowCommand", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    protected void gvApproveRejectTechnicalTraining_OnRowDataBound(object sender, GridViewRowEventArgs e)
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
                HiddenField hf = (HiddenField)e.Row.FindControl("hfgvRaiseID");
                ImageButton btnApprove = (ImageButton)e.Row.FindControl("imgApprove");
                ImageButton btnReject = (ImageButton)e.Row.FindControl("imgReject");
                HiddenField hfRequestby = (HiddenField)e.Row.FindControl("hfRequestByID");
                string StatusValue = string.Empty;
                if (hf.Value != "0")
                {
                    StatusValue = DataBinder.Eval(e.Row.DataItem, "Status").ToString();
                }
                btnView.Attributes[CommonConstants.EVENT_ONCLICK] = "location.href = 'RaiseTrainingRequest.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.TechnicalTrainingID.ToString()) + "&" + URLHelper.SecureParameters("RaiseID", hf.Value) + "&" + URLHelper.SecureParameters("Action", CommonConstants.View) + "&" + URLHelper.SecureParameters("Page", CommonConstants.ApproveRejectSummaryPage) + "'";

                if (EditCount != CommonConstants.DefaultFlagZero)
                {
                    //if (StatusValue == CommonConstants.Approved || StatusValue == CommonConstants.Rejected || StatusValue == CommonConstants.Deleted || StatusValue == CommonConstants.Cancelled)
                    if (StatusValue == CommonConstants.Approved || StatusValue == CommonConstants.Rejected || StatusValue == CommonConstants.Deleted || StatusValue == CommonConstants.Closed)
                    {
                        btnApprove.ImageUrl = CommonConstants.ImageMinus;
                        btnReject.ImageUrl = CommonConstants.ImageMinus;
                        btnApprove.ToolTip = string.Empty;
                        btnReject.ToolTip = string.Empty;
                        btnApprove.Enabled = false;
                        btnReject.Enabled = false;
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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvApproveRejectTechnicalTraining_OnRowDataBound", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// gvTechnicalTrainingSummary_OnDataBound event handler
    /// </summary>
    protected void gvApproveRejectTechnicalTraining_OnDataBound(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrPager = gvApproveRejectTechnicalTraining.BottomPagerRow;

            if (gvrPager == null) return;

            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            Label lblPageCount = (Label)gvrPager.Cells[0].FindControl("lblPageCount");

            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_APPREJTRAINING].ToString();

            if (lblPageCount != null)
                lblPageCount.Text = pageCount.ToString();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvApproveRejectTechnicalTraining_OnDataBound", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// gridview pageIndexChanging event handler
    /// </summary>
    protected void gvApproveRejectTechnicalTraining_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //Change the page index on page index changing event and bind the grid
            if (e.NewPageIndex != -1)
            {
                gvApproveRejectTechnicalTraining.PageIndex = e.NewPageIndex;
            }

            ApproveRejectTechnicalTrainingGridView(sortExpression, ASCENDING);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvApproveRejectTechnicalTraining_OnPageIndexChanging", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// SoftSkillsChangePage event handler
    /// </summary>
    //protected void SoftSkillsChangePage(object sender, CommandEventArgs e)
    //{
    //    GridViewRow gvrPager = gvApproveRejectSoftSkillTrainingSummary.BottomPagerRow;
    //    TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");

    //    switch (e.CommandName)
    //    {
    //        case PREVIOUS:
    //            if ((Convert.ToInt32(txtPages.Text) - 1) == 0)
    //            {
    //                Session[SessionNames.CURRENT_PAGE_INDEX_TRAINING] = 1;
    //                txtPages.Text = "1";
    //            }
    //            else
    //            {
    //                Session[SessionNames.CURRENT_PAGE_INDEX_TRAINING] = Convert.ToInt32(txtPages.Text) - 1;
    //                txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) - 1);
    //            }

    //            break;

    //        case NEXT:
    //            Session[SessionNames.CURRENT_PAGE_INDEX_TRAINING] = Convert.ToInt32(txtPages.Text) + 1;
    //            txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) + 1);
    //            break;
    //    }

    //    ApproveRejectSoftSkillsTrainingGridView(sortExpression, ASCENDING);
    //}

    /// <summary>
    /// Sort the SoftSkills gridview as per SortExpression and SortDirection
    /// </summary>
    /// <param name="sortExpression"></param>
    /// <param name="direction"></param>
    //private void ApproveRejectSoftSkillsTrainingGridView(string sortExpression, string direction)
    //{
    //    try
    //    {
    //        RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();

    //        RaiseTrainingCollection = GetFilterValues();

    //        if (sortExpression == CommonConstants.MRF_CODE)
    //        {
    //            objParameter.SortExpressionAndDirection = sortExpression + direction;
    //        }
    //        else
    //        {
    //            objParameter.SortExpressionAndDirection = CommonConstants.TrainingCreatedDate + direction;
    //        }

    //        objParameter.PageNumber = Convert.ToInt32(Session[SessionNames.CURRENT_PAGE_INDEX_TRAINING].ToString());
    //        objParameter.PageSize = 10;

    //        raveHRCollection = ApproveRejectViewSoftSkillsTriningSummary(RaiseTrainingCollection);

    //        if ((int.Parse(Session[SessionNames.PAGE_COUNT_TRAINING].ToString()) == 1) && (Convert.ToInt32(RaiseTrainingCollection.Count) == 1))
    //        {
    //            gvApproveRejectSoftSkillTrainingSummary.AllowSorting = false;
    //        }
    //        else
    //        {
    //            gvApproveRejectSoftSkillTrainingSummary.AllowSorting = true;
    //        }

    //        if (Convert.ToInt32(raveHRCollection.Count) == 0)
    //        {
    //            gvApproveRejectSoftSkillTrainingSummary.DataSource = raveHRCollection;
    //            gvApproveRejectSoftSkillTrainingSummary.DataBind();

    //            ShowHeaderWhenEmptyGrid(raveHRCollection, gvApproveRejectSoftSkillTrainingSummary);
    //        }
    //        else
    //        {
    //            gvApproveRejectSoftSkillTrainingSummary.DataSource = raveHRCollection;
    //            gvApproveRejectSoftSkillTrainingSummary.DataBind();
    //        }
    //        //Finds the Paging Text Box, Previous and Next Link Buttons in Grid Views Pagning Templet
    //        GridViewRow gvrPager = gvApproveRejectSoftSkillTrainingSummary.BottomPagerRow;
    //        TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
    //        LinkButton lbtnPrevious = (LinkButton)gvrPager.Cells[0].FindControl("lbtnPrevious");
    //        LinkButton lbtnNext = (LinkButton)gvrPager.Cells[0].FindControl("lbtnNext");

    //        if (pageCount > 1)
    //        {
    //            gvApproveRejectSoftSkillTrainingSummary.BottomPagerRow.Visible = true;
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
    //        Session[sessionPageCount] = pageCount;
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
    /// View the Technical Training Summary
    /// </summary>
    /// <returns></returns>
    private BusinessEntities.RaveHRCollection ApproveRejectViewTechnicalTriningSummary(BusinessEntities.RaiseTrainingRequest RaiseTraining)
    {
        try
        {
            saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
            raveHRCollection = saveTrainingBL.ApproveRejectViewTechnicalTraining(RaiseTraining, objParameter, ref pageCount, ref CurrentIndexCount);

            Session[SessionNames.PAGE_COUNT_TRAINING] = pageCount;
            if (CurrentIndexCount != CommonConstants.DefaultFlagZero)
            {
                Session[SessionNames.CURRENT_PAGE_INDEX_APPREJTRAINING] = CurrentIndexCount;
            }
            return raveHRCollection;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionApproveRejectViewTechnicalTriningSummary", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// View the SoftSkills Training Summary
    /// </summary>
    /// <returns></returns>
    private BusinessEntities.RaveHRCollection ApproveRejectViewSoftSkillsTriningSummary(BusinessEntities.RaiseTrainingRequest RaiseTraining)
    {
        try
        {
            saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
            raveHRCollection = saveTrainingBL.ApproveRejectViewSoftSkillsTraining(RaiseTraining, objParameter, ref pageCount);

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
    private BusinessEntities.RaveHRCollection ApproveRejectViewKSSTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining)
    {
        try
        {
            saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
            raveHRCollection = saveTrainingBL.ApproveRejectViewKSSTraining(RaiseTraining, objParameter, ref pageCount, ref CurrentIndexCount);

            Session[SessionNames.PAGE_COUNT_TRAINING] = pageCount;
            if (CurrentIndexCount != CommonConstants.DefaultFlagZero)
            {
                Session[SessionNames.CURRENT_PAGE_INDEX_APPREJKSSTRAINING] = CurrentIndexCount;
            }
            return raveHRCollection;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionApproveRejectViewKSSTraining", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// View the Seminars Training Summary
    /// </summary>
    /// <returns>raveHRCollection</returns>
    private BusinessEntities.RaveHRCollection ApproveRejectViewSeminarsTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining)
    {
        try
        {
            saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
            raveHRCollection = saveTrainingBL.ApproveRejectViewSeminarsTraining(RaiseTraining, objParameter, ref pageCount, ref CurrentIndexCount);

            Session[SessionNames.PAGE_COUNT_TRAINING] = pageCount;
            if (CurrentIndexCount != CommonConstants.DefaultFlagZero)
            {
                Session[SessionNames.CURRENT_PAGE_INDEX_APPREJSeminarTRAINING] = CurrentIndexCount;
            }
            return raveHRCollection;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctionApproveRejectViewSeminarsTraining", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// SoftSkillstxtPages_TextChanged event handler
    /// </summary>
    //protected void SoftSkillstxtPages_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        TextBox txtPages = (TextBox)sender;

    //        if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0 && Convert.ToInt32(txtPages.Text) <= Convert.ToInt32(Session[sessionPageCount].ToString()))
    //        {
    //            gvApproveRejectSoftSkillTrainingSummary.PageIndex = Convert.ToInt32(txtPages.Text) - 1;
    //            Session[SessionNames.CURRENT_PAGE_INDEX_TRAINING] = txtPages.Text;
    //        }
    //        else
    //        {
    //            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_TRAINING].ToString();
    //            return;
    //        }

    //        ApproveRejectSoftSkillsTrainingGridView(sortExpression, ASCENDING);
    //        txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_TRAINING].ToString();
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

    protected void gvApproveRejectSoftSkillTrainingSummary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
            commandName = e.CommandName;
            ViewState[RaiseTrainingId.ToString()] = CommonConstants.SoftSkillsTrainingID;
            if (e.CommandName == Approve)
            {
                ViewState[RaiseID] = int.Parse(e.CommandArgument.ToString());
                ViewState[StatusName] = CommonConstants.Approved;
                lblMandatory.Text = string.Empty;
                lblComments.Text = "Comments (if any) :";
                spanComments.Style.Add("color", "#D4DEFA");
            }
            else if (e.CommandName == Reject)
            {
                ViewState[RaiseID] = int.Parse(e.CommandArgument.ToString());
                ViewState[StatusName] = CommonConstants.Rejected;
                lblComments.Text = CommonConstants.Reasonforrejection;
                spanComments.Style.Add("color", "Red");
                lblMandatory.Text = string.Empty;
            }
            CommentPanel.Visible = true;

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvApproveRejectSoftSkillTrainingSummary_RowCommand", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    protected void gvApproveRejectKSSTrainingSummary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
            commandName = e.CommandName;
            ViewState[RaiseTrainingId.ToString()] = CommonConstants.KSSID;
            if (e.CommandName == Approve)
            {
                ViewState[RaiseID] = int.Parse(e.CommandArgument.ToString());
                ViewState[StatusName] = CommonConstants.Approved;
                lblMandatory.Text = string.Empty;
                lblComments.Text = "Comments (if any) :";
                spanComments.Style.Add("color", "#D4DEFA");
            }
            else if (e.CommandName == Reject)
            {
                ViewState[RaiseID] = int.Parse(e.CommandArgument.ToString());
                ViewState[StatusName] = CommonConstants.Rejected;
                lblComments.Text = CommonConstants.Reasonforrejection;
                spanComments.Style.Add("color", "Red");
                lblMandatory.Text = string.Empty;
            }
            CommentPanel.Visible = true;

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvApproveRejectKSSTrainingSummary_RowCommand", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// gvApproveRejectKSSTrainingSummary_OnDataBound event handler
    /// </summary>
    protected void gvApproveRejectKSSTrainingSummary_OnDataBound(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrPager = gvApproveRejectKSSTrainingSummary.BottomPagerRow;

            if (gvrPager == null) return;

            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            Label lblPageCount = (Label)gvrPager.Cells[0].FindControl("lblPageCount");

            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_APPREJKSSTRAINING].ToString();

            if (lblPageCount != null)
                lblPageCount.Text = pageCount.ToString();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvApproveRejectKSSTrainingSummary_OnDataBound", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// gvSoftSkillsTrainingSummary_OnDataBound event handler
    /// </summary>
    //protected void gvApproveRejectSoftSkillTrainingSummary_OnDataBound(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        GridViewRow gvrPager = gvApproveRejectSoftSkillTrainingSummary.BottomPagerRow;

    //        if (gvrPager == null) return;

    //        TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
    //        Label lblPageCount = (Label)gvrPager.Cells[0].FindControl("lblPageCount");

    //        txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_TRAINING].ToString();

    //        if (lblPageCount != null)
    //            lblPageCount.Text = pageCount.ToString();
    //    }
    //    catch (RaveHRException ex)
    //    {
    //        LogErrorMessage(ex);
    //    }
    //    catch (Exception ex)
    //    {
    //        RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvApproveRejectSoftSkillTrainingSummary_OnDataBound", EventIDConstants.TRAINING_PRESENTATION_LAYER);
    //        LogErrorMessage(objEx);
    //    }
    //}

    protected void gvApproveRejectSoftSkillTrainingSummary_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnView = (ImageButton)e.Row.FindControl("imgView");
                HiddenField hf = (HiddenField)e.Row.FindControl("hfgvRaiseID");
                HiddenField hfRequestby = (HiddenField)e.Row.FindControl("hfRequestByID");

                btnView.Attributes[CommonConstants.EVENT_ONCLICK] = "location.href = 'RaiseTrainingRequest.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.SoftSkillsTrainingID.ToString()) + "&" + URLHelper.SecureParameters("RaiseID", hf.Value) + "&" + URLHelper.SecureParameters("Action", CommonConstants.View) + "&" + URLHelper.SecureParameters("Page", CommonConstants.ApproveRejectSummaryPage) + "'";
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvApproveRejectSoftSkillTrainingSummary_OnRowDataBound", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    protected void gvApproveRejectKSSTrainingSummary_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton btnView = (ImageButton)e.Row.FindControl("imgView");
                HiddenField hf = (HiddenField)e.Row.FindControl("hfgvRaiseID");
                btnView.Attributes[CommonConstants.EVENT_ONCLICK] = "location.href = 'RaiseTrainingRequest.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.KSSID.ToString()) + "&" + URLHelper.SecureParameters("RaiseID", hf.Value) + "&" + URLHelper.SecureParameters("Action", CommonConstants.View) + "&" + URLHelper.SecureParameters("Page", CommonConstants.ApproveRejectSummaryPage) + "'";
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvApproveRejectKSSTrainingSummary_OnRowDataBound", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    protected void gvApproveRejectTechnicalTraining_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ApproveRejectTechnicalTrainingGridView(sortExpression, ASCENDING);
            CommentPanel.Visible = false;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvApproveRejectTechnicalTraining_Sorting", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    //protected void gvApproveRejectSoftSkillTrainingSummary_Sorting(object sender, GridViewSortEventArgs e)
    //{
    //    try
    //    {
    //        ApproveRejectSoftSkillsTrainingGridView(sortExpression, ASCENDING);
    //        CommentPanel.Visible = false;
    //    }
    //    catch (RaveHRException ex)
    //    {
    //        throw ex;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvApproveRejectSoftSkillTrainingSummary_Sorting", EventIDConstants.TRAINING_PRESENTATION_LAYER);
    //    }
    //}

    protected void gvApproveRejectKSSTrainingSummary_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ApproveRejectKSSTrainingGridView(sortExpression, ASCENDING);
            CommentPanel.Visible = false;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvApproveRejectKSSTrainingSummary_Sorting", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    protected void gvApproveRejectSeminarsSummary_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ApproveRejectSeminarsTrainingGridView(sortExpression, ASCENDING);
            CommentPanel.Visible = false;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvApproveRejectSeminarsSummary_Sorting", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// gridview gvApproveRejectSoftSkillTrainingSummary_OnPageIndexChanging event handler
    /// </summary>
    //protected void gvApproveRejectSoftSkillTrainingSummary_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    try
    //    {
    //        if (e.NewPageIndex != -1)
    //        {
    //            gvApproveRejectSoftSkillTrainingSummary.PageIndex = e.NewPageIndex;
    //        }

    //        ApproveRejectSoftSkillsTrainingGridView(sortExpression, ASCENDING);
    //    }
    //    catch (RaveHRException ex)
    //    {
    //        LogErrorMessage(ex);
    //    }
    //    catch (Exception ex)
    //    {
    //        RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvApproveRejectSoftSkillTrainingSummary_OnPageIndexChanging", EventIDConstants.TRAINING_PRESENTATION_LAYER);
    //        LogErrorMessage(objEx);
    //    }
    //}

    /// <summary>
    /// Sort the KSS gridview as per SortExpression and SortDirection
    /// </summary>
    /// <param name="sortExpression"></param>
    /// <param name="direction"></param>
    private void ApproveRejectKSSTrainingGridView(string sortExpression, string direction)
    {
        try
        {
            RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();

            RaiseTrainingCollection = GetFilterValues();

            if (sortExpression == CommonConstants.MRF_CODE)
            {
                objParameter.SortExpressionAndDirection = sortExpression + direction;
            }
            else
            {
                objParameter.SortExpressionAndDirection = CommonConstants.TrainingCreatedDate + direction;
            }

            objParameter.PageNumber = Convert.ToInt32(Session[SessionNames.CURRENT_PAGE_INDEX_APPREJKSSTRAINING].ToString());
            objParameter.PageSize = 2;

            raveHRCollection = ApproveRejectViewKSSTraining(RaiseTrainingCollection);

            if ((int.Parse(Session[SessionNames.PAGE_COUNT_TRAINING].ToString()) == 1) && (Convert.ToInt32(RaiseTrainingCollection.Count) == 1))
            {
                gvApproveRejectKSSTrainingSummary.AllowSorting = false;
            }
            else
            {
                gvApproveRejectKSSTrainingSummary.AllowSorting = true;
            }

            if (Convert.ToInt32(raveHRCollection.Count) == 0)
            {
                //gvApproveRejectKSSTrainingSummary.DataSource = raveHRCollection;
                //gvApproveRejectKSSTrainingSummary.DataBind();

                //ShowHeaderWhenEmptyGrid(raveHRCollection, gvApproveRejectKSSTrainingSummary);
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
                gvApproveRejectKSSTrainingSummary.DataSource = dt;
                gvApproveRejectKSSTrainingSummary.DataBind();
                for (int i = 1; i < gvApproveRejectKSSTrainingSummary.Rows[0].Cells.Count; i++)
                {
                    gvApproveRejectKSSTrainingSummary.Rows[0].Cells[i].Visible = false;
                }
            }
            else
            {
                gvApproveRejectKSSTrainingSummary.DataSource = raveHRCollection;
                gvApproveRejectKSSTrainingSummary.DataBind();

                //Finds the Paging Text Box, Previous and Next Link Buttons in Grid Views Pagning Templet
                GridViewRow gvrPager = gvApproveRejectKSSTrainingSummary.BottomPagerRow;
                TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
                LinkButton lbtnPrevious = (LinkButton)gvrPager.Cells[0].FindControl("lbtnPrevious");
                LinkButton lbtnNext = (LinkButton)gvrPager.Cells[0].FindControl("lbtnNext");

                if (pageCount > 1)
                {
                    gvApproveRejectKSSTrainingSummary.BottomPagerRow.Visible = true;
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

    /// <summary>
    /// gridview gvApproveRejectKSSTrainingSummary_OnPageIndexChanging event handler
    /// </summary>
    protected void gvApproveRejectKSSTrainingSummary_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (e.NewPageIndex != -1)
            {
                gvApproveRejectKSSTrainingSummary.PageIndex = e.NewPageIndex;
            }

            ApproveRejectKSSTrainingGridView(sortExpression, ASCENDING);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvApproveRejectKSSTrainingSummary_OnPageIndexChanging", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
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
                gvApproveRejectKSSTrainingSummary.PageIndex = Convert.ToInt32(txtPages.Text) - 1;
                Session[SessionNames.CURRENT_PAGE_INDEX_APPREJKSSTRAINING] = txtPages.Text;
            }
            else
            {
                txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_APPREJKSSTRAINING].ToString();
                return;
            }

            ApproveRejectKSSTrainingGridView(sortExpression, ASCENDING);
            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_APPREJKSSTRAINING].ToString();
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
    /// KSSChangePage event handler
    /// </summary>
    protected void KSSChangePage(object sender, CommandEventArgs e)
    {
        GridViewRow gvrPager = gvApproveRejectKSSTrainingSummary.BottomPagerRow;
        TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");

        switch (e.CommandName)
        {
            case PREVIOUS:
                if ((Convert.ToInt32(txtPages.Text) - 1) == 0)
                {
                    Session[SessionNames.CURRENT_PAGE_INDEX_APPREJKSSTRAINING] = 1;
                    txtPages.Text = "1";
                }
                else
                {
                    Session[SessionNames.CURRENT_PAGE_INDEX_APPREJKSSTRAINING] = Convert.ToInt32(txtPages.Text) - 1;
                    txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) - 1);
                }

                break;

            case NEXT:
                Session[SessionNames.CURRENT_PAGE_INDEX_APPREJKSSTRAINING] = Convert.ToInt32(txtPages.Text) + 1;
                txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) + 1);
                break;
        }

        ApproveRejectKSSTrainingGridView(sortExpression, ASCENDING);
    }

    protected void gvApproveRejectSeminarsSummary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
            commandName = e.CommandName;
            ViewState[RaiseTrainingId.ToString()] = CommonConstants.SeminarsID;
            if (e.CommandName == Approve)
            {
                ViewState[RaiseID] = int.Parse(e.CommandArgument.ToString());
                ViewState[StatusName] = CommonConstants.Approved;
                lblMandatory.Text = string.Empty;
                lblComments.Text = "Comments (if any) :";
                spanComments.Style.Add("color", "#D4DEFA");
            }
            else if (e.CommandName == Reject)
            {
                ViewState[RaiseID] = int.Parse(e.CommandArgument.ToString());
                ViewState[StatusName] = CommonConstants.Rejected;
                lblComments.Text = CommonConstants.Reasonforrejection;
                spanComments.Style.Add("color", "Red");
                lblMandatory.Text = string.Empty;
            }
            CommentPanel.Visible = true;

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvApproveRejectSeminarsSummary_RowCommand", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    protected void gvApproveRejectSeminarsSummary_OnRowDataBound(object sender, GridViewRowEventArgs e)
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
                ImageButton btnApprove = (ImageButton)e.Row.FindControl("imgApprove");
                ImageButton btnReject = (ImageButton)e.Row.FindControl("imgReject");
                HiddenField hf = (HiddenField)e.Row.FindControl("hfgvRaiseID");
                HiddenField hfRequestby = (HiddenField)e.Row.FindControl("hfRequestByID");
                string StatusValue = string.Empty;
                if (hf.Value != "0")
                {
                    StatusValue = DataBinder.Eval(e.Row.DataItem, "Status").ToString();
                }
                btnView.Attributes[CommonConstants.EVENT_ONCLICK] = "location.href = 'RaiseTrainingRequest.aspx?" + URLHelper.SecureParameters("TrainingType", CommonConstants.SeminarsID.ToString()) + "&" + URLHelper.SecureParameters("RaiseID", hf.Value) + "&" + URLHelper.SecureParameters("Action", CommonConstants.View) + "&" + URLHelper.SecureParameters("Page", CommonConstants.ApproveRejectSummaryPage) + "'";
                if (EditCount != CommonConstants.DefaultFlagZero)
                {
                    //if (StatusValue == CommonConstants.Approved || StatusValue == CommonConstants.Rejected || StatusValue == CommonConstants.Deleted || StatusValue == CommonConstants.Cancelled)
                    if (StatusValue == CommonConstants.Approved || StatusValue == CommonConstants.Rejected || StatusValue == CommonConstants.Deleted || StatusValue == CommonConstants.Closed)
                    {
                        btnApprove.ImageUrl = CommonConstants.ImageMinus;
                        btnReject.ImageUrl = CommonConstants.ImageMinus;
                        btnApprove.ToolTip = string.Empty;
                        btnReject.ToolTip = string.Empty;
                        btnApprove.Enabled = false;
                        btnReject.Enabled = false;
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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvApproveRejectSeminarsSummary_OnRowDataBound", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// gridview gvApproveRejectSeminarsSummary_OnPageIndexChanging event handler
    /// </summary>
    protected void gvApproveRejectSeminarsSummary_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //Change the page index on page index changing event and bind the grid
            if (e.NewPageIndex != -1)
            {
                gvApproveRejectSeminarsSummary.PageIndex = e.NewPageIndex;
            }

            ApproveRejectSeminarsTrainingGridView(sortExpression, ASCENDING);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvApproveRejectSeminarsSummary_OnPageIndexChanging", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// gvApproveRejectSeminarsSummary_OnDataBound event handler
    /// </summary>
    protected void gvApproveRejectSeminarsSummary_OnDataBound(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrPager = gvApproveRejectSeminarsSummary.BottomPagerRow;

            if (gvrPager == null) return;

            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            Label lblPageCount = (Label)gvrPager.Cells[0].FindControl("lblPageCount");

            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_APPREJSeminarTRAINING].ToString();

            if (lblPageCount != null)
                lblPageCount.Text = pageCount.ToString();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGENAME, "FunctiongvApproveRejectSeminarsSummary_OnDataBound", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Sort the Seminars gridview as per SortExpression and SortDirection
    /// </summary>
    /// <param name="sortExpression"></param>
    /// <param name="direction"></param>
    private void ApproveRejectSeminarsTrainingGridView(string sortExpression, string direction)
    {
        try
        {
            RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();

            RaiseTrainingCollection = GetFilterValues();

            if (sortExpression == CommonConstants.MRF_CODE)
            {
                objParameter.SortExpressionAndDirection = sortExpression + direction;
            }
            else
            {
                objParameter.SortExpressionAndDirection = CommonConstants.TrainingCreatedDate + direction;
            }

            objParameter.PageNumber = Convert.ToInt32(Session[SessionNames.CURRENT_PAGE_INDEX_APPREJSeminarTRAINING].ToString());
            objParameter.PageSize = 2;

            raveHRCollection = ApproveRejectViewSeminarsTraining(RaiseTrainingCollection);

            if ((int.Parse(Session[SessionNames.PAGE_COUNT_TRAINING].ToString()) == 1) && (Convert.ToInt32(RaiseTrainingCollection.Count) == 1))
            {
                gvApproveRejectSeminarsSummary.AllowSorting = false;
            }
            else
            {
                gvApproveRejectSeminarsSummary.AllowSorting = true;
            }

            if (Convert.ToInt32(raveHRCollection.Count) == 0)
            {
                //gvApproveRejectSeminarsSummary.DataSource = raveHRCollection;
                //gvApproveRejectSeminarsSummary.DataBind();
                //ShowHeaderWhenEmptyGrid(raveHRCollection, gvApproveRejectSeminarsSummary);
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
                gvApproveRejectSeminarsSummary.DataSource = dt;
                gvApproveRejectSeminarsSummary.DataBind();

                for (int i = 1; i < gvApproveRejectSeminarsSummary.Rows[0].Cells.Count; i++)
                {
                    gvApproveRejectSeminarsSummary.Rows[0].Cells[i].Visible = false;
                }
            }
            else
            {
                gvApproveRejectSeminarsSummary.DataSource = raveHRCollection;
                gvApproveRejectSeminarsSummary.DataBind();

                //Finds the Paging Text Box, Previous and Next Link Buttons in Grid Views Pagning Templet
                GridViewRow gvrPager = gvApproveRejectSeminarsSummary.BottomPagerRow;
                TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
                LinkButton lbtnPrevious = (LinkButton)gvrPager.Cells[0].FindControl("lbtnPrevious");
                LinkButton lbtnNext = (LinkButton)gvrPager.Cells[0].FindControl("lbtnNext");

                if (pageCount > 1)
                {
                    gvApproveRejectSeminarsSummary.BottomPagerRow.Visible = true;
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
    /// SeminarsChangePage event handler
    /// </summary>
    protected void SeminarsChangePage(object sender, CommandEventArgs e)
    {
        GridViewRow gvrPager = gvApproveRejectSeminarsSummary.BottomPagerRow;
        TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");

        switch (e.CommandName)
        {
            case PREVIOUS:
                if ((Convert.ToInt32(txtPages.Text) - 1) == 0)
                {
                    Session[SessionNames.CURRENT_PAGE_INDEX_APPREJSeminarTRAINING] = 1;
                    txtPages.Text = "1";
                }
                else
                {
                    Session[SessionNames.CURRENT_PAGE_INDEX_APPREJSeminarTRAINING] = Convert.ToInt32(txtPages.Text) - 1;
                    txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) - 1);
                }

                break;

            case NEXT:
                Session[SessionNames.CURRENT_PAGE_INDEX_APPREJSeminarTRAINING] = Convert.ToInt32(txtPages.Text) + 1;
                txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) + 1);
                break;
        }

        ApproveRejectSeminarsTrainingGridView(sortExpression, ASCENDING);
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
                gvApproveRejectSeminarsSummary.PageIndex = Convert.ToInt32(txtPages.Text) - 1;
                Session[SessionNames.CURRENT_PAGE_INDEX_APPREJSeminarTRAINING] = txtPages.Text;
            }
            else
            {
                txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_APPREJSeminarTRAINING].ToString();
                return;
            }

            ApproveRejectSeminarsTrainingGridView(sortExpression, ASCENDING);
            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_APPREJSeminarTRAINING].ToString();
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

    private void BindGridView()
    {
        //if (TrainingTypeID == CommonConstants.TechnicalTrainingID.ToString())
        //{
        //HideShowTechnicalTraining();
        ApproveRejectTechnicalTrainingGridView(sortExpression, ASCENDING);
        //}
        //else if (TrainingTypeID == CommonConstants.SoftSkillsTrainingID.ToString())
        //{
        //    HideShowSoftSkillsTraining();
        //    ApproveRejectSoftSkillsTrainingGridView(sortExpression, ASCENDING);
        //}
        //else if (TrainingTypeID == CommonConstants.KSSID.ToString())
        //{
        //HideShowKSSTraining();
        ApproveRejectKSSTrainingGridView(sortExpression, ASCENDING);
        //}
        //else if (TrainingTypeID == CommonConstants.SeminarsID.ToString())
        //{
        //HideShowSeminarsTraining();
        ApproveRejectSeminarsTrainingGridView(sortExpression, ASCENDING);
        // }
    }

    protected void btnSaveComment_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (lblComments.Text == CommonConstants.Reasonforrejection && txtComments.Text.Trim() == string.Empty)
            {
                lblMandatory.Text = "Please provide the reason for rejection.";
            }
            else
            {
                RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
                int RaiseTrainingID = Convert.ToInt32(ViewState[RaiseTrainingId.ToString()]);
                RaiseTrainingCollection.RaiseID = Convert.ToInt32(ViewState[RaiseID]);
                RaiseTrainingCollection.Status = ViewState[StatusName].ToString();
                RaiseTrainingCollection.UserEmpId = UserEmpID;
                RaiseTrainingCollection.Comments = txtComments.Text;

                saveTrainingBL = new Rave.HR.BusinessLayer.Training.RaiseTrainingRequest();
                RaiseTrainingId = saveTrainingBL.SaveApproveRejectTrainingRequest(RaiseTrainingCollection);

                if (RaiseTrainingId != 0)
                {
                    if (RaiseTrainingID == CommonConstants.TechnicalTrainingID)
                    {
                        if (lblComments.Text == CommonConstants.Reasonforrejection)
                        {
                            lblConfirmMessage.Text = "Training requisition rejected.";
                            //Ishwar Patil : Training Module : 22/08/2014 Start
                            SendMailForTechSoftSkillRejected(RaiseTrainingCollection);
                            //Ishwar Patil : Training Module : 22/08/2014 End
                        }
                        else
                        {
                            lblConfirmMessage.Text = "Training requisition approved.";

                            //Ishwar Patil : Training Module : 21/08/2014 Start
                            SendMailForTechSoftSkillApproved(RaiseTrainingCollection);
                            //Ishwar Patil : Training Module : 21/08/2014 End

                        }
                        ApproveRejectTechnicalTrainingGridView(sortExpression, ASCENDING);
                    }
                    //else if (RaiseTrainingID == CommonConstants.SoftSkillsTrainingID)
                    //{
                    //    if (lblComments.Text == CommonConstants.Reasonforrejection)
                    //    {
                    //        lblConfirmMessage.Text = "Training requisition rejected.";
                    //    }
                    //    else
                    //    {
                    //        lblConfirmMessage.Text = "Training requisition approved.";
                    //    }
                    //    ApproveRejectSoftSkillsTrainingGridView(sortExpression, ASCENDING);
                    //}
                    //else if (RaiseTrainingID == CommonConstants.KSSID)
                    //{
                    //    if (lblComments.Text == CommonConstants.Reasonforrejection)
                    //    {
                    //        lblConfirmMessage.Text = "Training requisition rejected.";
                    //    }
                    //    else
                    //    {
                    //        lblConfirmMessage.Text = "Knowledge Sharing Session details approved.";
                    //    }
                    //    ApproveRejectKSSTrainingGridView(sortExpression, ASCENDING);
                    //}
                    else if (RaiseTrainingID == CommonConstants.SeminarsID)
                    {
                        if (lblComments.Text == CommonConstants.Reasonforrejection)
                        {
                            lblConfirmMessage.Text = "Seminar request rejected.";
                            //Ishwar Patil : Training Module : 22/08/2014 Start
                            SendMailForSeminarRejected(RaiseTrainingCollection);
                            //Ishwar Patil : Training Module : 22/08/2014 End
                        }
                        else
                        {
                            lblConfirmMessage.Text = "Seminar request approved.";
                            //Ishwar Patil : Training Module : 22/08/2014 Start
                            SendMailForSeminarApproved(RaiseTrainingCollection);
                            //Ishwar Patil : Training Module : 22/08/2014 End
                        }
                        ApproveRejectSeminarsTrainingGridView(sortExpression, ASCENDING);
                    }
                }
                txtComments.Text = string.Empty;
                CommentPanel.Visible = false;
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

    protected void btnCancelComment_OnClick(object sender, EventArgs e)
    {
        CommentPanel.Visible = false;
        txtComments.Text = string.Empty;
        lblConfirmMessage.Text = string.Empty;
        lblMandatory.Text = string.Empty;
    }

    private void SendMailForTechSoftSkillApproved(BusinessEntities.RaiseTrainingRequest RaiseTrainingCollection)
    {
        try
        {
            IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
               Convert.ToInt16(EnumsConstants.EmailFunctionality.TechSoftSkillApproved));

            DataSet dt = saveTrainingBL.GetEmailIDForAppRejTechSoftSkill(RaiseTrainingCollection.RaiseID);
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
                obj.Body = string.Format(obj.Body, dt.Tables[1].Rows[0]["RequestRaiserName"], GetHTMLForTableDataForTechSoftApproved(dt.Tables[1]), CommonConstants.RMGroupName);

                obj.SendEmail(obj);
            }
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionSendMailForTechSoftSkillApproved", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    private void SendMailForTechSoftSkillRejected(BusinessEntities.RaiseTrainingRequest RaiseTrainingCollection)
    {
        try
        {
            IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
               Convert.ToInt16(EnumsConstants.EmailFunctionality.TechSoftSkillRejectd));

            DataSet dt = saveTrainingBL.GetEmailIDForAppRejTechSoftSkill(RaiseTrainingCollection.RaiseID);
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
                obj.Body = string.Format(obj.Body, dt.Tables[1].Rows[0]["RequestRaiserName"], GetHTMLForTableDataForTechSoftRejected(dt.Tables[1]), CommonConstants.RMGroupName);

                obj.SendEmail(obj);
            }
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionSendMailForTechSoftSkillRejected", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    private string GetHTMLForTableDataForTechSoftApproved(DataTable dt)
    {
        try
        {
            //list for table values
            List<string> objListTrainingDetail = new List<string>();
            objListTrainingDetail.Add(dt.Rows[0]["TrainingName"].ToString());
            objListTrainingDetail.Add(dt.Rows[0]["Quarter"].ToString());
            objListTrainingDetail.Add(dt.Rows[0]["Priority"].ToString());

            string[,] arrayData = new string[3, 2];

            if (objListTrainingDetail.Count > 0)
            {
                //Header Values
                arrayData[0, 0] = "Name";
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
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionGetHTMLForTableDataForTechSoftApproved", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    private string GetHTMLForTableDataForTechSoftRejected(DataTable dt)
    {
        try
        {
            //list for table values
            List<string> objListTrainingDetail = new List<string>();
            objListTrainingDetail.Add(dt.Rows[0]["TrainingName"].ToString());
            objListTrainingDetail.Add(dt.Rows[0]["Quarter"].ToString());
            objListTrainingDetail.Add(dt.Rows[0]["Priority"].ToString());
            objListTrainingDetail.Add(dt.Rows[0]["ApprovalComments"].ToString());

            string[,] arrayData = new string[4, 2];

            if (objListTrainingDetail.Count > 0)
            {
                //Header Values
                arrayData[0, 0] = "Name";
                arrayData[1, 0] = "Quarter";
                arrayData[2, 0] = "Priority";
                arrayData[3, 0] = "Reason for rejection";

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
            throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionGetHTMLForTableDataForTechSoftRejected", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }
    
    private void SendMailForSeminarApproved(BusinessEntities.RaiseTrainingRequest RaiseTrainingCollection)
    {
        try
        {
            IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
               Convert.ToInt16(EnumsConstants.EmailFunctionality.SeminarsApproved));

            DataSet dt = saveTrainingBL.GetEmailIDForAppRejTechSoftSkill(RaiseTrainingCollection.RaiseID);
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
                obj.Body = string.Format(obj.Body, dt.Tables[1].Rows[0]["RequestRaiserName"], GetHTMLForTableDataForSeminarApproved(dt.Tables[1]), CommonConstants.RMGroupName);

                obj.SendEmail(obj);
            }
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionSendMailForSeminarApproved", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    private void SendMailForSeminarRejected(BusinessEntities.RaiseTrainingRequest RaiseTrainingCollection)
    {
        try
        {
            IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
               Convert.ToInt16(EnumsConstants.EmailFunctionality.SeminarsRejectd));

            DataSet dt = saveTrainingBL.GetEmailIDForAppRejTechSoftSkill(RaiseTrainingCollection.RaiseID);
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
                obj.Body = string.Format(obj.Body, dt.Tables[1].Rows[0]["RequestRaiserName"], GetHTMLForTableDataForSeminarRejected(dt.Tables[1]), CommonConstants.RMGroupName);

                obj.SendEmail(obj);
            }
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionSendMailForSeminarRejected", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }
   
    private string GetHTMLForTableDataForSeminarApproved(DataTable dt)
    {
        try
        {
            //list for table values
            List<string> objListTrainingDetail = new List<string>();
            objListTrainingDetail.Add(dt.Rows[0]["Name"].ToString());
            objListTrainingDetail.Add(dt.Rows[0]["Date"].ToString());
            
            string[,] arrayData = new string[2, 2];

            if (objListTrainingDetail.Count > 0)
            {
                //Header Values
                arrayData[0, 0] = "Name";
                arrayData[1, 0] = "Date";
                
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
            throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionGetHTMLForTableDataForSeminarApproved", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }

    private string GetHTMLForTableDataForSeminarRejected(DataTable dt)
    {
        try
        {
            //list for table values
            List<string> objListTrainingDetail = new List<string>();
            objListTrainingDetail.Add(dt.Rows[0]["Name"].ToString());
            objListTrainingDetail.Add(dt.Rows[0]["Date"].ToString());
            objListTrainingDetail.Add(dt.Rows[0]["ApprovalComments"].ToString());

            string[,] arrayData = new string[3, 2];

            if (objListTrainingDetail.Count > 0)
            {
                //Header Values
                arrayData[0, 0] = "Name";
                arrayData[1, 0] = "Date";
                arrayData[2, 0] = "Reason for rejection";

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
            throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionGetHTMLForTableDataForSeminarRejected", EventIDConstants.TRAINING_PRESENTATION_LAYER);
        }
    }
}
