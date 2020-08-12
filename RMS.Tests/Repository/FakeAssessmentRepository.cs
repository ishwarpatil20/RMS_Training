using Domain.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Tests.Repository
{
    public class FakeAssessmentRepository:IAssessmentRepository
    {
        public int SaveAssessmentPaper(AssessmentPaperModel assessmentPaperModel)
        {
            int asessmentPaperId = 0;
            if (assessmentPaperModel.AssessmentPaperId == 0)
                asessmentPaperId = 1;
            return asessmentPaperId;
        }
        public AssessmentModel GetAssessmentPaperDetails(int assessmentPaperId)
        {
            AssessmentModel assessmentModel = new AssessmentModel();
            assessmentModel.AssessmentPaper = new AssessmentPaperModel();
            assessmentModel.AssessmentPaper.AssessmentPaperId = 1;
            return assessmentModel;
        }
        public int SaveAssessmentQuestion(int assessmentPaperId,AssessmentQuestionsModel assessmentQuestionsModel)
        {
            int questionId = 0;
            if (assessmentQuestionsModel.QuestionId == 0)
                questionId = 1;
            return questionId;
        }
        public int DeleteAssessmentQuestion(int questionId, int paperId)
        {
            int deleteStatus = 0;
            if (questionId == 1) { deleteStatus = 1; }
            
            return deleteStatus;
        }
        public AssessmentModel GetAssessmentQuestionDetails(int assessmentPaperId, int questionId)
        {
            AssessmentModel assessmentModel = new AssessmentModel();
            assessmentModel.AssessmentPaper = new AssessmentPaperModel();
            assessmentModel.AssessmentPaper.AssessmentPaperId = 1;
            return assessmentModel;
        }
        public int UpdateAssessmentQuestionDetails(AssessmentQuestionsModel assessmentQuestionsModel)
        {
            if (assessmentQuestionsModel.QuestionId == 1)
                return 1;
            else
                return 0;
        }
        public AssessmentModel GetAssessmentPaperDetailsByCourseId(int courseId)
        {
            AssessmentModel assessmentModel = new AssessmentModel();
            assessmentModel.AssessmentPaper = new AssessmentPaperModel();
            assessmentModel.AssessmentPaper.AssessmentPaperId = 1;
            return assessmentModel;
        }
        public int AddExistingQuestionInPaper(int assessmentPaperId, int questionId)
        {
            if (assessmentPaperId == 1)
                return 1;
            else
                return 0;
        }
        public int SaveAssessmentResult(DataTable dtResultDetails, AssessmentResult assessmentResult)
        {
            return 0;
        }
    }
}
