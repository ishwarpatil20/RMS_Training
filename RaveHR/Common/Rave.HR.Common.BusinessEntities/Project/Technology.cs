//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           Technology.cs       
//  Author:         vineet.kulkarni
//  Date written:   4/23/2009 12:50:54 PM
//  Description:    This class defines properties related to Technolgoy
//                  These business entities are used for ProjectSearch.aspx, AddProject.aspx etc. 
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  4/23/2009 12:50:54 PM  vineet.kulkarni    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    /// <summary>
    /// This class is used for to set and get technologies based upon category selection
    /// </summary>
    public class Technology
    {
      
        #region Private Field Members

        /// <summary>
        /// Create _technologyID variable with int type
        /// </summary>
        private int _technologyID;

        /// <summary>
        /// Create _technolgoyName variable with string type
        /// </summary>
        private string _technolgoyName;

        /// <summary>
        /// Create _categoryID variable with int type
        /// </summary>
        private int _categoryID;
       
        #endregion Private Field Members

        #region Constructor

        /// <summary>
        /// constructor for Technolgoy
        /// </summary>
        /// <param name="id">int</param>
        /// <param name="name">string</param>
        public Technology(int TechnologyID, string technolgoyName, int categoryID)
        {
            _technologyID = TechnologyID;
            _technolgoyName = technolgoyName;
            _categoryID = categoryID;          
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Define TechnologyID property
        /// </summary>
        public int TechnologyID
        {
            get 
            { 
                return _technologyID; 
            }
            set 
            { 
                _technologyID = value; 
            }
        }

        /// <summary>
        /// Define TechnolgoyName property
        /// </summary>
        public string TechnolgoyName
        {
            get
            {
                return _technolgoyName;
            }
            set
            {
                _technolgoyName = value;
            }
        }

        /// <summary>
        /// Define CategoryID property
        /// </summary>
        public int CategoryID
        {
            get
            {
                return _categoryID;
            }
            set
            {
                _categoryID = value;
            }
        }
       
        #endregion Properties
    }
}