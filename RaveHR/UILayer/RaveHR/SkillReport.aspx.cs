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
using Common;
using Common.AuthorizationManager;
using Rave.HR.BusinessLayer;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.DirectoryServices;
using Common.Constants;


public partial class SkillReport : BaseClass
{
    #region Members Variables

    BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
    Rave.HR.BusinessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.BusinessLayer.MRF.MRFDetail();
    BusinessEntities.Employee employee = new BusinessEntities.Employee();

    /// <summary>
    /// Default value for DropDown 
    /// </summary>
    private const string SELECT = "Select";
    private const string Level0 = "Level 0";
    private const string Level1 = "Level 1";
    private const string Level2 = "Level 2";
    private const string Level3 = "Level 3";

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


    /// <summary>
    /// All the parameters to be passed to SP
    /// </summary>
    BusinessEntities.ParameterCriteria objParameter = new BusinessEntities.ParameterCriteria();

    private const string CLASS_NAME = "SkillReport.aspx.cs";

    /// <summary>
    /// Determines the total no. roles for user.
    /// </summary>
    ArrayList rolesForUser = new ArrayList();

    string UserRaveDomainId;
    string UserMailId;

    /// <summary>
    /// Contains the list of roles
    /// </summary>
    ArrayList arrRolesForUser = new ArrayList();

    #endregion Members Variables

    #region Protected Events

    protected void Page_Load(object sender, EventArgs e)
    {
        //Make filter button as a default button.
        Page.Form.DefaultButton = btnFilter.UniqueID;

        if (!ValidateURL())
        {
            Response.Redirect(CommonConstants.INVALIDURL, false);
        }
        else
        {
            //GVResourcesOnboard.RowCommand += new GridViewCommandEventHandler(GVResourcesOnboard_RowCommand);

            // Get logged-in user's email id
            AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
            UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
            UserMailId = UserRaveDomainId.Replace("co.in", "com");
            AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
            arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(UserRaveDomainId);

            if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEEMPLOYEE) && arrRolesForUser.Count == 1)
            {
                Response.Redirect(CommonConstants.PAGE_HOME, true);
            }

            this.GetRolesforUser();

            if (!Page.IsPostBack)
            {
                Session["Level2Skill"] = "";
                Session["Level3Skill"] = "";

                ddlLevel.Items.Clear();
                ddlLevel.Items.Insert(CommonConstants.ZERO, SELECT);
                ddlLevel.Items.Insert(1, Level0);
                ddlLevel.Items.Insert(2, Level1);
                ddlLevel.Items.Insert(3, Level2);
                ddlLevel.Items.Insert(4, Level3);
                ddlLevel.SelectedIndex = 0;

                HidePanel();
                Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] = null;
            }
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlLevel.SelectedIndex != 0)
            {
                GetSkillReport("[Resource Type] Asc");
            }
            else
            {
                HidePanel();
                btnExport.Visible = false;
            }                
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnFilter_Click", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }



    #endregion Protected Events

    #region Private Methods

    protected void HidePanel()
    {
        PnlLevel0.Visible = false;
        PnlLevel1.Visible = false;
        PnlLevel2.Visible = false;
        PnlLevel3.Visible = false;
    }

    private void GetSkillReport(string SortExpressionAndDirection)
    {
        BusinessEntities.Employee employee = new BusinessEntities.Employee();
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
        Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
        try
        {
            HidePanel();
            DataSet ds = new DataSet();
            ds = employeeBL.GetSkillReport(Convert.ToInt32(ddlLevel.SelectedIndex) - 1, SortExpressionAndDirection);

            if (ds != null)
            {
                if (ddlLevel.SelectedValue.Equals(Level0))
                {
                    gvLevel0.DataSource = ds.Tables[0];
                    Session["CountLVL0"] = Convert.ToString(ds.Tables[0].Rows.Count);
                    gvLevel0.DataBind();
                    PnlLevel0.Visible = true;
                  
                    //Siddharth 16th Feb 2015
                    int count = Convert.ToInt16(ds.Tables[0].Compute("SUM([Resource Type Count])", "").ToString());
                    Level0Count.Text = "&nbsp;&nbsp;(Total Count : " + Convert.ToString(count) + ")";

                    if (ds.Tables[0].Rows.Count == 0)
                        btnExport.Visible = false;
                    else
                        btnExport.Visible = true;
                }
                else if (ddlLevel.SelectedValue.Equals(Level1))
                {
                    gvLevel1NPS.DataSource = ds.Tables[0];
                    Session["CountLVL1NPS"] = Convert.ToString(ds.Tables[0].Rows.Count);
                    gvLevel1NPS.DataBind();
                    gvLevel1NGA.DataSource = ds.Tables[1];
                    Session["CountLVL1NGA"] = Convert.ToString(ds.Tables[1].Rows.Count);
                    gvLevel1NGA.DataBind();
                    PnlLevel1.Visible = true;

                    //Siddharth 16th Feb 2015
                    int countNPS = Convert.ToInt16(ds.Tables[0].Compute("SUM([Resource Type Count])", String.Empty).ToString());
                    int countNGA = Convert.ToInt16(ds.Tables[1].Compute("SUM([Resource Type Count])", String.Empty).ToString());

                    Level1NPSCount.Text = "&nbsp;&nbsp;(Total Count : " + Convert.ToString(countNPS) + ")";
                    Level1NGSCount.Text = "&nbsp;&nbsp;(Total Count : " + Convert.ToString(countNGA) + ")";

                    if (ds.Tables[0].Rows.Count == 0 || ds.Tables[1].Rows.Count == 0)
                        btnExport.Visible = false;
                    else
                        btnExport.Visible = true;
                }
                else if (ddlLevel.SelectedValue.Equals(Level2))
                {
                    gvLevel2.DataSource = ds.Tables[0];
                    Session["Count2LVL"] = Convert.ToString(ds.Tables[0].Rows.Count);
                    gvLevel2.DataBind();
                    Session["Level2Skill"] = ds.Tables[1];
                    PnlLevel2.Visible = true;

                    //Siddharth 16th Feb 2015
                    int CountLvl2 = Convert.ToInt16(ds.Tables[0].Compute("SUM(Total)", String.Empty).ToString());
                    Level2Count.Text = "&nbsp;&nbsp;(Total Count : " + Convert.ToString(CountLvl2) + ")";

                    if (ds.Tables[1].Rows.Count == 0)
                        btnExport.Visible = false;
                    else
                        btnExport.Visible = true;
                }
                else if (ddlLevel.SelectedValue.Equals(Level3))
                {
                    gvLevel3.DataSource = ds.Tables[0];
                    Session["Count3LVL"] = Convert.ToString(ds.Tables[0].Rows.Count);
                    gvLevel3.DataBind();
                    Session["Level3Skill"] = ds.Tables[1];
                    PnlLevel3.Visible = true;

                    //Siddharth 16th Feb 2015
                    int CountLvl3 = Convert.ToInt16(ds.Tables[0].Compute("SUM(Total)", String.Empty).ToString());
                    Level3Count.Text = "&nbsp;&nbsp;(Total Count : " + Convert.ToString(CountLvl3) + ")";


                    if (ds.Tables[1].Rows.Count == 0)
                        btnExport.Visible = false;
                    else
                        btnExport.Visible = true;
                }
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetSkillLevel0", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    /// <summary>
    /// Gets the Role for Logged in User
    /// </summary>
    private void GetRolesforUser()
    {
        // Get the roles of user which is stored in the session variable
        rolesForUser = this.GetUserAndRoles();

        //Parse through the array and assign individual role to object
        foreach (string STR in rolesForUser)
        {
            switch (STR)
            {
                case AuthorizationManagerConstants.ROLERPM:
                    objParameter.Role = AuthorizationManagerConstants.ROLERPM;
                    break;

                case AuthorizationManagerConstants.ROLEPROJECTMANAGER:
                    objParameter.Role = AuthorizationManagerConstants.ROLEPROJECTMANAGER;
                    break;

                case AuthorizationManagerConstants.ROLEHR:
                    objParameter.Role = AuthorizationManagerConstants.ROLEHR;
                    break;

                default:
                    break;
            }
        }

        if ((objParameter.RoleRPM == AuthorizationManagerConstants.ROLERPM) || (objParameter.RoleCEO == AuthorizationManagerConstants.ROLECEO) || (objParameter.RoleCOO == AuthorizationManagerConstants.ROLECOO))
        {
            objParameter.RoleRPM = AuthorizationManagerConstants.ROLERPM;
            objParameter.RoleCEO = AuthorizationManagerConstants.ROLERPM;
            objParameter.RoleCOO = AuthorizationManagerConstants.ROLERPM;
        }

        if ((objParameter.RoleCFM == AuthorizationManagerConstants.ROLECFM) || (objParameter.RoleFM == AuthorizationManagerConstants.ROLEFM))
        {
            objParameter.RoleCFM = AuthorizationManagerConstants.ROLECFM;
            objParameter.RoleFM = AuthorizationManagerConstants.ROLECFM;
        }
    }

    private ArrayList GetUserAndRoles()
    {
        ArrayList usersRole = new ArrayList();
        AuthorizationManager athManager = new AuthorizationManager();
        string emailID = athManager.getLoggedInUser();
        usersRole = athManager.getRolesForUser(emailID);

        return usersRole;
    }

    #region Export to excel 1
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {           
            if (ddlLevel.SelectedValue.Equals(Level0))
            {
                GridView[] gvList = null;
                gvList = new GridView[] { gvLevel0 };
                ExportAv("SkillReport-Level0.xls", gvList, "Level 0- Skills Report", 2, Convert.ToString(Level0Count.Text), string.Empty);
            }
            else if (ddlLevel.SelectedValue.Equals(Level1))
            {
                GridView[] gvList = null;
                gvList = new GridView[] { gvLevel1NPS , gvLevel1NGA};
                ExportAv("SkillReport-Level1.xls", gvList, "Level 1- Skills Report (Division) - NPS", 2, Convert.ToString(Level1NPSCount.Text), Convert.ToString(Level1NGSCount.Text));
            }
            else if (ddlLevel.SelectedValue.Equals(Level2))
            {
                Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
                DataSet ds = new DataSet();
                GridView TempgvLevel2 = new GridView();

                ds = employeeBL.GetSkillReport(Convert.ToInt32(ddlLevel.SelectedIndex) - 1, "[Resource Type] Asc");
                TempgvLevel2.DataSource = ds.Tables[1];
                TempgvLevel2.DataBind();

                GridView[] gvList = null;
                gvList = new GridView[] { TempgvLevel2 };
                ExportAv("SkillReport-Level2.xls", gvList, "Level 2- Skills Report( Business Area)", 3, Convert.ToString(Level2Count.Text), string.Empty);
            }
            else if (ddlLevel.SelectedValue.Equals(Level3))
            {
                Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
                DataSet ds = new DataSet();
                GridView TempgvLevel3 = new GridView();

                ds = employeeBL.GetSkillReport(Convert.ToInt32(ddlLevel.SelectedIndex) - 1, "[Resource Type] Asc");
                TempgvLevel3.DataSource = ds.Tables[1];
                TempgvLevel3.DataBind();

                GridView[] gvList = null;
                gvList = new GridView[] { TempgvLevel3 };
                ExportAv("SkillReport-Level3.xls", gvList, "Level 3- Skills Report( Business Segment)", 3, Convert.ToString(Level3Count.Text), string.Empty);
            }            
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnExport_Click", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    public static void ExportAv(string fileName, GridView[] gvs, string Heading, int NoColumn, string SkillCount, string NGASkillCount)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=\"" + fileName + "\"");
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        System.IO.StringWriter sw = new System.IO.StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        int GVCount = 0;

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
                    TableCell cellCaption = new TableCell();
                    cellCaption.Text = Heading + " " + SkillCount.ToString();
                    if (GVCount > 0)
                    {
                        cellCaption.ColumnSpan = NoColumn;
                        TableRow trCaption1 = new TableRow();
                        trCaption1.Controls.Add(cellCaption);
                        table.Rows.Add(trCaption1);

                        cellCaption.Text = "Level 1- Skills Report (Division) - NGA" + NGASkillCount;
                    }
                    cellCaption.ColumnSpan = NoColumn;
                    cellCaption.Font.Bold = true;
                    //cellCaption.Font.Size = 15;
                    TableRow trCaption = new TableRow();
                    trCaption.Controls.Add(cellCaption);
                    table.Rows.Add(trCaption);

                    //TableCell cell = new TableCell();                    
                    //cell.Font.Bold = true;
                    ////cell.HorizontalAlign = "Center";
                    //cell.Style.Add("text-Decoration", "bold");
                    //cell.ColumnSpan = NoColumn;
                    //TableRow tr = new TableRow();
                    //tr.Controls.Add(cell);
                    //table.Rows.Add(tr);
                    if (GVCount > 0)
                    {
                        table.Rows.Add(trCaption);
                    }
                    GVCount++;
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
        //HttpContext.Current.Response.Write(headerTable);
        HttpContext.Current.Response.Write(sw.ToString());

        HttpContext.Current.Response.Flush();// Sends all currently buffered output to the client.
        HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
        HttpContext.Current.ApplicationInstance.CompleteRequest();// Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
        //HttpContext.Current.Response.End();


    }
    #endregion

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    #endregion Private Methods

    protected void gvLevel2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ChildGridSkillsForResource")
            {
                GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                ImageButton imgbtnExpandCollaspeChildGrid = (ImageButton)grv.FindControl("imgbtnExpandCollaspeChildGrid");
                GridView grdAllProjectGrid = (GridView)grv.FindControl("gvDetailSkills");
                HtmlTableRow tr_ProjectGrid = (HtmlTableRow)grv.FindControl("tr_DetalGrid");
                if ((imgbtnExpandCollaspeChildGrid != null) && (tr_ProjectGrid != null))
                {
                    if (imgbtnExpandCollaspeChildGrid.ImageUrl == Common.CommonConstants.IMAGE_MINUSSIGNPATH)
                    {
                        tr_ProjectGrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        imgbtnExpandCollaspeChildGrid.ImageUrl = Common.CommonConstants.IMAGE_PLUSSIGNPATH;
                        return;
                    }
                }

                // List<BusinessEntities.Employee> objEmployeeList = new List<BusinessEntities.Employee>();

                foreach (GridViewRow grvRow in gvLevel2.Rows)
                {
                    ImageButton imgbtnExpandCollaspe = (ImageButton)grvRow.FindControl("imgbtnExpandCollaspeChildGrid");
                    HtmlTableRow tr_ChildGrid = (HtmlTableRow)grvRow.FindControl("tr_DetalGrid");
                    if (tr_ChildGrid != null)
                    {
                        tr_ChildGrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        imgbtnExpandCollaspe.ImageUrl = Common.CommonConstants.IMAGE_PLUSSIGNPATH;
                    }
                }

                if (grdAllProjectGrid != null)
                {
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string CmdBA = commandArgs[0].ToString();
                    string[] commandArgsResourceType = commandArgs[1].ToString().Split(new char[] { '(' });
                    string CmdRT = commandArgsResourceType[0].ToString();

                    string expression = "[Business Area] = '" + CmdBA + "' and [Resource Type] = '" + CmdRT + "'";
                    DataTable dtDetailsSkill = new DataTable();
                    dtDetailsSkill = (DataTable)Session["Level2Skill"];
                    //var VarSkillDetails = dtDetailsSkill.Select(expression);                
                    //grdAllProjectGrid.DataSource = dtDetailsSkill.Select(expression).CopyToDataTable();

                    DataView dv = new DataView(dtDetailsSkill);
                    dv.RowFilter = expression;
                    grdAllProjectGrid.DataSource = dv;

                    //grdAllProjectGrid.DataSource = VarSkillDetails.ToList();
                    grdAllProjectGrid.DataBind();
                    //grdAllProjectGrid.Visible = false;
                    //hfEmpid.Value = e.CommandArgument.ToString();

                }

                if ((imgbtnExpandCollaspeChildGrid != null) && (tr_ProjectGrid != null))
                {
                    imgbtnExpandCollaspeChildGrid.ImageUrl = Common.CommonConstants.IMAGE_MINUSSIGNPATH;
                    //Name: Sanju:Issue Id 50201  Removed display property  so that it should display grid properly in IE10,9,Chrome and mozilla browser.
                    // tr_ProjectGrid.Style.Add(HtmlTextWriterStyle.Display, "block");
                    tr_ProjectGrid.Style.Add(HtmlTextWriterStyle.Display, "");
                    imgbtnExpandCollaspeChildGrid.ToolTip = "Collapse";
                }
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "gvLevel2_RowCommand", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    protected void gvLevel3_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ChildGridSkillsForResource")
            {
                GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                ImageButton imgbtnExpandCollaspeChildGrid = (ImageButton)grv.FindControl("imgbtnExpandCollaspeChildGrid3");
                GridView grdAllProjectGrid = (GridView)grv.FindControl("gvDetailSkills3");
                HtmlTableRow tr_ProjectGrid = (HtmlTableRow)grv.FindControl("tr_DetalGrid3");
                if ((imgbtnExpandCollaspeChildGrid != null) && (tr_ProjectGrid != null))
                {
                    if (imgbtnExpandCollaspeChildGrid.ImageUrl == Common.CommonConstants.IMAGE_MINUSSIGNPATH)
                    {
                        tr_ProjectGrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        imgbtnExpandCollaspeChildGrid.ImageUrl = Common.CommonConstants.IMAGE_PLUSSIGNPATH;
                        return;
                    }
                }

                // List<BusinessEntities.Employee> objEmployeeList = new List<BusinessEntities.Employee>();

                foreach (GridViewRow grvRow in gvLevel3.Rows)
                {
                    ImageButton imgbtnExpandCollaspe = (ImageButton)grvRow.FindControl("imgbtnExpandCollaspeChildGrid3");
                    HtmlTableRow tr_ChildGrid = (HtmlTableRow)grvRow.FindControl("tr_DetalGrid3");
                    if (tr_ChildGrid != null)
                    {
                        tr_ChildGrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        imgbtnExpandCollaspe.ImageUrl = Common.CommonConstants.IMAGE_PLUSSIGNPATH;
                    }
                }

                if (grdAllProjectGrid != null)
                {
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string CmdBA = commandArgs[0].ToString();
                    string[] commandArgsResourceType = commandArgs[1].ToString().Split(new char[] { '(' });
                    string CmdRT = commandArgsResourceType[0].ToString();

                    string expression = "[Business Segment] = '" + CmdBA + "' and [Resource Type] = '" + CmdRT + "'";
                    DataTable dtDetailsSkill = new DataTable();
                    dtDetailsSkill = (DataTable)Session["Level3Skill"];
                    //var VarSkillDetails = dtDetailsSkill.Select(expression);                
                    //grdAllProjectGrid.DataSource = dtDetailsSkill.Select(expression).CopyToDataTable();

                    DataView dv = new DataView(dtDetailsSkill);
                    dv.RowFilter = expression;
                    grdAllProjectGrid.DataSource = dv;

                    //grdAllProjectGrid.DataSource = VarSkillDetails.ToList();
                    grdAllProjectGrid.DataBind();
                    //grdAllProjectGrid.Visible = false;
                    //hfEmpid.Value = e.CommandArgument.ToString();

                }

                if ((imgbtnExpandCollaspeChildGrid != null) && (tr_ProjectGrid != null))
                {
                    imgbtnExpandCollaspeChildGrid.ImageUrl = Common.CommonConstants.IMAGE_MINUSSIGNPATH;
                    //Name: Sanju:Issue Id 50201  Removed display property  so that it should display grid properly in IE10,9,Chrome and mozilla browser.
                    // tr_ProjectGrid.Style.Add(HtmlTextWriterStyle.Display, "block");
                    tr_ProjectGrid.Style.Add(HtmlTextWriterStyle.Display, "");
                    imgbtnExpandCollaspeChildGrid.ToolTip = "Collapse";
                }
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "gvLevel3_RowCommand", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    #region "Sorting Events"

    protected void gvLevel0_Sorting(object sender, GridViewSortEventArgs e)
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "SkillReport", "Page_Load", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }



    protected void gvLevel1NPS_Sorting(object sender, GridViewSortEventArgs e)
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "SkillReport", "Page_Load", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }





    protected void gvLevel1NGA_Sorting(object sender, GridViewSortEventArgs e)
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "SkillReport", "Page_Load", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    protected void gvLevel2_Sorting(object sender, GridViewSortEventArgs e)
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "SkillReport", "Page_Load", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }



    //protected void gvDetailSkills_Sorting(object sender, GridViewSortEventArgs e)
    //{
    //    try
    //    {
    //        // GridViewRow gvrPager = GVResourcesOnboard.BottomPagerRow;
    //        // TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
    //        // txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_EMP].ToString();

    //        //On sorting assign new sort expression
    //        sortExpression = e.SortExpression;

    //        if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] == null)
    //        {
    //            Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] = sortExpression;
    //        }

    //        if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP].ToString() == sortExpression)
    //        {
    //            //Sort to opposite direction based upon previous sort direction
    //            if (Session[SessionNames.SORT_DIRECTION_EMP] == null || GridViewSortDirection == System.Web.UI.WebControls.SortDirection.Descending)
    //            {
    //                GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Ascending;
    //                SortGridView(sortExpression, ASCENDING);
    //            }
    //            else
    //            {
    //                GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Descending;
    //                SortGridView(sortExpression, DESCENDING);
    //            }
    //        }
    //        else
    //        {
    //            GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Ascending;
    //            SortGridView(sortExpression, ASCENDING);
    //        }

    //        if (Session[SessionNames.EMP_PROJECTID] != null || Session[SessionNames.EMP_DEPARTMENTID] != null || Session[SessionNames.EMP_ROLE] != null || Session[SessionNames.EMP_STATUSID] != null || Session[SessionNames.EMP_NAME] != null)
    //        {
    //            //Change here 26 March 2015
    //            //btnRemoveFilter.Visible = true;
    //        }

    //        //Set current sort expression as PreviousSortExpression
    //        Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] = sortExpression;
    //    }

    //    catch
    //    {

    //    }
    //}


    protected void gvLevel3_Sorting(object sender, GridViewSortEventArgs e)
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "SkillReport", "Page_Load", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    //protected void gvDetailSkills3_Sorting(object sender, GridViewSortEventArgs e)
    //{
    //    try
    //    {
    //        // GridViewRow gvrPager = GVResourcesOnboard.BottomPagerRow;
    //        // TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
    //        // txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_EMP].ToString();

    //        //On sorting assign new sort expression
    //        sortExpression = e.SortExpression;

    //        if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] == null)
    //        {
    //            Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] = sortExpression;
    //        }

    //        if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP].ToString() == sortExpression)
    //        {
    //            //Sort to opposite direction based upon previous sort direction
    //            if (Session[SessionNames.SORT_DIRECTION_EMP] == null || GridViewSortDirection == System.Web.UI.WebControls.SortDirection.Descending)
    //            {
    //                GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Ascending;
    //                SortGridView(sortExpression, ASCENDING);
    //            }
    //            else
    //            {
    //                GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Descending;
    //                SortGridView(sortExpression, DESCENDING);
    //            }
    //        }
    //        else
    //        {
    //            GridViewSortDirection = System.Web.UI.WebControls.SortDirection.Ascending;
    //            SortGridView(sortExpression, ASCENDING);
    //        }

    //        if (Session[SessionNames.EMP_PROJECTID] != null || Session[SessionNames.EMP_DEPARTMENTID] != null || Session[SessionNames.EMP_ROLE] != null || Session[SessionNames.EMP_STATUSID] != null || Session[SessionNames.EMP_NAME] != null)
    //        {
    //            //Change here 26 March 2015
    //            //btnRemoveFilter.Visible = true;
    //        }

    //        //Set current sort expression as PreviousSortExpression
    //        Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] = sortExpression;
    //    }

    //    catch
    //    {

    //    }
    //}

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
            this.GetSkillReport(objParameter.SortExpressionAndDirection);

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
    protected void gvLevel0_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (((Session["CountLVL0"] != null) && (int.Parse(Session["CountLVL0"].ToString()) > 1))) 
                 {
                    //Add sort Images to Grid View Header
                    AddSortImage_Level0(e.Row);
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
    private void AddSortImage_Level0(GridViewRow headerRow)
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
                    case CommonConstants.RESOURCE_TYPE_SKILL_RPT:
                        headerRow.Cells[0].Controls.Add(sortImage);
                        break;

                    case CommonConstants.RESOURCE_TYPE_COUNT_SKILL_RPT:
                        headerRow.Cells[1].Controls.Add(sortImage);
                        break;

                    case CommonConstants.BUSINESS_AREA:
                        headerRow.Cells[1].Controls.Add(sortImage);
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


    /// <summary>
    /// Add the Sort Image to gridview header row
    /// </summary>
    /// <param name="headerRow"></param>
    private void AddSortImage_Level2(GridViewRow headerRow)
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

                    case CommonConstants.BUSINESS_AREA_SKILL_RPT:
                        headerRow.Cells[0].Controls.Add(sortImage);
                        break;

                    case CommonConstants.RESOURCE_TYPE_SKILL_RPT:
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


    
        /// <summary>
    /// Add the Sort Image to gridview header row
    /// </summary>
    /// <param name="headerRow"></param>
    private void AddSortImage_Level3(GridViewRow headerRow)
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

                    case CommonConstants.BUSINESS_SEGMENT_SKILL_RPT:
                        headerRow.Cells[0].Controls.Add(sortImage);
                        break;

                    case CommonConstants.RESOURCE_TYPE_SKILL_RPT:
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

    protected void gvLevel1NPS_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (((Session["CountLVL1NPS"] != null) && (int.Parse(Session["CountLVL1NPS"].ToString()) > 1)))
                {
                    //Add sort Images to Grid View Header
                    AddSortImage_Level0(e.Row);
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



    protected void gvLevel1NGA_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (((Session["CountLVL1NGA"] != null) && (int.Parse(Session["CountLVL1NGA"].ToString()) > 1)))
                {
                    //Add sort Images to Grid View Header
                    AddSortImage_Level0(e.Row);
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




    protected void gvLevel2_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (((Session["Count2LVL"] != null) && (int.Parse(Session["Count2LVL"].ToString()) > 1)))
                {
                    //Add sort Images to Grid View Header
                    AddSortImage_Level2(e.Row);
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


    protected void gvLevel3_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (((Session["Count3LVL"] != null) && (int.Parse(Session["Count3LVL"].ToString()) > 1)))
                {
                    //Add sort Images to Grid View Header
                    AddSortImage_Level3(e.Row);
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
    
    #endregion "Sorting Events"

}
