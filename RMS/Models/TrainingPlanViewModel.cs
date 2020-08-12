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
using Infrastructure;
using System.Web.UI.WebControls;



namespace RMS.Models
{
    public class TrainingPlanViewModel
    {
        public int TrainingPlanId{ get; set; }
        public int Year { get; set; }
        public int Quarter{ get; set; }
        public int TrainingName { get; set; }   
        public int TrainingTypeId { get; set; }
        public int TrainingType { get; set; }
        public int Month { get; set; }
        public string  Hrs { get; set; }
        public string Target { get; set; }
        public string Mode { get; set; }

        public SelectList ListYear { get; set; }
        public SelectList ListQuarter { get; set; }
        public SelectList ListTrainingType { get; set; }
        public SelectList ListTrainingName { get; set; }
        public SelectList ListMonth { get; set; }

        public int TrainingId { get; set; }
        public string TrainingHour { get; set; }
        public string TrainingNameOther { get; set; }
        public string Operation { get; set; }
        public int CreatedById { get; set; }

        public bool FlagEdit { get; set; }
        public bool FlagEmployeePage { get; set; }
        
        //public string TrainingTypeName { get; set; }
        //public string MonthName { get; set; }
        //public string QuarterName { get; set; }
        
      public List<TrainingPlanModel> ListTrainingPan {get; set;}
        

        public TrainingPlanViewModel()
        {
            ListQuarter = GetQuarter();
            ListYear = GetYear();            
            ListTrainingName = CommonRepository.FillMasterDropDownList(CommonConstants.TrainingName);
            ListTrainingType = CommonRepository.FillMasterDropDownList(CommonConstants.TrainingMode);
            //ListTrainingType = GetTrainingType();
            ListMonth = GetMonth(0);      
        }

        SelectList GetYear()
        {
            List<SelectListItem> newList = new List<SelectListItem>();
            newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "" });
            for(int i=2015; i<=DateTime.Now.Year;i++)
            {
                int _year = i + 1;
                newList.Add(new SelectListItem { Text = i.ToString() + '-' + _year.ToString(), Value = i.ToString() });
            }
            return new SelectList(newList, "Value", "Text");
        }

        SelectList GetQuarter()
        {
            List<SelectListItem> newList = new List<SelectListItem>();
            newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "" });
            newList.Add(new SelectListItem { Text = "Apr-Jun", Value = "1" });
            newList.Add(new SelectListItem { Text = "Jul-Sep", Value = "2" });
            newList.Add(new SelectListItem { Text = "Oct-Dec", Value = "3" });
            newList.Add(new SelectListItem { Text = "Jan-Mar", Value = "4" });
            return new SelectList(newList, "Value", "Text");
        }


        SelectList GetMonth(int Quarter)
        {
            List<SelectListItem> newList = new List<SelectListItem>();
            newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "" });
            
            // Set current Quarter
            if (Quarter == 0)
                Quarter = checkcurrentQuarter();

            if (Quarter == 1)
            {
                newList.Add(new SelectListItem { Text = "Apr", Value = "4" });
                newList.Add(new SelectListItem { Text = "May", Value = "5" });
                newList.Add(new SelectListItem { Text = "Jun", Value = "6" });
            }
            else if (Quarter == 2)
            {
                newList.Add(new SelectListItem { Text = "Jul", Value = "7" });
                newList.Add(new SelectListItem { Text = "Aug", Value = "8" });
                newList.Add(new SelectListItem { Text = "Sep", Value = "9" });
            }
            else if (Quarter == 3)
            {
                newList.Add(new SelectListItem { Text = "Oct", Value = "10" });
                newList.Add(new SelectListItem { Text = "Nov", Value = "11" });
                newList.Add(new SelectListItem { Text = "Dec", Value = "12" });
            }
            else if (Quarter == 4)
            {
                newList.Add(new SelectListItem { Text = "Jan", Value = "1" });
                newList.Add(new SelectListItem { Text = "Feb", Value = "2" });
                newList.Add(new SelectListItem { Text = "Mar", Value = "3" });
            }            
            return new SelectList(newList, "Value", "Text");
        }

        int checkcurrentQuarter()
        {
            int CurrentMonth = DateTime.Today.Month;
            switch (CurrentMonth)
            {
                case 1:
                case 2:
                case 3:
                    return 4;
                case 4:
                case 5:
                case 6:
                    return 1;
                case 7:
                case 8:
                case 9:
                    return 2;
                case 10:
                case 11:
                case 12:
                    return 3;
                default:
                    return 0;
            }
        }

        SelectList GetTrainingType()
        {
            List<SelectListItem> newList = new List<SelectListItem>();
            newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "" });
            newList.Add(new SelectListItem { Text = "Internal", Value = "1909" });
            newList.Add(new SelectListItem { Text = "External", Value = "1910" });
            return new SelectList(newList, "Value", "Text");
        }

    }
}