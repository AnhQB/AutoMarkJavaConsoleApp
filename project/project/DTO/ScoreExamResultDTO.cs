using project.Models;

namespace project.DTO
{
    public class ScoreExamResultDTO
    {
        public ScoreExamResultDTO()
        {
            GradeDetails = new HashSet<GradeDetail>();
        }

        public int ExamresultId { get; set; }
        public double? Mark { get; set; }
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public int PaperNo { get; set; }
        public virtual ICollection<GradeDetail> GradeDetails { get; set; }
    }
}
