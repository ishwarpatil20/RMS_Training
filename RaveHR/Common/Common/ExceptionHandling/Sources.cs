//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           Sources.cs       
//  Author:         Prashant Mala
//  Date written:   11/04/2009/ 5:32:30 PM
//  Description:    This class  provides constants for layers defined
//                  
//
//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  28/04/2009 5:32:30 PM  Prashant Mala    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Text;

namespace Common
{
    /// <summary>
    /// The purpose of the <see cref="Sources"/> class is to
    /// define the constants for the various sources. Sources are nothing but the logical grouping of all dlls for a component. 
    /// For example, scoring engine component might be made up of following dlls 
    /// </summary>
    public static class Sources
    {
        /// Error Source  for Exceptions from PresentationLayer.
        /// </summary>
        public const string PresentationLayer = "Rave.HR.PresentationLayer";

        /// Error Source  for Exceptions from BusinessLayer.
        /// </summary>
        public const string BusinessLayer = "Rave.HR.BusinessLayer";

        /// Error Source  for Exceptions from DataAccessLayer.
        /// </summary>
        public const string DataAccessLayer = "Rave.HR.DataAccessLayer";

        /// Error Source  for Exceptions from Common Layer.
        /// </summary>
        public const string CommonLayer = "Common";
    }
}
