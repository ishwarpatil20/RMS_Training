﻿//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           RaiseTrainingRequest.cs       
//  Class:          RaiseTrainingRequest
//  Author:         Ishwar Patil
//  Date written:   08/04/2014
//  Description:    This class contains method related to Raise Training Request 

//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  08/04/2014            Ishwar Patil    n/a     Created    
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Common;
using Common.Constants;

namespace Rave.HR.DataAccessLayer.Training
{
    public class RaiseTrainingRequest
    {
        #region
        const string RaiseTrainingRqst = "RaiseTrainingRequest.cs";
        const string SAVE = "Save";
        const string SAVEKSS = "SaveKSS";
        const string FunctionViewTechnicalTraining = "ViewTechnicalTraining";
        const string FunctionViewSoftSkillsTraining = "ViewSoftSkillsTraining";
        const string FunctionGetTechnicalTraining = "GetTechnicalTraining";
        const string FunctionGetKSSTraining = "GetKSSTraining";
        const string FunctionDeleteTechnicalSoftSkillsTraining = "DeleteTechnicalSoftSkillsTraining";
        const string FunctionViewKSSTraining = "ViewKSSTraining";
        const string FunctionDeleteKSSTraining = "DeleteKSSTraining";
        const string SAVESEMINARS = "SaveSeminars";
        const string FunctionViewSeminarsTraining = "ViewSeminarsTraining";
        const string FunctionGetSeminarsTraining = "GetSeminarsTraining";
        const string FunctionDeleteSeminarsTraining = "DeleteSeminarsTraining";
        DataSet ds;

        private static BusinessEntities.RaiseTrainingRequest RaiseTrainingCollection;
        private static BusinessEntities.RaveHRCollection raveHRCollection;
        #endregion

        #region Public Method

        /// </summary>
        /// This method is used for saving the Technical Training details
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        public int Save(BusinessEntities.RaiseTrainingRequest RaiseTraining, int TechnicalTrainingID, int SoftSkillsTrainingID)
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;
            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                if (Convert.ToInt32(RaiseTraining.TrainingType) == TechnicalTrainingID)
                {
                    objCommand = new SqlCommand(SPNames.TNI_InsertUpdateTechnicalTraining, objConnection);
                }
                else if (Convert.ToInt32(RaiseTraining.TrainingType) == SoftSkillsTrainingID)
                {
                    objCommand = new SqlCommand(SPNames.TNI_InsertUpdateSoftskillsTraining, objConnection);
                }
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[14];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.RaiseID, RaiseTraining.RaiseID);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.TrainingType, RaiseTraining.TrainingType);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.TrainingStatus, RaiseTraining.TrainingStatus);
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.TrainingName, RaiseTraining.TrainingName);
                sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.TrainingNameOther, RaiseTraining.TrainingNameOther);
                sqlParam[5] = objCommand.Parameters.AddWithValue(SPParameter.Quarter, RaiseTraining.Quarter);
                sqlParam[6] = objCommand.Parameters.AddWithValue(SPParameter.NoOfParticipant, RaiseTraining.NoOfParticipant);
                sqlParam[7] = objCommand.Parameters.AddWithValue(SPParameter.Category, RaiseTraining.Category);
                sqlParam[8] = objCommand.Parameters.AddWithValue(SPParameter.RequestedBy, RaiseTraining.RequestedBy);
                sqlParam[9] = objCommand.Parameters.AddWithValue(SPParameter.Priority, RaiseTraining.Priority);
                sqlParam[10] = objCommand.Parameters.AddWithValue(SPParameter.BusinessImpact, RaiseTraining.BusinessImpact);
                sqlParam[11] = objCommand.Parameters.AddWithValue(SPParameter.Comments, RaiseTraining.Comments);
                sqlParam[12] = objCommand.Parameters.AddWithValue(SPParameter.CreatedByEmailId, RaiseTraining.CreatedByEmailId);
                if (Convert.ToInt32(RaiseTraining.TrainingType) == TechnicalTrainingID)
                {
                    sqlParam[13] = objCommand.Parameters.AddWithValue(SPParameter.OutTechnicalID, 0);
                }
                else if (Convert.ToInt32(RaiseTraining.TrainingType) == SoftSkillsTrainingID)
                {
                    sqlParam[13] = objCommand.Parameters.AddWithValue(SPParameter.OutSoftSkillsID, 0);
                }
                sqlParam[13].Direction = ParameterDirection.Output;
                
                int Raise = objCommand.ExecuteNonQuery();
                int RaiseTrainingID = Convert.ToInt32(sqlParam[13].Value);

                return RaiseTrainingID;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, SAVE, EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        /// </summary>
        /// This method is used for saving the KSS Training details
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        public int SaveKSS(BusinessEntities.RaiseTrainingRequest RaiseTraining)
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;
            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TNI_InsertUpdateKSSTraining, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[11];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.RaiseID, RaiseTraining.RaiseID);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.TrainingType, RaiseTraining.TrainingType);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.TrainingStatus, RaiseTraining.TrainingStatus);
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.Type, RaiseTraining.Type);
                sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.Topic, RaiseTraining.Topic);
                sqlParam[5] = objCommand.Parameters.AddWithValue(SPParameter.Agenda, RaiseTraining.Agenda);
                sqlParam[6] = objCommand.Parameters.AddWithValue(SPParameter.Presenter, RaiseTraining.PresenterID);
                sqlParam[7] = objCommand.Parameters.AddWithValue(SPParameter.Date, RaiseTraining.Date);
                sqlParam[8] = objCommand.Parameters.AddWithValue(SPParameter.Comments, RaiseTraining.Comments);
                sqlParam[9] = objCommand.Parameters.AddWithValue(SPParameter.CreatedByEmailId, RaiseTraining.CreatedByEmailId);
                sqlParam[10] = objCommand.Parameters.AddWithValue(SPParameter.OutKSSID, 0);
                sqlParam[10].Direction = ParameterDirection.Output;

                int Raise = objCommand.ExecuteNonQuery();
                int RaiseKSSID = Convert.ToInt32(sqlParam[10].Value);

                return RaiseKSSID;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, SAVEKSS, EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        /// </summary>
        /// This method is used for saving the Seminars Training details
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        public int SaveSeminars(BusinessEntities.RaiseTrainingRequest RaiseTraining)
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;
            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TNI_InsertUpdateSeminarsTraining, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[11];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.RaiseID, RaiseTraining.RaiseID);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.TrainingType, RaiseTraining.TrainingType);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.TrainingStatus, RaiseTraining.TrainingStatus);
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.SeminarsName, RaiseTraining.SeminarsName);
                sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.Date, RaiseTraining.Date);
                sqlParam[5] = objCommand.Parameters.AddWithValue(SPParameter.RequestedBy, RaiseTraining.RequestedBy);
                sqlParam[6] = objCommand.Parameters.AddWithValue(SPParameter.NameOfParticipant, RaiseTraining.NameOfParticipantID);
                sqlParam[7] = objCommand.Parameters.AddWithValue(SPParameter.URL, RaiseTraining.URL);
                sqlParam[8] = objCommand.Parameters.AddWithValue(SPParameter.Comments, RaiseTraining.Comments);
                sqlParam[9] = objCommand.Parameters.AddWithValue(SPParameter.CreatedByEmailId, RaiseTraining.CreatedByEmailId);
                sqlParam[10] = objCommand.Parameters.AddWithValue(SPParameter.OutSeminarsID, 0);
                sqlParam[10].Direction = ParameterDirection.Output;

                int Raise = objCommand.ExecuteNonQuery();
                int RaiseKSSID = Convert.ToInt32(sqlParam[10].Value);

                return RaiseKSSID;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, SAVESEMINARS, EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        /// <summary>
        /// Get Technical Training 
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaiseTrainingRequest GetTechnicalSoftSkills(int RaiseID, int TrainingTypeID)
        {
            DataAccessClass objGetTechnicalTraining = new DataAccessClass();
            SqlParameter[] sqlParam = new SqlParameter[1];
            try
            {
                objGetTechnicalTraining.OpenConnection(DBConstants.GetDBConnectionString());
                sqlParam[0] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                sqlParam[0].Value = RaiseID;
                ds = new DataSet();
                if (TrainingTypeID == CommonConstants.TechnicalTrainingID)
                {
                    ds = objGetTechnicalTraining.GetDataSet(SPNames.TNI_GetTechnicalTraining, sqlParam);
                }
                else if (TrainingTypeID == CommonConstants.SoftSkillsTrainingID)
                {
                    ds = objGetTechnicalTraining.GetDataSet(SPNames.TNI_GetSoftSkillsTraining, sqlParam);
                }
                RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
                if (ds.Tables[0].Rows.Count != CommonConstants.DefaultFlagZero)
                {
                    RaiseTrainingCollection.RaiseID = Convert.ToInt32(ds.Tables[0].Rows[0][DbTableColumn.RaiseID].ToString());
                    RaiseTrainingCollection.TrainingType = ds.Tables[0].Rows[0][DbTableColumn.TrainingType].ToString();
                    RaiseTrainingCollection.TrainingName = ds.Tables[0].Rows[0][DbTableColumn.TrainingName].ToString();
                    RaiseTrainingCollection.TrainingNameOther = ds.Tables[0].Rows[0][DbTableColumn.TrainingNameOther].ToString();
                    RaiseTrainingCollection.Quarter = ds.Tables[0].Rows[0][DbTableColumn.Quarter].ToString();
                    RaiseTrainingCollection.NoOfParticipant = ds.Tables[0].Rows[0][DbTableColumn.NoOfParticipant].ToString();
                    RaiseTrainingCollection.Category = ds.Tables[0].Rows[0][DbTableColumn.Category].ToString();
                    RaiseTrainingCollection.RequestedBy = ds.Tables[0].Rows[0][DbTableColumn.RequestedBy].ToString();
                    RaiseTrainingCollection.Priority = ds.Tables[0].Rows[0][DbTableColumn.Priority].ToString();
                    RaiseTrainingCollection.BusinessImpact = ds.Tables[0].Rows[0][DbTableColumn.BusinessImpact].ToString();
                    RaiseTrainingCollection.Comments = ds.Tables[0].Rows[0][DbTableColumn.Comments].ToString();
                    RaiseTrainingCollection.Status = ds.Tables[0].Rows[0][DbTableColumn.Status].ToString();
                    RaiseTrainingCollection.StatusName = ds.Tables[0].Rows[0][DbTableColumn.StatusName].ToString();
                }
                return RaiseTrainingCollection;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, FunctionGetTechnicalTraining, EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTechnicalTraining.CloseConncetion();
            }
        }

        /// <summary>
        /// Retrive Technical Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection ViewTechnicalTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining, BusinessEntities.ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount)
        {
            DataAccessClass objGetTechnicalTraining = new DataAccessClass();
            raveHRCollection = new BusinessEntities.RaveHRCollection();
            SqlParameter[] sqlParam = new SqlParameter[12];
            try
            {
                objGetTechnicalTraining.OpenConnection(DBConstants.GetDBConnectionString());
                sqlParam[0] = new SqlParameter(SPParameter.pageNum, SqlDbType.Int);
                if (objParameter.PageNumber == CommonConstants.ZERO)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objParameter.PageNumber;
                
                sqlParam[1] = new SqlParameter(SPParameter.pageSize, SqlDbType.Int);
                if (objParameter.PageSize == CommonConstants.ZERO)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objParameter.PageSize;

                sqlParam[2] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                if (objParameter.SortExpressionAndDirection == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objParameter.SortExpressionAndDirection;

                sqlParam[3] = new SqlParameter(SPParameter.UserEmpID, SqlDbType.Int);
                sqlParam[3].Value = RaiseTraining.UserEmpId;
                sqlParam[4] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                sqlParam[4].Value = RaiseTraining.RaiseID;
                sqlParam[5] = new SqlParameter(SPParameter.TrainingType, SqlDbType.Int);
                sqlParam[5].Value = RaiseTraining.TrainingType;
                sqlParam[6] = new SqlParameter(SPParameter.Priority, SqlDbType.Int);
                sqlParam[6].Value = RaiseTraining.Priority;
                sqlParam[7] = new SqlParameter(SPParameter.Status, SqlDbType.Int);
                sqlParam[7].Value = RaiseTraining.Status;
                sqlParam[8] = new SqlParameter(SPParameter.RequestedBy, SqlDbType.Int);
                sqlParam[8].Value = RaiseTraining.RequestedBy;
                sqlParam[9] = new SqlParameter(SPParameter.Quarter, SqlDbType.Int);
                sqlParam[9].Value = RaiseTraining.Quarter;
                sqlParam[10] = new SqlParameter(SPParameter.pageCount, SqlDbType.Int);
                sqlParam[10].Direction = ParameterDirection.Output;
                sqlParam[11] = new SqlParameter(SPParameter.CurrentIndexCount, SqlDbType.Int);
                sqlParam[11].Direction = ParameterDirection.Output;

                DataSet ds = objGetTechnicalTraining.GetDataSet(SPNames.TNI_ViewTechnicalTraining, sqlParam);

                pageCount = Convert.ToInt32(sqlParam[10].Value);
                CurrentIndexCount = Convert.ToInt32(sqlParam[11].Value);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
                    RaiseTrainingCollection.SerialNo = Convert.ToInt32(dr[DbTableColumn.SrNo].ToString());
                    RaiseTrainingCollection.RaiseID = Convert.ToInt32(dr[DbTableColumn.RaiseID].ToString());
                    RaiseTrainingCollection.TrainingType = dr[DbTableColumn.TrainingType].ToString();
                    RaiseTrainingCollection.TrainingName = dr[DbTableColumn.TrainingName].ToString();
                    RaiseTrainingCollection.TrainingNameOther = dr[DbTableColumn.TrainingNameOther].ToString();
                    RaiseTrainingCollection.Quarter = dr[DbTableColumn.Quarter].ToString();
                    RaiseTrainingCollection.NoOfParticipant = dr[DbTableColumn.NoOfParticipant].ToString();
                    RaiseTrainingCollection.Category = dr[DbTableColumn.Category].ToString();
                    RaiseTrainingCollection.RequestedBy = dr[DbTableColumn.RequestedBy].ToString();
                    RaiseTrainingCollection.RequestedByID = Convert.ToInt32(dr[DbTableColumn.RequestedByID]);
                    RaiseTrainingCollection.Priority = dr[DbTableColumn.Priority].ToString();
                    RaiseTrainingCollection.BusinessImpact = dr[DbTableColumn.BusinessImpact].ToString();
                    RaiseTrainingCollection.Comments = dr[DbTableColumn.Comments].ToString();
                    RaiseTrainingCollection.Status = dr[DbTableColumn.Status].ToString();
                    RaiseTrainingCollection.CreatedDate = Convert.ToDateTime(dr[DbTableColumn.CreatedByDate].ToString());
                    
                    raveHRCollection.Add(RaiseTrainingCollection);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, FunctionViewTechnicalTraining, EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTechnicalTraining.CloseConncetion();

            }
            // return Collection
            return raveHRCollection;
        }

        /// <summary>
        /// Retrive SoftSkills Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection ViewSoftSkillsTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining, BusinessEntities.ParameterCriteria objParameter, ref int pageCount)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            raveHRCollection = new BusinessEntities.RaveHRCollection();
            SqlParameter[] sqlParam = new SqlParameter[11];
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                sqlParam[0] = new SqlParameter(SPParameter.pageNum, SqlDbType.Int);
                if (objParameter.PageNumber == CommonConstants.ZERO)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objParameter.PageNumber;

                sqlParam[1] = new SqlParameter(SPParameter.pageSize, SqlDbType.Int);
                if (objParameter.PageSize == CommonConstants.ZERO)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objParameter.PageSize;

                sqlParam[2] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                if (objParameter.SortExpressionAndDirection == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objParameter.SortExpressionAndDirection;

                sqlParam[3] = new SqlParameter(SPParameter.UserEmpID, SqlDbType.Int);
                sqlParam[3].Value = RaiseTraining.UserEmpId;
                sqlParam[4] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                sqlParam[4].Value = RaiseTraining.RaiseID;
                sqlParam[5] = new SqlParameter(SPParameter.TrainingType, SqlDbType.Int);
                sqlParam[5].Value = RaiseTraining.TrainingType;
                sqlParam[6] = new SqlParameter(SPParameter.Priority, SqlDbType.Int);
                sqlParam[6].Value = RaiseTraining.Priority;
                sqlParam[7] = new SqlParameter(SPParameter.Status, SqlDbType.Int);
                sqlParam[7].Value = RaiseTraining.Status;
                sqlParam[8] = new SqlParameter(SPParameter.RequestedBy, SqlDbType.Int);
                sqlParam[8].Value = RaiseTraining.RequestedBy;
                sqlParam[9] = new SqlParameter(SPParameter.Quarter, SqlDbType.Int);
                sqlParam[9].Value = RaiseTraining.Quarter;
                sqlParam[10] = new SqlParameter(SPParameter.pageCount, SqlDbType.Int);
                sqlParam[10].Direction = ParameterDirection.Output;

                DataSet ds = objGetTraining.GetDataSet(SPNames.TNI_ViewSoftSkillsTraining, sqlParam);

                pageCount = Convert.ToInt32(sqlParam[10].Value);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
                    RaiseTrainingCollection.SerialNo = Convert.ToInt32(dr[DbTableColumn.SrNo].ToString());
                    RaiseTrainingCollection.RaiseID = Convert.ToInt32(dr[DbTableColumn.RaiseID].ToString());
                    RaiseTrainingCollection.TrainingType = dr[DbTableColumn.TrainingType].ToString();
                    RaiseTrainingCollection.TrainingName = dr[DbTableColumn.TrainingName].ToString();
                    RaiseTrainingCollection.TrainingNameOther = dr[DbTableColumn.TrainingNameOther].ToString();
                    RaiseTrainingCollection.Quarter = dr[DbTableColumn.Quarter].ToString();
                    RaiseTrainingCollection.NoOfParticipant = dr[DbTableColumn.NoOfParticipant].ToString();
                    RaiseTrainingCollection.Category = dr[DbTableColumn.Category].ToString();
                    RaiseTrainingCollection.RequestedBy = dr[DbTableColumn.RequestedBy].ToString();
                    RaiseTrainingCollection.RequestedByID = Convert.ToInt32(dr[DbTableColumn.RequestedByID]);
                    RaiseTrainingCollection.Priority = dr[DbTableColumn.Priority].ToString();
                    RaiseTrainingCollection.BusinessImpact = dr[DbTableColumn.BusinessImpact].ToString();
                    RaiseTrainingCollection.Comments = dr[DbTableColumn.Comments].ToString();
                    RaiseTrainingCollection.Status = dr[DbTableColumn.Status].ToString();
                    RaiseTrainingCollection.CreatedDate = Convert.ToDateTime(dr[DbTableColumn.CreatedByDate].ToString());

                    raveHRCollection.Add(RaiseTrainingCollection);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, FunctionViewSoftSkillsTraining, EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();

            }
            // return Collection
            return raveHRCollection;
        }
        
        /// </summary>
        /// This method is used for deleteing the Techinical Training details
        /// <param name="RaiseTraining"></param>
        /// <returns>RaiseTechnicalID</returns>
        public string DeleteTechnicalSoftSkillsTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining, int TrainingTypeID)
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;
            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                if (TrainingTypeID == CommonConstants.TechnicalTrainingID)
                {
                    objCommand = new SqlCommand(SPNames.TNI_DeleteTechnicalTraining, objConnection);
                }
                else if (TrainingTypeID == CommonConstants.SoftSkillsTrainingID)
                {
                    objCommand = new SqlCommand(SPNames.TNI_DeleteSoftSkillsTraining, objConnection);
                }
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.Add(SPParameter.RaiseID, SqlDbType.Int).Value = RaiseTraining.RaiseID;
                objCommand.Parameters.Add(SPParameter.Status, SqlDbType.Int).Value = RaiseTraining.Status;
                objCommand.Parameters.Add(SPParameter.UserEmpID, SqlDbType.Int).Value = RaiseTraining.UserEmpId;
                objCommand.Parameters.Add(SPParameter.TrainingName, SqlDbType.NChar, 100);
                objCommand.Parameters[SPParameter.TrainingName].Direction = ParameterDirection.Output;

                objCommand.ExecuteNonQuery();
                return objCommand.Parameters[SPParameter.TrainingName].Value.ToString().Trim();
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, FunctionDeleteTechnicalSoftSkillsTraining, EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        /// </summary>
        /// This method is used for deleteing the KSS Training details
        /// <param name="RaiseTraining"></param>
        /// <returns>RaiseKSSID</returns>
        public string DeleteKSSTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining)
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;
            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TNI_DeleteKSSTraining, objConnection);

                objCommand.CommandType = CommandType.StoredProcedure;
                
                objCommand.Parameters.Add(SPParameter.RaiseID, SqlDbType.Int).Value = RaiseTraining.RaiseID;
                objCommand.Parameters.Add(SPParameter.Status, SqlDbType.Int).Value = RaiseTraining.Status;
                objCommand.Parameters.Add(SPParameter.UserEmpID, SqlDbType.Int).Value = RaiseTraining.UserEmpId;
                objCommand.Parameters.Add(SPParameter.TrainingName, SqlDbType.NChar, 100);
                objCommand.Parameters[SPParameter.TrainingName].Direction = ParameterDirection.Output;

                objCommand.ExecuteNonQuery();
                return objCommand.Parameters[SPParameter.TrainingName].Value.ToString().Trim();
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, FunctionDeleteKSSTraining, EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        /// </summary>
        /// This method is used for deleteing the Seminars Training details
        /// <param name="RaiseTraining"></param>
        /// <returns>RaiseSeminarsID</returns>
        public string DeleteSeminarsTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining)
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;
            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TNI_DeleteSeminarsTraining, objConnection);

                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.Add(SPParameter.RaiseID, SqlDbType.Int).Value = RaiseTraining.RaiseID;
                objCommand.Parameters.Add(SPParameter.Status, SqlDbType.Int).Value = RaiseTraining.Status;
                objCommand.Parameters.Add(SPParameter.UserEmpID, SqlDbType.Int).Value = RaiseTraining.UserEmpId;
                objCommand.Parameters.Add(SPParameter.TrainingName, SqlDbType.NChar, 100);
                objCommand.Parameters[SPParameter.TrainingName].Direction = ParameterDirection.Output;

                objCommand.ExecuteNonQuery();
                return objCommand.Parameters[SPParameter.TrainingName].Value.ToString().Trim();
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, FunctionDeleteSeminarsTraining, EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        /// <summary>
        /// Retrive KSS Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection ViewKSSTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining, BusinessEntities.ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            raveHRCollection = new BusinessEntities.RaveHRCollection();
            SqlParameter[] sqlParam = new SqlParameter[9];
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                sqlParam[0] = new SqlParameter(SPParameter.pageNum, SqlDbType.Int);
                if (objParameter.PageNumber == CommonConstants.ZERO)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objParameter.PageNumber;

                sqlParam[1] = new SqlParameter(SPParameter.pageSize, SqlDbType.Int);
                if (objParameter.PageSize == CommonConstants.ZERO)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objParameter.PageSize;

                sqlParam[2] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                if (objParameter.SortExpressionAndDirection == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objParameter.SortExpressionAndDirection;

                sqlParam[3] = new SqlParameter(SPParameter.UserEmpID, SqlDbType.Int);
                sqlParam[3].Value = RaiseTraining.UserEmpId;

                sqlParam[4] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                sqlParam[4].Value = RaiseTraining.RaiseID;

                sqlParam[5] = new SqlParameter(SPParameter.TrainingType, SqlDbType.Int);
                sqlParam[5].Value = RaiseTraining.TrainingType;

                sqlParam[6] = new SqlParameter(SPParameter.Status, SqlDbType.Int);
                sqlParam[6].Value = RaiseTraining.Status;

                sqlParam[7] = new SqlParameter(SPParameter.pageCount, SqlDbType.Int);
                sqlParam[7].Direction = ParameterDirection.Output;

                sqlParam[8] = new SqlParameter(SPParameter.CurrentIndexCount, SqlDbType.Int);
                sqlParam[8].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds.Clear();
                ds = objGetTraining.GetDataSet(SPNames.TNI_ViewKSSTraining, sqlParam);

                pageCount = Convert.ToInt32(sqlParam[7].Value);
                CurrentIndexCount = Convert.ToInt32(sqlParam[8].Value);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
                    RaiseTrainingCollection.SerialNo = Convert.ToInt32(dr[DbTableColumn.SrNo].ToString());
                    RaiseTrainingCollection.RaiseID = Convert.ToInt32(dr[DbTableColumn.RaiseID].ToString());
                    RaiseTrainingCollection.TrainingType = dr[DbTableColumn.TrainingType].ToString();
                    RaiseTrainingCollection.Type = dr[DbTableColumn.Type].ToString();
                    RaiseTrainingCollection.Topic = dr[DbTableColumn.Topic].ToString();
                    RaiseTrainingCollection.Agenda = dr[DbTableColumn.Agenda].ToString();
                    RaiseTrainingCollection.RequestedBy = dr[DbTableColumn.RequestedBy].ToString();
                    RaiseTrainingCollection.Presenter = dr[DbTableColumn.Presenter].ToString();
                    RaiseTrainingCollection.Date = Convert.ToDateTime(dr[DbTableColumn.Date].ToString());
                    RaiseTrainingCollection.Comments = dr[DbTableColumn.Comments].ToString();
                    RaiseTrainingCollection.Status = dr[DbTableColumn.Status].ToString();
                    RaiseTrainingCollection.CreatedDate = Convert.ToDateTime(dr[DbTableColumn.CreatedByDate].ToString());

                    raveHRCollection.Add(RaiseTrainingCollection);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, FunctionViewKSSTraining, EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();

            }
            // return Collection
            return raveHRCollection;
        }

        /// <summary>
        /// Get KSS Training 
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaiseTrainingRequest GetKSSTraining(int RaiseID)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            SqlParameter[] sqlParam = new SqlParameter[1];
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                sqlParam[0] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                sqlParam[0].Value = RaiseID;
                ds = new DataSet();

                ds = objGetTraining.GetDataSet(SPNames.TNI_GetKSSTraining, sqlParam);

                RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
                RaiseTrainingCollection.RaiseID = Convert.ToInt32(ds.Tables[0].Rows[0][DbTableColumn.RaiseID].ToString());
                RaiseTrainingCollection.TrainingType = ds.Tables[0].Rows[0][DbTableColumn.TrainingType].ToString();
                RaiseTrainingCollection.Type = ds.Tables[0].Rows[0][DbTableColumn.Type].ToString();
                RaiseTrainingCollection.Topic = ds.Tables[0].Rows[0][DbTableColumn.Topic].ToString();
                RaiseTrainingCollection.Agenda = ds.Tables[0].Rows[0][DbTableColumn.Agenda].ToString();
                RaiseTrainingCollection.Presenter = ds.Tables[0].Rows[0][DbTableColumn.Presenter].ToString();
                RaiseTrainingCollection.PresenterID = ds.Tables[0].Rows[0][DbTableColumn.PresenterID].ToString();
                RaiseTrainingCollection.Date = Convert.ToDateTime(ds.Tables[0].Rows[0][DbTableColumn.Date].ToString());
                RaiseTrainingCollection.Comments = ds.Tables[0].Rows[0][DbTableColumn.Comments].ToString();
                RaiseTrainingCollection.Status = ds.Tables[0].Rows[0][DbTableColumn.Status].ToString();
                return RaiseTrainingCollection;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, FunctionGetKSSTraining, EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }
        }

        /// <summary>
        /// Retrive Seminars Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection ViewSeminarsTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining, BusinessEntities.ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            raveHRCollection = new BusinessEntities.RaveHRCollection();
            SqlParameter[] sqlParam = new SqlParameter[10];
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                sqlParam[0] = new SqlParameter(SPParameter.pageNum, SqlDbType.Int);
                if (objParameter.PageNumber == CommonConstants.ZERO)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objParameter.PageNumber;

                sqlParam[1] = new SqlParameter(SPParameter.pageSize, SqlDbType.Int);
                if (objParameter.PageSize == CommonConstants.ZERO)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objParameter.PageSize;

                sqlParam[2] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                if (objParameter.SortExpressionAndDirection == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objParameter.SortExpressionAndDirection;

                sqlParam[3] = new SqlParameter(SPParameter.UserEmpID, SqlDbType.Int);
                sqlParam[3].Value = RaiseTraining.UserEmpId;

                sqlParam[4] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                sqlParam[4].Value = RaiseTraining.RaiseID;

                sqlParam[5] = new SqlParameter(SPParameter.TrainingType, SqlDbType.Int);
                sqlParam[5].Value = RaiseTraining.TrainingType;

                sqlParam[6] = new SqlParameter(SPParameter.Status, SqlDbType.Int);
                sqlParam[6].Value = RaiseTraining.Status;

                sqlParam[7] = new SqlParameter(SPParameter.RequestedBy, SqlDbType.Int);
                sqlParam[7].Value = RaiseTraining.RequestedBy;

                sqlParam[8] = new SqlParameter(SPParameter.pageCount, SqlDbType.Int);
                sqlParam[8].Direction = ParameterDirection.Output;

                sqlParam[9] = new SqlParameter(SPParameter.CurrentIndexCount, SqlDbType.Int);
                sqlParam[9].Direction = ParameterDirection.Output;

                DataSet ds = objGetTraining.GetDataSet(SPNames.TNI_ViewSeminarsTraining, sqlParam);

                pageCount = Convert.ToInt32(sqlParam[8].Value);
                CurrentIndexCount = Convert.ToInt32(sqlParam[9].Value);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
                    RaiseTrainingCollection.SerialNo = Convert.ToInt32(dr[DbTableColumn.SrNo].ToString());
                    RaiseTrainingCollection.RaiseID = Convert.ToInt32(dr[DbTableColumn.RaiseID].ToString());
                    RaiseTrainingCollection.TrainingType = dr[DbTableColumn.TrainingType].ToString();
                    RaiseTrainingCollection.SeminarsName = dr[DbTableColumn.SeminarsName].ToString();
                    RaiseTrainingCollection.Date = Convert.ToDateTime(dr[DbTableColumn.Date].ToString());
                    RaiseTrainingCollection.NameOfParticipant = dr[DbTableColumn.NameOfParticipant].ToString();
                    RaiseTrainingCollection.URL = dr[DbTableColumn.URL].ToString();
                    RaiseTrainingCollection.RequestedBy = dr[DbTableColumn.RequestedBy].ToString();
                    RaiseTrainingCollection.RequestedByID = Convert.ToInt32(dr[DbTableColumn.RequestedByID]);
                    RaiseTrainingCollection.Comments = dr[DbTableColumn.Comments].ToString();
                    RaiseTrainingCollection.Status = dr[DbTableColumn.Status].ToString();
                    RaiseTrainingCollection.CreatedDate = Convert.ToDateTime(dr[DbTableColumn.CreatedByDate].ToString());

                    raveHRCollection.Add(RaiseTrainingCollection);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, FunctionViewSeminarsTraining, EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();

            }
            // return Collection
            return raveHRCollection;
        }

        /// <summary>
        /// Get Seminars Training 
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaiseTrainingRequest GetSeminarsTraining(int RaiseID)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            SqlParameter[] sqlParam = new SqlParameter[1];
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                sqlParam[0] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                sqlParam[0].Value = RaiseID;
                ds = new DataSet();

                ds = objGetTraining.GetDataSet(SPNames.TNI_GetSeminarsTraining, sqlParam);

                RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
                RaiseTrainingCollection.RaiseID = Convert.ToInt32(ds.Tables[0].Rows[0][DbTableColumn.RaiseID].ToString());
                RaiseTrainingCollection.TrainingType = ds.Tables[0].Rows[0][DbTableColumn.TrainingType].ToString();
                RaiseTrainingCollection.SeminarsName = ds.Tables[0].Rows[0][DbTableColumn.SeminarsName].ToString();
                RaiseTrainingCollection.Date = Convert.ToDateTime(ds.Tables[0].Rows[0][DbTableColumn.Date].ToString());
                RaiseTrainingCollection.URL = ds.Tables[0].Rows[0][DbTableColumn.URL].ToString();
                RaiseTrainingCollection.RequestedBy = ds.Tables[0].Rows[0][DbTableColumn.RequestedBy].ToString();
                RaiseTrainingCollection.NameOfParticipantID = ds.Tables[0].Rows[0][DbTableColumn.NameOfParticipantID].ToString();
                RaiseTrainingCollection.NameOfParticipant = ds.Tables[0].Rows[0][DbTableColumn.NameOfParticipant].ToString();
                RaiseTrainingCollection.Comments = ds.Tables[0].Rows[0][DbTableColumn.Comments].ToString();
                RaiseTrainingCollection.Status = ds.Tables[0].Rows[0][DbTableColumn.Status].ToString();
                RaiseTrainingCollection.StatusName = ds.Tables[0].Rows[0][DbTableColumn.StatusName].ToString();
                return RaiseTrainingCollection;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, FunctionGetSeminarsTraining, EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }
        }


        ////Approve Reject Training Request Method

        /// <summary>
        /// Retrive KSS Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection ApproveRejectViewKSSTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining, BusinessEntities.ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            raveHRCollection = new BusinessEntities.RaveHRCollection();
            SqlParameter[] sqlParam = new SqlParameter[9];
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                sqlParam[0] = new SqlParameter(SPParameter.pageNum, SqlDbType.Int);
                if (objParameter.PageNumber == CommonConstants.ZERO)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objParameter.PageNumber;

                sqlParam[1] = new SqlParameter(SPParameter.pageSize, SqlDbType.Int);
                if (objParameter.PageSize == CommonConstants.ZERO)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objParameter.PageSize;

                sqlParam[2] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                if (objParameter.SortExpressionAndDirection == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objParameter.SortExpressionAndDirection;

                sqlParam[3] = new SqlParameter(SPParameter.UserEmpID, SqlDbType.Int);
                sqlParam[3].Value = RaiseTraining.UserEmpId;

                sqlParam[4] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                sqlParam[4].Value = RaiseTraining.RaiseID;

                sqlParam[5] = new SqlParameter(SPParameter.TrainingType, SqlDbType.Int);
                sqlParam[5].Value = RaiseTraining.TrainingType;

                sqlParam[6] = new SqlParameter(SPParameter.Status, SqlDbType.Int);
                sqlParam[6].Value = RaiseTraining.Status;

                sqlParam[7] = new SqlParameter(SPParameter.pageCount, SqlDbType.Int);
                sqlParam[7].Direction = ParameterDirection.Output;

                sqlParam[8] = new SqlParameter(SPParameter.CurrentIndexCount, SqlDbType.Int);
                sqlParam[8].Direction = ParameterDirection.Output;

                DataSet ds = objGetTraining.GetDataSet(SPNames.TNI_ApproveRejectViewKSSTraining, sqlParam);

                pageCount = Convert.ToInt32(sqlParam[7].Value);
                CurrentIndexCount = Convert.ToInt32(sqlParam[8].Value);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
                    RaiseTrainingCollection.SerialNo = Convert.ToInt32(dr[DbTableColumn.SrNo].ToString());
                    RaiseTrainingCollection.RaiseID = Convert.ToInt32(dr[DbTableColumn.RaiseID].ToString());
                    RaiseTrainingCollection.TrainingType = dr[DbTableColumn.TrainingType].ToString();
                    RaiseTrainingCollection.Type = dr[DbTableColumn.Type].ToString();
                    RaiseTrainingCollection.Topic = dr[DbTableColumn.Topic].ToString();
                    RaiseTrainingCollection.Agenda = dr[DbTableColumn.Agenda].ToString();
                    RaiseTrainingCollection.RequestedBy = dr[DbTableColumn.RequestedBy].ToString();
                    RaiseTrainingCollection.Presenter = dr[DbTableColumn.Presenter].ToString();
                    RaiseTrainingCollection.Date = Convert.ToDateTime(dr[DbTableColumn.Date].ToString());
                    RaiseTrainingCollection.Comments = dr[DbTableColumn.Comments].ToString();
                    RaiseTrainingCollection.Status = dr[DbTableColumn.Status].ToString();
                    RaiseTrainingCollection.CreatedDate = Convert.ToDateTime(dr[DbTableColumn.CreatedByDate].ToString());

                    raveHRCollection.Add(RaiseTrainingCollection);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "FunctionApproveRejectViewKSSTrainingViewKSSTraining", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();

            }
            // return Collection
            return raveHRCollection;
        }

        /// <summary>
        /// Retrive Technical Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection ApproveRejectViewTechnicalTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining, BusinessEntities.ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount)
        {
            DataAccessClass objGetTechnicalTraining = new DataAccessClass();
            raveHRCollection = new BusinessEntities.RaveHRCollection();
            SqlParameter[] sqlParam = new SqlParameter[12];
            try
            {
                objGetTechnicalTraining.OpenConnection(DBConstants.GetDBConnectionString());
                sqlParam[0] = new SqlParameter(SPParameter.pageNum, SqlDbType.Int);
                if (objParameter.PageNumber == CommonConstants.ZERO)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objParameter.PageNumber;

                sqlParam[1] = new SqlParameter(SPParameter.pageSize, SqlDbType.Int);
                if (objParameter.PageSize == CommonConstants.ZERO)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objParameter.PageSize;

                sqlParam[2] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                if (objParameter.SortExpressionAndDirection == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objParameter.SortExpressionAndDirection;

                sqlParam[3] = new SqlParameter(SPParameter.UserEmpID, SqlDbType.Int);
                sqlParam[3].Value = RaiseTraining.UserEmpId;
                sqlParam[4] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                sqlParam[4].Value = RaiseTraining.RaiseID;
                sqlParam[5] = new SqlParameter(SPParameter.TrainingType, SqlDbType.Int);
                sqlParam[5].Value = RaiseTraining.TrainingType;
                sqlParam[6] = new SqlParameter(SPParameter.Priority, SqlDbType.Int);
                sqlParam[6].Value = RaiseTraining.Priority;
                sqlParam[7] = new SqlParameter(SPParameter.Status, SqlDbType.Int);
                sqlParam[7].Value = RaiseTraining.Status;
                sqlParam[8] = new SqlParameter(SPParameter.RequestedBy, SqlDbType.Int);
                sqlParam[8].Value = RaiseTraining.RequestedBy;
                sqlParam[9] = new SqlParameter(SPParameter.Quarter, SqlDbType.Int);
                sqlParam[9].Value = RaiseTraining.Quarter;
                sqlParam[10] = new SqlParameter(SPParameter.pageCount, SqlDbType.Int);
                sqlParam[10].Direction = ParameterDirection.Output;
                sqlParam[11] = new SqlParameter(SPParameter.CurrentIndexCount, SqlDbType.Int);
                sqlParam[11].Direction = ParameterDirection.Output;

                DataSet ds = objGetTechnicalTraining.GetDataSet(SPNames.TNI_ApproveRejectViewTechnicalTraining, sqlParam);

                pageCount = Convert.ToInt32(sqlParam[10].Value);
                CurrentIndexCount = Convert.ToInt32(sqlParam[11].Value);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
                    RaiseTrainingCollection.SerialNo = Convert.ToInt32(dr[DbTableColumn.SrNo].ToString());
                    RaiseTrainingCollection.RaiseID = Convert.ToInt32(dr[DbTableColumn.RaiseID].ToString());
                    RaiseTrainingCollection.TrainingType = dr[DbTableColumn.TrainingType].ToString();
                    RaiseTrainingCollection.TrainingName = dr[DbTableColumn.TrainingName].ToString();
                    RaiseTrainingCollection.TrainingNameOther = dr[DbTableColumn.TrainingNameOther].ToString();
                    RaiseTrainingCollection.Quarter = dr[DbTableColumn.Quarter].ToString();
                    RaiseTrainingCollection.NoOfParticipant = dr[DbTableColumn.NoOfParticipant].ToString();
                    RaiseTrainingCollection.Category = dr[DbTableColumn.Category].ToString();
                    RaiseTrainingCollection.RequestedBy = dr[DbTableColumn.RequestedBy].ToString();
                    RaiseTrainingCollection.RequestedByID = Convert.ToInt32(dr[DbTableColumn.RequestedByID]);
                    RaiseTrainingCollection.Priority = dr[DbTableColumn.Priority].ToString();
                    RaiseTrainingCollection.BusinessImpact = dr[DbTableColumn.BusinessImpact].ToString();
                    RaiseTrainingCollection.Comments = dr[DbTableColumn.Comments].ToString();
                    RaiseTrainingCollection.Status = dr[DbTableColumn.Status].ToString();
                    RaiseTrainingCollection.CreatedDate = Convert.ToDateTime(dr[DbTableColumn.CreatedByDate].ToString());

                    raveHRCollection.Add(RaiseTrainingCollection);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "FunctionApproveRejectViewTechnicalTraining", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTechnicalTraining.CloseConncetion();

            }
            // return Collection
            return raveHRCollection;
        }

        /// <summary>
        /// Retrive SoftSkills Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection ApproveRejectViewSoftSkillsTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining, BusinessEntities.ParameterCriteria objParameter, ref int pageCount)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            raveHRCollection = new BusinessEntities.RaveHRCollection();
            SqlParameter[] sqlParam = new SqlParameter[11];
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                sqlParam[0] = new SqlParameter(SPParameter.pageNum, SqlDbType.Int);
                if (objParameter.PageNumber == CommonConstants.ZERO)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objParameter.PageNumber;

                sqlParam[1] = new SqlParameter(SPParameter.pageSize, SqlDbType.Int);
                if (objParameter.PageSize == CommonConstants.ZERO)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objParameter.PageSize;

                sqlParam[2] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                if (objParameter.SortExpressionAndDirection == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objParameter.SortExpressionAndDirection;

                sqlParam[3] = new SqlParameter(SPParameter.UserEmpID, SqlDbType.Int);
                sqlParam[3].Value = RaiseTraining.UserEmpId;
                sqlParam[4] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                sqlParam[4].Value = RaiseTraining.RaiseID;
                sqlParam[5] = new SqlParameter(SPParameter.TrainingType, SqlDbType.Int);
                sqlParam[5].Value = RaiseTraining.TrainingType;
                sqlParam[6] = new SqlParameter(SPParameter.Priority, SqlDbType.Int);
                sqlParam[6].Value = RaiseTraining.Priority;
                sqlParam[7] = new SqlParameter(SPParameter.Status, SqlDbType.Int);
                sqlParam[7].Value = RaiseTraining.Status;
                sqlParam[8] = new SqlParameter(SPParameter.RequestedBy, SqlDbType.Int);
                sqlParam[8].Value = RaiseTraining.RequestedBy;
                sqlParam[9] = new SqlParameter(SPParameter.Quarter, SqlDbType.Int);
                sqlParam[9].Value = RaiseTraining.Quarter;
                sqlParam[10] = new SqlParameter(SPParameter.pageCount, SqlDbType.Int);
                sqlParam[10].Direction = ParameterDirection.Output;

                DataSet ds = objGetTraining.GetDataSet(SPNames.TNI_ApproveRejectViewSoftSkillsTraining, sqlParam);

                pageCount = Convert.ToInt32(sqlParam[10].Value);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
                    RaiseTrainingCollection.SerialNo = Convert.ToInt32(dr[DbTableColumn.SrNo].ToString());
                    RaiseTrainingCollection.RaiseID = Convert.ToInt32(dr[DbTableColumn.RaiseID].ToString());
                    RaiseTrainingCollection.TrainingType = dr[DbTableColumn.TrainingType].ToString();
                    RaiseTrainingCollection.TrainingName = dr[DbTableColumn.TrainingName].ToString();
                    RaiseTrainingCollection.TrainingNameOther = dr[DbTableColumn.TrainingNameOther].ToString();
                    RaiseTrainingCollection.Quarter = dr[DbTableColumn.Quarter].ToString();
                    RaiseTrainingCollection.NoOfParticipant = dr[DbTableColumn.NoOfParticipant].ToString();
                    RaiseTrainingCollection.Category = dr[DbTableColumn.Category].ToString();
                    RaiseTrainingCollection.RequestedBy = dr[DbTableColumn.RequestedBy].ToString();
                    RaiseTrainingCollection.RequestedByID = Convert.ToInt32(dr[DbTableColumn.RequestedByID]);
                    RaiseTrainingCollection.Priority = dr[DbTableColumn.Priority].ToString();
                    RaiseTrainingCollection.BusinessImpact = dr[DbTableColumn.BusinessImpact].ToString();
                    RaiseTrainingCollection.Comments = dr[DbTableColumn.Comments].ToString();
                    RaiseTrainingCollection.Status = dr[DbTableColumn.Status].ToString();
                    RaiseTrainingCollection.CreatedDate = Convert.ToDateTime(dr[DbTableColumn.CreatedByDate].ToString());

                    raveHRCollection.Add(RaiseTrainingCollection);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "FunctionApproveRejectViewSoftSkillsTraining", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();

            }
            // return Collection
            return raveHRCollection;
        }

        /// <summary>
        /// Retrive Seminars Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection ApproveRejectViewSeminarsTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining, BusinessEntities.ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            raveHRCollection = new BusinessEntities.RaveHRCollection();
            SqlParameter[] sqlParam = new SqlParameter[10];
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                sqlParam[0] = new SqlParameter(SPParameter.pageNum, SqlDbType.Int);
                if (objParameter.PageNumber == CommonConstants.ZERO)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objParameter.PageNumber;

                sqlParam[1] = new SqlParameter(SPParameter.pageSize, SqlDbType.Int);
                if (objParameter.PageSize == CommonConstants.ZERO)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objParameter.PageSize;

                sqlParam[2] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                if (objParameter.SortExpressionAndDirection == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objParameter.SortExpressionAndDirection;

                sqlParam[3] = new SqlParameter(SPParameter.UserEmpID, SqlDbType.Int);
                sqlParam[3].Value = RaiseTraining.UserEmpId;

                sqlParam[4] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                sqlParam[4].Value = RaiseTraining.RaiseID;

                sqlParam[5] = new SqlParameter(SPParameter.TrainingType, SqlDbType.Int);
                sqlParam[5].Value = RaiseTraining.TrainingType;

                sqlParam[6] = new SqlParameter(SPParameter.Status, SqlDbType.Int);
                sqlParam[6].Value = RaiseTraining.Status;

                sqlParam[7] = new SqlParameter(SPParameter.RequestedBy, SqlDbType.Int);
                sqlParam[7].Value = RaiseTraining.RequestedBy;

                sqlParam[8] = new SqlParameter(SPParameter.pageCount, SqlDbType.Int);
                sqlParam[8].Direction = ParameterDirection.Output;

                sqlParam[9] = new SqlParameter(SPParameter.CurrentIndexCount, SqlDbType.Int);
                sqlParam[9].Direction = ParameterDirection.Output;

                DataSet ds = objGetTraining.GetDataSet(SPNames.TNI_ApproveRejectViewSeminarsTraining, sqlParam);

                pageCount = Convert.ToInt32(sqlParam[8].Value);
                CurrentIndexCount = Convert.ToInt32(sqlParam[9].Value);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
                    RaiseTrainingCollection.SerialNo = Convert.ToInt32(dr[DbTableColumn.SrNo].ToString());
                    RaiseTrainingCollection.RaiseID = Convert.ToInt32(dr[DbTableColumn.RaiseID].ToString());
                    RaiseTrainingCollection.TrainingType = dr[DbTableColumn.TrainingType].ToString();
                    RaiseTrainingCollection.SeminarsName = dr[DbTableColumn.SeminarsName].ToString();
                    RaiseTrainingCollection.Date = Convert.ToDateTime(dr[DbTableColumn.Date].ToString());
                    RaiseTrainingCollection.NameOfParticipant = dr[DbTableColumn.NameOfParticipant].ToString();
                    RaiseTrainingCollection.URL = dr[DbTableColumn.URL].ToString();
                    RaiseTrainingCollection.RequestedBy = dr[DbTableColumn.RequestedBy].ToString();
                    RaiseTrainingCollection.RequestedByID = Convert.ToInt32(dr[DbTableColumn.RequestedByID]);
                    RaiseTrainingCollection.Comments = dr[DbTableColumn.Comments].ToString();
                    RaiseTrainingCollection.Status = dr[DbTableColumn.Status].ToString();
                    RaiseTrainingCollection.CreatedDate = Convert.ToDateTime(dr[DbTableColumn.CreatedByDate].ToString());

                    raveHRCollection.Add(RaiseTrainingCollection);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "FunctionApproveRejectViewSeminarsTraining", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();

            }
            // return Collection
            return raveHRCollection;
        }

        public int SaveApproveRejectTrainingRequest(BusinessEntities.RaiseTrainingRequest RaiseTraining)
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;
            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TNI_UpdateApproveRejectTechnicalTraining, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.RaiseID, RaiseTraining.RaiseID);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.Status, RaiseTraining.Status);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.UserEmpID, RaiseTraining.UserEmpId);
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.Comments, RaiseTraining.Comments);
                sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.OutTechnicalID, 0);
                sqlParam[4].Direction = ParameterDirection.Output;

                objCommand.ExecuteNonQuery();
                return Convert.ToInt32(sqlParam[4].Value);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, SAVEKSS, EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        public string UpdateRaiseTrainingStatus(BusinessEntities.RaiseTrainingRequest RaiseTraining)
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;
            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TNI_UpdateRaiseTrainingStatus, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;

                objCommand.Parameters.Add(SPParameter.RaiseID, SqlDbType.Int).Value = RaiseTraining.RaiseID;
                objCommand.Parameters.Add(SPParameter.Status, SqlDbType.NVarChar).Value = RaiseTraining.Status;
                objCommand.Parameters.Add(SPParameter.UserEmpID, SqlDbType.Int).Value = RaiseTraining.UserEmpId;
                objCommand.Parameters.Add(SPParameter.Comments, SqlDbType.NVarChar).Value = RaiseTraining.Comments;
                objCommand.Parameters.Add(SPParameter.TrainingName, SqlDbType.NChar, 100);
                objCommand.Parameters[SPParameter.TrainingName].Direction = ParameterDirection.Output;

                objCommand.ExecuteNonQuery();
                return objCommand.Parameters[SPParameter.TrainingName].Value.ToString().Trim();
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "FunctionUpdateRaiseTrainingStatus", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        /// <summary>
        /// Edit KSS Training Details using ATG/ATM, TM, STM Group
        /// </summary>
        /// <returns>Collection</returns>
        public DataTable GetEditKSSTrainingGroup(int UserEmpID, string Flag)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            SqlParameter[] sqlParam = new SqlParameter[2];
            raveHRCollection = new BusinessEntities.RaveHRCollection();
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                sqlParam[0] = new SqlParameter(SPParameter.UserEmpID, SqlDbType.Int);
                sqlParam[0].Value = UserEmpID;
                sqlParam[1] = new SqlParameter(SPParameter.Flag, SqlDbType.VarChar);
                sqlParam[1].Value = Flag;
                DataSet ds = objGetTraining.GetDataSet(SPNames.TNI_EditKSSTraining, sqlParam);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "FunctionGetEditKSSTrainingGroup", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }
        }

        public int AccessForTrainingModule(int UserEmpId)
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;
            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TNI_AccessRightForTrainingModule, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.UserEmpID, UserEmpId);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.COUNT, 0);
                sqlParam[1].Direction = ParameterDirection.Output;

                objCommand.ExecuteNonQuery();
                return Convert.ToInt32(sqlParam[1].Value);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "FunctionAccessForTrainingModule", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        public int CheckDuplication(BusinessEntities.RaiseTrainingRequest RaiseTraining)
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;
            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TNI_CheckDuplication, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.Category, RaiseTraining.Category);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.TrainingNameOther, RaiseTraining.TrainingNameOther);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.RecordCount, 0);
                sqlParam[2].Direction = ParameterDirection.Output;

                objCommand.ExecuteNonQuery();
                return Convert.ToInt32(sqlParam[2].Value);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "FuncitonCheckDuplication", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }
        //Ishwar Patil : 19/08/2014 Start
        public DataSet GetEmailIDDetails(string UserEmpID, int Raiseid)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            SqlParameter[] sqlParam = new SqlParameter[2];
            raveHRCollection = new BusinessEntities.RaveHRCollection();
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                sqlParam[0] = new SqlParameter(SPParameter.UserEmpID, SqlDbType.NVarChar);
                sqlParam[0].Value = UserEmpID;
                sqlParam[1] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                sqlParam[1].Value = Raiseid;
                return objGetTraining.GetDataSet(SPNames.TNI_GetEmailIDDetails, sqlParam);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "FunctionGetEmailIDDetails", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }
        }
        public DataTable GetEmailIDDetailsForKSS(string UserEmpID)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            SqlParameter[] sqlParam = new SqlParameter[1];
            raveHRCollection = new BusinessEntities.RaveHRCollection();
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                sqlParam[0] = new SqlParameter(SPParameter.UserEmpID, SqlDbType.NVarChar);
                sqlParam[0].Value = UserEmpID;
                DataSet ds = objGetTraining.GetDataSet(SPNames.TNI_GetEmailIDDetailsForKSS, sqlParam);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "FunctionGetEmailIDDetails", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }

        }

        //
        public DataSet GetEmailIDForAppRejTechSoftSkill(int RaiseID)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            SqlParameter[] sqlParam = new SqlParameter[1];
            raveHRCollection = new BusinessEntities.RaveHRCollection();
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                sqlParam[0] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                sqlParam[0].Value = RaiseID;
                DataSet ds = objGetTraining.GetDataSet(SPNames.TNI_GetEmailIDForAppRejTechSoftSkill, sqlParam);
                return ds;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "FunctionGetEmailIDForAppRejTechSoftSkill", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }
        }
        //Ishwar Patil : 19/08/2014 End
        #endregion Public Method
    }
}
