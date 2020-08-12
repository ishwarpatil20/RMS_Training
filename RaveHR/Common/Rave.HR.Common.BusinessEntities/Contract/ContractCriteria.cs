
//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           ContractCriteria.cs       
//  Class:          ContractCriteria
//  Author:         Gopal Chauhan
//  Date written:   17/8/2009 3:51:30 PM
//  Description:    This class contains properties related to Search criteria for contract. 
//                  These business entities used for ContractSummary.aspx.
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  17/8/2009 3:51:30 PM  Gopal Chauhan    n/a     Created    
//
//------------------------------------------------------------------------------


namespace BusinessEntities
{
    public class ContractCriteria
    {
        /// <summary>
        /// This property is used to describe the Mode of Page
        /// </summary>
        public int Mode { get; set; }

        /// <summary>
        /// This property is used for get or set the Contract Id
        /// </summary>
        public int ContractId { get; set; }

        public string SortExpression { get; set; }

        public string Direction { get; set; }

        public int PageNumber { get; set; }

        public string ContractType { get; set; }

        public int ContractTypeID { get; set; }

        public string DocumentName { get; set; }

        public int ContractStatus { get; set; }

        public int Filter { get; set; }

        public int ProjectId { get; set; }

        public int ClientNameId { get; set; }

    }
}
