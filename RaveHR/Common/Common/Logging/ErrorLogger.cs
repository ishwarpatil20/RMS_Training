///------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           ErrorLogger.cs       
//  Author:         Prashant Mala
//  Date written:   09/04/2009/ 5:51:30 PM
//  Description:    This class provides the methods error logging.
//                  
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  09/04/2009 5:51:30 PM  Prashant Mala    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for ErrorLogger
/// </summary>

namespace Common
{
    class ErrorLogger
    {
        public ErrorLogger()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        // *************************************************************
        //NAME:          WriteToErrorLog
        //PURPOSE:       Open or create an error log and submit error message
        //PARAMETERS:    msg - message to be written to error file
        //               stkTrace - stack trace from error message
        //               title - title of the error file entry
        //RETURNS:       Nothing
        //*************************************************************
        public void WriteToErrorLog(string msg, string stkTrace, string title)
        {
            if (!(System.IO.Directory.Exists("\\Errors\\")))
            {
                System.IO.Directory.CreateDirectory("\\Errors\\");
            }
            FileStream fs = new FileStream("\\Errors\\errlog.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter s = new StreamWriter(fs);
            s.Close();
            fs.Close();
            FileStream fs1 = new FileStream("\\Errors\\errlog.txt", FileMode.Append, FileAccess.Write);
            StreamWriter s1 = new StreamWriter(fs1);
            s1.Write("Title: " + title );
            s1.Write("Message: " + msg);
            s1.Write("StackTrace: " + stkTrace);
            s1.Write("Date/Time: " + DateTime.Now.ToString());
            s1.Write("===========================================================================================" );
            s1.Close();
            fs1.Close();
        }

    }
}
