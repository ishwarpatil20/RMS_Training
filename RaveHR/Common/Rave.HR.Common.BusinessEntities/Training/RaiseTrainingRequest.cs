//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           RaiseTrainingRequest.cs       
//  Class:          RaiseTrainingRequest
//  Author:         Ishwar Patil
//  Date written:   08/04/2014
//  Description:    This class contains properties related to Raise Training Request 

//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  08/04/2014            Ishwar Patil    n/a     Created    
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BusinessEntities
{
    [Serializable]
    public class RaiseTrainingRequest: CollectionBase
    {
        #region Field members

        /// <summary>
        /// intFlag
        /// </summary>
        public int Flag { get; set; }

        /// <summary>
        /// Raise  ID
        /// </summary>
        public int RaiseID { get; set; }

        /// <summary>
        /// Raise Details ID
        /// </summary>
        public int RaiseDetailsId { get; set; }

        /// <summary>
        /// Emp ID
        /// </summary>
        public int UserEmpId { get; set; }
        
        /// <summary>
        /// Sr.No.
        /// </summary>
        public int SerialNo { get; set; }

        /// <summary>
        /// Define iTrainingType property
        /// </summary>
        public string TrainingType
        { get; set; }

        /// <summary>
        /// Define iTrainingStatus property
        /// </summary>
        public string TrainingStatus
        { get; set; }

        /// <summary>
        /// Define strTrainingName property
        /// </summary>
        public string TrainingName
        { get; set; }

        /// <summary>
        /// Define strTrainingNameOther property
        /// </summary>
        public string TrainingNameOther
        { get; set; }

        /// <summary>
        /// Define iQuarter property
        /// </summary>
        public string Quarter
        { get; set; }

        /// <summary>
        /// Define iNoOfParticipant property
        /// </summary>
        public string NoOfParticipant
        { get; set; }

        /// <summary>
        /// Define iCategory property
        /// </summary>
        public string Category
        { get; set; }

        /// <summary>
        /// Define iRequestedBy property
        /// </summary>
        public string RequestedBy
        { get; set; }

        /// <summary>
        /// RequestedBy ID
        /// </summary>
        public int RequestedByID { get; set; }

        /// <summary>
        /// Define iPriority property
        /// </summary>
        public string Priority
        { get; set; }
        
        /// <summary>
        /// Define strBusinessImpact property
        /// </summary>
        public string BusinessImpact
        { get; set; }
        
        /// <summary>
        /// Define strComments property
        /// </summary>
        public string Comments
        { get; set; }
        
        /// <summary>
        /// Define iCreatedBy property
        /// </summary>
        public int CreatedBy
        { get; set; }
            
        /// <summary>
        /// Define strCreatedByEmailId property
        /// </summary>
        public string CreatedByEmailId
        { get; set; }

        /// <summary>
        /// Define iOutTechnicalID property
        /// </summary>
        public int OutTechnicalID
        { get; set; }

        /// <summary>
        /// Define iType property
        /// </summary>
        public string Type
        { get; set; }

        /// <summary>
        /// Define strTopic property
        /// </summary>
        public string Topic
        { get; set; }

        /// <summary>
        /// Define strAgenda property
        /// </summary>
        public string Agenda
        { get; set; }

        /// <summary>
        /// Define strDate property
        /// </summary>
        public DateTime Date
        { get; set; }

        /// <summary>
        /// Define strPresenterID property
        /// </summary>
        public string PresenterID
        { get; set; }

        /// <summary>
        /// Define strPresenter property
        /// </summary>
        public string Presenter
        { get; set; }

        /// <summary>
        /// Define dtDate property
        /// </summary>
        public DateTime CreatedDate
        { get; set; }

        /// <summary>
        /// Define strSeminarsName property
        /// </summary>
        public string SeminarsName
        { get; set; }

        /// <summary>
        /// Define strNameOfParticipantID property
        /// </summary>
        public string NameOfParticipantID
        { get; set; }

        /// <summary>
        /// Define strNameOfParticipant property
        /// </summary>
        public string NameOfParticipant
        { get; set; }

        /// <summary>
        /// Define strURL property
        /// </summary>
        public string URL
        { get; set; }

        /// <summary>
        /// Define strStatus property
        /// </summary>
        public string Status
        { get; set; }

        /// <summary>
        /// Define strStatusName property
        /// </summary>
        public string StatusName
        { get; set; }

        /// <summary>
        /// FlagStatus
        /// </summary>
        public int FlagStatus { get; set; }

        /// <summary>
        /// FlagPriority
        /// </summary>
        public int FlagPriority { get; set; }

        /// <summary>
        /// FlagRequestedby
        /// </summary>
        public int FlagRequestedby { get; set; }

        /// <summary>
        /// FlagQuarter
        /// </summary>
        public int FlagQuarter { get; set; }

        #endregion
    }
}
