using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Domain.Entities;
using RMS.Common.BusinessEntities;
using System.Web.Mvc;
using RMS.Common.BusinessEntities.Menu;
using System.Collections;
namespace Infrastructure.Interfaces
{
    public interface ICommonRepository
    {
        #region  Method

        string GetWindowsUsernameAsPerNorthgate(string windowsUsername, out string domainName);

        int GetEmployeeID(string EmailId);

        string AccessForTrainingModule(int UserEmpId);

        List<Menu> GetAuthoriseMenuList(int Empid);

        ArrayList GetEmployeeRole(int UserEmpId);
        SelectList GetDefaultSelectList(string defaultValue);

        ArrayList GetReportingManagerEmailIds(int empId);

        #endregion  Method
    }
}
