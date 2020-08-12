using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using System.Data;
using Rave.HR;
using Rave.HR.BusinessLayer.Interface;
using Common.AuthorizationManager;
using System.Text;
using System.Collections;
using Common.Constants;


namespace Rave.HR.BusinessLayer.Contracts
{
    public class Contract
    {
        #region Private Variables
        //Declaring contract object.
        DataAccessLayer.Contracts.Contract objContractProj = null;

        //Declaring function names.
        const string CONTRACT = "Contract.cs";
        const string SAVE = "Save";
        const string EDIT = "Edit";
        const string GETCONTRACTS = "GetContracts";
        const string GETCONTRACTDETAILS = "GetContractDetails";
        const string GETCONTRACTPROJECTDETAILS = "GetContractProjectDetails";
        const string GETEMAILID = "GetEmailID";
        const string DELETE = "Delete";
        #endregion

        #region Public Method

        /// <summary>
        /// This method is used for saving the contract details
        /// </summary>
        /// <param name="contractAdd"></param>
        /// <returns></returns>
        public int Save(BusinessEntities.Contract contractAdd, DataTable projectDetails)
        {
            int contractId = 0;

            try
            {
                Rave.HR.DataAccessLayer.Contracts.Contract saveContractBL = new Rave.HR.DataAccessLayer.Contracts.Contract();

                contractId = saveContractBL.Save(contractAdd);
                contractAdd.ContractID = contractId;

                if (contractId != 0)
                {
                    SaveProjects(contractAdd, projectDetails);
                }
                return contractId;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACT, SAVE, EventIDConstants.RAVE_HR_CONTRACT_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// This method is used for editing the contract details
        /// </summary>
        /// <param name="contractAdd"></param>
        /// <returns></returns>
        public bool Edit(BusinessEntities.Contract contractAdd, DataTable contractProject, RaveHRCollection CRDetailsCollection)
        {
            bool result = false;
            try
            {
                //Defines a bl layer object.
                Rave.HR.DataAccessLayer.Contracts.Contract EditContractDL = new Rave.HR.DataAccessLayer.Contracts.Contract();

                // Calls the data layer function for editing the contract details.
                result = EditContractDL.edit(contractAdd);

                if (result)
                {
                    result = EditProjects(contractAdd, contractProject,CRDetailsCollection);
                }
                return result;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACT, EDIT, EventIDConstants.RAVE_HR_CONTRACT_BUSNIESS_LAYER);
            }

        }
        /// <summary>
        /// This method will fetched records for contract summary from data layer and return to presentation layer
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public List<BusinessEntities.Contract> GetContracts(ContractCriteria criteria)
        {
            try
            {
                //instantiate the onject of data access layer of contract class
                objContractProj = new Rave.HR.DataAccessLayer.Contracts.Contract();
                return objContractProj.GetContracts(criteria);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACT, GETCONTRACTS, EventIDConstants.RAVE_HR_CONTRACT_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// This Function is used to View Contract Details.
        /// </summary>
        /// <param name="objViewProject"></param>
        /// <returns></returns>        
        public BusinessEntities.Contract GetContractDetails(BusinessEntities.Contract objViewContract, string SortDir, string SortExpression)
        {
            try
            {
                Rave.HR.DataAccessLayer.Contracts.Contract objViewContracDAL = new Rave.HR.DataAccessLayer.Contracts.Contract();

                BusinessEntities.Contract objContract = null;

                objContract = objViewContracDAL.ViewContractDetails(objViewContract);

                return objContract;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACT, GETCONTRACTDETAILS, EventIDConstants.RAVE_HR_CONTRACT_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// This Function is used to View Contract Details.
        /// </summary>
        /// <param name="objViewProject"></param>
        /// <returns></returns>        
        public List<BusinessEntities.ContractProject> GetContractProjectDetails(int ContractID, string SortDir, string SortExpression)
        {
            try
            {
                Rave.HR.DataAccessLayer.Contracts.Contract objViewContracDAL = new Rave.HR.DataAccessLayer.Contracts.Contract();

                List<BusinessEntities.ContractProject> objContractProject = null;

                objContractProject = objViewContracDAL.ViewContractProjectDetails(ContractID);

                return objContractProject;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACT, GETCONTRACTPROJECTDETAILS, EventIDConstants.RAVE_HR_CONTRACT_BUSNIESS_LAYER);
            }
        }


        /// <summary>
        /// This Function is used get the email id.
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns> 
        public string GetEmailID(int empId)
        {
            try
            {
                objContractProj = new Rave.HR.DataAccessLayer.Contracts.Contract();
                return objContractProj.GetEmailID(empId);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACT, GETEMAILID, EventIDConstants.RAVE_HR_CONTRACT_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// This function deletes the contract details.
        /// </summary>
        /// <param name="Contract"></param>
        public bool Delete(BusinessEntities.Contract Contract)
        {
            bool result = false;
            try
            {
                //declares a dL layer object to call the Delete function.
                objContractProj = new Rave.HR.DataAccessLayer.Contracts.Contract();

                //Calls the DL function to delete the Contract deatils.
                result = objContractProj.delete(Contract);

                if (result)
                {
                    SendMailForDeleteContract(Contract);
                }
                return result;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACT, DELETE, EventIDConstants.RAVE_HR_CONTRACT_BUSNIESS_LAYER);
            }
        }

        //GetEmailID
        public string GetEmailIdByEmpId(int EmpId)
        {
            string result = string.Empty;
            try
            {
                //declares a dL layer object to call the Delete function.
                objContractProj = new Rave.HR.DataAccessLayer.Contracts.Contract();

                //Calls the DL function to delete the Contract deatils.
                result = objContractProj.GetEmailID(EmpId);
                return result;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACT, DELETE, EventIDConstants.RAVE_HR_CONTRACT_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Adds the CR details.
        /// </summary>
        /// <param name="objAddQualificationDetails">The obj add qualification details.</param>
        public void AddCRDetails(BusinessEntities.Contract objAddCRDetails)
        {
            //Object declaration of Contract class
            Rave.HR.DataAccessLayer.Contracts.Contract objAddCRDetailsDAL;

            try
            {
                //Created new instance of Contract class to call AddCRDetails() of Data access layer
                objAddCRDetailsDAL = new Rave.HR.DataAccessLayer.Contracts.Contract();

                //Call to AddCRDetails() of Data access layer
                objAddCRDetailsDAL.AddCRDetails(objAddCRDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACT, "AddCRDetails", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Updates the CR details.
        /// </summary>
        /// <param name="objUpdateQualificationDetails">The obj update qualification details.</param>
        public void UpdateCRDetails(BusinessEntities.Contract objUpdateCRDetails)
        {
            //Object declaration of Contract class
            Rave.HR.DataAccessLayer.Contracts.Contract objUpdateCRDetailsDAL;

            try
            {
                //Created new instance of Contract class to call UpdateCRDetails() of Data access layer
                objUpdateCRDetailsDAL = new Rave.HR.DataAccessLayer.Contracts.Contract();

                //Call to UpdateQualificationDetails() of Data access layer
                objUpdateCRDetailsDAL.UpdateCRDetails(objUpdateCRDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACT, "UpdateCRDetails", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Gets the CR details.
        /// </summary>
        /// <param name="objGetQualificationDetails">The obj get qualification details.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetCRDetails(BusinessEntities.Contract objGetCRDetails)
        {
            //Object declaration of CRDetails class
            Rave.HR.DataAccessLayer.Contracts.Contract objGetCRDetailsDAL;

            try
            {
                //Created new instance of CRDetails class to call objGetQualificationDetailsDAL() of Data access layer
                objGetCRDetailsDAL = new Rave.HR.DataAccessLayer.Contracts.Contract();

                //Call to GetCRDetails() of Data access layer and return the CRdetails
                return objGetCRDetailsDAL.GetCRDetails(objGetCRDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACT, "GetCRDetails", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Deletes the CR details.
        /// </summary>
        /// <param name="objDeleteQualificationDetails">The obj delete qualification details.</param>
        public void DeleteCRDetails(BusinessEntities.Contract objDeleteCRDetails)
        {
            //Object declaration of QualificationDetails class
            Rave.HR.DataAccessLayer.Contracts.Contract objDeleteCRDetailsDAL;

            try
            {
                //Created new instance of CRDetails class to call DeleteCRDetails() of Data access layer
                objDeleteCRDetailsDAL = new Rave.HR.DataAccessLayer.Contracts.Contract();

                //Call to DeleteQualificationDetails() of Data access layer
                objDeleteCRDetailsDAL.DeleteCRDetails(objDeleteCRDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACT, "DeleteCRDetails", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Manipulates the specified obj qualification details list.
        /// </summary>
        /// <param name="objQualificationDetailsList">The obj qualification details list.</param>
        public void Manipulation(BusinessEntities.RaveHRCollection objCRDetailsCollection)
        {
            try
            {
                //Check for each each entry in CRDetails List
                foreach (BusinessEntities.Contract objAddCRDetails in objCRDetailsCollection)
                {
                    if (objAddCRDetails.Mode == "1")
                    {
                        this.AddCRDetails(objAddCRDetails);
                    }
                    if (objAddCRDetails.Mode == "2")
                    {
                        this.UpdateCRDetails(objAddCRDetails);
                    }
                    if (objAddCRDetails.CRId > 0 && objAddCRDetails.Mode == "3")
                    {
                        this.DeleteCRDetails(objAddCRDetails);
                    }
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACT, "Manipulation", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        public bool checkCRReferenceNo(BusinessEntities.Contract objCRDetails)
        {
            try
            {
                //Object declaration of QualificationDetails class
                Rave.HR.DataAccessLayer.Contracts.Contract objCRDetailsDAL;

                bool Check = false;

                objCRDetailsDAL = new Rave.HR.DataAccessLayer.Contracts.Contract();

                Check = objCRDetailsDAL.checkCRReferenceNo(objCRDetails);

                return Check;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACT, "checkCRReferenceNo", EventIDConstants.RAVE_HR_CONTRACT_BUSNIESS_LAYER);
            }
        }

        #endregion Public Method

        #region Private Methods

        /// <summary>
        /// Send mail for save projects.
        /// </summary>
        /// <param name="contractData">contract details</param>
        /// <param name="listProjectDetails">project deatils</param>
        /// <param name="allProjectsName">all associated projects</param>
        private void SendMailForSaveContract(BusinessEntities.Contract contractData,
                                            List<BusinessEntities.ContractProject> listProjectDetails,
                                            string allProjectsName)
        {
            try
            {
                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Contract),
                                            Convert.ToInt16(EnumsConstants.EmailFunctionality.AddedContract));

                obj.CC.Add(contractData.EmailID);

                obj.Subject = string.Format(obj.Subject, contractData.ContractCode,
                                                         contractData.ContractReferenceID,
                                                         contractData.ContractClientName,
                                                         allProjectsName);
                obj.Body = string.Format(obj.Body, contractData.ContractCode,
                                                   contractData.ContractClientName,
                                                   GetHTMLForContractTableData(contractData),
                                                   GetHTMLForTableData(listProjectDetails),
                                                   GetLinkForEmail(contractData.ContractID));

                obj.SendEmail(obj);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACT, "SendMailForSaveContract", EventIDConstants.RAVE_HR_CONTRACT_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Send mail for save projects.
        /// </summary>
        /// <param name="contractData">contract details</param>
        /// <param name="listProjectDetails">project deatils</param>
        /// <param name="allProjectsName">all associated projects</param>
        private void SendMailForEditContract(BusinessEntities.Contract contractDetails,
                                            List<BusinessEntities.ContractProject> listProjectDetails,
                                            string allProjectsName,RaveHRCollection ContractCRdetails)
        {
            try
            {
                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Contract),
                                             Convert.ToInt16(EnumsConstants.EmailFunctionality.EditedContract));

                obj.CC.Add(contractDetails.EmailID);

                obj.Subject = string.Format(obj.Subject, contractDetails.ContractCode,
                                                         contractDetails.ContractReferenceID,
                                                         contractDetails.ContractClientName,
                                                         allProjectsName);
                if (ContractCRdetails.Count == 0)
                    obj.Subject = obj.Subject.Replace(", new Change Request added", string.Empty);  

                obj.Body = string.Format(obj.Body, contractDetails.ContractCode,
                                                   contractDetails.ContractReferenceID,
                                                   contractDetails.ContractClientName,
                                                   GetHTMLForCRTableData(ContractCRdetails),
                                                   GetHTMLForTableData(listProjectDetails),
                                                   GetLinkForEmail(contractDetails.ContractID),
                                                   //ambar Issue Id:26114
                                                   GetHTMLForContractEditedTableData(contractDetails)
                                                   );
                obj.SendEmail(obj);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACT, "SendMailForSaveContract", EventIDConstants.RAVE_HR_CONTRACT_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Send mail after deleting contract.
        /// </summary>
        /// <param name="ContractDetails"></param>
        private void SendMailForDeleteContract(BusinessEntities.Contract ContractDetails)
        {
            try
            {
                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Contract),
                                             Convert.ToInt16(EnumsConstants.EmailFunctionality.DeletedContract));

                obj.Subject = string.Format(obj.Subject, ContractDetails.ContractCode,
                                                         ContractDetails.ContractReferenceID,
                                                         ContractDetails.ContractClientName);
                obj.Body = string.Format(obj.Body, ContractDetails.ContractCode,
                                                   ContractDetails.ContractReferenceID,
                                                   ContractDetails.ContractClientName,
                                                   ContractDetails.ReasonForDeletion,
                                                   GetLinkForEmail(ContractDetails.ContractID));
                obj.SendEmail(obj);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACT, "SendMailForDeleteContract", EventIDConstants.RAVE_HR_CONTRACT_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Get Table data in HTML format.
        /// </summary>
        /// <param name="listProjectDetails"></param>
        /// <returns></returns>
        private string GetHTMLForTableData(List<BusinessEntities.ContractProject> listProjectDetails)
        {
            string bodyTable = string.Empty;
            if (listProjectDetails.Count > 0)
            {
                bodyTable = "<br/>The following {0} associated with the above contract.<br/><br/>";
                string multipleProjects = listProjectDetails.Count == 1 ? "Project is" : "Projects are";
                bodyTable = string.Format(bodyTable, multipleProjects);

                string[] header = new string[6];
                header[0] = "Project Code";
                header[1] = "Project Name";
                header[2] = "Start Date";
                header[3] = "End Date";
                header[4] = "Project Description";
                header[5] = "Project Type";

                string[,] arrayData = new string[(listProjectDetails.Count), 6];

                int rowCounter = 0;
                foreach (BusinessEntities.ContractProject contractProject in listProjectDetails)
                {
                    arrayData[rowCounter, 0] = contractProject.ProjectCode;
                    arrayData[rowCounter, 1] = contractProject.ProjectName;
                    arrayData[rowCounter, 2] = contractProject.ProjectStartDate.ToString(CommonConstants.DATE_FORMAT);
                    arrayData[rowCounter, 3] = contractProject.ProjectEndDate.ToString(CommonConstants.DATE_FORMAT);
                    arrayData[rowCounter, 4] = contractProject.ProjectsDescription;
                    arrayData[rowCounter, 5] = contractProject.ProjectType;

                    rowCounter++;
                }

                IEmailTableData objEmailTableData = new EmailTableData();
                objEmailTableData.Header = header;
                objEmailTableData.RowDetail = arrayData;
                objEmailTableData.RowCount = listProjectDetails.Count;

                bodyTable += objEmailTableData.GetTableData(objEmailTableData);
            }
            return bodyTable;
        }

        /// <summary>
        /// Get page link.
        /// </summary>
        /// <param name="contractId"></param>
        /// <returns></returns>
        private string GetLinkForEmail(int contractId)
        {
            return Utility.GetUrl() + CommonConstants.ADDCONTRACT_PAGE + "?" + URLHelper.SecureParameters(CommonConstants.CON_QSTRING_CONTRACTID, contractId.ToString()) + "&" + URLHelper.CreateSignature(contractId.ToString());
        }

        /// <summary>
        /// Save project Details.
        /// </summary>
        /// <param name="contractAdd"></param>
        /// <param name="projectDetails"></param>
        /// <returns></returns>
        private bool SaveProjects(BusinessEntities.Contract contractAdd,
                                 DataTable projectDetails)
        {
            int counter = 0;
            bool sucess = true;
            string ProjectCodeAbbreviation = string.Empty;
            StringBuilder allProjectsName = new StringBuilder(string.Empty);
            try
            {
                List<BusinessEntities.ContractProject> listProjectData = new List<BusinessEntities.ContractProject>();

                contractAdd.ContractCode = "C" + contractAdd.ContractID.ToString();

                if (projectDetails != null)
                {
                    Rave.HR.BusinessLayer.Contracts.ContractProject ContractProjectBL = new Rave.HR.BusinessLayer.Contracts.ContractProject();

                    foreach (DataRow dr in projectDetails.Rows)
                    {
                        counter++;
                        BusinessEntities.ContractProject ProjectData = new BusinessEntities.ContractProject();

                        ProjectData.ClientName = contractAdd.ClientName;
                        ProjectData.ContractID = contractAdd.ContractID;

                        ProjectData.ProjectName = dr[DbTableColumn.Con_ProjectName].ToString();
                        ProjectData.ProjectType = dr[DbTableColumn.ProjectType].ToString();
                        ProjectData.ProjectTypeID = Convert.ToInt32(dr[DbTableColumn.ProjectTypeID]);
                        
                        ProjectData.ProjectLocationName = dr[DbTableColumn.ProjectLocation].ToString();
                        ProjectData.ProjectStartDate = Convert.ToDateTime(dr[DbTableColumn.ProjectStartDate]);
                        ProjectData.ProjectEndDate = Convert.ToDateTime(dr[DbTableColumn.ProjectEndDate]);

                        ProjectData.CreatedByEmailId = contractAdd.CreatedByEmailId;
                        ProjectData.NoOfResources = Convert.ToDecimal(dr[DbTableColumn.NoOfResources]);
                        ProjectData.ProjectCode = contractAdd.ClinetAbbrivation + "_" + contractAdd.ProjectAbbrivation + "_" + contractAdd.Phase;
                        
                        //Rakesh : HOD for Employees 11/07/2016 Begin   
                        ProjectData.ProjectHeadId = Convert.ToInt32(dr[DbTableColumn.ProjectHeadId]);
                        //Rakesh : HOD for Employees 11/07/2016 End


                        

                        if (dr[DbTableColumn.Description].ToString() != string.Empty)
                        {
                            ProjectData.ProjectsDescription = dr[DbTableColumn.Description].ToString();
                        }
                        else
                        {
                            ProjectData.ProjectsDescription = string.Empty;
                        }
                        ProjectData.ProjectCategoryID = Convert.ToInt32(dr[DbTableColumn.Con_ProjectCategoryID]);
                        ProjectData.ProjectCodeAbbreviation = contractAdd.ClinetAbbrivation + "_" + contractAdd.ProjectAbbrivation + "_" + contractAdd.Phase;

                        // Mohamed : Issue  : 26/09/2014 : Starts                        			  
                        // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
                        ProjectData.ProjectGroup = dr[DbTableColumn.Con_ProjectGroup].ToString();
                        ProjectData.ProjectDivision = Convert.ToInt32(dr[DbTableColumn.Con_ProjectDivision].ToString());
                        ProjectData.ProjectBussinessArea = Convert.ToInt32(dr[DbTableColumn.Con_ProjectBusinessArea].ToString());
                        ProjectData.ProjectBussinessSegment = Convert.ToInt32(dr[DbTableColumn.Con_ProjectBusinessSegment].ToString());
                        //ProjectData.ProjectAlias = dr[DbTableColumn.Con_ProjectAlias].ToString();
                        // Mohamed : Issue  : 26/09/2014 : Ends

                        //Siddharth 9 Sept 2015 Start
                        ProjectData.BusinessVertical = dr[DbTableColumn.BusinessVertical].ToString();
                        ProjectData.ProjectModel = dr[DbTableColumn.ProjectModel].ToString();
                        //Siddharth 9 Sept 2015 End


                        sucess = ContractProjectBL.Save(ProjectData, ref ProjectCodeAbbreviation);
                        
                        if (sucess == true)
                        {
                            ProjectData.ProjectCodeAbbreviation = ProjectCodeAbbreviation;
                        }
                        listProjectData.Add(ProjectData);

                        if (counter == 1)
                        {
                            allProjectsName.Append(",Project:");
                        }
                        //append all projects name to dispaly in Email.
                        allProjectsName.Append(",");
                        allProjectsName.Append(ProjectData.ProjectName);

                    }
                }
                if (sucess == true)
                {
                    SendMailForSaveContract(contractAdd, listProjectData, allProjectsName.ToString());
                }
                return sucess;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CONTRACT, "SaveProjects", EventIDConstants.RAVE_HR_CONTRACT_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Edit project Details.
        /// </summary>
        /// <param name="contractDetails"></param>
        /// <param name="contractProject"></param>
        /// <returns></returns>
        private bool EditProjects(BusinessEntities.Contract contractDetails,
                                  DataTable contractProject, RaveHRCollection CRDetailsCollection)
        {
            int counter = 0;
            bool sucess = true;
            Rave.HR.BusinessLayer.Contracts.ContractProject editContractProject = new Rave.HR.BusinessLayer.Contracts.ContractProject();
            List<BusinessEntities.ContractProject> listProjectData = new List<BusinessEntities.ContractProject>();
            StringBuilder allProjectsName = new StringBuilder(string.Empty);
            RaveHRCollection AddedCRDetailsCollection = new RaveHRCollection();

            foreach (DataRow dr in contractProject.Rows)
            {
                counter++;

                BusinessEntities.ContractProject ProjectData = new BusinessEntities.ContractProject();
                
                ProjectData.ContractID = contractDetails.ContractID;
                ProjectData.ProjectType = dr[DbTableColumn.ProjectType].ToString();
                ProjectData.ProjectStartDate = Convert.ToDateTime(dr[DbTableColumn.ProjectStartDate]);
                ProjectData.ProjectEndDate = Convert.ToDateTime(dr[DbTableColumn.ProjectEndDate]);
                ProjectData.ProjectName = dr[DbTableColumn.Con_ProjectName].ToString();
                ProjectData.NoOfResources = Convert.ToDecimal(dr[DbTableColumn.NoOfResources]);
                ProjectData.ProjectsDescription = dr[DbTableColumn.Description].ToString();
                ProjectData.ProjectCategoryID = Convert.ToInt32(dr[DbTableColumn.Con_ProjectCategoryID]);
                // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
                // Desc : Add project group in Contract page
                ProjectData.ProjectGroup = dr[DbTableColumn.ProjectGroup].ToString();
                // Mohamed : Issue 49791 : 15/09/2014 : Ends

                // Mohamed : Issue  : 23/09/2014 : Starts                        			  
                // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
                ProjectData.ProjectDivision = Convert.ToInt32(dr[DbTableColumn.ProjectDivision].ToString());
                ProjectData.ProjectBussinessArea = Convert.ToInt32(dr[DbTableColumn.ProjectBusinessArea].ToString());
                ProjectData.ProjectBussinessSegment = Convert.ToInt32(dr[DbTableColumn.ProjectBusinessSegment].ToString());
                //ProjectData.ProjectAlias = dr[DbTableColumn.ProjectAlias].ToString();
                // Mohamed : Issue  : 23/09/2014 : Ends

                //Siddharth 13 March 2015 Start

                //Note the below line is commented bcoz in session First time Project model is not stored
                //ProjectData.ProjectModel = dr[DbTableColumn.ProjectModel].ToString();
                ProjectData.ProjectModel = dr[DbTableColumn.ProjectModel].ToString();
                //Siddharth 13 March 2015 End

                //Siddharth 9 Sept 2015 Start
                ProjectData.BusinessVertical = dr[DbTableColumn.BusinessVertical].ToString();
                //Siddharth 9 Sept 2015 End


                //Added by Shrinivas for implementing new project code
                if (!string.IsNullOrEmpty(dr[DbTableColumn.ProjectCode].ToString()))
                    ProjectData.ProjectCode = dr[DbTableColumn.ProjectCode].ToString();              
                else
                    ProjectData.ProjectCode = contractDetails.ClinetAbbrivation + "_" + contractDetails.ProjectAbbrivation + "_" + contractDetails.Phase;

                //Edit only those have project code.
                if (dr[DbTableColumn.ProjectCode].ToString() != string.Empty)
                {
                    sucess = editContractProject.Edit(ProjectData, null);
                }
                listProjectData.Add(ProjectData);

                if (counter == 1)
                {
                    allProjectsName.Append(",Project:");
                }
                //append all projects name to dispaly in Email.
                allProjectsName.Append(",");
                allProjectsName.Append(ProjectData.ProjectName);

            }

            foreach (BusinessEntities.Contract CRdetails in CRDetailsCollection)
            {
                if (CRdetails.Mode == "1")
                {
                    AddedCRDetailsCollection.Add(CRdetails);
                }
            }

            if (sucess)
            {
                SendMailForEditContract(contractDetails, listProjectData, allProjectsName.ToString(), AddedCRDetailsCollection);
            }
            return sucess;
        }

        /// <summary>
        /// Gets the HTML for CR table data.
        /// </summary>
        /// <param name="contractCRDetails">The contract CR details.</param>
        /// <returns></returns>
        private string GetHTMLForCRTableData(RaveHRCollection contractCRDetails)
        {
            string bodyTable = string.Empty;
            if (contractCRDetails.Count > 0)
            {
                bodyTable = "<br/>A new Change Request (CR) has been added to contract.<br/><br/>";
                
                string[] header = new string[4];
                header[0] = "CR Reference No";
                header[1] = "Start Date";
                header[2] = "End Date";
                header[3] = "Remarks";

                string[,] arrayData = new string[(contractCRDetails.Count), 4];

                int rowCounter = 0;
                foreach (BusinessEntities.Contract contractCR in contractCRDetails)
                {
                    arrayData[rowCounter, 0] = contractCR.CRReferenceNo;
                    arrayData[rowCounter, 1] = contractCR.CRStartDate.ToString(CommonConstants.DATE_FORMAT);
                    arrayData[rowCounter, 2] = contractCR.CREndDate.ToString(CommonConstants.DATE_FORMAT);
                    arrayData[rowCounter, 3] = contractCR.CRRemarks;

                    rowCounter++;
                }

                IEmailTableData objEmailTableData = new EmailTableData();
                objEmailTableData.Header = header;
                objEmailTableData.RowDetail = arrayData;
                objEmailTableData.RowCount = contractCRDetails.Count;

                bodyTable += objEmailTableData.GetTableData(objEmailTableData);
            }
            return bodyTable;
        }

        /// <summary>
        /// Gets the HTML for contract table data.
        /// </summary>
        /// <param name="contractDetails">The contract details.</param>
        /// <returns></returns>
        private string GetHTMLForContractTableData(BusinessEntities.Contract contractDetails)
        {
            string bodyTable = string.Empty;
            if (contractDetails != null)
            {
                string[] header = new string[5];
                header[0] = "Contract Type";
                header[1] = "Contract Ref. Id";
                header[2] = "Start Date";
                header[3] = "End Date";
                header[4] = "Document Name";

                string[,] arrayData = new string[1, 5];

                int rowCounter = 0;

                arrayData[rowCounter, 0] = contractDetails.ContractTypeName;
                arrayData[rowCounter, 1] = contractDetails.ContractReferenceID;
                arrayData[rowCounter, 2] = contractDetails.ContractStartDate.ToString(CommonConstants.DATE_FORMAT);
                arrayData[rowCounter, 3] = contractDetails.ContractEndDate.ToString(CommonConstants.DATE_FORMAT);
                arrayData[rowCounter, 4] = contractDetails.DocumentName;

                rowCounter++;
               
                IEmailTableData objEmailTableData = new EmailTableData();
                objEmailTableData.Header = header;
                objEmailTableData.RowDetail = arrayData;
                objEmailTableData.RowCount = 1;

                bodyTable += objEmailTableData.GetTableData(objEmailTableData);
            }
            return bodyTable;
        }

        //Added by Ambar : Issue Id 26114
        private string GetHTMLForContractEditedTableData(BusinessEntities.Contract contractDetails)
        {
            string bodyTable = string.Empty;
            if (contractDetails != null)
            {
                bool b_no_changes = false;

                string[] header = new string[16];
                int i=0;
                string[,] arrayData = new string[1, 16];

                if (contractDetails.PreviousAccountManagerName != null)
                {
                    header[i] = "Prev.AccountMgrName";
                    arrayData[0, i] = contractDetails.PreviousAccountManagerName;
                    i++;
                    header[i] = "AccountMgrName";
                    arrayData[0, i] = contractDetails.TempAccountManagerName;
                    i++;
                    b_no_changes = true;
                }

                if ( (contractDetails.PreviousContractEndDate != null) && (contractDetails.PreviousContractEndDate.ToString() != "01/01/0001 00:00:00" ) )
                {
                    header[i] = "Prev.ContractEndDt";
                    arrayData[0, i] = contractDetails.PreviousContractEndDate.ToString();
                    i++;
                    header[i] = "ContractEndDt";
                    arrayData[0, i] = contractDetails.ContractEndDate.ToString();
                    i++;
                    b_no_changes = true;
                }

                if (contractDetails.PreviousContractReferenceID != null)  
                {
                    header[i] = "Prev.ContractRefId";
                    arrayData[0, i] = contractDetails.PreviousContractReferenceID.ToString();
                    i++;
                    header[i] = "ContractRefId";
                    arrayData[0, i] = contractDetails.ContractReferenceID.ToString();
                    i++;
                    b_no_changes = true;
                }

                if ((contractDetails.PreviousContractStartDate != null) && (contractDetails.PreviousContractStartDate.ToString() != "01/01/0001 00:00:00"))
                {
                    header[i] = "Prev.ContractStartDt";
                    arrayData[0, i] = contractDetails.PreviousContractStartDate.ToString();
                    i++;
                    header[i] = "ContractStartDt";
                    arrayData[0, i] = contractDetails.ContractStartDate.ToString();
                    i++;
                    b_no_changes = true;
                }

                 if (contractDetails.PreviousContractType != null)
                {
                    header[i] = "Prev.ContractType";
                    arrayData[0, i] = contractDetails.PreviousContractType.ToString();
                    i++;
                    header[i] = "ContractType";
                    arrayData[0, i] = contractDetails.ContractTypeName.ToString();
                    i++;
                    b_no_changes = true;
                }

                 if (contractDetails.PreviousCurrencyType != null)
                {
                    header[i] = "Prev.CurrencyType";
                   
                    arrayData[0, i] = contractDetails.PreviousCurrencyType.ToString();
                    i++;
                    header[i] = "CurrencyType";
                    arrayData[0, i] = contractDetails.TempCurrencyName.ToString();
                    i++;
                    b_no_changes = true;
                }

                 if (contractDetails.PreviousDocumentName != null)
                {
                    header[i] = "PrevDocumentName";
                    arrayData[0, i] = contractDetails.PreviousDocumentName.ToString();
                    i++;
                    header[i] = "DocumentName";
                    arrayData[0, i] = contractDetails.DocumentName.ToString();
                    i++;
                    b_no_changes = true;
                }

                 if (contractDetails.PreviousContractValue != 0)
                //if(contractDetails.flg=true)
                 //else
                 {
                     header[i] = "Prev.ContractValue";
                     arrayData[0, i] = String.Format("{0:0.##}",contractDetails.PreviousContractValue);
                     i++;
                     header[i] = "ContractValue";
                     arrayData[0, i] = contractDetails.ContractValue.ToString();
                     i++;
                     b_no_changes = true;
                 }

                 if (!b_no_changes)
                 {
                     header[i] = "No Changes has been made to contract";
                     arrayData[0, i] = "No Changes has been made to contract";
                     i++;
                 }

                string[] header_new = new string[i];
                string[,] arrayData_new = new string[1, i]; 
                
                for (int j = 0; j < i; j++)
                {
                    header_new[j] = header[j];
                }

                if (!b_no_changes)
                 {
                     i--;
                 }

                for (int k = 0; k < i; k++)
                {
                    arrayData_new[0, k] = arrayData[0, k];
                }

                IEmailTableData objEmailTableData = new EmailTableData();
                objEmailTableData.Header = header_new;
                objEmailTableData.RowDetail = arrayData_new;
                objEmailTableData.RowCount = 1;

                bodyTable += objEmailTableData.GetTableData(objEmailTableData);
            }

            return bodyTable;
        }
        //End :Ambar
        #endregion Private Methods

    }
}
