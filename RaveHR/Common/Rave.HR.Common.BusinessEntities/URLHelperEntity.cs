//------------------------------------------------------------------------------
//
//  File:           URLHelperEntity.cs       
//  Author:         Abhishek.Varma
//  Date written:   04/27/2010 10:39:10 AM
//  Description:    Class Contains the Name and Value field for Query String.
//  Amendments
//  Date                    Who                 Ref     Description
//  ----                    ---                 ---     -----------
//  04/27/2010 10:39:10 AM  Abhishek.Varma      n/a     Created    
//
//------------------------------------------------------------------------------


using System;
namespace BusinessEntities
{
    public class URLHelperEntity
    {
        
        #region Field Members
        
        private string queryStringName = string.Empty;

        private string queryStringValue = string.Empty;

        #endregion

        #region Properties

        public string QueryStringName
        {
            get { return queryStringName; }

            set { queryStringName = value; }
        }

        public string QueryStringValue
        {
            get { return queryStringValue; }

            set { queryStringValue = value; }
        }

        #endregion
    }
}
