using AutoMapper;
using project.DTO;
using project.Models;

namespace project.Repositories
{
    public class StudentRepository
    {
        private IMapper mapper;
        private MapperConfiguration config;
        private readonly project_PRN231Context context;
        public StudentRepository()
        {
            config = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            mapper = config.CreateMapper();
            context = new project_PRN231Context();
        }
        public List<Student> GetStudents()
        {
           
            return context.Students.ToList();
            
        }



        public Student GetStudent(int id)
        {
            return context.Students.FirstOrDefault(m => m.StudentId == id);
            
        }

        public void Add(StudentDTO student)
        {
            context.Students.Add(mapper.Map<StudentDTO, Student>(student));
            context.SaveChanges();
            
        }

        public void Update(StudentDTO student)
        {
            var auData = context.Students.FirstOrDefault(item => item.StudentId == student.StudentId);
            if (auData == null)
                throw new Exception("Not found Student to update");

            auData.StudentName = student.StudentName;

            context.SaveChanges();
            
        }

        public void Delete(int id)
        {
            var student = context.Students.FirstOrDefault(item => item.StudentId == id);
            if (student == null)
                throw new Exception("Not found Student");

            //context.Database.ExecuteSqlRaw("Delete from BookAuthor where book_id =" + id);
            context.Students.Remove(student);
            context.SaveChanges();
            
        }
    }
}
