///------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           EventLogger.cs       
//  Author:         Prashant Mala
//  Date written:   09/04/2009/ 5:51:00 PM
//  Description:    This class provides the methods event logging
//                  
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  09/04/2009 5:51:00 PM  Prashant Mala    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

/// <summary>
/// Summary description for ErrorLogger
/// </summary>

namespace Common
{
    public class EventLogger
    {
        public EventLogger()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //*************************************************************
        //NAME:          WriteToEventLog
        //PURPOSE:       Write to Event Log
        //PARAMETERS:    Entry - Value to Write
        //               AppName - Name of Client Application. Needed 
        //               because before writing to event log, you must 
        //               have a named EventLog source. 
        //               EventType - Entry Type, from EventLogEntryType 
        //               Structure e.g., EventLogEntryType.Warning, 
        //               EventLogEntryType.Error
        //               LogNam1e: Name of Log (System, Application; 
        //               Security is read-only) If you 
        //               specify a non-existent log, the log will be
        //               created

        //RETURNS:       True if successful
        //*************************************************************
        
        public bool WriteToEventLog(string strMessage, EventLogEntryType eventType, string strLogName, string strApplicationName)
        {
            EventLog objEventLog = new EventLog();
            try
            {
                if (!(EventLog.SourceExists(strApplicationName)))
                { 
                    EventLog.CreateEventSource(strApplicationName, strLogName);
                }
                objEventLog.Source = strApplicationName;
                objEventLog.WriteEntry(strMessage, eventType);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
                return false;
            }
        }
    }
}

