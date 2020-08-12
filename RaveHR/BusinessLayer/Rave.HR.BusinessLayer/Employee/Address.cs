//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           Address.cs       
//  Author:         Sudip.Guha
//  Date written:   20/11/2009 12:15:40 AM
//  Description:    This class provides the business layer methods for Employee module.
//
//  Amendments
//  Date                    Who                 Ref     Description
//  ----                    -----------         ---     -----------
//  20/11/2009 12:15:40 AM  Sudip.Guha          n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using Common;

namespace Rave.HR.BusinessLayer.Employee
{
    public class Address
    {
        #region Private Member Variables

        /// <summary>
        /// private variable for Class Name used in each catch block
        /// </summary>
        private string CLASS_NAME = "Address";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string ADDADDRESS = "AddAddress";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string UPDATEADDRESS = "UpdateAddress";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string GETADDRESS = "GetAddress";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string DELETEADDRESS = "DeleteAddress";

        #endregion Private Member Variables

        #region Public Member Functions

        /// <summary>
        /// Adds the address.
        /// </summary>
        /// <param name="objAddCertificationDetails">The obj add certification details.</param>
        public void AddAddress(BusinessEntities.Address objAddress)
        {
            //Object declaration of objAddAddressDAL class
            Rave.HR.DataAccessLayer.Employees.Address objAddAddressDAL;

            try
            {
                //Created new instance of CertificationDetails class to call AddCertificationDetails() of Data access layer
                objAddAddressDAL = new Rave.HR.DataAccessLayer.Employees.Address();

                //Call to AddCertificationDetails() of Data access layer
                objAddAddressDAL.AddAddress(objAddress);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, ADDADDRESS, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <param name="objEmployee">The obj get certification details.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetAddress(BusinessEntities.Employee objEmployee)
        {
            //Object declaration of QualificationDetails class
            Rave.HR.DataAccessLayer.Employees.Address objAddressDAL;

            try
            {
                //Created new instance of QualificationDetails class to call objGetQualificationDetailsDAL() of Data access layer
                objAddressDAL = new Rave.HR.DataAccessLayer.Employees.Address();

                //Call to GetQualificationDetails() of Data access layer and return the Qualifications
                return objAddressDAL.GetEmployeeAddress(objEmployee);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, GETADDRESS, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }


        /// <summary>
        /// Manipulates the address.
        /// </summary>
        /// <param name="objAddress">The obj address.</param>
        public void ManipulateAddress(BusinessEntities.Address objAddress)
        {
            //Object declaration of objAddAddressDAL class
            Rave.HR.DataAccessLayer.Employees.Address objManipulateAddressDAL;

            try
            {
                //Created new instance of CertificationDetails class to call AddCertificationDetails() of Data access layer
                objManipulateAddressDAL = new Rave.HR.DataAccessLayer.Employees.Address();

                //Call to AddCertificationDetails() of Data access layer
                objManipulateAddressDAL.ManipulateAddress(objAddress);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, ADDADDRESS, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        #endregion 
    }
}
