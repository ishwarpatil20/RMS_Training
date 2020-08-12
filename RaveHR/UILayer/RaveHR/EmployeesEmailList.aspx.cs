using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;
using Common;

public partial class EmployeesEmailList : BaseClass
{
    List<EmployeeEmail> employeeEmailList = new List<EmployeeEmail>();
    
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

    private List<EmployeeEmail> GetDomainUsers()
    {
        string strUserEmail = string.Empty;
        string name = string.Empty;
        employeeEmailList = new List<EmployeeEmail>();

        try
        {
            DirectoryEntry searchRoot = new DirectoryEntry(Common.AuthorizationManagerConstants.DIRECTORYSERVICE);

            DirectorySearcher search = new DirectorySearcher(searchRoot);

            //string query = "(|(objectCategory=group )(objectCategory=user))";
            //string query = "(SAMAccountName=" + strUserName + ")"; 
            string query = "(&(objectClass=user)(objectCategory=Person))";
            //venkatesh : start 16-Sep, Exceed the LDAP user list more than 1000
            search.PageSize = 10000;
            //venkatesh : start 16-Sep, Exceed the LDAP user list more than 1000
            search.Filter = query;

            SearchResult result;

            SearchResultCollection resultCol = search.FindAll();
            //search.

            if (resultCol != null)
            {

                for (int counter = 0; counter < resultCol.Count; counter++)
                {

                    result = resultCol[counter];

                    //if (result.Properties["samaccountname"][0].ToString().Trim().ToLower() == "sachin.jadhav")
                    //{
                    //    name = result.Properties["displayname"][0].ToString();
                    //    //empEmail.Name = name;
                    //}

                    //if (result.Properties.Contains("samaccountname"))
                    if (result.Properties.Contains("showinaddressbook"))
                    {
                        EmployeeEmail empEmail = new EmployeeEmail();
                        //if (!result.Properties.Contains("proxyaddresses"))
                        //strUserEmail = result.Properties["mail"][0].ToString();

                        //result = resultCol[counter];
                        //result.Properties["givenName"][0];
                        //result.Properties["initials"][0];
                        //strUserEmail = result.Properties["CN"][0].ToString();
                        //strUserEmail = result.Properties["samaccountname"][0].ToString();

                        strUserEmail = result.Properties["mail"][0].ToString();


                        name = result.Properties["displayname"][0].ToString();
                        empEmail.Name = name;
                        empEmail.EmailID = strUserEmail;

                        employeeEmailList.Add(empEmail);
                    }

                }

            }

            employeeEmailList.Sort(delegate(EmployeeEmail e1, EmployeeEmail e2) { return e1.Name.CompareTo(e2.Name); });
            
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

        return employeeEmailList;
    }
    [Serializable]
    private class EmployeeEmail
    {
        public string Name { get; set; }
        public string EmailID { get; set; }

        public EmployeeEmail(){}

    }

    private void BindGrid()
    {
        employeeEmailList = this.GetDomainUsers();
        ViewState["EmpList"] = employeeEmailList;
        grvEmployeeEmail.DataSource = employeeEmailList;
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
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "SelectJS", "jQuery.modalDialog.getCurrent().postMessageToParent('" + selectedValue.ToLower() + "');jQuery.modalDialog.getCurrent().close();", true);
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
            employeeEmailList = ViewState["EmpList"] as List<EmployeeEmail>;
           
            if (employeeEmailList != null)
            {
                //Filter data on the basis of text value of textbox.
                var filterdEmpList = from emp in employeeEmailList
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
