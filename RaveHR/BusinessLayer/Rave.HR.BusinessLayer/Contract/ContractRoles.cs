//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           ContractRoles.aspx      
//  Author:         Kanchan/Yagendra
//  Date written:   17/09/2009 7:58:30 AM 
//  Description:    This class provides the authorization functionality for Contract module.
//
//  Amendments
//  Date                     Who              Ref     Description
//  ----                     -----------      ---     -----------
//  17/09/2009 7:58:30 AM   Kanchan/Yagendra   n/a     Created    
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using Common;
using Common.AuthorizationManager;

namespace Rave.HR.BusinessLayer.Contract
{
   public class ContractRoles
    {

        #region Variable
        public string PMRole;
        public string COORole;
        public string RPMRole;
        public string PresalesRole;

        private const string CLASS_NAME = "MRFRoles.cs";

        #endregion 

        //To check Role for Contract Summary.
        //For RPM,AM/GPM/APM,FM,CFM,Presales is onle able to see the list.
        //these roles are also used for View  Add contract page.
        public static Boolean CheckRolesContractSummary()
        {
            ContractRoles contractRoles = new ContractRoles();
            ArrayList arrRolesForUser = GetAuthorizeUserRoles();
            Boolean validUser = false;
            if (arrRolesForUser.Count >= 1)
            {
                foreach (string STR in arrRolesForUser)
                {
                    switch (STR)
                    {
                        case AuthorizationManagerConstants.ROLECEO:
                            validUser = true;
                            return validUser;

                        //case AuthorizationManagerConstants.ROLEGPM:
                        //    validUser = true;
                        //    return validUser;

                        case AuthorizationManagerConstants.ROLECOO:
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

                       default:
                            break;
                    }

                }
            }
            return validUser;
        }


        //To check Role for Add contract.
        //For RPM,FM,CFM,Presales is onle able to ad the contract.
        public static Boolean CheckRolesOnlyViewAddContract()
        {
            ContractRoles contractRoles = new ContractRoles();
            ArrayList arrRolesForUser = GetAuthorizeUserRoles();
            Boolean validUser = false;
            if (arrRolesForUser.Count >= 1)
            {
                foreach (string STR in arrRolesForUser)
                {
                    switch (STR)
                    {
                       
                        case AuthorizationManagerConstants.ROLECOO:
                            validUser = true;
                            return validUser;

                        case AuthorizationManagerConstants.ROLECEO:
                            validUser = true;
                            return validUser;
                      
                        default:
                            break;
                    }

                }
            }
            return validUser;
        }

        //To check Role for Add internal contract.
        //For RPM,Presales is only able to add rave internal contract.
        public static Boolean CheckRolesAddInternalContract()
        {
            ContractRoles contractRoles = new ContractRoles();
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

                        case AuthorizationManagerConstants.ROLEPRESALES:
                            validUser = true;
                            return validUser;


                        default:
                            break;
                    }

                }
            }
            return validUser;
        }


        //To check Role for Add internal contract.
        //For RPM,Presales is only able to add rave internal contract.
        public static Boolean CheckRolesAddExternalContract()
        {
            ContractRoles contractRoles = new ContractRoles();
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

    }
}
