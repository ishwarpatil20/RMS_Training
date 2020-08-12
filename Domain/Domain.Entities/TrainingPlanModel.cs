using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using RMS.Common;

namespace Domain.Entities
{
    public class TrainingPlanModel
    {
        public int TrainingPlanId { get; set; }
        public int Year { get; set; }
        public int Quarter { get; set; }
        public int TrainingTypeId { get; set; }
        public int TrainingId { get; set; }
        public int Month { get; set; }
        public string TrainingHour { get; set; }
        public string Target { get; set; }
        public int CreatedById { get; set; }
        public string Mode { get; set; }

        public string TrainingNameOther { get; set; }

        public string TrainingTypeName { get; set; }
        public string TrainingName { get; set; }
        public string MonthName { get; set; }
        public string QuarterName { get; set; }
        public string YearFinancial { get; set; }
    }

}