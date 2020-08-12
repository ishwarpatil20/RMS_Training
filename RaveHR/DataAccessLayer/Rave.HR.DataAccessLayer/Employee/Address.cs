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
    public class Address
    {
        #region Private Member Variables

        /// <summary>
        /// private variable for Class Name used in each catch block
        /// </summary>
        private string CLASS_NAME = "Address";

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
        private BusinessEntities.Address objAddress;

        /// <summary>
        /// private object for RaveHRCollection
        /// </summary>
        private BusinessEntities.RaveHRCollection raveHRCollection;

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string ADDADDRESS = "AddAddress";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string GETADDRESS = "GetEmployeeAddress";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string UPDATEADDRESS = "UpdateAddress";

        #endregion Private Member Variables

        #region Public Member Function

        /// <summary>
        /// Adds the Address.
        /// </summary>
        /// <param name="objAddEmployee">The object add employee.</param>
        public int AddAddress(BusinessEntities.Address objAddress)
        {
            int empID = 0;
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[13];

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                    if (objAddress.EMPId == 0)
                        return 0;
                    else
                        sqlParam[0].Value = objAddress.EMPId;

                    sqlParam[1] = new SqlParameter(SPParameter.Address, SqlDbType.NVarChar, 50);
                    if (objAddress.Addres == "" || objAddress.Addres == null)
                        sqlParam[1].Value = DBNull.Value;
                    else
                        sqlParam[1].Value = objAddress.Addres;

                    sqlParam[2] = new SqlParameter(SPParameter.FlatNo, SqlDbType.NVarChar, 50);
                    if (objAddress.FlatNo == "" || objAddress.FlatNo == null)
                        sqlParam[2].Value = DBNull.Value;
                    else
                        sqlParam[2].Value = objAddress.FlatNo;

                    sqlParam[3] = new SqlParameter(SPParameter.BuildingName, SqlDbType.NVarChar, 50);
                    if (objAddress.BuildingName == "" || objAddress.BuildingName == null)
                        sqlParam[3].Value = DBNull.Value;
                    else
                        sqlParam[3].Value = objAddress.BuildingName;

                    sqlParam[4] = new SqlParameter(SPParameter.Street, SqlDbType.NVarChar, 50);
                    if (objAddress.Street == "" || objAddress.Street == null)
                        sqlParam[4].Value = DBNull.Value;
                    else
                        sqlParam[4].Value = objAddress.Street;

                    sqlParam[5] = new SqlParameter(SPParameter.LandMark, SqlDbType.NVarChar, 50);
                    if (objAddress.Landmark == "" || objAddress.Landmark == null)
                        sqlParam[5].Value = DBNull.Value;
                    else
                        sqlParam[5].Value = objAddress.Landmark;

                    sqlParam[6] = new SqlParameter(SPParameter.City, SqlDbType.NVarChar, 50);
                    if (objAddress.City == "" || objAddress.City == null)
                        sqlParam[6].Value = DBNull.Value;
                    else
                        sqlParam[6].Value = objAddress.City;

                    sqlParam[7] = new SqlParameter(SPParameter.State, SqlDbType.NVarChar, 50);
                    if (objAddress.State == "" || objAddress.State == null)
                        sqlParam[7].Value = DBNull.Value;
                    else
                        sqlParam[7].Value = objAddress.State;

                    sqlParam[8] = new SqlParameter(SPParameter.Country, SqlDbType.NVarChar, 50);
                    if (objAddress.Country == "" || objAddress.Country == null)
                        sqlParam[8].Value = DBNull.Value;
                    else
                        sqlParam[8].Value = objAddress.Country;


                    sqlParam[9] = new SqlParameter(SPParameter.PinCode, SqlDbType.NVarChar, 6);
                    if (objAddress.Pincode == "" || objAddress.Pincode == null)
                        sqlParam[9].Value = DBNull.Value;
                    else
                        sqlParam[9].Value = objAddress.Pincode;

                    
                    sqlParam[10] = new SqlParameter(SPParameter.CreatedById, SqlDbType.NVarChar, 50);
                    if (objAddress.CreatedById == "" || objAddress.CreatedById == null)
                        return 0;
                    else
                        sqlParam[10].Value = objAddress.CreatedById;


                    sqlParam[11] = new SqlParameter(SPParameter.CreatedDate, SqlDbType.SmallDateTime);
                    sqlParam[11].Value = DateTime.Today;

                    
                    sqlParam[12] = new SqlParameter(SPParameter.AddressType, SqlDbType.Int);
                    if (objAddress.AddressType == 0)
                        sqlParam[12].Value = 0;
                    else
                        sqlParam[12].Value = objAddress.AddressType;

                    objDA.ExecuteNonQuerySP(SPNames.Employee_AddAddress, sqlParam);
                   
                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, ADDADDRESS, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
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
        public BusinessEntities.RaveHRCollection GetEmployeeAddress(BusinessEntities.Employee objEmployee)
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
                    if (objEmployee.EMPId == 0)
                        sqlParam[0].Value = DBNull.Value;
                    else
                        sqlParam[0].Value = objEmployee.EMPId;

                    objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetAddress, sqlParam);

                    while (objDataReader.Read())
                    {
                        //Initialise the Business Entity object
                        objAddress = new BusinessEntities.Address();

                        objAddress.EmployeeAddressId = Convert.ToInt32(objDataReader[DbTableColumn.EmployeeAddressId].ToString());
                        objAddress.EMPId = Convert.ToInt32(objDataReader[DbTableColumn.EMPId].ToString());
                        objAddress.Addres = objDataReader[DbTableColumn.Address].ToString();
                        objAddress.FlatNo = objDataReader[DbTableColumn.FlatNo].ToString();
                        objAddress.BuildingName = objDataReader[DbTableColumn.BuildingName].ToString();
                        objAddress.Street = objDataReader[DbTableColumn.Street].ToString();
                        objAddress.Landmark = objDataReader[DbTableColumn.LandMark].ToString();
                        objAddress.State = objDataReader[DbTableColumn.State].ToString();
                        objAddress.Country = objDataReader[DbTableColumn.Country].ToString();
                        objAddress.City = objDataReader[DbTableColumn.City].ToString();
                        objAddress.Pincode = objDataReader[DbTableColumn.PinCode].ToString();
                        objAddress.AddressType = int.Parse(objDataReader[DbTableColumn.AddressType].ToString());

                        // Add the object to Collection
                        raveHRCollection.Add(objAddress);
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
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETADDRESS, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
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
        /// Updates the Address.
        /// </summary>
        /// <param name="objAddEmployee">The object add employee.</param>
        public int UpdateAddress(BusinessEntities.Address objAddress)
        {
            int empID = 0;
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[55];

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                    if (objAddress.EMPId == 0)
                        return 0;
                    else
                        sqlParam[0].Value = objAddress.EMPId;

                    sqlParam[1] = new SqlParameter(SPParameter.EmployeeAddressId, SqlDbType.Int);
                    if (objAddress.EmployeeAddressId == 0)
                        return 0;
                    else
                        sqlParam[1].Value = objAddress.EmployeeAddressId;

                    sqlParam[2] = new SqlParameter(SPParameter.Address, SqlDbType.NVarChar, 50);
                    if (objAddress.Addres == "" || objAddress.Addres == null)
                        sqlParam[2].Value = DBNull.Value;
                    else
                        sqlParam[2].Value = objAddress.Addres;

                    sqlParam[3] = new SqlParameter(SPParameter.FlatNo, SqlDbType.NVarChar, 50);
                    if (objAddress.FlatNo == "" || objAddress.FlatNo == null)
                        sqlParam[3].Value = DBNull.Value;
                    else
                        sqlParam[3].Value = objAddress.FlatNo;

                    sqlParam[4] = new SqlParameter(SPParameter.BuildingName, SqlDbType.NVarChar, 50);
                    if (objAddress.BuildingName == "" || objAddress.BuildingName == null)
                        sqlParam[4].Value = DBNull.Value;
                    else
                        sqlParam[4].Value = objAddress.BuildingName;

                    sqlParam[5] = new SqlParameter(SPParameter.Street, SqlDbType.NVarChar, 50);
                    if (objAddress.Street == "" || objAddress.Street == null)
                        sqlParam[5].Value = DBNull.Value;
                    else
                        sqlParam[5].Value = objAddress.Street;

                    sqlParam[6] = new SqlParameter(SPParameter.LandMark, SqlDbType.NVarChar, 50);
                    if (objAddress.Landmark == "" || objAddress.Landmark == null)
                        sqlParam[6].Value = DBNull.Value;
                    else
                        sqlParam[6].Value = objAddress.Landmark;

                    sqlParam[7] = new SqlParameter(SPParameter.City, SqlDbType.NVarChar, 50);
                    if (objAddress.City == "" || objAddress.City == null)
                        sqlParam[7].Value = DBNull.Value;
                    else
                        sqlParam[7].Value = objAddress.City;

                    sqlParam[8] = new SqlParameter(SPParameter.State, SqlDbType.NVarChar, 50);
                    if (objAddress.State == "" || objAddress.State == null)
                        sqlParam[8].Value = DBNull.Value;
                    else
                        sqlParam[8].Value = objAddress.State;

                    sqlParam[9] = new SqlParameter(SPParameter.Country, SqlDbType.NVarChar, 50);
                    if (objAddress.Country == "" || objAddress.Country == null)
                        sqlParam[9].Value = DBNull.Value;
                    else
                        sqlParam[9].Value = objAddress.Country;


                    sqlParam[10] = new SqlParameter(SPParameter.PinCode, SqlDbType.NVarChar, 6);
                    if (objAddress.Pincode == "" || objAddress.Pincode == null)
                        sqlParam[10].Value = DBNull.Value;
                    else
                        sqlParam[10].Value = objAddress.Pincode;

                    sqlParam[11] = new SqlParameter(SPParameter.AddressType, SqlDbType.Int);
                    if (objAddress.AddressType == 0)
                        sqlParam[11].Value = 0;
                    else
                        sqlParam[11].Value = objAddress.AddressType;

                    int UpdateAddress = objDA.ExecuteNonQuerySP(SPNames.Employee_UpdateAddress, sqlParam);

                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, UPDATEADDRESS, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
            return empID;
        }

        /// <summary>
        /// Manipulates the address.
        /// </summary>
        /// <param name="objAddress">The obj address.</param>
        /// <returns></returns>
        public int ManipulateAddress(BusinessEntities.Address objAddress)
        {
            int empID = 0;
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[17];

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                    if (objAddress.EMPId == 0)
                        return 0;
                    else
                        sqlParam[0].Value = objAddress.EMPId;

                    sqlParam[1] = new SqlParameter(SPParameter.EmployeeAddressId, SqlDbType.Int);
                    if (objAddress.EmployeeAddressId == 0)
                        sqlParam[1].Value = DBNull.Value;
                    else
                        sqlParam[1].Value = objAddress.EmployeeAddressId;

                    sqlParam[2] = new SqlParameter(SPParameter.Address, SqlDbType.NVarChar, 50);
                    if (objAddress.Addres == "" || objAddress.Addres == null)
                        sqlParam[2].Value = DBNull.Value;
                    else
                        sqlParam[2].Value = objAddress.Addres;

                    sqlParam[3] = new SqlParameter(SPParameter.FlatNo, SqlDbType.NVarChar, 50);
                    if (objAddress.FlatNo == "" || objAddress.FlatNo == null)
                        sqlParam[3].Value = DBNull.Value;
                    else
                        sqlParam[3].Value = objAddress.FlatNo;

                    sqlParam[4] = new SqlParameter(SPParameter.BuildingName, SqlDbType.NVarChar, 50);
                    if (objAddress.BuildingName == "" || objAddress.BuildingName == null)
                        sqlParam[4].Value = DBNull.Value;
                    else
                        sqlParam[4].Value = objAddress.BuildingName;

                    sqlParam[5] = new SqlParameter(SPParameter.Street, SqlDbType.NVarChar, 50);
                    if (objAddress.Street == "" || objAddress.Street == null)
                        sqlParam[5].Value = DBNull.Value;
                    else
                        sqlParam[5].Value = objAddress.Street;

                    sqlParam[6] = new SqlParameter(SPParameter.LandMark, SqlDbType.NVarChar, 50);
                    if (objAddress.Landmark == "" || objAddress.Landmark == null)
                        sqlParam[6].Value = DBNull.Value;
                    else
                        sqlParam[6].Value = objAddress.Landmark;

                    sqlParam[7] = new SqlParameter(SPParameter.City, SqlDbType.NVarChar, 50);
                    if (objAddress.City == "" || objAddress.City == null)
                        sqlParam[7].Value = DBNull.Value;
                    else
                        sqlParam[7].Value = objAddress.City;

                    sqlParam[8] = new SqlParameter(SPParameter.State, SqlDbType.NVarChar, 50);
                    if (objAddress.State == "" || objAddress.State == null)
                        sqlParam[8].Value = DBNull.Value;
                    else
                        sqlParam[8].Value = objAddress.State;

                    sqlParam[9] = new SqlParameter(SPParameter.Country, SqlDbType.NVarChar, 50);
                    if (objAddress.Country == "" || objAddress.Country == null)
                        sqlParam[9].Value = DBNull.Value;
                    else
                        sqlParam[9].Value = objAddress.Country;


                    sqlParam[10] = new SqlParameter(SPParameter.PinCode, SqlDbType.NVarChar, 6);
                    if (objAddress.Pincode == "" || objAddress.Pincode == null)
                        sqlParam[10].Value = DBNull.Value;
                    else
                        sqlParam[10].Value = objAddress.Pincode;


                    sqlParam[11] = new SqlParameter(SPParameter.CreatedById, SqlDbType.NVarChar, 50);
                    if (objAddress.CreatedById == "" || objAddress.CreatedById == null)
                        return 0;
                    else
                        sqlParam[11].Value = objAddress.CreatedById;


                    sqlParam[12] = new SqlParameter(SPParameter.CreatedDate, SqlDbType.SmallDateTime);
                    sqlParam[12].Value = DateTime.Today;


                    sqlParam[13] = new SqlParameter(SPParameter.AddressType, SqlDbType.Int);
                    if (objAddress.AddressType == 0)
                        sqlParam[13].Value = 0;
                    else
                        sqlParam[13].Value = objAddress.AddressType;

                    sqlParam[14] = new SqlParameter(SPParameter.LastModifiedById, SqlDbType.NVarChar, 50);
                    if (objAddress.LastModifiedById == "" || objAddress.LastModifiedById == null)
                        return 0;
                    else
                        sqlParam[14].Value = objAddress.LastModifiedById;


                    sqlParam[15] = new SqlParameter(SPParameter.LastModifiedDate, SqlDbType.SmallDateTime);
                    sqlParam[15].Value = DateTime.Today;

                    sqlParam[16] = new SqlParameter(SPParameter.IsActive, SqlDbType.Bit);
                    if (objAddress.IsActive == false)
                        sqlParam[16].Value = 0;
                    else
                        sqlParam[16].Value = 1;

                    objDA.ExecuteNonQuerySP(SPNames.Employee_ManipulateAddress, sqlParam);

                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, ADDADDRESS, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
            return empID;
        }        

        #endregion
    }
}
