using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using System.Web.Mvc;

namespace Services.Interfaces
{
    public interface IInterviewPanelService 
    {
        int Insert(InterviewPanelModel Obj);
        int Delete(InterviewPanelModel Obj);

        List<InterviewPanelModel> GetInterviewPanel(int PanelId, int Empid);
        List<SelectListItem> GetInterviewPanelDetail(int PanelId, string IPType);
        List<SelectListItem> GetEmployeeSkillLinked(string strType, string EmpId);
        int UpdateInterviewPanel(InterviewPanelModel objUpdate);
        string IsAlreadyExist(InterviewPanelModel Obj);

        List<InterviewPanelModel> GetInterviewPanelSearch(string Technology, string Level, string DeptId, string Designation, string BusinessVertical, string Skill);
        //List<InterviewPanelModel> GetInterviewPanel(InterviewPanelModel Obj);
        ////DataTable GetTrainingPlanDataSet(TrainingPlanModel Obj);
        //List<InterviewPanelModel> Delete(int PanelId, int CreatedBy);
    }
}
