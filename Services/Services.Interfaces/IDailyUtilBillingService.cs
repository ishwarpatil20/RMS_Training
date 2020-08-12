using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
   public interface IDailyUtilBillingService
    {
       void Insert_DailyUtilBilling(int StartDay, int StartMonth, int StartYear, int EndDay, int EndMonth, int EndYear);
    }
}
