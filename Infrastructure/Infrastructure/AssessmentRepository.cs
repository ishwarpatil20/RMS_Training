using Domain.Entities;
using Infrastructure.Interfaces;
using RMS.Common;
using RMS.Common.Constants;
using RMS.Common.DataBase;
using RMS.Common.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AssessmentRepository : IAssessmentRepository
    {
        public int SaveAssessmentPaper(AssessmentPaperModel assessmentPaperModel)
        {
            int saveStatus = 0;
            int assessmentPaperId = 0;
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;

            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                Master master = new Master();

                objCommand = new SqlCommand(SPNames.AssessmentPaperSave, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[7];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.CourseId, assessmentPaperModel.CourseId);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.AssessmentDate, assessmentPaperModel.AssessmentDate);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.TimeDuration, assessmentPaperModel.TimeDuration);
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.CreatedById, master.GetEmployeeIDByEmailID());
                sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.IsActive, assessmentPaperModel.IsActive);
                sqlParam[5] = objCommand.Parameters.AddWithValue(SPParameter.Status, 0);
                sqlParam[6] = objCommand.Parameters.AddWithValue(SPParameter.AssessmentPaperId, 0);
                sqlParam[5].Direction = ParameterDirection.Output;
                sqlParam[6].Direction = ParameterDirection.Output;
                objCommand.ExecuteNonQuery();
                saveStatus = Convert.ToInt32(sqlParam[5].Value);
                assessmentPaperId = Convert.ToInt32(sqlParam[6].Value);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "SaveAssessmentPaper", "SaveAssessmentPaper", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            return assessmentPaperId;
        }
        public AssessmentModel GetAssessmentPaperDetails(int assessmentPaperId)
        {
            AssessmentModel assessmentModel = new AssessmentModel();
            assessmentModel.AssessmentPaper = new AssessmentPaperModel();
            assessmentModel.AssessmentPaperDetails = new List<AssessmentPaperDetailsModel>();
            assessmentModel.AssessmentQuestions = new List<AssessmentQuestionsModel>();
            DataAccessClass dataAccessClass = new DataAccessClass();

            try
            {
                dataAccessClass.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.AssessmentPaperId, SqlDbType.Int);
                sqlParam[0].Value = assessmentPaperId;
                SqlDataReader dr = dataAccessClass.ExecuteReaderSP(SPNames.GetAssessmentPaperDetailsById, sqlParam);
                while (dr.Read())
                {
                    assessmentModel.AssessmentPaper.AssessmentPaperId = Convert.ToInt32(dr[DbTableColumn.AssessmentPaperId]);
                    assessmentModel.AssessmentPaper.CourseId = Convert.ToInt32(dr[DbTableColumn.CourseID]);
                    assessmentModel.AssessmentPaper.CourseName = Convert.ToString(dr[DbTableColumn.CourseName]);
                    assessmentModel.AssessmentPaper.AssessmentDate = Convert.ToDateTime(dr[DbTableColumn.AssessmentDate]);
                    assessmentModel.AssessmentPaper.TimeDuration = Convert.ToInt32(dr[DbTableColumn.TimeDuration]);
                    assessmentModel.AssessmentPaper.TrainingNameId = Convert.ToInt32(dr[DbTableColumn.TrainingNameID ]);                    
                }
                if (dr.NextResult())
                {
                    while (dr.Read())
                    {
                        AssessmentPaperDetailsModel assessmentPaperDetailsModel = new AssessmentPaperDetailsModel();
                        assessmentPaperDetailsModel.AssessmentPaperDetailsId = Convert.ToInt32(dr[DbTableColumn.AssessmentPaperDetailsId]);
                        assessmentPaperDetailsModel.AssessmentPaperId = Convert.ToInt32(dr[DbTableColumn.AssessmentPaperId]);
                        assessmentPaperDetailsModel.QuestionId = Convert.ToInt32(dr[DbTableColumn.QuestionId]);
                        assessmentPaperDetailsModel.IsNewQuestion = Convert.ToBoolean(dr[DbTableColumn.IsNewQuestion]);
                        assessmentModel.AssessmentPaperDetails.Add(assessmentPaperDetailsModel);
                        AssessmentQuestionsModel assessmentQuestionsModel = new AssessmentQuestionsModel();
                        assessmentQuestionsModel.AssessmentPaperDetailsId = Convert.ToInt32(dr[DbTableColumn.AssessmentPaperDetailsId]);
                        assessmentQuestionsModel.AssessmentPaperId = Convert.ToInt32(dr[DbTableColumn.AssessmentPaperId]);
                        assessmentQuestionsModel.QuestionId = Convert.ToInt32(dr[DbTableColumn.QuestionId]);
                        assessmentQuestionsModel.Question = Convert.ToString(dr[DbTableColumn.QuestionDescription]);
                        assessmentQuestionsModel.QuestionImgFileName = Convert.ToString(dr[DbTableColumn.QuestionImgFileName]);
                        assessmentQuestionsModel.Option1Description = Convert.ToString(dr[DbTableColumn.Option1Description]);
                        assessmentQuestionsModel.Option2Description = Convert.ToString(dr[DbTableColumn.Option2Description]);
                        assessmentQuestionsModel.Option3Description = Convert.ToString(dr[DbTableColumn.Option3Description]);
                        assessmentQuestionsModel.Option4Description = Convert.ToString(dr[DbTableColumn.Option4Description]);
                        assessmentQuestionsModel.Option5Description = Convert.ToString(dr[DbTableColumn.Option5Description]);
                        assessmentQuestionsModel.Option6Description = Convert.ToString(dr[DbTableColumn.Option6Description]);
                        assessmentQuestionsModel.IsOption1Correct = Convert.ToBoolean(dr[DbTableColumn.IsOption1Correct]);
                        assessmentQuestionsModel.IsOption2Correct = Convert.ToBoolean(dr[DbTableColumn.IsOption2Correct]);
                        assessmentQuestionsModel.IsOption3Correct = Convert.ToBoolean(dr[DbTableColumn.IsOption3Correct]);
                        assessmentQuestionsModel.IsOption4Correct = Convert.ToBoolean(dr[DbTableColumn.IsOption4Correct]);
                        assessmentQuestionsModel.IsOption5Correct = Convert.ToBoolean(dr[DbTableColumn.IsOption5Correct]);
                        assessmentQuestionsModel.IsOption6Correct = Convert.ToBoolean(dr[DbTableColumn.IsOption6Correct]);
                        assessmentQuestionsModel.Option1ImageFileName = Convert.ToString(dr[DbTableColumn.Option1Image]);
                        assessmentQuestionsModel.Option2ImageFileName = Convert.ToString(dr[DbTableColumn.Option2Image]);
                        assessmentQuestionsModel.Option3ImageFileName = Convert.ToString(dr[DbTableColumn.Option3Image]);
                        assessmentQuestionsModel.Option4ImageFileName = Convert.ToString(dr[DbTableColumn.Option4Image]);
                        assessmentQuestionsModel.Option5ImageFileName = Convert.ToString(dr[DbTableColumn.Option5Image]);
                        assessmentQuestionsModel.Option6ImageFileName = Convert.ToString(dr[DbTableColumn.Option6Image]);
                        assessmentQuestionsModel.IsMultiChoiceAnswer = Convert.ToBoolean(dr[DbTableColumn.IsMultiChoiceAnswer]);
                        assessmentModel.AssessmentQuestions.Add(assessmentQuestionsModel);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "GetAssessmentPaperDetails", "GetAssessmentPaperDetails", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                dataAccessClass.CloseConncetion();
            }

            return assessmentModel;
        }
        public int SaveAssessmentQuestion(int assessmentPaperId, AssessmentQuestionsModel assessmentQuestionsModel)
        {
            int assessmentPaperDetailsId = 0;
            int saveStatus = 0;
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;

            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                Master master = new Master();

                objCommand = new SqlCommand(SPNames.Assessment_QuestionSave, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[30];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.QuestionId, 0);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.QuestionDescription, assessmentQuestionsModel.Question);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.QuestionImage, assessmentQuestionsModel.QuestionImgFileName);
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.Option1Description, assessmentQuestionsModel.Option1Description);
                sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.Option1Image, assessmentQuestionsModel.Option1ImageFileName);
                sqlParam[5] = objCommand.Parameters.AddWithValue(SPParameter.IsOption1Correct, assessmentQuestionsModel.IsOption1Correct);
                sqlParam[6] = objCommand.Parameters.AddWithValue(SPParameter.Option2Description, assessmentQuestionsModel.Option2Description);
                sqlParam[7] = objCommand.Parameters.AddWithValue(SPParameter.Option2Image, assessmentQuestionsModel.Option2ImageFileName);
                sqlParam[8] = objCommand.Parameters.AddWithValue(SPParameter.IsOption2Correct, assessmentQuestionsModel.IsOption2Correct);
                sqlParam[9] = objCommand.Parameters.AddWithValue(SPParameter.Option3Description, assessmentQuestionsModel.Option3Description);
                sqlParam[10] = objCommand.Parameters.AddWithValue(SPParameter.Option3Image, assessmentQuestionsModel.Option3ImageFileName);
                sqlParam[11] = objCommand.Parameters.AddWithValue(SPParameter.IsOption3Correct, assessmentQuestionsModel.IsOption3Correct);
                sqlParam[12] = objCommand.Parameters.AddWithValue(SPParameter.Option4Description, assessmentQuestionsModel.Option4Description);
                sqlParam[13] = objCommand.Parameters.AddWithValue(SPParameter.Option4Image, assessmentQuestionsModel.Option4ImageFileName);
                sqlParam[14] = objCommand.Parameters.AddWithValue(SPParameter.IsOption4Correct, assessmentQuestionsModel.IsOption4Correct);
                sqlParam[15] = objCommand.Parameters.AddWithValue(SPParameter.Option5Description, assessmentQuestionsModel.Option5Description);
                sqlParam[16] = objCommand.Parameters.AddWithValue(SPParameter.Option5Image, assessmentQuestionsModel.Option5ImageFileName);
                sqlParam[17] = objCommand.Parameters.AddWithValue(SPParameter.IsOption5Correct, assessmentQuestionsModel.IsOption5Correct);
                sqlParam[18] = objCommand.Parameters.AddWithValue(SPParameter.Option6Description, assessmentQuestionsModel.Option6Description);
                sqlParam[19] = objCommand.Parameters.AddWithValue(SPParameter.Option6Image, assessmentQuestionsModel.Option6ImageFileName);
                sqlParam[20] = objCommand.Parameters.AddWithValue(SPParameter.IsOption6Correct, assessmentQuestionsModel.IsOption6Correct);
                sqlParam[21] = objCommand.Parameters.AddWithValue(SPParameter.IsMultiChoiceAnswer, assessmentQuestionsModel.IsMultiChoiceAnswer);
                sqlParam[22] = objCommand.Parameters.AddWithValue(SPParameter.CreatedById, master.GetEmployeeIDByEmailID());
                sqlParam[23] = objCommand.Parameters.AddWithValue(SPParameter.CreatedByDate, assessmentQuestionsModel.CreatedOn);
                sqlParam[24] = objCommand.Parameters.AddWithValue(SPParameter.LastModifiedById, master.GetEmployeeIDByEmailID());
                sqlParam[25] = objCommand.Parameters.AddWithValue(SPParameter.LastModifiedDate, assessmentQuestionsModel.LastEditedOn);
                sqlParam[26] = objCommand.Parameters.AddWithValue(SPParameter.IsActive, assessmentQuestionsModel.IsActive);
                sqlParam[27] = objCommand.Parameters.AddWithValue(SPParameter.Status, 0);
                sqlParam[28] = objCommand.Parameters.AddWithValue(SPParameter.AssessmentPaperId, assessmentPaperId);
                sqlParam[29] = objCommand.Parameters.AddWithValue(SPParameter.AssessmentPaperDetailsId, 0);
                sqlParam[27].Direction = ParameterDirection.Output;
                sqlParam[29].Direction = ParameterDirection.Output;
                objCommand.ExecuteNonQuery();
                saveStatus = Convert.ToInt32(sqlParam[27].Value);
                assessmentPaperDetailsId = Convert.ToInt32(sqlParam[29].Value);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "SaveAssessmentQuestion", "SaveAssessmentQuestion", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            return assessmentPaperDetailsId;
        }

        public int DeleteAssessmentQuestion(int questionId, int paperId)
        {
            int deleteStatus = 0;
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;

            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                Master master = new Master();

                objCommand = new SqlCommand(SPNames.Assessment_DeleteQuestion, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.AssessmentPaperId, paperId);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.QuestionId, questionId);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.Status, 0);
                sqlParam[2].Direction = ParameterDirection.Output;
                objCommand.ExecuteNonQuery();
                deleteStatus = Convert.ToInt32(sqlParam[2].Value);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "SaveAssessmentPaper", "SaveAssessmentPaper", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
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
        public AssessmentModel GetAssessmentQuestionDetails(int assessmentPaperId, int questionId)
        {
            AssessmentModel assessmentModel = new AssessmentModel();
            assessmentModel.AssessmentPaper = new AssessmentPaperModel();
            assessmentModel.AssessmentPaperDetails = new List<AssessmentPaperDetailsModel>();
            assessmentModel.AssessmentQuestions = new List<AssessmentQuestionsModel>();
            DataAccessClass dataAccessClass = new DataAccessClass();

            try
            {
                dataAccessClass.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter(SPParameter.AssessmentPaperId, SqlDbType.Int);
                sqlParam[0].Value = assessmentPaperId;
                sqlParam[1] = new SqlParameter(SPParameter.QuestionId, SqlDbType.Int);
                sqlParam[1].Value = questionId;
                SqlDataReader dr = dataAccessClass.ExecuteReaderSP(SPNames.GetAssessmentQuestionDetails, sqlParam);
                while (dr.Read())
                {
                    assessmentModel.AssessmentPaper.AssessmentPaperId = Convert.ToInt32(dr[DbTableColumn.AssessmentPaperId]);
                    assessmentModel.AssessmentPaper.CourseId = Convert.ToInt32(dr[DbTableColumn.CourseID]);
                    assessmentModel.AssessmentPaper.CourseName = Convert.ToString(dr[DbTableColumn.TrainingName]);
                    assessmentModel.AssessmentPaper.AssessmentDate = Convert.ToDateTime(dr[DbTableColumn.AssessmentDate]);
                    assessmentModel.AssessmentPaper.TimeDuration = Convert.ToInt32(dr[DbTableColumn.TimeDuration]);
                    
                }
                if (dr.NextResult())
                {
                    while (dr.Read())
                    {
                        AssessmentPaperDetailsModel assessmentPaperDetailsModel = new AssessmentPaperDetailsModel();
                        assessmentPaperDetailsModel.AssessmentPaperDetailsId = Convert.ToInt32(dr[DbTableColumn.AssessmentPaperDetailsId]);
                        assessmentPaperDetailsModel.AssessmentPaperId = Convert.ToInt32(dr[DbTableColumn.AssessmentPaperId]);
                        assessmentPaperDetailsModel.QuestionId = Convert.ToInt32(dr[DbTableColumn.QuestionId]);
                        assessmentPaperDetailsModel.IsNewQuestion = Convert.ToBoolean(dr[DbTableColumn.IsNewQuestion]);
                        assessmentModel.AssessmentPaperDetails.Add(assessmentPaperDetailsModel);
                        AssessmentQuestionsModel assessmentQuestionsModel = new AssessmentQuestionsModel();
                        assessmentQuestionsModel.AssessmentPaperDetailsId = Convert.ToInt32(dr[DbTableColumn.AssessmentPaperDetailsId]);
                        assessmentQuestionsModel.AssessmentPaperId = Convert.ToInt32(dr[DbTableColumn.AssessmentPaperId]);
                        assessmentQuestionsModel.QuestionId = Convert.ToInt32(dr[DbTableColumn.QuestionId]);
                        assessmentQuestionsModel.Question = Convert.ToString(dr[DbTableColumn.QuestionDescription]);
                        assessmentQuestionsModel.QuestionImgFileName = Convert.ToString(dr[DbTableColumn.QuestionImgFileName]);
                        assessmentQuestionsModel.Option1Description = Convert.ToString(dr[DbTableColumn.Option1Description]);
                        assessmentQuestionsModel.Option2Description = Convert.ToString(dr[DbTableColumn.Option2Description]);
                        assessmentQuestionsModel.Option3Description = Convert.ToString(dr[DbTableColumn.Option3Description]);
                        assessmentQuestionsModel.Option4Description = Convert.ToString(dr[DbTableColumn.Option4Description]);
                        assessmentQuestionsModel.Option5Description = Convert.ToString(dr[DbTableColumn.Option5Description]);
                        assessmentQuestionsModel.Option6Description = Convert.ToString(dr[DbTableColumn.Option6Description]);
                        assessmentQuestionsModel.IsOption1Correct = Convert.ToBoolean(dr[DbTableColumn.IsOption1Correct]);
                        assessmentQuestionsModel.IsOption2Correct = Convert.ToBoolean(dr[DbTableColumn.IsOption2Correct]);
                        assessmentQuestionsModel.IsOption3Correct = Convert.ToBoolean(dr[DbTableColumn.IsOption3Correct]);
                        assessmentQuestionsModel.IsOption4Correct = Convert.ToBoolean(dr[DbTableColumn.IsOption4Correct]);
                        assessmentQuestionsModel.IsOption5Correct = Convert.ToBoolean(dr[DbTableColumn.IsOption5Correct]);
                        assessmentQuestionsModel.IsOption6Correct = Convert.ToBoolean(dr[DbTableColumn.IsOption6Correct]);
                        assessmentQuestionsModel.Option1ImageFileName = Convert.ToString(dr[DbTableColumn.Option1Image]);
                        assessmentQuestionsModel.Option2ImageFileName = Convert.ToString(dr[DbTableColumn.Option2Image]);
                        assessmentQuestionsModel.Option3ImageFileName = Convert.ToString(dr[DbTableColumn.Option3Image]);
                        assessmentQuestionsModel.Option4ImageFileName = Convert.ToString(dr[DbTableColumn.Option4Image]);
                        assessmentQuestionsModel.Option5ImageFileName = Convert.ToString(dr[DbTableColumn.Option5Image]);
                        assessmentQuestionsModel.Option6ImageFileName = Convert.ToString(dr[DbTableColumn.Option6Image]);
                        assessmentModel.AssessmentQuestions.Add(assessmentQuestionsModel);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "GetAssessmentQuestionDetails", "GetAssessmentQuestionDetails", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                dataAccessClass.CloseConncetion();
            }

            return assessmentModel;
        }
        public int UpdateAssessmentQuestionDetails(AssessmentQuestionsModel assessmentQuestionsModel)
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

                Master master = new Master();

                objCommand = new SqlCommand(SPNames.Assessment_UpdateQuestionDetails, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[26];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.QuestionId, assessmentQuestionsModel.QuestionId);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.QuestionDescription, assessmentQuestionsModel.Question);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.QuestionImage, assessmentQuestionsModel.QuestionImgFileName);
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.Option1Description, assessmentQuestionsModel.Option1Description);
                sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.Option1Image, assessmentQuestionsModel.Option1ImageFileName);
                sqlParam[5] = objCommand.Parameters.AddWithValue(SPParameter.IsOption1Correct, assessmentQuestionsModel.IsOption1Correct);
                sqlParam[6] = objCommand.Parameters.AddWithValue(SPParameter.Option2Description, assessmentQuestionsModel.Option2Description);
                sqlParam[7] = objCommand.Parameters.AddWithValue(SPParameter.Option2Image, assessmentQuestionsModel.Option2ImageFileName);
                sqlParam[8] = objCommand.Parameters.AddWithValue(SPParameter.IsOption2Correct, assessmentQuestionsModel.IsOption2Correct);
                sqlParam[9] = objCommand.Parameters.AddWithValue(SPParameter.Option3Description, assessmentQuestionsModel.Option3Description);
                sqlParam[10] = objCommand.Parameters.AddWithValue(SPParameter.Option3Image, assessmentQuestionsModel.Option3ImageFileName);
                sqlParam[11] = objCommand.Parameters.AddWithValue(SPParameter.IsOption3Correct, assessmentQuestionsModel.IsOption3Correct);
                sqlParam[12] = objCommand.Parameters.AddWithValue(SPParameter.Option4Description, assessmentQuestionsModel.Option4Description);
                sqlParam[13] = objCommand.Parameters.AddWithValue(SPParameter.Option4Image, assessmentQuestionsModel.Option4ImageFileName);
                sqlParam[14] = objCommand.Parameters.AddWithValue(SPParameter.IsOption4Correct, assessmentQuestionsModel.IsOption4Correct);
                sqlParam[15] = objCommand.Parameters.AddWithValue(SPParameter.Option5Description, assessmentQuestionsModel.Option5Description);
                sqlParam[16] = objCommand.Parameters.AddWithValue(SPParameter.Option5Image, assessmentQuestionsModel.Option5ImageFileName);
                sqlParam[17] = objCommand.Parameters.AddWithValue(SPParameter.IsOption5Correct, assessmentQuestionsModel.IsOption5Correct);
                sqlParam[18] = objCommand.Parameters.AddWithValue(SPParameter.Option6Description, assessmentQuestionsModel.Option6Description);
                sqlParam[19] = objCommand.Parameters.AddWithValue(SPParameter.Option6Image, assessmentQuestionsModel.Option6ImageFileName);
                sqlParam[20] = objCommand.Parameters.AddWithValue(SPParameter.IsOption6Correct, assessmentQuestionsModel.IsOption6Correct);
                sqlParam[21] = objCommand.Parameters.AddWithValue(SPParameter.IsMultiChoiceAnswer, assessmentQuestionsModel.IsMultiChoiceAnswer);
                sqlParam[22] = objCommand.Parameters.AddWithValue(SPParameter.LastModifiedById, master.GetEmployeeIDByEmailID());
                sqlParam[23] = objCommand.Parameters.AddWithValue(SPParameter.LastModifiedDate, assessmentQuestionsModel.LastEditedOn);
                sqlParam[24] = objCommand.Parameters.AddWithValue(SPParameter.IsActive, assessmentQuestionsModel.IsActive);
                sqlParam[25] = objCommand.Parameters.AddWithValue(SPParameter.Status, 0);
                sqlParam[25].Direction = ParameterDirection.Output;
                objCommand.ExecuteNonQuery();
                saveStatus = Convert.ToInt32(sqlParam[25].Value);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "UpdateAssessmentQuestionDetails", "UpdateAssessmentQuestionDetails", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
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
        
        public AssessmentModel GetAssessmentPaperDetailsByCourseId(int courseId)
        {
            AssessmentModel assessmentModel = new AssessmentModel();
            assessmentModel.AssessmentPaper = new AssessmentPaperModel();
            assessmentModel.AssessmentPaperDetails = new List<AssessmentPaperDetailsModel>();
            assessmentModel.AssessmentQuestions = new List<AssessmentQuestionsModel>();
            DataAccessClass dataAccessClass = new DataAccessClass();

            try
            {
                dataAccessClass.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.CourseId, SqlDbType.Int);
                sqlParam[0].Value = courseId;
                SqlDataReader dr = dataAccessClass.ExecuteReaderSP(SPNames.GetAssessmentPaperDetailsByCourseId, sqlParam);
                while (dr.Read())
                {
                    assessmentModel.AssessmentPaper.AssessmentPaperId = Convert.ToInt32(dr[DbTableColumn.AssessmentPaperId]);
                    assessmentModel.AssessmentPaper.CourseId = Convert.ToInt32(dr[DbTableColumn.CourseID]);
                    assessmentModel.AssessmentPaper.CourseName = Convert.ToString(dr[DbTableColumn.CourseName]);
                    assessmentModel.AssessmentPaper.AssessmentDate = Convert.ToDateTime(dr[DbTableColumn.AssessmentDate]);
                    assessmentModel.AssessmentPaper.TimeDuration = Convert.ToInt32(dr[DbTableColumn.TimeDuration]);
                    assessmentModel.AssessmentPaper.TrainingNameId = Convert.ToInt32(dr[DbTableColumn.TrainingNameID]);
                }
                if (dr.NextResult())
                {
                    while (dr.Read())
                    {
                        AssessmentPaperDetailsModel assessmentPaperDetailsModel = new AssessmentPaperDetailsModel();
                        assessmentPaperDetailsModel.AssessmentPaperDetailsId = Convert.ToInt32(dr[DbTableColumn.AssessmentPaperDetailsId]);
                        assessmentPaperDetailsModel.AssessmentPaperId = Convert.ToInt32(dr[DbTableColumn.AssessmentPaperId]);
                        assessmentPaperDetailsModel.QuestionId = Convert.ToInt32(dr[DbTableColumn.QuestionId]);
                        assessmentPaperDetailsModel.IsNewQuestion = Convert.ToBoolean(dr[DbTableColumn.IsNewQuestion]);
                        assessmentModel.AssessmentPaperDetails.Add(assessmentPaperDetailsModel);
                        AssessmentQuestionsModel assessmentQuestionsModel = new AssessmentQuestionsModel();
                        assessmentQuestionsModel.AssessmentPaperDetailsId = Convert.ToInt32(dr[DbTableColumn.AssessmentPaperDetailsId]);
                        assessmentQuestionsModel.AssessmentPaperId = Convert.ToInt32(dr[DbTableColumn.AssessmentPaperId]);
                        assessmentQuestionsModel.QuestionId = Convert.ToInt32(dr[DbTableColumn.QuestionId]);
                        assessmentQuestionsModel.Question = Convert.ToString(dr[DbTableColumn.QuestionDescription]);
                        assessmentQuestionsModel.QuestionImgFileName = Convert.ToString(dr[DbTableColumn.QuestionImgFileName]);
                        assessmentQuestionsModel.Option1Description = Convert.ToString(dr[DbTableColumn.Option1Description]);
                        assessmentQuestionsModel.Option2Description = Convert.ToString(dr[DbTableColumn.Option2Description]);
                        assessmentQuestionsModel.Option3Description = Convert.ToString(dr[DbTableColumn.Option3Description]);
                        assessmentQuestionsModel.Option4Description = Convert.ToString(dr[DbTableColumn.Option4Description]);
                        assessmentQuestionsModel.Option5Description = Convert.ToString(dr[DbTableColumn.Option5Description]);
                        assessmentQuestionsModel.Option6Description = Convert.ToString(dr[DbTableColumn.Option6Description]);
                        assessmentQuestionsModel.IsOption1Correct = Convert.ToBoolean(dr[DbTableColumn.IsOption1Correct]);
                        assessmentQuestionsModel.IsOption2Correct = Convert.ToBoolean(dr[DbTableColumn.IsOption2Correct]);
                        assessmentQuestionsModel.IsOption3Correct = Convert.ToBoolean(dr[DbTableColumn.IsOption3Correct]);
                        assessmentQuestionsModel.IsOption4Correct = Convert.ToBoolean(dr[DbTableColumn.IsOption4Correct]);
                        assessmentQuestionsModel.IsOption5Correct = Convert.ToBoolean(dr[DbTableColumn.IsOption5Correct]);
                        assessmentQuestionsModel.IsOption6Correct = Convert.ToBoolean(dr[DbTableColumn.IsOption6Correct]);
                        assessmentQuestionsModel.Option1ImageFileName = Convert.ToString(dr[DbTableColumn.Option1Image]);
                        assessmentQuestionsModel.Option2ImageFileName = Convert.ToString(dr[DbTableColumn.Option2Image]);
                        assessmentQuestionsModel.Option3ImageFileName = Convert.ToString(dr[DbTableColumn.Option3Image]);
                        assessmentQuestionsModel.Option4ImageFileName = Convert.ToString(dr[DbTableColumn.Option4Image]);
                        assessmentQuestionsModel.Option5ImageFileName = Convert.ToString(dr[DbTableColumn.Option5Image]);
                        assessmentQuestionsModel.Option6ImageFileName = Convert.ToString(dr[DbTableColumn.Option6Image]); 
                        assessmentQuestionsModel.IsMultiChoiceAnswer = Convert.ToBoolean(dr[DbTableColumn.IsMultiChoiceAnswer]);
                        assessmentModel.AssessmentQuestions.Add(assessmentQuestionsModel);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "GetAssessmentPaperDetailsByCourseId", "GetAssessmentPaperDetailsByCourseId", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                dataAccessClass.CloseConncetion();
            }

            return assessmentModel;
        }
        public int AddExistingQuestionInPaper(int assessmentPaperId, int questionId)
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

                Master master = new Master();

                objCommand = new SqlCommand(SPNames.AddExistingQuestionInPaper, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[9];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.AssessmentPaperId, assessmentPaperId);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.AssessmentQuestionId, questionId);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.IsActive, true);
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.CreatedById, master.GetEmployeeIDByEmailID());
                sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.CreatedByDate, DateTime.Now);
                sqlParam[5] = objCommand.Parameters.AddWithValue(SPParameter.LastModifiedById, master.GetEmployeeIDByEmailID());
                sqlParam[6] = objCommand.Parameters.AddWithValue(SPParameter.LastModifiedDate, DateTime.Now);

                sqlParam[7] = objCommand.Parameters.AddWithValue(SPParameter.Status, 0);
                sqlParam[7].Direction = ParameterDirection.Output;
                sqlParam[8] = objCommand.Parameters.AddWithValue(SPParameter.IsNewQuestion, 0);
                objCommand.ExecuteNonQuery();
                saveStatus = Convert.ToInt32(sqlParam[7].Value);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "AddExistingQuestionInPaper", "AddExistingQuestionInPaper", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
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
        public string  SaveAssessmentResult(DataTable dtResultDetails, AssessmentResult assessmentResult)
        {
            string  totalScore = "";
            
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;

            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();
                
                Master master = new Master();
                foreach (DataRow row in dtResultDetails.Rows)
                {
                    row["CreatedBy"] = master.GetEmployeeIDByEmailID();
                    //row["LastEditedBy"] = master.GetEmployeeIDByEmailID();
                }

                objCommand = new SqlCommand(SPNames.AddAssessmentResult, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[10];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.AssessmentPaperId, assessmentResult.AssessmentPaperId);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.EmployeeID, assessmentResult.EmployeeId);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.IsActive, assessmentResult.IsActive);
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.CreatedById, master.GetEmployeeIDByEmailID());
                sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.CreatedByDate, assessmentResult.CreatedOn);
                sqlParam[5] = objCommand.Parameters.AddWithValue(SPParameter.LastModifiedById, master.GetEmployeeIDByEmailID());
                sqlParam[6] = objCommand.Parameters.AddWithValue(SPParameter.LastModifiedDate, assessmentResult.LastEditedOn);
                sqlParam[7] = objCommand.Parameters.AddWithValue(SPParameter.AssessmentResultDetails, dtResultDetails);
                sqlParam[7].SqlDbType = SqlDbType.Structured;
                sqlParam[8] = objCommand.Parameters.AddWithValue(SPParameter.TotalScore, 0);
                sqlParam[8].Direction = ParameterDirection.Output;
                sqlParam[9] = objCommand.Parameters.AddWithValue(SPParameter.Status, 0);
                sqlParam[9].Direction = ParameterDirection.Output;
                
                objCommand.ExecuteNonQuery();
                totalScore = Convert.ToString(sqlParam[8].Value);
                totalScore += "," + Convert.ToString(sqlParam[9].Value);
                
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "SaveAssessmentResult", "SaveAssessmentResult", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            return totalScore;
        }

        public bool SetAssessment(AssessmentPaperModel obj)
        {
            bool flag = false;
             DataAccessClass dataAccessClass = new DataAccessClass();
            try
            {
                dataAccessClass.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter(SPParameter.CourseId, SqlDbType.Int);
                sqlParam[0].Value = Int32.Parse(obj.CourseId.ToString());

                sqlParam[1] = new SqlParameter(SPParameter.EmployeesId, SqlDbType.Text);
                sqlParam[1].Value = Convert.ToString(obj.EmpIdAll.ToString());

                sqlParam[2] = new SqlParameter(SPParameter.CreatedBy, SqlDbType.Int);
                sqlParam[2].Value = Int32.Parse(obj.CreatedBy.ToString());

               SqlDataReader dr = dataAccessClass.ExecuteReaderSP(SPNames.SetAssessment, sqlParam);
                while (dr.Read())
                {
                    flag = Convert.ToBoolean(dr[DbTableColumn.Status]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }


        public DataTable  GetNominatedEmployees(int courseId)
        {        
            List<DynamicGrid> objDynamicGridList;
            DataAccessClass objDBCon = new DataAccessClass();
            objDynamicGridList = new List<DynamicGrid>();
            //DynamicGrid objDynamicGrid = null;
            DataSet ds = null;
            SqlParameter[] sqlParam = new SqlParameter[1];
            try
            {
                objDBCon.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.CourseID, SqlDbType.Int);
                sqlParam[0].Value = courseId;

                ds = objDBCon.GetDataSet(SPNames.TNI_GetNominatedEmp, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }            
            finally
            {
                objDBCon.CloseConncetion();
            }
            return ds.Tables[0];
            //return objDynamicGridList;
        }

    }
}
