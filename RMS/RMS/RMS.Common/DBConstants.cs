using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace RMS.Common
{
    public class DBConstants
    {
        //--Rave HR ConnectionString
        
        public static string GetDBConnectionString()
        {
            string strRaveHRConnectionString = ConfigurationManager.ConnectionStrings["RaveHRConnectionString"].ConnectionString;
           // string strRaveHRConnectionString =  ConfigurationSettings.AppSettings["RaveHRConnectionString"].ToString();
            strRaveHRConnectionString = Utility.DecryptPassword(strRaveHRConnectionString);
            return strRaveHRConnectionString;
        }
    }
}
