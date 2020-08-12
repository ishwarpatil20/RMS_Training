using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Common;
using BusinessEntities;
using System.Text;
using Common.Constants;
using System.Globalization;

public partial class RPBulkUpdate : BaseClass
{
    #region Private Field Members

    /// <summary>
    /// Defines Generic List for Resource Plan Data
    /// </summary>
    RaveHRCollection objListResourcePlan = new RaveHRCollection();

    /// <summary>
    /// Defines Ascending sorting order for grid
    /// </summary>
    private const string ASCENDING = " ASC";

    /// <summary>
    /// Defines Descending sorting order for grid
    /// </summary>
    private const string DESCENDING = " DESC";

    /// <summary>
    /// Defines a constant for Page Size
    /// </summary>
    private const int PAGE_SIZE = 10000;

    /// <summary>
    /// Defines a constant for Page Count
    /// </summary>
    private int pageCount = 0;

    /// <summary>
    /// Define RESOURCEBILLING
    /// </summary>
    private string RESOURCEBILLING = "Billing";

    /// <summary>
    /// Defines default value for sorting expression 
    /// </summary>
    private static string sortExpression = string.Empty;

    /// <summary>
    /// Define totalBilling
    /// </summary>
    private int totalBilling = 0;

    /// <summary>
    /// Error Message for dates
    /// </summary>
    private string dateErrorMsg = "Dates must match with project dates";

    /// <summary>
    /// Page Class Name
    /// </summary>
    private const string CLASS_NAME_RP = CommonConstants.RPBULKUPDATE_PAGE;

    /// <summary>
    /// Sort Expression Column Name
    /// </summary>
    private const string sortExpColumn = "RPDuRowNo";

    /// <summary>
    /// Date Validation
    /// </summary>
    private const string dateValidation = "Please Select End Date";
    
    #endregion

    #region "Page Load"
    /// <summary>
    /// page load eventhandler.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //--Default sort expression
                if (ViewState[SessionNames.PREVIOUS_SORT_EXPRESSION] == null)
                {
                    sortExpression = sortExpColumn;
                    ViewState[SessionNames.PREVIOUS_SORT_EXPRESSION] = sortExpression;
                }
                else
                {
                    sortExpression = ViewState[SessionNames.PREVIOUS_SORT_EXPRESSION].ToString();
                }

                GetLocation();
                GetProjectLocation();
                //--Get resource plan 
                GridResourcePlan();
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "Page_Load", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    #endregion

    #region "Control Events"
    /// <summary>
    /// Repeater Item Bound
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                BusinessEntities.ResourcePlan objBERPDurationDetail = new BusinessEntities.ResourcePlan();
                objBERPDurationDetail.ResourcePlanDurationId = Convert.ToInt32(((BusinessEntities.ResourcePlan)e.Item.DataItem).ResourcePlanDurationId);
                objBERPDurationDetail.Mode = "EDIT";
                objBERPDurationDetail.RPDEdited = false; //--false -RP details edited which is not commited
                objBERPDurationDetail.RPDDeletedStatusId = Convert.ToInt32(MasterEnum.RPDetailEditionStatus.Deleted);

                Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLRPDurationDetail = new Rave.HR.BusinessLayer.Projects.ResourcePlan();

                RaveHRCollection objListRPDurationDetail = new RaveHRCollection();
                objListRPDurationDetail = objBLLRPDurationDetail.GetRPDurationDetail(objBERPDurationDetail);

                ((HiddenField)e.Item.FindControl("hfRPDetailId")).Value = ((ResourcePlan)objListRPDurationDetail.Item(0)).RPDId.ToString();
                ((HiddenField)e.Item.FindControl("hfUtilization")).Value = ((ResourcePlan)objListRPDurationDetail.Item(0)).Utilization.ToString();
                ((HiddenField)e.Item.FindControl("hfBilling")).Value = ((ResourcePlan)objListRPDurationDetail.Item(0)).Billing.ToString();
                ((HiddenField)e.Item.FindControl("hfResourceLocation")).Value = ((ResourcePlan)objListRPDurationDetail.Item(0)).ResourceLocation.ToString();
                ((HiddenField)e.Item.FindControl("hfProjectLocation")).Value = ((ResourcePlan)objListRPDurationDetail.Item(0)).ProjectLocation.ToString();
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "rptData_ItemDataBound", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    #endregion

    #region "Procedure"
    /// <summary>
    /// Returns Resource Plan data for GridView
    /// </summary>
    /// <returns>List</returns>
    private RaveHRCollection GetResourcePlanList(string direction)
    {
        RaveHRCollection objListGetResourcePlan = null;
        try
        {
            //--Fill entiry
            BusinessEntities.ResourcePlan objBEResourcePlan = new BusinessEntities.ResourcePlan();
            objBEResourcePlan.RPId = Convert.ToInt32(Request.QueryString["rid"].ToString());
            objBEResourcePlan.RPDuDeletedStatusId = Convert.ToInt32(MasterEnum.RPDurationEditionStatus.Deleted);
            objBEResourcePlan.RPDDeletedStatusId = Convert.ToInt32(MasterEnum.RPDetailEditionStatus.Deleted);
            objBEResourcePlan.PageSize = PAGE_SIZE;
            objBEResourcePlan.PageNumber = 1;
            //--Sort Expression
            objBEResourcePlan.SortExpression = sortExpression;
            objBEResourcePlan.SortDirection = direction;

            //--Get data
            Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLGetResourcePlan = new Rave.HR.BusinessLayer.Projects.ResourcePlan();
            objListGetResourcePlan = new RaveHRCollection();

            objListGetResourcePlan = objBLLGetResourcePlan.GetResourcePlanById(objBEResourcePlan, ref pageCount);

            //--Get pagecount in viewstate
            Session[SessionNames.PageCount] = pageCount;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "GetResourcePlanList", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }

        //--
        return objListGetResourcePlan;
    }
    /// <summary>
    /// Bind resource plan grid
    /// </summary>
    private void GridResourcePlan()
    {
        try
        {
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                SortGridView(sortExpression, ASCENDING);
            }
            else
            {
                SortGridView(sortExpression, DESCENDING);
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "GridResourcePlan", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }
    /// <summary>
    /// Private Property to Get and Set direction for for sorting
    /// </summary>
    private SortDirection GridViewSortDirection
    {
        get
        {
            if (ViewState[Common.SessionNames.SORT_DIRECTION] == null)
                ViewState[Common.SessionNames.SORT_DIRECTION] = SortDirection.Ascending;

            return (SortDirection)ViewState[Common.SessionNames.SORT_DIRECTION];
        }
        set
        {
            ViewState[Common.SessionNames.SORT_DIRECTION] = value;
        }
    }
    /// <summary>
    /// Sorts grid view
    /// </summary>
    /// <param name="sortExpression">Sort expression</param>
    /// <param name="direction">Sorts in Ascending or Descending order</param>
    private void SortGridView(string sortExpression, string direction)
    {
        try
        {
            //--Get RP data
            objListResourcePlan = GetResourcePlanList(direction);

            //get totatl biling
            GetTotalBilling(objListResourcePlan);

            //--Bind Grid
            rptData.DataSource = objListResourcePlan;
            rptData.DataBind();
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "SortGridView", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }
    /// <summary>
    /// Method to get total billing
    /// </summary>
    private void GetTotalBilling(RaveHRCollection objListResourcePlan)
    {
        //loops through gridview to get totalbilling.
        foreach (BusinessEntities.ResourcePlanDuration objlist in objListResourcePlan)
        {
            //assign rp billing to totalBilling variable
            if (totalBilling != 0)
            {
                //assign billing value
                totalBilling = totalBilling + GetBillingValue(Convert.ToInt32(objlist.ResourcePlanDurationId));
            }
            else
            {
                //assign billing value
                totalBilling = GetBillingValue(Convert.ToInt32(objlist.ResourcePlanDurationId));
            }
        }

        //assign billing value in viewstate
        ViewState[RESOURCEBILLING] = totalBilling;
    }
    /// <summary>
    /// Get billing value
    /// </summary>
    private int GetBillingValue(int RPDurationId)
    {
        int billing = 0;
        BusinessEntities.ResourcePlan objBERPDurationDetail = new BusinessEntities.ResourcePlan();
        objBERPDurationDetail.ResourcePlanDurationId = RPDurationId;
        objBERPDurationDetail.Mode = "EDIT";
        objBERPDurationDetail.RPDEdited = false; //--false -RP details edited which is not commited
        objBERPDurationDetail.RPDDeletedStatusId = Convert.ToInt32(MasterEnum.RPDetailEditionStatus.Deleted);

        Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLRPDurationDetail = new Rave.HR.BusinessLayer.Projects.ResourcePlan();

        RaveHRCollection objListRPDurationDetail = new RaveHRCollection();
        objListRPDurationDetail = objBLLRPDurationDetail.GetRPDurationDetail(objBERPDurationDetail);

        //loops through objListRPDurationDetail collection object to get billing value
        foreach (BusinessEntities.ResourcePlan ObjRP in objListRPDurationDetail)
        {
            if (billing != 0)
                billing = billing + Convert.ToInt32(ObjRP.Billing);
            else
                billing = Convert.ToInt32(ObjRP.Billing);
        }

        return billing;
    }
    /// <summary>
    /// Get location from master
    /// </summary>
    private void GetLocation()
    {
        try
        {
            BindDropDown(ddlLocation, Convert.ToInt32(EnumsConstants.Category.ResourceLocation));
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "GetLocation", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);

        }
    }
    /// <summary>
    /// Get projectlocation from master
    /// </summary>
    private void GetProjectLocation()
    {
        try
        {
            BindDropDown(ddlProjectLocation, Convert.ToInt32(EnumsConstants.Category.ProjectLocation));
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "GetProjectLocation", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }
    /// <summary>
    /// Get master data
    /// </summary>
    private BusinessEntities.RaveHRCollection GetMaster(int CategoryId)
    {

        BusinessEntities.RaveHRCollection objListMaster = new BusinessEntities.RaveHRCollection();
        try
        {
            Rave.HR.BusinessLayer.Common.Master objRaveMaster = new Rave.HR.BusinessLayer.Common.Master();
            objListMaster = objRaveMaster.FillDropDownsBL(CategoryId);

        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "GetMaster", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
        return objListMaster;
    }
    /// <summary>
    /// Generic method for dropdown bind
    /// </summary>
    private void BindDropDown(DropDownList ddl, int CategoryId)
    {
        try
        {
            ddl.Items.Clear();
            ddl.DataSource = GetMaster(CategoryId);
            ddl.DataTextField = "Val";
            ddl.DataValueField = "KeyName";
            ddl.DataBind();

            if (ddl.Items.Count > 0)
            {
                ddl.Items.Insert(0, new ListItem("Select", ""));
            }
            else
            {
                ddl.Items.Add(new ListItem("Select", ""));
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "BindDropDown", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }
    #endregion

    #region "Click Events"
    /// <summary>
    /// BulkUpdate button click event handler.
    /// </summary>
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ucDatePickerEndDate.Text == "")
            {
                lblMessage.Text = dateValidation;
                lblMessage.Style["color"] = "red";
                return;
            }

            //--StartDate & EndDate should be within project start and enddate
            DateTime dtProjectStartDate, dtProjectEndDate;
            BusinessEntities.ResourcePlan objBEResourcePlan = new BusinessEntities.ResourcePlan();
            Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLResourcePlan = new Rave.HR.BusinessLayer.Projects.ResourcePlan();
            objBEResourcePlan.ProjectId = Convert.ToInt32(Request.QueryString["pid"].ToString());
            objBEResourcePlan = objBLLResourcePlan.GetProjectDetails(objBEResourcePlan);

            dtProjectStartDate = objBEResourcePlan.StartDate;
            dtProjectEndDate = objBEResourcePlan.EndDate;

            if ((DateTime.Parse(ucDatePickerEndDate.Text) > dtProjectEndDate) || (DateTime.Parse(ucDatePickerEndDate.Text) < dtProjectStartDate))
            {
                lblMessage.Text = dateErrorMsg + " i.e between " + dtProjectStartDate.ToString(CommonConstants.DATE_FORMAT) + " & " + dtProjectEndDate.ToString(CommonConstants.DATE_FORMAT);
                lblMessage.Style["color"] = "red";
                return;
            }

            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "dd/MM/yyyy";
            dtFormat.DateSeparator = "/";

            for (int i = 0; i < rptData.Items.Count; i++)
            {
                if (((CheckBox)rptData.Items[i].FindControl("chkSelect")).Checked)
                {
                    objBEResourcePlan = new BusinessEntities.ResourcePlan();
                    objBEResourcePlan.RPDId = Convert.ToInt32(((HiddenField)rptData.Items[i].FindControl("hfRPDetailId")).Value);
                    objBEResourcePlan.Utilization = Convert.ToInt32(((HiddenField)rptData.Items[i].FindControl("hfUtilization")).Value);
                    objBEResourcePlan.Billing = Convert.ToInt32(((HiddenField)rptData.Items[i].FindControl("hfBilling")).Value);
                    objBEResourcePlan.ResourceLocation = ddlLocation.Items.FindByText(((HiddenField)rptData.Items[i].FindControl("hfResourceLocation")).Value).Value;
                    objBEResourcePlan.ResourceStartDate = Convert.ToDateTime(((Label)rptData.Items[i].FindControl("lblStartDate")).Text, dtFormat);
                    objBEResourcePlan.ResourceEndDate = DateTime.Parse(ucDatePickerEndDate.Text);
                    objBEResourcePlan.ProjectLocation = ddlProjectLocation.Items.FindByText(((HiddenField)rptData.Items[i].FindControl("hfProjectLocation")).Value).Value;
                    objBEResourcePlan.RPEdited = false; //--false -RP not Edited --true -RP Edited

                    objBLLResourcePlan = new Rave.HR.BusinessLayer.Projects.ResourcePlan();

                    //--Update record
                    //objBLLResourcePlan.UpdateRPDetailByID(objBEResourcePlan, "UPDATE");
                    objBLLResourcePlan.EditRPDetailByID(objBEResourcePlan);
                }
            }

            //Umesh: Issue 'Modal Popup issue in chrome' Starts
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "CloseJS",
                        "jQuery.modalDialog.getCurrent().close();", true);
            //Umesh: Issue 'Modal Popup issue in chrome' Ends
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "btnUpdate_Click", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    #endregion

    //Umesh: Issue 'Modal Popup issue in chrome' Starts
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "CloseJS",
                        "jQuery.modalDialog.getCurrent().close();", true);
    }
    //Umesh: Issue 'Modal Popup issue in chrome' Ends
}
