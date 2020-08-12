using System;
using BusinessEntities;
using System.Web.UI.WebControls;
using Common;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Web;
using System.IO;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;


public partial class EmployeeSkillSearch : BaseClass
{
    #region Private Field Members

    private const string CLASS_NAME = "EmployeeSkillSearch.aspx.cs";
    BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
    DataTable dt;
    int GridViewRowCount = 4;


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



    #region ViewState Constants

    private string SKILLS = "Skills";
    
    #endregion ViewState Constants

    #region Private Properties

    /// <summary>
    /// Gets or sets the skill collection.
    /// </summary>
    /// <value>The skill collection.</value>
    private RaveHRCollection SkillCollection
    {
        get
        {
            if (ViewState[SKILLS] != null)
                return ((RaveHRCollection)ViewState[SKILLS]);
            else
                return null;
        }
        set
        {
            ViewState[SKILLS] = value;
        }
    }

   #endregion Private Properties

    #endregion Private Field Members

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                DefaultGridView();
                //Poonam : Issue : 54921 : Starts
                //Desc: Validation for Skills Dropdown	
                BtnSearch.Attributes.Add("OnClick", "return ValidateSearchBtn()");
                //Poonam : Issue : 54921 : Ends
            }
            lblError.Text = string.Empty;
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

    protected void DefaultGridView()
    {
        try
        {
            dt = new DataTable();

            dt.Columns.Add(new DataColumn("SkillNo", typeof(string)));
            dt.Columns.Add(new DataColumn("SkillName", typeof(string)));
            dt.Columns.Add(new DataColumn("SearchMode", typeof(string)));

            int i = 1;

            foreach (GridViewRow gdr in gvSkillCriteria.Rows)
            {
                DataRow dr = dt.NewRow();
                dr["SkillNo"] = i;
                dr["SkillName"] = ((DropDownList)gdr.FindControl("ddlSkill")).SelectedValue;
                dr["SearchMode"] = ((RadioButtonList)gdr.FindControl("rblMandatoryOptional")).SelectedValue;
                dt.Rows.Add(dr);
                i++;
            }

            DataRow defualtdr = dt.NewRow();
            defualtdr["SkillNo"] = i;
            dt.Rows.Add(defualtdr);

            BindGridView(dt);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "DefaultGridView", EventIDConstants.RAVE_EMP_SKILL_SEARCH_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlSkill = (DropDownList)gvSkillCriteria.Rows[(gvSkillCriteria.Rows.Count - 1)].FindControl("ddlSkill");
            if (ddlSkill.SelectedItem.Text == CommonConstants.SELECT)
            {
                lblError.Text = "Please select a Skill.";
                return;
            }
            else
            {
                grdvListofEmployees.Visible = true;
                this.BindGridSkillSearch();
            }
            
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "BtnSearch_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    private void BindGridSkillSearch()
    {
        try
        {
            SortGridView();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "BindGridEmployeeSummary", EventIDConstants.RAVE_EMP_SKILL_SEARCH_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    private void ShowHeaderWhenEmptyGrid(DataTable dt)
    {
        try
        {
            //set header visible
            grdvListofEmployees.ShowHeader = true;
            // Disable sorting
            grdvListofEmployees.AllowSorting = false;

            DataRow defualtdr = dt.NewRow();
            //defualtdr["SkillNo"] = 1;
            dt.Rows.Add(defualtdr);

            grdvListofEmployees.DataSource = dt;
            grdvListofEmployees.DataBind();
            
            grdvListofEmployees.Rows[0].Cells.Clear();

            //add a new blank cell
            grdvListofEmployees.Rows[0].Cells.Add(new TableCell());
            grdvListofEmployees.Rows[0].Cells[0].Text = CommonConstants.NO_RECORDS_FOUND_MESSAGE;
            grdvListofEmployees.Rows[0].Cells[0].Wrap = false;
            grdvListofEmployees.Rows[0].Cells[0].Width = Unit.Percentage(10);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ShowHeaderWhenEmptyGrid", EventIDConstants.RAVE_EMP_SKILL_SEARCH_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    private void SortGridView()
    {
        try
        {
            dt = new DataTable();
            dt = GetSkillSearchDetails("EmployeeName ASC");

            if (dt.Rows.Count > 0)
            {
                btnExport.Visible = true;
                lblError.Text = string.Empty;
                grdvListofEmployees.AllowSorting = true;
                grdvListofEmployees.DataSource = dt;
                Session["EmployeeSkillSearchCount"] = Convert.ToInt32(dt.Rows.Count);
                grdvListofEmployees.DataBind();
            }
            else
            {
                ShowHeaderWhenEmptyGrid(dt);
                //Siddharth 30 April 2015 Start - Clear the Session
                Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] = null;
                Session[SessionNames.SORT_DIRECTION_EMP] = null;
                //Siddharth 30 April 2015 End
                btnExport.Visible = false;
                lblError.Text = string.Empty;
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "SortGridView", EventIDConstants.RAVE_EMP_SKILL_SEARCH_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    //Siddharth 27 March 2015 Start
    private DataTable GetSkillSearchDetails(string SortExpressionAndDirection)
    {
        Rave.HR.BusinessLayer.Employee.SkillsDetails objSkillsDetailsBAL = new Rave.HR.BusinessLayer.Employee.SkillsDetails();
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        try
        {
            //Add selected items in collection
            IList<string> MandatorySkills = new List<string>();
            IList<string> OptionalSkills = new List<string>();

            for (int i = 0; i < gvSkillCriteria.Rows.Count; i++)
            {
                DropDownList ddlSkill = (DropDownList)gvSkillCriteria.Rows[i].FindControl("ddlSkill");
                RadioButtonList rblMandatoryOptional = (RadioButtonList)gvSkillCriteria.Rows[i].FindControl("rblMandatoryOptional");

                if (rblMandatoryOptional.SelectedValue == "0")
                {
                    if (Convert.ToString(ddlSkill.SelectedValue) != CommonConstants.SELECT)
                    {
                        MandatorySkills.Add(ddlSkill.SelectedValue);
                    }
                }
                else
                {
                    if (Convert.ToString(ddlSkill.SelectedValue) != CommonConstants.SELECT)
                    {
                        OptionalSkills.Add(ddlSkill.SelectedValue);
                    }
                }
            }

            string commaSeparatedMandatorySkills = String.Join(",", MandatorySkills.ToArray());
            string commadSepratedOptionalSkills = String.Join(",", OptionalSkills.ToArray());

            dt = new DataTable();
            dt = objSkillsDetailsBAL.GetSkillSearchDetails(commaSeparatedMandatorySkills, commadSepratedOptionalSkills, SortExpressionAndDirection);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetEmployeeSummary", EventIDConstants.RAVE_EMP_SKILL_SEARCH_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

        return dt;
    }
    //Siddharth 27 March 2015 End

    protected void BtnClear_OnClick(object sender, EventArgs e)
    {
        try
        {
            dt = new DataTable();
            BindGridView(dt);
            grdvListofEmployees.Visible = false;
            btnExport.Visible = false;
            btnAddRow.Visible = true;
            lblError.Text = string.Empty;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "BtnClear_OnClick", EventIDConstants.RAVE_EMP_SKILL_SEARCH_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void BindGridView(DataTable dt)
    {
        try
        {
            if (dt.Rows.Count > 0)
            {
                gvSkillCriteria.DataSource = dt;
                gvSkillCriteria.DataBind();

                if (dt.Rows.Count >= GridViewRowCount)
                {
                    btnAddRow.Visible = false;
                }
                else
                {
                    btnAddRow.Visible = true;
                }
                dt.Clear();
            }
            else
            {
                dt = new DataTable();

                dt.Columns.Add(new DataColumn("SkillNo", typeof(string)));
                dt.Columns.Add(new DataColumn("SkillName", typeof(string)));
                dt.Columns.Add(new DataColumn("SearchMode", typeof(string)));

                DataRow defualtdr = dt.NewRow();
                defualtdr["SkillNo"] = 1;
                dt.Rows.Add(defualtdr);

                gvSkillCriteria.DataSource = dt;
                gvSkillCriteria.DataBind();
            }
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "BindGridView", EventIDConstants.RAVE_EMP_SKILL_SEARCH_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Gets the skills.
    /// </summary>
    private RaveHRCollection GetSkills()
    {
        Rave.HR.BusinessLayer.Employee.SkillsDetails objSkillTypeBL = new Rave.HR.BusinessLayer.Employee.SkillsDetails();
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        raveHrColl = objSkillTypeBL.GetPrimaryAndSecondarySkills();
        return raveHrColl;
    }

    protected void gvSkillCriteria_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int RowID = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "DeleteSkill")
            {
                dt = new DataTable();

                dt.Columns.Add(new DataColumn("SkillNo", typeof(string)));
                dt.Columns.Add(new DataColumn("SkillName", typeof(string)));
                dt.Columns.Add(new DataColumn("SearchMode", typeof(string)));

                foreach (GridViewRow gdr in gvSkillCriteria.Rows)
                {
                    HiddenField hfSkillNo = (HiddenField)gdr.FindControl("HFSkillNo");
                    if (RowID != Convert.ToInt32(hfSkillNo.Value))
                    {
                        DataRow dr = dt.NewRow();
                        dr["SkillNo"] = ((HiddenField)gdr.FindControl("HFSkillNo")).Value;
                        dr["SkillName"] = ((DropDownList)gdr.FindControl("ddlSkill")).SelectedValue;
                        dr["SearchMode"] = ((RadioButtonList)gdr.FindControl("rblMandatoryOptional")).SelectedValue;
                        dt.Rows.Add(dr);
                    }
                }

                BindGridView(dt);
            }
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "gvSkillCriteria_RowCommand", EventIDConstants.RAVE_EMP_SKILL_SEARCH_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void gvSkillCriteria_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlSkill = (DropDownList)e.Row.FindControl("ddlSkill");
                RadioButtonList rblMandatoryOptional = (RadioButtonList)e.Row.FindControl("rblMandatoryOptional");

                HiddenField HFSkill = (HiddenField)e.Row.FindControl("HFSkill");
                HiddenField HFMandatoryOptional = (HiddenField)e.Row.FindControl("HFMandatoryOptional");

                SkillCollection = this.GetSkills();
                ddlSkill.DataSource = SkillCollection;
                ddlSkill.DataTextField = Common.CommonConstants.DDL_DataTextField;
                ddlSkill.DataValueField = Common.CommonConstants.DDL_DataValueField;
                ddlSkill.DataBind();
                ddlSkill.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                if (!String.IsNullOrEmpty(HFSkill.Value))
                {
                    ddlSkill.Items.FindByText(HFSkill.Value).Selected = true;
                }

                if (!String.IsNullOrEmpty(HFMandatoryOptional.Value))
                {
                    rblMandatoryOptional.SelectedValue = Convert.ToString(HFMandatoryOptional.Value);
                }
            }
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "gvSkillCriteria_RowDataBound", EventIDConstants.RAVE_EMP_SKILL_SEARCH_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddlSkill = (DropDownList)gvSkillCriteria.Rows[(gvSkillCriteria.Rows.Count - 1)].FindControl("ddlSkill");
            if (ddlSkill.SelectedItem.Text == CommonConstants.SELECT)
            {
                lblError.Text = "Please select Skill.";
                return;
            }
            else
            {
                DefaultGridView();
            }
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnAddRow_Click", EventIDConstants.RAVE_EMP_SKILL_SEARCH_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    //Ishwar NIS RMS 11112014 Start
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            GridView[] gvList = null;
            gvList = new GridView[] { grdvListofEmployees };
            ExportAv("Employee Skills Search.xls", gvList);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnExport_Click", EventIDConstants.RAVE_EMP_SKILL_SEARCH_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
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
                //   Create a form to contain the grid
                Table table = new Table();

                table.GridLines = gv.GridLines;
                //   add the header row to the table
                if (!(gv.Caption == null))
                {
                    TableCell cell = new TableCell();
                    if (gv.ID == "grdvListofEmployees")
                        cell.Text = "Employee Skills Search";
                    cell.Font.Bold = true;
                    //cell.HorizontalAlign = "Center";
                    cell.Style.Add("text-Decoration", "bold");
                    cell.ColumnSpan = 10;
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

    protected void grdvListofEmployees_DataBound(object sender, EventArgs e)
    {
        try
        {
            int RowSpan = 2;
            for (int i = grdvListofEmployees.Rows.Count - 2; i >= 0; i--)
            {
                GridViewRow currRow = grdvListofEmployees.Rows[i];
                GridViewRow prevRow = grdvListofEmployees.Rows[i + 1];

                //For Ist Cell--Employee Name
                string lblcurr = currRow.Cells[0].Text;
                string lblPrev = prevRow.Cells[0].Text;
                //For IInd Cell--Designation
                string lblcurr1 = currRow.Cells[1].Text;
                string lblPrev1 = prevRow.Cells[1].Text;

                if (lblcurr == lblPrev && lblcurr1 == lblPrev1)
                {
                    //Employee Name
                    currRow.Cells[0].RowSpan = RowSpan;
                    prevRow.Cells[0].Visible = false;

                    //Designation
                    currRow.Cells[1].RowSpan = RowSpan;
                    prevRow.Cells[1].Visible = false;

                    //Department
                    string lblcurr2 = currRow.Cells[2].Text;
                    string lblPrev2 = prevRow.Cells[2].Text;
                    if (lblcurr2 == lblPrev2)
                    {
                        currRow.Cells[2].RowSpan = RowSpan;
                        prevRow.Cells[2].Visible = false;
                    }

                    //Project Name
                    string lblcurr3 = currRow.Cells[3].Text;
                    string lblPrev3 = prevRow.Cells[3].Text;
                    if (lblcurr3 == lblPrev3)
                    {
                        currRow.Cells[3].RowSpan = RowSpan;
                        prevRow.Cells[3].Visible = false;
                    }

                    //Primary Skill 
                    string lblcurr4 = currRow.Cells[4].Text;
                    string lblPrev4 = prevRow.Cells[4].Text;
                    if (lblcurr4 == lblPrev4)
                    {
                        currRow.Cells[4].RowSpan = RowSpan;
                        prevRow.Cells[4].Visible = false;
                    }

                    RowSpan += 1;
                }
                else
                    RowSpan = 2;
            }
        }

        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvListofEmployees_DataBound", EventIDConstants.RAVE_EMP_SKILL_SEARCH_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    //Ishwar NIS RMS 11112014 End


    
        

    #region "Sorting Method"

    protected void grdvListofEmployees_Sorting(object sender, GridViewSortEventArgs e)
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvListofEmployees_Sorting", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
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
             dt = GetSkillSearchDetails(objParameter.SortExpressionAndDirection);

            if (dt.Rows.Count > 0)
            {
                btnExport.Visible = true;
                lblError.Text = string.Empty;
                grdvListofEmployees.AllowSorting = true;
                grdvListofEmployees.DataSource = dt;
                grdvListofEmployees.DataBind();
            }
            else
            {
                ShowHeaderWhenEmptyGrid(dt);
                //Siddharth 30 April 2015 Start - Clear the Session
                Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] = null;
                Session[SessionNames.SORT_DIRECTION_EMP] = null;
                //Siddharth 30 April 2015 End
                btnExport.Visible = false;
                lblError.Text = string.Empty;
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

    protected void grdvListofEmployees_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (((Session["EmployeeSkillSearchCount"] != null) && (int.Parse(Session["EmployeeSkillSearchCount"].ToString()) > 1)))
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
                System.Web.UI.WebControls.Image sortImage = new System.Web.UI.WebControls.Image();

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
                    case CommonConstants.EMPLOYEENAME_EMP_SKILL_SEARCH_RPT:
                        headerRow.Cells[0].Controls.Add(sortImage);
                        break;

                    case CommonConstants.DESIGNATION_EMP_SKILL_SEARCH_RPT:
                        headerRow.Cells[1].Controls.Add(sortImage);
                        break;

                    case CommonConstants.DEPARTMENT_EMP_SKILL_SEARCH_RPT:
                        headerRow.Cells[2].Controls.Add(sortImage);
                        break;

                    case CommonConstants.PROJECTS_ALLOCATED_EMP_SKILL_SEARCH_RPT:
                        headerRow.Cells[3].Controls.Add(sortImage);
                        break;

                    case CommonConstants.PRIMARY_SKILLS_EMP_SKILL_SEARCH_RPT:
                        headerRow.Cells[4].Controls.Add(sortImage);
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


}
