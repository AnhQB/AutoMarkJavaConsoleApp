using project.DTO;
using project.Models;
using project.Repositories;

namespace project.Services
{
    public class QuestionService
    {
        private QuestionRepository repository;

        private QuestionService()
        {
            repository = new QuestionRepository();
        }

        private static readonly QuestionService _singleton = new QuestionService();

        public static QuestionService GetSingleton()
        {
            return _singleton;
        }

        public Dictionary<string, Dictionary<string, List<Question>>> GetQuestions()
        {
            return repository.GetQuestions();
        }

        public Question GetQuestion(int id)
        {
            return repository.GetQuestion(id);
        }

        public void Update(QuestionDTO Question)
        {
            repository.Update(Question);
        }

        public void Add(QuestionDTO Question)
        {
            repository.Add(Question);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
