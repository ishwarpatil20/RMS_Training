using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Infrastructure
{
    public class IPanelDesignationRepository : BaseRepository<T_IP_PanelDesignation>, IIPanelDesignationRepository<T_IP_PanelDesignation>
    {
        public IPanelDesignationRepository (IUnitOfWork unit)
            : base(unit)
        {

        }
    }
}
