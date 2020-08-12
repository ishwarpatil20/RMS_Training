//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           ContractProject.cs       
//  Class:          ContractProject
//  Author:         prashant.mala
//  Date written:   8/05/2009 5:48:30 PM
//  Description:    This class contains methods related to Contract module. 
//                  These methods used for ProjectSummary.aspx etc.
//

//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  8/05/2009 5:48:30 PM  prashant.mala    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using Common;
using Common.Constants;
using Rave.HR.DataAccessLayer;

namespace Rave.HR.BusinessLayer.Contracts
{
    public class ContractProject
    {
        #region Private fields

        // Initialise the Collection class object
        BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();

        DataAccessLayer.Contracts.ContractProject objContractProj = null;

        BusinessEntities.ContractProject ObjProject = null;

        DataAccessClass objProjectDetails = null;

        const string CONTRACTPROJECT = "ContractPRoject.cs";
        //Declaring function name.
        const string GETCONTRACTFORPROJECT = "GetContractsForProject";
        const string SAVE = "Save";
        const string GETPROJECTSFORCONTRACT = "GetProjectsForContracts";
        const string GETPROJECTSDETAILS = "GetProjectsDetails";
        const string GETPROJECTBYID = "GetProjectDetailsByProjectID";
        const string ISEMPLOYEEASSOCIATED = "IsEmployeeAssociated";
        const string EDIT = "Edit";
        const string DISSOCIATEPROJECT = "DisassociateProject";
        const string GETPROJECTSLISTDETAILS = "GetProjectsListDetails";
        const string CHECKPROJECTNAME = "checkProjectName";


        #endregion Private fields
        public List<BusinessEntities.ContractProject> GetContractsForProject(BusinessEntities.ContractProject objBEContractsForProject)
        {
            try
            {
                objContractProj = new Rave.HR.DataAccessLayer.Contracts.ContractProject();
                return objContractProj.GetContractsForProject(objBEContractsForProject);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACTPROJECT, GETCONTRACTFORPROJECT, EventIDConstants.RAVE_HR_CONTRACT_BUSNIESS_LAYER);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="projectAdd"></param>
        public bool Save(BusinessEntities.ContractProject projectAdd, ref string ProjectCodeAbbreviation)
        {
            try
            {
                Rave.HR.DataAccessLayer.Contracts.ContractProject saveContractProjectBL = new Rave.HR.DataAccessLayer.Contracts.ContractProject();
                bool success = false;

                success = saveContractProjectBL.Save(projectAdd, ref ProjectCodeAbbreviation);
                return success;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACTPROJECT, SAVE, EventIDConstants.RAVE_HR_CONTRACT_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// This method will fetch record of project details under particular contract
        /// specified in criteria object from data base
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public List<BusinessEntities.ContractProject> GetProjectsForContracts(ContractCriteria criteria)
        {
            try
            {
                //instantiate contract project objectof data layer
                objContractProj = new Rave.HR.DataAccessLayer.Contracts.ContractProject();
                return objContractProj.GetProjectForContracts(criteria);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACTPROJECT, GETPROJECTSFORCONTRACT, EventIDConstants.RAVE_HR_CONTRACT_BUSNIESS_LAYER);
            }

        }

        #region GetProjectsListDetails

        /// <summary>
        /// This method will fetch record of all project details for project list deatails page.
        /// </summary>       
        /// <returns>list</returns>
        public List<BusinessEntities.ContractProject> GetProjectsDetails()
        {
            try
            {
                //instantiate contract project object of data layer
                objContractProj = new Rave.HR.DataAccessLayer.Contracts.ContractProject();
                List<BusinessEntities.ContractProject> objProjectDetails = new List<BusinessEntities.ContractProject>();
                objProjectDetails = objContractProj.GetProjectdetails();
                return objProjectDetails;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACTPROJECT, GETPROJECTSDETAILS, EventIDConstants.RAVE_HR_CONTRACT_BUSNIESS_LAYER);
            }

        }

        #endregion GetProjectsListDetails

        #region GetProjectDetailsByProjectID

        /// <summary>
        /// get the project details by project ID for a contract.
        /// </summary>
        /// <param name="contractProject"></param>
        /// <returns></returns>
        public BusinessEntities.ContractProject GetProjectDetailsByProjectID(BusinessEntities.ContractProject contractProject)
        {
            try
            {
                objContractProj = new Rave.HR.DataAccessLayer.Contracts.ContractProject();

                ObjProject = new BusinessEntities.ContractProject();

                ObjProject = objContractProj.getProjectDetailsByID(contractProject);

                return ObjProject;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACTPROJECT, GETPROJECTBYID, EventIDConstants.RAVE_HR_CONTRACT_BUSNIESS_LAYER);
            }
        }

        #endregion GetProjectDetailsByProjectID

        #region ProjectDetail

        /// <summary>
        /// Returns the project data.
        /// </summary>
        /// <param name="contractProject"></param>
        /// <returns></returns>
        public BusinessEntities.ContractProject ProjectDetail(BusinessEntities.ContractProject contractProject)
        {
            try
            {
                BusinessEntities.ContractProject projectDetail = new BusinessEntities.ContractProject();
                objContractProj = new Rave.HR.DataAccessLayer.Contracts.ContractProject();

                projectDetail = objContractProj.ProjectDetail(contractProject);

                return projectDetail;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACTPROJECT, GETPROJECTBYID, EventIDConstants.RAVE_HR_CONTRACT_BUSNIESS_LAYER);
            }
        }

        #endregion ProjectDetail

        #region IsEmployeeAssociated

        /// <summary>
        /// Checks wether any employee is associated with a project or not. 
        /// </summary>
        /// <param name="contractProject"></param>
        /// <returns></returns>
        public bool IsEmployeeAssociated(BusinessEntities.ContractProject contractProject)
        {
            try
            {
                bool Check = false;

                objContractProj = new Rave.HR.DataAccessLayer.Contracts.ContractProject();

                Check = objContractProj.isEmpAssociated(contractProject);

                return Check;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACTPROJECT, ISEMPLOYEEASSOCIATED, EventIDConstants.RAVE_HR_CONTRACT_BUSNIESS_LAYER);
            }
        }

        #endregion IsEmployeeAssociated

        #region Edit

        /// <summary>
        /// This methode will update the Project details.
        /// </summary>
        /// <param name="projectAdd"></param>

        public bool Edit(BusinessEntities.ContractProject projectAdd, string PrjLocation)
        {
            bool sucess = false;

            try
            {
                // Initialise a DL layer object.
                Rave.HR.DataAccessLayer.Contracts.ContractProject EditContractProjectDL = new Rave.HR.DataAccessLayer.Contracts.ContractProject();

                // Calls a DL layer function.
                int projectEdited = EditContractProjectDL.Edit(projectAdd, PrjLocation);
                sucess = true;
                return sucess;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACTPROJECT, EDIT, EventIDConstants.RAVE_HR_CONTRACT_BUSNIESS_LAYER);
            }
        }

        #endregion Edit

        #region DisassociateProject

        /// <summary>
        /// This function calls the dl layer function 
        /// to disassociate a project related to a contract.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="contract"></param>
        public void DisassociateProject(BusinessEntities.ContractProject project, BusinessEntities.Contract contract)
        {
            try
            {
                //declares a dl layer object
                Rave.HR.DataAccessLayer.Contracts.ContractProject ContractProjectDL = new Rave.HR.DataAccessLayer.Contracts.ContractProject();

                //calls the dl layer function.
                ContractProjectDL.DisassociateProject(project, contract);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACTPROJECT, DISSOCIATEPROJECT, EventIDConstants.RAVE_HR_CONTRACT_BUSNIESS_LAYER);
            }
        }

        #endregion DisassociateProject

        #region GetProjectsListDetails

        /// <summary>
        /// This method will fetch record of all project details for project list deatails page.
        /// </summary>       
        /// <returns>list</returns>
        public BusinessEntities.RaveHRCollection GetProjectsListDetails(BusinessEntities.ContractCriteria criteria)
        {
            try
            {
                //instantiate contract project object of data layer
                objContractProj = new Rave.HR.DataAccessLayer.Contracts.ContractProject();

                raveHRCollection = objContractProj.GetProjectsListDetails(criteria);

                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACTPROJECT, GETPROJECTSLISTDETAILS, EventIDConstants.RAVE_HR_CONTRACT_BUSNIESS_LAYER);
            }

        }

        public List<BusinessEntities.ContractProject> GetProjectsDetailsForFilter(BusinessEntities.ContractProject objGridDetail, BusinessEntities.ContractCriteria objContractCriteria)
        {
            objContractProj = new Rave.HR.DataAccessLayer.Contracts.ContractProject();
            
            return objContractProj.GetProjectDetailsForFilter(objGridDetail, objContractCriteria);
        }

        #endregion GetProjectsListDetails

        #region checkProjectName

        /// <summary>
        /// Checks wether the project name exists or not.
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public bool checkProjectName(string projectName)
        {
            try
            {
                bool Check = false;

                objContractProj = new Rave.HR.DataAccessLayer.Contracts.ContractProject();

                Check = objContractProj.checkProjectName(projectName);

                return Check;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACTPROJECT, CHECKPROJECTNAME, EventIDConstants.RAVE_HR_CONTRACT_BUSNIESS_LAYER);
            }
        }

        #endregion checkProjectName

        /// <summary>
        /// get the client name of project by project  code.
        /// </summary>
        /// <param name="projectCode"></param>
        /// <returns></returns>
        public BusinessEntities.ProjectDetails getClientNameByProjectCode(string projectCode)
        {
            objContractProj = new Rave.HR.DataAccessLayer.Contracts.ContractProject();

            return objContractProj.getClientNameByProjectCode(projectCode);
        }


        /// <summary>
        /// Checks the project code.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        /// <returns></returns>
        public bool checkProjectCode(string projectName)
        {
            try
            {
                bool Check = false;

                objContractProj = new Rave.HR.DataAccessLayer.Contracts.ContractProject();

                Check = objContractProj.checkProjectCode(projectName);

                return Check;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACTPROJECT, CHECKPROJECTNAME, EventIDConstants.RAVE_HR_CONTRACT_BUSNIESS_LAYER);
            }
        }
    }
}
