//------------------------------------------------------------------------------
//
//  File:           MRFDetail.cs       
//  Author:         Sunil.Mishra
//  Date written:   08/31/2009 01:39:10 PM
//  Description:    Class Contains the MRF Detail.
//  Amendments
//  Date                    Who                 Ref     Description
//  ----                    ---                 ---     -----------
//  08/31/2009 01:39:10 PM   Sunil.Mishra         n/a     Created    
//
//------------------------------------------------------------------------------

using System;

namespace BusinessEntities
{

    public struct NPS_Validation
    {
        public bool IsNPS_Project { get; set; }
        public bool IsDisableValidation { get; set; }
    }


    /// <summary>
    /// Defines the Business entities related to MRF Details
    /// </summary>
    [Serializable]
    public class MRFDetail
    {        

        #region Property

        /// <summary>
        /// Gets or sets the MrfCode.
        /// </summary>
        /// <value>The MRF Code.</value>
        public string MRFCode { get; set; }

        /// <summary>
        /// Gets or sets the MrfId.
        /// </summary>
        /// <value>The MRF Id.</value>
        public int MRFId { get; set; }

        /// <summary>
        /// Gets or sets the ProjectId.
        /// </summary>
        /// <value>The Project Id.</value>
        public int ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the ResourceOnBoard.
        /// </summary>
        /// <value>The Resource On Board.</value>
        public DateTime ResourceOnBoard { get; set; }

        /// <summary>
        /// Gets or sets the ResourceReleased.
        /// </summary>
        /// <value>The Resource Released.</value>
        public DateTime ResourceReleased { get; set; }

        /// <summary>
        /// Gets or sets the RoleId.
        /// </summary>
        /// <value>The Role Id.</value>
        public int RoleId { get; set; }


        /// <summary>
        /// Gets or sets the Role.
        /// </summary>
        /// <value>The Role .</value>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the SkillCategoryId.
        /// </summary>
        /// <value>The Skill Category Id.</value>
        public int SkillCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the Skill
        /// </summary>
        /// <value>The Skill Category Id.</value>
        public string Skill { get; set; }

        /// <summary>
        /// Gets or sets the MustToHaveSkills.
        /// </summary>
        /// <value>The Must To Have kills.</value>
        public string MustToHaveSkills { get; set; }

        /// <summary>
        /// Gets or sets the GoodToHaveSkills.
        /// </summary>
        /// <value>The Good To Have Skills.</value>
        public string GoodToHaveSkills { get; set; }

        /// <summary>
        /// Gets or sets the SoftSkills.
        /// </summary>
        /// <value>The Soft Skills.</value>
        public string SoftSkills { get; set; }

        /// <summary>
        /// Gets or sets the Operating Environment.
        /// </summary>
        /// <value>The Operating Environment.</value>
        public string OperatingEnvironment { get; set; }

        /// <summary>
        /// Gets or sets the BackEndSkills.
        /// </summary>
        /// <value>The Back End Skills.</value>
        public string BackEndSkills { get; set; }

        /// <summary>
        /// Gets or sets the Tools.
        /// </summary>
        /// <value>The Tools.</value>
        public string Tools { get; set; }

        /// <summary>
        /// Gets or sets the Experience.
        /// </summary>
        /// <value>The Experience.</value>
        public decimal Experience { get; set; }

        /// <summary>
        /// Gets or sets the Domain.
        /// </summary>
        /// <value>The Domain.</value>
        public string Domain { get; set; }

        /// <summary>
        /// Gets or sets the Qualification.
        /// </summary>
        /// <value>The Qualification.</value>
        public string Qualification { get; set; }

        /// <summary>
        /// Gets or sets the Utilization.
        /// </summary>
        /// <value>The Utilization.</value>
        public int Utilization { get; set; }

        /// <summary>
        /// Gets or sets the Billing.
        /// </summary>
        /// <value>The Billing.</value>
        public int Billing { get; set; }

        /// <summary>
        /// Gets or sets the BillingDate.
        /// </summary>
        /// <value>The BillingDate.</value>
        public DateTime BillingDate { get; set; }

        /// <summary>
        /// Gets or sets the ReportingToId.
        /// </summary>
        /// <value>The Reporting To Id.</value>
        public string ReportingToId { get; set; }

        /// <summary>
        /// Gets or sets the ReportingToEmployee.
        /// </summary>
        /// <value>The ReportingToEmployee.</value>
        public string ReportingToEmployee { get; set; }

        /// <summary>
        /// Gets or sets the Remarks.
        /// </summary>
        /// <value>The Remarks.</value>
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the MrfCTC.
        /// </summary>
        /// <value>The MRF CTC.</value>
        public decimal MRFCTC { get; set; }

        /// <summary>
        /// Gets or sets the DateOfRequisition.
        /// </summary>
        /// <value>The Date Of Requisition.</value>
        public DateTime DateOfRequisition { get; set; }

        /// <summary>
        /// Gets or sets the StatusId.
        /// </summary>
        /// <value>The Status Id.</value>
        public int StatusId { get; set; }

        /// <summary>
        /// Gets or sets the StatusName.
        /// </summary>
        /// <value>Name of Status</value>
        public string StatusName { get; set; }

        /// <summary>
        /// Gets or sets the MRF Status.
        /// </summary>
        /// <value>The Status Id.</value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the EMPId.
        /// </summary>
        /// <value>The EmpId.</value>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the CommentReason.
        /// </summary>
        /// <value>The Comment Reason.</value>
        public string CommentReason { get; set; }

        /// <summary>
        /// Gets or sets the CommentMoveMRF.
        /// </summary>
        /// <value>The CommentMoveMRF.</value>
        public string CommentMoveMRF { get; set; }

        /// <summary>
        /// Gets or sets the RequestForRecruitment.
        /// </summary>
        /// <value>The Request For Recruitment.</value>
        public DateTime RequestForRecruitment { get; set; }

        /// <summary>
        /// Gets or sets the DeptId.
        /// </summary>
        /// <value>The Dept Id.</value>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Gets or sets the RpId.
        /// </summary>
        /// <value>The Rp Id.</value>
        public int ResourcePlanId { get; set; }

        /// <summary>
        /// Gets or sets the ResourceResponsibility.
        /// </summary>
        /// <value>The Resource Responsibility.</value>
        public string ResourceResponsibility { get; set; }

        /// <summary>
        /// Gets or sets the ProjectName.
        /// </summary>
        /// <value>The Project Name .</value>
        public string ProjectName { get; set; }

        /// <summary>
        /// Gets or sets the Client Name.
        /// </summary>
        /// <value>The Client Name .</value>
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the OLDMRFRefID.
        /// </summary>
        /// <value>The OLDMRFRefID.</value>
        public int OLDMRFRefID { get; set; }


        /// <summary>
        /// Gets or sets the Recruitment Manager.
        /// </summary>
        /// <value>The Recruitment Manager .</value>
        public string RecruitmentManager { get; set; }


        /// <summary>
        /// Gets or sets the EmployeeName.
        /// </summary>
        /// <value>The Employee Name .</value>
        public string EmployeeName { get; set; }
        
        /// <summary>
        /// Gets or sets the RecruiterName.
        /// </summary>
        /// <value>The Recruiter Name .</value>
        public string RecruiterName { get; set; }

        /// <summary>
        /// Gets or sets the Newly Added EmployeeName.
        /// </summary>
        /// <value>The Employee Name .</value>
        public string NewEmployeeName { get; set; }

        

        /// <summary>
        /// Gets or sets the Parametrs to be passed to SP
        /// </summary>
        /// <value>ParameterCriteria .</value>
        public BusinessEntities.ParameterCriteria ParameterCriteria { get; set; }

        /// <summary>
        /// Gets or sets the RPCode.
        /// </summary>
        /// <value>The RPCode  .</value>
        public string RPCode { get; set; }

        /// <summary>
        /// Gets or sets the DepartmentName.
        /// </summary>
        /// <value>The DepartmentName .</value>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Gets or sets the ProjectStart Date
        /// </summary>
        public string ProjectStartDate { get; set; }

        /// <summary>
        /// Gets or sets the ProjectEnd Date
        /// </summary>
        public string ProjectEndDate { get; set; }


        /// <summary>
        /// Gets or sets the ProjectEndDate in DateTime Format
        /// </summary>
        public DateTime ProjEndDate { get; set; }

        /// <summary>
        /// Gets or sets the ProjectStartDate in DateTime Format
        /// </summary>
        public DateTime ProjStartDate { get; set; }
        /// <summary>
        /// Gets or sets the Project Description
        /// </summary>
        public string ProjectDescription { get; set; }

        /// <summary>
        /// Gets or sets the ResourcePlanDurationId
        /// </summary>
        public int ResourcePlanDurationId { get; set; }

        /// <summary>
        /// Gets or sets the ReplacedByMRFId
        /// </summary>
        public int ReplacedByMRFId { get; set; }

        /// <summary>
        /// Gets or sets the ResourcePlanStartDate
        /// </summary>
        public DateTime ResourcePlanStartDate { get; set; }

        /// <summary>
        /// Gets or sets the ResourcePlanEndDate
        /// </summary>
        public DateTime ResourcePlanEndDate { get; set; }

        /// <summary>
        /// Gets or sets the MRfType
        /// </summary>
        public int MRfType { get; set; }

        /// <summary>
        /// Gets or sets the NoOfResourceRequired
        /// </summary>
        public int NoOfResourceRequired { get; set; }

        /// <summary>
        ///Gets or sets the CheckGridValue 
        /// </summary>
        public bool CheckGridValue { get; set; }

        /// <summary>
        /// Gets or sets the LoggedInUserEmail 
        /// </summary>
        public string LoggedInUserEmail { get; set; }

        /// <summary>
        /// Gets or sets the SkillCategoryName 
        /// </summary>
        public string SkillCategoryName { get; set; }

        /// <summary>
        /// Gets or sets the AbortReason 
        /// </summary>
        public string AbortReason { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ExperienceString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MRFCTCString { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RoleName { get; set; }


        /// <summary>
        /// Gets or Sets the name of person who has raised the MRF
        /// </summary>
        public string RaisedBy { get; set; }

        /// <summary>
        /// Gets or sets the Email ID
        /// </summary>
        public string EmailId { get; set; }

        /// <summary>
        /// Gets or sets the created by id.
        /// </summary>
        /// <value>The created by id.</value>
        public int CreatedById { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>The created date.</value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the last modified by id.
        /// </summary>
        /// <value>The last modified by id.</value>
        public int LastModifiedById { get; set; }

        /// <summary>
        /// Gets or sets the last modified date.
        /// </summary>
        /// <value>The last modified date.</value>
        public DateTime LastModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the Location.
        /// </summary>
        /// <value>The Location.</value>
        public string ResourceLocation
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the NewMrfCode which is generated while Replacing old MRF.
        /// </summary>
        /// <value>The New MRF Code.</value>
        public string NewMRFCode { get; set; }

        /// <summary>
        /// Gets Or Sets the Allocation Id of Employee corresponding to EmployeeProjectAllocation
        /// </summary>
        public int ResourceAllocationID { get; set; }

        /// <summary>
        /// Gets Or Sets the Pm Id of Employee corresponding to Project On which he/She is assigned.
        /// </summary>
        public string PmID { get; set; }
        
        /// <summary>
        /// Gets or sets the Expected Closure Date.
        /// </summary>
        /// <value>The Expected Closure Date.</value>
        public string ExpectedClosureDate { get; set; }

        /// <summary>
        /// Gets or sets the reason for extending Expected Closure Date.
        /// </summary>
        /// <value>The Expected Closure Date.</value>
        public string ReasonForExtendingExpectedClosureDate { get; set; }

        /// <summary>
        /// Gets or sets the reason for Comments_DataMigration.
        /// </summary>
        /// <value>Comments_DataMigration.</value>
        public string Comments_DataMigration { get; set; }
        

        /// <summary>
        /// Gets or sets the Contract Type Id for a particular MRF
        /// </summary>
        public int ContractTypeID { get; set; }

        /// <summary>
        /// Gets or sets the Color Code.
        /// </summary>
        /// <value>The Expected Closure Date.</value>
        public string MRFColorCode { get; set; }

        /// <summary>
        /// Gets or sets the resignation date.
        /// </summary>
        /// <value>The resignation date.</value>
        public string ResignationDate { get; set; }

        /// <summary>
        /// Gets or sets the IsApproved status.
        /// </summary>
        /// <value>True/False.</value>
        public bool IsApproved { get; set; }

        /// <summary>
        /// Gets or sets the SLAId Id for a particular MRF
        /// </summary>
        public int SLAId { get; set; }

        /// <summary>
        /// Gets or sets the SLADays for a particular MRF
        /// </summary>
        public int SLADays { get; set; }

        /// <summary>
        /// Gets or sets the RecruitersId
        /// </summary>
        /// <value>The RecruitersId.</value>
        public string RecruitersId { get; set; }

        /// <summary>
        /// Gets or sets the AllocationDate.
        /// </summary>
        /// <value>The MRF Id.</value>
        public string AllocationDate { get; set; }

        /// <summary>
        /// Gets or sets Employee type of MRF.
        /// </summary>
        /// <value>The MRF Id.</value>
        public int EmployeeTypeId { get; set; }

        //33243-Subhra-03022012-Start  Added the property of EmployeeJoiningDate
        /// <summary>
        /// Gets or sets EmployeeJoining date
        /// </summary>
        public DateTime EmployeeJoiningDate { get; set; }
        //33243-Subhra-03022012-end
        

        /// <summary>
        /// Gets or Sets Employee IsDeptHead Status.
        /// </summary>
        /// <value>True/False.</value>
        public bool IsDeptHead { get; set; }

        /// <summary>
        /// Gets or sets the ActualClosureDate.
        /// </summary>
        /// <value>Actual Closure Date.</value>
        public DateTime ActualClosureDate { get; set; }


        public int TypeOfAllocation { get; set; }

        public int TypeOfSupply { get; set; }

        public int TypeOfClosure { get; set; }

        public string TypeOfSupplyName { get; set; }

        public string TypeOfAllocationName { get; set; }

        public string FutureAllocationDate { get; set; }

        public string FutureAllocateResourceName { get; set; }
        public int FutureEmpID { get; set; }

        public int FutureTypeOfSupplyID { get; set; }

        public int FutureTypeOfAllocationID { get; set; }

        

        /// <summary>
        /// Gets or sets the MRF purpose id.
        /// </summary>
        /// <value>The MRF purpose id.</value>
        public int MRFPurposeId { get; set; }

        /// <summary>
        /// Gets or sets the MRF purpose description.
        /// </summary>
        /// <value>The MRF purpose description.</value>
        public string MRFPurposeDescription { get; set; }


        /// <summary>
        /// Gets or sets the MRF purpose.
        /// </summary>
        /// <value>The MRF purpose.</value>
        public string MRFPurpose { get; set; }

        public int MRFDemandID { get; set; }

        /// <summary>
        /// Gets or sets the AbortedOrClosedDate.
        /// </summary>
        /// <value>The AbortedOrClosedDate.</value>
        public string AbortedOrClosedDate { get; set; }

        /// <summary>
        /// Gets or sets the RecruitmentAssignDate.
        /// </summary>
        /// <value>The RecruitmentAssignDate.</value>
        public string RecruitmentAssignDate { get; set; }

        /// <summary>
        /// Gets or sets the functional manager.
        /// </summary>
        /// <value>The functional manager.</value>
        public string FunctionalManager { get; set; }

        public DateTime SOWStartDate { get; set; }

        public DateTime SOWEndDate { get; set; }
        
        public string  SOWNo {get; set;}

        public string SowStartDtString { get; set; }

        public string SowEndDtString { get; set; }

        public string FunctionalManagerId { get; set; }

        // 29173-Ambar-Start-05092011                    
        public string TypeOFMRF { get; set; }        
        // 29173-Ambar-End-05092011

        // venkatesh  : Issue 46380 : 19/11/2013 : Starts
        // Desc : Mrf Summary - In export to excel 
        public string DateOfJoining { get; set; }
        public string Designation { get; set; }
        // venkatesh  : Issue 46380 : 19/11/2013 : end

        // Rajan Kumar : Issue 45222: 31/01/2014 : Starts                        			 
        // Desc : Rashwin is released from the project but the mail is still going to him since he has raised the MRF.
        public bool PMExistForProject { get; set; }
        // Rajan Kumar : Issue 45222: 31/01/2014 : END  

        // Rajan Kumar :Issue 49361: 21/02/2014 : Starts                              
        // Desc : Mail is addressed to Prerna although she is inactive. 
        public bool EmployeeExist { get; set; }
        // Rajan Kumar :Issue 49361: 21/02/2014 : END 

        #region Modified By Mohamed Dangra
        // Mohamed : Issue 50791 : 12/05/2014 : Starts                        			  
        // Desc : IN Mrf Details ,GroupId need to implement
        public int GroupId { get; set; }
        // Mohamed : Issue 50791 : 12/05/2014 : Ends
        #endregion Modified By Mohamed Dangra

        //Ishwar Patil 21/04/2015 Start
        public int SkillId { get; set; }
        public string MandatorySkills { get; set; }
        public string MandatorySkillsID { get; set; }
        //Ishwar Patil 21/04/2015 End
        #endregion

        //Rakesh : Actual vs Budget 06/06/2016 Begin
        public int ? CostCodeId { get; set; }
        public bool? IsOverride { get; set; }
        //End
    }
}
