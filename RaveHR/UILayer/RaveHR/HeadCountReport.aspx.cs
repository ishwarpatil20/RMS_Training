using System;
using System.Collections;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;

public partial class HeadCountReport : BaseClass
{
    #region Private Field Members

    private const string CLASS_NAME = "HeadCountReport.aspx.cs";

    BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

    /// <summary>
    /// Defines Ascending sorting order for grid
    /// </summary>
    private const string ASCENDING = " ASC";

    /// <summary>
    /// Defines Descending sorting order for grid
    /// </summary>
    private const string DESCENDING = " DESC";


    /// <summary>
    /// Sets the image direction either upwards or downwards
    /// </summary>
    private string imageDirection = string.Empty;


    BusinessEntities.ParameterCriteria objParameter = new BusinessEntities.ParameterCriteria();
    private static string sortExpression = string.Empty;

    /// <summary>
    /// Get or set the GridViewSortDirection property
    /// </summary>
    private System.Web.UI.WebControls.SortDirection GridViewSortDirection
    {
        get
        {
            if (Session[SessionNames.SORT_DIRECTION_EMP] == null)
                Session[SessionNames.SORT_DIRECTION_EMP] = System.Web.UI.WebControls.SortDirection.Ascending;

            return (System.Web.UI.WebControls.SortDirection)Session[SessionNames.SORT_DIRECTION_EMP];
        }

        set
        {
            Session[SessionNames.SORT_DIRECTION_EMP] = value;
        }
    }


    #endregion Private Field Members

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                GetMasterData_DivisionDropDown();
                GetMasterData_BussinessAreaDropDown();
                GetMasterData_BussinessSegmentDropDown();

                this.BindGridHeadCount();
                
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "Page_Load", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Binds the List Of MRF Grid
    /// </summary>
    private void BindGridHeadCount()
    {
        try
        {
            //Siddharth 7 May 2015 Start
            imageDirection = SortOrder.Ascending.ToString();
            Session[SessionNames.SORT_DIRECTION_EMP] = null;
            //Siddharth 7 May 2015 End
            sortExpression = "Division";
            raveHRCollection = GetHeadCountReport("Division ASC");
            lblCount.Visible = true;
            if (raveHRCollection.Count == 0)
            {
                grdvHeadCount.DataSource = raveHRCollection;
                grdvHeadCount.DataBind();
                lblCount.Text = " Total Count : 0";
                ShowHeaderWhenEmptyGrid(raveHRCollection);
                //Siddharth 30 April 2015 Start - Clear the Session
                Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] = null;
                Session[SessionNames.SORT_DIRECTION_EMP] = null;
                //Siddharth 30 April 2015 End
                btnExport.Visible = false;
            }
            else
            {
                grdvHeadCount.AllowSorting = true;
                grdvHeadCount.DataSource = raveHRCollection;
                grdvHeadCount.DataBind();
                int count = 0;
                for (int i = 0; i < raveHRCollection.Count; i++)
                {
                    count = count + Convert.ToInt32(((BusinessEntities.Employee)(raveHRCollection.Item(i))).ResourceTypeCount);
                }
                lblCount.Text = " Total Count : " + count;
                btnExport.Visible = true;
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "SortGridView", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    private BusinessEntities.RaveHRCollection GetHeadCountReport(string SortExpressionAndDirection)
    {
        BusinessEntities.Projects project = new Projects();
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
        Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
        try
        {
            project.ProjectDivision = ddlDivision.SelectedValue != "Select" ? Convert.ToInt32(ddlDivision.SelectedValue) : 0;
            project.ProjectBussinessArea = ddlBusinessArea.SelectedValue != "Select" ? Convert.ToInt32(ddlBusinessArea.SelectedValue) : 0;
            project.ProjectBussinessSegment = ddlBusinessSegment.SelectedValue != "Select" ? Convert.ToInt32(ddlBusinessSegment.SelectedValue) : 0;
            //Siddharth 27 March 2015 Start
            raveHRCollection = employeeBL.GetHeadCountReport(project, SortExpressionAndDirection);
            //Siddharth 27 March 2015 End
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetHeadCountReport", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

        return raveHRCollection;
    }

    private void ShowHeaderWhenEmptyGrid(BusinessEntities.RaveHRCollection raveHRCollection)
    {
        try
        {
            //set header visible
            grdvHeadCount.ShowHeader = true;
            // Disable sorting
            grdvHeadCount.AllowSorting = false;

            //Create empty datasource for Grid view and bind
            raveHRCollection.Add(new BusinessEntities.Employee());
            grdvHeadCount.DataSource = raveHRCollection;
            grdvHeadCount.DataBind();

            //clear all the cells in the row
            grdvHeadCount.Rows[0].Cells.Clear();

            //add a new blank cell
            grdvHeadCount.Rows[0].Cells.Add(new TableCell());
            grdvHeadCount.Rows[0].Cells[0].Text = CommonConstants.NO_RECORDS_FOUND_MESSAGE;
            grdvHeadCount.Rows[0].Cells[0].Wrap = false;
            grdvHeadCount.Rows[0].Cells[0].Width = Unit.Percentage(10);
        }
        catch
        {

        }
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDivision.SelectedValue == CommonConstants.Project_Division_PublicService)
            {
                ddlBusinessArea.Enabled = true;
                ddlBusinessArea.SelectedIndex = 0;
            }
            else
            {
                ddlBusinessArea.Enabled = false;
                ddlBusinessArea.SelectedIndex = 0;

                ddlBusinessSegment.Enabled = false;
                ddlBusinessSegment.SelectedIndex = 0;
            }
            btnSearch_Click(sender, e);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlDivision_SelectedIndexChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void ddlBusinessArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBusinessArea.SelectedValue == CommonConstants.Project_BussinessArea_Solutions)
            {
                ddlBusinessSegment.Enabled = true;
                ddlBusinessSegment.SelectedIndex = 0;
            }
            else
            {
                ddlBusinessSegment.Enabled = false;
                ddlBusinessSegment.SelectedIndex = 0;
            }
            btnSearch_Click(sender, e);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlBusinessArea_SelectedIndexChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    ///  method to get value of DivisionDropDown
    /// </summary>
    private void GetMasterData_DivisionDropDown()
    {
        try
        {
            List<BusinessEntities.Master> objRaveHRMaster = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            objRaveHRMaster = objRaveHRMasterBAL.GetMasterData((Convert.ToInt32(EnumsConstants.Category.ProjectDivision)).ToString());
            objRaveHRMaster.Remove(objRaveHRMaster.Find(a => a.MasterName == "N/A"));

            ddlDivision.DataSource = objRaveHRMaster;
            ddlDivision.DataTextField = "MasterName";
            ddlDivision.DataValueField = "MasterID";
            ddlDivision.DataBind();
            ddlDivision.Items.Insert(0, "Select");
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "HeadCountReport.aspx", "GetMasterData_DivisionDropDown", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    ///  method to get value of BussinessAreaDropDown
    /// </summary>
    private void GetMasterData_BussinessAreaDropDown()
    {
        try
        {
            List<BusinessEntities.Master> objRaveHRMaster = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            objRaveHRMaster = objRaveHRMasterBAL.GetMasterData((Convert.ToInt32(EnumsConstants.Category.ProjectBussinessArea)).ToString());
            objRaveHRMaster.Remove(objRaveHRMaster.Find(a => a.MasterName == "N/A"));

            ddlBusinessArea.DataSource = objRaveHRMaster;
            ddlBusinessArea.DataTextField = "MasterName";
            ddlBusinessArea.DataValueField = "MasterID";
            ddlBusinessArea.DataBind();
            ddlBusinessArea.Items.Insert(0, "Select");
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "HeadCountReport.aspx", "GetMasterData_BussinessAreaDropDown", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    ///  method to get value of BussinessSegmentDropDown
    /// </summary>
    private void GetMasterData_BussinessSegmentDropDown()
    {
        try
        {
            List<BusinessEntities.Master> objRaveHRMaster = new List<BusinessEntities.Master>();
            Rave.HR.BusinessLayer.Common.Master objRaveHRMasterBAL = new Rave.HR.BusinessLayer.Common.Master();
            objRaveHRMaster = objRaveHRMasterBAL.GetMasterData((Convert.ToInt32(EnumsConstants.Category.ProjectBussinessSegment)).ToString());
            objRaveHRMaster.Remove(objRaveHRMaster.Find(a => a.MasterName == "N/A"));

            ddlBusinessSegment.DataSource = objRaveHRMaster;
            ddlBusinessSegment.DataTextField = "MasterName";
            ddlBusinessSegment.DataValueField = "MasterID";
            ddlBusinessSegment.DataBind();
            ddlBusinessSegment.Items.Insert(0, "Select");
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "HeadCountReport.aspx", "GetMasterData_BussinessSegmentDropDown", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            this.BindGridHeadCount();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnSearch_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            GridView[] gvList = null;
            gvList = new GridView[] { grdvHeadCount };            
            ExportAv("HeadCountReport.xls", gvList);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnExport_Click", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    public static void ExportAv(string fileName, GridView[] gvs)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=\"" + fileName + "\"");
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
       
        foreach (GridView gv in gvs)
        {
            gv.AllowPaging = false;
            gv.AllowSorting = false;

            if (gv.Rows.Count > 0)
            {
                //   Create a form to contain the grid
                Table table = new Table();

                table.GridLines = gv.GridLines;
                //   add the header row to the table
                if (!(gv.Caption == null))
                {
                    TableCell cell = new TableCell();
                    if (gv.ID == "grdvHeadCount")
                        cell.Text = "Head Count Report";
                    cell.Font.Bold = true;
                    //cell.HorizontalAlign = "Center";
                    cell.Style.Add("text-Decoration", "bold");
                    cell.ColumnSpan = 6;
                    TableRow tr = new TableRow();
                    tr.Controls.Add(cell);
                    table.Rows.Add(tr);
                }

                if (!(gv.HeaderRow == null))
                {
                    table.Rows.Add(gv.HeaderRow);
                }
                //   add each of the data rows to the table
                foreach (GridViewRow row in gv.Rows)
                {
                    table.Rows.Add(row);
                }
                //   add the footer row to the table
                if (!(gv.FooterRow == null))
                {
                    table.Rows.Add(gv.FooterRow);
                }
                //   render the table into the htmlwriter
                //table.RenderControl("ds");
                table.RenderControl(htw);
            }
        }
        //   render the htmlwriter into the response

        //string headerTable = @"<table width='100%' class='TestCssStyle'><tr><td><h4>Report </h4> </td><td></td><td><h4>" + DateTime.Now.ToString("d") + "</h4></td></tr></table>";
        //HttpContext.Current.Response.Write(headerTable);
        HttpContext.Current.Response.Write(sw.ToString());

        HttpContext.Current.Response.Flush();// Sends all currently buffered output to the client.
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest();// Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
        //HttpContext.Current.Response.End();


    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        try
        {
            ddlDivision.SelectedIndex = 0;
            ddlBusinessArea.SelectedIndex = 0;
            ddlBusinessSegment.SelectedIndex = 0;

            ddlBusinessArea.Enabled = false;
            ddlBusinessSegment.Enabled = false;

            this.BindGridHeadCount();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "BtnClear_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }





    #region "Sorting Method"

    protected void grdvHeadCount_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            // GridViewRow gvrPager = GVResourcesOnboard.BottomPagerRow;
            // TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            // txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_EMP].ToString();

            //On sorting assign new sort expression
            sortExpression = e.SortExpression;

            if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] == null)
            {
                Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] = sortExpression;
            }

            if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP].ToString() == sortExpression)
            {
                //Sort to opposite direction based upon previous sort direction
                if (Session[SessionNames.SORT_DIRECTION_EMP] == null || GridViewSortDirection == System.Web.UI.WebControls.SortDirection.Descending)
                {
                    GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Ascending;
                    SortGridView(sortExpression, ASCENDING);
                }
                else
                {
                    GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Descending;
                    SortGridView(sortExpression, DESCENDING);
                }
            }
            else
            {
                GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Ascending;
                SortGridView(sortExpression, ASCENDING);
            }

            if (Session[SessionNames.EMP_PROJECTID] != null || Session[SessionNames.EMP_DEPARTMENTID] != null || Session[SessionNames.EMP_ROLE] != null || Session[SessionNames.EMP_STATUSID] != null || Session[SessionNames.EMP_NAME] != null)
            {
                //Change here 26 March 2015
                //btnRemoveFilter.Visible = true;
            }

            //Set current sort expression as PreviousSortExpression
            Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] = sortExpression;
        }

        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvHeadCount_Sorting", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    //Siddharth 26 March 2015 Start
    private void SortGridView(string sortExpression, string direction)
    {
        try
        {
            sortExpression = "[" + sortExpression + "]";
            if (sortExpression == CommonConstants.EMP_FIRSTNAME)
            {
                objParameter.SortExpressionAndDirection = sortExpression + direction;
            }
            else
            {
                objParameter.SortExpressionAndDirection = sortExpression + direction;
            }

            //This will call the bind function and pass sort details
            raveHRCollection = GetHeadCountReport(objParameter.SortExpressionAndDirection);

            if (raveHRCollection.Count == 0)
            {
                grdvHeadCount.DataSource = raveHRCollection;
                Session["HeadCountReport"] = raveHRCollection.Count;
                grdvHeadCount.DataBind();

                ShowHeaderWhenEmptyGrid(raveHRCollection);
                //Siddharth 30 April 2015 Start - Clear the Session
                Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] = null;
                Session[SessionNames.SORT_DIRECTION_EMP] = null;
                //Siddharth 30 April 2015 End
                btnExport.Visible = false;
            }
            else
            {
                grdvHeadCount.AllowSorting = true;
                grdvHeadCount.DataSource = raveHRCollection;
                grdvHeadCount.DataBind();
                btnExport.Visible = true;
            }

        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "SortGridView", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    //Siddharth 26 March 2015 End


    //Siddharth 30 March 2015 Start
    protected void grdvHeadCount_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (((Session["HeadCountReport"] != null) && (int.Parse(Session["HeadCountReport"].ToString()) > 1)) || ((raveHRCollection.Count > 1)))
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "gvListOfMrf_RowCreated", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Add the Sort Image to gridview header row
    /// </summary>
    /// <param name="headerRow"></param>
    private void AddSortImage(GridViewRow headerRow)
    {
        try
        {
            //Assign the sort direction of gridview to image
            imageDirection = GridViewSortDirection.ToString();

            if (!imageDirection.Equals(string.Empty))
            {
                // Create the sorting image based on the sort direction
                Image sortImage = new Image();

                if (imageDirection == SortOrder.Ascending.ToString())
                {
                    sortImage.ImageUrl = CommonConstants.IMAGE_UP_ARROW;
                    sortImage.AlternateText = CommonConstants.ASCENDING;
                }
                else if (imageDirection == SortOrder.Descending.ToString())
                {
                    sortImage.ImageUrl = CommonConstants.IMAGE_DOWN_ARROW;
                    sortImage.AlternateText = CommonConstants.DESCENDING;
                }

                // Add the image to the appropriate header cell
                switch (sortExpression)
                {
                    case CommonConstants.DIVISION:
                        headerRow.Cells[0].Controls.Add(sortImage);
                        break;

                    //case CommonConstants.RP_CODE:
                    //    headerRow.Cells[2].Controls.Add(sortImage);
                    //    break;

                    case CommonConstants.BUSINESS_AREA:
                        headerRow.Cells[1].Controls.Add(sortImage);
                        break;

                    case CommonConstants.BUSINESS_SEGMENT:
                        headerRow.Cells[2].Controls.Add(sortImage);
                        break;

                    case CommonConstants.COST_CODE:
                        headerRow.Cells[3].Controls.Add(sortImage);
                        break;

                    case CommonConstants.RESOURCE_TYPE:
                        headerRow.Cells[4].Controls.Add(sortImage);
                        break;

                    case CommonConstants.RESOURCE_TYPE_COUNT:
                        headerRow.Cells[5].Controls.Add(sortImage);
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
    //Siddharth 30 March 2015 End

    #endregion "Sorting Method"

    public override void VerifyRenderingInServerForm(Control aControl)
    {
        //###this removes the no forms error by overriding the error 
    }

}
