//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           QualificationDetails.cs       
//  Author:         vineet.kulkarni
//  Date written:   21/08/2009 11:15:40 AM
//  Description:    This class provides the business layer methods for Employee module.
//
//  Amendments
//  Date                    Who                 Ref     Description
//  ----                    -----------         ---     -----------
//  21/08/2009 11:15:40 AM  vineet.kulkarni     n/a     Created    
//  03/09/2009 02:58:32 PM  vineet.kulkarni     n/a     Added comments and defined class level constants
//
//------------------------------------------------------------------------------

using System;
using Common;

namespace Rave.HR.BusinessLayer.Employee
{

    /// <summary>
    /// This class provides business layer for employees qualification details
    /// </summary>
    public class QualificationDetails
    {
        #region Private Member Variables

        /// <summary>
        /// private string variable for Class Name used in each catch block
        /// </summary>
        private string CLASS_NAME = "QualificationDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string ADDQUALIFICATIONDETAILS = "AddQualificationDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string UPDATEQUALIFICATIONDETAILS = "UpdateQualificationDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string GETQUALIFICATIONDETAILS = "GetQualificationDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string DELETEQUALIFICATIONDETAILS = "DeleteQualificationDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string MANIPULATION = "Manipulation";

        #endregion Private Member Variables

        #region Public Member Functions

        /// <summary>
        /// Adds the qualification details.
        /// </summary>
        /// <param name="objAddQualificationDetails">The object add qualification details.</param>
        public void AddQualificationDetails(BusinessEntities.QualificationDetails objAddQualificationDetails)
        {
            //Object declaration of QualificationDetails class
            Rave.HR.DataAccessLayer.Employees.QualificationDetails objAddQualificationDetailsDAL;

            try
            {
                //Created new instance of QualificationDetails class to call AddQualificationDetails() of Data access layer
                objAddQualificationDetailsDAL = new Rave.HR.DataAccessLayer.Employees.QualificationDetails();

                //Call to AddQualificationDetails() of Data access layer
                objAddQualificationDetailsDAL.AddQualificationDetails(objAddQualificationDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, ADDQUALIFICATIONDETAILS, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Updates the qualification details.
        /// </summary>
        /// <param name="objUpdateQualificationDetails">The object update qualification details.</param>
        public void UpdateQualificationDetails(BusinessEntities.QualificationDetails objUpdateQualificationDetails)
        {
            //Object declaration of QualificationDetails class
            Rave.HR.DataAccessLayer.Employees.QualificationDetails objUpdateQualificationDetailsDAL;

            try
            {
                //Created new instance of QualificationDetails class to call UpdateQualificationDetails() of Data access layer
                objUpdateQualificationDetailsDAL = new Rave.HR.DataAccessLayer.Employees.QualificationDetails();

                //Call to UpdateQualificationDetails() of Data access layer
                objUpdateQualificationDetailsDAL.UpdateQualificationDetails(objUpdateQualificationDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, UPDATEQUALIFICATIONDETAILS, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Gets the qualification details.
        /// </summary>
        /// <param name="objGetQualificationDetails">The object get qualification details.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetQualificationDetails(BusinessEntities.QualificationDetails objGetQualificationDetails)
        {
            //Object declaration of QualificationDetails class
            Rave.HR.DataAccessLayer.Employees.QualificationDetails objGetQualificationDetailsDAL;

            try
            {
                //Created new instance of QualificationDetails class to call objGetQualificationDetailsDAL() of Data access layer
                objGetQualificationDetailsDAL = new Rave.HR.DataAccessLayer.Employees.QualificationDetails();

                //Call to GetQualificationDetails() of Data access layer and return the Qualifications
                return objGetQualificationDetailsDAL.GetQualificationDetails(objGetQualificationDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, GETQUALIFICATIONDETAILS, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Deletes the qualification details.
        /// </summary>
        /// <param name="objDeleteQualificationDetails">The object delete qualification details.</param>
        public void DeleteQualificationDetails(BusinessEntities.QualificationDetails objDeleteQualificationDetails)
        {
            //Object declaration of QualificationDetails class
            Rave.HR.DataAccessLayer.Employees.QualificationDetails objDeleteQualificationDetailsDAL;

            try
            {
                //Created new instance of QualificationDetails class to call DeleteQualificationDetails() of Data access layer
                objDeleteQualificationDetailsDAL = new Rave.HR.DataAccessLayer.Employees.QualificationDetails();

                //Call to DeleteQualificationDetails() of Data access layer
                objDeleteQualificationDetailsDAL.DeleteQualificationDetails(objDeleteQualificationDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, DELETEQUALIFICATIONDETAILS, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Manipulates the specified obj qualification details list.
        /// </summary>
        /// <param name="objQualificationDetailsList">The obj qualification details list.</param>
        public void Manipulation(BusinessEntities.RaveHRCollection objQualificationDetailsCollection)
        {
            try
            {
                //Check for each each entry in QualificationDetails List
                foreach (BusinessEntities.QualificationDetails objAddQualificationDetails in objQualificationDetailsCollection)
                {
                    //If mode of the entry is Add i.e. 1 then call Add()
                    if (objAddQualificationDetails.Mode == 1)
                    {
                        this.AddQualificationDetails(objAddQualificationDetails);
                    }

                    //If mode of the entry is Update i.e. 2 then call Update()
                    if (objAddQualificationDetails.Mode == 2)
                    {
                        this.UpdateQualificationDetails(objAddQualificationDetails);
                    }

                    //If QualificationId is greater than zero i.e. qualification is already added and mode of the entry is Delete i.e. 3 then call Delete()
                    if (objAddQualificationDetails.QualificationId > 0 && objAddQualificationDetails.Mode == 3)
                    {
                        this.DeleteQualificationDetails(objAddQualificationDetails);
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

        #endregion Public Member Functions
    }
}