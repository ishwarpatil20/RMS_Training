using Domain.Entities;
using Infrastructure;
using Infrastructure.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RMS.Controllers;
using RMS.Models;
using RMS.Tests.Repository;
using Services;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace RMS.Tests.Controllers
{
    [TestClass]
    public class AssessmentControllerTest
    {
        private ITrainingService _ITrainingService;
        private IAssessmentService _IAssessmentService;

        [TestInitialize]
        public void Initialize()
        {
            _ITrainingService = new TrainingService(new FakeTrainingRepository());
            _IAssessmentService = new AssessmentService(new FakeAssessmentRepository());
        }

        public static HttpContext FakeHttpContext()
        {
            var httpRequest = new HttpRequest("", "http://rav-dsk-386/RMS/", "1208");
            var stringWriter = new StringWriter();
            var httpResponse = new HttpResponse(stringWriter);
            var httpContext = new HttpContext(httpRequest, httpResponse);

            var sessionContainer = new HttpSessionStateContainer("id", new SessionStateItemCollection(),
                                                    new HttpStaticObjectsCollection(), 10, true,
                                                    HttpCookieMode.AutoDetect,
                                                    SessionStateMode.InProc, false);

            httpContext.Items["AspSession"] = typeof(HttpSessionState).GetConstructor(
                                        BindingFlags.NonPublic | BindingFlags.Instance,
                                        null, CallingConventions.Standard,
                                        new[] { typeof(HttpSessionStateContainer) },
                                        null)
                                .Invoke(new object[] { sessionContainer });

            string[] roles = null;
            var identity = new GenericIdentity("RAVE-TECH.CO.IN\\Vishakha.Parashar");
            var principal = new GenericPrincipal(identity, roles);
            httpContext.User = principal;

            return httpContext;
        }

        [TestMethod]
        public void CreateAssessment_Get()
        {
            // Arrange
            var controller = new AssessmentController(_ITrainingService, _IAssessmentService);

            // Act
            var result = (ViewResult)controller.CreateAssessment();

            // Assert
            Assert.AreEqual("CreateAssessment", result.ViewName);

        }
        [TestMethod]
        public void CreateAssessment_Post_Valid()
        {
            //Arrange 
            var controller = new AssessmentController(_ITrainingService, _IAssessmentService);

            //Initialize model     
            AssessmentPaperViewModel assessmentPaperViewModel = new AssessmentPaperViewModel();
            assessmentPaperViewModel.AssessmentPaperId = 0;
            assessmentPaperViewModel.CourseId = 27;
            assessmentPaperViewModel.AssessmentDate = DateTime.Now;
            assessmentPaperViewModel.TimeDuration = 120;
            assessmentPaperViewModel.CreatedOn = DateTime.Now;
            assessmentPaperViewModel.LastEditedOn = DateTime.Now;
            assessmentPaperViewModel.IsActive = true;

            //Act            
            var result = (ActionResult)controller.CreateAssessment(assessmentPaperViewModel);

            //Assert
            Assert.IsInstanceOfType(result,typeof(RedirectToRouteResult));
            RedirectToRouteResult routeResult = result as RedirectToRouteResult;
            Assert.AreEqual(routeResult.RouteValues["action"], "ShowAssessmentPaperDetails");
        }
        [TestMethod]
        public void CreateAssessment_Post_InValid()
        {
            //Arrange 
            var controller = new AssessmentController(_ITrainingService, _IAssessmentService);

            //Initialize model     
            AssessmentPaperViewModel assessmentPaperViewModel = new AssessmentPaperViewModel();
            assessmentPaperViewModel.AssessmentPaperId = 1;
            assessmentPaperViewModel.CourseId = 27;
            assessmentPaperViewModel.AssessmentDate = DateTime.Now;
            assessmentPaperViewModel.TimeDuration = 120;
            assessmentPaperViewModel.CreatedOn = DateTime.Now;
            assessmentPaperViewModel.LastEditedOn = DateTime.Now;
            assessmentPaperViewModel.IsActive = true;

            //Act            
            var result = (ActionResult)controller.CreateAssessment(assessmentPaperViewModel);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("Error", (result as ViewResult).ViewName);
        }
        [TestMethod]
        public void ShowAssessmentPaperDetails()
        {
            // Arrange
            var controller = new AssessmentController(_ITrainingService, _IAssessmentService);

            // Act
            int assessmentPaperId = 1;
            var result = (ViewResult)controller.ShowAssessmentPaperDetails(assessmentPaperId);

            // Assert
            Assert.AreEqual("ShowAssessmentPaperDetails", result.ViewName);
        }
        [TestMethod]
        public void AddAssessmentDetails_NewQuestion()
        {
            // Arrange
            var controller = new AssessmentController(_ITrainingService, _IAssessmentService);

            // Act
            int assessmentPaperId = 1;
            int trainingNameId=1;
            int courseId = 1;
            var result = (ActionResult)controller.AddAssessmentDetails(assessmentPaperId, courseId,trainingNameId, "Add New Question");

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            RedirectToRouteResult routeResult = result as RedirectToRouteResult;
            Assert.AreEqual(routeResult.RouteValues["action"], "AddNewQuestion");
            
        }
        [TestMethod]
        public void AddAssessmentDetails_ExistingAssessment()
        {
            // Arrange
            var controller = new AssessmentController(_ITrainingService, _IAssessmentService);

            // Act
            int assessmentPaperId = 1;
            int trainingNameId = 1;
            int courseId = 1;
            var result = (ActionResult)controller.AddAssessmentDetails(assessmentPaperId, courseId,trainingNameId, "Use Existing Assessment");

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            RedirectToRouteResult routeResult = result as RedirectToRouteResult;
            Assert.AreEqual(routeResult.RouteValues["action"], "UseExistingAssessment");
        }
        [TestMethod]
        public void AddAssessmentDetails_Default()
        {
            // Arrange
            var controller = new AssessmentController(_ITrainingService, _IAssessmentService);

            // Act
            int assessmentPaperId = 1;
            int trainingNameId = 1;
            int courseId = 1;
            var result = (ActionResult)controller.AddAssessmentDetails(assessmentPaperId, courseId,trainingNameId, "abc");

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
        [TestMethod]
        public void AddNewQuestion()
        {
            // Arrange
            var controller = new AssessmentController(_ITrainingService, _IAssessmentService);

            // Act
            int assessmentPaperId = 1;
            var result = (ActionResult)controller.AddNewQuestion(assessmentPaperId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("AddNewQuestion",((ViewResult) result).ViewName);
        }
        [TestMethod]
        public void AddNewQuestion_Post_Valid()
        {
            // Arrange
            var controller = new AssessmentController(_ITrainingService, _IAssessmentService);

            //Initialize model     
            AssessmentViewModel assessmentViewModel = new AssessmentViewModel();
            assessmentViewModel.AssessmentPaper = new AssessmentPaperViewModel();
            assessmentViewModel.AssessmentPaper.AssessmentPaperId = 1;
            assessmentViewModel.AssessmentQuestions = new List<AssessmentQuestionsViewModel>();
            AssessmentQuestionsViewModel assessmentQuestionsViewModel = new AssessmentQuestionsViewModel();
            assessmentQuestionsViewModel.QuestionId = 0;
            assessmentQuestionsViewModel.Question = "a";
            assessmentQuestionsViewModel.Option1Description = "a1";
            assessmentQuestionsViewModel.IsOption1Correct = true;
            assessmentQuestionsViewModel.Option2Description = "a1";
            assessmentQuestionsViewModel.IsOption2Correct = true;
            assessmentQuestionsViewModel.Option3Description = "a1";
            assessmentQuestionsViewModel.IsOption3Correct = true;
            assessmentQuestionsViewModel.Option4Description = "a1";
            assessmentQuestionsViewModel.IsOption4Correct = true;
            assessmentQuestionsViewModel.CreatedBy = 1;
            assessmentQuestionsViewModel.CreatedOn = DateTime.Now;
            assessmentQuestionsViewModel.LastEditedBy = 1;
            assessmentQuestionsViewModel.LastEditedOn = DateTime.Now;
            assessmentQuestionsViewModel.IsActive = true;
            assessmentViewModel.AssessmentQuestions.Add(assessmentQuestionsViewModel);

            //Act            
            var result = (ActionResult)controller.AddNewQuestion(assessmentViewModel);

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            RedirectToRouteResult routeResult = result as RedirectToRouteResult;
            Assert.AreEqual(routeResult.RouteValues["action"], "ShowAssessmentPaperDetails");
        }
        [TestMethod]
        public void AddNewQuestion_Post_InValid()
        {
            // Arrange
            var controller = new AssessmentController(_ITrainingService, _IAssessmentService);

            //Initialize model     
            AssessmentViewModel assessmentViewModel = new AssessmentViewModel();
            assessmentViewModel.AssessmentPaper = new AssessmentPaperViewModel();
            assessmentViewModel.AssessmentPaper.AssessmentPaperId = 1;
            assessmentViewModel.AssessmentQuestions = new List<AssessmentQuestionsViewModel>();
            AssessmentQuestionsViewModel assessmentQuestionsViewModel = new AssessmentQuestionsViewModel();
            assessmentQuestionsViewModel.QuestionId = 1;
            assessmentQuestionsViewModel.Question = "a";
            assessmentQuestionsViewModel.Option1Description = "a1";
            assessmentQuestionsViewModel.IsOption1Correct = true;
            assessmentQuestionsViewModel.Option2Description = "a1";
            assessmentQuestionsViewModel.IsOption2Correct = true;
            assessmentQuestionsViewModel.Option3Description = "a1";
            assessmentQuestionsViewModel.IsOption3Correct = true;
            assessmentQuestionsViewModel.Option4Description = "a1";
            assessmentQuestionsViewModel.IsOption4Correct = true;
            assessmentQuestionsViewModel.CreatedBy = 1;
            assessmentQuestionsViewModel.CreatedOn = DateTime.Now;
            assessmentQuestionsViewModel.LastEditedBy = 1;
            assessmentQuestionsViewModel.LastEditedOn = DateTime.Now;
            assessmentQuestionsViewModel.IsActive = true;
            assessmentViewModel.AssessmentQuestions.Add(assessmentQuestionsViewModel);

            //Act            
            var result = (ActionResult)controller.AddNewQuestion(assessmentViewModel);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
           
        }
        [TestMethod]
        public void AddNewQuestion_Post_NoQuestions()
        {
            // Arrange
            var controller = new AssessmentController(_ITrainingService, _IAssessmentService);

            //Initialize model     
            AssessmentViewModel assessmentViewModel = new AssessmentViewModel();
            assessmentViewModel.AssessmentPaper = new AssessmentPaperViewModel();
            assessmentViewModel.AssessmentPaper.AssessmentPaperId = 1;

            //Act            
            var result = (ActionResult)controller.AddNewQuestion(assessmentViewModel);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
           
        }
        [TestMethod]
        public void DeleteAssessmentQuestion_Error()
        {
            // Arrange
            var controller = new AssessmentController(_ITrainingService, _IAssessmentService);

            // Act
            int assessmentPaperId = 1;
            int assessmentQuestionID = 0;
            var result = (ActionResult)controller.DeleteAssessmentQuestion(assessmentQuestionID,assessmentPaperId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            Assert.AreEqual((result as JsonResult).Data, false);
        }
        [TestMethod]
        public void DeleteAssessmentQuestion_Success()
        {
            // Arrange
            var controller = new AssessmentController(_ITrainingService, _IAssessmentService);

            // Act
            int assessmentPaperId = 1;
            int assessmentQuestionID = 1;
            var result = (ActionResult)controller.DeleteAssessmentQuestion(assessmentQuestionID, assessmentPaperId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(JsonResult));
            Assert.AreEqual((result as JsonResult).Data, true);
        }
        [TestMethod]
        public void EditAssessmentQuestion_Get()
        {
            // Arrange
            var controller = new AssessmentController(_ITrainingService, _IAssessmentService);

            // Act
            int assessmentPaperId = 1;
            int assessmentQuestionID = 1;
            var result = (ActionResult)controller.EditAssessmentQuestion(assessmentQuestionID, assessmentPaperId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("EditAssessmentQuestion",(result as ViewResult).ViewName);
        }
        [TestMethod]
        public void EditAssessmentQuestion_PostValid()
        {
            // Arrange
            var controller = new AssessmentController(_ITrainingService, _IAssessmentService);

            // Act
            AssessmentViewModel assessmentViewModel = new AssessmentViewModel();
            assessmentViewModel.AssessmentPaper = new AssessmentPaperViewModel();
            assessmentViewModel.AssessmentPaper.AssessmentPaperId = 1;
            assessmentViewModel.AssessmentQuestions = new List<AssessmentQuestionsViewModel>();
            AssessmentQuestionsViewModel assessmentQuestionsViewModel = new AssessmentQuestionsViewModel();
            assessmentQuestionsViewModel.QuestionId = 1;
            assessmentViewModel.AssessmentQuestions.Add(assessmentQuestionsViewModel);
            var result = (ActionResult)controller.EditAssessmentQuestion(assessmentViewModel);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            RedirectToRouteResult routeResult = result as RedirectToRouteResult;
            Assert.AreEqual(routeResult.RouteValues["action"], "ShowAssessmentPaperDetails");
            
        }
        [TestMethod]
        public void EditAssessmentQuestion_PostInValid()
        {
            // Arrange
            var controller = new AssessmentController(_ITrainingService, _IAssessmentService);

            // Act
            AssessmentViewModel assessmentViewModel = new AssessmentViewModel();
            assessmentViewModel.AssessmentPaper = new AssessmentPaperViewModel();
            assessmentViewModel.AssessmentPaper.AssessmentPaperId = 1;
            assessmentViewModel.AssessmentQuestions = new List<AssessmentQuestionsViewModel>();
            AssessmentQuestionsViewModel assessmentQuestionsViewModel = new AssessmentQuestionsViewModel();
            assessmentQuestionsViewModel.QuestionId = 0;
            assessmentViewModel.AssessmentQuestions.Add(assessmentQuestionsViewModel);
            var result = (ActionResult)controller.EditAssessmentQuestion(assessmentViewModel);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("Error", (result as ViewResult).ViewName);
        }
        [TestMethod]
        public void EditAssessmentQuestion_PostError()
        {
            // Arrange
            var controller = new AssessmentController(_ITrainingService, _IAssessmentService);

            // Act
            AssessmentViewModel assessmentViewModel = new AssessmentViewModel(); 
            assessmentViewModel.AssessmentPaper = new AssessmentPaperViewModel();
            assessmentViewModel.AssessmentPaper.AssessmentPaperId = 1;
            assessmentViewModel.AssessmentQuestions = null;
            
            var result = (ActionResult)controller.EditAssessmentQuestion(assessmentViewModel);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("Error", (result as ViewResult).ViewName);

        }
        [TestMethod]
        public void UseExistingAssessment()
        {
            // Arrange
            var controller = new AssessmentController(_ITrainingService, _IAssessmentService);

            // Act
            int assessmentPaperId = 1;
            int trainingNameId = 1;
            int courseId = 1;
            var result = (ActionResult)controller.UseExistingAssessment(assessmentPaperId,courseId, trainingNameId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual("UseExistingAssessment", ((ViewResult)result).ViewName);
        }
    }
}
