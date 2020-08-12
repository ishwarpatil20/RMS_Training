//------------------------------------------------------------------------------
//
//  File:           Recruitment.cs       
//  Author:         Gaurav.Thakkar
//  Date written:   09/17/2009 04:39:10 PM
//  Description:    Class Contains Business Entities for Recruitment.
//  Amendments
//  Date                    Who                 Ref     Description
//  ----                    ---                 ---     -----------
//  09/17/2009 04:39:10 PM   Gaurav.Thakkar         n/a     Created    
//
//------------------------------------------------------------------------------

using System;

namespace BusinessEntities
{
    /// <summary>
    /// Defines the Business entities related to Recruitment
    /// </summary>
    [Serializable]
    public class Recruitment
    {
        #region Property

        /// <summary>
        /// Candidate Id
        /// </summary>
        public int CandidateId { get; set; }

        /// <summary>
        /// MRF Id
        /// </summary>
        public int MRFId { get; set; }

        /// <summary>
        /// Project Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Department Id
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Prefix of Candidate
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// PrefixId
        /// </summary>
        public int PrefixId { get; set; }

        /// <summary>
        /// First Name Of Candidate
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Middle Name Of Candidate
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Last Name Of Candidate
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Designation
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// Designation Id
        /// </summary>
        public int DesignationId { get; set; }

        /// <summary>
        /// Band
        /// </summary>
        public string Band { get; set; }

        /// <summary>
        /// Band Id
        /// </summary>
        public int BandId { get; set; }

        /// <summary>
        /// Expected Joinnig Date
        /// </summary>
        public DateTime ExpectedJoiningDate { get; set; }

        /// <summary>
        /// Employee Type
        /// </summary>
        public string EmployeeType { get; set; }

        /// <summary>
        /// Employee Type Id
        /// </summary>
        public int EmployeeTypeId { get; set; }

        /// <summary>
        /// Actual CTC
        /// </summary>
        public decimal ActualCTC { get; set; }

        /// <summary>
        /// Reporting Id
        /// </summary>
        public string ReportingId { get; set; }

        /// <summary>
        /// Resource Joined Date
        /// </summary>
        public DateTime ResourceJoinedDate { get; set; }

        /// <summary>
        /// Is Resource Joined
        /// </summary>
        public Int16 IsResourceJoined { get; set; }

        /// <summary>
        /// Client Name
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Project Name
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Reporting To
        /// </summary>
        public string ReportingTo { get; set; }

        /// <summary>
        /// Department
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Role Id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Resource Name
        /// </summary>
        public string ResourceName { get; set; }

        /// <summary>
        /// Recruitment Manager
        /// </summary>
        public string RecruitmentManager { get; set; }

        /// <summary>
        /// MRF Code
        /// </summary>
        public string MRFCode { get; set; }

        /// <summary>
        /// Role
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// EmailId
        /// </summary>
        public string EmailId { get; set; }

        /// <summary>
        /// EmailId of created by MRF
        /// </summary>
        public string EmailIdOfCreatedByMRF { get; set; }

        /// <summary>
        /// Reason
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Get Link
        /// </summary>
        public string GetLink { get; set; }

        /// <summary>
        /// Get Location of Candidate
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Get External Work Experience of Candidate.
        /// </summary>
        public string ExternalWorkExp { get; set; }

        /// <summary>
        /// Get Resource Bussiness Unit of Candidate.
        /// </summary>
        public int ResourceBussinessUnit { get; set; }

        /// <summary>
        /// Get Address of Candidate.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Get PhoneNo. of Candidate.
        /// </summary>
        public string PhoneNo { get; set; }

        /// <summary>
        /// Get wheather  EmailId field is editable or not for a department.
        /// </summary>
        public bool IsEditedEmailId { get; set; }


        /// <summary>
        /// Gets or sets the landline no.
        /// </summary>
        /// <value>The landline no.</value>
        public string LandlineNo { get; set; }
        /// <summary>
        /// Gets or sets the ProjectManagerName
        /// </summary>
        public string ProjectManagerName { get; set; }

        /// <summary>
        /// Gets or sets the DepartmentHeadName
        /// </summary>
        public string DepartmentHeadName { get; set; }


        public string Prev_ExternalWorkExp { get; set; }

        /// <summary>
        /// Get Resource Bussiness Unit of Candidate.
        /// </summary>
        public int Prev_ResourceBussinessUnit { get; set; }

        /// <summary>
        /// Get Address of Candidate.
        /// </summary>
        public string Prev_Address { get; set; }

        /// <summary>
        /// Get PhoneNo. of Candidate.
        /// </summary>
        public string Prev_PhoneNo { get; set; }

        /// <summary>
        /// Gets or sets the landline no.
        /// </summary>
        /// <value>The landline no.</value>
        public string Prev_LandlineNo { get; set; }

        /// <summary>
        /// Band Id
        /// </summary>
        public string Prev_Band { get; set; }

        /// <summary>
        /// Expected Joinnig Date
        /// </summary>
        public DateTime Prev_ExpectedJoiningDate { get; set; }
                
        /// <summary>
        /// Employee Type
        /// </summary>
        public string Prev_EmployeeType { get; set; }

        /// <summary>
        /// Department
        /// </summary>
        public string Prev_Department { get; set; }

        /// <summary>
        /// Actual CTC
        /// </summary>
        public decimal Prev_ActualCTC { get; set; }

        /// <summary>
        /// PrefixId
        /// </summary>
        public string Prev_Prefix { get; set; }

        /// <summary>
        /// First Name Of Candidate
        /// </summary>
        public string Prev_FirstName { get; set; }

        /// <summary>
        /// Middle Name Of Candidate
        /// </summary>
        public string Prev_MiddleName { get; set; }

        /// <summary>
        /// Last Name Of Candidate
        /// </summary>
        public string Prev_LastName { get; set; }

        /// <summary>
        /// Designation
        /// </summary>
        public string Prev_Designation { get; set; }

        public string CandidateEmailID { get; set; }

        public int ContractDuration { get; set; }


        public int ResourceBusinessUnitID { get; set; }

        public string ResourceBussinessUnitName { get; set; }

        public DateTime CandidateOfferAcceptedDate { get; set; }

        /// <summary>
        /// Gets or sets the MRF purpose.
        /// </summary>
        /// <value>The MRF purpose.</value>
        public string MRFPurpose { get; set; }

        /// <summary>
        /// Gets or sets the relevant experience year.
        /// </summary>
        /// <value>The relevant experience year.</value>
        public int RelevantExperienceYear { get; set; }

        /// <summary>
        /// Gets or sets the relavant experience month.
        /// </summary>
        /// <value>The relavant experience month.</value>
        public int RelavantExperienceMonth { get; set; }

        /// <summary>
        /// Gets or sets the MRF Status.
        /// </summary>
        /// <value>The MRF Status.</value>
        public int MRFStatus { get; set; }


        /// <summary>
        /// Gets or sets the prev_ mrfcode.
        /// </summary>
        /// <value>The prev_ mrfcode.</value>
        public string Prev_Mrfcode { get; set; }

        /// <summary>
        /// Gets or sets the prev_ recruitment manager.
        /// </summary>
        /// <value>The prev_ recruitment manager.</value>
        public string Prev_RecruitmentManager { get; set; }

        /// <summary>
        /// Gets or sets the prev_ project id.
        /// </summary>
        /// <value>The prev_ project id.</value>
        public int Prev_ProjectId { get; set; }

        // Rajan Kumar : Issue 39508: 31/01/2014 : Starts                        			 
        // Desc : Traninig for new joining employee. (Training Gaps).
        public bool IsTrainingRequired { get; set; }
        public string TrainingSubject { get; set; }
        // Rajan Kumar : Issue 39508: 31/01/2014 : END
        //Mohamed : Issue 50306 : 09/09/2014 : Starts                        			  
        //Desc : Expected Joinee's details edited[MRF Code: MRF_Testing_SrTstA_0387] old  Joining date is default date - Mail for de-linking MRF.
        public int Prev_MRFId { get; set; }
        //Mohamed : Issue 50306 : 09/09/2014 : Ends
        #endregion Property
    }
}
