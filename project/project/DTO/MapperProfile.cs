using AutoMapper;
using project.Models;

namespace project.DTO
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<Exam, ExamDTO>();
            CreateMap<ExamDTO, Exam>();
            CreateMap<GradeDetail, GradeDetailDTO>();
            CreateMap<GradeDetailDTO, GradeDetail>();
            CreateMap<ExamResult, ExamResultDTO>();
            CreateMap<ExamResultDTO, ExamResult>();
            CreateMap<ScoreExamResultDTO, ExamResult>();
            CreateMap<ExamResult, ScoreExamResultDTO>();
            CreateMap<ScoreExamResultDTO, ExamResultDTO>();
            CreateMap<ExamResultDTO, ScoreExamResultDTO>();
            CreateMap<Question, QuestionDTO>();
            CreateMap<QuestionDTO, Question>();
            CreateMap<TestCase, TestCaseDTO>();
            CreateMap<TestCaseDTO, TestCase>();

        }
    }
}
