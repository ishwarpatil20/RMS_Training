using Domain.Entities;
using System;
using System.Collections.Generic;
namespace Infrastructure.Interfaces
{
  public interface IOtherMasterRepository
    {
        int DeleteOtherMaster(Domain.Entities.OtherMaster objOtherMaster);
        System.Collections.Generic.List<Domain.Entities.OtherMasterResult> GetOtherMaster(Domain.Entities.OtherMaster objOtherMaster);
        int SaveOtherMaster(Domain.Entities.OtherMaster objOtherMaster);
        int UpdateOtherMaster(Domain.Entities.OtherMaster objOtherMaster);
        List<GroupMasterModel> GetGroupMaster();
    }
}
