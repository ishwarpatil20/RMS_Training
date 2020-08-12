using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMS.Common.BusinessEntities
{
    
    public class ParameterCriteria
    {

        /// <summary>
        /// Gets or sets the UserMailId.
        /// </summary>
        /// <value>The User MailId.</value>
        public string EMailID { get; set; }

        /// <summary>
        /// Gets or sets the RoleCOO.
        /// </summary>
        /// <value>Role COO.</value>
        public string RoleCOO { get; set; }

        /// <summary>
        /// Gets or sets the RoleCEO.
        /// </summary>
        /// <value>Role CEO.</value>
        public string RoleCEO { get; set; }

        /// <summary>
        /// Gets or sets the RoleRPM.
        /// </summary>
        /// <value>Role RPM.</value>
        public string RoleRPM { get; set; }

        /// <summary>
        /// Gets or sets the RoleCFM.
        /// </summary>
        /// <value>Role CFM.</value>
        public string RoleCFM { get; set; }

        /// <summary>
        /// Gets or sets the RoleFM.
        /// </summary>
        /// <value>Role FM.</value>
        public string RoleFM { get; set; }

        /// <summary>
        /// Gets or sets the RolePreSales.
        /// </summary>
        /// <value>Role PreSales.</value>
        public string RolePreSales { get; set; }

        /// <summary>
        /// Gets or sets the RolePM.
        /// </summary>
        /// <value>Role PM.</value>
        public string RolePM { get; set; }

        /// <summary>
        /// Gets or sets the RoleAPM.
        /// </summary>
        /// <value>Role APM.</value>
        public string RoleAPM { get; set; }

        /// <summary>
        /// Gets or sets the RoleGPM.
        /// </summary>
        /// <value>Role GPM.</value>
        public string RoleGPM { get; set; }

        /// <summary>
        /// Gets or sets the RoleHR.
        /// </summary>
        /// <value>Role HR.</value>
        public string RoleHR { get; set; }

        /// <summary>
        /// Gets or sets the RoleRecruitment.
        /// </summary>
        /// <value>Role Recruitment.</value>
        public string RoleRecruitment { get; set; }

        /// <summary>
        /// Gets or sets the SortExpressionAndDirection.
        /// </summary>
        /// <value>SortExpressionAndDirection.</value>
        public string SortExpressionAndDirection { get; set; }

        /// <summary>
        /// Gets or sets the FunctionalManagerID.
        /// </summary>
        /// <value>FunctionalManagerID.</value>
        public string FunctionalManagerID { get; set; }

        /// <summary>
        /// Gets or sets the PageNumber.
        /// </summary>
        /// <value>PageNumber.</value>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the PageSize.
        /// </summary>
        /// <value>PageSize.</value>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the Role.
        /// </summary>
        /// <value>Role.</value>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets the RoleTesting.
        /// </summary>
        /// <value>Role Testing.</value>
        public string RoleTesting { get; set; }

        /// <summary>
        /// Gets or sets the RoleQuality.
        /// </summary>
        /// <value>Role Quality.</value>
        public string RoleQuality { get; set; }

        /// <summary>
        /// Gets or sets the RoleMarketing.
        /// </summary>
        /// <value>Role Marketing.</value>
        public string RoleMarketing { get; set; }

        /// <summary>
        /// Gets or sets the RoleConsultant.
        /// </summary>
        /// <value>Role RaveConsultant.</value>
        public string RoleRaveConsultant { get; set; }

        ////25988-Ambar-Start

        /// <summary>
        /// Gets or sets the Project Status.
        /// </summary>
        /// <value>Project Status</value>
        public string ProjectStatus { get; set; }

        ////25988-Ambar-End

        //Ishwar Patil 29092014 For NIS : Start
        /// <summary>
        /// Gets or sets the MRF Type(RMS/NIS).
        /// </summary>
        /// <value>Project Status</value>
        public string IsRMSMRF { get; set; }
        //Ishwar Patil 29092014 For NIS : End

    }
}
