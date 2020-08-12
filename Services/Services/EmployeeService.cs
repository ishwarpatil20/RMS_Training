using Domain.Entities;
using Infrastructure.Interfaces;
using RMS.Common.BusinessEntities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _IEmployeeRepository;

        public EmployeeService(IEmployeeRepository Iemployeerepository)
        {
            _IEmployeeRepository = Iemployeerepository;
        }

        public SelectList GetSkillTypesCategory()
        {
            return _IEmployeeRepository.GetSkillTypesCategory();
        }
        public EmployeeModel GetEmployeeDetailByID(int empid)
        {
            return _IEmployeeRepository.GetEmployeeDetailByID(empid);
        }
        public IEnumerable<SelectListItem> FillDesignationList(int DeptId)
        {
            return _IEmployeeRepository.FillDesignationList(DeptId);
        }

        public List<EmployeeProjectAllocationModel> Employee_BillingTillDate(EmployeeProjectAllocationModel ObjEmp)
        {
            return _IEmployeeRepository.Employee_BillingTillDate(ObjEmp);
        }
    }
}
