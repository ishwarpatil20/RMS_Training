using Domain.Entities;
using RMS.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMS.Controllers
{
    public class EmployeeController : ErrorController
    {
        private List<EmployeeModel> viewEmployeeModel = new List<EmployeeModel>();
        private readonly IEmployeeService _service;
        private int result;
        private string message = string.Empty;

        
        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        public ActionResult ViewEmployeeTillBilled()
        {
            string Flag = "V";
            EmployeeTillBilledViewModel ObjEmpTillBilled = new EmployeeTillBilledViewModel();
            ObjEmpTillBilled = ListEmployeeBillDate(ObjEmpTillBilled, 0, Flag);
            return View(Common.Constants.CommonConstants.ViewEmployeeTillBilledView, ObjEmpTillBilled);
        }

        EmployeeTillBilledViewModel  ListEmployeeBillDate(EmployeeTillBilledViewModel ObjEmpTillBilled, int EPAId,string Flag)
        {
            EmployeeProjectAllocationModel objEPA = new EmployeeProjectAllocationModel () ;
            List<EmployeeProjectAllocationModel> lstObjEPA= new List<EmployeeProjectAllocationModel>();
            objEPA.Flag = Flag;
            objEPA.EPAId = EPAId;
            if (objEPA.Flag == "U")
            {
                objEPA.BillingTillDate = ObjEmpTillBilled.BillingTillDate;
                TempData["Message"] = "Billing updated successfully";
            }
            else if (objEPA.Flag == "D")
            {
                objEPA.BillingTillDate = DateTime.Today;
                TempData["Message"] = "Billing deleted successfully";
            }
            else
                objEPA.BillingTillDate = DateTime.Today;

            lstObjEPA = _service.Employee_BillingTillDate(objEPA).ToList();
            if (lstObjEPA.Count == 1 && EPAId !=0)
            {
                ObjEmpTillBilled.EmpId = lstObjEPA[0].EmpId;
                ObjEmpTillBilled.EmployeeName = lstObjEPA[0].EmployeeName;
                ObjEmpTillBilled.ProjectName= lstObjEPA[0].Projects.ProjectName ;
                ObjEmpTillBilled.StartDate = lstObjEPA[0].StartDate;
                ObjEmpTillBilled.ActualRelDate = lstObjEPA[0].ActualRelDate;
                ObjEmpTillBilled.BillingTillDate = lstObjEPA[0].BillingTillDate;
            }

            ObjEmpTillBilled.ListEmployeeBilled = new List<EmployeeProjectAllocationModel>();
            ObjEmpTillBilled.ListEmployeeBilled = lstObjEPA;
          
            return ObjEmpTillBilled;
        }

        public ActionResult EditEmployeeTillBilled(string PageMode, string EPAId)
        {
            string _EPAId = RMS.Helpers.CheckAccessAttribute.Decode(EPAId);

            EmployeeTillBilledViewModel ObjEdit = new EmployeeTillBilledViewModel();
            ObjEdit = ListEmployeeBillDate(ObjEdit, Convert.ToInt32(_EPAId), "V");
            
            EmployeeTillBilledViewModel ObjList = new EmployeeTillBilledViewModel(); 
            ObjList = ListEmployeeBillDate(ObjList, 0,"V");
            ObjEdit.ListEmployeeBilled = ObjList.ListEmployeeBilled;
            ObjEdit.EPAIdEncrp = EPAId;
            
            return View(Common.Constants.CommonConstants.ViewEmployeeTillBilledView, ObjEdit);
            //return View(ObjEmpTillBilled);
        }

        public ActionResult UpdateEmployeeTillBilled(string PageMode, string EPAId, string Billingdate)
        {
            string _EPAId = RMS.Helpers.CheckAccessAttribute.Decode(EPAId);
            EmployeeTillBilledViewModel ObjUpdate = new EmployeeTillBilledViewModel();
            ObjUpdate.BillingTillDate = Convert.ToDateTime(Billingdate);
            ObjUpdate.EPAId = Convert.ToInt32(_EPAId);
            ObjUpdate = ListEmployeeBillDate(ObjUpdate, Convert.ToInt32(_EPAId),"U");

            //ViewEmployeeTillBilled();
            return RedirectToAction( "ViewEmployeeTillBilled");
            //EmployeeTillBilledViewModel ObjList = new EmployeeTillBilledViewModel();
            //ObjList = ListEmployeeBillDate(ObjList, 0);
            //ObjEdit.ListEmployeeBilled = ObjList.ListEmployeeBilled;

            //return View(Common.Constants.CommonConstants.ViewEmployeeTillBilledView, ObjEdit);

        }
        public ActionResult DeleteEmployeeTillBilled(string PageMode, string EPAId)
        {
            string _EPAId = RMS.Helpers.CheckAccessAttribute.Decode(EPAId);

            EmployeeTillBilledViewModel ObjEdit = new EmployeeTillBilledViewModel();
            ObjEdit.EPAId = Convert.ToInt32(_EPAId);
            ObjEdit = ListEmployeeBillDate(ObjEdit, Convert.ToInt32(_EPAId), "D");

            //ViewEmployeeTillBilled();
            return RedirectToAction("ViewEmployeeTillBilled");

            //return View(ObjEmpTillBilled);
        }

    }
}
