//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           MRFRole.aspx      
//  Author:         Chhaya Gunjal 
//  Date written:   03/09/2009/ 10:58:30 AM
//  Description:    This class provides the authorization functionality for MRF module.
//
//  Amendments
//  Date                     Who              Ref     Description
//  ----                     -----------      ---     -----------
//  03/09/2009 10:58:30 AM   Chhaya Gunjal    n/a     Created    
//
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;

using System.Collections;
using Common;
using Common.AuthorizationManager;

namespace Rave.HR.BusinessLayer.MRF
{
    public class MRFRoles
    {
        #region Variable
        public string PMRole;
        public string COORole;
        public string RPMRole;
        public string PresalesRole;

        private const string CLASS_NAME = "MRFRoles.cs";
        #endregion

        #region Methods
        //To check Role for Raise MRF.
        //For RPM,AM/GPM/APM,FM,CFM,Presales is onle able to see the list
        public static Boolean CheckRolesRaiseMrf()
        {
            MRFRoles mrfRoles = new MRFRoles();
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

                        case AuthorizationManagerConstants.ROLEGPM:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEAPM:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEFM:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLERPM:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEPRESALES:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLECFM:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLERECRUITMENT:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEHR:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLETESTING:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEQUALITY:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEMH:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLERAVECONSULTANT:
                            validUser = true;
                            return validUser;

                        default:
                            break;
                    }

                }
            }
            return validUser;
        }

        //To check Role for Raise MRF.
        //For FM,CFM is onle able to see the list
        public static Boolean CheckRolesPendingApproval()
        {
            MRFRoles mrfRoles = new MRFRoles();
            ArrayList arrRolesForUser = GetAuthorizeUserRoles();
            Boolean validUser = false;
            if (arrRolesForUser.Count >= 1)
            {
                foreach (string STR in arrRolesForUser)
                {
                    switch (STR)
                    {
                        case AuthorizationManagerConstants.ROLEFM:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLECFM:
                            validUser = true;
                            return validUser;

                        default:
                            break;
                    }

                }
            }
            return validUser;
        }

        //To check Role for Raise MRF.
        //For RPM,AM/GPM/APM,FM,CFM,Presales,CEO,COO,HR is onle able to see the list
        public static Boolean CheckRolesMrfSummary()
        {
            MRFRoles mrfRoles = new MRFRoles();
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

                        case AuthorizationManagerConstants.ROLEGPM:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEAPM:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEFM:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLERPM:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEPRESALES:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLECFM:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLECOO:
                            validUser = true;
                            return validUser;

                            /*RoleHR changed To RoleRecruitment*/
                        case AuthorizationManagerConstants.ROLERECRUITMENT:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLECEO:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEHR:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLETESTING:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEQUALITY:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLEMH:
                            validUser = true;
                            return validUser;
                       
                        case AuthorizationManagerConstants.ROLERAVECONSULTANT:
                            validUser = true;
                            return validUser;
                        default:
                            break;
                    }

                }
            }
            return validUser;
        }


        //To check Role for Approve Reject head Count.
        //For CEO,COO and RPM is onle able to see the list
        public static Boolean CheckRolesApproveRejectHeadCount()
        {
            MRFRoles mrfRoles = new MRFRoles();
            ArrayList arrRolesForUser = GetAuthorizeUserRoles();
            Boolean validUser=false;
            if (arrRolesForUser.Count >= 1)
            {
                foreach (string STR in arrRolesForUser)
                {
                    switch (STR)
                    {
                        case AuthorizationManagerConstants.ROLECOO:
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
                           
                        default:
                            break;
                    }

                }
            }
            return validUser;
        }


        ////To check Role for Approve Reject head Count.
        //For CEO,COO and RPM is onle able to see the list
        public static Boolean CheckRolesPendingAllocation()
        {
            ArrayList arrRolesForUser = GetAuthorizeUserRoles();
            Boolean validUser = false;
            if (arrRolesForUser.Count >= 1)
            {
                foreach (string STR in arrRolesForUser)
                {
                    switch (STR)
                    {
                        case AuthorizationManagerConstants.ROLERPM:
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
            ArrayList arrRolesForUser= new ArrayList();;
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
        
        //To Get Role for Approve Reject head Count.
        //For CEO,COO and RPM is onle able to see the list
        //RPM=CEO+COO Authentication
        //RPM=CEO+RPM
        //RPM=COO+RPM
        //CEO=1;
        //COO=3
        public static string GetRolesApproveRejectHeadCount()
        {
            MRFRoles mrfRoles = new MRFRoles();
            ArrayList arrRolesForUser = GetAuthorizeUserRoles();
            int validUser = 2;
            if (arrRolesForUser.Count >= 1)
            {
                foreach (string STR in arrRolesForUser)
                {
                    switch (STR)
                    {
                        case AuthorizationManagerConstants.ROLECOO:
                            validUser++;
                            break;
                        case AuthorizationManagerConstants.ROLERPM:
                            return AuthorizationManagerConstants.ROLERPM;

                        case AuthorizationManagerConstants.ROLECEO:
                            validUser--;
                            break;

                        default:
                            break;
                    }
                }
                if (validUser == 3)
                    return AuthorizationManagerConstants.ROLECOO;
                else if (validUser == 2)
                    return AuthorizationManagerConstants.ROLERPM;
                else if (validUser == 1)
                    return AuthorizationManagerConstants.ROLECEO;
            }
            return AuthorizationManagerConstants.ROLERPM;
           
        }
        public static string ConvertToUpper(string InputString)
        {
            InputString = InputString.Substring(0, 1).ToUpper() + InputString.Substring(1, InputString.Length - 1);
            return InputString;
        }
        #endregion 
    }
}
