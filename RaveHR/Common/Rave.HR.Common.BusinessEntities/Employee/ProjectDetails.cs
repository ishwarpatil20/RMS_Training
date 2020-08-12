//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           EmpProjectDetails.cs       
//  Author:         vineet.kulkarni
//  Date written:   8/12/2009 5:35:14 PM
//  Description:    This class defines business entities related to Employees Project details
//                  These businesss entities are used for Employee related pages
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  8/12/2009 5:35:14 PM  vineet.kulkarni    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    /// <summary>
    /// Defines business entities related to employees project details
    /// </summary>
    [Serializable]
    public class ProjectDetails
    {
        #region Properties

        /// <summary>
        /// Gets or sets the project id.
        /// </summary>
        /// <value>The project id.</value>
        public int ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the EMP id.
        /// </summary>
        /// <value>The EMP id.</value>
        public int EMPId { get; set; }

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        /// <value>The name of the project.</value>
        public string ProjectName { get; set; }

        /// <summary>
        /// Gets or sets the organisation.
        /// </summary>
        /// <value>The organisation.</value>
        public string Organisation { get; set; }

        /// <summary>
        /// Gets or sets the years.
        /// </summary>
        /// <value>The years.</value>
        public string Years { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>The role.</value>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether employee is onsite.
        /// </summary>
        /// <value><c>true</c> if onsite; otherwise, <c>false</c>.</value>
        public bool Onsite { get; set; }

        /// <summary>
        /// Gets or sets the duration of the onsite.
        /// </summary>
        /// <value>The duration of the onsite.</value>
        public string OnsiteDuration { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the resposibility.
        /// </summary>
        /// <value>The resposibility.</value>
        public string Resposibility { get; set; }

        /// <summary>
        /// Gets or sets the projects.
        /// </summary>
        /// <value>The projects.</value>
        public List<ProjectDetails> Projects { get; set; }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The mode.</value>
        public int Mode { get; set; }

        /// <summary>
        /// Gets or sets the ProjectLocation.
        /// </summary>
        /// <value>The ProjectLocation.</value>
        public string ProjectLocation { get; set; }

        /// <summary>
        /// Gets or sets the LocationId.
        /// </summary>
        /// <value>The ProjectId.</value>
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        /// <value>The name of the client.</value>
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the size of the project.
        /// </summary>
        /// <value>The size of the project.</value>
        public int ProjectSize { get; set; }

        /// <summary>
        /// Gets or sets the project done.
        /// </summary>
        /// <value>The project done.</value>
        public int ProjectDone { get; set; }

        /// <summary>
        /// Gets or sets the name of the project done.
        /// </summary>
        /// <value>The name of the project done.</value>
        public string ProjectDoneName { get; set; }


        /// <summary>
        /// Gets or sets the name of the project RankOrder done.
        /// </summary>
        public int RankOrder { get; set; }

        #endregion Properties
    }
}