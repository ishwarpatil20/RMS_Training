//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           Contract.cs       
//  Class:          Contract
//  Author:         prashant.mala
//  Date written:   7/29/2009/ 5:09:30 PM
//  Description:    This class contains properties related to Contract module. 
//                  These business entities used for ProjectSummary.aspx etc.
//

//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  7/29/2009 5:09:30 PM  prashant.mala    n/a     Created    
//
//------------------------------------------------------------------------------

 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    [Serializable]
    public class Contract
    {
        #region Field members

        /// <summary>
        ///Define iContractID property of type int
        /// </summary>
        public int ContractID 
        { get; set; }

        /// <summary>
        ///Define strContractCode property of type string
        /// </summary>
        public string ContractCode
        { get; set; }

        /// <summary>
        ///Define strContractReferenceID property of type string
        /// </summary>
        public string ContractReferenceID
        { get; set; }

        

        /// <summary>
        ///Define iContractID property of type string 
        /// </summary>
        public string ContractType
        { get; set; }

        

        /// <summary>
        ///Define iContractID property of type string
        /// </summary>
        public string DocumentName
        { get; set; }

        

        /// <summary>
        ///Define iContractID property of type string 
        /// </summary>
        public string DocumentType
        { get; set; }
        

        /// <summary>
        ///Define iContractID property of type int
        /// </summary>        
        public int AccountManagerID
        { get; set; }

        /// <summary>
        ///Define AccountManagerName property of type String
        /// </summary>        
        public string AccountManagerName
        { get; set; }

        
        /// <summary>
        ///Define iContractID property of type string
        /// </summary>
        public string EmailID 
        { get; set; }

        /// <summary>
        ///Define iContractID property of type string
        /// </summary>
        public string ClientName
        { get; set; }

        /// <summary>
        ///Define iLocationID property of type int 
        /// </summary>
        public int LocationID
        { get; set; }

        /// <summary>
        ///Define iLocationName property of type int 
        /// </summary>
        public string LocationName
        { get; set; }

        /// <summary>
        ///Define iParentContractID property of type int
        /// </summary>
        public int ParentContractID
        { get; set; }

        /// <summary>
        ///Define iContractID property of type int
        /// </summary>
        public int CreatedByID
        { get; set; }

        /// <summary>
        ///Define dtCreatedDate property of type DateTime
        /// </summary>
        public DateTime CreatedDate
        { get; set; }

        /// <summary>
        ///Define iLastModifiedBy property of type DateTime
        /// </summary>
        public int LastModifiedBy 
        { get; set; }

        /// <summary>
        ///Define dtLastModifiedDate property of type DateTime
        /// </summary>
        public DateTime LastModifiedDate
        { get; set; }

        /// <summary>
        ///Define ContractStatus property 
        /// </summary>
        public string ContractStatus
        { get; set; }

        /// <summary>
        ///Define ContractCreatedById property 
        /// </summary>
        public int ContractCreatedById
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// get the page count.
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        ///Define Contracts reason for deletion property 
        /// </summary>
        public string ReasonForDeletion
        { get; set; }


        /// <summary>
        ///Define Contracts ProjectId property 
        /// </summary>
        public int ProjectId
        { get; set; }


        /// <summary>
        ///Define iContractTypeId property of type string 
        /// </summary>
        public string ContractTypeId
        { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatedByEmailId
        { get; set; }

        /// <summary>
        /// Decimal field for Contract Value
        /// </summary>
        public decimal ContractValue
        { get; set; }

        
        

        /// <summary>
        /// int variable for Currency type.
        /// </summary>
        public int CurrencyType
        { get; set; }

        
        
        /// <summary>
        ///Define contract client name property of type string
        /// </summary>
        public string ContractClientName
        { get; set; }

        public string ProjectCategoryName
        { get; set; }

        public int ProjectCategoryID
        { get; set; }

        /// <summary>
        ///Define Division property of type string
        /// </summary>
        public string Division
        { get; set; }

        /// <summary>
        ///Define Sponsor property of type string
        /// </summary>
        public string Sponsor
        { get; set; }

        /// <summary>
        /// Gets or sets the clinet abbrivation.
        /// </summary>
        /// <value>The clinet abbrivation.</value>
        public string ClinetAbbrivation
        { get; set; }

        /// <summary>
        /// Gets or sets the project abbrivation.
        /// </summary>
        /// <value>The project abbrivation.</value>
        public string ProjectAbbrivation
        { get; set; }

        /// <summary>
        /// Gets or sets the phase.
        /// </summary>
        /// <value>The phase.</value>
        public string Phase
        { get; set; }


        /// <summary>
        /// Gets or sets the CR reference no.
        /// </summary>
        /// <value>The CR reference no.</value>
        public string CRReferenceNo
        { get; set; }

        /// <summary>
        /// Gets or sets the phase.
        /// </summary>
        /// <value>The phase.</value>
        public DateTime CRStartDate
        { get; set; }

        /// <summary>
        /// Gets or sets the CR end date.
        /// </summary>
        /// <value>The CR end date.</value>
        public DateTime CREndDate
        { get; set; }

        /// <summary>
        /// Gets or sets the CR remarks.
        /// </summary>
        /// <value>The CR remarks.</value>
        public string CRRemarks
        { get; set; }

        /// <summary>
        /// Gets or sets the CR project code.
        /// </summary>
        /// <value>The CR project code.</value>
        public string CRProjectCode
        { get; set; }

        /// <summary>
        /// Gets or sets the CR id.
        /// </summary>
        /// <value>The CR id.</value>
        public int CRId
        { get; set; }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The mode.</value>
        public string Mode
        { get; set; }

        /// <summary>
        /// Gets or sets the name of the CR project.
        /// </summary>
        /// <value>The name of the CR project.</value>
        public string CRProjectName
        { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        public DateTime ContractStartDate
        { get; set; }

        

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        public DateTime ContractEndDate
        { get; set; }

        

        /// <summary>
        /// Gets or sets the name of the contract type.
        /// </summary>
        /// <value>The name of the contract type.</value>
        public string ContractTypeName
        { get; set; }


        // 26114-Subhra-Start
        // Added following entities
        /// <summary>
        /// To define PreviousContractReferenceID property of type string
        /// </summary>
        public string PreviousContractReferenceID
        { get; set; }

        /// <summary>
        /// Gets or sets the PreviousContractEndDate
        /// </summary>
        public DateTime PreviousContractEndDate
        { get; set; }

        /// <summary>
        ///Gets or sets the  PreviousContractStartDate
        /// </summary>
        public DateTime PreviousContractStartDate
        { get; set; }

        /// <summary>
        /// To define the PreviousCurrencyType property 
        /// </summary>
        public string PreviousCurrencyType
        { get; set; }

        /// <summary>
        ///To define Decimal field for PreviousContractValue 
        /// </summary>
        public decimal PreviousContractValue
        { get; set; }

        /// <summary>
        /// To define the PreviousAccountManagerName of tye String
        /// </summary>
        public string PreviousAccountManagerName
        { get; set; }

        /// <summary>
        /// To define the PreviousDocumentName property of type string
        /// </summary>
        public string PreviousDocumentName
        { get; set; }

        /// <summary>
        /// To define the PreviousContractType
        /// </summary>
        public string PreviousContractType
        { get; set; }

        /// <summary>
        /// Gets or sets the AccountManagerName in text format
        /// </summary>
        public string TempAccountManagerName
        { get; set; }

        public string TempCurrencyName
        { get; set; }

        // 26114-Subhra-End        

        //Siddharth 13 March 2015 Start
        public string ProjectModel
        { get; set; }
        //Siddharth 13 March 2015 End

        //Siddharth 28th August 2015 Start
        public string BusinessVertical
        { get; set; }
        //Siddharth 28th August 2015 End



        //Rakesh : HOD for Employees 11/07/2016 Begin   
        public int ProjectHeadId { get; set; }
        //Rakesh : HOD for Employees 11/07/2016 End


        #endregion
    }
}
