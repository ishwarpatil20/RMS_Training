using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Domain.Entities;
using Services.Interfaces;
namespace Services
{
   public class OtherMasterService : IOtherMasterService
    {
       private readonly IOtherMasterRepository _IOtherMasterRepository;


       public OtherMasterService(IOtherMasterRepository repository)
       {
           _IOtherMasterRepository = repository;
       }

       public int Insert_OtherMaster(OtherMaster objMaster)
       {
           return _IOtherMasterRepository.SaveOtherMaster(objMaster);
       }
       public int Update_OtherMaster(OtherMaster objMaster)
       {
           return _IOtherMasterRepository.UpdateOtherMaster(objMaster);
       }
       public int Delete_OtherMaster(OtherMaster objMaster)
       {
           return _IOtherMasterRepository.DeleteOtherMaster(objMaster);
       }
       public List<OtherMasterResult> Get_OtherMaster(OtherMaster objMaster)
       {
           return _IOtherMasterRepository.GetOtherMaster(objMaster);
       }
       public List<GroupMasterModel> GetGroupMaster() {
           return _IOtherMasterRepository.GetGroupMaster();
       }
    }
}
