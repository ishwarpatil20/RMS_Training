using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure;
using Domain.Entities;
using System.Transactions;
using System.Web.Mvc;
using System.Runtime.Remoting.Contexts;
namespace Services
{
    public class InterviewPanelService : IInterviewPanelService
    {
        private IInterviewPanelRepository<T_InterviewPanel> _IInterviewPanelRepository;
        private IIPanelLevelRepository<T_IP_PanelLevel> _objIIPanelLevelRepository;
        private IIPanelDesignationRepository<T_IP_PanelDesignation> _objIIPaneldesignationRepository;
        private IMasterRepository<T_Master> _objIMasterRepository;
        private readonly IEmployeeService _objEmployeeService;

        public InterviewPanelService(IInterviewPanelRepository<T_InterviewPanel> interviewpanelrepository,
            IIPanelLevelRepository<T_IP_PanelLevel> objIIPanelLevelRepository,
            IIPanelDesignationRepository<T_IP_PanelDesignation> objIIPaneldesignationRepository,
            IMasterRepository<T_Master> objIMasterRepository,
            IEmployeeService objEmployeeService)
        {
            _IInterviewPanelRepository = interviewpanelrepository;
            _objIIPanelLevelRepository= objIIPanelLevelRepository;
            _objIIPaneldesignationRepository = objIIPaneldesignationRepository;
            _objIMasterRepository = objIMasterRepository;
            _objEmployeeService = objEmployeeService;
        }

        public string IsAlreadyExist (InterviewPanelModel Obj)
        {
            string strResult = "";
            //using (TransactionScope ts = new TransactionScope())
            //{
                try
                {   
                    string[] strLevel = Obj.LevelIds.Split(',');
                    for (int i = 0; i < strLevel.Length; i++)
                    {                        
                        //Save Designation againt Level
                        string[] strDesg = Obj.CandidateDesignations.Split(',');
                        for (int j = 0; j < strDesg.Length; j++)
                        {
                            bool IslevelwiseDesignExist = _IInterviewPanelRepository.IsAlreadyExist(Convert.ToInt32(Obj.EmpId),
                                Convert.ToInt32(strLevel[i]), Convert.ToInt32(strDesg[j]));
                            //If exist, skip furture save
                            if (IslevelwiseDesignExist)
                            {
                                T_Master objLevel = new T_Master();
                                objLevel = _objIMasterRepository.SingleOrDefault(Convert.ToInt32(strLevel[i]));

                                //SelectList SL = _objEmployeeService.GetSkillTypesCategory();
                                IEnumerable<SelectListItem> SL= _objEmployeeService.FillDesignationList(Convert.ToInt32(Obj.CandidateDeptId));
                                string DesignationName = SL.Where(p => p.Value == strDesg[j]).First().Text;

                                 strResult = DesignationName + " Designation has already been entered for " + objLevel .Name+ " Level";
                               //ts.Dispose();
                                return strResult;
                            }                            
                        }
                    }
                    //ts.Complete();
                }
                catch (Exception ex)
                {
                    //ts.Dispose();
                    throw ex;
                }
            //}

            return strResult;
        }
        public int Insert(InterviewPanelModel Obj)
        {
            
            int PanelId = 0;

            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    T_InterviewPanel ObjT_IP = new T_InterviewPanel();

                    ObjT_IP.EmpId = Obj.EmpId;
                    ObjT_IP.CandidateDesignations = Obj.CandidateDesignations;
                    ObjT_IP.CandidateDeptId = Obj.CandidateDeptId;
                    ObjT_IP.SecondarySkills = Obj.SecondarySkills;
                    ObjT_IP.LevelIds = Obj.LevelIds;
                    ObjT_IP.TrainingAttended = Convert.ToInt32(Obj.TrainingAttended);
                    ObjT_IP.Inducted = Convert.ToInt32(Obj.Inducted);
                    ObjT_IP.CreatedBy = Obj.CreatedBy;
                    ObjT_IP.IsActive = 1;
                    ObjT_IP.CreatedDate = DateTime.Now;

                    ObjT_IP = _IInterviewPanelRepository.Insert(ObjT_IP);
                    PanelId = ObjT_IP.PanelId;

                    //Save Level in Child table
                    string[] strLevel = Obj.LevelIds.Split(',');
                    for (int i = 0; i < strLevel.Length; i++)
                    {
                        T_IP_PanelLevel objIPL = new T_IP_PanelLevel();

                        objIPL.PanelId = ObjT_IP.PanelId;
                        objIPL.Empid= ObjT_IP.EmpId;
                        objIPL.LevelId = Convert.ToInt32(strLevel[i]);
                        objIPL.CreatedBy = Obj.CreatedBy;
                        objIPL.CreatedDate = DateTime.Now;
                        objIPL.IsActive = 1;
                        objIPL = _objIIPanelLevelRepository.Insert(objIPL);

                        //Save Designation againt Level
                        string[] strDesign = Obj.CandidateDesignations.Split(',');
                        for (int j = 0; j < strDesign.Length; j++)
                        {
                            T_IP_PanelDesignation objDes = new T_IP_PanelDesignation();
                            objDes.PanelId = ObjT_IP.PanelId;
                            objDes.Empid = ObjT_IP.EmpId;
                            objDes.LevelId = Convert.ToInt32(strLevel[i]);
                            objDes.DesignationId = Convert.ToInt32(strDesign[j]);
                            objDes.CreatedBy = Obj.CreatedBy;
                            objDes.CreatedDate = DateTime.Now;
                            objDes.IsActive = 1;

                            bool IslevelwiseDesignExist = _IInterviewPanelRepository.IsAlreadyExist(Convert.ToInt32(objDes.Empid),Convert.ToInt32(objDes.LevelId), Convert.ToInt32(objDes.DesignationId));
                            //If exist, skip furture save
                            if (IslevelwiseDesignExist)
                            {
                                ts.Dispose();
                                return 0;
                            }
                            objDes = _objIIPaneldesignationRepository.Insert(objDes);
                        }
                    }
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    throw ex;
                }
            }
            
            return PanelId;
        }

        

        public int Delete(InterviewPanelModel Obj)
        {
            int result = 0;
            using (TransactionScope Ts = new TransactionScope())
            {
                //Delete Panel Data
                T_InterviewPanel objIP = new T_InterviewPanel();
                objIP .PanelId =Obj.PanelId;
                objIP = _IInterviewPanelRepository.SingleOrDefault(Obj.PanelId);                
                //Set The data which need to update
                objIP.PanelId = Obj.PanelId;
                objIP.IsActive= 0;
                objIP.UpdatedBy = Obj.CreatedBy;
                objIP.UpdatedDate = DateTime.Today;
                result = _IInterviewPanelRepository.Delete(objIP);
                
                //Delete Level
                if (result == 1)
                {
                    result =_IInterviewPanelRepository.DeleteInterviewPanel(Obj);                  
                }
                Ts.Complete(); 
                return 1;
            }
        }



        public List<InterviewPanelModel> GetInterviewPanel(int PanelId,int Empid)
        {
            return _IInterviewPanelRepository.GetInterviewPanel(PanelId,Empid );
        }

        public List<SelectListItem> GetInterviewPanelDetail(int PanelId, string IPType)
        {
            return _IInterviewPanelRepository.GetInterviewPanelDetail(PanelId, IPType);
        }

        public List<SelectListItem> GetEmployeeSkillLinked(string strType,string EmpId)
        {
            return _IInterviewPanelRepository.GetEmployeeSkillLinked(strType, EmpId);
        }

        public int UpdateInterviewPanel(InterviewPanelModel objUpdate)
        {
            return _IInterviewPanelRepository.UpdateInterviewPanel(objUpdate);
        }


        public List<InterviewPanelModel> GetInterviewPanelSearch(string Technology, string Level, string DeptId, string Designation, string BusinessVertical, string Skill)
        {
            return _IInterviewPanelRepository.GetInterviewPanelSearch( Technology,  Level,  DeptId,  Designation,  BusinessVertical,  Skill);
        }
        
        //public int Update(InterviewPanelModel Obj)
        //{ }
        //public List<InterviewPanelModel> GetInterviewPanel(InterviewPanelModel Obj)
        //{ 
        
        //}
        //{ }
        //public List<InterviewPanelModel> Delete(int PanelId, int CreatedBy)
        //{ }
    }
}
