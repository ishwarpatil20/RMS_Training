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

public partial class SkillsList : BaseClass
{
    private const string CLASS_NAME = "SkillsList.aspx";
    public StringBuilder value;// = new StringBuilder();
    public StringBuilder text = new StringBuilder();
    public StringBuilder lstSelectedEmployeeId;
    private string reportingToValue;
    //public int pSkillsID = 0;

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
                btnSelect.Attributes.Add("onclick", "return Validate();");
                if (!IsPostBack)
                {
                    value = new StringBuilder();
                    PopulateData();
                    reportingToValue = null;

                    //if (Request.QueryString["SkillsID"] != null)
                    //{
                    //    pSkillsID = Convert.ToInt32(Request.QueryString["SkillsID"]);
                    //}
                    if (Request.QueryString["PageName"] != null)
                    {
                        hidPageName.Value = Request.QueryString["PageName"];
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "Page_Load", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    private void PopulateData()
    {
        try
        {
            Rave.HR.BusinessLayer.MRF.MRFDetail MrfBL = new Rave.HR.BusinessLayer.MRF.MRFDetail();
            BusinessEntities.RaveHRCollection collObj = new BusinessEntities.RaveHRCollection();

            string SkillName = string.Empty;
            if (!string.IsNullOrEmpty(txtSkillsName.Text))
            {
                SkillName = txtSkillsName.Text;
            }
            else
            {
                SkillName = "";
            }

            collObj = MrfBL.GetSkillsList(SkillName);


            grdvListofSkill.DataSource = collObj;
            grdvListofSkill.DataBind();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "PopulateData", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    
    protected void ChangePage(object sender, CommandEventArgs e)
    {
        try
        {
            GridViewRow gvrPager = grdvListofSkill.BottomPagerRow;
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
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "ChangePage", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdvListofSkill_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (e.NewPageIndex != -1)
            {
                grdvListofSkill.PageIndex = e.NewPageIndex;
            }
            PopulateData();

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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvListofSkill_PageIndexChanging", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void txtPages_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox txtPages = (TextBox)sender;

            if (txtPages.Text != "" && int.Parse(txtPages.Text) != 0 && Convert.ToInt32(txtPages.Text) <= Convert.ToInt32(Session["pageCount"].ToString()))
            {
                grdvListofSkill.PageIndex = Convert.ToInt32(txtPages.Text) - 1;
                Session["currentPageIndex"] = txtPages.Text;
            }
            else
            {
                return;
            }

            txtPages.Text = Session["currentPageIndex"].ToString();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "txtPages_TextChanged", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void grdvListofSkill_RowDataBound(object sender, GridViewRowEventArgs e)
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
                //if (ViewState["str"] != null)
                //{
                //    strFunctionalManager = hidFunctionalManager.Text;
                //}
                chk.Attributes.Add("onclick", "FnCheck('" + chk.ClientID + "','" + hidSkillsCount.ClientID + "')");
            }

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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "grdvListofSkill_RowDataBound", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
    
    protected void btnSelect_Click(object sender, EventArgs e)
    {
        try
        {
            value = new StringBuilder();
            lstSelectedEmployeeId = new StringBuilder();
            StringBuilder Result = null;

            if (ViewState["SelectedEmployee"] != null)
            {
                value = (StringBuilder)ViewState["SelectedEmployee"];
                lstSelectedEmployeeId = (StringBuilder)ViewState["SelectedEmployeeId"];
            }

            for (int i = 0; i < grdvListofSkill.Rows.Count; i++)
            {
                CheckBox chkkkk = (CheckBox)grdvListofSkill.Rows[i].Cells[0].FindControl("chkSelect");
                HiddenField hfSkillsID = (HiddenField)grdvListofSkill.Rows[i].Cells[0].FindControl("hfSkillsID");

                if (chkkkk.Checked)
                {
                    string EmdID = hfSkillsID.Value;       //grdvListofSkill.Rows[i].Cells[1].Text;
                    string EmpName = grdvListofSkill.Rows[i].Cells[1].Text;

                    int nRemoveSpecialChar = EmpName.IndexOf("'");
                    if (nRemoveSpecialChar != -1)
                    {
                        EmpName = EmpName.Replace("'", "");
                    }
                    
                    value.Append(EmdID + "_" + EmpName);
                    value.Append(",");

                    string s;
                    string[] words;
                    Result = null;
                    
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

                    ViewState["SelectedEmployee"] = value;
                    lstSelectedEmployeeId.Append(EmdID);
                    lstSelectedEmployeeId.Append(",");
                    ViewState["SelectedEmployeeId"] = lstSelectedEmployeeId;
                }
            }
            ViewState["SelectedEmployee"] = null;
            ViewState["SelectedEmployee"] = Result;
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "SelectJS", "returnValue='" + Result.ToString() + "';", true);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnSelect_Click", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnListofSkills_Click(object sender, EventArgs e)
    {
        try
        {
            PopulateData();
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnListofSkills_Click", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
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

                    for (int j = 0; j < grdvListofSkill.Rows.Count; j++)
                    {
                        string EmdID = grdvListofSkill.Rows[j].Cells[1].Text;
                        if (tempEmpId.Trim() == EmdID.Trim())
                        {
                            CheckBox chk = (CheckBox)grdvListofSkill.Rows[j].Cells[0].FindControl("chkSelect");
                            chk.Checked = true;
                        }
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "SelectCheckedEmployee", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            string delimStr = "_,";
            char[] delimiter = delimStr.ToCharArray();
            string varPurpose = "";
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
            value = new StringBuilder();
            text = new StringBuilder();

            ViewState["SelectedEmployee"] = null;

            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "CloseJS",
                "jQuery.modalDialog.getCurrent().close();jQuery.modalDialog.getCurrent().postMessageToParent('" + varPurpose + "');", true);
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnClose_Click", EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
}
