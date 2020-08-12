//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           Address.cs       
//  Author:         sudip.guha
//  Date written:   16/11/2009/ 06:17:30 PM
//  Description:    This class  provides the Data Access layer methods for Address in  Employee module.
//                  
//
//  Amendments
//  Date                        Who             Ref     Description
//  ----                        -----------     ---     -----------
//  14/08/2009/ 06:17:30 PM     sudip.guha      n/a     Created 
//
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Data.SqlClient;
using Common;
using Common.Constants;
using System.Transactions;

namespace Rave.HR.DataAccessLayer.Employees
{
    public class EmergencyContact
    {
        #region Private Member Variables

        /// <summary>
        /// private variable for Class Name used in each catch block
        /// </summary>
        private string CLASS_NAME = "EmergencyContact";

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
        private BusinessEntities.EmergencyContact objEmergencyContact;

        /// <summary>
        /// private object for RaveHRCollection
        /// </summary>
        private BusinessEntities.RaveHRCollection raveHRCollection;

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string ADDEMERGENCYCONTACT = "AddEmergencyContact";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string GETEMERGENCYCONTACT = "GetEmergencyContact";
   
        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string UPDATEEMERGENCYCONTACT = "UpdateEmergencyContact";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string DELETEEMERGENCYCONTACT = "DeleteEmergencyContact";


        #endregion Private Member Variables

        #region Public Member Function

        /// <summary>
        /// Adds the EmergencyContact.
        /// </summary>
        /// <param name="objAddEmployee">The object add employee.</param>
        public int AddEmergencyContact(BusinessEntities.EmergencyContact objEmergencyContact)
        {
            int empID = 0;
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[4];

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                    if (objEmergencyContact.EMPId == 0)
                        return 0;
                    else
                        sqlParam[0].Value = objEmergencyContact.EMPId;

                    sqlParam[1] = new SqlParameter(SPParameter.ContactName, SqlDbType.NVarChar, 50);
                    if (objEmergencyContact.ContactName == "" || objEmergencyContact.ContactName == null)
                        sqlParam[1].Value = DBNull.Value;
                    else
                        sqlParam[1].Value = objEmergencyContact.ContactName;

                    sqlParam[2] = new SqlParameter(SPParameter.ContactNumber, SqlDbType.NVarChar, 50);
                    if (objEmergencyContact.ContactNumber == "" || objEmergencyContact.ContactNumber == null)
                        sqlParam[2].Value = DBNull.Value;
                    else
                        sqlParam[2].Value = objEmergencyContact.ContactNumber;

                    sqlParam[3] = new SqlParameter(SPParameter.Relation, SqlDbType.Int);
                    if (objEmergencyContact.RelationType == 0)
                        sqlParam[3].Value = DBNull.Value;
                    else
                        sqlParam[3].Value = objEmergencyContact.RelationType;


                    objDA.ExecuteNonQuerySP(SPNames.Employee_AddEmergencyContact, sqlParam);

                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, ADDEMERGENCYCONTACT, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
            return empID;
        }        
        
        /// <summary>
        /// Gets the Employee Address.
        /// </summary>
        /// <param name="objGetCertificationDetails">The object get certification details.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetEmergencyContact(BusinessEntities.EmergencyContact objGetEmergencyContact)
        {
            //Initialise Data Access Class object
            objDA = new DataAccessClass();

            //Initialise SqlParameter Class object
            sqlParam = new SqlParameter[1];

            //Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    //Open the connection to DB
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    //Check each parameters nullibality and add values to sqlParam object accordingly
                    sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                    if (objGetEmergencyContact.EMPId == 0)
                        sqlParam[0].Value = DBNull.Value;
                    else
                        sqlParam[0].Value = objGetEmergencyContact.EMPId;

                    objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetEmergencyContact, sqlParam);

                    while (objDataReader.Read())
                    {
                        //Initialise the Business Entity object
                        objEmergencyContact = new BusinessEntities.EmergencyContact();

                        objEmergencyContact.EmergencyContactId = Convert.ToInt32(objDataReader[DbTableColumn.EmergencyContactId].ToString());
                        objEmergencyContact.EMPId = Convert.ToInt32(objDataReader[DbTableColumn.EMPId].ToString());
                        objEmergencyContact.ContactName = objDataReader[DbTableColumn.ContactName].ToString();
                        objEmergencyContact.ContactNumber = objDataReader[DbTableColumn.ContactNumber].ToString();
                        objEmergencyContact.Relation = objDataReader[DbTableColumn.Relation].ToString();
                        objEmergencyContact.RelationType = int.Parse(objDataReader[DbTableColumn.RelationType].ToString());

                        // Add the object to Collection
                        raveHRCollection.Add(objEmergencyContact);
                    }

                    if (!objDataReader.IsClosed)
                    {
                        objDataReader.Close();
                    }

                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETEMERGENCYCONTACT, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (!objDataReader.IsClosed)
                {
                    objDataReader.Close();
                }
                objDA.CloseConncetion();
            }

            // Return the Collection
            return raveHRCollection;

        }

        /// <summary>
        /// Updates the emergency contact.
        /// </summary>
        /// <param name="objUpdateEmergencyContact">The obj update emergency contact.</param>
        public void UpdateEmergencyContact(BusinessEntities.EmergencyContact objEmergencyContact)
        {
            //Initialise Data Access Class object
            objDA = new DataAccessClass();

            //Initialise SqlParameter Class object
            sqlParam = new SqlParameter[5];

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Check each parameters nullibality and add values to sqlParam object accordingly
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objEmergencyContact.EMPId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objEmergencyContact.EMPId;

                sqlParam[1] = new SqlParameter(SPParameter.EmergencyContactId, SqlDbType.Int);
                if (objEmergencyContact.EmergencyContactId == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objEmergencyContact.EmergencyContactId;

                sqlParam[2] = new SqlParameter(SPParameter.ContactName, SqlDbType.NVarChar, 50);
                if (objEmergencyContact.ContactName == "" || objEmergencyContact.ContactName == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objEmergencyContact.ContactName;

                sqlParam[3] = new SqlParameter(SPParameter.ContactNumber, SqlDbType.NVarChar, 50);
                if (objEmergencyContact.ContactNumber == "" || objEmergencyContact.ContactNumber == null)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = objEmergencyContact.ContactNumber;

                sqlParam[4] = new SqlParameter(SPParameter.Relation, SqlDbType.Int);
                if (objEmergencyContact.RelationType == 0)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = objEmergencyContact.RelationType;

                //Execute SP along with proper parameters
                objDA.ExecuteNonQuerySP(SPNames.Employee_UpdateEmergencyContact, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, UPDATEEMERGENCYCONTACT, EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Deletes the emergency contact.
        /// </summary>
        /// <param name="objDeleteEmergencyContact">The obj delete emergency contact.</param>
        public void DeleteEmergencyContact(BusinessEntities.EmergencyContact objDeleteEmergencyContact)
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
                sqlParam[0] = new SqlParameter(SPParameter.EmergencyContactId, SqlDbType.Int);
                if (objDeleteEmergencyContact.EmergencyContactId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objDeleteEmergencyContact.EmergencyContactId;

                sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objDeleteEmergencyContact.EMPId == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objDeleteEmergencyContact.EMPId;

                //Execute SP along with proper parameters
                objDA.ExecuteNonQuerySP(SPNames.Employee_DeleteEmergencyContact, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, DELETEEMERGENCYCONTACT, EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        #endregion
    }
}
