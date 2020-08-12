using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Common;
using Common.AuthorizationManager;

public partial class Home : BaseClass
{
    #region Public Variables

    /// <summary>
    /// Defines a constant for Page Name used in each catch block 
    /// </summary>
    private const string CLASS_NAME = "Home";

    #endregion Public Variables

    #region Protected Methods
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
                    if (!IsPostBack)
                    {
                        //AuthorizeUserForProjectSubMenuTabs();
                    }
                }
            }
            catch (RaveHRException ex)
            {
                LogErrorMessage(ex);
            }
            catch (Exception ex)
            {
                RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "Page_Load", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
                LogErrorMessage(objEx);
            }
        }

        /// <summary>
        /// Authorize user for project sub menu tab
        /// </summary>
        /// <returns>void</returns>
        private void AuthorizeUserForProjectSubMenuTabs()
        {
            try
            {
                ArrayList arrPagesAccessForUser = new ArrayList();
                AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
                HtmlGenericControl divProjectSummary = (HtmlGenericControl)Page.Master.FindControl("divProjectSummary");
                HtmlGenericControl divApproveRejectResourcePlan = (HtmlGenericControl)Page.Master.FindControl("divApproveRejectResourcePlan");
                arrPagesAccessForUser = objRaveHRAuthorizationManager.getProjectTabMenuForUser();

                //--Check access for project summary sub menu tab
                if (arrPagesAccessForUser.Contains(CommonConstants.PROJECTSUMMARY_PAGE)){divProjectSummary.Visible = true;}
                else{divProjectSummary.Visible = false;}

                //--Check access for approve reject rp sub menu tab
                if (arrPagesAccessForUser.Contains(CommonConstants.APPROVEREJECTRP_PAGE)) { divApproveRejectResourcePlan.Visible = true; }
                else { divApproveRejectResourcePlan.Visible = false; }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, "AuthorizeUserForProjectSubMenuTabs", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            }
        }
    #endregion Protected Methods

 }
