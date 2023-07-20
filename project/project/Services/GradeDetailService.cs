using project.DTO;
using project.Models;
using project.Repositories;

namespace project.Services
{
    public class GradeDetailService
    {
        private GradeDetailRepository repository;

        private GradeDetailService()
        {
            repository = new GradeDetailRepository();
        }

        private static readonly GradeDetailService _singleton = new GradeDetailService();
        public static GradeDetailService GetSingleton()
        {
            return _singleton;
        }
        public void AddGradeDetail(GradeDetailDTO dTO)
        {
            repository.AddGradeDetail(dTO);
        }

        public void UpdateGradeDetail(GradeDetailDTO dTO)
        {
            repository.UpdateGradeDetail(dTO);
        }

        public GradeDetail GetGradeDetail(int examresultId, int questionId, int? testCaseId)
        {
            return repository.GetGradeDetail(examresultId, questionId, testCaseId);
        }
    }    
}
