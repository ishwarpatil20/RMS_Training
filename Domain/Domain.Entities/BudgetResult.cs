using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
  public class BudgetResult
    {
        public int BudgetId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int MasterID { get; set; }
        public decimal Budget { get; set; }
        public string Name { get; set; }
      //  public SelectList BusinessVerticals { get; set; }
        public string BusinessVerticalId { get; set; }
        public string BusinessVerticalName { get; set; }
        public int CostCodeId { get; set; }
        public int CreatedById { get; set; }
    }
}
