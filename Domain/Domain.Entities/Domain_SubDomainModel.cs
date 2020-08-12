using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
   public class Domain_SubDomainModel
    {
        public int DomainId { get; set; }
        public string Domain { get; set; }
        public Nullable<int> SubDomainId { get; set; }
        public string SubDomainName { get; set; }
    }
}
