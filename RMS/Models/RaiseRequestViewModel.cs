using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain.Entities;
using Infrastructure;
using RMS.Common.Constants;

namespace RMS.Models
{
    public class RaiseRequestViewModel
    {

        public RaiseRequestViewModel() {
            this.RaiseDetails = new List<RaiseRequest>();
            this.RaiseMode = CommonConstants.EditMode;
        }

        public RaiseRequestViewModel(List<TrainingModel> lstTrainingModel,string[] raiseIds, string raiseMode) {
            this.RaiseDetails = from r in lstTrainingModel
                                                  select new RaiseRequest()
                                                  {
                                                      RaiseID = r.RaiseID,
                                                      TrainingTypeID = Convert.ToInt32(r.TrainingType),
                                                      TrainingNameID = r.TrainingNameID,
                                                      TrainingName = r.TrainingName,
                                                      Status = r.Status,
                                                      Quarter = r.QuarterNo,
                                                      QuarterValue = CommonRepository.QuarterValueById(r.QuarterNo),
                                                      RaiseYear = Convert.ToInt32(r.CreatedDate.ToString("yyyy")),
                                                      RaiseByName = r.RequestedBy,
                                                      IsSelected = raiseIds.Contains(Convert.ToString(r.RaiseID)) ? true : false
                                                  };
            this.RaiseMode = raiseMode;
        }

        public IEnumerable<RaiseRequest> RaiseDetails { get; set; }

        public string RaiseMode { get; set; }
    }

    public class RaiseRequest
    {

        [DisplayName("Select")]
        public int RaiseID { get; set; }

        [DisplayName("Training Type ID")]
        public int TrainingTypeID { get; set; }

        [DisplayName("Training Name ID")]
        public int TrainingNameID { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Training Name")]
        public string TrainingName { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Request Status")]
        public string Status { get; set; }

        [DisplayName("Quarter")]
        public int Quarter { get; set; }

        [DisplayName("Quarter")]
        public string QuarterValue { get; set; }

        [DisplayName("Raise Year")]
        public int RaiseYear { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Request Raise Date")]
        public DateTime RaiseDate { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Request Raised By")]
        public string RaiseByName { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Start Date")]
        public DateTime TrainingStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("End Date")]
        public DateTime TrainingEndDate { get; set; }

        public bool IsSelected { get; set; }

    }
}