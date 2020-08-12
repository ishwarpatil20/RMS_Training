//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           ProjectDetails.cs       
//  Author:         Shrinivas.Dalavi
//  Date written:   27/08/2009 2:44:40 AM
//  Description:    This class provides the business layer methods for Employee module.
//
//  Amendments
//  Date                    Who                 Ref     Description
//  ----                    -----------         ---     -----------
//  27/08/2009 2:44:40 AM  Shrinivas.Dalavi     n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using Common;

namespace Rave.HR.BusinessLayer.Employee
{
    public class ProjectDetails
    {
        #region Private Member Variables

        /// <summary>
        /// private variable for Class Name used in each catch block
        /// </summary>
        private string CLASS_NAME = "ProjectDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string MANIPULATION = "Manipulation";

        #endregion Private Member Variables

        #region Public Member Functions

        /// <summary>
        /// Adds the project details.
        /// </summary>
        /// <param name="objAddProjectDetails">The obj add project details.</param>
        public void AddProjectDetails(BusinessEntities.ProjectDetails objAddProjectDetails)
        {
            Rave.HR.DataAccessLayer.Employees.ProjectDetails objAddProjectDetailsDAL;

            try
            {
                objAddProjectDetailsDAL = new Rave.HR.DataAccessLayer.Employees.ProjectDetails();

                objAddProjectDetailsDAL.AddProjectDetails(objAddProjectDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "AddProjectDetails", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

        }

        /// <summary>
        /// Updates the project details.
        /// </summary>
        /// <param name="objUpdateProjectDetails">The obj update project details.</param>
        public void UpdateProjectDetails(BusinessEntities.ProjectDetails objUpdateProjectDetails)
        {
            Rave.HR.DataAccessLayer.Employees.ProjectDetails objUpdateProjectDetailsDAL;

            try
            {
                objUpdateProjectDetailsDAL = new Rave.HR.DataAccessLayer.Employees.ProjectDetails();

                objUpdateProjectDetailsDAL.UpdateProjectDetails(objUpdateProjectDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "UpdateProjectDetails", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Gets the project details.
        /// </summary>
        /// <param name="objGetProjectDetails">The obj get project details.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetProjectDetails(BusinessEntities.ProjectDetails objGetProjectDetails)
        {
            //Object declaration of QualificationDetails class
            Rave.HR.DataAccessLayer.Employees.ProjectDetails objGetProjectDetailsDAL;

            try
            {
                //Created new instance of QualificationDetails class to call objGetQualificationDetailsDAL() of Data access layer
                objGetProjectDetailsDAL = new Rave.HR.DataAccessLayer.Employees.ProjectDetails();

                //Call to GetQualificationDetails() of Data access layer and return the Qualifications
                return objGetProjectDetailsDAL.GetProjectDetails(objGetProjectDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetProjectDetails", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
            
        }

        /// <summary>
        /// Deletes the project details.
        /// </summary>
        /// <param name="objDeleteProjectDetails">The obj delete project details.</param>
        public void DeleteProjectDetails(BusinessEntities.ProjectDetails objDeleteProjectDetails)
        {
            Rave.HR.DataAccessLayer.Employees.ProjectDetails objDeleteProjectDetailsDAL;

            try
            {
                objDeleteProjectDetailsDAL = new Rave.HR.DataAccessLayer.Employees.ProjectDetails();

                objDeleteProjectDetailsDAL.DeleteProjectDetails(objDeleteProjectDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "DeleteProjectDetails", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Manipulations the specified obj project details list.
        /// </summary>
        /// <param name="objProjectDetailsList">The obj project details list.</param>
        public void Manipulation(BusinessEntities.RaveHRCollection objProjectDetailsColletion)
        {
            try
            {
                foreach (BusinessEntities.ProjectDetails objAddProjectDetails in objProjectDetailsColletion)
                {
                    if (objAddProjectDetails.Mode == 1)
                    {
                        this.AddProjectDetails(objAddProjectDetails);

                    }
                    if (objAddProjectDetails.Mode == 2)
                    {
                        this.UpdateProjectDetails(objAddProjectDetails);

                    }
                    if (objAddProjectDetails.Mode == 3)
                    {
                        this.DeleteProjectDetails(objAddProjectDetails);

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
