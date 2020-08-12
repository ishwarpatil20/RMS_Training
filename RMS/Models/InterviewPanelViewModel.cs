using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RMS.Models
{
    public class InterviewPanelViewModel
    {
        public int InterviewPanelId { get; set; }

        public int EmpId { get; set; }        
        public IEnumerable<SelectListItem> ListEmpName { get; set; }
        
        public string EmpTechnology { get; set; }
        public IEnumerable<SelectListItem> ListTechnology { get; set; }
        public string EmpDesignation { get; set; }


        public int CandidateDeptId { get; set; }
        public IEnumerable<SelectListItem> ListDeptName { get; set; }

        public int CandidateDesigntionId { get; set; }
        public IEnumerable<SelectListItem> ListDesignationName { get; set; }
        public string CandidateDesignations { get; set; }

        //public string SecondarySkillId { get; set; }
        public IEnumerable<SelectListItem> ListSecondarySkills { get; set; }        
        public string SecondarySkills { get; set; }

        //public string CandidateDesignationsName { get; set; }
        //public string EmpName { get; set; }
        //public string SecondarySkillsName { get; set; }

        public int InductedId { get; set; }
        public IEnumerable<SelectListItem> ListInducted { get; set; }

        public IEnumerable<SelectListItem> ListLevel { get; set; }        
        public string LevelIds { get; set; }

        public int TrainingAttendedId { get; set; }
        public IEnumerable<SelectListItem> ListAttended { get; set; }
        //public int TrainingAttended { get; set; }
        public int CreatedBy { get; set; }
                
        
        //public string[] LstSecondarySkill { get; set; }

        //public int LevelId { get; set; }
        public List<InterviewPanelModel > ListAllIP { get; set; }
        public List<InterviewPanelModel> ListSingleIp { get; set; }

        public string Mode { get; set; }
        public string  Message { get; set; }

        public IEnumerable<SelectListItem> ListBusinessVertical { get; set; }
        public string  BusinessVertical { get; set; }
        
    }


}

