using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace candidate.Models.ViewModels
{
    public class CandidateVM
    {
        public CandidateVM()
        {
            this.SkillList = new List<int?>();
        }
        public int CandidateId { get; set; }
        [Display(Name = "Candidate Name")]
        public string CandidateName { get; set; } = default!;
        [DataType(DataType.Date), Display(Name = "Date Of Birth"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; } = default!;
        [Display(Name = "Image")]
        public IFormFile? ImagePath { get; set; } = default!;
        public string? Image { get; set; }
        public bool Fresher { get; set; }
        public List<int?> SkillList { get; set; }
    }

}
