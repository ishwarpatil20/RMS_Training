using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AssessmentResult
    {
        public int AssessmentResultId { get; set; }

        public int AssessmentPaperId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime AssessmentDate { get; set; }

        public int TotalScore { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int LastEditedBy { get; set; }

        public DateTime LastEditedOn { get; set; }
    }
}
