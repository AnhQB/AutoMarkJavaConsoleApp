using AutoMapper;
using Microsoft.EntityFrameworkCore;
using project.DTO;
using project.Models;
using project.Repositories.IRepositories;
using SharpCompress;
using System.Collections.Generic;

namespace project.Repositories
{
    public class ExamRepository
    {
        private IMapper mapper;
        private MapperConfiguration config;
        private readonly project_PRN231Context context;
        public ExamRepository()
        {
            config = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            mapper = config.CreateMapper();
            context = new project_PRN231Context();
        }
        public List<Exam> GetExamsDistinct()
        {
            var a = context.Exams.ToList();
            List<Exam> distinctExamId = new List<Exam>();
            foreach(var i in a)
            {
                if(distinctExamId.Count > 0)
                {
                    bool isExist = false;
                    foreach (var j in distinctExamId)
                    {
                        if(j.ExamId == i.ExamId)
                        {
                            isExist = true;
                        }
                    }

                    if (!isExist)
                    {
                        distinctExamId.Add(i);
                    }
                }
                else
                {
                    distinctExamId.Add(i);
                }
            }
            return distinctExamId;
        }
        public Dictionary<string, List<ExamDTO>> GetExams()
        {
            var a = context.Exams.ToList();
            Dictionary<string, List<ExamDTO>> list = new Dictionary<string, List<ExamDTO>>();
            foreach(var i in a)
            {
                var b =  mapper.Map<Exam, ExamDTO>(i); 
                if (!list.ContainsKey(i.ExamId.ToString()))
                {
                    list[i.ExamId.ToString()] = new List<ExamDTO>
                    {
                        b
                    };
                }
                else
                {
                    list[i.ExamId.ToString()].Add(b);
                }
            }
            return list;
        }

        public List<ExamDTO> GetExamsNo()
        {
            var a = context.Exams.ToList();
            return mapper.Map<List<ExamDTO>>(a);
        }


        public IQueryable<Exam> GetAllExams() 
        {
            
             return context.Exams.AsQueryable();
            
        }


        public List<Exam> GetExam(int id)
        {
            
            return context.Exams.Where(e => e.ExamId == id)
                .Include(e => e.Questions)
                .ToList();
            
        }

        public ExamDTO GetExam(int id, int paperNo)
        {
            var a = context.Exams.Where(e => e.ExamId == id && e.PaperNo == paperNo).FirstOrDefault();
            if (a != null)
            {
                return mapper.Map<Exam, ExamDTO>(a);
            }
            else
            {
                return null;
            }
        }

        public void Add(ExamDTO exam)
        {
            List<Exam> b = GetExamsDistinct();
            Exam temp = mapper.Map<ExamDTO, Exam>(exam);
            temp.ExamId = b.Count + 1;
            if (exam.PaperNo > 1)
            {
                for(int i = 1; i <= exam.PaperNo; i++){
                    temp.PaperNo = i;
                    context.Exams.Add(temp);
                    context.SaveChanges();
                }
            }
            else
            {
                context.Exams.Add(temp);
                context.SaveChanges();
            }
        }

        public void Update(ExamDTO exam)
        {
            
            /*var auData = context.Exams.FirstOrDefault(item => item.ExamId == exam.ExamId 
                && item.PaperNo == exam.PaperNo);
            if (auData == null)
                throw new Exception("Not found Exam to update");*/
            var auData1 = context.Exams.FirstOrDefault(item => item.ExamId == exam.ExamId 
                && item.PaperNo == exam.PaperNo);
            if(auData1 == null)
            {
                throw new Exception("Not found Exam to update");
            }

            auData1.ExamName = exam.ExamName;

            context.SaveChanges();
            
        }

        public void Delete(int id, int paperNo)
        {
            
            var author = context.Exams.FirstOrDefault(item => item.ExamId == id && item.PaperNo == paperNo);
            if (author == null)
                throw new Exception("Not found Exam");

            var question = context.Questions.FirstOrDefault(item => item.ExamId == id);
            if (question != null)
            {
                throw new Exception("U must delete question first");
            }

            //context.Database.ExecuteSqlRaw("Delete from BookAuthor where book_id =" + id);
            context.Exams.Remove(author);
            context.SaveChanges();
            
        }
    }
}
