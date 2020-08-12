using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Rave.HR.DataAccessLayer.Projects
{
    public class RaveHRMaster
    {

        SqlConnection objConnection = null;
        SqlCommand objCommand = null;
        SqlDataReader objReader = null;

        /// <summary>
        /// this function fills the dropdown
        /// </summary>
            public DataTable FillDropDownList(string strCategory)
            {
                try
                {
                    string ConnStr = ConfigurationManager.ConnectionStrings["RaveHRConnectionString"].ConnectionString;
                    //string ConnStr = Common.DBConstants.GetDBConnectionString();

                    objConnection = new SqlConnection(ConnStr);
                    objConnection.Open();
                    objCommand = new SqlCommand("USP_RaveHR_MasterSP", objConnection);
                    objCommand.CommandType = CommandType.StoredProcedure;
                    objCommand.Parameters.Add("@Category", strCategory);
                    objReader = objCommand.ExecuteReader();
                    DataTable objDataTable = new DataTable();
                    objDataTable.Load(objReader);
                    return objDataTable;

                    //if (dr.HasRows)
                    //{
                    //    ddlProjectCategory.DataSource = dr;
                    //    ddlProjectCategory.DataTextField = "Name";
                    //    ddlProjectCategory.DataValueField = "MasterID";
                    //    ddlProjectCategory.DataBind();
                    //}
                    //dr.Close();


                    //cmd = new SqlCommand("USP_RaveHR_AddProject_GetStatus", conn);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //dr = cmd.ExecuteReader();

                    ////Status dropdownlist is filled
                    //if (dr.HasRows)
                    //{
                    //    ddlPriorityStatus.DataSource = dr;
                    //    ddlPriorityStatus.DataTextField = "Name";
                    //    ddlPriorityStatus.DataValueField = "MasterID";
                    //    ddlPriorityStatus.DataBind();
                    //}
                    //dr.Close();

                    //cmd = new SqlCommand("USP_RaveHR_AddProject_GetProjectPriority", conn);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //dr = cmd.ExecuteReader();

                    ////Priority Status dropdownlist is filled
                    //if (dr.HasRows)
                    //{
                    //    ddlPriorityStatus.DataSource = dr;
                    //    ddlPriorityStatus.DataTextField = "Name";
                    //    ddlPriorityStatus.DataValueField = "MasterID";
                    //    ddlPriorityStatus.DataBind();
                    //}
                    //dr.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                finally
                {
                    if (objReader != null)
                    {
                        objReader.Close();
                    }
                    if (objConnection.State == ConnectionState.Open)
                    {
                        objConnection.Close();
                    }
                }
            }
        
    }
}

