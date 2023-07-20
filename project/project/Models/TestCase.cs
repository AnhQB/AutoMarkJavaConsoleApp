using System;
using System.Collections.Generic;

namespace project.Models
{
    public partial class TestCase
    {
        public TestCase()
        {
            GradeDetails = new HashSet<GradeDetail>();
        }

        public int TestcaseId { get; set; }
        public string? Input { get; set; }
        public string? Output { get; set; }
        public double? Mark { get; set; }
        public int? QuestionId { get; set; }

        public virtual Question? Question { get; set; }
        public virtual ICollection<GradeDetail> GradeDetails { get; set; }
    }
}
