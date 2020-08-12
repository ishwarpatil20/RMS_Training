using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Common.Constants
{
    public static class AuthorizationManagerConstants
    {

        #region Genereal
        /// <summary>
        /// Rave domain name
        /// </summary>
        public const string RAVEDOMAIN = "rave-tech.co.in";
        //public const string RAVEDOMAIN = "northgate-is.co.in";

        /// <summary>
        /// Northgate Domain
        /// </summary>
        /// 
        //Googleconfigurable
        //public const string NORTHGATEDOMAIN = "northgate-is.co.in";
        //public const string RAVEDOMAIN = "northgate-is.co.in";

        /// <summary>
        /// Rave domain for email
        /// </summary>
        public const string RAVEDOMAINEMAIL = "rave-tech.com";

        /// <summary>
        /// Northgate Domain for email
        /// </summary>
        public const string NORTHGATEDOMAINEMAIL = "northgate-is.com";




        /// <summary>
        /// AzManPolicyStoreConnectionString
        /// </summary>
        public const string AZMANPOLICYSTORECONNECTIONSTRING = "AzManPolicyStoreConnectionString";

        /// <summary>
        /// Application Name
        /// </summary>
        public const string AZMANAPPLICATION = "RaveHR";

        /// <summary>
        /// Home Page
        /// </summary>
        public const string PAGEHOME = "Home.aspx";

        /// <summary>
        /// LDAP Directory Service
        /// </summary>
        public const string DIRECTORYSERVICE = "LDAP://RAVE-TECH.CO.IN";

        #endregion

        #region Define Roles

        /// <summary>
        /// Finance Role
        /// </summary>
        public const string ROLEFINANCE = "roleFinance";

        /// <summary>
        /// HR Role
        /// </summary>
        public const string ROLEHR = "roleHR";

        /// <summary>
        /// Project Manager Role
        /// </summary>
        public const string ROLEPROJECTMANAGER = "rolePM";

        /// <summary>
        /// Presales Role
        /// </summary>
        public const string ROLEPRESALES = "rolePreSales";

        /// <summary>
        /// COO Role
        /// </summary>
        public const string ROLECOO = "roleCOO";

        /// <summary>
        /// MH Role
        /// </summary>
        public const string ROLEMH = "roleMH";

        /// <summary>
        /// RPM Role
        /// </summary>
        public const string ROLERPM = "roleRPM";

        /// <summary>
        /// CEO Role
        /// </summary>
        public const string ROLECEO = "roleCEO";

        /// <summary>
        /// CFM Role
        /// </summary>
        public const string ROLECFM = "roleCFM";

        /// <summary>
        /// FM Role
        /// </summary>
        public const string ROLEFM = "roleFM";

        /// <summary>
        /// PM Role
        /// </summary>
        public const string ROLEPM = "rolePM";

        /// <summary>
        /// GPM Role
        /// </summary>
        public const string ROLEGPM = "roleGPM";

        /// <summary>
        /// APM Role: Assistant Project Manager
        /// </summary>
        public const string ROLEAPM = "roleAPM";

        /// <summary>
        /// SPM Role: Senior Project Manager
        /// </summary>
        public const string ROLESPM = "roleSPM";

        /// <summary>
        /// Recruitment Role
        /// </summary>
        public const string ROLERECRUITMENT = "roleRecruitment";

        /// <summary>
        /// Employee Role
        /// </summary>
        public const string ROLEEMPLOYEE = "roleEmployee";

        /// <summary>
        /// Employee Role
        /// </summary>
        public const string ROLEADMIN = "roleADMIN";

        /// <summary>
        /// Testing Role
        /// </summary>
        public const string ROLETESTING = "roleTesting";

        /// <summary>
        /// Quality Role
        /// </summary>
        public const string ROLEQUALITY = "roleQuality";

        /// <summary>
        /// RaveConsultant Role
        /// </summary>
        public const string ROLERAVECONSULTANT = "roleRaveConsultant";

        #endregion

        #region Operations Number Constants

        #region MasterPage Menu

        ////Operations constants range for Main Menu tabs 001 - 020
        ////Operations constants range for SubMenu tabs 021 - 100
        ////Operations constants range for Page level operation events 101 - 999

        /// <summary>
        /// Operation Main Menu - Home - View
        /// </summary>
        public const int OPERATION_MAINMENUTAB_HOME_VIEW = 001;

        /// <summary>
        /// Operation Main Menu - Project - View
        /// </summary>
        public const int OPERATION_MAINMENUTAB_PROJECT_VIEW = 002;

        /// <summary>
        /// Operation Main Menu - MRF - View
        /// </summary>
        public const int OPERATION_MAINMENUTAB_MRF_VIEW = 003;

        /// <summary>
        /// Operation Sub Menu - Project Summary - View
        /// </summary>
        public const int OPERATION_SUBMENUTAB_PROJECTSUMMARY_VIEW = 021;

        #endregion MasterPage Menu Operations constants end

        #region Project Module

        /// <summary>
        /// Operation View Project - event
        /// </summary>
        public const int OPERATION_PAGE_VIEWPROJECT_EVENT = 101;

        /// <summary>
        /// Operation Edit Project - event
        /// </summary>
        public const int OPERATION_PAGE_EDITPROJECT_EVENT = 102;

        #endregion Project Module

        #region Resource Plan Module

        /// <summary>
        /// Operation View CreateRP Page  
        /// </summary>
        public const int OPERATION_PAGE_CREATERP_VIEW = 103;

        /// <summary>
        /// Operation View EditRPMain Page  
        /// </summary>
        public const int OPERATION_PAGE_EDITMAINRP_VIEW = 104;

        /// <summary>
        /// Operation View ViewRP Page  
        /// </summary>
        public const int OPERATION_PAGE_VIEWRP_VIEW = 105;

        /// <summary>
        /// Operation View ApproveRejectRP Page  
        /// </summary>
        public const int OPERATION_PAGE_APPROVEREJECTRP_VIEW = 106;

        /// <summary>
        /// Operation View EditRPOptions Page  
        /// </summary>
        public const int OPERATION_PAGE_EDITRPOPTIONS_VIEW = 107;

        /// <summary>
        /// Operation View Reports Page  
        /// </summary>
        public const int OPERATION_PAGE_REPORTS_VIEW = 108;

        #endregion Resource Plan Module

        #endregion Operations Number Constants
    }
}
