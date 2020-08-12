//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           EmpCertiDetails.cs       
//  Author:         vineet.kulkarni
//  Date written:   8/12/2009 5:15:39 PM
//  Description:    This class defines business entities related to Employees Certification details
//                  These businesss entities are used for Employee related pages
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  8/12/2009 5:15:39 PM  vineet.kulkarni    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    /// <summary>
    /// Defines business entities related to employees certification details
    /// </summary>
    [Serializable]
    public class CertificationDetails
    {
        #region Properties

        /// <summary>
        /// Gets or sets the certification id.
        /// </summary>
        /// <value>The certification id.</value>
        public int CertificationId { get; set; }

        /// <summary>
        /// Gets or sets the EMPId.
        /// </summary>
        /// <value>The EMPId.</value>
        public int EMPId { get; set; }

        /// <summary>
        /// Gets or sets the name of the certification.
        /// </summary>
        /// <value>The name of the certification.</value>
        public string CertificationName { get; set; }

        /// <summary>
        /// Gets or sets the certificate date.
        /// </summary>
        /// <value>The certificate date.</value>
        public DateTime CertificateDate { get; set; }

        /// <summary>
        /// Gets or sets the certificate valid date.
        /// </summary>
        /// <value>The certificate valid date.</value>
        public DateTime CertificateValidDate { get; set; }

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        /// <value>The score.</value>
        public float Score { get; set; }

        /// <summary>
        /// Gets or sets the out of.
        /// </summary>
        /// <value>The out of.</value>
        public float OutOf { get; set; }


        /// <summary>
        /// Gets or sets the certifications.
        /// </summary>
        /// <value>The certifications.</value>
        public List<CertificationDetails> Certifications { get; set; }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The mode.</value>
        public int Mode { get; set; }

        #endregion Properties
    }
}