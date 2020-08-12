using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using Common.AuthorizationManager;
using System.Text;
using System.Data.SqlClient;

public partial class EmpSetReleaseDate : BaseClass
{
    #region Members Variables

    // BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
    Rave.HR.BusinessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.BusinessLayer.MRF.MRFDetail();

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

    private const string CLASS_NAME = "EmpSetReleaseDate.aspx.cs";

    /// <summary>
    /// Rave Email domain
    /// </summary>
    /// 
    //Googleconfigurable
    //private const string RAVE_DOMAIN = "@rave-tech.com";

    static string EmailId = string.Empty;

    char[] SPILITER_SLASH = { '/' };

    int Rowindex;

    string UserRaveDomainId;
    string UserMailId;

    #region Function Names

    const string FunctionPage_Load = "Page_Load";

    const string FunctiongrdvListofEmployees_Sorting = "grdvListofEmployees_Sorting";

    const string FunctiongrdvListofEmployees_RowCreated = "grdvListofEmployees_RowCreated";

    const string FunctionbtnCancel_Click = "btnCancel_Click";

    const string FunctionbtnSave_Click = "btnSave_Click";

    const string FunctiontxtPages_TextChanged = "txtPages_TextChanged";

    const string FunctiongrdvListofEmployees_PageIndexChanging = "grdvListofEmployees_PageIndexChanging";

    const string FunctiongrdvListofEmployees_RowDataBound = "grdvListofEmployees_RowDataBound";

    const string FunctiongrdvListofEmployees_DataBound = "grdvListofEmployees_DataBound";

    #endregion


    #region ViewState Constants

    private string EMPDETAILS = "EmpDetails";
    private string ROWINDEX = "RowIndex";

    #endregion ViewState Constants

    #endregion Members Variables

    #region Local Properties

    /// <summary>
    /// Gets or sets the rave HR collection.
    /// </summary>
    /// <value>The rave HR collection.</value>
    private BusinessEntities.RaveHRCollection raveHRCollection
    {
        get
        {
            if (ViewState[EMPDETAILS] != null)
                return ((BusinessEntities.RaveHRCollection)ViewState[EMPDETAILS]);
            else
                return null;
        }
        set
        {
            ViewState[EMPDETAILS] = value;
        }
    }

    #endregion Local Properties

    #region Protected Events

    /// <summary>
    /// Handles the Load event of the Page control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        //31579 Abhishek start
        this.Header.DataBind();
        //31579 end

        //btnSave.Attributes.Add(CommonConstants.EVENT_ONCLICK, "javascript:return ValidateAll();");
        btnSave.OnClientClick = "if(ValidateAll()){" + ClientScript.GetPostBackEventReference(btnSave, null) + "}";

        //TODO: Need to remove comments and validate URL
        if (!ValidateURL())
        {
            Response.Redirect(CommonConstants.INVALIDURL, false);
        }
        else
        {
            //30163-Subhra-Start
            hfSystemDate.Value = System.DateTime.Now.ToString();
            //30163-Subhra-end

            //To solved the issue id 20769,22011
            //Start
            //ucDatePickerBillUtilDate.TextBox.Attributes.Add("onblur", "return SaveClickValidate(); ");
            //End

            txtReportingTo.Attributes.Add("readonly", "readonly");
            //Start//Ishwar-49082
            ucDatePickerReleaseDate.Attributes.Add("readonly", "readonly");
            if (!chkIsReleased.Checked)
                ucDatePickerReleaseDate.IsEnable = false;
            //End//Ishwar-49082

            this.GetRolesforUser();

            // Get logged-in user's email id
            AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
            UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
            UserMailId = UserRaveDomainId.Replace("co.in", "com");

            //31579 Abhishek start
            imgReportingToPopulate.Attributes.Add("onclick", "return popUpEmployeeSearch();");
            //end

            if (!Page.IsPostBack)
            {
                // venkatesh  : Issue  : 6/12/2013 : Starts                 
                // Desc : maintain Employee summary paging state after release
                //Session[SessionNames.CURRENT_PAGE_INDEX_EMP] = 1;
                // venkatesh  : Issue  : 6/12/2013 : End

                // Setting the Default sortExpression as EmpCode
                if (Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] == null)
                {
                    sortExpression = CommonConstants.EMP_CODE;
                    Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP] = sortExpression;
                }
                else
                {
                    sortExpression = Session[SessionNames.PREVIOUS_SORT_EXPRESSION_EMP].ToString();
                }

                this.BindGridEmployeeSummary();
                //To solved the issue id 20769
                //Start
                this.BindControlData();
                this.BindGridEmpProjectAllocation();
                //End


                //4C Bypass validation for only RM Group
                AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
                ArrayList arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());

                if (arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM))
                {
                    Chk4CByPassValidation.Visible = true;
                    lblByPass4CValidation.Visible = true;
                }
                else
                {
                    Chk4CByPassValidation.Visible = false;
                    lblByPass4CValidation.Visible = false;
                }

            }
        }
    }

    //protected void Page_UnLoad(object sender, EventArgs e)
    //{
    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "close", "<script language=javascript>window.opener.location.reload(true);</script>");
    //}



    /// <summary>
    /// Handles the Sorting event of the grdvListofEmployees control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewSortEventArgs"/> instance containing the event data.</param>
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

            if (Session[SessionNames.EMP_PROJECTID] != null || Session[SessionNames.EMP_DEPARTMENTID] != null || Session[SessionNames.EMP_ROLE] != null || Session[SessionNames.EMP_STATUSID] != null)
            {
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctiongrdvListofEmployees_Sorting, EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the RowCreated event of the grdvListofEmployees control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
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
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctiongrdvListofEmployees_RowCreated, EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the Click event of the btnCancel control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// Ishwar : Modal PopUp Issue 13012015 Start
    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //Response.Write("<script language='javascript'>window.close();</script>");
    //}
    //Ishwar : Modal PopUp Issue 13012015 End

    /// <summary>
    /// Handles the Click event of the btnSave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void btnSave_Click(object sender, EventArgs e)
    {

        //To solved issue id 22011
        //Start 
        if (true)
            //End 
            this.UpdateEmployeeReleaseDate();
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctiontxtPages_TextChanged, EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    //For Util and Billing report
    protected void txtBilling_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ucDatePickerBillDate.Text = string.Empty;
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "txtBilling_TextChanged", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    //For Util and Billing report
    protected void txtUtilization_TextChanged(object sender, EventArgs e)
    {
        try
        {
            ucDatePickerBillUtilDate.Text = string.Empty;
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "txtBilling_TextChanged", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }


    /// <summary>
    /// Handles the PageIndexChanging event of the grdvListofEmployees control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewPageEventArgs"/> instance containing the event data.</param>
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctiongrdvListofEmployees_PageIndexChanging, EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Handles the RowDataBound event of the grdvListofEmployees control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.Web.UI.WebControls.GridViewRowEventArgs"/> instance containing the event data.</param>
    protected void grdvListofEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //     string fullname=string.Empty;
        //     string Index = string.Empty;
        //    e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
        //    e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";

        //    if (e.Row.Cells[3].Text.Contains("'"))
        //    {
        //        fullname = e.Row.Cells[3].Text.Replace("'",string.Empty);
        //        e.Row.Attributes["onclick"] = "javascript:PopulateData('" + DataBinder.Eval(e.Row.DataItem, "EmpProjectAllocationId") +
        //                                                            "','" + DataBinder.Eval(e.Row.DataItem, "ProjectReleaseDate", "{0:dd/MM/yyyy}") +
        //                                                            "','" + DataBinder.Eval(e.Row.DataItem, "ReportingTo") +
        //                                                            "','" + DataBinder.Eval(e.Row.DataItem, "Billing") +
        //                                                            "','" + DataBinder.Eval(e.Row.DataItem, "Utilization") +
        //                                                            "','" + DataBinder.Eval(e.Row.DataItem, "ProjectName") +
        //                                                            "','" + fullname +
        //                                                            "','" + DataBinder.Eval(e.Row.DataItem, "ProjectEndDate", "{0:dd/MM/yyyy}") +
        //                                                            "','" + DataBinder.Eval(e.Row.DataItem, "ProjectId") +
        //                                                            "','" + DataBinder.Eval(e.Row.DataItem, "ClientName") +
        //                                                            "','" + e.Row.RowIndex + "')";
        //    }
        //    else
        //    {
        //        e.Row.Attributes["onclick"] = "javascript:PopulateData('" + DataBinder.Eval(e.Row.DataItem, "EmpProjectAllocationId") +
        //                                                            "','" + DataBinder.Eval(e.Row.DataItem, "ProjectReleaseDate", "{0:dd/MM/yyyy}") +
        //                                                            "','" + DataBinder.Eval(e.Row.DataItem, "ReportingTo") +
        //                                                            "','" + DataBinder.Eval(e.Row.DataItem, "Billing") +
        //                                                            "','" + DataBinder.Eval(e.Row.DataItem, "Utilization") +
        //                                                            "','" + DataBinder.Eval(e.Row.DataItem, "ProjectName") +
        //                                                            "','" + DataBinder.Eval(e.Row.DataItem, "FullName") +
        //                                                            "','" + DataBinder.Eval(e.Row.DataItem, "ProjectEndDate", "{0:dd/MM/yyyy}") +
        //                                                            "','" + DataBinder.Eval(e.Row.DataItem, "ProjectId") +
        //                                                             "','" + DataBinder.Eval(e.Row.DataItem, "ClientName") +
        //                                                            "','" + e.Row.RowIndex +  "')";
        //    }

        //}

    }

    /// <summary>
    /// Handles the DataBound event of the grdvListofEmployees control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, FunctiongrdvListofEmployees_DataBound, EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    #endregion Protected Events

    #region Private Methods

    /// <summary>
    /// Gets the employee summary.
    /// </summary>
    /// <returns></returns>
    private BusinessEntities.RaveHRCollection GetEmployeeSummary()
    {
        BusinessEntities.Employee employee = new BusinessEntities.Employee();
        Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
        try
        {
            if (grdvListofEmployees.BottomPagerRow != null)
            {
                GridViewRow gvrPager = grdvListofEmployees.BottomPagerRow;
                TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
                if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0)
                {
                    objParameter.PageNumber = int.Parse(txtPages.Text);
                    objParameter.PageSize = 10;
                }
            }
            else
            {
                objParameter.PageNumber = 1;
                objParameter.PageSize = 10;
            }
            //To solved the issue id 20769
            //Start
            //Change the parameter name from field to EmpId
            if ((Session[SessionNames.EMPLOYEEID] == null || Session[SessionNames.EMPLOYEEID].ToString() == CommonConstants.SELECT) && Request.QueryString["EMPId"] == null)
            //End
            {
                employee.EMPId = 0;
            }
            else
            {
                //To solved the issue id 20769
                //Start
                //employee.EMPId = int.Parse(DecryptQueryString("field").ToString());
                //hfEmpID.Value = DecryptQueryString("field").ToString();
                employee.EMPId = int.Parse(DecryptQueryString("EMPId").ToString());
                hfEmpID.Value = DecryptQueryString("EMPId").ToString();
                //End


            }
            if (Session[SessionNames.EMP_STATUSID] == null || Session[SessionNames.EMP_STATUSID].ToString() == CommonConstants.SELECT)
            {
                employee.StatusId = (int)MasterEnum.EmployeeStatus.Active;
            }
            else
            {
                employee.StatusId = int.Parse(Session[SessionNames.EMP_STATUSID].ToString());
            }

            //To solved the issue id 20769
            //Start
            //to get the value of project name and client name 
            //from query string
            if (Request.QueryString["ProjectName"] == null)
            {
                employee.ProjectName = string.Empty;
            }
            else
            {
                //employee.ProjectName = DecryptQueryString("prjname").ToString();
                employee.ProjectName = DecryptQueryString("ProjectName").ToString();
            }

            if (Request.QueryString["clientname"] == null)
            {
                employee.ClientName = string.Empty;
            }
            else
            {
                //employee.ClientName = DecryptQueryString("cltname").ToString();
                employee.ClientName = DecryptQueryString("clientname").ToString();
            }
            //End

            if (Request.QueryString["EPAID"] == null || int.Parse(DecryptQueryString("EPAID").ToString()) == CommonConstants.ZERO)
            {
                employee.EmpProjectAllocationId = CommonConstants.ZERO;
            }
            else
            {
                employee.EmpProjectAllocationId = Convert.ToInt32(DecryptQueryString("EPAID"));
            }

            raveHRCollection = employeeBL.GetEmployeesReleaseStatus(objParameter, employee, ref pageCount);

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

        return raveHRCollection;
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
                    objParameter.RoleRPM = AuthorizationManagerConstants.ROLERPM;

                    break;

                case AuthorizationManagerConstants.ROLEPROJECTMANAGER:
                    objParameter.RolePM = AuthorizationManagerConstants.ROLEPROJECTMANAGER;

                    break;

                case AuthorizationManagerConstants.ROLEHR:
                    objParameter.RoleHR = AuthorizationManagerConstants.ROLEHR;
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

        if (rolesForUser.Count == 1)
        {
            if (rolesForUser.Contains(AuthorizationManagerConstants.ROLEPM))
            {
                txtBilling.Attributes.Add("readonly", "readonly");
                txtUtilization.Attributes.Add("readonly", "readonly");
            }
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
            if (sortExpression == CommonConstants.EMP_CODE)
            {
                objParameter.SortExpressionAndDirection = sortExpression + direction;
            }
            else
            {
                objParameter.SortExpressionAndDirection = sortExpression + direction;
            }

            raveHRCollection = GetEmployeeSummary();

            foreach (BusinessEntities.Employee emp in raveHRCollection)
            {
                EmailId = emp.EmailId;
            }

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
    /// Binds the data of raveHRCollection to the Form controls
    /// </summary>

    private void BindControlData()
    {
        if (raveHRCollection != null)
        {
            foreach (BusinessEntities.Employee emp in raveHRCollection)
            {
                ucDatePickerReleaseDate.Text = emp.ProjectReleaseDate.ToString("dd/MM/yyyy");
                if (emp.ReportingTo != null)
                    txtReportingTo.Text = emp.ReportingTo.ToString();
                else
                    txtReportingTo.Text = string.Empty;
                txtBilling.Text = emp.Billing.ToString();
                txtUtilization.Text = emp.Utilization.ToString();
                if (emp.UtilizationAndBilling == null || emp.UtilizationAndBilling == DateTime.MinValue)
                    ucDatePickerBillUtilDate.Text = string.Empty;
                else
                    ucDatePickerBillUtilDate.Text = emp.UtilizationAndBilling.ToString("dd/MM/yyyy");

                //To solved issue id 22011
                //Start
                if (emp.BillingChangeDate == null || emp.BillingChangeDate == DateTime.MinValue)
                    ucDatePickerBillDate.Text = string.Empty;
                else
                    ucDatePickerBillDate.Text = emp.BillingChangeDate.ToString("dd/MM/yyyy");

                if (emp.ResourceBillingDate == null || emp.ResourceBillingDate == DateTime.MinValue)
                    ucDatePickerMrfBillingDate.Text = string.Empty;
                else
                    ucDatePickerMrfBillingDate.Text = emp.ResourceBillingDate.ToString("dd/MM/yyyy");
                //End

                //issue no: 31579 /Abhishek 
                //start
                hidReportingTo.Value = emp.ReportingToId;
                hidReportingToOld.Value = emp.ReportingToId;
                //end
            }
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
    //To solved the issue id 20769
    //Start
    //Bing the Resource Plan data to data grid
    private void BindGridEmpProjectAllocation()
    {
        try
        {

            BusinessEntities.RaveHRCollection raveHRColl = new BusinessEntities.RaveHRCollection();

            Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();

            foreach (BusinessEntities.Employee emp in raveHRCollection)
            {
                if (emp.ProjectId.ToString() != "0")
                {
                    raveHRColl = employeeBL.GetEmployeesResourcePlan(emp);

                    grdvListofEmpProject.DataSource = raveHRColl;
                    //grdvListofEmpProject.DataSource = ((BusinessEntities.ResourcePlanDetail)(employeeBL.GetEmployeesResourcePlan(emp).InnerList).Items[0]);
                    grdvListofEmpProject.DataBind();
                    lblResoursePlan.Visible = true;
                }
                else
                {
                    lblResoursePlan.Visible = false;
                }
                hfEmpProjectAllocationId.Value = emp.EmpProjectAllocationId.ToString();
                if (emp.ProjectName != null)
                    hfProjectName.Value = emp.ProjectName.ToString();
                else
                    hfProjectName.Value = string.Empty;
                hfProjectId.Value = emp.ProjectId.ToString();
                if (emp.ClientName != null)
                    hfClientName.Value = emp.ClientName.ToString();
                else
                    hfClientName.Value = string.Empty;

            }
            //grdvListofEmployees.Visible = false;

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
    //End

    /// <summary>
    /// Shows the header when empty grid.
    /// </summary>
    /// <param name="raveHRCollection">The rave HR collection.</param>
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
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ShowHeaderWhenEmptyGrid", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Gets the user and roles.
    /// </summary>
    /// <returns></returns>
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
                        headerRow.Cells[2].Controls.Add(sortImage);
                        break;

                    case CommonConstants.EMP_FIRSTNAME:
                        headerRow.Cells[3].Controls.Add(sortImage);
                        break;

                    case CommonConstants.EMP_CLIENTNAME:
                        headerRow.Cells[4].Controls.Add(sortImage);
                        break;

                    case CommonConstants.EMP_PROJECTNAME:
                        headerRow.Cells[5].Controls.Add(sortImage);
                        break;

                    case CommonConstants.EMP_ROLE:
                        headerRow.Cells[6].Controls.Add(sortImage);
                        break;

                    case CommonConstants.EMP_ENDDATE:
                        headerRow.Cells[7].Controls.Add(sortImage);
                        break;

                    case CommonConstants.EMP_UTILIZATION:
                        headerRow.Cells[8].Controls.Add(sortImage);
                        break;

                    case CommonConstants.EMP_BILLING:
                        headerRow.Cells[9].Controls.Add(sortImage);
                        break;

                    case CommonConstants.EMP_DEPTNAME:
                        headerRow.Cells[10].Controls.Add(sortImage);
                        break;

                    case CommonConstants.EMP_REPORTINGTO:
                        headerRow.Cells[11].Controls.Add(sortImage);
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

            switch (e.CommandName)
            {
                //Previous button is clicked
                case PREVIOUS:
                    Session[SessionNames.CURRENT_PAGE_INDEX_EMP] = Convert.ToInt32(txtPages.Text) - 1;
                    txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) - 1);
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
    /// Updates the employee release date.
    /// </summary>
    private void UpdateEmployeeReleaseDate()
    {
        try
        {
            Rave.HR.BusinessLayer.Employee.Employee objEmployeeBAL = new Rave.HR.BusinessLayer.Employee.Employee();
            if (grdvListofEmployees.Rows[0].Cells[0].Text == CommonConstants.NO_RECORDS_FOUND_MESSAGE)
            {
                raveHRCollection.Clear();
                ShowHeaderWhenEmptyGrid(raveHRCollection);
            }

            if (hfEmpID.Value != string.Empty && hfEmpProjectAllocationId.Value != string.Empty)
            {
                // CR - 25715 issue related to ePlatform MRF Sachin
                int NoofEmployeeinPR = 0;
                int NoofMRFOpen = 0;
                objEmployeeBAL.CheckLastEmployeeRelease(Convert.ToInt32(hfProjectId.Value.Trim()), ref NoofEmployeeinPR, ref NoofMRFOpen);

                if (NoofEmployeeinPR == 1)
                {
                    if (NoofMRFOpen > 0)
                    {
                        lblError.Text = "Please close open MRF of this project in order to release the last resource";
                        return;
                    }
                }
                // End CR - 25715 Sachin issue related to ePlatform MRF

                BusinessEntities.Employee employee = new BusinessEntities.Employee();
                BusinessEntities.Employee Emp = new BusinessEntities.Employee();
                BusinessEntities.MRFDetail Mrf = new BusinessEntities.MRFDetail();
                BusinessEntities.RaveHRCollection empcollection = new BusinessEntities.RaveHRCollection();

                Boolean Flag = false;
                Boolean IsProjectClosed = false;
                //Comment by Rahul 
                //to solved issue id 20769
                //Rowindex = Convert.ToInt32(hfRowindex.Value);
                Rowindex = Convert.ToInt32("1");

                Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();
                employee.EMPId = hfEmpID.Value == string.Empty ? 0 : int.Parse(hfEmpID.Value.Trim());
                employee = objEmployeeBAL.GetEmployee(employee);
                employee.EmpProjectAllocationId = Convert.ToInt32(hfEmpProjectAllocationId.Value.ToString());
                employee.Billing = Convert.ToInt32(txtBilling.Text.Trim());
                employee.Utilization = Convert.ToInt32(txtUtilization.Text.Trim());
                employee.ProjectReleaseDate = Convert.ToDateTime(ucDatePickerReleaseDate.Text.Trim());
                employee.ProjectName = hfProjectName.Value.Trim();
                employee.ReportingTo = txtReportingTo.Text.Trim();
                //start 31579 Abhishek
                employee.ReportingToId = hidReportingTo.Value;
                //end 31579 Abhishek
                employee.ProjectId = Convert.ToInt32(hfProjectId.Value.Trim());
                employee.ReasonForExtension = txtReasonforExtension.Text.Trim();
                employee.LastModifiedByMailId = UserMailId;
                employee.FullName = Convert.ToString(grdvListofEmployees.Rows[0].Cells[3].Text);
                employee.ClientName = Convert.ToString(hfClientName.Value);
                //To solved the issue id 20769
                //Start
                if (ucDatePickerBillUtilDate.Text != "")
                    employee.UtilizationAndBilling = Convert.ToDateTime(ucDatePickerBillUtilDate.Text.Trim());
                else
                    employee.UtilizationAndBilling = DateTime.MinValue;
                //End

                if (ucDatePickerBillDate.Text != "")
                    employee.BillingChangeDate = Convert.ToDateTime(ucDatePickerBillDate.Text.Trim());
                else
                    employee.BillingChangeDate = DateTime.MinValue;

                if (ucDatePickerMrfBillingDate.Text != "")
                    employee.ResourceBillingDate = Convert.ToDateTime(ucDatePickerMrfBillingDate.Text.Trim());
                else
                    employee.ResourceBillingDate = DateTime.MinValue;


                if (chkIsReleased.Checked)

                    employee.EmpReleasedStatus = 1;
                else
                    employee.EmpReleasedStatus = 0;
                //To solved the issue id 20769
                //Start 
                //Modify the below prevoiuslu the selected row was passing to 
                //the raveHRCollection
                Emp = (BusinessEntities.Employee)raveHRCollection.Item(0);
                //end
                employee.Role = Emp.Role;
                employee.Department = Emp.Department;
                employee.ProjectId = Convert.ToInt32(hfProjectId.Value.Trim());
                employee.ProjectCode = Emp.ProjectCode;

                if ((Emp.ProjectReleaseDate != employee.ProjectReleaseDate && employee.ProjectId == 0 && employee.EmpReleasedStatus != 1)
                    || Emp.Billing != employee.Billing || Emp.Utilization != employee.Utilization
                    //|| (Emp.ResourceBillingDate != employee.ResourceBillingDate && employee.ResourceBillingDate != DateTime.MaxValue)
                    )
                {
                    Flag = true;
                }

                if (Flag == true || employee.EmpReleasedStatus == 1 || hidReportingToOld.Value != hidReportingTo.Value)//31579 - added condition
                {

                    if (employee.EmpReleasedStatus == 1 && Chk4CByPassValidation.Checked == false)
                    {
                        Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
                        bool flag = false;
                        flag = fourCBAL.Check4CRatingFillForAll(employee.EMPId, Convert.ToDateTime(ucDatePickerReleaseDate.Text), Convert.ToInt32(hfProjectId.Value.Trim()));

                        if (flag)
                        {
                            lblError.Text = "4C rating incomplete, Please check the 4C rating details.";
                            return;
                        }
                    }

                    //Issue ID : 42856 Mahendra 24-06-2013 Start
                    if ((Emp.Billing != employee.Billing) && (Convert.ToInt32(txtBilling.Text.Trim()) > 0 && string.IsNullOrEmpty(ucDatePickerBillDate.Text)))
                    {
                        lblError.Text = "Please enter billing date.";
                        return;
                    }
                    if ((Emp.Utilization != employee.Utilization) && (Convert.ToInt32(txtUtilization.Text.Trim()) > 0 && string.IsNullOrEmpty(ucDatePickerBillUtilDate.Text)))
                    {
                        lblError.Text = "Please enter utilization date.";
                        return;
                    }
                    //Issue ID : 42856 Mahendra 24-06-2013 End

                    employeeBL.UpdateEmployeeReleaseStatus(employee, ref IsProjectClosed);

                    //Siddharth 7 April 2015 Start
                    //Logic:- If Employee Release check Box is checked then only Update Cost Code else ignore.
                    if (chkIsReleased.Checked == true)
                    {
                        employeeBL.Employee_UpdateEmpCostCodeProjRelease(employee);
                    }
                    //Siddharth 7 April 2015 End


                    //start 31579 Abhishek 
                    if (hidReportingToOld.Value != hidReportingTo.Value)
                    {
                        BusinessEntities.Employee employeeUpdateAccountableTo = new BusinessEntities.Employee();
                        employeeUpdateAccountableTo = this.GetEmployee(employee);
                        string reportingToIdOld = employeeUpdateAccountableTo.ReportingToId;

                        Rave.HR.BusinessLayer.MRF.MRFDetail MRFBL = new Rave.HR.BusinessLayer.MRF.MRFDetail();
                        Mrf.EmployeeId = employee.EMPId;

                        if (reportingToIdOld.Contains(hidReportingToOld.Value))
                        {
                            reportingToIdOld = reportingToIdOld.Replace(hidReportingToOld.Value, hidReportingTo.Value);
                        }
                        else if (reportingToIdOld.Contains(","))
                        {
                            reportingToIdOld += "," + hidReportingTo.Value;
                        }
                        else
                        {
                            reportingToIdOld = hidReportingTo.Value;
                        }

                        MRFBL.UpdateReportingTOForEmployee(Mrf, reportingToIdOld);
                        MRFBL = null;
                    }
                    //end 31579 
                }

                if (Flag)
                {
                    //To solved the issue id 20769
                    //Start
                    //if billing and utilization is modified then release date become billutilization modified date. 
                    //employee.ProjectReleaseDate = Emp.ProjectReleaseDate;
                    //--Call the BL method to send Emails for projectallocation updation.
                    objEmployeeBAL.SendMailEmployeeProjectAllocationUpdation(employee);
                    //End
                }

                if (employee.EmpReleasedStatus == 1)
                {
                    Boolean IsRoleHR = false;

                    //--Call the BL method to send Emails.
                    // 34967-Ambar-22062012-Changed If condition to check project and department
                    //if (emp.ProjectId.ToString() != "0")
                    //if (employee.Department == MasterEnum.Departments.Projects.ToString())
                    if (employee.ProjectId.ToString() != "0")
                    {
                        objEmployeeBAL.SendMailToEmployeeReleasedFromProject(employee, IsRoleHR);
                    }
                    else
                    {
                        objEmployeeBAL.SendMailToEmployeeReleasedFromDepartment(employee, IsRoleHR);
                    }

                    Mrf.ProjectId = Convert.ToInt32(hfProjectId.Value.Trim());


                    // Initialise the Data Layer object
                    Rave.HR.BusinessLayer.MRF.MRFDetail MRFBL = new Rave.HR.BusinessLayer.MRF.MRFDetail();
                    Mrf.EmployeeId = hfEmpID.Value == string.Empty ? 0 : int.Parse(hfEmpID.Value.Trim());
                    string Managerid = string.Empty;
                    string strReporingToEmailIds = string.Empty;
                    string ReporingToIds = string.Empty;

                    DataSet dsFromEmployee = MRFBL.GetReportingTOFromEmployee(Mrf);

                    // 35091-Ambar-Start-27062012
                    // Get Reporting person from allocation details instead of Project manager
                    //strReporingToEmailIds = employeeBL.GetProjectManagersEmailId(employee, ref ReporingToIds);
                    strReporingToEmailIds = employeeBL.GetAllocationReportingToEmailId(employee, ref ReporingToIds);
                    // 35091-Ambar-End-27062012


                    string[] MgrIds;
                    if (!string.IsNullOrEmpty(ReporingToIds))
                    {
                        if (ReporingToIds.Contains(','))
                        {
                            MgrIds = ReporingToIds.Split(',');
                        }
                        else
                        {
                            MgrIds = (ReporingToIds.Split(','));
                        }
                        string reportingToIDs = string.Empty;
                        string[] EmpReportingTo;
                        foreach (DataRow row in dsFromEmployee.Tables[0].Rows)
                        {
                            reportingToIDs = row["ReportingToId"].ToString();
                        }

                        if (reportingToIDs.Contains(','))
                        {
                            EmpReportingTo = reportingToIDs.Split(',');
                        }
                        else
                        {
                            EmpReportingTo = (reportingToIDs.Split(','));
                        }
                        string finalReportingTos = string.Empty;


                        foreach (string mgr in MgrIds)
                        {
                            finalReportingTos = string.Empty;
                            foreach (string Emprow in EmpReportingTo)
                            {
                                if (mgr != Emprow)
                                {
                                    finalReportingTos += Emprow;
                                    finalReportingTos += ',';
                                }
                            }

                            if (!string.IsNullOrEmpty(finalReportingTos))
                            {
                                finalReportingTos = finalReportingTos.Remove(finalReportingTos.Length - 1);

                                if (finalReportingTos.Contains(','))
                                {
                                    EmpReportingTo = finalReportingTos.Split(',');

                                }
                                else
                                {
                                    EmpReportingTo = (finalReportingTos.Split(','));
                                }
                            }
                        }

                        MRFBL.UpdateReportingTOForEmployee(Mrf, finalReportingTos);
                    }
                }

                if (IsProjectClosed)
                {
                    //then send Email for closed project
                    int ProjectId = 0;
                    ProjectId = int.Parse(hfProjectId.Value);
                    Rave.HR.BusinessLayer.Projects.Projects objProjectsBAL;
                    BusinessEntities.Projects objRaveHR = null;
                    //Get the prooject details for email functionality.
                    objProjectsBAL = new Rave.HR.BusinessLayer.Projects.Projects();
                    objRaveHR = new BusinessEntities.Projects();
                    objRaveHR = objProjectsBAL.RetrieveProjectDetails(ProjectId);
                    //--Call the BL method to send Emails.
                    objEmployeeBAL.SendMailToEmployeeForClosedProject(objRaveHR);
                }

                ////Close the window and refresh the employeesummary page.
                string varToParent = "ReloadEmpSummaryPage";
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "CloseJS",
                    "jQuery.modalDialog.getCurrent().close();jQuery.modalDialog.getCurrent().postMessageToParent('" + varToParent + "');", true);

                //string script = "window.location.href='EmployeeSummary.aspx';__doPostBack();";

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "close", script, true);
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "UpdateEmployeeReleaseDate", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    /// <summary>
    /// Gets the employee.
    /// </summary>
    /// <param name="employeeObj">The employee obj.</param>
    /// <returns></returns>
    private BusinessEntities.Employee GetEmployee(BusinessEntities.Employee employeeObj)
    {
        BusinessEntities.Employee empPar = null;
        empPar = new BusinessEntities.Employee();
        BusinessEntities.Employee employee = new BusinessEntities.Employee();
        Rave.HR.BusinessLayer.Employee.Employee employeeBL = new Rave.HR.BusinessLayer.Employee.Employee();

        try
        {
            employee = employeeBL.GetEmployee(employeeObj);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "GetEmployee", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

        return employee;
    }
    /// <summary>
    /// Validates the controls.
    /// </summary>
    /// <returns></returns>
    private bool ValidateControls()
    {
        StringBuilder errMessage = new StringBuilder();
        bool flag = true;
        string[] ProjectEndDateArr;
        string[] ReleaseDateArr;

        ProjectEndDateArr = Convert.ToString(hfProjectEndDate.Value).Split(SPILITER_SLASH);
        DateTime ProjectEndDate = new DateTime(Convert.ToInt32(ProjectEndDateArr[2]), Convert.ToInt32(ProjectEndDateArr[1]), Convert.ToInt32(ProjectEndDateArr[0]));

        ReleaseDateArr = Convert.ToString(ucDatePickerReleaseDate.Text).Split(SPILITER_SLASH);
        DateTime ReleaseDate = new DateTime(Convert.ToInt32(ReleaseDateArr[2]), Convert.ToInt32(ReleaseDateArr[1]), Convert.ToInt32(ReleaseDateArr[0]));

        if (ProjectEndDate < ReleaseDate)
        {
            errMessage.Append(CommonConstants.RELEASEDATE_VALIDATION + " i.e " + ProjectEndDate.ToString("dd/MM/yyyy") + ".");
            flag = false;
        }

        lblError.Text = errMessage.ToString();
        return flag;

    }

    protected void grdvListofEmpProject_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }



    #endregion Private Methods

}
