using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities ;
using RMS.Common.DataBase;
using System.Data.SqlClient;
using RMS.Common.Constants;
using Infrastructure.Interfaces; 
namespace Infrastructure
{
   public class BudgetRepository : IBudgetRepository
    {
       public List<ProjectResult> GetProjects()
       {
           DataAccessClass objData = new DataAccessClass();
           SqlCommand cmd = new SqlCommand();
           List<ProjectResult> lstProjectResult =  objData.ExecuteReaderSP_WithConnection<ProjectResult>(SPNames.Get_Projects, cmd);
           lstProjectResult.Insert(lstProjectResult.Count, new ProjectResult() { ProjectID = -9999, ProjectName = "Common Cost Code" });
           return lstProjectResult;
       }

       public int Insert_Update_Budget(BudgetResult objBudget)
       {
           DataAccessClass objData = new DataAccessClass();
           SqlCommand cmd = new SqlCommand();
           cmd.Parameters.AddWithValue(SPParameter.Budget_Year,objBudget.Year);
           cmd.Parameters.AddWithValue(SPParameter.Budget_Month, objBudget.Month);
           cmd.Parameters.AddWithValue(SPParameter.Budget_ProjectId, objBudget.ProjectId);
           cmd.Parameters.AddWithValue(SPParameter.Budget_CostCodeId, objBudget.CostCodeId);
           cmd.Parameters.AddWithValue(SPParameter.Budget_Budget, objBudget.Budget);
           cmd.Parameters.AddWithValue(SPParameter.Budget_BusinessVerticalId, objBudget.BusinessVerticalId);


           if (objBudget.BudgetId != 0)
           {               

               if (objBudget.Budget == 0)
               {
                   cmd = new SqlCommand();
                   cmd.Parameters.AddWithValue(SPParameter.LastModifiedById, objBudget.CreatedById);
                   cmd.Parameters.AddWithValue(SPParameter.BudgetId, objBudget.BudgetId);
                   return Convert.ToInt32(objData.ExecuteScalarSP_WithConnection(SPNames.Delete_Budget, cmd));
               }
               else
               {
                   cmd.Parameters.AddWithValue(SPParameter.LastModifiedById, objBudget.CreatedById);
                   cmd.Parameters.AddWithValue(SPParameter.BudgetId, objBudget.BudgetId);
                   return Convert.ToInt32(objData.ExecuteScalarSP_WithConnection(SPNames.Update_Budget, cmd));
               }
           }
           else
           {
               cmd.Parameters.AddWithValue(SPParameter.CreatedBy, objBudget.CreatedById);
               return Convert.ToInt32(objData.ExecuteScalarSP_WithConnection(SPNames.Insert_Budget, cmd));
           }
       }

       public int Delete_Budget(BudgetResult objBudget)
       {
           DataAccessClass objData = new DataAccessClass();
           SqlCommand cmd = new SqlCommand();
           cmd.Parameters.AddWithValue(SPParameter.BudgetId, objBudget.BudgetId);
           cmd.Parameters.AddWithValue(SPParameter.LastModifiedById, objBudget.CreatedById);
           return Convert.ToInt32(objData.ExecuteScalarSP_WithConnection(SPNames.Delete_Budget, cmd));
       }

       public List<BudgetResult> GetBudget(BudgetResult objBudget)
       {
           DataAccessClass objData = new DataAccessClass();
           SqlCommand cmd = new SqlCommand();
           cmd.Parameters.AddWithValue(SPParameter.Budget_Year, objBudget.Year);
           cmd.Parameters.AddWithValue(SPParameter.Budget_Month, objBudget.Month);
           cmd.Parameters.AddWithValue(SPParameter.Budget_ProjectId, objBudget.ProjectId);
           return objData.ExecuteReaderSP_WithConnection<BudgetResult>(SPNames.Get_Budget, cmd);
       }
    }
}
