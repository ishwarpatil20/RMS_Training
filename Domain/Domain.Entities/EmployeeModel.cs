using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class EmployeeModel
    {
        public int EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName{ get; set; }
        public string EmployeeName { get; set; }
        public string Designation  { get; set; }
        public string PrimarySkill{ get; set; }
        public string EmailID { get; set; }
    }

    public class EmployeeProjectAllocationModel : EmployeeModel
    {
        public int EPAId { get; set; }
        public string EPAIdEncr { get; set; }

        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ActualRelDate { get; set; }
        public DateTime BillingTillDate { get; set; }
        public string Flag { get; set; }
        public ProjectResult Projects { set; get; }
    }
  
}
