using RMS.Common.BusinessEntities.Training;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace RMS.Models
{
    public class AssessmentViewModel 
    {
        public AssessmentPaperViewModel AssessmentPaper { get; set; }
        public List<AssementPaperDetailsViewModel> AssessmentPaperDetails { get; set; }
        public List<AssessmentQuestionsViewModel> AssessmentQuestions { get; set; }
        public IEnumerable<SelectListItem> ListCourses { get; set; }
    }


}