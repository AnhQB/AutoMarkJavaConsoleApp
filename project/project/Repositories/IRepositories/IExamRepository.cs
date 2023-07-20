using AutoMapper;
using project.DTO;
using project.Models;

namespace project.Repositories.IRepositories
{
    public interface IExamRepository
    {
        public List<Exam> GetExams();

        public IQueryable<Exam> GetAllExams();


        public List<Exam> GetExam(int id);

        public void Add(ExamDTO exam);

        public void Update(ExamDTO exam);

        public void Delete(int id);
    }
}
