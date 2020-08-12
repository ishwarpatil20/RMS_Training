//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2015 all rights reserved.
//
//  File:           ITrainingModel.cs       
//  Author:         jagmohan.rawat
//  Date written:   13/07/2015/ 10:58:30 AM
//  Description:    ITrainingModel page contain list of abstract member to be implemented in Training Repository
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
using RMS.Common.BusinessEntities;

namespace Domain.Interfaces
{
    public interface ITrainingModel
    {
       
        #region  Method


        /// </summary>
        /// This method is used for saving the Technical Training details
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        int Save(TrainingModel RaiseTraining, int TechnicalTrainingID, int SoftSkillsTrainingID);


        /// </summary>
        /// This method is used for saving the KSS Training details
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
         int SaveKSS(TrainingModel RaiseTraining);

         /// </summary>
        /// This method is used for saving the Seminars Training details
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
         int SaveSeminars(TrainingModel RaiseTraining);

        /// <summary>
        /// Get Technical Training 
        /// </summary>
        /// <returns>Collection</returns>
         TrainingModel GetTechnicalSoftSkills(int RaiseID, int TrainingTypeID);


        /// <summary>
        /// Retrive Technical Training summary.
        /// </summary>
        /// <returns>Collection</returns>
         //List<TrainingModel> ViewTechnicalTraining(TrainingModel RaiseTraining, ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount);         
         List<TrainingModel> ViewTechnicalTraining(TrainingModel RaiseTraining);

                /// <summary>
        /// Retrive SoftSkills Training summary.
        /// </summary>
        /// <returns>Collection</returns>
         List<TrainingModel> ViewSoftSkillsTraining(TrainingModel RaiseTraining);

        /// </summary>
        /// This method is used for deleteing the Techinical Training details
        /// <param name="RaiseTraining"></param>
        /// <returns>RaiseTechnicalID</returns>
         string DeleteTechnicalSoftSkillsTraining(TrainingModel RaiseTraining, int TrainingTypeID);

        /// </summary>
        /// This method is used for deleteing the KSS Training details
        /// <param name="RaiseTraining"></param>
        /// <returns>RaiseKSSID</returns>
         string DeleteKSSTraining(TrainingModel RaiseTraining);


        /// </summary>
        /// This method is used for deleteing the Seminars Training details
        /// <param name="RaiseTraining"></param>
        /// <returns>RaiseSeminarsID</returns>
         string DeleteSeminarsTraining(TrainingModel RaiseTraining);

        /// <summary>
        /// Retrive KSS Training summary.
        /// </summary>
        /// <returns>Collection</returns>
         List<TrainingModel> ViewKSSTraining(TrainingModel RaiseTraining);


                /// <summary>
        /// Get KSS Training 
        /// </summary>
        /// <returns>Collection</returns>
         TrainingModel GetKSSTraining(int RaiseID);


        /// <summary>
        /// Retrive Seminars Training summary.
        /// </summary>
        /// <returns>Collection</returns>
         List<TrainingModel> ViewSeminarsTraining(TrainingModel RaiseTraining);

                /// <summary>
        /// Get Seminars Training 
        /// </summary>
        /// <returns>Collection</returns>
         TrainingModel GetSeminarsTraining(int RaiseID);


        /// <summary>
        /// Retrive KSS Training summary.
        /// </summary>
        /// <returns>Collection</returns>
         List<TrainingModel> ApproveRejectViewKSSTraining(TrainingModel RaiseTraining, ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount);


                /// <summary>
        /// Retrive Technical Training summary.
        /// </summary>
        /// <returns>Collection</returns>
         List<TrainingModel> ApproveRejectViewTechnicalTraining(TrainingModel RaiseTraining, ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount);


        /// <summary>
        /// Retrive SoftSkills Training summary.
        /// </summary>
        /// <returns>Collection</returns>
         List<TrainingModel> ApproveRejectViewSoftSkillsTraining(TrainingModel RaiseTraining, ParameterCriteria objParameter, ref int pageCount);


        /// <summary>
        /// Retrive Seminars Training summary.
        /// </summary>
        /// <returns>Collection</returns>
         List<TrainingModel> ApproveRejectViewSeminarsTraining(TrainingModel RaiseTraining, ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount);


         int SaveApproveRejectTrainingRequest(TrainingModel RaiseTraining);


         string UpdateRaiseTrainingStatus(TrainingModel RaiseTraining);

        /// <summary>
        /// Edit KSS Training Details using ATG/ATM, TM, STM Group
        /// </summary>
        /// <returns>Collection</returns>
         DataTable GetEditKSSTrainingGroup(int UserEmpID, string Flag);

         int AccessForTrainingModule(int UserEmpId);

         int CheckDuplication(TrainingModel RaiseTraining);

         DataSet GetEmailIDDetails(string UserEmpID, int Raiseid);

         DataTable GetEmailIDDetailsForKSS(string UserEmpID);

         DataSet GetEmailIDForAppRejTechSoftSkill(int RaiseID);

        //Ishwar Patil : 19/08/2014 End
        #endregion  Method


    }
}
