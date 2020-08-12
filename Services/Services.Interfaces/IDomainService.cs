using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
namespace Services.Interfaces
{
    public interface IDomainService<T>  where T : class 
    {
        int Insert(T obj);
        int Update(T obj);
        int Delete(T obj);
    }
}
