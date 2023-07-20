using System;
using System.Collections.Generic;

namespace projectClient.Models
{
    public partial class GradeDetail
    {
        public int ExamresultId { get; set; }
        public int QuestionId { get; set; }
        public int TestcaseId { get; set; }
        public string? Output { get; set; }
        public bool? Testresult { get; set; }

        public virtual TestCase Testcase { get; set; } = null!;
    }
}
