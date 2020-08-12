//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           CertificationDetails.cs       
//  Author:         sudip.guha
//  Date written:   14/08/2009/ 06:17:30 PM
//  Description:    This class  provides the Data Access layer methods for CertificationDetails in  Employee module.
//                  
//
//  Amendments
//  Date                        Who             Ref     Description
//  ----                        -----------     ---     -----------
//  14/08/2009/ 06:17:30 PM     sudip.guha      n/a     Created  
//  17/08/2009/ 05:30:30 PM     Vineet Kulkarni n/a     Used DataAccessClass,Added Delete() Get()
//  03/09/2009/ 03:58:15 PM     vineet.kulkarni n/a     moved BE's List to collection, DbColumnName used, proper commenting
//
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Data.SqlClient;
using Common;
using Common.Constants;

namespace Rave.HR.DataAccessLayer.Employees
{
    /// <summary>
    /// This class provide methods to Add, Update, Retrive, Delete Certification details
    /// </summary>
    public class CertificationDetails
    {
        #region Private Member Variables

        /// <summary>
        /// private variable for Class Name used in each catch block
        /// </summary>
        private string CLASS_NAME = "CertificationDetails";

        /// <summary>
        /// private variable for Data Access Class
        /// </summary>
        private DataAccessClass objDA;

        /// <summary>
        /// private array variable for Sql paramaters
        /// </summary>
        private SqlParameter[] sqlParam;

        /// <summary>
        /// private object for SqlDataReader
        /// </summary>
        private SqlDataReader objDataReader;

        /// <summary>
        /// private variable for Qualification
        /// </summary>
        private BusinessEntities.CertificationDetails objCertificationDetails;

        /// <summary>
        /// private object for RaveHRCollection
        /// </summary>
        private BusinessEntities.RaveHRCollection raveHRCollection;

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string ADDCERTIFICATIONDETAILS = "AddCertificationDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string UPDATECERTIFICATIONDETAILS = "UpdateCertificationDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string GETCERTIFICATIONDETAILS = "GetCertificationDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string DELETECERTIFICATIONDETAILS = "DeleteCertificationDetails";

        #endregion Private Member Variables

        #region Public Member Functions

        /// <summary>
        /// Adds the certification details.
        /// </summary>
        /// <param name="objAddCertificationDetails">The object add certification details.</param>
        public void AddCertificationDetails(BusinessEntities.CertificationDetails objAddCertificationDetails)
        {
            //Initialise Data Access Class object
            objDA = new DataAccessClass();

            //Initialise SqlParameter Class object
            sqlParam = new SqlParameter[6];

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Check each parameters nullibality and add values to sqlParam object accordingly
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objAddCertificationDetails.EMPId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objAddCertificationDetails.EMPId;

                sqlParam[1] = new SqlParameter(SPParameter.CertificationName, SqlDbType.NChar, 10);
                if (objAddCertificationDetails.CertificationName == "" || objAddCertificationDetails.CertificationName == null)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objAddCertificationDetails.CertificationName;

                sqlParam[2] = new SqlParameter(SPParameter.CertificateDate, SqlDbType.DateTime);
                if (objAddCertificationDetails.CertificateDate == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objAddCertificationDetails.CertificateDate;

                sqlParam[3] = new SqlParameter(SPParameter.CertificateValidDate, SqlDbType.DateTime);
                if (objAddCertificationDetails.CertificateValidDate == null || objAddCertificationDetails.CertificateValidDate == DateTime.MinValue)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = objAddCertificationDetails.CertificateValidDate;

                sqlParam[4] = new SqlParameter(SPParameter.Score, SqlDbType.Float);
                if (objAddCertificationDetails.Score == 0)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = objAddCertificationDetails.Score;

                sqlParam[5] = new SqlParameter(SPParameter.OutOf, SqlDbType.Float);
                if (objAddCertificationDetails.OutOf == 0)
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = objAddCertificationDetails.OutOf;

                //Execute SP along with proper parameters
                objDA.ExecuteNonQuerySP(SPNames.Employee_AddCertificationDetails, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, ADDCERTIFICATIONDETAILS, EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Updates the certification details.
        /// </summary>
        /// <param name="objUpdateCertificationDetails">The object update certification details.</param>
        public void UpdateCertificationDetails(BusinessEntities.CertificationDetails objUpdateCertificationDetails)
        {
            //Initialise Data Access Class object
            objDA = new DataAccessClass();

            //Initialise SqlParameter Class object
            sqlParam = new SqlParameter[7];

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Check each parameters nullibality and add values to sqlParam object accordingly
                sqlParam[0] = new SqlParameter(SPParameter.CertificationId, SqlDbType.Int);
                if (objUpdateCertificationDetails.CertificationId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objUpdateCertificationDetails.CertificationId;

                sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objUpdateCertificationDetails.EMPId == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objUpdateCertificationDetails.EMPId;

                sqlParam[2] = new SqlParameter(SPParameter.CertificationName, SqlDbType.NChar, 10);
                if (objUpdateCertificationDetails.CertificationName == "" || objUpdateCertificationDetails.CertificationName == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objUpdateCertificationDetails.CertificationName;

                sqlParam[3] = new SqlParameter(SPParameter.CertificateDate, SqlDbType.DateTime);
                if (objUpdateCertificationDetails.CertificateDate == null)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = objUpdateCertificationDetails.CertificateDate;

                sqlParam[4] = new SqlParameter(SPParameter.CertificateValidDate, SqlDbType.DateTime);
                if (objUpdateCertificationDetails.CertificateValidDate == null)
                    sqlParam[4].Value = DBNull.Value;
                else
                    if (objUpdateCertificationDetails.CertificateValidDate == DateTime.MinValue)
                        sqlParam[4].Value = DBNull.Value;
                    else
                        sqlParam[4].Value = objUpdateCertificationDetails.CertificateValidDate;

                sqlParam[5] = new SqlParameter(SPParameter.Score, SqlDbType.Float);
                if (objUpdateCertificationDetails.Score == 0)
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = objUpdateCertificationDetails.Score;

                sqlParam[6] = new SqlParameter(SPParameter.OutOf, SqlDbType.Float);
                if (objUpdateCertificationDetails.OutOf == 0)
                    sqlParam[6].Value = DBNull.Value;
                else
                    sqlParam[6].Value = objUpdateCertificationDetails.OutOf;

                //Execute SP along with proper parameters
                objDA.ExecuteNonQuerySP(SPNames.Employee_UpdateCertificationDetails, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, UPDATECERTIFICATIONDETAILS, EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Gets the certification details.
        /// </summary>
        /// <param name="objGetCertificationDetails">The object get certification details.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetCertificationDetails(BusinessEntities.CertificationDetails objGetCertificationDetails)
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
                if (objGetCertificationDetails.EMPId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objGetCertificationDetails.EMPId;

                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetCertificationDetails, sqlParam);

                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    objCertificationDetails = new BusinessEntities.CertificationDetails();

                    objCertificationDetails.CertificationId = Convert.ToInt32(objDataReader[DbTableColumn.CId].ToString());
                    objCertificationDetails.EMPId = Convert.ToInt32(objDataReader[DbTableColumn.EMPId].ToString());
                    objCertificationDetails.CertificationName = objDataReader[DbTableColumn.CertificationName].ToString();
                    objCertificationDetails.CertificateDate = Convert.ToDateTime(objDataReader[DbTableColumn.CertificateDate].ToString());
                    if (objDataReader[DbTableColumn.CertificateValidDate].ToString() != "")
                        objCertificationDetails.CertificateValidDate = Convert.ToDateTime(objDataReader[DbTableColumn.CertificateValidDate].ToString());
                    objCertificationDetails.Score = float.Parse(objDataReader[DbTableColumn.Score].ToString());
                    objCertificationDetails.OutOf = float.Parse(objDataReader[DbTableColumn.OutOf].ToString());

                    // Add the object to Collection
                    raveHRCollection.Add(objCertificationDetails);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETCERTIFICATIONDETAILS, EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
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

        /// <summary>
        /// Deletes the certification details.
        /// </summary>
        /// <param name="objDeleteCertificationDetails">The object delete certification details.</param>
        public void DeleteCertificationDetails(BusinessEntities.CertificationDetails objDeleteCertificationDetails)
        {
            //Initialise Data Access Class object
            objDA = new DataAccessClass();

            //Initialise SqlParameter Class object
            sqlParam = new SqlParameter[2];

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Check each parameters nullibality and add values to sqlParam object accordingly
                sqlParam[0] = new SqlParameter(SPParameter.CertificationId, SqlDbType.Int);
                if (objDeleteCertificationDetails.CertificationId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objDeleteCertificationDetails.CertificationId;

                sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objDeleteCertificationDetails.EMPId == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objDeleteCertificationDetails.EMPId;

                //Execute SP along with proper parameters
                objDA.ExecuteNonQuerySP(SPNames.Employee_DeleteCertificationDetails, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, DELETECERTIFICATIONDETAILS, EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        #endregion Public Member Functions
    }
}
