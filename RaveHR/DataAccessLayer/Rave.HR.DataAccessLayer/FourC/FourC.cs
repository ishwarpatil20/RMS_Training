using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Common;
using Common.Constants;
using System.Transactions;
using BusinessEntities;

namespace Rave.HR.DataAccessLayer.FourC
{
    public class FourC
    {
        #region Private Member Variables
        /// <summary>
        /// private variable for Data Access Class
        /// </summary>
        private DataAccessClass objDA;

        /// <summary>
        /// private array variable for Sql paramaters
        /// </summary>
        private SqlParameter[] sqlParam;

        /// <summary>
        /// private variable for Data Reader class
        /// </summary>
        private SqlDataReader objDataReader;

        /// <summary>
        /// private variable for Class Name used in each catch block
        /// </summary>
        private string CLASS_NAME = "FourC";

        SqlConnection objConnection = null;
        SqlCommand objCommand = null;
        SqlDataReader objReader;
        //private DataAccessClass objDA;
        //SqlDataAdapter objDataAdapter;


        #endregion Private Member Variables

        

        /// <summary>
        /// Gets the Reporting to Ids 
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet GetCreatorApproverDetails()
        {

            DataSet ds = null;
            try
            {
                //SqlParameter[] sqlParam = new SqlParameter[2];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                ds = new DataSet();

                //objCommand = new SqlCommand(SPNames.GET_REPORTING_FUNCTIONAL_MANAGER_IDS, objConnection);
                //objCommand.CommandType = CommandType.StoredProcedure;

                //objCommand.Parameters.AddWithValue(SPParameter.EmpId, EMPId);

                //sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.VarChar, 100);
                //if (EMPId == null)
                //    sqlParam[0].Value = DBNull.Value;
                //else
                //    sqlParam[0].Value = EMPId;

                //sqlParam[1] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                //if (objParameter.SortExpressionAndDirection == null)
                //    sqlParam[1].Value = DBNull.Value;
                //else
                //    sqlParam[1].Value = objParameter.SortExpressionAndDirection;

                //SqlDataAdapter objDataAdapter = new SqlDataAdapter(objCommand);
                //objDataAdapter.Fill(ds);

                ds = objDA.GetDataSet(SPNames.GET_Creator_Reviewer_Details);
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetCreatorApproverDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
            return ds;
        }


        /// <summary>
        /// Gets the Reporting to Ids 
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet GetViewAccessRightsData()
        {

            DataSet ds = null;
            try
            {
                //SqlParameter[] sqlParam = new SqlParameter[2];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                ds = new DataSet();
                ds = objDA.GetDataSet(SPNames.GET_ViewAccessRightsData);
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetCreatorApproverDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
            return ds;
        }


        /// <summary>
        /// Gets the Reporting to Ids 
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet Get4CEmployeeDeatils(int loginEmpId, int deptId, int projectId, int month, int year, string fourCRole, int empId)
        {

            DataSet dsEmp = null;
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[7];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                dsEmp = new DataSet();

                sqlParam[0] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.Int);
                if (deptId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = deptId;

                sqlParam[1] = new SqlParameter(SPParameter.PROJECTID, SqlDbType.Int);
                if (projectId == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = projectId;

                sqlParam[2] = new SqlParameter(SPParameter.Month, SqlDbType.Int);
                if (month == 0)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = month;

                sqlParam[3] = new SqlParameter(SPParameter.Year, SqlDbType.Int);
                if (year == 0)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = year;

                sqlParam[4] = new SqlParameter(SPParameter.fourCRole, SqlDbType.VarChar);
                if (fourCRole == null)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = fourCRole;

                sqlParam[5] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (empId == 0)
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = empId;

                sqlParam[6] = new SqlParameter(SPParameter.LoginEmpId, SqlDbType.Int);
                if (loginEmpId == 0)
                    sqlParam[6].Value = DBNull.Value;
                else
                    sqlParam[6].Value = loginEmpId;

                dsEmp = objDA.GetDataSet(SPNames.GET_Get4CEmployeeDetails, sqlParam);
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "Get4CEmployeeDeatils", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
            return dsEmp;
        }


        /// <summary>
        /// Gets the Reporting to Ids 
        /// </summary>
        /// <returns>DataSet</returns>
        public DataTable Get4CEmployeeDeatilsCarryForward(int loginEmpId, int deptId, int projectId, int month, int year, string fourCRole, string empIdlist)
        {

            DataTable dsEmpE = new DataTable();
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[7];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                var dsEmp = new DataSet();

                sqlParam[0] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.Int);
                if (deptId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = deptId;

                sqlParam[1] = new SqlParameter(SPParameter.PROJECTID, SqlDbType.Int);
                if (projectId == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = projectId;

                sqlParam[2] = new SqlParameter(SPParameter.Month, SqlDbType.Int);
                if (month == 0)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = month;

                sqlParam[3] = new SqlParameter(SPParameter.Year, SqlDbType.Int);
                if (year == 0)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = year;

                sqlParam[4] = new SqlParameter(SPParameter.fourCRole, SqlDbType.VarChar);
                if (fourCRole == null)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = fourCRole;

                sqlParam[5] = new SqlParameter(SPParameter.EmpId, SqlDbType.VarChar);
                if (empIdlist == null)
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = empIdlist;

                sqlParam[6] = new SqlParameter(SPParameter.LoginEmpId, SqlDbType.Int);
                if (loginEmpId == 0)
                    sqlParam[6].Value = DBNull.Value;
                else
                    sqlParam[6].Value = loginEmpId;

                dsEmp = objDA.GetDataSet(SPNames.GET_Get4CEmployeeDetails_CarryForward, sqlParam);

                if (dsEmp != null && dsEmp.Tables.Count >= 1)
                {
                    dsEmpE = dsEmp.Tables[0];
                }
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "Get4CEmployeeDeatils", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
            return dsEmpE;
        }


        /// <summary>
        /// Gets the crator reviewer details
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet GetCreatorReviewerDeatils(int deptId, int projectId)
        {

            DataSet dsEmp = null;
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[2];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                dsEmp = new DataSet();

                sqlParam[0] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.Int);
                if (deptId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = deptId;

                sqlParam[1] = new SqlParameter(SPParameter.PROJECTID, SqlDbType.Int);
                if (projectId == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = projectId;

                dsEmp = objDA.GetDataSet(SPNames.GET_Get4CCreatorReviewerDetails, sqlParam);
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "Get4CEmployeeDeatils", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
            return dsEmp;
        }



        /// <summary>
        /// Gets the Reporting to Ids 
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet Get4CReviewerEmployeeDeatils(int loginEmpId, int deptId, int projectId, int month, int year, string fourCRole, int empId)
        {

            DataSet dsEmp = null;
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[7];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                dsEmp = new DataSet();

                sqlParam[0] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.Int);
                if (deptId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = deptId;

                sqlParam[1] = new SqlParameter(SPParameter.PROJECTID, SqlDbType.Int);
                if (projectId == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = projectId;

                sqlParam[2] = new SqlParameter(SPParameter.Month, SqlDbType.Int);
                if (month == 0)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = month;

                sqlParam[3] = new SqlParameter(SPParameter.Year, SqlDbType.Int);
                if (year == 0)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = year;

                sqlParam[4] = new SqlParameter(SPParameter.fourCRole, SqlDbType.VarChar);
                if (fourCRole == null)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = fourCRole;

                sqlParam[5] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (empId == 0)
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = empId;

                sqlParam[6] = new SqlParameter(SPParameter.LoginEmpId, SqlDbType.Int);
                if (loginEmpId == 0)
                    sqlParam[6].Value = DBNull.Value;
                else
                    sqlParam[6].Value = loginEmpId;

                dsEmp = objDA.GetDataSet(SPNames.GET_Get4CReviewerEmployeeDetails, sqlParam);
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "Get4CEmployeeDeatils", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
            return dsEmp;
        }



        /// <summary>
        /// Gets the Creator details 
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet Get4CCreatorDeatils(int month, int year, string fourCRole, string emailId)
        {

            DataSet ds = null;
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[4];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                ds = new DataSet();

                sqlParam[0] = new SqlParameter(SPParameter.Month, SqlDbType.Int);
                if (month == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = month;

                sqlParam[1] = new SqlParameter(SPParameter.Year, SqlDbType.VarChar);
                if (year == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = year;

                sqlParam[2] = new SqlParameter(SPParameter.fourCRole, SqlDbType.VarChar);
                if (string.IsNullOrEmpty(fourCRole))
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = fourCRole;

                sqlParam[3] = new SqlParameter(SPParameter.EmailID, SqlDbType.VarChar);
                if (string.IsNullOrEmpty(emailId))
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = emailId;

                ds = objDA.GetDataSet(SPNames.GET_Get4CCreatorDetails, sqlParam);
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "Get4CEmployeeDeatils", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
            return ds;
        }


        /// <summary>
        /// Gets the Reporting to Ids 
        /// </summary>
        /// <returns>DataSet</returns>
        public FourCFeedback Get4CIndividualEmployeeDeatils(int deptid, int projectId, int month, int year, int empId)
        {

            DataSet ds = null;
            FourCFeedback objFeedback = null;
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[5];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                ds = new DataSet();

                sqlParam[0] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.Int);
                if (deptid == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = deptid;

                sqlParam[1] = new SqlParameter(SPParameter.PROJECTID, SqlDbType.Int);
                if (projectId == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = projectId;

                sqlParam[2] = new SqlParameter(SPParameter.Month, SqlDbType.Int);
                if (month == 0)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = month;

                sqlParam[3] = new SqlParameter(SPParameter.Year, SqlDbType.Int);
                if (year == 0)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = year;

                sqlParam[4] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (empId == 0)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = empId;

                ds = objDA.GetDataSet(SPNames.GET_Get4CIndividualEmployeeDetails, sqlParam);

                objFeedback = new FourCFeedback();

             

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (!string.IsNullOrEmpty(dr[DbTableColumn.FourCId].ToString()))
                            objFeedback.FBID = int.Parse(dr[DbTableColumn.FourCId].ToString());
                        else
                            objFeedback.FBID = 0;

                        if (!string.IsNullOrEmpty(dr[DbTableColumn.FourCEmpId].ToString()))
                            objFeedback.EmpId = int.Parse(dr[DbTableColumn.FourCEmpId].ToString());

                        if (!string.IsNullOrEmpty(dr[DbTableColumn.DesignationId].ToString()))
                            objFeedback.DesignationId = int.Parse(dr[DbTableColumn.DesignationId].ToString());

                        if (!string.IsNullOrEmpty(dr[DbTableColumn.LineManagerId].ToString()))
                            objFeedback.LineManagerId = dr[DbTableColumn.LineManagerId].ToString();

                        if (!string.IsNullOrEmpty(dr[DbTableColumn.FunctionalManagerId].ToString()))
                            objFeedback.FunctionalManagerId = dr[DbTableColumn.FunctionalManagerId].ToString();


                        if (!string.IsNullOrEmpty(dr[DbTableColumn.DepartmentId].ToString()))
                            objFeedback.DepartmentId = int.Parse(dr[DbTableColumn.DepartmentId].ToString());

                        if (!string.IsNullOrEmpty(dr[DbTableColumn.ProjectId].ToString()))
                            objFeedback.ProjectId = int.Parse(dr[DbTableColumn.ProjectId].ToString());

                        if (!string.IsNullOrEmpty(dr[DbTableColumn.Month].ToString()))
                            objFeedback.Month = int.Parse(dr[DbTableColumn.Month].ToString());

                        if (!string.IsNullOrEmpty(dr[DbTableColumn.Years].ToString()))
                            objFeedback.Year = int.Parse(dr[DbTableColumn.Years].ToString());

                        if (!string.IsNullOrEmpty(dr[DbTableColumn.Competency].ToString()))
                            objFeedback.Competency = dr[DbTableColumn.Competency].ToString();

                        if (!string.IsNullOrEmpty(dr[DbTableColumn.Communication].ToString()))
                            objFeedback.Communication = dr[DbTableColumn.Communication].ToString();

                        if (!string.IsNullOrEmpty(dr[DbTableColumn.Commitment].ToString()))
                            objFeedback.Commitment = dr[DbTableColumn.Commitment].ToString();

                        if (!string.IsNullOrEmpty(dr[DbTableColumn.Collaboration].ToString()))
                            objFeedback.Collaboration = dr[DbTableColumn.Collaboration].ToString();

                        if (!string.IsNullOrEmpty(dr[DbTableColumn.Employee4CStatus].ToString()))
                            objFeedback.Status = int.Parse(dr[DbTableColumn.Employee4CStatus].ToString());

                        if (!string.IsNullOrEmpty(dr[DbTableColumn.Creator].ToString()))
                            objFeedback.Creator = dr[DbTableColumn.Creator].ToString();

                        if (!string.IsNullOrEmpty(dr[DbTableColumn.Reviewer].ToString()))
                            objFeedback.Reviewer = dr[DbTableColumn.Reviewer].ToString();
                    }
               

                return objFeedback;


            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "Get4CEmployeeDeatils", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
            return objFeedback;
        }





        /// <summary>
        /// Gets the Reporting to Ids 
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet GetFilteredCreatorApproverDetails(int DeptId, int ProjectId)
        {

            DataSet ds = null;
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[2];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                ds = new DataSet();

                //objCommand = new SqlCommand(SPNames.GET_REPORTING_FUNCTIONAL_MANAGER_IDS, objConnection);
                //objCommand.CommandType = CommandType.StoredProcedure;

                //objCommand.Parameters.AddWithValue(SPParameter.EmpId, EMPId);

                sqlParam[0] = new SqlParameter(SPParameter.DeptId, SqlDbType.Int);
                if (DeptId == null)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = DeptId;

                sqlParam[1] = new SqlParameter(SPParameter.PROJECTID, SqlDbType.Int);
                if (ProjectId == null)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = ProjectId;

                //SqlDataAdapter objDataAdapter = new SqlDataAdapter(objCommand);
                //objDataAdapter.Fill(ds);

                ds = objDA.GetDataSet(SPNames.GET_Filtered_Creator_Reviewer_Details, sqlParam);
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetFilteredCreatorApproverDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
            return ds;
        }


        /// <summary>
        /// Gets the Reporting to Ids 
        /// </summary>
        /// <returns>DataSet</returns>
        public bool CheckCreatorReviewerSetForAll()
        {

            bool falg = false;
            DataAccessClass fourCDetails = new DataAccessClass();
            try
            {
                
                
                fourCDetails.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.FlagCreatorReviwerSet, DbType.Int32);
                sqlParam[0].Direction = ParameterDirection.Output;

             
                fourCDetails.ExecuteNonQuerySP(SPNames.Check_CreatorReviewerSetForAll, sqlParam);

                falg = Convert.ToBoolean(sqlParam[0].Value);

                return falg;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetCreatorApproverDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                fourCDetails.CloseConncetion();
            }
        }


        ///// <summary>
        ///// Gets the Reporting to Ids 
        ///// </summary>
        ///// <returns>DataSet</returns>
        //public bool Check4CAccessRights(string emailId)
        //{

        //    bool falg = false;
        //    DataAccessClass fourCDetails = new DataAccessClass();
        //    try
        //    {


        //        fourCDetails.OpenConnection(DBConstants.GetDBConnectionString());

        //        SqlParameter[] sqlParam = new SqlParameter[2];



        //        sqlParam[0] = new SqlParameter(SPParameter.EmailID, SqlDbType.VarChar);
        //        sqlParam[0].Value = emailId;

        //        sqlParam[1] = new SqlParameter(SPParameter.Flag, DbType.Int32);
        //        sqlParam[1].Direction = ParameterDirection.Output;

        //        fourCDetails.ExecuteNonQuerySP(SPNames.Check_AccessRights, sqlParam);

        //        falg = Convert.ToBoolean(sqlParam[1].Value);

        //        return falg;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetCreatorApproverDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
        //    }

        //    finally
        //    {
        //        fourCDetails.CloseConncetion();
        //    }
        //}


        
        /// <summary>
        /// Updates Creator Reviewer 
        /// </summary>
        /// <param name="objUpdateEmployee">The object update employee.</param>
        public void AddUpdateDeleteCreatorReviewer(int deptId, int ProjectId, string strCreator, string strReviewer, string ModifiedById, string mode)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[6];

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    sqlParam[0] = new SqlParameter(SPParameter.DeptId, deptId);
                    sqlParam[1] = new SqlParameter(SPParameter.PROJECTID, ProjectId);
                    sqlParam[2] = new SqlParameter(SPParameter.CreatorId, strCreator);
                    sqlParam[3] = new SqlParameter(SPParameter.ReviewerId, strReviewer);
                    sqlParam[4] = new SqlParameter(SPParameter.ModifiedById, ModifiedById);
                    sqlParam[5] = new SqlParameter(SPParameter.Mode, mode);

                    objDA.ExecuteNonQuerySP(SPNames.Add_Update_Delete_Creator_Reviewer_Detail, sqlParam);

                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "UpdateEmployeeFMRM", EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }


        /// <summary>
        /// Add Delete View Access Rights
        /// </summary>
        /// <param name="objUpdateEmployee">The object update employee.</param>
        public void AddDeleteViewAccessRights(List<string> lst, string ModifiedById, string mode)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[3];

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    foreach (var itm in lst)
                    {
                        sqlParam[0] = new SqlParameter(SPParameter.EmpId, int.Parse(itm.ToString()));
                        sqlParam[1] = new SqlParameter(SPParameter.ModifiedById, ModifiedById);
                        sqlParam[2] = new SqlParameter(SPParameter.Mode, mode);

                        objDA.ExecuteNonQuerySP(SPNames.Add_Delete_ViewAccessRights, sqlParam);
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
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "AddDeleteViewAccessRights", EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Gets the Reporting to Ids 
        /// </summary>
        /// <returns>DataSet</returns>
        public List<string> Check4CLoginRights(string emailId, ref int loginEmpId)
        {

            List<string> lstRights = new List<string> { };
            DataAccessClass objDAGetMRFId = new DataAccessClass();

            try
            {
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.EmailID, DbType.String);
                sqlParam[0].Value = emailId;

                //Open the connection to DB
                objDAGetMRFId.OpenConnection(DBConstants.GetDBConnectionString());

                objDataReader = objDAGetMRFId.ExecuteReaderSP(SPNames.GET_Login_AccessRights, sqlParam);

                while (objDataReader.Read())
                {
                    //if (objDataReader[MasterEnum.FourCRole.CREATOR.ToString()] != null && !string.IsNullOrEmpty(objDataReader[MasterEnum.FourCRole.CREATOR.ToString()].ToString()))
                    //{
                    //    lstRights.Add(objDataReader[MasterEnum.FourCRole.CREATOR.ToString()].ToString().Trim());
                    //}
                    //if (objDataReader[MasterEnum.FourCRole.REVIEWER.ToString()] != null && !string.IsNullOrEmpty(objDataReader[MasterEnum.FourCRole.REVIEWER.ToString()].ToString()))
                    //{
                    //    lstRights.Add(objDataReader[MasterEnum.FourCRole.REVIEWER.ToString()].ToString());
                    //}
                    //if (objDataReader[MasterEnum.FourCRole.CREATORANDREVIEWER.ToString()] != null && !string.IsNullOrEmpty(objDataReader[MasterEnum.FourCRole.CREATORANDREVIEWER.ToString()].ToString()))
                    //{
                    //    lstRights.Add(objDataReader[MasterEnum.FourCRole.CREATORANDREVIEWER.ToString()].ToString());
                    //}

                    if(!string.IsNullOrEmpty(objDataReader["EmpId"].ToString()))
                        loginEmpId = int.Parse(objDataReader["EmpId"].ToString());

                    if (objDataReader[MasterEnum.FourCRole.FOURCADMIN.ToString()] != null && !string.IsNullOrEmpty(objDataReader[MasterEnum.FourCRole.FOURCADMIN.ToString()].ToString()))
                    {
                        lstRights.Add(objDataReader[MasterEnum.FourCRole.FOURCADMIN.ToString()].ToString());
                    }
                    if (objDataReader[MasterEnum.FourCRole.FOURCADMIN_REVIEWER.ToString()] != null && !string.IsNullOrEmpty(objDataReader[MasterEnum.FourCRole.FOURCADMIN_REVIEWER.ToString()].ToString()))
                    {
                        lstRights.Add(objDataReader[MasterEnum.FourCRole.FOURCADMIN_REVIEWER.ToString()].ToString());
                    }
                    if (objDataReader[MasterEnum.FourCRole.ONLYCREATOR.ToString()] != null && !string.IsNullOrEmpty(objDataReader[MasterEnum.FourCRole.ONLYCREATOR.ToString()].ToString()))
                    {
                        lstRights.Add(objDataReader[MasterEnum.FourCRole.ONLYCREATOR.ToString()].ToString());
                    }
                    if (objDataReader[MasterEnum.FourCRole.VIEWACCESSRIGHTS.ToString()] != null && !string.IsNullOrEmpty(objDataReader[MasterEnum.FourCRole.VIEWACCESSRIGHTS.ToString()].ToString()))
                    {
                        lstRights.Add(objDataReader[MasterEnum.FourCRole.VIEWACCESSRIGHTS.ToString()].ToString());
                    }
                    if (objDataReader[MasterEnum.FourCRole.RMS_FUNCTIONALMANAGER.ToString()] != null && !string.IsNullOrEmpty(objDataReader[MasterEnum.FourCRole.RMS_FUNCTIONALMANAGER.ToString()].ToString()))
                    {
                        lstRights.Add(objDataReader[MasterEnum.FourCRole.RMS_FUNCTIONALMANAGER.ToString()].ToString());
                    }
                    if (objDataReader[MasterEnum.FourCRole.CREATOR.ToString()] != null && !string.IsNullOrEmpty(objDataReader[MasterEnum.FourCRole.CREATOR.ToString()].ToString()))
                    {
                        lstRights.Add(objDataReader[MasterEnum.FourCRole.CREATOR.ToString()].ToString().Trim());
                    }
                    if (objDataReader[MasterEnum.FourCRole.REVIEWER.ToString()] != null && !string.IsNullOrEmpty(objDataReader[MasterEnum.FourCRole.REVIEWER.ToString()].ToString()))
                    {
                        lstRights.Add(objDataReader[MasterEnum.FourCRole.REVIEWER.ToString()].ToString());
                    }
                    //My4CView Start
                    if (objDataReader[MasterEnum.FourCRole.ViewMy4C.ToString()] != null && !string.IsNullOrEmpty(objDataReader[MasterEnum.FourCRole.ViewMy4C.ToString()].ToString()))
                    {
                        lstRights.Add(objDataReader[MasterEnum.FourCRole.ViewMy4C.ToString()].ToString());
                    }
                    //My4CView End
                    // Mohamed : 13/02/2015 : Starts                        			  
                    // Desc : 4C access rights
                    if (objDataReader[MasterEnum.FourCRole.REPORTACCESS.ToString()] != null && !string.IsNullOrEmpty(objDataReader[MasterEnum.FourCRole.REPORTACCESS.ToString()].ToString()))
                    {
                        lstRights.Add(objDataReader[MasterEnum.FourCRole.REPORTACCESS.ToString()].ToString());
                    }
                    // Mohamed : 13/02/2015 : Ends
                }
                return lstRights;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetFilteredCreatorApproverDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                //objDA.CloseConncetion();
                objDAGetMRFId.CloseConncetion();
            }

        }


        /// <summary>
        /// Gets the Reporting to Ids 
        /// </summary>
        /// <returns>DataSet</returns>
        public DateTime? CurrentMonthLastDay()
        {

            DateTime? dtCurrentLastDayOfMonth = null;
            DataAccessClass objDAGetMRFId = new DataAccessClass();

            try
            {
                //Open the connection to DB
                objDAGetMRFId.OpenConnection(DBConstants.GetDBConnectionString());

                objDataReader = objDAGetMRFId.ExecuteReaderSP(SPNames.GET_CurrentMonthLastDay);

                while (objDataReader.Read())
                {
                    if (DateTime.Parse(objDataReader["LastDayOfCurrentMonth"].ToString()) != null)
                        dtCurrentLastDayOfMonth = DateTime.Parse(objDataReader["LastDayOfCurrentMonth"].ToString());
                }
                return dtCurrentLastDayOfMonth;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetFilteredCreatorApproverDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                //objDA.CloseConncetion();
                objDAGetMRFId.CloseConncetion();
            }

        }




        ///// <summary>
        ///// Get Project Name based on Employee
        ///// </summary>
        ///// <returns>DataSet</returns>
        //public DataSet Check4CLoginRights(string emailId)
        //{

        //    DataSet ds = null;
        //    try
        //    {
        //        SqlParameter[] sqlParam = new SqlParameter[1];
        //        objDA = new DataAccessClass();
        //        objDA.OpenConnection(DBConstants.GetDBConnectionString());

        //        ds = new DataSet();

        //        sqlParam[0] = new SqlParameter(SPParameter.EmailID, DbType.String);
        //        sqlParam[0].Value = emailId;

        //        ds = objDA.GetDataSet(SPNames.GET_Login_AccessRights, sqlParam);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "FillProjectDropdownOnEmp", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
        //    }
        //    finally
        //    {
        //        objDA.CloseConncetion();
        //    }
        //    return ds;
        //}


        /// <summary>
        /// this function fills the dropdown
        /// </summary>
        public DataTable FillParameterList(string strCategory)
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            SqlDataReader objReader = null;

            try
            {
                

                string ConnStr = DBConstants.GetDBConnectionString();
                //string ConnStr = Common.DBConstants.GetDBConnectionString();
                int ID = Convert.ToInt32(strCategory);
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();
                objCommand = new SqlCommand(SPNames.GET_ParameterOnC, objConnection);
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


        /// <summary>
        /// this function fills the dropdown
        /// </summary>
        public DataTable FillActionOwner(int projectId)
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            SqlDataReader objReader = null;

            try
            {


                string ConnStr = DBConstants.GetDBConnectionString();
                //string ConnStr = Common.DBConstants.GetDBConnectionString();
                //int ID = Convert.ToInt32(strCategory);
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();
                objCommand = new SqlCommand(SPNames.GET_ActionOwner, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.Add(SPParameter.PROJECTID, projectId);
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
        /// Get Project Name based on Employee
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet FillProjectDropdownOnEmp(int empId, int month, int year)
        {

            DataSet ds = null;
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[3];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                ds = new DataSet();

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (empId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = empId;

                sqlParam[1] = new SqlParameter(SPParameter.Month, SqlDbType.Int);
                if (month == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = month;

                sqlParam[2] = new SqlParameter(SPParameter.Year, SqlDbType.Int);
                if (year == 0)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = year;

                ds = objDA.GetDataSet(SPNames.GET_ProjectNameOnEmp, sqlParam);
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "FillProjectDropdownOnEmp", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
            return ds;
        }


        /// <summary>
        /// this function fills the dropdown
        /// </summary>
        public DataSet Get4CActionDetails(int deptid, int empId, int projectId, int month, int year, string loginEmailId, int Mode)
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            SqlDataReader objReader = null;

            DataSet ds = null;

            try
            {

                SqlParameter[] sqlParam = new SqlParameter[7];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                ds = new DataSet();

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (empId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = empId;

                sqlParam[1] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.Int);
                if (deptid == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = deptid;

                sqlParam[2] = new SqlParameter(SPParameter.PROJECTID, SqlDbType.Int);
                if (projectId == 0)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = projectId;

                sqlParam[3] = new SqlParameter(SPParameter.Month, SqlDbType.Int);
                if (month == 0)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = month;

                sqlParam[4] = new SqlParameter(SPParameter.Year, SqlDbType.Int);
                if (year == 0)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = year;

                sqlParam[5] = new SqlParameter(SPParameter.EmailID, SqlDbType.VarChar);
                if (string.IsNullOrEmpty(loginEmailId))
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = loginEmailId;

                sqlParam[6] = new SqlParameter(SPParameter.RatingOption, SqlDbType.Int);
                sqlParam[6].Value = Mode;

                ds = objDA.GetDataSet(SPNames.GET_ActionDetails, sqlParam);
                return ds;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                objDA.CloseConncetion();
               
            }
        }


        /// <summary>
        /// this function fills the dropdown
        /// </summary>
        public DataSet Get4CActionDetailsForDashboard(int deptid, int empId, int fbId, int projectId, int month, int year, string loginEmailId, int Mode)
        {
       
            DataSet ds = null;

            try
            {

                SqlParameter[] sqlParam = new SqlParameter[8];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                ds = new DataSet();

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (empId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = empId;

                sqlParam[1] = new SqlParameter(SPParameter.FBID, SqlDbType.Int);
                if (fbId == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = fbId;
                
                sqlParam[2] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.Int);
                if (deptid == 0)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = deptid;

                sqlParam[3] = new SqlParameter(SPParameter.PROJECTID, SqlDbType.Int);
                if (projectId == 0)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = projectId;

                sqlParam[4] = new SqlParameter(SPParameter.Month, SqlDbType.Int);
                if (month == 0)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = month;

                sqlParam[5] = new SqlParameter(SPParameter.Year, SqlDbType.Int);
                if (year == 0)
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = year;

                sqlParam[6] = new SqlParameter(SPParameter.EmailID, SqlDbType.VarChar);
                if (string.IsNullOrEmpty(loginEmailId))
                    sqlParam[6].Value = DBNull.Value;
                else
                    sqlParam[6].Value = loginEmailId;

                sqlParam[7] = new SqlParameter(SPParameter.RatingOption, SqlDbType.Int);
                sqlParam[7].Value = Mode;

                ds = objDA.GetDataSet(SPNames.GET_ActionDetailsForDashboard, sqlParam);
                return ds;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                objDA.CloseConncetion();

            }
        }


        /// <summary>
        /// this function fills the dropdown
        /// </summary>
        public DataSet GetEmpDashboardData(int empId, int Months)
        {
         

            DataSet ds = null;

            try
            {

                SqlParameter[] sqlParam = new SqlParameter[2];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                ds = new DataSet();

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (empId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = empId;

                sqlParam[1] = new SqlParameter(SPParameter.NoOfMonth, SqlDbType.Int);
                if (Months == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = Months;

                ds = objDA.GetDataSet(SPNames.GET_EmployeeDashboard, sqlParam);

                return ds;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                objDA.CloseConncetion();

            }
        }


        /// <summary>
        /// Gets the Reporting to Ids 
        /// </summary>
        /// <returns>DataSet</returns>
        public bool InsertActionDetails(DataTable dtActionData, FourCFeedback objFeedBack)
        {

            bool falg = false;
            DataAccessClass fourCDetails = new DataAccessClass();
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA = new DataAccessClass();

                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    int FBID = 0;

                    if (objFeedBack.FBID == 0)
                    {
                        //INSERT Records in T_Feedback

                        SqlParameter[] sqlParamFB = new SqlParameter[17];

                        sqlParamFB[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                        if (objFeedBack.EmpId == 0)
                            sqlParamFB[0].Value = DBNull.Value;
                        else
                            sqlParamFB[0].Value = objFeedBack.EmpId;


                        sqlParamFB[1] = new SqlParameter(SPParameter.Designation, SqlDbType.Int);
                        if (objFeedBack.DesignationId == 0)
                            sqlParamFB[1].Value = DBNull.Value;
                        else
                            sqlParamFB[1].Value = objFeedBack.DesignationId;

                        sqlParamFB[2] = new SqlParameter(SPParameter.LineManagerId, SqlDbType.VarChar);
                        if (string.IsNullOrEmpty(objFeedBack.LineManagerId))
                            sqlParamFB[2].Value = DBNull.Value;
                        else
                            sqlParamFB[2].Value = objFeedBack.LineManagerId;

                        sqlParamFB[3] = new SqlParameter(SPParameter.FunctionalManagerId, SqlDbType.VarChar);
                        if (string.IsNullOrEmpty(objFeedBack.FunctionalManagerId))
                            sqlParamFB[3].Value = DBNull.Value;
                        else
                            sqlParamFB[3].Value = objFeedBack.FunctionalManagerId;

                        sqlParamFB[4] = new SqlParameter(SPParameter.DeptId, SqlDbType.Int);
                        if (objFeedBack.DepartmentId == 0)
                            sqlParamFB[4].Value = DBNull.Value;
                        else
                            sqlParamFB[4].Value = objFeedBack.DepartmentId;

                        sqlParamFB[5] = new SqlParameter(SPParameter.PROJECTID, SqlDbType.Int);
                        if (objFeedBack.ProjectId == 0)
                            sqlParamFB[5].Value = DBNull.Value;
                        else
                            sqlParamFB[5].Value = objFeedBack.ProjectId;


                        sqlParamFB[6] = new SqlParameter(SPParameter.Month, SqlDbType.Int);
                        if (objFeedBack.Month == 0)
                            sqlParamFB[6].Value = DBNull.Value;
                        else
                            sqlParamFB[6].Value = objFeedBack.Month;


                        sqlParamFB[7] = new SqlParameter(SPParameter.Year, SqlDbType.Int);
                        if (objFeedBack.Year == 0)
                            sqlParamFB[7].Value = DBNull.Value;
                        else
                            sqlParamFB[7].Value = objFeedBack.Year;

                        sqlParamFB[8] = new SqlParameter(SPParameter.FourCStatus, SqlDbType.Int);
                        if (objFeedBack.Status == 0)
                            sqlParamFB[8].Value = DBNull.Value;
                        else
                            sqlParamFB[8].Value = objFeedBack.Status;

                        sqlParamFB[9] = new SqlParameter(SPParameter.Competency, SqlDbType.VarChar);
                        if (string.IsNullOrEmpty(objFeedBack.Competency))
                            sqlParamFB[9].Value = DBNull.Value;
                        else
                            sqlParamFB[9].Value = objFeedBack.Competency;

                        sqlParamFB[10] = new SqlParameter(SPParameter.Collaboration, SqlDbType.VarChar);
                        if (string.IsNullOrEmpty(objFeedBack.Collaboration))
                            sqlParamFB[10].Value = DBNull.Value;
                        else
                            sqlParamFB[10].Value = objFeedBack.Collaboration;

                        sqlParamFB[11] = new SqlParameter(SPParameter.Communication, SqlDbType.VarChar);
                        if (string.IsNullOrEmpty(objFeedBack.Communication))
                            sqlParamFB[11].Value = DBNull.Value;
                        else
                            sqlParamFB[11].Value = objFeedBack.Communication;

                        sqlParamFB[12] = new SqlParameter(SPParameter.Commitment, SqlDbType.VarChar);
                        if (string.IsNullOrEmpty(objFeedBack.Commitment))
                            sqlParamFB[12].Value = DBNull.Value;
                        else
                            sqlParamFB[12].Value = objFeedBack.Commitment;

                        sqlParamFB[13] = new SqlParameter(SPParameter.ModifiedById, SqlDbType.VarChar);
                        if (string.IsNullOrEmpty(objFeedBack.ModifiedById))
                            sqlParamFB[13].Value = DBNull.Value;
                        else
                            sqlParamFB[13].Value = objFeedBack.ModifiedById;

                        sqlParamFB[14] = new SqlParameter(SPParameter.CreatorId, SqlDbType.VarChar);
                        if (string.IsNullOrEmpty(objFeedBack.Creator))
                            sqlParamFB[14].Value = DBNull.Value;
                        else
                            sqlParamFB[14].Value = objFeedBack.Creator;

                        sqlParamFB[15] = new SqlParameter(SPParameter.ReviewerId, SqlDbType.VarChar);
                        if (string.IsNullOrEmpty(objFeedBack.Reviewer))
                            sqlParamFB[15].Value = DBNull.Value;
                        else
                            sqlParamFB[15].Value = objFeedBack.Reviewer;

                        sqlParamFB[16] = new SqlParameter(SPParameter.FBID, DbType.Int32);
                        sqlParamFB[16].Direction = ParameterDirection.Output;

                        objDA.ExecuteNonQuerySP(SPNames.Check_InsertFeedbackDetails, sqlParamFB);

                        FBID = int.Parse(sqlParamFB[16].Value.ToString());

                        falg = true;

                    }

                    //When any final rating update then below if condition executed.
                    // when first time if final rating added then in above SP color is inserted in table
                    if (objFeedBack.FBID != 0)
                    {
                        SqlParameter[] sqlParamColor = new SqlParameter[8];

                        sqlParamColor[0] = new SqlParameter(SPParameter.FBID, SqlDbType.Int);
                        if (string.IsNullOrEmpty(objFeedBack.FBID.ToString()))
                            sqlParamColor[0].Value = DBNull.Value;
                        else
                            sqlParamColor[0].Value = objFeedBack.FBID;

                        sqlParamColor[1] = new SqlParameter(SPParameter.Competency, SqlDbType.VarChar);
                        if (string.IsNullOrEmpty(objFeedBack.Competency))
                            sqlParamColor[1].Value = DBNull.Value;
                        else
                            sqlParamColor[1].Value = objFeedBack.Competency;

                        sqlParamColor[2] = new SqlParameter(SPParameter.Collaboration, SqlDbType.VarChar);
                        if (string.IsNullOrEmpty(objFeedBack.Collaboration))
                            sqlParamColor[2].Value = DBNull.Value;
                        else
                            sqlParamColor[2].Value = objFeedBack.Collaboration;

                        sqlParamColor[3] = new SqlParameter(SPParameter.Communication, SqlDbType.VarChar);
                        if (string.IsNullOrEmpty(objFeedBack.Communication))
                            sqlParamColor[3].Value = DBNull.Value;
                        else
                            sqlParamColor[3].Value = objFeedBack.Communication;

                        sqlParamColor[4] = new SqlParameter(SPParameter.Commitment, SqlDbType.VarChar);
                        if (string.IsNullOrEmpty(objFeedBack.Commitment))
                            sqlParamColor[4].Value = DBNull.Value;
                        else
                            sqlParamColor[4].Value = objFeedBack.Commitment;

                        sqlParamColor[5] = new SqlParameter(SPParameter.ModifiedById, SqlDbType.VarChar);
                        if (string.IsNullOrEmpty(objFeedBack.ModifiedById))
                            sqlParamColor[5].Value = DBNull.Value;
                        else
                            sqlParamColor[5].Value = objFeedBack.ModifiedById;

                        sqlParamColor[6] = new SqlParameter(SPParameter.RatingOption, SqlDbType.Int);
                        if (string.IsNullOrEmpty(objFeedBack.RatingOption.ToString()))
                            sqlParamColor[6].Value = 0;
                        else
                            sqlParamColor[6].Value = objFeedBack.RatingOption;

                        sqlParamColor[7] = new SqlParameter(SPParameter.RemarksReviewer, SqlDbType.VarChar);
                        if (objFeedBack.ReviewerRemarks == null)
                            sqlParamColor[7].Value = DBNull.Value;
                        else
                            sqlParamColor[7].Value = objFeedBack.ReviewerRemarks;

                        objDA.ExecuteNonQuerySP(SPNames.Check_UpdateFinalRating, sqlParamColor);

                        falg = true;
                    }

                    //INSERT
                    //foreach (DataRow dr in dtActionData.Select("ActionDML = 'INSERT'"))
                    if(dtActionData != null)
                    {
                        foreach (DataRow dr in dtActionData.Rows)
                        {

                            //fourCDetails.OpenConnection(DBConstants.GetDBConnectionString());

                            // Those row whose C and parameter are select update only that row.
                            if ((!string.IsNullOrEmpty(dr[DbTableColumn.FourCType].ToString()) && dr[DbTableColumn.FourCType].ToString() != "0") && (!string.IsNullOrEmpty(dr[DbTableColumn.FourCParameterType].ToString()) && dr[DbTableColumn.FourCParameterType].ToString() != "0"))
                            {
                                SqlParameter[] sqlParam = new SqlParameter[14];

                                sqlParam[0] = new SqlParameter(SPParameter.FBAID, SqlDbType.Int);
                                if (string.IsNullOrEmpty(dr[DbTableColumn.FourCActionId].ToString()))
                                    sqlParam[0].Value = DBNull.Value;
                                else
                                    sqlParam[0].Value = dr[DbTableColumn.FourCActionId];

                                sqlParam[1] = new SqlParameter(SPParameter.FBID, SqlDbType.Int);
                                if (!string.IsNullOrEmpty(dr[DbTableColumn.FourCId].ToString()) && dr[DbTableColumn.FourCId].ToString() == "0")
                                    sqlParam[1].Value = FBID;
                                else
                                    sqlParam[1].Value = dr[DbTableColumn.FourCId];

                                sqlParam[2] = new SqlParameter(SPParameter.CTYPE, SqlDbType.Int);
                                if (string.IsNullOrEmpty(dr[DbTableColumn.FourCType].ToString()))
                                    sqlParam[2].Value = DBNull.Value;
                                else
                                    sqlParam[2].Value = dr[DbTableColumn.FourCType];

                                sqlParam[3] = new SqlParameter(SPParameter.ParameterType, SqlDbType.Int);
                                if (string.IsNullOrEmpty(dr[DbTableColumn.FourCParameterType].ToString()))
                                    sqlParam[3].Value = DBNull.Value;
                                else
                                    sqlParam[3].Value = dr[DbTableColumn.FourCParameterType];

                                sqlParam[4] = new SqlParameter(SPParameter.Discription, SqlDbType.VarChar);
                                if (string.IsNullOrEmpty(dr[DbTableColumn.FourCDescription].ToString()))
                                    sqlParam[4].Value = DBNull.Value;
                                else
                                    sqlParam[4].Value = dr[DbTableColumn.FourCDescription];

                                sqlParam[5] = new SqlParameter(SPParameter.Action, SqlDbType.VarChar);
                                if (string.IsNullOrEmpty(dr[DbTableColumn.FourCAction].ToString()))
                                    sqlParam[5].Value = DBNull.Value;
                                else
                                    sqlParam[5].Value = dr[DbTableColumn.FourCAction];

                                sqlParam[6] = new SqlParameter(SPParameter.ActionOwnerId, SqlDbType.VarChar);
                                if (string.IsNullOrEmpty(dr[DbTableColumn.FourCActionOwnerId].ToString()))
                                    sqlParam[6].Value = DBNull.Value;
                                else
                                    sqlParam[6].Value = dr[DbTableColumn.FourCActionOwnerId];

                                sqlParam[7] = new SqlParameter(SPParameter.ActionCreatedDate, SqlDbType.DateTime);
                                if (string.IsNullOrEmpty(dr[DbTableColumn.FourCActionCreatedDate].ToString()))
                                    sqlParam[7].Value = DBNull.Value;
                                else
                                    sqlParam[7].Value = Convert.ToDateTime(dr[DbTableColumn.FourCActionCreatedDate].ToString());

                                sqlParam[8] = new SqlParameter(SPParameter.TargetClosureDate, SqlDbType.DateTime);
                                if (string.IsNullOrEmpty(dr[DbTableColumn.FourCTargetClosureDate].ToString()))
                                    sqlParam[8].Value = DBNull.Value;
                                else
                                    sqlParam[8].Value = Convert.ToDateTime(dr[DbTableColumn.FourCTargetClosureDate].ToString());

                                sqlParam[9] = new SqlParameter(SPParameter.ActualClosureDate, SqlDbType.DateTime);
                                if (string.IsNullOrEmpty(dr[DbTableColumn.FourCActualClosureDate].ToString()))
                                    sqlParam[9].Value = DBNull.Value;
                                else
                                    sqlParam[9].Value = Convert.ToDateTime(dr[DbTableColumn.FourCActualClosureDate].ToString());

                                sqlParam[10] = new SqlParameter(SPParameter.RemarksAction, SqlDbType.VarChar);
                                if (string.IsNullOrEmpty(dr[DbTableColumn.FourCRemarks].ToString()))
                                    sqlParam[10].Value = DBNull.Value;
                                else
                                    sqlParam[10].Value = dr[DbTableColumn.FourCRemarks];

                                sqlParam[11] = new SqlParameter(SPParameter.ActionStatus, SqlDbType.Int);
                                if (string.IsNullOrEmpty(dr[DbTableColumn.FourCActionStatus].ToString()))
                                    sqlParam[11].Value = DBNull.Value;
                                else
                                    sqlParam[11].Value = dr[DbTableColumn.FourCActionStatus];

                                sqlParam[12] = new SqlParameter(SPParameter.ActionDML, SqlDbType.VarChar);
                                if (string.IsNullOrEmpty(dr[DbTableColumn.ActionDML].ToString()))
                                    sqlParam[12].Value = DBNull.Value;
                                else
                                    sqlParam[12].Value = dr[DbTableColumn.ActionDML].ToString();

                                sqlParam[13] = new SqlParameter(SPParameter.ModifiedById, SqlDbType.VarChar);
                                if (string.IsNullOrEmpty(objFeedBack.ModifiedById))
                                    sqlParam[13].Value = DBNull.Value;
                                else
                                    sqlParam[13].Value = objFeedBack.ModifiedById;

                                //sqlParam[11] = new SqlParameter(SPParameter.Flag, DbType.Int32);
                                //sqlParam[11].Direction = ParameterDirection.Output;

                                objDA.ExecuteNonQuerySP(SPNames.Check_InsertActionDetails, sqlParam);

                                //falg = Convert.ToBoolean(sqlParam[11].Value);

                                falg = true;
                            }
                        }
                    }

                    ts.Complete();
                }

                return falg;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetCreatorApproverDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
        }



        /// <summary>
        /// Gets the Reporting to Ids 
        /// </summary>
        /// <returns>DataSet</returns>
        public bool IsAllowToFillActionData(int deptid, int projectId, string emailId)
        {

            bool falg = false;
            DataAccessClass fourCDetails = new DataAccessClass();
            try
            {


                fourCDetails.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[4];


                sqlParam[0] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.Int);
                sqlParam[0].Value = deptid;

                sqlParam[1] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int);
                sqlParam[1].Value = projectId;

                sqlParam[2] = new SqlParameter(SPParameter.EmailID, SqlDbType.VarChar);
                sqlParam[2].Value = emailId;

                sqlParam[3] = new SqlParameter(SPParameter.Flag, DbType.Int32);
                sqlParam[3].Direction = ParameterDirection.Output;

                fourCDetails.ExecuteNonQuerySP(SPNames.Check_IsAllowToFillActionData, sqlParam);

                falg = Convert.ToBoolean(sqlParam[3].Value);

                return falg;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "IsAllowToFillActionData", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                fourCDetails.CloseConncetion();
            }
        }


        public DataSet GetProjectName()
        {
            DataAccessClass objDAGetProjectName = new DataAccessClass();

            // Initialise Collection class object
            RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                DataSet ds = objDA.GetDataSet(SPNames.MRF_GetProjectName);

                return ds;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetProjectName", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
        }

        public DataSet GetProjectNameAddCreatorReviewer()
        {
            DataAccessClass objDAGetProjectName = new DataAccessClass();

            // Initialise Collection class object
            RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                DataSet ds = objDA.GetDataSet(SPNames.GetProjectAddCreatorApprover);

                return ds;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetProjectName", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
        }


        public DataSet GetDepartmentName(string UserMailId)
        {
            DataAccessClass objDAGetProjectName = new DataAccessClass();

            // Initialise Collection class object
            RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.EmailID, DbType.String);
                if (UserMailId == null)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = UserMailId;

                DataSet ds = objDA.GetDataSet(SPNames.Master_GetDepartment, sqlParam);

                return ds;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetProjectName", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
        }

        public DataSet GetDepartmentForCreatorApprover()
        {
            DataAccessClass objDAGetProjectName = new DataAccessClass();

            // Initialise Collection class object
            RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                DataSet ds = objDA.GetDataSet(SPNames.GetDepartmentCreatorApprover);

                return ds;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetProjectName", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Gets the Project Name.
        /// </summary>
        /// <returns>Collection</returns>
        //public BusinessEntities.RaveHRCollection GetProjectName(string mailId, string roleCreatorApprover, string strFourCType)
        public DataSet GetDepartmentName(string mailId, string roleCreatorApprover, string strFourCType)
        {
            // Initialise Data Access Class object
            DataAccessClass objDAGetProjectName = new DataAccessClass();

            // Initialise Collection class object
            //RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                //Open the connection to DB
                objDAGetProjectName.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[3];

                sqlParam[0] = new SqlParameter(SPParameter.EmailID, DbType.String);
                if (mailId == null)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = mailId;

                sqlParam[1] = new SqlParameter(SPParameter.fourCRole, DbType.String);
                if (roleCreatorApprover == null)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = roleCreatorApprover;

                sqlParam[2] = new SqlParameter(SPParameter.fourCType, DbType.String);
                if (strFourCType == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = strFourCType;

                DataSet ds = objDAGetProjectName.GetDataSet(SPNames.GET_4C_DepartmentName, sqlParam);

                return ds;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetProjectName", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDAGetProjectName.CloseConncetion();
            }
        }


        /// <summary>
        /// Gets the Project Name.
        /// </summary>
        /// <returns>Collection</returns>
        //public BusinessEntities.RaveHRCollection GetProjectName(string mailId, string roleCreatorApprover, string strFourCType)
        public DataSet GetFunctionalManagerDeptName(int empId)
        {
            // Initialise Data Access Class object
            DataAccessClass objDAGetProjectName = new DataAccessClass();

            try
            {
                //Open the connection to DB
                objDAGetProjectName.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, DbType.Int32);
                if (empId == null)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = empId;

                DataSet ds = objDAGetProjectName.GetDataSet(SPNames.GET_4C_FunctionalDeptName, sqlParam);

                return ds;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetProjectName", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDAGetProjectName.CloseConncetion();
            }
        }

        /// <summary>
        /// Gets the Project Name.
        /// </summary>
        /// <returns>Collection</returns>
        //public BusinessEntities.RaveHRCollection GetProjectName(string mailId, string roleCreatorApprover, string strFourCType)
        public DataSet GetFunctionalManagerProjectName(int empId)
        {
            // Initialise Data Access Class object
            DataAccessClass objDAGetProjectName = new DataAccessClass();

            try
            {
                //Open the connection to DB
                objDAGetProjectName.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, DbType.Int32);
                if (empId == null)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = empId;

                DataSet ds = objDAGetProjectName.GetDataSet(SPNames.GET_4C_FunctionalProjectName, sqlParam);

                return ds;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetProjectName", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDAGetProjectName.CloseConncetion();
            }
        }


        /// <summary>
        /// Gets the Project Name.
        /// </summary>
        /// <returns>Collection</returns>
        //public BusinessEntities.RaveHRCollection GetProjectName(string mailId, string roleCreatorApprover, string strFourCType)
        public DataSet GetFunctionalManagerEmployeeName(int empId)
        {
            // Initialise Data Access Class object
            DataAccessClass objDAGetProjectName = new DataAccessClass();

            try
            {
                //Open the connection to DB
                objDAGetProjectName.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, DbType.Int32);
                if (empId == null)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = empId;

                DataSet ds = objDAGetProjectName.GetDataSet(SPNames.GET_4C_FunctionalEmployeeName, sqlParam);

                return ds;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetProjectName", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDAGetProjectName.CloseConncetion();
            }
        }



        /// <summary>
        /// Gets the Project Name.
        /// </summary>
        /// <returns>Collection</returns>
        //public BusinessEntities.RaveHRCollection GetProjectName(string mailId, string roleCreatorApprover, string strFourCType)
        public DataSet GetProjectName(string mailId, string roleCreatorApprover, string strFourCType)
        {
            // Initialise Data Access Class object
            DataAccessClass objDAGetProjectName = new DataAccessClass();

            // Initialise Collection class object
            RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                //Open the connection to DB
                objDAGetProjectName.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[3];

                sqlParam[0] = new SqlParameter(SPParameter.EmailID, DbType.String);
                if (mailId == null)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = mailId;

                sqlParam[1] = new SqlParameter(SPParameter.fourCRole, DbType.String);
                if (roleCreatorApprover == null)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = roleCreatorApprover;

                sqlParam[2] = new SqlParameter(SPParameter.fourCType, DbType.String);
                if (strFourCType == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = strFourCType;

                ////Execute the SP
                //objDataReader = objDAGetProjectName.ExecuteReaderSP(SPNames.GET_4C_ProjectName, sqlParam);

                ////Read the data and assign to Collection object
                //while (objDataReader.Read())
                //{
                //    //Initialise the Business Entity object
                //    KeyValue<string> keyValue = new KeyValue<string>();

                //    keyValue.KeyName = objDataReader.GetValue(0).ToString();
                //    keyValue.Val = objDataReader.GetValue(1).ToString();

                //    // Add the object to Collection
                //    raveHRCollection.Add(keyValue);
                //}

                //// Return the Collection
                //return raveHRCollection;

                DataSet ds = objDAGetProjectName.GetDataSet(SPNames.GET_4C_ProjectName, sqlParam);

                return ds;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetProjectName", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDAGetProjectName.CloseConncetion();
            }
        }


        ///// <summary>
        ///// Gets the Reporting to Ids 
        ///// </summary>
        ///// <returns>DataSet</returns>
        //public bool CheckRatingFillForAll(string ratingOption, string ratingType, int deptid, int projectid, int month, int year, int loginEmpId)
        //{

        //    bool falg = false;
        //    DataAccessClass fourCDetails = new DataAccessClass();
        //    try
        //    {


        //        fourCDetails.OpenConnection(DBConstants.GetDBConnectionString());

        //      //  SqlParameter[] sqlParam = new SqlParameter[6];



        //        sqlParam = new SqlParameter[8];

        //        sqlParam[0] = new SqlParameter(SPParameter.RatingOption, SqlDbType.VarChar);
        //        if (string.IsNullOrEmpty(ratingOption))
        //            sqlParam[0].Value = DBNull.Value;
        //        else
        //            sqlParam[0].Value = ratingOption;

        //        sqlParam[1] = new SqlParameter(SPParameter.RatingType, SqlDbType.VarChar);
        //        if (string.IsNullOrEmpty(ratingType))
        //            sqlParam[1].Value = DBNull.Value;
        //        else
        //            sqlParam[1].Value = ratingType;

        //        sqlParam[2] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.Int);
        //        if (deptid == 0)
        //            sqlParam[2].Value = DBNull.Value;
        //        else
        //            sqlParam[2].Value = deptid;

        //        sqlParam[3] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int);
        //        if (projectid == 0)
        //            sqlParam[3].Value = DBNull.Value;
        //        else
        //            sqlParam[3].Value = projectid;

        //        sqlParam[4] = new SqlParameter(SPParameter.Month, SqlDbType.Int);
        //        if (month == 0)
        //            sqlParam[4].Value = DBNull.Value;
        //        else
        //            sqlParam[4].Value = month;

        //        sqlParam[5] = new SqlParameter(SPParameter.Year, SqlDbType.Int);
        //        if (year == 0)
        //            sqlParam[5].Value = DBNull.Value;
        //        else
        //            sqlParam[5].Value = year;

        //        sqlParam[6] = new SqlParameter(SPParameter.LoginEmpId, SqlDbType.Int);
        //        if (loginEmpId == 0)
        //            sqlParam[6].Value = DBNull.Value;
        //        else
        //            sqlParam[6].Value = loginEmpId;

        //        sqlParam[7] = new SqlParameter(SPParameter.Flag, DbType.Int32);
        //        sqlParam[7].Direction = ParameterDirection.Output;

        //        fourCDetails.ExecuteNonQuerySP(SPNames.Check_RatingFillForAll, sqlParam);

        //        falg = Convert.ToBoolean(sqlParam[7].Value);

        //        return falg;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "IsAllowToFillActionData", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
        //    }

        //    finally
        //    {
        //        fourCDetails.CloseConncetion();
        //    }
        //}


        /// <summary>
        /// Updates Creator Reviewer 
        /// </summary>
        /// <param name="objUpdateEmployee">The object update employee.</param>
        //public bool SubmitReviewRating(int projectIdSelected, int month, int year, string emailId, DataTable dtFinalRating)
        //public bool SubmitReviewRating(string emailId, string ratingOption, DataTable dtFinalRating)
        public bool SubmitReviewRating(string ratingOption, string finalSubmit,  int deptid, int projectid, int month, int year, int loginEmpId, DataTable dtFinalRating)
        {
            objDA = new DataAccessClass();
            //sqlParam = new SqlParameter[5];
            bool flag = false;
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    foreach (DataRow dr in dtFinalRating.Rows)
                    {



                        sqlParam = new SqlParameter[10];  //mahendra start end 8 => 10

                        sqlParam[0] = new SqlParameter(SPParameter.RatingOption, SqlDbType.VarChar);
                        if (string.IsNullOrEmpty(ratingOption))
                            sqlParam[0].Value = DBNull.Value;
                        else
                            sqlParam[0].Value = ratingOption;

                        sqlParam[1] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.Int);
                        if (deptid == 0)
                            sqlParam[1].Value = DBNull.Value;
                        else
                            sqlParam[1].Value = deptid;

                        sqlParam[2] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int);
                        if (projectid == 0)
                            sqlParam[2].Value = DBNull.Value;
                        else
                            sqlParam[2].Value = projectid;

                        sqlParam[3] = new SqlParameter(SPParameter.Month, SqlDbType.Int);
                        if (month == 0)
                            sqlParam[3].Value = DBNull.Value;
                        else
                            sqlParam[3].Value = month;

                        sqlParam[4] = new SqlParameter(SPParameter.Year, SqlDbType.Int);
                        if (year == 0)
                            sqlParam[4].Value = DBNull.Value;
                        else
                            sqlParam[4].Value = year;

                        sqlParam[5] = new SqlParameter(SPParameter.LoginEmpId, SqlDbType.Int);
                        if (loginEmpId == 0)
                            sqlParam[5].Value = DBNull.Value;
                        else
                            sqlParam[5].Value = loginEmpId;

                        sqlParam[6] = new SqlParameter(SPParameter.FBID, SqlDbType.Int);
                        if(string.IsNullOrEmpty(dr[DbTableColumn.FourCId].ToString()))
                            sqlParam[6].Value = DBNull.Value;
                        else
                            sqlParam[6].Value = int.Parse(dr[DbTableColumn.FourCId].ToString());

                        sqlParam[7] = new SqlParameter(SPParameter.finalSubmit, SqlDbType.VarChar);
                        if (string.IsNullOrEmpty(finalSubmit))
                            sqlParam[7].Value = DBNull.Value;
                        else
                            sqlParam[7].Value = finalSubmit;

                        //mahendra start
                        sqlParam[8] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                        if (string.IsNullOrEmpty(dr[DbTableColumn.FourCEmpId].ToString()))
                            sqlParam[8].Value = DBNull.Value;
                        else
                            sqlParam[8].Value = int.Parse(dr[DbTableColumn.FourCEmpId].ToString()); ;

                        sqlParam[9] = new SqlParameter(SPParameter.Flag, SqlDbType.VarChar);
                        if (string.IsNullOrEmpty(dr[DbTableColumn.Flag].ToString()))
                            sqlParam[9].Value = DBNull.Value;
                        else
                            sqlParam[9].Value = dr[DbTableColumn.Flag].ToString();

                        //mahendra end                       

                        //Venkatesh : 4C_Support : 3/3/2014 : Start 
                        if (Utility.IsSupportDept(deptid))
                            objDA.ExecuteNonQuerySP(SPNames.Submit_ReviewRatingSupport, sqlParam);
                        else
                            objDA.ExecuteNonQuerySP(SPNames.Submit_ReviewRating, sqlParam);

                        //Venkatesh : 4C_Support : 3/3/2014 : End

                        //objDA.ExecuteNonQuerySP(SPNames.Submit_ReviewRating, sqlParam);
                    }

                    ts.Complete();
                    flag = true;
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "SubmitReviewRating", EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
            return flag;
        }


        ///// <summary>
        ///// Gets the Reporting to Ids 
        ///// </summary>
        ///// <returns>DataSet</returns>
        //public DataSet GetSupportEmployeeList(string emailId, int month, int year)
        //{
            
        //    DataSet ds = null;
        //    //SqlCommand objCommand = null;
        //    SqlParameter[] sqlParam = new SqlParameter[3];
        //    try
        //    {
        //        objDA = new DataAccessClass();
        //        objDA.OpenConnection(DBConstants.GetDBConnectionString());

        //        ds = new DataSet();

        //        sqlParam[0] = new SqlParameter(SPParameter.EmailID, SqlDbType.VarChar);
        //        if (emailId == null)
        //            sqlParam[0].Value = DBNull.Value;
        //        else
        //            sqlParam[0].Value = emailId;

        //        sqlParam[1] = new SqlParameter(SPParameter.Month, SqlDbType.Int);
        //        sqlParam[1].Value = month;

        //        sqlParam[2] = new SqlParameter(SPParameter.Year, SqlDbType.Int);
        //        sqlParam[2].Value = year;

        //        ds = objDA.GetDataSet(SPNames.GET_SupportEmployeeList, sqlParam);

        //    }

        //    catch (Exception ex)
        //    {
        //        throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetSupportEmployeeList", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
        //    }

        //    finally
        //    {
        //        objDA.CloseConncetion();
        //    }
        //    return ds;
        //}



        ///// <summary>
        ///// Gets the Creator details 
        ///// </summary>
        ///// <returns>DataSet</returns>
        //public DataSet Get4CCreatorDeatilsAtSupportLevel(int month, int year, string fourCRole, string emailId, string RatingFillOrView)
        //{

        //    DataSet ds = null;
        //    try
        //    {
        //        SqlParameter[] sqlParam = new SqlParameter[5];
        //        objDA = new DataAccessClass();
        //        objDA.OpenConnection(DBConstants.GetDBConnectionString());

        //        ds = new DataSet();

        //        sqlParam[0] = new SqlParameter(SPParameter.Month, SqlDbType.Int);
        //        if (month == 0)
        //            sqlParam[0].Value = DBNull.Value;
        //        else
        //            sqlParam[0].Value = month;

        //        sqlParam[1] = new SqlParameter(SPParameter.Year, SqlDbType.VarChar);
        //        if (year == 0)
        //            sqlParam[1].Value = DBNull.Value;
        //        else
        //            sqlParam[1].Value = year;

        //        sqlParam[2] = new SqlParameter(SPParameter.fourCRole, SqlDbType.VarChar);
        //        if (string.IsNullOrEmpty(fourCRole))
        //            sqlParam[2].Value = DBNull.Value;
        //        else
        //            sqlParam[2].Value = fourCRole;

        //        sqlParam[3] = new SqlParameter(SPParameter.EmailID, SqlDbType.VarChar);
        //        if (string.IsNullOrEmpty(emailId))
        //            sqlParam[3].Value = DBNull.Value;
        //        else
        //            sqlParam[3].Value = emailId;

        //        sqlParam[4] = new SqlParameter(SPParameter.RatingFillOrView, SqlDbType.VarChar);
        //        if (string.IsNullOrEmpty(RatingFillOrView))
        //            sqlParam[4].Value = DBNull.Value;
        //        else
        //            sqlParam[4].Value = RatingFillOrView;

        //        ds = objDA.GetDataSet(SPNames.GET_Get4CCreatorDetailsAtSupportLevel, sqlParam);
        //    }

        //    catch (Exception ex)
        //    {
        //        throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "Get4CEmployeeDeatils", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
        //    }

        //    finally
        //    {
        //        objDA.CloseConncetion();
        //    }
        //    return ds;
        //}


        /// <summary>
        /// Gets the Creator details 
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet Get4CViewFeedbackDeatils(int deptd, int projectid, int month, int year, int empId, int fnEmpId, string role)
        {

            DataSet ds = null;
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[7];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                ds = new DataSet();

                sqlParam[0] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.Int);
                if (deptd == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = deptd;

                sqlParam[1] = new SqlParameter(SPParameter.PROJECTID, SqlDbType.Int);
                if (projectid == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = projectid;

                sqlParam[2] = new SqlParameter(SPParameter.Month, SqlDbType.Int);
                if (month == 0)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = month;

                sqlParam[3] = new SqlParameter(SPParameter.Year, SqlDbType.Int);
                if (year == 0)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = year;

                sqlParam[4] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (empId == 0)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = empId;

                sqlParam[5] = new SqlParameter(SPParameter.FunctionalManagerId, SqlDbType.Int);
                if (fnEmpId == 0)
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = fnEmpId;

                sqlParam[6] = new SqlParameter(SPParameter.fourCRole, SqlDbType.VarChar);
                if (fnEmpId == 0)
                    sqlParam[6].Value = DBNull.Value;
                else
                    sqlParam[6].Value = role;


                ds = objDA.GetDataSet(SPNames.GET_View4CFeedbackDetails, sqlParam);
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "Get4CEmployeeDeatils", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
            return ds;
        }


        /// <summary>
        /// Gets the Creator details 
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet Get4CViewEmployeeFromRMS(int deptd, int projectid, int month, int year, int functionalManagerId)
        {

            DataSet ds = null;
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[5];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                ds = new DataSet();

                sqlParam[0] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.Int);
                if (deptd == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = deptd;

                sqlParam[1] = new SqlParameter(SPParameter.PROJECTID, SqlDbType.Int);
                if (projectid == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = projectid;

                sqlParam[2] = new SqlParameter(SPParameter.Month, SqlDbType.Int);
                if (month == 0)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = month;

                sqlParam[3] = new SqlParameter(SPParameter.Year, SqlDbType.Int);
                if (year == 0)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = year;

                sqlParam[4] = new SqlParameter(SPParameter.FunManagerId, SqlDbType.Int);
                if (functionalManagerId == 0)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = functionalManagerId;

                ds = objDA.GetDataSet(SPNames.Get4CViewEmployeeFromRMS, sqlParam);
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "Get4CEmployeeDeatils", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
            return ds;
        }



        /// <summary>
        /// Gets the Reporting to Ids 
        /// </summary>
        /// <returns>DataSet</returns>
        public void CheckReviewerIsAllowForDepartment(int deptId, int projectId, string creator, string reviewer, ref bool isAllowCreator, ref bool isAllowReviewer)
        {

            bool falg = false;
            DataAccessClass fourCDetails = new DataAccessClass();
            try
            {

                fourCDetails.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[6];

                sqlParam[0] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.Int);
                if (deptId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = deptId;

                sqlParam[1] = new SqlParameter(SPParameter.PROJECTID, SqlDbType.Int);
                if (projectId == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = projectId;

                sqlParam[2] = new SqlParameter(SPParameter.CreatorId, creator);

                sqlParam[3] = new SqlParameter(SPParameter.ReviewerId, reviewer);


                sqlParam[4] = new SqlParameter(SPParameter.FlagCreator, DbType.Int32);
                sqlParam[4].Direction = ParameterDirection.Output;

                sqlParam[5] = new SqlParameter(SPParameter.FlagReviewer, DbType.Int32);
                sqlParam[5].Direction = ParameterDirection.Output;

                fourCDetails.ExecuteNonQuerySP(SPNames.Check_AllowReviewer, sqlParam);

                isAllowCreator = Convert.ToBoolean(sqlParam[4].Value);
                isAllowReviewer = Convert.ToBoolean(sqlParam[5].Value);

                
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "CheckReviewerIsAllowForDepartment", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                fourCDetails.CloseConncetion();
            }
        }


        /// <summary>
        /// Not Applicable validation
        /// </summary>
        /// <returns>DataSet</returns>
        public bool IsValidForNotApplication(int deptid, int projectId, int month, int year, int empId)
        {

            bool falg = false;
            DataAccessClass fourCDetails = new DataAccessClass();
            try
            {


                fourCDetails.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[6];


                sqlParam[0] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.Int);
                sqlParam[0].Value = deptid;

                sqlParam[1] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int);
                sqlParam[1].Value = projectId;

                sqlParam[2] = new SqlParameter(SPParameter.Month, SqlDbType.Int);
                sqlParam[2].Value = month;

                sqlParam[3] = new SqlParameter(SPParameter.Year, SqlDbType.Int);
                sqlParam[3].Value = year;

                sqlParam[4] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[4].Value = empId;

                sqlParam[5] = new SqlParameter(SPParameter.Flag, DbType.Int32);
                sqlParam[5].Direction = ParameterDirection.Output;

                fourCDetails.ExecuteNonQuerySP(SPNames.Check_IsAllowForNotApplicableOption, sqlParam);

                falg = Convert.ToBoolean(sqlParam[5].Value);

                return falg;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "IsAllowToFillActionData", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                fourCDetails.CloseConncetion();
            }
        }



        /// <summary>
        /// Gets the Redirect Month
        /// </summary>
        /// <returns>DataSet</returns>
        public void GetRedirectMonth(int loginEmpId, int DeptId, int ProjectId, int month, int year, int mode, ref int redirectMonth, ref int redirectYear)
        {

            List<string> lstRights = new List<string> { };
            DataAccessClass objDAGetMRFId = new DataAccessClass();

            try
            {
                SqlParameter[] sqlParam = new SqlParameter[6];

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, DbType.String);
                sqlParam[0].Value = loginEmpId;

                sqlParam[1] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.Int);
                sqlParam[1].Value = DeptId;

                sqlParam[2] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int);
                sqlParam[2].Value = ProjectId;

                sqlParam[3] = new SqlParameter(SPParameter.Month, SqlDbType.Int);
                sqlParam[3].Value = month;

                sqlParam[4] = new SqlParameter(SPParameter.Year, SqlDbType.Int);
                sqlParam[4].Value = year;

                sqlParam[5] = new SqlParameter(SPParameter.RatingOption, SqlDbType.Int);
                sqlParam[5].Value = mode;

                //Open the connection to DB
                objDAGetMRFId.OpenConnection(DBConstants.GetDBConnectionString());

                //Venkatesh : 4C_Support : 26/2/2014 : Start 
                if (Utility.IsSupportDept(DeptId))
                    objDataReader = objDAGetMRFId.ExecuteReaderSP(SPNames.GET_RedirectMonthSupport, sqlParam);
                else
                    objDataReader = objDAGetMRFId.ExecuteReaderSP(SPNames.GET_RedirectMonth, sqlParam);

                //Venkatesh : 4C_Support : 26/2/2014 : End

                while (objDataReader.Read())
                {
                    if (!string.IsNullOrEmpty(objDataReader["RedirectedMonth"].ToString()))
                        redirectMonth = int.Parse(objDataReader["RedirectedMonth"].ToString());

                    if (!string.IsNullOrEmpty(objDataReader["RedirectedYear"].ToString()))
                        redirectYear = int.Parse(objDataReader["RedirectedYear"].ToString());               

                }
               
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetFilteredCreatorApproverDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                //objDA.CloseConncetion();
                objDAGetMRFId.CloseConncetion();
            }

        }


        /// <summary>
        /// Not Applicable validation
        /// </summary>
        /// <returns>DataSet</returns>
        public bool Check4CRatingFillForAll(int EmpId, DateTime dtReleaseDate, int projectId)
        {

            bool falg = false;
            DataAccessClass fourCDetails = new DataAccessClass();
            try
            {


                fourCDetails.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[4];


                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[0].Value = EmpId;

                sqlParam[1] = new SqlParameter(SPParameter.ReleaseDate, SqlDbType.DateTime);
                sqlParam[1].Value = dtReleaseDate;

                sqlParam[2] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int);
                sqlParam[2].Value = projectId;

                sqlParam[3] = new SqlParameter(SPParameter.Flag, DbType.Int32);
                sqlParam[3].Direction = ParameterDirection.Output;

                fourCDetails.ExecuteNonQuerySP(SPNames.RMS_RelaseVal_4CFilledForAll, sqlParam);

                falg = Convert.ToBoolean(sqlParam[3].Value);

                return falg;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "IsAllowToFillActionData", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                fourCDetails.CloseConncetion();
            }
        }


        /// <summary>
        /// Gets the Reporting to Ids 
        /// </summary>
        /// <returns>DataSet</returns>
        //public DataSet GetActionReport(int loginEmpId, string EmpId, string ActionOwnerId, string deptid, string projectId, string CType, string ColorRating, int MonthDuration,bool IsSupportdept)
        public DataSet GetActionReport(int loginEmpId, string EmpId, string ActionOwnerId, string deptid, string projectId, string CType, string ColorRating, int MonthDuration)
        {

            DataSet ds = null;
            try
            {

               
                //objConnection = new SqlConnection(ConnStr);
                //objConnection.Open();
                //objCommand = new SqlCommand(SPNames.GET_4C_ActionReport, objConnection);
                //objCommand.CommandType = CommandType.StoredProcedure;
                //objCommand.Parameters.Add(SPParameter.LoginEmpId, loginEmpId);
                //objReader = objCommand.ExecuteReader();
                //DataTable objDataTable = new DataTable();
                //objDataTable.Load(objReader);
                //return objDataTable;

                SqlParameter[] sqlParam = new SqlParameter[8];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                ds = new DataSet();

                sqlParam[0] = new SqlParameter(SPParameter.LoginEmpId, SqlDbType.Int);
                if (loginEmpId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = loginEmpId;

                sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.VarChar);
                if (string.IsNullOrEmpty(EmpId))
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = EmpId;

                 sqlParam[2] = new SqlParameter(SPParameter.ActionOwnerId, SqlDbType.VarChar);
                 if (string.IsNullOrEmpty(ActionOwnerId))
                    sqlParam[2].Value = DBNull.Value;
                else
                     sqlParam[2].Value = ActionOwnerId;


                sqlParam[3] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.VarChar);
                if (string.IsNullOrEmpty(deptid))
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = deptid;

                sqlParam[4] = new SqlParameter(SPParameter.PROJECTID, SqlDbType.VarChar);
                if (string.IsNullOrEmpty(projectId))
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = projectId;

                sqlParam[5] = new SqlParameter(SPParameter.CTYPE, SqlDbType.VarChar);
                if (string.IsNullOrEmpty(CType))
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = CType;

                sqlParam[6] = new SqlParameter(SPParameter.ColorRating, SqlDbType.VarChar);
                if (string.IsNullOrEmpty(ColorRating))
                    sqlParam[6].Value = DBNull.Value;
                else
                    sqlParam[6].Value = ColorRating;

                sqlParam[7] = new SqlParameter(SPParameter.MonthDuration, SqlDbType.Int);
                if (MonthDuration == 0)
                    sqlParam[7].Value = DBNull.Value;
                else
                    sqlParam[7].Value = MonthDuration;


                //Venkatesh : 4C_Support : 3/3/2014 : Start 
                //if (IsSupportdept)
                //    ds = objDA.GetDataSet(SPNames.GET_4C_ActionReportSupport, sqlParam);
                //else
                    ds = objDA.GetDataSet(SPNames.GET_4C_ActionReport, sqlParam);

                //Venkatesh : 4C_Support : 3/3/2014 : End
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "Get4CEmployeeDeatils", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
            return ds;
        }

        /// <summary>
        /// Gets the Reporting to Ids 
        /// </summary>
        /// <returns>DataSet</returns>
        //public DataSet GetConsolidatedReport(int loginEmpId, string EmpId, string designationId, string deptid, string projectId, int MonthDuration, bool IsSupportdept)
        public DataSet GetConsolidatedReport(int loginEmpId, string EmpId, string designationId, string deptid, string projectId, int MonthDuration)
        {

            //DataTable dsEmp = null;
            DataSet ds = null;
            try
            {

                //string ConnStr = DBConstants.GetDBConnectionString();
                ////string ConnStr = Common.DBConstants.GetDBConnectionString();

                //objConnection = new SqlConnection(ConnStr);
                //objConnection.Open();
                //objCommand = new SqlCommand(SPNames.GET_4C_ConsolidatedReport, objConnection);
                //objCommand.CommandType = CommandType.StoredProcedure;
                //objCommand.Parameters.Add(SPParameter.LoginEmpId, loginEmpId);
                //objReader = objCommand.ExecuteReader();
                //DataTable objDataTable = new DataTable();
                //objDataTable.Load(objReader);
                //return objDataTable;

               
          
                SqlParameter[] sqlParam = new SqlParameter[6];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                ds = new DataSet();

                sqlParam[0] = new SqlParameter(SPParameter.LoginEmpId, SqlDbType.Int);
                if (loginEmpId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = loginEmpId;

                sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.VarChar);
                if (string.IsNullOrEmpty(EmpId))
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = EmpId;

                sqlParam[2] = new SqlParameter(SPParameter.DesignationId, SqlDbType.VarChar);
                if (string.IsNullOrEmpty(designationId))
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = designationId;

               sqlParam[3] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.VarChar);
                if (string.IsNullOrEmpty(deptid))
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = deptid;

                sqlParam[4] = new SqlParameter(SPParameter.PROJECTID, SqlDbType.VarChar);
                if (string.IsNullOrEmpty(projectId))
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = projectId;

                sqlParam[5] = new SqlParameter(SPParameter.MonthDuration, SqlDbType.Int);
                if (MonthDuration == 0)
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = MonthDuration;

                

                //Venkatesh : 4C_Support : 3/3/2014 : Start 
                //if (IsSupportdept)
                //    ds = objDA.GetDataSet(SPNames.GET_4C_ConsolidatedReportSupport, sqlParam);
                //else
                    ds = objDA.GetDataSet(SPNames.GET_4C_ConsolidatedReport, sqlParam);

                //Venkatesh : 4C_Support : 3/3/2014 : End

            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "Get4CEmployeeDeatils", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
            return ds;
        }


        /// <summary>
        /// Gets the Reporting to Ids 
        /// </summary>
        /// <returns>DataSet</returns>
        /// public DataSet GetAnalysisReport(int loginEmpId, int period, string deptId, string projectId, string designationId, string CType, bool IsSupportdept)
        public DataSet GetAnalysisReport(int loginEmpId, int period, string deptId, string projectId, string designationId, string CType)
        {

            //DataTable dsEmp = null;
            DataSet ds = null;
            try
            {

                SqlParameter[] sqlParam = new SqlParameter[6];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                ds = new DataSet();

                sqlParam[0] = new SqlParameter(SPParameter.LoginEmpId, SqlDbType.Int);
                if (loginEmpId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = loginEmpId;

                sqlParam[1] = new SqlParameter(SPParameter.Month, SqlDbType.Int);
                if (period == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = period;

                sqlParam[2] = new SqlParameter(SPParameter.DeptId, SqlDbType.VarChar);
                if (string.IsNullOrEmpty(deptId))
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = deptId;

                sqlParam[3] = new SqlParameter(SPParameter.ProjectName, SqlDbType.VarChar);
                if (string.IsNullOrEmpty(projectId))
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = projectId;

                sqlParam[4] = new SqlParameter(SPParameter.DesignationId, SqlDbType.VarChar);
                if (string.IsNullOrEmpty(designationId))
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = designationId;

                sqlParam[5] = new SqlParameter(SPParameter.fourCType, SqlDbType.VarChar);
                if (string.IsNullOrEmpty(CType))
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = CType;

                
                //Venkatesh : 4C_Support : 3/3/2014 : Start 
                //if (IsSupportdept)
                //    ds = objDA.GetDataSet(SPNames.GET_4C_AnalysisReportSupport, sqlParam);
                //else
                    ds = objDA.GetDataSet(SPNames.GET_4C_AnalysisReport, sqlParam);

                //Venkatesh : 4C_Support : 3/3/2014 : End
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "Get4CEmployeeDeatils", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
            return ds;
        }

        /// <summary>
        /// Gets the Reporting to Ids 
        /// </summary>
        /// <returns>DataSet</returns>
        //public DataSet GetStatusReport(int loginEmpId, int month, int year, bool IsSupportdept)
        public DataSet GetStatusReport(int loginEmpId, int month, int year)
        {

            //DataTable dsEmp = null;
            DataSet ds = null;
            try
            {

                SqlParameter[] sqlParam = new SqlParameter[3];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                ds = new DataSet();

                sqlParam[0] = new SqlParameter(SPParameter.LoginEmpId, SqlDbType.Int);
                if (loginEmpId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = loginEmpId;

                sqlParam[1] = new SqlParameter(SPParameter.Month, SqlDbType.Int);
                if (month == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = month;

                sqlParam[2] = new SqlParameter(SPParameter.Year, SqlDbType.Int);
                if (year == 0)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = year;

                //sqlParam[2] = new SqlParameter(SPParameter.Month, SqlDbType.Int);
                //if (month == 0)
                //    sqlParam[2].Value = DBNull.Value;
                //else
                //    sqlParam[2].Value = month;

                //sqlParam[3] = new SqlParameter(SPParameter.Year, SqlDbType.Int);
                //if (year == 0)
                //    sqlParam[3].Value = DBNull.Value;
                //else
                //    sqlParam[3].Value = year;

                //sqlParam[4] = new SqlParameter(SPParameter.FunManagerId, SqlDbType.Int);
                //if (functionalManagerId == 0)
                //    sqlParam[4].Value = DBNull.Value;
                //else
                //    sqlParam[4].Value = functionalManagerId;


                //Venkatesh : 4C_Support : 3/3/2014 : Start 
                //if (IsSupportdept)
                //    ds = objDA.GetDataSet(SPNames.GET_4C_StatusReportSupport, sqlParam);
                //else
                    ds = objDA.GetDataSet(SPNames.GET_4C_StatusReport, sqlParam);

                //Venkatesh : 4C_Support : 3/3/2014 : End
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "Get4CEmployeeDeatils", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
            return ds;
        }


        /// <summary>
        /// Gets the Reporting to Ids 
        /// </summary>
        /// <returns>DataSet</returns>
        //public DataSet GetMovementReport(int loginEmpId, int month, bool IsSupportdept)
        public DataSet GetMovementReport(int loginEmpId, int month)
        {

            //DataTable dsEmp = null;
            DataSet ds = null;
            try
            {

                SqlParameter[] sqlParam = new SqlParameter[2];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                ds = new DataSet();

                sqlParam[0] = new SqlParameter(SPParameter.LoginEmpId, SqlDbType.Int);
                if (loginEmpId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = loginEmpId;

                sqlParam[1] = new SqlParameter(SPParameter.Month, SqlDbType.Int);
                if (month == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = month;

                //sqlParam[2] = new SqlParameter(SPParameter.Year, SqlDbType.Int);
                //if (year == 0)
                //    sqlParam[2].Value = DBNull.Value;
                //else
                //    sqlParam[2].Value = year;

                //sqlParam[2] = new SqlParameter(SPParameter.Month, SqlDbType.Int);
                //if (month == 0)
                //    sqlParam[2].Value = DBNull.Value;
                //else
                //    sqlParam[2].Value = month;

                //sqlParam[3] = new SqlParameter(SPParameter.Year, SqlDbType.Int);
                //if (year == 0)
                //    sqlParam[3].Value = DBNull.Value;
                //else
                //    sqlParam[3].Value = year;

                //sqlParam[4] = new SqlParameter(SPParameter.FunManagerId, SqlDbType.Int);
                //if (functionalManagerId == 0)
                //    sqlParam[4].Value = DBNull.Value;
                //else
                //    sqlParam[4].Value = functionalManagerId;


                //Venkatesh : 4C_Support : 3/3/2014 : Start 
                //if (IsSupportdept)
                //    ds = objDA.GetDataSet(SPNames.GET_4C_MovementReportSupport, sqlParam);
                //else
                    ds = objDA.GetDataSet(SPNames.GET_4C_MovementReport, sqlParam);

                //Venkatesh : 4C_Support : 3/3/2014 : End

            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "Get4CEmployeeDeatils", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
            return ds;
        }

        /// <summary>
        /// Gets the Reporting to Ids 
        /// </summary>
        /// <returns>DataSet</returns>
        //public DataSet GetCountReport(int loginEmpId, int month, bool IsSupportdept)
        public DataSet GetCountReport(int loginEmpId, int month)
        {
            DataSet ds = null;
            try
            {

                SqlParameter[] sqlParam = new SqlParameter[2];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                ds = new DataSet();

                sqlParam[0] = new SqlParameter(SPParameter.LoginEmpId, SqlDbType.Int);
                if (loginEmpId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = loginEmpId;

                sqlParam[1] = new SqlParameter(SPParameter.Month, SqlDbType.Int);
                if (month == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = month;

                //Venkatesh : 4C_Support : 3/3/2014 : Start 
                //if (IsSupportdept)
                //    ds = objDA.GetDataSet(SPNames.GET_4C_CountReportSupport, sqlParam);
                //else
                    ds = objDA.GetDataSet(SPNames.GET_4C_CountReport, sqlParam);

                //Venkatesh : 4C_Support : 3/3/2014 : End

            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "Get4CEmployeeDeatils", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
            return ds;
        }


        /// <summary>
        /// Function will fill all the dropdowns
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection Fill4CDesignationReportDL(int empId, string deptval)
        {
            //Declare DataAccess Class Object
            objDA = new DataAccessClass();
            SqlParameter[] sqlParam = new SqlParameter[2];

            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[0].Value = empId;

                sqlParam[1] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.VarChar);
                sqlParam[1].Value = deptval;

                objReader = objDA.ExecuteReaderSP(SPNames.Get_4CReportDesignation, sqlParam);

                while (objReader.Read())
                {
                    if (objReader.GetValue(1).ToString() != "Abort")
                    {
                        KeyValue<string> keyValue = new KeyValue<string>();
                        keyValue.KeyName = objReader.GetValue(0).ToString();
                        keyValue.Val = objReader.GetValue(1).ToString();
                        raveHRCollection.Add(keyValue);
                    }
                }
                return raveHRCollection;

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

                objDA.CloseConncetion();
            }
        }


        /// <summary>
        /// Gets the crator reviewer details
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet GetCreatorReviewerMailsDeatils(int deptId, int projectId, int creatorEmpId)
        {

            DataSet dsEmp = null;
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[3];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                dsEmp = new DataSet();

                sqlParam[0] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.Int);
                if (deptId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = deptId;

                sqlParam[1] = new SqlParameter(SPParameter.PROJECTID, SqlDbType.Int);
                if (projectId == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = projectId;

                sqlParam[2] = new SqlParameter(SPParameter.CreatorId, SqlDbType.Int);
                if (creatorEmpId == 0)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = creatorEmpId;

                //Venkatesh : 4C_Support : 26/2/2014 : Start 
                if (Utility.IsSupportDept(deptId))
                    dsEmp = objDA.GetDataSet(SPNames.GET_Get4CCreatorReviewerMailsDetailsSupport, sqlParam);
                else
                    dsEmp = objDA.GetDataSet(SPNames.GET_Get4CCreatorReviewerMailsDetails, sqlParam);

                //Venkatesh : 4C_Support : 26/2/2014 : End



            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "Get4CEmployeeDeatils", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
            return dsEmp;
        }



        /// <summary>
        /// Gets the crator reviewer details
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet GetRejectRatingMailsDeatils(int fbId, int LoginEmpId)
        {

            DataSet dsEmp = null;
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[2];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                dsEmp = new DataSet();

                sqlParam[0] = new SqlParameter(SPParameter.FBID, SqlDbType.Int);
                if (fbId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = fbId;

                sqlParam[1] = new SqlParameter(SPParameter.LoginEmpId, SqlDbType.Int);
                if (LoginEmpId == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = LoginEmpId;

                dsEmp = objDA.GetDataSet(SPNames.GET_Get4CRejectedMailsDetails, sqlParam);
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "Get4CEmployeeDeatils", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
            return dsEmp;
        }



        /// <summary>
        /// Reject 4C rating
        /// </summary>
        /// <param name="objUpdateEmployee">The object update employee.</param>
        public bool Reject4CRating(int fbId, string rejectRemarks, int loginEmpId)
        {
            //objDA = new DataAccessClass();
            DataAccessClass fourCDetails = new DataAccessClass();
            //sqlParam = new SqlParameter[5];
            bool flag = false;
            try
            {
                    fourCDetails.OpenConnection(DBConstants.GetDBConnectionString());

                    SqlParameter[] sqlParam = new SqlParameter[4];

                    sqlParam[0] = new SqlParameter(SPParameter.FBID, SqlDbType.Int);
                    sqlParam[0].Value = fbId;

                    sqlParam[1] = new SqlParameter(SPParameter.LoginEmpId, SqlDbType.Int);
                    sqlParam[1].Value = loginEmpId;

                    sqlParam[2] = new SqlParameter(SPParameter.RemarksReviewer, SqlDbType.VarChar);
                    sqlParam[2].Value = rejectRemarks;

                    sqlParam[3] = new SqlParameter(SPParameter.Flag, DbType.Int32);
                    sqlParam[3].Direction = ParameterDirection.Output;

                    fourCDetails.ExecuteNonQuerySP(SPNames.Reject_4CRating, sqlParam);

                    flag = Convert.ToBoolean(sqlParam[3].Value);

                    flag = true;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "SubmitReviewRating", EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                fourCDetails.CloseConncetion();
            }
            return flag;
        }

        /// <summary>
        /// Export To Excel Sent For Review Ratings
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet ExportToExcelSentForReviewRatings(int loginEmpId)
        {

            //DataTable dsEmp = null;
            DataSet ds = null;
            try
            {

                SqlParameter[] sqlParam = new SqlParameter[1];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                ds = new DataSet();

                sqlParam[0] = new SqlParameter(SPParameter.LoginEmpId, SqlDbType.Int);
                if (loginEmpId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = loginEmpId;

                ds = objDA.GetDataSet(SPNames.GET_4C_ExportToExcelSentForReview, sqlParam);

            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "Get4CEmployeeDeatils", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
            return ds;
        }

        //Ishwar Patil 25082015 Start
        //Desc : 4C hightlight CR
        public DataTable Get4C_ActionDetailsHighlightForDashboard(int EmpId, int deptId, int projectId, int month, int year)
        {

            DataTable dsEmpE = new DataTable();
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[5];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                var dsEmp = new DataSet();

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (EmpId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = EmpId;

                sqlParam[1] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.Int);
                if (deptId == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = deptId;

                sqlParam[2] = new SqlParameter(SPParameter.PROJECTID, SqlDbType.Int);
                if (projectId == 0)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = projectId;

                sqlParam[3] = new SqlParameter(SPParameter.Month, SqlDbType.Int);
                if (month == 0)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = month;

                sqlParam[4] = new SqlParameter(SPParameter.Year, SqlDbType.Int);
                if (year == 0)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = year;

                dsEmp = objDA.GetDataSet(SPNames.GET_4C_GetActionDetailsHighlightForDashboard, sqlParam);

                if (dsEmp != null && dsEmp.Tables.Count >= 1)
                {
                    dsEmpE = dsEmp.Tables[0];
                }
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "Get4C_ActionDetailsHighlightForDashboard", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
            return dsEmpE;
        }
        //Ishwar Patil 25082015 End
    }
}
