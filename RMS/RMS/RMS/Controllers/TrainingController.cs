//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2015 all rights reserved.
//
//  File:           TrainingController.cs       
//  Author:         jagmohan.rawat
//  Date written:   13/07/2015/ 10:58:30 AM
//  Description:    Training controller page is use for Creating, Approving and viewing raise training request.
//
//  Amendments
//  Date                        Who                 Ref     Description
//  ----                        -----------         ---     -----------
//  13/07/2015/ 10:58:30 AM     jagmohan.rawat      n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using System.Configuration;
using RMS.Common.Constants;
using RMS.Common;
using RMS.Helpers;

namespace RMS.Controllers
{    
    [CheckAccess]    
    public class TrainingController : ErrorController
    {
        private List<TrainingModel> viewTrainingListModel = new List<TrainingModel>();
        private readonly ITrainingModel _service;
        private int trainingID;

        /// <summary>
        /// Constructor for creating reference for Training repository
        /// </summary>
        /// <value>Interface TrainingModel</value>
        public TrainingController(ITrainingModel service)
        {            
            _service = service;
        }


        /// <summary>
        /// Get the list of all request training raise by manager
        /// </summary>
        /// <value>TrainingTypeID</value>       
        public ActionResult ViewTechnicalTrainingRequest(string trainingTypeID)
        {            
            //create model and set all required field
            TrainingModel objTrainingmodel = new TrainingModel(trainingTypeID);
            
            //pass model to get the list of all training raise by manager
            var model = _service.ViewTechnicalTraining(objTrainingmodel);

            ViewData[CommonConstants.TrainingType] = string.IsNullOrWhiteSpace(trainingTypeID) ? CommonConstants.TechnicalTrainingID.ToString() : trainingTypeID;
            return View(CommonConstants.viewtrainingrequest, model);
        }

        /// <summary>
        /// Get the list of all request training list and bind the grid.
        /// </summary>
        /// <value>TrainingTypeID</value>       
        public ActionResult ViewTechnicalTrainingRequestGrid(string trainingTypeID)
        {
            //create model and set all required field
            trainingID = Convert.ToInt32(trainingTypeID);            
            TrainingModel objTrainingmodel = new TrainingModel(trainingTypeID);
            
            //check training type selected by user
            switch (trainingID)
            {
                    
                case (CommonConstants.TechnicalTrainingID):
                    //pass model and get the list of all technical training raise by manager
                    viewTrainingListModel = _service.ViewTechnicalTraining(objTrainingmodel);            
                    break;
                case (CommonConstants.SoftSkillsTrainingID):
                    //pass model and get the list of all Softskill training raise by manager
                    viewTrainingListModel = _service.ViewSoftSkillsTraining(objTrainingmodel);            
                    break;
                case (CommonConstants.SeminarsID):
                    //pass model and get the list of all seminar training raise by manager
                    viewTrainingListModel = _service.ViewSeminarsTraining(objTrainingmodel);            
                    break;
                case (CommonConstants.KSSID):
                    //pass model and get the list of all KSS training raise by manager
                    viewTrainingListModel = _service.ViewKSSTraining(objTrainingmodel);                                
                    break;
            }                        
            
            //set training type ID to be used for diaplaying training type column
            ViewData[CommonConstants.TrainingType] = string.IsNullOrWhiteSpace(trainingTypeID) ? CommonConstants.TechnicalTrainingID.ToString() : trainingTypeID;

            //set message on view page for required operation
            ViewData[CommonConstants.Result] = TempData[CommonConstants.Result];
            return View(CommonConstants.partialView_viewTrainingGrid, viewTrainingListModel);
        }


        /// <summary>
        /// Delete particular request training raised by manager
        /// </summary>
        /// <value>RaiseID</value>
        /// <value>TrainingTypeID</value>
        public ActionResult DeleteTrainingRequest(string raiseID, string trainingTypeID)
        {

            //initialize model
            int trainingID = Convert.ToInt32(trainingTypeID);
            TrainingModel RaiseTraining = new TrainingModel();
            RaiseTraining.RaiseID = Convert.ToInt32(raiseID);
            RaiseTraining.Status = "0";
            Master objmaster = new Master();
            RaiseTraining.UserEmpId = objmaster.GetEmployeeIDByEmailID();

            //check training type selected by user
            string result = string.Empty;
            switch (trainingID)
            {
                case (CommonConstants.TechnicalTrainingID):
                    //Delete technical training and send email to managers and trainee
                    result = _service.DeleteTechnicalSoftSkillsTraining(RaiseTraining, trainingID);
                    EmailHelper.SendMailForTechSoftSkillDeleted(RaiseTraining.RaiseID, _service.GetEmailIDForAppRejTechSoftSkill(RaiseTraining.RaiseID));
                    break;
                case (CommonConstants.SoftSkillsTrainingID):
                    //Delete technical training and send email to managers and trainee
                    result = _service.DeleteTechnicalSoftSkillsTraining(RaiseTraining, trainingID);
                    EmailHelper.SendMailForTechSoftSkillDeleted(RaiseTraining.RaiseID, _service.GetEmailIDForAppRejTechSoftSkill(RaiseTraining.RaiseID));
                    break;  
                case (CommonConstants.SeminarsID):
                    //Delete seminar training and send email to managers and trainee
                    result = _service.DeleteSeminarsTraining(RaiseTraining);
                    EmailHelper.SendMailForSeminarDeleted(RaiseTraining.RaiseID, _service.GetEmailIDForAppRejTechSoftSkill(RaiseTraining.RaiseID));
                    break;
                case (CommonConstants.KSSID):
                    //pass model and delete kss training
                    result = _service.DeleteKSSTraining(RaiseTraining);
                    break;
            }

            //set message on view page returned from above operation
            TempData[CommonConstants.Result] = string.Format(CommonConstants.StringFormat, result, CommonConstants.Request_Deleted);
            return RedirectToAction(CommonConstants.ViewTechnicalTrainingRequestGrid, new { trainingTypeID = trainingTypeID });
        }

        /// <summary>
        /// Close the request training raised by manager
        /// </summary>
        /// <value>RaiseID</value>
        /// <value>TrainingTypeID</value>
        public ActionResult CloseTrainingRequest(string raiseID, string trainingTypeID)
        {
            int trainingID = Convert.ToInt32(trainingTypeID);

            //Initialize model
            TrainingModel RaiseTraining = new TrainingModel();
            RaiseTraining.RaiseID = Convert.ToInt32(raiseID);
            RaiseTraining.Status = CommonConstants.Closed;
            RaiseTraining.Comments = string.Empty;
            Master objmaster = new Master();
            RaiseTraining.UserEmpId = objmaster.GetEmployeeIDByEmailID();

            //pass model and delete technical training
            string result=string.Empty;
            result = _service.UpdateRaiseTrainingStatus(RaiseTraining);

            //set message on view page returned from above operation
            TempData[CommonConstants.Result] = string.Format(CommonConstants.StringFormat, result, CommonConstants.Request_Closed);
            return RedirectToAction(CommonConstants.ViewTechnicalTrainingRequestGrid, new { trainingTypeID = trainingTypeID });
        }




       
    }
}
