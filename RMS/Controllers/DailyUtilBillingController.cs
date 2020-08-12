using RMS.Helpers;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using RMS.Models;

namespace RMS.Controllers
{
    public class DailyUtilBillingController : Controller
    {
        //
        // GET: /DailyUtilBilling/
  #region Initialization
        private readonly IDailyUtilBillingService _IDailyUtilBillingService;
        #endregion

        #region Constructor {othermaster}
        public DailyUtilBillingController(IDailyUtilBillingService utilBilling)
        {
            _IDailyUtilBillingService = utilBilling;
        }
        #endregion
        public ActionResult Index()
        {
            return View();
        }

    }
}
