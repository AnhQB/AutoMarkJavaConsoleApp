using System;
using System.Collections.Generic;

namespace project.Models
{
    public partial class Question
    {
        public Question()
        {
            GradeDetails = new HashSet<GradeDetail>();
            TestCases = new HashSet<TestCase>();
        }

        public int QuestionId { get; set; }
        public string? QuestionName { get; set; }
        public double? Mark { get; set; }
        public int? ExamId { get; set; }
        public int? PaperNo { get; set; }

        public virtual Exam? Exam { get; set; }
        public virtual ICollection<GradeDetail> GradeDetails { get; set; }
        public virtual ICollection<TestCase> TestCases { get; set; }
    }
}
