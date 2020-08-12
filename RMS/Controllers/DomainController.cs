using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Interfaces;
using Infrastructure;
using Domain.Entities;
namespace RMS.Controllers
{
    public class DomainController : Controller
    {

        IDomainRepository<T_Domain> _iDomainRepository;
        ISubDomainRepository<T_SubDomain> _iSubDomainRepository;
        //
        // GET: /Domain/

        public DomainController(IDomainRepository<T_Domain> iDomainRepository, ISubDomainRepository<T_SubDomain> iSubDomainRepository)
        {
            _iDomainRepository = iDomainRepository;
            _iSubDomainRepository = iSubDomainRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        #region Get Json Data Domains,Domain_SubDomain
        /// <summary>
        /// GetDomains
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDomains()
        {
            ResultData objData = new ResultData();
            try
            {
                objData.IsSucess = true;
                objData.JsonData = _iDomainRepository.GetAll().ToList().Where(p => p.isActive == true).ToList();
                return Json(objData, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                objData.IsSucess = false;
                return Json(objData, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// GetDomain Subdomain
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDomains_SubDomain(int DomainId, int SubDomainId)
        {
            ResultData objData = new ResultData();
            try
            {
                objData.IsSucess = true;
                objData.JsonData = _iDomainRepository.GetDomain(DomainId, SubDomainId).ToList();
                return Json(objData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                objData.IsSucess = false;
                return Json(objData, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion

    }
}
