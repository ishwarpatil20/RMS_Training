using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FeedbackModel
    {
        public int FeedbackID { get; set; }
        public int CourseID { get; set; }
        public int EmpID { get; set; }
        public int QuestionID { get; set; }

        [Required(ErrorMessage = "Please fill appropriate ratings in feedback form")]
        public double Rating { get; set; }

        public string CommentsFeedback { get; set; }

        public string EmpName { get; set; }
        public string Description { get; set;}
       
    }

    public class QuestionModel
    {
        public int QuestionMasterID { get; set; }
        public string Description { get; set; }
    }

}
