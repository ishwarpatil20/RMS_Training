using System;
namespace Services.Interfaces
{
   public interface IBudgetService
    {
       int Delete_Budget(Domain.Entities.BudgetResult objMaster);
       System.Collections.Generic.List<Domain.Entities.BudgetResult> Get_Budget(Domain.Entities.BudgetResult objMaster);
        System.Collections.Generic.List<Domain.Entities.ProjectResult> GetProjects();
        int Insert_Budget(Domain.Entities.BudgetResult objMaster);
       
    }
}
