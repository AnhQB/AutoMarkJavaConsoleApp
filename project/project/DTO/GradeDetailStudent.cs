using project.Models;

namespace project.DTO
{
    public class GradeDetailStudent
    {
        public GradeDetailStudent()
        {
            TestCases = new HashSet<TestCaseDetailStudent>();
        }
        public int QuestionId { get; set; }
        public string? QuestionName { get; set; }
        public double? QuestionMark { get; set; }
        public double? QuestionMarkStudent { get; set; }

        public virtual ICollection<TestCaseDetailStudent> TestCases { get; set; }

    }
}
