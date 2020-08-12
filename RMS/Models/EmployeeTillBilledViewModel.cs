using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;

namespace RMS.Models
{
    public class EmployeeTillBilledViewModel
    {
        public int EmpId { get; set; }
        public string EmployeeName { get; set; }
        public string ProjectName { get; set; }
        public int EPAId { get; set; }
        public string  EPAIdEncrp { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ActualRelDate { get; set; }

        public DateTime BillingTillDate { get; set; }
        public string Flag { get; set; }

        public List<EmployeeProjectAllocationModel> ListEmployeeBilled;

    }
}