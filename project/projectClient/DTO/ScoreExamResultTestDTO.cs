namespace projectClient.DTO
{
    public class ScoreExamResultTestDTO
    {
        public ScoreExamResultTestDTO()
        {
            GradeDetails = new HashSet<GradeDetailTestDTO>();
        }

        public double? Mark { get; set; }
        public int ExamId { get; set; }
        public int PaperNo { get; set; }
        public virtual ICollection<GradeDetailTestDTO> GradeDetails { get; set; }
    }
}
