using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using RMS.Common;
using RMS.Common.AuthorizationManager;
using RMS.Common.Constants;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Domain.Entities
{
    public class TrainingModel
    {
        public TrainingModel() { }

        /// <summary>
        /// Get Training Modelk
        /// </summary>        
        public TrainingModel(string trainingType)
        {
            Master objMaster = new Master();
            this.UserEmpId = objMaster.GetEmployeeIDByEmailID(); //objMaster.GetEmployeeID(UserMailId);
            //this.RaiseID = CommonConstants.DefaultRaiseID;
            this.RaiseID = RaiseID != 0 ? CommonConstants.DefaultRaiseID : RaiseID;
            this.TrainingType = string.IsNullOrWhiteSpace(trainingType) ? CommonConstants.TechnicalTrainingID.ToString() : trainingType;
            this.Priority = CommonConstants.DefaultFlagZero.ToString();
            this.Status = CommonConstants.DefaultFlagZero.ToString();
            this.RequestedBy = CommonConstants.DefaultFlagZero.ToString();
            this.Quarter = CommonConstants.DefaultFlagZero.ToString();
        }

        public SelectList ListTrainingType;
        public SelectList ListTrainingName;
        public SelectList ListQuarter;
        public SelectList ListParticipants;
        public SelectList ListPriority;
        public SelectList ListCategory;
        public SelectList ListRequestedBy;
        public SelectList ListKSSType;


        //CommonModel objCommonModel = new CommonModel();

        public List<CommonModel> objCommonModel { get; set; }

        public string Operation { get; set; }
        /// <summary>
        /// Gets or sets agenda
        /// </summary>        
        [Required(ErrorMessage = " ")]
        public string Agenda { get; set; }

        /// <summary>
        /// Gets or sets BusinessImpact
        /// </summary>        
        public string BusinessImpact { get; set; }
        /// <summary>
        /// Gets or sets Category
        /// </summary>        
        [Required(ErrorMessage = " ")]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets Comments
        /// </summary>        
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets CreatedBy
        /// </summary>        
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets CreatedByEmailId
        /// </summary>        
        public string CreatedByEmailId { get; set; }

        /// <summary>
        /// Gets or sets CreateDate
        /// </summary>        
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets Date
        /// </summary>                
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = " ")]
        public DateTime Date { get; set; }



        /// <summary>
        /// Gets or sets Flag
        /// </summary>        
        public int Flag { get; set; }

        /// <summary>
        /// Gets or sets FlagPriority
        /// </summary>        
        public int FlagPriority { get; set; }

        /// <summary>
        /// Gets or sets FlagQuarter
        /// </summary>        
        public int FlagQuarter { get; set; }

        /// <summary>
        /// Gets or sets FlagRequestedBy
        /// </summary>        
        public int FlagRequestedby { get; set; }

        /// <summary>
        /// Gets or sets FlagStatus
        /// </summary>        
        public int FlagStatus { get; set; }

        /// <summary>
        /// Gets or sets NameOfParticipant
        /// </summary>        
        [Required(ErrorMessage = " ")]
        public string NameOfParticipant { get; set; }

        /// <summary>
        /// Gets or sets NameOfParticipantID
        /// </summary>        
        public string NameOfParticipantID { get; set; }

        /// <summary>
        /// Gets or sets NoOfParticipant
        /// </summary>        
        [Required(ErrorMessage = " ")]
        public string NoOfParticipant { get; set; }

        /// <summary>
        /// Gets or sets OutTechnicalID
        /// </summary>        
        public int OutTechnicalID { get; set; }

        /// <summary>
        /// Gets or sets Presenter
        /// </summary>        
        [Required(ErrorMessage = " ")]
        public string Presenter { get; set; }

        /// <summary>
        /// Gets or sets PresenterID
        /// </summary>        
        public string PresenterID { get; set; }

        /// <summary>
        /// Gets or sets Priority
        /// </summary>        
        [Required(ErrorMessage = " ")]
        public string Priority { get; set; }

        /// <summary>
        /// Gets or sets Quarter
        /// </summary>   
        //[Required]
        //[Range(1, Int32.MaxValue, ErrorMessage = "")]
        [Required(ErrorMessage = " ")]
        public string Quarter { get; set; }

        /// <summary>
        /// Gets or sets RaiseDetailsID
        /// </summary>        
        public int RaiseDetailsId { get; set; }

        /// <summary>
        /// Gets or sets RaiseID
        /// </summary>        
        public int RaiseID { get; set; }

        /// <summary>
        /// Gets or sets RequestedBy
        /// </summary>        
        [Required(ErrorMessage = " ")]
        public string RequestedBy { get; set; }

        /// <summary>
        /// Gets or sets RequestedByID
        /// </summary>        
        public int RequestedByID { get; set; }

        /// <summary>
        /// Gets or sets SeminarsName
        /// </summary>        
        [Required(ErrorMessage = " ")]
        public string SeminarsName { get; set; }

        /// <summary>
        /// Gets or sets SerialNo
        /// </summary>        
        public int SerialNo { get; set; }

        /// <summary>
        /// Gets or sets Status
        /// </summary>        
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets StatusName
        /// </summary>        
        public string StatusName { get; set; }

        /// <summary>
        /// Gets or sets Topic
        /// </summary>        
        [Required(ErrorMessage = " ")]
        public string Topic { get; set; }

        /// <summary>
        /// Gets or sets TrainingName
        /// </summary> 
        [Required(ErrorMessage = " ")]
        public string TrainingName { get; set; }

        /// <summary>
        /// Gets or sets TrainingNameOther
        /// </summary>        
        public string TrainingNameOther { get; set; }

        /// <summary>
        /// Gets or sets TrainingStatus
        /// </summary>        
        public string TrainingStatus { get; set; }

        /// <summary>
        /// Gets or sets TrainingType
        /// </summary>        
        [Required(ErrorMessage = " ")]
        public string TrainingType { get; set; }

        /// <summary>
        /// Gets or sets Type
        /// </summary>        
        [Required(ErrorMessage = " ")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets URL
        /// </summary>        
        public string URL { get; set; }

        /// <summary>
        /// Gets or sets UserEmpID
        /// </summary>        
        public int UserEmpId { get; set; }

        /// <summary>
        /// Gets or sets UserMailID
        /// </summary>        
        public string UserMailId { get; set; }


        /// <summary>
        /// Gets or sets IsDeleteEnable
        /// </summary>        
        public Boolean IsDeleteEnable { get; set; }


        public List<NominationModel> objNominationModel { get; set; }

        public int TrainingNameID { get; set; }

        public int QuarterNo { get; set; }

        public string RoleName { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = " ")]
        public DateTime SeminarsEndDate { get; set; }

        public string Location { get; set; }

        public string NameOfInstitute { get; set; }

        public string Attender { get; set; }
        public string AttenderId { get; set; }

        [Required(ErrorMessage = " ")]
        [Range(1, int.MaxValue, ErrorMessage = "Total No of Training Hours should be greater than zero.")]
        public int TotalNumberOfHours { get; set; }

        [Required(ErrorMessage = " ")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of Days should be greater than zero.")]
        public int TotalNumberOfDays { get; set; }

        public string hdnRequestedBy { get; set; }

        [Required(ErrorMessage = " ")]
        [RegularExpression(@"^\d+(.\d{1,10})?$", ErrorMessage = "Seminar cost should be decimal or numeric")]
        [Range(1,float.MaxValue, ErrorMessage = "Seminar cost must be greater than 0")]
        public float SeminarCost { get; set; }

        [Required(ErrorMessage = " ")]
        public string AdditionalLogistics { get; set; }

        public bool AttendanceExist { get; set; }

        public DateTime AttendanceDate { get; set; }

        public List<DateTime> AttendanceDates { get; set; }

    }
}
