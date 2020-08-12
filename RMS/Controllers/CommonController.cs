using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Domain.Entities;
using Services.Interfaces;
using Services;
using System.Configuration;
using RMS.Common.Constants;
using RMS.Common;
using RMS.Helpers;
using Infrastructure;
using System.IO;
using System.Text;
using RMS.Common.BusinessEntities.Menu;

namespace RMS.Controllers
{
    public class CommonController : ErrorController
    {
        //private List<CommonModel> ListEmployeeListModel = new List<CommonModel>();
        //private CommonModel objCommonModel = new CommonModel();

        private readonly ICommonService _CommonService;

        public CommonController(ICommonService service)
        {
            _CommonService = service;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public string Index(List<Domain.Entities.CommonModel> model)
        {
            string pEmpID = string.Empty;
            string pEmpName = string.Empty;

            foreach (CommonModel revInfo in model)
            {
                if (revInfo.Checked == true)
                {
                    pEmpID +=  "," + revInfo.EmpID ;
                    pEmpName += ", " +revInfo.FirstName + " " + revInfo.LastName ;
                }
            }

            return pEmpID.Remove(0, 1) + "~" + pEmpName.Remove(0, 2);
        }

        [HttpGet]
        public FileResult DownloadFile(string filePath, string fileName)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@Server.MapPath(filePath));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        //[HttpGet]
        [OutputCache(Duration = 86400, VaryByParam = "none")]
        public ActionResult EmployeeList()
        {
            return PartialView("EmployeeList", CommonRepository.FillPopUpEmployeeList(""));
        }
   

        [HttpGet]
        public ActionResult DeleteFile(string fileId,string filePath, string fileName, string physicalFile, string module , string entityId, string targetId, string dir)
        {
            FileUploadModel objFileUploadModel = new FileUploadModel();
            
            if (module == "CourseContent")
            {
                return RedirectToAction("UpdateFileDetail", "TrainingCourse", new { module = module, entityId = entityId, fileName = physicalFile, fullFile = filePath, targetId = targetId, dir = dir });
            }
            else if (module == "CourseDAR")
            {
                return RedirectToAction("UpdateFileDetail", "TrainingCourse", new { module = module, entityId = entityId, fileName = physicalFile, fullFile = filePath, targetId = targetId, dir = dir });
            }
            else if (module == "CourseTrainer")
            {
                return RedirectToAction("UpdateFileDetail", "TrainingCourse", new { module = module, entityId = entityId, fileName = physicalFile, fullFile = filePath, targetId = targetId, dir = dir });
            }
            else if (module == "CourseInvoice")
            {
                return RedirectToAction("UpdateInvoiceDetail", "TrainingCourse", new { fileId = fileId, module = module, entityId = entityId, fileName = physicalFile, fullFile = filePath, targetId = targetId, dir = dir });
            }
            return PartialView(CommonConstants.FileHelperView, objFileUploadModel);
        }

        [CheckAccess]        
        [ChildActionOnly]
        public ActionResult GetMenu()
        {
            List<Menu> MenuList = _CommonService.GetAuthoriseMenuList(Convert.ToInt32( Session["EmpID"]));
            HttpContext context = System.Web.HttpContext.Current;            
            bool IsAuthoriseUser = MenuList.Exists(m => string.Concat(m.baseUrl, m.PageURL, Convert.ToString(context.Request.Url.Query)).Equals(Convert.ToString(context.Request.Url.OriginalString), StringComparison.InvariantCultureIgnoreCase));
            return PartialView("_menu", MenuList);           
        }

        public ActionResult Home()
        {
            return View("home");
        }

    }
}
