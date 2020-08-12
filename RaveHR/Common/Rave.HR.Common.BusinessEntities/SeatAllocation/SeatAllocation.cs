//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           SeatAllocation.cs       
//  Author:         Kanchan.Singh
//  Date written:   19/11/2009 2:00:00 PM
//  Description:    This page serves as the Business Entity definations for the Seat allocation module.
//
//  Amendments
//  Date                   Who               Ref      Description
//  ----                   -----------       ---      -----------
//  19/11/2009 2:00:00 PM  Kanchan.Singh     n/a      Created    
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
  public class SeatAllocation
    {
        #region Field members
        
        /// <summary>
        /// Define BayID property of type int
        /// </summary>
        public int BayID
        { get; set; }

        /// <summary>
        /// Define BayName property of type string
        /// </summary>
        public string BayName
        { get; set; }

        /// <summary>
        /// Define BayDescrition property of type string
        /// </summary>
        public string BayDescrition
        { get; set; }

        /// <summary>
        /// Define SectionID property of type int
        /// </summary>
        public int SectionID
        { get; set; }

        /// <summary>
        /// Define SectionDescription property of type string
        /// </summary>
        public string SectionDescription
        { get; set; }

        /// <summary>
        /// Define SeatID property of type int
        /// </summary>
        public int SeatID
        { get; set; }

        /// <summary>
        /// Define SeatSequenceNo property of type int
        /// </summary>
        public int SeatSequenceNo
        { get; set; }

        /// <summary>
        /// Define SeatName property of type string
        /// </summary>
        public string SeatName
        { get; set; }

        /// <summary>
        /// Define SeatDescription property of type string
        /// </summary>
        public string SeatDescription
        { get; set; }

        /// <summary>
        /// Define ExtensionNo property of type int
        /// </summary>
        public int ExtensionNo
        { get; set; }

        /// <summary>
        /// Define SeatLandmark property of type string
        /// </summary>
        public string SeatLandmark
        { get; set; }

        /// <summary>
        /// Define EmployeeID property of type int
        /// </summary>
        public int EmployeeID
        { get; set; }

        /// <summary>
        /// Define SeatAllocatedBy property of type int
        /// </summary>
        public int SeatAllocatedBy
        { get; set; }

        /// <summary>
        /// Define AllocationDate property of type DateTime
        /// </summary>
        public DateTime AllocationDate
        { get; set; }

        /// <summary>
        /// Define EmployeeName property of type string
        /// </summary>
        public string EmployeeName
        { get; set; }

        /// <summary>
        /// Define EmployeeCode property of type string
        /// </summary>
        public string EmployeeCode
        { get; set; }

        /// <summary>
        /// Define DepartmentName property of type string
        /// </summary>
        public string DepartmentName
        { get; set; }

        /// <summary>
        /// Define ProjectName property of type string
        /// </summary>
        public string ProjectName
        { get; set; }

        /// <summary>
        /// Define LoggedInuser property of type string
        /// </summary>
        public string LoggedInuser
        { get; set; }

        /// <summary>
        /// Define EmployeeEmailID property of type string
        /// </summary>
        public string EmployeeEmailID
        { get; set; }

        /// <summary>
        /// Define RaveBranchID property of type int
        /// </summary>
        public int RaveBranchID
        { get; set; }

        /// <summary>
        /// Define SectionName property of type string
        /// </summary>
        public string SectionName
        { get; set; }

        #endregion
    }
}
