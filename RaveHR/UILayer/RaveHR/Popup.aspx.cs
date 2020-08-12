using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System;
using Common;
using Rave.HR.BusinessLayer.Common;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Web.UI.WebControls;
using Common.Constants;
using System.Data;
using BusinessEntities;

public partial class Popup : BaseClass
{
    //Declaring COllection class object
    BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
    Rave.HR.BusinessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.BusinessLayer.MRF.MRFDetail();

    StringBuilder value = new StringBuilder();
    StringBuilder text = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!ValidateURL())
        {
            Response.Redirect(CommonConstants.INVALIDURL);
        }
        else
        {
            Response.Expires = 0;
            Response.Cache.SetNoStore();
            Response.AppendHeader("Pragma", "no-cache");

            if (!IsPostBack)
            {
                FillEmployeeGrid();
            }
        }
    }

    private void FillEmployeeGrid()
    {
        //Calling Fill dropdown Business layer method to fill the dropdown
        raveHRCollection = mRFDetail.GetEmployeeBL();

        //Check Collection object is null or not
        if (raveHRCollection != null)
        {
            //Assign DataSource
            gvPopUp.DataSource = raveHRCollection;

            //Bind Dropdown
            gvPopUp.DataBind();
        }
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
       
        for (int i = 0; i < gvPopUp.Rows.Count; i++)
        {
            CheckBox chkkkk = (CheckBox)gvPopUp.Rows[i].Cells[0].FindControl("chk");

            if (chkkkk.Checked)
            {
               // KeyValue<string> keyValue = new KeyValue<string>();
                string Name = gvPopUp.Rows[i].Cells[1].Text;
                value.Append(Name);
                value.Append(",");

                string s = gvPopUp.Rows[i].Cells[2].Text;
                text.Append(s);
                text.Append(",");
            }
        }

        string strScript = "<script>window.opener.document.forms[\'aspnetForm\']." + DecryptQueryString(QueryStringConstants.FIELD).ToString() + ".value = '" + text.Remove(text.Length - 1, 1).ToString() + "'; window.opener.document.forms[\'aspnetForm\']." + DecryptQueryString(QueryStringConstants.FIELDRES).ToString() + ".value = '" + value.Remove(value.Length - 1, 1).ToString() + "'; window.close();</script>";
        Response.Write(strScript);
    }
}
