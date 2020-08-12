using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using RMS.Common.DataBase;
using RMS.Common.Constants;
using RMS.Common.ExceptionHandling;

namespace RMS.Common
{
    public class Master
    {

        SqlConnection objConnection = null;
        SqlCommand objCommand = null;
        SqlDataReader objReader;
        private DataAccessClass objDA;
        SqlDataAdapter objDataAdapter;




        /// <summary>
        /// this function fills the dropdown
        /// </summary>
        public DataTable FillDropDownList(string strCategory)
        {
            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                //string ConnStr = Common.DBConstants.GetDBConnectionString();
                int ID = Convert.ToInt32(strCategory);
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();
                objCommand = new SqlCommand("USP_RaveHR_MasterSP", objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.Add("@Category", ID);
                objReader = objCommand.ExecuteReader();
                DataTable objDataTable = new DataTable();
                objDataTable.Load(objReader);
                return objDataTable;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (objReader != null)
                {
                    objReader.Close();
                }
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        public DataTable GetWorkFlowStatus()
        {
            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand("USP_Projects_GetWorkFlowStatus", objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;

                objReader = objCommand.ExecuteReader();
                DataTable objDataTable = new DataTable();
                objDataTable.Load(objReader);
                return objDataTable;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (objReader != null)
                {
                    objReader.Close();
                }
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        /// <summary>
        /// this function fills the Departments/Projects dropdown
        /// </summary>
        public DataTable FillDropDownListProj(string strProject)
        {
            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();
                objCommand = new SqlCommand("USP_MRF_GetProjectNames", objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objReader = objCommand.ExecuteReader();
                DataTable objDataTable = new DataTable();
                objDataTable.Load(objReader);
                return objDataTable;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (objReader != null)
                {
                    objReader.Close();
                }
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        /// <summary>
        /// this function fills the dropdown
        /// </summary>
        //public DataTable FillDropDownListAccountManager(string strCategory)
        //{
        //    try
        //    {
        //        string ConnStr = DBConstants.GetDBConnectionString();
        //        objConnection = new SqlConnection(ConnStr);
        //        objConnection.Open();
        //        objCommand = new SqlCommand(SPNames.Contract_GetEmployeeByDesignation, objConnection);
        //        objCommand.CommandType = CommandType.StoredProcedure;
        //        objCommand.Parameters.Add(SPParameter.Designation, strCategory);
        //        objCommand.Parameters.Add(SPParameter.FirstID, Convert.ToInt32(MasterEnum.FinanceRole.AccountManagerAM));

        //        objReader = objCommand.ExecuteReader();
        //        DataTable objDataTable = new DataTable();
        //        objDataTable.Load(objReader);
        //        return objDataTable;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    finally
        //    {
        //        if (objReader != null)
        //        {
        //            objReader.Close();
        //        }
        //        if (objConnection.State == ConnectionState.Open)
        //        {
        //            objConnection.Close();
        //        }
        //    }

        //}


        /// <summary>
        /// this function gets the employee id.
        /// </summary>
        public int GetEmployeeID(string EmailId)
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;
            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.Contract_GetLoggedInEmployeeId, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = objCommand.Parameters.AddWithValue("@EmailId", EmailId);
                sqlParam[1] = objCommand.Parameters.AddWithValue("@OutEmpId", SqlDbType.Int);
                sqlParam[1].Direction = ParameterDirection.Output;
                //int empId = Convert.ToInt32(sqlParam[1].Value);
                int contract = objCommand.ExecuteNonQuery();
                int empId = Convert.ToInt32(sqlParam[1].Value);
                return empId;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "Projects.cs", "UpdateProject", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }

        }

        ///// <summary>
        ///// Function will fill all the dropdowns
        ///// </summary>
        ///// <param name="category"></param>
        ///// <returns>Collection</returns>
        //public BusinessEntities.RaveHRCollection FillDropDownsDL(int categoryId)
        //{
        //    //Declare DataAccess Class Object
        //    objDA = new DataAccessClass();
        //    SqlParameter[] sqlParam = new SqlParameter[1];

        //    try
        //    {
        //        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
        //        objDA.OpenConnection(DBConstants.GetDBConnectionString());

        //        sqlParam[0] = new SqlParameter(SPParameter.Category, SqlDbType.Int);
        //        sqlParam[0].Value = categoryId;

        //        objReader = objDA.ExecuteReaderSP(SPNames.Master_GetMasterData, sqlParam);

        //        while (objReader.Read())
        //        {
        //            if (objReader.GetValue(1).ToString() != "Abort")
        //            {
        //                KeyValue<string> keyValue = new KeyValue<string>();
        //                keyValue.KeyName = objReader.GetValue(0).ToString();
        //                keyValue.Val = objReader.GetValue(1).ToString();
        //                raveHRCollection.Add(keyValue);
        //            }
        //        }
        //        return raveHRCollection;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    finally
        //    {
        //        if (objReader != null)
        //        {
        //            objReader.Close();
        //        }

        //        objDA.CloseConncetion();
        //    }
        //}
        ////Siddhesh Arekar Issue ID : 55884 Closure Type
        ///// <summary>
        ///// Get Master Type Details
        ///// </summary>
        ///// <param name="category"></param>
        ///// <returns>string</returns>
        //public KeyValue<string> GetMasterTypeDetails(int categoryId, string key)
        //{
        //    //Declare DataAccess Class Object
        //    objDA = new DataAccessClass();
        //    SqlParameter[] sqlParam = new SqlParameter[1];

        //    try
        //    {
        //        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
        //        objDA.OpenConnection(DBConstants.GetDBConnectionString());

        //        sqlParam[0] = new SqlParameter(SPParameter.Category, SqlDbType.Int);
        //        sqlParam[0].Value = categoryId;

        //        objReader = objDA.ExecuteReaderSP(SPNames.Master_GetMasterData, sqlParam);
        //        KeyValue<string> keyValue = new KeyValue<string>();
        //        while (objReader.Read())
        //        {
        //            if (objReader.GetValue(1).ToString() != "Abort")
        //            {
        //                if (Convert.ToString(objReader.GetValue(1)) == key)
        //                {
        //                    keyValue.KeyName = objReader.GetValue(0).ToString();
        //                    keyValue.Val = objReader.GetValue(1).ToString();
        //                    break;
        //                }
        //            }
        //        }
        //        return keyValue;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    finally
        //    {
        //        if (objReader != null)
        //        {
        //            objReader.Close();
        //        }

        //        objDA.CloseConncetion();
        //    }
        //}
        ////Siddhesh Arekar Issue ID : 55884 Closure Type

        //public BusinessEntities.RaveHRCollection FillDepartmentDropDownDL()
        //{
        //    objDA = new DataAccessClass();

        //    try
        //    {
        //        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
        //        objDA.OpenConnection(DBConstants.GetDBConnectionString());
        //        objReader = objDA.ExecuteReaderSP(SPNames.Master_GetDepartment);

        //        while (objReader.Read())
        //        {
        //            KeyValue<string> keyValue = new KeyValue<string>();
        //            keyValue.KeyName = objReader.GetValue(0).ToString();
        //            keyValue.Val = objReader.GetValue(1).ToString();
        //            raveHRCollection.Add(keyValue);
        //        }

        //        return raveHRCollection;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    finally
        //    {
        //        if (objReader != null)
        //        {
        //            objReader.Close();
        //        }

        //        objDA.CloseConncetion();
        //    }
        //}

        //#region Modified By Mohamed Dangra
        //// Mohamed : NIS-RMS : 29/12/2014 : Starts                        			  
        //// Desc : Show Departement for which the person is eligible

        //public BusinessEntities.RaveHRCollection FillEligibleDepartmentDropDownDL(string strCurrentUser)
        //{
        //    objDA = new DataAccessClass();

        //    try
        //    {
        //        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
        //        objDA.OpenConnection(DBConstants.GetDBConnectionString());

        //        SqlParameter[] sqlParam = new SqlParameter[1];

        //        sqlParam[0] = new SqlParameter(SPParameter.EmailId, DbType.String);
        //        sqlParam[0].Value = strCurrentUser;
        //        objReader = objDA.ExecuteReaderSP(SPNames.MRF_GetMRFRaiseAccesForDepartmentByEmpId, sqlParam);

        //        while (objReader.Read())
        //        {
        //            KeyValue<string> keyValue = new KeyValue<string>();
        //            keyValue.KeyName = objReader.GetValue(0).ToString();
        //            keyValue.Val = objReader.GetValue(1).ToString();
        //            raveHRCollection.Add(keyValue);
        //        }

        //        return raveHRCollection;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    finally
        //    {
        //        if (objReader != null)
        //        {
        //            objReader.Close();
        //        }

        //        objDA.CloseConncetion();
        //    }
        //}

        //// Mohamed :  : 29/12/2014 : Ends
        //#endregion Modified By Mohamed Dangra

        ///// <summary>
        ///// Added by Kanchan for the requirment specified in the Discussion with Sawita Kamath and Gaurav Thakkar.
        ///// Requirment raised:
        ///// Gives the emailId for the employee whose Employee id is supplied.
        ///// </summary>
        ///// <param name="empId"></param>
        ///// <returns></returns>
        //public string getEmployeeEmailID(int empId)
        //{
        //    DataAccessClass objDAForMaster = new DataAccessClass();
        //    SqlParameter[] sqlParam = new SqlParameter[1];
        //    BusinessEntities.MRFDetail mrfDetail = new MRFDetail();
        //    string emailID = string.Empty;
        //    DataSet empDetail = new DataSet();

        //    try
        //    {
        //        //Opens the connection.
        //        objDAForMaster.OpenConnection(DBConstants.GetDBConnectionString());
        //        sqlParam[0] = new SqlParameter(SPParameter.EmpId, DbType.Int32);
        //        sqlParam[0].Value = empId;
        //        empDetail = objDAForMaster.GetDataSet(SPNames.Contract_EmpEmailID, sqlParam);
        //        foreach (DataRow dr in empDetail.Tables[0].Rows)
        //        {
        //            emailID = dr[DbTableColumn.EmailId].ToString();
        //        }
        //        return emailID;
        //    }
        //    catch (RaveHRException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "Master", "getEmployeeEmailID", EventIDConstants.RAVE_HR_MASTER_DATA_ACCESS_LAYER);
        //    }
        //    finally
        //    {
        //        objDAForMaster.CloseConncetion();
        //    }
        //}


        ///// <summary>
        ///// Get_s the client abbrivation.
        ///// </summary>
        ///// <param name="MasterId">The master id.</param>
        ///// <returns></returns>
        //public string Get_ClientAbbrivation(int MasterId)
        //{
        //    //Initialise Data Access Class object
        //    objDA = new DataAccessClass();

        //    //Initialise SqlParameter Class object
        //    SqlParameter[] sqlParam = new SqlParameter[1];

        //    string sname = string.Empty;
        //    SqlDataReader objDataReader;

        //    try
        //    {
        //        //Open the connection to DB
        //        objDA.OpenConnection(DBConstants.GetDBConnectionString());

        //        //Check each parameters nullibality and add values to sqlParam object accordingly
        //        sqlParam[0] = new SqlParameter(SPParameter.MasterId, SqlDbType.Int);
        //        if (MasterId == 0)
        //            sqlParam[0].Value = DBNull.Value;
        //        else
        //            sqlParam[0].Value = MasterId;

        //        objDataReader = objDA.ExecuteReaderSP(SPNames.Contracts_GetClientAbbrivation, sqlParam);

        //        while (objDataReader.Read())
        //        {
        //            sname = objDataReader[DbTableColumn.Con_Details].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    finally
        //    {
        //        if (objReader != null)
        //        {
        //            objReader.Close();
        //            objDA.CloseConncetion();
        //        }
        //    }

        //    return sname;
        //}

        ////Ishwar Patil : Trainging Module 29/04/2014 : Starts
        ///// <summary>
        ///// this function fills the dropdown
        ///// </summary>
        //public DataTable FillTraining_DropDownList(string strCategory)
        //{
        //    try
        //    {
        //        string ConnStr = DBConstants.GetDBConnectionString();
        //        objConnection = new SqlConnection(ConnStr);
        //        objConnection.Open();
        //        objCommand = new SqlCommand(SPNames.TNI_GetMasterSP, objConnection);
        //        objCommand.CommandType = CommandType.StoredProcedure;
        //        objCommand.Parameters.Add("@Category", strCategory);
        //        objReader = objCommand.ExecuteReader();
        //        DataTable objDataTable = new DataTable();
        //        objDataTable.Load(objReader);
        //        return objDataTable;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    finally
        //    {
        //        if (objReader != null)
        //        {
        //            objReader.Close();
        //        }
        //        if (objConnection.State == ConnectionState.Open)
        //        {
        //            objConnection.Close();
        //        }
        //    }
        //}
        ////Ishwar Patil : Trainging Module 29/04/2014 : End

        ///// <summary>
        ///// Get Client Name
        ///// </summary>
        ///// <returns>DataSet</returns>
        //public BusinessEntities.RaveHRCollection GetClientNameDL()
        //{
        //    objDA = new DataAccessClass();

        //    try
        //    {
        //        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
        //        objDA.OpenConnection(DBConstants.GetDBConnectionString());
        //        objReader = objDA.ExecuteReaderSP(SPNames.Contracts_GetClientName);

        //        while (objReader.Read())
        //        {
        //            KeyValue<string> keyValue = new KeyValue<string>();
        //            keyValue.KeyName = objReader.GetValue(0).ToString();
        //            keyValue.Val = objReader.GetValue(1).ToString();
        //            raveHRCollection.Add(keyValue);
        //        }

        //        return raveHRCollection;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    finally
        //    {
        //        if (objReader != null)
        //        {
        //            objReader.Close();
        //        }

        //        objDA.CloseConncetion();
        //    }
        //}

        //// Ishwar NISRMS 13032015 Start
        //public BusinessEntities.RaveHRCollection FillDropDownsDLForStatus(int categoryId)
        //{
        //    //Declare DataAccess Class Object
        //    objDA = new DataAccessClass();
        //    SqlParameter[] sqlParam = new SqlParameter[1];

        //    try
        //    {
        //        BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
        //        objDA.OpenConnection(DBConstants.GetDBConnectionString());

        //        sqlParam[0] = new SqlParameter(SPParameter.Category, SqlDbType.Int);
        //        sqlParam[0].Value = categoryId;

        //        objReader = objDA.ExecuteReaderSP(SPNames.Master_GetMasterDataForStatus, sqlParam);

        //        while (objReader.Read())
        //        {
        //            if (objReader.GetValue(1).ToString() != "Abort")
        //            {
        //                KeyValue<string> keyValue = new KeyValue<string>();
        //                keyValue.KeyName = objReader.GetValue(0).ToString();
        //                keyValue.Val = objReader.GetValue(1).ToString();
        //                raveHRCollection.Add(keyValue);
        //            }
        //        }
        //        return raveHRCollection;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    finally
        //    {
        //        if (objReader != null)
        //        {
        //            objReader.Close();
        //        }

        //        objDA.CloseConncetion();
        //    }
        //}
        //// Ishwar NISRMS 13032015 End



        public int GetEmployeeIDByEmailID()
        {                                    
            AuthorizationManager.AuthorizationManager authoriseduser= new AuthorizationManager.AuthorizationManager();                                   
            return GetEmployeeID(authoriseduser.getLoggedInUserEmailId());             
        }




        /// </summary>
        /// This method is used for deleteing the Techinical Training details
        /// <param name="RaiseTraining"></param>
        /// <returns>RaiseTechnicalID</returns>
        public static bool CheckAccess(string username,string controllername,string actionName,out bool isDisabled)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            bool isAccessible = false;
            isDisabled = false;
            SqlParameter[] sqlParam = new SqlParameter[3];
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                sqlParam[0] = new SqlParameter(SPParameter.UserEmpID, SqlDbType.Int);
                sqlParam[0].Value = username;

                sqlParam[1] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                sqlParam[1].Value = controllername;

                sqlParam[2] = new SqlParameter(SPParameter.TrainingType, SqlDbType.Int);
                sqlParam[2].Value = actionName;               

                DataSet ds = new DataSet();
                ds.Clear();
                ds = objGetTraining.GetDataSet(SPNames.USP_RPL_CheckUserAccess, sqlParam);

                if (ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        isAccessible = Convert.ToBoolean(dr[DbTableColumn.IsDisabled]);
                        isDisabled = Convert.ToBoolean(dr[DbTableColumn.IsDisabled]);                                                
                    }
                }

                return isAccessible;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "CheckAccess", "FunctionCheckAccess", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }
        }
    }
}
