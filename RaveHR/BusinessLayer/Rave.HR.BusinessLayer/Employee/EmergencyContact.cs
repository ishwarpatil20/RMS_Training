//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           EmergencyContact.cs       
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
    public class EmergencyContact
    {
        #region Private Member Variables

        /// <summary>
        /// private variable for Class Name used in each catch block
        /// </summary>
        private string CLASS_NAME = "EmergencyContact";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string ADDEMERGENCYCONTACT = "AddEmergencyContact";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string UPDATEEMERGENCYCONTACT = "UpdateEmergencyContact";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string GETEMERGENCYCONTACT = "GetEmergencyContact";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string DELETEEMERGENCYCONTACT = "DeleteEmergencyContact";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string MANIPULATION = "Manipulation";

        #endregion Private Member Variables

        #region Public Member Functions

        /// <summary>
        /// Adds the address.
        /// </summary>
        /// <param name="objAddCertificationDetails">The obj add certification details.</param>
        public void AddEmergencyContact(BusinessEntities.EmergencyContact objEmergencyContact)
        {
            //Object declaration of objAddAddressDAL class
            Rave.HR.DataAccessLayer.Employees.EmergencyContact objAddEmergencyContactDAL;

            try
            {
                //Created new instance of CertificationDetails class to call AddCertificationDetails() of Data access layer
                objAddEmergencyContactDAL = new Rave.HR.DataAccessLayer.Employees.EmergencyContact();

                //Call to AddCertificationDetails() of Data access layer
                objAddEmergencyContactDAL.AddEmergencyContact(objEmergencyContact);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, ADDEMERGENCYCONTACT, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <param name="objEmployee">The obj get certification details.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetEmergencyContact(BusinessEntities.EmergencyContact objGetEmergencyContact)
        {
            //Object declaration of QualificationDetails class
            Rave.HR.DataAccessLayer.Employees.EmergencyContact objEmergencyContactDAL;

            try
            {
                //Created new instance of QualificationDetails class to call objGetQualificationDetailsDAL() of Data access layer
                objEmergencyContactDAL = new Rave.HR.DataAccessLayer.Employees.EmergencyContact();

                //Call to GetQualificationDetails() of Data access layer and return the Qualifications
                return objEmergencyContactDAL.GetEmergencyContact(objGetEmergencyContact);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, GETEMERGENCYCONTACT, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Updates the qualification details.
        /// </summary>
        /// <param name="objUpdateEmergencyContact">The object update qualification details.</param>
        public void UpdateEmergencyContact(BusinessEntities.EmergencyContact objUpdateEmergencyContact)
        {
            //Object declaration of EmergencyContact class
            Rave.HR.DataAccessLayer.Employees.EmergencyContact objUpdateEmergencyContactDAL;

            try
            {
                //Created new instance of EmergencyContact class to call UpdateEmergencyContact() of Data access layer
                objUpdateEmergencyContactDAL = new Rave.HR.DataAccessLayer.Employees.EmergencyContact();

                //Call to UpdateEmergencyContact() of Data access layer
                objUpdateEmergencyContactDAL.UpdateEmergencyContact(objUpdateEmergencyContact);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, UPDATEEMERGENCYCONTACT, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Deletes the qualification details.
        /// </summary>
        /// <param name="objDeleteEmergencyContact">The object delete qualification details.</param>
        public void DeleteEmergencyContact(BusinessEntities.EmergencyContact objDeleteEmergencyContact)
        {
            //Object declaration of EmergencyContact class
            Rave.HR.DataAccessLayer.Employees.EmergencyContact objDeleteEmergencyContactDAL;

            try
            {
                //Created new instance of EmergencyContact class to call DeleteEmergencyContact() of Data access layer
                objDeleteEmergencyContactDAL = new Rave.HR.DataAccessLayer.Employees.EmergencyContact();

                //Call to DeleteEmergencyContact() of Data access layer
                objDeleteEmergencyContactDAL.DeleteEmergencyContact(objDeleteEmergencyContact);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, DELETEEMERGENCYCONTACT, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Manipulates the specified obj qualification details list.
        /// </summary>
        /// <param name="objEmergencyContactList">The obj qualification details list.</param>
        public void Manipulation(BusinessEntities.RaveHRCollection objEmergencyContactCollection)
        {
            try
            {
                //Check for each each entry in EmergencyContact List
                foreach (BusinessEntities.EmergencyContact objAddEmergencyContact in objEmergencyContactCollection)
                {
                    //If mode of the entry is Add i.e. 1 then call Add()
                    if (objAddEmergencyContact.Mode == 1)
                    {
                        this.AddEmergencyContact(objAddEmergencyContact);
                    }

                    //If mode of the entry is Update i.e. 2 then call Update()
                    if (objAddEmergencyContact.Mode == 2)
                    {
                        this.UpdateEmergencyContact(objAddEmergencyContact);
                    }

                    //If QualificationId is greater than zero i.e. qualification is already added and mode of the entry is Delete i.e. 3 then call Delete()
                    if (objAddEmergencyContact.EmergencyContactId > 0 && objAddEmergencyContact.Mode == 3)
                    {
                        this.DeleteEmergencyContact(objAddEmergencyContact);
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

        #endregion 
    }
}
