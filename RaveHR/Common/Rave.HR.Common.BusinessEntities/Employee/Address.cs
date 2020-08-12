//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           Address.cs       
//  Author:         sudip.guha
//  Date written:   16/11/2009 5:15:39 PM
//  Description:    This class defines business entities related to Employees Address details
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
    /// Defines business entities related to employees address 
    /// </summary>
    [Serializable]
    public class Address
    {
        #region Properties

        /// <summary>
        /// Gets or sets the EmployeeAddressId .
        /// </summary>
        /// <value>The EmployeeAddressId.</value>
        public int EmployeeAddressId { get; set; }

        /// <summary>
        /// Gets or sets the EMPId.
        /// </summary>
        /// <value>The EMPId.</value>
        public int EMPId { get; set; }

        /// <summary>
        /// Gets or sets the Address of the Address.
        /// </summary>
        /// <value>The Addres of the Address.</value>
        public string Addres { get; set; }

        /// <summary>
        /// Gets or sets the FlatNo of the Address.
        /// </summary>
        /// <value>The FlatNo of the Address.</value>
        public string FlatNo { get; set; }

        /// <summary>
        /// Gets or sets the BuildingName of the Address.
        /// </summary>
        /// <value>The BuildingName of the Address.</value>
        public string BuildingName { get; set; }

        /// <summary>
        /// Gets or sets the Street of the Address.
        /// </summary>
        /// <value>The Street of Address.</value>
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the Landmark of the Address.
        /// </summary>
        /// <value>The Landmark of the Address.</value>
        public string Landmark { get; set; }


        /// <summary>
        /// Gets or sets the City of Address.
        /// </summary>
        /// <value>The City for Address.</value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the State of Address.
        /// </summary>
        /// <value>The State for Address.</value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the Country of Address.
        /// </summary>
        /// <value>The Country for Address.</value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the Pincode of Address.
        /// </summary>
        /// <value>The Pincode for Address.</value>
        public string Pincode { get; set; }

        /// <summary>
        /// Gets or sets the AddressType of Address.
        /// </summary>
        /// <value>The AddressType for Address.</value>
        public int AddressType { get; set; }

        /// <summary>
        /// Gets or sets the CreatedById of Address.
        /// </summary>
        /// <value>The CreatedById for Address.</value>
        public string CreatedById { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDate of Address.
        /// </summary>
        /// <value>The CreatedDate for Address.</value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the IsActive of Address.
        /// </summary>
        /// <value>The IsActive.</value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the CreatedById of Address.
        /// </summary>
        /// <value>The CreatedById for Address.</value>
        public string LastModifiedById { get; set; }

        /// <summary>
        /// Gets or sets the CreatedDate of Address.
        /// </summary>
        /// <value>The CreatedDate for Address.</value>
        public DateTime LastModifiedDate { get; set; }

        #endregion Properties
    }
}
