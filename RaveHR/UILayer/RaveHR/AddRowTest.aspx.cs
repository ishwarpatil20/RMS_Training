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

public partial class AddRowTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void gvSkillCriteria_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //DropDownList ddlSkill = (DropDownList)e.Row.FindControl("ddlSkill");
                //RadioButtonList rblMandatoryOptional = (RadioButtonList)e.Row.FindControl("rblMandatoryOptional");

                //HiddenField HFSkill = (HiddenField)e.Row.FindControl("HFSkill");
                //HiddenField HFMandatoryOptional = (HiddenField)e.Row.FindControl("HFMandatoryOptional");

                //SkillCollection = this.GetSkills();
                //ddlSkill.DataSource = SkillCollection;
                //ddlSkill.DataTextField = Common.CommonConstants.DDL_DataTextField;
                //ddlSkill.DataValueField = Common.CommonConstants.DDL_DataValueField;
                //ddlSkill.DataBind();
                //ddlSkill.Items.Insert(CommonConstants.ZERO, CommonConstants.SELECT);

                //if (!String.IsNullOrEmpty(HFSkill.Value))
                //{
                //    ddlSkill.Items.FindByText(HFSkill.Value).Selected = true;
                //}

                //if (!String.IsNullOrEmpty(HFMandatoryOptional.Value))
                //{
                //    rblMandatoryOptional.SelectedValue = Convert.ToString(HFMandatoryOptional.Value);
                //}
            }
        }
        catch (Exception ex)
        {
            //RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "gvSkillCriteria_RowDataBound", EventIDConstants.RAVE_EMP_SKILL_SEARCH_PRESENTATION_LAYER);
            //LogErrorMessage(objEx);
        }
    }

    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        try
        {
            //DropDownList ddlSkill = (DropDownList)gvSkillCriteria.Rows[(gvSkillCriteria.Rows.Count - 1)].FindControl("ddlSkill");
            //if (ddlSkill.SelectedItem.Text == CommonConstants.SELECT)
            //{
            //    lblError.Text = "Please select a Skill.";
            //    return;
            //}
            //else
            //{
            //    DefaultGridView();
            //}
        }
        catch (Exception ex)
        {
        //    RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "btnAddRow_Click", EventIDConstants.RAVE_EMP_SKILL_SEARCH_PRESENTATION_LAYER);
        //    LogErrorMessage(objEx);
        }
    }


}
