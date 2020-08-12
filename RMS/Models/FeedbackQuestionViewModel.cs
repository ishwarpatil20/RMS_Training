using Domain.Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMS.Models
{
    public class FeedbackQuestionViewModel
    {
        public SelectList CourseNames { get; set; }
        public List<FeedbackModel> FeedbackModel { get; set; }
        public List<QuestionModel> QuestionModel { get; set; }
        public SelectList CourseName { get; set; }
        
        public FeedbackQuestionViewModel(ITrainingService service,string empId) //Feedback Page For Employees
        {
            CourseName = new SelectList(service.GetTrainingNameList(empId), "Key", "Value");
        }
        public FeedbackQuestionViewModel(ITrainingService service) // Feedback Page for RMO
        {
            CourseNames = new SelectList(service.GetTrainingNameList(), "Key", "Value"); 
          
        }

        public NominationModel NominationModel { get; set; } 

        public FeedbackQuestionViewModel()
        {
            // TODO: Complete member initialization
            //ITrainingService service ;
            //TrainingName = new SelectList(service.GetTrainingNameList(), "Key", "Value");
        }
    }
}