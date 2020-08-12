//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           SkillsDetails.cs       
//  Author:         shrinivas.dalavi
//  Date written:   8/12/2009 7:00:47 PM
//  Description:    This class defines properties related to Employees Skills details
//                  These businesss entities are used for Employee related pages
//
//  Amendments
//  Date                    Who                 Ref     Description
//  ----                    -----------         ---     -----------
//  08/12/2009 07:00:47 PM  shrinivas.dalavi    n/a     Created 
//  04/09/2009 02:27:45 PM  vineet.kulkarni     n/a     Added [Serializable] for class   
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    /// <summary>
    /// Summary description for SkillsDetails
    /// </summary>
    [Serializable]
    public class SkillsDetails
    {
        #region Properties

        /// <summary>
        /// Gets or sets the skills ID.
        /// </summary>
        /// <value>The skills ID.</value>
        public int SkillsId { get; set; }

        /// <summary>
        /// Gets or sets the EMPID.
        /// </summary>
        /// <value>The EMPID.</value>
        public int EMPId { get; set; }

        /// <summary>
        /// Gets or sets the Skill.
        /// </summary>
        /// <value>The Skill.</value>
        public int Skill { get; set; }

        /// <summary>
        /// Gets or sets the experience.
        /// </summary>
        /// <value>The experience.</value>
        public string Experience { get; set; }

        /// <summary>
        /// Gets or sets the proficiency.
        /// </summary>
        /// <value>The proficiency.</value>
        public int Proficiency { get; set; }

        /// <summary>
        /// Gets or sets the last used date.
        /// </summary>
        /// <value>The last used date.</value>
        public DateTime LastUsedDate { get; set; }

        /// <summary>
        /// Gets or sets the skills.
        /// </summary>
        /// <value>The skills.</value>
        public List<SkillsDetails> Skills { get; set; }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The mode.</value>
        public int Mode { get; set; }

        /// <summary>
        /// Gets or sets the SkillName.
        /// </summary>
        /// <value>The SkillName.</value>
        public string SkillName { get; set; }

        /// <summary>
        /// Gets or sets the ProficiencyName.
        /// </summary>
        /// <value>The ProficiencyName.</value>
        public string ProficiencyLevel { get; set; }

        /// <summary>
        /// Gets or sets the SkillType.
        /// </summary>
        /// <value>The SkillType.</value>
        public int SkillType { get; set; }


        /// <summary>
        /// Gets or sets the SkillVersion.
        /// </summary>
        /// <value>The SkillVersion.</value>
        public string SkillVersion { get; set; }


        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>The month.</value>
        public int Month { get; set; }

        
        /// <summary>
        /// Gets or sets the SkillCategory.
        /// </summary>

        public string SkillCategory { get; set; }


        /// <summary>
        /// Gets or sets the Year.
        /// </summary>
        /// <value>The Year.</value>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the LastUsed.
        /// </summary>
        /// <value>The LastUsed.</value>
        public int LastUsed { get; set; }

        //Siddhesh Arekar Domain Details 10082015 Start
        /// <summary>
        /// Gets or sets the Domain of Employee.
        /// </summary>
        /// <value>The EmployeeDomainName.</value>
        public string EmployeeDomain { get; set; }
        //Siddhesh Arekar Domain Details 10082015 End
        #endregion
    }
}