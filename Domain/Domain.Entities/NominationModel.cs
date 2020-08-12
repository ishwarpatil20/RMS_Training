using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Domain.Entities;
using RMS.Common.Constants;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    public class NominationModel
    {
        public NominationModel() { }

        public NominationModel(string trainingType)
        {
        }
        public int CourseID { get; set; }

        public string CourseName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public SelectList TrainingName { get; set; }

        public int TrainingNameID { get; set; }

        public string TrainerName { get; set; }

        public string TrainingMode { get; set; }

        public SelectList CourseContent { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Comments { get; set; }

        public int NoOfDays { get; set; }

        public int TotalTrainingHours { get; set; }

        public DateTime? NominationDueDate { get; set; }

        public int NominatorID { get; set; }

        public bool IsConfirmNominationPage { get; set; }
                     
        public bool IsFoodForTrainer { get; set; }

        public string TrainerPreference { get; set; }

        public string ParticipantsPreference { get; set; }

        public string CommentsFoodAccomodaion { get; set; }

        public bool IsAccomodationTrainer { get; set; }

        public bool IsTravelDetailsTrainer { get; set; }

        public bool IsFoodTrainer { get; set; }

        public bool IsFoodParticipants { get; set; }

        
        public DateTime? AccomodationFromDate { get; set; }

        public DateTime? AccomodationToDate { get; set; }

        public bool IsSendMail { get; set; }
                     
        public int TrainingTypeID { get; set; }

        public int NominationTypeID { get; set; }

        public bool IsRMOLoggedIn { get; set; }

        public bool IsValidationRequired { get; set; }

        public string UploadedCourseFile { get; set; }

        public FileUploadModel CourseFile { get; set; }

        public string CommentsForFeedback { get; set; }

        public DateTime? FeedbackToBeSentTrainer { get; set; }

        public DateTime? FeedbackSentToTrainer { get; set; }

        public string ReasonSLANotMet { get; set; }

        public List<TrainingEffectiveness> employeeListModel { get; set; }

        public SelectList ListPreTrainingRating { get; set; }

        public SelectList ListEmployeeList { get; set; }

        public SelectList ListObjectiveMet { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PostRatingDueDate { get; set; }

        public int LoginEmpId { get; set; }

        public int EmpId { get; set; }

        public string RoleName { get; set; }

        public bool SendFeedback { get; set; }

        public DateTime FeedbackLastDate { get; set; }

        public bool SendMailToAll { get; set; }
                
        //Harsha
        public bool NoFeedbackReceived { get; set; }

        public bool SendMailToNewManager { get; set; }
    }

    public class TrainingEffectiveness
    {
        public string PreNominatorName { get; set; }

       
        public int PostNominatorNameID { get; set; }
        public string PostNominatorName { get; set; }
         //Neelam : 25/07/2017 start. IssueId 60562
        public int NominatorEmpID { get; set; }
         //Neelam : 25/07/2017 end. IssueId 60562
        public int PreTrainingRating { get; set; }

        public string PreTrainingRatingText { get; set; }

        [Required(ErrorMessage = " ")]
        [Range(1, int.MaxValue, ErrorMessage = "Please Select Employee.")]
        public int PostTrainingRating { get; set; }

        public int PreTrainingFlag { get; set; }
        public int PostTrainingFlag { get; set; }
        public int AssessmentFlag { get; set; }
        public int Assessment { get; set; }
        public int EmpId { get; set; }
        public string EmployeeName { get; set; }
        public int CourseID { get; set; }
        public string Comments { get; set; }
        public int TrainingEffectivenessFlag { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PostRatingDueDate { get; set; }

        public SelectList ListPreTrainingRating { get; set; }

        [Required(ErrorMessage = " ")]
        public string ObjectiveForNomination { get; set; }

        //public int ObjectiveMet { get; set; }
        
        public int IsObjectiveMet { get; set; }
        public int TrainingTypeID { get; set; }

        public bool IsAdminGroup { get; set; }
        public bool BehalfofManagerPostrating { get; set; }

        public bool IsReportingManagerUpdated { get; set; }
    }

    public class TravelDetailsModel
    {
        public int TravelDetailID { get; set; }

        public int CourseID { get; set; }

        public string FromLocation { get; set; }

        public string ToLocation { get; set; }

        public DateTime? Date { get; set; }

       

        //[DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        //[Required(ErrorMessage = " ")]
        //public DateTime Date { get; set; }

       
    }
    //Harsha
    public class EmployeeTrainingNominationEntity
    {
        public string EffectivenessId { get; set; }
        public int TrainingNameId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string TrainerName { get; set; }
        public string TrainingMode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CourseComments { get; set; }
        public string NominationComments { get; set; }
        public int NoOfDays { get; set; }
        public int TrainingHours { get; set; }
        public DateTime? LastDateOfNomination { get; set; }
        public int TrainingTypeId { get; set; }
        public int NominationTypeId { get; set; }
        public int EmpId { get; set; }
        public string EmployeeName { get; set; }
        public int NominatorId { get; set; }
        public string NominatorName { get; set; }
        public string PreTrainingRating { get; set; }
        public string Priority { get; set; }
        public string ObjectiveForSoftSkill { get; set; }
    }
}
