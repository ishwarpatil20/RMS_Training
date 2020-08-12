//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           Domain.cs       
//  Author:         vineet.kulkarni
//  Date written:   4/29/2009 07:12:50 PM
//  Description:    This class defines properties related to Sub Domain
//                  These business entities are used for ProjectSearch.aspx, AddProject.aspx etc 
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  4/29/2009 07:12:50 PM  vineet.kulkarni    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    /// <summary>
    /// This class is used for to set and get Sub Domain based upon Domain selection 
    /// </summary>
    public class SubDomain
    {
        #region Private Field Members

        /// <summary>
        /// Create _subDomainId variable with int type
        /// </summary>
        private int _subDomainId;

        /// <summary>
        /// Create _subDomainName variable with string type
        /// </summary>
        private string _subDomainName;

        /// <summary>
        /// Create _domainId variable with int type
        /// </summary>
        private int _domainId;

        #endregion Private Field Members

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SubDomain()
        { 
        }

        /// <summary>
        /// Default Parameterized Constructor
        /// </summary>
        /// <param name="subDomainId">int</param>
        /// <param name="domainId">int</param>
        /// <param name="name">string</param>
        public SubDomain(int subDomainId, int domainId, string name)
        {
            _subDomainId = subDomainId;
            _domainId = domainId;
            _subDomainName = name;
        }

        #region Properties

        /// <summary>
        /// Define SubDomainID property
        /// </summary>
        public int SubDomainId
        {
            get
            {
                return _subDomainId;
            }
            set
            {
                _subDomainId = value;
            }
        }

        /// <summary>
        /// Define SubDomainName property
        /// </summary>
        public string SubDomainName
        {
            get
            {
                return _subDomainName;
            }
            set
            {
                _subDomainName = value;
            }
        }

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

        #endregion Properties
    }
}
