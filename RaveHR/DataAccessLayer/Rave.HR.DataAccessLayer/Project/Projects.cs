//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           ProejctSummary.aspx.cs       
//  Author:         gaurav.thakkar
//  Date written:   4/3/2009/ 5:30:30 PM
//  Description:    This class  provides the Data Access layer methods for Project module.
//                  
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  4/3/2009 5:30:30 PM  gaurav.thakkar    n/a     Created    
//  13/8/2009 3:23:40 PM    prashant.mala   n/a    Added transaction to UpdateProject method
//------------------------------------------------------------------------------



using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Transactions;
using System.Data.SqlClient;
using System.Web;
using Common;
using Common.Constants;
using BusinessEntities;

namespace Rave.HR.DataAccessLayer.Projects
{
    public class Projects
    {

        #region Private Fields


        private static SqlDataReader objDataReader;

        private static SqlParameter[] objSqlParameter;

        private static BusinessEntities.Projects objBEProjects;

        private static BusinessEntities.RaveHRCollection raveHRCollection;

        //define SUCCESS
        private const string SUCCESS = "SUCCESS";

        /// <summary>
        /// Data reader object
        /// </summary>
        SqlDataReader objReader = null;

        /// <summary>
        /// Data access class 
        /// </summary>
        DataAccessClass objDAProjects = null;

        #endregion Private Fields

        #region Constant

        /// <summary>
        /// Class Name : Projects
        /// </summary>
        private const string CLASS_NAME_PROJECTS = "Projects";
        private const string GET_PROJECT_MANAGER_BY_PROJECTID = "GetProjectManagerByProjectId";

        /// <summary>
        /// Data Access class which used to open connection and execute.
        /// </summary>
        private static DataAccessClass objDA;

        #endregion Constant

        #region Public Member Functions

        /// <summary>
        /// Updates the selected project
        /// </summary>
        /// <param name="objAddProject"></param>
        /// <returns>string</returns>
        public void UpdateProject(BusinessEntities.Projects objAddProject, string IsUpdated)
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;

            ConnStr = DBConstants.GetDBConnectionString();
            objConnection = new SqlConnection(ConnStr);
            objConnection.Open();
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    //objCommand = new SqlCommand("USP_Projects_UpdateProject", objConnection);
                    objCommand = new SqlCommand(SPNames.Projects_UpdateProject, objConnection);
                    objCommand.CommandType = CommandType.StoredProcedure;

                    objCommand.Parameters.AddWithValue("@ProjectHeadId", objAddProject.ProjectHeadId);

                    objCommand.Parameters.AddWithValue("@ProjectID", objAddProject.ProjectId);
                    objCommand.Parameters.AddWithValue("@StatusID", objAddProject.ProjectStatus);
                    //objCommand.Parameters.AddWithValue("@ProjectCategoryID",objAddProject.strProjectCategoryID);
                    if (objAddProject.StandardHours == "")
                    {
                        objCommand.Parameters.AddWithValue("@StandardHours", DBNull.Value);
                    }
                    else
                    {
                        objCommand.Parameters.AddWithValue("@StandardHours", objAddProject.StandardHours);
                    }

                    //Siddharth 12 March 2015 Start
                    //Add ProjectModel as SP Parameter
                    if (objAddProject.ProjectModel == "")
                    {
                        objCommand.Parameters.AddWithValue("@ProjectModel", DBNull.Value);
                    }
                    else
                    {
                        objCommand.Parameters.AddWithValue("@ProjectModel", objAddProject.ProjectModel);
                    }
                    //Siddharth 12 March 2015 End

                    //Siddharth 3 August 2015 Start
                    //Add BusinessVertical as SP Parameter
                    if (objAddProject.BusinessVertical == "")
                    {
                        objCommand.Parameters.AddWithValue("@BusinessVertical", DBNull.Value);
                    }
                    else
                    {
                        objCommand.Parameters.AddWithValue("@BusinessVertical", objAddProject.BusinessVertical);
                    }
                    //Siddharth 3 August 2015 End
                    
                    objCommand.Parameters.AddWithValue("@Description", objAddProject.Description);
                    objCommand.Parameters.AddWithValue("@EmailID", objAddProject.CreatedBy);
                    objCommand.Parameters.AddWithValue("@IsUpdated", IsUpdated);
                    objCommand.Parameters.AddWithValue("@ProjectGroup", objAddProject.ProjectGroup);
                    objCommand.Parameters.AddWithValue("@EndDate", objAddProject.EndDate);

                    // Mohamed : Issue  : 26/09/2014 : Starts                        			  
                    // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
                    objCommand.Parameters.AddWithValue("@Division", objAddProject.ProjectDivision);
                    objCommand.Parameters.AddWithValue("@ProjectAlias", objAddProject.ProjectAlias);
                    objCommand.Parameters.AddWithValue("@BusinessArea", objAddProject.ProjectBussinessArea);
                    objCommand.Parameters.AddWithValue("@BusinessSegment", objAddProject.ProjectBussinessSegment);
                    // Mohamed : Issue  : 23/09/2014 : Ends
                 
                        objCommand.Parameters.AddWithValue("@OnGoingProjectStatusID", objAddProject.OnGoingProjectStatusID);
                   

                    int updateProject = objCommand.ExecuteNonQuery();
                    //return objCommand.ExecuteNonQuery().ToString();

                    //objCommand = new SqlCommand("USP_Projects_DeleteTechnologyForProject", objConnection);
                    objCommand = new SqlCommand(SPNames.Projects_DeleteTechnologyForProject, objConnection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@ProjectID", objAddProject.ProjectId);

                    int deleteTechnologyID = objCommand.ExecuteNonQuery();

                    foreach (BusinessEntities.Technology techID in objAddProject.Technologies)
                    {
                        //objCommand = new SqlCommand("USP_Projects_AddTechnologiesForProject", objConnection);
                        objCommand = new SqlCommand(SPNames.Projects_AddTechnologiesForProject, objConnection);
                        objCommand.CommandType = CommandType.StoredProcedure;
                        objCommand.Parameters.AddWithValue("@TechnologyID", techID.TechnologyID);
                        objCommand.Parameters.AddWithValue("@ProjectID", objAddProject.ProjectId);
                        int a = objCommand.ExecuteNonQuery();
                    }

                    //objCommand = new SqlCommand("USP_Projects_DeleteSubDomainForProject", objConnection);
                    objCommand = new SqlCommand(SPNames.Projects_DeleteSubDomainForProject, objConnection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@ProjectID", objAddProject.ProjectId);

                    int deleteSubDomainID = objCommand.ExecuteNonQuery();

                    foreach (BusinessEntities.SubDomain subDomainID in objAddProject.LstSubDomain)
                    {
                        //objCommand = new SqlCommand("USP_Projects_AddSubDomainsForProject", objConnection);
                        objCommand = new SqlCommand(SPNames.Projects_AddSubDomainsForProject, objConnection);
                        objCommand.CommandType = CommandType.StoredProcedure;
                        objCommand.Parameters.AddWithValue("@SubDomainID", subDomainID.SubDomainId);
                        objCommand.Parameters.AddWithValue("@ProjectID", objAddProject.ProjectId);
                        int a = objCommand.ExecuteNonQuery();
                    }

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "RaveHRProjects.cs", "UpdateProject", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        /// <summary>
        /// This function is used to retrieve Details of a single project from database.
        /// </summary>
        /// <param name="objViewProject"></param>
        /// <returns>Dataset</returns>        
        public DataSet ViewProjectDetails(BusinessEntities.Projects objViewProject, string SortDir, string SortExpression, string UserMailId, string PresalesRole, string PMRole, string COORole, string RPMRole, string Action, string clientName, int projectStatusId, string projectsummaryprojectname)
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            DataSet ds = null;

            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                
                objCommand = new SqlCommand("USP_Projects_ViewProject_New", objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@ProjectID", objViewProject.ProjectId);
                objCommand.Parameters.AddWithValue("@SortDirection", SortDir);
                if (SortExpression == "ProjectStatus")
                {
                    SortExpression = "StatusID";
                }
                objCommand.Parameters.AddWithValue("@SortExpression", SortExpression);

                if ((UserMailId == "") || (UserMailId == null))
                {
                    objCommand.Parameters.AddWithValue("@UserMailId", DBNull.Value);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@UserMailId", UserMailId);
                }

                if ((PresalesRole == "") || (PresalesRole == null))
                {
                    objCommand.Parameters.AddWithValue("@PresalesRole", DBNull.Value);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@PresalesRole", PresalesRole);
                }

                if ((PMRole == "") || (PMRole == null))
                {
                    objCommand.Parameters.AddWithValue("@PMRole", DBNull.Value);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@PMRole", PMRole);
                }

                if ((COORole == "") || (COORole == null))
                {
                    objCommand.Parameters.AddWithValue("@COORole", DBNull.Value);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@COORole", COORole);
                }

                if ((RPMRole == "") || (RPMRole == null))
                {
                    objCommand.Parameters.AddWithValue("@RPMRole", DBNull.Value);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@RPMRole", RPMRole);
                }

                if ((Action == "") || (Action == null))
                {
                    objCommand.Parameters.AddWithValue("@Mode", DBNull.Value);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@Mode", Action);
                }

                if ((clientName == "") || (clientName == null))
                {
                    objCommand.Parameters.AddWithValue("@ClientName", DBNull.Value);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@ClientName", clientName);
                }

                if (projectStatusId == 0)
                {
                    objCommand.Parameters.AddWithValue("@MasterId", DBNull.Value);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@MasterId", projectStatusId);
                }

                if ((projectsummaryprojectname == "") || (projectsummaryprojectname == null))
                {
                    objCommand.Parameters.AddWithValue("@ProjectName", DBNull.Value);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@ProjectName", projectsummaryprojectname);
                }

                ds = new DataSet();
                SqlDataAdapter objDataAdapter = new SqlDataAdapter(objCommand);

                objDataAdapter.Fill(ds);
                return ds;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "RaveHRProjects.cs", "ViewProjectDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserMailId"></param>
        /// <param name="COORole"></param>
        /// <param name="PresalesRole"></param>
        /// <param name="PMRole"></param>
        /// <param name="RPMRole"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpressionAndDirection"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>        
        public List<BusinessEntities.Projects> GetProjectSummaryForPageLoad(BusinessEntities.ProjectCriteria objProjectCriteria, int pageSize, ref int pageCount,bool setPageing)
        {
            List<BusinessEntities.Projects> objProjectSummary = new List<BusinessEntities.Projects>();
            try
            {
                BusinessEntities.Projects objRaveHR = null;
                DataAccessClass objDAUnfilteredProjectSummary = new DataAccessClass();
                objDAUnfilteredProjectSummary.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[10];
                sqlParam[0] = new SqlParameter("@UserMailId", SqlDbType.VarChar, 50);
                if (objProjectCriteria.UserMailId == "" || objProjectCriteria.UserMailId == null)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objProjectCriteria.UserMailId;

                sqlParam[1] = new SqlParameter("@COORole", SqlDbType.VarChar, 20);
                if (objProjectCriteria.RoleCOO == "" || objProjectCriteria.RoleCOO == null)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objProjectCriteria.RoleCOO;

                sqlParam[2] = new SqlParameter("@PresalesRole", SqlDbType.VarChar, 20);
                if (objProjectCriteria.RolePresales == "" || objProjectCriteria.RolePresales == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objProjectCriteria.RolePresales;

                sqlParam[3] = new SqlParameter("@PMRole", SqlDbType.VarChar, 20);
                if (objProjectCriteria.RolePM == "" || objProjectCriteria.RolePM == null)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = objProjectCriteria.RolePM;

                sqlParam[4] = new SqlParameter("@RPMRole", SqlDbType.VarChar, 20);
                if (objProjectCriteria.RoleRPM == "" || objProjectCriteria.RoleRPM == null)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = objProjectCriteria.RoleRPM;

                sqlParam[5] = new SqlParameter("@pageNum", SqlDbType.Int);
                if (objProjectCriteria.PageNumber == 0)
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = objProjectCriteria.PageNumber;

                sqlParam[6] = new SqlParameter("@pageSize", SqlDbType.Int);
                if (pageSize == 0)
                    sqlParam[6].Value = DBNull.Value;
                else
                    sqlParam[6].Value = pageSize;

                sqlParam[7] = new SqlParameter("@SortExpression", SqlDbType.VarChar, 50);
                if (objProjectCriteria.SortExpressionAndDirection == "" || objProjectCriteria.SortExpressionAndDirection == null)
                    sqlParam[7].Value = DBNull.Value;
                else
                    sqlParam[7].Value = objProjectCriteria.SortExpressionAndDirection;

                sqlParam[8] = new SqlParameter("@setPaging", SqlDbType.Bit);
                if (setPageing == true)
                    sqlParam[8].Value = 1;
                else
                    sqlParam[8].Value = 0;

                sqlParam[9] = new SqlParameter("@pageCount", SqlDbType.Int);
                sqlParam[9].Direction = ParameterDirection.Output;


                DataSet dsUnfilteredProjectSummary = objDAUnfilteredProjectSummary.GetDataSet(SPNames.Projects_GetUnfilteredProjectSummaryData, sqlParam);
                pageCount = Convert.ToInt32(sqlParam[9].Value);

                foreach (DataRow drProjectSummary in dsUnfilteredProjectSummary.Tables[0].Rows)
                {
                    objRaveHR = new BusinessEntities.Projects();
                    objRaveHR.ProjectId = int.Parse(drProjectSummary["ProjectID"].ToString());
                    objRaveHR.ProjectCode = drProjectSummary["ProjectCode"].ToString();
                    objRaveHR.ProjectName = drProjectSummary["ProjectName"].ToString();
                    objRaveHR.ClientName = drProjectSummary["ClientName"].ToString();
                    objRaveHR.ProjectStatus = drProjectSummary["ProjectStatus"].ToString();
                    objRaveHR.TotalMRF = int.Parse(drProjectSummary["TotalMRF"].ToString());
                    objRaveHR.OpenMRF = int.Parse(drProjectSummary["OpenMRF"].ToString());

                    //Siddharth 12 March 2015 Start
                    objRaveHR.ProjectModel = drProjectSummary["ProjectModel"].ToString();
                    //Siddharth 12 March 2015 End

                    objProjectSummary.Add(objRaveHR);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "RaveHRProjects.cs", "GetProjectSummaryForPageLoad", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            return objProjectSummary;
        }

        /// <summary>
        /// Gets the unfiltered Project Summary details.
        /// </summary>
        /// <param name="UserMailId">String value contains logged in users email id</param>
        /// <param name="COORole">Contains string value if logged in user is COO</param>
        /// <param name="PresalesRole">Contains string value if logged in user is Presales</param>
        /// <param name="PMRole">Contains string value if logged in user is PM</param>
        /// <param name="RPMRole">Contains string value if logged in user is RPM</param>
        /// <returns>List of unfiltered project details</returns>
        /// 
        public DataSet GetProjectSummaryForPageLoad(string UserMailId, string COORole, string PresalesRole, string PMRole, string RPMRole)
        {
            SqlConnection objConnection = null;
            DataSet ds = null;
            SqlCommand objCommand = null;
            try
            {
                string strConnection = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(strConnection);
                objConnection.Open();

                ds = new DataSet();
                objCommand = new SqlCommand(SPNames.Projects_GetUnfilteredProjectSummaryDataForAddProject, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;

                objCommand.Parameters.AddWithValue("@UserMailId", UserMailId);

                if (COORole != "" || COORole != null)
                {
                    objCommand.Parameters.AddWithValue("@COORole", COORole);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@COORole", DBNull.Value);
                }

                if (PresalesRole != "" || PresalesRole != null)
                {
                    objCommand.Parameters.AddWithValue("@PresalesRole", PresalesRole);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@PresalesRole", DBNull.Value);
                }

                if (PMRole != "" || PMRole != null)
                {
                    objCommand.Parameters.AddWithValue("@PMRole", PMRole);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@PMRole", DBNull.Value);
                }

                if (RPMRole != "" || RPMRole != null)
                {
                    objCommand.Parameters.AddWithValue("@RPMRole", RPMRole);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@RPMRole", DBNull.Value);
                }

                SqlDataAdapter objDataAdapter = new SqlDataAdapter(objCommand);
                objDataAdapter.Fill(ds);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "RaveHRProjects.cs", "GetProjectSummaryForPageLoad", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            return ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objGetProjectSummary"></param>
        /// <param name="objGetProjectStatus"></param>
        /// <param name="UserMailId"></param>
        /// <param name="COORole"></param>
        /// <param name="PresalesRole"></param>
        /// <param name="PMRole"></param>
        /// <param name="RPMRole"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortExpressionAndDirection"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        //public List<BusinessEntities.Projects> GetProjectSummaryForFilter(BusinessEntities.Projects objGetProjectSummary, BusinessEntities.RaveHRMaster objGetProjectStatus, string UserMailId, string COORole, string PresalesRole, string PMRole, string RPMRole, int pageNumber, int pageSize, string sortExpressionAndDirection, ref int pageCount)
        public List<BusinessEntities.Projects> GetProjectSummaryForFilter(BusinessEntities.Projects objGetProjectSummary, BusinessEntities.Master objGetProjectStatus, BusinessEntities.ProjectCriteria objProjectCriteria, int pageSize, ref int pageCount, bool setPageing)
        {
            List<BusinessEntities.Projects> objProjectSummary = new List<BusinessEntities.Projects>();
            DataAccessClass objDAFilteredProjectSummary = new DataAccessClass();
            try
            {
                BusinessEntities.Projects objRaveHR = null;

                objDAFilteredProjectSummary.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[13];
                sqlParam[0] = new SqlParameter("@ProjectName", SqlDbType.VarChar, 50);
                if (objGetProjectSummary.ProjectName == "" || objGetProjectSummary.ProjectName == null)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objGetProjectSummary.ProjectName;

                sqlParam[1] = new SqlParameter("@ClientName", SqlDbType.VarChar, 50);
                if (objGetProjectSummary.ClientName == "" || objGetProjectSummary.ClientName == null)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objGetProjectSummary.ClientName;

                sqlParam[2] = new SqlParameter("@MasterID", SqlDbType.Int);
                if (objGetProjectStatus.MasterId == 0)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objGetProjectStatus.MasterId;

                sqlParam[3] = new SqlParameter("@UserMailId", SqlDbType.VarChar, 50);
                if (objProjectCriteria.UserMailId == "" || objProjectCriteria.UserMailId == null)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = objProjectCriteria.UserMailId;

                sqlParam[4] = new SqlParameter("@COORole", SqlDbType.VarChar, 20);
                if (objProjectCriteria.RoleCOO == "" || objProjectCriteria.RoleCOO == null)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = objProjectCriteria.RoleCOO;

                sqlParam[5] = new SqlParameter("@PresalesRole", SqlDbType.VarChar, 20);
                if (objProjectCriteria.RolePresales == "" || objProjectCriteria.RolePresales == null)
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = objProjectCriteria.RolePresales;

                sqlParam[6] = new SqlParameter("@PMRole", SqlDbType.VarChar, 20);
                if (objProjectCriteria.RolePM == "" || objProjectCriteria.RolePM == null)
                    sqlParam[6].Value = DBNull.Value;
                else
                    sqlParam[6].Value = objProjectCriteria.RolePM;

                sqlParam[7] = new SqlParameter("@RPMRole", SqlDbType.VarChar, 20);
                if (objProjectCriteria.RoleRPM == "" || objProjectCriteria.RoleRPM == null)
                    sqlParam[7].Value = DBNull.Value;
                else
                    sqlParam[7].Value = objProjectCriteria.RoleRPM;

                sqlParam[8] = new SqlParameter("@pageNum", SqlDbType.Int);
                if (objProjectCriteria.PageNumber == 0)
                    sqlParam[8].Value = DBNull.Value;
                else
                    sqlParam[8].Value = objProjectCriteria.PageNumber;

                sqlParam[9] = new SqlParameter("@pageSize", SqlDbType.Int);
                if (pageSize == 0)
                    sqlParam[9].Value = DBNull.Value;
                else
                    sqlParam[9].Value = pageSize;

                sqlParam[10] = new SqlParameter("@SortExpression", SqlDbType.VarChar, 50);
                if (objProjectCriteria.SortExpressionAndDirection == "" || objProjectCriteria.SortExpressionAndDirection == null)
                    sqlParam[10].Value = DBNull.Value;
                else
                    sqlParam[10].Value = objProjectCriteria.SortExpressionAndDirection;


                sqlParam[11] = new SqlParameter("@setPageing", SqlDbType.Bit);
                if (setPageing == true)
                    sqlParam[11].Value = 1;
                else
                    sqlParam[11].Value = 0;

                sqlParam[12] = new SqlParameter("@pageCount", SqlDbType.Int);
                sqlParam[12].Direction = ParameterDirection.Output;


                DataSet dsFilteredProjectSummary = objDAFilteredProjectSummary.GetDataSet(SPNames.Projects_GetFilteredProjectSummaryData, sqlParam);
                pageCount = Convert.ToInt32(sqlParam[12].Value);


                foreach (DataRow drProjectSummary in dsFilteredProjectSummary.Tables[0].Rows)
                {
                    objRaveHR = new BusinessEntities.Projects();
                    objRaveHR.ProjectId = int.Parse(drProjectSummary["ProjectID"].ToString());
                    objRaveHR.ProjectCode = drProjectSummary["ProjectCode"].ToString();
                    objRaveHR.ProjectName = drProjectSummary["ProjectName"].ToString();
                    objRaveHR.ClientName = drProjectSummary["ClientName"].ToString();
                    objRaveHR.ProjectStatus = drProjectSummary["ProjectStatus"].ToString();
                    objRaveHR.TotalMRF = int.Parse(drProjectSummary["TotalMRF"].ToString());
                    objRaveHR.OpenMRF = int.Parse(drProjectSummary["OpenMRF"].ToString());

                    //Siddharth 12 March 2015 Start
                    objRaveHR.ProjectModel = drProjectSummary["ProjectModel"].ToString();
                    //Siddharth 12 March 2015 End

                    objProjectSummary.Add(objRaveHR);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "RaveHRProjects.cs", "GetProjectSummaryForFilter", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDAFilteredProjectSummary.CloseConncetion();
            }
            return objProjectSummary;
        }

        /// <summary>
        /// Gets the filtered Project Summary details.
        /// </summary>
        /// <param name="UserMailId">String value contains logged in users email id</param>
        /// <param name="COORole">Contains string value if logged in user is COO</param>
        /// <param name="PresalesRole">Contains string value if logged in user is Presales</param>
        /// <param name="PMRole">Contains string value if logged in user is PM</param>
        /// <param name="RPMRole">Contains string value if logged in user is RPM</param>
        /// <returns>List of filtered project details</returns>
        /// 
        public DataSet GetProjectSummaryForFilter(BusinessEntities.Projects objGetProjectSummary, BusinessEntities.Master objGetProjectStatus, string UserMailId, string COORole, string PresalesRole, string PMRole, string RPMRole)
        {
            SqlConnection objConnection = null;
            DataSet ds = null;
            SqlCommand objCommand = null;
            try
            {
                string strConnection = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(strConnection);
                objConnection.Open();

                ds = new DataSet();

                objCommand = new SqlCommand(SPNames.Projects_GetFilteredProjectSummaryDataForAddProject, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;

                if (objGetProjectSummary.ProjectName == "" || objGetProjectSummary.ProjectName == null)
                {
                    objCommand.Parameters.AddWithValue("@ProjectName", DBNull.Value);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@ProjectName", objGetProjectSummary.ProjectName);
                }
                if (objGetProjectSummary.ClientName == "" || objGetProjectSummary.ClientName == null)
                {
                    objCommand.Parameters.AddWithValue("@ClientName", DBNull.Value);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@ClientName", objGetProjectSummary.ClientName);
                }
                if (objGetProjectStatus.MasterId == 0)
                {
                    objCommand.Parameters.AddWithValue("@MasterID", DBNull.Value);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@MasterID", objGetProjectStatus.MasterId);
                }               

                objCommand.Parameters.AddWithValue("@UserMailId", UserMailId);

                if (COORole != "" || COORole != null)
                {
                    objCommand.Parameters.AddWithValue("@COORole", COORole);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@COORole", DBNull.Value);
                }

                if (PresalesRole != "" || PresalesRole != null)
                {
                    objCommand.Parameters.AddWithValue("@PresalesRole", PresalesRole);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@PresalesRole", DBNull.Value);
                }

                if (PMRole != "" || PMRole != null)
                {
                    objCommand.Parameters.AddWithValue("@PMRole", PMRole);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@PMRole", DBNull.Value);
                }

                if (RPMRole != "" || RPMRole != null)
                {
                    objCommand.Parameters.AddWithValue("@RPMRole", RPMRole);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@RPMRole", DBNull.Value);
                }

                SqlDataAdapter objDataAdapter = new SqlDataAdapter(objCommand);
                objDataAdapter.Fill(ds);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "RaveHRProjects.cs", "GetProjectSummaryForFilter", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            return ds;
        }

        /// <summary>
        /// Gets the project name 
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet GetProjectNames()
        {
            SqlConnection objConnection = null;
            DataSet ds = null;
            try
            {
                //Connection string               
                string strConnection = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(strConnection);
                objConnection.Open();

                ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(SPNames.Projects_ProjectName, objConnection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(ds);

                return ds;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "RaveHRProjects.cs", "getProjectName", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        /// <summary>
        /// Get pending approval and rejected projects
        /// </summary>
        public DataSet GetPendingApprovalAndRejectedProjects()
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            DataSet ds = null;

            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                //objCommand = new SqlCommand("USP_Projects_ListOfProjects", objConnection);
                objCommand = new SqlCommand(SPNames.Projects_UnfilteredListOfProjectsPendingForapproval, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;

                ds = new DataSet();
                SqlDataAdapter objDataAdapter = new SqlDataAdapter(objCommand);

                objDataAdapter.Fill(ds);
                return ds;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "RaveHRProjects.cs", "GetPendingApprovalAndRejectedProjects", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        /// <summary>
        /// Get Client Name
        /// </summary>
        /// <returns>DataSet</returns>
        public List<BusinessEntities.Projects> GetClientName(BusinessEntities.ProjectCriteria objProjectCriteria)
        {
            SqlConnection objConnection = null;
            DataSet ds = null;
            SqlCommand objCommand = null;
            BusinessEntities.Projects objClientName = new BusinessEntities.Projects();
            List<BusinessEntities.Projects> lstClientName = new List<BusinessEntities.Projects>();

            try
            {
                //Connection string               
                string strConnection = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(strConnection);
                objConnection.Open();

                ds = new DataSet();
                objCommand = new SqlCommand(SPNames.Projects_GetClientName, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;

                objCommand.Parameters.AddWithValue("@UserMailId", objProjectCriteria.UserMailId);

                if (objProjectCriteria.RoleCOO != "")
                {
                    objCommand.Parameters.AddWithValue("@COORole", objProjectCriteria.RoleCOO);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@COORole", DBNull.Value);
                }

                if (objProjectCriteria.RolePresales != "")
                {
                    objCommand.Parameters.AddWithValue("@PresalesRole", objProjectCriteria.RolePresales);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@PresalesRole", DBNull.Value);
                }

                if (objProjectCriteria.RolePM != "")
                {
                    objCommand.Parameters.AddWithValue("@PMRole", objProjectCriteria.RolePM);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@PMRole", DBNull.Value);
                }

                if (objProjectCriteria.RoleRPM != "")
                {
                    objCommand.Parameters.AddWithValue("@RPMRole", objProjectCriteria.RoleRPM);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@RPMRole", DBNull.Value);
                }


                SqlDataAdapter objDataAdapter = new SqlDataAdapter(objCommand);
                objDataAdapter.Fill(ds);

                if (ds.Tables.Count != 0)
                {
                    foreach (DataRow drClientName in ds.Tables[0].Rows)
                    {
                        objClientName = new BusinessEntities.Projects();
                        objClientName.ClientName= drClientName["ClientName"].ToString();
                        objClientName.ClientId = drClientName["ClientId"].ToString();
                        lstClientName.Add(objClientName);
                    }
                }

                return lstClientName;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "RaveHRProjects.cs", "GetClientName", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        // 27631-Subhra-Start
        // Added following method to select client data as per status
        /// <summary>
        /// 
        /// </summary>        
        public List<BusinessEntities.Projects> GetClientNameAsPerStatus(BusinessEntities.ProjectCriteria objProjectCriteria)
        {
            SqlConnection objConnection = null;
            DataSet ds = null;
            SqlCommand objCommand = null;
            BusinessEntities.Projects objClientName = new BusinessEntities.Projects();
            List<BusinessEntities.Projects> ClientName = new List<BusinessEntities.Projects>();
            try
            {
                 //Connection string               
                string strConnection = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(strConnection);
                objConnection.Open();

                ds = new DataSet();
                objCommand = new SqlCommand(SPNames.Projects_GetClosedClientName, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;

                objCommand.Parameters.AddWithValue("@UserMailId", objProjectCriteria.UserMailId);

                if (objProjectCriteria.RoleCOO != "")
                {
                    objCommand.Parameters.AddWithValue("@COORole", objProjectCriteria.RoleCOO);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@COORole", DBNull.Value);
                }

                if (objProjectCriteria.RolePresales != "")
                {
                    objCommand.Parameters.AddWithValue("@PresalesRole", objProjectCriteria.RolePresales);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@PresalesRole", DBNull.Value);
                }

                if (objProjectCriteria.RolePM != "")
                {
                    objCommand.Parameters.AddWithValue("@PMRole", objProjectCriteria.RolePM);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@PMRole", DBNull.Value);
                }

                if (objProjectCriteria.RoleRPM != "")
                {
                    objCommand.Parameters.AddWithValue("@RPMRole", objProjectCriteria.RoleRPM);
                }
                else
                {
                    objCommand.Parameters.AddWithValue("@RPMRole", DBNull.Value);
                }

                SqlDataAdapter objDataAdapter = new SqlDataAdapter(objCommand);
                objDataAdapter.Fill(ds);

                if (ds.Tables.Count != 0)
                {
                    foreach (DataRow drClientName in ds.Tables[0].Rows)
                    {
                        objClientName = new BusinessEntities.Projects();
                        objClientName.ClientName= drClientName["ClientName"].ToString();
                        objClientName.ClientId = drClientName["ClientId"].ToString();
                        ClientName.Add(objClientName);
                    }
                }

                return ClientName;

            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "RaveHRProjects.cs", "GetClientNameAsPerStatus", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }
        // 27631-Subhra-End
        

        /// <summary>
        /// method to get TechnologyCategory
        /// </summary>
        public DataTable TechnologyCategory()
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            SqlDataReader objReader = null;
            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                //objCommand = new SqlCommand("USP_Projects_TechnologyCategory", objConnection);
                objCommand = new SqlCommand(SPNames.Projects_TechnologyCategory, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;

                objReader = objCommand.ExecuteReader();
                DataTable objDataTable = new DataTable();
                objDataTable.Load(objReader);
                return objDataTable;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "RaveHRProjects.cs", "TechnologyCategory", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        /// <summary>
        /// method to get Technology
        /// </summary>
        public DataTable Technology(int CategoryID)
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            SqlDataReader objReader = null;
            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                //objCommand = new SqlCommand("USP_Projects_Technology", objConnection);
                objCommand = new SqlCommand(SPNames.Projects_Technology, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.Add("@CategoryID", CategoryID);
                objReader = objCommand.ExecuteReader();
                DataTable objDataTable = new DataTable();
                objDataTable.Load(objReader);
                return objDataTable;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "RaveHRProjects.cs", "Technology", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        ///THIS METHOD IS NO LONGER IN USE WILL GET REMOVED BEFORE CODE REVIEW
        /// <summary>
        /// Gets technology all the technology
        /// </summary>
        /// <returns></returns>
        public DataTable GetTechnology()
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            SqlDataReader objReader = null;
            DataTable objDataTable;

            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                //objCommand = new SqlCommand("USP_Projects_GetTechnology", objConnection);
                objCommand = new SqlCommand(SPNames.Projects_GetTechnology, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;

                objReader = objCommand.ExecuteReader();
                objDataTable = new DataTable();
                objDataTable.Load(objReader);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "RaveHRProjects.cs", "GetTechnology", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            return objDataTable;
        }

        ///THIS METHOD IS NO LONGER IN USE WILL GET REMOVED BEFORE CODE REVIEW
        /// <summary>
        /// Gets all the categories
        /// </summary>
        /// <returns></returns>
        public DataTable GetCategory()
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            SqlDataReader objReader = null;
            DataTable objDataTable;

            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.Projects_GetCategroy, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;

                objReader = objCommand.ExecuteReader();
                objDataTable = new DataTable();
                objDataTable.Load(objReader);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "RaveHRProjects.cs", "GetCategory", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            return objDataTable;
        }

        /// <summary>
        /// Get Domain Name
        /// </summary>
        /// <returns>DataSet</returns>
        public DataTable GetDomainName()
        {

            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            SqlDataReader objReader = null;
            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.Projects_GetDomainName, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;

                objReader = objCommand.ExecuteReader();
                DataTable objDataTable = new DataTable();
                objDataTable.Load(objReader);
                return objDataTable;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "RaveHRProjects.cs", "GetDomainName", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        /// <summary>
        /// Gets the Sub Domain based upon selected Domain 
        /// </summary>
        /// <param name="domainId">int</param>
        /// <returns>DataTable</returns>
        public DataTable GetSubDomain(int domainId)
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            SqlDataReader objReader = null;
            DataTable objDataTable = null;

            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.Projects_GetSubDomain, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.Add("@domainId", domainId);
                objReader = objCommand.ExecuteReader();
                objDataTable = new DataTable();
                objDataTable.Load(objReader);

            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "RaveHRProjects.cs", "GetSubDomain", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            return objDataTable;
        }

        /// <summary>
        /// This function is used to retrieve specific details of a project.
        /// </summary>
        /// <param name="objRetrieveProject"></param>
        /// <returns>Dataset</returns>
        public DataSet RetrieveProjectDetails(int ProjectId)
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            DataSet ds = null;

            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objCommand = new SqlCommand(SPNames.Projects_RetrieveProjDet, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@ProjectID", ProjectId);

                ds = new DataSet();
                SqlDataAdapter objDataAdapter = new SqlDataAdapter(objCommand);

                objDataAdapter.Fill(ds);
                return ds;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "RaveHRProjects.cs", "RetrieveProjectDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }       

        /// <summary>
        /// get to SearchResult for Project
        /// </summary>
        public DataSet GetProjectSearchResult(string strKeyword)
        {
            DataAccessClass objDAProjectSearchResult = new DataAccessClass();
            objDAProjectSearchResult.OpenConnection(DBConstants.GetDBConnectionString());
            try
            {
               
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@Keyword", DbType.String);
                sqlParam[0].Value = strKeyword;
                DataSet dsProjectSearchResult = objDAProjectSearchResult.GetDataSet(SPNames.Projects_GetProjectSearchResult, sqlParam);
                return dsProjectSearchResult;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "RaveHRProjects.cs", "GetProjectSearchResult", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAProjectSearchResult.CloseConncetion();

            }
        }

        /// <summary>
        /// Method To Get Project Edited Details
        /// </summary>
        public BusinessEntities.Projects GetEditedProjectDetails(BusinessEntities.Projects objProjectDetails)
        {

            // Initialise Data Access Class object
            DataAccessClass objDAGetProjects = new DataAccessClass();

            //Read the data and assign to Collection object
            BusinessEntities.Projects objBEProjects = new BusinessEntities.Projects();

            try
            {
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.PROJECTID, DbType.Int32);
                sqlParam[0].Value = objProjectDetails.ProjectId;

                //Open the connection to DB
                objDAGetProjects.OpenConnection(DBConstants.GetDBConnectionString());

                //Execute the SP
                objDataReader = objDAGetProjects.ExecuteReaderSP(SPNames.Projects_GetProjectEditedDetails, sqlParam);
                
                //call read method.
                while (objDataReader.Read())
                {

                    if (!string.IsNullOrEmpty(objDataReader[DbTableColumn.PrevEndDate].ToString()))
                    {
                        objBEProjects.PrevEndDate = DateTime.Parse(objDataReader[DbTableColumn.PrevEndDate].ToString());
                    }

                    if (!string.IsNullOrEmpty(objDataReader[DbTableColumn.EndDate].ToString()))
                    {
                        objBEProjects.EndDate = DateTime.Parse(objDataReader[DbTableColumn.EndDate].ToString());
                    }

                    if (!string.IsNullOrEmpty(objDataReader[DbTableColumn.PrevStanderdHours].ToString()))
                    {
                        objBEProjects.PrevStandardHours = Convert.ToDecimal(objDataReader[DbTableColumn.PrevStanderdHours].ToString());
                    }                    

                    objBEProjects.StandardHours = objDataReader[DbTableColumn.StanderdHours].ToString();

                    objBEProjects.PrevProjectGroup = objDataReader[DbTableColumn.PrevProjectGroup].ToString();
                    objBEProjects.ProjectGroup = objDataReader[DbTableColumn.ProjectGroup].ToString();


                    objBEProjects.PrevProjectStatus = objDataReader[DbTableColumn.PrevProjectStatus].ToString();
                    objBEProjects.ProjectStatus = objDataReader[DbTableColumn.ProjectStatus].ToString();

                    objBEProjects.PrevProjectExecutiveSummary = objDataReader[DbTableColumn.PrevProjectExecutiveSummary].ToString();
                    objBEProjects.ProjectExecutiveSummary = objDataReader[DbTableColumn.ProjectExecutiveSummary].ToString();
                
                }

                // Return the Collection
                return objBEProjects;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_PROJECTS, "GetEditedProjectDetails", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDAGetProjects.CloseConncetion();
            }
        }

        /// <summary>
        /// GetProjectManagerByProjectId
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>

        public static RaveHRCollection GetProjectManagerByProjectId(BusinessEntities.Projects objProject)
        {

            try
            {
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.ProjectId, objProject.ProjectId);

                //--Declare variable.
                BusinessEntities.Projects objBEGetProjectManagerByProjectId = null;
                RaveHRCollection objListGetProjectManagerByProjectId = new RaveHRCollection();

                //--Get data for Project 
                DataSet dsGetProjectManagerByProjectId = objDA.GetDataSet(SPNames.ResourcePlan_GetProjectManagerByProjectId, sqlParam);

                //create temporary datatable.
                DataTable tempDataTable = ((DataTable)(dsGetProjectManagerByProjectId.Tables[0])).Clone();

                //create new dataset.
                DataSet dsGetPMByProjectId = new DataSet();

                //declare datrow object.
                DataRow dataRow = null;

                foreach (DataRow row in dsGetProjectManagerByProjectId.Tables[0].Rows)
                {
                    //checks if Project Id already in dataset.
                    if (tempDataTable.Rows.Count == 0 || (!row[DbTableColumn.ProjectId].ToString().Equals(dataRow[DbTableColumn.ProjectId].ToString())))
                    {
                        dataRow = tempDataTable.NewRow();
                        dataRow[DbTableColumn.ProjectId] = row[DbTableColumn.ProjectId];
                        dataRow[DbTableColumn.ProjectManager] = row[DbTableColumn.ProjectManager];
                        dataRow[DbTableColumn.EmailId] = row[DbTableColumn.EmailId];
                        tempDataTable.Rows.Add(dataRow);
                    }
                    else
                    {
                        //comma separated values of different project managers for same project.
                        if (!row[DbTableColumn.ProjectManager].ToString().Equals(dataRow[DbTableColumn.ProjectManager].ToString()))
                        {
                            dataRow[DbTableColumn.ProjectManager] = dataRow[DbTableColumn.ProjectManager] + "," + row[DbTableColumn.ProjectManager];
                        }
                        if (!row[DbTableColumn.EmailId].ToString().Equals(dataRow[DbTableColumn.EmailId].ToString()))
                        {
                            dataRow[DbTableColumn.EmailId] = dataRow[DbTableColumn.EmailId] + "," + row[DbTableColumn.EmailId];
                        }
                    }

                }

                //// Fills the datatset with Project details. 
                dsGetPMByProjectId.Tables.Add(tempDataTable);

                //loops through dataset
                foreach (DataRow dr in dsGetPMByProjectId.Tables[0].Rows)
                {
                    objBEGetProjectManagerByProjectId = new BusinessEntities.Projects();
                    

                    //assign ProjectId 
                    objBEGetProjectManagerByProjectId.ProjectId = int.Parse(dr[DbTableColumn.ProjectId].ToString());

                    //assign CreatedByFullName 
                    objBEGetProjectManagerByProjectId.CreatedByFullName = dr[DbTableColumn.ProjectManager].ToString();
                    objBEGetProjectManagerByProjectId.EmailIdOfPM = dr[DbTableColumn.EmailId].ToString();

                    //add to collection object
                    objListGetProjectManagerByProjectId.Add(objBEGetProjectManagerByProjectId);
                }

                //--return the Collection

                return objListGetProjectManagerByProjectId;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_PROJECTS, GET_PROJECT_MANAGER_BY_PROJECTID, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();

            }

        }

        /// <summary>
        /// Add Category Technology
        /// </summary>
        public void AddCategoryTechnology(Category category, Technology technology, ref int CategoryId, ref int TechNologyId)
        {
            try
            {
                objDAProjects = new DataAccessClass();
                objDAProjects.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter(SPParameter.CategoryID, DbType.Int32);
                sqlParam[0].Value = category.CategoryId;

                sqlParam[1] = new SqlParameter(SPParameter.Category, DbType.String);
                sqlParam[1].Value = category.CategoryName;

                sqlParam[2] = new SqlParameter(SPParameter.TechnologyID, DbType.Int32);
                sqlParam[2].Value = technology.TechnologyID;

                sqlParam[3] = new SqlParameter(SPParameter.Technology, DbType.String);
                sqlParam[3].Value = technology.TechnolgoyName;

                sqlParam[4] = new SqlParameter(SPParameter.NewCategoryID, DbType.Int32);
                sqlParam[4].Direction = ParameterDirection.Output;

                sqlParam[5] = new SqlParameter(SPParameter.NewTechnologyID, DbType.Int32);
                sqlParam[5].Direction = ParameterDirection.Output;


                objDAProjects.ExecuteNonQuerySP(SPNames.Projects_AddProjectsCategoryAndTechnology, sqlParam);

                //--Get Category  id
                if (!string.IsNullOrEmpty(sqlParam[4].Value.ToString()))
                CategoryId = Convert.ToInt32(sqlParam[4].Value);

                //--Get Technology id
                if (!string.IsNullOrEmpty(sqlParam[5].Value.ToString()))
                TechNologyId = Convert.ToInt32(sqlParam[5].Value);

            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_PROJECTS, "AddCategoryTechnology", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDAProjects.CloseConncetion();
            }
        }

        /// <summary>
        /// Add Domain And SubDomain
        /// </summary>
        public void AddDomainAndSubDomain(Domain objDomain, SubDomain objSubDomain, ref int DomainId, ref int SubDomainId)
        {
            try
            {
                objDAProjects = new DataAccessClass();
                objDAProjects.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter(SPParameter.DomainID, DbType.Int32);
                sqlParam[0].Value = objDomain.DomainId;

                sqlParam[1] = new SqlParameter(SPParameter.Domain, DbType.String);
                sqlParam[1].Value = objDomain.DomainName;

                sqlParam[2] = new SqlParameter(SPParameter.SubdomainID, DbType.Int32);
                sqlParam[2].Value = objSubDomain.SubDomainId;

                sqlParam[3] = new SqlParameter(SPParameter.Subdomain, DbType.String);
                sqlParam[3].Value = objSubDomain.SubDomainName;

                sqlParam[4] = new SqlParameter(SPParameter.NewDomainID, DbType.Int32);
                sqlParam[4].Direction = ParameterDirection.Output;

                sqlParam[5] = new SqlParameter(SPParameter.NewSubdomainID, DbType.Int32);
                sqlParam[5].Direction = ParameterDirection.Output;

                objDAProjects.ExecuteNonQuerySP(SPNames.Projects_AddProjectsDomainAndSubDomain, sqlParam);

                //--Get Domain  id
                if(!string.IsNullOrEmpty(sqlParam[4].Value.ToString()))
                DomainId = Convert.ToInt32(sqlParam[4].Value);

                //--Get SubDomain id
                if (!string.IsNullOrEmpty(sqlParam[5].Value.ToString()))
                SubDomainId = Convert.ToInt32(sqlParam[5].Value);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_PROJECTS, "AddDomainAndSubDomain", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDAProjects.CloseConncetion();
            }
        }
        #endregion Public Member Functions

    }
}
