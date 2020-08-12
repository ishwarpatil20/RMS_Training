using Domain.Entities;
using System;
using System.Collections.Generic;
namespace Services.Interfaces
{
   public interface IOtherMasterService
    {
        int Delete_OtherMaster(Domain.Entities.OtherMaster objMaster);
        System.Collections.Generic.List<Domain.Entities.OtherMasterResult> Get_OtherMaster(Domain.Entities.OtherMaster objMaster);
        int Insert_OtherMaster(Domain.Entities.OtherMaster objMaster);
        int Update_OtherMaster(Domain.Entities.OtherMaster objMaster);
        List<GroupMasterModel> GetGroupMaster();
    }
}
