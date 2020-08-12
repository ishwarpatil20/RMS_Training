///------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           SessionNames.cs       
//  Author:         Gaurav Thakkar
//  Date written:   15/07/2009/ 1:00:00 PM
//  Description:    This class contains session name constants
//                  
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  15/07/2009 1:00:00 PM  Gaurav Thakkar    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class SessionNames
    {
        #region Session Names in Approve/Reject Project Page

        public const string CONFIRMATION_MESSAGE = "ConfirmationMessage";
        public const string EMAIL_MESSAGE = "EmailMessage";
        public const string PREVIOUS_SORT_EXPRESSION_AR = "PreviousSortExpressionAR";
        public const string PROJECT_ID = "ProjectId";
        public const string PROJECT_ID_FILTER = "ProjectIdFilter";
        public const string PROJECT_NAME = "ProjectName";
        public const string PROJECT_NAME_FILTER = "ProjectNameFilter";
        public const string SORT_DIRECTION_AR = "sortDirectionAR";
        public const string IS_FILTERED = "IsFiltered";

        #endregion

        #region Session Names in Project Summary Page

        /// <summary>
        /// Defines the PREVIOUS_SORT_EXPRESSION
        /// </summary>
        public const string PREVIOUS_SORT_EXPRESSION = "PreviousSortExpression";

        /// <summary>
        /// Defines the PREVIOUS_SORT_EXPRESSION
        /// </summary>
        public const string PROJECT_SUMMARY_PROJECT_NAME = "ProjectSummaryProjectName";

        /// <summary>
        /// Defines the CLIENT_NAME
        /// </summary>
        public const string CLIENT_NAME = "ClientName";

        /// <summary>
        /// Defines the PROJECT_STATUS
        /// </summary>
        public const string PROJECT_STATUS = "ProjectStatus";

        /// <summary>
        /// Defines the WORK_FLOW_STATUS
        /// </summary>
        public const string WORK_FLOW_STATUS = "WorkFlowStatus";

        /// <summary>
        /// Defines the SORT_DIRECTION
        /// </summary>
        public const string SORT_DIRECTION = "sortDirection";

        /// <summary>
        /// Defines the AZMAN_ROLES
        /// </summary>
        public const string AZMAN_ROLES = "azmanRoles";

        /// <summary>
        /// Defines the current page index i.e. the current page no. for Project Summary
        /// </summary>
        public const string CURRENT_PAGE_INDEX_PROJECT = "CurrentPageIndexProject";

        ///// <summary>
        ///// Defines the current page index i.e. the current page no. for Project Summary
        ///// </summary>
        //public const string CURRENT_PAGE_INDEX_RP = "CurrentPageIndexRP";

        /// <summary>
        /// Defines PROJECTSUMMARY_PROJECT_ID
        /// </summary>
        public const string PROJECTSUMMARY_PROJECT_ID = "PROJECTSUMMARY_PROJECT_ID";

        #endregion

        #region Session For Contract

        public const string PAGENUMBER = "PageNumber";

        public const string FILERCLICKED = "FilerClicked";

        public const string STATUS = "Status";

        public const string CONTRACT_PROJECT_DATA = "ProductData";

        public const string CONTRACT_LISTOFPROJECT_DATA = "ListOfProductData";

        public const string CONTRACT_SORT_DIRECTION = "ContractsortDirection";

        public const string CONTRACT_DOCUMENTNAME = "ContractDocumentName";

        public const string CONTRACT_CONTRACTTYPE = "ContractContractType";

        public const string CONTRACT_CONTRACTSTATUS = "ContractContractStatus";

        public const string CONTRACT_CURRENTPAGEINDEX = "ContractcurrentPageIndex";

        public const string CONTRACT_SORTDIRECTIONFORBINDING = "ContractSortDirectionForBinding";

        public const string CONTRACT_ACTUALPAGECOUNT = "ContractActualPageCount";

        public const string CONTRACT_PREVIOUS_SORT_EXPRESSION = "ContractPreviousSortExpression";

        public const string CONTRACT_EDIT_PROJECT_DETAILS = "EditProjectDetails";

        public const string CONTRACT_DISASSOCIATED_PROJECT = "DisassociatedProject";

        public const string CONTRACT_GRIDDATA = "GridData";

        public const string CONTRACT_SELECETED_PROJECT = "ContractSelectedProject";

        public const string CONTRACT_CONTRACTIDHASHTABLE = "ContractContractIdHashtable";
        //27633 Subhra Start
        /// <summary>
        /// To store the entire collection in this HashTable
        /// </summary>
        public const string CONTRACTPREVIOUSHASHTABLE = "CONTRACTPREVIOUSHASHTABLE";
        //End


        public const string CONTRACT_CLIENTNAME = "ContractClientName";

        #endregion

        #region Session Names in MRF Summary Page

        /// <summary>
        /// Defines the Sort Direction for MRF Summary
        /// </summary>
        public const string SORT_DIRECTION_MRF = "sortDirectionMrf";

        /// <summary>
        /// Defines the Sort Direction for Approve/Reject MRF 
        /// </summary>
        public const string SORT_DIRECTION_APPREJMRF = "sortDirectionAppRejMrf";

        /// <summary>
        /// Defines the Project Id
        /// </summary>
        public const string PROJECT_ID_MRF = "ProjectId";

        /// <summary>
        ///  Defines the Mrf Code
        /// </summary>
        public const string MRF_CODE = "MrfCode";

        /// <summary>
        ///  Defines the Role
        /// </summary>
        public const string ROLE = "Role";

        /// <summary>
        /// Defines the Mrf Status
        /// </summary>
        public const string MRF_STATUS_ID = "MrfStatus";

        /// <summary>
        /// Defines the Mrf Status Name
        /// </summary>
        public const string MRF_STATUS_NAME = "MrfStatusName";

        /// <summary>
        /// Defines the Department Id
        /// </summary>
        public const string DEPARTMENT_ID = "DepartmentId";

        /// <summary>
        /// Defines the Page Count of the pages for MRF Summary
        /// </summary>
        public const string PAGE_COUNT_MRF = "PageCountMrf";

        /// <summary>
        /// Defines the Page Count of the pages for Approve/Reject MRF
        /// </summary>
        public const string PAGE_COUNT_APPREJMRF = "PageCountAppRejMrf";


        /// <summary>
        /// Defines the current page index i.e. the current page no. for MRF Summary
        /// </summary>
        public const string CURRENT_PAGE_INDEX_MRF = "CurrentPageIndexMrf";

        /// <summary>
        /// Defines the current page index i.e. the current page no. for Approve/Reject MRF
        /// </summary>
        public const string CURRENT_PAGE_INDEX_APPREJMRF = "CurrentPageIndexAppRejMrf";

        /// <summary>
        /// Defines the previous sort expression for MRF Summary
        /// </summary>
        public const string PREVIOUS_SORT_EXPRESSION_MRF = "PreviousSortExpressionMrf";

        /// <summary>
        /// Defines the previous sort expression for Approve/Reject MRF
        /// </summary>
        public const string PREVIOUS_SORT_EXPRESSION_APPREJMRF = "PreviousSortExpressionAppRejMrf";

        #endregion Session Names in MRF Summary Page


        #region Session Names For Pending Allocation Page

        /// <summary>
        /// Defines the current page index i.e. the current page no. for Pending Allocation
        /// </summary>
        public const string CURRENT_PAGE_INDEX_PENDING_ALLOCATION = "CurrentPageIndexPendingAllocation";

        #endregion Session Names For Pending Allocation Page

        #region Session Names For MRf
        /// <summary>
        /// Define parameter mrfDetail for business entities of MRF Detail 
        /// </summary>
        public const string MRFDetail = "MRFDetail";

        /// <summary>
        /// Define parameter mrfDetail for business entities of Move MRF Detail 
        /// </summary>
        public const string MoveMRFDetail = "MoveMRFDetail";

        /// <summary>
        /// Constants will contain the Collection of MRF Prject Details
        /// </summary>
        public const string MRFPROJECTDETAIL_COLLECTION = "MRFProjectDetailCollection";

        /// <summary>
        /// Constants will contain the MRF Project Detail.
        /// </summary>
        public const string MRFPROJECTDETAIL = "MRFProjectDetail";

        /// <summary>
        /// Constants will contain the MRFGRIDFILL.
        /// </summary>
        public const string MRFGRIDFILL = "MRFGridFill";

        /// <summary>
        /// Constants will contain the MRFPREVIOUSVALUE.
        /// </summary>
        public const string MRFPREVIOUSVALUE = "MRFPREVIOUSVALUE";

        /// <summary>
        /// Constant will contain MRFDetail business entity object
        /// </summary>
        public const string MRFDETAIL_APPREJMRF = "MrfDetailAppRej";

        /// <summary>
        /// Constant will contain MRFDetail business entity object for Copy MRF
        /// </summary>
        public const string MRFCOPY = "MRFCOPY";

        /// <summary>
        /// Constant will contain MRFDetail business entity object for Copy MRF
        /// </summary>
        public const string InternalResource = "InternalResource";



        /// <summary>
        /// 
        /// </summary>
        public const string MRFVIEWINDEX = "MRFVIEWINDEX";

        //27633-Subhra-Start-12072011
        /// <summary>
        /// To contain the object of temporary Hashtable
        /// </summary>       
        public const string MRFPreviousHashTable = "MRFPREVIOUSHASHVIEWINDEX";
        //27633-Subhra-End

        /// <summary>
        /// Will store the confirmation Message
        /// </summary>
        public const string MRF_CONFIRMATION_MESSAGE = "ConfirmationMessage";

        /// <summary>
        /// Will store the information True if "RaiseHeadCount" page was visited
        /// </summary>
        public const string RAISE_HEAD_COUNT = "RaiseHeadCount";


        /// <summary>
        /// Will store the information abot Allocation date
        /// </summary>
        public const string Allocation_Date = "AllocationDate";


        /// <summary>
        /// Will store the all mrf id those have to raise MRF.
        /// </summary>
        public const string MRFRaiseHeadCOuntGroup = "MRFRaiseHeadCOuntGroup";


        /// <summary>
        /// Will store the all mrf id those have to raise MRF.
        /// </summary>
        public const string MoveMRFDetails = "MoveMRFDetails";

        #endregion

        #region Session Name For Resource Plan

        /// <summary>
        /// Define session variable PageCount
        /// </summary>
        public const string PageCount = "pagecount";

        /// <summary>
        /// Define session variable SaveEditedRP
        /// </summary>
        public const string SaveEditedRP = "SaveEditedRP";


        #endregion Session Name For Resource Plan

        #region Session Name For Employee

        /// <summary>
        /// Define session variable EmployeeDetails
        /// </summary>
        public const string EMPLOYEEDETAILS = "EmployeeDetails";

        /// <summary>
        /// Define session variable EmployeeDetailsCollection
        /// </summary>
        public const string EMPLOYEEDETAILSCOLLECTION = "EmployeeDetailsCollection";

        /// <summary>
        /// Define session variable EMPLOYEEID
        /// </summary>
        public const string EMPLOYEEID = "EMPLOYEEID";

        /// <summary>
        /// Define session variable EMP_ROLE
        /// </summary>
        public const string EMP_ROLE = "EMP_ROLE";

        /// <summary>
        /// Define session variable EMP_DEPARTMENTID
        /// </summary>
        public const string EMP_DEPARTMENTID = "EMP_DEPARTMENTID";

        /// <summary>
        /// Define session variable EMP_PROJECTID
        /// </summary>
        public const string EMP_PROJECTID = "EMP_PROJECTID";

        /// <summary>
        /// Define session variable EMP_ROLEID
        /// </summary>
        public const string EMP_ROLEID = "EMP_ROLEID";

        /// <summary>
        /// Define session variable EMP_STATUSID
        /// </summary>
        public const string EMP_STATUSID = "EMP_STATUSID";

        /// <summary>
        /// Defines the Page Count of the pages for EMP Summary
        /// </summary>
        public const string PAGE_COUNT_EMP = "PageCountEMP";

        /// <summary>
        /// Defines the Page Count of the pages for Approve/Reject EMP
        /// </summary>
        public const string PAGE_COUNT_APPREJEMP = "PageCountAppRejEMP";

        /// <summary>
        /// Defines the current page index i.e. the current page no. for EMP Summary
        /// </summary>
        public const string CURRENT_PAGE_INDEX_EMP = "CurrentPageIndexEMP";

        /// <summary>
        /// Defines the current page size i.e. the current page size for EMP Summary
        /// </summary>
        public const string CURRENT_PAGE_SIZE_EMP = "CurrentPageSizeEMP";

        /// <summary>
        /// Defines the current page index i.e. the current page no. for Approve/Reject EMP
        /// </summary>
        public const string CURRENT_PAGE_INDEX_APPREJEMP = "CurrentPageIndexAppRejEMP";

        /// <summary>
        /// Defines the previous sort expression for EMP Summary
        /// </summary>
        public const string PREVIOUS_SORT_EXPRESSION_EMP = "PreviousSortExpressionEMP";

        /// <summary>
        /// Defines the previous sort expression for Approve/Reject EMP
        /// </summary>
        public const string PREVIOUS_SORT_EXPRESSION_APPREJEMP = "PreviousSortExpressionAppRejEMP";

        /// <summary>
        /// Defines the Sort Direction for MRF Summary
        /// </summary>
        public const string SORT_DIRECTION_EMP = "sortDirectionEMP";

        /// <summary>
        /// Defines the Sort Direction for Approve/Reject MRF 
        /// </summary>
        public const string SORT_DIRECTION_APPREJEMP = "sortDirectionAppRejEMP";

        /// <summary>
        /// 
        /// </summary>
        public const string EMPLOYEEVIEWINDEX = "EMPLOYEEVIEWINDEX";

        //27633 Subhra Start
        /// <summary>
        /// To store the entire collection in this HashTable
        /// </summary>
        public const string EMPPREVIOUSHASHTABLE = "EMPPREVIOUSHASHTABLE";
        //End

        /// <summary>
        /// Define the string which is used to Refresh page when a employee is released.
        /// </summary>
        public const string REFRESHPAGE = "REFRESHPAGE";

        public const string PAGEMODE = "PageMode";

        public const string DefaultRow = "DefaultRow";

        public const string EMP_EXPERIENCE = "EmpExperience";

        public const string PREVIOUS_EMP = "PreviousEmp";

        /// <summary>
        /// Define session variable EMP_NAME
        /// </summary>
        public const string EMP_NAME = "EMP_NAME";

        #endregion Session Name For Employee

        #region Session Names For Recruitment Summary

        public const string SORT_DIRECTION_RS = "sortDirectionRS";

        public const string CURRENT_PAGE_INDEX_RS = "CurrentPageIndexRS";

        public const string PREVIOUS_SORT_EXPRESSION_RS = "PreviousSortExpressionRS";

        public const string PROJECT_ID_RS = "ProjectIdRS";

        public const string DEPARTMENT_ID_RS = "DepartmentIdRS";

        public const string ROLE_RS = "RoleRS";

        public const string PAGE_COUNT_RS = "PageCountRS";

        public const string RECRUITMENTVIEWINDEX = "RecruitmentViewIndex";
        //27633-Subhra-Start-12072011
        /// <summary>
        /// To contain the object of temporary Hashtable
        /// </summary>       
        public const string RECRUITMENTPREVIOUSHASHTABLE = "RECRUITMENTPREVIOUSHASHVIEWINDEX";
        //27633-Subhra-End

        public const string CONFIRMATION_MESSAGE_CANDIDATE_ADDED = "ConfirmationMessageCandidateAdded";

        public const string CONFIRMATION_MESSAGE_CANDIDATE_DELETED = "ConfirmationMessageCandidateDeleted";

        public const string CONFIRMATION_MESSAGE_CANDIDATE_UPDATED = "ConfirmationMessageCandidateUpdated";

        public const string CONFIRMATION_MESSAGE_CANDIDATE_JOINED = "ConfirmationMessageCandidateJoined";

        public const string RECRUITMENT = "Recruitment";

        public const string RECRUITMENTMANAGERID = "RecruitmentManagerID";

        #endregion Session Names For Recruitment Summary

        #region Session Names For Seat Allocation

        public const string SEAT_SEATID = "SeatId";

        #endregion Session Names For Seat Allocation

        //Aarohi : Issue 30885 : 14/12/2011 : Start
        //Variable to contain the Purpose value
        public const string PURPOSE = "Purpose";
        //Aarohi : Issue 30885 : 14/12/2011 : End

        //Aarohi : Issue 31826 : 16/12/2011 : Start
        public const string RESOURCE_JOINED = "ResourceJoined";
        //Aarohi : Issue 31826 : 16/12/2011 : End

        //Aarohi : Issue 28735(CR) : 26/12/2011 : Start
        public const string DEPARTMENT_NAME = "DepartmentName";
        //Aarohi : Issue 28735(CR) : 26/12/2011 : End

        //Aarohi : Issue 31838(CR) : 28/12/2011 : Start
        public const string RP_ID = "RPId";
        //Aarohi : Issue 31838(CR) : 28/12/2011 : End

        //Aarohi : Issue 28572(CR) : 05/01/2012 : Start
        public const string REPORTING_IDS = "ReportingIds";
        public const string FUNCTIONAL_MANAGER_IDS = "FunctionalManagerIds";

        public const string SORT_DIRECTION_UPManger = "sortDirectionUPManger";
        public const string CURRENT_PAGE_INDEX_UPManger = "CurrentPageIndexUPManger";
        public const string PREVIOUS_SORT_EXPRESSION_UPManger = "PreviousSortExpressionUPManger";

        //Aarohi : Issue 28572(CR) : 05/01/2012 : End

        #region "4C Session Data"
        public const string FOURC_ACTION_DATA = "ActionData";
        public const string FOURC_ACTION_DeletedData = "ActionDataDeleted";
        #endregion "4C Session Data"

        //Ishwar Patil : Trainging Module 16/04/2014 : Starts
        #region Session Names For Training Module

        public const string CURRENT_PAGE_INDEX_TRAINING = "CurrentPageIndexTraining";
        public const string PAGE_COUNT_TRAINING = "PageCountTraining";

        public const string CURRENT_PAGE_INDEX_SoftSkillTRAINING = "CurrentPageIndexSoftSkillTraining";
        public const string PAGE_COUNT_SoftSkillTRAINING = "SoftSkillPageCountTraining";

        public const string CURRENT_PAGE_INDEX_SeminarTRAINING = "SeminarCurrentPageIndexTraining";
        public const string PAGE_COUNT_SeminarTRAINING = "SeminarPageCountTraining";

        public const string CURRENT_PAGE_INDEX_KSSTRAINING = "KSSCurrentPageIndexTraining";
        public const string PAGE_COUNT_KSSTRAINING = "KSSPageCountTraining";

        public const string CURRENT_PAGE_INDEX_SOFTTRAINING = "SoftCurrentPageIndexTraining";
        public const string PAGE_COUNT_SOFTTRAINING = "SoftPageCountTraining";

        public const string CURRENT_PAGE_INDEX_APPREJTRAINING = "AppRejCurrentPageIndexTraining";
        public const string CURRENT_PAGE_INDEX_APPREJKSSTRAINING = "AppRejKSSCurrentPageIndexTraining";
        public const string CURRENT_PAGE_INDEX_APPREJSeminarTRAINING = "AppRejSeminarCurrentPageIndexTraining";

        #endregion Session Names For Training Module
        //Ishwar Patil : Trainging Module 16/04/2014 : End

        //Umesh: NIS-changes: Head Count Report Starts
        #region Session Names for Head Count Report
        /// <summary>
        /// Defines the Sort Direction for Head Count Report
        /// </summary>
        public const string SORT_DIRECTION_HEADCOUNT = "sortDirectionHeadCount";

        /// <summary>
        /// Defines the current page index i.e. the current page no. for Head Count Report
        /// </summary>
        public const string CURRENT_PAGE_INDEX_HEADCOUNT = "CurrentPageIndexHeadCount";

        /// <summary>
        /// Defines the previous sort expression for Head Count Report
        /// </summary>
        public const string PREVIOUS_SORT_EXPRESSION_HEADCOUNT = "PreviousSortExpressionHeadCount";

        /// <summary>
        /// Defines the Page Count of the pages for Head Count Report
        /// </summary>
        public const string PAGE_COUNT_HEADCOUNT = "PageCountHeadCount";

        /// <summary>
        /// To store the entire collection in this HashTable
        /// </summary>
        public const string HEADCOUNTPREVIOUSHASHTABLE = "HEADCOUNTPREVIOUSHASHTABLE";

        #endregion 
        //Umesh: NIS-changes: Head Count Report Ends
    }
}
