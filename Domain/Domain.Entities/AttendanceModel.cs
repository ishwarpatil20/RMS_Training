using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;




namespace Domain.Entities
{
    public class AttendanceModel
    {
        

        public int CourseId { get; set; }
        public int TrainingTypeID { get; set; }
        public string TrainingType { get; set; }
        public int TrainingID { get; set; }
        public string TrainingName { get; set; }
        public string TrainingNameSemiKss { get; set; }
        public string TrainerName { get; set; }
        public DateTime TrainingStartDate { get; set; }
        public DateTime TrainingEndDate { get; set; }
        public float? NoOfdays { get; set; }
        public float? NoOfDaysFilled { get; set; }
        public int Mode { get; set; }
        public DateTime? FeedbackLastDate { get; set; }
        public string FbkSaveUpdateMode { get; set; }
        public bool FeedbackMailNotToSend { get; set; }
        
        public IEnumerable<SelectListItem> ListTrainingName { get; set; }
        public IEnumerable<SelectListItem> ListAttendanceDate { get; set; }
        public IEnumerable<SelectListItem> SelectedAttendanceDate { get; set; }
        public string AttendanceDate { get; set; }
        public DataTable dtAttendance { get; set; }
        public List<DynamicGrid> objDynamicGrid { get; set; }
        public TrainingCourseModel objTrainingCourseModel { get; set; }
        //public TrainingModel objTrainingModel { get; set; }
        public string EmpIdAll { get; set; }
        public string dropEmpIdAll { get; set; }
        public string AttendanceType { get; set; }
        public int CreatedBy { get; set; }
        public IEnumerable<SelectListItem> TrainingTypeDetails { get; set; }

        public string SetAttendanceDates { get; set; }

        //public DateTime AttendanceDate { get; set; }

        public List<DateTime> AttendanceDates { get; set; }

        public int RaiseId { get; set; }

        public string Presenter { get; set; }

        /// <summary>
        /// Gets or sets PresenterID
        /// </summary>        
        public string PresenterID { get; set; }
        public string Operation { get; set; }
        public string Attender { get; set; }
        public string AttenderId { get; set; }
        //public List<DateTime> AttendanceDates { get; set; }
        public List<CommonModel> objCommonModel { get; set; }
    }

    public class DynamicGrid
    {
        public int CourseId { get; set; }
        public int Empid { get; set; }
        public string EmpName { get; set; }
        public string AttendanceDate { get; set; }
        public string Presentee { get; set; }
    }




}
