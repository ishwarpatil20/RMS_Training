using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Common
{
    public class EnumsConstants
    {
        public enum Category
        {
            AccountManager = 1,
            BackendSkills = 2,
            ClientName = 3,
            ContractStatus = 4,
            ContractType = 5,
            Designations = 6,
            Domain = 7,
            Duration = 8,
            HighestQual = 9,
            Location = 10,
            MRF = 11,
            MRFStatus = 12,
            MRFType = 13,
            OperatingEnvironments = 14,
            PrimarySkills = 15,
            ProjectCategory = 16,
            ProjectGroup = 17,
            ProjectLocation = 18,
            ProjectStandardHours = 19,
            ProjectStatus = 20,
            ProjectType = 21,
            Qualification = 22,
            ResourceLocation = 23,
            ResourceStatus = 24,
            //Roles=25,//--Commented enum as it is not being used 
            SkillsCategory = 26,
            Tools = 27,
            ResourcePlanStatus = 28,
            EmployeeStatus = 29,
            EmployeeType = 30,
            EmployeeBand = 31,
            ResourcePlanApprovalStatus = 32,
            Prefix = 33,
            AdminRole = 34,
            ProjectRole = 35,
            HRRole = 36,
            MarketingRole = 37,
            TestingRole = 38,
            FinanceRole = 39,
            PMOQualityRole = 40,
            RaveDevelopmentRole = 41,
            PreSalesRole = 42,
            ITSRole = 43,
            ExperienceType = 44,
            Languages = 45,
            Databases = 46,
            InternetTechnologies = 47,
            OperatingSystems = 48,
            Others = 49,
            ProficiencyLevel = 51,
            RecruitmentRole = 52,
            CandidateLocation = 56,
            ProjectDoneStatus = 57,
            SeatAllocationCategory = 59,
            ContactType = 60,
            RaveBranch = 62,
            EmployeeRelation = 63,
            ResourceBusinessunit = 64,
            MRFPurpose = 74,
            TypeOfAllocation = 75,
            TypeOfResourceAllocationSupply = 76,
            TypeOfMRFDemand = 77,
            OnGoingProjectStauts = 81

            // Mohamed : Issue  : 23/09/2014 : Starts                        			  
                // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
            ,
            ProjectDivision = 96
                ,
            ProjectBussinessArea = 97
                ,
            ProjectBussinessSegment = 98
                // Mohamed : Issue  : 23/09/2014 : Ends

                //Siddharth 12th March 2015 Start
                ,
            ProjectModel = 100    //make it 100 Before giving to Venkatesh
                //Siddharth 12th March 2015 End

                //Siddhesh Arekar Issue ID : 55884 Closure Type
                , TypeOfClosure = 103
            //Siddhesh Arekar Issue ID : 55884 Closure Type

        }

        #region Enums- NewEmailRecepient configuration
        public enum RMSModule
        {
            ResourcePlan = 1,
            Contract = 2,
            Projects = 3,
            MRF = 4,
            Recruitment = 5,
            Employee = 6,
            SeatAllocation = 7,
            FourC = 8,
            //Ishwar Patil : Training Module : 08/05/2014 Start
            TrainingModule = 9
            //Ishwar Patil : Training Module : 08/05/2014 End
        }

        public enum EmailFunctionality
        {
            #region Resource Plan
            CreateRPApproval = 1,
            EditedRPApproval = 2,
            ApprovedRejectedRP = 3,
            #endregion Resource Plan

            #region Contract
            AddedContract = 1,
            EditedContract = 2,
            DeletedContract = 3,
            #endregion Contract

            #region MRF
            RaiseMRFForProjects = 1,
            RaiseMRFForDepartments = 2,
            RaiseHeadCount = 3,
            RaiseHeadCountWithOutApproval = 4,
            HeadCountRequestapprovalToCOO = 5,
            RejectHeadCount = 6,
            ExternalResourceAllocation = 7,
            ResourceAllocationMailToWhizible = 8,
            MRFClosure = 9,
            InternalResourceAllocationApprovalFromFinance = 10,
            InternalResourceAllocationRejectionByFinance = 11,
            ReplaceMRF = 12,
            InternalResourceAllocated = 13,
            AbortMRF = 14,
            RaiseHeadCountWithOutApprovalInGroup = 15,
            RaiseHeadCountToRecruitmentTeam = 17,
            MRFStatusChange = 16,
            MRFStatusChangetoClosed = 18,
            //Move MRF
            MoveMrfPendingAllcation = 19,
            MoveMrfPendingExternalAllocation = 20,
            MoveMrfPendingApprovalOfFinance = 21,
            MoveMrfPendingHeadCountApprovalOfFinance = 22,
            MoveMrfPendingHeadCountApprovalOfCOO = 23,
            MoveMrfPendingExpectedResourceJoin = 24,
            MoveMrfResourceJoin = 25,
            MoveMrfPendingNewEmployeeAllocation = 26,
            DeleteMRF = 27,
            CandidateDelinkedfromMRF = 28,
            // 27642-Ambar-Start
            RejectedMRFStatusChange = 29,
            // 27642-Ambar-End
            // Mohamed : Issue 51824  : 19/08/2014 : Starts                        			  
            // Desc : Provision in RMS system for a notification to be sent when an MRf is assigned  from one recruiter to another.
            MRFAssignedFromOneRecruiterToAnother = 30,
            // Mohamed : Issue 51824  : 19/08/2014 : Ends
            #endregion MRF

            #region Project
            AddedProject = 1,
            EditedProject = 2,
            DeletedProject = 3,
            #endregion Project

            #region Employee
            AddEmployee = 1,
            DeleteEmployee = 2,
            EmployeeDetailsUpdate = 3,
            EditEmployee = 4,
            EditEmployeeNotSelfDetails = 5,
            ApprovalOfSkillsRating = 6,
            EmployeeProjectClosed = 7,
            EmployeeReleaseFromProject = 8,
            EmployeeReleaseFromDepartment = 9,
            EmployeeProjectAllocationUpdation = 10,
            EmployeeResignFromCompany = 11,
            EmployeeSeperationFromCompany = 12,
            EmployeeRollBackResignFromCompany = 13,
            //Aarohi : Issue 30053(CR) : 22/12/2011 : Start 
            EmployeeUploadedResume = 14,
            EmployeeDeletedResume = 15,
            //Aarohi : Issue 30053(CR) : 22/12/2011 : End
            #endregion Employee

            #region Recruitment
            CandidateAdded = 1,
            CandidateUpdated = 2,
            CandidateJoined = 3,
            CandidateExpectedToJoin = 4,
            CandidateDeleted = 5,
            //Rajan Kumar : Issue 39508: 04/02/2014 : Starts                        			 
            // Desc : Traninig for new joining employee. (Training Gaps).
            CandidateSkillsGap = 6,
            CandidateSkillsGapEdit = 7,
            // Rajan Kumar : Issue 39508: 04/02/2014 : END
            //Mohamed Dangra : Issue 50306 : 09/09/2014 : Starts                        			 
            // Desc : Check if current effective joining date is same as previous effective joining date and current designation id is same as previous designation id           
            MRFDeLinked = 8,
            // Rajan Kumar : Issue 50306 : 09/09/2014 : END
            #endregion Recruitment

            #region Seat Allocation

            AllocatedSeat = 1,
            ShiftedSeat = 2,

            #endregion Seat Allocation

            #region "Four C"
            SendForReview = 1,
            RejectRating4C = 2,
            //Venkatesh : 4C_Support : 18/3/2014 : Start 
            SendForReviewSupport = 3,
            //Venkatesh : 4C_Support : 18/3/2014 : End
            #endregion

            //Ishwar Patil : Training Module : 18/08/2014 Start
            #region "TrainingModule"
            TechSoftSkillRequest = 1,
            TechSoftSkillRequestbehalfManager = 2,
            KSSRequest = 3,
            SeminarRequest = 4,
            TechSoftSkillApproved = 5,
            TechSoftSkillRejectd = 6,
            SeminarsApproved = 7,
            SeminarsRejectd = 8,
            TechSoftSkillRequestEdit = 9,
            TechSoftSkillRequestDeleted = 10,
            SeminarsRequestEdit = 12,
            KSSRequestEdit = 13,
            SeminarsRequestDeleted = 11,
            NominationSubmitted = 14,
            NominationDeleted = 15,
            MailToAdmin = 16,
            InviteNomination = 17,
            SendFeedback = 19,
            SendDropOut = 20,
            NewNominationConfirmed = 21,
            NonConfirmNomination = 22,
            SendToManagerForPostRating = 23,
            ManagerFilledPostTrainingRating = 24,
            SendEffectivenessMailToAll = 25,

            TrainerFeedbackAvgRating = 26,
            BehalfOfManagerOrEmployeeNominationSubmitted = 27,
            PostTrainingRatingSubmitedByRMGOnBehalfOfManager = 28,
            NominationRejected = 29
            #endregion
            //Ishwar Patil : Training Module : 18/08/2014 End
            

        }

        public enum Location
        {
            Mumbai = 1,
            Bangalore = 2
        }

        #endregion Enums- NewEmailRecepient configuration
    }

}
