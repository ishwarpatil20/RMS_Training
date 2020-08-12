//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           MRFRole.aspx      
//  Author:         Gaurav Thakkar 
//  Date written:   23/09/2009/ 10:50:30 AM
//  Description:    This class provides the authorization functionality for Recruitment module.
//
//  Amendments
//  Date                     Who              Ref     Description
//  ----                     -----------      ---     -----------
//  23/09/2009/ 10:50:30 AM  Gaurav Thakkar    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Common.AuthorizationManager;

namespace Rave.HR.BusinessLayer.Recruitment
{
    public class RecruitmentRoles
    {
        #region Private Variables

        /// <summary>
        /// Class Name
        /// </summary>
        private const string CLASS_NAME = "RecruitmentRoles.cs";

        #endregion Private Variables        

        #region Public Methods

        /// <summary>
        /// Authorise user for Recruitment Tab
        /// </summary>
        /// <returns>Boolean</returns>
        public static Boolean CheckRolesForRecruitmentTab()
        {
            ArrayList arrRolesForUser = GetAuthorizeUserRoles();
            Boolean validUser = false;
            if (arrRolesForUser.Count >= 1)
            {
                foreach (string STR in arrRolesForUser)
                {
                    switch (STR)
                    {
                        case AuthorizationManagerConstants.ROLERECRUITMENT:
                            validUser = true;
                            return validUser;                        

                        default:
                            break;
                    }

                }
            }
            return validUser;
        }

        #endregion Public Methods

        #region Private Method

        /// <summary>
        /// Get toles from Azman
        /// </summary>
        /// <returns>Arraylist</returns>
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

        #endregion Private Method
    }
}
