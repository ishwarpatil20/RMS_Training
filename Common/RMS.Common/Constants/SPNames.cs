using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Common.Constants
{
    public static class SPNames
    {
        #region Names of stored procedures used in Data Access layer

        #region SP for Projects

        public const string Projects_AddProject = "USP_Projects_AddProject";
        public const string Projects_UpdateProject = "USP_Projects_UpdateProject";
        public const string Projects_ApproveProject = "USP_Projects_ApproveProject";
        public const string Projects_RejectProject = "USP_Projects_RejectProject";
        public const string Projects_DeleteProject = "USP_Projects_DeleteProject";
        public const string Projects_ViewProject = "USP_Projects_ViewProject";
        public const string Projects_AddTechnologiesForProject = "USP_Projects_AddTechnologiesForProject";
        public const string Projects_AddSubDomainsForProject = "USP_Projects_AddSubDomainsForProject";
        public const string Projects_DeleteTechnologyForProject = "USP_Projects_DeleteTechnologyForProject";
        public const string Projects_DeleteSubDomainForProject = "USP_Projects_DeleteSubDomainForProject";
        public const string Projects_ApproveRejectProject = "USP_Projects_ApproveRejectProject";
        public const string Projects_ProjectName = "USP_Projects_ProjectName";
        public const string Projects_ListOfProjects = "USP_Projects_ListOfProjects";
        public const string Projects_GetClientName = "USP_Projects_GetClientName";
        //27631-Subhra-start
        public const string Projects_GetClosedClientName = "USP_Projects_GetClosedClientName";
        //27631-Subhra-end
        public const string Projects_ListOfProjectManager = "USP_Projects_ListOfProjectManager";
        public const string Projects_TechnologyCategory = "USP_Projects_TechnologyCategory";
        public const string Projects_Technology = "USP_Projects_Technology";
        public const string Projects_GetTechnology = "USP_Projects_GetTechnology";
        public const string Projects_GetCategroy = "USP_Projects_GetCategroy";
        public const string Projects_CheckForDuplicatedProjName = "USP_Projects_CheckForDuplicatedProjName";
        public const string Projects_GetCheckedCategoryName = "USP_Projects_GetCheckedCategoryName";
        public const string Projects_GetDomainName = "USP_Projects_GetDomainName";
        public const string Projects_GetCheckedDomainName = "USP_Projects_GetCheckedDomainName";
        public const string Projects_GetSubDomain = "USP_Projects_GetSubDomain";
        public const string Projects_GetProjectsByTechAndDomain = "USP_Projects_GetProjectsByTechAndDomain";
        public const string Projects_RetrieveProjDet = "USP_Projects_RetrieveProjDet";
        public const string Projects_GetProjectSearchResult = "USP_Projects_GetProjectSearchResult";
        public const string Projects_GetFilteredProjectSummaryDataForAddProject = "USP_Projects_GetFilteredProjectSummaryData";
        public const string Projects_GetUnfilteredProjectSummaryDataForAddProject = "USP_Projects_GetUnfilteredProjectSummaryData";
        public const string Projects_GetFilteredProjectSummaryData = "USP_Projects_GetFilteredProjectSummaryData_Paging";
        public const string Projects_GetUnfilteredProjectSummaryData = "USP_Projects_GetUnfilteredProjectSummaryData_Paging";
        public const string Projects_UnfilteredListOfProjectsPendingForapproval = "USP_Projects_UnfilteredListOfProjectsPendingForapproval";
        public const string Projects_FilteredListOfProjectsPendingForapproval = "USP_Projects_FilteredListOfProjectsPendingForapproval";
        public const string Projects_GetContractsForProject = "USP_Projects_GetContractsForProject";
        public const string Projects_GetProjectEditedDetails = "USP_Projects_GetProjectEditedDetails";
        public const string Projects_AddProjectsCategoryAndTechnology = "USP_Projects_AddProjectsCategoryAndTechnology";
        public const string Projects_AddProjectsDomainAndSubDomain = "USP_Projects_AddProjectsDomainAndSubDomain";
        public const string Contracts_GetClientName = "USP_Contracts_GetClientName";
        //49187-Sanju-start
        public const string GET_Project_ManagerList = "USP_Project_GetManagerList";
        //49187-Sanju-end
        #endregion SP for Projects

        #region SP for Contract

        public const string Contract_AddContract = "USP_Contract_AddContract";
        public const string Contract_AddProject = "USP_Contracts_AddProject";
        public const string Contract_GetContractDetails = "USP_Contract_GetContractDetails";
        public const string Contract_DeleteContract = "USP_Contract_DeleteContract";
        public const string Contract_GetEmployeeByDesignation = "USP_Contract_GetEmployee_Designation";
        public const string Contract_SearchProjectDetails = "USP_Contract_SearchProjectDetails";
        public const string Contract_EmpEmailID = "USP_Contracts_getEmpEmailID";
        public const string Contract_ViewContracts = "USP_Contracts_ViewContracts";
        public const string Contract_GetProjectsDetails = "USP_Contracts_GetProjectsDetails";
        public const string Contract_GetLoggedInEmployeeId = "USP_Contracts_EmployeeId";
        public const string Contract_EditContracts = "USP_Contracts_EditContract";
        public const string Contract_EditProject = "USP_Contracts_EditProject";
        public const string Contract_DisassociateProject = "USP_Contracts_DisassociateProject";
        public const string Contract_GetProjectList = "USP_Contract_GetProjectList";
        public const string Contract_EmpAssociated = "USP_Contracts_CheckEmpRealese";
        public const string Contract_CheckProjectName = "USP_Contracts_CheckProjectName";
        public const string Contract_GetContractInFilter = "USP_Contract_GetContractInFilter";
        public const string Contract_GetContract = "USP_Contract_GetContract";
        public const string Contract_ViewContractsProjects = "USP_Contracts_ViewContractsProjects";
        public const string Contract_GetEmployeeDesignation = "USP_Contract_GetEmployee_Designation";
        public const string Contract_CheckProject = "USP_Contracts_CheckProject";
        public const string Contracts_GetRPDetails = "USP_Contracts_GetRPDetails";
        public const string CONTRACT_GETPROJECTLISTFORFILER = "USP_Contract_GetProjectListForFilter";
        public const string Contracts_GetClientAbbrivation = "USP_Contracts_GetClientAbbrivation";
        public const string Contract_CheckProjectCode = "USP_Contracts_CheckProjectCode";
        public const string Contracts_AddCRDetails = "USP_Contracts_AddCRDetails";
        public const string Contracts_DeleteCRDetails = "USP_Contracts_DeleteCRDetails";
        public const string Contracts_UpdateCRDetails = "USP_Contracts_UpdateCRDetails";
        public const string Contracts_GetCRDetails = "USP_Contracts_GetCRDetails";
        public const string Contracts_CheckCRReferenceNo = "USP_Contracts_CheckCRReferenceNo";

        #endregion

        #region SP for Employees

        public const string EMPLOYEE_GETPROJECTALLOCATIONFOREMPLOYEE = "USP_Employee_GetProjectAllocationForEmployee";
        public const string Employee_AddEmployee = "USP_Employee_AddEmployee";
        public const string Employee_DeleteEmployee = "USP_Employee_DeleteEmployee";
        public const string Employee_GetEmployee = "USP_Employee_GetEmployee";
        public const string Employee_UpdateEmployee = "USP_Employee_UpdateEmployee";

        //Siddharth 2 April 2015 Start
        public const string Employee_UpdateEmployeeCostCode = "USP_Employee_UpdateEmployeeCostCode";
        public const string Employee_GetEmployeeCostCodeByEmpID = "USP_Employee_GetEmployeeCostCodeByEmpID";
        public const string Employee_GetEmployeeCostCodeByEmpIDandPrjID = "USP_Employee_GetEmployeeCostCodeByEmpIDandPrjID";
        public const string Employee_UpdateEmpCostCodeProjRelease = "USP_Employee_UpdateEmpCostCodeProjRelease";
        public const string Employee_GetEmployeeCostCodeByEmpIDandNoPrjID = "USP_Employee_GetEmployeeCostCodeByEmpIDandNoPrjID";
        public const string Employee_DeleteEmployeeCostCode = "USP_Employee_DeleteEmployeeCostCode";
        public const string Employee_GetProjectWiseEmpCostCodeDetails = "USP_Employee_GetProjectWiseEmpCostCodeDetails";
        public const string Employee_GetEmployeeAndProjectForCCManager = "USP_Employee_GetEmployeeAndProjectForCCManager";
        public const string Employee_DeleteEmployeeCostCodeForCCManager = "USP_Employee_DeleteEmployeeCostCodeForCCManager";
        public const string Employee_UpdateEmpCostCodeProjReleaseForCCManager = "USP_Employee_UpdateEmpCostCodeProjReleaseForCCManager";
        public const string Employee_GetEmpIDFromWindowsUsernameForCCManager = "USP_Employee_GetEmpIDFromWindowsUsernameForCCManager";
        public const string Employee_GetAllCostCodes = "USP_Employee_GetAllCostCodes";
        public const string Employee_GetConsolidatedbyCostCode = "USP_Employee_GetConsolidatedbyCostCode";
        //Siddharth 2 April 2015 End

        public const string Employee_GetEmployeeDesignations = "USP_Employee_GetEmployeeDesignation";
        public const string Employee_GetEmployeeByEmailId = "USP_Employee_GetEmployeeByEmailId";
        public const string Employee_GetEmployeesList = "USP_Employee_GetEmployeesList";
        public const string Employee_GetMRFCode = "USP_Employee_GetMRFCode";
        public const string Employee_UpdateEmployeeMRFCode = "USP_Employee_UpdateEmployeeMRFCode";
        public const string Employee_GetEmployeesSummary = "USP_Employee_GetEmployeesSummary";
        public const string Employee_GetProjectName = "USP_Employee_GetProjectName";
        public const string Employee_GetEmployeesReleaseStatus = "USP_Employee_GetEmployeesReleaseStatus";
        public const string Employee_UpdateEmpProjectAllocation = "USP_Employee_UpdateEmpProjectAllocation";
        public const string Employee_EmployeeEmailExists = "USP_Employee_EmployeeExists";
        public const string Employee_ResignationDetails = "USP_Employee_UpdateEmployeeResignationDetails";
        public const string Employee_EmployeeDataExists = "USP_Employee_DataExists";
        public const string Employee_RollBackResignationDetails = "USP_Employee_RollBackEmployeeResignationDetails";

        //Siddharth 2 April 2015 Start
        public const string Employee_ProjectNameForEmpByEmpID = "USP_Employee_ProjectNameForEmpByEmpID";
        //Siddharth 2 April 2015 End

        public const string Employee_AddPassportDetails = "USP_Employee_AddPassportDetails";
        public const string Employee_DeletePassportDetails = "USP_Employee_DeletePassportDetails";
        public const string Employee_GetPassportDetails = "USP_Employee_GetPassportDetails";
        public const string Employee_UpdatePassportDetails = "USP_Employee_UpdatePassportDetails";

        public const string Employee_AddVisaDetails = "USP_Employee_AddVisaDetails";
        public const string Employee_DeleteVisaDetails = "USP_Employee_DeleteVisaDetails";
        public const string Employee_GetVisaDetails = "USP_Employee_GetVisaDetails";
        public const string Employee_UpdateVisaDetails = "USP_Employee_UpdateVisaDetails";
        public const string Employee_DeleteVisaDetailsByEmpId = "USP_Employee_DeleteVisaDetailsByEmpId";

        public const string Employee_AddQualificationDetails = "USP_Employee_AddQualificationDetails";
        public const string Employee_DeleteQualificationDetails = "USP_Employee_DeleteQualificationDetails";
        public const string Employee_GetQualificationDetails = "USP_Employee_GetQualificationDetails";
        public const string Employee_UpdateQualificationDetails = "USP_Employee_UpdateQualificationDetails";

        public const string Employee_AddProfessionalDetails = "USP_Employee_AddProfessionalDetails";
        public const string Employee_DeleteProfessionalDetails = "USP_Employee_DeleteProfessionalDetails";
        public const string Employee_GetProfessionalDetails = "USP_Employee_GetProfessionalDetails";
        public const string Employee_UpdateProfessionalDetails = "USP_Employee_UpdateProfessionalDetails";

        public const string Employee_AddCertificationDetails = "USP_Employee_AddCertificationDetails";
        public const string Employee_DeleteCertificationDetails = "USP_Employee_DeleteCertificationDetails";
        public const string Employee_GetCertificationDetails = "USP_Employee_GetCertificationDetails";
        public const string Employee_UpdateCertificationDetails = "USP_Employee_UpdateCertificationDetails";

        public const string Employee_AddOrganisationDetails = "USP_Employee_AddOrganisationDetails";
        public const string Employee_DeleteOrganisationDetails = "USP_Employee_DeleteOrganisationDetails";
        public const string Employee_GetOrganisationDetails = "USP_Employee_GetOrganisationDetails";
        public const string Employee_UpdateOrganisationDetails = "USP_Employee_UpdateOrganisationDetails";
        public const string Employee_DeleteNonOrganisationDetails = "USP_Employee_DeleteNonOrganisationDetails";
        public const string Employee_GetRelevantExperience = "USP_Employee_GetRelevantExperience";
        // 28109-Ambar-Start
        public const string Employee_UpdateTotalReleventExp = "USP_Employee_UpdateTotalReleventExp";
        // 28109-Ambar-Start
        public const string Employee_AddSkillsDetails = "USP_Employee_AddSkillsDetails";
        public const string Employee_DeleteSkillsDetails = "USP_Employee_DeleteSkillsDetails";
        public const string Employee_GetSkillsDetails = "USP_Employee_GetSkillsDetails";
        public const string Employee_UpdateSkillsDetails = "USP_Employee_UpdateSkillsDetails";
        public const string Employee_GetSkillData = "USP_Employee_GetSkillData";
        public const string Employee_AddSkillData = "USP_Employee_AddSkillData";
        public const string Employee_GetEmployeeSkillType = "USP_Employee_GetEmployeeSkillType";
        public const string Employee_GetEmployeeSkillTypeCategory = "USP_Employee_GetEmployeeSkillTypeCategory";

        public const string Employee_AddProjectsDetails = "USP_Employee_AddProjectsDetails";
        public const string Employee_DeleteProjectsDetails = "USP_Employee_DeleteProjectsDetails";
        public const string Employee_GetProjectsDetails = "USP_Employee_GetProjectsDetails";
        public const string Employee_UpdateProjectsDetails = "USP_Employee_UpdateProjectsDetails";
        public const string Employee_GetManagerEmailId = "USP_Employee_GetManagerEmailId";
        public const string Employee_GetProjectManagerEmailId = "USP_Employee_GetProjectManagerEmailId";
        public const string Employee_GetDepartmentHeadEmailId = "USP_Employee_GetDepartmentHeadEmailId";
        public const string Employee_GetDepartmentHeadIdByEmailId = "USP_Employee_GetDepartmentHeadIdByEmailId";

        public const string Employee_AddAddress = "USP_Employee_AddEmployeeAddress";
        public const string Employee_GetAddress = "USP_Employee_GetEmployeeAddress";
        public const string Employee_UpdateAddress = "USP_Employee_UpdateEmployeeAddress";
        public const string Employee_DeleteAddress = "USP_Employee_DeleteEmployeeAddress";
        public const string Employee_ManipulateAddress = "USP_Employee_ManipulateEmployeeAddress";

        public const string Employee_AddEmergencyContact = "USP_Employee_AddEmployeeEmergencyContact";
        public const string Employee_GetEmergencyContact = "USP_Employee_GetEmployeeEmergencyContact";
        public const string Employee_UpdateEmergencyContact = "USP_Employee_UpdateEmployeeEmergencyContact";
        public const string Employee_DeleteEmergencyContact = "USP_Employee_DeleteEmployeeEmergencyContact";

        public const string Employee_AddContact = "USP_Employee_AddEmployeeContact";
        public const string Employee_GetContact = "USP_Employee_GetEmployeeContact";
        public const string Employee_UpdateContact = "USP_Employee_UpdateEmployeeContact";
        public const string Employee_DeleteContact = "USP_Employee_DeleteEmployeeContact";
        public const string Employee_GetSeatDetails = "USP_Employee_GetSeatDetails";
        public const string Employee_AllocateSeat = "USP_Employee_AllocateSeat";

        public const string Employee_GetActiveEmployeesDetail = "USP_Employee_GetActiveEmployeeDetails";
        public const string Employee_GetEmployeesAllocation = "USP_Employee_GetEmployeesAllocation";

        public const string USP_EMPLOYEE_GETEMAILID = "USP_Employee_GetEmailID";

        public const string USP_EMPLOYEES_GETEMPLOYEENAMEBYEMAILID = "USP_Employees_GetEmployeeNameByEmailID";

        public const string USP_EMPLOYEE_GETPROJECTALLOCATION = "USP_Employee_GetProjectAllocation";
        public const string USP_Employee_GetEmployeesProjectDetails = "USP_Employee_GetEmployeesProjectDetails";

        public const string USP_Employee_GetEmployeeResumeDetails = "USP_Employee_GetEmployeeResumeDetails";
        public const string USP_Employee_DeleteEmployeeResumeDetails = "USP_Employee_DeleteEmployeeResumeDetails";
        public const string USP_Employee_AddEmployeeResumeDetails = "USP_Employee_AddEmployeeResumeDetails";
        public const string USP_Employee_GetEmployeeResumeCountDetails = "USP_Employee_GetEmployeeResumeCountDetails";

        //19645-Ambar-Start
        public const string USP_Employee_DelEmployeeResumeDetails = "USP_Employee_DelEmployeeResumeDetails";
        //19645-Ambar-End

        // CR - 25715 issue related to ePlatform MRF Sachin
        public const string Employee_ReleaseCheck = "USP_Employee_ReleaseCheck";
        // CR - 25715 Sachin End

        //Aarohi : Issue 28572(CR) : 03/02/2012 : Start
        public const string EMPLOYEES_UPDATE_FUNCTIONAL_AND_REPORTING_MANAGER = "USP_Employees_UpdateFunctionalAndReportingManager";
        public const string EMPLOYEES_UPDATE_REPORTING_MANAGER = "USP_Employees_UpdateReportingManager";
        public const string GET_REPORTING_FUNCTIONAL_MANAGER_IDS = "USP_Employees_GetReportingFunctionalManagerIds";
        public const string GET_REPORTING_ActiveEmployeeList = "USP_Employees_ActiveEmployeeList";
        public const string Employee_GetEmployeesListForFMRM = "USP_Employees_GetEmployeeDetails";
        public const string EMPLOYEE_GETReportingManager = "USP_Employee_CheckReportingManager";
        //Aarohi : Issue 28572(CR) : 03/02/2012 : End

        // 35901-Ambar-Start-27062012
        public const string Employee_GetAllocationReportingToEmailId = "USP_Employee_GetAllocationReportingToEmailId";
        // 35901-Ambar-End-27062012

        // Ishwar - NISRMS - 30102014 Start
        public const string Employee_GetNISEmployeeList = "USP_GetNISEmployeeList";
        // Ishwar - NISRMS - 30102014 End

        // Ishwar - NISRMS - 27112014 Start
        public const string Employee_GetEDCEmployeeCount = "USP_GetEDCEmployeeCount";
        // Ishwar - NISRMS - 27112014 End

        //Umesh: NIS-changes: Skill Search Report Starts
        public const string Employee_GetPrimaryAndSecondarySkills = "USP_GetPrimaryAndSecondarySkills";
        public const string Employee_GetSkillSearchDetails = "USP_GetGetSkillSearchDetails";
        //Umesh: NIS-changes: Skill Search Report Ends

        //Umesh: NIS-changes: Head Count Report Starts
        public const string Employee_Report_HeadCount = "USP_Report_HeadCount";
        //Umesh: NIS-changes: Head Count Report Ends

        #endregion SP for Employees

        #region SP for MRF

        public const string MRF_CHANGERECRUITMENTMANAGER = "USP_MRF_ChangeRecruitmentManager";
        public const string MRF_ApproveMRF = "dbo.USP_MRF_ApproveMRF";
        public const string MRF_Edit = "dbo.USP_MRF_Edit";
        public const string MRF_Move = "dbo.USP_MRF_Move";
        public const string MRF_GetApproveRejectHeadCountMRFDetails = "dbo.USP_MRF_GetApproveRejectHeadCountMRFDetails";
        public const string MRF_GetFilterPendingAllocationMRFDetails = "dbo.USP_MRF_GetFilterPendingAllocationMRFDetails";
        public const string MRF_GetInternalResource = "dbo.USP_MRF_GetInternalResource";
        public const string MRF_GetMRFSummaryForPageLoad = "USP_MRF_GetMRFSummaryForPageLoad";
        public const string MRF_GetMRFSummaryFilterAndPaging = "USP_MRF_GetMRFSummaryFilterAndPaging";
        public const string MRF_GetPendingAllocationMRFDetails = "dbo.USP_MRF_GetPendingAllocationMRFDetails";
        public const string MRF_Raise = "dbo.USP_MRF_Raise";
        public const string MRF_RejectMRF = "dbo.USP_MRF_RejectMRF";
        public const string MRF_View = "dbo.USP_MRF_View";
        public const string MRF_GetProjectName = "USP_MRF_GetProjectName";
        public const string MRF_GetMRFCode = "USP_MRF_GetMRFCode";
        public const string MRF_SetReasonOfApproveRejectMRF = "USP_MRF_SetReasonOfApproveRejectMRF";

        //Mahednra Issue Id : 33860
        public const string MRF_ConcurrencyCheckAllocation = "USP_MRF_checkConcurrencyResourceAllocation";
        //Mahendra Issue Id : 33860

        //Jignyasa Issue id : 42400,42315
        public const string MRF_GetClientNameFromProjectId = "USP_MRF_GetClientNameFromProjectID";
        //Jignyasa Issue id : 42400,42315

        public const string MRF_GetProjectName_RoleWise = "USP_MRF_GetProjectName_RoleWise";
        public const string MRF_GetRP_ProjectWise = "USP_MRF_GetRP_ProjectWise";
        public const string MRF_GetRole_RPWise = "USP_MRF_GetRole_RPWise";
        public const string MRF_GetResourcePlanDuration_RoleWise = "USP_MRF_GetResourcePlanDuration_RoleWise";
        public const string MRF_GetApproveRejectMrf = "USP_MRF_GetApproveRejectMrf";
        public const string MRF_GetProjectDomain = "USP_MRF_GetProjectDomain";
        public const string MRF_GetMRFEmployee = "USP_MRF_GetMRFEmployee";
        public const string MRF_CopyMRF = "USP_MRF_CopyMRF";
        public const string MRF_Copy = "USP_MRF_Copy";
        public const string MRF_SetMRFStatus = "USP_MRF_SetMRFStatus";
        public const string MRF_GetRecruitmentManager = "USP_MRF_GetRecruitmentManager";
        public const string MRF_GetEmployeeName = "USP_MRF_GetEmployeeName";
        public const string MRF_GetRole_DepartmentWise = "USP_MRF_GetRole_DepartmentWise";
        public const string MRF_Abort = "USP_MRF_Abort";
        public const string MRF_SetReasonOfApproveRejectMRFForPM = "USP_MRF_SetReasonOfApproveRejectMRFForPM";
        public const string MRF_GetMailDetailsOnApprovalOfMrf = "USP_MRF_GetMailDetailsOnApprovalOfMrf";
        public const string MRF_GetInternalResourceWithFilter = "USP_MRF_GetInternalResourceWithFilter";
        public const string MRF_GetInternalResourceWith_FilterCreatiria = "USP_MRF_GetInternalResourceWith_FilterCreatiria";
        public const string MRF_MRF_GetRPSplitDurationInOnsite_Offshore = "USP_MRF_GetRPSplitDurationInOnsite_Offshore";
        public const string MRF_GetMRFInternalResource = "USP_MRF_GetMRFInternalResource";
        public const string MRF_GetReportingTOofEmployee = "USP_MRF_GetReportingTOForEmployee";
        public const string MRF_GetReportingTOForEmployeeFromMRF = "USP_MRF_GetReportingTOForEmployeeFromMRF";
        public const string MRF_UpdateReportingTOForEmployeeAsPerMRF = "USP_MRF_UpdateReportingTOForEmployeeAsPerMRF";
        public const string MRF_CopyMRFList = "USP_MRF_CopyMRFList";
        public const string MRF_GetMRFRaiseAccesForDepartmentByEmpId = "USP_MRF_GetMRFRaiseAccesForDepartmentByEmpId";
        public const string USP_MRF_GetEmailIdForDeptWiseHeadCountApproval = "USP_MRF_GetEmailIdForDeptWiseHeadCountApproval";
        public const string USP_MRF_SetMRFSatusAfterApproval = "USP_MRF_SetMRFNextStatus";
        public const string USP_EMPLOYEE_EMPLOYEE_EXISTS = "USP_Employee_EmployeeExistsForProject";

        //get sp name for SLA Days for recruiter.
        public const string USP_MRF_GetSLADaysForRecruiter = "USP_MRF_GetSLADaysForRecruiter";

        //get sp name for allocation date details for resource.
        public const string USP_MRF_GetAllocationDateDetails = "USP_Mrf_GetAllocationDateDetails";
        public const string USP_MRF_GETMRFDETAILS = "USP_MRF_GetMRFDetails";
        public const string USP_MRF_GETEMPLOYEEBYMRFID = "USP_MRF_GetEmployeeByMRFID";
        public const string USP_MRF_GETCANDIDATEBYMRFID = "USP_MRF_GetCandidateByMRFID";
        public const string USP_MRF_GETDEPARTMENTHEADBYEMAIIDANDDEPTID = "USP_MRF_GetDepartmentHeadByEmaiIdAndDeptId";
        public const string USP_MRF_GetDepartmentHeadEmaiIdForRecruitment = "USP_MRF_GetDepartmentHeadEmaiIdForRecruitment";
        public const string USP_MRF_CheckSLARoles = "USP_MRF_CheckSLARoles";
        public const string USP_MRF_GetExpectedClosureDate = "USP_MRF_GetExpectedClosureDate";
        public const string USP_MRF_DELETE_FUTURE_ALLOCATE_EMPLOYEE = "USP_MRF_DeleteFutureAllocateEmployee";
        public const string USP_MRF_EDIT_FUTURE_ALLOCATE_EMPLOYEE = "USP_MRF_EditFutureAllocateEmployee";
        public const string USP_MRF_GETMRFDETAILSFORMOVEMRF = "USP_MRF_GetMRFDetailForMoveMRF";
        public const string MRF_GetEmployeeId = "USP_MRF_GetEmployeeId";
        public const string Master_GetDept_ProjectNames = "USP_Master_GetDept_ProjectNames";
        public const string MRF_SearchProjectDeptWiseMRFCode = "USP_MRF_SearchProjectDeptWiseMRFCode";
        public const string MRF_SearchProjectDeptWiseMRF_MRFRole = "USP_MRF_SearchProjectDeptWiseMRF_MRFRole";
        public const string MRF_CopyProjectDeptName = "USP_MRF_CopyProjectDeptName";
        public const string MRF_CopyMRFnew = "USP_MRF_CopyMRFnew";
        public const string MRF_Delete = "dbo.USP_MRF_Delete";

        //Aarohi : Issue 31838(CR) : 03/02/2012 : Start
        public const string GET_MRFID_BY_MRFCODE = "USP_MRF_GetMRFIdByMRFCode";
        public const string GET_MRF_CODE_FOR_RPG = "USP_MRF_GetMRFCodeForRPG";
        public const string EDIT_CANDIDATE_MRFDETAILS = "USP_MRF_EditCandidateMRFDetails";
        //Aarohi : Issue 31838(CR) : 03/02/2012 : End 

        //Mahendra STRAT 33960 Strat
        public const string GET_MRFID_BY_RPCODE = "USP_MRF_GetMRFIdByRP";
        //Mahendra STRAT 33960 End

        // Venkatesh : Issue48132 : 03/Jan/2014 : Starts                    
        // Desc : Check New Emp Allocation
        public const string USP_MRF_CheckNewEmployee_Allocation = "USP_MRF_CheckNewEmployee_Allocation";
        // Venkatesh : Issue48132 : 03/Jan/2014 : End 
        #region Modified By Mohamed Dangra
        // Mohamed : Issue 50791 : 12/05/2014 : Starts                        			  
        // Desc : IN Mrf Details ,GroupId need to implement
        public const string USP_GET_MRFDETAIL_MAX_GROUPID = "USP_GET_MRFDETAIL_MAX_GROUPID";
        // Mohamed : Issue 50791 : 12/05/2014 : Ends
        #endregion Modified By Mohamed Dangra

        // Venkatesh : Start NIS-RMS : 20-Oct-2014   
        // Desc :  NIS-RMS 
        public const string Employee_GetProjectNameNIS = "USP_Employee_GetProjectNameNIS";
        public const string Employee_GetConsolidated = "USP_Employee_GetConsolidated";
        // Venkatesh : End  NIS-RMS : 20-Oct-2014   


        // Mohamed : 03/12/2014 : Starts                        			  
        // Desc : NIS Changes
        public const string Employee_GetSkillsReport = "USP_GetSkillsReport";

        // Mohamed : 03/12/2014 : Ends

        //Ishwar NISRMS 16022015 Start
        public const string USP_MRF_AgingReport = "USP_Report_MrfAgingByPrimarySkillAndDesignation";
        public const string USP_MRF_AgingReportForOpenPosition = "USP_Report_MrfAgingByPrimarySkillAndDesignation_RecruitmentAgingReport";
        //Ishwar NISRMS 16022015 End

        //Siddharth 02-March-2015 Start
        public const string USP_MRF_GetFunctionalMangerAndReportingTo = "USP_MRF_GetFunctionalMangerAndReportingTo";
        //Siddharth 02-March-2015 End

        //Ishwar Patil 21/04/2015 Start
        public const string USP_MRF_GetPrimarySkills = "USP_GetPrimarySkills";
        //Ishwar Patil 21/04/2015 End
        #endregion

        #region SP for Common
        public const string Master_GetMasterData = "USP_Master_GetMasterData";
        public const string Master_GetDepartment = "USP_Master_GetDepartments";

        public const string Master_GetNorthgateUsername = "USP_Master_GetNorthgateUsername";

        public const string Master_CheckIfFunctionalManager = "USP_Master_CheckIfFunctionalManager";

        // Ishwar NISRMS 13032015 Start
        public const string Master_GetMasterDataForStatus = "USP_Master_GetMasterDataForStatus";
        // Ishwar NISRMS 13032015 End
        #endregion

        #region SP for Recruitment
        public const string Recruitment_GetMRFCode = "USP_Recruitment_GetMRFCode";
        public const string Recruitment_GetMRFDetails = "USP_Recruitment_GetMRFDetails";
        public const string Recruitment_AddPipelineDetails = "USP_Recruitment_AddPipelineDetails";
        public const string Recruitment_GetRecruitmentSummary_PageLoad = "USP_Recruitment_GetRecruitmentSummary_PageLoad";
        public const string Recruitment_GetRecruitmentSummary_FilterAndPaging = "USP_Recruitment_GetRecruitmentSummary_FilterAndPaging";
        public const string Recruitment_GetRecruitmentDetails = "USP_Recruitment_GetRecruitmentDetails";
        public const string Recruitment_RemoveRecruitmentRecord = "USP_Recruitment_RemoveRecruitmentRecord";
        public const string Recruitment_EditPipelineDetails = "USP_Recruitment_EditPipelineDetails";
        public const string Recruitment_ViewMRFCode = "USP_Recruitment_ViewMRFCode";
        public const string Recruitment_GetMRFDetailsForEmployee = "USP_Recruitment_GetMRFDetailsForEmployee";
        public const string Recruitment_GetDepartmentHeadByDepartmentId = "USP_Recruitment_GetDepartmentHeadByDepartmentId";
        public const string Recruitment_GetDepartmentDetailsForBusinessUnit = "USP_Recruitment_GetDepartmentDetails_ForBusinessUnit";
        public const string Recruitment_GetReportingToByEmployeeID = "USP_Recruitment_GetReportingToByEmployeeID";

        // 35913-Ambar-Start-06072012
        public const string Recruitment_GetCandidateJoinStatus = "USP_Recruitment_GetCandidateJoinStatus";
        // 35913-Ambar-End-06072012

        // Mohamed : Issue 50306 : 09/09/2014 : Starts                        			  
        public const string Recruitment_GetRecruiteEMailIdbyMRFId = "USP_Recruitment_GetRecruiterEMailIdbyMRFId";
        // Mohamed : Issue 50306 : 09/09/2014 : Ends

        #endregion

        #region SP for Resource Plan

        public const string ResourcePlan_AddResourcePlan = "USP_ResourcePlan_AddResourcePlan";

        public const string ResourcePlan_AddRPDuration = "USP_ResourcePlan_AddRPDuration";

        public const string ResourcePlan_AddRPDetail = "USP_ResourcePlan_AddRPDetail";

        public const string ResourcePlan_GetRPDuration = "USP_ResourcePlan_GetRPDuration";

        public const string ResourcePlan_GetRPDetail = "USP_ResourcePlan_GetRPDetail";

        public const string ResourcePlan_GetInactiveResourcePlan = "USP_ResourcePlan_GetInactiveResourcePlan";

        public const string ResourcePlan_GetInactiveRPDurationDetail = "USP_ResourcePlan_GetInactiveRPDurationDetail";

        public const string ResourcePlan_DeleteRPDuration = "USP_ResourcePlan_DeleteRPDuration";

        public const string ResourcePlan_DeleteRPDetail = "USP_ResourcePlan_DeleteRPDetail";

        public const string ResourcePlan_CreateRPDuration = "USP_ResourcePlan_CreateRPDuration";

        public const string ResourcePlan_GetResourcePlan = "USP_ResourcePlan_GetResourcePlan";

        public const string ResourcePlan_CreateResourcePlan = "USP_ResourcePlan_CreateResourcePlan";

        public const string ResourcePlan_RPDurationById = "USP_ResourcePlan_RPDurationById";

        public const string ResourcePlan_RPDetailById = "USP_ResourcePlan_RPDetailById";

        public const string ResourcePlan_DeleteResourcePlan = "USP_ResourcePlan_DeleteResourcePlan";

        public const string ResourcePlan_GetProjectDetailsForApproveRejectRP = "USP_ResourcePlan_GetProjectDetailsForApproveRejectRP";

        public const string ResourcePlan_SaveApproveRejectResourcePlan = "USP_ResourcePlan_SaveApproveRejectRP";

        public const string ResourcePlan_GetResourcePlanForApproveRejectRP = "USP_ResourcePlan_GetResourcePlanForApproveRejectRP";

        public const string ResourcePlan_GetActiveOrInactiveREsourcePlan = "USP_ResourcePlan_GetActiveOrInactiveResourcePlan";

        public const string ResourcePlan_GetProjectDetails = "USP_ResourcePlan_GetProjectDetails";

        public const string ResourcePlan_GetResourcePlanById = "USP_ResourcePlan_GetResourcePlanById";

        public const string ResourcePlan_AddRPEdited = "USP_ResourcePlan_AddRPEdited";

        public const string ResourcePlan_AddRPDetailEdited = "USP_ResourcePlan_AddRPDetailEdited";

        public const string ResourcePlan_DeleteRPEdited = "USP_ResourcePlan_DeleteRPEdited";

        public const string ResourcePlan_SaveResourcePlanEdited = "USP_ResourcePlan_SaveResourcePlanEdited";

        public const string ResourcePlan_SaveInactiveResourcePlanEdited = "USP_ResourcePlan_SaveInactiveResourcePlanEdited";

        public const string ResourcePlan_GetProjectByRPId = "USP_ResourcePlan_GetProjectByRPId";

        public const string ResourcePlan_GetDumpRPById = "USP_ResourcePlan_GetDumpRPById";

        public const string ResourcePlan_GetProjectManagerByProjectId = "USP_ResourcePlan_GetProjectManagerByProjectId";

        public const string ResourcePlan_GetResourcePlanByProjectId = "USP_ResourcePlan_GetResourcePlanByProjectId";
        public const string ResourcePlan_GetAllocatedResourceByProjectId = "USP_ResourcePlan_GetAllocatedResourceByProjectId";

        public const string ResourcePlan_GetPendingMRF = "USP_ResourcePlan_GetPendingMRF";

        public const string ResourcePlan_UpdateDatesRPDuration = "USP_ResourcePlan_UpdateDatesRPDuration";

        public const string ResourcePlan_CreateAndEditRPDuration = "USP_ResourcePlan_CreateAndEditRPDuration";

        public const string ResourcePlan_GetNoOfResourceByProjectId = "USP_ResourcePlan_GetNoOfResourceByProjectId";

        public const string ResourcePlan_GetResourcePlanDetailsForMail = "USP_ResourcePlan_GetResourcePlanDetailsForMail";

        public const string ResourcePlan_UpdateEmpProjectAllocationByResourcePlan = "USP_ResourcePlan_UpdateEmpProjectAllocationByResourcePlan ";

        //33126-Mahendra-Start
        public const string ResourcePlan_UpdateSendFroApprovalFlag = "USP_ResourcePlan_UpdateSendFroApprovalFlag";

        public const string ResourcePlan_GetResourcePlanApprovalStatus = "USP_ResourcePlan_GetRPApprovalStatus";
        //33126-Mahendra-End

        #endregion SP for Resource Plan

        #region SP for Seat Allocation Module

        public const string SeatAllocation_GetSeatDetailsForBay = "USP_SeatAllocation_GetSeatDetailsForBay";

        public const string SeatAllocation_ShiftLocation = "USP_SeatAllocation_ShiftLocation";

        public const string SeatAllocation_GetEmployeeDeatilsAtSeat = "USP_SeatAllocation_GetEmployeeDeatilsAtSeat";

        public const string SeatAllocation_SaveEmployeeDetails = "USP_SeatAllocation_SaveEmployeeDetails";

        public const string SeatAllocation_GetEmpName = "USP_SeatAllocation_GetEmpName";

        public const string SeatAllocation_Allocate = "USP_SeatAllocation_Allocate";

        public const string SeatAllocation_CheckEmployeeLocation = "USP_SeatAllocation_CheckEmployeeLocation";

        public const string SeatAllocation_GetEmployeeDetailsByID = "USP_SeatAllocation_GetEmployeeDetailsByID";

        public const string SeatAllocation_GetSeatDetailsByID = "USP_SeatAllocation_GetSeatDetailsByID";

        public const string SeatAllocation_GetUnallocatedEmployee = "USP_SeatAllocation_GetUnallocatedEmployee";

        public const string SeatAllocation_GetSection = "USP_SeatAllocation_GetSection";

        public const string SeatAllocation_SwapLocation = "USP_SeatAllocation_SwapLocation";
        #endregion

        #region SP For Reports

        /// <summary>
        /// define MRF_GetRecruitMentSLAReport
        /// </summary>
        public const string MRF_GetRecruitMentSLAReport = "USP_Report_GetRecruitMentSLAReport";

        #endregion SP For Reports

        #region SP for 4C

        public const string GET_Creator_Reviewer_Details = "USP_4C_GetCreatorApproverDetails";
        public const string GET_Filtered_Creator_Reviewer_Details = "USP_4C_GetFilteredCreatorApproverDetails";
        //public const string Update_Creator_Reviewer_Detail = "USP_4C_UpdateCreatorReviewer";
        //public const string Delete_Creator_Reviewer_Detail = "USP_4C_DeleteCreatorReviewer";
        public const string Add_Update_Delete_Creator_Reviewer_Detail = "USP_4C_AddUpdateDeleteCreatorReviewer";
        public const string Check_CreatorReviewerSetForAll = "USP_4C_CheckCreatorReviewerSetForAll";
        //public const string Check_AccessRights = "USP_4C_CheckAccessRight";
        public const string GET_ViewAccessRightsData = "USP_4C_GetViewAccessRightsDetails";
        public const string Add_Delete_ViewAccessRights = "USP_4C_AddDeleteViewAccessRights";
        public const string GET_Login_AccessRights = "USP_4C_CheckLoginAccessRight";
        public const string GET_Get4CEmployeeDetails = "USP_4C_Get4CEmployeedetails";
        public const string GET_Get4CReviewerEmployeeDetails = "USP_4C_Get4CReviewerEmployeedetails";
        public const string GET_Get4CIndividualEmployeeDetails = "USP_4C_Get4CIndividualEmployeedetails";
        public const string GET_ParameterOnC = "USP_4C_ParameterOnC";
        public const string GET_ProjectNameOnEmp = "USP_4C_GetProjectNameBasedOnEmp";
        public const string GET_ActionDetails = "USP_4C_GetActionDetails";
        public const string GET_ActionDetailsForDashboard = "USP_4C_GetActionDetailsForDashboard";
        public const string GET_EmployeeDashboard = "USP_4C_GetEmpDashbaord";
        public const string Check_InsertActionDetails = "USP_4C_InsertUpdateDeleteActionDetails";
        public const string Check_InsertFeedbackDetails = "USP_4C_InsertFeedbackDetails";
        public const string GET_ActionOwner = "USP_4C_GetActionOwner";
        public const string Check_UpdateFinalRating = "USP_4C_UpdateFinalRating";
        public const string Check_IsAllowToFillActionData = "USP_4C_IsAllowToFillActionData";
        public const string GET_4C_ProjectName = "USP_4C_GetProjectName";
        public const string GET_4C_DepartmentName = "USP_4C_GetDepartmentName";
        public const string GET_4C_FunctionalDeptName = "USP_4C_GetFunactionalManagerDeptName";
        public const string GET_4C_FunctionalProjectName = "USP_4C_GetFunactionalManagerProjectName";
        public const string GET_4C_FunctionalEmployeeName = "USP_4C_GetFunactionalManagerEmployeeName";
        public const string GET_Get4CCreatorDetails = "USP_4C_Get4CCreatorDetails";
        //public const string Check_RatingFillForAll = "USP_4C_CheckRatingFillForAll"; Removed
        public const string Submit_ReviewRating = "USP_4C_INSERTReviewRating";
        //public const string GET_SupportEmployeeList = "USP_4C_SupportEmployeeList";
        public const string GET_Get4CCreatorDetailsAtSupportLevel = "USP_4C_Get4CCreatorDetailsAtSupportLevel";
        public const string GET_View4CFeedbackDetails = "USP_4C_GET_View4CFeedbackDetails";
        public const string Get4CViewEmployeeFromRMS = "USP_4C_Get4CViewEmployeeFromRMS";
        public const string Check_AllowReviewer = "USP_4C_CheckAllowReviewer";
        public const string GetDepartmentCreatorApprover = "USP_4C_GetDepartmentCreatorApprover";
        public const string GetProjectAddCreatorApprover = "USP_4C_GetProjectAddCreatorApprover";
        public const string Check_IsAllowForNotApplicableOption = "USP_4C_IsAllowForNotApplicable";

        public const string GET_RedirectMonth = "USP_4C_GetRedirectedMonth";
        public const string GET_Get4CCreatorReviewerDetails = "USP_4C_Get4CCreatorReviewerDetails";

        public const string RMS_RelaseVal_4CFilledForAll = "USP_4C_RMSVal4CFilledForAll";

        public const string GET_4C_ActionReport = "USP_4C_ActionReport";
        public const string GET_4C_ConsolidatedReport = "USP_4C_ConsolidatedReport";
        public const string GET_4C_AnalysisReport = "USP_4C_AnalysisReport";
        public const string GET_4C_StatusReport = "USP_4C_StatusReport";
        public const string GET_4C_MovementReport = "USP_4C_MovementReport";
        public const string GET_4C_CountReport = "USP_4C_CountReports";
        //public const string GET_4C_CountReport = "USP_4C_CountReport";

        public const string Employee_GetEmployeesView = "USP_Employee_GetEmployeesView";
        public const string Get_4CReportDesignation = "USP_4C_EmployeeDesignationForReport";
        public const string GET_CurrentMonthLastDay = "USP_4C_getCurrentMonthLastday";

        public const string GET_Get4CCreatorReviewerMailsDetails = "USP_4C_Get4CCreatorReviewerMailsDetails";
        public const string Reject_4CRating = "USP_4C_Reject4CRating";
        public const string GET_Get4CRejectedMailsDetails = "USP_4C_GetRejectedMailsDetails";

        public const string GET_4C_ExportToExcelSentForReview = "USP_4C_ExportToExcelSentForReviewRatings";

        public const string GET_Get4CEmployeeDetails_CarryForward = "USP_4C_Get4CEmployeedetailsCarryForward";

        //Venkatesh : 4C_Support 26-Feb-2014 : Start 
        //Desc : Dept Support
        public const string GET_RedirectMonthSupport = "USP_4C_GetRedirectedMonth4CSupport";
        public const string Submit_ReviewRatingSupport = "USP_4C_INSERTReviewRatingSupport";
        public const string GET_Get4CCreatorReviewerMailsDetailsSupport = "USP_4C_Get4CCreatorReviewerMailsDetailsSupport";

        public const string GET_4C_ActionReportSupport = "USP_4C_ActionReportSupport";
        public const string GET_4C_ConsolidatedReportSupport = "USP_4C_ConsolidatedReportSupport";
        public const string GET_4C_AnalysisReportSupport = "USP_4C_AnalysisReportSupport";
        public const string GET_4C_StatusReportSupport = "USP_4C_StatusReportSupport";
        public const string GET_4C_MovementReportSupport = "USP_4C_MovementReportSupport";
        public const string GET_4C_CountReportSupport = "USP_4C_CountReportsSupport";
        //Venkatesh : 4C_Support 26-Feb-2014 : End

        #endregion SP for 4C

        //Ishwar Patil : Trainging Module 08/04/2014 : Starts
        #region SP for Training

        public const string TNI_InsertUpdateTechnicalTraining = "USP_TNI_InsertUpdateTechnicalTraining";
        public const string TNI_InsertUpdateKSSTraining = "USP_TNI_InsertUpdateKSSTraining";
        public const string TNI_InsertUpdateSeminarsTraining = "USP_TNI_InsertUpdateSeminarsTraining";
        public const string TNI_InsertUpdateSoftskillsTraining = "USP_TNI_InsertUpdateSoftskillsTraining";
        public const string TNI_ViewTechnicalTraining = "USP_TNI_ViewTechnicalTraining";
        //public const string TNI_ViewTechnicalTraining = "USP_TNI_ViewTechnicalTraining_Update";
        public const string TNI_GetTechnicalTraining = "USP_TNI_GetTechnicalTraining";
        public const string TNI_GetSoftSkillsTraining = "USP_TNI_GetSoftSkillsTraining";
        public const string TNI_GetKSSTraining = "USP_TNI_GetKSSTraining";
        public const string TNI_GetSeminarsTraining = "USP_TNI_GetSeminarsTraining";
        public const string TNI_DeleteTechnicalTraining = "USP_TNI_DeleteTechnicalTraining";
        public const string TNI_DeleteKSSTraining = "USP_TNI_DeleteKSSTraining";
        public const string TNI_ViewSoftSkillsTraining = "USP_TNI_ViewSoftSkillsTraining";
        public const string TNI_DeleteSoftSkillsTraining = "USP_TNI_DeleteSoftSkillsTraining";
        public const string TNI_ViewKSSTraining = "USP_TNI_ViewKSSTraining";
        public const string TNI_ViewSeminarsTraining = "USP_TNI_ViewSeminarsTraining";
        public const string TNI_DeleteSeminarsTraining = "USP_TNI_DeleteSeminarsTraining";
        public const string TNI_GetMasterSP = "USP_TNI_GetMasterSP";

        public const string TNI_GetMonthsSP = "USP_TNI_GetMonths";
        
        public const string TNI_GetTrainingNameForSubmitNomination = "USP_TNI_GetTrainingNameForSubmitNomination";

        public const string TNI_ApproveRejectViewKSSTraining = "USP_TNI_ApproveRejectViewKSSTraining";
        public const string TNI_ApproveRejectViewTechnicalTraining = "USP_TNI_ApproveRejectViewTechnicalTraining";
        public const string TNI_ApproveRejectViewSoftSkillsTraining = "USP_TNI_ApproveRejectViewSoftSkillsTraining";
        public const string TNI_ApproveRejectViewSeminarsTraining = "USP_TNI_ApproveRejectViewSeminarsTraining";
        public const string TNI_UpdateApproveRejectTechnicalTraining = "USP_TNI_UpdateApproveRejectTechnicalTraining";
        public const string TNI_EditKSSTraining = "USP_TNI_EditKSSTraining";
        public const string TNI_AccessRightForTrainingModule = "USP_TNI_AccessRightForTrainingModule";
        public const string TNI_UpdateRaiseTrainingStatus = "USP_TNI_UpdateRaiseTrainingStatus";
        public const string TNI_CheckDuplication = "USP_TNI_CheckDuplication";

        public const string TNI_GetEmailIDDetails = "USP_TNI_GetEmailIDDetails";
        public const string TNI_GetEmailIDDetailsForKSS = "USP_TNI_GetEmailIDDetailsForKSS";
        public const string TNI_GetEmailIDForAppRejTechSoftSkill = "USP_TNI_GetEmailIDForAppRejTechSoftSkill";
        public const string TNI_GetCurrentCourseTrainingName = "USP_TNI_GetCurrentCourseTrainingName";
        public const string TNI_GetConfirmCourseTrainingName = "USP_TNI_GetConfirmCourseTrainingName";
        //Harsha Issue Id-59073 - Start
        //Description- Training : Create emp Nominated page so that emp can view they nomination. Change confirmation email body
        public const string TNI_GetActiveTrainingCourseName = "USP_TNI_GetActiveTrainingCourseName";
        public const string TNI_ViewNominationDetails = "USP_TNI_ViewNominatonDetails";
        //Harsha Issue Id-59073 - End
        public const string TNI_GetTrainingDetailByID = "USP_TNI_GetTrainingDetailByID";
        public const string TNI_GetAllActiveEmployeeList = "USP_Employees_GetAllActiveEmployeeList";
        public const string TNI_GetEmployeeDetailwithProject = "USP_Employee_GetEmployeeDetailwithProject";
        public const string TNI_GetNominatedEmployeeListByManagerID = "USP_TNI_GetNominatedEmployeeListByManagerID";
        public const string TNI_USP_TNI_SaveTrainingNomination = "USP_TNI_SaveTrainingNomination";
        public const string TNI_USP_TNI_SubmitTrainingNomination = "USP_TNI_SubmitTrainingNomination";
        public const string TNI_USP_TNI_DeleteTrainingNomination = "USP_TNI_DeleteTrainingNomination";
        public const string TNI_USP_EditTrainingNomination = "USP_SelectNomineeForEdit";
        public const string TNI_USP_TNI_UpdateTrainingNomination = "USP_TNI_UpdateTrainingNominee";
        public const string TNI_USP_GetSubmittedNominationListByCourseID = "USP_TNI_GetSubmittedNominationListByCourseID";
        public const string TNI_USP_SaveUpdateConfirmNomination = "USP_TNI_SaveUpdateConfirmNomination";        
        
        //Attendance
        public const string TNI_GetAttendance = "USP_TNI_GetAttendance";
        public const string TNI_GetAttendanceSeminar = "USP_TNI_GetAttendanceSeminar";
        
        public const string TNI_GetAttendanceAll = "USP_TNI_GetAttendanceAll";
        public const string TNI_GetConfirmTraining = "USP_TNI_GetConfirmTraining";
        public const string TNI_GetSeminarKSSList = "USP_TNI_GetSeminarKSSList";
        
        public const string TNI_InsertUpdateGetAttendance = "USP_TNI_InsertUpdateGetAttendance";
        public const string TNI_InsertUpdateGetAttendanceSeminar = "USP_TNI_InsertUpdateGetAttendanceSeminar";
        public const string TNI_InsertUpdateGetAttendanceKss = "USP_TNI_InsertUpdateGetAttendanceKss";
        public const string TNI_InsertUpdateFeedbackSent = "USP_TNI_InsertUpdateFeedbackSent";        
        public const string TNI_InsertAttendanceDate = "USP_TNI_InsertAttendanceDate";
        public const string TNI_GetAttendanceDate = "USP_TNI_GetAttendanceDate";
        public const string TNI_GetNominationDetailsByCourseId = "USP_TNI_GetNominationDetailsByCourseId";

        //Training Effectiveness
        public const string TNI_GetTrainingEffectiveness = "USP_TNI_GetTrainingEffectiveness";
        public const string TNI_USP_TNI_UpdateTrainingEffectivenessDetails = "USP_TNI_UpdateTrainingEffectivenessDetails";
        //Neelam Issue Id:60562 start
        public const string TNI_USP_TNI_UpdateTrainingNominatorEmpID = "USP_TNI_UpdateTrainingNominatorEmpID";
        //Neelam Issue Id:60562 end
        public const string TNI_USP_TNI_GetTrainingNameEffectiveness = "USP_TNI_GetTrainingNameEffectiveness";
        public const string TNI_USP_TNI_SendTrainingEffectivenessDetails = "USP_TNI_SendTrainingEffectivenessDetails";
        public const string TNI_USP_TNI_SentManagerForPostTrainingRating = "USP_TNI_SentManagerForPostTrainingRating";
        public const string TNI_USP_TNI_ManagerFilledPostTrainingRating = "USP_TNI_ManagerFilledPostTrainingRating";
        public const string TNI_USP_TNI_CheckInActiveManagerForTrainingEffectivness = "USP_TNI_CheckInActiveManagerForTrainingEffectivness";
        public const string TNI_USP_TNI_GetDepartmentWiseKSSType = "USP_TNI_GetDepartmentWiseKSSType";
        public const string TNI_USP_TNI_GetMasterTrainingType = "USP_TNI_GetMasterTrainingType";
        public const string TNI_ReportAttendance = "USP_TNI_ReportAttendance";


        public const string USP_TNI_GetRMSRoles = "USP_GetRMSRoles";
        #endregion SP for Training

        #region CommonSPs
        public const string USP_RaveHR_MasterSP = "USP_RaveHR_MasterSP";
        public const string GET_ActiveEmployeeList = "USP_Employees_ActiveEmployeeList";
        #endregion CommonSPs

        #region SP for Training Course

        public const string GetTrainingVendorMaster = "USP_TNI_GetTrainingVendorMaster";
        public const string TrainingCourseSave = "USP_TNI_TrainingCourseInsert";
        public const string GetCoursesByTrainingType = "USP_TNI_GetCoursesTrainingType";
        public const string ApprovedTrainingName = "USP_TNI_ApprovedTrainingName";
        public const string GetTrainingVendorEmail = "USP_TNI_TrainingVendorEmail";
        public const string GetRaiseTrainingsByName = "USP_TNI_GetRaiseTrainingsByName";
        public const string GetTrainingCourses = "USP_TNI_GetTrainingCourses";
        public const string GetRaiseTrainingDetails = "USP_TNI_GetRaiseTrainingDetails";
        public const string GetCoursesByID = "USP_TNI_GetCoursesByID";
        public const string TrainingCourseDelete = "USP_TNI_TrainingCourseDelete";
        public const string TrainingCourseClose = "USP_TNI_TrainingCourseClose";
        public const string GetRaiseTrainingsById = "USP_TNI_GetRaiseTrainingsById";
        public const string GetCourseFilePath = "USP_TNI_GetCourseFilePath";
        public const string GetInvoiceFilePath = "USP_TNI_GetInvoiceFilePath";
        public const string UpdateCourseFilePath = "USP_TNI_UpdateCourseFilePath";
        public const string UpdateInvoiceFilePath = "USP_TNI_UpdateInvoiceFilePath";
        public const string GetInviteNominationDetails = "USP_TNI_GetInviteNominationDetails";
        public const string UpdateCourseNominationDate = "USP_TNI_UpdateCourseNominationDate";
        public const string GetMenuItemData = "USP_GetMenuItemData";
        public const string GetEmployeeRole = "USP_GetEmployeeRole";
        public const string CoursePaymentUpdate = "USP_TNI_CoursePaymentUpdate";
        public const string GetDuplicateStatusOfCourseName = "USP_TNI_GetDuplicateStatusOfCourseName";
        public const string TrainingCourseFileDetails = "USP_TNI_InsertUpdateFileDetails";
        public const string GetTrainingCoursesPageWise = "USP_TNI_GetTrainingCoursesPageWise";
        public const string GetReportingManagerEmailIds = "USP_TNI_GetReportingManagerEmailIds";
        //Harsha- Issue Id- 58975 & 58958 - Start
        //Description- Making Training Cost Editable for Internal Training after Closed status of the training
        public const string UpdateInternalTrainingCost = "USP_TNI_UpdateInternalTrainingCost";
        //Harsha- Issue Id- 58975 & 58958 - End

        #endregion SP for Training Course

        #region SP for AccomodationFooddetails

        public const string SaveAccomodationFoodDetails = "USP_SaveAccomodationFoodDetails";
        public const string SaveTravelDetails = "USP_SaveTravelDetails";
        public const string TNI_GetAccomodationDetailsByCourseID = "USP_TNI_GetAccomodationDetailsByCourseID";
        public const string GetTrainingNameByCourseID = "USP_TNI_GetTrainingNameByCourseID";
        public const string GetTravelDetailsByCourseID = "USP_TNI_GetTravelDetailsByCourseID";

        #endregion

        #region SP for Feedback
        public const string GetDescriptionByQuestionID = "USP_TNI_GetDescriptionByQuestionID";
        public const string GetTrainingNameList = "USP_TNI_GetTrainingNameList";
        public const string SaveFeedbackForm = "USP_TNI_SaveFeedbackForm";
        public const string GetEmployeeNameByEmpId = "USP_TNI_GetEmployeeNameByEmpId";
        public static string GetFeedbackRatings = "USP_TNI_GetFeedbackRatings";
        public static string GetEmployeesFeedbackByCourseId = "USP_TNI_GetEmployeesFeedbackByCourseId";
        public static string GetTrainingNameListForRMO = "USP_TNI_GetTrainingNameListForRMO";
        public static string SaveFeedbackDetailsByRMO = "USP_TNI_SaveFeedbackDetailsByRMO";
        public static string GetFeedbackDetailsForRMO = "USP_TNI_GetFeedbackDetailsForRMO";
        public static string GetAverageRatingsFromParticipants = "USP_TNI_GetAverageRatingsFromParticipants";
        public static string GetTrainingDetailsByCourseId = "USP_TNI_GetTrainingDetailsByCourseId";
        public static string GetTrainerNameByCourseID = "USP_TNI_GetTrainerNameByCourseID";
        public static string UpdateFlagToFeedbackFilled = "USP_TNI_UpdateFlagToFeedbackFilled";
        public static string SaveAndUpdateStatusOfTraining = "USP_TNI_SaveAndUpdateStatusOfTraining";
        public static string GetTrainingVendorDetails = "USP_TNI_GetTrainingVendorDetails";
        public static string GetVendorDetailsByVendorId = "USP_TNI_GetVendorDetailsByVendorId";
        public static string SaveVendorDetails = "USP_TNI_SaveVendorDetails";
        public static string UpdateVendorDetails = "USP_TNI_UpdateVendorDetails";
        public static string DeleteVendorDetails = "USP_TNI_DeleteVendorDetails";
        public static string GetDuplicateStatusOfEmialId = "USP_TNI_GetDuplicateStatusOfEmialId";

        public const string getParticipantsCount = "USP_TNI_GetParticipantsCount";
        public const string getFeedbackFilledCount = "USP_TNI_GetFeedbackFilledCount";
        #endregion

        #region SP for Assessment Module
        public const string GetOpenTrainingCourses = "USP_TNI_GetOpenTrainingCourses";
        public const string AssessmentPaperSave = "USP_TNI_InsertAssessmentPaper";
        public const string GetAssessmentPaperDetailsById = "USP_TNI_GetAssessmentPaper";
        public const string Assessment_QuestionSave = "USP_TNI_InsertAssessmentPaperQuestions";
        public const string Assessment_DeleteQuestion = "USP_TNI_DeleteAssessmentPaperQuestion";
        public const string GetAssessmentQuestionDetails = "USP_TNI_GetAssessmentQuestionDetails";
        public const string Assessment_UpdateQuestionDetails = "USP_TNI_UpdateAssessmentQuestionDetails";
        public const string GetTrainingByTrainingNameId = "USP_TNI_GetTrainingByTrainingNameId";
        public const string GetAssessmentPaperDetailsByCourseId = "USP_TNI_GetAssessmentPaperByCourseId";
        public const string AddExistingQuestionInPaper = "USP_TNI_InsertAssessmentPaper_QuestionMapping";
        public const string AddAssessmentResult = "USP_TNI_InsertAssessmentResult";
        public const string SetAssessment = "USP_TNI_SetAssessment";
        public const string TNI_GetNominatedEmp = "Usp_TNI_GetNominatedEmp";
        #endregion

        #region SP for Self Learning

        public const string TNI_InsertMyTraining = "USP_TNI_InsertMyTraining";
        public const string TNI_GetSelfTrainingsByEmpID = "USP_TNI_GetSelfTrainingsByEmpID";
        public const string TNI_DeleteSelfTraining = "USP_TNI_DeleteSelfTraining";
        public const string TNI_GetSelfTrainingDetails = "USP_TNI_GetSelfTrainingDetails";

        #endregion


        #region TrainingPlan
        public const string InsertUpdateTrainingPlan = "USP_TNI_InsertUpdateTrainingPlan";
        public const string GetTrainingPlan = "USP_TNI_GetTrainingPlan";
        public const string DeleteTrainingPlan = "USP_TNI_DeleteTrainingPlan";
        public const string GetTrainingPlanEmployees = "USP_TNI_GetTrainingPlanEmployees";
        public static string GetSeminarKSSCourse = "USP_TNI_GetSeminarKSSCourse";
        #endregion

        public const string InsertError = "USP_InsertError";

        #endregion Names of stored procedures used in Data Access layer

        //Rakesh
        #region RMS Admin
        public const string InsertDomain_SubDomain = "USP_RMS_Admin_InsertDomain_SubDomain";
        public const string Get_Domain = "USP_RMS_Admin_GetDomain";

        public const string Get_OtherMaster = "USP_RMS_Admin_GetMaster";
        public const string Update_OtherMaster = "USP_RMS_Admin_UpdateMaster";
        public const string Insert_OtherMaster = "USP_RMS_Admin_InsertMaster";
        public const string Delete_OtherMaster = "USP_RMS_Admin_DeleteMaster";
        public const string Get_GroupMaster = "USP_RMS_Admin_GetGroupMaster";


        #region Budget

        public const string Get_Projects = "USP_Budget_Projects";
        public const string Get_Budget = "USP_RMS_Admin_GetBudget";
        public const string Update_Budget = "USP_RMS_Admin_UpdateBudget";
        public const string Insert_Budget = "USP_RMS_Admin_Insert_Budget";
        public const string Delete_Budget = "USP_RMS_Admin_DeleteBudget";


        #endregion 

        #endregion

        //Interview Panel
        public const string GetDepartment = "USP_GetDepartment";
    }
}
