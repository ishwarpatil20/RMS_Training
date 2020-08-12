using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Common.Constants
{
    public static class SPParameter
    {
        #region SP Parameter For MRF

        /// <summary>
        /// Define Parameter Category
        /// </summary>
        public const string Category = "@Category";

        /// <summary>
        /// Define Parameter EmailIdOfOldMRFCreator
        /// </summary>
        public const string EmailIdOfCurrentLoggedInUser = "@EmailId";


        /// <summary>
        /// Define Parameter MRFCode
        /// </summary>
        public const string CurrentUserEmailAddress = "@CurrentUserEmailAddress";


        /// <summary>
        /// Define Parameter MRFCode
        /// </summary>
        public const string MRFCode = "@MRFCode";

        /// <summary>
        /// Define Parameter MRFStatus
        /// </summary>
        public const string MRFStatus = "@MRFStatus";

        /// <summary>
        /// Define Parameter Reason
        /// </summary>
        public const string Reason = "@Reason";

        /// <summary>
        /// Define Parameter DepartmentId
        /// </summary>
        public const string DepartmentId = "@DepartmentId";

        /// <summary>
        /// Define Parameter set MRF Detail Reason 
        /// </summary>
        public const string SetMRFDetailReason = "@setMRFDetailReason";
        /// <summary>
        /// Define the SuperRole
        /// </summary>
        public const string SuperRole = "@SuperRole";

        /// <summary>
        /// Define the PresalesRole
        /// </summary>
        public const string PresalesRole = "@PresalesRole";

        /// <summary>
        /// Define the RPMRole
        /// </summary>
        public const string RPMRole = "@RPMRole";

        /// <summary>
        /// Define the CEORole
        /// </summary>
        public const string CEORole = "@CEORole";

        /// <summary>
        /// Define the COORole
        /// </summary>
        public const string COORole = "@COORole";

        /// <summary>
        /// Define the CFMRole
        /// </summary>
        public const string CFMRole = "@CFMRole";

        /// <summary>
        /// Define the FMRole
        /// </summary>
        public const string FMRole = "@FMRole";

        /// <summary>
        /// Define the HRRole
        /// </summary>
        public const string HRRole = "@HRRole";

        /// <summary>
        /// Define the PMRole
        /// </summary>
        public const string PMRole = "@PMRole";

        /// <summary>
        /// Define the UserMailId
        /// </summary>
        public const string UserMailId = "@UserMailId";

        /// <summary>
        /// Define the MRFType
        /// </summary>
        public const string MRFType = "@MRFType";

        /// <summary>
        /// Define the NewMRFCode
        /// </summary>
        public const string NewMRFCode = "@NewMRFCode";

        /// <summary>
        /// Define the ResourceOnboard
        /// </summary>
        public const string ResourceOnboard = "@ResourceOnboard";

        /// <summary>
        /// Define the ResourceReleased
        /// </summary>
        public const string ResourceReleased = "@ResourceReleased";

        /// <summary>
        /// Define the SkillCategoryId
        /// </summary>
        public const string SkillCategoryId = "@SkillCategoryId";

        /// <summary>
        /// Define the MustTohaveSkills
        /// </summary>
        public const string MustTohaveSkills = "@MustTohaveSkills";

        /// <summary>
        /// Define the GoodToHaveSkills
        /// </summary>
        public const string GoodToHaveSkills = "@GoodToHaveSkills";

        /// <summary>
        /// Define the SoftSkills
        /// </summary>
        public const string SoftSkills = "@SoftSkills";

        /// <summary>
        /// Define the OperatingEnvironment
        /// </summary>
        public const string OperatingEnvironment = "@OperatingEnvironment";

        /// <summary>
        /// Define the BackEndSkills
        /// </summary>
        public const string BackEndSkills = "@BackEndSkills";


        /// <summary>
        /// Define the Tools
        /// </summary>
        public const string Tools = "@Tools";

        /// <summary>
        ///  Define the Experrience
        /// </summary>
        public const string Experrience = "@Experrience";

        /// <summary>
        /// Define the Domain
        /// </summary>
        public const string Domain = "@Domain";

        /// <summary>
        /// Define the ReportingToId
        /// </summary>
        public const string ReportingToId = "@ReportingToId";

        /// <summary>
        /// Define the Remarks
        /// </summary>
        public const string Remarks = "@Remarks";

        /// <summary>
        ///  Define the MRFCTC
        /// </summary>
        public const string MRFCTC = "@MRFCTC";

        /// <summary>
        /// Define the DateOfRequisition
        /// </summary>
        public const string DateOfRequisition = "@DateOfRequisition";

        /// <summary>
        /// Define the ResourseResponsibility
        /// </summary>
        public const string ResourseResponsibility = "@ResourseResponsibility";

        /// <summary>
        /// Define the MRFId
        /// </summary>
        public const string MRFId = "@MRFId";

        /// <summary>
        /// Define the CurrentMRFId
        /// </summary>
        public const string CurrentMRFId = "@CurrentMRFId";


        /// <summary>
        /// Define the SetMRFStatus
        /// </summary>
        public const string SetMRFStatus = "@SetMRFstatus";


        /// <summary>
        /// Define the Experience
        /// </summary>
        public const string LowerExperince = "@lowerExperience";
        public const string UpperExperience = "@upperExperience";

        /// <summary>
        /// Define the RecruitmentRole
        /// </summary>
        public const string RecruitmentRole = "@RecruitmentRole";

        /// <summary>
        /// Define the TestingRole
        /// </summary>
        public const string TestingRole = "@TestingRole";


        /// <summary>
        /// Define the QualityRole
        /// </summary>
        public const string QualityRole = "@QualityRole";

        /// <summary>
        /// Define the MarketingRole
        /// </summary>
        public const string MarketingRole = "@MarketingRole";


        /// <summary>
        /// Define the Rave Consultant role.
        /// </summary>
        public const string RaveConsultantRole = "@RaveConsultantRole";

        /// <summary>
        /// Define the IsApproved parameter.
        /// </summary>
        public const string IsApproved = "@IsApproved";

        //vandana
        /// <summary>
        /// Define the AllocationDate parameter.
        /// </summary>
        public const string AllocationDate = "@AllocationDate";
        public const string TypeOfSupply = "@TypeOfSupplyID";
        //Siddhesh Arekar Issue ID : 55884 Closure Type
        public const string TypeOfCloser = "@TypeOfCloserID";
        //Siddhesh Arekar Issue ID : 55884 Closure Type
        public const string TypeOfAllocation = "@TypeOfAllocationID";
        public const string MRFTypeOfAllocation = "@MRFTypeOfAllocation";

        public const string FutureEmpID = "@FutureEmpID";
        public const string FutureAllocationDate = "@FutureAllocationDate";

        public const string FutureTypeOfSupplyID = "@FutureTypeOfSupplyID";
        public const string FutureTypeOfAllocationID = "@FutureTypeOfAllocationID";

        public const string SOWStartDate = "@SOWStartDate";
        public const string SOWEndDate = "@SOWEndDate";
        public const string SOWNo = "@SOWNo";

        // Mohamed : 03/12/2014 : Starts                        			  
        // Desc : NIS Changes

        public const string SkillReportLevel = "@Level";
        // Mohamed : 03/12/2014 : Ends


        /// <summary>
        /// Define Parameter ResourceJoin
        /// </summary>
        public const string ResourceJoin = "@ResourceJoin";

        /// <summary>
        /// Define Parameter PendingExternalAllocation
        /// </summary>
        public const string PendingExternalAllocation = "@PendingExternalAllocation";

        /// <summary>
        /// Define Parameter PendingExpectedResourceJoin
        /// </summary>
        public const string PendingExpectedResourceJoin = "@PendingExpectedResourceJoin";

        /// <summary>
        /// Define Parameter MRFPurpose
        /// </summary>
        public const string MRFPurpose = "@MRFPurpose";

        public const string MRFDemandID = "@MRFDemandID";

        /// <summary>
        /// Define Parameter MRFPurposeDescription
        /// </summary>
        public const string MRFPurposeDescription = "@MRFPurposeDescription";

        /// <summary>
        /// Define Parameter previousMrfCode
        /// </summary>
        public const string PreviousMrfCode = "@previousMrfCode";


        public const string DeptProjectName = "@DeptProjectName";

        public const string DeptProjectMRFRoleID = "@DeptProjectMRFRoleID";

        /// <summary>
        /// Define Parameter EmailIdOfOldMRFCreator
        /// </summary>
        public const string EmailIdOfOldMRFCreator = "@EmailIdOfOldMRFCreator";
        /// <summary>
        /// Define Parameter EmailIdOfNewMrfPM
        /// </summary>
        public const string EmailIdOfNewMrfPM = "@EmailIdOfNewMrfPM";
        /// <summary>
        /// Define Parameter EmailIdOfOldMrfPM
        /// </summary>
        public const string EmailIdOfOldMrfPM = "@EmailIdOfOldMrfPM";
        /// <summary>
        /// Define Parameter EmailIdOfOldMrfResponsiblePerson
        /// </summary>
        public const string EmailIdOfOldMrfResponsiblePerson = "@EmailIdOfOldMrfResponsiblePerson";
        /// <summary>
        /// Define Parameter rolechange
        /// </summary>
        public const string rolechange = "@rolechange";

        /// <summary>
        /// Define the Role_mrf_delete parameter.
        /// </summary>
        public const string Role_mrf_delete = "@Role_mrf_delete";
        #region Modified By Mohamed Dangra
        // Mohamed : Issue 50791 : 12/05/2014 : Starts                        			  
        // Desc : IN Mrf Details ,GroupId need to implement

        /// <summary>
        /// Define the MRF_GroupId parameter.
        /// </summary>
        public const string MRF_GroupId = "@GroupId";

        // Mohamed : Issue 50791 : 12/05/2014 : Ends
        #endregion Modified By Mohamed Dangra

        //Ishwar Patil 29092014 For NIS : Start
        public const string IsRMSMRF = "@IsRMSMRF";
        public const string IsRMSEmp = "@IsRMSEmp";
        //Ishwar Patil 29092014 For NIS : End

        //Ishwar Patil 23/04/2015 Start
        public const string @MandatorySkillsID = "@MandatorySkillsID";
        //Ishwar Patil 23/04/2015 End
        #endregion SP Parameter For MRF

        #region SP Parameter For Resource Plan

        public const string RPDuId = "@RPDuId";
        /// <summary>
        /// Define parameter ProjectID.
        /// </summary>
        public const string ProjectId = "@ProjectId";

        /// <summary>
        /// Define parameter EmpCCId.
        /// </summary>
        public const string EmpCCId = "@EmpCCId";


        // CR - 25715 Sachin issue related to ePlatform MRF Sachin
        // Added new parameters
        /// <summary>
        /// Define parameter EmpCOUNT.
        /// </summary>
        public const string EmpCOUNT = "@EmpCOUNT";

        /// <summary>
        /// Define parameter MRFCOUNT.
        /// </summary>
        public const string MRFCOUNT = "@MRFCOUNT";
        // End CR - 25715 Sachin issue related to ePlatform MRF Sachin

        /// <summary>
        /// Define parameter Role.
        /// </summary>
        public const string Role = "@Role";

        /// <summary>
        /// Define parameter StartDate.
        /// </summary>
        public const string StartDate = "@StartDate";

        /// <summary>
        /// Define parameter EndDate.
        /// </summary>
        public const string EndDate = "@EndDate";

        /// <summary>
        /// Define parameter Resource Plan Status Id.
        /// </summary>
        public const string RPStatusId = "@RPStatusId";

        /// <summary>
        /// Define parameter ResourcePlanCreated.
        /// </summary>
        public const string ResourcePlanCreated = "@RPCreated";

        /// <summary>
        /// Define parameter ProjectLocation.
        /// </summary>
        public const string ProjectLocation = "@ProjectLocation";

        /// <summary>
        /// Define parameter Location.
        /// </summary>
        public const string Location = "@Location";

        /// <summary>
        /// Define parameter Utilization.
        /// </summary>
        public const string Utilization = "@Utilization";

        /// <summary>
        /// Define parameter Billing.
        /// </summary>
        public const string Billing = "@Billing";

        /// <summary>
        /// Define parameter BillingDate.
        /// </summary>
        public const string BillingDate = "@BillingDate";

        /// <summary>
        /// Define parameter ResourceStartDate.
        /// </summary>
        public const string ResourceStartDate = "@ResourceStartDate";

        /// <summary>
        /// Define parameter ResourceEndDate.
        /// </summary>
        public const string ResourceEndDate = "@ResourceEndDate";

        /// <summary>
        /// Define parameter ResourcePlanDurationCreated.
        /// </summary>
        public const string ResourcePlanDurationCreated = "@RPDurationCreated";

        /// <summary>
        /// Define parameter ResourcePlanID.
        /// </summary>
        public const string ResourcePlanId = "@ResourcePlanId";

        /// <summary>
        /// Define parameter ResourcePlanDurationID.
        /// </summary>
        public const string ResourcePlanDurationId = "@ResourcePlanDurationId";

        /// <summary>
        /// Define parameter ReplacedByMRFId.
        /// </summary>
        public const string ReplacedByMRFId = "@ReplacedByMRFId";

        /// <summary>
        /// Define parameter RPDurationID.
        /// </summary>
        public const string RPDurationId = "@RPDurationId";

        /// <summary>
        /// Define parameter RPDetailId.
        /// </summary>
        public const string RPDetailId = "@RPDetailId";

        /// <summary>
        /// Define parameter pageNum.
        /// </summary>
        public const string pageNum = "@pageNum";

        /// <summary>
        /// Define parameter pageSize.
        /// </summary>
        public const string pageSize = "@pageSize";

        /// <summary>
        /// Define parameter SortExpression.
        /// </summary>
        public const string SortExpression = "@SortExpression";

        /// <summary>
        /// Define parameter pageCount.
        /// </summary>
        public const string pageCount = "@pageCount";

        /// <summary>
        /// Define parameter CreatedById.
        /// </summary>
        public const string CreatedById = "@CreatedById";

        /// <summary>
        /// Define parameter CreatedDate.
        /// </summary>
        public const string CreatedDate = "@CreatedDate";

        /// <summary>
        /// Define parameter LastModifiedById.
        /// </summary>
        public const string LastModifiedById = "@LastModifiedById";

        /// <summary>
        /// Define parameter LastModifiedDate.
        /// </summary>
        public const string LastModifiedDate = "@LastModifiedDate";

        /// <summary>
        /// Define parameter Mode.
        /// </summary>
        public const string Mode = "@Mode";

        /// <summary>
        /// Define parameter GroupMasterId.
        /// </summary>
        public const string GroupMasterId = "@GroupMasterId";

        /// <summary>
        /// Define parameter MasterName.
        /// </summary>
        public const string MasterName = "@MasterName";

        /// <summary>
        /// Define parameter ApproverId.
        /// </summary>
        public const string ApproverId = "@ApproverId";

        /// <summary>
        /// Define parameter ReasonForApproval.
        /// </summary>
        public const string ReasonForApproval = "@ReasonForApproval";

        /// <summary>
        /// Define parameter ResourcePlanApprovalDate.
        /// </summary>
        public const string ResourcePlanApprovalDate = "@ResourcePlanApprovalDate";

        /// <summary>
        /// Define parameter ResourcePlanApprovalStatus.
        /// </summary>
        public const string RPApprovalStatusId = "@RPApprovalStatusId";

        /// <summary>
        /// Define parameter ResourcePlan is whether active or inactive.
        /// </summary>
        public const string IsActive = "@Isactive";

        /// <summary>
        /// Define parameter RPEdited 
        /// </summary>
        public const string RPEdited = "@RPEdited";

        /// <summary>
        /// Define parameter RPDEdited 
        /// </summary>
        public const string RPDEdited = "@RPDEdited";

        /// <summary>
        /// Define parameter RP Duration History Id
        /// </summary>
        public const string RPDuHistoryId = "@RPDuHistoryId";

        /// <summary>
        /// Define parameter RPDuEdited
        /// </summary>
        public const string RPDuEdited = "@RPDuEdited";

        /// <summary>
        /// Define parameter RPDuDeleted 
        /// </summary>
        public const string RPDuDeleted = "@RPDuDeleted";

        /// <summary>
        /// Define parameter RPDDeleted 
        /// </summary>
        public const string RPDDeleted = "@RPDDeleted";

        /// <summary>
        /// Define parameter RPRejectedStatusId.
        /// </summary>
        public const string RPRejectedStatusId = "@RPRejectedStatusId";

        /// <summary>
        /// Define parameter RPDurationStatusId.
        /// </summary>
        public const string RPDurationStatusId = "@RPDurationStatusId";

        /// <summary>
        /// Define parameter RPDetailStatusId.
        /// </summary>
        public const string RPDetailStatusId = "@RPDetailStatusId";

        /// <summary>
        /// Define parameter Actual Location.
        /// </summary>
        public const string ActualLocation = "@ActualLocation";

        /// <summary>
        /// Define RP File Name.
        /// </summary>
        public const string RPFileName = "@RPFileName";

        #endregion SP Parameter For Resource Plan

        #region SP Parameter For Employee

        /// <summary>
        /// Define parameter QualificationId.
        /// </summary>
        public const string QualificationId = "@QualificationId";

        /// <summary>
        /// Define parameter EmpId.
        /// </summary>        
        public const string EmpId = "@EmpId";

        /// <summary>
        /// Define parameter CommentReason.
        /// </summary>        
        public const string CommentReason = "@CommentReason";

        /// <summary>
        /// Define parameter CommentMoveMRF.
        /// </summary>        
        public const string CommentMoveMRF = "@CommentMoveMRF";


        /// <summary>
        /// Define parameter RequestForRecruitment.
        /// </summary>        
        public const string RequestForRecruitment = "@RequestForRecruitment";

        /// <summary>
        /// Define parameter Qualification.
        /// </summary>
        public const string Qualification = "@Qualification";

        /// <summary>
        /// Define parameter UniversityName.
        /// </summary>
        public const string UniversityName = "@UniversityName";

        /// <summary>
        /// Define parameter InstitutionName.
        /// </summary>
        public const string InstitutionName = "@InstitutionName";

        /// <summary>
        /// Define parameter PassingYear.
        /// </summary>
        public const string PassingYear = "@PassingYear";

        /// <summary>
        /// Define parameter Percentage.
        /// </summary>
        public const string Percentage = "@Percentage";

        /// <summary>
        /// Define parameter CertificationId.
        /// </summary>
        public const string CertificationId = "@CertificationId";

        /// <summary>
        /// Define parameter CertificationName.
        /// </summary>
        public const string CertificationName = "@CertificationName";

        /// <summary>
        /// Define parameter CertificateDate.
        /// </summary>
        public const string CertificateDate = "@CertificateDate";

        /// <summary>
        /// Define parameter CertificateValidDate.
        /// </summary>
        public const string CertificateValidDate = "@CertificateValidDate";

        /// <summary>
        /// Define parameter Score.
        /// </summary>
        public const string Score = "@Score";

        /// <summary>
        /// Define parameter OutOf.
        /// </summary>
        public const string OutOf = "@OutOf";

        /// <summary>
        /// Define parameter SkillsId.
        /// </summary>
        public const string SkillsId = "@SkillsId";

        /// <summary>
        /// Define parameter Skill.
        /// </summary>
        public const string Skill = "@Skill";

        /// <summary>
        /// Define parameter SkillVersion.
        /// </summary>
        public const string SkillVersion = "@SkillVersion";

        /// <summary>
        /// Define parameter SkillVersion.
        /// </summary>
        public const string SkillCategory = "@SkillCategory";

        /// <summary>
        /// Define parameter Experience.
        /// </summary>
        public const string Experience = "@Experience";

        /// <summary>
        /// Define parameter Proficiency.
        /// </summary>
        public const string Proficiency = "@Proficiency";

        /// <summary>
        /// Define parameter LastUsedDate.
        /// </summary>
        public const string LastUsedDate = "@LastUsedDate";

        /// <summary>
        /// Define parameter CourseName.
        /// </summary>
        public const string CourseName = "@CourseName";

        /// <summary>
        /// Define parameter Outof.
        /// </summary>
        public const string Outof = "@Outof";

        /// <summary>
        /// Define parameter ProfessionalId.
        /// </summary>
        public const string ProfessionalId = "@ProfessionalId";

        /// <summary>
        /// Define parameter CompanyName.
        /// </summary>
        public const string CompanyName = "@CompanyName";

        /// <summary>
        /// Define parameter Designation.
        /// </summary>
        public const string Designation = "@Designation";


        /// <summary>
        /// Define parameter ExperienceType.
        /// </summary>
        public const string ExperienceType = "@ExperienceType";

        /// <summary>
        /// Define parameter OrganisationId.
        /// </summary>
        public const string OrganisationId = "@OrganisationId";

        /// <summary>
        /// Define parameter ProjectName.
        /// </summary>
        public const string ProjectName = "@ProjectName";

        /// <summary>
        /// Define parameter Organisation.
        /// </summary>
        public const string Organisation = "@Organisation";

        /// <summary>
        /// Define parameter Years.
        /// </summary>
        public const string Years = "@Years";


        /// <summary>
        /// Define parameter Onsite.
        /// </summary>
        public const string Onsite = "@Onsite";

        /// <summary>
        /// Define parameter OnsiteDuration.
        /// </summary>
        public const string OnsiteDuration = "@OnsiteDuration";

        /// <summary>
        /// Define parameter Description.
        /// </summary>
        public const string Description = "@Description";

        public const string ProjectCategoryID = "@ProjectCategoryID";

        // public const string ProjectCategoryName = "@ProjectCategoryName";

        /// <summary>
        /// Define parameter Resposibility.
        /// </summary>
        public const string Resposibility = "@Resposibility";

        /// <summary>
        /// Define parameter EMPCode.
        /// </summary>
        public const string EMPCode = "@EMPCode";

        /// <summary>
        /// Define parameter FirstName.
        /// </summary>
        public const string FirstName = "@FirstName";

        /// <summary>
        /// Define parameter MiddleName.
        /// </summary>
        public const string MiddleName = "@MiddleName";

        /// <summary>
        /// Define parameter LastName.
        /// </summary>
        public const string LastName = "@LastName";

        /// <summary>
        /// Define parameter EMPPicture.
        /// </summary>
        public const string EMPPicture = "@EMPPicture";

        /// <summary>
        /// Define parameter EmailId.
        /// </summary>
        public const string EmailId = "@EmailId";

        /// <summary>
        /// Define parameter GroupId.
        /// </summary>
        public const string GroupId = "@GroupId";

        /// <summary>
        /// Define parameter DesignationId.
        /// </summary>
        public const string DesignationId = "@DesignationId";

        /// <summary>
        /// Define parameter RoleId.
        /// </summary>
        public const string RoleId = "@RoleId";

        /// <summary>
        /// Define parameter Band.
        /// </summary>
        public const string Band = "@Band";

        /// <summary>
        /// Define parameter JoiningDate.
        /// </summary>
        public const string JoiningDate = "@JoiningDate";

        /// <summary>
        /// Define parameter DOB.
        /// </summary>
        public const string DOB = "@DOB";

        /// <summary>
        /// Define parameter Gender.
        /// </summary>
        public const string Gender = "@Gender";

        /// <summary>
        /// Define parameter MaritalStatus.
        /// </summary>
        public const string MaritalStatus = "@MaritalStatus";

        /// <summary>
        /// Define parameter FatherName.
        /// </summary>
        public const string FatherName = "@FatherName";

        /// <summary>
        /// Define parameter SpouseName.
        /// </summary>
        public const string SpouseName = "@SpouseName";

        /// <summary>
        /// Define parameter BloodGroup.
        /// </summary>
        public const string BloodGroup = "@BloodGroup";

        /// <summary>
        /// Define parameter EmergencyContactNo.
        /// </summary>
        public const string EmergencyContactNo = "@EmergencyContactNo";

        /// <summary>
        /// Define parameter PassportNo.
        /// </summary>
        public const string PassportNo = "@PassportNo";

        //CR - 28321 Passport Application Number Sachin
        /// <summary>
        /// Define parameter PassportNo.
        /// </summary>        
        public const string PassportAppNo = "@PassportAppNo";
        //CR - 28321 Passport Application Number Sachin End

        /// <summary>
        /// Define parameter PassportIssueDate.
        /// </summary>
        public const string PassportIssueDate = "@PassportIssueDate";

        /// <summary>
        /// Define parameter PassportExpireDate.
        /// </summary>
        public const string PassportExpireDate = "@PassportExpireDate";

        /// <summary>
        /// Define parameter PassportIssuePlace.
        /// </summary>
        public const string PassportIssuePlace = "@PassportIssuePlace";

        /// <summary>
        /// Define parameter ReadyToRelocateIndia.
        /// </summary>
        public const string ReadyToRelocateIndia = "@ReadyToRelocateIndia";

        /// <summary>
        /// Define parameter ReasonNotToRelocateIndia.
        /// </summary>
        public const string ReasonNotToRelocateIndia = "@ReasonNotToRelocateIndia";

        /// <summary>
        /// Define parameter ReadytoRelocate.
        /// </summary>
        public const string ReadytoRelocate = "@ReadytoRelocate";

        /// <summary>
        /// Define parameter ReasonNotToRelocate.
        /// </summary>
        public const string ReasonNotToRelocate = "@ReasonNotToRelocate";

        /// <summary>
        /// Define parameter Duration.
        /// </summary>
        public const string Duration = "@Duration";

        /// <summary>
        /// Define parameter ResidencePhone.
        /// </summary>
        public const string ResidencePhone = "@ResidencePhone";

        /// <summary>
        /// Define parameter MobileNo.
        /// </summary>
        public const string MobileNo = "@MobileNo";

        /// <summary>
        /// Define parameter CAddress.
        /// </summary>
        public const string CAddress = "@CAddress";

        /// <summary>
        /// Define parameter CStreet.
        /// </summary>
        public const string CStreet = "@CStreet";

        /// <summary>
        /// Define parameter CCity.
        /// </summary>
        public const string CCity = "@CCity";

        /// <summary>
        /// Define parameter CPinCode.
        /// </summary>
        public const string CPinCode = "@CPinCode";

        /// <summary>
        /// Define parameter PAddress.
        /// </summary>
        public const string PAddress = "@PAddress";

        /// <summary>
        /// Define parameter PStreet.
        /// </summary>
        public const string PStreet = "@PStreet";

        /// <summary>
        /// Define parameter PCity.
        /// </summary>
        public const string PCity = "@PCity";

        /// <summary>
        /// Define parameter PPinCode.
        /// </summary>
        public const string PPinCode = "@PPinCode";

        /// <summary>
        /// Define parameter ResignationDate.
        /// </summary>
        public const string ResignationDate = "@ResignationDate";

        /// <summary>
        /// Define parameter ResignationReason.
        /// </summary>
        public const string ResignationReason = "@ResignationReason";

        /// <summary>
        /// Define parameter LastWorkingDay.
        /// </summary>
        public const string LastWorkingDay = "@LastWorkingDay";

        /// <summary>
        /// Define parameter RecruiterId.
        /// </summary>
        public const string RecruiterId = "@RecruiterId";

        /// <summary>
        /// Define parameter InterviewerId.
        /// </summary>
        public const string InterviewerId = "@InterviewerId";

        /// <summary>
        /// Define parameter Type.
        /// </summary>
        public const string Type = "@Type";

        /// <summary>
        /// Define parameter Id.
        /// </summary>
        public const string StatusId = "@StatusId";

        /// <summary>
        /// Define Prefix.
        /// </summary>
        public const string Prefix = "@Prefix";

        /// <summary>
        /// Define Prefix.
        /// </summary>
        public const string GPA = "@GPA";

        /// <summary>
        /// Define FileName.
        /// </summary>
        public const string FileName = "@FileName";

        /// <summary>
        /// Constants will contain VisaId column .
        /// </summary>
        public const string VisaId = "@VisaId";

        /// <summary>
        /// Constants will contain CountryName column .
        /// </summary>
        public const string CountryName = "@CountryName";

        /// <summary>
        /// Constants will contain VisaType column .
        /// </summary>
        public const string VisaType = "@VisaType";

        /// <summary>
        /// Constants will contain ExpiryDate column .
        /// </summary>
        public const string ExpiryDate = "@ExpiryDate";

        /// <summary>
        /// Constants will contain ProjectSize column .
        /// </summary>
        public const string ProjectSize = "@ProjectSize";

        /// <summary>
        /// Constants will contain ClientName column .
        /// </summary>
        public const string ClientName = "@ClientName";

        /// <summary>
        /// Constants will contain EmpProjectAllocationId column .
        /// </summary>
        public const string EmpProjectAllocationId = "@EPAId";

        /// <summary>
        /// Constants will contain ProjectReleaseDate column .
        /// </summary>
        public const string ProjectReleaseDate = "@ProjectReleaseDate";

        /// <summary>
        /// Constants will contain Utilization and Billing change Date.
        /// </summary>
        public const string UtilAndBillChangeDate = "@UtilAndBillChangeDate";

        //To solved issue id 22011
        //Start
        /// <summary>
        /// Constants will contain Utilization and Billing change Date.
        /// </summary>
        public const string BillingChangeDate = "@BillingChangeDate";

        /// <summary>
        /// Constants will contain Utilization and Billing change Date.
        /// </summary>
        public const string ResourceBillingDate = "@ResourceBillingDate";
        //End

        /// <summary>
        /// Constants will contain ProjectDone column .
        /// </summary>
        public const string ProjectDone = "@ProjectDone";

        /// <summary>
        /// Constants will contain IsFresher column .
        /// </summary>
        public const string IsFresher = "@IsFresher";

        /// <summary>
        /// Constants will contain MonthSince column.
        /// </summary>
        public const string MonthSince = "@MonthSince";

        /// <summary>
        /// Constants will contain YearSince column.
        /// </summary>
        public const string YearSince = "@YearSince";

        /// <summary>
        /// Constants will contain MonthTill column.
        /// </summary>
        public const string MonthTill = "@MonthTill";

        /// <summary>
        /// Constants will contain YearTill column.
        /// </summary>
        public const string YearTill = "@YearTill";

        /// <summary>
        /// Constants will contain EmployeeAddressId column.
        /// </summary>
        public const string EmployeeAddressId = "@EmpAddrsId";

        /// <summary>
        /// Constants will contain Address column.
        /// </summary>
        public const string Address = "@Address";

        /// <summary>
        /// Constants will contain FlatNo column.
        /// </summary>
        public const string FlatNo = "@FlatNo";

        /// <summary>
        /// Constants will contain BuildingName column.
        /// </summary>
        public const string BuildingName = "@BuildingName";

        /// <summary>
        /// Constants will contain Street column.
        /// </summary>
        public const string Street = "@Street";

        /// <summary>
        /// Constants will contain LandMark column.
        /// </summary>
        public const string LandMark = "@LandMark";

        /// <summary>
        /// Constants will contain City column.
        /// </summary>
        public const string City = "@City";

        /// <summary>
        /// Constants will contain State column.
        /// </summary>
        public const string State = "@State";

        /// <summary>
        /// Constants will contain Country column.
        /// </summary>
        public const string Country = "@Country";

        /// <summary>
        /// Constants will contain PinCode column.
        /// </summary>
        public const string PinCode = "@PinCode";

        /// <summary>
        /// Constants will contain IsPermanent column.
        /// </summary>
        public const string AddressType = "@AddressType";

        /// <summary>
        /// Constants will contain EmergencyContactId column.
        /// </summary>
        public const string EmergencyContactId = "@ECId";

        /// <summary>
        /// Constants will contain ContactName column.
        /// </summary>
        public const string ContactName = "@ContactName";

        /// <summary>
        /// Constants will contain ContactNumber column.
        /// </summary>
        public const string ContactNumber = "@ContactNumber";

        /// <summary>
        /// Constants will contain Relation column.
        /// </summary>
        public const string Relation = "@Relation";

        /// <summary>
        /// Constants will contain EmpContactId column name.
        /// </summary>
        public const string EmpContactId = "@EmpContactId";

        /// <summary>
        /// Constants will contain CityCode column name.
        /// </summary>
        public const string CityCode = "@CityCode";

        /// <summary>
        /// Constants will contain CountryCode column name.
        /// </summary>
        public const string CountryCode = "@CountryCode";

        /// <summary>
        /// Constants will contain ContactNo column name.
        /// </summary>
        public const string ContactNo = "@ContactNo";

        /// <summary>
        /// Constants will contain Extension column name.
        /// </summary>
        public const string Extension = "@Extension";

        /// <summary>
        /// Constants will contain AvalibilityTime column name.
        /// </summary>
        public const string AvalibilityTime = "@AvalibilityTime";

        /// <summary>
        /// Constants will contain ContactType column name.
        /// </summary>
        public const string ContactType = "@ContactType";

        /// <summary>
        /// Constants will contain SeatName column name.
        /// </summary>
        public const string SeatName = "@SeatName";

        /// <summary>
        /// Constants will contain Status column name.
        /// </summary>
        public const string Status = "@Status";

        /// <summary>
        /// Constants will contain DepartmentName column name.
        /// </summary>
        public const string DepartmentName = "@DepartmentName";

        /// <summary>
        /// Constants will contain ReasonForExtension column name.
        /// </summary>
        public const string ReasonForExtension = "@ReasonforExtension";

        /// <summary>
        /// Constants will contain PrimarySkillId column name.
        /// </summary>
        public const string PrimarySkillId = "@PrimarySkillId";

        /// <summary>
        /// Constants will contain PrimarySkillId column name.
        /// </summary>
        public const string ExpectedClosureDate = "@ExpectedClosureDate";

        /// <summary>
        /// Constants will contain reason to extend the Expected Closure Date.
        /// </summary>
        public const string ReasonExtendExpectedClosureDate = "@ReasonExtendExpectedClosureDate";

        /// <summary>
        /// Constants will contain ReportingToFM column name.
        /// </summary>
        public const string ReportingToFM = "@ReportingToFm";

        /// <summary>
        /// Constants will contain color code column name.
        /// </summary>
        public const string MRFColorCode = "@MRFColorCode";

        public const string EmpSkillTypeID = "@EmpSkillTypeID";

        public const string EmpLocation = "@Location";

        public const string RankOrder = "@RankOrder";

        public const string ExperienceInYear = "@ExperienceInYear";

        public const string ExperienceInMonth = "@ExperienceInMonth";

        //Rajan Kumar : Issue 39508: 31/01/2014 : Starts                        			 
        // Desc : Traninig for new joining employee. (Training Gaps).
        public const string IsTrainingRequired = "@IsTrainingRequired";
        public const string TrainingSubject = "@TrainingSubject";
        // Rajan Kumar : Issue 39508: 31/01/2014 : END

        /// <summary>
        /// Defines skill name.
        /// </summary>
        public const string SkillName = "@SkillName";

        public const string Stream = "@Stream";

        public const string IsEmployeeDetailsModified = "@IsEmployeeDetailsModified";

        public const string ModifyBy = "@ModifyBy";

        public const string ModifyDate = "@ModifyDate";

        public const string FileExtension = "@FileExtension";

        // 28109-Ambar-Start
        public const string PTotalMonths = "@PTotalMonths";

        public const string PTotalYears = "@PTotalYears";

        //public const string PReleventMonths = "@PReleventMonths";

        //public const string PReleventYears = "@PReleventYears";
        // 28109-Ambar-End

        // Mohamed : Issue 39461 : 30/01/2013 : Starts                        			  
        // Desc :When any employee department get changed resource business unit not get updated.(To update ResourceBusinessUnit)

        public const string DepartementChangeDate = "@DepartementChangeDate";

        public const string DesignationChangeDate = "@DesignationChangeDate";

        public const string ProbationDate = "@Probation_Date";

        public const string ProbationFlag = "@Probation_Flag";

        // Mohamed : Issue 39461 : 30/01/2013 : Ends

        public const string WindowsUsername = "@WindowUsername";

        public const string MandatorySkills = "@MandatorySkills";

        public const string OptionalSkills = "@OptionalSkills";

        //Umesh : NIS-RMS - Head Count Report Starts
        public const string DivisionId = "@DivisionId";

        public const string BusinessAreaId = "@BusinessAreaId";

        public const string BusinessSegmentId = "@BusinessSegmentId";
        //Umesh : NIS-RMS - Head Count Report Ends


        //Ishwar Patil 20112014 For NIS : Start
        public const string CostCode = "@CostCode";
        //Ishwar Patil 20112014 For NIS : End

        #endregion SP Parameter For Employee

        #region SP Parameter For Recruitment
        public const string MRFID = "@MRFId";
        public const string DeptId = "@DeptId";
        public const string ExpectedJoiningDate = "@ExpectedJoiningDate";
        public const string EmployeeType = "@EmployeeType";
        public const string ActualCTC = "@ActualCTC";
        public const string CreatedByDate = "@CreatedByDate";
        public const string IsInserted = "@isInserted";
        public const string CandidateId = "@CandidateId";
        public const string IsRemoved = "@isRemoved";
        public const string IsUpdated = "@isUpdated";
        public const string ResourceJoinedDate = "@ResourceJoinedDate";
        public const string IsResourceJoined = "@IsResourceJoined";
        public const string RecruitermanagerId = "@RecruitermanagerId";


        /// <summary>
        /// Defined parameter for external Work experience.
        /// </summary>
        public const string ExternalWorkExperience = "@externalWorkExperience";

        /// <summary>
        /// Defined parameter for Resource Business Unit.
        /// </summary>
        public const string ResourceBussinessUnit = "@resourceBussinessUnit";

        /// <summary>
        /// Defined parameter for Address.
        /// </summary>
        public const string candidateAddress = "@address";

        /// <summary>
        /// Defined parameter for Phone no.
        /// </summary>
        public const string phoneNo = "@PhoneNo";


        /// <summary>
        /// Define parameter COUNT.
        /// </summary>        
        public const string COUNT = "@COUNT";

        /// <summary>
        /// Define parameter ProjectActualCloseDate.
        /// </summary>        
        public const string ProjectActualCloseDate = "@ProjectActualCloseDate";

        /// <summary>
        /// Define parameter LandlineNo.
        /// </summary>        
        public const string LandlineNo = "@LandlineNo";
        public const string EmpExist = "EmpExist";
        public const string IsRoleExist = "@IsRoleExist";

        public const string CandidateEmailID = "@CandidateEmailID";
        public const string ContractDuration = "@ContractDuration";
        public const string CandidateOfferAcceptedDate = "@OfferAcceptedDate";

        // Mohamed : Issue 50306 : 09/09/2014 : Starts                        			  
        // Desc : Expected Joinee's details edited[MRF Code: MRF_Testing_SrTstA_0387] old  Joining date is default date -- Mail for de-linking MRF
        public const string OldMRFId = "@OldMRFID";
        public const string NewMRFId = "@NewMRFID";
        // Mohamed : Issue 50306 : 09/09/2014 : Ends
        #endregion

        #region SP Parameter For Contract

        public const string ContractId = "@ContractId";
        public const string DocumentName = "@DocumentName";
        public const string ContractType = "@ContractType";
        public const string ContractStatus = "@ContractStatus";
        public const string ContractReferenceID = "@ContractReferenceID";
        public const string AccountManagerID = "@AccountManagerID";
        public const string EmailID = "@EmailID";
        public const string LocationID = "@LocationID";
        public const string ParentContractID = "@ParentContractID";
        public const string OutContractId = "@OutContractId";
        public const string ContractCode = "@ContractCode";
        public const string ReasonForDeletion = "@ReasonForDeletion";
        public const string ProjectCode = "@ProjectCode";
        public const string ProjectType = "@ProjectType";
        public const string NoOfResource = "@NoOfResource";
        public const string CreatedBy = "@CreatedBy";
        public const string SortExpresion = "@SortExpresion";
        public const string PROJECTID = "@PROJECTID";
        public const string PROJECTNAME = "@PROJECTNAME";
        public const string iProjectID = "@iProjectID";
        public const string FirstID = "@FirstID";
        public const string ContractValue = "@ContractValue";
        public const string CurrencyType = "@CurrencyType";
        public const string Division = "@Division";
        public const string Sponsor = "@Sponsor";
        public const string ClientProjectAbbrivation = "@ClientProjectAbbrivation";
        public const string ProjectCodeAbbrivation = "@ProjectCodeAbbrivation";
        public const string MasterId = "@MasterId";
        public const string CRReferenceNo = "@CRReferenceNo";
        public const string CRId = "@CRId";
        public const string ContractStartDate = "@ContractStartDate";
        public const string ContractEndDate = "@ContractEndDate";
        //Ishwar : Issue 49176 : 26/03/2014 : Starts
        public const string ProjectGroup = "@ProjectGroup";
        //Ishwar : Issue 49176 : 26/03/2014 : End
        // Mohamed : Issue  : 23/09/2014 : Starts                        			  
        // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
        public const string ProjectDivision = "@Division";
        public const string ProjectAlias = "@ProjectAlias";
        public const string ProjectBusinessArea = "@BusinessArea";
        public const string ProjectBusinessSegment = "@BusinessSegment";
        // Mohamed : Issue  : 23/09/2014 : Ends

        //Siddharth 13 march 2015 Start
        public const string ProjectModel = "@ProjectModel";
        //Siddharth 13 march 2015 End

        #endregion SP Parameter For Contract

        #region SP Parameter For Seat Alloctaion

        public const string SectionID = "@SectionID";
        public const string SourceLocation = "@SourceLocation";
        public const string Destination = "@Destination";
        public const string EmployeeID = "@EmployeeID";
        public const string SeatID = "@SeatID";
        public const string ExtNo = "@ExtNo";
        public const string Landmark = "@Landmark";
        public const string EmpCode = "@EmpCode";
        public const string BranchID = "@BranchID";

        #endregion SP Parameter For Seat Alloctaion

        #region SP Parameter For Projects

        /// <summary>
        /// Define Parameter Technology
        /// </summary>
        public const string Technology = "@Technology";

        /// <summary>
        /// Define Parameter TechnologyID
        /// </summary>
        public const string TechnologyID = "@TechnologyID";


        /// <summary>
        /// Define Parameter NewTechnologyID
        /// </summary>
        public const string NewTechnologyID = "@NewTechnologyID";

        /// <summary>
        /// Define Parameter CategoryID
        /// </summary>
        public const string CategoryID = "@CategoryID";


        /// <summary>
        /// Define Parameter NewCategoryID
        /// </summary>
        public const string NewCategoryID = "@NewCategoryID";


        /// <summary>
        /// Define Parameter DomainID
        /// </summary>
        public const string DomainID = "@DomainID";


        /// <summary>
        /// Define Parameter NewDomainID
        /// </summary>
        public const string NewDomainID = "@NewDomainID";

        /// <summary>
        /// Define Parameter Subdomain
        /// </summary>
        public const string Subdomain = "@Subdomain";

        /// <summary>
        /// Define Parameter SubdomainID
        /// </summary>
        public const string SubdomainID = "@SubdomainID";

        /// <summary>
        /// Define Parameter NewSubdomainID
        /// </summary>
        public const string NewSubdomainID = "@NewSubdomainID";


        #endregion SP Parameter For Projects

        #region SP Parameter For 4C
        public const string CreatorId = "@CreatorId";
        public const string ReviewerId = "@ReviewerId";
        public const string ModifiedById = "@ModifiedById";
        public const string FlagCreatorReviwerSet = "@flagCreatorReviwerSet";
        public const string Flag = "@flag";
        public const string FlagCreator = "@FlagCreator";
        public const string FlagReviewer = "@FlagReviewer";
        public const string Month = "@month";
        public const string Year = "@year";
        public const string fourCRole = "@fourCRole";
        public const string fourCAllowDirectSubmit = "@AllowDirectSubmit";

        public const string FBAID = "@FBAID";
        public const string FBID = "@FBID";
        public const string CTYPE = "@CTYPE";
        public const string ParameterType = "@ParameterType";
        public const string Discription = "@Discription";
        public const string Action = "@Action";
        public const string ActionOwnerId = "@ActionOwnerId";
        public const string ActionCreatedDate = "@ActionCreatedDate";
        public const string TargetClosureDate = "@TargetClosureDate";
        public const string ActualClosureDate = "@ActualClosureDate";
        public const string RemarksAction = "@Remarks";
        public const string RemarksReviewer = "@ReviewerRemarks";
        public const string ActionStatus = "@ActionStatus";
        public const string LineManagerId = "@LineManagerId";
        public const string FunctionalManagerId = "@FunctionalManagerId";
        public const string FourCStatus = "@FourCStatus";

        public const string Competency = "@Competency";
        public const string Collaboration = "@Collaboration";
        public const string Communication = "@Communication";
        public const string Commitment = "@Commitment";

        public const string ActionDML = "@ActionDML";

        public const string RatingForEmpId = "@RatingForEmpId";
        public const string RatingOption = "@RatingOption";
        public const string RatingType = "@RatingType";
        public const string RatingFillOrView = "@RatingFillOrView";

        public const string fourCType = "@fourCType";
        public const string LoginEmpId = "@LoginEmpId";
        public const string FunManagerId = "@FnManagerId";
        public const string finalSubmit = "@FinalSubmit";
        public const string NoOfMonth = "@NoOfMonth";

        public const string ReleaseDate = "@ReleaseDate";

        public const string EmployeeViewType = "@EmployeeViewType";
        public const string MonthDuration = "@MonthDuration";
        public const string ColorRating = "@ColorRating";



        #endregion SP Parameter For 4C

        //Ishwar Patil : Trainging Module 08/04/2014 : Starts
        #region SP Parameter For Training

        public const string RaiseID = "@RaiseID";
        public const string TrainingType = "@TrainingType";
        public const string TrainingStatus = "@TrainingStatus";
        public const string TrainingName = "@TrainingName";
        public const string TrainingNameOther = "@TrainingNameOther";
        public const string Quarter = "@Quarter";
        public const string NoOfParticipant = "@NoOfParticipant";
        public const string RequestedBy = "@RequestedBy";
        public const string Priority = "@Priority";
        public const string BusinessImpact = "@BusinessImpact";
        public const string Comments = "@Comments";
        public const string OutTechnicalID = "@OutTechnicalID";
        public const string CreatedByEmailId = "@CreatedByEmailId";

        public const string Topic = "@Topic";
        public const string Agenda = "@Agenda";
        public const string Date = "@Date";
        public const string Presenter = "@Presenter";
        public const string OutKSSID = "@OutKSSID";
        public const string RecordCount = "@RecordCount";

        public const string SeminarsName = "@SeminarsName";
        public const string NameOfParticipant = "@NameOfParticipant";
        public const string URL = "@URL";
        public const string OutSeminarsID = "@OutSeminarsID";
        public const string OutSoftSkillsID = "@OutSoftSkillsID";
        public const string UserEmpID = "@UserEmpID";
        public const string CurrentIndexCount = "@CurrentIndexCount";
        #endregion SP Parameter For Training
        //Ishwar Patil : Trainging Module 08/04/2014 : End  

    }
}
