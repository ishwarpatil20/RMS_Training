using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RMS;
using RMS.Controllers;
using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure;
using System.Configuration;
using RMS.Common.Constants;
using RMS.Common;
using System.Web;
using System.Security.Principal;
using System.Web.Routing;
using System.IO;
using System.Web.SessionState;
using System.Reflection;
using System.Web.Http;
using Services;
using Services.Interfaces;


namespace RMS.Tests.Controllers
{
    [TestClass]
    public class TrainingControllerTest
    {
        private readonly ITrainingRepository _service;

            public TrainingControllerTest()
            {                
            } 

            public TrainingControllerTest(ITrainingRepository service)
            {            
                _service = service;
            }

        [TestMethod]
        public void ViewTechnicalTrainingRequest_returncorrectView_forCorrectTrainingTypeID()
        {                
            //Arrange           
            TrainingController cntrl = new TrainingController(new TrainingService(new TrainingRepository()));
            //var context = new ControllerContext(MockHttpContext("1331"), new RouteData(), cntrl);                                                        
            //cntrl.ControllerContext = context;

            //HttpRequest httpRequest = new HttpRequest("", "http://mySomething", "");
            //StringWriter stringWriter = new StringWriter();
            //HttpResponse httpResponse = new HttpResponse(stringWriter);
            //HttpContext httpContextMock = new HttpContext(httpRequest, httpResponse);
            //cntrl.ControllerContext = new ControllerContext(new HttpContextWrapper(httpContextMock), new RouteData(), cntrl);

            HttpContext.Current = FakeHttpContext();
            
            //Act            
            ViewResult actual = cntrl.ViewTechnicalTrainingRequest("1208") as ViewResult;


            //Assert
            Assert.AreEqual(CommonConstants.viewtrainingrequest, actual.ViewName);
            
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
            var identity = new GenericIdentity("RAVE-TECH.CO.IN\\jagmohan.rawat");
            var principal = new GenericPrincipal(identity, roles);
            httpContext.User = principal;            

            return httpContext;
        }
      
        [TestMethod]
        public void ViewTechnicalTrainingRequest_returncorrectView_forInCorrectTrainingTypeID()
        {
            //Arrange            
            TrainingController cntrl = new TrainingController(new TrainingService(new TrainingRepository()));
            HttpContext.Current = FakeHttpContext();

            //Act                        
            ViewResult actual;
            actual = cntrl.ViewTechnicalTrainingRequest("1204") as ViewResult;


            //Assert
            Assert.AreEqual(CommonConstants.viewtrainingrequest, actual.ViewName);

        }

        [TestMethod]
        public void ViewTechnicalTrainingRequestGrid_returncorrectView_forInCorrectTrainingTypeID()
        {
            //Arrange            
            TrainingController cntrl = new TrainingController(new TrainingService(new TrainingRepository()));
            HttpContext.Current = FakeHttpContext();

            //Act                        
            ViewResult actual;
            actual = cntrl.ViewTechnicalTrainingRequestGrid("1208") as ViewResult;

            //Assert
            Assert.AreEqual(CommonConstants.partialView_viewTrainingGrid, actual.ViewName);
        }
       

        [TestMethod]
        public void DeleteTrainingRequest_returncorrectView_CorrectRaiseIDAndTrainingTypeID()
        {
            //Arrange
            TrainingController cntrl = new TrainingController(new TrainingService(new TrainingRepository()));
            HttpContext.Current = FakeHttpContext();
          
            //Act                        
            ViewResult actual;
            actual = cntrl.DeleteTrainingRequest("12", "1208") as ViewResult;


            //Assert
            Assert.IsNull(actual);
            
        }

        [TestMethod]
        public void DeleteTrainingRequest_returncorrectView_InCorrectRaiseIDAndCorrectTrainingTypeID()
        {
            //Arrange
            TrainingController cntrl = new TrainingController(new TrainingService(new TrainingRepository()));
            HttpContext.Current = FakeHttpContext();

            //Act                        
            ViewResult actual;
            actual = cntrl.DeleteTrainingRequest("00", "1208") as ViewResult;


            //Assert
            Assert.IsNull(actual);

        }


        [TestMethod]
        public void DeleteTrainingRequest_returncorrectView_InCorrectRaiseIDAndInCorrectTrainingTypeID()
        {
            //Arrange
            TrainingController cntrl = new TrainingController(new TrainingService(new TrainingRepository()));
            HttpContext.Current = FakeHttpContext();

            //Act                        
            ViewResult actual;
            actual = cntrl.DeleteTrainingRequest("00", "0000") as ViewResult;


            //Assert
            Assert.IsNull(actual);

        }


        [TestMethod]
        public void DeleteTrainingRequest_returncorrectView_CorrectRaiseIDAndInCorrectTrainingTypeID()
        {
            //Arrange
            TrainingController cntrl = new TrainingController(new TrainingService(new TrainingRepository()));
            HttpContext.Current = FakeHttpContext();

            //Act                        
            ViewResult actual;
            actual = cntrl.DeleteTrainingRequest("12", "1204") as ViewResult;


            //Assert
            Assert.IsNull(actual);

        }


        [TestMethod]
        public void CloseTrainingRequest_returncorrectView_CorrectRaiseIDAndInCorrectTrainingTypeID()
        {
            //Arrange
            TrainingController cntrl = new TrainingController(new TrainingService(new TrainingRepository()));
            HttpContext.Current = FakeHttpContext();

            //Act                        
            ViewResult actual;
            actual = cntrl.CloseTrainingRequest("12", "1204") as ViewResult;


            //Assert
            Assert.IsNull(actual);
        }        

        [TestMethod]
        public void CloseTrainingRequest_returncorrectView_CorrectRaiseIDAndTrainingTypeID()
        {
            //Arrange
            TrainingController cntrl = new TrainingController(new TrainingService(new TrainingRepository()));
            HttpContext.Current = FakeHttpContext();

            //Act                        
            ViewResult actual;
            actual = cntrl.CloseTrainingRequest("12", "1208") as ViewResult;


            //Assert
            Assert.IsNull(actual);

        }

        [TestMethod]
        public void CloseTrainingRequest_returncorrectView_InCorrectRaiseIDAndCorrectTrainingTypeID()
        {
            //Arrange
            TrainingController cntrl = new TrainingController(new TrainingService(new TrainingRepository()));
            HttpContext.Current = FakeHttpContext();

            //Act                        
            ViewResult actual;
            actual = cntrl.CloseTrainingRequest("00", "1208") as ViewResult;


            //Assert
            Assert.IsNull(actual);

        }


        [TestMethod]
        public void CloseTrainingRequest_returncorrectView_InCorrectRaiseIDAndInCorrectTrainingTypeID()
        {
            //Arrange
            TrainingController cntrl = new TrainingController(new TrainingService(new TrainingRepository()));
            HttpContext.Current = FakeHttpContext();

            //Act                        
            ViewResult actual;
            actual = cntrl.CloseTrainingRequest("00", "0000") as ViewResult;


            //Assert
            Assert.IsNull(actual);

        }      
    }
}
