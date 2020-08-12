//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           PassportDetails.cs       
//  Author:         Shrinivas.Dalavi
//  Date written:   8/12/2009 3:09:27 PM
//  Description:    This class defines properties related to Employees Passport details
//                  These businesss entities are used for Employee related pages
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  8/12/2009 5:04:27 PM  Shrinivas.Dalavi    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    [Serializable]
    public class VisaDetails
    {
        #region Properties

        /// <summary>
        /// Gets or sets the visa ID.
        /// </summary>
        /// <value>The visa ID.</value>
        public int VisaId { get; set; }

        /// <summary>
        /// Gets or sets the EMPID.
        /// </summary>
        /// <value>The EMPID.</value>
        public int EMPId { get; set; }

        /// <summary>
        /// Gets or sets the country ID.
        /// </summary>
        /// <value>The country ID.</value>
        public string CountryName { get; set; }

        /// <summary>
        /// Gets or sets the type of the visa.
        /// </summary>
        /// <value>The type of the visa.</value>
        public string VisaType { get; set; }

        /// <summary>
        /// Gets or sets the expiry date.
        /// </summary>
        /// <value>The expiry date.</value>
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the visas.
        /// </summary>
        /// <value>The visas.</value>
        public List<VisaDetails> Visas { get; set; }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The mode.</value>
        public int Mode { get; set; }

        #endregion

    }
}