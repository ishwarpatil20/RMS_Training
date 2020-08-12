//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           ProejctSummary.aspx.cs       
//  Author:         sudip.guha
//  Date written:   13/09/2009/ 12:30:30 PM
//  Description:    This class  provides the Data Access layer methods for ProfessionalDetails in  Employee module.
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
    public class ProfessionalDetails
    {
        #region Private Field Members

        private string CLASS_NAME = "ProfessionalDetails.cs";

        /// <summary>
        /// private array variable for Sql paramaters
        /// </summary>
        private SqlParameter[] sqlParam;

        private DataAccessClass objDA;

        SqlDataReader objDataReader;

        private BusinessEntities.ProfessionalDetails objProfessionalDetails;

        BusinessEntities.RaveHRCollection raveHRCollection;

        #endregion Private Field Members

        #region Public Member Functions

        /// <summary>
        /// Adds the professional details.
        /// </summary>
        /// <param name="objAddProfessionalDetails">The obj add professional details.</param>
        public void AddProfessionalDetails(BusinessEntities.ProfessionalDetails objAddProfessionalDetails)
        {
            try
            {
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[6];

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[0].Value = objAddProfessionalDetails.EMPId;

                sqlParam[1] = new SqlParameter(SPParameter.CourseName, SqlDbType.NChar, 20);
                if (objAddProfessionalDetails.CourseName == "" || objAddProfessionalDetails.CourseName == null)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objAddProfessionalDetails.CourseName;

                sqlParam[2] = new SqlParameter(SPParameter.InstitutionName, SqlDbType.NChar, 50);
                if (objAddProfessionalDetails.InstitutionName == "" || objAddProfessionalDetails.InstitutionName == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objAddProfessionalDetails.InstitutionName;

                sqlParam[3] = new SqlParameter(SPParameter.PassingYear, SqlDbType.NChar, 4);
                if (objAddProfessionalDetails.PassingYear == "" || objAddProfessionalDetails.PassingYear == null)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = objAddProfessionalDetails.PassingYear;

                sqlParam[4] = new SqlParameter(SPParameter.Score, SqlDbType.NChar, 10);
                if (objAddProfessionalDetails.Score == "" || objAddProfessionalDetails.Score == null)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = objAddProfessionalDetails.Score;

                sqlParam[5] = new SqlParameter(SPParameter.Outof, SqlDbType.NChar, 10);
                if (objAddProfessionalDetails.Outof == "" || objAddProfessionalDetails.Outof == null)
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = objAddProfessionalDetails.Outof;

                int AddProfessinalDetails = objDA.ExecuteNonQuerySP(SPNames.Employee_AddProfessionalDetails, sqlParam);

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer,CLASS_NAME, "AddProfessionalDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
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
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[7];

                sqlParam[0] = new SqlParameter(SPParameter.ProfessionalId, SqlDbType.Int);
                sqlParam[0].Value = objUpdateProfessinalDetails.ProfessionalId;

                sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[1].Value = objUpdateProfessinalDetails.EMPId;

                sqlParam[2] = new SqlParameter(SPParameter.CourseName, SqlDbType.NChar, 20);
                if (objUpdateProfessinalDetails.CourseName == "" || objUpdateProfessinalDetails.CourseName == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objUpdateProfessinalDetails.CourseName;

                sqlParam[3] = new SqlParameter(SPParameter.InstitutionName, SqlDbType.NChar, 50);
                if (objUpdateProfessinalDetails.InstitutionName == "" || objUpdateProfessinalDetails.InstitutionName == null)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = objUpdateProfessinalDetails.InstitutionName;

                sqlParam[4] = new SqlParameter(SPParameter.PassingYear, SqlDbType.NChar, 4);
                if (objUpdateProfessinalDetails.PassingYear == "" || objUpdateProfessinalDetails.PassingYear == null)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = objUpdateProfessinalDetails.PassingYear;

                sqlParam[5] = new SqlParameter(SPParameter.Score, SqlDbType.NChar, 10);
                if (objUpdateProfessinalDetails.Score == "" || objUpdateProfessinalDetails.Score == null)
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = objUpdateProfessinalDetails.Score;

                sqlParam[6] = new SqlParameter(SPParameter.Outof, SqlDbType.NChar, 10);
                if (objUpdateProfessinalDetails.Outof == "" || objUpdateProfessinalDetails.Outof == null)
                    sqlParam[6].Value = DBNull.Value;
                else
                    sqlParam[6].Value = objUpdateProfessinalDetails.Outof;

                int UpdateProfessinalDetails = objDA.ExecuteNonQuerySP(SPNames.Employee_UpdateProfessionalDetails, sqlParam);
                
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer,CLASS_NAME, "UpdateProfessionalDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Gets the professional details.
        /// </summary>
        /// <param name="objGetProfessionalDetails">The obj get professional details.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetProfessionalDetails(BusinessEntities.ProfessionalDetails objGetProfessionalDetails)
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
                if (objGetProfessionalDetails.EMPId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objGetProfessionalDetails.EMPId;

                //Execute the SP
                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetProfessionalDetails,sqlParam);

                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    objProfessionalDetails = new BusinessEntities.ProfessionalDetails();

                    objProfessionalDetails.ProfessionalId = int.Parse(objDataReader[DbTableColumn.PId].ToString());
                    objProfessionalDetails.EMPId = int.Parse(objDataReader[DbTableColumn.EMPId].ToString());
                    objProfessionalDetails.CourseName = objDataReader[DbTableColumn.CourseName].ToString();
                    objProfessionalDetails.InstitutionName = objDataReader[DbTableColumn.InstitutionName].ToString();
                    objProfessionalDetails.PassingYear = objDataReader[DbTableColumn.PassingYear].ToString();
                    objProfessionalDetails.Score = objDataReader[DbTableColumn.Score].ToString();
                    objProfessionalDetails.Outof = objDataReader[DbTableColumn.Outof].ToString() == string.Empty ? string.Empty : objDataReader[DbTableColumn.Outof].ToString();

                    // Add the object to Collection
                    raveHRCollection.Add(objProfessionalDetails);
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
        /// Deletes the professional details.
        /// </summary>
        /// <param name="objDeleteProfessionalDetails">The obj delete professional details.</param>
        public void DeleteProfessionalDetails(BusinessEntities.ProfessionalDetails objDeleteProfessionalDetails)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[2];

            try
            {
                objDA.OpenConnection(DBConstants.GetDBConnectionString());
                

                sqlParam[0] = new SqlParameter(SPParameter.ProfessionalId, SqlDbType.Int);
                if (objDeleteProfessionalDetails.ProfessionalId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objDeleteProfessionalDetails.ProfessionalId;

                sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objDeleteProfessionalDetails.EMPId == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objDeleteProfessionalDetails.EMPId;

                objDA.ExecuteNonQuerySP(SPNames.Employee_DeleteProfessionalDetails, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "DeleteProfessionalDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        #endregion 
    }
}
