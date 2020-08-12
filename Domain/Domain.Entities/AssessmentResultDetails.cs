using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AssessmentResultDetails
    {
        public int AssessmentResultDetailsId { get; set; }

        public int AssessmentResultId { get; set; }

        public int QuestionId { get; set; }

        public string SelectedAnswer { get; set; }

        public int AnswerScore  { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int LastEditedBy { get; set; }

        public DateTime LastEditedOn { get; set; }
    }
}
