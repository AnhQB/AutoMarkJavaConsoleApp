using project.DTO;
using project.Models;
using project.Repositories;
using project.Repositories.IRepositories;
using project.Services.IServices;

namespace project.Services
{
    public class ExamService
    {
        private ExamRepository repository;

        private ExamService()
        {
            repository = new ExamRepository();
        }

        private static readonly ExamService _singleton = new ExamService();
        public static ExamService GetSingleton()
        {
            return _singleton;
        }

        public Dictionary<string, List<ExamDTO>> GetExams()
        {
            return repository.GetExams();
        }

        public List<Exam> GetExamsDistinct()
        {
            return repository.GetExamsDistinct();
        }

        public List<Exam> GetExam(int id)
        {
            return repository.GetExam(id);
        }
        public List<ExamDTO> GetExamNo()
        {
            return repository.GetExamsNo();
        }

        public ExamDTO GetExam(int id, int paperNo)
        {
            return repository.GetExam(id, paperNo);
        }

        public void Update(ExamDTO exam)
        {
            repository.Update(exam);
        }

        public void Add(ExamDTO exam)
        {
            repository.Add(exam);
        }

        public void Delete(int id, int paperNo)
        {
            repository.Delete(id, paperNo);
        }

        public IQueryable<Exam> GetAllExams()
        {
            return repository.GetAllExams();
        }
    }
}
