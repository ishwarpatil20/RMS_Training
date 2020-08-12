using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using Infrastructure.Interfaces;
namespace Infrastructure
{
    public class Daily_UtilBillingRepository  : IDay_UtilBillingRepository
    {
    public  void Insert_DailyUtilBilling(int StartDay, int StartMonth, int StartYear, int EndDay, int EndMonth, int EndYear)
      {
          using (var objEntities = new RMS_Entities())
          {
              objEntities.USP_Report_AvgUtilBillingforGivenPeriod_BB(StartDay, StartMonth, StartYear, EndDay, EndMonth, EndYear);
          }
      }
    }
}
