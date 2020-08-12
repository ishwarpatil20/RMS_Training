using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Domain.Entities;
using RMS.Common.BusinessEntities;
using System.Web.Mvc;
using RMS.Common.BusinessEntities.Training;

namespace Infrastructure.Interfaces
{
    public interface ITrainingRepository
    {
       
        #region  Method


        /// </summary>
        /// This method is used for saving the Technical Training details
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        int Save(TrainingModel RaiseTraining);


        /// </summary>
        /// This method is used for saving the KSS Training details
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
         int SaveKSS(TrainingModel RaiseTraining);

         /// </summary>
        /// This method is used for saving the Seminars Training details
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
         int SaveSeminars(TrainingModel RaiseTraining);

        /// <summary>
        /// Get Technical Training 
        /// </summary>
        /// <returns>Collection</returns>
         TrainingModel GetTechnicalSoftSkills(int RaiseID, int TrainingTypeID);


        /// <summary>
        /// Retrive Technical Training summary.
        /// </summary>
        /// <returns>Collection</returns>
         //List<TrainingModel> ViewTechnicalTraining(TrainingModel RaiseTraining, ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount);         
         List<TrainingModel> ViewTechnicalTraining(TrainingModel RaiseTraining);

                /// <summary>
        /// Retrive SoftSkills Training summary.
        /// </summary>
        /// <returns>Collection</returns>
         List<TrainingModel> ViewSoftSkillsTraining(TrainingModel RaiseTraining);

        /// </summary>
        /// This method is used for deleteing the Techinical Training details
        /// <param name="RaiseTraining"></param>
        /// <returns>RaiseTechnicalID</returns>
         string DeleteTechnicalSoftSkillsTraining(TrainingModel RaiseTraining, int TrainingTypeID);

        /// </summary>
        /// This method is used for deleteing the KSS Training details
        /// <param name="RaiseTraining"></param>
        /// <returns>RaiseKSSID</returns>
         string DeleteKSSTraining(TrainingModel RaiseTraining);


        /// </summary>
        /// This method is used for deleteing the Seminars Training details
        /// <param name="RaiseTraining"></param>
        /// <returns>RaiseSeminarsID</returns>
         string DeleteSeminarsTraining(TrainingModel RaiseTraining);

        /// <summary>
        /// Retrive KSS Training summary.
        /// </summary>
        /// <returns>Collection</returns>
         List<TrainingModel> ViewKSSTraining(TrainingModel RaiseTraining);


                /// <summary>
        /// Get KSS Training 
        /// </summary>
        /// <returns>Collection</returns>
         TrainingModel GetKSSTraining(int RaiseID);


        /// <summary>
        /// Retrive Seminars Training summary.
        /// </summary>
        /// <returns>Collection</returns>
         List<TrainingModel> ViewSeminarsTraining(TrainingModel RaiseTraining);

                /// <summary>
        /// Get Seminars Training 
        /// </summary>
        /// <returns>Collection</returns>
         TrainingModel GetSeminarsTraining(int RaiseID);

         int SaveApproveRejectTrainingRequest(TrainingModel RaiseTraining);


         string UpdateRaiseTrainingStatus(TrainingModel RaiseTraining);

        /// <summary>
        /// Edit KSS Training Details using ATG/ATM, TM, STM Group
        /// </summary>
        /// <returns>Collection</returns>
         DataTable GetEditKSSTrainingGroup(int UserEmpID, string Flag);         

         int CheckDuplication(TrainingModel RaiseTraining);

         DataSet GetEmailIDDetails(string UserEmpID, int Raiseid);

         DataSet GetEmailIDDetailsForKSS(string UserEmpID, int Raiseid);

         DataSet GetEmailIDForAppRejTechSoftSkill(int RaiseID);
         
        #endregion  Method

        #region Approve Reject Pages Training
         /// <summary>
         /// Retrive KSS Training summary.
         /// </summary>
         /// <returns>Collection</returns>
         List<TrainingModel> ApproveRejectViewKSSTraining(TrainingModel RaiseTraining);

         /// <summary>
         /// Retrive Technical Training summary.
         /// </summary>
         /// <returns>Collection</returns>
         List<TrainingModel> ApproveRejectViewTechnicalTraining(TrainingModel RaiseTraining);

         /// <summary>
         /// Retrive SoftSkills Training summary.
         /// </summary>
         /// <returns>Collection</returns>
         List<TrainingModel> ApproveRejectViewSoftSkillsTraining(TrainingModel RaiseTraining);

         /// <summary>
         /// Retrive Seminars Training summary.
         /// </summary>
         /// <returns>Collection</returns>
         List<TrainingModel> ApproveRejectViewSeminarsTraining(TrainingModel RaiseTraining);
         #endregion

        #region View Submit Nomination Method
        //Jagmohan : 14/09/2015 Start
        /// <summary>
        /// Get the list of Training name for all courses
        /// </summary>
        /// <returns>Collection</returns>
         List<KeyValuePair<int, string>> GetTrainingNameforAllCourses(int empID);

        /// <summary>
        /// Get the list of confirm/submitted Training name for all courses
        /// </summary>
        /// <returns>Collection</returns>
         List<KeyValuePair<int, string>> GetConfirmTrainingNameforAllCourses(int empID);

        /// <summary>
        /// Get the training detail by trainingid
        /// </summary>
         /// <returns>NominationModel</returns>
         NominationModel GetTrainingDetailByID(int courseid);

        /// <summary>
        /// Get the Employee detail by empid
        /// </summary>
         /// <returns>empid</returns>
         Employee GetEmployeeDetailByID(int empid);

         
         /// <summary>
         /// Get the list of all employee nominated by manager
         /// </summary>
         /// <returns>mngrid</returns>
         /// <returns>trainingnameid</returns>
         /// <returns>trainingcourseid</returns>
         List<Employee> GetAllNominatedEmployeeList(int mngrid, int trainingnameid, int trainingcourseid);


         /// <summary>
         /// Save nominated employee in table T_nomination
         /// </summary>
         /// <returns>Employee</returns>
         int SaveNominatedEmployee(Employee saveEmployee);


         /// <summary>
         /// Delete nominated employee in table T_nomination
         /// </summary>
         /// <returns>Employee</returns>
         Employee DeleteNominatedEmployee(Employee saveEmployee);

         /// <summary>
         /// Edit nominated employee in table T_nomination
         /// </summary>
         /// <returns>trainingcourseID</returns>
         /// <returns>trainingnameID</returns>
         /// <returns>editemployeeid</returns>
         Employee GetEmployeeDetailForEdit(int trainingcourseID, int trainingnameID, int editemployeeid);


        /// <summary>
         /// Update nominated employee in table T_nomination
         /// </summary>
         /// <returns>Employee</returns>
         int UpdateNominatedEmployee(Employee updatemployee);


         /// <summary>
         /// Submit nominated employee in table T_nomination
         /// </summary>
         /// <returns>Employee</returns>
         List<Employee> SubmitNominatedEmployee(int trainingcourseID, int trainingnameID, string selectedemployeeid);

        //Jagmohan : 14/09/2015 Start
        #endregion

        #region Confirm submit nomination method
        
        /// <summary>
        /// Get the list of all employee nominated by manager for confirmation
        /// </summary>
         /// <value>trainingnameid</value>
         /// <value>trainingcourseid</value>        
        /// <returns>mngrid</returns>
        List<Employee> GetAllSubmittedEmployeeListByCourseID(int trainingnameid, int trainingcourseid);

        /// <summary>
        /// Get datatable of all employee nominated by manager for confirmation
        /// </summary>
        /// <value>trainingnameid</value>
        /// <value>trainingcourseid</value>
        /// <returns>mngrid</returns>
        DataTable GetDataTableAllSubmittedEmployeeListByCourseID(int trainingnameid, int trainingcourseid);


        /// <summary>
        /// Save/Update confirmed nomination
        /// </summary>
        /// <returns>status</returns>
        List<Employee> SaveUpdateNominatedEmployee(int trainingnameid, int trainingcourseid, string selectedemployeeid);

        #endregion

        #region Training Course Method


         int SaveTrainingCourse(TrainingCourseModel trainingCourse);

         int SaveFileDetails(TrainingCourseModel course);

         int UpdateTrainingCourse(TrainingCourseModel trainingCourse, string unmappedRaiseIDs, string mappedRaiseIDs);

         //Harsha- Issue Id- 58975 & 58958 - Start
         //Description- Making Training Cost Editable for Internal Training after Closed status of the training
         //Disabling Number of Hours and Days field once the after training's status is feedback sent
         bool UpdateTrainingCourseTotalCost(int? courseId, float? totalCost);
         //Harsha- Issue Id- 58975 & 58958 - End

        int UpdateFileDetails(TrainingCourseModel course);

        List<CourseDetails> GetCoursesByTrainingTypeId(int trainingId, int courseStatus);

        TrainingCourseModel GetTrainingCourses(int courseId);

        List<SelectListItem> GetApprovedTrainingNameByTrainingType(int trainingTypeId, bool defaultValue, string selVal);

        Vendor GetVendorDetails(int vendorId);

        List<TrainingModel> GetApprovedRaiseTrainings(int trainingNameId, int trainingStatusId);

        List<TrainingModel> GetRaiseTrainings(string raiseIds, int trainingNameId);

        int DeleteTrainingCourse(int courseId);

        int CloseTrainingCourse(int courseId, string Prompt);

        List<TrainingModel> GetRaiseTrainingsById(string raiseIds);

        string GetFileDetailsByCourseId(string module, string courseId);

        TrainingCourseModel GetInvoiceDetailsByCourseId(string module, string courseId);

        int UpdateFileDetails(string module, string courseId, string filePath);

        int updateInvoiceDetails(string fileId,string module, string courseId, string filePath);

        InviteNominationModel GetInviteNominationDetails(int courseId);

        int UpdateCourseNominationDate(DateTime nominationDate, int courseId);

        int UpdateCoursePayment(TrainingCourseModel course);

        List<TraingCourse> GetOpenTrainingCourses();

        List<TraingCourse> GetTrainingCoursesByTrainingNameId(int courseId, int trainingNameId);

        string GetDuplicateStatusOfCoursName(string CourseName);

        List<TraingCourse> GetTrainingCoursesPageWise(string strCoursePageFor, int Empid);

        #endregion Training Course Method

        #region AccomodationFoodDetails

        int SaveAccomodationAndFoodDetails(NominationModel objNominationModel);

        int SaveTravelDetails(TravelDetailsModel objTravelDetailModel);
        
        DataSet GetAccomodationDetailsByCourseID(int CourseID);

        string GetTrainingNameByCourseID(int CourseID);

        DataSet GetTravelDetailsByCourseID(int CourseID);

        #endregion AccomodationFoodDetails

        #region FeedbackDetails
        DataSet GetQuestionDescription();
        List<KeyValuePair<int, string>> GetTrainingNameList(string empId);
        List<KeyValuePair<int, string>> GetTrainingNameList();
        int SaveFeedbackDetails(FeedbackModel objFeedbackModel);
        string GetEmpNameByEmpID(string empID);
        DataSet GetFeedbackRatings(string value, string EmpID);
        DataSet GetEmployeesFeedbackByCourseId(string CourseID);
        int SaveFeedbackDetailsByRMO(NominationModel objNominationModel,string CourseID);
        DataSet GetFeedbackDetailsForRMO(string CourseID);
        double GetAverageRatingsFromParticipants(string QuestionID, string CourseID);
        DataSet GetTrainingDetailsByCourseId(string CourseID);
        string GetTrainerNameByCourseID(string CourseID);
        int UpdateFlagToFeedbackFilled(string empId, string CourseID);
        int SaveAndUpdateStatusOfTraining(NominationModel objNominationModel, string CourseID);
        DataSet GetTrainingVendorDetails();
        DataSet GetVendorDetailsByVendorId(int VendorId);
        int SaveVendorDetails(VendorModel objVendorModel);
        int UpdateVendorDetails(VendorModel objVendorModel);
        int DeleteVendorDetails(int VendorId);
        string GetDuplicateStatusOfEmialId(string VendorEmailId);
        #endregion


        #region Attendance
        SelectList GetConfirmedTraining(int TrainingType);
        SelectList GetSeminarKSSList(int TrainingType);
        
        DataTable GetEmpPresentee(int CourseId, string AttDate, string Flag);
        DataSet GetEmpPresenteeAll(int CourseId, int TrainingTypeID);
        int SaveAttendance(AttendanceModel obj);
        int SaveAttendanceSemiKss(AttendanceModel obj);
        DataSet SaveFeedbackSent(AttendanceModel obj);
        DataTable GetEmpPresenteeSeminar(int CourseId, string AttendanceDate, string Flag);
        DataTable GetAttendanceReport(int TrainingTypeId, int CourseId);
        int SetAttendance(AttendanceModel obj);
        List<SelectListItem> GetAttendanceDates(int CourseID, int TrainingTypeId);
        List<Employee> GetNominationDetailsByCourseId(int CourseID, int TrainingTypeID);
        #endregion

        #region TrainingEffectiveness

        List<TrainingEffectiveness> GetTrainingEffectiveness(int trainingnameid, int LoginEmpId, int LoginEmpIdFlag, string RoleName);

        int UpdateTrainingEffectiveness(NominationModel objNominationmodel);

        string SendTrainingEffectiveness(NominationModel objNominationmodel);

        //int UpdatePostTrainingEffectiveness(NominationModel objNominationmodel);

        List<KeyValuePair<int, string>> GetTrainingNameforEffectiveness(int LoginEmpID, string RoleName, int TrainingTypeID);

        DataSet SendMailToManagerForPostTrainingRatining(int CourseID);

        DataSet ManagerFilledPostTrainingRating(int CourseID, int LoginEmpId, string EmpID, string RoleName);

        DataSet CheckInActiveManagerForTrainingEffectivness(int CourseID);

        #endregion TrainingEffectiveness

        #region TrainingPlan
        int InsertUpdateTrainingPlan(TrainingPlanModel Obj);
        List<TrainingPlanModel> GetTrainingPlan(TrainingPlanModel Obj);
        DataTable GetTrainingPlanDataSet(TrainingPlanModel Obj);
        List<TrainingPlanModel> DeleteTrainingPlan(int TrainingPlanid, int Createdby);

        List<TrainingPlanModel> GetTrainingPlanEmployees(TrainingPlanModel Obj, int NextQuarter, int Nextyear);
        #endregion


        AttendanceModel GetSeminarKSSCourse(int CourseId);

        int getParticipantsCount(string CourseID);

        int getFeedbackFilledCount(string CourseID);

        //Neelam Issue Id:60562 start
        int UpdateTrainingNominatorEmpID(int CourseID, int EmpID, int PostNominatorId);
        //Neelam Issue Id:60562 end

        //Harsha Issue Id-59073 - Start
        //Description- Training : Create emp Nominated page so that emp can view they nomination. Change confirmation email body
        EmployeeTrainingNominationEntity GetEmployeeTrainingDetails(int courseId);
        List<KeyValuePair<int, string>> GetTrainingNamesByEmployeeId(int empID);
        //Harsha Issue Id-59073 - End

        #region Self Learning

        int SaveMyTraining(MyTrainingModel myTraining, int empId);

        IEnumerable<MyTrainingModel> GetMyTrainings(int empId);

        int DeleteMyTraining(int trainingId);

        MyTrainingModel GetMyTrainingDetails(int trainingId);

        #endregion
    }
}
