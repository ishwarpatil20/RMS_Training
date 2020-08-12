using Domain.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DomainRepository : BaseRepository<T_Domain>, IDomainRepository<T_Domain>
    {

        public DomainRepository(Infrastructure.Interfaces.IUnitOfWork unit)
            : base(unit)
        {
            
        }

        public List<Domain_SubDomainModel> GetDomain(int DomainId, int SubDomainId)
        {
            using (RMS_Entities objRMS_Entities = new RMS_Entities())
            {
                return objRMS_Entities.USP_GetDomain_SubDomain(DomainId, SubDomainId).Cast<Domain_SubDomainModel>().ToList();
            }
        }
    }
}
