using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace Infrastructure
{
    public class InterviewPanelRepository : BaseRepository<T_InterviewPanel>, IInterviewPanelRepository<T_InterviewPanel>
    {
        //MasterRepository objMasterRepository = null;

        public InterviewPanelRepository(Infrastructure.Interfaces.IUnitOfWork unit)
            : base(unit)
        {
            
        }

        //public List<InterviewPanelModel> GetInterviewPanel(InterviewPanelModel objIP)
        public List<InterviewPanelModel> GetInterviewPanel(int PanelId, int Empid)
        {
            List<InterviewPanelModel> objIPList = new List<InterviewPanelModel>();
            InterviewPanelModel objIP;
            using (var result = new RMS_Entities())
            {
                var GetIPList = result.USP_IP_GetInterviewPanel(Convert.ToInt32(PanelId),Convert.ToInt32(Empid)).ToList<USP_IP_GetInterviewPanel_Result>();
                foreach (var obj in GetIPList)
                {
                    objIP = new InterviewPanelModel();
                    objIP.PanelId = obj.panelid;
                    objIP.PanelIdEncr = CommonRepository.Encode(obj.panelid.ToString());
                    objIP.EmpId = Convert.ToInt32(obj.empid);
                    objIP.EmpIdEncr = CommonRepository.Encode(obj.empid.ToString());
                    objIP.EmpName = obj.EmpName;
                    objIP.DesignationName = obj.DesignationName;
                    objIP.LINEMANAGER = obj.LINEMANAGER;
                    objIP.PrimarySkillName = obj.PrimarySkillName;
                    objIP.CandidateDeptId = Convert.ToInt32(obj.CandidateDeptId);
                    //objIP.DEPTName = obj.DEPTName;
                    objIP.CandidateDesignations = obj.CandidateDesignations;
                    objIP.CandidateDesignationName = obj.CandidateDesignationName;
                    objIP.SecondarySkills = obj.SecondarySkills;
                    objIP.SecondarySkillname = obj.SecondarySkillname;
                    objIP.LevelIds = obj.levelids;
                    objIP.levelidName = obj.levelidName;
                    objIP.Inducted = Convert.ToInt32(obj.Inducted);
                    objIP.InductedName = obj.InductedName;
                    objIP.TrainingAttended = Convert.ToInt32(obj.TrainingAttended);
                    objIP.TrainingAttendedName = obj.TrainingAttendedName;
                    objIP.EmpSecondarySkill = obj.SecondarySkill;
                    objIP.BusinessVertical= obj.BusinessVertical;
                    objIP.PageMode = "V";
                    objIPList.Add(objIP);
                }
            }
            return objIPList;
        }

        public List<SelectListItem> GetEmployeeSkillLinked(string IPType, string EmpId)
        {
            List<SelectListItem> objIPList = new List<SelectListItem>();
            SelectListItem objIP;
            using (var result = new RMS_Entities())
            {
                var GetIPList = result.USP_IP_GetEmpSkills(IPType, Convert.ToInt32( EmpId)).ToList<USP_IP_GetEmpSkills_Result>();
                foreach (var obj in GetIPList)
                {
                    objIP = new SelectListItem();
                    objIP.Value = Convert.ToInt32(obj.MasterID).ToString();
                    objIP.Text = obj.MasterName;
                    objIPList.Add(objIP);
                }

                SelectListItem tempValue = new SelectListItem();
                tempValue.Text="Select" ;
                tempValue.Value = "0";
                //var list = result.ToList(); // Create mutable list

                objIPList.Insert(0, tempValue);
            }
            return objIPList;
        }

        public List<SelectListItem> GetInterviewPanelDetail(int PanelId, string IPType)
        {
            List<SelectListItem> objIPList = new List<SelectListItem>();
            SelectListItem objIP;
            using (var result = new RMS_Entities())
            {
                var GetIPList = result.USP_IP_GetInterviewPanelDetail(PanelId, IPType).ToList < USP_IP_GetInterviewPanelDetail_Result>();
                foreach (var obj in GetIPList)
                {
                    objIP = new SelectListItem();
                    objIP.Value = Convert.ToInt32(obj.ListId).ToString();
                    objIP.Text = obj.Name;
                    objIPList.Add(objIP);
                }
            }
            return objIPList;
        }
        public int UpdateInterviewPanel(InterviewPanelModel objUpdate)
        {
            using (var _context = new RMS_Entities())
            {

                var GetIPList = _context.Usp_IP_UpdateIP(objUpdate.PanelId, objUpdate.EmpId, objUpdate.CandidateDeptId, objUpdate.CandidateDesignations, objUpdate.SecondarySkills, objUpdate.LevelIds, objUpdate.Inducted, objUpdate.TrainingAttended, objUpdate.CreatedBy)
                    .ToList();
                return 0;
            }
        }
        public int DeleteInterviewPanel(InterviewPanelModel objUpdate)
        {
            using (var db = new RMS_Entities())
            {

                var list = from data in db.T_IP_PanelLevel
                           where data.PanelId == objUpdate.PanelId
                           select data;
                //Delete Level
                foreach (T_IP_PanelLevel i in list)
                {
                    var c = (from singledata in db.T_IP_PanelLevel
                             where singledata.LevelId == i.LevelId && singledata.IsActive == 1
                             select singledata).FirstOrDefault();
                    if (c != null)
                    {
                        T_IP_PanelLevel objIPL = new T_IP_PanelLevel();
                        c.IsActive = 0;
                        c.UpdatedBy = objUpdate.CreatedBy;
                        c.UpdatedDate = DateTime.Today;
                    }
                    db.SaveChanges();
                }

                //Delete Level
                var listDesign = from data in db.T_IP_PanelDesignation
                                 where data.PanelId == objUpdate.PanelId
                                 select data;
                //Delete Level
                foreach (T_IP_PanelDesignation i in listDesign)
                {
                    var c = (from singledata in db.T_IP_PanelDesignation
                             where singledata.PanelDesignationId == i.PanelDesignationId && singledata.IsActive == 1
                             select singledata).FirstOrDefault();
                    if (c != null)
                    {
                        T_IP_PanelDesignation objIPLD = new T_IP_PanelDesignation();
                        // c.PanelDesignationId = objIPLD.PanelDesignationId;
                        c.IsActive = 0;
                        c.UpdatedBy = objUpdate.CreatedBy;
                        c.UpdatedDate = DateTime.Today;
                    }
                    db.SaveChanges();
                }
                return 1;
            }
        }

        public bool IsAlreadyExist( int EmpId , int LevelId,int DesignationId)
        {
            using (var db = new RMS_Entities())
            {
                var list = (from data in db.T_IP_PanelDesignation
                            where data.DesignationId == DesignationId
                            && data.LevelId == LevelId
                            && data.Empid == EmpId
                            && data.IsActive == 1
                            select data).FirstOrDefault();

                if (list != null)
                {
                    return true;
                }
            }
            return false;
        }

        public List<InterviewPanelModel> GetInterviewPanelSearch(string  Technology,string Level ,string DeptId,string Designation,
            string BusinessVertical,string Skill)
        {
            List<InterviewPanelModel> objIPList = new List<InterviewPanelModel>();
            InterviewPanelModel objIP;
            using (var result = new RMS_Entities())
            {
                var GetIPList = result.USP_IP_GetInterviewPanelSearch(Convert.ToInt32( Technology),
                    Convert.ToInt32(Level),
                    Convert.ToInt32(DeptId),
                    Convert.ToInt32(Designation),
                    Convert.ToInt32(BusinessVertical)
                    ,Skill).ToList<USP_IP_GetInterviewPanelSearch_Result>();
                foreach (var obj in GetIPList)
                {
                    objIP = new InterviewPanelModel();
                    objIP.PanelId = obj.PANELID;
                    objIP.PanelIdEncr = CommonRepository.Encode(obj.PANELID.ToString());
                    objIP.EmpId = Convert.ToInt32(obj.EMPID);
                    objIP.EmpName = obj.EMPNAME;
                    objIP.DesignationName = obj.DesignationName;
                    objIP.LINEMANAGER = obj.LINEMANAGER;
                    objIP.BusinessVertical = obj.BUSINESSVERTICAL;
                    objIP.EmpSecondarySkill= obj.SECONDARYSKILL;
                    objIP.PrimarySkillName = obj.PRIMARYSKILLNAME;
                    objIP.CandidateDeptId = Convert.ToInt32(obj.CANDIDATEDEPTID);
                    objIP.DEPTName = obj.DEPTNAME;
                    objIP.CandidateDesignations = obj.CANDIDATEDESIGNATIONS;
                    objIP.CandidateDesignationName = obj.CANDIDATEDESIGNATIONNAME;
                    objIP.SecondarySkills = obj.SECONDARYSKILLS;
                    objIP.SecondarySkillname = obj.SECONDARYSKILLNAME;
                    objIP.LevelIds = obj.LEVELIDS;
                    objIP.levelidName = obj.LEVELIDNAME;
                    objIP.Inducted = Convert.ToInt32(obj.INDUCTED);
                    objIP.InductedName = obj.INDUCTEDNAME;
                    objIP.TrainingAttended = Convert.ToInt32(obj.TRAININGATTENDED);
                    objIP.TrainingAttendedName = obj.TRAININGATTENDEDNAME;
                    objIP.PageMode = "S";
                    objIPList.Add(objIP);
                }
            }
            return objIPList;
        }


    }
}
