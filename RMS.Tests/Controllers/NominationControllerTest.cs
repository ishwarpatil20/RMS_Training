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
using RMS.Models;
using RMS.Common.BusinessEntities;


namespace RMS.Tests.Controllers
{
    [TestClass]
    public class NominationControllerTest
    {
        private readonly ITrainingRepository _service;

            public NominationControllerTest()
            {                
            }

            public NominationControllerTest(ITrainingRepository service)
            {            
                _service = service;
            }

            [TestMethod]
            public void ViewTrainingCourseDetailByID_returncorrectView_forcorrecttrainingnameid()
            {
                //Arrange           
                NominationController cntrl = new NominationController(new TrainingService(new TrainingRepository()));                                

                //Act            
                PartialViewResult actual =  cntrl.ViewTrainingCourseDetailByID(1269, false) as PartialViewResult;


                //Assert
                Assert.AreEqual(CommonConstants._PartialViewTrainingDetail, actual.ViewName);
            }


            [TestMethod]
            public void ViewTrainingCourseDetailByID_returncorrectView_forinorrecttrainingnameid()
            {
                //Arrange           
                NominationController cntrl = new NominationController(new TrainingService(new TrainingRepository()));

                //Act            
                PartialViewResult actual = cntrl.ViewTrainingCourseDetailByID(5000, false) as PartialViewResult;


                //Assert
                Assert.AreEqual(CommonConstants._PartialViewTrainingDetail, actual.ViewName);
            }

            [TestMethod]
            public void AddNomination_returncorrrectstatus_forcorrectentitties()
            {
                //Arrange 
                NominationController cntrl = new NominationController(new TrainingService(new TrainingRepository()));

                int trainingcourseID = 1207;
                int trainingnameID = 1028;

                //Initialize model     
                EmployeeNominationViewModel model = new EmployeeNominationViewModel();
                model.SelectedEmployee = 1331;
                model.SelectedPriority = 1925;
                model.SelectedPreTrainingRating = trainingnameID;
                model.Comment = "fresher batch";
                model.ObjectiveForSoftSkill = "no experience in technology";
                model.SelectedRMONominator = 0;
                model.isRMOLogin = false;


                Employee saveNominatedEmployee = new Employee();
                saveNominatedEmployee.courseID = trainingcourseID;
                saveNominatedEmployee.EmployeeID = model.SelectedEmployee;
                saveNominatedEmployee.TrainingNameID = trainingnameID;
                saveNominatedEmployee.PriorityCode = model.SelectedPriority;
                saveNominatedEmployee.PreTrainingCode = model.SelectedPreTrainingRating;
                saveNominatedEmployee.Comment = model.Comment;
                saveNominatedEmployee.ObjectiveForSoftSkill = model.ObjectiveForSoftSkill;
                saveNominatedEmployee.RMONominatorID = model.SelectedRMONominator;
                saveNominatedEmployee.IsRMOLogin = model.isRMOLogin;

                //Act            
                int status = _service.SaveNominatedEmployee(saveNominatedEmployee);

                //Assert
                Assert.AreEqual(1, 1);
            }


            [TestMethod]
            public void AddNomination_returnduplicatestatus_forduplicationadditionofnominee()
            {
                //Arrange 
                NominationController cntrl = new NominationController(new TrainingService(new TrainingRepository()));

                int trainingcourseID = 1207;
                int trainingnameID = 1028;

                //Initialize model     
                EmployeeNominationViewModel model = new EmployeeNominationViewModel();
                model.SelectedEmployee = 1331;
                model.SelectedPriority = 1925;
                model.SelectedPreTrainingRating = trainingnameID;
                model.Comment = "fresher batch";
                model.ObjectiveForSoftSkill = "no experience in technology";
                model.SelectedRMONominator = 0;
                model.isRMOLogin = false;

                Employee saveNominatedEmployee = new Employee();
                saveNominatedEmployee.courseID = trainingcourseID;
                saveNominatedEmployee.EmployeeID = model.SelectedEmployee;
                saveNominatedEmployee.TrainingNameID = trainingnameID;
                saveNominatedEmployee.PriorityCode = model.SelectedPriority;
                saveNominatedEmployee.PreTrainingCode = model.SelectedPreTrainingRating;
                saveNominatedEmployee.Comment = model.Comment;
                saveNominatedEmployee.ObjectiveForSoftSkill = model.ObjectiveForSoftSkill;
                saveNominatedEmployee.RMONominatorID = model.SelectedRMONominator;
                saveNominatedEmployee.IsRMOLogin = model.isRMOLogin;

                //Act            
                int status = _service.SaveNominatedEmployee(saveNominatedEmployee);

                //Assert
                Assert.AreEqual(4, 4);
            }


            [TestMethod]
            public void DeleteNomination_returnemployeelistonsuccessfuldeletion()
            {
                //Arrange 
                ITrainingService _service = new TrainingService(new TrainingRepository());

                int trainingcourseID = 1207;
                int trainingnameID = 1028;
                int deleteemployeeid = 1331;

                //Act            
                List<Employee> deletedEmployeeList = _service.DeleteNominatedEmployee(trainingcourseID, trainingnameID, deleteemployeeid);

                //Assert
                Assert.IsTrue(deletedEmployeeList.Count > 0);

            }            

        
            [TestMethod]
            public void UpdateNomination_returnsuccessfulstatus_forcorrecttrainingcourseandtrainingnameid()
            {
                //Arrange 
                ITrainingService _service = new TrainingService(new TrainingRepository());

                int trainingcourseID = 1207;
                int trainingnameID = 1028;
                
                EmployeeNominationViewModel model = new EmployeeNominationViewModel();
                model.SelectedEmployee = 1331;
                model.SelectedPriority = 1925;
                model.SelectedPreTrainingRating = trainingnameID;
                model.Comment = "fresher batch";
                model.ObjectiveForSoftSkill = "no experience in technology";
                model.SelectedRMONominator = 0;
                model.isRMOLogin = false;

                Employee updateNominatedEmployee = new Employee();
                updateNominatedEmployee.courseID = trainingcourseID;
                updateNominatedEmployee.EmployeeID = model.SelectedEmployee;
                updateNominatedEmployee.TrainingNameID = trainingnameID;
                updateNominatedEmployee.PriorityCode = model.SelectedPriority;
                updateNominatedEmployee.PreTrainingCode = model.SelectedPreTrainingRating;
                updateNominatedEmployee.Comment = model.Comment;
                updateNominatedEmployee.ObjectiveForSoftSkill = model.ObjectiveForSoftSkill;
                updateNominatedEmployee.RMONominatorID = model.SelectedRMONominator;
                updateNominatedEmployee.IsRMOLogin = model.isRMOLogin;


                //Act            
                int status = _service.UpdateNominatedEmployee(updateNominatedEmployee);

                //Assert
                Assert.AreEqual(1,1);

            }

        
            [TestMethod]
            public void SubmitTrainingNomination_returnsubmitedemployeelistonsuccess()
            {
                //Arrange 
                ITrainingService _service = new TrainingService(new TrainingRepository());
                int trainingcourseID = 1207;
                int trainingnameID = 1028;

                //Act            
                List<Employee> savedEmployee = _service.SubmitNominatedEmployee(trainingcourseID, trainingnameID);

                //Assert
                Assert.IsTrue(savedEmployee.Count > 0);

            }


            #region  Confirm Nomination

            [TestMethod]
            public void ConfirmTrainingNomination_returncorrectview()
            {
                //Arrange 
                NominationController cntrl = new NominationController(new TrainingService(new TrainingRepository()));

                //Act            
                ViewResult viewactual = cntrl.ViewConfirmNominationForTraining() as ViewResult;

                //Assert
                Assert.Equals(CommonConstants.ConfirmTrainingNomination, viewactual.ViewName);
            }

            [TestMethod]
            public void SaveUpdateConfirmedNomination_returnnewlyconfirmemployeelist_forselectedemployeeid()
            {
                //Arrange 
                ITrainingService _service = new TrainingService(new TrainingRepository());
                int trainingcourseID = 26;
                int trainingnameID = 1269;
                string selectedemployeeid = "1686,1384,1331";

                //Act            
                List<Employee> newlyconfirmemplist = _service.SaveUpdateNominatedEmployee(trainingnameID, trainingcourseID, selectedemployeeid);

                //Assert
                Assert.IsTrue(newlyconfirmemplist.Count > 0);
            }

            [TestMethod]
            public void SaveUpdateConfirmedNomination_returnnonconfirmemployeelist_forselectedemployeeid()
            {
                //Arrange 
                ITrainingService _service = new TrainingService(new TrainingRepository());
                int trainingcourseID = 26;
                int trainingnameID = 1269;
                string selectedemployeeid = "1384,1331";

                //Act            
                List<Employee> nonconfirmemplist = _service.SaveUpdateNominatedEmployee(trainingnameID, trainingcourseID, selectedemployeeid);

                //Assert
                Assert.IsTrue(nonconfirmemplist.Count > 0);
            }

            #endregion  Confirm Nomination
    }
}
