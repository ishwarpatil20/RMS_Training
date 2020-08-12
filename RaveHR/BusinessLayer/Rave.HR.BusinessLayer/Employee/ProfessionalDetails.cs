using System;
using Common;

namespace Rave.HR.BusinessLayer.Employee
{
    public class ProfessionalDetails
    {
        #region Private Members

        private string CLASS_NAME = "ProfessionalDetails.cs";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string MANIPULATION = "Manipulation";

        Rave.HR.DataAccessLayer.Employees.ProfessionalDetails objProfessionalDetailsDAL = new Rave.HR.DataAccessLayer.Employees.ProfessionalDetails();

        #endregion

        #region Public Members

        /// <summary>
        /// Adds the professional details.
        /// </summary>
        /// <param name="objAddProfessionalDetails">The obj add professional details.</param>
        public void AddProfessionalDetails(BusinessEntities.ProfessionalDetails objAddProfessionalDetails)
        {
            try
            {
                objProfessionalDetailsDAL.AddProfessionalDetails(objAddProfessionalDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "AddProfessionalDetails", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

        }

        /// <summary>
        /// Updates the professional details.
        /// </summary>
        /// <param name="objUpdateProfessinalDetails">The obj update professinal details.</param>
        public void UpdateProfessionalDetails(BusinessEntities.ProfessionalDetails objUpdateProfessinalDetails)
        {
            try
            {
                objProfessionalDetailsDAL.UpdateProfessionalDetails(objUpdateProfessinalDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "UpdateProfessinalDetails", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

        }

        /// <summary>
        /// Gets the professional details.
        /// </summary>
        /// <param name="objGetProfessionalDetails">The obj get professional details.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetProfessionalDetails(BusinessEntities.ProfessionalDetails objGetProfessionalDetails)
        {
            //Object declaration of QualificationDetails class
            Rave.HR.DataAccessLayer.Employees.ProfessionalDetails objGetProfessionalDetailsDAL;

            try
            {
                //Created new instance of QualificationDetails class to call objGetQualificationDetailsDAL() of Data access layer
                objGetProfessionalDetailsDAL = new Rave.HR.DataAccessLayer.Employees.ProfessionalDetails();

                //Call to GetQualificationDetails() of Data access layer and return the Qualifications
                return objGetProfessionalDetailsDAL.GetProfessionalDetails(objGetProfessionalDetails);

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetProfessionalDetails", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Deletes the professional details.
        /// </summary>
        /// <param name="objDeleteProfessionalDetails">The obj delete professional details.</param>
        public void DeleteProfessionalDetails(BusinessEntities.ProfessionalDetails objDeleteProfessionalDetails)
        {
            try
            {
                objProfessionalDetailsDAL.DeleteProfessionalDetails(objDeleteProfessionalDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "DeleteProfessionalDetails", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

        }

        public void Manipulation(BusinessEntities.RaveHRCollection ProfessionalDetailsCollection)
        {
            try
            {
                foreach (BusinessEntities.ProfessionalDetails ProfessionalDetails in ProfessionalDetailsCollection)
                {
                    if (ProfessionalDetails.Mode == 1)
                    {
                        this.AddProfessionalDetails(ProfessionalDetails);
                    }
                    else if (ProfessionalDetails.Mode == 2)
                    {
                        this.UpdateProfessionalDetails(ProfessionalDetails);
                    }
                    else if (ProfessionalDetails.Mode == 3)
                    {
                        this.DeleteProfessionalDetails(ProfessionalDetails);
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
