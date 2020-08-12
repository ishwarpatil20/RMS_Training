//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           EditRPOptions.aspx.cs    
//  Author:         Sameer.Chornele
//  Date written:   27/8/2009/ 12:01:00 PM
//  Description:    This Page comes when user clicks on edit button from project module.
//  Amendments
//  Date                        Who           Ref     Description
//  ----                      -----------     ---     -----------
//  27/8/2009/ 12:01:00 PM  Sameer.Chornele   n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using Common.Constants;

public partial class EditRPOptions : BaseClass
{
    #region Private Field Members

    /// <summary>
    /// define ZERO 
    /// </summary>
    private const string ZERO = "0";

    /// <summary>
    /// define SPLITER
    /// </summary>
    private const string SPLITER = " ";

    /// <summary>
    /// define SELECT
    /// </summary>
    private const string SELECT = "Select";

    /// <summary>
    /// define CLASS_NAME_RP
    /// </summary>
    private const string CLASS_NAME_RP = CommonConstants.EDITRPOPTIONS_PAGE;

    /// <summary>
    /// Define project id
    /// </summary>
    private int ProjectId = 0;

    #endregion Private Field Members

    #region Protected Method

    /// <summary>
    /// Page Load Event Handler.
    /// </summary>
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

                //TODO : need to get projectId from project module.
                ProjectId = int.Parse(DecryptQueryString(QueryStringConstants.PROJECTID.ToString()));
                if (Session[SessionNames.SaveEditedRP] != null)
                {
                    lblMessage.Text = Session[SessionNames.SaveEditedRP].ToString();
                    lblMessage.CssClass = "Messagetext";
                    Session.Remove(SessionNames.SaveEditedRP);
                }

                if (!Page.IsPostBack)
                {
                    //--Authorise User
                    AuthorizeUserForPageView();

                    //Bind the Active RP dropdownlist .
                    BindDropdown(GetActiveOrInactiveResourcePlan(Convert.ToInt32(Common.MasterEnum.ResourcePlanStatus.Active), ProjectId), ddlActiveRP);
                    //Bind the Inactive RP dropdownlist .
                    BindDropdown(GetActiveOrInactiveResourcePlan(Convert.ToInt32(Common.MasterEnum.ResourcePlanApprovalStatus.Rejected), ProjectId), ddlRejectRP);

                    btnEditRP.Attributes.Add("OnClick", "return validateViewButton()");
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "Page_Load", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// actve resource plan dropdown selectedindex change eventhandler.
    /// </summary>
    protected void ddlActiveRP_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblActiveRPDate.Text = GetLastUpdatedDateOfRP(ddlActiveRP.SelectedItem.Value, 1);
        //Aarohi : Issue 31838(CR) : 28/12/2011 : Start
        string value = ddlActiveRP.SelectedItem.Value;
        string delimStr = " ";
        char[] delimiter = delimStr.ToCharArray();
        string var = value.ToString();
        string[] varSplit = null;
        varSplit = var.Split(delimiter);
        Session[SessionNames.RP_ID] = varSplit[0];
        //Aarohi : Issue 31838(CR) : 28/12/2011 : End
    }

    /// <summary>
    /// rejected resource plan dropdown selectedindex change eventhandler.
    /// </summary>
    protected void ddlRejectRP_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblRejectRPDate.Text = GetLastUpdatedDateOfRP(ddlRejectRP.SelectedItem.Value, 1);
    }

    /// <summary>
    /// create resource plan button click eventhandler.
    /// </summary>
    protected void btnCreateRP_Click(object sender, CommandEventArgs e)
    {
        if (rbRPActiveStatus.Checked)
            Response.Redirect(CommonConstants.CREATERP_PAGE + "?" + URLHelper.SecureParameters("ProjectId", ProjectId.ToString()) + "&" + URLHelper.CreateSignature(ProjectId.ToString()));
        else
            Response.Redirect(CommonConstants.CREATERP_PAGE + "?" + URLHelper.SecureParameters("ProjectId", ProjectId.ToString()) + "&" + URLHelper.CreateSignature(ProjectId.ToString()));
    }

    /// <summary>
    /// edit resource plan button click eventhandler.
    /// </summary>
    protected void btnEditRP_Click(object sender, EventArgs e)
    {
        if (rbRPActiveStatus.Checked)
            Response.Redirect(CommonConstants.EDITMAINRP_PAGE + "?" + URLHelper.SecureParameters("RPId", GetLastUpdatedDateOfRP(ddlActiveRP.SelectedValue, 0).ToString()) + "&" + URLHelper.SecureParameters("ProjectId", ProjectId.ToString()) + "&" + URLHelper.CreateSignature(GetLastUpdatedDateOfRP(ddlActiveRP.SelectedValue, 0).ToString(), ProjectId.ToString()));
        else
            Response.Redirect(CommonConstants.EDITMAINRP_PAGE + "?" + URLHelper.SecureParameters("RPId", GetLastUpdatedDateOfRP(ddlRejectRP.SelectedValue, 0).ToString()) + "&" + URLHelper.SecureParameters("ProjectId", ProjectId.ToString()) + "&" + URLHelper.CreateSignature(GetLastUpdatedDateOfRP(ddlRejectRP.SelectedValue, 0).ToString(), ProjectId.ToString()));
    }

    /// <summary>
    /// cancel button click eventhandler.
    /// </summary>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(CommonConstants.PROJECTSUMMARY_PAGE, false);
    }

    #endregion Protected Method

    #region Private method

    /// <summary>
    /// Bind the dropdown list by respective colllection data.
    /// </summary>
    /// <param name="raveHRCollection">Collection of data</param>
    /// <param name="ddl">Dropdownlist which should  be filled</param>
    private void BindDropdown(BusinessEntities.RaveHRCollection raveHRCollection, DropDownList ddl)
    {
        try
        {
            if (raveHRCollection != null)
            {
                ddl.DataSource = raveHRCollection;
                ddl.DataTextField = Common.CommonConstants.DDL_DataTextField;
                ddl.DataValueField = Common.CommonConstants.DDL_DataValueField;
                ddl.DataBind();
            }
            ddl.Items.Insert(0, new ListItem(SELECT, ZERO));
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "BindDropDown", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Get the Last Updated date of RP from resource plan seleceted value.
    /// </summary>
    private string GetLastUpdatedDateOfRP(string dropDownValue, int index)
    {
        try
        {
            string strDateOfRP = string.Empty;

            if (!dropDownValue.Equals(ZERO))
            {
                string[] RPLastUpdatedDate = dropDownValue.Split(Convert.ToChar(SPLITER));
                strDateOfRP = RPLastUpdatedDate[index].ToString();
            }
            return strDateOfRP;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "GetLastUpdatedDateOfRP", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Get the Active or Inactive resource plan.
    /// </summary>
    private BusinessEntities.RaveHRCollection GetActiveOrInactiveResourcePlan(int IsActive, int ProjectId)
    {
        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
        Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLResourcePlan = new Rave.HR.BusinessLayer.Projects.ResourcePlan();

        try
        {
            //Call the business layer method.
            raveHRCollection = objBLLResourcePlan.GetActiveOrInactiveResourcePlan(IsActive, ProjectId);

            return raveHRCollection;
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "GetActiveOrInactiveResourcePlan", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
        }
    }

    /// <summary>
    /// Authorize User
    /// </summary>
    public void AuthorizeUserForPageView()
    {
        Common.AuthorizationManager.AuthorizationManager objAuthorizationManager = new Common.AuthorizationManager.AuthorizationManager();
        objAuthorizationManager.AuthorizeUserForPageView(new object[] { AuthorizationManagerConstants.OPERATION_PAGE_EDITRPOPTIONS_VIEW });
    }

    #endregion Private method

}
