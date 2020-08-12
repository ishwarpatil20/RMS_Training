using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Domain.Entities;
using Infrastructure.Interfaces;
using System.Data.Sql;
using System.Data.SqlClient;
using RMS.Common;
using RMS.Common.ExceptionHandling;
using RMS.Common.Constants;
using RMS.Common.DataBase;
using RMS.Common.BusinessEntities;
using System.Web.Mvc;
using System.Web;
using RMS.Common.BusinessEntities.Common;
using RMS.Common.BusinessEntities.Menu;
using System.Configuration;
namespace Infrastructure
{
    public class MasterRepository : BaseRepository<T_Master> ,IMasterRepository<T_Master> 
    {

        public MasterRepository(Infrastructure.Interfaces.IUnitOfWork unit)
            : base(unit)
        {
            
        }

        public List<KeyValuePair<int, string>> GetRMSRoles()
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            List<KeyValuePair<int, string>> RMSRoles = new List<KeyValuePair<int, string>>();
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                //SqlParameter[] sqlParam = new SqlParameter[1];
                //sqlParam[0] = new SqlParameter(SPParameter.RaiseID, SqlDbType.Int);
                //sqlParam[0].Value = roleid;

                SqlDataReader dr = objGetTraining.ExecuteReaderSP(SPNames.USP_TNI_GetRMSRoles);
                while (dr.Read())
                {
                    RMSRoles.Add(new KeyValuePair<int, string>(Convert.ToInt32(dr[DbTableColumn.RoleId]), Convert.ToString(dr[DbTableColumn.RoleName])));
                }
                return RMSRoles;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.CommonLayer, "GetRMSRoles", "GetRMSRoles", EventIDConstants.AUTHORIZATION_MANAGER_ERROR);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }
        }
                      
    }
}
