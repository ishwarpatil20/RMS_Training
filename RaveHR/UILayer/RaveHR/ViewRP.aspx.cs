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

public partial class ViewRP : BaseClass
{
    # region Private Field Members

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
    private const string CLASS_NAME_RP = CommonConstants.VIEWRP_PAGE;

    /// <summary>
    /// define OnClick
    /// </summary>
    private const string OnClick = "OnClick";

    /// <summary>
    /// define validateViewButton
    /// </summary>
    private const string validateViewButton = "return validateViewButton()";

    #endregion Private Field Members

    #region Protected Method

    /// <summary>
    /// page load eventhandler
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
                //Get projectId from project module.
                if (Request.QueryString[QueryStringConstants.PROJECTID] != null)
                {
                    int ProjectId = int.Parse(DecryptQueryString(QueryStringConstants.PROJECTID).ToString());

                    //checks if session is null
                    if (Session[SessionNames.PROJECTSUMMARY_PROJECT_ID] != null)
                    {
                        if (!(((ArrayList)Session[SessionNames.PROJECTSUMMARY_PROJECT_ID]).Contains(ProjectId)))
                        {
                            Response.Redirect(CommonConstants.UNAUTHORISEDUSER, false);
                        }
                    }

                    if (!IsPostBack)
                    {
                        //--Authorise User
                        AuthorizeUserForPageView();

                        //Bind the Inactive RP dropdownlist.
                        BindDropdown(GetActiveOrInactiveResourcePlan(Convert.ToInt32(Common.MasterEnum.ResourcePlanStatus.InActive), ProjectId), ddlInactiveRP);

                        //Bind the Active RP dropdownlist.
                        BindDropdown(GetActiveOrInactiveResourcePlan(Convert.ToInt32(Common.MasterEnum.ResourcePlanStatus.Active), ProjectId), ddlActiveRP);

                        btnViewRP.Attributes.Add(OnClick, validateViewButton);
                        if ((Request.QueryString[QueryStringConstants.RPCODE] != null) && (Request.QueryString[QueryStringConstants.RPID] != null) && (Request.QueryString[QueryStringConstants.FLAG] != null))
                        {
                            ddlActiveRP.SelectedItem.Text = DecryptQueryString(QueryStringConstants.RPCODE);
                         
                            ddlActiveRP.SelectedItem.Value = DecryptQueryString(QueryStringConstants.RPID);
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME_RP, "Page_Load", EventIDConstants.RAVE_HR_RP_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    ///  Set the last modified date of respective RP .
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlInactiveRP_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblInactiveRPDate.Text = GetLastUpdatedDateOfRP(ddlInactiveRP.SelectedItem.Value, 1);
    }

    /// <summary>
    ///  Set the last modified date of respective RP .
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlactiveRP_SelectedIndexChanged(object sender, EventArgs e)
    {
        //TODO. can be in javascript.
        lblActiveRPDate.Text = GetLastUpdatedDateOfRP(ddlActiveRP.SelectedItem.Value, 1);
    }

    /// <summary>
    /// Redirect on previous page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //redirect to project summary page.
        Response.Redirect(CommonConstants.PROJECTSUMMARY_PAGE, false);
    }

    /// <summary>
    /// get execl report by resource plan id.
    /// </summary>
    protected void btnViewRP_Click(object sender, EventArgs e)
    {
        Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLViewRP = new Rave.HR.BusinessLayer.Projects.ResourcePlan();
        if (rbRPInactiveStatus.Checked)
        {
            string[] RPId = ddlInactiveRP.SelectedValue.Split(Convert.ToChar(SPLITER));
            string strRPId = RPId[0].ToString();
            objBLLViewRP.ViewRPInExcel(int.Parse(strRPId), ddlInactiveRP.SelectedItem.Text,"");
        }
        else if (rbRPActiveStatus.Checked)
        {
            string[] RPId = ddlActiveRP.SelectedValue.Split(Convert.ToChar(SPLITER));
            string strRPId = RPId[0].ToString();
            objBLLViewRP.ViewRPInExcel(int.Parse(strRPId), ddlActiveRP.SelectedItem.Text, "");
        }
    }


    #endregion Protected Method

    #region Private Member Functions

    /// <summary>
    /// Get the Active or Inactive resource plan.
    /// </summary>
    /// <param name="IsActive"></param>
    /// <returns></returns>
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
    /// Bind the dropdown list by respective colllection data.
    /// </summary>
    /// <param name="raveHRCollection">Collection of data</param>
    /// <param name="ddl">Dropdownlist in which should  be filled</param>
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
    /// Dropdown value is appended by RPUpadtedDate.
    /// </summary>
    /// <param name="dropDownValue"></param>   
    /// <param name="index"></param>
    /// <returns></returns>
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
    /// Authorize User
    /// </summary>
    public void AuthorizeUserForPageView()
    {
        Common.AuthorizationManager.AuthorizationManager objAuthorizationManager = new Common.AuthorizationManager.AuthorizationManager();
        objAuthorizationManager.AuthorizeUserForPageView(new object[] { AuthorizationManagerConstants.OPERATION_PAGE_VIEWRP_VIEW });
    }

    #endregion  Private Member Functions

}
