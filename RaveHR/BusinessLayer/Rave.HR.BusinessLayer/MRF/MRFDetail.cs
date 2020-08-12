//------------------------------------------------------------------------------
//
//  File:           MRFDetail.cs       
//  Author:         Chhaya.Gunjal
//  Date written:   09/03/2009 01:39:10 PM
//  Description:    Class Contains business functionality for MRF module.
//  Amendments
//  Date                    Who                 Ref     Description
//  ----                    ---                 ---     -----------
//  09/03/2009 01:39:10 PM   Sunil.Mishra         n/a     Created    
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using BusinessEntities;
using Common;
using Common.AuthorizationManager;
using Common.Constants;
using Rave.HR.BusinessLayer.Interface;
using System.Globalization;
using Rave.HR.DataAccessLayer.MRF;

namespace Rave.HR.BusinessLayer.MRF
{


    public class MRFDetail
    {
        #region Private Members

        /// <summary>
        /// private variable for Class Name used in each catch block
        /// </summary>
        private const string MRFDETAIL = "MRFDetail";
        public const string SETMRFAPPROVEREJECTREASON = "SetMRFApproveRejectReason";
        //Mahednra STRAT Issue Id : 33860
        public const string ConcurrencyResourceAllocation = "checkConcurrencyResourceAllocation";
        //Mahednra END Issue Id : 33860


        public const string GETPROJECTNAMEROLEWISE = "GetProjectNameRoleWise";
        private const string GETMRFDETAILSFORAPPROVALOFMRFBYHEADCOUNT = "GetMRFDetailsForApprocalOfMRFByHeadCount";
        private const string GETLISTOFINTERNALRESOURCE = "GetListOfInternalResource";
        private const string MRFPENDINGALLOCATION = "MrfPendingAllocation";
        private const string GETMRFDETAILSFORPEDINGALLOCATION = "GetMRFDetailsForPendingAllocation";
        private const string SETMRFAPPROVEREJECTREASONFORPM = "SetMRFApproveRejectReasonForPM";
        private const string SETMRFSATUSAFTERAPPROVAL = "SetMRFSatusAfterApproval";
        //private const string RAVEDOMAIN = "@rave-tech.com";
        private const string PROJECT = "Projects";
        private const string EMAILMSG = "position is raised successfully,Email notification is sent for allocation.";
        private const string RMOTeam = "RMO Team";
        private string message = string.Empty;
        private BusinessEntities.MRFDetail mrfDetail;
        private static BusinessEntities.RaveHRCollection raveHRCollection;
        Rave.HR.BusinessLayer.MRF.MRFDetail mrfDetailBL;

        int MRFID;

        /// <summary>
        /// Function name : DeleteMRFBL
        /// </summary>
        private const string DELETEMRFBL = "DeleteMRFBL";
        /// <summary>
        /// Function name : GetMrfSummary
        /// </summary>
        private const string GETMRFSUMMARY = "GetMrfSummary";

        /// <summary>
        /// Function name : GetMrfCode
        /// </summary>
        private const string GETMRFCODE = "GetMrfCode";

        /// <summary>
        /// Function name : GetProjectName
        /// </summary>
        private const string GETPROJECTNAME = "GetProjectName";

        /// <summary>
        /// Function name : GetApproveRejectMrf
        /// </summary>
        private const string GETAPPROVEREJECTMRF = "GetApproveRejectMrf";

        /// <summary>
        /// Function name : GetMrfSummaryForPageLoad
        /// </summary>
        private const string GETMRFSUMMARYFORPAGELOAD = "GetMrfSummaryForPageLoad";

        /// <summary>
        /// Function name : AbortMRFBL
        /// </summary>
        private const string ABORTMRFBL = "AbortMRFBL";

        /// <summary>
        /// Function name : GetRoleDepartmentWiseBL
        /// </summary>
        private const string GETROLEDEPARTMENTWISEBL = "GetRoleDepartmentWiseBL";

        /// <summary>
        /// Function name : EditMRFBL
        /// </summary>
        private const string EDITMRFBL = "EditMRFBL";

        /// <summary>
        /// Function name : GetRecruitmentManager
        /// </summary>
        private const string GETRECRUITMENTMANAGER = "GetRecruitmentManager";

        /// <summary>
        /// Authorisation manager 
        /// </summary>
        AuthorizationManager objAuMan = new AuthorizationManager();

        /// <summary>
        /// Gets the logged in user name
        /// </summary>
        string LoggedInUserName;

        /// <summary>
        /// User name
        /// </summary>
        string[] UserName;

        /// <summary>
        /// Name to be appended in mail at "Regards".
        /// </summary>
        private string RegardsName;

        /// <summary>
        /// Function name : GetMailForResourceAllocation
        /// </summary>
        private const string GETMAILFORRESOURCEALLOCATION = "GetMailForResourceAllocation";

        /// <summary>
        /// Function name : GetLink
        /// </summary>
        private const string GETLINK = "GetLink";

        /// <summary>
        /// Rave Domain
        /// </summary>
        /// 
        //Googleconfigurable
        //const string RAVEDOMAIN = "@rave-tech.com";
        //const string DOMAIN = "@rave-tech.co.in";

        /// <summary>
        /// Function name : GETHTMLFORTABLEDATA
        /// </summary>
        private const string GETHTMLFORTABLEDATA = "GetHTMLForTableData";

        /// <summary>
        /// Function name : GETALLOCATIONDATEDETAILS
        /// </summary>
        private const string GETALLOCATIONDATEDETAILS = "GetAllocationDateDetails";

        //declaring global parameters
        string _project = "Project";
        string _department = "Department";
        string _logInUser = "RMO Team";

        //Declare Headers for Tabular Format Mails
        private const string _headerStartDate = "Start Date:";
        private const string _headerEndDate = "End Date:";
        private const string _headerUtilization = "Utilization:";
        private const string _headerBilling = "Billing:";
        private const string _checkNullValue = "&nbsp;";
        private const string _clientName = "Client Name:";


        //Qurey String Constants
        private const string _pageType = "pagetype";
        private const string _index = "index";
        private const string _zero = "0";
        private const string _and = "&";
        private const string _questionMark = "?";
        private const string _true = "True";
        private const string _isAccessUrl = "isAccessUrl";

        #endregion

        #region Methods

        /// <summary>
        /// Gets the Project Name.
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetProjectName()
        {
            // Initialise the Data Layer object
            DataAccessLayer.MRF.MRFDetail projectNameDL = new Rave.HR.DataAccessLayer.MRF.MRFDetail();

            // Initialise the Collection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();

            try
            {
                // Call the Data Layer Method
                raveHRCollection = projectNameDL.GetProjectName();

                // Return the Collection
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETPROJECTNAME, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }


        //Jignyasa Issue id : 42400,42315
        /// <summary>
        /// Gets the client name from Project ID.
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetClientNameFromProjectID(int ProjectID)
        {
            // Initialise the Data Layer object
            DataAccessLayer.MRF.MRFDetail projectNameDL = new Rave.HR.DataAccessLayer.MRF.MRFDetail();

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
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETPROJECTNAME, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Get the MRF Code
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetMrfCode()
        {
            // Initialise the Data Layer object
            DataAccessLayer.MRF.MRFDetail mrfCodeDL = new Rave.HR.DataAccessLayer.MRF.MRFDetail();

            // Initialise the Collection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();

            try
            {
                // Call the Data Layer Method
                // raveHRCollection = mrfCodeDL.GetMrfCode();

                // Return the Collection
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETMRFCODE, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// This function Gets the MRF Summary
        /// </summary>
        /// <param name="objParameter"></param>
        /// <param name="mrfDetail"></param>
        /// <param name="pageCount"></param>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetMrfSummary(BusinessEntities.ParameterCriteria objParameter, BusinessEntities.MRFDetail mrfDetail, ref int pageCount)
        {
            // Initialise the Data Layer object
            DataAccessLayer.MRF.MRFDetail mrfSummaryDL = new Rave.HR.DataAccessLayer.MRF.MRFDetail();

            // Initialise the Colection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();

            try
            {
                // Call the Data Layer method
                raveHRCollection = mrfSummaryDL.GetMrfSummary(objParameter, mrfDetail, ref pageCount);
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETMRFSUMMARY, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }

            // Return the Collection
            return raveHRCollection;
        }

        /// <summary>
        /// Gets the MRF summary on PageLoad.
        /// </summary>
        /// <param name="objParameter"></param>
        /// <param name="pageCount"></param>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetMrfSummaryForPageLoad(BusinessEntities.ParameterCriteria objParameter, ref int pageCount)
        {

            // Initialise the Data Layer object
            DataAccessLayer.MRF.MRFDetail mrfSummaryDL = new Rave.HR.DataAccessLayer.MRF.MRFDetail();

            // Initialise the Colection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();

            try
            {
                // Call the Data Layer method
                raveHRCollection = mrfSummaryDL.GetMrfSummaryForPageLoad(objParameter, ref pageCount);
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETMRFSUMMARYFORPAGELOAD, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }

            // Return the Collection
            return raveHRCollection;
        }

        /// <summary>
        /// Set the reason for MRF when it's status is changed.
        /// </summary>
        /// <returns>Int</returns>
        public static int SetMRFApproveRejectReason(BusinessEntities.MRFDetail mrfDetail)
        {
            int checkReasonSet;
            try
            {
                Rave.HR.DataAccessLayer.MRF.MRFDetail MRFDL = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
                checkReasonSet = MRFDL.SetMRFApproveRejectReason(mrfDetail);
                ReportingToForEmployeesUpdated(checkReasonSet, mrfDetail);
                return checkReasonSet;


            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, SETMRFAPPROVEREJECTREASON, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }


        //Mahendra ssue Id : 33860 STRAT
        /// <summary>
        /// Set the reason for MRF when it's status is changed.
        /// </summary>
        /// <returns>Int</returns>
        public DataSet checkConcurrencyResourceAllocation(BusinessEntities.MRFDetail mrfDetail)
        {
            try
            {
                Rave.HR.DataAccessLayer.MRF.MRFDetail MRFDL = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
                DataSet dsAllocationForMRF = MRFDL.checkConcurrencyResourceAllocation(mrfDetail);

                return dsAllocationForMRF;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, ConcurrencyResourceAllocation, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        //Mahendra ssue Id : 33860 END


        /// <summary>
        /// Set the reason for MRF when it's status is changed.
        /// </summary>
        /// <returns>Int</returns>
        public static int SetMRFApproveRejectReasonForPM(BusinessEntities.MRFDetail mrfDetail)
        {
            int checkReasonSet;
            try
            {
                checkReasonSet = Rave.HR.DataAccessLayer.MRF.MRFDetail.SetMRFApproveRejectReasonForPM(mrfDetail);
                return checkReasonSet;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, SETMRFAPPROVEREJECTREASONFORPM, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Function will get Project as per Role Wise
        /// </summary>
        /// <param name="mrfDetail"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetProjectNameRoleWiseBL(BusinessEntities.ParameterCriteria parameterCriteria)
        {
            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();
            try
            {
                DataAccessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
                raveHRCollection = mRFDetail.GetProjectNameRoleWiseDL(parameterCriteria);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETPROJECTNAMEROLEWISE, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
            return raveHRCollection;
        }

        /// <summary>
        /// Function will Get REsource Plan as per Project wise
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetResourcePlanProjectWiseBL(int ProjectID)
        {
            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();
            try
            {
                DataAccessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
                raveHRCollection = mRFDetail.GetResourcePlanProjectWiseDL(ProjectID);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETPROJECTNAMEROLEWISE, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
            return raveHRCollection;
        }

        /// <summary>
        /// Function will Fill Role Drop Down as per Selected Resource Plan DropDown
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetRoleResourcePlanWiseBL(int ResourcePlanID, int ResourcePlanDurationStatus)
        {
            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();
            try
            {
                DataAccessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
                raveHRCollection = mRFDetail.GetRoleResourcePlanWiseDL(ResourcePlanID, ResourcePlanDurationStatus);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETPROJECTNAMEROLEWISE, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
            return raveHRCollection;
        }

        /// <summary>
        /// Function will Get Resource Grid as per Selected Role
        /// </summary>
        /// <param name="ResourcePlanID"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetResourceGridRoleWiseBL(int RoleID, int ResourcePlanID)
        {
            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();
            try
            {
                DataAccessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
                raveHRCollection = mRFDetail.GetResourcePlanGridRoleWiseDL(RoleID, ResourcePlanID);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETPROJECTNAMEROLEWISE, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
            return raveHRCollection;
        }

        //<summary>
        //Retrive the MRF details for Approval of MRF by  Head Count 
        //</summary>
        //<returns>Collection</returns>
        public static BusinessEntities.RaveHRCollection GetMRFDetailsForApprocalOfMRFByHeadCount(string EmailId, BusinessEntities.ParameterCriteria objParameterCriteria, ref int pageCount)
        {
            try
            {
                raveHRCollection = DataAccessLayer.MRF.MRFDetail.GetMRFDetailsForApprocalOfMRFByHeadCount(EmailId, objParameterCriteria, ref pageCount);
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETMRFDETAILSFORAPPROVALOFMRFBYHEADCOUNT, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }

        }

        /// <summary>
        /// Get the internal resource to allocate internal resource for MRF.
        /// </summary>
        /// <returns>Collection</returns>
        public static BusinessEntities.RaveHRCollection GetListOfInternalResource(BusinessEntities.MRFDetail mrfDetail, int departmentId, BusinessEntities.ParameterCriteria objParameterCriteria, ref int pageCount)
        {
            try
            {
                raveHRCollection = DataAccessLayer.MRF.MRFDetail.GetListOfInternalResource(mrfDetail, departmentId, objParameterCriteria, ref pageCount);
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETLISTOFINTERNALRESOURCE, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Get the List of MRF whose status is pending Allocation.
        /// </summary>
        /// <returns>Collection</returns>
        public static BusinessEntities.RaveHRCollection GetMRFDetailsForPendingAllocation(BusinessEntities.ParameterCriteria objParameterCriteria, ref int pageCount)
        {
            try
            {
                raveHRCollection = DataAccessLayer.MRF.MRFDetail.GetMRFDetailsForPendingAllocation(objParameterCriteria, ref pageCount);
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETMRFDETAILSFORPEDINGALLOCATION, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Gets the Approve/Reject MRF
        /// </summary>
        /// <param name="objParameter"></param>
        /// <param name="pageCount"></param>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetApproveRejectMrf(BusinessEntities.ParameterCriteria objParameter, ref int pageCount)
        {

            // Initialise the Data Layer object
            DataAccessLayer.MRF.MRFDetail mrfSummaryDL = new Rave.HR.DataAccessLayer.MRF.MRFDetail();

            // Initialise the Colection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();

            try
            {
                // Call the Data Layer method to approve the MRF.
                raveHRCollection = mrfSummaryDL.GetApproveRejectMrf(objParameter, ref pageCount);
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETAPPROVEREJECTMRF, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }

            // Return the Collection
            return raveHRCollection;
        }

        /// <summary>
        /// Function will Get the Project Domain Name
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetProjectDomainBL(int ProjectID)
        {
            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();
            try
            {
                DataAccessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
                raveHRCollection = mRFDetail.GetProjectDomainDL(ProjectID);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETPROJECTNAMEROLEWISE, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
            return raveHRCollection;
        }

        /// <summary>
        /// Function will get employee
        /// </summary>
        /// <param name="RoleID"></param>
        /// <param name="ResourcePlanID"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetEmployeeBL()
        {
            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();
            try
            {
                DataAccessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
                raveHRCollection = mRFDetail.GetMRFEmployeeDL();
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETPROJECTNAMEROLEWISE, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
            return raveHRCollection;
        }

        #region Modified By Mohamed Dangra
        // Mohamed : Issue 50791 : 12/05/2014 : Starts                        			  
        // Desc : IN Mrf Details ,GroupId need to implement
        /// <summary>
        /// Function will Max GroupId of MRF
        /// </summary>
        /// <param name="RoleID"></param>
        /// <param name="ResourcePlanID"></param>
        /// <returns></returns>
        public int GetMRFDetailMaxGroupIdBL()
        {
            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();
            try
            {
                DataAccessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
                return mRFDetail.GetMRFDetailMaxGroupId();
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETPROJECTNAMEROLEWISE, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }
        // Mohamed : Issue 50791 : 12/05/2014 : Ends
        #endregion Modified By Mohamed Dangra
        /// <summary>
        /// Function will use in Raise MRF
        /// </summary>
        /// <param name="MRFDetailobject"></param>
        /// <param name="dtResourceGrid"></param>
        /// <returns></returns>
        public RaveHRCollection RaiseMRFBL(BusinessEntities.MRFDetail MRFDetailobject, DataTable dtResourceGrid)
        {
            string MRFCode;
            string NewMRFCode;
            int checkvalue;
            RaveHRCollection raveHRCollectionNew = new RaveHRCollection();

            DataAccessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
            mrfDetailBL = new MRFDetail();

            MRFDetailobject.GroupId = mRFDetail.GetMRFDetailMaxGroupId() + 1;

            try
            {
                if (MRFDetailobject != null)
                {
                    if (MRFDetailobject.ProjectId != 0)
                    {
                        MRFCode = "MRF_" + MRFDetailobject.ProjectName + "_" + MRFDetailobject.Role + "_";
                        MRFDetailobject.MRFCode = MRFCode;
                        if (dtResourceGrid != null)
                        {
                            for (int i = 0; i < dtResourceGrid.Rows.Count; i++)
                            {
                                KeyValue<string> key = new KeyValue<string>();
                                checkvalue = Convert.ToInt32(dtResourceGrid.Rows[i][CommonConstants.CHECKGRIDVALUE]);
                                if (checkvalue == 1)
                                {
                                    MRFDetailobject.ResourcePlanDurationId = Convert.ToInt32(dtResourceGrid.Rows[i][CommonConstants.RESOURCEPLANDURATIONID]);
                                    MRFDetailobject.ResourceOnBoard = Convert.ToDateTime(dtResourceGrid.Rows[i][CommonConstants.RESOURCEPLANSTARTDATE]);
                                    MRFDetailobject.ResourceReleased = Convert.ToDateTime(dtResourceGrid.Rows[i][CommonConstants.RESOURCEPLANENDDATE]);
                                    MRFDetailobject.Billing = Convert.ToInt32(dtResourceGrid.Rows[i][CommonConstants.RESOURCEPLANBILLING]);
                                    MRFDetailobject.Utilization = Convert.ToInt32(dtResourceGrid.Rows[i][CommonConstants.RESOURCEPLANUTILIZATION]);
                                    MRFDetailobject.BillingDate = Convert.ToDateTime(dtResourceGrid.Rows[i][CommonConstants.RESOURCEBILLINGDATE]);
                                    NewMRFCode = mRFDetail.RaiseMRFDL(MRFDetailobject);
                                    //key.KeyName = NewMRFCode.Split(',')[0].ToString();
                                    key.Val = NewMRFCode.Split(',')[0].ToString();
                                    //MRFDetailobject.MRFId = int.Parse(NewMRFCode.Split(',')[1].ToString());
                                    key.KeyName = NewMRFCode.Split(',')[1].ToString();
                                    // MRFDetailobject.MRFCode = NewMRFCode;
                                    raveHRCollectionNew.Add(key);
                                }
                            }
                        }
                        else
                        {
                            KeyValue<string> key = new KeyValue<string>();
                            NewMRFCode = mRFDetail.RaiseMRFDL(MRFDetailobject);
                            //key.KeyName = NewMRFCode.Split(',')[0].ToString();
                            key.Val = NewMRFCode.Split(',')[0].ToString();
                            //MRFDetailobject.MRFId = int.Parse(NewMRFCode.Split(',')[1].ToString());
                            key.KeyName = NewMRFCode.Split(',')[1].ToString();
                            // MRFDetailobject.MRFCode = NewMRFCode;
                            raveHRCollectionNew.Add(key);
                        }
                    }
                    else
                    {
                        MRFCode = "MRF_" + MRFDetailobject.DepartmentName + "_" + MRFDetailobject.Role + "_";
                        MRFDetailobject.MRFCode = MRFCode;

                        for (int i = 0; i < MRFDetailobject.NoOfResourceRequired; i++)
                        {
                            KeyValue<string> keynew = new KeyValue<string>();
                            KeyValue<string> keyMRfId = new KeyValue<string>();
                            MRFDetailobject.ResourcePlanDurationId = 0;
                            NewMRFCode = mRFDetail.RaiseMRFDL(MRFDetailobject);
                            //keynew.KeyName = NewMRFCode.Split(',')[0].ToString();
                            keynew.Val = NewMRFCode.Split(',')[0].ToString();
                            keynew.KeyName = NewMRFCode.Split(',')[1].ToString();
                            //MRFDetailobject.MRFCode = NewMRFCode;
                            raveHRCollectionNew.Add(keynew);
                        }
                    }

                    if (raveHRCollectionNew.Count != 0)
                    {
                        BusinessEntities.KeyValue<string> obj = null;

                        for (int i = 0; i < raveHRCollectionNew.Count; i++)
                        {

                            obj = (BusinessEntities.KeyValue<string>)raveHRCollectionNew.Item(i);

                            if (MRFDetailobject != null)
                            {
                                mrfDetailBL.SendEMailRaiseMRF(MRFDetailobject, obj.Val, int.Parse(obj.KeyName));
                            }
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
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETPROJECTNAMEROLEWISE, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
            return raveHRCollectionNew;
        }

        /// <summary>
        /// Function will Send mail.
        /// </summary>
        /// <param name="mrfDetailobj"></param>
        /// <returns></returns>
        private void SendMailAbortedMRF(BusinessEntities.MRFDetail mrfDetailobj, string rmsURL)
        {
            IRMSEmail rmsEmail = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),

                                           Convert.ToInt16(EnumsConstants.EmailFunctionality.AbortMRF));

            DataAccessLayer.Contracts.Contract objDataAccessContract = new Rave.HR.DataAccessLayer.Contracts.Contract();

            rmsEmail.From = objAuMan.getLoggedInUserEmailId();

            rmsEmail.To.Add(objDataAccessContract.GetEmailID(mrfDetailobj.RaisedBy));

            string recruitMentManager = objDataAccessContract.GetEmailID(mrfDetailobj.RecruitmentManager);

            if ((recruitMentManager != null) && (recruitMentManager != ""))

                rmsEmail.CC.Add(recruitMentManager);

            //rmsEmail.Subject = string.Format(rmsEmail.Subject, mrfDetailobj.MRFCode, mrfDetailobj.ClientName, mrfDetailobj.ProjectName);
            if (mrfDetailobj.ProjectName.ToString() != "")
            {
                rmsEmail.Subject = string.Format(rmsEmail.Subject, mrfDetailobj.MRFCode, mrfDetailobj.ClientName);
                int len = rmsEmail.Subject.Length;
                rmsEmail.Subject = rmsEmail.Subject.ToString().Substring(0, len - 1) + ", Project Name:" + mrfDetailobj.ProjectName + "]";
            }
            //rmsEmail.Subject = string.Format(rmsEmail.Subject, mrfDetailobj.MRFCode, mrfDetailobj.ClientName, mrfDetailobj.ProjectName);
            else
                rmsEmail.Subject = string.Format(rmsEmail.Subject, mrfDetailobj.MRFCode, mrfDetailobj.ClientName);

            string isDepartment = string.Empty;

            string deptProjName = string.Empty;

            if ((mrfDetailobj.ProjectName == "") && (mrfDetailobj.ProjectName == null))
            {
                isDepartment = "Department";
                deptProjName = mrfDetailobj.DepartmentName;
            }
            else
            {
                isDepartment = "Project";
                deptProjName = mrfDetailobj.ProjectName;
            }

            string role = string.Empty;
            if (mrfDetailobj.Role != null)
            {
                role = "For [" + mrfDetailobj.Role + "]";
            }
            else
            {
                role = "";
            }

            string rmsUrl = Utility.GetUrl() + CommonConstants.Page_MrfView + "?" + rmsURL;

            string loggedInUser = "";
            loggedInUser = objAuMan.getLoggedInUser();
            //GoogleMail
            //if (loggedInUser.ToLower().Contains(DOMAIN))
            //{
            //    loggedInUser = objAuMan.GetDomainUserName(loggedInUser.Replace(DOMAIN, string.Empty));
            //    loggedInUser = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(loggedInUser.Replace(".", " ")); 
            //}
            //else
            //{
            //    loggedInUser = objAuMan.GetDomainUserName(loggedInUser.Replace(AuthorizationManagerConstants.NORTHGATEDOMAIN, string.Empty));
            //    loggedInUser = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(loggedInUser.Replace(".", " "));
            //}


            loggedInUser = objAuMan.GetDomainUserName(loggedInUser);

            rmsEmail.Body = string.Format(rmsEmail.Body, mrfDetailobj.RaisedBy, isDepartment, deptProjName, role, mrfDetailobj.CommentReason, rmsUrl, loggedInUser);

            rmsEmail.SendEmail(rmsEmail);
        }

        /// <summary>
        /// Function will Send mail.
        /// </summary>
        /// <param name="mrfDetailobj"></param>
        /// <returns></returns>
        private void SendMailAbortedMRF(BusinessEntities.MRFDetail mrfDetailobj, string rmsURL, string emailRecruitment)
        {
            IRMSEmail rmsEmail = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),

                                           Convert.ToInt16(EnumsConstants.EmailFunctionality.AbortMRF));


            DataAccessLayer.Contracts.Contract objDataAccessContract = new Rave.HR.DataAccessLayer.Contracts.Contract();

            rmsEmail.From = objAuMan.getLoggedInUserEmailId();

            rmsEmail.To.Add(objDataAccessContract.GetEmailID(mrfDetailobj.RaisedBy));
            //rmsEmail.To.Add(emailRecruitment.ToString());

            string recruitMentManager = objDataAccessContract.GetEmailID(mrfDetailobj.RecruitmentManager);

            if ((recruitMentManager != null) && (recruitMentManager != ""))
            {
                rmsEmail.CC.Add(recruitMentManager);
            }

            rmsEmail.CC.Add(emailRecruitment.ToString());
            //rmsEmail.Subject = string.Format(rmsEmail.Subject, mrfDetailobj.MRFCode, mrfDetailobj.ClientName, mrfDetailobj.ProjectName);
            if (mrfDetailobj.ProjectName.ToString() != "")
            {
                rmsEmail.Subject = string.Format(rmsEmail.Subject, mrfDetailobj.MRFCode, mrfDetailobj.ClientName);
                int len = rmsEmail.Subject.Length;
                rmsEmail.Subject = rmsEmail.Subject.ToString().Substring(0, len - 1) + ", Project Name:" + mrfDetailobj.ProjectName + "]";
            }
            //rmsEmail.Subject = string.Format(rmsEmail.Subject, mrfDetailobj.MRFCode, mrfDetailobj.ClientName, mrfDetailobj.ProjectName);
            else
                rmsEmail.Subject = string.Format(rmsEmail.Subject, mrfDetailobj.MRFCode, mrfDetailobj.ClientName);

            string isDepartment = string.Empty;

            string deptProjName = string.Empty;

            if ((mrfDetailobj.ProjectName == "") && (mrfDetailobj.ProjectName == null))
            {
                isDepartment = "Department";
                deptProjName = mrfDetailobj.DepartmentName;
            }
            else
            {
                isDepartment = "Project";
                deptProjName = mrfDetailobj.ProjectName;
            }

            if (mrfDetailobj.DepartmentName.Contains("RaveConsultant"))
            {
                isDepartment = "Client";
                deptProjName = mrfDetailobj.ClientName;
            }
            string role = string.Empty;
            if (mrfDetailobj.Role != null)
            {
                role = "For [" + mrfDetailobj.Role + "]";
            }
            else
            {
                role = "";
            }

            string rmsUrl = Utility.GetUrl() + CommonConstants.Page_MrfView + "?" + rmsURL;

            //string loggedInUser = objAuMan.GetDomainUserName(objAuMan.getLoggedInUser().Replace(DOMAIN, string.Empty));

            string loggedInUser = "";
            loggedInUser = objAuMan.getLoggedInUser();
            //GoogleMail
            //if (loggedInUser.ToLower().Contains(DOMAIN))
            //{
            //    loggedInUser = objAuMan.GetDomainUserName(loggedInUser.Replace(DOMAIN, string.Empty));
            //    loggedInUser = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(loggedInUser.Replace(".", " ")); 
            //}
            //else
            //{
            //    loggedInUser = objAuMan.GetDomainUserName(loggedInUser.Replace(AuthorizationManagerConstants.NORTHGATEDOMAIN, string.Empty));
            //    loggedInUser = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(loggedInUser.Replace(".", " ")); 
            //}

            loggedInUser = objAuMan.GetDomainUserName(loggedInUser);

            rmsEmail.Body = string.Format(rmsEmail.Body, mrfDetailobj.RaisedBy, isDepartment, deptProjName, role, mrfDetailobj.CommentReason, rmsUrl, loggedInUser);

            rmsEmail.SendEmail(rmsEmail);
        }
        /// <summary>
        /// Sends E-mail for Replace MRF
        /// </summary>
        /// <param name="objMRFDetails"></param>
        public void SendEmailForReplaceMRF(BusinessEntities.MRFDetail objMRFDetails)
        {
            string newMRFCode = string.Empty;
            try
            {
                //Get the loggedin user name
                AuthorizationManager objAuMgr = new AuthorizationManager();
                //string strFromUser = objAuMgr.getLoggedInUserEmailId();
                string strFromUser = objAuMgr.getLoggedInUser();
                string username = "";
                //GoogleMail
                //if (strFromUser.ToLower().Contains(RAVEDOMAIN))
                //{
                //    username = objAuMgr.GetDomainUserName(strFromUser.Replace(RAVEDOMAIN, ""));
                //    username = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(username.Replace(".", " ")); 
                //}
                //else
                //{
                //    username = objAuMgr.GetDomainUserName(strFromUser.Replace(NOTHGATEDOMAIN, ""));
                //    username = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(username.Replace(".", " ")); 
                //}

                username = objAuMan.GetDomainUserName(strFromUser);

                //Get Server URL
                string strPendingAlocationLink = Utility.GetUrl() +
                                               CommonConstants.Page_MrfView +
                                               "?" +
                                            URLHelper.SecureParameters(CommonConstants.CON_QSTRING_AllocateResourec, _true) + "&" +
                                            URLHelper.SecureParameters(CommonConstants.MRF_ID, objMRFDetails.MRFId.ToString()) + "&" +
                                            URLHelper.SecureParameters(_index, _zero) + "&" +
                                            URLHelper.SecureParameters(_pageType, QueryStringConstants.MRFPENDINGALLOCATION) + "&" +
                                            URLHelper.CreateSignature(_true, objMRFDetails.MRFId.ToString(), _zero, QueryStringConstants.MRFPENDINGALLOCATION);



                raveHRCollection = ReplaceMRFBL(objMRFDetails, null);

                if (raveHRCollection.Count != 0)
                {
                    BusinessEntities.KeyValue<string> objKey = null;
                    objKey = (BusinessEntities.KeyValue<string>)raveHRCollection.Item(0);
                    newMRFCode = objKey.Val;


                    IRMSEmail objEmail = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),
                                                      Convert.ToInt16(EnumsConstants.EmailFunctionality.ReplaceMRF));
                    if (objMRFDetails != null)
                    {

                        objEmail.Subject = string.Format(objEmail.Subject,
                                                         newMRFCode,
                                                          ((objMRFDetails.DepartmentName == PROJECT) ? "Project: " + objMRFDetails.ProjectName : "Department: " + objMRFDetails.DepartmentName));

                        objEmail.Body = string.Format(objEmail.Body,
                                                          objMRFDetails.ProjectName,
                                                          objMRFDetails.Role,
                                                          ((objMRFDetails.DepartmentName == PROJECT) ? objMRFDetails.ProjectName : objMRFDetails.DepartmentName),
                                                          strPendingAlocationLink,
                                                          username);
                        message = "MRF for project [" + ((objMRFDetails.DepartmentName == PROJECT) ? objMRFDetails.ProjectName : objMRFDetails.DepartmentName) + "] for [" + objMRFDetails.RoleName + "]" + EMAILMSG;


                    }
                    objEmail.SendEmail(objEmail);

                    HttpContext.Current.Session[SessionNames.CONFIRMATION_MESSAGE] = message;

                }

            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, 
                    GETPROJECTNAMEROLEWISE, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }

        }


        /// <summary>
        /// Function will used in Copy MRF
        /// </summary>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetCopyMRFBL(ParameterCriteria paramCriteria)
        {
            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();
            try
            {
                DataAccessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
                raveHRCollection = mRFDetail.GetCopyMRFDL(paramCriteria);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, 
                    GETPROJECTNAMEROLEWISE, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
            return raveHRCollection;
        }

        /// <summary>
        /// Function will use in Copy MRF Functionality
        /// </summary>
        /// <param name="MRFID"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection CopyMRFBL(int MRFID)
        {
            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();
            try
            {
                DataAccessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
                raveHRCollection = mRFDetail.CopyMRFDL(MRFID);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, 
                    GETPROJECTNAMEROLEWISE, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
            return raveHRCollection;
        }

        //Rakesh : Actual vs Budget 06/06/2016 Begin
        /// <summary>
        /// Check Project is NIS or Northgate
        /// </summary>
        /// <returns>Int</returns>
        public static BusinessEntities.NPS_Validation Is_NIS_NorthgateProject(int ProjectId)
        {            
            Rave.HR.BusinessLayer.MRF.MRFDetail mrfDetailBL = new MRFDetail();
            try
            {               
                return Rave.HR.DataAccessLayer.MRF.MRFDetail.Is_NIS_NorthgateProject(ProjectId);                
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL,
                    SETMRFAPPROVEREJECTREASON, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

      
 //End

        /// <summary>
        /// Set the status of MRF when it will go from one transaction to another.
        /// </summary>
        /// <returns>Int</returns>
        public static int SetMRFStatus(BusinessEntities.MRFDetail mrfDetail)
        {
            int checkReasonSet;
            Rave.HR.BusinessLayer.MRF.MRFDetail mrfDetailBL = new MRFDetail();

            try
            {
                checkReasonSet = Rave.HR.DataAccessLayer.MRF.MRFDetail.SetMRFStatus(mrfDetail);

                return checkReasonSet;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, 
                    SETMRFAPPROVEREJECTREASON, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Gets the Recruitment Manager names.
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetRecruitmentManager()
        {
            // Initialise the Data Layer object
            DataAccessLayer.MRF.MRFDetail employeeNameDL = new Rave.HR.DataAccessLayer.MRF.MRFDetail();

            // Initialise the Collection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();

            try
            {
                // Call the Data Layer Method
                raveHRCollection = employeeNameDL.GetRecruitmentManager();

                // Return the Collection
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETRECRUITMENTMANAGER, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// /// Function will Edit MRF
        /// </summary>
        /// <param name="MRFDetailobject"></param>
        /// <returns></returns>
        public int EditMRFBL(BusinessEntities.MRFDetail MRFDetailobject, Boolean IsRecruiterReassigned, Boolean IsMrfStatusChanged)
        {
            DataAccessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();

            try
            {
                if (MRFDetailobject != null)
                {
                  // 27642-Ambar-Start
                  // If status of MRF is rejected then make it pending allocation.
                  bool Is_Rejected_Status = false;
                  if (MRFDetailobject.StatusId == (int)MasterEnum.MRFStatus.Rejected)
                  {
                    
                    MRFDetailobject.StatusId = (int)MasterEnum.MRFStatus.PendingAllocation;
                    IsMrfStatusChanged = true;
                    Is_Rejected_Status = true;
                  }
                  // 27642-Ambar-End                    

      
                    MRFID = mRFDetail.EditMRFDL(MRFDetailobject);

                    if (IsRecruiterReassigned)
                        SendEMailRaiseHeadCountWithOutApproval(MRFDetailobject, IsRecruiterReassigned);

                    if (IsMrfStatusChanged)
                    {
                        if (MRFDetailobject.StatusId == (int)MasterEnum.MRFStatus.MarketResearchCompleteAndClosed)
                        {
                            SendEMailMRFStatusChangedToClosed(MRFDetailobject);
                        }
                        // 27642-Ambar-Start
                        // If status of MRF is rejected then send rejection mail.
                        else if (Is_Rejected_Status)
                        {
                            SendEMailRejectedMRFStatusChanged(MRFDetailobject);
                        }
                        // 27642-Ambar-End
                        else
                        {
                            SendEMailMRFStatusChanged(MRFDetailobject);
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
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, EDITMRFBL, GETPROJECTNAMEROLEWISE, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
            return MRFID;
        }

        /// <summary>
        /// /// Function will Move MRF
        /// </summary>
        /// <param name="MRFDetailobject"></param>
        /// <returns></returns>
        public string MoveMRFBL(BusinessEntities.MRFDetail MRFDetailobject, BusinessEntities.MRFDetail MRFDetailobjForDeleteOldMRF)
        {
            DataAccessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
            string NewMRFCode = string.Empty;
            int n = 6;
            string[] mailDetail = new string[n];
            try
            {
                if (MRFDetailobject != null)
                {
                    mailDetail = mRFDetail.MoveMRFDL(MRFDetailobject);
                    NewMRFCode = mailDetail[0];
                    mRFDetail.DeleteMRFDL(MRFDetailobjForDeleteOldMRF);
                    if (NewMRFCode != string.Empty)
                    {
                        SendEMailMoveMRF(MRFDetailobject, NewMRFCode, mailDetail);
                    }
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, "MoveMRFBL", GETPROJECTNAMEROLEWISE, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
            return NewMRFCode;
        }



        /// <summary>
        /// Function will Get Role as per Selected Department.
        /// </summary>
        /// <param name="DepartmentId"></param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetRoleDepartmentWiseBL(int DepartmentId)
        {
            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();
            try
            {
                DataAccessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
                raveHRCollection = mRFDetail.GetRoleDepartmentWiseDL(DepartmentId);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETROLEDEPARTMENTWISEBL, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
            return raveHRCollection;
        }

        /// <summary>
        /// Function will Abort.
        /// </summary>
        /// <param name="mrfDetailobj"></param>
        /// <returns></returns>
        public int AbortMRFBL(BusinessEntities.MRFDetail mrfDetailobj, string rmsURL, Boolean isResourseRelease_E_MRF)
        {
            int intID;
            int mrfDid;
            string emaildRecruitment;

            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();
            try
            {
                DataAccessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
                emaildRecruitment = mRFDetail.GetEmployeeExistCheck();

                intID = mRFDetail.AbortMRFDL(mrfDetailobj);
                mrfDid = mrfDetailobj.MRFId;
                mrfDetailobj = null;
                if (intID == 0)
                {
                    mrfDetailobj = mRFDetail.GetMRFDetails(mrfDid);

                    if (isResourseRelease_E_MRF)
                    {
                        SendMailAbortedMRF(mrfDetailobj, rmsURL, emaildRecruitment);
                    }
                    else
                    {
                        SendMailAbortedMRF(mrfDetailobj, rmsURL);
                    }
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, ABORTMRFBL, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
            return intID;
        }

        public BusinessEntities.MRFDetail GetMailingDetails(BusinessEntities.MRFDetail mrfDetail)
        {
            DataAccessLayer.MRF.MRFDetail mrfMailingDL = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
            BusinessEntities.MRFDetail mailingDetails = new BusinessEntities.MRFDetail();

            try
            {
                mailingDetails = mrfMailingDL.GetMailingDetails(mrfDetail);
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, ABORTMRFBL, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }

            return mailingDetails;
        }

       //Siddharth 02-March-2015 Start
        public BusinessEntities.MRFDetail GetMailingDetailsOfFunctionalManagerAndReportingTo(BusinessEntities.MRFDetail mrfDetail)
        {
            DataAccessLayer.MRF.MRFDetail mrfMailingDL = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
            BusinessEntities.MRFDetail mailingDetails = new BusinessEntities.MRFDetail();

            try
            {
                mailingDetails = mrfMailingDL.GetMailingDetailsOfFunctionalManagerAndReportingTo(mrfDetail);
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, ABORTMRFBL, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }

            return mailingDetails;
        }
             //Siddharth 02-March-2015 End

        /// <summary>
        /// This Function gets the logged in user name i.e "gaurav.thakkar@rave-tech.com" 
        /// and converts it to "Gaurav Thakkar" which is to be appended in mail
        /// after "Regards".
        /// </summary>
        public string GetLoggedInUserName()
        {
            try
            {
                //Splits the name at separator.. For eg : If name is gaurav.thakkar than separator is "."
                char separator = Convert.ToChar(System.Configuration.ConfigurationSettings.AppSettings["SplitCharacter"]);
                //Gets the loggen i users email id
                LoggedInUserName = objAuMan.getLoggedInUserEmailId();
                //Splits the emailid at @
                UserName = LoggedInUserName.Split('@');

                if (UserName[0].Contains(separator.ToString()))
                {
                    UserName = UserName[0].Split(separator);
                    for (int i = 0; i < UserName.Length; i++)
                    {
                        UserName[i] = ConvertToUpper(UserName[i]);
                        RegardsName += UserName[i];

                        if (i < UserName.Length - 1)
                            RegardsName += " ";
                    }
                }
                else
                {
                    RegardsName = ConvertToUpper(UserName[0]);
                }

                return RegardsName;

            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, "GetLoggedInUserName", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Converts the 1st alphabet of input string to upper case
        /// </summary>
        /// <param name="InputString"></param>
        /// <returns>string</returns>
        public string ConvertToUpper(string InputString)
        {
            InputString = InputString.Substring(0, 1).ToUpper() + InputString.Substring(1, InputString.Length - 1);
            return InputString;
        }

        /// <summary>
        /// Get the Split Duration of Resource plan in Onsite/Offshore.
        /// </summary>
        /// <returns>Collection</returns>
        public static BusinessEntities.RaveHRCollection GetRPSplitDurationInOnsite_Offshore(int RPDuId)
        {
            try
            {
                raveHRCollection = DataAccessLayer.MRF.MRFDetail.GetRPSplitDurationInOnsite_Offshore(RPDuId);
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETLISTOFINTERNALRESOURCE, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }


        /// <summary>
        /// Get Domain Name as per Selected Project
        /// </summary>
        /// <param name="ProjectID"></param>
        /// <returns></returns>
        public string GetDomainName(int ProjectID)
        {
            StringBuilder sb = new StringBuilder(); ;
            BusinessEntities.RaveHRCollection raveHRCollection;
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                raveHRCollection = GetProjectDomainBL(ProjectID);

                BusinessEntities.KeyValue<string> obj = null;

                for (int i = 0; i < raveHRCollection.Count; i++)
                {
                    obj = (BusinessEntities.KeyValue<string>)raveHRCollection.Item(i);
                    sb.Append(obj.Val);
                    sb.Append(",");
                }

                sb.Remove(sb.Length - 1, 1);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, "GetDomain", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Get the internal resource to allocate internal resource for MRF.
        /// </summary>
        /// <returns>Collection</returns>
        public static BusinessEntities.RaveHRCollection GetMRFInternalResource(BusinessEntities.MRFDetail mrfDetail, string ResourceName, BusinessEntities.ParameterCriteria objParameterCriteria, ref int pageCount)
        {
            try
            {
                raveHRCollection = DataAccessLayer.MRF.MRFDetail.GetMRFInternalResource(mrfDetail, ResourceName, objParameterCriteria, ref pageCount);
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETLISTOFINTERNALRESOURCE, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Get Project Manager by ProjectID.
        /// </summary>
        public RaveHRCollection GetProjectManagerByProjectId(BusinessEntities.MRFDetail objBEDetails)
        {
            Rave.HR.DataAccessLayer.MRF.MRFDetail mrf = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
            return mrf.GetProjectManagerByProjectId(objBEDetails);
        }

        /// <summary>
        ///Added by kanchan
        ///Description:Changes done for (whizible)CR:16622
        /// </summary>
        /// <param name="Appoval"></param>
        /// <param name="mrf"></param>
        /// <returns></returns>
        public static bool ReportingToForEmployeesUpdated(int Appoval, BusinessEntities.MRFDetail mrf)
        {
            // Initialise the Data Layer object
            Rave.HR.DataAccessLayer.MRF.MRFDetail MRFDL = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
            try
            {
                DataSet dsFromEmployee = MRFDL.GetReportingTOFromEmployee(mrf);
                DataSet dsFromMRF = MRFDL.GetReportingTOFromMRF(mrf);

                // Venkatesh : Issue48132 : 03/Jan/2014 : Starts                    
                // Desc : Check New Emp Allocation
                bool  IsNewEmployee = MRFDL.CheckNewEmployee_Allocation(mrf);
                string finalReportingTos = string.Empty;
                if (IsNewEmployee == false)
                {
                    #region Old Employee allocation
                    string reportingToIDs = string.Empty;
                    string[] EmpReportingTo;
                    foreach (DataRow row in dsFromEmployee.Tables[0].Rows)
                    {
                        reportingToIDs = row[DbTableColumn.ReportingToId].ToString();
                    }
                    reportingToIDs += ',';
                    if (reportingToIDs.Contains(','))
                    {
                        EmpReportingTo = reportingToIDs.Split(',');
                    }
                    else
                    {
                        EmpReportingTo = (reportingToIDs.Split(','));
                    }

                    foreach (string Emprow in EmpReportingTo)
                    {
                        foreach (DataRow row in dsFromMRF.Tables[0].Rows)
                        {
                            if (Emprow.ToString() != string.Empty)
                            {
                                if (!row[DbTableColumn.ReportingToId].ToString().Contains(','))
                                {
                                    if (string.IsNullOrEmpty(row[DbTableColumn.ReportingToId].ToString()))
                                    {
                                        row[DbTableColumn.ReportingToId] = 0;
                                    }
                                    if (Convert.ToInt32(row[DbTableColumn.ReportingToId]) != Convert.ToInt32(Emprow))
                                    {
                                        if (reportingToIDs.LastIndexOf(',') == (reportingToIDs.Length - 1))
                                        {
                                            reportingToIDs += row[DbTableColumn.ReportingToId].ToString() + ',';
                                        }
                                        else
                                        {
                                            reportingToIDs += ',' + row[DbTableColumn.ReportingToId].ToString() + ',';
                                        }
                                    }
                                }
                                else
                                {
                                    string[] ReportingToMRF = row[DbTableColumn.ReportingToId].ToString().Split(',');

                                    foreach (string mrfId in ReportingToMRF)
                                    {
                                        if (Convert.ToInt32(Emprow.ToString()) != Convert.ToInt32(mrfId))
                                        {
                                            // reportingToIDs += row[DbTableColumn.ReportingToId].ToString() + ',';
                                            reportingToIDs += mrfId.ToString() + ',';
                                        }
                                    }
                                }
                            }
                        }
                    }

                    string repoting = string.Empty;
                    if (reportingToIDs.EndsWith(","))
                    {
                        repoting = (reportingToIDs.Remove(reportingToIDs.Length - 1)).ToString();
                    }
                    else
                    {
                        repoting = reportingToIDs;
                    }
                    string[] allReportingTo = repoting.Split(',');
                    string[] DistinctReportingTo = allReportingTo.Distinct().ToArray();

                    foreach (string emp in DistinctReportingTo)
                    {
                        finalReportingTos += emp + ',';
                    }
                    finalReportingTos = finalReportingTos.Remove(finalReportingTos.Length - 1);
                    #endregion
                }
                else
                {
                    #region New Employee Allocation
                    string reportingToIDs = string.Empty;
                    string[] EmpReportingTo;

                    foreach (DataRow row in dsFromMRF.Tables[0].Rows)
                    {
                        if (!row[DbTableColumn.ReportingToId].ToString().Contains(','))
                        {
                            if (string.IsNullOrEmpty(row[DbTableColumn.ReportingToId].ToString()))
                            {
                                row[DbTableColumn.ReportingToId] = 0;
                            }
                            if (reportingToIDs.LastIndexOf(',') == (reportingToIDs.Length - 1))
                            {
                                reportingToIDs += row[DbTableColumn.ReportingToId].ToString() + ',';
                            }
                            else
                            {
                                reportingToIDs += ',' + row[DbTableColumn.ReportingToId].ToString() + ',';
                            }
                        }
                        else
                        {
                            string[] ReportingToMRF = row[DbTableColumn.ReportingToId].ToString().Split(',');

                            foreach (string mrfId in ReportingToMRF)
                            {
                                reportingToIDs += mrfId.ToString() + ',';
                            }
                        }
                    }
                    string repoting = string.Empty;
                    if (reportingToIDs.EndsWith(","))
                    {
                        repoting = (reportingToIDs.Remove(reportingToIDs.Length - 1)).ToString();
                    }
                    else
                    {
                        repoting = reportingToIDs;
                    }
                    string[] allReportingTo = repoting.Split(',');
                    string[] DistinctReportingTo = allReportingTo.Distinct().ToArray();

                    foreach (string emp in DistinctReportingTo)
                    {
                        finalReportingTos += emp + ',';
                    }
                    finalReportingTos = finalReportingTos.Remove(finalReportingTos.Length - 1);
                    #endregion
                }
                // Venkatesh : Issue48132 : 03/Jan/2014 : End 

                if (MRFDL.UpdateReportingTOForEmployee(mrf, finalReportingTos))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, "ReportingToForEmployeesUpdated", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Gets the reporting TO from employee.
        /// </summary>
        /// <param name="mrfDetail">The MRF detail.</param>
        /// <returns></returns>
        public DataSet GetReportingTOFromEmployee(BusinessEntities.MRFDetail mrfDetail)
        {
            Rave.HR.DataAccessLayer.MRF.MRFDetail MRFDL = new Rave.HR.DataAccessLayer.MRF.MRFDetail();

            try
            {
                return MRFDL.GetReportingTOFromEmployee(mrfDetail);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, "GetReportingTOFromEmployee", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }


        /// <summary>
        /// Updates the reporting TO for employee.
        /// </summary>
        /// <param name="mrfDetail">The MRF detail.</param>
        /// <param name="finalReportingTos">The final reporting tos.</param>
        public void UpdateReportingTOForEmployee(BusinessEntities.MRFDetail mrfDetail, string finalReportingTos)
        {
            Rave.HR.DataAccessLayer.MRF.MRFDetail MRFDL = new Rave.HR.DataAccessLayer.MRF.MRFDetail();

            try
            {
                MRFDL.UpdateReportingTOForEmployee(mrfDetail, finalReportingTos);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, "UpdateReportingTOForEmployee", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Get email Id and other details of approver of department wise headcount.
        /// </summary>
        /// <returns></returns>
        public List<BusinessEntities.MRFDetail> GetEmailIdForHeadCountApproval()
        {
            Rave.HR.DataAccessLayer.MRF.MRFDetail MRFDetailsDL = new Rave.HR.DataAccessLayer.MRF.MRFDetail();

            try
            {

                return MRFDetailsDL.GetEmailIdForHeadCountApproval();
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, "GetEmailIdForHeadCountApproval", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Gets MRF raise access list of departments for user
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetMRFRaiseAccesForDepartmentByEmpId(BusinessEntities.MRFDetail objBEMRFDetail)
        {
            Rave.HR.DataAccessLayer.MRF.MRFDetail objDLLMRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
            return objDLLMRFDetail.GetMRFRaiseAccesForDepartmentByEmpId(objBEMRFDetail);
        }


        /// <summary>
        /// Function will used in Copy MRF Dropdown FIll
        /// </summary>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetCopyMRFList(ParameterCriteria paramCreatiria)
        {
            Rave.HR.DataAccessLayer.MRF.MRFDetail objDLLMRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
            return objDLLMRFDetail.GetCopyMRFList(paramCreatiria);
        }

        /// <summary>
        /// Set the MRF Status and Reason for all when it's status is changed.
        /// </summary>
        /// <returns>Int</returns>
        public int SetMRFSatusAfterApproval(BusinessEntities.MRFDetail mrfDetail, ref string ConfirmMasg)
        {
            int UpdatedStatus;
            try
            {
                Rave.HR.DataAccessLayer.MRF.MRFDetail objDLLMRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();

                UpdatedStatus = objDLLMRFDetail.SetMRFSatusAfterApproval(mrfDetail);

                if (UpdatedStatus == (int)(MasterEnum.MRFStatus.PendingHeadCountApprovalOfCOO))
                {

                    SendEMailRaiseHeadCountApprovalToCOO(mrfDetail);

                    ConfirmMasg = CommonConstants.MSG_MRF_APPROVAL_OF_HEADCOUNTTOCOO;
                }
                else if (UpdatedStatus == (int)(MasterEnum.MRFStatus.PendingExternalAllocation))
                {
                    SendEMailRaiseHeadCountWithOutApproval(mrfDetail);

                    ConfirmMasg = CommonConstants.MSG_MRF_APPROVAL_OF_HEADCOUNT;
                }
                else if (UpdatedStatus == (int)(MasterEnum.MRFStatus.Rejected))
                {
                    SendEMailHeadCountRequestRejected(mrfDetail);
                }
                return UpdatedStatus;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, SETMRFSATUSAFTERAPPROVAL, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Method to get SLA days for the recuriter by employee designation
        /// </summary>
        public BusinessEntities.RaveHRCollection GetSLADaysByMrfId(BusinessEntities.MRFDetail mrfDetail)
        {
            Rave.HR.DataAccessLayer.MRF.MRFDetail objDLLMRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();

            try
            {
                return objDLLMRFDetail.GetSLADaysByMrfId(mrfDetail);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, "GetSLADaysByMrfId", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Sends the E mail raise MRF.
        /// </summary>
        /// <param name="mrfDetail">The MRF detail.</param>
        /// <param name="MRFCode">The MRF code.</param>
        private void SendEMailRaiseMRF(BusinessEntities.MRFDetail mrfDetail, string MRFCode, int MrfId)
        {
            string message = string.Empty;
            //31585-Subhra-Start added the string
            string departmentHeadEmail = string.Empty;
            //31585- end
            AuthorizationManager objAuMan = new AuthorizationManager();

            string mailFrom = objAuMan.getLoggedInUserEmailId();

            //31585-Subhra-start-To keep PM's name in cc when MRF is raised by members other than PMs.
            // Initialise the Collection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();
            raveHRCollection = GetProjectManagerByProjectId(mrfDetail);
            //Initialise the Collection class object for department heads 
            BusinessEntities.RaveHRCollection raveHRCollectionDepartment = new RaveHRCollection();
            BusinessEntities.Recruitment objRecruitment = new BusinessEntities.Recruitment();
            objRecruitment.DepartmentId = mrfDetail.DepartmentId;
            //Gets department heads collection
            raveHRCollectionDepartment = DataAccessLayer.Recruitment.Recruitment.GetDepartmentHeadByDepartmentId(objRecruitment);
            //31585-Subhra-end

            // Venkatesh : 47714  : 06/Jan/2014 : Starts                    
            // Desc : Add Accoutable Person in MailTO 
            string AccountableEmailTo = "";
            if (mrfDetail.ReportingToId != null)
            {
                 AccountableEmailTo = new Rave.HR.BusinessLayer.Contracts.Contract().GetEmailID(Convert.ToInt32(mrfDetail.ReportingToId));
            }
            // Venkatesh : 47714  : 06/Jan/2014 : End

            if (mrfDetail.ProjectId != 0)
            {
                message = "MRF for project [" + mrfDetail.ProjectName + "] for [" + mrfDetail.RoleName + "] position has been raised successfully.An email notification is sent for allocation.";

                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),

                                             Convert.ToInt16(EnumsConstants.EmailFunctionality.RaiseMRFForProjects));
                //31585-Subhra-start-To keep PM's name in cc when MRF is raised by members other than PMs  
                //if (raveHRCollection.Count > 0)
                //{
                //    obj.CC.Add(GetEmailIdByEmpId(((BusinessEntities.MRFDetail)raveHRCollection.Item(0)).EmployeeId));
                //}

                // Venkatesh : 48132  : 06/Jan/2014 : Starts                    
                // Desc : Add Accoutable Person in MailTO 
                if (AccountableEmailTo != null || AccountableEmailTo != "")
                    obj.CC.Add(AccountableEmailTo);
                // Venkatesh : 48132  : 06/Jan/2014 : End

                if (raveHRCollection.Count > 0)
                {
                    //Get ProjectManager Name 
                    string projectManagerEmail = string.Empty;
                    foreach (BusinessEntities.MRFDetail objrer in raveHRCollection)
                    {
                        projectManagerEmail += objrer.EmailId;
                        objRecruitment.EmailId = projectManagerEmail;

                    }
                    if (objRecruitment.EmailId.EndsWith(","))
                    {
                        objRecruitment.EmailId = objRecruitment.EmailId.Substring(0, objRecruitment.EmailId.Length - 1);
                    }

                    obj.CC.Add(objRecruitment.EmailId);
                }

                //31585-Subhra-end

                obj.From = mailFrom;

                obj.Subject = string.Format(obj.Subject, MRFCode, mrfDetail.ClientName, mrfDetail.ProjectName);

                string mailAddressFrom = mailFrom;
                //GoogleConfugrable
                mailAddressFrom = objAuMan.GetUsernameBasedOnEmail(mailAddressFrom);
                mailAddressFrom = objAuMan.GetDomainUserName(mailAddressFrom);
                //if (mailAddressFrom.ToLower().Contains(RAVEDOMAIN))
                //{
                //    mailAddressFrom = mailFrom.Replace(RAVEDOMAIN, "");
                //    mailAddressFrom = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mailAddressFrom.Replace(".", " "));
                //}
                //else
                //{
                //    mailAddressFrom = mailFrom.Replace(NOTHGATEDOMAIN, "");
                //    mailAddressFrom = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mailAddressFrom.Replace(".", " "));
                //}


                obj.Body = string.Format(obj.Body, mrfDetail.ProjectName,
                                                   mrfDetail.RoleName,
                                                   mrfDetail.DepartmentName,
                                                   GetEmailLink(CommonConstants.Page_MrfView, mrfDetail, Convert.ToInt16(EnumsConstants.EmailFunctionality.RaiseMRFForProjects), MrfId),
                                                   //objAuMan.GetDomainUserName(mailFrom.Replace(RAVEDOMAIN, "")));
                                                   //objAuMan.GetDomainUserName(mailAddressFrom));
                                                   mailAddressFrom);

                obj.SendEmail(obj);
            }
            else
            {
                message = "MRF for department [" + mrfDetail.DepartmentName + "] for [" + mrfDetail.RoleName + "] position has been raised successfully.An email notification is sent for allocation.";

                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),

                                             Convert.ToInt16(EnumsConstants.EmailFunctionality.RaiseMRFForDepartments));

                // Venkatesh : 48132  : 06/Jan/2014 : Starts                    
                // Desc : Add Accoutable Person in MailTO 
                if (AccountableEmailTo != null || AccountableEmailTo != "")
                    obj.CC.Add(AccountableEmailTo);
                // Venkatesh : 48132  : 06/Jan/2014 : End

                //31585-Subhra-Start
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

                obj.CC.Add(objRecruitment.EmailId);
                //31585-Subhra-end

                obj.From = mailFrom;

                if (!string.IsNullOrEmpty(mrfDetail.ClientName))
                {
                    obj.Subject = string.Format(obj.Subject, MRFCode,
                                                          mrfDetail.ClientName, mrfDetail.DepartmentName);
                }
                else
                {
                    obj.Subject = string.Format(obj.Subject.Replace(",Client Name:", string.Empty), MRFCode, string.Empty,
                                                          mrfDetail.DepartmentName);
                }

                string mailAddressFrom = mailFrom;
                //GoogleMail
                //GoogleConfugrable
                mailAddressFrom = objAuMan.GetUsernameBasedOnEmail(mailAddressFrom);
                mailAddressFrom = objAuMan.GetDomainUserName(mailAddressFrom);
                //if (mailAddressFrom.ToLower().Contains(RAVEDOMAIN))
                //{
                //    mailAddressFrom = mailFrom.Replace(RAVEDOMAIN, "");
                //    mailAddressFrom = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mailAddressFrom.Replace(".", " "));
                //}
                //else
                //{
                //    mailAddressFrom = mailFrom.Replace(NOTHGATEDOMAIN, "");
                //    mailAddressFrom = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mailAddressFrom.Replace(".", " "));
                //}


                obj.Body = string.Format(obj.Body, mrfDetail.DepartmentName,
                                                   mrfDetail.RoleName,
                                                   GetEmailLink(CommonConstants.Page_MrfView, mrfDetail, Convert.ToInt16(EnumsConstants.EmailFunctionality.RaiseMRFForDepartments), MrfId),
                                                    //objAuMan.GetDomainUserName(mailFrom.Replace(RAVEDOMAIN, "")));
                                                    //objAuMan.GetDomainUserName(mailAddressFrom));
                                                   mailAddressFrom);


                obj.SendEmail(obj);
            }

            HttpContext.Current.Session[SessionNames.CONFIRMATION_MESSAGE] = message;

        }

        /// <summary>
        /// Function will GetLink
        /// </summary>
        /// <returns></returns>
        private string GetLink(string PageName)
        {
            try
            {
                string sComp = Utility.GetUrl() + PageName;

                return sComp;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, "GetLink", "FillDropDown", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Sends the E mail raise head count.
        /// </summary>
        /// <param name="mrfDetail">The MRF detail.</param>
        /// <param name="mailTo">The mail to.</param>
        private void SendEMailRaiseHeadCount(BusinessEntities.MRFDetail mrfDetail)
        {
            AuthorizationManager objAuMan = new AuthorizationManager();
            Utility objUtility = new Utility();

            IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),

                                             Convert.ToInt16(EnumsConstants.EmailFunctionality.RaiseHeadCount));

            if (!string.IsNullOrEmpty(mrfDetail.ProjectName))
            {
                obj.Subject = string.Format(obj.Subject, mrfDetail.MRFCode,
                                                    mrfDetail.ProjectName);
                
               
                obj.Body = string.Format(obj.Body, objUtility.GetEmployeeFirstName(obj.To[0].ToString()),
                                               mrfDetail.Role,
                                               mrfDetail.ProjectName,
                                               GetLink(CommonConstants.Page_MrfApproveRejectHeadCount),mrfDetail.MRFPurpose,  mrfDetail.MRFPurposeDescription.FormattedPurpose((mrfDetail.MRFPurposeId)));
                // 57877-Venkatesh-  29042016 : Start 
                // Add sai email if while raising headcount for nis projects
                Rave.HR.BusinessLayer.Employee.Employee mrfClientName = new Rave.HR.BusinessLayer.Employee.Employee();
                raveHRCollection = mrfClientName.GetClientNameFromProjectID(mrfDetail.ProjectId);

                string ClientName = string.Empty;
                if (raveHRCollection != null)
                {
                    if (raveHRCollection.Count > 0)
                    {
                        ClientName = ((BusinessEntities.KeyValue<string>)(raveHRCollection.Item(0))).KeyName;
                        if (ClientName.ToUpper().Contains("NPS") || ClientName.ToUpper().Contains("NORTHGATE"))
                        {
                            //obj.CC.Add(CommonConstants.SAIGIDDUNISandNAVYANISEmailId);
                            obj.CC.Add(CommonConstants.SAIGIDDUNISEmailId);
                        }
                    }
                }
                // 57877-Venkatesh-  29042016 : End
            }
            else
            {
                obj.Subject = string.Format(obj.Subject.Replace("Project", "Department"), mrfDetail.MRFCode,
                                                    mrfDetail.DepartmentName);

                obj.Body = string.Format(obj.Body, objUtility.GetEmployeeFirstName(obj.To[0].ToString()),
                                               mrfDetail.Role,
                                               mrfDetail.DepartmentName,
                                               GetLink(CommonConstants.Page_MrfApproveRejectHeadCount),
                                               mrfDetail.MRFPurpose,mrfDetail.MRFPurposeDescription.FormattedPurpose((mrfDetail.MRFPurposeId)));
            }            

            //Siddharth 3rd Dec 2015 Start
            //Approval required mail should not go to Sai or Navya or PM; only to Kedar, MVV, RMGroup & rmsadmin

            //// Venkatesh : 47714  : 22/Jan/2014 : Starts                    
            //// Desc : Add Accoutable Person in MailTO 
            //string AccountableEmailTo = "";

            //BusinessEntities.MRFDetail MRFDetailobject = new BusinessEntities.MRFDetail();
            //Rave.HR.BusinessLayer.MRF.MRFDetail mrfDetailsBll = new Rave.HR.BusinessLayer.MRF.MRFDetail();
            //MRFDetailobject = mrfDetailsBll.GetMrfMoveDetail(Convert.ToInt32(mrfDetail.MRFId));
            //if (MRFDetailobject.ReportingToId != null)
            //{
            //    AccountableEmailTo = new Rave.HR.BusinessLayer.Contracts.Contract().GetEmailID(Convert.ToInt32(MRFDetailobject.ReportingToId));
            //    obj.CC.Add(AccountableEmailTo);
            //}
            //// Venkatesh : 47714  : 22/Jan/2014 : End
            //Siddharth 3rd Dec 2015 End

            string purposeSelected = mrfDetail.MRFPurpose;

            obj.SendEmail(obj);
        }

        /// <summary>
        /// Sends the E mail raise head count with out approval.
        /// </summary>
        /// <param name="mrfDetail">The MRF detail.</param>
        private void SendEMailRaiseHeadCountWithOutApproval(BusinessEntities.MRFDetail mrfDetail)
        {
            AuthorizationManager objAuMan = new AuthorizationManager();
            Utility objUtility = new Utility();
            string mailFrom = objAuMan.getLoggedInUserEmailId();
            int RecruiterId = Convert.ToInt32(mrfDetail.RecruitersId);

            IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),

                                        Convert.ToInt16(EnumsConstants.EmailFunctionality.RaiseHeadCountWithOutApproval));


            if (mrfDetail.DepartmentName.Contains("RaveConsultant"))
            {
                obj.To.Insert(0, GetEmailIdByEmpId(RecruiterId));
            }
            else
            {
                obj.To.Insert(0, mrfDetail.EmailId);
            }

            // Venkatesh : 47714  : 22/Jan/2014 : Starts                    
            // Desc : Add Accoutable Person in MailTO 
            string AccountableEmailTo = "";

            BusinessEntities.MRFDetail MRFDetailobject = new BusinessEntities.MRFDetail();
            Rave.HR.BusinessLayer.MRF.MRFDetail mrfDetailsBll = new Rave.HR.BusinessLayer.MRF.MRFDetail();
            MRFDetailobject = mrfDetailsBll.GetMrfMoveDetail(Convert.ToInt32(mrfDetail.MRFId));
            if (MRFDetailobject.ReportingToId != null)
            {
                AccountableEmailTo = new Rave.HR.BusinessLayer.Contracts.Contract().GetEmailID(Convert.ToInt32(MRFDetailobject.ReportingToId));
                obj.CC.Add(AccountableEmailTo);
            }
            // Venkatesh : 47714  : 22/Jan/2014 : End


            obj.Subject = string.Format(obj.Subject, mrfDetail.MRFCode);
            //Mahendra ternuary operator added obj.To[0] check null or not Error getting while fixing different whizible issue. 
            obj.Body = string.Format(obj.Body, obj.To[0] == null ? "" : objUtility.GetEmployeeFirstName(obj.To[0].ToString()),
                                               mrfDetail.Role,
                                               mrfDetail.DepartmentName,
                                               this.GetExpectedClosureDate(mrfDetail).ToShortDateString(),
                                               GetEmailLink(CommonConstants.Page_MrfView, mrfDetail, Convert.ToInt16(EnumsConstants.EmailFunctionality.RaiseHeadCountWithOutApproval), mrfDetail.MRFId));

            obj.SendEmail(obj);
        }

        /// <summary>
        /// Sends the E mail raise head count with out approval to Recruitment Team.
        /// </summary>
        /// <param name="mrfDetail">The MRF detail.</param>
        private void SendEMailRaiseHeadCountToRecruitmentWithOutApproval(BusinessEntities.MRFDetail mrfDetail)
        {
            AuthorizationManager objAuMan = new AuthorizationManager();
            Utility objUtility = new Utility();
            string mailFrom = objAuMan.getLoggedInUserEmailId();
            int RecruiterId = Convert.ToInt32(mrfDetail.RecruitersId);

            IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),

                                        Convert.ToInt16(EnumsConstants.EmailFunctionality.RaiseHeadCountToRecruitmentTeam));
            try
            {
                if (mrfDetail.DepartmentName.Contains("RaveConsultant"))
                {
                    obj.To.Insert(0, GetEmailIdByEmpId(RecruiterId));
                }
                else
                {
                    obj.To.Insert(0, GetEmailIdByEmpId(RecruiterId));
                }

                string mailAddressFrom = mailFrom;
                //GoogleMail
                //GoogleConfugrable
                mailAddressFrom = objAuMan.GetUsernameBasedOnEmail(mailAddressFrom);
                mailAddressFrom = objAuMan.GetDomainUserName(mailAddressFrom);
                //if (mailAddressFrom.ToLower().Contains(RAVEDOMAIN))
                //{
                //    mailAddressFrom = mailFrom.Replace(RAVEDOMAIN, "");
                //    mailAddressFrom = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mailAddressFrom.Replace(".", " "));
                //}
                //else
                //{
                //    mailAddressFrom = mailFrom.Replace(NOTHGATEDOMAIN, "");
                //    mailAddressFrom = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mailAddressFrom.Replace(".", " "));
                //}


                if (!string.IsNullOrEmpty(mrfDetail.ProjectName))
                {
                    obj.Subject = string.Format(obj.Subject, mrfDetail.MRFCode,
                                                        mrfDetail.ProjectName);


                    obj.Body = string.Format(obj.Body, objUtility.GetEmployeeFirstName(obj.To[0].ToString()),
                                                  mrfDetail.MRFCode,
                                                 // objAuMan.GetDomainUserName(mailFrom.Replace(RAVEDOMAIN, ""))
                                                  //objAuMan.GetDomainUserName(mailAddressFrom)
                                                  mailAddressFrom);



                }
                else
                {
                    obj.Subject = string.Format(obj.Subject.Replace("Project", "Department"), mrfDetail.MRFCode,
                                                        mrfDetail.DepartmentName);

                    obj.Body = string.Format(obj.Body, objUtility.GetEmployeeFirstName(obj.To[0].ToString()),
                                                    mrfDetail.MRFCode,
                                                    //objAuMan.GetDomainUserName(mailFrom.Replace(RAVEDOMAIN, ""))
                                                    //objAuMan.GetDomainUserName(mailAddressFrom)
                                                    mailAddressFrom);
                }
                string purposeSelected = mrfDetail.MRFPurpose;

                obj.SendEmail(obj);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, SETMRFAPPROVEREJECTREASON, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }

        }


        /// <summary>
        /// Updates the MRF status.
        /// </summary>
        /// <param name="mrfDetail">The MRF detail.</param>
        /// <param name="MailTo">The mail to.</param>
        public void UpdateMrfStatus(BusinessEntities.MRFDetail mrfDetail, string MailTo, string[] newMrfId, bool isMailInGroup)
        {
            int checkReasonSet;
            Rave.HR.BusinessLayer.MRF.MRFDetail mrfDetailBL = new MRFDetail();

            try
            {
                if (mrfDetail.MRFPurposeId == Convert.ToInt32(MasterEnum.MRFPurpose.MarketResearchfeasibility))
                {
                    mrfDetail.StatusId = (int)MasterEnum.MRFStatus.PendingExternalAllocation;
                }
                checkReasonSet = MRFDetail.SetMRFStatus(mrfDetail);

                // Sending mail directly to Recreuitment team when purpose option is Market Research/ Feasibility
                // in case of head count raised
                if (mrfDetail.MRFPurposeId == Convert.ToInt32(MasterEnum.MRFPurpose.MarketResearchfeasibility))
                {
                    SendEMailRaiseHeadCountToRecruitmentWithOutApproval(mrfDetail);
                }
                else
                {
                    //For Rave consultant department no approval is required.mail is directly sent to Recruiment.
                    if (string.IsNullOrEmpty(MailTo) && newMrfId.Length == 1 && isMailInGroup)
                    {
                        SendEMailRaiseHeadCountWithOutApproval(mrfDetail);
                    }
                    else if (!string.IsNullOrEmpty(MailTo))
                    {
                        SendEMailRaiseHeadCount(mrfDetail);
                    }
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, SETMRFAPPROVEREJECTREASON, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Sends the E mail raise head count approval to COO.
        /// </summary>
        /// <param name="mrfDetail">The MRF detail.</param>
        /// <param name="mailTo">The mail to.</param>
        private void SendEMailRaiseHeadCountApprovalToCOO(BusinessEntities.MRFDetail mrfDetail)
        {
            AuthorizationManager objAuMan = new AuthorizationManager();
            Utility objUtility = new Utility();
            //31585-Subhra-Start added the string
            string departmentHeadEmail = string.Empty;
            //31585- end

            //Todo mail to chandrAN.

            //31585-Subhra-start-To keep PM's name in cc when finding external resource for MRF raised.
            // Initialise the Collection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();        
            raveHRCollection = GetProjectManagerByProjectId(mrfDetail);
            //Initialise the Collection class object for department heads 
            BusinessEntities.RaveHRCollection raveHRCollectionDepartment = new RaveHRCollection();
            BusinessEntities.Recruitment objRecruitment = new BusinessEntities.Recruitment();
            objRecruitment.DepartmentId = mrfDetail.DepartmentId;
            //Gets department heads collection
            raveHRCollectionDepartment = DataAccessLayer.Recruitment.Recruitment.GetDepartmentHeadByDepartmentId(objRecruitment);
            //31585-Subhra-end


            IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),

                                             Convert.ToInt16(EnumsConstants.EmailFunctionality.HeadCountRequestapprovalToCOO));

            if (mrfDetail.ProjectId!=0)
            {
                //31585-Subhra-start-To keep PM's name in cc when finding external resource for MRF raised.
                //if (raveHRCollection.Count > 0)
                //{
                //    obj.CC.Add(GetEmailIdByEmpId(((BusinessEntities.MRFDetail)raveHRCollection.Item(0)).EmployeeId));
                //}
                if (raveHRCollection.Count > 0)
                {
                    //Get ProjectManager Name 
                    string projectManagerEmail = string.Empty;
                    foreach (BusinessEntities.MRFDetail objrer in raveHRCollection)
                    {
                        projectManagerEmail += objrer.EmailId;
                        objRecruitment.EmailId = projectManagerEmail;

                    }
                    if (objRecruitment.EmailId.EndsWith(","))
                    {
                        objRecruitment.EmailId = objRecruitment.EmailId.Substring(0, objRecruitment.EmailId.Length - 1);
                    }

                    obj.CC.Add(objRecruitment.EmailId);
                }


                //31585-Subhra-end
                obj.Subject = string.Format(obj.Subject, mrfDetail.MRFCode,
                                                    mrfDetail.ProjectName);

                obj.Body = string.Format(obj.Body, objUtility.GetEmployeeFirstName(obj.To[0].ToString()),
                                               mrfDetail.Role,
                                               mrfDetail.ProjectName,
                                               GetLink(CommonConstants.Page_MrfApproveRejectHeadCount));
            }
            else
            {
                //31585-Subhra-Start
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

                obj.CC.Add(objRecruitment.EmailId);
                //31585-Subhra-end
                obj.Subject = string.Format(obj.Subject.Replace("Project", "Department"), mrfDetail.MRFCode,
                                                    mrfDetail.DepartmentName);

                obj.Body = string.Format(obj.Body, objUtility.GetEmployeeFirstName(obj.To[0].ToString()),
                                               mrfDetail.Role,
                                               mrfDetail.DepartmentName,
                                               GetLink(CommonConstants.Page_MrfApproveRejectHeadCount));
            }

            obj.SendEmail(obj);
        }

        /// <summary>
        /// Sends the E mail head count request rejected.
        /// </summary>
        /// <param name="mrfDetail">The MRF detail.</param>
        private void SendEMailHeadCountRequestRejected(BusinessEntities.MRFDetail mrfDetail)
        {
            AuthorizationManager objAuMan = new AuthorizationManager();
            Utility objUtility = new Utility();

            IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),

                                             Convert.ToInt16(EnumsConstants.EmailFunctionality.RejectHeadCount));

            obj.Subject = string.Format(mrfDetail.ProjectId != 0 ? obj.Subject : obj.Subject.Replace("Project", "Department"), mrfDetail.MRFCode,
                                        mrfDetail.ProjectId != 0 ? mrfDetail.ProjectName : mrfDetail.DepartmentName);

            obj.Body = string.Format(obj.Body, objUtility.GetEmployeeFirstName(obj.To[0].ToString()),
                                               mrfDetail.Role,
                                               mrfDetail.ProjectId != 0 ? mrfDetail.ProjectName : mrfDetail.DepartmentName,
                                               mrfDetail.CommentReason,
                                               GetLink(CommonConstants.Page_MrfSummary)
                                               );


            obj.SendEmail(obj);
        }

        /// <summary>
        /// Sends the E mail for Move MRF.
        /// </summary>
        /// <param name="mrfDetail">The MRF detail.</param>
        private void SendEMailMoveMRF(BusinessEntities.MRFDetail mrfDetail, string newMRFCode, string[] mailDetail)
        {
            AuthorizationManager objAuMan = new AuthorizationManager();
            Utility objUtility = new Utility();
            string mailFrom = objAuMan.getLoggedInUserEmailId();
            IRMSEmail obj = null;

            if (mrfDetail.StatusId == Convert.ToInt32(MasterEnum.MRFStatus.PendingAllocation))
            {
                obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),
                                             Convert.ToInt16(EnumsConstants.EmailFunctionality.MoveMrfPendingAllcation));
            }
            if (mrfDetail.StatusId == Convert.ToInt32(MasterEnum.MRFStatus.PendingExternalAllocation))
            {
                obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),
                                            Convert.ToInt16(EnumsConstants.EmailFunctionality.MoveMrfPendingExternalAllocation));

                if (mrfDetail.RecruitersId != null && mrfDetail.RecruitersId.Trim() != string.Empty)
                {
                    int recruitersID = Convert.ToInt32(mrfDetail.RecruitersId);
                    obj.To.Add(GetEmailIdByEmpId(recruitersID));
                }
            }
            if (mrfDetail.StatusId == Convert.ToInt32(MasterEnum.MRFStatus.PendingApprovalOfFinance))
            {
                obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),
                                          Convert.ToInt16(EnumsConstants.EmailFunctionality.MoveMrfPendingApprovalOfFinance));

            }
            if (mrfDetail.StatusId == Convert.ToInt32(MasterEnum.MRFStatus.PendingHeadCountApprovalOfFinance))
            {
                obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),
                                          Convert.ToInt16(EnumsConstants.EmailFunctionality.MoveMrfPendingHeadCountApprovalOfFinance));

            }
            if (mrfDetail.StatusId == Convert.ToInt32(MasterEnum.MRFStatus.PendingHeadCountApprovalOfCOO))
            {
                obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),
                                          Convert.ToInt16(EnumsConstants.EmailFunctionality.MoveMrfPendingHeadCountApprovalOfCOO));

            }
            if (mrfDetail.StatusId == Convert.ToInt32(MasterEnum.MRFStatus.PendingExpectedResourceJoin))
            {
                obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),
                                         Convert.ToInt16(EnumsConstants.EmailFunctionality.MoveMrfPendingExpectedResourceJoin));
                if (mrfDetail.RecruitersId != null && mrfDetail.RecruitersId.Trim() != string.Empty)
                {
                    int recruitersID = Convert.ToInt32(mrfDetail.RecruitersId);
                    obj.To.Add(GetEmailIdByEmpId(recruitersID));
                }
            }
            if (mrfDetail.StatusId == Convert.ToInt32(MasterEnum.MRFStatus.ResourceJoin))
            {
                obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),
                                          Convert.ToInt16(EnumsConstants.EmailFunctionality.MoveMrfResourceJoin));
                if (mrfDetail.RecruitersId != null && mrfDetail.RecruitersId.Trim() != string.Empty)
                {
                    int recruitersID = Convert.ToInt32(mrfDetail.RecruitersId);
                    obj.CC.Add(GetEmailIdByEmpId(recruitersID));
                }
            }
            if (mrfDetail.StatusId == Convert.ToInt32(MasterEnum.MRFStatus.PendingNewEmployeeAllocation))
            {
                obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),
                                          Convert.ToInt16(EnumsConstants.EmailFunctionality.MoveMrfPendingNewEmployeeAllocation));
                if (mrfDetail.RecruitersId != null && mrfDetail.RecruitersId.Trim() != string.Empty)
                {
                    int recruitersID = Convert.ToInt32(mrfDetail.RecruitersId);
                    obj.CC.Add(GetEmailIdByEmpId(recruitersID));
                }
            }

            //sending mail for employee who raised the new MRF
            if (mrfDetail.CreatedById > 0)
            {
                int CreatedId = Convert.ToInt32(mrfDetail.CreatedById);
                obj.CC.Add(GetEmailIdByEmpId(CreatedId));
            }

            //sending mail for new mrf Responsible person
            if (mrfDetail.ReportingToId != null && mrfDetail.ReportingToId.Trim() != string.Empty)
            {
                int ReportingToId = Convert.ToInt32(mrfDetail.ReportingToId);
                obj.CC.Add(GetEmailIdByEmpId(ReportingToId));
            }
            if (mailDetail[4].Trim() != null && mailDetail[4].Trim() != string.Empty)
            {//sending mail for new mrf Project Manager
                obj.CC.Add(mailDetail[4].Trim());

            }
            if (mailDetail[1].Trim() != null && mailDetail[1].Trim() != string.Empty)
            {//sending mail to old MRF Project Manager

                obj.CC.Add(mailDetail[1].Trim());
            }
            if ((mailDetail[5].Trim() == "2" || mailDetail[5].Trim() == "4") && mailDetail[2].Trim() != null && mailDetail[2].Trim() != string.Empty)
            {
                //sending mail to old MRF Responsible person
                obj.CC.Add(mailDetail[2].Trim());

            }
            else if ((mailDetail[5].Trim() == "3" || mailDetail[5].Trim() == "4") && mailDetail[3].Trim() != null && mailDetail[3].Trim() != string.Empty)
            {//sending mail for employee who raised the old MRF
                obj.CC.Add(mailDetail[3].Trim());
            }
            obj.Subject = string.Format(obj.Subject, mrfDetail.MRFCode,
                                                    newMRFCode);
            if (mrfDetail.StatusId == Convert.ToInt32(MasterEnum.MRFStatus.ResourceJoin))
            {
                obj.Body = string.Format(obj.Body,"HR Team",
                                                     mrfDetail.MRFCode,
                                                     newMRFCode
                                                     );
            }
            else
            {
                //Issue ID - 31921  Abhishek
                //Recruiter name missing from the Move MRF mail
                //START
                string employeeEmailID = string.Empty;
                foreach (string to in obj.To)
                {
                    if (!string.IsNullOrEmpty(to) && employeeEmailID == "")
                    {
                        employeeEmailID = to;                        
                    }
                }

                //obj.Body = string.Format(obj.Body, objUtility.GetEmployeeFirstName(employeeEmailID),
                //                                  mrfDetail.MRFCode,
                                                  //newMRFCode);

                
                // Rakesh : Issue 57965 : 10/May/2016 : Starts Remove Employeename from Mail 

                obj.Body = string.Format(obj.Body,mrfDetail.MRFCode,newMRFCode);
                //END
            

                //Old Code Start
                //obj.Body = string.Format(obj.Body, objUtility.GetEmployeeFirstName(obj.To[0].ToString()),
                //                                  mrfDetail.MRFCode,
                //                                  newMRFCode
                //                                  );
                //Old Code End
                //END

               
            }
                //objAuMan.GetDomainUserName(mailFrom.Replace(RAVEDOMAIN, ""))
            obj.SendEmail(obj);
        }

        /// <summary>
        /// Get the Employee email Id by empId.
        /// </summary>
        /// <param name="EmpId"></param>
        /// <returns></returns>
        public string GetEmailIdByEmpId(int EmpId)
        {
            try
            {

                Rave.HR.BusinessLayer.Contracts.Contract contractBL = new Rave.HR.BusinessLayer.Contracts.Contract();
                return contractBL.GetEmailIdByEmpId(EmpId);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, "GetEmailIdByEmpId", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Method for internal resource allocation mail functionality
        /// </summary>
        public void SendMailToFinanceForInternalResourceAllocation(BusinessEntities.MRFDetail mrfDetail)
        {
            try
            {
                Utility objUtil = new Utility();

                Rave.HR.BusinessLayer.Interface.IRMSEmail objMailForInternalResource = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),

                                           Convert.ToInt16(EnumsConstants.EmailFunctionality.InternalResourceAllocationApprovalFromFinance));

                /*mail subjects are different for projects and departments*/
                objMailForInternalResource.Subject = string.Format(objMailForInternalResource.Subject, mrfDetail.MRFCode,
                                                                (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? _project : _department,
                                                                (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? mrfDetail.ProjectName : mrfDetail.DepartmentName,
                                                                 mrfDetail.EmployeeName);

                string mailAddressFrom = objAuMan.getLoggedInUserEmailId();
                //GoogleMail
                //GoogleConfugrable
                mailAddressFrom = objAuMan.GetUsernameBasedOnEmail(mailAddressFrom);
                mailAddressFrom = objAuMan.GetDomainUserName(mailAddressFrom);
                //if (mailAddressFrom.ToLower().Contains(RAVEDOMAIN))
                //{
                //    mailAddressFrom = mailAddressFrom.Replace(RAVEDOMAIN, "");
                //    mailAddressFrom = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mailAddressFrom.Replace(".", " "));
                //}
                //else
                //{
                //    mailAddressFrom = mailAddressFrom.Replace(NOTHGATEDOMAIN, "");
                //    mailAddressFrom = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mailAddressFrom.Replace(".", " "));
                //}


                /*mail bodies are different for projects and departments*/
                objMailForInternalResource.Body = string.Format(objMailForInternalResource.Body,
                                                           objUtil.GetEmployeeFirstName(objMailForInternalResource.To[0].ToString()),
                                                           mrfDetail.EmployeeName,
                                                           (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? mrfDetail.ProjectName : mrfDetail.DepartmentName,
                                                           mrfDetail.Role,
                                                           (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? " belonging to department [" +
                                                           mrfDetail.DepartmentName : "",
                                                           GetHTMLForTableData(mrfDetail),
                                                           GetLink(CommonConstants.Page_MrfPendingApproval),
                    //objAuMan.GetDomainUserName(objAuMan.getLoggedInUserEmailId().Replace(RAVEDOMAIN, "")));
                    //objAuMan.GetDomainUserName(mailAddressFrom));
                                                           mailAddressFrom);



                //Siddharth 3rd Dec 2015 Start
                //Below lines commented by Siddharth
                //Approval required mail should not go to Sai or Navya or PM; only to Kedar, MVV, RMGroup & rmsadmin

                //// Venkatesh : 47714  : 06/Jan/2014 : Starts                    
                //// Desc : Add Accoutable Person in MailTO 
                //string AccountableEmailTo = "";
                //if (mrfDetail.ReportingToId != null)
                //{
                //    AccountableEmailTo = new Rave.HR.BusinessLayer.Contracts.Contract().GetEmailID(Convert.ToInt32(mrfDetail.ReportingToId));
                //    if (AccountableEmailTo != null || AccountableEmailTo != "")
                //        objMailForInternalResource.CC.Add(AccountableEmailTo);

                //}
                //// Venkatesh :47714  : 06/Jan/2014 : End



                ////Added by Siddharth : NIS-RMS : 23/02/2015 : Starts
                //Rave.HR.BusinessLayer.MRF.MRFDetail mrfClientName = new Rave.HR.BusinessLayer.MRF.MRFDetail();
                //raveHRCollection = mrfClientName.GetClientNameFromProjectID(mrfDetail.ProjectId);

                //if (raveHRCollection != null)
                //{
                //    if (raveHRCollection.Count > 0)
                //    {
                //        mrfDetail.ClientName = ((BusinessEntities.KeyValue<string>)(raveHRCollection.Item(0))).KeyName;
                //        if (mrfDetail.ClientName.ToUpper().Contains("NPS") || mrfDetail.ClientName.ToUpper().Contains("NORTHGATE"))
                //        {
                //            objMailForInternalResource.CC.Add(CommonConstants.SAIGIDDUNISandNAVYANISEmailId);
                //            //objMailForInternalResource.CC.Add(CommonConstants.SAIGIDDUNISEmailId);
                //            //objMailForInternalResource.CC.Add(CommonConstants.NAVYANISEmailId);
                //        }
                //    }
                //}
                //// Siddharth : NIS-RMS : 23/02/2015 : Ends

                //Siddharth 3rd Dec 2015 End


                objMailForInternalResource.SendEmail(objMailForInternalResource);

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETMAILFORRESOURCEALLOCATION, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Method for Internal Resource Allocation Mail To whizible
        /// </summary>
        public void SendMailToWhizibleForInternalResourceAllocation(BusinessEntities.MRFDetail mrfDetail, BusinessEntities.MRFDetail mailDetail, bool B_ClientName)
        {
            try
            {

                //for multiple reporting to
                if (mailDetail.EmployeeName.Contains(","))
                {
                    int lastComma = mailDetail.EmployeeName.LastIndexOf(',');
                    string reportingTo = mailDetail.EmployeeName.Remove(lastComma, 1);
                    string reportingToNames = reportingTo.Insert(lastComma, " and ");
                    mailDetail.EmployeeName = reportingToNames;
                }

                //Method For Send Email
                SendMailToWhizibleForInternalResource(mrfDetail, mailDetail, B_ClientName);


            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETMAILFORRESOURCEALLOCATION, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Method to send email to RPM,Whizible,reportingTo,allocated resource for Internal resource allocation
        /// </summary>
        private void SendMailToWhizibleForInternalResource(BusinessEntities.MRFDetail mrfDetail, BusinessEntities.MRFDetail mailDetail, bool B_ClientName)
        {
            Utility objUtil = new Utility();
            string departmentHeadEmail = string.Empty;

            //Get Allocation Date Details 
            BusinessEntities.MRFDetail mrfAlocationDateDetail = new BusinessEntities.MRFDetail();
            mrfAlocationDateDetail = GetAllocationDateDetails(mrfDetail);

            // Initialise the Collection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();

            raveHRCollection = GetProjectManagerByProjectId(mailDetail);

            //Initialise the Collection class object for department heads 
            BusinessEntities.RaveHRCollection raveHRCollectionDepartment = new RaveHRCollection();
            BusinessEntities.Recruitment objRecruitment = new BusinessEntities.Recruitment();

            objRecruitment.DepartmentId = mailDetail.DepartmentId;

            //Gets department heads collection
            raveHRCollectionDepartment = DataAccessLayer.Recruitment.Recruitment.GetDepartmentHeadByDepartmentId(objRecruitment);

            if (mailDetail.BillingDate > Convert.ToDateTime(mrfAlocationDateDetail.AllocationDate))
            {
                mailDetail.Billing = 0;
            }

            Rave.HR.BusinessLayer.Interface.IRMSEmail objMailForInternalResource = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),
                                       Convert.ToInt16(EnumsConstants.EmailFunctionality.InternalResourceAllocated));


            objMailForInternalResource.To.Add(GetEmailIdByEmpId(mrfDetail.EmployeeId));

            //For CC TO PM And Functional Manager
            //To Solve Issue ID:18693
            if (raveHRCollection.Count > 0)
            {
                //Get ProjectManager Name 
                string projectManagerEmail = string.Empty;
                foreach (BusinessEntities.MRFDetail objrer in raveHRCollection)
                {
                    projectManagerEmail += objrer.EmailId;
                    objRecruitment.EmailId = projectManagerEmail;

                }
                if (objRecruitment.EmailId.EndsWith(","))
                {
                    objRecruitment.EmailId = objRecruitment.EmailId.Substring(0, objRecruitment.EmailId.Length - 1);
                }

                objMailForInternalResource.CC.Add(objRecruitment.EmailId);
            }

            //commented Mahendra 
            //if (raveHRCollection.Count > 0)
            //{
            //    //commented Mahendra : only first PM record get consider instead of all Issue  ID :
            //    objMailForInternalResource.CC.Add(GetEmailIdByEmpId(((BusinessEntities.MRFDetail)raveHRCollection.Item(0)).EmployeeId));

            //    //if (!string.IsNullOrEmpty(mailDetail.ReportingToId))
            //    //{
            //    //    if (GetEmailIdByEmpId(int.Parse(mailDetail.ReportingToId)) != GetEmailIdByEmpId(((BusinessEntities.MRFDetail)raveHRCollection.Item(0)).EmployeeId))
            //    //        objMailForInternalResource.CC.Add(GetEmailIdByEmpId(int.Parse(mailDetail.ReportingToId)));
            //    //}
            //}

            //Mohamed : Issue 40242 : 19/03/2013 : Starts
            //Desc :  When the Resource is allocated then in mail its Functional manager should be in CC
            if (mailDetail.DepartmentId != CommonConstants.BA && mailDetail.DepartmentId != CommonConstants.USABILITY) // Check for Departement BA and Usablity if not then
            {
                objMailForInternalResource.CC.Add(GetEmailIdByEmpId(int.Parse(mailDetail.FunctionalManagerId)));
            }
            //Mohamed : Issue 40242 : 19/03/2013 : Ends

            // 32170-Subhra-start-21122011
            // To add Reporting To employee email address in the mail 
            objMailForInternalResource.CC.Add(GetEmailIdByEmpId(int.Parse(mailDetail.ReportingToId)));
            // 32170-Subhra-end-21122011

            // Siddharth : NIS-RMS : 19/02/2015 : Starts                        			  
            // Desc : Add Naviya and Sai Email id where Client Name is NIS or Northgate 
            
            //Added by Siddharth

            if (B_ClientName == true || (mrfDetail.ClientName.ToUpper().Contains("NPS") || mrfDetail.ClientName.ToUpper().Contains("NORTHGATE")))
            {
                objMailForInternalResource.CC.Add(CommonConstants.SAIGIDDUNISandNAVYANISEmailId);
                //objMailForInternalResource.CC.Add(CommonConstants.SAIGIDDUNISEmailId);
                //objMailForInternalResource.CC.Add(CommonConstants.NAVYANISEmailId);
            }
            // Siddharth : NIS-RMS : 19/02/2015 : Ends




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

            objMailForInternalResource.CC.Add(objRecruitment.EmailId);

            /*For Rave Internal Projects*/
            /*mail subjects are different for projects and departments*/
            objMailForInternalResource.Subject = string.Format(objMailForInternalResource.Subject,
                ((mrfDetail.ClientName != _checkNullValue) && (mrfDetail.ClientName != "")) ? _clientName : "",
                ((mrfDetail.ClientName != _checkNullValue) && (mrfDetail.ClientName != "")) ? mrfDetail.ClientName + "," : "",
                (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? _project : _department,
               (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? mrfDetail.ProjectName : mrfDetail.DepartmentName);

                string mailAddressFrom = objMailForInternalResource.From;
                //GoogleMail
                //GoogleConfugrable
                mailAddressFrom = objAuMan.GetUsernameBasedOnEmail(mailAddressFrom);
                mailAddressFrom = objAuMan.GetDomainUserName(mailAddressFrom);
                //if (mailAddressFrom.ToLower().Contains(RAVEDOMAIN))
                //{
                //    mailAddressFrom = mailAddressFrom.Replace(RAVEDOMAIN, "");
                //    mailAddressFrom = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mailAddressFrom.Replace(".", " "));
                //}
                //else
                //{
                //    mailAddressFrom = mailAddressFrom.Replace(NOTHGATEDOMAIN, "");
                //    mailAddressFrom = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mailAddressFrom.Replace(".", " "));
                //}


            /*mail bodies are different for projects and departments*/
                objMailForInternalResource.Body = string.Format(objMailForInternalResource.Body,
                                                   objUtil.GetEmployeeFirstName(mrfDetail.EmployeeName),
                                                   ((mrfDetail.ClientName != _checkNullValue) && (mrfDetail.ClientName != "")) ? mrfDetail.ClientName : "",
                                                   (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? mrfDetail.ProjectName : mrfDetail.DepartmentName,
                                                   (DateTime.Parse(mrfAlocationDateDetail.AllocationDate)).ToString(CommonConstants.DATE_FORMAT),
                                                   mailDetail.EmployeeName,
                                                   mrfDetail.DepartmentName == MasterEnum.Departments.RaveForecastedProjects.ToString() ? string.Empty : mailDetail.ResourceResponsibility,
                                                   mrfDetail.FunctionalManager,
                    // objAuMan.GetDomainUserName(objMailForInternalResource.From.Replace(RAVEDOMAIN, "")));
                    //objAuMan.GetDomainUserName(mailAddressFrom));
                                                  mailAddressFrom);

            if (mrfDetail.DepartmentName == MasterEnum.Departments.RaveForecastedProjects.ToString())
            {
                //36941-Ambar-Start
                if (mrfDetail.ClientName == null)
                {
                  mrfDetail.ClientName = "";
                }
                //36941-Ambar-End
                // 28369-Subhra-start
                // Added code to remove Client name 'RaveForecastedProject' from subject and body
                objMailForInternalResource.Subject = objMailForInternalResource.Subject.Replace(mrfDetail.ClientName + ",", string.Empty);
                objMailForInternalResource.Subject = objMailForInternalResource.Subject.Replace(_clientName, string.Empty);

                // venkatesh  : Issue 47525 : 4/12/2013 : Starts 
                // Desc : Client Name in Mail Allocation "
                if (mrfDetail.ClientName !="")
                    objMailForInternalResource.Body = objMailForInternalResource.Body.Replace(mrfDetail.ClientName, string.Empty);
                // venkatesh  : Issue 47525 : 4/12/2013 : End 
                // 28369-Subhra-end
                objMailForInternalResource.Body = objMailForInternalResource.Body.Replace(MasterEnum.Departments.RaveForecastedProjects.ToString(), "Rave Resource Pool");
                objMailForInternalResource.Body = objMailForInternalResource.Body.Replace("<br/><br/>Your responsibilities for the defined role <b><u>in the project are typically any/all of the following as per the requirements of the project:</b></u><br/>.", string.Empty);
            }
            objMailForInternalResource.SendEmail(objMailForInternalResource);


            //To solve IssueId 23772:Subhra
            //if (mrfDetail.EmployeeTypeId == Convert.ToInt32(MasterEnum.EmployeeType.OnsiteContract))
            //{
                /*For Rave Internal Projects*/
                Rave.HR.BusinessLayer.Interface.IRMSEmail objMailForInternalResourceToWhizible = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),
                                           Convert.ToInt16(EnumsConstants.EmailFunctionality.ResourceAllocationMailToWhizible));

                string mailAddress = objMailForInternalResourceToWhizible.From;
                //GoogleMail
                //GoogleConfugrable
                //GoogleConfugrable
                mailAddressFrom = objAuMan.GetUsernameBasedOnEmail(mailAddressFrom);
                mailAddressFrom = objAuMan.GetDomainUserName(mailAddressFrom);

                //if (mailAddress.ToLower().Contains(RAVEDOMAIN))
                //{
                //    mailAddress = mailAddress.Replace(RAVEDOMAIN, "");
                //    mailAddress = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mailAddress.Replace(".", " "));
                //}
                //else
                //{
                //    mailAddress = mailAddress.Replace(NOTHGATEDOMAIN, "");
                //    mailAddress = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mailAddress.Replace(".", " "));
                //}

                /*mail(To Whizible and RPM) bodies are different for projects and departments*/
                objMailForInternalResourceToWhizible.Body = string.Format(objMailForInternalResourceToWhizible.Body,
                                                       objUtil.GetEmployeeFirstName(objMailForInternalResourceToWhizible.To[0].ToString()),
                                                       mrfDetail.EmployeeName,
                                                       (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? _project : _department,
                                                       (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? mrfDetail.ProjectName : mrfDetail.DepartmentName,
                                                       (DateTime.Parse(mrfAlocationDateDetail.AllocationDate)).ToString(CommonConstants.DATE_FORMAT),
                                                       mailDetail.Utilization,
                                                       mailDetail.Billing,
                                                       (mailDetail.ProjectId.ToString() != "0") ? mailDetail.ProjectId.ToString() : "NA",
                                                       mailDetail.ResourceAllocationID,
                                                       mailDetail.MRFCode,
                                                       mrfDetail.Role,
                                                       mailDetail.ResourceReleased.ToString(CommonConstants.DATE_FORMAT),
                                                       mailDetail.EmployeeName,
                    //objAuMan.GetDomainUserName(objMailForInternalResourceToWhizible.From.Replace(RAVEDOMAIN, "")));
                    //objAuMan.GetDomainUserName(mailAddress));
                                                       mailAddressFrom);


                if (objMailForInternalResourceToWhizible.Body.Contains(DateTime.MinValue.ToString(CommonConstants.DATE_FORMAT)))
                {
                    objMailForInternalResourceToWhizible.Body = objMailForInternalResourceToWhizible.Body.Replace(DateTime.MinValue.ToString(CommonConstants.DATE_FORMAT), string.Empty);
                }

                objMailForInternalResourceToWhizible.SendEmail(objMailForInternalResourceToWhizible);
            //}
        }

        /// <summary>
        /// method to get html data
        /// </summary>
        private string GetHTMLForTableData(BusinessEntities.MRFDetail mrfDetail)
        {

            try
            {
                //list for table values
                List<string> objListMrfDetail = new List<string>();

                //Get Allocation Date Details 
                BusinessEntities.MRFDetail mrfAlocationDateDetail = new BusinessEntities.MRFDetail();
                mrfAlocationDateDetail = GetAllocationDateDetails(mrfDetail);

                objListMrfDetail.Add(mrfAlocationDateDetail.AllocationDate);
                objListMrfDetail.Add(mrfAlocationDateDetail.ResourceReleased.ToString(CommonConstants.DATE_FORMAT));
                objListMrfDetail.Add(mrfDetail.Utilization.ToString());
                objListMrfDetail.Add(mrfDetail.Billing.ToString());


                string[,] arrayData = new string[objListMrfDetail.Count, 2];

                if (objListMrfDetail.Count > 0)
                {
                    //Header Values
                    arrayData[0, 0] = _headerStartDate;
                    arrayData[1, 0] = _headerEndDate;
                    arrayData[2, 0] = _headerUtilization;
                    arrayData[3, 0] = _headerBilling;

                    //Row Details
                    for (int i = 0; i <= objListMrfDetail.Count - 1; i++)
                    {
                        arrayData[i, 1] = objListMrfDetail[i];
                    }

                }

                IEmailTableData objEmailTableData = new EmailTableData();
                objEmailTableData.RowDetail = arrayData;
                objEmailTableData.RowCount = objListMrfDetail.Count;
                return objEmailTableData.GetVerticalHeaderTableData(objEmailTableData);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETHTMLFORTABLEDATA, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Method To Get Allocation Start And EndDate of Resource.
        /// </summary>
        private BusinessEntities.MRFDetail GetAllocationDateDetails(BusinessEntities.MRFDetail mrfDetail)
        {
            //Declare DataAccesslayer and collection object 
            DataAccessLayer.MRF.MRFDetail ObjDLAllocationDateDetails = new DataAccessLayer.MRF.MRFDetail();
            BusinessEntities.RaveHRCollection objAllocationDateDetails = new BusinessEntities.RaveHRCollection();

            try
            {

                //Method to Allocation Date Details of resource
                objAllocationDateDetails = ObjDLAllocationDateDetails.GetAllocationDateDetails(mrfDetail);

                if (objAllocationDateDetails != null)
                {
                    foreach (BusinessEntities.MRFDetail objDateDetails in objAllocationDateDetails)
                    {
                        mrfDetail.AllocationDate = objDateDetails.AllocationDate;
                        // Rajan Kumar : Issue 45752 : 03/01/2014 : Starts                        			 
                        // Desc : When MRF is raised against department then MRF end date is not mandatory (remove validation).
                        if (objDateDetails.ResourceReleased != new DateTime())
                        {
                            mrfDetail.ResourceReleased = objDateDetails.ResourceReleased;
                        }
                        // Rajan Kumar : Issue 45752 : 03/01/2014: Ends 


                       
                    }
                }

                return mrfDetail;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETALLOCATIONDATEDETAILS, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Method to send Mail for Internal Resource Rejection
        /// </summary>
        public void SendMailToRPMForInternalResourceRejection(BusinessEntities.MRFDetail mrfDetail, BusinessEntities.MRFDetail mailDetail)
        {
            try
            {
                Utility objUtil = new Utility();

                Rave.HR.BusinessLayer.Interface.IRMSEmail objMailForInternalResource = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),

                                           Convert.ToInt16(EnumsConstants.EmailFunctionality.InternalResourceAllocationRejectionByFinance));

                objMailForInternalResource.To.Add(objAuMan.getLoggedInUserEmailId());


                //Siddharth 3rd Dec 2015 Start
                //Allocation rejected mail should go to Kedar, MVV, RMGroup & rmsadmin.

                ////Siddharth Add CC of Reporting To and Functional Manager  02-March-2015 : Starts                    
               
                //string[] PmEmailId = null;
                //if (mailDetail.ReportingToId != null || mailDetail.ReportingToId != "")
                //{
                //    PmEmailId = mailDetail.ReportingToId.Split(',');
                //    for (int i = 0; i < PmEmailId.Length; i++)
                //    {
                //        objMailForInternalResource.CC.Add(PmEmailId[i].ToString());
                //    }
                    
                //}
                //PmEmailId = null;
                //if (mailDetail.FunctionalManagerId != null || mailDetail.FunctionalManagerId != "")
                //{
                //    PmEmailId = mailDetail.FunctionalManagerId.Split(',');
                //    for (int i = 0; i < PmEmailId.Length; i++)
                //    {
                //        objMailForInternalResource.CC.Add(mailDetail.FunctionalManagerId);
                //    }
                //}
                //PmEmailId = null;

                // // Siddharth : 02-March-2015 : End

                //Rave.HR.BusinessLayer.MRF.MRFDetail mrfClientName = new Rave.HR.BusinessLayer.MRF.MRFDetail();
                //raveHRCollection = mrfClientName.GetClientNameFromProjectID(mrfDetail.ProjectId);

                //if (raveHRCollection != null)
                //{
                //    if (raveHRCollection.Count > 0)
                //    {
                //        mrfDetail.ClientName = ((BusinessEntities.KeyValue<string>)(raveHRCollection.Item(0))).KeyName;
                //        if (mrfDetail.ClientName.ToUpper().Contains("NPS") || mrfDetail.ClientName.ToUpper().Contains("NORTHGATE"))
                //        {
                //            objMailForInternalResource.CC.Add(CommonConstants.SAIGIDDUNISandNAVYANISEmailId);
                //            //objMailForInternalResource.CC.Add(CommonConstants.SAIGIDDUNISEmailId);
                //            //objMailForInternalResource.CC.Add(CommonConstants.NAVYANISEmailId);
                //        }
                //    }
                //}

                //Siddharth 3rd Dec 2015 End

                /*mail subjects are different for projects and departments*/
                objMailForInternalResource.Subject = string.Format(objMailForInternalResource.Subject,
                                                    mrfDetail.MRFCode,
                                                    mrfDetail.ClientName,
                                                    (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? _project : _department,
                                                    (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? mrfDetail.ProjectName : mrfDetail.DepartmentName);

                string mailAddress = objAuMan.getLoggedInUserEmailId();
                //GoogleMail
                //GoogleConfugrable
                mailAddress = objAuMan.GetUsernameBasedOnEmail(mailAddress);
                mailAddress = objAuMan.GetDomainUserName(mailAddress);
                //if (mailAddress.ToLower().Contains(RAVEDOMAIN))
                //{
                //    mailAddress = mailAddress.Replace(RAVEDOMAIN, "");
                //    mailAddress = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mailAddress.Replace(".", " "));
                //}
                //else
                //{
                //    mailAddress = mailAddress.Replace(NOTHGATEDOMAIN, "");
                //    mailAddress = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mailAddress.Replace(".", " "));
                //}


                /*mail bodies are different for projects and departments*/
                objMailForInternalResource.Body = string.Format(objMailForInternalResource.Body,
                                                   mrfDetail.EmployeeName,
                                                   (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? mrfDetail.ProjectName : mrfDetail.DepartmentName,
                                                   mrfDetail.Role,
                                                   mrfDetail.CommentReason,
                                                   GetLink(CommonConstants.Page_MrfPendingApproval),
                    //objAuMan.GetDomainUserName(objAuMan.getLoggedInUserEmailId().Replace(RAVEDOMAIN, "")));
                    //objAuMan.GetDomainUserName(mailAddress));
                                                   mailAddress);

                objMailForInternalResource.SendEmail(objMailForInternalResource);

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETMAILFORRESOURCEALLOCATION, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Function will use in Raise MRF
        /// </summary>
        /// <param name="MRFDetailobject"></param>
        /// <param name="dtResourceGrid"></param>
        /// <returns></returns>
        private RaveHRCollection ReplaceMRFBL(BusinessEntities.MRFDetail MRFDetailobject, DataTable dtResourceGrid)
        {
            string MRFCode;
            string NewMRFCode;
            int checkvalue;
            RaveHRCollection raveHRCollectionNew = new RaveHRCollection();

            DataAccessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
            mrfDetailBL = new MRFDetail();

            try
            {
                if (MRFDetailobject != null)
                {
                    if (MRFDetailobject.ProjectId != 0)
                    {
                        MRFCode = "MRF_" + MRFDetailobject.ProjectName + "_" + MRFDetailobject.Role + "_";
                        MRFDetailobject.MRFCode = MRFCode;
                        if (dtResourceGrid != null)
                        {
                            for (int i = 0; i < dtResourceGrid.Rows.Count; i++)
                            {
                                KeyValue<string> key = new KeyValue<string>();
                                checkvalue = Convert.ToInt32(dtResourceGrid.Rows[i][CommonConstants.CHECKGRIDVALUE]);
                                if (checkvalue == 1)
                                {
                                    MRFDetailobject.ResourcePlanDurationId = Convert.ToInt32(dtResourceGrid.Rows[i][CommonConstants.RESOURCEPLANDURATIONID]);
                                    MRFDetailobject.ResourceOnBoard = Convert.ToDateTime(dtResourceGrid.Rows[i][CommonConstants.RESOURCEPLANSTARTDATE]);
                                    MRFDetailobject.ResourceReleased = Convert.ToDateTime(dtResourceGrid.Rows[i][CommonConstants.RESOURCEPLANENDDATE]);
                                    MRFDetailobject.Billing = Convert.ToInt32(dtResourceGrid.Rows[i][CommonConstants.RESOURCEPLANBILLING]);
                                    MRFDetailobject.Utilization = Convert.ToInt32(dtResourceGrid.Rows[i][CommonConstants.RESOURCEPLANUTILIZATION]);
                                    NewMRFCode = mRFDetail.RaiseMRFDL(MRFDetailobject);
                                    key.KeyName = NewMRFCode.Split(',')[0].ToString();
                                    key.Val = NewMRFCode.Split(',')[0].ToString();
                                    MRFDetailobject.MRFId = int.Parse(NewMRFCode.Split(',')[1].ToString());
                                    // MRFDetailobject.MRFCode = NewMRFCode;
                                    raveHRCollectionNew.Add(key);
                                }
                            }
                        }
                        else
                        {
                            KeyValue<string> key = new KeyValue<string>();
                            NewMRFCode = mRFDetail.RaiseMRFDL(MRFDetailobject);
                            key.KeyName = NewMRFCode.Split(',')[0].ToString();
                            key.Val = NewMRFCode.Split(',')[0].ToString();
                            MRFDetailobject.MRFId = int.Parse(NewMRFCode.Split(',')[1].ToString());
                            // MRFDetailobject.MRFCode = NewMRFCode;
                            raveHRCollectionNew.Add(key);
                        }
                    }
                    else
                    {
                        MRFCode = "MRF_" + MRFDetailobject.DepartmentName + "_" + MRFDetailobject.Role + "_";
                        MRFDetailobject.MRFCode = MRFCode;

                        for (int i = 0; i < MRFDetailobject.NoOfResourceRequired; i++)
                        {
                            KeyValue<string> keynew = new KeyValue<string>();
                            MRFDetailobject.ResourcePlanDurationId = 0;
                            NewMRFCode = mRFDetail.RaiseMRFDL(MRFDetailobject);
                            keynew.KeyName = NewMRFCode.Split(',')[0].ToString();
                            keynew.Val = NewMRFCode.Split(',')[0].ToString();
                            MRFDetailobject.MRFId = int.Parse(NewMRFCode.Split(',')[1].ToString());
                            //MRFDetailobject.MRFCode = NewMRFCode;
                            raveHRCollectionNew.Add(keynew);
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
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, GETPROJECTNAMEROLEWISE, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
            return raveHRCollectionNew;
        }

        /// <summary>
        /// Function will GetLink
        /// </summary>
        /// <returns></returns>
        private string GetEmailLink(string PageName, BusinessEntities.MRFDetail mrfDetail, int mrfStatus, int MrfId)
        {
            string sComp = string.Empty;

            try
            {
                //Link for Raise MRF for Projects.
                if (mrfStatus == Convert.ToInt16(EnumsConstants.EmailFunctionality.RaiseMRFForProjects))
                {
                    sComp = Utility.GetUrl() + PageName + _questionMark + URLHelper.SecureParameters(CommonConstants.CON_QSTRING_AllocateResourec, _true)
                     + _and + URLHelper.SecureParameters(CommonConstants.CON_QSTRING_MRFID, MrfId.ToString())
                     + _and + URLHelper.SecureParameters(CommonConstants.CLIENT_NAME, mrfDetail.ClientName)
                     + _and + URLHelper.SecureParameters(_index, _zero) + _and + URLHelper.SecureParameters(_pageType, QueryStringConstants.MRFPENDINGALLOCATION)
                     + _and + URLHelper.CreateSignature(_true, MrfId.ToString(), mrfDetail.ClientName, _zero, QueryStringConstants.MRFPENDINGALLOCATION);
                }

                //Link for Raise/Replace MRF for Departments.
                else if (mrfStatus == Convert.ToInt16(EnumsConstants.EmailFunctionality.ReplaceMRF)
                     || (mrfStatus == Convert.ToInt16(EnumsConstants.EmailFunctionality.RaiseMRFForDepartments)))
                {
                    sComp = Utility.GetUrl() + PageName + _questionMark + URLHelper.SecureParameters(CommonConstants.CON_QSTRING_AllocateResourec, _true)
                     + _and + URLHelper.SecureParameters(CommonConstants.MRF_ID, MrfId.ToString())
                     + _and + URLHelper.SecureParameters(_index, _zero) + _and + URLHelper.SecureParameters(_pageType, QueryStringConstants.MRFPENDINGALLOCATION)
                     + _and + URLHelper.CreateSignature(_true, MrfId.ToString(), _zero, QueryStringConstants.MRFPENDINGALLOCATION);
                }
                else
                {
                    //Link for Raise HeadCount WithOut Approval.
                    if (mrfStatus == Convert.ToInt16(EnumsConstants.EmailFunctionality.RaiseHeadCountWithOutApproval))
                    {
                        sComp = Utility.GetUrl() + PageName + _questionMark + URLHelper.SecureParameters(CommonConstants.MRF_ID, MrfId.ToString())
                            + _and + URLHelper.SecureParameters(_index, _zero) + _and + URLHelper.SecureParameters(_pageType, QueryStringConstants.MRFSUMMARY)
                            + _and + URLHelper.SecureParameters(_isAccessUrl,QueryStringConstants.YES) + _and + URLHelper.CreateSignature(MrfId.ToString(), _zero, QueryStringConstants.MRFSUMMARY, QueryStringConstants.YES);
                    }
                }
                // Mail raised to Recruitment team when Purpose option is Market Research Feasibilty in case of raise head count.
                if (mrfStatus == Convert.ToInt16(EnumsConstants.EmailFunctionality.RaiseHeadCountToRecruitmentTeam))
                {
                    sComp = Utility.GetUrl() + PageName + _questionMark + URLHelper.SecureParameters(CommonConstants.CON_QSTRING_AllocateResourec, _true)
                    + _and + URLHelper.SecureParameters(CommonConstants.CON_QSTRING_MRFID, MrfId.ToString());
                    //+_and + URLHelper.SecureParameters(CommonConstants.CLIENT_NAME, mrfDetail.ClientName);
                    //+ _and + URLHelper.SecureParameters(_index, _zero) + _and + URLHelper.SecureParameters(_pageType, QueryStringConstants.MRFPENDINGALLOCATION)
                    //+ _and + URLHelper.CreateSignature(_true, MrfId.ToString(), mrfDetail.ClientName, _zero, QueryStringConstants.MRFPENDINGALLOCATION);
                }

                return sComp;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, MRFDETAIL, "GetEmailLink", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }


        /// <summary>
        /// Send a mail for multiple head count raised from pending allocation screen.
        /// </summary>
        /// <param name="mrfDetail"></param>
        /// <param name="newMrfId"></param>
        public void SendMailForRaiseHeadCountWithouApprovalInGroup(BusinessEntities.MRFDetail mrfDetail, string[] newMrfId)
        {
            try
            {
                AuthorizationManager objAuMan = new AuthorizationManager();
                Utility objUtility = new Utility();
                string mailFrom = objAuMan.getLoggedInUserEmailId();

                int RecruiterId = Convert.ToInt32(mrfDetail.RecruitersId);

                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),

                                            Convert.ToInt16(EnumsConstants.EmailFunctionality.RaiseHeadCountWithOutApprovalInGroup));

                obj.To.Add(GetEmailIdByEmpId(RecruiterId));

                // obj.Subject = string.Format(obj.Subject, mrfDetail.MRFCode);
                obj.Body = string.Format(obj.Body, objUtility.GetEmployeeFirstName(GetEmailIdByEmpId(RecruiterId)),
                                                   mrfDetail.Role,
                                                   mrfDetail.DepartmentName,
                                                   GetHTMLStringForMailInGroupForRaiseHeadCount(newMrfId),
                    //Convert.ToDateTime(mrfDetail.ExpectedClosureDate).ToShortDateString(),
                                                   (Utility.GetUrl() + CommonConstants.Page_MrfSummary));

                obj.SendEmail(obj);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.PresentationLayer, MRFDETAIL, "SendMailForRaiseHeadCountWithouApprovalInGroup", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Create thye HTMl table for mail of raise multiple head count.
        /// </summary>
        /// <param name="newMrfId"></param>
        /// <returns></returns>
        private string GetHTMLStringForMailInGroupForRaiseHeadCount(string[] newMrfId)
        {
            string bodyTable = string.Empty;
            string strMRFCode = string.Empty;
            string strExpectedClosureDate = string.Empty;

            string[] header = new string[3];
            header[0] = "Sr. No.";
            header[1] = "MRF Code";
            header[2] = "Expected Closure Date";

            string[,] arrayData = new string[(newMrfId.Length), 3];

            int rowCounter = 0;
            for (int i = 0; i < newMrfId.Length; i++)
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new RaveHRCollection();

                raveHRCollection = this.CopyMRFBL(Convert.ToInt32(newMrfId[i]));
                foreach (BusinessEntities.MRFDetail mrfDetails in raveHRCollection)
                {
                    strMRFCode = mrfDetails.MRFCode;
                    strExpectedClosureDate = Convert.ToDateTime(mrfDetails.ExpectedClosureDate).ToShortDateString();
                }

                arrayData[rowCounter, 0] = (rowCounter + 1).ToString();
                arrayData[rowCounter, 1] = strMRFCode;
                arrayData[rowCounter, 2] = strExpectedClosureDate;

                rowCounter++;
            }

            IEmailTableData objEmailTableData = new EmailTableData();
            objEmailTableData.Header = header;
            objEmailTableData.RowDetail = arrayData;
            objEmailTableData.RowCount = newMrfId.Length;

            bodyTable += objEmailTableData.GetTableData(objEmailTableData);
            return bodyTable;
        }


        #endregion

        /// <summary>
        /// Gets the candidate by MRFID.
        /// </summary>
        /// <param name="mrfDetail">The MRF detail.</param>
        /// <returns></returns>
        public string GetCandidateByMRFID(BusinessEntities.MRFDetail mrfDetail)
        {
            Rave.HR.DataAccessLayer.MRF.MRFDetail mrfDetailsObj = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
            return mrfDetailsObj.GetCandidateByMRFID(mrfDetail);
        }

        /// <summary>
        /// Gets the employee by MRFID.
        /// </summary>
        /// <param name="mrfDetail">The MRF detail.</param>
        /// <returns></returns>
        public RaveHRCollection GetEmployeeByMRFID(BusinessEntities.MRFDetail mrfDetail)
        {
            Rave.HR.DataAccessLayer.MRF.MRFDetail mrfDetailsObj = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
            return mrfDetailsObj.GetEmployeeByMRFID(mrfDetail);
        }


        /// <summary>
        /// Get the employee type by empid.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public int GetEmployeeTypeId(int employeeId)
        {
            Rave.HR.DataAccessLayer.MRF.MRFDetail mrfDetailsObj = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
            return mrfDetailsObj.GetEmployeeTypeId(employeeId);
        }

        /// <summary>
        /// Get Allocated Cost Code Name type by EmployeeId,ProjectId.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public string GET_EmployeeAllocation_CostCode(int employeeId,int ProjectId,int CostCodeId)
        {
            Rave.HR.DataAccessLayer.MRF.MRFDetail mrfDetailsObj = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
            return mrfDetailsObj.GetEmployee_ProjectAllocation(employeeId,ProjectId,CostCodeId);
        }


        /// <summary>
        /// Checks whether Employee exists for this Project or not.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public int GetEmployeeExistCheck(BusinessEntities.MRFDetail mrfDetail)
        {
            Rave.HR.DataAccessLayer.MRF.MRFDetail mrfDetailsObj = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
            return mrfDetailsObj.GetEmployeeExistCheck(mrfDetail);
        }

        /// <summary>
        /// Get Department Head by EmailId and DeptId
        /// </summary>
        public RaveHRCollection GetDepartmentHeadByEmaiIdAndDeptId(string loginUserEmailId)
        {
            Rave.HR.DataAccessLayer.MRF.MRFDetail mrfDetailsObj = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
            return mrfDetailsObj.GetDepartmentHeadByEmaiIdAndDeptId(loginUserEmailId);
        }

        /// <summary>
        /// Check whether SLA role exist or not.
        /// </summary> 
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public bool CheckSLARoleExist(int roleId)
        {
            Rave.HR.DataAccessLayer.MRF.MRFDetail mrfDetailsBL = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
            return mrfDetailsBL.CheckSLARoleExist(roleId);
        }

        /// <summary>
        /// Gets the expected closure date.
        /// </summary>
        /// <param name="mrfDetail">The MRF detail.</param>
        /// <returns></returns>
        public DateTime GetExpectedClosureDate(BusinessEntities.MRFDetail mrfDetail)
        {
            DateTime ExpectedClosureDate;
            try
            {
                Rave.HR.DataAccessLayer.MRF.MRFDetail objDLLMRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();

                ExpectedClosureDate = objDLLMRFDetail.GetExpectedClosureDate(mrfDetail);

                return ExpectedClosureDate;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, "GetExpectedClosureDate", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Sends the E mail raise head count with out approval.
        /// </summary>
        /// <param name="mrfDetail">The MRF detail.</param>
        /// <param name="IsRecruiterReassigned">if set to <c>true</c> [is recruiter reassigned].</param>
        private void SendEMailRaiseHeadCountWithOutApproval(BusinessEntities.MRFDetail mrfDetail, Boolean IsRecruiterReassigned)
        {
            AuthorizationManager objAuMan = new AuthorizationManager();
            Utility objUtility = new Utility();
            string mailFrom = objAuMan.getLoggedInUserEmailId();
            int RecruiterId = Convert.ToInt32(mrfDetail.RecruitersId);

            IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),

                                        Convert.ToInt16(EnumsConstants.EmailFunctionality.RaiseHeadCountWithOutApproval));


            if (mrfDetail.DepartmentName.Contains("RaveConsultant") || IsRecruiterReassigned == true)
            {
                obj.To.Insert(0, GetEmailIdByEmpId(RecruiterId));
            }
            else
            {
                obj.To.Insert(0, mrfDetail.EmailId);
            }
            obj.Subject = string.Format(obj.Subject, mrfDetail.MRFCode);
            obj.Body = string.Format(obj.Body, objUtility.GetEmployeeFirstName(obj.To[0].ToString()),
                                               mrfDetail.Role,
                                               mrfDetail.DepartmentName,
                                               this.GetExpectedClosureDate(mrfDetail).ToShortDateString(),
                                               GetEmailLink(CommonConstants.Page_MrfView, mrfDetail, Convert.ToInt16(EnumsConstants.EmailFunctionality.RaiseHeadCountWithOutApproval), mrfDetail.MRFId));

            obj.SendEmail(obj);
        }


        //Vandana-Start
        public void DeleteBLFutureAllocateEmployee(int mrfid)
        {
            try
            {
                Rave.HR.DataAccessLayer.MRF.MRFDetail objBLDeleteFutureAllocateEmployee = new Rave.HR.DataAccessLayer.MRF.MRFDetail();

                objBLDeleteFutureAllocateEmployee.DeleteDLFutureAllocateEmployee(mrfid);

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, "GetExpectedClosureDate", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }
        //Vandana-End



        //Vandana-Start
        public void EditBLFutureAllocationEmployee(int mrftempid,
                                                 string lint_type_of_allocation,
                                                 string lint_type_of_supply,
                                                 DateTime ldt_date_of_Allocation,
                                                 int lstr_employeeeID)
        {
            try
            {
                Rave.HR.DataAccessLayer.MRF.MRFDetail objBLEditFutureAllocateEmployee =
                    new Rave.HR.DataAccessLayer.MRF.MRFDetail();

                objBLEditFutureAllocateEmployee.EditDLFutureAllocateEmployee(mrftempid,
                                                                            lint_type_of_allocation,
                                                                            lint_type_of_supply,
                                                                            ldt_date_of_Allocation,
                                                                            lstr_employeeeID);

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer,
                    MRFDETAIL, "EditFutureAllocateEmployee", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }
        //Vandana-End



        /// <summary>
        /// Sends the E mail MRF status changed.
        /// </summary>
        /// <param name="mrfDetail">The MRF detail.</param>
        private void SendEMailMRFStatusChanged(BusinessEntities.MRFDetail mrfDetail)
        {
            AuthorizationManager objAuMan = new AuthorizationManager();
            Utility objUtility = new Utility();
            string mailFrom = objAuMan.getLoggedInUserEmailId();
            string emaildRecruitment = string.Empty;
            int RecruiterId = 0;

            if (mrfDetail.RecruitersId != CommonConstants.ZERO.ToString())
                RecruiterId = Convert.ToInt32(mrfDetail.RecruitersId);

            DataAccessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
            emaildRecruitment = mRFDetail.GetEmployeeExistCheck();

            IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),

                                        Convert.ToInt16(EnumsConstants.EmailFunctionality.MRFStatusChange));


            obj.To.Insert(0, GetEmailIdByEmpId(RecruiterId));

            //add recruitement heads email Ids into CC.
            obj.CC.Add(emaildRecruitment.ToString());

            if (!string.IsNullOrEmpty(mrfDetail.ClientName))
            {
                obj.Subject = string.Format(obj.Subject, mrfDetail.MRFCode
                                                       , mrfDetail.ClientName
                                                       , (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? _project : _department
                                                       , (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? mrfDetail.ProjectName : mrfDetail.DepartmentName);
            }
            else
            {
                obj.Subject = string.Format(obj.Subject.Replace(", Client Name:", string.Empty)
                                                         , mrfDetail.MRFCode
                                                         , string.Empty
                                                         , (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? _project : _department
                                                         , (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? mrfDetail.ProjectName : mrfDetail.DepartmentName);
            }

            if (!string.IsNullOrEmpty(mrfDetail.ClientName))
            {
                obj.Body = string.Format(obj.Body, objUtility.GetEmployeeFirstName(obj.To[0].ToString()),
                                                    mrfDetail.MRFCode,
                                                    mrfDetail.ClientName == string.Empty ? obj.Subject.Replace("Client", string.Empty) : mrfDetail.ClientName,
                                                   (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? _project : _department,
                                                   (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? mrfDetail.ProjectName : mrfDetail.DepartmentName);
            }

            else
            {
                obj.Body = string.Format(obj.Body.Replace("for client ", string.Empty), objUtility.GetEmployeeFirstName(obj.To[0].ToString()),
                                                    mrfDetail.MRFCode,
                                                    string.Empty,
                                                   (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? _project : _department,
                                                   (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? mrfDetail.ProjectName : mrfDetail.DepartmentName);
            }
            obj.SendEmail(obj);
        }


        /// <summary>
        /// Sends the E mail MRF status changed.
        /// </summary>
        /// <param name="mrfDetail">The MRF detail.</param>
        private void SendEMailMRFStatusChangedToClosed(BusinessEntities.MRFDetail mrfDetail)
        {
            AuthorizationManager objAuMan = new AuthorizationManager();
            Utility objUtility = new Utility();
            string mailFrom = objAuMan.getLoggedInUserEmailId();
            string emaildRecruitment = string.Empty;
            int RecruiterId = 0;

            if (mrfDetail.RecruitersId != CommonConstants.ZERO.ToString())
                RecruiterId = Convert.ToInt32(mrfDetail.RecruitersId);

            DataAccessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
            emaildRecruitment = mRFDetail.GetEmployeeExistCheck();

            IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),

                                        Convert.ToInt16(EnumsConstants.EmailFunctionality.MRFStatusChangetoClosed));


            obj.To.Insert(0, GetEmailIdByEmpId(RecruiterId));

            //add recruitement heads email Ids into CC.
            obj.CC.Add(emaildRecruitment.ToString());

            if (!string.IsNullOrEmpty(mrfDetail.ClientName))
            {
                obj.Subject = string.Format(obj.Subject, mrfDetail.MRFCode
                                                       , mrfDetail.ClientName
                                                       , (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? _project : _department
                                                       , (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? mrfDetail.ProjectName : mrfDetail.DepartmentName
                                                       );
            }
            else
            {
                obj.Subject = string.Format(obj.Subject.Replace(", Client Name:", string.Empty)
                                                         , mrfDetail.MRFCode
                                                         , string.Empty
                                                         , (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? _project : _department
                                                         , (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? mrfDetail.ProjectName : mrfDetail.DepartmentName);
            }

            string mailAddress = mailFrom;
            //GoogleConfugrable
            mailAddress = objAuMan.GetUsernameBasedOnEmail(mailAddress);
            mailAddress = objAuMan.GetDomainUserName(mailAddress);

            //if (mailAddress.ToLower().Contains(RAVEDOMAIN))
            //{
            //    mailAddress = mailAddress.Replace(RAVEDOMAIN, "");
            //    mailAddress = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mailAddress.Replace(".", " "));
                
            //}
            //else
            //{
            //    mailAddress = mailAddress.Replace(NOTHGATEDOMAIN, "");
            //    mailAddress = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(mailAddress.Replace(".", " "));
            //}

            if (!string.IsNullOrEmpty(mrfDetail.ClientName))
            {
                obj.Body = string.Format(obj.Body, objUtility.GetEmployeeFirstName(obj.To[0].ToString()),
                                                    mrfDetail.MRFCode,
                                                    mrfDetail.ClientName == string.Empty ? obj.Subject.Replace("Client", string.Empty) : mrfDetail.ClientName,
                                                   (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? _project : _department,
                                                   (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? mrfDetail.ProjectName : mrfDetail.DepartmentName,
                                                    //objAuMan.GetDomainUserName(mailFrom.Replace(RAVEDOMAIN, ""))
                                                   // objAuMan.GetDomainUserName(mailAddress)
                                                   mailAddress);
            }

            else
            {
                obj.Body = string.Format(obj.Body.Replace("for client ", string.Empty), objUtility.GetEmployeeFirstName(obj.To[0].ToString()),
                                                    mrfDetail.MRFCode,
                                                    string.Empty,
                                                   (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? _project : _department,
                                                   (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? mrfDetail.ProjectName : mrfDetail.DepartmentName,
                                                    //objAuMan.GetDomainUserName(mailFrom.Replace(RAVEDOMAIN, ""))
                                                    //objAuMan.GetDomainUserName(mailAddress)
                                                    mailAddress);
            }
            obj.SendEmail(obj);
        }

        /// <summary>
        /// Get the mrf
        /// </summary>
        /// <param name="MrfId"></param>
        /// <returns></returns>
        public BusinessEntities.MRFDetail GetMrfMoveDetail(int MrfId)
        {
            BusinessEntities.MRFDetail mrfDetails = new BusinessEntities.MRFDetail();
            Rave.HR.DataAccessLayer.MRF.MRFDetail objDLLMRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
            try
            {
                mrfDetails = objDLLMRFDetail.GetMrfDetailForMove(MrfId);
                return mrfDetails;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, "GetExpectedClosureDate", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }



        //
        public BusinessEntities.RaveHRCollection FillDepartmentProjectNamesDropDownBL(ParameterCriteria paramCriteria)
        {
            try
            {
                BusinessEntities.RaveHRCollection returnobj = new RaveHRCollection();
                Rave.HR.DataAccessLayer.MRF.MRFDetail objFillDepartmentProjectNamesDropDownBL =
                new Rave.HR.DataAccessLayer.MRF.MRFDetail();
                returnobj = objFillDepartmentProjectNamesDropDownBL.FillDepartmentProjectNamesDropDownDL(paramCriteria);
                
                //return the Collection
                return returnobj ;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
        }
        //
        public BusinessEntities.RaveHRCollection FillMRFCodeBL(string DeptProjectName, int DeptProjectMRFRoleID, ParameterCriteria paramCriteria)
        {
            try
            {
                BusinessEntities.RaveHRCollection returnobj = new RaveHRCollection();
                Rave.HR.DataAccessLayer.MRF.MRFDetail objFillMRFCodeBL = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
                returnobj = objFillMRFCodeBL.FillMRFCodeDL(DeptProjectName, DeptProjectMRFRoleID, paramCriteria);
                

                //return the Collection
                return returnobj;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
        }


        public BusinessEntities.RaveHRCollection FillMRFRoleBL(string DeptProjectName)
        {
            try
            {
                BusinessEntities.RaveHRCollection returnobj = new RaveHRCollection();
                Rave.HR.DataAccessLayer.MRF.MRFDetail objFillMRFRoleBL = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
                returnobj = objFillMRFRoleBL.FillMRFRoleDL(DeptProjectName);


                //return the Collection
                return returnobj;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Function will Send mail.
        /// </summary>
        /// <param name="mrfDetailobj"></param>
        /// <returns></returns>
        // Mahendra change Issue Id : 33960
        // Changed following from Private to Public
        /// Private void SendMailDeleteMRF(BusinessEntities.MRFDetail mrfDetailobj, int OldMRFStatusId) 
        public void SendMailDeleteMRF(BusinessEntities.MRFDetail mrfDetailobj, int OldMRFStatusId)
        {
            IRMSEmail rmsEmail = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),

                                           Convert.ToInt16(EnumsConstants.EmailFunctionality.DeleteMRF));
            DataAccessLayer.Contracts.Contract objDataAccessContract = new Rave.HR.DataAccessLayer.Contracts.Contract();

            rmsEmail.From = objAuMan.getLoggedInUserEmailId();

            //Issue ID : 41198 Mahendra Start 18 Mar 2013 START
            //rmsEmail.To.Add(objDataAccessContract.GetEmailID(mrfDetailobj.RaisedBy));
            ////rmsEmail.To.Add(objDataAccessContract.GetEmailID(mrfDetailobj.CreatedById));
            // Rajan Kumar : Issue 45222: 30/01/2014 : Starts                        			 
            // Desc : Rashwin is released from the project but the mail is still going to him since he has raised the MRF.
            if (mrfDetailobj.PMExistForProject)
            {
                rmsEmail.To.Add(objDataAccessContract.GetEmailID(mrfDetailobj.CreatedById));
            }
            else
            {
                rmsEmail.To.Add(CommonConstants.EmailIdOfRMOGroup);
            }


            //Siddharth 7th Dec 2015 Start
            //MRF deleted mail should go to all to whom MRF raised mail is sent.
            if (!string.IsNullOrEmpty(mrfDetailobj.ReportingToId))
            {
                 string AccountableEmailTo = "";
                 AccountableEmailTo = new Rave.HR.BusinessLayer.Contracts.Contract().GetEmailID(Convert.ToInt32(mrfDetailobj.ReportingToId));
                 rmsEmail.CC.Add(AccountableEmailTo);
            }
            //Siddharth 7th Dec 2015 End


            // Rajan Kumar : Issue 45222: 30/01/2014 : END
            //Issue ID : 41198 Mahendra Start 18 Mar 2013 END
            #region (Email should be cc, if MRF status is Pending External allocation)
            // Initialise the Collection class object
            BusinessEntities.RaveHRCollection raveHRCollectionDepartment = new RaveHRCollection();
            BusinessEntities.Recruitment objRecruitment = new BusinessEntities.Recruitment();
            
            //Gets department heads collection
            objRecruitment.DepartmentId = Convert.ToInt32(MasterEnum.Departments.Recruitment);
            raveHRCollectionDepartment = DataAccessLayer.Recruitment.Recruitment.GetDepartmentHeadByDepartmentId(objRecruitment);

            // Mohamed : Issue 41487 : 24/04/2013 : Starts                        			  
            // Desc : respective PM is not copied in MRF deleted mails if the MRF was not raised by him
            if (OldMRFStatusId != Convert.ToInt32(MasterEnum.MRFStatus.PendingAllocation))
            {
                BusinessEntities.Projects ObjProject = new BusinessEntities.Projects();
                ObjProject.ProjectId = mrfDetailobj.ProjectId;
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                raveHRCollection = DataAccessLayer.Projects.Projects.GetProjectManagerByProjectId(ObjProject);
                string projectManagerEmail = string.Empty;

                foreach (BusinessEntities.Projects objProj in raveHRCollection)
                {
                    projectManagerEmail += objProj.EmailIdOfPM;
                    //projectManagerEmail += ",";
                    mrfDetailobj.ReportingToId = projectManagerEmail;

                    if (mrfDetailobj.ReportingToId.EndsWith(","))
                    {
                        mrfDetailobj.ReportingToId = mrfDetailobj.ReportingToId.Substring(0, mrfDetailobj.ReportingToId.Length - 1);
                    }
                }

                //if (mrfDetailobj.ReportingToId != null && mrfDetailobj.ReportingToId != "")
                //{
                //    string[] PmEmailId = mrfDetailobj.ReportingToId.Split(',');
                //    for (int i = 0; i < PmEmailId.Length; i++)
                //    {
                //        rmsEmail.CC.Add(PmEmailId[i].ToString());
                //    }
                //}
            }
            // Mohamed : Issue 41487 : 24/04/2013 : Ends

            string departmentHeadEmail = string.Empty;
            if (OldMRFStatusId == Convert.ToInt32(MasterEnum.MRFStatus.PendingExternalAllocation))
            {
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

                    rmsEmail.CC.Add(objDataAccessContract.GetEmailID(mrfDetailobj.RecruitmentManager));
                    rmsEmail.CC.Add(objRecruitment.EmailId);
                }
            }
            #endregion 

            if (mrfDetailobj.ProjectName.ToString() != "")
            {
                rmsEmail.Subject = string.Format(rmsEmail.Subject, mrfDetailobj.MRFCode);
                int len = rmsEmail.Subject.Length;
                rmsEmail.Subject = rmsEmail.Subject.ToString().Substring(0, len - 1) + ", Project Name:" + mrfDetailobj.ProjectName + "]";
            }
            //rmsEmail.Subject = string.Format(rmsEmail.Subject, mrfDetailobj.MRFCode, mrfDetailobj.ClientName, mrfDetailobj.ProjectName);
            else
                rmsEmail.Subject = string.Format(rmsEmail.Subject, mrfDetailobj.MRFCode);

            string isDepartment = string.Empty;

            string deptProjName = string.Empty;

            if ((mrfDetailobj.ProjectName == "") || (mrfDetailobj.ProjectName == null))
            {
                isDepartment = "Department";
                deptProjName = mrfDetailobj.DepartmentName;
            }
            else
            {
                isDepartment = "Project";
                deptProjName = mrfDetailobj.ProjectName;
            }

            string role = string.Empty;
            if (mrfDetailobj.Role != null)
            {
                role = "For [" + mrfDetailobj.Role + "]";
            }
            else
            {
                role = "";
            }

            //string loggedInUser = objAuMan.GetDomainUserName(objAuMan.getLoggedInUser().Replace(DOMAIN, string.Empty));
            string loggedInUser = "";
            loggedInUser = objAuMan.getLoggedInUser();
            //GoogleMail
            //if (loggedInUser.ToLower().Contains(DOMAIN))
            //{
            //    loggedInUser = objAuMan.GetDomainUserName(loggedInUser.Replace(DOMAIN, string.Empty));
            //    loggedInUser = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(loggedInUser.Replace(".", " "));
            //}
            //else
            //{
            //    loggedInUser = objAuMan.GetDomainUserName(loggedInUser.Replace(AuthorizationManagerConstants.NORTHGATEDOMAIN, string.Empty));
            //    loggedInUser = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(loggedInUser.Replace(".", " "));
            //}

            loggedInUser = objAuMan.GetDomainUserName(loggedInUser);
            // Rajan Kumar :Issue 49361: 21/02/2014 : Starts                              
            // Desc : Mail is addressed to Prerna although she is inactive. 
            if (mrfDetailobj.EmployeeExist)
            {
                if (mrfDetailobj.PMExistForProject)
                {
                    rmsEmail.Body = string.Format(rmsEmail.Body, mrfDetailobj.RaisedBy, isDepartment, deptProjName, role, loggedInUser);
                }
                else
                {
                    rmsEmail.Body = string.Format(rmsEmail.Body, RMOTeam, isDepartment, deptProjName, role, loggedInUser);
                }
            }
            else
            {
                rmsEmail.Body = string.Format(rmsEmail.Body, RMOTeam, isDepartment, deptProjName, role, loggedInUser);
            }
            rmsEmail.SendEmail(rmsEmail);
            // Rajan Kumar :Issue 49361: 21/02/2014 : END
        }     

        #region Coded By Anuj
        //Issue ID: 20374
        //START
        /// <summary>
        /// Function will delete.
        /// </summary>
        /// <param name="mrfDetailobj"></param>
        /// <returns></returns>
        public void DeleteMRFBL(BusinessEntities.MRFDetail MRFDetaildeleteobj,int OldMRFStatusId)
        {
            int mrfDid;
            try
            {
                DataAccessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
                BusinessEntities.MRFDetail MRFDetaildeleteobjforMail = new BusinessEntities.MRFDetail();
                mrfDid = MRFDetaildeleteobj.MRFId;
                MRFDetaildeleteobjforMail = mRFDetail.GetMRFDetails(mrfDid);

                SendMailDeleteMRF(MRFDetaildeleteobjforMail, OldMRFStatusId);
                mRFDetail.DeleteMRFDL(MRFDetaildeleteobj);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, DELETEMRFBL, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }
        }
        //END
        #endregion Coded By Anuj


      #region code by Ambar
        // 27642-Ambar-Start

        /// <summary>
        /// Sends the E mail MRF status changed.
        /// </summary>
        /// <param name="mrfDetail">The MRF detail.</param>
        
        private void SendEMailRejectedMRFStatusChanged(BusinessEntities.MRFDetail mrfDetail)
        {
          AuthorizationManager objAuMan = new AuthorizationManager();
          Utility objUtility = new Utility();
          string mailFrom = objAuMan.getLoggedInUserEmailId();
          string emaildRecruitment = string.Empty;
          int RecruiterId = 0;

          if (mrfDetail.RecruitersId != CommonConstants.ZERO.ToString())
            RecruiterId = Convert.ToInt32(mrfDetail.RecruitersId);

          DataAccessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
          emaildRecruitment = mRFDetail.GetEmployeeExistCheck();

          IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),

                                      Convert.ToInt16(EnumsConstants.EmailFunctionality.RejectedMRFStatusChange));


          // 31247-Ambar-Start-28112011
          // Commented following code to Remove Recruitments Name for Rejected MRF
          //obj.To.Insert(0, GetEmailIdByEmpId(RecruiterId));

          ////add recruitement heads email Ids into CC.
          //obj.CC.Add(emaildRecruitment.ToString());
          // 31247-Ambar-Start-28112011-End

          if (!string.IsNullOrEmpty(mrfDetail.ClientName))
          {
            obj.Subject = string.Format(obj.Subject, mrfDetail.MRFCode
                                                   , mrfDetail.ClientName
                                                   , (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? _project : _department
                                                   , (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? mrfDetail.ProjectName : mrfDetail.DepartmentName);
          }
          else
          {
            obj.Subject = string.Format(obj.Subject.Replace(", Client Name:", string.Empty)
                                                     , mrfDetail.MRFCode
                                                     , string.Empty
                                                     , (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? _project : _department
                                                     , (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? mrfDetail.ProjectName : mrfDetail.DepartmentName);
          }

          if (!string.IsNullOrEmpty(mrfDetail.ClientName))
          {
            obj.Body = string.Format(obj.Body, objUtility.GetEmployeeFirstName(obj.To[0].ToString()),
                                                mrfDetail.MRFCode,
                                                mrfDetail.ClientName == string.Empty ? obj.Subject.Replace("Client", string.Empty) : mrfDetail.ClientName,
                                               (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? _project : _department,
                                               (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? mrfDetail.ProjectName : mrfDetail.DepartmentName);
          }

          else
          {
            obj.Body = string.Format(obj.Body.Replace("for client ", string.Empty), objUtility.GetEmployeeFirstName(obj.To[0].ToString()),
                                                mrfDetail.MRFCode,
                                                string.Empty,
                                               (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? _project : _department,
                                               (mrfDetail.DepartmentName == MasterEnum.Departments.Projects.ToString()) ? mrfDetail.ProjectName : mrfDetail.DepartmentName);
          }
          obj.SendEmail(obj);
        }

      // 27642-Ambar-End
      #endregion code by Ambar

        //Aarohi : Issue 31838(CR) : 28/12/2011 : Start
        public int GetMRFId(string MRFCode)
        {
            try
            {
                int MRFId;
                Rave.HR.DataAccessLayer.MRF.MRFDetail objGetMRFIdBL = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
                MRFId = objGetMRFIdBL.GetMRFId(MRFCode);
                
                //return the Collection
                return MRFId;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
        }
        //Aarohi : Issue 31838(CR) : 28/12/2011 : End

        #region Modified By Mohamed Dangra
        // Mohamed : Issue 51824  : 19/08/2014 : Starts                        			  
        // Desc : Provision in RMS system for a notification to be sent when an MRf is assigned  from one recruiter to another.
        public void SendMailMRFAssignedFromOneRecruiterToAnother(string NewRecruiterEmailId, string OldRecruiterEmailId, string NewRecruiterName, string MRFCode, string Role
                                                                    ,string OldRecruiterName,string RequiredDate, string ProjectName)
        {
            IRMSEmail rmsEmail = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.MRF),
                                           Convert.ToInt16(EnumsConstants.EmailFunctionality.MRFAssignedFromOneRecruiterToAnother));

            rmsEmail.To.Insert(0, NewRecruiterEmailId);

            //add recruitement heads email Ids into CC.
            rmsEmail.CC.Add(OldRecruiterEmailId);

            rmsEmail.Subject = string.Format(rmsEmail.Subject,MRFCode, NewRecruiterName
                                            );
            if (ProjectName != CommonConstants.SELECT)
            {
                rmsEmail.Body = string.Format(rmsEmail.Body
                                              , NewRecruiterName
                                              , Role
                                              , "in [" + ProjectName + "]"
                                              , OldRecruiterName
                                              , RequiredDate
                                                );
            }
            else
            {
                rmsEmail.Body = string.Format(rmsEmail.Body
                                              , NewRecruiterName
                                              , Role
                                              , string.Empty
                                              , OldRecruiterName, RequiredDate);
            }

            rmsEmail.SendEmail(rmsEmail);
            
        }

        // Mohamed : Issue 51824  : 19/08/2014 : Ends
        #endregion Modified By Mohamed Dangra

        // Ishwar : NISRMS : 16022015 Start
        public DataSet GetMRFAging(string SortExpressionAndDirection)
        {
            DataSet MRFAging = null;
            try
            {
                MRFAging = new DataSet();
                Rave.HR.DataAccessLayer.MRF.MRFDetail objMRFAging = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
                //Siddharth 27 March 2015 Start
                MRFAging = objMRFAging.GetMRFAging(SortExpressionAndDirection);
                //Siddharth 27 March 2015 End
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, "GetMRFAging", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }

            return MRFAging;
        }

        public DataSet GetMRFAgingForOpenPosition(string SortExpressionAndDirection)
        {
            DataSet MRFAgingForOpenPosition = null;
            try
            {
                MRFAgingForOpenPosition = new DataSet();
                Rave.HR.DataAccessLayer.MRF.MRFDetail objMRFAgingForOpenPosition = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
                //Siddharth 27 March 2015 Start
                MRFAgingForOpenPosition = objMRFAgingForOpenPosition.GetMRFAgingForOpenPosition(SortExpressionAndDirection);
                //Siddharth 27 March 2015 End
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, "GetMRFAgingForOpenPosition", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }

            return MRFAgingForOpenPosition;
        }
        // Ishwar : NISRMS : 16022015 End

        //Ishwar Patil 21042015 Start
        public BusinessEntities.RaveHRCollection GetSkillsList(string SkillsName)
        {
            BusinessEntities.RaveHRCollection SkillsList = null;
            try
            {
                SkillsList = new BusinessEntities.RaveHRCollection();
                Rave.HR.DataAccessLayer.MRF.MRFDetail objSkillsListDAL = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
                SkillsList = objSkillsListDAL.GetSkillsList(SkillsName);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, MRFDETAIL, "GetEmployeesList", EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }

            return SkillsList;
        }
        //Ishwar Patil 21042015 End
    }
}
