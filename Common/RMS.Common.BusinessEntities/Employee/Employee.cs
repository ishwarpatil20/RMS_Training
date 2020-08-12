using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Common.BusinessEntities
{
    public class Employee
    {
        public int EmployeeID { get; set; }

        public string EmployeeCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int StatusID { get; set; }


        //Added by jagmohan for submit nomination detail
        public int NominationID { get; set; }

        public string EmployeeName { get; set; }

        public string EmailID { get; set; }

        public string Designation { get; set; }

        public string Project { get; set; }

        public string Priority { get; set; }

        public int PriorityCode { get; set; }

        public string PreTraining { get; set; }

        public int PreTrainingCode { get; set; }

        public string Comment { get; set; }

        public int courseID { get; set; }
        
        public string  courseName { get; set; }

        public int TrainingNameID { get; set; }

        public string SubmitStatus { get; set; }

        public int SubmitStatusCode { get; set; }

        public string TrainingName { get; set; }

        public string NominatorName { get; set; }

        public string NominatorEmailID { get; set; }

        public string NomineeEmailID { get; set; }

        public string ObjectiveForSoftSkill { get; set; }

        public int TrainingTypeID { get; set; }

        public bool IsRMOLogin{ get; set; }

        public int RMONominatorID { get; set; }

        public int NominationTypeID { get; set; }

        public bool IsConfirmNomination { get; set; }

        public bool Confirmed { get; set; }

        public int NominationEmpID { get; set; }

        public string EffectivenessID { get; set; }

        public int NominatorEmpID { get; set; }
        //Added by jagmohan for submit nomination detail
    }
}
