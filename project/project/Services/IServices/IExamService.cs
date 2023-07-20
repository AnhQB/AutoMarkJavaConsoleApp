using project.DTO;
using project.Models;

namespace project.Services.IServices
{
    public interface IExamService
    {
        public List<Exam> GetExams();
        public List<Exam> GetExam(int id);
        public void Update(ExamDTO exam);
        public void Add(ExamDTO exam);
        public void Delete(int id);
        public IQueryable<Exam> GetAllExams();
    }
}
