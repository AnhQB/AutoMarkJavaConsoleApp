using AutoMapper;
using Microsoft.EntityFrameworkCore;
using project.DTO;
using project.Models;

namespace project.Repositories
{
    public class QuestionRepository
    {
        private IMapper mapper;
        private MapperConfiguration config;
        private readonly project_PRN231Context context;
        public QuestionRepository()
        {
            config = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            mapper = config.CreateMapper();
            context = new project_PRN231Context();
        }
        public Dictionary<string, Dictionary<string, List<Question>>> GetQuestions()
        {
            var a = context.Questions.ToList();
            Dictionary<string, Dictionary<string, List<Question>>> list = new Dictionary<string, Dictionary<string, List<Question>>>();
            foreach (var i in a)
            {
                if (!list.ContainsKey(i.ExamId.ToString() == null ? "*" : i.ExamId.ToString()))
                {
                    Dictionary<string, List<Question>> temp = new Dictionary<string, List<Question>>();
                    temp[i.PaperNo.ToString()] = new List<Question>
                    {
                        i
                    };
                    list[i.ExamId.ToString()] = temp;
                }
                else
                {
                    if (!list[i.ExamId.ToString()].ContainsKey(i.PaperNo.ToString()))
                    {
                        Dictionary<string, List<Question>> temp = new Dictionary<string, List<Question>>();
                       
                        list[i.ExamId.ToString()].Add(i.PaperNo.ToString(), new List<Question>
                        {
                            i
                        });
                    }
                    else
                    {
                        list[i.ExamId.ToString()][i.PaperNo.ToString()].Add(i);
                    }
                }
            }

            return list;
            
        }



        public Question GetQuestion(int id)
        {
            
            return context.Questions.FirstOrDefault(m => m.QuestionId == id);
            
        }

        public void Add(QuestionDTO Question)
        {
            
            context.Questions.Add(mapper.Map<QuestionDTO, Question>(Question));
            context.SaveChanges();
            
        }

        public void Update(QuestionDTO question)
        {
            var auData = context.Questions.FirstOrDefault(item => item.QuestionId == question.QuestionId);
            if (auData == null)
                throw new Exception("Not found Question to update");

            auData.QuestionName = question.QuestionName;
            auData.Mark = question.Mark;
            auData.ExamId = question.ExamId;
            auData.PaperNo = question.PaperNo;

            context.SaveChanges();
            
        }

        public void Delete(int id)
        {
            
            var author = context.Questions.FirstOrDefault(item => item.QuestionId == id);
            if (author == null)
                throw new Exception("Not found Question");

            var testcase = context.TestCases.FirstOrDefault(item => item.QuestionId == id);
            if (testcase != null)
            {
                throw new Exception("U must delete testcase link this question first");
            }

            //context.Database.ExecuteSqlRaw("Delete from BookAuthor where book_id =" + id);
            context.Questions.Remove(author);
            context.SaveChanges();
            
        }
    }
}
