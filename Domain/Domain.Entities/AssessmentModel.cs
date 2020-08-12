using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AssessmentModel
    {
        public AssessmentPaperModel AssessmentPaper { get; set; }
        public List<AssessmentPaperDetailsModel> AssessmentPaperDetails { get; set; }
        public List<AssessmentQuestionsModel> AssessmentQuestions { get; set; }
    }
}
