using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Configuration;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using RMS.Common.ExceptionHandling;
using RMS.Common.Constants;
using System.Web.Security;
using RMS.Common.DataBase;

namespace RMS.Common.AuthorizationManager
{
    public class AuthorizationManager
    {

        #region Private Field Members

        /// <summary>
        /// Page Class Name
        /// </summary>
        private const string CLASS_NAME_RP = "AuthorizationManager";

        /// <summary>
        /// Location of Rave Office "Benguluru"
        /// </summary>
        private const string BENGULURU = "Benguluru";

        #endregion Private Field Members


        #region Member Functions

        /// <summary>
        /// Gets the logged in user
        /// </summary>
        /// <returns>string</returns>
        public string getLoggedInUser()
        {
            //Mohamed -- Logout functionality --Start
            string strUserIdentity = string.Empty;
            string domainName = string.Empty;
            //GoogleMail
            try
            {
                if (HttpContext.Current.ApplicationInstance.Session["WindowsUsername"] == null)
                {

                    strUserIdentity = HttpContext.Current.Request.LogonUserIdentity.Name;
                    int position = strUserIdentity.IndexOf("\\");
                    strUserIdentity = strUserIdentity.Remove(0, position + 1);
                    strUserIdentity = strUserIdentity.Replace(AuthorizationManagerConstants.RAVEDOMAIN + @"\", "");

                    strUserIdentity = GetWindowsUsernameAsPerNorthgate(strUserIdentity, out domainName);
                    HttpContext.Current.ApplicationInstance.Session["WindowsUsername"] = strUserIdentity;
                    HttpContext.Current.ApplicationInstance.Session["domainName"] = domainName;
                }
                else
                {
                    strUserIdentity = HttpContext.Current.ApplicationInstance.Session["WindowsUsername"].ToString().Trim();
                    domainName = HttpContext.Current.ApplicationInstance.Session["domainName"].ToString().Trim();
                }

                //Mohamed -- Logout functionality --end

                strUserIdentity = strUserIdentity + "@" + domainName;
                strUserIdentity = strUserIdentity.Replace("com", "co.in");
                //Googleconfigurable
                //if (domainName.Trim().ToLower() == AuthorizationManagerConstants.RAVEDOMAINEMAIL)
                //{
                //    strUserIdentity = strUserIdentity + "@" + AuthorizationManagerConstants.RAVEDOMAIN;
                //}
                //else
                //{
                //    strUserIdentity = strUserIdentity + "@" + AuthorizationManagerConstants.NORTHGATEDOMAIN;
                //}

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                new RaveHRException(ex.Message, ex, Sources.CommonLayer, "RaveHRAuthorizationManager", "getLoggedInUser", EventIDConstants.AUTHORIZATION_MANAGER_ERROR);
            }
            return strUserIdentity;
        }

        /// <summary>
        /// Gets the roles for the user
        /// </summary>
        /// <returns>arraylist</returns>
        public ArrayList getRolesForUser(string strUser)
        {
            ArrayList arrRolesForUser = new ArrayList();
            try
            {
                string[] strUserInRoles = Roles.GetRolesForUser();
                //for northgate network
                //int position = strUser.IndexOf("@");
                //strUser = strUser.Replace("@northgate-is.co.in", "");
                //string[] strUserInRoles = Roles.GetRolesForUser(strUser);
                foreach (string strRole in strUserInRoles)
                {
                    arrRolesForUser.Add(strRole);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                new RaveHRException(ex.Message, ex, Sources.CommonLayer, "RaveHRAuthorizationManager", "getRolesForUser", EventIDConstants.AUTHORIZATION_MANAGER_ERROR);
            }
            return arrRolesForUser;
        }

        public ArrayList getRolesForUser1(string strUser)
        {
            ArrayList arrRolesForUser = new ArrayList();
            try
            {

                string[] strUserInRoles = Roles.GetRolesForUser(strUser);
                if (strUserInRoles != null)
                {
                    foreach (string strRole in strUserInRoles)
                    {
                        arrRolesForUser.Add(strRole);
                    }
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                new RaveHRException(ex.Message, ex, Sources.CommonLayer, "RaveHRAuthorizationManager", "getRolesForUser", EventIDConstants.AUTHORIZATION_MANAGER_ERROR);
            }
            return arrRolesForUser;
        }

        /// <summary>
        /// Gets the page name
        /// </summary>
        /// <returns>void</returns>
        public static string GetCurrentPageName()
        {
            string strPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(strPath);
            string strRet = oInfo.Name;
            return strRet;
        }

        public void IsUserAuthorizedToPage(ArrayList arrPagesAccessForUser)
        {
            try
            {
                string strPageName = GetCurrentPageName();
                if (!arrPagesAccessForUser.Contains(strPageName))
                {
                    HttpContext.Current.Response.Redirect(AuthorizationManagerConstants.PAGEHOME, false);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                new RaveHRException(ex.Message, ex, Sources.CommonLayer, "RaveHRAuthorizationManager", "IsUserAuthorizedToPage", EventIDConstants.AUTHORIZATION_MANAGER_ERROR);
            }
        }

        /// <summary>
        /// Gets the main tab menu for user based on authorization
        /// </summary>
        /// <returns>Arraylist</returns>
        public ArrayList getUpperTabMenuForUser()
        {
            ArrayList arrMainMenuTab = new ArrayList();
            bool bCheckAccess = false;
            object[] operationsId = null;
            try
            {
                //--Check acess for project menu
                operationsId = new object[] { AuthorizationManagerConstants.OPERATION_MAINMENUTAB_PROJECT_VIEW };
                bCheckAccess = CheckAccessPermissions(operationsId);
                if (bCheckAccess)
                {
                    arrMainMenuTab.Add(CommonConstants.TABPROJECT);
                    bCheckAccess = false;
                }

                //--Check acess for mrf menu
                operationsId = new object[] { AuthorizationManagerConstants.OPERATION_MAINMENUTAB_MRF_VIEW };
                bCheckAccess = CheckAccessPermissions(operationsId);
                if (bCheckAccess)
                {
                    arrMainMenuTab.Add(CommonConstants.TABMRF);
                    bCheckAccess = false;
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.CommonLayer, "RaveHRAuthorizationManager", "getUpperTabMenuForUser", EventIDConstants.AUTHORIZATION_MANAGER_ERROR);
            }

            return arrMainMenuTab;
        }

        //43639--Jignyasa--Start
        public bool CheckIfFunctionalManager()
        {
            bool functionalManager = false;
            DataAccessClass dataAccessClass = new DataAccessClass();

            try
            {
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.CurrentUserEmailAddress, DbType.String);
                sqlParam[0].Value = getLoggedInUserEmailId();

                //Open the connection to DB
                dataAccessClass.OpenConnection(DBConstants.GetDBConnectionString());

                var count = dataAccessClass.ExecuteScalarSP(SPNames.Master_CheckIfFunctionalManager, sqlParam);


                if (Convert.ToInt32(count) > 0)
                {
                    functionalManager = true;
                }

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.CommonLayer, "RaveHRAuthorizationManager", "getProjectTabMenuForUser", EventIDConstants.AUTHORIZATION_MANAGER_ERROR);
            }

            finally
            {
                dataAccessClass.CloseConncetion();
            }

            return functionalManager;
        }
        //43639--Jignyasa--End

         //<summary>
         //Gets the project tab menu for user based on authorization
         //</summary>
         //<returns>Arraylist</returns>
        public ArrayList getProjectTabMenuForUser()
        {
            ArrayList arrPagesAccessForUser = new ArrayList();
            bool bCheckAccess = false;
            object[] operationsId = null;
            try
            {
                //--Check acess for project summary sub menu
                operationsId = new object[] { AuthorizationManagerConstants.OPERATION_SUBMENUTAB_PROJECTSUMMARY_VIEW };
                bCheckAccess = CheckAccessPermissions(operationsId);
                if (bCheckAccess)
                {
                    arrPagesAccessForUser.Add(CommonConstants.PROJECTSUMMARY_PAGE);
                    bCheckAccess = false;
                }

                //--Check access for view project page.
                operationsId = new object[] { AuthorizationManagerConstants.OPERATION_PAGE_VIEWPROJECT_EVENT };
                bCheckAccess = CheckAccessPermissions(operationsId);
                if (bCheckAccess)
                {
                    arrPagesAccessForUser.Add(CommonConstants.ADDPROJECT_PAGE);
                    bCheckAccess = false;
                }

                //--Check access for view approverejectrp page.
                operationsId = new object[] { AuthorizationManagerConstants.OPERATION_PAGE_APPROVEREJECTRP_VIEW };
                bCheckAccess = CheckAccessPermissions(operationsId);
                if (bCheckAccess)
                {
                    arrPagesAccessForUser.Add(CommonConstants.APPROVEREJECTRP_PAGE);
                    bCheckAccess = false;
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.CommonLayer, "RaveHRAuthorizationManager", "getProjectTabMenuForUser", EventIDConstants.AUTHORIZATION_MANAGER_ERROR);
            }

            return arrPagesAccessForUser;
        }

        /// <summary>
        /// Check access permission for user
        /// </summary>
        /// <returns>void</returns>
        public bool CheckAccessPermissions(object[] operationIds)
        {
            bool bCheckAccess = false;

            //AzAuthorizationStoreClass AzManStore = new AzAuthorizationStoreClass();
            //AzManStore.Initialize(0, ConfigurationManager.ConnectionStrings[AuthorizationManagerConstants.AZMANPOLICYSTORECONNECTIONSTRING].ConnectionString, null);
            //IAzApplication azApp = AzManStore.OpenApplication(AuthorizationManagerConstants.AZMANAPPLICATION, null);

            //// Get the current user context 
            //IPrincipal userPrincipal = HttpContext.Current.User;
            //WindowsIdentity userIdentity = userPrincipal.Identity as WindowsIdentity;

            //IAzClientContext clientContext = azApp.InitializeClientContextFromToken((ulong)userIdentity.Token, null);

            //// Check if user has access to the operations
            //// The first argument, "Auditstring", is a string that is used if you 
            //// have run-time auditing turned on
            //object[] result = (object[])clientContext.AccessCheck("CheckAccessPermission", new object[1], operationIds, null, null, null, null, null);

            //// Test the integer array we got back to see which operations are
            //// authorized
            //int accessAllowed = (int)result[0];
            //if (accessAllowed != 0)
            //{
            //    // current user not authorized to perform operation
            //    bCheckAccess = false;
            //}
            //else
            //{
            //    // current user authorized to perform operation
            //    bCheckAccess = true;
            //}

            return bCheckAccess;
        }

        /// <summary>
        /// Gets the logged in user emailid
        /// </summary>
        /// <returns>string</returns>
        public string getLoggedInUserEmailId()
        {
            string strUserIdentity = HttpContext.Current.User.Identity.Name; //HttpContext.Current.Request.LogonUserIdentity.Name;
            //GoogleMail
            try
            {
                int position = strUserIdentity.IndexOf("\\");
                strUserIdentity = strUserIdentity.Remove(0, position + 1);
                strUserIdentity = strUserIdentity.Replace(AuthorizationManagerConstants.RAVEDOMAIN + @"\", "");

                string domainName = string.Empty;
                ////strUserIdentity = GetWindowsUsernameAsPerNorthgate(strUserIdentity);
                //if (HttpContext.Current.ApplicationInstance == null || HttpContext.Current.ApplicationInstance.Session["WindowsUsername"] == null)
                //{
                //    strUserIdentity = GetWindowsUsernameAsPerNorthgate(strUserIdentity, out domainName);
                //    HttpContext.Current.ApplicationInstance.Session["WindowsUsername"] = strUserIdentity;
                //    HttpContext.Current.ApplicationInstance.Session["domainName"] = domainName;
                //}
                //else
                //{
                //    strUserIdentity = HttpContext.Current.ApplicationInstance.Session["WindowsUsername"].ToString().Trim();
                //    domainName = HttpContext.Current.ApplicationInstance.Session["domainName"].ToString().Trim();
                //}
                strUserIdentity = GetWindowsUsernameAsPerNorthgate(strUserIdentity, out domainName);
                strUserIdentity = strUserIdentity + "@" + domainName;

                //NorthgateChange comment else part and remove if condition.
                //if (strUserIdentity.ToUpper().Trim() == "SAWITA.KAMAT" || strUserIdentity.ToUpper().Trim() == "VRATIKA.ARORA")
                //{
                //    //Google
                //    //strUserIdentity = strUserIdentity + "@" + AuthorizationManagerConstants.RAVEDOMAIN;
                //    strUserIdentity = strUserIdentity + "@" + AuthorizationManagerConstants.NORTHGATEDOMAIN;
                //}
                //else
                //{
                //    strUserIdentity = strUserIdentity + "@" + AuthorizationManagerConstants.RAVEDOMAIN;
                //}
                ////strUserIdentity = strUserIdentity + "@" + AuthorizationManagerConstants.RAVEDOMAIN;
                //strUserIdentity = strUserIdentity.Replace("co.in", "com");
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                new RaveHRException(ex.Message, ex, Sources.CommonLayer, "RaveHRAuthorizationManager", "getLoggedInUserEmailId", EventIDConstants.AUTHORIZATION_MANAGER_ERROR);
            }
            return strUserIdentity;
        }


        /// <summary>
        /// Gets the logged in user emailid
        /// </summary>
        /// <returns>string</returns>
        public string GetWindowsUsernameAsPerNorthgate(string windowsUsername, out string domainName)
        {
            string username = "";
            string domName = "";
            //SqlConnection objConnection = null;
            //SqlCommand objCommand = null;
            SqlDataReader objReader;
            DataAccessClass objDA;
            //SqlDataAdapter objDataAdapter;

            try
            {

                //string ConnStr = DBConstants.GetDBConnectionString();
                //objConnection = new SqlConnection(ConnStr);
                //objConnection.Open();

                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //objCommand = new SqlCommand(SPNames.Master_GetNorthgateUsername, objConnection);
                //objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@Username", SqlDbType.VarChar, 100);
                if (windowsUsername == "" || windowsUsername == null)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = windowsUsername;

                //sqlParam[0] = objCommand.Parameters.AddWithValue("@Username", windowsUsername);
                sqlParam[1] = new SqlParameter("@OutUsername", SqlDbType.VarChar, 100);
                sqlParam[1].Value = "";
                sqlParam[1].Direction = ParameterDirection.Output;

                sqlParam[2] = new SqlParameter("@domainName", SqlDbType.VarChar, 100);
                sqlParam[2].Value = "";
                sqlParam[2].Direction = ParameterDirection.Output;

                objReader = objDA.ExecuteReaderSP(SPNames.Master_GetNorthgateUsername, sqlParam);

                //sqlParam[1] = objCommand.Parameters.AddWithValue("@OutUsername", SqlDbType.VarChar, 100);
                //sqlParam[1].Direction = ParameterDirection.Output;
                //int empId = Convert.ToInt32(sqlParam[1].Value);
                // objCommand.ExecuteNonQuery();
                username = sqlParam[1].Value.ToString();
                domName = sqlParam[2].Value.ToString();
                domainName = domName;
                return username;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                new RaveHRException(ex.Message, ex, Sources.CommonLayer, "RaveHRAuthorizationManager", "GetWindowsUsernameAsPerNorthgate", EventIDConstants.AUTHORIZATION_MANAGER_ERROR);
            }
            domainName = domName;
            return username;
        }




        /// <summary>
        /// Gets users in role
        /// </summary>
        public string getUserEmailIdInRoles(string strRole)
        {
            StringBuilder strbUserEmailIdInRole = new StringBuilder();
            try
            {
                string[] strUsersInRole = Roles.GetUsersInRole(strRole);
                foreach (string strUser in strUsersInRole)
                {
                    strbUserEmailIdInRole.Append(GetDomainUsers(strUser) + ",");

                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                new RaveHRException(ex.Message, ex, Sources.CommonLayer, "RaveHRAuthorizationManager", "getUserInRoles", EventIDConstants.AUTHORIZATION_MANAGER_ERROR);
            }
            return strbUserEmailIdInRole.ToString();
        }

        //Googleconfigurable
        /// <summary>
        /// Gets the domain user emailid
        /// </summary>
        public string GetUsernameBasedOnEmail(string strUserName)
        {
            if (!string.IsNullOrEmpty(strUserName) && strUserName.Contains("@"))
            {
                int position = strUserName.IndexOf("@");
                if (position != -1)
                {
                    string replaceStr = strUserName.Remove(0, position);
                    strUserName = strUserName.Replace(replaceStr, "").ToLower().Trim();
                }
            }
            return strUserName;
        }



        /// <summary>
        /// Gets the domain user emailid
        /// </summary>
        public string GetDomainUsers(string strUserName)
        {
            //GoogleMail
            string strUserEmail = string.Empty;
            string domainName = string.Empty;
            //Googleconfigurable
            //strUserName = strUserName.ToLower().Replace("@rave-tech.co.in", "");
            //strUserName = strUserName.ToLower().Replace("@" + AuthorizationManagerConstants.NORTHGATEDOMAIN, "");
            strUserName = GetUsernameBasedOnEmail(strUserName);
            strUserName = GetWindowsUsernameAsPerNorthgate(strUserName, out domainName);

            strUserEmail = strUserName + "@" + domainName;

            //Google change point to northgate
            //if (strUserName.ToLower().Contains("@rave-tech.co.in"))
            //{
            //    strUserName = strUserName.Replace("@rave-tech.co.in", "");
            //    //strUserEmail = strUserName + "@" + AuthorizationManagerConstants.RAVEDOMAINEMAIL;
            //    strUserName = GetWindowsUsernameAsPerNorthgate(strUserName);
            //    strUserEmail = strUserName + "@" + AuthorizationManagerConstants.RAVEDOMAINEMAIL;
            //}
            //else
            //{
            //    strUserName = strUserName.Replace(AuthorizationManagerConstants.NORTHGATEDOMAIN, "");
            //    //strUserEmail = strUserName + "@" + AuthorizationManagerConstants.NORTHGATEDOMAINEMAIL;
            //    strUserName = GetWindowsUsernameAsPerNorthgate(strUserName);
            //    strUserEmail = strUserName + "@" + AuthorizationManagerConstants.NORTHGATEDOMAINEMAIL;
            //}



            //DirectoryEntry searchRoot = new DirectoryEntry(AuthorizationManagerConstants.DIRECTORYSERVICE);

            //DirectorySearcher search = new DirectorySearcher(searchRoot);

            //string query = "(SAMAccountName=" + strUserName + ")";

            //search.Filter = query;

            //SearchResult result;

            //SearchResultCollection resultCol = search.FindAll();

            //if (resultCol != null)
            //{

            //    for (int counter = 0; counter < resultCol.Count; counter++)
            //    {

            //        result = resultCol[counter];

            //        if (result.Properties.Contains("samaccountname"))
            //        {

            //            strUserEmail = result.Properties["mail"][0].ToString();

            //        }

            //    }

            //}

            return strUserEmail;
        }

        /// <summary>
        /// Authorize User for Page View
        /// </summary>
        public void AuthorizeUserForPageView(object[] operationsId)
        {
            bool bCheckAccess = false;
            try
            {
                //--Check acess for project summary sub menu
                bCheckAccess = CheckAccessPermissions(operationsId);
                if (!bCheckAccess)
                {
                    HttpContext.Current.Response.Redirect(CommonConstants.UNAUTHORISEDUSER, false);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME_RP, "AuthorizeUserForPageView", EventIDConstants.AUTHORIZATION_MANAGER_ERROR);
            }
        }

        /// <summary>
        /// Authorize User for Page Operations
        /// </summary>
        public bool AuthorizeUserForPageOperations(object[] operationsId)
        {
            bool bCheckAccess = false;
            try
            {
                //--Check acess
                bCheckAccess = CheckAccessPermissions(operationsId);
                return bCheckAccess;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME_RP, "AuthorizeUserForPageOperations", EventIDConstants.AUTHORIZATION_MANAGER_ERROR);
            }
        }

        /// <summary>
        /// Gets the domain user name
        /// </summary>
        public string GetDomainUserName(string strUserName)
        {
            //strUserName = strUserName.Replace("@rave-tech.co.in", "");
            //GoogleMail
            string strUserEmail = string.Empty;
            //Googleconfigurable

            //strUserName = strUserName.ToLower().Replace("@rave-tech.co.in", "");
            //strUserName = strUserName.ToLower().Replace("@" + AuthorizationManagerConstants.NORTHGATEDOMAIN, "");
            strUserName = GetUsernameBasedOnEmail(strUserName);
            strUserEmail = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(strUserName.Replace(".", " "));

            //Google change point to northgate
            //if (strUserName.ToLower().Contains("@rave-tech.co.in"))
            //{
            //    strUserName = strUserName.Replace("@rave-tech.co.in", "");
            //    //strUserEmail = strUserName + "@" + AuthorizationManagerConstants.RAVEDOMAINEMAIL;
            //    strUserName = GetWindowsUsernameAsPerNorthgate(strUserName);
            //    //strUserEmail = strUserName + "@" + AuthorizationManagerConstants.RAVEDOMAINEMAIL;
            //    strUserEmail = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(strUserName.Replace(".", " "));
            //}
            //else
            //{
            //    strUserName = strUserName.Replace(AuthorizationManagerConstants.NORTHGATEDOMAIN, "");
            //    //strUserEmail = strUserName + "@" + AuthorizationManagerConstants.NORTHGATEDOMAINEMAIL;
            //    strUserName = GetWindowsUsernameAsPerNorthgate(strUserName);
            //    //strUserEmail = strUserName + "@" + AuthorizationManagerConstants.NORTHGATEDOMAINEMAIL;
            //    strUserEmail = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(strUserName.Replace(".", " "));
            //}



            //DirectoryEntry searchRoot = new DirectoryEntry(AuthorizationManagerConstants.DIRECTORYSERVICE);

            //DirectorySearcher search = new DirectorySearcher(searchRoot);

            //string query = "(SAMAccountName=" + strUserName + ")";

            //search.Filter = query;

            //SearchResult result;

            //SearchResultCollection resultCol = search.FindAll();

            //if (resultCol != null)
            //{

            //    for (int counter = 0; counter < resultCol.Count; counter++)
            //    {

            //        result = resultCol[counter];

            //        if (result.Properties.Contains("samaccountname"))
            //        {

            //            strUserEmail = result.Properties["displayname"][0].ToString();

            //        }

            //    }

            //}

            return strUserEmail;
        }

        #endregion Member Functions

    }
}
