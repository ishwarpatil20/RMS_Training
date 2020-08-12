using Domain.Entities;
using RMS.Common;
using RMS.Common.BusinessEntities.Training;
using RMS.Common.Constants;
using RMS.Helpers;
using RMS.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMS.Controllers
{
    [CheckAccess]
    public class AssessmentController :  ErrorController
    {

        private readonly ITrainingService _ITrainingService;
        private readonly IAssessmentService _IAssessmentService;

        /// <summary>
        /// Constructor for creating reference for Training service layer
        /// </summary>
        public AssessmentController(ITrainingService trainingService, IAssessmentService _assessmentService)
        {
            _ITrainingService = trainingService;
            _IAssessmentService = _assessmentService;
        }

        #region Action Methods
        public ActionResult CreateAssessment(string pcourseId)
        {
            int courseId = Convert.ToInt32(CheckAccessAttribute.Decode(pcourseId));
            AssessmentViewModel assessmentViewModel = GetAssessmentPaperByCourseId(courseId);
            return View("CreateAssessment", assessmentViewModel);

            //AssessmentPaperViewModel assessmentPaperViewModel = new AssessmentPaperViewModel();
            //assessmentPaperViewModel.TrainingCourses = LoadTrainingCourses();
            //return View("CreateAssessment", assessmentPaperViewModel);
        }

        [HttpPost]
        public ActionResult CreateAssessment(AssessmentViewModel assessmentPaperViewModel) //AssessmentPaperViewModel  assessmentPaperViewModel
        {
            int assessmentPaperId = 0;
            AssessmentPaperModel assessmentPaperModel = new AssessmentPaperModel();
            assessmentPaperModel.AssessmentPaperId = assessmentPaperViewModel.AssessmentPaper.AssessmentPaperId;
            assessmentPaperModel.CourseId = assessmentPaperViewModel.AssessmentPaper.CourseId;
            assessmentPaperModel.AssessmentDate = assessmentPaperViewModel.AssessmentPaper.AssessmentDate;
            assessmentPaperModel.TimeDuration = assessmentPaperViewModel.AssessmentPaper.TimeDuration;
            assessmentPaperModel.IsActive = true;
            assessmentPaperModel.CreatedOn = DateTime.Now;
            assessmentPaperModel.LastEditedOn = DateTime.Now;
            assessmentPaperId = _IAssessmentService.SaveAssessmentPaper(assessmentPaperModel);
            if (assessmentPaperId > 0)
            {
                string passessmentPaperId = CheckAccessAttribute.Encode(Convert.ToString(assessmentPaperId));
                return RedirectToAction("ShowAssessmentPaperDetails", new { Id = passessmentPaperId });
            }
            else
            {
                return View("Error");
            }
        }


        public ActionResult ShowAssessmentPaperDetails(string  id)
        {
            int assessmentPaperId = Convert.ToInt32(CheckAccessAttribute.Decode(id));
            AssessmentViewModel assessmentViewModel = GetAssessmentPaperById(assessmentPaperId);
            return View("ShowAssessmentPaperDetails", assessmentViewModel);
        }

        public ActionResult StartAssessment()
        {
            //AssessmentResultViewModel assessmentResultViewModel = new AssessmentResultViewModel();            
            //assessmentResultViewModel.TotalScore= 2;
            //assessmentResultViewModel.OutofScore = 3;
            //assessmentResultViewModel.Message = "dsdsdsdsdsd";
            //return View("Result", assessmentResultViewModel);
            AssessmentViewModel ObjSetAssessmentViewModel = new AssessmentViewModel();
            ObjSetAssessmentViewModel.ListCourses = LoadTrainingCourses("StartAssessment", Convert.ToInt32(Session["EmpID"]));
            return View("PaperDetails", ObjSetAssessmentViewModel);

        }
        public ActionResult GiveAssessment(int id)//public ActionResult Details(int id, int empId)
        {
            AssessmentViewModel assessmentViewModel = GetAssessmentPaperByCourseId(id);
            ViewData["EmployeeId"] = Convert.ToInt32(Session["EmpID"]);
            ViewData["PaperPrefix"] = "AssessmentViewModel.AssessmentPaper";
            ViewData["prefix"] = "AssessmentViewModel.AssessmentQuestions";
            return PartialView("_StartAssessment", assessmentViewModel);
        }       

        //public ActionResult GiveAssessment(int id, int empId)//public ActionResult Details(int id, int empId)
        //{

        //    AssessmentViewModel assessmentViewModel = GetAssessmentPaperById(id);
        //    ViewData["EmployeeId"] = empId;
        //    ViewData["PaperPrefix"] = "AssessmentViewModel.AssessmentPaper";
        //    ViewData["prefix"] = "AssessmentViewModel.AssessmentQuestions";
        //    return View("PaperDetails", assessmentViewModel);
        //}
        [HttpPost]
        public ActionResult Result(AssessmentViewModel assessmentViewModel, FormCollection objf)//(int empId, AssessmentViewModel assessmentViewModel)
        {
            AssessmentResultViewModel assessmentResultViewModel = new AssessmentResultViewModel();
            //assessmentResultViewModel.AssessmentPaperId = assessmentViewModel.AssessmentPaper.AssessmentPaperId;
            assessmentResultViewModel.AssessmentPaperId = Convert.ToInt32( objf["AssessmentViewModel.AssessmentQuestions[1].AssessmentPaperId"]);
            assessmentResultViewModel.AssessmentPaperId = Convert.ToInt32(objf["HfldAssessmentPaperId"]);
            
            assessmentResultViewModel.AssessmentDate = DateTime.Now;
            assessmentResultViewModel.EmployeeId = Convert.ToInt32(Session["EmpID"]);
            AssessmentResult assessmentResult = ConvertAssessmentResultViewModelToDomain(assessmentResultViewModel);
            DataTable dtResultDetails;
            dtResultDetails = new DataTable("AssessmentResultDetails");
            dtResultDetails.Columns.Add("AssessmentResultId", typeof(int));
            dtResultDetails.Columns.Add("QuestionId", typeof(int));
            dtResultDetails.Columns.Add("SelectedAnswer", typeof(string));
            dtResultDetails.Columns.Add("AnswerScore", typeof(int));
            dtResultDetails.Columns.Add("isactive", typeof(bool));
            dtResultDetails.Columns.Add("CreatedBy", typeof(int));
            dtResultDetails.Columns.Add("CreatedOn", typeof(DateTime));
            dtResultDetails.Columns.Add("LastEditedBy", typeof(int));
            dtResultDetails.Columns.Add("LastEditedOn", typeof(DateTime));

            //AssessmentViewModel 
            assessmentViewModel = GetAssessmentPaperById(assessmentResultViewModel.AssessmentPaperId);
            if (assessmentViewModel.AssessmentQuestions != null && assessmentViewModel.AssessmentQuestions.Count > 0)
            {
                foreach (var question in assessmentViewModel.AssessmentQuestions)
                {
                    string selectedAnswer = string.Empty;

                    if (question.IsMultiChoiceAnswer)
                    {

                        if(Convert.ToString (objf["Chk_AssessmentViewModel.AssessmentQuestions[" + question.QuestionId + "].Option1"]).Contains("true"))
                            selectedAnswer = selectedAnswer + " Option 1 ,";
                        if (Convert.ToString(objf["Chk_AssessmentViewModel.AssessmentQuestions[" + question.QuestionId + "].Option2"]).Contains("true"))
                            selectedAnswer = selectedAnswer + " Option 2 ,";
                        
                        if (!string.IsNullOrEmpty(question.Option3Description))
                        {
                            if (Convert.ToString(objf["Chk_AssessmentViewModel.AssessmentQuestions[" + question.QuestionId + "].Option3"]).Contains("true"))
                                selectedAnswer = selectedAnswer + " Option 3 ,";
                        }
                        
                        if (!string.IsNullOrEmpty(question.Option4Description))
                        {
                            if (Convert.ToString(objf["Chk_AssessmentViewModel.AssessmentQuestions[" + question.QuestionId + "].Option4"]).Contains("true"))
                                selectedAnswer = selectedAnswer + " Option 4 ,";
                        }

                        if (!string.IsNullOrEmpty(question.Option5Description))
                        {
                            if (Convert.ToString(objf["Chk_AssessmentViewModel.AssessmentQuestions[" + question.QuestionId + "].Option5"]).Contains("true"))
                                selectedAnswer = selectedAnswer + " Option 5 ,";
                        }
                        if (!string.IsNullOrEmpty(question.Option6Description))
                        {
                            if (Convert.ToString(objf["Chk_AssessmentViewModel.AssessmentQuestions[" + question.QuestionId + "].Option6"]).Contains("true"))
                                selectedAnswer = selectedAnswer + " Option 6 ,";
                        }
                    }
                    else
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(objf["Rbtn_AssessmentViewModel.AssessmentQuestions[" + question.QuestionId + "].Option"]))
                                selectedAnswer = objf["Rbtn_AssessmentViewModel.AssessmentQuestions[" + question.QuestionId + "].Option"];
                            
                        }
                        catch (Exception)
                        { }
                    }

                    selectedAnswer = selectedAnswer.TrimEnd(',');
                    if (selectedAnswer != "")
                    {
                        question.SelectedAnswer = selectedAnswer;
                        int score = 0;
                        score = CalculateScore(assessmentViewModel.AssessmentPaper.AssessmentPaperId, question.QuestionId, selectedAnswer, assessmentViewModel);
                        dtResultDetails.Rows.Add(1, question.QuestionId, question.SelectedAnswer, score, true, 1, DateTime.Now, 1, DateTime.Now);
                    }
                }
            }
            string strResult= _IAssessmentService.SaveAssessmentResult(dtResultDetails, assessmentResult);
            string [] strSplited= strResult.Split(',');
            assessmentResultViewModel.TotalScore = Convert.ToInt32( strSplited[0]);
            assessmentResultViewModel.OutofScore = assessmentViewModel.AssessmentQuestions.Count;
            assessmentResultViewModel.Message = Convert.ToString(strSplited[1]);
            return View("Result", assessmentResultViewModel);
        }

        private int CalculateScore(int paperId, int questionId, string selectedAnswer, AssessmentViewModel assessmentViewModel)
        {
            int score = 0;
            //AssessmentViewModel assessmentViewModel = GetAssessmentPaperById(paperId);
            List<string> lstSelectedAnswer =new List<string>( selectedAnswer.Split(','));
            if (assessmentViewModel.AssessmentQuestions != null && assessmentViewModel.AssessmentQuestions.Count > 0)
            {
                var question = assessmentViewModel.AssessmentQuestions.Where(m => m.QuestionId == questionId).First();
                List<string> lstCorrectAnswers = new List<string>();
                if (question.IsOption1Correct)
                {
                    lstCorrectAnswers.Add("Option 1");
                }

                if (question.IsOption2Correct)
                {
                    lstCorrectAnswers.Add("Option 2");
                }

                if (question.IsOption3Correct)
                {
                    lstCorrectAnswers.Add("Option 3");
                }

                if (question.IsOption4Correct)
                {
                    lstCorrectAnswers.Add("Option 4");
                }

                if (question.IsOption5Correct)
                {
                    lstCorrectAnswers.Add("Option 5");
                }

                if (question.IsOption6Correct)
                {
                    lstCorrectAnswers.Add("Option 6");
                }
                //var result = lstSelectedAnswer.Any(f => lstCorrectAnswers.Any(s => f == s));
                bool isCorrectAnswer = false;
                foreach (var answer in lstSelectedAnswer)
                {
                   // if (lstCorrectAnswers.Any(str=>str.Contains(answer)))
                        if (lstCorrectAnswers.Contains(answer.Trim()))
                    {
                        isCorrectAnswer = true;
                        break;
                    }

                }
                if (isCorrectAnswer)
                {
                    score = 1;
                }
            }
            return score;
        }

        private AssessmentResult ConvertAssessmentResultViewModelToDomain(AssessmentResultViewModel assessmentResultViewModel)
        {
            AssessmentResult assessmentResult = new AssessmentResult();
            assessmentResult.AssessmentPaperId = assessmentResultViewModel.AssessmentPaperId;
            assessmentResult.EmployeeId = assessmentResultViewModel.EmployeeId;
            assessmentResult.AssessmentDate = assessmentResultViewModel.AssessmentDate;
            assessmentResult.IsActive = true;
            assessmentResult.CreatedOn = DateTime.Now;
            assessmentResult.LastEditedOn = DateTime.Now;
            return assessmentResult;
        }
        public ActionResult AddAssessmentDetails(string id, string course, string trainingNameId, string submitButton)
        {
            int _id = Convert.ToInt32(CheckAccessAttribute.Decode(id));
            int _trainingNameId = Convert.ToInt32(CheckAccessAttribute.Decode(trainingNameId));
            int _course = Convert.ToInt32(CheckAccessAttribute.Decode(course));            

            switch (submitButton)
            {
                case "Add New Question":                    
                    return RedirectToAction("AddNewQuestion", new { id = id });
                case "Use Existing Assessment":
                    return RedirectToAction("UseExistingAssessment", new { id = id, course = course, trainingNameId = trainingNameId });
                default:
                    return (View());
            }
        }

        public ActionResult AddNewQuestion(string id)
        {
            int AssessId = Convert.ToInt32(CheckAccessAttribute.Decode(id));

            AssessmentViewModel assessmentViewModel = GetAssessmentPaperById(AssessId);
            List<AssessmentQuestionsViewModel> assessmentQuestions = new List<AssessmentQuestionsViewModel>();
            AssessmentQuestionsViewModel assessmentQuestionsViewModel = new AssessmentQuestionsViewModel();
            assessmentQuestions.Add(assessmentQuestionsViewModel);
            assessmentViewModel.AssessmentQuestions = assessmentQuestions;
            return View("AddNewQuestion", assessmentViewModel);
        }
                
        [HttpPost, ValidateInput(false)]
        public ActionResult AddNewQuestion(AssessmentViewModel assessmentViewModel)
        {
            int questionId = 0;
            if (assessmentViewModel.AssessmentQuestions != null && assessmentViewModel.AssessmentQuestions.Count == 1)
            {
                if (assessmentViewModel.AssessmentQuestions[0].QuestionImg != null && assessmentViewModel.AssessmentQuestions[0].QuestionImg.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid() + "_" + Path.GetFileName(assessmentViewModel.AssessmentQuestions[0].QuestionImg.FileName);
                    var path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings[CommonConstants.AssessmentQuestionImgPath].ToString()), fileName);
                    assessmentViewModel.AssessmentQuestions[0].QuestionImg.SaveAs(path);
                    assessmentViewModel.AssessmentQuestions[0].QuestionImgFileName = fileName;
                }

                if (assessmentViewModel.AssessmentQuestions[0].Option1Image != null && assessmentViewModel.AssessmentQuestions[0].Option1Image.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid() + "_" + Path.GetFileName(assessmentViewModel.AssessmentQuestions[0].Option1Image.FileName);
                    var path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings[CommonConstants.AssessmentAnswerImgPath].ToString()), fileName);
                    assessmentViewModel.AssessmentQuestions[0].Option1Image.SaveAs(path);
                    assessmentViewModel.AssessmentQuestions[0].Option1ImageFileName = fileName;
                }

                if (assessmentViewModel.AssessmentQuestions[0].Option2Image != null && assessmentViewModel.AssessmentQuestions[0].Option2Image.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid() + "_" + Path.GetFileName(assessmentViewModel.AssessmentQuestions[0].Option2Image.FileName);
                    var path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings[CommonConstants.AssessmentAnswerImgPath].ToString()), fileName);
                    assessmentViewModel.AssessmentQuestions[0].Option2Image.SaveAs(path);
                    assessmentViewModel.AssessmentQuestions[0].Option2ImageFileName = fileName;
                }

                if (assessmentViewModel.AssessmentQuestions[0].Option3Image != null && assessmentViewModel.AssessmentQuestions[0].Option3Image.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid() + "_" + Path.GetFileName(assessmentViewModel.AssessmentQuestions[0].Option3Image.FileName);
                    var path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings[CommonConstants.AssessmentAnswerImgPath].ToString()), fileName);
                    assessmentViewModel.AssessmentQuestions[0].Option3Image.SaveAs(path);
                    assessmentViewModel.AssessmentQuestions[0].Option3ImageFileName = fileName;
                }

                if (assessmentViewModel.AssessmentQuestions[0].Option4Image != null && assessmentViewModel.AssessmentQuestions[0].Option4Image.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid() + "_" + Path.GetFileName(assessmentViewModel.AssessmentQuestions[0].Option4Image.FileName);
                    var path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings[CommonConstants.AssessmentAnswerImgPath].ToString()), fileName);
                    assessmentViewModel.AssessmentQuestions[0].Option4Image.SaveAs(path);
                    assessmentViewModel.AssessmentQuestions[0].Option4ImageFileName = fileName;
                }

                if (assessmentViewModel.AssessmentQuestions[0].Option5Image != null && assessmentViewModel.AssessmentQuestions[0].Option5Image.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid() + "_" + Path.GetFileName(assessmentViewModel.AssessmentQuestions[0].Option5Image.FileName);
                    var path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings[CommonConstants.AssessmentAnswerImgPath].ToString()), fileName);
                    assessmentViewModel.AssessmentQuestions[0].Option5Image.SaveAs(path);
                    assessmentViewModel.AssessmentQuestions[0].Option5ImageFileName = fileName;
                }

                if (assessmentViewModel.AssessmentQuestions[0].Option6Image != null && assessmentViewModel.AssessmentQuestions[0].Option6Image.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid() + "_" + Path.GetFileName(assessmentViewModel.AssessmentQuestions[0].Option6Image.FileName);
                    var path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings[CommonConstants.AssessmentAnswerImgPath].ToString()), fileName);
                    assessmentViewModel.AssessmentQuestions[0].Option6Image.SaveAs(path);
                    assessmentViewModel.AssessmentQuestions[0].Option6ImageFileName = fileName;
                }

                AssessmentQuestionsModel assessmentQuestionsModel = ConvertAssessmentQuestionsViewModelToDomain(assessmentViewModel.AssessmentQuestions[0]);

                questionId = _IAssessmentService.SaveAssessmentQuestion(assessmentViewModel.AssessmentPaper.AssessmentPaperId, assessmentQuestionsModel);
                if (questionId > 0)
                {
                    return RedirectToAction("ShowAssessmentPaperDetails", new { Id = CheckAccessAttribute.Encode(Convert.ToString(assessmentViewModel.AssessmentPaper.AssessmentPaperId)) });
                }
                return View("Error");

            }
            return View("Error");
        }

        public ActionResult DeleteAssessmentQuestion(string questionId, string paperId)
        {
            int _questionId = Convert.ToInt32(CheckAccessAttribute.Decode(questionId));
            int _paperId = Convert.ToInt32(CheckAccessAttribute.Decode(paperId));

            int deleteStatus = 0;
            deleteStatus = _IAssessmentService.DeleteAssessmentQuestion(_questionId, _paperId);
            if (deleteStatus == 1)
                return Json(true, JsonRequestBehavior.AllowGet);
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditAssessmentQuestion( string  questionId, string paperId)
        {
            int _questionId = Convert.ToInt32(CheckAccessAttribute.Decode(questionId));
            int _paperId = Convert.ToInt32(CheckAccessAttribute.Decode(paperId));
            AssessmentModel assessmentModel = _IAssessmentService.GetAssessmentQuestionDetails(_paperId, _questionId);
            AssessmentViewModel assessmentViewModel = ConvertAssessmentQuestionsModelToViewModel(assessmentModel);
            return View("EditAssessmentQuestion", assessmentViewModel);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EditAssessmentQuestion(AssessmentViewModel assessmentViewModel)
        {
            int updateSuccess = 0;

            if (assessmentViewModel.AssessmentQuestions != null && assessmentViewModel.AssessmentQuestions.Count == 1)
            {
                if (assessmentViewModel.AssessmentQuestions[0].QuestionImg != null && assessmentViewModel.AssessmentQuestions[0].QuestionImg.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid() + "_" + Path.GetFileName(assessmentViewModel.AssessmentQuestions[0].QuestionImg.FileName);
                    var path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings[CommonConstants.AssessmentQuestionImgPath].ToString()), fileName);
                    assessmentViewModel.AssessmentQuestions[0].QuestionImg.SaveAs(path);
                    assessmentViewModel.AssessmentQuestions[0].QuestionImgFileName = fileName;
                }

                if (assessmentViewModel.AssessmentQuestions[0].Option1Image != null && assessmentViewModel.AssessmentQuestions[0].Option1Image.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid() + "_" + Path.GetFileName(assessmentViewModel.AssessmentQuestions[0].Option1Image.FileName);
                    var path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings[CommonConstants.AssessmentAnswerImgPath].ToString()), fileName);
                    assessmentViewModel.AssessmentQuestions[0].Option1Image.SaveAs(path);
                    assessmentViewModel.AssessmentQuestions[0].Option1ImageFileName = fileName;
                }

                if (assessmentViewModel.AssessmentQuestions[0].Option2Image != null && assessmentViewModel.AssessmentQuestions[0].Option2Image.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid() + "_" + Path.GetFileName(assessmentViewModel.AssessmentQuestions[0].Option2Image.FileName);
                    var path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings[CommonConstants.AssessmentAnswerImgPath].ToString()), fileName);
                    assessmentViewModel.AssessmentQuestions[0].Option2Image.SaveAs(path);
                    assessmentViewModel.AssessmentQuestions[0].Option2ImageFileName = fileName;
                }

                if (assessmentViewModel.AssessmentQuestions[0].Option3Image != null && assessmentViewModel.AssessmentQuestions[0].Option3Image.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid() + "_" + Path.GetFileName(assessmentViewModel.AssessmentQuestions[0].Option3Image.FileName);
                    var path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings[CommonConstants.AssessmentAnswerImgPath].ToString()), fileName);
                    assessmentViewModel.AssessmentQuestions[0].Option3Image.SaveAs(path);
                    assessmentViewModel.AssessmentQuestions[0].Option3ImageFileName = fileName;
                }

                if (assessmentViewModel.AssessmentQuestions[0].Option4Image != null && assessmentViewModel.AssessmentQuestions[0].Option4Image.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid() + "_" + Path.GetFileName(assessmentViewModel.AssessmentQuestions[0].Option4Image.FileName);
                    var path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings[CommonConstants.AssessmentAnswerImgPath].ToString()), fileName);
                    assessmentViewModel.AssessmentQuestions[0].Option4Image.SaveAs(path);
                    assessmentViewModel.AssessmentQuestions[0].Option4ImageFileName = fileName;
                }

                if (assessmentViewModel.AssessmentQuestions[0].Option5Image != null && assessmentViewModel.AssessmentQuestions[0].Option5Image.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid() + "_" + Path.GetFileName(assessmentViewModel.AssessmentQuestions[0].Option5Image.FileName);
                    var path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings[CommonConstants.AssessmentAnswerImgPath].ToString()), fileName);
                    assessmentViewModel.AssessmentQuestions[0].Option5Image.SaveAs(path);
                    assessmentViewModel.AssessmentQuestions[0].Option5ImageFileName = fileName;
                }

                if (assessmentViewModel.AssessmentQuestions[0].Option6Image != null && assessmentViewModel.AssessmentQuestions[0].Option6Image.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid() + "_" + Path.GetFileName(assessmentViewModel.AssessmentQuestions[0].Option6Image.FileName);
                    var path = Path.Combine(Server.MapPath(ConfigurationManager.AppSettings[CommonConstants.AssessmentAnswerImgPath].ToString()), fileName);
                    assessmentViewModel.AssessmentQuestions[0].Option6Image.SaveAs(path);
                    assessmentViewModel.AssessmentQuestions[0].Option6ImageFileName = fileName;
                }


                AssessmentQuestionsModel assessmentQuestionsModel = ConvertAssessmentQuestionsViewModelToDomain(assessmentViewModel.AssessmentQuestions[0]);
                updateSuccess = _IAssessmentService.UpdateAssessmentQuestionDetails(assessmentQuestionsModel);
                if (updateSuccess > 0)
                {
                    return RedirectToAction("ShowAssessmentPaperDetails", new { Id = CheckAccessAttribute.Encode(Convert.ToString(assessmentViewModel.AssessmentPaper.AssessmentPaperId))});
                }
                return View("Error");

            }
            return View("Error");

        }
        public ActionResult UseExistingAssessment(string id, string course, string trainingNameId)
        {
            int QId = Convert.ToInt32(CheckAccessAttribute.Decode(id));
            int Qcourse= Convert.ToInt32(CheckAccessAttribute.Decode(course));
            int QtrainingNameId = Convert.ToInt32(CheckAccessAttribute.Decode(trainingNameId));

            AssessmentViewModel assessmentViewModel = new AssessmentViewModel();
            assessmentViewModel.AssessmentPaper = new AssessmentPaperViewModel();
            assessmentViewModel.AssessmentPaper.AssessmentPaperId = QId;
            assessmentViewModel.AssessmentPaper.TrainingCourses = LoadTrainingCoursesByTrainingNameId(Qcourse, QtrainingNameId);
            assessmentViewModel.AssessmentPaper.AssessmentPaperIdEncrypt = id;
            TempData["TrainingCourses"] = assessmentViewModel.AssessmentPaper.TrainingCourses;
            TempData["OriginalAssessmentPaperId"] = QId;

            return View("UseExistingAssessment", assessmentViewModel);

        }
        [HttpPost]
        public ActionResult UseExistingAssessment(AssessmentViewModel assessmentViewModel)
        {
            AssessmentViewModel currentAssessmentViewModel = new AssessmentViewModel();
            TempData["OriginalAssessmentPaperId"] = assessmentViewModel.AssessmentPaper.AssessmentPaperId;
            currentAssessmentViewModel = GetAssessmentPaperById(assessmentViewModel.AssessmentPaper.AssessmentPaperId);
            assessmentViewModel = GetAssessmentPaperByCourseId(assessmentViewModel.AssessmentPaper.CourseId);
            if (assessmentViewModel != null && assessmentViewModel.AssessmentQuestions != null && assessmentViewModel.AssessmentQuestions.Count > 0)
            {
                if (currentAssessmentViewModel != null)
                {
                    foreach (var question in assessmentViewModel.AssessmentQuestions)
                    {
                        if (currentAssessmentViewModel.AssessmentQuestions != null && currentAssessmentViewModel.AssessmentQuestions.Count > 0)
                        {
                            if (currentAssessmentViewModel.AssessmentQuestions.Any(q => q.QuestionId == question.QuestionId) == false)
                            {
                                question.IsDisplaySelect = true;
                            }
                        }
                        else
                        {
                            question.IsDisplaySelect = true;
                        }
                    }
                }

            }
            assessmentViewModel.AssessmentPaper.TrainingCourses = (IEnumerable<SelectListItem>)TempData["TrainingCourses"] as IEnumerable<SelectListItem>;
            ViewData["prefix"] = "AssessmentViewModel.AssessmentQuestions";
            return View("UseExistingAssessment", assessmentViewModel);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddExistingQuestion(AssessmentViewModel assessmentViewModel, int id)
        {
            int saveStatus = 0;
            Result data = new Result() { Id = id, Status = false };
            if (assessmentViewModel.AssessmentQuestions != null && assessmentViewModel.AssessmentQuestions.Count > 0)
            {
                foreach (var question in assessmentViewModel.AssessmentQuestions)
                {
                    if (question.IsAddQuestion)
                    {
                        saveStatus = _IAssessmentService.AddExistingQuestionInPaper(id, question.QuestionId);
                    }
                }
                if (saveStatus > 0)
                {
                    data.Status = true;
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AssignAssessment()
        {
            AssignAssessmentViewModel ObjSetAssessmentViewModel = new AssignAssessmentViewModel();
            ObjSetAssessmentViewModel.ListCourses = LoadTrainingCourses("AssignAssessment",0);//Vrp Hard code : for Admin login no neeed to pass empid
            return View("AssignAssessment", ObjSetAssessmentViewModel);
        }
        
        [HttpPost]
        public ActionResult AssignAssessment(AssignAssessmentViewModel obj, FormCollection objf)
        {
            bool flag = false;
            if (ModelState.IsValid)
            {
                AssessmentPaperModel assessmentPaperModel = new AssessmentPaperModel();
                assessmentPaperModel.CourseId = obj.CourseId;
                assessmentPaperModel.CreatedBy = Convert.ToInt32(Session["EmpID"]);
                assessmentPaperModel.EmpIdAll = Convert.ToString(objf["SelectedEmp"]);
                flag = _IAssessmentService.SetAssessment(assessmentPaperModel);
            }
                if (flag)
                {
                    TempData["Result"] = "Assessment Set successfully";

                    obj.ListCourses = LoadTrainingCourses("AssignAssessment", 0); //Vrp Hard code : for Admin login no neeed to pass empid
                    obj.CourseId = 0;
                    obj.CourseName = "";
                    return View("AssignAssessment", obj);
                }
                else
                {
                    return View("Error");
                }
            
        }



        ////[AcceptVerbs(HttpVerbs.Get)]
        public string GetAssessCourseDetail(string pcourseId)
        {
            AssessmentViewModel assessmentViewModel = GetAssessmentPaperByCourseId(Convert.ToInt32(pcourseId));
            assessmentViewModel.AssessmentPaper.AssessmentDataDDMMYYY = assessmentViewModel.AssessmentPaper.AssessmentDate.ToString("dd-MMM-yyyy");
            string strData = Newtonsoft.Json.JsonConvert.SerializeObject(assessmentViewModel);
            return strData;
        }

        public  PartialViewResult ViewAssesmentCourse(string pcourseId)        
        {
            AssessmentViewModel assessmentViewModel = GetAssessmentPaperByCourseId(Convert.ToInt32(pcourseId));
            assessmentViewModel.AssessmentPaper.AssessmentDataDDMMYYY = assessmentViewModel.AssessmentPaper.AssessmentDate.ToString("dd-MMM-yyyy");

            AssignAssessmentViewModel objAssignAss = new AssignAssessmentViewModel();
            //objAssignAss.AssessmentDate =assessmentViewModel .AssessmentPaper.AssessmentDate.ToString("dd-MMM-yyyy");
            //objAssignAss.TimeDuration  =assessmentViewModel .AssessmentPaper.TimeDuration.ToString("dd-MMM-yyyy");
            objAssignAss.AssessmentDate =assessmentViewModel .AssessmentPaper.AssessmentDate;
            objAssignAss.TimeDuration  =assessmentViewModel .AssessmentPaper.TimeDuration;
            //objAssignAss.dtEmp=assessmentViewModel.

             AssessmentModel assessmentModel = new AssessmentModel();
             objAssignAss.dtEmp = _IAssessmentService.GetNominatedEmployees( Convert.ToInt32(pcourseId));
            //AssessmentViewModel assessmentViewModel = ConvertDomainToViewModel(assessmentModel);
             

            return PartialView ("_ShowEmployee",objAssignAss);
        }

        #endregion

        #region Private Methods
        private IEnumerable<SelectListItem> LoadTrainingCourses()
        {
            List<TraingCourse> courses = _ITrainingService.GetOpenTrainingCourses();
            courses.Insert(0, new TraingCourse { CourseId = 0, CourseName = "Select" });
            var selectItemList = courses.Select(p => new SelectListItem { Value = p.CourseId.ToString(), Text = p.CourseName }).ToList();
            IEnumerable<SelectListItem> trainingCourses = new SelectList(selectItemList, "Value", "Text");
            return trainingCourses;
        }

        private IEnumerable<SelectListItem> LoadTrainingCourses(string strCourseInPage, int LoginEmpId)
        {
            List<TraingCourse> courses = _ITrainingService.GetTrainingCoursesPageWise(strCourseInPage, LoginEmpId);//vrp hardcode Empid = 0
            courses.Insert(0, new TraingCourse { CourseId = 0, CourseName = "Select" });
            var selectItemList = courses.Select(p => new SelectListItem { Value = p.CourseId.ToString(), Text = p.CourseName }).ToList();
            IEnumerable<SelectListItem> trainingCourses = new SelectList(selectItemList, "Value", "Text");
            return trainingCourses;
        }
        
        private IEnumerable<SelectListItem> LoadTrainingCoursesByTrainingNameId(int courseId, int trainingNameId)
        {
            List<TraingCourse> courses = _ITrainingService.GetTrainingCoursesByTrainingNameId(courseId, trainingNameId);
            courses.Insert(0, new TraingCourse { CourseId = 0, CourseName = "Select" });
            var selectItemList = courses.Select(p => new SelectListItem { Value = p.CourseId.ToString(), Text = p.CourseName }).ToList();
            IEnumerable<SelectListItem> trainingCourses = new SelectList(selectItemList, "Value", "Text");
            return trainingCourses;
        }

        private AssessmentViewModel GetAssessmentPaperById(int Id)
        {
            AssessmentModel assessmentModel = new AssessmentModel();
            assessmentModel = _IAssessmentService.GetAssessmentPaperDetails(Id);
            AssessmentViewModel assessmentViewModel = ConvertDomainToViewModel(assessmentModel);
            return assessmentViewModel;
        }
        private AssessmentViewModel GetAssessmentPaperByCourseId(int courseId)
        {
            AssessmentModel assessmentModel = new AssessmentModel();
            assessmentModel = _IAssessmentService.GetAssessmentPaperDetailsByCourseId(courseId);
            AssessmentViewModel assessmentViewModel = ConvertDomainToViewModel(assessmentModel);
            return assessmentViewModel;
        }

        private AssessmentViewModel ConvertDomainToViewModel(AssessmentModel assessmentModel)
        {
            AssessmentViewModel assessmentViewModel = new AssessmentViewModel();
            assessmentViewModel.AssessmentPaper = new AssessmentPaperViewModel();
            assessmentViewModel.AssessmentPaper.AssessmentPaperId = assessmentModel.AssessmentPaper.AssessmentPaperId;
            assessmentViewModel.AssessmentPaper.AssessmentDate = assessmentModel.AssessmentPaper.AssessmentDate;
            assessmentViewModel.AssessmentPaper.TimeDuration = assessmentModel.AssessmentPaper.TimeDuration;
            assessmentViewModel.AssessmentPaper.CourseId = assessmentModel.AssessmentPaper.CourseId;
            assessmentViewModel.AssessmentPaper.CourseName = assessmentModel.AssessmentPaper.CourseName;
            assessmentViewModel.AssessmentPaper.TrainingNameId = assessmentModel.AssessmentPaper.TrainingNameId;
            if (assessmentModel.AssessmentPaperDetails != null && assessmentModel.AssessmentPaperDetails.Count > 0)
            {
                assessmentViewModel.AssessmentPaperDetails = new List<AssementPaperDetailsViewModel>();
                foreach (var assementPaperDetail in assessmentModel.AssessmentPaperDetails)
                {
                    AssementPaperDetailsViewModel assementPaperDetailsViewModel = new AssementPaperDetailsViewModel();
                    assementPaperDetailsViewModel.AssessmentPaperDetailsId = assementPaperDetail.AssessmentPaperDetailsId;
                    assementPaperDetailsViewModel.AssessmentPaperId = assementPaperDetail.AssessmentPaperId;
                    assementPaperDetailsViewModel.QuestionId = assementPaperDetail.QuestionId;
                    assementPaperDetailsViewModel.IsNewQuestion = assementPaperDetail.IsNewQuestion;
                    assessmentViewModel.AssessmentPaperDetails.Add(assementPaperDetailsViewModel);
                }
                assessmentViewModel.AssessmentQuestions = new List<AssessmentQuestionsViewModel>();
                foreach (var assessmentQuestions in assessmentModel.AssessmentQuestions)
                {
                    AssessmentQuestionsViewModel assessmentQuestionsViewModel = new AssessmentQuestionsViewModel();
                    assessmentQuestionsViewModel.AssessmentPaperDetailsId = assessmentQuestions.AssessmentPaperDetailsId;
                    assessmentQuestionsViewModel.AssessmentPaperId = assessmentQuestions.AssessmentPaperId;
                    assessmentQuestionsViewModel.QuestionId = assessmentQuestions.QuestionId;
                    assessmentQuestionsViewModel.Question = assessmentQuestions.Question;
                    assessmentQuestionsViewModel.QuestionImgFileName = assessmentQuestions.QuestionImgFileName;
                    assessmentQuestionsViewModel.Option1Description = assessmentQuestions.Option1Description;
                    assessmentQuestionsViewModel.Option2Description = assessmentQuestions.Option2Description;
                    assessmentQuestionsViewModel.Option3Description = assessmentQuestions.Option3Description;
                    assessmentQuestionsViewModel.Option4Description = assessmentQuestions.Option4Description;
                    assessmentQuestionsViewModel.Option5Description = assessmentQuestions.Option5Description;
                    assessmentQuestionsViewModel.Option6Description = assessmentQuestions.Option6Description;
                    assessmentQuestionsViewModel.IsMultiChoiceAnswer = assessmentQuestions.IsMultiChoiceAnswer;

                    assessmentQuestionsViewModel.IsOption1Correct = assessmentQuestions.IsOption1Correct;
                    assessmentQuestionsViewModel.IsOption2Correct = assessmentQuestions.IsOption2Correct;
                    assessmentQuestionsViewModel.IsOption3Correct = assessmentQuestions.IsOption3Correct;
                    assessmentQuestionsViewModel.IsOption4Correct = assessmentQuestions.IsOption4Correct;
                    assessmentQuestionsViewModel.IsOption5Correct = assessmentQuestions.IsOption5Correct;
                    assessmentQuestionsViewModel.IsOption6Correct = assessmentQuestions.IsOption6Correct;

                    assessmentQuestionsViewModel.Option1ImageFileName = assessmentQuestions.Option1ImageFileName;
                    assessmentQuestionsViewModel.Option2ImageFileName = assessmentQuestions.Option2ImageFileName;
                    assessmentQuestionsViewModel.Option3ImageFileName = assessmentQuestions.Option3ImageFileName;
                    assessmentQuestionsViewModel.Option4ImageFileName = assessmentQuestions.Option4ImageFileName;
                    assessmentQuestionsViewModel.Option5ImageFileName = assessmentQuestions.Option5ImageFileName;
                    assessmentQuestionsViewModel.Option6ImageFileName = assessmentQuestions.Option6ImageFileName;
                    assessmentViewModel.AssessmentQuestions.Add(assessmentQuestionsViewModel);
                }
            }
            return assessmentViewModel;
        }
        private AssessmentQuestionsModel ConvertAssessmentQuestionsViewModelToDomain(AssessmentQuestionsViewModel assessmentQuestionsViewModel)
        {
            AssessmentQuestionsModel assessmentQuestionsModel = new AssessmentQuestionsModel();
            assessmentQuestionsModel.QuestionId = assessmentQuestionsViewModel.QuestionId;
            assessmentQuestionsModel.Question = assessmentQuestionsViewModel.Question;
            assessmentQuestionsModel.QuestionImgFileName = assessmentQuestionsViewModel.QuestionImgFileName;
            assessmentQuestionsModel.Option1Description = assessmentQuestionsViewModel.Option1Description;
            assessmentQuestionsModel.Option1ImageFileName = assessmentQuestionsViewModel.Option1ImageFileName;
            assessmentQuestionsModel.IsOption1Correct = assessmentQuestionsViewModel.IsOption1Correct;
            assessmentQuestionsModel.Option2Description = assessmentQuestionsViewModel.Option2Description;
            assessmentQuestionsModel.Option2ImageFileName = assessmentQuestionsViewModel.Option2ImageFileName;
            assessmentQuestionsModel.IsOption2Correct = assessmentQuestionsViewModel.IsOption2Correct;
            assessmentQuestionsModel.Option3Description = assessmentQuestionsViewModel.Option3Description;
            assessmentQuestionsModel.Option3ImageFileName = assessmentQuestionsViewModel.Option3ImageFileName;
            assessmentQuestionsModel.IsOption3Correct = assessmentQuestionsViewModel.IsOption3Correct;
            assessmentQuestionsModel.Option4Description = assessmentQuestionsViewModel.Option4Description;
            assessmentQuestionsModel.IsOption4Correct = assessmentQuestionsViewModel.IsOption4Correct;
            assessmentQuestionsModel.Option4ImageFileName = assessmentQuestionsViewModel.Option4ImageFileName;
            assessmentQuestionsModel.Option5Description = assessmentQuestionsViewModel.Option5Description;
            assessmentQuestionsModel.Option5ImageFileName = assessmentQuestionsViewModel.Option5ImageFileName;
            assessmentQuestionsModel.IsOption5Correct = assessmentQuestionsViewModel.IsOption5Correct;
            assessmentQuestionsModel.Option6Description = assessmentQuestionsViewModel.Option6Description;
            assessmentQuestionsModel.Option6ImageFileName = assessmentQuestionsViewModel.Option6ImageFileName;
            assessmentQuestionsModel.IsOption6Correct = assessmentQuestionsViewModel.IsOption6Correct;
            assessmentQuestionsModel.IsMultiChoiceAnswer = assessmentQuestionsViewModel.IsMultiChoiceAnswer;
            assessmentQuestionsModel.IsActive = true;
            assessmentQuestionsModel.CreatedOn = DateTime.Now;
            assessmentQuestionsModel.LastEditedOn = DateTime.Now;
            return assessmentQuestionsModel;
        }
        private AssessmentViewModel ConvertAssessmentQuestionsModelToViewModel(AssessmentModel assessmentModel)
        {
            AssessmentViewModel assessmentViewModel = new AssessmentViewModel();
            assessmentViewModel.AssessmentPaper = new AssessmentPaperViewModel();
            if (assessmentModel.AssessmentQuestions != null)
            {
                assessmentViewModel.AssessmentPaper.AssessmentPaperId = assessmentModel.AssessmentPaper.AssessmentPaperId;
                assessmentViewModel.AssessmentPaper.CourseId = assessmentModel.AssessmentPaper.CourseId;
                assessmentViewModel.AssessmentPaper.CourseName = assessmentModel.AssessmentPaper.CourseName;
                assessmentViewModel.AssessmentPaper.AssessmentDate = assessmentModel.AssessmentPaper.AssessmentDate;
                assessmentViewModel.AssessmentPaper.TimeDuration = assessmentModel.AssessmentPaper.TimeDuration;
            }
            assessmentViewModel.AssessmentQuestions = new List<AssessmentQuestionsViewModel>();
            if (assessmentModel.AssessmentQuestions != null && assessmentModel.AssessmentQuestions.Count > 0)
            {
                AssessmentQuestionsViewModel assessmentQuestionsViewModel = new AssessmentQuestionsViewModel();
                assessmentQuestionsViewModel.QuestionId = assessmentModel.AssessmentQuestions[0].QuestionId;
                assessmentQuestionsViewModel.Question = assessmentModel.AssessmentQuestions[0].Question;
                assessmentQuestionsViewModel.QuestionImgFileName = assessmentModel.AssessmentQuestions[0].QuestionImgFileName;
                assessmentQuestionsViewModel.Option1Description = assessmentModel.AssessmentQuestions[0].Option1Description;
                assessmentQuestionsViewModel.Option1ImageFileName = assessmentModel.AssessmentQuestions[0].Option1ImageFileName;
                assessmentQuestionsViewModel.IsOption1Correct = assessmentModel.AssessmentQuestions[0].IsOption1Correct;
                assessmentQuestionsViewModel.Option2Description = assessmentModel.AssessmentQuestions[0].Option2Description;
                assessmentQuestionsViewModel.Option2ImageFileName = assessmentModel.AssessmentQuestions[0].Option2ImageFileName;
                assessmentQuestionsViewModel.IsOption2Correct = assessmentModel.AssessmentQuestions[0].IsOption2Correct;
                assessmentQuestionsViewModel.Option3Description = assessmentModel.AssessmentQuestions[0].Option3Description;
                assessmentQuestionsViewModel.Option3ImageFileName = assessmentModel.AssessmentQuestions[0].Option3ImageFileName;
                assessmentQuestionsViewModel.IsOption3Correct = assessmentModel.AssessmentQuestions[0].IsOption3Correct;
                assessmentQuestionsViewModel.Option4Description = assessmentModel.AssessmentQuestions[0].Option4Description;
                assessmentQuestionsViewModel.IsOption4Correct = assessmentModel.AssessmentQuestions[0].IsOption4Correct;
                assessmentQuestionsViewModel.Option4ImageFileName = assessmentModel.AssessmentQuestions[0].Option4ImageFileName;
                assessmentQuestionsViewModel.Option5Description = assessmentModel.AssessmentQuestions[0].Option5Description;
                assessmentQuestionsViewModel.Option5ImageFileName = assessmentModel.AssessmentQuestions[0].Option5ImageFileName;
                assessmentQuestionsViewModel.IsOption5Correct = assessmentModel.AssessmentQuestions[0].IsOption5Correct;
                assessmentQuestionsViewModel.Option6Description = assessmentModel.AssessmentQuestions[0].Option6Description;
                assessmentQuestionsViewModel.Option6ImageFileName = assessmentModel.AssessmentQuestions[0].Option6ImageFileName;
                assessmentQuestionsViewModel.IsOption6Correct = assessmentModel.AssessmentQuestions[0].IsOption6Correct;
                assessmentQuestionsViewModel.IsMultiChoiceAnswer = assessmentModel.AssessmentQuestions[0].IsMultiChoiceAnswer;
                assessmentViewModel.AssessmentQuestions.Add(assessmentQuestionsViewModel);
            }
            return assessmentViewModel;
        }
        #endregion
    }
}
