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

namespace Services.Interfaces
{
    public interface ICommonService
    {
        #region  Method

        string getLoggedInUserEmailId();

        int GetEmployeeID();

        string AccessForTrainingModule(int UserEmpId);

        List<Menu> GetAuthoriseMenuList(int Empid);

        ArrayList GetEmployeeRole(int UserEmpId);

        ArrayList GetReportingManagerEmailIds(int empId);

        #endregion  Method

    }
}
