//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           QualificationDetails.cs       
//  Author:         sudip.guha
//  Date written:   14/08/2009/ 06:17:31 PM
//  Description:    This class  provides the Data Access layer methods for QualificationDetails in  Employee module.
//                  
//
//  Amendments
//  Date                        Who             Ref     Description
//  ----                        -----------     ---     -----------
//  14/08/2009/ 06:17:31 PM     sudip.guha      n/a     Created    
//  17/08/2009/ 01:03:27 PM     vineet.kulkarni n/a     Used DataAccessClass,Added Delete() Get()
//  03/09/2009/ o2:32:28 PM     vineet.kulkarni n/a     moved BE's List to collection, DbColumnName used, proper commenting
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
    /// This class provide methods to Add, Update, Retrive, Delete Qualification details
    /// </summary>
    public class QualificationDetails
    {
        #region Private Member Variables

        /// <summary>
        /// private variable for Class Name used in each catch block
        /// </summary>
        private string CLASS_NAME = "QualificationDetails";

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
        /// private object for Qualification
        /// </summary>
        private BusinessEntities.QualificationDetails objQualificationDetails;           

        /// <summary>
        /// private object for RaveHRCollection
        /// </summary>
        private BusinessEntities.RaveHRCollection raveHRCollection;

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string ADDQUALIFICATIONDETAILS = "AddQualificationDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string UPDATEQUALIFICATIONDETAILS = "UpdateQualificationDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string GETQUALIFICATIONDETAILS = "GetQualificationDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string DELETEQUALIFICATIONDETAILS = "DeleteQualificationDetails";

        #endregion Private Member Variables

        #region Public Member Functions

        /// <summary>
        /// Adds the qualification details.
        /// </summary>
        /// <param name="objAddQualificationDetails">The object add qualification details.</param>
        public void AddQualificationDetails(BusinessEntities.QualificationDetails objAddQualificationDetails)
        {
            //Initialise Data Access Class object
            objDA = new DataAccessClass();

            //Initialise SqlParameter Class object
            sqlParam = new SqlParameter[9];

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Check each parameters nullibality and add values to sqlParam object accordingly
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objAddQualificationDetails.EMPId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objAddQualificationDetails.EMPId;

                sqlParam[1] = new SqlParameter(SPParameter.Qualification, SqlDbType.NChar, 10);
                if (objAddQualificationDetails.Qualification == 0)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objAddQualificationDetails.Qualification;

                sqlParam[2] = new SqlParameter(SPParameter.UniversityName, SqlDbType.NChar, 50);
                if (objAddQualificationDetails.UniversityName == "" || objAddQualificationDetails.UniversityName == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objAddQualificationDetails.UniversityName;

                sqlParam[3] = new SqlParameter(SPParameter.InstitutionName, SqlDbType.NChar, 50);
                if (objAddQualificationDetails.InstituteName == "" || objAddQualificationDetails.InstituteName == null)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = objAddQualificationDetails.InstituteName;

                sqlParam[4] = new SqlParameter(SPParameter.PassingYear, SqlDbType.NChar, 50);
                if (objAddQualificationDetails.PassingYear == "" || objAddQualificationDetails.PassingYear == null)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = objAddQualificationDetails.PassingYear;

                sqlParam[5] = new SqlParameter(SPParameter.Percentage, SqlDbType.Float);
                if (objAddQualificationDetails.Percentage == 0)
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = objAddQualificationDetails.Percentage;

                sqlParam[6] = new SqlParameter(SPParameter.OutOf, SqlDbType.Float);
                if (objAddQualificationDetails.Outof == 0)
                    sqlParam[6].Value = DBNull.Value;
                else
                    sqlParam[6].Value = objAddQualificationDetails.Outof;

                sqlParam[7] = new SqlParameter(SPParameter.GPA, SqlDbType.Float);
                if (objAddQualificationDetails.GPA == 0)
                    sqlParam[7].Value = DBNull.Value;
                else
                    sqlParam[7].Value = objAddQualificationDetails.GPA;

                sqlParam[8] = new SqlParameter(SPParameter.Stream, SqlDbType.NChar, 50);
                if (objAddQualificationDetails.Stream == "" || objAddQualificationDetails.Stream == null)
                    sqlParam[8].Value = DBNull.Value;
                else
                    sqlParam[8].Value = objAddQualificationDetails.Stream;

                //Execute SP along with proper parameters
                objDA.ExecuteNonQuerySP(SPNames.Employee_AddQualificationDetails, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, ADDQUALIFICATIONDETAILS, EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Updates the qualification details.
        /// </summary>
        /// <param name="objUpdateQualificationDetails">The object update qualification details.</param>
        public void UpdateQualificationDetails(BusinessEntities.QualificationDetails objUpdateQualificationDetails)
        {
            //Initialise Data Access Class object
            objDA = new DataAccessClass();

            //Initialise SqlParameter Class object
            sqlParam = new SqlParameter[10];

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Check each parameters nullibality and add values to sqlParam object accordingly
                sqlParam[0] = new SqlParameter(SPParameter.QualificationId, SqlDbType.Int);
                if (objUpdateQualificationDetails.QualificationId == 0 )
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objUpdateQualificationDetails.QualificationId;
                
                sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objUpdateQualificationDetails.EMPId == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objUpdateQualificationDetails.EMPId;

                sqlParam[2] = new SqlParameter(SPParameter.Qualification, SqlDbType.Int);
                if (objUpdateQualificationDetails.Qualification == 0)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objUpdateQualificationDetails.Qualification;

                sqlParam[3] = new SqlParameter(SPParameter.UniversityName, SqlDbType.NChar, 50);
                if (objUpdateQualificationDetails.UniversityName == "" || objUpdateQualificationDetails.UniversityName == null)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = objUpdateQualificationDetails.UniversityName;

                sqlParam[4] = new SqlParameter(SPParameter.InstitutionName, SqlDbType.NChar, 50);
                if (objUpdateQualificationDetails.InstituteName == "" || objUpdateQualificationDetails.InstituteName == null)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = objUpdateQualificationDetails.InstituteName;

                sqlParam[5] = new SqlParameter(SPParameter.PassingYear, SqlDbType.NChar, 50);
                if (objUpdateQualificationDetails.PassingYear == "" || objUpdateQualificationDetails.PassingYear == null)
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = objUpdateQualificationDetails.PassingYear;

                sqlParam[6] = new SqlParameter(SPParameter.Percentage, SqlDbType.Float);
                if (objUpdateQualificationDetails.Percentage == 0)
                    sqlParam[6].Value = DBNull.Value;
                else
                    sqlParam[6].Value = objUpdateQualificationDetails.Percentage;

                sqlParam[7] = new SqlParameter(SPParameter.OutOf, SqlDbType.Float);
                if (objUpdateQualificationDetails.Outof == 0)
                    sqlParam[7].Value = DBNull.Value;
                else
                    sqlParam[7].Value = objUpdateQualificationDetails.Outof;

                sqlParam[8] = new SqlParameter(SPParameter.GPA, SqlDbType.Float);
                if (objUpdateQualificationDetails.GPA == 0)
                    sqlParam[8].Value = DBNull.Value;
                else
                    sqlParam[8].Value = objUpdateQualificationDetails.GPA;

                sqlParam[9] = new SqlParameter(SPParameter.Stream, SqlDbType.NChar, 50);
                if (objUpdateQualificationDetails.Stream == "" || objUpdateQualificationDetails.Stream == null)
                    sqlParam[9].Value = DBNull.Value;
                else
                    sqlParam[9].Value = objUpdateQualificationDetails.Stream;

                //Execute SP along with proper parameters
                objDA.ExecuteNonQuerySP(SPNames.Employee_UpdateQualificationDetails, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, UPDATEQUALIFICATIONDETAILS, EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Gets the qualification details.
        /// </summary>
        /// <param name="objGetQualificationDetails">The object get qualification details.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetQualificationDetails(BusinessEntities.QualificationDetails objGetQualificationDetails)
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
                if (objGetQualificationDetails.EMPId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objGetQualificationDetails.EMPId;

                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetQualificationDetails, sqlParam);

                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    objQualificationDetails = new BusinessEntities.QualificationDetails();
                    
                    objQualificationDetails.QualificationId = Convert.ToInt32(objDataReader[DbTableColumn.QId].ToString());
                    objQualificationDetails.EMPId = Convert.ToInt32(objDataReader[DbTableColumn.EMPId].ToString());
                    objQualificationDetails.QualificationName = objDataReader[DbTableColumn.Qualification].ToString();
                    objQualificationDetails.UniversityName = objDataReader[DbTableColumn.UniversityName].ToString();
                    objQualificationDetails.InstituteName = objDataReader[DbTableColumn.InstitutionName].ToString();
                    objQualificationDetails.PassingYear = objDataReader[DbTableColumn.PassingYear].ToString();
                    objQualificationDetails.Percentage = objDataReader[DbTableColumn.Percentage].ToString() == string.Empty ? 0 : float.Parse(objDataReader[DbTableColumn.Percentage].ToString());
                    objQualificationDetails.GPA = objDataReader[DbTableColumn.GPA].ToString() == string.Empty ? 0 : float.Parse(objDataReader[DbTableColumn.GPA].ToString());
                    objQualificationDetails.Outof = objDataReader[DbTableColumn.OutOf].ToString() == string.Empty ? 0 : float.Parse(objDataReader[DbTableColumn.OutOf].ToString());
                    objQualificationDetails.Qualification = int.Parse(objDataReader[DbTableColumn.QualificationId].ToString());
                    objQualificationDetails.Stream = objDataReader[DbTableColumn.Stream].ToString();

                    // Add the object to Collection
                    raveHRCollection.Add(objQualificationDetails);                   
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETQUALIFICATIONDETAILS, EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
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
        /// Deletes the qualification details.
        /// </summary>
        /// <param name="objDeleteQualificationDetails">The object delete qualification details.</param>
        public void DeleteQualificationDetails(BusinessEntities.QualificationDetails objDeleteQualificationDetails)
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
                sqlParam[0] = new SqlParameter(SPParameter.QualificationId, SqlDbType.Int);
                if (objDeleteQualificationDetails.QualificationId== 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objDeleteQualificationDetails.QualificationId;

                sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objDeleteQualificationDetails.EMPId == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objDeleteQualificationDetails.EMPId;

                //Execute SP along with proper parameters
                objDA.ExecuteNonQuerySP(SPNames.Employee_DeleteQualificationDetails, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, DELETEQUALIFICATIONDETAILS, EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        #endregion Public Member Functions
    }
}
