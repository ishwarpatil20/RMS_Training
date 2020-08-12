using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
//using Infrastructure.Interfaces;
using RMS.Common;
using RMS.Common.BusinessEntities;
using RMS.Common.BusinessEntities.Email;
using RMS.Common.Constants;
using RMS.Common.ExceptionHandling;
using System.Configuration;
using System.Net.Mail;
using System.IO;
using System.Collections;
using Services.Interfaces;
using Services;

namespace RMS.Helpers
{
    public class EmailHelper
    {
        private const string PAGENAME = "EmailHelper";

        public static void SendMailForTechSoftSkillDeleted(int Raiseid, DataSet dt)
        {
            try
            {
                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                   Convert.ToInt16(EnumsConstants.EmailFunctionality.TechSoftSkillRequestDeleted));

                //DataSet dt = _service.GetEmailIDForAppRejTechSoftSkill(Raiseid);
                if (dt.Tables[0].Rows.Count != 0)
                {

                    //For TO--RMGroup
                    obj.To.Add(dt.Tables[0].Rows[0]["RequestRaiserEMailID"]);

                    //For CC--Line manager of Request Raiser, Request Raiser
                    string EmailID = string.Empty;
                    for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                    {
                        EmailID += dt.Tables[0].Rows[i]["LineManagerEMailID"].ToString() + ',';
                    }
                    EmailID += CommonConstants.EmailIdOfRMOGroup;
                    obj.CC.Add(EmailID);

                    //For Body
                    obj.Body = string.Format(obj.Body, dt.Tables[1].Rows[0]["RequestRaiserName"], dt.Tables[0].Rows[0]["TrainingName"], CommonConstants.RMGroupName);

                    obj.SendEmail(obj);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionSendMailForTechSoftSkillDeleted", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }
        
        public static void SendMailForSeminarDeleted(int Raiseid, DataSet dt)
        {
            try
            {
                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                   Convert.ToInt16(EnumsConstants.EmailFunctionality.SeminarsRequestDeleted));

                //DataSet dt = _service.GetEmailIDForAppRejTechSoftSkill(Raiseid);
                if (dt.Tables[0].Rows.Count != 0)
                {

                    //For TO--RMGroup
                    obj.To.Add(dt.Tables[0].Rows[0]["RequestRaiserEMailID"]);

                    //For CC--Line manager of Request Raiser, Request Raiser
                    string EmailID = string.Empty;
                    for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                    {
                        EmailID += dt.Tables[0].Rows[i]["LineManagerEMailID"].ToString() + ',';
                    }
                    EmailID += CommonConstants.EmailIdOfRMOGroup;
                    obj.CC.Add(EmailID);

                    //For Body
                    obj.Body = string.Format(obj.Body, dt.Tables[1].Rows[0]["RequestRaiserName"], dt.Tables[0].Rows[0]["TrainingName"], CommonConstants.RMGroupName);

                    obj.SendEmail(obj);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionSendMailForSeminarDeleted", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }

        #region Raise Training Request

        public static void SendMailForTechSoftSkill(int LoginEmpID, DataSet ds)
        {
            try
            {
                //If Line/Function Manager requesting a training
                if (LoginEmpID == Convert.ToInt32(ds.Tables[1].Rows[0]["RequestedBy"]))
                {
                    IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                       Convert.ToInt16(EnumsConstants.EmailFunctionality.TechSoftSkillRequest));

                    //obj.Subject = string.Format(obj.Subject);

                    //DataSet ds = saveTrainingBL.GetEmailIDDetails(ddlRequestByTechnical.SelectedValue.ToString(), CommonConstants.DefaultRaiseID);

                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        //For TO
                        obj.To.Add(CommonConstants.EmailIdOfRMOGroup);

                        //For CC
                        string EmailID = string.Empty;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            EmailID += ds.Tables[0].Rows[i]["LineManagerEMailID"].ToString() + ',';
                        }
                        EmailID += ds.Tables[0].Rows[0]["RequestRaiserEMailID"].ToString();
                        obj.CC.Add(EmailID);

                        //For Body
                        obj.Body = string.Format(obj.Body, CommonConstants.RMGroupName, GetHTMLForTableData(ds.Tables[1]), ds.Tables[1].Rows[0]["RequestedByName"].ToString());

                        obj.SendEmail(obj);
                    }
                }
                //Behalf of Line/Function Manager RMGroup requesting a training.
                else
                {
                    IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                       Convert.ToInt16(EnumsConstants.EmailFunctionality.TechSoftSkillRequestbehalfManager));

                    //obj.Subject = string.Format(obj.Subject);

                    //DataSet ds = saveTrainingBL.GetEmailIDDetails(ddlRequestByTechnical.SelectedValue.ToString(), CommonConstants.DefaultRaiseID);

                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        //For TO
                        obj.To.Add(ds.Tables[0].Rows[0]["RequestRaiserEMailID"].ToString());

                        //For CC
                        string EmailID = string.Empty;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            EmailID += ds.Tables[0].Rows[i]["LineManagerEMailID"].ToString() + ',';
                        }
                        EmailID += CommonConstants.EmailIdOfRMOGroup;
                        obj.CC.Add(EmailID);

                        //For Body
                        obj.Body = string.Format(obj.Body, ds.Tables[1].Rows[0]["RequestRaiserName"].ToString(), GetHTMLForTableData(ds.Tables[1]), CommonConstants.RMGroupName);

                        obj.SendEmail(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "SendMailForTechSoftSkill", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }

        public static void SendMailForTechSoftSkillEdit(int LoginEmpID, int trainingtypeid, DataSet ds)
        {
            try
            {
                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                       Convert.ToInt16(EnumsConstants.EmailFunctionality.TechSoftSkillRequestEdit));

                string viewTechnicalRequest = string.Concat(ConfigurationManager.AppSettings[CommonConstants.BaseUrl].ToString(), CommonConstants.ViewTechnicalTrainingRequestRoute);
                string link = string.Format("<a href='{0}'>{1}</a>", viewTechnicalRequest, viewTechnicalRequest);

                //string URL = string.Empty;

                //if (trainingtypeid == CommonConstants.TechnicalTrainingID)
                //{
                //    URL = "http://rav-vm-srv-096/RMSTraining/Training/ViewTechnicalTrainingRequest";
                //}
                //else if (trainingtypeid == CommonConstants.SoftSkillsTrainingID)
                //{
                //    URL = "http://rav-vm-srv-096/RMSTraining/Training/ViewTechnicalTrainingRequest";
                //}

                //DataSet ds = saveTrainingBL.GetEmailIDDetails(ddlRequestByTechnical.SelectedValue.ToString(), Raiseid);

                if (ds.Tables[1].Rows[0]["Name"].ToString() == CommonConstants.Open.ToUpper().ToString())     //If Training Status is Open
                {
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        //For TO
                        obj.To.Add(CommonConstants.EmailIdOfRMOGroup);

                        //For CC
                        string EmailID = string.Empty;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            EmailID += ds.Tables[0].Rows[i]["LineManagerEMailID"].ToString() + ',';
                        }
                        EmailID += ds.Tables[0].Rows[0]["RequestRaiserEMailID"].ToString();
                        obj.CC.Add(EmailID);

                        //string msg = ddlTrainingNameTech.SelectedItem.Text.ToLower() == CommonConstants.TrainingTypeOther ? txtTrainingTypeOtherTech.Text.ToString() : ddlTrainingNameTech.SelectedItem.ToString();

                        //For Body
                        //obj.Body = string.Format(obj.Body, CommonConstants.RMGroupName, GetHTMLForTableData(), ddlRequestByTechnical.SelectedItem.Text);
                        obj.Body = string.Format(obj.Body, CommonConstants.RMGroupName, ds.Tables[2].Rows[0]["TrainingName"].ToString(), ds.Tables[2].Rows[0]["RequestedByName"].ToString(), viewTechnicalRequest);

                        obj.SendEmail(obj);
                    }
                }
                else if (ds.Tables[1].Rows[0]["Name"].ToString() == CommonConstants.Approved.ToUpper().ToString()) //RMGroup should update training when training stutus is approved.
                {
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        //For TO
                        obj.To.Add(ds.Tables[0].Rows[0]["RequestRaiserEMailID"].ToString());

                        //For CC
                        string EmailID = string.Empty;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            EmailID += ds.Tables[0].Rows[i]["LineManagerEMailID"].ToString() + ',';
                        }
                        EmailID += CommonConstants.EmailIdOfRMOGroup;
                        obj.CC.Add(EmailID);

                        //string msg = ddlTrainingNameTech.SelectedItem.Text.ToLower() == CommonConstants.TrainingTypeOther ? txtTrainingTypeOtherTech.Text.ToString() : ddlTrainingNameTech.SelectedItem.ToString();
                        //For Body

                        //obj.Body = string.Format(obj.Body, ddlRequestByTechnical.SelectedItem.Text, msg, CommonConstants.RMGroupName, URL);
                        obj.Body = string.Format(obj.Body, ds.Tables[2].Rows[0]["RequestedByName"].ToString(), ds.Tables[2].Rows[0]["TrainingName"].ToString(), CommonConstants.RMGroupName, viewTechnicalRequest);

                        obj.SendEmail(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "SendMailForTechSoftSkill", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }

        public static void SendMailForKSS(string UserMailId, DataSet ds, string PresenterName, string KssType)
        {
            try
            {
                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                   Convert.ToInt16(EnumsConstants.EmailFunctionality.KSSRequest));

//                DataTable dt = saveTrainingBL.GetEmailIDDetailsForKSS(RaiseTrainingCollection.PresenterID);

                if (ds.Tables[0].Rows.Count != 0)
                {

                    //For TO
                    obj.To.Add(CommonConstants.EmailIdOfRMOGroup);

                    //For CC
                    string EmailID = string.Empty;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        EmailID += ds.Tables[0].Rows[i]["RequestRaiserEMailID"].ToString() + ',';
                    }
                    if (KssType == "1261" || KssType == "1262")
                    {
                        EmailID += UserMailId + "," + System.Configuration.ConfigurationManager.AppSettings[CommonConstants.EmailIdOfKSSTechTesting];
                    }
                    else if (KssType == "2048")
                    {
                        EmailID += UserMailId + "," + System.Configuration.ConfigurationManager.AppSettings[CommonConstants.EmailIdOfKSSUsability];
                    }
                    else if (KssType == "2049")
                    {
                        EmailID += UserMailId + "," + System.Configuration.ConfigurationManager.AppSettings[CommonConstants.EmailIdOfKSSBA];
                    }

                    obj.CC.Add(EmailID);

                    //For Regards 
                    string[] UserName;
                    string RegardsUserName = string.Empty;
                    char separator = Convert.ToChar(System.Configuration.ConfigurationManager.AppSettings["SplitCharacter"]);
                    UserName = UserMailId.Split('@');
                    if (UserName[0].Contains(separator.ToString()))
                    {
                        UserName = UserName[0].Split(separator);
                        for (int i = 0; i < UserName.Length; i++)
                        {
                            RegardsUserName = RegardsUserName + " " + UserName[i];
                        }
                    }

                    //For Body
                    obj.Body = string.Format(obj.Body, CommonConstants.RMGroupName, GetHTMLForTableDataForKSS(ds.Tables[1], PresenterName), RegardsUserName);

                    obj.SendEmail(obj);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionSendMailForKSS", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }

        public static void SendMailForKSSEdit(string UserMailId, DataSet ds, string PresenterName, string KssType)
        {
            try
            {
                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                   Convert.ToInt16(EnumsConstants.EmailFunctionality.KSSRequestEdit));

                string viewTechnicalRequest = string.Concat(ConfigurationManager.AppSettings[CommonConstants.BaseUrl].ToString(), CommonConstants.ViewTechnicalTrainingRequestRoute);
                string link = string.Format("<a href='{0}'>{1}</a>", viewTechnicalRequest, viewTechnicalRequest);

                //string URL = string.Empty;

                //URL = "http://rav-vm-srv-096/RMSTraining/Training/ViewTechnicalTrainingRequest";

                //DataTable dt = saveTrainingBL.GetEmailIDDetailsForKSS(RaiseTrainingCollection.PresenterID);

                if (ds.Tables[0].Rows.Count != 0)
                {

                    //For TO
                    obj.To.Add(CommonConstants.EmailIdOfRMOGroup);

                    //For CC
                    string EmailID = string.Empty;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        EmailID += ds.Tables[0].Rows[i]["RequestRaiserEMailID"].ToString() + ',';
                    }

                    if (KssType == "1261" || KssType == "1262")
                    {
                        EmailID += UserMailId + "," + System.Configuration.ConfigurationManager.AppSettings[CommonConstants.EmailIdOfKSSTechTesting];
                    }
                    else if (KssType == "2048")
                    {
                        EmailID += UserMailId + "," + System.Configuration.ConfigurationManager.AppSettings[CommonConstants.EmailIdOfKSSUsability];
                    }
                    else if (KssType == "2049")
                    {
                        EmailID += UserMailId + "," + System.Configuration.ConfigurationManager.AppSettings[CommonConstants.EmailIdOfKSSBA];
                    }

                    obj.CC.Add(EmailID);

                    //For Regards 
                    string[] UserName;
                    string RegardsUserName = string.Empty;
                    char separator = Convert.ToChar(System.Configuration.ConfigurationManager.AppSettings["SplitCharacter"]);
                    UserName = UserMailId.Split('@');
                    if (UserName[0].Contains(separator.ToString()))
                    {
                        UserName = UserName[0].Split(separator);
                        for (int i = 0; i < UserName.Length; i++)
                        {
                            RegardsUserName = RegardsUserName + " " + UserName[i];
                        }
                    }

                    //For Body
                    //obj.Body = string.Format(obj.Body, CommonConstants.RMGroupName, ddlType.SelectedItem.Text, RegardsUserName, URL);
                    obj.Body = string.Format(obj.Body, CommonConstants.RMGroupName, ds.Tables[1].Rows[0]["Type"].ToString(), RegardsUserName, viewTechnicalRequest);

                    obj.SendEmail(obj);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionSendMailForKSSEdit", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }

        public static void SendMailForSeminar(DataSet ds, string NameOfParticipant)
        {
            try
            {
                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                   Convert.ToInt16(EnumsConstants.EmailFunctionality.SeminarRequest));

                //DataSet ds = saveTrainingBL.GetEmailIDDetails(ddlRequestedBySeminars.SelectedValue, CommonConstants.DefaultRaiseID);

                if (ds.Tables[0].Rows.Count != 0)
                {

                    //For TO--RMGroup
                    obj.To.Add(CommonConstants.EmailIdOfRMOGroup);

                    //For CC--Line manager of Request Raiser, Request Raiser
                    string EmailID = string.Empty;
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        EmailID += ds.Tables[0].Rows[i]["LineManagerEMailID"].ToString() + ',';
                    }
                    EmailID += ds.Tables[0].Rows[0]["RequestRaiserEMailID"].ToString();

                    obj.CC.Add(EmailID);

                    //For Body
                    obj.Body = string.Format(obj.Body, CommonConstants.RMGroupName, GetHTMLForTableDataForSeminar(ds.Tables[3], NameOfParticipant), ds.Tables[3].Rows[0]["RequestedByName"].ToString());

                    obj.SendEmail(obj);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionSendMailForSeminar", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }

        public static void SendMailForSeminarEdit(DataSet ds)
        {
            try
            {
                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                       Convert.ToInt16(EnumsConstants.EmailFunctionality.SeminarsRequestEdit));

                string viewTechnicalRequest = string.Concat(ConfigurationManager.AppSettings[CommonConstants.BaseUrl].ToString(), CommonConstants.ViewTechnicalTrainingRequestRoute);
                string link = string.Format("<a href='{0}'>{1}</a>", viewTechnicalRequest, viewTechnicalRequest);

                //string URL = string.Empty;
                //URL = "http://rav-vm-srv-096/RMSTraining/Training/ViewTechnicalTrainingRequest";

                //DataSet ds = saveTrainingBL.GetEmailIDDetails(ddlRequestedBySeminars.SelectedValue, Raiseid);

                if (ds.Tables[1].Rows[0]["Name"].ToString() == CommonConstants.Open.ToUpper().ToString())     //If Training Status is Open
                {
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        //For TO
                        obj.To.Add(CommonConstants.EmailIdOfRMOGroup);

                        //For CC
                        string EmailID = string.Empty;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            EmailID += ds.Tables[0].Rows[i]["LineManagerEMailID"].ToString() + ',';
                        }
                        EmailID += ds.Tables[0].Rows[0]["RequestRaiserEMailID"].ToString();
                        obj.CC.Add(EmailID);

                        //For Body
                        obj.Body = string.Format(obj.Body, CommonConstants.RMGroupName, ds.Tables[3].Rows[0]["SeminarsName"].ToString(), ds.Tables[3].Rows[0]["RequestedByName"].ToString(), viewTechnicalRequest);

                        obj.SendEmail(obj);
                    }
                }
                else if (ds.Tables[1].Rows[0]["Name"].ToString() == CommonConstants.Approved.ToUpper().ToString()) //RMGroup should update training when training stutus is approved.
                {
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        //For TO
                        obj.To.Add(ds.Tables[0].Rows[0]["RequestRaiserEMailID"].ToString());

                        //For CC
                        string EmailID = string.Empty;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            EmailID += ds.Tables[0].Rows[i]["LineManagerEMailID"].ToString() + ',';
                        }
                        EmailID += CommonConstants.EmailIdOfRMOGroup;
                        obj.CC.Add(EmailID);

                        //For Body
                        obj.Body = string.Format(obj.Body, ds.Tables[0].Rows[0]["RequestRaiserName"].ToString(), ds.Tables[3].Rows[0]["SeminarsName"].ToString(), CommonConstants.RMGroupName, viewTechnicalRequest);

                        obj.SendEmail(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionSendMailForSeminarEdit", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }

        private static string GetHTMLForTableData(DataTable dt)
        {

            try
            {
                //list for table values
                List<string> objListTrainingDetail = new List<string>();
                //if (TrainingNameOther.ToLower() == CommonConstants.TrainingTypeOther)
                //if (dt.Rows[0]["TrainingName"].ToString().ToLower() == CommonConstants.TrainingTypeOther)
                //{
                    objListTrainingDetail.Add(dt.Rows[0]["TrainingName"].ToString());
                //}
                //else
                //{
                //    objListTrainingDetail.Add(ddlTrainingNameTech.SelectedItem.Text);
                //}
                //objListTrainingDetail.Add(ddlQuarter.SelectedItem.Text);
                //objListTrainingDetail.Add(ddlpriority.SelectedItem.Text);
                objListTrainingDetail.Add(dt.Rows[0]["Quarter"].ToString());
                objListTrainingDetail.Add(dt.Rows[0]["Priority"].ToString());

                string[,] arrayData = new string[3, 2];

                if (objListTrainingDetail.Count > 0)
                {
                    //Header Values
                    arrayData[0, 0] = "Name of Training";
                    arrayData[1, 0] = "Quarter";
                    arrayData[2, 0] = "Priority";

                    //Row Details
                    for (int i = 0; i <= objListTrainingDetail.Count - 1; i++)
                    {
                        arrayData[i, 1] = objListTrainingDetail[i];
                    }

                }

                IEmailTableData objEmailTableData = new EmailTableData();
                objEmailTableData.RowDetail = arrayData;
                objEmailTableData.RowCount = objListTrainingDetail.Count;
                return objEmailTableData.GetVerticalHeaderTableData(objEmailTableData);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionGetHTMLForTableData", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }

        private static string GetHTMLForTableDataForKSS(DataTable dt, string PresenterName)
        {

            try
            {
                //list for table values
                List<string> objListTrainingDetail = new List<string>();
                //objListTrainingDetail.Add(ddlType.SelectedItem.Text);
                //objListTrainingDetail.Add(txtTopic.Text);
                //objListTrainingDetail.Add(ucDate.Text);
                objListTrainingDetail.Add(dt.Rows[0]["Type"].ToString());
                objListTrainingDetail.Add(dt.Rows[0]["Topic"].ToString());
                objListTrainingDetail.Add(dt.Rows[0]["Date"].ToString());
                objListTrainingDetail.Add(PresenterName);

                string[,] arrayData = new string[4, 2];

                if (objListTrainingDetail.Count > 0)
                {
                    //Header Values
                    arrayData[0, 0] = "Type";
                    arrayData[1, 0] = "Topic";
                    arrayData[2, 0] = "Date";
                    arrayData[3, 0] = "Presenter Name";

                    //Row Details
                    for (int i = 0; i <= objListTrainingDetail.Count - 1; i++)
                    {
                        arrayData[i, 1] = objListTrainingDetail[i];
                    }

                }

                IEmailTableData objEmailTableData = new EmailTableData();
                objEmailTableData.RowDetail = arrayData;
                objEmailTableData.RowCount = objListTrainingDetail.Count;
                return objEmailTableData.GetVerticalHeaderTableData(objEmailTableData);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionGetHTMLForTableDataForKSS", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }

        private static string GetHTMLForTableDataForSeminar(DataTable dt, string ParticipantsNames)
        {
            try
            {
                //list for table values
                List<string> objListTrainingDetail = new List<string>();
                objListTrainingDetail.Add(dt.Rows[0]["SeminarsName"].ToString());
                objListTrainingDetail.Add(dt.Rows[0]["Date"].ToString());
                objListTrainingDetail.Add("Open");
                objListTrainingDetail.Add(ParticipantsNames);

                string[,] arrayData = new string[4, 2];

                if (objListTrainingDetail.Count > 0)
                {
                    //Header Values
                    arrayData[0, 0] = "Name of Seminar";
                    arrayData[1, 0] = "Date";
                    arrayData[2, 0] = "Status";
                    arrayData[3, 0] = "Name of Participants";

                    //Row Details
                    for (int i = 0; i <= objListTrainingDetail.Count - 1; i++)
                    {
                        arrayData[i, 1] = objListTrainingDetail[i];
                    }

                }

                IEmailTableData objEmailTableData = new EmailTableData();
                objEmailTableData.RowDetail = arrayData;
                objEmailTableData.RowCount = objListTrainingDetail.Count;
                return objEmailTableData.GetVerticalHeaderTableData(objEmailTableData);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionGetHTMLForTableDataForSeminar", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }

        #endregion Raise Training Request

        #region Training Effectiveness

        public static void SendMailToManagerForPostTrainingRatining(int LoginEmpID, DataSet ds, bool SendMailToNewManager = false)
        {
            try
            {
                string URL = string.Empty;
                string LineManagerEmailID = string.Empty;
                ArrayList reportingManagerMailIds;
                ICommonService _commonsService = new CommonService();
                bool isManagerChanged = false;

                if (ds.Tables[0].Rows.Count != CommonConstants.DefaultFlagZero)
                {
                    //For To

                    IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                        Convert.ToInt16(EnumsConstants.EmailFunctionality.SendToManagerForPostRating));

                    //Issue id : 57808 Start
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        //isManagerChanged = Convert.ToBoolean(ds.Tables[0].Rows[i][DbTableColumn.IsReportingManagerUpdated]);

                        //if (!isManagerChanged || !SendMailToNewManager)
                        //{
                            if (!LineManagerEmailID.Contains(ds.Tables[0].Rows[i][CommonConstants.NominatingManagerEmailID].ToString()))
                            {
                                LineManagerEmailID += ds.Tables[0].Rows[i][CommonConstants.NominatingManagerEmailID].ToString() + ",";
                            }
                        //}
                        //else
                        //{
                        //    reportingManagerMailIds = _commonsService.GetReportingManagerEmailIds(Convert.ToInt32(ds.Tables[0].Rows[i][CommonConstants.EmpID]));

                        //    if (reportingManagerMailIds != null && reportingManagerMailIds.Count > 0)
                        //    {
                        //        foreach (string emailId in reportingManagerMailIds)
                        //        {
                        //            if (!LineManagerEmailID.Contains(emailId))
                        //                LineManagerEmailID += emailId + ",";
                        //        }
                        //    }
                        //}
                    }
                    //Issue id : 57808 End

                    //obj.To.Add(ds.Tables[0].Rows[i][CommonConstants.LineManagerEMailID].ToString());
                    obj.To.Add(LineManagerEmailID);
                    
                    //For CC
                    obj.CC.Add(CommonConstants.EmailIdOfRMOGroup);

                    string sendPostTrainingRating = string.Concat(ConfigurationManager.AppSettings[CommonConstants.BaseUrl].ToString(), CommonConstants.TrainingEffectivenessRoute);

                    string link = string.Format("<a href='{0}'>{1}</a>", sendPostTrainingRating, sendPostTrainingRating);

                    //URL = "http://rav-vm-srv-096/RMSTraining/Nomination/TrainingEffectiveness";

                    //For Body
                    //string strUserName = ds.Tables[0].Rows[i][CommonConstants.LineManagerEMailID].ToString();
                    //if (!string.IsNullOrEmpty(strUserName) && strUserName.Contains("@"))
                    //{
                    //    int position = strUserName.IndexOf("@");
                    //    if (position != -1)
                    //    {
                    //        string replaceStr = strUserName.Remove(0, position);
                    //        strUserName = (strUserName.Replace(replaceStr, "").Trim()).Replace(".", " ");
                    //    }
                    //}

                    obj.Subject = string.Format(obj.Subject, ds.Tables[0].Rows[0][CommonConstants.TrainingName]);
                    obj.Body = string.Format(obj.Body,
                        ds.Tables[0].Rows[0][CommonConstants.TrainingName].ToString(),
                        string.Empty,//strUserName,
                        ds.Tables[0].Rows[0][CommonConstants.PostRatingDueDate].ToString(),
                        link,
                        CommonConstants.RMGroupName);

                    obj.SendEmail(obj);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "SendMailToManagerForPostTrainingRatining", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }

        public static void ManagerFilledPostTrainingRating(string RoleName, DataSet ds)
        {
            try
            {
                IRMSEmail obj;

                if (RoleName.ToUpper() == CommonConstants.AdminRole.ToUpper())
                {
                    obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                     Convert.ToInt16(EnumsConstants.EmailFunctionality.PostTrainingRatingSubmitedByRMGOnBehalfOfManager));

                    if (ds.Tables[0].Rows.Count != CommonConstants.DefaultFlagZero)
                    {
                        obj.CC.Add(CommonConstants.EmailIdOfRMOGroup);

                        string ToEmailID = string.Empty;
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ToEmailID += ds.Tables[0].Rows[i][CommonConstants.LineManagerEMailID].ToString() + ",";
                        }

                        obj.To.Add(ToEmailID);

                        string strUserName = string.Empty;
                        if (ds.Tables[0].Rows.Count > 1)
                        {
                            strUserName = "All";
                        }
                        else
                        {
                            strUserName = ds.Tables[0].Rows[0][CommonConstants.LineManagerEMailID].ToString();
                            if (!string.IsNullOrEmpty(strUserName) && strUserName.Contains("@"))
                            {
                                int position = strUserName.IndexOf("@");
                                if (position != -1)
                                {
                                    string replaceStr = strUserName.Remove(0, position);
                                    strUserName = (strUserName.Replace(replaceStr, "").Trim()).Replace(".", " ");
                                }
                            }
                        }

                        obj.Body = string.Format(obj.Body,
                            ds.Tables[0].Rows[0][CommonConstants.TrainingName].ToString(),
                            //strUserName,
                            UppercaseWords(strUserName),
                            CommonConstants.RMGroupName);

                        obj.Subject = string.Format(obj.Subject, ds.Tables[0].Rows[0][CommonConstants.TrainingName]);

                        obj.SendEmail(obj);
                    }
                }
                else
                {
                    obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                            Convert.ToInt16(EnumsConstants.EmailFunctionality.ManagerFilledPostTrainingRating));

                    if (ds.Tables[0].Rows.Count != CommonConstants.DefaultFlagZero)
                    {
                        obj.To.Add(CommonConstants.EmailIdOfRMOGroup);

                        obj.CC.Add(ds.Tables[0].Rows[0][CommonConstants.LineManagerEMailID].ToString());

                        string strUserName = ds.Tables[0].Rows[0][CommonConstants.LineManagerEMailID].ToString();
                        if (!string.IsNullOrEmpty(strUserName) && strUserName.Contains("@"))
                        {
                            int position = strUserName.IndexOf("@");
                            if (position != -1)
                            {
                                string replaceStr = strUserName.Remove(0, position);
                                strUserName = (strUserName.Replace(replaceStr, "").Trim()).Replace(".", " ");
                            }
                        }

                        obj.Body = string.Format(obj.Body,
                            ds.Tables[0].Rows[0][CommonConstants.TrainingName].ToString(),
                            UppercaseWords(strUserName),
                            CommonConstants.RMGroupName);

                        obj.Subject = string.Format(obj.Subject, ds.Tables[0].Rows[0][CommonConstants.TrainingName]);

                        obj.SendEmail(obj);
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "ManagerFilledPostTrainingRating", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }

        static string UppercaseWords(string value)
        {
            char[] array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }

        public static void SendEffectivenessMailToAll(int LoginEmpID, DataSet ds)
        {
            try
            {
                string URL = string.Empty;
                string CCEmailID = string.Empty;
                string ToEmailID = string.Empty;
                if (ds.Tables[0].Rows.Count != CommonConstants.DefaultFlagZero)
                {
                    IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                     Convert.ToInt16(EnumsConstants.EmailFunctionality.SendEffectivenessMailToAll));

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ToEmailID += ds.Tables[0].Rows[i][CommonConstants.EmployeeEmailId].ToString() + ",";

                        CCEmailID += ds.Tables[0].Rows[i][CommonConstants.LineManagerEMailID].ToString() + ",";
                    }

                    CCEmailID += CommonConstants.EmailIdOfRMOGroup;

                    string DistinctToEmailID = string.Join(",", ToEmailID.Split(',').Distinct().ToArray());

                    obj.To.Add(DistinctToEmailID);

                    string DistinctCCEmailID = string.Join(",", CCEmailID.Split(',').Distinct().ToArray());

                    obj.CC.Add(DistinctCCEmailID);

                    //For Body
                    obj.Body = string.Format(obj.Body,
                        ds.Tables[0].Rows[0][CommonConstants.TrainingName].ToString(),
                        CommonConstants.RMGroupName,
                        GetHTMLTableForSendEffectivenessMailToAll(ds.Tables[0])
                        );

                    obj.SendEmail(obj);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "SendEffectivenessMailToAll", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }

        private static string GetHTMLTableForSendEffectivenessMailToAll(DataTable dt)
        {

            try
            {
                string[,] arrayData = new string[dt.Rows.Count, 5];

                if (dt.Rows.Count > 0)
                {
                    //Header Values
                    arrayData[0, 0] = "Employee Name";
                    arrayData[0, 1] = "Line Manager";
                    arrayData[0, 2] = "Pre Training Rating/Objective for nomination";
                    arrayData[0, 3] = "Post Assessment score";
                    arrayData[0, 4] = "Post Training rating";

                    //Row Details
                    for (int a = 0; a <= (dt.Rows.Count - 1); a++)
                    {
                        List<string> objListPostTrainingDetails = new List<string>();

                        objListPostTrainingDetails.Add(dt.Rows[a][CommonConstants.EMPLOYEE_NAME].ToString());
                        objListPostTrainingDetails.Add(dt.Rows[a][CommonConstants.LineManagerEMailID].ToString());
                        objListPostTrainingDetails.Add(dt.Rows[a][CommonConstants.PreTrainingRating].ToString());
                        objListPostTrainingDetails.Add(dt.Rows[a][CommonConstants.Assessment].ToString());
                        objListPostTrainingDetails.Add(dt.Rows[a][CommonConstants.PostTrainingRating].ToString());

                        for (int b = 0; b <= 4; b++)
                        {
                            arrayData[a, b] = objListPostTrainingDetails[b];
                        }
                    }
                }

                IEmailTableData objEmailTableData = new EmailTableData();
                objEmailTableData.RowDetail = arrayData;
                objEmailTableData.RowCount = dt.Rows.Count;
                return objEmailTableData.GetEfftivenessTableData(objEmailTableData);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "GetHTMLTableForSendEffectivenessMailToAll", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }

        #endregion Training Effectiveness

        #region Approved Reject
        public static void SendMailForTechSoftSkillApproved(int RaiseId, DataSet dt)
        {
            try
            {
                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                   Convert.ToInt16(EnumsConstants.EmailFunctionality.TechSoftSkillApproved));

                //DataSet dt = saveTrainingBL.GetEmailIDForAppRejTechSoftSkill(RaiseId);
                if (dt.Tables[0].Rows.Count != 0)
                {

                    //For TO--RMGroup
                    obj.To.Add(dt.Tables[0].Rows[0]["RequestRaiserEMailID"]);

                    //For CC--Line manager of Request Raiser, Request Raiser
                    string EmailID = string.Empty;
                    for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                    {
                        EmailID += dt.Tables[0].Rows[i]["LineManagerEMailID"].ToString() + ',';
                    }
                    EmailID += CommonConstants.EmailIdOfRMOGroup;
                    obj.CC.Add(EmailID);

                    //For Body
                    obj.Body = string.Format(obj.Body, dt.Tables[1].Rows[0]["RequestRaiserName"], GetHTMLForTableDataForTechSoftApproved(dt.Tables[1]), CommonConstants.RMGroupName);

                    obj.SendEmail(obj);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionSendMailForTechSoftSkillApproved", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }

        public static void SendMailForTechSoftSkillRejected(int RaiseId, DataSet dt)
        {
            try
            {
                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                   Convert.ToInt16(EnumsConstants.EmailFunctionality.TechSoftSkillRejectd));

                //DataSet dt = saveTrainingBL.GetEmailIDForAppRejTechSoftSkill(RaiseTrainingCollection.RaiseID);
                if (dt.Tables[0].Rows.Count != 0)
                {

                    //For TO--RMGroup
                    obj.To.Add(dt.Tables[0].Rows[0]["RequestRaiserEMailID"]);

                    //For CC--Line manager of Request Raiser, Request Raiser
                    string EmailID = string.Empty;
                    for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                    {
                        EmailID += dt.Tables[0].Rows[i]["LineManagerEMailID"].ToString() + ',';
                    }
                    EmailID += CommonConstants.EmailIdOfRMOGroup;
                    obj.CC.Add(EmailID);

                    //For Body
                    obj.Body = string.Format(obj.Body, dt.Tables[1].Rows[0]["RequestRaiserName"], GetHTMLForTableDataForTechSoftRejected(dt.Tables[1]), CommonConstants.RMGroupName);

                    obj.SendEmail(obj);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionSendMailForTechSoftSkillRejected", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }

        public static void SendMailForSeminarApproved(DataSet dt)
        {
            try
            {
                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                   Convert.ToInt16(EnumsConstants.EmailFunctionality.SeminarsApproved));

                //DataSet dt = saveTrainingBL.GetEmailIDForAppRejTechSoftSkill(RaiseTrainingCollection.RaiseID);
                if (dt.Tables[0].Rows.Count != 0)
                {

                    //For TO--RMGroup
                    obj.To.Add(dt.Tables[0].Rows[0]["RequestRaiserEMailID"]);

                    //For CC--Line manager of Request Raiser, Request Raiser
                    string EmailID = string.Empty;
                    for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                    {
                        EmailID += dt.Tables[0].Rows[i]["LineManagerEMailID"].ToString() + ',';
                    }
                    EmailID += CommonConstants.EmailIdOfRMOGroup;
                    
                    obj.CC.Add(EmailID);

                    //For Body
                    obj.Body = string.Format(obj.Body, dt.Tables[1].Rows[0]["RequestRaiserName"], GetHTMLForTableDataForSeminarApproved(dt.Tables[1]), CommonConstants.RMGroupName);

                    obj.SendEmail(obj);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionSendMailForSeminarApproved", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }

        public static void SendMailForSeminarRejected(DataSet dt)
        {
            try
            {
                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                   Convert.ToInt16(EnumsConstants.EmailFunctionality.SeminarsRejectd));

                //DataSet dt = saveTrainingBL.GetEmailIDForAppRejTechSoftSkill(RaiseTrainingCollection.RaiseID);
                if (dt.Tables[0].Rows.Count != 0)
                {

                    //For TO--RMGroup
                    obj.To.Add(dt.Tables[0].Rows[0]["RequestRaiserEMailID"]);

                    //For CC--Line manager of Request Raiser, Request Raiser
                    string EmailID = string.Empty;
                    for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                    {
                        EmailID += dt.Tables[0].Rows[i]["LineManagerEMailID"].ToString() + ',';
                    }
                    EmailID += CommonConstants.EmailIdOfRMOGroup;
                    obj.CC.Add(EmailID);

                    //For Body
                    obj.Body = string.Format(obj.Body, dt.Tables[1].Rows[0]["RequestRaiserName"], GetHTMLForTableDataForSeminarRejected(dt.Tables[1]), CommonConstants.RMGroupName);

                    obj.SendEmail(obj);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionSendMailForSeminarRejected", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }

        private static string GetHTMLForTableDataForTechSoftApproved(DataTable dt)
        {
            try
            {
                //list for table values
                List<string> objListTrainingDetail = new List<string>();
                objListTrainingDetail.Add(dt.Rows[0]["TrainingName"].ToString());
                objListTrainingDetail.Add(dt.Rows[0]["Quarter"].ToString());
                objListTrainingDetail.Add(dt.Rows[0]["Priority"].ToString());

                string[,] arrayData = new string[3, 2];

                if (objListTrainingDetail.Count > 0)
                {
                    //Header Values
                    arrayData[0, 0] = "Name";
                    arrayData[1, 0] = "Quarter";
                    arrayData[2, 0] = "Priority";

                    //Row Details
                    for (int i = 0; i <= objListTrainingDetail.Count - 1; i++)
                    {
                        arrayData[i, 1] = objListTrainingDetail[i];
                    }

                }

                IEmailTableData objEmailTableData = new EmailTableData();
                objEmailTableData.RowDetail = arrayData;
                objEmailTableData.RowCount = objListTrainingDetail.Count;
                return objEmailTableData.GetVerticalHeaderTableData(objEmailTableData);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionGetHTMLForTableDataForTechSoftApproved", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }

        private static string GetHTMLForTableDataForTechSoftRejected(DataTable dt)
        {
            try
            {
                //list for table values
                List<string> objListTrainingDetail = new List<string>();
                objListTrainingDetail.Add(dt.Rows[0]["TrainingName"].ToString());
                objListTrainingDetail.Add(dt.Rows[0]["Quarter"].ToString());
                objListTrainingDetail.Add(dt.Rows[0]["Priority"].ToString());
                objListTrainingDetail.Add(dt.Rows[0]["ApprovalComments"].ToString());

                string[,] arrayData = new string[4, 2];

                if (objListTrainingDetail.Count > 0)
                {
                    //Header Values
                    arrayData[0, 0] = "Name";
                    arrayData[1, 0] = "Quarter";
                    arrayData[2, 0] = "Priority";
                    arrayData[3, 0] = "Reason for rejection";

                    //Row Details
                    for (int i = 0; i <= objListTrainingDetail.Count - 1; i++)
                    {
                        arrayData[i, 1] = objListTrainingDetail[i];
                    }
                }
                IEmailTableData objEmailTableData = new EmailTableData();
                objEmailTableData.RowDetail = arrayData;
                objEmailTableData.RowCount = objListTrainingDetail.Count;
                return objEmailTableData.GetVerticalHeaderTableData(objEmailTableData);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionGetHTMLForTableDataForTechSoftRejected", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }

        private static string GetHTMLForTableDataForSeminarApproved(DataTable dt)
        {
            try
            {
                //list for table values
                List<string> objListTrainingDetail = new List<string>();
                objListTrainingDetail.Add(dt.Rows[0]["Name"].ToString());
                objListTrainingDetail.Add(dt.Rows[0]["Date"].ToString());

                string[,] arrayData = new string[2, 2];

                if (objListTrainingDetail.Count > 0)
                {
                    //Header Values
                    arrayData[0, 0] = "Name";
                    arrayData[1, 0] = "Date";

                    //Row Details
                    for (int i = 0; i <= objListTrainingDetail.Count - 1; i++)
                    {
                        arrayData[i, 1] = objListTrainingDetail[i];
                    }

                }

                IEmailTableData objEmailTableData = new EmailTableData();
                objEmailTableData.RowDetail = arrayData;
                objEmailTableData.RowCount = objListTrainingDetail.Count;
                return objEmailTableData.GetVerticalHeaderTableData(objEmailTableData);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionGetHTMLForTableDataForSeminarApproved", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }

        private static string GetHTMLForTableDataForSeminarRejected(DataTable dt)
        {
            try
            {
                //list for table values
                List<string> objListTrainingDetail = new List<string>();
                objListTrainingDetail.Add(dt.Rows[0]["Name"].ToString());
                objListTrainingDetail.Add(dt.Rows[0]["Date"].ToString());
                objListTrainingDetail.Add(dt.Rows[0]["ApprovalComments"].ToString());

                string[,] arrayData = new string[3, 2];

                if (objListTrainingDetail.Count > 0)
                {
                    //Header Values
                    arrayData[0, 0] = "Name";
                    arrayData[1, 0] = "Date";
                    arrayData[2, 0] = "Reason for rejection";

                    //Row Details
                    for (int i = 0; i <= objListTrainingDetail.Count - 1; i++)
                    {
                        arrayData[i, 1] = objListTrainingDetail[i];
                    }

                }

                IEmailTableData objEmailTableData = new EmailTableData();
                objEmailTableData.RowDetail = arrayData;
                objEmailTableData.RowCount = objListTrainingDetail.Count;
                return objEmailTableData.GetVerticalHeaderTableData(objEmailTableData);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionGetHTMLForTableDataForSeminarRejected", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }
                
        #endregion

        #region Mail TO Admin

        public static void SendMailToAdmin(DataSet ds, DataSet ds1)
        {
            try
            {
                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                   Convert.ToInt16(EnumsConstants.EmailFunctionality.MailToAdmin));

                string TravelDetails1 = string.Empty;

                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    TravelDetails1 = TravelDetails1 + ds1.Tables[0].Rows[i]["FromLocation"] + " to " + ds1.Tables[0].Rows[i]["ToLocation"] + " on " + string.Format("{0:dd/MM/yyyy}", ds1.Tables[0].Rows[i]["Date"]) + "<br/>";
                }

                string AccomodationDetails = "Accommodation has to be made from " + string.Format("{0:dd/MM/yyyy}",ds.Tables[0].Rows[0]["AccomodationFromDate"]) + " to " + string.Format("{0:dd/MM/yyyy}",ds.Tables[0].Rows[0]["AccomodationToDate"]);
                string TravelDetails = "Travel details- The trainer details are as follows : <br/>" + TravelDetails1;
                string TrainerPreference = "Food arrangements have to be made for the trainer " + ds.Tables[0].Rows[0]["TrainerPreference"];
                string ParticipantsPreference = "Food arrangements have to be made for Participants " + ds.Tables[0].Rows[0]["ParticipantsPreference"];
                
                int IsAccomodationTrainer = Convert.ToInt32(ds.Tables[0].Rows[0]["IsAccomodationTrainer"].ToString());
                int IsTravelDetails = Convert.ToInt32(ds.Tables[0].Rows[0]["IsTraveDetails"].ToString());
                int IsFoodTrainer = Convert.ToInt32(ds.Tables[0].Rows[0]["IsFoodTrainer"].ToString());
                int IsFoodParticipants = Convert.ToInt32(ds.Tables[0].Rows[0]["IsFoodParticipants"].ToString());

                int[] array = new int[4];
                array[0] = IsAccomodationTrainer;
                array[1] = IsTravelDetails;
                array[2] = IsFoodTrainer;
                array[3] = IsFoodParticipants;

                string body = string.Empty;

                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] == 1)
                    {
                        if (i == 0)
                        {
                            body = body + "<br>" + AccomodationDetails;
                        }

                        if (i == 1)
                        {
                            body = body + "<br>" + TravelDetails;
                        }

                        if (i == 2)
                        {
                            body = body + "<br>" + TrainerPreference;
                        }

                        if (i == 3)
                        {
                            body = body + "<br>" + ParticipantsPreference;
                        }

                    }
                }
                obj.CC.Add(CommonConstants.EmailIdOfRMOGroup);

                if (ds.Tables[0].Rows[0]["CommentsFoodAccomodaion"] != null)
                {
                    body = body + "<br>" + ds.Tables[0].Rows[0]["CommentsFoodAccomodaion"];
                }

                obj.Subject = string.Format(obj.Subject, ds.Tables[0].Rows[0]["CourseName"]);
                //For Body
                obj.Body = string.Format(obj.Body, ds.Tables[0].Rows[0]["TrainerName"], ds.Tables[0].Rows[0]["CourseName"], body, CommonConstants.RMGroupName);

                obj.SendEmail(obj);
                
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionSendMailForSeminarRejected", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }

        #endregion

        #region Feedback
        public static void SendMailToTrainerWithRatings(List<double> Total, DataSet TrainingDetails)
        {
            try
            {
                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                   Convert.ToInt16(EnumsConstants.EmailFunctionality.TrainerFeedbackAvgRating));
                double knowledge = 0, TrainingMethodology = 0, PresentationSkills, TrainerOverall = 0, Content = 0, Facility = 0, Overall = 0;
                for (int i = 0; i < 4; i++)
                {
                    knowledge += Total[i];
                }
                knowledge = Math.Round(knowledge / 4, 1, MidpointRounding.AwayFromZero);

                for (int i = 4; i < 7; i++)
                {
                    TrainingMethodology += Total[i];
                }
                TrainingMethodology = Math.Round(TrainingMethodology / 3, 1, MidpointRounding.AwayFromZero);

                PresentationSkills = Math.Round(Total[7], 1, MidpointRounding.AwayFromZero);

                for (int i = 8; i < 10; i++)
                {
                    TrainerOverall += Total[i];
                }
                TrainerOverall = Math.Round(TrainerOverall / 2, 1, MidpointRounding.AwayFromZero);

                for (int i = 10; i < 16; i++)
                {
                    Content += Total[i];
                }
                Content = Math.Round(Content / 6, 1, MidpointRounding.AwayFromZero);

                for (int i = 16; i < 19; i++)
                {
                    Facility += Total[i];
                }
                Facility = Math.Round(Facility / 3, 1, MidpointRounding.AwayFromZero);

                for (int i = 19; i < 21; i++)
                {
                    Overall += Total[i];
                }
                Overall = Math.Round(Overall / 2, 1, MidpointRounding.AwayFromZero);

                double TrainerAverageRating = Math.Round((knowledge + TrainingMethodology + PresentationSkills + TrainerOverall) / 4, 1, MidpointRounding.AwayFromZero);

                string TrainingName = TrainingDetails.Tables[0].Rows[0]["CourseName"].ToString();
                string TrainingStartDt = TrainingDetails.Tables[0].Rows[0]["TrainingStartDt"].ToString();
                string TrainingEndDt = TrainingDetails.Tables[0].Rows[0]["TrainingEndDt"].ToString();
                string TrainerEmailId = string.Empty; 
                int TrainingMode = Convert.ToInt32(TrainingDetails.Tables[0].Rows[0]["TrainingModeID"]);
                string TrainerName = string.Empty;
                if (TrainingMode == 1909)
                {
                    TrainerName = TrainingDetails.Tables[0].Rows[0]["TrainerNameInternal"].ToString();
                    TrainerEmailId = TrainingDetails.Tables[0].Rows[0]["TrainerEmailId"].ToString();

                }
                else if (TrainingMode == 1910)
                {
                    TrainerName = TrainingDetails.Tables[0].Rows[0]["TrainerName"].ToString();
                    TrainerEmailId = TrainingDetails.Tables[0].Rows[0]["VendorEmail"].ToString();
                }
                string CommentsForFeedback = TrainingDetails.Tables[0].Rows[0]["CommentsForFeedback"].ToString();

                string body = "<table border = '2' cellpadding = '0' cellspacing = '0' style='border-color:Black;' width = '80%'><tr><th style = 'height: 25px;background-color: #BFBFBF;font-size: 9pt;font-weight: bold;padding-left: 5px;font-family:Verdana;vertical-align:top;horizontal-align:center;'>Criteria</th><th style = 'height: 25px;background-color: #BFBFBF;font-size: 9pt;font-weight: bold;padding-left: 5px;font-family:Verdana;vertical-align:top;horizontal-align:center;'>Average Ratings(out of 5)</th></tr>";
                body += "<tr><td style = 'height: 25px;background-color: #BFBFBF;font-size: 9pt;font-weight: bold;padding-left: 5px;font-family:Verdana;vertical-align:top;horizontal-align:center;'>Knowledge of the Trainer ";
                body += "<ul><li>Concept clarity</li><li>Interest in the course</li><li>Answers given to the questions</li><li>Interaction with the participants</li></ul></td><td style = 'font-family: Verdana;	font-size: 9pt;	padding-left: 5px;vertical-align:top;'>" + knowledge + "</td></tr>";
                body += "<tr><td style = 'height: 25px;background-color: #BFBFBF;font-size: 9pt;font-weight: bold;padding-left: 5px;font-family:Verdana;vertical-align:top;horizontal-align:center;'>Training Methodology";
                body += "<ul><li>Helping participants learn through activities/exercise/examples </li><li>Trainer’s pace of teaching</li><li>Training methodology</li></ul></td><td style = 'font-family: Verdana;	font-size: 9pt;	padding-left: 5px;vertical-align:top;'>" + TrainingMethodology + "</td></tr>";
                body += "<tr><td style = 'height: 25px;background-color: #BFBFBF;font-size: 9pt;font-weight: bold;padding-left: 5px;font-family:Verdana;vertical-align:top;horizontal-align:center;'>Presentation Skills";
                body += "<ul><li>Clarity in speech</li><li>Body Language</li><li>Delivery</li><li>Audibility</li></ul></td><td style = 'font-family: Verdana;	font-size: 9pt;	padding-left: 5px;vertical-align:top;'>" + PresentationSkills + "</td></tr>";
                body += "<tr><td style = 'height: 25px;background-color: #BFBFBF;font-size: 9pt;font-weight: bold;padding-left: 5px;font-family:Verdana;vertical-align:top;horizontal-align:center;'>Overall rating for the Trainer <ul><li>Trainer can be recommended for similar training</li><li>Objective of the training was met by the trainer</li></ul><td style = 'font-family: Verdana;	font-size: 9pt;	padding-left: 5px;vertical-align:top;'>" + TrainerOverall + "</td></tr></table><br/><br/>";
                //body += "<tr style='border:inset'><td style='background: lightgrey'>Total Rating Od the trainer </td><td>" + TrainerAverageRating + "</td></tr>";
                if(CommentsForFeedback != "")
                {
                    body += "Additional Comments : " + CommentsForFeedback;
                }

                obj.Subject = string.Format(obj.Subject, TrainingName);
                obj.To.Add(CommonConstants.EmailIdOfRMOGroup);
                //obj.To.Add(TrainerEmailId);
                obj.CC.Add(CommonConstants.EmailIdOfRMOGroup);
                obj.Body = string.Format(obj.Body, TrainerEmailId, TrainingName, TrainerName, TrainingStartDt, TrainingEndDt, body, CommonConstants.RMGroupName);
                obj.SendEmail(obj);
                

            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "FunctionSendMailForSeminarRejected", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
            
        }
        #endregion


        /// <summary>
        /// Send mail on Nomination Submission
        /// </summary>
        /// <value>trainingcourseID</value>        
        /// <value>trainingnameID</value>        
        /// <value>deleteemployeeid</value>  
        public static void SendMailForNominationSubmission(List<Employee> savedEmployee)
        {
            try
            {
                IRMSEmail obj = null;
                // Changed by : Venkatesh  : Start
                bool IsAdminRole = false;
                //Harsha Issue Id-59073 
                //Description- Training : Create emp Nominated page so that emp can view they nomination. Change confirmation email body
                int courseId = 0;
                if (HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
                {
                    ArrayList arrRolesForUser = new ArrayList();
                    arrRolesForUser = (ArrayList)HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES];
                    if (arrRolesForUser.Contains(CommonConstants.AdminRole))
                        IsAdminRole = true;
                }
                // Changed by : Venkatesh  : End

                //Admin role can nominate Behalf of manager.
                if (IsAdminRole)
                {
                    obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                      Convert.ToInt16(EnumsConstants.EmailFunctionality.BehalfOfManagerOrEmployeeNominationSubmitted));
                }
                else
                {
                    obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                        Convert.ToInt16(EnumsConstants.EmailFunctionality.NominationSubmitted));
                }

                //DataSet savedEmployee = _service.GetEmailIDForAppRejTechSoftSkill(Raiseid);
                if (savedEmployee.Count > 0)
                {
                    //Harsha Issue Id-59073 
                    //Description- Training : Create emp Nominated page so that emp can view they nomination. Change confirmation email body
                    courseId = savedEmployee[0].courseID;
                    //For TO--RMGroup
                    obj.To.Add(CommonConstants.EmailIdOfRMOGroup);
                    //obj.To.Add(savedEmployee[0].NominatorEmailID)

                    //For CC--Line manager of Request Raiser, Request Raiser
                    string EmailID = string.Empty;
                    //Harsha Issue Id-58952 - Start
                    //Description- T Chetan submited 1 nomination, bhaumik also submited other 2 emp  nomination on behalf swapna.
                    //After submitted, email went with name chetan  instead of bhaumik in regards, with swapna  in cc
                    //EmailID = savedEmployee[0].NominatorEmailID + ',';
                    for (int i = 0; i < savedEmployee.Count; i++)
                    {
                        EmailID += savedEmployee[i].NominatorEmailID + ',';
                    }
                    //Harsha Issue Id-58952 - End
                    for (int i = 0; i < savedEmployee.Count; i++)
                    {
                        EmailID += savedEmployee[i].EmailID + ',';
                    }
                    //EmailID += CommonConstants.EmailIdOfRMOGroup;
                    obj.CC.Add(EmailID);


                    obj.Subject = string.Format(obj.Subject, savedEmployee[0].courseName);

                    string tableGrid = string.Empty;

                    string strHeaderStyle = "height: 25px;background-color: #BFBFBF;font-size: 9pt;font-weight: bold;padding-left: 5px;font-family:Verdana;vertical-align:top;horizontal-align:center;";

                    string strRowStyle = "font-family: Verdana;	font-size: 9pt;	padding-left: 5px;vertical-align:top;";

                    if (savedEmployee[0].TrainingTypeID == CommonConstants.TechnicalTrainingID)
                    {

                        int a = 0;
                        foreach (var Employee in savedEmployee)
                        {
                            //Table Header for first time
                            if (a == 0)
                            {
                                tableGrid = "<html><table border = '2' cellpadding = '0' cellspacing = '0' style='border-color:Black;' width = '80%'>" +
                                        "<th style = '" + strHeaderStyle + "'>Name</th>" +
                                        "<th style = '" + strHeaderStyle + "'>Email id</th>" +
                                        "<th style = '" + strHeaderStyle + "'>Designation</th>" +
                                        "<th style = '" + strHeaderStyle + "'>Project</th>" +
                                        "<th style = '" + strHeaderStyle + "'>Priority</th>";

                                if ((Employee.NominationTypeID == CommonConstants.ManagerNomination) && (Employee.EffectivenessID.Contains(Convert.ToString(CommonConstants.PreTrainingEffectiveness))))
                                {
                                    tableGrid += "<th style = '" + strHeaderStyle + "'>Pre Training Rating</th>";
                                }
                                //Harsha Issue Id-59073 - Start
                                //Description- Training : Create emp Nominated page so that emp can view they nomination. Change confirmation email body
                                //tableGrid += "<th style = '" + strHeaderStyle + "'>Comments</th>";
                                //Harsha Issue Id-59073 - End
                            }

                            //Table data
                            tableGrid += "<tr><td style = '" + strRowStyle + "'>" + Employee.EmployeeName + "</td>" +
                                         "<td style = '" + strRowStyle + "'>" + Employee.EmailID + "</td>" +
                                         "<td style = '" + strRowStyle + "'>" + Employee.Designation + "</td>" +
                                         "<td style = '" + strRowStyle + "'>" + Employee.Project + "</td>" +
                                         "<td style = '" + strRowStyle + "'>" + Employee.Priority + "</td>";

                            if ((Employee.NominationTypeID == CommonConstants.ManagerNomination) && (Employee.EffectivenessID.Contains(Convert.ToString(CommonConstants.PreTrainingEffectiveness))))
                            {
                                tableGrid += "<td style = '" + strRowStyle + "'>" + Employee.PreTraining + "</td>";
                            }

                            //Harsha Issue Id-59073 - Start
                            //Description- Training : Create emp Nominated page so that emp can view they nomination. Change confirmation email body
                            //tableGrid += "<td style = '" + strRowStyle + "'>" + Employee.Comment + "</td></tr>";
                            //Harsha Issue Id-59073 - End
                            a++;
                        }
                    }
                    else
                    {
                        int a = 0;
                        foreach (var Employee in savedEmployee)
                        {
                            //Table Header for first time
                            if (a == 0)
                            {
                                tableGrid = "<html><table border = '2' cellpadding = '0' cellspacing = '0' style='border-color:Black;' width = '80%'>" +
                                    "<th style = '" + strHeaderStyle + "'>Name</th>" +
                                    "<th style = '" + strHeaderStyle + "'>Email id</th>" +
                                    "<th style = '" + strHeaderStyle + "'>Designation</th>" +
                                    "<th style = '" + strHeaderStyle + "'>Project</th>" +
                                    "<th style = '" + strHeaderStyle + "'>Priority</th>";

                            //if ((Employee.NominationTypeID == CommonConstants.ManagerNomination))
                            //{
                            //    //Harsha Issue Id-59073 - Start
                            //    //Description- Training : Create emp Nominated page so that emp can view they nomination. Change confirmation email body
                            //    //tableGrid += "<th style = '" + strHeaderStyle + "'>Objective For Softskill</th>";
                            //}

                                //tableGrid += "<th style = '" + strHeaderStyle + "'>Comments</th>";
                                //Harsha Issue Id-59073 - End
                            }
                            //Table data
                            tableGrid += "<tr><td style = '" + strRowStyle + "'>" + Employee.EmployeeName + "</td>" +
                                         "<td style = '" + strRowStyle + "'>" + Employee.EmailID + "</td>" +
                                         "<td style = '" + strRowStyle + "'>" + Employee.Designation + "</td>" +
                                         "<td style = '" + strRowStyle + "'>" + Employee.Project + "</td>" +
                                         "<td style = '" + strRowStyle + "'>" + Employee.Priority + "</td>";

                            //if ((Employee.NominationTypeID == CommonConstants.ManagerNomination))
                            //{
                            //    //Harsha Issue Id-59073 - Start
                            //    //Description- Training : Create emp Nominated page so that emp can view they nomination. Change confirmation email body
                            //    //tableGrid += "<td style = '" + strRowStyle + "'>" + Employee.ObjectiveForSoftSkill + "</td>";
                            //}

                            //tableGrid += "<td style = '" + strRowStyle + "'>" + Employee.Comment + "</td></tr>";
                            //Harsha Issue Id-59073 - End

                            a++;
                        }
                    }
                    
                    
                     tableGrid += "</table></html>";
                     string confirmnominationlink = string.Concat(ConfigurationManager.AppSettings[CommonConstants.BaseUrl].ToString(), CommonConstants.NominationConfirmationRoute);
                     string link = string.Format("<a href='{0}'>{1}</a>", confirmnominationlink, confirmnominationlink);
                     //Harsha Issue Id-59073
                     //Description- Training : Create emp Nominated page so that emp can view they nomination. Change confirmation email body
                     string nominationDetailURL = string.Concat(ConfigurationManager.AppSettings[CommonConstants.BaseUrl].ToString(), CommonConstants.ViewEmployeeTrainingNominationURL + CheckAccessAttribute.Encode(Convert.ToString(courseId)));
                    string nominationDetailsLink = string.Format(" <a href='{0}' target='_blank'>Nomination Details</a>", nominationDetailURL);
                    //For Body
                     if (IsAdminRole)
                     {
                         obj.Body = string.Format(obj.Body, savedEmployee[0].NominatorName, savedEmployee[0].courseName, confirmnominationlink, tableGrid,nominationDetailsLink, CommonConstants.RMGroupName);
                     }
                     else
                     {
                         obj.Body = string.Format(obj.Body, savedEmployee[0].NominatorName, savedEmployee[0].courseName, confirmnominationlink, tableGrid, nominationDetailsLink,savedEmployee[0].NominatorName);
                     }
                    obj.SendEmail(obj);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "SendMailForNominationSubmission", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }


        /// <summary>
        /// Send mail on Nomination Deleted
        /// </summary>
        /// <value>trainingcourseID</value>        
        /// <value>trainingnameID</value>        
        /// <value>deleteemployeeid</value>  
        public static void SendMailForNominationDeleted(List<Employee> deletedEmployeeList, string LoginEmpName)
        {
            try
            {
                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                   Convert.ToInt16(EnumsConstants.EmailFunctionality.NominationDeleted));
                
                //DataSet savedEmployee = _service.GetEmailIDForAppRejTechSoftSkill(Raiseid);
                if (deletedEmployeeList.Count > 0)
                {
                    //For TO--RMGroup
                    obj.To.Add(deletedEmployeeList[0].EmailID);
                    //obj.To.Add(savedEmployee[0].NominatorEmailID)

                    //For CC--Line manager of Request Raiser, Request Raiser
                    string EmailID = string.Empty;
                    EmailID = deletedEmployeeList[0].NominatorEmailID + ',';                    
                    EmailID += CommonConstants.EmailIdOfRMOGroup;
                    obj.CC.Add(EmailID);

                    obj.Subject = string.Format(obj.Subject, deletedEmployeeList[0].courseName);
                                       
                    //For Body
                    obj.Body = string.Format(obj.Body, deletedEmployeeList[0].EmployeeName, deletedEmployeeList[0].courseName, CommonConstants.RMGroupName);

                    obj.SendEmail(obj);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "SendMailForNominationDeleted", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }


        /// <summary>
        /// Send mail on Nomination confirm
        /// </summary>
        /// <value>deletedEmployeeList</value>                
        public static void SendMailForConfirmNomination(List<Employee> confirmemployeelist)
        {
            //send mail to new confirm employee
            var newlyconfirmlist = confirmemployeelist.Where(m => m.SubmitStatus == CommonConstants.NominationSubmited).ToList<Employee>();
            if (newlyconfirmlist.Count > 0) { SendMailForNewConfirmNomination(newlyconfirmlist); }
            
            //send mail to employee who were confirm earlier but now unconfirmed
            //var nonconfirmlist = confirmemployeelist.Where(m => m.SubmitStatus == CommonConstants.NominationConfirmed).ToList<Employee>();
            //if (nonconfirmlist.Count > 0) { SendMailForNonConfirmNomination(nonconfirmlist); }            
        }

        /// <summary>
        /// Send mail on Newly Nomination confirm
        /// </summary>
        /// <value>newlyconfirmemployeelist</value>                
        public static void SendMailForNewConfirmNomination(List<Employee> newlyconfirmemployeelist)
        {
            try
            {
                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                   Convert.ToInt16(EnumsConstants.EmailFunctionality.NewNominationConfirmed));

                string salutation = string.Empty;
                //DataSet savedEmployee = _service.GetEmailIDForAppRejTechSoftSkill(Raiseid);
                if (newlyconfirmemployeelist.Count > 0)
                {
                    //For TO--RMGroup
                    foreach (var emp in newlyconfirmemployeelist)
                    {
                        obj.To.Add(emp.EmailID);
                    }
                    //obj.To.Add(newlyconfirmemployeelist[0].NomineeEmailID);
                    //obj.To.Add(savedEmployee[0].NominatorEmailID)

                    //For CC--Line manager of Request Raiser, Request Raiser
                    string EmailID = string.Empty;

                    //Ishwar Patil 58001 17052016 Start
                    foreach (var empCC in newlyconfirmemployeelist)
                    {
                        EmailID += empCC.NominatorEmailID + ',';
                    }
                    //Ishwar Patil 58001 17052016 End
                    EmailID += CommonConstants.EmailIdOfRMOGroup;

                    if (newlyconfirmemployeelist.Count == 1)
                    {
                        salutation = "Hi " + newlyconfirmemployeelist[0].EmployeeName;
                    }
                    else
                    {
                        salutation = "Dear all";
                    }

                    obj.CC.Add(EmailID);

                    obj.Subject = string.Format(obj.Subject, newlyconfirmemployeelist[0].courseName );

                    //For Body
                    obj.Body = string.Format(obj.Body, newlyconfirmemployeelist[0].courseName, salutation);

                    obj.SendEmail(obj);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "SendMailForNewConfirmNomination", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }


        /// <summary>
        /// Send mail on Removed old confirmation
        /// </summary>
        /// <value>deletedEmployeeList</value>                
        public static void SendMailForNonConfirmNomination(List<Employee> nonconfirmemployeelist)
        {
            try
            {
                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                   Convert.ToInt16(EnumsConstants.EmailFunctionality.NonConfirmNomination));

                //DataSet savedEmployee = _service.GetEmailIDForAppRejTechSoftSkill(Raiseid);
                if (nonconfirmemployeelist.Count > 0)
                {
                    //For TO--RMGroup
                    foreach (var emp in nonconfirmemployeelist)
                    {
                        obj.To.Add(emp.EmailID);
                    }
                    //obj.To.Add(savedEmployee[0].NominatorEmailID)

                    //For CC--Line manager of Request Raiser, Request Raiser
                    string EmailID = string.Empty;

                    //Ishwar Patil 58001 17052016 Start
                    //EmailID = nonconfirmemployeelist[0].NominatorEmailID + ',';
                    foreach (var empCC in nonconfirmemployeelist)
                    {
                        EmailID += empCC.NominatorEmailID + ',';
                    }
                    //Ishwar Patil 58001 17052016 Start
                    EmailID += CommonConstants.EmailIdOfRMOGroup;
                    obj.CC.Add(EmailID);

                    obj.Subject = string.Format(obj.Subject, nonconfirmemployeelist[0].TrainingName);

                    //For Body
                    obj.Body = string.Format(obj.Body, nonconfirmemployeelist[0].TrainingName);

                    obj.SendEmail(obj);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "SendMailForNonConfirmNomination", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }




        /// <summary>
        /// Send mail for Invite Nomination
        /// </summary>
        /// <value>trainingcourseID</value>        
        /// <value>trainingnameID</value>        
        /// <value>deleteemployeeid</value>  
        public static void SendMailForInviteNomination(bool reminderMail, string trainingName, string trainer, string trainingMode, DateTime startDt, DateTime endDt, float days, DateTime nominationDt, string[] strAttachments, string directory, int nominationId )   
        {
            try
            {
                IRMSEmail objRMSEmail = null;
                objRMSEmail = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule), Convert.ToInt16(EnumsConstants.EmailFunctionality.InviteNomination));
                
                objRMSEmail.Subject = string.Format(objRMSEmail.Subject, trainingName);

                string invitationLink = string.Concat(ConfigurationManager.AppSettings[CommonConstants.BaseUrl].ToString(), CommonConstants.InviteNominationRoute);
                string link = string.Format("<a href='{0}'>{1}</a>", invitationLink, invitationLink);
                //For Body
                string strNominationBy, strNominationWarning, strAttachText, invitationText;
                strNominationBy = strNominationWarning = strAttachText = invitationText = string.Empty;
                
                if (!reminderMail)
                    invitationText = "We have scheduled a training on " + trainingName;
                else
                    invitationText = "<b>Please note we have extended the nomination date to " + nominationDt.ToString("dd/MM/yyyy") + "</b>";

                if (nominationId == CommonConstants.SelfNomination)
                    strNominationBy = "Request you to send in your nominations by " + nominationDt.ToString("dd/MM/yyyy") + " by clicking on the link below:";
                else if (nominationId == CommonConstants.ManagerNomination)
                {
                    strNominationBy = "Request you to send in your nominations through your respective line managers by " + nominationDt.ToString("dd/MM/yyyy") + " by clicking on the link below:";
                    strNominationWarning = "Line Managers are requested to confirm the availability of the participants before sending the nominations.<br/>Nominations will be accepted only if they have come through the Line Managers and not through individuals.<br/><br/>";
                }
                
                for (int i = 0; i < strAttachments.Length; i++)
                {
                    Attachment courseContentFile = new Attachment(Path.Combine(HttpContext.Current.Server.MapPath("~/" + directory), strAttachments[i]));
                    courseContentFile.Name = strAttachments[i].Substring(strAttachments[i].IndexOf("_") + 1, (strAttachments[i].Length - (strAttachments[i].IndexOf("_") + 1)));
                    objRMSEmail.AddAttachment(courseContentFile);
                    strAttachText = "Course content: Attached<br/><br/>";
                }
                objRMSEmail.To.Add(CommonConstants.EmailIdOfRMOGroup);
                //objRMSEmail.To.Add(CommonConstants.EmailIdOfRaveIndia);
                objRMSEmail.CC.Add(CommonConstants.EmailIdOfRMOGroup);
                objRMSEmail.Body = string.Format(objRMSEmail.Body, trainingName, trainer, trainingMode, startDt.ToString("dd/MM/yyyy"), endDt.ToString("dd/MM/yyyy"), days, strNominationBy, link, strNominationWarning, strAttachText, invitationText);

                objRMSEmail.SendEmail(objRMSEmail);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "SendMailForInviteNomination", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }

        #region Attendance 
        public static void SendMailToProvideFeedback(int CourseId, DataSet dt)
        {
            try
            {
                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                   Convert.ToInt16(EnumsConstants.EmailFunctionality.SendFeedback));

                if (dt.Tables[0].Rows.Count != 0)
                {

                    //For TO--RMGroup
                    for (int i = 0; i < dt.Tables[0].Rows.Count; i++) 
                    {
                        //to mail address
                        if (Convert.ToString(dt.Tables[0].Rows[i]["Emailid"]).Trim() != "")
                            obj.To.Add(Convert.ToString(dt.Tables[0].Rows[i]["Emailid"]).Trim());
                    }

                    //For CC--Line manager of Request Raiser, Request Raiser
                    for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                    {
                        if (Convert.ToString(dt.Tables[0].Rows[i]["Managerid"]).Trim() != "")
                            obj.CC.Add(Convert.ToString(dt.Tables[0].Rows[i]["Managerid"]).Trim());
                    }
                    obj.CC.Add(CommonConstants.EmailIdOfRMOGroup);
                    obj.Subject = string.Format(obj.Subject, dt.Tables[0].Rows[0]["TrainingName"]);
                    //For Body

                    string FeedbackLink = string.Concat(ConfigurationManager.AppSettings[CommonConstants.BaseUrl].ToString(), CommonConstants.FeedbackLinkRoute);
                    string link = string.Format("<a href='{0}'>{1}</a>", FeedbackLink, FeedbackLink);

                    obj.Body = string.Format(obj.Body, dt.Tables[0].Rows[0]["TrainingName"], dt.Tables[0].Rows[0]["FeedbackLastDate"], link, CommonConstants.RMGroupName);

                    obj.SendEmail(obj);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.UILayer, PAGENAME, "SendMailToProvideFeedback", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }

        public static void SendMailToDropOut(int CourseId, DataSet dt)
        {
            try
            {
                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                   Convert.ToInt16(EnumsConstants.EmailFunctionality.SendDropOut));

                if (dt.Tables[1].Rows.Count != 0)
                {
                    //For TO--RMGroup
                    string strBodyEmployee = string.Empty;
                    strBodyEmployee = "<table>";
                    for (int i = 0; i < dt.Tables[1].Rows.Count; i++)
                    {
                        //to mail address
                        if (Convert.ToString(dt.Tables[1].Rows[i]["Managerid"]).Trim() != "")
                            obj.To.Add(Convert.ToString(dt.Tables[1].Rows[i]["Managerid"]).Trim());

                        if (Convert.ToString(dt.Tables[1].Rows[i]["Employeename"]).Trim() != "")
                            strBodyEmployee += "<tr><td><font size=2> " + Convert.ToString(dt.Tables[1].Rows[i]["Employeename"]).Trim() + "</font></td></tr>";
                    }
                    strBodyEmployee += "</table>";

                    //For CC--Line manager of Request Raiser, Request Raiser
                    for (int i = 0; i < dt.Tables[1].Rows.Count; i++)
                    {
                        if (Convert.ToString(dt.Tables[1].Rows[i]["Emailid"]).Trim() != "")
                            obj.CC.Add(Convert.ToString(dt.Tables[1].Rows[i]["Emailid"]).Trim());
                    }
                    obj.CC.Add(CommonConstants.EmailIdOfRMOGroup);
                    obj.Subject = string.Format(obj.Subject, dt.Tables[1].Rows[0]["TrainingName"]);
                    //For Body
                    obj.Body = string.Format(obj.Body, dt.Tables[1].Rows[0]["TrainingName"],  strBodyEmployee,CommonConstants.RMGroupName);

                    obj.SendEmail(obj);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.UILayer, PAGENAME, "SendMailToDropOut", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }
        #endregion






        public static void SendMailForNominationRejected(List<Employee> deletedEmployeeList, string empName, string ReasonForRejection)
        {
            try
            {
                IRMSEmail obj = new RMSEmail(Convert.ToInt16(EnumsConstants.RMSModule.TrainingModule),
                   Convert.ToInt16(EnumsConstants.EmailFunctionality.NominationRejected));

                string EmailID = string.Empty;
                string empEmail = string.Empty;
                string salutation = string.Empty;
                //DataSet savedEmployee = _service.GetEmailIDForAppRejTechSoftSkill(Raiseid);
                foreach (var emp in deletedEmployeeList)
                {
                    EmailID += emp.NominatorEmailID + ',';
                    empEmail += emp.EmailID + ',';
                }

                if (deletedEmployeeList.Count == 1)
                {
                    salutation = "Hi " + deletedEmployeeList[0].EmployeeName;
                }
                else
                {
                    salutation = "Dear all";
                }
                //For TO--RMGroup
                EmailID += CommonConstants.EmailIdOfRMOGroup;
                
                obj.To.Add(empEmail);
                //For CC--Line manager of Request Raiser, Request Raiser
                obj.CC.Add(EmailID);

                obj.Subject = string.Format(obj.Subject, deletedEmployeeList[0].courseName);

                //For Body
                obj.Body = string.Format(obj.Body, salutation, deletedEmployeeList[0].courseName, empName, ReasonForRejection);

                obj.SendEmail(obj);
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, PAGENAME, "SendMailForNominationDeleted", EventIDConstants.TRAINING_PRESENTATION_LAYER);
            }
        }
    }


     
        
}