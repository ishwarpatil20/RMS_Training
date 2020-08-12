using Domain.Entities;
using RMS.Common.Constants;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 
namespace RMS.Controllers
{
    public class VendorMasterController : ErrorController
    {
        //
        // GET: /VendorMaster/
        private readonly ITrainingService _service;

        public VendorMasterController(ITrainingService service)
        {
            _service = service;
        }

        public ActionResult VendorDetails()
        {
            List<VendorModel> lstVendorModel = new List<VendorModel>();

            DataSet dsVendor = _service.GetTrainingVendorDetails();
            for (int i = 0; i < dsVendor.Tables[0].Rows.Count; i++)
            {
                VendorModel objVendorModel = new VendorModel();
                objVendorModel.VendorId = Convert.ToInt32(dsVendor.Tables[0].Rows[i]["VendorID"]);
                objVendorModel.VendorName = dsVendor.Tables[0].Rows[i]["VendorName"].ToString();
                objVendorModel.VendorEmailId = dsVendor.Tables[0].Rows[i]["VendorEmail"].ToString();
                objVendorModel.ContactPersonName = dsVendor.Tables[0].Rows[i]["ContactPerson"].ToString();
                objVendorModel.ContactPersonNumber = dsVendor.Tables[0].Rows[i]["ContactPersonNumber"].ToString();
                objVendorModel.Expertise = dsVendor.Tables[0].Rows[i]["Expertise"].ToString();

                lstVendorModel.Add(objVendorModel);
            }

            return View(lstVendorModel);
        }

        [HttpGet]
        public ActionResult EditVendorDetails(int VendorId)
        {
            if (VendorId != 0)
            {

                VendorModel objVendorModel = new VendorModel();
                DataSet dsVendorDetails = _service.GetVendorDetailsByVendorId(VendorId);

                objVendorModel.VendorId = VendorId;
                objVendorModel.VendorName = dsVendorDetails.Tables[0].Rows[0]["VendorName"].ToString();
                objVendorModel.VendorEmailId = dsVendorDetails.Tables[0].Rows[0]["VendorEmail"].ToString();
                objVendorModel.ContactPersonName = dsVendorDetails.Tables[0].Rows[0]["ContactPerson"].ToString();
                objVendorModel.ContactPersonNumber = dsVendorDetails.Tables[0].Rows[0]["ContactPersonNumber"].ToString();
                objVendorModel.Expertise = dsVendorDetails.Tables[0].Rows[0]["Expertise"].ToString();

                return View(objVendorModel);
            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public ActionResult SaveVendorDetails(VendorModel objVendorModel, string Command)
        {
            

            if (ModelState.IsValid)
            {
                if (Command == "Save")
                {
                    
                    int status = 0;
                    if (objVendorModel.VendorId != 0)
                    {
                        status = _service.UpdateVendorDetails(objVendorModel);
                    }
                    else
                    {
                        status = _service.SaveVendorDetails(objVendorModel);
                    }

                    if (status == 1)
                    {
                        TempData["Result"] = "Vendor details saved successfully";
                    }
                }
                else if (Command == "Cancel")
                {
                    return RedirectToAction("VendorDetails");
                }
                return RedirectToAction("VendorDetails");
            }
            else
            {
                TempData[CommonConstants.Message] = "Contact Person Number should be number with comma seperated field";
                return RedirectToAction("EditVendorDetails");
            }
        }


        [HttpPost]
        public JsonResult doesEmailExists(VendorModel objVendorModel)
        {
            //string duplicateState = string.Empty;
            var duplicateState = _service.GetDuplicateStatusOfEmialId(objVendorModel.VendorEmailId);
            return Json(duplicateState == "0");
        }


        public ActionResult DeleteVendorDetails(int VendorId)
        {
            int deleteStatus = _service.DeleteVendorDetails(VendorId);
            if (deleteStatus == -1)
            {
                TempData["Result"] = "Vendor cannot be deleted";
            }
            else
            {
                TempData["Result"] = "Vendor deleted successfully";
            }

            return RedirectToAction("VendorDetails");
        }

    }
}
