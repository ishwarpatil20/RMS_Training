using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Infrastructure;
using RMS.Common.BusinessEntities;
using RMS.Common.BusinessEntities.Common;
using RMS.Common.Constants;
using System.Collections;

namespace RMS.Models
{
    public class TrainingCourseViewModel : IValidatableObject
    {
        public TrainingCourseViewModel()
        {
            this.TrainingTypeDetails = new SelectList(CommonRepository.GetMasterCategoryList(CommonConstants.TrainingType, true, new string[] { }).Where(x => x.Text == "Technical" || x.Text == "Soft Skill" || x.Text == "Select").ToList(), "Value", "Text");
            this.TrainingNameDetails = CommonRepository.GetEmptySelectList("0");
            this.PaymentModeDetails = new SelectList(CommonRepository.GetMasterCategoryList(CommonConstants.PaymentMode, true, new string[] { }).ToList(), "Value", "Text");
            this.TrainingModeDetails = new SelectList(CommonRepository.GetMasterCategoryList(CommonConstants.TrainingMode, false, new string[] { }).ToList(), "Value", "Text");
            this.TrainingModeID = Convert.ToInt32(CommonConstants.ExternalTrainer);
            this.PaymentMadeDetails = new SelectList(CommonRepository.GetMasterCategoryList(CommonConstants.PaymentMade, false, new string[] { }).ToList(), "Value", "Text");
            this.PaymentMadeID = CommonConstants.PaymentMadeNo;
            this.NominationDetails = new SelectList(CommonRepository.GetMasterCategoryList(CommonConstants.NominationType, false, new string[] { }).ToList(), "Value", "Text");
            this.NominationTypeID = CommonConstants.ManagerNomination;
            this.VendorDetails = CommonRepository.FillMasterVendorList();
            this.AllEffectivenessDetails = CommonRepository.GetMasterTrainingEffectivenessDetails();
            this.SelectedEffectiveness = new List<Effectiveness>();
            this.RaiseRequestDetails = new RaiseRequestViewModel();
            this.objCommonModel = CommonRepository.FillPopUpEmployeeList("");
            this.TrainingStartDate = this.TrainingEndDate = null;
            this.EmployeeDetails = new SelectList(CommonRepository.GetEmployeeNameList(true,"0"), "Value", "Text");
        }

        public TrainingCourseViewModel(string trainingNameId, string trainingName,string trainingTypeId)
        {
            this.TrainingTypeDetails = new SelectList(CommonRepository.GetMasterCategoryList(CommonConstants.TrainingType, true, new string[] { }).Where(x => x.Text == "Technical" || x.Text == "Soft Skill" || x.Text == "Select").ToList(), "Value", "Text");
            this.TrainingNameDetails = new SelectList(CommonRepository.GetListWithDefault(trainingName, trainingNameId, true), "Value", "Text", "Selected");
            this.PaymentModeDetails = new SelectList(CommonRepository.GetMasterCategoryList(CommonConstants.PaymentMode, true, new string[] { }).ToList(), "Value", "Text");
            this.TrainingModeDetails = new SelectList(CommonRepository.GetMasterCategoryList(CommonConstants.TrainingMode, false, new string[] { }).ToList(), "Value", "Text");
            this.PaymentMadeDetails = new SelectList(CommonRepository.GetMasterCategoryList(CommonConstants.PaymentMade, false, new string[] { }).ToList(), "Value", "Text");
            this.NominationDetails = new SelectList(CommonRepository.GetMasterCategoryList(CommonConstants.NominationType, false, new string[] { }).ToList(), "Value", "Text");
            this.VendorDetails = CommonRepository.FillMasterVendorList();
            this.AllEffectivenessDetails = CommonRepository.GetMasterTrainingEffectivenessDetails();
            this.SelectedEffectiveness = new List<Effectiveness>();
            this.RaiseRequestDetails = new RaiseRequestViewModel();
            this.objCommonModel = CommonRepository.FillPopUpEmployeeList("");
            this.EmployeeDetails = new SelectList(CommonRepository.GetEmployeeNameList(true, "0"), "Value", "Text");
        }
        public RaiseRequestViewModel RaiseRequestDetails { get; set; }

        public List<CommonModel> objCommonModel { get; set; }

        public int? CourseID { get; set; }

        //Harsha Issue Id- 58975 and 58958 - Start       
        public int TrainingStatus { get; set; }
        //Harsha Issue Id- 58975 and 58958 - End

        public IEnumerable<SelectListItem> TrainingTypeDetails { get; set; }

        [DisplayName("Training Type")]
        [Required(ErrorMessage = " ")]
        [Range(1, Int32.MaxValue, ErrorMessage = " ")]
        public int TrainingTypeID { get; set; }

        public IEnumerable<SelectListItem> TrainingNameDetails { get; set; }

        [DisplayName("Training Name")]
        [Required(ErrorMessage = " ")]
        [Range(1, Int32.MaxValue, ErrorMessage = " ")]
        public int TrainingNameID { get; set; }

        public SelectList VendorDetails { get; set; }

        [DisplayName("Vendor Name")]
        [Required(ErrorMessage = " ")]
        [Range(1, Int32.MaxValue, ErrorMessage = " ")]
        public int? VendorID { get; set; }

        [DisplayName("Vendor Email Id")]
        [Required(ErrorMessage = " ")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "<br/>Please enter valid Vendor Email Id.")]
        [StringLength(100, ErrorMessage = "<br/>Vendor Email Id cannot be longer than 100 characters.")]
        [DataType(DataType.EmailAddress)]
        public string VendorEmailId { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Trainer's Name")]
        [Required(ErrorMessage = " ")]//Please enter Trainer Name.
        //Poonam : 25/04/2016 Starts
        //Issue : Remove all special charater validation from all text field
        //[RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "<br/>Please enter valid Trainer Name.")]
        //Poonam : Ends
        [StringLength(100, ErrorMessage = "<br/>Trainer Name cannot be longer than 100 characters.")]
        public string TrainerName { get; set; }

        public string TrainerNameInternal { get; set; }
        public string TrainerNameInternalID { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Training Start Date")]
        [Required(ErrorMessage = " ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? TrainingStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Training End Date")]
        [Required(ErrorMessage = " ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? TrainingEndDate { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Comments")]
        [StringLength(200, ErrorMessage = "<br/>Training Comments shoulld not be longer than 200 characters.")]
        public string TrainingComments { get; set; }

        [DisplayName("Number of Days")]
        [Required(ErrorMessage = " ")]
        //[Range(1, 100, ErrorMessage = "Please enter Number of Days between 1 and 100.")]
        [Range(1, int.MaxValue, ErrorMessage = "<br/>Number of Days should be greater than zero.")]
        public float? NoOfdays { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Software Required")]
        [StringLength(200, ErrorMessage = "<br/>Software Required details shoulld not be longer than 200 characters.")]
        public string SoftwareDetails { get; set; }

        [DisplayName("Total Cost")]
        [Required(ErrorMessage = " ")]
        //[Range(1,int.MaxValue, ErrorMessage = "<br/>Total Cost should be greater than zero.")]
        public float? TotalCost { get; set; }

        //[DataType(DataType.Date)]
        [DisplayName("Payment Due Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? PaymentDueDt { get; set; }

        
        [DisplayName("Dates on which payment was made")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? PaymentDates { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Comments(if any)")]
        [StringLength(200, ErrorMessage = "<br/>Software Required details shoulld not be longer than 200 characters.")]
        public string PaymentComments { get; set; }

        public SelectList PaymentModeDetails { get; set; }

        [DisplayName("Payment Mode")]
        public int PaymentModeID { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase fileInvoice { get; set; }

        [DisplayName("Upload Invoice Form")]
        public FileUploadModel InvoiceFileDetails { get; set; }

        //public string CourseContentFiles { get; set; }

        [DataType(DataType.Upload)]
        public IEnumerable<HttpPostedFileBase> fileCourseContents { get; set; }

        [DisplayName("Course Content")]
        public FileUploadModel CourseContentFileDetails { get; set; }


        [DisplayName("Total Training Hours")]
        [Required(ErrorMessage = " ")]
        [Range(1, int.MaxValue, ErrorMessage = "<br/>Total No of Training Hours should be greater than zero.")]
        public float? TotalTrainingHours { get; set; }
        
        public SelectList NominationDetails { get; set; }

        [DisplayName("Type of Nomination")]
        public int NominationTypeID { get; set; }

        [DisplayName("Nomination End Date")]
        //[Required(ErrorMessage = "Nomination End Date")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime LastDateOfNomination { get; set; }
        

        [DisplayName("Effectiveness")]
        public IEnumerable<Effectiveness> AllEffectivenessDetails { get; set; }

        public IEnumerable<Effectiveness> SelectedEffectiveness { get; set; }

        public PostedEffectiveness PostedEffectiveness { get; set; }

        public int TrainingEffectivenessID { get; set; }

        [DisplayName("Select Technical Panel")]
        public string TechnicalPanelName { get; set; }

        [DisplayName("Select Technical Panel")]
        public string TechnicalPanelID { get; set; }

        public SelectList TrainingModeDetails { get; set; }

        [DisplayName("Training Mode")]
        //[Required(ErrorMessage = " ")]
        //[Range(1, Int32.MaxValue, ErrorMessage = " ")]
        public int TrainingModeID { get; set; }

        public SelectList PaymentMadeDetails { get; set; }

        [DisplayName("Payment Made")]
        public int? PaymentMadeID { get; set; }

        [DisplayName("Individual Training")]
        public bool IndividualPayementTraining { get; set; }

        [DisplayName("Raise ID")]
        public string RaiseTrainingIds { get; set; }

        [DisplayName("Course Status")]
        public bool IsActive { get; set; }

        //[DataType(DataType.Text)]
        //[DisplayName("Upload DAR Form")]
        //public string DARForm { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase fileDAR { get; set; }

        [DisplayName("Upload DAR Form")]
        public FileUploadModel DARFormFileDetails { get; set; }

        //[DataType(DataType.Text)]
        //[DisplayName("Trainer Profile")]
        //public string TrainerProfile { get; set; }

        [DataType(DataType.Upload)]
        public IEnumerable<HttpPostedFileBase> TrainerProfileFiles { get; set; }

        [DisplayName("Trainer Profile")]
        public FileUploadModel TrainerProfileFileDetails { get; set; }

        public string PageMode { get; set; }

        [DisplayName("Requested By")]
        [Required(ErrorMessage = " ")]
        [StringLength(200, ErrorMessage = "<br/>Requested For shoulld not be longer than 200 characters.")]
        public string RequestedBy { get; set; }

        public IEnumerable<SelectListItem> EmployeeDetails { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Requested For")]
        [Required(ErrorMessage = " ")]
        [StringLength(200, ErrorMessage = "<br/>Requested For shoulld not be longer than 200 characters.")]
        public string RequestedFor { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Location")]
        [Required(ErrorMessage = " ")]
        [StringLength(100, ErrorMessage = "<br/>Location shoulld not be longer than 100 characters.")]
        public string TrainingLocation { get; set; }

        [DisplayName("Course Name")]
        [Remote("doesCourseNameExists", "TrainingCourse", HttpMethod = "Post", ErrorMessage = "Course Name already exist. Please enter unique course name.")]
        public string CourseName { get; set; }

        public bool FeedbackSent { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TrainingEndDate < TrainingStartDate)
            {
                yield return new ValidationResult("Training End Date should not be less than Training Start Date.");
            }

            if (String.IsNullOrEmpty(RaiseTrainingIds))
            {
                yield return new ValidationResult("Please select Raise Trainings.");
            }
        }
    }

    public class ViewCourseViewModel
    {
        public ViewCourseViewModel()
        {
            this.TrainingTypeDetails = CommonRepository.FillMasterRadioButtonList(CommonConstants.TrainingType).Where(x => x.Text == "Technical" || x.Text == "Soft Skill").ToList<SelectListItem>();
            this.TrainingTypeID = CommonConstants.TechnicalTrainingID;
            this.TrainingStatusID = 1;
            SetTrainingStatus();
        }

        public ViewCourseViewModel(string selVal)
        {
            IEnumerable<SelectListItem> allTrainingTypeDetails = CommonRepository.FillMasterRadioButtonList(CommonConstants.TrainingType);
            this.TrainingTypeID = Convert.ToInt32(selVal);

            List<SelectListItem> selTrainingTypeDetails = new List<SelectListItem>();
            foreach (var s in allTrainingTypeDetails)
            {
                if (s.Text == "Technical" || s.Text == "Soft Skill")
                {
                    selTrainingTypeDetails.Add(new SelectListItem { Text = s.Text, Value = s.Value, Selected = (s.Value == selVal) ? true : false });
                }
            }
            this.TrainingTypeDetails = selTrainingTypeDetails.AsEnumerable();
            this.TrainingStatusID = 1;
            SetTrainingStatus();
        }

        [DisplayName("Training Type")]
        public IEnumerable<SelectListItem> TrainingTypeDetails { get; set; }

        public List<CourseDetails> CourseDetails { get; set; }

        [DisplayName("Training Type ID")]
        public int TrainingTypeID { get; set; }

        public IEnumerable<SelectListItem> TrainingStatusDetails { get; set; }

        [DisplayName("Training Status")]
        public int TrainingStatusID { get; set; }

        private void SetTrainingStatus()
        {
            List<SelectListItem> TrainingStatusList = new List<SelectListItem>() { };
            HttpContext context = HttpContext.Current;

            TrainingStatusList.Add(new SelectListItem() { Selected = true, Text = "Active", Value = "1" });
            TrainingStatusList.Add(new SelectListItem() { Selected = false, Text = "Closed", Value = "3" });
            // Changed by : Venkatesh  : Start
            string Role = string.Empty;
            if (System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
            {
                ArrayList arrRolesForUser = new ArrayList();
                arrRolesForUser = (ArrayList)System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES];
                if (arrRolesForUser.Contains("admin"))
                    Role = "Admin";
            }
            // Changed by : Venkatesh  : End

            if (Convert.ToString(Role).ToLower() == CommonConstants.AdminRole.ToLower())
            {
                TrainingStatusList.Add(new SelectListItem() { Selected = false, Text = "Payment Pending", Value = "4" });
            }
            this.TrainingStatusDetails = new SelectList(TrainingStatusList, "Value", "Text");
        }
    }

    public class CoursePaymentViewModel
    {
        public CoursePaymentViewModel()
        {
            this.PaymentModeDetails = new SelectList(CommonRepository.GetMasterCategoryList(CommonConstants.PaymentMode, true, new string[] { }).ToList(), "Value", "Text");
            this.PaymentMadeDetails = new SelectList(CommonRepository.GetMasterCategoryList(CommonConstants.PaymentMade, false, new string[] { }).ToList(), "Value", "Text");
            this.TrainingModeDetails = new SelectList(CommonRepository.GetMasterCategoryList(CommonConstants.TrainingMode, false, new string[] { }).ToList(), "Value", "Text");
        }

        public int CourseID { get; set; }

        public SelectList TrainingModeDetails { get; set; }

        [DisplayName("Training Mode")]
        //[Required(ErrorMessage = " ")]
        //[Range(1, Int32.MaxValue, ErrorMessage = " ")]
        public int hdnTrainingMode { get; set; }

        [DisplayName("Total Cost")]
        [Range(1, int.MaxValue, ErrorMessage = "<br/>Total Cost should be greater than zero.")]
        public float? TotalCost { get; set; }

        //[DataType(DataType.Date)]
        [DisplayName("Payment Due Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? PaymentDueDt { get; set; }

        public SelectList PaymentMadeDetails { get; set; }

        [DisplayName("Payment Made")]
        public int? PaymentMadeID { get; set; }

        [DisplayName("Individual Training")]
        public bool IndividualPayementTraining { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Dates on which payment was made")]
        [StringLength(100, ErrorMessage = "<br/>Payment Date details shoulld not be longer than 100 characters.")]
        public string PaymentDates { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Comments(if any)")]
        [StringLength(200, ErrorMessage = "<br/>Software Required details shoulld not be longer than 200 characters.")]
        public string PaymentComments { get; set; }

        public SelectList PaymentModeDetails { get; set; }

        [DisplayName("Payment Mode")]
        public int PaymentModeID { get; set; }


        //[DataType(DataType.Date)]
        //[DisplayName("Training Start Date")]
        //[Required(ErrorMessage = " ")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        //public DateTime? hdnTrainingStartDate { get; set; }

        //[DataType(DataType.Date)]
        //[DisplayName("Training End Date")]
        //[Required(ErrorMessage = " ")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        //public DateTime? hdnTrainingEndDate { get; set; }

    }
}