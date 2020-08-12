using Domain.Entities;
using Infrastructure;
using RMS.Common.BusinessEntities;
using RMS.Helpers;
using RMS.Models;
using Services;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Web.UI.WebControls;

namespace RMS.Controllers
{
    public class InterviewPanelController :  ErrorController
    {
        private readonly IEmployeeService _objEmployeeService;
        private readonly IInterviewPanelService _objIInterviewPanelService;

        public InterviewPanelController(IEmployeeService objemployeeservice, IInterviewPanelService objInterviewPanelService)
        {
            _objEmployeeService = objemployeeservice;
            _objIInterviewPanelService = objInterviewPanelService;
        }

        InterviewPanelViewModel init(InterviewPanelViewModel obj)
        {
            obj.EmpId = 0;
            obj.CandidateDeptId = 0;
            obj.CandidateDesignations = null;
            obj.SecondarySkills= null;
            obj.ListEmpName = CommonRepository.FillEmployeesList();
            obj.ListDeptName = CommonRepository.FillDepartmentList();
            obj.ListDesignationName = CommonRepository.FillDropdownDefaultValueSelect();
            obj.ListAttended = CommonRepository.FillMasterDropDownList("TrainingAttended");
            obj.ListInducted = CommonRepository.FillMasterDropDownList("InterviewInducted");
            obj.ListLevel = CommonRepository.FillMasterRadioButtonList("InterviewLevel");
            //obj.ListSecondarySkills = _objEmployeeService.GetSkillTypesCategory();
            //obj.ListTechnology = _objIInterviewPanelService.GetEmployeeSkillLinked("EP");
            obj.ListSecondarySkills = CommonRepository.FillDropdownDefaultValueSelect(); 
            //
            return obj;
        }

        #region "Add \update Interview Panel"
        public ActionResult AddInterviewPanel()
        {
            InterviewPanelViewModel obj = new InterviewPanelViewModel();
            obj=init(obj);
            obj.ListAllIP = _objIInterviewPanelService.GetInterviewPanel(0,0);
            obj.EmpId = 0;
            obj.Mode = "List";
            return View("AddInterviewPanel", obj);
        }

        [HttpPost]
        public ActionResult Responsearr(string EmpId)
        {

            TempData["passedArray"] = EmpId;

            return Json(new { ok = true, newurl = Url.Action("AddInterviewPanel", "InterviewPanel") });
            // var redirectUrl = new UrlHelper(Request.RequestContext).Action("ViewTechnicalTrainingRequest","Training");
            //return Json(new { Url = redirectUrl });

        }

        [HttpPost]
        public ActionResult AddInterviewPanel(InterviewPanelViewModel obj, FormCollection objf)
        {
           int result= AddIP(obj, objf);
            if(result ==0)
                RedirectToAction("AddInterviewPanel");
            return View();
        }

        int AddIP(InterviewPanelViewModel obj, FormCollection objf)
        {
            InterviewPanelModel objI = new InterviewPanelModel();
            if (obj.Mode == "A")
            {
                objI.EmpId = obj.EmpId;
                //objI.EmpId = Convert.ToInt32(objf["hfldEmpid"]);
                objI.CandidateDeptId = obj.CandidateDeptId;
                objI.CandidateDesignations = objf["lstDesignation"];
                objI.SecondarySkills = objf["lstSkill"];
                objI.LevelIds = objf["chkLevel"];
                objI.TrainingAttended = obj.TrainingAttendedId;
                objI.Inducted = obj.InductedId;
                objI.CreatedBy = Convert.ToInt32(Session["EmpID"]);
                string strErrorMessage = _objIInterviewPanelService.IsAlreadyExist(objI);

                if (strErrorMessage == "")
                {
                    objI.PanelId = _objIInterviewPanelService.Insert(objI);
                    if (objI.PanelId != 0)
                    {
                        TempData["Message"] = "Interview Panel added successfully";
                        obj = init(obj);
                        obj.ListAllIP = _objIInterviewPanelService.GetInterviewPanel(0, 0);
                        return 1;// RedirectToAction("AddInterviewPanel");
                    }
                    else
                        return 0;
                }
                else
                {
                    TempData["Message"] = strErrorMessage;
                    return 2;// RedirectToAction("AddInterviewPanel");
                }
            }
            return 1;
        }
        public ActionResult EditInterviewPanel(string InterviewPanelId, string Mode)
        {
            int _InterviewPanelId = Convert.ToInt32(CheckAccessAttribute.Decode(InterviewPanelId));

            InterviewPanelViewModel obj = new InterviewPanelViewModel();
            obj = init(obj);
            //Get single list
            if (Mode == "A")
                obj.ListSingleIp = _objIInterviewPanelService.GetInterviewPanel(0, _InterviewPanelId);
            else
                obj.ListSingleIp = _objIInterviewPanelService.GetInterviewPanel(_InterviewPanelId, 0);

            if (obj.ListSingleIp.Count > 0)
            {
                obj.EmpId = obj.ListSingleIp[0].EmpId;
                //Load Employee Designation & Technology
                EmployeeModel ObjEmp = _objEmployeeService.GetEmployeeDetailByID(obj.EmpId);
                obj.EmpDesignation = ObjEmp.Designation;
                obj.EmpTechnology = ObjEmp.PrimarySkill;

                obj.InterviewPanelId = obj.ListSingleIp[0].PanelId;
                obj.CandidateDeptId = obj.ListSingleIp[0].CandidateDeptId;
                obj.ListDesignationName = _objEmployeeService.FillDesignationList(obj.ListSingleIp[0].CandidateDeptId);
                obj.CandidateDesignations = obj.ListSingleIp[0].CandidateDesignations;
                obj.SecondarySkills = obj.ListSingleIp[0].SecondarySkills;
                obj.InductedId = obj.ListSingleIp[0].Inducted;
                obj.TrainingAttendedId = obj.ListSingleIp[0].TrainingAttended;
                obj.LevelIds = obj.ListSingleIp[0].LevelIds;
                obj.Mode = Mode;

                obj.ListSingleIp[0].ListCollectionDesign = _objIInterviewPanelService.GetInterviewPanelDetail(_InterviewPanelId, "D").ToList<SelectListItem>();
                obj.ListSingleIp[0].ListCollectionSecSkill = _objIInterviewPanelService.GetInterviewPanelDetail(_InterviewPanelId, "S").ToList<SelectListItem>();

                obj.ListSecondarySkills = _objIInterviewPanelService.GetEmployeeSkillLinked("ES", obj.EmpId.ToString());
                //Load Grid Data
                obj.ListAllIP = _objIInterviewPanelService.GetInterviewPanel(0,0);
            }

            return View("AddInterviewPanel", obj);
        }

        [HttpPost]
        public ActionResult EditInterviewPanel(InterviewPanelViewModel obj, FormCollection objf)
        {            
            InterviewPanelModel objI = new InterviewPanelModel();
            if (obj.Mode == "A")
            {
                int result = AddIP(obj, objf);
                if (result == 0)
                {
                    TempData["Message"] = "Interview Panel has not added";
                    return RedirectToAction("AddInterviewPanel");
                }
                //TempData["Message"] = "Interview Panel added successfully";
                return RedirectToAction("AddInterviewPanel");
            }
            if (obj.Mode == "U")
            {
                objI.PanelId = Convert.ToInt32(CheckAccessAttribute.Decode(objf["InterviewPanelId"]));
                objI.EmpId = obj.EmpId;
                objI.CandidateDesignations = objf["lstDesignation"];
                objI.SecondarySkills = objf["lstSkill"];
                objI.LevelIds = objf["chkLevel"];
                objI.TrainingAttended = obj.TrainingAttendedId;
                objI.Inducted = obj.InductedId;
                objI.CreatedBy = Convert.ToInt32(Session["EmpID"]);
                int result = _objIInterviewPanelService.UpdateInterviewPanel(objI);
                if (objI.PanelId != 0)
                {
                    TempData["Message"] = "Interview Panel updated successfully";
                    return RedirectToAction("AddInterviewPanel");
                }
            }
            return View();
        }

        public ActionResult DeleteInterviewPanel(string InterviewPanelId)
        {
            InterviewPanelViewModel objVm = new InterviewPanelViewModel();
            int _InterviewPanelid = Convert.ToInt32(CheckAccessAttribute.Decode(InterviewPanelId));
            InterviewPanelModel objI = new InterviewPanelModel();
            objI.PanelId = _InterviewPanelid;
            //Get single list
            int i = _objIInterviewPanelService.Delete(objI);

            if (i > 0)
            {
                objVm.Message = "Interview Panel deleted successfully";
                TempData["Message"] = "Interview Panel deleted successfully";
                return RedirectToAction("AddInterviewPanel");
            }

            return View();
        }

        #endregion
                
        public JsonResult  LoadEmpDetail(string EmpId)
        {
            if (EmpId == "")
                EmpId = "0";
            //int _EmpId = Convert.ToInt32(CheckAccessAttribute.Decode(EmpId));
            EmployeeModel ObjEmp = _objEmployeeService.GetEmployeeDetailByID(Convert.ToInt32( EmpId.ToString().Replace(' ','0')));

            
            InterviewPanelViewModel obj = new InterviewPanelViewModel();
            obj.ListSecondarySkills = _objIInterviewPanelService.GetEmployeeSkillLinked("ES", EmpId);
            //IEnumerable<SelectListItem> TrainingNameData= = _objIInterviewPanelService.GetEmployeeSkillLinked("ES");

            var result = new { PrimarySkill = ObjEmp.PrimarySkill, Designation = ObjEmp.Designation, ListData = obj.ListSecondarySkills };
            return Json(result, JsonRequestBehavior.AllowGet);
        }    
 
        //[AcceptVerbs(HttpVerbs.Get)]
        public JsonResult FillDesignationList(string DeptId)
        {
            IEnumerable<SelectListItem> TrainingNameData = _objEmployeeService.FillDesignationList(Convert.ToInt32(DeptId));
           //TrainingNameData = new SelectList(_service.FillDesignationList(Convert.ToInt32(traingType), true, ""), "Value", "Text");
           return Json(TrainingNameData, JsonRequestBehavior.AllowGet);
        }


        InterviewPanelViewModel InitSearch(InterviewPanelViewModel obj)
        {
            obj.ListDeptName = CommonRepository.FillDepartmentList();
            obj.ListDesignationName = CommonRepository.FillDropdownDefaultValueSelect();
            obj.ListLevel = CommonRepository.FillMasterDropDownList("InterviewLevel");
            //obj.ListTechnology = CommonRepository.FillMasterDropDownList("PrimarySkills");
            //obj.ListSecondarySkills = _objEmployeeService.GetSkillTypesCategory();
            obj.ListBusinessVertical = CommonRepository.FillMasterDropDownList("Business Vertical");

            obj.ListTechnology = _objIInterviewPanelService.GetEmployeeSkillLinked("EP","0");
            obj.ListSecondarySkills = _objIInterviewPanelService.GetEmployeeSkillLinked("SS_IP","0");
            return obj;
        }
        public ActionResult ListInterviewPanel()
        {
            InterviewPanelViewModel obj = new InterviewPanelViewModel();
            obj = InitSearch(obj);
            //obj.ListAllIP = _objIInterviewPanelService.GetInterviewPanel(0);
            return View("ListInterviewPanel", obj);
        }
        
        public ActionResult SearchIP(string  Technology,string Level ,string DeptId,string Designation,string BusinessVertical,string Skill)
        {
            InterviewPanelViewModel obj = new InterviewPanelViewModel();
            obj.ListAllIP = _objIInterviewPanelService.GetInterviewPanelSearch(Technology, Level , DeptId, Designation, BusinessVertical, Skill);
            return PartialView("_ListInterviewPanel", obj);
        }

         
    }
}

