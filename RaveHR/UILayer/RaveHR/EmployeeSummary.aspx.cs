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


public partial class EmployeeSummary : BaseClass
{
    #region Members Variables

    BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
    Rave.HR.BusinessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.BusinessLayer.MRF.MRFDetail();
    BusinessEntities.Employee employee = new BusinessEntities.Employee();

    /// <summary>
    /// Default value for DropDown 
    /// </summary>
    private const string SELECT = "Select";

    /// <summary>
    /// Defines Ascending sorting order for grid
    /// </summary>
    private const string ASCENDING = " ASC";

    /// <summary>
    /// Defines Descending sorting order for grid
    /// </summary>
    private const string DESCENDING = " DESC";

    /// <summary>
    /// Defines the command name Previous
    /// </summary>
    private const string PREVIOUS = "Previous";

    /// <summary>
    /// Defines the command name Next
    /// </summary>
    private const string NEXT = "Next";

    /// <summary>
    /// Defines a constant for Page Count
    /// </summary>
    private int pageCount = 0;

    private string ZERO = "0";
    private const string RELEASECOMMAND = "ReleaseImageBtn";
    private const string RELEASEBUTTONID = "imgRelease";
    private const string EMPID = "EmpID";
    //Ishwar Patil 01102014 For NIS : Start
    private const string _RMOGroupName = "RMOGroupName";
    //Ishwar Patil 01102014 For NIS : End

    /// <summary>
    /// All the parameters to be passed to SP
    /// </summary>
    BusinessEntities.ParameterCriteria objParameter = new BusinessEntities.ParameterCriteria();

    /// <summary>
    /// Determines the total no. roles for user.
    /// </summary>
    ArrayList rolesForUser = new ArrayList();

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
    /// Defines default value for sorting expression 
    /// </summary>
    private static string sortExpression = string.Empty;

    /// <summary>
    /// Sets the image direction either upwards or downwards
    /// </summary>
    private string imageDirection = string.Empty;

    /// <summary>
    /// Store the Value of row count
    /// </summary>
    Hashtable hashTable = new Hashtable();

    //27633-Subhra-start
    /// <summary>
    /// To store data for previous and next page for paging issue
    /// </summary>
    Hashtable temppreviousHashTable = new Hashtable();
    BusinessEntities.RaveHRCollection raveHRpreviousCollection = new BusinessEntities.RaveHRCollection();
    private int IntHashpageCount = 0;
    //end-27633

    private const string CLASS_NAME = "EmployeeSummary.aspx.cs";

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
        try
        {
            //Make filter button as a default button.
            Page.Form.DefaultButton = btnFilter.UniqueID;

            if (!ValidateURL())
            {
                Response.Redirect(CommonConstants.INVALIDURL, false);
            }
            else
            {
                //Siddharth 26th August 2015 Start
                //Task ID:- 56487 Hide the pages access for normal employees
                //ArrayList arrRolesForUser = new ArrayList();
                AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
                UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
                UserMailId = UserRaveDomainId.Replace("co.in", "com");
                //AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
                arrRolesForUser = objRaveHRAuthorizationManager.getRolesForUser(UserRaveDomainId);

                if (!(arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM) ||
                    arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEPM) ||
                    arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR) ||
                    arrRolesForUser.Contains(AuthorizationManagerConstants.ROLECOO) ||
                    arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEFINANCE) ||
                    arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEADMIN) ||
                    arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEFM) ||
                    arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEQUALITY) ||
                    arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERECRUITMENT) ||
                    arrRolesForUser.Contains(AuthorizationManagerConstants.ROLETESTING)))
                {
                    Response.Redirect(CommonConstants.PAGE_HOME, false);
                }
                //Siddharth 26th August 2015 End


                grdvListofEmployees.RowCommand += new GridViewCommandEventHandler(grdvListofEmployees_RowCommand);

                // Get logged-in user's email id

                //AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
                //UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
                //UserMailId = UserRaveDomainId.Replace("co.in", "com");
                //AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();

                ////ArrayList arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());
                ////arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());

                //arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(UserRaveDomainId);


                if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEEMPLOYEE) && arrRolesForUser.Count == 1)
                {
                    Response.Redirect(CommonConstants.PAGE_HOME, true);
                }

                this.GetRolesforUser();

                if (!Page.IsPostBack)
                {

                    if (Session[SessionNames.CURRENT_PAGE_INDEX_EMP] == null)
                        Session[SessionNames.CURRENT_PAGE_INDEX_EMP] = 1;

                    if (Session[SessionNames.CURRENT_PAGE_SIZE_EMP] == null)
                        Session[SessionNames.CURRENT_PAGE_SIZE_EMP] = 10;

                    // 29884-Ambar-Start-09062012
                    // Commented following code done by Rahul to avoid resetting curreent page index for employee page
                    ////START
                    ////Coded by Rahul P
                    ////26 May 10
                    ////Issue Id 18601
                    //if (Session[SessionNames.REFRESHPAGE] != null)
                    //  Session[SessionNames.CURRENT_PAGE_INDEX_EMP] = Session[SessionNames.REFRESHPAGE];
                    ////END
                    // 29884-Ambar-End-09062012

                    // Setting the Default sortExpression as MrfCode
                    if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] == null)
                    {
                        sortExpression = CommonConstants.EMP_FIRSTNAME;
                        Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] = sortExpression;
                    }
                    else
                    {
                        sortExpression = Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP].ToString();
                    }

                    //this.GetRolesforUser();
                    this.FillDropDowns();
                    ddlProject.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
                    ddlProject.Enabled = false;
                    //this.GetProjectName();
                    //this.FillRoleDepartmentWise(0);
                    GetEmployeeDesignations(0);
                    ddlRole.Enabled = false;
                    this.BindGridEmployeeSummary();

                    if (Session[SessionNames.EMP_NAME] != null)
                    {
                        txtEployeeName.Text = Session[SessionNames.EMP_NAME].ToString();
                    }
                    if (Session[SessionNames.EMP_DEPARTMENTID] != null)
                    {
                        ddlDepartment.SelectedValue = Session[SessionNames.EMP_DEPARTMENTID].ToString();
                        // 29937-Ambar-Start-19092011
                        // Called event to fill project and role drop down
                        ddlDepartment_SelectedIndexChanged(this, e);
                        // 29937-Ambar-End
                    }
                    if (Session[SessionNames.EMP_PROJECTID] != null)
                    {
                        // Assign ProjectId to Session
                        ddlProject.SelectedValue = Session[SessionNames.EMP_PROJECTID].ToString();
                    }
                    if (Session[SessionNames.EMP_ROLEID] != null)
                    {
                        // Assign RoleId to Session
                        ddlRole.SelectedValue = Session[SessionNames.EMP_ROLEID].ToString();
                    }
                    if (Session[SessionNames.EMP_STATUSID] != null)
                    {
                        // Assign MrfStatusId to Session
                        ddlStatus.SelectedValue = Session[SessionNames.EMP_STATUSID].ToString();
                    }
                    if (Session[SessionNames.EMP_PROJECTID] != null || Session[SessionNames.EMP_DEPARTMENTID] != null || Session[SessionNames.EMP_ROLE] != null || Session[SessionNames.EMP_STATUSID] != null || Session[SessionNames.EMP_NAME] != null)
                    {
                        btnRemoveFilter.Visible = true;
                    }

                    ////--Export Excel access
                    //Issue Id : 34521 START Changed IF condition.
                    //if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR))
                    //if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR) || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEPM) || Rave.HR.BusinessLayer.Employee.Employee.CheckDepartmentHeadbyEmailId(UserMailId))
                    //{
                    //    btnEmployeeDetails.Visible = true;
                    //}
                    //else
                    //{
                    //    btnEmployeeDetails.Visible = false;
                    //}

                    //Issue Id : 28572 Mahendra START
                    if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM))
                    {
                        btnRMFMHTML.Visible = true;
                    }
                    else
                    {
                        btnRMFMHTML.Visible = false;
                    }
                    //Issue Id : 28572 Mahendra END

                    // Ishwar NISRMS 17032015 Start
                    //Umesh: Hide Employee Status dropdown other than RMO and HR team - starts
                    if (!arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEHR)
                        && !arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM))
                    {
                        ddlStatus.Items.Remove(ddlStatus.Items.FindByText(CommonConstants.InActive));
                    }
                    //    trlblStatus.Attributes.Add("style", "display: ''");
                    //    trddlStatus.Attributes.Add("style", "display: ''");
                    //}
                    //else
                    //{
                    //    trlblStatus.Attributes.Add("style", "display: none");
                    //    trddlStatus.Attributes.Add("style", "display: none");
                    //}
                    //Umesh: Hide Employee Status dropdown other than RMO and HR team - ends
                    // Ishwar NISRMS 17032015 End

                    //Ishwar Patil 30092014 For NIS : Start
                    string strRMOGroupName = string.Empty;
                    if (ConfigurationManager.AppSettings[_RMOGroupName] != null)
                    {
                        strRMOGroupName = ConfigurationManager.AppSettings[_RMOGroupName].ToString();
                    }
                    Common.AuthorizationManager.AuthorizationManager objAuth =
                                                        new Common.AuthorizationManager.AuthorizationManager();
                    if (!strRMOGroupName.Contains(objAuth.getLoggedInUser()))
                    {
                        RBRMSEmp.Visible = false;
                    }
                    else
                    {
                        RBRMSEmp.Visible = true;
                    }

                    //Ishwar Patil 30092014 For NIS : End


                    //Siddharth 14 April 2015 Start
                    string strNISReportsAccess = string.Empty;
                    if (ConfigurationManager.AppSettings["NISReportsAccess"] != null)
                    {
                        strNISReportsAccess = ConfigurationManager.AppSettings["NISReportsAccess"].ToString();
                    }

                    if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM) )//|| (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEPM) && strNISReportsAccess.Contains(objAuth.getLoggedInUser())))
                    {
                        BtnCostCodeManager.Visible = true;
                    }
                    else
                    {
                        BtnCostCodeManager.Visible = false;
                    }
                    //Siddharth 14 April 2015 End
                }

                //if (ViewState[EMPID] != null)
                //{

                //    GridView grv = (GridView)grdvListofEmployees.FindControl("grdvDetailedListofEmployees");
                //    ImageButton imgRealeaseGrid = (ImageButton)grv.FindControl(RELEASEBUTTONID);

                //    int EmployeeID = Convert.ToInt32(ViewState[EMPID].ToString());
                //    imgRealeaseGrid.Attributes["onclick"] = "popUpEmployeeReleasePopulate('" + EmployeeID + "')";
                //}   
            }

            btnRMFMHTML.Attributes["onclick"] = "javascript:OpenUpdateManagerPopUp()";

            //Siddharth 9 April 2015 Start
            BtnCostCodeManager.Attributes["onclick"] = "javascript:OpenCostCodeManagerPopUp()";
            //Siddharth 9 April 2015 End
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


    void grdvListofEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ChildGridProjectsForEmployee")
            {
                GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                ImageButton imgbtnExpandCollaspeChildGrid = (ImageButton)grv.FindControl("imgbtnExpandCollaspeChildGrid");
                GridView grdAllProjectGrid = (GridView)grv.FindControl("grdvDetailedListofEmployees");
                HtmlTableRow tr_ProjectGrid = (HtmlTableRow)grv.FindControl("tr_EmpGrid");
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

                foreach (GridViewRow grvRow in grdvListofEmployees.Rows)
                {
                    ImageButton imgbtnExpandCollaspe = (ImageButton)grvRow.FindControl("imgbtnExpandCollaspeChildGrid");
                    HtmlTableRow tr_ChildGrid = (HtmlTableRow)grvRow.FindControl("tr_EmpGrid");
                    if (tr_ChildGrid != null)
                    {
                        tr_ChildGrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        imgbtnExpandCollaspe.ImageUrl = Common.CommonConstants.IMAGE_PLUSSIGNPATH;
                    }
                }

                if (grdAllProjectGrid != null)
                {
                    Rave.HR.BusinessLayer.Employee.Employee objEmployee = new Rave.HR.BusinessLayer.Employee.Employee();

                    //--Bind the grid.
                    grdAllProjectGrid.RowDataBound += new GridViewRowEventHandler(grdAllProjectGrid_RowDataBound);
                    grdAllProjectGrid.DataSource = objEmployee.BindGridDetailedEmployeeSummary(int.Parse(e.CommandArgument.ToString()));
                    grdAllProjectGrid.DataBind();
                    //grdAllProjectGrid.Visible = false;
                    //hfEmpid.Value = e.CommandArgument.ToString();
                    ViewState[EMPID] = e.CommandArgument.ToString();
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
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvListofEmployees_RowCommand", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
  

    void grdAllProjectGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToDateTime(e.Row.Cells[4].Text) == DateTime.MinValue)
                {
                    e.Row.Cells[4].Text = "";
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToDateTime(e.Row.Cells[5].Text) == DateTime.MinValue)
                {
                    e.Row.Cells[5].Text = "";
                }
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdAllProjectGrid_RowDataBound", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartment.SelectedItem.Text != Common.CommonConstants.PROJECTS)
            {
                ddlProject.SelectedValue = CommonConstants.SELECT;
            }
            if (ddlDepartment.SelectedItem.Text != Common.CommonConstants.SELECT)
            {
                //DepartmentWiseRoleDispaly(Convert.ToInt32(0));
                FillRoleDropdownAsPerDepartment();
            }
            else
            {
                //DepartmentWiseRoleDispaly(Convert.ToInt32(ddlDepartment.SelectedItem.Value));

                //FillRoleDropdownAsPerDepartment();
                ddlRole.Enabled = false;
                ddlProject.Enabled = false;
                ddlRole.Items.Clear();
                ddlRole.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlDepartment_SelectedIndexChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdvListofEmployees_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            GridViewRow gvrPager = grdvListofEmployees.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_EMP].ToString();

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
                btnRemoveFilter.Visible = true;
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

    protected void grdvListofEmployees_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (((Session[SessionNames.PAGE_COUNT_EMP] != null) && (int.Parse(Session[SessionNames.PAGE_COUNT_EMP].ToString()) > 1)) || ((raveHRCollection.Count > 1)))
                {
                    //Add sort Images to Grid View Header
                    AddSortImage(e.Row);
                }
            }

            if (e.Row.RowType == DataControlRowType.Pager)
            {
                DropDownList ddlPageSize = e.Row.Cells[0].FindControl("ddlPageSize") as DropDownList;
                string[] pageSizeItems = ConfigurationManager.AppSettings["PageSize"].ToString().Split(',');
                foreach (var item in pageSizeItems)
                {
                    ddlPageSize.Items.Add(new ListItem(item, item));
                }

                ddlPageSize.SelectedValue = Session[SessionNames.CURRENT_PAGE_SIZE_EMP].ToString();

                //selects item due to the GridView current page size
                ListItem li = ddlPageSize.Items.FindByText(grdvListofEmployees.PageSize.ToString());
                if (li != null)
                    ddlPageSize.SelectedIndex = ddlPageSize.Items.IndexOf(li);

                ddlPageSize.SelectedIndexChanged += new EventHandler(ddlPageSize_SelectedIndexChanged);
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvListofEmployees_RowCreated", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            // Assign EmployeeName to Session
            Session[SessionNames.EMP_NAME] = txtEployeeName.Text.Trim();
            // Assign DepartmentId to Session
            Session[SessionNames.EMP_DEPARTMENTID] = ddlDepartment.SelectedItem.Value;
            // Assign ProjectId to Session
            Session[SessionNames.EMP_PROJECTID] = ddlProject.SelectedItem.Value;
            // Assign RoleId to Session
            Session[SessionNames.EMP_ROLEID] = ddlRole.SelectedItem.Value;
            // Assign MrfStatusId to Session
            Session[SessionNames.EMP_STATUSID] = ddlStatus.SelectedItem.Value;

            // 29884-Ambar-Start-09062012
            Session[SessionNames.REFRESHPAGE] = null;
            // 29884-Ambar-End-09062012

            if (grdvListofEmployees.BottomPagerRow != null)
            {
                GridViewRow gvrPager = grdvListofEmployees.BottomPagerRow;
                TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");


                if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0)
                {
                    txtPages.Text = 1.ToString();

                    //START
                    //Coded by Rahul P
                    //26 May 10
                    //Issue Id 18601
                    if (Session[SessionNames.REFRESHPAGE] != null)
                        Session[SessionNames.CURRENT_PAGE_INDEX_EMP] = Session[SessionNames.REFRESHPAGE];
                    else
                        //END
                        Session[SessionNames.CURRENT_PAGE_INDEX_EMP] = txtPages.Text;
                }
            }

            this.BindGridEmployeeSummary();

            //grdvListofEmployees.Columns[0].Visible = false;

            if (Session[SessionNames.EMP_DEPARTMENTID] != null || Session[SessionNames.EMP_PROJECTID] != null || Session[SessionNames.EMP_ROLEID] != null || Session[SessionNames.EMP_STATUSID] != null || Session[SessionNames.EMP_NAME] != null)
            {
                btnRemoveFilter.Visible = true;
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnFilter_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Change the page on click of Previous or Next linkbutton and bind grid.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ChangePage(object sender, CommandEventArgs e)
    {
        try
        {
            GridViewRow gvrPager = grdvListofEmployees.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");

            // 29884-Ambar-Start-09062012
            Session[SessionNames.REFRESHPAGE] = null;
            // 29884-Ambar-End-09062012

            switch (e.CommandName)
            {
                //Previous button is clicked
                case PREVIOUS:
                    //To solved the issue id : 20375
                    //Start
                    if ((Convert.ToInt32(txtPages.Text) - 1) == 0)
                    {
                        Session[SessionNames.CURRENT_PAGE_INDEX_EMP] = 1;
                        txtPages.Text = "1";
                    }
                    else
                    {
                        //End
                        Session[SessionNames.CURRENT_PAGE_INDEX_EMP] = Convert.ToInt32(txtPages.Text) - 1;
                        txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) - 1);
                    }
                    break;

                //Next button is clicked
                case NEXT:
                    Session[SessionNames.CURRENT_PAGE_INDEX_EMP] = Convert.ToInt32(txtPages.Text) + 1;
                    txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) + 1);
                    break;
            }

            // Bind the grid on paging.
            BindGridEmployeeSummary();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ChangePage", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Text change of pager textbox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtPages_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txtPages = (TextBox)sender;

            // 29884-Ambar-Start-09062012
            Session[SessionNames.REFRESHPAGE] = null;
            // 29884-Ambar-End-09062012

            //Check for Paging text box is not empty, non-zero, and out of range paging no.
            if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0 && Convert.ToInt32(txtPages.Text) <= Convert.ToInt32(Session[SessionNames.PAGE_COUNT_EMP].ToString()))
            {
                grdvListofEmployees.PageIndex = Convert.ToInt32(txtPages.Text) - 1;
                Session[SessionNames.CURRENT_PAGE_INDEX_EMP] = txtPages.Text;
            }
            else
            {
                txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_EMP].ToString();
                return;
            }

            //Bind the grid on paging
            BindGridEmployeeSummary();

            //Assign the tetbox current page no.
            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_EMP].ToString();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "txtPages_TextChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdvListofEmployees_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (e.NewPageIndex != -1)
            {
                // Assign the new page index
                grdvListofEmployees.PageIndex = e.NewPageIndex;
            }

            // Bind the grid as per new page index.
            BindGridEmployeeSummary();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvListofEmployees_PageIndexChanging", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdvListofEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (((HiddenField)e.Row.Cells[0].FindControl("hfProjectCount")).Value == ZERO)
                {
                    if (((HiddenField)e.Row.Cells[0].FindControl("hfClientCount")).Value == ZERO)

                        e.Row.Cells[0].FindControl("imgbtnExpandCollaspeChildGrid").Visible = false;
                }

                //Name: Sanju:Issue Id 50201  Changed cursor property to pointer so that it should display hand in IE10,9,Chrome and mozilla browser.
                // e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
                // Mohamed : NIS-RMS : 07/01/2015 : Starts                        			  
                // Desc : Remove underline and click from remaining columns

                e.Row.Cells[1].Attributes["onmouseover"] = "this.style.cursor='pointer';this.style.textDecoration='underline';";

                e.Row.Cells[1].Attributes["onmouseout"] = "this.style.textDecoration='none';";

                e.Row.Cells[1].Attributes["onclick"] = "location.href = 'EmployeeDetails.aspx?" + URLHelper.SecureParameters("EMPId", DataBinder.Eval(e.Row.DataItem, "EmpId").ToString()) + "&" + URLHelper.SecureParameters("index", e.Row.RowIndex.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "EMPLOYEESUMMERY") + "&" + URLHelper.CreateSignature(DataBinder.Eval(e.Row.DataItem, "EmpId").ToString(), e.Row.RowIndex.ToString(), "EMPLOYEESUMMERY") + "'";
                // Ishwar NISRMS 17032015 Start
                Label lblResignationDate = (Label)e.Row.FindControl("lblFirstName");
                if (lblResignationDate.Text != "-")
                {
                    e.Row.Cells[7].ForeColor = System.Drawing.Color.Red;
                }
                // Ishwar NISRMS 17032015 End
                //e.Row.Cells[2].Attributes["onclick"] = "location.href = 'EmployeeDetails.aspx?" + URLHelper.SecureParameters("EMPId", DataBinder.Eval(e.Row.DataItem, "EmpId").ToString()) + "&" + URLHelper.SecureParameters("index", e.Row.RowIndex.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "EMPLOYEESUMMERY") + "&" + URLHelper.CreateSignature(DataBinder.Eval(e.Row.DataItem, "EmpId").ToString(), e.Row.RowIndex.ToString(), "EMPLOYEESUMMERY") + "'";

                //e.Row.Cells[3].Attributes["onclick"] = "location.href = 'EmployeeDetails.aspx?" + URLHelper.SecureParameters("EMPId", DataBinder.Eval(e.Row.DataItem, "EmpId").ToString()) + "&" + URLHelper.SecureParameters("index", e.Row.RowIndex.ToString()) + "&" + URLHelper.SecureParameters("pagetype", "EMPLOYEESUMMERY") + "&" + URLHelper.CreateSignature(DataBinder.Eval(e.Row.DataItem, "EmpId").ToString(), e.Row.RowIndex.ToString(), "EMPLOYEESUMMERY") + "'";

                //e.Row.Cells[0].Attributes["onmouseover"] = "this.style.cursor='';";

                // Mohamed : NIS-RMS : 07/01/2015 : Ends
                //To solved the issue id : 20375
                //Start
                if (!hashTable.Contains(e.Row.RowIndex))
                    hashTable.Add(e.Row.RowIndex, DataBinder.Eval(e.Row.DataItem, "EMPId").ToString());
                //End
            }
            Session[SessionNames.EMPLOYEEVIEWINDEX] = hashTable;
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvListofEmployees_RowDataBound", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdvListofEmployees_DataBound(object sender, EventArgs e)
    {
        try
        {
            GridViewRow gvrPager = grdvListofEmployees.BottomPagerRow;

            if (gvrPager == null) return;

            //Get Text Box and Lable from the Grid View
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            Label lblPageCount = (Label)gvrPager.Cells[0].FindControl("lblPageCount");

            //Assign current page index to text box
            txtPages.Text = Session[SessionNames.CURRENT_PAGE_INDEX_EMP].ToString();


            //Assign total no of pages to label
            if (lblPageCount != null)
                lblPageCount.Text = pageCount.ToString();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvListofEmployees_DataBound", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// On remove Filter Clear all the Filtering criteria and once again bind the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRemoveFilter_Click(object sender, EventArgs e)
    {
        try
        {
            // Assign EmployeeId to Session
            Session[SessionNames.EMPLOYEEID] = null;
            // Assign EmployeeName to Session
            Session[SessionNames.EMP_NAME] = null;
            // Assign Emp DepartmentId to Session
            Session[SessionNames.EMP_DEPARTMENTID] = null;
            // Assign Emp ProjectId to Session
            Session[SessionNames.EMP_PROJECTID] = null;
            // Assign Emp StatusId to Session
            Session[SessionNames.EMP_STATUSID] = null;
            // Assign Emp RoleID to Session
            Session[SessionNames.EMP_ROLEID] = null;

            sortExpression = CommonConstants.EMP_FIRSTNAME;
            Session[SessionNames.PREVIOUS_SORT_EXPRESSION_APPREJEMP] = sortExpression;

            //START
            //Coded by Rahul P
            //26 May 10
            //Issue Id 18601
            // Assign Default Page to Session
            Session[SessionNames.REFRESHPAGE] = null;
            //END
            ClearFilteringFields();
            BindGridEmployeeSummary();
            btnRemoveFilter.Visible = false;
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnRemoveFilter_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    //Ishwar Patil 01102014 For NIS : Start
    protected void OnSelectedIndexChanged_RBRMSEmp(object sender, EventArgs e)
    {
        try
        {
            this.BindGridEmployeeSummary();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "OnSelectedIndexChanged_RBRMSEmp", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    //Ishwar Patil 01102014 For NIS : End

    #endregion Protected Events

    #region Private Methods

    private void DepartmentWiseRoleDispaly(int DepartmentId)
    {
        FillRoleDepartmentWise(DepartmentId);
    }

    private void FillRoleDepartmentWise(int DepartmentId)
    {
        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        //Calling Business layer FillDropDown Method.
        //This method is present in MRFDetail BusinessLayer, from there it is called.
        raveHRCollection = mRFDetail.GetRoleDepartmentWiseBL(DepartmentId);

        //Check Collection is null or not
        if (raveHRCollection != null)
        {
            //Assign DataSOurce to Collection
            ddlRole.DataSource = raveHRCollection;

            //Assign DataText Filed to DropDown
            ddlRole.DataTextField = CommonConstants.DDL_DataTextField;

            //Assign DataValue Field to Dropdown
            ddlRole.DataValueField = CommonConstants.DDL_DataValueField;

            //Bind Dropdown
            ddlRole.DataBind();

            //Insert Select as a item for dropdown
            ddlRole.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

            raveHRCollection.Clear();
        }
    }

    /// <summary>
    /// Function will Fill Dropdowns of all the Master Data
    /// </summary>
    private void FillDropDowns()
    {

        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        //Calling Business layer FillDropDown Method
        raveHRCollection = master.FillDropDownsBLForStatus(Convert.ToInt32(Common.EnumsConstants.Category.EmployeeStatus));

        //Check Collection is null or not
        if (raveHRCollection != null)
        {
            //Assign DataSOurce to Collection
            ddlStatus.DataSource = raveHRCollection;

            //Assign DataText Filed to DropDown
            ddlStatus.DataTextField = CommonConstants.DDL_DataTextField;

            //Assign DataValue Field to Dropdown
            ddlStatus.DataValueField = CommonConstants.DDL_DataValueField;

            //Bind Dropdown
            ddlStatus.DataBind();

            //Insert Select as a item for dropdown
            ddlStatus.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

            raveHRCollection.Clear();
        }


        //Calling Fill dropdown Business layer method to fill the dropdown
        #region Modified By Mohamed Dangra
        // Mohamed : NIS-RMS : 29/12/2014 : Starts                        			  
        // Desc : Show Departement for which the person is eligible

        Common.AuthorizationManager.AuthorizationManager objAuMan = new Common.AuthorizationManager.AuthorizationManager();
        string strCurrentUser = objAuMan.getLoggedInUserEmailId();

        //raveHRCollection = master.FillDepartmentDropDownBL();
        raveHRCollection = master.FillEligibleDepartmentDropDownBL(strCurrentUser);




        //Check Collection object is null or not
        if (raveHRCollection != null)
        {
            ddlDepartment.Items.Clear();
            ddlDepartment.DataSource = raveHRCollection;
            ddlDepartment.DataTextField = CommonConstants.DDL_DataTextField;
            ddlDepartment.DataValueField = CommonConstants.DDL_DataValueField;
            ddlDepartment.DataBind();
            ////Assign DataSource to dropdown
            //ddlDepartment.DataSource = raveHRCollection;

            ////Assign DataText Field to dropdown
            //ddlDepartment.DataTextField = CommonConstants.DDL_DataTextField;

            ////Assign Data Value field to dropdown
            //ddlDepartment.DataValueField = CommonConstants.DDL_DataValueField;

            ////Bind Dropdown
            //ddlDepartment.DataBind();

            ////Insert Select as a Item for Dropdown
            ddlDepartment.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

            raveHRCollection.Clear();
        }
        // Mohamed :  : 29/12/2014 : Ends
        #endregion Modified By Mohamed Dangra
    }

    private void GetProjectName()
    {
        #region Modified By Mohamed Dangra
        // Mohamed : NIS-RMS : 29/12/2014 : Starts                        			  
        // Desc : Show Departement for which the person is eligible

        //Declaring COllection class object
        BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();

        AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
        try
        {
            string UserMailId = objRaveHRAuthorizationManager.getLoggedInUserEmailId();

            BusinessEntities.ParameterCriteria parameterCriteria = new BusinessEntities.ParameterCriteria();

            this.GetRolesforUser();
            parameterCriteria.EMailID = UserMailId;
            parameterCriteria.RoleRPM = objParameter.Role;
            //Calling Fill dropdown Business layer method to fill the dropdown
            raveHrColl = mRFDetail.GetProjectNameRoleWiseBL(parameterCriteria);

            //Check Collection object is null or not
            if (raveHrColl != null)
            {
                //Assign DataSource to dropdown
                ddlProject.DataSource = raveHrColl;

                //Assign DataText Field to dropdown
                ddlProject.DataTextField = DbTableColumn.ProjectName;

                //Assign Data Value field to dropdown
                ddlProject.DataValueField = DbTableColumn.ProjectId;

                //Bind Dropdown
                ddlProject.DataBind();

                //Insert Select as a Item for Dropdown
                ddlProject.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetProjectName", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
        }
        // Mohamed :  : 29/12/2014 : Ends
        #endregion Modified By Mohamed Dangra
    }



    private BusinessEntities.RaveHRCollection GetEmployeeSummary()
    {
        BusinessEntities.Employee employee = new BusinessEntities.Employee();
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
        Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
        try
        {
            if (grdvListofEmployees.BottomPagerRow != null)
            {
                GridViewRow gvrPager = grdvListofEmployees.BottomPagerRow;
                TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
                //performance 
                //if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0)
                if (!string.IsNullOrEmpty(txtPages.Text.ToString().Trim()) && !int.Parse(txtPages.Text).Equals(0))
                {
                    objParameter.PageNumber = Convert.ToInt32(Session[SessionNames.CURRENT_PAGE_INDEX_EMP].ToString());

                    objParameter.PageSize = Convert.ToInt32(Session[SessionNames.CURRENT_PAGE_SIZE_EMP].ToString());
                    grdvListofEmployees.PageSize = objParameter.PageSize;
                }
            }
            else
            {
                if (Session[SessionNames.CURRENT_PAGE_INDEX_EMP] != null)
                {
                    objParameter.PageNumber = Convert.ToInt32(Session[SessionNames.CURRENT_PAGE_INDEX_EMP].ToString());
                }
                else
                {
                    objParameter.PageNumber = 1;
                }

                if (Session[SessionNames.CURRENT_PAGE_SIZE_EMP] != null)
                {
                    objParameter.PageSize = Convert.ToInt32(Session[SessionNames.CURRENT_PAGE_SIZE_EMP].ToString());
                    grdvListofEmployees.PageSize = objParameter.PageSize;
                }
                else
                {
                    objParameter.PageSize = grdvListofEmployees.PageSize;
                }
            }
            //performance 
            //if (UserMailId == null || UserMailId == string.Empty)
            if (string.IsNullOrEmpty(UserMailId))
            {
                objParameter.EMailID = null;
            }
            else
            {
                objParameter.EMailID = UserMailId;

                //Issue Id : 34521 Mahendra Start
                employee.EmailId = UserMailId;
                //Issue Id : 34521 Mahendra End
            }
            //if(Session[SessionNames.DEPARTMENT_ID]
            if (Session[SessionNames.EMPLOYEEID] == null || Session[SessionNames.EMPLOYEEID].ToString() == CommonConstants.SELECT)
            {
                employee.EMPId = 0;
            }
            else
            {
                employee.EMPId = int.Parse(Session[SessionNames.EMPLOYEEID].ToString());
            }

            if (Session[SessionNames.EMP_NAME] == null || Session[SessionNames.EMP_NAME].ToString() == string.Empty)
            {
                employee.FullName = string.Empty;
            }
            else
            {
                employee.FullName = Session[SessionNames.EMP_NAME].ToString();
            }

            if (Session[SessionNames.EMP_DEPARTMENTID] == null ||
                Session[SessionNames.EMP_DEPARTMENTID].ToString() == CommonConstants.SELECT ||
                (Session[SessionNames.EMP_DEPARTMENTID].ToString() == CommonConstants.ONE.ToString()
                && (Session[SessionNames.EMP_PROJECTID].ToString() != CommonConstants.SELECT)))
            {
                employee.Department = "0";
            }
            else
            {
                if (Session[SessionNames.EMP_DEPARTMENTID].ToString() != CommonConstants.ZERO.ToString() && Session[SessionNames.EMP_PROJECTID] != CommonConstants.SELECT)
                    employee.Department = Session[SessionNames.EMP_DEPARTMENTID].ToString();
                else
                    employee.Department = "0";

            }

            if (Session[SessionNames.EMP_PROJECTID] == null || Session[SessionNames.EMP_PROJECTID].ToString() == CommonConstants.SELECT)
            {
                employee.ProjectName = "0";
            }
            else
            {
                employee.ProjectName = Session[SessionNames.EMP_PROJECTID].ToString();
            }

            if (Session[SessionNames.EMP_ROLEID] == null || Session[SessionNames.EMP_ROLEID].ToString() == CommonConstants.SELECT)
            {
                employee.DesignationId = 0;
            }
            else
            {
                employee.DesignationId = int.Parse(Session[SessionNames.EMP_ROLEID].ToString());
            }

            if (Session[SessionNames.EMP_STATUSID] == null || Session[SessionNames.EMP_STATUSID].ToString() == CommonConstants.SELECT)
            {
                employee.StatusId = (int)(MasterEnum.EmployeeStatus.Active);
            }
            else
            {
                employee.StatusId = int.Parse(Session[SessionNames.EMP_STATUSID].ToString());
            }

            employee.Role = objParameter.Role;

            //Ishwar Patil 30092014 For NIS : Start
            employee.IsRMSEmp = RBRMSEmp.SelectedValue;
            //Ishwar Patil 30092014 For NIS : End

            //Issue Id : 34521 START Changed IF condition.
            ViewState["EmployeeObject"] = employee;
            //Issue Id : 34521 END Changed IF condition.

            raveHRCollection = employeeBL.GetEmployeesSummary(objParameter, employee, ref pageCount);
            // 27633-ambar-start
            objParameter.PageSize = 10000;
            int temppagenumber = 0;
            temppagenumber = objParameter.PageNumber;
            objParameter.PageNumber = 1;
            // Mohamed : 14/01/2015 : Starts                        			  
            // Desc : Employee summary called 2 times            
            //raveHRpreviousCollection = employeeBL.GetEmployeesSummary(objParameter, employee, ref IntHashpageCount);
            raveHRpreviousCollection = raveHRCollection;
            // Mohamed : 14/01/2015 : Ends
            objParameter.PageSize = 10;
            objParameter.PageNumber = temppagenumber;
            int k = 0;
            foreach (BusinessEntities.Employee collectionprevious in raveHRpreviousCollection)
            //foreach (BusinessEntities.Employee collectionprevious in raveHRCollection)
            {
                temppreviousHashTable.Add(k, collectionprevious.EMPId.ToString());
                k++;
            }
            Session[SessionNames.EMPPREVIOUSHASHTABLE] = temppreviousHashTable;
            // 27633-ambar-End

            Session[SessionNames.PAGE_COUNT_EMP] = pageCount;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetEmployeeSummary", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

        //raveHRCollection = employeeBL.GetEmployeesSummary(objParameter,employee, ref pageCount);
        //grdvListofEmployees.DataSource = raveHRCollection;
        //grdvListofEmployees.DataBind();

        return raveHRCollection;
    }

    /// <summary>
    /// Sets the default value "Select" of all the dropdown in filter panel
    /// </summary>
    private void ClearFilteringFields()
    {
        ddlDepartment.SelectedIndex = CommonConstants.ZERO;
        ddlProject.SelectedIndex = CommonConstants.ZERO;
        ddlRole.SelectedIndex = CommonConstants.ZERO;
        ddlStatus.SelectedIndex = CommonConstants.ZERO;
        txtEployeeName.Text = string.Empty;
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

    /// <summary>
    /// Sort the gridview as per SortExpression and SortDirection
    /// </summary>
    /// <param name="sortExpression"></param>
    /// <param name="direction"></param>
    private void SortGridView(string sortExpression, string direction)
    {
        try
        {
            if (sortExpression == CommonConstants.EMP_FIRSTNAME)
            {
                objParameter.SortExpressionAndDirection = sortExpression + direction;
            }
            else
            {
                objParameter.SortExpressionAndDirection = sortExpression + direction;
            }

            raveHRCollection = GetEmployeeSummary();

            if ((int.Parse(Session[SessionNames.PAGE_COUNT_EMP].ToString()) == 1) && (raveHRCollection.Count == 1))
            {
                grdvListofEmployees.AllowSorting = false;
            }
            else
            {
                grdvListofEmployees.AllowSorting = true;
            }

            if (raveHRCollection.Count == 0)
            {
                grdvListofEmployees.DataSource = raveHRCollection;
                grdvListofEmployees.DataBind();

                ShowHeaderWhenEmptyGrid(raveHRCollection);
            }
            else
            {
                grdvListofEmployees.DataSource = raveHRCollection;
                grdvListofEmployees.DataBind();

            }

            //Finds the Paging Text Box, Previous and Next Link Buttons in Grid Views Pagning Templet
            GridViewRow gvrPager = grdvListofEmployees.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            LinkButton lbtnPrevious = (LinkButton)gvrPager.Cells[0].FindControl("lbtnPrevious");
            LinkButton lbtnNext = (LinkButton)gvrPager.Cells[0].FindControl("lbtnNext");

            if (pageCount > 1)
            {
                grdvListofEmployees.BottomPagerRow.Visible = true;
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

    /// <summary>
    /// Binds the List Of MRF Grid
    /// </summary>
    private void BindGridEmployeeSummary()
    {
        try
        {
            // By default sortDirection is Ascending
            if (GridViewSortDirection == System.Web.UI.WebControls.SortDirection.Ascending)
            {
                // Sort GridView in Ascending direction
                SortGridView(sortExpression, ASCENDING);
            }
            else
            {
                // Sort GridView in Descending direction
                SortGridView(sortExpression, DESCENDING);
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "BindGridEmployeeSummary", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    private void ShowHeaderWhenEmptyGrid(BusinessEntities.RaveHRCollection raveHRCollection)
    {
        try
        {
            //set header visible
            grdvListofEmployees.ShowHeader = true;
            // Disable sorting
            grdvListofEmployees.AllowSorting = false;

            //Create empty datasource for Grid view and bind
            raveHRCollection.Add(new BusinessEntities.Employee());
            grdvListofEmployees.DataSource = raveHRCollection;
            grdvListofEmployees.DataBind();

            //clear all the cells in the row
            grdvListofEmployees.Rows[0].Cells.Clear();

            //add a new blank cell
            grdvListofEmployees.Rows[0].Cells.Add(new TableCell());
            grdvListofEmployees.Rows[0].Cells[0].Text = CommonConstants.NO_RECORDS_FOUND_MESSAGE;
            grdvListofEmployees.Rows[0].Cells[0].Wrap = false;
            grdvListofEmployees.Rows[0].Cells[0].Width = Unit.Percentage(10);

        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ShowHeaderWhenEmptyGrid", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
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
                    case CommonConstants.EMP_CODE:
                        headerRow.Cells[1].Controls.Add(sortImage);
                        break;
                    case CommonConstants.EMP_FIRSTNAME:
                        headerRow.Cells[2].Controls.Add(sortImage);
                        break;
                    case CommonConstants.EMP_DESIGNATION:
                        headerRow.Cells[3].Controls.Add(sortImage);
                        break;
                    case CommonConstants.EMP_JOINING_DATE:
                        headerRow.Cells[4].Controls.Add(sortImage);
                        break;
                    case CommonConstants.EMP_DEPTNAME:
                        headerRow.Cells[5].Controls.Add(sortImage);
                        break;
                }
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "AddSortImage", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    private void GetEmployeeDesignations(int categoryID)
    {
        try
        {
            Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
            BusinessEntities.RaveHRCollection raveHrColl = new BusinessEntities.RaveHRCollection();
            Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();

            //if (categoryID != (int)EnumsConstants.Category.ProjectRole)
            //{
            //    raveHrColl = master.FillDropDownsBL(categoryID);
            //}
            //else
            //{
            raveHrColl = employeeBL.GetEmployeesDesignations(categoryID);
            //}

            ddlRole.Items.Clear();
            ddlRole.DataSource = raveHrColl;
            ddlRole.DataTextField = Common.CommonConstants.DDL_DataTextField;
            ddlRole.DataValueField = Common.CommonConstants.DDL_DataValueField;
            ddlRole.DataBind();
            ddlRole.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetEmployeeDesignations", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// Based on the Department selected, fill the Role 
    /// </summary>
    private void FillRoleDropdownAsPerDepartment()
    {
        //if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Projects))
        //{
        //    GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.ProjectRole));
        //}
        //if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Admin))
        //{
        //    GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.AdminRole));
        //}
        //if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Finance))
        //{
        //    GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.FinanceRole));
        //}
        //if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.HR))
        //{
        //    GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.HRRole));
        //}
        //if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.ITS))
        //{
        //    GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.ITSRole));
        //}
        //if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Marketing))
        //{
        //    GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.MarketingRole));
        //}
        //if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.PMOQuality))
        //{
        //    GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.PMOQualityRole));
        //}
        //if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.PreSales))
        //{
        //    GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.PreSalesRole));
        //}
        //if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.RaveDevelopment))
        //{
        //    GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.RaveDevelopmentRole));
        //}
        //if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Support))
        //{
        //    ddlRole.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
        //}
        //if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Testing))
        //{
        //    GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.TestingRole));
        //}
        //if (int.Parse(ddlDepartment.SelectedValue) == Convert.ToInt32(MasterEnum.Departments.Recruitment))
        //{
        //    GetEmployeeDesignations(Convert.ToInt32(EnumsConstants.Category.RecruitmentRole));
        //}
        ddlRole.Enabled = true;

        if (int.Parse(ddlDepartment.SelectedValue) != Convert.ToInt32(MasterEnum.Departments.Projects))
        {
            GetEmployeeDesignations(int.Parse(ddlDepartment.SelectedValue));
            ddlProject.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
            ddlProject.Enabled = false;
        }
        else
        {
            GetEmployeeDesignations(int.Parse(ddlDepartment.SelectedValue));
            ddlProject.Enabled = true;
            this.GetProjectName();
        }
    }

    protected void btnEmployeeDetails_Click(object sender, EventArgs e)
    {
        try
        {
            Rave.HR.BusinessLayer.Employee.Employee objBLEmployee = new Rave.HR.BusinessLayer.Employee.Employee();
            //Issue Id : 34521 START Changed IF condition. commented previous code
            BusinessEntities.Employee employee = (BusinessEntities.Employee)ViewState["EmployeeObject"];
            //objBLEmployee.EmployeeDetailsInExcel();
            objBLEmployee.EmployeeDetailsInExcel(employee);

            //Issue Id : 34521 END Changed IF condition.
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnEmployeeDetails_Click", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    #endregion Private Methods

    /* going google migration 
    public string GetDomainUsers(string strUserName)
    {
        //strUserName = strUserName.Replace("@rave-tech.co.in", "");
        //Google change point to northgate
        if (strUserName.ToLower().Contains("@rave-tech.co.in"))
        {
            strUserName = strUserName.Replace("@rave-tech.co.in", "");
        }
        else
        {
            strUserName = strUserName.Replace(AuthorizationManagerConstants.NORTHGATEDOMAIN, "");
        }


        string strUserEmail = string.Empty;
        string ou1 = string.Empty;
        string ou2 = string.Empty;
        string ou3 = string.Empty;
        int a = 1;

        DirectoryEntry searchRoot = new DirectoryEntry("LDAP://RAVE-TECH.CO.IN");

        DirectorySearcher search = new DirectorySearcher(searchRoot);

        //string query = "(|(objectCategory=group )(objectCategory=user))";
        //string query = "(SAMAccountName=" + strUserName + ")"; 
        string query = "(&(objectClass=user)(objectCategory=Person))";

        search.Filter = query;

        SearchResult result;

        SearchResultCollection resultCol = search.FindAll();

        if (resultCol != null)
        {

            for (int counter = 0; counter < resultCol.Count; counter++)
            {

                result = resultCol[counter];

                //if (result.Properties.Contains("samaccountname"))
                if (result.Properties.Contains("showinaddressbook"))
                {

                    //if (!result.Properties.Contains("proxyaddresses"))
                    //{
                    //strUserEmail = result.Properties["mail"][0].ToString();

                    //result = resultCol[counter];
                    //result.Properties["givenName"][0];
                    //result.Properties["initials"][0];
                    //strUserEmail = result.Properties["CN"][0].ToString();
                    //strUserEmail = result.Properties["samaccountname"][0].ToString();

                    strUserEmail = result.Properties["mail"][0].ToString();


                    ou1 = result.Properties["displayname"][0].ToString();
                    //ou2 = result.Properties["OU"][0].ToString();
                    //ou3 = result.Properties["OU"][0].ToString();
                    Response.Write(a.ToString() + "###" + strUserEmail + "-----" + ou1 + "-----" + ou2 + "-----" + ou3 + "<br/>");
                    a = a + 1;
                    //}
                }

            }

        }

        return strUserEmail;
    }
    */

    protected void grdvDetailedListofEmployees_onRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //To solved the issue id 20769
            //Start
            //Declare the string parameter to 
            //get the project name and client name
            string ProjectName;
            string ClientName;
            string qString1;
            string qString2;
            string qString3;
            string queryString;
            string EmpProjectAllocationId;
            string qString4;
            //
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgRelease = (ImageButton)e.Row.FindControl(RELEASEBUTTONID);

                //Ishwar NIS 31102014 Start
                //if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEPM) || arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM))
                if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM))
                //Ishwar NIS 31102014 End
                {
                    imgRelease.Visible = true;
                }
                else
                {
                    imgRelease.Visible = false;
                }

                //To solved the issue id 20769
                //Start

                ProjectName = (((BusinessEntities.Employee)(e.Row.DataItem))).ProjectName.ToString();
                if (ProjectName.ToString().Length != 0)
                    ClientName = string.Empty;
                else
                    ClientName = (((BusinessEntities.Employee)(e.Row.DataItem))).ClientName.ToString();

                EmpProjectAllocationId = (((BusinessEntities.Employee)(e.Row.DataItem))).EmpProjectAllocationId.ToString();

                qString1 = URLHelper.SecureParameters(QueryStringConstants.PROJECTNAME, ProjectName);
                qString2 = URLHelper.SecureParameters(QueryStringConstants.CLIENTNAME, ClientName);
                qString3 = URLHelper.SecureParameters(QueryStringConstants.EMPID, imgRelease.CommandArgument.ToString());
                qString4 = URLHelper.SecureParameters(QueryStringConstants.EPAID, EmpProjectAllocationId);
                string signature = URLHelper.CreateSignature(ProjectName, ClientName, imgRelease.CommandArgument.ToString(), EmpProjectAllocationId);

                queryString = qString1 + "&" + qString2 + "&" + qString3 + "&" + qString4 + "&" + signature;
                imgRelease.Attributes["onclick"] = "javascript:popUpEmployeeReleasePopulate('" + queryString + "' )";
                //End

                //START
                //Coded by Rahul P
                //26 May 10
                //Issue Id 18601
                Session[SessionNames.REFRESHPAGE] = Session[SessionNames.CURRENT_PAGE_INDEX_EMP].ToString();
                //END
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvDetailedListofEmployees_onRowDataBound", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            grdvListofEmployees.PageSize = int.Parse(((DropDownList)sender).SelectedValue);
            Session[SessionNames.CURRENT_PAGE_SIZE_EMP] = grdvListofEmployees.PageSize.ToString();
            this.BindGridEmployeeSummary();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ddlPageSize_SelectedIndexChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }
}
