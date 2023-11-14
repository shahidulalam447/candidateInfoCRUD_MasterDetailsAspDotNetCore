using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace candidate.Models
{
    public partial class Skill
    {
        public Skill()
        {
            CandidateSkills = new HashSet<CandidateSkill>();
        }

        public int SkillsId { get; set; }
        [Display(Name = "Skill Name")]
        public string SkillName { get; set; } = null!;

        public virtual ICollection<CandidateSkill> CandidateSkills { get; set; }
    }
}
