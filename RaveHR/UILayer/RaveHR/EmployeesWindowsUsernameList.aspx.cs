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
using System.DirectoryServices;
using Common;
using System.Linq;
using System.Collections.Generic;

public partial class EmployeesWindowsUsernameList : BaseClass
{
    List<EmployeeUsername> EmployeeUsernameList = new List<EmployeeUsername>();

    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (!ValidateURL())
        {
            Response.Redirect(CommonConstants.INVALIDURL, false);
        }
        else
        {
            //btnSelect.Attributes.Add("onclick", "return Validate();");
            btnSelect.Attributes.Add("onclick", "return RadioValidation();");
            if (!Page.IsPostBack)
                this.BindGrid();
        }
    }
    

    private List<EmployeeUsername> GetDomainUsers()
    {
        string strUserEmail = string.Empty;
        string name = string.Empty;
        EmployeeUsernameList = new List<EmployeeUsername>();

        try
        {
            DirectoryEntry searchRoot = new DirectoryEntry(Common.AuthorizationManagerConstants.DIRECTORYSERVICE);

            DirectorySearcher search = new DirectorySearcher(searchRoot);

            //string query = "(|(objectCategory=group )(objectCategory=user))";
            //string query = "(SAMAccountName=" + strUserName + ")"; 
            //string query = "(&(objectClass=user)(objectCategory=Person))";
            string query = "(&(objectClass=user)(objectCategory=contact))";
            //venkatesh : start 16-Sep, Exceed the LDAP user list more than 1000
            search.PageSize = 10000;
            //venkatesh : start 16-Sep, Exceed the LDAP user list more than 1000
            search.Filter = query;
            SearchResult result;

            SearchResultCollection resultCol = search.FindAll();
            //search.
            //vrp st

            
            //vrp s




            if (resultCol != null)
            {

                for (int counter = 0; counter < resultCol.Count ; counter++)
                {

                    result = resultCol[counter];

                    //if (result.Properties.Contains("showinaddressbook"))
                    //
                    if (result.Properties["displayname"] != null && result.Properties["displayname"].Count > 0)
                    {
                        EmployeeUsername empEmail = new EmployeeUsername();


                        strUserEmail = result.Properties["samaccountname"][0].ToString();


                        name = result.Properties["displayname"][0].ToString();
                        empEmail.Name = name;
                        //empEmail.EmailID = strUserEmail;
                        empEmail.Username = strUserEmail;

                        EmployeeUsernameList.Add(empEmail);
                    }
                    if (counter >= 999)
                    {
                        string str = "";
                    }
                }

            }

            EmployeeUsernameList.Sort(delegate(EmployeeUsername e1, EmployeeUsername e2) { return e1.Name.CompareTo(e2.Name); });

        }

        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "EmployeesEmailList.aspx.cs", "GetDomainUsers", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

        return EmployeeUsernameList;
    }
    [Serializable]
    private class EmployeeUsername
    {
        public string Name { get; set; }
        public string Username { get; set; }

        public EmployeeUsername() { }

    }

    private void BindGrid()
    {
        EmployeeUsernameList = this.GetDomainUsers();
        ViewState["EmpList"] = EmployeeUsernameList;
        grvEmployeeEmail.DataSource = EmployeeUsernameList;
        grvEmployeeEmail.DataBind();
    }

    protected void grvEmployeeEmail_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (e.NewPageIndex != -1)
            {
                // Assign the new page index
                grvEmployeeEmail.PageIndex = e.NewPageIndex;
            }

            // Bind the grid as per new page index.
            BindGrid();
        }

      //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "EmployeesEmailList.aspx.cs", "gvListOfMrf_PageIndexChanging", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    //protected void chkSelect_CheckChanged(object sender, EventArgs e)
    //{
    //    GridViewRow grvRow = GetRow(sender);
    //    CheckBox chkBox = (CheckBox)grvRow.FindControl("chkSelect");

    //    //save the value for index
    //    index = grvRow.RowIndex;

    //    foreach (GridViewRow row in grvEmployeeEmail.Rows)
    //    {
    //        CheckBox checkBox = (CheckBox)grvEmployeeEmail.Rows[row.RowIndex].FindControl("chkSelect");
    //        if (grvRow.RowIndex != row.RowIndex)
    //            if (checkBox.Checked == true)
    //            {
    //                checkBox.Checked = false;
    //            }
    //    }

    //}

    private GridViewRow GetRow(object sender)
    {
        return (GridViewRow)((Control)sender).NamingContainer;
    }


    protected void btnSelect_Click(object sender, EventArgs e)
    {
        string selectedValue = Request.Form["rbEmailList"];

        try
        {
            //Sanju:Issue Id 50201 
            //Passed value to window.opener in order to retieve it in another page (chrome issue)
            //Umesh: Issue 'Modal Popup issue in chrome' Starts
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "SelectJS", "jQuery.modalDialog.getCurrent().postMessageToParent('" + selectedValue + "');jQuery.modalDialog.getCurrent().close();", true);
            //Umesh: Issue 'Modal Popup issue in chrome' Ends
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "EmployeesEmailList.aspx.cs", "btnSelect_Click", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    protected void grvEmployeeEmail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //CheckBox chk = (CheckBox)e.Row.FindControl("chkSelect");

            //chk.Attributes.Add("onclick", "FnCheck('" + chk.ClientID + "','" + hidEmployeeCount.ClientID + "')");
        }
    }

    /// <summary>
    /// Filter the grid data on the basis of text value criteria.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// Added by : Yagendra Sharnagat
    protected void btnSearchEmpName_Click(object sender, EventArgs e)
    {
        try
        {
            //Set the value of grid datasource to employee list object.
            EmployeeUsernameList = ViewState["EmpList"] as List<EmployeeUsername>;

            if (EmployeeUsernameList != null)
            {
                //Filter data on the basis of text value of textbox.
                var filterdEmpList = from emp in EmployeeUsernameList
                                     where (emp.Name.ToLower().Contains(tbxEmpName.Text.Trim().ToLower()))
                                     select emp;

                //Fill the grid with filterd data.
                grvEmployeeEmail.DataSource = filterdEmpList.ToList();
                grvEmployeeEmail.DataBind();

                if (filterdEmpList.ToList().Count() <= 0)
                {
                    btnSelect.Enabled = false;
                }
                else
                {
                    btnSelect.Enabled = true;
                }
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "EmployeesEmailList.aspx.cs", "btnSearchEmpName_Click", EventIDConstants.RAVE_HR_EMPLOYEE_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

    }

}
