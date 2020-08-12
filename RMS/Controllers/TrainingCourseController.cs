using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Infrastructure;
using RMS.Common.BusinessEntities;
using RMS.Common.BusinessEntities.Common;
using RMS.Common.Constants;
using RMS.Helpers;
using RMS.ModelBinder;
using RMS.Models;
using Services.Interfaces;

namespace RMS.Controllers
{
    [CheckAccess]
    public class TrainingCourseController : ErrorController
    {
        //
        // GET: /TrainingCourse/
        private readonly ITrainingService _service;

        /// <summary>
        /// Constructor for creating reference for Training repository
        /// </summary>
        /// <value>Interface TrainingModel</value>
        public TrainingCourseController(ITrainingService service)
        {
            _service = service;
        }
        
        public ActionResult TrainingCourseGrid(string trainingTypeID, int courseStatusId)
        {
            int TrainingTypeID = Convert.ToInt32(trainingTypeID);
            TempData["trainingTypeID"] = TrainingTypeID;
            List<CourseDetails> lstCourse = _service.GetCoursesByTrainingTypeId(TrainingTypeID, courseStatusId);
            ViewData["courseStatus"] = courseStatusId;
            return View(CommonConstants.ViewTrainingCourseGrid, lstCourse);
        }

        public ActionResult RaiseRequestGrid(string trainingNameID)
        {
            int TrainingNameID = Convert.ToInt32(trainingNameID);
            RaiseRequestViewModel raiseModel = new RaiseRequestViewModel();
            if (TrainingNameID > 0)
            {
                List<TrainingModel> lstRaiseRequest = _service.GetApprovedRaiseTrainings(TrainingNameID,CommonConstants.ApprovedTraining);
                raiseModel = new RaiseRequestViewModel(lstRaiseRequest, new string[] { }, CommonConstants.EditMode);
            }
            return View(CommonConstants.RaiseRequestGrid, raiseModel);
        }

        [HttpGet]
        public ActionResult ListCourse()
        {
            string strTrainingType = Request.QueryString["trainingTypeID"];
            string CourseStatus = Request.QueryString["CourseStatus"];
          
            //ViewBag.qsTrainingTypeID = strTrainingType;
            ViewCourseViewModel objViewCourseModel;
            if (String.IsNullOrEmpty(strTrainingType) && String.IsNullOrEmpty(CourseStatus))
            {
                objViewCourseModel = new ViewCourseViewModel();
                TempData["trainingTypeID"] = CommonConstants.TechnicalTrainingID;
                objViewCourseModel.CourseDetails = _service.GetCoursesByTrainingTypeId(CommonConstants.TechnicalTrainingID,1);
            }
            else if (!String.IsNullOrEmpty(strTrainingType) && !String.IsNullOrEmpty(CourseStatus))
            {
                TempData["trainingTypeID"] = strTrainingType;
               
                objViewCourseModel = new ViewCourseViewModel();
                objViewCourseModel.CourseDetails = _service.GetCoursesByTrainingTypeId(Convert.ToInt32(strTrainingType),Convert.ToInt32(CourseStatus));
                objViewCourseModel.TrainingStatusID = Convert.ToInt32(CourseStatus);
            }
           
            else
            {
                TempData["trainingTypeID"] = strTrainingType;
                objViewCourseModel = new ViewCourseViewModel(strTrainingType);
                objViewCourseModel.CourseDetails = _service.GetCoursesByTrainingTypeId(Convert.ToInt32(strTrainingType),1);
            }
            //objViewCourseModel.QsTrainingTypeID = strTrainingType;
            return View(objViewCourseModel);
        }

        [HttpPost]
        public ActionResult Responsearr(string trainingTypeID, string CourseID, string CourseStatus)
        {
            TempData["passedArray"] = CourseID;

            return Json(new { ok = true, newurl = Url.Action("ListCourse", "TrainingCourse", new { trainingTypeID = trainingTypeID, CourseStatus = CourseStatus }) });
            // var redirectUrl = new UrlHelper(Request.RequestContext).Action("ViewTechnicalTrainingRequest","Training");
            //return Json(new { Url = redirectUrl });

        }

        [HttpGet]
        public ActionResult CreateCourse()
        {
            TrainingCourseViewModel objCourseDetails = new TrainingCourseViewModel();
            return View(objCourseDetails);
        }

        [HttpPost]
        public ActionResult CreateCourse(TrainingCourseViewModel course, IEnumerable<HttpPostedFileBase> fileCourseContents, HttpPostedFileBase fileDAR, IEnumerable<HttpPostedFileBase> fileTrainerProfiles,HttpPostedFileBase fileInvoice)
        {
            bool valFail = false;
            bool IsSuccess = false;
            TrainingCourseModel objCourse = new TrainingCourseModel();
            List<FileDetails> filelist = new List<FileDetails>();
            if (course.TrainingModeID == CommonConstants.InternalTrainer)
            {
                ModelState["VendorID"].Errors.Clear();
                ModelState["VendorEmailId"].Errors.Clear();
                ModelState["TotalCost"].Errors.Clear();
                ModelState["PaymentDueDt"].Errors.Clear();
                ModelState["PaymentMadeID"].Errors.Clear();
                ModelState["PaymentModeID"].Errors.Clear();
                ModelState["TrainerName"].Errors.Clear();
            }
            
            if (ModelState.IsValid)
            {
                //if(String.IsNullOrEmpty(course.RaiseTrainingIds)){
                //    ModelState.AddModelError(String.Empty, "Please select Raise Trainings");
                //}
                //if (course.TrainingStartDate > course.TrainingEndDate)
                //{
                //    ModelState.AddModelError(String.Empty, "Training Start Date should not be greater than Training End Date.");
                //}
                // Save Training Course - Set View Model values
                objCourse.TrainingTypeID = course.TrainingTypeID;
                objCourse.TrainingNameID = course.TrainingNameID;
                objCourse.TrainingModeID = course.TrainingModeID;
                objCourse.VendorID = course.VendorID;
                objCourse.VendorEmailId = course.VendorEmailId;
                objCourse.TechnicalPanelIds = "";
                objCourse.TrainerName = string.IsNullOrEmpty(course.TrainerName) ? "" : course.TrainerName;
                objCourse.TrainerNameInternalID = course.TrainerNameInternalID;
                objCourse.TrainingStartDate = Convert.ToDateTime(course.TrainingStartDate);
                objCourse.TrainingEndDate = Convert.ToDateTime(course.TrainingEndDate);
                objCourse.TrainingComments = course.TrainingComments;
                objCourse.NoOfdays = course.NoOfdays;
                objCourse.TotalTrainigHours = course.TotalTrainingHours;
                objCourse.NominationTypeID = course.NominationTypeID;
                objCourse.SoftwareDetails = course.SoftwareDetails;
                objCourse.TotalCost = course.TotalCost;
                objCourse.RequestedBy = course.RequestedBy;
                objCourse.RequestedFor = course.RequestedFor;
                objCourse.TrainingLocation = course.TrainingLocation;

                objCourse.CourseName = course.CourseName;

                if (Convert.ToString(course.TrainingModeID) != "")
                {
                    objCourse.PaymentDueDt = course.PaymentDueDt;
                    objCourse.PaymentMade = (course.PaymentMadeID == Convert.ToInt32(CommonConstants.PaymentMadeYes))?true:false;
                    objCourse.IndividualPayementTraining = course.IndividualPayementTraining;
                    objCourse.PaymentDates = course.PaymentDates;
                    objCourse.PaymentComments = course.PaymentComments;
                    objCourse.PaymentModeID = course.PaymentModeID;

                    // Save Training Course - Set View Model values : DAR Form
                    if (fileInvoice != null && fileInvoice.ContentLength > 0)
                    {
                        FileDetails fileInfo = new FileDetails();

                        fileInfo.Category = "Invoice";

                        var fileName = Path.GetFileNameWithoutExtension(fileInvoice.FileName);
                        string extension = Path.GetExtension(fileInvoice.FileName);

                        fileInfo.FileName = fileName;
                        fileInfo.FileGuid = Guid.NewGuid() + extension;

                        var path = Path.Combine(Server.MapPath("~/RMS_Files/InvoiceForm"), fileInfo.FileGuid);
                        fileInvoice.SaveAs(path);

                        filelist.Add(fileInfo);
                        
                    }
                }

                // Save Training Course - Set View Model values :Course contents
                //foreach (HttpPostedFileBase item in fileUpload)
                //{
                //    if (Array.Exists(course.FilesToBeUploaded.Split(','), s => s.Equals(item.FileName)))
                //    {
                //        //Save or do your action -  Each Attachment ( HttpPostedFileBase item )
                //    }
                //}
                string strCourseContent = "";
                foreach (var file in fileCourseContents)
                {
                    if (file!= null && file.ContentLength > 0)
                    {
                        var fileName = Guid.NewGuid() + "_" + Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/RMS_Files/CourseContent"), fileName);
                        file.SaveAs(path);
                        strCourseContent += (fileName + "|");
                    }
                }
                objCourse.CourseContentFiles = (strCourseContent.Length>0)?strCourseContent.Remove((strCourseContent.Length-1),1):strCourseContent;

                // Save Training Course - Set View Model values : DAR Form
                if (fileDAR != null && fileDAR.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid() + "_" + Path.GetFileName(fileDAR.FileName);
                    var path = Path.Combine(Server.MapPath("~/RMS_Files/DARForm"), fileName);
                    fileDAR.SaveAs(path);
                    objCourse.DARFormFileName = fileName;
                }

                // Save Training Course - Set View Model values :Trainer Profile
                string strTrainerProfile = "";
                foreach (var file in fileTrainerProfiles )
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Guid.NewGuid() + "_" + Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/RMS_Files/TrainerProfile"), fileName);
                        file.SaveAs(path);
                        strTrainerProfile += (fileName + "|");
                    }
                }
                objCourse.TrainerProfileFiles = (strTrainerProfile.Length > 0) ? strTrainerProfile.Remove((strTrainerProfile.Length - 1), 1) : strTrainerProfile;

                // Save Training Course - Set View Model values : Training Effectiveness
                var selectedEff = new List<Effectiveness>();
                var postedEffIds = new string[0];
                if (course.PostedEffectiveness == null) course.PostedEffectiveness = new PostedEffectiveness();

                if (course.PostedEffectiveness.EffectivenessIds != null && course.PostedEffectiveness.EffectivenessIds.Any())
                {
                    postedEffIds = course.PostedEffectiveness.EffectivenessIds;
                }

                TrainingRepository objTrainingRepository = new TrainingRepository();
                if (postedEffIds.Any())
                {
                    
                    selectedEff = CommonRepository.GetMasterTrainingEffectivenessDetails()
                        .Where(x => postedEffIds.Any(s => x.EffectivenessID.ToString().Equals(s))).ToList();
                    
                }
                course.AllEffectivenessDetails = CommonRepository.GetMasterTrainingEffectivenessDetails();
                course.SelectedEffectiveness = selectedEff;
                course.PostedEffectiveness = course.PostedEffectiveness;

                objCourse.EffectivenessIds = "";
                foreach (Effectiveness eff in course.SelectedEffectiveness)
                {
                    objCourse.EffectivenessIds += (eff.EffectivenessID + ",");
                }
                if (objCourse.EffectivenessIds.Length > 0) { objCourse.EffectivenessIds = objCourse.EffectivenessIds.Remove((objCourse.EffectivenessIds.Length - 1), 1); }
                objCourse.TechnicalPanelIds = course.TechnicalPanelID;
                objCourse.RaiseTrainingIds = course.RaiseTrainingIds;
                objCourse.FileDetails = filelist;

                // Save Training Course - Set course details for postback
                string[] selRaiseIds = course.RaiseTrainingIds.Split(new char[] { ',' });
                RaiseRequestViewModel raiseRequestModel = new RaiseRequestViewModel(_service.GetApprovedRaiseTrainings(course.TrainingNameID,CommonConstants.ApprovedTraining), selRaiseIds,CommonConstants.EditMode);
                course.RaiseRequestDetails = raiseRequestModel ;
                course.TrainingNameDetails = new SelectList(_service.GetApprovedTrainingNameByTrainingType(course.TrainingTypeID, true, Convert.ToString(course.TrainingNameID)), "Value", "Text");
                if (!ModelState.IsValid)
                {
                    return View(course);
                }
                // Save Training Course - Insert into Database
                if (!valFail)
                {
                    int courseId = _service.SaveTrainingCourse(objCourse);
                    if (courseId > 0 )
                    {
                        if (objCourse.FileDetails != null)
                        {
                            foreach (var item in objCourse.FileDetails)
                            {
                                objCourse.CourseID = courseId;
                                int saveStatus = _service.SaveFileDetails(objCourse);
                            }
                        }
                        IsSuccess = true;
                    }
                }
            }
            else
            {
                return View(course);
            }

            if (IsSuccess)
            {
                TempData["Message"] = "Course created successfully";
                return RedirectToAction("ListCourse", new { trainingTypeID = objCourse.TrainingTypeID });
            }
            else
            {
                return View(course);
            }
        }

        [HttpGet]
        public ActionResult EditCourse(string pcourseId, string pmode = "",string courseStatus="")
        {
            int courseId = Convert.ToInt32(CheckAccessAttribute.Decode(pcourseId));
            string mode = CheckAccessAttribute.Decode(pmode);
            ViewData["courseStatus"] = courseStatus;
            TrainingCourseModel course = _service.GetTrainingCourses(courseId);
            //TrainingCourseViewModel objCourseDetails = new TrainingCourseViewModel();
            TrainingCourseViewModel objCourseDetails = new TrainingCourseViewModel(course.TrainingNameID.ToString(), course.TrainingName, course.TrainingTypeID.ToString());
            if (mode == "vW")
                objCourseDetails.PageMode = CommonConstants.ViewMode;
            else
                objCourseDetails.PageMode = CommonConstants.EditMode;
            objCourseDetails.CourseID = course.CourseID;
            objCourseDetails.TrainingTypeID = course.TrainingTypeID;
            objCourseDetails.TrainingNameID = course.TrainingNameID;
            objCourseDetails.TrainingModeID = course.TrainingModeID;
            objCourseDetails.VendorID = course.VendorID;
            objCourseDetails.VendorEmailId = course.VendorEmailId;
            objCourseDetails.CourseContentFileDetails = new FileUploadModel(CommonRepository.GetFileDetailList(CommonConstants.CourseContentDir, (course.CourseContentFiles == null ? "" : course.CourseContentFiles)), "CourseContent", course.CourseID.ToString(), "divCourseContent", CommonConstants.CourseContentDir);
            string tempDAR = (course.DARFormFileName == null) ? "" : course.DARFormFileName;
            objCourseDetails.DARFormFileDetails = new FileUploadModel(CommonRepository.GetFileDetailList(CommonConstants.CourseDARDir, tempDAR), "CourseDAR", course.CourseID.ToString(), "divCourseDAR", CommonConstants.CourseDARDir);
            objCourseDetails.TrainerProfileFileDetails = new FileUploadModel(CommonRepository.GetFileDetailList(CommonConstants.CourseTrainerDir, (course.TrainerProfileFiles == null ? "" : course.TrainerProfileFiles)), "CourseTrainer", course.CourseID.ToString(), "divCourseTrainer", CommonConstants.CourseTrainerDir);
            if (objCourseDetails.PageMode == CommonConstants.EditMode)
            {
                bool deleteFlag = false;
                if (course.TrainingEndDate >= DateTime.Now) { deleteFlag = true; }
                objCourseDetails.CourseContentFileDetails.DeleteFlag = objCourseDetails.DARFormFileDetails.DeleteFlag = objCourseDetails.TrainerProfileFileDetails.DeleteFlag = deleteFlag;
            }
            objCourseDetails.TrainerName = course.TrainerName;
            objCourseDetails.TrainingStartDate = course.TrainingStartDate;
            objCourseDetails.TrainingEndDate = course.TrainingEndDate;
            objCourseDetails.TrainingComments = course.TrainingComments;
            objCourseDetails.NoOfdays = course.NoOfdays;
            objCourseDetails.TotalTrainingHours = course.TotalTrainigHours;
            objCourseDetails.NominationTypeID = course.NominationTypeID;
            if (!String.IsNullOrWhiteSpace(course.EffectivenessIds))
            {
                string[] tempEff = course.EffectivenessIds.Split(new char[] { ',' });
                List<Effectiveness> selEffs = new List<Effectiveness>();
                foreach (string e in tempEff)
                {
                    Effectiveness eff = (Effectiveness)objCourseDetails.AllEffectivenessDetails.First(x => x.EffectivenessID == Convert.ToInt32(e));
                    selEffs.Add(eff);
                }
                objCourseDetails.SelectedEffectiveness = selEffs;
            }
            objCourseDetails.SoftwareDetails = course.SoftwareDetails;
            objCourseDetails.TotalCost = course.TotalCost;
            objCourseDetails.RequestedBy = course.RequestedBy;
            objCourseDetails.RequestedFor = course.RequestedFor;
            objCourseDetails.TrainingLocation = course.TrainingLocation;
            objCourseDetails.CourseName = course.CourseName;
            objCourseDetails.TrainerNameInternal = course.TrainerNameInternal;
            objCourseDetails.TrainerNameInternalID = course.TrainerNameInternalID;
            objCourseDetails.PaymentDueDt = course.PaymentDueDt;
            objCourseDetails.PaymentMadeID = (course.PaymentMade == true) ? Convert.ToInt32(CommonConstants.PaymentMadeYes) : Convert.ToInt32(CommonConstants.PaymentMadeNo);
            objCourseDetails.IndividualPayementTraining = course.IndividualPayementTraining;
            objCourseDetails.PaymentDates = course.PaymentDates;
            objCourseDetails.PaymentComments = course.PaymentComments;
            objCourseDetails.PaymentModeID = course.PaymentModeID;
            objCourseDetails.TechnicalPanelID = course.TechnicalPanelIds;
            objCourseDetails.TechnicalPanelName = course.TechnicalPanelNames;
            objCourseDetails.RaiseTrainingIds = course.RaiseTrainingIds;
            objCourseDetails.FeedbackSent = course.FeedbackSent;

            //Harsha Issue Id- 58975 and 58958 - Start       
            objCourseDetails.TrainingStatus = course.TrainingStatus;
            //Harsha Issue Id- 58975 and 58958 - End

            if (course.FileDetails.Count > 0)
            {
                objCourseDetails.InvoiceFileDetails = new FileUploadModel(CommonRepository.GetFileDetailList(CommonConstants.CourseInvoiceDir, course.FileDetails, "Invoice", true), "CourseInvoice", course.CourseID.ToString(), "divCourseInvoice", CommonConstants.CourseInvoiceDir);
            }
            else
            {
                objCourseDetails.InvoiceFileDetails = new FileUploadModel(CommonRepository.GetFileDetailList(CommonConstants.CourseInvoiceDir, null, "Invoice", false), "CourseInvoice", course.CourseID.ToString(), "divCourseInvoice", CommonConstants.CourseInvoiceDir);
            }
            if (objCourseDetails.PageMode == CommonConstants.EditMode)
            {
                objCourseDetails.InvoiceFileDetails.DeleteFlag = true;
            }
            string[] selRaiseIds = course.RaiseTrainingIds.Split(new char[] { ',' });
            string Raisemode = CommonConstants.ViewMode;
            if (objCourseDetails.PageMode == CommonConstants.EditMode)
            {
                if (DateTime.Now.Date <= course.TrainingStartDate && course.LastDateOfNomination == null)
                {
                    Raisemode = CommonConstants.EditMode;
                    RaiseRequestViewModel raiseRequestModel = new RaiseRequestViewModel(_service.GetRaiseTrainings(course.RaiseTrainingIds, objCourseDetails.TrainingNameID), selRaiseIds, Raisemode);
                    objCourseDetails.RaiseRequestDetails = raiseRequestModel;
                }
                else
                {
                    RaiseRequestViewModel raiseRequestModel = new RaiseRequestViewModel(_service.GetRaiseTrainingsById(course.RaiseTrainingIds), selRaiseIds, Raisemode);
                    objCourseDetails.RaiseRequestDetails = raiseRequestModel;
                }
            }
            else
            {
                RaiseRequestViewModel raiseRequestModel = new RaiseRequestViewModel(_service.GetRaiseTrainingsById(course.RaiseTrainingIds), selRaiseIds, Raisemode);
                objCourseDetails.RaiseRequestDetails = raiseRequestModel;
            }
        
            int i = 0;
            foreach (var report in objCourseDetails.objCommonModel)
            {
                string[] values = objCourseDetails.TechnicalPanelID.Split(',');
                for (int a = 0; a < values.Length; a++)
                {
                    if (values[a].Trim() == report.EmpID)
                    {
                        objCourseDetails.objCommonModel[i].Checked = true;
                        break;
                    }
                }
                i++;
            }


            return View(objCourseDetails);
        }

        [HttpPost]
        public JsonResult doesCourseNameExists(TrainingCourseModel objTrainingCourse)
        {
            //string duplicateState = string.Empty;
            var duplicateState = _service.GetDuplicateStatusOfCoursName(objTrainingCourse.CourseName);
            return Json(duplicateState == "0");
        }

        
        [HttpPost]
        public ActionResult EditCourse(TrainingCourseViewModel course, IEnumerable<HttpPostedFileBase> fileCourseContents, HttpPostedFileBase fileDAR, IEnumerable<HttpPostedFileBase> fileTrainerProfiles,HttpPostedFileBase fileInvoice)
        {
            bool IsSuccess = false;
            bool valFail = false;
            int cId = (course.CourseID == null) ? 0 : Convert.ToInt32(course.CourseID);
            TrainingCourseModel objCourse = _service.GetTrainingCourses(cId);
            string effectivenessIds = string.Empty;
            List<Effectiveness> selectedEff = new List<Effectiveness>();
            List<FileDetails> filelist = new List<FileDetails>();

            //Harsha- Issue Id- 58975 & 58958 - Start
            //Description- Making Training Cost Editable for Internal Training after Closed status of the training
            //Disabling Number of Hours and Days field once the after training's status is feedback sent
            if (objCourse.TrainingModeID == CommonConstants.InternalTrainer)
            {
                if (objCourse.TrainingStatus == 1235)
                {
                    IsSuccess = _service.UpdateTrainingCourseTotalCost(objCourse.CourseID, course.TotalCost);
                    return View(course);
                }
                //Harsha- Issue Id- 58975 & 58958 - End
            }            
            
                
                EvalEffecivenessDetails(course.PostedEffectiveness, out effectivenessIds, out selectedEff);
                if (course.TrainingModeID == CommonConstants.InternalTrainer)
                {
                    ModelState["VendorID"].Errors.Clear();
                    ModelState["VendorEmailId"].Errors.Clear();
                    ModelState["TotalCost"].Errors.Clear();
                    ModelState["PaymentDueDt"].Errors.Clear();
                    ModelState["PaymentMadeID"].Errors.Clear();
                    ModelState["PaymentModeID"].Errors.Clear();
                    ModelState["TrainerName"].Errors.Clear();

                }

                if (ModelState.IsValid)
                {
                    //if (String.IsNullOrEmpty(course.RaiseTrainingIds))
                    //{
                    //    ModelState.AddModelError(String.Empty, "Please select Raise Trainings.");
                    //    valFail = true;
                    //}
                    //if (course.TrainingStartDate > course.TrainingEndDate)
                    //{
                    //    ModelState.AddModelError(String.Empty, "Training Start Date should not be greater than Training End Date.");
                    //    valFail = true;
                    //}
                    //return View();
                    // Save Training Course - Set View Model values
                    objCourse.CourseID = (course.CourseID == null) ? 0 : Convert.ToInt32(course.CourseID);
                    objCourse.TrainingTypeID = course.TrainingTypeID;
                    objCourse.TrainingNameID = course.TrainingNameID;
                    objCourse.TrainingModeID = course.TrainingModeID;
                    objCourse.VendorID = course.VendorID;
                    objCourse.VendorEmailId = course.VendorEmailId;
                    objCourse.TechnicalPanelIds = course.TechnicalPanelID;
                    objCourse.TrainerName = string.IsNullOrEmpty(course.TrainerName) ? "" : course.TrainerName;
                    objCourse.TrainingStartDate = Convert.ToDateTime(course.TrainingStartDate);
                    objCourse.TrainingEndDate = Convert.ToDateTime(course.TrainingEndDate);
                    objCourse.TrainingComments = course.TrainingComments;
                    objCourse.NoOfdays = course.NoOfdays;
                    objCourse.TotalTrainigHours = course.TotalTrainingHours;
                    objCourse.NominationTypeID = course.NominationTypeID;
                    objCourse.SoftwareDetails = course.SoftwareDetails;
                    objCourse.TotalCost = course.TotalCost;
                    objCourse.RequestedBy = course.RequestedBy;
                    objCourse.RequestedFor = course.RequestedFor;
                    objCourse.TrainingLocation = course.TrainingLocation;
                    objCourse.TrainerNameInternalID = course.TrainerNameInternalID;

                    objCourse.CourseName = course.CourseName;
                    //Rakesh: To Include Payment Dates
                    objCourse.PaymentDates = course.PaymentDates;
                    if (course.TrainingModeID == Convert.ToInt32(CommonConstants.ExternalTrainer))
                    {
                        objCourse.PaymentDueDt = course.PaymentDueDt;
                        objCourse.PaymentMade = (course.PaymentMadeID == Convert.ToInt32(CommonConstants.PaymentMadeYes)) ? true : false;
                        objCourse.IndividualPayementTraining = course.IndividualPayementTraining;

                        objCourse.PaymentComments = course.PaymentComments;
                        objCourse.PaymentModeID = course.PaymentModeID;

                        // Save Training Course - Set View Model values : Invoice Form
                        if (fileInvoice != null && fileInvoice.ContentLength > 0)
                        {
                            FileDetails fileInfo = new FileDetails();

                            fileInfo.Category = "Invoice";

                            var fileName = Path.GetFileNameWithoutExtension(fileInvoice.FileName);
                            string extension = Path.GetExtension(fileInvoice.FileName);

                            fileInfo.FileName = fileName;
                            fileInfo.FileGuid = Guid.NewGuid() + extension;

                            var path = Path.Combine(Server.MapPath("~/RMS_Files/InvoiceForm"), fileInfo.FileGuid);
                            fileInvoice.SaveAs(path);

                            filelist.Add(fileInfo);

                        }
                    }
                    else
                    {
                        objCourse.PaymentDueDt = null;
                        objCourse.PaymentMade = false;
                        objCourse.IndividualPayementTraining = false;
                        // objCourse.PaymentDates = null;
                        objCourse.PaymentComments = null;
                        objCourse.PaymentModeID = 0;
                        objCourse.TrainerProfileFiles = "";
                        objCourse.DARFormFileName = "";
                        objCourse.TechnicalPanelIds = "";
                        objCourse.TechnicalPanelNames = "";

                    }

                    // Save Training Course - Set View Model values :Course contents
                    //foreach (HttpPostedFileBase item in fileUpload)
                    //{
                    //    if (Array.Exists(course.FilesToBeUploaded.Split(','), s => s.Equals(item.FileName)))
                    //    {
                    //        //Save or do your action -  Each Attachment ( HttpPostedFileBase item )
                    //    }
                    //}
                    string strCourseContent = "";
                    foreach (var file in fileCourseContents)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Guid.NewGuid() + "_" + Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/RMS_Files/CourseContent"), fileName);
                            file.SaveAs(path);
                            strCourseContent += (fileName + "|");
                        }
                    }
                    //if (strCourseContent.Length > 0) { strCourseContent.Remove((strCourseContent.Length - 1), 1); }
                    //objCourse.CourseContentFiles += (!String.IsNullOrWhiteSpace(strCourseContent)) ? ("|" + strCourseContent) : strCourseContent;
                    //objCourse.CourseContentFiles += (!String.IsNullOrWhiteSpace(strCourseContent)) ? ("|" + strCourseContent.Remove((strCourseContent.Length - 1), 1)) : strCourseContent;
                    if (!String.IsNullOrWhiteSpace(strCourseContent))
                    {
                        if (String.IsNullOrWhiteSpace(objCourse.CourseContentFiles)) { objCourse.CourseContentFiles += strCourseContent.Remove((strCourseContent.Length - 1), 1); }
                        else { objCourse.CourseContentFiles += ("|" + strCourseContent.Remove((strCourseContent.Length - 1), 1)); }
                    }
                    course.CourseContentFileDetails = new FileUploadModel(CommonRepository.GetFileDetailList(CommonConstants.CourseContentDir, (objCourse.CourseContentFiles == null ? "" : objCourse.CourseContentFiles)), "CourseContent", course.CourseID.ToString(), "divCourseContent", CommonConstants.CourseContentDir);
                    // Save Training Course - Set View Model values : DAR Form
                    if (fileDAR != null && fileDAR.ContentLength > 0)
                    {
                        var fileName = Guid.NewGuid() + "_" + Path.GetFileName(fileDAR.FileName);
                        var path = Path.Combine(Server.MapPath("~/RMS_Files/DARForm"), fileName);
                        fileDAR.SaveAs(path);
                        objCourse.DARFormFileName = fileName;
                    }
                    string tempDAR = (!String.IsNullOrWhiteSpace(objCourse.DARFormFileName)) ? "" : objCourse.DARFormFileName;
                    course.DARFormFileDetails = new FileUploadModel(CommonRepository.GetFileDetailList(CommonConstants.CourseDARDir, tempDAR), "CourseDAR", course.CourseID.ToString(), "divCourseDAR", CommonConstants.CourseDARDir);
                    // Save Training Course - Set View Model values : Trainer Profile
                    string strTrainerProfile = "";
                    foreach (var file in fileTrainerProfiles)
                    {
                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Guid.NewGuid() + "_" + Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/RMS_Files/TrainerProfile"), fileName);
                            file.SaveAs(path);
                            strTrainerProfile += (fileName + "|");
                        }
                    }
                    //if (strTrainerProfile.Length > 0) { strTrainerProfile.Remove((strTrainerProfile.Length - 1), 1); }
                    //objCourse.TrainerProfileFiles += (!String.IsNullOrWhiteSpace(strTrainerProfile)) ? ("|" + strTrainerProfile) : strTrainerProfile;
                    //objCourse.TrainerProfileFiles += (!String.IsNullOrWhiteSpace(strTrainerProfile)) ? ("|" + strTrainerProfile.Remove((strTrainerProfile.Length - 1), 1)) : strTrainerProfile;
                    if (!String.IsNullOrWhiteSpace(strTrainerProfile))
                    {
                        if (String.IsNullOrWhiteSpace(objCourse.TrainerProfileFiles)) { objCourse.TrainerProfileFiles += strTrainerProfile.Remove((strTrainerProfile.Length - 1), 1); }
                        else { objCourse.TrainerProfileFiles += ("|" + strTrainerProfile.Remove((strTrainerProfile.Length - 1), 1)); }
                    }
                    course.TrainerProfileFileDetails = new FileUploadModel(CommonRepository.GetFileDetailList(CommonConstants.CourseTrainerDir, (objCourse.TrainerProfileFiles == null ? "" : objCourse.TrainerProfileFiles)), "CourseTrainer", course.CourseID.ToString(), "divCourseTrainer", CommonConstants.CourseTrainerDir);
                    // Save Training Course - Set View Model values : Training Effectiveness
                    //var selectedEff = new List<Effectiveness>();
                    //var postedEffIds = new string[0];
                    //if (course.PostedEffectiveness == null) course.PostedEffectiveness = new PostedEffectiveness();

                    //if (course.PostedEffectiveness.EffectivenessIds != null && course.PostedEffectiveness.EffectivenessIds.Any())
                    //{
                    //    postedEffIds = course.PostedEffectiveness.EffectivenessIds;
                    //}

                    //TrainingRepository objTrainingRepository = new TrainingRepository();
                    //if (postedEffIds.Any())
                    //{

                    //    selectedEff = CommonRepository.GetMasterTrainingEffectivenessDetails()
                    //        .Where(x => postedEffIds.Any(s => x.EffectivenessID.ToString().Equals(s))).ToList();

                    //}
                    //course.AllEffectivenessDetails = CommonRepository.GetMasterTrainingEffectivenessDetails();
                    //course.SelectedEffectiveness = selectedEff;
                    //course.PostedEffectiveness = course.PostedEffectiveness;

                    //objCourse.EffectivenessIds = "";
                    //foreach (Effectiveness eff in course.SelectedEffectiveness)
                    //{
                    //    objCourse.EffectivenessIds += (eff.EffectivenessID + ",");
                    //}
                    //if (objCourse.EffectivenessIds.Length > 0) { objCourse.EffectivenessIds = objCourse.EffectivenessIds.Remove((objCourse.EffectivenessIds.Length - 1), 1); }

                    objCourse.TechnicalPanelIds = course.TechnicalPanelID;
                    //string temp = objCourse.RaiseTrainingIds;

                    var unmappedRequests = objCourse.RaiseTrainingIds.Split(',').Except(course.RaiseTrainingIds.Split(',')).ToList();
                    var mappedRequests = course.RaiseTrainingIds.Split(',').Except(objCourse.RaiseTrainingIds.Split(',')).ToList();

                    objCourse.RaiseTrainingIds = course.RaiseTrainingIds;
                    string unmappedRaiseIDs = string.Empty;
                    string mappedRaiseIDs = string.Empty;

                    foreach (var item in unmappedRequests)
                    {
                        unmappedRaiseIDs += item + ',';
                    }

                    foreach (var item in mappedRequests)
                    {
                        mappedRaiseIDs += item + ',';
                    }
                    objCourse.EffectivenessIds = effectivenessIds;
                    objCourse.FileDetails = filelist;
                    // Save Training Course - Insert into Database

                    if (!valFail)
                    {
                        int courseId = _service.UpdateTrainingCourse(objCourse, unmappedRaiseIDs, mappedRaiseIDs);

                        if (courseId > 0)
                        {
                            if (objCourse.FileDetails != null)
                            {
                                foreach (var item in objCourse.FileDetails)
                                {
                                    objCourse.CourseID = courseId;
                                    int saveStatus = _service.UpdateFileDetails(objCourse);
                                }
                            }
                            IsSuccess = true;
                        }
                        //int savetStatus = _service.UpdateTrainingCourse(objCourse);
                        //if (savetStatus == 1) IsSuccess = true;
                    }
                }
            

            if (IsSuccess)
            {
                TempData["Message"] = "Course updated successfully";
                return RedirectToAction("ListCourse", new { trainingTypeID = objCourse.TrainingTypeID });
            }
            else
            {
                course.PageMode = CommonConstants.EditMode;
                course.TrainingTypeID = objCourse.TrainingTypeID;
                course.TrainingNameID = course.TrainingNameID;
                course.SelectedEffectiveness = selectedEff;
                course.CourseContentFileDetails = new FileUploadModel(CommonRepository.GetFileDetailList(CommonConstants.CourseContentDir, (objCourse.CourseContentFiles == null ? "" : objCourse.CourseContentFiles)), "CourseContent", course.CourseID.ToString(), "divCourseContent", CommonConstants.CourseContentDir);
                string tempDAR = (objCourse.DARFormFileName == null) ? "" : objCourse.DARFormFileName;
                course.DARFormFileDetails = new FileUploadModel(CommonRepository.GetFileDetailList(CommonConstants.CourseDARDir, tempDAR), "CourseDAR", course.CourseID.ToString(), "divCourseDAR", CommonConstants.CourseDARDir);
                course.TrainerProfileFileDetails = new FileUploadModel(CommonRepository.GetFileDetailList(CommonConstants.CourseTrainerDir, (objCourse.TrainerProfileFiles == null ? "" : objCourse.TrainerProfileFiles)), "CourseTrainer", course.CourseID.ToString(), "divCourseTrainer", CommonConstants.CourseTrainerDir);
                if (course.PageMode == CommonConstants.EditMode)
                {
                    course.CourseContentFileDetails.DeleteFlag = course.DARFormFileDetails.DeleteFlag = course.TrainerProfileFileDetails.DeleteFlag = true;
                }
                if(String.IsNullOrWhiteSpace(course.RaiseTrainingIds))
                {
                    string[] selRaiseIds = new string[] { };
                    RaiseRequestViewModel raiseRequestModel = new RaiseRequestViewModel(_service.GetRaiseTrainings(objCourse.RaiseTrainingIds, course.TrainingNameID), selRaiseIds, CommonConstants.EditMode);
                    course.RaiseRequestDetails = raiseRequestModel;
                }
                else
                {
                    string[] selRaiseIds = course.RaiseTrainingIds.Split(new char[] { ',' });
                    RaiseRequestViewModel raiseRequestModel = new RaiseRequestViewModel(_service.GetRaiseTrainings(objCourse.RaiseTrainingIds, course.TrainingNameID), selRaiseIds, CommonConstants.EditMode);
                    course.RaiseRequestDetails = raiseRequestModel;
                }
                course.TrainingNameDetails = new SelectList(_service.GetApprovedTrainingNameByTrainingType(course.TrainingTypeID, true, Convert.ToString(course.TrainingNameID)), "Value", "Text");

                return View(course);
            }

        }

        [HttpPost]
        public ActionResult UpdatePaymentDetails(CoursePaymentViewModel course, [ModelBinder(typeof(CoursePaymentModelBinder))]TrainingCourseModel objCourse, HttpPostedFileBase fileInvoice)
        {
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                //commented by Rakesh to save Internal Course Cost,Payment Date
               // if (course.hdnTrainingMode == CommonConstants.ExternalTrainer)
                {
                    int updateStatus = _service.UpdateCoursePayment(objCourse);

                    //Ishwar Patil 11052016 Desc: Invoice file not uploading in folder and dabase
                    if (updateStatus == 1) 
                    {
                        List<FileDetails> filelist = new List<FileDetails>();

                        if (fileInvoice != null && fileInvoice.ContentLength > 0)
                        {
                            //uploaded file store in invoice folder.
                            FileDetails fileInfo = new FileDetails();

                            fileInfo.Category = "Invoice";

                            var fileName = Path.GetFileNameWithoutExtension(fileInvoice.FileName);
                            string extension = Path.GetExtension(fileInvoice.FileName);

                            fileInfo.FileName = fileName;
                            fileInfo.FileGuid = Guid.NewGuid() + extension;

                            var path = Path.Combine(Server.MapPath("~/RMS_Files/InvoiceForm"), fileInfo.FileGuid);
                            fileInvoice.SaveAs(path);

                            filelist.Add(fileInfo);

                            objCourse.FileDetails = filelist;

                            //Uploaded file name store in database.
                            if (objCourse.FileDetails != null)
                            {
                                foreach (var item in objCourse.FileDetails)
                                {
                                    int saveStatus = _service.UpdateFileDetails(objCourse);
                                }
                            }

                        }
                    //Ishwar Patil 11052016 Desc: Invoice file not uploading in folder and dabase
                        return RedirectToAction("ListCourse"); 
                    }
                }
            }
            else
            {
                return RedirectToAction("EditCourse", new { courseId = course.CourseID });
            }
            //return RedirectToAction("EditCourse", new { courseId = course.CourseID });
            return View(course);
        }

        [HttpGet]
        public ActionResult DeleteCourse(int courseId)
        {
            int deleteStatus = 0;
            deleteStatus = _service.DeleteTrainingCourse(courseId);
            if (deleteStatus == 1)
                return Json(true, JsonRequestBehavior.AllowGet);
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CloseCourse(int courseId, string prompt)
        {
            int closeStatus = 0;
            closeStatus = _service.CloseTrainingCourse(courseId, prompt);
            return Json(closeStatus, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadTrainingName(string traingType)
        {
            IEnumerable<SelectListItem> TrainingNameData = new SelectList(CommonRepository.GetEmptySelectList("0"), "Value", "Text");
            if (traingType != "0")
            {
                TrainingNameData = new SelectList(_service.GetApprovedTrainingNameByTrainingType(Convert.ToInt32(traingType), true,""), "Value", "Text");
            }
            return Json(TrainingNameData, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadTrainingEffectiveness(string traingType)
        {
            List<Effectiveness> AllEffectivenessDetails = new List<Effectiveness>();
            if (traingType == CommonConstants.TechnicalTrainingID.ToString())
                AllEffectivenessDetails = CommonRepository.GetMasterTrainingEffectivenessDetails().Where(x => x.EffectivenessName != "Pre Rating").ToList();
            else if (traingType == CommonConstants.SoftSkillsTrainingID.ToString())
                AllEffectivenessDetails = CommonRepository.GetMasterTrainingEffectivenessDetails().Where(x => x.EffectivenessName != "Objective").ToList();

            return Json(AllEffectivenessDetails, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public String GetVendorEmailID(string vendorId)
        {
            Vendor vendor = _service.GetVendorDetails(Convert.ToInt32(vendorId));
            string vendorEmailId = vendor.VendorEmail;
            return vendorEmailId;
        }

        private void EvalEffecivenessDetails(PostedEffectiveness objPostedEff, out string effectivenessIds, out List<Effectiveness> selectedEff)
        {
            effectivenessIds = String.Empty;
            selectedEff = new List<Effectiveness>();
            // Save Training Course - Set View Model values : Training Effectiveness
            //var selectedEff = new List<Effectiveness>();
            var postedEffIds = new string[0];
            if (objPostedEff == null) objPostedEff = new PostedEffectiveness();

            if (objPostedEff.EffectivenessIds != null && objPostedEff.EffectivenessIds.Any())
            {
                postedEffIds = objPostedEff.EffectivenessIds;
            }

            TrainingRepository objTrainingRepository = new TrainingRepository();
            if (postedEffIds.Any())
            {

                selectedEff = CommonRepository.GetMasterTrainingEffectivenessDetails()
                    .Where(x => postedEffIds.Any(s => x.EffectivenessID.ToString().Equals(s))).ToList();

            }
            //List<Effectiveness> AllEffectivenessDetails = CommonRepository.GetMasterTrainingEffectivenessDetails();
            //course.SelectedEffectiveness = selectedEff;
            //course.PostedEffectiveness = course.PostedEffectiveness;

            //objCourse.EffectivenessIds = "";
            
            foreach (Effectiveness eff in selectedEff)
            {
                effectivenessIds += (eff.EffectivenessID + ",");
            }
            if (effectivenessIds.Length > 0) { effectivenessIds = effectivenessIds.Remove((effectivenessIds.Length - 1), 1); }
        }

        [HttpGet]
        public ActionResult UpdateFileDetail(string module, string entityId, string fileName, string fullFile, string targetId, string dir)
        {
            FileUploadModel objFileUploadModel = null;
            string filePath = _service.GetFileDetailsByCourseId(module, entityId);
            string newFilePath = String.Empty;
            if (!String.IsNullOrWhiteSpace(filePath))
            {
                string[] arrFile = filePath.Split(new char[]{'|'});
                for (int i = 0; i < arrFile.Length; i++)
                {
                    if (arrFile[i] != fileName)
                    {
                        newFilePath += (arrFile[i] + "|");
                    }
                }
                if (newFilePath.Length > 0)
                {
                    newFilePath = newFilePath.Substring(0, newFilePath.Length - 1);
                }
                int updateFileStatus = _service.UpdateFileDetails(module, entityId, newFilePath);
                string fullFilePath = @Server.MapPath(fullFile);
                if (System.IO.File.Exists(fullFilePath))
                {
                    System.IO.File.Delete(fullFilePath);
                }
                objFileUploadModel = new FileUploadModel(CommonRepository.GetFileDetailList(dir, Convert.ToString(newFilePath)), module, entityId, targetId, dir);
                objFileUploadModel.DeleteFlag = true;
            }

            return PartialView(CommonConstants.FileHelperView, objFileUploadModel);
        }

        [HttpGet]
        public ActionResult UpdateInvoiceDetail(string fileId, string module, string entityId, string fileName, string fullFile, string targetId, string dir)
        {
            //TrainingCourseModel model = new TrainingCourseModel();
            //model.InvoiceCategory = module;
            //model.CourseID = Convert.ToInt32(entityId);
            //model.InvoiceFileName = fileName;
            ////int updateFileStatus = _service.UpdateInvoiceFileDetails(module, entityId);
            ////string fullFilePath = @Server.MapPath(fullFile);
            ////if (System.IO.File.Exists(fullFilePath))
            ////{
            ////    System.IO.File.Delete(fullFilePath);
            ////}
            //return PartialView(CommonConstants.FileHelperView,on
            //return View();

            int updateFileStatus = _service.updateInvoiceDetails(fileId, module, entityId, fileName);
            string fullFilePath = @Server.MapPath(fullFile);
            if (System.IO.File.Exists(fullFilePath))
            {
                System.IO.File.Delete(fullFilePath);
            }

            FileUploadModel objFileUploadModel = null;
            TrainingCourseModel course = _service.GetInvoiceDetailsByCourseId(module, entityId);
            string newFilePath = String.Empty;
            module = (module == "CourseInvoice" ? "Invoice" : module);

            if (course.FileDetails.Count > 0)
            {
                objFileUploadModel = new FileUploadModel(CommonRepository.GetFileDetailList(CommonConstants.CourseInvoiceDir, course.FileDetails, "Invoice", true), "CourseInvoice", course.CourseID.ToString(), "divCourseInvoice", CommonConstants.CourseInvoiceDir);
                
            }
            else
            {
                objFileUploadModel = new FileUploadModel(CommonRepository.GetFileDetailList(CommonConstants.CourseInvoiceDir, null, "Invoice", false), "CourseInvoice", course.CourseID.ToString(), "divCourseInvoice", CommonConstants.CourseInvoiceDir);
            }
            objFileUploadModel.DeleteFlag = true;

           
            return PartialView(CommonConstants.FileHelperView, objFileUploadModel);
            //if (!String.IsNullOrWhiteSpace(filePath))
            //{
            //    string[] arrFile = filePath.Split(new char[] { '|' });
            //    for (int i = 0; i < arrFile.Length; i++)
            //    {
            //        if (arrFile[i] != fileName)
            //        {
            //            newFilePath += (arrFile[i] + "|");
            //        }
            //    }
            //    if (newFilePath.Length > 0)
            //    {
            //        newFilePath = newFilePath.Substring(0, newFilePath.Length - 1);
            //    }
            //    int updateFileStatus = _service.updateInvoiceDetails(fileId,module, entityId, fileName);
            //    string fullFilePath = @Server.MapPath(fullFile);
            //    if (System.IO.File.Exists(fullFilePath))
            //    {
            //        System.IO.File.Delete(fullFilePath);
            //    }
            //    objFileUploadModel = new FileUploadModel(CommonRepository.GetFileDetailList(dir, Convert.ToString(newFilePath)), module, entityId, targetId, dir);
            //    objFileUploadModel.DeleteFlag = true;
            //}

            //return PartialView(CommonConstants.FileHelperView, objFileUploadModel);
        }

        [HttpGet]
        public ActionResult InviteNomination(string pcourseId,string courseStatus)
        {
            int courseId = Convert.ToInt32(CheckAccessAttribute.Decode(pcourseId));
            ViewData["courseStatus"] = courseStatus;
            TempData.Peek("trainingTypeID");
            InviteNominationModel nomination = _service.GetInviteNominationDetails(courseId);
            InviteNominationViewModel invitationViewModel = new InviteNominationViewModel(nomination);
            return View(invitationViewModel);

        }

        [HttpPost]
        public ActionResult InviteNomination(InviteNominationViewModel inviteModel)
        {
            DateTime nominationDt = (DateTime)inviteModel.NominationEndDate;
            
            if (ModelState.IsValid)
            {
                TrainingCourseModel course = _service.GetTrainingCourses(inviteModel.CourseID);
                int updateStatus = _service.UpdateCourseNominationDate(nominationDt, course.CourseID);

                if (updateStatus == 1)
                {
                    bool extend = false;
                    if (!(course.LastDateOfNomination == null))
                    {
                        int i = DateTime.Compare(nominationDt, (DateTime)course.LastDateOfNomination);
                        extend = (i == 0) ? false : true;
                    }

                    string trainingMode = (course.TrainingModeID == CommonConstants.InternalTrainer) ? "Internal" : "External";
                    string[] arrFilePath = new string[] { };
                    if (!String.IsNullOrWhiteSpace(course.CourseContentFiles))
                        arrFilePath = course.CourseContentFiles.Split(new char[] { '|' });

                    if (course.TrainingModeID == 1909)
                    {
                        EmailHelper.SendMailForInviteNomination(extend, course.CourseName, course.TrainerNameInternal, trainingMode, course.TrainingStartDate, course.TrainingEndDate, (Single)course.NoOfdays, nominationDt, arrFilePath, CommonConstants.CourseContentDir, course.NominationTypeID);
                    }
                    else if (course.TrainingModeID == 1910)
                    {
                        EmailHelper.SendMailForInviteNomination(extend, course.CourseName, course.TrainerName, trainingMode, course.TrainingStartDate, course.TrainingEndDate, (Single)course.NoOfdays, nominationDt, arrFilePath, CommonConstants.CourseContentDir, course.NominationTypeID);
                    }
                    return RedirectToAction("ListCourse", new { trainingTypeID = course.TrainingTypeID });
                }                
            }
            inviteModel = new InviteNominationViewModel(_service.GetInviteNominationDetails(inviteModel.CourseID));
            inviteModel.NominationEndDate = nominationDt;
            return View(inviteModel);

        }
    }
}
