using System;
namespace Infrastructure.Interfaces
{
 public  interface IBudgetRepository
    {
     int Delete_Budget(Domain.Entities.BudgetResult objBudget);
     System.Collections.Generic.List<Domain.Entities.BudgetResult> GetBudget(Domain.Entities.BudgetResult objBudget);
        System.Collections.Generic.List<Domain.Entities.ProjectResult> GetProjects();
        int Insert_Update_Budget(Domain.Entities.BudgetResult objBudget);
    }
}
