using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Infrastructure;
using RMS.Common.Constants;
using Services;
using Services.Interfaces;

namespace RMS.ModelBinder
{
    public class CreateCourseModelBinder : IModelBinder
    {
        private readonly ITrainingService _service;

        public CreateCourseModelBinder() {
            _service = new TrainingService(new TrainingRepository());
        }

        //public CreateCourseModelBinder(ITrainingService service)
        //{
        //    _service = service;
        //}

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            TrainingCourseModel objCourse = new TrainingCourseModel();
            return objCourse;
        }
    }

    public class EditCourseModelBinder : IModelBinder
    {
        private readonly ITrainingService _service;

        public EditCourseModelBinder() {
            _service = new TrainingService(new TrainingRepository());
        }

        //public EditCourseModelBinder(ITrainingService service)
        //{
        //    _service = service;
        //}
        
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            int courseId = Convert.ToInt32(request.Form.Get("CourseID"));

            TrainingCourseModel objCourse = _service.GetTrainingCourses(courseId);
            DateTime trainingEndDate = (Convert.ToString(request.Form.Get("TrainingEndDate")) == null) ? objCourse.TrainingEndDate : Convert.ToDateTime(request.Form.Get("TrainingEndDate"));


            objCourse.TrainingModeID = Convert.ToInt32(request.Form.Get("TrainingModeID"));
            objCourse.VendorID = Convert.ToInt32(request.Form.Get("VendorID"));
            objCourse.VendorEmailId = Convert.ToString(request.Form.Get("VendorEmailId"));
            objCourse.TechnicalPanelIds = Convert.ToString(request.Form.Get("TechnicalPanelID"));
            objCourse.TrainerName = Convert.ToString(request.Form.Get("TrainerName"));
            objCourse.TrainingStartDate = Convert.ToDateTime(request.Form.Get("TrainingStartDate"));
            objCourse.TrainingEndDate = Convert.ToDateTime(request.Form.Get("TrainingEndDate"));
            objCourse.TrainingComments = Convert.ToString(request.Form.Get("TrainingComments"));
            objCourse.NoOfdays = Convert.ToSingle (request.Form.Get("NoOfdays"));
            objCourse.TotalTrainigHours = Convert.ToSingle(request.Form.Get("TotalTrainingHours"));
            objCourse.NominationTypeID = Convert.ToInt32(request.Form.Get("NominationTypeID"));
            objCourse.SoftwareDetails = Convert.ToString(request.Form.Get("SoftwareDetails"));
            objCourse.TotalCost = Convert.ToSingle(request.Form.Get("TotalCost"));
            objCourse.RequestedBy = Convert.ToString(request.Form.Get("RequestedBy"));
            objCourse.RequestedFor = Convert.ToString(request.Form.Get("RequestedFor"));
            objCourse.TrainingLocation = Convert.ToString(request.Form.Get("TrainingLocation"));
            if (objCourse.TrainingModeID == Convert.ToInt32(CommonConstants.ExternalTrainer))
            {
                objCourse.PaymentDueDt = (request.Form.Get("PaymentDueDt") == "") ? (DateTime?)null : Convert.ToDateTime(request.Form.Get("PaymentDueDt"));
                objCourse.PaymentMade = (Convert.ToInt32(request.Form.Get("PaymentMade")) == Convert.ToInt32(CommonConstants.PaymentMadeYes)) ? true : false;
                objCourse.PaymentDates = (request.Form.Get("PaymentDates")=="") ? (DateTime?)null : Convert.ToDateTime(request.Form.Get("PaymentDates"));
                objCourse.PaymentComments = Convert.ToString(request.Form.Get("PaymentComments"));
                objCourse.PaymentModeID = Convert.ToInt32(request.Form.Get("PaymentModeID"));
            }
            return objCourse;
         }
    }

    public class CoursePaymentModelBinder : IModelBinder
    {
        private readonly ITrainingService _service;

        public CoursePaymentModelBinder()
        {
            _service = new TrainingService(new TrainingRepository());
        }

        //public EditCourseModelBinder(ITrainingService service)
        //{
        //    _service = service;
        //}

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            int courseId = Convert.ToInt32(request.Form.Get("CourseID"));
            //TrainingCourseModel obj = new TrainingCourseModel();
            //obj = _service.GetTrainingCourses(courseId);

            TrainingCourseModel objCourse = new TrainingCourseModel();
            int trainingMode = Convert.ToInt32(request.Form.Get("hdnTrainingMode"));
            //DateTime trainingStartDate = Convert.ToDateTime(request.Form.Get("hdnTrainingStartDate"));
            //DateTime trainingEndDate = Convert.ToDateTime(request.Form.Get("hdnTrainingEndDate"));

            //By Rakesh to Get Total Cost,Payment Dates
            objCourse.TotalCost = (request.Form.Get("TotalCost") == "") ? (float?)null : Convert.ToSingle(request.Form.Get("TotalCost"));
            objCourse.PaymentDates = (request.Form.Get("PaymentDates") == "") ? (DateTime?)null : Convert.ToDateTime(request.Form.Get("PaymentDates"));
            objCourse.CourseID = courseId;

            if (trainingMode == CommonConstants.ExternalTrainer)
            {
                // objCourse.CourseID = courseId;
                //objCourse.TotalCost = (request.Form.Get("TotalCost") == "") ? (float?)null : Convert.ToSingle(request.Form.Get("TotalCost"));
                objCourse.PaymentDueDt = (request.Form.Get("PaymentDueDt") == "") ? (DateTime?)null : Convert.ToDateTime(request.Form.Get("PaymentDueDt"));
                objCourse.PaymentMade = (Convert.ToInt32(request.Form.Get("PaymentMadeID")) == Convert.ToInt32(CommonConstants.PaymentMadeYes)) ? true : false;
                
                objCourse.PaymentComments = Convert.ToString(request.Form.Get("PaymentComments"));
                objCourse.PaymentModeID = Convert.ToInt32(request.Form.Get("PaymentModeID"));
                objCourse.IndividualPayementTraining = Convert.ToBoolean(request.Form.Get("IndividualPayementTraining"));
            }

            if (request.Form.Get("TrainingStartDate") != null)
            {
                objCourse.TrainingStartDate = Convert.ToDateTime(request.Form.Get("TrainingStartDate"));
                objCourse.TrainingEndDate = Convert.ToDateTime(request.Form.Get("TrainingEndDate"));

            }
            else
            {
                objCourse.TrainingStartDate = Convert.ToDateTime("01/01/1900");
                objCourse.TrainingEndDate = Convert.ToDateTime("01/01/1900");
            }
            
                
            
            return objCourse;
        }
    }

}