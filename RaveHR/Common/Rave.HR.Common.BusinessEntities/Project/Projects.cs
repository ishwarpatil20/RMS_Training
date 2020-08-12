//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           RaveHRProjects.cs       
//  Author:         vineet.kulkarni
//  Date written:   4/3/2009/ 3:12:30 PM
//  Description:    This class contains properties related to Project module. 
//                  These business entities used for ProjectSummary.aspx, ApproveRejectProject.aspx.cs etc.
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  4/3/2009 3:12:30 PM  vineet.kulkarni    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    [Serializable]
    public class Projects
    {
        #region Field Members

        /// <summary>
        /// Create _projectId variable with integer type
        /// </summary>
        private int _projectId;

        /// <summary>
        /// Create _projectCode variable with string type
        /// </summary>
        private string _projectCode;

        /// <summary>
        /// Create _projectName variable with string type
        /// </summary>
        private string _projectName;

        /// <summary>
        /// Create _clientName variable with string type
        /// </summary>
        private string _clientName;

        /// <summary>
        /// Create _projectStatus variable with string type
        /// </summary>
        private string _projectStatus;

        /// <summary>
        /// Create _totalMRF variable with integer type
        /// </summary>
        private int _totalMRF;

        /// <summary>
        /// Create _openMRF variable with integer type
        /// </summary>
        private int _openMRF;        

        /// <summary>
        /// Create _location variable with string type
        /// </summary>
        private string _location;

        /// <summary>
        /// Create _domain variable with string type
        /// </summary>
        private string _domain;
        
        /// <summary>
        /// Create _projectCategoryID variable with string type
        /// </summary>
        private string _projectCategoryID;
       
        /// <summary>
        /// Create _standardHours variable with float type
        /// </summary>
        private string _standardHours;
        
        /// <summary>
        /// Create _projectGroup variable with string type
        /// </summary>
        private string _projectGroup;

        /// <summary>
        /// Create _description variable with string type
        /// </summary>
        private string _description;

        /// <summary>
        /// Create _delete variable with string type
        /// </summary>
        private string _delete;

        /// <summary>
        /// Create _reject variable with string type
        /// </summary>
        private string _reject;

        /// <summary>
        /// Create _prevProjectId variable with int type
        /// </summary>
        private int _prevProjectId;

        /// <summary>
        /// Create _nextProjectId variable with int type
        /// </summary>
        private int _nextProjectId;

        /// <summary>
        /// Create _firstName variable with string type
        /// </summary>
        private string _firstName;

        /// <summary>
        /// Create _resourceID variable with int type
        /// </summary>
        private int _resourceId;

        /// <summary>
        /// Create _fullName variable with string type
        /// </summary>
        private string _fullName;
        
        /// <summary>
        /// Create _startDate variable with DateTime type
        /// </summary>
        private DateTime _startDate;

        /// <summary>
        /// Create _endDate variable with DateTime type
        /// </summary>
        private DateTime _endDate;

        /// <summary>
        /// Need to be removed: Create _category variable with string type
        /// </summary>
        private string _category;
        
        /// <summary>
        /// Need to be removed: create _technologyName with string type
        /// </summary>
        private string _technologyName;

        /// <summary>
        /// Create _id variable with integer type 
        /// </summary>
        private int _id;

        /// <summary>
        /// Create _name variable with string type
        /// </summary>
        private string _name;

        /// <summary>
        /// Create _lstcat object with generic list type
        /// </summary>
        private List<Category> _lstcat = new List<Category>();

        /// <summary>
        /// Create _lstTechnologies object with generic list type
        /// </summary>
        List<BusinessEntities.Technology> _lstTechnologies = new List<Technology>();

        /// <summary>
        /// create _createdBy as string
        /// </summary>
        private string _createdBy;

        /// <summary>
        /// create _lstDomain as List
        /// </summary>
        private List<Domain> _lstDomain = new List<Domain>();

        /// <summary>
        /// create _lstSubDomain as List
        /// </summary>
        private List<SubDomain> _lstSubDomain = new List<SubDomain>();

        /// <summary>
        /// create workflowstatus property
        /// </summary>
        private string _workFlowStatus;

        /// <summary>
        /// create createdByFullName property
        /// </summary>
        private string _createdByFullName;

        /// <summary>
        /// create projectStartYear property
        /// </summary>
        private int _projectStartYear;

        /// <summary>
        /// create projectEndYear property
        /// </summary>
        private int _projectEndYear;

        /// <summary>
        /// create EmailIdOfPM property
        /// </summary>
        private string _emailIdOfPM;

        /// <summary>
        /// Create _clientId variable with string type
        /// </summary>
        private string _clientId;

        /// <summary>
        /// Create _prevEndDate variable with string type
        /// </summary>
        private DateTime _prevEndDate;

        /// <summary>
        /// Create PrevStanderdHrs variable with decimal type
        /// </summary>
        private Decimal _prevStanderdHours;

        /// <summary>
        /// Create PrevProjectGroup variable with string type
        /// </summary>
        private string _prevProjectGroup;

        /// <summary>
        /// Create PrevProjectStatus variable with string type
        /// </summary>
        private string _prevProjectStatus;

        /// <summary>
        /// Create _projectExecutiveSummary variable with string type
        /// </summary>
        private string _projectExecutiveSummary;

        /// <summary>
        /// Create PrevProjectExecutiveSummary variable with string type
        /// </summary>
        private string _prevProjectExecutiveSummary;

        private string _OnGoingProjectStatusID;

        private string _projectCodeAbbrevation;

        // Mohamed : Issue  : 23/09/2014 : Starts                        			  
        // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page

        public int _ProjectDivision;
        public int _ProjectBussinessArea;
        public int _ProjectBussinessSegment;
        public string _ProjectAlias;

        public string _division;
        public string _businessSegment;
        public string _businessArea;

        // Mohamed : Issue  : 23/09/2014 : Ends


        //Siddharth 12 March 2015 Start
        public string _projectmodel;
        //Siddharth 12 March 2015 End

        //Siddharth 3 August 2015 Start
        public string _businessVertical;
        //Siddharth 3 August 2015 End

        #endregion Field Members

        #region Properties 
        
        /// <summary>
        /// Define ProjectId property
        /// </summary>
        public int ProjectId
        {
            get
            {
                return _projectId;
            }
            set
            {
                _projectId = value;
            }
        }

        /// <summary>
        /// Define ProjectCode property
        /// </summary>
        public string ProjectCode
        {
            get
            {
                return _projectCode;
            }
            set
            {
                _projectCode = value;
            }
        }

        /// <summary>
        /// Define ProjectName property
        /// </summary>
        public string ProjectName
        {
            get
            {
                return _projectName;
            }
            set
            {
                _projectName = value;
            }
        }

        /// <summary>
        /// Define ClientName property
        /// </summary>
        public string ClientName
        {
            get
            {
                return _clientName;
            }
            set
            {
                _clientName = value;
            }
        }

        /// <summary>
        /// Define ProjectStatus property
        /// </summary>
        public string ProjectStatus
        {
            get
            {
                return _projectStatus;
            }
            set
            {
                _projectStatus = value;
            }
        }

        /// <summary>
        /// Define TotalMRF property
        /// </summary>
        public int TotalMRF
        {
            get
            {
                return _totalMRF;
            }
            set
            {
                _totalMRF = value;
            }
        }

        /// <summary>
        /// Define OpenMRF property
        /// </summary>
        public int OpenMRF
        {
            get
            {
                return _openMRF;
            }
            set
            {
                _openMRF = value;
            }
        }        
      
        /// <summary>
        /// Define Location property
        /// </summary>
        public string Location
        {
            get 
            { 
                return _location; 
            }
            set 
            { 
                _location = value; 
            }
        }

        /// <summary>
        /// Define Domain property
        /// </summary>
        public string Domain
        {
            get 
            { 
                return _domain; 
            }
            set 
            { 
                _domain = value; 
            }
        }

        /// <summary>
        /// Define ProjectCategoryID property
        /// </summary>
        public string ProjectCategoryID
        {
            get 
            {
                return _projectCategoryID;
            }
            set 
            { 
                _projectCategoryID = value; 
            }
        }

        /// <summary>
        /// Define StandardHours property
        /// </summary>
        public string StandardHours
        {
            get 
            { 
                return _standardHours; 
            }
            set 
            { 
                _standardHours = value; 
            }
        }

        /// <summary>
        /// Define ProjectGroup property
        /// </summary>
        public string ProjectGroup
        {
            get 
            { 
                return _projectGroup; 
            }
            set 
            { 
                _projectGroup = value; 
            }
        }

        /// <summary>
        /// Define Description property
        /// </summary>
        public string Description
        {
            get 
            { 
                return _description; 
            }
            set 
            {
                _description = value; 
            }
        }

        /// <summary>
        /// Define Delete property
        /// </summary>
        public string Delete
        {
            get 
            { 
                return _delete; 
            }
            set 
            { 
                _delete = value; 
            }
        }

        /// <summary>
        /// Define Reject property
        /// </summary>
        public string Reject
        {
            get
            {
                return _reject;
            }
            set
            {
                _reject = value;
            }
        }

        /// <summary>
        /// Define PrevProjectId property
        /// </summary>
        public int PrevProjectId
        {
            get 
            { 
                return _prevProjectId; 
            }
            set 
            {
                _prevProjectId = value; 
            }
        }

        /// <summary>
        /// Define NextProjectId property
        /// </summary>
        public int NextProjectId
        {
            get 
            { 
                return _nextProjectId; 
            }
            set 
            { 
                _nextProjectId = value; 
            }
        }

        /// <summary>
        /// Define FirstName property
        /// </summary>
        public string FirstName
        {
            get 
            { 
                return _firstName; 
            }
            set 
            { 
                _firstName = value; 
            }
        }

        /// <summary>
        /// Define ResourceId property
        /// </summary>
        public int ResourceId
        {
            get
            {
                return _resourceId;
            }
            set
            {
                _resourceId = value;
            }
        }

        /// <summary>
        /// Define FullName property
        /// </summary>
        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                _fullName = value;
            }
        }
        
        /// <summary>
        /// Define StartDate property
        /// </summary>
        public DateTime StartDate
        {
            get 
            { 
                return _startDate; 
            }
            set 
            { 
                _startDate = value; 
            }
        }
        /// <summary>
        /// Define EndDate property
        /// </summary>
        public DateTime EndDate
        {
            get 
            { 
                return _endDate; 
            }
            set 
            { 
                _endDate = value; 
            }
        }
        /// <summary>
        /// Define Category property
        /// </summary>
        public string Category
        {
            get 
            { 
                return _category; 
            }
            set 
            { 
                _category = value; 
            }
        }

        /// <summary>
        /// Define TechnologyName property
        /// </summary>
        public string TechnologyName
        {
            get 
            { 
                return _technologyName; 
            }
            set 
            { 
                _technologyName = value; 
            }
        }

        /// <summary>
        /// Defines Define ID property
        /// </summary>
        public int ID
        {
            get 
            { 
                return _id; 
            }
            set 
            { 
                _id = value; 
            }
        }

        /// <summary>
        /// Defines Define Name property
        /// </summary>
        public string Name
        {
            get 
            { 
                return _name; 
            }
            set 
            { 
                _name = value; 
            }
        }

        /// <summary>
        /// Defines Define Categories property
        /// </summary>
        public List<Category> Categories
        {
            get 
            { 
                return _lstcat; 
            }
            set 
            { 
                _lstcat = value; 
            }
        }

        /// <summary>
        /// Define Technologies property
        /// </summary>
        public List<BusinessEntities.Technology> Technologies
        {
            get
            {
                return _lstTechnologies;
            }
            set
            {
                _lstTechnologies = value;
            }

        }

        /// <summary>
        /// Define CreatedBy property
        /// </summary>
        public string CreatedBy
        {
            get
            {
                return _createdBy;
            }
            set
            {
                _createdBy = value;
            }
        }

        /// <summary>
        /// Define LstDomain property
        /// </summary>
        public List<Domain> LstDomain
        {
            get 
            { 
                return _lstDomain ;
            }
            set 
            { 
                _lstDomain  = value; 
            }
        }

        /// <summary>
        /// Define LstSubDomain property
        /// </summary>
        public List<SubDomain> LstSubDomain
        {
            get 
            { 
                return _lstSubDomain ; 
            }
            set 
            { 
                _lstSubDomain  = value; 
            }
        }

        /// <summary>
        /// Define WorkFlowStatus property
        /// </summary>
        public string WorkFlowStatus
        {
            get 
            { 
                return _workFlowStatus; 
            }
            set 
            { 
                _workFlowStatus = value; 
            }
        }

        /// <summary>
        /// Define createdByFullName property
        /// </summary>
        public string CreatedByFullName
        {
            get
            {
                return _createdByFullName;
            }
            set
            {
                _createdByFullName = value;
            }
        }        

        /// <summary>
        /// Define ProjectStartYear property
        /// </summary>
        public int ProjectStartYear
        {
            get
            {
                return _projectStartYear;
            }
            set
            {
                _projectStartYear = value;
            }
        }

        /// <summary>
        /// Define ProjectEndYear property
        /// </summary>
        public int ProjectEndYear
        {
            get
            {
                return _projectEndYear;
            }
            set
            {
                _projectEndYear = value;
            }
        }

        /// <summary>
        /// Define EmailIdOfPM property
        /// </summary>
        public string EmailIdOfPM
        {
            get
            {
                return _emailIdOfPM;
            }
            set
            {
                _emailIdOfPM = value;
            }
        }

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
        /// Define the Project row num .
        /// </summary>
        public string ProjectRowNo { get; set; }

        /// <summary>
        /// Define ClientId property
        /// </summary>
        public string ClientId
        {
            get
            {
                return _clientId;
            }
            set
            {
                _clientId = value;
            }
        }

        /// <summary>
        /// Define PrevEndDate property
        /// </summary>
        public DateTime PrevEndDate
        {
            get
            {
                return _prevEndDate;
            }
            set
            {
                _prevEndDate = value;
            }
        }

        /// <summary>
        /// Define PrevStandardHours property
        /// </summary>
        public Decimal PrevStandardHours
        {
            get
            {
                return _prevStanderdHours;
            }
            set
            {
                _prevStanderdHours = value;
            }
        }

        /// <summary>
        /// Define PrevProjectGroup property
        /// </summary>
        public string PrevProjectGroup
        {
            get
            {
                return _prevProjectGroup;
            }
            set
            {
                _prevProjectGroup = value;
            }
        }

        /// <summary>
        /// Define PrevProjectStatus property
        /// </summary>
        public string PrevProjectStatus
        {
            get
            {
                return _prevProjectStatus;
            }
            set
            {
                _prevProjectStatus = value;
            }
        }

        /// <summary>
        /// Define ProjectExecutiveSummary property
        /// </summary>
        public string ProjectExecutiveSummary
        {
            get
            {
                return _projectExecutiveSummary;
            }
            set
            {
                _projectExecutiveSummary = value;
            }
        }

        /// <summary>
        /// Define PrevProjectExecutiveSummary property
        /// </summary>
        public string PrevProjectExecutiveSummary
        {
            get
            {
                return _prevProjectExecutiveSummary;
            }
            set
            {
                _prevProjectExecutiveSummary = value;
            }
        }


        public string OnGoingProjectStatusID
        {
            get
            {
                return _OnGoingProjectStatusID;
            }
            set
            {
                _OnGoingProjectStatusID = value;
            }
        }

        /// <summary>
        /// Gets or sets the project code abbrevation.
        /// </summary>
        /// <value>The project code abbrevation.</value>
        public string ProjectCodeAbbrevation
        {
            get
            {
                return _projectCodeAbbrevation;
            }
            set
            {
                _projectCodeAbbrevation = value;
            }
        }

        // Mohamed : Issue  : 23/09/2014 : Starts                        			  
        // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page

        public int ProjectDivision
        {
            get
            {
                return _ProjectDivision;
            }
            set
            {
                _ProjectDivision = value;
            }
        }

        public string Division
        {
            get
            {
                return _division;
            }
            set
            {
                _division = value;
            }
        }

        //Siddharth 12 March 2015 Start
        public string ProjectModel
        {
            get
            {
                return _projectmodel;
            }
            set
            {
                _projectmodel = value;
            }
        }
        //Siddharth 12 March 2015 End

        //Siddharth 3 August 2015 Start
        public string BusinessVertical
        {
            get
            {
                return _businessVertical;
            }
            set
            {
                _businessVertical = value;
            }
        }
        //Siddharth 3 August 2015 End

        public int ProjectBussinessArea
        {
            get
            {
                return _ProjectBussinessArea;
            }
            set
            {
                _ProjectBussinessArea = value;
            }
        }


        public string BusinessArea
        {
            get
            {
                return _businessArea;
            }
            set
            {
                _businessArea = value;
            }
        }

        public int ProjectBussinessSegment
        {
            get
            {
                return _ProjectBussinessSegment;
            }
            set
            {
                _ProjectBussinessSegment = value;
            }
        }

        public string BusinessSegment
        {
            get
            {
                return _businessSegment;
            }
            set
            {
                _businessSegment = value;
            }
        }

        public string ProjectAlias
        {
            get
            {
                return _ProjectAlias;
            }
            set
            {
                _ProjectAlias = value;
            }
        }

        // Mohamed : Issue  : 23/09/2014 : Ends



        //Rakesh : HOD for Employees 11/07/2016 Begin   
        public int ProjectHeadId { get; set; }
        //Rakesh : HOD for Employees 11/07/2016 End

        #endregion Properties
    }
}
