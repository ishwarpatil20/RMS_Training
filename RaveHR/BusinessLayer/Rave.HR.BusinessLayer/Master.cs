///------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           Master.cs       
//  Author:         Vineet Kulkarni
//  Date written:   28/04/2009/ 5:32:30 PM
//  Description:    This class gets the master data
//                  
//
//  Amendments
//  Date                  Who                   Ref     Description
//  ----                  -----------           ---     -----------
//  28/04/2009 5:32:30 PM  Vineet Kulkarni      n/a     Created    
//  02/09/2009 4:41:30 PM  Sudip Guha           n/a     Modified 
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using BusinessEntities;
using Rave.HR.DataAccessLayer.Common;
using Common;


namespace Rave.HR.BusinessLayer.Common
{
       

    public class Master
    {
        #region Private Member

        /// <summary>
        /// private variable for Class Name used in each catch block
        /// </summary>
        private string CLASS_NAME = "Master";

        /// <summary>
        /// private variable for split email footer;
        /// </summary>
        char[] SPILITER_DOT = { '.' };

        string regards;

        #endregion

        /// <summary>
        /// Gets master data such as ProjectStatus, MRFStatus etc.
        /// </summary>
        /// <returns>List</returns>
        public List<BusinessEntities.Master> GetMasterData(string Category)
        {
            try
            {
                Rave.HR.DataAccessLayer.Common.Master masterData = new Rave.HR.DataAccessLayer.Common.Master();
                DataTable dtMasterData = new DataTable();
                dtMasterData = masterData.FillDropDownList(Category);

                List<BusinessEntities.Master> listMasterData = new List<BusinessEntities.Master>();
                BusinessEntities.Master fetchMasterData = null;
                foreach (DataRow drMasterData in dtMasterData.Rows)
                {
                    fetchMasterData = new BusinessEntities.Master();
                    fetchMasterData.MasterId = int.Parse(drMasterData["MasterID"].ToString());
                    fetchMasterData.MasterName = drMasterData["MasterName"].ToString();
                    listMasterData.Add(fetchMasterData);
                }
                return listMasterData;
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw ex;
                //throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, "RaveHRMaster.cs", "GetMasterData");
            }
        }

        /// <summary>
        /// Gets WorkFlow Status data.
        /// </summary>
        /// <returns>List</returns>
        //public List<BusinessEntities.RaveHRMaster> GetWorkFlowStatus()
        //{
        //    try
        //    {
        //        Rave.HR.DataAccessLayer.Common.RaveHRMaster_Temp workFlowStatusData = new Rave.HR.DataAccessLayer.Common.RaveHRMaster_Temp();
        //        DataTable dtWorkFlowStatusData = new DataTable();
        //        dtWorkFlowStatusData = workFlowStatusData.GetWorkFlowStatus();

        //        List<BusinessEntities.RaveHRMaster> listWorkFlowStatusData = new List<BusinessEntities.RaveHRMaster>();
        //        BusinessEntities.RaveHRMaster fetchWorkFlowStatusData = null;
        //        foreach (DataRow drWorkFlowStatusData in dtWorkFlowStatusData.Rows)
        //        {
        //            fetchWorkFlowStatusData = new BusinessEntities.RaveHRMaster();
        //            fetchWorkFlowStatusData.WorkFlowStatusId = int.Parse(drWorkFlowStatusData["ID"].ToString());
        //            fetchWorkFlowStatusData.WorkFlowStatus = drWorkFlowStatusData["Code"].ToString();
        //            listWorkFlowStatusData.Add(fetchWorkFlowStatusData);
        //        }
        //        return listWorkFlowStatusData;
        //    }

        //    catch (RaveHRException ex)
        //    {
        //        throw ex;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //        //throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, "RaveHRMaster.cs", "GetWorkFlowStatus");
        //    }
        //}

        /// <summary>
        /// Gets list of Project names
        /// </summary>
        /// <returns>List</returns>
        //public List<BusinessEntities.RaveHRMaster>  GetMasterDataProj(string strProj)
        //public List<BusinessEntities.RaveHRMaster> GetProjects(string Category)
        //{
        //    try
        //    {
        //        Rave.HR.DataAccessLayer.Common.RaveHRMaster_Temp projectNames = new Rave.HR.DataAccessLayer.Common.RaveHRMaster_Temp();
        //        DataTable dtProjectNames = new DataTable();
        //        dtProjectNames = projectNames.FillDropDownListProj(Category);

        //        List<BusinessEntities.RaveHRMaster> listProjectNames = new List<BusinessEntities.RaveHRMaster>();
        //        BusinessEntities.RaveHRMaster fetchProjectNames = null;
        //        foreach (DataRow drProjectNames in dtProjectNames.Rows)
        //        {
        //            fetchProjectNames = new BusinessEntities.RaveHRMaster();
        //            fetchProjectNames.iProjID = int.Parse(drProjectNames["ProjectID"].ToString());
        //            fetchProjectNames.strProjName = drProjectNames["ProjectName"].ToString();
        //            listProjectNames.Add(fetchProjectNames);
        //        }
        //        return listProjectNames;
        //    }

        //    catch (RaveHRException ex)
        //    {
        //        throw ex;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //        //throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, "RaveHRMaster.cs", " GetProjects");
        //    }
        //}

        /// <summary>
        /// Gets master data such as ProjectStatus, MRFStatus etc.
        /// </summary>
        /// <returns>List</returns>
        public List<BusinessEntities.Master> GetEmployeeDesignationWise(string Designation)
        {
            try
            {
                Rave.HR.DataAccessLayer.Common.Master masterData = new Rave.HR.DataAccessLayer.Common.Master();
                DataTable dtMasterData = new DataTable();
                dtMasterData = masterData.FillDropDownListAccountManager(Designation);

                List<BusinessEntities.Master> listMasterData = new List<BusinessEntities.Master>();
                BusinessEntities.Master fetchMasterData = null;
                foreach (DataRow drMasterData in dtMasterData.Rows)
                {
                    fetchMasterData = new BusinessEntities.Master();
                    fetchMasterData.MasterId = int.Parse(drMasterData["EMPId"].ToString());
                    fetchMasterData.MasterName = drMasterData["MasterName"].ToString();
                    fetchMasterData.MasterEmailID = drMasterData["EmailID"].ToString();
                    listMasterData.Add(fetchMasterData);
                }
                return listMasterData;
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw ex;
                //throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, "RaveHRMaster.cs", "GetMasterData");
            }
        }

        /// <summary>
        /// Gets Employee ID.
        /// </summary>
        /// <returns>List</returns>
        public int GetLoggedInUserID(string EmailId)
        {
            int employeeID;
            try
            {
                Rave.HR.DataAccessLayer.Common.Master getEmployeeID = new Rave.HR.DataAccessLayer.Common.Master();

                employeeID = getEmployeeID.GetEmployeeID(EmailId);
                return employeeID;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, "RaveHRProjects.cs", "getProjectName", EventIDConstants.RAVE_HR_PROJECTS_BUSNIESS_LAYER);
            }
            //return Con_Obj;
        }

        /// <summary>
        /// Master data for dropDown
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection FillDropDownsBL(int categoryId)
        {
            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                Rave.HR.DataAccessLayer.Common.Master master = new Rave.HR.DataAccessLayer.Common.Master();
                raveHRCollection = master.FillDropDownsDL(categoryId);
                return raveHRCollection;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }           
        }
        //Siddhesh Arekar Issue ID : 55884 Closure Type
        /// <summary>
        /// Get Master Type Details
        /// </summary>
        /// <param name="category"></param>
        /// <returns>string</returns>
        public KeyValue<string> GetMasterTypeDetails(int categoryId, string key)
        {
            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                Rave.HR.DataAccessLayer.Common.Master master = new Rave.HR.DataAccessLayer.Common.Master();
                return master.GetMasterTypeDetails(categoryId, key);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
        }
        //Siddhesh Arekar Issue ID : 55884 Closure Type

        /// <summary>
        /// Master data for Department dropdown
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection FillDepartmentDropDownBL()
        {
            try
            {
                // initialise Collection class object
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

                // Initialise Data Layer object
                Rave.HR.DataAccessLayer.Common.Master master = new Rave.HR.DataAccessLayer.Common.Master();

                // call the Data layer Method
                raveHRCollection = master.FillDepartmentDropDownDL();

                //return the Collection
                return raveHRCollection;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }           
        }

        #region Modified By Mohamed Dangra
        // Mohamed : NIS-RMS : 29/12/2014 : Starts                        			  
        // Desc : Show Departement for which the person is eligible

        /// <summary>
        /// Master data for Department dropdown
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection FillEligibleDepartmentDropDownBL(string strCurrentUser)
        {
            try
            {
                // initialise Collection class object
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

                // Initialise Data Layer object
                Rave.HR.DataAccessLayer.Common.Master master = new Rave.HR.DataAccessLayer.Common.Master();

                // call the Data layer Method
                raveHRCollection = master.FillEligibleDepartmentDropDownDL(strCurrentUser);

                //return the Collection
                return raveHRCollection;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
        }

        // Mohamed :  : 29/12/2014 : Ends
        #endregion Modified By Mohamed Dangra

       /// <summary>
       /// Function will Get Email Footer with Upper case of first character & Upper 
       /// case of Last character.
       /// </summary>
       /// <param name="regrdsString"></param>
       /// <returns></returns>
        public string GetEmailFooter(string regrdsString)
        {
            string[] regardsarr = regrdsString.Split(SPILITER_DOT);

            regards = regardsarr[0].Substring(0, 1).ToUpper() + regardsarr[0].Substring(1, regardsarr[0].Length -1 );
            regards = regards + " " + regardsarr[1].Substring(0, 1).ToUpper() + regardsarr[1].Substring(1, regardsarr[1].Length -1 );

            return regards;
        }

        /// <summary>
        /// Added by Kanchan for the requirment specified in the Discussion with Sawita Kamath and Gaurav Thakkar.
        /// Requirment raised:
        /// Gives the emailId for the employee whose Employee id is supplied.
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns> 
        public string GetEmailID(int empId)
        {
            try
            {
                Rave.HR.DataAccessLayer.Common.Master master = new Rave.HR.DataAccessLayer.Common.Master();
                return master.getEmployeeEmailID(empId);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, "master", "GetEmailID", EventIDConstants.RAVE_HR_MASTER_BUSINESS_ACCESS_LAYER);
            }
        }

        /// <summary>
        /// Added by Abhishek for Converting all the roles in Pascal format.
        /// Issue logged:
        /// Returns the passed string in Pascal Format.
        /// </summary>
        /// <param name="strWord"></param>
        /// <returns></returns>
        /// To be implemented after discussion with Prashant and Vishal
        public string ConvertToPascal(string strWord)
        {
            try
            {
                string eachChar = string.Empty;
                string finalPascalWord = string.Empty;
                int counter = 0;
                foreach (char strWordChar in strWord)
                {
                    if (counter == 0)
                    {
                        eachChar = strWordChar.ToString();
                        eachChar = eachChar.ToUpper();
                    }
                    else
                    {
                        eachChar = strWordChar.ToString();
                        eachChar = eachChar.ToLower();
                    }
                    finalPascalWord += eachChar;
                    counter++;
                }
                return finalPascalWord;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }   
        }


        /// <summary>
        /// Get_s the client abbrivation.
        /// </summary>
        /// <param name="MasterId">The master id.</param>
        /// <returns></returns>
        public string Get_ClientAbbrivation(int MasterId)
        {
            string sname = string.Empty;
            try
            {
                Rave.HR.DataAccessLayer.Common.Master masterData = new Rave.HR.DataAccessLayer.Common.Master();
                DataTable dtMasterData = new DataTable();
               
                sname = masterData.Get_ClientAbbrivation(MasterId);
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw ex;
                //throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, "RaveHRMaster.cs", "GetMasterData");
            }
            return sname;
        }



        /// <summary>
        /// Master data for GetClientName dropdown
        /// </summary>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection GetClientNameBL()
        {
            try
            {
                // initialise Collection class object
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

                // Initialise Data Layer object
                Rave.HR.DataAccessLayer.Common.Master master = new Rave.HR.DataAccessLayer.Common.Master();

                // call the Data layer Method
                raveHRCollection = master.GetClientNameDL();

                //return the Collection
                return raveHRCollection;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
        }

        //Ishwar Patil : Trainging Module 29/04/2014 : Starts
        /// <summary>
        /// Gets Training module master data 
        /// </summary>
        /// <returns>List</returns>
        public List<BusinessEntities.Master> GetTraining_MasterData(string Category)
        {
            try
            {
                Rave.HR.DataAccessLayer.Common.Master masterData = new Rave.HR.DataAccessLayer.Common.Master();
                DataTable dtMasterData = new DataTable();
                dtMasterData = masterData.FillTraining_DropDownList(Category);

                List<BusinessEntities.Master> listMasterData = new List<BusinessEntities.Master>();
                BusinessEntities.Master fetchMasterData = null;
                foreach (DataRow drMasterData in dtMasterData.Rows)
                {
                    fetchMasterData = new BusinessEntities.Master();
                    fetchMasterData.MasterId = int.Parse(drMasterData["MasterID"].ToString());
                    fetchMasterData.MasterName = drMasterData["MasterName"].ToString();
                    listMasterData.Add(fetchMasterData);
                }
                return listMasterData;
            }

            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Ishwar Patil : Trainging Module 29/04/2014 : End
        
        // Ishwar NISRMS 13032015 Start
        public BusinessEntities.RaveHRCollection FillDropDownsBLForStatus(int categoryId)
        {
            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                Rave.HR.DataAccessLayer.Common.Master master = new Rave.HR.DataAccessLayer.Common.Master();
                raveHRCollection = master.FillDropDownsDLForStatus(categoryId);
                return raveHRCollection;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
        }
        // Ishwar NISRMS 13032015 End
    }
}
