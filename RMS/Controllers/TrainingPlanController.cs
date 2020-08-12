using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Services.Interfaces;
using Infrastructure.Interfaces;
using RMS.Models;
using RMS.Common.Constants ;
using Domain.Entities;
using Infrastructure;
using RMS.Common;
using RMS.Helpers;
using System.Data;

namespace RMS.Controllers
{
    public class TrainingPlanController : ErrorController
    {
        private  readonly ITrainingService _ItrainingService = null;

        public TrainingPlanController(ITrainingService iTrainingService, ICommonService commonservice)
        {
            _ItrainingService = iTrainingService;
        }
                
        public ActionResult AddTrainingPlan()
        {
            TrainingPlanViewModel objVModel = new TrainingPlanViewModel();
            int CurrentMonth= DateTime.Today.Month;
            int CurrentYear= DateTime.Today.Year;

            // Set Current Year
            if (CurrentMonth ==1 || CurrentMonth ==2 ||CurrentMonth ==3)
                objVModel.Year = CurrentYear - 1;
            else
                objVModel.Year = CurrentYear;
            
            // Set Current Quarter
            switch (CurrentMonth)
            {
                case 1:
                case 2:
                case 3:
                    objVModel.Quarter = 4;
                    break;
                case 4:
                case 5:
                case 6:
                    objVModel.Quarter = 1;
                    break;
                case 7:
                case 8:
                case 9:
                    objVModel.Quarter = 2;
                    break;
                case 10:
                case 11:
                case 12:
                    objVModel.Quarter = 3;
                    break;
                default:
                    break;
            }
            objVModel.Month = CurrentMonth;
            objVModel.Mode = "I";
            objVModel.FlagEdit = true;

            TrainingPlanModel objSingle = new TrainingPlanModel();
            objSingle.Year = objVModel.Year;
            objSingle.Quarter = objVModel.Quarter;

            objVModel.ListTrainingPan = _ItrainingService.GetTrainingPlan(objSingle);

            return View(Common.Constants.CommonConstants.AddTrainingPlan, objVModel);
        }
                
        public PartialViewResult ShowPlan(int year, int Quarter,string Mode)
        {
            TrainingPlanViewModel obj = new TrainingPlanViewModel();
            obj.ListMonth = GetMonth(Quarter);
            obj.Mode = Mode;
            obj.Year= year;
            obj.Quarter = Quarter;

            //Check Editable or not as per Current Month
            int CurrentMonth = DateTime.Today.Month;
            int CurrentYear = DateTime.Today.Year;
            if (CurrentMonth ==1 || CurrentMonth ==2||CurrentMonth ==3)
                CurrentYear = DateTime.Today.Year-1;

            int CurrentQuarter=1;
            switch (CurrentMonth)
            {
                case 1:
                case 2:
                case 3:
                    CurrentQuarter = 4;
                    break;
                case 4:
                case 5:
                case 6:
                    CurrentQuarter = 1;
                    break;
                case 7:
                case 8:
                case 9:
                    CurrentQuarter = 2;
                    break;
                case 10:
                case 11:
                case 12:
                    CurrentQuarter = 3;
                    break;
                default:
                    break;
            }


            if (year == CurrentYear && (Quarter == CurrentQuarter))
                obj.FlagEdit = true;
            else
                obj.FlagEdit = false ;

            TrainingPlanModel objModel = new TrainingPlanModel();
            objModel.Mode = Mode;
            objModel.Year = year;
            objModel.Quarter = Quarter;
            obj.ListTrainingPan = _ItrainingService.GetTrainingPlan(objModel);
            return PartialView (CommonConstants._PartialViewAddPlan ,obj);
        }

        SelectList GetMonth(int Quarter)
        {
            List<SelectListItem> newList = new List<SelectListItem>();
            newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "" });
            if (Quarter == 1)
            {                
                newList.Add(new SelectListItem { Text = "Apr", Value = "4" });
                newList.Add(new SelectListItem { Text = "May", Value = "5" });
                newList.Add(new SelectListItem { Text = "Jun", Value = "6" });
            } else if (Quarter == 2)
            {
                newList.Add(new SelectListItem { Text = "Jul", Value = "7" });
                newList.Add(new SelectListItem { Text = "Aug", Value = "8" });
                newList.Add(new SelectListItem { Text = "Sep", Value = "9" });
            }
            else if (Quarter == 3)
            {
                newList.Add(new SelectListItem { Text = "Oct", Value = "10" });
                newList.Add(new SelectListItem { Text = "Nov", Value = "11" });
                newList.Add(new SelectListItem { Text = "Dec", Value = "12" });
            }
            else if (Quarter == 4)
            {
                newList.Add(new SelectListItem { Text = "Jan", Value = "1" });
                newList.Add(new SelectListItem { Text = "Feb", Value = "2" });
                newList.Add(new SelectListItem { Text = "Mar", Value = "3" });                
            }
            return new SelectList(newList, "Value", "Text");
        }
        
        [HttpPost]
        public ActionResult  InsertUpdateTrainingPlan(TrainingPlanViewModel objTrPlan)
        {
            bool IsSuccess = false;
            TrainingPlanModel objTrainingPlan = new TrainingPlanModel();

            if (ModelState.IsValid)
            {
                Master m = new Master();
                if(objTrPlan.Mode=="I")
                    objTrainingPlan.TrainingPlanId = 0;
                else
                    objTrainingPlan.TrainingPlanId = objTrPlan.TrainingPlanId;

                objTrainingPlan.Quarter = objTrPlan.Quarter;
                objTrainingPlan.TrainingId = objTrPlan.TrainingName;
                if (objTrainingPlan.TrainingId == 1260)
                {
                    objTrainingPlan.TrainingNameOther = objTrPlan.TrainingNameOther;
                }
                else
                {
                    objTrainingPlan.TrainingNameOther = string.Empty;
                }
                
                objTrainingPlan.TrainingHour = objTrPlan.Hrs;
                objTrainingPlan.TrainingTypeId = objTrPlan.TrainingType;
                objTrainingPlan.Month = objTrPlan.Month;
                objTrainingPlan.Target = objTrPlan.Target;
                objTrainingPlan.Year = objTrPlan.Year;
                objTrainingPlan.Mode = objTrPlan.Mode;
                objTrainingPlan.CreatedById = m.GetEmployeeIDByEmailID();
                
                if (!ModelState.IsValid)
                {
                    return View(objTrPlan);
                }
                else
                {
                    int savetStatus = _ItrainingService.InsertUpdateTrainingPlan(objTrainingPlan);
                    if (savetStatus == 1) IsSuccess = true;
                }
            }
            else
            {
                return View(objTrPlan);
            }

            if (IsSuccess)
            {
                if (objTrPlan.Mode == "I")
                    TempData["Message"] = "Training created successfully.";
                else
                    TempData["Message"] = "Training updated successfully.";

                TrainingPlanModel objModel1 = new TrainingPlanModel();
                objModel1.Year = objTrPlan.Year;
                objModel1.Quarter = objTrPlan.Quarter;
                objTrPlan.ListTrainingPan = _ItrainingService.GetTrainingPlan(objModel1);
                objTrPlan.FlagEdit = true;
                objTrPlan.ListTrainingName = CommonRepository.FillMasterDropDownList(CommonConstants.TrainingName);
                if (objTrPlan.ListTrainingPan.Count > 0)
                {
                    objTrPlan.TrainingId = 0;
                    objTrPlan.Hrs=null;
                    objTrPlan.Target = "";
                    objTrPlan.Month = 0;
                    objTrPlan.TrainingTypeId = 0;
                    return PartialView(CommonConstants._PartialViewAddPlan, objTrPlan);
                }
                
                return new EmptyResult();
            }
            else
            {
                return View(objTrPlan);

            }            
        }


        public PartialViewResult EditPlan(int TrainingPlanId, int Year, int Quarter)
        {
            TrainingPlanViewModel obj = new TrainingPlanViewModel();
            TrainingPlanModel objModel = new TrainingPlanModel();
            objModel.Mode = "U";
            objModel.Year = Year;
            objModel.Quarter = Quarter;
            objModel.TrainingPlanId = TrainingPlanId;
            obj.ListTrainingPan = _ItrainingService.GetTrainingPlan(objModel);
            
            obj.ListMonth = GetMonth(Quarter);
            obj.Mode = "U";
            obj.Year = Year;
            obj.Quarter = Quarter;
            
            if (objModel.TrainingPlanId > 0)
            {
                obj.TrainingName = obj.ListTrainingPan[0].TrainingId;
                obj.Hrs = obj.ListTrainingPan[0].TrainingHour;
                obj.TrainingType = obj.ListTrainingPan[0].TrainingTypeId;
                obj.Month = obj.ListTrainingPan[0].Month;
                obj.Target = obj.ListTrainingPan[0].Target;
            }
            //Load ListPlan
            TrainingPlanModel objList = new TrainingPlanModel();
            objList.Year = Year;
            objList.Quarter = Quarter;
            objList.TrainingPlanId = 0;
            obj.ListTrainingPan = _ItrainingService.GetTrainingPlan(objList);
            //obj.ListTrainingPan = obj.ListTrainingPan;
            obj.FlagEdit = true;
            return PartialView(CommonConstants._PartialViewAddPlan, obj);
        }

        public PartialViewResult DeletePlan(int TrainingPlanId, int Year, int Quarter)
        {
            TrainingPlanViewModel obj = new TrainingPlanViewModel();
            //TrainingPlanModel objModel = new TrainingPlanModel();
            //objModel.Mode = "U";
            //objModel.Year = Year;
            //objModel.Quarter = Quarter;
            obj.ListTrainingPan = _ItrainingService.DeleteTrainingPlan(TrainingPlanId,Convert.ToInt32(Session["EmpID"]));
            //Load ListPlan
            TrainingPlanModel objList = new TrainingPlanModel();
            objList.Year = Year;
            objList.Quarter = Quarter;
            objList.TrainingPlanId = 0;
            obj.ListTrainingPan = _ItrainingService.GetTrainingPlan(objList);
            //obj.ListTrainingPan = obj.ListTrainingPan;
            obj.FlagEdit = true;
            return PartialView(CommonConstants._PartialViewAddPlan, obj);


            //obj.ListMonth = GetMonth(Quarter);
            //obj.Mode = "U";
            //obj.Year = Year;
            //obj.Quarter = Quarter;

            //if (objModel.TrainingPlanId > 0)
            //{
            //    obj.TrainingName = obj.ListTrainingPan[0].TrainingId;
            //    obj.Hrs = obj.ListTrainingPan[0].TrainingHour;
            //    obj.TrainingType = obj.ListTrainingPan[0].TrainingTypeId;
            //    obj.Month = obj.ListTrainingPan[0].Month;
            //    obj.Target = obj.ListTrainingPan[0].Target;
            //}
            ////Load ListPlan
            //TrainingPlanModel objList = new TrainingPlanModel();
            //objList.Year = Year;
            //objList.Quarter = Quarter;
            //objList.TrainingPlanId = 0;
            //obj.ListTrainingPan = _ItrainingService.GetTrainingPlan(objList);
            //obj.ListTrainingPan = obj.ListTrainingPan;
            //obj.FlagEdit = true;
            //return PartialView(CommonConstants._PartialViewAddPlan, obj);
        }
        
        public ExcelFileResult ExportExcel(int year, int Quarter)
        {
            TrainingPlanViewModel obj = new TrainingPlanViewModel();
            TrainingPlanModel objModel = new TrainingPlanModel();
            objModel.Year = year;
            objModel.Quarter = Quarter;
            //obj.ListTrainingPan = _ItrainingService.GetTrainingPlan(objModel);

            DataTable dt = _ItrainingService.GetTrainingPlanDataSet(objModel);
            dt.Columns.Remove("TrainingPlanId");
            dt.Columns.Remove("Quarter");
            dt.Columns.Remove("TrainingId");
            dt.Columns.Remove("TrainingTypeId");
            dt.Columns.Remove("month");
            dt.Columns.Remove("isActive");

            // DataTable dt = -- > get your data
            ExcelFileResult actionResult = new ExcelFileResult(dt) { FileDownloadName = "TrainingPlan.xls" };
            //ExcelFileResult actionResult = new ExcelFileResult(dt) { FileDownloadName = string.Format("TrainingPlan{0}.xls", trainingcourseid) };
            return actionResult;
        }


        #region Training Plan for employees
        public ActionResult ViewTrainingPlanned()
        {
            TrainingPlanViewModel objVModel = new TrainingPlanViewModel();
            int CurrentMonth = DateTime.Today.Month;
            int CurrentYear = DateTime.Today.Year;

            // Set Current Year
            int Nextyear = CurrentYear; 
            int NextQuarter = 0;
            if (CurrentMonth == 1 || CurrentMonth == 2 || CurrentMonth == 3)
            {
                objVModel.Year = CurrentYear - 1;
                Nextyear = CurrentYear;
            }
            else
                objVModel.Year = CurrentYear;

            //int Nextyear = objVModel.Year;
           
            // Set Current Quarter
            switch (CurrentMonth)
            {
                case 1:
                    objVModel.Quarter = 4;
                    NextQuarter = 4;
                    break;
                case 2:
                    objVModel.Quarter = 4;
                    NextQuarter = 4;
                    break;
                case 3:
                    objVModel.Quarter = 4;
                    NextQuarter = 1;
                    break;

                case 4:
                    objVModel.Quarter = 1;
                    NextQuarter = 1;
                    break;
                case 5:
                    objVModel.Quarter = 1;
                    NextQuarter = 1;
                    break;
                case 6:
                    objVModel.Quarter = 1;
                    NextQuarter = 2;
                    break;
                
                case 7:
                     objVModel.Quarter = 2;
                    NextQuarter = 2;
                    break;
                case 8:
                     objVModel.Quarter = 2;
                    NextQuarter = 2;
                    break;
                case 9:
                    objVModel.Quarter = 2;
                    NextQuarter = 3;
                    break;

                case 10:
                     objVModel.Quarter = 3;
                    NextQuarter = 3;
                    break;
                case 11:
                     objVModel.Quarter = 3;
                    NextQuarter = 3;
                    break;
                case 12:
                    objVModel.Quarter = 3;
                    NextQuarter = 4;
                    break;
                default:
                    break;
            }
            objVModel.Month = CurrentMonth;
            objVModel.FlagEdit = false ;
            objVModel.FlagEmployeePage  = true ;

            TrainingPlanModel objSingle = new TrainingPlanModel();  //objModel.ListTrainingPan; 
            objSingle.Year = objVModel.Year;
            objSingle.Quarter = objVModel.Quarter;
            //List<TrainingPlanModel> objs = new List<TrainingPlanModel>();  //objModel.ListTrainingPan; 


            objVModel.ListTrainingPan = _ItrainingService.GetTrainingPlanEmployees(objSingle, NextQuarter, Nextyear);

            return View("TrainingPlanned", objVModel);
        }
        #endregion
    }
}
