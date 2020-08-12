using Domain.Entities;
using Infrastructure.Interfaces;
using RMS.Common.BusinessEntities;
using RMS.Common.BusinessEntities.Training;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages.Html;

namespace RMS.Tests.Repository
{
    public class FakeTrainingRepository:ITrainingRepository
    {
        #region  Method


        /// </summary>
        /// This method is used for saving the Technical Training details
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        public int Save(TrainingModel RaiseTraining)
        {
            throw new NotImplementedException();
        }


        /// </summary>
        /// This method is used for saving the KSS Training details
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        public int SaveKSS(TrainingModel RaiseTraining)
        {
            throw new NotImplementedException();
        }

        /// </summary>
        /// This method is used for saving the Seminars Training details
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        public int SaveSeminars(TrainingModel RaiseTraining)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get Technical Training 
        /// </summary>
        /// <returns>Collection</returns>
        public TrainingModel GetTechnicalSoftSkills(int RaiseID, int TrainingTypeID)
        {
            throw new NotImplementedException();
        }



        /// <summary>
        /// Retrive Technical Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        //List<TrainingModel> ViewTechnicalTraining(TrainingModel RaiseTraining, ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount);         
        public List<TrainingModel> ViewTechnicalTraining(TrainingModel RaiseTraining)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrive SoftSkills Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public List<TrainingModel> ViewSoftSkillsTraining(TrainingModel RaiseTraining)
        {
            throw new NotImplementedException();
        }

        /// </summary>
        /// This method is used for deleteing the Techinical Training details
        /// <param name="RaiseTraining"></param>
        /// <returns>RaiseTechnicalID</returns>
        public string DeleteTechnicalSoftSkillsTraining(TrainingModel RaiseTraining, int TrainingTypeID)
        {
            throw new NotImplementedException();
        }

        /// </summary>
        /// This method is used for deleteing the KSS Training details
        /// <param name="RaiseTraining"></param>
        /// <returns>RaiseKSSID</returns>
        public string DeleteKSSTraining(TrainingModel RaiseTraining)
        {
            throw new NotImplementedException();
        }


        /// </summary>
        /// This method is used for deleteing the Seminars Training details
        /// <param name="RaiseTraining"></param>
        /// <returns>RaiseSeminarsID</returns>
        public string DeleteSeminarsTraining(TrainingModel RaiseTraining)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrive KSS Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public List<TrainingModel> ViewKSSTraining(TrainingModel RaiseTraining)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Get KSS Training 
        /// </summary>
        /// <returns>Collection</returns>
        public TrainingModel GetKSSTraining(int RaiseID)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Retrive Seminars Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public List<TrainingModel> ViewSeminarsTraining(TrainingModel RaiseTraining)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get Seminars Training 
        /// </summary>
        /// <returns>Collection</returns>
        public TrainingModel GetSeminarsTraining(int RaiseID)
        {
            throw new NotImplementedException();
        }

        public int SaveApproveRejectTrainingRequest(TrainingModel RaiseTraining)
        {
            throw new NotImplementedException();
        }


        public string UpdateRaiseTrainingStatus(TrainingModel RaiseTraining)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Edit KSS Training Details using ATG/ATM, TM, STM Group
        /// </summary>
        /// <returns>Collection</returns>
        public DataTable GetEditKSSTrainingGroup(int UserEmpID, string Flag)
        {
            throw new NotImplementedException();
        }

        public int CheckDuplication(TrainingModel RaiseTraining)
        {
            throw new NotImplementedException();
        }

        public DataSet GetEmailIDDetails(string UserEmpID, int Raiseid)
        {
            throw new NotImplementedException();
        }

        public DataTable GetEmailIDDetailsForKSS(string UserEmpID)
        {
            throw new NotImplementedException();
        }

        public DataSet GetEmailIDForAppRejTechSoftSkill(int RaiseID)
        {
            throw new NotImplementedException();
        }

        #endregion  Method

        #region Approve Reject Pages Training
        /// <summary>
        /// Retrive KSS Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public List<TrainingModel> ApproveRejectViewKSSTraining(TrainingModel RaiseTraining)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrive Technical Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public List<TrainingModel> ApproveRejectViewTechnicalTraining(TrainingModel RaiseTraining)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrive SoftSkills Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public List<TrainingModel> ApproveRejectViewSoftSkillsTraining(TrainingModel RaiseTraining)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrive Seminars Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public List<TrainingModel> ApproveRejectViewSeminarsTraining(TrainingModel RaiseTraining)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region View Submit Nomination Method
        //Jagmohan : 14/09/2015 Start
        /// <summary>
        /// Get the list of Training name for all courses
        /// </summary>
        /// <returns>Collection</returns>
        public List<KeyValuePair<int, string>> GetTrainingNameforAllCourses(int empID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the training detail by trainingid
        /// </summary>
        /// <returns>NominationModel</returns>
        public NominationModel GetTrainingDetailByID(int courseid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the Employee detail by empid
        /// </summary>
        /// <returns>empid</returns>
        public Employee GetEmployeeDetailByID(int empid)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Get the list of all employee nominated by manager
        /// </summary>
        /// <returns>mngrid</returns>
        /// <returns>trainingnameid</returns>
        /// <returns>trainingcourseid</returns>
        public List<Employee> GetAllNominatedEmployeeList(int mngrid, int trainingnameid, int trainingcourseid)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Save nominated employee in table T_nomination
        /// </summary>
        /// <returns>Employee</returns>
        public int SaveNominatedEmployee(Employee saveEmployee)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Delete nominated employee in table T_nomination
        /// </summary>
        /// <returns>Employee</returns>
        public List<Employee> DeleteNominatedEmployee(Employee saveEmployee)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Edit nominated employee in table T_nomination
        /// </summary>
        /// <returns>trainingcourseID</returns>
        /// <returns>trainingnameID</returns>
        /// <returns>editemployeeid</returns>
        public Employee GetEmployeeDetailForEdit(int trainingcourseID, int trainingnameID, int editemployeeid)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Update nominated employee in table T_nomination
        /// </summary>
        /// <returns>Employee</returns>
        public int UpdateNominatedEmployee(Employee updatemployee)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Submit nominated employee in table T_nomination
        /// </summary>
        /// <returns>Employee</returns>
        public List<Employee> SubmitNominatedEmployee(int trainingcourseID, int trainingnameID)
        {
            throw new NotImplementedException();
        }
        public List<KeyValuePair<int, string>> GetConfirmTrainingNameforAllCourses(int empID)
        {
            throw new NotImplementedException();
        }
        //Jagmohan : 14/09/2015 Start
        #endregion

        #region Confirm submit nomination method

        /// <summary>
        /// Get the list of all employee nominated by manager for confirmation
        /// </summary>
        /// <value>trainingnameid</value>
        /// <value>trainingcourseid</value>        
        /// <returns>mngrid</returns>
        public List<Employee> GetAllSubmittedEmployeeListByCourseID(int trainingnameid, int trainingcourseid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get datatable of all employee nominated by manager for confirmation
        /// </summary>
        /// <value>trainingnameid</value>
        /// <value>trainingcourseid</value>
        /// <returns>mngrid</returns>
        public DataTable GetDataTableAllSubmittedEmployeeListByCourseID(int trainingnameid, int trainingcourseid)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Save/Update confirmed nomination
        /// </summary>
        /// <returns>status</returns>
        public List<Employee> SaveUpdateNominatedEmployee(int trainingnameid, int trainingcourseid, string selectedemployeeid)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Training Course Method


        public int SaveTrainingCourse(TrainingCourseModel trainingCourse)
        {
            throw new NotImplementedException();
        }

        public int UpdateTrainingCourse(TrainingCourseModel trainingCourse)
        {
            throw new NotImplementedException();
        }

        public List<CourseDetails> GetCoursesByTrainingTypeId(int trainingId, int courseStatus)
        {
            throw new NotImplementedException();
        }

        public TrainingCourseModel GetTrainingCourses(int courseId)
        {
            throw new NotImplementedException();
        }

        public List<System.Web.Mvc.SelectListItem> GetApprovedTrainingNameByTrainingType(int trainingTypeId, bool defaultValue, string selVal)
        {
            throw new NotImplementedException();
        }

        public Vendor GetVendorDetails(int vendorId)
        {
            throw new NotImplementedException();
        }

        public List<TrainingModel> GetApprovedRaiseTrainings(int trainingNameId, int trainingStatusId)
        {
            throw new NotImplementedException();
        }

        public List<TrainingModel> GetRaiseTrainings(string raiseIds, int trainingNameId)
        {
            throw new NotImplementedException();
        }

        public int DeleteTrainingCourse(int courseId)
        {
            throw new NotImplementedException();
        }

        public int CloseTrainingCourse(int courseId)
        {
            throw new NotImplementedException();
        }

        public List<TrainingModel> GetRaiseTrainingsById(string raiseIds)
        {
            throw new NotImplementedException();
        }

        public string GetFileDetailsByCourseId(string module, string courseId)
        {
            throw new NotImplementedException();
        }

        public int UpdateFileDetails(string module, string courseId, string filePath)
        {
            throw new NotImplementedException();
        }

        public InviteNominationModel GetInviteNominationDetails(int courseId)
        {
            throw new NotImplementedException();
        }

        public int UpdateCourseNominationDate(DateTime nominationDate, int courseId)
        {
            throw new NotImplementedException();
        }

        public List<TraingCourse> GetOpenTrainingCourses()
        {
            List<TraingCourse> traingCourses = new List<TraingCourse>();
            TraingCourse traingCourse = new TraingCourse();
            traingCourses.Add(traingCourse);
            return traingCourses;
        }
        public List<TraingCourse> GetTrainingCoursesByTrainingNameId(int courseId,int trainingNameId)
        {
            List<TraingCourse> traingCourses = new List<TraingCourse>();
            TraingCourse traingCourse = new TraingCourse();
            traingCourses.Add(traingCourse);
            return traingCourses;
        }
        public int UpdateCoursePayment(TrainingCourseModel course)
        {
            throw new NotImplementedException();
        }
        #endregion Training Course Method

        #region AccomodationFoodDetails

        public int SaveAccomodationAndFoodDetails(NominationModel objNominationModel)
        {
            throw new NotImplementedException();
        }

        public int SaveTravelDetails(TravelDetailsModel objTravelDetailModel)
        {
            throw new NotImplementedException();
        }

        public DataSet GetAccomodationDetailsByCourseID(int CourseID)
        {
            throw new NotImplementedException();
        }

        public string GetTrainingNameByCourseID(int CourseID)
        {
            throw new NotImplementedException();
        }

        public DataSet GetTravelDetailsByCourseID(int CourseID)
        {
            throw new NotImplementedException();
        }

        #endregion AccomodationFoodDetails

        #region FeedbackDetails
        public DataSet GetQuestionDescription()
        {
            throw new NotImplementedException();
        }
        public List<KeyValuePair<int, string>> GetTrainingNameList(string empId)
        {
            throw new NotImplementedException();
        }
        public List<KeyValuePair<int, string>> GetTrainingNameList()
        {
            throw new NotImplementedException();
        }
        public int SaveFeedbackDetails(FeedbackModel objFeedbackModel)
        {
            throw new NotImplementedException();
        }
        public string GetEmpNameByEmpID(string empID)
        {
            throw new NotImplementedException();
        }
        public DataSet GetFeedbackRatings(string value, string EmpID)
        {
            throw new NotImplementedException();
        }
        public DataSet GetEmployeesFeedbackByCourseId(string CourseID)
        {
            throw new NotImplementedException();
        }
        public int SaveFeedbackDetailsByRMO(NominationModel objNominationModel, string CourseID)
        {
            throw new NotImplementedException();
        }
        public DataSet GetFeedbackDetailsForRMO(string CourseID)
        {
            throw new NotImplementedException();
        }
        public double GetAverageRatingsFromParticipants(string QuestionID, string CourseID)
        {
            throw new NotImplementedException();
        }
        public DataSet GetTrainingDetailsByCourseId(string CourseID)
        {
            throw new NotImplementedException();
        }
        public string GetTrainerNameByCourseID(string CourseID)
        {
            throw new NotImplementedException();
        }
        public int UpdateFlagToFeedbackFilled(string empId, string CourseID)
        {
            throw new NotImplementedException();
        }
        public int SaveAndUpdateStatusOfTraining(NominationModel objNominationModel, string CourseID)
        {
            throw new NotImplementedException();
        }
        public DataSet GetTrainingVendorDetails()
        {
            throw new NotImplementedException();
        }
        public DataSet GetVendorDetailsByVendorId(int VendorId)
        {
            throw new NotImplementedException();
        }
        public int SaveVendorDetails(VendorModel objVendorModel)
        {
            throw new NotImplementedException();
        }
        public int UpdateVendorDetails(VendorModel objVendorModel)
        {
            throw new NotImplementedException();
        }
        public int DeleteVendorDetails(int VendorId)
        {
            throw new NotImplementedException();
        }
        public string GetDuplicateStatusOfEmialId(string VendorEmailId)
        {
            throw new NotImplementedException();
        }
        #endregion


        #region Attendance
        public SelectList GetConfirmedTraining(int TrainingType)
        {
            throw new NotImplementedException();
        }
        public SelectList GetSeminarKSSList(int TrainingTypeId)
        {
            throw new NotImplementedException();
        }
        public DataTable GetEmpPresentee(int CourseId, string AttDate, string Flag)
        {
            throw new NotImplementedException();
        }
        public DataSet GetEmpPresenteeAll(int CourseId)
        {
            throw new NotImplementedException();
        }
        public int SaveAttendance(AttendanceModel obj)
        {
            throw new NotImplementedException();
        }
        public DataSet SaveFeedbackSent(AttendanceModel obj)
        {
            throw new NotImplementedException();
        }
        public int SaveAttendanceSemiKss(AttendanceModel obj)
        {
            throw new NotImplementedException();
        }
        public DataTable GetEmpPresenteeSeminar(int Raiseid, string Flag)
        {
            throw new NotImplementedException();
        }
        public DataTable GetAttendanceReport(int TrainingTypeId, int CourseId)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region TrainingEffectiveness

        public List<TrainingEffectiveness> GetTrainingEffectiveness(int trainingnameid, int LoginEmpId, int LoginEmpIdFlag, string RoleName)
        {
            throw new NotImplementedException();

        }

        public int UpdateTrainingEffectiveness(NominationModel objNominationmodel)
        {
            throw new NotImplementedException();
        }

        public string SendTrainingEffectiveness(NominationModel objNominationmodel)
        {
            throw new NotImplementedException();
        }

        public DataSet CheckInActiveManagerForTrainingEffectivness(int CourseID)
        {
            throw new NotImplementedException();
        }

        public List<KeyValuePair<int, string>> GetTrainingNameforEffectiveness(int LoginEmpID, string RoleName, int TrainingTypeID)
        {

            throw new NotImplementedException();
        }

        public DataSet SendMailToManagerForPostTrainingRatining(int CourseID)
        {
            throw new NotImplementedException();
        }

        public DataSet ManagerFilledPostTrainingRating(int CourseID, int LoginEmpID)
        {
            throw new NotImplementedException();
        }

        #endregion TrainingEffectiveness

        public DataSet GetEmailIDDetailsForKSS(string UserEmpID, int Raiseid)
        {
            throw new NotImplementedException();
        }
    }
}
