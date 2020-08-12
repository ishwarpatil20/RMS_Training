using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
   public class OtherMasterResult
    {
       public int GroupMasterID { get; set; }
       public int MasterID { get; set; }
       public int OrderBy { get; set; }
       public string Name { get; set; }
       public string IsCommonCostCode { get; set; }
       public string Category { get; set; }

       public Nullable<bool> IsActive { get; set; }

       public Nullable<int> CreatedByID { get; set; }

       public bool IsMapped { get; set; }

       public string IsCommon_CostCode { get; set; }

       

       //public Nullable<DateTime> CreatedDate { get; set; }

       //public Nullable<int> LastModifiedByID { get; set; }

       //public Nullable<DateTime> LastModifiedDate { get; set; }
    }
}
