using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OtherMaster
    {
        public int MasterID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsCommonCostCode { get; set; }

        public Nullable<int> GroupMasterID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> CreatedByID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> LastModifiedByID { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        
    
    }
}
