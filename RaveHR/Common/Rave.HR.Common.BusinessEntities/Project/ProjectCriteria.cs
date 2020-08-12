//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           ProjectCriteria.cs       
//  Author:         vineet.kulkarni
//  Date written:   12/08/2009 10:50:48 AM
//  Description:    This class defines properties related to Projects Criteria 
//                  These business entities are used for ViewProject.aspx etc 
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  12/08/2009 10:50:48 AM  vineet.kulkarni    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class ProjectCriteria
    {
        #region Private Field Members

        /// <summary>
        /// Create _userMailId variable with string type
        /// </summary>
        private string _userMailId;

        /// <summary>
        /// Create _roleCOO variable with string type
        /// </summary>
        private string _roleCOO;

        /// <summary>
        /// Create _rolePresales variable with string type
        /// </summary>
        private string _rolePresales;

        /// <summary>
        /// Create _rolePM variable with string type
        /// </summary>
        private string _rolePM;

        /// <summary>
        /// Create _roleRoleRPM variable with string type
        /// </summary>
        private string _roleRoleRPM;

        /// <summary>
        /// Create _pageNumber variable with int type
        /// </summary>
        private int _pageNumber;

        /// <summary>
        /// Create _sortExpressionAndDirection variable with string type
        /// </summary>
        private string _sortExpressionAndDirection;

        #endregion Private Field Members

        #region Properties

        /// <summary>
        /// Define UserMailId property
        /// </summary>
        public string UserMailId
        {
            get
            {
                return _userMailId;
            }
            set
            {
                _userMailId = value;
            }
        }

        /// <summary>
        /// Define RoleCOO property
        /// </summary>
        public string RoleCOO
        {
            get
            {
                return _roleCOO;
            }
            set
            {
                _roleCOO = value;
            }
        }

        /// <summary>
        /// Define RolePresales property
        /// </summary>
        public string RolePresales
        {
            get
            {
                return _rolePresales;
            }
            set
            {
                _rolePresales = value;
            }
        }

        /// <summary>
        /// Define RolePM property
        /// </summary>
        public string RolePM
        {
            get
            {
                return _rolePM;
            }
            set
            {
                _rolePM = value;
            }
        }

        /// <summary>
        /// Define RoleRPM property
        /// </summary>
        public string RoleRPM
        {
            get
            {
                return _roleRoleRPM;
            }
            set
            {
                _roleRoleRPM = value;
            }
        }

        /// <summary>
        /// Define PageNumber property
        /// </summary>
        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                _pageNumber = value;
            }
        }

        /// <summary>
        /// Define SortExpressionAndDirection property
        /// </summary>
        public string SortExpressionAndDirection
        {
            get
            {
                return _sortExpressionAndDirection;
            }
            set
            {
                _sortExpressionAndDirection = value;
            }
        }

        #endregion Properties


    }
}
