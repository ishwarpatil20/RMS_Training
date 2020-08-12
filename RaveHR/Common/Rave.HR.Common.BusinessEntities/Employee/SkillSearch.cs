//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           SkillSearch.cs       
//  Author:         umesh.pandit
//  Date written:   3/11/2014
//  Description:    This class defines properties related to Skills Search Report
//                  These businesss entities are used for Employee related pages
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    /// <summary>
    /// Summary description for SkillSearch
    /// </summary>
    [Serializable]
    public class SkillSearch
    {
        #region Properties

        /// <summary>
        /// Gets or sets the skills ID.
        /// </summary>
        /// <value>The skills ID.</value>
        public int SkillsId { get; set; }

        /// <summary>
        /// Gets or sets the SkillName.
        /// </summary>
        /// <value>The SkillName.</value>
        public string SkillName { get; set; }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The mode.</value>
        public int SearchMode { get; set; }

        /// <summary>
        /// Gets or sets the MandatorySkills.
        /// </summary>
        /// <value></value>
        public string MandatorySkills { get; set; }

        /// <summary>
        /// Gets or sets the OptionalSkills.
        /// </summary>
        /// <value></value>
        public string OptionalSkills { get; set; }

        /// <summary>
        /// Gets or sets the EmployeeName.
        /// </summary>
        /// <value></value>
        public string EmployeeName { get; set; }

        /// <summary>
        /// Gets or sets the Designation.
        /// </summary>
        /// <value></value>
        public string Designation { get; set; }

        /// <summary>
        /// Gets or sets the Department.
        /// </summary>
        /// <value></value>
        public string Department { get; set; }

        /// <summary>
        /// Gets or sets the ProjectsAllocated.
        /// </summary>
        /// <value></value>
        public string ProjectsAllocated { get; set; }

        /// <summary>
        /// Gets or sets the PrimarySkill.
        /// </summary>
        /// <value></value>
        public string PrimarySkill { get; set; }

        /// <summary>
        /// Gets or sets the SkillVersion.
        /// </summary>
        /// <value></value>
        public string SkillVersion { get; set; }

        /// <summary>
        /// Gets or sets the ExpInMonths.
        /// </summary>
        /// <value></value>
        public int ExpInMonths { get; set; }

        /// <summary>
        /// Gets or sets the Proficiency.
        /// </summary>
        /// <value></value>
        public string Proficiency { get; set; }

        /// <summary>
        /// Gets or sets the LastUsed.
        /// </summary>
        /// <value></value>
        public int LastUsed { get; set; }

        #endregion
    }
}