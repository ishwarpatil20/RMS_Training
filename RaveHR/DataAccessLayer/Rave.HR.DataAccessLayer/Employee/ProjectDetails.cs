//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           ProejctSummary.aspx.cs       
//  Author:         sudip.guha
//  Date written:   13/09/2009/ 12:30:30 PM
//  Description:    This class  provides the Data Access layer methods for ProjectDetails in  Employee module.
//                  
//
//  Amendments
//  Date                        Who             Ref     Description
//  ----                        -----------     ---     -----------
//  13/09/2009/ 12:30:30 PM     sudip.guha    n/a     Created    
//  17/08/2009/ 04:30:30 PM     Shrinivas Dalavi n/a     Used DataAccessClass,Add() Update() Delete() Get()
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Data.SqlClient;
using Common;
using Common.Constants;

namespace Rave.HR.DataAccessLayer.Employees
{
    public class ProjectDetails
    {
        #region Private Field Members

        private string CLASS_NAME = "ProjectDetails.cs";

        /// <summary>
        /// private variable for Data Access Class
        /// </summary>
        private DataAccessClass objDA;

        /// <summary>
        /// private array variable for Sql paramaters
        /// </summary>
        private SqlParameter[] sqlParam;

        SqlDataReader objDataReader;

        private BusinessEntities.ProjectDetails objProjectDetails;

        BusinessEntities.RaveHRCollection raveHRCollection;

        #endregion

        #region Public Member Functions

        /// <summary>
        /// Adds the project details.
        /// </summary>
        /// <param name="objAddProjectDetails">The obj add project details.</param>
        public void AddProjectDetails(BusinessEntities.ProjectDetails objAddProjectDetails)
        {
            try
            {
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[14];

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[0].Value = objAddProjectDetails.EMPId;

                sqlParam[1] = new SqlParameter(SPParameter.ProjectName, SqlDbType.NVarChar, 50);
                if (objAddProjectDetails.ProjectName == "" || objAddProjectDetails.ProjectName == null)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objAddProjectDetails.ProjectName;


                sqlParam[2] = new SqlParameter(SPParameter.Organisation, SqlDbType.NVarChar, 50);
                if (objAddProjectDetails.Organisation == "" || objAddProjectDetails.Organisation == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objAddProjectDetails.Organisation;

                sqlParam[3] = new SqlParameter(SPParameter.Years, SqlDbType.NChar, 10);
                if (objAddProjectDetails.Years == "" || objAddProjectDetails.Years == null)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = objAddProjectDetails.Years;

                sqlParam[4] = new SqlParameter(SPParameter.Role, SqlDbType.NChar, 10);
                if (objAddProjectDetails.Role == "" || objAddProjectDetails.Role == null)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = objAddProjectDetails.Role;

                sqlParam[5] = new SqlParameter(SPParameter.Onsite, SqlDbType.Bit);
                if (objAddProjectDetails.Onsite == null)
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = objAddProjectDetails.Onsite;

                sqlParam[6] = new SqlParameter(SPParameter.OnsiteDuration, SqlDbType.NChar, 10);
                if (objAddProjectDetails.OnsiteDuration == "" || objAddProjectDetails.OnsiteDuration == null)
                    sqlParam[6].Value = DBNull.Value;
                else
                    sqlParam[6].Value = objAddProjectDetails.OnsiteDuration;

                sqlParam[7] = new SqlParameter(SPParameter.Description, SqlDbType.NChar);
                if (objAddProjectDetails.Description == "" || objAddProjectDetails.Description == null)
                    sqlParam[7].Value = DBNull.Value;
                else
                    sqlParam[7].Value = objAddProjectDetails.Description;

                sqlParam[8] = new SqlParameter(SPParameter.Resposibility, SqlDbType.NChar);
                if (objAddProjectDetails.Resposibility == "" || objAddProjectDetails.Resposibility == null)
                    sqlParam[8].Value = DBNull.Value;
                else
                    sqlParam[8].Value = objAddProjectDetails.Resposibility;

                sqlParam[9] = new SqlParameter(SPParameter.ProjectLocation, SqlDbType.Int);
                if (objAddProjectDetails.ProjectLocation == "" || objAddProjectDetails.ProjectLocation == null)
                    sqlParam[9].Value = DBNull.Value;
                else
                    sqlParam[9].Value = objAddProjectDetails.LocationId;

                sqlParam[10] = new SqlParameter(SPParameter.ClientName, SqlDbType.NChar);
                if (objAddProjectDetails.ClientName == "" || objAddProjectDetails.ClientName == null)
                    sqlParam[10].Value = DBNull.Value;
                else
                    sqlParam[10].Value = objAddProjectDetails.ClientName;

                sqlParam[11] = new SqlParameter(SPParameter.ProjectSize, SqlDbType.Int);
                sqlParam[11].Value = objAddProjectDetails.ProjectSize;

                sqlParam[12] = new SqlParameter(SPParameter.ProjectDone, SqlDbType.Int);
                sqlParam[12].Value = objAddProjectDetails.ProjectDone;

                sqlParam[13] = new SqlParameter(SPParameter.RankOrder, SqlDbType.Int);
                sqlParam[13].Value = objAddProjectDetails.RankOrder;


                int AddProjectDetails = objDA.ExecuteNonQuerySP(SPNames.Employee_AddProjectsDetails, sqlParam);

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "AddProjectsDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Updates the project details.
        /// </summary>
        /// <param name="objUpdateProjectDetails">The obj update project details.</param>
        public void UpdateProjectDetails(BusinessEntities.ProjectDetails objUpdateProjectDetails)
        {
            try
            {
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[15];

                sqlParam[0] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int);
                sqlParam[0].Value = objUpdateProjectDetails.ProjectId;

                sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[1].Value = objUpdateProjectDetails.EMPId;

                sqlParam[2] = new SqlParameter(SPParameter.ProjectName, SqlDbType.NVarChar, 50);
                if (objUpdateProjectDetails.ProjectName == "" || objUpdateProjectDetails.ProjectName == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objUpdateProjectDetails.ProjectName;


                sqlParam[3] = new SqlParameter(SPParameter.Organisation, SqlDbType.NVarChar, 50);
                if (objUpdateProjectDetails.Organisation == "" || objUpdateProjectDetails.Organisation == null)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = objUpdateProjectDetails.Organisation;

                sqlParam[4] = new SqlParameter(SPParameter.Years, SqlDbType.NChar,10);
                if (objUpdateProjectDetails.Years == "" || objUpdateProjectDetails.Years == null)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = objUpdateProjectDetails.Years;

                sqlParam[5] = new SqlParameter(SPParameter.Role, SqlDbType.NChar, 10);
                if (objUpdateProjectDetails.Role == "" || objUpdateProjectDetails.Role == null)
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = objUpdateProjectDetails.Role;

                sqlParam[6] = new SqlParameter(SPParameter.Onsite, SqlDbType.Bit);
                sqlParam[6].Value = objUpdateProjectDetails.Onsite;

                sqlParam[7] = new SqlParameter(SPParameter.OnsiteDuration, SqlDbType.NChar, 10);
                if (objUpdateProjectDetails.OnsiteDuration == "" || objUpdateProjectDetails.OnsiteDuration == null)
                    sqlParam[7].Value = DBNull.Value;
                else
                    sqlParam[7].Value = objUpdateProjectDetails.OnsiteDuration;

                sqlParam[8] = new SqlParameter(SPParameter.Description, SqlDbType.NChar);
                if (objUpdateProjectDetails.Description == "" || objUpdateProjectDetails.Description == null)
                    sqlParam[8].Value = DBNull.Value;
                else
                    sqlParam[8].Value = objUpdateProjectDetails.Description;

                sqlParam[9] = new SqlParameter(SPParameter.Resposibility, SqlDbType.NChar);
                if (objUpdateProjectDetails.Resposibility == "" || objUpdateProjectDetails.Resposibility == null)
                    sqlParam[9].Value = DBNull.Value;
                else
                    sqlParam[9].Value = objUpdateProjectDetails.Resposibility;

                sqlParam[10] = new SqlParameter(SPParameter.ProjectLocation, SqlDbType.Int);
                if (objUpdateProjectDetails.ProjectLocation == "" || objUpdateProjectDetails.ProjectLocation == null)
                    sqlParam[10].Value = DBNull.Value;
                else
                    sqlParam[10].Value = objUpdateProjectDetails.LocationId;

                sqlParam[11] = new SqlParameter(SPParameter.ClientName, SqlDbType.NChar);
                if (objUpdateProjectDetails.ClientName == "" || objUpdateProjectDetails.ClientName == null)
                    sqlParam[11].Value = DBNull.Value;
                else
                    sqlParam[11].Value = objUpdateProjectDetails.ClientName;

                sqlParam[12] = new SqlParameter(SPParameter.ProjectSize, SqlDbType.Int);
                sqlParam[12].Value = objUpdateProjectDetails.ProjectSize;

                sqlParam[13] = new SqlParameter(SPParameter.ProjectDone, SqlDbType.Int);
                sqlParam[13].Value = objUpdateProjectDetails.ProjectDone;

                sqlParam[14] = new SqlParameter(SPParameter.RankOrder, SqlDbType.Int);
                sqlParam[14].Value = objUpdateProjectDetails.RankOrder;


                int UpdateProjectDetails = objDA.ExecuteNonQuerySP(SPNames.Employee_UpdateProjectsDetails, sqlParam);

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "UpdateProjectDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Gets the project details.
        /// </summary>
        /// <param name="objGetProjectDetails">The obj get project details.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetProjectDetails(BusinessEntities.ProjectDetails objGetProjectDetails)
        {
            // Initialise Data Access Class object
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[1];

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objGetProjectDetails.EMPId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objGetProjectDetails.EMPId;

                //Execute the SP
                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetProjectsDetails, sqlParam);

                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    objProjectDetails = new BusinessEntities.ProjectDetails();

                    objProjectDetails.ProjectId = int.Parse(objDataReader[DbTableColumn.PId].ToString());
                    objProjectDetails.EMPId = int.Parse(objDataReader[DbTableColumn.EMPId].ToString());
                    objProjectDetails.ProjectName = objDataReader[DbTableColumn.ProjectName].ToString();
                    objProjectDetails.Organisation = objDataReader[DbTableColumn.Organisation].ToString();
                    objProjectDetails.Years = objDataReader[DbTableColumn.Years].ToString();
                    objProjectDetails.Role = objDataReader[DbTableColumn.Role].ToString();
                    objProjectDetails.Onsite =Convert.ToBoolean(objDataReader[DbTableColumn.Onsite].ToString());
                    objProjectDetails.OnsiteDuration = objDataReader[DbTableColumn.OnsiteDuration].ToString();
                    objProjectDetails.Description = objDataReader[DbTableColumn.Description].ToString();
                    objProjectDetails.LocationId = int.Parse(objDataReader[DbTableColumn.ProjectLocation].ToString());
                    objProjectDetails.ProjectLocation = objDataReader[DbTableColumn.LocationName].ToString();
                    objProjectDetails.ProjectSize = int.Parse(objDataReader[DbTableColumn.ProjectSize].ToString());
                    objProjectDetails.ClientName = objDataReader[DbTableColumn.ClientName].ToString();
                    objProjectDetails.Resposibility = objDataReader[DbTableColumn.Resposibility].ToString();
                    objProjectDetails.ProjectDone = int.Parse(objDataReader[DbTableColumn.ProjectDone].ToString());
                    objProjectDetails.ProjectDoneName = objDataReader[DbTableColumn.ProjectDoneName].ToString();
                    if (!string.IsNullOrEmpty(objDataReader[DbTableColumn.RankOrder].ToString()))
                    objProjectDetails.RankOrder = int.Parse(objDataReader[DbTableColumn.RankOrder].ToString());
                   
                    // Add the object to Collection
                    raveHRCollection.Add(objProjectDetails);
                }

                // Return the Collection
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetProfessionalDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Deletes the project details.
        /// </summary>
        /// <param name="objDeleteProjectDetails">The obj delete project details.</param>
        public void DeleteProjectDetails(BusinessEntities.ProjectDetails objDeleteProjectDetails)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[2];

            try
            {
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int);
                if (objDeleteProjectDetails.ProjectId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objDeleteProjectDetails.ProjectId;

                sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objDeleteProjectDetails.EMPId == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objDeleteProjectDetails.EMPId;

                objDA.ExecuteNonQuerySP(SPNames.Employee_DeleteProjectsDetails, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "DeleteProjectsDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        #endregion
    }
}
