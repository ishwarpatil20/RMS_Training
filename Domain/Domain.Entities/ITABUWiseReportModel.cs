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
    //Rakesh : Issue 59585: 08/May/2017 : Starts
    public class ITABUWiseReportModel
    {
        public SelectList ListQuarters;
        public SelectList ListYears;

        [Required(ErrorMessage="Please Select Year")]
        public string YearId { get; set; }

        [Required(ErrorMessage = "Please Select Quarter")]
        public string Quarter { get; set; }

        public string Message { get; set; }

        SelectList GetQuarters()
        {
            List<SelectListItem> QuartersList = new List<SelectListItem>();
            QuartersList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "" });            
            QuartersList.Add(new SelectListItem { Text = "April - June", Value = "1" });
            QuartersList.Add(new SelectListItem { Text = "July - September", Value = "2" });
            QuartersList.Add(new SelectListItem { Text = "October - December", Value = "3" });
            QuartersList.Add(new SelectListItem { Text = "January - March", Value = "4" });
            return new SelectList(QuartersList, "Value", "Text");
        }


        SelectList GetYears()
        {
            List<SelectListItem> YearsList = new List<SelectListItem>();
            YearsList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "" });
            YearsList.Add(new SelectListItem { Text = (DateTime.Now.Year - 1).ToString() + '-' + DateTime.Now.Year.ToString(), Value = (DateTime.Now.Year - 1).ToString() });
            YearsList.Add(new SelectListItem { Text = (DateTime.Now.Year).ToString() + '-' + (DateTime.Now.Year + 1).ToString(), Value = (DateTime.Now.Year).ToString() });
            return new SelectList(YearsList, "Value", "Text");
        }


        public ITABUWiseReportModel()
        {
            ListYears = GetYears();
            ListQuarters = GetQuarters();

        }

    }

    //Rakesh : Issue 59585: 08/May/2017   : End
}

