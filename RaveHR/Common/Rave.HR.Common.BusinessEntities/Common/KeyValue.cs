//------------------------------------------------------------------------------
//
//  File:           KeyValue.cs       
//  Author:         Sunil.Mishra
//  Date written:   08/31/2009 01:39:10 PM
//  Description:    Class Contains the Key and Value field for filling Dropdowns.
//  Amendments
//  Date                    Who                 Ref     Description
//  ----                    ---                 ---     -----------
//  08/31/2009 01:39:10 PM   Sunil.Mishra         n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    [Serializable]
    public class KeyValue<T>
    {
        #region Field Members
        /// <summary>
        ///  Create keyName variable with string type
        /// </summary>
        private string key;

        /// <summary>
        /// Create keyName variable with string type
        /// </summary>
        ///  /// <summary>
        private T val;

        private T group;

        #endregion Field Members

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public KeyValue()
        {

        }

        /// <summary>
        /// Create a consturctor with parameter
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="val"></param>
        public KeyValue(string keyName, T val)
        {

            this.key = keyName;

            this.val = val;

        }
        #endregion Constructors

        #region Properties

        /// <summary>
        /// Define KeyName property
        /// </summary>
        public string KeyName
        {

            get { return key; }

            set { key = value; }

        }

        /// <summary>
        /// Define KeyValue property
        /// </summary>
        public T Val
        {

            get { return val; }

            set { val = value; }

        }

        public T Group
        {
            get { return group; }
            set { group = value; }
        }

        #endregion Properties
    }
}
