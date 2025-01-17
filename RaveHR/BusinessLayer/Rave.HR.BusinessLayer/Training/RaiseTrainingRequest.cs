﻿//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           RaiseTrainingRequest.cs       
//  Class:          RaiseTrainingRequest
//  Author:         Ishwar Patil
//  Date written:   08/04/2014
//  Description:    This class contains method related to Raise Training Request 

//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  08/04/2014            Ishwar Patil    n/a     Created    
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using BusinessEntities;
using Common;
using System.Data;
using Rave.HR;
using Rave.HR.BusinessLayer.Interface;
using Common.AuthorizationManager;
using System.Text;
using System.Collections;
using Common.Constants;

namespace Rave.HR.BusinessLayer.Training
{
    public class RaiseTrainingRequest
    {
        #region Private Variables

        //Declaring function names.
        const string RaiseTrainingRqst = "RaiseTrainingRequest.cs";
        const string SAVE = "Save";
        const string SAVEKSS = "SaveKSS";
        const string SAVESEMINARS = "SaveSeminars";
        const string FunctionViewTechnicalTraining = "ViewTechnicalTraining";
        const string FunctionViewSoftSkillsTraining = "ViewSoftSkillsTraining";
        const string FunctionViewKSSTraining = "ViewKSSTraining";
        const string FunctionViewSeminarsTraining = "ViewSeminarsTraining";
        const string FunctionGetTechnicalTraining = "GetTechnicalTraining";
        const string FunctionGetKSSTraining = "GetKSSTraining";
        const string FunctionDeleteKSSTraining = "DeleteKSSTraining";
        const string FunctionGetSeminarsTraining = "GetSeminarsTraining";
        const string FunctionDeleteSeminarsTraining = "DeleteSeminarsTraining";
        const string FunctionDeleteTechnicalSoftSkillsTraining = "DeleteTechnicalSoftSkillsTraining";
        
        Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest saveTrainingDL = null;
        private static BusinessEntities.RaiseTrainingRequest RaiseTrainingCollection;

        #endregion

        #region Public Method

        /// <summary>
        /// This method is used for saving the Technical Training details
        /// </summary>
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        public int Save(BusinessEntities.RaiseTrainingRequest RaiseTraining, int TechnicalTrainingID, int SoftSkillsTrainingID)
        {
            int RaiseTrainingID = 0;
            try
            {
                saveTrainingDL = new Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest();

                RaiseTrainingID = saveTrainingDL.Save(RaiseTraining, TechnicalTrainingID, SoftSkillsTrainingID);
                return RaiseTrainingID;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RaiseTrainingRqst, SAVE, EventIDConstants.TRAINING_BUSINESS_ACCESS_LAYER);
            }
        }

        /// <summary>
        /// This method is used for saving the KSS Training details
        /// </summary>
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        public int SaveKSS(BusinessEntities.RaiseTrainingRequest RaiseTraining)
        {
            int RaiseTrainingID = 0;
            try
            {
                saveTrainingDL = new Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest();

                RaiseTrainingID = saveTrainingDL.SaveKSS(RaiseTraining);
                return RaiseTrainingID;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RaiseTrainingRqst, SAVEKSS, EventIDConstants.TRAINING_BUSINESS_ACCESS_LAYER);
            }
        }

        /// <summary>
        /// This method is used for saving the Seminars Training details
        /// </summary>
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        public int SaveSeminars(BusinessEntities.RaiseTrainingRequest RaiseTraining)
        {
            int RaiseTrainingID = 0;
            try
            {
                saveTrainingDL = new Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest();

                RaiseTrainingID = saveTrainingDL.SaveSeminars(RaiseTraining);
                return RaiseTrainingID;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RaiseTrainingRqst, SAVESEMINARS, EventIDConstants.TRAINING_BUSINESS_ACCESS_LAYER);
            }
        }

        /// <summary>
        /// View the Technical Training Summary details.
        /// </summary>
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection ViewTechnicalTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining, BusinessEntities.ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount)
        {
            try
            {
                saveTrainingDL = new Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest();
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                raveHRCollection = saveTrainingDL.ViewTechnicalTraining(RaiseTraining, objParameter, ref pageCount, ref CurrentIndexCount);
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RaiseTrainingRqst, FunctionViewTechnicalTraining, EventIDConstants.TRAINING_BUSINESS_ACCESS_LAYER);
            }
        }

        /// <summary>
        /// View the SoftSkills Training Summary Details.
        /// </summary>
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection ViewSoftSkillsTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining, BusinessEntities.ParameterCriteria objParameter, ref int pageCount)
        {
            try
            {
                saveTrainingDL = new Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest();
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                raveHRCollection = saveTrainingDL.ViewSoftSkillsTraining(RaiseTraining, objParameter, ref pageCount);
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RaiseTrainingRqst, FunctionViewSoftSkillsTraining, EventIDConstants.TRAINING_BUSINESS_ACCESS_LAYER);
            }
        }

        /// <summary>
        /// Gets the Technical Training details.
        /// </summary>
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        public BusinessEntities.RaiseTrainingRequest GetTechnicalSoftSkills(int RaiseID, int TrainingTypeID)
        {
            try
            {
                RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
                saveTrainingDL = new Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest();
                RaiseTrainingCollection = saveTrainingDL.GetTechnicalSoftSkills(RaiseID, TrainingTypeID);
                return RaiseTrainingCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RaiseTrainingRqst, FunctionGetTechnicalTraining, EventIDConstants.TRAINING_BUSINESS_ACCESS_LAYER);
            }
        }

        /// <summary>
        /// Gets the KSS Training details.
        /// </summary>
        /// <param name="RaiseTraining"></param>
        /// <returns>RaiseTrainingCollection</returns>
        public BusinessEntities.RaiseTrainingRequest GetKSSTraining(int RaiseID)
        {
            try
            {
                RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
                saveTrainingDL = new Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest();
                RaiseTrainingCollection = saveTrainingDL.GetKSSTraining(RaiseID);
                return RaiseTrainingCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RaiseTrainingRqst, FunctionGetKSSTraining, EventIDConstants.TRAINING_BUSINESS_ACCESS_LAYER);
            }
        }
        
        /// <summary>
        /// Gets the Seminars Training details.
        /// </summary>
        /// <param name="RaiseTraining"></param>
        /// <returns>RaiseTrainingCollection</returns>
        public BusinessEntities.RaiseTrainingRequest GetSeminarsTraining(int RaiseID)
        {
            try
            {
                RaiseTrainingCollection = new BusinessEntities.RaiseTrainingRequest();
                saveTrainingDL = new Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest();
                RaiseTrainingCollection = saveTrainingDL.GetSeminarsTraining(RaiseID);
                return RaiseTrainingCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RaiseTrainingRqst, FunctionGetSeminarsTraining, EventIDConstants.TRAINING_BUSINESS_ACCESS_LAYER);
            }
        }

        /// <summary>
        /// This method is used for deleting the Technical Training details
        /// </summary>
        /// <param name="RaiseTraining"></param>
        /// <returns>RaiseTechnicalID</returns>
        public string DeleteTechnicalSoftSkillsTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining, int TrainingTypeID)
        {
            try
            {
                saveTrainingDL = new Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest();

                return saveTrainingDL.DeleteTechnicalSoftSkillsTraining(RaiseTraining, TrainingTypeID);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RaiseTrainingRqst, FunctionDeleteTechnicalSoftSkillsTraining, EventIDConstants.TRAINING_BUSINESS_ACCESS_LAYER);
            }
        }

        /// <summary>
        /// This method is used for deleting the KSS Training details
        /// </summary>
        /// <param name="RaiseTraining"></param>
        /// <returns>RaiseKSSID</returns>
        public string DeleteKSSTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining)
        {
            int RaiseKSSID = 0;
            try
            {
                saveTrainingDL = new Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest();
                return saveTrainingDL.DeleteKSSTraining(RaiseTraining);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RaiseTrainingRqst, FunctionDeleteKSSTraining, EventIDConstants.TRAINING_BUSINESS_ACCESS_LAYER);
            }
        }

        /// <summary>
        /// View the KSS Training Summary details.
        /// </summary>
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection ViewKSSTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining, BusinessEntities.ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount)
        {
            try
            {
                saveTrainingDL = new Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest();
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                raveHRCollection = saveTrainingDL.ViewKSSTraining(RaiseTraining, objParameter, ref pageCount, ref CurrentIndexCount);
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RaiseTrainingRqst, FunctionViewKSSTraining, EventIDConstants.TRAINING_BUSINESS_ACCESS_LAYER);
            }
        }

        /// <summary>
        /// View the Seminars Training Summary details.
        /// </summary>
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection ViewSeminarsTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining, BusinessEntities.ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount)
        {
            try
            {
                saveTrainingDL = new Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest();
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                raveHRCollection = saveTrainingDL.ViewSeminarsTraining(RaiseTraining, objParameter, ref pageCount, ref CurrentIndexCount);
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RaiseTrainingRqst, FunctionViewSeminarsTraining, EventIDConstants.TRAINING_BUSINESS_ACCESS_LAYER);
            }
        }

        /// <summary>
        /// This method is used for deleting the Seminars Training details
        /// </summary>
        /// <param name="RaiseTraining"></param>
        /// <returns>RaiseSeminarsID</returns>
        public string DeleteSeminarsTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining)
        {
            try
            {
                saveTrainingDL = new Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest();
                return saveTrainingDL.DeleteSeminarsTraining(RaiseTraining);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RaiseTrainingRqst, FunctionDeleteSeminarsTraining, EventIDConstants.TRAINING_BUSINESS_ACCESS_LAYER);
            }
        }

        /// <summary>
        /// View the KSS Training Summary details.
        /// </summary>
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection ApproveRejectViewKSSTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining, BusinessEntities.ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount)
        {
            try
            {
                saveTrainingDL = new Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest();
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                raveHRCollection = saveTrainingDL.ApproveRejectViewKSSTraining(RaiseTraining, objParameter, ref pageCount, ref CurrentIndexCount);
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RaiseTrainingRqst, "FunctionApproveRejectViewKSSTrainingViewKSSTraining", EventIDConstants.TRAINING_BUSINESS_ACCESS_LAYER);
            }
        }
       
        /// <summary>
        /// View the Technical Training Summary details.
        /// </summary>
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection ApproveRejectViewTechnicalTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining, BusinessEntities.ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount)
        {
            try
            {
                saveTrainingDL = new Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest();
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                raveHRCollection = saveTrainingDL.ApproveRejectViewTechnicalTraining(RaiseTraining, objParameter, ref pageCount, ref CurrentIndexCount);
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RaiseTrainingRqst, "FunctionApproveRejectViewTechnicalTraining", EventIDConstants.TRAINING_BUSINESS_ACCESS_LAYER);
            }
        }

        /// <summary>
        /// View the SoftSkills Training Summary Details.
        /// </summary>
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection ApproveRejectViewSoftSkillsTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining, BusinessEntities.ParameterCriteria objParameter, ref int pageCount)
        {
            try
            {
                saveTrainingDL = new Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest();
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                raveHRCollection = saveTrainingDL.ApproveRejectViewSoftSkillsTraining(RaiseTraining, objParameter, ref pageCount);
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RaiseTrainingRqst, "FunctionApproveRejectViewSoftSkillsTraining", EventIDConstants.TRAINING_BUSINESS_ACCESS_LAYER);
            }
        }

        /// <summary>
        /// View the Seminars Training Summary details.
        /// </summary>
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection ApproveRejectViewSeminarsTraining(BusinessEntities.RaiseTrainingRequest RaiseTraining, BusinessEntities.ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount)
        {
            try
            {
                saveTrainingDL = new Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest();
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                raveHRCollection = saveTrainingDL.ApproveRejectViewSeminarsTraining(RaiseTraining, objParameter, ref pageCount, ref CurrentIndexCount);
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RaiseTrainingRqst, "FunctionApproveRejectViewSeminarsTraining", EventIDConstants.TRAINING_BUSINESS_ACCESS_LAYER);
            }
        }

        public int SaveApproveRejectTrainingRequest(BusinessEntities.RaiseTrainingRequest RaiseTraining)
        {
            try
            {
                saveTrainingDL = new Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest();

                return saveTrainingDL.SaveApproveRejectTrainingRequest(RaiseTraining);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RaiseTrainingRqst, "SaveApproveRejectTrainingRequest", EventIDConstants.TRAINING_BUSINESS_ACCESS_LAYER);
            }
        }

        public string UpdateRaiseTrainingStatus(BusinessEntities.RaiseTrainingRequest RaiseTraining)
        {
            try
            {
                saveTrainingDL = new Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest();

                return saveTrainingDL.UpdateRaiseTrainingStatus(RaiseTraining);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RaiseTrainingRqst, "SaveApproveRejectTrainingRequest", EventIDConstants.TRAINING_BUSINESS_ACCESS_LAYER);
            }
        }

        //<summary>
        //Edit KSS Training Details using ATG/ATM, TM, STM Group
        //</summary>
        //<param name="RaiseTraining"></param>
        //<returns>RaiseTrainingCollection</returns>
        public DataTable GetEditKSSTrainingGroup(int UserEmpID, string Flag)
        {
            try
            {
                saveTrainingDL = new Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest();
                DataTable dt = new DataTable();
                dt = saveTrainingDL.GetEditKSSTrainingGroup(UserEmpID, Flag);
                return dt;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RaiseTrainingRqst, FunctionGetKSSTraining, EventIDConstants.TRAINING_BUSINESS_ACCESS_LAYER);
            }
        }

        public int AccessForTrainingModule(int UserEmpId)
        {
            try
            {
                saveTrainingDL = new Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest();

                return saveTrainingDL.AccessForTrainingModule(UserEmpId);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RaiseTrainingRqst, "FunctionAccessForTrainingModule", EventIDConstants.TRAINING_BUSINESS_ACCESS_LAYER);
            }
        }

        public int CheckDuplication(BusinessEntities.RaiseTrainingRequest RaiseTraining)
        {
            try
            {
                saveTrainingDL = new Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest();

                return saveTrainingDL.CheckDuplication(RaiseTraining);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RaiseTrainingRqst, "FunctionCheckDuplication", EventIDConstants.TRAINING_BUSINESS_ACCESS_LAYER);
            }
        }

        //Ishwar Patil : 19/08/2014 Start
        public DataSet GetEmailIDDetails(string UserEmpID, int Raiseid)
        {
            try
            {
                saveTrainingDL = new Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest();
                DataTable dt = new DataTable();
                return saveTrainingDL.GetEmailIDDetails(UserEmpID, Raiseid);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RaiseTrainingRqst, "FunctionGetEmailIDDetails", EventIDConstants.TRAINING_BUSINESS_ACCESS_LAYER);
            }
        }
        public DataTable GetEmailIDDetailsForKSS(string UserEmpID)
        {
            try
            {
                saveTrainingDL = new Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest();
                DataTable dt = new DataTable();
                dt = saveTrainingDL.GetEmailIDDetailsForKSS(UserEmpID);
                return dt;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RaiseTrainingRqst, "FunctionGetEmailIDDetails", EventIDConstants.TRAINING_BUSINESS_ACCESS_LAYER);
            }
        }

        public DataSet GetEmailIDForAppRejTechSoftSkill(int RaiseID)
        {
            try
            {
                saveTrainingDL = new Rave.HR.DataAccessLayer.Training.RaiseTrainingRequest();
                return saveTrainingDL.GetEmailIDForAppRejTechSoftSkill(RaiseID);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RaiseTrainingRqst, "FunctionGetEmailIDForAppRejTechSoftSkill", EventIDConstants.TRAINING_BUSINESS_ACCESS_LAYER);
            }
        }
        //Ishwar Patil : 19/08/2014 End

        #endregion
    }
}
