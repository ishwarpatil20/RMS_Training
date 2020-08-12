//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2015 all rights reserved.
//
//  File:           EmailHelper.cs       
//  Author:         jagmohan.rawat
//  Date written:   13/07/2015/ 10:58:30 AM
//  Description:    EmailHelper page is use for creating email template and sending mail to end user
//
//  Amendments
//  Date                        Who                 Ref     Description
//  ----                        -----------         ---     -----------
//  13/07/2015/ 10:58:30 AM     jagmohan.rawat      n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Domain.Interfaces;
using RMS.Common;
using RMS.Common.BusinessEntities.Email;
using RMS.Common.Constants;
using RMS.Common.ExceptionHandling;

namespace RMS.Helpers
{
    public class EmailHelper
    {
        private const string PAGENAME = "RaiseTrainingSummary.aspx";        

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
    }
}