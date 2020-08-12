//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           CertificationDetails.cs       
//  Author:         Shrinivas.Dalavi
//  Date written:   27/08/2009 12:15:40 AM
//  Description:    This class provides the business layer methods for Employee module.
//
//  Amendments
//  Date                    Who                 Ref     Description
//  ----                    -----------         ---     -----------
//  27/08/2009 12:15:40 AM  Shrinivas.Dalavi     n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using Common;

namespace Rave.HR.BusinessLayer.Employee
{
    public class CertificationDetails
    {
        #region Private Member Variables

        /// <summary>
        /// private variable for Class Name used in each catch block
        /// </summary>
        private string CLASS_NAME = "CertificationDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string ADDCERTIFICATIONDETAILS = "AddCertificationDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string UPDATECERTIFICATIONDETAILS = "UpdateCertificationDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string GETCERTIFICATIONDETAILS = "GetCertificationDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string DELETECERTIFICATIONDETAILS = "DeleteCertificationDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string MANIPULATION = "Manipulation";

        #endregion Private Member Variables

        #region Public Member Functions

        /// <summary>
        /// Adds the certification details.
        /// </summary>
        /// <param name="objAddCertificationDetails">The obj add certification details.</param>
        public void AddCertificationDetails(BusinessEntities.CertificationDetails objAddCertificationDetails)
        {
            //Object declaration of objAddCertificationDetailsDAL class
            Rave.HR.DataAccessLayer.Employees.CertificationDetails objAddCertificationDetailsDAL;

            try
            {
                //Created new instance of CertificationDetails class to call AddCertificationDetails() of Data access layer
                objAddCertificationDetailsDAL = new Rave.HR.DataAccessLayer.Employees.CertificationDetails();

                //Call to AddCertificationDetails() of Data access layer
                objAddCertificationDetailsDAL.AddCertificationDetails(objAddCertificationDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, ADDCERTIFICATIONDETAILS, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Updates the certification details.
        /// </summary>
        /// <param name="objUpdateCertificationDetails">The obj update certification details.</param>
        public void UpdateCertificationDetails(BusinessEntities.CertificationDetails objUpdateCertificationDetails)
        {
            //Object declaration of CertificationDetails class
            Rave.HR.DataAccessLayer.Employees.CertificationDetails objUpdateCertificationDetailsDAL;

            try
            {
                //Created new instance of CertificationDetails class to call UpdateCertificationDetails() of Data access layer
                objUpdateCertificationDetailsDAL = new Rave.HR.DataAccessLayer.Employees.CertificationDetails();

                //Call to UpdateCertificationDetails() of Data access layer
                objUpdateCertificationDetailsDAL.UpdateCertificationDetails(objUpdateCertificationDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, UPDATECERTIFICATIONDETAILS, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Gets the certification details.
        /// </summary>
        /// <param name="objGetCertificationDetails">The obj get certification details.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetCertificationDetails(BusinessEntities.CertificationDetails objGetCertificationDetails)
        {
            //Object declaration of QualificationDetails class
            Rave.HR.DataAccessLayer.Employees.CertificationDetails objGetCertificationDetailsDAL;
            
            try
            {
                //Created new instance of QualificationDetails class to call objGetQualificationDetailsDAL() of Data access layer
                objGetCertificationDetailsDAL = new Rave.HR.DataAccessLayer.Employees.CertificationDetails();

                //Call to GetQualificationDetails() of Data access layer and return the Qualifications
                return objGetCertificationDetailsDAL.GetCertificationDetails(objGetCertificationDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, GETCERTIFICATIONDETAILS, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }           
        }

        /// <summary>
        /// Deletes the certification details.
        /// </summary>
        /// <param name="objDeleteCertificationDetails">The obj delete certification details.</param>
        public void DeleteCertificationDetails(BusinessEntities.CertificationDetails objDeleteCertificationDetails)
        {
            //Object declaration of CertificationDetails class
            Rave.HR.DataAccessLayer.Employees.CertificationDetails objDeleteCertificationDetailsDAL;

            try
            {
                //Created new instance of CertificationDetails class to call DeleteCertificationDetails() of Data access layer
                objDeleteCertificationDetailsDAL = new Rave.HR.DataAccessLayer.Employees.CertificationDetails();

                //Call to DeleteCertificationDetails() of Data access layer
                objDeleteCertificationDetailsDAL.DeleteCertificationDetails(objDeleteCertificationDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, DELETECERTIFICATIONDETAILS, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Manipulations the specified obj certification details list.
        /// </summary>
        /// <param name="objCertificationDetailsList">The obj certification details list.</param>
        public void Manipulation(BusinessEntities.RaveHRCollection objCertificationDetailsCollection)
        {
            try
            {
                //Check for each each entry in CertificationDetails List
                foreach (BusinessEntities.CertificationDetails objAddCertificationDetails in objCertificationDetailsCollection)
                {
                    //If mode of the entry is Add i.e. 1 then call Add()
                    if (objAddCertificationDetails.Mode == 1)
                    {
                        this.AddCertificationDetails(objAddCertificationDetails);

                    }

                    //If mode of the entry is Update i.e. 2 then call Update()
                    if (objAddCertificationDetails.Mode == 2)
                    {
                        this.UpdateCertificationDetails(objAddCertificationDetails);

                    }

                    //If CertificationId is greater than zero i.e. Certification is already added and mode of the entry is Delete i.e. 3 then call Delete()
                    if (objAddCertificationDetails.Mode == 3)
                    {
                        this.DeleteCertificationDetails(objAddCertificationDetails);
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
