using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RMS.Models
{
    public class MasterViewModel
    {
        public MasterViewModel() { }

        [DataType(DataType.Text)]
        [Display(Name = "Role Name")]
        public SelectList Roles { get; set; }

        [Required(ErrorMessage = "Role Name Required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please Select Role.")]        
        public int SelectedRole { get; set;}

        [Required(ErrorMessage = "Role Name Required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please Select Role.")]  
        public string SelectedEmployeeID { get; set; }

        public string SelectedEmployeeName { get; set; }

        

        //public MasterViewModel(ITrainingService service)
        //{            
        //    Roles = new SelectList(service.GetTrainingNameforAllCourses(), "Key", "Value");
        //}

    }
}