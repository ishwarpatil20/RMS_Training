using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Domain.Entities;
namespace Infrastructure.Interfaces
{
    public interface IDomainRepository<T> : IBaseRepository<T> where T :class 
    {
        List<Domain_SubDomainModel> GetDomain(int DomainId, int SubDomainId);
    }
}
