//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           EmployeeResume.cs       
//  Author:         Rahul.Parwekar
//  Date written:   19/10/2010 
//  Description:    This class provides the business layer methods for Employee Resume module.
//
//  Amendments
//  Date                    Who                 Ref     Description
//  ----                    -----------         ---     -----------
//  19/10/2010          Rahul.Parwekar          n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using Common;

namespace Rave.HR.BusinessLayer.Employee
{
   public class EmployeeResume
    {
        #region Private Member Variables
        /// <summary>
        /// private variable for Class Name used in each catch block
        /// </summary>
        private string CLASS_NAME = "EmployeeResume";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string EMPLOYEERESUME = "AddEmployeeResume";
        #endregion

        #region Public Member Functions

        /// <summary>
        /// Adds the Employee Resume details.
        /// </summary>
        /// <param name="objAddProjectDetails">The obj add project details.</param>
        public BusinessEntities.RaveHRCollection AddEmployeeResumeDetails(BusinessEntities.EmployeeResume objAddEmployeeResume)
        {
            Rave.HR.DataAccessLayer.Employees.EmployeeResume objAddProjectDetailsDAL;

            try
            {
                int i = 0; 
                //Created new instance of CertificationDetails class to call AddCertificationDetails() of Data access layer
                objAddProjectDetailsDAL = new Rave.HR.DataAccessLayer.Employees.EmployeeResume();

                //Call to AddCertificationDetails() of Data access layer
                 return objAddProjectDetailsDAL.AddEmpResumeDetails(objAddEmployeeResume);
                 
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, EMPLOYEERESUME, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

        }

        /// <summary>
        /// To Get the Employee Resume Count
        /// </summary>
        /// <param name="objAddProjectDetails">The obj add project details.</param>
        public BusinessEntities.RaveHRCollection GetEmployeeResumeCountDetails(BusinessEntities.EmployeeResume objGetEmployeeResumeCount)
        {
            Rave.HR.DataAccessLayer.Employees.EmployeeResume objEmployeeResumeCountDAL;

            try
            {
                //Created new instance of CertificationDetails class to call AddCertificationDetails() of Data access layer
                objEmployeeResumeCountDAL = new Rave.HR.DataAccessLayer.Employees.EmployeeResume();

                //Call to AddCertificationDetails() of Data access layer
               return  objEmployeeResumeCountDAL.GetEmpResumeCountDetails(objGetEmployeeResumeCount);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, EMPLOYEERESUME, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

        }

        /// <summary>
        /// Gets the Employee Resume details.
        /// </summary>
        /// <param name="objGetCertificationDetails">The obj get certification details.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetEmployeeResumeDetails(BusinessEntities.EmployeeResume objGetEmployeeResumeDetails)
        {
            //Object declaration of QualificationDetails class
            Rave.HR.DataAccessLayer.Employees.EmployeeResume objGetEmployeeResumeDetailsDAL;

            try
            {
                //Created new instance of QualificationDetails class to call objGetQualificationDetailsDAL() of Data access layer
                objGetEmployeeResumeDetailsDAL = new Rave.HR.DataAccessLayer.Employees.EmployeeResume();

                //Call to GetQualificationDetails() of Data access layer and return the Qualifications
                return objGetEmployeeResumeDetailsDAL.GetEmployeeResumeDetails(objGetEmployeeResumeDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, EMPLOYEERESUME, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        //19645-Ambar-Start
        /// <summary>
        /// Delete the Employee Resume details.
        /// </summary>
        public void DeleteEmployeeResumeDetails(string str_deletedfile, int EMPId)
        {
          Rave.HR.DataAccessLayer.Employees.EmployeeResume objDeleteEmployeeResumeDetailsDAL;

          try
          {
            objDeleteEmployeeResumeDetailsDAL = new Rave.HR.DataAccessLayer.Employees.EmployeeResume();

            objDeleteEmployeeResumeDetailsDAL.DeleteEmpResumeDetails(str_deletedfile, EMPId);
          }
          catch (RaveHRException ex)
          {
            throw ex;
          }
          catch (Exception ex)
          {
            throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, EMPLOYEERESUME, EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
          }

        }
        //19645-Ambar-End

        #endregion
    }
}
