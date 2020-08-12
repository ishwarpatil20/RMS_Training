using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Domain.Entities;
using System.Web.Mvc;
namespace Infrastructure.Interfaces
{
    public interface IInterviewPanelRepository<T> : IBaseRepository<T>
        where T : class
    {

        List<InterviewPanelModel> GetInterviewPanel(int PanelId, int EmpId);
        List<SelectListItem> GetInterviewPanelDetail(int PanelId, string strType);
        List<SelectListItem> GetEmployeeSkillLinked(string strType, string EmpId);        
        int UpdateInterviewPanel(InterviewPanelModel objUpdate);
        int DeleteInterviewPanel(InterviewPanelModel objUpdate);
        bool IsAlreadyExist(int EmpId, int LevelId, int DesignationId);

        List<InterviewPanelModel> GetInterviewPanelSearch(string Technology, string Level, string DeptId, string Designation, string BusinessVertical, string Skill);
    }

}
