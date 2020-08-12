//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           MrfListOfInternalResource.aspx      
//  Author:         Chhaya Gunjal 
//  Date written:   03/09/2009/ 10:58:30 AM
//  Description:    The MrfListOfInternalResource page summarises Resource details. 
//
//  Amendments
//  Date                    Who              Ref     Description
//  ----                    -----------      ---     -----------
//  03/09/2009 10:58:30 AM  Chhaya Gunjal    n/a     Created    
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
using BusinessEntities;
using System.Data.SqlClient;
using Common.Constants;
using Common;

public partial class MrfListOfInternalResource : BaseClass
{
    #region Constants
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    private static string sortExpression = string.Empty;
    private const string NO_RECORDS_FOUND_MESSAGE = "No records found.";
    private const string CLASS_NAME = "MrfPendingAllocation.aspx";
    private int pageCount = 0;
    private const string PREVIOUS_SORT_EXPRESSION = "Previous Sort Expression of List Of Internal Resource";
    private const string PAGE_LOAD = "Page_Load";
    private const string MRFLISTOFINTERNALRESOURCE = "MrfListOfInternalResource";
    private const string CURRENT_PAGE_INDEX = "currentPageIndex";
    private const string RELEASE_DATE = "ReleaseDate";
    private const string TXT_PAGES = "txtPages";
    public const string SORT_DIRECTION = "sortDirection for List Of Internal Resource ";
    private const string PREVIOUS = "Previous";
    private const string NEXT = "Next";
    private const string PAGE_COUNT = "pageCount for List of Internal Resource";
    private const string LBL_PAGE_COUNT = "lblPageCount";
    private const string RESOURCE_NAME = "ResourceName";
    private const string CURRENT_PROJECT_NAME = "CurrentProjectName";
    private const string SELECTED_ROW = "SelectedRow";
    private const string BIND_GRID = "BindGrid";
    private const string DEPARTMENT = "Department";
    //private const string RESOURCE_NAME = "ResourceName";
    #endregion

    #region Variable
    private BusinessEntities.MRFDetail mrfDetail;
    BusinessEntities.ParameterCriteria objParameterCriteria = new BusinessEntities.ParameterCriteria();
    RaveHRCollection objListSort = new RaveHRCollection();
    //Declaring COllection class object
    BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
    private int departmentID;
    #endregion

    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.AppendHeader("Pragma", "no-cache");
        if (!ValidateURL())
        {
            Response.Redirect(CommonConstants.INVALIDURL);
        }
        else
        {
            try
            {
                btnOk.Attributes.Add(CommonConstants.EVENT_ONCLICK, "return IsResourceSelected();");
                if (!IsPostBack)
                {
                    Session[CURRENT_PAGE_INDEX] = 1;
                    if (Session[PREVIOUS_SORT_EXPRESSION] == null)
                    {
                        sortExpression = RESOURCE_NAME;
                        Session[PREVIOUS_SORT_EXPRESSION] = sortExpression;
                    }
                    else
                    {
                        sortExpression = Session[PREVIOUS_SORT_EXPRESSION].ToString();
                    }
                    if (Session[SessionNames.MRFDetail] != null)
                    {
                        mrfDetail = (MRFDetail)Session[SessionNames.MRFDetail];
                        if (mrfDetail.DepartmentId != 1)
                        {
                            //Fill drop down of department
                            FillDropDowns();
                        }
                        else
                        {
                            pnlDepartmentDropDown.Visible = false;
                            departmentID = 1;
                            //Bind the grid while Loading the page
                            BindGrid();
                        }
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
                RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, PAGE_LOAD, EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
                LogErrorMessage(objEx);
            }
        }
    }

    /// <summary>
    /// Function will Fill Dropdowns of all the Master Data
    /// </summary>
    private void FillDropDowns()
    {
        try
        {
            //Declaring Master Class Object
            Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "FillDropDowns", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
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

            departmentID = Convert.ToInt32(ddlDepartment.SelectedItem.Value);
            ViewState[DEPARTMENT] = departmentID;
            sortExpression = RESOURCE_NAME;
            Session[PREVIOUS_SORT_EXPRESSION] = sortExpression;
            if (Session[SessionNames.MRFDetail] != null)
            {
                mrfDetail = (MRFDetail)Session[SessionNames.MRFDetail];
            }
            else
                mrfDetail = new MRFDetail();
            if (departmentID == mrfDetail.DepartmentId)
            {
                grdvListofInternalResource.DataSource = objListSort;
                grdvListofInternalResource.DataBind();
                ShowHeaderWhenEmptyGrid(objListSort);
            }
            else if (departmentID != 1)
            {
                grdvListofInternalResource.Columns[3].Visible = false;
                grdvListofInternalResource.Columns[4].Visible = false;
                grdvListofInternalResource.Columns[5].Visible = false;
                grdvListofInternalResource.Columns[6].Visible = false;
                BindGrid();
            }
            else if (departmentID == 1)
            {
                grdvListofInternalResource.Columns[3].Visible = true;
                grdvListofInternalResource.Columns[4].Visible = true;
                grdvListofInternalResource.Columns[5].Visible = true;
                grdvListofInternalResource.Columns[6].Visible = true;
                BindGrid();
            }

        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlDepartment_SelectedIndexChanged", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdvListofInternalResource_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            GridViewRow gvrPager = grdvListofInternalResource.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl(TXT_PAGES);
            txtPages.Text = Session[CURRENT_PAGE_INDEX].ToString();

            //On sorting assign new sort expression
            sortExpression = e.SortExpression;
            if (Session[PREVIOUS_SORT_EXPRESSION] == null)
            {
                Session[PREVIOUS_SORT_EXPRESSION] = sortExpression;
            }
            if (Session[PREVIOUS_SORT_EXPRESSION].ToString() == sortExpression)
            {
                //Sort to opposite direction based upon previous sort direction
                if (Session[SORT_DIRECTION] == null || GridViewSortDirection == SortDirection.Descending)
                {
                    GridViewSortDirection = SortDirection.Ascending;
                    SortGridView(sortExpression, ASCENDING);
                }
                else
                {
                    GridViewSortDirection = SortDirection.Descending;
                    SortGridView(sortExpression, DESCENDING);
                }
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;
                SortGridView(sortExpression, ASCENDING);
            }
            //Set current sort expression as PreviousSortExpression
            Session[PREVIOUS_SORT_EXPRESSION] = sortExpression;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvListofInternalResource_Sorting", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void ChangePage(object sender, CommandEventArgs e)
    {
        try
        {
            GridViewRow gvrPager = grdvListofInternalResource.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            switch (e.CommandName)
            {
                case PREVIOUS:
                    Session[CURRENT_PAGE_INDEX] = Convert.ToInt32(txtPages.Text) - 1;
                    txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) - 1);
                    break;

                case NEXT:
                    Session[CURRENT_PAGE_INDEX] = Convert.ToInt32(txtPages.Text) + 1;
                    txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) + 1);
                    break;
            }
            BindGrid();
        }//catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ChangePage", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void txtPages_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txtPages = (TextBox)sender;

            //Check for Paging text box is not empty, non-zero, and out of range paging no.
            if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0 && Convert.ToInt32(txtPages.Text) <= Convert.ToInt32(Session[PAGE_COUNT].ToString()))
            {
                grdvListofInternalResource.PageIndex = Convert.ToInt32(txtPages.Text) - 1;
                Session[CURRENT_PAGE_INDEX] = txtPages.Text;
            }
            else
            {
                txtPages.Text = Session[CURRENT_PAGE_INDEX].ToString();
                return;
            }
            //Bind the grid on paging
            BindGrid();

            //Assign the tetbox current page no.
            txtPages.Text = Session[CURRENT_PAGE_INDEX].ToString();
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "txtPages_TextChanged", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void radioId_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            int selected = ((System.Web.UI.WebControls.GridViewRow)(((System.Web.UI.Control)(sender)).BindingContainer)).RowIndex;
            ViewState[SELECTED_ROW] = selected;
        }//catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "radioId_CheckedChanged", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        try
        {
            int selected = (int)ViewState[SELECTED_ROW];
            GridViewRow dr = grdvListofInternalResource.Rows[selected];
            if (Session[SessionNames.MRFDetail] != null)
            {
                mrfDetail = (MRFDetail)Session[SessionNames.MRFDetail];
                Label lbl = (Label)dr.Cells[0].FindControl("lbl");
                mrfDetail.EmployeeId = Convert.ToInt32(lbl.Text);
                mrfDetail.EmployeeName = dr.Cells[2].Text.ToString();
                Session[SessionNames.MRFDetail] = mrfDetail;
                Session[SessionNames.InternalResource] = mrfDetail;

            }

            Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "window.close();", true);
        }//catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnOk_Click", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "window.close();", true);
        }//catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnCancel_Click", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdvListofInternalResource_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //Change the page index on page index changing event and bind the grid
            if (e.NewPageIndex != -1)
            {
                grdvListofInternalResource.PageIndex = e.NewPageIndex;
            }

            BindGrid();
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvListofInternalResource_PageIndexChanging", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdvListofInternalResource_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (((Session[PAGE_COUNT] != null) && (int.Parse(Session[PAGE_COUNT].ToString()) > 1)) || ((objListSort.Count > 1)))
                {
                    //Add sort Images to Grid View Header
                    AddSortImage(e.Row);
                }
            }
            //if (e.Row.RowIndex >= 0)
            //    grdvListofInternalResource.SelectedIndex = e.Row.RowIndex;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvListofInternalResource_RowCreated", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdvListofInternalResource_DataBound(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrPager = grdvListofInternalResource.BottomPagerRow;

            if (gvrPager == null) return;

            //Get Text Box and Lable from the Grid View
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl(TXT_PAGES);
            Label lblPageCount = (Label)gvrPager.Cells[0].FindControl(LBL_PAGE_COUNT);

            //Assign current page index to text box
            if (Session[CURRENT_PAGE_INDEX] != null)
                txtPages.Text = Session[CURRENT_PAGE_INDEX].ToString();

            //Assign total no of pages to label
            if (lblPageCount != null)
                lblPageCount.Text = pageCount.ToString();
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvListofInternalResource_DataBound", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdvListofInternalResource_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //--Add sort image in header
            if (int.Parse(Session[PAGE_COUNT].ToString()) == 1)
            {
                AddSortImage(grdvListofInternalResource.HeaderRow);
            }
            if (e.CommandName != "Sort")
            {
                GridView _gridView = (GridView)sender;
                // Get the selected index and the command name
                int _selectedIndex = int.Parse(e.CommandArgument.ToString());
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvListofInternalResource_RowCommand", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    #endregion

    #region Methods
    /// <summary>
    /// Private Property to Get and Set direction for for sorting
    /// </summary>
    public SortDirection GridViewSortDirection
    {
        get
        {
            if (Session[SORT_DIRECTION] == null)
                Session[SORT_DIRECTION] = SortDirection.Ascending;
            return (SortDirection)Session[SORT_DIRECTION];
        }
        set
        {
            Session[SORT_DIRECTION] = value;
        }
    }

    /// <summary>
    /// Returns MRF Details whose status is pending allocation for GridView
    /// </summary>
    /// <returns>List</returns>
    private RaveHRCollection GetListOfInternalResource()
    {
        RaveHRCollection objListMRFDetails = new RaveHRCollection();
        try
        {
            if (grdvListofInternalResource.BottomPagerRow != null)
            {
                GridViewRow gvrPager = grdvListofInternalResource.BottomPagerRow;
                TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl(TXT_PAGES);
                if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0 && Convert.ToInt32(txtPages.Text) <= Convert.ToInt32(Session[PAGE_COUNT].ToString()))
                {
                    objParameterCriteria.PageNumber = int.Parse(txtPages.Text);
                }
            }
            else
            {
                objParameterCriteria.PageNumber = 1;
            }
            if (Session[SessionNames.MRFDetail] != null)
                mrfDetail = (MRFDetail)Session[SessionNames.MRFDetail];
            else
            {
                mrfDetail = new MRFDetail();
            }
            if (ViewState[DEPARTMENT] != null)
            {
                departmentID = (int)ViewState[DEPARTMENT];
            }
            objParameterCriteria.PageSize = 10;
            objListMRFDetails = Rave.HR.BusinessLayer.MRF.MRFDetail.GetListOfInternalResource(mrfDetail, departmentID, objParameterCriteria, ref pageCount);
            Session[PAGE_COUNT] = pageCount;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetListOfInternalResource", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
        return objListMRFDetails;
    }

    /// <summary>
    /// Add the sorting image on header of column
    /// </summary>
    private void AddSortImage(GridViewRow headerRow)
    {
        try
        {
            string _sortDirection = GridViewSortDirection.ToString();

            if (!_sortDirection.Equals(String.Empty))
            {
                // Create the sorting image based on the sort direction
                Image sortImage = new Image();

                if (_sortDirection == SortOrder.Ascending.ToString())
                {
                    sortImage.ImageUrl = CommonConstants.IMAGE_UP_ARROW;
                    sortImage.AlternateText = CommonConstants.ASCENDING;
                }
                else if (_sortDirection == SortOrder.Descending.ToString())
                {
                    sortImage.ImageUrl = CommonConstants.IMAGE_DOWN_ARROW;
                    sortImage.AlternateText = CommonConstants.DESCENDING;
                }

                // Add the image to the appropriate header cell
                switch (sortExpression)
                {
                    case CommonConstants.RESOURCE_NAME:
                        headerRow.Cells[2].Controls.Add(sortImage);
                        break;
                    case CommonConstants.CURRENT_PROJECT_NAME:
                        headerRow.Cells[3].Controls.Add(sortImage);
                        break;
                    case CommonConstants.BILLING:
                        headerRow.Cells[4].Controls.Add(sortImage);
                        break;
                    case CommonConstants.UTILIZATION:
                        headerRow.Cells[5].Controls.Add(sortImage);
                        break;
                    case CommonConstants.RELEASE_DATE:
                        headerRow.Cells[6].Controls.Add(sortImage);
                        break;
                    case CommonConstants.DEPARTMENT:
                        headerRow.Cells[7].Controls.Add(sortImage);
                        break;

                }
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "AddSortImage", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Bind Grid View for Internal Resource Selection
    /// </summary>
    private void BindGrid()
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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, BIND_GRID, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Display Header When GridView Empty with proper message
    /// </summary>
    /// <param name="objListSort">EmptyList</param>
    private void ShowHeaderWhenEmptyGrid(RaveHRCollection objListSort)
    {
        try
        {
            //set header visible
            grdvListofInternalResource.ShowHeader = true;
            grdvListofInternalResource.AllowSorting = false;

            //Create empty datasource for Grid view and bind
            objListSort.Add(new BusinessEntities.MRFDetail());
            grdvListofInternalResource.DataSource = objListSort;
            grdvListofInternalResource.DataBind();

            //Calculate number of columns in Grid view used for column Span
            int columnsCount = grdvListofInternalResource.Columns.Count;

            //clear all the cells in the row
            grdvListofInternalResource.Rows[0].Cells.Clear();

            //add a new blank cell
            grdvListofInternalResource.Rows[0].Cells.Add(new TableCell());
            grdvListofInternalResource.Rows[0].Cells[0].Text = NO_RECORDS_FOUND_MESSAGE;
            grdvListofInternalResource.Rows[0].Cells[0].Wrap = false;
            grdvListofInternalResource.Rows[0].Cells[0].Width = Unit.Percentage(10);
            grdvListofInternalResource.Rows[0].Attributes.Remove("onclick");
            grdvListofInternalResource.Rows[0].Attributes.Remove("onmouseover");
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ShowHeaderWhenEmptyGrid", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Sort GridView 
    /// </summary>
    private void SortGridView(string sortExpression, string direction)
    {
        try
        {
            if (sortExpression == RESOURCE_NAME)
            {
                objParameterCriteria.SortExpressionAndDirection = sortExpression + direction;
            }
            else
            {
                objParameterCriteria.SortExpressionAndDirection = sortExpression + direction;
            }
            objListSort = GetListOfInternalResource();
            if (Session[PAGE_COUNT] != null)
                if ((int.Parse(Session[PAGE_COUNT].ToString()) == 1) && (objListSort.Count == 1))
                {
                    grdvListofInternalResource.AllowSorting = false;
                }
                else
                {
                    grdvListofInternalResource.AllowSorting = true;
                }

            if (objListSort.Count == 0)
            {
                grdvListofInternalResource.DataSource = objListSort;
                grdvListofInternalResource.DataBind();
                ShowHeaderWhenEmptyGrid(objListSort);
            }
            else
            {
                //Bind the Grid View in Sorted order
                grdvListofInternalResource.DataSource = objListSort;
                grdvListofInternalResource.DataBind();

            }

            //Finds the Paging Text Box, Previous and Next Link Buttons in Grid Views Pagning Templet
            GridViewRow gvrPager = grdvListofInternalResource.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            LinkButton lbtnPrevious = (LinkButton)gvrPager.Cells[0].FindControl("lbtnPrevious");
            LinkButton lbtnNext = (LinkButton)gvrPager.Cells[0].FindControl("lbtnNext");

            if (pageCount > 1)
            {
                grdvListofInternalResource.BottomPagerRow.Visible = true;
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
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "SortGridView", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }
    #endregion

}