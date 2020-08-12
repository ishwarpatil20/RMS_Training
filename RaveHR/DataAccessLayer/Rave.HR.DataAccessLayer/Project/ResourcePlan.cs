//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           ResourcePlan.cs       
//  Author:         prashant.mala
//  Date written:   01/09/2009/ 6:41:30 PM
//  Description:    This class provides the Data Access layer methods for Resource Plan.
//                  
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  4/8/2009 5:30:30 PM  prashant.mala    n/a     Created    
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Common;
using Common.Constants;
using BusinessEntities;
using System.Reflection;

namespace Rave.HR.DataAccessLayer.Projects
{
    public class ResourcePlan
    {
        #region Private Field Members

        //define CLASS_NAME_RP
        private const string CLASS_NAME_RP = "ResourcePlan.cs";

        //define SUCCESS
        private const string SUCCESS = "SUCCESS";

        #endregion

        /// <summary>
        /// Data reader object
        /// </summary>
        SqlDataReader objReader = null;

        /// <summary>
        /// Data access class 
        /// </summary>
        DataAccessClass objDAResourcePlan = null;

        /// <summary>
        /// Add Resource Plan
        /// </summary>
        public void AddResourcePlan(BusinessEntities.ResourcePlan objBEResourcePlan, ref int ResourcePlanId)
        {
            objDAResourcePlan = new DataAccessClass();
            objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());
            try
            {
               SqlParameter[] sqlParam = new SqlParameter[16];
                sqlParam[0] = new SqlParameter(SPParameter.ProjectId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.ProjectId;

                sqlParam[1] = new SqlParameter(SPParameter.Role, DbType.String);
                sqlParam[1].Value = objBEResourcePlan.Role;

                sqlParam[2] = new SqlParameter(SPParameter.StartDate, DbType.DateTime);
                sqlParam[2].Value = objBEResourcePlan.StartDate;

                sqlParam[3] = new SqlParameter(SPParameter.EndDate, DbType.DateTime);
                sqlParam[3].Value = objBEResourcePlan.EndDate;

                sqlParam[4] = new SqlParameter(SPParameter.RPStatusId, DbType.String);
                sqlParam[4].Value = objBEResourcePlan.RPStatusId;

                sqlParam[5] = new SqlParameter(SPParameter.ResourcePlanCreated, DbType.String);
                sqlParam[5].Value = objBEResourcePlan.ResourcePlanCreated;

                sqlParam[6] = new SqlParameter(SPParameter.ProjectLocation, DbType.String);
                sqlParam[6].Value = objBEResourcePlan.ProjectLocation;

                sqlParam[7] = new SqlParameter(SPParameter.Location, DbType.String);
                sqlParam[7].Value = objBEResourcePlan.Location;

                sqlParam[8] = new SqlParameter(SPParameter.Utilization, DbType.Int32);
                sqlParam[8].Value = objBEResourcePlan.Utilization;

                sqlParam[9] = new SqlParameter(SPParameter.Billing, DbType.Int32);
                sqlParam[9].Value = objBEResourcePlan.Billing;

                sqlParam[10] = new SqlParameter(SPParameter.ResourceStartDate, DbType.DateTime);
                sqlParam[10].Value = objBEResourcePlan.ResourceStartDate;

                sqlParam[11] = new SqlParameter(SPParameter.ResourceEndDate, DbType.DateTime);
                sqlParam[11].Value = objBEResourcePlan.ResourceEndDate;

                sqlParam[12] = new SqlParameter(SPParameter.ResourcePlanDurationCreated, DbType.Boolean);
                sqlParam[12].Value = objBEResourcePlan.ResourcePlanDurationCreated;

                sqlParam[13] = new SqlParameter(SPParameter.ActualLocation, DbType.Int32);
                sqlParam[13].Value = objBEResourcePlan.ResourceLocation;

                sqlParam[14] = new SqlParameter(SPParameter.NoOfResource, DbType.Int32);
                sqlParam[14].Value = objBEResourcePlan.NumberOfResources;

                sqlParam[15] = new SqlParameter(SPParameter.ResourcePlanId, DbType.Int32);
                sqlParam[15].Direction = ParameterDirection.Output;

                //--
                objDAResourcePlan.ExecuteNonQuerySP(SPNames.ResourcePlan_AddResourcePlan, sqlParam);

                //--Get resource plan id
                ResourcePlanId = Convert.ToInt32(sqlParam[15].Value);
            }

            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "AddResourcePlan", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// Add Resource Plan duration 
        /// </summary>
        public void AddRPDuration(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            objDAResourcePlan = new DataAccessClass();
            objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[14];

                sqlParam[0] = new SqlParameter(SPParameter.ResourcePlanId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.RPId;

                sqlParam[1] = new SqlParameter(SPParameter.StartDate, DbType.DateTime);
                sqlParam[1].Value = objBEResourcePlan.StartDate;

                sqlParam[2] = new SqlParameter(SPParameter.EndDate, DbType.DateTime);
                sqlParam[2].Value = objBEResourcePlan.EndDate;

                sqlParam[3] = new SqlParameter(SPParameter.Role, DbType.String);
                sqlParam[3].Value = objBEResourcePlan.Role;

                sqlParam[4] = new SqlParameter(SPParameter.ResourcePlanDurationCreated, DbType.Boolean);
                sqlParam[4].Value = objBEResourcePlan.ResourcePlanDurationCreated;

                sqlParam[5] = new SqlParameter(SPParameter.ProjectLocation, DbType.String);
                sqlParam[5].Value = objBEResourcePlan.ProjectLocation;

                sqlParam[6] = new SqlParameter(SPParameter.Location, DbType.String);
                sqlParam[6].Value = objBEResourcePlan.ResourceLocation;

                sqlParam[7] = new SqlParameter(SPParameter.Utilization, DbType.Int32);
                sqlParam[7].Value = objBEResourcePlan.Utilization;

                sqlParam[8] = new SqlParameter(SPParameter.Billing, DbType.Int32);
                sqlParam[8].Value = objBEResourcePlan.Billing;

                sqlParam[9] = new SqlParameter(SPParameter.ResourceStartDate, DbType.DateTime);
                sqlParam[9].Value = objBEResourcePlan.ResourceStartDate;

                sqlParam[10] = new SqlParameter(SPParameter.ResourceEndDate, DbType.DateTime);
                sqlParam[10].Value = objBEResourcePlan.ResourceEndDate;

                sqlParam[11] = new SqlParameter(SPParameter.RPDurationStatusId, DbType.DateTime);
                sqlParam[11].Value = objBEResourcePlan.RPDuEditedStatusId;

                sqlParam[12] = new SqlParameter(SPParameter.ActualLocation, DbType.DateTime);
                sqlParam[12].Value = objBEResourcePlan.ResourceLocation;

                sqlParam[13] = new SqlParameter(SPParameter.NoOfResource, DbType.DateTime);
                sqlParam[13].Value = objBEResourcePlan.NumberOfResources;

                objDAResourcePlan.ExecuteNonQuerySP(SPNames.ResourcePlan_AddRPDuration, sqlParam);
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "AddRPDuration", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// Add Resource Plan duration details
        /// </summary>
        public void AddRPDurationDetail(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            objDAResourcePlan = new DataAccessClass();
            objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

            try
            {
                SqlParameter[] sqlParam = new SqlParameter[10];

                sqlParam[0] = new SqlParameter(SPParameter.ResourcePlanDurationId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.ResourcePlanDurationId;

                sqlParam[1] = new SqlParameter(SPParameter.ProjectLocation, DbType.String);
                sqlParam[1].Value = objBEResourcePlan.ProjectLocation;

                sqlParam[2] = new SqlParameter(SPParameter.ActualLocation, DbType.String);
                sqlParam[2].Value = objBEResourcePlan.ResourceLocation;

                sqlParam[3] = new SqlParameter(SPParameter.Utilization, DbType.Int32);
                sqlParam[3].Value = objBEResourcePlan.Utilization;

                sqlParam[4] = new SqlParameter(SPParameter.Billing, DbType.Int32);
                sqlParam[4].Value = objBEResourcePlan.Billing;

                sqlParam[5] = new SqlParameter(SPParameter.ResourceStartDate, DbType.DateTime);
                sqlParam[5].Value = objBEResourcePlan.ResourceStartDate;

                sqlParam[6] = new SqlParameter(SPParameter.ResourceEndDate, DbType.DateTime);
                sqlParam[6].Value = objBEResourcePlan.ResourceEndDate;

                sqlParam[7] = new SqlParameter(SPParameter.StartDate, DbType.DateTime);
                sqlParam[7].Value = objBEResourcePlan.StartDate;

                sqlParam[8] = new SqlParameter(SPParameter.EndDate, DbType.DateTime);
                sqlParam[8].Value = objBEResourcePlan.EndDate;

                if (objBEResourcePlan.RPDetailStatusId != string.Empty)
                {
                    sqlParam[9] = new SqlParameter(SPParameter.RPDetailStatusId, DbType.String);
                    sqlParam[9].Value = objBEResourcePlan.RPDetailStatusId;
                }
                else
                {
                    sqlParam[9] = new SqlParameter(SPParameter.RPDetailStatusId, DbType.String);
                    sqlParam[9].Value = null;
                }

                //--
                objDAResourcePlan.ExecuteNonQuerySP(SPNames.ResourcePlan_AddRPDetail, sqlParam);
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "AddRPDurationDetail", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// Get resource plan duration
        /// </summary>
        public RaveHRCollection GetRPDuration(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter(SPParameter.ResourcePlanId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.RPId;

                sqlParam[1] = new SqlParameter(SPParameter.ResourcePlanDurationCreated, DbType.Boolean);
                sqlParam[1].Value = objBEResourcePlan.ResourcePlanDurationCreated;

                //--
                BusinessEntities.ResourcePlan objBERPDurationPlan = null;
                RaveHRCollection objListRPDuration = new RaveHRCollection();

                objReader = objDAResourcePlan.ExecuteReaderSP(SPNames.ResourcePlan_GetRPDuration, sqlParam);
                //--create list
                while (objReader.Read())
                {
                    objBERPDurationPlan = new BusinessEntities.ResourcePlan();

                    objBERPDurationPlan.RPId = int.Parse(objReader[DbTableColumn.ResourcePlanId].ToString());
                    objBERPDurationPlan.StartDate = DateTime.Parse(objReader[DbTableColumn.RPDurationStartDate].ToString());
                    objBERPDurationPlan.EndDate = DateTime.Parse(objReader[DbTableColumn.RPDurationEndDate].ToString());
                    objBERPDurationPlan.ResourcePlanDurationId = int.Parse(objReader[DbTableColumn.ResourcePlanDurationId].ToString());
                    objBERPDurationPlan.Role = objReader[DbTableColumn.Role].ToString();
                    objBERPDurationPlan.RPDId = int.Parse(objReader[DbTableColumn.RPDetailId].ToString());
                    objBERPDurationPlan.Utilization = int.Parse(objReader[DbTableColumn.Utilization].ToString());
                    objBERPDurationPlan.Billing = int.Parse(objReader[DbTableColumn.Billing].ToString());
                    objBERPDurationPlan.ResourceLocation = objReader[DbTableColumn.Location].ToString();
                    objBERPDurationPlan.ResourceStartDate = DateTime.Parse(objReader[DbTableColumn.StartDate].ToString());
                    objBERPDurationPlan.ResourceEndDate = DateTime.Parse(objReader[DbTableColumn.EndDate].ToString());
                    objBERPDurationPlan.ProjectLocation = objReader[DbTableColumn.ProjectLocation].ToString();

                    objListRPDuration.Add(objBERPDurationPlan);
                }

                //--return
                return objListRPDuration;
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "GetRPDuration", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            //close datareader and connection
            finally
            {
                //checks if datareader is null
                if (!objReader.IsClosed)
                {
                    //close datareader
                    objReader.Close();
                }

                //close connection
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// Get resource plan duration detail
        /// </summary>
        public RaveHRCollection GetRPDurationDetail(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter(SPParameter.RPDurationId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.ResourcePlanDurationId;

                sqlParam[1] = new SqlParameter(SPParameter.Mode, DbType.String);
                sqlParam[1].Value = objBEResourcePlan.Mode;

                sqlParam[2] = new SqlParameter(SPParameter.RPDEdited, DbType.Boolean);
                sqlParam[2].Value = objBEResourcePlan.RPDEdited;

                sqlParam[3] = new SqlParameter(SPParameter.RPDDeleted, DbType.Boolean);
                sqlParam[3].Value = objBEResourcePlan.RPDDeletedStatusId;

                //--
                objReader = objDAResourcePlan.ExecuteReaderSP(SPNames.ResourcePlan_GetRPDetail, sqlParam);

                BusinessEntities.ResourcePlan objBERPDurationDetail = null;
                RaveHRCollection objListRPDurationDetail = new RaveHRCollection();

                //--create list
                while (objReader.Read())
                {
                    objBERPDurationDetail = new BusinessEntities.ResourcePlan();

                    objBERPDurationDetail.RPDRowNo = objReader[DbTableColumn.RowNo].ToString();
                    objBERPDurationDetail.RPDId = int.Parse(objReader[DbTableColumn.RPDetailId].ToString());
                    objBERPDurationDetail.Utilization = int.Parse(objReader[DbTableColumn.Utilization].ToString());
                    objBERPDurationDetail.Billing = int.Parse(objReader[DbTableColumn.Billing].ToString());
                    objBERPDurationDetail.ResourceLocation = objReader[DbTableColumn.Location].ToString();
                    objBERPDurationDetail.ResourceStartDate = DateTime.Parse(objReader[DbTableColumn.StartDate].ToString());
                    objBERPDurationDetail.ResourceEndDate = DateTime.Parse(objReader[DbTableColumn.EndDate].ToString());
                    objBERPDurationDetail.ProjectLocation = objReader[DbTableColumn.ProjectLocation].ToString();

                    objListRPDurationDetail.Add(objBERPDurationDetail);
                }

                //--return
                return objListRPDurationDetail;
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "GetRPDurationDetail", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            //close datareader and connection
            finally
            {
                //checks if datareader is null
                if (!objReader.IsClosed)
                {
                    //close datareader
                    objReader.Close();
                }
                //close connection
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// Get inactive resource plan for the project
        /// </summary>
        public RaveHRCollection GetInactiveResourcePlan(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[2];

                sqlParam[0] = new SqlParameter(SPParameter.ProjectId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.ProjectId;

                sqlParam[1] = new SqlParameter(SPParameter.ResourcePlanCreated, DbType.Boolean);
                sqlParam[1].Value = objBEResourcePlan.ResourcePlanCreated;

                //--
                objReader = objDAResourcePlan.ExecuteReaderSP(SPNames.ResourcePlan_GetInactiveResourcePlan, sqlParam);
                BusinessEntities.ResourcePlan objBEInactiveResourcePlan = null;
                RaveHRCollection objListInactiveResourcePlan = new RaveHRCollection();

                //--create list
                while (objReader.Read())
                {
                    objBEInactiveResourcePlan = new BusinessEntities.ResourcePlan();

                    objBEInactiveResourcePlan.RPId = int.Parse(objReader[DbTableColumn.ResourcePlanId].ToString());
                    objListInactiveResourcePlan.Add(objBEInactiveResourcePlan);
                }

                //--return
                return objListInactiveResourcePlan;
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "GetInactiveResourcePlan", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            //close datareader and connection
            finally
            {
                //checks if datareader is null
                if (!objReader.IsClosed)
                {
                    //close datareader
                    objReader.Close();
                }
                //close connection
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// Get inactive resource plan duration detail for the resource plan
        /// </summary>
        public RaveHRCollection GetInactiveRPDurationDetail(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[2];

                sqlParam[0] = new SqlParameter(SPParameter.ResourcePlanId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.RPId;
                sqlParam[1] = new SqlParameter(SPParameter.ResourcePlanDurationCreated, DbType.Boolean);
                sqlParam[1].Value = objBEResourcePlan.ResourcePlanDurationCreated;

                //--
                objReader = objDAResourcePlan.ExecuteReaderSP(SPNames.ResourcePlan_GetInactiveRPDurationDetail, sqlParam);

                BusinessEntities.ResourcePlan objBERPInactiveRPDurationDetail = null;
                RaveHRCollection objListRPInactiveRPDurationDetail = new RaveHRCollection();

                //--create list
                while (objReader.Read())
                {
                    objBERPInactiveRPDurationDetail = new BusinessEntities.ResourcePlan();

                    objBERPInactiveRPDurationDetail.RPId = int.Parse(objReader[DbTableColumn.ResourcePlanId].ToString());
                    objBERPInactiveRPDurationDetail.ResourcePlanDurationId = int.Parse(objReader[DbTableColumn.ResourcePlanDurationId].ToString());
                    objBERPInactiveRPDurationDetail.RPDId = int.Parse(objReader[DbTableColumn.RPDetailId].ToString());
                    objListRPInactiveRPDurationDetail.Add(objBERPInactiveRPDurationDetail);

                }

                //--return
                return objListRPInactiveRPDurationDetail;
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "GetInactiveRPDurationDetail", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            //close datareader and connection
            finally
            {
                //checks if datareader is null
                if (!objReader.IsClosed)
                {
                    //close datareader
                    objReader.Close();
                }
                //close connection
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// Delete resource plan duration
        /// </summary>
        public void DeleteRPDuration(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.RPDurationId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.ResourcePlanDurationId;

                //--
                objDAResourcePlan.ExecuteNonQuerySP(SPNames.ResourcePlan_DeleteRPDuration, sqlParam);
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "DeleteRPDuration", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// Delete resource plan detail
        /// </summary>
        public void DeleteRPDetail(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.RPDetailId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.RPDId;

                //--
                objDAResourcePlan.ExecuteNonQuerySP(SPNames.ResourcePlan_DeleteRPDetail, sqlParam);
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "DeleteRPDetail", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// Save resource plan
        /// </summary>
        public void CreateRPDuration(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[4];

                sqlParam[0] = new SqlParameter(SPParameter.ResourcePlanDurationId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.ResourcePlanDurationId;

                sqlParam[1] = new SqlParameter(SPParameter.StartDate, DbType.Int32);
                sqlParam[1].Value = objBEResourcePlan.StartDate;

                sqlParam[2] = new SqlParameter(SPParameter.EndDate, DbType.Int32);
                sqlParam[2].Value = objBEResourcePlan.EndDate;

                sqlParam[3] = new SqlParameter(SPParameter.NoOfResource, DbType.Int32);
                sqlParam[3].Value = objBEResourcePlan.NumberOfResources;

                //--
                objDAResourcePlan.ExecuteNonQuerySP(SPNames.ResourcePlan_CreateRPDuration, sqlParam);
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "CreateRPDuration", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// Get resource plan
        /// </summary>
        public RaveHRCollection GetResourcePlan(BusinessEntities.ResourcePlan objBEResourcePlan, ref int pageCount)
        {
            try
            {
                //--Get the sort expression
                switch (objBEResourcePlan.SortExpression)
                {
                    case "RPDuRowNo":
                        objBEResourcePlan.SortExpression = "RowNo";
                        break;
                    case "Role":
                        objBEResourcePlan.SortExpression = "Role";
                        break;
                    case "StartDate":
                        objBEResourcePlan.SortExpression = "StartDate";
                        break;
                    case "EndDate":
                        objBEResourcePlan.SortExpression = "EndDate";
                        break;
                    default:
                        objBEResourcePlan.SortExpression = "RowNo";
                        break;
                }

                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[7];

                sqlParam[0] = new SqlParameter(SPParameter.ProjectId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.ProjectId;

                sqlParam[1] = new SqlParameter(SPParameter.ResourcePlanCreated, DbType.Boolean);
                sqlParam[1].Value = objBEResourcePlan.ResourcePlanCreated;

                sqlParam[2] = new SqlParameter(SPParameter.ResourcePlanDurationCreated, DbType.Boolean);
                sqlParam[2].Value = objBEResourcePlan.ResourcePlanDurationCreated;

                sqlParam[3] = new SqlParameter(SPParameter.pageNum, DbType.Int32);
                sqlParam[3].Value = objBEResourcePlan.PageNumber;

                sqlParam[4] = new SqlParameter(SPParameter.pageSize, DbType.Int32);
                sqlParam[4].Value = objBEResourcePlan.PageSize;

                sqlParam[5] = new SqlParameter(SPParameter.SortExpression, DbType.String);
                sqlParam[5].Value = objBEResourcePlan.SortExpression + "  " + objBEResourcePlan.SortDirection;

                sqlParam[6] = new SqlParameter(SPParameter.pageCount, DbType.Int32);
                sqlParam[6].Direction = ParameterDirection.Output;

                //--Get data;
                DataSet dsGetResourcePlan = objDAResourcePlan.GetDataSet(SPNames.ResourcePlan_GetResourcePlan, sqlParam);
                //objReader = objDAResourcePlan.ExecuteReaderSP(SPNames.ResourcePlan_GetResourcePlan, sqlParam);

                //--Get pagecount
                pageCount = Convert.ToInt32(sqlParam[6].Value.ToString());
                //if (objReader.Read())
                //{

                //}

                BusinessEntities.ResourcePlan objBEGetResourcePlan = null;
                RaveHRCollection objListGetResourcePlan = new RaveHRCollection();
                //--create list
                //--create list
                foreach (DataRow dr in dsGetResourcePlan.Tables[0].Rows)
                {
                    objBEGetResourcePlan = new BusinessEntities.ResourcePlan();

                    objBEGetResourcePlan.RPDuRowNo = dr[DbTableColumn.RowNo].ToString();
                    objBEGetResourcePlan.RPId = int.Parse(dr[DbTableColumn.ResourcePlanId].ToString());
                    objBEGetResourcePlan.ResourcePlanDurationId = int.Parse(dr[DbTableColumn.ResourcePlanDurationId].ToString());
                    objBEGetResourcePlan.Role = dr[DbTableColumn.Role].ToString();
                    objBEGetResourcePlan.StartDate = DateTime.Parse(dr[DbTableColumn.StartDate].ToString());
                    objBEGetResourcePlan.EndDate = DateTime.Parse(dr[DbTableColumn.EndDate].ToString());
                    objBEGetResourcePlan.Billing = int.Parse(dr[DbTableColumn.Billing].ToString());
                    objListGetResourcePlan.Add(objBEGetResourcePlan);
                }

                //--return
                return objListGetResourcePlan;
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "GetResourcePlan", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAResourcePlan.CloseConncetion();
            }

        }

        /// <summary>
        /// Create resource plan
        /// </summary>
        public void CreateResourcePlan(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[7];

                sqlParam[0] = new SqlParameter(SPParameter.ResourcePlanId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.RPId;

                sqlParam[1] = new SqlParameter(SPParameter.ResourcePlanCreated, DbType.Boolean);
                sqlParam[1].Value = objBEResourcePlan.ResourcePlanCreated;

                sqlParam[2] = new SqlParameter(SPParameter.CreatedById, DbType.String);
                sqlParam[2].Value = objBEResourcePlan.CreatedById;

                sqlParam[3] = new SqlParameter(SPParameter.CreatedDate, DbType.DateTime);
                sqlParam[3].Value = objBEResourcePlan.CreatedDate;

                sqlParam[4] = new SqlParameter(SPParameter.LastModifiedById, DbType.String);
                sqlParam[4].Value = objBEResourcePlan.LastModifiedById;

                sqlParam[5] = new SqlParameter(SPParameter.LastModifiedDate, DbType.DateTime);
                sqlParam[5].Value = objBEResourcePlan.LastModifiedDate;

                sqlParam[6] = new SqlParameter(SPParameter.RPStatusId, DbType.String);
                sqlParam[6].Value = objBEResourcePlan.RPStatusId;

                //--
                objDAResourcePlan.ExecuteNonQuerySP(SPNames.ResourcePlan_CreateResourcePlan, sqlParam);
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "CreateResourcePlan", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// Get Resource plan duration by id
        /// </summary>
        public BusinessEntities.ResourcePlan RPDurationByID(BusinessEntities.ResourcePlan objBEResourcePlan, string Mode)
        {
            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());
                // Rajan Kumar : Issue 46252: 14/02/2014 : Starts                        			 
                // Desc : In MRF history need to implemented in all cases in RMS.
                //Pass Email to know who is going to modified the data
                // SqlParameter[] sqlParam = new SqlParameter[2];
                SqlParameter[] sqlParam = new SqlParameter[3];
                // Rajan Kumar : Issue 46252: 14/02/2014 : END               
                sqlParam[0] = new SqlParameter(SPParameter.RPDurationId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.ResourcePlanDurationId;

                sqlParam[1] = new SqlParameter(SPParameter.Mode, DbType.String);
                sqlParam[1].Value = Mode;
                // Rajan Kumar : Issue 46252: 14/02/2014 : Starts                        			 
                // Desc : In MRF history need to implemented in all cases in RMS.
                //Pass Email to know who is going to modified the data
                sqlParam[2] = new SqlParameter(SPParameter.EmailId, DbType.String);
                sqlParam[2].Value = objBEResourcePlan.ApproverId;
                // Rajan Kumar : Issue 46252: 14/02/2014 : END     
                //--
                objReader = objDAResourcePlan.ExecuteReaderSP(SPNames.ResourcePlan_RPDurationById, sqlParam);
                BusinessEntities.ResourcePlan objBERPDuration = new BusinessEntities.ResourcePlan();

                if (objReader.Read())
                {
                    objBERPDuration.ResourcePlanDurationId = Convert.ToInt32(objReader[DbTableColumn.ResourcePlanDurationId].ToString());
                    objBERPDuration.RPId = Convert.ToInt32(objReader[DbTableColumn.ResourcePlanId].ToString());
                    objBERPDuration.StartDate = DateTime.Parse(objReader[DbTableColumn.RPDurationStartDate].ToString());
                    objBERPDuration.EndDate = DateTime.Parse(objReader[DbTableColumn.RPDurationEndDate].ToString());
                    objBERPDuration.Role = objReader[DbTableColumn.Role].ToString();
                }

                //--return
                return objBERPDuration;
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "RPDurationByID", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            //close datareader and connection
            finally
            {
                //checks if datareader is null
                if (!objReader.IsClosed)
                {
                    //close datareader
                    objReader.Close();
                }
                //close connection
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// update Resource plan duration by id
        /// </summary>
        public void UpdateRPDurationByID(BusinessEntities.ResourcePlan objBEResourcePlan, string Mode)
        {
            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());
                // Rajan Kumar : Issue 46252: 14/02/2014 : Starts                        			 
                // Desc : In MRF history need to implemented in all cases in RMS.
                //Pass Email to know who is going to modified the data
                //SqlParameter[] sqlParam = new SqlParameter[5];
                SqlParameter[] sqlParam = new SqlParameter[6];
                // Rajan Kumar : Issue 46252: 14/02/2014 : END
                
                sqlParam[0] = new SqlParameter(SPParameter.RPDurationId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.ResourcePlanDurationId;

                sqlParam[1] = new SqlParameter(SPParameter.Mode, DbType.String);
                sqlParam[1].Value = Mode;

                sqlParam[2] = new SqlParameter(SPParameter.StartDate, DbType.DateTime);
                sqlParam[2].Value = objBEResourcePlan.StartDate;

                sqlParam[3] = new SqlParameter(SPParameter.EndDate, DbType.DateTime);
                sqlParam[3].Value = objBEResourcePlan.EndDate;

                sqlParam[4] = new SqlParameter(SPParameter.Role, DbType.String);
                sqlParam[4].Value = objBEResourcePlan.Role;
                // Rajan Kumar : Issue 46252: 14/02/2014 : Starts                        			 
                // Desc : In MRF history need to implemented in all cases in RMS.
                //Pass Email to know who is going to modified the data
                sqlParam[5] = new SqlParameter(SPParameter.EmailId, DbType.String);
                sqlParam[5].Value = objBEResourcePlan.ApproverId;
                // Rajan Kumar : Issue 46252: 14/02/2014 : END

                //--
                objDAResourcePlan.ExecuteNonQuerySP(SPNames.ResourcePlan_RPDurationById, sqlParam);
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "UpdateRPDurationByID", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// Get Resource plan detail by id
        /// </summary>
        public BusinessEntities.ResourcePlan RPDetailByID(BusinessEntities.ResourcePlan objBEResourcePlan, string Mode)
        {
            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter(SPParameter.RPDetailId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.RPDId;

                sqlParam[1] = new SqlParameter(SPParameter.Mode, DbType.String);
                sqlParam[1].Value = Mode;

                //--
                objReader = objDAResourcePlan.ExecuteReaderSP(SPNames.ResourcePlan_RPDetailById, sqlParam);

                BusinessEntities.ResourcePlan objBERPDetail = new BusinessEntities.ResourcePlan();

                if (objReader.Read())
                {
                    objBERPDetail.StartDate = DateTime.Parse(objReader[DbTableColumn.StartDate].ToString());
                    objBERPDetail.EndDate = DateTime.Parse(objReader[DbTableColumn.EndDate].ToString());
                    objBERPDetail.Role = objReader[DbTableColumn.Role].ToString();
                    objBERPDetail.RPDId = int.Parse(objReader[DbTableColumn.RPDetailId].ToString());
                    objBERPDetail.ResourcePlanDurationId = int.Parse(objReader[DbTableColumn.ResourcePlanDurationId].ToString());
                    objBERPDetail.Utilization = int.Parse(objReader[DbTableColumn.Utilization].ToString());
                    objBERPDetail.Billing = int.Parse(objReader[DbTableColumn.Billing].ToString());
                    objBERPDetail.ResourceLocation = objReader[DbTableColumn.Location].ToString();
                    objBERPDetail.ProjectLocation = objReader[DbTableColumn.ProjectLocation].ToString();
                    objBERPDetail.ResourceStartDate = DateTime.Parse(objReader[DbTableColumn.ResourceStartDate].ToString());
                    objBERPDetail.ResourceEndDate = DateTime.Parse(objReader[DbTableColumn.ResourceEndDate].ToString());
                    if (objReader[DbTableColumn.RPDuHistoryId].ToString() != "")
                    {
                        objBERPDetail.RPDuHistoryId = int.Parse(objReader[DbTableColumn.RPDuHistoryId].ToString());
                    }
                    else
                    {
                        objBERPDetail.RPDuHistoryId = 0;
                    }
                }
                //--return
                return objBERPDetail;
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "RPDetailByID", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            //close datareader and connection
            finally
            {
                //checks if datareader is null
                if (!objReader.IsClosed)
                {
                    //close datareader
                    objReader.Close();
                }
                //close connection
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// update Resource plan detail by id
        /// </summary>
        public void UpdateRPDetailByID(BusinessEntities.ResourcePlan objBEResourcePlan, string Mode)
        {
            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[8];
                sqlParam[0] = new SqlParameter(SPParameter.RPDetailId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.RPDId;

                sqlParam[1] = new SqlParameter(SPParameter.Mode, DbType.String);
                sqlParam[1].Value = Mode;

                sqlParam[2] = new SqlParameter(SPParameter.ProjectLocation, DbType.String);
                sqlParam[2].Value = objBEResourcePlan.ProjectLocation;

                sqlParam[3] = new SqlParameter(SPParameter.Location, DbType.String);
                sqlParam[3].Value = objBEResourcePlan.ResourceLocation;

                sqlParam[4] = new SqlParameter(SPParameter.Utilization, DbType.Int32);
                sqlParam[4].Value = objBEResourcePlan.Utilization;

                sqlParam[5] = new SqlParameter(SPParameter.Billing, DbType.Int32);
                sqlParam[5].Value = objBEResourcePlan.Billing;

                sqlParam[6] = new SqlParameter(SPParameter.ResourceStartDate, DbType.DateTime);
                sqlParam[6].Value = objBEResourcePlan.ResourceStartDate;

                sqlParam[7] = new SqlParameter(SPParameter.ResourceEndDate, DbType.DateTime);
                sqlParam[7].Value = objBEResourcePlan.ResourceEndDate;

                //--
                string strResponse = string.Empty;
                objDAResourcePlan.ExecuteNonQuerySP(SPNames.ResourcePlan_RPDetailById, sqlParam);
            }

             //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "UpdateRPDetailByID", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// Delete Resource plan
        /// </summary>
        public void DeleteResourcePlan(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.ResourcePlanId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.RPId;

                //--
                objDAResourcePlan.ExecuteNonQuerySP(SPNames.ResourcePlan_DeleteResourcePlan, sqlParam);
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "DeleteResourcePlan", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// method to get project details for approve/reject resource plan
        /// </summary>
        public BusinessEntities.RaveHRCollection GetResourcePlanForApproveRejectRP(BusinessEntities.ResourcePlan objBEApproveRejectRP)
        {

            // Initialise Data Access Class object
            objDAResourcePlan = new DataAccessClass();

            // Initialise Collection class object
            RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                //Open the connection to DB
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                //declare array of sqlparameter
                SqlParameter[] sqlParam = new SqlParameter[5];

                //create sqlparameter @ProjectId of type int and assign value to it
                sqlParam[0] = new SqlParameter(SPParameter.ProjectId, DbType.Int32);
                sqlParam[0].Value = objBEApproveRejectRP.ProjectId;

                //create sqlparameter @RPApprovalStatusId of type int and assign value to it 
                sqlParam[1] = new SqlParameter(SPParameter.RPApprovalStatusId, DbType.Int32);
                sqlParam[1].Value = objBEApproveRejectRP.RPApprovalStatusId;

                //create sqlparameter @RPApprovalStatusId of type int and assign value to it 
                sqlParam[2] = new SqlParameter(SPParameter.ResourcePlanCreated, DbType.Int32);
                sqlParam[2].Value = 1;

                //create sqlparameter @RPStatusId of type int and assign value to it 
                sqlParam[3] = new SqlParameter(SPParameter.RPStatusId, DbType.String);
                sqlParam[3].Value = Convert.ToInt32(MasterEnum.RPEditionStatus.Deleted);

                sqlParam[4] = new SqlParameter(SPParameter.RPRejectedStatusId, DbType.Int32);
                sqlParam[4].Value = Convert.ToInt32(MasterEnum.ResourcePlanApprovalStatus.Rejected);

                //--Get data for resource plan 
                DataSet dsGetResourcePlan = objDAResourcePlan.GetDataSet(SPNames.ResourcePlan_GetResourcePlanForApproveRejectRP, sqlParam);

                //loops through dataset
                foreach (DataRow dr in dsGetResourcePlan.Tables[0].Rows)
                {
                    //create new instance for BusinessEntities.ResourcePlan()
                    BusinessEntities.ResourcePlan objBEGetResourcePlan = new BusinessEntities.ResourcePlan();

                    //assign resource plan id to businessentity object
                    objBEGetResourcePlan.ProjectId = int.Parse(dr[DbTableColumn.ProjectId].ToString());

                    //assign resource plan id to businessentity object
                    objBEGetResourcePlan.RPId = int.Parse(dr[DbTableColumn.ResourcePlanId].ToString());

                    //assign resource plan code to businessentity object
                    objBEGetResourcePlan.ResourcePlanCode = dr[DbTableColumn.RPCode].ToString();

                    //assign StartDate 
                    objBEGetResourcePlan.StartDate = DateTime.Parse(dr[DbTableColumn.StartDate].ToString());

                    //assign EndDate 
                    objBEGetResourcePlan.EndDate = DateTime.Parse(dr[DbTableColumn.EndDate].ToString());

                    //assign resource plan createddate to businessentity object
                    objBEGetResourcePlan.CreatedDate = DateTime.Parse(dr[DbTableColumn.CreatedDate].ToString());

                    //assign resource plan createdbyid to businessentity object
                    objBEGetResourcePlan.CreatedBy = dr[DbTableColumn.CreatedBy].ToString();

                    //assign resource plan createdby to businessentity object
                    objBEGetResourcePlan.CreatedById = dr[DbTableColumn.CreatedById].ToString();

                    //assign resource plan createdby to businessentity object
                    objBEGetResourcePlan.ResourcePlanApprovalStatus = dr[DbTableColumn.Status].ToString();

                    //add to collection object
                    raveHRCollection.Add(objBEGetResourcePlan);
                }

                //--return the Collection
                return raveHRCollection;

            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "GetResourcePlanForApproveRejectRP", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            //close datareader and connection
            finally
            {
                //close connection
                objDAResourcePlan.CloseConncetion();
            }

        }

        /// <summary>
        /// method to add comments while approval or rejection
        /// </summary>
        public string AddReasonForApproveRejectRP(BusinessEntities.ResourcePlan objBEApproveRejectRP)
        {
            try
            {
                //declare DataAccessClass object
                objDAResourcePlan = new DataAccessClass();

                //openconnection
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                //declare sqlparameter array object
                SqlParameter[] sqlParam = new SqlParameter[5];

                //declare type and assign value for ResourcePlanId 
                sqlParam[0] = new SqlParameter(SPParameter.ResourcePlanId, DbType.Int32);
                sqlParam[0].Value = objBEApproveRejectRP.RPId;

                //declare type and assign value for ApproverId 
                sqlParam[1] = new SqlParameter(SPParameter.ApproverId, DbType.String);
                sqlParam[1].Value = objBEApproveRejectRP.ApproverId;

                //declare type and assign value for ReasonForApproval 
                sqlParam[2] = new SqlParameter(SPParameter.ReasonForApproval, DbType.String);
                sqlParam[2].Value = objBEApproveRejectRP.ReasonForApproval;

                //declare type and assign value for ResourcePlanApprovalDate 
                sqlParam[3] = new SqlParameter(SPParameter.ResourcePlanApprovalDate, DbType.DateTime);
                sqlParam[3].Value = objBEApproveRejectRP.ResourcePlanApprovalDate;

                //declare type and assign value for ResourcePlanApprovalDate 
                sqlParam[4] = new SqlParameter(SPParameter.RPApprovalStatusId, DbType.Int32);
                sqlParam[4].Value = objBEApproveRejectRP.RPApprovalStatusId;

                //declare strResponse as empty string
                string strResponse = string.Empty;

                //execute sp
                if (objDAResourcePlan.ExecuteNonQuerySP(SPNames.ResourcePlan_SaveApproveRejectResourcePlan, sqlParam) != 0)
                {
                    //if data updated successfully
                    strResponse = SUCCESS;
                }
                else
                {
                    //if data updatation fails
                    strResponse = "";
                }

                //-return
                return strResponse;
            }

            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "AddReasonForApproveRejectRP", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// This function is used to retrieve Details of a project from database for resource plan.
        /// </summary>      
        public BusinessEntities.RaveHRCollection ViewProjectDetailsForApproveRejectRP(BusinessEntities.Projects objViewProject, ref int pageCount)
        {
            // initialise data access class object
            objDAResourcePlan = new DataAccessClass();

            // initialise collection class object
            RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                //--Get the sort expression
                switch (objViewProject.SortExpression)
                {
                    case DbTableColumn.RowNo:
                        objViewProject.SortExpression = DbTableColumn.RowNo;
                        break;
                    case DbTableColumn.ProjectCode:
                        objViewProject.SortExpression = DbTableColumn.ProjectCode;
                        break;
                    case DbTableColumn.ProjectName:
                        objViewProject.SortExpression = DbTableColumn.ProjectName;
                        break;
                    case DbTableColumn.Status:
                        objViewProject.SortExpression = DbTableColumn.Status;
                        break;
                    case DbTableColumn.CreatedBy:
                        objViewProject.SortExpression = DbTableColumn.CreatedBy;
                        break;
                    case DbTableColumn.StartDate:
                        objViewProject.SortExpression = DbTableColumn.StartDate;
                        break;
                    case DbTableColumn.EndDate:
                        objViewProject.SortExpression = DbTableColumn.EndDate;
                        break;
                }

                //open the connection to db
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[6];

                //add pagenumber parameter and assign value to it
                sqlParam[0] = new SqlParameter(SPParameter.pageNum, DbType.Int32);
                sqlParam[0].Value = objViewProject.PageNumber;

                //add pagesize parameter and assign value to it
                sqlParam[1] = new SqlParameter(SPParameter.pageSize, DbType.Int32);
                sqlParam[1].Value = objViewProject.PageSize;

                //add sortexpression and sortdirection parameter and assign value to it
                sqlParam[2] = new SqlParameter(SPParameter.SortExpression, DbType.String);
                sqlParam[2].Value = objViewProject.SortExpression + "  " + objViewProject.SortDirection;

                //add sortexpression and sortdirection parameter and assign value to it
                sqlParam[3] = new SqlParameter(SPParameter.RPStatusId, DbType.String);
                sqlParam[3].Value = Convert.ToInt32(MasterEnum.ResourcePlanStatus.Active);

                //add sortexpression and sortdirection parameter and assign value to it
                sqlParam[4] = new SqlParameter(SPParameter.ResourcePlanCreated, DbType.Int32);
                sqlParam[4].Value = 1;

                //add pageCount parameter and get value from it
                sqlParam[5] = new SqlParameter(SPParameter.pageCount, DbType.Int32);
                sqlParam[5].Direction = ParameterDirection.Output;

                //--Get data for resource plan 
                DataSet dsGetProjectForResourcePlan = objDAResourcePlan.GetDataSet(SPNames.ResourcePlan_GetProjectDetailsForApproveRejectRP, sqlParam);

                //--Get pagecount
                pageCount = Convert.ToInt32(sqlParam[5].Value.ToString());

                //loops through dataset
                foreach (DataRow dr in dsGetProjectForResourcePlan.Tables[0].Rows)
                {
                    //create new instance for BusinessEntities.ResourcePlan()
                    BusinessEntities.Projects objBEGetProjectDetails = new BusinessEntities.Projects();

                    //assign ProjectRowNo 
                    objBEGetProjectDetails.ProjectRowNo = dr[DbTableColumn.RowNo].ToString();

                    //assign ProjectId 
                    objBEGetProjectDetails.ProjectId = int.Parse(dr[DbTableColumn.ProjectId].ToString());

                    //assign ProjectCode 
                    objBEGetProjectDetails.ProjectCode = dr[DbTableColumn.ProjectCode].ToString();

                    //assign ProjectName 
                    objBEGetProjectDetails.ProjectName = dr[DbTableColumn.ProjectName].ToString();

                    //assign ProjectStatus 
                    objBEGetProjectDetails.ProjectStatus = dr[DbTableColumn.Status].ToString();

                    //assign CreatedBy 
                    objBEGetProjectDetails.CreatedBy = dr[DbTableColumn.CreatedBy].ToString();

                    //checks if CreatedBy is empty
                    if (objBEGetProjectDetails.CreatedBy == "")
                    {
                        objBEGetProjectDetails.CreatedBy = "-";
                    }

                    //assign StartDate 
                    objBEGetProjectDetails.StartDate = DateTime.Parse(dr[DbTableColumn.StartDate].ToString());

                    //assign EndDate 
                    objBEGetProjectDetails.EndDate = DateTime.Parse(dr[DbTableColumn.EndDate].ToString());

                    objBEGetProjectDetails.ProjectCodeAbbrevation = dr[DbTableColumn.Con_ProjectCodeAbbrivation].ToString();

                    //add to collection object
                    raveHRCollection.Add(objBEGetProjectDetails);
                }

                //--return the Collection
                return raveHRCollection;
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "ViewProjectDetailsForApproveRejectRP", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// Get project details
        /// </summary>
        public BusinessEntities.ResourcePlan GetProjectDetails(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.ProjectId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.ProjectId;

                //--
                objReader = objDAResourcePlan.ExecuteReaderSP(SPNames.ResourcePlan_GetProjectDetails, sqlParam);
                BusinessEntities.ResourcePlan objBEProjectDetail = null;

                string str1 = objReader.HasRows.ToString();

                //--Create entity
                if (objReader.Read())
                {
                    objBEProjectDetail = new BusinessEntities.ResourcePlan();
                    objBEProjectDetail.StartDate = Convert.ToDateTime(objReader[DbTableColumn.ProjectStartDate].ToString());
                    objBEProjectDetail.EndDate = Convert.ToDateTime(objReader[DbTableColumn.ProjectEndDate].ToString());
                }

                //--return
                return objBEProjectDetail;
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "GetProjectDetails", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            //close datareader and connection
            finally
            {
                //checks if datareader is null
                if (!objReader.IsClosed)
                {
                    //close datareader
                    objReader.Close();
                }

                //close connection
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// Get Resource Plan by Id
        /// </summary>
        public RaveHRCollection GetResourcePlanById(BusinessEntities.ResourcePlan objBEResourcePlan, ref int pageCount)
        {
            try
            {
                //--Get the sort expression
                switch (objBEResourcePlan.SortExpression)
                {
                    case "RPDuRowNo":
                        objBEResourcePlan.SortExpression = "RowNo";
                        break;
                    case "Role":
                        objBEResourcePlan.SortExpression = "Role";
                        break;
                    case "StartDate":
                        objBEResourcePlan.SortExpression = "StartDate";
                        break;
                    case "EndDate":
                        objBEResourcePlan.SortExpression = "EndDate";
                        break;
                    default:
                        objBEResourcePlan.SortExpression = "RowNo";
                        break;
                }

                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[7];

                sqlParam[0] = new SqlParameter(SPParameter.ResourcePlanId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.RPId;

                sqlParam[1] = new SqlParameter(SPParameter.pageNum, DbType.Int32);
                sqlParam[1].Value = objBEResourcePlan.PageNumber;

                sqlParam[2] = new SqlParameter(SPParameter.pageSize, DbType.Int32);
                sqlParam[2].Value = objBEResourcePlan.PageSize;

                sqlParam[3] = new SqlParameter(SPParameter.SortExpression, DbType.String);
                sqlParam[3].Value = objBEResourcePlan.SortExpression + "  " + objBEResourcePlan.SortDirection;

                sqlParam[4] = new SqlParameter(SPParameter.RPDuDeleted, DbType.Int32);
                sqlParam[4].Value = objBEResourcePlan.RPDuDeletedStatusId;

                sqlParam[5] = new SqlParameter(SPParameter.RPDDeleted, DbType.Int32);
                sqlParam[5].Value = objBEResourcePlan.RPDDeletedStatusId;

                sqlParam[6] = new SqlParameter(SPParameter.pageCount, DbType.Int32);
                sqlParam[6].Direction = ParameterDirection.Output;

                //--Get data;
                DataSet dsGetResourcePlan = objDAResourcePlan.GetDataSet(SPNames.ResourcePlan_GetResourcePlanById, sqlParam);

                //--Get pagecount
                pageCount = Convert.ToInt32(sqlParam[6].Value.ToString());

                BusinessEntities.ResourcePlan objBEGetResourcePlan = null;
                RaveHRCollection objListGetResourcePlan = new RaveHRCollection();
                //--create list
                foreach (DataRow dr in dsGetResourcePlan.Tables[0].Rows)
                {
                    objBEGetResourcePlan = new BusinessEntities.ResourcePlan();

                    objBEGetResourcePlan.RPDuRowNo = dr[DbTableColumn.RowNo].ToString();
                    objBEGetResourcePlan.RPId = int.Parse(dr[DbTableColumn.ResourcePlanId].ToString());
                    objBEGetResourcePlan.ResourcePlanCode = dr[DbTableColumn.RPCode].ToString();
                    objBEGetResourcePlan.RPStatusId = int.Parse(dr[DbTableColumn.RPStatusId].ToString());
                    objBEGetResourcePlan.ResourcePlanDurationId = int.Parse(dr[DbTableColumn.ResourcePlanDurationId].ToString());
                    objBEGetResourcePlan.Role = dr[DbTableColumn.Role].ToString();
                    objBEGetResourcePlan.StartDate = DateTime.Parse(dr[DbTableColumn.StartDate].ToString());
                    objBEGetResourcePlan.EndDate = DateTime.Parse(dr[DbTableColumn.EndDate].ToString());
                    objBEGetResourcePlan.ResourceName = dr[DbTableColumn.EmployeeName].ToString();
                    objBEGetResourcePlan.MRFCode = dr[DbTableColumn.MRFCode].ToString();
                    objBEGetResourcePlan.MRFStatus = dr[DbTableColumn.MRFStaus].ToString();
                    objListGetResourcePlan.Add(objBEGetResourcePlan);
                }

                //--
                return objListGetResourcePlan;
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "GetResourcePlanById", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAResourcePlan.CloseConncetion();
            }

        }

        /// <summary>
        /// Edit Resource Plan by Id
        /// </summary>
        public void EditRPDuration(BusinessEntities.ResourcePlan objBEResourcePlan, bool rolechange, string StrMRFCode)
        {

            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[9];

                sqlParam[0] = new SqlParameter(SPParameter.ResourcePlanDurationId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.ResourcePlanDurationId;

                sqlParam[1] = new SqlParameter(SPParameter.StartDate, DbType.DateTime);
                sqlParam[1].Value = objBEResourcePlan.StartDate;

                sqlParam[2] = new SqlParameter(SPParameter.EndDate, DbType.DateTime);
                sqlParam[2].Value = objBEResourcePlan.EndDate;

                sqlParam[3] = new SqlParameter(SPParameter.Role, DbType.String);
                sqlParam[3].Value = objBEResourcePlan.Role;

                sqlParam[4] = new SqlParameter(SPParameter.RPEdited, DbType.Boolean);
                sqlParam[4].Value = objBEResourcePlan.RPEdited;

                sqlParam[5] = new SqlParameter(SPParameter.RPDuDeleted, DbType.Boolean);
                sqlParam[5].Value = objBEResourcePlan.RPDuDeleted;

                sqlParam[6] = new SqlParameter(SPParameter.RPStatusId, DbType.Boolean);
                sqlParam[6].Value = objBEResourcePlan.RPStatusId;

                sqlParam[7] = new SqlParameter(SPParameter.MRFCode, DbType.String);
                if (StrMRFCode == string.Empty || StrMRFCode == null)
                    sqlParam[7].Value = DBNull.Value;
                else
                    sqlParam[7].Value = StrMRFCode;

                sqlParam[8] = new SqlParameter(SPParameter.rolechange, DbType.Boolean);
                sqlParam[8].Value = rolechange;
                //--
                objDAResourcePlan.ExecuteNonQuerySP(SPNames.ResourcePlan_AddRPEdited, sqlParam);
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "EditRPDuration", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAResourcePlan.CloseConncetion();
            }
        }
        /// <summary>
        /// To get the active or inactive resource plan.
        /// </summary>
        public BusinessEntities.RaveHRCollection GetActiveOrInactiveResourcePlan(int IsActive, int ProjectId)
        {
            // Initialise Data Access Class object.
            objDAResourcePlan = new DataAccessClass();

            // Initialise Collection class object.
            RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                //Open the connection to DB.
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] objSqlParameter = new SqlParameter[6];
                objSqlParameter[0] = new SqlParameter(SPParameter.IsActive, IsActive);
                objSqlParameter[1] = new SqlParameter(SPParameter.ProjectId, ProjectId);

                objSqlParameter[2] = new SqlParameter(SPParameter.ResourcePlanCreated, SqlDbType.Int);
                objSqlParameter[2].Value = 1;

                objSqlParameter[3] = new SqlParameter(SPParameter.RPApprovalStatusId, SqlDbType.Int);
                objSqlParameter[3].Value = Convert.ToInt32(MasterEnum.ResourcePlanApprovalStatus.Approved);

                objSqlParameter[4] = new SqlParameter(SPParameter.RPStatusId, SqlDbType.Int);
                objSqlParameter[4].Value = Convert.ToInt32(MasterEnum.ResourcePlanStatus.InActive);

                objSqlParameter[5] = new SqlParameter(SPParameter.RPRejectedStatusId, SqlDbType.Int);
                objSqlParameter[5].Value = Convert.ToInt32(MasterEnum.ResourcePlanApprovalStatus.Rejected);

                //Execute the SP
                objReader = objDAResourcePlan.ExecuteReaderSP(SPNames.ResourcePlan_GetActiveOrInactiveREsourcePlan, objSqlParameter);

                //Read the data and assign to Collection object.
                while (objReader.Read())
                {
                    //Initialise the Business Entity object.
                    KeyValue<string> keyValue = new KeyValue<string>();

                    /* Here value is appended by RPID with RPlastUpadated Date 
                      To get date on date textbox So no need to hit the database again and again*/
                    keyValue.KeyName = objReader.GetValue(0).ToString() + " " + objReader.GetValue(2).ToString();
                    keyValue.Val = objReader.GetValue(1).ToString();

                    // Add the object to Collection.
                    raveHRCollection.Add(keyValue);
                }

                // Return the Collection.
                return raveHRCollection;

            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "GetActiveOrInactiveResourcePlan", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            //close datareader and connection
            finally
            {
                //checks if datareader is null
                if (!objReader.IsClosed)
                {
                    //close datareader
                    objReader.Close();
                }
                //close connection
                objDAResourcePlan.CloseConncetion();
            }

        }

        /// <summary>
        /// update Resource plan detail by id
        /// </summary>
        public void EditRPDetailByID(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[10];
                sqlParam[0] = new SqlParameter(SPParameter.RPDetailId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.RPDId;

                sqlParam[1] = new SqlParameter(SPParameter.ProjectLocation, DbType.String);
                sqlParam[1].Value = objBEResourcePlan.ProjectLocation;

                sqlParam[2] = new SqlParameter(SPParameter.Location, DbType.String);
                sqlParam[2].Value = objBEResourcePlan.ResourceLocation;

                sqlParam[3] = new SqlParameter(SPParameter.Utilization, DbType.Int32);
                sqlParam[3].Value = objBEResourcePlan.Utilization;

                sqlParam[4] = new SqlParameter(SPParameter.Billing, DbType.Int32);
                sqlParam[4].Value = objBEResourcePlan.Billing;

                sqlParam[5] = new SqlParameter(SPParameter.ResourceStartDate, DbType.DateTime);
                sqlParam[5].Value = objBEResourcePlan.ResourceStartDate;

                sqlParam[6] = new SqlParameter(SPParameter.ResourceEndDate, DbType.DateTime);
                sqlParam[6].Value = objBEResourcePlan.ResourceEndDate;

                sqlParam[7] = new SqlParameter(SPParameter.RPEdited, DbType.Boolean);
                sqlParam[7].Value = objBEResourcePlan.RPEdited;

                sqlParam[8] = new SqlParameter(SPParameter.RPDuHistoryId, DbType.Boolean);
                sqlParam[8].Value = objBEResourcePlan.RPDuHistoryId;

                sqlParam[9] = new SqlParameter(SPParameter.RPDDeleted, DbType.Boolean);
                sqlParam[9].Value = objBEResourcePlan.RPDDeleted;

                //--
                string strResponse = string.Empty;
                objDAResourcePlan.ExecuteNonQuerySP(SPNames.ResourcePlan_AddRPDetailEdited, sqlParam);
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "EditRPDetailByID", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// Delete RP Edited in history
        /// </summary>
        public void DeleteRPEdited(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[2];

                sqlParam[0] = new SqlParameter(SPParameter.ResourcePlanId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.RPId;

                sqlParam[1] = new SqlParameter(SPParameter.RPDurationStatusId, DbType.Int32);
                sqlParam[1].Value = objBEResourcePlan.RPDuEditedStatusId;

                //--
                objDAResourcePlan.ExecuteNonQuerySP(SPNames.ResourcePlan_DeleteRPEdited, sqlParam);
            }

            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "DeleteRPEdited", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// Save RP Edited in history
        /// </summary>
        public void SaveRPEdited(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[8];
                
                //RP RP Issue 33126 START
                SqlParameter[] sqlParamUpdateFlag = new SqlParameter[1];
                //RP RP Issue 33126 END

                sqlParam[0] = new SqlParameter(SPParameter.ResourcePlanId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.RPId;

                //RP RP Issue 33126 STRAT
                sqlParamUpdateFlag[0] = new SqlParameter(SPParameter.ResourcePlanId, DbType.Int32);
                sqlParamUpdateFlag[0].Value = objBEResourcePlan.RPId;
                //RP RP Issue 33126 END


                sqlParam[1] = new SqlParameter(SPParameter.RPStatusId, DbType.Int32);
                sqlParam[1].Value = objBEResourcePlan.RPStatusId;

                sqlParam[2] = new SqlParameter(SPParameter.LastModifiedById, DbType.Int32);
                sqlParam[2].Value = objBEResourcePlan.LastModifiedById;

                sqlParam[3] = new SqlParameter(SPParameter.LastModifiedDate, DbType.DateTime);
                sqlParam[3].Value = objBEResourcePlan.LastModifiedDate;

                sqlParam[4] = new SqlParameter(SPParameter.RPDuDeleted, DbType.Int32);
                sqlParam[4].Value = objBEResourcePlan.RPDuDeletedStatusId;

                sqlParam[5] = new SqlParameter(SPParameter.RPDDeleted, DbType.Int32);
                sqlParam[5].Value = objBEResourcePlan.RPDDeletedStatusId;

                sqlParam[6] = new SqlParameter(SPParameter.RPDurationStatusId, DbType.Int32);
                sqlParam[6].Value = objBEResourcePlan.RPDuEditedStatusId;

                sqlParam[7] = new SqlParameter(SPParameter.RPFileName, DbType.String);
                sqlParam[7].Value = objBEResourcePlan.RPFileName;

                //RP RP Issue 33126 STRAT
                objDAResourcePlan.ExecuteNonQuerySP(SPNames.ResourcePlan_UpdateSendFroApprovalFlag, sqlParamUpdateFlag);
                //RP RP Issue 33126 END

                //--Check if Resoource Plan Status Id
                if (objBEResourcePlan.RPStatusId == Convert.ToInt32(MasterEnum.ResourcePlanStatus.InActive))
                {
                    objDAResourcePlan.ExecuteNonQuerySP(SPNames.ResourcePlan_SaveInactiveResourcePlanEdited, sqlParam);
                }
                else
                {
                    objDAResourcePlan.ExecuteNonQuerySP(SPNames.ResourcePlan_SaveResourcePlanEdited, sqlParam);
                }

                
            }

            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "SaveRPEdited", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// Get Project Details by Resource Plan Id
        /// </summary>
        public RaveHRCollection GetProjectById(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.ProjectId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.ProjectId;

                //--Declare var
                BusinessEntities.Projects objBEGetProject = null;
                RaveHRCollection objListGetResourcePlan = new RaveHRCollection();

                //--Get data;
                objReader = objDAResourcePlan.ExecuteReaderSP(SPNames.ResourcePlan_GetProjectDetails, sqlParam);
                //--create list
                if (objReader.Read())
                {
                    objBEGetProject = new BusinessEntities.Projects();
                    objBEGetProject.ProjectName = objReader[DbTableColumn.ProjectName].ToString();

                    objListGetResourcePlan.Add(objBEGetProject);
                }

                //--return
                return objListGetResourcePlan;
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "GetProjectById", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            //close datareader and connection
            finally
            {
                //checks if datareader is null
                if (!objReader.IsClosed)
                {
                    //close datareader
                    objReader.Close();
                }

                //close connection
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// Get Project details by Resource Plan ID.
        /// </summary>
        public RaveHRCollection GetProjectByRPId(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.ResourcePlanId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.RPId;

                //--Declare var
                BusinessEntities.Projects objBEProjectByRPId = null;
                objBEResourcePlan = null;
                RaveHRCollection objListGetProjectByRPId = new RaveHRCollection();

                //--Get data;
                objReader = objDAResourcePlan.ExecuteReaderSP(SPNames.ResourcePlan_GetProjectByRPId, sqlParam);

                while (objReader.Read())
                {
                    objBEProjectByRPId = new BusinessEntities.Projects();
                    objBEResourcePlan = new BusinessEntities.ResourcePlan();
                    objBEProjectByRPId.ProjectId = int.Parse(objReader[DbTableColumn.ProjectId].ToString());
                    objBEProjectByRPId.ProjectName = objReader[DbTableColumn.ProjectName].ToString();
                    objBEProjectByRPId.ProjectCode = objReader[DbTableColumn.ProjectCode].ToString();
                    objBEProjectByRPId.EmailIdOfPM = objReader[DbTableColumn.EmailId].ToString();
                    objBEProjectByRPId.ProjectCodeAbbrevation = objReader[DbTableColumn.Con_ProjectCodeAbbrivation].ToString();
                    objBEResourcePlan.ResourcePlanCode = objReader[DbTableColumn.RPCode].ToString();
                    objBEResourcePlan.ClientName = objReader[DbTableColumn.ClientName].ToString();
                    objBEResourcePlan.RPFileName = objReader[DbTableColumn.RPFileName].ToString();
                    objListGetProjectByRPId.Add(objBEProjectByRPId);
                    objListGetProjectByRPId.Add(objBEResourcePlan);
                }

                return objListGetProjectByRPId;

            }

            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "GetProjectByRPId", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            //close datareader and connection
            finally
            {
                //checks if datareader is null
                if (objReader != null)
                {
                    //close datareader
                    objReader.Close();
                }

                //close connection
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// get project manager by projectId.
        /// </summary>
        /// <param name="objBEResourcePlan"></param>
        /// <returns></returns>
        public RaveHRCollection GetProjectManagerByProjectId(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.ProjectId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.ProjectId;

                //--Declare variable.
                BusinessEntities.Projects objBEGetProjectManagerByProjectId = null;

                RaveHRCollection objListGetProjectManagerByProjectId = new RaveHRCollection();

                //--Get data for resource plan 
                DataSet dsGetProjectManagerByProjectId = objDAResourcePlan.GetDataSet(SPNames.ResourcePlan_GetProjectManagerByProjectId, sqlParam);

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
                    objBEGetProjectManagerByProjectId = new BusinessEntities.Projects();

                    //assign ProjectId 
                    objBEGetProjectManagerByProjectId.ProjectId = int.Parse(dr[DbTableColumn.ProjectId].ToString());

                    //assign CreatedByFullName 
                    objBEGetProjectManagerByProjectId.CreatedByFullName = dr[DbTableColumn.ProjectManager].ToString();

                    objBEGetProjectManagerByProjectId.EmailIdOfPM = dr[DbTableColumn.EmailId].ToString();

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
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "GetProjectManagerByProjectId", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// Get Resource Plan by Id
        /// </summary>
        public RaveHRCollection GetResourcePlanById(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());
                
                //RP RP Issue 33126
                SqlParameter[] sqlParam = new SqlParameter[4]; // Change array size from 3 to 4

                sqlParam[0] = new SqlParameter(SPParameter.ResourcePlanId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.RPId;

                sqlParam[1] = new SqlParameter(SPParameter.RPDurationStatusId, DbType.String);
                sqlParam[1].Value = objBEResourcePlan.RPDurationStatusId;

                sqlParam[2] = new SqlParameter(SPParameter.RPDetailStatusId, DbType.String);
                sqlParam[2].Value = objBEResourcePlan.RPDetailStatusId;

                //RP RP Issue 33126
                sqlParam[3] = new SqlParameter(SPParameter.Mode, DbType.String);
                sqlParam[3].Value = objBEResourcePlan.Mode;
                //RP Issue 33126

                //--Get data;
                DataSet dsGetResourcePlan = objDAResourcePlan.GetDataSet(SPNames.ResourcePlan_GetDumpRPById, sqlParam);

                BusinessEntities.ResourcePlan objBEGetResourcePlan = null;
                RaveHRCollection objListGetResourcePlan = new RaveHRCollection();
                //--create list
                foreach (DataRow dr in dsGetResourcePlan.Tables[0].Rows)
                {
                    objBEGetResourcePlan = new BusinessEntities.ResourcePlan();

                    objBEGetResourcePlan.Role = dr[DbTableColumn.Role].ToString();
                    objBEGetResourcePlan.StartDate = DateTime.Parse(dr[DbTableColumn.StartDate].ToString());
                    objBEGetResourcePlan.EndDate = DateTime.Parse(dr[DbTableColumn.EndDate].ToString());
                    objBEGetResourcePlan.Utilization = int.Parse(dr[DbTableColumn.Utilization].ToString());
                    objBEGetResourcePlan.Billing = int.Parse(dr[DbTableColumn.Billing].ToString());
                    objBEGetResourcePlan.ResourceLocation = dr[DbTableColumn.Location].ToString();
                    objBEGetResourcePlan.ResourcePlanDurationId = int.Parse(dr[DbTableColumn.ResourcePlanDurationId].ToString());
                    objBEGetResourcePlan.ResourceName = dr[DbTableColumn.ResourceName].ToString();
                    objListGetResourcePlan.Add(objBEGetResourcePlan);
                }

                //--
                return objListGetResourcePlan;
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "GetResourcePlanById", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// gets resoucePlanId by projectId.
        /// </summary>>
        public RaveHRCollection GetResourcePlanForProjectId(BusinessEntities.ResourcePlan objBEResourcePlan)
        {

            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[7];

                sqlParam[0] = new SqlParameter(SPParameter.ProjectId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.ProjectId;

                sqlParam[1] = new SqlParameter(SPParameter.RPStatusId, DbType.Int32);
                sqlParam[1].Value = Convert.ToInt32(MasterEnum.RPEditionStatus.Deleted);

                sqlParam[2] = new SqlParameter(SPParameter.Mode, DbType.String);
                sqlParam[2].Value = objBEResourcePlan.Mode;

                sqlParam[3] = new SqlParameter(SPParameter.ResourcePlanCreated, DbType.Boolean);
                sqlParam[3].Value = true;

                sqlParam[4] = new SqlParameter(SPParameter.RPApprovalStatusId, DbType.Int32);
                sqlParam[4].Value = Convert.ToInt32(MasterEnum.ResourcePlanApprovalStatus.Approved);

                sqlParam[5] = new SqlParameter(SPParameter.IsActive, DbType.Int32);
                sqlParam[5].Value = Convert.ToInt32(MasterEnum.ResourcePlanStatus.Active);

                sqlParam[6] = new SqlParameter(SPParameter.ResourcePlanId, DbType.Int32);
                sqlParam[6].Value = objBEResourcePlan.RPId;

                objReader = objDAResourcePlan.ExecuteReaderSP(SPNames.ResourcePlan_GetResourcePlanByProjectId, sqlParam);
                RaveHRCollection objListGetResourcePlan = new RaveHRCollection();

                while (objReader.Read())
                {
                    objBEResourcePlan = new BusinessEntities.ResourcePlan();
                    objBEResourcePlan.RPId = int.Parse(objReader[DbTableColumn.ResourcePlanId].ToString());
                    objListGetResourcePlan.Add(objBEResourcePlan);
                }

                //--
                return objListGetResourcePlan;

            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "GetResourcePlanForProjectId", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            //close datareader and connection
            finally
            {
                //checks if datareader is null
                if (!objReader.IsClosed)
                {
                    //close datareader
                    objReader.Close();
                }

                //close connection
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// get allocated resource by projectId
        /// </summary>
        public RaveHRCollection GetAllocatedResourceByProjectId(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.ProjectId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.ProjectId;

                objReader = objDAResourcePlan.ExecuteReaderSP(SPNames.ResourcePlan_GetAllocatedResourceByProjectId, sqlParam);
                RaveHRCollection objListGetResourcePlan = new RaveHRCollection();

                while (objReader.Read())
                {
                    BusinessEntities.Employee objBEEmployee = new BusinessEntities.Employee();
                    objBEEmployee.EmpProjectAllocationId = int.Parse(objReader[DbTableColumn.EmpProjectAllocationId].ToString());
                    objListGetResourcePlan.Add(objBEEmployee);
                }

                //--
                return objListGetResourcePlan;

            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "GetAllocatedResourceByProjectId", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            //close datareader and connection
            finally
            {
                //checks if datareader is null
                if (!objReader.IsClosed)
                {
                    //close datareader
                    objReader.Close();
                }

                //close connection
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// Get pending MRF's 
        /// </summary>
        public RaveHRCollection GetPendingMRF(BusinessEntities.ResourcePlan objBEResourcePlan, string StatusIds)
        {

            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[2];

                sqlParam[0] = new SqlParameter(SPParameter.ResourcePlanId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.RPId;

                sqlParam[1] = new SqlParameter(SPParameter.StatusId, DbType.String);
                sqlParam[1].Value = StatusIds;

                objReader = objDAResourcePlan.ExecuteReaderSP(SPNames.ResourcePlan_GetPendingMRF, sqlParam);
                RaveHRCollection objListGetPendingMRF = new RaveHRCollection();

                BusinessEntities.MRFDetail objBEMRF = null;
                while (objReader.Read())
                {
                    objBEMRF = new BusinessEntities.MRFDetail();
                    objListGetPendingMRF.Add(objBEMRF);
                }

                //--
                return objListGetPendingMRF;

            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "GetPendingMRF", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            //close datareader and connection
            finally
            {
                //checks if datareader is null
                if (!objReader.IsClosed)
                {
                    //close datareader
                    objReader.Close();
                }

                //close connection
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// Save resource plan
        /// </summary>
        public void CreateAndEditRPDuration(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[5];

                sqlParam[0] = new SqlParameter(SPParameter.ResourcePlanDurationId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.ResourcePlanDurationId;

                sqlParam[1] = new SqlParameter(SPParameter.StartDate, DbType.Int32);
                sqlParam[1].Value = objBEResourcePlan.StartDate;

                sqlParam[2] = new SqlParameter(SPParameter.EndDate, DbType.Int32);
                sqlParam[2].Value = objBEResourcePlan.EndDate;

                sqlParam[3] = new SqlParameter(SPParameter.NoOfResource, DbType.Int32);
                sqlParam[3].Value = objBEResourcePlan.NumberOfResources;

                sqlParam[4] = new SqlParameter(SPParameter.RPDurationStatusId, DbType.Int32);
                sqlParam[4].Value = objBEResourcePlan.RPDuEditedStatusId;

                //--
                objDAResourcePlan.ExecuteNonQuerySP(SPNames.ResourcePlan_CreateAndEditRPDuration, sqlParam);
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "CreateRPDuration", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// Get Number Of Resource By ProjectId
        /// </summary>
        public BusinessEntities.ResourcePlan GetNoOfResouceByProjectId(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[2];

                sqlParam[0] = new SqlParameter(SPParameter.ProjectId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.ProjectId;

                sqlParam[1] = new SqlParameter(SPParameter.ProjectType, DbType.Int32);
                sqlParam[1].Value = Convert.ToInt32(MasterEnum.ProjectType.TandM);

                objReader = objDAResourcePlan.ExecuteReaderSP(SPNames.ResourcePlan_GetNoOfResourceByProjectId, sqlParam);
                BusinessEntities.ResourcePlan objBENoOfResource = null;

                //--Create entity
                if (objReader.Read())
                {
                    objBENoOfResource = new BusinessEntities.ResourcePlan();
                    objBENoOfResource.ResourceNo = Convert.ToDecimal(objReader[DbTableColumn.NoOfResources].ToString());
                }

                //--return
                return objBENoOfResource;

            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "CreateRPDuration", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// method to get resource plan details by project Id.
        /// Created By : Yagendra Sharnagat
        /// Created Date: 05 Feb 2010 
        /// To get rp details on contract summary page.
        /// </summary>
        public BusinessEntities.RaveHRCollection GetResourcePlanByProjectIdForContract(BusinessEntities.ResourcePlan objBEApproveRejectRP)
        {

            // Initialise Data Access Class object
            objDAResourcePlan = new DataAccessClass();

            // Initialise Collection class object
            RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                //Open the connection to DB
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                //declare array of sqlparameter
                SqlParameter[] sqlParam = new SqlParameter[5];

                //create sqlparameter @ProjectId of type int and assign value to it
                sqlParam[0] = new SqlParameter(SPParameter.ProjectId, DbType.Int32);
                sqlParam[0].Value = objBEApproveRejectRP.ProjectId;

                //create sqlparameter @RPApprovalStatusId of type int and assign value to it 
                sqlParam[1] = new SqlParameter(SPParameter.RPApprovalStatusId, DbType.Int32);
                sqlParam[1].Value = objBEApproveRejectRP.RPApprovalStatusId;

                //create sqlparameter @RPApprovalStatusId of type int and assign value to it 
                sqlParam[2] = new SqlParameter(SPParameter.ResourcePlanCreated, DbType.Int32);
                sqlParam[2].Value = 1;

                //create sqlparameter @RPStatusId of type int and assign value to it 
                sqlParam[3] = new SqlParameter(SPParameter.RPStatusId, DbType.String);
                sqlParam[3].Value = Convert.ToInt32(MasterEnum.RPEditionStatus.Deleted);

                sqlParam[4] = new SqlParameter(SPParameter.RPRejectedStatusId, DbType.Int32);
                sqlParam[4].Value = Convert.ToInt32(MasterEnum.ResourcePlanApprovalStatus.Rejected);

                //--Get data for resource plan 
                DataSet dsGetResourcePlan = objDAResourcePlan.GetDataSet(SPNames.Contracts_GetRPDetails, sqlParam);

                //loops through dataset
                foreach (DataRow dr in dsGetResourcePlan.Tables[0].Rows)
                {
                    //create new instance for BusinessEntities.ResourcePlan()
                    BusinessEntities.ResourcePlan objBEGetResourcePlan = new BusinessEntities.ResourcePlan();

                    //assign resource plan id to businessentity object
                    objBEGetResourcePlan.ProjectId = int.Parse(dr[DbTableColumn.ProjectId].ToString());

                    //assign resource plan id to businessentity object
                    objBEGetResourcePlan.RPId = int.Parse(dr[DbTableColumn.ResourcePlanId].ToString());

                    //assign resource plan code to businessentity object
                    objBEGetResourcePlan.ResourcePlanCode = dr[DbTableColumn.RPCode].ToString();

                    //assign StartDate 
                    objBEGetResourcePlan.StartDate = DateTime.Parse(dr[DbTableColumn.StartDate].ToString());

                    //assign EndDate 
                    objBEGetResourcePlan.EndDate = DateTime.Parse(dr[DbTableColumn.EndDate].ToString());

                    //assign resource plan createddate to businessentity object
                    objBEGetResourcePlan.CreatedDate = DateTime.Parse(dr[DbTableColumn.CreatedDate].ToString());

                    //assign resource plan createdbyid to businessentity object
                    objBEGetResourcePlan.CreatedBy = dr[DbTableColumn.CreatedBy].ToString();

                    //assign resource plan createdby to businessentity object
                    objBEGetResourcePlan.CreatedById = dr[DbTableColumn.CreatedById].ToString();

                    //assign resource plan createdby to businessentity object
                    objBEGetResourcePlan.ResourcePlanApprovalStatus = dr[DbTableColumn.Status].ToString();

                    //add to collection object
                    raveHRCollection.Add(objBEGetResourcePlan);
                }

                //--return the Collection
                return raveHRCollection;

            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "GetResourcePlanForApproveRejectRP", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            //close datareader and connection
            finally
            {
                //close connection
                objDAResourcePlan.CloseConncetion();
            }

        }


        /// <summary>
        ///  Get edited details of resource Plan for mailing
        /// </summary>
        public RaveHRCollection GetRPDetailsForMail(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            // Initialise Data Access Class object
            objDAResourcePlan = new DataAccessClass();

            // Initialise Collection class object
            RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                //Open the connection to DB
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                //declare array of sqlparameter
                SqlParameter[] sqlParam = new SqlParameter[2];

                //create sqlparameter @ProjectId of type int and assign value to it
                sqlParam[0] = new SqlParameter(SPParameter.ResourcePlanId, DbType.Int32);
                sqlParam[0].Value = objBEResourcePlan.RPId;

                ////create sqlparameter @RPApprovalStatusId of type int and assign value to it 
                sqlParam[1] = new SqlParameter(SPParameter.RPDuEdited, DbType.Int32);
                sqlParam[1].Value = Convert.ToInt32(MasterEnum.RPDurationEditionStatus.Edited);

                //--Get data for resource plan 
                DataSet dsGetResourcePlan = objDAResourcePlan.GetDataSet(SPNames.ResourcePlan_GetResourcePlanDetailsForMail, sqlParam);

                //loops through dataset
                foreach (DataRow dr in dsGetResourcePlan.Tables[0].Rows)
                {
                    //create new instance for BusinessEntities.ResourcePlan()
                    BusinessEntities.ResourcePlan objBEGetResourcePlan = new BusinessEntities.ResourcePlan();

                    //assign resource plan id to businessentity object
                    objBEGetResourcePlan.ResourceName = dr[DbTableColumn.ResourceName].ToString();

                    //assign resource plan id to businessentity object
                    objBEGetResourcePlan.PreviousRole = dr[DbTableColumn.PreviousRole].ToString();

                    //assign resource plan id to businessentity object
                    objBEGetResourcePlan.Role = dr[DbTableColumn.Role].ToString();

                    //Aarohi : Issue 31510(CR) : 21/12/2011 : Start
                    //Commented following code for not to show Utilisation and Billing details in case of EditRP

                    //Rajan : Issue 48242 : 10/01/2013 : Start
                    //UnCommented following code for not to show Utilisation and Billing details in case of EditRP
                    //In "Resource Plan Edited" mail add Util and Billing change section in mail body. 
                    //assign resource plan code to businessentity object
                    objBEGetResourcePlan.PreviousUtilization = int.Parse(dr[DbTableColumn.PreviousUtilization].ToString());

                    //assign Utilization
                    objBEGetResourcePlan.Utilization = int.Parse(dr[DbTableColumn.Utilization].ToString());

                    //assign PreviousBilling 
                    objBEGetResourcePlan.PreviousBilling = int.Parse(dr[DbTableColumn.PreviousBilling].ToString());

                    //assign Billing 
                    objBEGetResourcePlan.Billing = int.Parse(dr[DbTableColumn.Billing].ToString());

                    if (objBEGetResourcePlan.PreviousUtilization != objBEGetResourcePlan.Utilization)
                    {
                        objBEGetResourcePlan.IsUtilizationValueChanged = true;
                    }
                    if (objBEGetResourcePlan.PreviousBilling != objBEGetResourcePlan.Billing)
                    {
                        objBEGetResourcePlan.IsBillingValueChanged = true;
                    }
                    //Rajan : Issue 48242 : 10/01/2013 : End

                    //Aarohi : Issue 31510(CR) : 21/12/2011 : End

                    //assign PreviousResourceStartDate 
                    objBEGetResourcePlan.PreviousResourceStartDate = DateTime.Parse(dr[DbTableColumn.PreviousResourceStartDate].ToString());

                    //assign ResourceStartDate 
                    objBEGetResourcePlan.ResourceStartDate = DateTime.Parse(dr[DbTableColumn.StartDate].ToString());

                    //assign PreviousResourceEndDate 
                    objBEGetResourcePlan.PreviousResourceEndDate = DateTime.Parse(dr[DbTableColumn.PreviousResourceEndDate].ToString());

                    //assign ResourceEndDate 
                    objBEGetResourcePlan.ResourceEndDate = DateTime.Parse(dr[DbTableColumn.EndDate].ToString());

                    //assign RPDurationStatusId 
                    objBEGetResourcePlan.RPDurationStatusId = dr[DbTableColumn.Status].ToString();

                    //add to collection object
                    raveHRCollection.Add(objBEGetResourcePlan);
                }

                //loops through dataset
                foreach (DataRow dr in dsGetResourcePlan.Tables[1].Rows)
                {
                    //create new instance for BusinessEntities.ResourcePlan()
                    BusinessEntities.ResourcePlan objBEGetResourcePlan = new BusinessEntities.ResourcePlan();

                    //assign resource plan id to businessentity object
                    objBEGetResourcePlan.ResourceName = dr[DbTableColumn.ResourceName].ToString();

                    //assign resource plan id to businessentity object
                    objBEGetResourcePlan.Role = dr[DbTableColumn.Role].ToString();

                    //assign Utilization
                    objBEGetResourcePlan.Utilization = int.Parse(dr[DbTableColumn.Utilization].ToString());

                    //assign Billing 
                    objBEGetResourcePlan.Billing = int.Parse(dr[DbTableColumn.Billing].ToString());

                    //assign ResourceStartDate 
                    objBEGetResourcePlan.ResourceStartDate = DateTime.Parse(dr[DbTableColumn.StartDate].ToString());

                    //assign ResourceEndDate 
                    objBEGetResourcePlan.ResourceEndDate = DateTime.Parse(dr[DbTableColumn.EndDate].ToString());

                    //add to collection object
                    raveHRCollection.Add(objBEGetResourcePlan);
                }

                //--return the Collection
                return raveHRCollection;

            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "GetRPDetailsForMail", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            //close datareader and connection
            finally
            {
                //close connection
                objDAResourcePlan.CloseConncetion();
            }
        }

        /// <summary>
        /// Method to Update Employee ProjectAllocation By Resource Plan
        /// </summary>
        public void UpdateEmployeeProjectAllocation(BusinessEntities.ResourcePlan objBEGetResourcePlanId)
        {
            try
            {
                objDAResourcePlan = new DataAccessClass();
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());
                // Rajan Kumar : Issue 46252: 13/02/2014 : Starts                        			 
                // Desc : In MRF history need to implemented in all cases in RMS.
                //Pass Email to know who is going to modified the data
                //SqlParameter[] sqlParam = new SqlParameter[2];
                SqlParameter[] sqlParam = new SqlParameter[3];
                // Rajan Kumar : Issue 46252: 13/02/2014 : END
                
                sqlParam[0] = new SqlParameter(SPParameter.ResourcePlanId, DbType.Int32);
                sqlParam[0].Value = objBEGetResourcePlanId.RPId;

                sqlParam[1] = new SqlParameter(SPParameter.RPDEdited, DbType.String);
                sqlParam[1].Value = Convert.ToInt32(MasterEnum.RPDetailEditionStatus.Edited);
                // Rajan Kumar : Issue 46252: 13/02/2014 : Starts                        			 
                // Desc : In MRF history need to implemented in all cases in RMS.
                //Pass Email to know who is going to modified the data
                sqlParam[2] = new SqlParameter(SPParameter.EmailId, DbType.String);
                sqlParam[2].Value = Convert.ToString(objBEGetResourcePlanId.ApproverId);
                // Rajan Kumar : Issue 46252: 13/02/2014 : END
                objDAResourcePlan.ExecuteNonQuerySP(SPNames.ResourcePlan_UpdateEmpProjectAllocationByResourcePlan, sqlParam);
            }

             //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "UpdateEmployeeProjectAllocation", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAResourcePlan.CloseConncetion();
            }
        }

        //RP RP Issue 33126 Start
        /// <summary>
        ///  Get edited details of resource Plan for mailing
        /// </summary>
        public int? GetRPApprovalStatus(int ResourcePlanId)
        {
            // Initialise Data Access Class object
            objDAResourcePlan = new DataAccessClass();

            // Initialise Collection class object
            RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                //Open the connection to DB
                objDAResourcePlan.OpenConnection(DBConstants.GetDBConnectionString());

                //declare array of sqlparameter
                SqlParameter[] sqlParam = new SqlParameter[1];

                //create sqlparameter @ProjectId of type int and assign value to it
                sqlParam[0] = new SqlParameter(SPParameter.ResourcePlanId, DbType.Int32);
                sqlParam[0].Value = ResourcePlanId;

                //--Get data for resource plan 
                DataSet dsGetResourcePlan = objDAResourcePlan.GetDataSet(SPNames.ResourcePlan_GetResourcePlanApprovalStatus, sqlParam);

                int? RPApprovalStatus = null;

                if (dsGetResourcePlan.Tables[0].Rows[0]["RPApprovalStatusId"] != null && dsGetResourcePlan.Tables[0].Rows[0]["RPApprovalStatusId"].ToString() != "0")
                    RPApprovalStatus = Convert.ToInt32(dsGetResourcePlan.Tables[0].Rows[0]["RPApprovalStatusId"].ToString());
                

                //--return the Collection
                return RPApprovalStatus;

            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME_RP, "GetRPApprovalStatus", EventIDConstants.RAVE_HR_RP_DATA_ACCESS_LAYER);
            }
            //close datareader and connection
            finally
            {
                //close connection
                objDAResourcePlan.CloseConncetion();
            }
        }

        //RP RP Issue 33126 End


    }




}
