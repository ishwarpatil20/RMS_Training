using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    [Serializable]
    public class FourCFeedback
    {
        // From T_Feedback Table
        public int FBID { get; set; }
        public int EmpId { get; set; }
        public int DesignationId { get; set; }
        public string LineManagerId { get; set; }
        public string FunctionalManagerId { get; set; }
        public string Competency { get; set; }
        public string Communication { get; set; }
        public string Commitment { get; set; }
        public string Collaboration { get; set; }
        public string ReviewerRemarks { get; set; }
        public int RatingOption { get; set; } //0 Add 1 Review

        // FROM T_4CRatingDetails Table
        public int DepartmentId { get; set; }
        public int ProjectId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Status { get; set; }
        public string Creator { get; set; }
        public string Reviewer { get; set; }
        public int SendForReviewBy { get; set; }
        public DateTime SendForReviewDate { get; set; }
        public DateTime ReviewTo { get; set; }
        public int ReviewedBy { get; set; }
        public int ReviewedDate { get; set; }
        public string ModifiedById { get; set; }

    }
}
