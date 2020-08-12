using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using RMS.Common.DataBase;
using System.Data.SqlClient;
using RMS.Common.Constants;
using Infrastructure.Interfaces;
namespace Infrastructure
{
   public class OtherMasterRepository : IOtherMasterRepository
    {
       public int SaveOtherMaster(OtherMaster objOtherMaster)
       {
           DataAccessClass objData = new DataAccessClass();           
           SqlCommand cmd = new SqlCommand();
           cmd.Parameters.AddWithValue("@GroupMasterID", objOtherMaster.GroupMasterID);
           cmd.Parameters.AddWithValue("@CreatedByID", objOtherMaster.CreatedByID);
           cmd.Parameters.AddWithValue("@Name", objOtherMaster.Name);
           cmd.Parameters.AddWithValue("@IsCommonCostCode", objOtherMaster.IsCommonCostCode);
           
       //    cmd.Parameters.AddWithValue("@Category", objOtherMaster.Category);                      
           return Convert.ToInt32(objData.ExecuteScalarSP_WithConnection(SPNames.Insert_OtherMaster , cmd));

       }
       public int UpdateOtherMaster(OtherMaster objOtherMaster)
       {
           DataAccessClass objData = new DataAccessClass();
           
           SqlCommand cmd = new SqlCommand();
           cmd.Parameters.AddWithValue("@MasterID", objOtherMaster.MasterID);
           cmd.Parameters.AddWithValue("@GroupMasterID", objOtherMaster.GroupMasterID);
           cmd.Parameters.AddWithValue("@LastModifiedByID", objOtherMaster.LastModifiedByID);
           cmd.Parameters.AddWithValue("@Name", objOtherMaster.Name);
           cmd.Parameters.AddWithValue("@IsCommonCostCode", objOtherMaster.IsCommonCostCode);
         //  cmd.Parameters.AddWithValue("@Category", objOtherMaster.Category);
           return Convert.ToInt32(objData.ExecuteScalarSP_WithConnection(SPNames.Update_OtherMaster, cmd));

       }
       public int DeleteOtherMaster(OtherMaster objOtherMaster)
       {
           DataAccessClass objData = new DataAccessClass();
           
           SqlCommand cmd = new SqlCommand();
           cmd.Parameters.AddWithValue("@LastModifiedByID", objOtherMaster.LastModifiedByID);
           cmd.Parameters.AddWithValue("@MasterID", objOtherMaster.MasterID);
           return Convert.ToInt32(objData.ExecuteScalarSP_WithConnection(SPNames.Delete_OtherMaster, cmd));
       }
       public List<OtherMasterResult> GetOtherMaster(OtherMaster objOtherMaster)
       {
           DataAccessClass objData = new DataAccessClass();
           
           SqlCommand cmd = new SqlCommand();
           if (objOtherMaster.MasterID!=0)
            cmd.Parameters.AddWithValue("@MasterID", objOtherMaster.MasterID);

           if (objOtherMaster.GroupMasterID != 0)
                cmd.Parameters.AddWithValue("@GroupMasterID", objOtherMaster.GroupMasterID);

           


           return objData.ExecuteReaderSP_WithConnection<OtherMasterResult>(SPNames.Get_OtherMaster,cmd);
       }

       public List<GroupMasterModel> GetGroupMaster()
       {
           DataAccessClass objData = new DataAccessClass();
           SqlCommand cmd = new SqlCommand();
           return objData.ExecuteReaderSP_WithConnection<GroupMasterModel>(SPNames.Get_GroupMaster, cmd);
       }



    }
}
