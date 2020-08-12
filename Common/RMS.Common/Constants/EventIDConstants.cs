using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Common.Constants
{
    public static class EventIDConstants
    {

        #region EventID constants

        #region Common Event Ids From 2001- 2099

        /// <summary>
        /// Error code for SQL exception.
        /// </summary>
        public const int SQL_EXCEPTION = 2001;

        /// <summary>
        /// Error code for Wrong Connection string.
        /// </summary>
        public const int WRONG_CONNECTIONSTRING = 2002;

        /// <summary>
        /// Error code for Mailing.
        /// </summary>
        public const int MAIL_EXCEPTION = 2003;

        #endregion Common Event Ids

        #region Authorization Manager From  2101- 2199
        /// <summary>
        /// Error code for exception thrown in Data Access layer.
        /// </summary>
        public const int AUTHORIZATION_MANAGER_ERROR = 2101;

        /// <summary>
        /// Error code for exception of Invalid credential
        /// </summary>
        public const int AUTHORIZATION_MANAGER_INVALID_CREDENTIAL = 2102;

        #endregion Authorization Manager

        #region Projects Module From 2201 - 2299

        /// <summary>
        /// Error code for exception thrown in Data Access layer.
        /// </summary>
        public const int RAVE_HR_PROJECTS_DATA_ACCESS_LAYER = 2201;

        /// <summary>
        /// Error code for exception thrown in Business layer.
        /// </summary>
        public const int RAVE_HR_PROJECTS_BUSNIESS_LAYER = 2202;

        /// <summary>
        /// Error code for exception thrown in Presentation layer.
        /// </summary>
        public const int RAVE_HR_PROJECTS_PRESENTATION_LAYER = 2203;


        #endregion Projects Module

        #region Resource Plan Module From 2301 - 2399

        /// <summary>
        /// Error code for exception thrown in Data Access layer.
        /// </summary>
        public const int RAVE_HR_RP_DATA_ACCESS_LAYER = 2301;

        /// <summary>
        /// Error code for exception thrown in Business layer.
        /// </summary>
        public const int RAVE_HR_RP_BUSNIESS_LAYER = 2302;

        /// <summary>
        /// Error code for exception thrown in Presentation layer.
        /// </summary>
        public const int RAVE_HR_RP_PRESENTATION_LAYER = 2303;


        #endregion Resource Plan Module

        #region Contract Module From 2401-2499

        /// <summary>
        /// Error code for exception thrown in Data Access layer.
        /// </summary>
        public const int RAVE_HR_CONTRACT_DATA_ACCESS_LAYER = 2401;

        /// <summary>
        /// Error code for exception thrown in Business layer.
        /// </summary>
        public const int RAVE_HR_CONTRACT_BUSNIESS_LAYER = 2402;

        /// <summary>
        /// Error code for exception thrown in Presentation layer.
        /// </summary>
        public const int RAVE_HR_CONTRACT_PRESENTATION_LAYER = 2403;

        #endregion Contract Module From 2401-2499

        #region MRF Module From 2501-2599

        /// <summary>
        /// Error code for exception thrown in Data Access layer.
        /// </summary>
        public const int RAVE_HR_MRF_DATA_ACCESS_LAYER = 2501;

        /// <summary>
        /// Error code for exception thrown in Business layer.
        /// </summary>
        public const int RAVE_HR_MRF_BUSNIESS_LAYER = 2502;

        /// <summary>
        /// Error code for exception thrown in Presentation layer.
        /// </summary>
        public const int RAVE_HR_MRF_PRESENTATION_LAYER = 2503;

        #endregion MRF Module

        #region Employee Module From 2601-2699

        /// <summary>
        /// Error code for exception thrown in Data Access layer.
        /// </summary>
        public const int RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER = 2601;

        /// <summary>
        /// Error code for exception thrown in Business layer.
        /// </summary>
        public const int RAVE_HR_EMPLOYEE_BUSNIESS_LAYER = 2602;

        /// <summary>
        /// Error code for exception thrown in Presentation layer.
        /// </summary>
        public const int RAVE_HR_EMPLOYEE_PRESENTATION_LAYER = 2603;

        #endregion Employee Module

        #region Recruitment Module From 2701-2799

        /// <summary>
        /// Error code for exception thrown in Data Access layer.
        /// </summary>
        public const int RAVE_HR_RECRUITMENT_DATA_ACCESS_LAYER = 2701;

        /// <summary>
        /// Error code for exception thrown in Business layer.
        /// </summary>
        public const int RAVE_HR_RECRUITMENT_BUSNIESS_LAYER = 2702;

        /// <summary>
        /// Error code for exception thrown in Presentation layer.
        /// </summary>
        public const int RAVE_HR_RECRUITMENT_PRESENTATION_LAYER = 2703;

        #endregion Recruitment Module From 2701-2799

        #region Seat Allocation Module From 2800-2802

        /// <summary>
        /// Error code for exception thrown in Data Access layer.
        /// </summary>
        public const int RAVE_HR_SEATALLOCATION_DATA_ACCESS_LAYER = 2800;

        /// <summary>
        /// Error code for exception thrown in Business layer.
        /// </summary>
        public const int RAVE_HR_SEATALLOCATION_BUSNIESS_LAYER = 2801;

        /// <summary>
        /// Error code for exception thrown in Presentation layer.
        /// </summary>
        public const int RAVE_HR_SEATALLOCATION_PRESENTATION_LAYER = 2802;

        #endregion

        #region Constant For Master DL. From 2803

        /// <summary>
        /// Error code for exception thrown in Data Acess layer.
        /// </summary>
        public const int RAVE_HR_MASTER_DATA_ACCESS_LAYER = 2803;

        /// <summary>
        /// Error code for exception thrown in Business Acess layer.
        /// </summary>
        public const int RAVE_HR_MASTER_BUSINESS_ACCESS_LAYER = 2804;

        #endregion

        //Ishwar Patil : Trainging Module 11/04/2014 : Starts
        #region Training Mdodule From 2805 - 2806

        /// <summary>
        /// Error code for exception thrown in Data Acess layer.
        /// </summary>
        public const int TRAINING_DATA_ACCESS_LAYER = 2807;

        /// <summary>
        /// Error code for exception thrown in Business Acess layer.
        /// </summary>
        public const int TRAINING_BUSINESS_ACCESS_LAYER = 2806;

        /// <summary>
        /// Error code for exception thrown in Presentation layer.
        /// </summary>
        public const int TRAINING_PRESENTATION_LAYER = 2805;

        #endregion
        //Ishwar Patil : Trainging Module 11/04/2014 : Starts

        //Ishwar Patil : NISRMS 15/12/2014 : Starts
        #region NISRMS From 2808 - 2811

        public const int RAVE_EMP_SKILL_SEARCH_DATA_ACCESS_LAYER = 2808;
        public const int RAVE_EMP_SKILL_SEARCH_BUSINESS_ACCESS_LAYER = 2809;
        public const int RAVE_EMP_SKILL_SEARCH_PRESENTATION_LAYER = 2810;

        #endregion
        //Ishwar Patil : NISRMS 15/12/2014 : End


        #region RMS UILayer
        /// <summary>
        /// Error code for exception thrown in controller layer.
        /// </summary>
        public const int TRAINING_CONTROLLER_LAYER = 2709;

        /// <summary>
        /// Error code for exception thrown in controller layer.
        /// </summary>
        public const int ERROR_CONTROLLER_LAYER = 2708;

        /// <summary>
        /// Error code for exception thrown in controller layer.
        /// </summary>
        public const int ERROR_UI_LAYER = 2707;
        #endregion

        


        #endregion EventID contants

    }
}
