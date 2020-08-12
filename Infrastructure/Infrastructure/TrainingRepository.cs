using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web;
using Domain.Entities;
using Infrastructure.Interfaces;
using System.Data.Sql;
using System.Data.SqlClient;
using RMS.Common;
using RMS.Common.ExceptionHandling;
using RMS.Common.Constants;
using RMS.Common.DataBase;
using RMS.Common.BusinessEntities;
using System.Web.Mvc;
using RMS.Common.BusinessEntities.Training;
using System.Collections;

namespace Infrastructure
{
    public class TrainingRepository : ITrainingRepository
    {
        public static SqlConnection objConnection = null;
        public static SqlCommand objCommand = null;
        public static SqlDataReader objReader;

        #region
        const string RaiseTrainingRqst = "TrainingRepository.cs";
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
        const string TrainingCourse = "TrainingCourse";
        DataSet ds;

        private static TrainingModel RaiseTrainingCollection;
        private static List<TrainingModel> raveHRCollection;
        #endregion

        #region Public Method


        /// </summary>
        /// This method is used for saving the Technical Training details
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        public int Save(TrainingModel RaiseTraining)
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;
            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                if (Convert.ToInt32(RaiseTraining.TrainingType) == CommonConstants.TechnicalTrainingID)
                {
                    objCommand = new SqlCommand(SPNames.TNI_InsertUpdateTechnicalTraining, objConnection);
                }
                else if (Convert.ToInt32(RaiseTraining.TrainingType) == CommonConstants.SoftSkillsTrainingID)
                {
                    objCommand = new SqlCommand(SPNames.TNI_InsertUpdateSoftskillsTraining, objConnection);
                }
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[13];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.RaiseID, RaiseTraining.RaiseID);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.TrainingType, RaiseTraining.TrainingType);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.TrainingName, RaiseTraining.TrainingName);
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.TrainingNameOther, String.IsNullOrEmpty(RaiseTraining.TrainingNameOther) ? String.Empty : RaiseTraining.TrainingNameOther);
                sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.Quarter, RaiseTraining.Quarter);
                sqlParam[5] = objCommand.Parameters.AddWithValue(SPParameter.NoOfParticipant, RaiseTraining.NoOfParticipant);
                sqlParam[6] = objCommand.Parameters.AddWithValue(SPParameter.Category, RaiseTraining.Category);
                sqlParam[7] = objCommand.Parameters.AddWithValue(SPParameter.RequestedBy, (RaiseTraining.RequestedBy) == null ? RaiseTraining.hdnRequestedBy : RaiseTraining.RequestedBy );
                sqlParam[8] = objCommand.Parameters.AddWithValue(SPParameter.Priority, RaiseTraining.Priority);
                sqlParam[9] = objCommand.Parameters.AddWithValue(SPParameter.BusinessImpact, String.IsNullOrEmpty(RaiseTraining.BusinessImpact) ? String.Empty : RaiseTraining.BusinessImpact);
                sqlParam[10] = objCommand.Parameters.AddWithValue(SPParameter.Comments, String.IsNullOrEmpty(RaiseTraining.Comments) ? String.Empty : RaiseTraining.Comments);
                sqlParam[11] = objCommand.Parameters.AddWithValue(SPParameter.CreatedByEmailId, RaiseTraining.CreatedByEmailId);
                if (Convert.ToInt32(RaiseTraining.TrainingType) == CommonConstants.TechnicalTrainingID)
                {
                    sqlParam[12] = objCommand.Parameters.AddWithValue(SPParameter.OutTechnicalID, 0);
                    sqlParam[12].Direction = ParameterDirection.Output; 
                }
                else if (Convert.ToInt32(RaiseTraining.TrainingType) == CommonConstants.SoftSkillsTrainingID)
                {
                    sqlParam[12] = objCommand.Parameters.AddWithValue(SPParameter.OutSoftSkillsID, 0);
                sqlParam[12].Direction = ParameterDirection.Output;
                }

                //sqlParam[12].Value = ParameterDirection.Output;

                objCommand.ExecuteNonQuery();
                return Convert.ToInt32(sqlParam[12].Value);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, SAVE, RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
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
        public int SaveKSS(TrainingModel RaiseTraining)
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
                SqlParameter[] sqlParam = new SqlParameter[10];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.RaiseID, RaiseTraining.RaiseID);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.TrainingType, RaiseTraining.TrainingType);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.Type, RaiseTraining.Type);
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.Topic, RaiseTraining.Topic);
                sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.Agenda, RaiseTraining.Agenda);
                sqlParam[5] = objCommand.Parameters.AddWithValue(SPParameter.Presenter, RaiseTraining.PresenterID);
                sqlParam[6] = objCommand.Parameters.AddWithValue(SPParameter.Date, RaiseTraining.Date);
                sqlParam[7] = objCommand.Parameters.AddWithValue(SPParameter.Comments, String.IsNullOrEmpty(RaiseTraining.Comments) ? String.Empty : RaiseTraining.Comments);
                sqlParam[8] = objCommand.Parameters.AddWithValue(SPParameter.CreatedByEmailId, String.IsNullOrEmpty(RaiseTraining.CreatedByEmailId) ? String.Empty : RaiseTraining.CreatedByEmailId);
                sqlParam[9] = objCommand.Parameters.AddWithValue(SPParameter.OutKSSID, 0);
                sqlParam[9].Direction = ParameterDirection.Output;

                objCommand.ExecuteNonQuery();
                return Convert.ToInt32(sqlParam[9].Value);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, SAVEKSS, RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
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
        public int SaveSeminars(TrainingModel RaiseTraining)
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
                SqlParameter[] sqlParam = new SqlParameter[17];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.RaiseID, RaiseTraining.RaiseID);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.TrainingType, RaiseTraining.TrainingType);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.SeminarsName, RaiseTraining.SeminarsName);
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.Date, RaiseTraining.Date);
                sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.RequestedBy, RaiseTraining.RequestedBy);
                sqlParam[5] = objCommand.Parameters.AddWithValue(SPParameter.NameOfParticipant, RaiseTraining.NameOfParticipantID);
                sqlParam[6] = objCommand.Parameters.AddWithValue(SPParameter.URL, String.IsNullOrEmpty(RaiseTraining.URL) ? String.Empty : RaiseTraining.URL);
                sqlParam[7] = objCommand.Parameters.AddWithValue(SPParameter.Comments, String.IsNullOrEmpty(RaiseTraining.Comments) ? String.Empty : RaiseTraining.Comments);
                sqlParam[8] = objCommand.Parameters.AddWithValue(SPParameter.CreatedByEmailId, String.IsNullOrEmpty(RaiseTraining.CreatedByEmailId) ? String.Empty : RaiseTraining.CreatedByEmailId);
                sqlParam[9] = objCommand.Parameters.AddWithValue(SPParameter.SeminarsEndDate, RaiseTraining.SeminarsEndDate);
                sqlParam[10] = objCommand.Parameters.AddWithValue(SPParameter.Location, RaiseTraining.Location);
                sqlParam[11] = objCommand.Parameters.AddWithValue(SPParameter.NameOfInstitute, RaiseTraining.NameOfInstitute);
                sqlParam[12] = objCommand.Parameters.AddWithValue(SPParameter.TotalNumberOfHours, RaiseTraining.TotalNumberOfHours);
                sqlParam[13] = objCommand.Parameters.AddWithValue(SPParameter.TotalNumberOfDays, RaiseTraining.TotalNumberOfDays);
                sqlParam[14] = objCommand.Parameters.AddWithValue(SPParameter.OutSeminarsID, 0);
                sqlParam[14].Direction = ParameterDirection.Output;

                sqlParam[15] = objCommand.Parameters.AddWithValue(SPParameter.SeminarCost, RaiseTraining.SeminarCost);
                sqlParam[16] = objCommand.Parameters.AddWithValue(SPParameter.AdditionalLogistics, RaiseTraining.AdditionalLogistics);


                objCommand.ExecuteNonQuery();
                return Convert.ToInt32(sqlParam[14].Value);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, SAVESEMINARS, RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
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
        public TrainingModel GetTechnicalSoftSkills(int RaiseID, int TrainingTypeID)
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
                RaiseTrainingCollection = new TrainingModel();
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
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, FunctionGetTechnicalTraining, RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
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
        /// public List<TrainingModel> ViewTechnicalTraining(TrainingModel RaiseTraining,  ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount)
        public List<TrainingModel> ViewTechnicalTraining(TrainingModel RaiseTraining)
        {
            DataAccessClass objGetTechnicalTraining = new DataAccessClass();
            raveHRCollection = new List<TrainingModel>();            
            SqlParameter[] sqlParam = new SqlParameter[7];
            try
            {
                objGetTechnicalTraining.OpenConnection(DBConstants.GetDBConnectionString());
                //sqlParam[0] = new SqlParameter(SPParameter.pageNum, SqlDbType.Int);
                //if (objParameter.PageNumber == CommonConstants.ZERO)
                //    sqlParam[0].Value = DBNull.Value;
                //else
                //    sqlParam[0].Value = objParameter.PageNumber;

                //sqlParam[1] = new SqlParameter(SPParameter.pageSize, SqlDbType.Int);
                //if (objParameter.PageSize == CommonConstants.ZERO)
                //    sqlParam[1].Value = DBNull.Value;
                //else
                //    sqlParam[1].Value = objParameter.PageSize;

                //sqlParam[2] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                //if (objParameter.SortExpressionAndDirection == null)
                //    sqlParam[2].Value = DBNull.Value;
                //else
                //    sqlParam[2].Value = objParameter.SortExpressionAndDirection;

                sqlParam[0] = new SqlParameter(SPParameter.UserEmpID, SqlDbType.Int);
                sqlParam[0].Value = RaiseTraining.UserEmpId;
                sqlParam[1] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                sqlParam[1].Value = RaiseTraining.RaiseID;
                sqlParam[2] = new SqlParameter(SPParameter.TrainingType, SqlDbType.Int);
                sqlParam[2].Value = RaiseTraining.TrainingType;
                sqlParam[3] = new SqlParameter(SPParameter.Priority, SqlDbType.Int);
                sqlParam[3].Value = RaiseTraining.Priority;
                sqlParam[4] = new SqlParameter(SPParameter.Status, SqlDbType.Int);
                sqlParam[4].Value = RaiseTraining.Status;
                sqlParam[5] = new SqlParameter(SPParameter.RequestedBy, SqlDbType.Int);
                sqlParam[5].Value = RaiseTraining.RequestedBy;
                sqlParam[6] = new SqlParameter(SPParameter.Quarter, SqlDbType.Int);
                sqlParam[6].Value = RaiseTraining.Quarter;
                //sqlParam[10] = new SqlParameter(SPParameter.pageCount, SqlDbType.Int);
                //sqlParam[10].Direction = ParameterDirection.Output;
                //sqlParam[11] = new SqlParameter(SPParameter.CurrentIndexCount, SqlDbType.Int);
                //sqlParam[11].Direction = ParameterDirection.Output;

                DataSet ds = objGetTechnicalTraining.GetDataSet(SPNames.TNI_ViewTechnicalTraining, sqlParam);

                //pageCount = Convert.ToInt32(sqlParam[10].Value);
                //CurrentIndexCount = Convert.ToInt32(sqlParam[11].Value);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RaiseTrainingCollection = new TrainingModel();
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
                    RaiseTrainingCollection.IsDeleteEnable = dr[DbTableColumn.RequestedByID].ToString() == Convert.ToString(RaiseTraining.UserEmpId) ? true : false;
                    RaiseTrainingCollection.Flag = Convert.ToInt32(dr[DbTableColumn.Flag]);
                    raveHRCollection.Add(RaiseTrainingCollection);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, FunctionViewTechnicalTraining, RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
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
        public List<TrainingModel> ViewSoftSkillsTraining(TrainingModel RaiseTraining)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            raveHRCollection = new List<TrainingModel>();
            SqlParameter[] sqlParam = new SqlParameter[7];
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                //sqlParam[0] = new SqlParameter(SPParameter.pageNum, SqlDbType.Int);
                //if (objParameter.PageNumber == CommonConstants.ZERO)
                //    sqlParam[0].Value = DBNull.Value;
                //else
                //    sqlParam[0].Value = objParameter.PageNumber;

                //sqlParam[1] = new SqlParameter(SPParameter.pageSize, SqlDbType.Int);
                //if (objParameter.PageSize == CommonConstants.ZERO)
                //    sqlParam[1].Value = DBNull.Value;
                //else
                //    sqlParam[1].Value = objParameter.PageSize;

                //sqlParam[2] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                //if (objParameter.SortExpressionAndDirection == null)
                //    sqlParam[2].Value = DBNull.Value;
                //else
                //    sqlParam[2].Value = objParameter.SortExpressionAndDirection;

                sqlParam[0] = new SqlParameter(SPParameter.UserEmpID, SqlDbType.Int);
                sqlParam[0].Value = RaiseTraining.UserEmpId;
                sqlParam[1] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                sqlParam[1].Value = RaiseTraining.RaiseID;
                sqlParam[2] = new SqlParameter(SPParameter.TrainingType, SqlDbType.Int);
                sqlParam[2].Value = RaiseTraining.TrainingType;
                sqlParam[3] = new SqlParameter(SPParameter.Priority, SqlDbType.Int);
                sqlParam[3].Value = RaiseTraining.Priority;
                sqlParam[4] = new SqlParameter(SPParameter.Status, SqlDbType.Int);
                sqlParam[4].Value = RaiseTraining.Status;
                sqlParam[5] = new SqlParameter(SPParameter.RequestedBy, SqlDbType.Int);
                sqlParam[5].Value = RaiseTraining.RequestedBy;
                sqlParam[6] = new SqlParameter(SPParameter.Quarter, SqlDbType.Int);
                sqlParam[6].Value = RaiseTraining.Quarter;
                

                DataSet ds = objGetTraining.GetDataSet(SPNames.TNI_ViewSoftSkillsTraining, sqlParam);

                //pageCount = Convert.ToInt32(sqlParam[10].Value);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RaiseTrainingCollection = new TrainingModel();
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
                    RaiseTrainingCollection.IsDeleteEnable = dr[DbTableColumn.RequestedByID].ToString() == Convert.ToString(RaiseTraining.UserEmpId) ? true : false;
                    RaiseTrainingCollection.Flag = Convert.ToInt32(dr[DbTableColumn.Flag]);
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
        public string DeleteTechnicalSoftSkillsTraining(TrainingModel RaiseTraining, int TrainingTypeID)
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
        public string DeleteKSSTraining(TrainingModel RaiseTraining)
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
        public string DeleteSeminarsTraining(TrainingModel RaiseTraining)
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
        public List<TrainingModel> ViewKSSTraining(TrainingModel RaiseTraining)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            raveHRCollection = new List<TrainingModel>();
            SqlParameter[] sqlParam = new SqlParameter[4];
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                //sqlParam[0] = new SqlParameter(SPParameter.pageNum, SqlDbType.Int);
                //if (objParameter.PageNumber == CommonConstants.ZERO)
                //    sqlParam[0].Value = DBNull.Value;
                //else
                //    sqlParam[0].Value = objParameter.PageNumber;

                //sqlParam[1] = new SqlParameter(SPParameter.pageSize, SqlDbType.Int);
                //if (objParameter.PageSize == CommonConstants.ZERO)
                //    sqlParam[1].Value = DBNull.Value;
                //else
                //    sqlParam[1].Value = objParameter.PageSize;

                //sqlParam[2] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                //if (objParameter.SortExpressionAndDirection == null)
                //    sqlParam[2].Value = DBNull.Value;
                //else
                //    sqlParam[2].Value = objParameter.SortExpressionAndDirection;

                sqlParam[0] = new SqlParameter(SPParameter.UserEmpID, SqlDbType.Int);
                sqlParam[0].Value = RaiseTraining.UserEmpId;

                sqlParam[1] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                sqlParam[1].Value = RaiseTraining.RaiseID;

                sqlParam[2] = new SqlParameter(SPParameter.TrainingType, SqlDbType.Int);
                sqlParam[2].Value = RaiseTraining.TrainingType;

                sqlParam[3] = new SqlParameter(SPParameter.Status, SqlDbType.Int);
                sqlParam[3].Value = RaiseTraining.Status;

                

                DataSet ds = new DataSet();
                ds.Clear();
                ds = objGetTraining.GetDataSet(SPNames.TNI_ViewKSSTraining, sqlParam);

                //pageCount = Convert.ToInt32(sqlParam[7].Value);
                //CurrentIndexCount = Convert.ToInt32(sqlParam[8].Value);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RaiseTrainingCollection = new TrainingModel();
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
                    RaiseTrainingCollection.IsDeleteEnable = dr[DbTableColumn.RequestedByID].ToString() == Convert.ToString(RaiseTraining.UserEmpId) ? true : false;                    
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
        public TrainingModel GetKSSTraining(int RaiseID)
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

                RaiseTrainingCollection = new TrainingModel();
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
                RaiseTrainingCollection.AttenderId = ds.Tables[0].Rows[0][DbTableColumn.Attenderid].ToString();
                RaiseTrainingCollection.Attender = ds.Tables[0].Rows[0][DbTableColumn.Attender].ToString();
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
        public List<TrainingModel> ViewSeminarsTraining(TrainingModel RaiseTraining)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            raveHRCollection = new List<TrainingModel>();
            SqlParameter[] sqlParam = new SqlParameter[5];
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                //sqlParam[0] = new SqlParameter(SPParameter.pageNum, SqlDbType.Int);
                //if (objParameter.PageNumber == CommonConstants.ZERO)
                //    sqlParam[0].Value = DBNull.Value;
                //else
                //    sqlParam[0].Value = objParameter.PageNumber;

                //sqlParam[1] = new SqlParameter(SPParameter.pageSize, SqlDbType.Int);
                //if (objParameter.PageSize == CommonConstants.ZERO)
                //    sqlParam[1].Value = DBNull.Value;
                //else
                //    sqlParam[1].Value = objParameter.PageSize;

                //sqlParam[2] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                //if (objParameter.SortExpressionAndDirection == null)
                //    sqlParam[2].Value = DBNull.Value;
                //else
                //    sqlParam[2].Value = objParameter.SortExpressionAndDirection;

                sqlParam[0] = new SqlParameter(SPParameter.UserEmpID, SqlDbType.Int);
                sqlParam[0].Value = RaiseTraining.UserEmpId;

                sqlParam[1] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                sqlParam[1].Value = RaiseTraining.RaiseID;

                sqlParam[2] = new SqlParameter(SPParameter.TrainingType, SqlDbType.Int);
                sqlParam[2].Value = RaiseTraining.TrainingType;

                sqlParam[3] = new SqlParameter(SPParameter.Status, SqlDbType.Int);
                sqlParam[3].Value = RaiseTraining.Status;

                sqlParam[4] = new SqlParameter(SPParameter.RequestedBy, SqlDbType.Int);
                sqlParam[4].Value = RaiseTraining.RequestedBy;             

                DataSet ds = objGetTraining.GetDataSet(SPNames.TNI_ViewSeminarsTraining, sqlParam);

                //pageCount = Convert.ToInt32(sqlParam[8].Value);
                //CurrentIndexCount = Convert.ToInt32(sqlParam[9].Value);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RaiseTrainingCollection = new TrainingModel();
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
                    RaiseTrainingCollection.IsDeleteEnable = dr[DbTableColumn.RequestedByID].ToString() == Convert.ToString(RaiseTraining.UserEmpId) ? true : false;                    
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
        public TrainingModel GetSeminarsTraining(int RaiseID)
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

                RaiseTrainingCollection = new TrainingModel();
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
                RaiseTrainingCollection.SeminarsEndDate = Convert.ToDateTime(ds.Tables[0].Rows[0][DbTableColumn.SeminarsEndDate].ToString());
                RaiseTrainingCollection.Location = ds.Tables[0].Rows[0][DbTableColumn.Location].ToString();
                RaiseTrainingCollection.NameOfInstitute = ds.Tables[0].Rows[0][DbTableColumn.NameOfInstitute].ToString();
                RaiseTrainingCollection.TotalNumberOfDays = Convert.ToInt32(ds.Tables[0].Rows[0][DbTableColumn.TotalNumberOfDays]);
                RaiseTrainingCollection.TotalNumberOfHours = Convert.ToInt32(ds.Tables[0].Rows[0][DbTableColumn.TotalNumberOfHours]);
                RaiseTrainingCollection.SeminarCost = float.Parse(ds.Tables[0].Rows[0][DbTableColumn.SeminarCost].ToString());
                RaiseTrainingCollection.AdditionalLogistics = ds.Tables[0].Rows[0][DbTableColumn.AdditonalLogistics].ToString();
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
                
        public int SaveApproveRejectTrainingRequest(TrainingModel RaiseTraining)
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

        public string UpdateRaiseTrainingStatus(TrainingModel RaiseTraining)
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
            raveHRCollection = new List<TrainingModel>();
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

       

        public int CheckDuplication(TrainingModel RaiseTraining)
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
        
        public DataSet GetEmailIDDetails(string UserEmpID, int Raiseid)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            SqlParameter[] sqlParam = new SqlParameter[2];
            raveHRCollection = new List<TrainingModel>();
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
        public DataSet GetEmailIDDetailsForKSS(string UserEmpID, int Raiseid)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            SqlParameter[] sqlParam = new SqlParameter[2];
            raveHRCollection = new List<TrainingModel>();
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                sqlParam[0] = new SqlParameter(SPParameter.UserEmpID, SqlDbType.NVarChar);
                sqlParam[0].Value = UserEmpID;
                sqlParam[1] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                sqlParam[1].Value = Raiseid;
                DataSet ds = objGetTraining.GetDataSet(SPNames.TNI_GetEmailIDDetailsForKSS, sqlParam);
                return ds;
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
            raveHRCollection = new List<TrainingModel>();
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
        #endregion Public Method
        
        #region Approve Reject Training Request Method

        /// <summary>
        /// Retrive Technical Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public List<TrainingModel> ApproveRejectViewTechnicalTraining(TrainingModel RaiseTraining)
        {
            DataAccessClass objGetTechnicalTraining = new DataAccessClass();
            raveHRCollection = new List<TrainingModel>();
            SqlParameter[] sqlParam = new SqlParameter[7];
            try
            {
                objGetTechnicalTraining.OpenConnection(DBConstants.GetDBConnectionString());
                
                sqlParam[0] = new SqlParameter(SPParameter.UserEmpID, SqlDbType.Int);
                sqlParam[0].Value = RaiseTraining.UserEmpId;
                sqlParam[1] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                sqlParam[1].Value = RaiseTraining.RaiseID;
                sqlParam[2] = new SqlParameter(SPParameter.TrainingType, SqlDbType.Int);
                sqlParam[2].Value = RaiseTraining.TrainingType;
                sqlParam[3] = new SqlParameter(SPParameter.Priority, SqlDbType.Int);
                sqlParam[3].Value = RaiseTraining.Priority;
                sqlParam[4] = new SqlParameter(SPParameter.Status, SqlDbType.Int);
                sqlParam[4].Value = RaiseTraining.Status;
                sqlParam[5] = new SqlParameter(SPParameter.RequestedBy, SqlDbType.Int);
                sqlParam[5].Value = RaiseTraining.RequestedBy;
                sqlParam[6] = new SqlParameter(SPParameter.Quarter, SqlDbType.Int);
                sqlParam[6].Value = RaiseTraining.Quarter;

                DataSet ds = objGetTechnicalTraining.GetDataSet(SPNames.TNI_ApproveRejectViewTechnicalTraining, sqlParam);
                                
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RaiseTrainingCollection = new TrainingModel();
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
        public List<TrainingModel> ApproveRejectViewSoftSkillsTraining(TrainingModel RaiseTraining)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            raveHRCollection = new List<TrainingModel>();
            SqlParameter[] sqlParam = new SqlParameter[7];
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                
                sqlParam[0] = new SqlParameter(SPParameter.UserEmpID, SqlDbType.Int);
                sqlParam[0].Value = RaiseTraining.UserEmpId;
                sqlParam[1] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                sqlParam[1].Value = RaiseTraining.RaiseID;
                sqlParam[2] = new SqlParameter(SPParameter.TrainingType, SqlDbType.Int);
                sqlParam[2].Value = RaiseTraining.TrainingType;
                sqlParam[3] = new SqlParameter(SPParameter.Priority, SqlDbType.Int);
                sqlParam[3].Value = RaiseTraining.Priority;
                sqlParam[4] = new SqlParameter(SPParameter.Status, SqlDbType.Int);
                sqlParam[4].Value = RaiseTraining.Status;
                sqlParam[5] = new SqlParameter(SPParameter.RequestedBy, SqlDbType.Int);
                sqlParam[5].Value = RaiseTraining.RequestedBy;
                sqlParam[6] = new SqlParameter(SPParameter.Quarter, SqlDbType.Int);
                sqlParam[6].Value = RaiseTraining.Quarter;
                
                DataSet ds = objGetTraining.GetDataSet(SPNames.TNI_ApproveRejectViewSoftSkillsTraining, sqlParam);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RaiseTrainingCollection = new TrainingModel();
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
        /// Retrive KSS Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public List<TrainingModel> ApproveRejectViewKSSTraining(TrainingModel RaiseTraining)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            raveHRCollection = new List<TrainingModel>();
            SqlParameter[] sqlParam = new SqlParameter[4];
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                
                sqlParam[0] = new SqlParameter(SPParameter.UserEmpID, SqlDbType.Int);
                sqlParam[0].Value = RaiseTraining.UserEmpId;
                sqlParam[1] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                sqlParam[1].Value = RaiseTraining.RaiseID;
                sqlParam[2] = new SqlParameter(SPParameter.TrainingType, SqlDbType.Int);
                sqlParam[2].Value = RaiseTraining.TrainingType;
                sqlParam[3] = new SqlParameter(SPParameter.Status, SqlDbType.Int);
                sqlParam[3].Value = RaiseTraining.Status;
                                
                DataSet ds = objGetTraining.GetDataSet(SPNames.TNI_ApproveRejectViewKSSTraining, sqlParam);
                
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RaiseTrainingCollection = new TrainingModel();
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
        /// Retrive Seminars Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public List<TrainingModel> ApproveRejectViewSeminarsTraining(TrainingModel RaiseTraining)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            raveHRCollection = new List<TrainingModel>();
            SqlParameter[] sqlParam = new SqlParameter[5];
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                
                sqlParam[0] = new SqlParameter(SPParameter.UserEmpID, SqlDbType.Int);
                sqlParam[0].Value = RaiseTraining.UserEmpId;
                sqlParam[1] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                sqlParam[1].Value = RaiseTraining.RaiseID;
                sqlParam[2] = new SqlParameter(SPParameter.TrainingType, SqlDbType.Int);
                sqlParam[2].Value = RaiseTraining.TrainingType;
                sqlParam[3] = new SqlParameter(SPParameter.Status, SqlDbType.Int);
                sqlParam[3].Value = RaiseTraining.Status;
                sqlParam[4] = new SqlParameter(SPParameter.RequestedBy, SqlDbType.Int);
                sqlParam[4].Value = RaiseTraining.RequestedBy;
                
                DataSet ds = objGetTraining.GetDataSet(SPNames.TNI_ApproveRejectViewSeminarsTraining, sqlParam);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    RaiseTrainingCollection = new TrainingModel();
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

        #endregion

        //Harsha Issue Id-59073 - Start
        //Description- Training : Create emp Nominated page so that emp can view they nomination. Change confirmation email body
        #region Fetch Nomination Employee Details
        public EmployeeTrainingNominationEntity GetEmployeeTrainingDetails(int courseId)
        {
            EmployeeTrainingNominationEntity employeeCourse = new EmployeeTrainingNominationEntity();

            int empid=0;
            if (System.Web.HttpContext.Current.Session["EmpID"] != null)
            {
                empid = Convert.ToInt16(HttpContext.Current.Session["EmpID"]);
            }
            DataAccessClass objGetTraining = new DataAccessClass();
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value =  courseId;
                sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[1].Value = empid;
                SqlDataReader dr = objGetTraining.ExecuteReaderSP(SPNames.TNI_ViewNominationDetails, sqlParam);
                while (dr.Read())
                {
                    employeeCourse.TrainingNameId = Convert.ToInt32(dr[DbTableColumn.TrainingNameID]);
                    employeeCourse.TrainerName = Convert.ToString(dr[DbTableColumn.TrainerName]);
                    employeeCourse.TrainingTypeId = Convert.ToInt32(dr[DbTableColumn.TrainingTypeID]);
                    employeeCourse.TrainingMode = Convert.ToString(dr[DbTableColumn.TrainingMode]);
                    employeeCourse.CourseId = Convert.ToInt32(dr[DbTableColumn.CourseID]);
                    employeeCourse.CourseName = Convert.ToString(dr[DbTableColumn.CourseName]);
                    employeeCourse.StartDate = Convert.ToDateTime(dr[DbTableColumn.StartDate]);
                    employeeCourse.EndDate = Convert.ToDateTime(dr[DbTableColumn.EndDate]);
                    employeeCourse.CourseComments = Convert.ToString(dr[DbTableColumn.CourseComments]);
                    employeeCourse.NominationComments = Convert.ToString(dr[DbTableColumn.NominationComments]);
                    employeeCourse.NoOfDays = Convert.ToInt32(dr[DbTableColumn.NoOfDays]);
                    employeeCourse.TrainingHours = Convert.ToInt16(dr[DbTableColumn.TotalTrainingHours]);
                    employeeCourse.LastDateOfNomination = (Convert.ToString(dr[DbTableColumn.NominationDueDate]) == string.Empty) ? (DateTime?)null : Convert.ToDateTime(dr[DbTableColumn.NominationDueDate]);
                    employeeCourse.NominationTypeId = Convert.ToInt32(dr[DbTableColumn.NominationTypeID]);
                    employeeCourse.EmpId = Convert.ToInt32(dr[DbTableColumn.EMPId]);
                    employeeCourse.EmployeeName = Convert.ToString(dr[DbTableColumn.EmployeeName]);
                    employeeCourse.PreTrainingRating = Convert.ToString(dr[DbTableColumn.PreTrainingRating]);
                    employeeCourse.Priority = Convert.ToString(dr[DbTableColumn.Priority]);
                    employeeCourse.NominatorName = Convert.ToString(dr[DbTableColumn.NominatorName]);
                    employeeCourse.ObjectiveForSoftSkill = Convert.ToString(dr[DbTableColumn.ObjectiveForSoftSkill]);
                    //employeeCourse.Comments = Convert.ToString(dr[DbTableColumn.Comments]);
                    //employeeCourse.UploadedCourseFile = Convert.ToString(dr[DbTableColumn.UploadedCourseFiles]);
                    //employeeCourse.IsRMOLoggedIn = Convert.ToString(Role).ToLower() == CommonConstants.AdminRole.ToLower() ? true : false;
                
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "GetEmployeeTrainingDetails", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally {
                objGetTraining.CloseConncetion();
            }
            return employeeCourse;
        }
        public List<KeyValuePair<int, string>> GetTrainingNamesByEmployeeId(int empID)
        {

            DataAccessClass objGetTraining = new DataAccessClass();
            List<KeyValuePair<int, string>> trainingList = new List<KeyValuePair<int, string>>();
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[0].Value = empID;
                SqlDataReader dr = objGetTraining.ExecuteReaderSP(SPNames.TNI_GetActiveTrainingCourseName, sqlParam);
                while (dr.Read())
                {
                    trainingList.Add(new KeyValuePair<int, string>(Convert.ToInt32(dr[DbTableColumn.CourseID]), Convert.ToString(dr[DbTableColumn.CourseName])));
                }
                return trainingList;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "GetTrainingNamesByEmployeeId", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }
        }

        //Harsha Issue Id-59073 - End

        #endregion
        #region Submit Nomination Method
        //Jagmohan : 14/09/2015 Start
        /// <summary>
        /// Get the list of Training name for all courses
        /// </summary>
        /// <returns>Collection</returns>
        public List<KeyValuePair<int, string>> GetTrainingNameforAllCourses(int empID)
        {

            // Changed by : Venkatesh  : Start
            string Role = string.Empty;
            if (System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
            {
                ArrayList arrRolesForUser = new ArrayList();
                arrRolesForUser = (ArrayList)System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES];
                if (arrRolesForUser.Contains("admin"))
                    Role = "Admin";
                else if (arrRolesForUser.Contains("manager"))
                    Role = "Manager";
            }
            // Changed by : Venkatesh  : End


            DataAccessClass objGetTraining = new DataAccessClass();            
            List<KeyValuePair<int, string>> trainingList = new List<KeyValuePair<int, string>>();
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[0].Value =  empID;
                sqlParam[1] = new SqlParameter(SPParameter.RoleName, SqlDbType.VarChar);
                sqlParam[1].Value = Convert.ToString(Role);
                SqlDataReader dr = objGetTraining.ExecuteReaderSP(SPNames.TNI_GetCurrentCourseTrainingName, sqlParam);
                while (dr.Read())
                {
                    trainingList.Add(new KeyValuePair<int, string>(Convert.ToInt32(dr[DbTableColumn.CourseID]), Convert.ToString(dr[DbTableColumn.CourseName])));
                }
                return trainingList;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "GetTrainingNameforAllCourses", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }
        }

        
        /// <summary>
        /// Get the list of confirm/submitted Training name for all courses
        /// </summary>
        /// <returns>Collection</returns>
        public List<KeyValuePair<int, string>> GetConfirmTrainingNameforAllCourses(int empID)
        {
            // Changed by : Venkatesh  : Start
            string Role = string.Empty;
            if (System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
            {
                ArrayList arrRolesForUser = new ArrayList();
                arrRolesForUser = (ArrayList)System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES];
                if (arrRolesForUser.Contains("admin"))
                    Role = "Admin";               
            }
            // Changed by : Venkatesh  : End
            DataAccessClass objGetTraining = new DataAccessClass();
            List<KeyValuePair<int, string>> trainingList = new List<KeyValuePair<int, string>>();
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[0].Value = empID;
                sqlParam[1] = new SqlParameter(SPParameter.RoleName, SqlDbType.VarChar);
                sqlParam[1].Value = Convert.ToString(Role);
                SqlDataReader dr = objGetTraining.ExecuteReaderSP(SPNames.TNI_GetConfirmCourseTrainingName, sqlParam);
                while (dr.Read())
                {
                    trainingList.Add(new KeyValuePair<int, string>(Convert.ToInt32(dr[DbTableColumn.CourseID]), Convert.ToString(dr[DbTableColumn.CourseName])));
                }
                return trainingList;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "GetConfirmTrainingNameforAllCourses", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }
        }


        /// <summary>
        /// Get the list of Training name for all courses
        /// </summary>
        /// <returns>Collection</returns>
        public NominationModel GetTrainingDetailByID(int courseid)
        {
            // Changed by : Venkatesh  : Start
            string  Role = "";
            if (System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
            {
                ArrayList arrRolesForUser = new ArrayList();
                arrRolesForUser = (ArrayList)System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES];
                if (arrRolesForUser.Contains("admin"))
                    Role = "Admin";
            }
            // Changed by : Venkatesh  : End

            DataAccessClass objGetTraining = new DataAccessClass();
            NominationModel courseDetail = new NominationModel();
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];               

                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value = courseid;

                SqlDataReader dr = objGetTraining.ExecuteReaderSP(SPNames.TNI_GetTrainingDetailByID, sqlParam);
                while (dr.Read())
                {                    
                    courseDetail.TrainingNameID = Convert.ToInt32(dr[DbTableColumn.TrainingNameID]);
                    courseDetail.TrainerName = Convert.ToString(dr[DbTableColumn.TrainerName]);
                    courseDetail.TrainingTypeID = Convert.ToInt32(dr[DbTableColumn.TrainingTypeID]);
                    courseDetail.TrainingMode = Convert.ToString(dr[DbTableColumn.TrainingMode]);
                    courseDetail.CourseID = Convert.ToInt32(dr[DbTableColumn.CourseID]);
                    courseDetail.CourseName = Convert.ToString(dr[DbTableColumn.CourseName]);
                    courseDetail.StartDate = Convert.ToDateTime(dr[DbTableColumn.StartDate]);
                    courseDetail.EndDate = Convert.ToDateTime(dr[DbTableColumn.EndDate]);
                    courseDetail.Comments = Convert.ToString(dr[DbTableColumn.Comments]);
                    courseDetail.NoOfDays = Convert.ToInt32(dr[DbTableColumn.NoOfDays]);
                    courseDetail.TotalTrainingHours = Convert.ToInt16(dr[DbTableColumn.TotalTrainingHours]);
                    courseDetail.NominationDueDate = (Convert.ToString(dr[DbTableColumn.NominationDueDate]) == string.Empty) ? (DateTime?)null : Convert.ToDateTime(dr[DbTableColumn.NominationDueDate]);
                    courseDetail.NominationTypeID = Convert.ToInt32(dr[DbTableColumn.NominationTypeID]);
                    courseDetail.IsValidationRequired = Convert.ToBoolean(dr[DbTableColumn.IsValidationRequired]);
                    courseDetail.UploadedCourseFile = Convert.ToString(dr[DbTableColumn.UploadedCourseFiles]);
                    courseDetail.IsRMOLoggedIn = Convert.ToString(Role).ToLower() == CommonConstants.AdminRole.ToLower() ? true : false;
                   
                }
                
                return courseDetail;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "GetTrainingDetailByID", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {              
                objGetTraining.CloseConncetion();
            }
        }
    
        /// <summary>
        /// Get employee detail by id
        /// </summary>
        /// <returns>empid</returns>
        public Employee GetEmployeeDetailByID(int empid)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            Employee employeedetail = new Employee();
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[0].Value = empid;

                SqlDataReader dr = objGetTraining.ExecuteReaderSP(SPNames.TNI_GetEmployeeDetailwithProject, sqlParam);
                while (dr.Read())
                {
                    employeedetail.EmployeeID = Convert.ToInt16(dr[DbTableColumn.EMPId]);
                    employeedetail.EmployeeName = Convert.ToString(dr[DbTableColumn.EmployeeName]);
                    employeedetail.EmailID = Convert.ToString(dr[DbTableColumn.EmailId]);
                    employeedetail.Designation = Convert.ToString(dr[DbTableColumn.Designation]);
                    employeedetail.Project = Convert.ToString(dr[DbTableColumn.Project]);                                        
                }
                return employeedetail;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "GetEmployeeDetailByID", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {              
                objGetTraining.CloseConncetion();
            }
        }


        /// <summary>
        /// Get the list of all employee nominated by manager
        /// </summary>
        /// <returns>mngrid</returns>
        public List<Employee> GetAllNominatedEmployeeList(int mngrid, int trainingnameid, int trainingcourseid)
        {
            // Changed by : Venkatesh  : Start
            string Role = string.Empty;
            if (System.Web.HttpContext.Current. Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
            {
                ArrayList arrRolesForUser = new ArrayList();
                arrRolesForUser = (ArrayList)System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES];
                if (arrRolesForUser.Contains("admin"))
                    Role = "Admin";
                else if (arrRolesForUser.Contains("manager"))
                        Role = "Manager";
            }
            // Changed by : Venkatesh  : End

            DataAccessClass objGetTraining = new DataAccessClass();
            Employee addEmployee;
            List<Employee> employeeList = new List<Employee>();            
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[0].Value = mngrid;

                sqlParam[1] = new SqlParameter(SPParameter.RoleName, SqlDbType.VarChar);
                sqlParam[1].Value = Convert.ToString(Role);

                sqlParam[2] = new SqlParameter(SPParameter.TrainingID, SqlDbType.Int);
                sqlParam[2].Value = trainingnameid;

                sqlParam[3] = new SqlParameter(SPParameter.TrainingCourseID, SqlDbType.Int);
                sqlParam[3].Value = trainingcourseid;

                SqlDataReader dr = objGetTraining.ExecuteReaderSP(SPNames.TNI_GetNominatedEmployeeListByManagerID, sqlParam);
                while (dr.Read())
                {
                    addEmployee = new Employee();
                    addEmployee.NominationID = Convert.ToInt16(dr[DbTableColumn.NominationID]);
                    addEmployee.EmployeeID = Convert.ToInt16(dr[DbTableColumn.EMPId]);
                    addEmployee.EmployeeName = Convert.ToString(dr[DbTableColumn.EmployeeName]);
                    addEmployee.EmailID = Convert.ToString(dr[DbTableColumn.EmailId]);
                    addEmployee.Designation = Convert.ToString(dr[DbTableColumn.Designation]);
                    addEmployee.Project = Convert.ToString(dr[DbTableColumn.Project]);
                    addEmployee.Priority = Convert.ToString(dr[DbTableColumn.Priority]);
                    addEmployee.PreTraining = Convert.ToString(dr[DbTableColumn.PreTrainingRating]);
                    addEmployee.SubmitStatus = Convert.ToString(dr[DbTableColumn.SubmitStatus]);
                    addEmployee.Comment = Convert.ToString(dr[DbTableColumn.Comments]);
                    addEmployee.ObjectiveForSoftSkill = Convert.ToString(dr[DbTableColumn.ObjectiveForSoftSkill]);
                    addEmployee.courseID = Convert.ToInt16(dr[DbTableColumn.CourseID]);
                    addEmployee.TrainingNameID = Convert.ToInt16(dr[DbTableColumn.TrainingNameID]);
                    addEmployee.TrainingTypeID = Convert.ToInt16(dr[DbTableColumn.TrainingTypeID]);
                    addEmployee.NominationTypeID = Convert.ToInt16(dr[DbTableColumn.NominationTypeID]);
                    //Poonam : 27/04/2016 Starts
                    //Issue : 57841 : On Submit Nomination page if pre rating check box is not ticked for that course still pre rating column appears in employee grid 
                    addEmployee.EffectivenessID = dr[DbTableColumn.EffectivenessID].ToString();
                    //Poonam : 27/04/2016 Ends
                    addEmployee.IsRMOLogin = Convert.ToString(Role).ToLower() == CommonConstants.AdminRole.ToLower() ? true : false;
                    employeeList.Add(addEmployee);
                }
                return employeeList;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "GetAllNominatedEmployeeList", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }
        
        }


        /// <summary>
        /// save nominated employee in table T_nomination
        /// </summary>
        /// <returns>Employee</returns>
        public int SaveNominatedEmployee(Employee saveEmployee)
        {
            // Changed by : Venkatesh  : Start
            string Role = string.Empty;
            if (System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
            {
                ArrayList arrRolesForUser = new ArrayList();
                arrRolesForUser = (ArrayList)System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES];
                if (arrRolesForUser.Contains("admin"))
                    Role = "Admin";                
            }
            // Changed by : Venkatesh  : End


            DataAccessClass objGetTraining = new DataAccessClass();            
            
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[12];
                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value = saveEmployee.courseID;

                sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[1].Value = saveEmployee.EmployeeID;

                sqlParam[2] = new SqlParameter(SPParameter.TrainingID, SqlDbType.Int);
                sqlParam[2].Value = saveEmployee.TrainingNameID;

                sqlParam[3] = new SqlParameter(SPParameter.Priority, SqlDbType.Int);
                sqlParam[3].Value = saveEmployee.PriorityCode;

                sqlParam[4] = new SqlParameter(SPParameter.PreTrainingRating, SqlDbType.Int);
                sqlParam[4].Value = saveEmployee.PreTrainingCode;

                sqlParam[5] = new SqlParameter(SPParameter.ObjectiveForSoftSkill, SqlDbType.VarChar);
                sqlParam[5].Value = string.IsNullOrWhiteSpace(saveEmployee.ObjectiveForSoftSkill) ? string.Empty : saveEmployee.ObjectiveForSoftSkill;  

                sqlParam[6] = new SqlParameter(SPParameter.Comments, SqlDbType.VarChar);
                sqlParam[6].Value = string.IsNullOrWhiteSpace(saveEmployee.Comment) ? string.Empty : saveEmployee.Comment;                

                if (saveEmployee.IsRMOLogin)
                {
                    sqlParam[7] = new SqlParameter(SPParameter.NominatorEmpID, SqlDbType.Int);
                    sqlParam[7].Value = saveEmployee.RMONominatorID;

                    sqlParam[8] = new SqlParameter(SPParameter.RMONominatorEmpID, SqlDbType.Int);
                    sqlParam[8].Value = Convert.ToInt16(HttpContext.Current.Session["EmpID"]);
                }
                else
                {
                    sqlParam[7] = new SqlParameter(SPParameter.NominatorEmpID, SqlDbType.Int);
                    sqlParam[7].Value = Convert.ToInt16(HttpContext.Current.Session["EmpID"]);

                    sqlParam[8] = new SqlParameter(SPParameter.RMONominatorEmpID, SqlDbType.Int);
                    sqlParam[8].Value = null;
                }

                sqlParam[9] = new SqlParameter(SPParameter.RoleName, SqlDbType.VarChar);
                sqlParam[9].Value = Convert.ToString(Role);

                sqlParam[10] = new SqlParameter(SPParameter.Status, SqlDbType.Int);
                sqlParam[10].Direction = ParameterDirection.Output;

                sqlParam[11] = new SqlParameter(SPParameter.LoginEmpId, SqlDbType.Int);
                sqlParam[11].Value = Convert.ToInt32(HttpContext.Current.Session["EmpID"]);

                objGetTraining.ExecuteNonQuerySP(SPNames.TNI_USP_TNI_SaveTrainingNomination, sqlParam);
                int i = Convert.ToInt16(sqlParam[10].Value);
                return i;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "SaveNominatedEmployee", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }

        }


        /// <summary>
        /// delete nominated employee in table T_nomination
        /// </summary>
        /// <returns>Employee</returns>
        public Employee DeleteNominatedEmployee(Employee deleteEmployee)
        {
            // Changed by : Venkatesh  : Start
            string Role = string.Empty;
            if (System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
            {
                ArrayList arrRolesForUser = new ArrayList();
                arrRolesForUser = (ArrayList)System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES];
                if (arrRolesForUser.Contains("admin"))
                    Role = "Admin";
            }
            // Changed by : Venkatesh  : End

            DataAccessClass objGetTraining = new DataAccessClass();
            Employee deletedEmployee;
            List<Employee> deletedemployeeList = new List<Employee>(); 

            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                // Harsha Issue Id-59072 
                // Description: Training : provision to Re-approve the  Rejected Nomination for the courses
                SqlParameter[] sqlParam = new SqlParameter[8];
                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value = deleteEmployee.courseID;

                sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[1].Value = deleteEmployee.EmployeeID;

                sqlParam[2] = new SqlParameter(SPParameter.TrainingID, SqlDbType.Int);
                sqlParam[2].Value = deleteEmployee.TrainingNameID;                

                sqlParam[3] = new SqlParameter(SPParameter.NominatorEmpID, SqlDbType.Int);
                //sqlParam[3].Value = Convert.ToInt16(HttpContext.Current.Session["EmpID"]);
                sqlParam[3].Value = deleteEmployee.NominationEmpID;

                sqlParam[4] = new SqlParameter(SPParameter.RoleName, SqlDbType.VarChar);
                sqlParam[4].Value = Convert.ToString(Role);

                sqlParam[5] = new SqlParameter(SPParameter.NominationID, SqlDbType.Int);
                sqlParam[5].Value = deleteEmployee.NominationID;

                // Harsha Issue Id-59072 - Start
                // Description: Training : provision to Re-approve the  Rejected Nomination for the courses
                sqlParam[6] = new SqlParameter(SPParameter.isConfirmNomination, SqlDbType.Bit);
                sqlParam[6].Value = deleteEmployee.IsConfirmNomination;

                sqlParam[7] = new SqlParameter(SPParameter.Status, SqlDbType.Int);
                sqlParam[7].Direction = ParameterDirection.Output;
                // Harsha Issue Id-59072 - End
                

                SqlDataReader dr = objGetTraining.ExecuteReaderSP(SPNames.TNI_USP_TNI_DeleteTrainingNomination, sqlParam);
                deletedEmployee = new Employee();
                while (dr.Read())
                {
                    
                    deletedEmployee.EmployeeID = Convert.ToInt16(dr[DbTableColumn.EMPId]);
                    deletedEmployee.EmployeeName = Convert.ToString(dr[DbTableColumn.EmployeeName]);
                    deletedEmployee.EmailID = Convert.ToString(dr[DbTableColumn.EmailId]);
                    deletedEmployee.Designation = Convert.ToString(dr[DbTableColumn.Designation]);
                    deletedEmployee.Project = Convert.ToString(dr[DbTableColumn.Project]);
                    deletedEmployee.Priority = Convert.ToString(dr[DbTableColumn.Priority]);
                    deletedEmployee.PreTraining = Convert.ToString(dr[DbTableColumn.PreTrainingRating]);
                    deletedEmployee.Comment = Convert.ToString(dr[DbTableColumn.Comments]);
                    deletedEmployee.courseID = Convert.ToInt16(dr[DbTableColumn.CourseID]);
                    deletedEmployee.TrainingNameID = Convert.ToInt16(dr[DbTableColumn.TrainingNameID]);

                    deletedEmployee.NominatorName = Convert.ToString(dr[DbTableColumn.NominatorName]);
                    deletedEmployee.NominatorEmailID = Convert.ToString(dr[DbTableColumn.NominatorEmailID]);
                    deletedEmployee.TrainingName = Convert.ToString(dr[DbTableColumn.TrainingName]);
                    deletedEmployee.ObjectiveForSoftSkill = Convert.ToString(dr[DbTableColumn.ObjectiveForSoftSkill]);
                    deletedEmployee.courseName= Convert.ToString(dr[DbTableColumn.CourseName]);
                    deletedEmployee.TrainingTypeID = Convert.ToInt16(dr[DbTableColumn.TrainingTypeID]);

                    deletedemployeeList.Add(deletedEmployee);
                }
                return deletedEmployee;
               
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "DeleteNominatedEmployee", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }

        }



        /// <summary>
        /// Get edit employee detail by id
        /// </summary>
        /// <returns>empid</returns>
        public Employee GetEmployeeDetailForEdit(int trainingcourseID, int trainingnameID, int editemployeeid)
        {
            // Changed by : Venkatesh  : Start
            string Role = string.Empty;
            if (System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
            {
                ArrayList arrRolesForUser = new ArrayList();
                arrRolesForUser = (ArrayList)System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES];
                if (arrRolesForUser.Contains("admin"))
                    Role = "Admin";
            }
            // Changed by : Venkatesh  : End

            DataAccessClass objGetTraining = new DataAccessClass();
            Employee employeedetail = new Employee();
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = new SqlParameter(SPParameter.TrainingCourseID, SqlDbType.Int);
                sqlParam[0].Value = trainingcourseID;

                sqlParam[1] = new SqlParameter(SPParameter.TrainingNameID, SqlDbType.Int);
                sqlParam[1].Value = trainingnameID;

                sqlParam[2] = new SqlParameter(SPParameter.EditEmpID, SqlDbType.Int);
                sqlParam[2].Value = editemployeeid;

                sqlParam[3] = new SqlParameter(SPParameter.NominatorEmpID, SqlDbType.Int);
                sqlParam[3].Value = Convert.ToInt16(HttpContext.Current.Session["EmpID"]);

                sqlParam[4] = new SqlParameter(SPParameter.RoleName, SqlDbType.VarChar);
                sqlParam[4].Value = Convert.ToString(Role);

                SqlDataReader dr = objGetTraining.ExecuteReaderSP(SPNames.TNI_USP_EditTrainingNomination, sqlParam);
                while (dr.Read())
                {
                    employeedetail.EmployeeID = Convert.ToInt16(dr[DbTableColumn.EMPId]);
                    employeedetail.EmployeeName = Convert.ToString(dr[DbTableColumn.EmployeeName]);
                    employeedetail.PriorityCode = Convert.ToInt16(dr[DbTableColumn.Priority]);
                    employeedetail.PreTrainingCode = Convert.ToInt16(dr[DbTableColumn.PreTrainingRating]);
                    employeedetail.Comment = Convert.ToString(dr[DbTableColumn.Comments]);
                    employeedetail.ObjectiveForSoftSkill = Convert.ToString(dr[DbTableColumn.ObjectiveForSoftSkill]);
                    employeedetail.TrainingTypeID = Convert.ToInt16(dr[DbTableColumn.TrainingTypeID]);
                    employeedetail.courseID = Convert.ToInt16(dr[DbTableColumn.CourseID]);
                    employeedetail.TrainingNameID = Convert.ToInt16(dr[DbTableColumn.TrainingNameID]);
                    employeedetail.RMONominatorID = Convert.ToInt16(dr[DbTableColumn.RMONominatedByEmpID]);
                    employeedetail.NominationTypeID = Convert.ToInt16(dr[DbTableColumn.NominationTypeID]);
                    employeedetail.NominationEmpID = Convert.ToInt16(dr[DbTableColumn.NominatorEmpID]);
                }
                return employeedetail;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "GetEmployeeDetailForEdit", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }
        }



        /// <summary>
        /// Update nominated employee in table T_nomination
        /// </summary>
        /// <returns>Employee</returns>
        public int UpdateNominatedEmployee(Employee updatemployee)
        {
            // Changed by : Venkatesh  : Start
            string Role = string.Empty;
            if (System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
            {
                ArrayList arrRolesForUser = new ArrayList();
                arrRolesForUser = (ArrayList)System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES];
                if (arrRolesForUser.Contains("admin"))
                    Role = "Admin";
            }
            // Changed by : Venkatesh  : End


            DataAccessClass objGetTraining = new DataAccessClass();

            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[10];
                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value = updatemployee.courseID;

                sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[1].Value = updatemployee.EmployeeID;

                sqlParam[2] = new SqlParameter(SPParameter.TrainingID, SqlDbType.Int);
                sqlParam[2].Value = updatemployee.TrainingNameID;

                sqlParam[3] = new SqlParameter(SPParameter.Priority, SqlDbType.Int);
                sqlParam[3].Value = updatemployee.PriorityCode;

                sqlParam[4] = new SqlParameter(SPParameter.PreTrainingRating, SqlDbType.Int);
                sqlParam[4].Value = updatemployee.PreTrainingCode;

                sqlParam[5] = new SqlParameter(SPParameter.Comments, SqlDbType.NVarChar);
                sqlParam[5].Value = string.IsNullOrWhiteSpace(updatemployee.Comment) ? string.Empty : updatemployee.Comment;               

                sqlParam[6] = new SqlParameter(SPParameter.ObjectiveForSoftSkill, SqlDbType.VarChar);
                sqlParam[6].Value = string.IsNullOrWhiteSpace(updatemployee.ObjectiveForSoftSkill) ? string.Empty : updatemployee.ObjectiveForSoftSkill;

                if (updatemployee.IsRMOLogin)
                {
                    sqlParam[7] = new SqlParameter(SPParameter.NominatorEmpID, SqlDbType.Int);
                    sqlParam[7].Value = updatemployee.RMONominatorID;
                }
                else
                {
                    sqlParam[7] = new SqlParameter(SPParameter.NominatorEmpID, SqlDbType.Int);
                    sqlParam[7].Value = Convert.ToInt16(HttpContext.Current.Session["EmpID"]);
                }


                sqlParam[8] = new SqlParameter(SPParameter.RoleName, SqlDbType.VarChar);
                sqlParam[8].Value = Convert.ToString(Role);

                sqlParam[9] = new SqlParameter(SPParameter.Status, SqlDbType.Int);
                sqlParam[9].Direction = ParameterDirection.Output;

                objGetTraining.ExecuteNonQuerySP(SPNames.TNI_USP_TNI_UpdateTrainingNomination, sqlParam);
                int i = Convert.ToInt16(sqlParam[9].Value);
                return i;                

            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "UpdateNominatedEmployee", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }

        }



        /// <summary>
        /// Submit nominated employee in table T_nomination
        /// </summary>
        /// <returns>Employee</returns>
        public List<Employee> SubmitNominatedEmployee(int trainingcourseID, int trainingnameID, string selectedemployeeid)
        {
            // Changed by : Venkatesh  : Start
            string Role = string.Empty;
            if (System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
            {
                ArrayList arrRolesForUser = new ArrayList();
                arrRolesForUser = (ArrayList)System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES];
                if (arrRolesForUser.Contains("admin"))
                    Role = "Admin";
            }
            // Changed by : Venkatesh  : End


            DataAccessClass objGetTraining = new DataAccessClass();
            Employee submitEmployee;
            List<Employee> submittedemployeeList = new List<Employee>();  
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value = trainingcourseID;

                sqlParam[1] = new SqlParameter(SPParameter.TrainingID, SqlDbType.Int);
                sqlParam[1].Value = trainingnameID;

                sqlParam[2] = new SqlParameter(SPParameter.NominatorEmpID, SqlDbType.Int);
                sqlParam[2].Value = Convert.ToInt16(HttpContext.Current.Session["EmpID"]);

                sqlParam[3] = new SqlParameter(SPParameter.RoleName, SqlDbType.VarChar);
                sqlParam[3].Value = Convert.ToString(Role);

                sqlParam[4] = new SqlParameter(SPParameter.Status, SqlDbType.Int);
                sqlParam[4].Direction = ParameterDirection.Output;

                if (selectedemployeeid == "")
                {
                    sqlParam[5] = new SqlParameter(SPParameter.SelectedEmpID,  DBNull.Value);
                    
                }
                else
                {
                    sqlParam[5] = new SqlParameter(SPParameter.SelectedEmpID, SqlDbType.NVarChar);
                    sqlParam[5].Value = selectedemployeeid;
                }

                SqlDataReader dr = objGetTraining.ExecuteReaderSP(SPNames.TNI_USP_TNI_SubmitTrainingNomination, sqlParam);
                while (dr.Read())
                {
                    submitEmployee = new Employee();
                    submitEmployee.EmployeeID = Convert.ToInt16(dr[DbTableColumn.EMPId]);
                    submitEmployee.EmployeeName = Convert.ToString(dr[DbTableColumn.EmployeeName]);
                    submitEmployee.EmailID = Convert.ToString(dr[DbTableColumn.EmailId]);
                    submitEmployee.Designation = Convert.ToString(dr[DbTableColumn.Designation]);
                    submitEmployee.Project = Convert.ToString(dr[DbTableColumn.Project]);
                    submitEmployee.Priority = Convert.ToString(dr[DbTableColumn.Priority]);
                    submitEmployee.PreTraining = Convert.ToString(dr[DbTableColumn.PreTrainingRating]);
                    submitEmployee.Comment = Convert.ToString(dr[DbTableColumn.Comments]);
                    submitEmployee.courseID = Convert.ToInt16(dr[DbTableColumn.CourseID]);
                    submitEmployee.TrainingNameID = Convert.ToInt16(dr[DbTableColumn.TrainingNameID]);
                    submitEmployee.NominatorName = Convert.ToString(dr[DbTableColumn.NominatorName]);
                    submitEmployee.NominatorEmailID = Convert.ToString(dr[DbTableColumn.NominatorEmailID]);
                    submitEmployee.TrainingName = Convert.ToString(dr[DbTableColumn.TrainingName]);
                    submitEmployee.ObjectiveForSoftSkill = Convert.ToString(dr[DbTableColumn.ObjectiveForSoftSkill]);
                    submitEmployee.courseName = Convert.ToString(dr[DbTableColumn.CourseName]);
                    submitEmployee.TrainingTypeID = Convert.ToInt16(dr[DbTableColumn.TrainingTypeID]);
                    submitEmployee.EffectivenessID = Convert.ToString(dr[DbTableColumn.EffectivenessID]);
                    submitEmployee.NominationTypeID = Convert.ToInt16(dr[DbTableColumn.NominationTypeID]);
                    submitEmployee.NominatorEmpID = Convert.ToInt16(dr[DbTableColumn.NominatorEmpID]);
                    submittedemployeeList.Add(submitEmployee);
                }
                return submittedemployeeList;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "SubmitNominatedEmployee", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }
        }

        //Jagmohan : 14/09/2015 Start
        #endregion


        #region Confirm Nomination

        /// <summary>
        /// Get the list of all employee nominated by manager for confirmation
        /// </summary>
        /// <returns>mngrid</returns>
        public List<Employee> GetAllSubmittedEmployeeListByCourseID(int trainingnameid, int trainingcourseid)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            Employee confirmEmployee;
            List<Employee> employeeList = new List<Employee>();
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[0].Value =  Convert.ToInt16(HttpContext.Current.Session["EmpID"]);                

                sqlParam[1] = new SqlParameter(SPParameter.TrainingID, SqlDbType.Int);
                sqlParam[1].Value = trainingnameid;

                sqlParam[2] = new SqlParameter(SPParameter.TrainingCourseID, SqlDbType.Int);
                sqlParam[2].Value = trainingcourseid;

                SqlDataReader dr = objGetTraining.ExecuteReaderSP(SPNames.TNI_USP_GetSubmittedNominationListByCourseID, sqlParam);
                while (dr.Read())
                {
                    confirmEmployee = new Employee();
                    confirmEmployee.NominationID = Convert.ToInt16(dr[DbTableColumn.NominationID]);
                    confirmEmployee.EmployeeID = Convert.ToInt16(dr[DbTableColumn.EMPId]);
                    confirmEmployee.EmployeeName = Convert.ToString(dr[DbTableColumn.EmployeeName]);
                    confirmEmployee.EmailID = Convert.ToString(dr[DbTableColumn.EmailId]);
                    confirmEmployee.Designation = Convert.ToString(dr[DbTableColumn.Designation]);
                    confirmEmployee.Project = Convert.ToString(dr[DbTableColumn.Project]);
                    confirmEmployee.Priority = Convert.ToString(dr[DbTableColumn.Priority]);
                    confirmEmployee.PreTraining = Convert.ToString(dr[DbTableColumn.PreTrainingRating]);
                    confirmEmployee.SubmitStatus = Convert.ToString(dr[DbTableColumn.SubmitStatus]);
                    confirmEmployee.Comment = Convert.ToString(dr[DbTableColumn.Comments]);
                    confirmEmployee.ObjectiveForSoftSkill = Convert.ToString(dr[DbTableColumn.ObjectiveForSoftSkill]);
                    confirmEmployee.courseID = Convert.ToInt16(dr[DbTableColumn.CourseID]);
                    confirmEmployee.TrainingNameID = Convert.ToInt16(dr[DbTableColumn.TrainingNameID]);
                    confirmEmployee.TrainingTypeID = Convert.ToInt16(dr[DbTableColumn.TrainingTypeID]);
                    confirmEmployee.NominationTypeID = Convert.ToInt16(dr[DbTableColumn.NominationTypeID]);
                    confirmEmployee.NominatorName = Convert.ToString(dr[DbTableColumn.NominatorName]);
                    confirmEmployee.EffectivenessID = dr[DbTableColumn.EffectivenessID].ToString();
                    confirmEmployee.IsConfirmNomination = true;
                    confirmEmployee.Confirmed = Convert.ToString(dr[DbTableColumn.SubmitStatus]).Equals(CommonConstants.NominationConfirmed, StringComparison.InvariantCulture) ? true : false;
                    employeeList.Add(confirmEmployee);
                }
                return employeeList;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "GetAllSubmittedEmployeeListByCourseID", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }

        }


        /// <summary>
        /// Get datatable of all employee nominated by manager for confirmation
        /// </summary>
        /// <returns>mngrid</returns>
        public DataTable GetDataTableAllSubmittedEmployeeListByCourseID(int trainingnameid, int trainingcourseid)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            DataTable confirmEmployee = new DataTable();                
            try
            {                
                using (SqlConnection conn = new SqlConnection(DBConstants.GetDBConnectionString()))                
                using (SqlDataAdapter sda = new SqlDataAdapter(SPNames.TNI_USP_GetSubmittedNominationListByCourseID, conn))
                {
                    sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                    sda.SelectCommand.Parameters.AddWithValue(SPParameter.EmpId, Convert.ToInt16(HttpContext.Current.Session["EmpID"]));
                    sda.SelectCommand.Parameters.AddWithValue(SPParameter.TrainingID, trainingnameid);
                    sda.SelectCommand.Parameters.AddWithValue(SPParameter.TrainingCourseID, trainingcourseid);
                    sda.Fill(confirmEmployee);
                }                
                return confirmEmployee;             
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "GetAllSubmittedEmployeeListByCourseID", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }

        }


        /// <summary>
        /// Save/Update confirmed nomination
        /// </summary>
        /// <returns>status</returns>
        public List<Employee> SaveUpdateNominatedEmployee(int trainingnameid, int trainingcourseid, string selectedemployeeid)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            List<Employee> confirmNominationList = new List<Employee>();
            Employee newconfirmnomination;
            Employee deleteconfirmnomination;
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter(SPParameter.TrainingID, SqlDbType.Int);
                sqlParam[0].Value = trainingnameid;

                sqlParam[1] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[1].Value = trainingcourseid;

                sqlParam[2] = new SqlParameter(SPParameter.NominatedEmpID, SqlDbType.VarChar);
                sqlParam[2].Value = selectedemployeeid;

                sqlParam[3] = new SqlParameter(SPParameter.Status, SqlDbType.Int);
                sqlParam[3].Direction = ParameterDirection.Output;

                //objGetTraining.ExecuteNonQuerySP(SPNames.TNI_USP_SaveUpdateConfirmNomination, sqlParam);
                

                SqlDataReader dr = objGetTraining.ExecuteReaderSP(SPNames.TNI_USP_SaveUpdateConfirmNomination, sqlParam);
                int i = Convert.ToInt16(sqlParam[3].Value);
                while (dr.Read())
                {
                    newconfirmnomination = new Employee();
                    newconfirmnomination.courseID = Convert.ToInt16(dr[DbTableColumn.CourseID]);
                    newconfirmnomination.TrainingNameID = Convert.ToInt16(dr[DbTableColumn.TrainingNameID]);
                    newconfirmnomination.EmailID = Convert.ToString(dr[DbTableColumn.NominatedEmployeeEmailID]);
                    newconfirmnomination.NominatorEmailID = Convert.ToString(dr[DbTableColumn.NominatorEmailID]);
                    newconfirmnomination.TrainingName = Convert.ToString(dr[DbTableColumn.TrainingName]);
                    newconfirmnomination.SubmitStatus = Convert.ToString(dr[DbTableColumn.SubmitStatus]);
                    newconfirmnomination.courseName = Convert.ToString(dr[DbTableColumn.CourseName]);
                    newconfirmnomination.EmployeeName = Convert.ToString(dr[DbTableColumn.EmployeeName]);
                    newconfirmnomination.StatusID = i;
                    confirmNominationList.Add(newconfirmnomination);
                }

                if (dr.NextResult())
                {
                    while (dr.Read())
                    {
                        deleteconfirmnomination = new Employee();
                        deleteconfirmnomination.courseID = Convert.ToInt16(dr[DbTableColumn.CourseID]);
                        deleteconfirmnomination.TrainingNameID = Convert.ToInt16(dr[DbTableColumn.TrainingNameID]);
                        deleteconfirmnomination.EmailID = Convert.ToString(dr[DbTableColumn.NominatedEmployeeEmailID]);
                        deleteconfirmnomination.NominatorEmailID = Convert.ToString(dr[DbTableColumn.NominatorEmailID]);
                        deleteconfirmnomination.TrainingName = Convert.ToString(dr[DbTableColumn.TrainingName]);
                        deleteconfirmnomination.SubmitStatus = Convert.ToString(dr[DbTableColumn.SubmitStatus]);
                        deleteconfirmnomination.courseName= Convert.ToString(dr[DbTableColumn.CourseName]);
                        deleteconfirmnomination.EmployeeName = Convert.ToString(dr[DbTableColumn.EmployeeName]);
                        deleteconfirmnomination.StatusID = i;
                        confirmNominationList.Add(deleteconfirmnomination);
                    }
                }

                return confirmNominationList;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "SaveUpdateNominatedEmployee", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }

        }




        #endregion

        #region Training Course Methods

        public int SaveTrainingCourse(TrainingCourseModel course)
        {
            int saveStatus = 0;
            int courseId = 0;
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;

            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TrainingCourseSave, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[41];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.OprMode, "I");
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.TrainingCourseId, course.CourseID);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.TrainingTypeID, course.TrainingTypeID);
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.TrainingNameID, course.TrainingNameID);
                sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.TrainingModeID, course.TrainingModeID);
                sqlParam[5] = objCommand.Parameters.AddWithValue(SPParameter.TrainerName, course.TrainerName);
                sqlParam[6] = objCommand.Parameters.AddWithValue(SPParameter.VendorID, course.VendorID);
                sqlParam[7] = objCommand.Parameters.AddWithValue(SPParameter.CourseContentUrl, course.CourseContentFiles);
                sqlParam[8] = objCommand.Parameters.AddWithValue(SPParameter.DARFormUrl, course.DARFormFileName);
                sqlParam[9] = objCommand.Parameters.AddWithValue(SPParameter.TechnicalPanelEmpID, course.TechnicalPanelIds);
                sqlParam[10] = objCommand.Parameters.AddWithValue(SPParameter.TrainingStartDt, course.TrainingStartDate);
                sqlParam[11] = objCommand.Parameters.AddWithValue(SPParameter.TrainingEndDt, course.TrainingEndDate);
                sqlParam[12] = objCommand.Parameters.AddWithValue(SPParameter.TrainingComments, course.TrainingComments);
                sqlParam[13] = objCommand.Parameters.AddWithValue(SPParameter.NoOfDays, course.NoOfdays);
                sqlParam[14] = objCommand.Parameters.AddWithValue(SPParameter.TrainingHours, course.TotalTrainigHours);
                sqlParam[15] = objCommand.Parameters.AddWithValue(SPParameter.NominationTypeID, course.NominationTypeID);
                sqlParam[16] = objCommand.Parameters.AddWithValue(SPParameter.EffectivenessID, course.EffectivenessIds);
                sqlParam[17] = objCommand.Parameters.AddWithValue(SPParameter.SoftwareDetails, course.SoftwareDetails);
                sqlParam[18] = objCommand.Parameters.AddWithValue(SPParameter.TrainingCost, course.TotalCost);
                sqlParam[19] = objCommand.Parameters.AddWithValue(SPParameter.PaymentDueDt, course.PaymentDueDt);
                sqlParam[20] = objCommand.Parameters.AddWithValue(SPParameter.PaymentModeID, course.PaymentModeID);
                sqlParam[21] = objCommand.Parameters.AddWithValue(SPParameter.PaymentCompleteFlag, course.PaymentMade);
                sqlParam[22] = objCommand.Parameters.AddWithValue(SPParameter.PaymentDates, course.PaymentDates);
                sqlParam[23] = objCommand.Parameters.AddWithValue(SPParameter.PaymentComment, course.PaymentComments);
                sqlParam[24] = objCommand.Parameters.AddWithValue(SPParameter.CourseCloseFlag, false);
                sqlParam[25] = objCommand.Parameters.AddWithValue(SPParameter.InviteNominationflag, false);
                sqlParam[26] = objCommand.Parameters.AddWithValue(SPParameter.LastDateOfNomination, null);
                Master m = new Master();
                sqlParam[27] = objCommand.Parameters.AddWithValue(SPParameter.CreatedById, m.GetEmployeeIDByEmailID());
                sqlParam[28] = objCommand.Parameters.AddWithValue(SPParameter.CreatedByDate, DateTime.Now);
                sqlParam[29] = objCommand.Parameters.AddWithValue(SPParameter.LastModifiedById, null);
                sqlParam[30] = objCommand.Parameters.AddWithValue(SPParameter.LastModifiedDate, null);
                sqlParam[31] = objCommand.Parameters.AddWithValue(SPParameter.Status, 0);
                sqlParam[32] = objCommand.Parameters.AddWithValue(SPParameter.RaiseTrainingIds, course.RaiseTrainingIds);
                sqlParam[33] = objCommand.Parameters.AddWithValue(SPParameter.VendorEmail, course.VendorEmailId);
                sqlParam[34] = objCommand.Parameters.AddWithValue(SPParameter.TrainerProfileUrl, course.TrainerProfileFiles);
                sqlParam[35] = objCommand.Parameters.AddWithValue(SPParameter.RequestedBy, course.RequestedBy);
                sqlParam[36] = objCommand.Parameters.AddWithValue(SPParameter.RequestedFor, course.RequestedFor);
                sqlParam[37] = objCommand.Parameters.AddWithValue(SPParameter.TrainingLocation, course.TrainingLocation);
                sqlParam[38] = objCommand.Parameters.AddWithValue(SPParameter.CourseName, course.CourseName);
                sqlParam[39] = objCommand.Parameters.AddWithValue(SPParameter.TrainerNameInternal, course.TrainerNameInternalID);
                sqlParam[40] = objCommand.Parameters.AddWithValue(SPParameter.IndividualPayementTraining, course.IndividualPayementTraining);

                sqlParam[1].Direction = ParameterDirection.Output;
                sqlParam[31].Direction = ParameterDirection.Output;

                objCommand.ExecuteNonQuery();
                saveStatus = Convert.ToInt32(sqlParam[31].Value);
                courseId = Convert.ToInt32(sqlParam[1].Value);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "SaveTrainingCourse", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            return courseId;
        }

        public int SaveFileDetails(TrainingCourseModel course)
        {
            int saveStatus = 0;
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;

            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TrainingCourseFileDetails, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[9];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.CourseId, course.CourseID);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.FileName, course.FileDetails[0].FileName);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.FileGuid, course.FileDetails[0].FileGuid);
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.Category, course.FileDetails[0].Category);
                Master m = new Master();
                sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.CreatedById, m.GetEmployeeIDByEmailID());
                sqlParam[5] = objCommand.Parameters.AddWithValue(SPParameter.CreatedByDate, DateTime.Now);
                sqlParam[8] = objCommand.Parameters.AddWithValue(SPParameter.Status, 0);
                
                sqlParam[8].Direction = ParameterDirection.Output;

                objCommand.ExecuteNonQuery();
                saveStatus = Convert.ToInt32(sqlParam[8].Value);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "SaveFileDetails", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            return saveStatus;
        }

        public int UpdateTrainingCourse(TrainingCourseModel course, string unmappedRaiseIDs, string mappedRaiseIDs)
        {
            int saveStatus = 0;
            int courseId = 0;
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;

            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TrainingCourseSave, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[43];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.OprMode, "U");
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.TrainingCourseId, course.CourseID);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.TrainingTypeID, course.TrainingTypeID);
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.TrainingNameID, course.TrainingNameID);
                sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.TrainingModeID, course.TrainingModeID);
                sqlParam[5] = objCommand.Parameters.AddWithValue(SPParameter.TrainerName, course.TrainerName);
                sqlParam[6] = objCommand.Parameters.AddWithValue(SPParameter.VendorID, course.VendorID);
                sqlParam[7] = objCommand.Parameters.AddWithValue(SPParameter.CourseContentUrl, course.CourseContentFiles);
                sqlParam[8] = objCommand.Parameters.AddWithValue(SPParameter.DARFormUrl, course.DARFormFileName);
                sqlParam[9] = objCommand.Parameters.AddWithValue(SPParameter.TechnicalPanelEmpID, course.TechnicalPanelIds);
                sqlParam[10] = objCommand.Parameters.AddWithValue(SPParameter.TrainingStartDt, course.TrainingStartDate);
                sqlParam[11] = objCommand.Parameters.AddWithValue(SPParameter.TrainingEndDt, course.TrainingEndDate);
                sqlParam[12] = objCommand.Parameters.AddWithValue(SPParameter.TrainingComments, course.TrainingComments);
                sqlParam[13] = objCommand.Parameters.AddWithValue(SPParameter.NoOfDays, course.NoOfdays);
                sqlParam[14] = objCommand.Parameters.AddWithValue(SPParameter.TrainingHours, course.TotalTrainigHours);
                sqlParam[15] = objCommand.Parameters.AddWithValue(SPParameter.NominationTypeID, course.NominationTypeID);
                sqlParam[16] = objCommand.Parameters.AddWithValue(SPParameter.EffectivenessID, course.EffectivenessIds);
                sqlParam[17] = objCommand.Parameters.AddWithValue(SPParameter.SoftwareDetails, course.SoftwareDetails);
                sqlParam[18] = objCommand.Parameters.AddWithValue(SPParameter.TrainingCost, course.TotalCost);
                sqlParam[19] = objCommand.Parameters.AddWithValue(SPParameter.PaymentDueDt, course.PaymentDueDt);
                sqlParam[20] = objCommand.Parameters.AddWithValue(SPParameter.PaymentModeID, course.PaymentModeID);
                sqlParam[21] = objCommand.Parameters.AddWithValue(SPParameter.PaymentCompleteFlag, course.PaymentMade);
                sqlParam[22] = objCommand.Parameters.AddWithValue(SPParameter.PaymentDates, course.PaymentDates);
                sqlParam[23] = objCommand.Parameters.AddWithValue(SPParameter.PaymentComment, course.PaymentComments);
                sqlParam[24] = objCommand.Parameters.AddWithValue(SPParameter.CourseCloseFlag, false);
                sqlParam[25] = objCommand.Parameters.AddWithValue(SPParameter.InviteNominationflag, false);
                sqlParam[26] = objCommand.Parameters.AddWithValue(SPParameter.LastDateOfNomination, null);
                Master m = new Master();
                sqlParam[27] = objCommand.Parameters.AddWithValue(SPParameter.CreatedById, null);
                sqlParam[28] = objCommand.Parameters.AddWithValue(SPParameter.CreatedByDate, null);
                sqlParam[29] = objCommand.Parameters.AddWithValue(SPParameter.LastModifiedById, m.GetEmployeeIDByEmailID());
                sqlParam[30] = objCommand.Parameters.AddWithValue(SPParameter.LastModifiedDate, DateTime.Now);
                sqlParam[31] = objCommand.Parameters.AddWithValue(SPParameter.Status, 0);
                sqlParam[32] = objCommand.Parameters.AddWithValue(SPParameter.RaiseTrainingIds, course.RaiseTrainingIds);
                sqlParam[33] = objCommand.Parameters.AddWithValue(SPParameter.VendorEmail, course.VendorEmailId);
                sqlParam[34] = objCommand.Parameters.AddWithValue(SPParameter.TrainerProfileUrl, course.TrainerProfileFiles);
                sqlParam[35] = objCommand.Parameters.AddWithValue(SPParameter.RequestedBy, course.RequestedBy);
                sqlParam[36] = objCommand.Parameters.AddWithValue(SPParameter.RequestedFor, course.RequestedFor);
                sqlParam[37] = objCommand.Parameters.AddWithValue(SPParameter.TrainingLocation, course.TrainingLocation);
                if (course.CourseName == null)
                {
                    sqlParam[38] = objCommand.Parameters.AddWithValue(SPParameter.CourseName, null);
                }
                else
                {
                    sqlParam[38] = objCommand.Parameters.AddWithValue(SPParameter.CourseName, course.CourseName);
                }
                sqlParam[39] = objCommand.Parameters.AddWithValue(SPParameter.TrainerNameInternal, course.TrainerNameInternalID);
                sqlParam[40] = objCommand.Parameters.AddWithValue(SPParameter.IndividualPayementTraining, course.IndividualPayementTraining);
                if (unmappedRaiseIDs != "")
                {
                    sqlParam[41] = objCommand.Parameters.AddWithValue(SPParameter.UnmappedRaiseIDs, unmappedRaiseIDs);
                }
                else
                {
                    sqlParam[41] = objCommand.Parameters.AddWithValue(SPParameter.UnmappedRaiseIDs, null);
                }
                if (mappedRaiseIDs != "")
                {
                    sqlParam[42] = objCommand.Parameters.AddWithValue(SPParameter.MappedRaiseIDs, mappedRaiseIDs);
                }
                else
                {
                    sqlParam[42] = objCommand.Parameters.AddWithValue(SPParameter.MappedRaiseIDs, null);
                }
                sqlParam[1].Direction = ParameterDirection.InputOutput;
                sqlParam[31].Direction = ParameterDirection.Output;

                objCommand.ExecuteNonQuery();
                saveStatus = Convert.ToInt32(sqlParam[31].Value);
                courseId = Convert.ToInt32(sqlParam[1].Value);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "UpdateTrainingCourse", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            return courseId;
        }


        public int UpdateFileDetails(TrainingCourseModel course)
        {
            int saveStatus = 0;
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;

            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TrainingCourseFileDetails, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[9];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.CourseId, course.CourseID);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.FileName, course.FileDetails[0].FileName);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.FileGuid, course.FileDetails[0].FileGuid);
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.Category, course.FileDetails[0].Category);
                Master m = new Master();
                sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.CreatedById, m.GetEmployeeIDByEmailID());
                sqlParam[5] = objCommand.Parameters.AddWithValue(SPParameter.CreatedByDate, DateTime.Now);
                sqlParam[8] = objCommand.Parameters.AddWithValue(SPParameter.Status, 0);

                sqlParam[8].Direction = ParameterDirection.Output;

                objCommand.ExecuteNonQuery();
                saveStatus = Convert.ToInt32(sqlParam[8].Value);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "UpdateFileDetails", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            return saveStatus;
        }


        public List<CourseDetails> GetCoursesByTrainingTypeId(int trainingTypeId, int courseStatus)
        {
            List<CourseDetails> lstCourses = new List<CourseDetails>();
            DataAccessClass daTraining = new DataAccessClass();

            try
            {
                daTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[2];

                sqlParam[0] = new SqlParameter(SPParameter.TrainingTypeID, SqlDbType.Int);
                sqlParam[0].Value = trainingTypeId;

                sqlParam[1] = new SqlParameter(SPParameter.CourseStatusID, SqlDbType.Int);
                sqlParam[1].Value = courseStatus;

                SqlDataReader dr = daTraining.ExecuteReaderSP(SPNames.GetCoursesByTrainingType, sqlParam);
                while (dr.Read())
                {
                    CourseDetails course = new CourseDetails();
                    course.CourseID = Convert.ToInt32(dr[DbTableColumn.CourseID]);
                    course.CourseName = Convert.ToString(dr[DbTableColumn.CourseName]);
                    course.TrainingNameID = Convert.ToInt32(dr[DbTableColumn.TrainingNameID]);
                    course.TrainingName = Convert.ToString(dr[DbTableColumn.TrainingName]);
                    course.TrainingTypeID = Convert.ToInt32(dr[DbTableColumn.TrainingTypeID]);
                    course.TrainingType = Convert.ToString(dr[DbTableColumn.TrainingType]);
                    course.TrainingStartDate = Convert.ToDateTime(dr[DbTableColumn.TrainingStartDate]);
                    course.TrainingEndDate = Convert.ToDateTime(dr[DbTableColumn.TrainingEndDate]);
                    course.CourseCloseFlag = Convert.ToBoolean(dr[DbTableColumn.CourseCloseFlag]);
                    course.InviteNominationFlag = Convert.ToBoolean(dr[DbTableColumn.InviteNominationFlag]);
                    course.Status = Convert.ToString(dr[DbTableColumn.Status]);
                    course.LastNominationDate = (Convert.ToString(dr[DbTableColumn.NominationDueDate]) == "") ? (DateTime?)null : Convert.ToDateTime(dr[DbTableColumn.NominationDueDate]);
                    course.IsActive = Convert.ToInt32(dr[DbTableColumn.CourseIsActive]);
                    course.IsAssessment = Convert.ToInt32(dr[DbTableColumn.IsAssessment]);
                    //Harsha- Issue Id- 58975 & 58958 - Start
                    //Description- Making Training Cost Editable for Internal Training after Closed status of the training
                    //Disabling Number of Hours and Days field once the after training's status is feedback sent
                    course.TrainingModeId = Convert.ToInt32(dr[DbTableColumn.TrainingModeID]);
                    course.TrainingStatusId = Convert.ToInt32(dr[DbTableColumn.TrainingStatusId]);
                    //Harsha- Issue Id- 58975 & 58958 - End
                    lstCourses.Add(course);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetCoursesByTrainingTypeId", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                daTraining.CloseConncetion();
            }
            return lstCourses;
        }


        public TrainingCourseModel GetTrainingCourses(int courseId)
        {
            TrainingCourseModel course = new TrainingCourseModel();
            DataAccessClass daTraining = new DataAccessClass();
            course.FileDetails = new List<FileDetails>();
            course.AttendanceDates = new List<DateTime>();

            try
            {
                daTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value = courseId;

                SqlDataReader dr = daTraining.ExecuteReaderSP(SPNames.GetTrainingCourses, sqlParam);
                while (dr.Read())
                {
                    course.CourseID = Convert.ToInt32(dr[DbTableColumn.CourseID]);
                    course.TrainingNameID = Convert.ToInt32(dr[DbTableColumn.TrainingNameID]);
                    course.TrainingName = Convert.ToString(dr[DbTableColumn.TrainingName]);
                    course.TrainingTypeID = Convert.ToInt32(dr[DbTableColumn.TrainingTypeID]);
                    course.TrainingModeID = Convert.ToInt32(dr[DbTableColumn.TrainingModeID]);
                    course.VendorID = (Convert.ToString(dr[DbTableColumn.VendorID]) == "") ? (int?)null : Convert.ToInt32(dr[DbTableColumn.VendorID]);
                    course.VendorEmailId = Convert.ToString(dr[DbTableColumn.VendorEmailId]);
                    course.CourseContentFiles = Convert.ToString(dr[DbTableColumn.CourseContent]);
                    course.DARFormFileName = Convert.ToString(dr[DbTableColumn.DARForm]);
                    course.TrainerProfileFiles = Convert.ToString(dr[DbTableColumn.TrainerProfile]);
                    course.TechnicalPanelIds = Convert.ToString(dr[DbTableColumn.TechnicalPanelIds]);
                    course.TechnicalPanelNames = Convert.ToString(dr[DbTableColumn.TechnicalPanelNames]);
                    course.TrainerName = Convert.ToString(dr[DbTableColumn.TrainerName]);
                    course.TrainingStartDate = Convert.ToDateTime(dr[DbTableColumn.TrainingStartDate]);
                    course.TrainingEndDate = Convert.ToDateTime(dr[DbTableColumn.TrainingEndDate]);
                    course.TrainingComments = Convert.ToString(dr[DbTableColumn.Comments]);
                    course.NoOfdays = Convert.ToSingle(dr[DbTableColumn.NoOfDays]);
                    course.TotalTrainigHours = Convert.ToSingle(dr[DbTableColumn.TotalTrainingHours]);
                    course.NominationTypeID = Convert.ToInt32(dr[DbTableColumn.NominationTypeID]);
                    course.EffectivenessIds = Convert.ToString(dr[DbTableColumn.EffectivenessID]);
                    course.SoftwareDetails = Convert.ToString(dr[DbTableColumn.SoftwareDetails]);
                    course.TotalCost = (Convert.ToString(dr[DbTableColumn.TrainingCost]) == "") ? (float?)null : Convert.ToSingle(dr[DbTableColumn.TrainingCost]);
                    course.PaymentDueDt = (Convert.ToString(dr[DbTableColumn.PaymentDueDate]) == "") ? (DateTime?)null : Convert.ToDateTime(dr[DbTableColumn.PaymentDueDate]);
                    course.PaymentMade = (Convert.ToBoolean(dr[DbTableColumn.PaymentMade]));                    
                    if (dr[DbTableColumn.IndividualPayementTraining].ToString() == "")
                    {
                        course.IndividualPayementTraining = false;
                    }
                    else
                    {
                        course.IndividualPayementTraining = Convert.ToBoolean(dr[DbTableColumn.IndividualPayementTraining]);
                    }
                    course.PaymentDates = (Convert.ToString(dr[DbTableColumn.PaymentDates]) == "") ? (DateTime?)null : Convert.ToDateTime(dr[DbTableColumn.PaymentDates]);
                    course.PaymentComments = Convert.ToString(dr[DbTableColumn.PaymentComment]);
                    course.PaymentModeID = Convert.ToInt32(dr[DbTableColumn.PaymentModeID]);
                    course.LastDateOfNomination = (Convert.ToString(dr[DbTableColumn.NominationDueDate]) == "") ? (DateTime?)null : Convert.ToDateTime(dr[DbTableColumn.NominationDueDate]);
                    course.IsActive = Convert.ToBoolean(dr[DbTableColumn.IsActive]);
                    course.RaiseTrainingIds = Convert.ToString(dr[DbTableColumn.RaiseTrainingIds]);
                    course.RequestedBy = Convert.ToString(dr[DbTableColumn.RequestedBy]);
                    course.RequestedFor = Convert.ToString(dr[DbTableColumn.RequestedFor]);
                    course.TrainingLocation = Convert.ToString(dr[DbTableColumn.TrainingLocation]);
                    course.CourseName = Convert.ToString(dr[DbTableColumn.CourseName]);
                    if (dr[DbTableColumn.Status].ToString() == "0")
                    {
                        course.FeedbackSent = false;
                    }
                    else
                    {
                        course.FeedbackSent = true;
                    }
                        course.TrainerNameInternal = Convert.ToString(dr[DbTableColumn.TrainerNameInternal]);
                    course.TrainerNameInternalID = Convert.ToString(dr[DbTableColumn.TrainerNameInternalID]);
                    //Harsha Issue Id- 58975 and 58958 - Start       
                    course.TrainingStatus = Convert.ToInt32(dr[DbTableColumn.TrainingStatus]);
                    //Harsha Issue Id- 58975 and 58958 - End
                }
                if (dr.NextResult())
                {
                    while (dr.Read())
                    {
                        FileDetails fileDetailsModel = new FileDetails();
                        fileDetailsModel.FileId = Convert.ToInt32(dr[DbTableColumn.FileID]);
                        fileDetailsModel.FileName = Convert.ToString(dr[DbTableColumn.FileName]);
                        fileDetailsModel.FileGuid = Convert.ToString(dr[DbTableColumn.FileNameGuid]);
                        fileDetailsModel.Category = Convert.ToString(dr[DbTableColumn.Category]);
                        course.FileDetails.Add(fileDetailsModel);
                    }
                }
                if (dr.NextResult())
                {
                    while (dr.Read())
                    {
                        course.AttendanceDate = Convert.ToDateTime(dr[DbTableColumn.AttendanceDate]);
                        course.AttendanceDates.Add(course.AttendanceDate);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetTrainingCourses", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                daTraining.CloseConncetion();
            }
            return course;
        }

        public AttendanceModel GetSeminarKSSCourse(int CourseId)
        {
            AttendanceModel c = new AttendanceModel();
            TrainingModel course = new TrainingModel();
            DataAccessClass daTraining = new DataAccessClass();

            try
            {
                daTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value = CourseId;

                SqlDataReader dr = daTraining.ExecuteReaderSP(SPNames.GetSeminarKSSCourse, sqlParam);
                while (dr.Read())
                {
                    
                    c.TrainingNameSemiKss = dr[DbTableColumn.Name].ToString();
                    
                    c.TrainingStartDate = Convert.ToDateTime(dr[DbTableColumn.Date]);
                    
                    c.TrainingEndDate = Convert.ToDateTime(dr[DbTableColumn.SeminarsEndDate]);
                    

                }
                if (dr.NextResult())
                {
                    while (dr.Read())
                    {
                        c.AttendanceDate = dr[DbTableColumn.AttendanceDate].ToString();
                       // course.AttendanceDate = Convert.ToDateTime(dr[DbTableColumn.AttendanceDate]);
                        c.AttendanceDates = new List<DateTime>();
                        c.AttendanceDates.Add(Convert.ToDateTime(c.AttendanceDate));
                        //course.AttendanceDates.Add(course.AttendanceDate);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetTrainingCourses", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                daTraining.CloseConncetion();
            }
            return c;
        }

        public List<SelectListItem> GetApprovedTrainingNameByTrainingType(int trainingTypeId, bool defaultValue, string selVal)
        {
            List<SelectListItem> newList = new List<SelectListItem>();
            DataAccessClass daTraining = new DataAccessClass();
            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                daTraining.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.TrainingTypeID, SqlDbType.Int);
                sqlParam[0].Value = trainingTypeId;

                SqlDataReader dr = daTraining.ExecuteReaderSP(SPNames.ApprovedTrainingName, sqlParam);

                SelectListItem selListItem;
                if (defaultValue) { newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "0"}); }

                while (dr.Read())
                {
                    selListItem = new SelectListItem() { Value = dr[0].ToString().Trim(), Text = dr[1].ToString().Trim(), Selected = (dr[0].ToString().Trim()==selVal)?true:false };

                    newList.Add(selListItem);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetApprovedTrainingNameByTrainingType", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                daTraining.CloseConncetion();
            }
            return newList;
        }

        public Vendor GetVendorDetails(int vendorId)
        {
            Vendor vendorDetails = new Vendor();
            DataAccessClass daTraining = new DataAccessClass();
            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                daTraining.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.VendorID, SqlDbType.Int);
                sqlParam[0].Value = vendorId;

                SqlDataReader dr = daTraining.ExecuteReaderSP(SPNames.GetTrainingVendorEmail, sqlParam);

                while (dr.Read())
                {
                    vendorDetails.VendorID = Convert.ToInt32(dr[0]);
                    vendorDetails.VendorName = Convert.ToString(dr[1]);
                    vendorDetails.VendorEmail = Convert.ToString(dr[2]);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetApprovedTrainingNameByTrainingType", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                daTraining.CloseConncetion();
            }
            return vendorDetails;
        }

        public List<TrainingModel> GetApprovedRaiseTrainings(int trainingNameId,int trainingStatusId)
        {
            List<TrainingModel> lstRaiseReq = new List<TrainingModel>();
            DataAccessClass daTraining = new DataAccessClass();

            try
            {
                daTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[2];

                sqlParam[0] = new SqlParameter(SPParameter.TrainingNameID, SqlDbType.Int);
                sqlParam[0].Value = trainingNameId;
                sqlParam[1] = new SqlParameter(SPParameter.TrainingStatusID, SqlDbType.Int);
                sqlParam[1].Value = trainingStatusId;

                SqlDataReader dr = daTraining.ExecuteReaderSP(SPNames.GetRaiseTrainingsByName, sqlParam);
                while (dr.Read())
                {
                    TrainingModel raise = new TrainingModel();
                    raise.RaiseID = Convert.ToInt32(dr[DbTableColumn.RaiseID]);
                    raise.TrainingNameID = Convert.ToInt32(dr[DbTableColumn.TrainingNameID]);
                    raise.TrainingName = Convert.ToString(dr[DbTableColumn.TrainingName]);
                    raise.TrainingType = Convert.ToString(dr[DbTableColumn.TrainingTypeID]);
                    raise.Status = Convert.ToString(dr[DbTableColumn.Status]);
                    raise.QuarterNo = Convert.ToInt32(dr[DbTableColumn.Quarter]);
                    //raise.QuarterValue = CommonRepository.QuarterValueById(Convert.ToInt32(dr[DbTableColumn.Quarter])); 
                    //raise.RaiseYear = Convert.ToInt32(dr[DbTableColumn.RaiseYear]);
                    raise.CreatedDate = Convert.ToDateTime(dr[DbTableColumn.CreatedDate]);
                    raise.RequestedBy = Convert.ToString(dr[DbTableColumn.RaiseByName]);
                    //raise.IsSelected = raiseIds.Contains(Convert.ToString(dr[DbTableColumn.RaiseID])) ? true : false;
                    lstRaiseReq.Add(raise);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetApprovedRaiseTrainings", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                daTraining.CloseConncetion();
            }
            return lstRaiseReq;
        }

        public List<TrainingModel> GetRaiseTrainings(string raiseIds, int trainingNameId)
        {
            List<TrainingModel> lstRaiseTraining = new List<TrainingModel>();
            DataAccessClass daTraining = new DataAccessClass();

            try
            {
                daTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[2];

                sqlParam[0] = new SqlParameter(SPParameter.RaiseTrainingIds, SqlDbType.VarChar);
                sqlParam[0].Value = raiseIds;

                sqlParam[1] = new SqlParameter(SPParameter.TrainingNameID, SqlDbType.Int);
                sqlParam[1].Value = trainingNameId;

                SqlDataReader dr = daTraining.ExecuteReaderSP(SPNames.GetRaiseTrainingDetails, sqlParam);
                while (dr.Read())
                {
                    TrainingModel raiseTraining = new TrainingModel();
                    raiseTraining.RaiseID = Convert.ToInt32(dr[DbTableColumn.RaiseID]);
                    raiseTraining.TrainingNameID = Convert.ToInt32(dr[DbTableColumn.TrainingNameID]);
                    raiseTraining.TrainingName = Convert.ToString(dr[DbTableColumn.TrainingName]);
                    raiseTraining.TrainingType = Convert.ToString(dr[DbTableColumn.TrainingTypeID]);
                    raiseTraining.Status = Convert.ToString(dr[DbTableColumn.Status]);
                    raiseTraining.QuarterNo = Convert.ToInt32(dr[DbTableColumn.Quarter]);
                    raiseTraining.CreatedDate = Convert.ToDateTime(dr[DbTableColumn.CreatedDate]);
                    raiseTraining.RequestedBy = Convert.ToString(dr[DbTableColumn.RaiseByName]);
                    lstRaiseTraining.Add(raiseTraining);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetRaiseTrainings", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                daTraining.CloseConncetion();
            }
            return lstRaiseTraining;
        }

        public List<TrainingModel> GetRaiseTrainingsById(string raiseIds)
        {
            List<TrainingModel> lstRaiseTraining = new List<TrainingModel>();
            DataAccessClass daTraining = new DataAccessClass();

            try
            {
                daTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.RaiseTrainingIds, SqlDbType.VarChar);
                sqlParam[0].Value = raiseIds;

                SqlDataReader dr = daTraining.ExecuteReaderSP(SPNames.GetRaiseTrainingsById, sqlParam);
                while (dr.Read())
                {
                    TrainingModel raiseTraining = new TrainingModel();
                    raiseTraining.RaiseID = Convert.ToInt32(dr[DbTableColumn.RaiseID]);
                    raiseTraining.TrainingNameID = Convert.ToInt32(dr[DbTableColumn.TrainingNameID]);
                    raiseTraining.TrainingName = Convert.ToString(dr[DbTableColumn.TrainingName]);
                    raiseTraining.TrainingType = Convert.ToString(dr[DbTableColumn.TrainingTypeID]);
                    raiseTraining.Status = Convert.ToString(dr[DbTableColumn.Status]);
                    raiseTraining.QuarterNo = Convert.ToInt32(dr[DbTableColumn.Quarter]);
                    raiseTraining.CreatedDate = Convert.ToDateTime(dr[DbTableColumn.CreatedDate]);
                    raiseTraining.RequestedBy = Convert.ToString(dr[DbTableColumn.RaiseByName]);
                    lstRaiseTraining.Add(raiseTraining);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetRaiseTrainings", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                daTraining.CloseConncetion();
            }
            return lstRaiseTraining;
        }

        public int DeleteTrainingCourse(int courseId)
        {
            int deleteStatus = 0;
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;

            try
            {
                Master m = new Master();
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TrainingCourseDelete, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.TrainingCourseId, courseId);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.Status, 0);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.LastModifiedById,m.GetEmployeeIDByEmailID());
                sqlParam[1].Direction = ParameterDirection.Output;

                objCommand.ExecuteNonQuery();
                deleteStatus = Convert.ToInt32(sqlParam[1].Value);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "UpdateTrainingCourse", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            return deleteStatus;
        }

        public int CloseTrainingCourse(int courseId, string Prompt)
        {
            int closeStatus = 0;
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;

            try
            {
                Master m = new Master();
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TrainingCourseClose, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.TrainingCourseId, courseId);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.Status, 0);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.LastModifiedById, m.GetEmployeeIDByEmailID());
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.Prompt, Prompt);

                sqlParam[1].Direction = ParameterDirection.Output;

                objCommand.ExecuteNonQuery();
                closeStatus = Convert.ToInt32(sqlParam[1].Value);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "UpdateTrainingCourse", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            return closeStatus;
        }

        public TrainingCourseModel GetInvoiceDetailsByCourseId(string module, string courseId)
        {
            TrainingCourseModel course = new TrainingCourseModel();
            DataAccessClass daTraining = new DataAccessClass();
            course.FileDetails = new List<FileDetails>();

            string filePath = "";
            try
            {
                daTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value = courseId;
                
                SqlDataReader dr = daTraining.ExecuteReaderSP(SPNames.GetInvoiceFilePath, sqlParam);
                while (dr.Read())
                {
                    FileDetails fileDetailsModel = new FileDetails();
                    fileDetailsModel.FileId = Convert.ToInt32(dr[DbTableColumn.FileID]);
                    fileDetailsModel.FileName = Convert.ToString(dr[DbTableColumn.FileName]);
                    fileDetailsModel.FileGuid = Convert.ToString(dr[DbTableColumn.FileNameGuid]);
                    fileDetailsModel.Category = Convert.ToString(dr[DbTableColumn.Category]);
                    course.FileDetails.Add(fileDetailsModel);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetInvoiceDetailsByCourseId", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                daTraining.CloseConncetion();
            }
            return course;
        }

        public string GetFileDetailsByCourseId(string module, string courseId)
        {
            DataAccessClass daTraining = new DataAccessClass();
            string filePath = "";
            try
            {
                daTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[2];

                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value = courseId;
                sqlParam[1] = new SqlParameter(SPParameter.ModuleName, SqlDbType.VarChar);
                sqlParam[1].Value = module;

                SqlDataReader dr = daTraining.ExecuteReaderSP(SPNames.GetCourseFilePath, sqlParam);
                while (dr.Read())
                {
                    filePath = Convert.ToString(dr[DbTableColumn.FilePath]);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetFileDetailsByCourseId", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                daTraining.CloseConncetion();
            }
            return filePath;
        }

        public int UpdateFileDetails(string module, string courseId, string filePath)
        {
            int updateStatus = 0;
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;

            try
            {
                Master m = new Master();
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.UpdateCourseFilePath, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[5];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.TrainingCourseId, courseId);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.ModuleName, module );
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.FilePath, filePath);
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.Status, 0);
                sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.LastModifiedById, m.GetEmployeeIDByEmailID());

                sqlParam[3].Direction = ParameterDirection.Output;

                objCommand.ExecuteNonQuery();
                updateStatus = Convert.ToInt32(sqlParam[3].Value);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "UpdateFileDetails", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            return updateStatus;
        }

        public int updateInvoiceDetails(string fileId,string module, string courseId, string fileName)
        {
            int updateStatus = 0;
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;

            try
            {
                Master m = new Master();
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.UpdateInvoiceFilePath, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.FileID, fileId);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.LastModifiedById, m.GetEmployeeIDByEmailID());
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.Status, 0);

                sqlParam[2].Direction = ParameterDirection.Output;

                objCommand.ExecuteNonQuery();
                updateStatus = Convert.ToInt32(sqlParam[2].Value);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "UpdateInvoiceDetails", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            return updateStatus;
        }

        public InviteNominationModel GetInviteNominationDetails(int courseId)
        {
            InviteNominationModel objInviteNomination = new InviteNominationModel();
            DataAccessClass daTraining = new DataAccessClass();

            try
            {
                daTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.TrainingCourseID, SqlDbType.VarChar);
                sqlParam[0].Value = courseId;

                SqlDataReader dr = daTraining.ExecuteReaderSP(SPNames.GetInviteNominationDetails, sqlParam);
                while (dr.Read())
                {
                    objInviteNomination.CourseID = Convert.ToInt32(dr[DbTableColumn.CourseID]);
                    objInviteNomination.CourseName = Convert.ToString(dr[DbTableColumn.CourseName]);
                    objInviteNomination.TrainingStartDate = Convert.ToDateTime(dr[DbTableColumn.TrainingStartDate]);
                    objInviteNomination.TrainingEndDate = Convert.ToDateTime(dr[DbTableColumn.TrainingEndDate]);
                    objInviteNomination.NominationEndDate = (Convert.ToString(dr[DbTableColumn.NominationDueDate]) == "") ? (DateTime?)null : Convert.ToDateTime(dr[DbTableColumn.NominationDueDate]);
                    objInviteNomination.TrainingName = Convert.ToString(dr[DbTableColumn.TrainingName]);
                    objInviteNomination.TrainingMode = (Convert.ToString(dr[DbTableColumn.TrainingModeID]) == CommonConstants.ExternalTrainer.ToString()) ? "External" : "Internal";
                    objInviteNomination.TrainerName = Convert.ToString(dr[DbTableColumn.TrainerName]);
                    objInviteNomination.NoOfDays = Convert.ToSingle(dr[DbTableColumn.NoOfDays]);
                    objInviteNomination.InviteNominationFlag = Convert.ToBoolean(dr[DbTableColumn.InviteNominationFlag]);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetInviteNominationDetails", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                daTraining.CloseConncetion();
            }
            return objInviteNomination;
        }

        public int UpdateCourseNominationDate(DateTime nominationDate, int courseId)
        {
            int updateStatus = 0;
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;

            try
            {
                Master m = new Master();
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.UpdateCourseNominationDate, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.TrainingCourseID, courseId);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.LastDateOfNomination, nominationDate);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.Status, 0);
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.LastModifiedById, m.GetEmployeeIDByEmailID());

                sqlParam[2].Direction = ParameterDirection.Output;

                objCommand.ExecuteNonQuery();
                updateStatus = Convert.ToInt32(sqlParam[2].Value);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "UpdateCourseNominationDate", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            return updateStatus;
        }

        public int UpdateCoursePayment(TrainingCourseModel course)
        {
            int updateStatus = 0;
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;

            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.CoursePaymentUpdate, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[12];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.TrainingCourseId, course.CourseID);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.TrainingCost, course.TotalCost);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.PaymentDueDt, course.PaymentDueDt);
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.PaymentModeID, course.PaymentModeID);
                sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.PaymentCompleteFlag, course.PaymentMade);
                sqlParam[5] = objCommand.Parameters.AddWithValue(SPParameter.PaymentDates, course.PaymentDates);
                sqlParam[6] = objCommand.Parameters.AddWithValue(SPParameter.PaymentComment, course.PaymentComments);
                Master m = new Master();
                sqlParam[7] = objCommand.Parameters.AddWithValue(SPParameter.LastModifiedById, m.GetEmployeeIDByEmailID());
                sqlParam[8] = objCommand.Parameters.AddWithValue(SPParameter.TrainingStartDt, course.TrainingStartDate);
                sqlParam[9] = objCommand.Parameters.AddWithValue(SPParameter.TrainingEndDt, course.TrainingEndDate);
                sqlParam[10] = objCommand.Parameters.AddWithValue(SPParameter.Status, 0);

                sqlParam[10].Direction = ParameterDirection.Output;
                sqlParam[11] = objCommand.Parameters.AddWithValue(SPParameter.IndividualPayementTraining, course.IndividualPayementTraining);


                objCommand.ExecuteNonQuery();
                updateStatus = Convert.ToInt32(sqlParam[10].Value);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "UpdateCoursePayment", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            return updateStatus;
        }

        public List<TraingCourse> GetOpenTrainingCourses()
        {

            List<TraingCourse> traingCourses = new List<TraingCourse>();
            DataAccessClass daTraining = new DataAccessClass();

            try
            {
                daTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlDataReader dr = daTraining.ExecuteReaderSP(SPNames.GetOpenTrainingCourses);
                while (dr.Read())
                {
                    TraingCourse traingCourse = new TraingCourse();
                    traingCourse.CourseId = Convert.ToInt32(dr[DbTableColumn.CourseID]);
                    traingCourse.CourseName = Convert.ToString(dr[DbTableColumn.TrainingName]);
                    traingCourses.Add(traingCourse);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetOpenTrainingCourses", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                daTraining.CloseConncetion();
            }
            return traingCourses;
        }

        public List<TraingCourse> GetTrainingCoursesByTrainingNameId(int courseId, int trainingNameId)
        {

            List<TraingCourse> traingCourses = new List<TraingCourse>();
            DataAccessClass daTraining = new DataAccessClass();

            try
            {
                daTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter(SPParameter.TrainingNameID, SqlDbType.Int);
                sqlParam[0].Value = trainingNameId;
                sqlParam[1] = new SqlParameter(SPParameter.CourseId, SqlDbType.Int);
                sqlParam[1].Value = courseId;
                
                SqlDataReader dr = daTraining.ExecuteReaderSP(SPNames.GetTrainingByTrainingNameId,sqlParam);
              
                while (dr.Read())
                {
                    TraingCourse traingCourse = new TraingCourse();
                    traingCourse.CourseId = Convert.ToInt32(dr[DbTableColumn.CourseID]);
                    traingCourse.CourseName = Convert.ToString(dr[DbTableColumn.TrainingName]);
                    traingCourses.Add(traingCourse);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetTrainingCoursesByTrainingNameId", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                daTraining.CloseConncetion();
            }
            return traingCourses;
        }


        public string GetDuplicateStatusOfCoursName(string CourseName)
        {
            DataAccessClass objGetDuplicateStatusOfCoursName = new DataAccessClass();
            string duplicateStatus = string.Empty;
            try
            {
                objGetDuplicateStatusOfCoursName.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.CourseName, SqlDbType.Text);
                sqlParam[0].Value = CourseName;

                ds = new DataSet();
                ds = objGetDuplicateStatusOfCoursName.GetDataSet(SPNames.GetDuplicateStatusOfCourseName, sqlParam);
                duplicateStatus = ds.Tables[0].Rows[0]["duplicateStatus"].ToString();
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "objGetDuplicateStatusOfCoursName", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetDuplicateStatusOfCoursName.CloseConncetion();
            }
            return duplicateStatus;
        }

        public List<TraingCourse> GetTrainingCoursesPageWise(string strCoursePageFor, int Empid)
        {

            List<TraingCourse> traingCourses = new List<TraingCourse>();
            DataAccessClass daTraining = new DataAccessClass();

            try
            {
                daTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[2];

                sqlParam[0] = new SqlParameter(SPParameter.PageName, SqlDbType.Text);
                sqlParam[0].Value = strCoursePageFor;
                sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int );
                sqlParam[1].Value = Empid;
                SqlDataReader dr = daTraining.ExecuteReaderSP(SPNames.GetTrainingCoursesPageWise, sqlParam);
                while (dr.Read())
                {
                    TraingCourse traingCourse = new TraingCourse();
                    traingCourse.CourseId = Convert.ToInt32(dr[DbTableColumn.CourseID]);
                    traingCourse.CourseName = Convert.ToString(dr[DbTableColumn.CourseName]);
                    traingCourses.Add(traingCourse);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetTrainingCoursesPageWise", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                daTraining.CloseConncetion();
            }
            return traingCourses;
        }


        #endregion Training Course Methods

        #region AccomodationFoodDetails

        public int SaveAccomodationAndFoodDetails(NominationModel objNominationModel)
        {
            int saveStatus = 0;
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;

            try
            {
                objConnection = new SqlConnection(DBConstants.GetDBConnectionString());
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.SaveAccomodationFoodDetails, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[11];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.CourseID, objNominationModel.CourseID);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.IsAccomodationTrainer, objNominationModel.IsAccomodationTrainer);
                if (objNominationModel.AccomodationFromDate.ToString() != "1/01/0001 12:00:00 AM" && objNominationModel.AccomodationFromDate.ToString()!="")
                {
                    sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.AccomodationFromDate, objNominationModel.AccomodationFromDate);
                }
                else
                    sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.AccomodationFromDate, DBNull.Value);

                if (objNominationModel.AccomodationToDate.ToString() != "1/01/0001 12:00:00 AM" && objNominationModel.AccomodationToDate.ToString() !="")
                {
                    sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.AccomodationToDate, objNominationModel.AccomodationToDate);
                }
                else
                {
                    sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.AccomodationToDate, DBNull.Value);
                }
                sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.IsTravelDetailsTrainer, objNominationModel.IsTravelDetailsTrainer);
                sqlParam[5] = objCommand.Parameters.AddWithValue(SPParameter.IsFoodTrainer, objNominationModel.IsFoodTrainer);
                sqlParam[6] = objCommand.Parameters.AddWithValue(SPParameter.TrainerPreference, String.IsNullOrEmpty(objNominationModel.TrainerPreference) ? string.Empty : objNominationModel.TrainerPreference);
                sqlParam[7] = objCommand.Parameters.AddWithValue(SPParameter.IsFoodParticipants, objNominationModel.IsFoodParticipants);
                sqlParam[8] = objCommand.Parameters.AddWithValue(SPParameter.ParticipantsPreference, String.IsNullOrEmpty(objNominationModel.ParticipantsPreference) ? string.Empty : objNominationModel.ParticipantsPreference);
                sqlParam[9] = objCommand.Parameters.AddWithValue(SPParameter.CommentsFoodAccomodaion, String.IsNullOrEmpty(objNominationModel.CommentsFoodAccomodaion) ? string.Empty : objNominationModel.CommentsFoodAccomodaion); ;
                sqlParam[10] = objCommand.Parameters.AddWithValue(SPParameter.IsSendMail, objNominationModel.IsSendMail);

                saveStatus = objCommand.ExecuteNonQuery();
                
            }

            catch (Exception ex)
                {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "SaveAccomodationFoodDetails", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
                }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            return saveStatus;
                }

        public int SaveTravelDetails(TravelDetailsModel objTravelDetailModel)
                {
            int insertStatus = 0;
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;

            try
                {
                objConnection = new SqlConnection(DBConstants.GetDBConnectionString());
                objConnection.Open();

                
                SqlParameter[] sqlParam = new SqlParameter[5];

                
                objCommand = new SqlCommand(SPNames.SaveTravelDetails, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.CourseID, objTravelDetailModel.CourseID);
                sqlParam[1] = objCommand.Parameters.Add(SPParameter.TravelDetailID, objTravelDetailModel.TravelDetailID);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.FromLocation, objTravelDetailModel.FromLocation);
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.ToLocation, objTravelDetailModel.ToLocation);
                sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.DateOfTravel, objTravelDetailModel.Date);

                //if (objTravelDetailModel.TravelDetailID == 0)
                //{
                //    int TravelDetailID = Convert.ToInt32(objCommand.Parameters[SPParameter.TravelDetailID].Value.ToString());
                //}
                //else
                //{
                    
                //}

                insertStatus = objCommand.ExecuteNonQuery();
                //insertStatus = objCommand.ExecuteNonQuery();
                
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "SaveAccomodationFoodDetails", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }

            return insertStatus;

            
        }

        public DataSet GetAccomodationDetailsByCourseID(int CourseID)
        {
            DataAccessClass objGetAccomodationDetails = new DataAccessClass();

            try
            {
                objGetAccomodationDetails.OpenConnection(DBConstants.GetDBConnectionString());
                
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value = CourseID;
                
                ds = new DataSet();
                ds = objGetAccomodationDetails.GetDataSet(SPNames.TNI_GetAccomodationDetailsByCourseID, sqlParam);

                return ds;
            

            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetAccomodationDetailsByCourseID", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetAccomodationDetails.CloseConncetion();
            }
            
        }

        public DataSet GetTravelDetailsByCourseID(int CourseID)
        {
            DataAccessClass objGetAccomodationDetails = new DataAccessClass();

            try
            {
                objGetAccomodationDetails.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value = CourseID;
                //sqlParam[1] = new SqlParameter(SPParameter.TravelDetailID, SqlDbType.Int)
                //sqlParam[1].Direction = ParameterDirection.Output;

                ds = new DataSet();
                ds = objGetAccomodationDetails.GetDataSet(SPNames.GetTravelDetailsByCourseID, sqlParam);
                
                return ds;


            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetAccomodationDetailsByCourseID", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetAccomodationDetails.CloseConncetion();
            }
        }
            

        public string GetTrainingNameByCourseID(int CourseID)
        {
            DataAccessClass objGetAccomodationDetails = new DataAccessClass();
            string CourseName = string.Empty;
            try
            {
                objGetAccomodationDetails.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value = CourseID;

                ds = new DataSet();
                ds = objGetAccomodationDetails.GetDataSet(SPNames.GetTrainingNameByCourseID, sqlParam);

                CourseName = ds.Tables[0].Rows[0]["TrainingName"].ToString();

        }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetTrainingNameByCourseID", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetAccomodationDetails.CloseConncetion();
            }
            return CourseName;
        }

        #endregion AccomodationFoodDetails

        #region FeedbackDetails
        public DataSet GetQuestionDescription()
        {
            DataAccessClass objGetQuestionDescription = new DataAccessClass();
            string Description = string.Empty;
            try
            {
                objGetQuestionDescription.OpenConnection(DBConstants.GetDBConnectionString());
                //SqlParameter[] sqlParam = new SqlParameter[1];

                //sqlParam[0] = new SqlParameter(SPParameter.QuestionID, SqlDbType.Int);
                //sqlParam[0].Value = QuestionID;

                ds = new DataSet();
                ds = objGetQuestionDescription.GetDataSet(SPNames.GetDescriptionByQuestionID);

            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetQuestionDescription", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetQuestionDescription.CloseConncetion();
            }
            return ds;
        }


        public List<KeyValuePair<int, string>> GetTrainingNameList(string empId) //Feedback page for employee
        {

            DataAccessClass objGetTrainingName = new DataAccessClass();
            List<KeyValuePair<int, string>> trainingList = new List<KeyValuePair<int, string>>();
            try
            {
                objGetTrainingName.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[0].Value = empId;

                SqlDataReader dr = objGetTrainingName.ExecuteReaderSP(SPNames.GetTrainingNameList, sqlParam);

                while (dr.Read())
                {
                    trainingList.Add(new KeyValuePair<int, string>(Convert.ToInt32(dr[DbTableColumn.CourseID]), Convert.ToString(dr[DbTableColumn.CourseName])));
                }
                return trainingList;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "GetTrainingNameList", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTrainingName.CloseConncetion();
            }
        }

        public List<KeyValuePair<int, string>> GetTrainingNameList() //Feedback Page for RMO
        {

            DataAccessClass objGetTrainingName = new DataAccessClass();
            List<KeyValuePair<int, string>> trainingList = new List<KeyValuePair<int, string>>();
            try
            {
                objGetTrainingName.OpenConnection(DBConstants.GetDBConnectionString());
                //SqlParameter[] sqlParam = new SqlParameter[1];
                //sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                //sqlParam[0].Value = empId;

                SqlDataReader dr = objGetTrainingName.ExecuteReaderSP(SPNames.GetTrainingNameListForRMO);

                while (dr.Read())
                {
                    trainingList.Add(new KeyValuePair<int, string>(Convert.ToInt32(dr[DbTableColumn.CourseID]), Convert.ToString(dr[DbTableColumn.CourseName])));
                }
                return trainingList;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "GetTrainingNameList", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTrainingName.CloseConncetion();
            }
        }

        public int SaveFeedbackDetails(FeedbackModel objFeedbackModel)
        {

            int insertStatus = 0;
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;

            try
            {
                objConnection = new SqlConnection(DBConstants.GetDBConnectionString());
                objConnection.Open();


                SqlParameter[] sqlParam = new SqlParameter[11];


                objCommand = new SqlCommand(SPNames.SaveFeedbackForm, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                Master m = new Master();
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.CourseID, objFeedbackModel.CourseID);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.FeedbackID, ParameterDirection.Output);
                //sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.EmpID, m.GetEmployeeIDByEmailID());
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.EmpID, objFeedbackModel.EmpID);
                if (objFeedbackModel.CommentsFeedback != null)
                {
                    sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.CommentsFeedback, objFeedbackModel.CommentsFeedback);
                }
                else
                {
                    sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.CommentsFeedback, string.Empty);

                }
                
                sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.QuestionID, objFeedbackModel.QuestionID);
                sqlParam[5] = objCommand.Parameters.AddWithValue(SPParameter.Rating, objFeedbackModel.Rating);
                sqlParam[6] = objCommand.Parameters.AddWithValue(SPParameter.CreatedById, m.GetEmployeeIDByEmailID());
                sqlParam[7] = objCommand.Parameters.AddWithValue(SPParameter.CreatedByDate, DateTime.Now);
                sqlParam[8] = objCommand.Parameters.AddWithValue(SPParameter.LastModifiedById, m.GetEmployeeIDByEmailID());
                sqlParam[9] = objCommand.Parameters.AddWithValue(SPParameter.LastModifiedDate, DateTime.Now);
                sqlParam[10] = objCommand.Parameters.AddWithValue(SPParameter.Status, 1);


                insertStatus = objCommand.ExecuteNonQuery();
                //if (objFeedbackModel.FeedbackID == 0)
                //{
                //    int FeedbackID = Convert.ToInt32(objCommand.Parameters[SPParameter.FeedbackID].Value.ToString());
                //}
                return insertStatus;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "SaveFeedbackDetails", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        public string GetEmpNameByEmpID(string empID)
        {
            DataAccessClass objGetEmpName = new DataAccessClass();
            string empName = string.Empty;
            try
            {
                objGetEmpName.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[0].Value = empID;

                ds = new DataSet();
                ds = objGetEmpName.GetDataSet(SPNames.GetEmployeeNameByEmpId, sqlParam);

                empName = ds.Tables[0].Rows[0]["Name"].ToString();

            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetEmpNameByEmpID", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetEmpName.CloseConncetion();
            }
            return empName;
        }

        public string GetTrainerNameByCourseID(string CourseID)
        {
            DataAccessClass objGetEmpName = new DataAccessClass();
            string TrainerName = string.Empty;
            try
            {
                objGetEmpName.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value = CourseID;

                ds = new DataSet();
                ds = objGetEmpName.GetDataSet(SPNames.GetTrainerNameByCourseID, sqlParam);

                TrainerName = ds.Tables[0].Rows[0]["TrainerName"].ToString();

            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetTrainerNameByCourseID", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetEmpName.CloseConncetion();
            }
            return TrainerName;
        }




        public DataSet GetFeedbackRatings(string value,string EmpID)
        {
            DataAccessClass objGetFeedbackRatings = new DataAccessClass();
            string Description = string.Empty;
            try
            {
                objGetFeedbackRatings.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[2];

                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value = value;

                sqlParam[1] = new SqlParameter(SPParameter.EmpID, SqlDbType.Int);
                sqlParam[1].Value = EmpID;

                ds = new DataSet();
                ds = objGetFeedbackRatings.GetDataSet(SPNames.GetFeedbackRatings, sqlParam);

            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetFeedbackRatings", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetFeedbackRatings.CloseConncetion();
            }
            return ds;
        }
        public DataSet GetEmployeesFeedbackByCourseId(string CourseID)
        {
            DataAccessClass objGetFeedbackRatings = new DataAccessClass();
            try
            {
                objGetFeedbackRatings.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value = CourseID;

                ds = new DataSet();
                ds = objGetFeedbackRatings.GetDataSet(SPNames.GetEmployeesFeedbackByCourseId, sqlParam);

            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetEmployeesFeedbackByCourseId", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetFeedbackRatings.CloseConncetion();
            }
            return ds;
        }

        public int getParticipantsCount(string CourseID)
        {
            DataAccessClass objgetParticipantsCount = new DataAccessClass();
            int count = 0;
            try
            {
                objgetParticipantsCount.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlparam = new SqlParameter[1];
                sqlparam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlparam[0].Value = Convert.ToInt32(CourseID);

                count = Convert.ToInt32(objgetParticipantsCount.ExecuteScalarSP(SPNames.getParticipantsCount, sqlparam));


            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "getParticipantsCount", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objgetParticipantsCount.CloseConncetion();
            }
            return count;
        }

        public int getFeedbackFilledCount(string CourseID)
        {
            DataAccessClass objgetFeedbackFilledCount = new DataAccessClass();
            int count = 0;
            try
            {
                objgetFeedbackFilledCount.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlparam = new SqlParameter[1];
                sqlparam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlparam[0].Value = Convert.ToInt32(CourseID);

                count = Convert.ToInt32(objgetFeedbackFilledCount.ExecuteScalarSP(SPNames.getFeedbackFilledCount, sqlparam));
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "getFeedbackFilledCount", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objgetFeedbackFilledCount.CloseConncetion();
            }
            return count;
        }

        public int SaveFeedbackDetailsByRMO(NominationModel objNominationModel,string CourseID)
        {
            int insertStatus = 0;
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;

            try
            {
                objConnection = new SqlConnection(DBConstants.GetDBConnectionString());
                objConnection.Open();

                SqlParameter[] sqlParam = new SqlParameter[6];

                objCommand = new SqlCommand(SPNames.SaveFeedbackDetailsByRMO, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                Master m = new Master();
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.CourseID, CourseID);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.FeedbackToBeSentTrainer, objNominationModel.FeedbackToBeSentTrainer);

                if (objNominationModel.FeedbackSentToTrainer.ToString() != "")
                {
                    sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.FeedbackSentToTrainer, objNominationModel.FeedbackSentToTrainer);
                }
                else
                {
                    sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.FeedbackSentToTrainer, DBNull.Value);
                }

                if (objNominationModel.ReasonSLANotMet != null)
                {
                    sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.ReasonSLANotMet, objNominationModel.ReasonSLANotMet);
                }
                else
                {
                    sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.ReasonSLANotMet, DBNull.Value);
                }

                if (objNominationModel.CommentsForFeedback !=null)
                {
                    sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.CommentsForFeedback, objNominationModel.CommentsForFeedback);
                }

                else
                {
                    sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.CommentsForFeedback, DBNull.Value);
                }

                sqlParam[5] = objCommand.Parameters.AddWithValue(SPParameter.LastModifiedById, m.GetEmployeeIDByEmailID());

                insertStatus = objCommand.ExecuteNonQuery();
                //if (objFeedbackModel.FeedbackID == 0)
                //{
                //    int FeedbackID = Convert.ToInt32(objCommand.Parameters[SPParameter.FeedbackID].Value.ToString());
                //}
                return insertStatus;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "SaveFeedbackDetails", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        public DataSet GetFeedbackDetailsForRMO(string CourseID)
        {
            DataAccessClass objGetFeedbackDetailsForRMO = new DataAccessClass();
            try
            {
                objGetFeedbackDetailsForRMO.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value = CourseID;

                ds = new DataSet();
                ds = objGetFeedbackDetailsForRMO.GetDataSet(SPNames.GetFeedbackDetailsForRMO, sqlParam);

            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "objGetFeedbackDetailsForRMO", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetFeedbackDetailsForRMO.CloseConncetion();
            }
            return ds;
        }
        public double GetAverageRatingsFromParticipants(string QuestionID, string CourseID)
        {
            DataAccessClass objGetFeedbackDetailsForRMO = new DataAccessClass();
            double TotatlRating = 0;
            try
            {
                objGetFeedbackDetailsForRMO.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[2];

                sqlParam[0] = new SqlParameter(SPParameter.QuestionID, SqlDbType.Int);
                sqlParam[0].Value = QuestionID;
                sqlParam[1] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[1].Value = CourseID;

                ds = new DataSet();
                ds = objGetFeedbackDetailsForRMO.GetDataSet(SPNames.GetAverageRatingsFromParticipants, sqlParam);
                if (ds.Tables[0].Rows[0]["TotalRating"].ToString() != "")
                {
                    TotatlRating = Convert.ToDouble(ds.Tables[0].Rows[0]["TotalRating"]);
                }
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "objGetFeedbackDetailsForRMO", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetFeedbackDetailsForRMO.CloseConncetion();
            }
            return TotatlRating;
        }

        public DataSet GetTrainingDetailsByCourseId(string CourseID)
        {
            DataAccessClass objGetTrainingDetailsByCourseId = new DataAccessClass();
            try
            {
                objGetTrainingDetailsByCourseId.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value = CourseID;

                ds = new DataSet();
                ds = objGetTrainingDetailsByCourseId.GetDataSet(SPNames.GetTrainingDetailsByCourseId, sqlParam);
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "objGetFeedbackDetailsForRMO", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTrainingDetailsByCourseId.CloseConncetion();
            }
            return ds;
        }
        public int UpdateFlagToFeedbackFilled(string empId, string CourseID)
        {
            int updateStatus = 0;
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;

            try
            {
                objConnection = new SqlConnection(DBConstants.GetDBConnectionString());
                objConnection.Open();


                SqlParameter[] sqlParam = new SqlParameter[2];


                objCommand = new SqlCommand(SPNames.UpdateFlagToFeedbackFilled, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                Master m = new Master();
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.CourseID, CourseID);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.EmpID, empId);


                updateStatus = objCommand.ExecuteNonQuery();
                //if (objFeedbackModel.FeedbackID == 0)
                //{
                //    int FeedbackID = Convert.ToInt32(objCommand.Parameters[SPParameter.FeedbackID].Value.ToString());
                //}
                return updateStatus;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "SaveFeedbackDetails", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }


        }

        public int SaveAndUpdateStatusOfTraining(NominationModel objNominationModel, string CourseID)
        {
            int updateStatus = 0;
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;

            try
            {
                objConnection = new SqlConnection(DBConstants.GetDBConnectionString());
                objConnection.Open();

                //Harsha Issue Id- 58436- Start
                //Description: on training record: if no emp fill feedback the show comment in SLA Met  columns
                SqlParameter[] sqlParam = new SqlParameter[8];

                objCommand = new SqlCommand(SPNames.SaveAndUpdateStatusOfTraining, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                Master m = new Master();
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.CourseID, CourseID);

                if (objNominationModel.FeedbackToBeSentTrainer != null)
                {
                    sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.FeedbackToBeSentTrainer, objNominationModel.FeedbackToBeSentTrainer);
                }
                else
                {
                    sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.FeedbackToBeSentTrainer, " ");
                }
                //Harsha Issue Id- 58436- End

                if (objNominationModel.FeedbackSentToTrainer.ToString() != "")
                {
                    sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.FeedbackSentToTrainer, objNominationModel.FeedbackSentToTrainer);
                }
                else
                {
                    sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.FeedbackSentToTrainer, objNominationModel.FeedbackSentToTrainer);
                }

                if (objNominationModel.ReasonSLANotMet != null)
                {
                    sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.ReasonSLANotMet, objNominationModel.ReasonSLANotMet);
                }
                else
                {
                    sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.ReasonSLANotMet, DBNull.Value);
                }

                if (objNominationModel.CommentsForFeedback != null)
                {
                    sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.CommentsForFeedback, objNominationModel.CommentsForFeedback);
                }

                else
                {
                    sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.CommentsForFeedback, DBNull.Value);
                }

                
                
                sqlParam[5] = objCommand.Parameters.AddWithValue(SPParameter.SendFeedback, objNominationModel.SendFeedback);
                sqlParam[6] = objCommand.Parameters.AddWithValue(SPParameter.LastModifiedById, m.GetEmployeeIDByEmailID());
                sqlParam[7] = objCommand.Parameters.AddWithValue(SPParameter.NoFeedbackReceived, objNominationModel.NoFeedbackReceived);

                updateStatus = objCommand.ExecuteNonQuery();
                //if (objFeedbackModel.FeedbackID == 0)
                //{
                //    int FeedbackID = Convert.ToInt32(objCommand.Parameters[SPParameter.FeedbackID].Value.ToString());
                //}
                return updateStatus;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "SaveFeedbackDetails", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        public DataSet GetTrainingVendorDetails()
        {
            DataAccessClass objGetTrainingDetailsByCourseId = new DataAccessClass();
            try
            {
                objGetTrainingDetailsByCourseId.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];

                ds = new DataSet();
                ds = objGetTrainingDetailsByCourseId.GetDataSet(SPNames.GetTrainingVendorDetails);
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "objGetFeedbackDetailsForRMO", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTrainingDetailsByCourseId.CloseConncetion();
            }
            return ds;
        }

        public DataSet GetVendorDetailsByVendorId(int VendorId)
        {
            DataAccessClass objGetTrainingDetailsByCourseId = new DataAccessClass();
            try
            {
                objGetTrainingDetailsByCourseId.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.VendorID, SqlDbType.Int);
                sqlParam[0].Value = VendorId;

                ds = new DataSet();
                ds = objGetTrainingDetailsByCourseId.GetDataSet(SPNames.GetVendorDetailsByVendorId, sqlParam);
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "objGetFeedbackDetailsForRMO", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTrainingDetailsByCourseId.CloseConncetion();
            }
            return ds;
        }

        public int SaveVendorDetails(VendorModel objVendorModel)
        {

            int insertStatus = 0;
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;

            try
            {
                objConnection = new SqlConnection(DBConstants.GetDBConnectionString());
                objConnection.Open();

                SqlParameter[] sqlParam = new SqlParameter[5];

                objCommand = new SqlCommand(SPNames.SaveVendorDetails, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                
                //sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.VendorID, objVendorModel.VendorId);
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.VendorName, objVendorModel.VendorName);
                if (objVendorModel.VendorEmailId != null)
                {
                    sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.VendorEmailId, objVendorModel.VendorEmailId);
                }
                else
                {
                    sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.VendorEmailId, DBNull.Value);
                }
                if (objVendorModel.ContactPersonName != null)
                {
                    sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.ContactPersonName, objVendorModel.ContactPersonName);
                }
                else
                {
                    sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.ContactPersonName, DBNull.Value);
                }
                if (objVendorModel.ContactPersonNumber != null)
                {
                    sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.ContactPersonNumber, objVendorModel.ContactPersonNumber);
                }
                else
                {
                    sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.ContactPersonNumber, DBNull.Value);
                }
                if (objVendorModel.Expertise != null)
                {
                    sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.Expertise, objVendorModel.Expertise);
                }
                else
                {
                    sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.Expertise, DBNull.Value);
                }
                
                insertStatus = objCommand.ExecuteNonQuery();

                //if (objFeedbackModel.FeedbackID == 0)
                //{
                //    int FeedbackID = Convert.ToInt32(objCommand.Parameters[SPParameter.FeedbackID].Value.ToString());
                //}
                return insertStatus;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "SaveFeedbackDetails", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }
        
        public int UpdateVendorDetails(VendorModel objVendorModel)
        {
            int updateStatus = 0;
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;

            try
            {
                objConnection = new SqlConnection(DBConstants.GetDBConnectionString());
                objConnection.Open();

                SqlParameter[] sqlParam = new SqlParameter[5];

                objCommand = new SqlCommand(SPNames.UpdateVendorDetails, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;

                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.VendorID, objVendorModel.VendorId);
                //sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.VendorName, objVendorModel.VendorName);
                if (objVendorModel.VendorEmailId != null)
                {
                    sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.VendorEmailId, objVendorModel.VendorEmailId);
                }
                else
                {
                    sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.VendorEmailId, DBNull.Value);
                }
                if (objVendorModel.ContactPersonName != null)
                {
                    sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.ContactPersonName, objVendorModel.ContactPersonName);
                }
                else
                {
                    sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.ContactPersonName, DBNull.Value);
                }
                if (objVendorModel.ContactPersonNumber != null)
                {
                    sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.ContactPersonNumber, objVendorModel.ContactPersonNumber);
                }
                else
                {
                    sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.ContactPersonNumber, DBNull.Value);
                }
                if (objVendorModel.Expertise != null)
                {
                    sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.Expertise, objVendorModel.Expertise);
                }
                else
                {
                    sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.Expertise, DBNull.Value);
                }

                updateStatus = objCommand.ExecuteNonQuery();

                //if (objFeedbackModel.FeedbackID == 0)
                //{
                //    int FeedbackID = Convert.ToInt32(objCommand.Parameters[SPParameter.FeedbackID].Value.ToString());
                //}
                return updateStatus;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "SaveFeedbackDetails", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        public int DeleteVendorDetails(int VendorId)
        {
            int deleteStatus = 0;
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;

            try
            {
                objConnection = new SqlConnection(DBConstants.GetDBConnectionString());
                objConnection.Open();

                SqlParameter[] sqlParam = new SqlParameter[1];

                objCommand = new SqlCommand(SPNames.DeleteVendorDetails, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;

                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.VendorID, VendorId);

                deleteStatus = objCommand.ExecuteNonQuery();
                return deleteStatus;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "SaveFeedbackDetails", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }

        }

        public string GetDuplicateStatusOfEmialId(string VendorEmailId)
        {
            DataAccessClass objGetDuplicateStatusOfEmialId = new DataAccessClass();
            string duplicateStatus = string.Empty;
            try
            {
                objGetDuplicateStatusOfEmialId.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.VendorEmailId, SqlDbType.Text);
                sqlParam[0].Value = VendorEmailId;

                ds = new DataSet();
                ds = objGetDuplicateStatusOfEmialId.GetDataSet(SPNames.GetDuplicateStatusOfEmialId, sqlParam);
                duplicateStatus = ds.Tables[0].Rows[0]["duplicateStatus"].ToString();
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "objGetFeedbackDetailsForRMO", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetDuplicateStatusOfEmialId.CloseConncetion();
            }
            return duplicateStatus ;
        }

        #endregion

        #region Attendance
        public SelectList GetConfirmedTraining(int TrainingTypeId)
        {
            DataAccessClass objGetConfirmedTraining = new DataAccessClass();
            List<TrainingCourseModel> objTrainingCourseModel = new List<TrainingCourseModel>();
            SqlParameter[] sqlParam = new SqlParameter[1];
            try
            {
                //objGetConfirmedTraining.OpenConnection(DBConstants.GetDBConnectionString());
 
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TNI_GetConfirmTraining, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue(SPParameter.TrainingType, TrainingTypeId);
                objReader = objCommand.ExecuteReader();

                SelectListItem selListItem;
                List<SelectListItem> newList = new List<SelectListItem>();
                newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "" });
                while (objReader.Read())
                {
                    selListItem = new SelectListItem() { Value = objReader[0].ToString().Trim(), Text = objReader[1].ToString().Trim() };

                    newList.Add(selListItem);
                }
                return new SelectList(newList, "Value", "Text");
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetConfirmedTraining", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        } 
            
        public SelectList GetSeminarKSSList(int TrainingTypeId)
        {
            DataAccessClass objGetConfirmedTraining = new DataAccessClass();
            List<TrainingCourseModel> objTrainingCourseModel = new List<TrainingCourseModel>();
            SqlParameter[] sqlParam = new SqlParameter[1];
            try
            {
                //objGetConfirmedTraining.OpenConnection(DBConstants.GetDBConnectionString());

                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TNI_GetSeminarKSSList, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue(SPParameter.TrainingType, TrainingTypeId);
                objReader = objCommand.ExecuteReader();

                SelectListItem selListItem;
                List<SelectListItem> newList = new List<SelectListItem>();
                newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "" });
                while (objReader.Read())
                {
                    selListItem = new SelectListItem() { Value = objReader[0].ToString().Trim(), Text = objReader[2].ToString().Trim() };

                    newList.Add(selListItem);
                }
                return new SelectList(newList, "Value", "Text");
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetSeminarKSSList", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        public DataTable GetEmpPresentee(int CourseId, string AttDate, string Flag)
        {
            List<DynamicGrid> objDynamicGridList;
            DataAccessClass objDBCon = new DataAccessClass();
            objDynamicGridList = new List<DynamicGrid>();
            //DynamicGrid objDynamicGrid = null;
            DataSet ds = null;
            SqlParameter[] sqlParam = new SqlParameter[3];
            try
            {
                objDBCon.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value = CourseId ;
                sqlParam[1] = new SqlParameter(SPParameter.AttendanceDate, SqlDbType.VarChar);
                sqlParam[1].Value = AttDate;
                sqlParam[2] = new SqlParameter(SPParameter.ViewMode, SqlDbType.VarChar);
                sqlParam[2].Value = Flag;

                 ds = objDBCon.GetDataSet(SPNames.TNI_GetAttendance, sqlParam);

                //foreach (DataRow dr in ds.Tables[0].Rows)
                //{
                //    objDynamicGrid = new DynamicGrid();
                //    objDynamicGrid.CourseId = Convert.ToInt32(dr[DbTableColumn.CourseID]);
                //    objDynamicGrid.Empid = Convert.ToInt32(dr[DbTableColumn.EMPId]);
                //    objDynamicGrid.EmpName = Convert.ToString(dr[DbTableColumn.EmployeeName]);
                //    objDynamicGrid.AttendanceDate = dr[DbTableColumn.AttendanceDate].ToString();
                //    objDynamicGrid.Presentee = dr[DbTableColumn.Presentee].ToString();

                //    objDynamicGridList.Add(objDynamicGrid);
                //}
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "GetEmpPresentee", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDBCon.CloseConncetion();

            }
            return ds.Tables[0];
            //return objDynamicGridList;
        }

        public DataSet GetEmpPresenteeAll(int CourseId, int TrainingTypeID)
        {
            List<DynamicGrid> objDynamicGridList;
            DataAccessClass objDBCon = new DataAccessClass();
            objDynamicGridList = new List<DynamicGrid>();
            //DynamicGrid objDynamicGrid = null;
            DataSet ds = null;
            SqlParameter[] sqlParam = new SqlParameter[2];
            try
            {
                objDBCon.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value = CourseId;
                sqlParam[1] = new SqlParameter(SPParameter.TrainingTypeID, SqlDbType.Int);
                sqlParam[1].Value = TrainingTypeID;
                ds = objDBCon.GetDataSet(SPNames.TNI_GetAttendanceAll, sqlParam);

                if (ds.Tables.Count > 1)
                {
                    for (int i = 0; i <= ds.Tables[1].Rows.Count - 1; i++)
                    {
                        ds.Tables[1].Rows[i][0] = ds.Tables[1].Rows[i]["FirstName"] + " " + ds.Tables[1].Rows[i]["LastName"];
                    }
                    ds.Tables[1].Columns["FirstName"].ColumnName = "Employee Name";
                    ds.Tables[1].Columns["Dropout"].ColumnName = "Dropout";
                    ds.Tables[1].Columns["Feedbacksent"].ColumnName = "Send Feedback Form";
                    ds.Tables[1].Columns["Percents"].ColumnName = "Percentage";
                    ds.Tables[1].AcceptChanges();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "GetEmpPresenteeAll", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDBCon.CloseConncetion();

            }

              //for (int i =0, i++;i<ds.Tables[0].Rows.Count)
                  
            return ds;
            //return objDynamicGridList;
        }

        public int SaveAttendance(AttendanceModel obj)
        {
            int output = 0;
            DataAccessClass objDataAccessClass = new DataAccessClass();
            raveHRCollection = new List<TrainingModel>();
            SqlParameter[] sqlParam = new SqlParameter[4];
            try
            {
                objDataAccessClass.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value = obj.CourseId;
                sqlParam[1] = new SqlParameter(SPParameter.AttendanceDate, SqlDbType.DateTime);
                sqlParam[1].Value = obj.AttendanceDate ;
                sqlParam[2] = new SqlParameter(SPParameter.EmployeesId, SqlDbType.VarChar);
                sqlParam[2].Value = obj.EmpIdAll;
                sqlParam[3] = new SqlParameter(SPParameter.CreatedBy , SqlDbType.Int);
                sqlParam[3].Value = obj.CreatedBy;
                

                DataSet ds = objDataAccessClass.GetDataSet(SPNames.TNI_InsertUpdateGetAttendance, sqlParam);

                if (ds.Tables[0].Rows.Count > 0)
                { output = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                }
                else
                {
                    output = 0;
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
                objDataAccessClass.CloseConncetion();

            }
            // return Collection
            return output;
        }
        public int SaveAttendanceSemiKss(AttendanceModel obj)
        {
            int output = 0;
            DataAccessClass objDataAccessClass = new DataAccessClass();
            raveHRCollection = new List<TrainingModel>();
            SqlParameter[] sqlParam = new SqlParameter[5];
            try
            {
                objDataAccessClass.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                sqlParam[0].Value = obj.CourseId;                
                sqlParam[1] = new SqlParameter(SPParameter.EmployeesId, SqlDbType.VarChar);
                sqlParam[1].Value = obj.EmpIdAll;
                sqlParam[2] = new SqlParameter(SPParameter.TrainingTypeID, SqlDbType.Int);
                sqlParam[2].Value = obj.TrainingTypeID;
                sqlParam[3] = new SqlParameter(SPParameter.CreatedBy, SqlDbType.Int);
                sqlParam[3].Value = obj.CreatedBy;
                sqlParam[4] = new SqlParameter(SPParameter.AttendanceDate, SqlDbType.DateTime);
                sqlParam[4].Value = Convert.ToDateTime(obj.AttendanceDate);

                DataSet ds = new DataSet();
                if (obj.TrainingTypeID ==1209)
                     ds = objDataAccessClass.GetDataSet(SPNames.TNI_InsertUpdateGetAttendanceSeminar, sqlParam);
                else if (obj.TrainingTypeID ==1210)
                     ds = objDataAccessClass.GetDataSet(SPNames.TNI_InsertUpdateGetAttendanceKss, sqlParam);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    output = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                }
                else
                {
                    output = 0;
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
                objDataAccessClass.CloseConncetion();

            }
            // return Collection
            return output;
        }

        public DataSet  SaveFeedbackSent(AttendanceModel obj)
        {
            DataSet ds = null;
            DataAccessClass objDataAccessClass = new DataAccessClass();
            raveHRCollection = new List<TrainingModel>();
            SqlParameter[] sqlParam = new SqlParameter[7];
            try
            {
                objDataAccessClass.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value = obj.CourseId;
                sqlParam[1] = new SqlParameter(SPParameter.AttendanceDate, SqlDbType.DateTime);
                sqlParam[1].Value = obj.FeedbackLastDate;
                sqlParam[2] = new SqlParameter(SPParameter.FbkSaveUpdateMode, SqlDbType.VarChar);
                sqlParam[2].Value = obj.FbkSaveUpdateMode;
                sqlParam[3] = new SqlParameter(SPParameter.EmployeesId, SqlDbType.VarChar);
                if (obj.AttendanceType.ToString() == "Feedback")
                {
                    sqlParam[3].Value = obj.EmpIdAll;
                }
                else
                {
                    sqlParam[3].Value = obj.dropEmpIdAll;
                }
                //sqlParam[3].Value = obj.EmpIdAll;
                sqlParam[4] = new SqlParameter(SPParameter.CreatedBy, SqlDbType.Int);
                sqlParam[4].Value = obj.CreatedBy;
                sqlParam[5] = new SqlParameter(SPParameter.Type, SqlDbType.VarChar);
                sqlParam[5].Value = obj.AttendanceType;
                sqlParam[6] = new SqlParameter(SPParameter.FeedbackMailNotToSend, SqlDbType.VarChar);
                sqlParam[6].Value = obj.FeedbackMailNotToSend;

                ds = objDataAccessClass.GetDataSet(SPNames.TNI_InsertUpdateFeedbackSent, sqlParam);
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
                objDataAccessClass.CloseConncetion();

            }
            // return Collection
            return ds;
        }

        public int SetAttendance(AttendanceModel obj)
        {
            int output = 0;
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;

            try
            {
                objConnection = new SqlConnection(DBConstants.GetDBConnectionString());
                objConnection.Open();

                SqlParameter[] sqlParam = new SqlParameter[4];

                objCommand = new SqlCommand(SPNames.TNI_InsertAttendanceDate, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;

                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.CourseID, obj.CourseId);
                sqlParam[1] = objCommand.Parameters.Add(SPParameter.AttendanceDate, obj.SetAttendanceDates);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.TrainingTypeID, obj.TrainingTypeID);
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.CreatedBy, obj.CreatedBy);

                output = objCommand.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "FunctionApproveRejectViewTechnicalTraining", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }

            return output;
        }

        public List<SelectListItem> GetAttendanceDates(int CourseID,int TrainingTypeId)
        {
            List<SelectListItem> newList = new List<SelectListItem>();
            DataAccessClass daTraining = new DataAccessClass();

            SqlParameter[] sqlParam = new SqlParameter[2];
            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TNI_GetAttendanceDate, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue(SPParameter.CourseID, CourseID);
                objCommand.Parameters.AddWithValue(SPParameter.TrainingTypeID, TrainingTypeId);
                objReader = objCommand.ExecuteReader();

                SelectListItem selListItem;
                newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "" });
                while (objReader.Read())
                {
                    selListItem = new SelectListItem() { Value = objReader[0].ToString().Trim(), Text = objReader[0].ToString().Trim() };

                    newList.Add(selListItem);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetAttendanceDates", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                daTraining.CloseConncetion();
            }
            return newList;
        }

        public DataTable GetEmpPresenteeSeminar(int Raiseid,string AttendanceDate,  string Flag)
        {
            List<DynamicGrid> objDynamicGridList;
            DataAccessClass objDBCon = new DataAccessClass();
            objDynamicGridList = new List<DynamicGrid>();
            DataSet ds = null;
            SqlParameter[] sqlParam = new SqlParameter[3];
            try
            {
                objDBCon.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                sqlParam[0].Value = Raiseid;
                sqlParam[1] = new SqlParameter(SPParameter.ViewMode, SqlDbType.VarChar);
                sqlParam[1].Value = Flag;
                sqlParam[2] = new SqlParameter(SPParameter.AttendanceDate,SqlDbType.DateTime);
                sqlParam[2].Value = Convert.ToDateTime(AttendanceDate);

                ds = objDBCon.GetDataSet(SPNames.TNI_GetAttendanceSeminar, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "GetEmpPresentee", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDBCon.CloseConncetion();

            }
            return ds.Tables[0];
        }

       public  DataTable GetAttendanceReport(int TrainingTypeId, int CourseId)
       {
             List<DynamicGrid> objDynamicGridList;
            DataAccessClass objDBCon = new DataAccessClass();
            objDynamicGridList = new List<DynamicGrid>();
            DataSet ds = null;
            SqlParameter[] sqlParam = new SqlParameter[2];
            try
            {
                objDBCon.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.TrainingTypeID, SqlDbType.Int);
                sqlParam[0].Value = TrainingTypeId;
                sqlParam[1] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[1].Value = CourseId;

                ds = objDBCon.GetDataSet(SPNames.TNI_ReportAttendance, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "GetAttendanceReport", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDBCon.CloseConncetion();

            }
            return ds.Tables[0];
        }

       public List<Employee> GetNominationDetailsByCourseId(int CourseID, int TrainingTypeID)
       {
           List<Employee> newList = new List<Employee>();
           DataAccessClass daTraining = new DataAccessClass();

           SqlParameter[] sqlParam = new SqlParameter[2];
           try
           {
               string ConnStr = DBConstants.GetDBConnectionString();
               objConnection = new SqlConnection(ConnStr);
               objConnection.Open();
               
               objCommand = new SqlCommand(SPNames.TNI_GetNominationDetailsByCourseId, objConnection);
               objCommand.CommandType = CommandType.StoredProcedure;
               sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.CourseID, CourseID);
               sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.TrainingTypeID, TrainingTypeID);

               objReader = objCommand.ExecuteReader();

               Employee selListItem;
               while (objReader.Read())
               {
                   selListItem = new Employee() { FirstName = objReader[0].ToString().Trim(), LastName = objReader[1].ToString().Trim() };

                   newList.Add(selListItem);
               }
           }
           catch (Exception ex)
           {
               throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetNominationDetailsByCourseId", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
           }
           finally
           {
               daTraining.CloseConncetion();
           }
           return newList;
       }
        #endregion

        #region TrainingEffectiveness

        public List<TrainingEffectiveness> GetTrainingEffectiveness(int trainingnameid, int LoginEmpId, int LoginEmpIdFlag, string RoleName)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            TrainingEffectiveness addEmployee;
            List<TrainingEffectiveness> employeeList = new List<TrainingEffectiveness>();
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter(SPParameter.TrainingID, SqlDbType.Int);
                sqlParam[0].Value = trainingnameid;
                sqlParam[1] = new SqlParameter(SPParameter.LoginEmpId, SqlDbType.Int);
                sqlParam[1].Value = LoginEmpId;
                sqlParam[2] = new SqlParameter(SPParameter.LoginEmpFlag, SqlDbType.Int);
                sqlParam[2].Value = LoginEmpIdFlag;
                sqlParam[3] = new SqlParameter(SPParameter.RoleName, SqlDbType.NVarChar,100);
                sqlParam[3].Value = RoleName;

                SqlDataReader dr = objGetTraining.ExecuteReaderSP(SPNames.TNI_GetTrainingEffectiveness, sqlParam);

                while (dr.Read())
                {
                    addEmployee = new TrainingEffectiveness();
                    addEmployee.EmpId = Convert.ToInt16(dr[DbTableColumn.EMPId]);
                    addEmployee.EmployeeName = Convert.ToString(dr[DbTableColumn.EmployeeName]);
                    addEmployee.PreTrainingFlag = Convert.ToInt32(dr[DbTableColumn.PreTrainingFlag]);
                    addEmployee.PostTrainingFlag = Convert.ToInt32(dr[DbTableColumn.PostTrainingFlag]);
                    addEmployee.AssessmentFlag = Convert.ToInt32(dr[DbTableColumn.AssessmentFlag]);
                    addEmployee.PreTrainingRatingText = Convert.ToString(dr[DbTableColumn.PreTrainingRatingText]);
                    addEmployee.PreTrainingRating = Convert.ToInt32(dr[DbTableColumn.PreTrainingRating]);
                    addEmployee.PostTrainingRating = Convert.ToInt32(dr[DbTableColumn.PostTrainingRating]);
                    addEmployee.Assessment = Convert.ToInt32(dr[DbTableColumn.Assessment]);
                    addEmployee.PreNominatorName = Convert.ToString(dr[DbTableColumn.PreNominatorName]);
                    //Neelam Issue Id:60562 start
                    addEmployee.NominatorEmpID = Convert.ToInt32(dr[DbTableColumn.NominatorEmpID]);
                    //Neelam Issue Id:60562 end
                    addEmployee.PostNominatorNameID = (Convert.ToInt32(dr[DbTableColumn.PostNominatorNameID]) == CommonConstants.DefaultFlagZero) ? addEmployee.NominatorEmpID : Convert.ToInt32(dr[DbTableColumn.PostNominatorNameID]);
                    addEmployee.PostNominatorName = String.IsNullOrEmpty(dr[DbTableColumn.PostNominatorName].ToString()) ? Convert.ToString(dr[DbTableColumn.LoginEmpName]) : Convert.ToString(dr[DbTableColumn.PostNominatorName]);
                    addEmployee.CourseID = Convert.ToInt32(dr[DbTableColumn.CourseID]);
                    addEmployee.Comments = Convert.ToString(dr[DbTableColumn.Comments]);
                    addEmployee.ObjectiveForNomination = Convert.ToString(dr[DbTableColumn.ObjectiveForNomination]);
                    addEmployee.IsObjectiveMet = Convert.ToInt32(dr[DbTableColumn.IsObjectiveMet]);
                    addEmployee.TrainingTypeID = Convert.ToInt32(dr[DbTableColumn.TrainingTypeID]);
                    addEmployee.TrainingEffectivenessFlag = Convert.ToInt32(dr[DbTableColumn.TrainingEffectivenessFlag]);
                    addEmployee.IsReportingManagerUpdated = Convert.ToBoolean(dr[DbTableColumn.IsReportingManagerUpdated]);
                   
                    if (!String.IsNullOrWhiteSpace(Convert.ToString(dr[DbTableColumn.PostRatingDueDate])))
                    {
                        addEmployee.PostRatingDueDate = Convert.ToDateTime(dr[DbTableColumn.PostRatingDueDate]);
                    }
                    employeeList.Add(addEmployee);
                }
                return employeeList;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "GetTrainingEffectiveness", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }

        }

        public int UpdateTrainingEffectiveness(NominationModel objNominationmodel)
        {
            string ConnStr = string.Empty;
            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                for (int i = 0; i < objNominationmodel.employeeListModel.Count; i++)
                {
                    // Changed by : Venkatesh  : Start
                    string  ManagerRole = "";
                    string AdminRole = "";
                    if (System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
                    {
                        ArrayList arrRolesForUser = new ArrayList();
                        arrRolesForUser = (ArrayList)HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES];
                        if (arrRolesForUser.Contains("manager"))
                            ManagerRole = "Manager";
                        if (arrRolesForUser.Contains("admin"))
                            AdminRole = "Admin";
                    }
                    // Changed by : Venkatesh  : End

                    if (objNominationmodel.RoleName.ToLower() == ManagerRole.ToLower())  
                    {
                        objNominationmodel.employeeListModel[i].IsAdminGroup = true;
                    }
                    if (objNominationmodel.employeeListModel[i].IsAdminGroup == true)
                    {
                        objCommand = new SqlCommand(SPNames.TNI_USP_TNI_UpdateTrainingEffectivenessDetails, objConnection);
                        objCommand.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] sqlParam = new SqlParameter[11];
                        sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.CourseID, objNominationmodel.employeeListModel[i].CourseID);
                        sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.PreTrainingRating, objNominationmodel.employeeListModel[i].PreTrainingRating);
                        sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.Assessment, objNominationmodel.employeeListModel[i].Assessment);
                        sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.PostTrainingRating, objNominationmodel.employeeListModel[i].PostTrainingRating);
                        //sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.PostNominatorNameID, (objNominationmodel.RoleName.ToLower() == CommonConstants.AdminRole) ? objNominationmodel.employeeListModel[i].PostNominatorNameID : objNominationmodel.LoginEmpId);
                        sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.PostNominatorNameID, (objNominationmodel.RoleName.ToLower() == AdminRole.ToLower()) ? objNominationmodel.employeeListModel[i].PostNominatorNameID : objNominationmodel.LoginEmpId);
                        sqlParam[5] = objCommand.Parameters.AddWithValue(SPParameter.EmpID, objNominationmodel.employeeListModel[i].EmpId);
                        sqlParam[6] = objCommand.Parameters.AddWithValue(SPParameter.LoginEmpId, objNominationmodel.LoginEmpId);
                        sqlParam[7] = objCommand.Parameters.AddWithValue(SPParameter.ObjectiveForNomination, String.IsNullOrEmpty(objNominationmodel.employeeListModel[i].ObjectiveForNomination) ? string.Empty : objNominationmodel.employeeListModel[i].ObjectiveForNomination);
                        sqlParam[8] = objCommand.Parameters.AddWithValue(SPParameter.IsObjectiveMet, objNominationmodel.employeeListModel[i].IsObjectiveMet);
                        sqlParam[9] = objCommand.Parameters.AddWithValue(SPParameter.Comments, String.IsNullOrWhiteSpace(objNominationmodel.employeeListModel[i].Comments) ? string.Empty : objNominationmodel.employeeListModel[i].Comments);
                        sqlParam[10] = objCommand.Parameters.AddWithValue(SPParameter.TrainingTypeID, objNominationmodel.TrainingTypeID);

                        objCommand.ExecuteNonQuery();
                    }
                }

                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "UpdateTrainingEffectiveness", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
            }
        }
        //Neelam Issue Id:60562 start
        public int UpdateTrainingNominatorEmpID(int CourseID, int EmpID, int PostNominatorId)
        {
            string ConnStr = string.Empty;
            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TNI_USP_TNI_UpdateTrainingNominatorEmpID, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.CourseID, CourseID);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.EmpID, EmpID);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.PostNominatorID, PostNominatorId);
                objCommand.ExecuteNonQuery();
                   
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "UpdateTrainingEffectiveness", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
            }
        }
        //Neelam Issue Id:60562 end
        public string SendTrainingEffectiveness(NominationModel objNominationmodel)
        {
            //for (int i = 0; i < objNominationmodel.employeeListModel.Count; i++)
            //{
            string ConnStr = string.Empty;
            try
            {

                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TNI_USP_TNI_SendTrainingEffectivenessDetails, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[6];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.CourseID, objNominationmodel.employeeListModel[0].CourseID);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.PostRatingDueDate, objNominationmodel.PostRatingDueDate.ToString("MM/dd/yyyy"));
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.EmpID, objNominationmodel.employeeListModel[0].EmpId);
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.LoginEmpId, objNominationmodel.LoginEmpId);
                sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.TrainingTypeID, objNominationmodel.TrainingTypeID);
                sqlParam[5] = objCommand.Parameters.AddWithValue(SPParameter.InactiveManagerName, 0);
                sqlParam[5].Direction = ParameterDirection.Output;

                objCommand.ExecuteNonQuery();

                return Convert.ToString(sqlParam[5].Value);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "UpdateTrainingEffectiveness", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            //}
            //return 0;
        }

        public DataSet CheckInActiveManagerForTrainingEffectivness(int CourseID)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            SqlParameter[] sqlParam = new SqlParameter[1];
            raveHRCollection = new List<TrainingModel>();
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value = CourseID;
                return objGetTraining.GetDataSet(SPNames.TNI_USP_TNI_CheckInActiveManagerForTrainingEffectivness, sqlParam);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "CheckInActiveManagerForTrainingEffectivnesss", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }
        }
        
        public List<KeyValuePair<int, string>> GetTrainingNameforEffectiveness(int LoginEmpID, string RoleName, int TrainingTypeID)
        {

            DataAccessClass objGetTraining = new DataAccessClass();
            List<KeyValuePair<int, string>> trainingList = new List<KeyValuePair<int, string>>();
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter(SPParameter.LoginEmpId, SqlDbType.Int);
                sqlParam[0].Value = LoginEmpID;
                sqlParam[1] = new SqlParameter(SPParameter.RoleName, SqlDbType.NVarChar, 100);
                sqlParam[1].Value = RoleName;
                sqlParam[2] = new SqlParameter(SPParameter.TrainingTypeID, SqlDbType.Int);
                sqlParam[2].Value = TrainingTypeID;

                SqlDataReader dr = objGetTraining.ExecuteReaderSP(SPNames.TNI_USP_TNI_GetTrainingNameEffectiveness, sqlParam);
                while (dr.Read())
                {
                    trainingList.Add(new KeyValuePair<int, string>(Convert.ToInt32(dr[DbTableColumn.CourseID]), Convert.ToString(dr[DbTableColumn.CourseName])));
                }
                return trainingList;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "GetTrainingNameforEffectiveness", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }
        }

        public DataSet SendMailToManagerForPostTrainingRatining(int CourseID)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            SqlParameter[] sqlParam = new SqlParameter[1];
            raveHRCollection = new List<TrainingModel>();
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value = CourseID;
                return objGetTraining.GetDataSet(SPNames.TNI_USP_TNI_SentManagerForPostTrainingRating, sqlParam);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "SendMailToManagerForPostTrainingRatining", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }
        }

        public DataSet ManagerFilledPostTrainingRating(int CourseID, int LoginEmpID, string EmpID, string RoleName)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            SqlParameter[] sqlParam = new SqlParameter[4];
            raveHRCollection = new List<TrainingModel>();
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value = CourseID;
                sqlParam[1] = new SqlParameter(SPParameter.LoginEmpId, SqlDbType.Int);
                sqlParam[1].Value = LoginEmpID;
                sqlParam[2] = new SqlParameter(SPParameter.EmpId, SqlDbType.NVarChar);
                sqlParam[2].Value = EmpID;
                sqlParam[3] = new SqlParameter(SPParameter.RoleName, SqlDbType.NVarChar);
                sqlParam[3].Value = RoleName;
                return objGetTraining.GetDataSet(SPNames.TNI_USP_TNI_ManagerFilledPostTrainingRating, sqlParam);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "ManagerFilledPostTrainingRating", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }
        }

        #endregion TrainingEffectiveness

        #region Training Plan

        public int InsertUpdateTrainingPlan(TrainingPlanModel Obj)
        {
            string strCon = DBConstants.GetDBConnectionString();
            
            DataAccessClass dataaccess = new DataAccessClass();
                                    
            try
            {
                dataaccess.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] pa = new SqlParameter[11]; 
                pa[0] = new SqlParameter(SPParameter.TrainingPlanId, SqlDbType.Int);
                pa[0].Value = Obj.TrainingPlanId;
                pa[1] = new SqlParameter(SPParameter.Quarter, SqlDbType.Int);
                pa[1].Value = Obj.Quarter;
                pa[2] = new SqlParameter(SPParameter.TrainingId, SqlDbType.Int);
                pa[2].Value = Obj.TrainingId;
                pa[3] = new SqlParameter(SPParameter.TrainingHour, SqlDbType.VarChar);
                pa[3].Value = Obj.TrainingHour;
                pa[4] = new SqlParameter(SPParameter.TrainingTypeID, SqlDbType.Int);
                pa[4].Value = Obj.TrainingTypeId;
                pa[5] = new SqlParameter(SPParameter.Month, SqlDbType.Int);
                pa[5].Value = Obj.Month;
                pa[6] = new SqlParameter(SPParameter.Target, SqlDbType.VarChar);
                pa[6].Value = Obj.Target;
                pa[7] = new SqlParameter(SPParameter.Year, SqlDbType.Int);
                pa[7].Value = Obj.Year;
                pa[8] = new SqlParameter(SPParameter.Mode, SqlDbType.VarChar);
                pa[8].Value = Obj.Mode;
                pa[9] = new SqlParameter(SPParameter.CreatedById, SqlDbType.Int);
                pa[9].Value = Obj.CreatedById;
                pa[10] = new SqlParameter(SPParameter.TrainingNameOther, SqlDbType.VarChar);
                pa[10].Value = Obj.TrainingNameOther;
                                                               
               var output=  dataaccess.ExecuteScalarSP(SPNames.InsertUpdateTrainingPlan, pa);
               Obj.TrainingPlanId = 1;
            }
            catch (Exception ex)
            { 
                throw ex; 
            }
            finally 
            {
                dataaccess.CloseConncetion();
            }
            return Obj.TrainingPlanId;
        }


        public List<TrainingPlanModel> GetTrainingPlan(TrainingPlanModel Obj)
        {
            List<TrainingPlanModel> ListTrainingPlans = new List<TrainingPlanModel>();
            
            TrainingPlanModel objTrainingPlan = new TrainingPlanModel();
            DataAccessClass dataaccess = new DataAccessClass();
            try
            {
                dataaccess.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] pa = new SqlParameter[3];
                pa[0] = new SqlParameter(SPParameter.TrainingPlanId, SqlDbType.Int);
                pa[0].Value = Obj.TrainingPlanId;
                pa[1] = new SqlParameter(SPParameter.Year , SqlDbType.Int);
                pa[1].Value = Obj.Year;
                pa[2] = new SqlParameter(SPParameter.Quarter, SqlDbType.Int);
                pa[2].Value = Obj.Quarter;
                SqlDataReader dr = dataaccess.ExecuteReaderSP(SPNames.GetTrainingPlan, pa);
                while (dr.Read())
                {
                    TrainingPlanModel objData = new TrainingPlanModel();
                    objData.TrainingPlanId = Convert.ToInt32(dr[DbTableColumn.TrainingPlanId]);
                    objData.Quarter = Convert.ToInt32(dr[DbTableColumn.Quarter]);
                    objData.QuarterName = Convert.ToString(dr[DbTableColumn.QuarterName]);
                    objData.TrainingId = Convert.ToInt32(dr[DbTableColumn.TrainingId]);
                    objData.TrainingName = Convert.ToString(dr[DbTableColumn.TrainingName]);
                    objData.TrainingHour = Convert.ToString(dr[DbTableColumn.TrainingHour]);
                    objData.Month = Convert.ToInt32(dr[DbTableColumn.Month]);
                    objData.MonthName  = Convert.ToString(dr[DbTableColumn.MonthName]);
                    objData.Target = Convert.ToString(dr[DbTableColumn.Target]);
                    objData.TrainingTypeId = Convert.ToInt32(dr[DbTableColumn.TrainingTypeID]);
                    objData.TrainingTypeName = Convert.ToString(dr[DbTableColumn.TrainingTypeName]);
                    objData.Year = Convert.ToInt32(dr[DbTableColumn.Year]);
                    ListTrainingPlans.Add(objData);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "GetTrainingPlan", "GetTrainingPlan", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally 
            {
                dataaccess.CloseConncetion();
            }

            return ListTrainingPlans;
        }
                
        public DataTable GetTrainingPlanDataSet(TrainingPlanModel Obj)
        {
            DataSet ds = null;
            TrainingPlanModel objTrainingPlan = new TrainingPlanModel();
            DataAccessClass dataaccess = new DataAccessClass();
            try
            {
                dataaccess.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] pa = new SqlParameter[3];
                pa[0] = new SqlParameter(SPParameter.TrainingPlanId, SqlDbType.Int);
                pa[0].Value = Obj.TrainingPlanId;
                pa[1] = new SqlParameter(SPParameter.Year, SqlDbType.Int);
                pa[1].Value = Obj.Year;
                pa[2] = new SqlParameter(SPParameter.Quarter, SqlDbType.Int);
                pa[2].Value = Obj.Quarter;
                ds = dataaccess.GetDataSet(SPNames.GetTrainingPlan, pa);                
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "GetTrainingPlan", "GetTrainingPlan", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                dataaccess.CloseConncetion();
            }
            return ds.Tables [0];
        }

        /// <summary>
        /// Delete the Training Plan and Fetch all plan for that Quarter
        /// </summary>
        /// <param name="TrainingPlanid"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<TrainingPlanModel> DeleteTrainingPlan(int TrainingPlanid, int UserId)
        {
            List<TrainingPlanModel> ListTrainingPlans = new List<TrainingPlanModel>();

            TrainingPlanModel objTrainingPlan = new TrainingPlanModel();
            DataAccessClass dataaccess = new DataAccessClass();
            try
            {
                dataaccess.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] pa = new SqlParameter[2];
                pa[0] = new SqlParameter(SPParameter.TrainingPlanId, SqlDbType.Int);
                pa[0].Value = TrainingPlanid;
                pa[1] = new SqlParameter(SPParameter.CreatedBy , SqlDbType.Int);
                pa[1].Value = UserId;

                SqlDataReader dr = dataaccess.ExecuteReaderSP(SPNames.DeleteTrainingPlan, pa);
                while (dr.Read())
                {
                    TrainingPlanModel objData = new TrainingPlanModel();
                    objData.TrainingPlanId = Convert.ToInt32(dr[DbTableColumn.TrainingPlanId]);
                    objData.Quarter = Convert.ToInt32(dr[DbTableColumn.Quarter]);
                    objData.QuarterName = Convert.ToString(dr[DbTableColumn.QuarterName]);
                    objData.TrainingId = Convert.ToInt32(dr[DbTableColumn.TrainingId]);
                    objData.TrainingName = Convert.ToString(dr[DbTableColumn.TrainingName]);
                    objData.TrainingHour = Convert.ToString(dr[DbTableColumn.TrainingHour]);
                    objData.Month = Convert.ToInt32(dr[DbTableColumn.Month]);
                    objData.MonthName = Convert.ToString(dr[DbTableColumn.MonthName]);
                    objData.Target = Convert.ToString(dr[DbTableColumn.Target]);
                    objData.TrainingTypeId = Convert.ToInt32(dr[DbTableColumn.TrainingTypeID]);
                    objData.TrainingTypeName = Convert.ToString(dr[DbTableColumn.TrainingTypeName]);
                    objData.Year = Convert.ToInt32(dr[DbTableColumn.Year]);
                    ListTrainingPlans.Add(objData);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "GetTrainingPlan", "GetTrainingPlan", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                dataaccess.CloseConncetion();
            }

            return ListTrainingPlans;
        }

        /// <summary>
        /// Get Training plan for current Quarter and Next quarter is current month is last month in Quarter
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="NextQuarter"></param>
        /// <param name="Nextyear"></param>
        /// <returns></returns>
        public List<TrainingPlanModel> GetTrainingPlanEmployees(TrainingPlanModel Obj, int NextQuarter, int Nextyear)
        {
            List<TrainingPlanModel> ListTrainingPlans = new List<TrainingPlanModel>();

            TrainingPlanModel objTrainingPlan = new TrainingPlanModel();
            DataAccessClass dataaccess = new DataAccessClass();
            try
            {
                dataaccess.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] pa = new SqlParameter[4];
                pa[0] = new SqlParameter(SPParameter.Year, SqlDbType.Int);
                pa[0].Value = Obj.Year;
                pa[1] = new SqlParameter(SPParameter.Quarter, SqlDbType.Int);
                pa[1].Value = Obj.Quarter;
                pa[2] = new SqlParameter(SPParameter.NextYear, SqlDbType.Int);
                pa[2].Value = Nextyear ;
                pa[3] = new SqlParameter(SPParameter.NextQuarter, SqlDbType.Int);
                pa[3].Value = NextQuarter;
                SqlDataReader dr = dataaccess.ExecuteReaderSP(SPNames.GetTrainingPlanEmployees, pa);
                while (dr.Read())
                {
                    TrainingPlanModel objData = new TrainingPlanModel();
                    objData.TrainingPlanId = Convert.ToInt32(dr[DbTableColumn.TrainingPlanId]);
                    objData.Quarter = Convert.ToInt32(dr[DbTableColumn.Quarter]);
                    objData.QuarterName = Convert.ToString(dr[DbTableColumn.QuarterName]);
                    objData.TrainingId = Convert.ToInt32(dr[DbTableColumn.TrainingId]);
                    objData.TrainingName = Convert.ToString(dr[DbTableColumn.TrainingName]);
                    objData.TrainingHour = Convert.ToString(dr[DbTableColumn.TrainingHour]);
                    objData.Month = Convert.ToInt32(dr[DbTableColumn.Month]);
                    objData.MonthName = Convert.ToString(dr[DbTableColumn.MonthName]);
                    objData.Target = Convert.ToString(dr[DbTableColumn.Target]);
                    objData.TrainingTypeId = Convert.ToInt32(dr[DbTableColumn.TrainingTypeID]);
                    objData.TrainingTypeName = Convert.ToString(dr[DbTableColumn.TrainingTypeName]);
                    objData.Year = Convert.ToInt32(dr[DbTableColumn.Year]);
                    objData.YearFinancial = Convert.ToString(dr[DbTableColumn.YearFinancial]);
                    ListTrainingPlans.Add(objData);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "GetTrainingPlan", "GetTrainingPlan", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                dataaccess.CloseConncetion();
            }

            return ListTrainingPlans;
        }

        
        #endregion



        //Harsha- Issue Id- 58975 & 58958 - Start
        //Description- Making Training Cost Editable for Internal Training after Closed status of the training
        //Disabling Number of Hours and Days field once the after training's status is feedback sent

        bool ITrainingRepository.UpdateTrainingCourseTotalCost(int? courseId, float? totalCost)
        {
            int updateStatus = 0;
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;

            try
            {
                objConnection = new SqlConnection(DBConstants.GetDBConnectionString());
                objConnection.Open();

                SqlParameter[] sqlParam = new SqlParameter[2];

                objCommand = new SqlCommand(SPNames.UpdateInternalTrainingCost, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;

                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.CourseId, courseId);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.TotalCost, totalCost);
                updateStatus = objCommand.ExecuteNonQuery();
                return (updateStatus!=0) ? true: false;

            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "UpdateTrainingCourseTotalCost", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
                
        }

        //Harsha- Issue Id- 58975 & 58958 - End

        #region Self Learning

        public int SaveMyTraining(MyTrainingModel myTraining, int empId)
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;
            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TNI_InsertMyTraining, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[12];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.EmpId, empId);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.SelfTrainingTypeId, myTraining.TrainingTypeID);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.SelfTrainingName, myTraining.TrainingName);
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.SelfTotalHours, myTraining.TotalHours);
                sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.SelfStartDate, myTraining.TrainingStartDate);
                sqlParam[5] = objCommand.Parameters.AddWithValue(SPParameter.SelfEndDate, myTraining.TrainingEndDate);
                sqlParam[6] = objCommand.Parameters.AddWithValue(SPParameter.SelfWebsite, myTraining.Website);
                sqlParam[7] = objCommand.Parameters.AddWithValue(SPParameter.SelfComments, String.IsNullOrEmpty(myTraining.TrainingComments) ? String.Empty : myTraining.TrainingComments);
                sqlParam[8] = objCommand.Parameters.AddWithValue(SPParameter.SelfCertificate, String.IsNullOrEmpty(myTraining.CertificateName) ? String.Empty : myTraining.CertificateName);
                sqlParam[11] = objCommand.Parameters.AddWithValue(SPParameter.SelfCertificateGuid, String.IsNullOrEmpty(myTraining.CertificateGuid) ? String.Empty : myTraining.CertificateGuid);
                sqlParam[9] = objCommand.Parameters.AddWithValue(SPParameter.SelfOutTrainingId, 0);
                sqlParam[9].Direction = ParameterDirection.Output;
                sqlParam[10] = objCommand.Parameters.AddWithValue(SPParameter.TrainingId, myTraining.Operation == CommonConstants.Update ? myTraining.TrainingId : 0);

                objCommand.ExecuteNonQuery();
                return Convert.ToInt32(sqlParam[9].Value);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, RaiseTrainingRqst, "SaveMyTraining", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        public IEnumerable<MyTrainingModel> GetMyTrainings(int empId)
        {
            List<MyTrainingModel> myTrainings = new List<MyTrainingModel>();
            DataAccessClass daTraining = new DataAccessClass();
            SqlParameter[] sqlParam = new SqlParameter[1];

            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TNI_GetSelfTrainingsByEmpID, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.EmpId, empId);

                objReader = objCommand.ExecuteReader();

                //SqlDataReader dr = daTraining.ExecuteReaderSP(SPNames.TNI_GetSelfTrainingsByEmpID);
                MyTrainingModel myTraining = null;

                while (objReader.Read())
                {
                    myTraining = new MyTrainingModel();

                    myTraining.TrainingTypeID = Convert.ToInt32(objReader[1].ToString().Trim());
                    myTraining.TrainingId = Convert.ToInt32(objReader[0].ToString().Trim());
                    myTraining.TrainingName = Convert.ToString(objReader[2].ToString().Trim());
                    myTraining.TotalHours = Convert.ToInt32(objReader[3].ToString().Trim());
                    myTraining.TrainingStartDate = Convert.ToDateTime(objReader[4].ToString().Trim());
                    myTraining.TrainingEndDate = Convert.ToDateTime(objReader[5].ToString().Trim());
                    myTraining.Website = Convert.ToString(objReader[6].ToString().Trim());
                    myTraining.TrainingComments = Convert.ToString(objReader[7].ToString().Trim());
                    myTraining.CertificateName = Convert.ToString(objReader[8].ToString().Trim());
                    myTraining.CreatedDate = Convert.ToDateTime(objReader[9].ToString().Trim());
                    myTraining.TrainingTypeName = Convert.ToString(objReader[10].ToString().Trim());
                    myTraining.CertificateGuid = Convert.ToString(objReader[11].ToString().Trim());
                    
                    myTrainings.Add(myTraining);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetMyTrainings", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                daTraining.CloseConncetion();
            }
            return myTrainings;
        }

        public int DeleteMyTraining(int trainingId)
        {
            int deleteStatus = 0;
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;

            try
            {
                Master m = new Master();
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TNI_DeleteSelfTraining, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.TrainingId, trainingId);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.Status, 0);
                sqlParam[1].Direction = ParameterDirection.Output;

                objCommand.ExecuteNonQuery();
                deleteStatus = Convert.ToInt32(sqlParam[1].Value);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "DeleteMyTraining", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            return deleteStatus;
        }

        public MyTrainingModel GetMyTrainingDetails(int trainingId)
        {
            MyTrainingModel myTraining = new MyTrainingModel();
            DataAccessClass daTraining = new DataAccessClass();

            try
            {
                daTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.TrainingId, SqlDbType.Int);
                sqlParam[0].Value = trainingId;

                SqlDataReader dr = daTraining.ExecuteReaderSP(SPNames.TNI_GetSelfTrainingDetails, sqlParam);
                while (dr.Read())
                {
                    myTraining.TrainingTypeID = Convert.ToInt32(dr[1].ToString().Trim());
                    myTraining.TrainingId = Convert.ToInt32(dr[0].ToString().Trim());
                    myTraining.TrainingName = Convert.ToString(dr[2].ToString().Trim());
                    myTraining.TotalHours = Convert.ToInt32(dr[3].ToString().Trim());
                    myTraining.TrainingStartDate = Convert.ToDateTime(dr[4].ToString().Trim());
                    myTraining.TrainingEndDate = Convert.ToDateTime(dr[5].ToString().Trim());
                    myTraining.Website = Convert.ToString(dr[6].ToString().Trim());
                    myTraining.TrainingComments = Convert.ToString(dr[7].ToString().Trim());
                    myTraining.CertificateName = Convert.ToString(dr[8].ToString().Trim());
                    myTraining.CreatedDate = Convert.ToDateTime(dr[9].ToString().Trim());
                    myTraining.CertificateGuid = Convert.ToString(dr[10].ToString().Trim());
                }               
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, TrainingCourse, "GetTrainingCourses", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                daTraining.CloseConncetion();
            }
            return myTraining;
        }

        #endregion
        
    }
}