//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           SkillsDetails.cs       
//  Author:         Ishwar.Patil
//  Date written:   05/12/2014
//  Description:    This class  provides the Data Access layer methods for SkillsDetails in Employee module.
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Data.SqlClient;
using Common;
using Common.Constants;
using BusinessEntities;

namespace Rave.HR.DataAccessLayer.Employees
{
    /// <summary>
    /// This class provide methods to Add, Update, Retrive, Delete Skills details
    /// </summary>
    public class SkillsDetails
    {
        #region Private Member Variables

        /// <summary>
        /// private variable for Class Name used in each catch block
        /// </summary>
        private string CLASS_NAME = "SkillsDetails";

        /// <summary>
        /// private variable for Data Access Class
        /// </summary>
        private DataAccessClass objDA;

        /// <summary>
        /// private array variable for Sql paramaters
        /// </summary>
        private SqlParameter[] sqlParam;

        /// <summary>
        /// private object for SqlDataReader
        /// </summary>
        private SqlDataReader objDataReader;

        /// <summary>
        /// private variable for Skills
        /// </summary>
        private BusinessEntities.SkillsDetails objSkillsDetails;

        /// <summary>
        /// private variable for Skill Search Report
        /// </summary>
        private BusinessEntities.SkillSearch objSkillSearchDetails;

        /// <summary>
        /// private object for RaveHRCollection
        /// </summary>
        private BusinessEntities.RaveHRCollection raveHRCollection;

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
        private string GETMANAGERSEMAILID = "GetManagersEmailId";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string SKILLSEARCHDETAILS = "SkillSearchDetails";

        #endregion Private Member Variables

        #region Public Member Functions

        /// <summary>
        /// Adds the skills details.
        /// </summary>
        /// <param name="objAddSkillsDetails">The object add skills details.</param>
        public void AddSkillsDetails(BusinessEntities.SkillsDetails objAddSkillsDetails)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[8];

            try
            {
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[0].Value = objAddSkillsDetails.EMPId;

                sqlParam[1] = new SqlParameter(SPParameter.Skill, SqlDbType.Int);
                if (objAddSkillsDetails.Skill == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objAddSkillsDetails.Skill;


                sqlParam[2] = new SqlParameter(SPParameter.Proficiency, SqlDbType.Int);
                if (objAddSkillsDetails.Proficiency == 0)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objAddSkillsDetails.Proficiency;

                sqlParam[3] = new SqlParameter(SPParameter.LastUsedDate, SqlDbType.Int);
                if (objAddSkillsDetails.LastUsed == 0)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = objAddSkillsDetails.LastUsed;

                sqlParam[4] = new SqlParameter(SPParameter.SkillVersion, SqlDbType.NVarChar,50);
                if (
                        objAddSkillsDetails.SkillVersion == null || 
                        objAddSkillsDetails.SkillVersion == string.Empty || 
                        objAddSkillsDetails.SkillVersion == "&nbsp;" ||
                        objAddSkillsDetails.SkillVersion == "&amp;nbsp;"            
                    )
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = objAddSkillsDetails.SkillVersion;

                sqlParam[5] = new SqlParameter(SPParameter.ExperienceInYear, SqlDbType.Int);
                sqlParam[5].Value = objAddSkillsDetails.Year;

                sqlParam[6] = new SqlParameter(SPParameter.ExperienceInMonth, SqlDbType.Int);
                sqlParam[6].Value = objAddSkillsDetails.Month;

                sqlParam[7] = new SqlParameter(SPParameter.SkillName, SqlDbType.NVarChar,50);
                if (objAddSkillsDetails.SkillName == null)
                    sqlParam[7].Value = DBNull.Value;
                else
                    sqlParam[7].Value = objAddSkillsDetails.SkillName;
                objDA.ExecuteNonQuerySP(SPNames.Employee_AddSkillsDetails, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, ADDSKILLSDETAILS, EventIDConstants.RAVE_EMP_SKILL_SEARCH_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Updates the skills details.
        /// </summary>
        /// <param name="objUpdateSkillsDetails">The object update skills details.</param>
        public void UpdateSkillsDetails(BusinessEntities.SkillsDetails objUpdateSkillsDetails)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[9];

            try
            {
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.SkillsId, SqlDbType.Int);
                sqlParam[0].Value = objUpdateSkillsDetails.SkillsId;

                sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[1].Value = objUpdateSkillsDetails.EMPId;

                sqlParam[2] = new SqlParameter(SPParameter.Skill, SqlDbType.Int);
                if (objUpdateSkillsDetails.Skill == 0)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objUpdateSkillsDetails.Skill;
 
                sqlParam[3] = new SqlParameter(SPParameter.Proficiency, SqlDbType.Int);
                if (objUpdateSkillsDetails.Proficiency == 0)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = objUpdateSkillsDetails.Proficiency;

                sqlParam[4] = new SqlParameter(SPParameter.LastUsedDate, SqlDbType.Int);
                if (objUpdateSkillsDetails.LastUsed == 0)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = objUpdateSkillsDetails.LastUsed;

                sqlParam[5] = new SqlParameter(SPParameter.SkillVersion, SqlDbType.NVarChar,50);
                if (
                        objUpdateSkillsDetails.SkillVersion == null ||
                        objUpdateSkillsDetails.SkillVersion == string.Empty ||
                        objUpdateSkillsDetails.SkillVersion == "&nbsp;"  
                    )
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = objUpdateSkillsDetails.SkillVersion;

                sqlParam[6] = new SqlParameter(SPParameter.ExperienceInYear, SqlDbType.Int);
                sqlParam[6].Value = objUpdateSkillsDetails.Year;

                sqlParam[7] = new SqlParameter(SPParameter.ExperienceInMonth, SqlDbType.Int);
                sqlParam[7].Value = objUpdateSkillsDetails.Month;

                sqlParam[8] = new SqlParameter(SPParameter.SkillName, SqlDbType.NVarChar, 50);
                if (objUpdateSkillsDetails.SkillName == null)
                    sqlParam[8].Value = DBNull.Value;
                else
                    sqlParam[8].Value = objUpdateSkillsDetails.SkillName;

                objDA.ExecuteNonQuerySP(SPNames.Employee_UpdateSkillsDetails, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, UPDATESKILLSDETAILS, EventIDConstants.RAVE_EMP_SKILL_SEARCH_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Gets the skills details.
        /// </summary>
        /// <param name="objGetSkillsDetails">The object get skills details.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetSkillsDetails(BusinessEntities.SkillsDetails objGetSkillsDetails)
        {
            //Initialise Data Access Class object
            objDA = new DataAccessClass();

            //Initialise SqlParameter Class object
            sqlParam = new SqlParameter[1];

            //Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection(); 

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Check each parameters nullibality and add values to sqlParam object accordingly
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objGetSkillsDetails.EMPId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objGetSkillsDetails.EMPId;

                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetSkillsDetails, sqlParam);

                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    objSkillsDetails = new BusinessEntities.SkillsDetails();

                    objSkillsDetails.SkillsId = Convert.ToInt32(objDataReader[DbTableColumn.SId].ToString());
                    objSkillsDetails.EMPId = Convert.ToInt32(objDataReader[DbTableColumn.EMPId].ToString());
                    objSkillsDetails.Skill = Convert.ToInt32(objDataReader[DbTableColumn.SkillId].ToString());
                    objSkillsDetails.SkillVersion = objDataReader[DbTableColumn.SkillVersion].ToString();
                    objSkillsDetails.SkillName = objDataReader[DbTableColumn.Skill].ToString();
                    objSkillsDetails.Month = Convert.ToInt32(objDataReader[DbTableColumn.ExperienceInMonth].ToString());
                    objSkillsDetails.Year = Convert.ToInt32(objDataReader[DbTableColumn.ExperienceInYear].ToString());
                    objSkillsDetails.Proficiency = Convert.ToInt32(objDataReader[DbTableColumn.ProficiencyLevel].ToString());
                    objSkillsDetails.ProficiencyLevel = objDataReader[DbTableColumn.Proficiency].ToString();
                    objSkillsDetails.LastUsed = Convert.ToInt32(objDataReader[DbTableColumn.LastUsedDate].ToString());                  

                    // Add the object to Collection
                    raveHRCollection.Add(objSkillsDetails);                    
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETSKILLSDETAILS, EventIDConstants.RAVE_EMP_SKILL_SEARCH_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDA.CloseConncetion();
            }
            return raveHRCollection;
        }

        /// <summary>
        /// Deletes the skills details.
        /// </summary>
        /// <param name="objDeleteSkillsDetails">The object delete skills details.</param>
        public void DeleteSkillsDetails(BusinessEntities.SkillsDetails objDeleteSkillsDetails)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[2];

            try
            {
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.SkillsId, SqlDbType.Int);
                if (objDeleteSkillsDetails.SkillsId== 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objDeleteSkillsDetails.SkillsId;

                sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objDeleteSkillsDetails.EMPId == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objDeleteSkillsDetails.EMPId;

                objDA.ExecuteNonQuerySP(SPNames.Employee_DeleteSkillsDetails, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, DELETESKILLSDETAILS, EventIDConstants.RAVE_EMP_SKILL_SEARCH_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        public string GetManagersEmailId(int empid)
        {
            //Initialise Data Access Class object
            objDA = new DataAccessClass();

            //Initialise SqlParameter Class object
            sqlParam = new SqlParameter[1];

            //Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            string EmailIds = string.Empty;

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Check each parameters nullibality and add values to sqlParam object accordingly
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (empid == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = empid;

                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetManagerEmailId, sqlParam);

                //Read the data and assign to Collection object
                while (objDataReader.Read())
                {
                    EmailIds = EmailIds + objDataReader[DbTableColumn.EmailId].ToString() + ", ";
                }

                EmailIds = EmailIds.Remove(EmailIds.Length - 2, 1);

                // Return the Collection
                return EmailIds;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETMANAGERSEMAILID, EventIDConstants.RAVE_EMP_SKILL_SEARCH_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Function will fill all the dropdowns
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Collection</returns>
        public BusinessEntities.RaveHRCollection FillDropDownsDL(int EmpId)
        {
            //Declare DataAccess Class Object
            objDA = new DataAccessClass();
            SqlParameter[] sqlParam = new SqlParameter[1];

            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[0].Value = EmpId;

                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetSkillData, sqlParam);

                while (objDataReader.Read())
                {
                    KeyValue<string> keyValue = new KeyValue<string>();
                    keyValue.KeyName = objDataReader.GetValue(1).ToString();
                    keyValue.Val = objDataReader.GetValue(0).ToString();
                    raveHRCollection.Add(keyValue);
                }
                return raveHRCollection;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Adds the employee skill details.
        /// </summary>
        /// <param name="EmpId">The emp id.</param>
        /// <returns></returns>
        public void AddPrimarySkillDetails(int EmpId, string PrimarySkills)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[2];

            try
            {
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[0].Value = EmpId;

                sqlParam[1] = new SqlParameter(SPParameter.PrimarySkillId, SqlDbType.NVarChar, 50);
                if (PrimarySkills == "" || PrimarySkills == null)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = PrimarySkills;

                objDA.ExecuteNonQuerySP(SPNames.Employee_AddSkillData, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, ADDSKILLSDETAILS, EventIDConstants.RAVE_EMP_SKILL_SEARCH_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }


        /// <summary>
        /// Function will fill all the dropdowns
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Collection</returns>
        /// 

        //done on 9 April 2010
        public BusinessEntities.RaveHRCollection GetSkillTypes()
        {
            //Declare DataAccess Class Object
            objDA = new DataAccessClass();
            
            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetEmployeeSkillType);

                while (objDataReader.Read())
                {
                    KeyValue<string> keyValue = new KeyValue<string>();
                    keyValue.KeyName = objDataReader.GetValue(0).ToString();
                    keyValue.Val = objDataReader.GetValue(1).ToString();
                    raveHRCollection.Add(keyValue);
                }
                return raveHRCollection;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDA.CloseConncetion();
            }
        }

        //done on 9 April 2010
        public BusinessEntities.RaveHRCollection GetSkillTypesCategory()
        {
            //Declare DataAccess Class Object
            objDA = new DataAccessClass();

            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetEmployeeSkillTypeCategory);

                while (objDataReader.Read())
                {
                    KeyValue<string> keyValue = new KeyValue<string>();
                    keyValue.KeyName = objDataReader.GetValue(0).ToString();
                    keyValue.Val = objDataReader.GetValue(1).ToString();
                    raveHRCollection.Add(keyValue);
                }
                return raveHRCollection;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDA.CloseConncetion();
            }
        }

        //Umesh: NIS-changes: Skill Search Report Starts
        public BusinessEntities.RaveHRCollection GetPrimaryAndSecondarySkills()
        {
            objDA = new DataAccessClass();
            try
            {
                BusinessEntities.RaveHRCollection raveHRCollection = new BusinessEntities.RaveHRCollection();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());
                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetPrimaryAndSecondarySkills);

                while (objDataReader.Read())
                {
                    KeyValue<string> keyValue = new KeyValue<string>();
                    keyValue.KeyName = objDataReader.GetValue(0).ToString();
                    keyValue.Val = objDataReader.GetValue(0).ToString();
                    raveHRCollection.Add(keyValue);
                }
                return raveHRCollection;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }
                objDA.CloseConncetion();
            }
        }

        public DataTable GetSkillSearchDetails(BusinessEntities.SkillSearch objSkillSearchCriteria, string SortExpressionAndDirection)
        {
            //Initialise Data Access Class object
            objDA = new DataAccessClass();
            
            //Initialise SqlParameter Class object
            sqlParam = new SqlParameter[3];

            //Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();
            DataSet ds;

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Check each parameters nullibality and add values to sqlParam object accordingly
                sqlParam[0] = new SqlParameter(SPParameter.MandatorySkills, SqlDbType.VarChar);
                sqlParam[0].Value = objSkillSearchCriteria.MandatorySkills;

                sqlParam[1] = new SqlParameter(SPParameter.OptionalSkills, SqlDbType.VarChar);
                sqlParam[1].Value = objSkillSearchCriteria.OptionalSkills;

                //Siddharth 26 March 2015 Start
                // Parameter Sort Expression And Direction
                sqlParam[2] = new SqlParameter(SPParameter.SortExpression, SqlDbType.VarChar, 50);
                if (SortExpressionAndDirection == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = SortExpressionAndDirection;
                //Siddharth 26 March 2015 End

                ds = new DataSet();
                ds = objDA.GetDataSet(SPNames.Employee_GetSkillSearchDetails, sqlParam);

                
                //objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetSkillSearchDetails, sqlParam);

                //while (objDataReader.Read())
                //{
                //    //Initialise the Business Entity object
                //    objSkillSearchDetails = new BusinessEntities.SkillSearch();

                //    objSkillSearchDetails.EmployeeName = objDataReader[DbTableColumn.EmployeeName].ToString();
                //    objSkillSearchDetails.Designation = objDataReader[DbTableColumn.Designation].ToString();
                //    objSkillSearchDetails.Department = objDataReader[DbTableColumn.Department].ToString();
                //    objSkillSearchDetails.ProjectsAllocated = objDataReader[DbTableColumn.ProjectsAllocated].ToString();
                //    objSkillSearchDetails.PrimarySkill = objDataReader[DbTableColumn.PrimarySkills].ToString();
                //    objSkillSearchDetails.SkillName = objDataReader[DbTableColumn.SecondarySkills].ToString();
                //    objSkillSearchDetails.SkillVersion = objDataReader[DbTableColumn.SkillVersion].ToString();
                    
                //    if (objDataReader[DbTableColumn.ExperienceInMonth].ToString() != string.Empty)
                //        objSkillSearchDetails.ExpInMonths = Convert.ToInt32(objDataReader[DbTableColumn.ExperienceInMonth].ToString());
                    
                //    objSkillSearchDetails.Proficiency = objDataReader[DbTableColumn.Proficiency].ToString();
                    
                //    if (objDataReader[DbTableColumn.LastUsedDate].ToString() != string.Empty)
                //        objSkillSearchDetails.LastUsed = Convert.ToInt32(objDataReader[DbTableColumn.LastUsedDate].ToString());

                //    // Add the object to Collection
                //    raveHRCollection.Add(objSkillSearchDetails);
                //}
                //if (!objDataReader.IsClosed)
                //{
                //    objDataReader.Close();
                //}
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, SKILLSEARCHDETAILS, EventIDConstants.RAVE_EMP_SKILL_SEARCH_DATA_ACCESS_LAYER);
            }
            finally
            {
                //if (objDataReader != null)
                //{
                //    objDataReader.Close();
                //}

                objDA.CloseConncetion();
            }
            return ds.Tables[0];
        }
        //Umesh: NIS-changes: Skill Search Report Ends

        //Siddhesh Arekar Domain Details 09032015 Start
        public bool Check_SkillCategory_Exists(string skillCategory)
        {
            bool isExists = false;
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[2];
            try
            {

                    objDA.OpenConnection(DBConstants.GetDBConnectionString());

                    sqlParam[0] = new SqlParameter(SPParameter.SkillCategory, skillCategory);
                    sqlParam[1] = new SqlParameter(SPParameter.IsSkillExist, isExists);
                    sqlParam[1].Direction = ParameterDirection.Output;
                    objDA.ExecuteNonQuerySP(SPNames.CheckSkillCategory, sqlParam);
                    isExists = (bool)sqlParam[1].Value;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "Check_SkillCategory_Exists", EventIDConstants.RAVE_HR_EMPLOYEE_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
            return isExists;
        }
        //Siddhesh Arekar Domain Details 09032015 Start
        #endregion
    }
}
