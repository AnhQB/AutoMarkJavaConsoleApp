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

        public Dictionary<string, Dictionary<string, ExamResultStudent>> ReformatGrade(Dictionary<string, Dictionary<string, ScoreExamResultDTO>> result)
        {
            Dictionary<string, Dictionary<string, ExamResultStudent>> listGrade =
                new Dictionary<string, Dictionary<string, ExamResultStudent>>();
            
            foreach(var paperNo in result.Keys)
            {
                listGrade.Add(paperNo, new Dictionary<string, ExamResultStudent>());
                foreach(var studentId in result[paperNo].Keys)
                {
                    ScoreExamResultDTO temp = result[paperNo][studentId];
                    List<GradeNote> listGradeNote = new List<GradeNote>();
                    foreach(var gd in temp.GradeDetails)
                    {
                        int index = repository.ExistQuestionInGradeNote(listGradeNote, gd.QuestionId);
                        if (index == -1)
                        {
                            listGradeNote.Add(new GradeNote
                            {
                                QuestionId = gd.QuestionId,
                                QuestionName = gd.Question.QuestionName,
                                Mark = (bool)gd.Testresult ? gd.Testcase.Mark : 0
                            });
                        }
                        else
                        {
                            listGradeNote[index].Mark = (bool)gd.Testresult
                                ? listGradeNote[index].Mark + gd.Testcase.Mark : listGradeNote[index].Mark;
                        }
                    }


                    listGrade[paperNo].Add(studentId, new ExamResultStudent
                    {
                        StudentId = temp.StudentId,
                        ExamId = temp.ExamId,
                        PaperNo = temp.PaperNo,
                        Mark =  temp.Mark,
                        GradeNote = String.Join("; ", listGradeNote.Select(x => x.ToString()))
                    });
                }
            }

            return listGrade;
        }

        public Dictionary<string, ExamResultStudent> ReformatGradeTest(Dictionary<string, ScoreExamResultTestDTO> resultTest)
        {
            Dictionary<string, ExamResultStudent> listGrade =
                new Dictionary<string, ExamResultStudent>();

            foreach(var paperNo in resultTest.Keys)
            {
                listGrade.Add(paperNo, new ExamResultStudent());
                ScoreExamResultTestDTO temp = resultTest[paperNo];
                List<GradeNote> listGradeNote = new List<GradeNote>();
                foreach (var gd in temp.GradeDetails)
                {
                    int index = repository.ExistQuestionInGradeNote(listGradeNote, gd.QuestionId);
                    if (index == -1)
                    {
                        listGradeNote.Add(new GradeNote
                        {
                            QuestionId = gd.QuestionId,
                            QuestionName = gd.Question.QuestionName,
                            Mark = (bool)gd.Testresult ? gd.Testcase.Mark : 0
                        });
                    }
                    else
                    {
                        listGradeNote[index].Mark = (bool)gd.Testresult
                            ? listGradeNote[index].Mark + gd.Testcase.Mark : listGradeNote[index].Mark;
                    }
                }

                listGrade[paperNo] = new ExamResultStudent
                {
                    ExamId = temp.ExamId,
                    PaperNo = temp.PaperNo,
                    Mark = temp.Mark,
                    GradeNote = String.Join("; ", listGradeNote.Select(x => x.ToString()))
                };
            }
            return listGrade;
        }
        
    }
}
