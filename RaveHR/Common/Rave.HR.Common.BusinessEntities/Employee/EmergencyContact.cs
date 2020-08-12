//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           EmergencyContact.cs       
//  Author:         sudip.guha
//  Date written:   16/11/2009 5:15:39 PM
//  Description:    This class defines business entities related to Employees EmergencyContact details
//                  These businesss entities are used for Employee related pages
//
//  Amendments
//  Date                    Who             Ref     Description
//  ----                    -----------     ---     -----------
//  16/11/2009 5:15:39 PM   sudip.guha      n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    /// <summary>
    /// Defines business entities related to employees emergencycontact 
    /// </summary>
    [Serializable]
    public class EmergencyContact
    {
        #region Properties

        /// <summary>
        /// Gets or sets the EmergencyContactId .
        /// </summary>
        /// <value>The certification id.</value>
        public int EmergencyContactId { get; set; }

        /// <summary>
        /// Gets or sets the EMPId.
        /// </summary>
        /// <value>The EMPId.</value>
        public int EMPId { get; set; }

        /// <summary>
        /// Gets or sets the ContactName of the EmergencyContact.
        /// </summary>
        /// <value>The ContactName of the EmergencyContact.</value>
        public string ContactName { get; set; }

        /// <summary>
        /// Gets or sets the ContactNumber of the EmergencyContact.
        /// </summary>
        /// <value>The ContactNumber of EmergencyContact.</value>
        public string ContactNumber { get; set; }

        /// <summary>
        /// Gets or sets the Relation of EmergencyContact.
        /// </summary>
        /// <value>The Relation for EmergencyContact.</value>
        public string Relation { get; set; }

        /// <summary>
        /// Gets or sets the IsActive of EmergencyContact.
        /// </summary>
        /// <value>The IsActive.</value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The mode.</value>
        public int Mode { get; set; }

        /// <summary>
        /// Gets or sets the type of the relation.
        /// </summary>
        /// <value>The type of the relation.</value>
        public int RelationType { get; set; }

        #endregion Properties
    }
}
