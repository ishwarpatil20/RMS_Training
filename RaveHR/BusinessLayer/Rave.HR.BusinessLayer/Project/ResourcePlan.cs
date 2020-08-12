//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           ResourcePlan.cs       
//  Author:         prashant.mala
//  Date written:   01/09/2009/ 6:41:30 PM
//  Description:    This class provides the Buisness layer methods for Resource Plan.
//                  
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  4/8/2009 5:30:30 PM  prashant.mala    n/a     Created    
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using Rave.HR;
using Common;
using System.Data;
using System.Web;
using System.Collections;
using Rave.HR.BusinessLayer.Interface;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using Common.AuthorizationManager;
using System.Transactions;
using Common.Constants;
using System.IO;

namespace Rave.HR.BusinessLayer.Projects
{
    public class ResourcePlan
    {

        #region Private Field Members

        //define CLASS_NAME_RP
        private const string CLASS_NAME_RP = "ResourcePlan.cs";


        // define Constant
        private const string RESOURCEPLAN = "ResourcePlan";
        /// <summary>
        /// Header Style
        /// </summary>
        private const string strHeaderStyle = "height: 25px;background-color: #BFBFBF;font-size: 9pt;font-weight: bold;padding-left: 5px;font-family:Verdana;vertical-align:top;";

        /// <summary>
        /// Define Resource plan id
        /// </summary>
        private int ResourcePlanID = 0;

        /// <summary>
        /// Rave Email domain
        /// </summary>
        /// 
        //Googleconfigurable
        //private const string RAVE_DOMAIN = "@rave-tech.com";

        /// <summary>
        /// northgate Email domain
        /// </summary>
        /// 
        //Googleconfigurable
        //private const string NORTHGATE_DOMAIN = "@northgateps.com";

        /// <summary>
        /// Define Resource plan duration id
        /// </summary>
        private int ResourcePlanDurationID = 0;


        /// <summary>
        /// Row Style
        /// </summary>
        private const string strRowStyle = "font-family: Verdana;	font-size: 9pt;	padding-left: 5px;vertical-align:top;";

        /// <summary>
        /// Roles Style
        /// </summary>
        private const string strRoleStyle = "font-family: Verdana;font-size: 9pt;	padding-left: 5px;background-color:#F3F781;vertical-align:top;";

        /// <summary>
        /// Utilization Row Style
        /// </summary>
        private const string strUtilizationRowStyle = "font-family: Verdana;font-size: 9pt;	padding-left: 5px;background-color:#CEF6CE;vertical-align:top;";

        #endregion Private Field Members

        //assign mode value
        public enum Mode
        {
            View,
            Update,
            IsDeleted
        }

        /// <summary>
        /// Declare Rave.HR.DataAccessLayer.Projects.ResourcePlan object
        /// </summary>
        Rave.HR.DataAccessLayer.Projects.ResourcePlan objDALResourcePlan = null;

        /// <summary>
        /// Add Resource Plan
        /// </summary>
        public void AddResourcePlan(BusinessEntities.ResourcePlan objBEResourcePlan, ref int ResourcePlanId)
        {
            objDALResourcePlan = new DataAccessLayer.Projects.ResourcePlan();
            objDALResourcePlan.AddResourcePlan(objBEResourcePlan, ref ResourcePlanId);
        }

        /// <summary>
        /// Add Resource Plan duration 
        /// </summary>
        public void AddRPDuration(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();
            objDALResourcePlan.AddRPDuration(objBEResourcePlan);
        }

        /// <summary>
        /// Add Resource Plan duration details
        /// </summary>
        public void AddRPDurationDetail(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();
            objDALResourcePlan.AddRPDurationDetail(objBEResourcePlan);
        }

        /// <summary>
        /// Get resource plan duration
        /// </summary>
        public RaveHRCollection GetRPDuration(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            objDALResourcePlan = new DataAccessLayer.Projects.ResourcePlan();
            return objDALResourcePlan.GetRPDuration(objBEResourcePlan);
        }

        /// <summary>
        /// Get resource plan duration detail
        /// </summary>
        public RaveHRCollection GetRPDurationDetail(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            objDALResourcePlan = new DataAccessLayer.Projects.ResourcePlan();
            return objDALResourcePlan.GetRPDurationDetail(objBEResourcePlan);
        }

        /// <summary>
        /// Get inactive resource plan for the project
        /// </summary>
        public RaveHRCollection GetInactiveResourcePlan(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            objDALResourcePlan = new DataAccessLayer.Projects.ResourcePlan();
            return objDALResourcePlan.GetInactiveResourcePlan(objBEResourcePlan);
        }

        /// <summary>
        /// Get inactive resource plan duration detail for the resource plan
        /// </summary>
        public RaveHRCollection GetInactiveRPDurationDetail(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            objDALResourcePlan = new DataAccessLayer.Projects.ResourcePlan();
            return objDALResourcePlan.GetInactiveRPDurationDetail(objBEResourcePlan);
        }

        /// <summary>
        /// Delete resource plan duration
        /// </summary>
        public void DeleteRPDuration(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();
            objDALResourcePlan.DeleteRPDuration(objBEResourcePlan);
        }

        /// <summary>
        /// Delete resource plan detail
        /// </summary>
        public void DeleteRPDetail(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();
            objDALResourcePlan.DeleteRPDetail(objBEResourcePlan);
        }

        /// <summary>
        /// Save resource plan
        /// </summary>
        public void CreateRPDuration(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();
            objDALResourcePlan.CreateRPDuration(objBEResourcePlan);
        }

        /// <summary>
        /// Get resource plan
        /// </summary>
        public RaveHRCollection GetResourcePlan(BusinessEntities.ResourcePlan objBEResourcePlan, ref int pageCount)
        {
            objDALResourcePlan = new DataAccessLayer.Projects.ResourcePlan();
            return objDALResourcePlan.GetResourcePlan(objBEResourcePlan, ref pageCount);
        }

        /// <summary>
        /// Create resource plan
        /// </summary>
        public void CreateResourcePlan(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            AuthorizationManager objAuMan = new AuthorizationManager();

            objDALResourcePlan = new DataAccessLayer.Projects.ResourcePlan();

            objDALResourcePlan.CreateResourcePlan(objBEResourcePlan);

            //string strCurrentUser = objAuMan.getLoggedInUserEmailId();
            string strCurrentUser = objAuMan.getLoggedInUser();

            //google
            strCurrentUser = objAuMan.GetDomainUserName(strCurrentUser);

            //--Get Project Name
            BusinessEntities.RaveHRCollection objListProjectDetail = new BusinessEntities.RaveHRCollection();

            objListProjectDetail = GetProjectByRP(objBEResourcePlan);

            string sComp = Utility.GetUrl();

            string strProjectId = (((BusinessEntities.Projects)(objListProjectDetail.Item(0))).ProjectId).ToString();

            string strClientName = (((BusinessEntities.ResourcePlan)(objListProjectDetail.Item(1))).ClientName).ToString();

            string strProjectName = (((BusinessEntities.Projects)(objListProjectDetail.Item(0))).ProjectName).ToString();

            string strProjectCode = (((BusinessEntities.Projects)(objListProjectDetail.Item(0))).ProjectCode).ToString();

            string strResourcePlanCode = (((BusinessEntities.ResourcePlan)(objListProjectDetail.Item(1))).ResourcePlanCode).ToString();

            string strApproveRejectLink = sComp + CommonConstants.APPROVEREJECTRP_PAGE + "?" + URLHelper.SecureParameters(QueryStringConstants.PROJECTID, strProjectId) + "&" + URLHelper.CreateSignature(strProjectId);

            //SendCreateResourcePlanMails(strApproveRejectLink, strProjectName, strResourcePlanCode, strClientName, strProjectCode,  strCurrentUser.ToLower().Contains("@rave-tech.co.in") ? objAuMan.GetDomainUserName(strCurrentUser.Replace(RAVE_DOMAIN, "")) : objAuMan.GetDomainUserName(strCurrentUser.Replace(NORTHGATE_DOMAIN, "") ));
            SendCreateResourcePlanMails(strApproveRejectLink, strProjectName, strResourcePlanCode, strClientName, strProjectCode, strCurrentUser);

        }

        /// <summary>
        /// Get Resource plan duration by id
        /// </summary>
        public BusinessEntities.ResourcePlan RPDurationByID(BusinessEntities.ResourcePlan objBEResourcePlan, string Mode)
        {
            objDALResourcePlan = new DataAccessLayer.Projects.ResourcePlan();
            return objDALResourcePlan.RPDurationByID(objBEResourcePlan, Mode);
        }

        /// <summary>
        /// update Resource plan duration by id
        /// </summary>
        public void UpdateRPDurationByID(BusinessEntities.ResourcePlan objBEResourcePlan, string Mode)
        {
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();
            objDALResourcePlan.UpdateRPDurationByID(objBEResourcePlan, Mode);
        }

        /// <summary>
        /// Get Resource plan detail by id
        /// </summary>
        public BusinessEntities.ResourcePlan RPDetailByID(BusinessEntities.ResourcePlan objBEResourcePlan, string Mode)
        {
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();
            return objDALResourcePlan.RPDetailByID(objBEResourcePlan, Mode);
        }

        /// <summary>
        /// update Resource plan detail by id
        /// </summary>
        public void UpdateRPDetailByID(BusinessEntities.ResourcePlan objBEResourcePlan, string Mode)
        {
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();
            objDALResourcePlan.UpdateRPDetailByID(objBEResourcePlan, Mode);
        }

        /// <summary>
        /// Delete Resource plan
        /// </summary>
        public void DeleteResourcePlan(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();
            objDALResourcePlan.DeleteResourcePlan(objBEResourcePlan);
        }

        /// <summary>
        /// method to get resource plan for approve/reject resource plan.
        /// </summary>
        public BusinessEntities.RaveHRCollection GetResourcePlanForApproveRejectRP(BusinessEntities.ResourcePlan objBEApproveRejectRP)
        {
            //create object of dataaccesslayer of resourceplan
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();

            // Initialise the Collection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            //method to get resourceplan for ApproveRejectRP
            raveHRCollection = objDALResourcePlan.GetResourcePlanForApproveRejectRP(objBEApproveRejectRP);

            //return collection object.
            return raveHRCollection;
        }

        /// <summary>
        /// this funtion is used to add comments for approve/reject resource plan.
        /// </summary>
        public string AddReasonForApproveRejectRP(BusinessEntities.ResourcePlan objBEApproveRejectRP)
        {
            //create object for resource plan
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();

            //calls method to add reason for Approve/Reject resource plan and return the same. 
            return objDALResourcePlan.AddReasonForApproveRejectRP(objBEApproveRejectRP);
        }

        /// <summary>
        /// this function is used to get project details for approve/reject resource plan.
        /// </summary>
        public BusinessEntities.RaveHRCollection GetProjectDetailsForApproveRejectRP(BusinessEntities.Projects objViewProject, ref int pageCount)
        {
            // Initialise the Collection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            //create object for dataaccesslayer of ResourcePlan
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();

            //method to get project details for approve/reject resource plan.
            raveHRCollection = objDALResourcePlan.ViewProjectDetailsForApproveRejectRP(objViewProject, ref pageCount);

            // return list 
            return raveHRCollection;

        }

        /// <summary>
        /// Get project details
        /// </summary>
        public BusinessEntities.ResourcePlan GetProjectDetails(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            objDALResourcePlan = new DataAccessLayer.Projects.ResourcePlan();
            return objDALResourcePlan.GetProjectDetails(objBEResourcePlan);
        }

        /// <summary>
        /// Get Resource Plan by Id
        /// </summary>
        public RaveHRCollection GetResourcePlanById(BusinessEntities.ResourcePlan objBEResourcePlan, ref int pageCount)
        {
            objDALResourcePlan = new DataAccessLayer.Projects.ResourcePlan();
            return objDALResourcePlan.GetResourcePlanById(objBEResourcePlan, ref pageCount);
        }

        /// <summary>
        /// Edit Resource Plan by Id
        /// </summary>
        public void EditRPDuration(BusinessEntities.ResourcePlan objBEResourcePlan, bool rolechange, string StrMRFCode)
        {
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();
            objDALResourcePlan.EditRPDuration(objBEResourcePlan, rolechange, StrMRFCode);
        }

        /// <summary>
        /// To get the active or inactive resource plan.
        /// </summary>
        public RaveHRCollection GetActiveOrInactiveResourcePlan(int IsActive, int ProjectId)
        {
            objDALResourcePlan = new DataAccessLayer.Projects.ResourcePlan();
            return objDALResourcePlan.GetActiveOrInactiveResourcePlan(IsActive, ProjectId);
        }

        /// <summary>
        /// update Resource plan detail by id
        /// </summary>
        public void EditRPDetailByID(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();
            objDALResourcePlan.EditRPDetailByID(objBEResourcePlan);
        }

        /// <summary>
        /// Delete RP Edited in history
        /// </summary>
        public void DeleteRPEdited(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();
            objDALResourcePlan.DeleteRPEdited(objBEResourcePlan);
        }

        /// <summary>
        /// Save RP Edited in history
        /// </summary>
        public void SaveRPEdited(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();
            objDALResourcePlan.SaveRPEdited(objBEResourcePlan);
        }

        /// <summary>
        /// Get Project Details by Resource Plan Id
        /// </summary>
        public RaveHRCollection GetProjectById(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();
            return objDALResourcePlan.GetProjectById(objBEResourcePlan);
        }

        /// <summary>
        /// Get Project details by Resource Plan ID.
        /// </summary>
        public RaveHRCollection GetProjectByRPId(BusinessEntities.ResourcePlan objBEResourcePlan, string strApproveOrReject, string fileName, RaveHRCollection raveHRCollection)
        {
            AuthorizationManager objAuMan = new AuthorizationManager();

            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();

            RaveHRCollection objProjectDetailsByRPId = new RaveHRCollection();

            objProjectDetailsByRPId = objDALResourcePlan.GetProjectByRPId(objBEResourcePlan);

            string strPMEmailId = string.Empty;

            if (objProjectDetailsByRPId.Count > 1)
            {
                //--ProjectName
                strPMEmailId = ((BusinessEntities.Projects)objProjectDetailsByRPId.Item(0)).EmailIdOfPM;

                //--Empty list
                objProjectDetailsByRPId.Clear();
            }

            //string strRPApproverUserEamilIdsInRole = objAuMan.MailRecepientTo(Convert.ToInt32(EnumsConstants.MailFunctionality.ApprovedRejectedRP));

            //google
            //string strCurrentUser = objAuMan.getLoggedInUserEmailId();
            string strCurrentUser = objAuMan.getLoggedInUser();
            strCurrentUser = objAuMan.GetDomainUserName(strCurrentUser);

            //string strCCUserEamilIdsInRole = objAuMan.MailRecepientCC(Convert.ToInt32(EnumsConstants.MailFunctionality.ApprovedRejectedRP)) + "," + strPMEmailId;

            BusinessEntities.RaveHRCollection objListProjectDetail = new BusinessEntities.RaveHRCollection();

            objListProjectDetail = GetProjectByRP(objBEResourcePlan);

            string strProjectID = (((BusinessEntities.Projects)(objListProjectDetail.Item(0))).ProjectId).ToString();

            string strProjectName = (((BusinessEntities.Projects)(objListProjectDetail.Item(0))).ProjectName).ToString();

            string strProjectCode = (((BusinessEntities.Projects)(objListProjectDetail.Item(0))).ProjectCode).ToString();

            string strNewProjectCode = (((BusinessEntities.Projects)(objListProjectDetail.Item(0))).ProjectCodeAbbrevation).ToString();

            string strResourcePlanID = objBEResourcePlan.RPId.ToString();

            if (fileName.EndsWith(@"\"))  
                fileName += (((BusinessEntities.ResourcePlan)(objListProjectDetail.Item(1))).RPFileName).ToString();

            string strResourcePlanCode = (((BusinessEntities.ResourcePlan)(objListProjectDetail.Item(1))).ResourcePlanCode).ToString();

            string strReasonForApproval = objBEResourcePlan.ReasonForApproval.ToString();

            string strClientName = (((BusinessEntities.ResourcePlan)(objListProjectDetail.Item(1))).ClientName).ToString();

            ((BusinessEntities.ResourcePlan)(objListProjectDetail.Item(1))).ProjectId = Convert.ToInt32(strProjectID);

            string sComp = Utility.GetUrl();

            string strApproveRejectLink = sComp + CommonConstants.VIEWRP_PAGE + "?" + URLHelper.SecureParameters(QueryStringConstants.PROJECTID, strProjectID) + "&" + URLHelper.SecureParameters(QueryStringConstants.FLAG, true.ToString()) + "&" + URLHelper.SecureParameters(QueryStringConstants.RPID, strResourcePlanID) + "&" + URLHelper.SecureParameters(QueryStringConstants.RPCODE, strResourcePlanCode) + "&" + URLHelper.CreateSignature(strProjectID, true.ToString(), strResourcePlanID, strResourcePlanCode);

            string projectManagerEmailId = GetProjectManagerEmailID((BusinessEntities.ResourcePlan)(objListProjectDetail.Item(1)));

            //SendApproveRejectResourcePlanMails(strCurrentUser.ToLower().Contains("@rave-tech.co.in") ? objAuMan.GetDomainUserName(strCurrentUser.Replace(RAVE_DOMAIN, "")) : objAuMan.GetDomainUserName(strCurrentUser.Replace(NORTHGATE_DOMAIN, "") ),
            SendApproveRejectResourcePlanMails(strCurrentUser,
                //gging google
               // objAuMan.GetDomainUserName(strCurrentUser.Replace(RAVE_DOMAIN, "")),
                                                strApproveOrReject,
                                                strNewProjectCode,
                                                strProjectName,
                                                strClientName,
                                                strResourcePlanCode,
                                                strReasonForApproval,
                                                strApproveRejectLink,
                                                projectManagerEmailId,
                                                fileName,
                                                raveHRCollection);

            return objProjectDetailsByRPId;
        }

        private string GetProjectManagerEmailID(BusinessEntities.ResourcePlan resourcePlan)
        {
            string projectManagerEmailID = string.Empty;

            try
            {
                RaveHRCollection projectManager = new RaveHRCollection();

                projectManager = GetProjectManagerByProjectId(resourcePlan);

                foreach (BusinessEntities.Projects projectManagerName in projectManager)
                {
                    projectManagerEmailID = projectManagerName.EmailIdOfPM;
                }

            }
            catch (Exception ex)
            {

            }
            return projectManagerEmailID;
        }

        /// <summary>
        /// Get Project Manager by ProjectID.
        /// </summary>
        public RaveHRCollection GetProjectManagerByProjectId(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();

            return objDALResourcePlan.GetProjectManagerByProjectId(objBEResourcePlan);
        }

        /// <summary>
        /// Get Project details by Resource Plan ID.
        /// </summary>
        public RaveHRCollection GetProjectByRPId(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();
            return objDALResourcePlan.GetProjectByRPId(objBEResourcePlan);
        }


        /// <summary>
        /// Get complete data of Resource Plan by Id for excel dump
        /// </summary>
        public RaveHRCollection GetResourcePlanById(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();
            return objDALResourcePlan.GetResourcePlanById(objBEResourcePlan);
        }

        /// <summary>
        /// view resource plan in excel
        /// </summary>
        /// 
        //RP Issue 33126 Start
        //public void ViewRPInExcel(int ResourcePlanId, string strRPCode)
        public void ViewRPInExcel(int ResourcePlanId, string strRPCode,string RPMode)
        {
            //RP Issue 33126 End
            try
            {
                //--Get data for RP
                BusinessEntities.ResourcePlan objBE = new BusinessEntities.ResourcePlan();
                objBE.RPId = ResourcePlanId;
                objBE.RPDurationStatusId = Convert.ToInt32(MasterEnum.RPDurationEditionStatus.Deleted).ToString();
                objBE.RPDetailStatusId = Convert.ToInt32(MasterEnum.RPDetailEditionStatus.Deleted).ToString();

                //RP Issue 33126 Start
                if (RPMode.Equals("BeforeApproveMode"))
                {
                    objBE.Mode = "BeforeApproveMode";
                }
                else
                {
                    objBE.Mode = "";
                }
                //RP Issue 33126 End


                Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLRP = new Rave.HR.BusinessLayer.Projects.ResourcePlan();
                BusinessEntities.RaveHRCollection objListRP = new BusinessEntities.RaveHRCollection();
                //--Get Project Details
                string strProjectName = string.Empty;
                objListRP = objBLLRP.GetProjectByRPId(objBE);
                if (objListRP.Count > 1)
                {
                    //--ProjectName
                    strProjectName = ((BusinessEntities.ResourcePlan)objListRP.Item(1)).ClientName + "-" + ((BusinessEntities.Projects)objListRP.Item(0)).ProjectName;
                    //--Add RPCode
                    strProjectName += "(" + ((BusinessEntities.ResourcePlan)objListRP.Item(1)).ResourcePlanCode + ")";
                    //--Empty list
                    objListRP.Clear();
                }
                else
                {
                    strProjectName = strRPCode;
                }

                //--Get RP Details
                objListRP = objBLLRP.GetResourcePlanById(objBE);

                //--Check count 
                if (objListRP.Count <= 0)
                {
                    GetExcel(strProjectName, "No records found");
                    return;
                }

                //--Get the minimun & max Date
                DateTime dtMinDate, dtMaxDate;
                objBE = (BusinessEntities.ResourcePlan)objListRP.Item(0);
                dtMinDate = objBE.StartDate;
                dtMaxDate = objBE.EndDate;
                foreach (BusinessEntities.ResourcePlan objBERP in objListRP)
                {
                    //--Get Min Date
                    if (dtMinDate > objBERP.StartDate)
                    {
                        dtMinDate = objBERP.StartDate;
                    }

                    //--Get Max Date
                    if (dtMaxDate < objBERP.EndDate)
                    {
                        dtMaxDate = objBERP.EndDate;
                    }
                }

                //--Get number of months between dates
                int totalMonths = GetMonthDifference(dtMaxDate, dtMinDate);

                //****************Dump data
                StringBuilder strbHTML = new StringBuilder("");

                //--Check if totalmonths less than 2 months
                if (totalMonths <= 2)
                {
                    //strbHTML = GetRPDetailForTwoMonthDuration(objListRP, totalMonths, dtMaxDate);
                    strbHTML = GetRPDetailForTwoMonthDuration(objListRP, totalMonths, dtMinDate);
                }
                else
                {
                    strbHTML = GetRPDetails(objListRP, totalMonths, dtMaxDate);
                }

                //--Generate Excel
                GetExcel(strProjectName, strbHTML.ToString());

            }

            catch (System.Threading.ThreadAbortException ex) { }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME_RP, "ViewRPInExcel", EventIDConstants.RAVE_HR_RP_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Get RP details
        /// </summary>
        private StringBuilder GetRPDetails(BusinessEntities.RaveHRCollection objListRP, int totalMonths, DateTime dtMaxDate)
        {
            try
            {
                StringBuilder strbHTML = new StringBuilder("");

                //--Create table for RP
                strbHTML.Append("<table border = '0' cellpadding = '0' cellspacing = '0' style='border-color:Black;' width = '100%'>");
                strbHTML.Append("<tr><td>&nbsp;</td></tr>");
                strbHTML.Append("<tr>");
                strbHTML.Append("<td width = '120%'>");

                //--******************Create Table Header********************************/

                strbHTML.Append("<table border = '2' cellpadding = '0' cellspacing = '0' style='border-color:Black;' width = '100%'>");

                //--Get width  for months
                int width = 80 / (totalMonths + 2); //--+2 for start date & end date col

                //--Display month names at the top of M1, M2 etc
                strbHTML.Append("<tr>");
                strbHTML.Append("<td width = '10%' style='" + strRowStyle + "'>&nbsp;</td>");
                strbHTML.Append("<td width = '20%' style='" + strRowStyle + "'>&nbsp;</td>");
                strbHTML.Append("<td width = '10%' style='" + strRowStyle + "'>&nbsp;</td>");
                strbHTML.Append("<td width = '20%' style='" + strRowStyle + "'>&nbsp;</td>");
                strbHTML.Append("<td width = '10%' style='" + strRowStyle + "'>&nbsp;</td>");

                DateTime dtMaxDate1 = dtMaxDate;
                ArrayList arrMonths = new ArrayList();
                for (int i = 1; i <= totalMonths; i++)
                {
                    strbHTML.Append("<td align = 'center' width = '" + width + "' style='" + strHeaderStyle + "'><b>" + string.Format("{0:y}", dtMaxDate1) + "</b></td>");

                    arrMonths.Add("01-" + dtMaxDate1.Month + "-" + dtMaxDate1.Year);

                    dtMaxDate1 = dtMaxDate1.AddMonths(-1);
                }

                strbHTML.Append("</tr>");

                strbHTML.Append("<tr>");
                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>Roles</b></td>");
                
                //CR 30928 RP View excel naming convention to be changed Sachin Start
                // Changed column Name from 'Resource Name' to 'Resource Name - Actual Allocation'
                strbHTML.Append("<td align = 'center' width = '20%' style='" + strHeaderStyle + "'><b>Resource Name - Actual Allocation</b></td>");
                // CR 30928 RP View excel naming convention to be changed Sachin End

                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>Location</b></td>");

                // CR 30928 RP View excel naming convention to be changed Sachin Start
                // Changed column Name from 'Start Date' to 'Planned Start Date' and 'End Date' to 'Planned End Date'
                strbHTML.Append("<td align = 'center' width = '" + width + "' style='" + strHeaderStyle + "'><b>Planned Start Date</b></td>");
                strbHTML.Append("<td align = 'center' width = '" + width + "' style='" + strHeaderStyle + "'><b>Planned End Date</b></td>");
                // CR 30928 RP View excel naming convention to be changed Sachin End

                //--
                for (int i = totalMonths; i >= 1; i--)
                {
                    strbHTML.Append("<td align = 'center' width = '" + width + "' style='" + strHeaderStyle + "'><b>M" + i + "</b></td>");
                }

                strbHTML.Append("</tr>");

                //--**********************End of Create Table Header***************************/

                //************************Create Data Rows**************************************/

                //--
                ArrayList arrRoles = new ArrayList();
                foreach (BusinessEntities.ResourcePlan objBERP in objListRP)
                {
                    strbHTML.Append("<tr>");
                    //if (!arrRoles.Contains(objBERP.Role))
                    if (!arrRoles.Contains(objBERP.ResourcePlanDurationId))
                    {
                        strbHTML.Append("<td align = 'left' style='" + strRoleStyle + "'>" + objBERP.Role + "</td>");
                        arrRoles.Add(objBERP.ResourcePlanDurationId);
                        //--Add the resourcename
                        strbHTML.Append("<td align = 'center' style='" + strRowStyle + "'>" + objBERP.ResourceName + "</td>");
                    }
                    else
                    {
                        strbHTML.Append("<td align = 'left' style='" + strRowStyle + "'>&nbsp;</td>");
                        //--Add the resourcename
                        strbHTML.Append("<td align = 'center' style='" + strRowStyle + "'>&nbsp;</td>");
                    }

                    strbHTML.Append("<td align = 'center' style='" + strRowStyle + "'>" + objBERP.ResourceLocation + "</td>");

                    //--Get Utlization for months for startdate
                    DateTime dtMonths, dtStartDate, dtEndDate;
                    dtStartDate = Convert.ToDateTime("01-" + objBERP.StartDate.Month + "-" + objBERP.StartDate.Year);
                    dtEndDate = Convert.ToDateTime("01-" + objBERP.EndDate.Month + "-" + objBERP.EndDate.Year);
                    //--Add Start Date & End Dates
                    strbHTML.Append("<td align = 'center' style='" + strRowStyle + "'>" + objBERP.StartDate.ToString(CommonConstants.DATE_FORMAT) + "</td>");
                    strbHTML.Append("<td align = 'center' style='" + strRowStyle + "'>" + objBERP.EndDate.ToString(CommonConstants.DATE_FORMAT) + "</td>");
                    for (int i = 0; i < arrMonths.Count; i++)
                    {
                        dtMonths = Convert.ToDateTime(Convert.ToDateTime(arrMonths[i].ToString()).ToShortDateString());
                        if ((dtMonths >= dtStartDate) && (dtMonths <= dtEndDate))
                        {
                            strbHTML.Append("<td align = 'right' style='" + strUtilizationRowStyle + "'>" + objBERP.Utilization + "%, " + objBERP.Billing + "%</td>");
                        }
                        else
                        {
                            strbHTML.Append("<td>&nbsp;</td>");
                        }
                    }


                    strbHTML.Append("</tr>");
                }

                //************************End of Create Data Rows*******************************/

                strbHTML.Append("</td>");
                strbHTML.Append("</tr>");
                strbHTML.Append("</table>");

                return strbHTML;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME_RP, "GetRPDetails", EventIDConstants.RAVE_HR_RP_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// get month difference for different dates
        /// </summary>
        private int GetMonthDifference(DateTime date_start, DateTime date_end)
        {
            int totalmonths = 0;
            date_start = DateTime.Parse("01-" + date_start.Month + "-" + date_start.Year);
            date_end = DateTime.Parse("01-" + date_end.Month + "-" + date_end.Year);
            while (date_start >= date_end)
            {
                totalmonths++;
                date_end = date_end.AddMonths(1);
            }

            return totalmonths;
        }

        /// <summary>
        /// Get the excel
        /// </summary>
        private void GetExcel(string strFileName, string strbHTML)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".xls");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";

            HttpContext.Current.Response.Write(strbHTML);

            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// gets resoucePlanId by projectId.
        /// </summary>
        public RaveHRCollection GetResourcePlanForProjectId(BusinessEntities.ResourcePlan objBEApproveRejectRP)
        {
            //create object for resource plan
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();

            //calls method to check if any resource plan exist for any project.
            return objDALResourcePlan.GetResourcePlanForProjectId(objBEApproveRejectRP);
        }

        /// <summary>
        /// get allocated resource by projectId
        /// </summary>
        public RaveHRCollection GetAllocatedResourceByProjectId(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            //create object for resource plan
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();

            //calls method to check if any resource plan exist for any project.
            return objDALResourcePlan.GetAllocatedResourceByProjectId(objBEResourcePlan);
        }

        /// <summary>
        /// Get pending MRF's 
        /// </summary>
        public RaveHRCollection GetPendingMRF(BusinessEntities.ResourcePlan objBEResourcePlan, string StatusIds)
        {
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();
            SaveRPEdited(objBEResourcePlan);
            return objDALResourcePlan.GetPendingMRF(objBEResourcePlan, StatusIds);
        }

        /// <summary>
        /// Create Resource Plan in Edit page 
        /// </summary>
        public void CreateAndEditRPDuration(BusinessEntities.ResourcePlan objBECreateResourcePlanDuration)
        {
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();
            objDALResourcePlan.CreateAndEditRPDuration(objBECreateResourcePlanDuration);
        }

        /// <summary>
        /// Get RP details if less than two month duration
        /// </summary>
        private StringBuilder GetRPDetailForTwoMonthDuration(BusinessEntities.RaveHRCollection objListRP, int totalMonths, DateTime dtMaxDate)
        {
            try
            {
                StringBuilder strbHTML = new StringBuilder("");

                //--Create table for RP
                strbHTML.Append("<table border = '0' cellpadding = '0' cellspacing = '0' style='border-color:Black;' width = '100%'>");
                strbHTML.Append("<tr><td>&nbsp;</td></tr>");
                strbHTML.Append("<tr>");
                strbHTML.Append("<td width = '120%'>");

                //--******************Create Table Header********************************/

                strbHTML.Append("<table border = '2' cellpadding = '0' cellspacing = '0' style='border-color:Black;' width = '100%'>");

                //--Get width  for months
                int width = 80 / (totalMonths + 2); //--+2 for start date & end date col

                //--Display month names at the top of M1, M2 etc
                strbHTML.Append("<tr>");
                strbHTML.Append("<td width = '10%' style='" + strRowStyle + "'>&nbsp;</td>");
                strbHTML.Append("<td width = '20%' style='" + strRowStyle + "'>&nbsp;</td>");
                strbHTML.Append("<td width = '10%' style='" + strRowStyle + "'>&nbsp;</td>");
                strbHTML.Append("<td align = '10%' style='" + strRowStyle + "'><b></b></td>");
                strbHTML.Append("<td align = '10%' style='" + strRowStyle + "'><b></b></td>");

                //--Columns for weeks
                ArrayList arrMonths = new ArrayList();
                int iNumberOfWeeksInMonth = 0;
                //for (int i = totalMonths; i >= 1; i--)
                for (int i = 1; i <= totalMonths; i++)
                {
                    iNumberOfWeeksInMonth = GetWeeks(dtMaxDate.Month);
                    strbHTML.Append("<td align = 'center' width = '" + width + "' colspan='" + iNumberOfWeeksInMonth + "'  style='" + strHeaderStyle + "'><b>" + string.Format("{0:y}", dtMaxDate) + "</b></td>");
                    arrMonths.Add("01-" + dtMaxDate.Month + "-" + dtMaxDate.Year);
                    //dtMaxDate = dtMaxDate.AddMonths(-1);
                    dtMaxDate = dtMaxDate.AddMonths(1);
                }

                strbHTML.Append("</tr>");

                strbHTML.Append("<tr >");
                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>Roles</b></td>");
                strbHTML.Append("<td align = 'center' width = '20%' style='" + strHeaderStyle + "'><b>Resource Name</b></td>");
                strbHTML.Append("<td align = 'center' width = '10%' style='" + strHeaderStyle + "'><b>Location</b></td>");
                strbHTML.Append("<td align = 'center' width = '" + width + "' style='" + strHeaderStyle + "'><b>Start Date</b></td>");
                strbHTML.Append("<td align = 'center' width = '" + width + "' style='" + strHeaderStyle + "'><b>End Date</b></td>");


                //--get weeks
                for (int i = 0; i < arrMonths.Count; i++)
                {
                    DateTime dt = DateTime.Parse(arrMonths[i].ToString());
                    iNumberOfWeeksInMonth = GetWeeks(dt.Month);
                    //for (int j = iNumberOfWeeksInMonth; j >= 1; j--)
                    for (int j = 1; j <= iNumberOfWeeksInMonth; j++ )
                    {
                        strbHTML.Append("<td align = 'center' width = '" + width + "' style='" + strHeaderStyle + "'><b>W" + j + "</b></td>");
                    }
                }
                strbHTML.Append("</tr>");

                //--**********************End of Create Table Header***************************/
                //************************Create Data Rows**************************************/

                //--Get Data
                ArrayList arrRoles = new ArrayList();
                foreach (BusinessEntities.ResourcePlan objBERP in objListRP)
                {
                    strbHTML.Append("<tr>");
                    //if (!arrRoles.Contains(objBERP.Role))
                    if (!arrRoles.Contains(objBERP.ResourcePlanDurationId))
                    {
                        strbHTML.Append("<td align = 'left' style='" + strRoleStyle + "'>" + objBERP.Role + "</td>");
                        arrRoles.Add(objBERP.ResourcePlanDurationId);
                        //--Add the resourcename
                        strbHTML.Append("<td align = 'center' style='" + strRowStyle + "'>" + objBERP.ResourceName + "</td>");
                    }
                    else
                    {
                        strbHTML.Append("<td align = 'left' style='" + strRowStyle + "'>&nbsp;</td>");
                        //--Add the resourcename
                        strbHTML.Append("<td align = 'center' style='" + strRowStyle + "'>&nbsp;</td>");
                    }

                    strbHTML.Append("<td align = 'center' style='" + strRowStyle + "'>" + objBERP.ResourceLocation + "</td>");
                    //--Add Start Date & End Dates
                    strbHTML.Append("<td align = 'center' style='" + strRowStyle + "'>" + objBERP.StartDate.ToString(CommonConstants.DATE_FORMAT) + "</td>");
                    strbHTML.Append("<td align = 'center' style='" + strRowStyle + "'>" + objBERP.EndDate.ToString(CommonConstants.DATE_FORMAT) + "</td>");

                    //--Get utilization for weeks
                    DateTime dtStartDate, dtEndDate;
                    dtStartDate = objBERP.StartDate;
                    dtEndDate = objBERP.EndDate;
                    //for (int i = arrMonths.Count - 1; i >= 0; i--)
                    for (int i = 0; i < arrMonths.Count; i++)
                    {
                        DateTime dt = DateTime.Parse(arrMonths[i].ToString());
                        iNumberOfWeeksInMonth = GetWeeks(dt.Month);
                        DayOfWeek day = dt.DayOfWeek;
                        int days = day - DayOfWeek.Monday;

                        DateTime start = dt.AddDays(-days);
                        DateTime end = start.AddDays(6);
                        //for (int j = iNumberOfWeeksInMonth - 1; j >= 0; j--)
                        for (int j = 0; j < iNumberOfWeeksInMonth; j++ )
                        {
                            //--Check month
                            if ((dt.Month == dtStartDate.Month) || (dt.Month == dtEndDate.Month))
                            {
                                if ((((start >= dtStartDate) && (start <= dtEndDate)) || ((end >= dtStartDate) && (end <= dtEndDate))) || ((dt == dtStartDate) || (dt == dtEndDate))
                                    ||
                                (((dtStartDate >= start) && (dtStartDate <= end)) || ((dtEndDate >= start) && (dtEndDate <= end))))
                                {
                                    strbHTML.Append("<td align = 'right' style='" + strUtilizationRowStyle + "'>" + objBERP.Utilization + "%, " + objBERP.Billing + "%</td>");
                                }
                                else
                                {
                                    strbHTML.Append("<td style='" + strRowStyle + "'>&nbsp;</td>");
                                }
                            }
                            else
                            {
                                strbHTML.Append("<td style='" + strRowStyle + "'>&nbsp;</td>");
                            }
                            start = end.AddDays(1);
                            end = start.AddDays(6);
                            dt = dt.AddDays(-360);  //--default adddays
                        }
                    }

                    strbHTML.Append("</tr>");
                }

                //************************End of Create Data Rows*******************************/

                strbHTML.Append("</td>");
                strbHTML.Append("</tr>");
                strbHTML.Append("</table>");

                return strbHTML;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME_RP, "GetRPDetailForTwoMonthDuration", EventIDConstants.RAVE_HR_RP_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Get number of weeks for the month
        /// </summary>
        public int GetWeeks(int month)
        {
            DateTime dateFrom = DateTime.Parse("01-" + month + "-" + DateTime.Now.Year);
            DateTime dateTo = DateTime.Parse(System.DateTime.DaysInMonth(DateTime.Now.Year, month) + "-" + month + "-" + DateTime.Now.Year);

            TimeSpan Span = dateTo.Subtract(dateFrom);

            if (Span.Days <= 7)
            {
                if (dateFrom.DayOfWeek > dateTo.DayOfWeek)
                {
                    return 2;
                }

                return 1;
            }

            int Days = Span.Days - 7 + (int)dateFrom.DayOfWeek;
            int WeekCount = 1;
            int DayCount = 0;

            for (WeekCount = 1; DayCount < Days; WeekCount++)
            {
                DayCount += 7;
            }

            return WeekCount;
        }

        /// <summary>
        /// Get Number Of Resource By ProjectId
        /// </summary>
        public BusinessEntities.ResourcePlan GetNoOfResouceByProjectId(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();
            return objDALResourcePlan.GetNoOfResouceByProjectId(objBEResourcePlan);
        }


        /// <summary>
        /// method to get resource plan details by project Id.
        /// Created By : Yagendra Sharnagat
        /// Created Date: 05 Feb 2010 
        /// To get rp details on contract summary page.
        /// </summary>
        public BusinessEntities.RaveHRCollection GetResourcePlanByProjectIdForContract(BusinessEntities.ResourcePlan objBEApproveRejectRP)
        {
            //create object of dataaccesslayer of resourceplan
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();

            // Initialise the Collection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            //method to get resourceplan for ApproveRejectRP
            raveHRCollection = objDALResourcePlan.GetResourcePlanByProjectIdForContract(objBEApproveRejectRP);

            //return collection object.
            return raveHRCollection;
        }

        /// <summary>
        /// method to get edited details for mail
        /// </summary>
        public RaveHRCollection GetRPDetailsForMail(BusinessEntities.ResourcePlan objBEResourcePlan, int projectId, out bool isValidActiveRP, out string strProjectName, string txtFilePath)
        {
            //create object of dataaccesslayer of resourceplan
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();

            // Initialise the Collection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            int counter = 0;

            while (File.Exists(txtFilePath + CommonConstants.RESOURCEPLANTABLEDATA + counter.ToString() + CommonConstants.EXTENSION))
            {
                counter++;
            }

            string fileName = CommonConstants.RESOURCEPLANTABLEDATA + counter.ToString() + CommonConstants.EXTENSION;

            string fileLocation = txtFilePath + CommonConstants.RESOURCEPLANTABLEDATA + counter.ToString() + CommonConstants.EXTENSION;

            objBEResourcePlan.RPFileName = fileName;

            //Rakesh Switched raveHRCollection above SaveRPEdited To Solve Getting Multiple Rows
            SaveRPEdited(objBEResourcePlan);


            //method to get resourceplan for ApproveRejectRP
            raveHRCollection = objDALResourcePlan.GetRPDetailsForMail(objBEResourcePlan);

         

            BusinessEntities.RaveHRCollection objListProjectDetail = new BusinessEntities.RaveHRCollection();

            objListProjectDetail = GetProjectByRPId(objBEResourcePlan);

            string strResourcePlanId = objBEResourcePlan.RPId.ToString();

            string sComp = Utility.GetUrl();

            string strApproveRejectLink = sComp + CommonConstants.APPROVEREJECTRP_PAGE + "?" + URLHelper.SecureParameters(QueryStringConstants.PROJECTID, projectId.ToString()) + "&" + URLHelper.SecureParameters(QueryStringConstants.TXTFILELOCATION, fileName) + "&" + URLHelper.CreateSignature(projectId.ToString(), fileName);

            strProjectName = (((BusinessEntities.Projects)(objListProjectDetail.Item(0))).ProjectName).ToString();

            string strProjectCode = (((BusinessEntities.Projects)(objListProjectDetail.Item(0))).ProjectCode).ToString();

            string strResourcePlanCode = (((BusinessEntities.ResourcePlan)(objListProjectDetail.Item(1))).ResourcePlanCode).ToString();

            string strClientName = (((BusinessEntities.ResourcePlan)(objListProjectDetail.Item(1))).ClientName).ToString();

            ((BusinessEntities.ResourcePlan)(objListProjectDetail.Item(1))).ProjectId = Convert.ToInt32((((BusinessEntities.Projects)(objListProjectDetail.Item(0))).ProjectId).ToString());

            string projectManagerEmailId = GetProjectManagerEmailID((BusinessEntities.ResourcePlan)(objListProjectDetail.Item(1)));

            isValidActiveRP = ValidateRPIsActive(projectId, strResourcePlanId);

            if (isValidActiveRP)
            {
                if (raveHRCollection.Count > 0)
                {

                    SendEditResourcePlanMails(strProjectCode,
                                                strProjectName,
                                                strClientName,
                                                strResourcePlanCode,
                                                strApproveRejectLink,
                                                raveHRCollection,
                                                projectManagerEmailId,
                                                fileLocation);
                }

            }

            //Issue ID : 33960 When MRF get delete mail should goes to RMGroup START
            //We are sending the mail on Send For Approval beacuse while Approve Reject RP on both cases MRF phiscally deleted. So It gives wrong impression 
            //when you are rejecting MRF and send mail MRD Deleted.
            DataAccessLayer.MRF.MRFDetail mRFDetail = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
            BusinessEntities.MRFDetail MRFDetaildeleteobjforMail = new BusinessEntities.MRFDetail();
            Rave.HR.BusinessLayer.MRF.MRFDetail mRFDetailBL = new Rave.HR.BusinessLayer.MRF.MRFDetail();

            List<int> lstMRFID = mRFDetail.GetListOfMRFIdFROMRP(objBEResourcePlan.RPId);

            foreach (var item in lstMRFID)
            {
                MRFDetaildeleteobjforMail = mRFDetail.GetMRFDetails(item);
                mRFDetailBL.SendMailDeleteMRF(MRFDetaildeleteobjforMail, 0);
            }

            //Issue ID : 33960 When MRF get delete mail should goes to RMGroup END

            return raveHRCollection;
        }

        /// <summary>
        /// Method to Update Employee ProjectAllocation By Resource Plan
        /// </summary>
        public void UpdateEmployeeProjectAllocation(BusinessEntities.ResourcePlan objBEGetResourcePlanId)
        {
            //create object of dataaccesslayer of resourceplan
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();

            // Call Method to Update Employee ProjectAllocation
            objDALResourcePlan.UpdateEmployeeProjectAllocation(objBEGetResourcePlanId);
        }

        /// <summary>
        /// Method to Send Mail for Newly Created Resource Plan
        /// </summary>
        public void SendCreateResourcePlanMails(string strApproveRejectLink,
                                                string strProjectName,
                                                string strResourcePlanCode,
                                                string strClientName,
                                                string strProjectCode,
                                                string strCurrentUser)
        {
            try
            {
                IRMSEmail rmsEmail = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.ResourcePlan),

                                    Convert.ToInt16(EnumsConstants.EmailFunctionality.CreateRPApproval));

                rmsEmail.Body = string.Format(rmsEmail.Body,
                                         strProjectName,
                                         strClientName,
                                         strApproveRejectLink,
                                         strCurrentUser);

                rmsEmail.Subject = string.Format(rmsEmail.Subject,
                                            strProjectCode,
                                            strClientName,
                                            strProjectName,
                                            strResourcePlanCode);

                rmsEmail.SendEmail(rmsEmail);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RESOURCEPLAN, "SendMailForCreateResourcePlan", EventIDConstants.RAVE_HR_RP_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Method to Send Mail for Edit Resource Plan
        /// </summary>
        public void SendEditResourcePlanMails(string strProjectCode,
                                                string strProjectName,
                                                string strClientName,
                                                string strResourcePlanCode,
                                                string strApproveRejectLink,
                                                RaveHRCollection objGetRPDetailsForMail,
                                                string projectManagerEmailId,
                                                string fileName)
        {
            try
            {

                string strTabularMailBody = GetHTMLForTableData(objGetRPDetailsForMail, string.Empty);

                bool isSuccess = CreateTextFile(fileName, strTabularMailBody);

                if (isSuccess)
                {
                    IRMSEmail rmsEmail = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.ResourcePlan),

                                    Convert.ToInt16(EnumsConstants.EmailFunctionality.EditedRPApproval));

                    rmsEmail.Body = string.Format(rmsEmail.Body,
                                                    strResourcePlanCode,
                                                    strProjectName,
                                                    strClientName,
                                                    strTabularMailBody,
                                                    strApproveRejectLink);

                    rmsEmail.Subject = string.Format(rmsEmail.Subject,
                                                        strProjectCode,
                                                        strProjectName,
                                                        strClientName,
                                                        strResourcePlanCode);
                    rmsEmail.CC.Add(projectManagerEmailId);                    
                    // Rajan Kumar : Issue 46055 : 31/12/2013 : Starts                        			 
                    // Desc : Mrf Summary - If RP role is deleted without MRF then mail should not trigger                    
                    if (!string.IsNullOrEmpty(strTabularMailBody))
                    {                        
                    // Rajan Kumar : Issue 46055 : 31/12/2013: Ends 
                        rmsEmail.SendEmail(rmsEmail);
                    }

                }
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RESOURCEPLAN, "SendMailForCreateResourcePlan", EventIDConstants.RAVE_HR_RP_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Method to Send Mail for Approve Reject Resource Plan
        /// </summary>
        public void SendApproveRejectResourcePlanMails(string strCurrentUser,
                                                        string strApproveOrReject,
                                                        string strProjectCode,
                                                        string strProjectName,
                                                        string strClientName,
                                                        string strResourcePlanCode,
                                                        string strReasonForApproval,
                                                        string strApproveRejectLink,
                                                        string projectManagerEmailId,
                                                        string fileName,
                                                        RaveHRCollection raveHRCollection)
        {
            try
            {

                string strTabularMailBody = GetHTMLForTableData(raveHRCollection, strApproveOrReject);

                string tableContent = ReadTextFile(fileName);

                IRMSEmail rmsEmail = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.ResourcePlan),

                                    Convert.ToInt16(EnumsConstants.EmailFunctionality.ApprovedRejectedRP));
                //To solved issue id 20406
                //Start
                //if (tableContent.Trim() != string.Empty)
                //    rmsEmail.CC.Add(CommonConstants.WhizibleGroupEmailId);
                if (tableContent.Trim().Length == 0)
                    rmsEmail.CC.Remove(CommonConstants.WhizibleGroupEmailId);
                //End
                rmsEmail.Body = string.Format(rmsEmail.Body,
                                                strResourcePlanCode,
                                                strProjectName,
                                                strClientName,
                                                strApproveOrReject+"[Comments : '"+ strReasonForApproval +"']",
                                                strTabularMailBody,
                                                tableContent,
                                                strApproveRejectLink,
                                                strCurrentUser);
 

                rmsEmail.Subject = string.Format(rmsEmail.Subject,
                                                 strApproveOrReject,
                                                 strProjectCode,
                                                 strClientName,
                                                 strProjectName,
                                                 strResourcePlanCode);

                rmsEmail.CC.Add(projectManagerEmailId);

                rmsEmail.SendEmail(rmsEmail);
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RESOURCEPLAN, "SendMailForCreateResourcePlan", EventIDConstants.RAVE_HR_RP_BUSNIESS_LAYER);
            }

        }

        private bool CreateTextFile(string fileName, string txtFileContent)
        {
            bool isCreated = false;
            try
            {
                FileInfo t = new FileInfo(fileName);
                StreamWriter Tex = t.CreateText();
                Tex.WriteLine(txtFileContent);
                Tex.Close();
                isCreated = true;
                return isCreated;
            }

            catch (Exception ex)
            {
                isCreated = false;
                throw ex;
            }
        }

        private string ReadTextFile(string fileName)
        {
            string input = string.Empty; ;

            if (File.Exists(fileName))
            {
                StreamReader re = File.OpenText(fileName);
                
                input = re.ReadLine();

                re.Close();
                
                File.Delete(fileName);
            }
            return input;
        }

        private RaveHRCollection GetProjectByRP(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();

            return objDALResourcePlan.GetProjectByRPId(objBEResourcePlan);
        }

        private string GetHTMLForTableData(RaveHRCollection objGetRPDetailsForMail, string strApproveOrReject)
        {

            string bodyTable = "";

            if (objGetRPDetailsForMail.Count > 0)
            {
                bodyTable += "<table cellspacing = '0' style = 'font-family: Verdana; fontstyle:Bold;'><tr><td style = 'font-family: Verdana; fontstyle:Bold;	font-size: 9pt;	padding-left: 5px;vertical-align:top;'>Resource Edited Details:<td/><tr/><table>";

                string multipleProjects = objGetRPDetailsForMail.Count == 1 ? "Details is" : "Details are";

                bodyTable = string.Format(bodyTable, multipleProjects);

                if (objGetRPDetailsForMail.Count > 0)
                {
                    //Rajan : Issue 48242 : 10/01/2013 : Start
                    //UnCommented following code for not to show Utilisation and Billing details in case of EditRP
                    //In "Resource Plan Edited" mail add Util and Billing change section in mail body.                  

                    string[] header = CreateHeader(objGetRPDetailsForMail);
                    string[,] arrayData = new string[(objGetRPDetailsForMail.Count), header.Length];
                    int rowCounter = 0;
                    //Rajan : Issue 48242 : 10/01/2013 : End

                    #region "Code commented beacuse creted gerneric code for Header "
                    //Rajan : Issue 48242 : 10/01/2013 : Start
                    //Commented because created generic header name function
                    //If billing and utilization is changed then only we need to add billing and utilization header in email.
                    //In "Resource Plan Edited" mail add Util and Billing change section in mail body. 


                    //// Aarohi : Issue 31510(CR) : 21/12/2011
                    //// Change 'header' array size from 12 to 8                    
                    //string[] header = new string[8];

                    //header[0] = "Resource Name";

                    //header[1] = "Prev Role";

                    //header[2] = "Role";

                    //// Aarohi : Issue 31510(CR) : 21/12/2011 : Start
                    //// Changed column heading for not showing the Utilisation and Billing details in case of EditRP                    

                    //header[3] = "PrevStartDate";// Previous Column Name - "PrevUtil.(%)"

                    //header[4] = "StartDate";// Previous Column Name - "Util.(%)"

                    //header[5] = "PrevEndDate";// Previous Column Name - "PrevBill.(%)"

                    //header[6] = "EndDate";// Previous Column Name - "Bill.(%)"

                    //header[7] = "Status";// Previous Column Name - "PrevStartDate"

                    //// Commented following as not required
                    ////header[8] = "StartDate";

                    ////header[9] = "PrevEndDate";

                    ////header[10] = "EndDate";

                    ////header[11] = "Status";

                    ////Change 'arrayData' array size from 12 to 8                    
                    //string[,] arrayData = new string[(objGetRPDetailsForMail.Count), 8];
                    ////Aarohi : Issue 31510(CR) : 21/12/2011 : End
                    #endregion                    


                    foreach (BusinessEntities.ResourcePlan resourcePlan in objGetRPDetailsForMail)
                    {
                        if (resourcePlan.PreviousRole != null && resourcePlan.RPDurationStatusId.ToString().Trim() != "Deleted" && resourcePlan.RPDurationStatusId.ToString() != null && resourcePlan.RPDurationStatusId.ToString() != string.Empty)
                        {
                            //Venkatesh : 57878 : 29/04/2016 :Start
                            arrayData[rowCounter, 0] = resourcePlan.ResourceName;

                            arrayData[rowCounter, 1] = resourcePlan.PreviousRole.ToString();

                            arrayData[rowCounter, 2] = resourcePlan.Role.ToString();


                            //Aarohi : Issue 31510(CR) : 21/12/2011 : Start
                            //Commented the below lines of Code to fetch correct data
                            
                            //arrayData[rowCounter, 3] = resourcePlan.PreviousUtilization.ToString();
                        //    arrayData[rowCounter, 3] = resourcePlan.PreviousResourceStartDate.ToString(CommonConstants.DATE_FORMAT);

                            //arrayData[rowCounter, 4] = resourcePlan.Utilization.ToString();
                            arrayData[rowCounter, 3] = resourcePlan.ResourceStartDate.ToString(CommonConstants.DATE_FORMAT); ;

                            //arrayData[rowCounter, 5] = resourcePlan.PreviousBilling.ToString();

                            

                            arrayData[rowCounter, 4] = resourcePlan.PreviousResourceEndDate.ToString(CommonConstants.DATE_FORMAT); ;

                            //arrayData[rowCounter, 6] = resourcePlan.Billing.ToString();
                            arrayData[rowCounter, 5] = resourcePlan.ResourceEndDate.ToString(CommonConstants.DATE_FORMAT); ;

                            // Commented following as not required these columns
                            //arrayData[rowCounter, 7] = resourcePlan.PreviousResourceStartDate.ToString(CommonConstants.DATE_FORMAT);

                            //arrayData[rowCounter, 8] = resourcePlan.ResourceStartDate.ToString(CommonConstants.DATE_FORMAT); ;

                            //arrayData[rowCounter, 9] = resourcePlan.PreviousResourceEndDate.ToString(CommonConstants.DATE_FORMAT); ;

                            //arrayData[rowCounter, 10] = resourcePlan.ResourceEndDate.ToString(CommonConstants.DATE_FORMAT); ;

                            //Rajan : Issue 48242 : 10/01/2013 : Start
                            //UnCommented following code for not to show Utilisation and Billing details in case of EditRP
                            //In "Resource Plan Edited" mail add Util and Billing change section in mail body. 
                            //assign resource plan code to businessentity object
                            if (header != null)
                            {
                                if (header.Length == 10)
                                {
                                    if (resourcePlan.IsUtilizationValueChanged)
                                    {
                                        arrayData[rowCounter, 6] = resourcePlan.PreviousUtilization.ToString();
                                        arrayData[rowCounter, 7] = resourcePlan.Utilization.ToString();
                                    }
                                    else
                                    {
                                        arrayData[rowCounter, 6] = resourcePlan.PreviousUtilization.ToString();
                                        arrayData[rowCounter, 7] = "No Change";
                                    }
                                    if (resourcePlan.IsBillingValueChanged)
                                    {
                                        arrayData[rowCounter, 8] = resourcePlan.PreviousBilling.ToString();
                                        arrayData[rowCounter, 9] = resourcePlan.Billing.ToString();
                                    }
                                    else
                                    {
                                        arrayData[rowCounter, 8] = resourcePlan.PreviousBilling.ToString();
                                        arrayData[rowCounter, 9] = "No Change";
                                    }
                                }
                            }
                            //Venkatesh : 57878 : 29/04/2016 :End
                            //Status is not required as per new requirement Issue 48242

                            //if ((resourcePlan.PreviousRole.ToString() == resourcePlan.Role.ToString()) &&
                            //    //(resourcePlan.PreviousUtilization.ToString() == resourcePlan.Utilization.ToString()) && (resourcePlan.PreviousBilling.ToString() == resourcePlan.Billing.ToString())
                            //    (resourcePlan.PreviousResourceStartDate.ToString() == resourcePlan.ResourceStartDate.ToString()) && (resourcePlan.PreviousResourceEndDate.ToString() == resourcePlan.ResourceEndDate.ToString()))
                            //{
                            //    arrayData[rowCounter, 7] = "Added";//Change 'arrayData' array position from 11 to 7   
                            //}
                            //else
                            //{
                            //    arrayData[rowCounter, 7] = resourcePlan.RPDurationStatusId.ToString();//Change 'arrayData' array position from 11 to 7   
                            //}


                            //if (strApproveOrReject == "Approved")
                            //{
                            //    arrayData[rowCounter, 7] = "Approved";//Change 'arrayData' array position from 11 to 7   
                            //}

                            //Rajan : Issue 48242 : 10/01/2013 : End



                            //Aarohi : Issue 31510(CR) : 21/12/2011 : End

                            rowCounter++;
                        }
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

                string newBodyTable = string.Empty;

                if (bodyTable != "")

                    newBodyTable = "<br/><br/>";

                newBodyTable += "<table cellspacing = '0' style = 'font-family: Verdana; fontstyle:Bold;' ><tr><td style = 'font-size: 9pt; font-family: Verdana; fontstyle:Bold; padding-left: 5px;vertical-align:top;'>New Role Added: <td/><tr/><table>";

                if (objGetRPDetailsForMail.Count > 0)
                {

                    string[] newHeader = new string[6];

                    newHeader[0] = "Resource Name";

                    //Ishwar Patil 51996 09092014 Start
                    //newHeader[1] = "Role";
                    newHeader[1] = "Designation";
                    //Ishwar Patil 51996 09092014 End

                    newHeader[2] = "Util.(%)";

                    newHeader[3] = "Bill.(%)";

                    newHeader[4] = "StartDate";

                    newHeader[5] = "EndDate";


                    string[,] newJoinedArrayData = new string[(objGetRPDetailsForMail.Count), 6];

                    int rowCounter = 0;

                    foreach (BusinessEntities.ResourcePlanDuration resourcePlan in objGetRPDetailsForMail)
                    {
                        if (resourcePlan.PreviousRole == null && resourcePlan.RPDurationStatusId == null)
                        {
                            newJoinedArrayData[rowCounter, 0] = resourcePlan.ResourceName;

                            newJoinedArrayData[rowCounter, 1] = resourcePlan.Role.ToString();



                            newJoinedArrayData[rowCounter, 2] = resourcePlan.Utilization.ToString();

                            newJoinedArrayData[rowCounter, 3] = resourcePlan.Billing.ToString();

                            newJoinedArrayData[rowCounter, 4] = resourcePlan.ResourceStartDate.ToString(CommonConstants.DATE_FORMAT); ;

                            newJoinedArrayData[rowCounter, 5] = resourcePlan.ResourceEndDate.ToString(CommonConstants.DATE_FORMAT); ;

                            rowCounter++;
                        }
                    }



                    IEmailTableData objNewEmailTableData = new EmailTableData();

                    objNewEmailTableData.Header = newHeader;

                    objNewEmailTableData.RowDetail = newJoinedArrayData;

                    objNewEmailTableData.RowCount = rowCounter;

                    newBodyTable += objNewEmailTableData.GetTableData(objNewEmailTableData);

                    if (rowCounter <= 0)
                    {
                        newBodyTable = "";
                    }
                }

                bodyTable += newBodyTable;
            }
            return bodyTable;

        }
        //Rajan : Issue 48242 : 10/01/2013 : Start
        /// <summary>
        /// Its create the header for the email
        /// </summary>
        /// <param name="objGetRPDetailsForMail"></param>
        /// <returns></returns>
        private string[] CreateHeader(RaveHRCollection objGetRPDetailsForMail)
        {
            string[] header = null;
            // Rakesh 57966 Converted Array to List Collection
            List<string> lstHeader = new List<string>();
            bool utilization = false;
            bool billing = false;
            //57966 Rakesh 
            bool IsResourceAvailable = false;
            foreach (BusinessEntities.ResourcePlan resourcePlan in objGetRPDetailsForMail)
            {
                // ~::~   57966 : Rakesh | To Check Resource Available or not ~::~
                if (resourcePlan.ResourceName != "NA")
                    IsResourceAvailable = true;

                if (resourcePlan.PreviousRole != null && resourcePlan.RPDurationStatusId.ToString().Trim() != "Deleted" && resourcePlan.RPDurationStatusId.ToString() != null && resourcePlan.RPDurationStatusId.ToString() != string.Empty)
                {
                    if (resourcePlan.IsUtilizationValueChanged || resourcePlan.IsBillingValueChanged)
                    {
                        utilization = true;
                        billing = true;
                    }
                }
            }
            //Venkatesh : 57878 : 29/04/2016 :Start
            if (utilization || billing)
            {
                header = new string[9];
            }
            else
            {
                header = new string[5];
            }
            // Rakesh: 57966   To Check Resource Available or not  16-05-2016 start 
            lstHeader.Add("Resource Name");
            if (IsResourceAvailable)
            {
                lstHeader.Add("Prev Designation");
                lstHeader.Add("Designation");
              //  lstHeader.Add("Prev AllocationDate");
                lstHeader.Add("AllocationDate");
            }
            else
            {
                lstHeader.Add("Prev Role");
                lstHeader.Add("Role");
              //  lstHeader.Add("PrevStartDate");
                lstHeader.Add("StartDate");
            }           
         
         
            lstHeader.Add("PrevEndDate");
            lstHeader.Add("EndDate");
          

            if (utilization || billing)
            {
                lstHeader.Add("PrevUtil.(%)");
                lstHeader.Add("Util.(%)");
                lstHeader.Add("PrevBill.(%)");
                lstHeader.Add("Bill.(%)");
            }
            //End


            //header[0] = "Resource Name";
            ////Ishwar Patil 51996 09092014 Start
            //header[1] = "Prev Role";
            //header[2] = "Role";
            ////header[1] = "Prev Designation";
            ////header[1] = "Designation";
            ////Ishwar Patil 51996 09092014 End
            
            //header[3] = "PrevStartDate";
            //header[4] = "StartDate";

            ////header[2] = "Allocated Date";

            //header[5] = "PrevEndDate";
            //header[6] = "EndDate";
            //if (utilization || billing)
            //{
            //    header[5] = "PrevUtil.(%)";
            //    header[6] = "Util.(%)";
            //    header[7] = "PrevBill.(%)";
            //    header[8] = "Bill.(%)";
            //}
            //Venkatesh : 57878 : 29/04/2016 :End
            return lstHeader.ToArray();
        }
        //Rajan : Issue 48242 : 10/01/2013 : End
        private bool ValidateRPIsActive(int projectId, string strResourcePlanId)
        {
            //assign resourcePlanId
            BusinessEntities.RaveHRCollection objListRPIdByProjectId = GetResourcePlanIdByProjectId(Mode.IsDeleted.ToString(), projectId, strResourcePlanId);
            if (objListRPIdByProjectId.Count > 0)
                return true;
            else
                return false;
        }

        private BusinessEntities.RaveHRCollection GetResourcePlanIdByProjectId(string mode, int projectId, string strResourcePlanId)
        {
            //declare BusinessEntities.ResourcePlan object
            BusinessEntities.ResourcePlan objBEResourcePlan = new BusinessEntities.ResourcePlan();
            objBEResourcePlan.RPId = Convert.ToInt32(strResourcePlanId);
            objBEResourcePlan.ProjectId = projectId;
            objBEResourcePlan.Mode = mode;
            //declare Rave.HR.BusinessLayer.Projects.ResourcePlan object
            Rave.HR.BusinessLayer.Projects.ResourcePlan objBLLResourcePlan = new Rave.HR.BusinessLayer.Projects.ResourcePlan();

            //call method to get resource plan count by project id
            return (GetResourcePlanForProjectId(objBEResourcePlan));

        }

        /// <summary>
        /// Gets the RP edited details for mail.
        /// </summary>
        /// <param name="objBEResourcePlan">The obj BE resource plan.</param>
        /// <returns></returns>
        public RaveHRCollection GetRPEditedDetailsForMail(BusinessEntities.ResourcePlan objBEResourcePlan)
        {
            //create object of dataaccesslayer of resourceplan
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();

            // Initialise the Collection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();


            //method to get resourceplan for ApproveRejectRP
            raveHRCollection = objDALResourcePlan.GetRPDetailsForMail(objBEResourcePlan);

            //return collection object.
            return raveHRCollection;
        }

        //RP Issue 33126 Start
        /// <summary>
        /// Gets the RP edited details for mail.
        /// </summary>
        /// <param name="objBEResourcePlan">The obj BE resource plan.</param>
        /// <returns></returns>
        public int? GetRPApprovalStatus(int ResourcePlanId)
        {
            //create object of dataaccesslayer of resourceplan
            objDALResourcePlan = new Rave.HR.DataAccessLayer.Projects.ResourcePlan();

            //method to get resourceplan for ApproveRejectRP
            int? RPApprovalStatus = objDALResourcePlan.GetRPApprovalStatus(ResourcePlanId);

            //return collection object.
            return RPApprovalStatus;
        }

        //RP Issue 33126 End
    }
}
