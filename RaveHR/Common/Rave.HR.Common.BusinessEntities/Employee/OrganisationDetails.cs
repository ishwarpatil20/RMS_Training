//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           OrganisationDetails.cs       
//  Author:         shrinivas.dalavi
//  Date written:   8/12/2009 6:50:43 PM
//  Description:    This class defines properties related to Employees Oraganisation details
//                  These businesss entities are used for Employee related pages
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  8/12/2009 6:50:43 PM  shrinivas.dalavi    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    /// <summary>
    /// Summary description for OrganisationDetails
    /// </summary>
    [Serializable]
    public class OrganisationDetails
    {

        #region Properties

        /// <summary>
        /// Gets or sets the organisation ID.
        /// </summary>
        /// <value>The organisation ID.</value>
        public int OrganisationId { get; set; }

        /// <summary>
        /// Gets or sets the EMPID.
        /// </summary>
        /// <value>The EMPID.</value>
        public int EMPId { get; set; }

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        /// <value>The name of the company.</value>
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the designation.
        /// </summary>
        /// <value>The designation.</value>
        public string Designation { get; set; }

        /// <summary>
        /// Gets or sets the experience.
        /// </summary>
        /// <value>The experience.</value>
        public string Experience { get; set; }

        /// <summary>
        /// Gets or sets the type of the experience.
        /// </summary>
        /// <value>The type of the experience.</value>
        public int ExperienceType { get; set; }

        /// <summary>
        /// Gets or sets the MonthSince of the experience.
        /// </summary>
        /// <value>The MonthSince of the experience.</value>
        public int MonthSince { get; set; }

        /// <summary>
        /// Gets or sets the YearSince of the experience.
        /// </summary>
        /// <value>The YearSince of the experience.</value>
        public int YearSince { get; set; }

        /// <summary>
        /// Gets or sets the MonthTill of the experience.
        /// </summary>
        /// <value>The MonthTill of the experience.</value>
        public int MonthTill { get; set; }

        /// <summary>
        /// Gets or sets the YearTill of the experience.
        /// </summary>
        /// <value>The YearTill of the experience.</value>
        public int YearTill { get; set; }

        /// <summary>
        /// Gets or sets the organisations.
        /// </summary>
        /// <value>The organisations.</value>
        public List<OrganisationDetails> Organisations { get; set; }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The mode.</value>
        public int Mode { get; set; }

        /// <summary>
        /// Gets or sets the WorkingSince.
        /// </summary>
        /// <value>The WorkingSince.</value>
        public string WorkingSince { get; set; }

        /// <summary>
        /// Gets or sets the WorkingTill.
        /// </summary>
        /// <value>The WorkingTill.</value>
        public string WorkingTill { get; set; }

        /// <summary>
        /// Gets or sets the MonthSince of the experience.
        /// </summary>
        /// <value>The MonthSince of the experience.</value>
        public string MonthSinceName { get; set; }

        /// <summary>
        /// Gets or sets the MonthTillName.
        /// </summary>
        /// <value>The MonthTillName.</value>
        public string MonthTillName { get; set; }

        /// <summary>
        /// Gets or sets the experience year.
        /// </summary>
        /// <value>The experience year.</value>
        public int ExperienceYear { get; set; }

        /// <summary>
        /// Gets or sets the experience year.
        /// </summary>
        /// <value>The experience year.</value>
        public int ExperienceMonth { get; set; }

        /// <summary>
        /// Gets or sets the experience non year.
        /// </summary>
        /// <value>The experience non year.</value>
        public int ExperienceNonYear { get; set; }

        /// <summary>
        /// Gets or sets the experience non month.
        /// </summary>
        /// <value>The experience non month.</value>
        public int ExperienceNonMonth { get; set; }

        #endregion

    }
}