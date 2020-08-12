using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Services.Interfaces;
using Domain.Entities;
namespace Services
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepository _IBudgetRepository;


        public BudgetService(IBudgetRepository repository)
        {
            _IBudgetRepository = repository;
        }

        public int Insert_Budget(BudgetResult objMaster)
        {
            return _IBudgetRepository.Insert_Update_Budget(objMaster);
        }

        public int Delete_Budget(BudgetResult objMaster)
        {
            return _IBudgetRepository.Delete_Budget(objMaster);
        }
        public List<BudgetResult> Get_Budget(BudgetResult objMaster)
        {
            return _IBudgetRepository.GetBudget(objMaster);
        }
        public List<ProjectResult> GetProjects()
        {
            return _IBudgetRepository.GetProjects();
        }

      

    }
}
