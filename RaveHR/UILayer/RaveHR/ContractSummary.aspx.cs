
//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           ContractSummary.cs       
//  Class:          ContractSummary
//  Author:         Gopal Chauhan
//  Date written:   17/8/2009 3:51:30 PM
//  Description:    This class contains properties related to Contract Summary for contract. 
//                  
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  17/8/2009 3:51:30 PM  Gopal Chauhan    n/a     Created    
//  17/09/2009            yagendra sharnagat       Modified 
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using Common;
using Rave.HR.BusinessLayer;
using Common.Constants;
using Common.AuthorizationManager;


public partial class ContractSummary : BaseClass
{
    #region Private Field Variables

    //Declare contract criteria object of ContractCriteria class for fetching record from databse based on criteria set. 
    BusinessEntities.ContractCriteria contractCriteria = null;

    //Declare ContractProject object of ContractProject class of business layer for calling method of business layer.
    Rave.HR.BusinessLayer.Contracts.ContractProject contractProject = null;

    //Declare Contract object of Contract class of business layer for calling method of business layer.
    Rave.HR.BusinessLayer.Contracts.Contract contract = null;

    //Declare Contract List Object for holding the contract records. 
    List<BusinessEntities.Contract> contractList = null;

    // Store the Value of row count.
    Hashtable hashTable = new Hashtable();

    //27633-Subhra-Start
    /// <summary>
    /// To store the data for previous and next page for paging issue
    /// </summary>
    Hashtable temppreviousHashTable = new Hashtable();
    BusinessEntities.RaveHRCollection raveHRpreviousCollection = new BusinessEntities.RaveHRCollection();
    private int IntHashpageCount = 0;
    //End 27633

    // Defines default value for sorting expression .
    private static string sortExpression = string.Empty;

    //this variable wil contain the information of page count based on total record fetch at loading time.
    private int pageCount;

    //DEfine the zero as string.
    private string ZERO = "0";

    //DEfine the select as string.
    private string SELECTONE = "Select";

    string ContractStatus = "4";

    string ContractType = "5";

    string ClientName = "3";

    //this property is used to get or set the sorting direction.
    private SortDirection GridViewSortDirection
    {
        get
        {
            if (Session[SessionNames.CONTRACT_SORT_DIRECTION] == null)
                Session[SessionNames.CONTRACT_SORT_DIRECTION] = SortDirection.Ascending;

            return (SortDirection)Session[SessionNames.CONTRACT_SORT_DIRECTION];
        }
        set
        {
            Session[SessionNames.CONTRACT_SORT_DIRECTION] = value;
        }
    }

    //this variable contain the value for "ASC" ie ascending.
    private const string ASCENDING = " ASC";

    //this variable contain the value for "DESC" ie descending.
    private const string DESCENDING = " DESC";

    // This variable used for assign gridview command argument.
    private int currentRow = 0;

    //this variable hold the value of class name and used in exception handing.
    private const string CLASS_NAME_CONTRACT_SUMMARY = "ContractSummary.aspx";

    // Sets the image direction either upwards or downwards.
    private string imageDirection = string.Empty;

    #endregion

    #region Protected Methods

    /// <summary>
    /// This is page load method which is fired on loading of page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
                ArrayList arrRolesForUser = new ArrayList();
                AuthorizationManager RaveHRAuthorizationManager = new AuthorizationManager();
                arrRolesForUser = RaveHRAuthorizationManager.getRolesForUser(RaveHRAuthorizationManager.getLoggedInUser());

                if (!(arrRolesForUser.Contains(AuthorizationManagerConstants.ROLERPM) ||
                     arrRolesForUser.Contains(AuthorizationManagerConstants.ROLECOO) ||
                     arrRolesForUser.Contains(AuthorizationManagerConstants.ROLECEO) ||
                     arrRolesForUser.Contains(AuthorizationManagerConstants.ROLECFM) ||
                     arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEPRESALES) ||
                     arrRolesForUser.Contains(AuthorizationManagerConstants.ROLEFM)))
                {
                    Response.Redirect(CommonConstants.PAGE_HOME, true);
                }
                //Siddharth 26th August 2015 End


                if (!IsPostBack)
                {
                    if (Session[SessionNames.CONTRACT_CONTRACTIDHASHTABLE] != null)
                    {
                        Session.Remove(SessionNames.CONTRACT_CONTRACTIDHASHTABLE);
                    }

                    //This methos will fill the contract status dropdown in filter panel.
                    FillContractStatus();

                    //This method will fill the Contract type drop down  in filter panel.
                    FillContractType();

                    //This method will fill the Client name drop down  in filter panel.
                   // FillClientName();

                    FillClientNameDropDown();

                    if (Request.QueryString[QueryStringConstants.CANCLECLICK] != null)
                    {
                        BindGrid(GetContractCriteriaData());

                        //Set selected value in filter.
                        SetFilterControlsValue();
                    }
                    else
                    {
                        //Remove the filter session.
                        RemoveFilterSession();

                        //instantiate the object of contract criteria.
                        contractCriteria = new BusinessEntities.ContractCriteria();

                        //contractCriteria.Mode = 1 when contract summary page is opening first time 
                        //in this mode all list of contract will come
                        contractCriteria.Mode = 1;

                        //Since first time no specific Contract Details required so contract Id =0
                        contractCriteria.ContractId = 0;

                        sortExpression = CommonConstants.CON_CONTRACTCODE;

                        GridViewSortDirection = SortDirection.Descending;

                        //This method will bind the grid with specified criteria.
                        BindGrid(contractCriteria);
                    }
                }
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_CONTRACT_SUMMARY, "Page_Load", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }


    }

    /// <summary>
    /// This event is fired on click on Filter button on filter panel.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            //Set values of filtering criterias to session so as can use in sorting time
            Session[SessionNames.CONTRACT_DOCUMENTNAME] = txtDocumentName.Text.Trim();
            Session[SessionNames.CONTRACT_CONTRACTTYPE] = ddlContractType.SelectedValue;
            Session[SessionNames.CONTRACT_CONTRACTSTATUS] = ddlContractStatus.SelectedValue;
            Session[SessionNames.CONTRACT_CLIENTNAME] = ddlClientName.SelectedValue;

            //instantiating criteria object for stipulation on which record will fetch.
            contractCriteria = new BusinessEntities.ContractCriteria();

            //Checking and imputing the value of document name. 
            contractCriteria.DocumentName = txtDocumentName.Text.Trim();

            //Checking and imputing the value of Contract type. if no value is stipulated then pass 0.
            if (string.IsNullOrEmpty(ddlContractType.SelectedValue))
            {
                contractCriteria.ContractTypeID = 0;
            }
            else
            {
                contractCriteria.ContractTypeID = Convert.ToInt32(ddlContractType.SelectedValue);
            }

            //Checking and imputing the value of Client name. if no value is stipulated then pass 0.
            if (string.IsNullOrEmpty(ddlClientName.SelectedValue))
            {
                contractCriteria.ClientNameId = 0;
            }
            else
            {
                contractCriteria.ClientNameId = Convert.ToInt32(ddlClientName.SelectedValue);
            }

            //Checking and imputing the value of Contract Status. if no value is stipulated then pass 0.
            if (string.IsNullOrEmpty(ddlContractStatus.SelectedValue))
            {
                contractCriteria.ContractStatus = 0;
            }
            else
            {
                if (ddlContractStatus.SelectedValue != ZERO)
                {
                    contractCriteria.ContractStatus = Convert.ToInt32(ddlContractStatus.SelectedValue);
                }
                else
                {
                    contractCriteria.ContractStatus = 0;
                }
            }

            //on click on filter button record should come as stipulated in filter panel.
            //while filtering Mode should be 2.
            contractCriteria.Mode = 2;

            //Check direction of order & add to criteria object.
            if (GridViewSortDirection == SortDirection.Ascending)
            {
                contractCriteria.Direction = ASCENDING;
            }
            else
            {
                contractCriteria.Direction = DESCENDING;
            }

            //get sort expression.
            if (string.IsNullOrEmpty(sortExpression))
            {
                contractCriteria.SortExpression = CommonConstants.CON_CONTRACTCODE;
            }
            else
            {
                contractCriteria.SortExpression = sortExpression;
            }

            //Get page number 1 as on the click of filter.
            contractCriteria.PageNumber = 1;
            Session[SessionNames.CONTRACT_CURRENTPAGEINDEX] = contractCriteria.PageNumber;

            //this method is used for fetching the records based on criteria
            BindGrid(contractCriteria);

            //Make visible to remove filter button.
            btnRemoveFilter.Visible = true;

        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_CONTRACT_SUMMARY, "btnFilter_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// This event is fired on changing the page number 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ChangePage(object sender, CommandEventArgs e)
    {
        try
        {
            //Getting the object of bottom pager of grid view which contains the previous and next link buttons 
            GridViewRow gvrPager = grdvListofContract.BottomPagerRow;

            //getting text box object form bottom page row.
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");

            //getting Previous link button object form bottom page row.
            LinkButton lbtnPrevious = (LinkButton)gvrPager.Cells[0].FindControl("lbtnPrevious");

            //getting Next link button object form bottom page row.
            LinkButton lbtnNext = (LinkButton)gvrPager.Cells[0].FindControl("lbtnNext");

            //this variable is used to hold the value of total page count.
            int pageCountForLnkBtn = Convert.ToInt32(Session[SessionNames.CONTRACT_ACTUALPAGECOUNT].ToString());

            //Checking the command name and changing the page number in textbox according to that.
            switch (e.CommandName)
            {
                //if Previous lnk button is clicked then page number in text box should be reduced by 1.
                case "Previous":
                    //This value is hold in session.
                    //To solved the issue id : 20375
                    //Start
                    if ((Convert.ToInt32(txtPages.Text) - 1) == 0)
                    {
                        Session[SessionNames.CONTRACT_CURRENTPAGEINDEX] = 1;
                        txtPages.Text = "1";
                    }
                    else
                    {
                    //End
                        Session[SessionNames.CONTRACT_CURRENTPAGEINDEX] = Convert.ToInt32(txtPages.Text) - 1;

                        //changing the value of textbox
                        txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) - 1);
                    }
                    break;

                //if Next lnk button is clicked then page number in text box should be increased by 1.
                case "Next":
                    //This value is hold in session.
                    Session[SessionNames.CONTRACT_CURRENTPAGEINDEX] = Convert.ToInt32(txtPages.Text) + 1;

                    //changing the value of textbox
                    txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) + 1);
                    break;
            }

            //this method will bind the rcord to grid view specified in criteria object.
            BindGrid(GetContractCriteriaData());
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_CONTRACT_SUMMARY, "ChangePage", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// This event is fired on changed the value of text box.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void txtPages_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txtPages = (TextBox)sender;

            //Check text entered in text box is numeric or not.
            txtPages.Attributes.Add(CommonConstants.EVENT_ONKEYPRESS, "return isNumberKey(event)");

            //Check for Paging text box is not empty, non-zero, and out of range paging no.
            if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0 && Convert.ToInt32(txtPages.Text) <= Convert.ToInt32(Session[SessionNames.CONTRACT_ACTUALPAGECOUNT].ToString()))
            {
                grdvListofContract.PageIndex = Convert.ToInt32(txtPages.Text) - 1;

                //Getting the cureent page index in session.
                Session[SessionNames.CONTRACT_CURRENTPAGEINDEX] = txtPages.Text;
            }
            else
            {
                txtPages.Text = Session[SessionNames.CONTRACT_CURRENTPAGEINDEX].ToString();
                return;
            }

            //this method will bind the rcord to grid view specified in criteria object.
            BindGrid(GetContractCriteriaData());

            //Setting the text of text box
            txtPages.Text = Session[SessionNames.CONTRACT_CURRENTPAGEINDEX].ToString();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_CONTRACT_SUMMARY, "txtPages_TextChanged", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Remove the filter criteria .
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnRemoveFilter_Click(object sender, EventArgs e)
    {
        //remove the session values.
        Session.Remove(SessionNames.CONTRACT_DOCUMENTNAME);
        Session.Remove(SessionNames.CONTRACT_CONTRACTTYPE);
        Session.Remove(SessionNames.CONTRACT_CONTRACTSTATUS);
        Session.Remove(SessionNames.CONTRACT_CLIENTNAME);
        //Session.Remove(SessionNames.CONTRACT_ACTUALPAGECOUNT);
        //Session.Remove(SessionNames.CONTRACT_CURRENTPAGEINDEX);
        Session.Remove(SessionNames.CONTRACT_PREVIOUS_SORT_EXPRESSION);
        Session.Remove(SessionNames.CONTRACT_SORT_DIRECTION);

        //Clear the filter controls.
        ddlContractStatus.SelectedValue = ZERO;
        ddlContractType.SelectedValue = ZERO;
        ddlClientName.SelectedValue = ZERO;
        txtDocumentName.Text = string.Empty;

        contractCriteria = new BusinessEntities.ContractCriteria();

        //contractCriteria.Mode = 1 when contract summary page is opening first time 
        //in this mode all list of contract will come
        contractCriteria.Mode = 1;

        //Since first time no specific Contract Details required so contract Id =0
        contractCriteria.ContractId = 0;

        sortExpression = CommonConstants.CON_CONTRACTCODE;
        GridViewSortDirection = SortDirection.Descending;
        //This method will bind the grid with specified criteria.
        BindGrid(contractCriteria);

        //Make visible false to remove filter button.
        btnRemoveFilter.Visible = false;
    }

    /// <summary>
    /// This event occurs when a button is clicked in a GridView control.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdvListofContracts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //instantiate the criteria object
            BusinessEntities.ContractCriteria criteria = new BusinessEntities.ContractCriteria();

            //displaying child grid when button is clicked.
            if (e.CommandName == "ChildGridContractsForProject")
            {
                GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                ImageButton imgbtnExpandCollaspeChildGrid = (ImageButton)grv.FindControl("imgbtnExpandCollaspeChildGrid");
                GridView grdProjectGrid = (GridView)grv.FindControl("grdProjectGrid");
                HtmlTableRow tr_ProjectGrid = (HtmlTableRow)grv.FindControl("tr_ProjectGrid");

                //--Collaspe 
                if ((imgbtnExpandCollaspeChildGrid != null) && (tr_ProjectGrid != null))
                {
                    if (imgbtnExpandCollaspeChildGrid.ImageUrl == Common.CommonConstants.IMAGE_MINUSSIGNPATH)
                    {
                        tr_ProjectGrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        imgbtnExpandCollaspeChildGrid.ImageUrl = Common.CommonConstants.IMAGE_PLUSSIGNPATH;
                        return;
                    }
                }

                //--Collaspe the child grids
                foreach (GridViewRow grvRow in grdvListofContract.Rows)
                {
                    ImageButton imgbtnExpandCollaspe = (ImageButton)grvRow.FindControl("imgbtnExpandCollaspeChildGrid");
                    HtmlTableRow tr_ChildGrid = (HtmlTableRow)grvRow.FindControl("tr_ProjectGrid");

                    tr_ChildGrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                    imgbtnExpandCollaspe.ImageUrl = Common.CommonConstants.IMAGE_PLUSSIGNPATH;

                }

                //--Set entity
                criteria.ContractId = int.Parse(e.CommandArgument.ToString());

                //Set mode=0 when u have to fetch project under particular contract.
                criteria.Mode = 0;

                //--Get contracts for project                
                Rave.HR.BusinessLayer.Contracts.ContractProject objBLLGetProjectsForContract = new Rave.HR.BusinessLayer.Contracts.ContractProject();

                //instantiate object of contract project class for holding the data of fetched record. 
                List<BusinessEntities.ContractProject> objListGetProjectsForContract = new List<BusinessEntities.ContractProject>();

                //Geting record of project under any particular contract.
                objListGetProjectsForContract = objBLLGetProjectsForContract.GetProjectsForContracts(criteria);

                //--Display child grid
                if (grdProjectGrid != null)
                {
                    //--Bind the grid.
                    grdProjectGrid.DataSource = objListGetProjectsForContract;
                    grdProjectGrid.DataBind();
                }

                //--Expand child grid
                if ((imgbtnExpandCollaspeChildGrid != null) && (tr_ProjectGrid != null))
                {
                    imgbtnExpandCollaspeChildGrid.ImageUrl = Common.CommonConstants.IMAGE_MINUSSIGNPATH;
                    //Name: Sanju:Issue Id 50201 Kushwaha Removed display property  so that it should display grid properly in IE10,Chrome and mozilla browser.
                    //tr_ProjectGrid.Style.Add(HtmlTextWriterStyle.Display, "block");
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_CONTRACT_SUMMARY, "grdvListofContracts_RowCommand", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// This event sorts the contents of the DataGridView control.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdvListofContract_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            //On sorting assign new sort expression.
            sortExpression = e.SortExpression;

            //Keep the Sorting direction in session.
            Session[SessionNames.CONTRACT_SORTDIRECTIONFORBINDING] = ASCENDING;

            //Check the session for previous sort expression and setting value according to that.
            if (Session[SessionNames.CONTRACT_PREVIOUS_SORT_EXPRESSION] == null)
            {
                //Keep the Sorting direction in session.
                Session[SessionNames.CONTRACT_PREVIOUS_SORT_EXPRESSION] = sortExpression;
            }

            //Setting the sort direction and fetch data accroding to that.
            if (Session[SessionNames.CONTRACT_PREVIOUS_SORT_EXPRESSION].ToString() == sortExpression)
            {
                //Sort to opposite direction based upon previous sort direction
                if (Session[SessionNames.CONTRACT_SORT_DIRECTION] == null || GridViewSortDirection == SortDirection.Descending)
                {
                    GridViewSortDirection = SortDirection.Ascending;

                    //Keep the Sorting direction in session.
                    Session[SessionNames.CONTRACT_SORTDIRECTIONFORBINDING] = ASCENDING;

                    //getting data according to sort direction and on sortexpression field.
                    SortGridView(sortExpression, ASCENDING);
                }
                else
                {
                    GridViewSortDirection = SortDirection.Descending;

                    //Keep the Sorting direction in session.
                    Session[SessionNames.CONTRACT_SORTDIRECTIONFORBINDING] = DESCENDING;

                    //getting data according to sort direction and on sortexpression field.
                    SortGridView(sortExpression, DESCENDING);
                }
            }
            else
            {
                GridViewSortDirection = SortDirection.Ascending;

                //Keep the Sorting direction in session.
                Session[SessionNames.CONTRACT_SORTDIRECTIONFORBINDING] = ASCENDING;

                //getting data according to sort direction and on sortexpression field.
                SortGridView(sortExpression, ASCENDING);
            }

            //Set current sort expression as PreviousSortExpression
            Session[SessionNames.CONTRACT_PREVIOUS_SORT_EXPRESSION] = sortExpression;
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_CONTRACT_SUMMARY, "grdvListofContract_Sorting", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// This event fires when data will bound on gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdvListofContract_DataBound(object sender, EventArgs e)
    {
        try
        {
            //Getting object that represent bottom pager.
            GridViewRow gvrPager = grdvListofContract.BottomPagerRow;
            if (gvrPager == null) return;

            //Get Text Box and Lable from the Grid View
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            Label lblPageCount = (Label)gvrPager.Cells[0].FindControl("lblPageCount");

            //Assign current page index to text box
            if (Session[SessionNames.CONTRACT_CURRENTPAGEINDEX] != null)
            {
                txtPages.Text = Session[SessionNames.CONTRACT_CURRENTPAGEINDEX].ToString();
            }
            else
            {
                txtPages.Text = "1";
            }
            //Assign total no of pages to label
            if (lblPageCount != null)
                lblPageCount.Text = (Session[SessionNames.CONTRACT_ACTUALPAGECOUNT].ToString());
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_CONTRACT_SUMMARY, "grdvListofContract_DataBound", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// This event occurs when a data row is bound to data in a GridView control.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdvListofContract_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //Check whether row is data row or not.
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //On mouse over add style(Underline).
                //Name: Sanju:Issue Id 50201  Changed cursor property to pointer so that it should display hand in IE10,9,Chrome and mozilla browser.
               // e.Row.Attributes[CommonConstants.EVENT_ONMOUSEOVER] = "this.style.cursor='hand';this.style.textDecoration='underline';";
                // Mohamed : NIS-RMS : 07/01/2015 : Starts                        			  
                // Desc : Remove underline and click from remaining columns


                e.Row.Cells[1].Attributes[CommonConstants.EVENT_ONMOUSEOVER] = "this.style.cursor='pointer';this.style.textDecoration='underline';";
                e.Row.Cells[1].Attributes[CommonConstants.EVENT_ONMOUSEOUT] = "this.style.textDecoration='none';";

                //Page should be redirect on Add Contract page onclick of respective row.
                e.Row.Cells[1].Attributes[CommonConstants.EVENT_ONCLICK] = "location.href = 'AddContract.aspx?" + URLHelper.SecureParameters("ContractID", DataBinder.Eval(e.Row.DataItem, "ContractId").ToString()) + "&" + URLHelper.SecureParameters("index", e.Row.RowIndex.ToString()) + "&" + URLHelper.CreateSignature(DataBinder.Eval(e.Row.DataItem, "ContractId").ToString(), e.Row.RowIndex.ToString()) + "'";
                //e.Row.Cells[2].Attributes[CommonConstants.EVENT_ONCLICK] = "location.href = 'AddContract.aspx?" + URLHelper.SecureParameters("ContractID", DataBinder.Eval(e.Row.DataItem, "ContractId").ToString()) + "&" + URLHelper.SecureParameters("index", e.Row.RowIndex.ToString()) + "&" + URLHelper.CreateSignature(DataBinder.Eval(e.Row.DataItem, "ContractId").ToString(), e.Row.RowIndex.ToString()) + "'";
                //e.Row.Cells[3].Attributes[CommonConstants.EVENT_ONCLICK] = "location.href = 'AddContract.aspx?" + URLHelper.SecureParameters("ContractID", DataBinder.Eval(e.Row.DataItem, "ContractId").ToString()) + "&" + URLHelper.SecureParameters("index", e.Row.RowIndex.ToString()) + "&" + URLHelper.CreateSignature(DataBinder.Eval(e.Row.DataItem, "ContractId").ToString(), e.Row.RowIndex.ToString()) + "'";
                //e.Row.Cells[4].Attributes[CommonConstants.EVENT_ONCLICK] = "location.href = 'AddContract.aspx?" + URLHelper.SecureParameters("ContractID", DataBinder.Eval(e.Row.DataItem, "ContractId").ToString()) + "&" + URLHelper.SecureParameters("index", e.Row.RowIndex.ToString()) + "&" + URLHelper.CreateSignature(DataBinder.Eval(e.Row.DataItem, "ContractId").ToString(), e.Row.RowIndex.ToString()) + "'";
                //e.Row.Cells[5].Attributes[CommonConstants.EVENT_ONCLICK] = "location.href = 'AddContract.aspx?" + URLHelper.SecureParameters("ContractID", DataBinder.Eval(e.Row.DataItem, "ContractId").ToString()) + "&" + URLHelper.SecureParameters("index", e.Row.RowIndex.ToString()) + "&" + URLHelper.CreateSignature(DataBinder.Eval(e.Row.DataItem, "ContractId").ToString(), e.Row.RowIndex.ToString()) + "'";

                // Mohamed : NIS-RMS : 07/01/2015 : Ends
                //Remove the above mentioned style from cell[0].
                //Sanju:Issue Id 50201: commented the below code so that cursor should be displayed on hover of "+" symbol
           //     e.Row.Cells[0].Attributes[CommonConstants.EVENT_ONMOUSEOVER] = "this.style.cursor='none';this.style.textDecoration='none';";
                //sanju end

                //Check if the Contract does not contain project than make unvisible to plus sign.
                if (((HiddenField)e.Row.Cells[0].FindControl("hfProjectId")).Value == ZERO)
                {
                    e.Row.Cells[0].FindControl("imgbtnExpandCollaspeChildGrid").Visible = false;
                }
                //To solved the issue id : 20375
                //Start
                if (!hashTable.Contains(e.Row.RowIndex))
                //Add Contract id to hashtable because it is used in add contracts page to get previous next contracts.
                hashTable.Add(e.Row.RowIndex, DataBinder.Eval(e.Row.DataItem, "ContractID").ToString());
                //End
            }
            //Added contract id in sorted order to get previous next contract on add contract page.
            Session[SessionNames.CONTRACT_CONTRACTIDHASHTABLE] = hashTable;
        }

        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_CONTRACT_SUMMARY, "grdvListofContract_DataBound", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Add the sorting image in the haeder of grid.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdvListofContract_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //Check whether row is header or not.
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (Session[SessionNames.CONTRACT_ACTUALPAGECOUNT] != null)
                {
                    if ((Convert.ToInt32(Session[SessionNames.CONTRACT_ACTUALPAGECOUNT].ToString()) > 0) && grdvListofContract.AllowSorting == true)
                    {
                        //Add sort Images to Grid View Header
                        AddSortImage(e.Row);
                    }
                }
            }
        }

        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_CONTRACT_SUMMARY, "grdvListofContract_RowCreated", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// On row command expand the inner grid to show RP details of project.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdProjectGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //displaying child grid when button is clicked.
            if (e.CommandName == "ChildGridContractsForRP")
            {
                GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                ImageButton imgbtnExpandCollaspeChildGrid = (ImageButton)grv.FindControl("imgbtnExpandCollaspeRPChildGrid");
                GridView grdRPGrid = (GridView)grv.FindControl("grdRPGrid");
                HtmlTableRow tr_RPGrid = (HtmlTableRow)grv.FindControl("tr_RPGrid");
                GridView grdProjectGrid = (GridView)grv.NamingContainer;

                //--Collaspe 
                if ((imgbtnExpandCollaspeChildGrid != null) && (tr_RPGrid != null))
                {
                    if (imgbtnExpandCollaspeChildGrid.ImageUrl == Common.CommonConstants.IMAGE_MINUSSIGNPATH)
                    {
                        tr_RPGrid.Style.Add(HtmlTextWriterStyle.Display, "none");
                        imgbtnExpandCollaspeChildGrid.ImageUrl = Common.CommonConstants.IMAGE_PLUSSIGNPATH;
                        return;
                    }
                }

                if (grdProjectGrid != null)
                {
                    //--Collaspe the child grids
                    foreach (GridViewRow grvRow in grdProjectGrid.Rows)
                    {
                        ImageButton imgbtnExpandCollaspe = (ImageButton)grvRow.FindControl("imgbtnExpandCollaspeRPChildGrid");
                        HtmlTableRow tr_ChildGrid = (HtmlTableRow)grvRow.FindControl("tr_RPGrid");
                        if (tr_ChildGrid != null)
                            tr_ChildGrid.Style.Add(HtmlTextWriterStyle.Display, "none");

                        if (imgbtnExpandCollaspe != null)
                            imgbtnExpandCollaspe.ImageUrl = Common.CommonConstants.IMAGE_PLUSSIGNPATH;

                    }
                }

                //--Display child grid
                if (grdRPGrid != null)
                {
                    //--Bind the grid.
                    grdRPGrid.DataSource = GetRPDetailsByProjectId(int.Parse(e.CommandArgument.ToString()));
                    grdRPGrid.DataBind();
                }

                //--Expand child grid
                if ((imgbtnExpandCollaspeChildGrid != null) && (tr_RPGrid != null))
                {
                    imgbtnExpandCollaspeChildGrid.ImageUrl = Common.CommonConstants.IMAGE_MINUSSIGNPATH;
                   // tr_RPGrid.Style.Add(HtmlTextWriterStyle.Display, "block");
                  //Name: Sanju:Issue Id 50201 . Removed display property  so that it should display grid properly in IE10,Chrome and mozilla browser.
                    tr_RPGrid.Style.Add(HtmlTextWriterStyle.Display, "");
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_CONTRACT_SUMMARY, "grdProjectGrid_RowCommand", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

    /// <summary>
    /// On row command view RP details on excel sheet.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdRPGrid_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ViewImageBtn")
            {
                //Get the grid view row where the command is being fired.
                GridViewRow grv = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                //Define the object of business layer.
                Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLViewApproveRejectRP = new Rave.HR.BusinessLayer.Projects.ResourcePlan();

                //get the deatils on excel sheet.
                //Issue Id : 33959 STrAT
                //objBLLViewApproveRejectRP.ViewRPInExcel(int.Parse(e.CommandArgument.ToString()), grv.Cells[1].Text);
                objBLLViewApproveRejectRP.ViewRPInExcel(int.Parse(e.CommandArgument.ToString()), grv.Cells[1].Text, "");
                //Issue Id : 33959 END
            }
        }
        catch (System.Threading.ThreadAbortException ex)
        { }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_CONTRACT_SUMMARY, "grdRPGrid_OnRowCommand", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Hide the expand sign(+) if no RP is available for project.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdProjectGrid_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //Check whether row is data row or not.
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Check if the Contract does not contain project than make unvisible to plus sign.
                if (((HiddenField)e.Row.Cells[0].FindControl("hfRPId")).Value == ZERO)
                {
                    e.Row.Cells[0].FindControl("imgbtnExpandCollaspeRPChildGrid").Visible = false;
                }
            }
        }

        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_CONTRACT_SUMMARY, "grdvListofContract_DataBound", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// This method will bind the grid based on specified criteria object 
    /// </summary>
    /// <param name="criteria"></param>
    private void BindGrid(BusinessEntities.ContractCriteria criteria)
    {
        try
        {
            // contractProject = new Rave.HR.BusinessLayer.Contracts.ContractProject();
            contract = new Rave.HR.BusinessLayer.Contracts.Contract();

            //This object will contain the details of list of contracts
            contractList = new List<BusinessEntities.Contract>();

            //contractList = contractProject.GetContracts(criteria);
            contractList = contract.GetContracts(criteria);

            //27633-Subhra-Start
            int k = 0;
            foreach (BusinessEntities.Contract collectionprevious in contractList)
            {
                temppreviousHashTable.Add(k, collectionprevious.ContractID.ToString());
                k++;
            }
            Session[SessionNames.CONTRACTPREVIOUSHASHTABLE] = temppreviousHashTable;
            // 27633-ambar-End

            //If  filter  is not used.
            if (criteria.Mode == 1)
            {
                //Get the page count from contractlist which is assigned to contract
                pageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(contractList.Count) / 10));
            }
            else
            {
                //Get the page count from contract which is assigned to contract 
                foreach (BusinessEntities.Contract ContractItem in contractList)
                {
                    pageCount = ContractItem.PageCount;
                    break;
                }
            }
            //Assign page count to session variable.
            Session[SessionNames.CONTRACT_ACTUALPAGECOUNT] = pageCount;

            //If page count is 1 and only single record is display then no need to do sorting.
            if ((Convert.ToInt32(Session[SessionNames.CONTRACT_ACTUALPAGECOUNT].ToString()) == 1) && contractList.Count == 1)
            {
                grdvListofContract.AllowSorting = false;
            }
            else
            {
                grdvListofContract.AllowSorting = true;
            }

            if (contractList.Count > 0)
            {
                //Bind the grid view.
                grdvListofContract.DataSource = contractList;
                grdvListofContract.AllowPaging = true;
                grdvListofContract.PageSize = 10;
                grdvListofContract.DataBind();
            }
            else
            {
                grdvListofContract.DataSource = contractList;
                grdvListofContract.DataBind();

                //Display Header with proper message When GridView Empty .
                ShowHeaderWhenEmptyGrid(contractList);
            }



            //Create the object of bottom row of grid.
            GridViewRow gvrPager = grdvListofContract.BottomPagerRow;

            if (gvrPager != null)
            {
                //Get Text Box and Lable from the Grid View
                TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
                Label lblPageCount = (Label)gvrPager.Cells[0].FindControl("lblPageCount");
                LinkButton lbtnPrevious = (LinkButton)gvrPager.Cells[0].FindControl("lbtnPrevious");
                LinkButton lbtnNext = (LinkButton)gvrPager.Cells[0].FindControl("lbtnNext");

                //If page count is greater than one than make visible to bottom row.
                if (Convert.ToInt32(Session[SessionNames.CONTRACT_ACTUALPAGECOUNT].ToString()) > 1)
                {
                    grdvListofContract.BottomPagerRow.Visible = true;
                }
                else
                {
                    grdvListofContract.BottomPagerRow.Visible = false;
                }

                //For page load set the default values of controls.
                if (criteria.Mode == 1 && Convert.ToInt32(Session[SessionNames.CONTRACT_ACTUALPAGECOUNT].ToString()) > 1)
                {
                    lblPageCount.Text = pageCount.ToString();
                    txtPages.Text = "1";
                    lbtnNext.Enabled = true;
                    Session[SessionNames.CONTRACT_CURRENTPAGEINDEX] = txtPages.Text;
                }
                else
                {
                    //If pageindex is one than disable the previous button.
                    if (Convert.ToInt32(Session[SessionNames.CONTRACT_CURRENTPAGEINDEX]) == 1)
                    {
                        lbtnNext.Enabled = true;
                        lbtnPrevious.Enabled = false;
                    }
                    //If pageindex is greater than one than enable next & previous button.
                    else if (Convert.ToInt32(Session[SessionNames.CONTRACT_CURRENTPAGEINDEX]) > 1 && Convert.ToInt32(Session[SessionNames.CONTRACT_CURRENTPAGEINDEX]) < Convert.ToInt32(Session[SessionNames.CONTRACT_ACTUALPAGECOUNT].ToString()))
                    {
                        lbtnNext.Enabled = true;
                        lbtnPrevious.Enabled = true;
                    }
                    //If pageindex is last page than disable next  button.
                    if (Convert.ToInt32(Session[SessionNames.CONTRACT_CURRENTPAGEINDEX]) == Convert.ToInt32(Session[SessionNames.CONTRACT_ACTUALPAGECOUNT].ToString()))
                    {
                        lbtnNext.Enabled = false;
                        lbtnPrevious.Enabled = true;
                    }
                }
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_CONTRACT_SUMMARY, "BindGrid", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// this method will fill the drop down of contract status in filter panel.
    /// </summary>
    private void FillContractStatus()
    {
        try
        {
            //This object contains the records of master ata for contract status.
            List<BusinessEntities.Master> objType = new List<BusinessEntities.Master>();

            //Instantiate the object of master class to call its method .
            Rave.HR.BusinessLayer.Common.Master objTypeOfProject = new Rave.HR.BusinessLayer.Common.Master();

            //This region will fill the Contract Status drop down with the valid location types.
            objType = objTypeOfProject.GetMasterData(ContractStatus);

            //assigning the data source to drop down.
            ddlContractStatus.DataSource = objType;

            //Setting the data value field.
            ddlContractStatus.DataValueField = CommonConstants.CON_MASTERID;

            //setting data text value.
            ddlContractStatus.DataTextField = CommonConstants.CON_MASTERNAME;

            // binding to drop down
            ddlContractStatus.DataBind();
            ddlContractStatus.Items.Insert(0, new ListItem(SELECTONE, ZERO));
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_CONTRACT_SUMMARY, "FillContractStatus", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// This method fill the contract type drop down in filter panel.
    /// </summary>
    private void FillContractType()
    {
        try
        {
            //This object contains the records of master ata for contract status.
            List<BusinessEntities.Master> objType = new List<BusinessEntities.Master>();

            //Instantiate the object of master class to call its method .
            Rave.HR.BusinessLayer.Common.Master objTypeOfProject = new Rave.HR.BusinessLayer.Common.Master();

            //This region will fill the Contract Status drop down with the valid location types.
            objType = objTypeOfProject.GetMasterData(ContractType);

            //assigning the data source to drop down.
            ddlContractType.DataSource = objType;

            //Setting the data value field.
            ddlContractType.DataValueField = CommonConstants.CON_MASTERID;

            //setting data text value.
            ddlContractType.DataTextField = CommonConstants.CON_MASTERNAME;

            // binding to drop down
            ddlContractType.DataBind();
            ddlContractType.Items.Insert(0, new ListItem(SELECTONE, ZERO));
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_CONTRACT_SUMMARY, "FillContractType", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// this method will fill the drop down of Client Name in filter panel.
    /// </summary>
    private void FillClientName()
    {
        try
        {
            //This object contains the records of master ata for contract status.
            List<BusinessEntities.Master> objType = new List<BusinessEntities.Master>();

            //Instantiate the object of master class to call its method .
            Rave.HR.BusinessLayer.Common.Master objTypeOfProject = new Rave.HR.BusinessLayer.Common.Master();

            //This region will fill the Contract Status drop down with the valid location types.
            objType = objTypeOfProject.GetMasterData(ClientName);

            //assigning the data source to drop down.
            ddlClientName.DataSource = objType;

            //Setting the data value field.
            ddlClientName.DataValueField = CommonConstants.CON_MASTERID;

            //setting data text value.
            ddlClientName.DataTextField = CommonConstants.CON_MASTERNAME;

            // binding to drop down
            ddlClientName.DataBind();
            ddlClientName.Items.Insert(0, new ListItem(SELECTONE, ZERO));
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_CONTRACT_SUMMARY, "FillContractStatus", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// This method will sort the grid based on sort expression and direction.
    /// </summary>
    /// <param name="sortExpression"></param>
    /// <param name="direction"></param>
    private void SortGridView(string sortExpression, string direction)
    {
        try
        {
            //instantiate the criteria object for fetching records.
            contractCriteria = new BusinessEntities.ContractCriteria();

            //Set the sort expression value
            contractCriteria.SortExpression = sortExpression;

            //set the direction of criteria object
            contractCriteria.Direction = direction;

            //set the page of criteria object 
            contractCriteria.PageNumber = Convert.ToInt32(Session[SessionNames.CONTRACT_CURRENTPAGEINDEX]);

            //Set the document name if specified in filter panel
            if (Session[SessionNames.CONTRACT_DOCUMENTNAME] != null)
            {
                contractCriteria.DocumentName = Session[SessionNames.CONTRACT_DOCUMENTNAME].ToString();

                //If sorting is happening based on filter panel values then mode will be 2.
                contractCriteria.Mode = 2;
            }

            //Set the Contract Type name if specified in filter panel
            if (Session[SessionNames.CONTRACT_CONTRACTTYPE] != null)
            {
                contractCriteria.ContractTypeID = Convert.ToInt32(Session[SessionNames.CONTRACT_CONTRACTTYPE].ToString());

                //If sorting is happening based on filter panel values then mode will be 2.
                contractCriteria.Mode = 2;
            }

            //Set the contract status name if specified in filter panel
            if (Session[SessionNames.CONTRACT_CONTRACTSTATUS] != null)
            {
                contractCriteria.ContractStatus = Convert.ToInt32(Session[SessionNames.CONTRACT_CONTRACTSTATUS]);

                //If sorting is happening based on filter panel values then mode will be 2.
                contractCriteria.Mode = 2;
            }

            //Set the client name if specified in filter panel
            if (Session[SessionNames.CONTRACT_CLIENTNAME] != null)
            {
                contractCriteria.ClientNameId = Convert.ToInt32(Session[SessionNames.CONTRACT_CLIENTNAME]);

                //If sorting is happening based on filter panel values then mode will be 2.
                contractCriteria.Mode = 2;
            }

            //Getting object which will hold the fetched record.
            contractList = new List<BusinessEntities.Contract>();

            //fetching records based on criteria object and assign to variable.
            contractList = GetProjectSummaryData(contractCriteria);

            if (contractList.Count == 0)
            {
                //Assigning Data source
                grdvListofContract.DataSource = contractList;

                //Bind the Grid View in Sorted order
                grdvListofContract.DataBind();
            }
            else
            {
                //Assigning Data source
                grdvListofContract.DataSource = contractList;

                //Bind the Grid View in Sorted order
                grdvListofContract.DataBind();
            }

            //Finds the Paging Text Box, Previous and Next Link Buttons in Grid Views Pagning Templet
            GridViewRow gvrPager = grdvListofContract.BottomPagerRow;
            TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");
            LinkButton lbtnPrevious = (LinkButton)gvrPager.Cells[0].FindControl("lbtnPrevious");
            LinkButton lbtnNext = (LinkButton)gvrPager.Cells[0].FindControl("lbtnNext");

            //if Records are greater than 10 or morethan 1 page then bottom pager should visible
            if (Convert.ToInt32(Session[SessionNames.CONTRACT_ACTUALPAGECOUNT].ToString()) > 1)
            {
                grdvListofContract.BottomPagerRow.Visible = true;
            }

            //Setting the value txt box
            txtPages.Text = Convert.ToInt32(Session[SessionNames.CONTRACT_CURRENTPAGEINDEX]).ToString();

            //Don't allow any character other than Number in paging text box
            txtPages.Attributes.Add(CommonConstants.EVENT_ONKEYPRESS, "return isNumberKey(event)");

            //Enable Prvious and Disable Next when Paging Text box contains last paging number
            if (Convert.ToInt32(txtPages.Text) == Convert.ToInt32(Session[SessionNames.CONTRACT_ACTUALPAGECOUNT].ToString()))
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
            if ((Convert.ToInt32(txtPages.Text) > 1) && (Convert.ToInt32(txtPages.Text) < Convert.ToInt32(Session[SessionNames.CONTRACT_ACTUALPAGECOUNT].ToString())))
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
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_CONTRACT_SUMMARY, "SortGridView", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// This method will givethe records of project under any contract and based on criteria object
    /// </summary>
    /// <param name="criteria"></param>
    /// <returns></returns>
    private List<BusinessEntities.Contract> GetProjectSummaryData(BusinessEntities.ContractCriteria criteria)
    {
        //instantiate  object of contract class. 
        contract = new Rave.HR.BusinessLayer.Contracts.Contract();
        return contract.GetContracts(criteria);
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
                    case CommonConstants.CON_CONTRACTCODE:
                        headerRow.Cells[1].Controls.Add(sortImage);
                        break;

                    case CommonConstants.CON_CONTRACTREFID:
                        headerRow.Cells[2].Controls.Add(sortImage);
                        break;

                    case CommonConstants.CON_DOCUMENTNAME:
                        headerRow.Cells[3].Controls.Add(sortImage);
                        break;

                    case CommonConstants.CON_NAME:
                        headerRow.Cells[4].Controls.Add(sortImage);
                        break;

                    case CommonConstants.CLIENT_NAME:
                        headerRow.Cells[5].Controls.Add(sortImage);
                        break;

                    case CommonConstants.CON_FIRSTNAME:
                        headerRow.Cells[6].Controls.Add(sortImage);
                        break;
                }
            }
        }

        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_CONTRACT_SUMMARY, "AddSortImage", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Display Header When GridView Empty with proper message
    /// </summary>
    /// <param name="raveHRCollection">EmptyCollection</param>
    private void ShowHeaderWhenEmptyGrid(List<BusinessEntities.Contract> ContractList)
    {
        try
        {
            //set header visible.
            grdvListofContract.ShowHeader = true;
            // Disable sorting.
            grdvListofContract.AllowSorting = false;

            //Create empty datasource for Grid view and bind.
            ContractList.Add(new BusinessEntities.Contract());
            grdvListofContract.DataSource = ContractList;
            grdvListofContract.DataBind();

            //clear all the cells in the row.
            grdvListofContract.Rows[0].Cells.Clear();

            //add a new blank cell.
            grdvListofContract.Rows[0].Cells.Add(new TableCell());
            grdvListofContract.Rows[0].Cells[0].Text = CommonConstants.NO_RECORDS_FOUND_MESSAGE;
            grdvListofContract.Rows[0].Cells[0].Wrap = false;
            grdvListofContract.Rows[0].Cells[0].Width = Unit.Percentage(10);
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_CONTRACT_SUMMARY, "ShowHeaderWhenEmptyGrid", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Get Contract criteria data to get details.
    /// </summary>
    /// <returns></returns>
    private BusinessEntities.ContractCriteria GetContractCriteriaData()
    {
        //instantiate the criteria object for fetching the record 
        contractCriteria = new BusinessEntities.ContractCriteria();

        //setting the Sort Expression ie on which field sort will happen.By default on contract ref id, sorting will done.
        if (Session[SessionNames.CONTRACT_PREVIOUS_SORT_EXPRESSION] == null)
        {
            contractCriteria.SortExpression = CommonConstants.CON_CONTRACTCODE;
        }
        else
        {
            contractCriteria.SortExpression = Session[SessionNames.CONTRACT_PREVIOUS_SORT_EXPRESSION].ToString();
        }

        // setting page number according to which record will fetch.
        if (Session[SessionNames.CONTRACT_CURRENTPAGEINDEX] != null)
        {
            contractCriteria.PageNumber = Convert.ToInt32(Session[SessionNames.CONTRACT_CURRENTPAGEINDEX].ToString());
        }

        // setting sort direction according to which record will fetch.
        if (Session[SessionNames.CONTRACT_SORTDIRECTIONFORBINDING] == null)
        {
            contractCriteria.Direction = DESCENDING;
            GridViewSortDirection = SortDirection.Descending;
        }
        else
            contractCriteria.Direction = Session[SessionNames.CONTRACT_SORTDIRECTIONFORBINDING].ToString();

        // setting Document name according to which record will fetch.
        if (Session[SessionNames.CONTRACT_DOCUMENTNAME] != null)
            contractCriteria.DocumentName = Session[SessionNames.CONTRACT_DOCUMENTNAME].ToString();

        // setting contract type according to which record will fetch.
        if (Session[SessionNames.CONTRACT_CONTRACTTYPE] != null)
            contractCriteria.ContractTypeID = Convert.ToInt32(Session[SessionNames.CONTRACT_CONTRACTTYPE].ToString());

        // setting contract status according to which record will fetch.
        if (Session[SessionNames.CONTRACT_CONTRACTSTATUS] != null)
            contractCriteria.ContractStatus = Convert.ToInt32(Session[SessionNames.CONTRACT_CONTRACTSTATUS].ToString());

        // setting client name according to which record will fetch.
        if (Session[SessionNames.CONTRACT_CLIENTNAME] != null)
            contractCriteria.ClientNameId = Convert.ToInt32(Session[SessionNames.CONTRACT_CLIENTNAME].ToString());

        return contractCriteria;
    }

    /// <summary>
    /// Set the values in FILter controls After come back from Add contract page.
    /// </summary>
    private void SetFilterControlsValue()
    {
        // setting Document name according to which record will fetch.
        if (Session[SessionNames.CONTRACT_DOCUMENTNAME] != null)
            txtDocumentName.Text = Session[SessionNames.CONTRACT_DOCUMENTNAME].ToString();

        // setting contract type according to which record will fetch.
        if (Session[SessionNames.CONTRACT_CONTRACTTYPE] != null)
            ddlContractType.SelectedValue = Session[SessionNames.CONTRACT_CONTRACTTYPE].ToString();

        // setting contract status according to which record will fetch.
        if (Session[SessionNames.CONTRACT_CONTRACTSTATUS] != null)
            ddlContractStatus.SelectedValue = Session[SessionNames.CONTRACT_CONTRACTSTATUS].ToString();

        // setting client name according to which record will fetch.
        if (Session[SessionNames.CONTRACT_CLIENTNAME] != null)
            ddlClientName.SelectedValue = Session[SessionNames.CONTRACT_CLIENTNAME].ToString();

    }

    /// <summary>
    /// Remove the filter session values.
    /// </summary>
    private void RemoveFilterSession()
    {
        Session.Remove(SessionNames.CONTRACT_CONTRACTTYPE);
        Session.Remove(SessionNames.CONTRACT_CONTRACTSTATUS);
        Session.Remove(SessionNames.CONTRACT_DOCUMENTNAME);
        Session.Remove(SessionNames.CONTRACT_CLIENTNAME);
        Session.Remove(SessionNames.CONTRACT_PREVIOUS_SORT_EXPRESSION);
        Session.Remove(SessionNames.CONTRACT_CURRENTPAGEINDEX);
        Session.Remove(SessionNames.CONTRACT_SORTDIRECTIONFORBINDING);
    }

    /// <summary>
    /// To get Resource plan details by project id.
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns></returns>
    private BusinessEntities.RaveHRCollection GetRPDetailsByProjectId(int projectId)
    {
        try
        {
            Rave.HR.BusinessLayer.Projects.ResourcePlan resourcePlanBL = new Rave.HR.BusinessLayer.Projects.ResourcePlan();
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            //define the object of ResourcePlan & assign ProjectId,RPApprovalStatusId properties to object.
            BusinessEntities.ResourcePlan resourcePlanDetails = new BusinessEntities.ResourcePlan();
            resourcePlanDetails.ProjectId = projectId;
            resourcePlanDetails.RPApprovalStatusId = Convert.ToInt32(MasterEnum.ResourcePlanApprovalStatus.Approved);

            //call the business layer method to get RP of a project.
            raveHRCollection = resourcePlanBL.GetResourcePlanByProjectIdForContract(resourcePlanDetails);
            return raveHRCollection;
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_CONTRACT_SUMMARY, 
                "GetRPDetailsByProjectId", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    ///  Fills the Department dropdown
    /// </summary>
    private void FillClientNameDropDown()
    {
        // Initialise Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

        // Initialise Business layer object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();

        try
        {
            // Call the Business layer method
            raveHRCollection = master.GetClientNameBL();

            if (raveHRCollection != null)
            {
                // Assign the data source to dropdown
                ddlClientName.DataSource = raveHRCollection;

                ddlClientName.DataTextField = CommonConstants.DDL_DataTextField;
                ddlClientName.DataValueField = CommonConstants.DDL_DataValueField;

                // Bind the data to dropdown
                ddlClientName.DataBind();

                // Default value of dropdown is "Select"
                // 27832,27833 - Ambar - Start - Commented following line and added correct one.
                // ddlClientName.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);
                ddlClientName.Items.Insert(CommonConstants.ZERO, new ListItem(SELECTONE, ZERO));
                // 27832,27833 - Ambar - End
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
                CLASS_NAME_CONTRACT_SUMMARY, "FillClientNameDropDown",
                EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
        }
    }
    

    #endregion

}
