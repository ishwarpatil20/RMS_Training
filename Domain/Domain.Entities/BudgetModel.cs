using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BudgetModel
    {
        public int BudgetId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int ProjectId { get; set; }
        public int CostCodeId { get; set; }
        public int BusinessVerticalId { get; set; }
        public int CreatedById { get; set; }
        public decimal Budget { get; set; }

    }
}
