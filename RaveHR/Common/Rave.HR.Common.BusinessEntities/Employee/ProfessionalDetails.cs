//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           EmpProfDetails.cs       
//  Author:         vineet.kulkarni
//  Date written:   8/12/2009 5:03:28 PM
//  Description:    This class defines business entities related to Employees Professional details
//                  These businesss entities are used for Employee related pages
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  8/12/2009 5:03:28 PM  vineet.kulkarni    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    /// <summary>
    /// Defines business entities related to employees professional details
    /// </summary>
    [Serializable]
    public class ProfessionalDetails
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Employee Professional id.
        /// </summary>
        /// <value>The Professional id.</value>
        public int ProfessionalId { get; set; }

        /// <summary>
        /// Gets or sets the EMPId.
        /// </summary>
        /// <value>The EMPId.</value>
        public int EMPId { get; set; }

        /// <summary>
        /// Gets or sets the name of the course.
        /// </summary>
        /// <value>The name of the course.</value>
        public string CourseName { get; set; }

        /// <summary>
        /// Gets or sets the name of the institution.
        /// </summary>
        /// <value>The name of the institution.</value>
        public string InstitutionName { get; set; }

        /// <summary>
        /// Gets or sets the passing year.
        /// </summary>
        /// <value>The passing year.</value>
        public string PassingYear { get; set; }

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>The score.</value>
        public string Score { get; set; }

        /// <summary>
        /// Gets or sets the outof.
        /// </summary>
        /// <value>The outof.</value>
        public string Outof { get; set; }

        /// <summary>
        /// Gets or sets the professionals.
        /// </summary>
        /// <value>The professionals.</value>
        public List<ProfessionalDetails> Professionals { get; set; }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The mode.</value>
        public int Mode { get; set; }

        #endregion Properties

    }
}