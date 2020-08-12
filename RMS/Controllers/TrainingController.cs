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
using System.Web.Script.Serialization;
using System.Text;
using RMS.Common.BusinessEntities;
using RMS.Helpers;
using System.IO;
using System.Collections;

namespace RMS.Controllers
{
    [CheckAccess]
    public class TrainingController : ErrorController
    {
        private List<TrainingModel> viewTrainingListModel = new List<TrainingModel>();
        private readonly ITrainingService _service;
        private int trainingID;
        int result;
        string message = string.Empty;
        
        /// <summary>
        /// Constructor for creating reference for Training repository
        /// </summary>
        /// <value>Interface TrainingModel</value>
        public TrainingController(ITrainingService service)
        {
            _service = service;
        }

        public TrainingController()
        {

        }

        public ActionResult TrainingModule()
        {   
            return View(CommonConstants.ViewTrainingModule);
        }


        /// <summary>
        /// Get the list of all request training raise by manager
        /// </summary>
        /// <value>TrainingTypeID</value>       
        public ActionResult ViewTechnicalTrainingRequest(string trainingTypeID)
        {
            //create model and set all required field
            TrainingModel objTrainingmodel = new TrainingModel(trainingTypeID);

            //pass model to get the list of all training raise by manager
            var model = _service.ViewTechnicalTraining(objTrainingmodel);
             if(Convert.ToInt32(trainingTypeID)==CommonConstants.SeminarsID)
             {
                    //pass model and get the list of all seminar training raise by manager
                    model = _service.ViewSeminarsTraining(objTrainingmodel);
             }
             if (Convert.ToInt32(trainingTypeID) == CommonConstants.KSSID)
             {
                 //pass model and get the list of all KSS training raise by manager
                 model = _service.ViewKSSTraining(objTrainingmodel);
             }
            ViewData[CommonConstants.TrainingType] = string.IsNullOrWhiteSpace(trainingTypeID) ? CommonConstants.TechnicalTrainingID.ToString() : trainingTypeID;
            return View(CommonConstants.viewtrainingrequest, model);
        }


        [HttpPost]
        public ActionResult Responsearr(string Raiseid, string trainingTypeID)
        {
            TempData["passedArray"] = Raiseid;
            return Json(new { ok = true, newurl = Url.Action("ViewTechnicalTrainingRequest", "Training", new { trainingTypeID = trainingTypeID }) });
           // var redirectUrl = new UrlHelper(Request.RequestContext).Action("ViewTechnicalTrainingRequest","Training");
            //return Json(new { Url = redirectUrl });
            
        }
        /// <summary>
        /// Get the list of all request training list and bind the grid.
        /// </summary>
        /// <value>TrainingTypeID</value>       
        public ActionResult ViewTechnicalTrainingRequestGrid(string trainingTypeID)
        {
            //create model and set all required field
            trainingID = Convert.ToInt32(trainingTypeID);
            TrainingModel objTrainingmodel = new TrainingModel(trainingTypeID);

            //check training type selected by user
            switch (trainingID)
            {

                case (CommonConstants.TechnicalTrainingID):
                    //pass model and get the list of all technical training raise by manager
                    viewTrainingListModel = _service.ViewTechnicalTraining(objTrainingmodel);
                    break;
                case (CommonConstants.SoftSkillsTrainingID):
                    //pass model and get the list of all Softskill training raise by manager
                    viewTrainingListModel = _service.ViewSoftSkillsTraining(objTrainingmodel);
                    break;
                case (CommonConstants.SeminarsID):
                    //pass model and get the list of all seminar training raise by manager
                    viewTrainingListModel = _service.ViewSeminarsTraining(objTrainingmodel);
                    break;
                case (CommonConstants.KSSID):
                    //pass model and get the list of all KSS training raise by manager
                    viewTrainingListModel = _service.ViewKSSTraining(objTrainingmodel);
                    break;
            }

            //set training type ID to be used for diaplaying training type column
            ViewData[CommonConstants.TrainingType] = string.IsNullOrWhiteSpace(trainingTypeID) ? CommonConstants.TechnicalTrainingID.ToString() : trainingTypeID;

            //set message on view page for required operation
            ViewData[CommonConstants.Result] = TempData[CommonConstants.Result];
            return View(CommonConstants.partialView_viewTrainingGrid, viewTrainingListModel);
        }


        /// <summary>
        /// Delete particular request training raised by manager
        /// </summary>
        /// <value>RaiseID</value>
        /// <value>TrainingTypeID</value>
        public ActionResult DeleteTrainingRequest(string raiseID, string trainingTypeID)
        {

            //initialize model
            int trainingID = Convert.ToInt32(trainingTypeID);
            TrainingModel RaiseTraining = new TrainingModel();
            RaiseTraining.RaiseID = Convert.ToInt32(raiseID);
            RaiseTraining.Status = "0";
            Master objmaster = new Master();
            RaiseTraining.UserEmpId = objmaster.GetEmployeeIDByEmailID();

            //check training type selected by user
            string result = string.Empty;
            switch (trainingID)
            {
                case (CommonConstants.TechnicalTrainingID):
                    //Delete technical training and send email to managers and trainee
                    result = _service.DeleteTechnicalSoftSkillsTraining(RaiseTraining, trainingID);
                    EmailHelper.SendMailForTechSoftSkillDeleted(RaiseTraining.RaiseID, _service.GetEmailIDForAppRejTechSoftSkill(RaiseTraining.RaiseID));
                    break;
                case (CommonConstants.SoftSkillsTrainingID):
                    //Delete technical training and send email to managers and trainee
                    result = _service.DeleteTechnicalSoftSkillsTraining(RaiseTraining, trainingID);
                    EmailHelper.SendMailForTechSoftSkillDeleted(RaiseTraining.RaiseID, _service.GetEmailIDForAppRejTechSoftSkill(RaiseTraining.RaiseID));
                    break;
                case (CommonConstants.SeminarsID):
                    //Delete seminar training and send email to managers and trainee
                    result = _service.DeleteSeminarsTraining(RaiseTraining);
                    EmailHelper.SendMailForSeminarDeleted(RaiseTraining.RaiseID, _service.GetEmailIDForAppRejTechSoftSkill(RaiseTraining.RaiseID));
                    break;
                case (CommonConstants.KSSID):
                    //pass model and delete kss training
                    result = _service.DeleteKSSTraining(RaiseTraining);
                    break;
            }

            //set message on view page returned from above operation
            TempData[CommonConstants.Result] = string.Format(CommonConstants.StringFormat, result, CommonConstants.Request_Deleted);
            return RedirectToAction(CommonConstants.ViewTechnicalTrainingRequestGrid, new { trainingTypeID = trainingTypeID });
        }

        /// <summary>
        /// Close the request training raised by manager
        /// </summary>
        /// <value>RaiseID</value>
        /// <value>TrainingTypeID</value>
        public ActionResult CloseTrainingRequest(string raiseID, string trainingTypeID)
        {
            int trainingID = Convert.ToInt32(trainingTypeID);

            //Initialize model
            TrainingModel RaiseTraining = new TrainingModel();
            RaiseTraining.RaiseID = Convert.ToInt32(raiseID);
            RaiseTraining.Status = CommonConstants.Closed;
            RaiseTraining.Comments = string.Empty;
            Master objmaster = new Master();
            RaiseTraining.UserEmpId = objmaster.GetEmployeeIDByEmailID();

            //pass model and delete technical training
            string result = string.Empty;
            result = _service.UpdateRaiseTrainingStatus(RaiseTraining);

            //set message on view page returned from above operation
            TempData[CommonConstants.Result] = string.Format(CommonConstants.StringFormat, result, CommonConstants.Request_Closed);
            return RedirectToAction(CommonConstants.ViewTechnicalTrainingRequestGrid, new { trainingTypeID = trainingTypeID });
        }

        #region RaiseTrainingRequest



        public ActionResult EditRaiseTrainingRequest(string pRaiseId, string ptrainingtypeid, string pOperation)
        {
            string Operation = CheckAccessAttribute.Decode(pOperation);
            int RaiseId = Convert.ToInt32(CheckAccessAttribute.Decode(pRaiseId));
            int trainingtypeid = Convert.ToInt32(CheckAccessAttribute.Decode(ptrainingtypeid));

            TrainingModel ObjRaiseTrainingModel = new TrainingModel();

            //if (!String.IsNullOrEmpty(Convert.ToString(Session["_RoleName"]))) \\ Removed by Venkatesh
            //{
            //    ObjRaiseTrainingModel.RoleName = Convert.ToString(Session["_RoleName"]);
            //}

            ObjRaiseTrainingModel.TrainingType = Convert.ToString(trainingtypeid);
            
            if (Convert.ToInt32(trainingtypeid) == CommonConstants.TechnicalTrainingID ||
                Convert.ToInt32(trainingtypeid) == CommonConstants.SoftSkillsTrainingID)
            {
                ObjRaiseTrainingModel = _service.GetTechnicalSoftSkills(RaiseId, trainingtypeid);
                //if (!String.IsNullOrEmpty(Convert.ToString(Session["_RoleName"]))) \\ Removed by Venkatesh
                //{
                //    ObjRaiseTrainingModel.RoleName = Convert.ToString(Session["_RoleName"]);
                //}
                // Changed by : Venkatesh  : Start
                ObjRaiseTrainingModel.RoleName = "";
                if (System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
                {
                    ArrayList arrRolesForUser = new ArrayList();
                    arrRolesForUser = (ArrayList)System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES];
                    if (arrRolesForUser.Contains("admin"))
                        ObjRaiseTrainingModel.RoleName = "Admin";
                }
                // Changed by : Venkatesh  : End

                ObjRaiseTrainingModel.Operation = Operation;
                BindDropDownList(ObjRaiseTrainingModel);
            }
            else if (Convert.ToInt32(trainingtypeid) == CommonConstants.SeminarsID)
            {
                ObjRaiseTrainingModel = _service.GetSeminarsTraining(RaiseId);
                //if (!String.IsNullOrEmpty(Convert.ToString(Session["_RoleName"])))
                //{
                //    ObjRaiseTrainingModel.RoleName = Convert.ToString(Session["_RoleName"]);
                //}
                // Changed by : Venkatesh  : Start
                ObjRaiseTrainingModel.RoleName = string.Empty;
                if (System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
                {
                    ArrayList arrRolesForUser = new ArrayList();
                    arrRolesForUser = (ArrayList)System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES];
                    if (arrRolesForUser.Contains("admin"))
                        ObjRaiseTrainingModel.RoleName = "Admin";
                }
                // Changed by : Venkatesh  : End

                //ObjRaiseTrainingModel.ListTrainingType = CommonRepository.FillMasterDropDownList(CommonConstants.TrainingType);     //Binding Training Type dropdown
                ObjRaiseTrainingModel.ListTrainingType = CommonRepository.FillMasterTrainingTypelist(CommonConstants.TrainingType, ObjRaiseTrainingModel.RoleName);
                ObjRaiseTrainingModel.ListRequestedBy = CommonRepository.FillEmployeesList();                                       //Binding RequestedBy dropdown
                ObjRaiseTrainingModel.objCommonModel = CommonRepository.FillPopUpEmployeeList("");

                int i = 0;
                foreach (var report in ObjRaiseTrainingModel.objCommonModel)
                {
                    string[] values = ObjRaiseTrainingModel.NameOfParticipantID.Split(',');
                    for (int a = 0; a < values.Length; a++)
                    {
                        if (values[a].Trim() == report.EmpID)
                        {
                            ObjRaiseTrainingModel.objCommonModel[i].Checked = true;
                            break;
                        }
                    }
                    i++;
                }
            }
            else if (Convert.ToInt32(trainingtypeid) == CommonConstants.KSSID)
            {
                ObjRaiseTrainingModel = _service.GetKSSTraining(RaiseId);
                //if (!String.IsNullOrEmpty(Convert.ToString(Session["_RoleName"])))
                //{
                //    ObjRaiseTrainingModel.RoleName = Convert.ToString(Session["_RoleName"]);
                //}
                // Changed by : Venkatesh  : Start
                ObjRaiseTrainingModel.RoleName = string.Empty;
                if (System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
                {
                    ArrayList arrRolesForUser = new ArrayList();
                    arrRolesForUser = (ArrayList)System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES];
                    if (arrRolesForUser.Contains("admin"))
                        ObjRaiseTrainingModel.RoleName = "Admin";
                }
                // Changed by : Venkatesh  : End
                //ObjRaiseTrainingModel.ListTrainingType = CommonRepository.FillMasterDropDownList(CommonConstants.TrainingType);     //Binding Training Type dropdown
                ObjRaiseTrainingModel.ListTrainingType = CommonRepository.FillMasterTrainingTypelist(CommonConstants.TrainingType, ObjRaiseTrainingModel.RoleName);
                ObjRaiseTrainingModel.ListRequestedBy = CommonRepository.FillEmployeesList();
                //ObjRaiseTrainingModel.ListKSSType = CommonRepository.FillMasterDropDownList(CommonConstants.KSSType);
                ObjRaiseTrainingModel.ListKSSType = CommonRepository.FillMasterKSSTypeList(CommonConstants.KSSType, Convert.ToInt32(Session["EmpID"]));
                ObjRaiseTrainingModel.objCommonModel = CommonRepository.FillPopUpEmployeeList("");

                int i = 0;
                foreach (var report in ObjRaiseTrainingModel.objCommonModel)
                {
                    string[] values = ObjRaiseTrainingModel.PresenterID.Split(','); 
                    for (int a = 0; a < values.Length; a++)
                    {
                        if (values[a].Trim() == report.EmpID)
                        {
                            ObjRaiseTrainingModel.objCommonModel[i].Checked = true;
                            break;
                        }
                    }
                    i++;
                }
            }

            ObjRaiseTrainingModel.RaiseID = RaiseId;
            ObjRaiseTrainingModel.Operation = Operation;
            ObjRaiseTrainingModel.TrainingType = Convert.ToString(trainingtypeid);
            //ObjRaiseTrainingModel.ListTrainingType = ObjRaiseTrainingModel.ListTrainingType.ToList().Select(x => new SelectList { Items = "1207", SelectedValues = "1207" });
            
            return View(CommonConstants.RaiseTrainingRequest, ObjRaiseTrainingModel);
        }

        public ActionResult ResetRequestedBy(string trainingtypeid)
        {
            TrainingModel ObjRaiseTrainingModel = new TrainingModel();

            ObjRaiseTrainingModel.TrainingType = trainingtypeid;

            if (!String.IsNullOrEmpty(Convert.ToString(Session["EmpID"])))    //ByDefault select login user name in "RequestBy" dropdown list
            {
                ObjRaiseTrainingModel.RequestedBy = Convert.ToString(Session["EmpID"]);
            }

            return RedirectToAction(CommonConstants.RaiseTrainingRequest, new { trainingTypeID = trainingtypeid, ResetValue = true });
        }

        public ActionResult RaiseTrainingRequest(string trainingtypeid, bool ResetValue = false)
        {
            TrainingModel ObjRaiseTrainingModel = new TrainingModel();

            //if (!String.IsNullOrEmpty(Convert.ToString(Session["_RoleName"])))
            //{
            //    ObjRaiseTrainingModel.RoleName = Convert.ToString(Session["_RoleName"]);
            //}
            // Changed by : Venkatesh  : Start
            ObjRaiseTrainingModel.RoleName = string.Empty;
            if (System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
            {
                ArrayList arrRolesForUser = new ArrayList();
                arrRolesForUser = (ArrayList)System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES];
                if (arrRolesForUser.Contains("admin"))
                    ObjRaiseTrainingModel.RoleName = "Admin";
            }
            // Changed by : Venkatesh  : End
            if (Convert.ToInt32(trainingtypeid) == CommonConstants.TechnicalTrainingID ||
                Convert.ToInt32(trainingtypeid) == CommonConstants.SoftSkillsTrainingID ||
                Convert.ToInt32(trainingtypeid) == CommonConstants.DefaultFlagZero)
            {

                BindDropDownList(ObjRaiseTrainingModel);
                ObjRaiseTrainingModel.objCommonModel = CommonRepository.FillPopUpEmployeeList("");
            }
            else
            {
                //ObjRaiseTrainingModel.ListTrainingType = CommonRepository.FillMasterDropDownList(CommonConstants.TrainingType);
                ObjRaiseTrainingModel.ListTrainingType = CommonRepository.FillMasterTrainingTypelist(CommonConstants.TrainingType, ObjRaiseTrainingModel.RoleName);
                ObjRaiseTrainingModel.objCommonModel = CommonRepository.FillPopUpEmployeeList("");

                if (Convert.ToInt32(trainingtypeid) == CommonConstants.SeminarsID)
                {
                    ObjRaiseTrainingModel.ListRequestedBy = CommonRepository.FillEmployeesList();

                    if (ResetValue == false)
                    {
                        ObjRaiseTrainingModel.TrainingType = trainingtypeid;
                    }
                    else
                    {
                        ObjRaiseTrainingModel.TrainingType = Convert.ToString(CommonConstants.DefaultFlagOne);
                    }
                    
                    if (!String.IsNullOrEmpty(Convert.ToString(Session["EmpID"])))    //ByDefault select login user name in "RequestBy" dropdown list
                    {
                        ObjRaiseTrainingModel.RequestedBy = Convert.ToString(Session["EmpID"]);
                    }
                }
                else if (Convert.ToInt32(trainingtypeid) == CommonConstants.KSSID)
                {
                    //ObjRaiseTrainingModel.ListKSSType = CommonRepository.FillMasterDropDownList(CommonConstants.KSSType);
                    ObjRaiseTrainingModel.ListKSSType = CommonRepository.FillMasterKSSTypeList(CommonConstants.KSSType, Convert.ToInt32(Session["EmpID"]));

                    if (ResetValue == false)
                    {
                        ObjRaiseTrainingModel.TrainingType = trainingtypeid;
                    }
                    else
                    {
                        ObjRaiseTrainingModel.TrainingType = Convert.ToString(CommonConstants.DefaultFlagTwo);
                    }
                }
            }

            //ObjRaiseTrainingModel.TrainingType = trainingtypeid;
            ObjRaiseTrainingModel.Operation = CommonConstants.Submit;

            return View(ObjRaiseTrainingModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RaiseTrainingRequest(TrainingModel ObjRaiseTrainingModel)
        {
            int trainingtypeid;
            //if (ModelState.IsValid)
            //{
            if (!String.IsNullOrEmpty(Convert.ToString(Session["EmpID"])))
                {
                    ObjRaiseTrainingModel.UserEmpId = Convert.ToInt32(Session["EmpID"]);
                }
            if (!String.IsNullOrEmpty(Convert.ToString(Session["UserEmailID"])))
            {
                ObjRaiseTrainingModel.CreatedByEmailId = Convert.ToString(Session["UserEmailID"]);
            }

                trainingtypeid = Convert.ToInt32(ObjRaiseTrainingModel.TrainingType);
                if (trainingtypeid == CommonConstants.TechnicalTrainingID || trainingtypeid == CommonConstants.SoftSkillsTrainingID)
                {
                    if (string.IsNullOrEmpty(ObjRaiseTrainingModel.TrainingNameOther))
                    {
                        ObjRaiseTrainingModel.TrainingNameOther = string.Empty;
                    }
                    result = _service.Save(ObjRaiseTrainingModel);

                    if (result == CommonConstants.DefaultFlagMinus)
                    {
                        message = "Training requisition already raised.";
                    }
                    else if (result != CommonConstants.DefaultFlagZero)
                    {
                        ObjRaiseTrainingModel.RaiseID = result;

                        if (ObjRaiseTrainingModel.Operation.ToLower() == CommonConstants.Update.ToLower())
                        {
                            //message = String.Empty;
                            //message = ObjRaiseTrainingModel.TrainingName == CommonConstants.TrainingTypeOther ? ObjRaiseTrainingModel.TrainingNameOther : ObjRaiseTrainingModel.TrainingName;
                            //message += " training details edited.";
                            EmailHelper.SendMailForTechSoftSkillEdit(Convert.ToInt32(ObjRaiseTrainingModel.UserEmpId), trainingtypeid, _service.GetEmailIDDetails(CommonConstants.DefaultFlagZero.ToString(), ObjRaiseTrainingModel.RaiseID));
                            message = "Training requisition edited.";
                        }
                        else if (ObjRaiseTrainingModel.Operation.ToLower() == CommonConstants.Submit.ToLower())
                        {
                            EmailHelper.SendMailForTechSoftSkill(Convert.ToInt32(ObjRaiseTrainingModel.UserEmpId), _service.GetEmailIDForAppRejTechSoftSkill(ObjRaiseTrainingModel.RaiseID));
                            message = "Training requisition submitted.";
                        }
                    }
                }
                else if (trainingtypeid == CommonConstants.KSSID)
                {
                    result = _service.SaveKSS(ObjRaiseTrainingModel);

                    if (result == CommonConstants.DefaultFlagMinus)
                    {
                        message = "Knowledge Sharing Session(" + ObjRaiseTrainingModel.Topic + ") already raised.";
                    }
                    else if (result != CommonConstants.DefaultFlagZero)
                    {
                        ObjRaiseTrainingModel.RaiseID = result;

                        if (ObjRaiseTrainingModel.Operation.ToLower() == CommonConstants.Submit.ToLower())
                        {
                            EmailHelper.SendMailForKSS(Convert.ToString(ObjRaiseTrainingModel.CreatedByEmailId), _service.GetEmailIDDetailsForKSS(ObjRaiseTrainingModel.PresenterID, ObjRaiseTrainingModel.RaiseID), ObjRaiseTrainingModel.Presenter,ObjRaiseTrainingModel.Type);

                            message = "Knowledge Sharing Session(" + ObjRaiseTrainingModel.Topic + ") details submitted.";
                        }
                        else if (ObjRaiseTrainingModel.Operation.ToLower() == CommonConstants.Update.ToLower())
                        {
                            EmailHelper.SendMailForKSSEdit(Convert.ToString(ObjRaiseTrainingModel.CreatedByEmailId), _service.GetEmailIDDetailsForKSS(ObjRaiseTrainingModel.PresenterID, ObjRaiseTrainingModel.RaiseID), ObjRaiseTrainingModel.Presenter,ObjRaiseTrainingModel.Type);

                            message = "Knowledge Sharing Session(" + ObjRaiseTrainingModel.Topic + ") details edited.";
                        }
                    }
                }
                else if (trainingtypeid == CommonConstants.SeminarsID)
                {
                    result = _service.SaveSeminars(ObjRaiseTrainingModel);

                    if (result == CommonConstants.DefaultFlagMinus)
                    {
                        message = "Seminar request already raised.";
                    }
                    else if (result != CommonConstants.DefaultFlagZero)
                    {
                        ObjRaiseTrainingModel.RaiseID = result;

                        if (ObjRaiseTrainingModel.Operation.ToLower() == CommonConstants.Submit.ToLower())
                        {
                            EmailHelper.SendMailForSeminar(_service.GetEmailIDDetails(ObjRaiseTrainingModel.RequestedBy, ObjRaiseTrainingModel.RaiseID), ObjRaiseTrainingModel.NameOfParticipant);

                            message = "Seminar request submitted.";
                        }
                        else if (ObjRaiseTrainingModel.Operation.ToLower() == CommonConstants.Update.ToLower())
                        {
                            EmailHelper.SendMailForSeminarEdit(_service.GetEmailIDDetails(ObjRaiseTrainingModel.RequestedBy, ObjRaiseTrainingModel.RaiseID));

                            message = ObjRaiseTrainingModel.SeminarsName + " details edited.";
                        }
                    }
                }

                TempData[CommonConstants.Result] = string.Format(CommonConstants.StringFormat, "", message);

                return RedirectToAction(CommonConstants.RaiseTrainingRequest, new { trainingTypeID = trainingtypeid });
            //}
            //return View(CommonConstants.RaiseTrainingRequest, ObjRaiseTrainingModel);
        }

        public void BindDropDownList(TrainingModel ObjRaiseTrainingModel)
        {
            int trainingtypeid = Convert.ToInt32(ObjRaiseTrainingModel.TrainingType);
            if (trainingtypeid == CommonConstants.TechnicalTrainingID)
            {
                ObjRaiseTrainingModel.ListTrainingName = CommonRepository.FillMasterDropDownList(CommonConstants.TrainingName);
            }
            else if (trainingtypeid == CommonConstants.SoftSkillsTrainingID)
            {
                ObjRaiseTrainingModel.ListTrainingName = CommonRepository.FillMasterDropDownList(CommonConstants.SoftSkills);
            }
            else
            {
                ObjRaiseTrainingModel.ListTrainingName = CommonRepository.GetEmptySelectList("");
            }
            //ObjRaiseTrainingModel.ListTrainingType = CommonRepository.FillMasterDropDownList(CommonConstants.TrainingType);
            ObjRaiseTrainingModel.ListTrainingType = CommonRepository.FillMasterTrainingTypelist(CommonConstants.TrainingType, ObjRaiseTrainingModel.RoleName);
            ObjRaiseTrainingModel.ListQuarter = CommonRepository.FillQuarterDropDownList(ObjRaiseTrainingModel.Operation);
            ObjRaiseTrainingModel.ListParticipants = CommonRepository.FillMasterDropDownList(CommonConstants.TrainingParticipants);
            ObjRaiseTrainingModel.ListPriority = CommonRepository.FillPriorityDropDownList();
            ObjRaiseTrainingModel.ListCategory = CommonRepository.FillMasterDropDownList(CommonConstants.TrainingCategory);
            ObjRaiseTrainingModel.ListRequestedBy = CommonRepository.FillEmployeesList();

            //ByDefault select login user name in "RequestBy" dropdown list
            if (!String.IsNullOrEmpty(Convert.ToString(Session["EmpID"])))
            {
                if (String.IsNullOrEmpty(ObjRaiseTrainingModel.RequestedBy))
                {
                    ObjRaiseTrainingModel.RequestedBy = Convert.ToString(Session["EmpID"]);
                }
            }
        }

       
        public PartialViewResult RaiseTrainingRequestAction(string value)
        {
            TrainingModel ObjRaiseTrainingModel = new TrainingModel();

            //RoleName
            //if (!String.IsNullOrEmpty(Convert.ToString(Session["_RoleName"])))
            //{
            //    ObjRaiseTrainingModel.RoleName = Convert.ToString(Session["_RoleName"]);
            //}
            // Changed by : Venkatesh  : Start
            if (System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES] != null)
            {
                ArrayList arrRolesForUser = new ArrayList();
                arrRolesForUser = (ArrayList)System.Web.HttpContext.Current.Session[AuthorizationManagerConstants.AZMAN_ROLES];
                if (arrRolesForUser.Contains("admin"))
                    ObjRaiseTrainingModel.RoleName = "Admin";
                else if (arrRolesForUser.Contains("manager"))
                    ObjRaiseTrainingModel.RoleName = "Manager";
            }
            // Changed by : Venkatesh  : End


            ObjRaiseTrainingModel.TrainingType = value;
            switch (Convert.ToInt32(ObjRaiseTrainingModel.TrainingType))
            {
                case (CommonConstants.TechnicalTrainingID):
                    BindDropDownList(ObjRaiseTrainingModel);
                    return PartialView("_TechnicalSoftSkillTrainingRequest", ObjRaiseTrainingModel);

                case (CommonConstants.SoftSkillsTrainingID):
                    BindDropDownList(ObjRaiseTrainingModel);
                    return PartialView("_TechnicalSoftSkillTrainingRequest", ObjRaiseTrainingModel);

                case (CommonConstants.SeminarsID):
                    ObjRaiseTrainingModel.ListRequestedBy = CommonRepository.FillEmployeesList();
                    ObjRaiseTrainingModel.objCommonModel = CommonRepository.FillPopUpEmployeeList("");
                    //ByDefault select login user name in "RequestBy" dropdown list
                    if (!String.IsNullOrEmpty(Convert.ToString(Session["EmpID"])))
                    {
                        ObjRaiseTrainingModel.RequestedBy = Convert.ToString(Session["EmpID"]);
                    }
                    return PartialView("_SeminarTrainingRequest", ObjRaiseTrainingModel);

                case (CommonConstants.KSSID):
                    //ObjRaiseTrainingModel.ListRequestedBy = CommonRepository.FillEmployeesList();
                    //ObjRaiseTrainingModel.ListKSSType = CommonRepository.FillMasterDropDownList(CommonConstants.KSSType);
                    ObjRaiseTrainingModel.ListKSSType = CommonRepository.FillMasterKSSTypeList(CommonConstants.KSSType, Convert.ToInt32(Session["EmpID"]));
                    ObjRaiseTrainingModel.objCommonModel = CommonRepository.FillPopUpEmployeeList("");
                    return PartialView("_KSSTrainingRequest", ObjRaiseTrainingModel);
            }
            return PartialView(ObjRaiseTrainingModel);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public int CheckduplicateForTrainingName(string trainingtypeid, string trainingothername)
        {
            TrainingModel ObjRaiseTrainingModel = new TrainingModel();

            ObjRaiseTrainingModel.TrainingType = trainingtypeid;
            ObjRaiseTrainingModel.TrainingNameOther = trainingothername;
            
            if (Convert.ToInt32(trainingtypeid) == CommonConstants.TechnicalTrainingID)
            {
                ObjRaiseTrainingModel.Category = CommonConstants.TrainingName;
            }
            else if(Convert.ToInt32(trainingtypeid) == CommonConstants.SoftSkillsTrainingID)
            {
                ObjRaiseTrainingModel.Category = CommonConstants.SoftSkills;
            }
            result = _service.CheckDuplication(ObjRaiseTrainingModel);
            return result;
        }

        #endregion RaiseTrainingRequest

        #region Approve Reject Training

        public ActionResult ApproveRejectTrainingRequest(string trainingTypeID)
        {
            TrainingModel objTrainingmodell = new TrainingModel(trainingTypeID);//CommonConstants.TechnicalTrainingID.ToString());
            objTrainingmodell.Status =Convert.ToString( CommonConstants.TrainingStatusOpen);
            var model = _service.ApproveRejectViewTechnicalTraining(objTrainingmodell);
            
            ViewData[CommonConstants.TrainingType] = string.IsNullOrWhiteSpace(trainingTypeID) ? CommonConstants.TechnicalTrainingID.ToString() : trainingTypeID;
            return View(CommonConstants.ApproveRejectTrainingRequest, model);
        }

        /// <summary>
        /// Get the list of all request training list and bind the grid.
        /// </summary>
        /// <value>TrainingTypeID</value>       
        public ActionResult ApproveRejectTrainingRequestGrid(string trainingTypeID)
        {
            //create model and set all required field
            trainingID = Convert.ToInt32(trainingTypeID);
            TrainingModel objTrainingmodel = new TrainingModel(trainingTypeID);
            objTrainingmodel.Status = Convert.ToString(CommonConstants.TrainingStatusOpen);
            //check training type selected by user
            switch (trainingID)
            {
                case (CommonConstants.TechnicalTrainingID):
                    //pass model and get the list of all technical training raise by manager
                    viewTrainingListModel = _service.ApproveRejectViewTechnicalTraining(objTrainingmodel);
                    break;
                case (CommonConstants.SoftSkillsTrainingID):
                    //pass model and get the list of all Softskill training raise by manager
                    viewTrainingListModel = _service.ApproveRejectViewTechnicalTraining(objTrainingmodel);
                    break;
                case (CommonConstants.SeminarsID):                   
                    //pass model and get the list of all Softskill training raise by manager
                    viewTrainingListModel = _service.ApproveRejectViewSeminarsTraining(objTrainingmodel);
                    break;
                case (CommonConstants.KSSID):
                    //pass model and get the list of all seminar training raise by manager
                    viewTrainingListModel = _service.ApproveRejectViewKSSTraining(objTrainingmodel);
                    break;
                //case (CommonConstants.):
                //    //pass model and get the list of all KSS training raise by manager
                //    viewTrainingListModel = _service.ViewKSSTraining(objTrainingmodel);
                //    break;
            }

            //set training type ID to be used for diaplaying training type column
            ViewData[CommonConstants.TrainingType] = string.IsNullOrWhiteSpace(trainingTypeID) ? CommonConstants.TechnicalTrainingID.ToString() : trainingTypeID;

            //set message on view page for required operation
            ViewData[CommonConstants.Result] = TempData[CommonConstants.Result];
            return View(CommonConstants.partialView_ApproveRejectGrid , viewTrainingListModel);
        }        

        /// <summary>
        /// Delete particular request training raised by manager
        /// </summary>
        /// <value>RaiseID</value>
        /// <value>TrainingTypeID</value>
        public ActionResult SaveApproveRejectTrainingRequest(string raiseID, string Comments, string Status, int trainingTypeid)
        {
            //initialize model
            //int trainingID = Convert.ToInt32(trainingTypeID);
            TrainingModel RaiseTraining = new TrainingModel();
            RaiseTraining.RaiseID = Convert.ToInt32(raiseID);
            RaiseTraining.Status = Status;
            RaiseTraining.Comments= Convert.ToString(Comments);
            Master objmaster = new Master();
            RaiseTraining.UserEmpId = objmaster.GetEmployeeIDByEmailID();
            trainingID = _service.SaveApproveRejectTrainingRequest(RaiseTraining);
            
            //check training type selected by user
            string result = string.Empty;
            string message = string.Empty;
            if (trainingID == 1)
            {
                if (Status == CommonConstants.Approved)
                {
                    if ((CommonConstants.TechnicalTrainingID == trainingTypeid || trainingTypeid == CommonConstants.SoftSkillsTrainingID))
                    {
                        message = CommonConstants.TrainingApproved;
                        EmailHelper.SendMailForTechSoftSkillApproved(RaiseTraining.RaiseID, _service.GetEmailIDForAppRejTechSoftSkill(RaiseTraining.RaiseID));
                    }// Seminar
                    else if (trainingTypeid == CommonConstants.SeminarsID)
                    {
                        message = CommonConstants.SeminarApproved;
                        EmailHelper.SendMailForSeminarApproved(_service.GetEmailIDForAppRejTechSoftSkill(RaiseTraining.RaiseID));
                    }
                }
                else// Rejection
                {
                    if (CommonConstants.TechnicalTrainingID == trainingTypeid || trainingTypeid == CommonConstants.SoftSkillsTrainingID)
                    {
                        message = CommonConstants.TrainingRejected;
                        EmailHelper.SendMailForTechSoftSkillRejected(RaiseTraining.RaiseID, _service.GetEmailIDForAppRejTechSoftSkill(RaiseTraining.RaiseID));
                    }//Seminar
                    else if (trainingTypeid == CommonConstants.SeminarsID)
                    {
                        message = CommonConstants.Seminarrejected;
                        EmailHelper.SendMailForSeminarRejected(_service.GetEmailIDForAppRejTechSoftSkill(RaiseTraining.RaiseID));
                    }
                }
                TempData[CommonConstants.ResultStyle] = string.Format(CommonConstants.StringFormat, result, CommonConstants.MessageStyleSuccess);
            }
            else
            {
                message = "Request failed!!";
                TempData[CommonConstants.ResultStyle] = string.Format(CommonConstants.StringFormat, result, CommonConstants.MessageStyleFail);
            }

            //KSS
            //else if (trainingTypeid == CommonConstants.KSSID && Status == CommonConstants.Approved)
            //{
            //    //pass model and delete kss training
            //    result = _service.DeleteKSSTraining(RaiseTraining);
            //}
            //else if (trainingTypeid == CommonConstants.KSSID && Status == CommonConstants.Rejected)
            //{
            //    //Delete seminar training and send email to managers and trainee
            //    result = _service.DeleteSeminarsTraining(RaiseTraining);
            //    EmailHelper.SendMailForSeminarDeleted(RaiseTraining.RaiseID, _service.GetEmailIDForAppRejTechSoftSkill(RaiseTraining.RaiseID));
            //}
            ////set message on view page returned from above operation
            
            TempData[CommonConstants.Result] = string.Format(CommonConstants.StringFormat, result, message);
            return RedirectToAction(CommonConstants.ApproveRejectTrainingRequestGrid, new { trainingTypeID = trainingTypeid });
        }
        #endregion

        #region Attendance
        /// <summary>
        /// Bind Technical Training on page load
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewAttendance()//string  TrainingTypeId)
        {
            AttendanceModel ObjRaiseTrainingModel = new AttendanceModel();
            ObjRaiseTrainingModel.objTrainingCourseModel = new TrainingCourseModel();
            ObjRaiseTrainingModel.TrainingTypeID = CommonConstants.TechnicalTrainingID ;
            ObjRaiseTrainingModel.ListTrainingName = _service.GetConfirmedTraining(Convert.ToInt32(ObjRaiseTrainingModel.TrainingTypeID));
            return View(ObjRaiseTrainingModel );
        }


        /// <summary>
        /// Bind Technical Training on page load
        /// </summary>
        /// <returns></returns>
        public ActionResult SetAttendance()
        {
            AttendanceModel ObjRaiseTrainingModel = new AttendanceModel();
            ObjRaiseTrainingModel.objTrainingCourseModel = new TrainingCourseModel();
            ObjRaiseTrainingModel.TrainingTypeID = CommonConstants.TechnicalTrainingID;
            ObjRaiseTrainingModel.ListTrainingName = _service.GetConfirmedTraining(Convert.ToInt32(ObjRaiseTrainingModel.TrainingTypeID));
            return View(ObjRaiseTrainingModel);
        }

       
        /// <summary>
        /// Bind Technical Training on page load
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintAttendance()
        {
            AttendanceModel ObjRaiseTrainingModel = new AttendanceModel();
            ObjRaiseTrainingModel.objTrainingCourseModel = new TrainingCourseModel();
            ObjRaiseTrainingModel.TrainingTypeID = CommonConstants.TechnicalTrainingID;
            ObjRaiseTrainingModel.ListTrainingName = _service.GetConfirmedTraining(Convert.ToInt32(ObjRaiseTrainingModel.TrainingTypeID));
            return View(ObjRaiseTrainingModel);
        }

        /// <summary>
        /// Bind Traininig based on Training Type
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ActionResult OnRadioChangeBindConfirmedTraining(int value, string PageName)
        {
            AttendanceModel ObjRaiseTrainingModel = new AttendanceModel();
            ObjRaiseTrainingModel.TrainingTypeID = value;
            if (ObjRaiseTrainingModel.TrainingTypeID == 1207 || ObjRaiseTrainingModel.TrainingTypeID == 1208)
            {
                ObjRaiseTrainingModel.ListTrainingName = _service.GetConfirmedTraining(Convert.ToInt32(value));
                ObjRaiseTrainingModel.TrainingType = value.ToString();
                if (PageName == "Set")
                {
                    return PartialView("_SetAttendanceTechSoft", ObjRaiseTrainingModel);
                }
                else if (PageName == "Print")
                {
                    ObjRaiseTrainingModel.objTrainingCourseModel = new TrainingCourseModel();
                    return PartialView("_PrintAttendanceTechSoft", ObjRaiseTrainingModel);
                }
                else return Json(ObjRaiseTrainingModel,JsonRequestBehavior.AllowGet);

            }
            else
                if (ObjRaiseTrainingModel.TrainingTypeID == 1209)
                {
                ObjRaiseTrainingModel.ListTrainingName = _service.GetSeminarKSSList(Convert.ToInt32(value));
                    ObjRaiseTrainingModel.TrainingType = value.ToString();
                    if (PageName == "Set")
                    {
                        return PartialView("_SetAttendanceSeminar", ObjRaiseTrainingModel);
                    }
                    else if(PageName == "Print")
                    {
                        return PartialView("_PrintSeminar", ObjRaiseTrainingModel);
                    }
                }
            if (ObjRaiseTrainingModel.TrainingTypeID == 1210)
            {
                ObjRaiseTrainingModel.ListTrainingName = _service.GetSeminarKSSList(Convert.ToInt32(value));
                ObjRaiseTrainingModel.TrainingType = value.ToString();
                return PartialView("_PrintKss", ObjRaiseTrainingModel);
            }


                return null;
        }
        
        #region Technical \ Soft Skill
        public ActionResult OnTrainingChange_GetAttendanceDetail(string _CourseId)
        {
            AttendanceModel ObjAttendanceModel = new AttendanceModel();            
            //Get Course Detail
            ObjAttendanceModel.objTrainingCourseModel = _service.GetTrainingCourses(Convert.ToInt32(_CourseId));
            ObjAttendanceModel.ListAttendanceDate = SetAttendanceDate(ObjAttendanceModel.objTrainingCourseModel.AttendanceDates);

            //Load Training Type & Training Name            
            ObjAttendanceModel.TrainingTypeID = ObjAttendanceModel.objTrainingCourseModel.TrainingTypeID;
            ObjAttendanceModel.ListTrainingName = _service.GetConfirmedTraining(Convert.ToInt32(ObjAttendanceModel.TrainingTypeID));
            ObjAttendanceModel.TrainingName = _CourseId;
            ObjAttendanceModel.Mode = 1;

            return View("ViewAttendance", ObjAttendanceModel);
        }

        SelectList SetAttendanceDate(List<DateTime> attendanceDates)
        {
            SelectListItem selListItem;
            List<SelectListItem> newList = new List<SelectListItem>();
            newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "" });
            
            if (attendanceDates != null)
            {
                foreach (var item in attendanceDates)
                {
                    if (item <= DateTime.Now)
                    {
                        selListItem = new SelectListItem { Value = item.ToString("dd-MMM-yyyy").Trim(), Text = item.ToString("dd-MMM-yyyy").Trim() };
                        newList.Add(selListItem);
                    }
                }
            }
            return new SelectList(newList, "Value", "Text");
        }


        SelectList BindAttendanceDate(DateTime dtStart, DateTime dtEnd)
        {
            SelectListItem selListItem;
            List<SelectListItem> newList = new List<SelectListItem>();
            newList.Add(new SelectListItem { Selected = true, Text = "Select", Value = "" });
            DateTime dt = new DateTime();
            dt = dtStart;
            while (dt <= dtEnd)
            {
                // Checking attendance date should not be greater than current date 
                if (dt <= DateTime.Now)
                {
                    selListItem = new SelectListItem() { Value = dt.ToString("dd-MMM-yyyy").Trim(), Text = dt.ToString("dd-MMM-yyyy").Trim() };
                    newList.Add(selListItem);
                    dt = dt.AddDays(1);
                }
                else
                    break;
            }
            return new SelectList(newList, "Value", "Text");
        }
        
        public ActionResult AddAttendance(int CourseId, string AddAttendanceDate, string TrainingTypeID)
        {
            AttendanceModel ObjAttendanceModel = new AttendanceModel();
            ObjAttendanceModel.CourseId = CourseId;
            ObjAttendanceModel.AttendanceDate = AddAttendanceDate;
            ObjAttendanceModel.TrainingTypeID = Convert.ToInt32(TrainingTypeID);
 
            ObjAttendanceModel.dtAttendance = CreateAttendanceGriddt(ObjAttendanceModel);
            return PartialView("_AddAttendance", ObjAttendanceModel);
        }

        public ActionResult ViewAllAttendance(string AddAttendanceDate, int CourseId, int TrainingTypeID)
        {
            AttendanceModel ObjAttendanceModel = new AttendanceModel();
            ObjAttendanceModel.CourseId = CourseId;
            //ObjAttendanceModel.AttendanceDate = AddAttendanceDate;

            DataSet ds = _service.GetEmpPresenteeAll(ObjAttendanceModel.CourseId, ObjAttendanceModel.TrainingTypeID );
             if (Convert.ToString( ds.Tables[0].Rows[0]["status"]) == "1")
             {
                 ObjAttendanceModel.dtAttendance = ds.Tables[1];
                 if (ds.Tables.Count > 1)
                 {
                     if (ds.Tables[2].Rows.Count > 0)
                     {
                         ObjAttendanceModel.NoOfDaysFilled = Convert.ToInt32(ds.Tables[2].Rows[0]["NoOfDaysFilled"]);
                         ObjAttendanceModel.NoOfdays = Convert.ToInt32(ds.Tables[2].Rows[0]["NoOfdays"]);

                         if (Convert.ToInt32(ds.Tables[2].Rows[0]["Insert_Update"]) > 0)
                             ObjAttendanceModel.FbkSaveUpdateMode = "U";
                         else
                             ObjAttendanceModel.FbkSaveUpdateMode = "I";
                     }
                 }
             }
             else
             {
                 TempData[CommonConstants.ResultPartial] = string.Format(CommonConstants.StringFormat, "", "Employee Attendance has not been added as yet.");
             }
             ObjAttendanceModel.TrainingTypeID = TrainingTypeID;
             ObjAttendanceModel.TrainingID = CourseId;
            return PartialView("_ViewAttendance", ObjAttendanceModel);
        }        
        
        private DataTable CreateAttendanceGriddt(AttendanceModel ObjAttendanceModel)
        {
            AttendanceModel objAtn = new AttendanceModel();
            if(ObjAttendanceModel.TrainingTypeID == 1207 || ObjAttendanceModel.TrainingTypeID == 1208)
            {
            objAtn.dtAttendance = _service.GetEmpPresentee(ObjAttendanceModel.CourseId, ObjAttendanceModel.AttendanceDate, "N");
            }
            else if (ObjAttendanceModel.TrainingTypeID == 1209)
            {
                objAtn.dtAttendance = _service.GetEmpPresenteeSeminar(ObjAttendanceModel.CourseId,ObjAttendanceModel.AttendanceDate, "N");
            }
            #region Commented Code to make Datatabe 
            //DataTable dt = new DataTable();
            //dt.Columns.Add(new DataColumn("Empid"));
            //dt.Columns.Add(new DataColumn("EmpName"));
            //dt.Columns.Add(new DataColumn("Days"));
            //dt.Columns.Add(new DataColumn("Values"));

            //DataRow row = dt.NewRow();
            //row["Empid"] = "1";
            //row["EmpName"] = "Marc";
            //row["Days"] = "Clifton";
            //row["Values"] = "1";
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row["Empid"] = "2";
            //row["EmpName"] = "Marc";
            //row["Days"] = "Clifton";
            //row["Values"] = "0";
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row["Empid"] = "3";
            //row["EmpName"] = "Marc";
            //row["Days"] = "Clifton";
            //row["Values"] = "1";
            //dt.Rows.Add(row);
         
            //    List<DynamicGrid> dtList = dt.AsEnumerable().Select(row1 => new DynamicGrid
            //{
            //    Empid = Convert.ToString(row1["Empid"]),
            //    EmpName = Convert.ToString(row1["EmpName"]),
            //    Days = Convert.ToString(row1["Days"]),
            //    values = Convert.ToString(row1["values"]),

            //    //...
            //}).ToList();

            //ObjAttendanceModel.objDynamicGrid = choiceList ;
            #endregion
            return objAtn.dtAttendance;
        }

        public ActionResult OnTrainingChange_SetAttendanceDetail(string _CourseId, string _TrainingTypeID)
        {
            AttendanceModel ObjAttendanceModel = new AttendanceModel();            
            if (_TrainingTypeID == "1207" || _TrainingTypeID == "1208")
            {
            //Get Course Detail
            ObjAttendanceModel.objTrainingCourseModel = _service.GetTrainingCourses(Convert.ToInt32(_CourseId));
            ObjAttendanceModel.SelectedAttendanceDate = _service.GetAttendanceDates(Convert.ToInt32(_CourseId), Convert.ToInt32(ObjAttendanceModel.objTrainingCourseModel.TrainingTypeID));
            ObjAttendanceModel.ListAttendanceDate = SetAttendanceDates(ObjAttendanceModel.objTrainingCourseModel.TrainingStartDate, ObjAttendanceModel.objTrainingCourseModel.TrainingEndDate);

            //Load Training Type & Training Name            
            ObjAttendanceModel.TrainingTypeID = ObjAttendanceModel.objTrainingCourseModel.TrainingTypeID;
            ObjAttendanceModel.ListTrainingName = _service.GetConfirmedTraining(Convert.ToInt32(ObjAttendanceModel.TrainingTypeID));
            ObjAttendanceModel.TrainingName = _CourseId;
            ObjAttendanceModel.NoOfdays = ObjAttendanceModel.objTrainingCourseModel.NoOfdays;
            if (ObjAttendanceModel.objTrainingCourseModel.TrainingModeID == 1909)
            {
                ObjAttendanceModel.objTrainingCourseModel.TrainerName = ObjAttendanceModel.objTrainingCourseModel.TrainerNameInternal.ToString();
            }
            else if (ObjAttendanceModel.objTrainingCourseModel.TrainingModeID == 1910)
            {
                ObjAttendanceModel.objTrainingCourseModel.TrainerName = ObjAttendanceModel.objTrainingCourseModel.TrainerName.ToString();
            }
            

            }
            else if (_TrainingTypeID == "1209" || _TrainingTypeID == "1210")
            {
                ObjAttendanceModel = _service.GetSeminarKSSCourse(Convert.ToInt32(_CourseId));
                ObjAttendanceModel.SelectedAttendanceDate = _service.GetAttendanceDates(Convert.ToInt32(_CourseId), Convert.ToInt32(_TrainingTypeID));
                //DateTime TrainingStartDate = ObjAttendanceModel.TrainingStartDate;
                //DateTime TrainingEndDate = ObjAttendanceModel.TrainingEndDate;
                ObjAttendanceModel.ListAttendanceDate = SetAttendanceDates(ObjAttendanceModel.TrainingStartDate, ObjAttendanceModel.TrainingEndDate);

                //Load Training Type & Training Name            
                ObjAttendanceModel.TrainingTypeID = Convert.ToInt32(_TrainingTypeID);
                ObjAttendanceModel.ListTrainingName = _service.GetSeminarKSSList(Convert.ToInt32(ObjAttendanceModel.TrainingTypeID));
                ObjAttendanceModel.TrainingName = _CourseId;
            }
            
            return View("SetAttendance", ObjAttendanceModel);
        }


        public ActionResult OnTrainingChange_PrintAttendanceDetail(string _CourseId, string _TrainingTypeID)
        {
            AttendanceModel ObjAttendanceModel = new AttendanceModel();
            if (_TrainingTypeID == "1207" || _TrainingTypeID == "1208")
            {
            //Get Course Detail
            ObjAttendanceModel.objTrainingCourseModel = _service.GetTrainingCourses(Convert.ToInt32(_CourseId));
            ObjAttendanceModel.SelectedAttendanceDate = _service.GetAttendanceDates(Convert.ToInt32(_CourseId), Convert.ToInt32(ObjAttendanceModel.objTrainingCourseModel.TrainingTypeID));

            //Load Training Type & Training Name            
            ObjAttendanceModel.TrainingTypeID = ObjAttendanceModel.objTrainingCourseModel.TrainingTypeID;
            ObjAttendanceModel.ListTrainingName = _service.GetConfirmedTraining(Convert.ToInt32(ObjAttendanceModel.TrainingTypeID));
            ObjAttendanceModel.TrainingName = _CourseId;

            if (ObjAttendanceModel.objTrainingCourseModel.TrainingModeID == 1909)
            {
                ObjAttendanceModel.objTrainingCourseModel.TrainerName = ObjAttendanceModel.objTrainingCourseModel.TrainerNameInternal.ToString();
            }
            else if (ObjAttendanceModel.objTrainingCourseModel.TrainingModeID == 1910)
            {
                ObjAttendanceModel.objTrainingCourseModel.TrainerName = ObjAttendanceModel.objTrainingCourseModel.TrainerName.ToString();
            }
            
            
            }
            else if (_TrainingTypeID == "1209" || _TrainingTypeID == "1210")
            {
                ObjAttendanceModel = _service.GetSeminarKSSCourse(Convert.ToInt32(_CourseId));
                if (_TrainingTypeID == "1209")
                {
                    ObjAttendanceModel.SelectedAttendanceDate = _service.GetAttendanceDates(Convert.ToInt32(_CourseId), Convert.ToInt32(_TrainingTypeID));
                }
                else
                {
                    ObjAttendanceModel = LoadSemiKssDetails(Convert.ToInt32(_TrainingTypeID), Convert.ToInt32(_CourseId));
                    ObjAttendanceModel.SelectedAttendanceDate = ObjAttendanceModel.SelectedAttendanceDate.Select( x=> new SelectListItem() { Value = ObjAttendanceModel.TrainingStartDate.ToString(), Text = ObjAttendanceModel.TrainingStartDate.ToString() });
                }
                //Load Training Type & Training Name            
                ObjAttendanceModel.TrainingTypeID = Convert.ToInt32(_TrainingTypeID);
                ObjAttendanceModel.ListTrainingName = _service.GetSeminarKSSList(Convert.ToInt32(ObjAttendanceModel.TrainingTypeID));
                ObjAttendanceModel.TrainingName = _CourseId;
            }
            return View("PrintAttendance", ObjAttendanceModel);
        }

        SelectList SetAttendanceDates(DateTime dtStart, DateTime dtEnd)
        {
            SelectListItem selListItem;
            List<SelectListItem> newList = new List<SelectListItem>();
            DateTime dt = new DateTime();
            dt = dtStart;
            while (dt <= dtEnd)
            {
               selListItem = new SelectListItem() { Value = dt.ToString("dd-MMM-yyyy").Trim(), Text = dt.ToString("dd-MMM-yyyy").Trim()};
               newList.Add(selListItem);
               dt = dt.AddDays(1);
            }
            return new SelectList(newList, "Value", "Text");
        }
        #endregion

        #region Seminar \ KSS
        public ActionResult LoadSemiKSSName(int TrainingTypeId)
        {
            AttendanceModel ObjAttendanceModel = new AttendanceModel();
            ObjAttendanceModel.TrainingTypeID = TrainingTypeId;
            if (ObjAttendanceModel.TrainingTypeID == 1207 || ObjAttendanceModel.TrainingTypeID == 1208)
                ObjAttendanceModel.ListTrainingName = _service.GetConfirmedTraining(Convert.ToInt32(TrainingTypeId));
            else if (ObjAttendanceModel.TrainingTypeID == 1209  || ObjAttendanceModel.TrainingTypeID == 1210)
                ObjAttendanceModel.ListTrainingName = _service.GetSeminarKSSList(Convert.ToInt32(TrainingTypeId));

            if (ObjAttendanceModel.TrainingTypeID == 1209 )
                return PartialView("_AddSeminar", ObjAttendanceModel);
            else if  (ObjAttendanceModel.TrainingTypeID == 1210)
                return PartialView("_AddKSS", ObjAttendanceModel);

            return null;
        }

        public ActionResult LoadSemiKss(int TrainingTypeId, int RaiseId)
        {            
            AttendanceModel ObjAttendanceModel = new AttendanceModel();
            ObjAttendanceModel = LoadSemiKssDetails(TrainingTypeId, RaiseId);
            
            if (ObjAttendanceModel.TrainingTypeID == 1209)
                return PartialView("_AddSeminar", ObjAttendanceModel);

            else if (ObjAttendanceModel.TrainingTypeID == 1210)
            {
                // Rakesh : Issue : 16/June/2016 : Starts 
                ObjAttendanceModel.TrainingID = RaiseId;
                //End
                return PartialView("_AddKSS", ObjAttendanceModel);
            }

            return null;
        }

        AttendanceModel LoadSemiKssDetails(int TrainingTypeId, int RaiseId)
        {
            AttendanceModel ObjAttendanceModel = new AttendanceModel();
            TrainingModel ObjRaiseTrainingModel = new TrainingModel();

            if (TrainingTypeId == 1209)
            {
                ObjRaiseTrainingModel = _service.GetSeminarsTraining(RaiseId);
                ObjAttendanceModel.TrainingName = ObjRaiseTrainingModel.SeminarsName.ToString();
            }
            else if (TrainingTypeId == 1210)
            {
                ObjRaiseTrainingModel = _service.GetKSSTraining(RaiseId);
                ObjAttendanceModel.TrainingName = ObjRaiseTrainingModel.Topic.ToString();
            }

            ObjAttendanceModel.TrainingStartDate = ObjRaiseTrainingModel.Date;
            ObjAttendanceModel.TrainingEndDate = ObjRaiseTrainingModel.SeminarsEndDate;

            //Default Value
            ObjAttendanceModel = SetDefaultValueTechSoft(ObjAttendanceModel);

            //Load Training Type & Training Name            
            ObjAttendanceModel.TrainingTypeID = Convert.ToInt32(ObjRaiseTrainingModel.TrainingType);
            ObjAttendanceModel.ListTrainingName = _service.GetSeminarKSSList(Convert.ToInt32(ObjRaiseTrainingModel.TrainingType));
            //ObjAttendanceModel.TrainingName = RaiseId.ToString();
            ObjAttendanceModel.RaiseId = RaiseId;

            
            ObjAttendanceModel.SelectedAttendanceDate = _service.GetAttendanceDates(Convert.ToInt32(RaiseId), Convert.ToInt32(TrainingTypeId));
            
            // Load Employee List
            //ObjAttendanceModel.dtAttendance = _service.GetEmpPresenteeSeminar(ObjAttendanceModel.RaiseId, "N");

            ObjAttendanceModel.PresenterID = ObjRaiseTrainingModel.PresenterID;
            ObjAttendanceModel.Presenter = ObjRaiseTrainingModel.Presenter;
            ObjAttendanceModel.AttenderId = ObjRaiseTrainingModel.AttenderId;
            ObjAttendanceModel.Attender = ObjRaiseTrainingModel.Attender;

            ObjAttendanceModel.objCommonModel = CommonRepository.FillPopUpEmployeeList("");

            int i = 0;
            foreach (var report in ObjAttendanceModel.objCommonModel)
            {
                if (ObjAttendanceModel.AttenderId != null)
                {
                    string[] values = ObjAttendanceModel.AttenderId.Split(',');
                    for (int a = 0; a < values.Length; a++)
                    {
                        if (values[a].Trim() == report.EmpID)
                        {
                            ObjAttendanceModel.objCommonModel[i].Checked = true;
                            break;
                        }
                    }
                    i++;
                }
            }
            return ObjAttendanceModel;
        }

        //Set Default Value for Tech-Soft Training7
        AttendanceModel SetDefaultValueTechSoft(AttendanceModel ObjAttendanceModel)
        {
            ObjAttendanceModel.TrainerName = string.Empty;
            ObjAttendanceModel.objTrainingCourseModel = new TrainingCourseModel();
            ObjAttendanceModel.objTrainingCourseModel.TrainerName = "";
            ObjAttendanceModel.objTrainingCourseModel.TrainingStartDate = DateTime.MinValue;
            ObjAttendanceModel.objTrainingCourseModel.TrainingEndDate = DateTime.MinValue;
            ObjAttendanceModel.NoOfdays=0;
            ObjAttendanceModel.Mode= 1;
            return ObjAttendanceModel;
        }

        #endregion

        [HttpPost]
        public ActionResult SaveAttendance(FormCollection objf)
        {
            AttendanceModel objAttendanceModel = new AttendanceModel();            
            objAttendanceModel.CreatedBy = Convert.ToInt32(Session["EmpID"]);

            if ((Convert.ToString(objf["category"]) == "1207") || (Convert.ToString(objf["category"]) == "1208"))
            {
                objAttendanceModel = SaveTechnicalSoftskill(objAttendanceModel, objf);
            }
            else if (Convert.ToString(objf["category"]) == "1209" || (Convert.ToString(objf["category"]) == "1210"))
            {
                objAttendanceModel = SaveSeminar(objAttendanceModel, objf);
            }
            return View(CommonConstants.ViewAttendance, objAttendanceModel);
        }

        AttendanceModel SaveTechnicalSoftskill(AttendanceModel objAttendanceModel, FormCollection objf)
        {
            string CourseId = Convert.ToString(objf["TrainingName"]);
            objAttendanceModel.CourseId = Convert.ToInt32(objf["TrainingName"]);
            objAttendanceModel.TrainingTypeID = Convert.ToInt32(objf["category"]);
            objAttendanceModel.dropEmpIdAll = Convert.ToString(objf["dropout"]);
            objAttendanceModel.EmpIdAll = Convert.ToString(objf["Feedback"]);
            
            // Save Attendance data
            if (Convert.ToString(objf["Mode"]) == "1")
            {
                objAttendanceModel.EmpIdAll = Convert.ToString(objf["SelectedChoices"]);
                objAttendanceModel.AttendanceDate = Convert.ToString(objf["AttendanceDate"]);
                int output = _service.SaveAttendance(objAttendanceModel);
                TempData[CommonConstants.Result] = string.Format(CommonConstants.StringFormat, "", "Technical or SoftSkill Data Saved Successfully.");
            }
            // Save Feedback Sent data
            else
            {
                objAttendanceModel.FbkSaveUpdateMode = Convert.ToString(objf["FbkSaveUpdateMode"]);
                objAttendanceModel.FeedbackLastDate = Convert.ToDateTime(objf["FeedbackLastDate"]);
                objAttendanceModel.FeedbackMailNotToSend = Convert.ToBoolean(objf["FeedbackMailNotToSend"].Split(',')[0]);
                
                if (Convert.ToString(objf["dropBtn"]) == "Send Dropout")
                {
                    objAttendanceModel.AttendanceType = "Dropout";
                }
                else
                {
                    objAttendanceModel.AttendanceType = "Feedback";
                }

                DataSet ds = _service.SaveFeedbackSent(objAttendanceModel);

                //For Feedback email
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToString(ds.Tables[0].Rows[0]["Emailid"]) != "")
                    {
                        EmailHelper.SendMailToProvideFeedback(objAttendanceModel.CourseId, ds);
                    }
                }

                //For Dropout email
                if (ds.Tables[1].Rows.Count > 0)
                {
                    if (Convert.ToString(ds.Tables[1].Rows[0]["Emailid"]) != "")
                    {
                        EmailHelper.SendMailToDropOut(objAttendanceModel.CourseId, ds);
                    }
                }

                TempData[CommonConstants.Result] = string.Format(CommonConstants.StringFormat, "", "Feedback form Sent Successfully.");
            }

            //Load Data
            //AttendanceModel ObjAttendanceModel = new AttendanceModel();
            //Get Course Detail
            objAttendanceModel.objTrainingCourseModel = _service.GetTrainingCourses(Convert.ToInt32(CourseId));
            //objAttendanceModel.ListAttendanceDate = BindAttendanceDate(objAttendanceModel.objTrainingCourseModel.TrainingStartDate, objAttendanceModel.objTrainingCourseModel.TrainingEndDate);
            objAttendanceModel.ListAttendanceDate = SetAttendanceDate(objAttendanceModel.objTrainingCourseModel.AttendanceDates);
            //Load Training Type & Training Name            
            objAttendanceModel.TrainingTypeID = objAttendanceModel.objTrainingCourseModel.TrainingTypeID;
            objAttendanceModel.ListTrainingName = _service.GetConfirmedTraining(Convert.ToInt32(objAttendanceModel.TrainingTypeID));
            objAttendanceModel.TrainingName = CourseId;
            objAttendanceModel.Mode = 1;// Convert.ToInt32(Convert.ToString(objf["Mode"]));
            objAttendanceModel.AttendanceDate = "";
            return objAttendanceModel;

        }

        AttendanceModel SaveSeminar(AttendanceModel objAttendanceModel, FormCollection objf)
        {
            string CourseId = Convert.ToString(objf["TrainingName"]);
            objAttendanceModel.CourseId = Convert.ToInt32(objf["TrainingNameSemiKss"]);
            objAttendanceModel.TrainingTypeID = Convert.ToInt32(objf["category"]);

            //Poonam 18052016 - Starts
            //Issue : Seminar adding attendance gives error
             if(Convert.ToString(objf["submitkss"]) == "Submit")
            {
            //Ishwar Patil 04042016 Start Desc: AttendanceDate is null
            //objAttendanceModel.AttendanceDate = objf["AttendanceDate"];
            objAttendanceModel.AttendanceDate = objf["TrainingStartDate"];
            //Ishwar Patil 04042016 End
            }
            else if (Convert.ToString(objf["submitBtn"]) == "Submit")
            {
                objAttendanceModel.AttendanceDate = objf["AttendanceDate"];
            }
            //Poonam 18052016 - ENds

            if (Convert.ToString(objf["category"]) == "1209")
                objAttendanceModel.EmpIdAll = Convert.ToString(objf["SelectedChoices"]); 
            else if (Convert.ToString(objf["category"]) == "1210")
                objAttendanceModel.EmpIdAll = Convert.ToString(objf["AttenderId"]);
            
            int output = _service.SaveAttendanceSemiKss(objAttendanceModel);
            if (Convert.ToString(objf["category"]) == "1209")
                TempData[CommonConstants.Result] = string.Format(CommonConstants.StringFormat, "", "Seminar Data Saved Successfully.");
            else if (Convert.ToString(objf["category"]) == "1210")
                TempData[CommonConstants.Result] = string.Format(CommonConstants.StringFormat, "", "KSS Data Saved Successfully.");
            //Load Data
            objAttendanceModel = LoadSemiKssDetails(objAttendanceModel.TrainingTypeID, objAttendanceModel.CourseId);
            objAttendanceModel.TrainingID = Convert.ToInt32(objf["TrainingNameSemiKss"]);
            return objAttendanceModel;
        }

        [HttpPost]
        public ActionResult SetAttendanceDates(FormCollection objf)
        {
            AttendanceModel objAttendanceModel = new AttendanceModel();
            objAttendanceModel.CreatedBy = Convert.ToInt32(Session["EmpID"]);

            if ((Convert.ToString(objf["category"]) == "1207") || (Convert.ToString(objf["category"]) == "1208"))
            {
                objAttendanceModel = SetTechnicalSoftskill(objAttendanceModel, objf);
                
            }
            else if (Convert.ToString(objf["category"]) == "1209" || (Convert.ToString(objf["category"]) == "1210"))
            {
                objAttendanceModel = SetSeminar(objAttendanceModel, objf);
                objAttendanceModel.TrainingTypeID = Convert.ToInt32(objf["category"]);
                objAttendanceModel.ListTrainingName = _service.GetSeminarKSSList(Convert.ToInt32(objAttendanceModel.TrainingTypeID));
                objAttendanceModel.TrainingName = Convert.ToString(objf["TrainingName"]);
            }
            return View(CommonConstants.SetAttendance, objAttendanceModel);
            
        }

        AttendanceModel SetSeminar(AttendanceModel objAttendanceModel, FormCollection objf)
        {
            string CourseId = Convert.ToString(objf["TrainingName"]);
            objAttendanceModel.TrainingTypeID = Convert.ToInt32(objf["category"]);
            objAttendanceModel.CourseId = Convert.ToInt32(objf["TrainingName"]);

            objAttendanceModel.SetAttendanceDates = Convert.ToString(objf["SelectedChoices"]);
            string TrainingName = Convert.ToString(objf["TrainingName"]);
            string startDate = Convert.ToString(objf["startDate"]);
            string endDate = Convert.ToString(objf["endDate"]);
            
            int output = _service.SetAttendance(objAttendanceModel);
            TempData[CommonConstants.Result] = string.Format(CommonConstants.StringFormat, "", "Attendance Date Saved Successfully.");


            objAttendanceModel = _service.GetSeminarKSSCourse(Convert.ToInt32(CourseId));
            //objAttendanceModel.TrainingTypeID = Convert.ToInt32(objAttendanceModel.TrainingTypeID);
            


            return objAttendanceModel;


        }

        AttendanceModel SetTechnicalSoftskill(AttendanceModel objAttendanceModel, FormCollection objf)
        {
            string CourseId = Convert.ToString(objf["TrainingName"]);
            objAttendanceModel.CourseId = Convert.ToInt32(objf["TrainingName"]);
            objAttendanceModel.TrainingTypeID = Convert.ToInt32(objf["category"]);
            objAttendanceModel.SetAttendanceDates = Convert.ToString(objf["SelectedChoices"]);
            string TrainingName = Convert.ToString(objf["TrainingName"]);
            string Presenter = Convert.ToString(objf["TrainerName"]);
            string startDate = Convert.ToString(objf["startDate"]);
            string endDate = Convert.ToString(objf["endDate"]);
            // Save Attendance data
            //if (Convert.ToString(objf["Mode"]) == "1")
            //{
            int output = _service.SetAttendance(objAttendanceModel);
            TempData[CommonConstants.Result] = string.Format(CommonConstants.StringFormat, "", "Attendance Date Set Successfully.");
            //}
           
            //Load Data
            //AttendanceModel ObjAttendanceModel = new AttendanceModel();

            //Get Course Detail
            objAttendanceModel.objTrainingCourseModel = _service.GetTrainingCourses(Convert.ToInt32(CourseId));
            
            //Load Training Type & Training Name            
            objAttendanceModel.TrainingTypeID = objAttendanceModel.objTrainingCourseModel.TrainingTypeID;
            objAttendanceModel.ListTrainingName = _service.GetConfirmedTraining(Convert.ToInt32(objAttendanceModel.TrainingTypeID));
            objAttendanceModel.TrainingName = CourseId;

            return objAttendanceModel;

        }

        public ActionResult LoadFullSeminar(int TrainingTypeId, int RaiseId)
        {            
            AttendanceModel ObjAttendanceModel = new AttendanceModel();
            ObjAttendanceModel = LoadSemiKssDetails(TrainingTypeId, RaiseId); 
            return View("ViewAttendance", ObjAttendanceModel);
        }


        /// <summary>
        /// Excel-Export
        /// </summary>
        public ExcelFileResult ExportExcel(int TrainingTypeId, int RaiseId)
        {
            DataTable dt = _service.GetAttendanceReport(TrainingTypeId, RaiseId);

            // DataTable dt = -- > get your data
            ExcelFileResult actionResult = new ExcelFileResult(dt) { FileDownloadName = string.Format("AttendanceReport{0}.xls", TrainingTypeId.ToString() + "" + RaiseId.ToString()) };
            return actionResult;
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ExportDateToExcel(FormCollection objf)
        {
            int CourseId = Convert.ToInt32(objf["TrainingName"]);
            string Presenter = Convert.ToString(objf["TrainerName"]);
            string startDate = Convert.ToString(objf["startDate"]);
            string endDate = Convert.ToString(objf["endDate"]);
            string selectedDates = Convert.ToString(objf["SelectedChoices"]);


            AttendanceModel objAttendanceModel = new AttendanceModel();
            objAttendanceModel.CourseId = Convert.ToInt32(objf["TrainingName"]);
            objAttendanceModel.TrainingTypeID = Convert.ToInt32(objf["category"]);

            string fileName = ConfigurationManager.AppSettings["ReportLogoPath"].ToString();

            if (objAttendanceModel.TrainingTypeID == 1207 || objAttendanceModel.TrainingTypeID == 1208)
            {
                objAttendanceModel.objTrainingCourseModel = _service.GetTrainingCourses(Convert.ToInt32(objAttendanceModel.CourseId));
                objAttendanceModel.ListTrainingName = _service.GetConfirmedTraining(Convert.ToInt32(objAttendanceModel.TrainingTypeID));

                List<Employee> NominationDetails = _service.GetNominationDetailsByCourseId(CourseId, objAttendanceModel.TrainingTypeID);

                string TrainingName = objAttendanceModel.objTrainingCourseModel.CourseName;

                string[] datepart = selectedDates.Split(',');
                int datecount = datepart.Count();
                int attendanceSpan = datecount + 1;

                //Start image copying code if not exist in downloads folder or user logged in
                //string root = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                //string downloadPath = root + "\\Downloads";
                // string root = @"~/Downloads";
                //string downloadPath = "~/Downloads";

                //string downloadPath = @"C:\\Users\\ishwar.patil\\Downloads";
                //string result;

                //result = Path.GetFileName(fileName);

                //string SourceFile = fileName;
                //string DestinationFile = downloadPath +"\\" + result;
                //string DestinationFile = fileName;

                //string imagePath = "";
                //if (!System.IO.File.Exists(DestinationFile))
                //{
                //    System.IO.File.Copy(SourceFile, DestinationFile, true);
                //}
                //End image copying code if not exist in downloads folder or user logged in

                StringBuilder StrExport = new StringBuilder();

                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV ><table><tr><td></td><td colspan=" + attendanceSpan + " align=center><img src=" + fileName + "></img></td></tr></table></DIV>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr></table>");
                StrExport.Append("<table><tr><td></td><td><table border=1>");
                StrExport.Append("<tr><td colspan=" + attendanceSpan + " align=center><b>Attendance Sheet</b></td></tr>");
                StrExport.Append("<tr><td><b>Training Name</b></td>");
                StrExport.Append("<td colspan=" + datecount + " align=center><b>" + TrainingName + "</b></td></tr>");
                StrExport.Append("<tr><td></td><td colspan=" + datecount + "></td></tr>");
                StrExport.Append("<tr><td><b>Training Start Date</b></td>");
                StrExport.Append("<td colspan=" + datecount + " align=center><b>" + startDate + "</b></td></tr>");
                StrExport.Append("<tr><td></td><td colspan=" + datecount + "></td></tr>");
                StrExport.Append("<tr><td><b>Training End Date</b></td>");
                StrExport.Append("<td colspan=" + datecount + " align=center><b>" + endDate + "</b></td></tr>");
                StrExport.Append("<tr><td></td><td colspan=" + datecount + "></td></tr>");
                StrExport.Append("<tr><td><b>Trainer Name</b></td>");
                StrExport.Append("<td colspan=" + datecount + " align=center><b>" + Presenter + "</b></td></tr>");
                StrExport.Append("<tr><td></td><td colspan=" + datecount + "></td></tr>");
                StrExport.Append("<tr><td><b>Name of Participant</b></td>");
                StrExport.Append("<td colspan=" + datecount + " align=center><b>Signature</b></td></tr>");
                StrExport.Append("<tr><td></td><td colspan=" + datecount + "></td></tr>");
                StrExport.Append("<tr><td></td>");
                foreach (string date in datepart)
                {
                    StrExport.Append("<td width='300px;'  align=center><b>" + date + "</b></td>");
                }

                StrExport.Append("</tr>");
                foreach (var item in NominationDetails)
                {
                    StrExport.Append("<tr>");
                    StrExport.Append("<td>" + item.FirstName + " " + item.LastName + "</td>");
                    for (int count = 0; count < datecount; count++)
                    {
                        StrExport.Append("<td></td>");
                    }
                    StrExport.Append("</tr>");
                }
                StrExport.Append("</table></td></tr></table>");
                StrExport.Append("</div>");
                StrExport.Append("</body></html>");
                string strFile = "Attendance.xls";
                Response.ClearContent();
                Response.ClearHeaders();
                Response.BufferOutput = true;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
                Response.AddHeader("Content-Type", "application/vnd.ms-excel");
                Response.Write(StrExport.ToString());
                Response.Flush();
                Response.End();
            }
            else if (objAttendanceModel.TrainingTypeID == 1209)
            {
                int TrainingTypeID = objAttendanceModel.TrainingTypeID;
                objAttendanceModel = _service.GetSeminarKSSCourse(Convert.ToInt32(CourseId));
                objAttendanceModel.ListTrainingName = _service.GetSeminarKSSList(TrainingTypeID);

                List<Employee> NominationDetails = _service.GetNominationDetailsByCourseId(CourseId, TrainingTypeID);

                string TrainingName = objAttendanceModel.TrainingNameSemiKss;
               
                string[] datepart = selectedDates.Split(',');
                int datecount = datepart.Count();
                int attendanceSpan = datecount + 1;


                //Start image copying code if not exist in downloads folder or user logged in
                ////string root = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                ////string downloadPath = root + "\\Downloads";

                ////string downloadPath = @"C:\\Users\\ishwar.patil\\Downloads";


                //string fileName = Server.MapPath("~/Content/css/Images/ravelogo.png");
                //string fileName = "http://rav-vm-srv-096:8024/Content/css/Images/ravelogo.png"; // Server.MapPath("~/Content/css/Images/ravelogo.png");
                ////string result;

                ////result = Path.GetFileName(fileName);

                ////string SourceFile = fileName;
                ////string DestinationFile = downloadPath + "\\" + result;
                //string DestinationFile = fileName;

                //string imagePath = "";
                ////if (!System.IO.File.Exists(DestinationFile))
                ////{
                ////    System.IO.File.Copy(SourceFile, DestinationFile, true);
                ////}
                //End image copying code if not exist in downloads folder or user logged in


                StringBuilder StrExport = new StringBuilder();

                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV ><table><tr><td></td><td colspan=" + attendanceSpan + " align=center><img src=" + fileName + "></img></td></tr></table></DIV>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table><tr><td></td><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr></table>");
                StrExport.Append("<table><tr><td></td><td><table border=1>");
                StrExport.Append("<tr><td colspan=" + attendanceSpan + " align=center><b>Attendance Sheet</b></td></tr>");
                StrExport.Append("<tr><td><b>Training Name</b></td>");
                StrExport.Append("<td colspan=" + datecount + " align=center><b>" + TrainingName + "</b></td></tr>");
                StrExport.Append("<tr><td></td><td colspan=" + datecount + "></td></tr>");
                StrExport.Append("<tr><td><b>Training Start Date</b></td>");
                StrExport.Append("<td colspan=" + datecount + " align=center><b>" + startDate + "</b></td></tr>");
                StrExport.Append("<tr><td></td><td colspan=" + datecount + "></td></tr>");
                StrExport.Append("<tr><td><b>Training End Date</b></td>");
                StrExport.Append("<td colspan=" + datecount + " align=center><b>" + endDate + "</b></td></tr>");
                StrExport.Append("<tr><td></td><td colspan=" + datecount + "></td></tr>");
                
                StrExport.Append("<tr><td><b>Name of Participant</b></td>");
                StrExport.Append("<td colspan=" + datecount + " align=center><b>Signature</b></td></tr>");
                StrExport.Append("<tr><td></td><td colspan=" + datecount + "></td></tr>");
                StrExport.Append("<tr><td></td>");
                foreach (string date in datepart)
                {
                    StrExport.Append("<td width='300px;'  align=center><b>" + date + "</b></td>");
                }

                StrExport.Append("</tr>");
                foreach (var item in NominationDetails)
                {
                    StrExport.Append("<tr>");
                    StrExport.Append("<td>" + item.FirstName + " " + item.LastName + "</td>");
                    for (int count = 0; count < datecount; count++)
                    {
                        StrExport.Append("<td></td>");
                    }
                    StrExport.Append("</tr>");
                }
                StrExport.Append("</table></td></tr></table>");
                StrExport.Append("</div>");
                StrExport.Append("</body></html>");
                string strFile = "Attendance.xls";
                Response.ClearContent();
                Response.ClearHeaders();
                Response.BufferOutput = true;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
                Response.AddHeader("Content-Type", "application/vnd.ms-excel");
                Response.Write(StrExport.ToString());
                Response.Flush();
                Response.End();
            }
            else if (objAttendanceModel.TrainingTypeID == 1210)
            {
                int TrainingTypeID = objAttendanceModel.TrainingTypeID;
                objAttendanceModel = LoadSemiKssDetails(objAttendanceModel.TrainingTypeID,CourseId); // _service.GetSeminarKSSCourse(Convert.ToInt32(CourseId));
                objAttendanceModel.ListTrainingName = _service.GetSeminarKSSList(TrainingTypeID);

                //List<Employee> NominationDetails = _service.GetNominationDetailsByCourseId(CourseId, TrainingTypeID);

                string TrainingName = objAttendanceModel.TrainingName;
                Presenter = objAttendanceModel.Presenter;

                string[] datepart = selectedDates.Split(',');
                int datecount = datepart.Count();
                int attendanceSpan = datecount + 1;


                //Start image copying code if not exist in downloads folder or user logged in
                ////string root = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                ////string downloadPath = root + "\\Downloads";

                ////string downloadPath = @"C:\\Users\\ishwar.patil\\Downloads";


                //string fileName = Server.MapPath("~/Content/css/Images/ravelogo.png");
                //string fileName = "http://rav-vm-srv-096:8024/Content/css/Images/ravelogo.png"; // Server.MapPath("~/Content/css/Images/ravelogo.png");
                ////string result;

                ////result = Path.GetFileName(fileName);

                ////string SourceFile = fileName;
                ////string DestinationFile = downloadPath + "\\" + result;
                //string DestinationFile = fileName;

                //string imagePath = "";
                ////if (!System.IO.File.Exists(DestinationFile))
                ////{
                ////    System.IO.File.Copy(SourceFile, DestinationFile, true);
                ////}
                //End image copying code if not exist in downloads folder or user logged in


                StringBuilder StrExport = new StringBuilder();

                StrExport.Append(@"<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:w='urn:schemas-microsoft-com:office:excel' xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>");
                StrExport.Append(@"<body lang=EN-US style='mso-element:header' id=h1><span style='mso--code:DATE'></span><div class=Section1>");
                StrExport.Append("<DIV ><table><tr><td></td><td colspan=" + attendanceSpan + " align=center><img src=" + fileName + "></img></td></tr></table></DIV>");
                StrExport.Append("<DIV  style='font-size:12px;'>");
                StrExport.Append("<table><tr><td></td><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr><tr><td></td></tr></table>");
                StrExport.Append("<table><tr><td></td><td><table border=1>");
                StrExport.Append("<tr><td colspan=" + attendanceSpan + " align=center><b>Attendance Sheet</b></td></tr>");
                StrExport.Append("<tr><td><b>Training Name</b></td>");
                StrExport.Append("<td colspan=" + datecount + " align=center><b>" + TrainingName + "</b></td></tr>");
                StrExport.Append("<tr><td></td><td colspan=" + datecount + "></td></tr>");
                StrExport.Append("<tr><td><b>Training Start Date</b></td>");
                StrExport.Append("<td colspan=" + datecount + " align=center><b>" + startDate + "</b></td></tr>");
                StrExport.Append("<tr><td></td><td colspan=" + datecount + "></td></tr>");
                
                StrExport.Append("<tr><td><b>Presenter</b></td>");
                StrExport.Append("<td colspan=" + datecount + " align=center><b>" + Presenter + "</b></td></tr>");
                StrExport.Append("<tr><td></td><td colspan=" + datecount + "></td></tr>");
                StrExport.Append("<tr><td><b>Name of Participant</b></td>");
                StrExport.Append("<td colspan=" + datecount + " align=center><b>Signature</b></td></tr>");
                StrExport.Append("<tr><td></td><td colspan=" + datecount + "></td></tr>");
                StrExport.Append("<tr><td></td>");
                foreach (string date in datepart)
                {
                    StrExport.Append("<td width='300px;'  align=center><b>" + date + "</b></td>");
                }

                StrExport.Append("</tr>");
                //foreach (var item in NominationDetails)
                //{
                for (int i = 0; i < 20; i++) //gridlines should appear for empty cell (added 20 rows random number)
                {
                    StrExport.Append("<tr>");
                    //    StrExport.Append("<td>" + item.FirstName + " " + item.LastName + "</td>");
                    //    for (int count = 0; count < datecount; count++)
                    //    {
                    StrExport.Append("<td></td><td></td>");
                    //    }
                    StrExport.Append("</tr>");
                }
                //}
                StrExport.Append("</table></td></tr></table>");
                StrExport.Append("</div>");
                StrExport.Append("</body></html>");
                string strFile = "Attendance.xls";
                Response.ClearContent();
                Response.ClearHeaders();
                Response.BufferOutput = true;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + strFile);
                Response.AddHeader("Content-Type", "application/vnd.ms-excel");
                Response.Write(StrExport.ToString());
                Response.Flush();
                Response.End();
            }

            return View(CommonConstants.PrintAttendance,objAttendanceModel);
        }
        #endregion

        #region Self Learning

        public ActionResult MyTraining()
        {
            int empId = 0;

            if (!String.IsNullOrEmpty(Convert.ToString(Session["EmpID"])))
                empId = Convert.ToInt32(Session["EmpID"]);

            MyTrainingView myTraining = new MyTrainingView();
            myTraining.MyTrainingModel = new MyTrainingModel();
            myTraining.MyTrainingModel.Operation = CommonConstants.Submit;
            myTraining.MyTrainings = _service.GetMyTrainings(empId);
            return View(myTraining);
        }

        [HttpPost]
        public ActionResult MyTraining(MyTrainingView myTraining, HttpPostedFileBase fileCertificate)
        {
            int empId = 0;
            if (!String.IsNullOrEmpty(Convert.ToString(Session["EmpID"])))
                empId = Convert.ToInt32(Session["EmpID"]);

            if (fileCertificate != null && fileCertificate.ContentLength > 0)
            {
                var fileName = Path.GetFileNameWithoutExtension(fileCertificate.FileName);
                string extension = Path.GetExtension(fileCertificate.FileName);

                var fileGuid = Guid.NewGuid() + extension;

                var path = Path.Combine(Server.MapPath("~/RMS_Files/SelfTrainingCertificates"), fileGuid);
                fileCertificate.SaveAs(path);

                myTraining.MyTrainingModel.CertificateGuid = fileGuid.ToString();
                myTraining.MyTrainingModel.CertificateName = fileName.ToString();

            }

            result = _service.SaveMyTraining(myTraining.MyTrainingModel, empId);

            if (result != CommonConstants.DefaultFlagZero)
            {
                myTraining.MyTrainingModel.TrainingId = result;
                message = "Your training is submitted successfully.";
                TempData[CommonConstants.Result] = string.Format(CommonConstants.StringFormat, "", message);

                MyTrainingView newView = new MyTrainingView();
                newView.MyTrainingModel = new MyTrainingModel();
                newView.MyTrainingModel.Operation = CommonConstants.Submit;
                newView.MyTrainings = _service.GetMyTrainings(empId);

                return RedirectToAction(CommonConstants.MyTrainingView, newView);
            }
            else
            {
                message = "Error while submitting your training.";
                TempData[CommonConstants.Result] = string.Format(CommonConstants.StringFormat, "", message);
                TempData[CommonConstants.ResultStyle] = "MessageStyleFail";
                return View(CommonConstants.MyTrainingView, myTraining);
            }
        }

        [HttpGet]
        public ActionResult DeleteMyTraining(int trainingId, string certificate)
        {
            int deleteStatus = 0;
            deleteStatus = _service.DeleteMyTraining(trainingId);
            if (deleteStatus == 1)
            {
                string fullFilePath = Path.Combine(Server.MapPath("~/RMS_Files/SelfTrainingCertificates"), certificate); 
                if (System.IO.File.Exists(fullFilePath))
                {
                    System.IO.File.Delete(fullFilePath);
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditMyTraining(string trainingId)
        {
            MyTrainingView myTrainingView = new MyTrainingView();
            int id = Convert.ToInt32(CheckAccessAttribute.Decode(trainingId));
            int empId = 0;
            if (!String.IsNullOrEmpty(Convert.ToString(Session["EmpID"])))
                empId = Convert.ToInt32(Session["EmpID"]);

            myTrainingView.MyTrainingModel = _service.GetMyTrainingDetails(id);
            myTrainingView.MyTrainingModel.Operation = CommonConstants.Update;
            myTrainingView.MyTrainings = _service.GetMyTrainings(empId);

            return View(CommonConstants.MyTrainingView, myTrainingView);
        }

        [HttpGet]
        public FileResult DownloadFile(string file)
        {
            string fullFilePath = Path.Combine(Server.MapPath("~/RMS_Files/SelfTrainingCertificates"), file);
            byte[] fileBytes = System.IO.File.ReadAllBytes(fullFilePath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fullFilePath);
        }

        #endregion
    }
}

