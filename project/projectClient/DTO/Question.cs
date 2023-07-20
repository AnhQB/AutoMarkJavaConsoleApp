using projectClient.Models;
using System;
using System.Collections.Generic;

namespace project.Models
{
    public partial class Question
    {
        public Question()
        {
            TestCases = new HashSet<TestCase>();
        }

        public int QuestionId { get; set; }
        public string? QuestionName { get; set; }
        public double? Mark { get; set; }
        public int? ExamId { get; set; }
        public int? PaperNo { get; set; }

        public virtual ICollection<TestCase> TestCases { get; set; }
    }
}
