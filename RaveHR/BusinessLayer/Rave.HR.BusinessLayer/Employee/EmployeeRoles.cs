using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Common;
using Common.AuthorizationManager;

namespace Rave.HR.BusinessLayer.Employee
{
    public class EmployeeRoles
    {
        #region Local member Declaration

        const string CLASS_NAME = "EmployeeRoles.cs";

        #endregion Local member Declaration

        #region Public Methodes

        public static Boolean CheckRolesEmployee()
        {
            EmployeeRoles AllocationRole = new EmployeeRoles();
            ArrayList arrRolesForUser = GetAuthorizeUserRoles();
            Boolean validUser = false;
            if (arrRolesForUser.Count >= 1)
            {
                foreach (string STR in arrRolesForUser)
                {
                    switch (STR)
                    {
                        case AuthorizationManagerConstants.ROLEHR:
                            validUser = true;
                            return validUser;

                        default:
                            break;
                    }

                }
            }
            return validUser;
        }

        /// <summary>
        /// CheckRoles For Employee Summary And Profile
        /// </summary>
        public static Boolean CheckRolesEmployeeSummaryAndProfile()
        {
            EmployeeRoles AllocationRole = new EmployeeRoles();
            ArrayList arrRolesForUser = GetAuthorizeUserRoles();
            Boolean validUser = false;
            if (arrRolesForUser.Count >= 1)
            {
                foreach (string STR in arrRolesForUser)
                {
                    switch (STR)
                    {

                        case AuthorizationManagerConstants.ROLEPM:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEPRESALES:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLERPM:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEAPM:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEGPM:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEFINANCE:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLECEO:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLECFM:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEFM:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLERECRUITMENT:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEHR:
                            validUser = true;
                            return validUser;

                        default:
                            break;
                    }

                }
            }
            return validUser;
        }

        public static Boolean CheckRolesEmployeeSummary()
        {
            EmployeeRoles contractRoles = new EmployeeRoles();
            ArrayList arrRolesForUser = GetAuthorizeUserRoles();
            Boolean validUser = false;
            if (arrRolesForUser.Count >= 1)
            {
                foreach (string STR in arrRolesForUser)
                {
                    switch (STR)
                    {
                        case AuthorizationManagerConstants.ROLEHR:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEPM:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEFINANCE:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEPRESALES:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLECOO:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEMH:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLERPM:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLECEO:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLECFM:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEFM:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEGPM:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEAPM:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLESPM:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLERECRUITMENT:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEADMIN:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLETESTING:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEQUALITY:
                            validUser = true;
                            return validUser;

                        default:
                            break;
                    }

                }
            }
            return validUser;
        }

        private static ArrayList GetAuthorizeUserRoles()
        {
            ArrayList arrRolesForUser = new ArrayList(); ;
            try
            {
                AuthorizationManager objRaveHRAuthorizationManager = new AuthorizationManager();
                arrRolesForUser = objRaveHRAuthorizationManager.getRolesForUser(objRaveHRAuthorizationManager.getLoggedInUser());
                return arrRolesForUser;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                RaveHRException objEx = new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "AuthorizeUser", EventIDConstants.RAVE_HR_PROJECTS_BUSNIESS_LAYER);
            }
            return arrRolesForUser;
        }

        #endregion Public Methodes
    }
}
