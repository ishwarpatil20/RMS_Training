//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           Master.cs       
//  Author:         Gaurav.Thakkar
//  Date written:   4/03/2009 03:57:20 PM
//  Description:    This class defines properties related to Master Data 
//                  These business entities are used for ProjectSummary.aspx ViewProject.aspx etc 
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  4/03/2009 03:57:20 PM  Gaurav.Thakkar    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    /// <summary>
    /// This class is used for to set and get Master Data
    /// </summary>
    public class Master
    {
        #region Private Field Members

        /// <summary>
        /// Create _masterId variable with int type
        /// </summary>
        private int _masterId;

        /// <summary>
        /// Create _masterName variable with string type
        /// </summary>
        private string _masterName;

        /// <summary>
        /// Create _details variable with string type
        /// </summary>
        private string _details;

        /// <summary>
        /// Create _category variable with string type
        /// </summary>
        //private string _category;                

        #endregion Private Field Members

        #region Properties

        /// <summary>
        /// Define MasterId property
        /// </summary>
        public int MasterId
        {
            get 
            { 
                return _masterId; 
            }
            set 
            { 
                _masterId = value; 
            }
        }        

        /// <summary>
        /// Define MasterName property
        /// </summary>
        public string MasterName
        {
            get 
            { 
                return _masterName; 
            }
            set 
            { 
                _masterName = value; 
            }
        }

        /// <summary>
        ///Declared and define EmailID property
        /// </summary>
        public string MasterEmailID
        { get; set; }

        /// <summary>
        /// Define GroupMaster Id property
        /// </summary>
        public int GroupMasterId { get; set; }


        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>The details.</value>
        public string Details
        {
            get
            {
                return _details;
            }
            set
            {
                _details = value;
            }
        }

        #endregion Properties
    }
}
