//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           Projects.cs       
//  Author:         vineet.kulkarni
//  Date written:   4/3/2009/ 4:37:30 PM
//  Description:    This class  provides the business layer methods for Project module.
//                  
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  19/8/2009 4:37:30 PM  sudip.guha       n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using BusinessEntities;
using Common;
using System.Web;
using Rave.HR.BusinessLayer.Interface;
using Common.AuthorizationManager;
//30053-Aarohi-Start-12012012
using System.Collections;
using System.Data;
using System.Configuration;
//30053-Aarohi-End

namespace Rave.HR.BusinessLayer.Employee
{
    public class Employee
    {
        #region Constants

        const string Empty = "";
        const string CLASS_NAME = "Employee.cs";

        const string forMale = "His";
        const string forFemale = "Her";
        const string PreSalesUK = "PreSales - UK";
        const string PreSalesUSA = "PreSales - USA";
        const string PreSalesIndia = "PreSales - India";
        const string Project = "Project";
        const string Department = "Department";
        const string DepartmentName = "Department Name";
        const string ProjectName = "Project Name";
        private static BusinessEntities.RaveHRCollection raveHRCollection;
        /// <summary>
        /// Rave Email domain
        /// </summary>
        /// 
        //Googleconfigurable
        //private const string RAVE_DOMAIN1 = "@rave-tech.com";
        //private const string RAVE_DOMAIN2 = "@rave-tech.co.in";
        #endregion Constants

        /// <summary>
        /// Adds the existing Employee.
        /// </summary>
        /// <param name="objAddEmployee"></param>
        /// <returns></returns>
        public int AddEmployee(BusinessEntities.Employee objAddEmployee, ref string empCode)
        {
            int empID = 0;

            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objAddEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                empID = objAddEmployeeDAL.AddEmployee(objAddEmployee, ref empCode);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "AddEmployee", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return empID;
        }

        /// <summary>
        /// Adds the existing Employee.
        /// </summary>
        /// <param name="objUpdateEmployee"></param>
        /// <returns></returns>
        public void UpdateEmployee(BusinessEntities.Employee objUpdateEmployee, Boolean IsEmployeeDetailsModified)
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                objUpdateEmployeeDAL.UpdateEmployee(objUpdateEmployee, IsEmployeeDetailsModified);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "UpdateEmployee", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

        }

        //Siddharth 3 April 2015 Start
        /// <summary>
        /// Adds the existing Employee.
        /// </summary>
        /// <param name="objUpdateEmployee"></param>
        /// <returns></returns>
        public void UpdateEmployeeCostCode(DataTable dt, BusinessEntities.Employee objUpdateEmployee)
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                objUpdateEmployeeDAL.UpdateEmployeeCostCode(dt, objUpdateEmployee);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "UpdateEmployeeCostCode", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }



        /// <summary>
        /// Adds the existing Employee.
        /// </summary>
        /// <param name="objUpdateEmployee"></param>
        /// <returns></returns>
        public void UpdateEmployeeCostCode(DataTable dt, int EmpId, int LastModifiedByID)
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                objUpdateEmployeeDAL.UpdateEmployeeCostCode(dt, EmpId, LastModifiedByID);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "UpdateEmployeeCostCode", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }



        public void UpdateEmpCostCodeProjReleaseForCCManager(string EMPId, string ProjectId, string Billing, string CostCode, int LastModifiedByID)
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                objUpdateEmployeeDAL.UpdateEmpCostCodeProjReleaseForCCManager(EMPId, ProjectId, Billing, CostCode, LastModifiedByID );
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "UpdateEmpCostCodeProjReleaseForCCManager", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }
        public void DeleteEmployeeCostcode(string EMPId, string ProjectId, int LastModifiedByID)
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                objUpdateEmployeeDAL.DeleteEmployeeCostcode(EMPId, ProjectId, LastModifiedByID);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "UpdateEmpCostCodeProjReleaseForCCManager", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        public DataTable Employee_GetEmployeeCostCodeByEmpID(int EmpID)
        {
            DataTable dt = null;
            try
            {
                dt = new DataTable();
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                dt = objUpdateEmployeeDAL.Employee_GetEmployeeCostCodeByEmpID(EmpID);
                return dt;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "Employee_GetEmployeeCostCodeByEmpID", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }


        public string Employee_GetEmployeeCostCodeByEmpIDandPrjID(int EmpID, int ProjectID)
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                return objUpdateEmployeeDAL.Employee_GetEmployeeCostCodeByEmpIDandPrjID(EmpID, ProjectID);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "Employee_GetEmployeeCostCodeByEmpIDandPrjID", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }



        public string Employee_GetEmployeeCostCodeByEmpIDandNoPrjID(int EmpID)
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                return objUpdateEmployeeDAL.Employee_GetEmployeeCostCodeByEmpIDandNoPrjID(EmpID);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "Employee_GetEmployeeCostCodeByEmpIDandNoPrjID", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }





        public int Employee_GetWindowsUsenameofLoggedInUserForCCManager(string WindowsUsername)
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                return objUpdateEmployeeDAL.Employee_GetWindowsUsenameofLoggedInUserForCCManager(WindowsUsername);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "Employee_GetWindowsUsenameofLoggedInUserForCCManager", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }


        //Siddharth 3 April 2015 End


        /// <summary>
        /// Get the existing Employee.
        /// </summary>
        /// <param name="objUpdateEmployee"></param>
        /// <returns></returns>
        public BusinessEntities.Employee GetEmployee(BusinessEntities.Employee objUpdateEmployee)
        {
            BusinessEntities.Employee employee = null;
            try
            {
                employee = new BusinessEntities.Employee();
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                employee = objUpdateEmployeeDAL.GetEmployee(objUpdateEmployee);
                // 29637-Ambar-Added following IF Condition.
                //CR - 29936 On exit employee his name to be removed from mailing list Sachin Start
                //if (employee.ReportingTo != null)   add validation of employee for more safe code
                if (employee != null && employee.ReportingTo != null)
                //CR - 29936 On exit employee his name to be removed from mailing list Sachin End
                {
                    if (employee.ReportingTo.EndsWith(","))
                    {
                        employee.ReportingTo = employee.ReportingTo.Substring(0, employee.ReportingTo.Length - 1);
                    }
                    else
                    {
                        if (employee.ReportingTo.EndsWith(", "))
                        {
                            employee.ReportingTo = employee.ReportingTo.Substring(0, employee.ReportingTo.Length - 2);
                        }
                    }
                }
                // 29637-Ambar-Added following IF Condition.
                //CR - 29936 On exit employee his name to be removed from mailing list Sachin Start
                //if (employee.ReportingTo != null)   add validation of employee for more safe code
                if (employee != null && employee.ReportingToFM != null)
                //CR - 29936 On exit employee his name to be removed from mailing list Sachin End
                {
                    if (employee.ReportingToFM.EndsWith(","))
                    {
                        employee.ReportingToFM = employee.ReportingToFM.Substring(0, employee.ReportingToFM.Length - 1);
                    }
                    else
                    {
                        if (employee.ReportingToFM.EndsWith(", "))
                        {
                            employee.ReportingToFM = employee.ReportingToFM.Substring(0, employee.ReportingToFM.Length - 2);
                        }
                    }
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "UpdateEmployee", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return employee;
        }

        /// <summary>
        /// Get the existing Employee by Email Id.
        /// </summary>
        /// <param name="objUpdateEmployee"></param>
        /// <returns></returns>
        public BusinessEntities.Employee GetEmployeeByEmpId(BusinessEntities.Employee objUpdateEmployee)
        {
            BusinessEntities.Employee employee = null;
            try
            {
                employee = new BusinessEntities.Employee();
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                employee = objUpdateEmployeeDAL.GetEmployeeByEmailId(objUpdateEmployee);

                //CR - 29936 On exit employee his name to be removed from mailing list Sachin Start
                //if (employee.ReportingTo != null)   add validation of employee for more safe code
                if (employee != null && employee.ReportingTo != null)
                //CR - 29936 On exit employee his name to be removed from mailing list Sachin End
                {
                    if (employee.ReportingTo.EndsWith(","))
                    {
                        employee.ReportingTo = employee.ReportingTo.Substring(0, employee.ReportingTo.Length - 1);
                    }
                    else
                    {
                        if (employee.ReportingTo.EndsWith(", "))
                        {
                            employee.ReportingTo = employee.ReportingTo.Substring(0, employee.ReportingTo.Length - 2);
                        }
                    }
                }

                //CR - 29936 On exit employee his name to be removed from mailing list Sachin Start
                //if (employee.ReportingTo != null)   add validation of employee for more safe code
                if (employee != null && employee.ReportingToFM != null)
                //CR - 29936 On exit employee his name to be removed from mailing list Sachin End
                {
                    if (employee.ReportingToFM.EndsWith(","))
                    {
                        employee.ReportingToFM = employee.ReportingToFM.Substring(0, employee.ReportingToFM.Length - 1);
                    }
                    else
                    {
                        if (employee.ReportingToFM.EndsWith(", "))
                        {
                            employee.ReportingToFM = employee.ReportingToFM.Substring(0, employee.ReportingToFM.Length - 2);
                        }
                    }
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "UpdateEmployee", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return employee;
        }

        /// <summary>
        /// Get the existing Employee List.
        /// </summary>
        /// <param name="objUpdateEmployee"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetEmployeesList(string resourceName)
        {
            BusinessEntities.RaveHRCollection employeesList = null;
            try
            {
                employeesList = new BusinessEntities.RaveHRCollection();
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                employeesList = objUpdateEmployeeDAL.GetEmployeeList(resourceName);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetEmployeesList", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return employeesList;
        }


        /// <summary>
        /// Get the existing Employee List.
        /// </summary>
        /// <param name="objUpdateEmployee"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetEmployeesView(string view, int managerEmpId, string resourceName)
        {
            BusinessEntities.RaveHRCollection employeesList = null;
            try
            {
                employeesList = new BusinessEntities.RaveHRCollection();
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                employeesList = objUpdateEmployeeDAL.GetEmployeeView(view, managerEmpId, resourceName);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetEmployeesList", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return employeesList;
        }



        /// <summary>
        /// MRF Code for dropDown in Add Employee page
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection FillMRFCodeDropDowns()
        {
            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                Rave.HR.DataAccessLayer.Employees.Employee employee = new Rave.HR.DataAccessLayer.Employees.Employee();
                raveHRCollection = employee.GetMRFCode();
                return raveHRCollection;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "FillMRFCodeDropDowns", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Update the existing Employee with MRFCode in the T_MRFCodeDetail.
        /// </summary>
        /// <param name="objAddEmployee"></param>
        /// <returns></returns>
        public void UpdateEmployeeMRFCode(BusinessEntities.Employee objAddEmployee, int mrfStatus)
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objAddEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                objAddEmployeeDAL.UpdateEmployeeMRFCode(objAddEmployee, mrfStatus);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "AddEmployee", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

        }

        /// <summary>
        /// Get the existing Employee Summary.
        /// </summary>
        /// <param name="objUpdateEmployee"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetEmployeesSummary(BusinessEntities.ParameterCriteria objParameter, BusinessEntities.Employee employee, ref int pageCount)
        {
            BusinessEntities.RaveHRCollection employeesList = null;
            try
            {
                employeesList = new BusinessEntities.RaveHRCollection();
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                employeesList = objUpdateEmployeeDAL.GetEmployeesSummary(objParameter, employee, ref pageCount);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetEmployeesList", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return employeesList;
        }

        /// <summary>
        /// Project Name dropDown in Employee summary page
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection FillProjectNameDropDowns()
        {
            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                Rave.HR.DataAccessLayer.Employees.Employee employee = new Rave.HR.DataAccessLayer.Employees.Employee();
                raveHRCollection = employee.GetProjectName();
                return raveHRCollection;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "FillProjectNameDropDowns", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }


        /// <summary>
        /// Project Name dropDown in Employee summary page
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetProjectNameForEmpByEmpID(int empid)
        {
            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                Rave.HR.DataAccessLayer.Employees.Employee employee = new Rave.HR.DataAccessLayer.Employees.Employee();
                raveHRCollection = employee.GetProjectNameForEmpByEmpID(empid);
                return raveHRCollection;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "FillProjectNameDropDowns", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }



        /// <summary>
        /// Get the existing Employee release status list.
        /// </summary>
        /// <param name="objUpdateEmployee"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetEmployeesReleaseStatus(BusinessEntities.ParameterCriteria objParameter, BusinessEntities.Employee employee, ref int pageCount)
        {
            BusinessEntities.RaveHRCollection employeesList = null;
            try
            {
                employeesList = new BusinessEntities.RaveHRCollection();
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                employeesList = objUpdateEmployeeDAL.GetEmployeesReleaseStatus(objParameter, employee, ref pageCount);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetEmployeesList", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return employeesList;
        }

        /// Added new Function to solved issue no 20769
        /// Start the function
        /// <summary>
        /// Get the information about the Project Allocation for the Employee.
        /// </summary>
        /// <param name="objUpdateEmployee"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetEmployeesResourcePlan(BusinessEntities.Employee employee)
        {
            BusinessEntities.RaveHRCollection employeesList = null;

            try
            {
                employeesList = new BusinessEntities.RaveHRCollection();
                Rave.HR.DataAccessLayer.Employees.Employee EmpResoursePlan = new Rave.HR.DataAccessLayer.Employees.Employee();
                employeesList = EmpResoursePlan.GetEmployeesResourcePlan(employee);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetEmployeesList", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return employeesList;
        }
        ///End of function

        /// <summary>
        /// Updates the existing Employee.
        /// </summary>
        /// <param name="objUpdateEmployee"></param>
        /// <returns></returns>
        public void UpdateEmployeeReleaseStatus(BusinessEntities.Employee objUpdateEmployee, ref Boolean IsProjectClosed)
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                objUpdateEmployeeDAL.UpdateEmployeeReleaseStatus(objUpdateEmployee, ref IsProjectClosed);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "UpdateEmployeeReleaseStatus", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

        }


        //Siddharth 7 April 2015 Start

        /// <summary>
        /// Updates the Release Status Employee.
        /// </summary>
        /// <param name="objUpdateEmployee"></param>
        /// <returns></returns>
        public void Employee_UpdateEmpCostCodeProjRelease(BusinessEntities.Employee objUpdateEmployee)
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                objUpdateEmployeeDAL.Employee_UpdateEmpCostCodeProjRelease(objUpdateEmployee);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "UpdateEmployeeReleaseStatus", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

        }
        //Siddharth 7 April 2015 End


        // CR - 25715 issue related to ePlatform MRF Sachin
        /// <summary>
        /// Get Number Allocated Employee & Pending MRF count
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="NoofAllocatedEmployee"></param>
        /// <param name="NoofMRF"></param>
        public void CheckLastEmployeeRelease(int ProjectId, ref int NoofAllocatedEmployee, ref int NoofMRF)
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objCheckLastEmployeeReleaseDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                objCheckLastEmployeeReleaseDAL.CheckLastEmployeeRelease(ProjectId, ref NoofAllocatedEmployee, ref NoofMRF);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "CheckLastEmployeeRelease", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }
        // CR - 25715 Sachin End

        /// <summary>
        /// Delete the existing Employee.
        /// </summary>
        /// <param name="objUpdateEmployee"></param>
        /// <returns></returns>
        public void DeleteEmployee(BusinessEntities.Employee objDeleteEmployee)
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                objUpdateEmployeeDAL.DeleteEmployee(objDeleteEmployee);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "DeleteEmployee", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

        }

        /// <summary>
        /// Check the existing Employee.
        /// </summary>
        /// <param name="objEmployee"></param>
        /// <returns></returns>
        public bool IsEmployeeEmailExists(BusinessEntities.Employee objEmployee)
        {
            bool isExist = false;

            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                isExist = objUpdateEmployeeDAL.IsEmployeeEmailExists(objEmployee);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "IsEmployeeEmailExists", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return isExist;
        }

        /// <summary>
        /// Updates the employee resignation details.
        /// </summary>
        /// <param name="objUpdateEmployee">The obj update employee.</param>
        public void UpdateEmployeeResignationDetails(BusinessEntities.Employee objUpdateEmployee)
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                objUpdateEmployeeDAL.UpdateEmployeeResignationDetails(objUpdateEmployee);
                //RaveHRCollection raveHRCollection = new RaveHRCollection();
                //raveHRCollection = objUpdateEmployeeDAL.UpdateRecruiterID(objUpdateEmployee);
                //if (raveHRCollection.Count > 0)
                //    SendMailForRecruiterChangeWithMRFPending(raveHRCollection);
                //else
                //    SendMailForRecruiterChangeWithNoMRFPending();
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "UpdateEmployeeResignationDetails", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

        }

        private string GetHTMLForTableData(RaveHRCollection objGetRPDetailsForMail)
        {

            string bodyTable = "";

            bodyTable += "<table cellspacing = '0' style = 'fontstyle:Bold;'><tr><td style = 'fontstyle:Bold;	font-size: 9pt;	padding-left: 5px;vertical-align:top;'>Resource Edited Details:<td/><tr/><table>";

            string multipleProjects = objGetRPDetailsForMail.Count == 1 ? "Details is" : "Details are";

            bodyTable = string.Format(bodyTable, multipleProjects);

            if (objGetRPDetailsForMail.Count > 0)
            {

                string[] header = new string[10];

                header[0] = "MRF Code";

                header[1] = "Resource Plan Code";

                header[2] = "Role";

                header[3] = "Project Name";

                header[4] = "Resource On Board";

                header[5] = "Raised By";

                header[6] = "Status";

                header[7] = "Department";

                header[8] = "Client Name";

                header[9] = "Recruitment Manager";

                string[,] arrayData = new string[(objGetRPDetailsForMail.Count), 10];

                int rowCounter = 0;

                foreach (BusinessEntities.MRFDetail mrfDetails in objGetRPDetailsForMail)
                {

                    arrayData[rowCounter, 0] = mrfDetails.MRFCode;

                    arrayData[rowCounter, 1] = mrfDetails.RPCode.ToString();

                    arrayData[rowCounter, 2] = mrfDetails.Role.ToString();

                    arrayData[rowCounter, 3] = mrfDetails.ProjectName.ToString();

                    arrayData[rowCounter, 4] = mrfDetails.ResourceOnBoard.ToString(CommonConstants.DATE_FORMAT);

                    arrayData[rowCounter, 5] = mrfDetails.RaisedBy.ToString();

                    arrayData[rowCounter, 6] = mrfDetails.Status.ToString();

                    arrayData[rowCounter, 7] = mrfDetails.DepartmentName.ToString();

                    arrayData[rowCounter, 8] = mrfDetails.ClientName.ToString(); ;

                    arrayData[rowCounter, 9] = mrfDetails.RecruitmentManager.ToString();

                    rowCounter++;
                }



                IEmailTableData objEmailTableData = new EmailTableData();

                objEmailTableData.Header = header;

                objEmailTableData.RowDetail = arrayData;

                objEmailTableData.RowCount = rowCounter;

                bodyTable += objEmailTableData.GetTableData(objEmailTableData);

                if (rowCounter <= 0)
                {
                    bodyTable = "";
                }
            }

            return bodyTable;

        }

        private void SendMailForRecruiterChangeWithNoMRFPending()
        {

        }

        private void SendMailForRecruiterChangeWithMRFPending(RaveHRCollection raveHRCollection)
        {

        }

        /// <summary>
        /// Get Employee's Designation by Departmentwise
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetEmployeesDesignations(int departmentId)
        {
            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                Rave.HR.DataAccessLayer.Employees.Employee employeeDL = new Rave.HR.DataAccessLayer.Employees.Employee();
                raveHRCollection = employeeDL.GetEmployeesDesignations(departmentId);
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "UpdateEmployeeResignationDetails", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Check the existing Employee.
        /// </summary>
        /// <param name="objEmployee"></param>
        /// <returns></returns>
        public int IsEmployeeDataExists(BusinessEntities.Employee objEmployee)
        {
            int empCount = 0;

            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                empCount = objUpdateEmployeeDAL.IsEmployeeDataExists(objEmployee);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "IsEmployeeEmailExists", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return empCount;
        }

        /// <summary>
        /// Gets the managers email id.
        /// </summary>
        /// <param name="EmpID">The emp ID.</param>
        /// <returns></returns>
        public string GetProjectManagersEmailId(BusinessEntities.Employee objEmployee, ref string ProjectManagerIds)
        {
            //Object declaration of SkillsDetails class
            Rave.HR.DataAccessLayer.Employees.Employee objEmployeeDAL;

            try
            {
                //Created new instance of employee class to call GetProjectManagersEmailId() of Data access layer
                objEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();

                //Call to GetSkillsDetails() of Data access layer and return the Skills
                return objEmployeeDAL.GetProjectManagersEmailId(objEmployee, ref ProjectManagerIds);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetProjectManagersEmailId", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Header Style
        /// </summary>
        private const string strHeaderStyle = "height: 25px;background-color: #BFBFBF;font-size: 9pt;font-weight: bold;padding-left: 5px;font-family:Verdana;vertical-align:top;";

        /// <summary>
        /// Row Style
        /// </summary>
        private const string strRowStyle = "font-family: Verdana;	font-size: 9pt;	padding-left: 5px;vertical-align:top;";

        /// <summary>
        /// Export employee details to excel.
        /// </summary>
        /// 
        //Issue Id : 34521 START Mahendra 
        //commented EmployeeDetailsInExcel() and add parameter employee
        public void EmployeeDetailsInExcel(BusinessEntities.Employee employee)
        {
            try
            {
                StringBuilder strbHTML = new StringBuilder();

                //--Create html for excel
                strbHTML.Append("<table border = '0' cellpadding = '0' cellspacing = '0' style='border-color:Black;' width = '100%'");
                strbHTML.Append("<tr><td>&nbsp;</td></tr>");
                strbHTML.Append("<tr>");
                strbHTML.Append("<td width = '80%'>");

                //--******************Create Table Header********************************/

                strbHTML.Append("<table border = '2' cellpadding = '0' cellspacing = '0' style='border-color:Black;' width = '100%'");
                strbHTML.Append("<tr>");
                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>EmpCode</b></td>");
                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>EmailId </b></td>");
                strbHTML.Append("<td align = 'center' width = '5%' style='" + strHeaderStyle + "'><b>First Name</b></td>");
                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>Last Name</b></td>");
                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>Gender</b></td>");

                //Mohamed : Issue 40424 : 07/02/2013 : Starts                        			  
                //Desc :  "In Employee Detail Report Add 2 new Columns 1. Date of Birth 2. Mobile Number"

                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>DOB</b></td>");
                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>MobileNo</b></td>");
                //Mohamed : Issue 40424 : 07/02/2013 : Ends

                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>Band</b></td>");
                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>Department</b></td>");
                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>Designation</b></td>");
                // 31009-Ambar-30122011-Start
                // Changed Report Column Name
                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>Line Manager</b></td>");
                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>Line Manager Email ID </b></td>");
                //strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>Reporting To</b></td>");
                // 31009-Ambar-30122011-End
                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>Functional Manager</b></td>");
                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>Location</b></td>");
                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>Date of Joining</b></td>");
                strbHTML.Append("<td align = 'center' width = '5%' style='" + strHeaderStyle + "'><b>External Work Exp(yr)</b></td>");
                strbHTML.Append("<td align = 'center' width = '5%' style='" + strHeaderStyle + "'><b>Rave Work Exp(yr)</b></td>");
                strbHTML.Append("<td align = 'center' width = '5%' style='" + strHeaderStyle + "'><b>Total Work Exp(yr)</b></td>");
                
                // Rakesh : Issue 58054: 23/May/2016 : Starts 
                strbHTML.Append("<td align = 'center' width = '5%' style='" + strHeaderStyle + "'><b>External Work Exp(Months)</b></td>");
                strbHTML.Append("<td align = 'center' width = '5%' style='" + strHeaderStyle + "'><b>Rave Work Exp(Months)</b></td>");
                strbHTML.Append("<td align = 'center' width = '5%' style='" + strHeaderStyle + "'><b>Total Work Exp(Months)</b></td>");
                //END

                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>Primary Skill</b></td>");

                //Rajnikant: Issue 45558 : 18/09/2014 : Starts                        			  
                //Desc :  Get project name of an employee
                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>Project Name</b></td>");
                //Rajnikant: Issue 45558 : 18/09/2014 : Ends


                //Rakesh : Department head HOD, Project wise HOD 15/July/2016 Begin   
                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>HOD Name</b></td>");
                //Rakesh : Department head HOD, Project wise HOD 15/July/2016 End

                //Siddharth 3rd June 2015 Starts                        			  
                //Desc :  Get project joining date of an employee
                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>Project Joining Date</b></td>");
                //Siddharth 3rd June 2015 Ends 


                //Ishwar NISRMS 28112014 Starts
                //Desc :  Get CostCode of an EDC Employee
                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>Cost Code</b></td>");
                //Ishwar NISRMS 28112014 Ends


                //Siddharth 22nd June 2015 Starts                        			  
                //Desc :  Get project joining date of an employee
                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>Business Vertical</b></td>");
                //Siddharth 22nd June 2015 Ends 


                // Ishwar NISRMS 17032015 Start
                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>Resignation Date</b></td>");
                // Ishwar NISRMS 17032015 End

                //Siddharth 13th Oct 2015 Starts                        			  
                //Desc :  Get Domains of an employee
                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>Domain</b></td>");
                //Siddharth 13th Oct 2015 Ends 

                //Siddharth 17th Nov 2015 Starts                        			  
                //Desc :  Get Domains of an employee
                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>Resource Business Unit</b></td>");
                //Siddharth 17th Nov 2015 Ends 


                // Rakesh  : Commented Duplicate Entries
             //   strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>Line Manager Email ID </b></td>");
                //END

                strbHTML.Append("</tr>");

                //--Get the active employee details
                RaveHRCollection objActiveEmployeeDetails = new RaveHRCollection();
                Rave.HR.DataAccessLayer.Employees.Employee objDALEmployee = new Rave.HR.DataAccessLayer.Employees.Employee();
                //Issue Id : 34521 START Mahendra 
                //objActiveEmployeeDetails = objDALEmployee.GetActiveEmployeeDetails();
                objActiveEmployeeDetails = objDALEmployee.GetActiveEmployeeDetails(employee);
                //Issue Id : 34521 END Mahendra 

                foreach (BusinessEntities.Employee objBEEmployee in objActiveEmployeeDetails)
                {
                    strbHTML.Append("<tr>");
                    strbHTML.Append("<td align = 'center' style='" + strRowStyle + "'>" + objBEEmployee.EMPCode + "</td>");
                    strbHTML.Append("<td align = 'left' style='" + strRowStyle + "'>" + objBEEmployee.EmailId + "</td>");
                    strbHTML.Append("<td align = 'left' style='" + strRowStyle + "'>" + objBEEmployee.FirstName + "</td>");
                    strbHTML.Append("<td align = 'left' style='" + strRowStyle + "'>" + objBEEmployee.LastName + "</td>");
                    strbHTML.Append("<td align = 'left' style='" + strRowStyle + "'>" + objBEEmployee.Gender + "</td>");

                    //Mohamed : Issue 40424 : 07/02/2013 : Starts                        			  
                    //Desc :  "In Employee Detail Report Add 2 new Columns 1. Date of Birth 2. Mobile Number"

                    //Mohamed : Issue 41810 : 16/04/2013 : Starts                        			  
                    //Desc :  "Employee Summary : In export to excel "Date of Joining is not display properly" eg: it is  "MM/DD/YYYY" instead of "DD/MM/YYYY""

                    //strbHTML.Append("<td align = 'center' style='" + strRowStyle + "'>" + objBEEmployee.DOB.ToString(CommonConstants.DATE_FORMAT) + "</td>");
                    //strbHTML.Append("<td align = 'center' style='" + strRowStyle + @"mso-number-format:dd-mmm-yyyy;'>" + objBEEmployee.DOB + "</td>");

                    //Rajnikant: Issue 45558 : 18/09/2014 : Starts                        			  
                    //Desc :  Change format of Joining date
                    strbHTML.Append("<td align = 'center' style='" + strRowStyle + @"mso-number-format:dd-mmm-yyyy;'>" + objBEEmployee.DOB.ToString("dd-MMM-yyyy") + "</td>");
                    //Rajnikant: Issue 45558 : 18/09/2014 : End

                    //Mohamed : Issue 41810 : 16/04/2013 : Starts                        			  


                    strbHTML.Append("<td align = 'center' style='" + strRowStyle + @"mso-number-format:\@;'>" + objBEEmployee.MobileNo + "</td>");
                    //Mohamed : Issue 40424 : 07/02/2013 : Ends

                    strbHTML.Append("<td align = 'center' style='" + strRowStyle + "'>" + objBEEmployee.BandName + "</td>");
                    strbHTML.Append("<td align = 'left' style='" + strRowStyle + "'>" + objBEEmployee.Department + "</td>");
                    strbHTML.Append("<td align = 'left' style='" + strRowStyle + "'>" + objBEEmployee.Designation + "</td>");
                    strbHTML.Append("<td align = 'left' style='" + strRowStyle + "'>" + objBEEmployee.ReportingTo + "</td>");
                    strbHTML.Append("<td align = 'left' style='" + strRowStyle + "'>" + objBEEmployee.LineManagerEmailId + "</td>");
                    strbHTML.Append("<td align = 'left' style='" + strRowStyle + "'>" + objBEEmployee.ReportingToFM + "</td>");
                    strbHTML.Append("<td align = 'center' style='" + strRowStyle + "'>" + objBEEmployee.Location + "</td>");
                    //Mohamed : Issue 41810 : 16/04/2013 : Starts                        			  
                    //Desc :  "Employee Summary : In export to excel "Date of Joining is not display properly" eg: it is  "MM/DD/YYYY" instead of "DD/MM/YYYY""

                    //strbHTML.Append("<td align = 'center' style='" + strRowStyle + "'>" + objBEEmployee.JoiningDate.ToString(CommonConstants.DATE_FORMAT) + "</td>");
                    //strbHTML.Append("<td align = 'center' style='" + strRowStyle + @"mso-number-format:dd-mmm-yyyy;'>" + objBEEmployee.JoiningDate + "</td>");
                    //Mohamed : Issue 41810 : 16/04/2013 : Starts

                    //Rajnikant: Issue 45558 : 18/09/2014 : Starts                        			  
                    //Desc :  Change format of Joining date
                    strbHTML.Append("<td align = 'center' style='" + strRowStyle + @"mso-number-format:dd-mmm-yyyy;'>" + objBEEmployee.JoiningDate.ToString("dd-MMM-yyyy") + "</td>");
                    //Rajnikant: Issue 45558 : 18/09/2014 : Ends

                    //Mohamed : Issue 40225 : 28/01/2013 : Starts                        			  
                    //Desc :  "Export to Excel experience not seen properly eg. 1.10 display as 1.1"


                    //strbHTML.Append("<td align = 'right' style='" + strRowStyle + "'>" + objBEEmployee.ExternalWorkExp + "</td>");
                    //strbHTML.Append("<td align = 'right' style='" + strRowStyle + "'>" + objBEEmployee.InternalWorkExperience + "</td>");
                    //strbHTML.Append("<td align = 'right' style='" + strRowStyle + "'>" + objBEEmployee.TotalWorkExperience + "</td>");

                    strbHTML.Append("<td align = 'right' style='" + strRowStyle + @"mso-number-format:\@;'>" + objBEEmployee.ExternalWorkExp + "</td>");
                    strbHTML.Append("<td align = 'right' style='" + strRowStyle + @"mso-number-format:\@;'>" + objBEEmployee.InternalWorkExperience + "</td>");
                    strbHTML.Append("<td align = 'right' style='" + strRowStyle + @"mso-number-format:\@;'>" + objBEEmployee.TotalWorkExperience + "</td>");

                    // Rakesh : Issue 58054: 23/May/2016 : Starts 

                    strbHTML.Append("<td align = 'right' style='" + strRowStyle + @"mso-number-format:\@;'>" + objBEEmployee.ExternalWorkExpInMonths + "</td>");                    
                    strbHTML.Append("<td align = 'right' style='" + strRowStyle + @"mso-number-format:\@;'>" + objBEEmployee.InternalWorkExperienceInMonths + "</td>");
                    strbHTML.Append("<td align = 'right' style='" + strRowStyle + @"mso-number-format:\@;'>" + objBEEmployee.TotalWorkExperienceInMonths + "</td>");
                    
                    //END


                    //Mohamed : Issue 40225 : 28/01/2013 : Ends				                      

                    strbHTML.Append("<td align = 'left' style='" + strRowStyle + "'>" + objBEEmployee.EmployeePrimarySkill + "</td>");


                    //Rajnikant: Issue 45558 : 18/09/2014 : Starts                        			  
                    //Desc :  Get project name of an employee
                    strbHTML.Append("<td align = 'left' style='" + strRowStyle + "'>" + objBEEmployee.ProjectName + "</td>");
                    //Rajnikant: Issue 45558 : 18/09/2014 : Ends


                    strbHTML.Append("<td align = 'left' style='" + strRowStyle + "'>" + objBEEmployee.ProjectHeadName + "</td>");

                    //Siddharth 3rd June 2015 Starts                        			  
                    //Desc :  Get project joining date of an employee
                    strbHTML.Append("<td align = 'left' style='" + strRowStyle + "'>" + objBEEmployee.ProjectJoiningDate + "</td>");
                    //Siddharth 3rd June 2015 Starts 

                    //Ishwar NISRMS 28112014 Starts
                    //Desc :  Get CostCode of an EDC Employee
                    strbHTML.Append("<td align = 'left' style='" + strRowStyle + "'>" + objBEEmployee.CostCode + "</td>");
                    //Ishwar NISRMS 28112014 Ends


                    //Siddharth 22nd June 2015 Starts                        			  
                    //Desc :  Get project joining date of an employee
                    strbHTML.Append("<td align = 'left' style='" + strRowStyle + "'>" + objBEEmployee.BusinessVertical + "</td>");
                    //Siddharth 22nd June 2015 Ends 

                    // Ishwar NISRMS 17032015 Start
                    string ResignationDate = string.Empty;
                    if (objBEEmployee.ResignationDate.ToString("dd-MMM-yyyy") == "01-Jan-0001")
                    {
                        ResignationDate = "-";
                    }
                    else
                    {
                        ResignationDate = objBEEmployee.ResignationDate.ToString("dd-MMM-yyyy");
                    }
                    strbHTML.Append("<td align = 'center' style='" + strRowStyle + @"mso-number-format:dd-mmm-yyyy;'>" + ResignationDate + "</td>");
                    // Ishwar NISRMS 17032015 End

                    //Siddharth 13 Oct 2015 Starts                        			  
                    //Desc :  Get Domains of an employee
                    strbHTML.Append("<td align = 'left' style='" + strRowStyle + "'>" + objBEEmployee.Domain + "</td>");
                    //Siddharth 13 Oct 2015 Ends 

                    //Siddharth 17 Nov 2015 Starts                        			  
                    //Desc :  Get RBU of an employee
                    strbHTML.Append("<td align = 'left' style='" + strRowStyle + "'>" + objBEEmployee.RBU + "</td>");
                    //Siddharth 17 Nov 2015 Ends 

                    // Rakesh  : Commented Duplicate Entries
                   // strbHTML.Append("<td align = 'left' style='" + strRowStyle + "'>" + objBEEmployee.LineManagerEmailId + "</td>");
                    //END

                    strbHTML.Append("</tr>");
                }

                //--Generate Excel
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=RaveEmployees.xls");
                HttpContext.Current.Response.Charset = Empty;
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";

                HttpContext.Current.Response.AddHeader("Content-Length", strbHTML.Length.ToString());
                HttpContext.Current.Response.Write(strbHTML.ToString());
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();

                //HttpContext.Current.Response.Write(strbHTML.ToString());
                ////HttpContext.Current.Response.End();
                //HttpContext.Current.Response.Flush();
                //HttpContext.Current.Response.Close();
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (System.Threading.ThreadAbortException ex) { }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "EmployeeDetailsInExcel", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }


        public string GetDepartmentHeadEmailId(BusinessEntities.Employee objEmployee)
        {
            //Object declaration of SkillsDetails class
            Rave.HR.DataAccessLayer.Employees.Employee objEmployeeDAL;

            try
            {
                //Created new instance of employee class to call GetProjectManagersEmailId() of Data access layer
                objEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();

                //Call to GetSkillsDetails() of Data access layer and return the Skills
                return objEmployeeDAL.GetDepartmentHeadEmailId(objEmployee);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetProjectManagersEmailId", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Gets the projects on which an employee is currently working.
        /// </summary>
        /// <param name="empId">ID of Employee.</param>
        /// <returns>List<BusinessEntity.Employee></returns>
        public List<BusinessEntities.Employee> BindGridDetailedEmployeeSummary(int empID)
        {
            try
            {
                List<BusinessEntities.Employee> objEmployeeEntity = new List<BusinessEntities.Employee>();

                Rave.HR.DataAccessLayer.Employees.Employee objEmployee = new Rave.HR.DataAccessLayer.Employees.Employee();

                return objEmployee.GetProjectByEmployee(empID);
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetProjectManagersEmailId", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Checks wether employee is department head
        /// </summary>
        public static Boolean CheckDepartmentHeadbyEmailId(string emailId)
        {

            //Object declaration of SkillsDetails class
            Rave.HR.DataAccessLayer.Employees.Employee objEmployeeDAL;

            try
            {
                //Created new instance of employee class to call GetProjectManagersEmailId() of Data access layer
                objEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();

                //Call to GetSkillsDetails() of Data access layer and return the Skills
                return objEmployeeDAL.CheckDepartmentHeadbyEmailId(emailId);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetProjectManagersEmailId", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

        }


        public static bool CheckDepartmentHeadbyEmailId()
        {
            throw new NotImplementedException();
        }

        public void SendMailForAddEmployee(BusinessEntities.Employee AddEmployee)
        {
            Utility objEmailData = new Utility();
            try
            {
                AuthorizationManager authoriseduser = new AuthorizationManager();
                string LoggedInUserMailId = authoriseduser.getLoggedInUser();

                string fullName = String.Concat(AddEmployee.FirstName, " ", AddEmployee.LastName);

                IRMSEmail obj;

                if (AddEmployee.Location == CommonConstants.BENGULURU)
                    obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Employee),
                                                                Convert.ToInt16(EnumsConstants.EmailFunctionality.AddEmployee),
                                                                CommonConstants.BENGULURU);

                else

                    obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Employee),
                                                Convert.ToInt16(EnumsConstants.EmailFunctionality.AddEmployee));


                // Venkatesh : 47714  : 22/Jan/2014 : Starts                    
                // Desc : Add Accoutable Person in MailTO 
                if (AddEmployee.MRFId != null)
                {
                    string AccountableEmailTo = "";

                    BusinessEntities.MRFDetail MRFDetailobject = new BusinessEntities.MRFDetail();
                    Rave.HR.BusinessLayer.MRF.MRFDetail mrfDetailsBll = new Rave.HR.BusinessLayer.MRF.MRFDetail();
                    MRFDetailobject = mrfDetailsBll.GetMrfMoveDetail(Convert.ToInt32(AddEmployee.MRFId));
                    if (MRFDetailobject.ReportingToId != null)
                    {
                        AccountableEmailTo = new Rave.HR.BusinessLayer.Contracts.Contract().GetEmailID(Convert.ToInt32(MRFDetailobject.ReportingToId));
                        obj.CC.Add(AccountableEmailTo);
                    }
                }
                // Venkatesh : 47714  : 22/Jan/2014 : End


                obj.CC.Add(LoggedInUserMailId);

                obj.Subject = string.Format(obj.Subject, AddEmployee.EMPCode,
                                                         fullName);
                obj.Body = string.Format(obj.Body, AddEmployee.MRFcode,
                                                   AddEmployee.Designation,
                                                   AddEmployee.Department,
                                                   AddEmployee.EmployeeType);

                obj.SendEmail(obj);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "SendMailForAddEmployee", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        public void SendMailForAddEmployeeToUpdateDetails(BusinessEntities.Employee AddEmployee)
        {
            Utility objEmailData = new Utility();
            try
            {
                AuthorizationManager authoriseduser = new AuthorizationManager();
                string LoggedInUserMailId = authoriseduser.getLoggedInUser();
                //Google change to northgate
                //string strFrom = "";
                string username = "";
                //GoogleMail
                //if (LoggedInUserMailId.ToLower().Contains(RAVE_DOMAIN1))
                //{
                //    strFrom = LoggedInUserMailId.Replace(RAVE_DOMAIN1, Empty);
                //    username = authoriseduser.GetDomainUserName(strFrom.Replace(RAVE_DOMAIN2, Empty));
                //}
                //else
                //{
                //    strFrom = LoggedInUserMailId.Replace(RAVE_NORTHGATE_DOMAIN1, Empty);
                //    username = authoriseduser.GetDomainUserName(strFrom.Replace(AuthorizationManagerConstants.NORTHGATEDOMAIN, Empty));
                //}
                username = authoriseduser.GetDomainUserName(LoggedInUserMailId);

                //link to Update the employee Details.
                string strEmployeeDetails = GetLinkForEmailForUpdateDetailsForNewEmployee(AddEmployee.EMPId);

                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Employee),
                                            Convert.ToInt16(EnumsConstants.EmailFunctionality.EmployeeDetailsUpdate));

                obj.To.Add(AddEmployee.EmailId);

                obj.Subject = string.Format(obj.Subject);

                obj.Body = string.Format(obj.Body, AddEmployee.FirstName,
                                                   strEmployeeDetails,
                                                   strEmployeeDetails,
                                                   username);

                obj.SendEmail(obj);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "SendMailForAddEmployeeToUpdateDetails", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        public void SendMailForEditEmployee(BusinessEntities.Employee AddEmployee, BusinessEntities.Employee prevEmployeeDetails)
        {
            Utility objEmailData = new Utility();
            try
            {
                List<string> prev = new List<string>();
                List<string> curr = new List<string>();
                AuthorizationManager authoriseduser = new AuthorizationManager();
                string LoggedInUserMailId = authoriseduser.getLoggedInUser();

                string fullName = String.Concat(AddEmployee.FirstName, " ", AddEmployee.LastName);


                if (AddEmployee.Prefix != prevEmployeeDetails.Prefix)
                {
                    prev.Add(String.Concat("Prefix: ", (Convert.ToString(prevEmployeeDetails.PrefixName))));
                    curr.Add(String.Concat("Prefix: ", (Convert.ToString(AddEmployee.PrefixName))));
                }
                if (AddEmployee.FirstName != prevEmployeeDetails.FirstName)
                {
                    prev.Add(String.Concat("First Name: ", (Convert.ToString(prevEmployeeDetails.FirstName))));
                    curr.Add(String.Concat("First Name: ", (Convert.ToString(AddEmployee.FirstName))));
                }
                if (AddEmployee.LastName != prevEmployeeDetails.LastName)
                {
                    prev.Add(String.Concat("Last Name: ", (Convert.ToString(prevEmployeeDetails.LastName))));
                    curr.Add(String.Concat("Last Name: ", (Convert.ToString(AddEmployee.LastName))));
                }
                if (AddEmployee.EMPCode.Trim() != prevEmployeeDetails.EMPCode.Trim())
                {
                    prev.Add(String.Concat("EMP Code: ", (Convert.ToString(prevEmployeeDetails.EMPCode))));
                    curr.Add(String.Concat("EMP Code: ", (Convert.ToString(AddEmployee.EMPCode))));
                }
                if (AddEmployee.EmailId != prevEmployeeDetails.EmailId)
                {
                    prev.Add(String.Concat("Email Id: ", (Convert.ToString(prevEmployeeDetails.EmailId))));
                    curr.Add(String.Concat("Email Id: ", (Convert.ToString(AddEmployee.EmailId))));
                }
                if (AddEmployee.GroupId != prevEmployeeDetails.GroupId)
                {
                    prev.Add(String.Concat("Department Name: ", (Convert.ToString(prevEmployeeDetails.Department))));
                    curr.Add(String.Concat("Department Name: ", (Convert.ToString(AddEmployee.Department))));
                }
                if (AddEmployee.Band != prevEmployeeDetails.Band)
                {
                    prev.Add(String.Concat("Band: ", (Convert.ToString(prevEmployeeDetails.BandName))));
                    curr.Add(String.Concat("Band: ", (Convert.ToString(AddEmployee.BandName))));
                }
                if (AddEmployee.JoiningDate != prevEmployeeDetails.JoiningDate)
                {
                    prev.Add(String.Concat("Joining Date: ", (Convert.ToString(prevEmployeeDetails.JoiningDate.ToShortDateString()))));
                    curr.Add(String.Concat("Joining Date: ", (Convert.ToString(AddEmployee.JoiningDate.ToShortDateString()))));
                }
                if (AddEmployee.Designation != prevEmployeeDetails.Designation)
                {
                    prev.Add(String.Concat("Designation: ", (Convert.ToString(prevEmployeeDetails.Designation))));
                    curr.Add(String.Concat("Designation: ", (Convert.ToString(AddEmployee.Designation))));
                }
                if (AddEmployee.ReportingToId != prevEmployeeDetails.ReportingToId)
                {
                    //30717-Subhra-Start Commented the 2 lines and added the modified 2 lines.
                    //prev.Add(String.Concat("Reporting To: ", (Convert.ToString(prevEmployeeDetails.ReportingTo))));
                    //curr.Add(String.Concat("Reporting To: ", (Convert.ToString(AddEmployee.ReportingTo))));
                    prev.Add(String.Concat("Accountable To: ", (Convert.ToString(prevEmployeeDetails.ReportingTo))));
                    curr.Add(String.Concat("Accountable To: ", (Convert.ToString(AddEmployee.ReportingTo))));
                    //30717-Subhra-End

                }
                if (AddEmployee.Type != prevEmployeeDetails.Type)
                {
                    prev.Add(String.Concat("Type: ", (Convert.ToString(prevEmployeeDetails.EmployeeType))));
                    curr.Add(String.Concat("Type: ", (Convert.ToString(AddEmployee.EmployeeType))));
                }
                if (AddEmployee.ReportingToFMId != prevEmployeeDetails.ReportingToFMId)
                {
                    //30717-Subhra-Start Commented the 2 lines and added the modified 2 lines.
                    //prev.Add(String.Concat("Functional Manager: ", (Convert.ToString(prevEmployeeDetails.ReportingToFM))));
                    //curr.Add(String.Concat("Functional Manager: ", (Convert.ToString(AddEmployee.ReportingToFM))));
                    prev.Add(String.Concat("Reporting To: ", (Convert.ToString(prevEmployeeDetails.ReportingToFM))));
                    curr.Add(String.Concat("Reporting To: ", (Convert.ToString(AddEmployee.ReportingToFM))));
                    //30717-Subhra-end

                }
                if (AddEmployee.EmpLocation != prevEmployeeDetails.EmpLocation)
                {
                    prev.Add(String.Concat("Location: ", (Convert.ToString(prevEmployeeDetails.EmpLocation))));
                    curr.Add(String.Concat("Location: ", (Convert.ToString(AddEmployee.EmpLocation))));
                }

                //link to Update the employee Details.
                string strEmployeeDetails = Utility.GetUrl() + CommonConstants.PAGE_EMPLOYEEDETAILS;

                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Employee),
                                            Convert.ToInt16(EnumsConstants.EmailFunctionality.EditEmployee));

                obj.Subject = string.Format(obj.Subject, AddEmployee.EMPCode,
                                                         fullName);

                obj.Body = string.Format(obj.Body, GetHTMLForTableData(prev, curr));

                if (curr.Count > 0 && prev.Count > 0)
                {
                    obj.SendEmail(obj);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "SendMailForAddEmployeeToUpdateDetails", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        public string GetLinkForEmailForSkillsUpdate(int EMPId)
        {
            return Utility.GetUrl() + "EmployeeSkillsDetails.aspx" + "?" + URLHelper.SecureParameters(CommonConstants.EMP_ID, EMPId.ToString()) + "&" + URLHelper.CreateSignature(EMPId.ToString());
        }
        //Aarohi : Issue 30053(CR) : 22/12/2011 : Start
        //Added the following method to set the link for viewing the updated & deleted employee Resume

        //method to set the link for viewing the updated employee Resume
        public string GetLinkForEmailForResumeUpdate(int EMPId)
        {
            return Utility.GetUrl() + "AddEmployeeResume.aspx" + "?" + URLHelper.SecureParameters(CommonConstants.EMP_ID, EMPId.ToString()) + "&" + URLHelper.CreateSignature(EMPId.ToString());
        }

        //method to set the link for viewing the deleted employee Resume
        public string GetLinkForEmailForResumeDelete(int EMPId)
        {
            return Utility.GetUrl() + "AddEmployeeResume.aspx" + "?" + URLHelper.SecureParameters(CommonConstants.EMP_ID, EMPId.ToString()) + "&" + URLHelper.CreateSignature(EMPId.ToString());
        }
        //Aarohi : Issue 30053(CR) : 22/12/2011 : End

        public string GetLinkForEmailForUpdateDetailsForNewEmployee(int EMPId)
        {
            return Utility.GetUrl() + "EmployeeDetails.aspx" + "?" + URLHelper.SecureParameters(CommonConstants.EMP_ID, EMPId.ToString()) + "&" + URLHelper.CreateSignature(EMPId.ToString());
        }

        public void SendMailToEmployeeForClosedProject(BusinessEntities.Projects project)
        {
            Utility objEmailData = new Utility();
            try
            {
                AuthorizationManager authoriseduser = new AuthorizationManager();
                string LoggedInUserMailId = authoriseduser.getLoggedInUser();
                //string strFrom = LoggedInUserMailId.Replace(RAVE_DOMAIN1, Empty);
                //string username = authoriseduser.GetDomainUserName(strFrom.Replace(RAVE_DOMAIN2, Empty));

                //Google change to northgate //GoogleMail
                //string strFrom = "";
                //string username = "";
                //if (LoggedInUserMailId.ToLower().Contains(RAVE_DOMAIN1))
                //{
                //    strFrom = LoggedInUserMailId.Replace(RAVE_DOMAIN1, Empty);
                //    username = authoriseduser.GetDomainUserName(strFrom.Replace(RAVE_DOMAIN2, Empty));
                //}
                //else
                //{
                //    strFrom = LoggedInUserMailId.Replace(RAVE_NORTHGATE_DOMAIN1, Empty);
                //    username = authoriseduser.GetDomainUserName(strFrom.Replace(AuthorizationManagerConstants.NORTHGATEDOMAIN, Empty));
                //}
                string username = "";
                username = authoriseduser.GetDomainUserName(LoggedInUserMailId);

                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Employee),
                                            Convert.ToInt16(EnumsConstants.EmailFunctionality.EmployeeProjectClosed));

                string strProjectSummaryLink = Utility.GetUrl() + CommonConstants.PROJECTSUMMARY_PAGE;

                obj.Subject = string.Format(obj.Subject, project.ProjectCode,
                                                         project.ProjectName);

                obj.Body = string.Format(obj.Body, project.ProjectName,
                                                   project.StartDate.ToShortDateString(),
                                                   project.EndDate.ToShortDateString(),
                                                   project.ClientName,
                                                   username);

                obj.SendEmail(obj);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "SendMailToEmployeeForClosedProject", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        public void SendMailToEmployeeReleasedFromDepartment(BusinessEntities.Employee employee, Boolean IsRoleHR)
        {
            Utility objEmailData = new Utility();
            try
            {
                AuthorizationManager authoriseduser = new AuthorizationManager();
                string LoggedInUserMailId = authoriseduser.getLoggedInUser();
                //GoogleMail
                //string strFrom = LoggedInUserMailId.Replace(RAVE_DOMAIN1, Empty);
                //string username = authoriseduser.GetDomainUserName(strFrom.Replace(RAVE_DOMAIN2, Empty));

                //Google change to northgate
                //string strFrom = "";
                //string username = "";
                //if (LoggedInUserMailId.ToLower().Contains(RAVE_DOMAIN1))
                //{
                //    strFrom = LoggedInUserMailId.Replace(RAVE_DOMAIN1, Empty);
                //    username = authoriseduser.GetDomainUserName(strFrom.Replace(RAVE_DOMAIN2, Empty));
                //}
                //else
                //{
                //    strFrom = LoggedInUserMailId.Replace(RAVE_NORTHGATE_DOMAIN1, Empty);
                //    username = authoriseduser.GetDomainUserName(strFrom.Replace(AuthorizationManagerConstants.NORTHGATEDOMAIN, Empty));
                //}
                string username = "";
                username = authoriseduser.GetDomainUserName(LoggedInUserMailId);


                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Employee),
                                            Convert.ToInt16(EnumsConstants.EmailFunctionality.EmployeeReleaseFromDepartment));

                string strReporingToEmailIds = GetDepartmentHeadEmailId(employee);
                if (!IsRoleHR && obj.From.Length != 0)
                {
                    obj.From = obj.From.Remove(0);
                }
                obj.CC.Add(strReporingToEmailIds);

                // Venkatesh : 48132  : 06/Jan/2014 : Starts                    
                // Desc : Add Accoutable Person in MailTO 
                string AccountableEmailTo = "";
                if (employee.ReportingToId != null)
                {
                    AccountableEmailTo = new Rave.HR.BusinessLayer.Contracts.Contract().GetEmailID(Convert.ToInt32(employee.ReportingToId));
                    obj.CC.Add(AccountableEmailTo);
                }
                // Venkatesh : 48132  : 06/Jan/2014 : End


                //Added by Siddharth : NIS-RMS : 23/02/2015 : Starts
                Rave.HR.BusinessLayer.Employee.Employee mrfClientName = new Rave.HR.BusinessLayer.Employee.Employee();
                raveHRCollection = mrfClientName.GetClientNameFromProjectID(employee.ProjectId);

                string ClientName = string.Empty;
                if (raveHRCollection != null)
                {
                    if (raveHRCollection.Count > 0)
                    {
                        ClientName = ((BusinessEntities.KeyValue<string>)(raveHRCollection.Item(0))).KeyName;
                        // Siddharth : NIS-RMS : 19/02/2015 : Starts                        			  
                        // Desc : Add Naviya and Sai Email id where Client Name is NIS or  
                        if (ClientName.ToUpper().Contains("NPS") || ClientName.ToUpper().Contains("NORTHGATE"))
                        {
                            obj.CC.Add(CommonConstants.SAIGIDDUNISandNAVYANISEmailId);
                            //obj.CC.Add(CommonConstants.SAIGIDDUNISEmailId);
                            //obj.CC.Add(CommonConstants.NAVYANISEmailId);
                        }
                    }
                }
                // Siddharth : NIS-RMS : 23/02/2015 : Ends

                obj.To.Add(employee.EmailId);

                obj.Subject = string.Format(obj.Subject, employee.FullName,
                                                         employee.Department);

                obj.Body = string.Format(obj.Body, employee.FullName,
                                                   employee.ProjectReleaseDate.ToShortDateString(),
                                                   employee.Department);
                                                   //Below part is commented by Siddharth
                                                   //after consulting Venkatesh on 30th nov 2015
                                                   //Sawita's name is hardcoded in EmailConfig.xml
                                                   //"Sawita Kamat");
                                                   //username);

                obj.SendEmail(obj);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "SendMailToEmployeeReleasedFromDepartment", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        public void SendMailToEmployeeReleasedFromProject(BusinessEntities.Employee employee, Boolean IsRoleHR)
        {
            Utility objEmailData = new Utility();
            try
            {
                //GoogleMail
                //string strFrom = LoggedInUserMailId.Replace(RAVE_DOMAIN1, Empty);
                //string username = authoriseduser.GetDomainUserName(strFrom.Replace(RAVE_DOMAIN2, Empty));

                //Google change to northgate
                //string strFrom = "";
                //string username = "";
                //if (LoggedInUserMailId.ToLower().Contains(RAVE_DOMAIN1))
                //{
                //    strFrom = LoggedInUserMailId.Replace(RAVE_DOMAIN1, Empty);
                //    username = authoriseduser.GetDomainUserName(strFrom.Replace(RAVE_DOMAIN2, Empty));
                //}
                //else
                //{
                //    strFrom = LoggedInUserMailId.Replace(RAVE_NORTHGATE_DOMAIN1, Empty);
                //    username = authoriseduser.GetDomainUserName(strFrom.Replace(AuthorizationManagerConstants.NORTHGATEDOMAIN, Empty));
                //}

                string username = string.Empty;

                //Ishwar 24022016 Desc: Sawita's name is hardcoded using constants :Start

                //AuthorizationManager authoriseduser = new AuthorizationManager();
                //string LoggedInUserMailId = authoriseduser.getLoggedInUser();
                //username = authoriseduser.GetDomainUserName(LoggedInUserMailId);
                username = CommonConstants.RMOGroupHead;
                //Ishwar 24022016 Desc: Sawita's name is hardcoded using constants :End


                string departmentHeadEmail = string.Empty;
                BusinessLayer.MRF.MRFDetail objMRFBL = new Rave.HR.BusinessLayer.MRF.MRFDetail();

                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Employee),
                                            Convert.ToInt16(EnumsConstants.EmailFunctionality.EmployeeReleaseFromProject));

                string projectManagerId = string.Empty;

                string strReporingToEmailIds = GetProjectManagersEmailId(employee, ref projectManagerId);

                //Initialise the Collection class object for department heads 
                BusinessEntities.RaveHRCollection raveHRCollectionDepartment = new RaveHRCollection();
                BusinessEntities.Recruitment objRecruitment = new BusinessEntities.Recruitment();

                objRecruitment.DepartmentId = employee.GroupId;

                //Gets department heads collection
                raveHRCollectionDepartment = DataAccessLayer.Recruitment.Recruitment.GetDepartmentHeadByDepartmentId(objRecruitment);

                //add reporting person emailId in CC.
                //if (!string.IsNullOrEmpty(employee.ReportingToId))
                //{
                //    obj.CC.Add(objMRFBL.GetEmailIdByEmpId(int.Parse(employee.ReportingToId)));
                //}

                //Insert new condition to check is there any mail id in 
                //obj.From.If Yes then only we will removed the mail id of 
                //Index 0.

                //add the respective functional managers emailId in CC.
                if (raveHRCollectionDepartment.Count > 0)
                {
                    //Get Department Head Name
                    foreach (BusinessEntities.Recruitment objrer in raveHRCollectionDepartment)
                    {
                        departmentHeadEmail += objrer.EmailId;
                        objRecruitment.EmailId = departmentHeadEmail;

                    }
                    if (objRecruitment.EmailId.EndsWith(","))
                    {
                        objRecruitment.EmailId = objRecruitment.EmailId.Substring(0, objRecruitment.EmailId.Length - 1);
                    }
                }

                // Venkatesh : 48132  : 06/Jan/2014 : Starts                    
                // Desc : Add Accoutable Person in MailTO 
                string AccountableEmailTo = "";
                if (employee.ReportingToId != null)
                {
                    AccountableEmailTo = new Rave.HR.BusinessLayer.Contracts.Contract().GetEmailID(Convert.ToInt32(employee.ReportingToId));
                    if (AccountableEmailTo != null || AccountableEmailTo != "")
                        obj.CC.Add(AccountableEmailTo);
                }
                // Venkatesh : 48132  : 06/Jan/2014 : End



                obj.CC.Add(objRecruitment.EmailId);


                //Added by Siddharth : NIS-RMS : 23/02/2015 : Starts
                Rave.HR.BusinessLayer.Employee.Employee mrfClientName = new Rave.HR.BusinessLayer.Employee.Employee();
                raveHRCollection = mrfClientName.GetClientNameFromProjectID(employee.ProjectId);

                string ClientName = string.Empty;
                if (raveHRCollection != null)
                {
                    if (raveHRCollection.Count > 0)
                    {
                        ClientName = ((BusinessEntities.KeyValue<string>)(raveHRCollection.Item(0))).KeyName;
                        // Siddharth : NIS-RMS : 19/02/2015 : Starts                        			  
                        // Desc : Add Naviya and Sai Email id where Client Name is NIS or  
                        if (ClientName.ToUpper().Contains("NPS") || ClientName.ToUpper().Contains("NORTHGATE"))
                        {
                            obj.CC.Add(CommonConstants.SAIGIDDUNISandNAVYANISEmailId);
                            //obj.CC.Add(CommonConstants.SAIGIDDUNISEmailId);
                            //obj.CC.Add(CommonConstants.NAVYANISEmailId);
                        }
                    }
                }
                // Siddharth : NIS-RMS : 19/02/2015 : Ends

                if (obj.From.Length > 0)
                {
                    if (!IsRoleHR)
                        obj.From = obj.From.Remove(0);
                }
                obj.To.Add(employee.EmailId);

                obj.CC.Add(strReporingToEmailIds);

                obj.Subject = string.Format(obj.Subject, employee.FullName,
                                                         employee.ProjectName);

                string appendCVInfo = "Request you to kindly update your resume in RMS with the details of the last project that you have worked on along with the details of your latest skills." + "<br/><br/>" +
                    "RMS Link: " + Utility.GetUrl() + CommonConstants.PAGE_HOME + "<br/><br/>" +
                    "Steps to update Employee Skills:" + "<br/><br/>" +
                    "1. Click on employee tab " + "<br/>" +
                    "2. Click on employee profile " + "<br/>" +
                    "3. Click on employee skills " + "<br/>" +
                    "4. Click on edit " + "<br/>" +
                    "<b>5. Update Skills </b>" + "<br/>" +
                    "<b>6. Update Domain Details at the bottom of the page </b>" + "<br/><br/>" +
                    "Steps to update resume: " + "<br/><br/>" +
                    "1. Click on employee tab" + "<br/>" +
                    "2. Click on employee profile " + "<br/>" +
                    "3. Click on resume template & download the latest template " + "<br/>" +
                    "<b>4. Update resume </b>" + "<br/>" +
                    "<b>5. Upload resume </b>" + "<br/>";

                //Change here
                //Add 1 parameter
                if (employee.StatusId == 142)
                {
                    obj.Body = string.Format(obj.Body, employee.FullName,
                                                   employee.ProjectReleaseDate.ToShortDateString(),
                                                   employee.ProjectName,
                                                   employee.ClientName,
                                                   username,
                        //Siddharth 21st August 2015 Start
                        //ConfigurationManager.AppSettings["RMSUrl"].ToString());
                                                   appendCVInfo);
                    //Siddharth 21st August 2015 Start
                }
                else
                {
                    obj.Body = string.Format(obj.Body, employee.FullName,
                                                   employee.ProjectReleaseDate.ToShortDateString(),
                                                   employee.ProjectName,
                                                   employee.ClientName,
                                                   username, "");
                }

                obj.SendEmail(obj);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "SendMailToEmployeeReleasedFromProject", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        public void SendMailEmployeeProjectAllocationUpdation(BusinessEntities.Employee employee)
        {
            Utility objEmailData = new Utility();
            try
            {
                AuthorizationManager authoriseduser = new AuthorizationManager();
                string LoggedInUserMailId = authoriseduser.getLoggedInUser();
                BusinessEntities.Recruitment objRecruitment = new BusinessEntities.Recruitment();
                objRecruitment.DepartmentId = employee.GroupId;

                //GoogleMail
                //string strFrom = LoggedInUserMailId.Replace(RAVE_DOMAIN1, Empty);
                //string username = authoriseduser.GetDomainUserName(strFrom.Replace(RAVE_DOMAIN2, Empty));
                //Google change to northgate
                //string strFrom = "";
                //string username = "";
                //if (LoggedInUserMailId.ToLower().Contains(RAVE_DOMAIN1))
                //{
                //    strFrom = LoggedInUserMailId.Replace(RAVE_DOMAIN1, Empty);
                //    username = authoriseduser.GetDomainUserName(strFrom.Replace(RAVE_DOMAIN2, Empty));
                //}
                //else
                //{
                //    strFrom = LoggedInUserMailId.Replace(RAVE_NORTHGATE_DOMAIN1, Empty);
                //    username = authoriseduser.GetDomainUserName(strFrom.Replace(AuthorizationManagerConstants.NORTHGATEDOMAIN, Empty));
                //}

                string username = "";
                username = authoriseduser.GetDomainUserName(LoggedInUserMailId);



                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Employee),
                                            Convert.ToInt16(EnumsConstants.EmailFunctionality.EmployeeProjectAllocationUpdation));


                string projectManagerId = string.Empty;

                string strReporingToEmailIds = GetProjectManagersEmailId(employee, ref projectManagerId);

                obj.CC.Add(strReporingToEmailIds);


                // Siddharth : NIS-RMS : 19/02/2015 : Starts                        			  
                // Desc : Add Naviya and Sai Email id where Client Name is NIS or  Northgate
                if (employee.EmpReleasedStatus == 1)
                {
                    if (employee.ClientName.ToUpper().Contains("NPS") || employee.ClientName.ToUpper().Contains("NORTHGATE"))
                    {
                        obj.CC.Add(CommonConstants.SAIGIDDUNISandNAVYANISEmailId);
                        //obj.CC.Add(CommonConstants.SAIGIDDUNISEmailId);
                        //obj.CC.Add(CommonConstants.NAVYANISEmailId);
                    }
                }
                // Siddharth : NIS-RMS : 19/02/2015 : Ends



                if (employee.ProjectId != 0)
                {
                    obj.Subject = string.Format(obj.Subject, employee.FullName,
                                                             employee.ProjectName);

                    obj.Body = string.Format(obj.Body, Project,
                                                       employee.ProjectName,
                                                       employee.FullName,
                                                       employee.Utilization,
                                                       employee.Billing,
                                                       employee.ProjectId,
                                                       employee.ProjectCode,
                                                       employee.EmpProjectAllocationId,
                                                       employee.Role,
                                                       employee.ProjectReleaseDate.ToShortDateString(),
                        //Added to solved issue no 20769
                                                       employee.UtilizationAndBilling.ToShortDateString(),
                        //End
                        //Added to solved issue no 22011
                                                       employee.BillingChangeDate.ToShortDateString(),
                        //End
                                                       employee.ReasonForExtension,
                                                       username);
                }
                else
                {
                    if (employee.Department == PreSalesUK || employee.Department == PreSalesUSA || employee.Department == PreSalesIndia)
                    {
                        obj.Subject = string.Format(obj.Subject, employee.FullName,
                                                                 employee.Department);

                        obj.Body = string.Format(obj.Body.Replace("<br/>ProjectID:", string.Empty),
                                                       Project,
                                                       employee.Department,
                                                       employee.FullName,
                                                       employee.Utilization,
                                                       employee.Billing,
                                                       string.Empty,
                                                       string.Empty,
                                                       employee.EmpProjectAllocationId,
                                                       employee.Role,
                                                       employee.ProjectReleaseDate.ToShortDateString(),
                                                       employee.UtilizationAndBilling.ToShortDateString(),
                                                       employee.BillingChangeDate.ToShortDateString(),
                                                       employee.ReasonForExtension,
                                                       username);

                        obj.Body = obj.Body.Replace("<br/>Project Code:", string.Empty);
                    }
                    else
                    {
                        obj.Subject = string.Format(obj.Subject.Replace(Project, Department), employee.FullName,
                                                                 employee.Department);

                        obj.Body = obj.Body.Replace(ProjectName, DepartmentName);
                    }
                    obj.Body = string.Format(obj.Body.Replace("<br/>ProjectID:", string.Empty),
                                                       Department,
                                                       employee.Department,
                                                       employee.FullName,
                                                       employee.Utilization,
                                                       employee.Billing,
                                                       string.Empty,
                                                       string.Empty,
                                                       employee.EmpProjectAllocationId,
                                                       employee.Role,
                                                       employee.ProjectReleaseDate.ToShortDateString(),
                                                       employee.UtilizationAndBilling.ToShortDateString(),
                                                       employee.BillingChangeDate.ToShortDateString(),
                                                       employee.ReasonForExtension,
                                                       username);

                    obj.Body = obj.Body.Replace("<br/>Project Code:", string.Empty);

                }
                if (obj.Body.Contains("01/01/0001"))
                {
                    obj.Body = obj.Body.Replace("01/01/0001", "");
                }
                obj.SendEmail(obj);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "SendMailEmployeeProjectAllocationUpdation", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        public void SendMailEmployeeSeperationFromCompany(BusinessEntities.Employee employee, bool B_ClientName)
        {
            Utility objEmailData = new Utility();
            try
            {
                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Employee),
                                            Convert.ToInt16(EnumsConstants.EmailFunctionality.EmployeeSeperationFromCompany));

                BusinessEntities.Recruitment objRecruitment = new BusinessEntities.Recruitment();
                objRecruitment.DepartmentId = employee.GroupId;


                obj.Subject = string.Format(obj.Subject, employee.EMPCode.Trim(),
                                                         employee.FullName);

                obj.Body = string.Format(obj.Body, employee.FullName,
                                                   employee.Designation,
                                                   employee.Department,
                                                   employee.ResignationDate.ToShortDateString());



                // Siddharth : NIS-RMS : 19/02/2015 : Starts                        			  
                // Desc : Add Naviya and Sai Email id where Client Name is NIS or  Northgate
                if (B_ClientName == true)
                {
                    obj.CC.Add(CommonConstants.SAIGIDDUNISandNAVYANISEmailId);
                    //obj.CC.Add(CommonConstants.SAIGIDDUNISEmailId);
                    //obj.CC.Add(CommonConstants.NAVYANISEmailId);
                }
                // Siddharth : NIS-RMS : 19/02/2015 : Ends



                obj.SendEmail(obj);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "SendMailEmployeeSeperationFromCompany", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        public void SendMailEmployeeResignFromCompany(BusinessEntities.Employee employee, bool B_ClientName)
        {
            Utility objEmailData = new Utility();
            try
            {
                AuthorizationManager authoriseduser = new AuthorizationManager();
                string LoggedInUserMailId = authoriseduser.getLoggedInUser();
                //GoogleMail
                //string strFrom = LoggedInUserMailId.Replace(RAVE_DOMAIN1, Empty);
                //string username = authoriseduser.GetDomainUserName(strFrom.Replace(RAVE_DOMAIN2, Empty));

                //Google change to northgate
                //string strFrom = "";
                //string username = "";
                //if (LoggedInUserMailId.ToLower().Contains(RAVE_DOMAIN1))
                //{
                //    strFrom = LoggedInUserMailId.Replace(RAVE_DOMAIN1, Empty);
                //    username = authoriseduser.GetDomainUserName(strFrom.Replace(RAVE_DOMAIN2, Empty));
                //}
                //else
                //{
                //    strFrom = LoggedInUserMailId.Replace(RAVE_NORTHGATE_DOMAIN1, Empty);
                //    username = authoriseduser.GetDomainUserName(strFrom.Replace(AuthorizationManagerConstants.NORTHGATEDOMAIN, Empty));
                //}
                string username = "";
                username = authoriseduser.GetDomainUserName(LoggedInUserMailId);

                //BusinessEntities.Recruitment objRecruitment = new BusinessEntities.Recruitment();
                //objRecruitment.DepartmentId = employee.GroupId;

                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Employee),
                                            Convert.ToInt16(EnumsConstants.EmailFunctionality.EmployeeResignFromCompany));


                obj.Subject = string.Format(obj.Subject, employee.EMPCode.Trim(),
                                                         employee.FullName);

                if (employee.Gender.Trim() == "M")
                {
                    obj.Body = string.Format(obj.Body, employee.FullName,
                                                       employee.Designation,
                                                       employee.Department,
                                                       forMale,
                                                       employee.LastWorkingDay.ToShortDateString());
                }
                else
                {
                    obj.Body = string.Format(obj.Body, employee.FullName,
                                                       employee.Designation,
                                                       employee.Department,
                                                       forFemale,
                                                       employee.LastWorkingDay.ToShortDateString());
                }


                // Siddharth : NIS-RMS : 19/02/2015 : Starts                        			  
                // Desc : Add Naviya and Sai Email id where Client Name is NIS or  Northgate
                if (B_ClientName == true)
                {
                    obj.CC.Add(CommonConstants.SAIGIDDUNISandNAVYANISEmailId);
                    //obj.CC.Add(CommonConstants.SAIGIDDUNISEmailId);
                    //obj.CC.Add(CommonConstants.NAVYANISEmailId);
                }
                // Siddharth : NIS-RMS : 19/02/2015 : Ends
                // Siddharth : NIS-RMS : 19/02/2015 : Ends



                obj.SendEmail(obj);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "SendMailEmployeeResignFromCompany", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        public void SendMailEmployeeRollBackResignFromCompany(BusinessEntities.Employee employee)
        {
            Utility objEmailData = new Utility();
            try
            {
                AuthorizationManager authoriseduser = new AuthorizationManager();
                string LoggedInUserMailId = authoriseduser.getLoggedInUser();
                //GoogleMail
                //string strFrom = LoggedInUserMailId.Replace(RAVE_DOMAIN1, Empty);
                //string username = authoriseduser.GetDomainUserName(strFrom.Replace(RAVE_DOMAIN2, Empty));
                //Google change to northgate
                //string strFrom = "";
                //string username = "";
                //if (LoggedInUserMailId.ToLower().Contains(RAVE_DOMAIN1))
                //{
                //    strFrom = LoggedInUserMailId.Replace(RAVE_DOMAIN1, Empty);
                //    username = authoriseduser.GetDomainUserName(strFrom.Replace(RAVE_DOMAIN2, Empty));
                //}
                //else
                //{
                //    strFrom = LoggedInUserMailId.Replace(RAVE_NORTHGATE_DOMAIN1, Empty);
                //    username = authoriseduser.GetDomainUserName(strFrom.Replace(AuthorizationManagerConstants.NORTHGATEDOMAIN, Empty));
                //}

                string username = "";
                username = authoriseduser.GetDomainUserName(LoggedInUserMailId);

                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Employee),
                                            Convert.ToInt16(EnumsConstants.EmailFunctionality.EmployeeRollBackResignFromCompany));


                obj.Subject = string.Format(obj.Subject, employee.EMPCode.Trim(),
                                                         employee.FullName);

                if (employee.Gender.Trim() == "M")
                {
                    obj.Body = string.Format(obj.Body, employee.FullName,
                                                       employee.Designation,
                                                       employee.Department,
                                                       forMale.ToLower(),
                                                       employee.LastWorkingDay.ToShortDateString());
                }
                else
                {
                    obj.Body = string.Format(obj.Body, employee.FullName,
                                                       employee.Designation,
                                                       employee.Department,
                                                       forFemale.ToLower(),
                                                       employee.LastWorkingDay.ToShortDateString());
                }

                obj.SendEmail(obj);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer,
                    CLASS_NAME, "SendMailEmployeeRooBackResignFromCompany",
                    EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        public void SendMailApprovalOfSkillsRating(BusinessEntities.Employee employee)
        {
            Utility objEmailData = new Utility();
            try
            {
                string projectManagerId = String.Empty;
                AuthorizationManager authoriseduser = new AuthorizationManager();
                string LoggedInUserMailId = authoriseduser.getLoggedInUserEmailId();
                //GoogleMail
                //string strFrom = LoggedInUserMailId.Replace(RAVE_DOMAIN1, Empty);
                //string username = authoriseduser.GetDomainUserName(strFrom.Replace(RAVE_DOMAIN2, Empty));
                //Google change to northgate
                //string strFrom = "";
                //string username = "";
                //if (LoggedInUserMailId.ToLower().Contains(RAVE_DOMAIN1))
                //{
                //    strFrom = LoggedInUserMailId.Replace(RAVE_DOMAIN1, Empty);
                //    username = authoriseduser.GetDomainUserName(strFrom.Replace(RAVE_DOMAIN2, Empty));
                //}
                //else
                //{
                //    strFrom = LoggedInUserMailId.Replace(RAVE_NORTHGATE_DOMAIN1, Empty);
                //    username = authoriseduser.GetDomainUserName(strFrom.Replace(AuthorizationManagerConstants.NORTHGATEDOMAIN, Empty));
                //}

                string username = "";
                username = authoriseduser.GetDomainUserName(authoriseduser.getLoggedInUser());

                string strReporingToEmailIds = GetProjectManagersEmailId(employee, ref projectManagerId);

                string fullName = String.Concat(employee.FirstName, " ", employee.LastName);

                Utility ut = new Utility();
                //link to approve/reject resource plan page.
                string strPendingAllocation = GetLinkForEmailForSkillsUpdate(employee.EMPId);


                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Employee),
                                            Convert.ToInt16(EnumsConstants.EmailFunctionality.ApprovalOfSkillsRating));

                obj.From = LoggedInUserMailId;

                obj.To.Add(strReporingToEmailIds);

                obj.Subject = string.Format(obj.Subject, employee.EMPCode.Trim(),
                                                         fullName);
                if (employee.Gender.Trim() == "M")
                {

                    obj.Body = string.Format(obj.Body, fullName,
                                                       forMale,
                                                       forMale.ToLower(),
                                                       strPendingAllocation,
                                                       strPendingAllocation,
                                                       username);
                }
                else
                {
                    obj.Body = string.Format(obj.Body, fullName,
                                                       forFemale,
                                                       forFemale.ToLower(),
                                                       strPendingAllocation,
                                                       strPendingAllocation,
                                                       username);

                }

                obj.SendEmail(obj);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "SendMailApprovalOfSkillsRating", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Get Table data in HTML format.
        /// </summary>
        /// <param name="listProjectDetails"></param>
        /// <returns></returns>
        private string GetHTMLForTableData(List<string> listPrevEmployeeDetails, List<string> listCurrEmployeeDetails)
        {
            string bodyTable = string.Empty;
            if (listPrevEmployeeDetails.Count > 0)
            {
                string[] header = new string[2];
                header[0] = "Previous Details";
                header[1] = "Current Details";

                string[,] arrayData = new string[(listPrevEmployeeDetails.Count), 2];

                int rowCounter = 0;
                foreach (string employee in listPrevEmployeeDetails)
                {
                    arrayData[rowCounter, 0] = listPrevEmployeeDetails[rowCounter];
                    arrayData[rowCounter, 1] = listCurrEmployeeDetails[rowCounter];
                    rowCounter++;
                }

                IEmailTableData objEmailTableData = new EmailTableData();
                objEmailTableData.Header = header;
                objEmailTableData.RowDetail = arrayData;
                objEmailTableData.RowCount = listPrevEmployeeDetails.Count;

                bodyTable += objEmailTableData.GetTableData(objEmailTableData);
            }
            return bodyTable;
        }

        public RaveHRCollection IsAllocatedToProject(BusinessEntities.Employee employee)
        {
            Rave.HR.DataAccessLayer.Employees.Employee employeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();

            return employeeDAL.IsAllocatedToProject(employee);
        }

        public RaveHRCollection GetEmployeesAllocation(BusinessEntities.Employee employee)
        {
            Rave.HR.DataAccessLayer.Employees.Employee employeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();

            return employeeDAL.GetEmployeesAllocation(employee);
        }

        //Issue Id : 28572 Mahendra START
        public RaveHRCollection IsRepotingManager(BusinessEntities.Employee employee)
        {
            Rave.HR.DataAccessLayer.Employees.Employee employeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();

            return employeeDAL.IsRepotingManager(employee);
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
            BusinessEntities.RaveHRCollection employeesList = null;
            try
            {
                employeesList = new BusinessEntities.RaveHRCollection();
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                employeesList = objUpdateEmployeeDAL.GetEmployeesProjectDetails(objParameter, employee, ref pageCount);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetEmployeesList", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return employeesList;
        }




        public void RollBackEmployeeResignationDetailsBL(BusinessEntities.Employee objRollBackEmployee)
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objRollBackUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                objRollBackUpdateEmployeeDAL.RollBackEmployeeResignationDetailsDL(objRollBackEmployee);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer,
                    CLASS_NAME, "UpdateEmployeeResignationDetails",
                    EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

        }
        // Aarohi : Issue 30053(CR) : 22/12/2011 : Start
        // New Methods to send the mail for Uploading & Deleting the Employee Resume 

        /// <summary>
        /// Method to Send Mail for Uploading the Employee Resume
        /// </summary>
        public void SendUploadResumeMails(BusinessEntities.Employee employee)
        {
            Utility objEmailData = new Utility();
            try
            {
                string projectManagerId = String.Empty;
                AuthorizationManager authoriseduser = new AuthorizationManager();
                string LoggedInUserMailId = authoriseduser.getLoggedInUserEmailId();
                //GoogleMail
                //string strFrom = LoggedInUserMailId.Replace(RAVE_DOMAIN1, string.Empty);
                //string username = authoriseduser.GetDomainUserName(strFrom.Replace(RAVE_DOMAIN2, string.Empty));
                //Google change to northgate
                //string strFrom = "";
                //string username = "";
                //if (LoggedInUserMailId.ToLower().Contains(RAVE_DOMAIN1))
                //{
                //    strFrom = LoggedInUserMailId.Replace(RAVE_DOMAIN1, Empty);
                //    username = authoriseduser.GetDomainUserName(strFrom.Replace(RAVE_DOMAIN2, Empty));
                //}
                //else
                //{
                //    strFrom = LoggedInUserMailId.Replace(RAVE_NORTHGATE_DOMAIN1, Empty);
                //    username = authoriseduser.GetDomainUserName(strFrom.Replace(AuthorizationManagerConstants.NORTHGATEDOMAIN, Empty));
                //}

                //string username = "";
                //username = authoriseduser.GetDomainUserName(authoriseduser.getLoggedInUser());

                //string strReporingToEmailIds = GetProjectManagersEmailId(employee, ref projectManagerId);

                string fullName = String.Concat(employee.FirstName, " ", employee.LastName);

                Utility ut = new Utility();

                //link to see the uploaded resume
                string strUploadedResume = GetLinkForEmailForResumeUpdate(employee.EMPId);


                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Employee),
                                            Convert.ToInt16(EnumsConstants.EmailFunctionality.EmployeeUploadedResume));

                obj.From = LoggedInUserMailId;

                //obj.To.Add(strReporingToEmailIds);

                obj.Subject = string.Format(obj.Subject, employee.EMPCode.Trim(),
                                                         fullName);
                if (employee.Gender.Trim() == "M")
                {

                    obj.Body = string.Format(obj.Body, fullName,
                                                       strUploadedResume,
                                                       forMale.ToLower());
                }
                else
                {
                    obj.Body = string.Format(obj.Body, fullName,
                                                       strUploadedResume,
                                                       forFemale.ToLower());

                }

                obj.SendEmail(obj);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "SendUploadResumeMails", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Method to Send Mail for Uploading the Employee Resume
        /// </summary>
        public void SendDeleteResumeMails(BusinessEntities.Employee employee)
        {
            Utility objEmailData = new Utility();
            try
            {
                string projectManagerId = String.Empty;
                AuthorizationManager authoriseduser = new AuthorizationManager();
                string LoggedInUserMailId = authoriseduser.getLoggedInUserEmailId();
                //GoogleMail
                //string strFrom = LoggedInUserMailId.Replace(RAVE_DOMAIN1, string.Empty);
                //string username = authoriseduser.GetDomainUserName(strFrom.Replace(RAVE_DOMAIN2, string.Empty));
                //Google change to northgate
                //string strFrom = "";
                //string username = "";
                //if (LoggedInUserMailId.ToLower().Contains(RAVE_DOMAIN1))
                //{
                //    strFrom = LoggedInUserMailId.Replace(RAVE_DOMAIN1, Empty);
                //    username = authoriseduser.GetDomainUserName(strFrom.Replace(RAVE_DOMAIN2, Empty));
                //}
                //else
                //{
                //    strFrom = LoggedInUserMailId.Replace(RAVE_NORTHGATE_DOMAIN1, Empty);
                //    username = authoriseduser.GetDomainUserName(strFrom.Replace(AuthorizationManagerConstants.NORTHGATEDOMAIN, Empty));
                //}

                //string username = "";
                //username = authoriseduser.GetDomainUserName(LoggedInUserMailId);

                //string strReporingToEmailIds = GetProjectManagersEmailId(employee, ref projectManagerId);

                string fullName = String.Concat(employee.FirstName, " ", employee.LastName);

                Utility ut = new Utility();
                //link to see the uploaded resume
                string strDeletedResume = GetLinkForEmailForResumeDelete(employee.EMPId);


                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Employee),
                                            Convert.ToInt16(EnumsConstants.EmailFunctionality.EmployeeDeletedResume));

                obj.From = LoggedInUserMailId;

                //obj.To.Add(strReporingToEmailIds);

                obj.Subject = string.Format(obj.Subject, employee.EMPCode.Trim(),
                                                         fullName);
                if (employee.Gender.Trim() == "M")
                {

                    obj.Body = string.Format(obj.Body, fullName,
                                                       strDeletedResume,
                                                       forMale.ToLower());
                }
                else
                {
                    obj.Body = string.Format(obj.Body, fullName,
                                                       strDeletedResume,
                                                       forFemale.ToLower());

                }

                obj.SendEmail(obj);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "SendDeleteResumeMails", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }
        // Aarohi : Issue 30053(CR) : 22/12/2011 : End

        //#region 28572
        //Aarohi : Issue 28572(CR) : 05/01/2012 : Start
        // Added following methods


        /// <summary>
        /// Gets the employees details.
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DataSet GetActiveEmployeeList()
        {
            DataSet empDetails = new DataSet();
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objEmpDetailsDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                empDetails = objEmpDetailsDAL.GetActiveEmployeeList();
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetReportingFunctionalManagerIds", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return empDetails;
        }


        /// <summary>
        /// Gets the employees details.
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DataSet GetReportingFunctionalManagerIds(string EMPId, BusinessEntities.ParameterCriteria objParameter)
        {
            DataSet empDetails = new DataSet();
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objEmpDetailsDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                empDetails = objEmpDetailsDAL.GetReportingFunctionalManagerIds(EMPId, objParameter);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetReportingFunctionalManagerIds", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return empDetails;
        }


        //Siddharth 9 April 2015 Start

        /// <summary>
        /// Gets all the Project details for Employees.
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DataSet GetProjectIdsforEmployee(string EMPId, BusinessEntities.ParameterCriteria objParameter)
        {
            DataSet empDetails = new DataSet();
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objEmpDetailsDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                empDetails = objEmpDetailsDAL.GetReportingFunctionalManagerIds(EMPId, objParameter);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetReportingFunctionalManagerIds", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return empDetails;
        }

        //Siddharth 9 April 2015 End

        //Aarohi : Issue 28572(CR) : 05/01/2012 : END

        //Sanju : Issue 49187 : 11/03/2014 : START
        /// <summary>
        /// Gets the manager details.
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DataSet GetManagersList()
        {
            DataSet empDetails = new DataSet();
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objEmpDetailsDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                empDetails = objEmpDetailsDAL.GetManagersList();
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GET_Project_ManagerList", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return empDetails;
        }
        //Sanju : Issue 49187 : 11/03/2014 : END

        //Issue Id : 28572 Mahendra START

        /// <summary>
        /// Updates FM/RM for the existing Employee.
        /// </summary>
        /// <param name="objUpdateEmployee"></param>
        /// <returns></returns>
        public void UpdateEmployeeFMRM(int empId, string rmId, int fmId)
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                objUpdateEmployeeDAL.UpdateEmployeeFMRM(empId, rmId, fmId);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, "Employee.cs", "UpdateEmployeeFMRM", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }


        /// <summary>
        /// Updates FM/RM for the project allocation.
        /// </summary>
        /// <param name="objUpdateEmployee"></param>
        /// <returns></returns>
        public void UpdateEmployeeFMRMForProjectAllocation(int empId, string rmId, int projectId)
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                objUpdateEmployeeDAL.UpdateEmployeeFMRMForProjectAllocation(empId, rmId, projectId);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, "Employee.cs", "UpdateEmployeeFMRM", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        //Siddharth 9 April 2015 Start
        
        /// <summary>
        /// Get Project Wise Emp Cost Code Details For CC Manager.
        /// </summary>
        /// <param name="objUpdateEmployee"></param>
        /// <returns></returns>
        public DataSet GetProjectWiseEmpCostCodeDetailsForCCManager(int empId, int projectId, int mode)
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                return objUpdateEmployeeDAL.GetProjectWiseEmpCostCodeDetailsForCCManager(empId, projectId, mode);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, "Employee.cs", "GetProjectWiseEmpCostCodeDetailsForCCManager", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Gets the manager details.
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DataSet GetEmployeeAndProjectForCCManager(int role)
        {
            DataSet empDetails = new DataSet();
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objEmpDetailsDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                empDetails = objEmpDetailsDAL.GetEmployeeAndProjectForCCManager(role);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GET_Project_ManagerList", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return empDetails;
        }




        //Siddharth 9 April 2015 End

        //Issue Id : 28572 Mahendra END



        /// <summary>
        /// Get the existing Employee List.
        /// </summary>
        /// <param name="objUpdateEmployee"></param>
        /// <returns></returns>        
        public BusinessEntities.RaveHRCollection GetEmployeeListForFMRM(string resourceName, string EMPId)
        {
            BusinessEntities.RaveHRCollection employeesList = null;
            try
            {
                employeesList = new BusinessEntities.RaveHRCollection();
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                employeesList = objUpdateEmployeeDAL.GetEmployeeListForFMRM(resourceName, EMPId);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetEmployeeListForFMRM", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return employeesList;
        }
        //Aarohi : Issue 28572(CR) : 31/01/2012 : End
        //#endregion 28572

        #region 35901
        // 35091-Ambar-Start-27062012
        /// <summary>
        /// Gets the managers email id.
        /// </summary>
        /// <param name="EmpID">The emp ID.</param>
        /// <returns></returns>
        public string GetAllocationReportingToEmailId(BusinessEntities.Employee objEmployee, ref string AllocationReportingToEmailId)
        {
            //Object declaration of SkillsDetails class
            Rave.HR.DataAccessLayer.Employees.Employee objEmployeeDAL;

            try
            {
                //Created new instance of employee class to call GetProjectManagersEmailId() of Data access layer
                objEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();

                //Call to GetSkillsDetails() of Data access layer and return the Skills
                return objEmployeeDAL.GetAllocationReportingToEmailId(objEmployee, ref AllocationReportingToEmailId);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetProjectManagersEmailId", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }
        // 35091-Ambar-End-27062012
        #endregion


        #region Modified By Mohamed Dangra
        // Mohamed : 03/12/2014 : Starts                        			  
        // Desc : NIS Changes

        /// <summary>
        /// Get the Level0 for skill.
        /// </summary>        
        public DataSet GetSkillReport(int Level, string SortExpressionAndDirection)
        {
            DataSet employeesList = null;
            //BusinessEntities.RaveHRCollection employeesList = null;
            try
            {
                employeesList = new DataSet();// BusinessEntities.RaveHRCollection();
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                employeesList = objUpdateEmployeeDAL.GetSkillReport(Level, SortExpressionAndDirection);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetSkillLevel0", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return employeesList;
        }


        // Mohamed : 03/12/2014 : Ends
        #endregion Modified By Mohamed Dangra

        // Venkatesh : Start NIS-RMS : 16-Oct-2014   
        // Desc :  NIS-RMS  
        /// <summary>
        /// NIS Project Name dropDown in Consolidation report
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection FillProjectNameNISDropDowns(string UserRaveDomainId, string Role)
        {
            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                Rave.HR.DataAccessLayer.Employees.Employee employee = new Rave.HR.DataAccessLayer.Employees.Employee();
                raveHRCollection = employee.GetProjectNameNIS(UserRaveDomainId, Role);
                return raveHRCollection;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "FillProjectNameDropDowns", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }



        // Siddharth : Start NIS-RMS : 27th April 2015   
        // Desc :  NIS-RMS  
        /// <summary>
        /// CostCode dropDown in Consolidation report
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection FillProjectCostCodeDropDowns()
        {
            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                Rave.HR.DataAccessLayer.Employees.Employee employee = new Rave.HR.DataAccessLayer.Employees.Employee();
                raveHRCollection = employee.GetAllCostCodes();
                return raveHRCollection;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "FillProjectNameDropDowns", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }






        /// <summary>
        /// Get the existing Employee Summary.
        /// </summary>
        /// <param name="objUpdateEmployee"></param>
        /// <returns></returns>
        public DataSet GetConsolidated(int ProjectId, string SortExpressionAndDirection)
        {
            DataSet employeesList = null;
            //BusinessEntities.RaveHRCollection employeesList = null;
            try
            {
                employeesList = new DataSet();// BusinessEntities.RaveHRCollection();
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                employeesList = objUpdateEmployeeDAL.GetConsolidated(ProjectId, SortExpressionAndDirection);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetEmployeesList", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return employeesList;
        }
        // Venkatesh : End NIS-RMS : 16-Oct-2014   



        
            /// <summary>
        /// Get the existing Employee Summary.
        /// </summary>
        /// <param name="objUpdateEmployee"></param>
        /// <returns></returns>
        public DataSet GetConsolidatedByCostCode(string CostCode, string SortExpressionAndDirection)
        {
            DataSet employeesList = null;
            //BusinessEntities.RaveHRCollection employeesList = null;
            try
            {
                employeesList = new DataSet();// BusinessEntities.RaveHRCollection();
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                employeesList = objUpdateEmployeeDAL.GetConsolidatedByCostCode(CostCode, SortExpressionAndDirection);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetEmployeesList", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return employeesList;
        }
        // Venkatesh : End NIS-RMS : 16-Oct-2014   




        // Ishwar - NISRMS - 30102014 Start
        public BusinessEntities.Employee GetNISEmployeeList(string strUserIdentity)
        {
            BusinessEntities.Employee Employee = new BusinessEntities.Employee();
            Rave.HR.DataAccessLayer.Employees.Employee EmployeeDL = new Rave.HR.DataAccessLayer.Employees.Employee();
            Employee = EmployeeDL.GetNISEmployeeList(strUserIdentity);
            return Employee;
        }
        // Ishwar - NISRMS - 30102014 End

        //Umesh: NIS-changes: Head Count Report Starts
        /// <summary>
        /// Get Head Count Report.
        /// </summary>
        /// <param name="objUpdateEmployee"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetHeadCountReport(BusinessEntities.Projects project, string SortExpressionAndDirection)
        {
            BusinessEntities.RaveHRCollection employeesList = null;
            try
            {
                employeesList = new BusinessEntities.RaveHRCollection();
                Rave.HR.DataAccessLayer.Employees.Employee objEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                //Siddharth 27 March 2015 Start
                employeesList = objEmployeeDAL.GetHeadCountReport(project, SortExpressionAndDirection);
                //Siddharth 27 March 2015 End
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetHeadCountReport", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return employeesList;
        }
        //Umesh: NIS-changes: Head Count Report Starts

        // Ishwar - NISRMS - 27112014 Start
        // For EDC Employee Count
        public int EDCEmployeeCount(string WindowsUserName, string empid)
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee EmployeeDL = new Rave.HR.DataAccessLayer.Employees.Employee();
                return EmployeeDL.EDCEmployeeCount(WindowsUserName, empid);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "AddEmployee", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }
        // Ishwar - NISRMS - 27112014 End




        //Siddharth 23-02-2015 Start
        /// <summary>
        /// Gets the client name from Project ID.
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetClientNameFromProjectID(int ProjectID)
        {
            // Initialise the Data Layer object
            DataAccessLayer.Employees.Employee projectNameDL = new DataAccessLayer.Employees.Employee();

            // Initialise the Collection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();

            try
            {
                // Call the Data Layer Method
                raveHRCollection = projectNameDL.GetClientNameFromProjectID(ProjectID);

                // Return the Collection
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, "Employee.cs", "GetClientNameFromProjectID", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }
        //Siddharth 23-02-2015 End

        //Siddhesh Arekar Domain Details 09032015 Start
        /// <summary>
        /// Manipulations the specified obj employee Domain list.
        /// </summary>
        /// <param name="objSkillsDetailsList">The obj skills details list.</param>
        public void Manipulation(BusinessEntities.RaveHRCollection EmployeeDomainCollection, int empId)
        {
            try
            {
                string empDomain = "";
                //Check for each each entry in SkillsDetails List
                foreach (BusinessEntities.Employee objAddEmployee in EmployeeDomainCollection)
                {
                    if (empDomain == "")
                    {
                        empDomain = objAddEmployee.EmployeeDomain.ToString();
                    }
                    else
                    {
                        empDomain = empDomain + ',' + objAddEmployee.EmployeeDomain.ToString();
                    }
                    //this.UpdateEmployeeDomain(objAddEmployee);
                }

                this.UpdateEmployeeDomain(empDomain, empId);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "Manipulation", EventIDConstants.RAVE_EMP_SKILL_SEARCH_BUSINESS_ACCESS_LAYER);
            }
        }

        /// <summary>
        /// Updates the skills details.
        /// </summary>
        /// <param name="objUpdateSkillsDetails">The obj update skills details.</param>
        public void UpdateEmployeeDomain(string empDomain, int empId)
        {
            //Object declaration of SkillsDetails class
            Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDomainDAL;

            try
            {
                //Created new instance of SkillsDetails class to call UpdateSkillsDetails() of Data access layer
                objUpdateEmployeeDomainDAL = new Rave.HR.DataAccessLayer.Employees.Employee();

                //Call to UpdateSkillsDetails() of Data access layer
                objUpdateEmployeeDomainDAL.UpdateEmployeeDomain(empDomain, empId);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "UpdateEmployeeDomain", EventIDConstants.RAVE_EMP_SKILL_SEARCH_BUSINESS_ACCESS_LAYER);
            }
        }

        public BusinessEntities.RaveHRCollection Employee_GetEmployeeDomain(BusinessEntities.Employee objEmp)
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                return objUpdateEmployeeDAL.Employee_GetEmployeeDomain(objEmp);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "Employee_GetEmployeeCostCodeByEmpIDandNoPrjID", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        public int Employee_Add_Domain_Master(string empDomain, int groupCategoryId, string createdById, out int categoryID)
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                return objUpdateEmployeeDAL.Employee_Add_Domain_Master(empDomain, groupCategoryId, createdById , out categoryID);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "Employee_Add_Domain_Master", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        //Siddhesh Arekar Domain Details 09032015 End

        public string GetEmployeBusinessVertical(int EmpId)
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                return objUpdateEmployeeDAL.GetEmployeeBusinessVertical(EmpId);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetEmployeBusinessVertical", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }

        //Rakesh : Business Vertical Wise Resume Template Download 08/07/2016 Begin 
        /// <summary>
        /// Returns Business Vertical ID
        /// </summary>
        /// <param name="EmpId"></param>
        /// <returns></returns>
        public string GetEmployeBusinessVerticalID(int EmpId)
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                return objUpdateEmployeeDAL.GetEmployeeBusinessVerticalID(EmpId);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetEmployeBusinessVertical", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }
        //Rakesh : Business Vertical Wise Resume Template Download 08/07/2016 End 


        //Rakesh : HOD for Employees 11/07/2016 Begin   

        public BusinessEntities.RaveHRCollection Get_HOD_Employees()
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                return objEmployeeDAL.Get_HOD_Employees();
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "Get_HOD_Employees", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }
        //Rakesh : HOD for Employees 11/07/2016 End


        public int CheckEmployeeIsProjectManager(int EmpId)
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.Employee objUpdateEmployeeDAL = new Rave.HR.DataAccessLayer.Employees.Employee();
                return objUpdateEmployeeDAL.CheckEmployeeIsProjectManager(EmpId);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "CheckEmployeeIsProjectManager", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }
    }
}
