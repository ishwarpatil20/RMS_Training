using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Domain.Entities;
using Services.Interfaces;
using Services;
using System.Configuration;
using RMS.Common.Constants;
using RMS.Common;
using RMS.Helpers;
using RMS.Models;
using RMS.Common.BusinessEntities;
using Infrastructure;
using System.Collections;

namespace RMS.Controllers
{
    [CheckAccess]
    public class NominationController : ErrorController
    {

        private List<TrainingModel> viewTrainingListModel = new List<TrainingModel>();
        private readonly ITrainingService _service;
        private int result;
        private string message = string.Empty;

        /// <summary>
        /// Constructor for creating reference for Training service layer
        /// </summary>
        /// <value>Interface TrainingModel</value>
        public NominationController(ITrainingService service)
        {
            _service = service;
        }

        public NominationController()
        {
        }

        //Harsha Issue Id-59073 - Start
        //Description- Training : Create emp Nominated page so that emp can view they nomination. Change confirmation email body
        ///<summary> 
        /// Get ViewNomination View for Training
        /// </summary>
        public ActionResult ViewNominationForEmployeeTraining(string courseId) 
        {
            ViewBag.CourseId = CheckAccessAttribute.Decode(Convert.ToString(courseId));
            NominationViewModel model = new NominationViewModel(_service,"ViewNominationForEmployeeTraining");            
            //set message on view page for required operation
            ViewData[CommonConstants.Result] = TempData[CommonConstants.Message];
            return View(model);
        }
                
        public PartialViewResult ShowEmployeeTrainingNominationDetails(int courseId) {
            EmployeeTrainingNominationDetailsModel model = new EmployeeTrainingNominationDetailsModel(_service, courseId);
            //model.CourseId = courseId;
            return PartialView(CommonConstants._PartialViewEmployeeTrainingNominationDetails, model);
        }
        //Harsha Issue Id-59073 - End
        /// <summary>
        /// Get Submit Nomination View for Training
        /// </summary>        
        public ActionResult ViewSubmitNominationForTraining()
        {
            //create view model and bind it to view
            NominationViewModel model = new NominationViewModel(_service);
            //set message on view page for required operation
            ViewData[CommonConstants.Result] = TempData[CommonConstants.Message];
            
            return View(CommonConstants.ViewSubmitNominationForTraining, model);
        }



        /// <summary>
        /// Get Course Detail by Training ID
        /// </summary>
        /// <value>trainingid</value>
        public PartialViewResult ViewTrainingCourseDetailByID(int trainingcourseid, bool isConfirmPageNomination = false)
        {
            NominationModel model = new NominationModel();
            model = _service.GetTrainingDetailByID(trainingcourseid);
            model.CourseFile = new FileUploadModel(CommonRepository.GetFileDetailList(CommonConstants.CourseContentDir, model.UploadedCourseFile));
            model.CourseFile.FileViewMode = 1;
            model.IsConfirmNominationPage = isConfirmPageNomination;
            return PartialView(CommonConstants._PartialViewTrainingDetail, model);
        }

        //Neelam Issue Id:59566 start
        public ActionResult ShowResult()
        {
           
            ViewData[CommonConstants.Result] = TempData[CommonConstants.Message];
            return View("_Result");
        }
        //Neelam Issue Id:59566 end

        /// <summary>
        /// Get Employee list and Grid 
        /// </summary>        
        public ActionResult ShowEmployeeGrid(int trainingnameid, int trainingcourseid)
        {
            int modificationFlag;

            List<Employee> employeeListModel = new List<Employee>();
            employeeListModel = _service.GetAllNominatedEmployeeList(trainingnameid, trainingcourseid);

            foreach (Employee e in employeeListModel)
            {
                if (e.SubmitStatus == "NominationSaved")
                {
                    modificationFlag = e.EmployeeID;
                    ViewData["SavedFlag"] = Convert.ToString(ViewData["SavedFlag"]) + ',' + modificationFlag;
                }
                else
                {
                    ViewData["SubmitFlag"] = e.EmployeeID;

                }
            }
            

            //set message on view page for required operation
            ViewData[CommonConstants.Result] = TempData[CommonConstants.Message];

            if (employeeListModel.Count > 0)
            {
                return View(CommonConstants._PartialVIewEmployeeDetail, employeeListModel);
            }
            else
            {
                ViewData["SavedFlag"] = true;
                Employee addEmployee = new Employee();
                // Changed by : Venkatesh  : Start
                bool IsAdminRole = false;
                if (Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
                {
                    ArrayList arrRolesForUser = new ArrayList();
                    arrRolesForUser = (ArrayList)Session[AuthorizationManagerConstants.AZMAN_ROLES];
                    if (arrRolesForUser.Contains(CommonConstants.AdminRole))
                        IsAdminRole = true;
                }
                // Changed by : Venkatesh  : End

                addEmployee.IsRMOLogin = IsAdminRole == true ? true : false;

                NominationModel model = new NominationModel();
                model = _service.GetTrainingDetailByID(trainingcourseid);
                addEmployee.TrainingTypeID = model.TrainingTypeID;

                employeeListModel.Add(addEmployee);

                return View(CommonConstants._PartialVIewEmployeeDetail, employeeListModel);
            }
            //return new EmptyResult();
        }

        /// <summary>
        /// Get detail of Employee based on ID
        /// </summary>
        /// <value>Employee ID</value>
        public ActionResult AddNomination(int trainingtypeid, bool isrmologin = false, int nominationtypeid = 0)
        {
            EmployeeNominationViewModel model = new EmployeeNominationViewModel(_service);
            model.TrainingTypeID = trainingtypeid;
            model.isRMOLogin = isrmologin;
            model.NominationTypeID = nominationtypeid;
            if (!isrmologin)
            {
                if (nominationtypeid == CommonConstants.SelfNomination)
                {

                    int empid = Convert.ToInt16(System.Web.HttpContext.Current.Session["EmpID"]);
                    // Changed by : Venkatesh  : Start
                    bool IsManagerRole = false;
                    if (Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
                    {
                        ArrayList arrRolesForUser = new ArrayList();
                        arrRolesForUser =  (ArrayList)Session[AuthorizationManagerConstants.AZMAN_ROLES];
                        if (arrRolesForUser.Contains("manager"))                       
                                IsManagerRole = true;
                    }
                    // Changed by : Venkatesh  : End
                    //string Rolename = System.Web.HttpContext.Current.Session["_RoleName"].ToString();
                    if (IsManagerRole )
                    {
                        model.SelectedEmployee = 0;
                        model.SelectedNominator = empid;
                    }
                    else
                    {
                        model.EmployeeList = new SelectList(_service.GetSelfEmployeeList(), "Key", "Value");
                        model.SelectedEmployee = empid;
                    }
                }
                
            }
            else
            {
                model.SelectedRMONominator = Convert.ToInt16(System.Web.HttpContext.Current.Session["EmpID"]);
            }

            return View(CommonConstants._PartialVIewEmployeeDetailDialog, model);
        }


        /// <summary>
        /// Get detail of Employee based on ID
        /// </summary>
        /// <value>Employee ID</value>
        public ActionResult ShowEmployeeNominationForm(int trainingtypeid, int trainingcourseid, int trainingnameid, bool isrmologin, int NominationTypeID)
        {
            int empid = Convert.ToInt16(System.Web.HttpContext.Current.Session["EmpID"]);
            EmployeeNominationViewModel model = new EmployeeNominationViewModel(_service, trainingcourseid, trainingnameid, empid);
            model.isRMOLogin = isrmologin;
            model.SelectedEmployee = empid;
            model.TrainingTypeID = trainingtypeid;
            model.NominationTypeID = NominationTypeID;
            
            if (!isrmologin)
            {
                if (model.NominationTypeID == CommonConstants.SelfNomination)
                {

                    // Changed by : Venkatesh  : Start
                    bool IsManagerRole = false;
                    if (Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
                    {
                        ArrayList arrRolesForUser = new ArrayList();
                        arrRolesForUser = (ArrayList)Session[AuthorizationManagerConstants.AZMAN_ROLES];
                        if (arrRolesForUser.Contains("manager"))
                            IsManagerRole = true;
                    }
                    // Changed by : Venkatesh  : End


                    //string Rolename = System.Web.HttpContext.Current.Session["_RoleName"].ToString();
                    if (IsManagerRole)
                    {
                        model.SelectedEmployee = 0;
                        model.SelectedNominator = empid;
                    }
                    else
                    {
                        model.EmployeeList = new SelectList(_service.GetSelfEmployeeList(), "Key", "Value");
                        model.SelectedEmployee = empid;
                    }
                }
                //model.EmployeeList = new SelectList(_service.GetSelfEmployeeList(), "Key", "Value");
            }
            else if (isrmologin)
            {
                model.SelectedRMONominator = empid;
                model.SelectedEmployee = 0;
            }
            
            model.EditMode = false;
          
            return View(CommonConstants._PartialVIewEmployeeDetailDialog, model);
        }


        /// <summary>
        /// Get detail of Employee based on ID
        /// </summary>
        /// <value>EmployeeNominationViewModel</value>
        /// <value>trainingcourseID</value>
        /// <value>trainingnameID</value>        
        [HttpPost]
        public ActionResult AddNomination(EmployeeNominationViewModel model, int trainingcourseID, int trainingnameID)
        {
            if (ModelState.IsValid)
            {
                string RoleManager = string.Empty;
                if (System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
                {
                    System.Collections.ArrayList arrRolesForUser = new System.Collections.ArrayList();
                    arrRolesForUser = (System.Collections.ArrayList)System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES];

                    if (arrRolesForUser.Contains("manager"))
                    {
                        RoleManager = "Manager";
                    }
                }

                Employee saveNominatedEmployee = new Employee();
                saveNominatedEmployee.courseID = trainingcourseID;
                saveNominatedEmployee.EmployeeID = model.SelectedEmployee;
                saveNominatedEmployee.TrainingNameID = trainingnameID;
                saveNominatedEmployee.PriorityCode = model.SelectedPriority;
                saveNominatedEmployee.PreTrainingCode = model.SelectedPreTrainingRating;
                saveNominatedEmployee.Comment = model.Comment;
                saveNominatedEmployee.ObjectiveForSoftSkill = model.ObjectiveForSoftSkill;
                saveNominatedEmployee.RMONominatorID = model.SelectedNominator;
                saveNominatedEmployee.IsRMOLogin = model.isRMOLogin;
                saveNominatedEmployee.NominationTypeID = model.NominationTypeID;
                //Neelam : 24/07/2017 Ends. IssueId 59566
                if (saveNominatedEmployee.NominationTypeID == 1913 &&
                        saveNominatedEmployee.IsRMOLogin == false && RoleManager.ToLower() != CommonConstants.ManagerRole.ToLower() &&
                        saveNominatedEmployee.EmployeeID == Convert.ToInt16(System.Web.HttpContext.Current.Session["EmpID"]))
                {
                    int status = _service.SaveNominatedEmployee(saveNominatedEmployee);
                  
                   
                    if (status == 1)
                    {
                       
                        SubmitNomination(saveNominatedEmployee.courseID, saveNominatedEmployee.TrainingNameID, Convert.ToString(saveNominatedEmployee.EmployeeID));
                        TempData["savedStatus"] = true;
                        


                    }
                    else
                    {
                        TempData[CommonConstants.Message] = "The selected nominee has already been nominated for this training.";
                        
                        
                    }
                  
                    return RedirectToAction("ShowResult", "nomination");
                }
                else
                {
                    int status = _service.SaveNominatedEmployee(saveNominatedEmployee);
                    
                    if (status == 1)
                    {
                        TempData[CommonConstants.Message] = "Nomination added successFully.";
                    }
                    else
                    {
                        TempData[CommonConstants.Message] = "The selected nominee has already been nominated for this training.";
                    }
                    return RedirectToAction("ShowEmployeeGrid", "nomination", new { trainingnameid = trainingnameID, trainingcourseid = trainingcourseID });

                }
            }
            else
            {
                TempData[CommonConstants.Message] = "Invalid input.";
                return RedirectToAction("ShowEmployeeGrid", "nomination", new { trainingnameid = trainingnameID, trainingcourseid = trainingcourseID });
                
            }
            
            //Neelam : 24/07/2017 Ends. IssueId 59566
        
        }

        //Neelam : 31/05/2017 Starts. IssueId 59566
        private void SubmitNomination(int trainingcourseID, int trainingnameID, string selectedemployeeid)
        {
            List<Employee> savedEmployee = _service.SubmitNominatedEmployee(trainingcourseID, trainingnameID, selectedemployeeid);
            if (savedEmployee.Count > 0)
            {
                EmailHelper.SendMailForNominationSubmission(savedEmployee);
            }
            TempData[CommonConstants.Message] = "Nomination Submitted Successfully.";
            
        }
        //Neelam : 31/05/2017 End. IssueId 59566

        /// <summary>
        /// Delete nominated employee
        /// </summary>
        /// <value>Employee ID</value>        
        public ActionResult DeleteNomination(int trainingcourseID, int trainingnameID, int deleteemployeeid, int NominationID, string IsConfirmNominationID, string ReasonForRejection, string submitStatus)
        {
            string abc = Request.QueryString["ReasonForRejection"];
            //int status = _service.DeleteNominatedEmployee(trainingcourseID, trainingnameID, deleteemployeeid);
            // Harsha Issue Id-59072 - Start
            // Description: Training : provision to Re-approve the  Rejected Nomination for the courses
            bool isConfirmedNomination = Convert.ToBoolean(IsConfirmNominationID);
            Employee deletedEmployee = _service.DeleteNominatedEmployee(trainingcourseID, trainingnameID, deleteemployeeid, NominationID, isConfirmedNomination);
            // Harsha Issue Id-59072 - End
            List<Employee> deletedEmployeeList = new List<Employee>();
            deletedEmployeeList.Add(deletedEmployee);
            if (submitStatus != CommonConstants.NominationSaved)
            {
                if (deletedEmployeeList.Count > 0)
                {
                    if (IsConfirmNominationID == "false")
                    {
                        Employee LoginEmpID = _service.GetEmployeeDetailByID(Convert.ToInt32(Session["EmpID"]));
                        EmailHelper.SendMailForNominationDeleted(deletedEmployeeList, LoginEmpID.EmployeeName);
                    }
                    else
                    {
                        Employee LoginEmpID = _service.GetEmployeeDetailByID(Convert.ToInt32(Session["EmpID"]));
                        EmailHelper.SendMailForNominationRejected(deletedEmployeeList, LoginEmpID.EmployeeName, ReasonForRejection);
                    }
                }
            }
            TempData[CommonConstants.Message] = "Nomination Deleted Successfully.";

            //Issue ID : 57843 Ishwar Patil start
            //desc : pass confirmation nomination parameter to detele nomination function in controller
            if (IsConfirmNominationID == "false")
            {
                return RedirectToAction("ShowEmployeeGrid", "nomination", new { trainingnameid = trainingnameID, trainingcourseid = trainingcourseID });
            }
            else
            {
                return RedirectToAction("ShowConfirmNominationEmployeeGrid", "nomination", new { trainingnameid = trainingnameID, trainingcourseid = trainingcourseID });
            }
            //Issue ID : 57843 Ishwar Patil End
        }

        
        public ActionResult RejectNomination(int trainingcourseID, int trainingnameID, string deleteemployeeid, string IsConfirmNominationID, string ReasonForRejection)
        {
            string[] delete = deleteemployeeid.Split(',');
            List<Employee> deletedEmployeeList = new List<Employee>();
            Employee deleteEmployee = new Employee();
            //int status = _service.DeleteNominatedEmployee(trainingcourseID, trainingnameID, deleteemployeeid);
            // Harsha Issue Id-59072 
            // Description: Training : provision to Re-approve the  Rejected Nomination for the courses
            bool isConfirmedNomination = Convert.ToBoolean(IsConfirmNominationID);
            foreach (var item in delete)
            {
                if (item != "")
                {
                    deleteEmployee = _service.DeleteNominatedEmployee(trainingcourseID, trainingnameID, Convert.ToInt32(item), 0, isConfirmedNomination);
                    // Harsha Issue Id-59072 - End
                    deletedEmployeeList.Add(deleteEmployee);
                }
            }
            //List<Employee> deletedEmployeeList = _service.DeleteNominatedEmployee(trainingcourseID, trainingnameID, deleteemployeeid, 0);
            if (deletedEmployeeList.Count > 0)
            {
                if (IsConfirmNominationID == "false")
                {
                    Employee LoginEmpID = _service.GetEmployeeDetailByID(Convert.ToInt32(Session["EmpID"]));
                    EmailHelper.SendMailForNominationDeleted(deletedEmployeeList, LoginEmpID.EmployeeName);
                }
                else
                {
                    Employee LoginEmpID = _service.GetEmployeeDetailByID(Convert.ToInt32(Session["EmpID"]));
                    EmailHelper.SendMailForNominationRejected(deletedEmployeeList, LoginEmpID.EmployeeName, ReasonForRejection);
                }
            }
            //Harsha Issue Fix- Start
            //Description- Incorrect message ‘Nomination deleted successfully’ appears when a nomination is rejected.
            TempData[CommonConstants.Message] = "Nomination Rejected Successfully.";
            //Harsha Issue Fix- End

            //Issue ID : 57843 Ishwar Patil start
            //desc : pass confirmation nomination parameter to detele nomination function in controller
            if (IsConfirmNominationID == "false")
            {
                return RedirectToAction("ShowEmployeeGrid", "nomination", new { trainingnameid = trainingnameID, trainingcourseid = trainingcourseID });
            }
            else
            {
                return RedirectToAction("ShowConfirmNominationEmployeeGrid", "nomination", new { trainingnameid = trainingnameID, trainingcourseid = trainingcourseID });
            }
            //Issue ID : 57843 Ishwar Patil End
        }


        /// <summary>
        /// Edit nominated employee
        /// </summary>
        /// <value>trainingcourseID</value>        
        /// <value>trainingnameID</value>        
        /// <value>editemployeeid</value>        
        public ActionResult EditNomination(int trainingcourseid, int trainingnameid, int editemployeeid, bool isrmologin)
        {
            EmployeeNominationViewModel model = new EmployeeNominationViewModel(_service, trainingcourseid, trainingnameid, editemployeeid);
            model.isRMOLogin = isrmologin;
            return View(CommonConstants._PartialVIewEmployeeDetailDialog, model);
        }


        /// <summary>
        /// Update nominated employee
        /// </summary>
        /// <value>model</value>        
        /// <value>trainingcourseID</value>        
        /// <value>trainingnameID</value>        
        public ActionResult UpdateNomination(EmployeeNominationViewModel model, int trainingcourseID, int trainingnameID)
        {
            if (ModelState.IsValid)
            {
                Employee updateNominatedEmployee = new Employee();
                updateNominatedEmployee.courseID = trainingcourseID;
                updateNominatedEmployee.EmployeeID = model.SelectedEmployee;
                updateNominatedEmployee.TrainingNameID = trainingnameID;
                updateNominatedEmployee.PriorityCode = model.SelectedPriority;
                updateNominatedEmployee.PreTrainingCode = model.SelectedPreTrainingRating;
                updateNominatedEmployee.Comment = model.Comment;
                updateNominatedEmployee.ObjectiveForSoftSkill = model.ObjectiveForSoftSkill;
                updateNominatedEmployee.RMONominatorID = model.SelectedNominator;
                updateNominatedEmployee.IsRMOLogin = model.isRMOLogin;

                int status = _service.UpdateNominatedEmployee(updateNominatedEmployee);

                switch (status)
                {
                    case (1):
                        TempData[CommonConstants.Message] = "Nomination Updated SuccessFully.";
                        break;
                    case (2):
                        TempData[CommonConstants.Message] = "Employee not found for update.";
                        break;
                }
            }
            else
            {
                TempData[CommonConstants.Message] = "Invalid input.";
            }

            return RedirectToAction("ShowEmployeeGrid", "nomination", new { trainingnameid = trainingnameID, trainingcourseid = trainingcourseID });
        }


        /// <summary>
        /// Submit nominated employee permanently
        /// </summary>        
        /// <value>Employee ID</value> 
        //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SubmitTrainingNomination(int trainingcourseID, int trainingnameID, string selectedemployeeid)
        {
            //Neelam : 31/05/2017 Starts. IssueId 59566
            SubmitNomination(trainingcourseID, trainingnameID, selectedemployeeid);
            //Neelam : 31/05/2017 End. IssueId 59566
            return RedirectToAction(CommonConstants.ViewSubmitNominationForTraining);
        }


        #region Confirm Nomination Section


        /// <summary>
        /// Get Confirm Nomination View for Training
        /// </summary>
        public ActionResult ViewConfirmNominationForTraining()
        {
            int empID = Convert.ToInt16(System.Web.HttpContext.Current.Session["EmpID"]);            
            //create view model and bind it to view
            NominationViewModel model = new NominationViewModel();
            model.CourseName = new SelectList(_service.GetConfirmTrainingNameforAllCourses(empID), "Key", "Value");
            //set message on view page for required operation
            ViewData[CommonConstants.Result] = TempData[CommonConstants.Message];
            return View(CommonConstants.ConfirmTrainingNomination, model);
        }

        /// <summary>
        /// Get Nominated Employee list in Grid 
        /// </summary>        
        public ActionResult ShowConfirmNominationEmployeeGrid(int trainingnameid, int trainingcourseid)
        {
            List<Employee> employeeListModel = new List<Employee>();
            employeeListModel = _service.GetAllSubmittedEmployeeListByCourseID(trainingnameid, trainingcourseid);

            //set message on view page for required operation
            ViewData[CommonConstants.Result] = TempData[CommonConstants.Message];

            if (employeeListModel.Count > 0)
            {
                return View(CommonConstants._PartialVIewEmployeeDetail, employeeListModel);
            }
            return new EmptyResult();
        }

        /// <summary>
        /// Save or Update confirm nomination
        /// </summary>  
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SaveUpdateConfirmedNomination(int trainingnameid, int trainingcourseid, string selectedemployeeid)
        {
            List<Employee> confirmnominationlist = new List<Employee>();
            confirmnominationlist = _service.SaveUpdateNominatedEmployee(trainingnameid, trainingcourseid, selectedemployeeid);

            //send confirmation email
            EmailHelper.SendMailForConfirmNomination(confirmnominationlist);

            //set message
            TempData[CommonConstants.Message] = "Nomination Confirmed Successfully.";

            return RedirectToAction(CommonConstants.ShowConfirmNominationEmployeeGrid, CommonConstants.NominationController, new { trainingnameid = trainingnameid, trainingcourseid = trainingcourseid });
        }

        /// <summary>
        /// Excel-Export
        /// </summary>
        public ExcelFileResult ExportExcel(int trainingnameid, int trainingcourseid)
        {
            DataTable dt = _service.GetDataTableAllSubmittedEmployeeListByCourseID(trainingnameid, trainingcourseid);
            int TrainingTypeID = Convert.ToInt32(dt.Rows[0]["TrainingTypeID"]);
            DataTable dt1 = new DataTable();
            if (TrainingTypeID == 1207)
            {
                string[] selectedColumns = new[] { "EmployeeCode", "EmployeeName", "EmailId", "Designation", "Project", "Priority", "PreTrainingRating", "Comments", "TrainingName","NominatorName","NominatorEmailID"};
                dt1 = new DataView(dt).ToTable(false, selectedColumns);
            }
            else if (TrainingTypeID == 1208)
            {
                string[] selectedColumns = new[] { "EmployeeCode", "EmployeeName", "EmailId", "Designation", "Project", "Priority", "ObjectiveForSoftSkill", "Comments", "TrainingName", "NominatorName", "NominatorEmailID" };
                dt1 = new DataView(dt).ToTable(false, selectedColumns);
            }
            
            
            // DataTable dt = -- > get your data
            ExcelFileResult actionResult = new ExcelFileResult(dt1) { FileDownloadName = string.Format("ConfirmNominationForCourseID_{0}.xls", trainingcourseid) };
            return actionResult;
        }


        #endregion


        [HttpGet]
        public ActionResult MailToAdmin(int CourseID)
        {
            //int CourseID = 1;
            //ViewBag.CourseName = _service.GetTrainingNameByCourseID(CourseID);
            //ViewBag.CourseID = CourseID;
            DataSet ds = _service.GetAccomodationDetailsByCourseID(CourseID);

            ViewBag.CourseName = ds.Tables[0].Rows[0]["CourseName"].ToString();

            DataSet ds1 = _service.GetTravelDetailsByCourseID(CourseID);

            AccomodationTravelViewModel objAccomodationModel = new AccomodationTravelViewModel();
            objAccomodationModel.NominationModel = new NominationModel();
            objAccomodationModel.TravelDetailModel = new List<TravelDetailsModel>();
            objAccomodationModel.NominationModel.CourseID = CourseID;
            if (ds == null && ds1 == null)
            {
                return View();
            }
            else
            {
                if (ds.Tables[0].Rows[0]["AccomodationFromDate"].ToString() == "")
                {
                    objAccomodationModel.NominationModel.AccomodationFromDate = null;
                }
                else
                {
                    objAccomodationModel.NominationModel.AccomodationFromDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["AccomodationFromDate"]);
                }
                if (ds.Tables[0].Rows[0]["AccomodationToDate"].ToString() == "")
                {
                    objAccomodationModel.NominationModel.AccomodationToDate = null;
                }
                else
                {
                    objAccomodationModel.NominationModel.AccomodationToDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["AccomodationToDate"]);
                }

                if (ds.Tables[0].Rows[0]["IsAccomodationTrainer"].ToString() != "0" && ds.Tables[0].Rows[0]["IsAccomodationTrainer"].ToString() != "")
                {
                    objAccomodationModel.NominationModel.IsAccomodationTrainer = true;
                }
                else
                {
                    objAccomodationModel.NominationModel.IsAccomodationTrainer = false;
                }

                if (ds.Tables[0].Rows[0]["IsTraveDetails"].ToString() != "0" && ds.Tables[0].Rows[0]["IsTraveDetails"].ToString() != "")
                {
                    objAccomodationModel.NominationModel.IsTravelDetailsTrainer = true;
                }
                else
                {
                    objAccomodationModel.NominationModel.IsTravelDetailsTrainer = false;
                }
                if (ds.Tables[0].Rows[0]["IsFoodTrainer"].ToString() != "0" && ds.Tables[0].Rows[0]["IsFoodTrainer"].ToString() != "")
                {
                    objAccomodationModel.NominationModel.IsFoodTrainer = true;
                }
                else
                {
                    objAccomodationModel.NominationModel.IsFoodTrainer = false;
                }

                if (ds.Tables[0].Rows[0]["IsFoodParticipants"].ToString() != "0" && ds.Tables[0].Rows[0]["IsFoodParticipants"].ToString() != "")
                {
                    objAccomodationModel.NominationModel.IsFoodParticipants = true;
                }
                else
                {
                    objAccomodationModel.NominationModel.IsFoodParticipants = false;
                }
                objAccomodationModel.NominationModel.TrainerPreference = ds.Tables[0].Rows[0]["TrainerPreference"].ToString();
                objAccomodationModel.NominationModel.ParticipantsPreference = ds.Tables[0].Rows[0]["ParticipantsPreference"].ToString();
                objAccomodationModel.NominationModel.CommentsFoodAccomodaion = ds.Tables[0].Rows[0]["CommentsFoodAccomodaion"].ToString();
                //if (ds.Tables[0].Rows[0]["IsAccomodationTrainer"].ToString() == "1")
                //{
                //    objAccomodationModel.NominationModel.IsAccomodationTrainer = true;
                //}
                //else
                //{
                //    objAccomodationModel.NominationModel.IsAccomodationTrainer = false;
                //}


                for (int i = 0; i < 4; i++)
                {
                    TravelDetailsModel objTravelDetailModel = new TravelDetailsModel();
                    if (i >= ds1.Tables[0].Rows.Count)
                    {
                        objTravelDetailModel.FromLocation = null;
                        objTravelDetailModel.ToLocation = null;
                        objTravelDetailModel.Date = null;
                        //objTravelDetailModel.TravelDetailID = null;
                    }
                    else
                    {
                        objTravelDetailModel.FromLocation = ds1.Tables[0].Rows[i]["FromLocation"].ToString();
                        objTravelDetailModel.ToLocation = ds1.Tables[0].Rows[i]["ToLocation"].ToString();
                        objTravelDetailModel.Date = Convert.ToDateTime(ds1.Tables[0].Rows[i]["Date"]);
                        objTravelDetailModel.TravelDetailID = Convert.ToInt32(ds1.Tables[0].Rows[i]["TravelDetailsID"]);
                    }
                    objAccomodationModel.TravelDetailModel.Add(objTravelDetailModel);
                }


                return View(objAccomodationModel);
            }

        }

        [HttpPost]
        public ActionResult MailToAdmin(AccomodationTravelViewModel objAccomodationTravelViewModel, FormCollection FC)
        {
            int update_status = 0, insert_status = 0;

            NominationModel objNominationModel = new NominationModel();
            //objNominationModel.CourseID = ViewBag.CourseID;
            objNominationModel.CourseID = objAccomodationTravelViewModel.NominationModel.CourseID;

            objNominationModel.IsAccomodationTrainer = objAccomodationTravelViewModel.NominationModel.IsAccomodationTrainer;
            objNominationModel.AccomodationFromDate = objAccomodationTravelViewModel.NominationModel.AccomodationFromDate;
            objNominationModel.AccomodationToDate = objAccomodationTravelViewModel.NominationModel.AccomodationToDate;
            objNominationModel.IsTravelDetailsTrainer = objAccomodationTravelViewModel.NominationModel.IsTravelDetailsTrainer;
            objNominationModel.IsFoodTrainer = objAccomodationTravelViewModel.NominationModel.IsFoodTrainer;
            objNominationModel.IsFoodParticipants = objAccomodationTravelViewModel.NominationModel.IsFoodParticipants;
            objNominationModel.ParticipantsPreference = objAccomodationTravelViewModel.NominationModel.ParticipantsPreference;
            objNominationModel.TrainerPreference = objAccomodationTravelViewModel.NominationModel.TrainerPreference;
            objNominationModel.CommentsFoodAccomodaion = objAccomodationTravelViewModel.NominationModel.CommentsFoodAccomodaion;

            update_status = _service.SaveAccomodationAndFoodDetails(objNominationModel);

            List<TravelDetailsModel> lstTravelDetailModel = new List<TravelDetailsModel>();

            TravelDetailsModel objTravelDetailModel = new TravelDetailsModel();

            objTravelDetailModel.CourseID = objAccomodationTravelViewModel.NominationModel.CourseID;

            foreach (var lst in objAccomodationTravelViewModel.TravelDetailModel)
            {
                //objTravelDetailModel.CourseID = ViewBag.CourseID;
                //objTravelDetailModel.CourseID = objAccomodationTravelViewModel.NominationModel.CourseID;
                objTravelDetailModel.Date = lst.Date;
                objTravelDetailModel.FromLocation = lst.FromLocation;
                objTravelDetailModel.ToLocation = lst.ToLocation;
                objTravelDetailModel.TravelDetailID = lst.TravelDetailID;
                //lstTravelDetailModel.Add(objTravelDetailModel);

                if (objTravelDetailModel.FromLocation != null && objTravelDetailModel.ToLocation != null)
                {
                    insert_status = _service.SaveTravelDetails(objTravelDetailModel);
                }
                else
                {
                    insert_status = 0;
                }

            }

            if (update_status >= 1)
            {   //Update Successfull
                if (objAccomodationTravelViewModel.NominationModel.IsSendMail)
                {
                    EmailHelper.SendMailToAdmin(_service.GetAccomodationDetailsByCourseID(objAccomodationTravelViewModel.NominationModel.CourseID), _service.GetTravelDetailsByCourseID(objAccomodationTravelViewModel.NominationModel.CourseID));
                    //give message that data saved successfully and mail sent successfully
                    TempData[CommonConstants.Message] = "Accomodation and Food details saved successfully and mail is sent to Admin department. ";
                    //ViewData[CommonConstants.Result] = TempData[CommonConstants.Message];
                }
                else
                {
                    //give message that data saved succeessfully
                    TempData[CommonConstants.Message] = "Accomodation and Food details saved successfully.";
                    //ViewData[CommonConstants.Result] = TempData[CommonConstants.Message];
                }
                //return RedirectToAction("ViewSubmitNominationForTraining", "Nomination"); //redirect to confirm nomination page
                //EmailHelper.SendMailToAdmin(_service.GetAccomodationDetailsByCourseID(objNominationModel.CourseID));
                return RedirectToAction("ViewConfirmNominationForTraining", "Nomination"); //redirect to confirm nomination page
            }

            return RedirectToAction("ViewConfirmNominationForTraining", "Nomination");
        }

        public ActionResult Cancel()
        {
            //return Confirm Nomination Page
            return RedirectToAction("ViewConfirmNominationForTraining", "Nomination");
        }

        public ActionResult TrainingEffectiveness(string TrainingTypeID)
        {
            NominationModel objNominationmodel = new NominationModel();
            if(String.IsNullOrEmpty(TrainingTypeID))
            {
                TrainingTypeID = Convert.ToString(CommonConstants.TechnicalTrainingID);
            }
            if (!String.IsNullOrEmpty(Convert.ToString(Session["EmpID"])))
            {
                objNominationmodel.LoginEmpId = Convert.ToInt32(Session["EmpID"]);
            }

            // Changed by : Venkatesh  : Start
            if (Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
            {
                ArrayList arrRolesForUser = new ArrayList();
                arrRolesForUser = (ArrayList)Session[AuthorizationManagerConstants.AZMAN_ROLES];
                if (arrRolesForUser.Contains(CommonConstants.AdminRole))
                    objNominationmodel.RoleName = "Admin";
                else if (arrRolesForUser.Contains(CommonConstants.ManagerRole))
                    objNominationmodel.RoleName = "Manager";
            }
            // Changed by : Venkatesh  : End
            
            //if (!String.IsNullOrEmpty(Convert.ToString(Session["_RoleName"])))
            //{
            //    objNominationmodel.RoleName = Convert.ToString(Session["_RoleName"]);
            //}

            objNominationmodel.TrainingName = new SelectList(_service.GetTrainingNameforEffectiveness(Convert.ToInt32(objNominationmodel.LoginEmpId), Convert.ToString(objNominationmodel.RoleName), Convert.ToInt32(TrainingTypeID)), "Key", "Value");

            return View(objNominationmodel);
        }

        public ActionResult TrainingEffectiveness_Radio(string TrainingTypeID)
        {
            NominationModel objNominationmodel = new NominationModel();

            if (!String.IsNullOrEmpty(Convert.ToString(Session["EmpID"])))
            {
                objNominationmodel.LoginEmpId = Convert.ToInt32(Session["EmpID"]);
            }
            // Changed by : Venkatesh  : Start
            if (Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
            {
                ArrayList arrRolesForUser = new ArrayList();
                arrRolesForUser = (ArrayList)Session[AuthorizationManagerConstants.AZMAN_ROLES];
                if (arrRolesForUser.Contains(CommonConstants.AdminRole))
                    objNominationmodel.RoleName = "Admin";
                else if (arrRolesForUser.Contains(CommonConstants.ManagerRole))
                    objNominationmodel.RoleName = "Manager";
            }
            // Changed by : Venkatesh  : End

            //if (!String.IsNullOrEmpty(Convert.ToString(Session["_RoleName"])))
            //{
            //    objNominationmodel.RoleName = Convert.ToString(Session["_RoleName"]);
            //}

            objNominationmodel.TrainingName = new SelectList(_service.GetTrainingNameforEffectiveness(Convert.ToInt32(objNominationmodel.LoginEmpId), Convert.ToString(objNominationmodel.RoleName), Convert.ToInt32(TrainingTypeID)), "Key", "Value");

            objNominationmodel.TrainingTypeID = Convert.ToInt32(TrainingTypeID);

            return Json(objNominationmodel, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult ViewTrainingEffectiveness(int trainingnameid)
        {
            NominationModel objNominationmodel = new NominationModel();

            objNominationmodel.ListPreTrainingRating = new SelectList(_service.FillMasterDropDownList(CommonConstants.ProficiencyLevel), "Value", "Text");

            objNominationmodel.ListEmployeeList = CommonRepository.FillEmployeesList();

            if (!String.IsNullOrEmpty(Convert.ToString(Session["EmpID"])))
            {
                objNominationmodel.LoginEmpId = Convert.ToInt32(Session["EmpID"]);
            }

            // Changed by : Venkatesh  : Start
            if (Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
            {
                ArrayList arrRolesForUser = new ArrayList();
                arrRolesForUser = (ArrayList)Session[AuthorizationManagerConstants.AZMAN_ROLES];
                if (arrRolesForUser.Contains(CommonConstants.AdminRole))
                    objNominationmodel.RoleName = "Admin";
                else if (arrRolesForUser.Contains(CommonConstants.ManagerRole))
                    objNominationmodel.RoleName = "Manager";
            }
            // Changed by : Venkatesh  : End          

            objNominationmodel.employeeListModel = _service.GetTrainingEffectiveness(trainingnameid, objNominationmodel.LoginEmpId, CommonConstants.DefaultFlagZero, objNominationmodel.RoleName);

            if (objNominationmodel.employeeListModel.Count > 0)
            {
                objNominationmodel.PostRatingDueDate = objNominationmodel.employeeListModel[0].PostRatingDueDate;
                objNominationmodel.TrainingTypeID = objNominationmodel.employeeListModel[0].TrainingTypeID;

                //Start
                //For Send Mail TO All Input Button
                //If Post taining are filled by manager then button will showing for RMO group
                objNominationmodel.SendMailToAll = true;
                for (int i = 0; i < objNominationmodel.employeeListModel.Count; i++)
                {
                    if (objNominationmodel.employeeListModel[i].TrainingEffectivenessFlag == Convert.ToInt32(CommonConstants.DefaultFlagZero))
                    {
                        objNominationmodel.SendMailToAll = false;
                    }
                }
                //End
            }

            return PartialView("_ViewTrainingEffectiveness", objNominationmodel);
        }

        public PartialViewResult ViewSoftTrainingEffectiveness(int trainingnameid)
        {
            NominationModel objNominationmodel = new NominationModel();
            objNominationmodel.ListEmployeeList = CommonRepository.FillEmployeesList();

            objNominationmodel.ListObjectiveMet = new SelectList(_service.FillObjectiveMetDropDownList(), "Value", "Text");
           

            if (!String.IsNullOrEmpty(Convert.ToString(Session["EmpID"])))
            {
                objNominationmodel.LoginEmpId = Convert.ToInt32(Session["EmpID"]);
            }
            // Changed by : Venkatesh  : Start
            if (Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
            {
                ArrayList arrRolesForUser = new ArrayList();
                arrRolesForUser = (ArrayList)Session[AuthorizationManagerConstants.AZMAN_ROLES];
                if (arrRolesForUser.Contains(CommonConstants.AdminRole))
                    objNominationmodel.RoleName = "Admin";
                else if (arrRolesForUser.Contains(CommonConstants.ManagerRole))
                    objNominationmodel.RoleName = "Manager";
            }
            // Changed by : Venkatesh  : End            

            objNominationmodel.employeeListModel = _service.GetTrainingEffectiveness(trainingnameid, objNominationmodel.LoginEmpId, CommonConstants.DefaultFlagZero, Convert.ToString(objNominationmodel.RoleName));

            if (objNominationmodel.employeeListModel.Count > 0)
            {
                objNominationmodel.PostRatingDueDate = objNominationmodel.employeeListModel[0].PostRatingDueDate;
                objNominationmodel.TrainingTypeID = objNominationmodel.employeeListModel[0].TrainingTypeID;

                //Start
                //For Send Mail TO All Input Button
                //If Post taining are filled by manager then button will showing for RMO group
                objNominationmodel.SendMailToAll = true;
                for (int i = 0; i < objNominationmodel.employeeListModel.Count; i++)
                {
                    if (objNominationmodel.employeeListModel[i].TrainingEffectivenessFlag == Convert.ToInt32(CommonConstants.DefaultFlagZero))
                    {
                        objNominationmodel.SendMailToAll = false;
                    }
                }
                //End
            }
            return PartialView("_ViewSoftTrainingEffectiveness", objNominationmodel);
        }

        //[HttpPost]
        //public ActionResult ViewTrainingEffectiveness(string Command, bool IsManagerUpdated)
        //{
        //    return;
        //}

        [HttpPost]
        public ActionResult ViewTrainingEffectiveness(NominationModel model, string Command)
        {
            //if (ModelState.IsValid)
            //{
                if (!String.IsNullOrEmpty(Convert.ToString(Session["EmpID"])))
                {
                    model.LoginEmpId = Convert.ToInt32(Session["EmpID"]);
                }
                // Changed by : Venkatesh  : Start
                if (Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
                {
                    ArrayList arrRolesForUser = new ArrayList();
                    arrRolesForUser = (ArrayList)Session[AuthorizationManagerConstants.AZMAN_ROLES];
                    if (arrRolesForUser.Contains(CommonConstants.AdminRole))
                        model.RoleName = "Admin";
                    else if (arrRolesForUser.Contains(CommonConstants.ManagerRole))
                        model.RoleName = "Manager";
                }
                // Changed by : Venkatesh  : End  
                //if (!String.IsNullOrEmpty(Convert.ToString(Session["_RoleName"])))
                //{
                //    model.RoleName = Convert.ToString(Session["_RoleName"]);
                //}


                if (Command.ToLower() == CommonConstants.Send)
                {
                    string output = string.Empty;
                    //Neelam : 25/07/2017 Starts. IssueId 60562
                    for (int i = 0; i < model.employeeListModel.Count; i++)
                    {
                        if (model.employeeListModel[i].IsAdminGroup == true)
                        {
                            result = _service.UpdateTrainingNominatorEmpID(model.employeeListModel[i].CourseID, model.employeeListModel[i].EmpId, model.employeeListModel[i].PostNominatorNameID);
                        }
                    }
                    //Neelam : 25/07/2017 end. IssueId 60562
                    DataSet ds = _service.CheckInActiveManagerForTrainingEffectivness(Convert.ToInt32(model.employeeListModel[0].CourseID));

                    if (Convert.ToString(ds.Tables[0].Rows[0]["InActiveManagerName"]) == Convert.ToString(CommonConstants.DefaultFlagZero))
                    {
                        output = _service.SendTrainingEffectiveness(model);

                        if (output == CommonConstants.DefaultFlagZero.ToString())
                        {
                            EmailHelper.SendMailToManagerForPostTrainingRatining(Convert.ToInt32(model.LoginEmpId), _service.SendMailToManagerForPostTrainingRatining(Convert.ToInt32(model.employeeListModel[0].CourseID)), model.SendMailToNewManager);

                            message = "Post training effectiveness sent to the nominators.";
                        }
                    }
                    else
                    {
                        //Neelam : 25/07/2017 start. IssueId 60562
                        message = Convert.ToString(ds.Tables[0].Rows[0]["InActiveManagerName"]) + " is in inActive.Kindly select the respective checkbox for which manager has to be updated";
                        //Neelam : 25/07/2017 end. IssueId 60562
                    }
                    TempData[CommonConstants.Result] = string.Format(CommonConstants.StringFormat, "", message);

                    // return PartialView("ViewTrainingEffectiveness", Convert.ToInt32(model.employeeListModel[0].CourseID));
                }
                else if (Command.ToLower() == CommonConstants.Submit)
                {
                    string Empid = string.Empty;

                    result = _service.UpdateTrainingEffectiveness(model);


                    for (int i = 0; i < model.employeeListModel.Count; i++)
                    {
                        if (model.employeeListModel[i].IsAdminGroup == true)
                        {
                            Empid += Convert.ToString(model.employeeListModel[i].EmpId) + ",";
                        }
                    }

                    if (result == CommonConstants.DefaultFlagZero)
                    {
                        EmailHelper.ManagerFilledPostTrainingRating(Convert.ToString(model.RoleName), _service.ManagerFilledPostTrainingRating(Convert.ToInt32(model.employeeListModel[0].CourseID), Convert.ToInt32(model.LoginEmpId), Empid, Convert.ToString(model.RoleName)));

                        message = "Post training rating submitted.";
                    }

                    TempData[CommonConstants.Result] = string.Format(CommonConstants.StringFormat, "", message);
                }
                else if (Command.ToLower() == CommonConstants.SendMailToAll.ToLower())
                {
                    EmailHelper.SendEffectivenessMailToAll(Convert.ToInt32(model.LoginEmpId), _service.SendMailToManagerForPostTrainingRatining(Convert.ToInt32(model.employeeListModel[0].CourseID)));
                }
           // }
            return RedirectToAction("TrainingEffectiveness", model);
        }
    }
}
