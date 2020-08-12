using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using Common;
using Common.Constants;


namespace Rave.HR.DataAccessLayer.Contracts
{
    public class Contract
    {
        #region
        int parentContractId;

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
        /// object for Contract Business entity.
        /// </summary>
        BusinessEntities.Contract objcontract = null;

        /// <summary>
        /// object for Contract Project Business entity.
        /// </summary>
        BusinessEntities.ContractProject objcontractproject = null;

        /// <summary>
        /// private object for RaveHRCollection
        /// </summary>
        private BusinessEntities.RaveHRCollection raveHRCollection;


        /// <summary>
        /// private object for objCRDetails
        /// </summary>
        private BusinessEntities.Contract objCRDetails;

        List<BusinessEntities.ContractProject> lstProjectDetails = null;

        const string Contracts = "Contracts.cs";

        const string DELETE = "delete";

        const string EDIT = "edit";
        const string GETEMAILID = "GetEmailID";
        const string VIEWCONTRACTPROJECT = "ViewContractProjectDetails";
        const string VIEWCONTRACT = "ViewContractDetails";
        const string GETCONTRACTS = "GetContracts";
        const string SAVE = "Save";

        #endregion

        #region Public Method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addContract"></param>
        /// <returns></returns>
        public int Save(BusinessEntities.Contract addContract)
        {

            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;
            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.Contract_AddContract, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[18];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.ContractReferenceID, addContract.ContractReferenceID);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.ContractType, addContract.ContractType);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.DocumentName, addContract.DocumentName);
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.ContractStatus, addContract.ContractStatus);
                sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.AccountManagerID, addContract.AccountManagerID);
                sqlParam[5] = objCommand.Parameters.AddWithValue(SPParameter.EmailID, addContract.EmailID);
                sqlParam[6] = objCommand.Parameters.AddWithValue(SPParameter.ClientName, addContract.ClientName);
                sqlParam[7] = objCommand.Parameters.AddWithValue(SPParameter.LocationID, addContract.LocationID);
                sqlParam[8] = objCommand.Parameters.AddWithValue(SPParameter.ParentContractID, parentContractId);
                sqlParam[9] = objCommand.Parameters.AddWithValue(SPParameter.CreatedById, addContract.CreatedByEmailId);
                sqlParam[10] = objCommand.Parameters.AddWithValue(SPParameter.CreatedDate, DateTime.Now);
                sqlParam[11] = objCommand.Parameters.AddWithValue(SPParameter.ContractValue, addContract.ContractValue);
                sqlParam[12] = objCommand.Parameters.AddWithValue(SPParameter.CurrencyType, addContract.CurrencyType);
                sqlParam[13] = objCommand.Parameters.AddWithValue(SPParameter.Division, addContract.Division);
                sqlParam[14] = objCommand.Parameters.AddWithValue(SPParameter.Sponsor, addContract.Sponsor);
                sqlParam[15] = objCommand.Parameters.AddWithValue(SPParameter.OutContractId, 0);
                sqlParam[15].Direction = ParameterDirection.Output;
                sqlParam[16] = objCommand.Parameters.AddWithValue(SPParameter.ContractStartDate, addContract.ContractStartDate);
                sqlParam[17] = objCommand.Parameters.AddWithValue(SPParameter.ContractEndDate, addContract.ContractEndDate);

                int contract = objCommand.ExecuteNonQuery();
                int ContractId = Convert.ToInt32(sqlParam[15].Value);


                return ContractId;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, Contracts, SAVE, EventIDConstants.RAVE_HR_CONTRACT_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        /// <summary>
        /// This method will fetch record based on criteria object from data base and return back to buisness layer.
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public List<BusinessEntities.Contract> GetContracts(BusinessEntities.ContractCriteria criteria)
        {
            int pageCount = 1;
            int counter = 0;
            DataAccessClass objDAContractsForProject = new DataAccessClass();
            try
            {
                //Getting List in which data will hold
                List<BusinessEntities.Contract> contractList = new List<BusinessEntities.Contract>();
                DataSet dsContractsForProject = null;
                objDAContractsForProject.OpenConnection(DBConstants.GetDBConnectionString());
                //At loading firstTime
                if (criteria.Mode == 1)
                {
                    SqlParameter[] sqlParam = new SqlParameter[2];
                    sqlParam[0] = new SqlParameter(SPParameter.ContractId, DbType.Int32);
                    sqlParam[0].Value = criteria.ContractId;
                    sqlParam[1] = new SqlParameter(SPParameter.Mode, DbType.Int32);
                    sqlParam[1].Value = criteria.Mode;
                    //--get result
                    dsContractsForProject = objDAContractsForProject.GetDataSet(SPNames.Contract_GetContract, sqlParam);
                }

                //At the time of filtering 
                else
                {
                    SqlParameter[] sqlParam = new SqlParameter[8];

                    sqlParam[0] = new SqlParameter(SPParameter.SortExpression, DbType.String);
                    if (!string.IsNullOrEmpty(criteria.SortExpression))
                    {
                        if (criteria.SortExpression == CommonConstants.CON_CONTRACTCODE)
                        {
                            sqlParam[0].Value = criteria.SortExpression + criteria.Direction;
                        }
                        else
                        {
                            sqlParam[0].Value = criteria.SortExpression + criteria.Direction + "," +
                                CommonConstants.CON_CONTRACTCODE + criteria.Direction;
                        }
                    }
                    else
                    {
                        sqlParam[0].Value = CommonConstants.CON_CONTRACTCODE + " ASC";
                    }

                    sqlParam[1] = new SqlParameter(SPParameter.pageNum, DbType.Int32);
                    sqlParam[1].Value = criteria.PageNumber;


                    sqlParam[2] = new SqlParameter(SPParameter.@DocumentName, DbType.String);
                    if (!string.IsNullOrEmpty(criteria.DocumentName))
                    {
                        sqlParam[2].Value = criteria.DocumentName;
                    }
                    else
                    {
                        sqlParam[2].Value = DBNull.Value;
                    }
                    sqlParam[3] = new SqlParameter(SPParameter.ContractType, DbType.Int32);
                    sqlParam[3].Value = criteria.ContractTypeID;

                    sqlParam[4] = new SqlParameter(SPParameter.ContractStatus, DbType.Int32);
                    sqlParam[4].Value = criteria.ContractStatus;

                    sqlParam[5] = new SqlParameter(SPParameter.pageSize, DbType.Int32);
                    sqlParam[5].Value = 10;

                    sqlParam[6] = new SqlParameter(SPParameter.ClientName, DbType.Int32);
                    sqlParam[6].Value = criteria.ClientNameId;

                    sqlParam[7] = new SqlParameter(SPParameter.pageCount, SqlDbType.Int);
                    sqlParam[7].Direction = ParameterDirection.Output;

                    //--get result
                    dsContractsForProject = objDAContractsForProject.GetDataSet(SPNames.Contract_GetContractInFilter, sqlParam);

                    //Get the pagecount from output parameter.
                    pageCount = Convert.ToInt32(sqlParam[7].Value);

                }

                //--Create entities and add to list
                BusinessEntities.Contract objBEContractsForProject = null;
                counter = 0;

                //assigning the values to contract object and add that object into list.
                foreach (DataRow dr in dsContractsForProject.Tables[0].Rows)
                {
                    counter++;

                    objBEContractsForProject = new BusinessEntities.Contract();
                    if (dr[DbTableColumn.Con_ContractId] != null)
                        objBEContractsForProject.ContractID = Convert.ToInt32(dr[DbTableColumn.Con_ContractId]);
                    if (dr[DbTableColumn.Con_ContractRefId] != null)
                        objBEContractsForProject.ContractReferenceID = dr[DbTableColumn.Con_ContractRefId].ToString();
                    if (dr[DbTableColumn.Con_ContractCode] != null)
                        objBEContractsForProject.ContractCode = dr[DbTableColumn.Con_ContractCode].ToString();
                    if (dr[DbTableColumn.Con_ContractName] != null)
                        objBEContractsForProject.ContractType = dr[DbTableColumn.Con_ContractName].ToString();
                    if (dr[DbTableColumn.Con_DocumentName] != null)
                        objBEContractsForProject.DocumentName = dr[DbTableColumn.Con_DocumentName].ToString();
                    if (dr[DbTableColumn.Con_FirstName] != null)
                        objBEContractsForProject.FirstName = dr[DbTableColumn.Con_FirstName].ToString() + " " + dr[DbTableColumn.Con_LastName].ToString();
                    if (dr[DbTableColumn.ClientName] != null)
                        objBEContractsForProject.ClientName = dr[DbTableColumn.ClientName].ToString();
                    if (dr[DbTableColumn.Con_ContractProjectId] != null)
                        objBEContractsForProject.ProjectId = Convert.ToInt32(dr[DbTableColumn.Con_ContractProjectId].ToString());
                    //Add page count to objBEContractsForProject.
                    if (counter == 1 && criteria.Mode != 1)
                    {
                        objBEContractsForProject.PageCount = pageCount;
                    }
                    //--add to list
                    contractList.Add(objBEContractsForProject);
                }

                //return fetched record
                return contractList;

            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, Contracts, GETCONTRACTS, EventIDConstants.RAVE_HR_CONTRACT_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDAContractsForProject.CloseConncetion();
            }
        }


        /// <summary>
        /// This function is used to retrieve Details of contract from database.
        /// </summary>
        /// <param name="objViewProject"></param>
        /// <returns>Dataset</returns>        

        public BusinessEntities.Contract ViewContractDetails(BusinessEntities.Contract objViewContract)
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            DataSet ds = null;

            try
            {

                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);

                objCommand = new SqlCommand(SPNames.Contract_ViewContracts, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue(SPParameter.ContractId, objViewContract.ContractID);
                ds = new DataSet();
                SqlDataAdapter objDataAdapter = new SqlDataAdapter(objCommand);
                objDataAdapter.Fill(ds);
                //return ds;
                objcontract = new BusinessEntities.Contract();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (dr[DbTableColumn.Con_ContractId] != null)
                    {
                        objcontract.ContractID = Convert.ToInt32(dr[DbTableColumn.Con_ContractId]);
                    }
                    if (dr[DbTableColumn.Con_ContractCode] != null)
                    {
                        objcontract.ContractCode = dr[DbTableColumn.Con_ContractCode].ToString();
                    }
                    if (dr[DbTableColumn.Con_ContractType] != null)
                    {
                        objcontract.ContractType = dr[DbTableColumn.Con_ContractType].ToString();
                    }
                    if (dr[DbTableColumn.Con_ContractRefId] != null)
                    {
                        objcontract.ContractReferenceID = dr[DbTableColumn.Con_ContractRefId].ToString();
                    }
                    if (dr[DbTableColumn.Con_AccountManagerId] != null)
                    {
                        objcontract.AccountManagerID = Convert.ToInt32(dr[DbTableColumn.Con_AccountManagerId]);
                    }
                    if (dr[DbTableColumn.Con_AccountManagerName] != null)
                    {
                        objcontract.AccountManagerName = dr[DbTableColumn.Con_AccountManagerName].ToString();
                    }
                    if (dr[DbTableColumn.Con_DocumentName] != null)
                    {
                        objcontract.DocumentName = dr[DbTableColumn.Con_DocumentName].ToString();
                    }
                    if (dr[DbTableColumn.Con_contractStatus] != null)
                    {
                        objcontract.ContractStatus = dr[DbTableColumn.Con_contractStatus].ToString();
                    }
                    if (dr[DbTableColumn.EmailId] != null)
                    {
                        objcontract.EmailID = dr[DbTableColumn.EmailId].ToString();
                    }
                    if (dr[DbTableColumn.ClientName] != null)
                    {
                        objcontract.ClientName = dr[DbTableColumn.ClientName].ToString();
                    }
                    if (dr[DbTableColumn.Con_LocationId].ToString() != "")
                    {
                        objcontract.LocationID = Convert.ToInt32(dr[DbTableColumn.Con_LocationId]);
                    }
                    if (dr[DbTableColumn.Con_Contractlocation] != null)
                    {
                        objcontract.LocationName = dr[DbTableColumn.Con_Contractlocation].ToString();
                    }
                    if (dr[DbTableColumn.Con_ContractValue] != null)
                    {
                        objcontract.ContractValue = Convert.ToDecimal(dr[DbTableColumn.Con_ContractValue]);
                    }
                    if (dr[DbTableColumn.Con_CurrencyType] != null)
                    {
                        objcontract.CurrencyType = Convert.ToInt32(dr[DbTableColumn.Con_CurrencyType]);
                    }
                    if (dr[DbTableColumn.Con_Division] != null)
                    {
                        objcontract.Division = Convert.ToString(dr[DbTableColumn.Con_Division]);
                    }
                    if (dr[DbTableColumn.Con_Sponsor] != null)
                    {
                        objcontract.Sponsor = Convert.ToString(dr[DbTableColumn.Con_Sponsor]);
                    }
                    if (dr[DbTableColumn.Con_ContractStartDate].ToString() != string.Empty)
                    {
                        objcontract.ContractStartDate = Convert.ToDateTime(dr[DbTableColumn.Con_ContractStartDate]);
                    }
                    if (dr[DbTableColumn.Con_ContractEndDate].ToString() != string.Empty)
                    {
                        objcontract.ContractEndDate = Convert.ToDateTime(dr[DbTableColumn.Con_ContractEndDate]);
                    }
                    if (dr[DbTableColumn.Con_ClientAbbreviation].ToString() != string.Empty)
                    {
                        objcontract.ClinetAbbrivation = dr[DbTableColumn.Con_ClientAbbreviation].ToString();
                    }
                    //Siddharth 13 March 2015 Start
                    if (dr[DbTableColumn.ProjectModel].ToString() != string.Empty)
                    {
                        objcontract.ProjectModel = dr["ProjectModel"].ToString();
                    }
                    //Siddharth 13 March 2015 End

                    //Siddharth 9 Sept 2015 Start
                    if (dr[DbTableColumn.BusinessVertical].ToString() != string.Empty)
                    {
                        objcontract.BusinessVertical = dr["BusinessVertical"].ToString();
                    }
                    //Siddharth 9 Sept 2015 Start
                }
                return objcontract;
            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, Contracts, VIEWCONTRACT, EventIDConstants.RAVE_HR_CONTRACT_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        /// <summary>
        /// This function is used to retrieve Details of a single project from database.
        /// </summary>
        /// <param name="objViewProject"></param>
        /// <returns>Dataset</returns>        

        public List<BusinessEntities.ContractProject> ViewContractProjectDetails(int ContractID)
        {
            //initialized connection object to null.
            SqlConnection objConnection = null;

            //initialized command object to null.
            SqlCommand objCommand = null;

            //Initialized dataset to null.
            DataSet ds = null;

            //Initialized the List Object of BusinessEntities.ContractProject type.
            lstProjectDetails = new List<BusinessEntities.ContractProject>();

            try
            {

                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);

                objCommand = new SqlCommand(SPNames.Contract_ViewContractsProjects, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue(SPParameter.ContractId, ContractID);
                ds = new DataSet();

                SqlDataAdapter objDataAdapter = new SqlDataAdapter(objCommand);
                objDataAdapter.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        objcontractproject = new BusinessEntities.ContractProject();

                        if (dr[DbTableColumn.Con_ContractProjectId] != null)
                        {
                            objcontractproject.ProjectID = Convert.ToInt32(dr[DbTableColumn.Con_ContractProjectId]);
                        }
                        if (dr[DbTableColumn.Con_ProjectCode] != null)
                        {
                            objcontractproject.ProjectCode = dr[DbTableColumn.Con_ProjectCode].ToString();
                        }
                        if (dr[DbTableColumn.ProjectType] != null)
                        {
                            objcontractproject.ProjectType = dr[DbTableColumn.ProjectType].ToString();
                        }
                        if (dr[DbTableColumn.Con_ProjectName] != null)
                        {
                            objcontractproject.ProjectName = dr[DbTableColumn.Con_ProjectName].ToString();
                        }
                        if (dr[DbTableColumn.Con_ResourceNo].ToString() != "")
                        {
                            objcontractproject.NoOfResources = Convert.ToDecimal(dr[DbTableColumn.Con_ResourceNo]);
                        }
                        if (dr[DbTableColumn.Con_StartDate].ToString() != "")
                        {
                            objcontractproject.ProjectStartDate = Convert.ToDateTime(dr[DbTableColumn.Con_StartDate]);
                        }
                        if (dr[DbTableColumn.Con_EndDate].ToString() != "")
                        {
                            objcontractproject.ProjectEndDate = Convert.ToDateTime(dr[DbTableColumn.Con_EndDate]);
                        }
                        if (dr[DbTableColumn.ProjectLocation] != null)
                        {
                            objcontractproject.ProjectLocationName = dr[DbTableColumn.ProjectLocation].ToString();
                        }
                        if (dr[DbTableColumn.Description] != null)
                        {
                            objcontractproject.ProjectsDescription = dr[DbTableColumn.Description].ToString();
                        }
                        if (dr[DbTableColumn.Con_StatusID] != null)
                        {
                            objcontractproject.StatusID = Convert.ToInt32(dr[DbTableColumn.Con_StatusID]);
                        }

                        if (dr[DbTableColumn.Con_ProjectCategoryID] != null)
                        {
                            objcontractproject.ProjectCategoryID = Convert.ToInt32(dr[DbTableColumn.Con_ProjectCategoryID].ToString());
                        }
                        if (dr[DbTableColumn.Con_ProjectCategoryName] != null)
                        {
                            objcontractproject.ProjectCategoryName = dr[DbTableColumn.Con_ProjectCategoryName].ToString();
                        }
                        // Mohamed : Issue 49791 : 15/09/2014 : Starts                        			  
                        // Desc : Add project group in Contract page            
                        if (dr[DbTableColumn.ProjectGroup] != null)
                        {
                            objcontractproject.ProjectGroup = dr[DbTableColumn.ProjectGroup].ToString();
                        }
                        // Mohamed : Issue 49791 : 15/09/2014 : Ends

                        // Mohamed : Issue  : 23/09/2014 : Starts                        			  
                        // Desc : Add Division, Business Area, Business Segment, Project Alias in Contract page -- NIS-RMS
                        if (dr[DbTableColumn.ProjectDivision] != null)
                        {
                            objcontractproject.ProjectDivision = dr[DbTableColumn.ProjectDivision].ToString() == "" ? 0 : Convert.ToInt32(dr[DbTableColumn.ProjectDivision].ToString());
                        }
                        if (dr[DbTableColumn.ProjectBusinessArea] != null)
                        {
                            objcontractproject.ProjectBussinessArea = dr[DbTableColumn.ProjectBusinessArea].ToString() == "" ? 0 : Convert.ToInt32(dr[DbTableColumn.ProjectBusinessArea].ToString());
                        }
                        if (dr[DbTableColumn.ProjectBusinessSegment] != null)
                        {
                            objcontractproject.ProjectBussinessSegment = dr[DbTableColumn.ProjectBusinessSegment].ToString() == "" ? 0 : Convert.ToInt32(dr[DbTableColumn.ProjectBusinessSegment].ToString());
                        }
                        //if (dr[DbTableColumn.ProjectAlias] != null)
                        //{
                        //    objcontractproject.ProjectAlias = dr[DbTableColumn.ProjectAlias].ToString();
                        //}
                        // Mohamed : Issue  : 23/09/2014 : Ends

                        //Siddharth 13 March 2015 Start
                        if (dr[DbTableColumn.ProjectModel] != null)
                        {
                            objcontractproject.ProjectModel = dr[DbTableColumn.ProjectModel].ToString();
                        }
                        else
                        {
                            objcontractproject.ProjectModel = null;
                        }
                        //Siddharth 13 March 2015 End

                        //Siddharth 9 Sept 2015 Start
                        if (dr[DbTableColumn.BusinessVertical] != null)
                        {
                            objcontractproject.BusinessVertical = dr[DbTableColumn.BusinessVertical].ToString();
                        }
                        else
                        {
                            objcontractproject.BusinessVertical = null;
                        }
                        //Siddharth 9 Sept 2015 End

                        lstProjectDetails.Add(objcontractproject);
                    }
                }

                return lstProjectDetails;

            }

            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, Contracts, VIEWCONTRACTPROJECT, EventIDConstants.RAVE_HR_CONTRACT_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }
        /// <summary>
        /// gets the email id for a employee.
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        public string GetEmailID(int empId)
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            DataSet ds = null;

            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);

                objCommand = new SqlCommand(SPNames.Contract_EmpEmailID, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue(SPParameter.EmpId, empId);

                objConnection.Open();

                string emailID = (objCommand.ExecuteScalar()).ToString();

                return emailID;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, Contracts, GETEMAILID, EventIDConstants.RAVE_HR_CONTRACT_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }

        }


        /// <summary>
        /// gets the email id for a employee.
        /// </summary>
        /// <param name="empId"></param>
        /// <returns></returns>
        public string GetEmailID(string empName)
        {
            string[] empNameArray = empName.Split(' ');

            string firstName = empNameArray[0];

            string lastName = empNameArray[empNameArray.Length - 1];

            SqlConnection objConnection = null;
            SqlCommand objCommand = null;

            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);

                objCommand = new SqlCommand(SPNames.USP_EMPLOYEE_GETEMAILID, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue(SPParameter.FirstName, firstName);
                objCommand.Parameters.AddWithValue(SPParameter.LastName, lastName);

                objConnection.Open();

                string emailID = string.Empty;
                objDataReader = objCommand.ExecuteReader();
                while (objDataReader.Read())
                {
                    emailID = objDataReader[DbTableColumn.EmailId].ToString();
                }

                return emailID;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, Contracts, GETEMAILID, EventIDConstants.RAVE_HR_CONTRACT_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }

        }


        /// <summary>
        /// updates the edited contract.
        /// </summary>
        /// <param name="addContract"></param>
        /// <returns></returns>
        public bool edit(BusinessEntities.Contract Contract)
        {
            bool result = false;

            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;
            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.Contract_EditContracts, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[16];
                sqlParam[0] = objCommand.Parameters.AddWithValue(SPParameter.ContractReferenceID, Contract.ContractReferenceID);
                sqlParam[1] = objCommand.Parameters.AddWithValue(SPParameter.ContractType, Contract.ContractType);
                sqlParam[2] = objCommand.Parameters.AddWithValue(SPParameter.DocumentName, Contract.DocumentName);
                sqlParam[3] = objCommand.Parameters.AddWithValue(SPParameter.ContractStatus, Contract.ContractStatus);
                sqlParam[4] = objCommand.Parameters.AddWithValue(SPParameter.AccountManagerID, Contract.AccountManagerID);
                sqlParam[5] = objCommand.Parameters.AddWithValue(SPParameter.EmailID, Contract.EmailID);
                sqlParam[6] = objCommand.Parameters.AddWithValue(SPParameter.ClientName, Contract.ClientName.ToString());
                sqlParam[7] = objCommand.Parameters.AddWithValue(SPParameter.LocationID, Contract.LocationID);
                sqlParam[8] = objCommand.Parameters.AddWithValue(SPParameter.ContractCode, Contract.ContractCode);
                sqlParam[9] = objCommand.Parameters.AddWithValue(SPParameter.ContractValue, Contract.ContractValue);
                sqlParam[10] = objCommand.Parameters.AddWithValue(SPParameter.CurrencyType, Contract.CurrencyType);
                sqlParam[11] = objCommand.Parameters.AddWithValue(SPParameter.CreatedById, Contract.CreatedByEmailId);
                sqlParam[12] = objCommand.Parameters.AddWithValue(SPParameter.Division, Contract.Division);
                sqlParam[13] = objCommand.Parameters.AddWithValue(SPParameter.Sponsor, Contract.Sponsor);
                sqlParam[14] = objCommand.Parameters.AddWithValue(SPParameter.ContractStartDate, Contract.ContractStartDate);
                sqlParam[15] = objCommand.Parameters.AddWithValue(SPParameter.ContractEndDate, Contract.ContractEndDate);

                int contract = objCommand.ExecuteNonQuery();
                if (contract != 0)
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
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, Contracts, EDIT, EventIDConstants.RAVE_HR_CONTRACT_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        /// <summary>
        /// Deletes the Contract details from the
        /// contracts table and saves it to the contract history table.
        /// </summary>
        /// <param name="contract"></param>
        public bool delete(BusinessEntities.Contract contract)
        {
            //creates a DataAccessClass object.
            DataAccessClass Contract = new DataAccessClass();
            try
            {
                bool result = false;

                //creates a new business entity.
                objcontract = new BusinessEntities.ContractProject();

                //Opens the connection.
                Contract.OpenConnection(DBConstants.GetDBConnectionString());

                //Declares the parameters for the sp.
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = new SqlParameter(SPParameter.ContractCode, DbType.String);
                sqlParam[0].Value = contract.ContractCode;
                sqlParam[1] = new SqlParameter(SPParameter.ReasonForDeletion, DbType.String);
                sqlParam[1].Value = contract.ReasonForDeletion;

                //update changes in the database.
                int delete = Contract.ExecuteNonQuerySP(SPNames.Contract_DeleteContract, sqlParam);

                result = true;

                return result;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, Contracts, DELETE, EventIDConstants.RAVE_HR_CONTRACT_DATA_ACCESS_LAYER);
            }
            finally
            {
                Contract.CloseConncetion();
            }

        }


        /// <summary>
        /// Adds the CR details.
        /// </summary>
        /// <param name="objAddCRDetails">The obj add CR details.</param>
        public void AddCRDetails(BusinessEntities.Contract objAddCRDetails)
        {
            //Initialise Data Access Class object
            objDA = new DataAccessClass();

            //Initialise SqlParameter Class object
            sqlParam = new SqlParameter[6];

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Check each parameters nullibality and add values to sqlParam object accordingly
                sqlParam[0] = new SqlParameter(SPParameter.ContractId, SqlDbType.Int);
                if (objAddCRDetails.ContractID == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objAddCRDetails.ContractID;

                sqlParam[1] = new SqlParameter(SPParameter.ProjectCode, SqlDbType.NChar, 50);
                if (string.IsNullOrEmpty(objAddCRDetails.CRProjectCode))
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objAddCRDetails.CRProjectCode;

                sqlParam[2] = new SqlParameter(SPParameter.CRReferenceNo, SqlDbType.NChar, 50);
                if (objAddCRDetails.CRReferenceNo == "" || objAddCRDetails.CRReferenceNo == null)
                    sqlParam[2].Value = DBNull.Value;
                else
                    sqlParam[2].Value = objAddCRDetails.CRReferenceNo;

                sqlParam[3] = new SqlParameter(SPParameter.StartDate, SqlDbType.DateTime);
                if (objAddCRDetails.CRStartDate == DateTime.MinValue)
                    sqlParam[3].Value = DBNull.Value;
                sqlParam[3].Value = objAddCRDetails.CRStartDate;

                sqlParam[4] = new SqlParameter(SPParameter.EndDate, SqlDbType.DateTime);
                if (objAddCRDetails.CREndDate == DateTime.MinValue)
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = objAddCRDetails.CREndDate;

                sqlParam[5] = new SqlParameter(SPParameter.Remarks, SqlDbType.NChar, 50);
                if (string.IsNullOrEmpty(objAddCRDetails.CRRemarks))
                    sqlParam[5].Value = DBNull.Value;
                else
                    sqlParam[5].Value = objAddCRDetails.CRRemarks;

                //Execute SP along with proper parameters
                objDA.ExecuteNonQuerySP(SPNames.Contracts_AddCRDetails, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, Contracts, "AddCRDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Updates the CR details.
        /// </summary>
        /// <param name="objUpdateCRDetails">The obj update CR details.</param>
        public void UpdateCRDetails(BusinessEntities.Contract objUpdateCRDetails)
        {
            //Initialise Data Access Class object
            objDA = new DataAccessClass();

            //Initialise SqlParameter Class object
            sqlParam = new SqlParameter[5];

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Check each parameters nullibality and add values to sqlParam object accordingly
                sqlParam[0] = new SqlParameter(SPParameter.CRId, SqlDbType.Int);
                if (objUpdateCRDetails.CRId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objUpdateCRDetails.CRId;

                sqlParam[1] = new SqlParameter(SPParameter.CRReferenceNo, SqlDbType.NChar, 50);
                if (objUpdateCRDetails.CRReferenceNo == "" || objUpdateCRDetails.CRReferenceNo == null)
                    sqlParam[1].Value = DBNull.Value;
                else
                    sqlParam[1].Value = objUpdateCRDetails.CRReferenceNo;

                sqlParam[2] = new SqlParameter(SPParameter.StartDate, SqlDbType.DateTime);
                if (objUpdateCRDetails.CRStartDate == DateTime.MinValue)
                    sqlParam[2].Value = DBNull.Value;
                sqlParam[2].Value = objUpdateCRDetails.CRStartDate;

                sqlParam[3] = new SqlParameter(SPParameter.EndDate, SqlDbType.DateTime);
                if (objUpdateCRDetails.CREndDate == DateTime.MinValue)
                    sqlParam[3].Value = DBNull.Value;
                else
                    sqlParam[3].Value = objUpdateCRDetails.CREndDate;

                sqlParam[4] = new SqlParameter(SPParameter.Remarks, SqlDbType.NChar, 50);
                if (string.IsNullOrEmpty(objUpdateCRDetails.CRRemarks))
                    sqlParam[4].Value = DBNull.Value;
                else
                    sqlParam[4].Value = objUpdateCRDetails.CRRemarks;

                //Execute SP along with proper parameters
                objDA.ExecuteNonQuerySP(SPNames.Contracts_UpdateCRDetails, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, Contracts, "UpdateCRDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }

        /// <summary>
        /// Gets the CR details.
        /// </summary>
        /// <param name="objGetCRDetails">The obj get CR details.</param>
        /// <returns></returns>
        public BusinessEntities.RaveHRCollection GetCRDetails(BusinessEntities.Contract objGetCRDetails)
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
                sqlParam[0] = new SqlParameter(SPParameter.ContractId, SqlDbType.Int);
                if (objGetCRDetails.ContractID == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objGetCRDetails.ContractID;

                objDataReader = objDA.ExecuteReaderSP(SPNames.Contracts_GetCRDetails, sqlParam);

                while (objDataReader.Read())
                {
                    //Initialise the Business Entity object
                    objCRDetails = new BusinessEntities.Contract();

                    objCRDetails.CRId = Convert.ToInt32(objDataReader[DbTableColumn.Con_CRId].ToString());
                    objCRDetails.CRProjectCode = objDataReader[DbTableColumn.ProjectCode].ToString();
                    objCRDetails.CRReferenceNo = objDataReader[DbTableColumn.Con_CRReferenceNo].ToString();
                    objCRDetails.CRStartDate = Convert.ToDateTime(objDataReader[DbTableColumn.Con_CRStartDate].ToString());
                    objCRDetails.CREndDate = Convert.ToDateTime(objDataReader[DbTableColumn.Con_CREndDate].ToString());
                    objCRDetails.CRRemarks = objDataReader[DbTableColumn.Con_Remarks].ToString();

                    // Add the object to Collection
                    raveHRCollection.Add(objCRDetails);
                }
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, Contracts, "GetCRDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
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
        /// Deletes the CR details.
        /// </summary>
        /// <param name="objDeleteCRDetails">The obj delete CR details.</param>
        public void DeleteCRDetails(BusinessEntities.Contract objDeleteCRDetails)
        {
            //Initialise Data Access Class object
            objDA = new DataAccessClass();

            //Initialise SqlParameter Class object
            sqlParam = new SqlParameter[1];

            try
            {
                //Open the connection to DB
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                //Check each parameters nullibality and add values to sqlParam object accordingly
                sqlParam[0] = new SqlParameter(SPParameter.CRId, SqlDbType.Int);
                if (objDeleteCRDetails.CRId == 0)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = objDeleteCRDetails.CRId;

                //Execute SP along with proper parameters
                objDA.ExecuteNonQuerySP(SPNames.Contracts_DeleteCRDetails, sqlParam);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, Contracts, "DeleteCRDetails", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
        }


        /// <summary>
        /// Checks the CR reference no.
        /// </summary>
        /// <param name="objCRDetails">The obj CR details.</param>
        /// <returns></returns>
        public bool checkCRReferenceNo(BusinessEntities.Contract objCRDetails)
        {
            bool result = false;
            DataAccessClass ProjectDetails = new DataAccessClass();
            try
            {
                ProjectDetails.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[4];
                sqlParam[0] = new SqlParameter(SPParameter.CRReferenceNo, DbType.String);
                sqlParam[0].Value = objCRDetails.CRReferenceNo;

                sqlParam[1] = new SqlParameter(SPParameter.ContractId, DbType.Int32);
                sqlParam[1].Value = objCRDetails.ContractID;

                sqlParam[2] = new SqlParameter(SPParameter.ProjectCode, DbType.String);
                sqlParam[2].Value = objCRDetails.CRProjectCode;

                sqlParam[3] = new SqlParameter(SPParameter.COUNT, DbType.Int32);
                sqlParam[3].Direction = ParameterDirection.Output;

                //gets the all  employee details related to project .
                ProjectDetails.ExecuteNonQuerySP(SPNames.Contracts_CheckCRReferenceNo, sqlParam);

                int count = Convert.ToInt32(sqlParam[3].Value);

                if (count > 0)
                    result = true;
                else
                    result = false;

                return result;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, Contracts, "checkCRReferenceNo", EventIDConstants.RAVE_HR_CONTRACT_PRESENTATION_LAYER);
            }
            finally
            {
                ProjectDetails.CloseConncetion();
            }
        }

        #endregion Public Method

    }
}
