using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
namespace RMS.Models
{
    public class BudgetViewModel
    {
        public int YearId { get; set; }
        public string MonthId { get; set; }
        public SelectList Years { get; set; }        
        public SelectList Months { get; set; }
        public SelectList Projects { get; set; }
        public List<BudgetResult> ListBudgets { get; set; }
    }
}