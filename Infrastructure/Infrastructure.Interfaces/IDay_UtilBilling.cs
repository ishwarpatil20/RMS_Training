using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Domain.Entities;
using System.Web.Mvc;
namespace Infrastructure.Interfaces
{
    public interface IDay_UtilBillingRepository
    
    {
        void Insert_DailyUtilBilling(int StartDay, int StartMonth, int StartYear, int EndDay, int EndMonth, int EndYear);
    }
}
