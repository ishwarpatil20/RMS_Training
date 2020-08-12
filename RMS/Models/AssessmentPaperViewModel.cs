using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMS.Models
{
    public class AssessmentPaperViewModel
    {
        public int AssessmentPaperId { get; set; }

        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public int TrainingNameId { get; set; }

        public IEnumerable<SelectListItem> TrainingCourses { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime AssessmentDate { get; set; }

        [Required(ErrorMessage = " ")]
        [Range(0, 999, ErrorMessage = "Time duration can not exceed beyond 999.")]

        public int TimeDuration { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int LastEditedBy { get; set; }

        public DateTime LastEditedOn { get; set; }

        public string AssessmentDataDDMMYYY { get; set; }

        public string  AssessmentPaperIdEncrypt { get; set; }
        
    }
}