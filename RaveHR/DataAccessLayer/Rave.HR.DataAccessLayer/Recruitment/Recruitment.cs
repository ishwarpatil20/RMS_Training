//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           MRFDetail.aspx      
//  Author:         Chhaya Gunjal 
//  Date written:   03/09/2009/ 10:58:30 AM
//  Description:    This class  provides the Data Access layer methods for Recruitment module.
//
//  Amendments
//  Date                    Who              Ref     Description
//  ----                    -----------      ---     -----------
//  03/09/2009 10:58:30 AM  Chhaya Gunjal    n/a     Created    
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Common;
using Common.Constants;
using BusinessEntities;
using Rave.HR.DataAccessLayer.Employees;

namespace Rave.HR.DataAccessLayer.Recruitment
{
    public class Recruitment
    {

        #region Private Field Members

        /// <summary>
        /// Data Access class which used to open connection and execute.
        /// </summary>
        private static DataAccessClass objDA;

        /// <summary>
        ///  To retrive record of SQL.
        /// </summary>
        private static SqlDataReader objDataReader;

        /// <summary>
        ///  The parameter of SQL ehich used as parameter in stored procedure.
        /// </summary>
        private static SqlParameter[] objSqlParameter;

        /// <summary>
        ///  Business Entities of MRFDetails.
        /// </summary>
        private static BusinessEntities.MRFDetail objMrfDetail;

        /// <summary>
        ///  Business Entities of collection.
        /// </summary>
        private static BusinessEntities.RaveHRCollection raveHRCollection;

        /// <summary>
        ///  Business Entities of Recruitment.
        /// </summary>
        private static BusinessEntities.Recruitment objRecruitment;
        #endregion

        #region Constants
        private const string CLASS_NAME_RECRUITMENT = "Recruitment";
        private const string GETPROJECTNAME = "GetProjectName";
        private const string GETRECRUITMENTSUMMARYFORPAGELOAD = "GetRecruitmentSummaryForPageLoad";
        private const string GETRECRUITMENTSUMMARYFORFILTERANDPAGING = "GetRecruitmentSummaryForFilterAndPaging";
        private const string GET_MRF_CODE = "GetMrfCode";
        private const string VIEW_MRF_CODE = "ViewMrfCode";
        private const string GET_MRF_DETAIL = "GetMrfDetail";
        private const string GET_RECRUITMENT_DETAIL = "GetRecruitmentDetail";
        private const string GET_MRF_RESPONSIBLE_PERSON = "GetMRfResponsiblePersonName";
        private const string GET_PROJECT_NAME = "GetProjectName";
        private const string ADD_PIPELINE_DETAILS = "AddPipelineDetails";
        private const string REMOVE_PIPELINE_DETAILS = "RemovePipelineDetails";
        private const string EDIT_PIPELINE_DETAILS = "EditPipelineDetails";
        private const string GET_PROJECT_MANAGER_BY_PROJECTID="GetProjectManagerByProjectId";
        private const string GET_DEPARMENT_HEAD_BY_DEPARTMENTID = "GetDepartmentHeadByDepartmentId";
        private const string GET_REPORTINGTO_BY_EMPLOYEEID = "GetReportingToByEmployeeId";

        #endregion

        #region Method
        /// <summary>
        /// Gets the MRF Code whose status is pending external allocation.
        /// </summary>
        public static BusinessEntities.RaveHRCollection GetMrfCode()
        {
            // Initialise Data Access Class object
            DataAccessClass objDAGetMrfCode = new DataAccessClass();

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();
            try
            {
                //Open the connection to DB
                objDAGetMrfCode.OpenConnection(DBConstants.GetDBConnectionString());

                //Execute the SP
                objDataReader = objDAGetMrfCode.ExecuteReaderSP(SPNames.Recruitment_GetMRFCode);
                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    KeyValue<string> keyValue = new KeyValue<string>();
                    keyValue.KeyName = objDataReader.GetValue(0).ToString();
                    keyValue.Val = objDataReader.GetValue(1).ToString();
                    // Add the object to Collection
                    raveHRCollection.Add(keyValue);
                }
                // Return the Collection
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RECRUITMENT, "GetMrfCode", EventIDConstants.RAVE_HR_RECRUITMENT_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDAGetMrfCode.CloseConncetion();
            }
            return raveHRCollection;
        }

        /// <summary>
        /// Gets the MRF Code whose status is pending expected resource join.
        /// </summary>
        /// <returns>Collection</returns>
        public static BusinessEntities.RaveHRCollection ViewMrfCode()
        {
            // Initialise Data Access Class object
            DataAccessClass objDAViewMrfCode = new DataAccessClass();

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();
            try
            {

                //Open the connection to DB
                objDAViewMrfCode.OpenConnection(DBConstants.GetDBConnectionString());

                //Execute the SP
                objDataReader = objDAViewMrfCode.ExecuteReaderSP(SPNames.Recruitment_ViewMRFCode);
                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    KeyValue<string> keyValue = new KeyValue<string>();
                    keyValue.KeyName = objDataReader.GetValue(0).ToString();
                    keyValue.Val = objDataReader.GetValue(1).ToString();
                    // Add the object to Collection
                    raveHRCollection.Add(keyValue);
                }
                // Return the Collection
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RECRUITMENT, "ViewMrfCode", EventIDConstants.RAVE_HR_RECRUITMENT_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDAViewMrfCode.CloseConncetion();

            }
            return raveHRCollection;
        }

        /// <summary>
        /// Gets the MRF Detaill for particular MRF.
        /// </summary>
        /// <returns>Collection</returns>
        public static BusinessEntities.Recruitment GetMrfDetail(int mrfId)
        {
            DataAccessClass objDAGetMrfDetail = new DataAccessClass();
            try
            {
                BusinessEntities.Recruitment recruitment = new BusinessEntities.Recruitment();
                objDAGetMrfDetail.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter(SPParameter.MRFId, mrfId);
                objDataReader = objDAGetMrfDetail.ExecuteReaderSP(SPNames.Recruitment_GetMRFDetails, param);
                while (objDataReader.Read())
                {
                    recruitment.DepartmentId = Convert.ToInt32(objDataReader[DbTableColumn.DepartmentId]);
                    recruitment.Department = Convert.ToString(objDataReader[DbTableColumn.DeptName]);
                    recruitment.ClientName = Convert.ToString(objDataReader[DbTableColumn.ClientName]);
                    recruitment.ProjectName = Convert.ToString(objDataReader[DbTableColumn.ProjectName]);
                    recruitment.ReportingTo = Convert.ToString(objDataReader[DbTableColumn.ReportingTo]);
                    recruitment.EmailIdOfCreatedByMRF = Convert.ToString(objDataReader[DbTableColumn.EmailId]);
                    recruitment.MRFCode = Convert.ToString(objDataReader[DbTableColumn.MRFCode]);
                    //recruitment Role is the role which is designation at the time of MRF.
                    recruitment.Role = Convert.ToString(objDataReader[DbTableColumn.Role]);
                    recruitment.ProjectId = Convert.ToInt32(objDataReader[DbTableColumn.ProjectId]);
                    // Rajan Kumar : Issue 45752 : 03/01/2014 : Starts                        			 
                    // Desc : When MRF is raised against department then MRF end date is not mandatory (remove validation).
                    if (objDataReader[DbTableColumn.ContractDuration] != DBNull.Value)
                    {
                        recruitment.ContractDuration = Convert.ToInt32(objDataReader[DbTableColumn.ContractDuration]);
                    }
                    else 
                    {
                        recruitment.ContractDuration = 0;
                    }
                    // Rajan Kumar : Issue 45752 : 03/01/2014: Ends                     
                    recruitment.ResourceBusinessUnitID = Convert.ToInt32(objDataReader[DbTableColumn.ResourceBusinessUnitID]);
                    recruitment.ResourceBussinessUnitName = objDataReader[DbTableColumn.ResourceBusinessUnitName].ToString();
                    recruitment.MRFPurpose = objDataReader[DbTableColumn.MRFPurpose].ToString();    
                        

                }
                return recruitment;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RECRUITMENT, "GetMrfDetail", EventIDConstants.RAVE_HR_RECRUITMENT_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDAGetMrfDetail.CloseConncetion();

            }
        }

        /// <summary>
        /// Gets the MRF Detaill for particular Candidate.
        /// </summary>
        /// <returns>Collection</returns>
        public static BusinessEntities.Recruitment GetRecruitmentDetail(int candidateId)
        {
            DataAccessClass objDAGetRecruitmentDetail = new DataAccessClass();
            try
            {
                BusinessEntities.Recruitment recruitment = new BusinessEntities.Recruitment();
                objDAGetRecruitmentDetail.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter(SPParameter.CandidateId, candidateId);
                objDataReader = objDAGetRecruitmentDetail.ExecuteReaderSP(SPNames.Recruitment_GetRecruitmentDetails, param);
                while (objDataReader.Read())
                {
                    recruitment.MRFId = Convert.ToInt32(objDataReader[DbTableColumn.MRFID]);
                    recruitment.MRFCode = Convert.ToString(objDataReader[DbTableColumn.MRFCode]);
                    recruitment.DepartmentId = Convert.ToInt32(objDataReader[DbTableColumn.DepartmentId]);
                    recruitment.Department = Convert.ToString(objDataReader[DbTableColumn.DeptName]);
                    recruitment.ClientName = Convert.ToString(objDataReader[DbTableColumn.ClientName]);
                    recruitment.ProjectName = Convert.ToString(objDataReader[DbTableColumn.ProjectName]);
                    recruitment.ProjectId = Convert.ToInt32(objDataReader[DbTableColumn.ProjectId]);
                    recruitment.ReportingTo = Convert.ToString(objDataReader[DbTableColumn.ReportingTo]);
                    recruitment.ActualCTC = Convert.ToDecimal(objDataReader[DbTableColumn.ActualCTC]);
                    recruitment.BandId = Convert.ToInt32(objDataReader[DbTableColumn.Band]);
                    recruitment.CandidateId = Convert.ToInt32(objDataReader[DbTableColumn.CandidateId]);
                    recruitment.DesignationId = Convert.ToInt32(objDataReader[DbTableColumn.Designation]);
                    recruitment.Designation = Convert.ToString(objDataReader[DbTableColumn.Role]);
                    recruitment.EmployeeTypeId = Convert.ToInt32(objDataReader[DbTableColumn.EmployeeType]);
                    recruitment.ExpectedJoiningDate = Convert.ToDateTime(objDataReader[DbTableColumn.ExpectedDateOfJoining]);
                    recruitment.FirstName = Convert.ToString(objDataReader[DbTableColumn.FirstName]);
                    recruitment.LastName = Convert.ToString(objDataReader[DbTableColumn.LastName]);
                    recruitment.MiddleName = Convert.ToString(objDataReader[DbTableColumn.MiddleName]);
                    recruitment.PrefixId = Convert.ToInt32(objDataReader[DbTableColumn.Prefix]);
                    recruitment.Role = Convert.ToString(objDataReader[DbTableColumn.Role]);
                    recruitment.Location = Convert.ToString(objDataReader[DbTableColumn.Location]);
                    recruitment.ResourceBussinessUnit = Convert.ToInt32(objDataReader[DbTableColumn.ResourceBusinessUnit]);
                    recruitment.PhoneNo = Convert.ToString(objDataReader[DbTableColumn.candidatePhoneNo]);
                    recruitment.Address = Convert.ToString(objDataReader[DbTableColumn.candidateAddress]);
                    recruitment.LandlineNo = Convert.ToString(objDataReader[DbTableColumn.LandlineNo]);
                    recruitment.CandidateEmailID = Convert.ToString(objDataReader[DbTableColumn.CandidateEmailID]);
                    // Rajan Kumar : Issue 45752 : 03/01/2014 : Starts                        			 
                    // Desc : When MRF is raised against department then MRF end date is not mandatory (remove validation).
                    if (objDataReader[DbTableColumn.ContractDuration] != DBNull.Value)
                    {
                        recruitment.ContractDuration = Convert.ToInt32(objDataReader[DbTableColumn.ContractDuration]);
                    }
                    else
                    {
                        recruitment.ContractDuration = 0;
                    }
                    // Rajan Kumar : Issue 45752 : 03/01/2014: Ends                      
                    recruitment.CandidateOfferAcceptedDate = Convert.ToDateTime(objDataReader[DbTableColumn.CandidateOfferAcceptedDate]);
                    recruitment.MRFPurpose = Convert.ToString(objDataReader[DbTableColumn.MRFPurpose]);
                    recruitment.RelavantExperienceMonth = objDataReader[DbTableColumn.ExperienceInMonth].ToString() == string.Empty  ? 0 : Convert.ToInt32(objDataReader[DbTableColumn.ExperienceInMonth]);
                    recruitment.RelevantExperienceYear = objDataReader[DbTableColumn.ExperienceInYear].ToString() == string.Empty  ? 0 : Convert.ToInt32(objDataReader[DbTableColumn.ExperienceInYear]);
                    //Rajan Kumar : Issue 39508: 31/01/2014 : Starts                        			 
                    //Desc : Traninig for new joining employee. (Training Gaps).
                    recruitment.IsTrainingRequired = Convert.ToBoolean(objDataReader[DbTableColumn.ISTrainingRequired]);
                    if (objDataReader[DbTableColumn.ISTrainingRequired] != DBNull.Value)
                    {
                        recruitment.TrainingSubject = Convert.ToString(objDataReader[DbTableColumn.TrainingSubject]);
                    }      
                    // Rajan Kumar : Issue 39508: 31/01/2014 : END                                 
                }                
                return recruitment;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RECRUITMENT, "GetRecruitmentDetail", EventIDConstants.RAVE_HR_RECRUITMENT_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDAGetRecruitmentDetail.CloseConncetion();

            }
        }

        /// <summary>
        /// Gets the Reporting Person by using id.
        /// </summary>
        public static string GetMRfResponsiblePersonName(string empid)
        {
            DataAccessClass objDAGetMRfResponsiblePersonName = new DataAccessClass();
            string sname = string.Empty;
            try
            {
                //Open the connection to DB
                objDAGetMRfResponsiblePersonName.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter(SPParameter.Resposibility, empid);

                //Execute the SP
                objDataReader = objDAGetMRfResponsiblePersonName.ExecuteReaderSP(SPNames.MRF_GetEmployeeName, param);

                //Read the data and assign to Collection object
                while (objDataReader.Read())
                {
                    sname = sname + objDataReader[DbTableColumn.EmployeeName].ToString() + ", ";
                }
                
                // 35512-Ambar-Start-08062012
                sname = sname.Trim();

                if ( sname.EndsWith(",") )
                {
                  sname = sname.Substring(0,sname.Length -1);
                }
                // 35512-Ambar-Start-08062012

                // Return the Collection
                return sname;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RECRUITMENT, "GetMRfResponsiblePersonName", EventIDConstants.RAVE_HR_RECRUITMENT_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAGetMRfResponsiblePersonName.CloseConncetion();
            }
            return sname;
        }

        /// <summary>
        /// Gets the Project Name.
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetProjectName()
        {
            // Initialise Data Access Class object
            DataAccessClass objDAGetProjectName = new DataAccessClass();

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();
            try
            {
                //Open the connection to DB
                objDAGetProjectName.OpenConnection(DBConstants.GetDBConnectionString());

                //Execute the SP
                objDataReader = objDAGetProjectName.ExecuteReaderSP(SPNames.MRF_GetProjectName);

                //Read the data and assign to Collection object
                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    KeyValue<string> keyValue = new KeyValue<string>();

                    keyValue.KeyName = objDataReader.GetValue(0).ToString();
                    keyValue.Val = objDataReader.GetValue(1).ToString();

                    // Add the object to Collection
                    raveHRCollection.Add(keyValue);
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
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RECRUITMENT, GETPROJECTNAME, EventIDConstants.RAVE_HR_RECRUITMENT_DATA_ACCESS_LAYER);
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
        /// Retrive recruitment summary at page load. 
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetRecruitmentSummaryForPageLoad(BusinessEntities.ParameterCriteria objParameter, ref int pageCount)
        {
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            // Initialise the Data Access class object
            DataAccessClass objDAGetRecruitmentSummaryForPageLoad = new DataAccessClass();

            // Initialise the SQL parameter.
            SqlParameter[] sqlParam = new SqlParameter[4];
            try
            {
                // Open the DB connection
                objDAGetRecruitmentSummaryForPageLoad.OpenConnection(DBConstants.GetDBConnectionString());

                // Parameter Page Number
                sqlParam[0] = new SqlParameter(SPParameter.pageNum, SqlDbType.Int);
                if (objParameter.PageNumber == CommonConstants.ZERO)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objParameter.PageNumber;

                // Parameter Page Size
                sqlParam[1] = new SqlParameter(SPParameter.pageSize, SqlDbType.Int);
                if (objParameter.PageSize == CommonConstants.ZERO)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objParameter.PageSize;

                // Parameter Sort Expression And Direction
                sqlParam[2] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                if (objParameter.SortExpressionAndDirection == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objParameter.SortExpressionAndDirection;

                // Output parameter Page Count
                sqlParam[3] = new SqlParameter(SPParameter.pageCount, SqlDbType.Int);
                sqlParam[3].Direction = ParameterDirection.Output;

                // Execute the SP and pass parameter to SP
                DataSet ds = objDAGetRecruitmentSummaryForPageLoad.GetDataSet(SPNames.Recruitment_GetRecruitmentSummary_PageLoad, sqlParam);

                // Assign PageCount the value returned from SP
                pageCount = Convert.ToInt32(sqlParam[3].Value);

                // Read the Dataset and assign it to Collection
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    // Initialise the Business Entity 
                    objRecruitment = new BusinessEntities.Recruitment();

                    //Assign MRf Id
                    objRecruitment.CandidateId = int.Parse(dr[DbTableColumn.CId].ToString());
                    // Assign MRF Code
                    objRecruitment.MRFCode = dr[DbTableColumn.MRFCode].ToString();
                    // Assign Project Name
                    objRecruitment.ProjectName = dr[DbTableColumn.ProjectName].ToString();
                    objRecruitment.ClientName = dr[DbTableColumn.ClientName].ToString();
                    // Assign Role
                    objRecruitment.Role = dr[DbTableColumn.Role].ToString();
                    objRecruitment.ResourceName = dr[DbTableColumn.ResourceName].ToString();
                    objRecruitment.ExpectedJoiningDate = DateTime.Parse(dr[DbTableColumn.ExpectedDateOfJoining].ToString());
                    objRecruitment.RecruitmentManager = dr[DbTableColumn.RecruitmentManager].ToString();
                    objRecruitment.Department = dr[DbTableColumn.Department].ToString();

                    // Add the object to Collection
                    raveHRCollection.Add(objRecruitment);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RECRUITMENT, GETRECRUITMENTSUMMARYFORPAGELOAD, EventIDConstants.RAVE_HR_RECRUITMENT_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAGetRecruitmentSummaryForPageLoad.CloseConncetion();

            }
            // return Collection
            return raveHRCollection;
        }

        /// <summary>
        /// Retrive recruitment summary at filter condition. 
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetRecruitmentSummaryForFilterAndPaging(BusinessEntities.ParameterCriteria objParameter, BusinessEntities.Recruitment recruitment, ref int pageCount)
        {
            // Initialise the Collection class Object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            // Initialise the Data Access class object
            DataAccessClass objDAGetRecruitmentSummaryForFilterAndPaging = new DataAccessClass();

            // Initialise the SQL parameter.
            SqlParameter[] sqlParam = new SqlParameter[7];

            try
            {
                // Open the DB connection
                objDAGetRecruitmentSummaryForFilterAndPaging.OpenConnection(DBConstants.GetDBConnectionString());

                // Parameter Department Id
                sqlParam[0] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.Int);
                if (recruitment.DepartmentId == CommonConstants.ZERO)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = recruitment.DepartmentId;

                // Parameter Project Id
                sqlParam[1] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int);
                if (recruitment.ProjectId == CommonConstants.ZERO)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = recruitment.ProjectId;

                // Parameter Role Id 
                sqlParam[2] = new SqlParameter(SPParameter.RoleId, SqlDbType.Int);
                if (recruitment.RoleId == CommonConstants.ZERO)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = recruitment.RoleId;

                // Parameter Page Number
                sqlParam[3] = new SqlParameter(SPParameter.pageNum, SqlDbType.Int);
                if (objParameter.PageNumber == CommonConstants.ZERO)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = objParameter.PageNumber;

                // Parameter Page Size
                sqlParam[4] = new SqlParameter(SPParameter.pageSize, SqlDbType.Int);
                if (objParameter.PageSize == CommonConstants.ZERO)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = objParameter.PageSize;

                // Parameter Sort Expression And Direction
                sqlParam[5] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                if (objParameter.SortExpressionAndDirection == null)
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = objParameter.SortExpressionAndDirection;

                // Output parameter Page Count
                sqlParam[6] = new SqlParameter(SPParameter.pageCount, SqlDbType.Int);
                sqlParam[6].Direction = ParameterDirection.Output;

                // Execute the SP and pass parameter to SP
                DataSet ds = objDAGetRecruitmentSummaryForFilterAndPaging.GetDataSet(SPNames.Recruitment_GetRecruitmentSummary_FilterAndPaging, sqlParam);

                // Assign PageCount the value returned from SP
                pageCount = Convert.ToInt32(sqlParam[6].Value);

                // Read the Dataset and assign it to Collection
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    // Initialise the Business Entity 
                    objRecruitment = new BusinessEntities.Recruitment();

                    //Assign MRf Id
                    objRecruitment.CandidateId = int.Parse(dr[DbTableColumn.CId].ToString());
                    // Assign MRF Code
                    objRecruitment.MRFCode = dr[DbTableColumn.MRFCode].ToString();
                    // Assign Project Name
                    objRecruitment.ProjectName = dr[DbTableColumn.ProjectName].ToString();
                    objRecruitment.ClientName = dr[DbTableColumn.ClientName].ToString();
                    // Assign Role
                    objRecruitment.Role = dr[DbTableColumn.Role].ToString();
                    objRecruitment.ResourceName = dr[DbTableColumn.ResourceName].ToString();
                    objRecruitment.ExpectedJoiningDate = DateTime.Parse(dr[DbTableColumn.ExpectedDateOfJoining].ToString());
                    objRecruitment.RecruitmentManager = dr[DbTableColumn.RecruitmentManager].ToString();
                    objRecruitment.Department = dr[DbTableColumn.Department].ToString();
                    // Add the object to Collection
                    raveHRCollection.Add(objRecruitment);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RECRUITMENT, GETRECRUITMENTSUMMARYFORFILTERANDPAGING, EventIDConstants.RAVE_HR_RECRUITMENT_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAGetRecruitmentSummaryForFilterAndPaging.CloseConncetion();

            }
            // return Collection
            return raveHRCollection;
        }

        /// <summary>
        /// Add recruitment pipeline details 
        /// </summary>
        public static int AddPipelineDetails(BusinessEntities.Recruitment recruitment)
        {

            DataAccessClass objDAAddPipelineDetails = new DataAccessClass();
            try
            {
                objDAAddPipelineDetails.OpenConnection(DBConstants.GetDBConnectionString());
                //Rajan Kumar : Issue 39508: 31/01/2014 : Starts                        			 
                // Desc : Traninig for new joining employee. (Training Gaps).
                //Increase array length from 24 to 26
                SqlParameter[] param = new SqlParameter[26];

                if (recruitment.MRFId != 0)
                    param[0] = new SqlParameter(SPParameter.MRFID, recruitment.MRFId);
                else
                    param[0] = new SqlParameter(SPParameter.MRFID, DBNull.Value);

                if (recruitment.DepartmentId != 0)
                    param[1] = new SqlParameter(SPParameter.DeptId, recruitment.DepartmentId);
                else
                    param[1] = new SqlParameter(SPParameter.DeptId, DBNull.Value);

                param[2] = new SqlParameter(SPParameter.ProjectId, recruitment.ProjectId);

                if (recruitment.Prefix != null)
                    param[3] = new SqlParameter(SPParameter.Prefix, recruitment.PrefixId);
                else
                    param[3] = new SqlParameter(SPParameter.Prefix, DBNull.Value);
                if (recruitment.FirstName != null)
                    param[4] = new SqlParameter(SPParameter.FirstName, recruitment.FirstName);
                else
                    param[4] = new SqlParameter(SPParameter.FirstName, DBNull.Value);
                if (recruitment.LastName != null)
                    param[5] = new SqlParameter(SPParameter.LastName, recruitment.LastName);
                else
                    param[5] = new SqlParameter(SPParameter.LastName, DBNull.Value);
                if (recruitment.MiddleName != null)
                    param[6] = new SqlParameter(SPParameter.MiddleName, recruitment.MiddleName);
                else
                    param[6] = new SqlParameter(SPParameter.MiddleName, DBNull.Value);
                if (recruitment.DesignationId != 0)
                    param[7] = new SqlParameter(SPParameter.Designation, recruitment.DesignationId);
                else
                    param[7] = new SqlParameter(SPParameter.Designation, DBNull.Value);
                if (recruitment.Band != null)
                    param[8] = new SqlParameter(SPParameter.Band, recruitment.Band);
                else
                    param[8] = new SqlParameter(SPParameter.Band, DBNull.Value);
                if (recruitment.ExpectedJoiningDate != null)
                    param[9] = new SqlParameter(SPParameter.ExpectedJoiningDate, recruitment.ExpectedJoiningDate);
                else
                    param[9] = new SqlParameter(SPParameter.ExpectedJoiningDate, DBNull.Value);
                if (recruitment.EmployeeType != null)
                    param[10] = new SqlParameter(SPParameter.EmployeeType, recruitment.EmployeeTypeId);
                else
                    param[10] = new SqlParameter(SPParameter.EmployeeType, DBNull.Value);
                if (recruitment.ActualCTC != null)
                    param[11] = new SqlParameter(SPParameter.ActualCTC, recruitment.ActualCTC);
                else
                    param[11] = new SqlParameter(SPParameter.ActualCTC, DBNull.Value);
                if (recruitment.ReportingTo != null)
                    param[12] = new SqlParameter(SPParameter.ReportingToId, recruitment.ReportingTo);
                else
                    param[12] = new SqlParameter(SPParameter.ReportingToId, DBNull.Value);
                if (recruitment.EmailId != null)
                    param[13] = new SqlParameter(SPParameter.EmailId, recruitment.EmailId);
                else
                    param[13] = new SqlParameter(SPParameter.EmailId, DBNull.Value);
                if (recruitment.Location != null)
                    param[14] = new SqlParameter(SPParameter.Location, recruitment.Location);
                else
                    param[14] = new SqlParameter(SPParameter.Location, DBNull.Value);

                if (recruitment.ResourceBussinessUnit != 0)
                    param[15] = new SqlParameter(SPParameter.ResourceBussinessUnit, recruitment.ResourceBussinessUnit);
                else
                    param[15] = new SqlParameter(SPParameter.ResourceBussinessUnit, DBNull.Value);

                if (recruitment.PhoneNo != null)
                    param[16] = new SqlParameter(SPParameter.phoneNo, recruitment.PhoneNo);
                else
                    param[16] = new SqlParameter(SPParameter.phoneNo, DBNull.Value);
                if (recruitment.Address != string.Empty)
                    param[17] = new SqlParameter(SPParameter.Address, recruitment.Address);
                else
                    param[17] = new SqlParameter(SPParameter.Address, DBNull.Value);
                param[18] = new SqlParameter(SPParameter.IsInserted, DbType.Int32);
                param[18].Direction = ParameterDirection.Output;

                if (!string.IsNullOrEmpty(recruitment.LandlineNo))
                    param[19] = new SqlParameter(SPParameter.LandlineNo, recruitment.LandlineNo);
                else
                    param[19] = new SqlParameter(SPParameter.LandlineNo, DBNull.Value);

                if (recruitment.CandidateEmailID != null)
                    param[20] = new SqlParameter(SPParameter.CandidateEmailID, recruitment.CandidateEmailID);
                else
                    param[20] = new SqlParameter(SPParameter.CandidateEmailID, DBNull.Value);

                if (recruitment.CandidateOfferAcceptedDate != null)
                    param[21] = new SqlParameter(SPParameter.CandidateOfferAcceptedDate, recruitment.CandidateOfferAcceptedDate);
                else
                    param[21] = new SqlParameter(SPParameter.CandidateOfferAcceptedDate, DBNull.Value);

                param[22] = new SqlParameter(SPParameter.ExperienceInMonth, recruitment.RelavantExperienceMonth);

                param[23] = new SqlParameter(SPParameter.ExperienceInYear, recruitment.RelevantExperienceYear);
                //Rajan Kumar : Issue 39508: 31/01/2014 : Starts                        			 
                //Desc : Traninig for new joining employee. (Training Gaps).
                param[24] = new SqlParameter(SPParameter.IsTrainingRequired, recruitment.IsTrainingRequired);
                param[25] = new SqlParameter(SPParameter.TrainingSubject, recruitment.TrainingSubject);
                // Rajan Kumar : Issue 39508: 31/01/2014 : END
                objDAAddPipelineDetails.ExecuteNonQuerySP(SPNames.Recruitment_AddPipelineDetails, param);
                return Convert.ToInt32(param[18].Value);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RECRUITMENT, ADD_PIPELINE_DETAILS, EventIDConstants.RAVE_HR_RECRUITMENT_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAAddPipelineDetails.CloseConncetion();

            }
        }

        /// <summary>
        /// Remove recruitment pipeline details 
        /// </summary>
        public static int RemovePipelineDetails(BusinessEntities.Recruitment recruitment)
        {
            DataAccessClass objDARemovePipelineDetails = new DataAccessClass();
            try
            {
                objDARemovePipelineDetails.OpenConnection(DBConstants.GetDBConnectionString());
                // Rajan Kumar : Issue 46252: 10/02/2014 : Starts                        			 
                // Desc : In MRF history need to implemented in all cases in RMS.
                //Pass Email to know who is going to modified the data
                //SqlParameter[] param = new SqlParameter[3];
                SqlParameter[] param = new SqlParameter[4];
                // Rajan Kumar : Issue 46252: 10/02/2014 : END                
                param[0] = new SqlParameter(SPParameter.CandidateId, recruitment.CandidateId);
                param[1] = new SqlParameter(SPParameter.Reason, recruitment.Reason);
                param[2] = new SqlParameter(SPParameter.IsRemoved, 0);
                // Rajan Kumar : Issue 46252: 10/02/2014 : Starts                        			 
                // Desc : In MRF history need to implemented in all cases in RMS.
                //Pass Email to know who is going to modified the data
                param[2].Direction = ParameterDirection.Output;
                param[3] = new SqlParameter(SPParameter.EmailId, recruitment.EmailId);
                // Rajan Kumar : Issue 46252: 10/02/2014 : END
                objDARemovePipelineDetails.ExecuteNonQuerySP(SPNames.Recruitment_RemoveRecruitmentRecord, param);
                return Convert.ToInt32(param[2].Value);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RECRUITMENT, REMOVE_PIPELINE_DETAILS, EventIDConstants.RAVE_HR_RECRUITMENT_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDARemovePipelineDetails.CloseConncetion();

            }
        }

        /// <summary>
        /// Edit recruitment pipeline details 
        /// </summary>
        public static int EditPipelineDetails(BusinessEntities.Recruitment recruitment,string previousMrfCode)
        {
            DataAccessClass objDAEditPipelineDetails = new DataAccessClass();
            try
            {
                objDAEditPipelineDetails.OpenConnection(DBConstants.GetDBConnectionString());

                // 29516-Ambar-Start-05092011-Changed Param array size from 26 to 27
                //Rajan Kumar : Issue 39508: 31/01/2014 : Starts                        			 
                // Desc : Traninig for new joining employee. (Training Gaps).
                //Increase array lenght from 27 to 29
                SqlParameter[] param = new SqlParameter[29];
                // Rajan Kumar : Issue 39508: 31/01/2014 : END

               
                // 29516-Ambar-End-05092011

                if (recruitment.CandidateId != 0)
                    param[0] = new SqlParameter(SPParameter.CandidateId, recruitment.CandidateId);
                else
                    param[0] = new SqlParameter(SPParameter.CandidateId, DBNull.Value);

                if (recruitment.Prefix != null)
                    param[1] = new SqlParameter(SPParameter.Prefix, recruitment.PrefixId);
                else
                    param[1] = new SqlParameter(SPParameter.Prefix, DBNull.Value);

                if (recruitment.FirstName != null)
                    param[2] = new SqlParameter(SPParameter.FirstName, recruitment.FirstName);
                else
                    param[2] = new SqlParameter(SPParameter.FirstName, DBNull.Value);

                if (recruitment.LastName != null)
                    param[3] = new SqlParameter(SPParameter.LastName, recruitment.LastName);
                else
                    param[3] = new SqlParameter(SPParameter.LastName, DBNull.Value);

                if (recruitment.MiddleName != null)
                    param[4] = new SqlParameter(SPParameter.MiddleName, recruitment.MiddleName);
                else
                    param[4] = new SqlParameter(SPParameter.MiddleName, DBNull.Value);

                if (recruitment.DesignationId != null)
                    param[5] = new SqlParameter(SPParameter.Designation, recruitment.DesignationId);
                else
                    param[5] = new SqlParameter(SPParameter.Designation, DBNull.Value);

                if (recruitment.Band != null)
                    param[6] = new SqlParameter(SPParameter.Band, recruitment.Band);
                else
                    param[6] = new SqlParameter(SPParameter.Band, DBNull.Value);

                if (recruitment.ExpectedJoiningDate != null)
                    param[7] = new SqlParameter(SPParameter.ExpectedJoiningDate, recruitment.ExpectedJoiningDate);
                else
                    param[7] = new SqlParameter(SPParameter.ExpectedJoiningDate, DBNull.Value);

                if (recruitment.EmployeeType != null)
                    param[8] = new SqlParameter(SPParameter.EmployeeType, recruitment.EmployeeTypeId);
                else
                    param[8] = new SqlParameter(SPParameter.EmployeeType, DBNull.Value);
                if (recruitment.ActualCTC != 0)
                    param[9] = new SqlParameter(SPParameter.ActualCTC, recruitment.ActualCTC);
                else
                    param[9] = new SqlParameter(SPParameter.ActualCTC, DBNull.Value);
                if (recruitment.ReportingTo != null)
                    param[10] = new SqlParameter(SPParameter.ReportingToId, recruitment.ReportingTo);
                else
                    param[10] = new SqlParameter(SPParameter.ReportingToId, DBNull.Value);
                if (recruitment.EmailId != null)
                    param[11] = new SqlParameter(SPParameter.EmailId, recruitment.EmailId);
                else
                    param[11] = new SqlParameter(SPParameter.EmailId, DBNull.Value);

                if (recruitment.ResourceJoinedDate == System.DateTime.MinValue)
                    param[12] = new SqlParameter(SPParameter.ResourceJoinedDate, DBNull.Value);
                else
                    param[12] = new SqlParameter(SPParameter.ResourceJoinedDate, recruitment.ResourceJoinedDate);

                if (recruitment.IsResourceJoined != null)
                    param[13] = new SqlParameter(SPParameter.IsResourceJoined, recruitment.IsResourceJoined);
                else
                    param[13] = new SqlParameter(SPParameter.IsResourceJoined, DBNull.Value);
                param[14] = new SqlParameter(SPParameter.IsUpdated, 0);
                param[14].Direction = ParameterDirection.Output;
                if (recruitment.Location != null)
                    param[15] = new SqlParameter(SPParameter.Location, recruitment.Location);
                else
                    param[15] = new SqlParameter(SPParameter.Location, DBNull.Value);
               
                if (recruitment.ResourceBussinessUnit != 0)
                    param[16] = new SqlParameter(SPParameter.ResourceBussinessUnit, recruitment.ResourceBussinessUnit);
                else
                    param[16] = new SqlParameter(SPParameter.ResourceBussinessUnit, DBNull.Value);
                if (recruitment.PhoneNo != null)
                    param[17] = new SqlParameter(SPParameter.phoneNo, recruitment.PhoneNo);
                else
                    param[17] = new SqlParameter(SPParameter.phoneNo, DBNull.Value);
                if (recruitment.Address != null)
                    param[18] = new SqlParameter(SPParameter.Address, recruitment.Address);
                else
                    param[18] = new SqlParameter(SPParameter.Address, DBNull.Value);

                if (!string.IsNullOrEmpty(recruitment.LandlineNo))
                    param[19] = new SqlParameter(SPParameter.LandlineNo, recruitment.LandlineNo);
                else
                    param[19] = new SqlParameter(SPParameter.LandlineNo, DBNull.Value);

                if (recruitment.CandidateEmailID != null)
                    param[20] = new SqlParameter(SPParameter.CandidateEmailID, recruitment.CandidateEmailID);
                else
                    param[20] = new SqlParameter(SPParameter.CandidateEmailID, DBNull.Value);

                if (recruitment.CandidateOfferAcceptedDate != null)
                    param[21] = new SqlParameter(SPParameter.CandidateOfferAcceptedDate, recruitment.CandidateOfferAcceptedDate);
                else
                    param[21] = new SqlParameter(SPParameter.CandidateOfferAcceptedDate, DBNull.Value);

                //Adding parameter for MRf code.
                if (recruitment.MRFCode != null)
                    param[22] = new SqlParameter(SPParameter.MRFCode, recruitment.MRFCode);
                else
                    param[22] = new SqlParameter(SPParameter.MRFCode, DBNull.Value);

                //Adding parameter for previous MRF
                if (previousMrfCode != null)
                    param[23] = new SqlParameter(SPParameter.PreviousMrfCode, previousMrfCode);
                else
                    param[23] = new SqlParameter(SPParameter.PreviousMrfCode, DBNull.Value);

                param[24] = new SqlParameter(SPParameter.ExperienceInMonth, recruitment.RelavantExperienceMonth);

                param[25] = new SqlParameter(SPParameter.ExperienceInYear, recruitment.RelevantExperienceYear);

                // 29516-Ambar-Start-05092011
                if (recruitment.ProjectId != null)
                  param[26] = new SqlParameter(SPParameter.ProjectId, recruitment.ProjectId);
                else
                  param[26] = new SqlParameter(SPParameter.ProjectId, DBNull.Value);
                // 29516-Ambar-End-05092011

                //Rajan Kumar : Issue 39508: 03/02/2014 : Starts                        			 
                // Desc : Traninig for new joining employee. (Training Gaps).
                param[27] = new SqlParameter(SPParameter.IsTrainingRequired, recruitment.IsTrainingRequired);

                param[28] = new SqlParameter(SPParameter.TrainingSubject, recruitment.TrainingSubject);
                // Rajan Kumar : Issue 39508: 03/02/2014 : END

                objDAEditPipelineDetails.ExecuteNonQuerySP(SPNames.Recruitment_EditPipelineDetails, param);
                return Convert.ToInt32(param[14].Value);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RECRUITMENT, EDIT_PIPELINE_DETAILS, EventIDConstants.RAVE_HR_RECRUITMENT_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAEditPipelineDetails.CloseConncetion();

            }
        }

        /// <summary>
        /// Gets the MRF Detaill for particular Employee for Add Employee page.
        /// sudip
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.Recruitment GetMrfDetailForEmployee(int mrfId)
        {
            DataAccessClass objDAGetMrfDetailForEmployee = new DataAccessClass();
            BusinessEntities.Recruitment recruitment = null;
            try
            {
                objDAGetMrfDetailForEmployee.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter(SPParameter.MRFId, mrfId);
                objDataReader = objDAGetMrfDetailForEmployee.ExecuteReaderSP(SPNames.Recruitment_GetMRFDetailsForEmployee, param);
                while (objDataReader.Read())
                {
                    recruitment = new BusinessEntities.Recruitment();
                    recruitment.PrefixId = Convert.ToInt32(objDataReader[DbTableColumn.Prefix]);
                    recruitment.FirstName = objDataReader[DbTableColumn.FirstName].ToString();
                    recruitment.MiddleName = objDataReader[DbTableColumn.MiddleName].ToString();
                    recruitment.LastName = objDataReader[DbTableColumn.LastName].ToString();
                    recruitment.DepartmentId = Convert.ToInt32(objDataReader[DbTableColumn.DepartmentId]);
                    recruitment.EmployeeTypeId = Convert.ToInt32(objDataReader[DbTableColumn.Type]);
                    recruitment.BandId = Convert.ToInt32(objDataReader[DbTableColumn.Band]);
                    recruitment.DesignationId = Convert.ToInt32(objDataReader[DbTableColumn.DesignationId]);
                    recruitment.ReportingId = objDataReader[DbTableColumn.ReportingToId].ToString();
                    //recruitment.ResourceJoinedDate = Convert.ToDateTime(objDataReader[DbTableColumn.JoiningDate]);
                    recruitment.ResourceJoinedDate = objDataReader[DbTableColumn.JoiningDate].ToString() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(objDataReader[DbTableColumn.JoiningDate].ToString());
                    
                    recruitment.ResourceBussinessUnit = objDataReader[DbTableColumn.ResourceBusinessUnit].ToString() == string.Empty ? 0 : Convert.ToInt32(objDataReader[DbTableColumn.ResourceBusinessUnit]);
                    recruitment.IsEditedEmailId = Convert.ToBoolean(objDataReader[DbTableColumn.IsEditedEmailId]);
                    recruitment.Location = objDataReader[DbTableColumn.Location].ToString();
                    recruitment.CandidateEmailID = objDataReader[DbTableColumn.CandidateEmailID].ToString();
                    recruitment.RelavantExperienceMonth = objDataReader[DbTableColumn.ExperienceInMonth].ToString() == string.Empty ? 0 : Convert.ToInt32(objDataReader[DbTableColumn.ExperienceInMonth]);
                    recruitment.RelevantExperienceYear = objDataReader[DbTableColumn.ExperienceInYear].ToString() == string.Empty ? 0 : Convert.ToInt32(objDataReader[DbTableColumn.ExperienceInYear]);
                }
                if (!objDataReader.IsClosed)
                {
                    objDataReader.Close();
                }
                if (recruitment != null && recruitment.ReportingId != null && recruitment.ReportingId != string.Empty)
                    recruitment.ReportingTo = this.GetEmployeeReportingToName(recruitment.ReportingId);

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RECRUITMENT, "GetMrfDetail", EventIDConstants.RAVE_HR_RECRUITMENT_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAGetMrfDetailForEmployee.CloseConncetion();

                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
            }

            return recruitment;
        }

        /// <summary>
        /// Gets the Employee's Reporting to Name
        /// sudip
        /// </summary>
        /// <returns>Collection</returns>
        public string GetEmployeeReportingToName(string empid)
        {
            DataAccessClass objDAGetEmployeeReportingToName = new DataAccessClass();
            string sname = string.Empty;
            try
            {
                //Open the connection to DB
                objDAGetEmployeeReportingToName.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter(SPParameter.Resposibility, empid);

                //Execute the SP
                objDataReader = objDAGetEmployeeReportingToName.ExecuteReaderSP(SPNames.MRF_GetEmployeeName, param);

                //Read the data and assign to Collection object
                while (objDataReader.Read())
                {
                    sname = sname + objDataReader[DbTableColumn.EmployeeName].ToString() + ", ";
                }

                // Return the Collection
                return sname;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RECRUITMENT, "GetEmployeeReportingToName", EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAGetEmployeeReportingToName.CloseConncetion();

            }




        }

        // Mohamed : Issue 50306 : 09/09/2014 : Starts                        			  
        // Desc : Expected Joinee's details edited[MRF Code: MRF_Testing_SrTstA_0387] old  Joining date is default date -- Mail for de-linking MRF
        /// <summary>
        /// Gets the Employee's Reporting to Name
        /// sudip
        /// </summary>
        /// <returns>Collection</returns>
        public string GetRecruiterEmailByMRFId(int OldMRFId, int NewMRFId)
        {
            DataAccessClass objDAGetEmployeeReportingToName = new DataAccessClass();
            string sname = string.Empty;
            try
            {
                //Open the connection to DB
                objDAGetEmployeeReportingToName.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter(SPParameter.OldMRFId, OldMRFId);
                param[1] = new SqlParameter(SPParameter.NewMRFId, NewMRFId);

                //Execute the SP
                objDataReader = objDAGetEmployeeReportingToName.ExecuteReaderSP(SPNames.Recruitment_GetRecruiteEMailIdbyMRFId, param);

                //Read the data and assign to Collection object
                while (objDataReader.Read())
                {
                    sname = sname + objDataReader[DbTableColumn.EmailId].ToString() + ", ";
                }

                // Return the Collection
                return sname;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RECRUITMENT, "GetRecruiterEmailByMRFId", EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAGetEmployeeReportingToName.CloseConncetion();
            }
        }
        // Mohamed : Issue 50306 : 09/09/2014 : Ends

        /// <summary>
        /// GetProjectManagerByProjectId
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>

        public static RaveHRCollection GetProjectManagerByProjectId(BusinessEntities.Recruitment objRecruitment)
        {            
            
            try
            {
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.ProjectId, objRecruitment.ProjectId);

                //--Declare variable.
                BusinessEntities.Recruitment objBEGetProjectManagerByProjectId = null;
                RaveHRCollection objListGetProjectManagerByProjectId = new RaveHRCollection();

                //--Get data for recruitment 
                DataSet dsGetProjectManagerByProjectId = objDA.GetDataSet(SPNames.ResourcePlan_GetProjectManagerByProjectId, sqlParam);

                //create temporary datatable.
                DataTable tempDataTable = ((DataTable)(dsGetProjectManagerByProjectId.Tables[0])).Clone();

                //create new dataset.
                DataSet dsGetPMByProjectId = new DataSet();

                //declare datrow object.
                DataRow dataRow = null;

                foreach (DataRow row in dsGetProjectManagerByProjectId.Tables[0].Rows)
                {
                    //checks if Project Id already in dataset.
                    if (tempDataTable.Rows.Count == 0 || (!row[DbTableColumn.ProjectId].ToString().Equals(dataRow[DbTableColumn.ProjectId].ToString())))
                    {
                        dataRow = tempDataTable.NewRow();
                        dataRow[DbTableColumn.ProjectId] = row[DbTableColumn.ProjectId];
                        dataRow[DbTableColumn.ProjectManager] = row[DbTableColumn.ProjectManager];
                        dataRow[DbTableColumn.EmailId] = row[DbTableColumn.EmailId];
                        tempDataTable.Rows.Add(dataRow);
                    }
                    else
                    {
                        //comma separated values of different project managers for same project.
                        if (!row[DbTableColumn.ProjectManager].ToString().Equals(dataRow[DbTableColumn.ProjectManager].ToString()))
                        {
                            dataRow[DbTableColumn.ProjectManager] = dataRow[DbTableColumn.ProjectManager] + "," + row[DbTableColumn.ProjectManager];
                        }
                        if (!row[DbTableColumn.EmailId].ToString().Equals(dataRow[DbTableColumn.EmailId].ToString()))
                        {
                            dataRow[DbTableColumn.EmailId] = dataRow[DbTableColumn.EmailId] + "," + row[DbTableColumn.EmailId];
                        }
                    }

                }

                //// Fills the datatset with Project details. 
                dsGetPMByProjectId.Tables.Add(tempDataTable);

                //loops through dataset
                foreach (DataRow dr in dsGetPMByProjectId.Tables[0].Rows)
                {                    
                    objBEGetProjectManagerByProjectId = new BusinessEntities.Recruitment();

                    //assign ProjectId 
                    objBEGetProjectManagerByProjectId.ProjectId = int.Parse(dr[DbTableColumn.ProjectId].ToString());

                    //assign CreatedByFullName 
                    objBEGetProjectManagerByProjectId.ProjectManagerName = dr[DbTableColumn.ProjectManager].ToString();
                    objBEGetProjectManagerByProjectId.EmailId = dr[DbTableColumn.EmailId].ToString();

                    //add to collection object
                    objListGetProjectManagerByProjectId.Add(objBEGetProjectManagerByProjectId);
                }

                //--return the Collection

                return objListGetProjectManagerByProjectId;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RECRUITMENT, GET_PROJECT_MANAGER_BY_PROJECTID, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();

            }

        }


        /// <summary>
        /// Gets DepartmentHead by DepartmentId
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>

        public static RaveHRCollection GetDepartmentHeadByDepartmentId(BusinessEntities.Recruitment objRecruitment)
        {

            try
            {
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.@DepartmentId, objRecruitment.DepartmentId);

                //--Declare variable.
                BusinessEntities.Recruitment objBEGetDepartmentHeadByDepartmentId = null;
                RaveHRCollection objListGetDepartmentHeadByDepartmentId = new RaveHRCollection();

                //--Get data for recruitment 
                DataSet dsGetDepartmentHeadByDepartmentId = objDA.GetDataSet(SPNames.Recruitment_GetDepartmentHeadByDepartmentId, sqlParam);

                //create temporary datatable.
                DataTable tempDataTable = ((DataTable)(dsGetDepartmentHeadByDepartmentId.Tables[0])).Clone();

                //create new dataset.
                DataSet dsGetDHByDepartmentId = new DataSet();

                //declare datrow object.
                DataRow dataRow = null;

                foreach (DataRow row in dsGetDepartmentHeadByDepartmentId.Tables[0].Rows)
                {
                    //checks if DepartmentId already in dataset.
                    if (tempDataTable.Rows.Count == 0 || (!row[DbTableColumn.DeptId].ToString().Equals(dataRow[DbTableColumn.DeptId].ToString())))
                    {
                        dataRow = tempDataTable.NewRow();
                        dataRow[DbTableColumn.DeptId] = row[DbTableColumn.DeptId];
                        dataRow[DbTableColumn.DepartmentHead] = row[DbTableColumn.DepartmentHead];
                        dataRow[DbTableColumn.EmailId] = row[DbTableColumn.EmailId];
                        tempDataTable.Rows.Add(dataRow);
                    }
                    else
                    {
                        //comma separated values of different Department  heads for same Department.
                        if (!row[DbTableColumn.DepartmentHead].ToString().Equals(dataRow[DbTableColumn.DepartmentHead].ToString()))
                        {
                            dataRow[DbTableColumn.DepartmentHead] = dataRow[DbTableColumn.DepartmentHead] + "," + row[DbTableColumn.DepartmentHead];
                        }
                        //comma separated values of different EmailID for Department  heads
                        if (!row[DbTableColumn.EmailId].ToString().Equals(dataRow[DbTableColumn.EmailId].ToString()))
                        {
                            dataRow[DbTableColumn.EmailId] = dataRow[DbTableColumn.EmailId] + "," + row[DbTableColumn.EmailId];
                        }
                    }

                }

                //// Fills the datatset with Department Head details. 
                dsGetDHByDepartmentId.Tables.Add(tempDataTable);

                //loops through dataset
                foreach (DataRow dr in dsGetDHByDepartmentId.Tables[0].Rows)
                {
                    objBEGetDepartmentHeadByDepartmentId = new BusinessEntities.Recruitment();

                    //assign DepartmentId 
                    objBEGetDepartmentHeadByDepartmentId.DepartmentId = int.Parse(dr[DbTableColumn.DeptId].ToString());

                    //assign CreatedByFullName 
                    objBEGetDepartmentHeadByDepartmentId.DepartmentHeadName = dr[DbTableColumn.DepartmentHead].ToString();
                    objBEGetDepartmentHeadByDepartmentId.EmailId = dr[DbTableColumn.EmailId].ToString();

                    //add to collection object
                    objListGetDepartmentHeadByDepartmentId.Add(objBEGetDepartmentHeadByDepartmentId);
                }

                //--return the Collection

                return objListGetDepartmentHeadByDepartmentId;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RECRUITMENT, GET_DEPARMENT_HEAD_BY_DEPARTMENTID, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();

            }

        }



        /// <summary>
        /// Gets USP_Recruitment_GetDepartmentDetails_ForBusinessUnit
        /// </summary>
        /// <param name="DeptID"></param>
        /// <returns></returns>
        public static BusinessEntities.Recruitment ResourceBussinesUnitAsperDept(int Deptid)
        {
            DataAccessClass objDAGetResourceBussinesUnitAsperDept = new DataAccessClass();
            try
            {
                BusinessEntities.Recruitment recruitment = new BusinessEntities.Recruitment();
                objDAGetResourceBussinesUnitAsperDept.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter(SPParameter.DepartmentId, Deptid);
                objDataReader = objDAGetResourceBussinesUnitAsperDept.ExecuteReaderSP(SPNames.Recruitment_GetDepartmentDetailsForBusinessUnit, param);
                while (objDataReader.Read())
                {
                    recruitment.ResourceBussinessUnitName = 
                            objDataReader[DbTableColumn.ResourceBusinessUnitName].ToString();

                }
                return recruitment;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RECRUITMENT, "GetDepartmentDetailsForBusinessUnit", EventIDConstants.RAVE_HR_RECRUITMENT_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDAGetResourceBussinesUnitAsperDept.CloseConncetion();

            }
        }


        /// <summary>
        /// Get Reporting to Email ID from employee ID 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>

        public static string GetReportingToByEmployeeId(int EmpId)
        {

            try
            {
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];
                
                // 36834-Ambar-Start-08 Aug 2012-Change made to pass correct Parameter value
                // sqlParam[0] = new SqlParameter(SPParameter.CandidateId, objRecruitment.CandidateId);
                sqlParam[0] = new SqlParameter(SPParameter.CandidateId, EmpId);
                // 36834-Ambar-End


                SqlDataReader reader = objDA.ExecuteReaderSP(SPNames.Recruitment_GetReportingToByEmployeeID, sqlParam);
                BusinessEntities.Employee ReportingTo = new BusinessEntities.Employee();
                //--Get data for recruitment 
               
                    while(reader.Read())
                    {
                        ReportingTo.FirstName = reader[CommonConstants.EMPLOYEE_NAME].ToString();
                        ReportingTo.EMPId =Convert.ToInt32(reader[CommonConstants.EMP_ID]);
                        ReportingTo.EmailId = reader[DbTableColumn.EmailId].ToString();
                    }
                return ReportingTo.EmailId;
             }              
            
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RECRUITMENT, GET_REPORTINGTO_BY_EMPLOYEEID, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();

            }

        }

        #endregion

      
      #region 35913
      // 35913-Ambar-Start
        public static bool GetCandidateJoinStatus(int CandidateId)
        {
          try
          {
            objDA = new DataAccessClass();
            objDA.OpenConnection(DBConstants.GetDBConnectionString());

            SqlParameter[] sqlParam = new SqlParameter[2];
            sqlParam[0] = new SqlParameter(SPParameter.CandidateId, CandidateId);

            sqlParam[1] = new SqlParameter(SPParameter.IsResourceJoined, "0");
            sqlParam[1].Direction = ParameterDirection.Output;

            objDA.ExecuteNonQuerySP(SPNames.Recruitment_GetCandidateJoinStatus, sqlParam);

            if (Convert.ToInt32(sqlParam[1].Value) == 1)
            {
              return true;
            }
            else
            {
              return false;
            }                       
          }

          catch (RaveHRException ex)
          {
            throw ex;
          }

          catch (Exception ex)
          {
            throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RECRUITMENT, GET_REPORTINGTO_BY_EMPLOYEEID, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
          }
          finally
          {
            objDA.CloseConncetion();
          }
        }
      // 35913-Ambar-End
      #endregion 35913
    }
}
