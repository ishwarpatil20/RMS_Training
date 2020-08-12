using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure;
using RMS.Common.BusinessEntities;
using RMS.Common.Constants;
using Services.Interfaces;
using System.Web.Mvc;
using RMS.Common;
using System.Web;
using RMS.Common.BusinessEntities.Training;

namespace Services
{
    public class TrainingService : ITrainingService
    {

        private readonly ITrainingRepository _repository;
        

        public TrainingService(ITrainingRepository repository)
        {
            _repository = repository;
        }

       #region  Method

        /// </summary>
        /// This method is used for saving the Technical Training details
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        public int Save(TrainingModel RaiseTraining)
        {
            return _repository.Save(RaiseTraining);
        }


        /// </summary>
        /// This method is used for saving the KSS Training details
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        public int SaveKSS(TrainingModel RaiseTraining)
        {
            return _repository.SaveKSS(RaiseTraining);

        }
    


        /// </summary>
        /// This method is used for saving the Seminars Training details
        /// <param name="RaiseTraining"></param>
        /// <returns></returns>
        public int SaveSeminars(TrainingModel RaiseTraining)
        {
            return _repository.SaveSeminars(RaiseTraining);
        }

        /// <summary>
        /// Get Technical Training 
        /// </summary>
        /// <returns>Collection</returns>
        public TrainingModel GetTechnicalSoftSkills(int RaiseID, int TrainingTypeID)
        {
            return _repository.GetTechnicalSoftSkills(RaiseID, TrainingTypeID);
        }


        /// <summary>
        /// Retrive Technical Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        //public List<TrainingModel> ViewTechnicalTraining(TrainingModel RaiseTraining, ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount)
        //{
        //    return _repository.ViewTechnicalTraining(RaiseTraining, objParameter, ref pageCount, ref CurrentIndexCount);
        //}
        public List<TrainingModel> ViewTechnicalTraining(TrainingModel RaiseTraining)
        {
            return _repository.ViewTechnicalTraining(RaiseTraining);
        }

    
        //public List<TrainingModel> ViewTechnicalTraining(TrainingModel RaiseTraining, ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount);

        /// <summary>
        /// Retrive SoftSkills Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public List<TrainingModel> ViewSoftSkillsTraining(TrainingModel RaiseTraining)
        {
            return _repository.ViewSoftSkillsTraining(RaiseTraining);
        }

        /// </summary>
        /// This method is used for deleteing the Techinical Training details
        /// <param name="RaiseTraining"></param>
        /// <returns>RaiseTechnicalID</returns>
        public string DeleteTechnicalSoftSkillsTraining(TrainingModel RaiseTraining, int TrainingTypeID)
        {
            return _repository.DeleteTechnicalSoftSkillsTraining(RaiseTraining, TrainingTypeID);
        }

        /// </summary>
        /// This method is used for deleteing the KSS Training details
        /// <param name="RaiseTraining"></param>
        /// <returns>RaiseKSSID</returns>
        public string DeleteKSSTraining(TrainingModel RaiseTraining)
        {
            return _repository.DeleteKSSTraining(RaiseTraining);
        }


        /// </summary>
        /// This method is used for deleteing the Seminars Training details
        /// <param name="RaiseTraining"></param>
        /// <returns>RaiseSeminarsID</returns>
        public string DeleteSeminarsTraining(TrainingModel RaiseTraining)
        {
            return _repository.DeleteSeminarsTraining(RaiseTraining);
        }

        /// <summary>
        /// Retrive KSS Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public List<TrainingModel> ViewKSSTraining(TrainingModel RaiseTraining)
        {
            return _repository.ViewKSSTraining(RaiseTraining);
        }



        /// <summary>
        /// Get KSS Training 
        /// </summary>
        /// <returns>Collection</returns>
        public TrainingModel GetKSSTraining(int RaiseID)
        {
            return _repository.GetKSSTraining(RaiseID);
        }


        /// <summary>
        /// Retrive Seminars Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public List<TrainingModel> ViewSeminarsTraining(TrainingModel RaiseTraining)
        {
            return _repository.ViewSeminarsTraining(RaiseTraining);
        }

        /// <summary>
        /// Get Seminars Training 
        /// </summary>
        /// <returns>Collection</returns>
        public TrainingModel GetSeminarsTraining(int RaiseID)
        {
            return _repository.GetSeminarsTraining(RaiseID);
        }

        public int SaveApproveRejectTrainingRequest(TrainingModel RaiseTraining)
        {
            return _repository.SaveApproveRejectTrainingRequest(RaiseTraining);
        }



        public string UpdateRaiseTrainingStatus(TrainingModel RaiseTraining)
        {
            return _repository.UpdateRaiseTrainingStatus(RaiseTraining);
        }

        /// <summary>
        /// Edit KSS Training Details using ATG/ATM, TM, STM Group
        /// </summary>
        /// <returns>Collection</returns>
        public DataTable GetEditKSSTrainingGroup(int UserEmpID, string Flag)
        {
            return _repository.GetEditKSSTrainingGroup(UserEmpID, Flag);
        }        

        public int CheckDuplication(TrainingModel RaiseTraining)
        {
            return _repository.CheckDuplication(RaiseTraining);
        }

        public DataSet GetEmailIDDetails(string UserEmpID, int Raiseid)
        {
            return _repository.GetEmailIDDetails(UserEmpID, Raiseid);
        }

        public DataSet GetEmailIDDetailsForKSS(string UserEmpID, int Raiseid)
        {
            return _repository.GetEmailIDDetailsForKSS(UserEmpID, Raiseid);
        }

        public DataSet GetEmailIDForAppRejTechSoftSkill(int RaiseID)
        {
            return _repository.GetEmailIDForAppRejTechSoftSkill(RaiseID);
        }

        //Harsha Issue Id-59073 - Start
        //Description- Training : Create emp Nominated page so that emp can view they nomination. Change confirmation email body
        public List<KeyValuePair<int, string>> GetTrainingNamesByEmployeeId(int empID)
        {
            return _repository.GetTrainingNamesByEmployeeId(empID);
        }
        public EmployeeTrainingNominationEntity GetEmployeeTrainingDetails(int courseId)
        {
            return _repository.GetEmployeeTrainingDetails(courseId);
        }
        //Harsha Issue Id-59073 - End

        #region Approve Reject Training

        /// <summary>
        /// Retrive KSS Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public List<TrainingModel> ApproveRejectViewKSSTraining(TrainingModel RaiseTraining)
        {
            return _repository.ApproveRejectViewKSSTraining(RaiseTraining);
        }


        /// <summary>
        /// Retrive Technical Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public List<TrainingModel> ApproveRejectViewTechnicalTraining(TrainingModel RaiseTraining)
        {
            return _repository.ApproveRejectViewTechnicalTraining(RaiseTraining);
        }
        //public List<TrainingModel> ApproveRejectViewTechnicalTraining(TrainingModel RaiseTraining, ParameterCriteria objParameter, ref int pageCount, ref int CurrentIndexCount)
        //{
        //    return _repository.ApproveRejectViewTechnicalTraining(RaiseTraining, objParameter, ref pageCount, ref CurrentIndexCount);
        //}


        /// <summary>
        /// Retrive SoftSkills Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public List<TrainingModel> ApproveRejectViewSoftSkillsTraining(TrainingModel RaiseTraining)
        {
            return _repository.ApproveRejectViewSoftSkillsTraining(RaiseTraining);
        }


        /// <summary>
        /// Retrive Seminars Training summary.
        /// </summary>
        /// <returns>Collection</returns>
        public List<TrainingModel> ApproveRejectViewSeminarsTraining(TrainingModel RaiseTraining)
        {
            return _repository.ApproveRejectViewSeminarsTraining(RaiseTraining);
        }

        #endregion

        #region Nomination Method
        //Jagmohan : 14/09/2015 Start
        /// <summary>
        /// Get the list of Training name for all courses
        /// </summary>
        /// <returns>Collection</returns>
        public List<KeyValuePair<int, string>> GetTrainingNameforAllCourses(int empID)
        {
            var trainingList = _repository.GetTrainingNameforAllCourses(empID);
            trainingList.Insert(0, CommonConstants.selectDefault);
            return trainingList;           
        }        


        /// <summary>
        /// Get the list of confirm/submitted Training name for all courses
        /// </summary>
        /// <returns>Collection</returns>
        public List<KeyValuePair<int, string>> GetConfirmTrainingNameforAllCourses(int empID)
        {
            var trainingList = _repository.GetConfirmTrainingNameforAllCourses(empID);
            trainingList.Insert(0, CommonConstants.selectDefault);
            return trainingList;
        }     


        /// <summary>
        /// Get training detail by id
        /// </summary>
        /// <returns>NominationModel</returns>
        public NominationModel GetTrainingDetailByID(int courseid)
        {
            return _repository.GetTrainingDetailByID(courseid);           
        }


        /// <summary>
        /// Get all active employee list
        /// </summary>        
        public List<KeyValuePair<int, string>> GetAllEmployeeList()
        {
            var employeeList = CommonRepository.GetAllEmployeeList();
            employeeList.Insert(0, CommonConstants.selectDefault);
            return employeeList;
        }


        /// <summary>
        /// Get self employeeid in list
        /// </summary>     
        public List<KeyValuePair<int, string>> GetSelfEmployeeList()
        {
            int managerID = Convert.ToInt16(HttpContext.Current.Session["EmpID"]);
            List<KeyValuePair<int, string>> selfemplist = new List<KeyValuePair<int, string>>();
            var mod = CommonRepository.GetAllEmployeeList().Single(x => x.Key ==  managerID);
            selfemplist.Add(mod);
            return selfemplist;
        }


        /// <summary>
        /// Get all active employee list
        /// </summary>        
        public SelectList FillMasterDropDownList(string categoryname)
        {
            return CommonRepository.FillMasterDropDownList(categoryname);
        }

        public SelectList FillObjectiveMetDropDownList()
        {
            return CommonRepository.FillObjectiveMetDropDownList();
        }

       
        /// <summary>
        /// Get the Employee detail by empid
        /// </summary>
        /// <returns>empid</returns>
        public Employee GetEmployeeDetailByID(int empid)
        {
            return _repository.GetEmployeeDetailByID(empid);
        }


        /// <summary>
        /// Get the list of all employee nominated by manager
        /// </summary>
        /// <returns>mngrid</returns>
        public List<Employee> GetAllNominatedEmployeeList(int trainingnameid, int trainingcourseid)
        {
            int managerID = Convert.ToInt16(HttpContext.Current.Session["EmpID"]);
            return _repository.GetAllNominatedEmployeeList(managerID, trainingnameid, trainingcourseid);

        }


        /// <summary>
        /// save nominated employee in table
        /// </summary>
        /// <returns>saveEmployee</returns>
        public int SaveNominatedEmployee(Employee saveEmployee)
        {
            return _repository.SaveNominatedEmployee(saveEmployee);
        }

        /// <summary>
        /// delete nominated employee in table
        /// </summary>
        /// <returns>saveEmployee</returns>
        public Employee DeleteNominatedEmployee(int trainingcourseID, int trainingnameID, int deleteemployeeid, int NominationID, bool IsConfirmNominationID)
        {
            Employee deleteemployee = new Employee();
            deleteemployee.courseID = trainingcourseID;
            deleteemployee.EmployeeID = deleteemployeeid;
            deleteemployee.TrainingNameID = trainingnameID;
            deleteemployee.NominationID = NominationID;
            // Harsha Issue Id-59072 - Start
            // Description: Training : provision to Re-approve the  Rejected Nomination for the courses
            deleteemployee.IsConfirmNomination = IsConfirmNominationID;
            // Harsha Issue Id-59072 - End
            return _repository.DeleteNominatedEmployee(deleteemployee);
        }


        /// <summary>
        /// Get edit employee detail by id
        /// </summary>
        /// <returns>empid</returns>
        public Employee GetEmployeeDetailForEdit(int trainingcourseID, int trainingnameID, int editemployeeid)
        {
            return _repository.GetEmployeeDetailForEdit(trainingcourseID, trainingnameID, editemployeeid);
        }


        /// <summary>
        /// Update nominated employee in table
        /// </summary>
        /// <returns>updatemployee</returns>
        public int UpdateNominatedEmployee(Employee updatemployee)
        {
            return _repository.UpdateNominatedEmployee(updatemployee);
        }


        /// <summary>
        /// Submit nominated employee in table
        /// </summary>
        /// <returns>submitemployee</returns>
        public List<Employee> SubmitNominatedEmployee(int trainingcourseID, int trainingnameID, string selectedemployeeid)
        {
            return _repository.SubmitNominatedEmployee(trainingcourseID, trainingnameID, selectedemployeeid);
        }

        #endregion


        #region Confirm submit nomination method

        /// <summary>
        /// Get the list of all employee nominated by manager for confirmation
        /// </summary>
        /// <value>trainingnameid</value>
        /// <value>trainingcourseid</value>
        /// <returns>EmployeeList</returns>
        public List<Employee> GetAllSubmittedEmployeeListByCourseID(int trainingnameid, int trainingcourseid)
        {
            return _repository.GetAllSubmittedEmployeeListByCourseID(trainingnameid, trainingcourseid);
        }


        /// <summary>
        /// Get the list of all employee nominated by manager for confirmation
        /// </summary>
        /// <value>trainingnameid</value>
        /// <value>trainingcourseid</value>
        /// <returns>datatable</returns>
        public DataTable GetDataTableAllSubmittedEmployeeListByCourseID(int trainingnameid, int trainingcourseid)
        {
            return _repository.GetDataTableAllSubmittedEmployeeListByCourseID(trainingnameid, trainingcourseid);
        }


        public List<Employee> SaveUpdateNominatedEmployee(int trainingnameid, int trainingcourseid, string selectedemployeeid)
        {
            return _repository.SaveUpdateNominatedEmployee(trainingnameid, trainingcourseid, selectedemployeeid);
        }

        #endregion


        #region Training Course Method

        public int SaveTrainingCourse(TrainingCourseModel trainingCourse)
        {
            return _repository.SaveTrainingCourse(trainingCourse);
        }

        public int SaveFileDetails(TrainingCourseModel course)
        {
            return _repository.SaveFileDetails(course);
        }

        public int UpdateTrainingCourse(TrainingCourseModel trainingCourse, string unmappedRaiseIDs, string mappedRaiseIDs)
        {
            return _repository.UpdateTrainingCourse(trainingCourse, unmappedRaiseIDs, mappedRaiseIDs);
        }

        public int UpdateFileDetails(TrainingCourseModel course)
        {
            return _repository.UpdateFileDetails(course);
        }

        public List<CourseDetails> GetCoursesByTrainingTypeId(int trainingTypeId, int courseStatus)
        {
            return _repository.GetCoursesByTrainingTypeId(trainingTypeId,courseStatus);
        }

        public TrainingCourseModel GetTrainingCourses(int courseId)
        {
            return _repository.GetTrainingCourses(courseId);
        }

        public AttendanceModel GetSeminarKSSCourse(int CourseId)
        {
            return _repository.GetSeminarKSSCourse(CourseId);
        }

        public List<SelectListItem> GetApprovedTrainingNameByTrainingType(int trainingTypeId, bool defaultValue, string selVal)
        {
            return _repository.GetApprovedTrainingNameByTrainingType(trainingTypeId, defaultValue, selVal);
        }
        
        public Vendor GetVendorDetails(int vendorId)
        {
            return _repository.GetVendorDetails(vendorId);
        }

        public List<TrainingModel> GetApprovedRaiseTrainings(int trainingNameId, int trainingStatusId)
        {
            return _repository.GetApprovedRaiseTrainings(trainingNameId,trainingStatusId);
        }

        public List<TrainingModel> GetRaiseTrainings(string raiseIds, int trainingNameId)
        {
            return _repository.GetRaiseTrainings(raiseIds, trainingNameId);
        }

        public int DeleteTrainingCourse(int courseId)
        {
            return _repository.DeleteTrainingCourse(courseId);
        }

        public int CloseTrainingCourse(int courseId, string Prompt)
        {
            return _repository.CloseTrainingCourse(courseId, Prompt);
        }

        public List<TrainingModel> GetRaiseTrainingsById(string raiseIds)
        {
            return _repository.GetRaiseTrainingsById(raiseIds);
        }

        public string GetFileDetailsByCourseId(string module, string courseId)
        {
            return _repository.GetFileDetailsByCourseId(module, courseId);
        }

        public TrainingCourseModel GetInvoiceDetailsByCourseId(string module, string courseId)
        {
            return _repository.GetInvoiceDetailsByCourseId(module, courseId);
        }
        public int UpdateFileDetails(string module, string courseId, string filePath)
        {
            return _repository.UpdateFileDetails(module, courseId, filePath);
        }

        public int updateInvoiceDetails(string fileId,string module, string courseId, string fileName)
        {
            return _repository.updateInvoiceDetails(fileId,module, courseId, fileName);
        }

        public List<TraingCourse> GetOpenTrainingCourses()
        {
            return _repository.GetOpenTrainingCourses();
        }
        public List<TraingCourse> GetTrainingCoursesByTrainingNameId(int courseId, int trainingNameId)
        {
            return _repository.GetTrainingCoursesByTrainingNameId(courseId,trainingNameId);
        }
        #endregion Training Course Method

        public InviteNominationModel GetInviteNominationDetails(int courseId)
        {
            return _repository.GetInviteNominationDetails(courseId);
        }

        public int UpdateCourseNominationDate(DateTime nominationDate, int courseId)
        {
            return _repository.UpdateCourseNominationDate(nominationDate, courseId);
        }

        public int UpdateCoursePayment(TrainingCourseModel course)
        {
            return _repository.UpdateCoursePayment(course);
        }

        public string GetDuplicateStatusOfCoursName(string CourseName)
        {
            return _repository.GetDuplicateStatusOfCoursName(CourseName);
        }

        public List<TraingCourse> GetTrainingCoursesPageWise(string strCoursePageFor, int Empid)
        {
            return _repository.GetTrainingCoursesPageWise(strCoursePageFor, Empid);
        }
        #endregion Training Course Method

        #region AccomodationFoodDetails

        public int SaveAccomodationAndFoodDetails(NominationModel objNominationModel)
        {
            return _repository.SaveAccomodationAndFoodDetails(objNominationModel);
        }

        public int SaveTravelDetails(TravelDetailsModel objTravelDetailModel)
        {
            return _repository.SaveTravelDetails(objTravelDetailModel);
        }

        public DataSet GetAccomodationDetailsByCourseID(int CourseID)
        {
            return _repository.GetAccomodationDetailsByCourseID(CourseID);
        }

        public string GetTrainingNameByCourseID(int CourseID)
        {
            return _repository.GetTrainingNameByCourseID(CourseID);
        }

        public DataSet GetTravelDetailsByCourseID(int CourseID)
        {
            return _repository.GetTravelDetailsByCourseID(CourseID);
        }


        #endregion AccomodationFoodDetails

        #region FeedbackDetails

        public int getParticipantsCount(string CourseID)
        {
            return _repository.getParticipantsCount(CourseID);
        }

        public int getFeedbackFilledCount(string CourseID)
        {
            return _repository.getFeedbackFilledCount(CourseID);
        }

        public DataSet GetQuestionDescription()
        {
            return _repository.GetQuestionDescription();
        }

        public List<KeyValuePair<int, string>> GetTrainingNameList(string empId)
        {
            var trainingList = _repository.GetTrainingNameList(empId);
            trainingList.Insert(0, CommonConstants.selectDefault);
            return trainingList;
        }

        public List<KeyValuePair<int, string>> GetTrainingNameList()
        {
            var trainingList = _repository.GetTrainingNameList();
            trainingList.Insert(0, CommonConstants.selectDefault);
            return trainingList;
        }

        public int SaveFeedbackDetails(FeedbackModel objFeedbackModel)
        {
            return _repository.SaveFeedbackDetails(objFeedbackModel);
        }

        public string GetEmpNameByEmpID(string empID)
        {
            return _repository.GetEmpNameByEmpID(empID);
        }
        #endregion

        public DataSet GetFeedbackRatings(string value, string EmpID)
        {
            return _repository.GetFeedbackRatings(value,EmpID);
        }

        public DataSet GetEmployeesFeedbackByCourseId(string CourseID)
        {
            return _repository.GetEmployeesFeedbackByCourseId(CourseID);
        }

        public int SaveFeedbackDetailsByRMO(NominationModel objNominationModel, string CourseID)
        {
            return _repository.SaveFeedbackDetailsByRMO(objNominationModel, CourseID);
        }

        public DataSet GetFeedbackDetailsForRMO(string CourseID)
        {
            return _repository.GetFeedbackDetailsForRMO(CourseID);
        }

        public double GetAverageRatingsFromParticipants(string QuestionID, string CourseID)
        {
            return _repository.GetAverageRatingsFromParticipants(QuestionID, CourseID);
        }
        public DataSet GetTrainingDetailsByCourseId(string CourseID)
        {
            return _repository.GetTrainingDetailsByCourseId(CourseID);
        }

        public string GetTrainerNameByCourseID(string CourseID)
        {
            return _repository.GetTrainerNameByCourseID(CourseID);
        }

        public int UpdateFlagToFeedbackFilled(string empId, string CourseID)
        {
            return _repository.UpdateFlagToFeedbackFilled(empId, CourseID);
        }


        public int SaveAndUpdateStatusOfTraining(NominationModel objNominationModel, string CourseID)
        {
            return _repository.SaveAndUpdateStatusOfTraining(objNominationModel, CourseID);
        }

        public DataSet GetTrainingVendorDetails()
        {
            return _repository.GetTrainingVendorDetails();
        }
        public DataSet GetVendorDetailsByVendorId(int VendorId)
        {
            return _repository.GetVendorDetailsByVendorId(VendorId);
        }

        public int SaveVendorDetails(VendorModel objVendorModel)
        {
            return  _repository.SaveVendorDetails(objVendorModel);
        }

        public int UpdateVendorDetails(VendorModel objVendorModel)
        {
            return _repository.UpdateVendorDetails(objVendorModel);
        }

        public int DeleteVendorDetails(int VendorId)
        {
            return _repository.DeleteVendorDetails(VendorId);
        }

        public string GetDuplicateStatusOfEmialId(string VendorEmailId)
        {
            return _repository.GetDuplicateStatusOfEmialId(VendorEmailId);
        }

       

        #region TrainingEffectiveness

        public List<TrainingEffectiveness> GetTrainingEffectiveness(int trainingnameid, int LoginEmpId, int LoginEmpIdFlag, string RoleName)
        {
            return _repository.GetTrainingEffectiveness(trainingnameid, LoginEmpId, LoginEmpIdFlag, RoleName);
        }

        public int UpdateTrainingEffectiveness(NominationModel objNominationmodel)
        {
            return _repository.UpdateTrainingEffectiveness(objNominationmodel);
        }

        //Neelam Issue Id:60562 start
        public int UpdateTrainingNominatorEmpID(int CourseID, int EmpID, int PostNominatorId)
        {
            return _repository.UpdateTrainingNominatorEmpID(CourseID, EmpID, PostNominatorId);
        }
        //Neelam Issue Id:60562 end
        public string SendTrainingEffectiveness(NominationModel objNominationmodel)
        {
            return _repository.SendTrainingEffectiveness(objNominationmodel);
        }

        public List<KeyValuePair<int, string>> GetTrainingNameforEffectiveness(int LoginEmpID, string RoleName, int TrainingTypeID)
        {
            var trainingList = _repository.GetTrainingNameforEffectiveness(LoginEmpID, RoleName, TrainingTypeID);
            trainingList.Insert(0, CommonConstants.selectDefault);
            return trainingList;
        }

        public DataSet SendMailToManagerForPostTrainingRatining(int CourseID)
        {
            return _repository.SendMailToManagerForPostTrainingRatining(CourseID);
        }

        public DataSet ManagerFilledPostTrainingRating(int CourseID, int LoginEmpId, string EmpID, string RoleName)
        {
            return _repository.ManagerFilledPostTrainingRating(CourseID, LoginEmpId, EmpID, RoleName);
        }

        public DataSet CheckInActiveManagerForTrainingEffectivness(int CourseID)
        {
            return _repository.CheckInActiveManagerForTrainingEffectivness(CourseID);
        }
        
        #endregion TrainingEffectiveness

        #region Attendance
        public SelectList GetConfirmedTraining(int TrainingTypeId)
        {
            return _repository.GetConfirmedTraining(TrainingTypeId);
        }
        public SelectList GetSeminarKSSList(int TrainingTypeId)
        {
            return _repository.GetSeminarKSSList(TrainingTypeId);
        }
        public DataTable GetEmpPresentee(int CourseId, string AttDate, string Flag)
        {
            return _repository.GetEmpPresentee(CourseId, AttDate, Flag);
        }
        public DataSet GetEmpPresenteeAll(int CourseId, int TrainingTypeID)
        {
            return _repository.GetEmpPresenteeAll(CourseId, TrainingTypeID);
        }

        public int SaveAttendance(AttendanceModel obj)
        {
            return _repository.SaveAttendance(obj);
        }

        public int SaveAttendanceSemiKss(AttendanceModel obj)
        { 
            return _repository.SaveAttendanceSemiKss(obj);
        }

        public DataSet SaveFeedbackSent(AttendanceModel obj)
        {
            return _repository.SaveFeedbackSent (obj);
        }

        public DataTable GetEmpPresenteeSeminar(int RaiseId, string AttendanceDate, string Flag)
        {
            return _repository.GetEmpPresenteeSeminar(RaiseId,AttendanceDate,  Flag);
        }

        public DataTable GetAttendanceReport(int TrainingTypeId, int CourseId)
        {
            return _repository.GetAttendanceReport(TrainingTypeId, CourseId);
        }

        public int SetAttendance(AttendanceModel obj)
        {
            return _repository.SetAttendance(obj);
        }

        public List<SelectListItem> GetAttendanceDates(int CourseId, int TrainingTypeId)
        {
            return _repository.GetAttendanceDates(CourseId, TrainingTypeId);
        }

        public List<Employee> GetNominationDetailsByCourseId(int CourseID, int TrainingTypeID)
        {
            return _repository.GetNominationDetailsByCourseId(CourseID, TrainingTypeID);
        }
        #endregion

        #region TrainingPlan
        public int InsertUpdateTrainingPlan(TrainingPlanModel Obj)
        {
           return _repository.InsertUpdateTrainingPlan(Obj);
        }

        public List<TrainingPlanModel > GetTrainingPlan(TrainingPlanModel Obj)
        {
            return _repository.GetTrainingPlan(Obj);
        }
        public DataTable GetTrainingPlanDataSet(TrainingPlanModel Obj)
        {
            return _repository.GetTrainingPlanDataSet(Obj);
        }
        public List<TrainingPlanModel> DeleteTrainingPlan(int TrainingPlanid, int CreatedBy)
        { 
            return _repository.DeleteTrainingPlan (TrainingPlanid, CreatedBy);
        }

        public List<TrainingPlanModel> GetTrainingPlanEmployees(TrainingPlanModel Obj, int NextQuarter, int Nextyear)
        {
            return _repository.GetTrainingPlanEmployees(Obj, NextQuarter, Nextyear);
        }
        #endregion




        //Harsha- Issue Id- 58975 & 58958 - Start
        //Description- Making Training Cost Editable for Internal Training after Closed status of the training
        //Disabling Number of Hours and Days field once the after training's status is feedback sent
        
        bool ITrainingService.UpdateTrainingCourseTotalCost(int? courseId, float? totalCost)
        {            
            return _repository.UpdateTrainingCourseTotalCost(courseId, totalCost);
        }

        //Harsha- Issue Id- 58975 & 58958 - End


        #region Self Learning

        public int SaveMyTraining(MyTrainingModel myTraining, int empId)
        {
            return _repository.SaveMyTraining(myTraining, empId);
        }

        public IEnumerable<MyTrainingModel> GetMyTrainings(int empId)
        {
            return _repository.GetMyTrainings(empId);
        }

        public int DeleteMyTraining(int trainingId)
        {
            return _repository.DeleteMyTraining(trainingId);
        }

        public MyTrainingModel GetMyTrainingDetails(int trainingId)
        {
            return _repository.GetMyTrainingDetails(trainingId);
        }

        #endregion

    }
}
