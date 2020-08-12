//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           ResourcePlanDetail.cs       
//  Class:          ResourcePlanDetail
//  Author:         prashant.mala
//  Date written:   24/08/2009 10:39:30 PM
//  Description:    This class contains properties related to Resource Plan module. 
//                  These business entities used for CreateRP.aspx, EditRP.aspx, ApproveRejectRP.aspx etc.
//

//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  24/08/2009 10:39:30 PM  prashant.mala    n/a     Created    
//
//------------------------------------------------------------------------------

using System;

namespace BusinessEntities
{
    [Serializable]
    public class ResourcePlanDetail
    {
        #region Field members
        
        /// <summary>
        /// Define the Resource Plan Details ID.
        /// </summary>
        public int RPDId { get; set; }

        /// <summary>
        /// Define the Project Location.
        /// </summary>
        public string ProjectLocation { get; set; }

        /// <summary>
        /// Define the Resource Location.
        /// </summary>
        public string ResourceLocation { get; set; }

        /// <summary>
        /// Define the Resource Utilization.
        /// </summary>
        public int Utilization { get; set; }

        /// <summary>
        /// Define the Resource Billing.
        /// </summary>
        public int Billing { get; set; }

        /// <summary>
        /// Define the Resource Start Date for resource location.
        /// </summary>
        public DateTime ResourceStartDate { get; set; }

        /// <summary>
        /// Define the Resource End Date for resource location.
        /// </summary>
        public DateTime ResourceEndDate { get; set; }

        /// <summary>
        /// Define the Resource Plan detail status id.
        /// </summary>
        public string RPDetailStatusId { get; set; }

        /// <summary>
        /// Define the Resource Plan detail row num .
        /// </summary>
        public string RPDRowNo { get; set; }

        /// <summary>
        /// Define the RPDEdited .
        /// </summary>
        public bool RPDEdited { get; set; }

        /// <summary>
        /// Define the RPDDeleted .
        /// </summary>
        public bool RPDDeleted { get; set; }

        /// <summary>
        /// Define the Resource Plan detail Deleted status id int .
        /// </summary>
        public int RPDDeletedStatusId { get; set; }

        /// <summary>
        /// Define the Resource Plan detail Edition status id int .
        /// </summary>
        public int RPDEditedStatusId { get; set; }

        /// <summary>
        /// Define the Resource PreviousUtilization.
        /// </summary>
        public int PreviousUtilization { get; set; }

        /// <summary>
        /// Define the Resource PreviousBilling.
        /// </summary>
        public int PreviousBilling { get; set; }

        /// <summary>
        /// Define the Previous Resource Start Date for resource location.
        /// </summary>
        public DateTime PreviousResourceStartDate { get; set; }

        /// <summary>
        /// Define the Previous Resource End Date for resource location.
        /// </summary>
        public DateTime PreviousResourceEndDate { get; set; }

        #endregion Field members
    }
}
