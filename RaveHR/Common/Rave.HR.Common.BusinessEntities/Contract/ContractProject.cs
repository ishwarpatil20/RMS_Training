//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           ContractProject.cs       
//  Class:          ContractProject
//  Author:         prashant.mala
//  Date written:   8/08/2009 5:48:30 PM
//  Description:    This class contains properties related to Contract module. 
//                  These business entities used for ProjectSummary.aspx etc.
//

//  Amendments
//  Date                  Who             Ref     Description
//  ----                  -----------     ---     -----------
//  8/08/2009 5:48:30 PM  prashant.mala    n/a     Created    
//
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class ContractProject : Contract
    {
        #region Field members
        /// <summary>
        /// Define iContractProjectID property
        /// </summary>
         public int ContractProjectID
         { get; set; }

         /// <summary>
         /// Define iProjectID property
         /// </summary>
         public int ProjectID
         { get; set; }

         /// <summary>
         ///Define strProjectCode property 
         /// </summary>
         public string ProjectCode
         { get; set; }

         /// <summary>
         ///Define strProjectType property 
         /// </summary>
         public string ProjectType
         { get; set; }

         /// <summary>
         ///Define strProjectTypeID property 
         /// </summary>
         public int ProjectTypeID
         { get; set; }

         /// <summary>
         ///Define strProjectName property 
         /// </summary>
         public string ProjectName
         { get; set; }

         /// <summary>
         ///Define iProjectLocationID property 
         /// </summary>
         public int ProjectLocationID
         { get; set; }

         /// <summary>
         ///Define iProjectLocationID property 
         /// </summary>
         public string ProjectLocationName
         { get; set; }

         /// <summary>
         ///Define dtStartDate property of type DateTime
         /// </summary>
         public DateTime ProjectStartDate
         { get; set; }

         /// <summary>
         ///Define dtEndDate property of type DateTime
         /// </summary>
         public DateTime ProjectEndDate
         { get; set; }

         /// <summary>
         ///Define iNoOfResources property of type Decimal
         /// </summary>
         public Decimal NoOfResources
         { get; set; }

         /// <summary>
         ///Define strContractCode property 
         /// </summary>
         public string ContractCode
         { get; set; }

         /// <summary>
         ///Define projects description property 
         /// </summary>
         public string ProjectsDescription
         { get; set; }

        /// <summary>
         /// Define projects Status property.
        /// </summary>
         public int StatusID
         { get; set; }

         /// <summary>
         /// Define projects RP Id.
         /// </summary>
         public int RPId
         { get; set; }

         public int ProjectCategoryID
         { get; set; }

         public string ProjectCategoryName
         { get; set; }

         /// <summary>
         /// Gets or sets the project code abbreviation.
         /// </summary>
         /// <value>The project code abbreviation.</value>
         public string ProjectCodeAbbreviation
         { get; set; }

        // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
        // Desc : Add project group in Contract page
         /// <summary>
         /// Gets or sets the project Group
         /// </summary>
         /// <value>The project Group.</value>
         public string ProjectGroup
         { get; set; }
        // Mohamed : Issue 49791 : 15/09/2014 : Ends

        // Mohamed : Issue  : 23/09/2014 : Starts                        			  
        // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page

         public int ProjectDivision
         { get; set; }
         public int ProjectBussinessArea
         { get; set; }
         public int ProjectBussinessSegment
         { get; set; }
         public string ProjectAlias
         { get; set; }

        // Mohamed : Issue  : 23/09/2014 : Ends


         //Siddharth 12 March 2015 Start
         public string ProjectModel
         { get; set; }
        //Siddharth 12 March 2015 End

         //Siddharth 9 Sept 2015 Start
         public string BusinessVertical
         { get; set; }
        //Siddharth 9 Sept 2015 End

     
     #endregion
    }
}
