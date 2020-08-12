//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2015 all rights reserved.
//
//  File:           TrainingModel.cs       
//  Author:         jagmohan.rawat
//  Date written:   13/07/2015/ 10:58:30 AM
//  Description:    TrainingModel page contain validation and other business logic binding to view
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
using Domain.Entities;
using RMS.Common;
using RMS.Common.AuthorizationManager;
using RMS.Common.Constants;

namespace Domain.Entities
{
    public class TrainingModel
    {
        public TrainingModel() { }

        /// <summary>
        /// Get Training Model
        /// </summary>        
        public TrainingModel(string trainingType)
        {             
             Master objMaster = new Master();
             this.UserEmpId = objMaster.GetEmployeeIDByEmailID(); //objMaster.GetEmployeeID(UserMailId);
             this.RaiseID = CommonConstants.DefaultRaiseID;
             this.TrainingType = string.IsNullOrWhiteSpace(trainingType) ? CommonConstants.TechnicalTrainingID.ToString() : trainingType;
             this.Priority = CommonConstants.DefaultFlagZero.ToString();
             this.Status = CommonConstants.DefaultFlagZero.ToString();
             this.RequestedBy = CommonConstants.DefaultFlagZero.ToString();
             this.Quarter = CommonConstants.DefaultFlagZero.ToString();
        }

        /// <summary>
        /// Gets or sets agenda
        /// </summary>        
        public string Agenda { get; set; }

        /// <summary>
        /// Gets or sets BusinessImpact
        /// </summary>        
        public string BusinessImpact { get; set; }
        /// <summary>
        /// Gets or sets Category
        /// </summary>        
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets Comments
        /// </summary>        
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets CreatedBy
        /// </summary>        
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets CreatedByEmailId
        /// </summary>        
        public string CreatedByEmailId { get; set; }

        /// <summary>
        /// Gets or sets CreateDate
        /// </summary>        
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets Date
        /// </summary>        
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets Flag
        /// </summary>        
        public int Flag { get; set; }

        /// <summary>
        /// Gets or sets FlagPriority
        /// </summary>        
        public int FlagPriority { get; set; }

        /// <summary>
        /// Gets or sets FlagQuarter
        /// </summary>        
        public int FlagQuarter { get; set; }

        /// <summary>
        /// Gets or sets FlagRequestedBy
        /// </summary>        
        public int FlagRequestedby { get; set; }

        /// <summary>
        /// Gets or sets FlagStatus
        /// </summary>        
        public int FlagStatus { get; set; }

        /// <summary>
        /// Gets or sets NameOfParticipant
        /// </summary>        
        public string NameOfParticipant { get; set; }

        /// <summary>
        /// Gets or sets NameOfParticipantID
        /// </summary>        
        public string NameOfParticipantID { get; set; }

        /// <summary>
        /// Gets or sets NoOfParticipant
        /// </summary>        
        public string NoOfParticipant { get; set; }

        /// <summary>
        /// Gets or sets OutTechnicalID
        /// </summary>        
        public int OutTechnicalID { get; set; }

        /// <summary>
        /// Gets or sets Presenter
        /// </summary>        
        public string Presenter { get; set; }

        /// <summary>
        /// Gets or sets PresenterID
        /// </summary>        
        public string PresenterID { get; set; }

        /// <summary>
        /// Gets or sets Priority
        /// </summary>        
        public string Priority { get; set; }

        /// <summary>
        /// Gets or sets Quarter
        /// </summary>        
        public string Quarter { get; set; }

        /// <summary>
        /// Gets or sets RaiseDetailsID
        /// </summary>        
        public int RaiseDetailsId { get; set; }

        /// <summary>
        /// Gets or sets RaiseID
        /// </summary>        
        public int RaiseID { get; set; }

        /// <summary>
        /// Gets or sets RequestedBy
        /// </summary>        
        public string RequestedBy { get; set; }

        /// <summary>
        /// Gets or sets RequestedByID
        /// </summary>        
        public int RequestedByID { get; set; }

        /// <summary>
        /// Gets or sets SeminarsName
        /// </summary>        
        public string SeminarsName { get; set; }

        /// <summary>
        /// Gets or sets SerialNo
        /// </summary>        
        public int SerialNo { get; set; }

        /// <summary>
        /// Gets or sets Status
        /// </summary>        
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets StatusName
        /// </summary>        
        public string StatusName { get; set; }

        /// <summary>
        /// Gets or sets Topic
        /// </summary>        
        public string Topic { get; set; }

        /// <summary>
        /// Gets or sets TrainingName
        /// </summary>        
        public string TrainingName { get; set; }

        /// <summary>
        /// Gets or sets TrainingNameOther
        /// </summary>        
        public string TrainingNameOther { get; set; }

        /// <summary>
        /// Gets or sets TrainingStatus
        /// </summary>        
        public string TrainingStatus { get; set; }

        /// <summary>
        /// Gets or sets TrainingType
        /// </summary>        
        public string TrainingType { get; set; }

        /// <summary>
        /// Gets or sets Type
        /// </summary>        
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets URL
        /// </summary>        
        public string URL { get; set; }

        /// <summary>
        /// Gets or sets UserEmpID
        /// </summary>        
        public int UserEmpId { get; set; }

        /// <summary>
        /// Gets or sets UserMailID
        /// </summary>        
        public string UserMailId{ get; set; }


        /// <summary>
        /// Gets or sets IsDeleteEnable
        /// </summary>        
        public Boolean IsDeleteEnable { get; set; }
        
    }   

}
