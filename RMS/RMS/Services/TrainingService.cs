//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2015 all rights reserved.
//
//  File:           TrainingServices.cs       
//  Author:         jagmohan.rawat
//  Date written:   13/07/2015/ 10:58:30 AM
//  Description:    TrainingServices page contain list of method interacting with Training repository which fetches data from repository and pass it to model.
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
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Domain.Entities;
using Domain.Interfaces;
using RMS.Common.BusinessEntities;
using Services.Interfaces;


namespace Services
{
    public class TrainingService : ITrainingService
    {

        private readonly ITrainingModel _repository;

        public TrainingService(ITrainingModel repository)
        {
            _repository = repository;
        }





        #region  Method


        /// </summary>
        /// This method is used for saving the Technical Training details
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        public int Save(TrainingModel RaiseTraining, int TechnicalTrainingID, int SoftSkillsTrainingID)
        {
            return _repository.Save(RaiseTraining,TechnicalTrainingID,SoftSkillsTrainingID);
        }


        /// </summary>
        /// This method is used for saving the KSS Training details
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        public int SaveKSS(TrainingModel RaiseTraining)
        {
            return _repository.SaveKSS(RaiseTraining);
        }
    


        /// </summary>
        /// This method is used for saving the Seminars Training details
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        public int SaveSeminars(TrainingModel RaiseTraining)
        {
            return _repository.SaveSeminars(RaiseTraining);
        }

        /// <summary>
        /// Get Technical Training 
        /// </summary>
        /// <returns>Collection</returns>
        public TrainingModel GetTechnicalSoftSkills(int RaiseID, int TrainingTypeID)
        {
            return _repository.GetTechnicalSoftSkills(RaiseID, TrainingTypeID);
        }


        /// <summary>
        /// Retrive Technical Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        //public List<TrainingModel> ViewTechnicalTraining(TrainingModel RaiseTraining, ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount)
        //{
        //    return _repository.ViewTechnicalTraining(RaiseTraining, objParameter, ref pageCount, ref CurrentIndexCount);
        //}
        public List<TrainingModel> ViewTechnicalTraining(TrainingModel RaiseTraining)
        {
            return _repository.ViewTechnicalTraining(RaiseTraining);
        }

    
        //public List<TrainingModel> ViewTechnicalTraining(TrainingModel RaiseTraining, ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount);

        /// <summary>
        /// Retrive SoftSkills Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public List<TrainingModel> ViewSoftSkillsTraining(TrainingModel RaiseTraining)
        {
            return _repository.ViewSoftSkillsTraining(RaiseTraining);
        }

        /// </summary>
        /// This method is used for deleteing the Techinical Training details
        /// <param name="RaiseTraining"></param>
        /// <returns>RaiseTechnicalID</returns>
        public string DeleteTechnicalSoftSkillsTraining(TrainingModel RaiseTraining, int TrainingTypeID)
        {
            return _repository.DeleteTechnicalSoftSkillsTraining(RaiseTraining, TrainingTypeID);
        }

        /// </summary>
        /// This method is used for deleteing the KSS Training details
        /// <param name="RaiseTraining"></param>
        /// <returns>RaiseKSSID</returns>
        public string DeleteKSSTraining(TrainingModel RaiseTraining)
        {
            return _repository.DeleteKSSTraining(RaiseTraining);
        }


        /// </summary>
        /// This method is used for deleteing the Seminars Training details
        /// <param name="RaiseTraining"></param>
        /// <returns>RaiseSeminarsID</returns>
        public string DeleteSeminarsTraining(TrainingModel RaiseTraining)
        {
            return _repository.DeleteSeminarsTraining(RaiseTraining);
        }

        /// <summary>
        /// Retrive KSS Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public List<TrainingModel> ViewKSSTraining(TrainingModel RaiseTraining)
        {
            return _repository.ViewKSSTraining(RaiseTraining);
        }



        /// <summary>
        /// Get KSS Training 
        /// </summary>
        /// <returns>Collection</returns>
        public TrainingModel GetKSSTraining(int RaiseID)
        {
            return _repository.GetKSSTraining(RaiseID);
        }


        /// <summary>
        /// Retrive Seminars Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public List<TrainingModel> ViewSeminarsTraining(TrainingModel RaiseTraining)
        {
            return _repository.ViewSeminarsTraining(RaiseTraining);
        }

        /// <summary>
        /// Get Seminars Training 
        /// </summary>
        /// <returns>Collection</returns>
        public TrainingModel GetSeminarsTraining(int RaiseID)
        {
            return _repository.GetSeminarsTraining(RaiseID);
        }
            


        /// <summary>
        /// Retrive KSS Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public List<TrainingModel> ApproveRejectViewKSSTraining(TrainingModel RaiseTraining, ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount)
        {
            return _repository.ApproveRejectViewKSSTraining(RaiseTraining, objParameter, ref pageCount, ref CurrentIndexCount);
        }


        /// <summary>
        /// Retrive Technical Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public List<TrainingModel> ApproveRejectViewTechnicalTraining(TrainingModel RaiseTraining, ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount)
        {
            return _repository.ApproveRejectViewTechnicalTraining(RaiseTraining, objParameter, ref pageCount, ref CurrentIndexCount);
        }


        /// <summary>
        /// Retrive SoftSkills Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public List<TrainingModel> ApproveRejectViewSoftSkillsTraining(TrainingModel RaiseTraining, ParameterCriteria objParameter, ref int pageCount)
        {
            return _repository.ApproveRejectViewSoftSkillsTraining(RaiseTraining, objParameter, ref pageCount);
        }


        /// <summary>
        /// Retrive Seminars Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public List<TrainingModel> ApproveRejectViewSeminarsTraining(TrainingModel RaiseTraining, ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount)

        {
            return _repository.ApproveRejectViewSeminarsTraining(RaiseTraining, objParameter, ref pageCount, ref CurrentIndexCount);
        }


        public int SaveApproveRejectTrainingRequest(TrainingModel RaiseTraining)
        {
            return _repository.SaveApproveRejectTrainingRequest(RaiseTraining);
        }



        public string UpdateRaiseTrainingStatus(TrainingModel RaiseTraining)
        {
            return _repository.UpdateRaiseTrainingStatus(RaiseTraining);
        }

        /// <summary>
        /// Edit KSS Training Details using ATG/ATM, TM, STM Group
        /// </summary>
        /// <returns>Collection</returns>
        public DataTable GetEditKSSTrainingGroup(int UserEmpID, string Flag)
        {
            return _repository.GetEditKSSTrainingGroup(UserEmpID, Flag);
        }

        public int AccessForTrainingModule(int UserEmpId)
        {
            return _repository.AccessForTrainingModule(UserEmpId);
        }

        public int CheckDuplication(TrainingModel RaiseTraining)
        {
            return _repository.CheckDuplication(RaiseTraining);
        }

        public DataSet GetEmailIDDetails(string UserEmpID, int Raiseid)
        {
            return _repository.GetEmailIDDetails(UserEmpID, Raiseid);
        }

        public DataTable GetEmailIDDetailsForKSS(string UserEmpID)
        {
            return _repository.GetEmailIDDetailsForKSS(UserEmpID);
        }

        public DataSet GetEmailIDForAppRejTechSoftSkill(int RaiseID)
        {
            return _repository.GetEmailIDForAppRejTechSoftSkill(RaiseID);
        }

        //Ishwar Patil : 19/08/2014 End
        #endregion  Method







    }
}
