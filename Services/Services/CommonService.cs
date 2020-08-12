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
using System.Collections;

namespace Services
{
    public class CommonService : ICommonService
    {
        private readonly ICommonRepository _Commrepository;

        public CommonService()
        {
            _Commrepository = new CommonRepository();
        }

        //public CommonService(ICommonRepository Commrepository)
        //{
        //    _Commrepository = Commrepository;
        //}        

        public string getLoggedInUserEmailId()
        {

            string strUserIdentity = HttpContext.Current.User.Identity.Name;

            try
            {
                int position = strUserIdentity.IndexOf("\\");
                strUserIdentity = strUserIdentity.Remove(0, position + 1);
                strUserIdentity = strUserIdentity.Replace(AuthorizationManagerConstants.RAVEDOMAIN + @"\", "");

                string domainName = string.Empty;
                strUserIdentity = _Commrepository.GetWindowsUsernameAsPerNorthgate(strUserIdentity, out domainName);
                strUserIdentity = strUserIdentity + "@" + domainName;
            }
            catch (Exception e)
            {
                throw e;
            }

            return strUserIdentity;
        }

        public int GetEmployeeID()
        {
            return _Commrepository.GetEmployeeID(getLoggedInUserEmailId());
        }

        public string AccessForTrainingModule(int UserEmpId)
        {
            return _Commrepository.AccessForTrainingModule(UserEmpId);
        }

        public List<Menu> GetAuthoriseMenuList(int Empid)
        {
            return _Commrepository.GetAuthoriseMenuList( Empid);
        }

        public ArrayList GetEmployeeRole(int UserEmpId)
        {
            return _Commrepository.GetEmployeeRole(UserEmpId);
        }

        public ArrayList GetReportingManagerEmailIds(int empId)
        {
            return _Commrepository.GetReportingManagerEmailIds(empId);
        }

    }



    
}
