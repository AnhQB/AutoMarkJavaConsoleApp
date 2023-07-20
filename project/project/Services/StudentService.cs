using project.DTO;
using project.Models;
using project.Repositories;

namespace project.Services
{
    public class StudentService
    {
        private StudentRepository repository;

        private StudentService()
        {
            repository = new StudentRepository();
        }

        private static readonly StudentService _singleton = new StudentService();

        public static StudentService GetSingleton()
        {
            return _singleton;
        }

        public List<Student> GetStudents()
        {
            return repository.GetStudents();
        }

        public Student GetStudent(int id)
        {
            return repository.GetStudent(id);
        }

        public void Update(StudentDTO Student)
        {
            repository.Update(Student);
        }

        public void Add(StudentDTO Student)
        {
            repository.Add(Student);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
