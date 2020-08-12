using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using RMS.Common.BusinessEntities;

namespace Domain.Entities
{
    public partial class TrainingCourseModel
    {
        public int CourseID { get; set; }

        public int TrainingTypeID { get; set; }

        public int TrainingNameID { get; set; }

        public string TrainingName { get; set; }

        public int? VendorID { get; set; }

        public string VendorEmailId { get; set; }

        public string CourseContentFiles { get; set; }
               
        public string DARFormFileName { get; set; }

        public string TechnicalPanelIds { get; set; }

        public string TechnicalPanelNames { get; set; }

        public string TrainerName { get; set; }

        public string TrainerNameInternal { get; set; }

        public string TrainerNameInternalID { get; set; }

        public string TrainerProfileFiles { get; set; }

        public DateTime TrainingStartDate { get; set; }

        public DateTime TrainingEndDate { get; set; }

        public string TrainingComments { get; set; }

        public float? NoOfdays { get; set; }

        public float? TotalTrainigHours { get; set; }

        public int NominationTypeID { get; set; }

        public DateTime? LastDateOfNomination { get; set; }

        public string EffectivenessIds { get; set; }

        public string SoftwareDetails { get; set; }

        public float? TotalCost { get; set; }

        public DateTime? PaymentDueDt { get; set; }

        public DateTime? PaymentDates { get; set; }

        public string PaymentComments { get; set; }

        public int PaymentModeID { get; set; }

        public int TrainingModeID { get; set; }

        public bool PaymentMade { get; set; }

        public int PaymentMadeID { get; set; }

        public bool IndividualPayementTraining { get; set; }

        public string RaiseTrainingIds { get; set; }

        public bool IsActive { get; set; }

        public string RequestedBy { get; set; }

        public string RequestedFor { get; set; }

        public string TrainingLocation { get; set; }

        public string CourseName { get; set; }

        public bool FeedbackSent { get; set; }

        public List<FileDetails> FileDetails { get; set; }

        public DateTime AttendanceDate { get; set; }

        public List<DateTime> AttendanceDates { get; set; }

        //Harsha Issue Id- 58975 and 58958 - Start       
        public int TrainingStatus { get; set; }
        //Harsha Issue Id- 58975 and 58958 - End
    }

    public class CourseDetails
    {

        public int CourseID { get; set; }

        [DisplayName("Course Name")]
        public string CourseName { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime TrainingStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime TrainingEndDate { get; set; }

        public int TrainingTypeID { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Training Type")]
        public string TrainingType { get; set; }

        public int TrainingNameID { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Training Name")]
        public string TrainingName { get; set; }

        [DisplayName("Close Training")]
        public bool CourseCloseFlag { get; set; }

        [DisplayName("Invite Nominations")]
        public bool InviteNominationFlag { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Last date for Nomination")]
        [DisplayFormat( DataFormatString="{0:dd/MM/yyyy}", ApplyFormatInEditMode=true )]
        public DateTime? LastNominationDate { get; set; }

        public string Status { get; set; }

        public int IsActive {get; set;}

        public int IsAssessment { get; set; }

        //Harsha- Issue Id- 58975 & 58958 - Start
        //Description- Making Training Cost Editable for Internal Training after Closed status of the training
        public int TrainingModeId;
        public int TrainingStatusId;
        //Harsha- Issue Id- 58975 & 58958 - End

    }

    public class InviteNominationModel
    {
        //public int AllRaveId { get; set; }

        //public int EmailList { get; set; }

        //public int PMId { get; set; }

        //public string Employees { get; set; }
        
        public int CourseID { get; set; }

        public string CourseName { get; set; }

        public bool InviteNominationFlag { get; set; }

        public DateTime? NominationEndDate { get; set; }

        public string TrainingName { get; set; }

        public string TrainerName { get; set; }

        public string TrainingMode { get; set; }

        public DateTime TrainingStartDate { get; set; }

        public DateTime TrainingEndDate { get; set; }

        public float NoOfDays { get; set; }

        public string strCourseContent { get; set; }

        //public string NominationLinkText { get; set; }
    }

}
