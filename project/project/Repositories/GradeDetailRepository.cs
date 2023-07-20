using AutoMapper;
using project.DTO;
using project.Models;

namespace project.Repositories
{
    public class GradeDetailRepository
    {
        private IMapper mapper;
        private MapperConfiguration config;
        private readonly project_PRN231Context context;
        public GradeDetailRepository()
        {
            config = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            mapper = config.CreateMapper();
            context = new project_PRN231Context();
        }
        public void AddGradeDetail(GradeDetailDTO dTO)
        {
            
            context.GradeDetails.Add(mapper.Map<GradeDetailDTO, GradeDetail>(dTO));
            context.SaveChanges();
            
        }

        public void UpdateGradeDetail(GradeDetailDTO dTO)
        {
            
            var auData = context.GradeDetails.FirstOrDefault(item => item.ExamresultId == dTO.ExamresultId 
                && item.QuestionId ==  dTO.QuestionId && item.TestcaseId == dTO.TestcaseId);
            if (auData == null)
                throw new Exception("Not found Exam to update");

            auData.Output = dTO.Output;
            auData.Testresult = dTO.Testresult;

            context.SaveChanges();
            
        }

        public GradeDetail GetGradeDetail(int examResultId, int questionId, int? testCaseId)
        {
            
            var auData = context.GradeDetails.FirstOrDefault(item =>
                item.ExamresultId == examResultId
                && item.QuestionId == questionId
                && item.TestcaseId == testCaseId
                );
            return auData;
            
        }
    }
}
