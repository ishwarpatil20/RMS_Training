using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Domain.Entities;
using Infrastructure.Interfaces;
using System.Data.Sql;
using System.Data.SqlClient;
using RMS.Common;
using RMS.Common.ExceptionHandling;
using RMS.Common.Constants;
using RMS.Common.DataBase;
using RMS.Common.BusinessEntities;
using System.Web.Mvc;
using System.Web;
using RMS.Common.BusinessEntities.Common;
using RMS.Common.BusinessEntities.Menu;
using System.Configuration;
using System.Collections;

namespace Infrastructure
{
    public class CommonRepository : ICommonRepository
    {
        #region

        public static SqlConnection objConnection = null;
        public static SqlCommand objCommand = null;
        public static SqlDataReader objReader;
        const string clsCommonRepository = "CommonRepository.cs";

        private static List<CommonModel> ListCommonModel;
        private static CommonModel ObjCommonModel;
        private static DataSet ds; 

        #endregion


        #region Encrypt
        public static string Encode(string encodeMe)
        {
            byte[] encoded = System.Text.Encoding.UTF8.GetBytes(encodeMe);
            return Convert.ToBase64String(encoded);
        }

        public static string Decode(string decodeMe)
        {
            byte[] encoded = Convert.FromBase64String(decodeMe);
            return System.Text.Encoding.UTF8.GetString(encoded);
        }
        #endregion
        //Binding Priority master entry
        public static SelectList FillPriorityDropDownList()
        {
            try
            {
                List<SelectListItem> newList = new List<SelectListItem>();
                newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "" });
                newList.Add(new SelectListItem { Value = "1", Text = "High" });
                newList.Add(new SelectListItem { Value = "2", Text = "Medium" });
                newList.Add(new SelectListItem { Value = "3", Text = "Low" });

                return new SelectList(newList, "Value", "Text");
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, clsCommonRepository, "FillPriorityDropDownList", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
        }

        //Binding Quarter master entry
        public static SelectList FillQuarterDropDownList(string Operation)
        {
            try
            {
                int mth = DateTime.Now.Month;
                List<SelectListItem> newList = new List<SelectListItem>();
                newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "" });

                if (Operation == CommonConstants.View)
                {
                    newList.Add(new SelectListItem { Value = "4", Text = "Jan - Mar" });
                    newList.Add(new SelectListItem { Value = "1", Text = "Apr - Jun" });
                    newList.Add(new SelectListItem { Value = "2", Text = "Jul - Sep" });
                    newList.Add(new SelectListItem { Value = "3", Text = "Oct - Dec" });
                }
                else
                {
                    if (mth <= 3)
                    {
                        newList.Add(new SelectListItem { Value = "4", Text = "Jan - Mar" });
                        newList.Add(new SelectListItem { Value = "1", Text = "Apr - Jun" });
                    }
                    else if (mth > 3 && mth <= 6)
                    {
                        newList.Add(new SelectListItem { Value = "1", Text = "Apr - Jun" });
                        newList.Add(new SelectListItem { Value = "2", Text = "Jul - Sep" });
                    }
                    else if (mth > 6 && mth <= 9)
                    {
                        newList.Add(new SelectListItem { Value = "2", Text = "Jul - Sep" });
                        newList.Add(new SelectListItem { Value = "3", Text = "Oct - Dec" });
                    }
                    else if (mth > 9 && mth <= 12)
                    {
                        newList.Add(new SelectListItem { Value = "3", Text = "Oct - Dec" });
                        newList.Add(new SelectListItem { Value = "4", Text = "Jan - Mar" });
                    }
                }

                return new SelectList(newList, "Value", "Text");
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, clsCommonRepository, "FillDropDownList", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
        }

        public static SelectList FillObjectiveMetDropDownList()
        {
            try
            {
                List<SelectListItem> newList = new List<SelectListItem>();
                //Neelam Issue Id:60562 start
                newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "0" });
                //Neelam Issue Id:60562 end
                newList.Add(new SelectListItem { Value = "1", Text = "Yes" });
                newList.Add(new SelectListItem { Value = "2", Text = "No" });
                newList.Add(new SelectListItem { Value = "3", Text = "Partially Met" });
                newList.Add(new SelectListItem { Value = "4", Text = "NA" });

                return new SelectList(newList, "Value", "Text");
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, clsCommonRepository, "FillObjectiveMetDropDownList", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
         }

        //Binding T_Master master dropdown values
        public static SelectList FillMasterDropDownList(string CategoryName)
        {
            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TNI_GetMasterSP, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@Category", CategoryName);
                objReader = objCommand.ExecuteReader();

                SelectListItem selListItem;
                List<SelectListItem> newList = new List<SelectListItem>();
                newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "0" });
                while (objReader.Read())
                {
                    selListItem = new SelectListItem() { Value = objReader[0].ToString().Trim(), Text = objReader[1].ToString().Trim() };

                    newList.Add(selListItem);
                }
                return new SelectList(newList, "Value", "Text");
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, clsCommonRepository, "FillMasterDropDownList", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }
        public static SelectList FillMonthsDropDownList(string CategoryName)
        {
            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TNI_GetMonthsSP, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@Category", CategoryName);
                objReader = objCommand.ExecuteReader();

                SelectListItem selListItem;
                List<SelectListItem> newList = new List<SelectListItem>();
                newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "0" });
                while (objReader.Read())
                {
                    selListItem = new SelectListItem() { Value = objReader[0].ToString().Trim(), Text = objReader[1].ToString().Trim() };

                    newList.Add(selListItem);
                }
                return new SelectList(newList, "Value", "Text");
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, clsCommonRepository, "FillMasterDropDownList", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }
                
        public static SelectList FillMasterTrainingTypelist(string CategoryName, string RoleName)
        {
            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TNI_USP_TNI_GetMasterTrainingType, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@Category", CategoryName);
                objCommand.Parameters.AddWithValue("@RoleName", RoleName);
                objReader = objCommand.ExecuteReader();

                SelectListItem selListItem;
                List<SelectListItem> newList = new List<SelectListItem>();
                newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "" });
                while (objReader.Read())
                {
                    selListItem = new SelectListItem() { Value = objReader[0].ToString().Trim(), Text = objReader[1].ToString().Trim() };

                    newList.Add(selListItem);
                }
                return new SelectList(newList, "Value", "Text");
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, clsCommonRepository, "FillMasterDropDownList", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        public static SelectList FillMasterRadioButtonList(string CategoryName)
        {
            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TNI_GetMasterSP, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@Category", CategoryName);
                objReader = objCommand.ExecuteReader();

                SelectListItem selListItem;
                List<SelectListItem> newList = new List<SelectListItem>();
                while (objReader.Read())
                {
                    selListItem = new SelectListItem() { Value = objReader[0].ToString().Trim(), Text = objReader[1].ToString().Trim() };

                    newList.Add(selListItem);
                }
                return new SelectList(newList, "Value", "Text");
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, clsCommonRepository, "FillMasterRadioButtonList", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        public static SelectList FillMasterVendorList()
        {
            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.GetTrainingVendorMaster, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objReader = objCommand.ExecuteReader();

                SelectListItem selListItem;
                List<SelectListItem> newList = new List<SelectListItem>();
                newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "0" });
                while (objReader.Read())
                {
                    selListItem = new SelectListItem() { Value = objReader[0].ToString().Trim(), Text = objReader[1].ToString().Trim() };

                    newList.Add(selListItem);
                }
                return new SelectList(newList, "Value", "Text");
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, clsCommonRepository, "FillMasterVendorList", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        public static List<Effectiveness> GetMasterTrainingEffectivenessDetails()
        {
            DataAccessClass daTrainingCourse = new DataAccessClass();
            List<Effectiveness> lstEffDtls = new List<Effectiveness>();

            try
            {
                daTrainingCourse.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter("@Category", SqlDbType.VarChar, 50);
                sqlParam[0].Value = "TrainingEffectiveness";
                SqlDataReader dr = daTrainingCourse.ExecuteReaderSP(SPNames.TNI_GetMasterSP, sqlParam);

                while (dr.Read())
                {
                    Effectiveness teff = new Effectiveness();
                    teff.EffectivenessID = Convert.ToInt32(dr[DbTableColumn.TrainingEffectivenessID]);
                    teff.EffectivenessName = Convert.ToString(dr[DbTableColumn.TrainingEffectivenessName]);
                    teff.IsSelected = false;
                    lstEffDtls.Add(teff);
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, clsCommonRepository, "GetTrainingEffectivenessDetails", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                daTrainingCourse.CloseConncetion();
            }
            return lstEffDtls;
        }

        //Showing Active employees list
        public static SelectList FillEmployeesList()
        {
            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.GET_ActiveEmployeeList, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objReader = objCommand.ExecuteReader();

                SelectListItem selListItem;
                List<SelectListItem> newList = new List<SelectListItem>();
                newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "" });
                while (objReader.Read())
                {
                    selListItem = new SelectListItem() { Value = objReader[0].ToString().Trim(), Text = objReader[1].ToString().Trim() };

                    newList.Add(selListItem);
                }
                return new SelectList(newList, "Value", "Text");
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, clsCommonRepository, "FillEmployeesList", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        public static List<string> GetListFromString(string strItem)
        {
            try
            {
                List<string> lstItems = new List<string>(); 
                string[] strContentArray = strItem.Split(new char[] { '|' });
                foreach (string s in strContentArray)
                {
                    lstItems.Add(s);
                }
                //List<SelectListItem> newList = new List<SelectListItem>();
                //for (int i = 0; i < strContentArray.Length; i++)
                //{
                //    string strContentName = ("View Course Content " + (i + 1));
                //    SelectListItem selListItem = new SelectListItem() { Value = strContentArray[i].ToString().Trim(), Text = strContentName };

                //    newList.Add(selListItem);
                //}
                //return new SelectList(newList, "Value", "Text");
                return lstItems;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, clsCommonRepository, "GetCourseContentDetails", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
            }
        }

        public static List<FileDetail> GetFileDetailList(string filelocation, string strItem)
        {
            List<FileDetail> lstFiles = new List<FileDetail>();
            try
            {
                if (strItem != "")
                {
                string[] strContentArray = strItem.Split(new char[] { '|' });
                foreach (string s in strContentArray)
                {
                    FileDetail fileDetail = new FileDetail();
                        fileDetail.PhysicalFileName = s;
                        fileDetail.FileName = s.Substring(s.IndexOf("_") + 1, (s.Length - (s.IndexOf("_") + 1)));
                        fileDetail.FilePath = "~/" + filelocation + "/" + s;
                    lstFiles.Add(fileDetail);
                }
            }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, clsCommonRepository, "GetCourseContentDetails", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
            }
            return lstFiles;
        }

        public static List<FileDetail> GetFileDetailList(string filelocation, List<FileDetails> Filelist, string FileCategory,bool EnableDeleteFlag)
        {
            List<FileDetail> lstFiles = new List<FileDetail>();
            try
            {
                if (Filelist != null)
                {
                    //string[] strContentArray = strItem.Split(new char[] { '|' });

                    foreach (var s in Filelist)
                    {
                        if (s.Category == FileCategory)
                        {
                            FileDetail fileDetail = new FileDetail();
                            fileDetail.FileId = s.FileId;
                            fileDetail.PhysicalFileName = s.FileGuid;
                            fileDetail.FileName = s.FileName;
                            fileDetail.FilePath = "~/" + filelocation + "/" +s.FileGuid;
                            fileDetail.DeleteFlag = EnableDeleteFlag;
                            lstFiles.Add(fileDetail);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, clsCommonRepository, "GetCourseContentDetails", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
            }
            return lstFiles;
        }

        //Showing Active employee list with All fields(Employee PopUP List)
        public static List<CommonModel> FillPopUpEmployeeList(string FirstName)
        {
            SqlParameter[] sqlParam = new SqlParameter[2];
            ListCommonModel = new List<CommonModel>();

            DataAccessClass objGetTraining = new DataAccessClass();
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                sqlParam[0] = new SqlParameter(SPParameter.FirstName, SqlDbType.NVarChar);
                sqlParam[0].Value = FirstName;
                sqlParam[1] = new SqlParameter(SPParameter.AllEmpList, SqlDbType.Int);
                sqlParam[1].Value = 0;
                ds = new DataSet();

                ds = objGetTraining.GetDataSet(SPNames.Employee_GetEmployeesList, sqlParam);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ObjCommonModel = new CommonModel();

                    ObjCommonModel.EmpID = dr[DbTableColumn.EMPId].ToString();
                    ObjCommonModel.EMPCode = dr[DbTableColumn.EMPCode].ToString();
                    ObjCommonModel.FirstName = dr[DbTableColumn.FirstName].ToString();
                    ObjCommonModel.LastName = dr[DbTableColumn.LastName].ToString();
                    ObjCommonModel.Designation = dr[DbTableColumn.Designation].ToString();

                    ListCommonModel.Add(ObjCommonModel);
                }
                ds.Clear();
                return ListCommonModel;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, clsCommonRepository, "FillPopUpEmployeeList", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }

        }


        public static List<KeyValuePair<int, string>> GetAllEmployeeList()
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            List<KeyValuePair<int, string>> employeeList = new List<KeyValuePair<int, string>>();
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlDataReader dr = objGetTraining.ExecuteReaderSP(SPNames.TNI_GetAllActiveEmployeeList);
                while (dr.Read())
                {
                    employeeList.Add(new KeyValuePair<int, string>(Convert.ToInt32(dr[DbTableColumn.EMPId]), Convert.ToString(dr[DbTableColumn.EmployeeName])));
                }
                
                return employeeList;                
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.InfrastructureLayer, CommonConstants.CommonRepository, "GetAllEmployeeList", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }
        }

         /// <summary>
        /// Gets the logged in user emailid
        /// </summary>
        /// <returns>string</returns>
        public string GetWindowsUsernameAsPerNorthgate(string windowsUsername, out string domainName)
        {
            string username = "";
            string domName = "";
            SqlDataReader objReader;
            DataAccessClass objDA;
            try
            {
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[3];
                sqlParam[0] = new SqlParameter("@Username", SqlDbType.VarChar, 100);
                if (windowsUsername == "" || windowsUsername == null)
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = windowsUsername;

                sqlParam[1] = new SqlParameter("@OutUsername", SqlDbType.VarChar, 100);
                sqlParam[1].Value = "";
                sqlParam[1].Direction = ParameterDirection.Output;

                sqlParam[2] = new SqlParameter("@domainName", SqlDbType.VarChar, 100);
                sqlParam[2].Value = "";
                sqlParam[2].Direction = ParameterDirection.Output;

                objReader = objDA.ExecuteReaderSP(SPNames.Master_GetNorthgateUsername, sqlParam);

                username = sqlParam[1].Value.ToString();
                domName = sqlParam[2].Value.ToString();
                domainName = domName;
                return username;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                new RaveHRException(ex.Message, ex, Sources.CommonLayer, "RaveHRAuthorizationManager", "GetWindowsUsernameAsPerNorthgate", EventIDConstants.AUTHORIZATION_MANAGER_ERROR);
            }
            domainName = domName;
            return username;
        }

        public int GetEmployeeID(string EmailId)
        {
            SqlConnection objConnection = null;
            SqlCommand objCommand = null;
            string ConnStr = string.Empty;
            try
            {
                ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.Contract_GetLoggedInEmployeeId, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                SqlParameter[] sqlParam = new SqlParameter[2];
                sqlParam[0] = objCommand.Parameters.AddWithValue("@EmailId", EmailId);
                sqlParam[1] = objCommand.Parameters.AddWithValue("@OutEmpId", SqlDbType.Int);
                sqlParam[1].Direction = ParameterDirection.Output;
                int contract = objCommand.ExecuteNonQuery();
                int empId = Convert.ToInt32(sqlParam[1].Value);
                return empId;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, "Projects.cs", "UpdateProject", EventIDConstants.RAVE_HR_PROJECTS_DATA_ACCESS_LAYER);
            }

            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }

        }
        public static List<SelectListItem> GetMasterCategoryList(string CategoryName, bool defaultValue, string[] selItems)
        {
            
            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TNI_GetMasterSP, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.Parameters.AddWithValue("@Category", CategoryName);
                objReader = objCommand.ExecuteReader();

                SelectListItem selListItem;
                List<SelectListItem> newList = new List<SelectListItem>();
                if (defaultValue) { newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "0" }); }
                while (objReader.Read())
                {
                    selListItem = new SelectListItem() { Value = objReader[0].ToString().Trim(), Text = objReader[1].ToString().Trim(), Selected = selItems.Contains(Convert.ToString(objReader[0]).Trim()) ? true : false };

                    newList.Add(selListItem);
                }
                return newList;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, clsCommonRepository, "GetMasterCategoryList", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        public static SelectList GetEmptySelectList(string defaultVal)
        {
            List<SelectListItem> newList = new List<SelectListItem>();
            newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = defaultVal });
            return new SelectList(newList, "Value", "Text");
        }

        public static List<SelectListItem> GetListWithDefault(string text, string val, bool selected)
        {
            List<SelectListItem> newList = new List<SelectListItem>();
            newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "0" });
            newList.Add(new SelectListItem { Selected = selected, Text = text , Value = val });
            return newList;
        }

        public static string QuarterValueById(int id)
        {
            Dictionary<int, string> QuarterDict = new Dictionary<int, string>();
            QuarterDict.Add(4, "Jan - Mar");
            QuarterDict.Add(1, "Apr - Jun");
            QuarterDict.Add(2, "Jul - Sep");
            QuarterDict.Add(3, "Oct - Dec");

            return QuarterDict[id];
        }

        public SelectList GetDefaultSelectList(string defaultValue)
        {
            List<SelectListItem> newList = new List<SelectListItem>();
            newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = defaultValue });
            return new SelectList(newList, "Value", "Text");
        }
        public string AccessForTrainingModule(int UserEmpId)
        {
            SqlDataReader objReader;
            DataAccessClass objDA;
            try
            {
                objDA = new DataAccessClass();
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[3];

                sqlParam[0] = new SqlParameter(SPParameter.UserEmpID, SqlDbType.VarChar, 100);
                if (String.IsNullOrWhiteSpace(Convert.ToString(UserEmpId)))
                    sqlParam[0].Value = DBNull.Value;
                else
                    sqlParam[0].Value = Convert.ToString(UserEmpId);

                sqlParam[1] = new SqlParameter(SPParameter.RoleId, SqlDbType.VarChar, 10);
                sqlParam[1].Value = "";
                sqlParam[1].Direction = ParameterDirection.Output;

                sqlParam[2] = new SqlParameter(SPParameter.RoleName, SqlDbType.VarChar, 50);
                sqlParam[2].Value = "";
                sqlParam[2].Direction = ParameterDirection.Output;
                
                //objCommand.ExecuteNonQuery();
                objReader = objDA.ExecuteReaderSP(SPNames.TNI_AccessRightForTrainingModule, sqlParam);
                return Convert.ToString(sqlParam[2].Value);
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
                {
                new RaveHRException(ex.Message, ex, Sources.CommonLayer, "AccessForTrainingModule", "AccessForTrainingModule", EventIDConstants.AUTHORIZATION_MANAGER_ERROR);
            }
            return string.Empty;
        }

        public static List<SelectListItem> GetEmployeeNameList(bool SelectItem, string SelectVal)
        {
            DataAccessClass objGetTraining = new DataAccessClass();
            List<SelectListItem> employeeList = new List<SelectListItem>();
            try
            {
                objGetTraining.OpenConnection(DBConstants.GetDBConnectionString());
                SqlDataReader dr = objGetTraining.ExecuteReaderSP(SPNames.TNI_GetAllActiveEmployeeList);
                if (SelectItem) { employeeList.Add(new SelectListItem { Selected = true, Text = "Select", Value = SelectVal }); }
                SelectListItem ListItem;
                while (dr.Read())
                {
                    ListItem = new SelectListItem() { Value = dr[0].ToString().Trim(), Text = dr[1].ToString().Trim()};
                    employeeList.Add(ListItem);
                }

                return employeeList;
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.InfrastructureLayer, CommonConstants.CommonRepository, "GetAllEmployeeList", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objGetTraining.CloseConncetion();
            }
        }

        /// <summary>
        /// Get menu for employee
        /// </summary>
        /// <returns>menu</returns>
        public List<Menu> GetAuthoriseMenuList(int Empid)
        {
            DataAccessClass daTrainingCourse = new DataAccessClass();
            List<Menu> LstMenu = new List<Menu>();
            Menu objMenu;
            try
            {
                daTrainingCourse.OpenConnection(DBConstants.GetDBConnectionString());
                SqlParameter[] sqlParam = new SqlParameter[1];
                sqlParam[0] = new SqlParameter(SPParameter.EmpId, SqlDbType.Int);
                sqlParam[0].Value = Empid ;                
                SqlDataReader dr = daTrainingCourse.ExecuteReaderSP(SPNames.GetMenuItemData, sqlParam);


                //fetch menu and submenulist
                while (dr.Read())
                {
                    objMenu = new Menu();
                    //objMenu.ResponsibilityID = Convert.ToInt16(dr[DbTableColumn.ResponsibilityID]);
                    //objMenu.MenuName = Convert.ToString(dr[DbTableColumn.Name]);
                    objMenu.PageID = Convert.ToInt16(dr[DbTableColumn.PageID]);
                    objMenu.ParentID = Convert.ToInt16(dr[DbTableColumn.ParentID]);
                    objMenu.PageName= Convert.ToString(dr[DbTableColumn.PageName]);
                    objMenu.PageURL = Convert.ToString(dr[DbTableColumn.PageURL]);
                    objMenu.MenuOrderID = Convert.ToInt16(dr[DbTableColumn.MenuOrderID]);
                    objMenu.ReportName = Convert.ToString(dr[DbTableColumn.ReportName]);
                    if (objMenu.ParentID == CommonConstants.ONE && objMenu.PageName == "RMS")
                    {
                        objMenu.baseUrl = "";
                    }
                    else
                    {
                        objMenu.baseUrl = ConfigurationManager.AppSettings[CommonConstants.BaseUrl];
                    }
                    //objMenu.SubMenu += LstMenu.Where(m => m.MenuOrderID == objMenu.ParentID).ToList<Menu>();                    
                    LstMenu.Add(objMenu);
                }

                //set submenu
                foreach (var menu in LstMenu)
                {
                    menu.SubMenu = LstMenu.Where(m => m.ParentID == menu.MenuOrderID).ToList<Menu>();
                }
               
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, clsCommonRepository, "GetAuthoriseMenuList", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                daTrainingCourse.CloseConncetion();
            }
            return LstMenu;        
        }


        public ArrayList GetEmployeeRole(int UserEmpId)
        {
            ArrayList Arr = new ArrayList();
            SqlDataReader objReader;
            DataAccessClass objDA = new DataAccessClass();
            try
            {                
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.UserEmpID, SqlDbType.Int);
                sqlParam[0].Value = UserEmpId;

                //sqlParam[1] = new SqlParameter(SPParameter.RoleId, SqlDbType.Int);
                //sqlParam[1].Direction = ParameterDirection.Output;
               
                //sqlParam[2] = new SqlParameter(SPParameter.RoleName, SqlDbType.VarChar, 50);
                //sqlParam[2].Direction = ParameterDirection.Output;
                                
                objReader = objDA.ExecuteReaderSP(SPNames.GetEmployeeRole, sqlParam);

                while (objReader.Read())
                {
                    Arr.Add(Convert.ToString(objReader[DbTableColumn.Name]));
                }
                return Arr;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                new RaveHRException(ex.Message, ex, Sources.CommonLayer, "AccessForTrainingModule", "AccessForTrainingModule", EventIDConstants.AUTHORIZATION_MANAGER_ERROR);
            }
            finally
            {
                objDA.CloseConncetion();
            }
            return Arr;
        }

        public static SelectList FillMasterKSSTypeList(string CategoryName, int UserEmpId)
        {
            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.TNI_USP_TNI_GetDepartmentWiseKSSType, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;

                objCommand.Parameters.AddWithValue("@Category", CategoryName);
                objCommand.Parameters.AddWithValue("@UserEmpId", UserEmpId);

                objReader = objCommand.ExecuteReader();

                SelectListItem selListItem;
                List<SelectListItem> newList = new List<SelectListItem>();
                newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "" });

                while (objReader.Read())
                {
                    selListItem = new SelectListItem() { Value = objReader[0].ToString().Trim(), Text = objReader[1].ToString().Trim() };

                    newList.Add(selListItem);
                }
                return new SelectList(newList, "Value", "Text");
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, clsCommonRepository, "FillMasterKSSTypeList", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        public static SelectList FillDepartmentList()
        {
            try
            {
                string ConnStr = DBConstants.GetDBConnectionString();
                objConnection = new SqlConnection(ConnStr);
                objConnection.Open();

                objCommand = new SqlCommand(SPNames.GetDepartment, objConnection);
                objCommand.CommandType = CommandType.StoredProcedure;                

                objReader = objCommand.ExecuteReader();

                SelectListItem selListItem;
                List<SelectListItem> newList = new List<SelectListItem>();
                newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "" });

                while (objReader.Read())
                {
                    selListItem = new SelectListItem() { Value = objReader[0].ToString().Trim(), Text = objReader[1].ToString().Trim() };

                    newList.Add(selListItem);
                }
                return new SelectList(newList, "Value", "Text");
            }
            catch (Exception ex)
            {
                throw new RaveHRException(ex.Message, ex, Sources.DataAccessLayer, clsCommonRepository, "FilldDepartmentList", RMS.Common.Constants.EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                if (objConnection.State == ConnectionState.Open)
                {
                    objConnection.Close();
                }
            }
        }

        public static SelectList FillDropdownDefaultValueSelect()
        {
            //SelectListItem selListItem;
            List<SelectListItem> newList = new List<SelectListItem>();
            newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "" });
            return new SelectList(newList, "Value", "Text");
        }

        public ArrayList GetReportingManagerEmailIds(int empId)
        {
            ArrayList Arr = new ArrayList();
            SqlDataReader objReader;
            DataAccessClass objDA = new DataAccessClass();
            try
            {
                objDA.OpenConnection(DBConstants.GetDBConnectionString());

                SqlParameter[] sqlParam = new SqlParameter[1];

                sqlParam[0] = new SqlParameter(SPParameter.EmpID, SqlDbType.Int);
                sqlParam[0].Value = empId;

                objReader = objDA.ExecuteReaderSP(SPNames.GetReportingManagerEmailIds, sqlParam);

                while (objReader.Read())
                {
                    Arr.Add(Convert.ToString(objReader[DbTableColumn.EmailId]));
                }
                return Arr;
            }
            catch (RaveHRException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                new RaveHRException(ex.Message, ex, Sources.CommonLayer, "CommonRepository", "GetReportingManagerEmailIds", EventIDConstants.TRAINING_DATA_ACCESS_LAYER);
            }
            finally
            {
                objDA.CloseConncetion();
            }
            return Arr;
        }
    }

}


