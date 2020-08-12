//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           ProejctSummary.aspx.cs       
//  Author:         sudip.guha
//  Date written:   13/09/2009/ 12:30:30 PM
//  Description:    This class  provides the Data Access layer methods for VisaDetails in  Employee module.
//                  
//
//  Amendments
//  Date                        Who             Ref     Description
//  ----                        -----------     ---     -----------
//  13/09/2009/ 12:30:30 PM     sudip.guha    n/a     Created    
//  18/10/2009  12:30:30 PM     Shrinivas.Dalavi    n/a     add,update,get,delete methods     
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Data.SqlClient;
using Common;
using Common.Constants;

namespace Rave.HR.DataAccessLayer.Employees
{
    public class VisaDetails
    {

        #region Private Field Members

        private string CLASS_NAME = "VisaDetails.cs";

        private string Fn_AddVisaDetails = "AddVisaDetails";

        private string Fn_UpdateVisaDetails = "UpdateVisaDetails";

        private string Fn_GetProfessionalDetails = "GetProfessionalDetails";

        private string Fn_DeleteVisaDetails = "DeleteVisaDetails";

        private string Fn_DeleteVisaDetailsByEmpId = "DeleteVisaDetailsByEmpId";

        /// <summary>
        /// private array variable for Sql paramaters
        /// </summary>
        private SqlParameter[] sqlParam;

        private DataAccessClass objDA;

        SqlDataReader objDataReader;

        private BusinessEntities.VisaDetails objVisaDetails;

        BusinessEntities.RaveHRCollection raveHRCollection;

        #endregion

        #region Public Member Functions

        /// <summary>
        /// Adds the visa details.
        /// </summary>
        /// <param name="objAddVisaDetails">The obj add visa details.</param>
        public void AddVisaDetails(BusinessEntities.VisaDetails objAddVisaDetails)
        {
            try
            {
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString()); 
                SqlParameter[] sqlParam = new SqlParameter[4];

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[0].Value = objAddVisaDetails.EMPId;

                sqlParam[1] = new SqlParameter(SPParameter.CountryName, SqlDbType.NChar, 50);
                if (objAddVisaDetails.CountryName == "" || objAddVisaDetails.CountryName == null)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objAddVisaDetails.CountryName;

                sqlParam[2] = new SqlParameter(SPParameter.VisaType, SqlDbType.NChar, 50);
                if (objAddVisaDetails.VisaType == "" || objAddVisaDetails.VisaType == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objAddVisaDetails.VisaType;

                sqlParam[3] = new SqlParameter(SPParameter.ExpiryDate, SqlDbType.SmallDateTime);
                if (objAddVisaDetails.ExpiryDate == null)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = objAddVisaDetails.ExpiryDate;

                int AddVisaDetails = objDA.ExecuteNonQuerySP(SPNames.Employee_AddVisaDetails, sqlParam);

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, Fn_AddVisaDetails, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
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
        /// Updates the organisation details.
        /// </summary>
        /// <param name="objUpdateVisaDetails">The obj update visa details.</param>
        public void UpdateVisaDetails(BusinessEntities.VisaDetails objUpdateVisaDetails)
        {
            try
            {
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[5];

                sqlParam[0] = new SqlParameter(SPParameter.VisaId, SqlDbType.Int);
                sqlParam[0].Value = objUpdateVisaDetails.VisaId;

                sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[1].Value = objUpdateVisaDetails.EMPId;

                sqlParam[2] = new SqlParameter(SPParameter.CountryName, SqlDbType.NChar, 50);
                if (objUpdateVisaDetails.CountryName == "" || objUpdateVisaDetails.CountryName == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objUpdateVisaDetails.CountryName;

                sqlParam[3] = new SqlParameter(SPParameter.VisaType, SqlDbType.NChar, 50);
                if (objUpdateVisaDetails.VisaType == "" || objUpdateVisaDetails.VisaType == null)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = objUpdateVisaDetails.VisaType;

                sqlParam[4] = new SqlParameter(SPParameter.ExpiryDate, SqlDbType.SmallDateTime);
                if (objUpdateVisaDetails.ExpiryDate == null)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = objUpdateVisaDetails.ExpiryDate;

                int UpdateVisaDetails = objDA.ExecuteNonQuerySP(SPNames.Employee_UpdateVisaDetails, sqlParam);

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, Fn_UpdateVisaDetails, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Gets the visa details.
        /// </summary>
        /// <param name="objGetVisaDetails">The obj get visa details.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetVisaDetails(BusinessEntities.VisaDetails objGetVisaDetails)
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
                if (objGetVisaDetails.EMPId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objGetVisaDetails.EMPId;

                //Execute the SP
                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetVisaDetails, sqlParam);

                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    objVisaDetails = new BusinessEntities.VisaDetails();

                    objVisaDetails.VisaId = int.Parse(objDataReader[DbTableColumn.VisaId].ToString());
                    objVisaDetails.EMPId = int.Parse(objDataReader[DbTableColumn.EMPId].ToString());
                    objVisaDetails.CountryName = objDataReader[DbTableColumn.CountryName].ToString();
                    objVisaDetails.VisaType = objDataReader[DbTableColumn.VisaType].ToString();
                    objVisaDetails.ExpiryDate =Convert.ToDateTime(objDataReader[DbTableColumn.ExpiryDate].ToString());
                    
                    // Add the object to Collection
                    raveHRCollection.Add(objVisaDetails);
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
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, Fn_GetProfessionalDetails, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
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
        /// Deletes the visa details.
        /// </summary>
        /// <param name="objDeleteVisaDetails">The obj delete visa details.</param>
        public void DeleteVisaDetails(BusinessEntities.VisaDetails objDeleteVisaDetails)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[2];

            try
            {
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.VisaId, SqlDbType.Int);
                if (objDeleteVisaDetails.VisaId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objDeleteVisaDetails.VisaId;

                sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objDeleteVisaDetails.EMPId == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objDeleteVisaDetails.EMPId;

                objDA.ExecuteNonQuerySP(SPNames.Employee_DeleteVisaDetails, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, Fn_DeleteVisaDetails, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Deletes the visa details by emp id.
        /// </summary>
        /// <param name="EmployeeId">The employee id.</param>
        public void DeleteVisaDetailsByEmpId(int EmployeeId)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[1];

            try
            {
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[0].Value = EmployeeId;

                objDA.ExecuteNonQuerySP(SPNames.Employee_DeleteVisaDetailsByEmpId, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, Fn_DeleteVisaDetailsByEmpId, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        #endregion

    }
}
