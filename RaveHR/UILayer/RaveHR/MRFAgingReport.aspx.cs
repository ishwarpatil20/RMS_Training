//------------------------------------------------------------------------------
//  File:           MRFAgingReport.aspx.cs       
//  Author:         Ishwar Patil
//  Date written:   18/02/2015
//  Description:    This page showing MRF Aging details.
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
using System.Xml.Linq;
using Common;
using System.Data.SqlClient;

public partial class MRFAgingReport : BaseClass
{
    #region Members Variables
    private const string CLASS_NAME = "MRFAgingReport.aspx.cs";
    /// <summary>
    /// Defines Ascending sorting order for grid
    /// </summary>
    private const string ASCENDING = " ASC";


    /// <summary>
    /// Sets the image direction either upwards or downwards
    /// </summary>
    private string imageDirection = string.Empty;


    /// <summary>
    /// Defines Descending sorting order for grid
    /// </summary>
    private const string DESCENDING = " DESC";
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


    #endregion Members Variables

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!ValidateURL())
            {
                Response.Redirect(CommonConstants.INVALIDURL, false);
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    GetMRFAging("Technology ASC");
                }
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

    //Siddharth 27 March 2015 Start
    private void GetMRFAging(string SortExpressionAndDirection)
    {
        Rave.HR.BusinessLayer.MRF.MRFDetail MRFAgingBL = new Rave.HR.BusinessLayer.MRF.MRFDetail();
        try
        {
            DataSet ds = new DataSet();
            ds = MRFAgingBL.GetMRFAging(SortExpressionAndDirection);

            if (ds.Tables[0].Rows.Count != CommonConstants.ZERO)
            {
                btnExport.Visible = true;

                GVMRFAgingReport.DataSource = ds.Tables[0];
                Session["MRFAgingReportCount"]= Convert.ToInt32(ds.Tables[0].Rows.Count);
                GVMRFAgingReport.DataBind();
            }
            else
            {
                btnExport.Visible = false;
                ShowHeaderWhenEmptyGrid(ds);
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetMRFAging", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    //Siddharth 27 March 2015 ENd


    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            GridView[] gvList = null;
            gvList = new GridView[] { GVMRFAgingReport };
            ExportAv("MRF Aging.xls", gvList);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnExport_Click", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    private void ShowHeaderWhenEmptyGrid(DataSet ds)
    {
        try
        {
            GVMRFAgingReport.ShowHeader = true;
            GVMRFAgingReport.AllowSorting = false;

            DataTable dt = new DataTable();
            dt.Clear();
            dt = ds.Tables[0];
            DataRow dr = dt.NewRow();
            dt.Rows.Add(dr);

            GVMRFAgingReport.DataSource = dt;
            GVMRFAgingReport.DataBind();

            GVMRFAgingReport.Rows[0].Cells.Clear();

            GVMRFAgingReport.Rows[0].Cells.Add(new TableCell());
            GVMRFAgingReport.Rows[0].Cells[0].Text = CommonConstants.NO_RECORDS_FOUND_MESSAGE;
            GVMRFAgingReport.Rows[0].Cells[0].Wrap = false;
            GVMRFAgingReport.Rows[0].Cells[0].Width = Unit.Percentage(10);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ShowHeaderWhenEmptyGrid", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
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

            if (gv.Rows.Count > 0)
            {
                Table table = new Table();

                table.GridLines = gv.GridLines;

                //if (!(gv.Caption == null))
                //{
                //    TableCell cell = new TableCell();
                //    cell.Text = "";

                //    if (gv.ID == "GVMRFAgingReport")
                //        cell.Text = "MRF Aging Report";

                //    cell.Font.Bold = true;
                //    cell.Style.Add("text-Decoration", "bold");
                //    cell.ColumnSpan = 3;
                //    TableRow tr = new TableRow();
                //    tr.Controls.Add(cell);
                //    table.Rows.Add(tr);
                //}

                if (!(gv.HeaderRow == null))
                {
                    table.Rows.Add(gv.HeaderRow);
                }
                //   add each of the data rows to the table
                foreach (GridViewRow row in gv.Rows)
                {
                    row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                    row.Cells[1].HorizontalAlign = HorizontalAlign.Left;      
                    row.Cells[2].HorizontalAlign = HorizontalAlign.Right;      
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

        HttpContext.Current.Response.Write(sw.ToString());

        HttpContext.Current.Response.Flush();// Sends all currently buffered output to the client.
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest();// Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
    }

#region "Sorting Method"

    protected void GVMRFAgingReport_Sorting(object sender, GridViewSortEventArgs e)
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "MRFAgingReport", "Page_Load", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
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
            GetMRFAging(objParameter.SortExpressionAndDirection);

            //if (raveHRCollection.Count == 0)
            //{
            //    GVResourcesOnboard.DataSource = raveHRCollection;
            //    GVResourcesOnboard.DataBind();

            //   // ShowHeaderWhenEmptyGrid(raveHRCollection);
            //}
            //else
            //{
            //    GVResourcesOnboard.DataSource = raveHRCollection;
            //    GVResourcesOnboard.DataBind();

            //}
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

    protected void GVMRFAgingReport_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (((Session["MRFAgingReportCount"] != null) && (int.Parse(Session["MRFAgingReportCount"].ToString()) > 1)))
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
                    case CommonConstants.TECHNOLOGY_MRF_AGING_RPT:
                        headerRow.Cells[0].Controls.Add(sortImage);
                        break;

                    case CommonConstants.DESIGNATION_MRF_AGING_RPT:
                        headerRow.Cells[1].Controls.Add(sortImage);
                        break;

                    case CommonConstants.AVGTIMETAKEN_MRF_AGING_RPT:
                        headerRow.Cells[2].Controls.Add(sortImage);
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
