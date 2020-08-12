using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AssessmentPaperDetailsModel
    {
        public int AssessmentPaperDetailsId { get; set; }

        public int AssessmentPaperId { get; set; }

        public int QuestionId { get; set; }

        public bool IsActive { get; set; }

        public bool IsNewQuestion { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int LastEditedBy { get; set; }

        public DateTime LastEditedOn { get; set; }
    }
}
