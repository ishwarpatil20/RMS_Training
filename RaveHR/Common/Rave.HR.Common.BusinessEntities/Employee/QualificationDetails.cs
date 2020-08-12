//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           QualificationDetails.cs       
//  Author:         shrinivas.dalavi
//  Date written:   8/12/2009 5:46:43 PM
//  Description:    This class defines properties related to Employees Qualification details
//                  These businesss entities are used for Employee related pages
//
//  Amendments
//  Date                    Who                 Ref     Description
//  ----                    -----------         ---     -----------
//  08/12/2009 05:46:43 PM  shrinivas.dalavi    n/a     Created 
//  03/09/2009 03:00:00 PM  vineet.kulkarni     n/a     Added [Serializable] for class
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    /// <summary>
    /// Defines business entities related to employees qualification details
    /// </summary>
    [Serializable]
    public class QualificationDetails
    {
        #region Properties

        /// <summary>
        /// Gets or sets the qualification ID.
        /// </summary>
        /// <value>The qualification ID.</value>
        public int QualificationId { get; set; }

        /// <summary>
        /// Gets or sets the EMPID.
        /// </summary>
        /// <value>The EMPID.</value>
        public int EMPId { get; set; }

        /// <summary>
        /// Gets or sets the qualification.
        /// </summary>
        /// <value>The qualification.</value>
        public int Qualification { get; set; }

        /// <summary>
        /// Gets or sets the name of the university.
        /// </summary>
        /// <value>The name of the university.</value>
        public string UniversityName { get; set; }

        /// <summary>
        /// Gets or sets the name of the institute.
        /// </summary>
        /// <value>The name of the institute.</value>
        public string InstituteName { get; set; }

        /// <summary>
        /// Gets or sets the passing year.
        /// </summary>
        /// <value>The passing year.</value>
        public string PassingYear { get; set; }

        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        /// <value>The percentage.</value>
        public float Percentage { get; set; }

        /// <summary>
        /// Gets or sets the  qualifications.
        /// </summary>
        /// <value>The qualifications.</value>
        public List<QualificationDetails> Qualifications { get; set; }

        public int Mode { get; set; }

        /// <summary>
        /// Gets or sets the GPA.
        /// </summary>
        /// <value>The GPA.</value>
        public float GPA { get; set; }

        /// <summary>
        /// Gets or sets the Outof.
        /// </summary>
        /// <value>The Outof.</value>
        public float Outof { get; set; }

        /// <summary>
        /// Gets or sets the QualificatioName.
        /// </summary>
        /// <value>The QualificatioName.</value>
        public string QualificationName { get; set; }

        /// <summary>
        /// Gets or sets the stream.
        /// </summary>
        /// <value>The stream.</value>
        public string Stream { get; set; }

        #endregion
    }
}