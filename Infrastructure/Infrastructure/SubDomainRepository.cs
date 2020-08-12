using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class SubDomainRepository : BaseRepository<T_SubDomain>, ISubDomainRepository<T_SubDomain>
    {
        public SubDomainRepository(Infrastructure.Interfaces.IUnitOfWork unit)
            : base(unit)
        {
            
        }


    }
}
