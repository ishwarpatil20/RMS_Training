//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           Category.cs       
//  Author:         vineet.kulkarni
//  Date written:   4/23/2009 12:39:31 PM
//  Description:    This class defines properties related to Category of Technolgy 
//                  These business entities are used for ViewProject.aspx etc 
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  4/23/2009 12:39:31 PM  vineet.kulkarni    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    /// <summary>
    /// This class is used to get and set categories
    /// </summary>
    public class Category
    {
        #region Private Field Members

        /// <summary>
        /// Create _categoryId variable with integer type
        /// </summary>
        /// 
        private int _categoryId;
        /// <summary>
        /// Create _categoryName variable with string type
        /// </summary>
        private string _categoryName;

        /// <summary>
        /// Create _lstTechnology object with generic list type
        /// </summary>
        private List<Technology> _lstTechnology = new List<Technology>();

        #endregion Private Field Members

        #region Properties

        /// <summary>
        /// Define CategoryId property
        /// </summary>
        public int CategoryId
        {
            get
            {
                return _categoryId;
            }
            set
            {
                _categoryId = value;
            }
        }

        /// <summary>
        /// Define CategoryName property
        /// </summary>
        public string CategoryName
        {
            get
            {
                return _categoryName;
            }
            set
            {
                _categoryName = value;
            }
        }

        /// <summary>
        /// Define Technologies property
        /// </summary>
        public List<Technology> Technologies
        {
            get
            {
                return _lstTechnology;
            }
            set
            {
                _lstTechnology = value;
            }
        }

        #endregion Properties
    }
}