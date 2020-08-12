using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using RMS.Common;
using RMS.Common.ExceptionHandling;
using RMS.Common.Constants;

namespace RMS.Common.DataBase
{
     public class DataAccessClass
    {
        #region Member Variables
        private int _commandTimeout = 0;
        private DbProviderFactory _PF = null;
        private DbConnection _CN = null;
        #endregion Member Variables

        #region Constructors
        /// <summary>
        /// The default constructor initializes the provider factory private variable to SqlDbProvider.
        /// </summary>
        public DataAccessClass()
        {
            _commandTimeout = 0;
            _PF = DbProviderFactories.GetFactory("System.Data.SqlClient");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandTimeout"></param>
        public DataAccessClass(int commandTimeout)
        {
            _commandTimeout = commandTimeout;
            _PF = DbProviderFactories.GetFactory("System.Data.SqlClient");
        }

        /// <summary>
        /// The construtor initializes the provider factory private variable to as per the parameter value
        /// </summary>
        /// <param name="dataProvider">The Provider name for example System.Data.SqlClient</param>
        public DataAccessClass(string dataProvider)
        {
            try
            {
                _commandTimeout = 0;
                _PF = DbProviderFactories.GetFactory(dataProvider);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        ///   
        /// </summary>
        /// <param name="dataProvider"></param>
        /// <param name="commandTimeout"></param>
        public DataAccessClass(string dataProvider, int commandTimeout)
        {
            try
            {
                _commandTimeout = commandTimeout;
                _PF = DbProviderFactories.GetFactory(dataProvider);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion Constructors

        /// <summary>
        /// Open the connection
        /// </summary>
        /// <param name="connectionString"></param>
        public void OpenConnection(string connectionString)
        {
            try
            {
                _CN = _PF.CreateConnection();
                _CN.ConnectionString = connectionString;
                _CN.Open();
            }
            catch (SqlException sqlException)
            {
                sqlException.Data.Clear();
                if (sqlException.Number == 4060 || sqlException.Number == 53 || sqlException.Number == 18456)
                {
                    throw new RaveHRException(sqlException.Message, sqlException, Sources.DataAccessLayer, "DataAccessClass.cs", "OpenConnection", EventIDConstants.WRONG_CONNECTIONSTRING);
                }
                else
                {
                    throw new RaveHRException(sqlException.Message, sqlException, Sources.DataAccessLayer, "DataAccessClass.cs", "OpenConnection", EventIDConstants.SQL_EXCEPTION);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "DataAccessClass.cs", "OpenConnection", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

        }

        /// <summary>
        ///  To close connection.
        /// </summary>

        public void CloseConncetion()
        {
            if (_CN != null)
            {
                _CN.Close();
                _CN.Dispose();
            }
        }

        /// <summary>
        ///  To Clear sql pool
        /// </summary>
        public void ClearSQLPool()
        {
            SqlConnection.ClearPool((SqlConnection)_CN);
        }

        /// <summary>
        ///  Execute the sql command with parameter and returns the datareader through 
        ///  which the result can be read.
        /// </summary>
        /// <param name="SQLStatement"></param>
        /// <returns></returns>
        public DbDataReader ExecuteReader(string SQLStatement)
        {
            try
            {
                DbCommand cmd = _PF.CreateCommand();
                cmd.CommandTimeout = _commandTimeout;
                cmd.Connection = _CN;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SQLStatement;
                return cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                throw (new RaveHRException(e.Message, e, "DataAccessClass", "ExecuteReader"));
            }
        }

        /// <summary>
        ///  Execute the stored procedure with parameter and returns the datareader through 
        ///  which the result can be read.
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public SqlDataReader ExecuteReaderSP(string spName, SqlParameter[] parameters)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = _commandTimeout;
                cmd.Connection = (SqlConnection)_CN;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw (new RaveHRException("Unable to execute following store procedure " + spName, ex, "DataAccessClass", "ExecuteReaderSP"));
            }
        }

        /// <summary>
        /// Executes the stored procudere and returns the SqlDataReader
        /// </summary>
        /// <param name="spName"></param>
        /// <returns></returns>
        public SqlDataReader ExecuteReaderSP(string spName)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = _commandTimeout;
                cmd.Connection = (SqlConnection)_CN;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                return cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw (new RaveHRException("Unable to execute stored procedure " + spName, ex, "DataAccessClass", "ExecuteReaderSP"));
            }
        }
        /// <summary>
        /// It uses the CommandBehavior.CloseConnection while executing the ExecuteReader method
        /// </summary>
        /// <param name="sqlStatement">SQL statement to be executed</param>
        /// <returns>SqlDataReader and the connection will be closed on calling the Close method</returns>
        public SqlDataReader ExecuteSqlReader(string sqlStatement)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = _commandTimeout;
                cmd.Connection = (SqlConnection)_CN;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlStatement;
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw (new RaveHRException("Unable to execute following SQL statement" + sqlStatement, ex, "DataAccessClass", "ExecuteSqlReader"));
            }

        }

        /// <summary>
        /// Execute the sql statement and returns the first column of 
        /// the first row in the result set returned by the query. 
        /// Extra columns or rows are ignored.
        /// </summary>
        /// <param name="SQLStatement"></param>
        /// <returns></returns>
        public object ExecuteScalarTSQL(string SQLStatement)
        {
            try
            {
                DbCommand cmd = _PF.CreateCommand();
                cmd.CommandTimeout = _commandTimeout;
                cmd.Connection = _CN;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SQLStatement;
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw (new RaveHRException("Unable to execute following SQL statement" + SQLStatement, ex, "DataAccessClass", "ExecuteScalarTSQL"));
            }

        }

        /// <summary>
        ///  Execute the sql statement and returns the results in a new dataset.
        /// </summary>
        /// <param name="SQLStatement"></param>
        /// <returns></returns>
        public DataSet ExecuteQueryTSQL(string SQLStatement)
        {
            try
            {
                DbCommand cmd = _PF.CreateCommand();
                cmd.CommandTimeout = _commandTimeout;
                cmd.Connection = _CN;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SQLStatement;
                DataSet ds = new DataSet();
                DbDataAdapter da = _PF.CreateDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw (new RaveHRException(ex.Message, ex, "DataAccessClass", "ExecuteQueryTSQL"));
            }
        }

        /// <summary>
        ///   Execute the sql statement and returns the number of rows affected
        /// </summary>
        /// <param name="SQLStatement"></param>
        public void ExecuteNonQueryTSQL(string SQLStatement)
        {
            try
            {
                DbCommand cmd = _PF.CreateCommand();
                cmd.CommandTimeout = _commandTimeout;
                cmd.Connection = _CN;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SQLStatement;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw (new RaveHRException(e.Message, e, "DataAccessClass", "ExecuteNonQueryTSQL"));
            }
        }

        /// <summary>
        /// Execute the stored procedure with array of parameter and returns the first column of 
        /// the first row in the result set returned by the query. 
        /// Extra columns or rows are ignored.
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExecuteScalarSP(string spName, SqlParameter[] parameters)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = _commandTimeout;
                cmd.Connection = (SqlConnection)_CN;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw (new RaveHRException(e.Message, e, "DataAccessClass", "ExecuteScalarSP"));
            }
        }

        /// <summary>
        /// Execute the stored procedure and returns the first column of 
        /// the first row in the result set returned by the query. 
        /// Extra columns or rows are ignored.
        /// </summary>
        /// <param name="spName"></param>
        /// <returns> returns the first column of 
        /// the first row in the result set returned by the query. 
        /// Extra columns or rows are ignored</returns>
        public object ExecuteScalarSP(string spName)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = _commandTimeout;
                cmd.Connection = (SqlConnection)_CN;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                return cmd.ExecuteScalar();
            }
            catch (Exception e)
            {
                throw (new RaveHRException(e.Message, e, "DataAccessClass", "ExecuteNonQuerySP"));
            }
        }


        /// <summary>
        ///  Execute the stored procedure and returns the number of rows affected.
        /// </summary>
        /// <param name="spName"></param>
        /// <returns>The number of rows affected.</returns>
        public int ExecuteNonQuerySP(string spName)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = _commandTimeout;
                cmd.Connection = (SqlConnection)_CN;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw (new RaveHRException(e.Message, e, "DataAccessClass", "ExecuteNonQuerySP"));
            }
        }

        /// <summary>
        ///     Execute the stored procedure with array of parameters and returns the number of rows affected.
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteNonQuerySP(string spName, SqlParameter[] parameters)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = _commandTimeout;
                cmd.Connection = (SqlConnection)_CN;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw (new RaveHRException(e.Message, e, "DataAccessClass", "ExecuteNonQuerySP"));
            }
        }

        /// <summary>
        /// Execute the stored procedure and returns the number of rows affected.
        /// </summary>
        /// <param name="spName"> stored procedure</param>
        /// <param name="param"></param>
        /// <returns>The number of rows affected</returns>
        public int ExecuteNonQuerySP(string spName, SqlParameter param)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = _commandTimeout;
                cmd.Connection = (SqlConnection)_CN;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                cmd.Parameters.Add(param);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw (new RaveHRException(e.Message, e, "DataAccessClass", "ExecuteNonQuerySP"));
            }
            finally
            {
                //--Close connection
                CloseConncetion();
            }
        }

        /// <summary>
        ///   Executes the commandtext and returns the results in a new dataset. 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <returns> A dataset with the result of the command text.</returns>
        public DataSet GetDataSet(string commandText, CommandType commandType)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = _commandTimeout;
                DbDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                cmd.Connection = (SqlConnection)_CN;
                cmd.CommandType = commandType;
                cmd.CommandText = commandText;
                da.SelectCommand = cmd;
                da.Fill(ds);
                return ds;
            }
            catch (Exception e)
            {
                throw (new RaveHRException(e.Message, e, "DataAccessClass", "GetDataSet"));
            }
        }

        public DataSet GetDataSet(string commandText, CommandType commandType, MissingSchemaAction action)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = _commandTimeout;
                DbDataAdapter da = new SqlDataAdapter();
                da.MissingSchemaAction = action;
                DataSet ds = new DataSet();
                cmd.Connection = (SqlConnection)_CN;
                cmd.CommandType = commandType;
                cmd.CommandText = commandText;
                da.SelectCommand = cmd;
                da.Fill(ds);
                return ds;
            }
            catch (Exception e)
            {
                throw (new RaveHRException(e.Message, e, "DataAccessClass", "GetDataSet"));
            }
        }

        /// <summary>
        ///   Executes the stored procedure and returns the results in a new dataset. 
        /// </summary>
        /// <param name="spName">The stored procedure to execute.</param>
        /// <returns> A dataset with the result of the stored procedure</returns>
        public DataSet GetDataSet(string spName)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = _commandTimeout;
                DbDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                cmd.Connection = (SqlConnection)_CN;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                da.SelectCommand = cmd;
                da.Fill(ds);
                return ds;
            }
            catch (Exception e)
            {
                throw (new RaveHRException(e.Message, e, "DataAccessClass", "GetDataSet"));
            }
        }

        /// <summary>
        ///  Executes the stored procedure with array of parameter and returns the results in a new dataset.
        /// </summary>
        /// <param name="spName">The stored procedure to execute.</param>
        /// <param name="parameters">An array of paramters to pass to the stored procedure.</param>
        /// <returns></returns>
        public DataSet GetDataSet(string spName, SqlParameter[] parameters)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = _commandTimeout;
                DbDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                cmd.Connection = (SqlConnection)_CN;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                cmd.Parameters.AddRange(parameters);
                da.SelectCommand = cmd;
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "DataAccessClass.cs", "GetDataSet", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (_CN.State == ConnectionState.Open)
                {
                    CloseConncetion();
                }
            }
        }

        /// <summary>
        ///     Executes the stored procedure with single parameter and returns the results in a new dataset.
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string spName, SqlParameter param)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = _commandTimeout;
            DbDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                cmd.Connection = (SqlConnection)_CN;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                cmd.Parameters.Add(param);
                da.SelectCommand = cmd;
                da.Fill(ds);
                cmd.Parameters.Clear();
                return ds;
            }
            catch (Exception e)
            {
                throw (new RaveHRException(e.Message, e, "DataAccessClass", "GetDataSet"));
            }
        }

        /// <summary>
        ///   This method is used to update Staging Area table
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="updateCommand"></param>
        /// <param name="sqlparams"></param>
        /// <returns></returns>
        public Boolean UpdateStagingAreaTable(DataTable dt, string updateCommand, SqlParameter[] sqlparams)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand(updateCommand, (SqlConnection)_CN);
                cmd.CommandTimeout = _commandTimeout;
                cmd.Parameters.AddRange(sqlparams);
                da.UpdateCommand = cmd;
                da.Update(dt);
                return true;
            }
            catch (Exception e)
            {
                throw (new RaveHRException(e.Message, e, "DataAccessClass", "UpdateStagingAreaTable"));
            }
        }

        public Boolean UpdateStagingAreaTable(DataTable dt, string updateCommand, CommandType commandType, SqlParameter[] sqlparams)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand(updateCommand, (SqlConnection)_CN);
                cmd.CommandTimeout = _commandTimeout;
                cmd.CommandType = commandType;
                cmd.Parameters.AddRange(sqlparams);
                da.UpdateCommand = cmd;
                da.Update(dt);
                return true;
            }
            catch (Exception e)
            {
                throw (new RaveHRException(e.Message, e, "DataAccessClass", "UpdateStagingAreaTable"));
            }
        }

        //Rakesh

        /// <summary>
        /// Execute the stored procedure with array of parameter and returns the first column of 
        /// the first row in the result set returned by the query. 
        /// Extra columns or rows are ignored.
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExecuteScalarSP_WithConnection(string spName, SqlCommand cmd)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection())
                {
                    sqlcon.ConnectionString = DBConstants.GetDBConnectionString();
                    // SqlCommand cmd = new SqlCommand();
                    sqlcon.Open();
                    cmd.CommandTimeout = _commandTimeout;
                    cmd.Connection = sqlcon;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = spName;
                    //cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception e)
            {
                throw (new RaveHRException(e.Message, e, "DataAccessClass", "ExecuteScalarSP"));
            }
        }

         

        /// <summary>
        /// Executes the stored procudere and returns the SqlDataReader
        /// </summary>
        /// <param name="spName"></param>
        /// <returns></returns>
        public List<T> ExecuteReaderSP_WithConnection<T>(string spName,SqlCommand cmd) where T : class, new()
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection())
                {
                    sqlcon.ConnectionString = DBConstants.GetDBConnectionString();
                    sqlcon.Open();
                   // SqlCommand cmd = new SqlCommand();
                    cmd.CommandTimeout = _commandTimeout;
                    cmd.Connection = sqlcon;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = spName;
                    return cmd.ExecuteReader().SqlReaderToList<T>();
                }
            }
            catch (Exception ex)
            {
                throw (new RaveHRException("Unable to execute stored procedure " + spName, ex, "DataAccessClass", "ExecuteReaderSP"));
            }
        }


    }
}
