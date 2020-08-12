using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AssessmentPaperModel
    {
        public int AssessmentPaperId { get; set; }

        public int CourseId { get; set; }
        
        public string CourseName { get; set; }

        public int TrainingNameId { get; set; }

        public DateTime AssessmentDate { get; set; }

        public int TimeDuration { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int LastEditedBy { get; set; }

        public DateTime LastEditedOn { get; set; }
        public string EmpIdAll { get; set; }
    }
}
