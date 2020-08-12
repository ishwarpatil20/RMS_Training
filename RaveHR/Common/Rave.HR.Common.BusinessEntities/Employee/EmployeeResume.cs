//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           EmployeeResume.cs       
//  Author:         Rahul.Parwekar
//  Date written:   19/10/2010 
//  Description:    This class defines business entities related to Employees Resume details
//                  These businesss entities are used for Employee related pages
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  19/10/2010           Rahul.Parwekar   n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    /// <summary>
    /// Defines business entities related to employees Resume details
    /// </summary>
    [Serializable]
    public class EmployeeResume
    {
        /// <summary>
        /// Gets or sets the Resume id.
        /// </summary>
        /// <value>The project id.</value>
        public int ResumeId { get; set; }

        /// <summary>
        /// Gets or sets the EMP id.
        /// </summary>
        /// <value>The EMP id.</value>
        public int EMPId { get; set; }

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        /// <value>The name of the project.</value>
        public string DocumentName{ get; set; }

        /// <summary>
        /// Gets or sets the Modify Date.
        /// </summary>
        /// <value>To set the date at which Resume is modify.</value>
        public DateTime ModifyDate{ get; set; }

        /// <summary>
        /// Gets or sets the name of the person who has modify the Resume.
        /// </summary>
        /// <value>The name of the person who has modify the Resume.</value>
        public string ModifyBy{ get; set; }

        /// <summary>
        /// Gets or sets the name of the person who has modify the Resume.
        /// </summary>
        /// <value>The name of the person who has modify the Resume.</value>
        public string ResumeCount{ get; set; }

        /// <summary>
        /// Gets or sets the File Extension.
        /// </summary>
        /// <value>Gets or sets the File Extension..</value>
        public string FileExtension { get; set; }

    }
}
