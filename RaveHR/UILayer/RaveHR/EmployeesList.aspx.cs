using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Text;

public partial class EmployeesList : BaseClass
{
    /// <summary>
    /// Defines a constant for Page Name used in each catch block 
    /// </summary>
    private const string CLASS_NAME = "EmployeesList.aspx";

    // 28512-Ambar-Start-02092011
    //public static StringBuilder value = new StringBuilder();
    //public static StringBuilder text = new StringBuilder();
    //Issue ID : 38237 Mahendra Start
    public StringBuilder value;// = new StringBuilder();
    public StringBuilder text = new StringBuilder();
    public StringBuilder lstSelectedEmployeeId;

    //private string reportingToValue;
    //Issue ID : 38237 Mahendra End
    // 28512-Ambar-End-02092011

    //private static string reportingToValue;
    private string reportingToValue;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!ValidateURL())
        {
            Response.Redirect(CommonConstants.INVALIDURL, false);
        }
        else
        {
            btnSelect.Attributes.Add("onclick", "return Validate();");
            if (!IsPostBack)
            {
                value = new StringBuilder();
                //Aarohi : Issue 28572 : 11/01/2012 : Start
                //if (Request.QueryString["EMPId"] != null)
                //{
                //    hidEMPId.Text = DecryptQueryString("EMPId");
                //}
                //Aarohi : Issue 28572 : 11/01/2012 : End

                PopulateData();
                //refreshing the static variable if page is loaded for the first time
                reportingToValue = null;
                if (Request.QueryString["str"] != null)
                {
                    hidFunctionalManager.Text = DecryptQueryString("str");
                }
                if (Request.QueryString["hidFldValue"] != null)
                {
                    reportingToValue = DecryptQueryString("hidFldValue");
                    this.SelectCheckedEmployee(reportingToValue);
                }
                if (Request.QueryString["PageName"] != null)
                {
                    hidPageName.Value = Request.QueryString["PageName"];
                }
            }
        }
    }

    private void PopulateData()
    {
        try
        {
            Rave.HR.BusinessLayer.Employee.Employee empBL = new Rave.HR.BusinessLayer.Employee.Employee();
            BusinessEntities.RaveHRCollection collObj = new BusinessEntities.RaveHRCollection();


            //Aarohi : Issue 28572(CR) : 05/01/2012 : Start
            //Commented the below line of code to add a new parameter for employeeId
            //commented for 4C report
            //collObj = empBL.GetEmployeesList(txtResourceName.Text.Trim());





            //if (hidEMPId.Text != null || hidEMPId.Text == string.Empty || hidEMPId.Text == "")
            //if (hidEMPId.Text != null && hidEMPId.Text != string.Empty || hidEMPId.Text != "")
            //{
            //    collObj = empBL.GetEmployeeListForFMRM(txtResourceName.Text.Trim(), hidEMPId.Text);
            //}
            //else
            //{
            //    collObj = empBL.GetEmployeesList(txtResourceName.Text.Trim());
            //}
            //Aarohi : Issue 28572(CR) : 05/01/2012 : End


            //CASES to show employee 
            //First to show all sub-ordinate 
            if (DecryptQueryString("VIEW") != null && !string.IsNullOrEmpty(DecryptQueryString("VIEW")))
            {
                collObj = empBL.GetEmployeesView(DecryptQueryString("VIEW"), int.Parse(DecryptQueryString("EmpId")), txtResourceName.Text.Trim());
            }
            else
            {
                collObj = empBL.GetEmployeesList(txtResourceName.Text.Trim());
            }

            grdvListofEmployees.DataSource = collObj;
            grdvListofEmployees.DataBind();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "PopulateData", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdvListofEmployees_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {

        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvListofEmployees_Sorting", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void ChangePage(object sender, CommandEventArgs e)
    {
        GridViewRow gvrPager = grdvListofEmployees.BottomPagerRow;
        TextBox txtPages = (TextBox)gvrPager.Cells[0].FindControl("txtPages");

        switch (e.CommandName)
        {
            case "Previous":
                Session["currentPageIndex"] = Convert.ToInt32(txtPages.Text) - 1;
                txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) - 1);
                break;

            case "Next":
                Session["currentPageIndex"] = Convert.ToInt32(txtPages.Text) + 1;
                txtPages.Text = Convert.ToString(Convert.ToInt32(txtPages.Text) + 1);
                break;
        }

        //BindGrid();
    }

    protected void grdvListofEmployees_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //Change the page index on page index changing event and bind the grid
            if (e.NewPageIndex != -1)
            {
                grdvListofEmployees.PageIndex = e.NewPageIndex;
            }
            PopulateData();

            //if (Session["SelectedEmployeeId"] != null)
            //{
            //    reportingToValue = Session["SelectedEmployeeId"].ToString();
            //}
            if (ViewState["SelectedEmployeeId"] != null)
            {
                reportingToValue = ViewState["SelectedEmployeeId"].ToString();
            }


            if (reportingToValue != null && reportingToValue != string.Empty && reportingToValue.Length > 0)
            {
                this.SelectCheckedEmployee(reportingToValue);
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvListofEmployees_PageIndexChanging", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void txtPages_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txtPages = (TextBox)sender;

            //Check for Paging text box is not empty, non-zero, and out of range paging no.
            if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0 && Convert.ToInt32(txtPages.Text) <= Convert.ToInt32(Session["pageCount"].ToString()))
            {
                grdvListofEmployees.PageIndex = Convert.ToInt32(txtPages.Text) - 1;
                Session["currentPageIndex"] = txtPages.Text;
            }
            else
            {
                return;
            }

            //Bind the grid on paging USP_Projects_GetUnfilteredProjectSummaryData_Paging
            //BindGrid();
            txtPages.Text = Session["currentPageIndex"].ToString();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "txtPages_TextChanged", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdvListofEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string strFunctionalManager = string.Empty;
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.textDecoration='underline';";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelect");
                if (ViewState["str"] != null)
                {
                    strFunctionalManager = hidFunctionalManager.Text;
                }
                //Aarohi : Issue 28572(CR) : 05/01/2012 : Start
                //Added a new parameter for EMPId
                //chk.Attributes.Add("onclick", "FnCheck('" + chk.ClientID + "','" + hidEmployeeCount.ClientID + "','" + strFunctionalManager + "','" + hidEMPId.ClientID + "')");
                chk.Attributes.Add("onclick", "FnCheck('" + chk.ClientID + "','" + hidEmployeeCount.ClientID + "','" + strFunctionalManager + "')");
                //Aarohi : Issue 28572(CR) : 05/01/2012 : End
            }

            //if (Session["SelectedEmployeeId"] != null)
            //{
            //    reportingToValue = Session["SelectedEmployeeId"].ToString();
            //}
            if (ViewState["SelectedEmployeeId"] != null)
            {
                reportingToValue = ViewState["SelectedEmployeeId"].ToString();
            }

            if (reportingToValue != null && reportingToValue != string.Empty && reportingToValue.Length > 0)
            {
                this.SelectCheckedEmployee(reportingToValue);
            }


        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvListofEmployees_RowDataBound", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        try
        {
            //Issue ID : 38237 Mahendra Start
            value = new StringBuilder();
            lstSelectedEmployeeId = new StringBuilder();
            StringBuilder Result = null;

            //if (Session["SelectedEmployee"] != null)
            //{
            //    //lstSelectedEmployee = (List<string>)Session["SelectedEmployee"];
            //    value = (StringBuilder)Session["SelectedEmployee"];
            //    lstSelectedEmployeeId = (StringBuilder)Session["SelectedEmployeeId"];
            //}

            if (ViewState["SelectedEmployee"] != null)
            {
                //lstSelectedEmployee = (List<string>)Session["SelectedEmployee"];
                value = (StringBuilder)ViewState["SelectedEmployee"];
                lstSelectedEmployeeId = (StringBuilder)ViewState["SelectedEmployeeId"];
            }

            //Issue ID : 38237 Mahendra End

            for (int i = 0; i < grdvListofEmployees.Rows.Count; i++)
            {
                CheckBox chkkkk = (CheckBox)grdvListofEmployees.Rows[i].Cells[0].FindControl("chkSelect");

                if (chkkkk.Checked)
                {
                    //Get the EmployeeID and EmployeeName
                    string EmdID = grdvListofEmployees.Rows[i].Cells[1].Text;
                    string EmpName = grdvListofEmployees.Rows[i].Cells[3].Text;


                    //Issue ID : 28572 Mahendra START 
                    //when any employee name contain special char like Emp Name Elton D'Rego , which give JS error so that has been removed from Emp Name.
                    int nRemoveSpecialChar = EmpName.IndexOf("'");
                    if (nRemoveSpecialChar != -1)
                    {
                        EmpName = EmpName.Replace("'", "");
                    }
                    //Issue ID : 28572 Mahendra END

                    value.Append(EmdID + "_" + EmpName);
                    value.Append(",");

                    //Issue ID : 38237 Mahendra Start
                    string s;
                    string[] words;
                    Result = null;
                    //Mahendra Remove duplicate value from string
                    //if (Session["SelectedEmployee"] != null)
                    //{
                    //    value = (StringBuilder)Session["SelectedEmployee"];
                    //}
                    if (ViewState["SelectedEmployee"] != null)
                    {
                        value = (StringBuilder)ViewState["SelectedEmployee"];
                    }

                    s = value.ToString();
                    words = s.Split(',');

                    Result = new StringBuilder();
                    for (int iRow = 0; iRow < words.Length; iRow++)
                    {
                        if (Result.ToString().IndexOf(words[iRow]) == -1)
                        {
                            Result.Append(words[iRow].ToString()).Append(",");
                        }
                    }

                    //Session["SelectedEmployee"] = value;
                    ViewState["SelectedEmployee"] = value;
                    lstSelectedEmployeeId.Append(EmdID);
                    lstSelectedEmployeeId.Append(",");
                    //Session["SelectedEmployeeId"] = lstSelectedEmployeeId;
                    ViewState["SelectedEmployeeId"] = lstSelectedEmployeeId;

                    //Issue ID : 38237 Mahendra END

                }
            }
            //Response.Write("<script>returnValue='" + Result.ToString() + "';</script>");

            //Issue ID : 38237 Mahendra Start
            //Session["SelectedEmployee"] = null;
            //Session["SelectedEmployee"] = Result;
            ViewState["SelectedEmployee"] = null;
            ViewState["SelectedEmployee"] = Result;
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "SelectJS", "returnValue='" + Result.ToString() + "';", true);
            //Issue ID : 38237 Mahendra End
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnSelect_Click", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnListofInternalREsources_Click(object sender, EventArgs e)
    {
        try
        {
            //On Click of search button once again the grid should get populate based on search value.
            PopulateData();
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnListofInternalREsources_Click", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    private void SelectCheckedEmployee(string empid)
    {
        try
        {
            if (empid != "")
            {
                string[] empidarray = empid.Split(',');

                for (int i = 0; i < empidarray.Length; i++)
                {
                    string tempEmpId = empidarray[i];

                    for (int j = 0; j < grdvListofEmployees.Rows.Count; j++)
                    {
                        //Get the EmployeeID
                        string EmdID = grdvListofEmployees.Rows[j].Cells[1].Text;
                        if (tempEmpId.Trim() == EmdID.Trim())
                        {
                            CheckBox chk = (CheckBox)grdvListofEmployees.Rows[j].Cells[0].FindControl("chkSelect");
                            chk.Checked = true;
                        }
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "SelectCheckedEmployee", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    // 28512-Ambar-Start-02092011
    protected void btnClose_Click(object sender, EventArgs e)
    {
        //Aarohi : Issue 30885 : 14/12/2011 : Start
        //Code to seperate Id from Name for displaying in the "Resource Name" text box
        string delimStr = "_,";
        char[] delimiter = delimStr.ToCharArray();
        //string varPurpose = value.ToString();
        string varPurpose = "";
        //if (Session["SelectedEmployee"] != null)
        //{
        //    varPurpose = Session["SelectedEmployee"].ToString();
        //}
        if (ViewState["SelectedEmployee"] != null)
        {
            varPurpose = ViewState["SelectedEmployee"].ToString();
        }


        string[] varPurposeSplit = null;
        ArrayList arr = new ArrayList();
        varPurposeSplit = varPurpose.Split(delimiter);
        for (int i = 1; i < varPurposeSplit.Length; i = i + 2)
        {
            arr.Add(varPurposeSplit[i]);
        }
        Session[SessionNames.PURPOSE] = arr;
        //Aarohi : Issue 30885 : 14/12/2011 : End
        value = new StringBuilder();
        text = new StringBuilder();

        //Session["SelectedEmployee"] = null;
        ViewState["SelectedEmployee"] = null;

        //Sanju:Issue Id 50201 
        //Passed value to window.opener in order to retieve it in another page (chrome issue)
        ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "CloseJS",
            "jQuery.modalDialog.getCurrent().close();jQuery.modalDialog.getCurrent().postMessageToParent('" + varPurpose + "');", true);
        //Response.Write("<script>window.close();</script>");
    }
    // 28512-Ambar-End-02092011
}
