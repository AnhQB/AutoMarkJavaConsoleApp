using AutoMapper;
using project.DTO;
using project.Models;

namespace project.Repositories
{
    public class TestCaseRepository
    {
        private IMapper mapper;
        private MapperConfiguration config;
        private readonly project_PRN231Context context;
        public TestCaseRepository()
        {
            config = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            mapper = config.CreateMapper();
            context = new project_PRN231Context();
        }
        public List<TestCase> GetTestCases()
        {
         
            return context.TestCases.ToList();
            
        }

        public List<TestCase> GetTestCasesByQuestionId(int quesId)
        {
            return context.TestCases.Where(t => t.QuestionId == quesId).ToList();
        }

        public TestCase GetTestCase(int id)
        {
            return context.TestCases.FirstOrDefault(m => m.TestcaseId == id);
        }

        public void Add(TestCaseDTO testCase)
        {
            context.TestCases.Add(mapper.Map<TestCaseDTO, TestCase>(testCase));
            context.SaveChanges();
            
        }

        public void Update(TestCaseDTO testCase)
        {
            var auData = context.TestCases.FirstOrDefault(item => item.TestcaseId == testCase.TestcaseId);
            if (auData == null)
                throw new Exception("Not found TestCase to update");

            auData.Input = testCase.Input;
            auData.Output = testCase.Output;
            auData.Mark = testCase.Mark;
            auData.QuestionId = testCase.QuestionId;

            context.SaveChanges();
            
        }

        public void Delete(int id)
        {
            var author = context.TestCases.FirstOrDefault(item => item.TestcaseId == id);
            if (author == null)
                throw new Exception("Not found TestCase");


            //context.Database.ExecuteSqlRaw("Delete from BookAuthor where book_id =" + id);
            context.TestCases.Remove(author);
            context.SaveChanges();
            
        }
    }
}
