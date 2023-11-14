using System;
using System.Collections.Generic;

namespace candidate.Models
{
    public partial class CandidateSkill
    {
        public int CandidateSkillsId { get; set; }
        public int? CandidateId { get; set; }
        public int? SkillsId { get; set; }

        public virtual Candidate? Candidate { get; set; }
        public virtual Skill? Skills { get; set; }
    }
}
