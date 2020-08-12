using Domain.Entities;
using RMS.Common;
using RMS.Helpers;
using RMS.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RMS.Controllers
{
    [CheckAccess]
    public class FeedbackController : Controller
    {
        private readonly ITrainingService _service;
        //
        // GET: /Feedback/
        public FeedbackController(ITrainingService service)
        {
            _service = service;
        }

        public FeedbackController()
        {

        }

        [HttpGet]
        public ActionResult FeedbackForm(string empId, string CourseID)
        {
            Master m = new Master();
            empId = m.GetEmployeeIDByEmailID().ToString();
            
            FeedbackQuestionViewModel objFeedbackQuestionViewModel = new FeedbackQuestionViewModel(_service,empId);
            objFeedbackQuestionViewModel.QuestionModel = new List<Domain.Entities.QuestionModel>();
            objFeedbackQuestionViewModel.FeedbackModel = new List<FeedbackModel>();
            
            ViewBag.empId = empId;
            ViewBag.empName = _service.GetEmpNameByEmpID(empId);

            DataSet description = _service.GetQuestionDescription();
            
            //description.Tables[0].Rows.Count

            for (int i = 0; i < description.Tables[0].Rows.Count; i++)
            {
                QuestionModel objQuestionModel = new QuestionModel();
                objQuestionModel.Description = description.Tables[0].Rows[i]["Description"].ToString();
                objQuestionModel.QuestionMasterID = Convert.ToInt32(description.Tables[0].Rows[i]["QuestionMasterID"]);
                objFeedbackQuestionViewModel.QuestionModel.Add(objQuestionModel);
               
            }

            for (int i = 0; i < description.Tables[0].Rows.Count; i++)
            {
                FeedbackModel objFeedbackModel = new FeedbackModel();
                objFeedbackModel.QuestionID = Convert.ToInt32(description.Tables[0].Rows[i]["QuestionMasterID"]);
                objFeedbackModel.Rating = 0;
                objFeedbackQuestionViewModel.FeedbackModel.Add(objFeedbackModel);
                objFeedbackModel.EmpID = Convert.ToInt32(Session["EmpID"]);
            }

            return View(objFeedbackQuestionViewModel);
        }

        public ActionResult GetTrainerName(string CourseID)
        {
            string TrainerName = string.Empty;
            TrainerName = _service.GetTrainerNameByCourseID(CourseID);

            var result = new {Trainername = TrainerName};
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult FeedbackForm(FeedbackQuestionViewModel objFeedbackQuestionViewModel, string CourseID)
        {
            int insert_status = 0;
            Master m = new Master();
            string empId = m.GetEmployeeIDByEmailID().ToString();
            
            for (int i = 0; i < objFeedbackQuestionViewModel.FeedbackModel.Count; i++)
            {
                FeedbackModel objFeedbackModel = new FeedbackModel();
                objFeedbackModel.EmpID = Convert.ToInt32(empId);
                objFeedbackModel.QuestionID = objFeedbackQuestionViewModel.QuestionModel[i].QuestionMasterID;
                objFeedbackModel.Rating = objFeedbackQuestionViewModel.FeedbackModel[i].Rating;
                objFeedbackModel.CourseID = Convert.ToInt32(CourseID);
                objFeedbackModel.CommentsFeedback = objFeedbackQuestionViewModel.FeedbackModel[i].CommentsFeedback;

                //objFeedbackQuestionViewModel.FeedbackModel.Add(objFeedbackModel);
                insert_status = _service.SaveFeedbackDetails(objFeedbackModel);
            }

            int update_status = _service.UpdateFlagToFeedbackFilled(empId, CourseID);
            if (update_status == 1)
            {
                TempData["Result"] = "Feedback Form submitted successfully";
            }
            objFeedbackQuestionViewModel.CourseName = new SelectList(_service.GetTrainingNameList(empId), "Key", "Value");
            return View(objFeedbackQuestionViewModel);
        }

        public ActionResult Cancel()
        {
            return RedirectToAction("ViewTrainingRequest", "Training");
        }

        [HttpGet]
        //[Authorize(Roles = "RMO")]
        public ActionResult FeedbackForRMO()
        {
            Master m = new Master();
            string empId;
            empId = m.GetEmployeeIDByEmailID().ToString();
            FeedbackQuestionViewModel objFeedbackQuestionViewModel = new FeedbackQuestionViewModel(_service);
            objFeedbackQuestionViewModel.QuestionModel = new List<Domain.Entities.QuestionModel>();
            objFeedbackQuestionViewModel.FeedbackModel = new List<FeedbackModel>();
            
            DataSet description = _service.GetQuestionDescription();

            for (int i = 0; i < description.Tables[0].Rows.Count; i++)
            {
                QuestionModel objQuestionModel = new QuestionModel();
                objQuestionModel.Description = description.Tables[0].Rows[i]["Description"].ToString();
                objQuestionModel.QuestionMasterID = Convert.ToInt32(description.Tables[0].Rows[i]["QuestionMasterID"]);
                objFeedbackQuestionViewModel.QuestionModel.Add(objQuestionModel);

            }

            return View(objFeedbackQuestionViewModel);
        }

        [HttpPost]
        public ActionResult FeedbackFormRMO(FeedbackQuestionViewModel objFeedbackQuestionViewModel, string Command, string CourseID)
        {
            int update_status = 0;
            if (Command == "Save")
            {
                NominationModel objNominationModel = new NominationModel();
                objNominationModel.FeedbackToBeSentTrainer = objFeedbackQuestionViewModel.NominationModel.FeedbackToBeSentTrainer;
                objNominationModel.FeedbackSentToTrainer = objFeedbackQuestionViewModel.NominationModel.FeedbackSentToTrainer;
                objNominationModel.ReasonSLANotMet = objFeedbackQuestionViewModel.NominationModel.ReasonSLANotMet;
                objNominationModel.CommentsForFeedback = objFeedbackQuestionViewModel.NominationModel.CommentsForFeedback;

                update_status = _service.SaveFeedbackDetailsByRMO(objNominationModel, CourseID);

                //objFeedbackQuestionViewModel.CourseNames = new SelectList(_service.GetTrainingNameList(), "Key", "Value");
                return RedirectToAction("ViewTechnicalTrainingRequest", "Training");
            }
            else if (Command == "Send Feedback")
            {
                NominationModel objNominationModel = new NominationModel();
                objNominationModel.FeedbackToBeSentTrainer = objFeedbackQuestionViewModel.NominationModel.FeedbackToBeSentTrainer;
                objNominationModel.FeedbackSentToTrainer = objFeedbackQuestionViewModel.NominationModel.FeedbackSentToTrainer;
                objNominationModel.ReasonSLANotMet = objFeedbackQuestionViewModel.NominationModel.ReasonSLANotMet;
                objNominationModel.CommentsForFeedback = objFeedbackQuestionViewModel.NominationModel.CommentsForFeedback;
                objNominationModel.SendFeedback = true;
                objNominationModel.CourseID = Convert.ToInt32(CourseID);

                int statusUpdate = _service.SaveAndUpdateStatusOfTraining(objNominationModel, CourseID);
                
                List<double> Total = new List<double>();
                DataSet description = _service.GetQuestionDescription();

                for (int i = 0; i < description.Tables[0].Rows.Count; i++)
                {
                    Total.Add(_service.GetAverageRatingsFromParticipants(description.Tables[0].Rows[i]["QuestionMasterID"].ToString(), CourseID));
                }

                DataSet TrainingDetails = _service.GetTrainingDetailsByCourseId(CourseID);
                EmailHelper.SendMailToTrainerWithRatings(Total, TrainingDetails);
                TempData["Result"] = "Feedback details saved successfully and mail is sent to trainer";
                return RedirectToAction("FeedbackForRMO", "Feedback");
            }
            //Harsha Issue Id- 58436- Start
            //Description: on training record: if no emp fill feedback the show comment in SLA Met  columns
            else if (Command == "No Feedback Received")
            {
                NominationModel objNominationModel = new NominationModel();
                objNominationModel.FeedbackToBeSentTrainer = null;
                objNominationModel.FeedbackSentToTrainer = null;
                objNominationModel.ReasonSLANotMet = "";
                objNominationModel.CommentsForFeedback = "";
                objNominationModel.SendFeedback = false;
                objNominationModel.CourseID = Convert.ToInt32(CourseID);
                objNominationModel.NoFeedbackReceived = true;
                int statusUpdate = _service.SaveAndUpdateStatusOfTraining(objNominationModel, CourseID);
                return RedirectToAction("ViewTechnicalTrainingRequest", "Training");
            }
            //Harsha Issue Id- 58436- End
            else
                return View();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ExportToExcel(string htmlstring, string CourseID, string txtNoOfParticipants, string txtNoOfFeedbacksReceived)
        {

            DataSet TrainingDetails = _service.GetTrainingDetailsByCourseId(CourseID);
            string TrainingName = TrainingDetails.Tables[0].Rows[0]["TrainingName"].ToString();
            int TrainingMode = Convert.ToInt32(TrainingDetails.Tables[0].Rows[0]["TrainingModeID"]);
            string TrainerName = string.Empty;
            if (TrainingMode == 1909)
            {
                TrainerName = TrainingDetails.Tables[0].Rows[0]["TrainerNameInternal"].ToString();
            }
            else if (TrainingMode == 1910)
            {
                TrainerName = TrainingDetails.Tables[0].Rows[0]["TrainerName"].ToString();
            }
            
            string TrainingStartDt = TrainingDetails.Tables[0].Rows[0]["TrainingStartDt"].ToString();
            string TrainingEndDt = TrainingDetails.Tables[0].Rows[0]["TrainingEndDt"].ToString();
            
            string TrainingVenue = TrainingDetails.Tables[0].Rows[0]["TrainingLocation"].ToString();

            StringBuilder StrHtmlGenerate = new StringBuilder();
            StringBuilder StrExport = new StringBuilder();

            StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
            StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
            StrExport.Append("<DIV  style='font-size:12px;'>");
            StrExport.Append("<table><tr style='border: inset'><td style='border: inset'><b>Course Title</b></td>");
            StrExport.Append("<td style='border: inset'>" + TrainingName + "</td></tr>");
            StrExport.Append("<tr style='border: inset'><td style='border: inset'><b>Trainer's Name </td></b>");
            StrExport.Append("<td style='border: inset'>" + TrainerName + "</td>");
            StrExport.Append("<td style='border: inset'><b>Start Date:</b></td>");
            StrExport.Append("<td style='border: inset'>" + TrainingStartDt + "</td></tr>");
            StrExport.Append("<tr style='border: inset'><td style='border: inset'><b>Training Venue:</b></td>");
            StrExport.Append("<td style='border: inset'>" + TrainingVenue + "</td>");
            StrExport.Append("<td style='border: inset'><b>End Date:</b></td>");
            StrExport.Append("<td style='border: inset'>" + TrainingEndDt + "</td></tr>");
            StrExport.Append("<tr style='border: inset'><td style='border: inset'><b>No of Participants</b></td>");
            StrExport.Append("<td style='border: inset'>" + txtNoOfParticipants + "</td>");
            StrExport.Append("<td style='border: inset'><b>Feedbacks Received</b></td>");
            StrExport.Append("<td style='border: inset'>" + txtNoOfFeedbacksReceived + "</td></tr><tr></tr>");

            StrExport.Append(htmlstring.ToString());
            StrExport.Append("</div></body></html>");
            string strFile = "FeedbackCollationTemplate.xls";
            string strcontentType = "application/excel";
            Response.ClearContent();
            Response.ClearHeaders();
            Response.BufferOutput = true;
            Response.ContentType = strcontentType;
            Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
            Response.Write(StrExport.ToString());
            Response.Flush();
            //Response.Close();
            Response.End();

            ////Ishwar Patil 04/11/2016 Start Desc: Not able to download xls file
            
            //Response.ContentType = "Application/x-msexcel";
            //Response.AppendHeader("Content-Disposition", "attachment; filename=FeedbackCollationTemplate-" + DateTime.Now.ToShortDateString() + ".xls;");
            //Response.Write(StrExport.ToString());

            //// Write the file to the Response
            //const int bufferLength = 10000;
            //byte[] buffer = new Byte[bufferLength];
            //int length = 0;
            //Stream download = null;
            //try
            //{
            //    String FilePath = Server.MapPath("~/FeedbackCollationTemplate.xls");
            //    download = new FileStream(FilePath,
            //                                                   FileMode.Open,
            //                                                   FileAccess.Read);
            //    do
            //    {
            //        if (Response.IsClientConnected)
            //        {
            //            length = download.Read(buffer, 0, bufferLength);
            //            Response.OutputStream.Write(buffer, 0, length);
            //            buffer = new Byte[bufferLength];
            //        }
            //        else
            //        {
            //            length = -1;
            //        }
            //    }
            //    while (length > 0);
            //    Response.Flush();
            //    Response.End();
            //}
            //finally
            //{
            //    if (download != null)
            //        download.Close();
            //}

            return View();
        }

        public ActionResult FeedbackGrid(string CourseID, string Command)
        {
            if (Convert.ToInt32(CourseID) == 0)
            {
                return RedirectToAction("FeedbackForRMO");
            }
            else
            {
            //Feedback to be sent to participants need to be picked from attendance screen
            DataSet EmpId = _service.GetEmployeesFeedbackByCourseId(CourseID);
            
            List<string> Empname = new List<string>();
            int ParticipantsCount = _service.getParticipantsCount(CourseID);
            int FeedbackFilledCount = _service.getFeedbackFilledCount(CourseID);

            List<FeedbackModel> objListFeedbackModel = new List<FeedbackModel>();
            for (int j = 0; j < EmpId.Tables[0].Rows.Count; j++)
            {
                DataSet FeedbackGridRatings = _service.GetFeedbackRatings(CourseID, EmpId.Tables[0].Rows[j]["EMPId"].ToString());
                if (FeedbackGridRatings.Tables[0].Rows.Count != 0)
                {
                    for (int i = 0; i < FeedbackGridRatings.Tables[0].Rows.Count - 4; i++)
                    {
                        FeedbackModel objFeedbackModel = new FeedbackModel();
                        objFeedbackModel.EmpID = Convert.ToInt32(FeedbackGridRatings.Tables[0].Rows[i]["EmpID"]);
                        objFeedbackModel.EmpName = _service.GetEmpNameByEmpID(FeedbackGridRatings.Tables[0].Rows[i]["EmpID"].ToString());
                        objFeedbackModel.Rating = Convert.ToInt32(FeedbackGridRatings.Tables[0].Rows[i]["Rating"]);
                        objListFeedbackModel.Add(objFeedbackModel);

                    }
                    DataSet FeedbackGridComments = _service.GetFeedbackRatings(CourseID, EmpId.Tables[0].Rows[j]["EMPId"].ToString());

                    for (int i = FeedbackGridRatings.Tables[0].Rows.Count - 4; i < FeedbackGridRatings.Tables[0].Rows.Count; i++)
                    {
                        FeedbackModel objFeedbackModel = new FeedbackModel();
                        objFeedbackModel.EmpID = Convert.ToInt32(FeedbackGridComments.Tables[0].Rows[i]["EmpID"]);
                        objFeedbackModel.EmpName = _service.GetEmpNameByEmpID(FeedbackGridComments.Tables[0].Rows[i]["EmpID"].ToString());
                        objFeedbackModel.CommentsFeedback = FeedbackGridComments.Tables[0].Rows[i]["CommentsFeedback"].ToString();
                        objListFeedbackModel.Add(objFeedbackModel);
                    }
                }
            }

            //to calculate average rating start
            List<double> Total = new List<double>();
            DataSet description = _service.GetQuestionDescription();

            for(int i=0; i< description.Tables[0].Rows.Count-4; i++ )
            {
                Total.Add(_service.GetAverageRatingsFromParticipants(description.Tables[0].Rows[i]["QuestionMasterID"].ToString(), CourseID));
            }
            
            double knowledge = 0, TrainingMethodology = 0, PresentationSkills, TrainerOverall = 0, Content = 0, Facility = 0, Overall = 0;
            //Harsha- Issue Id: 58954- Start
            //Description: Need to remove training face from feedback page
            for (int i = 0; i < 4; i++)
            {
                knowledge += Total[i];
            }
            knowledge = Math.Round(knowledge / 4, 1, MidpointRounding.AwayFromZero);

            for (int i = 4; i < 7; i++)
            {
                TrainingMethodology += Total[i];
            }
            TrainingMethodology = Math.Round(TrainingMethodology / 3, 1, MidpointRounding.AwayFromZero);

            PresentationSkills = Total[7];

            for (int i = 8; i < 10; i++)
            {
                TrainerOverall += Total[i];
            }
            TrainerOverall = Math.Round(TrainerOverall / 2, 1, MidpointRounding.AwayFromZero);

            for (int i = 10; i < 15; i++)
            {
                Content += Total[i];
            }
            Content = Math.Round(Content / 5, 1, MidpointRounding.AwayFromZero);

            for (int i = 15; i < 18; i++)
            {
                Facility += Total[i];
            }
            Facility = Math.Round(Facility / 3, 1, MidpointRounding.AwayFromZero);

            for (int i = 18; i < 20; i++)
            {
                Overall += Total[i];
            }
            //Harsha- Issue Id: 58954- End
            Overall = Math.Round(Overall / 2, 1, MidpointRounding.AwayFromZero);

            double TrainerAverageRating = Math.Round((knowledge + TrainingMethodology + PresentationSkills + TrainerOverall) / 4, 1, MidpointRounding.AwayFromZero);

            List<double> TrainerAverage = new List<double>();
            TrainerAverage.Add(knowledge);
            TrainerAverage.Add(TrainingMethodology);
            TrainerAverage.Add(PresentationSkills);
            TrainerAverage.Add(TrainerOverall);
            TrainerAverage.Add(Content);
            TrainerAverage.Add(Facility);
            TrainerAverage.Add(Overall);
            TrainerAverage.Add(TrainerAverageRating);

            //List<>


            //End
            for (int i = 0; i < EmpId.Tables[0].Rows.Count; i++)
            {
                Empname.Add(_service.GetEmpNameByEmpID(EmpId.Tables[0].Rows[i]["EMPId"].ToString()));
            }

            NominationModel objNominationModel = new NominationModel();
            DataSet dsNominationModel = _service.GetFeedbackDetailsForRMO(CourseID);
            List<NominationModel> lstNominationModel = new List<NominationModel>();

            objNominationModel.FeedbackLastDate = Convert.ToDateTime(dsNominationModel.Tables[0].Rows[0]["FeedbackLastDate"]);

            if (dsNominationModel.Tables[0].Rows[0]["FeedbackToBeSentTrainer"].ToString() != "")
            {
                objNominationModel.FeedbackToBeSentTrainer = Convert.ToDateTime(dsNominationModel.Tables[0].Rows[0]["FeedbackToBeSentTrainer"]);
                if (dsNominationModel.Tables[0].Rows[0]["FeedbackSentToTrainer"].ToString() != "")
                {
                    objNominationModel.FeedbackSentToTrainer = Convert.ToDateTime(dsNominationModel.Tables[0].Rows[0]["FeedbackSentToTrainer"]);
                }
                else
                {
                    objNominationModel.FeedbackSentToTrainer = null;
                }
                objNominationModel.ReasonSLANotMet = dsNominationModel.Tables[0].Rows[0]["ReasonSLANotMet"].ToString();
                objNominationModel.CommentsForFeedback = dsNominationModel.Tables[0].Rows[0]["CommentsForFeedback"].ToString();
                lstNominationModel.Add(objNominationModel);
            }
            else
            {
                objNominationModel.FeedbackToBeSentTrainer = null;
                lstNominationModel.Add(objNominationModel);
            }

            List<QuestionModel> lstQuestionModel = new List<QuestionModel>();
            DataSet desc = _service.GetQuestionDescription();

            for (int i = 0; i < desc.Tables[0].Rows.Count; i++)
            {
                QuestionModel objQuestionModel = new QuestionModel();
                objQuestionModel.Description = desc.Tables[0].Rows[i]["Description"].ToString();
                objQuestionModel.QuestionMasterID = Convert.ToInt32(desc.Tables[0].Rows[i]["QuestionMasterID"]);
                lstQuestionModel.Add(objQuestionModel);

            }


            var result = new { objectList = objListFeedbackModel, stringList = Empname, nominationModel = lstNominationModel, TotalRating = Total, AvgTrainer = TrainerAverage, TotalParticpants = ParticipantsCount, NoOfFeedbackFilled = FeedbackFilledCount };

            return Json(result, JsonRequestBehavior.AllowGet);
            }

        }


    }
}
