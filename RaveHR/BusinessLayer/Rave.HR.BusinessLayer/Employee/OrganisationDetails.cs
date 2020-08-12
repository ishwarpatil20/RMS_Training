//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           OrganisationDetails.cs       
//  Author:         Shrinivas.Dalavi
//  Date written:   27/08/2009 12:44:40 AM
//  Description:    This class provides the business layer methods for Employee module.
//
//  Amendments
//  Date                    Who                 Ref     Description
//  ----                    -----------         ---     -----------
//  27/08/2009 12:44:40 AM  Shrinivas.Dalavi     n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using Common;

namespace Rave.HR.BusinessLayer.Employee
{
    public class OrganisationDetails
    {
        #region Private Member Variables

        /// <summary>
        /// private variable for Class Name used in each catch block
        /// </summary>
        private string CLASS_NAME = "OrganisationDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string MANIPULATION = "Manipulation";

        #endregion Private Member Variables

        #region Public Member Functions

        /// <summary>
        /// Adds the organisation details.
        /// </summary>
        /// <param name="objAddOrganisationDetails">The obj add organisation details.</param>
        public void AddOrganisationDetails(BusinessEntities.OrganisationDetails objAddOrganisationDetails)
        {
            Rave.HR.DataAccessLayer.Employees.OrganisationDetails objAddOrganisationDetailsDAL;

            try
            {
                objAddOrganisationDetailsDAL = new Rave.HR.DataAccessLayer.Employees.OrganisationDetails();

                objAddOrganisationDetailsDAL.AddOrganisationDetails(objAddOrganisationDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "AddOrganisationDetails", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

        }

        /// <summary>
        /// Updates the organisation details.
        /// </summary>
        /// <param name="objUpdateOrganisationDetails">The obj update organisation details.</param>
        public void UpdateOrganisationDetails(BusinessEntities.OrganisationDetails objUpdateOrganisationDetails)
        {
            Rave.HR.DataAccessLayer.Employees.OrganisationDetails objUpdateOrganisationDetailsDAL;

            try
            {
                objUpdateOrganisationDetailsDAL = new Rave.HR.DataAccessLayer.Employees.OrganisationDetails();

                objUpdateOrganisationDetailsDAL.UpdateOrganisationDetails(objUpdateOrganisationDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "UpdateOrganisationDetails", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Gets the organisation details.
        /// </summary>
        /// <param name="objGetOrganisationDetails">The obj get organisation details.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetOrganisationDetails(BusinessEntities.OrganisationDetails objGetOrganisationDetails)
        {
            Rave.HR.DataAccessLayer.Employees.OrganisationDetails objGetOrganisationDetailsDAL;
            
            try
            {
                objGetOrganisationDetailsDAL = new Rave.HR.DataAccessLayer.Employees.OrganisationDetails();

                return objGetOrganisationDetailsDAL.GetOrganisationDetails(objGetOrganisationDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetOrganisationDetails", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
           
        }

        /// <summary>
        /// Deletes the organisation details.
        /// </summary>
        /// <param name="objDeleteOrganisationDetails">The obj delete organisation details.</param>
        public void DeleteOrganisationDetails(BusinessEntities.OrganisationDetails objDeleteOrganisationDetails)
        {
            Rave.HR.DataAccessLayer.Employees.OrganisationDetails objDeleteOrganisationDetailsDAL;

            try
            {
                objDeleteOrganisationDetailsDAL = new Rave.HR.DataAccessLayer.Employees.OrganisationDetails();

                objDeleteOrganisationDetailsDAL.DeleteOrganisationDetails(objDeleteOrganisationDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "DeleteOrganisationDetails", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Manipulations the specified obj organisation details list.
        /// </summary>
        /// <param name="objOrganisationDetailsList">The obj organisation details list.</param>
        public void Manipulation(BusinessEntities.RaveHRCollection OrganisationDetailsCollection)
        {
            try
            {
                foreach (BusinessEntities.OrganisationDetails objAddOrganisationDetails in OrganisationDetailsCollection)
                {
                    if (objAddOrganisationDetails.Mode == 1)
                    {
                        this.AddOrganisationDetails(objAddOrganisationDetails);

                    }
                    if (objAddOrganisationDetails.Mode == 2)
                    {
                        this.UpdateOrganisationDetails(objAddOrganisationDetails);

                    }
                    if (objAddOrganisationDetails.Mode == 3)
                    {
                        this.DeleteOrganisationDetails(objAddOrganisationDetails);

                    }
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, MANIPULATION, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Deletes the non organisation details.
        /// </summary>
        /// <param name="objDeleteOrganisationDetails">The obj delete organisation details.</param>
        public void DeleteNonOrganisationDetails(BusinessEntities.OrganisationDetails objDeleteOrganisationDetails)
        {
            Rave.HR.DataAccessLayer.Employees.OrganisationDetails objDeleteOrganisationDetailsDAL;

            try
            {
                objDeleteOrganisationDetailsDAL = new Rave.HR.DataAccessLayer.Employees.OrganisationDetails();

                objDeleteOrganisationDetailsDAL.DeleteNonOrganisationDetails(objDeleteOrganisationDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "DeleteOrganisationDetails", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }


        /// <summary>
        /// Gets the relevant experience.
        /// </summary>
        /// <param name="objGetOrganisationDetails">The obj get organisation details.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetRelevantExperience(BusinessEntities.OrganisationDetails objGetOrganisationDetails)
        {
            Rave.HR.DataAccessLayer.Employees.OrganisationDetails objGetOrganisationDetailsDAL;

            try
            {
                objGetOrganisationDetailsDAL = new Rave.HR.DataAccessLayer.Employees.OrganisationDetails();

                return objGetOrganisationDetailsDAL.GetRelevantExperience(objGetOrganisationDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetOrganisationDetails", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

        }
        
        // 28109-Ambar-Start
        // Added New Method
        public void UpdateTotalReleventExp(int AemployeeID, int ATotalMonths, int ATotalYears)//, int AReleventMonths, int AReleventYears)
        {
            Rave.HR.DataAccessLayer.Employees.OrganisationDetails objUpdateTotalReleventExpDAL;

            try
            {
              objUpdateTotalReleventExpDAL = new Rave.HR.DataAccessLayer.Employees.OrganisationDetails();

              objUpdateTotalReleventExpDAL.UpdateTotalReleventExp(AemployeeID, ATotalMonths, ATotalYears);//, AReleventMonths, AReleventYears);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
              throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "UpdateTotalReleventExp", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

        }
        // 28109-Ambar-End
      


        #endregion Public Member Functions
    }
}
