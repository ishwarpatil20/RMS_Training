//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           ContractProject.cs       
//  Class:          ContractProject
//  Author:         prashant.mala
//  Date written:   8/05/2009 5:48:30 PM
//  Description:    This class contains methods related to Contract module. 
//                  These methods are used for ProjectSummary.aspx etc.
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
using Common;
using System.Data;
using System.Data.SqlClient;
using Common.Constants;
using BusinessEntities;

namespace Rave.HR.DataAccessLayer.Contracts
{

    public class ContractProject
    {
        BusinessEntities.ContractProject objContractProject = null;

        //Define the Class name.
        const string CLASSNAME = "ContractProject.cs";

        private static BusinessEntities.RaveHRCollection raveHRCollection;

        #region Public Memeber Functions

        #region GetContractsForProject

        /// <summary>
        /// This Function is used to get list of contracts for project
        /// </summary>
        /// <param name="objRaveHRContractProject"></param>
        /// <returns>List</returns>
        public List<BusinessEntities.ContractProject> GetContractsForProject(BusinessEntities.ContractProject objContractProject)
        {

            //Initialise a list of BusinessEntities.ContractProject type.

            List<BusinessEntities.ContractProject> objListContractsForProject = null;

            //Initialse a object of DataAccessClass.

            DataAccessClass objDAContractsForProject = new DataAccessClass();

            try
            {
                //opens the connection.
                objDAContractsForProject.OpenConnection(DBConstants.GetDBConnectionString());

                //Initialise a object of SqlParameter.                
                SqlParameter[] sqlParam = new SqlParameter[1];

                //Defined and assigned variables of SqlParameter.
                sqlParam[0] = new SqlParameter(SPParameter.iProjectID, DbType.Int32);
                sqlParam[0].Value = objContractProject.ProjectID;

                //--get result
                DataSet dsContractsForProject = objDAContractsForProject.GetDataSet(SPNames.Projects_GetContractsForProject, sqlParam);

                //Create entities and add to list
                BusinessEntities.ContractProject objBEContractsForProject = null;
                objListContractsForProject = new List<BusinessEntities.ContractProject>();
                foreach (DataRow dr in dsContractsForProject.Tables[0].Rows)
                {
                    objBEContractsForProject = new BusinessEntities.ContractProject();
                    objBEContractsForProject.ContractID = int.Parse(dr[DbTableColumn.Con_ContractId].ToString());
                    objBEContractsForProject.ContractCode = dr[DbTableColumn.Con_ContractCode].ToString();
                    objBEContractsForProject.ContractReferenceID = dr[DbTableColumn.Con_ContractRefId].ToString();
                    objBEContractsForProject.ContractType = dr[DbTableColumn.Con_ContractType].ToString();
                    objBEContractsForProject.ContractTypeId = dr[DbTableColumn.Con_ContractTypeId].ToString();
                    objBEContractsForProject.DocumentType = dr[DbTableColumn.Con_DocumentType].ToString();

                    //Ishwar : Issue 49176 : 20/02/2014 : Starts                        			                      
                    if (dr[DbTableColumn.Con_ContractEndDate]!= DBNull.Value)
                        objBEContractsForProject.ContractEndDate = Convert.ToDateTime(dr[DbTableColumn.Con_ContractEndDate]);                        
                    //Ishwar : Issue 49176 : 20/02/2014 : Ends

                    //added to list
                    objListContractsForProject.Add(objBEContractsForProject);
                }
                return objListContractsForProject;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, "GetContractsForProject", EventIDConstants.RAVE_HR_CONTRACT_DATA_ACCESS_LAYER);
            }
            finally
            {
                //closes the connection.
                objDAContractsForProject.CloseConncetion();
            }
        }

        #endregion GetContractsForProject

        #region Save

        /// <summary>
        /// This Function is used to save the project details associated to contracts
        /// <param name="addContractProject"></param>
        ///  <param name="contractId"></param>
        /// <returns></returns>
        public bool Save(BusinessEntities.ContractProject addContractProject,ref string ProjectCodeAbbreviation)
        {
            SqlConnection objConnection = null;
            //SqlCommand objCommand = null;
            string ConnStr = string.Empty;
            DataAccessClass objDA = new DataAccessClass();
            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //objCommand = new SqlCommand(SPNames.Contract_AddProject, objConnection);
                //objCommand.CommandType = CommandType.StoredProcedure;

                //objCommand.Parameters.AddWithValue(SPParameter.ClientName, addContractProject.ClientName);
                //objCommand.Parameters.AddWithValue(SPParameter.ContractId, addContractProject.ContractID);
                //objCommand.Parameters.AddWithValue(SPParameter.ProjectName, addContractProject.ProjectName);
                //objCommand.Parameters.AddWithValue(SPParameter.ProjectType, addContractProject.ProjectTypeID);
                //objCommand.Parameters.AddWithValue(SPParameter.Location, addContractProject.ProjectLocationName);
                //objCommand.Parameters.AddWithValue(SPParameter.StartDate, addContractProject.ProjectStartDate);
                //objCommand.Parameters.AddWithValue(SPParameter.EndDate, addContractProject.ProjectEndDate);
                //objCommand.Parameters.AddWithValue(SPParameter.CreatedBy, addContractProject.CreatedByEmailId);
                //objCommand.Parameters.AddWithValue(SPParameter.NoOfResource, addContractProject.NoOfResources);
                //objCommand.Parameters.AddWithValue(SPParameter.Description, addContractProject.ProjectsDescription);
                //objCommand.Parameters.AddWithValue(SPParameter.ProjectCategoryID, addContractProject.ProjectCategoryID);
                //objCommand.Parameters.AddWithValue(SPParameter.ClientProjectAbbrivation, addContractProject.ProjectCodeAbbreviation);

                //Siddharth 9 Sept 2015 Start
                //Made the parameter size from 18 to 20
                SqlParameter[] sqlParam = new SqlParameter[21];
                //Siddharth 9 Sept 2015 End

                sqlParam[0] = new SqlParameter(SPParameter.ClientName, SqlDbType.VarChar,20);
                sqlParam[0].Value = addContractProject.ClientName;

                sqlParam[1] = new SqlParameter(SPParameter.ContractId, SqlDbType.Int);
                sqlParam[1].Value = addContractProject.ContractID;

                sqlParam[2] = new SqlParameter(SPParameter.ProjectName, SqlDbType.VarChar,50);
                sqlParam[2].Value = addContractProject.ProjectName;

                sqlParam[3] = new SqlParameter(SPParameter.ProjectType, SqlDbType.Int);
                sqlParam[3].Value = addContractProject.ProjectTypeID;

                sqlParam[4] = new SqlParameter(SPParameter.Location, SqlDbType.VarChar,30);
                sqlParam[4].Value = addContractProject.ProjectLocationName;

                sqlParam[5] = new SqlParameter(SPParameter.StartDate, SqlDbType.DateTime);
                sqlParam[5].Value = addContractProject.ProjectStartDate;

                sqlParam[6] = new SqlParameter(SPParameter.EndDate, SqlDbType.DateTime);
                sqlParam[6].Value = addContractProject.ProjectEndDate;

                sqlParam[7] = new SqlParameter(SPParameter.CreatedBy, SqlDbType.VarChar,50);
                sqlParam[7].Value = addContractProject.CreatedByEmailId;

                //Bug Solved by Siddharth 30th Sept 2015 Start
                //Initially ProjectID was getting saved as No Of Resources
                sqlParam[8] = new SqlParameter(SPParameter.NoOfResource, SqlDbType.Decimal,10);
                sqlParam[8].Value = addContractProject.NoOfResources;
                //Bug Solved by Siddharth 30th Sept 2015 End

                sqlParam[9] = new SqlParameter(SPParameter.Description, SqlDbType.VarChar,Int32.MaxValue);
                sqlParam[9].Value = addContractProject.ProjectsDescription;

                sqlParam[10] = new SqlParameter(SPParameter.ProjectCategoryID, SqlDbType.Int);
                sqlParam[10].Value = addContractProject.ProjectCategoryID;

                sqlParam[11] = new SqlParameter(SPParameter.ClientProjectAbbrivation, SqlDbType.NVarChar,50);
                sqlParam[11].Value = addContractProject.ProjectCodeAbbreviation;

                // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
                // Desc : Add project group in Contract page
                sqlParam[12] = new SqlParameter(SPParameter.ProjectGroup, SqlDbType.NVarChar, 50);
                sqlParam[12].Value = addContractProject.ProjectGroup;
                // Mohamed : Issue 49791 : 15/09/2014 : Ends

                // Mohamed : Issue  : 23/09/2014 : Starts                        			  
                // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
                sqlParam[13] = new SqlParameter(SPParameter.ProjectDivision, SqlDbType.Int);
                sqlParam[13].Value = addContractProject.ProjectDivision;

                sqlParam[14] = new SqlParameter(SPParameter.ProjectAlias, SqlDbType.NVarChar, 200);
                sqlParam[14].Value = addContractProject.ProjectAlias;

                sqlParam[15] = new SqlParameter(SPParameter.ProjectBusinessArea, SqlDbType.Int);
                sqlParam[15].Value = addContractProject.ProjectBussinessArea;

                sqlParam[16] = new SqlParameter(SPParameter.ProjectBusinessSegment, SqlDbType.Int);
                sqlParam[16].Value = addContractProject.ProjectBussinessSegment;
                // Mohamed : Issue  : 23/09/2014 : Ends

                sqlParam[17] = new SqlParameter(SPParameter.ProjectCodeAbbrivation, SqlDbType.NVarChar,50);
                sqlParam[17].Direction = ParameterDirection.Output;

            
                sqlParam[18] = new SqlParameter(SPParameter.BusinessVertical, SqlDbType.Int);
                sqlParam[18].Value = addContractProject.BusinessVertical;

                sqlParam[19] = new SqlParameter(SPParameter.ProjectModel, SqlDbType.Int);
                sqlParam[19].Value = addContractProject.ProjectModel;

                sqlParam[20] = new SqlParameter(SPParameter.ProjectHeadId, SqlDbType.Int);
                sqlParam[20].Value = addContractProject.ProjectHeadId;

                int project = objDA.ExecuteNonQuerySP(SPNames.Contract_AddProject,sqlParam);

                ProjectCodeAbbreviation = sqlParam[17].Value.ToString();
                bool success = true;
                return success;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, "Save", EventIDConstants.RAVE_HR_CONTRACT_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }
        #endregion Save

        #region GetProjectForContracts

        /// <summary>
        /// This method will fetch records from data base and return to business layer
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public List<BusinessEntities.ContractProject> GetProjectForContracts(BusinessEntities.ContractCriteria criteria)
        {
            DataAccessClass objDAContractsForProject = new DataAccessClass();
            List<BusinessEntities.ContractProject> objListProjectsForContract = null;
            try
            {
                objDAContractsForProject.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter(SPParameter.ContractId, DbType.Int32);
                sqlParam[0].Value = criteria.ContractId;
                sqlParam[1] = new SqlParameter(SPParameter.Mode, DbType.Int32);
                sqlParam[1].Value = criteria.Mode;

                //--get result
                DataSet dsContractsForProject = objDAContractsForProject.GetDataSet(SPNames.Contract_GetContract, sqlParam);

                //--Create entities and add to list
                BusinessEntities.ContractProject objBEProjectsForContratcs = null;
                objListProjectsForContract = new List<BusinessEntities.ContractProject>();
                foreach (DataRow dr in dsContractsForProject.Tables[0].Rows)
                {
                    objBEProjectsForContratcs = new BusinessEntities.ContractProject();
                    if (dr[DbTableColumn.ProjectId] != null)
                        objBEProjectsForContratcs.ContractProjectID = Convert.ToInt32(dr[DbTableColumn.ProjectId].ToString());

                    if (dr[DbTableColumn.Con_ProjectCode] != null)
                        objBEProjectsForContratcs.ProjectCode = dr[DbTableColumn.Con_ProjectCode].ToString();

                    if (dr[DbTableColumn.Con_ProjectName] != null)
                        objBEProjectsForContratcs.ProjectName = dr[DbTableColumn.Con_ProjectName].ToString();

                    if (dr[DbTableColumn.ProjectType] != null)
                        objBEProjectsForContratcs.ProjectType = dr[DbTableColumn.ProjectType].ToString();

                    if (!string.IsNullOrEmpty(dr[DbTableColumn.Con_StartDate].ToString()))
                        objBEProjectsForContratcs.ProjectStartDate = Convert.ToDateTime(dr[DbTableColumn.Con_StartDate]);

                    if (!string.IsNullOrEmpty(dr[DbTableColumn.Con_EndDate].ToString()))
                        objBEProjectsForContratcs.ProjectEndDate = Convert.ToDateTime(dr[DbTableColumn.Con_EndDate]);

                    if (dr[DbTableColumn.ResourcePlanId] != null)
                        objBEProjectsForContratcs.RPId = Convert.ToInt32(dr[DbTableColumn.ResourcePlanId].ToString());

                    //--add to list
                    objListProjectsForContract.Add(objBEProjectsForContratcs);
                }

                return objListProjectsForContract;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, "GetProjectForContracts", EventIDConstants.RAVE_HR_CONTRACT_DATA_ACCESS_LAYER);
            }
            finally
            {

                objDAContractsForProject.CloseConncetion();

            }
        }

        #endregion GetProjectForContracts

        #region GetProjectdetails

        /// <summary>
        /// This method will fetch records from data base and return to business layer
        /// </summary>
        /// <param name=></param>
        /// <returns></returns>

        public List<BusinessEntities.ContractProject> GetProjectdetails()
        {
            List<BusinessEntities.ContractProject> objListOfProjects = null;
            DataAccessClass objProjectDetails = new DataAccessClass();
            try
            {
                objProjectDetails.OpenConnection(DBConstants.GetDBConnectionString());

                //gets the all project details.
                DataSet dsProjectdetails = objProjectDetails.GetDataSet(SPNames.Contract_SearchProjectDetails);

                //Create entities and add to list
                BusinessEntities.ContractProject objProjects = null;
                objListOfProjects = new List<BusinessEntities.ContractProject>();

                foreach (DataRow dr in dsProjectdetails.Tables[0].Rows)
                {
                    objProjects = new BusinessEntities.ContractProject();

                    objProjects.ProjectID = Convert.ToInt32(dr[DbTableColumn.Con_ProjectID]);

                    objProjects.ProjectCode = dr[DbTableColumn.Con_ProjectCode].ToString();

                    objProjects.DocumentName = dr[DbTableColumn.Con_DocumentName].ToString();

                    objProjects.ContractCode = dr[DbTableColumn.Con_ContractCode].ToString();

                    objProjects.ProjectName = dr[DbTableColumn.Con_ConProjectName].ToString();

                    objProjects.ContractType = dr[DbTableColumn.Con_ContractType].ToString();

                    //--add to list
                    objListOfProjects.Add(objProjects);
                }
                return objListOfProjects;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, "GetProjectdetails", EventIDConstants.RAVE_HR_CONTRACT_DATA_ACCESS_LAYER);
            }
            finally
            {
                objProjectDetails.CloseConncetion();
            }
        }

        #endregion GetProjectdetails

        #region getProjectDetailsByID

        /// <summary>
        /// this function gets the project details by ID.
        /// </summary>
        /// <param name="contractProject"></param>
        /// <returns></returns>

        public BusinessEntities.ContractProject getProjectDetailsByID(BusinessEntities.ContractProject contractProject)
        {
            // Initialise Data Access Class object
            DataAccessClass objDA = new DataAccessClass();

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                objContractProject = new BusinessEntities.ContractProject();

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.PROJECTNAME, DbType.String);
                sqlParam[0].Value = contractProject.ProjectCode;

                //gets the all project details.
                DataSet dsProjectdetails = objDA.GetDataSet(SPNames.Contract_CheckProjectName, sqlParam);

                foreach (DataRow dr in dsProjectdetails.Tables[0].Rows)
                {
                    if (dr[DbTableColumn.Con_ContractProjectId].ToString() != null)
                    {
                        objContractProject.ProjectID = Convert.ToInt32(dr[DbTableColumn.Con_ContractProjectId]);
                    }
                    if (dr[DbTableColumn.Con_ProjectCode] != null)
                    {
                        objContractProject.ProjectCode = Convert.ToString(dr[DbTableColumn.Con_ProjectCode]);
                    }
                    if (dr[DbTableColumn.ProjectType] != null)
                    {
                        objContractProject.ProjectType = Convert.ToString(dr[DbTableColumn.ProjectType]);
                    }
                    if (dr[DbTableColumn.Con_ProjectName] != null)
                    {
                        objContractProject.ProjectName = Convert.ToString(dr[DbTableColumn.Con_ProjectName]);
                    }
                    if (dr[DbTableColumn.Con_ResourceNo] != null)
                    {
                        objContractProject.NoOfResources = Convert.ToDecimal(dr[DbTableColumn.Con_ResourceNo]);
                    }

                    if (!string.IsNullOrEmpty(dr[DbTableColumn.Con_StartDate].ToString()))
                    {
                        objContractProject.ProjectStartDate = Convert.ToDateTime(dr[DbTableColumn.Con_StartDate].ToString());
                    }

                    if (!string.IsNullOrEmpty(dr[DbTableColumn.Con_EndDate].ToString()))
                    {
                        objContractProject.ProjectEndDate = Convert.ToDateTime(dr[DbTableColumn.Con_EndDate].ToString());
                    }
                    if (dr[DbTableColumn.Location].ToString() != null)
                    {
                        objContractProject.ProjectLocationName = Convert.ToString(dr[DbTableColumn.Location]);
                    }
                    if (dr[DbTableColumn.Description].ToString() != null)
                    {
                        objContractProject.ProjectsDescription = dr[DbTableColumn.Description].ToString();
                    }
                    if (dr[DbTableColumn.Con_ProjectCategoryID] != null)
                    {
                        objContractProject.ProjectCategoryID = Convert.ToInt32(dr[DbTableColumn.Con_ProjectCategoryID]);
                    }
                    //if (dr[DbTableColumn.Con_ProjectCategoryName].ToString() != null)
                    //{
                    //    objContractProject.ProjectCategoryName = dr[DbTableColumn.Con_ProjectCategoryName].ToString();
                    //}

                    
                }


                return objContractProject;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, "getProjectDetailsByID", EventIDConstants.RAVE_HR_CONTRACT_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        #endregion getProjectDetailsByID

        #region ProjectDetail

        /// <summary>
        /// gets the project Details.
        /// </summary>
        /// <param name="contractProject"></param>
        /// <returns></returns>
        public BusinessEntities.ContractProject ProjectDetail(BusinessEntities.ContractProject contractProject)
        {
            // Initialise Data Access Class object
            DataAccessClass objDA = new DataAccessClass();
            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                objContractProject = new BusinessEntities.ContractProject();

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.PROJECTNAME, DbType.String);
                sqlParam[0].Value = contractProject.ProjectName;

                //gets the all project details.
                DataSet dsProjectdetails = objDA.GetDataSet(SPNames.Contract_CheckProject, sqlParam);

                foreach (DataRow dr in dsProjectdetails.Tables[0].Rows)
                {
                    if (dr[DbTableColumn.Con_ContractProjectId].ToString() != null)
                    {
                        objContractProject.ProjectID = Convert.ToInt32(dr[DbTableColumn.Con_ContractProjectId]);
                    }
                    if (dr[DbTableColumn.Con_ProjectCode] != null)
                    {
                        objContractProject.ProjectCode = Convert.ToString(dr[DbTableColumn.Con_ProjectCode]);
                    }
                    if (dr[DbTableColumn.ProjectType] != null)
                    {
                        objContractProject.ProjectType = Convert.ToString(dr[DbTableColumn.ProjectType]);
                    }
                    if (dr[DbTableColumn.Con_ProjectName] != null)
                    {
                        objContractProject.ProjectName = Convert.ToString(dr[DbTableColumn.Con_ProjectName]);
                    }
                    if (dr[DbTableColumn.Con_ResourceNo] != null)
                    {
                        objContractProject.NoOfResources = Convert.ToInt32(dr[DbTableColumn.Con_ResourceNo]);
                    }

                    if (!string.IsNullOrEmpty(dr[DbTableColumn.Con_StartDate].ToString()))
                    {
                        objContractProject.ProjectStartDate = Convert.ToDateTime(dr[DbTableColumn.Con_StartDate].ToString());
                    }

                    if (!string.IsNullOrEmpty(dr[DbTableColumn.Con_EndDate].ToString()))
                    {
                        objContractProject.ProjectEndDate = Convert.ToDateTime(dr[DbTableColumn.Con_EndDate].ToString());
                    }
                    if (dr[DbTableColumn.Location].ToString() != null)
                    {
                        objContractProject.ProjectLocationName = Convert.ToString(dr[DbTableColumn.Location]);
                    }
                    if (dr[DbTableColumn.Con_ProjectCategoryID] != null)
                    {
                        objContractProject.ProjectCategoryID = Convert.ToInt32(dr[DbTableColumn.Con_ProjectCategoryID]);
                    }
                    //if (dr[DbTableColumn.Con_ProjectCategoryName].ToString() != null)
                    //{
                    //    objContractProject.ProjectCategoryName = dr[DbTableColumn.Con_ProjectCategoryName].ToString();
                    //}
                }

                return objContractProject;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, "getProjectDetailsByID", EventIDConstants.RAVE_HR_CONTRACT_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }

        }
        #endregion ProjectDetail

        #region Edit

        /// <summary>
        /// This Function is used to Edit the project details associated to contracts
        /// </summary>
        /// <param name="addContractProject"></param>
        ///  <param name="contractId"></param>
        /// <returns></returns>
        public int Edit(BusinessEntities.ContractProject addContractProject, string PrjLocation)
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.Contract_EditProject, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;

                objCommand.Parameters.AddWithValue(SPParameter.ProjectCode, addContractProject.ProjectCode);

                objCommand.Parameters.AddWithValue(SPParameter.NoOfResource, addContractProject.NoOfResources);

                objCommand.Parameters.AddWithValue(SPParameter.Description, addContractProject.ProjectsDescription);

                objCommand.Parameters.AddWithValue(SPParameter.ContractId, addContractProject.ContractID);

                objCommand.Parameters.AddWithValue(SPParameter.ProjectCategoryID, addContractProject.ProjectCategoryID);

                // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
                // Desc : Add project group in Contract page
                objCommand.Parameters.AddWithValue(SPParameter.ProjectGroup, addContractProject.ProjectGroup);
                objCommand.Parameters.AddWithValue(SPParameter.ProjectName, addContractProject.ProjectName);
                // Mohamed : Issue 49791 : 15/09/2014 : Ends              

                // Mohamed : Issue  : 23/09/2014 : Starts                        			  
                // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
                objCommand.Parameters.AddWithValue(SPParameter.ProjectDivision, addContractProject.ProjectDivision);
                objCommand.Parameters.AddWithValue(SPParameter.ProjectAlias, addContractProject.ProjectAlias);
                objCommand.Parameters.AddWithValue(SPParameter.ProjectBusinessArea, addContractProject.ProjectBussinessArea);
                objCommand.Parameters.AddWithValue(SPParameter.ProjectBusinessSegment, addContractProject.ProjectBussinessSegment);
                // Mohamed : Issue  : 23/09/2014 : Ends

                //Siddharth 13 March 2015 Start
                if (!addContractProject.ProjectModel.Contains("Select"))
                    objCommand.Parameters.AddWithValue(SPParameter.ProjectModel, addContractProject.ProjectModel);
                else
                    objCommand.Parameters.AddWithValue(SPParameter.ProjectModel, DBNull.Value);
                //Siddharth 13 March 2015 End

                //Siddharth 9 Sept 2015 Start
                if (!addContractProject.BusinessVertical.Contains("Select"))
                    objCommand.Parameters.AddWithValue(SPParameter.BusinessVertical, addContractProject.BusinessVertical);
                else
                    objCommand.Parameters.AddWithValue(SPParameter.BusinessVertical, DBNull.Value);
                //Siddharth 9 Sept  2015 End

                int project = objCommand.ExecuteNonQuery();

                return project;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, "Edit", EventIDConstants.RAVE_HR_CONTRACT_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        #endregion Edit

        #region DisassociateProject

        /// <summary>
        /// This function disassociates the project from a contract
        /// </summary>
        /// <param name="project"></param>
        /// <param name="contract"></param>
        public void DisassociateProject(BusinessEntities.ContractProject project, BusinessEntities.Contract contract)
        {
            //creates a DataAccessClass object.
            DataAccessClass ProjectDisassociate = new DataAccessClass();

            try
            {
                //creates a new business entity.
                objContractProject = new BusinessEntities.ContractProject();

                //Opens the connection.
                ProjectDisassociate.OpenConnection(DBConstants.GetDBConnectionString());

                //Declares the parameters for the sp.
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter(SPParameter.ContractCode, DbType.String);
                sqlParam[0].Value = contract.ContractCode;
                sqlParam[1] = new SqlParameter(SPParameter.ProjectName, DbType.String);
                sqlParam[1].Value = project.ProjectName;

                //update changes in the database.
                int disassociateProject = ProjectDisassociate.ExecuteNonQuerySP(SPNames.Contract_DisassociateProject, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, "DisassociateProject", EventIDConstants.RAVE_HR_CONTRACT_DATA_ACCESS_LAYER);
            }
            finally
            {
                ProjectDisassociate.CloseConncetion();
            }
        }

        #endregion DisassociateProject

        #region ProjectDetail
        /// <summary>
        /// This method will fetch records from data base and return to business layer
        /// </summary>
        /// <param name=>BusinessEntities.ContractCriteria</param>
        /// <returns>BusinessEntities.RaveHRCollection</returns>
        public BusinessEntities.RaveHRCollection GetProjectsListDetails(BusinessEntities.ContractCriteria criteria)
        {
            int pageCount = 1;
            int counter = 0;
            List<BusinessEntities.ContractProject> objListOfProjects = null;

            // Initialise Data Access Class object
            DataAccessClass objProjectDetails = new DataAccessClass();

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();
            try
            {
                //Open the connection to DB
                objProjectDetails.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[4];

                sqlParam[0] = new SqlParameter(SPParameter.SortExpresion, DbType.String);
                sqlParam[0].Value = criteria.SortExpression + criteria.Direction;

                sqlParam[1] = new SqlParameter(SPParameter.pageNum, DbType.Int32);
                sqlParam[1].Value = criteria.PageNumber;

                sqlParam[2] = new SqlParameter(SPParameter.pageSize, DbType.Int32);
                sqlParam[2].Value = 10;

                sqlParam[3] = new SqlParameter(SPParameter.pageCount, SqlDbType.Int);
                sqlParam[3].Direction = ParameterDirection.Output;

                //gets the all project details.
                DataSet dsProjectdetails = objProjectDetails.GetDataSet(SPNames.Contract_GetProjectList, sqlParam);

                pageCount = Convert.ToInt32(sqlParam[3].Value);

                //Create entities and add to list
                BusinessEntities.ContractProject objProjects = null;
                objListOfProjects = new List<BusinessEntities.ContractProject>();

                foreach (DataRow dr in dsProjectdetails.Tables[0].Rows)
                {
                    counter++;
                    objProjects = new BusinessEntities.ContractProject();

                    objProjects.ProjectID = Convert.ToInt32(dr[DbTableColumn.Con_ProjectID]);
                    objProjects.ProjectCode = dr[DbTableColumn.Con_ProjectCode].ToString();
                    objProjects.DocumentName = dr[DbTableColumn.Con_DocumentName].ToString();
                    objProjects.ContractCode = dr[DbTableColumn.Con_ContractCode].ToString();
                    objProjects.ProjectName = dr[DbTableColumn.Con_ConProjectName].ToString();
                    objProjects.ContractType = dr[DbTableColumn.Con_ContractName].ToString();
                    objProjects.ClientName = dr[DbTableColumn.ClientName].ToString();
                    //add page count only first time.
                    if (counter == 1)
                        objProjects.PageCount = pageCount;

                    //--add to list
                    objListOfProjects.Add(objProjects);
                }
                raveHRCollection.Add(objListOfProjects);
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, "GetProjectsListDetails", EventIDConstants.RAVE_HR_CONTRACT_DATA_ACCESS_LAYER);
            }
            finally
            {
                objProjectDetails.CloseConncetion();
            }
        }
        #endregion ProjectDetail

        #region isEmpAssociated

        /// <summary>
        /// Checks emp associated.
        /// </summary>
        /// <param name="contractProject"></param>
        /// <returns></returns>
        public bool isEmpAssociated(BusinessEntities.ContractProject contractProject)
        {
            bool result = false;
            DataAccessClass ProjectDetails = new DataAccessClass();
            try
            {
                objContractProject = new BusinessEntities.ContractProject();

                ProjectDetails.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.PROJECTID, DbType.Int32);
                sqlParam[0].Value = contractProject.ProjectID;

                //gets the all  employee details related to project .
                DataSet dsProjectdetails = ProjectDetails.GetDataSet(SPNames.Contract_EmpAssociated, sqlParam);

                if (dsProjectdetails.Tables[0].Rows.Count == 0)
                {
                    result = true;
                }
                return result;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, "isEmpAssociated", EventIDConstants.RAVE_HR_CONTRACT_DATA_ACCESS_LAYER);
            }
            finally
            {
                ProjectDetails.CloseConncetion();
            }

        }

        #endregion isEmpAssociated

        #region checkProjectName

        /// <summary>
        /// Checks the name of project.
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public bool checkProjectName(string projectName)
        {
            bool result = false;
            DataAccessClass ProjectDetails = new DataAccessClass();
            try
            {
                objContractProject = new BusinessEntities.ContractProject();

                ProjectDetails.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.PROJECTNAME, DbType.Int32);
                sqlParam[0].Value = projectName;

                //gets the all  employee details related to project .
                DataSet dsProjectdetails = ProjectDetails.GetDataSet(SPNames.Contract_CheckProject, sqlParam);

                if (dsProjectdetails.Tables[0].Rows.Count == 0)
                {
                    result = true;
                }
                return result;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, "checkProjectName", EventIDConstants.RAVE_HR_CONTRACT_DATA_ACCESS_LAYER);
            }
            finally
            {
                ProjectDetails.CloseConncetion();
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
            DataAccessClass ProjectDetailsDA = new DataAccessClass();
            try
            {
                BusinessEntities.ProjectDetails projectDetails = new BusinessEntities.ProjectDetails();
                SqlDataReader contractDataReader;

                ProjectDetailsDA.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.ProjectName, DbType.String);
                sqlParam[0].Value = projectCode;

                contractDataReader = ProjectDetailsDA.ExecuteReaderSP(SPNames.Contract_CheckProjectName, sqlParam);

                while (contractDataReader.Read())
                {
                    projectDetails.ClientName = contractDataReader.GetValue(2).ToString();
                }
                return projectDetails;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, "getClientNameByProjectCode", EventIDConstants.RAVE_HR_CONTRACT_DATA_ACCESS_LAYER);
            }
            finally
            {
                ProjectDetailsDA.CloseConncetion();
            }
        }



        #endregion Public Member Functions


        public List<BusinessEntities.ContractProject> GetProjectDetailsForFilter(BusinessEntities.ContractProject objGridDetail, ContractCriteria criteria)
        {
            int pageCount = 1;
            
            int counter = 0;
            
            List<BusinessEntities.ContractProject> objListOfProjects = null;

            // Initialise Data Access Class object
            DataAccessClass objProjectDetails = new DataAccessClass();

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();
            try
            {
                //Open the connection to DB
                objProjectDetails.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[7];

                sqlParam[0] = new SqlParameter(SPParameter.SortExpresion, DbType.String);
                sqlParam[0].Value = criteria.SortExpression + criteria.Direction;

                sqlParam[1] = new SqlParameter(SPParameter.pageNum, DbType.Int32);
                sqlParam[1].Value = criteria.PageNumber;

                sqlParam[2] = new SqlParameter(SPParameter.pageSize, DbType.Int32);
                sqlParam[2].Value = 10;

                sqlParam[3] = new SqlParameter(SPParameter.ClientName, SqlDbType.VarChar);
                if (objGridDetail.ClientName == null)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = objGridDetail.ClientName;

                sqlParam[4] = new SqlParameter(SPParameter.ProjectName, SqlDbType.VarChar);
                sqlParam[4].Value = objGridDetail.ProjectName;

                sqlParam[5] = new SqlParameter(SPParameter.StatusId, SqlDbType.Int);
                sqlParam[5].Value = objGridDetail.StatusID;

                sqlParam[6] = new SqlParameter(SPParameter.pageCount, SqlDbType.Int);
                sqlParam[6].Direction = ParameterDirection.Output;

                //gets the all project details.
                DataSet dsProjectdetails = objProjectDetails.GetDataSet(SPNames.CONTRACT_GETPROJECTLISTFORFILER, sqlParam);

                pageCount = Convert.ToInt32(sqlParam[6].Value);

                //Create entities and add to list
                BusinessEntities.ContractProject objProjects = null;
                objListOfProjects = new List<BusinessEntities.ContractProject>();

                foreach (DataRow dr in dsProjectdetails.Tables[0].Rows)
                {
                    counter++;
                    
                    objProjects = new BusinessEntities.ContractProject();

                    objProjects.ProjectCode = dr[DbTableColumn.Con_ProjectCode].ToString();
                    
                    objProjects.DocumentName = dr[DbTableColumn.Con_DocumentName].ToString();
                    
                    objProjects.ContractCode = dr[DbTableColumn.Con_ContractCode].ToString();
                    
                    objProjects.ProjectName = dr[DbTableColumn.Con_ConProjectName].ToString();
                    
                    objProjects.ContractType = dr[DbTableColumn.Con_ContractName].ToString();
                    
                    objProjects.ClientName = dr[DbTableColumn.ClientName].ToString();
                    //add page count only first time.
                    if (counter == 1)
                        
                        objProjects.PageCount = pageCount;

                    //--add to list
                    objListOfProjects.Add(objProjects);
                }

                return objListOfProjects;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, "getClientNameByProjectCode", EventIDConstants.RAVE_HR_CONTRACT_DATA_ACCESS_LAYER);
            }
            finally
            {
                objProjectDetails.CloseConncetion();
            }
        }

        /// <summary>
        /// Checks the name of project.
        /// </summary>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public bool checkProjectCode(string projectCode)
        {
            bool result = false;
            DataAccessClass ProjectDetails = new DataAccessClass();
            try
            {
                objContractProject = new BusinessEntities.ContractProject();

                ProjectDetails.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter(SPParameter.ProjectCode, DbType.String);
                sqlParam[0].Value = projectCode;

                sqlParam[1] = new SqlParameter(SPParameter.COUNT, DbType.Int32);
                sqlParam[1].Direction = ParameterDirection.Output;

                //gets the all  employee details related to project .
                ProjectDetails.ExecuteNonQuerySP(SPNames.Contract_CheckProjectCode, sqlParam);

                int count = Convert.ToInt32(sqlParam[1].Value);

                if (count > 0)
                    result = true;
                else
                    result = false;

                return result;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, "checkProjectName", EventIDConstants.RAVE_HR_CONTRACT_DATA_ACCESS_LAYER);
            }
            finally
            {
                ProjectDetails.CloseConncetion();
            }
        }
    }
}
