namespace project.DTO
{
    public class GradeNote
    {
        public int QuestionId { get; set; }
        public string? QuestionName { get; set; }

        public double? Mark { get; set; }

        public override string? ToString()
        {
            return "[" + QuestionName +", Mark="+ Mark + "]";
        }
    }
}
