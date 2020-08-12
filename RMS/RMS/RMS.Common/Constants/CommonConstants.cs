using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Common.Constants
{
    public static class CommonConstants
    {

        /// <summary>
        /// Application name
        /// </summary>
        public const string APPLICATIONNAME = "RaveHR";

        public const string EMPLOYEE_RELEASED = "EmpReleased";

        public const string CANCELCLICK = "cancel";

        public const string PREVIOUSPAGE = "Prev";

        public const string RESOURCEPLANTABLEDATA = "RPTxtFile";

        public const string EXTENSION = ".txt";

        public const string DATANOTSAVED = "Do you want to save your details?";

        public const string DATANOTSAVEDCAPTION = "Data Not Saved";

        public const string ISCANCELED = "IsCanceled";

        /// <summary>
        /// Logger Name
        /// </summary>
        public const string LOGGERNAME = "RaveHRLogger";

        /// <summary>
        /// The name of the connection string stored in the config file.
        /// </summary>
        public const string ConnectionName = "RaveHRConnection";

        /// <summary>
        /// Debug category name 
        /// </summary>
        public const string DEBUG = "Debug";

        /// <summary>
        /// Exception category name 
        /// </summary>
        public const string EXCEPTION = "Exception";

        /// <summary>
        /// Warning category name 
        /// </summary>
        public const string WARNING = "Warning";

        /// <summary>
        /// General category name 
        /// </summary>
        public const string GENERAL = "General";

        /// <summary>
        /// Report Generation Status as Completed
        /// </summary>
        public const string COMPLETED = "COMPLETED";

        public const string InActive = "InActive";

        /// <summary>
        /// EmailId of RPG Group
        /// </summary>
        //public const string EmailIdOfRPGGroup = "MrfGroup@rave-tech.com";


        /// <summary>
        /// EmailId of Whizible Group
        /// </summary>

        public const string EmailIdOfITSGroup = "its@rave-tech.com";

        public const string WhizibleGroupEmailId = "Whizible.Support.Team@northgate-is.com";

        public const string SAIGIDDUNISEmailId = "sai.giddu@northgate-is.com";
        public const string SAIGIDDUNISandNAVYANISEmailId = "sai.giddu@northgate-is.com,navya.annamraju@northgate-is.com";
        #region Modified By Mohamed Dangra
        // Mohamed : NIS-RMS : 02/01/2015 : Starts                        			  
        // Desc : Add Naviya name where there is Sai name "navya.annamraju@northgate-is.com"				
        public const string NAVYANISEmailId = "navya.annamraju@northgate-is.com";
        // Mohamed : NIS-RMS : 02/01/2015 : Ends
        #endregion Modified By Mohamed Dangra

        public const string EmailIdOfRMOGroup = "rave.rm.group@northgate-is.com";
        //Ishwar Patil : Training Module : 20/08/2014 Start
        public const string EmailIdForKSS = "nitin.madkaikar@northgate-is.com";
        //Ishwar Patil : Training Module : 20/08/2014 End

        /// <summary>
        /// Date Format variable
        /// </summary>
        public const string DATE_FORMAT = "dd/MM/yyyy";


        public const string SIGNATURE = "Signature";

        public const string INVALIDURL = "URLTampered.htm";

        /// <summary>
        /// Benguluru Location
        /// </summary>
        public const string BENGULURU = "Benguluru";


        /// <summary>
        /// RMS URL Constant
        /// </summary>
        /// 
        public const string RMSURL = "RMSUrl";

        /// <summary>
        /// Rave Domain
        /// </summary>
        /// 
        //Googleconfigurable
        //public const string RAVEDOMAIN = "@rave-tech.com";

        public const string FutureAllocation = "Future";

        public const string CurrentAllocation = "Current";

        //Venkatesh : 4C_Support 26-Feb-2014 : Start 
        //Desc : Dept Support
        public const string SupportDept = "SupportDept";
        public const int SupportDeptStartMonth = 03; // VRP Hardcode
        public const int SupportDeptStartYear = 2014;
        //Venkatesh : 4C_Support 26-Feb-2014 : End


        #region User Defined Messages

        /// <summary>
        /// Error in page load
        /// </summary>
        public const string PAGELOADERROR = "An error occurred while loading form";

        /// <summary>
        /// Error in sending mail
        /// </summary>
        public const string MAILMESSAGE = "Error in sending Mail";

        /// <summary>
        /// Project Summary form load error
        /// </summary>
        public const string PROJECT_SUMMARY_FORM_LOAD_ERROR = "An error occurred while loading the Project Summary form";

        public const string ApproveReject_Project_FORM_LOAD_ERROR = "An error occurred while loading the Approve/Reject project form";

        /// <summary>
        /// Defines a constant error for no records found after applying filter criteria
        /// </summary>
        public const string NO_RECORDS_FOUND_MESSAGE = "No records found.";

        #region Messages for Contract Module
        public const string CONTRACT_SUMMARY_FORM_FILTER_BUTTON = "An error occurred while filtering the Contract Summary Page";
        public const string CONTRACT_SUMMARY_NO_MATCHING_RECORDS_FOUND_MESSAGE = "No matching records found.";
        public const string CONTRACT_UPDATE_SUCESSFULL = "Contract and project details have been sucessfully update, email notification is sent.";
        #endregion

        #endregion

        #region Messages for Advanced Search Screen
        /// <summary>
        /// form load error
        /// </summary>
        public const string PAGE_LOAD_ERROR = "An error occurred while loading the form";

        /// <summary>
        /// form reset error
        /// </summary>
        public const string RESET_CLICK_ERROR = "Error on page reset";

        /// <summary>
        /// project search error
        /// </summary>
        public const string SEARCH_PROJECTS_ERROR = "Error in project search";

        /// <summary>
        /// no records message
        /// </summary>
        public const string NO_RECORD_MESSAGE = "No. Records Found";

        #endregion Messages for Advanced Search Screen

        #region User Defined Messages
        /// <summary>
        /// Error page due to code error
        /// </summary>
        public const string ERROR_PAGE = "ErrorPage.htm";
        public const string UNAUTHORISEDUSER = "UnAuthorisedUser.htm";
        public const string ADDCONTRACT_PAGE = "AddContract.aspx";
        #endregion User Defined Messages

        #region File Path Variables

        /// <summary>
        /// Plus sign Image path
        /// </summary>
        public const string IMAGE_PLUSSIGNPATH = "Images/plus.jpg";

        /// <summary>
        /// Minus sign Image path
        /// </summary>
        public const string IMAGE_MINUSSIGNPATH = "Images/minus.jpg";

        /// <summary>
        /// Up Arrow image path
        /// </summary>
        public const string IMAGE_UP_ARROW = "Images/arrow_up.gif";

        /// <summary>
        /// Down Arrow image path
        /// </summary>
        public const string IMAGE_DOWN_ARROW = "Images/arrow_down.gif";

        /// <summary>
        /// MailRecepient file
        /// </summary>
        //public const string MAIL_FUNCTIONALITY_FILEPATH = "config/mailrecepient.xml";

        /// <summary>
        /// MailRecepient file
        /// </summary>
        public const string EMAIL_CONFIGURATION_FILEPATH = "config/EmailConfig.xml";

        /// <summary>
        /// MailRecepient file
        /// </summary>
        //public const string EMAIL_CONFIGURATION_FILEPATH_LevelBack = "../config/EmailConfig.xml";

        #endregion File Path Variables

        #region ValidationFunction
        public const string VALIDATE_ALPHA_NUMERIC_FUNCTION = "IsAplhaNumeric";
        public const string VALIDATE_ALPHA_NUMERIC_WITDSPACE = "ALPHANUMERIC_WITHSPACE";
        public const string VALIDATE_NUMERIC_FUNCTION = "IsNumeric";
        public const string VALIDATE_ISALPHABET = "IsAlphabet";
        public const string VALIDATE_ALPHABET_WITHSPACE = "ALPHABET_WITHSPACE";
        public const string VALIDATE_ALPHABET_WITHSPECIALCHAR = "ALPHABET_WITHSPECIALCHAR";
        public const string VALIDATE_BILLINGNUMERIC_FUNCTION = "IsBillingNumeric";
        public const string VALIDATE_DECIMAL_FUNCTION = "Decimal";


        #endregion

        #region ValidationMessage
        public const string MSG_ALPHA_NUMERIC = "Please enter alpha numeric text.";
        public const string MSG_ALPHABET = "Please enter alphabet text with out space.";
        public const string MSG_NUMERIC = "Enter numeric value.";
        public const string MSG_MULTILINETEXTBOX_LENGTH = "Only 100 characters allowed.";
        public const string MSG_DATERANGE = "End Date must be greater than Start date.";
        public const string MSG_DECIMAL = "Please enter numeric value.";
        public const string MSG_DATE = "Enter date";
        public const string MSG_ONLY_ALPHABET = "Kindly enter only alphabets in this field.";
        public const string MSG_MAXVALUE = "Please enter a postive value equal to or less than 100";
        public const string MSG_ALPHABET_SPECIALCHAR = "Only alphabets and '-' is allowed.";
        public const string MSG_DATE_FORMAT = "Enter Date in DD/MM/YYYY format.";

        #endregion

        #region ValidationEvents
        public const string EVENT_ONCLICK = "onclick";
        public const string EVENT_ONBLUR = "onblur";
        public const string EVENT_ONMOUSEOVER = "onmouseover";
        public const string EVENT_ONMOUSEOUT = "onmouseout";
        public const string EVENT_ONKEYPRESS = "onKeyPress";
        // 26755-Ambar-Start
        public const string EVENT_ONFOCUS = "onfocus";
        // 26755-Ambar-End
        #endregion

        #region Key Value

        public const string DDL_DataTextField = "Val";
        public const string DDL_DataValueField = "KeyName";
        public const string DDL_DataGroupField = "Group";

        #endregion

        #region SELECT
        public const string SELECT = "SELECT";
        public const int ZERO = 0;
        public const string YES = "Yes";
        public const int ONE = 1;
        public const string FIRM = "799";
        public const string FORECAST = "800";

        #endregion

        #region Constants for MRF

        public const string ASCENDING = "Ascending";
        public const string DESCENDING = "Descending";
        public const string NEW_LINE = "</br>";
        public const string MRF_CODE = "MrfCode";
        public const string RP_CODE = "RpCode";
        public const string ROLE = "Role";
        public const string PROJECT_ID = "ProjectId";
        public const string DATE_OF_REQUISITION = "DateOfRequisition";
        public const string RAISED_BY = "RaiseBy";
        public const string STATUS = "Status";
        public const string DEPARTMENT_NAME = "DeptName";
        public const string PROJECT_NAME = "ProjectName";
        public const string MRF_ID = "MRFId";
        public const string EMPLOYEE_NAME = "EmployeeName";
        public const string RESOURCE_ON_BOARD = "ResourceOnBoard";
        public const string EXPECTED_CLOSURE_DATE = "ExpectedClosureDate";
        public const string RESOURCE_RELEASED = "ResourceReleased";
        public const string UTILIZATION = "Utilization";
        public const string BILLING = "Billing";
        public const string CLIENT_NAME = "ClientName";
        public const string MRF_CTC = "MRFCTC";
        public const string RESOURCE_NAME = "ResourceName";
        public const string CURRENT_PROJECT_NAME = "CurrentProjectName";
        public const string RELEASE_DATE = "ReleaseDate";
        public const string START_DATE = "StartDate";
        public const string END_DATE = "EndDate";
        public const string RAISE_BY = "RaisedBy";
        public const string TARGET_CTC = "TargetCTC";
        public const string DEPARTMENT = "Department";
        public const string MRF_RAISED_BY = "MRFRaisedBy";
        public const string DEPT_NAME = "DeptName";
        public const string RESOURCE_JOIN = "ResourceJoin";
        public const string RECRUITERNAME = "RecruiterName";
        public const string ABORTEDORCLOSEDDATE = "LastModifiedDate";
        public const string ALLOCATIONDATE = "AllocationDate";

        public const string TYPE_OF_SUPPLY = "TypeOfSupply";
        public const string TYPE_OF_ALLOCATION = "TypeOfAllocation";
        public const string FUTURE_ALLOCATION_DATE = "FutureAllocationDate";
        public const string FUTURE_Allocate_ResourcName = "FutureAllocateResourceName";


        //Siddharth 30 March 2015 Start
        public const string BUSINESS_AREA = "BusinessArea";
        public const string BUSINESS_SEGMENT = "BusinessSegment";
        public const string COST_CODE = "CostCode";
        public const string RESOURCE_TYPE = "ResourceType";
        public const string RESOURCE_TYPE_COUNT = "ResourceTypeCount";
        public const string RESOURCE_TYPE_SKILL_RPT = "Resource Type";
        public const string RESOURCE_TYPE_COUNT_SKILL_RPT = "Resource Type Count";
        public const string BUSINESS_AREA_SKILL_RPT = "Business Area";
        public const string BUSINESS_SEGMENT_SKILL_RPT = "Business Segment";
        public const string TECHNOLOGY_MRF_AGING_RPT = "Technology";
        public const string DESIGNATION_MRF_AGING_RPT = "Designation";
        public const string AVGTIMETAKEN_MRF_AGING_RPT = "AverageTimeTaken";
        public const string PROJECT_NAME_MRF_AGING_RPT = "ProjectName";
        public const string MRFCODE_MRF_AGING_RPT = "MRFCode";
        public const string RECRUITMENT_START_DATE_MRF_AGING_RPT = "RecruitmentStartDate";
        public const string AGING_MRF_AGING_RPT = "Aging";
        public const string AVGAGING_MRF_AGING_RPT = "AvgAging";
        public const string EMPLOYEENAME_EMP_SKILL_SEARCH_RPT = "EmployeeName";
        public const string DESIGNATION_EMP_SKILL_SEARCH_RPT = "Designation";
        public const string DEPARTMENT_EMP_SKILL_SEARCH_RPT = "Department";
        public const string PROJECTS_ALLOCATED_EMP_SKILL_SEARCH_RPT = "ProjectsAllocated";
        public const string PRIMARY_SKILLS_EMP_SKILL_SEARCH_RPT = "PrimarySkills";
        public const string SKILLS_CONSOLIDATED_SUMMARY_RPT = "[Skills]";
        public const string RESOURCE_TYPE_SKILL_CONSOLIDATED_SUMMARY_RPT = "[Resource Type]";
        public const string COST_CODE_SKILL_CONSOLIDATED_SUMMARY_RPT = "CostCode";
        public const string PROJECT_START_DATE_SKILL_CONSOLIDATED_SUMMARY_RPT = "ProjectStartDate";
        public const string PROJECT_END_DATE_SKILL_CONSOLIDATED_SUMMARY_RPT = "ProjectEndDate";
        public const string JOB_TITLE_SKILL_CONSOLIDATED_SUMMARY_RPT = "[Job Title]";
        //Siddharth 30 March 2015 End



        public const string MSG_MRFREQUIREDFROM = "Required From should be greater then Project Start Date i.e ";
        public const string MSG_MRFREQUIREDFROM_Date = " and less than Project End Date i.e ";

        public const string MSG_MRFREQUIREDTILL = "Required Till should be greater then Project Start Date i.e ";
        public const string MSG_MRFREQUIREDTILL_Date = " and less than Project End Date i.e ";

        public const string MSG_MRFREQUIRED_FROM_GREATEREQUAL = "Please select the required from Date lesser then required till date.";

        public const string MSG_MRFREQUIRED_TILL_LESSEQUAL = "Please select the required till date greater than required from date";

        public const string CHECKGRIDVALUE = "CheckGridValue";

        public const string RESOURCEPLANDURATIONID = "ResourcePlanDurationId";

        public const string RESOURCEPLANSTARTDATE = "ResourcePlanStartDate";

        public const string RESOURCEPLANENDDATE = "ResourcePlanEndDate";

        public const string RESOURCEPLANENDLOCATION = "ResourceLocation";

        public const string RESOURCEPLANBILLING = "Billing";

        public const string RESOURCEPLANUTILIZATION = "Utilization";

        public const string RESOURCEBILLINGDATE = "BillingDate";

        public const string MSG_MRFSAVEED = "MRF Saved Successfully";

        public const string APM = "Assistant Project Manager-APM";

        public const string PM = "Project Manager-PM";

        public const string SPM = "Senior Project Manager-SPM";
        // Rajan Kumar : Issue 48183 : 24/01/2014 : Starts                        			 
        // Desc : When employee(PM or AVP) assigned or released from project then project level PM access on project should get assigned or removed.
        public const string AVP = "Asst. Vice President - AVP";
        public const string PRM = "Programme Manager-PRM";
        // Rajan Kumar : Issue 48183 : 24/01/2014: Ends  

        public const string MSG_MRFEDIT = "MRF Edited Successfully";

        public const string MSG_MRFABORT = "MRF Aborted Successfully";

        public const string MSG_APPROVAL_OF_FINANCE = "MRF is approved successfully, allocation mail is sent.";

        public const string MSG_REJECTION_OF_FINANCE = "MRF is rejected, email notification is sent to RPM.";

        public const string MSG_MRF_APPROVAL_OF_HEADCOUNT = "MRF is approved successfully, email is sent to recruitment manager to find the suitable candidate.";

        public const string MSG_MRF_APPROVAL_OF_HEADCOUNTTOCOO = "MRF is approved successfully, email notification is sent to COO.";

        public const string MSG_MRF_REJECTION_OF_HEADCOUNT = "MRF is rejected, email notification is sent to RPM.";

        public const string MSG_MRF_APPROVAL_HEADCOUNT_TXTCOMMENT = "Kindly enter data in the comment textbox.";

        public const string MSG_MRF_REJECTION_HEADCOUNT_TXTCOMMENT = "Kindly enter data in the reason for rejection textbox.";

        public const string MSG_MRF_REASSIGNED = "MRF is reassigned successfully, email notification is sent to recruiter.";

        public const string PRESALES_USA = "PreSales - USA";
        public const string PRESALES_UK = "PreSales - UK";
        public const string PRESALES_INDIA = "PreSales - India";

        public const string RAVECONSULTANT_INDIA = "RaveConsultant-India";
        public const string RAVECONSULTANT_USA = "RaveConsultant-USA";
        public const string RAVECONSULTANT_UK = "RaveConsultant-UK";
        public const string RAVECONSULTANT = "RaveConsultant";
        public const string RAVEFORCASTEDPROJECT = "RaveForecastedProjects";
        public const int RAVEFORECASTEDDEPARTMENT = 29;

        //VANDANA
        public const string RAVECONSULTANT_INDIA_ID = "22";
        public const string RAVECONSULTANT_USA_ID = "25";
        public const string RAVECONSULTANT_UK_ID = "28";

        public const string ColorCodeAmber = "Amber";
        public const string ColorCodeRed = "Red";
        public const string ColorCodeGreen = "Green";
        public const string ColorCodeWhite = "White";

        public const string RaveConsultant = "RaveConsultant";

        public const string EmployeeBandC = "Band - Contract";
        public const int EmployeePermanentID = 144;
        public const int DeptId_RaveDevelopment = 9;
        public const int DeptId_RaveForecastedProject = 29;

        // 36837-Ambar-Start-28082012
        public const int DeptId_PRESALES_USA = 14;
        public const int DeptId_PRESALES_UK = 13;
        public const int DeptId_PRESALES_INDIA = 23;
        // 36837-Ambar-End

        //Issue ID : 41292 Mahendra
        public const string Employee_Type_ASE = "ASE";
        public const string Employee_Type_Permanent = "Permanent";
        public const string Consultant = "Consultant";
        public const string Consultant_Tester = "Consultant - Tester";


        //Issue ID : 41292 Mahendra 

        /// <summary>
        /// Defines the Query string comparer.
        /// </summary>
        public const string MOVEMRF = "MoveMrf";
        public const string _isAccessUrl = "isAccessUrl";

        #region Constants For MRF QueryString

        public const string CON_QSTRING_MRFID = "MRFID";
        public const string CON_QSTRING_AllocateResourec = "AllocateResourec";

        #endregion Constants For MRF QueryString

        #region Constants For MRF Status
        /// <summary>
        /// Define parameter for MRF Status.This Status is come from database .
        /// </summary>
        public const string MRFStatus_PendingAllocation = "Pending Allocation";
        public const string MRFStatus_PedingApprovalOfHeadCount = "Pending Head Count Approval Of COO";
        public const string MRFStatus_PendingApprovalOfFinance = "Pending Approval Of Finance";
        public const string MRFStatus_PendingExternalAllocation = "Pending External Allocation";
        public const string MRFStatus_Closed = "Closed";
        public const string MRFStatus_Rejected = "Rejected";
        public const string MRFStatus_PendingExpectedResourceJoin = "Pending Expected Resource Join";
        public const string MRFStatus_PendingHeadCountApprovalOfFinance = "Pending Head Count Approval Of Finance";
        public const string MRFStatus_PendingNewEmployeeAllocation = "Pending New Employee Allocation";
        #endregion

        #endregion

        #region  Contract

        public const string CON_CONTRACTREFID = "ContractRefId";
        public const string CON_CONTRACTCODE = "ContractCode";
        public const string CON_NAME = "Name";
        public const string CON_DOCUMENTNAME = "DocumentName";
        public const string CON_FIRSTNAME = "FirstName";
        public const string CON_CONTRACTTYPE = "ContractType";

        public const string CON_PROJECTCODE = "PROJECTCODE";
        public const string CON_PROJECTNAME = "PROJECTNAME";
        public const string CON_CONTRACTTYPENAME = "Name";

        public const string CONTRACTSUMMARY_PAGE = "ContractSummary.aspx";

        public const string CONTRACT_RAVE_INTERNAL = "116";

        public const string CONTRACT_INTERNAL = "Rave Internal";

        public const string CON_MASTERID = "MasterID";
        public const string CON_MASTERNAME = "MasterName";

        public const string CONTRACTSUMMARY = "ContractSummary";
        public const string CON_QSTRING_CONTRACTID = "ContractID";

        #endregion Contract

        #region Resource Management Main Tabs

        /// <summary>
        /// Project Tab
        /// </summary>
        public const string TABPROJECT = "TABPROJECT";

        /// <summary>
        /// MRF Tab
        /// </summary>
        public const string TABMRF = "TABMRF";

        #endregion Resource Management Tabs

        #region Resource Management Pages

        #region Project Module

        /// <summary>
        /// Define ProjectSummary page
        /// </summary>
        public const string PROJECTSUMMARY_PAGE = "ProjectSummary.aspx";

        /// <summary>
        /// Define AddProject page
        /// </summary>
        public const string ADDPROJECT_PAGE = "AddProject.aspx";

        /// <summary>
        /// Define Reports page
        /// </summary>
        public const string Reports_PAGE = "Reports.aspx";

        #endregion Project Module

        #region ResourcePlan Module

        /// <summary>
        /// define CREATERP_PAGE
        /// </summary>
        public const string CREATERP_PAGE = "CreateRP.aspx";

        /// <summary>
        /// define EDITMAINRP_PAGE
        /// </summary>
        public const string EDITMAINRP_PAGE = "EditMainRP.aspx";

        //CR - 31837 Updating end date in RP Sachin
        /// <summary>
        /// define RPBULKUPDATE_PAGE
        /// </summary>
        public const string RPBULKUPDATE_PAGE = "RPBulkUpdate.aspx";
        //CR - 31837 Updating end date in RP Sachin End

        /// <summary>
        /// define EDITRPOPTIONS_PAGE
        /// </summary>
        public const string EDITRPOPTIONS_PAGE = "EditRPOptions.aspx";

        /// <summary>
        /// define APPROVEREJECTRP_PAGE
        /// </summary>
        public const string APPROVEREJECTRP_PAGE = "ApproveRejectRP.aspx";

        /// <summary>
        /// define VIEWRP_PAGE
        /// </summary>
        public const string VIEWRP_PAGE = "ViewRP.aspx";

        #endregion ResourcePlan Module

        #region MRF Module
        public const string Page_MrfApproveRejectHeadCount = "MrfApproveRejectHeadCount.aspx";
        public const string Page_MrfListOfInternalResource = "MrfListOfInternalResource.aspx";
        public const string Page_MrfPendingAllocation = "MrfPendingAllocation.aspx";
        public const string Page_MrfPendingApproval = "MrfPendingApproval.aspx";
        public const string Page_MrfRaiseHeadCount = "MrfRaiseHeadCount.aspx";
        public const string Page_MrfRaiseNext = "MrfRaiseNext.aspx";
        public const string Page_MrfRaisePrevious = "MrfRaisePrevious.aspx";
        public const string Page_MrfSummary = "MrfSummary.aspx";
        public const string Page_MrfView = "MrfView.aspx";
        #endregion

        #region Recruitment Module

        public const string Page_RecruitmentSummary = "RecruitmentSummary.aspx";
        public const string Page_RecruitmentPipelineDetails = "RecruitmentPipelineDetails.aspx";

        #endregion Recruitment Module

        #endregion Resource Management Pages

        # region Project
        /// <summary>
        /// Define Constant for query string.
        /// </summary>
        public const string CON_QSTRING_PROJECTID = "ProjectID";
        // Mohamed : Issue  : 23/09/2014 : Starts                        			  
        // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS

        //Project Divisions
        public const string Project_Division_PublicService = "1344";
        public const string Project_Division_Arinso = "1345";

        //Project Bussiness Area
        public const string Project_BussinessArea_Solutions = "1346";
        public const string Project_BussinessArea_Services = "1347";

        //Siddharth 12th March 2015  Project Model Drop Down --Start
        public const string Project_Model = "";
        //Siddharth 12th March 2015  Project Model Drop Down --End

        // Mohamed : Issue  : 23/09/2014 : Ends

        #endregion Project

        #region Recruitment Summary

        public const string RECRUITMENT_MANAGER = "RecruitmentManager";
        public const string EXPECTED_DATE_OF_JOINING = "ExpectedDateOfJoining";
        public const string CON_QSTRING_CANDIDATEID = "CandidateID";
        public const string RECRUITMENTASSIGNDATE = "RecruitmentAssignDate";

        #endregion Recruitment Summary

        #region Constants For Employee

        public const string EMP_JOINING_DATE = "JoiningDate";
        public const string EMP_ID = "EMPId";
        public const string EMP_CODE = "EMPCode";
        public const string EMP_FIRSTNAME = "FirstName";
        public const string EMP_NAME = "Name";
        public const string EMP_PROJECTNAME = "ProjectName";
        public const string EMP_DESIGNATION = "Designation";
        public const string EMP_DEPTNAME = "DeptName";
        public const string EMP_STARTDATE = "StartDate";
        public const string EMP_BILLING = "Billing";
        public const string EMP_UTILIZATION = "Utilization";
        public const string EMP_ENDDATE = "EndDate";
        public const string EMP_CLIENTNAME = "ClientName";
        public const string EMP_REPORTINGTO = "ReportingToId";
        public const string EMP_ROLE = "Role";

        public const string OUTOF_LESSTHAN_GPA = "Out of should be greater than GPA entered.";
        public const string YEAROF_PASSING_VALIDATION = "Year of Passing should not be greater than Current Year.";
        // 26416-Ambar-Start
        // Added following Validation
        public const string YEAROF_PASSING_FORMAT_VALIDATION = "Year of Passing should be in YYYY format.";
        // Edited following Validation
        public const string PERCENTAGE_VALIDATION = "Percentage should be lesser or equal to 100 and should not be equal to 0.";
        // 26416-Ambar-End
        public const string RELEVENTEXPERIENCE_VALIDATION = "Experience(in Relevant Experience tab)should be lesser or equal to Relevent Experience.";
        public const string RELEVENTEXPERIENCE_GRID_VALIDATION = "Experience(in Relevent Experience grid)should not be exceeded than Relevant Experience.";
        public const string NONRELEVENTEXPERIENCE_VALIDATION = "Experience(in NonRelevant Experience tab)should be lesser or equal to NonRelevant Experience.";
        public const string NONRELEVENTEXPERIENCE_GRID_VALIDATION = "Experience(in NonRelevant Experience grid)should not be exceeded than NonRelevant Experience.";
        //Mahendra Temp Fix 28109 STRAT
        public const string EXPERIENCE_EQUAL_RELEVENTEXPERIENCE = "Experience total(in Relevant Experience grid) should be lesser  or equal to Relevant Experience.";
        //Mahendra Temp Fix 28109 STRAT
        public const string EXPERIENCE_EQUAL_NONRELEVENTEXPERIENCE = "Experience total(in NonRelevant Experience grid) should be equal to NonRelevant Experience.";
        public const string TOTALEXPERIENCE_VALIDATION = "Total experience should be lesser or equal to Relevant experience.";
        public const string RELEVENT_VALIDATION = "Relevant experience should be lesser or equal to total experience.";
        public const string OUTOF_LESSTHAN_SCORE = "Out of should be greater than score entered.";
        public const string OUTOF_LESSTHAN_TOTALSCORE = "Out of should be greater than total score entered.";
        public const string CERTIFICATION_DATE_VALIDATION = "Certification Date should not be future date.";
        public const string CERTIFICATION_VALIDDATE_VALIDATION = "Certification valid date should be greater than today's date.";
        public const string LAST_USED_DATE_VALIDATION = "Last Used Date should not be future date.";
        public const string OTHERDETAILS_VALIDATION = "Please provide a reason.";
        public const string SELECT_DURATION = "Please select the duration.";
        public const string EXPIRYDATE_LESSTHAN_DATEOFISSUE = "Expiry date should be greater than date of issue.";
        public const string DATEOFISSUE_VALIDATION = "Date of issue should not be future date.";
        public const string VISAEXPIRYDATE_LESSTHAN_CURRENTDATE = "Visa expiry date should be greater than current date.";
        public const string EXPIRYDATE_LESSTHAN_CURRENTDATE = "Expiry date should be greater than current date.";
        public const string REL_WORKINGSINCE_VALIDATION = "Working Since(in Relevent Experience tab) should not be future Month.";
        public const string REL_WORKINGTILL_VALIDATION = "Working Till(in Relevent Experience tab) should not be future Month.";
        public const string NONREL_WORKINGSINCE_VALIDATION = "Working Since(in NonRelevent Experience tab) should not be future Month.";
        public const string NONREL_WORKINGTILL_VALIDATION = "Working Till(in NonRelevent Experience tab) should not be future Month.";
        public const string RELWORKINGTILL_LESSTHAN_WORKINGSINCE = "Working Till should be greater than Working Since((in Relevent Experience tab).";
        public const string NONRELWORKINGTILL_LESSTHAN_WORKINGSINCE = "Working Till should be greater than Working Since((in NonRelevent Experience tab).";
        public const string JOINING_DATE_VALIDATION = "Joining Date should not be future date.";
        public const string RESIGNATION_DATE_VALIDATION = "Resignation Date should not be future date.";
        public const string LASTWORKING_DAY_VALIDATION = "Last Working Day should not be future date.";
        public const string BIRTH_DATE_VALIDATION = "Date of Birth should not be future date.";
        public const string SPACES_VALIDATION = "Only spaces are not allowed.";
        public const string GPAOUTOF_VALIDATION = "Please enter GPA or Out Of.";
        public const string GPA_VALIDATION = "Please enter Out Of for GPA entered.";
        public const string PHONEMOBILE_VALIDATION = "Please enter Residential Phone OR Mobile number.";
        public const string RELEASEDATE_VALIDATION = "Release Date should be equal or less than Project EndDate.";
        public const string OUT_OF_SCORE_VALIDATION = "When Score is entered Out Of is mandatory.";
        public const string OUT_OF_NUMERIC_VALIDATION = "If Score is numeric, Out Of should be in Numeric.";
        public const string SCORE_NUMERIC_VALIDATION = "If Out Of is numeric, Score should be in Numeric.";
        public const string CONTRACT_CURRENCY_VALIDATION = "currency is manadatory,if Contract Value is added";
        public const string GPAPercentage_VALIDATION = "Please enter Percentage OR GPA.";
        public const string UNIVERSITYINSTITUTE_VALIDATION = "Please enter University/Board Name OR Institute Name.";

        public const string PROJECTS = "Projects";
        public const string BTN_Save = "Save";
        public const string BTN_AddRow = "Add Row";

        #region PageNameConstants

        public const string PAGE_PASSPORTDETAILS = "EmpPassportDetails.aspx";
        public const string PAGE_QUALIFICATIONDETAILS = "EmpQualificationDetails.aspx";
        public const string PAGE_PROFESSIONALETAILS = "EmpProfessionalCourses.aspx";
        public const string PAGE_CERTIFICATIONDETAILS = "EmpCertification.aspx";
        public const string PAGE_ORGANIZATIONDETAILS = "EmpOrganization.aspx";
        public const string PAGE_SKILLSDETAILS = "EmpSkillsDetails.aspx";
        public const string PAGE_PROJECTDETAILS = "EmpProjectDetails.aspx";
        public const string PAGE_OTHERDETAILS = "EmpOtherDetails.aspx";
        public const string PAGE_GENERATERESUME = "GenerateResume.aspx";
        public const string PAGE_EMPLOYEE = "Employee.aspx";
        public const string PAGE_HOME = "Home.aspx";
        public const string PAGE_EMPLOYEESUMMARY = "EmployeeSummary.aspx";
        public const string PAGE_EMPLOYEEDETAILS = "EmployeeDetails.aspx";

        #endregion PageNameConstants

        #endregion Constants For Employee

        #region Constants for Resource Plan

        #region Resource Plan Status

        public const string RPStatus_SaveCreatedResourcePlan = "Resource plan is successfully added, email is sent to COO for approval";
        public const string RPStatus_SaveApprovedResourcePlan = "Resource plan for project <<Project name>> is approved successfully.";
        public const string RPStatus_SaveRejectedResourcePlan = "Resource Plan for project <<Project Name>> is rejected. Email notification is sent.";
        public const string RPStatus_SaveEditedResourcePlan = "Resource plan for <<Project>> is successfully updated.";
        public const string RPStatus_DeleteResourcePlan = "Resource plan for project <<Project Name>> has been successfully deleted.";

        #endregion Resource Plan Status

        #region Error Message Constants for Resource Plan

        public const string RPErrorMsg_AddResourcePlanDuration = "Duration dates can not be repeated";

        #endregion Error Message Constants for Resource Plan

        #endregion Constants for Resource Plan

        #region Constants for Project

        #region Error Message Constants for Project

        public const string ProjectErrorMsg_SaveProject = "Resource is already allocated to this project so project cannot closed.";

        public const string ProjectErrorMsg_ResourcePlanIsActive = "Resource Plan is active for this project so it cannot closed.";

        #endregion Error Message Constants for Project

        #endregion Constants for Project

        #region Constant for Seat Allocation

        public const string SeatAln_AllocateSeatID = "AllocateSeatID";
        public const string SeatAln_BranchID = "BranchID";
        public const string SeatAln_SectionID = "SectionID";
        public const string SEATALLOCATION_PAGE = "SeatAllocation.aspx";

        #endregion Constant for Seat Allocation

        //Umesh: NIS-changes: Head Count Report Starts
        #region Constants for Head Count

        public const string DIVISION = "Division";
        //Umesh: NIS-changes: Head Count Report Ends

        #endregion

        #region Regex Constants

        public const string INTEGER_NUMBER_REGEX = @"^[0-9]*$";
        public const string ALPHABET_WITHSPACE = "^[a-zA-Z ]+$";
        public const string ALPHABET_WITHPLUS = "^[a-zA-Z]$"; // added on 5 april 2010 -vandana
        public const string ALPHANUMERIC_WITHSPACE = @"^[0-9a-zA-Z\.\,\/\:\[\]\(\)\-\&\'\\\r\n\ ]+$";
        public const string INTEGER_NUMBER_WITHSPECIALCHARS = @"^[0-9\[\]\(\)\-\+\\\r\n\ ]+$";
        public const string DECIMAL_NUMBER_REGEX = "^([0-9][0-9].[0-9][0-9])$";//26864-Ambar-01072011

        #endregion

        //Mohamed : Issue 40339 : 26/03/2013 : Starts                        			  
        //Desc :  Remove "Bengaluru,India" from project location drop down in Edit resource plan

        #region Project Location Constants
        public const string BENGALURU_INDIA = "825";
        #endregion

        //Mohamed : Issue 40339 : 26/03/2013 : Ends
        //Mohamed : Issue 40242 : 26/03/2013 : Starts
        //Desc :  When the Resource is allocated then in mail its Functional manager should be in CC

        #region Project Location Constants
        public const int BA = 16;
        public const int USABILITY = 17;
        #endregion

        //Mohamed : Issue 40242 : 26/03/2013 : Ends

        //Ishwar Patil Training Module : 23/03/2013 : Starts
        #region Training Module
        public const string TrainingTypeID = "89";
        public const string TrainingType = "Training Type";
        public const string TrainingName = "Training Name";
        public const string TrainingCategory = "Training Category";
        public const string TrainingQuarter = "Training Quarter";
        public const string TrainingStatus = "Training Status";
        public const string TrainingParticipants = "Training Participants";
        public const string KSSType = "KSSTraining Type";
        public const string SoftSkills = "Soft-Skills Training";
        public const string TrainingCreatedDate = "CreatedDate";
        public const string TrainingTypeOther = "others";
        public const string Update = "update";
        public const string Submit = "submit";
        public const string View = "view";
        public const string Back = "back";
        public const string SummaryPage = "SP";
        public const string ApproveRejectSummaryPage = "ARSP";
        public const string Cancel = "Cancel";
        public const string Approved = "Approved";
        public const string Rejected = "Rejected";
        public const string Deleted = "Deleted";
        public const string Closed = "Closed";
        public const string Open = "Open";
        public const string ImagePlus = "~/Images/plus.JPG";
        public const string ImageMinus = "~/Images/minus.JPG";
        public const string Reasonforrejection = "Reason for rejection :";
        public const int DefaultRaiseID = 0;
        public const int DefaultFlagZero = 0;
        public const int DefaultFlagOne = 1;
        public const int DefaultFlagMinus = -1;
        public const int TechnicalTrainingID = 1207;
        public const int SoftSkillsTrainingID = 1208;
        public const int KSSID = 1210;
        public const int SeminarsID = 1209;
        public const int ApproveRejectID = 288;
        public const int TrainingStatusOpen = 1232;
        public const string BaseUrl= "BaseUrl";
        public const string CheckAccessAttribute = "CheckAccessAttribute";
        public const string OnResultExecuting = "OnResultExecuting";
        public const string Seminar = "seminar";
        public const string Technical = "technical";
        public const string SoftSkill = "soft skill";
        public const string KSS = "knowledge sharing session";
        public const string ViewTechnicalTrainingRequestGrid = "ViewTechnicalTrainingRequestGrid";
        public const string StringFormat = "{0} {1}";
        public const string Request_Closed = "Request Closed";
        public const string Result = "Result";        
        public const string Request_Deleted = "Request Deleted";
        public const string partialView_viewTrainingGrid = "_viewTrainingGrid";
        public const string viewtrainingrequest = "viewtrainingrequest";
        public const string EmailConfigUrl = "EmailConfigUrl"; 
        //Ishwar Patil Training Module : 19/08/2013 : Start
        public const string RMGroupName = "RMGroup";
        //Ishwar Patil Training Module : 19/08/2013 : End
        //public const int TrainingStatusDeleted = 1241;
        //public const int ApproveRejectID = 1415; 

        #endregion
        //Ishwar Patil Training Module : 23/03/2013 : End

        #region
        public const string BENCH_PROJECT = "BENCH";
        public const string BENCH_DEPARTMENT = "--NA--";
        public const string BENCH_VALUE = "9999";
        public const string SALES_DEPARTMENT = "Sales";
        public const string Project_Mentee2010_DEPARTMENT = "Project Mentee -2010";
        public const string Senior_Mgt_DEPARTMENT = "Senior Mgt";



        //*************** 4C Roles *****************

        //@Role1 = CREATORFORPROJECT
        //@Role2 = CREATORFORDEPARTMENT
        //@Role3 = REVIEWERFORPROJECT
        //@Role4 = REVIEWERFORDEPARTMENT
        //@Role5 = REVIEWERFORPROJECT_REVIEWERFORDEPARTMENT 		Reviewer  for project + Reviewer for Department (only reviewer)
        //@Role6 = CREATORFORPROJECT_REVIEWERFORPROJECT (CREATORFORPROJECT + REVIEWERFORPROJECT) // for Same project
        //@Role7 = CREATORFORDEPARTMENT_REVIEWERFORDEPARTMENT (CREATORFORDEPARTMENT + REVIEWERFORDEPARTMENT)  == FUNCTIONAL MANAGER
        //@Role8 = FUNCTIONALMANAGER_REVIEWERFORPROJECT (FUNCTIONAL MANAGER + REVIEWERFORPROJECT)
        //@Role9 = FUNCTIONALMANAGER_CREATORFORPROJECT(P1)_REVIEWERFORPROJECT(P2) (FUNCTIONAL MANAGER + REVIEWERFORPROJECT)
        //@Role10 = FUNCTIONALMANAGER_REVIEWERFORPROJECT_REVIEWERFORDEPARTMENT  // creator and reviewer for same proejct
        //@Role11 = FUNCTIONALMANAGER_CREATORFORPROJECT(P1)_REVIEWERFORPROJECT(P2)
        //@Role12 = FUNCTIONALMANAGER_CREATORFORPROJECT(P1)_REVIEWERFORPROJECT(P1)
        //@Role13 = FUNCTIONALMANAGER_CREATORFORPROJECT(P1)_REVIEWERFORPROJECT(P2)_CREATORFORPROJECT(P3)_REVIEWERFORPROJECT(P3)
        //@Role14 = FOURCADMIN
        //@Role15 = FOURCADMIN_REVIEWERFORPROJECT
        //@Role16 = FOURCADMIN_REVIEWERFORDEPARTMENT

        public const string FOURCROLE1 = "FOURCROLE1";
        public const string FOURCROLE2 = "FOURCROLE2";
        public const string FOURCROLE3 = "FOURCROLE3";
        public const string FOURCROLE4 = "FOURCROLE4";
        public const string FOURCROLE5 = "FOURCROLE5";
        public const string FOURCROLE6 = "FOURCROLE6";
        public const string FOURCROLE7 = "FOURCROLE7";
        public const string FOURCROLE8 = "FOURCROLE8";
        public const string FOURCROLE9 = "FOURCROLE9";
        public const string FOURCROLE10 = "FOURCROLE10";
        public const string FOURCROLE11 = "FOURCROLE11";
        public const string FOURCROLE12 = "FOURCROLE12";
        public const string FOURCROLE13 = "FOURCROLE13";
        public const string FOURCROLE14 = "FOURCROLE14";
        public const string FOURCROLE15 = "FOURCROLE15";
        public const string FOURCROLE16 = "FOURCROLE16";


        public const string FOURCTypeADD = "ADD";
        public const string FOURCTypeReview = "REVIEW";


        #endregion




    }
}
