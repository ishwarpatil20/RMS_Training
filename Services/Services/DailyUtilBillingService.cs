using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Interfaces;
namespace Services
{
   public class DailyUtilBillingService : IDailyUtilBillingService
    {
    public void Insert_DailyUtilBilling(int StartDay, int StartMonth, int StartYear, int EndDay, int EndMonth, int EndYear)
       {
           Daily_UtilBillingRepository obj = new Daily_UtilBillingRepository();
           obj.Insert_DailyUtilBilling(StartDay, StartMonth,StartYear,  EndDay, EndMonth, EndYear);
       }
    }
}
