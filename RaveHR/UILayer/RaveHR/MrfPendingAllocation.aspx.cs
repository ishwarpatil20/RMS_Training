//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           MrfPendingAllocation.aspx      
//  Author:         Chhaya Gunjal 
//  Date written:   16/09/2009/ 10:58:30 AM
//  Description:    The MRfPendingAllocation page summarises MRF details whose status is 
//                  Pending Allocation of MRF.
//
//  Amendments
//  Date                    Who              Ref     Description
//  ----                    -----------      ---     -----------
//  03/09/2009 10:58:30 AM  Chhaya Gunjal    n/a     Created    
//
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using BusinessEntities;
using Common;
using System.Collections;
using Common.AuthorizationManager;

public partial class MrfPendingAllocation : BaseClass
{
    #region Constants
    private const string ASCENDING = " ASC";
    private const string DESCENDING = " DESC";
    private static string sortExpression = string.Empty;
    private const string NO_RECORDS_FOUND_MESSAGE = "No records found.";
    private const string CLASS_NAME = "MrfPendingAllocation.aspx";
    private const int PAGE_SIZE = 10;
    private int pageCount = 0;
    private const string MRFCode = "MRFCode";
    private const string CURRENT_PAGE_INDEX = "Current Page Index for Pending Allocation";
    private const string PROJECT_NAME = "ProjectName";
    private const string CLIENT_NAME = "ClientName";
    private const string TXT_PAGES = "txtPages";
    private const string BTN_PREVIOUS = "lbtnPrevious";
    private const string BTN_NEXT = "lbtnNext";
    private const string PAGE_COUNT = "pagecount for pending allocation";
    private const string LBL_PAGE_COUNT = "lblPageCount";
    private const string SORT_DIRECTION = "sortDirection";
    private const string PREVIOUS = "Previous";
    private const string NEXT = "Next";
    private const string PREVIOUS_SORT_EXPRESSION = "Previous Sort Expression for Pending Allocation";
    private const string RPCode = "RPCode";
    const string AllocateResourec = "AllocateResourec";
    public const string DEPARTMENT_ID = "DepartmentId for Pending allocation filter";
    public const string PROJECT_ID_MRF = "ProjectId for Pending allocation filter";
    public const string ROLE = "Role for pending allocation filter";
    public const string MRF_STATUS_ID = "Status for pending allocation filter";//18402-Ambar
    public const string TYPE_ALLOCATION = "Type Of Allocation filter";
    public const string TYPE_OF_SUPPLY = "Type Of Supply filter";
    string UserRaveDomainId;
    string UserMailId;
    #region EventNameConstants
    private const string GRDVLISTOFPENDINGALLOCATION_PAGEINDEXCHANGING = "grdvListofPendingAllocation_PageIndexChanging";
    private const string PAGE_LOAD = "Page_Load";
    private const string GRDVLISTOFPENDINGALLOCATION_ROWCREATED = "grdvListofPendingAllocation_RowCreated";
    private const string GRDVLISTOFPENDINGALLOCATION_SORTING = "grdvListofPendingAllocation_Sorting";
    private const string TXTPAGES_TEXTCHANGED = "txtPages_TextChanged";
    private const string GRDVLISTOFPENDINGALLOCATION_ROWDATABOUND = "grdvListofPendingAllocation_RowDataBound";
    private const string GRDVLISTOFPENDINGALLOCATION_DATABOUND = "grdvListofPendingAllocation_DataBound";
    private const string BTNREMOVEFILTER_CLICK = "btnRemoveFilter_Click";
    private const string GRDVLISTOFPENDINGALLOCATION_ROWCOMMAND = "grdvListofPendingAllocation_RowCommand";
    private const string BTNFILTERCLICK = "btnFilter_Click";
    private const string DDLDEPARTMENT_SELECTEDINDEXCHANGED = "ddlDepartment_SelectedIndexChanged";
    private const string CHANGE_PAGE = "ChangePage";
    #endregion

    #region MethodNameConstants
    private const string BINDGRID = "BindGrid";
    private const string CLEARFILTERFIELDS = "ClearFilteringFields";
    private const string GRIDVIEWSORTDIRECTION = "GridViewSortDirection";
    private const string SORTGRIDVIEW = "SortGridView";
    private const string ADDSORTIMAGE = "AddSortImage";
    private const string SHOWHEADERWHENEMPTYGRID = "ShowHeaderWhenEmptyGrid";
    private const string GETMRFDETAILSFORPENDINGALLOCATION = "GetMRFDetailsForPendingAllocation";
    private const string FILLROLEDROPDOWNASPERDEPARTMENT = "FillRoleDropdownAsPerDepartment";
    private const string FILLPROJECTNAMEDROPDOWN = "FillProjectNameDropDown";
    private const string FILLROLEDROPDOWN = "FillRoleDropDown";
    private const string FILLDEPARTMENTDROPDOWN = "FillDepartmentDropDown";
    private const string FILLDROPDOWNS = "FillDropDowns";
    private const string FILLSTATUSDROPDOWN = "FillStatusDropDown";//18402-Ambar

    private const string FILLTYPEOFALLOCATIONDROPDOWN = "FillTypeOfAllocationDropDown";
    private const string FILLTYPEOFSUPPLYRDROPDOWN = "FillTypeOfSupplyDropDown";

    #endregion


    #endregion

    #region Public Field Members

    BusinessEntities.ParameterCriteria objParameterCriteria = new BusinessEntities.ParameterCriteria();
    RaveHRCollection objListSort = new RaveHRCollection();
    /// <summary>
    /// Store the Value of row count
    /// </summary>
    Hashtable hashTable = new Hashtable();

    /// <summary>
    /// Define projectClientName  
    /// </summary>
    public string ProjectClientName = string.Empty;

    #endregion Public Field Members

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Make filter button as a default button.
            Page.Form.DefaultButton = btnFilter.UniqueID;
            btnRaiseHeadCount.Attributes.Add(Common.CommonConstants.EVENT_ONCLICK, "return IsCheckBoxChecked();");

            if (!ValidateURL())
            {
                Response.Redirect(CommonConstants.INVALIDURL);
            }
            else
            {
                //Siddharth 26th August 2015 Start
                //Task ID:- 56487 Hide the pages access for normal employees
                ArrayList arrRolesForUser = new ArrayList();
                AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
                UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
                UserMailId = UserRaveDomainId.Replace("co.in", "com");
                //AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
                arrRolesForUser = objRaveHRAuthorizationManager.getRolesForUser(UserRaveDomainId);

                if (!(arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM)))
                {
                    Response.Redirect(CommonConstants.PAGE_HOME, false);
                }               
                //Siddharth 26th August 2015 End


                if (Session[SessionNames.CONFIRMATION_MESSAGE] != null)
                {
                    string message = Convert.ToString(Session[SessionNames.CONFIRMATION_MESSAGE]);
                    Session.Remove(SessionNames.CONFIRMATION_MESSAGE);
                    lblMessage.Visible = true;
                    lblMessage.Text = message;
                }
                else
                {
                    lblMessage.Visible = false;
                    lblMessage.Text = "";
                }


                if (!IsPostBack)
                {
                    //Session[CURRENT_PAGE_INDEX] = 1;
                    if (Session[SessionNames.CURRENT_PAGE_INDEX_PENDING_ALLOCATION] == null)
                    {
                        Session[SessionNames.CURRENT_PAGE_INDEX_PENDING_ALLOCATION] = 1;
                    }

                    if (Session[SessionNames.MRFVIEWINDEX] != null)
                    {
                        Session.Remove(SessionNames.MRFVIEWINDEX);
                    }

                    // Ambar-Start
                    // IF Session[SessionNames.CURRENT_PAGE_INDEX_MRF] is not initiated on MRF Summary Page then Initiate it here
                    if (Session[SessionNames.CURRENT_PAGE_INDEX_MRF] == null)
                    {
                      Session[SessionNames.CURRENT_PAGE_INDEX_MRF] = 1;
                    }
                    // Ambar-Start

                    if (Session[PREVIOUS_SORT_EXPRESSION] == null)
                    {
                        sortExpression = PROJECT_NAME;
                        Session[PREVIOUS_SORT_EXPRESSION] = sortExpression;
                    }
                    else
                    {
                        sortExpression = Session[PREVIOUS_SORT_EXPRESSION].ToString();
                    }
                    //if page is not calling from Allocate Resource Screen.
                    //if (Request.QueryString[AllocateResourec] == null)
                    //{
                    //    Session[PROJECT_ID_MRF] = null;
                    //    Session[DEPARTMENT_ID] = null;
                    //    Session[ROLE] = null;
                    //}
                    if (Session[PROJECT_ID_MRF] != null || Session[DEPARTMENT_ID] != null || Session[ROLE] != null)
                    {
                        btnRemoveFilter.Visible = true;
                    }

                    FillDropDowns();
                    //Bind the grid while Loading the page
                    BindGrid();
                    grdvListofPendingAllocation.Columns[1].Visible = false;
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

    protected void grdvListofPendingAllocation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //Change the page index on page index changing event and bind the grid
            if (e.NewPageIndex != -1)
            {
                grdvListofPendingAllocation.PageIndex = e.NewPageIndex;
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, GRDVLISTOFPENDINGALLOCATION_PAGEINDEXCHANGING, EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdvListofPendingAllocation_RowCreated(object sender, GridViewRowEventArgs e)
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
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, GRDVLISTOFPENDINGALLOCATION_ROWCREATED, EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdvListofPendingAllocation_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            GridViewRow gvrPager = grdvListofPendingAllocation.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl(TXT_PAGES);
            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_PENDING_ALLOCATION].ToString();

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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, GRDVLISTOFPENDINGALLOCATION_SORTING, EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
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
                grdvListofPendingAllocation.PageIndex = Convert.ToInt32(txtPages.Text) - 1;
                Session[SessionNames.CURRENT_PAGE_INDEX_PENDING_ALLOCATION] = txtPages.Text;
            }
            else
            {
                txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_PENDING_ALLOCATION].ToString();
                return;
            }
            //Bind the grid on paging
            BindGrid();
            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_PENDING_ALLOCATION].ToString();
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, TXTPAGES_TEXTCHANGED, EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdvListofPendingAllocation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
              //   Sanju:Issue Id 50201: Changed cursor property
                // Mohamed : NIS-RMS : 07/01/2015 : Starts                        			  
                // Desc : Remove underline and click from remaining columns

                e.Row.Cells[2].Attributes[CommonConstants.EVENT_ONMOUSEOVER] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                //Sanju:Issue Id 50201:End

                e.Row.Cells[2].Attributes[CommonConstants.EVENT_ONMOUSEOUT] = "this.style.textDecoration='none';";

                GridView _gridView = (GridView)sender;
                string url = "MrfView.aspx?";
                //e.Row.Attributes.Add(CommonConstants.EVENT_ONCLICK, "sendWindow('" + url + URLHelper.SecureParameters("AllocateResourec", "True") + "&" + URLHelper.SecureParameters("MRFID", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString()) + "&" + URLHelper.SecureParameters("ClientName", ProjectClientName) + "&" + URLHelper.SecureParameters("index", e.Row.RowIndex.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "MRFPendingAllocation") + "&" + URLHelper.CreateSignature("True", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString(), ProjectClientName, e.Row.RowIndex.ToString(), "MRFPendingAllocation") + "')");
                // e.Row.Cells[0].Attributes[CommonConstants.EVENT_ONMOUSEOVER] = "this.style.cursor='';";
                //e.Row.Cells[1].Attributes.Add(CommonConstants.EVENT_ONCLICK, "sendWindow('" + url + URLHelper.SecureParameters("AllocateResourec", "True") + "&" + URLHelper.SecureParameters("MRFID", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString()) + "&" + URLHelper.SecureParameters("ClientName", ProjectClientName) + "&" + URLHelper.SecureParameters("index", e.Row.RowIndex.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "MRFPendingAllocation") + "&" + URLHelper.CreateSignature("True", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString(), ProjectClientName, e.Row.RowIndex.ToString(), "MRFPendingAllocation") + "')");
                e.Row.Cells[2].Attributes.Add(CommonConstants.EVENT_ONCLICK, "sendWindow('" + url + URLHelper.SecureParameters("AllocateResourec", "True") + "&" + URLHelper.SecureParameters("MRFID", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString()) + "&" + URLHelper.SecureParameters("ClientName", ProjectClientName) + "&" + URLHelper.SecureParameters("index", e.Row.RowIndex.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "MRFPendingAllocation") + "&" + URLHelper.CreateSignature("True", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString(), ProjectClientName, e.Row.RowIndex.ToString(), "MRFPendingAllocation") + "')");
                //e.Row.Cells[3].Attributes.Add(CommonConstants.EVENT_ONCLICK, "sendWindow('" + url + URLHelper.SecureParameters("AllocateResourec", "True") + "&" + URLHelper.SecureParameters("MRFID", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString()) + "&" + URLHelper.SecureParameters("ClientName", ProjectClientName) + "&" + URLHelper.SecureParameters("index", e.Row.RowIndex.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "MRFPendingAllocation") + "&" + URLHelper.CreateSignature("True", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString(), ProjectClientName, e.Row.RowIndex.ToString(), "MRFPendingAllocation") + "')");
                //e.Row.Cells[4].Attributes.Add(CommonConstants.EVENT_ONCLICK, "sendWindow('" + url + URLHelper.SecureParameters("AllocateResourec", "True") + "&" + URLHelper.SecureParameters("MRFID", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString()) + "&" + URLHelper.SecureParameters("ClientName", ProjectClientName) + "&" + URLHelper.SecureParameters("index", e.Row.RowIndex.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "MRFPendingAllocation") + "&" + URLHelper.CreateSignature("True", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString(), ProjectClientName, e.Row.RowIndex.ToString(), "MRFPendingAllocation") + "')");
                //e.Row.Cells[5].Attributes.Add(CommonConstants.EVENT_ONCLICK, "sendWindow('" + url + URLHelper.SecureParameters("AllocateResourec", "True") + "&" + URLHelper.SecureParameters("MRFID", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString()) + "&" + URLHelper.SecureParameters("ClientName", ProjectClientName) + "&" + URLHelper.SecureParameters("index", e.Row.RowIndex.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "MRFPendingAllocation") + "&" + URLHelper.CreateSignature("True", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString(), ProjectClientName, e.Row.RowIndex.ToString(), "MRFPendingAllocation") + "')");
                //e.Row.Cells[6].Attributes.Add(CommonConstants.EVENT_ONCLICK, "sendWindow('" + url + URLHelper.SecureParameters("AllocateResourec", "True") + "&" + URLHelper.SecureParameters("MRFID", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString()) + "&" + URLHelper.SecureParameters("ClientName", ProjectClientName) + "&" + URLHelper.SecureParameters("index", e.Row.RowIndex.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "MRFPendingAllocation") + "&" + URLHelper.CreateSignature("True", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString(), ProjectClientName, e.Row.RowIndex.ToString(), "MRFPendingAllocation") + "')");
                //e.Row.Cells[7].Attributes.Add(CommonConstants.EVENT_ONCLICK, "sendWindow('" + url + URLHelper.SecureParameters("AllocateResourec", "True") + "&" + URLHelper.SecureParameters("MRFID", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString()) + "&" + URLHelper.SecureParameters("ClientName", ProjectClientName) + "&" + URLHelper.SecureParameters("index", e.Row.RowIndex.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "MRFPendingAllocation") + "&" + URLHelper.CreateSignature("True", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString(), ProjectClientName, e.Row.RowIndex.ToString(), "MRFPendingAllocation") + "')");
                //e.Row.Cells[8].Attributes.Add(CommonConstants.EVENT_ONCLICK, "sendWindow('" + url + URLHelper.SecureParameters("AllocateResourec", "True") + "&" + URLHelper.SecureParameters("MRFID", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString()) + "&" + URLHelper.SecureParameters("ClientName", ProjectClientName) + "&" + URLHelper.SecureParameters("index", e.Row.RowIndex.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "MRFPendingAllocation") + "&" + URLHelper.CreateSignature("True", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString(), ProjectClientName, e.Row.RowIndex.ToString(), "MRFPendingAllocation") + "')");
                //e.Row.Cells[9].Attributes.Add(CommonConstants.EVENT_ONCLICK, "sendWindow('" + url + URLHelper.SecureParameters("AllocateResourec", "True") + "&" + URLHelper.SecureParameters("MRFID", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString()) + "&" + URLHelper.SecureParameters("ClientName", ProjectClientName) + "&" + URLHelper.SecureParameters("index", e.Row.RowIndex.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "MRFPendingAllocation") + "&" + URLHelper.CreateSignature("True", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString(), ProjectClientName, e.Row.RowIndex.ToString(), "MRFPendingAllocation") + "')");
                //e.Row.Cells[10].Attributes.Add(CommonConstants.EVENT_ONCLICK, "sendWindow('" + url + URLHelper.SecureParameters("AllocateResourec", "True") + "&" + URLHelper.SecureParameters("MRFID", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString()) + "&" + URLHelper.SecureParameters("ClientName", ProjectClientName) + "&" + URLHelper.SecureParameters("index", e.Row.RowIndex.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "MRFPendingAllocation") + "&" + URLHelper.CreateSignature("True", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString(), ProjectClientName, e.Row.RowIndex.ToString(), "MRFPendingAllocation") + "')");
                //e.Row.Cells[11].Attributes.Add(CommonConstants.EVENT_ONCLICK, "sendWindow('" + url + URLHelper.SecureParameters("AllocateResourec", "True") + "&" + URLHelper.SecureParameters("MRFID", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString()) + "&" + URLHelper.SecureParameters("ClientName", ProjectClientName) + "&" + URLHelper.SecureParameters("index", e.Row.RowIndex.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "MRFPendingAllocation") + "&" + URLHelper.CreateSignature("True", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString(), ProjectClientName, e.Row.RowIndex.ToString(), "MRFPendingAllocation") + "')");
                //e.Row.Cells[12].Attributes.Add(CommonConstants.EVENT_ONCLICK, "sendWindow('" + url + URLHelper.SecureParameters("AllocateResourec", "True") + "&" + URLHelper.SecureParameters("MRFID", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString()) + "&" + URLHelper.SecureParameters("ClientName", ProjectClientName) + "&" + URLHelper.SecureParameters("index", e.Row.RowIndex.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "MRFPendingAllocation") + "&" + URLHelper.CreateSignature("True", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString(), ProjectClientName, e.Row.RowIndex.ToString(), "MRFPendingAllocation") + "')");
                //e.Row.Cells[13].Attributes.Add(CommonConstants.EVENT_ONCLICK, "sendWindow('" + url + URLHelper.SecureParameters("AllocateResourec", "True") + "&" + URLHelper.SecureParameters("MRFID", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString()) + "&" + URLHelper.SecureParameters("ClientName", ProjectClientName) + "&" + URLHelper.SecureParameters("index", e.Row.RowIndex.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "MRFPendingAllocation") + "&" + URLHelper.CreateSignature("True", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString(), ProjectClientName, e.Row.RowIndex.ToString(), "MRFPendingAllocation") + "')");
                url = "MrfRaisePrevious.aspx?";
                e.Row.Cells[15].Attributes.Add(CommonConstants.EVENT_ONCLICK, "sendWindow('" + url + URLHelper.SecureParameters("MoveMRF", "True") + "&" + URLHelper.SecureParameters("MRFID", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString()) + "&" + URLHelper.SecureParameters("ClientName", ProjectClientName) + "&" + URLHelper.SecureParameters("index", e.Row.RowIndex.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "MoveMRF") + "&" + URLHelper.CreateSignature("True", DataBinder.Eval(e.Row.DataItem, "MRFId").ToString(), ProjectClientName, e.Row.RowIndex.ToString(), "MoveMRF") + "')");

                // Mohamed : NIS-RMS : 07/01/2015 : Ends

                //e.Row.Cells[0].Attributes.Add(CommonConstants.EVENT_ONCLICK, "return buClick();");
                // e.Row.Cells[0].Attributes.Remove(CommonConstants.EVENT_ONCLICK);
                //e.Row.Cells[0].Attributes[CommonConstants.EVENT_ONCLICK] = "this.style.cursor='';";

                //e.Row.Cells[0].Attributes.Remove(CommonConstants.EVENT_ONMOUSEOUT);
                // e.Row.Cells[0].Attributes[CommonConstants.EVENT_ONMOUSEOVER] = "this.style.textDecoration='none';";
                hashTable.Add(e.Row.RowIndex, DataBinder.Eval(e.Row.DataItem, "MRFID").ToString());

            }

            Session[SessionNames.MRFVIEWINDEX] = hashTable;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, GRDVLISTOFPENDINGALLOCATION_ROWDATABOUND, EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdvListofPendingAllocation_DataBound(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrPager = grdvListofPendingAllocation.BottomPagerRow;
            if (gvrPager == null) return;

            //Get Text Box and Lable from the Grid View
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl(TXT_PAGES);
            Label lblPageCount = (Label)gvrPager.Cells[0].FindControl(LBL_PAGE_COUNT);

            //Assign current page index to text box
            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_PENDING_ALLOCATION].ToString();

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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, GRDVLISTOFPENDINGALLOCATION_DATABOUND, EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnRemoveFilter_Click(object sender, EventArgs e)
    {
        try
        {
            // Assign DepartmentId to Session
            Session[DEPARTMENT_ID] = null;
            // Assign ProjectId to Session
            Session[PROJECT_ID_MRF] = null;

            // Assign RoleId to Session
            Session[ROLE] = null;
            //18402-Ambar-Assign MrfStatusId to Session
            Session[SessionNames.MRF_STATUS_ID] = null;

            //User is directed to 1st page
            Session[SessionNames.CURRENT_PAGE_INDEX_PENDING_ALLOCATION] = 1;
            sortExpression = PROJECT_NAME;

            ClearFilteringFields();
            BindGrid();
            btnRemoveFilter.Visible = false;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, BTNREMOVEFILTER_CLICK, EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdvListofPendingAllocation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //--Add sort image in header
            if (Session[PAGE_COUNT] != null)
                if (int.Parse(Session[PAGE_COUNT].ToString()) == 1)
                {
                    AddSortImage(grdvListofPendingAllocation.HeaderRow);
                }
            GridView _gridView = (GridView)sender;
            if (e.CommandName != "Sort")
            {
                int _selectedIndex = int.Parse(e.CommandArgument.ToString());
                string _commandName = e.CommandName;
                grdvListofPendingAllocation.SelectedIndex = _selectedIndex;
                //GridViewRow dr = grdvListofPendingAllocation.Rows[_selectedIndex];
            }
            
            //if (e.CommandName == CommonConstants.MOVEMRF)
            //{
            //    string mrfId = e.CommandArgument.ToString();
            //    string i = grdvListofPendingAllocation.SelectedIndex.ToString();
            //    //grdvListofPendingAllocation.SelectedRow.Cells.ToString();
            //    //grdvListofPendingAllocation.SelectedDataKey.Value.ToString();
            //    //grdvListofPendingAllocation.SelectedDataKey.Values.ToString();
            //   // GridViewRow row = grdvListofPendingAllocation.SelectedRow;
            //    //row.Cells[1].ToString();
            //    //
            //  //  Server.Transfer(URLHelper.SecureParameters("MrfRaisePrevious.aspx", "True") + "&" + URLHelper.SecureParameters("MRFID", mrfId));
                
            //    //DataBinder.Eval(mrfId//grdvListofPendingAllocation.SelectedIndex.GetTypeCode()
            //        //,e.Row.DataItem
            //        //, "MRFId").ToString())); 
            //        //+ "&" + URLHelper.SecureParameters("ClientName", ProjectClientName) + "&" + URLHelper.SecureParameters("index", 
            //        //e.Row.RowIndex.ToString()
            //        //"") + "&" + URLHelper.SecureParameters("pagetype", "MRFPendingAllocation") + "&" + URLHelper.CreateSignature("True", DataBinder.Eval(
            //        //e.Row.DataItem
            //        //6, "MRFId").ToString(), ProjectClientName, 
            //        //e.Row.RowIndex.ToString()
            //        //"", "MRFPendingAllocation"));
            //}
             
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, GRDVLISTOFPENDINGALLOCATION_ROWCOMMAND, EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            // Assign DepartmentId to Session
            Session[DEPARTMENT_ID] = ddlDepartment.SelectedItem.Value;
            // Assign ProjectId to Session
            Session[PROJECT_ID_MRF] = ddlProjectName.SelectedItem.Value;
            // Assign RoleId to Session
            Session[ROLE] = ddlRole.SelectedItem.Value;
            //18402-Ambar-Assign MrfStatusId to Session
            Session[MRF_STATUS_ID] = ddlStatus.SelectedItem.Value;
            Session[TYPE_ALLOCATION] = ddlTypeOfAllocation.SelectedItem.Value;
            Session[TYPE_OF_SUPPLY] = ddlTypeOfSupply.SelectedItem.Value;

            if (grdvListofPendingAllocation.BottomPagerRow != null)
            {
                GridViewRow gvrPager = grdvListofPendingAllocation.BottomPagerRow;
                TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");


                if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0)
                {
                    txtPages.Text = 1.ToString();
                    Session[SessionNames.CURRENT_PAGE_INDEX_PENDING_ALLOCATION] = txtPages.Text;
                }
            }

            BindGrid();

            Session[PREVIOUS_SORT_EXPRESSION] = sortExpression;

            if (Session[PROJECT_ID_MRF] != null || Session[DEPARTMENT_ID] != null || Session[ROLE] != null)
            {
                btnRemoveFilter.Visible = true;
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, BTNFILTERCLICK, EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlProjectName.ClearSelection();
        if (ddlDepartment.SelectedValue != CommonConstants.SELECT)
        {
            FillRoleDropdownAsPerDepartment();
            FillTypeOfAllocationDropDown();
            ddlTypeOfAllocation.Enabled = true;
        }
        else
        {
            ddlProjectName.Enabled = false;
            ddlRole.Enabled = false;
            ddlTypeOfAllocation.Enabled = false;
            ClearFilteringFields();

        }

        // 34445-Ambar-09062012-Modified IF condition for data type compatability

        //if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.RaveConsultant_India)
        //          || int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.RaveConsultant_UK)
        //          || int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.RaveConsultant_USA)
        //          || ddlDepartment.SelectedItem.Text == CommonConstants.PROJECTS)
        if ( ddlDepartment.SelectedValue.ToString() == Convert.ToString(MasterEnum.Departments.RaveConsultant_India)
          || ddlDepartment.SelectedValue.ToString() == Convert.ToString(MasterEnum.Departments.RaveConsultant_UK)
          || ddlDepartment.SelectedValue.ToString() == Convert.ToString(MasterEnum.Departments.RaveConsultant_USA)
          || ddlDepartment.SelectedItem.Text == CommonConstants.PROJECTS)
        {
            ddlTypeOfAllocation.Enabled = true;
        }
        else
        {
            ddlTypeOfAllocation.Enabled = false;
        }

    }

    protected void ChangePage(object sender, CommandEventArgs e)
    {
        GridViewRow gvrPager = grdvListofPendingAllocation.BottomPagerRow;
        TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl(TXT_PAGES);
        switch (e.CommandName)
        {
            case PREVIOUS:
                Session[SessionNames.CURRENT_PAGE_INDEX_PENDING_ALLOCATION] = Convert.ToInt32(txtPages.Text) - 1;
                txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) - 1);
                break;

            case NEXT:
                Session[SessionNames.CURRENT_PAGE_INDEX_PENDING_ALLOCATION] = Convert.ToInt32(txtPages.Text) + 1;
                txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) + 1);
                break;
        }
        BindGrid();
    }

    /// <summary>
    /// Raise the headcount for selected MRF.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRaiseHeadCount_Click(object sender, EventArgs e)
    {
        string MRFId = string.Empty;
        string MRFIdCollection = string.Empty;
        RaveHRCollection raveHRCollection = new RaveHRCollection();
        Rave.HR.BusinessLayer.MRF.MRFDetail MRFDetailsBL = new Rave.HR.BusinessLayer.MRF.MRFDetail();

        try
        {
            for (int i = 0; i < grdvListofPendingAllocation.Rows.Count; i++)
            {
                CheckBox chbTemp = grdvListofPendingAllocation.Rows[i].FindControl("cbxMRFID") as CheckBox;

                if (chbTemp.Checked)
                {
                    HiddenField hfMRFID = grdvListofPendingAllocation.Rows[i].FindControl("hfMRFID") as HiddenField;
                    MRFId = hfMRFID.Value;
                    //MRFId = grdvListofPendingAllocation.Rows[i].Cells[1].Text ;
                    raveHRCollection = MRFDetailsBL.CopyMRFBL(Convert.ToInt32(MRFId));

                    foreach (BusinessEntities.MRFDetail objMRFDetails in raveHRCollection)
                    {
                        //Utility.GetUrl() +
                        hidEncryptedQueryString.Value =
                            "MrfRaiseHeadCount.aspx?"
                            + URLHelper.SecureParameters("MrfId", MRFId.ToString())
                            + "&" + URLHelper.SecureParameters("ProjectName", objMRFDetails.ProjectName)
                            + "&" + URLHelper.SecureParameters("Role", objMRFDetails.Role)
                            + "&" + URLHelper.SecureParameters("Exp", objMRFDetails.ExperienceString)
                            + "&" + URLHelper.SecureParameters("TargetCTC", objMRFDetails.MRFCTCString)
                            + "&" + URLHelper.SecureParameters("Dept", objMRFDetails.DepartmentName)
                            + "&" + URLHelper.SecureParameters("MrfCode", objMRFDetails.MRFCode)
                            + "&" + URLHelper.SecureParameters("SLADays", GetSLADaysByMrfId(Convert.ToInt32(MRFId)))
                            + "&" + URLHelper.CreateSignature(MRFId.ToString(),
                                                              objMRFDetails.ProjectName,
                                                              objMRFDetails.Role,
                                                              objMRFDetails.ExperienceString,
                                                              objMRFDetails.MRFCTCString,
                                                              objMRFDetails.DepartmentName,
                                                              objMRFDetails.MRFCode,
                                                              GetSLADaysByMrfId(Convert.ToInt32(MRFId)));
                    }
                    MRFIdCollection += MRFId + ",";
                }
            }
            if (MRFIdCollection.Length > 0)
                MRFIdCollection = MRFIdCollection.Substring(0, MRFIdCollection.Length - 1);

            Session[SessionNames.MRFRaiseHeadCOuntGroup] = MRFIdCollection;
            string script = "<script type='text/javascript'> window.open('" + hidEncryptedQueryString.Value + "', null, 'height=400,width=500,left=170,top=150'); </script>";

            //window.showModalDialog(" + hidEncryptedQueryString.Value + ", null, "dialogHeight:400px; dialogLeft:15px; dialogWidth:550px;");

            //Page.RegisterClientScriptBlock("clientScript",script );
            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "clientScript", script);
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnRaiseHeadCount_Click", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    #endregion  Events

    #region Methods
    /// <summary>
    /// Binds grid in sorted order in given direction
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
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, BINDGRID, EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);

        }
    }

    /// <summary>
    /// Clearing the filter fields
    /// </summary>
    private void ClearFilteringFields()
    {
        try
        {
            ddlDepartment.SelectedIndex = CommonConstants.ZERO;
            ddlProjectName.SelectedIndex = CommonConstants.ZERO;
            ddlRole.SelectedIndex = CommonConstants.ZERO;
            ddlStatus.SelectedIndex = CommonConstants.ZERO; //issue -18402 
            ddlTypeOfAllocation.SelectedIndex = CommonConstants.ZERO;
            ddlTypeOfSupply.SelectedIndex = CommonConstants.ZERO;

        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, CLEARFILTERFIELDS, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
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
            if (sortExpression == PROJECT_NAME)
            {
                objParameterCriteria.SortExpressionAndDirection = sortExpression + direction;
            }
            else
            {
                objParameterCriteria.SortExpressionAndDirection = sortExpression + direction;
            }
            objListSort = GetMRFDetailsForPendingAllocation();

            //Get ClientName for the MRF
            foreach (BusinessEntities.MRFDetail objMrfDetail in objListSort)
            {
                if (!string.IsNullOrEmpty(objMrfDetail.ClientName))
                    ProjectClientName = objMrfDetail.ClientName;
            }

            if ((int.Parse(Session[PAGE_COUNT].ToString()) == 1) && (objListSort.Count == 1))
            {
                grdvListofPendingAllocation.AllowSorting = false;
            }
            else
            {
                grdvListofPendingAllocation.AllowSorting = true;
            }

            if (objListSort.Count == 0)
            {
                grdvListofPendingAllocation.DataSource = objListSort;
                grdvListofPendingAllocation.DataBind();
                ShowHeaderWhenEmptyGrid(objListSort);
            }
            else
            {
                //Bind the Grid View in Sorted order
                grdvListofPendingAllocation.DataSource = objListSort;
                grdvListofPendingAllocation.DataBind();
            }

            //Finds the Paging Text Box, Previous and Next Link Buttons in Grid Views Pagning Templet
            GridViewRow gvrPager = grdvListofPendingAllocation.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl(TXT_PAGES);
            LinkButton lbtnPrevious = (LinkButton)gvrPager.Cells[0].FindControl(BTN_PREVIOUS);
            LinkButton lbtnNext = (LinkButton)gvrPager.Cells[0].FindControl(BTN_NEXT);

            if (pageCount > 1)
            {
                grdvListofPendingAllocation.BottomPagerRow.Visible = true;
            }

            //Don't allow any character other than Number in paging text box
            txtPages.Attributes.Add(CommonConstants.EVENT_ONKEYPRESS, "return isNumberKey(event)");

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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, SORTGRIDVIEW, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Adds the sorting image
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
                    case MRFCode:
                        headerRow.Cells[1].Controls.Add(sortImage);
                        break;
                    //case RPCode:
                    //    headerRow.Cells[2].Controls.Add(sortImage);
                    //    break;
                    case CommonConstants.ROLE:
                        headerRow.Cells[3].Controls.Add(sortImage);
                        break;
                    case CLIENT_NAME:
                        headerRow.Cells[4].Controls.Add(sortImage);
                        break;
                    case PROJECT_NAME:
                        headerRow.Cells[5].Controls.Add(sortImage);
                        break;
                    case CommonConstants.RESOURCE_ON_BOARD:
                        headerRow.Cells[6].Controls.Add(sortImage);
                        break;
                    case CommonConstants.MRF_RAISED_BY:
                        headerRow.Cells[7].Controls.Add(sortImage);
                        break;
                    case CommonConstants.STATUS:
                        headerRow.Cells[8].Controls.Add(sortImage);
                        break;
                    case CommonConstants.DEPT_NAME:
                        headerRow.Cells[9].Controls.Add(sortImage);
                        break;
                    case CommonConstants.RECRUITERNAME:
                        headerRow.Cells[10].Controls.Add(sortImage);
                        break;
                    case CommonConstants.TYPE_OF_SUPPLY:
                        headerRow.Cells[11].Controls.Add(sortImage);
                        break;
                    case CommonConstants.TYPE_OF_ALLOCATION:
                        headerRow.Cells[12].Controls.Add(sortImage);
                        break;
                    case CommonConstants.FUTURE_Allocate_ResourcName:
                        headerRow.Cells[13].Controls.Add(sortImage);
                        break;
                    case CommonConstants.FUTURE_ALLOCATION_DATE:
                        headerRow.Cells[14].Controls.Add(sortImage);
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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, ADDSORTIMAGE, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
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
            grdvListofPendingAllocation.ShowHeader = true;
            grdvListofPendingAllocation.AllowSorting = false;

            //Create empty datasource for Grid view and bind
            objListSort.Add(new BusinessEntities.MRFDetail());
            grdvListofPendingAllocation.DataSource = objListSort;
            grdvListofPendingAllocation.DataBind();

            //Calculate number of columns in Grid view used for column Span
            int columnsCount = grdvListofPendingAllocation.Columns.Count;

            //clear all the cells in the row
            grdvListofPendingAllocation.Rows[0].Cells.Clear();

            //add a new blank cell
            grdvListofPendingAllocation.Rows[0].Cells.Add(new TableCell());
            grdvListofPendingAllocation.Rows[0].Cells[0].Text = NO_RECORDS_FOUND_MESSAGE;
            grdvListofPendingAllocation.Rows[0].Cells[0].Wrap = false;
            grdvListofPendingAllocation.Rows[0].Cells[0].Width = Unit.Percentage(10);
            grdvListofPendingAllocation.Rows[0].Attributes.Remove(CommonConstants.EVENT_ONCLICK);
            grdvListofPendingAllocation.Rows[0].Attributes.Remove(CommonConstants.EVENT_ONMOUSEOVER);
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, SHOWHEADERWHENEMPTYGRID, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Returns MRF Details whose status is pending allocation for GridView
    /// </summary>
    /// <returns>List</returns>
    private RaveHRCollection GetMRFDetailsForPendingAllocation()
    {
        RaveHRCollection objListMRFDetails = new RaveHRCollection();
        MRFDetail mrfDetail = new MRFDetail();
        try
        {
            if (grdvListofPendingAllocation.BottomPagerRow != null)
            {
                GridViewRow gvrPager = grdvListofPendingAllocation.BottomPagerRow;
                TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl(TXT_PAGES);
                //if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0 && Convert.ToInt32(txtPages.Text) <= Convert.ToInt32(Session[PAGE_COUNT].ToString()))
                //{
                //    objParameterCriteria.PageNumber = int.Parse(txtPages.Text);
                //}
                if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0)
                {
                    //objParameter.PageNumber = int.Parse(txtPages.Text);
                    objParameterCriteria.PageNumber = Convert.ToInt32(Session[SessionNames.CURRENT_PAGE_INDEX_PENDING_ALLOCATION].ToString());
                    objParameterCriteria.PageSize = 10;
                }
            }
            else
            {
                //objParameterCriteria.PageNumber = 1;
                objParameterCriteria.PageNumber = Convert.ToInt32(Session[SessionNames.CURRENT_PAGE_INDEX_PENDING_ALLOCATION].ToString());
                objParameterCriteria.PageSize = 10;
            }
            objParameterCriteria.PageSize = PAGE_SIZE;

            if (Session[PROJECT_ID_MRF] == null || Session[PROJECT_ID_MRF].ToString() == CommonConstants.SELECT)
            {
                mrfDetail.ProjectId = 0;
            }
            else
            {
                mrfDetail.ProjectId = int.Parse(Session[PROJECT_ID_MRF].ToString());
            }
            if (Session[ROLE] == null || Session[ROLE].ToString() == CommonConstants.SELECT)
            {
                mrfDetail.RoleId = 0;
            }
            else
            {
                mrfDetail.RoleId = int.Parse(Session[ROLE].ToString());
            }

            if (Session[DEPARTMENT_ID] == null || Session[DEPARTMENT_ID].ToString() == CommonConstants.SELECT)
            {
                mrfDetail.DepartmentId = 0;
            }
            else
            {
                mrfDetail.DepartmentId = int.Parse(Session[DEPARTMENT_ID].ToString());
            }

            //18402-Start
            if (Session[MRF_STATUS_ID] == null || Session[MRF_STATUS_ID].ToString() == CommonConstants.SELECT)
            {
                mrfDetail.StatusId = 0;
            }
            else
            {
                mrfDetail.StatusId = int.Parse(Session[MRF_STATUS_ID].ToString());
            }
            //18402-End


            if (Session[TYPE_ALLOCATION] == null || Session[TYPE_ALLOCATION].ToString() == CommonConstants.SELECT)
            {
                mrfDetail.TypeOfAllocation = 0;
            }
            else
            {
                mrfDetail.TypeOfAllocation = int.Parse(Session[TYPE_ALLOCATION].ToString());
            }

            if (Session[TYPE_OF_SUPPLY] == null || Session[TYPE_OF_SUPPLY].ToString() == CommonConstants.SELECT)
            {
                mrfDetail.TypeOfSupply = 0;
            }
            else
            {
                mrfDetail.TypeOfSupply = int.Parse(Session[TYPE_OF_SUPPLY].ToString());
            }

            if (Session[PROJECT_ID_MRF] == null && Session[ROLE] == null && Session[DEPARTMENT_ID] == null)
            {
                // Method for Pageload
                objListMRFDetails = Rave.HR.BusinessLayer.MRF.MRFDetail.GetMRFDetailsForPendingAllocation(objParameterCriteria, ref pageCount);
            }
            else
            {
                // Method for Filter
                objListMRFDetails = Rave.HR.DataAccessLayer.MRF.MRFDetail.GetMRFDetailsForPendingAllocationWithFilter(objParameterCriteria, mrfDetail, ref pageCount);
            }

            Session[PAGE_COUNT] = pageCount;


            Rave.HR.BusinessLayer.MRF.MRFDetail MRFDetailBL = new Rave.HR.BusinessLayer.MRF.MRFDetail();

            //Code added if filter is done for role and status is pending allocation
            //then only checkbox column would be displayed.
            //Also check role should be available in SLA table.
            if (mrfDetail.RoleId != 0
                && mrfDetail.StatusId == Convert.ToInt32(MasterEnum.MRFStatus.PendingAllocation)
                && objListMRFDetails.Count > 1
                && MRFDetailBL.CheckSLARoleExist(mrfDetail.RoleId))
            {
                grdvListofPendingAllocation.Columns[0].Visible = true;
                btnRaiseHeadCount.Visible = true;
            }
            else
            {
                grdvListofPendingAllocation.Columns[0].Visible = false;
                btnRaiseHeadCount.Visible = false;
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, GETMRFDETAILSFORPENDINGALLOCATION, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
        return objListMRFDetails;
    }

    /// <summary>
    /// Based on the Department selected, fill the Role 
    /// </summary>
    private void FillRoleDropdownAsPerDepartment()
    {
        try
        {
            ddlRole.Enabled = true;

            if (int.Parse(ddlDepartment.SelectedValue) != Convert.ToInt32(MasterEnum.Departments.Projects))
            {
                if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Admin))
                {
                    FillRoleDropDown(Convert.ToInt32(EnumsConstants.Category.AdminRole));
                }
                if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Finance))
                {
                    FillRoleDropDown(Convert.ToInt32(EnumsConstants.Category.FinanceRole));
                }
                if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.HR))
                {
                    FillRoleDropDown(Convert.ToInt32(EnumsConstants.Category.HRRole));
                }
                if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.ITS))
                {
                    FillRoleDropDown(Convert.ToInt32(EnumsConstants.Category.ITSRole));
                }
                if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Marketing))
                {
                    FillRoleDropDown(Convert.ToInt32(EnumsConstants.Category.MarketingRole));
                }
                if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.PMOQuality))
                {
                    FillRoleDropDown(Convert.ToInt32(EnumsConstants.Category.PMOQualityRole));
                }
                if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.PreSales))
                {
                    FillRoleDropDown(Convert.ToInt32(EnumsConstants.Category.PreSalesRole));
                }
                if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.RaveDevelopment))
                {
                    FillRoleDropDown(Convert.ToInt32(EnumsConstants.Category.RaveDevelopmentRole));
                }
                if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Support))
                {
                    ddlRole.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
                }
                if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Testing))
                {
                    FillRoleDropDown(Convert.ToInt32(EnumsConstants.Category.TestingRole));
                }

                if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.RaveConsultant_India)
                    || int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.RaveConsultant_UK)
                    || int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.RaveConsultant_USA))
                {
                    FillRoleDropDown(Convert.ToInt32(EnumsConstants.Category.ProjectRole));
                }
                if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.RaveForecastedProjects))
                {
                    FillRoleDropDown(Convert.ToInt32(EnumsConstants.Category.RaveDevelopmentRole));
                }

                if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.ProjectMentee2010))
                {
                    FillRoleDropDown(Convert.ToInt32(EnumsConstants.Category.RaveDevelopmentRole));
                }

                ddlProjectName.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
                ddlProjectName.Enabled = false;

            }
            else
            {
                FillRoleDropDown(Convert.ToInt32(EnumsConstants.Category.ProjectRole));
                FillProjectNameDropDown();
                ddlProjectName.Enabled = true;
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                                        CLASS_NAME, FILLROLEDROPDOWNASPERDEPARTMENT,
                                        EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Fills the Project Name dropdown
    /// </summary>
    private void FillProjectNameDropDown()
    {
        try
        {
            // Initialise Collection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            // Initialise Business layer object
            Rave.HR.BusinessLayer.MRF.MRFDetail mrfProjectName = new Rave.HR.BusinessLayer.MRF.MRFDetail();

            // Call the Business layer method
            raveHRCollection = mrfProjectName.GetProjectName();

            if (raveHRCollection != null)
            {
                // Assign the data source to dropdown
                ddlProjectName.DataSource = raveHRCollection;

                ddlProjectName.DataTextField = CommonConstants.DDL_DataTextField;
                ddlProjectName.DataValueField = CommonConstants.DDL_DataValueField;

                // Bind the data to dropdown
                ddlProjectName.DataBind();

                // Default value of dropdown is "Select"
                ddlProjectName.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FILLPROJECTNAMEDROPDOWN, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    //18402-Start
    /// <summary>
    ///  Fills the Status dropdown
    /// </summary>
    private void FillStatusDropDown()
    {
        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
        BusinessEntities.RaveHRCollection newRaveHRCollection = new BusinessEntities.RaveHRCollection();

        // Initialise Business layer object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        try
        {
            // Call the Business layer method
            raveHRCollection = master.FillDropDownsBL(Convert.ToInt32(EnumsConstants.Category.MRFStatus));//PendingAllocationStatus


            foreach (KeyValue<string> keyValue in raveHRCollection)
            {
                // VRP :  Issue 45753 : 06/11/2013  : Start
                // Desc : commented the Pending External Allocatoin data KeyName == "98"    
                if (keyValue.KeyName == "74" || keyValue.KeyName == "733" || keyValue.KeyName == "SELECT")                    
                {
                    newRaveHRCollection.Add(keyValue);
                }
                // VRP :  Issue 45753 : 06/11/2013  : End
            }

            if (newRaveHRCollection != null)
            {
                // Assign the data source to dropdown
                ddlStatus.DataSource = newRaveHRCollection;

                ddlStatus.DataTextField = CommonConstants.DDL_DataTextField;
                ddlStatus.DataValueField = CommonConstants.DDL_DataValueField;

                // Bind the data to dropdown
                ddlStatus.DataBind();

                // Default value of dropdown is "Select"
                ddlStatus.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
            }

            //foreach (ListItem lItem in ddlStatus.Items)
            //{
            //    if (lItem.Value != "74" || lItem.Value != "98" || lItem.Value != "733" || lItem.Value != "SELECT")
            //    {
            //        ddlStatus.Items.Remove(lItem);
            //    }
            //}



        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FILLSTATUSDROPDOWN, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }
    //18402-End

    /// <summary>
    ///  Fills the Role dropdown
    /// </summary>
    private void FillRoleDropDown(int categoryId)
    {
        try
        {
            // Initialise Collection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            // Initialise Business layer object
            Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

            // Call the Business layer method
            raveHRCollection = master.FillDropDownsBL(categoryId);

            if (raveHRCollection != null)
            {
                // Assign the data source to dropdown
                ddlRole.DataSource = raveHRCollection;

                ddlRole.DataTextField = CommonConstants.DDL_DataTextField;
                ddlRole.DataValueField = CommonConstants.DDL_DataValueField;

                // Bind the data to dropdown
                ddlRole.DataBind();

                // Default value of dropdown is "Select"
                ddlRole.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FILLROLEDROPDOWN, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    ///  Fills the Department dropdown
    /// </summary>
    private void FillDepartmentDropDown()
    {
        try
        {
            // Initialise Collection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            // Initialise Business layer object
            Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

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
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FILLDEPARTMENTDROPDOWN, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Fills all the dropdown in Filter panel.
    /// </summary>
    private void FillDropDowns()
    {
        try
        {
            //Fill Department dropdown
            FillDepartmentDropDown();
            ddlProjectName.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
            ddlProjectName.Enabled = false;
            ddlRole.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
            ddlRole.Enabled = false;
            //18402-Ambar-Fill Status dropdown
            FillStatusDropDown();

            ////Vandana-Fill dropdown
            FillTypeOfAllocationDropDown();

            ddlTypeOfAllocation.Enabled = false;

            FillTypeOfSupplyAllocationDropDown();
            ddlTypeOfSupply.Enabled = false;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FILLDROPDOWNS, EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }


    }

    /// <summary>
    /// Method to get SLA days for the recuriter by employee designation
    /// </summary>
    private string GetSLADaysByMrfId(int MRFID)
    {
        string SLAdays = string.Empty;
        // Initialise Business layer object
        Rave.HR.BusinessLayer.MRF.MRFDetail objBLMrf = new Rave.HR.BusinessLayer.MRF.MRFDetail();

        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            BusinessEntities.MRFDetail mrfDetail = new MRFDetail();
            //mrfDetail.MRFId = int.Parse(hidMRFID.Value);
            mrfDetail.MRFId = MRFID;
            raveHRCollection = objBLMrf.GetSLADaysByMrfId(mrfDetail);

            //checks if object is not null
            if (raveHRCollection != null)
            {
                foreach (BusinessEntities.MRFDetail objMrfDetails in raveHRCollection)
                {
                    SLAdays = objMrfDetails.SLADays.ToString();
                }
            }
            return SLAdays;
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetSLADaysByMrfId", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
        }

    }




    private void FillTypeOfAllocationDropDown()
    {
        try
        {
            // Initialise Collection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            // Initialise Business layer object
            Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

            // Call the Business layer method
            //raveHRCollection = master.FillDepartmentDropDownBL();
            raveHRCollection = master.FillDropDownsBL(Convert.ToInt32(EnumsConstants.Category.TypeOfAllocation));

            if (raveHRCollection != null)
            {
                // Assign the data source to dropdown
                ddlTypeOfAllocation.DataSource = raveHRCollection;

                ddlTypeOfAllocation.DataTextField = CommonConstants.DDL_DataTextField;
                ddlTypeOfAllocation.DataValueField = CommonConstants.DDL_DataValueField;

                // Bind the data to dropdown
                ddlTypeOfAllocation.DataBind();

                // Default value of dropdown is "Select"
                ddlTypeOfAllocation.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);


            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FILLTYPEOFALLOCATIONDROPDOWN,
                                        EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    private void FillTypeOfSupplyAllocationDropDown()
    {
        try
        {
            // Initialise Collection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            // Initialise Business layer object
            Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

            // Call the Business layer method            
            raveHRCollection = master.FillDropDownsBL(Convert.ToInt32(EnumsConstants.Category.TypeOfResourceAllocationSupply));

            if (raveHRCollection != null)
            {
                // Assign the data source to dropdown
                ddlTypeOfSupply.DataSource = raveHRCollection;

                ddlTypeOfSupply.DataTextField = CommonConstants.DDL_DataTextField;
                ddlTypeOfSupply.DataValueField = CommonConstants.DDL_DataValueField;

                // Bind the data to dropdown
                ddlTypeOfSupply.DataBind();

                // Default value of dropdown is "Select"
                ddlTypeOfSupply.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
            }
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer,
                                        CLASS_NAME, FILLTYPEOFSUPPLYRDROPDOWN,
                                        EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }


    protected void ddlTypeOfAllocation_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlTypeOfAllocation.SelectedItem.Text == CommonConstants.FutureAllocation)
        {

            ddlTypeOfSupply.Enabled = true;
        }
        else
        {
            ddlTypeOfSupply.Enabled = false;

        }
    }


    #endregion

    #region Property
    /// <summary>
    /// Private Property to Get and Set direction for for sorting
    /// </summary>
    public SortDirection GridViewSortDirection
    {
        get
        {
            if (Session[SORT_DIRECTION] == null)
                Session[SORT_DIRECTION] = SortDirection.Descending;
            return (SortDirection)Session[SORT_DIRECTION];
        }
        set
        {
            Session[SORT_DIRECTION] = value;
        }
    }
    #endregion

}
