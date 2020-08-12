//  ----                    -----------         ---     -----------
//  12/28/2009 3:58:30 PM  Sameer Chornele      n/a     Created    
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

public partial class MRFInternalResource : BaseClass
{

    #region Private Field Members

    /// <summary>
    /// define ASCENDING 
    /// </summary>
    private const string ASCENDING = " ASC";

    /// <summary>
    /// define DESCENDING 
    /// </summary>
    private const string DESCENDING = " DESC";

    /// <summary>
    /// declare sortExpression
    /// </summary>
    private static string sortExpression = string.Empty;

    /// <summary>
    /// define NO_RECORDS_FOUND_MESSAGE
    /// </summary>
    private const string NO_RECORDS_FOUND_MESSAGE = "No records found.";

    /// <summary>
    /// define CLASS_NAME
    /// </summary>
    private const string CLASS_NAME = "MrfPendingAllocation.aspx";

    /// <summary>
    /// declare pageCount
    /// </summary>
    private int pageCount = 0;

    /// <summary>
    /// define PREVIOUS_SORT_EXPRESSION
    /// </summary>
    private const string PREVIOUS_SORT_EXPRESSION = "Previous Sort Expression of List Of Internal Resource";

    /// <summary>
    /// define PAGE_LOAD
    /// </summary>
    private const string PAGE_LOAD = "Page_Load";

    /// <summary>
    /// define MRFLISTOFINTERNALRESOURCE
    /// </summary>
    private const string MRFLISTOFINTERNALRESOURCE = "MrfListOfInternalResource";

    /// <summary>
    /// define CURRENT_PAGE_INDEX
    /// </summary>
    private const string CURRENT_PAGE_INDEX = "currentPageIndex";

    /// <summary>
    /// define SORT_DIRECTION
    /// </summary>
    private const string SORT_DIRECTION = "sortDirection for List Of Internal Resource ";

    /// <summary>
    /// define PAGE_COUNT
    /// </summary>
    private const string PAGE_COUNT = "pageCount for List of Internal Resource";
    
    /// <summary>
    /// define RESOURCE_NAME
    /// </summary>
    private const string RESOURCE_NAME = "ResourceName";
    
    /// <summary>
    /// define SELECTED_ROW
    /// </summary>
    private const string SELECTED_ROW = "SelectedRow";

    /// <summary>
    /// define BIND_GRID
    /// </summary>
    private const string BIND_GRID = "BindGrid";

    /// <summary>
    /// declare mrfDetail
    /// </summary>
    private BusinessEntities.MRFDetail mrfDetail;

    /// <summary>
    /// declare  objParameterCriteria object
    /// </summary>
    BusinessEntities.ParameterCriteria objParameterCriteria = new BusinessEntities.ParameterCriteria();
    
    /// <summary>
    /// declare objListSort object
    /// </summary>
    RaveHRCollection objListSort = new RaveHRCollection();

    #endregion Private Field Members

    #region Protected Events

    /// <summary>
    /// Page_Load Event handler
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.AppendHeader("Pragma", "no-cache");        

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
                    //Bind the grid while Loading the page
                    BindGrid();
                }
                txtResourceName.Focus();
                //this.btnListofInternalREsources.Focus();
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

    /// <summary>
    /// grdvListofInternalResource_Sorting Event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdvListofInternalResource_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
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

    /// <summary>
    /// radioId_CheckedChanged Event Handler
    /// </summary>
    protected void radioId_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            int selected = ((System.Web.UI.WebControls.GridViewRow)(((System.Web.UI.Control)(sender)).BindingContainer)).RowIndex;
            ViewState[SELECTED_ROW] = selected;
        }
        //catches RaveHRException exception
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

    /// <summary>
    /// btnOk_Click Event Handler
    /// </summary>
    protected void btnOk_Click(object sender, EventArgs e)
    {
        try
        {
            int selected = (int)ViewState[SELECTED_ROW];
            GridViewRow dr = grdvListofInternalResource.Rows[selected];
            if (Session[SessionNames.MRFDetail] != null)
            {
                mrfDetail = (MRFDetail)Session[SessionNames.MRFDetail];                
                Label lblEmpId = (Label)dr.Cells[0].FindControl("lblEmployeeId");
                mrfDetail.EmployeeId = Convert.ToInt32(lblEmpId.Text);
                mrfDetail.EmployeeName = dr.Cells[2].Text.ToString();

                Rave.HR.BusinessLayer.MRF.MRFDetail objMrfDetail = new Rave.HR.BusinessLayer.MRF.MRFDetail();
                int EmpExist = objMrfDetail.GetEmployeeExistCheck(mrfDetail);
                if (EmpExist != 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "SelectAnotherResourceJS",
                           "alert('Please Select another resource as " + mrfDetail.EmployeeName + " is already allocated to this project.');", true);
                }
                else
                {
                    Session[SessionNames.MRFDetail] = mrfDetail;
                    Session[SessionNames.InternalResource] = mrfDetail;
                    //Aarohi : Issue 31826 : 16/12/2011 : Start
                    Session[SessionNames.RESOURCE_JOINED] = mrfDetail.EmployeeName;

                    HiddenField hdJoinigDate = (HiddenField)dr.Cells[0].FindControl("hdEmpJoinigDay");
                    mrfDetail.EmployeeJoiningDate = Convert.ToDateTime(hdJoinigDate.Value);

                    //Aarohi : Issue 31826 : 16/12/2011 : End
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "CloseJS",
                        "jQuery.modalDialog.getCurrent().close();", true);
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnOk_Click", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// btnCancel_Click Event Handler
    /// </summary>
    protected void btnCancel_Click(object sender, EventArgs e)
     {
        try
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "CloseJS",
                       "jQuery.modalDialog.getCurrent().close();", true);
        }
        //catches RaveHRException exception
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

    /// <summary>
    ///  grdvListofInternalResource_PageIndexChanging Event Handler
    /// </summary>
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

    /// <summary>
    /// grdvListofInternalResource_RowCommand Event Handler
    /// </summary>
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

    /// <summary>
    /// btnSearch_Click EventHandler
    /// </summary>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //Bind Grid
        BindGrid();
    }

    #endregion Protected Events

    #region Private Member Functions

    /// <summary>
    /// Returns MRF Details whose status is pending allocation for GridView
    /// </summary>
    /// <returns>List</returns>
    private RaveHRCollection GetListOfInternalResource()
    {
        RaveHRCollection objListMRFDetails = new RaveHRCollection();
        try
        {
            if (Session[SessionNames.MRFDetail] != null)
                mrfDetail = (MRFDetail)Session[SessionNames.MRFDetail];
            else
            {
                mrfDetail = new MRFDetail();
            }
            
            objParameterCriteria.PageSize = 10;
            objListMRFDetails = Rave.HR.BusinessLayer.MRF.MRFDetail.GetMRFInternalResource(mrfDetail,txtResourceName.Text.Trim(), objParameterCriteria, ref pageCount);
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
                    case CommonConstants.DEPARTMENT:
                        headerRow.Cells[3].Controls.Add(sortImage);
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
    /// Private Property to Get and Set direction for for sorting
    /// </summary>
    private SortDirection GridViewSortDirection
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

    #endregion Private Member Functions

}
