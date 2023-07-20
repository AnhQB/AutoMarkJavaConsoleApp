using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using project.DTO;
using project.Models;
using project.Repositories;
using project.Services.IServices;
using System.IO;

namespace project.Services
{
    public class ExamResultService 
    {
        private ExamResultRepository repository;
        private ExamRepository examRepository;

        private ExamResultService()
        {
            repository = new ExamResultRepository();
            examRepository = new ExamRepository();
        }

        private static readonly ExamResultService _singleton = new ExamResultService();
        public static ExamResultService GetSingleton()
        {
            return _singleton;
        }

        /*
        public async void AutoScoring(IFormFile file)
        {
            string filePath = await repository.ExtractFileToLocal(file);
            
            List<TestCase> testCases = new List<TestCase>();
            repository.AutomaticScoring(filePath, testCases);
        }

        public async Task<Dictionary<string, Dictionary<string, ScoreExamResultDTO>>>
            AutoScoring(IFormFile file, int examId)
        {
            string filePath = await repository.ExtractFileToLocal(file);
            string name = Path.GetFileName(filePath);

            //get list exam by examId
            List<Exam> exams = ExamService.GetSingleton().GetExam(examId);
            return null;
            return repository.AutomaticScoring(filePath, exams);
        }
        */
        public async Task AutoScoringAsync(IFormFile file)
        {
            string filePath = await repository.ExtractFileToLocalAsync(file);

            List<TestCase> testCases = new List<TestCase>();
            repository.AutomaticScoring(filePath, testCases);
        }

        public async Task<Dictionary<string, Dictionary<string, ScoreExamResultDTO>>> AutoScoringAsync(IFormFile file, int examId)
        {
            string filePath = await repository.ExtractFileToLocalAsync(file);
            string name = Path.GetFileName(filePath);
            
            //get list exam by examId
            List<Exam> exams = examRepository.GetExam(examId);
            
            if (!name.Equals(exams[0].ExamName)) 
                throw new Exception("Tên folder không giống với tên Exam đã chọn");
            return repository.AutomaticScoring(filePath, exams);
        }

        public async Task<Dictionary<string, ScoreExamResultTestDTO>> AutoScoringAsyncTest(IFormFile file, int examId)
        {
            string filePath = await repository.ExtractFileToLocalAsync(file);
            string name = Path.GetFileName(filePath);

            //get list exam by examId
            List<Exam> exams = examRepository.GetExam(examId);

            return repository.AutomaticScoringTest(filePath, exams);
        }

        public Dictionary<string, ExamResultStudent> GetExamResults()
        {
            return repository.GetExamResults();
        }

        public List<GradeDetailStudent> GetGradeDetail(int examresultId)
        {
            return repository.GetGradeDetail(examresultId);
        }
    }
}
