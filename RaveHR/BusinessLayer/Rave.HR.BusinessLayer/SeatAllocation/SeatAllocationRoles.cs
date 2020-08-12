//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           SeatAllocation.aspx      
//  Author:         Kanchan
//  Date written:   25/11/2009 4:00:00 pm 
//  Description:    This class provides the authorization functionality for Seat Allocation module.
//
//  Amendments
//  Date                     Who              Ref     Description
//  ----                     -----------      ---     -----------
//  25/11/2009 4:00:00 pm    Kanchan          n/a     Created    
//
//------------------------------------------------------------------------------
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;
using Common;
using Common.AuthorizationManager;

namespace Rave.HR.BusinessLayer.SeatAllocation
{
    
    public class SeatAllocationRoles
    {
        #region Local member Declaration

        const string CLASS_NAME = "SeatAllocationRoles.cs";

        #endregion Local member Declaration

        #region Public Methodes

        public static Boolean CheckRolesSeatAllocation()
        { 
            SeatAllocationRoles AllocationRole = new SeatAllocationRoles();
            ArrayList arrRolesForUser = GetAuthorizeUserRoles();
            Boolean validUser = false;
            if (arrRolesForUser.Count >= 1)
            {
                foreach (string STR in arrRolesForUser)
                {
                    switch (STR)
                    {
                        case AuthorizationManagerConstants.ROLEADMIN:
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
