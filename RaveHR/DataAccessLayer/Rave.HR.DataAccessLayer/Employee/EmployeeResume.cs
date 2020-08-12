//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           ProejctSummary.aspx.cs       
//  Author:         Rahul.Parwekar
//  Date written:   19/10/2010
//  Description:    This class  provides the Data Access layer methods for Employee Resume in  Employee module.
//                  
//
//  Amendments
//  Date                        Who             Ref     Description
//  ----                        -----------     ---     -----------
//  19/10/2010                  Rahul.Parwekar  n/a     
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Data.SqlClient;
using Common;
using Common.Constants;

namespace Rave.HR.DataAccessLayer.Employees
{
    public class EmployeeResume
    {
        #region Private Field Members

        private string CLASS_NAME = "EmployeeResume.cs";

        private string GETEMPLOYEERESUMEETAILS = "GetEmployeeResumeDetails";

        /// <summary>
        /// private variable for Data Access Class
        /// </summary>
        private DataAccessClass objDA;

        /// <summary>
        /// private array variable for Sql paramaters
        /// </summary>
        private SqlParameter[] sqlParam;

        SqlDataReader objDataReader;

        private BusinessEntities.EmployeeResume objEmployeeResume;

        BusinessEntities.RaveHRCollection raveHRCollection;

        #endregion

         #region Public Member Functions

        /// <summary>
        /// Adds the project details.
        /// </summary>
        /// <param name="objAddProjectDetails">The obj add project details.</param>
        public BusinessEntities.RaveHRCollection AddEmpResumeDetails(BusinessEntities.EmployeeResume objEmployeeResume)
        {
            //Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[5];

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[0].Value = objEmployeeResume.EMPId;

                sqlParam[1] = new SqlParameter(SPParameter.DocumentName, SqlDbType.NVarChar, 50);
                if (objEmployeeResume.DocumentName == "" || objEmployeeResume.DocumentName == null)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objEmployeeResume.DocumentName;

                sqlParam[2] = new SqlParameter(SPParameter.ModifyDate, SqlDbType.SmallDateTime);
                if (objEmployeeResume.ModifyDate == DateTime.MinValue || objEmployeeResume.ModifyDate == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objEmployeeResume.ModifyDate;

                sqlParam[3] = new SqlParameter(SPParameter.ModifyBy, SqlDbType.NVarChar, 50);
                if (objEmployeeResume.ModifyBy == "" || objEmployeeResume.ModifyBy == null)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = objEmployeeResume.ModifyBy;

                sqlParam[4] = new SqlParameter(SPParameter.FileExtension, SqlDbType.NVarChar, 50);
                if (objEmployeeResume.FileExtension == string.Empty || objEmployeeResume.FileExtension == null)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = objEmployeeResume.FileExtension;


                objDataReader = objDA.ExecuteReaderSP (SPNames.USP_Employee_AddEmployeeResumeDetails, sqlParam);

                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    objEmployeeResume = new BusinessEntities.EmployeeResume();

                    objEmployeeResume.ResumeCount = objDataReader[DbTableColumn.ResumeCount].ToString();

                    // Add the object to Collection
                    raveHRCollection.Add(objEmployeeResume);
                }

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "AddResumeDetails", EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
            return raveHRCollection;
        }


        /// <summary>
        /// Adds the project details.
        /// </summary>
        /// <param name="objAddProjectDetails">The obj add project details.</param>
        public BusinessEntities.RaveHRCollection GetEmpResumeCountDetails(BusinessEntities.EmployeeResume objEmployeeResumeCount)
        {
            //Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();
            try
            {
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[0].Value = objEmployeeResumeCount.EMPId;

                objDataReader = objDA.ExecuteReaderSP(SPNames.USP_Employee_GetEmployeeResumeCountDetails, sqlParam);

                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    objEmployeeResumeCount = new BusinessEntities.EmployeeResume();

                    objEmployeeResumeCount.ResumeCount = objDataReader[DbTableColumn.ResumeCount].ToString();
                    
                    // Add the object to Collection
                    raveHRCollection.Add(objEmployeeResumeCount);
                }

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetResumeDetailsCount", EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
            return raveHRCollection;
        }


        /// <summary>
        /// Deletes the Resume details.
        /// </summary>
        /// <param name="objDeleteProjectDetails">The obj delete project details.</param>
        public void DeleteResumeDetails(BusinessEntities.ProjectDetails objDeleteProjectDetails)
        {   

            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[2];

            try
            {
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objDeleteProjectDetails.EMPId  == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objDeleteProjectDetails.EMPId;

                sqlParam[1] = new SqlParameter(SPParameter.DocumentName, SqlDbType.NVarChar);
                if (objDeleteProjectDetails.EMPId == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objDeleteProjectDetails.EMPId;

                objDA.ExecuteNonQuerySP(SPNames.USP_Employee_DeleteEmployeeResumeDetails, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "DeleteResumeDetails", EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Gets the Employee Resume details.
        /// </summary>
        /// <param name="objGetCertificationDetails">The object get Employee Resume details.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetEmployeeResumeDetails(BusinessEntities.EmployeeResume objGetEmployeeResumeDetails)
        {
            //Initialise Data Access Class object
            objDA = new DataAccessClass();

            //Initialise SqlParameter Class object
            sqlParam = new SqlParameter[1];

            //Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Check each parameters nullibality and add values to sqlParam object accordingly
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objGetEmployeeResumeDetails.EMPId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objGetEmployeeResumeDetails.EMPId;

                objDataReader = objDA.ExecuteReaderSP(SPNames.USP_Employee_GetEmployeeResumeDetails, sqlParam);

                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    objGetEmployeeResumeDetails = new BusinessEntities.EmployeeResume();

                    objGetEmployeeResumeDetails.ResumeId = Convert.ToInt32(objDataReader[DbTableColumn.ResumeId].ToString());
                    objGetEmployeeResumeDetails.EMPId = Convert.ToInt32(objDataReader[DbTableColumn.EMPId].ToString());
                    objGetEmployeeResumeDetails.DocumentName = objDataReader[DbTableColumn.DocumentName].ToString();
                    objGetEmployeeResumeDetails.ModifyDate = Convert.ToDateTime(objDataReader[DbTableColumn.ModifyDate].ToString());
                    objGetEmployeeResumeDetails.ModifyBy = objDataReader[DbTableColumn.ModifyBy].ToString();
                    objGetEmployeeResumeDetails.FileExtension = objDataReader[DbTableColumn.FileExtension].ToString();

                    // Add the object to Collection
                    raveHRCollection.Add(objGetEmployeeResumeDetails);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETEMPLOYEERESUMEETAILS, EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDA.CloseConncetion();
            }
            // Return the Collection
            return raveHRCollection;
        }


        //19645-Ambar-Start
        public void DeleteEmpResumeDetails(string str_deletedfile, int EMPId)
        {
          try
          {
            objDA = new DataAccessClass();
            objDA.OpenConnection(DBConstants.GetDBConnectionString());
            SqlParameter[] sqlParam = new SqlParameter[2];

            sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
            sqlParam[0].Value = EMPId;

            sqlParam[1] = new SqlParameter(SPParameter.FileName, SqlDbType.NVarChar, 100);
            sqlParam[1].Value = str_deletedfile;

            objDataReader = objDA.ExecuteReaderSP(SPNames.USP_Employee_DelEmployeeResumeDetails, sqlParam);
          }
          catch (RaveHRException ex)
          {
            throw ex;
          }
          catch (Exception ex)
          {
            throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "AddResumeDetails", EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
          }
          finally
          {
            objDA.CloseConncetion();
          }          
        }
        //19645-Ambar-End
        
        #endregion
    }
}
