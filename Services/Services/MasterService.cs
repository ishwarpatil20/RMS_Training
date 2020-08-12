using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure;
using RMS.Common.BusinessEntities;
using RMS.Common.Constants;
using Services.Interfaces;
using System.Web.Mvc;
using System.Configuration;
using System.Web;
using RMS.Common;
using RMS.Common.BusinessEntities.Menu;

namespace Services
{
    public class MasterService : IMasterService
    {
        private readonly IMasterRepository<T_Master> _repository;

        public MasterService(IMasterRepository<T_Master> repository)
        {
            _repository = repository;
        }


        public List<KeyValuePair<int, string>> GetRMSRoles()
        {
            return _repository.GetRMSRoles();
        }
    }
}
