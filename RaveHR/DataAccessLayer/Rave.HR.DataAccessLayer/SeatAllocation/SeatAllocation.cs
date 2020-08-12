//------------------------------------------------------------------------------
//  Copyright (c) Rave Technologies Pvt. Ltd. 2009 all rights reserved.
//
//  File:           SeatAllocation.cs       
//  Author:         Kanchan.Singh
//  Date written:   19/11/2009 2:00:00 PM
//  Description:    This page serves as the Data access layer for the Seat allocation module.
//
//  Amendments
//  Date                   Who               Ref      Description
//  ----                   -----------       ---      -----------
//  19/11/2009 2:00:00 PM  Kanchan.Singh     n/a      Created    
//
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Common;
using Common.Constants;
using BusinessEntities;


namespace Rave.HR.DataAccessLayer.SeatAllocation
{
    public class SeatAllocation
    {
        #region Local member Declaration

        BusinessEntities.RaveHRCollection raveHRCollection = null;

        const string CLASSNAME = "SeatAllocation.cs";

        const string SHIFTLOCATION = "ShiftLocation";

        const string ALLOCATE = "Allocate";

        bool result = false;

        BusinessEntities.SeatAllocation BESeatInformation = new BusinessEntities.SeatAllocation();

        #endregion Local member Declaration

        #region Public Methodes
        /// <summary>
        /// This method will fetch records of seats from data base and return to business layer
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>

        public RaveHRCollection GetSeatDetails(BusinessEntities.SeatAllocation SeatSection)
        {
            DataAccessClass objDASeatAllocation = new DataAccessClass();
            List<BusinessEntities.SeatAllocation> objListSeatAlloctaion = null;

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();
            try
            {
                objDASeatAllocation.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter(SPParameter.SectionID, DbType.Int32);
                sqlParam[0].Value = SeatSection.SectionID;

                sqlParam[1] = new SqlParameter(SPParameter.BranchID, DbType.Int32);
                sqlParam[1].Value = SeatSection.RaveBranchID;

                //--get result
                DataSet dsSeatDescription = objDASeatAllocation.GetDataSet(SPNames.SeatAllocation_GetSeatDetailsForBay, sqlParam);

                //--Create entities and add to list
                BusinessEntities.SeatAllocation objBESeatDetail = null;
                objListSeatAlloctaion = new List<BusinessEntities.SeatAllocation>();

                foreach (DataRow dr in dsSeatDescription.Tables[0].Rows)
                {
                    objBESeatDetail = new BusinessEntities.SeatAllocation();
                    if (dr[DbTableColumn.Seat_SectionID].ToString() != null)
                    {
                        objBESeatDetail.SectionID = Convert.ToInt32(dr[DbTableColumn.Seat_SectionID]);
                    }
                    if (dr[DbTableColumn.Seat_BayID].ToString() != null)
                    {
                        objBESeatDetail.BayID = Convert.ToInt32(dr[DbTableColumn.Seat_BayID]);
                    }
                    if (dr[DbTableColumn.Seat_SeatID].ToString() != null)
                    {
                        objBESeatDetail.SeatID = Convert.ToInt32(dr[DbTableColumn.Seat_SeatID]);
                    }
                    if (dr[DbTableColumn.Seat_SeatName].ToString() != null)
                    {
                        objBESeatDetail.SeatName = dr[DbTableColumn.Seat_SeatName].ToString();
                    }
                    if (dr[DbTableColumn.Seat_EmployeeID].ToString() != string.Empty)
                    {
                        objBESeatDetail.EmployeeID = Convert.ToInt32(dr[DbTableColumn.Seat_EmployeeID]);
                    }
                    else
                    {
                        objBESeatDetail.EmployeeID = 0;
                    }
                    if (dr[DbTableColumn.Seat_EmployeeName].ToString() != string.Empty)
                    {
                        objBESeatDetail.EmployeeName = dr[DbTableColumn.Seat_EmployeeName].ToString();
                    }                   
                    //--add to list
                    objListSeatAlloctaion.Add(objBESeatDetail);
                }

                raveHRCollection.Add(objListSeatAlloctaion);
                return raveHRCollection;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, "GetSeatDetails", EventIDConstants.RAVE_HR_SEATALLOCATION_DATA_ACCESS_LAYER);
            }
            finally
            {

                objDASeatAllocation.CloseConncetion();

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="Destination"></param>
        /// <returns></returns>
        public bool ShiftLocation(BusinessEntities.SeatAllocation Source, BusinessEntities.SeatAllocation Destination)
        {
            DataAccessClass objDASeatAllocation = new DataAccessClass();
            try
            {
                bool result = false;

                //Opens the connection.
                objDASeatAllocation.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter(SPParameter.SourceLocation, DbType.Int32);
                sqlParam[0].Value = Source.SeatID;
                sqlParam[1] = new SqlParameter(SPParameter.Destination, DbType.Int32);
                sqlParam[1].Value = Destination.SeatID;
                //sqlParam[2] = new SqlParameter(SPParameter.EmployeeID, DbType.Int32);
                //sqlParam[2].Value = Destination.EmployeeID;

                //update changes in the database.
                int shift = objDASeatAllocation.ExecuteNonQuerySP(SPNames.SeatAllocation_ShiftLocation, sqlParam);

                if (shift != 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                return result;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, SHIFTLOCATION, EventIDConstants.RAVE_HR_SEATALLOCATION_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDASeatAllocation.CloseConncetion();
            }
        }

        public BusinessEntities.SeatAllocation GetEmployeeDetailsAtSeat(BusinessEntities.SeatAllocation SeatID)
        {
            DataAccessClass objDASeatAllocation = new DataAccessClass();
            try
            {
                objDASeatAllocation.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.SeatID, DbType.Int32);
                sqlParam[0].Value = SeatID.SeatID;

                //--get result
                DataSet dsSeatDescription = objDASeatAllocation.GetDataSet(SPNames.SeatAllocation_GetEmployeeDeatilsAtSeat, sqlParam);

                //--Create entities and add to list
                BusinessEntities.SeatAllocation objBESeatDetail = new BusinessEntities.SeatAllocation();

                foreach (DataRow dr in dsSeatDescription.Tables[0].Rows)
                {
                    if (dr[DbTableColumn.Seat_EmployeeName].ToString() != string.Empty)
                    {
                        objBESeatDetail.EmployeeName = dr[DbTableColumn.Seat_EmployeeName].ToString();
                    }
                    if (dr[DbTableColumn.Seat_EmployeeCode].ToString() != string.Empty)
                    {
                        objBESeatDetail.EmployeeCode = dr[DbTableColumn.Seat_EmployeeCode].ToString();
                    }
                    if (dr[DbTableColumn.Seat_DepartmentName].ToString() != string.Empty)
                    {
                        objBESeatDetail.DepartmentName = dr[DbTableColumn.Seat_DepartmentName].ToString();
                    }
                    if (dr[DbTableColumn.Seat_ProjectName].ToString() != string.Empty)
                    {
                        objBESeatDetail.ProjectName = dr[DbTableColumn.Seat_ProjectName].ToString();
                    }
                    if (dr[DbTableColumn.Seat_ExtentionNo].ToString() != string.Empty)
                    {
                        objBESeatDetail.ExtensionNo = Convert.ToInt32(dr[DbTableColumn.Seat_ExtentionNo]);
                    }
                    if (dr[DbTableColumn.Seat_Landmark].ToString() != string.Empty)
                    {
                        objBESeatDetail.SeatLandmark = dr[DbTableColumn.Seat_Landmark].ToString();
                    }
                    if (dr[DbTableColumn.Seat_EMPEmailID].ToString() != string.Empty)
                    {
                        objBESeatDetail.EmployeeEmailID = dr[DbTableColumn.Seat_EMPEmailID].ToString();
                    }
                }
                return objBESeatDetail;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, "GetSeatDetails", EventIDConstants.RAVE_HR_SEATALLOCATION_DATA_ACCESS_LAYER);
            }
            finally
            {

                objDASeatAllocation.CloseConncetion();

            }
        }

        public bool SaveEmpDetails(BusinessEntities.SeatAllocation seat)
        {
            DataAccessClass DADetails = new DataAccessClass();
            try
            {
                

                DADetails.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter(SPParameter.SeatID, DbType.Int32);
                sqlParam[0].Value = seat.SeatID;
                sqlParam[1] = new SqlParameter(SPParameter.ExtNo, DbType.Int32);
                sqlParam[1].Value = seat.ExtensionNo;
                sqlParam[2] = new SqlParameter(SPParameter.Landmark, DbType.String);
                sqlParam[2].Value = seat.SeatLandmark;

                //update changes in the database.
                int shift = DADetails.ExecuteNonQuerySP(SPNames.SeatAllocation_SaveEmployeeDetails, sqlParam);

                if (shift != 0)
                {
                    result = true;
                }

                return result;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, "SaveEmpDetails", EventIDConstants.RAVE_HR_CONTRACT_DATA_ACCESS_LAYER);
            }
            finally
            {
                DADetails.CloseConncetion();
            }
        }

        public BusinessEntities.SeatAllocation GetEmployeeName(BusinessEntities.SeatAllocation SeatID)
        {
            DataAccessClass objDASeatAllocation = new DataAccessClass();
            try
            {
                objDASeatAllocation.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.EmpCode, DbType.String);
                sqlParam[0].Value = SeatID.EmployeeCode;

                //--get result
                DataSet dsDescription = objDASeatAllocation.GetDataSet(SPNames.SeatAllocation_GetEmpName, sqlParam);

                //--Create entities and add to list
                BusinessEntities.SeatAllocation objBESeatDetail = new BusinessEntities.SeatAllocation();

                foreach (DataRow dr in dsDescription.Tables[0].Rows)
                {
                    if (dr[DbTableColumn.Seat_EmployeeID].ToString() != string.Empty)
                    {
                        objBESeatDetail.EmployeeID = Convert.ToInt32(dr[DbTableColumn.Seat_EmployeeID]);
                    }
                    if (dr[DbTableColumn.Seat_EmployeeName].ToString() != string.Empty)
                    {
                        objBESeatDetail.EmployeeName = dr[DbTableColumn.Seat_EmployeeName].ToString();
                    }
                    if (dr[DbTableColumn.Seat_EmployeeCode].ToString() != string.Empty)
                    {
                        objBESeatDetail.EmployeeCode = dr[DbTableColumn.Seat_EmployeeCode].ToString();
                    }                   
                    if (dr[DbTableColumn.Seat_EMPEmailID].ToString() != string.Empty)
                    {
                        objBESeatDetail.EmployeeEmailID = dr[DbTableColumn.Seat_EMPEmailID].ToString();
                    }
                }
                return objBESeatDetail;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, "GetEmployeeName", EventIDConstants.RAVE_HR_SEATALLOCATION_DATA_ACCESS_LAYER);
            }
            finally
            {

                objDASeatAllocation.CloseConncetion();

            }
        }

        public bool Allocate(BusinessEntities.SeatAllocation Seat)
        {
            DataAccessClass objDASeatAllocation = new DataAccessClass();
            try
            {
                bool result = false;

                //Opens the connection.
                objDASeatAllocation.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter(SPParameter.SeatID, DbType.Int32);
                sqlParam[0].Value = Seat.SeatID;
                sqlParam[1] = new SqlParameter(SPParameter.EmployeeID, DbType.Int32);
                sqlParam[1].Value = Seat.EmployeeID;
                
                //update changes in the database.
                int shift = objDASeatAllocation.ExecuteNonQuerySP(SPNames.SeatAllocation_Allocate, sqlParam);

                if (shift != 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                return result;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, ALLOCATE, EventIDConstants.RAVE_HR_SEATALLOCATION_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDASeatAllocation.CloseConncetion();
            }
        }

        /// <summary>
        /// To check employee location.
        /// </summary>>
        public BusinessEntities.SeatAllocation CheckEmployeeLocation(int employeeId)
        {
            BusinessEntities.SeatAllocation objSeatAllocation = null;
            SqlDataReader objReader = null; ;
            DataAccessClass objDASeatAllocation = new DataAccessClass();
            try
            {
                objDASeatAllocation.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.EmployeeID, DbType.Int32);
                sqlParam[0].Value = employeeId;

                objReader = objDASeatAllocation.ExecuteReaderSP(SPNames.SeatAllocation_CheckEmployeeLocation, sqlParam);
                
                while (objReader.Read())
                {
                    objSeatAllocation = new BusinessEntities.SeatAllocation();
                    objSeatAllocation.EmployeeID = int.Parse(objReader[DbTableColumn.EMPId].ToString());
                    objSeatAllocation.EmployeeName = objReader[DbTableColumn.EmployeeName].ToString();
                    objSeatAllocation.SeatID = int.Parse(objReader[DbTableColumn.Seat_SeatID].ToString());
                    objSeatAllocation.SeatName = objReader[DbTableColumn.Seat_SeatName].ToString();
                }

                return objSeatAllocation;
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, "CheckEmployeeLocation", EventIDConstants.RAVE_HR_SEATALLOCATION_DATA_ACCESS_LAYER);
            }
            //close datareader and connection
            finally
            {
                //checks if datareader is null
                if (!objReader.IsClosed)
                {
                    //close datareader
                    objReader.Close();
                }

                //close connection
                objDASeatAllocation.CloseConncetion();
            }
        }

        /// <summary>
        /// gets the Employee details as per the ID.
        /// </summary>
        /// <param name="EmpID"></param>
        /// <returns></returns>
        public BusinessEntities.SeatAllocation GetEmployeeDetailsByID(BusinessEntities.SeatAllocation EmpID)
        {
            DataAccessClass objDASeatAllocation = new DataAccessClass();
            try
            {
                objDASeatAllocation.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.EmployeeID, DbType.Int32);
                sqlParam[0].Value = EmpID.EmployeeID;

                //--get result
                DataSet dsSeatDescription = objDASeatAllocation.GetDataSet(SPNames.SeatAllocation_GetEmployeeDetailsByID, sqlParam);

                //--Create entities and add to list
                BusinessEntities.SeatAllocation objBESeatDetail = new BusinessEntities.SeatAllocation();

                foreach (DataRow dr in dsSeatDescription.Tables[0].Rows)
                {
                    if (dr[DbTableColumn.Seat_EmployeeCode].ToString() != string.Empty)
                    {
                        objBESeatDetail.EmployeeCode = dr[DbTableColumn.Seat_EmployeeCode].ToString();
                    } 
                    if (dr[DbTableColumn.Seat_EmployeeCode].ToString() != string.Empty)
                    {
                        objBESeatDetail.EmployeeCode = dr[DbTableColumn.Seat_EmployeeCode].ToString();
                    } 
                    if (dr[DbTableColumn.Seat_EmployeeName].ToString() != string.Empty)
                    {
                        objBESeatDetail.EmployeeName = dr[DbTableColumn.Seat_EmployeeName].ToString();
                    }                    
                    if (dr[DbTableColumn.Seat_EMPEmailID].ToString() != string.Empty)
                    {
                        objBESeatDetail.EmployeeEmailID = dr[DbTableColumn.Seat_EMPEmailID].ToString();
                    }
                }
                return objBESeatDetail;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, "GetEmployeeDetailsByID", EventIDConstants.RAVE_HR_SEATALLOCATION_DATA_ACCESS_LAYER);
            }
            finally
            {

                objDASeatAllocation.CloseConncetion();

            }
        }
        
        /// <summary>
        /// gets the Seat details as per the ID.
        /// </summary>
        /// <param name="EmpID"></param>
        /// <returns></returns>
        public BusinessEntities.SeatAllocation GetSeatDeatilsByID(BusinessEntities.SeatAllocation Seat)
        {
            DataAccessClass objDASeatAllocation = new DataAccessClass();
            try
            {
                objDASeatAllocation.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.SeatID, DbType.Int32);
                sqlParam[0].Value = Seat.SeatID;

                //--get result
                DataSet dsSeatDescription = objDASeatAllocation.GetDataSet(SPNames.SeatAllocation_GetSeatDetailsByID, sqlParam);

                //--Create entities and add to list
                BusinessEntities.SeatAllocation objBESeatDetail = new BusinessEntities.SeatAllocation();

                foreach (DataRow dr in dsSeatDescription.Tables[0].Rows)
                {
                    if (dr[DbTableColumn.Seat_SeatName].ToString() != string.Empty)
                    {
                        objBESeatDetail.SeatName = dr[DbTableColumn.Seat_SeatName].ToString();
                    }
                    if (dr[DbTableColumn.Seat_BayID].ToString() != string.Empty)
                    {
                        objBESeatDetail.BayID = Convert.ToInt32(dr[DbTableColumn.Seat_BayID]);
                    }
                    if (dr[DbTableColumn.Seat_Description].ToString() != string.Empty)
                    {
                        objBESeatDetail.SeatDescription = dr[DbTableColumn.Seat_Description].ToString();
                    }
                    if (dr[DbTableColumn.Seat_ExtentionNo].ToString() != string.Empty)
                    {
                        objBESeatDetail.ExtensionNo = Convert.ToInt32(dr[DbTableColumn.Seat_ExtentionNo]);
                    }
                    if (dr[DbTableColumn.Seat_Landmark].ToString() != string.Empty)
                    {
                        objBESeatDetail.SeatLandmark = dr[DbTableColumn.Seat_Landmark].ToString();
                    }
                    if (dr[DbTableColumn.Seat_EmployeeID].ToString() != string.Empty)
                    {
                        objBESeatDetail.EmployeeID = Convert.ToInt32(dr[DbTableColumn.Seat_EmployeeID].ToString());
                    }
                }
                return objBESeatDetail;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, "GetEmployeeDetailsByID", EventIDConstants.RAVE_HR_SEATALLOCATION_DATA_ACCESS_LAYER);
            }
            finally
            {

                objDASeatAllocation.CloseConncetion();

            }
        }

        /// <summary>
        /// Deallocate the employee from seat.
        /// </summary>
        /// <returns></returns>
        public List<BusinessEntities.SeatAllocation> UnallocatedEmployee()
        {
            DataAccessClass objDASeatAllocation = new DataAccessClass();
            List<BusinessEntities.SeatAllocation> objListSeatAlloctaion = null;

            // Initialise Collection class object
            raveHRCollection = new BusinessEntities.RaveHRCollection();
            try
            {
                objDASeatAllocation.OpenConnection(DBConstants.GetDBConnectionString());
                

                //--get result
                DataSet dsSeatDescription = objDASeatAllocation.GetDataSet(SPNames.SeatAllocation_GetUnallocatedEmployee);

                //--Create entities and add to list
                BusinessEntities.SeatAllocation objBESeatDetail = null;
                objListSeatAlloctaion = new List<BusinessEntities.SeatAllocation>();

                foreach (DataRow dr in dsSeatDescription.Tables[0].Rows)
                {
                    objBESeatDetail = new BusinessEntities.SeatAllocation();

                    if (dr[DbTableColumn.Seat_EmployeeID].ToString() != string.Empty)
                    {
                        objBESeatDetail.EmployeeID =Convert.ToInt32(dr[DbTableColumn.Seat_EmployeeID]);
                    }
                    if (dr[DbTableColumn.Seat_EmployeeCode].ToString() != string.Empty)
                    {
                        objBESeatDetail.EmployeeCode = dr[DbTableColumn.Seat_EmployeeCode].ToString();
                    }
                    if (dr[DbTableColumn.Seat_EmployeeName].ToString() != string.Empty)
                    {
                        objBESeatDetail.EmployeeName = dr[DbTableColumn.Seat_EmployeeName].ToString();
                    }
                    if (dr[DbTableColumn.Seat_EMPEmailID].ToString() != string.Empty)
                    {
                        objBESeatDetail.EmployeeEmailID = dr[DbTableColumn.Seat_EMPEmailID].ToString();
                    }

                    //--add to list
                    objListSeatAlloctaion.Add(objBESeatDetail);
                }


                return objListSeatAlloctaion;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, "UnallocatedEmployee", EventIDConstants.RAVE_HR_SEATALLOCATION_DATA_ACCESS_LAYER);
            }
            finally
            {

                objDASeatAllocation.CloseConncetion();

            }

        }
        
        /// <summary>
        /// get the branches of a section.
        /// </summary>
        /// <param name="branchID"></param>
        /// <returns></returns>
        public RaveHRCollection GetSectionByBranch(int branchID)
        {
            BusinessEntities.SeatAllocation objSeatAllocation = null;
            SqlDataReader objReader = null; ;
            DataAccessClass objDASeatAllocation = new DataAccessClass();
            try
            {
                raveHRCollection = new RaveHRCollection();
                objDASeatAllocation.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.BranchID, DbType.Int32);
                sqlParam[0].Value = branchID;

                objReader = objDASeatAllocation.ExecuteReaderSP(SPNames.SeatAllocation_GetSection, sqlParam);

                while (objReader.Read())
                {
                    objSeatAllocation = new BusinessEntities.SeatAllocation();
                    objSeatAllocation.SectionID = int.Parse(objReader[DbTableColumn.Seat_SectionID].ToString());
                    objSeatAllocation.SectionName = objReader[DbTableColumn.Seat_SectionName].ToString();
                    raveHRCollection.Add(objSeatAllocation);
                }

                return raveHRCollection;
            }
            //catches RaveHRException exception
            catch (RaveHRException ex)
            {
                throw ex;
            }
            //catches genral exception
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, "GetSectionByBranch", EventIDConstants.RAVE_HR_SEATALLOCATION_DATA_ACCESS_LAYER);
            }
            //close datareader and connection
            finally
            {
                //checks if datareader is null
                if (!objReader.IsClosed)
                {
                    //close datareader
                    objReader.Close();
                }

                //close connection
                objDASeatAllocation.CloseConncetion();
            }
        }

        /// <summary>
        /// Swaping seat location.
        /// </summary>
        /// <param name="sourceSeatId"> first seat location</param>
        /// <param name="destinationSeatId"> 2nd seat location</param>
        /// <returns></returns>
        public bool SwapLocation(BusinessEntities.SeatAllocation source, BusinessEntities.SeatAllocation destination)
        {
            DataAccessClass objDASeatAllocation = new DataAccessClass();
            try
            {
                bool result = false;

                //Opens the connection.
                objDASeatAllocation.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter(SPParameter.SourceLocation, DbType.Int32);
                sqlParam[0].Value = source.SeatID;
                sqlParam[1] = new SqlParameter(SPParameter.Destination, DbType.Int32);
                sqlParam[1].Value = destination.SeatID;
                
                //update changes in the database.
                int swap = objDASeatAllocation.ExecuteNonQuerySP(SPNames.SeatAllocation_SwapLocation, sqlParam);

                if (swap != 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                return result;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, CLASSNAME, SHIFTLOCATION, EventIDConstants.RAVE_HR_SEATALLOCATION_DATA_ACCESS_LAYER);
            }

            finally
            {
                objDASeatAllocation.CloseConncetion();
            }
        }

        #endregion Public Methodes
    }
}
