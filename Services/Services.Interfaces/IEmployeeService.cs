using RMS.Common.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Domain.Entities;

namespace Services.Interfaces
{
    public interface IEmployeeService
    {
        SelectList GetSkillTypesCategory();
        EmployeeModel GetEmployeeDetailByID(int empid);
        IEnumerable<SelectListItem> FillDesignationList(int DeptId);
        List<EmployeeProjectAllocationModel> Employee_BillingTillDate(EmployeeProjectAllocationModel ObjEmp);
    }
}
