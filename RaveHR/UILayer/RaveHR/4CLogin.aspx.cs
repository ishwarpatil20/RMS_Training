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
using Common.Constants;
using Common.AuthorizationManager;
using System.Collections.Generic;

public partial class _4CLogin : BaseClass
{
    public string UserName
    {
        get { return this.txtUsername.Text; }
    }

    List<string> lstRights;
    const string CLASS_NAME = "_4CLogin.aspx.cs";

    protected void Page_Load(object sender, EventArgs e)
    {
        //UserName = txtUsername.Text;
        AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
        string UserRaveDomainId = objRaveHRAuthorizationManager.getLoggedInUser();
        UserRaveDomainId = UserRaveDomainId.Replace("co.in", "com");

        Page.Form.DefaultButton = btnLogin.UniqueID;

        if (!IsPostBack)
        {
            btnLogin.PostBackUrl = "~/4C_Add4CFeedback.aspx";
        }

        lstRights = CheckAccessRights(UserRaveDomainId);

        if (lstRights.Any(o => o.ToString() == MasterEnum.FourCRole.FOURCADMIN.ToString()) || (UserRaveDomainId.ToLower() == "sawita.kamat@rave-tech.com" || UserRaveDomainId.ToLower() == "sawita.kamat@northgateps.com"))
        {
            lblMessage.Visible = false;
            divLogin.Visible = true;
        }
        else
        {
            lblMessage.Visible = true;
            divLogin.Visible = false;
            lblMessage.Text = "You don’t have the rights to access this module. Please contact the website administrator.";
            lblMessage.Style["color"] = "red";
            return;
        }
    }

    protected void rdbl4COption_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(rdbl4COption.SelectedValue == "1")
        {
            btnLogin.PostBackUrl = "~/4C_Add4CFeedback.aspx";
        }
        else if (rdbl4COption.SelectedValue == "2")
        {
            btnLogin.PostBackUrl = "~/4C_Reports.aspx";
        }
        else if (rdbl4COption.SelectedValue == "3")
        {
            btnLogin.PostBackUrl = "~/ViewEmp4C.aspx";
        }
    }


    private List<string> CheckAccessRights(string emailId)
    {
        lstRights = new List<string> { };
        //DataSet ds = null;
        try
        {
            int loginEmpId = 0;
            Rave.HR.BusinessLayer.FourC.FourC fourCBAL = new Rave.HR.BusinessLayer.FourC.FourC();
            lstRights = fourCBAL.Check4CLoginRights(emailId, ref loginEmpId);
            ViewState["LoginEmpId"] = loginEmpId;
            return lstRights;
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "CheckAccessRights", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
        return lstRights;
    }
}
