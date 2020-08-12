using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.Interfaces;
using RMS.Helpers;
using Infrastructure;
using RMS.Models;
using RMS.Common.Constants;
using Domain.Entities;
namespace RMS.Controllers
{
    [CheckAccess]
    public class BudgetController : ErrorController
    {


        #region Initialization
        private readonly IBudgetService _IBudgetService;
        #endregion

        #region Constructor {Budget}
        public BudgetController(IBudgetService Budget)
        {
            _IBudgetService = Budget;
        }
        #endregion

        #region Methods
        SelectList GetYear()
        {
            List<SelectListItem> newList = new List<SelectListItem>();
            newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "" });
            for (int i = 2015; i <= DateTime.Now.Year; i++)
            {
                int _year = i + 1;
                newList.Add(new SelectListItem { Text = i.ToString() + '-' + _year.ToString(), Value = i.ToString() });
            }
            return new SelectList(newList, "Value", "Text");
        }

        #endregion

        #region Actions  {Index,RenderCostCodeLists,SaveBudget}

        public ActionResult Index()
        {
            BudgetViewModel objBudget = new BudgetViewModel();

            int CurrentMonth = DateTime.Today.Month;
            int CurrentYear = DateTime.Today.Year;

            // Set Current Year
            if (CurrentMonth == 1 || CurrentMonth == 2 || CurrentMonth == 3)
                objBudget.YearId = CurrentYear - 1;
            else
                objBudget.YearId = CurrentYear;



            objBudget.MonthId = DateTime.Today.ToString("MMMM");

            objBudget.Months = CommonRepository.FillMonthsDropDownList(CommonConstants.MonthsName);
         //   objBudget.BusinessVerticals = CommonRepository.FillMonthsDropDownList(CommonConstants.BusinessVerticals);
            objBudget.Projects = new SelectList(_IBudgetService.GetProjects(), "ProjectID", "ProjectName", "select"); 
            objBudget.Years = GetYear();          
            return View(objBudget);
        }

        [HttpPost]
        public PartialViewResult RenderCostCodeLists(BudgetResult objBudgetModel)
        {   
            List<BudgetResult> lstBudgetResult =  _IBudgetService.Get_Budget(objBudgetModel) ;

            var lstBudget = lstBudgetResult.Where(p => p.BudgetId != 0).ToList();

            foreach (var item in lstBudgetResult)
            {
                if (!lstBudget.Where(p => p.MasterID == item.MasterID).Any())
                    lstBudget.Add(item);
            }

            
            if(objBudgetModel.ProjectId==-9999)
            ViewData["BusinessVerticals"] = CommonRepository.FillMasterDropDownList(CommonConstants.BusinessVerticals);

            return PartialView(RMS.Common.Constants.CommonConstants.PartialListCostCodes, lstBudget);
        }

        public JsonResult SaveBudget(List<BudgetResult> arrCostCode)
        {

            foreach (var item in arrCostCode)
            {
                item.CreatedById = Convert.ToInt32(Session["EmpID"]); 
                _IBudgetService.Insert_Budget(item);    
            }           

            return Json("Saved", JsonRequestBehavior.AllowGet);
        }

        //Neelam : 20/03/2017 Starts
        //Issue : 57842 : <taskname.>
        //Neelam : 21/03/2017 Ends
        public JsonResult DeleteBudget(List<BudgetResult> arrBudgetResult)
        {

            foreach (var item in arrBudgetResult)
            {
              
                _IBudgetService.Delete_Budget(item);
            }

            return Json("Delete", JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
