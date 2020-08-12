using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Web;
using System.Net.Mail;
using System.Configuration;
using System;
using System.Collections;
using RMS.Common.BusinessEntities.Email;
using RMS.Common.Constants;
using RMS.Common.AuthorizationManager;
using RMS.Common.ExceptionHandling;



namespace RMS.Common.BusinessEntities.Email
{
    public class RMSEmail : IRMSEmail
    {

        #region Private Field Members

        private string CLASS_NAME_RP = "RMSEmail.cs";
        private int _moduleTypeId;
        private int _emailTypeId;
        private string _from;
        private ArrayList _to;
        private ArrayList _cc;
        private string _subject;
        private string _body;
        private XmlDocument xmlDoc;
        private XmlNode defaultnode;
        private string _location;
        private XmlNode node;

        #endregion Private Field Members

        #region Public methods
        public RMSEmail(int moduleTypeId,
                        int emailTypeId)
        {
            //--Get xml document
            xmlDoc = new XmlDocument();

            XmlUrlResolver resolver = new XmlUrlResolver();
            resolver.Credentials = System.Net.CredentialCache.DefaultCredentials;

            //XmlReader reader = XmlReader.Create(url);
            
            xmlDoc.XmlResolver = resolver;

            //if (moduleTypeId == 8)
            //{
            //    xmlDoc.Load(HttpContext.Current.Server.MapPath(CommonConstants.EMAIL_CONFIGURATION_FILEPATH_LevelBack)); 
            //}
            //else
            //{
            xmlDoc.Load(ConfigurationManager.AppSettings[CommonConstants.EmailConfigUrl].ToString());
            //}
            
            //--Get default location
            defaultnode = xmlDoc.SelectSingleNode("//defaulSetting");
            XmlNode childdefaultnode = defaultnode.SelectSingleNode("location");
            _location = defaultnode.InnerText.Trim();

            //--Asign properties
            _moduleTypeId = moduleTypeId;
            _emailTypeId = emailTypeId;

            //--Get node
            node = xmlDoc.SelectSingleNode("//Module[@ModuleTypeId='" + _moduleTypeId + "']/Emails/Email[@EmailTypeId='" + _emailTypeId + "']/Location[@Name='" + _location + "']");

            //--Get values
            _to = To;
            _cc = CC;
            _subject = Subject;
            _body = Body;
        }

        public RMSEmail(int moduleTypeId,
                        int emailTypeId, string location)
        {
            //--Asign properties
            _moduleTypeId = moduleTypeId;
            _emailTypeId = emailTypeId;
            _location = location;

            //--Get xml document
            xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Server.MapPath(CommonConstants.EMAIL_CONFIGURATION_FILEPATH));

            //--Get default location
            defaultnode = xmlDoc.SelectSingleNode("//defaulSetting");
            XmlNode childdefaultnode = defaultnode.SelectSingleNode("location");
            _location = _location.Trim();


            //--Get node
            node = xmlDoc.SelectSingleNode("//Module[@ModuleTypeId='" + _moduleTypeId + "']/Emails/Email[@EmailTypeId='" + _emailTypeId + "']/Location[@Name='" + _location + "']");

            //--Get values
            _to = To;
            _cc = CC;
            _subject = Subject;
            _body = Body;
        }

        public string EmailFrom()
        {
            //--Get child node
            XmlNode childnode = node.SelectSingleNode("From");
            return childnode.InnerText.Trim();
        }

        public ArrayList EmailReceipientTo()
        {
            //--Get child node
            XmlNode childnode = node.SelectSingleNode("To");
            //return childnode.InnerText.Trim();
            return GetArrayList(childnode.InnerText.Trim());
        }

        public ArrayList EmailReceipientCC()
        {
            //--Get child node
            XmlNode childnode = node.SelectSingleNode("CC");
            return GetArrayList(childnode.InnerText.Trim());
        }

        public string EmailSubject()
        {
            //--Get child node
            XmlNode childnode = node.SelectSingleNode("Subject");
            return childnode.InnerText.Trim();
        }

        public string EmailBody()
        {
            //--Get child node
            XmlNode childnode = node.SelectSingleNode("Body");
            return childnode.InnerText.Trim();
        }

        #endregion Public methods

        #region IRMSEmail Members

        public string From
        {
            get
            {
                if (_from == null) { return EmailFrom(); } else { return _from; }
            }
            set
            {
                _from = value;
            }
        }

        public ArrayList To
        {
            get
            {
                if (_to == null) { return EmailReceipientTo(); } else { return _to; }
            }
            set
            {
                _to.Add(value);
            }
        }

        public ArrayList CC
        {
            get
            {
                if (_cc == null) { return EmailReceipientCC(); } else { return _cc; }
            }
            set
            {
                _cc.Add(value);
            }
        }

        public string Subject
        {
            get
            {
                if (_subject == null) { return EmailSubject(); } else { return _subject; }
            }
            set
            {
                _subject = value;
            }
        }

        public string Body
        {
            get
            {
                if (_body == null) { return EmailBody(); } else { return _body; }
            }
            set
            {
                _body = value;
            }
        }

        private ArrayList GetArrayList(string str)
        {
            ArrayList arrList = new ArrayList();
            string[] arr = str.Split(',');
            foreach (string list in arr)
            {
                arrList.Add(list);
            }

            return arrList;
        }

        //send mail function
        public void SendEmail(IRMSEmail objEmailConfigEntities)
        {
            SmtpClient mailClient = null;
            MailMessage message = null;
            try
            {
                //--Check mailing on or off
                if (ConfigurationManager.AppSettings["MailingOnOrOff"].ToString().ToLower() == "off")
                {
                    return;
                }

                //--Get the entity values
                string strFrom = objEmailConfigEntities.From;
                if (strFrom.Trim() == string.Empty)
                {
                    //--Get logged in user
                    AuthorizationManager.AuthorizationManager authoriseduser = new AuthorizationManager.AuthorizationManager();
                    strFrom = authoriseduser.getLoggedInUserEmailId();
                    //GoogleMail
                    //strFrom = getLoggedInUserEmailId();
                }
                string strTo = string.Join(",", (string[])objEmailConfigEntities.To.ToArray(Type.GetType("System.String")));
                string strCC = string.Join(",", (string[])objEmailConfigEntities.CC.ToArray(Type.GetType("System.String")));
                string strSubject = objEmailConfigEntities.Subject;
                string strBody = objEmailConfigEntities.Body;

                string strHead = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Transitional//EN\"><HEAD><META http-equiv=Content-Type content=\"text/html; charset=iso-8859-1\"><META content=\"MSHTML 6.00.6000.16525\" name=GENERATOR><STYLE></STYLE></HEAD><BODY><DIV><FONT face=Verdana size=2>";

                string strFooter = "</font></div></body>";

                mailClient = new SmtpClient();
                message = new MailMessage();
                string strFullBody = strBody;

                //smtp mail host
                mailClient.Host = ConfigurationManager.AppSettings["SmtpServer"];
                //smtp mail port
                mailClient.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);

                //from mail Address
                MailAddress fromAddress = new MailAddress(strFrom);
                message.From = fromAddress;

                char chr = ',';
                strTo = strTo.Trim(chr);
                strCC = strCC.Trim(chr);

                string[] strToMails = strTo.Split(chr);
                string[] strCCMails = strCC.Split(chr);

                for (int cont = 0; cont < strToMails.Length; cont++)
                {
                    //to mail address
                    if (strToMails[cont].Trim() != "")
                    {
                        message.To.Add(strToMails[cont]);
                        //// CR - 29936 Sachin Start
                        //// Commented following code and Added conditions to check whether employee has exited of not
                        //// message.To.Add(strToMails[cont]);
                        //Rave.HR.BusinessLayer.Employee.Employee EmpTo = new Rave.HR.BusinessLayer.Employee.Employee();
                        //BusinessEntities.Employee emp = new BusinessEntities.Employee();
                        //emp.EmailId = strToMails[cont];
                        //emp = EmpTo.GetEmployeeByEmpId(emp);
                        //if (emp != null && emp.LastWorkingDay != null)
                        //{
                        //    if (emp.LastWorkingDay == DateTime.MinValue)
                        //        message.To.Add(strToMails[cont]);
                        //}
                        //// CR - 29936 Sachin End
                    }
                }

                for (int cont = 0; cont < strCCMails.Length; cont++)
                {
                    //cc mail address
                    if (strCCMails[cont].Trim() != "")
                    {
                        message.CC.Add(strCCMails[cont]);
                        //// CR - 29936 Sachin
                        //// Commented following code and Added conditions to check whether employee has exited of not
                        //// message.CC.Add(strCCMails[cont]);
                        //Rave.HR.BusinessLayer.Employee.Employee EmpTo = new Rave.HR.BusinessLayer.Employee.Employee();
                        //BusinessEntities.Employee emp = new BusinessEntities.Employee();
                        //emp.EmailId = strCCMails[cont];
                        //emp = EmpTo.GetEmployeeByEmpId(emp);
                        //if (emp != null && emp.LastWorkingDay != null)
                        //{
                        //    if (emp.LastWorkingDay == DateTime.MinValue)
                        //        message.CC.Add(strCCMails[cont]);
                        //}
                        //// CR - 29936 Sachin End
                    }
                }

                //--CC to self
                message.CC.Add(strFrom);

                //--Subject
                message.Subject = strSubject;
                //--Body
                message.Body = strHead + strFullBody + strFooter;
                message.IsBodyHtml = true;

                //--Send
                mailClient.Send(message);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message,
                                          ex,
                                          Sources.CommonLayer,
                                          CLASS_NAME_RP,
                                          "SendEmail",
                                          EventIDConstants.MAIL_EXCEPTION);
            }
            finally
            {
                if (message != null)
                    message = null;
                if (mailClient != null)
                    mailClient = null;
            }
        }

        //GoogleMail
        //private string getLoggedInUserEmailId()
        //{
        //    string strUserIdentity = HttpContext.Current.Request.LogonUserIdentity.Name;
        //    try
        //    {
        //        int position = strUserIdentity.IndexOf("\\");
        //        strUserIdentity = strUserIdentity.Remove(0, position + 1);
        //        strUserIdentity = strUserIdentity.Replace(AuthorizationManagerConstants.RAVEDOMAIN + @"\", "");
        //        //strUserIdentity = strUserIdentity + "@" + AuthorizationManagerConstants.RAVEDOMAIN;
        //        //NorthgateChange comment else part and remove if condition.
        //        if (strUserIdentity.ToUpper().Trim() == "SAWITA.KAMAT" || strUserIdentity.ToUpper().Trim() == "VRATIKA.ARORA")
        //        {
        //            //Google
        //            //strUserIdentity = strUserIdentity + "@" + AuthorizationManagerConstants.RAVEDOMAIN;
        //            strUserIdentity = strUserIdentity + "@" + AuthorizationManagerConstants.NORTHGATEDOMAIN;
        //        }
        //        else
        //        {
        //            strUserIdentity = strUserIdentity + "@" + AuthorizationManagerConstants.RAVEDOMAIN;
        //        }
        //        strUserIdentity = strUserIdentity.Replace("co.in", "com");
        //    }
        //    catch (RaveHRException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME_RP, "getLoggedInUserEmailId", EventIDConstants.AUTHORIZATION_MANAGER_ERROR);
        //    }
        //    return strUserIdentity;
        //}

        #endregion

    }
}
