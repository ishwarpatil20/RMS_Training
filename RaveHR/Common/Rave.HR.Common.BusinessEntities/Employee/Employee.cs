//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           Employee.cs       
//  Author:         vineet.kulkarni
//  Date written:   12/08/2009 3:09:27 PM
//  Description:    This class defines business entities related to Employee details
//                  These businesss entities are used for Employee related pages
//
//  Amendments
//  Date                    Who                 Ref     Description
//  ----                    -----------         ---     -----------
//  12/08/2009 3:09:27 PM   vineet.kulkarni     n/a     Created    
//
//------------------------------------------------------------------------------

using System;

namespace BusinessEntities
{
    /// <summary>
    /// Defines business entities related to employees details
    /// </summary>
    [Serializable]
    public class Employee
    {
        #region Properties

        /// <summary>
        /// Gets or sets the EMPId.
        /// </summary>
        /// <value>The EMPId.</value>
        public int EMPId { get; set; }

        /// <summary>
        /// Gets or sets the EMP code.
        /// </summary>
        /// <value>The EMP code.</value>
        public string EMPCode { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the name of the middle.
        /// </summary>
        /// <value>The name of the middle.</value>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the EMP picture.
        /// </summary>
        /// <value>The EMP picture.</value>
        public byte[] EMPPicture { get; set; }

        /// <summary>
        /// Gets or sets the email id.
        /// </summary>
        /// <value>The email id.</value>
        public string EmailId { get; set; }

        /// <summary>
        /// Gets or sets the status id.
        /// </summary>
        /// <value>The status id.</value>
        public int StatusId { get; set; }

        /// <summary>
        /// Gets or sets the group id.
        /// </summary>
        /// <value>The group id.</value>
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or sets the designation id.
        /// </summary>
        /// <value>The designation id.</value>
        public int DesignationId { get; set; }

        /// <summary>
        /// Gets or sets the role id.
        /// </summary>
        /// <value>The role id.</value>
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the band.
        /// </summary>
        /// <value>The band.</value>
        public int Band { get; set; }

        /// <summary>
        /// Gets or sets the joining date.
        /// </summary>
        /// <value>The joining date.</value>
        public DateTime JoiningDate { get; set; }

        /// <summary>
        /// Gets or sets the DOB.
        /// </summary>
        /// <value>The DOB.</value>
        public DateTime DOB { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>The gender.</value>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the marital status.
        /// </summary>
        /// <value>The marital status.</value>
        public string MaritalStatus { get; set; }

        /// <summary>
        /// Gets or sets the name of the father.
        /// </summary>
        /// <value>The name of the father.</value>
        public string FatherName { get; set; }

        /// <summary>
        /// Gets or sets the name of the spouse.
        /// </summary>
        /// <value>The name of the spouse.</value>
        public string SpouseName { get; set; }

        /// <summary>
        /// Gets or sets the blood group.
        /// </summary>
        /// <value>The blood group.</value>
        public string BloodGroup { get; set; }

        /// <summary>
        /// Gets or sets the emergency contact no.
        /// </summary>
        /// <value>The emergency contact no.</value>
        public string EmergencyContactNo { get; set; }

        /// <summary>
        /// Gets or sets the passport no.
        /// </summary>
        /// <value>The passport no.</value>
        public string PassportNo { get; set; }

        //CR - 28321 Passport Application Number Sachin
        /// <summary>
        /// Gets or sets the Passport Appliaction no.
        /// </summary>
        /// <value>The Passport Appliaction no.</value>        
        public string PassportAppNo { get; set; }
        //CR - 28321 Passport Application Number Sachin End

        /// <summary>
        /// Gets or sets the passport issue date.
        /// </summary>
        /// <value>The passport issue date.</value>
        public DateTime PassportIssueDate { get; set; }

        /// <summary>
        /// Gets or sets the passport expire date.
        /// </summary>
        /// <value>The passport expire date.</value>
        public DateTime PassportExpireDate { get; set; }

        /// <summary>
        /// Gets or sets the passport issue place.
        /// </summary>
        /// <value>The passport issue place.</value>
        public string PassportIssuePlace { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [ready to relocate india].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [ready to relocate india]; otherwise, <c>false</c>.
        /// </value>
        public bool ReadyToRelocateIndia { get; set; }

        /// <summary>
        /// Gets or sets the reason not to relocate india.
        /// </summary>
        /// <value>The reason not to relocate india.</value>
        public string ReasonNotToRelocateIndia { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [ready to relocate].
        /// </summary>
        /// <value><c>true</c> if [ready to relocate]; otherwise, <c>false</c>.</value>
        public bool ReadyToRelocate { get; set; }

        /// <summary>
        /// Gets or sets the reason not to relocate.
        /// </summary>
        /// <value>The reason not to relocate.</value>
        public string ReasonNotToRelocate { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>The duration.</value>
        public string Duration { get; set; }

        /// <summary>
        /// Gets or sets the residence phone.
        /// </summary>
        /// <value>The residence phone.</value>
        public string ResidencePhone { get; set; }

        /// <summary>
        /// Gets or sets the mobile no.
        /// </summary>
        /// <value>The mobile no.</value>
        public string MobileNo { get; set; }

        /// <summary>
        /// Gets or sets the current address.
        /// </summary>
        /// <value>The Current address.</value>
        public string CAddress { get; set; }

        /// <summary>
        /// Gets or sets the Current street.
        /// </summary>
        /// <value>The Current street.</value>
        public string CStreet { get; set; }

        /// <summary>
        /// Gets or sets the Current city.
        /// </summary>
        /// <value>The Current city.</value>
        public string CCity { get; set; }

        /// <summary>
        /// Gets or sets the Current pin code.
        /// </summary>
        /// <value>The Current pin code.</value>
        public string CPinCode { get; set; }

        /// <summary>
        /// Gets or sets the permanent address.
        /// </summary>
        /// <value>The permanent address.</value>
        public string PAddress { get; set; }

        /// <summary>
        /// Gets or sets the permanent street.
        /// </summary>
        /// <value>The permanent street.</value>
        public string PStreet { get; set; }

        /// <summary>
        /// Gets or sets the permanent city.
        /// </summary>
        /// <value>The permanent city.</value>
        public string PCity { get; set; }

        /// <summary>
        /// Gets or sets the permanent pin code.
        /// </summary>
        /// <value>The permanent pin code.</value>
        public string PPinCode { get; set; }

        /// <summary>
        /// Gets or sets the resignation date.
        /// </summary>
        /// <value>The resignation date.</value>
        public DateTime ResignationDate { get; set; }

        /// <summary>
        /// Gets or sets the resignation reason.
        /// </summary>
        /// <value>The resignation reason.</value>
        public string ResignationReason { get; set; }

        /// <summary>
        /// Gets or sets the last working day.
        /// </summary>
        /// <value>The last working day.</value>
        public DateTime LastWorkingDay { get; set; }

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
        /// Gets or sets the recruiter id.
        /// </summary>
        /// <value>The recruiter id.</value>
        public int RecruiterId { get; set; }

        /// <summary>
        /// Gets or sets the interviewer id.
        /// </summary>
        /// <value>The interviewer id.</value>
        public int InterviewerId { get; set; }

        /// <summary>
        /// Gets or sets the Type.
        /// </summary>
        /// <value>The Type.</value>
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the Prefix of Employee.
        /// </summary>
        /// <value>The Type.</value>
        public int Prefix { get; set; }

        /// <summary>
        /// Gets or sets the ProjectName of Employee.
        /// </summary>
        /// <value>The ProjectName.</value>
        public string ProjectName { get; set; }


        /// <summary>
        /// Gets or sets the ProjectJoiningDate of Employee.
        /// </summary>
        /// <value>The ProjectName.</value>
        public string ProjectJoiningDate { get; set; }


        /// <summary>
        /// Gets or sets the ClientName of Employee.
        /// </summary>
        /// <value>The ClientName.</value>
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the Client Count of Employee.
        /// </summary>
        /// <value>The ClientName.</value>
        public int ClientCount { get; set; }

        /// <summary>
        /// Gets or sets the Designation of Employee.
        /// </summary>
        /// <value>The Designation.</value>
        public string Designation { get; set; }

        /// <summary>
        /// Gets or sets the Role of Employee.
        /// </summary>
        /// <value>The Role.</value>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the EmployeeType of Employee.
        /// </summary>
        /// <value>The Role.</value>
        public string EmployeeType { get; set; }

        /// <summary>
        /// Gets or sets the FullName of Employee.
        /// </summary>
        /// <value>The FullName.</value>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the MRFId of Employee.
        /// </summary>
        /// <value>The MRFId.</value>
        public int MRFId { get; set; }

        /// <summary>
        /// Gets or sets the FileName of Employee.
        /// </summary>
        /// <value>The FileName.</value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the Department of Employee.
        /// </summary>
        /// <value>The Department.</value>
        public string Department { get; set; }

       
        /// <summary>
        /// Gets or sets the ProjectStartDate of Employee.
        /// </summary>
        /// <value>The ProjectStartDate.</value>
        public DateTime ProjectStartDate { get; set; }

        /// <summary>
        /// Gets or sets the Billing of Employee.
        /// </summary>
        /// <value>The Billing.</value>
        public int Billing { get; set; }

        /// <summary>
        /// Gets or sets the Utilization of Employee.
        /// </summary>
        /// <value>The Utilization.</value>
        public int Utilization { get; set; }

        /// <summary>
        /// Gets or sets the ProjectStartDate of Employee.
        /// </summary>
        /// <value>The ProjectStartDate.</value>
        public DateTime ProjectReleaseDate { get; set; }

        /// <summary>
        /// Gets or sets the EmpProjectAllocationId of Employee.
        /// </summary>
        /// <value>The EmpProjectAllocationId.</value>
        public int EmpProjectAllocationId { get; set; }
        
        /// <summary>
        /// Gets or sets the ReportingToId of Employee.
        /// </summary>
        /// <value>The ReportingToId.</value>
        public string ReportingToId { get; set; }

        /// <summary>
        /// Gets or sets the ReportingTo of Employee.
        /// </summary>
        /// <value>The ReportingTo.</value>
        public string ReportingTo { get; set; }

        /// <summary>
        /// Gets or sets the EmpResourceRelease status of Employee.
        /// </summary>
        /// <value>The EmpProjectAllocationId.</value>
        public int EmpReleasedStatus { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Employee is fresher or not.
        /// </summary>
        /// <value><c>1</c>Fresher, <c>2</c>Experienced.</value>
        public int IsFresher { get; set; }

        /// <summary>
        /// Gets or sets the created by id.
        /// </summary>
        /// <value>The created by id.</value>
        public string CreatedByMailId { get; set; }

        /// <summary>
        /// Gets or sets the last modified by id.
        /// </summary>
        /// <value>The last modified by id.</value>
        public string LastModifiedByMailId { get; set; }


        /// <summary>
        /// Gets or sets the project end date.
        /// </summary>
        /// <value>The project end date.</value>
        public DateTime ProjectEndDate { get; set; }


        /// <summary>
        /// Gets or sets the project id.
        /// </summary>
        /// <value>The project id.</value>
        public int ProjectId { get; set; }

        /// <summary>
        /// Get External Work Experience.
        /// </summary>
        public string ExternalWorkExp { get; set; }

       
        /// <summary>
        /// Get Resource Bussiness Unit.
        /// </summary>
        public int ResourceBussinessUnit { get; set; }

        /// <summary>
        /// Gets or sets the reason for extension.
        /// </summary>
        /// <value>The reason for extension.</value>
        public string ReasonForExtension { get; set; }


        /// <summary>
        /// Gets or sets the reporting to FM id.
        /// </summary>
        /// <value>The reporting to FM id.</value>
        public int ReportingToFMId { get; set; }

        /// <summary>
        /// Gets or sets the reporting to FM.
        /// </summary>
        /// <value>The reporting to FM.</value>
        public string ReportingToFM { get; set; }

        /// <summary>
        /// Gets or sets the primary skills
        /// </summary>
        /// <value>The reporting to FM.</value>
        public string EmployeePrimarySkill { get; set; }

        /// <summary>
        /// Gets or sets the total work exp
        /// </summary>
        public string TotalWorkExperience { get; set; }

        /// <summary>
        /// Gets or sets the internal work exp
        /// </summary>
        public string InternalWorkExperience { get; set; }

        // Rakesh : Issue 58054: 23/May/2016 : Starts 

        /// <summary>
        /// Get External Work ExternalWorkExpInMonths.
        /// </summary>
        public string ExternalWorkExpInMonths { get; set; }

        //Rakesh : Department head HOD, Project wise HOD 15/July/2016 Begin   
        public string ProjectHeadName { get; set; }
        //Rakesh : Department head HOD, Project wise HOD 15/July/2016 End


        /// <summary>
        /// Gets or sets the total work exp in months
        /// </summary>
        public string TotalWorkExperienceInMonths { get; set; }

        /// <summary>
        /// Gets or sets the internal work exp in months
        /// </summary>
        public string InternalWorkExperienceInMonths { get; set; }


        //END


        /// <summary>
        /// Gets or sets the bandname
        /// </summary>
        public string BandName { get; set; }

        /// <summary>
        /// Gets or sets the no of days billed
        /// </summary>
        public double NoOfDaysBilled { get; set; }

        /// <summary>
        /// Gets or sets the no of days utilised
        /// </summary>
        public double NoOfDaysUtilised { get; set; }

        /// <summary>
        /// Gets or sets the total no of days billed
        /// </summary>
        public double TotalNoOfDaysBilled { get; set; }

        /// <summary>
        /// Gets or sets the total no of days utilised
        /// </summary>
        public double TotalNoOfDaysUtilised { get; set; }

        /// <summary>
        /// Gets or sets the Project Count.
        /// </summary>
        /// <value>The Project Count.</value>
        public int ProjectCount { get; set; }

        /// <summary>
        /// Gets or sets the Location.
        /// </summary>
        /// <value>The Location.</value>
        public string Location { get; set; }


        /// <summary>
        /// Gets or sets the PLocation
        /// </summary>
        /// <value>The Location.</value>
        public string EmpLocation { get; set; }

        /// <summary>
        /// Gets or sets the Prefix Name of Employee.
        /// </summary>
        /// <value>The Type.</value>
        public string PrefixName { get; set; }

        /// <summary>
        /// Gets or sets the MR fcode.
        /// </summary>
        /// <value>The MR fcode.</value>
        public string MRFcode { get; set; }

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
        /// Gets or sets the Utilization change date
        /// </summary>
        /// <value>The Utilization And Billing change date</value>
        public DateTime UtilizationAndBilling { get; set; }

        /// <summary>
        /// Gets or sets the Billing change date
        /// </summary>
        /// <value>The Billing change date</value>
        public DateTime BillingChangeDate { get; set; }

        /// <summary>
        /// Gets or sets the Resource Billing Date
        /// </summary>
        /// <value>The Billing change date</value>
        public DateTime ResourceBillingDate { get; set; }

        /// <summary>
        /// Gets or sets the project code.
        /// </summary>
        /// <value>The project code.</value>
        public string ProjectCode { get; set; }

        //Mohamed : Issue 39509/41062 : 07/03/2013 : Starts                        			  
        //Desc :  Adding new Columns date for Probation,Designation and Departement

        /// <summary>
        /// Gets or sets the Departement Change Date .
        /// </summary>
        /// <value>The Departement Change Date.</value>
        public string DepartementChangeDate { get; set; }

        /// <summary>
        /// Gets or sets the Designation Change Date .
        /// </summary>
        /// <value>The Designation Change Date .</value>
        public string DesignationChangeDate { get; set; }

        /// <summary>
        /// Gets or sets the Probation Date .
        /// </summary>
        /// <value>The Probation Date.</value>
        public string ConfirmedDate { get; set; }

        /// <summary>
        /// Gets or sets the Probation Flag.
        /// </summary>
        /// <value>The Probation Flag.</value>
        public bool ProbationFlag { get; set; }
        

        //Mohamed : Issue 39509/41062 : 07/03/2013 : Ends

        //Mahendra : Going google 26-Jun-2013

        /// <summary>
        /// Gets or sets the username .
        /// </summary>
        /// <value>The Probation Flag.</value>
        public string WindowsUserName { get; set; }

        //Ishwar Patil 30092014 For NIS : Start
        /// <summary>
        /// Gets or sets the RMS employee.
        /// </summary>
        public string IsRMSEmp { get; set; }
        //Ishwar Patil 30092014 For NIS : End

        //Umesh: NIS-changes: Head Count Report Starts

        /// <summary>
        /// Gets or sets the Cost Code.
        /// </summary>
        public string CostCode { get; set; }

        /// <summary>
        /// Gets or sets the Resource Type.
        /// </summary>
        public string ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the Resource Type Count.
        /// </summary>
        public string ResourceTypeCount { get; set; }

        /// <summary>
        /// Gets or sets the Project object.
        /// </summary>
        public Projects Projects { get; set; }

        //Umesh: NIS-changes: Head Count Report Starts

       

        //Siddharth 9th June 2015 Start
        /// <summary>
        /// Gets or sets the BPSSVersion.
        /// </summary>
        /// <value>The BPSSVersion.</value>
        public string BPSSVersion { get; set; }

        /// <summary>
        /// Gets or sets the BPSSCompletionDate.
        /// </summary>
        /// <value>The BPSSCompletionDate.</value>
        public string BPSSCompletionDate { get; set; }
        //Siddharth 9th June 2015 End


        //Siddharth 22nd June 2015 Start
        /// <summary>
        /// Gets or sets the BPSSVersion.
        /// </summary>
        /// <value>The BPSSVersion.</value>
        public string BusinessVertical { get; set; }
        //Siddharth 22nd June 2015 End

        //Siddharth 13th Oct 2015 Start
        /// <summary>
        /// Gets or sets the Domains.
        /// </summary>
        /// <value>The Domain.</value>
        public string Domain { get; set; }
        //Siddharth 13th Oct 2015 End


        //Siddharth  17 Nov 2015 Start
        /// <summary>
        /// Gets or sets the Domains.
        /// </summary>
        /// <value>The Domain.</value>
        public string RBU { get; set; }
        //Siddharth  17 Nov 2015 End

        //Siddhesh Arekar 08/07/2015 Start
        /// <summary>
        /// Gets or sets the LoginRole.
        /// </summary>
        /// <value>LoginRole</value>
        public string LoginRole { get; set; }
        //Siddhesh Arekar 08/07/2015 End


        //Siddhesh Arekar Domain Details 10082015 Start
        /// <summary>
        /// Gets or sets the Domain of Employee.
        /// </summary>
        /// <value>The EmployeeDomain.</value>
        public string EmployeeDomain { get; set; }


        /// <summary>
        /// Gets or sets the Domain of Employee Name.
        /// </summary>
        /// <value>The EmployeeDomainName.</value>
        public string EmployeeDomainName { get; set; }
        //Siddhesh Arekar Domain Details 10082015 End

        public string LineManagerEmailId { get; set; }


        #endregion Properties
    }
}
