using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IAssessmentRepository
    {
        #region  Method

        int SaveAssessmentPaper(AssessmentPaperModel assessmentPaperModel);

        AssessmentModel GetAssessmentPaperDetails(int CourseId);//assessmentPaperId)

        int SaveAssessmentQuestion(int assessmentPaperId,AssessmentQuestionsModel assessmentQuestionsModel);

        int DeleteAssessmentQuestion(int questionId, int paperId);

        AssessmentModel GetAssessmentQuestionDetails(int assessmentPaperId, int questionId);

        int UpdateAssessmentQuestionDetails(AssessmentQuestionsModel assessmentQuestionsModel);

        AssessmentModel GetAssessmentPaperDetailsByCourseId(int courseId);        

        int AddExistingQuestionInPaper(int assessmentPaperId, int questionId);

        string  SaveAssessmentResult(DataTable dtResultDetails, AssessmentResult assessmentResult);

        bool SetAssessment(AssessmentPaperModel obj);

        DataTable  GetNominatedEmployees(int courseId);
        #endregion  Method
    }
}
