using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMS.Models
{
    public class AssignAssessmentViewModel 
    {
        public int AssessmentPaperId { get; set; }
        public int CourseId { get; set; }
        public string  CourseName{ get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        //public List<AssementPaperDetailsViewModel> AssessmentPaperDetails { get; set; }
        public IEnumerable<SelectListItem> ListCourses { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime AssessmentDate { get; set; }

        [Required(ErrorMessage = " ")]
        [Range(0, 999, ErrorMessage = "Time duration can not exceed beyond 999.")]

        public int TimeDuration { get; set; }
        public DataTable dtEmp { get; set; }
    }

    public class EmployeeList
    {
        public string EmpName { get; set; }
        public int EmpId { get; set; }
    }
}