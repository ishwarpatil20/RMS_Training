//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           ProejctSummary.aspx.cs       
//  Author:         sudip.guha
//  Date written:   13/09/2009/ 12:30:30 PM
//  Description:    This class  provides the Data Access layer methods for OrganisationDetails in  Employee module.
//                  
//
//  Amendments
//  Date                        Who             Ref     Description
//  ----                        -----------     ---     -----------
//  13/09/2009/ 12:30:30 PM     sudip.guha    n/a     Created    
//  17/08/2009/ 04:30:30 PM     Shrinivas Dalavi n/a     Used DataAccessClass,Add() Update() Delete() Get()
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Data.SqlClient;
using Common;
using Common.Constants;

namespace Rave.HR.DataAccessLayer.Employees
{
    public class OrganisationDetails
    {
        #region Private Field Members

        private string CLASS_NAME = "OrganisationDetails.cs";

        /// <summary>
        /// private variable for Data Access Class
        /// </summary>
        private DataAccessClass objDA;

        /// <summary>
        /// private array variable for Sql paramaters
        /// </summary>
        private SqlParameter[] sqlParam;

        SqlDataReader objDataReader;

        private BusinessEntities.OrganisationDetails objOrganisationDetails;

        BusinessEntities.RaveHRCollection raveHRCollection;

        string MONTHS = "Months";
        string YEARS = "Years";

        #endregion

        #region Public Member Functions

        /// <summary>
        /// Adds the organisation details.
        /// </summary>
        /// <param name="objAddOrganisationDetails">The obj add organisation details.</param>
        public void AddOrganisationDetails(BusinessEntities.OrganisationDetails objAddOrganisationDetails)
        {
            try
            {
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[10];
                 
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[0].Value = objAddOrganisationDetails.EMPId;

                sqlParam[1] = new SqlParameter(SPParameter.CompanyName, SqlDbType.NChar, 50);
                if (objAddOrganisationDetails.CompanyName == "" || objAddOrganisationDetails.CompanyName == null)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objAddOrganisationDetails.CompanyName;

                sqlParam[2] = new SqlParameter(SPParameter.MonthSince, SqlDbType.Int);
                sqlParam[2].Value = objAddOrganisationDetails.MonthSince;

                sqlParam[3] = new SqlParameter(SPParameter.YearSince, SqlDbType.Int);
                sqlParam[3].Value = objAddOrganisationDetails.YearSince;

                sqlParam[4] = new SqlParameter(SPParameter.MonthTill, SqlDbType.Int);
                sqlParam[4].Value = objAddOrganisationDetails.MonthTill;

                sqlParam[5] = new SqlParameter(SPParameter.YearTill, SqlDbType.Int);
                sqlParam[5].Value = objAddOrganisationDetails.YearTill;
                
                sqlParam[6] = new SqlParameter(SPParameter.Designation, SqlDbType.NChar,50);
                if (objAddOrganisationDetails.Designation == "" || objAddOrganisationDetails.Designation == null)
                    sqlParam[6].Value = DBNull.Value;
                else
                    sqlParam[6].Value = objAddOrganisationDetails.Designation;

                sqlParam[7] = new SqlParameter(SPParameter.ExperienceType, SqlDbType.Int, 10);
                sqlParam[7].Value = objAddOrganisationDetails.ExperienceType;

                sqlParam[8] = new SqlParameter(SPParameter.ExperienceInMonth, SqlDbType.Int);
                sqlParam[8].Value = objAddOrganisationDetails.ExperienceMonth;

                sqlParam[9] = new SqlParameter(SPParameter.ExperienceInYear, SqlDbType.Int);
                sqlParam[9].Value = objAddOrganisationDetails.ExperienceYear;
             

                int AddProfessinalDetails = objDA.ExecuteNonQuerySP(SPNames.Employee_AddOrganisationDetails, sqlParam);

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "AddOrganisationDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Updates the organisation details.
        /// </summary>
        /// <param name="objUpdateOrganisationDetails">The obj update organisation details.</param>
        public void UpdateOrganisationDetails(BusinessEntities.OrganisationDetails objUpdateOrganisationDetails)
        {
            try
            {
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[11];

                sqlParam[0] = new SqlParameter(SPParameter.OrganisationId, SqlDbType.Int);
                sqlParam[0].Value = objUpdateOrganisationDetails.OrganisationId;

                sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[1].Value = objUpdateOrganisationDetails.EMPId;

                sqlParam[2] = new SqlParameter(SPParameter.CompanyName, SqlDbType.NChar, 50);
                if (objUpdateOrganisationDetails.CompanyName == "" || objUpdateOrganisationDetails.CompanyName == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objUpdateOrganisationDetails.CompanyName;

                sqlParam[3] = new SqlParameter(SPParameter.MonthSince, SqlDbType.Int);
                sqlParam[3].Value = objUpdateOrganisationDetails.MonthSince;

                sqlParam[4] = new SqlParameter(SPParameter.YearSince, SqlDbType.Int);
                sqlParam[4].Value = objUpdateOrganisationDetails.YearSince;

                sqlParam[5] = new SqlParameter(SPParameter.MonthTill, SqlDbType.Int);
                sqlParam[5].Value = objUpdateOrganisationDetails.MonthTill;

                sqlParam[6] = new SqlParameter(SPParameter.YearTill, SqlDbType.Int);
                sqlParam[6].Value = objUpdateOrganisationDetails.YearTill;

                sqlParam[7] = new SqlParameter(SPParameter.Designation, SqlDbType.NChar, 50);
                if (objUpdateOrganisationDetails.Designation == "" || objUpdateOrganisationDetails.Designation == null)
                    sqlParam[7].Value = DBNull.Value;
                else
                    sqlParam[7].Value = objUpdateOrganisationDetails.Designation;

                sqlParam[8] = new SqlParameter(SPParameter.ExperienceType, SqlDbType.Int, 10);
                sqlParam[8].Value = objUpdateOrganisationDetails.ExperienceType;

                sqlParam[9] = new SqlParameter(SPParameter.ExperienceInMonth, SqlDbType.Int);
                sqlParam[9].Value = objUpdateOrganisationDetails.ExperienceMonth;

                sqlParam[10] = new SqlParameter(SPParameter.ExperienceInYear, SqlDbType.Int);
                sqlParam[10].Value = objUpdateOrganisationDetails.ExperienceYear;

                int UpdateProfessinalDetails = objDA.ExecuteNonQuerySP(SPNames.Employee_UpdateOrganisationDetails, sqlParam);

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "UpdateOrganisationDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Gets the organisation details.
        /// </summary>
        /// <param name="objGetOrganisationDetails">The obj get organisation details.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetOrganisationDetails(BusinessEntities.OrganisationDetails objGetOrganisationDetails)
        {
            // Initialise Data Access Class object
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[2];

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objGetOrganisationDetails.EMPId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objGetOrganisationDetails.EMPId;

                sqlParam[1] = new SqlParameter(SPParameter.ExperienceType, SqlDbType.Int);
                if (objGetOrganisationDetails.ExperienceType == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objGetOrganisationDetails.ExperienceType;

                //Execute the SP
                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetOrganisationDetails, sqlParam);


                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    objOrganisationDetails = new BusinessEntities.OrganisationDetails();

                    objOrganisationDetails.OrganisationId = int.Parse(objDataReader[DbTableColumn.OId].ToString());
                    objOrganisationDetails.EMPId = int.Parse(objDataReader[DbTableColumn.EMPId].ToString());
                    objOrganisationDetails.CompanyName = objDataReader[DbTableColumn.CompanyName].ToString();
                    objOrganisationDetails.MonthSince = int.Parse(objDataReader[DbTableColumn.MonthSince].ToString());
                    objOrganisationDetails.YearSince = int.Parse(objDataReader[DbTableColumn.YearSince].ToString());
                    objOrganisationDetails.MonthTill = int.Parse(objDataReader[DbTableColumn.MonthTill].ToString());
                    objOrganisationDetails.YearTill = int.Parse(objDataReader[DbTableColumn.YearTill].ToString());
                    objOrganisationDetails.Designation = objDataReader[DbTableColumn.Designation].ToString();
                    objOrganisationDetails.MonthSinceName = objDataReader[DbTableColumn.MonthSinceName].ToString();
                    objOrganisationDetails.MonthTillName = objDataReader[DbTableColumn.MonthTillName].ToString();
                    objOrganisationDetails.WorkingSince = objOrganisationDetails.MonthSinceName + "-" + objOrganisationDetails.YearSince.ToString();
                    objOrganisationDetails.WorkingTill = objOrganisationDetails.MonthTillName + "-" + objOrganisationDetails.YearTill.ToString();
                    objOrganisationDetails.ExperienceMonth = objDataReader[DbTableColumn.ExperienceInMonth].ToString() == string.Empty ? 0 : int.Parse(objDataReader[DbTableColumn.ExperienceInMonth].ToString());
                    objOrganisationDetails.ExperienceYear = objDataReader[DbTableColumn.ExperienceInYear].ToString() == string.Empty ? 0 : int.Parse(objDataReader[DbTableColumn.ExperienceInYear].ToString());                   
                    objOrganisationDetails.Experience = objOrganisationDetails.ExperienceYear + " " + YEARS + "-" + objOrganisationDetails.ExperienceMonth + " " + MONTHS;

                    // Add the object to Collection
                    raveHRCollection.Add(objOrganisationDetails);
                }

                // Return the Collection
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetProfessionalDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
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
        /// Deletes the organisation details.
        /// </summary>
        /// <param name="objDeleteOrganisationDetails">The obj delete organisation details.</param>
        public void DeleteOrganisationDetails(BusinessEntities.OrganisationDetails objDeleteOrganisationDetails)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[2];

            try
            {
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.OrganisationId, SqlDbType.Int);
                if (objDeleteOrganisationDetails.OrganisationId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objDeleteOrganisationDetails.OrganisationId;

                sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objDeleteOrganisationDetails.EMPId == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objDeleteOrganisationDetails.EMPId;

                objDA.ExecuteNonQuerySP(SPNames.Employee_DeleteOrganisationDetails, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "DeleteOrganisationDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Deletes the non organisation details.
        /// </summary>
        /// <param name="objDeleteOrganisationDetails">The obj delete organisation details.</param>
        public void DeleteNonOrganisationDetails(BusinessEntities.OrganisationDetails objDeleteOrganisationDetails)
        {
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[2];

            try
            {
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objDeleteOrganisationDetails.EMPId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objDeleteOrganisationDetails.EMPId;

                sqlParam[1] = new SqlParameter(SPParameter.ExperienceType, SqlDbType.Int);
                if (objDeleteOrganisationDetails.ExperienceType == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objDeleteOrganisationDetails.ExperienceType;

                objDA.ExecuteNonQuerySP(SPNames.Employee_DeleteNonOrganisationDetails, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "DeleteOrganisationDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Gets the relevant experience.
        /// </summary>
        /// <param name="objGetOrganisationDetails">The obj get organisation details.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetRelevantExperience(BusinessEntities.OrganisationDetails objGetOrganisationDetails)
        {
            // Initialise Data Access Class object
            objDA = new DataAccessClass();
            sqlParam = new SqlParameter[1];

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objGetOrganisationDetails.EMPId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objGetOrganisationDetails.EMPId;

                //Execute the SP
                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetRelevantExperience, sqlParam);


                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    objOrganisationDetails = new BusinessEntities.OrganisationDetails();
                    objOrganisationDetails.ExperienceMonth = objDataReader[DbTableColumn.ExperienceInMonth].ToString() == string.Empty ? 0: int.Parse(objDataReader[DbTableColumn.ExperienceInMonth].ToString());
                    objOrganisationDetails.ExperienceYear = objDataReader[DbTableColumn.ExperienceInYear].ToString() ==string.Empty ? 0: int.Parse(objDataReader[DbTableColumn.ExperienceInYear].ToString());                   

                    // Add the object to Collection
                    raveHRCollection.Add(objOrganisationDetails);
                }

                // Return the Collection
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "GetRelevantExperience", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
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

        // 28109-Ambar-Start
        // Added New Method
        public void UpdateTotalReleventExp(int AemployeeID, int ATotalMonths, int ATotalYears)//, int AReleventMonths, int AReleventYears)
        {
          objDA = new DataAccessClass();
          sqlParam = new SqlParameter[3];

          try
          {
            objDA.OpenConnection(DBConstants.GetDBConnectionString());

            sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);            
            sqlParam[0].Value = AemployeeID;

            sqlParam[1] = new SqlParameter(SPParameter.PTotalMonths, SqlDbType.Int);
            sqlParam[1].Value = ATotalMonths;

            sqlParam[2] = new SqlParameter(SPParameter.PTotalYears, SqlDbType.Int);
            sqlParam[2].Value = ATotalYears;

            //sqlParam[3] = new SqlParameter(SPParameter.PReleventMonths, SqlDbType.Int);
            //sqlParam[3].Value = AReleventMonths;

            //sqlParam[4] = new SqlParameter(SPParameter.PReleventYears, SqlDbType.Int);
            //sqlParam[4].Value = AReleventYears;

            objDA.ExecuteNonQuerySP(SPNames.Employee_UpdateTotalReleventExp, sqlParam);
          }
          catch (RaveHRException ex)
          {
            throw ex;
          }
          catch (Exception ex)
          {
            throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, "UpdateTotalReleventExp", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
          }
          finally
          {
            objDA.CloseConncetion();
          }

        }
      // 28109-Ambar-End

        #endregion

    }
}
