using Domain.Entities;
using Infrastructure.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AssessmentService : IAssessmentService
    {
        private readonly IAssessmentRepository _IAssessmentRepository;
        public AssessmentService(IAssessmentRepository assessmentRepository)
        {
            _IAssessmentRepository = assessmentRepository;
        }

        public int SaveAssessmentPaper(AssessmentPaperModel assessmentPaperModel)
        {
            return _IAssessmentRepository.SaveAssessmentPaper(assessmentPaperModel);
        }
        public AssessmentModel GetAssessmentPaperDetails(int assessmentPaperId)
        {
            return _IAssessmentRepository.GetAssessmentPaperDetails(assessmentPaperId);
        }
        public int SaveAssessmentQuestion(int assessmentPaperId, AssessmentQuestionsModel assessmentQuestionsModel)
        {
            return _IAssessmentRepository.SaveAssessmentQuestion(assessmentPaperId, assessmentQuestionsModel);
        }
        public int DeleteAssessmentQuestion(int questionId, int paperId)
        {
            return _IAssessmentRepository.DeleteAssessmentQuestion(questionId, paperId);
        }
        public AssessmentModel GetAssessmentQuestionDetails(int assessmentPaperId, int questionId)
        {
            return _IAssessmentRepository.GetAssessmentQuestionDetails(assessmentPaperId, questionId);
        }
        public int UpdateAssessmentQuestionDetails(AssessmentQuestionsModel assessmentQuestionsModel)
        {
            return _IAssessmentRepository.UpdateAssessmentQuestionDetails(assessmentQuestionsModel);
        }
        public AssessmentModel GetAssessmentPaperDetailsByCourseId(int courseId)
        {
            return _IAssessmentRepository.GetAssessmentPaperDetailsByCourseId(courseId);
        }        
        public int AddExistingQuestionInPaper(int assessmentPaperId, int questionId)
        {
            return _IAssessmentRepository.AddExistingQuestionInPaper(assessmentPaperId, questionId);
        }
        public string  SaveAssessmentResult(DataTable dtResultDetails, AssessmentResult assessmentResult)
        {
            return _IAssessmentRepository.SaveAssessmentResult(dtResultDetails, assessmentResult);
        }

        public bool SetAssessment(AssessmentPaperModel obj)
        {
            return _IAssessmentRepository.SetAssessment(obj);
        }
        public DataTable GetNominatedEmployees(int courseId)
        {
            return _IAssessmentRepository.GetNominatedEmployees(courseId);
        }
    }

}
