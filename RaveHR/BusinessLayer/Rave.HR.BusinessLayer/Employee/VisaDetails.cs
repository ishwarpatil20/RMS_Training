//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           VisaDetails.cs       
//  Author:         Shrinivas.Dalavi
//  Date written:   27/08/2009 2:15:40 AM
//  Description:    This class provides the business layer methods for Employee module.
//
//  Amendments
//  Date                    Who                 Ref     Description
//  ----                    -----------         ---     -----------
//  27/08/2009 2:15:40 AM  Shrinivas.Dalavi     n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Common;

namespace Rave.HR.BusinessLayer.Employee
{
    public class VisaDetails
    {
        #region Private Member Variables

        /// <summary>
        /// private variable for Class Name used in each catch block
        /// </summary>
        private string CLASS_NAME = "VisaDetails";

        private string Fn_AddVisaDetails = "AddVisaDetails";

        private string Fn_UpdateVisaDetails = "UpdateVisaDetails";

        private string Fn_GetProfessionalDetails = "GetProfessionalDetails";

        private string Fn_DeleteVisaDetails = "DeleteVisaDetails";

        private string Fn_DeleteVisaDetailsByEmpId = "DeleteVisaDetailsByEmpId";

        #endregion Private Member Variables

        #region Public Member Functions

        /// <summary>
        /// Adds the visa details.
        /// </summary>
        /// <param name="objAddVisaDetails">The obj add visa details.</param>
        public void AddVisaDetails(BusinessEntities.VisaDetails objAddVisaDetails)
        {
            Rave.HR.DataAccessLayer.Employees.VisaDetails objAddVisaDetailsDAL;

            try
            {
                objAddVisaDetailsDAL = new Rave.HR.DataAccessLayer.Employees.VisaDetails();

                objAddVisaDetailsDAL.AddVisaDetails(objAddVisaDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, Fn_AddVisaDetails, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

        }

        /// <summary>
        /// Updates the visa details.
        /// </summary>
        /// <param name="objUpdateVisaDetails">The obj update visa details.</param>
        public void UpdateVisaDetails(BusinessEntities.VisaDetails objUpdateVisaDetails)
        {
            Rave.HR.DataAccessLayer.Employees.VisaDetails objUpdateVisaDetailsDAL;

            try
            {
                objUpdateVisaDetailsDAL = new Rave.HR.DataAccessLayer.Employees.VisaDetails();

                objUpdateVisaDetailsDAL.UpdateVisaDetails(objUpdateVisaDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, Fn_UpdateVisaDetails, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Gets the visa details.
        /// </summary>
        /// <param name="objGetVisaDetails">The obj get visa details.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetVisaDetails(BusinessEntities.VisaDetails objGetVisaDetails)
        {
            //Object declaration of QualificationDetails class
            Rave.HR.DataAccessLayer.Employees.VisaDetails objGetVisaDetailsDAL;

            try
            {
                //Created new instance of QualificationDetails class to call objGetQualificationDetailsDAL() of Data access layer
                objGetVisaDetailsDAL = new Rave.HR.DataAccessLayer.Employees.VisaDetails();

                //Call to GetQualificationDetails() of Data access layer and return the Qualifications
                return objGetVisaDetailsDAL.GetVisaDetails(objGetVisaDetails);

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, Fn_GetProfessionalDetails, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Deletes the visa details.
        /// </summary>
        /// <param name="objDeleteVisaDetails">The obj delete visa details.</param>
        public void DeleteVisaDetails(BusinessEntities.VisaDetails objDeleteVisaDetails)
        {
            Rave.HR.DataAccessLayer.Employees.VisaDetails objDeleteVisaDetailsDAL;

            try
            {
                objDeleteVisaDetailsDAL = new Rave.HR.DataAccessLayer.Employees.VisaDetails();

                objDeleteVisaDetailsDAL.DeleteVisaDetails(objDeleteVisaDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, Fn_DeleteVisaDetails, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Manipulations the specified obj visa details list.
        /// </summary>
        /// <param name="objVisaDetailsList">The obj visa details list.</param>
        public void Manipulation(BusinessEntities.RaveHRCollection objVisaDetailsCollection)
        {
            foreach (BusinessEntities.VisaDetails objAddVisaDetails in objVisaDetailsCollection)
            {
                if (objAddVisaDetails.Mode == 1)
                {
                    this.AddVisaDetails(objAddVisaDetails);

                }
                if (objAddVisaDetails.Mode == 2)
                {
                    this.UpdateVisaDetails(objAddVisaDetails);

                }
                if (objAddVisaDetails.Mode == 3)
                {
                    this.DeleteVisaDetails(objAddVisaDetails);

                }
            }
        }

        /// <summary>
        /// Deletes the visa details by emp id.
        /// </summary>
        /// <param name="EmployeeId">The employee id.</param>
        public void DeleteVisaDetailsByEmpId(int EmployeeId)
        {
            Rave.HR.DataAccessLayer.Employees.VisaDetails objDeleteVisaDetailsDAL;

            try
            {
                objDeleteVisaDetailsDAL = new Rave.HR.DataAccessLayer.Employees.VisaDetails();

                objDeleteVisaDetailsDAL.DeleteVisaDetailsByEmpId(EmployeeId);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, Fn_DeleteVisaDetailsByEmpId, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        #endregion Public Member Functions
    }
}
