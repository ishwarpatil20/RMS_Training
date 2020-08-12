//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           Domain.cs       
//  Author:         vineet.kulkarni
//  Date written:   4/29/2009 06:48:20 PM
//  Description:    This class defines properties related to Domain of Sub Domain 
//                  These business entities are used for ViewProject.aspx etc 
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  4/29/2009 06:48:20 PM  vineet.kulkarni    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{   
    /// <summary>
    /// This class is used to get and set Domain
    /// </summary>
    public class Domain
    {
        #region Private Field Members

        /// <summary>
        /// Create _domainId variable with integer type
        /// </summary>
        private int _domainId;

        /// <summary>
        /// Create _domainName variable with string type
        /// </summary>
        private string _domainName;

        /// <summary>
        /// create _lstSubDomain variable with List type
        /// </summary>
        private List<SubDomain> _lstSubDomain = new List<SubDomain>();

        #endregion Private Field Members

        #region Properties

        /// <summary>
        /// Define DomainId property
        /// </summary>
        public int DomainId
        {
            get
            {
                return _domainId;
            }
            set
            {
                _domainId = value;
            }

        }

        /// <summary>
        /// Define DomainName property
        /// </summary>
        public string DomainName
        {
            get
            {
                return _domainName;
            }
            set
            {
                _domainName = value;
            }
        }

        /// <summary>
        /// Define lstSubDomains property
        /// </summary>
        public List<SubDomain> lstSubDomain
        {
            get
            {
                return _lstSubDomain;
            }
            set
            {
                _lstSubDomain = value;
            }
        }

        #endregion Properties
    }
}
