using project.DTO;

namespace project.Services.IServices
{
    public interface IExamResultService
    {
        public void AutoScoring(IFormFile file);

        public  Dictionary<string, Dictionary<string, ScoreExamResultDTO>>
            AutoScoring(IFormFile file, int examId);
    }
}
