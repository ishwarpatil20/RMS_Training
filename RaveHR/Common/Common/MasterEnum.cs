///------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           Enums.cs       
//  Author:         Gaurav Thakkar
//  Date written:   11/05/2009/ 8:10:19 PM
//  Description:    This class provides enums for add project
//                  
//
//  Amendments
//  Date                   Who             Ref     Description
//  ----                   -----------     ---     -----------
//  09/03/2009 1:10:19 PM  Sudip Guha      n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class MasterEnum
    {

        #region Employee

        public enum PrimarySkills
        {
            AspNetCompactFramework = 35,
            AutomatedTesting = 36,
            CPlusPlus = 37,
            COBOL = 38,
            Delphi = 39,
            J2EE = 40,
            J2ME = 41,
            Oracle = 42,
            Uniface = 43,
            Foxpro = 44
        }

        public enum Designations
        {
            Developer = 54,
            BA = 55,
            DBA = 56,
            Marketting = 57,
            PreSales = 58,
            PM = 59,
            SQA = 60,
            TechLead = 61,
            Trainee = 62,
            Tester = 63
        }

        public enum Qualification
        {
            BE = 24,
            MCA = 25,
            MCom = 26,
            BCom = 27,
            BCA = 28,
            MBA = 29,
            BTech = 30,
            PhD = 31,
            MPharma = 32,
            BSc = 33,
            Dr = 34
        }

        public enum Roles
        {
            DatabaseAdministrator = 45,
            Developer = 46,
            GPM = 47,
            Marketing = 48,
            Presales = 49,
            ProjectManager = 50,
            Recruiter = 51,
            SeniorDeveloper = 52,
            SeniorTester = 53
        }

        public enum BackendSkills
        {
            SQL = 14,
            SQL2000 = 15,
            SQL2005 = 16,
            Oracle = 17,
            Oracle10G =	18,
            MySql = 19,
            Access = 20,
            PalmDB = 21,
            Symbian = 22,
            Palm = 23
        }

        public enum EmployeeStatus
        {
            Active = 142,
            InActive = 143
        }

        public enum OperatingEnvironments
        {
            Linux = 7,
            Macintosh = 8,
            DOS = 9,
            Windows = 10,
            Palm = 11,
            RIMBlackberry = 12,
            Symbian = 13

        }

        /// <summary>
        /// Enum will define all the Department Name Master
        /// </summary>
        public enum Departments
        { 
            Projects = 1,
            Admin = 2,
            HR=3,
            Marketing=4,
            Finance=5,
            Support=6,
            Testing = 7,
            PMOQuality = 8,
            RaveDevelopment=9,
            PreSales= 10,
            ITS= 11,
            Recruitment = 12,
            PreSalesUK = 13,
            PreSalesUSA = 14,
            BA = 16,
            Usability = 17,
            ATG = 18,
            RPG = 19, 
            RaveConsultant_India = 22,
            RaveConsultant_USA = 25,
            RaveConsultant_UK = 28,
            RaveForecastedProjects = 29,
            ProjectMentee2010 = 30
        }

        /// <summary>
        /// Enum will define all the ExperienceType Master
        /// </summary>
        public enum ExperienceType
        {
            Relevent =231,
            NonRelevent = 232
        }

        /// <summary>
        /// define all the ProficiencyLevel
        /// </summary>
        public enum ProficiencyLevel
        {
            BelowAverage = 258,
            Average = 259,
            Good = 260,
            VeryGood = 261,
            OutStanding = 262
        }

        /// <summary>
        /// define the pageMode
        /// </summary>
        public enum PageModeEnum
        {  
            View,
            Edit
        }

        /// <summary>
        /// define the Months.
        /// </summary>
        public enum Months
        { 
            January =1,
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        }

        /// <summary>
        /// define the ContactType
        /// </summary>
        public enum ContactType
        {
            CarPhone =302,
            Home,
            Office,
            Mobile
        }

        /// <summary>
        /// define the AddressType
        /// </summary>
        public enum AddressType
        {
            Current = 1,
            Permanent
        }

        /// <summary>
        /// define the AddressType
        /// </summary>
        public enum EmployeeRelation
        {
            Father = 566,
            Mother,
            Spouse,
            Sibling,
            Other

        }

        public enum EmployeeType
        {
            Permanent = 144,
            Contract=145,
            OnsiteContract = 684,
            ASE=683,
        }
        #endregion

        #region Resource Plan

        /// <summary>
        /// Enum will define all the Resource Plan status Master
        /// </summary>
        public enum ResourcePlanStatus
        {
            Active = 151,
            InActive = 152,
        }

        /// <summary>
        /// Enum will define all the Resource Plan approval Master
        /// </summary>
        public enum ResourcePlanApprovalStatus
        {
            Approved = 153,
            Rejected = 154,
        }

        /// <summary>
        /// Enum will define all the Resource Location Master
        /// </summary>
        public enum ResourceLocation
        {
            Onsite = 101,
            Offshore = 103,
            OnsiteOffshore = 102
        }

        /// <summary>
        /// Enum will define all the Resource Plan Duration Edition Status
        /// </summary>
        public enum RPDurationEditionStatus
        {
            Edited = 265,
            Deleted = 266,
        }

        /// <summary>
        /// Enum will define all the Resource Plan Detail Edition Status
        /// </summary>
        public enum RPDetailEditionStatus
        {
            Edited = 267,
            Deleted = 268,
        }

        /// <summary>
        /// Enum will define all the Resource Plan Edition Status
        /// </summary>
        public enum RPEditionStatus
        {
            Deleted = 269,
        }

        #endregion Resource Plan

        #region MRF

        public enum AdminRole
        {
            ExecutiveAdministrationER=159,
            SecretarySec=160
        }

        public enum ProjectRole
        {
            ApplicationSpecialistAS =161,
            AssociateSoftwareEngineerASE = 162,
            AssociateSoftwareSpecialistASS=163,
            SeniorSoftwareEngineerSSE= 164,
            SeniorWebDesignerSWB = 165,
            SoftwareEngineerSE= 166,
            SoftwareSpecialistSS= 167,
            WebdesignerWD= 168,
            WebdeveloperWdev = 169,
            AssistantProjectManagerAPM= 170,
            ProjectManagerPM = 171,
            SeniorProjectManagerSPM= 172,
            JuniorBusinessAnalystJBA= 173,
            SeniorBusinessAnalystSBA=174,
            BusinessAnalystBA =175,
            HeadBusinessAnalysisandUsabilityHBA = 176,
            TechnicalArchitectTA = 177,
            SrTechnicalArchitectSTA=178,

            // Added by sunil as per discussion with megha
            TestAnalystTstA = 270,
            TestLeadTstL = 271,
            TestLeadAutomationTstAutho = 272,
            SeniorTestAnalystSrTstA = 273,
            SeniorTestLeadSrTstL = 274,
            SeniorAutomationEngineerSrAE = 275,
            JuniorAutomationEngineerJrAE = 276,
            JuniorTestLeadJrTstL = 277,
            JuniorTestAnalystJrTstA = 278,
            AssociateTestAnalystATstA = 279
        }

        public enum HRRole
        {
            AssociateExecutiveHRAHR = 179,
            ExecutiveHREHR = 182
        }

        public enum RecruitmentRole
        {
            ExecutiveRecruitmentER = 180,
            ManagerRecruitmentMR = 181,
            TeamLeadRecruitmentTLR = 183
        }
        public enum MarketingRole
        {
            ManagerPresalesandMarketingMPM = 184,
            ExecutiveMarketingEM = 185,
            SeniorExecutiveCorporatecommunicationsSrCC = 186,
            ExecutiveVPMaketingVPM = 187
        }

        public enum TestingRole
        {
            TestAnalystTstA = 188,
            TestLeadTstL = 189,
            TestLeadAutomationTstAutho = 190,
            SeniorTestAnalystSrTstA = 191,
            SeniorTestLeadSrTstL= 192,
            SeniorAutomationEngineerSrAE = 193,
            JuniorAutomationEngineerJrAE = 194,
            JuniorTestLeadJrTstL = 195,
            JuniorTestAnalystJrTstA = 196,
            AssociateTestAnalystATstA = 197
        }

        public enum FinanceRole
        {
            ChiefFinancialOfficerCFO= 198,
            AccountManagerAM = 257
        }

        public enum PMOQualityRole
        {
            ExecutivePMOEPMO = 199,
            SoftwareEngineerQualitySEQ = 200
        }

        public enum RaveDevelopmentRole
        {
            ApplicationSpecialistAS = 201,
            AssociateSoftwareEngineerASE = 202,
            AssociateSoftwareSpecialistASS = 203,
            SeniorSoftwareEngineerSSE = 204,
            SeniorWebDesignerSWB = 205,
            SoftwareEngineerSE = 206,
            SoftwareSpecialistSS = 207,
            WebdesignerWD = 208,
            WebdeveloperWdev = 209,
            AssistantProjectManagerAPM = 210,
            ProjectManagerPM = 211,
            SeniorProjectManagerSPM = 212,
            JuniorBusinessAnalystJBA = 213,
            SeniorBusinessAnalystSBA = 214,
            BusinessAnalystBA = 215,
            HeadBusinessAnalysisandUsabilityHBA = 216,
            TechnicalArchitectTA = 217,
            SrTechnicalArchitectSTA = 218,
            TestAnalystTstA = 219,
            TestLeadTstL = 220,
            TestLeadAutomationTstAutho = 221,
            SeniorTestAnalystSrTstA = 222,
            SeniorTestLeadSrTstL = 223,
            SeniorAutomationEngineerSrAE = 224,
            JuniorAutomationEngineerJrAE = 225,
            JuniorTestLeadJrTstL = 226,
            JuniorTestAnalystJrTstA = 227,
            AssociateTestAnalystATstA = 228

        }

        public enum PreSalesRole
        {
            AssistantVicePresidentAVP = 229
        }

        public enum ITSRole
        {
            ManagerITSMITS = 230
        }

        public enum MRFStatus
        {
            PendingAllocation = 74,
            PendingHeadCountApprovalOfCOO=75,
            PendingApprovalOfFinance = 76,
            PendingExternalAllocation=98,
            Closed=99,
            Rejected=100,
            PendingExpectedResourceJoin=141,
            ResourceJoin= 263,
            Abort= 264,
            Replaced= 287,
            PendingNewEmployeeAllocation = 733,
            PendingHeadCountApprovalOfFinance=764,
            MarketResearchCompleteAndClosed= 818
        }


        /// <summary>
        /// Only used in MRF Raise Page.
        /// </summary>
        public enum PageMode
        {
            ADD = 1,
            COPY = 2,
            PREVIOUS = 3,
            MOVE = 4
        }

        public enum CandidateLocation
        {
            Benguluru = 280,
            Mumbai = 281,
            Pune = 282,
            UK = 283,
            US = 284
        }

        public enum MRFDepartment
        {
            PreSales=10,
            PreSales_UK=13,
            PreSales_US=14,
            RaveConsultant_India = 22,
            PreSales_India = 23,
            RaveConsultant_USA = 25,
            RaveConsultant_UK = 28,
            RaveForecastedProject = 29,
            Testing = 7
            
        }
        //Rakesh :   57942   Id's are Using 790,791,793,2097,817
        public enum MRFPurpose
        { 
            HiringForProject=790,
            HiringForNewRole=791,
            HiringForFutureBusiness=792,
            Replacement=793,
            MarketResearchfeasibility=794,
            HiringForInternalProject=823,
            SubstituteForMaternityLeave = 2095,
            Others = 817,
            //Department
            //HiringforDepartment=2101   //local,
            HiringforDepartment=2105  //Live

        }

        public enum MRFType
        {
            Shortlist_and_make_anoffer = 85,
            Shortlist = 86
        }

        #endregion

        #region Contract
        /// <summary>
        /// 
        /// </summary>
        public enum AccountManager
        {
            AccountManager = 128,
            AccountManagertest = 129
        }

        public enum ProjectType
        { 
            FixedPrice = 123,
            FixedPriceIncentiveFee = 124,
            CostplusIncentiveFee = 125,
            FixedPriceFixedfee = 126,
            TandM =127
        }

        public enum ContractStatus
        {
            Active =117,
            InActive = 118
        }

        public enum ClientLocation
        {
            India = 67,
            UK = 68,
            USA = 69
        }

        public enum ProjectLocation
        {
            RaveIndia=137,
            RaveUK = 138,
            RaveUSA=139,
            RaveJapan=140
        }

        public enum ContractType
        {
            MSA = 107,
            PO = 108,
            SOW = 109,
            CR = 110,
            SORR = 111,
            WorkOrder = 112,
            Temporary = 113,
            Invoice = 114,
            RaveInternal = 116,
            Proposal = 764
        }

        #endregion

        #region Project
        
        public enum ProjectStatus
        {
            PreSales = 1,
            Delivery = 2,
            Closed = 3
        }

        #endregion Project

        #region SeatAllocation

        public enum RaveBranch
        {
            Mumbai = 563,
            Banglore = 564,
            Pune = 565
        }

        #endregion  SeatAllocation

        #region 4C
        public enum FourCRole
        {
            //CREATOR = 1,
            //REVIEWER = 2,
            //CREATORANDREVIEWER = 3,
            FOURCADMIN = 1,
            FOURCADMIN_REVIEWER = 2,
            ONLYCREATOR = 3,
            VIEWACCESSRIGHTS = 4,
            RMS_FUNCTIONALMANAGER = 5,
            CREATOR = 6,
            REVIEWER = 7,
            ViewMy4C = 8
            // Mohamed : 13/02/2015 : Starts                        			  
            // Desc : 4C access rights

            , REPORTACCESS = 9
            // Mohamed : 13/02/2015 : Ends
        }
        #endregion 4C

    }
}

