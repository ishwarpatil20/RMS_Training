using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Services.Interfaces;
using Services;
using RMS.Common.Constants;
using System.ComponentModel;
using Infrastructure;


namespace RMS.Models
{
    public class NominationViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Training name")]
        public SelectList CourseName { get; set; }

        public string TrainingNameID { get; set; }

        public NominationViewModel()
        { }

        public NominationViewModel(ITrainingService service)
        {
            int empID = Convert.ToInt16(HttpContext.Current.Session["EmpID"]);
            CourseName = new SelectList(service.GetTrainingNameforAllCourses(empID), "Key", "Value");
        }
        //Harsha Issue Id-59073 - Start
        //Description- Training : Create emp Nominated page so that emp can view they nomination. Change confirmation email body
        public NominationViewModel(ITrainingService service,string pageName)
        {
            int empID = Convert.ToInt16(HttpContext.Current.Session["EmpID"]);
            CourseName = new SelectList(service.GetTrainingNamesByEmployeeId(empID), "Key", "Value");
        }
        //Harsha Issue Id-59073 - End
    }

    public class EmployeeNominationViewModel
    {

        public EmployeeNominationViewModel()
        { }

        public  EmployeeNominationViewModel(ITrainingService service)
        {
            this.EmployeeList = new SelectList(service.GetAllEmployeeList(), "Key", "Value");
            this.Priority = new SelectList(service.FillMasterDropDownList(CommonConstants.Priority), "Value", "Text");
            this.PreTrainingRating = new SelectList(service.FillMasterDropDownList(CommonConstants.PreTrainingRating), "Value", "Text");
            this.RMONominationList = new SelectList(service.GetAllEmployeeList(), "Key", "Value");
        }

        public EmployeeNominationViewModel(ITrainingService service, int empid)
        {
            this.EmployeeList = new SelectList(service.GetAllEmployeeList(), "Key", "Value");
            this.Priority = new SelectList(service.FillMasterDropDownList(CommonConstants.Priority), "Value", "Text");
            this.PreTrainingRating = new SelectList(service.FillMasterDropDownList(CommonConstants.PreTrainingRating), "Value", "Text");

            var employeedetail = service.GetEmployeeDetailByID(empid);
            this.EmployeeName = employeedetail.EmployeeName;
            this.EmailID = employeedetail.EmailID;
            this.Designation = employeedetail.Designation;
            this.Project = employeedetail.Project;
        }


        public EmployeeNominationViewModel(ITrainingService service, int trainingcourseID, int trainingnameID, int deleteemployeeid)
        {
            var employeedetail = service.GetEmployeeDetailForEdit(trainingcourseID, trainingnameID, deleteemployeeid);
            
            this.EmployeeList = new SelectList(service.GetAllEmployeeList(), "Key", "Value",employeedetail.EmployeeID);
            this.RMONominationList = new SelectList(service.GetAllEmployeeList(), "Key", "Value", employeedetail.EmployeeID);
            this.SelectedRMONominator = employeedetail.RMONominatorID;
            this.SelectedNominator =  employeedetail.NominationEmpID; 
            this.SelectedEmployee = employeedetail.EmployeeID;
            this.EmployeeName = employeedetail.EmployeeName;
            this.Priority = new SelectList(service.FillMasterDropDownList(CommonConstants.Priority), "Value", "Text", employeedetail.PriorityCode);
            this.SelectedPriority = employeedetail.PriorityCode;
            this.PreTrainingRating = new SelectList(CommonRepository.GetMasterCategoryList(CommonConstants.ProficiencyLevel, true, new string[] { }).ToList(), "Value", "Text", employeedetail.PreTrainingCode);
            this.SelectedPreTrainingRating = employeedetail.PreTrainingCode;
            this.ObjectiveForSoftSkill = employeedetail.ObjectiveForSoftSkill;
            this.Comment = employeedetail.Comment;
            this.TrainingTypeID = employeedetail.TrainingTypeID;
            this.NominationTypeID = employeedetail.NominationTypeID;
            //Neelam : 31/05/2017 Starts. IssueId 59566
            this.courseID = trainingcourseID;
            this.TrainingNameID = trainingnameID;
            //Neelam : 31/05/2017 End. IssueId 59566
            this.EditMode = true;
        }

       

        public SelectList EmployeeList { get; set; }

        [Required(ErrorMessage = "Employee Name Required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please Select Employee.")]
        [Display(Name = "Employee Name")]
        public int SelectedEmployee { get; set; }


        public int EmployeeCode { get; set; }

        public string EmployeeName { get; set; }

        public string EmailID { get; set; }

        public string Designation { get; set; }

        public string Project { get; set; }

        public bool EditMode{ get; set; }

        public int TrainingTypeID { get; set; }

        public int NominationTypeID { get; set; }

        public bool isRMOLogin { get; set; }

        public SelectList Priority { get; set; }

        [Required(ErrorMessage = "Priority Required")]
        [Range(1.0, int.MaxValue, ErrorMessage = "Please Select Priority.")]
        [Display(Name = "Priority")]
        public int SelectedPriority { get; set; }

        public SelectList PreTrainingRating { get; set; }

        public int SelectedPreTrainingRating { get; set; }

        [StringLength(200,ErrorMessage = "Max 200 character allowed.")]
        public string Comment { get; set; }

        public string ObjectiveForSoftSkill { get; set; }

        public SelectList RMONominationList { get; set; }
        
        [Display(Name = "Nominated By")]
        public int SelectedRMONominator { get; set; }

        public int SelectedNominator { get; set; }

        //Neelam : 31/05/2017 Starts. IssueId 59566
        public int courseID { get; set; }

        public int TrainingNameID { get; set; }
        //Neelam : 31/05/2017 End. IssueId 59566
    }

    public class AccomodationTravelViewModel
    {
        public NominationModel NominationModel { get; set; }
        public List<TravelDetailsModel> TravelDetailModel { get; set; }
    }

    public class InviteNominationViewModel : IValidatableObject
    {
        public InviteNominationViewModel() { }

        public InviteNominationViewModel(InviteNominationModel invitationModel)
        {
            this.CourseID = invitationModel.CourseID;
            this.CourseName = invitationModel.CourseName;
            this.TrainingName = invitationModel.TrainingName;
            this.TrainingMode = invitationModel.TrainingMode;
            this.TrainerName = invitationModel.TrainerName;
            this.TrainingStartDate = invitationModel.TrainingStartDate;
            this.TrainingEndDate = invitationModel.TrainingEndDate;
            this.NominationEndDate = invitationModel.NominationEndDate;
            this.NoOfDays = invitationModel.NoOfDays;
        }

        public InviteNominationViewModel(int courseId,string CourseName, string training, string mode, string trainer, DateTime startDt, DateTime endDt, float days, DateTime nominationDate)
        {
            this.CourseID = courseId;
            this.CourseName = CourseName;
            this.TrainingName = training;
            this.TrainingMode = mode;
            this.TrainerName = trainer;
            this.TrainingStartDate = startDt;
            this.TrainingEndDate = endDt;
            this.NominationEndDate = nominationDate;
            this.NoOfDays = days;
        }

        public int CourseID { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Last Date of Nomination")]
        [Required(ErrorMessage = " ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime?  NominationEndDate { get; set; }

        public int NominationTypeID { get; set; } 

        //[Display(Name = "All Rave Employees")]
        //public int AllRaveId { get; set; }

        //public int EmailList { get; set; }

        //[Display(Name="Line Manager/Functional Manager")]
        //public int PMId { get; set; }

        //public bool PM { get; set; }

        //[Display(Name = "Add Recepients")]
        //public string Employees { get; set; }

        [Display(Name = "Training Name")]
        public string  TrainingName { get; set; }
        
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        [Display(Name = "Trainer Name")]
        public string TrainerName { get; set; }

        [Display(Name = "Training Mode")]
        public string TrainingMode { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Training Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime TrainingStartDate { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Training End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime TrainingEndDate { get; set; }

        [Display(Name = "No of Days")]
        public float NoOfDays { get; set; }

        public string  CourseContentFileNames { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NominationEndDate > TrainingStartDate)
            {
                yield return new ValidationResult("Nomination End Date should not be greater than Training Start Date.");
            }
        }
    }
        
    /// <summary>
    /// Harsha Issue Id-59073 - Start
    /// Description- Training : Create emp Nominated page so that emp can view they nomination. Change confirmation email body
    /// </summary>
    public class EmployeeTrainingNominationDetailsModel
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

        public EmployeeTrainingNominationDetailsModel(ITrainingService service, int courseId)
        {
            EmployeeTrainingNominationEntity entity = new EmployeeTrainingNominationEntity();
            entity = service.GetEmployeeTrainingDetails(courseId);
            this.TrainingNameId = entity.TrainingNameId;
            this.CourseComments = entity.CourseComments;
            this.NominationComments = entity.NominationComments;
            this.CourseId = entity.CourseId;
            this.CourseName = entity.CourseName;
            this.EmpId = entity.EmpId;
            this.EmployeeName = entity.EmployeeName;
            this.EndDate = entity.EndDate;
            this.LastDateOfNomination = entity.LastDateOfNomination;
            this.NominationTypeId = entity.NominationTypeId;
            this.NominatorId = entity.NominatorId;
            this.NominatorName = entity.NominatorName;
            this.NoOfDays = entity.NoOfDays;
            this.PreTrainingRating = entity.PreTrainingRating;
            this.Priority = entity.Priority;
            this.StartDate = entity.StartDate;
            this.TrainerName = entity.TrainerName;
            this.TrainingHours = entity.TrainingHours;
            this.TrainingMode = entity.TrainingMode;
            this.TrainingTypeId = entity.TrainingTypeId;
            this.ObjectiveForSoftSkill = entity.ObjectiveForSoftSkill;
        }
    }
    //Harsha Issue Id-59073 - End
   
}