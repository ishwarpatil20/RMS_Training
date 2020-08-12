//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           Employee.cs       
//  Author:         sudip.guha
//  Date written:   14/08/2009/ 06:17:55 PM
//  Description:    This class  provides the Data Access layer methods for Employee module.
//                  
//
//  Amendments
//  Date                        Who             Ref     Description
//  ----                        -----------     ---     -----------
//  14/08/2009/ 06:17:55 PM     sudip.guha      n/a     Created  
//  18/08/2009/ 11:07:29 PM     vineet.kulkarni n/a     Used DataAccessClass,Added Delete() Get()  
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Common;
using Common.Constants;
using System.Transactions;
using BusinessEntities;

namespace Rave.HR.DataAccessLayer.Employees
{
    /// <summary>
    /// This class provide methods to Add, Update, Retrive, Delete Employee details
    /// </summary>
    public class Employee
    {
        #region Private Member Variables

        /// <summary>
        /// private variable for Data Access Class
        /// </summary>
        private DataAccessClass objDA;

        /// <summary>
        /// private array variable for Sql paramaters
        /// </summary>
        private SqlParameter[] sqlParam;

        private static BusinessEntities.RaveHRCollection raveHRCollection;
        /// <summary>
        /// private variable for Data Reader class
        /// </summary>
        private SqlDataReader objDataReader;

        /// <summary>
        /// private variable for Employee
        /// </summary>
        private BusinessEntities.Employee objEmployee;

        /// <summary>
        /// private variable for Projects
        /// </summary>
        private BusinessEntities.Projects objProjects;

        /// <summary>
        /// private List variable for Employees
        /// </summary>
        private List<BusinessEntities.Employee> lstEmployees;

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string GETPROJECTMANAGERSEMAILID = "GetProjectManagersEmailId";

        #endregion Private Member Variables

        #region Constants

        /// <summary>
        /// private variable for Class Name used in each catch block
        /// </summary>
        private string CLASS_NAME = "Employee";

        /// <summary>
        /// Function name : AddEmployee
        /// </summary>
        private const string ADDEMPLOYEE = "AddEmployee";

        /// <summary>
        /// Function name : UpdateEmployee
        /// </summary>
        private const string UPDATEEMPLOYEE = "UpdateEmployee";

        /// <summary>
        /// Function name : GetEmployee
        /// </summary>
        private const string GETEMPLOYEE = "GetEmployee";

        /// <summary>
        /// Function name : DeleteEmployee
        /// </summary>
        private const string DELETEEMPLOYEE = "DeleteEmployee";

        /// <summary>
        /// Function name : GetEmployeeByEmailId
        /// </summary>
        private const string GETEMPLOYEEBYEMAILID = "GetEmployeeByEmailId";

        /// <summary>
        /// Function name : GetEmployeeList
        /// </summary>
        private const string GETEMPLOYEELIST = "GetEmployeeList";

        /// <summary>
        /// Function name : GetMRFCode
        /// </summary>
        private const string GETMRFCODE = "GetMRFCode";

        private const string GETPROJECTFOREMPLOYEE = "GetProjectForEmployee";

        /// <summary>
        /// Function name : UpdateEmployeeMRFCode
        /// </summary>
        private const string UPDATEEMPLOYEEMRFCODE = "UpdateEmployeeMRFCode";

        /// <summary>
        /// Function name : GetEmployeesSummary
        /// </summary>
        private const string GETEMPLOYEESSUMMARY = "GetEmployeesSummary";

        /// <summary>
        /// Function name : GetProjectName
        /// </summary>
        private const string GETPROJECTNAME = "GetProjectName";

        /// <summary>
        /// Function name : GetEmployeeReportingToName
        /// </summary>
        private const string GETEMPLOYEEREPORTINGTONAME = "GetEmployeeReportingToName";

        /// <summary>
        /// Function name : UpdateEmployeeReleaseStatus
        /// </summary>
        private const string UPDATEEMPLOYEERELEASESTATUS = "UpdateEmployeeReleaseStatus";

        // CR - 25715 Sachin Start
        /// <summary>
        /// Function name : CheckLastEmployeeRelease
        /// </summary>
        private const string CHECKLASTEMPLOYEERELEASE = "CheckLastEmployeeRelease";
        // CR - 25715 Sachin Start

        /// <summary>
        /// Function name : UpdateEmployeeResignationDetails
        /// </summary>
        private const string UPDATEEMPLOYEERESIGNATIONDETAILS = "UpdateEmployeeResignationDetails";

        #endregion Constants

        #region Public Member Functions

        /// <summary>
        /// Adds the employee.
        /// </summary>
        /// <param name="objAddEmployee">The object add employee.</param>
        public int AddEmployee(BusinessEntities.Employee objAddEmployee, ref string empCode)
        {
            int empID = 0;
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[61];

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    sqlParam[0] = new SqlParameter(SPParameter.EMPCode, SqlDbType.NChar, 10);
                    if (objAddEmployee.EMPCode == "" || objAddEmployee.EMPCode == null)
                        sqlParam[0].Value = DBNull.Value;
                    else
                        sqlParam[0].Value = objAddEmployee.EMPCode;

                    sqlParam[1] = new SqlParameter(SPParameter.FirstName, SqlDbType.NVarChar, 50);
                    if (objAddEmployee.FirstName == "" || objAddEmployee.FirstName == null)
                        sqlParam[1].Value = DBNull.Value;
                    else
                        sqlParam[1].Value = objAddEmployee.FirstName;

                    sqlParam[2] = new SqlParameter(SPParameter.MiddleName, SqlDbType.NVarChar, 50);
                    if (objAddEmployee.MiddleName == "" || objAddEmployee.MiddleName == null)
                        sqlParam[2].Value = DBNull.Value;
                    else
                        sqlParam[2].Value = objAddEmployee.MiddleName;

                    sqlParam[3] = new SqlParameter(SPParameter.LastName, SqlDbType.NVarChar, 50);
                    if (objAddEmployee.LastName == "" || objAddEmployee.LastName == null)
                        sqlParam[3].Value = DBNull.Value;
                    else
                        sqlParam[3].Value = objAddEmployee.LastName;

                    sqlParam[4] = new SqlParameter(SPParameter.EMPPicture, SqlDbType.Image);
                    if (objAddEmployee.EMPPicture == null)
                        sqlParam[4].Value = DBNull.Value;
                    else
                        sqlParam[4].Value = objAddEmployee.EMPPicture;

                    sqlParam[5] = new SqlParameter(SPParameter.EmailId, SqlDbType.NVarChar, 50);
                    if (objAddEmployee.EmailId == "" || objAddEmployee.EmailId == null)
                        sqlParam[5].Value = DBNull.Value;
                    else
                        sqlParam[5].Value = objAddEmployee.EmailId;

                    sqlParam[6] = new SqlParameter(SPParameter.StatusId, SqlDbType.Int);
                    if (objAddEmployee.StatusId == 0)
                        sqlParam[6].Value = 0;
                    else
                        sqlParam[6].Value = objAddEmployee.StatusId;

                    sqlParam[7] = new SqlParameter(SPParameter.GroupId, SqlDbType.Int);
                    if (objAddEmployee.GroupId == 0)
                        sqlParam[7].Value = 0;
                    else
                        sqlParam[7].Value = objAddEmployee.GroupId;

                    sqlParam[8] = new SqlParameter(SPParameter.DesignationId, SqlDbType.Int);
                    if (objAddEmployee.DesignationId == 0)
                        sqlParam[8].Value = 0;
                    else
                        sqlParam[8].Value = objAddEmployee.DesignationId;

                    sqlParam[9] = new SqlParameter(SPParameter.RoleId, SqlDbType.Int);
                    if (objAddEmployee.RoleId == 0)
                        sqlParam[9].Value = 0;
                    else
                        sqlParam[9].Value = objAddEmployee.RoleId;

                    sqlParam[10] = new SqlParameter(SPParameter.Band, SqlDbType.Int);
                    if (objAddEmployee.Band == 0)
                        sqlParam[10].Value = 0;
                    else
                        sqlParam[10].Value = objAddEmployee.Band;

                    sqlParam[11] = new SqlParameter(SPParameter.JoiningDate, SqlDbType.SmallDateTime);
                    if (objAddEmployee.JoiningDate == null || objAddEmployee.JoiningDate == DateTime.MinValue)
                        sqlParam[11].Value = DBNull.Value;
                    else
                        sqlParam[11].Value = objAddEmployee.JoiningDate;

                    sqlParam[12] = new SqlParameter(SPParameter.DOB, SqlDbType.SmallDateTime);
                    if (objAddEmployee.DOB == null || objAddEmployee.DOB == DateTime.MinValue)
                        sqlParam[12].Value = DBNull.Value;
                    else
                        sqlParam[12].Value = objAddEmployee.DOB;

                    sqlParam[13] = new SqlParameter(SPParameter.Gender, SqlDbType.NChar, 6);
                    if (objAddEmployee.Gender == "" || objAddEmployee.Gender == null)
                        sqlParam[13].Value = DBNull.Value;
                    else
                        sqlParam[13].Value = objAddEmployee.Gender;

                    sqlParam[14] = new SqlParameter(SPParameter.MaritalStatus, SqlDbType.NChar, 10);
                    if (objAddEmployee.MaritalStatus == "" || objAddEmployee.MaritalStatus == null)
                        sqlParam[14].Value = DBNull.Value;
                    else
                        sqlParam[14].Value = objAddEmployee.MaritalStatus;

                    sqlParam[15] = new SqlParameter(SPParameter.FatherName, SqlDbType.NVarChar, 100);
                    if (objAddEmployee.FatherName == "" || objAddEmployee.FatherName == null)
                        sqlParam[15].Value = DBNull.Value;
                    else
                        sqlParam[15].Value = objAddEmployee.FatherName;

                    sqlParam[16] = new SqlParameter(SPParameter.SpouseName, SqlDbType.NVarChar, 100);
                    if (objAddEmployee.FatherName == "" || objAddEmployee.FatherName == null)
                        sqlParam[16].Value = DBNull.Value;
                    else
                        sqlParam[16].Value = objAddEmployee.SpouseName;

                    sqlParam[17] = new SqlParameter(SPParameter.BloodGroup, SqlDbType.NChar, 5);
                    if (objAddEmployee.BloodGroup == "" || objAddEmployee.BloodGroup == null)
                        sqlParam[17].Value = DBNull.Value;
                    else
                        sqlParam[17].Value = objAddEmployee.BloodGroup;

                    sqlParam[18] = new SqlParameter(SPParameter.EmergencyContactNo, SqlDbType.NChar, 10);
                    if (objAddEmployee.EmergencyContactNo == "" || objAddEmployee.EmergencyContactNo == null)
                        sqlParam[18].Value = DBNull.Value;
                    else
                        sqlParam[18].Value = objAddEmployee.EmergencyContactNo;

                    sqlParam[19] = new SqlParameter(SPParameter.PassportNo, SqlDbType.NChar, 10);
                    if (objAddEmployee.PassportNo == "" || objAddEmployee.PassportNo == null)
                        sqlParam[19].Value = DBNull.Value;
                    else
                        sqlParam[19].Value = objAddEmployee.PassportNo;

                    sqlParam[20] = new SqlParameter(SPParameter.PassportIssueDate, SqlDbType.SmallDateTime);
                    if (objAddEmployee.PassportIssueDate == null || objAddEmployee.PassportIssueDate == DateTime.MinValue)
                        sqlParam[20].Value = DBNull.Value;
                    else
                        sqlParam[20].Value = objAddEmployee.PassportIssueDate;

                    sqlParam[21] = new SqlParameter(SPParameter.PassportExpireDate, SqlDbType.SmallDateTime);
                    if (objAddEmployee.PassportExpireDate == null || objAddEmployee.PassportExpireDate == DateTime.MinValue)
                        sqlParam[21].Value = DBNull.Value;
                    else
                        sqlParam[21].Value = objAddEmployee.PassportExpireDate;

                    sqlParam[22] = new SqlParameter(SPParameter.PassportIssuePlace, SqlDbType.NChar, 10);
                    if (objAddEmployee.PassportIssuePlace == "" || objAddEmployee.PassportIssuePlace == null)
                        sqlParam[22].Value = DBNull.Value;
                    else
                        sqlParam[22].Value = objAddEmployee.PassportIssuePlace;

                    sqlParam[23] = new SqlParameter(SPParameter.ReadyToRelocateIndia, SqlDbType.Bit);
                    sqlParam[23].Value = objAddEmployee.ReadyToRelocateIndia;

                    sqlParam[24] = new SqlParameter(SPParameter.ReasonNotToRelocateIndia, SqlDbType.NVarChar, 100);
                    if (objAddEmployee.ReasonNotToRelocateIndia == "" || objAddEmployee.ReasonNotToRelocateIndia == null)
                        sqlParam[24].Value = DBNull.Value;
                    else
                        sqlParam[24].Value = objAddEmployee.ReasonNotToRelocateIndia;

                    sqlParam[25] = new SqlParameter(SPParameter.ReadytoRelocate, SqlDbType.Bit);
                    sqlParam[25].Value = objAddEmployee.ReadyToRelocate;

                    sqlParam[26] = new SqlParameter(SPParameter.ReasonNotToRelocate, SqlDbType.NVarChar, 100);
                    if (objAddEmployee.ReasonNotToRelocate == "" || objAddEmployee.ReasonNotToRelocate == null)
                        sqlParam[26].Value = DBNull.Value;
                    else
                        sqlParam[26].Value = objAddEmployee.ReasonNotToRelocate;

                    sqlParam[27] = new SqlParameter(SPParameter.Duration, SqlDbType.NVarChar, 10);
                    if (objAddEmployee.Duration == "" || objAddEmployee.Duration == null)
                        sqlParam[27].Value = DBNull.Value;
                    else
                        sqlParam[27].Value = objAddEmployee.Duration;

                    sqlParam[28] = new SqlParameter(SPParameter.ResidencePhone, SqlDbType.NChar, 10);
                    if (objAddEmployee.ResidencePhone == "" || objAddEmployee.ResidencePhone == null)
                        sqlParam[28].Value = DBNull.Value;
                    else
                        sqlParam[28].Value = objAddEmployee.ResidencePhone;

                    sqlParam[29] = new SqlParameter(SPParameter.MobileNo, SqlDbType.NChar, 10);
                    if (objAddEmployee.MobileNo == "" || objAddEmployee.MobileNo == null)
                        sqlParam[29].Value = DBNull.Value;
                    else
                        sqlParam[29].Value = objAddEmployee.MobileNo;

                    sqlParam[30] = new SqlParameter(SPParameter.CAddress, SqlDbType.NVarChar, 50);
                    if (objAddEmployee.CAddress == "" || objAddEmployee.CAddress == null)
                        sqlParam[30].Value = DBNull.Value;
                    else
                        sqlParam[30].Value = objAddEmployee.CAddress;

                    sqlParam[31] = new SqlParameter(SPParameter.CStreet, SqlDbType.NVarChar, 50);
                    if (objAddEmployee.CStreet == "" || objAddEmployee.CStreet == null)
                        sqlParam[31].Value = DBNull.Value;
                    else
                        sqlParam[31].Value = objAddEmployee.CStreet;

                    sqlParam[32] = new SqlParameter(SPParameter.CCity, SqlDbType.NVarChar, 50);
                    if (objAddEmployee.CCity == "" || objAddEmployee.CCity == null)
                        sqlParam[32].Value = DBNull.Value;
                    else
                        sqlParam[32].Value = objAddEmployee.CCity;

                    sqlParam[33] = new SqlParameter(SPParameter.CPinCode, SqlDbType.NChar, 6);
                    if (objAddEmployee.CPinCode == "" || objAddEmployee.CPinCode == null)
                        sqlParam[33].Value = DBNull.Value;
                    else
                        sqlParam[33].Value = objAddEmployee.CPinCode;

                    sqlParam[34] = new SqlParameter(SPParameter.PAddress, SqlDbType.NVarChar, 50);
                    if (objAddEmployee.PAddress == "" || objAddEmployee.PAddress == null)
                        sqlParam[34].Value = DBNull.Value;
                    else
                        sqlParam[34].Value = objAddEmployee.PAddress;

                    sqlParam[35] = new SqlParameter(SPParameter.PStreet, SqlDbType.NVarChar, 50);
                    if (objAddEmployee.PAddress == "" || objAddEmployee.PAddress == null)
                        sqlParam[35].Value = DBNull.Value;
                    else
                        sqlParam[35].Value = objAddEmployee.PAddress;

                    sqlParam[36] = new SqlParameter(SPParameter.PCity, SqlDbType.NVarChar, 50);
                    if (objAddEmployee.PCity == "" || objAddEmployee.PCity == null)
                        sqlParam[36].Value = DBNull.Value;
                    else
                        sqlParam[36].Value = objAddEmployee.PCity;

                    sqlParam[37] = new SqlParameter(SPParameter.PPinCode, SqlDbType.NChar, 6);
                    if (objAddEmployee.PPinCode == "" || objAddEmployee.PPinCode == null)
                        sqlParam[37].Value = DBNull.Value;
                    else
                        sqlParam[37].Value = objAddEmployee.PPinCode;

                    sqlParam[38] = new SqlParameter(SPParameter.ResignationDate, SqlDbType.SmallDateTime);
                    if (objAddEmployee.ResignationDate == null || objAddEmployee.ResignationDate == DateTime.MinValue)
                        sqlParam[38].Value = DBNull.Value;
                    else
                        sqlParam[38].Value = objAddEmployee.ResignationDate;

                    sqlParam[39] = new SqlParameter(SPParameter.ResignationReason, SqlDbType.NVarChar, 100);
                    if (objAddEmployee.ResignationReason == "" || objAddEmployee.ResignationReason == null)
                        sqlParam[39].Value = DBNull.Value;
                    else
                        sqlParam[39].Value = objAddEmployee.ResignationReason;

                    sqlParam[40] = new SqlParameter(SPParameter.LastWorkingDay, SqlDbType.SmallDateTime);
                    if (objAddEmployee.LastWorkingDay == null || objAddEmployee.LastWorkingDay == DateTime.MinValue)
                        sqlParam[40].Value = DBNull.Value;
                    else
                        sqlParam[40].Value = objAddEmployee.LastWorkingDay;

                    sqlParam[41] = new SqlParameter(SPParameter.CreatedById, SqlDbType.NVarChar, 50);
                    if (objAddEmployee.CreatedByMailId == null || objAddEmployee.CreatedByMailId == string.Empty)
                        sqlParam[41].Value = DBNull.Value;
                    else
                        sqlParam[41].Value = objAddEmployee.CreatedByMailId;

                    sqlParam[42] = new SqlParameter(SPParameter.CreatedDate, SqlDbType.DateTime);
                    if (objAddEmployee.CreatedDate == null || objAddEmployee.CreatedDate == DateTime.MinValue)
                        sqlParam[42].Value = DBNull.Value;
                    else
                        sqlParam[42].Value = objAddEmployee.CreatedDate;

                    sqlParam[43] = new SqlParameter(SPParameter.LastModifiedById, SqlDbType.NVarChar, 50);
                    if (objAddEmployee.LastModifiedByMailId == null || objAddEmployee.LastModifiedByMailId == string.Empty)
                        sqlParam[43].Value = DBNull.Value;
                    else
                        sqlParam[43].Value = objAddEmployee.LastModifiedByMailId;

                    sqlParam[44] = new SqlParameter(SPParameter.LastModifiedDate, SqlDbType.DateTime);
                    if (objAddEmployee.LastModifiedDate == null || objAddEmployee.LastModifiedDate == DateTime.MinValue)
                        sqlParam[44].Value = DBNull.Value;
                    else
                        sqlParam[44].Value = objAddEmployee.LastModifiedDate;

                    sqlParam[45] = new SqlParameter(SPParameter.RecruiterId, SqlDbType.Int);
                    if (objAddEmployee.RecruiterId == 0)
                        sqlParam[45].Value = 0;
                    else
                        sqlParam[45].Value = objAddEmployee.RecruiterId;

                    sqlParam[46] = new SqlParameter(SPParameter.InterviewerId, SqlDbType.Int);
                    if (objAddEmployee.InterviewerId == 0)
                        sqlParam[46].Value = 0;
                    else
                        sqlParam[46].Value = objAddEmployee.InterviewerId;

                    sqlParam[47] = new SqlParameter(SPParameter.Type, SqlDbType.Int);
                    if (objAddEmployee.Type == 0)
                        sqlParam[47].Value = 0;
                    else
                        sqlParam[47].Value = objAddEmployee.Type;

                    sqlParam[48] = new SqlParameter(SPParameter.Prefix, SqlDbType.Int);
                    if (objAddEmployee.Type == 0)
                        sqlParam[48].Value = 0;
                    else
                        sqlParam[48].Value = objAddEmployee.Prefix;

                    sqlParam[49] = new SqlParameter(SPParameter.MRFId, SqlDbType.Int);
                    if (objAddEmployee.MRFId == 0)
                        sqlParam[49].Value = 0;
                    else
                        sqlParam[49].Value = objAddEmployee.MRFId;

                    sqlParam[50] = new SqlParameter(SPParameter.FileName, SqlDbType.NVarChar, 100);
                    if (objAddEmployee.FileName == "" || objAddEmployee.FileName == null)
                        sqlParam[50].Value = DBNull.Value;
                    else
                        sqlParam[50].Value = objAddEmployee.FileName;

                    sqlParam[51] = new SqlParameter(SPParameter.ReportingToId, SqlDbType.NVarChar, 50);
                    if (objAddEmployee.ReportingToId == "" || objAddEmployee.ReportingToId == null)
                        sqlParam[51].Value = DBNull.Value;
                    else
                        sqlParam[51].Value = objAddEmployee.ReportingToId;

                    sqlParam[52] = new SqlParameter("@OutEmpId", SqlDbType.Int);
                    sqlParam[52].Value = 0;
                    sqlParam[52].Direction = ParameterDirection.Output;

                    sqlParam[53] = new SqlParameter("@OutEmpCode", SqlDbType.NVarChar, 10);
                    sqlParam[53].Value = 0;
                    sqlParam[53].Direction = ParameterDirection.Output;

                    sqlParam[54] = new SqlParameter(SPParameter.IsFresher, SqlDbType.Int);
                    if (objAddEmployee.IsFresher == 0)
                        sqlParam[54].Value = DBNull.Value;
                    else
                        sqlParam[54].Value = objAddEmployee.IsFresher;

                    sqlParam[55] = new SqlParameter(SPParameter.ResourceBussinessUnit, SqlDbType.Int);
                    if (objAddEmployee.ResourceBussinessUnit == 0)
                        sqlParam[55].Value = DBNull.Value;
                    else
                        sqlParam[55].Value = objAddEmployee.ResourceBussinessUnit;

                    sqlParam[56] = new SqlParameter(SPParameter.ReportingToFM, SqlDbType.Int);
                    if (objAddEmployee.ReportingToFMId == 0)
                        sqlParam[56].Value = DBNull.Value;
                    else
                        sqlParam[56].Value = objAddEmployee.ReportingToFMId;

                    sqlParam[57] = new SqlParameter(SPParameter.Location, SqlDbType.NChar, 20);
                    if (objAddEmployee.EmpLocation == "")
                        sqlParam[57].Value = DBNull.Value;
                    else
                        sqlParam[57].Value = objAddEmployee.EmpLocation;

                    sqlParam[58] = new SqlParameter(SPParameter.ExperienceInMonth, objAddEmployee.RelavantExperienceMonth);

                    sqlParam[59] = new SqlParameter(SPParameter.ExperienceInYear, objAddEmployee.RelevantExperienceYear);

                    sqlParam[60] = new SqlParameter(SPParameter.WindowsUsername, SqlDbType.NVarChar, 100);
                    if (objAddEmployee.WindowsUserName == "")
                        sqlParam[60].Value = DBNull.Value;
                    else
                        sqlParam[60].Value = objAddEmployee.WindowsUserName;

                    int row = objDA.ExecuteNonQuerySP(SPNames.Employee_AddEmployee, sqlParam);

                    empID = Convert.ToInt32(sqlParam[52].Value.ToString());
                    empCode = sqlParam[53].Value.ToString();

                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, ADDEMPLOYEE, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
            return empID;
        }

        /// <summary>
        /// Updates the employee.
        /// </summary>
        /// <param name="objUpdateEmployee">The object update employee.</param>
        public void UpdateEmployee(BusinessEntities.Employee objUpdateEmployee, Boolean IsEmployeeDetailsModified)
        {
            objDA = new DataAccessClass();

            // CR - 28321 Passport Application Number Sachin Start
            // Changed Parameter size from 56 to 57

            //Mohamed : Issue 39509/41062 : 08/03/2013 : Starts                        			  
            //Desc :  Adding new Columns date for Probation,Designation and Departement
            // Changed Parameter size from 57 to 61

            //Siddharth 9th June 2015 Start
            //Desc :  Adding new Columns for BPSSVersion and BPSSCompletionDate
            // Changed Parameter size from 61 to 63


            //Siddharth 25th August 2015 Start
            //Desc :  Adding new Column for Resource Business Unit
            // Changed Parameter size from 65 to 66
            sqlParam = new SqlParameter[66];

            //Siddharth 25th August 2015 End

            //Siddharth 9th June 2015 End
            //Mohamed : Issue 39509/41062 : 08/03/2013 : Ends
            // CR - 28321 Passport Application Number Sachin End

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    sqlParam[0] = new SqlParameter(SPParameter.EMPCode, SqlDbType.NChar, 10);
                    if (objUpdateEmployee.EMPCode == "" || objUpdateEmployee.EMPCode == null)
                        sqlParam[0].Value = DBNull.Value;
                    else
                        sqlParam[0].Value = objUpdateEmployee.EMPCode;

                    sqlParam[1] = new SqlParameter(SPParameter.FirstName, SqlDbType.NVarChar, 50);
                    if (objUpdateEmployee.FirstName == "" || objUpdateEmployee.FirstName == null)
                        sqlParam[1].Value = DBNull.Value;
                    else
                        sqlParam[1].Value = objUpdateEmployee.FirstName;

                    sqlParam[2] = new SqlParameter(SPParameter.MiddleName, SqlDbType.NVarChar, 50);
                    if (objUpdateEmployee.MiddleName == "" || objUpdateEmployee.MiddleName == null)
                        sqlParam[2].Value = DBNull.Value;
                    else
                        sqlParam[2].Value = objUpdateEmployee.MiddleName;

                    sqlParam[3] = new SqlParameter(SPParameter.LastName, SqlDbType.NVarChar, 50);
                    if (objUpdateEmployee.LastName == "" || objUpdateEmployee.LastName == null)
                        sqlParam[3].Value = DBNull.Value;
                    else
                        sqlParam[3].Value = objUpdateEmployee.LastName;

                    sqlParam[4] = new SqlParameter(SPParameter.EMPPicture, SqlDbType.Image);
                    if (objUpdateEmployee.EMPPicture == null)
                        sqlParam[4].Value = DBNull.Value;
                    else
                        sqlParam[4].Value = objUpdateEmployee.EMPPicture;

                    sqlParam[5] = new SqlParameter(SPParameter.EmailId, SqlDbType.NVarChar, 50);
                    if (objUpdateEmployee.EmailId == "" || objUpdateEmployee.EmailId == null)
                        sqlParam[5].Value = DBNull.Value;
                    else
                        sqlParam[5].Value = objUpdateEmployee.EmailId;

                    sqlParam[6] = new SqlParameter(SPParameter.StatusId, SqlDbType.Int);
                    if (objUpdateEmployee.StatusId == 0)
                        sqlParam[6].Value = 0;
                    else
                        sqlParam[6].Value = objUpdateEmployee.StatusId;

                    sqlParam[7] = new SqlParameter(SPParameter.GroupId, SqlDbType.Int);
                    if (objUpdateEmployee.GroupId == 0)
                        sqlParam[7].Value = 0;
                    else
                        sqlParam[7].Value = objUpdateEmployee.GroupId;

                    sqlParam[8] = new SqlParameter(SPParameter.DesignationId, SqlDbType.Int);
                    if (objUpdateEmployee.DesignationId == 0)
                        sqlParam[8].Value = 0;
                    else
                        sqlParam[8].Value = objUpdateEmployee.DesignationId;

                    sqlParam[9] = new SqlParameter(SPParameter.RoleId, SqlDbType.Int);
                    if (objUpdateEmployee.RoleId == 0)
                        sqlParam[9].Value = 0;
                    else
                        sqlParam[9].Value = objUpdateEmployee.RoleId;

                    sqlParam[10] = new SqlParameter(SPParameter.Band, SqlDbType.Int);
                    if (objUpdateEmployee.Band == 0)
                        sqlParam[10].Value = 0;
                    else
                        sqlParam[10].Value = objUpdateEmployee.Band;

                    sqlParam[11] = new SqlParameter(SPParameter.JoiningDate, SqlDbType.SmallDateTime);
                    if (objUpdateEmployee.JoiningDate == null || objUpdateEmployee.JoiningDate == DateTime.MinValue)
                        sqlParam[11].Value = DBNull.Value;
                    else
                        sqlParam[11].Value = objUpdateEmployee.JoiningDate;

                    sqlParam[12] = new SqlParameter(SPParameter.DOB, SqlDbType.SmallDateTime);
                    if (objUpdateEmployee.DOB == null || objUpdateEmployee.DOB == DateTime.MinValue)
                        sqlParam[12].Value = DBNull.Value;
                    else
                        sqlParam[12].Value = objUpdateEmployee.DOB;

                    sqlParam[13] = new SqlParameter(SPParameter.Gender, SqlDbType.NChar, 6);
                    if (objUpdateEmployee.Gender == "" || objUpdateEmployee.Gender == null)
                        sqlParam[13].Value = DBNull.Value;
                    else
                        sqlParam[13].Value = objUpdateEmployee.Gender;

                    sqlParam[14] = new SqlParameter(SPParameter.MaritalStatus, SqlDbType.NChar, 10);
                    if (objUpdateEmployee.MaritalStatus == "" || objUpdateEmployee.MaritalStatus == null)
                        sqlParam[14].Value = DBNull.Value;
                    else
                        sqlParam[14].Value = objUpdateEmployee.MaritalStatus;

                    sqlParam[15] = new SqlParameter(SPParameter.FatherName, SqlDbType.NVarChar, 100);
                    if (objUpdateEmployee.FatherName == "" || objUpdateEmployee.FatherName == null)
                        sqlParam[15].Value = DBNull.Value;
                    else
                        sqlParam[15].Value = objUpdateEmployee.FatherName;

                    sqlParam[16] = new SqlParameter(SPParameter.SpouseName, SqlDbType.NVarChar, 100);
                    if (objUpdateEmployee.SpouseName == "" || objUpdateEmployee.SpouseName == null)
                        sqlParam[16].Value = DBNull.Value;
                    else
                        sqlParam[16].Value = objUpdateEmployee.SpouseName;

                    sqlParam[17] = new SqlParameter(SPParameter.BloodGroup, SqlDbType.NChar, 5);
                    if (objUpdateEmployee.BloodGroup == "" || objUpdateEmployee.BloodGroup == null)
                        sqlParam[17].Value = DBNull.Value;
                    else
                        sqlParam[17].Value = objUpdateEmployee.BloodGroup;

                    sqlParam[18] = new SqlParameter(SPParameter.EmergencyContactNo, SqlDbType.NChar, 10);
                    if (objUpdateEmployee.EmergencyContactNo == "" || objUpdateEmployee.EmergencyContactNo == null)
                        sqlParam[18].Value = DBNull.Value;
                    else
                        sqlParam[18].Value = objUpdateEmployee.EmergencyContactNo;

                    sqlParam[19] = new SqlParameter(SPParameter.PassportNo, SqlDbType.NChar, 10);
                    if (objUpdateEmployee.PassportNo == "" || objUpdateEmployee.PassportNo == null)
                        sqlParam[19].Value = DBNull.Value;
                    else
                        sqlParam[19].Value = objUpdateEmployee.PassportNo;

                    sqlParam[20] = new SqlParameter(SPParameter.PassportIssueDate, SqlDbType.SmallDateTime);
                    if (objUpdateEmployee.PassportIssueDate == null || objUpdateEmployee.PassportIssueDate == DateTime.MinValue)
                        sqlParam[20].Value = DBNull.Value;
                    else
                        sqlParam[20].Value = objUpdateEmployee.PassportIssueDate;

                    sqlParam[21] = new SqlParameter(SPParameter.PassportExpireDate, SqlDbType.SmallDateTime);
                    if (objUpdateEmployee.PassportExpireDate == null || objUpdateEmployee.PassportExpireDate == DateTime.MinValue)
                        sqlParam[21].Value = DBNull.Value;
                    else
                        sqlParam[21].Value = objUpdateEmployee.PassportExpireDate;

                    sqlParam[22] = new SqlParameter(SPParameter.PassportIssuePlace, SqlDbType.NChar, 10);
                    if (objUpdateEmployee.PassportIssuePlace == "" || objUpdateEmployee.PassportIssuePlace == null)
                        sqlParam[22].Value = DBNull.Value;
                    else
                        sqlParam[22].Value = objUpdateEmployee.PassportIssuePlace;

                    sqlParam[23] = new SqlParameter(SPParameter.ReadyToRelocateIndia, SqlDbType.Bit);
                    sqlParam[23].Value = objUpdateEmployee.ReadyToRelocateIndia;

                    sqlParam[24] = new SqlParameter(SPParameter.ReasonNotToRelocateIndia, SqlDbType.NVarChar, 100);
                    if (objUpdateEmployee.ReasonNotToRelocateIndia == "" || objUpdateEmployee.ReasonNotToRelocateIndia == null)
                        sqlParam[24].Value = DBNull.Value;
                    else
                        sqlParam[24].Value = objUpdateEmployee.ReasonNotToRelocateIndia;

                    sqlParam[25] = new SqlParameter(SPParameter.ReadytoRelocate, SqlDbType.Bit);
                    sqlParam[25].Value = objUpdateEmployee.ReadyToRelocate;

                    sqlParam[26] = new SqlParameter(SPParameter.ReasonNotToRelocate, SqlDbType.NVarChar, 100);
                    if (objUpdateEmployee.ReasonNotToRelocate == "" || objUpdateEmployee.ReasonNotToRelocate == null)
                        sqlParam[26].Value = DBNull.Value;
                    else
                        sqlParam[26].Value = objUpdateEmployee.ReasonNotToRelocate;

                    sqlParam[27] = new SqlParameter(SPParameter.Duration, SqlDbType.NVarChar, 10);
                    if (objUpdateEmployee.Duration == "" || objUpdateEmployee.Duration == null)
                        sqlParam[27].Value = DBNull.Value;
                    else
                        sqlParam[27].Value = objUpdateEmployee.Duration;

                    sqlParam[28] = new SqlParameter(SPParameter.ResidencePhone, SqlDbType.NChar, 10);
                    if (objUpdateEmployee.ResidencePhone == "" || objUpdateEmployee.ResidencePhone == null)
                        sqlParam[28].Value = DBNull.Value;
                    else
                        sqlParam[28].Value = objUpdateEmployee.ResidencePhone;

                    sqlParam[29] = new SqlParameter(SPParameter.MobileNo, SqlDbType.NChar, 10);
                    if (objUpdateEmployee.MobileNo == "" || objUpdateEmployee.MobileNo == null)
                        sqlParam[29].Value = DBNull.Value;
                    else
                        sqlParam[29].Value = objUpdateEmployee.MobileNo;

                    sqlParam[30] = new SqlParameter(SPParameter.CAddress, SqlDbType.NVarChar, 50);
                    if (objUpdateEmployee.CAddress == "" || objUpdateEmployee.CAddress == null)
                        sqlParam[30].Value = DBNull.Value;
                    else
                        sqlParam[30].Value = objUpdateEmployee.CAddress;

                    sqlParam[31] = new SqlParameter(SPParameter.CStreet, SqlDbType.NVarChar, 50);
                    if (objUpdateEmployee.CStreet == "" || objUpdateEmployee.CStreet == null)
                        sqlParam[31].Value = DBNull.Value;
                    else
                        sqlParam[31].Value = objUpdateEmployee.CStreet;

                    sqlParam[32] = new SqlParameter(SPParameter.CCity, SqlDbType.NVarChar, 50);
                    if (objUpdateEmployee.CCity == "" || objUpdateEmployee.CCity == null)
                        sqlParam[32].Value = DBNull.Value;
                    else
                        sqlParam[32].Value = objUpdateEmployee.CCity;

                    sqlParam[33] = new SqlParameter(SPParameter.CPinCode, SqlDbType.NChar, 6);
                    if (objUpdateEmployee.CPinCode == "" || objUpdateEmployee.CPinCode == null)
                        sqlParam[33].Value = DBNull.Value;
                    else
                        sqlParam[33].Value = objUpdateEmployee.CPinCode;

                    sqlParam[34] = new SqlParameter(SPParameter.PAddress, SqlDbType.NVarChar, 50);
                    if (objUpdateEmployee.PAddress == "" || objUpdateEmployee.PAddress == null)
                        sqlParam[34].Value = DBNull.Value;
                    else
                        sqlParam[34].Value = objUpdateEmployee.PAddress;

                    sqlParam[35] = new SqlParameter(SPParameter.PStreet, SqlDbType.NVarChar, 50);
                    if (objUpdateEmployee.PAddress == "" || objUpdateEmployee.PAddress == null)
                        sqlParam[35].Value = DBNull.Value;
                    else
                        sqlParam[35].Value = objUpdateEmployee.PAddress;

                    sqlParam[36] = new SqlParameter(SPParameter.PCity, SqlDbType.NVarChar, 50);
                    if (objUpdateEmployee.PCity == "" || objUpdateEmployee.PCity == null)
                        sqlParam[36].Value = DBNull.Value;
                    else
                        sqlParam[36].Value = objUpdateEmployee.PCity;

                    sqlParam[37] = new SqlParameter(SPParameter.PPinCode, SqlDbType.NChar, 6);
                    if (objUpdateEmployee.PPinCode == "" || objUpdateEmployee.PPinCode == null)
                        sqlParam[37].Value = DBNull.Value;
                    else
                        sqlParam[37].Value = objUpdateEmployee.PPinCode;

                    sqlParam[38] = new SqlParameter(SPParameter.ResignationDate, SqlDbType.SmallDateTime);
                    if (objUpdateEmployee.ResignationDate == null || objUpdateEmployee.ResignationDate == DateTime.MinValue)
                        sqlParam[38].Value = DBNull.Value;
                    else
                        sqlParam[38].Value = objUpdateEmployee.ResignationDate;

                    sqlParam[39] = new SqlParameter(SPParameter.ResignationReason, SqlDbType.NVarChar, 100);
                    if (objUpdateEmployee.ResignationReason == "" || objUpdateEmployee.ResignationReason == null)
                        sqlParam[39].Value = DBNull.Value;
                    else
                        sqlParam[39].Value = objUpdateEmployee.ResignationReason;

                    sqlParam[40] = new SqlParameter(SPParameter.LastWorkingDay, SqlDbType.SmallDateTime);
                    if (objUpdateEmployee.LastWorkingDay == null || objUpdateEmployee.LastWorkingDay == DateTime.MinValue)
                        sqlParam[40].Value = DBNull.Value;
                    else
                        sqlParam[40].Value = objUpdateEmployee.LastWorkingDay;

                    sqlParam[41] = new SqlParameter(SPParameter.CreatedById, SqlDbType.Int);
                    if (objUpdateEmployee.CreatedById == 0)
                        sqlParam[41].Value = 0;
                    else
                        sqlParam[41].Value = objUpdateEmployee.CreatedById;

                    sqlParam[42] = new SqlParameter(SPParameter.CreatedDate, SqlDbType.DateTime);
                    if (objUpdateEmployee.CreatedDate == null || objUpdateEmployee.CreatedDate == DateTime.MinValue)
                        sqlParam[42].Value = DBNull.Value;
                    else
                        sqlParam[42].Value = objUpdateEmployee.CreatedDate;

                    sqlParam[43] = new SqlParameter(SPParameter.LastModifiedById, SqlDbType.NVarChar, 50);
                    if (objUpdateEmployee.LastModifiedByMailId == null || objUpdateEmployee.LastModifiedByMailId == string.Empty)
                        sqlParam[43].Value = DBNull.Value;
                    else
                        sqlParam[43].Value = objUpdateEmployee.LastModifiedByMailId;

                    sqlParam[44] = new SqlParameter(SPParameter.LastModifiedDate, SqlDbType.DateTime);
                    if (objUpdateEmployee.LastModifiedDate == null || objUpdateEmployee.LastModifiedDate == DateTime.MinValue)
                        sqlParam[44].Value = DBNull.Value;
                    else
                        sqlParam[44].Value = objUpdateEmployee.LastModifiedDate;

                    sqlParam[45] = new SqlParameter(SPParameter.RecruiterId, SqlDbType.Int);
                    if (objUpdateEmployee.RecruiterId == 0)
                        sqlParam[45].Value = 0;
                    else
                        sqlParam[45].Value = objUpdateEmployee.RecruiterId;

                    sqlParam[46] = new SqlParameter(SPParameter.InterviewerId, SqlDbType.Int);
                    if (objUpdateEmployee.InterviewerId == 0)
                        sqlParam[46].Value = 0;
                    else
                        sqlParam[46].Value = objUpdateEmployee.InterviewerId;

                    sqlParam[47] = new SqlParameter(SPParameter.Type, SqlDbType.Int);
                    if (objUpdateEmployee.Type == 0)
                        sqlParam[47].Value = 0;
                    else
                        sqlParam[47].Value = objUpdateEmployee.Type;

                    sqlParam[48] = new SqlParameter(SPParameter.Prefix, SqlDbType.Int);
                    if (objUpdateEmployee.Prefix == 0)
                        sqlParam[48].Value = 0;
                    else
                        sqlParam[48].Value = objUpdateEmployee.Prefix;

                    sqlParam[49] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                    if (objUpdateEmployee.EMPId == 0)
                        return;
                    else
                        sqlParam[49].Value = objUpdateEmployee.EMPId;

                    sqlParam[50] = new SqlParameter(SPParameter.FileName, SqlDbType.NVarChar, 100);
                    if (objUpdateEmployee.FileName == "" || objUpdateEmployee.FileName == null)
                        sqlParam[50].Value = DBNull.Value;
                    else
                        sqlParam[50].Value = objUpdateEmployee.FileName;

                    sqlParam[51] = new SqlParameter(SPParameter.ReportingToId, SqlDbType.NVarChar, 50);
                    if (objUpdateEmployee.ReportingToId == "" || objUpdateEmployee.ReportingToId == null)
                        sqlParam[51].Value = DBNull.Value;
                    else
                        sqlParam[51].Value = objUpdateEmployee.ReportingToId;

                    sqlParam[52] = new SqlParameter(SPParameter.IsFresher, SqlDbType.NVarChar, 50);
                    if (objUpdateEmployee.IsFresher == 0)
                        sqlParam[52].Value = DBNull.Value;
                    else
                        sqlParam[52].Value = objUpdateEmployee.IsFresher;

                    sqlParam[53] = new SqlParameter(SPParameter.ReportingToFM, SqlDbType.Int);
                    if (objUpdateEmployee.ReportingToFMId == 0)
                        sqlParam[53].Value = DBNull.Value;
                    else
                        sqlParam[53].Value = objUpdateEmployee.ReportingToFMId;

                    sqlParam[54] = new SqlParameter(SPParameter.Location, SqlDbType.NChar, 20);
                    if (objUpdateEmployee.EmpLocation == "")
                        sqlParam[54].Value = DBNull.Value;
                    else
                        sqlParam[54].Value = objUpdateEmployee.EmpLocation;

                    sqlParam[55] = new SqlParameter(SPParameter.IsEmployeeDetailsModified, SqlDbType.Bit);
                    sqlParam[55].Value = IsEmployeeDetailsModified;

                    //CR - 28321 Passport Application Number Sachin Start
                    sqlParam[56] = new SqlParameter(SPParameter.PassportAppNo, SqlDbType.NVarChar, 100);
                    if (objUpdateEmployee.PassportAppNo == "" || objUpdateEmployee.PassportAppNo == null)
                        sqlParam[56].Value = DBNull.Value;
                    else
                        sqlParam[56].Value = objUpdateEmployee.PassportAppNo;
                    //CR - 28321 Passport Application Number Sachin End

                    // Mohamed : Issue 39461 : 30/01/2013 : Starts                        			  
                    // Desc :When any employee department get changed resource business unit not get updated.(To update ResourceBusinessUnit)

                    sqlParam[57] = new SqlParameter(SPParameter.DepartementChangeDate, SqlDbType.DateTime);
                    if (string.IsNullOrEmpty(objUpdateEmployee.DepartementChangeDate.ToString().Trim()))
                        sqlParam[57].Value = DBNull.Value;
                    else
                        sqlParam[57].Value = objUpdateEmployee.DepartementChangeDate;

                    sqlParam[58] = new SqlParameter(SPParameter.DesignationChangeDate, SqlDbType.DateTime);
                    if (string.IsNullOrEmpty(objUpdateEmployee.DesignationChangeDate.ToString().Trim()))
                        sqlParam[58].Value = DBNull.Value;
                    else
                        sqlParam[58].Value = objUpdateEmployee.DesignationChangeDate;

                    sqlParam[59] = new SqlParameter(SPParameter.ProbationDate, SqlDbType.DateTime);
                    if (string.IsNullOrEmpty(objUpdateEmployee.ConfirmedDate.ToString().Trim()))
                        sqlParam[59].Value = DBNull.Value;
                    else
                        sqlParam[59].Value = objUpdateEmployee.ConfirmedDate;

                    sqlParam[60] = new SqlParameter(SPParameter.ProbationFlag, SqlDbType.Bit);
                    sqlParam[60].Value = objUpdateEmployee.ProbationFlag;

                    //Ishwar Patil 20112014 For NIS : Start
                    sqlParam[61] = new SqlParameter(SPParameter.CostCode, SqlDbType.NVarChar, 500);
                    if (string.IsNullOrEmpty(objUpdateEmployee.CostCode))
                        sqlParam[61].Value = DBNull.Value;
                    else
                        sqlParam[61].Value = objUpdateEmployee.CostCode;
                    //Ishwar Patil 20112014 For NIS : End

                    // Mohamed : Issue 39461 : 30/01/2013 : Ends

                    //Siddharth 9th June 2015 Start
                    sqlParam[62] = new SqlParameter(SPParameter.BPSSVersion, SqlDbType.NVarChar, 50);
                    if (string.IsNullOrEmpty(objUpdateEmployee.BPSSVersion))
                        sqlParam[62].Value = DBNull.Value;
                    else
                        sqlParam[62].Value = objUpdateEmployee.BPSSVersion;

                    sqlParam[63] = new SqlParameter(SPParameter.BPSSCompletiondate, SqlDbType.DateTime);
                    if (string.IsNullOrEmpty(objUpdateEmployee.BPSSCompletionDate.ToString().Trim()))
                        sqlParam[63].Value = DBNull.Value;
                    else
                        sqlParam[63].Value = objUpdateEmployee.BPSSCompletionDate;

                    //Siddharth 9th June 2015 End						

                    //Siddhesh Arekar 08/07/2015 Start
                    sqlParam[64] = new SqlParameter(SPParameter.LoginRole, SqlDbType.NVarChar, 50);
                    if (string.IsNullOrEmpty(objUpdateEmployee.LoginRole))
                        sqlParam[64].Value = DBNull.Value;
                    else
                        sqlParam[64].Value = objUpdateEmployee.LoginRole;
                    //Siddhesh Arekar 08/07/2015 End

                    //Siddharth 25th August 2015 Start		
                    sqlParam[65] = new SqlParameter(SPParameter.ResourceBusinessUnit, SqlDbType.Int);
                    if (objUpdateEmployee.ResourceBussinessUnit == 0)
                        sqlParam[65].Value = DBNull.Value;
                    else
                        sqlParam[65].Value = objUpdateEmployee.ResourceBussinessUnit;
                    //Siddharth 25th August 2015 End	


                    objDA.ExecuteNonQuerySP(SPNames.Employee_UpdateEmployee, sqlParam);

                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, UPDATEEMPLOYEE, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }



        //Siddharth 3 April 2015 Start
        /// <summary>
        /// Updates the employee.
        /// </summary>
        /// <param name="objUpdateEmployee">The object update employee.</param>
        public void UpdateEmployeeCostCode(DataTable dt, BusinessEntities.Employee objUpdateEmployee)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[6];

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    SqlParameter[] sqlEMpIDparam = new SqlParameter[1];
                    sqlEMpIDparam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int, 10);
                    sqlEMpIDparam[0].Value = objUpdateEmployee.EMPId;

                    objDA.ExecuteNonQuerySP(SPNames.Employee_DeleteEmployeeCostCode, sqlEMpIDparam);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        //Mention datatable column names
                        sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int, 10);
                        if (dt.Rows[i]["EmpId"].ToString().Trim() == "" || dt.Rows[i]["EmpId"].ToString().Trim() == null)
                            sqlParam[0].Value = DBNull.Value;
                        else
                            sqlParam[0].Value = objUpdateEmployee.EMPId;

                        sqlParam[1] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int, 10);
                        if (dt.Rows[i]["ProjectName"].ToString().Trim() == "" || dt.Rows[i]["ProjectName"].ToString().Trim() == null || dt.Rows[i]["ProjectName"].ToString().Trim() == CommonConstants.SELECT)
                            sqlParam[1].Value = DBNull.Value;
                        else
                            sqlParam[1].Value = dt.Rows[i]["ProjectName"].ToString().Trim();

                        sqlParam[2] = new SqlParameter(SPParameter.Billing, SqlDbType.Int, 10);
                        if (dt.Rows[i]["Billing"].ToString().Trim() == "" || dt.Rows[i]["Billing"].ToString().Trim() == null)
                            sqlParam[2].Value = DBNull.Value;
                        else
                            sqlParam[2].Value = dt.Rows[i]["Billing"].ToString().Trim();

                        sqlParam[3] = new SqlParameter(SPParameter.LastModifiedById, SqlDbType.Int, 10);
                        if (objUpdateEmployee.LastModifiedById == 0 || objUpdateEmployee.LastModifiedById == null)
                            sqlParam[3].Value = DBNull.Value;
                        else
                            sqlParam[3].Value = objUpdateEmployee.LastModifiedById;

                        sqlParam[4] = new SqlParameter(SPParameter.CostCode, SqlDbType.NVarChar, 500);
                        if (dt.Rows[i]["CostCode"].ToString().Trim() == "" || dt.Rows[i]["CostCode"].ToString().Trim() == null)
                            sqlParam[4].Value = DBNull.Value;
                        else
                            sqlParam[4].Value = dt.Rows[i]["CostCode"].ToString().Trim();

                        //Check here
                        sqlParam[5] = new SqlParameter(SPParameter.IsActive, SqlDbType.Int);
                        //if (objUpdateEmployee.StatusId == 0)
                        //    sqlParam[5].Value = 0;
                        //else
                        sqlParam[5].Value = 1;//objUpdateEmployee.StatusId;


                        objDA.ExecuteNonQuerySP(SPNames.Employee_UpdateEmployeeCostCode, sqlParam);
                    }
                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (TransactionAbortedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, UPDATEEMPLOYEE, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Updates the employee.
        /// </summary>
        /// <param name="objUpdateEmployee">The object update employee.</param>
        public void UpdateEmployeeCostCode(DataTable dt, int EmpId, int LastModifiedByID)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[6];

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    SqlParameter[] sqlEMpIDparam = new SqlParameter[1];
                    sqlEMpIDparam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int, 10);
                    sqlEMpIDparam[0].Value = EmpId;

                    //First delete all Rows and then insert all new data
                    objDA.ExecuteNonQuerySP(SPNames.Employee_DeleteEmployeeCostCode, sqlEMpIDparam);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        //Mention datatable column names
                        sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int, 10);
                        if (dt.Rows[i]["EmpId"].ToString().Trim() == "" || dt.Rows[i]["EmpId"].ToString().Trim() == null)
                            sqlParam[0].Value = DBNull.Value;
                        else
                            sqlParam[0].Value = EmpId;

                        sqlParam[1] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int, 10);
                        if (dt.Rows[i]["ProjectName"].ToString().Trim() == "" || dt.Rows[i]["ProjectName"].ToString().Trim() == null || dt.Rows[i]["ProjectName"].ToString().Trim() == CommonConstants.SELECT)
                            sqlParam[1].Value = DBNull.Value;
                        else
                            sqlParam[1].Value = dt.Rows[i]["ProjectName"].ToString().Trim();

                        sqlParam[2] = new SqlParameter(SPParameter.Billing, SqlDbType.Int, 10);
                        if (dt.Rows[i]["Billing"].ToString().Trim() == "" || dt.Rows[i]["Billing"].ToString().Trim() == null)
                            sqlParam[2].Value = DBNull.Value;
                        else
                            sqlParam[2].Value = dt.Rows[i]["Billing"].ToString().Trim();

                        sqlParam[3] = new SqlParameter(SPParameter.LastModifiedById, SqlDbType.Int, 10);
                        if (LastModifiedByID == 0 || LastModifiedByID == null)
                            sqlParam[3].Value = DBNull.Value;
                        else
                            sqlParam[3].Value = LastModifiedByID;

                        sqlParam[4] = new SqlParameter(SPParameter.CostCode, SqlDbType.NVarChar, 500);
                        if (dt.Rows[i]["CostCode"].ToString().Trim() == "" || dt.Rows[i]["CostCode"].ToString().Trim() == null)
                            sqlParam[4].Value = DBNull.Value;
                        else
                            sqlParam[4].Value = dt.Rows[i]["CostCode"].ToString().Trim();

                        //Check here
                        sqlParam[5] = new SqlParameter(SPParameter.IsActive, SqlDbType.Int);
                        //if (objUpdateEmployee.StatusId == 0)
                        //    sqlParam[5].Value = 0;
                        //else
                        sqlParam[5].Value = 1;//objUpdateEmployee.StatusId;

                        //Siddharth 8th May 2015 Start
                        //Insert in Database only if CostCode is Present
                        //This checking is done to ensure that empty row is not getting added to database
                        if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["CostCode"]).Trim()))
                        {
                            objDA.ExecuteNonQuerySP(SPNames.Employee_UpdateEmployeeCostCode, sqlParam);
                        }
                        //Siddharth 8th May 2015 End
                    }
                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (TransactionAbortedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, UPDATEEMPLOYEE, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }
  
        public void UpdateEmpCostCodeProjReleaseForCCManager(string EMPId, string ProjectId, string Billing, string CostCode, int LastModifiedByID)//, int EmpCCId)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[5];

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    SqlParameter[] sqlEMpIDparam = new SqlParameter[3];
                    sqlEMpIDparam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int, 10);
                    sqlEMpIDparam[0].Value = EMPId;

                    sqlEMpIDparam[1] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int, 10);
                    if (string.IsNullOrEmpty(ProjectId.Trim()))
                        sqlEMpIDparam[1].Value = DBNull.Value;
                    else
                        sqlEMpIDparam[1].Value = ProjectId;

                    sqlEMpIDparam[2] = new SqlParameter(SPParameter.LastModifiedById, SqlDbType.Int, 10);
                    sqlEMpIDparam[2].Value = LastModifiedByID;
                    //if (EmpCCId == 0 || EmpCCId == null)
                    //    sqlEMpIDparam[2].Value = DBNull.Value;
                    //else
                         //sqlEMpIDparam[2].Value = LastModifiedByID;

                    objDA.ExecuteNonQuerySP(SPNames.Employee_DeleteEmployeeCostCodeForCCManager, sqlEMpIDparam);


                    //Mention datatable column names
                    sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int, 10);
                    if (EMPId == "" || String.IsNullOrEmpty(EMPId))
                        sqlParam[0].Value = DBNull.Value;
                    else
                        sqlParam[0].Value = Convert.ToInt16(EMPId);

                    sqlParam[1] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int, 10);
                    if (String.IsNullOrEmpty(ProjectId) || ProjectId.ToString().Trim().Contains(CommonConstants.SELECT))
                        sqlParam[1].Value = DBNull.Value;
                    else
                        sqlParam[1].Value = Convert.ToInt16(ProjectId);

                    sqlParam[2] = new SqlParameter(SPParameter.Billing, SqlDbType.Int, 10);
                    if (Billing == "" || String.IsNullOrEmpty(Billing))
                        sqlParam[2].Value = DBNull.Value;
                    else
                        sqlParam[2].Value = Convert.ToInt16(Billing);

                    sqlParam[3] = new SqlParameter(SPParameter.LastModifiedById, SqlDbType.Int, 10);
                    if (LastModifiedByID == 0 || LastModifiedByID == null)
                        sqlParam[3].Value = DBNull.Value;
                    else
                        sqlParam[3].Value = LastModifiedByID;

                    sqlParam[4] = new SqlParameter(SPParameter.CostCode, SqlDbType.NVarChar, 500);
                    if (CostCode == "" || String.IsNullOrEmpty(CostCode))
                        sqlParam[4].Value = DBNull.Value;
                    else
                        sqlParam[4].Value = CostCode.ToString().Trim();

                    objDA.ExecuteNonQuerySP(SPNames.Employee_UpdateEmpCostCodeProjReleaseForCCManager, sqlParam);

                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (TransactionAbortedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, UPDATEEMPLOYEE, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }




        public DataTable Employee_GetEmployeeCostCodeByEmpID(int EmpID)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[1];
            DataTable dt = null;
            try
            {
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int, 10);
                sqlParam[0].Value = EmpID;

                //Execute the SP
                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetEmployeeCostCodeByEmpID, sqlParam);

                dt = new DataTable();
                dt.Columns.Add(new DataColumn("ProjectNo", typeof(string)));
                dt.Columns.Add(new DataColumn("ProjectName", typeof(string)));
                dt.Columns.Add(new DataColumn("CostCode", typeof(string)));
                dt.Columns.Add(new DataColumn("Billing", typeof(string)));

                while (objDataReader.Read())
                {
                    DataRow dr = dt.NewRow();
                    dr["ProjectName"] = objDataReader["ProjectId"].ToString();
                    dr["CostCode"] = objDataReader["CostCode"].ToString();

                    if (!string.IsNullOrEmpty(objDataReader["ProjectId"].ToString().Trim()))
                        dr["Billing"] = objDataReader["ECC_Billing"].ToString();
                    else
                        dr["Billing"] = objDataReader["EPA_Billing"].ToString();

                    dt.Rows.Add(dr);
                }

                return dt;
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETEMPLOYEE, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }




        public string Employee_GetEmployeeCostCodeByEmpIDandPrjID(int EmpID, int ProjectID)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[2];
            string str = string.Empty;
            try
            {
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int, 10);
                sqlParam[0].Value = EmpID;

                sqlParam[1] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int, 10);
                sqlParam[1].Value = ProjectID;

                //Execute the SP
                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetEmployeeCostCodeByEmpIDandPrjID, sqlParam);

                while (objDataReader.Read())
                {
                    str = objDataReader["CostCode"].ToString() + "~" + objDataReader["Billing"].ToString();
                }
                return str;
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETEMPLOYEE, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }


        public string Employee_GetEmployeeCostCodeByEmpIDandNoPrjID(int EmpID)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[1];
            string str = string.Empty;
            try
            {
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int, 10);
                sqlParam[0].Value = EmpID;

                //Execute the SP
                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetEmployeeCostCodeByEmpIDandNoPrjID, sqlParam);

                while (objDataReader.Read())
                {
                    str = objDataReader["CostCode"].ToString() + "~" + objDataReader["Billing"].ToString();
                }
                return str;
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETEMPLOYEE, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }





        public int Employee_GetWindowsUsenameofLoggedInUserForCCManager(string WindowsUsername)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[1];
            int val = 0;
            try
            {
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.WindowsUsername, SqlDbType.VarChar, 50);
                sqlParam[0].Value = WindowsUsername;

                //Execute the SP
                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetEmpIDFromWindowsUsernameForCCManager, sqlParam);

                while (objDataReader.Read())
                {
                    if (!string.IsNullOrEmpty(objDataReader["EMPId"].ToString().Trim()))
                    {
                        val = Convert.ToInt16(objDataReader["EMPId"].ToString().Trim());
                    }
                }

                return val;
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETEMPLOYEE, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }


        //Siddharth 3 April 2015 End





        /// <summary>
        /// Gets the employee.
        /// </summary>
        /// <param name="objGetEmployee">The object get employee.</param>
        /// <returns>List<BusinessEntities.Employee></returns>
        public BusinessEntities.Employee GetEmployee(BusinessEntities.Employee objGetEmployee)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[2];
            int count = 0;

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);

                    //If empID is 0 then there is no need of fetching data;
                    if (objGetEmployee.EMPId == 0)
                        return null;
                    else
                        sqlParam[0].Value = objGetEmployee.EMPId;

                    //if (objGetEmployee.EMPId == 0)
                    //    return null;
                    //else
                    //    sqlParam[0].Value = objGetEmployee.EMPId;

                    sqlParam[1] = new SqlParameter(SPParameter.COUNT, SqlDbType.Int);
                    sqlParam[1].Value = 0;
                    sqlParam[1].Direction = ParameterDirection.Output;

                    //Execute the SP
                    objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetEmployee, sqlParam);

                    while (objDataReader.Read())
                    {
                        objEmployee = new BusinessEntities.Employee();

                        objEmployee.EMPId = Convert.ToInt32(objDataReader[DbTableColumn.EMPId].ToString());
                        objEmployee.EMPCode = objDataReader[DbTableColumn.EMPCode].ToString();
                        objEmployee.FirstName = objDataReader[DbTableColumn.FirstName].ToString();
                        objEmployee.MiddleName = objDataReader[DbTableColumn.MiddleName].ToString();
                        objEmployee.LastName = objDataReader[DbTableColumn.LastName].ToString();
                        objEmployee.EMPPicture = System.Text.Encoding.ASCII.GetBytes(objDataReader[DbTableColumn.EMPPicture].ToString());
                        objEmployee.EmailId = objDataReader[DbTableColumn.EmailId].ToString();
                        objEmployee.StatusId = Convert.ToInt32(objDataReader[DbTableColumn.StatusId].ToString());
                        objEmployee.GroupId = Convert.ToInt32(objDataReader[DbTableColumn.GroupId].ToString());
                        objEmployee.DesignationId = Convert.ToInt32(objDataReader[DbTableColumn.DesignationId].ToString());
                        objEmployee.RoleId = Convert.ToInt32(objDataReader[DbTableColumn.RoleId].ToString());
                        objEmployee.Band = Convert.ToInt32(objDataReader[DbTableColumn.Band].ToString());
                        objEmployee.JoiningDate = objDataReader[DbTableColumn.JoiningDate].ToString() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(objDataReader[DbTableColumn.JoiningDate].ToString());
                        objEmployee.DOB = objDataReader[DbTableColumn.DOB].ToString() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(objDataReader[DbTableColumn.DOB].ToString());
                        objEmployee.Gender = objDataReader[DbTableColumn.Gender].ToString();
                        objEmployee.MaritalStatus = objDataReader[DbTableColumn.MaritalStatus].ToString();
                        objEmployee.FatherName = objDataReader[DbTableColumn.FatherName].ToString();
                        objEmployee.SpouseName = objDataReader[DbTableColumn.SpouseName].ToString();
                        objEmployee.BloodGroup = objDataReader[DbTableColumn.BloodGroup].ToString();
                        objEmployee.EmergencyContactNo = objDataReader[DbTableColumn.EmergencyContactNo].ToString();
                        objEmployee.PassportNo = objDataReader[DbTableColumn.PassportNo].ToString();

                        //Mohamed : Issue 39509/41062 : 07/03/2013 : Starts                        			  
                        //Desc :  Adding new Columns date for Probation,Designation and Departement

                        objEmployee.DepartementChangeDate = objDataReader[DbTableColumn.DepartementChangeDate].ToString() == string.Empty ? string.Empty : objDataReader[DbTableColumn.DepartementChangeDate].ToString();
                        objEmployee.DesignationChangeDate = objDataReader[DbTableColumn.DesignationChangeDate].ToString() == string.Empty ? string.Empty : objDataReader[DbTableColumn.DesignationChangeDate].ToString();
                        objEmployee.ConfirmedDate = objDataReader[DbTableColumn.ProbationDate].ToString() == string.Empty ? string.Empty : objDataReader[DbTableColumn.ProbationDate].ToString();
                        objEmployee.ProbationFlag = Convert.ToBoolean(objDataReader[DbTableColumn.ProbationFlag].ToString());

                        //Mohamed : Issue 39509/41062 : 07/03/2013 : Ends

                        //CR - 28321 Passport Application Number Sachin
                        objEmployee.PassportAppNo = objDataReader[DbTableColumn.PassportAppNo].ToString();
                        //CR - 28321 Passport Application Number Sachin - End

                        objEmployee.PassportIssueDate = objDataReader[DbTableColumn.PassportIssueDate].ToString() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(objDataReader[DbTableColumn.PassportIssueDate].ToString());
                        objEmployee.PassportExpireDate = objDataReader[DbTableColumn.PassportExpireDate].ToString() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(objDataReader[DbTableColumn.PassportExpireDate].ToString());
                        objEmployee.PassportIssuePlace = objDataReader[DbTableColumn.PassportIssuePlace].ToString();
                        objEmployee.ReadyToRelocateIndia = Convert.ToBoolean(objDataReader[DbTableColumn.ReadyToRelocateIndia].ToString());
                        objEmployee.ReasonNotToRelocateIndia = objDataReader[DbTableColumn.ReasonNotToRelocateIndia].ToString();
                        objEmployee.ReadyToRelocate = Convert.ToBoolean(objDataReader[DbTableColumn.ReadyToRelocate].ToString());
                        objEmployee.ReasonNotToRelocate = objDataReader[DbTableColumn.ReasonNotToRelocate].ToString();
                        objEmployee.Duration = objDataReader[DbTableColumn.Duration].ToString();
                        objEmployee.ResidencePhone = objDataReader[DbTableColumn.ResidencePhone].ToString();
                        objEmployee.MobileNo = objDataReader[DbTableColumn.MobileNo].ToString();
                        objEmployee.CAddress = objDataReader[DbTableColumn.CAddress].ToString();
                        objEmployee.CStreet = objDataReader[DbTableColumn.CStreet].ToString();
                        objEmployee.CCity = objDataReader[DbTableColumn.CCity].ToString();
                        objEmployee.CPinCode = objDataReader[DbTableColumn.CPinCode].ToString();
                        objEmployee.PAddress = objDataReader[DbTableColumn.PAddress].ToString();
                        objEmployee.PStreet = objDataReader[DbTableColumn.PStreet].ToString();
                        objEmployee.PCity = objDataReader[DbTableColumn.PCity].ToString();
                        objEmployee.PPinCode = objDataReader[DbTableColumn.PPinCode].ToString();
                        objEmployee.ResignationDate = objDataReader[DbTableColumn.ResignationDate].ToString() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(objDataReader[DbTableColumn.ResignationDate].ToString());
                        objEmployee.ResignationReason = objDataReader[DbTableColumn.ResignationReason].ToString();
                        objEmployee.LastWorkingDay = objDataReader[DbTableColumn.LastWorkingDay].ToString() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(objDataReader[DbTableColumn.LastWorkingDay].ToString());

                        //CR - 29936 On exit employee his name to be removed from mailing list Sachin Start
                        objEmployee.CreatedById = objDataReader[DbTableColumn.CreatedById].ToString() == string.Empty ? 0 : Convert.ToInt32(objDataReader[DbTableColumn.CreatedById].ToString());
                        //CR - 29936 On exit employee his name to be removed from mailing list Sachin End

                        objEmployee.CreatedDate = objDataReader[DbTableColumn.CreatedDate].ToString() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(objDataReader[DbTableColumn.CreatedDate].ToString());
                        //To solved the issue id 20769
                        //Start
                        if (objDataReader[DbTableColumn.LastModifiedById].ToString().Length != 0)
                            objEmployee.LastModifiedById = Convert.ToInt32(objDataReader[DbTableColumn.LastModifiedById].ToString());
                        else
                            objEmployee.LastModifiedById = 0;
                        //End
                        objEmployee.LastModifiedDate = objDataReader[DbTableColumn.LastModifiedDate].ToString() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(objDataReader[DbTableColumn.LastModifiedDate].ToString());

                        //CR - 29936 On exit employee his name to be removed from mailing list Sachin Start
                        objEmployee.RecruiterId = objDataReader[DbTableColumn.RecruiterId].ToString() == string.Empty ? 0 : Convert.ToInt32(objDataReader[DbTableColumn.RecruiterId].ToString());
                        objEmployee.InterviewerId = objDataReader[DbTableColumn.InterviewerId].ToString() == string.Empty ? 0 : Convert.ToInt32(objDataReader[DbTableColumn.InterviewerId].ToString());
                        //CR - 29936 On exit employee his name to be removed from mailing list Sachin End

                        objEmployee.Type = Convert.ToInt32(objDataReader[DbTableColumn.Type].ToString());
                        objEmployee.Prefix = Convert.ToInt32(objDataReader[DbTableColumn.Prefix].ToString());
                        objEmployee.FileName = objDataReader[DbTableColumn.FileName].ToString();
                        objEmployee.ReportingToId = objDataReader[DbTableColumn.ReportingToId].ToString();
                        objEmployee.IsFresher = objDataReader[DbTableColumn.IsFresher].ToString() == string.Empty ? 0 : Convert.ToInt32(objDataReader[DbTableColumn.IsFresher]);
                        objEmployee.Department = objDataReader[DbTableColumn.Department].ToString();
                        objEmployee.Designation = objDataReader[DbTableColumn.Designation].ToString();
                        objEmployee.ReportingToFMId = objDataReader[DbTableColumn.FunctionalManagerId].ToString() == string.Empty ? 0 : Convert.ToInt32(objDataReader[DbTableColumn.FunctionalManagerId].ToString());
                        objEmployee.EmpLocation = objDataReader[DbTableColumn.Location].ToString().Trim();

                        // Ishwar - NISRMS - 24112014 Start
                        objEmployee.CostCode = objDataReader[DbTableColumn.CostCode].ToString().Trim();
                        // Ishwar - NISRMS - 24112014 End


                        //Siddharth 9th June 2015 Start
                        objEmployee.BPSSVersion = objDataReader[DbTableColumn.BPSSVersion].ToString().Trim();
                        objEmployee.BPSSCompletionDate = objDataReader[DbTableColumn.BPSSCompletiondate].ToString();// == string.Empty ? DateTime.MinValue : Convert.ToDateTime(objDataReader[DbTableColumn.BPSSCompletiondate].ToString());
                        //Siddharth 9th June 2015 End

                        //Siddharth 25th August 2015 Start
                        objEmployee.ResourceBussinessUnit = Convert.ToInt32(objDataReader[DbTableColumn.ResourceBusinessUnit].ToString());
                        //Siddharth 25th August 2015 End

                    }

                    if (!objDataReader.IsClosed)
                    {
                        objDataReader.Close();
                        count = sqlParam[1] == null ? 1 : Convert.ToInt32(sqlParam[1].Value.ToString());
                    }
                    ts.Complete();

                    //CR - 29936 On exit employee his name to be removed from mailing list Sachin Start
                    // Added IF Block Condition.
                    if (objEmployee != null && objEmployee.ReportingToId != null)
                    {
                        if (count == 0)
                            objEmployee.ReportingToId = objEmployee.ReportingToFMId.ToString();
                    }
                    //CR - 29936 On exit employee his name to be removed from mailing list Sachin End
                }
                if (objEmployee != null && objEmployee.ReportingToId != null && objEmployee.ReportingToId != string.Empty && objEmployee.ReportingToFMId != null)
                {
                    objEmployee.ReportingTo = this.GetEmployeeReportingToName(objEmployee.ReportingToId);
                    objEmployee.ReportingToFM = this.GetEmployeeReportingToName(objEmployee.ReportingToFMId.ToString());
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETEMPLOYEE, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    if (!objDataReader.IsClosed)
                    {
                        objDataReader.Close();
                    }
                }
                objDA.CloseConncetion();
            }
            return objEmployee;
        }

        /// <summary>
        /// Deletes the employee.
        /// </summary>
        /// <param name="objDeleteEmployee">The object delete employee.</param>
        public void DeleteEmployee(BusinessEntities.Employee objDeleteEmployee)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[1];

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                    if (objDeleteEmployee.EMPId == 0)
                        return;
                    else
                        sqlParam[0].Value = objDeleteEmployee.EMPId;

                    objDA.ExecuteNonQuerySP(SPNames.Employee_DeleteEmployee, sqlParam);

                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, DELETEEMPLOYEE, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Gets the employee by Employee Email ID.
        /// </summary>
        /// <param name="objGetEmployee">The object get employee.</param>
        /// <returns>List<BusinessEntities.Employee></returns>
        public BusinessEntities.Employee GetEmployeeByEmailId(BusinessEntities.Employee objGetEmployee)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[2];
            int count = 0;

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    sqlParam[0] = new SqlParameter(SPParameter.EmailId, SqlDbType.VarChar);

                    //If empID is 0 then there is no need of fetching data;
                    if (objGetEmployee.EmailId == null)
                        return null;
                    else
                        sqlParam[0].Value = objGetEmployee.EmailId;

                    sqlParam[1] = new SqlParameter(SPParameter.COUNT, SqlDbType.Int);
                    sqlParam[1].Value = 0;
                    sqlParam[1].Direction = ParameterDirection.Output;

                    //Execute the SP
                    objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetEmployeeByEmailId, sqlParam);

                    while (objDataReader.Read())
                    {
                        objEmployee = new BusinessEntities.Employee();

                        objEmployee.EMPId = Convert.ToInt32(objDataReader[DbTableColumn.EMPId].ToString());
                        objEmployee.EMPCode = objDataReader[DbTableColumn.EMPCode].ToString();
                        objEmployee.FirstName = objDataReader[DbTableColumn.FirstName].ToString();
                        objEmployee.MiddleName = objDataReader[DbTableColumn.MiddleName].ToString();
                        objEmployee.LastName = objDataReader[DbTableColumn.LastName].ToString();
                        objEmployee.EMPPicture = System.Text.Encoding.ASCII.GetBytes(objDataReader[DbTableColumn.EMPPicture].ToString());
                        objEmployee.EmailId = objDataReader[DbTableColumn.EmailId].ToString();
                        objEmployee.StatusId = Convert.ToInt32(objDataReader[DbTableColumn.StatusId].ToString());
                        objEmployee.GroupId = Convert.ToInt32(objDataReader[DbTableColumn.GroupId].ToString());
                        objEmployee.DesignationId = Convert.ToInt32(objDataReader[DbTableColumn.DesignationId].ToString());
                        objEmployee.RoleId = Convert.ToInt32(objDataReader[DbTableColumn.RoleId].ToString());
                        objEmployee.Band = Convert.ToInt32(objDataReader[DbTableColumn.Band].ToString());
                        objEmployee.JoiningDate = objDataReader[DbTableColumn.JoiningDate].ToString() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(objDataReader[DbTableColumn.JoiningDate].ToString());
                        objEmployee.DOB = objDataReader[DbTableColumn.DOB].ToString() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(objDataReader[DbTableColumn.DOB].ToString());
                        objEmployee.Gender = objDataReader[DbTableColumn.Gender].ToString();
                        objEmployee.MaritalStatus = objDataReader[DbTableColumn.MaritalStatus].ToString();
                        objEmployee.FatherName = objDataReader[DbTableColumn.FatherName].ToString();
                        objEmployee.SpouseName = objDataReader[DbTableColumn.SpouseName].ToString();
                        objEmployee.BloodGroup = objDataReader[DbTableColumn.BloodGroup].ToString();
                        objEmployee.EmergencyContactNo = objDataReader[DbTableColumn.EmergencyContactNo].ToString();
                        objEmployee.PassportNo = objDataReader[DbTableColumn.PassportNo].ToString();

                        //Mohamed : Issue 39509/41062 : 07/03/2013 : Starts                        			  
                        //Desc :  Adding new Columns date for Probation,Designation and Departement

                        objEmployee.DepartementChangeDate = objDataReader[DbTableColumn.DepartementChangeDate].ToString() == string.Empty ? string.Empty : objDataReader[DbTableColumn.DepartementChangeDate].ToString();
                        objEmployee.DesignationChangeDate = objDataReader[DbTableColumn.DesignationChangeDate].ToString() == string.Empty ? string.Empty : objDataReader[DbTableColumn.DesignationChangeDate].ToString();
                        objEmployee.ConfirmedDate = objDataReader[DbTableColumn.ProbationDate].ToString() == string.Empty ? string.Empty : objDataReader[DbTableColumn.ProbationDate].ToString();
                        objEmployee.ProbationFlag = Convert.ToBoolean(objDataReader[DbTableColumn.ProbationFlag].ToString());

                        //Mohamed : Issue 39509/41062 : 07/03/2013 : Ends

                        //CR - 28321 Passport Application Number Sachin
                        objEmployee.PassportAppNo = objDataReader[DbTableColumn.PassportAppNo].ToString();
                        //CR - 28321 Passport Application Number Sachin - End

                        objEmployee.PassportIssueDate = objDataReader[DbTableColumn.PassportIssueDate].ToString() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(objDataReader[DbTableColumn.PassportIssueDate].ToString());
                        objEmployee.PassportExpireDate = objDataReader[DbTableColumn.PassportExpireDate].ToString() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(objDataReader[DbTableColumn.PassportExpireDate].ToString());
                        objEmployee.PassportIssuePlace = objDataReader[DbTableColumn.PassportIssuePlace].ToString();
                        objEmployee.ReadyToRelocateIndia = Convert.ToBoolean(objDataReader[DbTableColumn.ReadyToRelocateIndia].ToString());
                        objEmployee.ReasonNotToRelocateIndia = objDataReader[DbTableColumn.ReasonNotToRelocateIndia].ToString();
                        objEmployee.ReadyToRelocate = Convert.ToBoolean(objDataReader[DbTableColumn.ReadyToRelocate].ToString());
                        objEmployee.ReasonNotToRelocate = objDataReader[DbTableColumn.ReasonNotToRelocate].ToString();
                        objEmployee.Duration = objDataReader[DbTableColumn.Duration].ToString();
                        objEmployee.ResidencePhone = objDataReader[DbTableColumn.ResidencePhone].ToString();
                        objEmployee.MobileNo = objDataReader[DbTableColumn.MobileNo].ToString();
                        objEmployee.CAddress = objDataReader[DbTableColumn.CAddress].ToString();
                        objEmployee.CStreet = objDataReader[DbTableColumn.CStreet].ToString();
                        objEmployee.CCity = objDataReader[DbTableColumn.CCity].ToString();
                        objEmployee.CPinCode = objDataReader[DbTableColumn.CPinCode].ToString();
                        objEmployee.PAddress = objDataReader[DbTableColumn.PAddress].ToString();
                        objEmployee.PStreet = objDataReader[DbTableColumn.PStreet].ToString();
                        objEmployee.PCity = objDataReader[DbTableColumn.PCity].ToString();
                        objEmployee.PPinCode = objDataReader[DbTableColumn.PPinCode].ToString();
                        objEmployee.ResignationDate = objDataReader[DbTableColumn.ResignationDate].ToString() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(objDataReader[DbTableColumn.ResignationDate].ToString());
                        objEmployee.ResignationReason = objDataReader[DbTableColumn.ResignationReason].ToString();
                        objEmployee.LastWorkingDay = objDataReader[DbTableColumn.LastWorkingDay].ToString() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(objDataReader[DbTableColumn.LastWorkingDay].ToString());

                        //CR - 29936 On exit employee his name to be removed from mailing list Sachin Start
                        objEmployee.CreatedById = objDataReader[DbTableColumn.CreatedById].ToString() == string.Empty ? 0 : Convert.ToInt32(objDataReader[DbTableColumn.CreatedById].ToString());
                        //CR - 29936 On exit employee his name to be removed from mailing list Sachin End

                        objEmployee.CreatedDate = objDataReader[DbTableColumn.CreatedDate].ToString() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(objDataReader[DbTableColumn.CreatedDate].ToString());

                        //CR - 29936 On exit employee his name to be removed from mailing list Sachin Start
                        objEmployee.LastModifiedById = objDataReader[DbTableColumn.LastModifiedById].ToString() == string.Empty ? 0 : Convert.ToInt32(objDataReader[DbTableColumn.LastModifiedById].ToString());
                        //CR - 29936 On exit employee his name to be removed from mailing list Sachin End

                        objEmployee.LastModifiedDate = objDataReader[DbTableColumn.LastModifiedDate].ToString() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(objDataReader[DbTableColumn.LastModifiedDate].ToString());

                        //CR - 29936 On exit employee his name to be removed from mailing list Sachin Start
                        objEmployee.RecruiterId = objDataReader[DbTableColumn.RecruiterId].ToString() == string.Empty ? 0 : Convert.ToInt32(objDataReader[DbTableColumn.RecruiterId].ToString());
                        objEmployee.InterviewerId = objDataReader[DbTableColumn.InterviewerId].ToString() == string.Empty ? 0 : Convert.ToInt32(objDataReader[DbTableColumn.InterviewerId].ToString());
                        //CR - 29936 On exit employee his name to be removed from mailing list Sachin End

                        objEmployee.Type = Convert.ToInt32(objDataReader[DbTableColumn.Type].ToString());
                        objEmployee.Prefix = Convert.ToInt32(objDataReader[DbTableColumn.Prefix].ToString());
                        objEmployee.FileName = objDataReader[DbTableColumn.FileName].ToString();
                        objEmployee.ReportingToId = objDataReader[DbTableColumn.ReportingToId].ToString();
                        objEmployee.IsFresher = objDataReader[DbTableColumn.IsFresher].ToString() == string.Empty ? 0 : Convert.ToInt32(objDataReader[DbTableColumn.IsFresher]);
                        objEmployee.Department = objDataReader[DbTableColumn.Department].ToString();
                        objEmployee.Designation = objDataReader[DbTableColumn.Designation].ToString();
                        objEmployee.ReportingToFMId = objDataReader[DbTableColumn.FunctionalManagerId].ToString() == string.Empty ? 0 : Convert.ToInt32(objDataReader[DbTableColumn.FunctionalManagerId].ToString());
                        objEmployee.EmpLocation = objDataReader[DbTableColumn.Location].ToString().Trim();

                        //Siddharth 9th June 2015 Start
                        objEmployee.BPSSVersion = objDataReader[DbTableColumn.BPSSVersion].ToString().Trim();
                        objEmployee.BPSSCompletionDate = objDataReader[DbTableColumn.BPSSCompletiondate].ToString();// == string.Empty ? DateTime.MinValue : Convert.ToDateTime(objDataReader[DbTableColumn.BPSSCompletiondate].ToString());
                        //Siddharth 9th June 2015 End

                        //Siddharth 25th August 2015 Start
                        objEmployee.ResourceBussinessUnit = Convert.ToInt32(objDataReader[DbTableColumn.ResourceBusinessUnit].ToString());
                        //Siddharth 25th August 2015 End
                    }

                    if (!objDataReader.IsClosed)
                    {
                        objDataReader.Close();
                        count = Convert.ToInt32(sqlParam[1].Value.ToString());
                    }
                    ts.Complete();

                    //CR - 29936 On exit employee his name to be removed from mailing list Sachin Start
                    // Added IF block condition
                    if (objEmployee != null && objEmployee.ReportingToId != null)
                    {
                        if (count == 0)
                            objEmployee.ReportingToId = objEmployee.ReportingToFMId.ToString();
                    }
                    //CR - 29936 On exit employee his name to be removed from mailing list Sachin End
                }
                if (objEmployee != null && objEmployee.ReportingToId != null && objEmployee.ReportingToId != string.Empty && objEmployee.ReportingToFMId != null)
                {
                    objEmployee.ReportingTo = this.GetEmployeeReportingToName(objEmployee.ReportingToId);
                    objEmployee.ReportingToFM = this.GetEmployeeReportingToName(objEmployee.ReportingToFMId.ToString());
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETEMPLOYEE, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    if (!objDataReader.IsClosed)
                    {
                        objDataReader.Close();
                    }
                }

                objDA.CloseConncetion();
            }

            return objEmployee;
        }


        /// <summary>
        /// Gets the employee list.
        /// </summary>
        /// <param name="objGetEmployee">The object get employees list.</param>
        /// <returns>List<BusinessEntities.Employee></returns>
        public BusinessEntities.RaveHRCollection GetEmployeeList(string resourceName)
        {
            string firstName = string.Empty;
            string lastName = string.Empty;
            string middleName = string.Empty;
            
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[1];

            sqlParam[0] = new SqlParameter(SPParameter.FirstName, resourceName);

            BusinessEntities.RaveHRCollection empList = new BusinessEntities.RaveHRCollection();
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());
                    objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetEmployeesList, sqlParam);
                    while (objDataReader.Read())
                    {
                        objEmployee = new BusinessEntities.Employee();


                        objEmployee.EMPId = Convert.ToInt32(objDataReader[DbTableColumn.EMPId].ToString());


                        objEmployee.EMPCode = objDataReader[DbTableColumn.EMPCode].ToString();

                        firstName = objDataReader[DbTableColumn.FirstName].ToString();

                        lastName = objDataReader[DbTableColumn.LastName].ToString();

                        objEmployee.FullName = firstName + " " + " " + lastName;

                        ////Poonam : Issue 56396 : 19/08/2015 : Starts
                        objEmployee.Designation = objDataReader[DbTableColumn.Designation].ToString();
                        ////Poonam : Issue 56396 : 19/08/2015 : Ends

                        ////Aarohi : Issue 28572(CR) : 12/01/2012 : Start
                        //if (EMPId != "")
                        //{
                        //    objEmployee.ProjectId = Convert.ToInt32(objDataReader[DbTableColumn.ProjectId]);

                        //}
                        ////Aarohi : Issue 28572(CR) : 12/01/2012 : End

                        empList.Add(objEmployee);
                    }
                    if (!objDataReader.IsClosed)
                    {
                        objDataReader.Close();
                    }

                    ts.Complete();
                }
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETEMPLOYEELIST, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    if (!objDataReader.IsClosed)
                    {
                        objDataReader.Close();
                    }
                }
                objDA.CloseConncetion();
            }

            return empList;
        }


        /// <summary>
        /// Gets the employee list.
        /// </summary>
        /// <param name="objGetEmployee">The object get employees list.</param>
        /// <returns>List<BusinessEntities.Employee></returns>
        public BusinessEntities.RaveHRCollection GetEmployeeView(string view, int managerEmpId, string resourceName)
        {
            string firstName = string.Empty;
            string lastName = string.Empty;
            string middleName = string.Empty;


            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[3];

            sqlParam[0] = new SqlParameter(SPParameter.EmployeeViewType, view);
            sqlParam[1] = new SqlParameter(SPParameter.EmpId, managerEmpId);
            sqlParam[2] = new SqlParameter(SPParameter.FirstName, resourceName);

            BusinessEntities.RaveHRCollection empList = new BusinessEntities.RaveHRCollection();
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());
                    objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetEmployeesView, sqlParam);
                    while (objDataReader.Read())
                    {
                        objEmployee = new BusinessEntities.Employee();

                        objEmployee.EMPId = Convert.ToInt32(objDataReader[DbTableColumn.EMPId].ToString());

                        objEmployee.EMPCode = objDataReader[DbTableColumn.EMPCode].ToString();

                        firstName = objDataReader[DbTableColumn.FirstName].ToString();

                        lastName = objDataReader[DbTableColumn.LastName].ToString();

                        objEmployee.FullName = firstName + " " + " " + lastName;


                        ////Aarohi : Issue 28572(CR) : 12/01/2012 : Start
                        //if (EMPId != "")
                        //{
                        //    objEmployee.ProjectId = Convert.ToInt32(objDataReader[DbTableColumn.ProjectId]);

                        //}
                        ////Aarohi : Issue 28572(CR) : 12/01/2012 : End

                        empList.Add(objEmployee);
                    }
                    if (!objDataReader.IsClosed)
                    {
                        objDataReader.Close();
                    }

                    ts.Complete();
                }
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETEMPLOYEELIST, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    if (!objDataReader.IsClosed)
                    {
                        objDataReader.Close();
                    }
                }
                objDA.CloseConncetion();
            }

            return empList;
        }


        /// <summary>
        /// Gets the Project Name.
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetMRFCode()
        {
            // Initialise Data Access Class object
            objDA = new DataAccessClass();

            //initialise RaveHRCollection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Execute the SP
                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetMRFCode);

                //Read the data and assign to Collection object
                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    BusinessEntities.KeyValue<string> keyValue = new BusinessEntities.KeyValue<string>();

                    keyValue.KeyName = objDataReader.GetValue(0).ToString();
                    keyValue.Val = objDataReader.GetValue(1).ToString();

                    // Add the object to Collection
                    raveHRCollection.Add(keyValue);
                }

                // Return the Collection
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETMRFCODE, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Updates the MRFCode Detail table with corresponding employee.
        /// </summary>
        /// <param name="objUpdateEmployee">The object update employee.</param>
        public void UpdateEmployeeMRFCode(BusinessEntities.Employee objUpdateEmployee, int mrfStatus)
        {
            objDA = new DataAccessClass();
            // Rajan Kumar : Issue 46252: 10/02/2014 : Starts                        			 
            // Desc : In MRF history need to implemented in all cases in RMS.
            //set array 4 to 5
            //sqlParam = new SqlParameter[4];
            sqlParam = new SqlParameter[5];
            // Rajan Kumar : Issue 46252: 10/02/2014 : END 


            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    sqlParam[0] = new SqlParameter(SPParameter.MRFId, SqlDbType.Int);
                    if (objUpdateEmployee.MRFId == 0)
                        return;
                    else
                        sqlParam[0].Value = objUpdateEmployee.MRFId;

                    sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                    if (objUpdateEmployee.EMPId == 0)
                        return;
                    else
                        sqlParam[1].Value = objUpdateEmployee.EMPId;

                    sqlParam[2] = new SqlParameter(SPParameter.MRFStatus, SqlDbType.Int);
                    if (mrfStatus == 0)
                        return;
                    else
                        sqlParam[2].Value = mrfStatus;

                    sqlParam[3] = new SqlParameter(SPParameter.ReportingToId, SqlDbType.NVarChar);
                    if (objUpdateEmployee.ReportingToId == "" || objUpdateEmployee.ReportingToId == null)
                        objUpdateEmployee.ReportingToId = null;
                    else
                        sqlParam[3].Value = objUpdateEmployee.ReportingToId;
                    // Rajan Kumar : Issue 46252: 10/02/2014 : Starts                        			 
                    // Desc : In MRF history need to implemented in all cases in RMS.
                    //Pass Email to know who is going to modified the data
                    sqlParam[4] = new SqlParameter(SPParameter.EmailIdOfCurrentLoggedInUser, SqlDbType.NVarChar);
                    sqlParam[4].Value = objUpdateEmployee.CreatedByMailId;
                    // Rajan Kumar : Issue 46252: 10/02/2014 : END  
                    objDA.ExecuteNonQuerySP(SPNames.Employee_UpdateEmployeeMRFCode, sqlParam);
                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, UPDATEEMPLOYEEMRFCODE, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Gets the employee list.
        /// </summary>
        /// <param name="objGetEmployee">The object get employees list.</param>
        /// <returns>List<BusinessEntities.Employee></returns>
        public BusinessEntities.RaveHRCollection GetEmployeesSummary(BusinessEntities.ParameterCriteria objParameter, BusinessEntities.Employee employee, ref int pageCount)
        {
            string firstName = string.Empty;
            string lastName = string.Empty;

            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[13];
            BusinessEntities.RaveHRCollection empList = new BusinessEntities.RaveHRCollection();
            try
            {
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (employee.EMPId == 0)
                    sqlParam[0].Value = 0;
                else
                    sqlParam[0].Value = employee.EMPId;

                sqlParam[1] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.Int);
                if (employee.Department == "0")
                    sqlParam[1].Value = 0;
                else
                    sqlParam[1].Value = int.Parse(employee.Department);

                sqlParam[2] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int);
                if (employee.ProjectName == "0")
                    sqlParam[2].Value = 0;
                else
                    sqlParam[2].Value = int.Parse(employee.ProjectName);

                sqlParam[3] = new SqlParameter(SPParameter.RoleId, SqlDbType.Int);
                if (employee.DesignationId == 0)
                    sqlParam[3].Value = 0;
                else
                    sqlParam[3].Value = employee.DesignationId;

                sqlParam[4] = new SqlParameter(SPParameter.StatusId, SqlDbType.Int);
                if (employee.StatusId == 0)
                    sqlParam[4].Value = 0;
                else
                    sqlParam[4].Value = employee.StatusId;

                // Parameter RPM Role
                sqlParam[5] = new SqlParameter("@Role", SqlDbType.VarChar, 20);
                if (objParameter.Role == null)
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = objParameter.Role;

                // Parameter Page Number
                sqlParam[6] = new SqlParameter(SPParameter.pageNum, SqlDbType.Int);
                if (objParameter.PageNumber == CommonConstants.ZERO)
                    sqlParam[6].Value = 0;
                else
                    sqlParam[6].Value = objParameter.PageNumber;

                // Parameter Page Size
                sqlParam[7] = new SqlParameter(SPParameter.pageSize, SqlDbType.Int);
                if (objParameter.PageSize == CommonConstants.ZERO)
                    sqlParam[7].Value = 0;
                else
                    sqlParam[7].Value = objParameter.PageSize;

                // Parameter Sort Expression And Direction
                sqlParam[8] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                if (objParameter.SortExpressionAndDirection == null)
                    sqlParam[8].Value = DBNull.Value;
                else
                    sqlParam[8].Value = objParameter.SortExpressionAndDirection;

                sqlParam[9] = new SqlParameter(SPParameter.FirstName, SqlDbType.VarChar, 50);
                if (employee.FullName == null || employee.FullName == "")
                    sqlParam[9].Value = DBNull.Value;
                else
                    sqlParam[9].Value = employee.FullName;

                // Output parameter Page Count
                sqlParam[10] = new SqlParameter(SPParameter.pageCount, SqlDbType.Int);
                sqlParam[10].Direction = ParameterDirection.Output;

                sqlParam[11] = new SqlParameter(SPParameter.EmailId, SqlDbType.VarChar, 50);
                if (objParameter.EMailID == null || objParameter.EMailID == "")
                    sqlParam[11].Value = DBNull.Value;
                else
                    sqlParam[11].Value = objParameter.EMailID;

                //Ishwar Patil 30092014 For NIS : Start
                sqlParam[12] = new SqlParameter(SPParameter.IsRMSEmp, SqlDbType.VarChar, 10);
                if (employee.IsRMSEmp == null)
                    sqlParam[12].Value = DBNull.Value;
                else
                    sqlParam[12].Value = employee.IsRMSEmp;
                //Ishwar Patil 3092014 For NIS : End

                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());
                    objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetEmployeesSummary, sqlParam);

                    while (objDataReader.Read())
                    {
                        objEmployee = new BusinessEntities.Employee();

                        objEmployee.EMPId = Convert.ToInt32(objDataReader[DbTableColumn.EMPId].ToString());

                        objEmployee.EMPCode = objDataReader[DbTableColumn.EMPCode].ToString();

                        firstName = objDataReader[DbTableColumn.FirstName].ToString().ToLower();

                        lastName = objDataReader[DbTableColumn.LastName].ToString().ToLower();

                        objEmployee.FullName = firstName + " " + lastName;

                        objEmployee.Designation = objDataReader[DbTableColumn.Designation].ToString();

                        objEmployee.Department = objDataReader[DbTableColumn.DeptName].ToString();

                        objEmployee.JoiningDate = objDataReader[DbTableColumn.JoiningDate].ToString() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(objDataReader[DbTableColumn.JoiningDate].ToString());

                        objEmployee.ProjectCount = Convert.ToInt32(objDataReader[DbTableColumn.ProjectCount].ToString());

                        //Ishwar NISRMS 25112014 Start
                        objEmployee.CostCode = objDataReader[DbTableColumn.CostCode].ToString();
                        //Ishwar NISRMS 25112014 End

                        //Ishwar NISRMS 12032015 Start
                        objEmployee.ResignationDate = objDataReader[DbTableColumn.ResignationDate].ToString() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(objDataReader[DbTableColumn.ResignationDate].ToString());
                        //Ishwar NISRMS 12032015 End

                        if (objDataReader[DbTableColumn.ClientCount].ToString() != "")

                            objEmployee.ClientCount = Convert.ToInt32(objDataReader[DbTableColumn.ClientCount].ToString());

                        empList.Add(objEmployee);
                    }

                    if (!objDataReader.IsClosed)
                    {
                        objDataReader.Close();

                        // Assign PageCount the value returned from SP
                        pageCount = Convert.ToInt32(sqlParam[10].Value);
                    }

                    ts.Complete();
                }

                //for getting the reporting to employee name by their ids
                foreach (BusinessEntities.Employee emp in empList)
                {
                    if (emp.ReportingToId != null && emp.ReportingToId != string.Empty)
                    {
                        emp.ReportingTo = this.GetEmployeeReportingToName(emp.ReportingToId);
                    }
                }
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETEMPLOYEESSUMMARY, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    if (!objDataReader.IsClosed)
                    {
                        objDataReader.Close();
                    }
                }
                objDA.CloseConncetion();
            }

            return empList;
        }

        /// <summary>
        /// Gets the Project Name.
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetProjectName()
        {
            // Initialise Data Access Class object
            objDA = new DataAccessClass();

            //initialise RaveHRCollection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Execute the SP
                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetProjectName);

                //Read the data and assign to Collection object
                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    BusinessEntities.KeyValue<string> keyValue = new BusinessEntities.KeyValue<string>();

                    keyValue.KeyName = objDataReader.GetValue(0).ToString();
                    keyValue.Val = objDataReader.GetValue(1).ToString();

                    // Add the object to Collection
                    raveHRCollection.Add(keyValue);
                }

                // Return the Collection
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETPROJECTNAME, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDA.CloseConncetion();
            }
        }



        /// <summary>
        /// Gets the Project Name.
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetProjectNameForEmpByEmpID(int EmpId)
        {
            // Initialise Data Access Class object
            objDA = new DataAccessClass();

            //initialise RaveHRCollection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter(SPParameter.EmpId, EmpId);


                //Execute the SP
                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_ProjectNameForEmpByEmpID, param);

                //Read the data and assign to Collection object
                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    BusinessEntities.KeyValue<string> keyValue = new BusinessEntities.KeyValue<string>();

                    keyValue.KeyName = objDataReader.GetValue(1).ToString();
                    keyValue.Val = objDataReader.GetValue(0).ToString();

                    // Add the object to Collection
                    raveHRCollection.Add(keyValue);
                }

                // Return the Collection
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETPROJECTNAME, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDA.CloseConncetion();
            }
        }


        /// <summary>
        /// Gets the Employee's Reporting to Name
        /// </summary>
        /// <returns>Collection</returns>
        public string GetEmployeeReportingToName(string empid)
        {
            objDA = new DataAccessClass();
            string sname = string.Empty;
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    //Open the connection to DB
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    SqlParameter[] param = new SqlParameter[1];
                    param[0] = new SqlParameter(SPParameter.Resposibility, empid);

                    //Execute the SP
                    objDataReader = objDA.ExecuteReaderSP(SPNames.MRF_GetEmployeeName, param);
                    //Start
                    //Coded by Rahul P on 21 May 10
                    //Issue Id 17882
                    //Declare variable to count
                    int count = 0;
                    //Read the data and assign to Collection object
                    while (objDataReader.Read())
                    {

                        //Start
                        //Coded by Rahul P on 21 Apr 10
                        //Issue Id 17882
                        //One If condition is insert if there is only one Reporting Too of the Emp
                        if (count == 0)
                        {
                            sname = objDataReader[DbTableColumn.EmployeeName].ToString();
                        }
                        //End
                        else
                        {
                            sname = sname + "," + objDataReader[DbTableColumn.EmployeeName].ToString();
                        }
                        count = count + 1;
                    }
                    if (!objDataReader.IsClosed)
                    {
                        objDataReader.Close();
                    }

                    ts.Complete();
                }

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETEMPLOYEEREPORTINGTONAME, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    if (!objDataReader.IsClosed)
                    {
                        objDataReader.Close();
                    }
                }
                objDA.CloseConncetion();
            }

            // Return the Collection
            return sname;
        }

        /// <summary>
        /// Gets the employee release status list.
        /// </summary>
        /// <param name="objGetEmployee">The object get employees list.</param>
        /// <returns>List<BusinessEntities.Employee></returns>
        public BusinessEntities.RaveHRCollection GetEmployeesReleaseStatus(BusinessEntities.ParameterCriteria objParameter, BusinessEntities.Employee employee, ref int pageCount)
        {
            string firstName = string.Empty;
            string lastName = string.Empty;

            objDA = new DataAccessClass();
            //To solved the issue id 20769
            //Start
            sqlParam = new SqlParameter[9];
            //End
            BusinessEntities.RaveHRCollection empList = new BusinessEntities.RaveHRCollection();
            try
            {
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (employee.EMPId == 0)
                    return empList;
                else
                    sqlParam[0].Value = employee.EMPId;


                sqlParam[1] = new SqlParameter(SPParameter.StatusId, SqlDbType.Int);
                if (employee.StatusId == 0)
                    sqlParam[1].Value = 0;
                else
                    sqlParam[1].Value = employee.StatusId;

                // Parameter Page Number
                sqlParam[2] = new SqlParameter(SPParameter.pageNum, SqlDbType.Int);
                if (objParameter.PageNumber == CommonConstants.ZERO)
                    sqlParam[2].Value = 0;
                else
                    sqlParam[2].Value = objParameter.PageNumber;

                // Parameter Page Size
                sqlParam[3] = new SqlParameter(SPParameter.pageSize, SqlDbType.Int);
                if (objParameter.PageSize == CommonConstants.ZERO)
                    sqlParam[3].Value = 0;
                else
                    sqlParam[3].Value = objParameter.PageSize;

                // Parameter Sort Expression And Direction
                sqlParam[4] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                if (objParameter.SortExpressionAndDirection == null)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = objParameter.SortExpressionAndDirection;

                // Output parameter Page Count
                sqlParam[5] = new SqlParameter(SPParameter.pageCount, SqlDbType.Int);
                sqlParam[5].Direction = ParameterDirection.Output;
                //To solved the issue id 20769
                //Start
                sqlParam[6] = new SqlParameter(SPParameter.ProjectName, SqlDbType.VarChar);
                if (employee.ProjectName.ToString().Length == 0)
                {
                    sqlParam[6].Value = "";
                }
                else
                {
                    sqlParam[6].Value = employee.ProjectName.ToString();
                }
                sqlParam[7] = new SqlParameter(SPParameter.ClientName, SqlDbType.VarChar);
                if (employee.ClientName.ToString().Length == 0)
                {
                    sqlParam[7].Value = "";
                }
                else
                {
                    sqlParam[7].Value = employee.ClientName.ToString();
                }
                //End
                sqlParam[8] = new SqlParameter(SPParameter.EmpProjectAllocationId, SqlDbType.Int);
                if (employee.EmpProjectAllocationId == 0)
                {
                    sqlParam[8].Value = 0;
                }
                else
                {
                    sqlParam[8].Value = Convert.ToInt32(employee.EmpProjectAllocationId.ToString());
                }
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());
                    objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetEmployeesReleaseStatus, sqlParam);

                    while (objDataReader.Read())
                    {
                        objEmployee = new BusinessEntities.Employee();
                        objEmployee.EMPId = Convert.ToInt32(objDataReader[DbTableColumn.EMPId].ToString());
                        objEmployee.EmpProjectAllocationId = Convert.ToInt32(objDataReader[DbTableColumn.EmpProjectAllocationId].ToString());
                        objEmployee.EMPCode = objDataReader[DbTableColumn.EMPCode].ToString();
                        firstName = objDataReader[DbTableColumn.FirstName].ToString();
                        lastName = objDataReader[DbTableColumn.LastName].ToString();
                        objEmployee.FullName = firstName + " " + lastName;
                        objEmployee.Role = objDataReader[DbTableColumn.Role].ToString();
                        objEmployee.Department = objDataReader[DbTableColumn.DeptName].ToString();
                        objEmployee.ProjectName = objDataReader[DbTableColumn.ProjectName].ToString();
                        objEmployee.ClientName = objDataReader[DbTableColumn.ClientName].ToString();
                        objEmployee.ProjectStartDate = Convert.ToDateTime(objDataReader[DbTableColumn.StartDate].ToString());
                        objEmployee.Billing = Convert.ToInt32(objDataReader[DbTableColumn.Billing].ToString());
                        objEmployee.Utilization = Convert.ToInt32(objDataReader[DbTableColumn.Utilization].ToString());
                        objEmployee.ProjectReleaseDate = Convert.ToDateTime(objDataReader[DbTableColumn.EndDate].ToString());
                        objEmployee.ReportingToId = objDataReader[DbTableColumn.ReportingToId].ToString();
                        objEmployee.EmailId = objDataReader[DbTableColumn.EmailId].ToString();
                        objEmployee.ProjectEndDate = objDataReader[DbTableColumn.ProjectEndDate].ToString() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(objDataReader[DbTableColumn.ProjectEndDate].ToString());
                        //To solved the issue id 20769
                        //Start
                        if (!string.IsNullOrEmpty(objDataReader[DbTableColumn.UtilAndBillChange].ToString()))
                            objEmployee.UtilizationAndBilling = Convert.ToDateTime(objDataReader[DbTableColumn.UtilAndBillChange].ToString());
                        else
                            objEmployee.UtilizationAndBilling = DateTime.MinValue;
                        //End

                        //To solved issue id 22011
                        //Start
                        if (!string.IsNullOrEmpty(objDataReader[DbTableColumn.BillingChangeDate].ToString()))
                            objEmployee.BillingChangeDate = Convert.ToDateTime(objDataReader[DbTableColumn.BillingChangeDate].ToString());
                        else
                            objEmployee.BillingChangeDate = DateTime.MinValue;

                        if (!string.IsNullOrEmpty(objDataReader[DbTableColumn.ResourceBillingDate].ToString()))
                            objEmployee.ResourceBillingDate = Convert.ToDateTime(objDataReader[DbTableColumn.ResourceBillingDate].ToString());
                        else
                            objEmployee.ResourceBillingDate = DateTime.MinValue;
                        //End

                        if (!string.IsNullOrEmpty(objDataReader[DbTableColumn.ProjectId].ToString()))
                            objEmployee.ProjectId = Convert.ToInt32(objDataReader[DbTableColumn.ProjectId].ToString());
                        else
                            objEmployee.ProjectId = 0;
                        //objEmployee.ReportingTo = this.GetEmployeeReportingToName(objEmployee.ReportingToId);
                        objEmployee.ProjectCode = objDataReader[DbTableColumn.ProjectCode].ToString();

                        empList.Add(objEmployee);
                    }

                    if (!objDataReader.IsClosed)
                    {
                        objDataReader.Close();

                        // Assign PageCount the value returned from SP
                        pageCount = Convert.ToInt32(sqlParam[5].Value);
                    }

                    ts.Complete();
                }

                //for getting the reporting to employee name by their ids
                foreach (BusinessEntities.Employee emp in empList)
                {
                    if (emp.ReportingToId != null && emp.ReportingToId != string.Empty)
                    {
                        emp.ReportingTo = this.GetEmployeeReportingToName(emp.ReportingToId);
                    }
                }
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETEMPLOYEESSUMMARY, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    if (!objDataReader.IsClosed)
                    {
                        objDataReader.Close();
                    }
                }
                objDA.CloseConncetion();
            }

            return empList;
        }


        /// <summary>
        /// Updates the employee project allocation.
        /// </summary>
        /// <param name="objUpdateEmployee">The object update employee.</param>
        public BusinessEntities.RaveHRCollection GetEmployeesResourcePlan(BusinessEntities.Employee employee)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[2];
            BusinessEntities.ResourcePlan objBERPDurationDetail = null;
            BusinessEntities.RaveHRCollection objListRPDurationDetail = new BusinessEntities.RaveHRCollection();
            objDA = new DataAccessClass();
            objDA.OpenConnection(DBConstants.GetDBConnectionString());

            try
            {
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);

                if (employee.EMPId == 0)
                    return objListRPDurationDetail;
                else
                    sqlParam[0].Value = employee.EMPId;

                sqlParam[1] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int);
                if (employee.ProjectId == 0)
                {
                    sqlParam[1].Value = 0;
                }
                else
                {
                    sqlParam[1].Value = Convert.ToInt32(employee.ProjectId.ToString());
                }


                objDA.OpenConnection(DBConstants.GetDBConnectionString());
                objDataReader = objDA.ExecuteReaderSP(SPNames.USP_EMPLOYEE_GETPROJECTALLOCATION, sqlParam);

                while (objDataReader.Read())
                {
                    objBERPDurationDetail = new BusinessEntities.ResourcePlan();

                    objBERPDurationDetail.RPDRowNo = objDataReader[DbTableColumn.RowNo].ToString();
                    objBERPDurationDetail.RPDId = int.Parse(objDataReader[DbTableColumn.RPDetailId].ToString());
                    objBERPDurationDetail.Utilization = int.Parse(objDataReader[DbTableColumn.Utilization].ToString());
                    objBERPDurationDetail.Billing = int.Parse(objDataReader[DbTableColumn.Billing].ToString());
                    objBERPDurationDetail.ResourceLocation = objDataReader[DbTableColumn.Location].ToString();
                    objBERPDurationDetail.ResourceStartDate = DateTime.Parse(objDataReader[DbTableColumn.StartDate].ToString());
                    objBERPDurationDetail.ResourceEndDate = DateTime.Parse(objDataReader[DbTableColumn.EndDate].ToString());
                    objBERPDurationDetail.ProjectLocation = objDataReader[DbTableColumn.ProjectLocation].ToString();
                }
                objListRPDurationDetail.Add(objBERPDurationDetail);


            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETEMPLOYEESSUMMARY, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    if (!objDataReader.IsClosed)
                    {
                        objDataReader.Close();
                    }
                }
                objDA.CloseConncetion();
            }

            return objListRPDurationDetail;

        }


        /// <summary>
        /// Updates the employee project allocation.
        /// </summary>
        /// <param name="objUpdateEmployee">The object update employee.</param>
        public void UpdateEmployeeReleaseStatus(BusinessEntities.Employee objUpdateEmployee, ref Boolean IsProjectClosed)
        {
            objDA = new DataAccessClass();
            //To solved the issue id 20769
            //Start
            //issue no : 31579  Abhishek - Changed array size
            sqlParam = new SqlParameter[14];
            //End

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                    if (objUpdateEmployee.EMPId == 0)
                        sqlParam[0].Value = 0;
                    else
                        sqlParam[0].Value = objUpdateEmployee.EMPId;

                    sqlParam[1] = new SqlParameter(SPParameter.EmpProjectAllocationId, SqlDbType.Int);
                    if (objUpdateEmployee.EmpProjectAllocationId == 0)
                        return;
                    else
                        sqlParam[1].Value = objUpdateEmployee.EmpProjectAllocationId;

                    sqlParam[2] = new SqlParameter(SPParameter.Billing, SqlDbType.Int);
                    if (objUpdateEmployee.Billing == 0)
                        sqlParam[2].Value = 0;
                    else
                        sqlParam[2].Value = objUpdateEmployee.Billing;

                    sqlParam[3] = new SqlParameter(SPParameter.Utilization, SqlDbType.Int);
                    if (objUpdateEmployee.Utilization == 0)
                        sqlParam[3].Value = 0;
                    else
                        sqlParam[3].Value = objUpdateEmployee.Utilization;

                    sqlParam[4] = new SqlParameter(SPParameter.ProjectReleaseDate, SqlDbType.SmallDateTime);
                    if (objUpdateEmployee.ProjectReleaseDate == null || objUpdateEmployee.ProjectReleaseDate == DateTime.MinValue)
                        sqlParam[4].Value = DBNull.Value;
                    else
                        sqlParam[4].Value = objUpdateEmployee.ProjectReleaseDate;

                    sqlParam[5] = new SqlParameter(SPParameter.ResourceReleased, SqlDbType.Int);
                    if (objUpdateEmployee.EmpReleasedStatus == 0)
                        sqlParam[5].Value = 0;
                    else
                        sqlParam[5].Value = objUpdateEmployee.EmpReleasedStatus;

                    sqlParam[6] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int);
                    if (objUpdateEmployee.ProjectId == 0)
                        sqlParam[6].Value = 0;
                    else
                        sqlParam[6].Value = objUpdateEmployee.ProjectId;

                    sqlParam[7] = new SqlParameter(SPParameter.ReasonForExtension, SqlDbType.VarChar, 500);
                    if (objUpdateEmployee.ReasonForExtension == null || objUpdateEmployee.ReasonForExtension == string.Empty)
                        sqlParam[7].Value = DBNull.Value;
                    else
                        sqlParam[7].Value = objUpdateEmployee.ReasonForExtension;

                    sqlParam[8] = new SqlParameter(SPParameter.LastModifiedById, SqlDbType.NVarChar, 50);
                    if (objUpdateEmployee.LastModifiedByMailId == null || objUpdateEmployee.LastModifiedByMailId == string.Empty)
                        sqlParam[8].Value = DBNull.Value;
                    else
                        sqlParam[8].Value = objUpdateEmployee.LastModifiedByMailId;

                    sqlParam[9] = new SqlParameter(SPParameter.COUNT, SqlDbType.Int);
                    sqlParam[9].Value = 0;
                    sqlParam[9].Direction = ParameterDirection.Output;
                    //To solved the issue id 20769
                    //Start
                    sqlParam[10] = new SqlParameter(SPParameter.UtilAndBillChangeDate, SqlDbType.SmallDateTime);
                    if (objUpdateEmployee.UtilizationAndBilling == null || objUpdateEmployee.UtilizationAndBilling == DateTime.MinValue)
                        sqlParam[10].Value = DBNull.Value;
                    else
                        sqlParam[10].Value = objUpdateEmployee.UtilizationAndBilling;
                    //End

                    //To solved issue id 22011
                    //Start
                    sqlParam[11] = new SqlParameter(SPParameter.BillingChangeDate, SqlDbType.SmallDateTime);
                    if (objUpdateEmployee.BillingChangeDate == null || objUpdateEmployee.BillingChangeDate == DateTime.MinValue)
                        sqlParam[11].Value = DBNull.Value;
                    else
                        sqlParam[11].Value = objUpdateEmployee.BillingChangeDate;

                    sqlParam[12] = new SqlParameter(SPParameter.ResourceBillingDate, SqlDbType.SmallDateTime);
                    if (objUpdateEmployee.ResourceBillingDate == null || objUpdateEmployee.ResourceBillingDate == DateTime.MinValue)
                        sqlParam[12].Value = DBNull.Value;
                    else
                        sqlParam[12].Value = objUpdateEmployee.ResourceBillingDate;

                    //End
                    //issue no : 31579  Abhishek 
                    //start 
                    sqlParam[13] = new SqlParameter(SPParameter.ReportingToId, SqlDbType.NVarChar);
                    if (string.IsNullOrEmpty(objUpdateEmployee.ReportingToId))
                        sqlParam[13].Value = DBNull.Value;
                    else
                        sqlParam[13].Value = objUpdateEmployee.ReportingToId;
                    //end

                    int a = objDA.ExecuteNonQuerySP(SPNames.Employee_UpdateEmpProjectAllocation, sqlParam);

                    if (Convert.ToInt32(sqlParam[9].Value) == 0)
                        IsProjectClosed = true;

                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, UPDATEEMPLOYEERELEASESTATUS, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }



        //Siddharth 7th April 2015 Start
        /// <summary>
        /// Updates the employee project allocation.
        /// </summary>
        /// <param name="objUpdateEmployee">The object update employee.</param>
        public void Employee_UpdateEmpCostCodeProjRelease(BusinessEntities.Employee objUpdateEmployee)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[2];

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                    if (objUpdateEmployee.EMPId == 0)
                        sqlParam[0].Value = 0;
                    else
                        sqlParam[0].Value = objUpdateEmployee.EMPId;

                    sqlParam[1] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int);
                    if (objUpdateEmployee.ProjectId == 0)
                        sqlParam[1].Value = 0;
                    else
                        sqlParam[1].Value = objUpdateEmployee.ProjectId;

                    int a = objDA.ExecuteNonQuerySP(SPNames.Employee_UpdateEmpCostCodeProjRelease, sqlParam);

                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, UPDATEEMPLOYEERELEASESTATUS, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        //Siddharth 7th April 2015 End



        // CR - 25715 issue related to ePlatform MRF Sachin
        // Added New Method
        /// <summary>
        /// Get Number Allocated Employee & Pending MRF count
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="NoofAllocatedEmployee"></param>
        /// <param name="NoofMRF"></param>
        public void CheckLastEmployeeRelease(int ProjectId, ref int NoofAllocatedEmployee, ref int NoofMRF)
        {
            objDA = new DataAccessClass();

            sqlParam = new SqlParameter[3];

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    sqlParam[0] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int);
                    sqlParam[0].Value = ProjectId;

                    sqlParam[1] = new SqlParameter(SPParameter.EmpCOUNT, SqlDbType.Int);
                    sqlParam[1].Value = 0;
                    sqlParam[1].Direction = ParameterDirection.Output;

                    sqlParam[2] = new SqlParameter(SPParameter.MRFCOUNT, SqlDbType.Int);
                    sqlParam[2].Value = 0;
                    sqlParam[2].Direction = ParameterDirection.Output;

                    int inttemp = objDA.ExecuteNonQuerySP(SPNames.Employee_ReleaseCheck, sqlParam);

                    NoofAllocatedEmployee = Convert.ToInt32(sqlParam[1].Value);
                    NoofMRF = Convert.ToInt32(sqlParam[2].Value);

                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, CHECKLASTEMPLOYEERELEASE, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }
        // CR - 25715 Sachin  End

        /// <summary>
        /// Checks for employee having same email id.
        /// </summary>
        /// <param name="objEmployee">The object employee.</param>
        public bool IsEmployeeEmailExists(BusinessEntities.Employee objEmployee)
        {
            int empCount = 0;
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[2];

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());


                    sqlParam[0] = new SqlParameter(SPParameter.EmailId, SqlDbType.NVarChar, 50);
                    if (objEmployee.EmailId == "" || objEmployee.EmailId == null)
                        sqlParam[0].Value = DBNull.Value;
                    else
                        sqlParam[0].Value = objEmployee.EmailId;

                    sqlParam[1] = new SqlParameter("@OutEmpCount", SqlDbType.Int);
                    sqlParam[1].Value = 0;
                    sqlParam[1].Direction = ParameterDirection.Output;

                    int row = objDA.ExecuteNonQuerySP(SPNames.Employee_EmployeeEmailExists, sqlParam);

                    empCount = Convert.ToInt32(sqlParam[1].Value.ToString());

                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "IsEmployeeEmailExists", EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
            if (empCount > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Updates the employee resignation details.
        /// </summary>
        /// <param name="objUpdateEmployee">The obj update employee.</param>
        public void UpdateEmployeeResignationDetails(BusinessEntities.Employee objUpdateEmployee)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[7];

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                    if (objUpdateEmployee.EMPId == 0)
                        sqlParam[0].Value = DBNull.Value;
                    else
                        sqlParam[0].Value = objUpdateEmployee.EMPId;

                    sqlParam[1] = new SqlParameter(SPParameter.StatusId, SqlDbType.Int);
                    if (objUpdateEmployee.StatusId == 0)
                        sqlParam[1].Value = 0;
                    else
                        sqlParam[1].Value = objUpdateEmployee.StatusId;

                    sqlParam[2] = new SqlParameter(SPParameter.ResignationDate, SqlDbType.SmallDateTime);
                    if (objUpdateEmployee.ResignationDate == null || objUpdateEmployee.ResignationDate == DateTime.MinValue)
                        sqlParam[2].Value = DBNull.Value;
                    else
                        sqlParam[2].Value = objUpdateEmployee.ResignationDate;

                    sqlParam[3] = new SqlParameter(SPParameter.ResignationReason, SqlDbType.NVarChar, 100);
                    if (objUpdateEmployee.ResignationReason == "" || objUpdateEmployee.ResignationReason == null)
                        sqlParam[3].Value = DBNull.Value;
                    else
                        sqlParam[3].Value = objUpdateEmployee.ResignationReason;

                    sqlParam[4] = new SqlParameter(SPParameter.LastWorkingDay, SqlDbType.SmallDateTime);
                    if (objUpdateEmployee.LastWorkingDay == null || objUpdateEmployee.LastWorkingDay == DateTime.MinValue)
                        sqlParam[4].Value = DBNull.Value;
                    else
                        sqlParam[4].Value = objUpdateEmployee.LastWorkingDay;

                    sqlParam[5] = new SqlParameter(SPParameter.EmailId, SqlDbType.VarChar, 50);
                    if (objUpdateEmployee.LastModifiedByMailId == null || objUpdateEmployee.LastModifiedByMailId == string.Empty || objUpdateEmployee.LastModifiedByMailId == "")
                        sqlParam[5].Value = DBNull.Value;
                    else
                        sqlParam[5].Value = objUpdateEmployee.LastModifiedByMailId;

                    sqlParam[6] = new SqlParameter(SPParameter.LastModifiedDate, SqlDbType.DateTime);
                    if (objUpdateEmployee.LastModifiedDate == null || objUpdateEmployee.LastModifiedDate == DateTime.MinValue)
                        sqlParam[6].Value = DBNull.Value;
                    else
                        sqlParam[6].Value = objUpdateEmployee.LastModifiedDate;

                    objDA.ExecuteNonQuerySP(SPNames.Employee_ResignationDetails, sqlParam);

                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, UPDATEEMPLOYEERESIGNATIONDETAILS, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }


        /// <summary>
        /// Updated the RecruiterId in all Pending External Allocation MRF.
        /// </summary>
        /// <param name="objUpdateEmployee">The obj update employee.</param>
        public RaveHRCollection UpdateRecruiterID(BusinessEntities.Employee objUpdateEmployee)
        {

            RaveHRCollection raveHRCollection = new RaveHRCollection();

            objDA = new DataAccessClass();

            sqlParam = new SqlParameter[1];

            try
            {
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objUpdateEmployee.EMPId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objUpdateEmployee.EMPId;

                objDataReader = objDA.ExecuteReaderSP(SPNames.MRF_CHANGERECRUITMENTMANAGER, sqlParam);

                BusinessEntities.MRFDetail objBEMRFDetail = null;

                while (objDataReader.Read())
                {
                    objBEMRFDetail = new BusinessEntities.MRFDetail();

                    if (objDataReader[DbTableColumn.MRFID] != null)
                        objBEMRFDetail.MRFId = Convert.ToInt32(objDataReader[DbTableColumn.MRFID].ToString());

                    objBEMRFDetail.MRFCode = objDataReader[DbTableColumn.MRFCode].ToString();

                    objBEMRFDetail.RPCode = objDataReader[DbTableColumn.RPCode].ToString();

                    objBEMRFDetail.Role = objDataReader[DbTableColumn.Role].ToString();

                    objBEMRFDetail.ProjectName = objDataReader[DbTableColumn.ProjectName].ToString();

                    if (objDataReader[DbTableColumn.MRFID] != null)
                        objBEMRFDetail.ResourceOnBoard = Convert.ToDateTime(objDataReader[DbTableColumn.ResourceOnBoard].ToString());

                    objBEMRFDetail.RaisedBy = objDataReader[DbTableColumn.RaiseBy].ToString();

                    objBEMRFDetail.Status = objDataReader[DbTableColumn.Status].ToString();

                    objBEMRFDetail.CommentReason = objDataReader[DbTableColumn.CommentReason].ToString();

                    objBEMRFDetail.DepartmentName = objDataReader[DbTableColumn.Department].ToString();

                    objBEMRFDetail.ClientName = objDataReader[DbTableColumn.ClientName].ToString();

                    objBEMRFDetail.RecruitmentManager = objDataReader[DbTableColumn.RecruitmentManager].ToString();

                    raveHRCollection.Add(objBEMRFDetail);
                }

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, UPDATEEMPLOYEERESIGNATIONDETAILS, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
                objDataReader.Close();
            }
            return raveHRCollection;
        }
        /// <summary>
        /// Function will fill all the dropdowns
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetEmployeesDesignations(int departmentId)
        {
            //Declare DataAccess Class Object
            objDA = new DataAccessClass();
            SqlParameter[] sqlParam = new SqlParameter[1];

            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.Int);
                sqlParam[0].Value = departmentId;

                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetEmployeeDesignations, sqlParam);

                while (objDataReader.Read())
                {
                    KeyValue<string> keyValue = new KeyValue<string>();
                    keyValue.KeyName = objDataReader.GetValue(0).ToString();
                    keyValue.Val = objDataReader.GetValue(1).ToString();
                    raveHRCollection.Add(keyValue);
                }
                return raveHRCollection;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, UPDATEEMPLOYEERESIGNATIONDETAILS, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Checks for employee having same email id.
        /// </summary>
        /// <param name="objEmployee">The object employee.</param>
        public int IsEmployeeDataExists(BusinessEntities.Employee objEmployee)
        {
            int empCount = 0;
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[4];

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());


                    sqlParam[0] = new SqlParameter(SPParameter.EmailId, SqlDbType.NVarChar, 50);
                    if (objEmployee.EmailId == "" || objEmployee.EmailId == null)
                        sqlParam[0].Value = DBNull.Value;
                    else
                        sqlParam[0].Value = objEmployee.EmailId;

                    sqlParam[1] = new SqlParameter("@OutFlag", SqlDbType.Int);
                    sqlParam[1].Value = 0;
                    sqlParam[1].Direction = ParameterDirection.Output;

                    sqlParam[2] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                    if (objEmployee.EMPId == 0)
                        sqlParam[2].Value = 0;
                    else
                        sqlParam[2].Value = objEmployee.EMPId;

                    sqlParam[3] = new SqlParameter(SPParameter.EmpCode, SqlDbType.NVarChar, 10);
                    if (objEmployee.EMPCode == "" || objEmployee.EMPCode == null)
                        sqlParam[3].Value = DBNull.Value;
                    else
                        sqlParam[3].Value = objEmployee.EMPCode;

                    int row = objDA.ExecuteNonQuerySP(SPNames.Employee_EmployeeDataExists, sqlParam);

                    empCount = Convert.ToInt32(sqlParam[1].Value.ToString());

                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "IsEmployeeEmailExists", EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }


            return empCount;
        }

        /// <summary>
        /// Gets the managers email id.
        /// </summary>
        /// <param name="empid">The empid.</param>
        /// <returns></returns>
        public string GetProjectManagersEmailId(BusinessEntities.Employee objEmployee, ref string ProjectManagerIds)
        {

            //initialise RaveHRCollection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            //Initialise Data Access Class object
            objDA = new DataAccessClass();

            //Initialise SqlParameter Class object
            sqlParam = new SqlParameter[3];

            string EmailIds = string.Empty;

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Check each parameters nullibality and add values to sqlParam object accordingly
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objEmployee.EMPId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objEmployee.EMPId;

                sqlParam[1] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int);
                sqlParam[1].Value = objEmployee.ProjectId;

                sqlParam[2] = new SqlParameter(SPParameter.EmpProjectAllocationId, SqlDbType.Int);
                sqlParam[2].Value = objEmployee.EmpProjectAllocationId;

                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetProjectManagerEmailId, sqlParam);

                //Read the data and assign to Collection object
                while (objDataReader.Read())
                {
                    EmailIds = EmailIds + objDataReader[DbTableColumn.EmailId].ToString() + ",";
                    ProjectManagerIds = ProjectManagerIds + objDataReader[DbTableColumn.EMPId].ToString() + ",";

                }

                if (!string.IsNullOrEmpty(EmailIds))
                {
                    EmailIds = EmailIds.Remove(EmailIds.Length - 1, 1);
                }
                if (!string.IsNullOrEmpty(ProjectManagerIds))
                {
                    ProjectManagerIds = ProjectManagerIds.Remove(ProjectManagerIds.Length - 1, 1);
                }

                // Return the Collection
                return EmailIds;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETPROJECTMANAGERSEMAILID, EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Gets the active employees details.
        /// </summary>
        /// <returns>Collection</returns>
        /// BusinessEntities.Employee employee
        //public BusinessEntities.RaveHRCollection GetActiveEmployeeDetails()
        //Issue Id : 34521 START Mahendra add parameter employee in function
        //public BusinessEntities.RaveHRCollection GetActiveEmployeeDetails()
        public BusinessEntities.RaveHRCollection GetActiveEmployeeDetails(BusinessEntities.Employee employee)
        {

            //Issue Id : 34521 START Mahendra 
            // Initialise Data Access Class object
            objDA = new DataAccessClass();

            //initialise RaveHRCollection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();
            //Issue Id : 34521 END Mahendra 

            try
            {
                //Issue Id : 34521 START Mahendra 

                string firstName = string.Empty;
                string lastName = string.Empty;

                objDA = new DataAccessClass();
                sqlParam = new SqlParameter[9];

                //Issue Id : 34521 END Mahendra 

                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Issue Id : 34521 START Mahendra 
                sqlParam[0] = new SqlParameter("@Role", SqlDbType.VarChar, 20);
                if (employee.Role == null)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = employee.Role;

                sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (employee.EMPId == 0)
                    sqlParam[1].Value = 0;
                else
                    sqlParam[1].Value = employee.EMPId;

                sqlParam[2] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.Int);
                if (employee.Department == "0")
                    sqlParam[2].Value = 0;
                else
                    sqlParam[2].Value = int.Parse(employee.Department);

                sqlParam[3] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int);
                if (employee.ProjectName == "0")
                    sqlParam[3].Value = 0;
                else
                    sqlParam[3].Value = int.Parse(employee.ProjectName);

                sqlParam[4] = new SqlParameter(SPParameter.RoleId, SqlDbType.Int);
                if (employee.DesignationId == 0)
                    sqlParam[4].Value = 0;
                else
                    sqlParam[4].Value = employee.DesignationId;

                sqlParam[5] = new SqlParameter(SPParameter.StatusId, SqlDbType.Int);
                if (employee.StatusId == 0)
                    sqlParam[5].Value = 0;
                else
                    sqlParam[5].Value = employee.StatusId;

                // Parameter RPM Role


                sqlParam[6] = new SqlParameter(SPParameter.FirstName, SqlDbType.VarChar, 50);
                if (employee.FullName == null || employee.FullName == "")
                    sqlParam[6].Value = DBNull.Value;
                else
                    sqlParam[6].Value = employee.FullName;

                sqlParam[7] = new SqlParameter(SPParameter.EmailId, SqlDbType.VarChar, 50);
                if (employee.EmailId == null || employee.EmailId == "")
                    sqlParam[7].Value = DBNull.Value;
                else
                    sqlParam[7].Value = employee.EmailId;

                //Ishwar Patil 30092014 For NIS : Start
                sqlParam[8] = new SqlParameter(SPParameter.IsRMSEmp, SqlDbType.VarChar, 10);
                if (employee.IsRMSEmp == null)
                    sqlParam[8].Value = DBNull.Value;
                else
                    sqlParam[8].Value = employee.IsRMSEmp;
                //Ishwar Patil 3092014 For NIS : End

                //Execute the SP
                //objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetActiveEmployeesDetail);
                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetActiveEmployeesDetail, sqlParam);

                //Issue Id : 34521 END Mahendra 

                BusinessEntities.Employee objBEEmployee = null;
                //Read the data and assign to Collection object
                while (objDataReader.Read())
                {
                    objBEEmployee = new BusinessEntities.Employee();
                    objBEEmployee.EMPCode = objDataReader[DbTableColumn.EMPCode].ToString();
                    objBEEmployee.EmailId = objDataReader[DbTableColumn.EmailId].ToString();
                    objBEEmployee.FirstName = objDataReader[DbTableColumn.FirstName].ToString();
                    objBEEmployee.LastName = objDataReader[DbTableColumn.LastName].ToString();
                    objBEEmployee.BandName = objDataReader[DbTableColumn.Band].ToString();
                    objBEEmployee.JoiningDate = objDataReader[DbTableColumn.JoiningDate].ToString() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(objDataReader[DbTableColumn.JoiningDate].ToString());
                    objBEEmployee.ExternalWorkExp = objDataReader[DbTableColumn.ExternalWorkExp].ToString();
                    objBEEmployee.EmployeePrimarySkill = objDataReader[DbTableColumn.EmployeePrimarySkill].ToString();
                    objBEEmployee.TotalWorkExperience = objDataReader[DbTableColumn.TotalWorkExperience].ToString();
                    objBEEmployee.InternalWorkExperience = objDataReader[DbTableColumn.InternalWorkExperience].ToString();

                    // Rakesh : Issue 58054: 23/May/2016 : Starts 

                    objBEEmployee.ExternalWorkExpInMonths = objDataReader[DbTableColumn.ExternalWorkExpInMonths].ToString();
                    objBEEmployee.InternalWorkExperienceInMonths = objDataReader[DbTableColumn.InternalWorkExperienceInMonths].ToString();
                    objBEEmployee.TotalWorkExperienceInMonths = objDataReader[DbTableColumn.TotalWorkExperienceInMonths].ToString();

                    //END


                    //Rakesh : Department head HOD, Project wise HOD 15/July/2016 Begin   
                        objBEEmployee.ProjectHeadName = objDataReader[DbTableColumn.HODName].ToString();
                    //Rakesh : Department head HOD, Project wise HOD 15/July/2016 End

                    objBEEmployee.Designation = objDataReader[DbTableColumn.Designation].ToString();
                    objBEEmployee.Department = objDataReader[DbTableColumn.DeptName].ToString();
                    objBEEmployee.ReportingTo = objDataReader[DbTableColumn.ReportingTo].ToString();
                    objBEEmployee.Location = objDataReader[DbTableColumn.Location].ToString();
                    objBEEmployee.ReportingToFM = objDataReader[DbTableColumn.FunctionalManagerId].ToString();
                    objBEEmployee.Gender = objDataReader[DbTableColumn.Gender].ToString();

                    objBEEmployee.LineManagerEmailId = objDataReader[DbTableColumn.LineManagerEmailId].ToString();

                    //Mohamed : Issue 40424 : 07/02/2013 : Starts                        			  
                    //Desc :  "In Employee Detail Report Add 2 new Columns 1. Date of Birth 2. Mobile Number"

                    objBEEmployee.DOB = Convert.ToDateTime(objDataReader[DbTableColumn.DOB].ToString());
                    objBEEmployee.MobileNo = objDataReader[DbTableColumn.MobileNo].ToString();
                    //Mohamed : Issue 40424 : 07/02/2013 : Ends

                    //Rajnikant: Issue 45558 : 18/09/2014 : Starts                        			  
                    //Desc :  Get project name of an employee
                    objBEEmployee.ProjectName = objDataReader[DbTableColumn.ProjectName].ToString();
                    //Rajnikant: Issue 45558 : 18/09/2014 : Ends

                    //Siddharth 3rd June 2015 Start                       			  
                    //Desc :  Get project joining date of an employee
                    objBEEmployee.ProjectJoiningDate = objDataReader[DbTableColumn.ProjectJoiningdate].ToString();
                    //Siddharth 3rd June 2015 End

                    //Ishwar NISRMS 28112014 Starts
                    //Desc :  Get CostCode of an EDC Employee
                    objBEEmployee.CostCode = objDataReader[DbTableColumn.CostCode].ToString();
                    //Ishwar NISRMS 28112014 Ends

                    //Siddharth 3rd June 2015 Start                       			  
                    //Desc :  Get project joining date of an employee
                    objBEEmployee.BusinessVertical = objDataReader[DbTableColumn.BusinessVertical].ToString();
                    //Siddharth 3rd June 2015 End

                    //Ishwar NISRMS 17032015 Start
                    objBEEmployee.ResignationDate = objDataReader[DbTableColumn.ResignationDate].ToString() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(objDataReader[DbTableColumn.ResignationDate].ToString());
                    //Ishwar NISRMS 17032015 End

                    //Siddharth 13th Oct 2015 Start                       			  
                    //Desc :  Get Domains of an employee
                    objBEEmployee.Domain = objDataReader[DbTableColumn.Domain].ToString();
                    //Siddharth 13th Oct 2015 End

                    //Siddharth 17th Nov 2015 Start                       			  
                    //Desc :  Get Domains of an employee
                    objBEEmployee.RBU = objDataReader[DbTableColumn.ResourceBusinessUnitStr].ToString();
                    //Siddharth 17th Nov 2015 End

                    raveHRCollection.Add(objBEEmployee);
                }

                // Return the Collection
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETMRFCODE, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDA.CloseConncetion();
            }
        }


        /// <summary>
        /// Gets the department head email id.
        /// </summary>
        /// <param name="objEmployee">The obj employee.</param>
        /// <param name="ProjectManagerIds">The project manager ids.</param>
        /// <returns></returns>
        public string GetDepartmentHeadEmailId(BusinessEntities.Employee objEmployee)
        {
            //Initialise Data Access Class object
            objDA = new DataAccessClass();

            //Initialise SqlParameter Class object
            sqlParam = new SqlParameter[1];

            string EmailIds = string.Empty;

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Check each parameters nullibality and add values to sqlParam object accordingly
                sqlParam[0] = new SqlParameter(SPParameter.DepartmentName, SqlDbType.NVarChar, 40);
                if (objEmployee.Department == string.Empty)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objEmployee.Department;

                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetDepartmentHeadEmailId, sqlParam);

                //Read the data and assign to Collection object
                while (objDataReader.Read())
                {
                    EmailIds = EmailIds + objDataReader[DbTableColumn.EmailId].ToString() + ", ";

                }

                if (!string.IsNullOrEmpty(EmailIds))
                {
                    EmailIds = EmailIds.Remove(EmailIds.Length - 2, 1);
                }

                return EmailIds;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETPROJECTMANAGERSEMAILID, EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDA.CloseConncetion();
            }
        }


        /// <summary>
        /// Gets the projects on which an employee is currently working.
        /// </summary>
        /// <param name="empId">ID of Employee.</param>
        /// <returns>List<BusinessEntity.Employee></returns>
        public List<BusinessEntities.Employee> GetProjectByEmployee(int empId)
        {
            sqlParam = new SqlParameter[1];

            List<BusinessEntities.Employee> objEmployeeList = new List<BusinessEntities.Employee>();

            objDA = new DataAccessClass();
            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Check each parameters nullibality and add values to sqlParam object accordingly
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);

                if (empId == 0)

                    sqlParam[0].Value = DBNull.Value;

                else

                    sqlParam[0].Value = empId;

                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetEmployeesAllocation, sqlParam);

                //Read the data and assign to Collection object
                while (objDataReader.Read())
                {
                    BusinessEntities.Employee objEmployeeEntity = new BusinessEntities.Employee();

                    objEmployeeEntity.ClientName = objDataReader[DbTableColumn.ClientName].ToString();

                    objEmployeeEntity.ProjectName = objDataReader[DbTableColumn.ProjectName].ToString();
                    //Siddharth 13 April 2015 Start
                    if (Convert.ToString(objDataReader[DbTableColumn.EndDate]) != "")
                        objEmployeeEntity.CostCode = objDataReader[DbTableColumn.CostCode].ToString();
                    //Siddharth 13 April 2015 End
                    if (objDataReader[DbTableColumn.EndDate].ToString() != "")

                        objEmployeeEntity.ProjectReleaseDate = Convert.ToDateTime(objDataReader[DbTableColumn.EndDate].ToString());

                    if (objDataReader[DbTableColumn.Utilization].ToString() != "")

                        objEmployeeEntity.Utilization = Convert.ToInt32(objDataReader[DbTableColumn.Utilization].ToString());

                    if (objDataReader[DbTableColumn.Billing].ToString() != "")

                        objEmployeeEntity.Billing = Convert.ToInt32(objDataReader[DbTableColumn.Billing].ToString());

                    if (objDataReader[DbTableColumn.StartDate].ToString() != "")
                        objEmployeeEntity.ProjectStartDate = Convert.ToDateTime(objDataReader[DbTableColumn.StartDate].ToString());

                    objEmployeeEntity.ReportingTo = objDataReader[DbTableColumn.ReportingTo].ToString();
                    objEmployeeEntity.EMPId = empId;
                    objEmployeeEntity.EmpProjectAllocationId = Convert.ToInt32(objDataReader[DbTableColumn.EmpProjectAllocationId].ToString());

                    objEmployeeList.Add(objEmployeeEntity);
                }

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETPROJECTMANAGERSEMAILID, EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDA.CloseConncetion();
            }

            return objEmployeeList;
        }


        /// <summary>
        /// Checks wether employee is department head
        /// </summary>
        public bool CheckDepartmentHeadbyEmailId(string emailId)
        {
            // Initialise Data Access Class object
            objDA = new DataAccessClass();

            //Initialise SqlParameter Class object
            sqlParam = new SqlParameter[1];

            bool Flag = false;

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Check each parameters nullibality and add values to sqlParam object accordingly
                sqlParam[0] = new SqlParameter(SPParameter.EmailId, SqlDbType.VarChar, 50);
                if (emailId == string.Empty)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = emailId;

                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetDepartmentHeadIdByEmailId, sqlParam);

                BusinessEntities.Employee objBEEmployee = null;

                //Read the data and assign to Collection object
                while (objDataReader.Read())
                {
                    objBEEmployee = new BusinessEntities.Employee();
                    objBEEmployee.EMPId = int.Parse(objDataReader[DbTableColumn.EMPId].ToString());
                    if (objBEEmployee.EMPId > 0)
                    {
                        Flag = true;
                    }
                }
                return Flag;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETMRFCODE, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDA.CloseConncetion();
            }

        }


        public RaveHRCollection IsAllocatedToProject(BusinessEntities.Employee employee)
        {
            try
            {
                objDA = new DataAccessClass();

                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Initialise SqlParameter Class object
                sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (employee.EMPId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = employee.EMPId;

                objDataReader = objDA.ExecuteReaderSP(SPNames.EMPLOYEE_GETPROJECTALLOCATIONFOREMPLOYEE, sqlParam);

                BusinessEntities.Employee objBEEmployee = null;

                RaveHRCollection raveHRCollection = new RaveHRCollection();

                while (objDataReader.Read())
                {
                    objBEEmployee = new BusinessEntities.Employee();

                    objBEEmployee.ClientName = objDataReader[DbTableColumn.ClientName].ToString();

                    raveHRCollection.Add(objBEEmployee);
                }

                return raveHRCollection;
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETPROJECTFOREMPLOYEE, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDA.CloseConncetion();
            }
        }



        public RaveHRCollection GetEmployeesAllocation(BusinessEntities.Employee employee)
        {
            try
            {
                objDA = new DataAccessClass();

                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Initialise SqlParameter Class object
                sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (employee.EMPId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = employee.EMPId;

                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetEmployeesAllocation, sqlParam);

                BusinessEntities.Employee objBEEmployee = null;

                RaveHRCollection raveHRCollection = new RaveHRCollection();

                while (objDataReader.Read())
                {
                    objBEEmployee = new BusinessEntities.Employee();

                    objBEEmployee.ClientName = objDataReader[DbTableColumn.ClientName].ToString();

                    raveHRCollection.Add(objBEEmployee);
                }

                return raveHRCollection;
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETPROJECTFOREMPLOYEE, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDA.CloseConncetion();
            }
        }

        //Issue Id : 28572 Mahendra START
        public RaveHRCollection IsRepotingManager(BusinessEntities.Employee employee)
        {
            try
            {
                objDA = new DataAccessClass();

                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Initialise SqlParameter Class object
                sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (employee.EMPId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = employee.EMPId;

                objDataReader = objDA.ExecuteReaderSP(SPNames.EMPLOYEE_GETReportingManager, sqlParam);

                BusinessEntities.Employee objBEEmployee = null;

                RaveHRCollection raveHRCollection = new RaveHRCollection();

                while (objDataReader.Read())
                {
                    objBEEmployee = new BusinessEntities.Employee();

                    objBEEmployee.EMPId = Convert.ToInt32(objDataReader[DbTableColumn.EMPId]);

                    raveHRCollection.Add(objBEEmployee);
                }

                return raveHRCollection;
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETPROJECTFOREMPLOYEE, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDA.CloseConncetion();
            }
        }
        //Issue Id : 28572 Mahendra END




        /// <summary>
        /// Gets the employees project details.
        /// </summary>
        /// <param name="objParameter">The obj parameter.</param>
        /// <param name="employee">The employee.</param>
        /// <param name="pageCount">The page count.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetEmployeesProjectDetails(BusinessEntities.ParameterCriteria objParameter, BusinessEntities.Employee employee, ref int pageCount)
        {
            string firstName = string.Empty;
            string lastName = string.Empty;

            objDA = new DataAccessClass();

            sqlParam = new SqlParameter[6];

            BusinessEntities.RaveHRCollection empList = new BusinessEntities.RaveHRCollection();
            try
            {
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (employee.EMPId == 0)
                    return empList;
                else
                    sqlParam[0].Value = employee.EMPId;


                sqlParam[1] = new SqlParameter(SPParameter.StatusId, SqlDbType.Int);
                if (employee.StatusId == 0)
                    sqlParam[1].Value = 0;
                else
                    sqlParam[1].Value = employee.StatusId;

                // Parameter Page Number
                sqlParam[2] = new SqlParameter(SPParameter.pageNum, SqlDbType.Int);
                if (objParameter.PageNumber == CommonConstants.ZERO)
                    sqlParam[2].Value = 0;
                else
                    sqlParam[2].Value = objParameter.PageNumber;

                // Parameter Page Size
                sqlParam[3] = new SqlParameter(SPParameter.pageSize, SqlDbType.Int);
                if (objParameter.PageSize == CommonConstants.ZERO)
                    sqlParam[3].Value = 0;
                else
                    sqlParam[3].Value = objParameter.PageSize;

                // Parameter Sort Expression And Direction
                sqlParam[4] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                if (objParameter.SortExpressionAndDirection == null)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = objParameter.SortExpressionAndDirection;

                // Output parameter Page Count
                sqlParam[5] = new SqlParameter(SPParameter.pageCount, SqlDbType.Int);
                sqlParam[5].Direction = ParameterDirection.Output;

                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());
                    objDataReader = objDA.ExecuteReaderSP(SPNames.USP_Employee_GetEmployeesProjectDetails, sqlParam);

                    while (objDataReader.Read())
                    {
                        objEmployee = new BusinessEntities.Employee();
                        objEmployee.EMPId = Convert.ToInt32(objDataReader[DbTableColumn.EMPId].ToString());
                        objEmployee.EmpProjectAllocationId = Convert.ToInt32(objDataReader[DbTableColumn.EmpProjectAllocationId].ToString());
                        objEmployee.EMPCode = objDataReader[DbTableColumn.EMPCode].ToString();
                        firstName = objDataReader[DbTableColumn.FirstName].ToString();
                        lastName = objDataReader[DbTableColumn.LastName].ToString();
                        objEmployee.FullName = firstName + " " + lastName;
                        objEmployee.Role = objDataReader[DbTableColumn.Role].ToString();
                        objEmployee.Department = objDataReader[DbTableColumn.DeptName].ToString();
                        objEmployee.ProjectName = objDataReader[DbTableColumn.ProjectName].ToString();
                        objEmployee.ClientName = objDataReader[DbTableColumn.ClientName].ToString();
                        objEmployee.ProjectStartDate = Convert.ToDateTime(objDataReader[DbTableColumn.StartDate].ToString());
                        objEmployee.Billing = Convert.ToInt32(objDataReader[DbTableColumn.Billing].ToString());
                        objEmployee.Utilization = Convert.ToInt32(objDataReader[DbTableColumn.Utilization].ToString());

                        if (!String.IsNullOrEmpty(objDataReader[DbTableColumn.EndDate].ToString())) // Umesh : Issue 52816 : 03/09/2014 "Null or empty check for string"
                            objEmployee.ProjectReleaseDate = Convert.ToDateTime(objDataReader[DbTableColumn.EndDate].ToString());

                        objEmployee.ReportingToId = objDataReader[DbTableColumn.ReportingToId].ToString();
                        objEmployee.EmailId = objDataReader[DbTableColumn.EmailId].ToString();
                        objEmployee.ProjectEndDate = objDataReader[DbTableColumn.ProjectEndDate].ToString() == string.Empty ? DateTime.MinValue : Convert.ToDateTime(objDataReader[DbTableColumn.ProjectEndDate].ToString());

                        if (!string.IsNullOrEmpty(objDataReader[DbTableColumn.UtilAndBillChange].ToString()))
                            objEmployee.UtilizationAndBilling = Convert.ToDateTime(objDataReader[DbTableColumn.UtilAndBillChange].ToString());
                        else
                            objEmployee.UtilizationAndBilling = DateTime.MinValue;

                        if (!string.IsNullOrEmpty(objDataReader[DbTableColumn.ProjectId].ToString()))
                            objEmployee.ProjectId = Convert.ToInt32(objDataReader[DbTableColumn.ProjectId].ToString());
                        else
                            objEmployee.ProjectId = 0;
                        //objEmployee.ReportingTo = this.GetEmployeeReportingToName(objEmployee.ReportingToId);

                        empList.Add(objEmployee);
                    }

                    if (!objDataReader.IsClosed)
                    {
                        objDataReader.Close();

                        // Assign PageCount the value returned from SP
                        pageCount = Convert.ToInt32(sqlParam[5].Value);
                    }

                    ts.Complete();
                }

                //for getting the reporting to employee name by their ids
                foreach (BusinessEntities.Employee emp in empList)
                {
                    if (emp.ReportingToId != null && emp.ReportingToId != string.Empty)
                    {
                        emp.ReportingTo = this.GetEmployeeReportingToName(emp.ReportingToId);
                    }
                }
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETEMPLOYEESSUMMARY, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    if (!objDataReader.IsClosed)
                    {
                        objDataReader.Close();
                    }
                }
                objDA.CloseConncetion();
            }

            return empList;
        }


        public void RollBackEmployeeResignationDetailsDL(BusinessEntities.Employee objRollBackEmployee)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[5];

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                    if (objRollBackEmployee.EMPId == 0)
                        sqlParam[0].Value = DBNull.Value;
                    else
                        sqlParam[0].Value = objRollBackEmployee.EMPId;

                    sqlParam[1] = new SqlParameter(SPParameter.ResignationDate, SqlDbType.NVarChar, 20);

                    sqlParam[2] = new SqlParameter(SPParameter.ResignationReason, SqlDbType.NVarChar, 20);

                    sqlParam[3] = new SqlParameter(SPParameter.EmailId, SqlDbType.VarChar, 50);
                    if (objRollBackEmployee.LastModifiedByMailId == null ||
                        objRollBackEmployee.LastModifiedByMailId == string.Empty ||
                        objRollBackEmployee.LastModifiedByMailId == "")

                        sqlParam[3].Value = DBNull.Value;
                    else
                        sqlParam[3].Value = objRollBackEmployee.LastModifiedByMailId;

                    sqlParam[4] = new SqlParameter(SPParameter.LastModifiedDate, SqlDbType.DateTime);
                    if (objRollBackEmployee.LastModifiedDate == null ||
                        objRollBackEmployee.LastModifiedDate == DateTime.MinValue)
                        sqlParam[4].Value = DBNull.Value;
                    else
                        sqlParam[4].Value = objRollBackEmployee.LastModifiedDate;

                    objDA.ExecuteNonQuerySP(SPNames.Employee_RollBackResignationDetails, sqlParam);

                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer,
                    CLASS_NAME, UPDATEEMPLOYEERESIGNATIONDETAILS,
                    EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        #endregion

        //#region 28572
        //Aarohi : Issue 28572(CR) : 05/01/2012 : Start

        /// <summary>
        /// Updates FM/RM for the existing Employee.
        /// </summary>
        /// <param name="objUpdateEmployee">The object update employee.</param>
        public void UpdateEmployeeFMRM(int empId, string rmId, int fmId)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[3];

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    sqlParam[0] = new SqlParameter(SPParameter.EmpId, empId);
                    sqlParam[1] = new SqlParameter(SPParameter.ReportingToId, rmId);
                    sqlParam[2] = new SqlParameter(SPParameter.ReportingToFM, fmId);

                    objDA.ExecuteNonQuerySP(SPNames.EMPLOYEES_UPDATE_FUNCTIONAL_AND_REPORTING_MANAGER, sqlParam);

                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "UpdateEmployeeFMRM", EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }


        public void UpdateEmployeeFMRMForProjectAllocation(int empId, string rmId, int projectId)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[3];

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    sqlParam[0] = new SqlParameter(SPParameter.EmpId, empId);
                    sqlParam[1] = new SqlParameter(SPParameter.ReportingToId, rmId);
                    sqlParam[2] = new SqlParameter(SPParameter.ProjectId, projectId);

                    objDA.ExecuteNonQuerySP(SPNames.EMPLOYEES_UPDATE_REPORTING_MANAGER, sqlParam);

                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "UpdateEmployeeFMRM", EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        //Aarohi : Issue 28572(CR) : 05/01/2012 : END


        //Siddharth 9 April 2015 Start
        public DataSet GetProjectWiseEmpCostCodeDetailsForCCManager(int empId, int projectId, int mode)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[3];
            DataSet ds;
            try
            {
                objDA.OpenConnection(DBConstants.GetDBConnectionString());
                ds = new DataSet();

                sqlParam[0] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int, 100);
                if (projectId == null)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = projectId;

                sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int, 100);
                if (empId == null)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = empId;

                sqlParam[2] = new SqlParameter(SPParameter.Mode, SqlDbType.Int, 50);
                if (mode == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = mode;

                ds = objDA.GetDataSet(SPNames.Employee_GetProjectWiseEmpCostCodeDetails, sqlParam);
                return ds;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "UpdateEmployeeFMRM", EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Gets Project manager names, Id in Update manager pop up
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet GetEmployeeAndProjectForCCManager(int role)
        {
            DataSet ds = null;
            objDA = new DataAccessClass();
            SqlParameter[] sqlParam = new SqlParameter[1];
            try
            {
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.Role, SqlDbType.Int, 100);
                sqlParam[0].Value = role;

                ds = new DataSet();
                ds = objDA.GetDataSet(SPNames.Employee_GetEmployeeAndProjectForCCManager, sqlParam);
                return ds;

                //objConnection = new SqlConnection(strConnection);
                //objConnection.Open();

                //ds = new DataSet();
                //objCommand = new SqlCommand(SPNames.Employee_GetEmployeeAndProjectForCCManager, objConnection);
                //objCommand.CommandType = CommandType.StoredProcedure;
                //SqlDataAdapter objDataAdapter = new SqlDataAdapter(objCommand);
                //objDataAdapter.Fill(ds);
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetManagersList", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
        }

        //Siddharth 9 April 2015 End

        public void DeleteEmployeeCostcode (string EMPId, string ProjectId,  int LastModifiedByID)//, int EmpCCId)
        {
            objDA = new DataAccessClass();
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    SqlParameter[] sqlEMpIDparam = new SqlParameter[3];
                    sqlEMpIDparam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int, 10);
                    sqlEMpIDparam[0].Value = EMPId;

                    sqlEMpIDparam[1] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int, 10);
                    if (string.IsNullOrEmpty(ProjectId.Trim()))
                        sqlEMpIDparam[1].Value = DBNull.Value;
                    else
                        sqlEMpIDparam[1].Value = ProjectId;

                    sqlEMpIDparam[2] = new SqlParameter(SPParameter.LastModifiedById, SqlDbType.Int, 10);
                    sqlEMpIDparam[2].Value = LastModifiedByID;

                    objDA.ExecuteNonQuerySP(SPNames.Employee_DeleteEmployeeCostCodeForCCManager, sqlEMpIDparam);
                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (TransactionAbortedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, UPDATEEMPLOYEE, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }           

        //Issue Id : 28572 Mahendra START

        /// <summary>
        /// Gets the Reporting to Ids 
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet GetActiveEmployeeList()
        {
            SqlConnection objConnection = null;
            DataSet ds = null;
            SqlCommand objCommand = null;
            try
            {
                string strConnection = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(strConnection);
                objConnection.Open();

                ds = new DataSet();
                objCommand = new SqlCommand(SPNames.GET_REPORTING_ActiveEmployeeList, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter objDataAdapter = new SqlDataAdapter(objCommand);
                objDataAdapter.Fill(ds);
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetActiveEmployeeList", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            return ds;
        }

        //Issue Id : 49187 Sanju Kushwaha
        // Added following method

        /// <summary>
        /// Gets Project manager names, Id in Update manager pop up
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet GetManagersList()
        {
            SqlConnection objConnection = null;
            DataSet ds = null;
            SqlCommand objCommand = null;
            try
            {
                string strConnection = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(strConnection);
                objConnection.Open();

                ds = new DataSet();
                objCommand = new SqlCommand(SPNames.GET_Project_ManagerList, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter objDataAdapter = new SqlDataAdapter(objCommand);
                objDataAdapter.Fill(ds);
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetManagersList", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
            return ds;
        }
        // Sanju Kushwaha : Issue 49187 : 11/03/2014 : End


        /// <summary>
        /// Gets the Reporting to Ids 
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet GetReportingFunctionalManagerIds(string EMPId, BusinessEntities.ParameterCriteria objParameter)
        {

            DataSet ds = null;
            try
            {
                SqlParameter[] sqlParam = new SqlParameter[2];
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());
                ds = new DataSet();
                //objCommand = new SqlCommand(SPNames.GET_REPORTING_FUNCTIONAL_MANAGER_IDS, objConnection);
                //objCommand.CommandType = CommandType.StoredProcedure;

                //objCommand.Parameters.AddWithValue(SPParameter.EmpId, EMPId);

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.VarChar, 100);
                if (EMPId == null)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = EMPId;

                sqlParam[1] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                if (objParameter.SortExpressionAndDirection == null)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objParameter.SortExpressionAndDirection;

                //SqlDataAdapter objDataAdapter = new SqlDataAdapter(objCommand);
                //objDataAdapter.Fill(ds);

                ds = objDA.GetDataSet(SPNames.GET_REPORTING_FUNCTIONAL_MANAGER_IDS, sqlParam);
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetReportingFunctionalManagerIds", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDA.CloseConncetion();
            }
            return ds;
        }
        //Issue Id : 28572 Mahendra END

        /// <summary>
        /// Gets the employee list.
        /// </summary>
        /// <param name="objGetEmployee">The object get employees list.</param>
        /// <returns>List<BusinessEntities.Employee></returns>
        public BusinessEntities.RaveHRCollection GetEmployeeListForFMRM(string resourceName, string EMPId)
        {
            string firstName = string.Empty;
            string lastName = string.Empty;
            string middleName = string.Empty;
            int projectId = 0;

            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[2];

            sqlParam[0] = new SqlParameter(SPParameter.FirstName, resourceName);
            sqlParam[1] = new SqlParameter(SPParameter.EmpId, EMPId);
            BusinessEntities.RaveHRCollection empList = new BusinessEntities.RaveHRCollection();
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());
                    objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetEmployeesListForFMRM, sqlParam);
                    while (objDataReader.Read())
                    {
                        objEmployee = new BusinessEntities.Employee();

                        objEmployee.EMPId = Convert.ToInt32(objDataReader[DbTableColumn.EMPId].ToString());

                        objEmployee.EMPCode = objDataReader[DbTableColumn.EMPCode].ToString();

                        firstName = objDataReader[DbTableColumn.FirstName].ToString();

                        lastName = objDataReader[DbTableColumn.LastName].ToString();

                        objEmployee.FullName = firstName + " " + " " + lastName;

                        if (EMPId != "")
                        {
                            objEmployee.ProjectId = Convert.ToInt32(objDataReader[DbTableColumn.ProjectId]);

                        }

                        empList.Add(objEmployee);
                    }
                    if (!objDataReader.IsClosed)
                    {
                        objDataReader.Close();
                    }

                    ts.Complete();
                }
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetEmployeeListForFMRM", EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    if (!objDataReader.IsClosed)
                    {
                        objDataReader.Close();
                    }
                }
                objDA.CloseConncetion();
            }

            return empList;
        }
        //Aarohi : Issue 28572(CR) : 31/01/2012 : End

        //#endregion 28572


        #region 35091
        // 35091-Ambar-Start-27062012
        /// <summary>
        /// Gets the managers email id.
        /// </summary>
        /// <param name="empid">The empid.</param>
        /// <returns></returns>
        public string GetAllocationReportingToEmailId(BusinessEntities.Employee objEmployee, ref string AllocationReportingToEmailId)
        {

            //initialise RaveHRCollection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            //Initialise Data Access Class object
            objDA = new DataAccessClass();

            //Initialise SqlParameter Class object
            sqlParam = new SqlParameter[3];

            string EmailIds = string.Empty;

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Check each parameters nullibality and add values to sqlParam object accordingly
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objEmployee.EMPId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objEmployee.EMPId;

                sqlParam[1] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int);
                sqlParam[1].Value = objEmployee.ProjectId;

                sqlParam[2] = new SqlParameter(SPParameter.EmpProjectAllocationId, SqlDbType.Int);
                sqlParam[2].Value = objEmployee.EmpProjectAllocationId;

                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetAllocationReportingToEmailId, sqlParam);

                //Read the data and assign to Collection object
                while (objDataReader.Read())
                {
                    EmailIds = EmailIds + objDataReader[DbTableColumn.EmailId].ToString() + ",";
                    AllocationReportingToEmailId = AllocationReportingToEmailId + objDataReader[DbTableColumn.EMPId].ToString() + ",";

                }

                if (!string.IsNullOrEmpty(EmailIds))
                {
                    EmailIds = EmailIds.Remove(EmailIds.Length - 1, 1);
                }
                if (!string.IsNullOrEmpty(AllocationReportingToEmailId))
                {
                    AllocationReportingToEmailId = AllocationReportingToEmailId.Remove(AllocationReportingToEmailId.Length - 1, 1);
                }

                // Return the Collection
                return EmailIds;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetAllocationReportingToEmailId", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDA.CloseConncetion();
            }
        }

        // 35091-Ambar-End-27062012
        #endregion 35901

        // Venkatesh : Start NIS-RMS : 16-Oct-2014   
        // Desc :  NIS-RMS 
        /// <summary>
        /// Gets the Project Name.
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetProjectNameNIS(string UserRaveDomainId, string EmployeeRole)
        {
            // Initialise Data Access Class object
            objDA = new DataAccessClass();

            //initialise RaveHRCollection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();


            SqlParameter[] sqlParam = new SqlParameter[2];
            try
            {
                sqlParam[0] = new SqlParameter(SPParameter.EmailId, SqlDbType.VarChar);
                if (UserRaveDomainId == "")
                    sqlParam[0].Value = "";
                else
                    sqlParam[0].Value = UserRaveDomainId;

                sqlParam[1] = new SqlParameter(SPParameter.Role, SqlDbType.VarChar);
                if (EmployeeRole == "")
                    sqlParam[1].Value = "";
                else
                    sqlParam[1].Value = EmployeeRole;
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Execute the SP
                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetProjectNameNIS, sqlParam);

                //Read the data and assign to Collection object
                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    BusinessEntities.KeyValue<string> keyValue = new BusinessEntities.KeyValue<string>();

                    keyValue.KeyName = objDataReader.GetValue(0).ToString();
                    keyValue.Val = objDataReader.GetValue(1).ToString();

                    // Add the object to Collection
                    raveHRCollection.Add(keyValue);
                }

                // Return the Collection
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETPROJECTNAME, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDA.CloseConncetion();
            }
        }



        // Siddharth : Start NIS-RMS : 27th April 2015   
        // Desc :  NIS-RMS 
        /// <summary>
        /// Gets the CostCodes.
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetAllCostCodes()
        {
            objDA = new DataAccessClass();
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            raveHRCollection = new BusinessEntities.RaveHRCollection();
            try
            {

                objDA.OpenConnection(DBConstants.GetDBConnectionString());
                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetAllCostCodes);
                while (objDataReader.Read())
                {
                    BusinessEntities.KeyValue<string> keyValue = new BusinessEntities.KeyValue<string>();

                    keyValue.KeyName = objDataReader.GetValue(0).ToString();
                    keyValue.Val = objDataReader.GetValue(0).ToString();
                    raveHRCollection.Add(keyValue);
                }

                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETPROJECTNAME, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDA.CloseConncetion();
            }
        }





        #region Modified By Mohamed Dangra
        // Mohamed : 03/12/2014 : Starts                        			  
        // Desc : NIS Changes

        /// <summary>
        /// Gets the employee list.
        /// </summary>
        /// <param name="objGetEmployee">The object get employees list.</param>
        /// <returns>List<BusinessEntities.Employee></returns>
        public DataSet GetSkillReport(int Level, string SortExpressionAndDirection)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[2];
            DataSet employeesList = null;
            try
            {
                sqlParam[0] = new SqlParameter(SPParameter.SkillReportLevel, SqlDbType.Int);
                sqlParam[0].Value = Level;

                //Siddharth 26 March 2015 Start
                // Parameter Sort Expression And Direction
                sqlParam[1] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                if (SortExpressionAndDirection == null)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = SortExpressionAndDirection;
                //Siddharth 26 March 2015 End

                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());
                    //objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetEmployeesSummary, sqlParam);
                    employeesList = objDA.GetDataSet(SPNames.Employee_GetSkillsReport, sqlParam);
                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETEMPLOYEESSUMMARY, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
            return employeesList;
        }

        // Mohamed : 03/12/2014 : Ends
        #endregion Modified By Mohamed Dangra

        /// <summary>
        /// Gets the employee list.
        /// </summary>
        /// <param name="objGetEmployee">The object get employees list.</param>
        /// <returns>List<BusinessEntities.Employee></returns>
        public DataSet GetConsolidated(int ProjectId, string SortExpressionAndDirection)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[2];
            DataSet employeesList = null;

            try
            {
                sqlParam[0] = new SqlParameter(SPParameter.ProjectId, SqlDbType.Int);
                if (ProjectId == 0)
                    sqlParam[0].Value = 0;
                else
                    sqlParam[0].Value = ProjectId;

                //Siddharth 26 March 2015 Start
                // Parameter Sort Expression And Direction
                sqlParam[1] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                if (SortExpressionAndDirection == null)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = SortExpressionAndDirection;
                //Siddharth 26 March 2015 End


                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());
                    //objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetEmployeesSummary, sqlParam);
                    employeesList = objDA.GetDataSet(SPNames.Employee_GetConsolidated, sqlParam);
                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETEMPLOYEESSUMMARY, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
            return employeesList;
        }
        // Venkatesh : End  NIS-RMS : 16-Oct-2014   




        /// <summary>
        /// Gets the employee list.
        /// </summary>
        /// <param name="objGetEmployee">The object get employees list.</param>
        /// <returns>List<BusinessEntities.Employee></returns>
        public DataSet GetConsolidatedByCostCode(string CostCode, string SortExpressionAndDirection)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[2];
            DataSet employeesList = null;

            try
            {
                sqlParam[0] = new SqlParameter(SPParameter.CostCode, SqlDbType.VarChar, 50);
                if (string.IsNullOrEmpty(CostCode.Trim()))
                    sqlParam[0].Value = "";
                else
                    sqlParam[0].Value = CostCode;

                //Siddharth 26 March 2015 Start
                // Parameter Sort Expression And Direction
                sqlParam[1] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                if (SortExpressionAndDirection == null)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = SortExpressionAndDirection;
                //Siddharth 26 March 2015 End


                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());
                    //objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetEmployeesSummary, sqlParam);
                    employeesList = objDA.GetDataSet(SPNames.Employee_GetConsolidatedbyCostCode, sqlParam);
                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETEMPLOYEESSUMMARY, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
            return employeesList;
        }
        // Venkatesh : End  NIS-RMS : 16-Oct-2014 







        // Ishwar - NISRMS - 30102014 Start
        public BusinessEntities.Employee GetNISEmployeeList(string strUserIdentity)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            SqlParameter[] sqlParam = new SqlParameter[1];
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                sqlParam[0] = new SqlParameter(SPParameter.WindowsUsername, SqlDbType.NVarChar);
                sqlParam[0].Value = strUserIdentity;

                DataSet ds = new DataSet();

                ds = objGetTraining.GetDataSet(SPNames.Employee_GetNISEmployeeList, sqlParam);

                BusinessEntities.Employee employee = new BusinessEntities.Employee();
                if (ds.Tables[0].Rows.Count != 0)
                {
                    employee.WindowsUserName = ds.Tables[0].Rows[0][DbTableColumn.WindowsUserName].ToString();
                }
                return employee;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetNISEmployeeList", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }
        }
        // Ishwar - NISRMS - 30102014 End

        //Umesh: NIS-changes: Head Count Report Starts
        /// <summary>
        /// Gets Head Count Report.
        /// </summary>
        /// <returns>List<BusinessEntities.Projects></returns>
        public BusinessEntities.RaveHRCollection GetHeadCountReport(BusinessEntities.Projects project, string SortExpressionAndDirection)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[4];
            BusinessEntities.RaveHRCollection headCountList = new BusinessEntities.RaveHRCollection();
            try
            {
                sqlParam[0] = new SqlParameter(SPParameter.DivisionId, SqlDbType.Int);
                sqlParam[0].Value = project.ProjectDivision;

                sqlParam[1] = new SqlParameter(SPParameter.BusinessAreaId, SqlDbType.Int);
                sqlParam[1].Value = project.ProjectBussinessArea;

                sqlParam[2] = new SqlParameter(SPParameter.BusinessSegmentId, SqlDbType.Int);
                sqlParam[2].Value = project.ProjectBussinessSegment;

                //Siddharth 26 March 2015 Start
                // Parameter Sort Expression And Direction
                sqlParam[3] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                if (SortExpressionAndDirection == null)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = SortExpressionAndDirection;
                //Siddharth 26 March 2015 End

                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());
                    objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_Report_HeadCount, sqlParam);

                    while (objDataReader.Read())
                    {
                        objEmployee = new BusinessEntities.Employee();
                        objProjects = new BusinessEntities.Projects();

                        objProjects.Division = objDataReader[DbTableColumn.Division].ToString();

                        objProjects.BusinessArea = objDataReader[DbTableColumn.BusinessArea].ToString();

                        objProjects.BusinessSegment = objDataReader[DbTableColumn.BusinessSegment].ToString();

                        objEmployee.CostCode = objDataReader[DbTableColumn.CostCode].ToString();

                        objEmployee.ResourceType = objDataReader[DbTableColumn.ResourceType].ToString();

                        objEmployee.ResourceTypeCount = objDataReader[DbTableColumn.ResourceTypeCount].ToString();

                        objEmployee.Projects = objProjects;

                        headCountList.Add(objEmployee);
                    }

                    if (!objDataReader.IsClosed)
                    {
                        objDataReader.Close();
                    }

                    ts.Complete();
                }
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetHeadCountReport", EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    if (!objDataReader.IsClosed)
                    {
                        objDataReader.Close();
                    }
                }
                objDA.CloseConncetion();
            }

            return headCountList;
        }
        //Umesh: NIS-changes: Head Count Report Ends

        // Ishwar - NISRMS - 27112014 Start
        // For EDC Employee Count
        public int EDCEmployeeCount(string WindowsUserName, string empid)
        {
            int pCount = 0;
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[3];

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    sqlParam[0] = new SqlParameter(SPParameter.WindowsUsername, SqlDbType.NVarChar, 100);
                    if (WindowsUserName == "")
                        sqlParam[0].Value = DBNull.Value;
                    else
                        sqlParam[0].Value = WindowsUserName;

                    sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.NVarChar, 100);
                    if (WindowsUserName == "")
                        sqlParam[1].Value = DBNull.Value;
                    else
                        sqlParam[1].Value = empid;

                    sqlParam[2] = new SqlParameter("@EDCEmployeeCount", SqlDbType.Int);
                    sqlParam[2].Value = 0;
                    sqlParam[2].Direction = ParameterDirection.Output;

                    int row = objDA.ExecuteNonQuerySP(SPNames.Employee_GetEDCEmployeeCount, sqlParam);

                    pCount = Convert.ToInt32(sqlParam[2].Value.ToString());
                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, ADDEMPLOYEE, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
            return pCount;
        }
        // Ishwar - NISRMS - 27112014 End




        //Siddharth 23-02-2015
        /// <summary>
        /// Gets the client name from Project ID.
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetClientNameFromProjectID(int ProjectID)
        {
            // Initialise Data Access Class object
            DataAccessClass objDAGetProjectName = new DataAccessClass();

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                //Open the connection to DB
                objDAGetProjectName.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] objSqlParameter = new SqlParameter[1];
                objSqlParameter[0] = new SqlParameter(SPParameter.ProjectId, ProjectID);

                //Execute the SP
                objDataReader = objDAGetProjectName.ExecuteReaderSP(SPNames.MRF_GetClientNameFromProjectId, objSqlParameter);

                //Read the data and assign to Collection object
                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    KeyValue<string> keyValue = new KeyValue<string>();


                    if (objDataReader.GetValue(0) != null)
                    {
                        keyValue.KeyName = objDataReader.GetValue(0).ToString();
                    }
                    // Add the object to Collection
                    raveHRCollection.Add(keyValue);
                }

                // Return the Collection
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "Employee.cs", GETPROJECTNAME, EventIDConstants.RAVE_HR_MRF_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDAGetProjectName.CloseConncetion();
            }
        }
        //Siddharth 23-02-2015


        //Siddhesh Arekar Domain Details 09032015 Start
        /// <summary>
        /// Updates the skills details.
        /// </summary>
        /// <param name="objUpdateSkillsDetails">The object update skills details.</param>
        public void UpdateEmployeeDomain(string empDomain, int empId)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[2];

            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    sqlParam[0] = new SqlParameter(SPParameter.EmpId, empId);
                    sqlParam[1] = new SqlParameter(SPParameter.EmployeeDomain, empDomain);
                    objDA.ExecuteNonQuerySP(SPNames.Employee_UpdateEmployeeDomain, sqlParam);

                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "UpdateEmployeeDomain", EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        public BusinessEntities.RaveHRCollection Employee_GetEmployeeDomain(BusinessEntities.Employee objEmp)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[1];
            string str = string.Empty;

            //Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int, 10);
                sqlParam[0].Value = objEmp.EMPId;

                //Execute the SP
                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_Employee_GetEmployeeDomain, sqlParam);

                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    if (Convert.ToString(objDataReader["EmployeeDomain"]) != "")
                    {
                        objEmp = new BusinessEntities.Employee();
                        objEmp.EmployeeDomain = objDataReader["EmployeeDomain"].ToString();
                        raveHRCollection.Add(objEmp);
                    }
                }
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETEMPLOYEE, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
            return raveHRCollection;
        }

        public int Employee_Add_Domain_Master(string empDomain, int groupCategoryId, string createdById, out int categoryID)
        {
            int insertStatus = 0;
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[5];
            categoryID = 0;
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    sqlParam[0] = new SqlParameter(SPParameter.GroupMasterId, groupCategoryId);
                    sqlParam[1] = new SqlParameter(SPParameter.CategoryName, empDomain);
                    sqlParam[2] = new SqlParameter(SPParameter.CreatedById, createdById);
                    sqlParam[3] = new SqlParameter(SPParameter.CategoryID, categoryID);
                    sqlParam[3].Direction = ParameterDirection.Output;
                    sqlParam[4] = new SqlParameter(SPParameter.Status, insertStatus);
                    sqlParam[4].Direction = ParameterDirection.Output;
                    objDA.ExecuteNonQuerySP(SPNames.Insert_T_Master, sqlParam);
                    insertStatus = Convert.ToInt32(sqlParam[4].Value);
                    if (insertStatus == 1)
                    {
                        categoryID = Convert.ToInt32(sqlParam[3].Value);
                    }

                    ts.Complete();
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "Employee_Add_Domain_Master", EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
            return insertStatus;
        }

        //Siddhesh Arekar Domain Details 09032015 End

        public string GetEmployeeBusinessVertical(int EmpId)
        {
            
            string BusinessVertical = string.Empty;
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[1];
            try
            {
            
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, EmpId);
                
                BusinessVertical = objDA.ExecuteScalarSP(SPNames.GetEmployeeBusinessVertical, sqlParam).ToString();

                return BusinessVertical;
                
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "Employee_Add_Domain_Master", EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }



        //Rakesh : Business Vertical Wise Resume Template Download 08/07/2016 Begin 

        public string GetEmployeeBusinessVerticalID(int EmpId)
        {

            string BusinessVertical = string.Empty;
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[1];
            try
            {

                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, EmpId);

                BusinessVertical = objDA.ExecuteScalarSP(SPNames.GetEmployeeBusinessVerticalID, sqlParam).ToString();

                return BusinessVertical;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "Employee_Add_Domain_Master", EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }


        //Rakesh : Business Vertical Wise Resume Template Download 08/07/2016 End



        public int CheckEmployeeIsProjectManager(int EmpId)
        {
            int Count = 0;
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[1];
            try
            {

                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, EmpId);

                Count = Convert.ToInt32(objDA.ExecuteScalarSP(SPNames.CheckEmployeeIsProjectManager, sqlParam));

                return Count;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "Employee_Add_Domain_Master", EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }


        //Rakesh : HOD for Employees 08/07/2016 Begin   
        public BusinessEntities.RaveHRCollection Get_HOD_Employees()
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[1];
            string str = string.Empty;
            BusinessEntities.Employee objEmp = new BusinessEntities.Employee(); 
            //Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                objDA.OpenConnection(DBConstants.GetDBConnectionString());
                //Execute the SP
                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetHeadOfDepartments);

                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object                    
                        objEmp = new BusinessEntities.Employee();
                        objEmp.EMPId = objDataReader["EmpId"].CastToInt32();
                        objEmp.FullName = objDataReader["Name"].CastToString();
                        raveHRCollection.Add(objEmp);                    
                }
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETEMPLOYEE, EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
            return raveHRCollection;
        }


        //Rakesh : HOD for Employees 08/07/2016 End  

    }
}
