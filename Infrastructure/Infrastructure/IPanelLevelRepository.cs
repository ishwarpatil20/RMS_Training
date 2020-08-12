using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Interfaces;

namespace Infrastructure
{
    public class IPanelLevelRepository : BaseRepository<T_IP_PanelLevel>, IIPanelLevelRepository<T_IP_PanelLevel>
    {
        public IPanelLevelRepository(Infrastructure.Interfaces.IUnitOfWork unit)
            : base(unit)
        {

        }
    }
}
