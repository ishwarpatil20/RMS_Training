///------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           DBConstants.cs       
//  Author:         vineet Kulkarni
//  Date written:   03/04/2009/ 3:41:29 PM
//  Description:    This class provides the method to access connection string
//                  
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  03/04/2009 3:41:29 PM  Prashant Mala    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Common
{
    public class DBConstants
    {        
        //--Rave HR ConnectionString
        public static string GetDBConnectionString()
        {
            string strRaveHRConnectionString = ConfigurationManager.ConnectionStrings["RaveHRConnectionString"].ConnectionString;
            strRaveHRConnectionString = Utility.DecryptPassword(strRaveHRConnectionString);
            return strRaveHRConnectionString;
        }
    }
}
