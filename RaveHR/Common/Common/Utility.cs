///------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           Utility.cs       
//  Author:         Gaurav Thakkar
//  Date written:   14/04/2009/ 11:59:35 PM
//  Description:    This class provides the methods utilities like mailing etc.
//                  
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  14/04/2009 11:59:35 PM  Gaurav Thakkar    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using System.Linq;
namespace Common
{
    public class Utility
    {
        #region Private Field Members

        /// <summary>
        /// define CLASS_NAME_RP
        /// </summary>
        private const string CLASS_NAME_RP = "Utility.cs";

        private const string PASSWORD = "Password";

        private const string EQUALS = "=";

        private const string SEMICOLON = ";";

        private const string BLANK = "";

        #endregion Private Field Members

        public string GetEmployeeFirstName(string strName)
        {
            //Googleconfigurable
            if (strName.Contains("@"))
            {
                int position = strName.IndexOf("@");
                string replaceStr = strName.Remove(0, position);
                strName = strName.Replace(replaceStr, "").ToLower().Trim();
                //strName = strName.Replace("@rave-tech.com", "");
                string[] strNameArr = strName.Split('.');

                if (strNameArr.Length >= 1)
                {
                    strName = strNameArr[0];
                }
                else
                {
                    strName = "";
                }
            }
            else if(strName.Contains(" "))
            {
                string[] strNameArr = strName.Split(' ');

                if (strNameArr.Length >= 1)
                {
                    strName = strNameArr[0];
                }
                else
                {
                    strName = "";
                }
            }

            strName = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(strName);
            return strName;
        }

        /// <summary>
        /// Get The URL from Config File
        /// </summary>
        public static string GetUrl()
        {
            try
            {
                return ConfigurationSettings.AppSettings[CommonConstants.RMSURL].ToString();
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.CommonLayer, CLASS_NAME_RP, "SendMail", EventIDConstants.MAIL_EXCEPTION);
            }
        }

        /// <summary>
        /// Method to Decrypt the Password Taken of Connectionstring from ConfigFile
        /// </summary>
        public static string DecryptPassword(string strRaveHRConnectionString)
        {
            int indexOfPassword = strRaveHRConnectionString.IndexOf(PASSWORD);

            string passwordText = strRaveHRConnectionString.Substring(indexOfPassword);

            passwordText = passwordText.Replace(PASSWORD, BLANK);

            passwordText = passwordText.Replace(EQUALS, BLANK);

            passwordText = passwordText.Trim();

            int newPasswordint = passwordText.IndexOf(SEMICOLON);

            passwordText = passwordText.Substring(0, newPasswordint);

            passwordText = passwordText.Trim();

            strRaveHRConnectionString = strRaveHRConnectionString.Replace(passwordText, Clarify(passwordText));

            return strRaveHRConnectionString;
        }

        /// <summary>
        /// Metohod to convert String from Safe64 to UTF8
        /// </summary>
        private static string Clarify(string s)
        {
            string returnValue = s;
            try
            {
                byte[] b = FromSafe64(s);
                return Encoding.UTF8.GetString(b);
            }
            catch (Exception)
            {
                // Subpressing exception here
            }

            return returnValue;
        }

        /// <summary>
        /// Method to perform steps of Encryption
        /// </summary>
        private static byte[] FromSafe64(string s)
        {
            try
            {
                StringBuilder s2 = new StringBuilder(s);
                s2.Replace('|', '/');
                s2.Replace('~', '=');
                s2.Replace('*', '+');
                return Convert.FromBase64String(s2.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Venkatesh : 4C_Support 26-Feb-2014 : Start 
        //Desc : Dept Support

        /// <summary>
        /// Check is department is support dept for 4C
        /// </summary>
        /// <param name="DeptId">Dept</param>
        /// <returns></returns>
       public  static bool IsSupportDept(int DeptId)
        {
            bool isSupport = false;
            string ConfigureDeptId = ConfigurationSettings.AppSettings[CommonConstants.SupportDept].ToString();
            string[] SplitDept = ConfigureDeptId.Split(',');
            if (SplitDept.Contains(DeptId.ToString()))
            {
                isSupport = true;
            }
            return isSupport;
        }

       /// <summary>
       /// Check is department is support dept for 4C
       /// </summary>
       /// <param name="DeptId">Dept</param>
       /// <returns></returns>
       public static bool IsSupportDept(string DeptIds)
       {
           bool isSupport = false;
           string ConfigureDeptId = ConfigurationSettings.AppSettings[CommonConstants.SupportDept].ToString();
           string[] SplitDept = ConfigureDeptId.Split(',');
           string[] SplitSelectedDept = DeptIds.Split(',');
           foreach (string Dept in SplitSelectedDept)
           {
               if (SplitDept.Contains(Dept.ToString()))
               {
                   isSupport = true;
                   return isSupport;
               }
           }
           return isSupport;
       }

        //Venkatesh : 4C_Support 26-Feb-2014 : End



    }
}
