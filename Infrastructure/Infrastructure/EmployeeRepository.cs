using Domain.Entities;
using Infrastructure.Interfaces;
using RMS.Common;
using RMS.Common.BusinessEntities;
using RMS.Common.Constants;
using RMS.Common.DataBase;
using RMS.Common.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Infrastructure
{
    public class EmployeeRepository : IEmployeeRepository
    {
        #region

        public static SqlConnection objConnection = null;
        public static SqlCommand objCommand = null;
        public static SqlDataReader objReader;
        const string clsEmployeeRepository = "EmployeeRepository.cs";

        //private static List<CommonModel> ListCommonModel;
        //private static CommonModel ObjCommonModel;
        //private static DataSet ds; 

        #endregion
        public SelectList GetSkillTypesCategory()
        {
            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.Employee_GetEmployeeSkillTypeCategory, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objReader = objCommand.ExecuteReader();

                SelectListItem selListItem;
                List<SelectListItem> newList = new List<SelectListItem>();
                newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "0" });
                while (objReader.Read())
                {
                    selListItem = new SelectListItem() { Value = objReader[0].ToString().Trim(), Text = objReader[1].ToString().Trim() };

                    newList.Add(selListItem);
                }
                return new SelectList(newList, "Value", "Text");
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, clsEmployeeRepository, "GetSkillTypesCategory", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        /// <summary>
        /// Get employee detail by id
        /// </summary>
        /// <returns>empid</returns>
        public EmployeeModel GetEmployeeDetailByID(int empid)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            EmployeeModel employeedetail = new EmployeeModel();
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[0].Value = empid;

                SqlDataReader dr = objGetTraining.ExecuteReaderSP(SPNames.TNI_GetEmployeeDetailwithProject, sqlParam);
                while (dr.Read())
                {
                    employeedetail.EmpId = Convert.ToInt16(dr[DbTableColumn.EMPId]);
                    employeedetail.EmployeeName = Convert.ToString(dr[DbTableColumn.EmployeeName]);
                    employeedetail.EmailID = Convert.ToString(dr[DbTableColumn.EmailId]);
                    employeedetail.Designation = Convert.ToString(dr[DbTableColumn.Designation]);
                    employeedetail.PrimarySkill = Convert.ToString(dr[DbTableColumn.PrimarySkills]);
                }
                return employeedetail;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, clsEmployeeRepository, "GetEmployeeDetailByID", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }
        }

        public IEnumerable<SelectListItem> FillDesignationList(int DeptId)
        {
            try
            {
                DataAccessClass dataAccessClass = new DataAccessClass();
                dataAccessClass.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.DepartmentId, SqlDbType.Int);
                sqlParam[0].Value = DeptId;
                SqlDataReader objReader = dataAccessClass.ExecuteReaderSP(SPNames.Employee_GetEmployeeDesignations, sqlParam);

                SelectListItem selListItem;
                List<SelectListItem> newList = new List<SelectListItem>();
                newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "" });

                while (objReader.Read())
                {
                    //assessmentModel.AssessmentPaper.AssessmentPaperId = Convert.ToInt32(dr[DbTableColumn.AssessmentPaperId]);
                    selListItem = new SelectListItem() { Value = objReader[0].ToString().Trim(), Text = objReader[1].ToString().Trim() };
                    newList.Add(selListItem);
                }
                return new SelectList(newList, "Value", "Text");
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, clsEmployeeRepository, "GetDesignation", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
        }

        public List<EmployeeProjectAllocationModel> Employee_BillingTillDate(EmployeeProjectAllocationModel ObjEmp)
        {
            List<EmployeeProjectAllocationModel> objList = new List<EmployeeProjectAllocationModel>();
            using (var _context = new RMS_Entities())
            {
                var db = _context.USP_Employee_BillingTillDate(Convert.ToString(ObjEmp.Flag), Convert.ToDateTime(ObjEmp.BillingTillDate), Convert.ToInt32(ObjEmp.EmpId),
                                                                Convert.ToInt32(ObjEmp.EPAId)).ToList<USP_Employee_BillingTillDate_Result>();
                EmployeeProjectAllocationModel objEmpl = null;
                foreach (var objSingle in db)
                {
                    objEmpl = new EmployeeProjectAllocationModel();
                    objEmpl.EPAId = objSingle.EPAId;
                    objEmpl.EPAIdEncr = CommonRepository.Encode(objEmpl.EPAId.ToString());
                    objEmpl.EmpId = objSingle.EmpId;
                    objEmpl.EmployeeName = objSingle.EmployeeName;
                    ProjectResult objProject = new ProjectResult();
                    objProject.ProjectName = objSingle.ProjectName;
                    objEmpl.Projects = objProject;
                    objEmpl.StartDate = Convert.ToDateTime(objSingle.AllocationDate);
                    objEmpl.ActualRelDate = Convert.ToDateTime(objSingle.ReleaseDate);
                    objEmpl.BillingTillDate = Convert.ToDateTime(objSingle.BillingTillDate);
                    //objEmpl.FirstName= objSingle.p

                    objList.Add(objEmpl);
                }
                return objList;
            }
        }
    }
}