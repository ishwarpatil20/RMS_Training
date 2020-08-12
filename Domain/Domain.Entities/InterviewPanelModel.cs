using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class InterviewPanelModel
    {
        public int PanelId{ get; set; }
        public string PanelIdEncr { get; set; }        
        public int EmpId { get; set; }
        public string EmpIdEncr { get; set; }        
        public int CandidateDeptId   { get; set; }
        public string CandidateDesignations { get; set; }
        public string SecondarySkills { get; set; }
        public string LevelIds { get; set; }
        public int Inducted { get; set; }
        public int TrainingAttended { get; set; }
        public int CreatedBy{ get; set; }

        //public IEnumerable<SelectListItem> ListEmpName { get; set; }
        //public IEnumerable<SelectListItem> ListDeptName { get; set; }
        //public IEnumerable<SelectListItem> ListDesignationName { get; set; }
        //public string CandidateDesignationsName { get; set; }
        //public string EmpName { get; set; }
        //public string SecondarySkillsName { get; set; }

        //Get Data
        public string EmpName { get; set; }
        public string DesignationName { get; set; }
        public string LINEMANAGER { get; set; }
        //public string DEPTName { get; set; }
        public string PrimarySkillName { get; set; }
        public string CandidateDesignationName { get; set; }
        public string SecondarySkillname { get; set; }
        public string levelidName { get; set; }
        public string InductedName { get; set; }
        public string TrainingAttendedName { get; set; }
        public string PageMode{ get; set; }
        public string Message { get; set; }
        public string DEPTName { get; set; }
        public string EmpSecondarySkill { get; set; }
        public string BusinessVertical { get; set; }

        public List<SelectListItem> ListCollectionDesign = new List<SelectListItem>();
        public List<SelectListItem> ListCollectionSecSkill = new List<SelectListItem>();


        //public System.Web.Mvc.SelectListItem.<IPListCollecttion> ListCollection { get; set; }
    }

    public class IPListCollecttion
    {
        public int ListId{ get; set; }
        public string ListName { get; set; }
    }


}
