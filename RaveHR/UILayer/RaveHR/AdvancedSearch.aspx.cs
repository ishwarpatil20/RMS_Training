//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           AdvancedSearch.aspx.cs       
//  Author:         prashant.mala 
//  Date written:   11/06/2009/ 10:45:00 AM
//  Description:    Advanced search for projects
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  11/06/2009 10:45:00 AM  prashant.mala    n/a     Created    
//
//------------------------------------------------------------------------------

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
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Common;
using Common.AuthorizationManager;

public partial class AdvancedSearch : BaseClass
{
    #region Private Field Memebers

    /// <summary>
    /// defines page name
    /// </summary>
    private const string PAGE_NAME = "AdvancedSearch.aspx.cs";

    #endregion Private Field Members

    #region Protected Events

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
                //--Error message 
                lblMessage.Visible = false;

                if (!IsPostBack)
                {
                    //--project categry list
                    GetProjectCategory();
                    //GetProjectDomain(); 
                }
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGE_NAME, "Page_Load", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void rpCategories_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                ListBox lbCategory = (ListBox)e.Item.FindControl("lbCategory");
                Label lblCategoryID = (Label)e.Item.FindControl("lblCategoryID");
                if (lblCategoryID != null)
                {
                    //--Get Category ID
                    string strCategoryID = lblCategoryID.Text;

                    Rave.HR.BusinessLayer.Projects.Projects objTechnologyBAL = new Rave.HR.BusinessLayer.Projects.Projects();
                    Rave.HR.BusinessLayer.Projects.Projects objCheckedCategoryNameBAL = new Rave.HR.BusinessLayer.Projects.Projects();
                    List<BusinessEntities.Technology> objListTechnology;
                    objListTechnology = new List<BusinessEntities.Technology>();
                    objListTechnology = objTechnologyBAL.Technology(int.Parse(strCategoryID));

                    //--Bind category listbox
                    lbCategory.Items.Clear();
                    lbCategory.DataSource = objListTechnology;
                    lbCategory.DataTextField = "TechnolgoyName";
                    lbCategory.DataValueField = "TechnologyID";
                    lbCategory.DataBind();
                }
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGE_NAME, "rpCategories_OnItemDataBound", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            //--reset fields
            txtKeyword.Text = "";
            txtDateFrom.Text = "";
            txtDateTo.Text = "";
            txtYear.Text = "";
            txtMonth.Text = "";
            //lbDomain.ClearSelection();
            //lbSubDomain.Items.Clear();

            //--Get collections
            RepeaterItemCollection rpCategoriesCollection = rpCategories.Items;
            for (int i = 0; i < rpCategoriesCollection.Count; i++)
            {
                RepeaterItem rpCategoryItem = rpCategoriesCollection[i];
                ListBox lbCategory = (ListBox)rpCategoryItem.FindControl("lbCategory");
                lbCategory.ClearSelection();
            }
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGE_NAME, "btnReset_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //--Check if search keyword entered
            string strKeyword = null;
            bool bKeyword = false;
            if (txtKeyword.Text.Trim() != "")
            {
                strKeyword = txtKeyword.Text.Trim();
                bKeyword = true;
            }

            Rave.HR.BusinessLayer.Projects.Projects objRaveHRProjects = new Rave.HR.BusinessLayer.Projects.Projects();
            List<BusinessEntities.Projects> objRaveHRProjectsList = new List<BusinessEntities.Projects>();
            objRaveHRProjectsList = objRaveHRProjects.GetProjectSearchResult(strKeyword);

            //--Get datatable
            if (bKeyword == true)
            {
                //--project search result datasource
                rpProjectSearchResult.DataSource = objRaveHRProjectsList;
            }
            else
            {
                DataTable dtProjectList = null;
                dtProjectList = GetDataTable(objRaveHRProjectsList);
                List<BusinessEntities.Projects> objProjectSearchResult = GetProjectSearchResult(dtProjectList);
                if (objProjectSearchResult == null)
                {
                    return;
                }
                //--assign datasource to repeater
                if (objProjectSearchResult.Count > 0)
                {
                    rpProjectSearchResult.DataSource = objProjectSearchResult;
                }
                else
                {
                    //--display message
                    lblMessage.Text = CommonConstants.NO_RECORD_MESSAGE;
                    lblMessage.Visible = true;
                }
            }

            //--Bind Grid
            rpProjectSearchResult.DataBind();

            //--Display div
            divProjectSearchResult.Visible = true;
            divProjectSearch.Visible = false;
        }
        catch (RaveHRException ex)
        {
            LogErrorMessage(ex);
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGE_NAME, "btnSearch_Click", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    protected void btnBackToSearch_Click(object sender, EventArgs e)
    {
        //--
        divProjectSearchResult.Visible = false;
        divProjectSearch.Visible = true;
    }

    protected void rpProjectSearchResult_OnItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HyperLink hypProject = (HyperLink)e.Item.FindControl("hypProject");
                BusinessEntities.Projects drv = (BusinessEntities.Projects)e.Item.DataItem;
                if (drv != null)
                {
                    if (hypProject != null)
                    {
                        hypProject.Text = drv.ClientName + " - " + drv.ProjectName;
                        hypProject.NavigateUrl = "AddProject.aspx?" + URLHelper.SecureParameters("ProjectID", drv.ID.ToString()) + "&" + URLHelper.SecureParameters("Mode", "View") + "&" + URLHelper.SecureParameters("sortExpression", "ProjectID") + "&" + URLHelper.SecureParameters("sortDirection", "ASC") + "&" + URLHelper.SecureParameters("workFlow", "Select") + "&" + URLHelper.CreateSignature(drv.ID.ToString(), "View", "ProjectID", "ASC", "Select");
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
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGE_NAME, "rpProjectSearchResult_OnItemDataBound", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    #endregion Protected Events

    #region Private Memebers Functions

    /// <summary>
    /// Project Category List
    /// </summary>
    /// <returns>void</returns>
    private void GetProjectCategory()
    {
        try
        {
            Rave.HR.BusinessLayer.Projects.Projects objCategoryBAL = new Rave.HR.BusinessLayer.Projects.Projects();

            List<BusinessEntities.Category> objListCategory = new List<BusinessEntities.Category>();
            objListCategory = objCategoryBAL.TechnologyCategory();

            rpCategories.DataSource = objListCategory;
            //--bind repeater
            rpCategories.DataBind();

        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGE_NAME, "GetProjectCategory", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    /// <summary>
    /// Projects search result
    /// </summary>
    /// <param name="dtProjectSearchResult">DataTable</param>
    /// <returns>List</returns>
    private List<BusinessEntities.Projects> GetProjectSearchResult(DataTable dtProjectSearchResult)
    {
        List<BusinessEntities.Projects> objProjectSearchList = null;
        try
        {
            DataTable dt = dtProjectSearchResult;

            RepeaterItemCollection objRepeaterItemCollection = rpCategories.Items;
            DataRow[] oRows = null;
            StringBuilder strbTechnology = new StringBuilder("");
            foreach (RepeaterItem objRepeaterItem in objRepeaterItemCollection)
            {
                Label lblCategory = (Label)objRepeaterItem.FindControl("lblCategory");
                ListBox lbCategory = (ListBox)objRepeaterItem.FindControl("lbCategory");
                if ((lblCategory != null) && (lbCategory != null))
                {
                    foreach (ListItem oItem in lbCategory.Items)
                    {
                        if (oItem.Selected == true)
                        {
                            strbTechnology.Append("'" + oItem.Text + "',");
                        }
                    }
                }
            }

            string strSQL = string.Empty;
            bool bSelectSQL = false;
            if (strbTechnology.ToString() != "")
            {
                strSQL += "technologyName in (" + strbTechnology.ToString().Trim(',') + ")";
                bSelectSQL = true;
            }

            if ((txtDateFrom.Text.Trim() != "") && (txtDateTo.Text.Trim() != ""))
            {
                if (bSelectSQL == true)
                {
                    strSQL += " or ((startDate >= '" + txtDateFrom.Text.Trim() + "' ) and ( endDate <= '" + txtDateTo.Text.Trim() + "' ) )";
                }
                else
                {
                    strSQL = " ((startDate >= '" + txtDateFrom.Text.Trim() + "' ) and ( endDate <= '" + txtDateTo.Text.Trim() + "' ) )";
                    bSelectSQL = true;
                }
            }

            if (txtYear.Text.Trim() != "")
            {
                if (bSelectSQL == true)
                {
                    strSQL += " or (( projectStartYear = '" + txtYear.Text.Trim() + "' ) or ( projectEndYear = '" + txtYear.Text.Trim() + "' )) ";
                }
                else
                {
                    strSQL = " (( projectStartYear = '" + txtYear.Text.Trim() + "' ) or ( projectEndYear = '" + txtYear.Text.Trim() + "' )) ";
                    bSelectSQL = true;
                }
            }

            if (txtMonth.Text.Trim() != "")
            {
                int iMonths = int.Parse(txtMonth.Text.Trim());
                string strDate = DateTime.Now.AddMonths(-iMonths).ToShortDateString();
                if (bSelectSQL == true)
                {
                    strSQL += " or ((startDate >= '" + strDate + "' ) and (startDate <= '" + DateTime.Now.ToString() + "'))";

                }
                else
                {
                    strSQL = " ((startDate >= '" + strDate + "' ) and (startDate <= '" + DateTime.Now.ToString() + "'))";
                    bSelectSQL = true;
                }
            }

            if (bSelectSQL == false)
            {
                lblMessage.Text = "Enter Search Criteria";
                lblMessage.Visible = true;
                return null;
            }

            oRows = dt.Select(strSQL);
            objProjectSearchList = new List<BusinessEntities.Projects>();
            BusinessEntities.Projects objProjects = null;
            foreach (DataRow objRow in oRows)
            {
                objProjects = new BusinessEntities.Projects();
                objProjects.ID = int.Parse(objRow["ID"].ToString());
                objProjects.ClientName = objRow["strClientName"].ToString();
                objProjects.ProjectName = objRow["strProjectName"].ToString();
                objProjects.Location = objRow["strLocation"].ToString();
                objProjects.Category = objRow["category"].ToString();
                objProjects.TechnologyName = objRow["technologyName"].ToString();
                objProjects.StartDate = DateTime.Parse(objRow["startDate"].ToString());
                objProjects.EndDate = DateTime.Parse(objRow["endDate"].ToString());

                objProjectSearchList.Add(objProjects);
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGE_NAME, "GetProjectSearchResult", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }

        return objProjectSearchList;
    }

    /// <summary>
    /// Converts generic List to DataTable
    /// </summary>
    /// <param name="objListSort">List</param>
    /// <returns>DataTable</returns>
    private DataTable GetDataTable(List<BusinessEntities.Projects> objListSort)
    {
        Type type = null;
        PropertyInfo[] pinfos = null;
        DataTable table = new DataTable();

        try
        {
            foreach (object t in objListSort)
            {
                if (type == null)
                {
                    type = t.GetType();
                    pinfos = type.GetProperties();
                    CreateColumns(pinfos, table);
                }
                DataRow row = table.NewRow();

                foreach (DataColumn column in table.Columns)
                {
                    object value = pinfos[column.Ordinal].GetValue(t, null);
                    row[column.ColumnName] = value;
                }
                table.Rows.Add(row);
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGE_NAME, "GetDataTable", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
        return table;
    }

    /// <summary>
    /// Creates Data Column
    /// </summary>
    /// <param name="pinfos">Property Information</param>
    /// <param name="table">Data Table</param>
    private void CreateColumns(PropertyInfo[] pinfos, DataTable table)
    {
        try
        {
            foreach (PropertyInfo pinfo in pinfos)
            {
                DataColumn column = new DataColumn(pinfo.Name, pinfo.PropertyType);
                table.Columns.Add(column);
            }
        }
        catch (RaveHRException ex)
        {
            throw ex;
        }
        catch (Exception ex)
        {
            RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.PresentationLayer, PAGE_NAME, "CreateColumns", EventIDConstants.RAVE_HR_PROJECTS_PRESENTATION_LAYER);
            LogErrorMessage(objEx);
        }
    }

    #endregion Private Memebers Functions

    //private void GetProjectDomain()
    //{
    //    try
    //    {
    //        Rave.HR.BusinessLayer.Projects.Projects objDomainBAL = new Rave.HR.BusinessLayer.Projects.Projects();

    //        List<BusinessEntities.Domain> objListDomain = new List<BusinessEntities.Domain>();
    //        objListDomain = objDomainBAL.GetDomainName();
    //        lbDomain.Items.Clear();
    //        lbDomain.DataSource = objListDomain;
    //        lbDomain.DataTextField = "DomainName";
    //        lbDomain.DataValueField = "DomainID";
    //        lbDomain.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex.Message + "<br>" + ex.StackTrace);
    //    }
    //}

    //protected void lbDomain_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        Rave.HR.BusinessLayer.Projects.Projects objSubDomainBAL = new Rave.HR.BusinessLayer.Projects.Projects();

    //        Rave.HR.BusinessLayer.Projects.Projects objCheckedDomainNameBAL = new Rave.HR.BusinessLayer.Projects.Projects();

    //        List<BusinessEntities.SubDomain> objListSubDomain;
    //        objListSubDomain = new List<BusinessEntities.SubDomain>();
    //        objListSubDomain = objSubDomainBAL.GetSubDomain(int.Parse(lbDomain.SelectedValue));
    //        lbSubDomain.Items.Clear();
    //        lbSubDomain.DataSource = objListSubDomain;
    //        lbSubDomain.DataTextField = "SubDomainName";
    //        lbSubDomain.DataValueField = "SubDomainID";
    //        lbSubDomain.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        Response.Write(ex.Message + "<br>" + ex.StackTrace);
    //    }
    //}
}
