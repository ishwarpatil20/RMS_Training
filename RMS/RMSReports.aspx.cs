using RMS.Common.AuthorizationManager;
using RMS.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Infrastructure.Interfaces;
using Services.Interfaces;
using Services;
using RMS.Common.Constants;
using RMS.Common.ExceptionHandling;

namespace RMS
{
    public partial class RMSReports : System.Web.UI.Page
    {
        #region Methods {Init,PopulateMenu,GetReport}
        void Init()
        {
            ArrayList arrRolesForUser = new ArrayList();
            AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
            arrRolesForUser = objRaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());

            //--Add to session 


            string UserMailId = objRaveHRAuthorizationManager.getLoggedInUser();
            string[] UserName;
            string UserDisplayName = null;
            string FinalUserName = null;
            char separator = Convert.ToChar(System.Configuration.ConfigurationManager.AppSettings["SplitCharacter"]);

            if (arrRolesForUser.Count == 0)
            {
                ////Response.Redirect(CommonConstants.UNAUTHORISEDUSER);
            }
            UserName = UserMailId.Split('@');
            if (UserName[0].Contains(separator.ToString()))
            {
                UserName = UserName[0].Split(separator);
                for (int i = 0; i < UserName.Length; i++)
                {
                    //UserName[i] = ConvertToUpper(UserName[i]);
                    UserDisplayName += UserName[i];

                    if (i < UserName.Length - 1)
                        UserDisplayName += ".";
                }
            }
            else
            {
                FinalUserName = UserName[0];//ConvertToUpper(UserName[0]);
                UserDisplayName = FinalUserName;
            }
            lblUser.Text = lblUser.Text + " " + UserDisplayName;
        }




        private DataView GetData(int roleId, int parentMenuId)
        {
            Services.CommonService objservice = new Services.CommonService();
            List<RMS.Common.BusinessEntities.Menu.Menu> MenuList = objservice.GetAuthoriseMenuList(Convert.ToInt32(Session["EmpID"]));
            DataTable dt = abc.ToDataTable<Common.BusinessEntities.Menu.Menu>(MenuList);
            DataView da = new DataView(dt);

            da.RowFilter = "ParentID = " + parentMenuId;
            return da;
        }


        private void PopulateMenu(DataView dt, int parentMenuId, MenuItem parentMenuItem)
        {
            string currentPage = Path.GetFileName(Request.Url.AbsolutePath);
            foreach (DataRow row in dt.Table.Rows)
            {
                MenuItem menuItem = new MenuItem
                {
                    Value = row["PageId"].ToString(),
                    Text = row["PageName"].ToString(),
                    NavigateUrl = row["PageURL"].ToString(),
                    Selected = row["PageURL"].ToString().EndsWith(currentPage, StringComparison.CurrentCultureIgnoreCase)
                };
                int p = int.Parse(row["ParentID"].ToString());

                if (parentMenuId == 0)
                {
                    if (p == parentMenuId)
                    {
                        Menu.Items.Add(menuItem);
                        //DataView dtChild = dt;
                        //dtChild.RowFilter = "ParentID = " + menuItem.Value;
                        PopulateMenu(dt, int.Parse(menuItem.Value), menuItem);
                    }
                }
                else if (p == int.Parse(parentMenuItem.Value))
                {
                    parentMenuItem.ChildItems.Add(menuItem);
                }
            }
        }
        private void GetReport(string strReport)
        {
            try
            {

                rpvReports.ShowCredentialPrompts = false;

                //--Credentials
                //rpvReports.ServerReport.ReportServerCredentials = new ReportCredentials("username", "passwd", "domain");

                //--Processing mode
                rpvReports.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;

                //--Report Server
                rpvReports.ServerReport.ReportServerUrl = new System.Uri(ConfigurationManager.AppSettings["ReportServerURL"].ToString());
                //rpvReports.ServerReport.ReportServerUrl = new System.Uri("http://cu-489:8080/ReportServer/");

                //--Report dir
                rpvReports.ServerReport.ReportPath = "/TrainingModuleReports/" + strReport;
                //--report
                rpvReports.ServerReport.Refresh();
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "RMSReports", "GetReport", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }

        }

        #endregion

        #region Events {Page_Load}
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Initializations {_commonservice}
            ICommonService _commonservice = new CommonService();
            //Commented By Rakesh 
            //    DataView dt = this.GetData(Convert.ToInt32(Session["RoleID"]), 0);
            #endregion

            #region Set EmpId to Session,Set Azman Roles to Session
            if (String.IsNullOrEmpty(Convert.ToString(Session["EmpID"])))
            {
                Session["EmpID"] = _commonservice.GetEmployeeID();
            }
            if (Session[AuthorizationManagerConstants.AZMAN_ROLES] == null)
            {
                ArrayList arrRolesForUser = new ArrayList();
                arrRolesForUser = _commonservice.GetEmployeeRole(Convert.ToInt32(Session["EmpID"]));
                Session[AuthorizationManagerConstants.AZMAN_ROLES] = arrRolesForUser;
            }

            #endregion

            List<RMS.Common.BusinessEntities.Menu.Menu> MenuList = _commonservice.GetAuthoriseMenuList(Convert.ToInt32(Session["EmpID"]));
            DataTable dt = abc.ToDataTable<Common.BusinessEntities.Menu.Menu>(MenuList);
            DataView da = new DataView(dt);
            da.RowFilter = "ParentID = " + 0;
            PopulateMenu(da, 0, null);

            if (!IsPostBack)
            {
                Init();
                #region Report Rendering
                //Rakesh : Issue 59584: 05/May/2017 : Starts
                int pageId = Convert.ToInt32(Request.QueryString["pid"]);
                var qryMenu = MenuList.Where(p => p.PageID == pageId).FirstOrDefault();
                if (qryMenu != null)
                {
                    lblReportName.Text = qryMenu.PageName;
                    GetReport(qryMenu.ReportName);
                }
                //Rakesh : Issue 59584: 05/May/2017   : End            

                #endregion
            }
        }
        #endregion


    }
    #region Extension Methods{ToDataTable}
    public static class abc
    {
        public static DataTable ToDataTable<T>(this List<T> iList)
        {
            DataTable dataTable = new DataTable();
            PropertyDescriptorCollection propertyDescriptorCollection =
                TypeDescriptor.GetProperties(typeof(T));
            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);


                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }
            object[] values = new object[propertyDescriptorCollection.Count];
            foreach (T iListItem in iList)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

    }
    #endregion
}