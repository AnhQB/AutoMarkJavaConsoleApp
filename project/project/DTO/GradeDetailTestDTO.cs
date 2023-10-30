using project.Models;

namespace project.DTO
{
    public class GradeDetailTestDTO
    {
        public int QuestionId { get; set; }
        public string? Output { get; set; }
        public bool? Testresult { get; set; }
        public int TestcaseId { get; set; }

        public virtual Question Question { get; set; } = null!;

        public virtual TestCase Testcase { get; set; } = null!;
    }
}
