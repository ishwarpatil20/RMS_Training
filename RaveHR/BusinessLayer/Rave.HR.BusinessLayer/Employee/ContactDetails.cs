using System;
using Common;

namespace Rave.HR.BusinessLayer.Employee
{
    public class ContactDetails
    {

        #region Private Member Variables

        /// <summary>
        /// private string variable for Class Name used in each catch block
        /// </summary>
        private string CLASS_NAME = "ContactDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string ADDCONTACTDETAILS = "AddContactDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string UPDATECONTACTDETAILS = "UpdateContactDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string GETCONTACTDETAILS = "GetContactDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string DELETECONTACTDETAILS = "DeleteContactDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string MANIPULATION = "Manipulation";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string GETSEATDETAILS = "GetSeatDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string ALLOCATESEAT = "AllocateSeat";

        #endregion Private Member Variables

        #region Public Member Functions

        /// <summary>
        /// Adds the qualification details.
        /// </summary>
        /// <param name="objAddContactDetails">The object add qualification details.</param>
        public void AddContactDetails(BusinessEntities.ContactDetails objAddContactDetails)
        {
            //Object declaration of ContactDetails class
            Rave.HR.DataAccessLayer.Employees.ContactDetails objAddContactDetailsDAL;

            try
            {
                //Created new instance of ContactDetails class to call AddContactDetails() of Data access layer
                objAddContactDetailsDAL = new Rave.HR.DataAccessLayer.Employees.ContactDetails();

                //Call to AddContactDetails() of Data access layer
                objAddContactDetailsDAL.AddContactDetails(objAddContactDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, ADDCONTACTDETAILS, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Updates the qualification details.
        /// </summary>
        /// <param name="objUpdateContactDetails">The object update qualification details.</param>
        public void UpdateContactDetails(BusinessEntities.ContactDetails objUpdateContactDetails)
        {
            //Object declaration of ContactDetails class
            Rave.HR.DataAccessLayer.Employees.ContactDetails objUpdateContactDetailsDAL;

            try
            {
                //Created new instance of ContactDetails class to call UpdateContactDetails() of Data access layer
                objUpdateContactDetailsDAL = new Rave.HR.DataAccessLayer.Employees.ContactDetails();

                //Call to UpdateContactDetails() of Data access layer
                objUpdateContactDetailsDAL.UpdateContactDetails(objUpdateContactDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, UPDATECONTACTDETAILS, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Gets the qualification details.
        /// </summary>
        /// <param name="objGetContactDetails">The object get qualification details.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetContactDetails(BusinessEntities.ContactDetails objGetContactDetails)
        {
            //Object declaration of ContactDetails class
            Rave.HR.DataAccessLayer.Employees.ContactDetails objGetContactDetailsDAL;

            try
            {
                //Created new instance of ContactDetails class to call objGetContactDetailsDAL() of Data access layer
                objGetContactDetailsDAL = new Rave.HR.DataAccessLayer.Employees.ContactDetails();

                //Call to GetContactDetails() of Data access layer and return the Qualifications
                return objGetContactDetailsDAL.GetContactDetails(objGetContactDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, GETCONTACTDETAILS, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Deletes the qualification details.
        /// </summary>
        /// <param name="objDeleteContactDetails">The object delete qualification details.</param>
        public void DeleteContactDetails(BusinessEntities.ContactDetails objDeleteContactDetails)
        {
            //Object declaration of ContactDetails class
            Rave.HR.DataAccessLayer.Employees.ContactDetails objDeleteContactDetailsDAL;

            try
            {
                //Created new instance of ContactDetails class to call DeleteContactDetails() of Data access layer
                objDeleteContactDetailsDAL = new Rave.HR.DataAccessLayer.Employees.ContactDetails();

                //Call to DeleteContactDetails() of Data access layer
                objDeleteContactDetailsDAL.DeleteContactDetails(objDeleteContactDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, DELETECONTACTDETAILS, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Manipulates the specified obj qualification details list.
        /// </summary>
        /// <param name="objContactDetailsList">The obj qualification details list.</param>
        public void Manipulation(BusinessEntities.RaveHRCollection objContactDetailsCollection)
        {
            try
            {
                //Check for each each entry in ContactDetails List
                foreach (BusinessEntities.ContactDetails objAddContactDetails in objContactDetailsCollection)
                {
                    //If mode of the entry is Add i.e. 1 then call Add()
                    if (objAddContactDetails.Mode == 1)
                    {
                        this.AddContactDetails(objAddContactDetails);
                    }

                    //If mode of the entry is Update i.e. 2 then call Update()
                    if (objAddContactDetails.Mode == 2)
                    {
                        this.UpdateContactDetails(objAddContactDetails);
                    }

                    //If QualificationId is greater than zero i.e. qualification is already added and mode of the entry is Delete i.e. 3 then call Delete()
                    if (objAddContactDetails.EmployeeContactId > 0 && objAddContactDetails.Mode == 3)
                    {
                       this.DeleteContactDetails(objAddContactDetails);
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
        /// Gets the seat details.
        /// </summary>
        /// <param name="objGetContactDetails">The obj get contact details.</param>
        /// <returns></returns>
        public string GetSeatDetails(BusinessEntities.ContactDetails objGetContactDetails)
        {
            //Object declaration of ContactDetails class
            Rave.HR.DataAccessLayer.Employees.ContactDetails objGetContactDetailsDAL;
          

            try
            {
                //Created new instance of ContactDetails class to call objGetContactDetailsDAL() of Data access layer
                objGetContactDetailsDAL = new Rave.HR.DataAccessLayer.Employees.ContactDetails();

                //Call to GetContactDetails() of Data access layer and return the Qualifications
                return objGetContactDetailsDAL.GetSeatDetails(objGetContactDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, GETSEATDETAILS, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Allocates the seat.
        /// </summary>
        /// <param name="objGetContactDetails">The obj get contact details.</param>
        /// <returns></returns>
        public int AllocateSeat(BusinessEntities.ContactDetails objGetContactDetails)
        {
            //Object declaration of ContactDetails class
            Rave.HR.DataAccessLayer.Employees.ContactDetails objGetContactDetailsDAL;

            try
            {
                //Created new instance of ContactDetails class to call objGetContactDetailsDAL() of Data access layer
                objGetContactDetailsDAL = new Rave.HR.DataAccessLayer.Employees.ContactDetails();

                //Call to GetContactDetails() of Data access layer and return the Qualifications
                return objGetContactDetailsDAL.AllocateSeat(objGetContactDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, ALLOCATESEAT, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }


        #endregion Public Member Functions
    } 
}
