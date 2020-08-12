//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           MRFDetail.aspx      
//  Author:         Chhaya Gunjal 
//  Date written:   03/09/2009/ 10:58:30 AM
//  Description:    This class  provides the Business Access layer methods for Recruitment module.
//
//  Amendments
//  Date                    Who              Ref     Description
//  ----                    -----------      ---     -----------
//  07/10/2009 10:58:30 AM  Chhaya Gunjal    n/a     Created    
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Common.AuthorizationManager;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using Rave.HR.BusinessLayer.Interface;
using System.Web;
using System.Collections;

namespace Rave.HR.BusinessLayer.Recruitment
{
    public class Recruitment
    {
        #region Constants

        private const string RECRUITMENT = "Recruitment";
        private const string GETPROJECTNAME = "GetProjectName";
        private const string GETRECRUITMENTSUMMARYFORPAGELOAD = "GetRecruitmentSummaryForPageLoad";
        private const string GETRECRUITMENTSUMMARYFORFILTERANDPAGING = "GetRecruitmentSummaryForFilterAndPaging";
        private const string SENDMAILCANDIDATEJOINED = "SendMailCandidateJoined";
        private const string GETEMAILSUBJECTCANDIDATEJOINED = "GetEMailSubjectCandidateJoined";
        private const string GETEMAILBODYCANDIDATEJOINED = "GetEMailBodyCandidateJoined";
        private const string SENDMAILMESSAGECANDIDATEJOINED = "SendMailMesssageCandidateJoined";
        private const string SENDMAIL = "SendMail";
        private const string GETEMAILSUBJECT = "GetEMailSubject";
        private const string GETEMAILBODY = "GetEMailBody";
        private const string SENDMAILMESSAGE = "SendMailMesssage";
        private const string GETMRFDETAIL = "GetMrfDetail";
        private const string GETMRFRESPONSIBLEPERSON = "GetMRfResponsiblePersonName";
        private const string REMOVEPIPELINEDETAILS = "RemovePipelineDetails";
        private const string GETRECRUITMENTDETAIL = "GetRecruitmentDetail";
        private const string ADDPIPELINEDETAILS = "AddPipelineDetails";
        private const string GETMRFCODE = "GetMrfCode";
        private const string VIEWMRFCODE = "ViewMrfCode";
        private const string GETMRFDETAILFOREMPLOYEE = "GetMrfDetailForEmployee";
        private const string BENGULURU = "Benguluru";
        private const string SENDEMAIL_CANDIDATE_UPDATED = "SendEmailForCandidateDetailsUpdated";
        private const string SENDEMAIL_CANDIDATE_JOINED = "SendsMailForCandidateJoined";
        private const string SENDEMAIL_CANDIDATE_REMOVED = "SendEmailCandidateRemoved";
        private const string SENDEMAIL_CANDIDATE_ADDED = "SendEmailForCandidateAdded";
        private const string GET_HTML_TABLE_DATA = "GetHTMLForTableData";
        private const string GET_HTML_CANDIDATEDETAILS_EDITED_TABLE_DATA = "GetHTMLForCandidateDetailsEditedTableData";

        #endregion

        #region Variable
        private static string subject;
        private static string body;
        AuthorizationManager objAuMan = new AuthorizationManager();
        private const string PROJECT = "Projects";
        private static string message = string.Empty;
        private const string _index = "index";
        private const string _zero = "0";
        // 29503-Ambar-Start-30082011
        private const int default_project_id = 0;
        // 29503-Ambar-End-30082011

        //Declaring Master Class Object
        Rave.HR.BusinessLayer.Common.Master master = new Rave.HR.BusinessLayer.Common.Master();
        #endregion

        //Rajan Kumar : Issue 39508: 05/02/2014 : Starts                        			 
        // Desc : Traninig for new joining employee. (Training Gaps).    
        #region ENUM
        public enum TraningSubjectChanged
        {
            Add = 0,
            Edit = 1,
            Delete = 2
        }
        #endregion
        // Rajan Kumar : Issue 39508: 05/02/2014 : END

        #region Public Methods

        /// <summary>
        /// Gets the Project Name.
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetProjectName()
        {
            // Initialise the Data Layer object
            DataAccessLayer.Recruitment.Recruitment projectNameDL = new Rave.HR.DataAccessLayer.Recruitment.Recruitment();

            // Initialise the Collection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
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
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RECRUITMENT, GETPROJECTNAME, EventIDConstants.RAVE_HR_RECRUITMENT_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Gets the Recruitment details.
        /// </summary>
        public BusinessEntities.RaveHRCollection GetRecruitmentSummaryForPageLoad(BusinessEntities.ParameterCriteria objParameter, ref int pageCount)
        {
            // Initialise the Data Layer object
            DataAccessLayer.Recruitment.Recruitment recruitmentDL = new Rave.HR.DataAccessLayer.Recruitment.Recruitment();

            // Initialise the Colection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
            try
            {
                // Call the Data Layer method
                raveHRCollection = recruitmentDL.GetRecruitmentSummaryForPageLoad(objParameter, ref pageCount);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RECRUITMENT, GETRECRUITMENTSUMMARYFORPAGELOAD, EventIDConstants.RAVE_HR_RECRUITMENT_BUSNIESS_LAYER);
            }
            // Return the Collection
            return raveHRCollection;
        }

        /// <summary>
        /// Gets the Recruitment details for filter and pending allocation.
        /// </summary>
        public BusinessEntities.RaveHRCollection GetRecruitmentSummaryForFilterAndPaging(BusinessEntities.ParameterCriteria objParameter, BusinessEntities.Recruitment recruitment, ref int pageCount)
        {
            // Initialise the Data Layer object
            DataAccessLayer.Recruitment.Recruitment recruitmentDL = new Rave.HR.DataAccessLayer.Recruitment.Recruitment();

            // Initialise the Colection class object
            BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                // Call the Data Layer method
                raveHRCollection = recruitmentDL.GetRecruitmentSummaryForFilterAndPaging(objParameter, recruitment, ref pageCount);
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RECRUITMENT, GETRECRUITMENTSUMMARYFORFILTERANDPAGING, EventIDConstants.RAVE_HR_RECRUITMENT_BUSNIESS_LAYER);
            }

            // Return the Collection
            return raveHRCollection;
        }

        /// <summary>
        /// Gets the MRF Detaill for particular MRF.
        /// </summary>
        public static BusinessEntities.Recruitment GetMrfDetail(int mrfId)
        {
            try
            {
                BusinessEntities.Recruitment recruitment = Rave.HR.DataAccessLayer.Recruitment.Recruitment.GetMrfDetail(mrfId);
                return recruitment;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RECRUITMENT, GETMRFDETAIL, EventIDConstants.RAVE_HR_RECRUITMENT_BUSNIESS_LAYER);
            }

        }

        /// <summary>
        /// Gets the Reporting Person by using id.
        /// </summary>
        public static string GetMRfResponsiblePersonName(string empid)
        {
            try
            {
                string sname = Rave.HR.DataAccessLayer.Recruitment.Recruitment.GetMRfResponsiblePersonName(empid);
                return sname;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RECRUITMENT, GETMRFRESPONSIBLEPERSON, EventIDConstants.RAVE_HR_RECRUITMENT_BUSNIESS_LAYER);
            }

        }

        /// <summary>
        /// Remove recruitment pipeline details 
        /// </summary>
        public static int RemovePipelineDetails(BusinessEntities.Recruitment recruitment)
        {
            try
            {
                int removePipelineDetails = Rave.HR.DataAccessLayer.Recruitment.Recruitment.RemovePipelineDetails(recruitment);

                //Calling Business layer method when candidate is Removed from pipelinedetails.
                Rave.HR.BusinessLayer.Recruitment.Recruitment.SendEmailCandidateRemoved(recruitment);
                return removePipelineDetails;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RECRUITMENT, REMOVEPIPELINEDETAILS, EventIDConstants.RAVE_HR_RECRUITMENT_BUSNIESS_LAYER);
            }

        }

        /// <summary>
        /// Gets the MRF Detaill for particular Candidate.
        /// </summary>
        /// <returns>Collection</returns>
        public static BusinessEntities.Recruitment GetRecruitmentDetail(int candidateId)
        {
            try
            {
                BusinessEntities.Recruitment recruitment = Rave.HR.DataAccessLayer.Recruitment.Recruitment.GetRecruitmentDetail(candidateId);
                return recruitment;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RECRUITMENT, GETRECRUITMENTDETAIL, EventIDConstants.RAVE_HR_RECRUITMENT_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Gets the MRF Code whose status is pending expected resource join.
        /// </summary>
        /// <returns>Collection</returns>
        public static BusinessEntities.RaveHRCollection ViewMrfCode()
        {
            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = Rave.HR.DataAccessLayer.Recruitment.Recruitment.ViewMrfCode();
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RECRUITMENT, GETRECRUITMENTDETAIL, EventIDConstants.RAVE_HR_RECRUITMENT_BUSNIESS_LAYER);
            }
        }

        /// <summary>
        /// Add recruitment pipeline details 
        /// </summary>
        public static int AddPipelineDetails(BusinessEntities.Recruitment recruitment)
        {
            try
            {
                int isPipelinedDetailsAdded = Rave.HR.DataAccessLayer.Recruitment.Recruitment.AddPipelineDetails(recruitment);
                BusinessEntities.Employee departmentHeadDetails = new BusinessEntities.Employee();
                SendEmailForCandidateAdded(recruitment);
                //Rajan Kumar : Issue 39508: 05/02/2014 : Starts                        			 
                // Desc : Traninig for new joining employee. (Training Gaps).
                if (recruitment.IsTrainingRequired)
                {
                    string mode = TraningSubjectChanged.Add.ToString();
                    SendEmailForCandidateSkillGaps(recruitment, mode);
                }
                // Rajan Kumar : Issue 39508: 05/02/2014 : END            
                return isPipelinedDetailsAdded;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RECRUITMENT, ADDPIPELINEDETAILS, EventIDConstants.RAVE_HR_RECRUITMENT_BUSNIESS_LAYER);
            }

        }

        /// <summary>
        /// Gets the MRF Code whose status is pending external allocation.
        /// </summary>
        public static BusinessEntities.RaveHRCollection GetMrfCode()
        {
            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = Rave.HR.DataAccessLayer.Recruitment.Recruitment.GetMrfCode();
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RECRUITMENT, GETMRFCODE, EventIDConstants.RAVE_HR_RECRUITMENT_BUSNIESS_LAYER);
            }


        }

        #region Email Notification
        /// <summary>
        /// When Candidate is Deleted from pipeline, Email is send.
        /// </summary>
        public static void SendEmailCandidateRemoved(BusinessEntities.Recruitment objRecruitment)
        {
            BusinessEntities.RaveHRCollection raveHRCollectionProject = new BusinessEntities.RaveHRCollection();
            BusinessEntities.RaveHRCollection raveHRCollectionDepartment = new BusinessEntities.RaveHRCollection();
            string projectManagerEmail = string.Empty;
            string departmentHeadEmail = string.Empty;
            //Get the loggedin user name
            AuthorizationManager objAuMgr = new AuthorizationManager();
            //string strFromUser = objAuMgr.getLoggedInUserEmailId();
            string username = "";
            string strFromUser = objAuMgr.getLoggedInUser();
            //GoogleMail
            //if (strFromUser.ToLower().Contains("@rave-tech.co.in"))
            //{
            //    username = objAuMgr.GetDomainUserName(strFromUser.Replace(RAVEDOMAIN, ""));
            //}
            //else
            //{
            //    username = objAuMgr.GetDomainUserName(strFromUser.Replace(NORTHGATEDOMAIN, ""));
            //}
            //going google
            //string username = objAuMgr.GetDomainUserName(strFromUser.Replace(RAVEDOMAIN, ""));
            username = objAuMgr.GetDomainUserName(strFromUser);


            string ExpjoinedDate = objRecruitment.ExpectedJoiningDate.ToShortDateString();

            //Get Server URL
            string strRecruitmentSummaryLink = Utility.GetUrl() + CommonConstants.Page_RecruitmentSummary;
            IRMSEmail objEmail;

            if (objRecruitment.Location == CommonConstants.BENGULURU)

                objEmail = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Recruitment),
                                                 Convert.ToInt16(EnumsConstants.EmailFunctionality.CandidateDeleted),
                                                 objRecruitment.Location);
            else

                objEmail = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Recruitment),
                                                 Convert.ToInt16(EnumsConstants.EmailFunctionality.CandidateDeleted));
            if (objRecruitment.Department == PROJECT)
            {
                raveHRCollectionProject = DataAccessLayer.Recruitment.Recruitment.GetProjectManagerByProjectId(objRecruitment);
            }
            else
            {
                raveHRCollectionProject = DataAccessLayer.Recruitment.Recruitment.GetProjectManagerByProjectId(objRecruitment);
                raveHRCollectionDepartment = DataAccessLayer.Recruitment.Recruitment.GetDepartmentHeadByDepartmentId(objRecruitment);
            }

            try
            {
                //Assigned the variable to set the correct date in mail table.
                objRecruitment.ResourceJoinedDate = objRecruitment.ExpectedJoiningDate;

                string tableBody = GetHTMLForTableData(objRecruitment);

                if (raveHRCollectionProject.Count > 0)
                {
                    //Get ProjectManager Name 

                    foreach (BusinessEntities.Recruitment objrer in raveHRCollectionProject)
                    {
                        projectManagerEmail += objrer.EmailId;
                        objRecruitment.EmailId = projectManagerEmail;

                    }
                    if (objRecruitment.EmailId.EndsWith(","))
                    {
                        objRecruitment.EmailId = objRecruitment.EmailId.Substring(0, objRecruitment.EmailId.Length - 1);
                    }

                    objEmail.To.Add(objRecruitment.EmailId);
                }

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

                    objEmail.To.Add(objRecruitment.EmailId);
                }

                //Mahendra : Issue Id : 44284 : Start
                //DESC : FOR NIS and Northgate projects add Sai name in CC.
                if (objRecruitment.ClientName.ToUpper().Contains("NPS") || objRecruitment.ClientName.ToUpper().Contains("NORTHGATE"))
                {
                    objEmail.CC.Add(CommonConstants.SAIGIDDUNISandNAVYANISEmailId);
                    //objEmail.CC.Add(CommonConstants.SAIGIDDUNISEmailId);
                    //// Mohamed : NIS-RMS : 02/01/2015 : Starts                        			  
                    //// Desc : Add Naviya name where there is Sai name "navya.annamraju@northgateps.com"				
                    //objEmail.CC.Add(CommonConstants.NAVYANISEmailId);
                    // Mohamed : NIS-RMS : 02/01/2015 : Ends
                }
                #region Modified By Mohamed Dangra
                // Mohamed : Issue 52218 : 27/08/2014 : Starts                        			  
                // Desc : Line Manger(Accountable To) of MRF should be in cc -- Candidate not joining mail does not gone to line manager (Umesh) although Candidate Expected to join does.
                DataAccessLayer.Contracts.Contract objDataAccessContract = new Rave.HR.DataAccessLayer.Contracts.Contract();

                if (objRecruitment.ReportingTo != null)
                {
                    objEmail.CC.Add(objDataAccessContract.GetEmailID(Convert.ToInt32(objRecruitment.ReportingTo)));
                }
                // Mohamed : Issue 52218 : 27/08/2014 : Ends
                #endregion Modified By Mohamed Dangra


                objEmail.Subject = string.Format(objEmail.Subject,
                                                         objRecruitment.MRFCode,
                                                         ((objRecruitment.Department == PROJECT) ? "Project" : "Department"),
                                                         ((objRecruitment.Department == PROJECT) ? objRecruitment.ProjectName : objRecruitment.Department));


                objEmail.Body = string.Format(objEmail.Body,
                                              objRecruitment.Prefix,
                                              (objRecruitment.FirstName + " " + objRecruitment.LastName),
                                              ExpjoinedDate,
                                              objRecruitment.Role,
                                              tableBody,
                                              username
                    //objRecruitment.Reason
                                              );



                message = "Pipelines details against MRF  [" + objRecruitment.MRFCode + "]  is been successfully deleted, email notification is sent.";
                HttpContext.Current.Session[SessionNames.CONFIRMATION_MESSAGE_CANDIDATE_DELETED] = message;

                //Calling method for mailing
                objEmail.SendEmail(objEmail);
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RECRUITMENT, SENDEMAIL_CANDIDATE_REMOVED, EventIDConstants.RAVE_HR_RECRUITMENT_BUSNIESS_LAYER);
            }
        }



        /// <summary>
        /// Gets the MRF Detaill for particular MRF.
        /// sudip
        /// </summary>
        public BusinessEntities.Recruitment GetMrfDetailForEmployee(int mrfId)
        {
            BusinessEntities.Recruitment recruitment = null;
            Rave.HR.DataAccessLayer.Recruitment.Recruitment rectDL = new Rave.HR.DataAccessLayer.Recruitment.Recruitment();

            try
            {
                recruitment = rectDL.GetMrfDetailForEmployee(mrfId);

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RECRUITMENT, GETMRFDETAILFOREMPLOYEE, EventIDConstants.RAVE_HR_RECRUITMENT_BUSNIESS_LAYER);
            }

            return recruitment;
        }

        /// <summary>
        /// Edit recruitment pipeline details 
        /// </summary>
        public static int EditPipelineDetails(BusinessEntities.Recruitment recruitment, string previousMRFCode, bool IsMailSentStatus, bool SendMail, bool ChkDesignationAndJoiningDate, string mode)
        {
            try
            {
                // 35913-Ambar-06072012-Start-Check if candidate is joined is updated or not.
                // this flag is use decide whether to send candidate joined mail or not
                bool BIsCandidateJoined = DataAccessLayer.Recruitment.Recruitment.GetCandidateJoinStatus(recruitment.CandidateId);
                // 35913-Ambar-06072012-End 

                int EditPipelineDetails = Rave.HR.DataAccessLayer.Recruitment.Recruitment.EditPipelineDetails(recruitment, previousMRFCode);

                if (EditPipelineDetails != 0)
                {
                    //28568-Subhra-Start
                    //if (Convert.ToBoolean(IsMailSentStatus) == true)
                    //{
                    //calling mail method
                    // 35913-Ambar-06072012-Added a parameter to following

                    // Mohamed : Issue 50306 : 09/09/2014 : Starts                        			  
                    // Desc : Expected Joinee's details edited[MRF Code: MRF_Testing_SrTstA_0387] old  Joining date is default date -- Mail for de-linking MRF
                    if (recruitment.MRFCode.Equals(previousMRFCode))
                    {
                        // Jignyasa : Issue 42211 : 6/06/2013 : Starts 
                        // Desc : Check if current effective joining date is same as previous effective joining date and current designation id is same as previous designation id           
                        if (!ChkDesignationAndJoiningDate)
                        {
                            SendEmailForCandidateDetailsUpdated(recruitment, BIsCandidateJoined);
                        }
                    }
                    else
                    {
                        SendEmailForDeLinkMRF(recruitment, BIsCandidateJoined, previousMRFCode);
                    }

                    // Mohamed : Issue 50306 : 09/09/2014 : Ends

                    //Rajan Kumar : Issue 39508: 05/02/2014 : Starts                        			 
                    // Desc : Traninig for new joining employee. (Training Gaps).
                    if (!string.IsNullOrEmpty(mode))
                    {
                        SendEmailForCandidateSkillGaps(recruitment, mode);
                    }
                    // Rajan Kumar : Issue 39508: 05/02/2014 : END
                    //}
                    //else
                    //28568-Subhra-End
                    Rave.HR.BusinessLayer.Recruitment.Recruitment.message = "Pipeline details  updated  against MRF  [" + recruitment.MRFCode + "]  has been successfully updated.";
                    HttpContext.Current.Session[SessionNames.CONFIRMATION_MESSAGE_CANDIDATE_UPDATED] = message;

                }
                HttpContext.Current.Session[SessionNames.CONFIRMATION_MESSAGE_CANDIDATE_UPDATED] = message;
                return EditPipelineDetails;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RECRUITMENT, REMOVEPIPELINEDETAILS, EventIDConstants.RAVE_HR_RECRUITMENT_BUSNIESS_LAYER);
            }

        }

        /// <summary>
        /// Returns link for Recruitment Summary.
        /// </summary>
        /// <param name="strLink"></param>
        /// <returns></returns>
        private static string GetURLUserName(string strLink)
        {
            //Get Server URL
            string strRecruitmentSummaryLink = Utility.GetUrl() + CommonConstants.Page_RecruitmentSummary;
            return strRecruitmentSummaryLink;

        }

        // Mohamed : Issue 50306 : 09/09/2014 : Starts                        			  
        // Desc : Expected Joinee's details edited[MRF Code: MRF_Testing_SrTstA_0387] old  Joining date is default date -- Mail for de-linking MRF
        /// <summary>
        /// Send mail when MRF de-link (change from 1 MRF to another)
        /// </summary>
        /// <param name="objRecruitment"></param>
        // 35913-Ambar-06072012-Added parameter to the defination of function
        private static void SendEmailForDeLinkMRF(BusinessEntities.Recruitment objRecruitment, bool BIsCandidateJoined, string previousMRFCode)
        {
            try
            {
                bool CandidateNotJoining = false;
                //Get the loggedin user name
                AuthorizationManager objAuMgr = new AuthorizationManager();
                //string strFromUser = objAuMgr.getLoggedInUserEmailId();

                //string username = objAuMgr.GetDomainUserName(strFromUser.Replace(RAVEDOMAIN, ""));
                //going google
                string username = "";
                string strFromUser = objAuMgr.getLoggedInUser();
                //GoogleMail
                //if (strFromUser.ToLower().Contains("@rave-tech.co.in"))
                //{
                //    username = objAuMgr.GetDomainUserName(strFromUser.Replace(RAVEDOMAIN, ""));
                //}
                //else
                //{
                //    username = objAuMgr.GetDomainUserName(strFromUser.Replace(NORTHGATEDOMAIN, ""));
                //}
                username = objAuMgr.GetDomainUserName(strFromUser);

                //28966-Subhra-Start
                BusinessEntities.RaveHRCollection raveHRCollectionProject = new BusinessEntities.RaveHRCollection();
                BusinessEntities.RaveHRCollection raveHRCollectionDepartment = new BusinessEntities.RaveHRCollection();
                string projectManagerEmail = string.Empty;
                string departmentHeadEmail = string.Empty;
                //28966-Subhra-End

                //42211-Check if current effective joining date is same as previous effective joining date
                //Check if current designation id is same as previous designation id
                //if (objRecruitment.Prev_ExpectedJoiningDate == objRecruitment.ExpectedJoiningDate && objRecruitment.Prev_Designation == objRecruitment.Designation && objRecruitment.IsResourceJoined == 0)
                //{
                //    return;
                //}

                string tableData = GetHTMLForDelinkMRF(objRecruitment);

                string strRecruitmentSummaryLink = Utility.GetUrl() +
                                                CommonConstants.Page_RecruitmentPipelineDetails +
                                                "?" +
                                                URLHelper.SecureParameters(CommonConstants.CON_QSTRING_CANDIDATEID, objRecruitment.CandidateId.ToString()) + "&" +
                                                URLHelper.SecureParameters("CandidateNotJoining", CandidateNotJoining.ToString()) + "&" +
                                                URLHelper.SecureParameters(_index, _zero) + "&" +
                                                URLHelper.CreateSignature(objRecruitment.CandidateId.ToString(), CandidateNotJoining.ToString(), _zero);


                IRMSEmail objEmail = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Recruitment),
                                                 Convert.ToInt16(EnumsConstants.EmailFunctionality.MRFDeLinked));
                ////28966-Subhra-Start
                //if (objRecruitment.Location == CommonConstants.BENGULURU)

                //    objEmail = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Recruitment),
                //                                 Convert.ToInt16(EnumsConstants.EmailFunctionality.CandidateUpdated),
                //                                 objRecruitment.Location);
                //else

                //    objEmail = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Recruitment),
                //                                     Convert.ToInt16(EnumsConstants.EmailFunctionality.CandidateUpdated));
                // Mohamed : Issue 51087 : 27/05/2014 : Starts                        			  
                // Desc : Expected Joinee's details edited Mail should go to all who receive Expected to join mail
                //if (objRecruitment.Department == PROJECT)
                if (objRecruitment.ProjectId != default_project_id)
                {
                    raveHRCollectionProject = DataAccessLayer.Recruitment.Recruitment.GetProjectManagerByProjectId(objRecruitment);
                    raveHRCollectionDepartment = DataAccessLayer.Recruitment.Recruitment.GetDepartmentHeadByDepartmentId(objRecruitment);
                }
                else
                {
                    //BusinessEntities.Recruitment recruitment = new BusinessEntities.Recruitment();
                    //recruitment.EmailId = DataAccessLayer.Recruitment.Recruitment.GetReportingToByEmployeeId(objRecruitment.CandidateId);
                    //raveHRCollectionProject.Add(recruitment);
                    raveHRCollectionDepartment = DataAccessLayer.Recruitment.Recruitment.GetDepartmentHeadByDepartmentId(objRecruitment);
                }


                if (raveHRCollectionProject.Count > 0)
                {
                    //Get ProjectManager Name 
                    foreach (BusinessEntities.Recruitment objrer in raveHRCollectionProject)
                    {
                        projectManagerEmail += objrer.EmailId;
                        objRecruitment.EmailId = projectManagerEmail;

                    }
                    if (objRecruitment.EmailId.EndsWith(","))
                    {
                        objRecruitment.EmailId = objRecruitment.EmailId.Substring(0, objRecruitment.EmailId.Length - 1);
                    }
                    objEmail.CC.Add(objRecruitment.EmailId);
                }
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
                    objEmail.CC.Add(objRecruitment.EmailId);
                }
                //Gets the Email ID of the person who had created the mrf and keep him/her in CC.
                objEmail.CC.Add(objRecruitment.EmailIdOfCreatedByMRF);

                #region Modified By Mohamed Dangra
                // Mohamed : Issue 41942 : 26/04/2013 : Starts                        			  
                // Desc : Line Manger(Accountable To) of MRF should be in cc
                DataAccessLayer.Contracts.Contract objDataAccessContract = new Rave.HR.DataAccessLayer.Contracts.Contract();
                //Venkatesh : Issue Id : 47714  Start
                //project + dept MRF line manager should be in CC.
                //if (objRecruitment.ProjectId == default_project_id)
                if (objRecruitment.ReportingTo != null)
                {
                    objEmail.CC.Add(objDataAccessContract.GetEmailID(Convert.ToInt32(objRecruitment.ReportingTo)));
                }
                // Mohamed : Issue 41942 : 26/04/2013 : Ends
                #endregion Modified By Mohamed Dangra

                //Add new and old MRF Recruiter's in CC
                DataAccessLayer.Recruitment.Recruitment ObjDataAccessRecruitment = new Rave.HR.DataAccessLayer.Recruitment.Recruitment();
                objEmail.CC.Add(ObjDataAccessRecruitment.GetRecruiterEmailByMRFId(objRecruitment.Prev_MRFId, objRecruitment.MRFId));

                //28966-Subhra-End

                //Mahendra : Issue Id : 44284 : Start
                //DESC : FOR NIS and Northgate projects add Sai name in CC.
                if (objRecruitment.ProjectId != default_project_id)
                {
                    if (objRecruitment.ClientName.ToUpper().Contains("NPS") || objRecruitment.ClientName.ToUpper().Contains("NORTHGATE"))
                    {
                        objEmail.CC.Add(CommonConstants.SAIGIDDUNISandNAVYANISEmailId);
                        //objEmail.CC.Add(CommonConstants.SAIGIDDUNISEmailId);
                        //// Mohamed : NIS-RMS : 02/01/2015 : Starts                        			  
                        //// Desc : Add Naviya name where there is Sai name "navya.annamraju@northgateps.com"				
                        //objEmail.CC.Add(CommonConstants.NAVYANISEmailId);
                        // Mohamed : NIS-RMS : 02/01/2015 : Ends
                    }
                }
                //Mahendra : Issue Id : 44284 : End
                // Mohamed : Issue 51087 : 27/05/2014 : Ends



                objEmail.Subject = string.Format(objEmail.Subject,
                                                 previousMRFCode);

                objEmail.Body = string.Format(objEmail.Body,
                                              objRecruitment.FirstName + " " + objRecruitment.LastName,
                                              previousMRFCode,
                                              objRecruitment.MRFCode,
                                              tableData,
                                              username);

                Rave.HR.BusinessLayer.Recruitment.Recruitment.message = "Pipeline details  updated  against MRF  [" + objRecruitment.MRFCode + "]  has been successfully updated,email notification is sent.";
                HttpContext.Current.Session[SessionNames.CONFIRMATION_MESSAGE_CANDIDATE_ADDED] = message;
                objEmail.SendEmail(objEmail);

                if (objRecruitment.IsResourceJoined == 1)
                {
                    // 35913-Ambar-06072012-Added If condition to avoid multiple mails.
                    if (!BIsCandidateJoined)
                    {
                        //calling method when candidate has joined.
                        Rave.HR.BusinessLayer.Recruitment.Recruitment.SendsMailForCandidateJoined(objRecruitment);
                    }
                }

                HttpContext.Current.Session[SessionNames.CONFIRMATION_MESSAGE] = message;
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RECRUITMENT, SENDEMAIL_CANDIDATE_UPDATED, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }


        }

        // Mohamed : Issue 50306 : 09/09/2014 : Ends

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objRecruitment"></param>
        // 35913-Ambar-06072012-Added parameter to the defination of function
        private static void SendEmailForCandidateDetailsUpdated(BusinessEntities.Recruitment objRecruitment, bool BIsCandidateJoined)
        {
            bool CandidateNotJoining = false;
            //Get the loggedin user name
            AuthorizationManager objAuMgr = new AuthorizationManager();
            //string strFromUser = objAuMgr.getLoggedInUserEmailId();

            //string username = objAuMgr.GetDomainUserName(strFromUser.Replace(RAVEDOMAIN, ""));
            //going google
            string username = "";
            string strFromUser = objAuMgr.getLoggedInUser();
            //GoogleMail
            //if (strFromUser.ToLower().Contains("@rave-tech.co.in"))
            //{
            //    username = objAuMgr.GetDomainUserName(strFromUser.Replace(RAVEDOMAIN, ""));
            //}
            //else
            //{
            //    username = objAuMgr.GetDomainUserName(strFromUser.Replace(NORTHGATEDOMAIN, ""));
            //}
            username = objAuMgr.GetDomainUserName(strFromUser);

            //28966-Subhra-Start
            BusinessEntities.RaveHRCollection raveHRCollectionProject = new BusinessEntities.RaveHRCollection();
            BusinessEntities.RaveHRCollection raveHRCollectionDepartment = new BusinessEntities.RaveHRCollection();
            string projectManagerEmail = string.Empty;
            string departmentHeadEmail = string.Empty;
            //28966-Subhra-End

            //42211-Check if current effective joining date is same as previous effective joining date
            //Check if current designation id is same as previous designation id
            //if (objRecruitment.Prev_ExpectedJoiningDate == objRecruitment.ExpectedJoiningDate && objRecruitment.Prev_Designation == objRecruitment.Designation && objRecruitment.IsResourceJoined == 0)
            //{
            //    return;
            //}

            string tableData = GetHTMLForExpectedDOJEdited(objRecruitment);

            string strRecruitmentSummaryLink = Utility.GetUrl() +
                                            CommonConstants.Page_RecruitmentPipelineDetails +
                                            "?" +
                                            URLHelper.SecureParameters(CommonConstants.CON_QSTRING_CANDIDATEID, objRecruitment.CandidateId.ToString()) + "&" +
                                            URLHelper.SecureParameters("CandidateNotJoining", CandidateNotJoining.ToString()) + "&" +
                                            URLHelper.SecureParameters(_index, _zero) + "&" +
                                            URLHelper.CreateSignature(objRecruitment.CandidateId.ToString(), CandidateNotJoining.ToString(), _zero);


            IRMSEmail objEmail = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Recruitment),
                                             Convert.ToInt16(EnumsConstants.EmailFunctionality.CandidateUpdated));
            //28966-Subhra-Start
            if (objRecruitment.Location == CommonConstants.BENGULURU)

                objEmail = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Recruitment),
                                             Convert.ToInt16(EnumsConstants.EmailFunctionality.CandidateUpdated),
                                             objRecruitment.Location);
            else

                objEmail = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Recruitment),
                                                 Convert.ToInt16(EnumsConstants.EmailFunctionality.CandidateUpdated));
            // Mohamed : Issue 51087 : 27/05/2014 : Starts                        			  
            // Desc : Expected Joinee's details edited Mail should go to all who receive Expected to join mail
            //if (objRecruitment.Department == PROJECT)
            if (objRecruitment.ProjectId != default_project_id)
            {
                raveHRCollectionProject = DataAccessLayer.Recruitment.Recruitment.GetProjectManagerByProjectId(objRecruitment);
                raveHRCollectionDepartment = DataAccessLayer.Recruitment.Recruitment.GetDepartmentHeadByDepartmentId(objRecruitment);
            }
            else
            {
                //BusinessEntities.Recruitment recruitment = new BusinessEntities.Recruitment();
                //recruitment.EmailId = DataAccessLayer.Recruitment.Recruitment.GetReportingToByEmployeeId(objRecruitment.CandidateId);
                //raveHRCollectionProject.Add(recruitment);
                raveHRCollectionDepartment = DataAccessLayer.Recruitment.Recruitment.GetDepartmentHeadByDepartmentId(objRecruitment);
            }

            try
            {
                if (raveHRCollectionProject.Count > 0)
                {
                    //Get ProjectManager Name 
                    foreach (BusinessEntities.Recruitment objrer in raveHRCollectionProject)
                    {
                        projectManagerEmail += objrer.EmailId;
                        objRecruitment.EmailId = projectManagerEmail;

                    }
                    if (objRecruitment.EmailId.EndsWith(","))
                    {
                        objRecruitment.EmailId = objRecruitment.EmailId.Substring(0, objRecruitment.EmailId.Length - 1);
                    }
                    objEmail.To.Add(objRecruitment.EmailId);
                }
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
                    objEmail.To.Add(objRecruitment.EmailId);
                }
                //Gets the Email ID of the person who had created the mrf and keep him/her in CC.
                objEmail.CC.Add(objRecruitment.EmailIdOfCreatedByMRF);

                #region Modified By Mohamed Dangra
                // Mohamed : Issue 41942 : 26/04/2013 : Starts                        			  
                // Desc : Line Manger(Accountable To) of MRF should be in cc
                DataAccessLayer.Contracts.Contract objDataAccessContract = new Rave.HR.DataAccessLayer.Contracts.Contract();
                //Venkatesh : Issue Id : 47714  Start
                //project + dept MRF line manager should be in CC.
                //if (objRecruitment.ProjectId == default_project_id)
                if (objRecruitment.ReportingTo != null)
                {
                    objEmail.CC.Add(objDataAccessContract.GetEmailID(Convert.ToInt32(objRecruitment.ReportingTo)));
                }
                // Mohamed : Issue 41942 : 26/04/2013 : Ends
                #endregion Modified By Mohamed Dangra

                //28966-Subhra-End

                //Mahendra : Issue Id : 44284 : Start
                //DESC : FOR NIS and Northgate projects add Sai name in CC.
                if (objRecruitment.ProjectId != default_project_id)
                {
                    if (objRecruitment.ClientName.ToUpper().Contains("NPS") || objRecruitment.ClientName.ToUpper().Contains("NORTHGATE"))
                    {
                        objEmail.CC.Add(CommonConstants.SAIGIDDUNISandNAVYANISEmailId);
                        //objEmail.CC.Add(CommonConstants.SAIGIDDUNISEmailId);
                        // Mohamed : NIS-RMS : 02/01/2015 : Starts                        			  
                        // Desc : Add Naviya name where there is Sai name "navya.annamraju@northgateps.com"				
                        //objEmail.CC.Add(CommonConstants.NAVYANISEmailId);
                        // Mohamed : NIS-RMS : 02/01/2015 : Ends
                    }
                }
                //Mahendra : Issue Id : 44284 : End
                // Mohamed : Issue 51087 : 27/05/2014 : Ends


                if (objRecruitment.IsResourceJoined == 1)
                {
                    // 35913-Ambar-06072012-Added If condition to avoid multiple mails.
                    if (!BIsCandidateJoined)
                    {
                        //calling method when candidate has joined.
                        Rave.HR.BusinessLayer.Recruitment.Recruitment.SendsMailForCandidateJoined(objRecruitment);
                    }
                }

                else
                {
                    objEmail.Subject = string.Format(objEmail.Subject,
                                                     objRecruitment.MRFCode,
                                                     ((objRecruitment.Department == PROJECT) ? objRecruitment.ProjectName : objRecruitment.Department));

                    objEmail.Body = string.Format(objEmail.Body,
                                                  objRecruitment.MRFCode,
                                                  ((objRecruitment.Department == PROJECT) ? objRecruitment.ProjectName : objRecruitment.Department),
                                                  tableData,
                                                  strRecruitmentSummaryLink,
                                                  username);

                    Rave.HR.BusinessLayer.Recruitment.Recruitment.message = "Pipeline details  updated  against MRF  [" + objRecruitment.MRFCode + "]  has been successfully updated,email notification is sent.";
                    HttpContext.Current.Session[SessionNames.CONFIRMATION_MESSAGE_CANDIDATE_ADDED] = message;
                    objEmail.SendEmail(objEmail);
                }

                HttpContext.Current.Session[SessionNames.CONFIRMATION_MESSAGE] = message;
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RECRUITMENT, SENDEMAIL_CANDIDATE_UPDATED, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }


        }

        /// <summary>
        /// Sends mail when Candidate has joined.
        /// </summary>
        /// <param name="objRecruitment"></param>
        private static void SendsMailForCandidateJoined(BusinessEntities.Recruitment objRecruitment)
        {
            BusinessEntities.RaveHRCollection raveHRCollectionProject = new BusinessEntities.RaveHRCollection();
            BusinessEntities.RaveHRCollection raveHRCollectionDepartment = new BusinessEntities.RaveHRCollection();
            string projectManagerEmail = string.Empty;
            string departmentHeadEmail = string.Empty;
            //Get the loggedin user name
            AuthorizationManager objAuMgr = new AuthorizationManager();
            //string strFromUser = objAuMgr.getLoggedInUserEmailId();
            //going google
            //string username = objAuMgr.GetDomainUserName(strFromUser.Replace(RAVEDOMAIN, ""));
            string username = "";
            string strFromUser = objAuMgr.getLoggedInUser();
            //GoogleMail
            //if (strFromUser.ToLower().Contains("@rave-tech.co.in"))
            //{
            //    username = objAuMgr.GetDomainUserName(strFromUser.Replace(RAVEDOMAIN, ""));
            //}
            //else
            //{
            //    username = objAuMgr.GetDomainUserName(strFromUser.Replace(NORTHGATEDOMAIN, ""));
            //}

            username = objAuMgr.GetDomainUserName(strFromUser);

            string joinedDate = objRecruitment.ResourceJoinedDate.ToShortDateString();

            //Get Server URL
            string strRecruitmentSummaryLink = Utility.GetUrl() + CommonConstants.Page_RecruitmentSummary;

            //18416
            string MRFID = objRecruitment.MRFCode;
            bool flag = MRFID.Contains("RaveConsultant") || MRFID.Contains("Rave Consultant"); ;

            IRMSEmail objEmail;
            if (objRecruitment.Location == CommonConstants.BENGULURU)

                objEmail = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Recruitment),
                                             Convert.ToInt16(EnumsConstants.EmailFunctionality.CandidateJoined),
                                             objRecruitment.Location);
            else

                objEmail = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Recruitment),
                                                 Convert.ToInt16(EnumsConstants.EmailFunctionality.CandidateJoined));

            // 29609-Ambar-30082011
            // Modified IF condition
            // if (objRecruitment.Department == PROJECT)
            // Mohamed : Issue 51087 : 27/05/2014 : Starts                        			  
            // Desc : Expected Joinee's details edited Mail should go to all who receive Expected to join mail
            if (objRecruitment.ProjectId != default_project_id)
            {
                raveHRCollectionProject = DataAccessLayer.Recruitment.Recruitment.GetProjectManagerByProjectId(objRecruitment);
                raveHRCollectionDepartment = DataAccessLayer.Recruitment.Recruitment.GetDepartmentHeadByDepartmentId(objRecruitment);
            }
            else
            {
                //BusinessEntities.Recruitment recruitment = new BusinessEntities.Recruitment();
                //recruitment.EmailId = DataAccessLayer.Recruitment.Recruitment.GetReportingToByEmployeeId(objRecruitment.CandidateId);
                //raveHRCollectionProject.Add(recruitment);
                raveHRCollectionDepartment = DataAccessLayer.Recruitment.Recruitment.GetDepartmentHeadByDepartmentId(objRecruitment);
            }

            //Issue Id : 44934 Start
            //project + dept MRF line manager should be in CC.
            DataAccessLayer.Contracts.Contract objDataAccessContract = new Rave.HR.DataAccessLayer.Contracts.Contract();
            if (objRecruitment.ReportingTo != null)
            {
                objEmail.CC.Add(objDataAccessContract.GetEmailID(Convert.ToInt32(objRecruitment.ReportingTo)));
            }
            //Issue Id : 44934 End

            try
            {
                string tableBody = GetHTMLForTableData(objRecruitment);

                if (raveHRCollectionProject.Count > 0)
                {
                    //Get ProjectManager Name 

                    foreach (BusinessEntities.Recruitment objrer in raveHRCollectionProject)
                    {
                        projectManagerEmail += objrer.EmailId;
                        objRecruitment.EmailId = projectManagerEmail;

                    }
                    if (objRecruitment.EmailId.EndsWith(","))
                    {
                        objRecruitment.EmailId = objRecruitment.EmailId.Substring(0, objRecruitment.EmailId.Length - 1);
                    }

                    #region
                    //18416-Start
                    // For RaveConsultant do not add PM Email Id.
                    if (!flag)
                    {
                        objEmail.To.Add(objRecruitment.EmailId);
                    }
                    //18416-Ambar-End
                }

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
                    objEmail.To.Add(objRecruitment.EmailId);
                }



                //18416-Start
                string strCandidateJoined = "ITS: Kindly generate an email ID for the employee.<br/>";
                string strPMNotify = "PM: Kindly coordinate with ITS and provide them list of software which needs to be installed in the machine.<br/>";

                if (flag)
                {
                    string[] mailTo = objEmail.To.ToArray(typeof(string)) as string[];

                    objEmail.To.Clear();

                    foreach (string toMail in mailTo)
                    {
                        //if (toMail == "its@rave-tech.com")
                        if (toMail == CommonConstants.EmailIdOfITSGroup)
                        {
                            strCandidateJoined = "";
                            strPMNotify = "";
                        }
                        else
                        {
                            objEmail.To.Add(toMail);
                        }
                    }

                }

                // Check for Rave Consultant India
                string[] mailCC = objEmail.CC.ToArray(typeof(string)) as string[];
                if (flag && MRFID.Contains("India"))
                {
                }
                else
                {
                    objEmail.CC.Clear();
                    foreach (string ccMail in mailCC)
                    {
                        //if ((ccMail == "Deepali.Salunke@rave-tech.com") || (ccMail == "Amol.Shinde@rave-tech.com"))
                        //{
                        //}
                        //else
                        //{
                        objEmail.CC.Add(ccMail);
                        //}

                    }
                    //18416-End

                    //Mahendra : Issue Id : 44284 : Start
                    //DESC : FOR NIS and Northgate projects add Sai name in CC.
                    if (objRecruitment.ClientName.ToUpper().Contains("NPS") || objRecruitment.ClientName.ToUpper().Contains("NORTHGATE"))
                    {
                        objEmail.CC.Add(CommonConstants.SAIGIDDUNISandNAVYANISEmailId);
                        //objEmail.CC.Add(CommonConstants.SAIGIDDUNISEmailId);
                        //// Mohamed : NIS-RMS : 02/01/2015 : Starts                        			  
                        //// Desc : Add Naviya name where there is Sai name "navya.annamraju@northgateps.com"				
                        //objEmail.CC.Add(CommonConstants.NAVYANISEmailId);
                        //// Mohamed : NIS-RMS : 02/01/2015 : Ends
                    }
                    //Mahendra : Issue Id : 44284 : End
                    // Mohamed : Issue 51087 : 27/05/2014 : Ends

                    #endregion

                    objEmail.Subject = string.Format(objEmail.Subject,
                                                             objRecruitment.MRFCode,
                                                             ((objRecruitment.Department == PROJECT) ? "Project" : "Department"),
                                                             ((objRecruitment.Department == PROJECT) ? objRecruitment.ProjectName : objRecruitment.Department));

                    objEmail.Body = string.Format(objEmail.Body,
                                                  null,//objRecruitment.Prefix,
                                                  null,// objRecruitment.FirstName + " " + objRecruitment.LastName,
                                                  null, //joinedDate,
                                                  null,//objRecruitment.Role,
                                                  tableBody,
                                                  strCandidateJoined,
                                                  strPMNotify,
                                                  username);

                    message = "MRF  [" + objRecruitment.MRFCode + "] is been successfully updated,Candidate is joined, email notification is sent.";
                    HttpContext.Current.Session[SessionNames.CONFIRMATION_MESSAGE_CANDIDATE_JOINED] = message;

                    //Calling method for mailing
                    objEmail.SendEmail(objEmail);
                }
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RECRUITMENT, SENDEMAIL_CANDIDATE_JOINED, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }

        }

        /// <summary>
        /// Sends E-mail when new Candidate details added in the Add pipeline details.
        /// </summary>
        /// <param name="objRecruitment"></param>
        public static void SendEmailForCandidateAdded(BusinessEntities.Recruitment objRecruitment)
        {
            BusinessEntities.RaveHRCollection raveHRCollectionProject = new BusinessEntities.RaveHRCollection();
            BusinessEntities.RaveHRCollection raveHRCollectionDepartment = new BusinessEntities.RaveHRCollection();
            // Mohamed : Issue 41942 : 26/04/2013 : Starts                        			  
            // Desc : Line Manger(Accountable To) should be in cc
            DataAccessLayer.Contracts.Contract objDataAccessContract = new Rave.HR.DataAccessLayer.Contracts.Contract();
            // Mohamed : Issue 41942 : 26/04/2013 : Ends
            AuthorizationManager objAuMgr = new AuthorizationManager();
            string projectManagerEmail = string.Empty;
            string departmentHeadEmail = string.Empty;

            //Get the loggedin user name            
            //string strFromUser = objAuMgr.getLoggedInUserEmailId();
            // going google
            //string username = objAuMgr.GetDomainUserName(strFromUser.Replace(RAVEDOMAIN, ""));
            string username = "";
            string strFromUser = objAuMgr.getLoggedInUser();
            //GoogleMail
            //if (strFromUser.ToLower().Contains(RAVEDOMAIN))
            //{
            //    username = objAuMgr.GetDomainUserName(strFromUser.Replace(RAVEDOMAIN, ""));
            //}
            //else
            //{
            //    username = objAuMgr.GetDomainUserName(strFromUser.Replace(NORTHGATEDOMAIN, ""));
            //}
            username = objAuMgr.GetDomainUserName(strFromUser);

            string expJoinedDate = objRecruitment.ExpectedJoiningDate.ToShortDateString();


            //Get Server URL
            string strRecruitmentSummaryLink = Utility.GetUrl() + CommonConstants.Page_RecruitmentSummary;
            IRMSEmail objEmail;
            if (objRecruitment.Location == CommonConstants.BENGULURU)

                objEmail = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Recruitment),
                                        Convert.ToInt16(EnumsConstants.EmailFunctionality.CandidateExpectedToJoin),
                                        objRecruitment.Location);
            else
                objEmail = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Recruitment),
                                             Convert.ToInt16(EnumsConstants.EmailFunctionality.CandidateExpectedToJoin));

            // 29503-Ambar-30082011
            // Modified IF condition
            // if (objRecruitment.Department == PROJECT)
            if (objRecruitment.ProjectId != default_project_id)
            {
                raveHRCollectionProject = DataAccessLayer.Recruitment.Recruitment.GetProjectManagerByProjectId(objRecruitment);
                raveHRCollectionDepartment = DataAccessLayer.Recruitment.Recruitment.GetDepartmentHeadByDepartmentId(objRecruitment);
            }
            else
            {
                // raveHRCollectionProject = DataAccessLayer.Recruitment.Recruitment.GetProjectManagerByProjectId(objRecruitment);
                raveHRCollectionDepartment = DataAccessLayer.Recruitment.Recruitment.GetDepartmentHeadByDepartmentId(objRecruitment);
            }



            try
            {

                if (raveHRCollectionProject.Count > 0)
                {
                    //Get ProjectManager Name 

                    foreach (BusinessEntities.Recruitment objrer in raveHRCollectionProject)
                    {
                        projectManagerEmail += objrer.EmailId;
                        objRecruitment.EmailId = projectManagerEmail;

                    }
                    if (objRecruitment.EmailId.EndsWith(","))
                    {
                        objRecruitment.EmailId = objRecruitment.EmailId.Substring(0, objRecruitment.EmailId.Length - 1);
                    }

                    objEmail.To.Add(objRecruitment.EmailId);
                }

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

                    objEmail.To.Add(objRecruitment.EmailId);
                }
                /*Modified For the Issue ID :19512 and 20035*/
                //Gets the Email ID of the person who had created the mrf and keep him/her in CC.
                objEmail.CC.Add(objRecruitment.EmailIdOfCreatedByMRF);

                //Changed the date for the email .
                #region Modified By Mohamed Dangra
                // Mohamed : Issue 41942 : 26/04/2013 : Starts                        			  
                // Desc : Line Manger(Accountable To) of MRF should be in cc

                //Venkatesh : Issue Id : 47714  Start
                //project + dept MRF line manager should be in CC.
                if (objRecruitment.ReportingTo != null)
                {
                    objEmail.CC.Add(objDataAccessContract.GetEmailID(Convert.ToInt32(objRecruitment.ReportingTo)));
                }
                //Venkatesh : Issue Id : 47714  End

                if (objRecruitment.ProjectId != default_project_id)
                {
                    //commented Mahendra project + dept MRF line manager should be in CC.
                    //Issue Id : 46026 Start
                    //objEmail.CC.Add(objDataAccessContract.GetEmailID(Convert.ToInt32(objRecruitment.ReportingTo)));
                    //Issue Id : 46026 End

                    //Mahendra : Issue Id : 44284 : Start
                    //DESC : FOR NIS and Northgate projects add Sai name in CC.
                    if (objRecruitment.ClientName.ToUpper().Contains("NPS") || objRecruitment.ClientName.ToUpper().Contains("NORTHGATE"))
                    {
                        objEmail.CC.Add(CommonConstants.SAIGIDDUNISandNAVYANISEmailId);
                        //objEmail.CC.Add(CommonConstants.SAIGIDDUNISEmailId);
                        //// Mohamed : NIS-RMS : 02/01/2015 : Starts                        			  
                        //// Desc : Add Naviya name where there is Sai name "navya.annamraju@northgateps.com"				
                        //objEmail.CC.Add(CommonConstants.NAVYANISEmailId);
                        // Mohamed : NIS-RMS : 02/01/2015 : Ends
                    }
                    //Mahendra : Issue Id : 44284 : End
                }
                // Mohamed : Issue 41942 : 26/04/2013 : Ends
                #endregion Modified By Mohamed Dangra
                objRecruitment.ResourceJoinedDate = objRecruitment.ExpectedJoiningDate;

                string tableBody = GetHTMLForTableData(objRecruitment);



                objEmail.Subject = string.Format(objEmail.Subject,
                                                         objRecruitment.MRFCode,
                                                         ((objRecruitment.Department == PROJECT) ? "Project" : "Department"),
                                                         ((objRecruitment.Department == PROJECT) ? objRecruitment.ProjectName : objRecruitment.Department));

                objEmail.Body = string.Format((objEmail.Body),
                                              null,//objRecruitment.Prefix,
                                              null,// objRecruitment.FirstName + " " + objRecruitment.LastName,
                                              null,//expJoinedDate,
                                              null,//objRecruitment.Role,
                                              tableBody,
                                              username);





                //Calling method for mailing
                objEmail.SendEmail(objEmail);
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RECRUITMENT, SENDEMAIL_CANDIDATE_ADDED, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }

        }

        //Rajan Kumar : Issue 39508: 04/02/2014 : Starts                        			 
        // Desc : Traninig for new joining employee. (Training Gaps).      

        /// <summary>
        /// Sends E-mail when new Candidate Skill gaps.
        /// </summary>
        /// <param name="objRecruitment"></param>
        public static void SendEmailForCandidateSkillGaps(BusinessEntities.Recruitment objRecruitment, string mode)
        {
            BusinessEntities.RaveHRCollection raveHRCollectionProject = new BusinessEntities.RaveHRCollection();
            BusinessEntities.RaveHRCollection raveHRCollectionDepartment = new BusinessEntities.RaveHRCollection();
            DataAccessLayer.Contracts.Contract objDataAccessContract = new Rave.HR.DataAccessLayer.Contracts.Contract();
            AuthorizationManager objAuMgr = new AuthorizationManager();
            string projectManagerEmail = string.Empty;
            string departmentHeadEmail = string.Empty;
            string username = "";
            string strFromUser = objAuMgr.getLoggedInUser();
            username = objAuMgr.GetDomainUserName(strFromUser);
            string expJoinedDate = objRecruitment.ExpectedJoiningDate.ToShortDateString();
            IRMSEmail objEmail;
            int enumSkillGap = Convert.ToInt16(EnumsConstants.EmailFunctionality.CandidateSkillsGapEdit);
            if (mode.ToUpper() == "ADD")
            {
                enumSkillGap = Convert.ToInt16(EnumsConstants.EmailFunctionality.CandidateSkillsGap);
            }
            if (objRecruitment.Location == CommonConstants.BENGULURU)

                objEmail = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Recruitment),
                                        enumSkillGap,
                                        objRecruitment.Location);
            else
                objEmail = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.Recruitment),
                                             enumSkillGap);
            if (objRecruitment.ProjectId != default_project_id)
            {
                raveHRCollectionProject = DataAccessLayer.Recruitment.Recruitment.GetProjectManagerByProjectId(objRecruitment);
            }
            else
            {
                raveHRCollectionDepartment = DataAccessLayer.Recruitment.Recruitment.GetDepartmentHeadByDepartmentId(objRecruitment);
            }

            try
            {
                objEmail.To.Add(CommonConstants.EmailIdOfRMOGroup);

                if (raveHRCollectionProject.Count > 0)
                {
                    //Get ProjectManager Name 

                    foreach (BusinessEntities.Recruitment objrer in raveHRCollectionProject)
                    {
                        projectManagerEmail += objrer.EmailId;
                        objRecruitment.EmailId = projectManagerEmail;

                    }
                    if (objRecruitment.EmailId.EndsWith(","))
                    {
                        objRecruitment.EmailId = objRecruitment.EmailId.Substring(0, objRecruitment.EmailId.Length - 1);
                    }

                    objEmail.CC.Add(objRecruitment.EmailId);
                }

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

                    objEmail.CC.Add(objRecruitment.EmailId);
                }
                if (objRecruitment.ReportingTo != null)
                {
                    objEmail.CC.Add(objDataAccessContract.GetEmailID(Convert.ToInt32(objRecruitment.ReportingTo)));
                }
        #endregion Modified By Mohamed Dangra
                objRecruitment.ResourceJoinedDate = objRecruitment.ExpectedJoiningDate;

                string tableBody = GetHTMLForCandidateSkillGaps(objRecruitment);
                objEmail.Subject = string.Format(objEmail.Subject,
                                                         objRecruitment.MRFCode);
                if (mode.ToUpper() == "ADD")
                {
                    objEmail.Body = string.Format((objEmail.Body),
                                             objRecruitment.FirstName + " " + objRecruitment.LastName,
                                             expJoinedDate,
                                             objRecruitment.Location,
                                             (string.IsNullOrEmpty(objRecruitment.ProjectName) ? "" : objRecruitment.ProjectName),
                                            (string.IsNullOrEmpty(objRecruitment.Department) ? "" : objRecruitment.Department),
                                             tableBody,
                                             username,
                                             (!string.IsNullOrEmpty(objRecruitment.ProjectName) && !string.IsNullOrEmpty(objRecruitment.Department) ? "/" : " "));
                }
                else
                {
                    objEmail.Body = string.Format((objEmail.Body),
                                            objRecruitment.FirstName + " " + objRecruitment.LastName,
                                            (string.IsNullOrEmpty(objRecruitment.ProjectName) ? "" : objRecruitment.ProjectName),
                                            (!string.IsNullOrEmpty(objRecruitment.ProjectName) && !string.IsNullOrEmpty(objRecruitment.Department) ? "/" : " "),
                                            (string.IsNullOrEmpty(objRecruitment.Department) ? "" : objRecruitment.Department),
                                           tableBody,
                                           username
                                            );
                }
                //Calling method for mailing
                objEmail.SendEmail(objEmail);
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RECRUITMENT, SENDEMAIL_CANDIDATE_ADDED, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }

        }
        // Rajan Kumar : Issue 39508: 04/02/2014 : END
        /// <summary>
        /// Displays data in Table format
        /// </summary>
        /// <param name="listRecruitmentDetails"></param>
        /// <returns></returns>
        private static string GetHTMLForTableData(BusinessEntities.Recruitment objRecruitment)
        {
            Boolean bldeptflag = false;
            Boolean blContDuration = false;
            Boolean blNotJoined = false;
            int larraysize;

            if ((objRecruitment.Department != "Projects") && !(objRecruitment.Department.Contains("Consultant")))
            {
                bldeptflag = true;
                larraysize = 9;
            }
            else
            {
                if (objRecruitment.Department.Contains("Consultant"))
                {
                    blContDuration = true;
                    larraysize = 11;
                }
                else
                {
                    larraysize = 10;

                }

            }

            //Mahendra In case of Not joining
            if (!string.IsNullOrEmpty(objRecruitment.Reason))
            {
                blNotJoined = true;
                larraysize = 12;
            }


            try
            {

                string[,] arrayData = new string[larraysize, 2];

                //Header Values
                arrayData[0, 0] = "Candidate’s Name";
                arrayData[1, 0] = "Date of joining";
                arrayData[2, 0] = "Designation";
                arrayData[3, 0] = "Employee Type";
                arrayData[4, 0] = "MRF Code";
                arrayData[5, 0] = "Department";
                arrayData[6, 0] = "Experience";
                arrayData[7, 0] = "Location";

                //Issue :57942 Rakesh removed Purpose 

                // Rakesh : Issue 57942 : 10/May/2016 : Starts commented to remove purpose

                //  arrayData[8, 0] = "Purpose";

                //End
                if (!bldeptflag)
                {
                    arrayData[9, 0] = "Client Name";
                }
                if (blContDuration)
                {
                    arrayData[10, 0] = "Contract Duration(months)";
                }
                if (blNotJoined)
                {
                    arrayData[11, 0] = "Reason";
                }



                //Row Details
                arrayData[0, 1] = objRecruitment.FirstName + " " + objRecruitment.MiddleName + " " + objRecruitment.LastName;
                arrayData[1, 1] = objRecruitment.ResourceJoinedDate.ToString("dd/MM/yyyy");
                arrayData[2, 1] = objRecruitment.Designation;
                arrayData[3, 1] = objRecruitment.EmployeeType;
                arrayData[4, 1] = objRecruitment.MRFCode;
                arrayData[5, 1] = objRecruitment.Department;
                arrayData[6, 1] = objRecruitment.RelevantExperienceYear.ToString() + " Years" + " " + objRecruitment.RelavantExperienceMonth.ToString() + " Months";
                arrayData[7, 1] = objRecruitment.Location;

                // Rakesh : Issue 57942 : 10/May/2016 : Starts commented to remove purpose
                //  arrayData[8, 1] = objRecruitment.MRFPurpose;
                //End
                if (!bldeptflag)
                {
                    arrayData[9, 1] = objRecruitment.ClientName;
                }

                if (blContDuration)
                {
                    arrayData[10, 1] = objRecruitment.ContractDuration.ToString();

                }
                if (blNotJoined)
                {
                    arrayData[11, 1] = objRecruitment.Reason.ToString();
                }


                IEmailTableData objEmailTableData = new EmailTableData();
                objEmailTableData.RowDetail = arrayData;

                objEmailTableData.RowCount = larraysize;

                return objEmailTableData.GetVerticalHeaderTableData(objEmailTableData);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RECRUITMENT, GET_HTML_TABLE_DATA, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }


        }
        #endregion

        public static BusinessEntities.Recruitment ResourceBussinesUnitAsperDept(int deptid)
        {
            try
            {
                BusinessEntities.Recruitment recruitment = Rave.HR.DataAccessLayer.Recruitment.Recruitment.ResourceBussinesUnitAsperDept(deptid);
                return recruitment;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RECRUITMENT, GETMRFDETAIL, EventIDConstants.RAVE_HR_RECRUITMENT_BUSNIESS_LAYER);
            }

        }


        /// <summary>
        /// Displays data in Table format
        /// </summary>
        /// <param name="listRecruitmentDetails"></param>
        /// <returns></returns>
        /// 
        //public static string GetHTMLForCandidateDetailsEditedTableData(BusinessEntities.Recruitment objRecruitment)
        //{
        //    try
        //    {
        //        string[] header = new string[8];
        //        string[,] arrayData = new string[1, 8];                

        //        objRecruitment = (BusinessEntities.Recruitment)HttpContext.Current.Session[SessionNames.RECRUITMENT];


        //        //Header Details
        //        header[0] = "Prev End Date";
        //        header[1] = "Project End Date";
        //        header[2] = "Prev ProjectGroup";
        //        header[3] = "Project Group";
        //        header[4] = "Prev Project Standard Hours";
        //        header[5] = "Project Standard Hours";
        //        header[6] = " Prev Project Status";
        //        header[7] = "Project Status";

        //        //RowDetails     

        //        arrayData[0, 1] = objRecruitment.Prev_Prefix;
        //        arrayData[0, 2] = objRecruitment.Prefix;
        //        arrayData[0, 3] = objRecruitment.Prev_FirstName;
        //        arrayData[0, 4] = objRecruitment.FirstName;
        //        arrayData[0, 5] = objRecruitment.Prev_MiddleName;
        //        arrayData[0, 6] = objRecruitment.MiddleName;
        //        arrayData[0, 7] = objRecruitment.Prev_LastName;
        //        arrayData[0, 8] = objRecruitment.LastName;
        //        arrayData[0, 9] = objRecruitment.Prev_Department;
        //        arrayData[0, 10] = objRecruitment.Department;
        //        arrayData[0, 11] = objRecruitment.Prev_Designation;
        //        arrayData[0, 12] = objRecruitment.Designation;
        //        arrayData[0, 13] = objRecruitment.ExpectedJoiningDate.ToString(CommonConstants.DATE_FORMAT); 
        //        arrayData[0, 14] = objRecruitment.ExpectedJoiningDate.ToString(CommonConstants.DATE_FORMAT);
        //        arrayData[0, 15] =
        //        arrayData[0, 16] = objRecruitment.Band;
        //        arrayData[0, 17] =
        //        arrayData[0, 18] = objRecruitment.ActualCTC.ToString();
        //        arrayData[0, 19] =
        //        arrayData[0, 20] = objRecruitment.EmployeeType;
        //        arrayData[0, 21] =
        //        arrayData[0, 22] = objRecruitment.Location;
        //        arrayData[0, 23] =
        //        arrayData[0, 24] = objRecruitment.ReportingTo;
        //        arrayData[0, 25] =
        //        arrayData[0, 26] = objRecruitment.PhoneNo;
        //        arrayData[0, 27] =
        //        arrayData[0, 28] = objRecruitment.ResourceBussinessUnit.ToString();
        //        arrayData[0, 29] =
        //        arrayData[0, 30] = objRecruitment.ExternalWorkExp;
        //        arrayData[0, 31] =
        //        arrayData[0, 32] = objRecruitment.Address;
        //        arrayData[0, 33] =
        //        arrayData[0, 34] = objRecruitment.LandlineNo;


        //        IEmailTableData objEmailTableData = new EmailTableData();
        //        objEmailTableData.Header = header;
        //        objEmailTableData.RowDetail = arrayData;
        //        objEmailTableData.RowCount = 1;

        //        return objEmailTableData.GetTableData(objEmailTableData);
        //    }
        //    catch (RaveHRException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RECRUITMENT, GET_HTML_CANDIDATEDETAILS_EDITED_TABLE_DATA, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
        //    }


        //}
        //Mohamed : Issue 50306 : 09/09/2014 : Starts                        			  
        //Desc : Expected Joinee's details edited[MRF Code: MRF_Testing_SrTstA_0387] old  Joining date is default date -- Mail for de-linking MRF.

        public static string GetHTMLForDelinkMRF(BusinessEntities.Recruitment objRecruitment)
        {
            try
            {
                // Venkatesh : Issue 46194 : 12/Dec/2013 : Starts
                // Desc : Show Edited joining date & designation
                string[] header = new string[5];
                string[,] arrayData = new string[1, 5];

                objRecruitment = (BusinessEntities.Recruitment)HttpContext.Current.Session[SessionNames.RECRUITMENT];

                //Header Details
                header[0] = "Candidate Name";

                //RowDetails
                //Start Subhra  Issue 28568
                arrayData[0, 0] = objRecruitment.FirstName + " " + objRecruitment.LastName;

                header[1] = "Old project/department name";
                header[2] = "New project/department name";

                BusinessEntities.MRFDetail ObjMRFDetails = new BusinessEntities.MRFDetail();
                DataAccessLayer.MRF.MRFDetail ObjMRF = new Rave.HR.DataAccessLayer.MRF.MRFDetail();
                ObjMRFDetails = ObjMRF.GetMRFDetails(objRecruitment.Prev_MRFId);
                arrayData[0, 1] = (ObjMRFDetails.DepartmentName == PROJECT) ? ObjMRFDetails.ProjectName : ObjMRFDetails.DepartmentName;
                ObjMRFDetails = ObjMRF.GetMRFDetails(objRecruitment.MRFId);
                arrayData[0, 2] = (ObjMRFDetails.DepartmentName == PROJECT) ? ObjMRFDetails.ProjectName : ObjMRFDetails.DepartmentName;

                //if (objRecruitment.Prev_Designation != objRecruitment.Designation)
                //{
                //    header[3] = "Old Designation";
                //    header[4] = "New Designation";
                //    arrayData[0, 3] = objRecruitment.Prev_Designation.ToString();
                //    arrayData[0, 4] = objRecruitment.Designation.ToString();
                //}
                //end Subhra Issue 28568

                ////Start Subhra  Issue 28568
                //if (objRecruitment.Prev_ExpectedJoiningDate != objRecruitment.ExpectedJoiningDate)
                //{
                //    arrayData[0, 0] = objRecruitment.FirstName + " " + objRecruitment.LastName;
                //    arrayData[0, 1] = objRecruitment.Prev_ExpectedJoiningDate.ToString(CommonConstants.DATE_FORMAT);
                //    arrayData[0, 2] = objRecruitment.ExpectedJoiningDate.ToString(CommonConstants.DATE_FORMAT);
                //}
                //else
                //{
                //    arrayData[0, 0] = objRecruitment.FirstName + " " + objRecruitment.LastName;
                //    arrayData[0, 1] = objRecruitment.Prev_ExpectedJoiningDate.ToString(CommonConstants.DATE_FORMAT);
                //    arrayData[0, 2] = "No changes in the Expected Joining Date ";
                //}
                ////end Subhra Issue 28568

                IEmailTableData objEmailTableData = new EmailTableData();
                objEmailTableData.Header = header;
                objEmailTableData.RowDetail = arrayData;
                objEmailTableData.RowCount = 1;
                // Venkatesh : Issue 46194 : 12/Dec/2013 : End
                return objEmailTableData.GetTableDataForDate(objEmailTableData);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer,
                    RECRUITMENT, GET_HTML_CANDIDATEDETAILS_EDITED_TABLE_DATA,
                    EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }


        }

        //Mohamed : Issue 50306 : 09/09/2014 : Ends
        public static string GetHTMLForExpectedDOJEdited(BusinessEntities.Recruitment objRecruitment)
        {
            try
            {
                // Venkatesh : Issue 46194 : 12/Dec/2013 : Starts
                // Desc : Show Edited joining date & designation
                string[] header = new string[5];
                string[,] arrayData = new string[1, 5];

                objRecruitment = (BusinessEntities.Recruitment)HttpContext.Current.Session[SessionNames.RECRUITMENT];

                //Header Details
                header[0] = "Candidate’s Name";

                //RowDetails
                //Start Subhra  Issue 28568
                arrayData[0, 0] = objRecruitment.FirstName + " " + objRecruitment.LastName;
                if (objRecruitment.Prev_ExpectedJoiningDate != objRecruitment.ExpectedJoiningDate)
                {
                    header[1] = "Old Expected Joining Date";
                    header[2] = "New Expected Joining Date";
                    arrayData[0, 1] = objRecruitment.Prev_ExpectedJoiningDate.ToString(CommonConstants.DATE_FORMAT);
                    arrayData[0, 2] = objRecruitment.ExpectedJoiningDate.ToString(CommonConstants.DATE_FORMAT);
                }
                if (objRecruitment.Prev_Designation != objRecruitment.Designation)
                {
                    header[3] = "Old Designation";
                    header[4] = "New Designation";
                    arrayData[0, 3] = objRecruitment.Prev_Designation.ToString();
                    arrayData[0, 4] = objRecruitment.Designation.ToString();
                }
                //end Subhra Issue 28568

                ////Start Subhra  Issue 28568
                //if (objRecruitment.Prev_ExpectedJoiningDate != objRecruitment.ExpectedJoiningDate)
                //{
                //    arrayData[0, 0] = objRecruitment.FirstName + " " + objRecruitment.LastName;
                //    arrayData[0, 1] = objRecruitment.Prev_ExpectedJoiningDate.ToString(CommonConstants.DATE_FORMAT);
                //    arrayData[0, 2] = objRecruitment.ExpectedJoiningDate.ToString(CommonConstants.DATE_FORMAT);
                //}
                //else
                //{
                //    arrayData[0, 0] = objRecruitment.FirstName + " " + objRecruitment.LastName;
                //    arrayData[0, 1] = objRecruitment.Prev_ExpectedJoiningDate.ToString(CommonConstants.DATE_FORMAT);
                //    arrayData[0, 2] = "No changes in the Expected Joining Date ";
                //}
                ////end Subhra Issue 28568

                IEmailTableData objEmailTableData = new EmailTableData();
                objEmailTableData.Header = header;
                objEmailTableData.RowDetail = arrayData;
                objEmailTableData.RowCount = 1;
                // Venkatesh : Issue 46194 : 12/Dec/2013 : End
                return objEmailTableData.GetTableDataForDate(objEmailTableData);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer,
                    RECRUITMENT, GET_HTML_CANDIDATEDETAILS_EDITED_TABLE_DATA,
                    EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }


        }
        //Rajan Kumar : Issue 39508: 04/02/2014 : Starts                        			 
        // Desc : Traninig for new joining employee. (Training Gaps).        
        private static string GetHTMLForCandidateSkillGaps(BusinessEntities.Recruitment objRecruitment)
        {
            try
            {

                string[,] arrayData = new string[7, 2];

                //Header Values
                arrayData[0, 0] = "Candidate’s Name";
                arrayData[1, 0] = "Date of joining";
                arrayData[2, 0] = "Designation";
                arrayData[3, 0] = "MRF Code";
                arrayData[4, 0] = "Department";
                arrayData[5, 0] = "Experience";
                arrayData[6, 0] = "Skills Gap Identified";
                //Row Details
                arrayData[0, 1] = objRecruitment.FirstName + " " + objRecruitment.MiddleName + " " + objRecruitment.LastName;
                arrayData[1, 1] = objRecruitment.ResourceJoinedDate.ToString("dd/MM/yyyy");
                arrayData[2, 1] = objRecruitment.Designation;
                arrayData[3, 1] = objRecruitment.MRFCode;
                arrayData[4, 1] = objRecruitment.Department;
                arrayData[5, 1] = objRecruitment.RelevantExperienceYear.ToString() + " Years" + " " + objRecruitment.RelavantExperienceMonth.ToString() + " Months";
                arrayData[6, 1] = objRecruitment.TrainingSubject;

                IEmailTableData objEmailTableData = new EmailTableData();
                objEmailTableData.RowDetail = arrayData;
                objEmailTableData.RowCount = 7;
                return objEmailTableData.GetVerticalHeaderTableData(objEmailTableData);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, RECRUITMENT, GET_HTML_TABLE_DATA, EventIDConstants.RAVE_HR_MRF_BUSNIESS_LAYER);
            }


        }
        // Rajan Kumar : Issue 39508: 04/02/2014 : END

    }
}
