//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           SkillsDetails.cs       
//  Author:         Ishwar Patil
//  Date written:   05/12/2014
//  Description:    This class provides the business layer methods for SkillsDetails.
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Common;
using Common.Constants;

namespace Rave.HR.BusinessLayer.Employee
{
    public class SkillsDetails
    {
        #region Private Member Variables

        /// <summary>
        /// private variable for Class Name used in each catch block
        /// </summary>
        private string CLASS_NAME = "SkillsDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string ADDSKILLSDETAILS = "AddSkillsDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string UPDATESKILLSDETAILS = "UpdateSkillsDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string GETSKILLSDETAILS = "GetSkillsDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string DELETESKILLSDETAILS = "DeleteSkillsDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string MANIPULATION = "Manipulation";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string GETMANAGERSEMAILID = "GetManagersEmailId";

        #endregion Private Member Variables

        #region Public Member Functions

        /// <summary>
        /// Adds the skills details.
        /// </summary>
        /// <param name="objAddSkillsDetails">The obj add skills details.</param>
        public void AddSkillsDetails(BusinessEntities.SkillsDetails objAddSkillsDetails)
        {
            //Object declaration of SkillsDetails class
            Rave.HR.DataAccessLayer.Employees.SkillsDetails objAddSkillsDetailsDAL;

            try
            {
                //Created new instance of SkillsDetails class to call AddSkillsDetails() of Data access layer
                objAddSkillsDetailsDAL = new Rave.HR.DataAccessLayer.Employees.SkillsDetails();

                //Call to AddSkillsDetails() of Data access layer
                objAddSkillsDetailsDAL.AddSkillsDetails(objAddSkillsDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, ADDSKILLSDETAILS, EventIDConstants.RAVE_EMP_SKILL_SEARCH_BUSINESS_ACCESS_LAYER);
            }

        }

        /// <summary>
        /// Updates the skills details.
        /// </summary>
        /// <param name="objUpdateSkillsDetails">The obj update skills details.</param>
        public void UpdateSkillsDetails(BusinessEntities.SkillsDetails objUpdateSkillsDetails)
        {
            //Object declaration of SkillsDetails class
            Rave.HR.DataAccessLayer.Employees.SkillsDetails objUpdateSkillsDetailsDAL;

            try
            {
                //Created new instance of SkillsDetails class to call UpdateSkillsDetails() of Data access layer
                objUpdateSkillsDetailsDAL = new Rave.HR.DataAccessLayer.Employees.SkillsDetails();

                //Call to UpdateSkillsDetails() of Data access layer
                objUpdateSkillsDetailsDAL.UpdateSkillsDetails(objUpdateSkillsDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, UPDATESKILLSDETAILS, EventIDConstants.RAVE_EMP_SKILL_SEARCH_BUSINESS_ACCESS_LAYER);
            }
        }

        /// <summary>
        /// Gets the skills details.
        /// </summary>
        /// <param name="objGetSkillsDetails">The obj get skills details.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetSkillsDetails(BusinessEntities.SkillsDetails objGetSkillsDetails)
        {
            //Object declaration of SkillsDetails class
            Rave.HR.DataAccessLayer.Employees.SkillsDetails objGetSkillsDetailsDAL;
            
            try
            {
                //Created new instance of SkillsDetails class to call GetSkillsDetails() of Data access layer
                objGetSkillsDetailsDAL = new Rave.HR.DataAccessLayer.Employees.SkillsDetails();

                //Call to GetSkillsDetails() of Data access layer and return the Skills
                return objGetSkillsDetailsDAL.GetSkillsDetails(objGetSkillsDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, GETSKILLSDETAILS, EventIDConstants.RAVE_EMP_SKILL_SEARCH_BUSINESS_ACCESS_LAYER);
            }           
        }

        /// <summary>
        /// Deletes the skills details.
        /// </summary>
        /// <param name="objDeleteSkillsDetails">The obj delete skills details.</param>
        public void DeleteSkillsDetails(BusinessEntities.SkillsDetails objDeleteSkillsDetails)
        {
            //Object declaration of SkillsDetails class
            Rave.HR.DataAccessLayer.Employees.SkillsDetails objDeleteSkillsDetailsDAL;

            try
            {
                //Created new instance of SkillsDetails class to call DeleteSkillsDetails() of Data access layer
                objDeleteSkillsDetailsDAL = new Rave.HR.DataAccessLayer.Employees.SkillsDetails();

                //Call to DeleteQualificationDetails() of Data access layer
                objDeleteSkillsDetailsDAL.DeleteSkillsDetails(objDeleteSkillsDetails);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, DELETESKILLSDETAILS, EventIDConstants.RAVE_EMP_SKILL_SEARCH_BUSINESS_ACCESS_LAYER);
            }
        }

        /// <summary>
        /// Manipulations the specified obj skills details list.
        /// </summary>
        /// <param name="objSkillsDetailsList">The obj skills details list.</param>
        public void Manipulation(BusinessEntities.RaveHRCollection SkillsDetailsCollection)
        {
            try
            {
                //Check for each each entry in SkillsDetails List
                foreach (BusinessEntities.SkillsDetails objAddSkillsDetails in SkillsDetailsCollection)
                {
                    //If mode of the entry is Add i.e. 1 then call Add()
                    if (objAddSkillsDetails.Mode == 1)
                    {
                        this.AddSkillsDetails(objAddSkillsDetails);

                    }

                    //If mode of the entry is Update i.e. 2 then call Update()
                    if (objAddSkillsDetails.Mode == 2)
                    {
                        this.UpdateSkillsDetails(objAddSkillsDetails);

                    }

                    //If SkillId is greater than zero i.e. Skills is already added and mode of the entry is Delete i.e. 3 then call Delete()
                    if (objAddSkillsDetails.Mode == 3)
                    {
                        this.DeleteSkillsDetails(objAddSkillsDetails);
                    }
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, MANIPULATION, EventIDConstants.RAVE_EMP_SKILL_SEARCH_BUSINESS_ACCESS_LAYER);
            }
        }

        /// <summary>
        /// Gets the managers email id.
        /// </summary>
        /// <param name="EmpID">The emp ID.</param>
        /// <returns></returns>
        public string GetManagersEmailId(int EmpID)
        {
            //Object declaration of SkillsDetails class
            Rave.HR.DataAccessLayer.Employees.SkillsDetails objGetSkillsDetailsDAL;

            try
            {
                //Created new instance of SkillsDetails class to call GetSkillsDetails() of Data access layer
                objGetSkillsDetailsDAL = new Rave.HR.DataAccessLayer.Employees.SkillsDetails();

                //Call to GetSkillsDetails() of Data access layer and return the Skills
                return objGetSkillsDetailsDAL.GetManagersEmailId(EmpID);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, GETMANAGERSEMAILID, EventIDConstants.RAVE_EMP_SKILL_SEARCH_BUSINESS_ACCESS_LAYER);
            }
        }

        /// <summary>
        /// Master data for dropDown
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection FillDropDownsBL(int EmpId)
        {
            //Object declaration of SkillsDetails class
            Rave.HR.DataAccessLayer.Employees.SkillsDetails objGetSkillsDetailsDAL;

            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                
                //Created new instance of SkillsDetails class to call GetSkillsDetails() of Data access layer
                objGetSkillsDetailsDAL = new Rave.HR.DataAccessLayer.Employees.SkillsDetails();

                raveHRCollection = objGetSkillsDetailsDAL.FillDropDownsDL(EmpId);

                return raveHRCollection;

            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "FillDropDownsBL", EventIDConstants.RAVE_EMP_SKILL_SEARCH_BUSINESS_ACCESS_LAYER);
            }
        }

        /// <summary>
        /// Adds the employee skill details.
        /// </summary>
        public void AddPrimarySkillDetails(int EmpId,string PrimarySkills)
        {
            //Object declaration of SkillsDetails class
            Rave.HR.DataAccessLayer.Employees.SkillsDetails objGetSkillsDetailsDAL;

            try
            {
                //Created new instance of SkillsDetails class to call GetSkillsDetails() of Data access layer
                objGetSkillsDetailsDAL = new Rave.HR.DataAccessLayer.Employees.SkillsDetails();

                objGetSkillsDetailsDAL.AddPrimarySkillDetails(EmpId, PrimarySkills);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "AddPrimarySkillDetails", EventIDConstants.RAVE_EMP_SKILL_SEARCH_BUSINESS_ACCESS_LAYER);
            }
 
        }

        /// <summary>
        /// Gets the skill type.
        /// </summary>
        public BusinessEntities.RaveHRCollection GetSkillTypes()
        {
            //Object declaration of SkillsDetails class
            Rave.HR.DataAccessLayer.Employees.SkillsDetails objGetSkillsDetailsDAL;

            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

                //Created new instance of SkillsDetails class to call GetSkillsDetails() of Data access layer
                objGetSkillsDetailsDAL = new Rave.HR.DataAccessLayer.Employees.SkillsDetails();

                raveHRCollection = objGetSkillsDetailsDAL.GetSkillTypes();

                return raveHRCollection;

            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetSkillTypes", EventIDConstants.RAVE_EMP_SKILL_SEARCH_BUSINESS_ACCESS_LAYER);
            }
        }


        public BusinessEntities.RaveHRCollection GetSkillTypesCategory()
        {
            //Object declaration of SkillsDetails class
            Rave.HR.DataAccessLayer.Employees.SkillsDetails objGetSkillsDetailsDAL;

            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

                //Created new instance of SkillsDetails class to call GetSkillsDetails() of Data access layer
                objGetSkillsDetailsDAL = new Rave.HR.DataAccessLayer.Employees.SkillsDetails();

                raveHRCollection = objGetSkillsDetailsDAL.GetSkillTypesCategory();

                return raveHRCollection;

            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetSkillTypesCategory", EventIDConstants.RAVE_EMP_SKILL_SEARCH_BUSINESS_ACCESS_LAYER);
            }
        }

        public BusinessEntities.RaveHRCollection GetPrimaryAndSecondarySkills()
        {
            Rave.HR.DataAccessLayer.Employees.SkillsDetails objGetSkillsDetailsDAL;
            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

                //Created new instance of SkillsDetails class to call GetSkillsDetails() of Data access layer
                objGetSkillsDetailsDAL = new Rave.HR.DataAccessLayer.Employees.SkillsDetails();

                raveHRCollection = objGetSkillsDetailsDAL.GetPrimaryAndSecondarySkills();

                return raveHRCollection;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetPrimaryAndSecondarySkills", EventIDConstants.RAVE_EMP_SKILL_SEARCH_BUSINESS_ACCESS_LAYER);
            }
        }

        public DataTable GetSkillSearchDetails(string commaSeparatedMandatorySkills, string commadSepratedOptionalSkills, string SortExpressionAndDirection)
        {
            Rave.HR.DataAccessLayer.Employees.SkillsDetails objGetSkillsDetailsDAL;
            try
            {
                BusinessEntities.SkillSearch objSkillSearch = new BusinessEntities.SkillSearch();
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();

                objSkillSearch.MandatorySkills = commaSeparatedMandatorySkills;
                objSkillSearch.OptionalSkills = commadSepratedOptionalSkills;

                objGetSkillsDetailsDAL = new Rave.HR.DataAccessLayer.Employees.SkillsDetails();
                //Siddharth 27 March 2015 Start
                return objGetSkillsDetailsDAL.GetSkillSearchDetails(objSkillSearch, SortExpressionAndDirection);
                //Siddharth 27 March 2015 End
                //return raveHRCollection;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "GetSkillSearchDetails", EventIDConstants.RAVE_EMP_SKILL_SEARCH_BUSINESS_ACCESS_LAYER);
            }
        }

        //Siddhesh Arekar Domain Details 09032015 Start
        public bool Check_SkillCategory_Exists(string skillCategory)
        {
            try
            {
                Rave.HR.DataAccessLayer.Employees.SkillsDetails objGetSkillsDetailsDAL = new Rave.HR.DataAccessLayer.Employees.SkillsDetails();
                return objGetSkillsDetailsDAL.Check_SkillCategory_Exists(skillCategory);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.BusinessLayer, CLASS_NAME, "Check_SkillCategory_Exists", EventIDConstants.RAVE_HR_EMPLOYEE_BUSNIESS_LAYER);
            }
        }
        //Siddhesh Arekar Domain Details 09032015 End
        #endregion Public Member Functions
    }
}
