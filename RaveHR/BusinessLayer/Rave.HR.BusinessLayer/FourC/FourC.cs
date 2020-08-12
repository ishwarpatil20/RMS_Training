using System;
using System.Collections.Generic;
using System.Text;
using BusinessEntities;
using Common;
using System.Web;
using Rave.HR.BusinessLayer.Interface;
using Common.AuthorizationManager;
using System.Collections;
using System.Data;

namespace Rave.HR.BusinessLayer.FourC
{
    public class FourC
    {

        #region Constants
        /// <summary>
        /// private variable for Class Name used in each catch block
        /// </summary>
        private string CLASS_NAME = "FourC";
        

        #endregion Constants


        #region Mentods
        
        /// <summary>
        /// Gets the employees details.
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DataSet GetCreatorApproverDetails()
        {
            DataSet empDetails = new DataSet();
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                empDetails = objEmpDetailsDAL.GetCreatorApproverDetails();
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
        public DataSet GetViewAccessRightsData()
        {
            DataSet empDetails = new DataSet();
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                empDetails = objEmpDetailsDAL.GetViewAccessRightsData();
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
        /// Gets the Project data
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DataSet Get4CEmployeeDeatils(int loginEmpId, int deptId, int projectId, int month, int year, string fourCRole, int empId)
        {
            DataSet empDetails = new DataSet();
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                empDetails = objEmpDetailsDAL.Get4CEmployeeDeatils(loginEmpId, deptId, projectId, month, year, fourCRole, empId);
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
        /// Gets the Project data
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DataTable Get4CEmployeeDeatilsCarryForward(int loginEmpId, int deptId, int projectId, int month, int year, string fourCRole, string empIdlist)
        {
            DataTable empDetails = new DataTable();
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                empDetails = objEmpDetailsDAL.Get4CEmployeeDeatilsCarryForward(loginEmpId, deptId, projectId, month, year, fourCRole, empIdlist);
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
        /// Gets the Project data
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DataSet GetCreatorReviewerDetails(int deptId, int projectId)
        {
            DataSet empDetails = new DataSet();
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                empDetails = objEmpDetailsDAL.GetCreatorReviewerDeatils(deptId, projectId);
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
        /// Gets the Project data
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DataSet Get4CReviewerEmployeeDeatils(int loginEmpId, int deptId, int projectId, int month, int year, string fourCRole, int empId)
        {
            DataSet empDetails = new DataSet();
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                empDetails = objEmpDetailsDAL.Get4CReviewerEmployeeDeatils(loginEmpId, deptId, projectId, month, year, fourCRole, empId);
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
        /// Gets the Project data
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DataSet Get4CCreatorDeatils(int month, int year, string fourCRole, string emailId)
        {
            DataSet empDetails = new DataSet();
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                empDetails = objEmpDetailsDAL.Get4CCreatorDeatils(month, year, fourCRole, emailId);
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
        /// Gets the Project data
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public FourCFeedback Get4CIndividualEmployeeDeatils(int deptid, int projectId, int month, int year, int empId)
        {
            FourCFeedback objFeedback;
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                objFeedback = objEmpDetailsDAL.Get4CIndividualEmployeeDeatils(deptid, projectId, month, year, empId);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetReportingFunctionalManagerIds", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return objFeedback;
        }

        



        /// <summary>
        /// Gets the employees details.
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public bool CheckCreatorReviewerSetForAll()
        {
            bool flag = false;
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                flag = objEmpDetailsDAL.CheckCreatorReviewerSetForAll();
                return flag;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetReportingFunctionalManagerIds", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return flag;
        }


        ///// <summary>
        ///// Gets the employees details.
        ///// </summary>
        ///// <param name="objParameter">EMPId</param>
        ///// <returns></returns>
        //public bool Check4CAccessRights(string emaild)
        //{
        //    bool flag = false;
        //    try
        //    {
        //        Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
        //        flag = objEmpDetailsDAL.Check4CAccessRights(emaild);
        //        return flag;
        //    }
        //    catch (RaveHRException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetReportingFunctionalManagerIds", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
        //    }

        //    return flag;
        //}


        ///// <summary>
        ///// Gets the employees details.
        ///// </summary>
        ///// <param name="objParameter">EMPId</param>
        ///// <returns></returns>
        //public DataSet Check4CLoginRights(string emaild)
        //{
        //    DataSet ds = null;
        //    try
        //    {
        //        Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
        //        ds = objEmpDetailsDAL.Check4CLoginRights(emaild);
        //        return ds;
        //    }
        //    catch (RaveHRException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetReportingFunctionalManagerIds", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
        //    }

        //    return ds;
        //}


        /// <summary>
        /// Gets the employees details.
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public List<string> Check4CLoginRights(string emaild, ref int loginEmpId)
        {
            List<string> lstRights = new List<string> { };
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                lstRights = objEmpDetailsDAL.Check4CLoginRights(emaild, ref loginEmpId);
                return lstRights;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetReportingFunctionalManagerIds", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return lstRights;
        }


        /// <summary>
        /// Gets the employees details.
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DateTime? CurrentMonthLastDay()
        {
            DateTime? dtCurrentLastDayOfMonth = null;
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                dtCurrentLastDayOfMonth = objEmpDetailsDAL.CurrentMonthLastDay();
                return dtCurrentLastDayOfMonth;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetReportingFunctionalManagerIds", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

           // return dtCurrentLastDayOfMonth;
        }



        /// <summary>
        /// Gets the employees details.
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DataSet GetFilteredCreatorApproverDetails(int DeptId, int ProjectId)
        {
            DataSet empDetails = new DataSet();
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                empDetails = objEmpDetailsDAL.GetFilteredCreatorApproverDetails(DeptId, ProjectId);
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
        /// Updates Creator Reviewer 
        /// </summary>
        /// <param name="objUpdateEmployee"></param>
        /// <returns></returns>
        public void AddUpdateDeleteCreatorReviewer(int deptId, int ProjectId, string strCreator, string strReviewer, string ModifiedById, string mode)
        {
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objUpdateCR = new Rave.HR.DataAccessLayer.FourC.FourC();
                objUpdateCR.AddUpdateDeleteCreatorReviewer(deptId, ProjectId, strCreator, strReviewer, ModifiedById, mode);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "UpdateCreatorReviewer", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }


        /// <summary>
        /// Add Delete View Access Rights
        /// </summary>
        /// <param name="objUpdateEmployee"></param>
        /// <returns></returns>
        public void AddDeleteViewAccessRights(List<string> lst, string ModifiedById, string mode)
        {
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objUpdateCR = new Rave.HR.DataAccessLayer.FourC.FourC();
                objUpdateCR.AddDeleteViewAccessRights(lst, ModifiedById, mode);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "UpdateCreatorReviewer", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }


        /// <summary>
        /// Gets master data such as ProjectStatus, MRFStatus etc.
        /// </summary>
        /// <returns>List</returns>
        public List<BusinessEntities.Master> GetParameter(string Category)
        {
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objFourC = new Rave.HR.DataAccessLayer.FourC.FourC();
                DataTable dtMasterData = new DataTable();
                dtMasterData = objFourC.FillParameterList(Category);

                List<BusinessEntities.Master> listMasterData = new List<BusinessEntities.Master>();
                BusinessEntities.Master fetchMasterData = null;
                foreach (DataRow drMasterData in dtMasterData.Rows)
                {
                    fetchMasterData = new BusinessEntities.Master();
                    fetchMasterData.MasterId = int.Parse(drMasterData["MasterID"].ToString());
                    fetchMasterData.MasterName = drMasterData["MasterName"].ToString();
                    listMasterData.Add(fetchMasterData);
                }
                return listMasterData;
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw ex;
                //throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, "RaveHRMaster.cs", "GetMasterData");
            }
        }


        /// <summary>
        /// Gets master data such as ProjectStatus, MRFStatus etc.
        /// </summary>
        /// <returns>List</returns>
        public List<BusinessEntities.Master> GetActionOwner(int projectId)
        {
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objFourC = new Rave.HR.DataAccessLayer.FourC.FourC();
                DataTable dtMasterData = new DataTable();
                dtMasterData = objFourC.FillActionOwner(projectId);

                List<BusinessEntities.Master> listMasterData = new List<BusinessEntities.Master>();
                BusinessEntities.Master fetchMasterData = null;
                foreach (DataRow drMasterData in dtMasterData.Rows)
                {
                    fetchMasterData = new BusinessEntities.Master();
                    fetchMasterData.MasterId = int.Parse(drMasterData["MasterID"].ToString());
                    fetchMasterData.MasterName = drMasterData["MasterName"].ToString();
                    listMasterData.Add(fetchMasterData);
                }
                return listMasterData;
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw ex;
                //throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, "RaveHRMaster.cs", "GetMasterData");
            }
        }


        /// <summary>
        /// Gets the employees details.
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DataSet FillProjectDropdownOnEmp(int empId, int month, int year)
        {
            DataSet dsDetails = new DataSet();
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                dsDetails = objEmpDetailsDAL.FillProjectDropdownOnEmp(empId, month, year);
                return dsDetails;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetReportingFunctionalManagerIds", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return dsDetails;
        }


        /// <summary>
        /// Gets the employees details.
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DataSet Get4CActionDetails(int deptid, int empId, int projectId, int month, int year, string loginEmailId, int Mode)
        {
            DataSet dsDetails = new DataSet();
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                dsDetails = objEmpDetailsDAL.Get4CActionDetails(deptid, empId, projectId, month, year, loginEmailId, Mode);
                return dsDetails;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetReportingFunctionalManagerIds", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return dsDetails;
        }

        /// <summary>
        /// Gets the employees details.
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DataSet Get4CActionDetailsForDashboard(int deptid, int empId, int fbId, int projectId, int month, int year, string loginEmailId, int Mode)
        {
            DataSet dsDetails = new DataSet();
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                dsDetails = objEmpDetailsDAL.Get4CActionDetailsForDashboard(deptid, empId, fbId, projectId, month, year, loginEmailId, Mode);
                return dsDetails;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetReportingFunctionalManagerIds", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            //return dsDetails;
        }


        /// <summary>
        /// Gets the employees details.
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DataSet GetEmpDashboardData(int empId, int Months)
        {
            DataSet dsDetails = new DataSet();
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                dsDetails = objEmpDetailsDAL.GetEmpDashboardData(empId, Months);
                return dsDetails;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetReportingFunctionalManagerIds", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            //return dsDetails;
        }


        /// <summary>
        /// Gets the employees details.
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public bool InsertActionDetails(DataTable dtActionData, FourCFeedback objFeedBack)
        {
            bool flag = false;
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                flag = objEmpDetailsDAL.InsertActionDetails(dtActionData, objFeedBack);
                return flag;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetReportingFunctionalManagerIds", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return flag;
        }

        /// <summary>
        /// Gets the employees details.
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public bool IsAllowToFillActionData(int deptid, int projectId, string strMailId)
        {
            bool flag = false;
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                flag = objEmpDetailsDAL.IsAllowToFillActionData(deptid, projectId, strMailId);
                return flag;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetReportingFunctionalManagerIds", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            //return flag;
        }

        public DataSet GetProjectName()
        {
            Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
            DataSet ds = null;
            try
            {
                ds = objEmpDetailsDAL.GetProjectName();
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetProjectName", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
            return ds;
        }

        public DataSet GetProjectNameAddCreatorReviewer()
        {
            Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
            DataSet ds = null;
            try
            {
                ds = objEmpDetailsDAL.GetProjectNameAddCreatorReviewer();
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetProjectName", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
            return ds;
        }


        public DataSet GetDepartmentName(string UserMailId)
        {
            Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
            DataSet ds = null;
            try
            {
                ds = objEmpDetailsDAL.GetDepartmentName(UserMailId);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetProjectName", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
            return ds;
        }

        /// <summary>
        /// Gets the Project Name.
        /// </summary>
        /// <returns>Collection</returns>
        public DataSet GetDepartmentName(string mailId, string roleCreatorApprover, string strFourCType)
        {
            // Initialise the Data Layer object

            Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
            // Initialise the Collection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();
            DataSet ds = null;
            try
            {
                // Call the Data Layer Method
                //raveHRCollection = objEmpDetailsDAL.GetProjectName(mailId, roleCreatorApprover, strFourCType);
                ds = objEmpDetailsDAL.GetDepartmentName(mailId, roleCreatorApprover, strFourCType);

                // Return the Collection
                return ds;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetProjectName", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }


        public DataSet GetDepartmentForCreatorApprover()
        {
            Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
            DataSet ds = null;
            try
            {
                ds = objEmpDetailsDAL.GetDepartmentForCreatorApprover();
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetProjectName", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
            return ds;
        }


        /// <summary>
        /// Gets the Project Name.
        /// </summary>
        /// <returns>Collection</returns>
        public DataSet GetFunctionalManagerDeptName(int empId)
        {
            // Initialise the Data Layer object

            Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
            // Initialise the Collection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();
            DataSet ds = null;
            try
            {
                // Call the Data Layer Method
                //raveHRCollection = objEmpDetailsDAL.GetProjectName(mailId, roleCreatorApprover, strFourCType);

                ds = objEmpDetailsDAL.GetFunctionalManagerDeptName(empId);

                // Return the Collection
                return ds;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetProjectName", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Gets the Project Name.
        /// </summary>
        /// <returns>Collection</returns>
        public DataSet GetFunctionalManagerProjectName(int empId)
        {
            // Initialise the Data Layer object

            Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
            // Initialise the Collection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();
            DataSet ds = null;
            try
            {
                // Call the Data Layer Method
                //raveHRCollection = objEmpDetailsDAL.GetProjectName(mailId, roleCreatorApprover, strFourCType);

                ds = objEmpDetailsDAL.GetFunctionalManagerProjectName(empId);

                // Return the Collection
                return ds;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetProjectName", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Gets the Project Name.
        /// </summary>
        /// <returns>Collection</returns>
        public DataSet GetFunctionalManagerEmployeeName(int empId)
        {
            // Initialise the Data Layer object

            Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
            // Initialise the Collection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();
            DataSet ds = null;
            try
            {
                // Call the Data Layer Method
                //raveHRCollection = objEmpDetailsDAL.GetProjectName(mailId, roleCreatorApprover, strFourCType);

                ds = objEmpDetailsDAL.GetFunctionalManagerEmployeeName(empId);

                // Return the Collection
                return ds;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetProjectName", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }



        /// <summary>
        /// Gets the Project Name.
        /// </summary>
        /// <returns>Collection</returns>
        public DataSet GetProjectName(string mailId, string roleCreatorApprover, string strFourCType)
        {
            // Initialise the Data Layer object

            Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
            // Initialise the Collection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();
            DataSet ds = null;
            try
            {
                // Call the Data Layer Method
                //raveHRCollection = objEmpDetailsDAL.GetProjectName(mailId, roleCreatorApprover, strFourCType);
                ds = objEmpDetailsDAL.GetProjectName(mailId, roleCreatorApprover, strFourCType);

                // Return the Collection
                return ds;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetProjectName", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }


        ///// <summary>
        ///// Gets the employees details.
        ///// </summary>
        ///// <param name="objParameter">EMPId</param>
        ///// <returns></returns>
        //public bool CheckRatingFillForAll(string ratingOption, string ratingType, int deptid, int projectid, int month, int year, int loginEmpId)
        //{
        //    bool flag = false;
        //    try
        //    {
        //        Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
        //        flag = objEmpDetailsDAL.CheckRatingFillForAll(ratingOption, ratingType, deptid, projectid, month, year, loginEmpId);
        //        return flag;
        //    }
        //    catch (RaveHRException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetReportingFunctionalManagerIds", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
        //    }

            
        //}

        /// <summary>
        /// Gets the employees details.
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
       // public bool SubmitReviewRating(int projectId, int month, int year, string emailId, DataTable dtFinalRating)
       // public bool SubmitReviewRating(string emailId, string ratingOption, DataTable dtFinalRating)
        public bool SubmitReviewRating(string ratingOption, string finalSubmit, int deptid, int projectid, int month, int year, int loginEmpId, DataTable dtFinalRating)
        {
            bool flag = false;
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                //flag = objEmpDetailsDAL.SubmitReviewRating(emailId, ratingOption, dtFinalRating);
                flag = objEmpDetailsDAL.SubmitReviewRating(ratingOption, finalSubmit, deptid, projectid, month, year, loginEmpId, dtFinalRating);
                return flag;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetReportingFunctionalManagerIds", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return flag;
        }


        ///// <summary>
        ///// Gets the employees details.
        ///// </summary>
        ///// <param name="objParameter">EMPId</param>
        ///// <returns></returns>
        //public DataSet GetSupportEmployeeList(string emailId, int month, int year)
        //{
        //    DataSet empDetails = new DataSet();
        //    try
        //    {
        //        Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
        //        empDetails = objEmpDetailsDAL.GetSupportEmployeeList(emailId, month, year);
        //    }
        //    catch (RaveHRException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetReportingFunctionalManagerIds", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
        //    }

        //    return empDetails;
        //}


        ///// <summary>
        ///// Gets the Project data
        ///// </summary>
        ///// <param name="objParameter">EMPId</param>
        ///// <returns></returns>
        //public DataSet Get4CCreatorDeatilsAtSupportLevel(int month, int year, string fourCRole, string emailId, string FillOrView)
        //{
        //    DataSet empDetails = new DataSet();
        //    try
        //    {
        //        Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
        //        empDetails = objEmpDetailsDAL.Get4CCreatorDeatilsAtSupportLevel(month, year, fourCRole, emailId, FillOrView);
        //    }
        //    catch (RaveHRException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetReportingFunctionalManagerIds", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
        //    }

        //    return empDetails;
        //}


        /// <summary>
        /// Gets the Project data
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DataSet Get4CViewFeedbackDeatils(int deptd, int projectid, int month, int year, int empId, int fnEmpId, string role)
        {
            DataSet empDetails = new DataSet();
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                empDetails = objEmpDetailsDAL.Get4CViewFeedbackDeatils(deptd, projectid, month, year, empId, fnEmpId, role);
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
        /// Gets the Project data
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DataSet Get4CViewEmployeeFromRMS(int deptd, int projectid, int month, int year, int functionalManagerId)
        {
            DataSet empDetails = new DataSet();
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                empDetails = objEmpDetailsDAL.Get4CViewEmployeeFromRMS(deptd, projectid, month, year, functionalManagerId);
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
        public void CheckReviewerIsAllowForDepartment(int deptId, int projectId, string creator, string reviewer, ref bool isAllowCreator, ref bool isAllowReviewer)
        {
            
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                objEmpDetailsDAL.CheckReviewerIsAllowForDepartment(deptId, projectId, creator, reviewer, ref isAllowCreator, ref isAllowReviewer);
                //return flag;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetReportingFunctionalManagerIds", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            //return flag;
        }


        /// <summary>
        /// Not Applicable validation
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public bool IsValidForNotApplication(int deptid, int projectId, int month, int year, int empId)
        {
            bool flag = false;
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                flag = objEmpDetailsDAL.IsValidForNotApplication(deptid, projectId, month, year, empId);
                return flag;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetReportingFunctionalManagerIds", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            //return flag;
        }



        /// <summary>
        /// Gets the employees details.
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public void GetRedirectMonth(int loginEmpId, int DeptId, int ProjectId, int month, int year, int mode, ref int redirectMonth, ref int redirectYear)
        {
            
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                objEmpDetailsDAL.GetRedirectMonth(loginEmpId, DeptId, ProjectId, month, year, mode, ref redirectMonth, ref redirectYear);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetReportingFunctionalManagerIds", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }


        /// <summary>
        /// RMS 4C Rating Validation
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public bool Check4CRatingFillForAll(int EmpId, DateTime dtReleaseDate, int projectId)
        {
            bool flag = false;
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                flag = objEmpDetailsDAL.Check4CRatingFillForAll(EmpId, dtReleaseDate, projectId);
                return flag;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetReportingFunctionalManagerIds", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            //return flag;
        }


        /// <summary>
        /// Gets the Project data
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DataSet GetActionReport(int loginEmpId, string EmpId, string ActionOwnerId, string deptid, string projectId, string CType, string ColorRating, int MonthDuration)//, bool IsSupportdept)
        {
            DataSet empDetails = new DataSet();
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                empDetails = objEmpDetailsDAL.GetActionReport(loginEmpId, EmpId, ActionOwnerId, deptid, projectId, CType, ColorRating, MonthDuration);//, IsSupportdept);
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
        /// Gets the Project data
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DataSet GetConsolidatedReport(int loginEmpId, string EmpId, string designationId, string deptid, string projectId, int MonthDuration)//, bool IsSupportdept)
        {
            DataSet empDetails = new DataSet();
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                empDetails = objEmpDetailsDAL.GetConsolidatedReport(loginEmpId, EmpId, designationId, deptid, projectId, MonthDuration);//,IsSupportdept );
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
        /// Gets the Project data
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DataSet GetAnalysisReport(int loginEmpId, int period, string deptId, string projectId, string designationId, string CType)//, bool IsSupportdept)
        {
            DataSet empDetails = new DataSet();
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                empDetails = objEmpDetailsDAL.GetAnalysisReport(loginEmpId, period, deptId, projectId, designationId, CType);//, IsSupportdept);
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
        /// Gets the Project data
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DataSet GetStatusReport(int loginEmpId, int month, int year)//, bool IsSupportdept)
        {
            DataSet empDetails = new DataSet();
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                empDetails = objEmpDetailsDAL.GetStatusReport(loginEmpId, month, year);//, IsSupportdept);
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
        /// Gets the Project data
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DataSet GetMovementReport(int loginEmpId, int month)//, bool IsSupportdept)
        {
            DataSet empDetails = new DataSet();
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                empDetails = objEmpDetailsDAL.GetMovementReport(loginEmpId, month);//, IsSupportdept);
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
        /// Gets the Project data
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DataSet GetCountReport(int loginEmpId, int month)//, bool IsSupportdept)
        {
            DataSet empDetails = new DataSet();
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                empDetails = objEmpDetailsDAL.GetCountReport(loginEmpId, month);//,  IsSupportdept);
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
        /// Master data for dropDown
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection Fill4CReportDesignation(int empId, string deptval)
        {
            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                raveHRCollection = objEmpDetailsDAL.Fill4CDesignationReportDL(empId, deptval);


                return raveHRCollection;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
        }


        public void SendMailForSendForReview(int deptId, int projectId, string monthYear, int creatorEmpId)
        {
            try
            {
                

                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                DataSet dsCratorReviewerDetails = objEmpDetailsDAL.GetCreatorReviewerMailsDeatils(deptId, projectId, creatorEmpId);

                if (dsCratorReviewerDetails != null && dsCratorReviewerDetails.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(dsCratorReviewerDetails.Tables[0].Rows[0]["ReviewerEmailId"].ToString()))
                {

                    IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.FourC),
                                                   Convert.ToInt16(EnumsConstants.EmailFunctionality.SendForReview));

                    obj.Subject = string.Format(obj.Subject, dsCratorReviewerDetails.Tables[0].Rows[0]["ProjectName"].ToString(),
                                                                 monthYear);

                    obj.Body = string.Format(obj.Body, dsCratorReviewerDetails.Tables[0].Rows[0]["ReviewerName"].ToString(), dsCratorReviewerDetails.Tables[0].Rows[0]["ProjectName"].ToString(), monthYear, dsCratorReviewerDetails.Tables[0].Rows[0]["CreatorName"].ToString());

                    obj.To.Add(dsCratorReviewerDetails.Tables[0].Rows[0]["ReviewerEmailId"].ToString());
                    obj.From = dsCratorReviewerDetails.Tables[0].Rows[0]["CreatorEmailId"].ToString();

                   // obj.To.Add("mahendra.bharambe@rave-tech.com");
                   // obj.From = "mahendra.bharambe@rave-tech.com";

                    obj.SendEmail(obj);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
        }



        public void RejectRatingSendMailToCreator(int fbId, int loginEmpId, string empName, string projectName, string monthYear, string rejectRemarks)
        {
            try
            {


                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                DataSet dsCratorReviewerDetails = objEmpDetailsDAL.GetRejectRatingMailsDeatils(fbId, loginEmpId);

                if (dsCratorReviewerDetails != null && dsCratorReviewerDetails.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(dsCratorReviewerDetails.Tables[0].Rows[0]["ReviewerEmailId"].ToString()))
                {

                    IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.FourC),
                                                   Convert.ToInt16(EnumsConstants.EmailFunctionality.RejectRating4C));

                    obj.Subject = string.Format(obj.Subject, empName);

                    obj.Body = string.Format(obj.Body, dsCratorReviewerDetails.Tables[0].Rows[0]["CreatorName"].ToString(), empName, monthYear, projectName, rejectRemarks, empName, dsCratorReviewerDetails.Tables[0].Rows[0]["ReviewerName"].ToString());

                    obj.To.Add(dsCratorReviewerDetails.Tables[0].Rows[0]["CreatorEmailId"].ToString());
                    obj.From = dsCratorReviewerDetails.Tables[0].Rows[0]["ReviewerEmailId"].ToString();
                    obj.CC.Add(dsCratorReviewerDetails.Tables[0].Rows[0]["ReviewerEmailId"].ToString());


                    // obj.To.Add("mahendra.bharambe@rave-tech.com");
                    // obj.From = "mahendra.bharambe@rave-tech.com";

                    obj.SendEmail(obj);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Reject 4C rating 
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public bool Reject4CRating(int fbId, string rejectRemarks, int loginEmpId, string empName, string projectName, string monthYear)
        {
            bool flag = false;
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();

                RejectRatingSendMailToCreator(fbId, loginEmpId, empName, projectName, monthYear, rejectRemarks);

                flag = objEmpDetailsDAL.Reject4CRating(fbId, rejectRemarks, loginEmpId);

                return flag;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetReportingFunctionalManagerIds", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return flag;
        }

        /// <summary>
        /// Gets the Project data
        /// </summary>
        /// <param name="objParameter">EMPId</param>
        /// <returns></returns>
        public DataSet ExportToExcelSentForReviewRatings(int loginEmpId)
        {
            DataSet empDetails = new DataSet();
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                empDetails = objEmpDetailsDAL.ExportToExcelSentForReviewRatings(loginEmpId);
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

        //Venkatesh : 4C_Support : 18/3/2014 : Start 
        public void SendMailForSendForReviewSupport(int deptId, int projectId, string monthYear, int creatorEmpId)
        {
            try
            {


                Rave.HR.DataAccessLayer.FourC.FourC objEmpDetailsDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                DataSet dsCratorReviewerDetails = objEmpDetailsDAL.GetCreatorReviewerMailsDeatils(deptId, projectId, creatorEmpId);

                if (dsCratorReviewerDetails != null && dsCratorReviewerDetails.Tables[0].Rows.Count > 0 && !string.IsNullOrEmpty(dsCratorReviewerDetails.Tables[0].Rows[0]["ReviewerEmailId"].ToString()))
                {

                    IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.FourC),
                                                   Convert.ToInt16(EnumsConstants.EmailFunctionality.SendForReviewSupport));

                    obj.Subject = string.Format(obj.Subject, dsCratorReviewerDetails.Tables[0].Rows[0]["DepartmentName"].ToString(),
                                                                 monthYear);

                    obj.Body = string.Format(obj.Body, dsCratorReviewerDetails.Tables[0].Rows[0]["ReviewerName"].ToString(), dsCratorReviewerDetails.Tables[0].Rows[0]["DepartmentName"].ToString(), monthYear, dsCratorReviewerDetails.Tables[0].Rows[0]["CreatorName"].ToString());

                    obj.To.Add(dsCratorReviewerDetails.Tables[0].Rows[0]["ReviewerEmailId"].ToString());
                    obj.From = dsCratorReviewerDetails.Tables[0].Rows[0]["CreatorEmailId"].ToString();

                    obj.SendEmail(obj);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
        }
        //Venkatesh : 4C_Support : 18/3/2014 : End

        //Ishwar Patil 25082015 Start
        //Desc : 4C hightlight CR
        public DataTable Get4C_ActionDetailsHighlightForDashboard(int EmpId, int deptId, int projectId, int month, int year)
        {
            DataTable empDetails = new DataTable();
            try
            {
                Rave.HR.DataAccessLayer.FourC.FourC objHighlightDAL = new Rave.HR.DataAccessLayer.FourC.FourC();
                empDetails = objHighlightDAL.Get4C_ActionDetailsHighlightForDashboard(EmpId, deptId, projectId, month, year);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "Get4C_ActionDetailsHighlightForDashboard", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }

            return empDetails;
        }
        //Ishwar Patil 25082015 End
        
        #endregion Mentods
    }
}
