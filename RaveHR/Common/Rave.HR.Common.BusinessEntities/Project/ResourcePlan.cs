//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           ResourcePlan.cs       
//  Class:          ResourcePlan
//  Author:         prashant.mala
//  Date written:   24/08/2009 10:36:30 PM
//  Description:    This class contains properties related to Resource Plan module. 
//                  These business entities used for CreateRP.aspx, EditRP.aspx, ApproveRejectRP.aspx etc.
//

//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  24/08/2009 10:36:30 PM  prashant.mala    n/a     Created    
//
//------------------------------------------------------------------------------

using System;

namespace BusinessEntities
{
    [Serializable]
    public class ResourcePlan : ResourcePlanDuration
    {
        #region Field members

        /// <summary>
        /// Define the Resource Plan ID.
        /// </summary>
        public int RPId { get; set; }

        /// <summary>
        /// Define the Resource Plan code.
        /// </summary>
        public string ResourcePlanCode { get; set; }

        /// <summary>
        /// Define the Project ID.
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Define the Resource Plan Status Id.
        /// </summary>
        public int RPStatusId { get; set; }

        /// <summary>
        /// Define the Resource Plan Created bool.
        /// </summary>
        public bool ResourcePlanCreated { get; set; }

        /// <summary>
        /// Define the Created by ID.
        /// </summary>
        public string CreatedById { get; set; }

        /// <summary>
        /// Define the Created Date.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Define the Last Modified by ID.
        /// </summary>
        public string LastModifiedById { get; set; }

        /// <summary>
        /// Define the Last Modified Date.
        /// </summary>
        public DateTime LastModifiedDate { get; set; }

        /// <summary>
        /// Define PageNumber property
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Define PageSize property
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Define SortExpression property
        /// </summary>
        public string SortExpression { get; set; }

        /// <summary>
        /// Define Direction property
        /// </summary>
        public string SortDirection { get; set; }

        /// <summary>
        /// Define the Approver Id.
        /// </summary>
        public string ApproverId { get; set; }

        /// <summary>
        /// Define the Reason For Approval.
        /// </summary>
        public string ReasonForApproval { get; set; }

        /// <summary>
        /// Define the Project Approval Date.
        /// </summary>
        public DateTime ResourcePlanApprovalDate { get; set; }

        /// <summary>
        /// Define the Createdby.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Define the RPApprovalStatusId.
        /// </summary>
        public int RPApprovalStatusId { get; set; }

        /// <summary>
        /// Define the Resource Plan Edited bool.
        /// </summary>
        public bool RPEdited { get; set; }

        /// <summary>
        /// Define Mode for Resource Plan Edited.
        /// </summary>
        public string Mode { get; set; }

        /// <summary>
        /// Define the ResourcePlanApprovalStatus.
        /// </summary>
        public string ResourcePlanApprovalStatus { get; set; }

        /// <summary>
        /// Define the ClientName.
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Define the RPFileName.
        /// </summary>
        public string RPFileName { get; set; }

        //Rajan : Issue 48242 : 10/01/2013 : Start
        //In "Resource Plan Edited" mail add Util and Billing change section in mail body. 
        //These two properties defines the billing or utility value is changed or not
        /// <summary>
        /// define Utilization is changed or not
        /// </summary>
        public bool IsUtilizationValueChanged { get; set; }

        /// <summary>
        /// define Billing is changed or not
        /// </summary>
        public bool IsBillingValueChanged { get; set; }
        //Rajan : Issue 48242 : 10/01/2013 : End


        #endregion Field members
    }
}
