//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           ResourcePlanDuration.cs       
//  Class:          ResourcePlanDuration
//  Author:         prashant.mala
//  Date written:   25/08/2009 11:29:30 PM
//  Description:    This class contains properties related to Resource Plan module. 
//                  These business entities used for CreateRP.aspx, EditRP.aspx, ApproveRejectRP.aspx etc.
//

//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  25/08/2009 11:29:30 PM  prashant.mala    n/a     Created    
//
//------------------------------------------------------------------------------

using System;

namespace BusinessEntities
{
    [Serializable]
    public class ResourcePlanDuration : ResourcePlanDetail
    {
        #region Field members

        /// <summary>
        /// Define the Resource Plan duration id.
        /// </summary>
        public int ResourcePlanDurationId { get; set; }

        /// <summary>
        /// Define the Resource Role.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Define the Start Date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Define the End Date.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Define the Resource Plan duration created bit.
        /// </summary>
        public bool ResourcePlanDurationCreated { get; set; }

        /// <summary>
        /// Define the Resource Plan duration status id.
        /// </summary>
        public string RPDurationStatusId { get; set; }

        /// <summary>
        /// Define the Resource Plan duration row num .
        /// </summary>
        public string RPDuRowNo { get; set; }

        /// <summary>
        /// Define the Resource Plan duration history id .
        /// </summary>
        public int RPDuHistoryId { get; set; }

        /// <summary>
        /// Define the Resource Plan duration Deleted bool .
        /// </summary>
        public bool RPDuDeleted { get; set; }

        /// <summary>
        /// Define the Resource Plan duration Deleted status id int .
        /// </summary>
        public int RPDuDeletedStatusId { get; set; }

        /// <summary>
        /// Define the Resource Plan duration Edition status id int .
        /// </summary>
        public int RPDuEditedStatusId { get; set; }

        /// <summary>
        /// Define the Resource Plan duration Location.
        /// </summary>
        public int Location { get; set; }

        /// <summary>
        /// Define the Number of Resources.
        /// </summary>
        public int NumberOfResources { get; set; }

        /// <summary>
        /// Define the ResourceNo.
        /// </summary>
        public decimal ResourceNo { get; set; }

        /// <summary>
        /// Define the Resource Name for duration.
        /// </summary>
        public string ResourceName { get; set; }

        /// <summary>
        /// Define the PreviousRole.
        /// </summary>
        public string PreviousRole { get; set; }



        /// <summary>
        /// Define the MRFCODE
        /// </summary>
        public string MRFCode { get; set; }


        /// <summary>
        /// Define the MRFStatus.
        /// </summary>
        public string MRFStatus { get; set; }


        #endregion Field members
    }
}
