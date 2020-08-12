using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RMS.Models;
using Services.Interfaces;
using RMS.Helpers;

namespace RMS.Controllers
{
    [CheckAccess]
    public class MasterController : ErrorController
    {
        private readonly IMasterService _Service;
       
        public MasterController(IMasterService service)
        {
            _Service = service;
        }        

        [HttpGet]
        public ActionResult GetMasterRoles()
        {
            MasterViewModel roleViewModel = new MasterViewModel();
            roleViewModel.Roles = new SelectList(_Service.GetRMSRoles(), "Key", "Value");
            return PartialView("RoleManagement", roleViewModel);
        }

        [HttpPost]
        public ActionResult GetMasterRoles(MasterViewModel masterViewModel)
        {
            return PartialView("RoleManagement", masterViewModel);
        }

    }
}
