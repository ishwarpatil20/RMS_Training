//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           MrfListOfInternalResource.aspx      
//  Author:         Chhaya Gunjal 
//  Date written:   03/09/2009/ 10:58:30 AM
//  Description:    The MrfListOfInternalResource page summarises Resource details. 
//
//  Amendments
//  Date                    Who              Ref     Description
//  ----                    -----------      ---     -----------
//  13/11/2009 10:58:30 AM  Chhaya Gunjal    n/a     Created    
//
//------------------------------------------------------------------------------
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
using BusinessEntities;
using System.Data.SqlClient;
using Common.Constants;
using Common;
public partial class ResourcePlanLocationList : BaseClass
{
    #region Constants
    private const string CLASS_NAME = "ResourcePlanLocationList.aspx";
    private const string PAGE_LOAD = "Page_Load";
    #endregion

    /// <summary>
    /// Gets The Resource Plan Id
    /// </summary>
    private string RPDuId = string.Empty;

    //Declaring COllection class object
    BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

    //Create Object for mrfDetail for Business Layer
    Rave.HR.BusinessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.BusinessLayer.MRF.MRFDetail();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!ValidateURL())
            {
                Response.Redirect(CommonConstants.INVALIDURL);
            }
            else
            {
                //Extract the parameters from Query String
                if (Request.QueryString[QueryStringConstants.RPDUID] != null)
                    RPDuId = DecryptQueryString(QueryStringConstants.RPDUID).ToString();

                if (!IsPostBack)
                {
                    if (Request.QueryString[QueryStringConstants.RPDUID] != null)
                    {
                        RPDuId = DecryptQueryString(QueryStringConstants.RPDUID).ToString();

                        raveHRCollection = Rave.HR.BusinessLayer.MRF.MRFDetail.GetRPSplitDurationInOnsite_Offshore(Convert.ToInt32(RPDuId));
                        //Check Collection object is null or not
                        if (raveHRCollection != null)
                        {
                            //Assign DataSource
                            grdvResourcePlanLocationList.DataSource = raveHRCollection;
                            //Bind Dropdown
                            grdvResourcePlanLocationList.DataBind();
                        }
                        btnOk.Focus();
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, PAGE_LOAD, EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            //Umesh: Issue 'Modal Popup issue in chrome' Starts
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "CloseJS", "jQuery.modalDialog.getCurrent().close();", true);
            //Umesh: Issue 'Modal Popup issue in chrome' Ends
        }
        //catches RaveHRException exception
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, CLASS_NAME, PAGE_LOAD, EventIDConstants.RAVE_HR_MRF_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }
}
