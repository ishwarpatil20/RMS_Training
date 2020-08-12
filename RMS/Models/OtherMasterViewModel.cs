using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.Interfaces;
using Domain.Entities;
namespace RMS.Models
{
    public class OtherMasterViewModel
    {
        public SelectList GroupMasters { get; set; }
        public int IsReset { get; set; }
        public int MasterId { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Master Name")]        
        [Required(ErrorMessage = "Master Name Required.")]       
        public string Name { get; set; }


        public string Category { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please Select Group Master.")]
        public int GroupMasterId { get; set; }
        public List<OtherMasterResult> lstOtherMasters { get; set; }

        public string Message { get; set; }

        public int IsRepeat { get; set; }

        public int IsEdit { get; set; }


        public bool IsCommonCostCode { get; set; }

        
    }
}