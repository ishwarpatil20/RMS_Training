using System;
using System.Data;
using System.Data.SqlClient;
using Common;
using Common.Constants;

namespace Rave.HR.DataAccessLayer.Employees
{
    public class ContactDetails
    {

        #region Private Member Variables

        /// <summary>
        /// private variable for Class Name used in each catch block
        /// </summary>
        private string CLASS_NAME = "ContactDetails";

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
        /// private object for Qualification
        /// </summary>
        private BusinessEntities.ContactDetails objContactDetails;

        /// <summary>
        /// private object for RaveHRCollection
        /// </summary>
        private BusinessEntities.RaveHRCollection raveHRCollection;

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string ADDCONTACTDETAILS = "AddContactDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string UPDATECONTACTDETAILS = "UpdateContactDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string GETCONTACTDETAILS = "GetContactDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string DELETECONTACTDETAILS = "DeleteContactDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string GETSEATDETAILS = "GetSeatDetails";

        /// <summary>
        /// private string variable for Method Name used in each catch block
        /// </summary>
        private string ALLOCATESEAT = "AllocateSeat";

        #endregion Private Member Variables

        #region Public Member Functions

        /// <summary>
        /// Adds the contact details.
        /// </summary>
        /// <param name="objAddContactDetails">The obj add contact details.</param>
        public void AddContactDetails(BusinessEntities.ContactDetails objAddContactDetails)
        {
            //Initialise Data Access Class object
            objDA = new DataAccessClass();

            //Initialise SqlParameter Class object
            sqlParam = new SqlParameter[9];

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Check each parameters nullibality and add values to sqlParam object accordingly
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objAddContactDetails.EMPId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objAddContactDetails.EMPId;

                sqlParam[1] = new SqlParameter(SPParameter.CityCode, SqlDbType.NChar, 10);
                if (objAddContactDetails.CityCode == 0)
                    sqlParam[1].Value = DBNull.Value; 
                else
                    sqlParam[1].Value = objAddContactDetails.CityCode;

                sqlParam[2] = new SqlParameter(SPParameter.CountryCode, SqlDbType.Int);
                if (objAddContactDetails.CountryCode == 0)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objAddContactDetails.CountryCode;

                sqlParam[3] = new SqlParameter(SPParameter.ContactNo, SqlDbType.NChar, 10);
                if (objAddContactDetails.ContactNo == "" || objAddContactDetails.ContactNo == null)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = objAddContactDetails.ContactNo;

                sqlParam[4] = new SqlParameter(SPParameter.Extension, SqlDbType.Int);
                if (objAddContactDetails.Extension == 0)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = objAddContactDetails.Extension;

                sqlParam[5] = new SqlParameter(SPParameter.AvalibilityTime, SqlDbType.NChar, 50);
                if (objAddContactDetails.AvalibilityTime == "" || objAddContactDetails.AvalibilityTime == null)
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = objAddContactDetails.AvalibilityTime;

                sqlParam[6] = new SqlParameter(SPParameter.ContactType, SqlDbType.Int);
                if (objAddContactDetails.ContactType == 0)
                    sqlParam[6].Value = DBNull.Value;
                else
                    sqlParam[6].Value = objAddContactDetails.ContactType;

                sqlParam[7] = new SqlParameter(SPParameter.CreatedById, SqlDbType.NChar, 50);
                if (objAddContactDetails.CreatedById == "" || objAddContactDetails.CreatedById == null)
                    sqlParam[7].Value = DBNull.Value;
                else
                    sqlParam[7].Value = objAddContactDetails.CreatedById;

                sqlParam[8] = new SqlParameter(SPParameter.CreatedDate, SqlDbType.DateTime);  
                if (objAddContactDetails.CreatedDate == null)
                    sqlParam[8].Value = DBNull.Value;
                else
                    sqlParam[8].Value = objAddContactDetails.CreatedDate;

                //Execute SP along with proper parameters
                objDA.ExecuteNonQuerySP(SPNames.Employee_AddContact, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, ADDCONTACTDETAILS, EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Updates the qualification details.
        /// </summary>
        /// <param name="objUpdateContactDetails">The object update qualification details.</param>
        public void UpdateContactDetails(BusinessEntities.ContactDetails objUpdateContactDetails)
        {
            //Initialise Data Access Class object
            objDA = new DataAccessClass();

            //Initialise SqlParameter Class object
            sqlParam = new SqlParameter[10];

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Check each parameters nullibality and add values to sqlParam object accordingly
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objUpdateContactDetails.EMPId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objUpdateContactDetails.EMPId;

                sqlParam[1] = new SqlParameter(SPParameter.EmpContactId, SqlDbType.Int);
                if (objUpdateContactDetails.EmployeeContactId == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objUpdateContactDetails.EmployeeContactId;

                sqlParam[2] = new SqlParameter(SPParameter.CityCode, SqlDbType.NChar, 10);
                if (objUpdateContactDetails.CityCode == 0)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objUpdateContactDetails.CityCode;

                sqlParam[3] = new SqlParameter(SPParameter.CountryCode, SqlDbType.Int);
                if (objUpdateContactDetails.CountryCode == 0)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = objUpdateContactDetails.CountryCode;

                sqlParam[4] = new SqlParameter(SPParameter.ContactNo, SqlDbType.NChar, 10);
                if (objUpdateContactDetails.ContactNo == "" || objUpdateContactDetails.ContactNo == null)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = objUpdateContactDetails.ContactNo;

                sqlParam[5] = new SqlParameter(SPParameter.Extension, SqlDbType.Int);
                if (objUpdateContactDetails.Extension == 0)
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = objUpdateContactDetails.Extension;

                sqlParam[6] = new SqlParameter(SPParameter.AvalibilityTime, SqlDbType.NChar, 50);
                if (objUpdateContactDetails.AvalibilityTime == "" || objUpdateContactDetails.AvalibilityTime == null)
                    sqlParam[6].Value = DBNull.Value;
                else
                    sqlParam[6].Value = objUpdateContactDetails.AvalibilityTime;

                sqlParam[7] = new SqlParameter(SPParameter.ContactType, SqlDbType.Int);
                if (objUpdateContactDetails.ContactType == 0)
                    sqlParam[7].Value = DBNull.Value;
                else
                    sqlParam[7].Value = objUpdateContactDetails.ContactType;

                sqlParam[8] = new SqlParameter(SPParameter.LastModifiedById, SqlDbType.NChar, 50);
                if (objUpdateContactDetails.LastModifiedById == "" || objUpdateContactDetails.LastModifiedById == null)
                    sqlParam[8].Value = DBNull.Value;
                else
                    sqlParam[8].Value = objUpdateContactDetails.LastModifiedById;

                sqlParam[9] = new SqlParameter(SPParameter.LastModifiedDate, SqlDbType.DateTime);
                if (objUpdateContactDetails.LastModifiedDate == null || objUpdateContactDetails.LastModifiedDate== DateTime.MinValue)
                    sqlParam[9].Value = DBNull.Value;
                else
                    sqlParam[9].Value = objUpdateContactDetails.LastModifiedDate;

                //Execute SP along with proper parameters
                objDA.ExecuteNonQuerySP(SPNames.Employee_UpdateContact, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, UPDATECONTACTDETAILS, EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Gets the qualification details.
        /// </summary>
        /// <param name="objGetContactDetails">The object get qualification details.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetContactDetails(BusinessEntities.ContactDetails objGetContactDetails)
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
                if (objGetContactDetails.EMPId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objGetContactDetails.EMPId;

                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetContact, sqlParam);

                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    objContactDetails = new BusinessEntities.ContactDetails();

                    objContactDetails.EmployeeContactId = Convert.ToInt32(objDataReader[DbTableColumn.EmpContactId].ToString());
                    objContactDetails.EMPId = Convert.ToInt32(objDataReader[DbTableColumn.EMPId].ToString());
                    objContactDetails.CityCode = objDataReader[DbTableColumn.CityCode].ToString() == string.Empty ? 0 : int.Parse(objDataReader[DbTableColumn.CityCode].ToString());
                    objContactDetails.CountryCode = objDataReader[DbTableColumn.CountryCode].ToString() == string.Empty ? 0: int.Parse(objDataReader[DbTableColumn.CountryCode].ToString());
                    objContactDetails.ContactNo = objDataReader[DbTableColumn.ContactNo].ToString().Trim();
                    objContactDetails.Extension = objDataReader[DbTableColumn.Extension].ToString() == string.Empty ? 0 : int.Parse(objDataReader[DbTableColumn.Extension].ToString());
                    objContactDetails.AvalibilityTime = objDataReader[DbTableColumn.AvalibilityTime].ToString().Trim();
                    objContactDetails.ContactType = int.Parse(objDataReader[DbTableColumn.ContactType].ToString());
                    //objContactDetails.IsActive = Convert.ToBoolean(objDataReader[DbTableColumn.IsActive].ToString());
                 
                     objContactDetails.ContactTypeName = objDataReader[DbTableColumn.ContactTypeName].ToString().Trim();
                    

                    // Add the object to Collection
                    raveHRCollection.Add(objContactDetails);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETCONTACTDETAILS, EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDA.CloseConncetion();
            }
            // Return the Collection
            return raveHRCollection;
        }

        /// <summary>
        /// Deletes the contact details.
        /// </summary>
        /// <param name="objDeleteContactDetails">The obj delete contact details.</param>
        public void DeleteContactDetails(BusinessEntities.ContactDetails objDeleteContactDetails)
        {
            //Initialise Data Access Class object
            objDA = new DataAccessClass();

            //Initialise SqlParameter Class object
            sqlParam = new SqlParameter[2];

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Check each parameters nullibality and add values to sqlParam object accordingly
                sqlParam[0] = new SqlParameter(SPParameter.EmpContactId, SqlDbType.Int);
                if (objDeleteContactDetails.EmployeeContactId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objDeleteContactDetails.EmployeeContactId;

                sqlParam[1] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objDeleteContactDetails.EMPId == 0)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objDeleteContactDetails.EMPId;

                //Execute SP along with proper parameters
                objDA.ExecuteNonQuerySP(SPNames.Employee_DeleteContact, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, DELETECONTACTDETAILS, EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Gets the seat details.
        /// </summary>
        /// <param name="objGetContactDetails">The obj get contact details.</param>
        /// <returns></returns>
        public string GetSeatDetails(BusinessEntities.ContactDetails objGetContactDetails)
        {
            //Initialise Data Access Class object
            objDA = new DataAccessClass();

            //Initialise SqlParameter Class object
            sqlParam = new SqlParameter[1];

            string sname = string.Empty;

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Check each parameters nullibality and add values to sqlParam object accordingly
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                if (objGetContactDetails.EMPId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objGetContactDetails.EMPId;

                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_GetSeatDetails, sqlParam);

                while (objDataReader.Read())
                {
                    sname = objDataReader[DbTableColumn.Seat_SeatName].ToString(); 
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, GETSEATDETAILS, EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDA.CloseConncetion();
            }
            // Return the Collection
            return sname;
        }

        /// <summary>
        /// Allocates the seat.
        /// </summary>
        /// <param name="objGetContactDetails">The obj get contact details.</param>
        /// <returns></returns>
        public int AllocateSeat(BusinessEntities.ContactDetails objGetContactDetails)
        {
            //Initialise Data Access Class object
            objDA = new DataAccessClass();

            //Initialise SqlParameter Class object
            sqlParam = new SqlParameter[3];

            int Status=0;

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Check each parameters nullibality and add values to sqlParam object accordingly
                sqlParam[0] = new SqlParameter(SPParameter.EmployeeID, SqlDbType.Int);
                if (objGetContactDetails.EMPId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objGetContactDetails.EMPId;

                sqlParam[1] = new SqlParameter(SPParameter.SeatName, SqlDbType.NVarChar,50);
                if (objGetContactDetails.SeatName == "" || objGetContactDetails.SeatName== null)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objGetContactDetails.SeatName;

                sqlParam[2] = new SqlParameter(SPParameter.Status, SqlDbType.Int);
                sqlParam[2].Value = 0;
                sqlParam[2].Direction = ParameterDirection.Output;

                objDataReader = objDA.ExecuteReaderSP(SPNames.Employee_AllocateSeat, sqlParam);

                Status = int.Parse(sqlParam[2].Value.ToString());
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASS_NAME, ALLOCATESEAT, EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objDataReader != null)
                {
                    objDataReader.Close();
                }

                objDA.CloseConncetion();
            }
            // Return the status
            return Status;
        }


        #endregion Public Member Functions
    }
}  
