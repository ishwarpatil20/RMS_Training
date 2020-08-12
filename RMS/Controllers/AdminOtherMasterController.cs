using RMS.Helpers;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using RMS.Models;
namespace RMS.Controllers
{
    [CheckAccess]
    public class AdminOtherMasterController : ErrorController 
    {
   
        #region Initialization
        private readonly IOtherMasterService _IOtherMasterService;
        #endregion

        #region Constructor {othermaster}
        public AdminOtherMasterController(IOtherMasterService othermaster)
        {
            _IOtherMasterService = othermaster;
        }
        #endregion

        #region Methods {Validate_Sucess}

        private void Validate_Sucess(ref OtherMasterViewModel objMasterView, int Id)
        {
            if (Id > 0)
            {
                objMasterView.Message = "Saved Sucess";
                objMasterView.IsReset = 1;
            }

            else if (Id == -1)
            {
                OtherMaster objOtherMaster = new OtherMaster();
                objMasterView.lstOtherMasters = _IOtherMasterService.Get_OtherMaster(objOtherMaster);
                ViewData["Result"] = "Record Already Exist ' " + objMasterView.Name + " '";
                objMasterView.Message = "Record Already Exist ' " + objMasterView.Name + " '";
                ModelState.AddModelError("Name", "Record Already Exist");
                objMasterView.IsReset = 0;
                objMasterView.IsRepeat = 1;
                objMasterView.GroupMasters = new SelectList(_IOtherMasterService.GetGroupMaster(), "GroupMasterID", "Category", "-select-");
                //   return View("Index",objMasterView);
                //    RedirectToAction("Load"); 
                // Load(objMasterView);
            }
            else
            {
                objMasterView.IsReset = 0;
                objMasterView.Message = "Error Occured";
                // Load(objMasterView);
            }
        }
        #endregion

        #region Action Methods {Index,Create,Edit,Delete,LoadAllMasters}

        public ActionResult Index()
        {            
            OtherMasterViewModel objMasterviewModel = new OtherMasterViewModel();
            OtherMaster objOtherMaster = new OtherMaster();
            objMasterviewModel.lstOtherMasters = _IOtherMasterService.Get_OtherMaster(objOtherMaster);
            objMasterviewModel.GroupMasters = new SelectList(_IOtherMasterService.GetGroupMaster(), "GroupMasterID", "Category", "-select-");
            return View(objMasterviewModel);

        }

        public ActionResult Create()
        {

            OtherMasterViewModel objMasterviewModel = new OtherMasterViewModel();
            OtherMaster objOtherMaster = new OtherMaster();
            objMasterviewModel.lstOtherMasters = _IOtherMasterService.Get_OtherMaster(objOtherMaster);

            objMasterviewModel.GroupMasters = new SelectList(_IOtherMasterService.GetGroupMaster(), "GroupMasterID", "Category", "-select-");
            return View(objMasterviewModel);
        }

        [HttpPost]        
        public ActionResult Create(OtherMasterViewModel objMasterView)
        {
           // if (ModelState.IsValid)
            {
                OtherMaster objMaster = new OtherMaster();
                objMaster.GroupMasterID = objMasterView.GroupMasterId;
                objMaster.Name = objMasterView.Name;
                objMaster.IsCommonCostCode = objMasterView.IsCommonCostCode;
                objMaster.CreatedByID = Convert.ToInt32(Session["EmpID"]);
                int Id = 0;

                if (objMasterView.MasterId == 0)

                    Id = _IOtherMasterService.Insert_OtherMaster(objMaster);
                else
                {
                    objMasterView.IsEdit = 1;
                    objMaster.LastModifiedByID = Convert.ToInt32(Session["EmpID"]);
                    objMaster.MasterID = objMasterView.MasterId;
                    Id = _IOtherMasterService.Update_OtherMaster(objMaster);
                }


                Validate_Sucess(ref objMasterView, Id);
                return Load(objMasterView);

            }
           // else
              //  return View("Error");

        }

        public ActionResult Load(OtherMasterViewModel objMasterviewModel)
        {
            OtherMaster objOtherMaster = new OtherMaster();
           // OtherMasterViewModel objMasterviewModel = new OtherMasterViewModel();
            objOtherMaster.GroupMasterID = 0;
            objMasterviewModel.lstOtherMasters = _IOtherMasterService.Get_OtherMaster(objOtherMaster);
            objMasterviewModel.GroupMasterId = 0;
            objMasterviewModel.Name = "";
           // objMasterviewModel.IsReset = 1;
            return PartialView(RMS.Common.Constants.CommonConstants.PartialListMasters, objMasterviewModel);
        }

        public PartialViewResult LoadAllMasters(int GroupMasterId)
        {
            OtherMaster objOtherMaster = new OtherMaster();
            OtherMasterViewModel objMasterviewModel = new OtherMasterViewModel();
            objOtherMaster.GroupMasterID = GroupMasterId;
            objMasterviewModel.lstOtherMasters = _IOtherMasterService.Get_OtherMaster(objOtherMaster);
            return PartialView(RMS.Common.Constants.CommonConstants.PartialListMasters, objMasterviewModel);
        }

        
        public ActionResult Edit(int id)
        {
            OtherMaster objOtherMaster = new OtherMaster();
            objOtherMaster.MasterID = id;
            List<OtherMasterResult> lstMasters = _IOtherMasterService.Get_OtherMaster(objOtherMaster);
            OtherMasterViewModel objMasterviewModel = new OtherMasterViewModel();
            objMasterviewModel.GroupMasters = new SelectList(_IOtherMasterService.GetGroupMaster(), "GroupMasterID", "Category", "-select-");
            if (lstMasters.Count > 0)
            {
                objMasterviewModel.GroupMasterId = lstMasters.FirstOrDefault().GroupMasterID;
                objMasterviewModel.Name = lstMasters.FirstOrDefault().Name;
                objMasterviewModel.MasterId = lstMasters.FirstOrDefault().MasterID;
                objMasterviewModel.IsCommonCostCode = (lstMasters.FirstOrDefault().IsCommon_CostCode.ToUpper() == "YES") ? true : false; 
            }
            objOtherMaster = new OtherMaster();
            objMasterviewModel.lstOtherMasters = _IOtherMasterService.Get_OtherMaster(objOtherMaster);

            return View("Index",objMasterviewModel);
        }

        //[HttpPost]
        //public ActionResult Edit(OtherMasterViewModel objMasterView)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        OtherMaster objMaster = new OtherMaster();
        //        objMaster.GroupMasterID = objMasterView.GroupMasterId;
        //        objMaster.Name = objMasterView.Name;
        //        objMaster.LastModifiedByID = Convert.ToInt32(Session["EmpID"]);
        //        objMaster.MasterID = objMasterView.MasterId;
        //        int Id = _IOtherMasterService.Update_OtherMaster(objMaster);

        //        return Validate_Sucess(ref objMasterView, Id);
        //    }
        //    else
        //        return View("Error");

        //}
        public ActionResult Delete(int id)
        {
            OtherMaster objMaster = new OtherMaster();
            objMaster.LastModifiedByID = Convert.ToInt32(Session["EmpID"]);
            objMaster.MasterID = id;
            int Id = _IOtherMasterService.Delete_OtherMaster(objMaster);
            if (Id > 0)
                return RedirectToAction("Index");
            else

                return View("Error");


        }

        #endregion


    }
}
