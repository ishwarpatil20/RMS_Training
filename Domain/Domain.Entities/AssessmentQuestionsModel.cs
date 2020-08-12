using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AssessmentQuestionsModel
    {
        public int AssessmentPaperDetailsId { get; set; }

        public int AssessmentPaperId { get; set; }

        public int QuestionId { get; set; }

        public string Question { get; set; }

        public byte[]  QuestionImage { get; set; }

        public string QuestionImgFileName { get; set; }

        public string Option1Description { get; set; }

        public byte[] Option1Image { get; set; }

        public string Option1ImageFileName { get; set; }

        public bool IsOption1Correct { get; set; }

        public string Option2Description { get; set; }

        public byte[] Option2Image { get; set; }

        public string Option2ImageFileName { get; set; }

        public bool IsOption2Correct { get; set; }

        public string Option3Description { get; set; }

        public byte[] Option3Image { get; set; }

        public string Option3ImageFileName { get; set; }

        public bool IsOption3Correct { get; set; }

        public string Option4Description { get; set; }

        public byte[] Option4Image { get; set; }

        public string Option4ImageFileName { get; set; }

        public bool IsOption4Correct { get; set; }

        public string Option5Description { get; set; }

        public byte[] Option5Image { get; set; }

        public string Option5ImageFileName { get; set; }

        public bool IsOption5Correct { get; set; }

        public string Option6Description { get; set; }

        public byte[] Option6Image { get; set; }

        public string Option6ImageFileName { get; set; }

        public bool IsOption6Correct { get; set; }

        public bool IsMultiChoiceAnswer { get; set; }

        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int LastEditedBy { get; set; }

        public DateTime LastEditedOn { get; set; }

    }
}
