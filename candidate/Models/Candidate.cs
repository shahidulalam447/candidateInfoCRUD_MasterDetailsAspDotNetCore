using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace candidate.Models
{
    public partial class Candidate
    {
        public Candidate()
        {
            CandidateSkills = new HashSet<CandidateSkill>();
        }

        public int CandidateId { get; set; }
        [Display(Name = "Candidate Name")]
        public string CandidateName { get; set; } = null!;
        [DataType(DataType.Date), Display(Name = "Date Of Birth"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; } = null!;
        public string? Image { get; set; }
        public bool Fresher { get; set; }

        public virtual ICollection<CandidateSkill> CandidateSkills { get; set; }
    }
}
