using project.DTO;
using project.Models;
using project.Repositories;

namespace project.Services
{
    public class TestCaseService
    {
        private TestCaseRepository repository;

        private TestCaseService()
        {
            repository = new TestCaseRepository();
        }

        private static readonly TestCaseService _singleton = new TestCaseService();

        public static TestCaseService GetSingleton()
        {
            return _singleton;
        }

        public List<TestCase> GetTestCases()
        {
            return repository.GetTestCases();
        }

        public List<TestCase> GetTestCases(int quesId)
        {
            return repository.GetTestCases(quesId);
        }

        public List<TestCase> GetTestCasesByQuestionId(int quesId)
        {
            return repository.GetTestCasesByQuestionId(quesId);
        }
        public TestCase GetTestCase(int id)
        {
            return repository.GetTestCase(id);
        }

        public void Update(TestCaseDTO testCase)
        {
            repository.Update(testCase);
        }

        public void Add(TestCaseDTO testCase)
        {
            repository.Add(testCase);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
