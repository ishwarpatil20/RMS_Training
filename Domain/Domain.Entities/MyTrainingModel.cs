using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
//using Infrastructure;
using RMS.Common.Constants;

namespace Domain.Entities
{
    public class MyTrainingModel //: IValidatableObject
    {
        public MyTrainingModel()
        {
            //this.TrainingTypeName = new SelectList(CommonRepository.GetMasterCategoryList(CommonConstants.TrainingType, true, new string[] { }).Where(x => x.Text == "Technical" || x.Text == "Soft Skill" || x.Text == "Select").ToList(), "Value", "Text");
            List<SelectListItem> typesList = new List<SelectListItem>();
            typesList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "" });            
            typesList.Add(new SelectListItem { Text = CommonConstants.TechnicalTrainingText, Value = CommonConstants.TechnicalTrainingID.ToString() });
            typesList.Add(new SelectListItem { Text = CommonConstants.SoftSkillTrainingText, Value = CommonConstants.SoftSkillsTrainingID.ToString() });

            this.TrainingType = new SelectList(typesList, "Value", "Text");
        }

        public int TrainingId { get; set; }

        [DisplayName("Training Type")]
        [Required(ErrorMessage = "<br/>")]
        [Range(1, Int32.MaxValue, ErrorMessage = " ")]
        public int TrainingTypeID { get; set; }

        public IEnumerable<SelectListItem> TrainingType { get; set; }

        [DisplayName("Training Name")]
        [Required(ErrorMessage = "<br/>")]
        [DataType(DataType.Text)]
        public string TrainingName { get; set; }

        [DisplayName("No. of Hours")]
        [Required(ErrorMessage = "<br />")]
        [Range(1, Int32.MaxValue, ErrorMessage = " ")]
        public int TotalHours { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Training Start Date")]
        [Required(ErrorMessage = "<br/>")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? TrainingStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Training End Date")]
        [Required(ErrorMessage = "<br />")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? TrainingEndDate { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Comments")]
        public string TrainingComments { get; set; }

        [DataType(DataType.Url)]
        [DisplayName("Website")]
        [Required(ErrorMessage = "<br />")]
        public string Website { get; set; }

        [DataType(DataType.Upload)]
        [DisplayName("Certificate")]    
        public string CertificateName { get; set; }

        public string CertificateGuid { get; set; }

        public string Operation { get; set; }

        public DateTime CreatedDate { get; set; }

        public string TrainingTypeName { get; set; }

        [DisplayName("Upoad Certificate")]
        public FileUploadModel CertificateFileDetails { get; set; }

        #region IValidatableObject Members

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (TrainingEndDate < TrainingStartDate)
        //    {
        //        yield return new ValidationResult("Training End Date should not be less than Training Start Date.");
        //    }
        //}

        #endregion
    }

    public class MyTrainingView
    {
        public MyTrainingModel MyTrainingModel { get; set; }
        public IEnumerable<MyTrainingModel> MyTrainings { get; set; }
    }
}
