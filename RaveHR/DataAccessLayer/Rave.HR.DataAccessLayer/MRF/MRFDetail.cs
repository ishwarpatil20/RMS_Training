//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           MRFDetail.aspx      
//  Author:         Chhaya Gunjal 
//  Date written:   03/09/2009/ 10:58:30 AM
//  Description:    This class  provides the Data Access layer methods for MRF module.
//
//  Amendments
//  Date                    Who              Ref     Description
//  ----                    -----------      ---     -----------
//  03/09/2009 10:58:30 AM  Chhaya Gunjal    n/a     Created    
//
//------------------------------------------------------------------------------
using System;
//using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Common;
using Common.Constants;
using BusinessEntities;
using System.Configuration;
using System.Text;
using Common.AuthorizationManager;
using System.Transactions;

namespace Rave.HR.DataAccessLayer.MRF
{
    public class MRFDetail
    {
        #region Private Field Members

        // private static DataAccessClass objDA;

        private static SqlDataReader objDataReader;

        private static SqlParameter[] objSqlParameter;

        private static BusinessEntities.MRFDetail objMrfDetail;

        private static BusinessEntities.RaveHRCollection raveHRCollection;

        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo(((System.Web.Configuration.GlobalizationSection)(ConfigurationSettings.GetConfig("system.web/globalization"))).Culture);

        #endregion

        #region constants

        /// <summary>
        /// Class Name : MRFDetail
        /// </summary>
        private const string CLASS_NAME_MRF = "MRFDetail";
        private const string SUPER_ROLE = "roleSuper";
        private const string GET_EMPLOYEE_EXITS = "GetEmployeeExistCheck";

        /// <summary>
        /// Function name : DeleteMRFDL
        /// </summary>
        private const string DELETEMRFDL = "DeleteMRFDL";

        /// <summary>
        /// Function name : GetMRFDetailsForApprocalOfMRFByHeadCount
        /// </summary>
        private const string GETMRFDETAILSFORAPPROVALOFMRFBYHEADCOUNT = "GetMRFDetailsForApprocalOfMRFByHeadCount";

        /// <summary>
        /// Function name : SetMRFApproveRejectReason
        /// </summary>
        private const string SETMRFAPPROVEREJECTREASON = "SetMRFApproveRejectReason";

        //Mahednra Issue Id : 33860 STRAT
        /// <summary>
        /// Function name : checkConcurrencyResourceAllocation
        /// </summary>
        private const string ConcurrencyResourceAllocation = "checkConcurrencyResourceAllocation";

        //Mahednra Issue Id : 33860 END
        

        /// <summary>
        /// Function name : GetListOfInternalResource
        /// </summary>
        private const string GETLISTOFINTERNALRESOURCE = "GetListOfInternalResource";

        /// <summary>
        /// Function name : GetMRFDetailsForPendingAllocation
        /// </summary>
        private const string GETMRFDETAILSFORPENDINGALLOCATION = "GetMRFDetailsForPendingAllocation";

        /// <summary>
        /// Function name : GetMrfSummary
        /// </summary>
        private const string GETMRFSUMMARY = "GetMrfSummary";

        /// <summary>
        /// Function name : GetMrfCode
        /// </summary>
        private const string GETMRFCODE = "GetMrfCode";

        /// <summary>
        /// Function name : GetProjectName
        /// </summary>
        private const string GETPROJECTNAME = "GetProjectName";

        /// <summary>
        /// Function Name: GetApproveRejectMrf
        /// </summary>
        private const string GETAPPROVEREJECTMRF = "GetApproveRejectMrf";

        /// <summary>
        /// Function name : SetMRFApproveRejectReason
        /// </summary>
        private const string SETMRFAPPROVEREJECTREASONFORPM = "SetMRFApproveRejectReasonForPM";

        /// <summary>
        /// Function name : GetProjectNameRoleWiseDL
        /// </summary>
        private const string GETPROJECTNAMEROLEWISEDL = "GetProjectNameRoleWiseDL";

        /// <summary>
        ///  Function name : GetResourcePlanProjectWiseDL
        /// </summary>
        private const string GETRESOURCEPLANPROJECTWISEDL = "GetResourcePlanProjectWiseDL";

        /// <summary>
        ///  Function name : GetRoleResourcePlanWiseDL
        /// </summary>
        private const string GETROLERESOURCEPLANWISEDL = "GetRoleResourcePlanWiseDL";

        /// <summary>
        ///  Function name : GetResourcePlanGridRoleWiseDL
        /// </summary>
        private const string GETRESOURCEPLANGRIDROLEWISEDL = "GetResourcePlanGridRoleWiseDL";

        /// <summary>
        /// Function name : GetProjectDomainDL
        /// </summary>
        private const string GETPROJECTDOMAINDL = "GetProjectDomainDL";

        /// <summary>
        /// Function name : GetMRFEmployeeDL
        /// </summary>
        private const string GETMRFEMPLOYEEDL = "GetMRFEmployeeDL";

        /// <summary>
        /// Function name : RaiseMRFDL
        /// </summary>
        private const string RAISEMRFDL = "RaiseMRFDL";

        /// <summary>
        /// Function name : GetCopyMRFDL
        /// </summary>
        private const string GETCOPYMRFDL = "GetCopyMRFDL";

        /// <summary>
        /// Function name : CopyMRFDL
        /// </summary>
        private const string COPYMRFDL = "CopyMRFDL";

        /// <summary>
        /// Function name : GetMRfResponsiblePersonName
        /// </summary>
        private const string GETMRFRESPONSIBLEPERSONNAME = "GetMRfResponsiblePersonName";

        /// <summary>
        /// Function name : GetRecruitmentManager
        /// </summary>
        private const string GETRECRUITMENTMANAGER = "GetRecruitmentManager";

        /// <summary>
        /// Function name : EditMRFDL
        /// </summary>
        private const string EDITMRFDL = "EditMRFDL";

        /// <summary>
        /// Function name : GetRoleDepartmentWiseDL
        /// </summary>
        private const string GETROLEDEPARTMENTWISEDL = "GetRoleDepartmentWiseDL";

        private const string GETDELETEFUTUREEMPLOYEEDL = "GetDeleteFutureEmployeeDL";

        private const string GETEDITEFUTUREEMPLOYEEDL = "GetEditFutureEmployeeDL";

        /// <summary>
        /// Function name : AbortMRFDL
        /// </summary>
        private const string ABORTMRFDL = "AbortMRFDL";

        /// <summary>
        /// Function name : SetMRFStatus
        /// </summary>
        private const string SETMRFSTATUS = "SetMRFStatus";

        /// <summary>
        /// Function name : GetMrfSummaryForPageLoad
        /// </summary>
        private const string GETMRFSUMMARYFORPAGELOAD = "GetMrfSummaryForPageLoad";

        /// <summary>
        /// Function name : SetMRFSatusAfterApproval
        /// </summary>
        private const string SETMRFSATUSAFTERAPPROVAL = "SetMRFSatusAfterApproval";

        #endregion Private Field Members

        #region Public Methods

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
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, GETPROJECTNAME, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
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

        //Jignyasa Issue id : 42400,42315
        /// <summary>
        /// Gets the client name from Project ID.
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetClientNameFromProjectID(int ProjectID)
        {
            // Initialise Data Access Class object
            DataAccessClass objDAGetProjectName = new DataAccessClass();

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                //Open the connection to DB
                objDAGetProjectName.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] objSqlParameter = new SqlParameter[1];
                objSqlParameter[0] = new SqlParameter(SPParameter.ProjectId, ProjectID);

                //Execute the SP
                objDataReader = objDAGetProjectName.ExecuteReaderSP(SPNames.MRF_GetClientNameFromProjectId, objSqlParameter);

                //Read the data and assign to Collection object
                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    KeyValue<string> keyValue = new KeyValue<string>();


                    if (objDataReader.GetValue(0) != null)
                    {
                        keyValue.KeyName = objDataReader.GetValue(0).ToString();
                    }
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
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, GETPROJECTNAME, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
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
        //Jignyasa Issue id : 42400,42315

        /// <summary>
        /// Gets the MRF Code
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetMrfCode()
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
                objDataReader = objDAGetMrfCode.ExecuteReaderSP(SPNames.MRF_GetMRFCode);


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
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, GETMRFCODE, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDAGetMrfCode.CloseConncetion();

            }
        }

        /// <summary>
        /// Retrive the MRF details for Approval of MRF by  Head Count 
        /// </summary>
        /// <returns>Collection</returns>
        public static BusinessEntities.RaveHRCollection GetMRFDetailsForApprocalOfMRFByHeadCount(string EmailId, BusinessEntities.ParameterCriteria objParameterCriteria, ref int pageCount)
        {
            // Initialise Data Access Class object
            DataAccessClass objDAGetMRFDetailsForApprocalOfMRFByHeadCount = new DataAccessClass();
            try
            {
                // Initialise Collection class object
                raveHRCollection = new BusinessEntities.RaveHRCollection();
                //Open the connection to DB
                objDAGetMRFDetailsForApprocalOfMRFByHeadCount.OpenConnection(DBConstants.GetDBConnectionString());
                //Add Parameter
                objSqlParameter = new SqlParameter[5];
                objSqlParameter[0] = new SqlParameter(SPParameter.UserMailId, EmailId);
                objSqlParameter[1] = new SqlParameter(SPParameter.SortExpression, objParameterCriteria.SortExpressionAndDirection);
                objSqlParameter[2] = new SqlParameter(SPParameter.pageNum, objParameterCriteria.PageNumber);
                objSqlParameter[3] = new SqlParameter(SPParameter.pageSize, objParameterCriteria.PageSize);
                objSqlParameter[4] = new SqlParameter(SPParameter.pageCount, SqlDbType.Int);
                objSqlParameter[4].Direction = ParameterDirection.Output;
                //Execute the SP
                DataSet ds = objDAGetMRFDetailsForApprocalOfMRFByHeadCount.GetDataSet(SPNames.MRF_GetApproveRejectHeadCountMRFDetails, objSqlParameter);
                pageCount = Convert.ToInt32(objSqlParameter[4].Value);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //Initialise the Business Entity object
                    objMrfDetail = new BusinessEntities.MRFDetail();
                    objMrfDetail.MRFId = Convert.ToInt32(dr[DbTableColumn.MRFID]);
                    objMrfDetail.MRFCode = dr[DbTableColumn.MRFCode].ToString();
                    objMrfDetail.ResourceOnBoard = Convert.ToDateTime(dr[DbTableColumn.StartDate]);
                    // Rajan Kumar : Issue 45752 : 03/01/2014 : Starts                        			 
                    // Desc : When MRF is raised against department then MRF end date is not mandatory (remove validation).
                    if (dr[DbTableColumn.EndDate] != DBNull.Value)
                    {
                        objMrfDetail.ResourceReleased = Convert.ToDateTime(dr[DbTableColumn.EndDate]);
                    }
                    // Rajan Kumar : Issue 45752 : 03/01/2014: Ends                    
                    objMrfDetail.EmployeeName = dr[DbTableColumn.MRFRaisedBy].ToString();
                    objMrfDetail.DepartmentName = dr[DbTableColumn.Department].ToString();
                    //if (role != AuthorizationManagerConstants.ROLECEO)
                    //{
                    objMrfDetail.ProjectName = dr[DbTableColumn.ProjectName].ToString();
                    objMrfDetail.ClientName = dr[DbTableColumn.ClientName].ToString();
                    //}
                    objMrfDetail.Utilization = Convert.ToInt32(dr[DbTableColumn.Utilization]);
                    objMrfDetail.Billing = Convert.ToInt32(dr[DbTableColumn.Billing]);

                    objMrfDetail.Role = dr[DbTableColumn.Role].ToString();
                    objMrfDetail.MRFCTCString = Convert.ToString(dr[DbTableColumn.MRFCTC]);
                    objMrfDetail.ExperienceString = Convert.ToString(dr[DbTableColumn.Experience]);
                    objMrfDetail.RecruitmentManager = dr[DbTableColumn.RecruitmentManager].ToString();
                    objMrfDetail.EmailId = dr[DbTableColumn.EmailId].ToString();
                    objMrfDetail.StatusId = Convert.ToInt32(dr[DbTableColumn.StatusId].ToString());
                    objMrfDetail.StatusName = dr[DbTableColumn.StatusName].ToString();
                    objMrfDetail.ExpectedClosureDate = dr[DbTableColumn.ExpectedClosureDate].ToString();
                    objMrfDetail.MRFPurpose = dr[DbTableColumn.MRFPurpose].ToString();
                    
                    raveHRCollection.Add(objMrfDetail);

                }
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer,
                    CLASS_NAME_MRF, GETMRFDETAILSFORAPPROVALOFMRFBYHEADCOUNT,
                    EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDAGetMRFDetailsForApprocalOfMRFByHeadCount.CloseConncetion();
            }
        }

        /// <summary>
        /// Set the reason for MRF when it's status is changed.
        /// </summary>
        /// <returns>Int</returns>

        public int SetMRFApproveRejectReason(BusinessEntities.MRFDetail mrfDetail)
        {
            int checkReasonSet = -1;
            DataAccessClass objDASetMRFApproveRejectReason = new DataAccessClass();
            try
            {

                //Open the connection to DB
                objDASetMRFApproveRejectReason.OpenConnection(DBConstants.GetDBConnectionString());
                // Rajan Kumar : Issue 46252: 12/02/2014 : Starts                        			 
                // Desc : In MRF history need to implemented in all cases in RMS.
                //Pass Email to know who is going to modified the data            
                //objSqlParameter = new SqlParameter[4];
                objSqlParameter = new SqlParameter[5];
                // Rajan Kumar : Issue 46252: 12/02/2014 : END

                //Add Parameter
               
                objSqlParameter[0] = new SqlParameter(SPParameter.MRFId, mrfDetail.MRFId);
                objSqlParameter[1] = new SqlParameter(SPParameter.MRFStatus, mrfDetail.Status);
                objSqlParameter[2] = new SqlParameter(SPParameter.Reason, mrfDetail.CommentReason);
                objSqlParameter[3] = new SqlParameter(SPParameter.SetMRFDetailReason, 0);
                objSqlParameter[3].Direction = ParameterDirection.Output;
                // Rajan Kumar : Issue 46252: 12/02/2014 : Starts                        			 
                // Desc : In MRF history need to implemented in all cases in RMS.
                //Pass Email to know who is going to modified the data    
                objSqlParameter[4] = new SqlParameter(SPParameter.EmailId, mrfDetail.LoggedInUserEmail);
                // Rajan Kumar : Issue 46252: 12/02/2014 : END
                objDASetMRFApproveRejectReason.ExecuteNonQuerySP(SPNames.MRF_SetReasonOfApproveRejectMRF, objSqlParameter);
                checkReasonSet = Convert.ToInt32(objSqlParameter[3].Value);

                return checkReasonSet;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, SETMRFAPPROVEREJECTREASON, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDASetMRFApproveRejectReason.CloseConncetion();

            }

        }



        // Mahednra Issue Id : 33860 START
        /// <summary>
        /// Set the reason for MRF when it's status is changed.
        /// </summary>
        /// <returns>Int</returns>

        public DataSet checkConcurrencyResourceAllocation(BusinessEntities.MRFDetail mrfDetail)
        {
           // int checkReasonSet = -1;
            DataAccessClass objDASetMRFApproveRejectReason = new DataAccessClass();
            try
            {

                //Open the connection to DB
                objDASetMRFApproveRejectReason.OpenConnection(DBConstants.GetDBConnectionString());

                //Add Parameter
                objSqlParameter = new SqlParameter[4];
                objSqlParameter[0] = new SqlParameter(SPParameter.MRFId, mrfDetail.MRFId);
                objSqlParameter[1] = new SqlParameter(SPParameter.MRFStatus, mrfDetail.Status);
                objSqlParameter[2] = new SqlParameter(SPParameter.Reason, mrfDetail.CommentReason);
                objSqlParameter[3] = new SqlParameter(SPParameter.SetMRFDetailReason, 0);
                objSqlParameter[3].Direction = ParameterDirection.Output;

                DataSet ds = objDASetMRFApproveRejectReason.GetDataSet(SPNames.MRF_ConcurrencyCheckAllocation, objSqlParameter);

                //objDASetMRFApproveRejectReason.ExecuteNonQuerySP(SPNames.MRF_ConcurrencyCheckAllocation, objSqlParameter);
                return ds;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, ConcurrencyResourceAllocation, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDASetMRFApproveRejectReason.CloseConncetion();

            }

        }

        //Mahednra Issue Id : 33860 END


        /// <summary>
        /// Set the reason for MRF when it's status is changed.
        /// </summary>
        /// <returns>Int</returns>
        public static int SetMRFApproveRejectReasonForPM(BusinessEntities.MRFDetail mrfDetail)
        {
            int checkReasonSet = -1;
            DataAccessClass objDASetMRFApproveRejectReasonForPM = new DataAccessClass();
            try
            {

                //Open the connection to DB
                objDASetMRFApproveRejectReasonForPM.OpenConnection(DBConstants.GetDBConnectionString());

                //Add Parameter
                // Rajan Kumar : Issue 46252: 12/02/2014 : Starts                        			 
                // Desc : In MRF history need to implemented in all cases in RMS.
                //Pass Email to know who is going to modified the data            
                //objSqlParameter = new SqlParameter[4];
                objSqlParameter = new SqlParameter[5];
                // Rajan Kumar : Issue 46252: 12/02/2014 : END
                objSqlParameter[0] = new SqlParameter(SPParameter.MRFId, mrfDetail.MRFId);
                objSqlParameter[1] = new SqlParameter(SPParameter.MRFStatus, mrfDetail.Status);
                objSqlParameter[2] = new SqlParameter(SPParameter.Reason, mrfDetail.CommentReason);
                objSqlParameter[3] = new SqlParameter(SPParameter.SetMRFDetailReason, 0);
                objSqlParameter[3].Direction = ParameterDirection.Output;
                objSqlParameter[4] = new SqlParameter(SPParameter.EmailId, mrfDetail.LoggedInUserEmail);
                objDASetMRFApproveRejectReasonForPM.ExecuteNonQuerySP(SPNames.MRF_SetReasonOfApproveRejectMRFForPM, objSqlParameter);
                checkReasonSet = Convert.ToInt32(objSqlParameter[3].Value);

                return checkReasonSet;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, SETMRFAPPROVEREJECTREASONFORPM, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDASetMRFApproveRejectReasonForPM.CloseConncetion();

            }

        }

        //Rakesh : Actual vs Budget 06/06/2016 Begin
        /// <summary>
        /// Set the reason for MRF when it's status is changed.
        /// </summary>
        /// <returns>Int</returns>
        //public static bool Is_NIS_NorthgateProject(int ProjectId)
        //{
        //    bool checkReasonSet = false;
        //    DataAccessClass objDASetMRFApproveRejectReasonForPM = new DataAccessClass();
        //    try
        //    {
        //        //Open the connection to DB
        //        objDASetMRFApproveRejectReasonForPM.OpenConnection(DBConstants.GetDBConnectionString());
        //        objSqlParameter = new SqlParameter[1];
        //        objSqlParameter[0] = new SqlParameter(SPParameter.ProjectId, ProjectId);

        //        int Value = Convert.ToInt32(objDASetMRFApproveRejectReasonForPM.ExecuteScalarSP(SPNames.MRF_IS_NIS_NORTHGATE, objSqlParameter));


        //        checkReasonSet = Convert.ToBoolean(Value);
        //        return checkReasonSet;
        //    }
        //    catch (RaveHRException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, SETMRFAPPROVEREJECTREASONFORPM, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
        //    }
        //    finally
        //    {
        //        objDASetMRFApproveRejectReasonForPM.CloseConncetion();

        //    }

        //}



        public static NPS_Validation Is_NIS_NorthgateProject(int ProjectId)
        {            
            DataAccessClass objDASetMRFApproveRejectReasonForPM = new DataAccessClass();
            try
            {
                //Open the connection to DB
                objDASetMRFApproveRejectReasonForPM.OpenConnection(DBConstants.GetDBConnectionString());
                objSqlParameter = new SqlParameter[1];
                objSqlParameter[0] = new SqlParameter(SPParameter.ProjectId, ProjectId);

                IDataReader objDataReader = objDASetMRFApproveRejectReasonForPM.ExecuteReaderSP(SPNames.MRF_IS_NIS_NORTHGATE, objSqlParameter);
                NPS_Validation objNPS = new NPS_Validation();
                while (objDataReader.Read())
                {
                    objNPS.IsDisableValidation = objDataReader["IsDisableValidation"].CastToBool();
                    objNPS.IsNPS_Project = objDataReader["IsNPSProject"].CastToBool();
                    
                }
                return objNPS;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, SETMRFAPPROVEREJECTREASONFORPM, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDASetMRFApproveRejectReasonForPM.CloseConncetion();
            }

        }

        /// <summary>
        /// Set the status of MRF when it will go from one transaction to another.
        /// </summary>
        /// <returns>Int</returns>
        public static int SetMRFStatus(BusinessEntities.MRFDetail mrfDetail)
        {
            int isStatusChanged = -1;
            // int isMRfTypeOFAllocation = -1;
            DataAccessClass objDASetMRFStatus = new DataAccessClass();
            string date = "01/01/0001 00:00:00";
            System.DBNull Emptydate = DBNull.Value;

            try
            {

                //Open the connection to DB
                objDASetMRFStatus.OpenConnection(DBConstants.GetDBConnectionString());

                //Add Parameter
                // Rajan Kumar : Issue 46252: 10/02/2014 : Starts                        			 
                // Desc : In MRF history need to implemented in all cases in RMS.
                //Pass Email to know who is going to modified the data
                //objSqlParameter = new SqlParameter[10];
                objSqlParameter = new SqlParameter[12];
                // Rajan Kumar : Issue 46252: 10/02/2014 : END
                
                objSqlParameter[0] = new SqlParameter(SPParameter.MRFId, mrfDetail.MRFId);
                if (mrfDetail.Status == null)
                {
                    objSqlParameter[1] = new SqlParameter(SPParameter.MRFStatus, DBNull.Value);
                }
                else
                {
                    objSqlParameter[1] = new SqlParameter(SPParameter.MRFStatus, mrfDetail.Status);
                }
                objSqlParameter[2] = new SqlParameter(SPParameter.EmpId, mrfDetail.EmployeeId);
                //objSqlParameter[3] = new SqlParameter(SPParameter.RecruitermanagerId, mrfDetail.RecruitersId);
                if (mrfDetail.ExpectedClosureDate != date && mrfDetail.ExpectedClosureDate != null)
                {
                    objSqlParameter[3] = new SqlParameter(SPParameter.ExpectedClosureDate, Convert.ToDateTime(mrfDetail.ExpectedClosureDate));
                }
                else
                {
                    objSqlParameter[3] = new SqlParameter(SPParameter.ExpectedClosureDate, Emptydate);
                }
                if (mrfDetail.AllocationDate != date && mrfDetail.AllocationDate != null)
                {
                    objSqlParameter[4] = new SqlParameter(SPParameter.AllocationDate,
                                                            Convert.ToDateTime(mrfDetail.AllocationDate));
                }
                else
                {
                    objSqlParameter[4] = new SqlParameter(SPParameter.AllocationDate, Emptydate);
                }

                objSqlParameter[5] = new SqlParameter(SPParameter.SetMRFStatus, 0);
                objSqlParameter[5].Direction = ParameterDirection.Output;

                //Siddhesh Arekar Issue ID : 55884 Closure Type
                if (mrfDetail.TypeOfClosure == 0)
                {
                    objSqlParameter[6] = new SqlParameter(SPParameter.TypeOfCloser, DBNull.Value);
                }
                else
                {
                    objSqlParameter[6] = new SqlParameter(SPParameter.TypeOfCloser, mrfDetail.TypeOfClosure);
                }
                //Siddhesh Arekar Issue ID : 55884 Closure Type

                //Adding MRFPurpose and MRFPurposeDescription data into T_MRFDetails_Hist table

                if (mrfDetail.MRFPurposeId != 0)
                {
                    objSqlParameter[7] = new SqlParameter(SPParameter.MRFPurpose, mrfDetail.MRFPurposeId);
                }
                else
                {
                    objSqlParameter[7] = new SqlParameter(SPParameter.MRFPurpose, DBNull.Value);
                }

                if (mrfDetail.MRFPurposeDescription != null && mrfDetail.MRFPurposeDescription != string.Empty)
                {
                    objSqlParameter[8] = new SqlParameter(SPParameter.MRFPurposeDescription, mrfDetail.MRFPurposeDescription);
                }
                else
                {
                    objSqlParameter[8] = new SqlParameter(SPParameter.MRFPurposeDescription, DBNull.Value);
                }

                // Rajan Kumar : Issue 46252: 10/02/2014 : Starts                        			 
                // Desc : In MRF history need to implemented in all cases in RMS.
                //Pass Email to know who is going to modified the data
                objSqlParameter[9] = new SqlParameter(SPParameter.EmailId, mrfDetail.LoggedInUserEmail);
                // Rajan Kumar : Issue 46252: 10/02/2014 : END               

                //Rakesh : Actual vs Budget 06/06/2016 Begin
                objSqlParameter[10] = new SqlParameter(SPParameter.CostCodeId, mrfDetail.CostCodeId);

                if (mrfDetail.IsOverride != null)
                    objSqlParameter[11] = new SqlParameter(SPParameter.IsOverride, mrfDetail.IsOverride);
                else
                    objSqlParameter[11] = new SqlParameter(SPParameter.IsOverride, DBNull.Value);

                //End


                objDASetMRFStatus.ExecuteNonQuerySP(SPNames.MRF_SetMRFStatus, objSqlParameter);
                isStatusChanged = Convert.ToInt32(objSqlParameter[5].Value);
                return isStatusChanged;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer,
                    CLASS_NAME_MRF, SETMRFSTATUS, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDASetMRFStatus.CloseConncetion();

            }

        }


        /// <summary>
        /// This is used to Get the MRF Summary
        /// </summary>
        /// <param name="objParameter"></param>
        /// <param name="mrfDetail"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetMrfSummary(BusinessEntities.ParameterCriteria objParameter, BusinessEntities.MRFDetail mrfDetail, ref int pageCount)
        {
            // Initialise the Collection class Object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            // Initialise the Data Access class object
            DataAccessClass objDAGetMrfSummary = new DataAccessClass();

            // Initialise the SQL parameter.
            SqlParameter[] sqlParam = new SqlParameter[20];

            try
            {
                // Open the DB connection
                objDAGetMrfSummary.OpenConnection(DBConstants.GetDBConnectionString());

                // Parameter SuperRole: check for the role COO/CEO/CFM/FM/RPM
                sqlParam[0] = new SqlParameter(SPParameter.RPMRole, SqlDbType.VarChar, 20);
                if (objParameter.RoleCEO == null && objParameter.RoleCOO == null && objParameter.RoleRPM == null)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objParameter.RoleRPM;

                sqlParam[1] = new SqlParameter(SPParameter.CFMRole, SqlDbType.VarChar, 20);
                if (objParameter.RoleCFM == null && objParameter.RoleFM == null)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objParameter.RoleCFM;

                // Parameter PreSales role
                sqlParam[2] = new SqlParameter(SPParameter.PresalesRole, SqlDbType.VarChar, 20);
                if (objParameter.RolePreSales == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objParameter.RolePreSales;

                // Parameter PM Role
                sqlParam[3] = new SqlParameter(SPParameter.PMRole, SqlDbType.VarChar, 20);
                if (objParameter.RolePM == null && objParameter.RoleGPM == null && objParameter.RoleAPM == null)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = objParameter.RolePM;

                // Parameter Recruitment role
                sqlParam[4] = new SqlParameter(SPParameter.RecruitmentRole, SqlDbType.VarChar, 20);
                if (objParameter.RoleRecruitment == null)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = objParameter.RoleRecruitment;

                // Parameter HR role
                sqlParam[5] = new SqlParameter(SPParameter.HRRole, SqlDbType.VarChar, 20);
                if (objParameter.RoleHR == null)
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = objParameter.RoleHR;

                // Parameter Testing role
                sqlParam[6] = new SqlParameter(SPParameter.TestingRole, SqlDbType.VarChar, 20);
                if (objParameter.RoleTesting == null)
                    sqlParam[6].Value = DBNull.Value;
                else
                    sqlParam[6].Value = objParameter.RoleTesting;

                // Parameter PMOQuality role
                sqlParam[7] = new SqlParameter(SPParameter.QualityRole, SqlDbType.VarChar, 20);
                if (objParameter.RoleQuality == null)
                    sqlParam[7].Value = DBNull.Value;
                else
                    sqlParam[7].Value = objParameter.RoleQuality;

                // Parameter Marketing role
                sqlParam[8] = new SqlParameter(SPParameter.MarketingRole, SqlDbType.VarChar, 20);
                if (objParameter.RoleMarketing == null)
                    sqlParam[8].Value = DBNull.Value;
                else
                    sqlParam[8].Value = objParameter.RoleMarketing;

                // Parameter UserMail Id
                sqlParam[9] = new SqlParameter(SPParameter.UserMailId, SqlDbType.VarChar, 50);
                if (objParameter.EMailID == null)
                    sqlParam[9].Value = DBNull.Value;
                else
                    sqlParam[9].Value = objParameter.EMailID;

                // Parameter Department Id
                sqlParam[10] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.Int);
                if (mrfDetail.DepartmentId == CommonConstants.ZERO)
                    sqlParam[10].Value = DBNull.Value;
                else
                    sqlParam[10].Value = mrfDetail.DepartmentId;

                // Parameter Project Id
                sqlParam[11] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int);
                if (mrfDetail.ProjectId == CommonConstants.ZERO)
                    sqlParam[11].Value = DBNull.Value;
                else
                    sqlParam[11].Value = mrfDetail.ProjectId;

                // Parameter Role Id 
                sqlParam[12] = new SqlParameter(SPParameter.RoleId, SqlDbType.Int);
                if (mrfDetail.RoleId == CommonConstants.ZERO)
                    sqlParam[12].Value = DBNull.Value;
                else
                    sqlParam[12].Value = mrfDetail.RoleId;

                // Parameter Status Id
                sqlParam[13] = new SqlParameter(SPParameter.StatusId, SqlDbType.Int);
                if (mrfDetail.StatusId == CommonConstants.ZERO)
                    sqlParam[13].Value = DBNull.Value;
                else
                    sqlParam[13].Value = mrfDetail.StatusId;

                // Parameter Page Number
                sqlParam[14] = new SqlParameter(SPParameter.pageNum, SqlDbType.Int);
                if (objParameter.PageNumber == CommonConstants.ZERO)
                    sqlParam[14].Value = DBNull.Value;
                else
                    sqlParam[14].Value = objParameter.PageNumber;

                // Parameter Page Size
                sqlParam[15] = new SqlParameter(SPParameter.pageSize, SqlDbType.Int);
                if (objParameter.PageSize == CommonConstants.ZERO)
                    sqlParam[15].Value = DBNull.Value;
                else
                    sqlParam[15].Value = objParameter.PageSize;

                // Parameter Sort Expression And Direction
                sqlParam[16] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                if (objParameter.SortExpressionAndDirection == null)
                    sqlParam[16].Value = DBNull.Value;
                else
                    sqlParam[16].Value = objParameter.SortExpressionAndDirection;

                // Parameter RaveCOnsultant role
                sqlParam[17] = new SqlParameter(SPParameter.RaveConsultantRole, SqlDbType.VarChar, 20);
                if (objParameter.RoleRaveConsultant == null)
                    sqlParam[17].Value = DBNull.Value;
                else
                    sqlParam[17].Value = objParameter.RoleRaveConsultant;

                //Ishwar Patil 30092014 For NIS : Start
                //Parameter for RMS MRF
                sqlParam[18] = new SqlParameter(SPParameter.IsRMSMRF, SqlDbType.VarChar, 10);
                if (objParameter.IsRMSMRF == null)
                    sqlParam[18].Value = DBNull.Value;
                else
                    sqlParam[18].Value = objParameter.IsRMSMRF;
                //Ishwar Patil 30092014 For NIS : End

                // Output parameter Page Count
                sqlParam[19] = new SqlParameter(SPParameter.pageCount, SqlDbType.Int);
                sqlParam[19].Direction = ParameterDirection.Output;

                // Execute the SP and pass parameter to SP
                DataSet ds = objDAGetMrfSummary.GetDataSet(SPNames.MRF_GetMRFSummaryFilterAndPaging, sqlParam);

                // Assign PageCount the value returned from SP
                pageCount = Convert.ToInt32(sqlParam[19].Value);

                // Read the Dataset and assign it to Collection
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    // Initialise the Business Entity 
                    objMrfDetail = new BusinessEntities.MRFDetail();

                    //Assign MRf Id
                    objMrfDetail.MRFId = int.Parse(dr[DbTableColumn.MRFID].ToString());
                    // Assign MRF Code
                    objMrfDetail.MRFCode = dr[DbTableColumn.MRFCode].ToString();
                    // Assign RP Code
                    objMrfDetail.RPCode = dr[DbTableColumn.RPCode].ToString();
                    // Assign Role
                    objMrfDetail.Role = dr[DbTableColumn.Role].ToString();
                    // Assign Project Name
                    objMrfDetail.ProjectName = dr[DbTableColumn.ProjectName].ToString();
                    // Assign Resource On Board
                    objMrfDetail.ResourceOnBoard = DateTime.Parse(dr[DbTableColumn.ResourceOnBoard].ToString());
                    //Assign ExpectedClosureDate
                    if (dr[DbTableColumn.ExpectedClosureDate].ToString() != null && dr[DbTableColumn.ExpectedClosureDate].ToString() != "")
                    {
                        objMrfDetail.ExpectedClosureDate = DateTime.Parse(dr[DbTableColumn.ExpectedClosureDate].ToString()).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        objMrfDetail.ExpectedClosureDate = string.Empty;
                    }
                    // Assign Employee Name
                    objMrfDetail.RaisedBy = dr[DbTableColumn.RaiseBy].ToString();
                    // Assign Status
                    objMrfDetail.Status = dr[DbTableColumn.Status].ToString();

                    // Assign Department Name
                    objMrfDetail.DepartmentName = dr[DbTableColumn.DeptName].ToString();
                    //Assign the Resource Alllocated name.
                    objMrfDetail.EmployeeName = dr[DbTableColumn.EmployeeName].ToString();

                    // venkatesh  : Issue 46380 : 19/11/2013 : Starts
                    // Desc : Mrf Summary - In export to excel 
                    if (dr[DbTableColumn.JoiningDate].ToString() != null && dr[DbTableColumn.JoiningDate].ToString() != "")
                    {
                        objMrfDetail.DateOfJoining = DateTime.Parse(dr[DbTableColumn.JoiningDate].ToString()).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        objMrfDetail.DateOfJoining = string.Empty;
                    }
                    objMrfDetail.Designation = dr[DbTableColumn.Designation].ToString();
                    // venkatesh  : Issue 46380 : 19/11/2013 : End


                    objMrfDetail.RecruitmentManager = dr[DbTableColumn.RecruitmentManager].ToString();

                    if (dr[DbTableColumn.ClientName].ToString() != null && dr[DbTableColumn.ClientName].ToString() != string.Empty)
                    {
                        objMrfDetail.ClientName = dr[DbTableColumn.ClientName].ToString();
                    }
                    else
                    {
                        objMrfDetail.ClientName = string.Empty;
                    }
                    if (objMrfDetail.Status == "Abort" || objMrfDetail.Status == "Closed")
                    {
                        if (dr["LastModifiedDate"].ToString() != null && dr["LastModifiedDate"].ToString() != "")
                        {
                            objMrfDetail.AbortedOrClosedDate = Convert.ToDateTime(dr["LastModifiedDate"]).ToShortDateString();
                        }
                        else
                        {
                            objMrfDetail.AbortedOrClosedDate = "";
                        }
                    }
                    if (objMrfDetail.Status == "Closed")
                    {
                        if (dr["AllocationDate"].ToString() != null && dr["AllocationDate"].ToString() != "")
                        {
                            objMrfDetail.AllocationDate = Convert.ToDateTime(dr["AllocationDate"]).ToShortDateString();
                        }
                        else
                        {
                            objMrfDetail.AllocationDate = "";
                        }
                    }
                    if (dr[DbTableColumn.RecruitmentAssignDate].ToString() != null && dr[DbTableColumn.RecruitmentAssignDate].ToString() != "")
                    {
                        objMrfDetail.RecruitmentAssignDate = DateTime.Parse(dr[DbTableColumn.RecruitmentAssignDate].ToString()).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        objMrfDetail.RecruitmentAssignDate = string.Empty;
                    }

                    // 29173-Ambar-Start-05092011
                    objMrfDetail.TypeOFMRF = dr["TypeOFMRF"].ToString();
                    // 29173-Ambar-End-05092011

                    //Rajnikant: Issue 45708 : 18/09/2014 : Starts                        			  
                    //Desc :  Get Skill of MRF
                    objMrfDetail.Skill = dr[DbTableColumn.Skill].ToString();
                    //Rajnikant: Issue 45708 : 18/09/2014 : Ends

                    // Add the object to Collection
                    raveHRCollection.Add(objMrfDetail);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, GETMRFSUMMARY, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAGetMrfSummary.CloseConncetion();
            }
            // return Collection
            return raveHRCollection;
        }

        /// <summary>
        /// Gets the MRF Summary for PageLoad
        /// </summary>
        /// <param name="objParameter"></param>
        /// <param name="pageCount"></param>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetMrfSummaryForPageLoad(BusinessEntities.ParameterCriteria objParameter, ref int pageCount)
        {
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            // Initialise the Data Access class object
            DataAccessClass objDAGetMrfSummaryForPageLoad = new DataAccessClass();

            // Initialise the SQL parameter.
            SqlParameter[] sqlParam = new SqlParameter[16];

            try
            {
                // Open the DB connection
                objDAGetMrfSummaryForPageLoad.OpenConnection(DBConstants.GetDBConnectionString());

                // Parameter SuperRole: check for the role COO/CEO/CFM/FM/RPM
                sqlParam[0] = new SqlParameter(SPParameter.RPMRole, SqlDbType.VarChar, 20);
                if (objParameter.RoleCEO == null && objParameter.RoleCOO == null && objParameter.RoleRPM == null)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objParameter.RoleRPM;

                // Parameter SuperRole: check for the role COO/CEO/CFM/FM/RPM
                sqlParam[1] = new SqlParameter(SPParameter.CFMRole, SqlDbType.VarChar, 20);
                if (objParameter.RoleCFM == null && objParameter.RoleFM == null)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objParameter.RoleCFM;

                // Parameter PreSales role
                sqlParam[2] = new SqlParameter(SPParameter.PresalesRole, SqlDbType.VarChar, 20);
                if (objParameter.RolePreSales == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objParameter.RolePreSales;

                // Parameter PM Role
                sqlParam[3] = new SqlParameter(SPParameter.PMRole, SqlDbType.VarChar, 20);
                if (objParameter.RolePM == null && objParameter.RoleGPM == null && objParameter.RoleAPM == null)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = objParameter.RolePM;

                // Parameter Recruitment role
                sqlParam[4] = new SqlParameter(SPParameter.RecruitmentRole, SqlDbType.VarChar, 20);
                if (objParameter.RoleRecruitment == null)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = objParameter.RoleRecruitment;

                // Parameter HR role
                sqlParam[5] = new SqlParameter(SPParameter.HRRole, SqlDbType.VarChar, 20);
                if (objParameter.RoleHR == null)
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = objParameter.RoleHR;

                // Parameter Testing role
                sqlParam[6] = new SqlParameter(SPParameter.TestingRole, SqlDbType.VarChar, 20);
                if (objParameter.RoleTesting == null)
                    sqlParam[6].Value = DBNull.Value;
                else
                    sqlParam[6].Value = objParameter.RoleTesting;

                // Parameter PMOQuality role
                sqlParam[7] = new SqlParameter(SPParameter.QualityRole, SqlDbType.VarChar, 20);
                if (objParameter.RoleQuality == null)
                    sqlParam[7].Value = DBNull.Value;
                else
                    sqlParam[7].Value = objParameter.RoleQuality;

                // Parameter Marketing role
                sqlParam[8] = new SqlParameter(SPParameter.MarketingRole, SqlDbType.VarChar, 20);
                if (objParameter.RoleMarketing == null)
                    sqlParam[8].Value = DBNull.Value;
                else
                    sqlParam[8].Value = objParameter.RoleMarketing;

                // Parameter UserMail Id
                sqlParam[9] = new SqlParameter(SPParameter.UserMailId, SqlDbType.VarChar, 50);
                if (objParameter.EMailID == null)
                    sqlParam[9].Value = DBNull.Value;
                else
                    sqlParam[9].Value = objParameter.EMailID;

                // Parameter Page Number
                sqlParam[10] = new SqlParameter(SPParameter.pageNum, SqlDbType.Int);
                if (objParameter.PageNumber == CommonConstants.ZERO)
                    sqlParam[10].Value = DBNull.Value;
                else
                    sqlParam[10].Value = objParameter.PageNumber;

                // Parameter Page Size
                sqlParam[11] = new SqlParameter(SPParameter.pageSize, SqlDbType.Int);
                if (objParameter.PageSize == CommonConstants.ZERO)
                    sqlParam[11].Value = DBNull.Value;
                else
                    sqlParam[11].Value = objParameter.PageSize;

                // Parameter Sort Expression And Direction
                sqlParam[12] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                if (objParameter.SortExpressionAndDirection == null)
                    sqlParam[12].Value = DBNull.Value;
                else
                    sqlParam[12].Value = objParameter.SortExpressionAndDirection;

                sqlParam[13] = new SqlParameter(SPParameter.RaveConsultantRole, SqlDbType.VarChar, 20);
                if (objParameter.RoleRaveConsultant == null)
                    sqlParam[13].Value = DBNull.Value;
                else
                    sqlParam[13].Value = objParameter.RoleRaveConsultant;

                //Ishwar Patil 29092014 For NIS : Start
                sqlParam[14] = new SqlParameter(SPParameter.IsRMSMRF, SqlDbType.VarChar, 10);
                if (objParameter.IsRMSMRF == null)
                    sqlParam[14].Value = DBNull.Value;
                else
                    sqlParam[14].Value = objParameter.IsRMSMRF;
                //Ishwar Patil 29092014 For NIS : End

                // Output parameter Page Count
                sqlParam[15] = new SqlParameter(SPParameter.pageCount, SqlDbType.Int);
                sqlParam[15].Direction = ParameterDirection.Output;

                // Execute the SP and pass parameter to SP
                DataSet ds = objDAGetMrfSummaryForPageLoad.GetDataSet(SPNames.MRF_GetMRFSummaryForPageLoad, sqlParam);

                // Assign PageCount the value returned from SP
                pageCount = Convert.ToInt32(sqlParam[15].Value);

                // Read the Dataset and assign it to Collection
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    // Initialise the Business Entity 
                    objMrfDetail = new BusinessEntities.MRFDetail();

                    //Assign MRf Id
                    objMrfDetail.MRFId = int.Parse(dr[DbTableColumn.MRFID].ToString());
                    // Assign MRF Code
                    objMrfDetail.MRFCode = dr[DbTableColumn.MRFCode].ToString();
                    // Assign RP Code
                    objMrfDetail.RPCode = dr[DbTableColumn.RPCode].ToString();
                    // Assign Role
                    objMrfDetail.Role = dr[DbTableColumn.Role].ToString();
                    // Assign Project Name
                    objMrfDetail.ProjectName = dr[DbTableColumn.ProjectName].ToString();
                    // Assign Resource On Board
                    objMrfDetail.ResourceOnBoard = DateTime.Parse(dr[DbTableColumn.ResourceOnBoard].ToString());
                    //Assign ExpectedClosureDate
                    if (dr[DbTableColumn.ExpectedClosureDate].ToString() != null && dr[DbTableColumn.ExpectedClosureDate].ToString() != "")
                    {
                        objMrfDetail.ExpectedClosureDate = DateTime.Parse(dr[DbTableColumn.ExpectedClosureDate].ToString()).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        objMrfDetail.ExpectedClosureDate = string.Empty;
                    }
                    // Assign Employee Name
                    objMrfDetail.RaisedBy = dr[DbTableColumn.RaiseBy].ToString();
                    // Assign Status
                    objMrfDetail.Status = dr[DbTableColumn.Status].ToString();

                    // Assign Department Name
                    objMrfDetail.DepartmentName = dr[DbTableColumn.DeptName].ToString();

                    // venkatesh  : Issue 46380 : 19/11/2013 : Starts
                    // Desc : Mrf Summary - In export to excel 
                    objMrfDetail.EmployeeName = dr[DbTableColumn.EmployeeName].ToString();
                    if (dr[DbTableColumn.JoiningDate].ToString() != null && dr[DbTableColumn.JoiningDate].ToString() != "")
                    {
                        objMrfDetail.DateOfJoining = DateTime.Parse(dr[DbTableColumn.JoiningDate].ToString()).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        objMrfDetail.DateOfJoining = string.Empty;
                    }
                    objMrfDetail.Designation = dr[DbTableColumn.Designation].ToString();
                    // venkatesh  : Issue 46380 : 19/11/2013 : End


                    if (dr[DbTableColumn.RecruitmentManager].ToString() != null && dr[DbTableColumn.RecruitmentManager].ToString() != string.Empty)
                    {
                        objMrfDetail.RecruitmentManager = dr[DbTableColumn.RecruitmentManager].ToString();
                    }

                    else
                    {
                        objMrfDetail.RecruitmentManager = dr[DbTableColumn.RecruitmentManager].ToString();
                    }


                    if (dr[DbTableColumn.ClientName].ToString() != null && dr[DbTableColumn.ClientName].ToString() != string.Empty)
                    {
                        objMrfDetail.ClientName = dr[DbTableColumn.ClientName].ToString();
                    }
                    else
                    {
                        objMrfDetail.ClientName = string.Empty;
                    }
                    if (dr[DbTableColumn.RecruitmentAssignDate].ToString() != null && dr[DbTableColumn.RecruitmentAssignDate].ToString() != "")
                    {
                        objMrfDetail.RecruitmentAssignDate = DateTime.Parse(dr[DbTableColumn.RecruitmentAssignDate].ToString()).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        objMrfDetail.RecruitmentAssignDate = string.Empty;
                    }

                    // 29173-Ambar-Start-05092011
                    objMrfDetail.TypeOFMRF = dr[DbTableColumn.TypeOFMRF].ToString();// dr["TypeOFMRF"].ToString();
                    // 29173-Ambar-End-05092011

                    //Rajnikant: Issue 45708 : 18/09/2014 : Starts                        			  
                    //Desc :  Get Skill of MRF
                    objMrfDetail.Skill = dr[DbTableColumn.Skill].ToString();
                    //Rajnikant: Issue 45708 : 18/09/2014 : Ends

                    // Add the object to Collection
                    raveHRCollection.Add(objMrfDetail);
                }

            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, GETMRFSUMMARYFORPAGELOAD, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAGetMrfSummaryForPageLoad.CloseConncetion();
            }

            // return Collection
            return raveHRCollection;
        }

        /// <summary>
        /// Function will Get The Project Name as per Role Wise
        /// </summary>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetProjectNameRoleWiseDL(BusinessEntities.ParameterCriteria parameterCriteria)
        {
            DataAccessClass objDAGetProjectNameRoleWiseDL = new DataAccessClass();

            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                objDAGetProjectNameRoleWiseDL.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter(SPParameter.EmpId, parameterCriteria.EMailID);
                param[1] = new SqlParameter(SPParameter.Role, parameterCriteria.RoleRPM);
                param[2] = new SqlParameter(SPParameter.PMRole, Convert.ToString(AuthorizationManagerConstants.ROLEPROJECTMANAGER));
                param[3] = new SqlParameter(SPParameter.RPMRole, Convert.ToString(AuthorizationManagerConstants.ROLERPM));

                objDataReader = objDAGetProjectNameRoleWiseDL.ExecuteReaderSP(SPNames.MRF_GetProjectName_RoleWise, param);

                while (objDataReader.Read())
                {
                    BusinessEntities.MRFDetail mRFDetail = new BusinessEntities.MRFDetail();
                    mRFDetail.ProjectId = Convert.ToInt32(objDataReader[DbTableColumn.ProjectId]);
                    mRFDetail.ProjectName = objDataReader[DbTableColumn.ProjectName].ToString();
                    mRFDetail.Domain = objDataReader[DbTableColumn.Domain].ToString();
                    mRFDetail.ProjectStartDate = objDataReader[DbTableColumn.ProjectStartDate].ToString();
                    mRFDetail.ProjectEndDate = objDataReader[DbTableColumn.ProjectEndDate].ToString();
                    mRFDetail.ProjectDescription = objDataReader[DbTableColumn.ProjectDescription].ToString();

                    raveHRCollection.Add(mRFDetail);
                }

                return raveHRCollection;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, GETPROJECTNAMEROLEWISEDL, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDAGetProjectNameRoleWiseDL.CloseConncetion();
            }
        }

        /// <summary>
        /// Function will Get Resource Plan as per Project Name wise
        /// </summary>
        /// <param name="mRFDetailBusinessEntity"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetResourcePlanProjectWiseDL(int ProjectID)
        {
            DataAccessClass objDAGetResourcePlanProjectWiseDL = new DataAccessClass();

            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                objDAGetResourcePlanProjectWiseDL.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter(SPParameter.ProjectId, ProjectID);
                param[1] = new SqlParameter(SPParameter.RPStatusId, Convert.ToInt32(MasterEnum.ResourcePlanStatus.Active));
                param[2] = new SqlParameter(SPParameter.RPApprovalStatusId, Convert.ToInt32(MasterEnum.ResourcePlanApprovalStatus.Approved));

                objDataReader = objDAGetResourcePlanProjectWiseDL.ExecuteReaderSP(SPNames.MRF_GetRP_ProjectWise, param);

                while (objDataReader.Read())
                {
                    KeyValue<string> keyValue = new KeyValue<string>();
                    keyValue.KeyName = objDataReader.GetValue(0).ToString();
                    keyValue.Val = objDataReader.GetValue(1).ToString();
                    raveHRCollection.Add(keyValue);
                }

                return raveHRCollection;

            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, GETRESOURCEPLANPROJECTWISEDL, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDAGetResourcePlanProjectWiseDL.CloseConncetion();
            }
        }

        /// <summary>
        /// Function will Get Roles as per Sellected Resource Plan
        /// </summary>
        /// <param name="ResourcePlanID"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetRoleResourcePlanWiseDL(int ResourcePlanID, int ResourcePlanDurationStatus)
        {
            DataAccessClass objDAGetRoleResourcePlanWiseDL = new DataAccessClass();

            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                objDAGetRoleResourcePlanWiseDL.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter(SPParameter.ResourcePlanId, ResourcePlanID);
                param[1] = new SqlParameter(SPParameter.RPDuDeleted, ResourcePlanDurationStatus);

                objDataReader = objDAGetRoleResourcePlanWiseDL.ExecuteReaderSP(SPNames.MRF_GetRole_RPWise, param);

                while (objDataReader.Read())
                {
                    KeyValue<string> keyValue = new KeyValue<string>();
                    keyValue.KeyName = objDataReader.GetValue(0).ToString();
                    keyValue.Val = objDataReader.GetValue(1).ToString();
                    raveHRCollection.Add(keyValue);
                }

                return raveHRCollection;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, GETROLERESOURCEPLANWISEDL, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDAGetRoleResourcePlanWiseDL.CloseConncetion();
            }
        }

        /// <summary>
        /// Function Will Fill Resource Plan Grid As per Selected Role
        /// </summary>
        /// <param name="ResourcePlanID"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetResourcePlanGridRoleWiseDL(int RoleID, int ResourcePlanID)
        {
            DataAccessClass objDAGetResourcePlanGridRoleWiseDL = new DataAccessClass();
            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                objDAGetResourcePlanGridRoleWiseDL.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter(SPParameter.RoleId, RoleID);
                param[1] = new SqlParameter(SPParameter.ResourcePlanId, ResourcePlanID);
                param[2] = new SqlParameter(SPParameter.RPDuDeleted, Convert.ToInt32(MasterEnum.RPDurationEditionStatus.Deleted));

                objDataReader = objDAGetResourcePlanGridRoleWiseDL.ExecuteReaderSP(SPNames.MRF_GetResourcePlanDuration_RoleWise, param);
                while (objDataReader.Read())
                {
                    BusinessEntities.MRFDetail mRFDetail = new BusinessEntities.MRFDetail();

                    mRFDetail.CheckGridValue = Convert.ToBoolean(objDataReader[DbTableColumn.ChkValue]);
                    mRFDetail.ResourcePlanDurationId = Convert.ToInt32(objDataReader[DbTableColumn.ResourcePlanDurationId]);
                    mRFDetail.ResourcePlanStartDate = Convert.ToDateTime(objDataReader[DbTableColumn.StartDate]);
                    mRFDetail.ResourcePlanEndDate = Convert.ToDateTime(objDataReader[DbTableColumn.EndDate]);
                    mRFDetail.ResourceLocation = Convert.ToString(objDataReader[DbTableColumn.Location]);
                    mRFDetail.Billing = Convert.ToInt32(objDataReader[DbTableColumn.Billing]);
                    mRFDetail.Utilization = Convert.ToInt32(objDataReader[DbTableColumn.Utilization]);

                    raveHRCollection.Add(mRFDetail);
                }

                return raveHRCollection;

            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, GETRESOURCEPLANGRIDROLEWISEDL, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDAGetResourcePlanGridRoleWiseDL.CloseConncetion();
            }
        }

        /// <summary>
        /// Get the internal resource to allocate internal resource for MRF.
        /// </summary>
        /// <returns>Collection</returns>
        public static BusinessEntities.RaveHRCollection GetListOfInternalResource(BusinessEntities.MRFDetail mrfDetail, int departmentId, BusinessEntities.ParameterCriteria objParameterCriteria, ref int pageCount)
        {

            int ilowerExperience = 0;
            int iupperExperience = 0;
            // Initialise Data Access Class object
            DataAccessClass objDAGetListOfInternalResource = new DataAccessClass();

            try
            {
                string lowerExperience = string.Empty;
                string upperExperience = string.Empty;
                char[] SPILITER_DASH = { '-' };
                if (mrfDetail.ExperienceString != null)
                {
                    string[] CTCarr = Convert.ToString(mrfDetail.ExperienceString).Split(SPILITER_DASH);
                    lowerExperience = CTCarr[0].ToString();
                    upperExperience = CTCarr[1].ToString();
                    ilowerExperience = Convert.ToInt32(lowerExperience) * 12;
                    iupperExperience = Convert.ToInt32(upperExperience) * 12;

                }
                // Initialise Collection class object
                raveHRCollection = new BusinessEntities.RaveHRCollection();
                //Open the connection to DB
                objDAGetListOfInternalResource.OpenConnection(DBConstants.GetDBConnectionString());
                //Add Parameter
                SqlParameter[] param = new SqlParameter[10];

                param[0] = new SqlParameter(SPParameter.MRFId, mrfDetail.MRFId);

                param[1] = new SqlParameter(SPParameter.DepartmentId, departmentId);

                param[2] = new SqlParameter(SPParameter.LowerExperince, ilowerExperience);

                param[3] = new SqlParameter(SPParameter.UpperExperience, iupperExperience);

                param[4] = new SqlParameter(SPParameter.RoleId, mrfDetail.RoleId);

                if (mrfDetail.Skill != null)
                    param[5] = new SqlParameter(SPParameter.Skill, mrfDetail.MustToHaveSkills);
                else
                    param[5] = new SqlParameter(SPParameter.Skill, DBNull.Value);

                param[6] = new SqlParameter(SPParameter.SortExpression, objParameterCriteria.SortExpressionAndDirection);
                param[7] = new SqlParameter(SPParameter.pageNum, objParameterCriteria.PageNumber);
                param[8] = new SqlParameter(SPParameter.pageSize, objParameterCriteria.PageSize);
                param[9] = new SqlParameter(SPParameter.pageCount, SqlDbType.Int);
                param[9].Direction = ParameterDirection.Output;

                //DataSet ds = objDAGetListOfInternalResource.GetDataSet(SPNames.MRF_GetInternalResourceWith_FilterCreatiria, param);
                DataSet ds = objDAGetListOfInternalResource.GetDataSet("USP_MRF_Test", param);
                pageCount = Convert.ToInt32(param[9].Value);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //Initialise the Business Entity object
                    objMrfDetail = new BusinessEntities.MRFDetail();
                    objMrfDetail.EmployeeId = Convert.ToInt32(dr[DbTableColumn.EMPId]);
                    objMrfDetail.EmployeeName = Convert.ToString(dr[DbTableColumn.ResourceName]);
                    if (departmentId == 1)
                    {
                        objMrfDetail.ProjectName = Convert.ToString(dr[DbTableColumn.CurrentProjectName]);
                        objMrfDetail.Billing = Convert.ToInt32(dr[DbTableColumn.Utilization]);
                        objMrfDetail.Utilization = Convert.ToInt32(dr[DbTableColumn.Utilization]);
                        if (Convert.ToDateTime(dr[DbTableColumn.ReleaseDate]).Date.Date.Year != 1900)
                        {
                            objMrfDetail.ResourceReleased = Convert.ToDateTime(dr[DbTableColumn.ReleaseDate]);
                            objMrfDetail.Remarks = objMrfDetail.ResourceReleased.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            objMrfDetail.Remarks = "NA";
                        }
                    }
                    if (mrfDetail.DepartmentId != 1)
                    {
                        //CR: In is Designation is dispalyed.
                        objMrfDetail.DepartmentName = Convert.ToString(dr[DbTableColumn.Designation]);
                    }
                    raveHRCollection.Add(objMrfDetail);
                }
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, GETLISTOFINTERNALRESOURCE, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDAGetListOfInternalResource.CloseConncetion();
            }
        }

        /// <summary>
        /// Get the List of MRF whose status is pending Allocation.
        /// </summary>
        /// <returns>Collection</returns>
        public static BusinessEntities.RaveHRCollection GetMRFDetailsForPendingAllocation(BusinessEntities.ParameterCriteria objParameterCriteria, ref int pageCount)
        {
            // Initialise Data Access Class object
            DataAccessClass objDAGetMRFDetailsForPendingAllocation = new DataAccessClass();
            try
            {
                // Initialise Collection class object
                raveHRCollection = new BusinessEntities.RaveHRCollection();
                //Open the connection to DB
                objDAGetMRFDetailsForPendingAllocation.OpenConnection(DBConstants.GetDBConnectionString());
                //Add Parameter
                objSqlParameter = new SqlParameter[4];
                objSqlParameter[0] = new SqlParameter(SPParameter.SortExpression, objParameterCriteria.SortExpressionAndDirection);
                objSqlParameter[1] = new SqlParameter(SPParameter.pageNum, objParameterCriteria.PageNumber);
                objSqlParameter[2] = new SqlParameter(SPParameter.pageSize, objParameterCriteria.PageSize);
                objSqlParameter[3] = new SqlParameter(SPParameter.pageCount, SqlDbType.Int);
                objSqlParameter[3].Direction = ParameterDirection.Output;
                DataSet ds = objDAGetMRFDetailsForPendingAllocation.GetDataSet(SPNames.MRF_GetPendingAllocationMRFDetails, objSqlParameter);
                pageCount = Convert.ToInt32(objSqlParameter[3].Value);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //Initialise the Business Entity object
                    objMrfDetail = new BusinessEntities.MRFDetail();
                    objMrfDetail.MRFId = Convert.ToInt32(dr[DbTableColumn.MRFID]);
                    objMrfDetail.MRFCode = Convert.ToString(dr[DbTableColumn.MRFCode]);
                    //objMrfDetail.RPCode = Convert.ToString(dr[DbTableColumn.RPCode]);
                    objMrfDetail.Role = Convert.ToString(dr[DbTableColumn.Role]);
                    objMrfDetail.ProjectName = Convert.ToString(dr[DbTableColumn.ProjectName]);
                    objMrfDetail.ResourceOnBoard = Convert.ToDateTime(dr[DbTableColumn.ResourceOnBoard]);
                    objMrfDetail.EmployeeName = Convert.ToString(dr[DbTableColumn.MRFRaisedBy]);
                    if (!string.IsNullOrEmpty(Convert.ToString(dr[DbTableColumn.EmployeeName])))
                    {
                        objMrfDetail.NewEmployeeName = Convert.ToString(dr[DbTableColumn.EmployeeName]);
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(dr[DbTableColumn.RecruiterName])))
                    {
                        objMrfDetail.RecruiterName = Convert.ToString(dr[DbTableColumn.RecruiterName]);
                    }
                    objMrfDetail.DepartmentName = Convert.ToString(dr[DbTableColumn.DeptName]);
                    objMrfDetail.Status = Convert.ToString(dr[DbTableColumn.Status]);
                    if (!string.IsNullOrEmpty(Convert.ToString(dr[DbTableColumn.ClientName])))
                    {
                        objMrfDetail.ClientName = Convert.ToString(dr[DbTableColumn.ClientName]);
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dr[DbTableColumn.TypeofSupply])))
                    {
                        objMrfDetail.TypeOfSupplyName = Convert.ToString(dr[DbTableColumn.TypeofSupply]);
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dr[DbTableColumn.TypeOfAllocation])))
                    {
                        objMrfDetail.TypeOfAllocationName = Convert.ToString(dr[DbTableColumn.TypeOfAllocation]);
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dr[DbTableColumn.FutureAllocateResourceName])))
                    {
                        objMrfDetail.FutureAllocateResourceName = Convert.ToString(dr[DbTableColumn.FutureAllocateResourceName]);
                    }

                    if (dr[DbTableColumn.FutureAllocationDate] != DBNull.Value)
                    {
                        objMrfDetail.FutureAllocationDate = Convert.ToDateTime(dr[DbTableColumn.FutureAllocationDate]).ToShortDateString().ToString();
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dr[DbTableColumn.FutureAllocateResourceName])))
                    {
                        objMrfDetail.FutureAllocateResourceName = Convert.ToString(dr[DbTableColumn.FutureAllocateResourceName]);
                    }

                    if (dr[DbTableColumn.FutureEmpID] != DBNull.Value)
                    {
                        objMrfDetail.FutureEmpID = Convert.ToInt32(dr[DbTableColumn.FutureEmpID].ToString());
                    }


                    raveHRCollection.Add(objMrfDetail);
                }
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME_MRF, GETMRFDETAILSFORPENDINGALLOCATION, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAGetMRFDetailsForPendingAllocation.CloseConncetion();
            }
        }

        /// <summary>
        /// Gets the Approve/Reject MRF 
        /// </summary>
        /// <param name="objParameter"></param>
        /// <param name="pageCount"></param>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetApproveRejectMrf(BusinessEntities.ParameterCriteria objParameter, ref int pageCount)
        {

            raveHRCollection = new BusinessEntities.RaveHRCollection();

            // Initialise the Data Access class object
            DataAccessClass objDAetApproveRejectMrf = new DataAccessClass();

            // Initialise the SQL parameter.
            SqlParameter[] sqlParam = new SqlParameter[4];

            try
            {
                // Open the DB connection
                objDAetApproveRejectMrf.OpenConnection(DBConstants.GetDBConnectionString());

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
                DataSet ds = objDAetApproveRejectMrf.GetDataSet(SPNames.MRF_GetApproveRejectMrf, sqlParam);

                // Assign PageCount the value returned from SP
                pageCount = Convert.ToInt32(sqlParam[3].Value);

                // Read the DataReader and assign it to Collection
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    // Initialise the Business Entity 
                    objMrfDetail = new BusinessEntities.MRFDetail();

                    //Assign MrfId
                    objMrfDetail.MRFId = int.Parse(dr[DbTableColumn.MRFID].ToString());
                    // Assign MRF Code
                    objMrfDetail.MRFCode = dr[DbTableColumn.MRFCode].ToString();
                    //Asign resource Name
                    objMrfDetail.EmployeeName = dr[DbTableColumn.ResourceName].ToString();
                    //Assign Start Date
                    objMrfDetail.ResourceOnBoard = DateTime.Parse(dr[DbTableColumn.StartDate].ToString());
                    //Assign End Date
                    // Rajan Kumar : Issue 45752 : 03/01/2014 : Starts                        			 
                    // Desc : When MRF is raised against department then MRF end date is not mandatory (remove validation).
                    if (dr[DbTableColumn.EndDate] != DBNull.Value)
                    {
                        objMrfDetail.ResourceReleased = DateTime.Parse(dr[DbTableColumn.EndDate].ToString());
                    }
                    // Rajan Kumar : Issue 45752 : 03/01/2014: Ends 
                                       
                    //Assign Billing
                    objMrfDetail.Billing = int.Parse(dr[DbTableColumn.Billing].ToString());
                    // Assign Department Name
                    objMrfDetail.DepartmentName = dr[DbTableColumn.DeptName].ToString();
                    // Assign Employee Name
                    objMrfDetail.RaisedBy = dr[DbTableColumn.RaiseBy].ToString();
                    // Assign Project Name
                    objMrfDetail.ProjectName = dr[DbTableColumn.ProjectName].ToString();
                    //Assign Client Name
                    objMrfDetail.ClientName = dr[DbTableColumn.ClientName].ToString();
                    // Assign Role
                    objMrfDetail.Role = dr[DbTableColumn.Role].ToString();
                    //Assign Target CTC
                    objMrfDetail.MRFCTCString = dr[DbTableColumn.MRFCTC].ToString();
                    //Assign EmployeeId
                    objMrfDetail.EmployeeId = int.Parse(dr[DbTableColumn.EMPId].ToString());

                    objMrfDetail.FunctionalManager = dr[DbTableColumn.FunctionalManager].ToString();

                    objMrfDetail.SOWNo = dr[DbTableColumn.SOWNo].ToString();


                     if (!string.IsNullOrEmpty(Convert.ToString(dr[DbTableColumn.SOWStartDate])))
                    { 
                        objMrfDetail.SowStartDtString = Convert.ToDateTime(dr[DbTableColumn.StartDate]).ToShortDateString();
                    }
                     else
                    {
                        objMrfDetail.SowStartDtString = "";
                    }

                     if (!string.IsNullOrEmpty(Convert.ToString(dr[DbTableColumn.SOWEndDate])))
                     {                        
                         objMrfDetail.SowEndDtString = Convert.ToDateTime(dr[DbTableColumn.SOWEndDate]).ToShortDateString();
                     }
                     else
                     {
                         objMrfDetail.SowEndDtString = "";
                     }
                    
                   

                    // Add the object to Collection
                    raveHRCollection.Add(objMrfDetail);
                }
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, 
                    CLASS_NAME_MRF, GETAPPROVEREJECTMRF, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDAetApproveRejectMrf.CloseConncetion();
            }

            // return Collection
            return raveHRCollection;
        }

        /// <summary>
        /// Function will Get the Project Domain Name
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetProjectDomainDL(int ProjectID)
        {
            DataAccessClass objDAGetProjectDomainDL = new DataAccessClass();

            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                objDAGetProjectDomainDL.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter(SPParameter.ProjectId, ProjectID);

                objDataReader = objDAGetProjectDomainDL.ExecuteReaderSP(SPNames.MRF_GetProjectDomain, param);

                while (objDataReader.Read())
                {
                    KeyValue<string> keyValue = new KeyValue<string>();
                    keyValue.KeyName = objDataReader[DbTableColumn.ProjectId].ToString();
                    keyValue.Val = objDataReader[DbTableColumn.Domain].ToString();
                    raveHRCollection.Add(keyValue);
                }

                return raveHRCollection;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, 
                    CLASS_NAME_MRF, GETPROJECTDOMAINDL, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDAGetProjectDomainDL.CloseConncetion();
            }
        }

        /// <summary>
        /// Function will Get Employee Detail
        /// </summary>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetMRFEmployeeDL()
        {
            DataAccessClass objDAGetMRFEmployeeDL = new DataAccessClass();
            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                objDAGetMRFEmployeeDL.OpenConnection(DBConstants.GetDBConnectionString());

                objDataReader = objDAGetMRFEmployeeDL.ExecuteReaderSP(SPNames.MRF_GetMRFEmployee);
                while (objDataReader.Read())
                {
                    BusinessEntities.MRFDetail mRFDetail = new BusinessEntities.MRFDetail();

                    mRFDetail.EmployeeId = Convert.ToInt32(objDataReader[DbTableColumn.EMPId]);
                    mRFDetail.EmployeeName = objDataReader[DbTableColumn.EmployeeName].ToString();

                    raveHRCollection.Add(mRFDetail);
                }

                return raveHRCollection;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, 
                    CLASS_NAME_MRF, GETMRFEMPLOYEEDL, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDAGetMRFEmployeeDL.CloseConncetion();
            }
        }

        #region Modified By Mohamed Dangra
        // Mohamed : Issue 50791 : 12/05/2014 : Starts                        			  
        // Desc : IN Mrf Details ,GroupId need to implement
        /// <summary>
        /// Gets the Max GroupID from MRFDetails
        /// </summary>
        /// <returns>Collection</returns>
        public int GetMRFDetailMaxGroupId()
        {
            // Initialise Data Access Class object
            DataAccessClass objDAGetProjectName = new DataAccessClass();
            
            try
            {
                //Open the connection to DB
                objDAGetProjectName.OpenConnection(DBConstants.GetDBConnectionString());
                            
                //Execute the SP
                objDataReader = objDAGetProjectName.ExecuteReaderSP(SPNames.USP_GET_MRFDETAIL_MAX_GROUPID);

                int MaxGroupId = 0;
                //Read the data and assign to Collection object
                while (objDataReader.Read())
                {                    
                    if (objDataReader.GetValue(0) != null)
                    {
                        MaxGroupId = Convert.ToInt32(objDataReader.GetValue(0).ToString());
                    }                    
                }

                // Return the Collection
                return MaxGroupId;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, GETPROJECTNAME, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
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

        // Mohamed : Issue 50791 : 12/05/2014 : Ends
        #endregion Modified By Mohamed Dangra
        /// <summary>
        /// Function will Raise MRF
        /// </summary>
        /// <param name="MRFDetailobject"></param>
        /// <returns></returns>
        public string RaiseMRFDL(BusinessEntities.MRFDetail MRFDetailobject)
        {
            StringBuilder strMrfObject = new StringBuilder();
            string MRFCode;

            DataAccessClass objDARaiseMRFDL = new DataAccessClass();
            try
            {
                objDARaiseMRFDL.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] param = new SqlParameter[37];

                param[0] = new SqlParameter(SPParameter.MRFCode, MRFDetailobject.MRFCode);
                param[1] = new SqlParameter(SPParameter.MRFType, MRFDetailobject.MRfType);
                param[2] = new SqlParameter(SPParameter.ProjectId, MRFDetailobject.ProjectId);
                param[3] = new SqlParameter(SPParameter.ResourceOnboard, MRFDetailobject.ResourceOnBoard);
                
                // Rajan Kumar : Issue 45752 : 03/01/2013 : Starts                        			 
                // Desc : When MRF is raised against department then MRF end date is not mandatory (remove validation).
                //param[4] = new SqlParameter(SPParameter.ResourceReleased, MRFDetailobject.ResourceReleased);
                if (MRFDetailobject.ResourceReleased != new DateTime())
                {
                    param[4] = new SqlParameter(SPParameter.ResourceReleased, MRFDetailobject.ResourceReleased);
                }
                else
                {
                    param[4] = new SqlParameter(SPParameter.ResourceReleased, DBNull.Value);
                }
                // Rajan Kumar : Issue 45752 : 03/01/2013: Ends 
                
                param[5] = new SqlParameter(SPParameter.RoleId, MRFDetailobject.RoleId);
                param[6] = new SqlParameter(SPParameter.SkillCategoryId, MRFDetailobject.SkillCategoryId);
                param[7] = new SqlParameter(SPParameter.MustTohaveSkills, MRFDetailobject.MustToHaveSkills);
                param[8] = new SqlParameter(SPParameter.GoodToHaveSkills, MRFDetailobject.GoodToHaveSkills);
                param[9] = new SqlParameter(SPParameter.SoftSkills, MRFDetailobject.SoftSkills);
                param[10] = new SqlParameter(SPParameter.Tools, MRFDetailobject.Tools);
                param[11] = new SqlParameter(SPParameter.Experience, MRFDetailobject.ExperienceString);
                param[12] = new SqlParameter(SPParameter.Domain, MRFDetailobject.Domain);
                param[13] = new SqlParameter(SPParameter.Qualification, MRFDetailobject.Qualification);
                param[14] = new SqlParameter(SPParameter.Utilization, MRFDetailobject.Utilization);
                param[15] = new SqlParameter(SPParameter.Billing, MRFDetailobject.Billing);
                param[16] = new SqlParameter(SPParameter.ReportingToId, MRFDetailobject.ReportingToId);
                param[17] = new SqlParameter(SPParameter.Remarks, MRFDetailobject.Remarks);
                param[18] = new SqlParameter(SPParameter.MRFCTC, MRFDetailobject.MRFCTCString);
                param[19] = new SqlParameter(SPParameter.DateOfRequisition, MRFDetailobject.DateOfRequisition);
                param[20] = new SqlParameter(SPParameter.DepartmentId, MRFDetailobject.DepartmentId);
                param[21] = new SqlParameter(SPParameter.ResourcePlanId, MRFDetailobject.ResourcePlanId);
                param[22] = new SqlParameter(SPParameter.ResourseResponsibility, MRFDetailobject.ResourceResponsibility);
                param[23] = new SqlParameter(SPParameter.ResourcePlanDurationId, MRFDetailobject.ResourcePlanDurationId);
                param[24] = new SqlParameter(SPParameter.EmailId, MRFDetailobject.LoggedInUserEmail);
                param[25] = new SqlParameter(SPParameter.StatusId, Convert.ToInt32(MasterEnum.MRFStatus.PendingAllocation));

                param[26] = new SqlParameter(SPParameter.OutOf, SqlDbType.VarChar, 250);
                param[26].Direction = ParameterDirection.Output;

                if (MRFDetailobject.MRFId == 0)
                    param[27] = new SqlParameter(SPParameter.MRFId, DBNull.Value);
                else
                    param[27] = new SqlParameter(SPParameter.MRFId, MRFDetailobject.MRFId);

                if (MRFDetailobject.CommentReason == null)
                    param[28] = new SqlParameter(SPParameter.Reason, DBNull.Value);
                else
                    param[28] = new SqlParameter(SPParameter.Reason, MRFDetailobject.CommentReason);
                if (MRFDetailobject.ClientName == "" || MRFDetailobject.ClientName == null)
                {
                    param[29] = new SqlParameter(SPParameter.ClientName, DBNull.Value);
                }
                else
                {
                    param[29] = new SqlParameter(SPParameter.ClientName, MRFDetailobject.ClientName);
                }


                param[30] = new SqlParameter(SPParameter.CurrentMRFId, SqlDbType.Int, 50);
                param[30].Direction = ParameterDirection.Output;

                if (MRFDetailobject.BillingDate != new DateTime())
                {
                    param[31] = new SqlParameter(SPParameter.BillingDate, MRFDetailobject.BillingDate);
                }
                else
                {
                    param[31] = new SqlParameter(SPParameter.BillingDate, DBNull.Value);
                }

                if (MRFDetailobject.SOWStartDate != new DateTime())
                    param[32] = new SqlParameter(SPParameter.SOWStartDate, MRFDetailobject.SOWStartDate);
                else
                    param[32] = new SqlParameter(SPParameter.SOWStartDate, DBNull.Value);

                if (MRFDetailobject.SOWEndDate != new DateTime())
                    param[33] = new SqlParameter(SPParameter.SOWEndDate, MRFDetailobject.SOWEndDate);                    
                else
                    param[33] = new SqlParameter(SPParameter.SOWEndDate, DBNull.Value);

                if (MRFDetailobject.SOWNo == "" || MRFDetailobject.SOWNo == null)
                {
                    param[34] = new SqlParameter(SPParameter.SOWNo, DBNull.Value);
                }
                else
                {
                    param[34] = new SqlParameter(SPParameter.SOWNo, MRFDetailobject.SOWNo);
                }
                #region Modified By Mohamed Dangra
                // Mohamed : Issue 50791 : 12/05/2014 : Starts                        			  
                // Desc : IN Mrf Details ,GroupId need to implement
                param[35] = new SqlParameter(SPParameter.MRF_GroupId, MRFDetailobject.GroupId);
                // Mohamed : Issue 50791 : 12/05/2014 : Ends
                #endregion Modified By Mohamed Dangra

                //Ishwar Patil 22/04/2015 Start
                param[36] = new SqlParameter(SPParameter.MandatorySkillsID, MRFDetailobject.MandatorySkills);
                //Ishwar Patil 22/04/2015 End

                objDARaiseMRFDL.ExecuteNonQuerySP(SPNames.MRF_Raise, param);

                strMrfObject.Append(Convert.ToString(param[26].Value));
                strMrfObject.Append(",");
                strMrfObject.Append(Convert.ToString(param[30].Value));

                MRFCode = strMrfObject.ToString();


            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, 
                    CLASS_NAME_MRF, RAISEMRFDL, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDARaiseMRFDL.CloseConncetion();
            }
            return MRFCode;
        }

        /// <summary>
        /// Function will used in Copy MRF Dropdown FIll
        /// </summary>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetCopyMRFDL(ParameterCriteria paramCreatiria)
        {
            DataAccessClass objDAGetCopyMRFDL = new DataAccessClass();
            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                objDAGetCopyMRFDL.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] param = new SqlParameter[12];

                param[0] = new SqlParameter(SPParameter.EmpId, paramCreatiria.EMailID);
                param[1] = new SqlParameter(SPParameter.Role, paramCreatiria.RoleRPM);
                param[2] = new SqlParameter(SPParameter.PMRole, Convert.ToString(AuthorizationManagerConstants.ROLEPROJECTMANAGER));
                param[3] = new SqlParameter(SPParameter.RPMRole, Convert.ToString(AuthorizationManagerConstants.ROLERPM));
                param[4] = new SqlParameter(SPParameter.CFMRole, Convert.ToString(AuthorizationManagerConstants.ROLECFM));
                param[5] = new SqlParameter(SPParameter.PresalesRole, Convert.ToString(AuthorizationManagerConstants.ROLEPRESALES));
                param[6] = new SqlParameter(SPParameter.RecruitmentRole, Convert.ToString(AuthorizationManagerConstants.ROLERECRUITMENT));
                param[7] = new SqlParameter(SPParameter.HRRole, Convert.ToString(AuthorizationManagerConstants.ROLEHR));
                param[8] = new SqlParameter(SPParameter.TestingRole, Convert.ToString(AuthorizationManagerConstants.ROLETESTING));
                param[9] = new SqlParameter(SPParameter.QualityRole, Convert.ToString(AuthorizationManagerConstants.ROLEQUALITY));
                param[10] = new SqlParameter(SPParameter.MarketingRole, Convert.ToString(AuthorizationManagerConstants.ROLEMH));
                param[11] = new SqlParameter(SPParameter.RaveConsultantRole, Convert.ToString(AuthorizationManagerConstants.ROLERAVECONSULTANT));


                objDataReader = objDAGetCopyMRFDL.ExecuteReaderSP(SPNames.MRF_CopyMRF, param);

                while (objDataReader.Read())
                {
                    KeyValue<string> keyvalue = new KeyValue<string>();

                    keyvalue.KeyName = objDataReader[DbTableColumn.MRFID].ToString();
                    keyvalue.Val = objDataReader[DbTableColumn.MRFCode].ToString();
                    keyvalue.Group = objDataReader[DbTableColumn.DeptName].ToString();

                    raveHRCollection.Add(keyvalue);
                }

                return raveHRCollection;

            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, GETCOPYMRFDL, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDAGetCopyMRFDL.CloseConncetion();
            }
        }

        /// <summary>
        /// Function will used in Copy MRF Functionlity
        /// </summary>
        /// <param name="MRFID"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection CopyMRFDL(int MRFID)
        {
            raveHRCollection = new RaveHRCollection();
            DataAccessClass objDACopyMRFDL = new DataAccessClass();
            try
            {
                objDACopyMRFDL.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] param = new SqlParameter[1];

                param[0] = new SqlParameter(SPParameter.MRFId, MRFID);

                objDataReader = objDACopyMRFDL.ExecuteReaderSP(SPNames.MRF_Copy, param);
                BusinessEntities.MRFDetail mRFDetail = null;
                while (objDataReader.Read())
                {
                    mRFDetail = new BusinessEntities.MRFDetail();
                    mRFDetail.MRFId = MRFID;
                    mRFDetail.MRFCode = objDataReader[DbTableColumn.MRFCode].ToString();
                    mRFDetail.MRfType = Convert.ToInt32(objDataReader[DbTableColumn.MRFType]);
                    mRFDetail.ProjectId = Convert.ToInt32(objDataReader[DbTableColumn.ProjectId]);
                    mRFDetail.ResourceOnBoard = Convert.ToDateTime(objDataReader[DbTableColumn.ResourceOnBoard]);
                    // Rajan Kumar : Issue 45752 : 03/01/2014 : Starts                        			 
                    // Desc : When MRF is raised against department then MRF end date is not mandatory (remove validation).
                    if (objDataReader[DbTableColumn.ReleaseDate] != DBNull.Value)
                    {
                        mRFDetail.ResourceReleased = Convert.ToDateTime(objDataReader[DbTableColumn.ReleaseDate]);
                    }
                    // Rajan Kumar : Issue 45752 : 03/01/2014: Ends 
                    mRFDetail.RoleId = Convert.ToInt32(objDataReader[DbTableColumn.RoleId]);
                    mRFDetail.SkillCategoryId = Convert.ToInt32(objDataReader[DbTableColumn.SkillCategoryId]);
                    mRFDetail.MustToHaveSkills = Convert.ToString(objDataReader[DbTableColumn.MustToHaveSkills]);
                    mRFDetail.GoodToHaveSkills = Convert.ToString(objDataReader[DbTableColumn.GoodToHaveSkills]);
                    mRFDetail.SoftSkills = Convert.ToString(objDataReader[DbTableColumn.SoftSkills]);
                    mRFDetail.Tools = Convert.ToString(objDataReader[DbTableColumn.Tools]);
                    mRFDetail.ExperienceString = Convert.ToString(objDataReader[DbTableColumn.Experience]);
                    mRFDetail.Domain = Convert.ToString(objDataReader[DbTableColumn.Domain]);
                    mRFDetail.Qualification = Convert.ToString(objDataReader[DbTableColumn.Qualification]);
                    mRFDetail.Utilization = Convert.ToInt32(objDataReader[DbTableColumn.Utilization]);
                    mRFDetail.Billing = Convert.ToInt32(objDataReader[DbTableColumn.Billing]);
                    mRFDetail.ReportingToId = Convert.ToString(objDataReader[DbTableColumn.ReportingToId]);
                    mRFDetail.Remarks = Convert.ToString(objDataReader[DbTableColumn.Remarks]);
                    mRFDetail.MRFCTCString = Convert.ToString(objDataReader[DbTableColumn.MRFCTC]);
                    mRFDetail.DateOfRequisition = Convert.ToDateTime(objDataReader[DbTableColumn.DateOfRequisition]);
                    mRFDetail.DepartmentId = Convert.ToInt32(objDataReader[DbTableColumn.DepartmentId]);
                    //mRFDetail.ResourcePlanId = Convert.ToInt32(objDataReader[DbTableColumn.ResourcePlanId]);
                    mRFDetail.ResourceResponsibility = Convert.ToString(objDataReader[DbTableColumn.ResourceResponsibility]);
                    mRFDetail.ProjectName = Convert.ToString(objDataReader[DbTableColumn.ProjectName]);
                    mRFDetail.Role = Convert.ToString(objDataReader[DbTableColumn.Role]);
                    mRFDetail.SkillCategoryName = Convert.ToString(objDataReader[DbTableColumn.SkillCategoryName]);
                    mRFDetail.RPCode = Convert.ToString(objDataReader[DbTableColumn.RPCode]);
                    mRFDetail.StatusId = Convert.ToInt32(objDataReader[DbTableColumn.StatusId]);
                    mRFDetail.CommentReason = Convert.ToString(objDataReader[DbTableColumn.CommentReason]);
                    mRFDetail.DepartmentName = Convert.ToString(objDataReader[DbTableColumn.Department]);
                    mRFDetail.NewMRFCode = Convert.ToString(objDataReader[DbTableColumn.NewMRfCode]);
                    mRFDetail.ProjStartDate = Convert.ToDateTime(objDataReader[DbTableColumn.ProjectStartDate]);
                    mRFDetail.ProjEndDate = Convert.ToDateTime(objDataReader[DbTableColumn.ProjectEndDate]);
                    mRFDetail.ResourcePlanDurationId = Convert.ToInt32(objDataReader[DbTableColumn.ResourcePlanDurationId]);
                    mRFDetail.ClientName = objDataReader[DbTableColumn.ClientName].ToString();
                    mRFDetail.RecruitersId = objDataReader[DbTableColumn.RecruiterId].ToString();
                    mRFDetail.ReasonForExtendingExpectedClosureDate = objDataReader[DbTableColumn.ReasonExtendECLDate].ToString();


                    mRFDetail.ReasonForExtendingExpectedClosureDate = objDataReader[DbTableColumn.ReasonExtendECLDate].ToString();
                    //Rakesh : Actual vs Budget 07/06/2016 Begin
                    mRFDetail.CostCodeId = objDataReader[DbTableColumn.CostCodeId].CastToInt32();
                    //End

                    //mRFDetail.RequestForRecruitment = Convert.ToDateTime(objDataReader[DbTableColumn.RequestforRecruitement]);

                    if ((objDataReader[DbTableColumn.RequestforRecruitement] != null) && (objDataReader[DbTableColumn.RequestforRecruitement] != " ")
                        && (objDataReader[DbTableColumn.RequestforRecruitement] != DBNull.Value))
                    {
                        mRFDetail.RequestForRecruitment = Convert.ToDateTime(objDataReader[DbTableColumn.RequestforRecruitement]);
                    }
                    else
                    {
                        mRFDetail.RequestForRecruitment = DateTime.MinValue;
                    }

                    if (objDataReader[DbTableColumn.ExpectedClosureDate] != DBNull.Value)
                    {
                        mRFDetail.ExpectedClosureDate = objDataReader[DbTableColumn.ExpectedClosureDate].ToString();
                    }
                    else
                    {
                        mRFDetail.ExpectedClosureDate = Convert.ToString(DateTime.MinValue);
                    }

                    if (objDataReader[DbTableColumn.BillingDate] != DBNull.Value)
                    {
                        mRFDetail.BillingDate = Convert.ToDateTime(objDataReader[DbTableColumn.BillingDate]);
                    }
                    else
                    {
                        mRFDetail.BillingDate = DateTime.MinValue;
                    }

                    if (objDataReader[DbTableColumn.MRFColorCode] != DBNull.Value)
                    {
                        mRFDetail.MRFColorCode = objDataReader[DbTableColumn.MRFColorCode].ToString();
                    }
                    if (objDataReader[DbTableColumn.Con_ContractType] != DBNull.Value)
                    {
                        mRFDetail.ContractTypeID = Convert.ToInt32(objDataReader[DbTableColumn.Con_ContractType]);
                    }
                    else
                    {
                        mRFDetail.ContractTypeID = 0;
                    }

                    if (objDataReader[DbTableColumn.ExpectedClosureDate] != DBNull.Value)
                        mRFDetail.ExpectedClosureDate =
                            Convert.ToDateTime(objDataReader[DbTableColumn.ExpectedClosureDate]).ToShortDateString();

                    if (objDataReader[DbTableColumn.FutureEmpID] != DBNull.Value)
                    {
                        //mRFDetail.FutureEmpID = Convert.ToInt32(objDataReader[DbTableColumn.FutureEmpID]);
                        mRFDetail.EmployeeName = GetMRfResponsiblePersonName(Convert.ToInt32(objDataReader[DbTableColumn.FutureEmpID]).ToString());
                        mRFDetail.EmployeeId = Convert.ToInt32(objDataReader[DbTableColumn.FutureEmpID]);

                    }
                    mRFDetail.ReportingToEmployee = GetMRfResponsiblePersonName(mRFDetail.ReportingToId.ToString());

                    mRFDetail.MRFPurposeId = Convert.ToInt32(objDataReader[DbTableColumn.MRFPurposeId] == DBNull.Value ? 0 : Convert.ToInt32(objDataReader[DbTableColumn.MRFPurposeId]));
                    mRFDetail.MRFPurposeDescription = objDataReader[DbTableColumn.MRFPurposeDescription].ToString();
                    mRFDetail.MRFPurpose = objDataReader[DbTableColumn.MRFPurpose] == DBNull.Value ? string.Empty : objDataReader[DbTableColumn.MRFPurpose].ToString();

                    if (objDataReader[DbTableColumn.MRFDemandID] != DBNull.Value)
                    {
                        mRFDetail.MRFDemandID = Convert.ToInt32(objDataReader[DbTableColumn.MRFDemandID]);
                    }

                    if (objDataReader[DbTableColumn.FutureTypeOfAllocationID] != DBNull.Value)
                    {
                        mRFDetail.FutureTypeOfAllocationID = Convert.ToInt32(objDataReader[DbTableColumn.FutureTypeOfAllocationID]);
                    }


                    if (objDataReader[DbTableColumn.FutureTypeofSupplyID] != DBNull.Value)
                    {
                        mRFDetail.FutureTypeOfSupplyID = Convert.ToInt32(objDataReader[DbTableColumn.FutureTypeofSupplyID]);
                    }

                    if (objDataReader[DbTableColumn.FutureAllocationDate] != DBNull.Value)
                    {
                        mRFDetail.FutureAllocationDate =
                               Convert.ToDateTime(objDataReader[DbTableColumn.FutureAllocationDate]).ToShortDateString();
                    }

                    if (objDataReader[DbTableColumn.TypeOfAllocationName] != DBNull.Value)
                    {
                        mRFDetail.TypeOfAllocationName = objDataReader[DbTableColumn.TypeOfAllocationName].ToString();
                    }

                    if (objDataReader[DbTableColumn.TypeofSupplyName] != DBNull.Value)
                    {
                        mRFDetail.TypeOfSupplyName = objDataReader[DbTableColumn.TypeofSupplyName].ToString();
                    }



                    if (objDataReader[DbTableColumn.FutureAllocateResourceName] != DBNull.Value)
                    {
                        mRFDetail.FutureAllocateResourceName =
                            objDataReader[DbTableColumn.FutureAllocateResourceName].ToString();
                    }

                    if (objDataReader[DbTableColumn.FutureEmpID] != DBNull.Value)
                    {
                        mRFDetail.FutureEmpID = Convert.ToInt32(objDataReader[DbTableColumn.FutureEmpID]);
                    }

                    if (mRFDetail.StatusId == Convert.ToInt32(MasterEnum.MRFStatus.Abort) 
                        || mRFDetail.StatusId == Convert.ToInt32(MasterEnum.MRFStatus.Closed))
                    {
                        if (objDataReader[DbTableColumn.LastModifiedDate] != DBNull.Value)
                        {
                            mRFDetail.AbortedOrClosedDate =
                                   Convert.ToDateTime(objDataReader[DbTableColumn.LastModifiedDate]).ToShortDateString();
                        }
                    }
                    if (mRFDetail.StatusId == Convert.ToInt32(MasterEnum.MRFStatus.Closed))
                    {
                        if (objDataReader[DbTableColumn.AllocationDate] != DBNull.Value)
                        {                            
                            mRFDetail.AllocationDate =
                                   Convert.ToDateTime(objDataReader[DbTableColumn.AllocationDate]).ToShortDateString();
                        }
                    }

                    mRFDetail.FunctionalManager = Convert.ToString(objDataReader[DbTableColumn.FunctionalManager]);

                    if (objDataReader[DbTableColumn.SOWNo] != DBNull.Value)
                    {
                        mRFDetail.SOWNo = objDataReader[DbTableColumn.SOWNo].ToString();
                    }


                    if (objDataReader[DbTableColumn.SOWStartDate] != DBNull.Value)
                    {
                        mRFDetail.SOWStartDate = Convert.ToDateTime(objDataReader[DbTableColumn.SOWStartDate]);
                    }

                    if (objDataReader[DbTableColumn.SOWEndDate] != DBNull.Value)
                    {
                        mRFDetail.SOWEndDate = Convert.ToDateTime(objDataReader[DbTableColumn.SOWEndDate]);
                    }

                    //Ishwar Patil 22/04/2015 Start
                    mRFDetail.MandatorySkills = Convert.ToString(objDataReader[DbTableColumn.MandatorySkills]);
                    mRFDetail.MandatorySkillsID = Convert.ToString(objDataReader[DbTableColumn.MandatorySkillsID]);
                    //Ishwar Patil 22/04/2015 End
                    raveHRCollection.Add(mRFDetail);
                }

                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, COPYMRFDL, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDACopyMRFDL.CloseConncetion();
            }


        }

        public string GetMRfResponsiblePersonName(string empid)
        {
            DataAccessClass objDAGetMRfResponsiblePersonName = new DataAccessClass();
            string sname = string.Empty;
            string reportingTo = string.Empty;
            SqlDataReader dataReaderObj;
            try
            {
                //Open the connection to DB
                objDAGetMRfResponsiblePersonName.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter(SPParameter.Resposibility, empid);

                //Execute the SP
                dataReaderObj = objDAGetMRfResponsiblePersonName.ExecuteReaderSP(SPNames.MRF_GetEmployeeName, param);

                //Read the data and assign to Collection object
                while (dataReaderObj.Read())
                {
                    //sname = sname + objDataReader[DbTableColumn.EmployeeName].ToString() + ", ";
                    sname += dataReaderObj[DbTableColumn.EmployeeName].ToString() + ", ";
                }

                if (!string.IsNullOrEmpty(sname))
                    reportingTo = sname.Remove(sname.Length - 2);

                // Return the Collection
                //return sname;
                return reportingTo;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, GETMRFRESPONSIBLEPERSONNAME, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                //here this code is not required bcz this method has been called insode a method.
                //objDA.CloseConncetion();
                //objDAGetMRfResponsiblePersonName.CloseConncetion();
            }


        }

        /// <summary>
        /// Gets the Project Name.
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetRecruitmentManager()
        {
            // Initialise Data Access Class object
            DataAccessClass objDAGetRecruitmentManager = new DataAccessClass();

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                //Open the connection to DB
                objDAGetRecruitmentManager.OpenConnection(DBConstants.GetDBConnectionString());

                //Execute the SP
                objDataReader = objDAGetRecruitmentManager.ExecuteReaderSP(SPNames.MRF_GetRecruitmentManager);

                //Read the data and assign to Collection object
                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    KeyValue<string> keyValue = new KeyValue<string>();

                    keyValue.KeyName = objDataReader.GetValue(0).ToString();
                    keyValue.Val = objDataReader.GetValue(2).ToString();

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
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, GETRECRUITMENTMANAGER, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDAGetRecruitmentManager.CloseConncetion();
            }
        }

        /// <summary>
        /// Get the List of MRF whose status is pending Allocation.
        /// </summary>
        /// <returns>Collection</returns>
        public static BusinessEntities.RaveHRCollection GetMRFDetailsForPendingAllocationWithFilter(BusinessEntities.ParameterCriteria objParameterCriteria, BusinessEntities.MRFDetail mrfDetail, ref int pageCount)
        {
            // Initialise Data Access Class object
            DataAccessClass objDAGetMRFDetailsForPendingAllocationWithFilter = new DataAccessClass();
            try
            {
                // Initialise Collection class object
                raveHRCollection = new BusinessEntities.RaveHRCollection();
                //Open the connection to DB
                objDAGetMRFDetailsForPendingAllocationWithFilter.OpenConnection(DBConstants.GetDBConnectionString());
                //Add Parameter
                objSqlParameter = new SqlParameter[10];//18402-Ambar-Changed from 7 to 8
                objSqlParameter[0] = new SqlParameter(SPParameter.SortExpression, objParameterCriteria.SortExpressionAndDirection);
                objSqlParameter[1] = new SqlParameter(SPParameter.pageNum, objParameterCriteria.PageNumber);
                objSqlParameter[2] = new SqlParameter(SPParameter.pageSize, objParameterCriteria.PageSize);
                objSqlParameter[3] = new SqlParameter(SPParameter.pageCount, SqlDbType.Int);
                objSqlParameter[3].Direction = ParameterDirection.Output;


                objSqlParameter[4] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.Int);
                if (mrfDetail.DepartmentId == CommonConstants.ZERO)
                    objSqlParameter[4].Value = DBNull.Value;
                else
                    objSqlParameter[4].Value = mrfDetail.DepartmentId;

                // Parameter Project Id
                objSqlParameter[5] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int);
                if (mrfDetail.ProjectId == CommonConstants.ZERO)
                    objSqlParameter[5].Value = DBNull.Value;
                else
                    objSqlParameter[5].Value = mrfDetail.ProjectId;

                // Parameter Role Id 
                objSqlParameter[6] = new SqlParameter(SPParameter.RoleId, SqlDbType.Int);
                if (mrfDetail.RoleId == CommonConstants.ZERO)
                    objSqlParameter[6].Value = DBNull.Value;
                else
                    objSqlParameter[6].Value = mrfDetail.RoleId;

                //18402-Ambar-Start
                //Parameter For Status ID
                objSqlParameter[7] = new SqlParameter(SPParameter.StatusId, SqlDbType.Int);
                if (mrfDetail.StatusId == CommonConstants.ZERO)
                    objSqlParameter[7].Value = DBNull.Value;
                else
                    objSqlParameter[7].Value = mrfDetail.StatusId;
                //18402-Ambar-End

                objSqlParameter[8] = new SqlParameter(SPParameter.TypeOfAllocation, SqlDbType.Int);
                if (mrfDetail.TypeOfAllocation == CommonConstants.ZERO)
                    objSqlParameter[8].Value = DBNull.Value;
                else
                    objSqlParameter[8].Value = mrfDetail.TypeOfAllocation;

                objSqlParameter[9] = new SqlParameter(SPParameter.TypeOfSupply, SqlDbType.Int);
                if (mrfDetail.TypeOfSupply == CommonConstants.ZERO)
                    objSqlParameter[9].Value = DBNull.Value;
                else
                    objSqlParameter[9].Value = mrfDetail.TypeOfSupply;




                DataSet ds = objDAGetMRFDetailsForPendingAllocationWithFilter.GetDataSet(SPNames.MRF_GetFilterPendingAllocationMRFDetails, objSqlParameter);
                pageCount = Convert.ToInt32(objSqlParameter[3].Value);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //Initialise the Business Entity object
                    objMrfDetail = new BusinessEntities.MRFDetail();
                    objMrfDetail.MRFId = Convert.ToInt32(dr[DbTableColumn.MRFID]);
                    objMrfDetail.MRFCode = Convert.ToString(dr[DbTableColumn.MRFCode]);
                    objMrfDetail.RPCode = Convert.ToString(dr[DbTableColumn.RPCode]);
                    objMrfDetail.Role = Convert.ToString(dr[DbTableColumn.Role]);
                    objMrfDetail.ProjectName = Convert.ToString(dr[DbTableColumn.ProjectName]);
                    objMrfDetail.ResourceOnBoard = Convert.ToDateTime(dr[DbTableColumn.ResourceOnBoard]);
                    objMrfDetail.EmployeeName = Convert.ToString(dr[DbTableColumn.MRFRaisedBy]);
                    if (!string.IsNullOrEmpty(Convert.ToString(dr[DbTableColumn.EmployeeName])))
                    {
                        objMrfDetail.NewEmployeeName = Convert.ToString(dr[DbTableColumn.EmployeeName]);
                    }
                    if (!string.IsNullOrEmpty(Convert.ToString(dr[DbTableColumn.RecruiterName])))
                    {
                        objMrfDetail.RecruiterName = Convert.ToString(dr[DbTableColumn.RecruiterName]);
                    }
                    objMrfDetail.DepartmentName = Convert.ToString(dr[DbTableColumn.DeptName]);
                    objMrfDetail.Status = Convert.ToString(dr[DbTableColumn.Status]);
                    if (!string.IsNullOrEmpty(Convert.ToString(dr[DbTableColumn.ClientName])))
                    {
                        objMrfDetail.ClientName = Convert.ToString(dr[DbTableColumn.ClientName]);
                    }


                    if (!string.IsNullOrEmpty(Convert.ToString(dr[DbTableColumn.TypeOfAllocation])))
                    {
                        objMrfDetail.TypeOfAllocationName = Convert.ToString(dr[DbTableColumn.TypeOfAllocation]);
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dr[DbTableColumn.TypeofSupply])))
                    {
                        objMrfDetail.TypeOfSupplyName = Convert.ToString(dr[DbTableColumn.TypeofSupply]);
                    }

                    if (!string.IsNullOrEmpty(Convert.ToString(dr[DbTableColumn.FutureAllocateResourceName])))
                    {
                        objMrfDetail.FutureAllocateResourceName = Convert.ToString(dr[DbTableColumn.FutureAllocateResourceName]);
                    }
                    if (dr[DbTableColumn.FutureAllocationDate] != DBNull.Value)
                    {
                        objMrfDetail.FutureAllocationDate = Convert.ToDateTime(dr[DbTableColumn.FutureAllocationDate]).ToShortDateString().ToString();
                    }

                    raveHRCollection.Add(objMrfDetail);
                }
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME_MRF, GETMRFDETAILSFORPENDINGALLOCATION, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAGetMRFDetailsForPendingAllocationWithFilter.CloseConncetion();
            }
        }

        public int EditMRFDL(BusinessEntities.MRFDetail MRFDetailobject)
        {
            int ID;

            DataAccessClass objDAEditMRFDL = new DataAccessClass();
            try
            {
                objDAEditMRFDL.OpenConnection(DBConstants.GetDBConnectionString());
                // venkatesh  : Issue 39795 : 21/11/2013 : Starts
                //Aarohi : Issue 31173 : 06/02/2012 : Start
                //Changed the array number from 31 to 34
                SqlParameter[] param = new SqlParameter[39];//SqlParameter[] param = new SqlParameter[31];
                //Aarohi : Issue 31173 : 06/02/2012 : End
                // venkatesh  : Issue 39795 : 21/11/2013 : End
                param[0] = new SqlParameter(SPParameter.MRFId, MRFDetailobject.MRFId);
                param[1] = new SqlParameter(SPParameter.MRFType, MRFDetailobject.MRfType);
                param[2] = new SqlParameter(SPParameter.SkillCategoryId, MRFDetailobject.SkillCategoryId);
                param[3] = new SqlParameter(SPParameter.MustTohaveSkills, MRFDetailobject.MustToHaveSkills);
                param[4] = new SqlParameter(SPParameter.GoodToHaveSkills, MRFDetailobject.GoodToHaveSkills);
                param[5] = new SqlParameter(SPParameter.SoftSkills, MRFDetailobject.SoftSkills);
                param[6] = new SqlParameter(SPParameter.Tools, MRFDetailobject.Tools);
                param[7] = new SqlParameter(SPParameter.Experience, MRFDetailobject.ExperienceString);
                param[8] = new SqlParameter(SPParameter.Qualification, MRFDetailobject.Qualification);
                param[9] = new SqlParameter(SPParameter.Utilization, MRFDetailobject.Utilization);
                param[10] = new SqlParameter(SPParameter.Billing, MRFDetailobject.Billing);
                param[11] = new SqlParameter(SPParameter.ReportingToId, MRFDetailobject.ReportingToId);
                param[12] = new SqlParameter(SPParameter.Remarks, MRFDetailobject.Remarks);
                param[13] = new SqlParameter(SPParameter.MRFCTC, MRFDetailobject.MRFCTCString);
                param[14] = new SqlParameter(SPParameter.ResourseResponsibility, MRFDetailobject.ResourceResponsibility);
                param[15] = new SqlParameter(SPParameter.EmailId, MRFDetailobject.LoggedInUserEmail);
                param[16] = new SqlParameter(SPParameter.StatusId, MRFDetailobject.StatusId);
                param[17] = new SqlParameter(SPParameter.ClientName, MRFDetailobject.ClientName);
                param[18] = new SqlParameter(SPParameter.MRFColorCode, MRFDetailobject.MRFColorCode);
                param[19] = new SqlParameter(SPParameter.Domain, MRFDetailobject.Domain);
                param[20] = new SqlParameter(SPParameter.RecruiterId, Convert.ToInt32(MRFDetailobject.RecruitersId));
                param[21] = new SqlParameter(SPParameter.OutOf, SqlDbType.Int);
                param[21].Direction = ParameterDirection.Output;
                param[22] = new SqlParameter(SPParameter.MRFPurpose, MRFDetailobject.MRFPurposeId);
                if (!string.IsNullOrEmpty(MRFDetailobject.MRFPurposeDescription))
                    param[23] = new SqlParameter(SPParameter.MRFPurposeDescription, MRFDetailobject.MRFPurposeDescription);
                else
                    param[23] = new SqlParameter(SPParameter.MRFPurposeDescription, DBNull.Value);


                param[22] = new SqlParameter(SPParameter.MRFPurpose, MRFDetailobject.MRFPurposeId);

                if (!string.IsNullOrEmpty(MRFDetailobject.MRFPurposeDescription))
                    param[23] = new SqlParameter(SPParameter.MRFPurposeDescription, MRFDetailobject.MRFPurposeDescription);
                else
                    param[23] = new SqlParameter(SPParameter.MRFPurposeDescription, DBNull.Value);

                param[24] = new SqlParameter(SPParameter.MRFDemandID, MRFDetailobject.MRFDemandID);

                if (MRFDetailobject.BillingDate != DateTime.MinValue)
                {
                    param[25] = new SqlParameter(SPParameter.BillingDate, MRFDetailobject.BillingDate);
                }
                else
                {
                    param[25] = new SqlParameter(SPParameter.BillingDate, DBNull.Value);
                }
                if (MRFDetailobject.ExpectedClosureDate != DateTime.MinValue.ToShortDateString() && MRFDetailobject.ExpectedClosureDate != null)
                {
                    param[26] = new SqlParameter(SPParameter.ExpectedClosureDate, Convert.ToDateTime(MRFDetailobject.ExpectedClosureDate));
                }
                else
                {
                    param[26] = new SqlParameter(SPParameter.ExpectedClosureDate, DBNull.Value);
                }
                if (MRFDetailobject.ReasonForExtendingExpectedClosureDate != null
                    && MRFDetailobject.ReasonForExtendingExpectedClosureDate != string.Empty)
                {
                    param[27] = new SqlParameter(SPParameter.ReasonExtendExpectedClosureDate, MRFDetailobject.ReasonForExtendingExpectedClosureDate);
                }
                else
                {
                    param[27] = new SqlParameter(SPParameter.ReasonExtendExpectedClosureDate, DBNull.Value);
                }



                //vandana

                if (MRFDetailobject.SOWNo == "" || MRFDetailobject.SOWNo == null)
                {
                    param[28] = new SqlParameter(SPParameter.SOWNo, DBNull.Value);
                }
                else
                {
                    param[28] = new SqlParameter(SPParameter.SOWNo, MRFDetailobject.SOWNo);
                }

                if (MRFDetailobject.SOWStartDate != new DateTime())
                    param[29] = new SqlParameter(SPParameter.SOWStartDate, MRFDetailobject.SOWStartDate);
                else
                    param[29] = new SqlParameter(SPParameter.SOWStartDate, DBNull.Value);

                if (MRFDetailobject.SOWEndDate != new DateTime())
                    param[30] = new SqlParameter(SPParameter.SOWEndDate, MRFDetailobject.SOWEndDate);
                else
                    param[30] = new SqlParameter(SPParameter.SOWEndDate, DBNull.Value);

                //Aarohi : Issue 31173 : 06/02/2012 : Start
                if (MRFDetailobject.ResourceOnBoard != new DateTime())
                    param[31] = new SqlParameter(SPParameter.ResourceOnboard, MRFDetailobject.ResourceOnBoard);
                else
                    param[31] = new SqlParameter(SPParameter.ResourceOnboard, DBNull.Value);

                if (MRFDetailobject.ResourceReleased != new DateTime())
                    param[32] = new SqlParameter(SPParameter.ResourceReleased, MRFDetailobject.ResourceReleased);
                else
                    param[32] = new SqlParameter(SPParameter.ResourceReleased, DBNull.Value);

                if (MRFDetailobject.DateOfRequisition != new DateTime())
                    param[33] = new SqlParameter(SPParameter.DateOfRequisition, MRFDetailobject.DateOfRequisition);
                else
                    param[33] = new SqlParameter(SPParameter.DateOfRequisition, DBNull.Value);
                //Aarohi : Issue 31173 : 06/02/2012 : End

                // venkatesh  : Issue 39795 : 24/12/2013 : Starts       
                // Desc : Role Editable
                param[34] = new SqlParameter(SPParameter.RoleId, MRFDetailobject.RoleId);
                param[35] = new SqlParameter(SPParameter.MRFCode , MRFDetailobject.MRFCode );
                // venkatesh  : Issue 39795 : 24/12/2013 : End
                #region Modified By Mohamed Dangra
                // Mohamed : Issue 50791 : 12/05/2014 : Starts                        			  
                // Desc : IN Mrf Details ,GroupId need to implement
                param[36] = new SqlParameter(SPParameter.MRF_GroupId, MRFDetailobject.GroupId);
                // Mohamed : Issue 50791 : 12/05/2014 : Ends

                //Ishwar Patil 23/04/2015 Start
                param[37] = new SqlParameter(SPParameter.MandatorySkillsID, MRFDetailobject.MandatorySkillsID);

                param[38] = new SqlParameter(SPParameter.CostCodeId, MRFDetailobject.CostCodeId);


                //Ishwar Patil 23/04/2015 End
                #endregion Modified By Mohamed Dangra
                objDAEditMRFDL.ExecuteNonQuerySP(SPNames.MRF_Edit, param);

                ID = Convert.ToInt32(param[21].Value);

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME_MRF, EDITMRFDL, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAEditMRFDL.CloseConncetion();
            }

            return ID;
        }


        public string[] MoveMRFDL(BusinessEntities.MRFDetail MRFDetailobject)
        {
            string ID;
            int n = 6;
            string[] strMailDetail = new string[6];


            DataAccessClass objDAEditMRFDL = new DataAccessClass();
            try
            {
                objDAEditMRFDL.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] param = new SqlParameter[54];
                string NewMRFCode = string.Empty;

                if (MRFDetailobject.ProjectId != 0)
                {
                    NewMRFCode = "MRF_" + MRFDetailobject.ProjectName + "_" + MRFDetailobject.Role + "_";
                }
                else
                {
                    NewMRFCode = "MRF_" + MRFDetailobject.DepartmentName + "_" + MRFDetailobject.Role + "_";
                }

                param[0] = new SqlParameter(SPParameter.NewMRFCode, NewMRFCode);
                param[1] = new SqlParameter("@OldMRFCode", MRFDetailobject.MRFCode);
                param[2] = new SqlParameter(SPParameter.MRFType, MRFDetailobject.MRfType);
                param[3] = new SqlParameter(SPParameter.ProjectId, MRFDetailobject.ProjectId);

                //Rakesh : Date Passing Issue 07/07/2016 Begin  

                if (MRFDetailobject.ResourceOnBoard != DateTime.MinValue)
                {
                    param[4] = new SqlParameter(SPParameter.ResourceOnboard, MRFDetailobject.ResourceOnBoard);
                }
                else
                {
                    param[4] = new SqlParameter(SPParameter.ResourceOnboard, DBNull.Value);
                }

                if (MRFDetailobject.ResourceReleased != DateTime.MinValue)
                {
                    param[5] = new SqlParameter(SPParameter.ResourceReleased, MRFDetailobject.ResourceReleased);
                }
                else
                {
                    param[5] = new SqlParameter(SPParameter.ResourceReleased, DBNull.Value);
                }

                //Rakesh : Date Passing Issue 07/07/2016 End  

                param[6] = new SqlParameter(SPParameter.RoleId, MRFDetailobject.RoleId);
                param[7] = new SqlParameter(SPParameter.SkillCategoryId, MRFDetailobject.SkillCategoryId);
                param[8] = new SqlParameter(SPParameter.MustTohaveSkills, MRFDetailobject.MustToHaveSkills);
                param[9] = new SqlParameter(SPParameter.GoodToHaveSkills, MRFDetailobject.GoodToHaveSkills);
                param[10] = new SqlParameter(SPParameter.SoftSkills, MRFDetailobject.SoftSkills);

                param[11] = new SqlParameter(SPParameter.OperatingEnvironment, MRFDetailobject.OperatingEnvironment);
                param[12] = new SqlParameter(SPParameter.BackEndSkills, MRFDetailobject.BackEndSkills);
                param[13] = new SqlParameter(SPParameter.Tools, MRFDetailobject.Tools);
                param[14] = new SqlParameter(SPParameter.Experience, MRFDetailobject.ExperienceString);
                param[15] = new SqlParameter(SPParameter.Domain, MRFDetailobject.Domain);
                param[16] = new SqlParameter(SPParameter.Qualification, MRFDetailobject.Qualification);
                param[17] = new SqlParameter(SPParameter.Utilization, MRFDetailobject.Utilization);
                param[18] = new SqlParameter(SPParameter.Billing, MRFDetailobject.Billing);
                param[19] = new SqlParameter(SPParameter.ReportingToId, MRFDetailobject.ReportingToId);
                param[20] = new SqlParameter(SPParameter.Remarks, MRFDetailobject.Remarks);

                param[21] = new SqlParameter(SPParameter.MRFCTC, MRFDetailobject.MRFCTCString);
                if (MRFDetailobject.DateOfRequisition != DateTime.MinValue)
                {
                    param[22] = new SqlParameter(SPParameter.DateOfRequisition, MRFDetailobject.DateOfRequisition);
                }
                else
                {
                    param[22] = new SqlParameter(SPParameter.DateOfRequisition, DBNull.Value);
                }
                param[23] = new SqlParameter(SPParameter.StatusId, MRFDetailobject.StatusId);
                param[24] = new SqlParameter(SPParameter.EmpId, MRFDetailobject.EmployeeId);
                param[25] = new SqlParameter(SPParameter.CommentReason, MRFDetailobject.CommentReason);
                if (MRFDetailobject.RequestForRecruitment != DateTime.MinValue)
                {
                    param[26] = new SqlParameter(SPParameter.RequestForRecruitment, MRFDetailobject.RequestForRecruitment);
                }
                else
                {
                    param[26] = new SqlParameter(SPParameter.RequestForRecruitment, DBNull.Value);
                }

                param[27] = new SqlParameter(SPParameter.DepartmentId, MRFDetailobject.DepartmentId);
                param[28] = new SqlParameter(SPParameter.ResourcePlanId, MRFDetailobject.ResourcePlanId);
                param[29] = new SqlParameter(SPParameter.ResourseResponsibility, MRFDetailobject.ResourceResponsibility);

                AuthorizationManager objAuMan = new AuthorizationManager();
                string mailFrom = objAuMan.getLoggedInUserEmailId();
                param[30] = new SqlParameter(SPParameter.EmailID, mailFrom);
                param[31] = new SqlParameter(SPParameter.ResourcePlanDurationId, MRFDetailobject.ResourcePlanDurationId);

                param[32] = new SqlParameter(SPParameter.ClientName, MRFDetailobject.ClientName);
                param[33] = new SqlParameter("@OLDMRFRefID", MRFDetailobject.OLDMRFRefID);
                if (MRFDetailobject.RecruitersId != null && Convert.ToInt32(MRFDetailobject.RecruitersId) > 0)
                {
                    param[34] = new SqlParameter(SPParameter.RecruiterId, Convert.ToInt32(MRFDetailobject.RecruitersId));
                }
                else
                {
                    param[34] = new SqlParameter(SPParameter.RecruiterId, DBNull.Value);
                }
                if (MRFDetailobject.ExpectedClosureDate != null)
                {
                    if (Convert.ToDateTime(MRFDetailobject.ExpectedClosureDate) != DateTime.MinValue)
                    {
                        param[35] = new SqlParameter(SPParameter.ExpectedClosureDate, Convert.ToDateTime(MRFDetailobject.ExpectedClosureDate));
                    }
                    else
                    {
                        param[35] = new SqlParameter(SPParameter.ExpectedClosureDate, DBNull.Value);
                    }
                }
                param[36] = new SqlParameter("@Comments_DataMigration", MRFDetailobject.Comments_DataMigration);
                param[37] = new SqlParameter(SPParameter.MRFColorCode, MRFDetailobject.MRFColorCode);

                if (MRFDetailobject.AllocationDate != null && MRFDetailobject.AllocationDate != "")
                {
                    param[38] = new SqlParameter(SPParameter.AllocationDate, Convert.ToDateTime(MRFDetailobject.AllocationDate.ToString()));
                }
                else
                {
                    param[38] = new SqlParameter(SPParameter.AllocationDate, DBNull.Value);
                }

                param[39] = new SqlParameter(SPParameter.MRFPurpose, MRFDetailobject.MRFPurposeId);
                if (!string.IsNullOrEmpty(MRFDetailobject.MRFPurposeDescription))
                    param[40] = new SqlParameter(SPParameter.MRFPurposeDescription, MRFDetailobject.MRFPurposeDescription);
                else
                    param[40] = new SqlParameter(SPParameter.MRFPurposeDescription, DBNull.Value);

                param[41] = new SqlParameter(SPParameter.MRFDemandID, MRFDetailobject.MRFDemandID);


                if (MRFDetailobject.BillingDate != DateTime.MinValue)
                {
                    param[42] = new SqlParameter(SPParameter.BillingDate, MRFDetailobject.BillingDate);
                }
                else
                {
                    param[42] = new SqlParameter(SPParameter.BillingDate, DBNull.Value);
                }
                if (MRFDetailobject.ReasonForExtendingExpectedClosureDate != null
                    && MRFDetailobject.ReasonForExtendingExpectedClosureDate != string.Empty)
                {
                    param[43] = new SqlParameter(SPParameter.ReasonExtendExpectedClosureDate, MRFDetailobject.ReasonForExtendingExpectedClosureDate);
                }
                else
                {
                    param[43] = new SqlParameter(SPParameter.ReasonExtendExpectedClosureDate, DBNull.Value);
                }
                param[44] = new SqlParameter(SPParameter.OutOf, SqlDbType.VarChar, 250);
                param[44].Direction = ParameterDirection.Output;
                param[45] = new SqlParameter(SPParameter.CommentMoveMRF, MRFDetailobject.CommentMoveMRF);

                param[46] = new SqlParameter(SPParameter.ReplacedByMRFId, MRFDetailobject.ReplacedByMRFId);


                param[47] = new SqlParameter(SPParameter.EmailIdOfOldMRFCreator, SqlDbType.VarChar, 100);
                param[47].Direction = ParameterDirection.Output;
                    
                param[48] = new SqlParameter(SPParameter.EmailIdOfOldMrfPM, SqlDbType.VarChar, 100);
                param[48].Direction = ParameterDirection.Output;
                param[49] = new SqlParameter(SPParameter.EmailIdOfOldMrfResponsiblePerson, SqlDbType.VarChar, 100);
                param[49].Direction = ParameterDirection.Output;
                param[50] = new SqlParameter(SPParameter.EmailIdOfNewMrfPM, SqlDbType.VarChar, 100);
                param[50].Direction = ParameterDirection.Output;
                #region Modified By Mohamed Dangra
                // Mohamed : Issue 50791 : 12/05/2014 : Starts                        			  
                // Desc : IN Mrf Details ,GroupId need to implement
                param[51] = new SqlParameter(SPParameter.MRF_GroupId, MRFDetailobject.GroupId);
                // Mohamed : Issue 50791 : 12/05/2014 : Ends
                #endregion Modified By Mohamed Dangra

                //Ishwar Patil 23/04/2015 Start
                param[52] = new SqlParameter(SPParameter.MandatorySkillsID, MRFDetailobject.MandatorySkillsID);
                //Ishwar Patil 23/04/2015 End

                //Rakesh : Actual vs Budget 07/06/2016 Begin  
                param[53] = new SqlParameter(SPParameter.CostCodeId, MRFDetailobject.CostCodeId);
                //End
                objDAEditMRFDL.ExecuteNonQuerySP(SPNames.MRF_Move, param);
                ID = Convert.ToString(param[44].Value);
                strMailDetail[0] = ID;
                string EmailIdOfOldMrfResponsiblePerson, EmailIdOfOldMRFCreator, EmailIdOfOldMrfPM;
                strMailDetail[4] = Convert.ToString(param[50].Value);
                EmailIdOfOldMRFCreator = Convert.ToString(param[47].Value);
                EmailIdOfOldMrfPM = Convert.ToString(param[48].Value);
                EmailIdOfOldMrfResponsiblePerson = Convert.ToString(param[49].Value);
                strMailDetail[5] = "0";
                if (EmailIdOfOldMRFCreator.Trim() == EmailIdOfOldMrfPM.Trim() && EmailIdOfOldMrfPM.Trim() == EmailIdOfOldMrfResponsiblePerson.Trim())
                {
                    strMailDetail[1] = EmailIdOfOldMrfPM;

                }
                else if (EmailIdOfOldMRFCreator.Trim() == EmailIdOfOldMrfPM.Trim() || EmailIdOfOldMRFCreator.Trim() == EmailIdOfOldMrfResponsiblePerson.Trim())
                {
                    strMailDetail[5] = "2";
                    strMailDetail[1] = EmailIdOfOldMrfPM;
                    strMailDetail[2] = EmailIdOfOldMrfResponsiblePerson;
                }
                else if (EmailIdOfOldMrfPM.Trim() == EmailIdOfOldMrfResponsiblePerson.Trim())
                {
                    strMailDetail[5] = "3";
                    strMailDetail[3] = EmailIdOfOldMRFCreator;
                    strMailDetail[1] = EmailIdOfOldMrfPM;
                }
                else
                {
                    strMailDetail[5] = "4";
                    strMailDetail[1] = EmailIdOfOldMrfPM;
                    strMailDetail[2] = EmailIdOfOldMrfResponsiblePerson;
                    strMailDetail[3] = EmailIdOfOldMRFCreator;
                }





            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME_MRF, EDITMRFDL, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAEditMRFDL.CloseConncetion();
            }

            return strMailDetail;
        }

        /// <summary>
        /// Function will Get Roles as per Sellected Resource Plan
        /// </summary>
        /// <param name="ResourcePlanID"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetRoleDepartmentWiseDL(int DepartmentId)
        {
            DataAccessClass objDAGetRoleDepartmentWiseDL = new DataAccessClass();

            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                objDAGetRoleDepartmentWiseDL.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter(SPParameter.DepartmentId, DepartmentId);

                objDataReader = objDAGetRoleDepartmentWiseDL.ExecuteReaderSP(SPNames.MRF_GetRole_DepartmentWise, param);

                while (objDataReader.Read())
                {
                    KeyValue<string> keyValue = new KeyValue<string>();
                    keyValue.KeyName = objDataReader.GetValue(0).ToString();
                    keyValue.Val = objDataReader.GetValue(1).ToString();
                    raveHRCollection.Add(keyValue);
                }

                return raveHRCollection;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME_MRF, GETROLEDEPARTMENTWISEDL, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDAGetRoleDepartmentWiseDL.CloseConncetion();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MRFDetailobject"></param>
        /// <returns></returns>
        public int AbortMRFDL(BusinessEntities.MRFDetail MRFDetailobject)
        {
            int ID;

            DataAccessClass objDAAbortMRFDL = new DataAccessClass();
            try
            {
                objDAAbortMRFDL.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] param = new SqlParameter[5];

                param[0] = new SqlParameter(SPParameter.MRFId, MRFDetailobject.MRFId);
                param[1] = new SqlParameter(SPParameter.Reason, MRFDetailobject.AbortReason);
                param[2] = new SqlParameter(SPParameter.StatusId, MRFDetailobject.StatusId);
                param[3] = new SqlParameter(SPParameter.EmailId, MRFDetailobject.LoggedInUserEmail);

                param[4] = new SqlParameter(SPParameter.OutOf, SqlDbType.Int);
                param[4].Direction = ParameterDirection.Output;

                objDAAbortMRFDL.ExecuteNonQuerySP(SPNames.MRF_Abort, param);

                ID = Convert.ToInt32(param[4].Value);

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME_MRF, ABORTMRFDL, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAAbortMRFDL.CloseConncetion();
            }
            return ID;
        }

        public BusinessEntities.MRFDetail GetMailingDetails(BusinessEntities.MRFDetail mrfDetail)
        {
            // Initialise the Data Access class object
            DataAccessClass objDAGetMailingDetails = new DataAccessClass();

            // Initialise the SQL parameter.
            SqlParameter[] sqlParam = new SqlParameter[2];

            //Initialise object
            BusinessEntities.MRFDetail mailDetail = new BusinessEntities.MRFDetail();

            try
            {
                // Open the DB connection
                objDAGetMailingDetails.OpenConnection(DBConstants.GetDBConnectionString());

                // Parameter SuperRole: check for the role COO/CEO/CFM/FM/RPM
                sqlParam[0] = new SqlParameter(SPParameter.MRFId, mrfDetail.MRFId);
                sqlParam[1] = new SqlParameter(SPParameter.EmpId, mrfDetail.EmployeeId);

                DataSet ds = objDAGetMailingDetails.GetDataSet(SPNames.MRF_GetMailDetailsOnApprovalOfMrf, sqlParam);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    mailDetail.ResourceResponsibility = dr[DbTableColumn.ResourceResponsibility].ToString();
                    mailDetail.ReportingToId = Convert.ToString(dr[DbTableColumn.ReportingToId]);
                    mailDetail.EmployeeName = GetMRfResponsiblePersonName(mailDetail.ReportingToId);
                    mailDetail.CommentReason = dr[DbTableColumn.CommentReason].ToString();
                    mailDetail.MRFCode = dr[DbTableColumn.MRFCode].ToString();
                    mailDetail.Utilization = Convert.ToInt32(dr[DbTableColumn.Utilization]);
                    mailDetail.Billing = Convert.ToInt32(dr[DbTableColumn.Billing]);
                    mailDetail.ProjectId = Convert.ToInt32(dr[DbTableColumn.ProjectId]);
                    // Rajan Kumar : Issue 45752 : 03/01/2014 : Starts                        			 
                    // Desc : When MRF is raised against department then MRF end date is not mandatory (remove validation).
                    if (dr[DbTableColumn.ResourceReleased] != DBNull.Value)
                    {
                        mailDetail.ResourceReleased = Convert.ToDateTime(dr[DbTableColumn.ResourceReleased]);
                    }
                    // Rajan Kumar : Issue 45752 : 03/01/2014: Ends                    
                    mailDetail.ResourceAllocationID = Convert.ToInt32(dr[DbTableColumn.EmpProjectAllocationId]);
                    mailDetail.BillingDate = dr[DbTableColumn.BillingDate].ToString() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(dr[DbTableColumn.BillingDate]);
                    mailDetail.DepartmentId = Convert.ToInt32(dr[DbTableColumn.DepartmentId]);
                    mailDetail.FunctionalManagerId = Convert.ToString(dr[DbTableColumn.FunctionalManagerId]);
                    mailDetail.FunctionalManager = GetMRfResponsiblePersonName(mailDetail.FunctionalManagerId);
                }

                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    mailDetail.EmailId = dr[DbTableColumn.EmailId].ToString();
                }

                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    mailDetail.RaisedBy = dr[DbTableColumn.EmailId].ToString();
                }

            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME_MRF, "GetMailingDetails", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAGetMailingDetails.CloseConncetion();
            }

            return mailDetail;
        }



        //Siddharth 02-March-2015 Start
        public BusinessEntities.MRFDetail GetMailingDetailsOfFunctionalManagerAndReportingTo(BusinessEntities.MRFDetail mrfDetail)
        {
            // Initialise the Data Access class object
            DataAccessClass objDAGetMailingDetails = new DataAccessClass();

            // Initialise the SQL parameter.
            SqlParameter[] sqlParam = new SqlParameter[2];

            //Initialise object
            BusinessEntities.MRFDetail mailDetail = new BusinessEntities.MRFDetail();

            try
            {
                // Open the DB connection
                objDAGetMailingDetails.OpenConnection(DBConstants.GetDBConnectionString());

                // Parameter SuperRole: check for the role COO/CEO/CFM/FM/RPM
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, mrfDetail.EmployeeId);
                sqlParam[1] = new SqlParameter(SPParameter.MRFId, mrfDetail.MRFId);

                DataSet ds = objDAGetMailingDetails.GetDataSet(SPNames.USP_MRF_GetFunctionalMangerAndReportingTo, sqlParam);
                //Siddharth Feb 2015 Start
                foreach (DataRow dr in ds.Tables[0].Rows)
                {//Functional Manager
                    if (mailDetail.FunctionalManagerId == null || string.IsNullOrEmpty(mailDetail.FunctionalManagerId))
                        mailDetail.FunctionalManagerId = dr[0].ToString();
                    else
                        mailDetail.FunctionalManagerId = mailDetail.FunctionalManagerId + "," + dr[0].ToString();
                }

                foreach (DataRow dr in ds.Tables[1].Rows)
                {//Reporting To
                    if (mailDetail.ReportingToId == null || string.IsNullOrEmpty(mailDetail.ReportingToId))
                                         mailDetail.ReportingToId = dr[0].ToString();
                   else
                        mailDetail.ReportingToId = mailDetail.ReportingToId + "," + dr[0].ToString(); 
                 }
                //Siddharth Feb 2015 End
                foreach (DataRow dr in ds.Tables[2].Rows)
                {//Raised By
                    mailDetail.RaisedBy = dr[0].ToString();
                }
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME_MRF, "GetMailingDetails", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAGetMailingDetails.CloseConncetion();
            }

            return mailDetail;
        }

        //Siddharth 02-March-2015 End





        /// <summary>
        /// Get the Split Duration of Resource plan in Onsite/Offshore.
        /// </summary>
        /// <returns>Collection</returns>
        public static BusinessEntities.RaveHRCollection GetRPSplitDurationInOnsite_Offshore(int RPDuId)
        {

            // Initialise Data Access Class object
            DataAccessClass objDAGetList = new DataAccessClass();

            try
            {
                // Initialise Collection class object
                raveHRCollection = new BusinessEntities.RaveHRCollection();

                //Open the connection to DB
                objDAGetList.OpenConnection(DBConstants.GetDBConnectionString());
                //Add Parameter
                SqlParameter[] param = new SqlParameter[1];

                param[0] = new SqlParameter(SPParameter.RPDuId, RPDuId);

                DataSet ds = objDAGetList.GetDataSet(SPNames.MRF_MRF_GetRPSplitDurationInOnsite_Offshore, param);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //Initialise the Business Entity object
                    objMrfDetail = new BusinessEntities.MRFDetail();
                    objMrfDetail.ResourcePlanStartDate = Convert.ToDateTime(dr[DbTableColumn.StartDate]);
                    objMrfDetail.ResourcePlanEndDate = Convert.ToDateTime(dr[DbTableColumn.EndDate]);
                    objMrfDetail.ResourceLocation = Convert.ToString(dr[DbTableColumn.Location]);
                    raveHRCollection.Add(objMrfDetail);
                }
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, GETLISTOFINTERNALRESOURCE, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDAGetList.CloseConncetion();
            }
        }

        /// <summary>
        /// Get the internal resource to allocate internal resource for MRF.
        /// </summary>
        /// <returns>Collection</returns>
        public static BusinessEntities.RaveHRCollection GetMRFInternalResource(BusinessEntities.MRFDetail mrfDetail, string ResourceName, BusinessEntities.ParameterCriteria objParameterCriteria, ref int pageCount)
        {

            // Initialise Data Access Class object
            DataAccessClass objDAGetListOfInternalResource = new DataAccessClass();

            try
            {
                // Initialise Collection class object
                raveHRCollection = new BusinessEntities.RaveHRCollection();
                //Open the connection to DB
                objDAGetListOfInternalResource.OpenConnection(DBConstants.GetDBConnectionString());
                //Add Parameter
                SqlParameter[] param = new SqlParameter[4];

                param[0] = new SqlParameter(SPParameter.MRFId, mrfDetail.MRFId);

                param[1] = new SqlParameter(SPParameter.SortExpression, objParameterCriteria.SortExpressionAndDirection);
                param[2] = new SqlParameter(SPParameter.FirstName, ResourceName);
                param[3] = new SqlParameter(SPParameter.IsActive, Convert.ToInt32(MasterEnum.EmployeeStatus.Active));

                DataSet ds = objDAGetListOfInternalResource.GetDataSet(SPNames.MRF_GetMRFInternalResource, param);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //Initialise the Business Entity object
                    objMrfDetail = new BusinessEntities.MRFDetail();
                    objMrfDetail.EmployeeId = Convert.ToInt32(dr[DbTableColumn.EMPId]);
                    objMrfDetail.EmployeeName = Convert.ToString(dr[DbTableColumn.ResourceName]);
                    objMrfDetail.DepartmentName = Convert.ToString(dr[DbTableColumn.Designation]);
                    objMrfDetail.ResignationDate = dr[DbTableColumn.ResignationDate].ToString() == string.Empty ? string.Empty : Convert.ToDateTime(dr[DbTableColumn.ResignationDate]).ToString("dd/MM/yyyy");
                    objMrfDetail.EmailId = dr[DbTableColumn.EmailId].ToString();
                    objMrfDetail.EmployeeJoiningDate =  Convert.ToDateTime(dr[DbTableColumn.JoiningDate]);
                    raveHRCollection.Add(objMrfDetail);
                }
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, GETLISTOFINTERNALRESOURCE, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDAGetListOfInternalResource.CloseConncetion();
            }
        }

        /// <summary>
        /// get project manager by projectId.
        /// </summary>
        /// <param name="objBEResourcePlan"></param>
        /// <returns></returns>
        public RaveHRCollection GetProjectManagerByProjectId(BusinessEntities.MRFDetail objBEResourcePlan)
        {
            DataAccessClass objDAMrfDetail = null;
            objDAMrfDetail = new DataAccessClass();

            try
            {
                objDAMrfDetail.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.ProjectId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.ProjectId;

                //--Declare variable.
                BusinessEntities.MRFDetail objBEGetProjectManagerByProjectId = null;

                RaveHRCollection objListGetProjectManagerByProjectId = new RaveHRCollection();

                //--Get data for resource plan 
                DataSet dsGetProjectManagerByProjectId = objDAMrfDetail.GetDataSet(SPNames.ResourcePlan_GetProjectManagerByProjectId, sqlParam);

                //create temporary datatable.
                DataTable tempDataTable = ((DataTable)(dsGetProjectManagerByProjectId.Tables[0])).Clone();
                tempDataTable.Columns.Add(DbTableColumn.EmployeeType);

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
                        dataRow[DbTableColumn.EMPId] = row[DbTableColumn.EMPId];
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
                        if (!row[DbTableColumn.EMPId].ToString().Equals(dataRow[DbTableColumn.EMPId].ToString()))
                        {
                            dataRow[DbTableColumn.EmployeeType] = Convert.ToString(dataRow[DbTableColumn.EMPId]) + "," + Convert.ToString(row[DbTableColumn.EMPId]);
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
                    //create new instance for BusinessEntities.ResourcePlan()
                    objBEGetProjectManagerByProjectId = new BusinessEntities.MRFDetail();

                    //assign ProjectId 
                    objBEGetProjectManagerByProjectId.ProjectId = int.Parse(dr[DbTableColumn.ProjectId].ToString());

                    //assign CreatedByFullName 
                    objBEGetProjectManagerByProjectId.EmployeeName = dr[DbTableColumn.ProjectManager].ToString();

                    //assign EmployeeId 
                    objBEGetProjectManagerByProjectId.EmployeeId = int.Parse(dr[DbTableColumn.EMPId].ToString());

                    //assign CreatedByFullName 
                    
                    objBEGetProjectManagerByProjectId.PmID = dr[DbTableColumn.EmployeeType].ToString();

                    //added Mahendra
                    objBEGetProjectManagerByProjectId.EmailId = dr[DbTableColumn.EmailId].ToString();

                    //add to collection object
                    objListGetProjectManagerByProjectId.Add(objBEGetProjectManagerByProjectId);
                }

                //--return the Collection
                return objListGetProjectManagerByProjectId;
            }

            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, "GetProjectManagerByProjectId", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDAMrfDetail.CloseConncetion();
            }
        }

        /// <summary>
        /// Get Reporting to from Mrf For a employee
        /// </summary>
        /// <param name="MRFDetail">MRFDetail</param>
        /// <returns String[]></returns>
        public DataSet GetReportingTOFromMRF(BusinessEntities.MRFDetail mrfDetail)
        {
            // Initialise the Data Access class object
            DataAccessClass objDAGet = new DataAccessClass();

            // Initialise the SQL parameter.
            //Issue Id :34321 Mahendra STRAT
            //commented by mahendra
            //SqlParameter[] sqlParam = new SqlParameter[1];
            SqlParameter[] sqlParam = new SqlParameter[2];
            //Issue Id :34321 Mahendra END
            try
            {
                // Open the DB connection
                objDAGet.OpenConnection(DBConstants.GetDBConnectionString());

                // Parameter passed.                
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, mrfDetail.EmployeeId);
                //Issue Id :34321 Mahendra STRAT
                sqlParam[1] = new SqlParameter(SPParameter.MRFID, mrfDetail.MRFId);
                //Issue Id :34321 Mahendra END

                DataSet dataSet = objDAGet.GetDataSet(SPNames.MRF_GetReportingTOForEmployeeFromMRF, sqlParam);
                //dataSet.Tables[0].Columns.Add(DbTableColumn.EMPId);
                //dataSet.Tables[0].Columns.Add(DbTableColumn.ReportingToId);

                return dataSet;
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME_MRF, "GetReportingTOFromMRF", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAGet.CloseConncetion();
            }
        }

        /// <summary>
        /// Get Reporting to from Employee module For a employee.
        /// </summary>
        /// <param name="objBEResourcePlan"></param>
        /// <returns></returns>
        public DataSet GetReportingTOFromEmployee(BusinessEntities.MRFDetail mrfDetail)
        {
            // Initialise the Data Access class object
            DataAccessClass objDAGet = new DataAccessClass();

            // Initialise the SQL parameter.
            SqlParameter[] sqlParam = new SqlParameter[1];

            try
            {
                // Open the DB connection
                objDAGet.OpenConnection(DBConstants.GetDBConnectionString());

                // Parameter passed.                
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, mrfDetail.EmployeeId);

                DataSet dataSet = objDAGet.GetDataSet(SPNames.MRF_GetReportingTOofEmployee, sqlParam);

                return dataSet;

            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME_MRF, "GetReportingTOFromEmployee", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAGet.CloseConncetion();
            }
        }

        /// <summary>
        /// Get Reporting to from Employee module For a employee.
        /// </summary>
        /// <param name="objBEResourcePlan"></param>
        /// <returns></returns>
        public bool UpdateReportingTOForEmployee(BusinessEntities.MRFDetail mrfDetail, string reportingTo)
        {
            // Initialise the Data Access class object
            DataAccessClass objDAGet = new DataAccessClass();

            // Initialise the SQL parameter.
            SqlParameter[] sqlParam = new SqlParameter[2];

            try
            {
                // Open the DB connection
                objDAGet.OpenConnection(DBConstants.GetDBConnectionString());

                // Parameter passed.                
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, mrfDetail.EmployeeId);
                sqlParam[1] = new SqlParameter(SPParameter.ReportingToId, reportingTo);

                objDAGet.ExecuteNonQuerySP(SPNames.MRF_UpdateReportingTOForEmployeeAsPerMRF, sqlParam);
                return true;

            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME_MRF, "GetReportingTOFromEmployee", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAGet.CloseConncetion();
            }
        }

        /// <summary>
        /// Gets MRF raise access list of departments for user
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetMRFRaiseAccesForDepartmentByEmpId(BusinessEntities.MRFDetail objBEMRFDetail)
        {
            // Initialise Data Access Class object
            DataAccessClass objDAGetProjectName = new DataAccessClass();

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.EmailId, DbType.String);
                sqlParam[0].Value = objBEMRFDetail.EmailId;

                //Open the connection to DB
                objDAGetProjectName.OpenConnection(DBConstants.GetDBConnectionString());

                //Execute the SP
                objDataReader = objDAGetProjectName.ExecuteReaderSP(SPNames.MRF_GetMRFRaiseAccesForDepartmentByEmpId, sqlParam);

                //Read the data and assign to Collection object
                BusinessEntities.MRFDetail objBEMRF = null;
                while (objDataReader.Read())
                {
                    objBEMRF = new BusinessEntities.MRFDetail();
                    objBEMRF.DepartmentId = Convert.ToInt16(objDataReader[DbTableColumn.DepartmentId].ToString());
                    objBEMRF.DepartmentName = objDataReader[DbTableColumn.DeptName].ToString();

                    // Add the object to Collection
                    raveHRCollection.Add(objBEMRF);
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
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, "GetMRFRaiseAccesForDepartmentByEmpId", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
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
        /// Function will used in Copy MRF Dropdown FIll
        /// </summary>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetCopyMRFList(ParameterCriteria paramCreatiria)
        {
            DataAccessClass objDAGetCopyMRFDL = new DataAccessClass();
            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                objDAGetCopyMRFDL.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] param = new SqlParameter[1];

                param[0] = new SqlParameter(SPParameter.EmailId, paramCreatiria.EMailID);

                objDataReader = objDAGetCopyMRFDL.ExecuteReaderSP(SPNames.MRF_CopyMRFList, param);

                while (objDataReader.Read())
                {
                    KeyValue<string> keyvalue = new KeyValue<string>();

                    keyvalue.KeyName = objDataReader[DbTableColumn.MRFID].ToString();
                    keyvalue.Val = objDataReader[DbTableColumn.MRFCode].ToString();
                    keyvalue.Group = objDataReader[DbTableColumn.DeptName].ToString();

                    raveHRCollection.Add(keyvalue);
                }

                return raveHRCollection;

            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, "GetCopyMRFList", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDAGetCopyMRFDL.CloseConncetion();
            }
        }


        /// <summary>
        /// Get email Id and other details of approver of department wise headcount.
        /// </summary>
        /// <returns></returns>
        public List<BusinessEntities.MRFDetail> GetEmailIdForHeadCountApproval()
        {
            DataAccessClass objDA = new DataAccessClass();

            try
            {
                // Open the DB connection
                objDA.OpenConnection(DBConstants.GetDBConnectionString());
                List<BusinessEntities.MRFDetail> listMRFDetail = new List<BusinessEntities.MRFDetail>();

                objDataReader = objDA.ExecuteReaderSP(SPNames.USP_MRF_GetEmailIdForDeptWiseHeadCountApproval);

                while (objDataReader.Read())
                {
                    BusinessEntities.MRFDetail objMRFDetails = new BusinessEntities.MRFDetail();
                    objMRFDetails.DepartmentId = Convert.ToInt32(objDataReader[DbTableColumn.DepartmentId].ToString());
                    objMRFDetails.DepartmentName = objDataReader[DbTableColumn.Department].ToString();
                    objMRFDetails.EmployeeId = Convert.ToInt32(objDataReader[DbTableColumn.EMPId].ToString());
                    objMRFDetails.EmailId = objDataReader[DbTableColumn.EmailId].ToString();


                    listMRFDetail.Add(objMRFDetails);
                }
                return listMRFDetail;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME_MRF, "GetEmailIdForHeadCountApproval", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
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
        /// Set the MRF Status and Reason for all when it's status is changed.
        /// </summary>
        /// <returns>Int</returns>
        public int SetMRFSatusAfterApproval(BusinessEntities.MRFDetail mrfDetail)
        {
            DataAccessClass objDASetMRFStatusReason = new DataAccessClass();
            int UpdatedStatus = 0;
            try
            {

                //Open the connection to DB
                objDASetMRFStatusReason.OpenConnection(DBConstants.GetDBConnectionString());

                //Add Parameter
                // Rajan Kumar : Issue 46252: 10/02/2014 : Starts                        			 
                // Desc : In MRF history need to implemented in all cases in RMS.
                //Pass Email to know who is going to modified the data
                //objSqlParameter = new SqlParameter[4];
                objSqlParameter = new SqlParameter[5];
                // Rajan Kumar : Issue 46252: 10/02/2014 : END
                
                objSqlParameter[0] = new SqlParameter(SPParameter.MRFId, mrfDetail.MRFId);
                objSqlParameter[1] = new SqlParameter(SPParameter.Reason, mrfDetail.CommentReason);
                objSqlParameter[2] = new SqlParameter(SPParameter.IsApproved, mrfDetail.IsApproved);
                objSqlParameter[3] = new SqlParameter(SPParameter.StatusId, 0);
                objSqlParameter[3].Direction = ParameterDirection.Output;
                // Rajan Kumar : Issue 46252: 10/02/2014 : Starts                        			 
                // Desc : In MRF history need to implemented in all cases in RMS.
                //Pass Email to know who is going to modified the data
                objSqlParameter[4] = new SqlParameter(SPParameter.EmailId, mrfDetail.LoggedInUserEmail);
                // Rajan Kumar : Issue 46252: 10/02/2014 : END
                objDASetMRFStatusReason.ExecuteNonQuerySP(SPNames.USP_MRF_SetMRFSatusAfterApproval, objSqlParameter);

                UpdatedStatus = Convert.ToInt32(objSqlParameter[3].Value);

                return UpdatedStatus;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, SETMRFSATUSAFTERAPPROVAL, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDASetMRFStatusReason.CloseConncetion();

            }

        }

        /// <summary>
        /// Method to get SLA days for the recuriter by employee designation
        /// </summary>
        public RaveHRCollection GetSLADaysByMrfId(BusinessEntities.MRFDetail objMrfDetail)
        {
            // Initialise Data Access Class object
            DataAccessClass objDAGetProjectName = new DataAccessClass();

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.MRFId, DbType.String);
                sqlParam[0].Value = objMrfDetail.MRFId;

                //Open the connection to DB
                objDAGetProjectName.OpenConnection(DBConstants.GetDBConnectionString());

                //Execute the SP
                objDataReader = objDAGetProjectName.ExecuteReaderSP(SPNames.USP_MRF_GetSLADaysForRecruiter, sqlParam);

                //Read the data and assign to Collection object
                BusinessEntities.MRFDetail objBEMRF = null;

                //call read method.
                while (objDataReader.Read())
                {
                    objBEMRF = new BusinessEntities.MRFDetail();
                    objBEMRF.SLAId = int.Parse(objDataReader[DbTableColumn.SLAId].ToString());
                    objBEMRF.SLADays = int.Parse(objDataReader[DbTableColumn.SLADays].ToString());

                    // Add the object to Collection
                    raveHRCollection.Add(objBEMRF);
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
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, "GetMRFRaiseAccesForDepartmentByEmpId", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
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
        /// Method To Get Allocation Start And EndDate of Resource.
        /// </summary>
        public BusinessEntities.RaveHRCollection GetAllocationDateDetails(BusinessEntities.MRFDetail objMrfDetail)
        {
            // Initialise Data Access Class object
            DataAccessClass objDAGetAllocationDate = new DataAccessClass();

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.MRFId, DbType.String);
                sqlParam[0].Value = objMrfDetail.MRFId;

                //Open the connection to DB
                objDAGetAllocationDate.OpenConnection(DBConstants.GetDBConnectionString());

                //Execute the SP
                objDataReader = objDAGetAllocationDate.ExecuteReaderSP("USP_Mrf_GetAllocationDateDetails", sqlParam);

                //Read the data and assign to Collection object
                BusinessEntities.MRFDetail objBEMRF = null;

                //call read method.
                while (objDataReader.Read())
                {
                    objBEMRF = new BusinessEntities.MRFDetail();
                    objBEMRF.AllocationDate = (((objDataReader[DbTableColumn.AllocationDate].ToString()) != null) ? (objDataReader[DbTableColumn.AllocationDate].ToString()) : "");
                    // Rajan Kumar : Issue 45752 : 03/01/2014 : Starts                        			 
                    // Desc : When MRF is raised against department then MRF end date is not mandatory (remove validation).
                    if (objDataReader[DbTableColumn.ResourceReleased] != DBNull.Value)
                    {
                        objBEMRF.ResourceReleased = DateTime.Parse((objDataReader[DbTableColumn.ResourceReleased].ToString() != null) ? (objDataReader[DbTableColumn.ResourceReleased].ToString()) : "");
                    }
                    // Rajan Kumar : Issue 45752 : 03/01/2014: Ends  
                    // Add the object to Collection
                    raveHRCollection.Add(objBEMRF);
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
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, "GetAllocationDateDetails", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDAGetAllocationDate.CloseConncetion();
            }
        }

        public BusinessEntities.MRFDetail GetMRFDetails(int mrfId)
        {
            BusinessEntities.MRFDetail objBEMRF = new BusinessEntities.MRFDetail();
            // Initialise Data Access Class object
            DataAccessClass objDAGetProjectName = new DataAccessClass();

            try
            {
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.MRFId, DbType.Int32);
                sqlParam[0].Value = mrfId;

                //Open the connection to DB
                objDAGetProjectName.OpenConnection(DBConstants.GetDBConnectionString());

                //Execute the SP
                objDataReader = objDAGetProjectName.ExecuteReaderSP(SPNames.USP_MRF_GETMRFDETAILS, sqlParam);
                while (objDataReader.Read())
                {
                    objBEMRF.MRFCode = objDataReader[DbTableColumn.MRFCode].ToString();

                    objBEMRF.RaisedBy = objDataReader[DbTableColumn.RaiseBy].ToString();

                    if (objDataReader[DbTableColumn.RecruitmentManager] != null)
                        objBEMRF.RecruitmentManager = objDataReader[DbTableColumn.RecruitmentManager].ToString();

                    if (objDataReader[DbTableColumn.ProjectName] != null)
                        objBEMRF.ProjectName = objDataReader[DbTableColumn.ProjectName].ToString();

                    if (objDataReader[DbTableColumn.DeptName] != null)
                        objBEMRF.DepartmentName = objDataReader[DbTableColumn.DeptName].ToString();

                    if (objDataReader[DbTableColumn.ClientName] != null)
                        objBEMRF.ClientName = objDataReader[DbTableColumn.ClientName].ToString();

                    if (objDataReader[DbTableColumn.CommentReason] != null)
                        objBEMRF.CommentReason = objDataReader[DbTableColumn.CommentReason].ToString();
                    //Issue ID : 41198 Mahendra Start 18 Mar 2013 START
                    if (objDataReader[DbTableColumn.CreatedById] != null)
                        objBEMRF.CreatedById = int.Parse(objDataReader[DbTableColumn.CreatedById].ToString());
                    //Issue ID : 41198 Mahendra Start 18 Mar 2013 END
                    // Mohamed : Issue 41487 : 24/04/2013 : Starts                        			  
                    // Desc : respective PM is not copied in MRF deleted mails if the MRF was not raised by him
                    if (objDataReader[DbTableColumn.ProjectId] != null)
                        objBEMRF.ProjectId = Convert.ToInt32(objDataReader[DbTableColumn.ProjectId].ToString());
                    // Mohamed : Issue 41487 : 24/04/2013 : Ends
                    
                    // Venkatesh : Issue 46381 : 12/Dec/2013 : Starts                    
                    // Desc : Show Role while Deleteing or abot
                    if (objDataReader[DbTableColumn.Role ] != null)
                        objBEMRF.Role =objDataReader[DbTableColumn.Role].ToString();
                    // Venkatesh : Issue 46381 : 12/Dec/2013 : End

                    // Rajan Kumar : Issue 45222: 31/01/2014 : Starts                        			 
                    // Desc : Rashwin is released from the project but the mail is still going to him since he has raised the MRF.
                    if (objDataReader[DbTableColumn.PMExistForProject] != null)
                    {
                        if (objDataReader[DbTableColumn.PMExistForProject].ToString().ToUpper() == "YES")
                        {
                            objBEMRF.PMExistForProject = true;
                        }
                        else
                        {
                            objBEMRF.PMExistForProject = false;
                        }
                    }
                    // Rajan Kumar : Issue 45222: 31/01/2014 : END

                    // Rajan Kumar :Issue 49361: 21/02/2014 : Starts                              
                    // Desc : Mail is addressed to Prerna although she is inactive. 
                    if (objDataReader[DbTableColumn.EmployeeExist] != null)
                    {
                        if (objDataReader[DbTableColumn.EmployeeExist].ToString().ToUpper() == "YES")
                        {
                            objBEMRF.EmployeeExist = true;
                        }
                        else
                        {
                            objBEMRF.EmployeeExist = false;
                        }
                    }
                    // Rajan Kumar :Issue 49361: 21/02/2014 : END 
                    // Venkatesh Patange : 31/3/2016 : Starts                              
                    if (objDataReader[DbTableColumn.ReportingToId] != null)
                        objBEMRF.ReportingToId = objDataReader[DbTableColumn.ReportingToId].ToString();
                    // Venkatesh Patange : 31/3/2016 : End
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, "GetMRFRaiseAccesForDepartmentByEmpId", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDAGetProjectName.CloseConncetion();
            }
            return objBEMRF;

        }

        public string GetCandidateByMRFID(BusinessEntities.MRFDetail mrfDetail)
        {
            BusinessEntities.Employee objBEEmployee = new BusinessEntities.Employee();
            // Initialise Data Access Class object
            DataAccessClass objDAGetEmployeeName = new DataAccessClass();

            try
            {
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.MRFId, DbType.Int32);
                sqlParam[0].Value = mrfDetail.MRFId;

                //Open the connection to DB
                objDAGetEmployeeName.OpenConnection(DBConstants.GetDBConnectionString());

                //Execute the SP
                objDataReader = objDAGetEmployeeName.ExecuteReaderSP(SPNames.USP_MRF_GETCANDIDATEBYMRFID, sqlParam);
                while (objDataReader.Read())
                {
                    if ((objDataReader[DbTableColumn.FirstName] != null) && (objDataReader[DbTableColumn.LastName] != null))
                    {
                        objBEEmployee.FirstName = objDataReader[DbTableColumn.FirstName].ToString();
                        objBEEmployee.LastName = objDataReader[DbTableColumn.LastName].ToString();
                    }

                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, "GetCandidateByMRFID", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDAGetEmployeeName.CloseConncetion();
            }
            return objBEEmployee.FirstName + " " + objBEEmployee.LastName;
        }

        public RaveHRCollection GetEmployeeByMRFID(BusinessEntities.MRFDetail mrfDetail)
        {

            BusinessEntities.Employee objBEEmployee = new BusinessEntities.Employee();
            // Initialise Data Access Class object
            DataAccessClass objDAGetEmployeeName = new DataAccessClass();

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.MRFId, DbType.Int32);
                sqlParam[0].Value = mrfDetail.MRFId;

                //Open the connection to DB
                objDAGetEmployeeName.OpenConnection(DBConstants.GetDBConnectionString());

                //Execute the SP
                objDataReader = objDAGetEmployeeName.ExecuteReaderSP(SPNames.USP_MRF_GETEMPLOYEEBYMRFID, sqlParam);
                while (objDataReader.Read())
                {
                    if ((objDataReader[DbTableColumn.FirstName] != null) && (objDataReader[DbTableColumn.LastName] != null))
                    {
                        objBEEmployee.FirstName = objDataReader[DbTableColumn.FirstName].ToString();
                        objBEEmployee.LastName = objDataReader[DbTableColumn.LastName].ToString();
                        objBEEmployee.EMPId = int.Parse(objDataReader[DbTableColumn.EMPId].ToString());
                        objBEEmployee.Type = int.Parse(objDataReader[DbTableColumn.Type].ToString());
                        
                        //33243-Subhra-03022012 -start     Added the joining date column 
                        objBEEmployee.JoiningDate = Convert.ToDateTime(objDataReader[DbTableColumn.JoiningDate]);
                        //33243-Subhra-03022012-end
                        
                        // Add the object to Collection
                        raveHRCollection.Add(objBEEmployee);
                    }

                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, "GetMRFRaiseAccesForDepartmentByEmpId", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDAGetEmployeeName.CloseConncetion();
            }

            // Return the Collection
            return raveHRCollection;
        }

        /// <summary>
        /// Get the employee type by empid.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public int GetEmployeeTypeId(int employeeId)
        {
            // Initialise Data Access Class object
            DataAccessClass objDAGetEmployeeName = new DataAccessClass();

            try
            {
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.EmployeeID, DbType.Int32);
                sqlParam[0].Value = employeeId;

                //Open the connection to DB
                objDAGetEmployeeName.OpenConnection(DBConstants.GetDBConnectionString());

                object abcType = objDAGetEmployeeName.ExecuteScalarSP("USP_MRF_GetEmployeeTypeIdByEmpId", sqlParam);
                return Convert.ToInt32(abcType);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, "GetEmployeeTypeId", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDAGetEmployeeName.CloseConncetion();
            }

        }

        //Rakesh : Actual vs Budget 22/06/2016 Begin  

        /// <summary>
        /// Get Allocated Cost Code Name type by EmployeeId,ProjectId.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public string GetEmployee_ProjectAllocation(int employeeId,int ProjectId,int CostCodeId)
        {
            // Initialise Data Access Class object
            DataAccessClass objDAGetEmployeeName = new DataAccessClass();

            try
            {
                SqlParameter[] sqlParam = new SqlParameter[3];

                sqlParam[0] = new SqlParameter(SPParameter.EmployeeID, DbType.Int32);
                sqlParam[0].Value = employeeId;

                sqlParam[1] = new SqlParameter(SPParameter.ProjectId, DbType.Int32);
                sqlParam[1].Value = ProjectId;

                sqlParam[2] = new SqlParameter(SPParameter.CostCodeId, DbType.Int32);
                sqlParam[2].Value = CostCodeId;

                //Open the connection to DB
                objDAGetEmployeeName.OpenConnection(DBConstants.GetDBConnectionString());

                object abcType = objDAGetEmployeeName.ExecuteScalarSP("USP_GET_EmployeeAllocation", sqlParam);
                return Convert.ToString(abcType);

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, "GetEmployeeTypeId", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDAGetEmployeeName.CloseConncetion();
            }

        }
        //Rakesh : Actual vs Budget 22/06/2016 End  

        /// <summary>
        /// Get the employee Exixts status for Project 
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public int GetEmployeeExistCheck(BusinessEntities.MRFDetail mrfDetail)
        {
            // Initialise Data Access Class object
            DataAccessClass objDA = new DataAccessClass();

            try
            {
                // 23719-Ambar-Change Parameter array size from 4 to 5
                // 28573-Ambar-Change Parameter array size from 3 to 4
                SqlParameter[] sqlParam = new SqlParameter[5];

                sqlParam[0] = new SqlParameter(SPParameter.EmployeeID, DbType.String);
                sqlParam[0].Value = mrfDetail.EmployeeId;
                sqlParam[1] = new SqlParameter(SPParameter.ProjectId, DbType.String);
                sqlParam[1].Value = mrfDetail.ProjectId;
                sqlParam[2] = new SqlParameter(SPParameter.EmpExist, SqlDbType.Int);
                sqlParam[2].Direction = ParameterDirection.Output;
                // 28573-Ambar-Start
                sqlParam[3] = new SqlParameter(SPParameter.DepartmentId, DbType.String);
                sqlParam[3].Value = mrfDetail.DepartmentId;                
                // 28573-Ambar-End

                // 23719-Ambar-Start
                sqlParam[4] = new SqlParameter(SPParameter.RoleId, DbType.String);
                sqlParam[4].Value = mrfDetail.RoleId;
                // 23719-Ambar-End

                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                object EmpExist = objDA.ExecuteScalarSP(SPNames.USP_EMPLOYEE_EMPLOYEE_EXISTS, sqlParam);
                int str = (int)sqlParam[2].Value;
                return str;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, GET_EMPLOYEE_EXITS, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }

        }

        /// <summary>
        /// Get Department Head by EmailId and DeptId
        /// </summary>
        public RaveHRCollection GetDepartmentHeadByEmaiIdAndDeptId(string loginUserEmailId)
        {
            // Initialise Data Access Class object
            DataAccessClass objDAGetDepartmentHead = new DataAccessClass();

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                SqlParameter[] sqlParam = new SqlParameter[2];

                sqlParam[0] = new SqlParameter(SPParameter.EmailId, DbType.String);
                sqlParam[0].Value = loginUserEmailId;


                sqlParam[1] = new SqlParameter(SPParameter.DeptId, DbType.Int32);
                sqlParam[1].Value = Convert.ToInt32(MasterEnum.Departments.Recruitment);

                //Open the connection to DB
                objDAGetDepartmentHead.OpenConnection(DBConstants.GetDBConnectionString());

                //Execute the SP
                objDataReader = objDAGetDepartmentHead.ExecuteReaderSP(SPNames.USP_MRF_GETDEPARTMENTHEADBYEMAIIDANDDEPTID, sqlParam);

                //Read the data and assign to Collection object
                BusinessEntities.MRFDetail objBEMRF = null;

                //call read method.
                while (objDataReader.Read())
                {
                    objBEMRF = new BusinessEntities.MRFDetail();
                    objBEMRF.EmailId = (objDataReader[DbTableColumn.DeptId].ToString());
                    objBEMRF.IsDeptHead = Convert.ToBoolean(objDataReader[DbTableColumn.IsDeptHead].ToString());

                    // Add the object to Collection
                    raveHRCollection.Add(objBEMRF);
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
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, "GetDepartmentHeadByEmaiIdAndDeptId", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDAGetDepartmentHead.CloseConncetion();
            }
        }


        /// <summary>
        /// Gets the department head of Recruitment.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public string GetEmployeeExistCheck()
        {
            // Initialise Data Access Class object
            DataAccessClass objDAGetRecruitmentHead = new DataAccessClass();

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.DeptId, DbType.Int32);
                sqlParam[0].Value = Convert.ToInt32(MasterEnum.Departments.Recruitment);

                //Open the connection to DB
                objDAGetRecruitmentHead.OpenConnection(DBConstants.GetDBConnectionString());

                //Execute the SP
                objDataReader = objDAGetRecruitmentHead.ExecuteReaderSP(SPNames.USP_MRF_GetDepartmentHeadEmaiIdForRecruitment, sqlParam);

                //Read the data and assign to Collection object
                //BusinessEntities.MRFDetail objBEMRF = null;
                String emailIds;
                emailIds = "";
                //call read method.
                while (objDataReader.Read())
                {

                    emailIds += (objDataReader[DbTableColumn.EmailId].ToString()) + ',';
                    // Add the object to Collection
                }

                emailIds = emailIds.Substring(0, emailIds.Length - 1);
                // Return the Collection
                return emailIds;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, "GetDepartmentHeadByEmaiIdAndDeptId", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDAGetRecruitmentHead.CloseConncetion();
            }

        }


        /// <summary>
        /// Check whether SLA role exist or not.
        /// </summary> 
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public bool CheckSLARoleExist(int roleId)
        {
            // Initialise Data Access Class object
            DataAccessClass objDA = new DataAccessClass();

            try
            {
                SqlParameter[] sqlParam = new SqlParameter[2];

                sqlParam[0] = new SqlParameter(SPParameter.RoleId, DbType.String);
                sqlParam[0].Value = roleId;
                sqlParam[1] = new SqlParameter(SPParameter.IsRoleExist, SqlDbType.Bit);
                sqlParam[1].Direction = ParameterDirection.Output;

                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                object EmpExist = objDA.ExecuteScalarSP(SPNames.USP_MRF_CheckSLARoles, sqlParam);
                bool str = (bool)sqlParam[1].Value;
                return str;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, "GetEmployeeExistCheck", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }

        }


        /// <summary>
        /// Gets the expected closure date.
        /// </summary>
        /// <param name="mrfDetail">The MRF detail.</param>
        /// <returns></returns>
        public DateTime GetExpectedClosureDate(BusinessEntities.MRFDetail mrfDetail)
        {
            DataAccessClass objDASetMRFStatusReason = new DataAccessClass();
            DateTime ExpectedClosureDate = DateTime.MinValue;
            try
            {

                //Open the connection to DB
                objDASetMRFStatusReason.OpenConnection(DBConstants.GetDBConnectionString());

                //Add Parameter
                objSqlParameter = new SqlParameter[2];
                objSqlParameter[0] = new SqlParameter(SPParameter.MRFId, mrfDetail.MRFId);
                objSqlParameter[1] = new SqlParameter(SPParameter.ExpectedClosureDate, DateTime.MinValue);
                objSqlParameter[1].Direction = ParameterDirection.Output;
                objDASetMRFStatusReason.ExecuteNonQuerySP(SPNames.USP_MRF_GetExpectedClosureDate, objSqlParameter);

                if (Convert.ToDateTime(objSqlParameter[1].Value) != null)
                    ExpectedClosureDate = (Convert.ToDateTime(objSqlParameter[1].Value));

                return ExpectedClosureDate;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, "GetExpectedClosureDate", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDASetMRFStatusReason.CloseConncetion();

            }

        }

        //VANDANA start
        public BusinessEntities.RaveHRCollection DeleteDLFutureAllocateEmployee(int MRFID)
        {
            DataAccessClass objDADeleteFutureAllocateEmployeeDL = new DataAccessClass();

            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                objDADeleteFutureAllocateEmployeeDL.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter(SPParameter.MRFId, MRFID);


                objDADeleteFutureAllocateEmployeeDL.ExecuteScalarSP
                    (SPNames.USP_MRF_DELETE_FUTURE_ALLOCATE_EMPLOYEE, param);


                return raveHRCollection;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer,
                    CLASS_NAME_MRF, GETDELETEFUTUREEMPLOYEEDL, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
        }
        //VANDANA end

        //VANDANA start
        public void EditDLFutureAllocateEmployee(int mrftempid,
                                                 string lint_type_of_allocation,
                                                 string lint_type_of_supply,
                                                 DateTime ldt_date_of_Allocation,
                                                 int lstr_employeeeID)
        {
            DataAccessClass objEditDLFutureAllocateEmployee = new DataAccessClass();
            try
            {
                objEditDLFutureAllocateEmployee.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] param = new SqlParameter[5];

                if (mrftempid != 0)
                    param[0] = new SqlParameter(SPParameter.MRFId, mrftempid);
                else
                    param[0] = new SqlParameter(SPParameter.MRFId, DBNull.Value);

                if (lint_type_of_allocation != "0")
                    param[1] = new SqlParameter(SPParameter.FutureTypeOfAllocationID, lint_type_of_allocation);
                else
                    param[1] = new SqlParameter(SPParameter.FutureTypeOfAllocationID, DBNull.Value);

                if (lint_type_of_supply != "0")
                    param[2] = new SqlParameter(SPParameter.FutureTypeOfSupplyID, lint_type_of_supply);
                else
                    param[2] = new SqlParameter(SPParameter.FutureTypeOfSupplyID, DBNull.Value);

                if (ldt_date_of_Allocation != null)
                    param[3] = new SqlParameter(SPParameter.FutureAllocationDate, ldt_date_of_Allocation);
                else
                    param[3] = new SqlParameter(SPParameter.FutureAllocationDate, DBNull.Value);

                if (lstr_employeeeID != 0)
                    param[4] = new SqlParameter(SPParameter.FutureEmpID, lstr_employeeeID);
                else
                    param[4] = new SqlParameter(SPParameter.FutureEmpID, DBNull.Value);


                objEditDLFutureAllocateEmployee.ExecuteNonQuerySP
                    (SPNames.USP_MRF_EDIT_FUTURE_ALLOCATE_EMPLOYEE, param);

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer,
                    CLASS_NAME_MRF, GETEDITEFUTUREEMPLOYEEDL,
                    EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
        }


        /// <summary>
        /// This function will get the Mrf Detail for the provided Mrf Id.
        /// </summary>
        /// <param name="MrfId"></param>
        /// <returns></returns>
        public BusinessEntities.MRFDetail GetMrfDetailForMove(int MRFID)
        {
            DataAccessClass objDACopyMRFDL = new DataAccessClass();
            try
            {
                objDACopyMRFDL.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] param = new SqlParameter[1];

                param[0] = new SqlParameter(SPParameter.MRFId, MRFID);

                objDataReader = objDACopyMRFDL.ExecuteReaderSP(SPNames.USP_MRF_GETMRFDETAILSFORMOVEMRF, param);
                BusinessEntities.MRFDetail mRFDetail = null;

                while (objDataReader.Read())
                {
                    mRFDetail = new BusinessEntities.MRFDetail();
                    mRFDetail.MRFId = MRFID;
                    mRFDetail.MRFCode = objDataReader[DbTableColumn.MRFCode].ToString();
                    mRFDetail.MRfType = Convert.ToInt32(objDataReader[DbTableColumn.MRFType]);
                    if (objDataReader[DbTableColumn.ProjectId] != DBNull.Value)
                    {
                        mRFDetail.ProjectId = Convert.ToInt32(objDataReader[DbTableColumn.ProjectId]);
                    }
                    mRFDetail.ResourceOnBoard = Convert.ToDateTime(objDataReader[DbTableColumn.ResourceOnBoard]);
                    // Rajan Kumar : Issue 45752 : 03/01/2014 : Starts                        			 
                    // Desc : When MRF is raised against department then MRF end date is not mandatory (remove validation).
                    if (objDataReader[DbTableColumn.ReleaseDate] != DBNull.Value)
                    {
                        mRFDetail.ResourceReleased = Convert.ToDateTime(objDataReader[DbTableColumn.ReleaseDate]);
                    }
                    // Rajan Kumar : Issue 45752 : 03/01/2014: Ends 
                  
                    if (objDataReader[DbTableColumn.RoleId] != DBNull.Value)
                    {
                        mRFDetail.RoleId = Convert.ToInt32(objDataReader[DbTableColumn.RoleId]);
                    }
                    if (objDataReader[DbTableColumn.SkillCategoryId] != DBNull.Value)
                    {
                        mRFDetail.SkillCategoryId = Convert.ToInt32(objDataReader[DbTableColumn.SkillCategoryId]);
                    }

                    mRFDetail.MustToHaveSkills = Convert.ToString(objDataReader[DbTableColumn.MustToHaveSkills]);
                    mRFDetail.GoodToHaveSkills = Convert.ToString(objDataReader[DbTableColumn.GoodToHaveSkills]);
                    mRFDetail.SoftSkills = Convert.ToString(objDataReader[DbTableColumn.SoftSkills]);
                    mRFDetail.OperatingEnvironment = Convert.ToString(objDataReader[DbTableColumn.OperatingEnvironment]);
                    mRFDetail.BackEndSkills = Convert.ToString(objDataReader[DbTableColumn.BackEndSkills]);
                    mRFDetail.Tools = Convert.ToString(objDataReader[DbTableColumn.Tools]);
                    mRFDetail.ExperienceString = Convert.ToString(objDataReader[DbTableColumn.Experience]);
                    mRFDetail.Domain = Convert.ToString(objDataReader[DbTableColumn.Domain]);
                    mRFDetail.Qualification = Convert.ToString(objDataReader[DbTableColumn.Qualification]);
                    mRFDetail.Utilization = Convert.ToInt32(objDataReader[DbTableColumn.Utilization]);
                    mRFDetail.Billing = Convert.ToInt32(objDataReader[DbTableColumn.Billing]);
                    if (objDataReader[DbTableColumn.ReportingToId] != DBNull.Value)
                    {
                        mRFDetail.ReportingToId = Convert.ToString(objDataReader[DbTableColumn.ReportingToId]);
                    }
                    mRFDetail.Remarks = Convert.ToString(objDataReader[DbTableColumn.Remarks]);
                    mRFDetail.MRFCTCString = Convert.ToString(objDataReader[DbTableColumn.MRFCTC]);
                    mRFDetail.DateOfRequisition = Convert.ToDateTime(objDataReader[DbTableColumn.DateOfRequisition]);

                    if (objDataReader[DbTableColumn.StatusId] != DBNull.Value)
                    {
                        mRFDetail.StatusId = Convert.ToInt32(objDataReader[DbTableColumn.StatusId]);
                    }

                    if (objDataReader[DbTableColumn.EMPId] != null && objDataReader[DbTableColumn.EMPId] != string.Empty
                        && objDataReader[DbTableColumn.EMPId] != DBNull.Value)
                    {
                        mRFDetail.EmployeeId = Convert.ToInt32(objDataReader[DbTableColumn.EMPId]);
                    }
                    mRFDetail.CommentReason = Convert.ToString(objDataReader[DbTableColumn.CommentReason]);

                    if ((objDataReader[DbTableColumn.RequestforRecruitement] != null) && (objDataReader[DbTableColumn.RequestforRecruitement] != " ")
                       && (objDataReader[DbTableColumn.RequestforRecruitement] != DBNull.Value))
                    {
                        mRFDetail.RequestForRecruitment = Convert.ToDateTime(objDataReader[DbTableColumn.RequestforRecruitement]);
                    }
                    else
                    {
                        mRFDetail.RequestForRecruitment = DateTime.MinValue;
                    }
                    if (objDataReader[DbTableColumn.DepartmentId] != DBNull.Value)
                    {
                        mRFDetail.DepartmentId = Convert.ToInt32(objDataReader[DbTableColumn.DepartmentId]);
                    }
                    mRFDetail.ResourcePlanId = Convert.ToInt32(objDataReader[DbTableColumn.ResourcePlanId]);
                    mRFDetail.ResourceResponsibility = Convert.ToString(objDataReader[DbTableColumn.ResourceResponsibility]);

                    if (objDataReader[DbTableColumn.CreatedById] != DBNull.Value)
                    {
                        mRFDetail.CreatedById = Convert.ToInt32(objDataReader[DbTableColumn.CreatedById]);
                    }
                    mRFDetail.CreatedDate = Convert.ToDateTime(objDataReader[DbTableColumn.CreatedByDate]);


                    if (objDataReader[DbTableColumn.LastModifiedDate] != DBNull.Value)
                    {
                        mRFDetail.LastModifiedDate = Convert.ToDateTime(objDataReader[DbTableColumn.LastModifiedDate]);
                    }
                    else
                    {
                        mRFDetail.LastModifiedDate = DateTime.MinValue;
                    }
                    if (objDataReader[DbTableColumn.LastModifiedById] != DBNull.Value)
                    {
                        mRFDetail.LastModifiedById = Convert.ToInt32(objDataReader[DbTableColumn.LastModifiedById]);
                    }
                    if (objDataReader[DbTableColumn.ResourcePlanDurationId] != DBNull.Value)
                    {
                        mRFDetail.ResourcePlanDurationId = Convert.ToInt32(objDataReader[DbTableColumn.ResourcePlanDurationId]);
                    }
                    if (objDataReader[DbTableColumn.ReplacedByMRFId] != DBNull.Value)
                    {
                        mRFDetail.ReplacedByMRFId = Convert.ToInt32(objDataReader[DbTableColumn.ReplacedByMRFId]);
                    }
                    mRFDetail.ProjectName = Convert.ToString(objDataReader[DbTableColumn.ProjectName]);
                    mRFDetail.Role = Convert.ToString(objDataReader[DbTableColumn.Role]);
                    mRFDetail.SkillCategoryName = Convert.ToString(objDataReader[DbTableColumn.SkillCategoryName]);
                    mRFDetail.RPCode = Convert.ToString(objDataReader[DbTableColumn.RPCode]);
                    mRFDetail.CommentReason = Convert.ToString(objDataReader[DbTableColumn.CommentReason]);
                    mRFDetail.DepartmentName = Convert.ToString(objDataReader[DbTableColumn.Department]);
                    mRFDetail.NewMRFCode = Convert.ToString(objDataReader[DbTableColumn.NewMRfCode]);
                    mRFDetail.ProjStartDate = Convert.ToDateTime(objDataReader[DbTableColumn.ProjectStartDate]);
                    mRFDetail.ProjEndDate = Convert.ToDateTime(objDataReader[DbTableColumn.ProjectEndDate]);

                    mRFDetail.ClientName = objDataReader[DbTableColumn.ClientName].ToString();
                    if (objDataReader[DbTableColumn.RecruiterId] != DBNull.Value)
                    {
                        if (Convert.ToInt32(objDataReader[DbTableColumn.RecruiterId].ToString()) > 0)
                        {
                            mRFDetail.RecruitersId = objDataReader[DbTableColumn.RecruiterId].ToString();
                        }
                        else
                        {
                            mRFDetail.RecruitersId = null;
                        }
                    }

                    if (objDataReader[DbTableColumn.ExpectedClosureDate] != DBNull.Value)
                    {
                        mRFDetail.ExpectedClosureDate =
                             Convert.ToString(objDataReader[DbTableColumn.ExpectedClosureDate]);
                    }
                    else
                    {
                        mRFDetail.ExpectedClosureDate = Convert.ToString(DateTime.MinValue);
                    }

                    mRFDetail.Comments_DataMigration = objDataReader[DbTableColumn.Comments_DataMigration].ToString();

                    if (objDataReader[DbTableColumn.MRFColorCode] != DBNull.Value)
                    {
                        mRFDetail.MRFColorCode = objDataReader[DbTableColumn.MRFColorCode].ToString();
                    }

                    mRFDetail.AllocationDate = objDataReader[DbTableColumn.AllocationDate].ToString();
                    mRFDetail.AllocationDate = objDataReader[DbTableColumn.AllocationDate].ToString();

                    mRFDetail.MRFPurposeId = Convert.ToInt32(objDataReader[DbTableColumn.MRFPurposeId] == DBNull.Value ? 0 : Convert.ToInt32(objDataReader[DbTableColumn.MRFPurposeId]));
                    mRFDetail.MRFPurposeDescription = objDataReader[DbTableColumn.MRFPurposeDescription].ToString();
                    mRFDetail.MRFPurpose = objDataReader[DbTableColumn.MRFPurpose] == DBNull.Value ? string.Empty : objDataReader[DbTableColumn.MRFPurpose].ToString();

                    if (objDataReader[DbTableColumn.MRFDemandID] != DBNull.Value)
                    {
                        mRFDetail.MRFDemandID = Convert.ToInt32(objDataReader[DbTableColumn.MRFDemandID]);
                    }
                    if (objDataReader[DbTableColumn.BillingDate] != DBNull.Value)
                    {
                        mRFDetail.BillingDate = Convert.ToDateTime(objDataReader[DbTableColumn.BillingDate]);
                    }
                    else
                    {
                        mRFDetail.BillingDate = DateTime.MinValue;
                    }

                    mRFDetail.ReasonForExtendingExpectedClosureDate = objDataReader[DbTableColumn.ReasonExtendECLDate].ToString();

                    if (objDataReader[DbTableColumn.Con_ContractType] != DBNull.Value)
                    {
                        mRFDetail.ContractTypeID = Convert.ToInt32(objDataReader[DbTableColumn.Con_ContractType]);
                    }
                    else
                    {
                        mRFDetail.ContractTypeID = 0;
                    }

                    mRFDetail.EmployeeName = GetMRfResponsiblePersonName(mRFDetail.EmployeeId.ToString());

                    if (objDataReader[DbTableColumn.FutureTypeOfAllocationID] != DBNull.Value)
                    {
                        mRFDetail.FutureTypeOfAllocationID = Convert.ToInt32(objDataReader[DbTableColumn.FutureTypeOfAllocationID]);
                    }

                    if (objDataReader[DbTableColumn.FutureTypeofSupplyID] != DBNull.Value)
                    {
                        mRFDetail.FutureTypeOfSupplyID = Convert.ToInt32(objDataReader[DbTableColumn.FutureTypeofSupplyID]);
                    }

                    if (objDataReader[DbTableColumn.FutureAllocationDate] != DBNull.Value)
                    {
                        mRFDetail.FutureAllocationDate =
                               Convert.ToString(objDataReader[DbTableColumn.FutureAllocationDate]);
                    }

                    if (objDataReader[DbTableColumn.TypeOfAllocationName] != DBNull.Value)
                    {
                        mRFDetail.TypeOfAllocationName = objDataReader[DbTableColumn.TypeOfAllocationName].ToString();
                    }

                    if (objDataReader[DbTableColumn.TypeofSupplyName] != DBNull.Value)
                    {
                        mRFDetail.TypeOfSupplyName = objDataReader[DbTableColumn.TypeofSupplyName].ToString();
                    }

                    if (objDataReader[DbTableColumn.FutureAllocateResourceName] != DBNull.Value)
                    {
                        mRFDetail.FutureAllocateResourceName =
                            objDataReader[DbTableColumn.FutureAllocateResourceName].ToString();
                    }

                    if (objDataReader[DbTableColumn.FutureEmpID] != DBNull.Value)
                    {
                        mRFDetail.FutureEmpID = Convert.ToInt32(objDataReader[DbTableColumn.FutureEmpID]);
                    }
                    //Rakesh : Actual vs Budget 07/06/2016 Begin  
                    if (objDataReader[DbTableColumn.CostCodeId] != DBNull.Value)
                    {
                        mRFDetail.CostCodeId = Convert.ToInt32(objDataReader[DbTableColumn.CostCodeId]);
                    }
                    //End

                    //Ishwar Patil 23/04/2015 Start
                    mRFDetail.MandatorySkillsID = Convert.ToString(objDataReader[DbTableColumn.MandatorySkillsID]);
                    mRFDetail.MandatorySkills = Convert.ToString(objDataReader[DbTableColumn.MandatorySkills]);
                    //Ishwar Patil 23/04/2015 End
                }
                return mRFDetail;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, "GetMrfDetailForMove", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDACopyMRFDL.CloseConncetion();
            }
        }



        public BusinessEntities.RaveHRCollection FillDepartmentProjectNamesDropDownDL(ParameterCriteria paramCreatiria)
        {
            // Initialise Data Access Class object
            DataAccessClass objDAGetProjectDeptName = new DataAccessClass();

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                //Open the connection to DB
                objDAGetProjectDeptName.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] param = new SqlParameter[13];////25988-Ambar
                param[0] = new SqlParameter(SPParameter.EmpId, paramCreatiria.EMailID);
                param[1] = new SqlParameter(SPParameter.Role, paramCreatiria.RoleRPM);
                param[2] = new SqlParameter(SPParameter.PMRole, Convert.ToString(AuthorizationManagerConstants.ROLEPROJECTMANAGER));
                param[3] = new SqlParameter(SPParameter.RPMRole, Convert.ToString(AuthorizationManagerConstants.ROLERPM));
                param[4] = new SqlParameter(SPParameter.CFMRole, Convert.ToString(AuthorizationManagerConstants.ROLECFM));
                param[5] = new SqlParameter(SPParameter.PresalesRole, Convert.ToString(AuthorizationManagerConstants.ROLEPRESALES));
                param[6] = new SqlParameter(SPParameter.RecruitmentRole, Convert.ToString(AuthorizationManagerConstants.ROLERECRUITMENT));
                param[7] = new SqlParameter(SPParameter.HRRole, Convert.ToString(AuthorizationManagerConstants.ROLEHR));
                param[8] = new SqlParameter(SPParameter.TestingRole, Convert.ToString(AuthorizationManagerConstants.ROLETESTING));
                param[9] = new SqlParameter(SPParameter.QualityRole, Convert.ToString(AuthorizationManagerConstants.ROLEQUALITY));
                param[10] = new SqlParameter(SPParameter.MarketingRole, Convert.ToString(AuthorizationManagerConstants.ROLEMH));
                param[11] = new SqlParameter(SPParameter.RaveConsultantRole, Convert.ToString(AuthorizationManagerConstants.ROLERAVECONSULTANT));
                ////25988-Ambar-Start
                if (paramCreatiria.ProjectStatus == null)
                  param[12] = new SqlParameter(SPParameter.Status, DBNull.Value);
                else
                  param[12] = new SqlParameter(SPParameter.Status, paramCreatiria.ProjectStatus);
                ////25988-Ambar-End
                
                //Execute the SP
                // objDataReader = objDAGetProjectDeptName.ExecuteReaderSP(SPNames.Master_GetDept_ProjectNames);
                objDataReader = objDAGetProjectDeptName.ExecuteReaderSP(SPNames.MRF_CopyProjectDeptName, param);

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
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, 
                    CLASS_NAME_MRF, GETPROJECTNAME, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDAGetProjectDeptName.CloseConncetion();
            }
        }

        public BusinessEntities.RaveHRCollection FillMRFCodeDL(string DeptProjectName, int DeptProjectMRFRoleID, ParameterCriteria paramCreatiria)
        {
            // Initialise Data Access Class object
            DataAccessClass objDAGetProjectDeptMRFCode = new DataAccessClass();

            // Initialise Collection class object
            //raveHRCollection = new BusinessEntities.RaveHRCollection();
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
            try
            {
                //Open the connection to DB
                objDAGetProjectDeptMRFCode.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] param = new SqlParameter[15];////25988-Ambar

                if(DeptProjectName == string.Empty || DeptProjectName == null)
                    param[0] = new SqlParameter(SPParameter.DeptProjectName, DBNull.Value);
                else
                    param[0] = new SqlParameter(SPParameter.DeptProjectName, DeptProjectName);
                
                param[1] = new SqlParameter(SPParameter.DeptProjectMRFRoleID, DeptProjectMRFRoleID);
                param[2] = new SqlParameter(SPParameter.EmpId, paramCreatiria.EMailID);
                param[3] = new SqlParameter(SPParameter.Role, paramCreatiria.RoleRPM);
                param[4] = new SqlParameter(SPParameter.PMRole, Convert.ToString(AuthorizationManagerConstants.ROLEPROJECTMANAGER));
                param[5] = new SqlParameter(SPParameter.RPMRole, Convert.ToString(AuthorizationManagerConstants.ROLERPM));
                param[6] = new SqlParameter(SPParameter.CFMRole, Convert.ToString(AuthorizationManagerConstants.ROLECFM));
                param[7] = new SqlParameter(SPParameter.PresalesRole, Convert.ToString(AuthorizationManagerConstants.ROLEPRESALES));
                param[8] = new SqlParameter(SPParameter.RecruitmentRole, Convert.ToString(AuthorizationManagerConstants.ROLERECRUITMENT));
                param[9] = new SqlParameter(SPParameter.HRRole, Convert.ToString(AuthorizationManagerConstants.ROLEHR));
                param[10] = new SqlParameter(SPParameter.TestingRole, Convert.ToString(AuthorizationManagerConstants.ROLETESTING));
                param[11] = new SqlParameter(SPParameter.QualityRole, Convert.ToString(AuthorizationManagerConstants.ROLEQUALITY));
                param[12] = new SqlParameter(SPParameter.MarketingRole, Convert.ToString(AuthorizationManagerConstants.ROLEMH));
                param[13] = new SqlParameter(SPParameter.RaveConsultantRole, Convert.ToString(AuthorizationManagerConstants.ROLERAVECONSULTANT));
                ////25988-Ambar-Start
                if ( paramCreatiria.ProjectStatus == null)
                  param[14] = new SqlParameter(SPParameter.Status, DBNull.Value);
                else
                  param[14] = new SqlParameter(SPParameter.Status, paramCreatiria.ProjectStatus);
                ////25988-Ambar-End
                
                //Execute the SP
                objDataReader = objDAGetProjectDeptMRFCode.ExecuteReaderSP(SPNames.MRF_CopyMRFnew, param);

                //Read the data and assign to Collection object
                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    KeyValue<string> keyValue = new KeyValue<string>();

                    keyValue.KeyName = objDataReader[DbTableColumn.ProjectDeptMRFID].ToString();
                    keyValue.Val = objDataReader[DbTableColumn.MRFCode].ToString();
                   
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
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer,
                    CLASS_NAME_MRF, GETPROJECTNAME, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDAGetProjectDeptMRFCode.CloseConncetion();
            }
        }

        public BusinessEntities.RaveHRCollection FillMRFRoleDL(string DeptProjectName)
        {
            // Initialise Data Access Class object
            DataAccessClass objDAGetProjectDeptMRFRole = new DataAccessClass();

            // Initialise Collection class object
            //raveHRCollection = new BusinessEntities.RaveHRCollection();
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
            try
            {
                //Open the connection to DB
                objDAGetProjectDeptMRFRole.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter(SPParameter.DeptProjectName, DeptProjectName);

                //Execute the SP
                objDataReader = objDAGetProjectDeptMRFRole.ExecuteReaderSP(SPNames.MRF_SearchProjectDeptWiseMRF_MRFRole, param);

                //Read the data and assign to Collection object
                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    KeyValue<string> keyValue = new KeyValue<string>();

                    keyValue.KeyName = objDataReader[DbTableColumn.ProjectDeptMRFRoleID].ToString();
                    keyValue.Val = objDataReader[DbTableColumn.ProjectDeptMRFRole].ToString();
                   

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
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer,
                    CLASS_NAME_MRF, GETPROJECTNAME, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDAGetProjectDeptMRFRole.CloseConncetion();
            }
        }

        #region Coded By Anuj
        //Issue ID:-20374
        //START
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MRFDetailobject"></param>
        /// <returns></returns>
        public void DeleteMRFDL(BusinessEntities.MRFDetail MRFDetaildeleteobj)
        {
            bool roleAndMrfBothArePresent;
            DataAccessClass objDADeleteMRFDL = new DataAccessClass();
            try
            {
                objDADeleteMRFDL.OpenConnection(DBConstants.GetDBConnectionString());

                //27632-Ambar-Change Param Array Size
                SqlParameter[] param = new SqlParameter[7];
                if (MRFDetaildeleteobj.ResourcePlanDurationId == 0 && MRFDetaildeleteobj.ResourcePlanId == 0)
                {
                    roleAndMrfBothArePresent = false;
                }
                else
                {
                    roleAndMrfBothArePresent = true;
                }
                param[0] = new SqlParameter(SPParameter.MRFId, MRFDetaildeleteobj.MRFId);
                param[1] = new SqlParameter(SPParameter.Role, MRFDetaildeleteobj.Role);
                param[2] = new SqlParameter(SPParameter.RPDuId, MRFDetaildeleteobj.ResourcePlanDurationId);
                param[3] = new SqlParameter(SPParameter.ResourcePlanId, MRFDetaildeleteobj.ResourcePlanId);
                param[4] = new SqlParameter(SPParameter.Role_mrf_delete, roleAndMrfBothArePresent);
                //27632-Ambar-Start-Added Extra Parameter
                if (MRFDetaildeleteobj.AbortReason != null)
                  param[5] = new SqlParameter(SPParameter.Reason, MRFDetaildeleteobj.AbortReason);
                else
                  param[5] = new SqlParameter(SPParameter.Reason, DBNull.Value);
                
                if (MRFDetaildeleteobj.LoggedInUserEmail != null)
                  param[6] = new SqlParameter(SPParameter.EmailId, MRFDetaildeleteobj.LoggedInUserEmail);
                else
                  param[6] = new SqlParameter(SPParameter.EmailId, DBNull.Value);
                //27632-Ambar-Start
                objDADeleteMRFDL.ExecuteNonQuerySP(SPNames.MRF_Delete, param);

                
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME_MRF, DELETEMRFDL, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDADeleteMRFDL.CloseConncetion();
            }

        }
        //END
        #endregion Coded By Anuj

        #endregion

        //Aarohi : Issue 31838(CR) : 28/12/2011 : Start
        /// <summary>
        /// Get the MRFId by MRFCode.
        /// </summary>
        /// <param name="MRFCode"></param>
        /// <returns></returns>
        public int GetMRFId(string MRFCode)
        {
            // Initialise Data Access Class object
            DataAccessClass objDAGetMRFId = new DataAccessClass();

            try
            {
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.MRFCode, DbType.VarNumeric);
                sqlParam[0].Value = MRFCode;

                //Open the connection to DB
                objDAGetMRFId.OpenConnection(DBConstants.GetDBConnectionString());

                object abcType = objDAGetMRFId.ExecuteScalarSP(SPNames.GET_MRFID_BY_MRFCODE, sqlParam);
                return Convert.ToInt32(abcType);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, "GetMRFId", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDAGetMRFId.CloseConncetion();
            }

        }
        //Aarohi : Issue 31838(CR) : 28/12/2011 : End

        //Mahendra IssueId 33960 START

        public List<int> GetListOfMRFIdFROMRP(int intResourcePlanId)
        {
            List<int> lstMRFId = new List<int> { };
            DataAccessClass objDAGetMRFId = new DataAccessClass();

            try
            {
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.ResourcePlanId, DbType.VarNumeric);
                sqlParam[0].Value = intResourcePlanId;

                //Open the connection to DB
                objDAGetMRFId.OpenConnection(DBConstants.GetDBConnectionString());

                objDataReader = objDAGetMRFId.ExecuteReaderSP(SPNames.GET_MRFID_BY_RPCODE, sqlParam);

                while (objDataReader.Read())
                {
                    if (objDataReader[DbTableColumn.ProjectDeptMRFID] != null)
                        lstMRFId.Add(Convert.ToInt32(objDataReader[DbTableColumn.ProjectDeptMRFID].ToString()));
                }
                return lstMRFId;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, "GetListOfMRFIdFROMRP", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDAGetMRFId.CloseConncetion();
            }
        }

      //Mahendra IssueId 33960 END


        // Venkatesh : Issue48132 : 03/Jan/2014 : Starts                    
        // Desc : Check New Emp Allocation 
        /// <summary>
        /// Get Reporting to from Employee module For a employee.
        /// </summary>
        /// <param name="objBEResourcePlan"></param>
        /// <returns></returns>
        public bool CheckNewEmployee_Allocation(BusinessEntities.MRFDetail mrfDetail)
        {
            // Initialise the Data Access class object
            DataAccessClass objDAGet = new DataAccessClass();
            bool IsNewEmployee = false;
            // Initialise the SQL parameter.
            SqlParameter[] sqlParam = new SqlParameter[2];

            try
            {
                // Open the DB connection
                objDAGet.OpenConnection(DBConstants.GetDBConnectionString());

                // Parameter passed.                
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, mrfDetail.EmployeeId);
                sqlParam[1] = new SqlParameter(SPParameter.MRFID, mrfDetail.MRFId);

                DataSet dataSet = objDAGet.GetDataSet(SPNames.USP_MRF_CheckNewEmployee_Allocation, sqlParam);
                if(dataSet.Tables[0].Rows.Count >0)
                    IsNewEmployee = false;
                else
                    IsNewEmployee = true ;

                return IsNewEmployee;

            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME_MRF, "CheckNewEmployee_Allocation", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAGet.CloseConncetion();
            }
        }

        // Ishwar : NISRMS : 16022015 Start
        public DataSet GetMRFAging(string SortExpressionAndDirection)
        {
            DataAccessClass objDA = new DataAccessClass();
            DataSet MRFAging = null;
            SqlParameter[] sqlParam = new SqlParameter[1];
            try
            {
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Siddharth 26 March 2015 Start
                // Parameter Sort Expression And Direction
                sqlParam[0] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                if (SortExpressionAndDirection == null)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = SortExpressionAndDirection;
                //Siddharth 26 March 2015 End

                MRFAging = objDA.GetDataSet(SPNames.USP_MRF_AgingReport, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, "GetMRFAging", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
            return MRFAging;
        }

        public DataSet GetMRFAgingForOpenPosition(string SortExpressionAndDirection)
        {
            DataAccessClass objDA = new DataAccessClass();
            DataSet MRFAgingForOpenPosition = null;
            SqlParameter[] sqlParam = new SqlParameter[1];
            try
            {
                objDA.OpenConnection(DBConstants.GetDBConnectionString());
                //Siddharth 26 March 2015 Start
                // Parameter Sort Expression And Direction
                sqlParam[0] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                if (SortExpressionAndDirection == null)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = SortExpressionAndDirection;
                //Siddharth 26 March 2015 End
                MRFAgingForOpenPosition = objDA.GetDataSet(SPNames.USP_MRF_AgingReportForOpenPosition, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, "GetMRFAgingForOpenPosition", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
            return MRFAgingForOpenPosition;
        }
        
        // Ishwar : NISRMS : 16022015 End

        //Siddharth 23-02-2015
        /// <summary>
        /// get the client name of project by project  code.
        /// </summary>
        /// <param name="projectCode"></param>
        /// <returns></returns>
        public BusinessEntities.ProjectDetails getClientNameByProjectCode(string projectCode)
        {
            DataAccessClass ProjectDetailsDA = new DataAccessClass();
            try
            {
                BusinessEntities.ProjectDetails projectDetails = new BusinessEntities.ProjectDetails();
                SqlDataReader contractDataReader;

                ProjectDetailsDA.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.ProjectName, DbType.String);
                sqlParam[0].Value = projectCode;

                contractDataReader = ProjectDetailsDA.ExecuteReaderSP(SPNames.Contract_CheckProjectName, sqlParam);

                while (contractDataReader.Read())
                {
                    projectDetails.ClientName = contractDataReader.GetValue(2).ToString();
                }
                return projectDetails;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "MRFDetail.cs", "getClientNameByProjectCode", EventIDConstants.RAVE_HR_CONTRACT_DATA_ACCESS_LAYER);
            }
            finally
            {
                ProjectDetailsDA.CloseConncetion();
            }
        }

        //Ishwar Patil 21/04/2015 Start
        public BusinessEntities.RaveHRCollection GetSkillsList(string SkillsName)
        {
            DataAccessClass objDA = new DataAccessClass();
            objSqlParameter = new SqlParameter[1];

            objSqlParameter[0] = new SqlParameter(SPParameter.SkillName, SkillsName);

            BusinessEntities.RaveHRCollection skillsList = new BusinessEntities.RaveHRCollection();
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());
                    objDataReader = objDA.ExecuteReaderSP(SPNames.USP_MRF_GetPrimarySkills, objSqlParameter);
                    while (objDataReader.Read())
                    {
                        objMrfDetail = new BusinessEntities.MRFDetail();

                        objMrfDetail.SkillId = Convert.ToInt32(objDataReader[DbTableColumn.SkillId].ToString());
                        objMrfDetail.Skill = objDataReader[DbTableColumn.Skill].ToString();
                                                
                        skillsList.Add(objMrfDetail);
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
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_MRF, "GetSkillsList", EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    if (!objDataReader.IsClosed)
                    {
                        objDataReader.Close();
                    }
                }
                objDA.CloseConncetion();
            }

            return skillsList;
        }
        //Ishwar Patil 21/04/2015 End
    }
}
