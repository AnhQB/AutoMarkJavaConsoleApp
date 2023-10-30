using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using project.DTO;
using project.Models;
using project.Services;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace project.Repositories
{
    public class ExamResultRepository
    {
        private IMapper mapper;
        private MapperConfiguration config;
        private readonly project_PRN231Context context;
        public ExamResultRepository()
        {
            config = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            mapper = config.CreateMapper();
            context = new project_PRN231Context();
        }

        public ExamResult AddExamResult(ExamResultDTO dTO)
        {
            
            ExamResult examResult = mapper.Map<ExamResultDTO, ExamResult>(dTO);
            context.ExamResults.Add(examResult);
            context.SaveChanges();
            return examResult;
            
        }

        public void UpdateExamResult(ExamResultDTO dTO)
        {
            
            var auData = context.ExamResults.FirstOrDefault(item => item.ExamresultId == dTO.ExamresultId);
            if (auData == null)
                throw new Exception("Not found Exam to update");

            auData.Mark = dTO.Mark;
            auData.StudentId = dTO.StudentId;
            auData.ExamId = dTO.ExamId;
            auData.PaperNo = dTO.PaperNo;

            context.SaveChanges();
            
        }

        public ExamResult GetExistExamResult(int studentId, int examId, int paperNo)
        {
            var auData = context.ExamResults.FirstOrDefault(item => 
                item.StudentId == studentId
                && item.ExamId == examId
                && item.PaperNo == paperNo);
            return auData;
            
        }

        public void Test(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                using (var archive = RarArchive.Open(stream))
                {
                    foreach (var entry in archive.Entries)
                    {
                        if (entry.IsDirectory)
                        {
                            // Xử lý thư mục
                            Console.WriteLine($"Folder name: {entry.Key}");
                            //test\123\1\nbproject\private
                            string[] folderNameSplit = entry.Key.Split('\\');
                            var folderName = folderNameSplit[0] + "\\" + folderNameSplit[1];
                            //get number question follow exam
                            for (int i = 1; i <= 4; i++)
                            {
                                var nameQ = "\\Q" + i;
                                var pathFolder = "\\run";
                                var a = folderName + nameQ + pathFolder + nameQ + ".jar";
                                // Duyệt vào các thư mục con của thư mục đó
                                RarArchiveEntry subentry = archive.Entries.Where(e => e.Key.StartsWith(folderName + nameQ + pathFolder + nameQ + ".jar")).FirstOrDefault();
                                if (subentry != null)
                                {
                                    using (var entryStream = subentry.OpenEntryStream())
                                    {
                                        Console.WriteLine("tim thay file jar" + folderName + nameQ + pathFolder + nameQ + ".jar");
                                        //đọc file .jar
                                        // Lưu nội dung của file .jar vào một file tạm thời
                                        var tempFilePath = Path.GetTempFileName();
                                        using (var tempFileStream = System.IO.File.OpenWrite(tempFilePath))
                                        {
                                            entryStream.CopyTo(tempFileStream);
                                        }

                                        // Chuẩn bị các tham số cho lệnh Java
                                        var classPath = tempFilePath;
                                        var input = "alibabaubaka";
                                        //TC1: alibabaubaka
                                        //TC2: the fOx brOwn kc

                                        // Tạo một quy trình mới để thực thi lệnh Java
                                        var process = new Process();
                                        process.StartInfo.FileName = "java";
                                        process.StartInfo.Arguments = $"-jar \"{tempFilePath}\"";
                                        process.StartInfo.UseShellExecute = false;
                                        process.StartInfo.RedirectStandardInput = true;
                                        process.StartInfo.RedirectStandardOutput = true;

                                        // Bắt đầu thực thi lệnh Java
                                        process.Start();

                                        // Ghi dữ liệu đầu vào vào quy trình
                                        process.StandardInput.WriteLine(input);
                                        process.StandardInput.Close();

                                        // Đọc dữ liệu đầu ra từ quy trình
                                        var output = process.StandardOutput.ReadToEnd();

                                        // Đợi cho quy trình kết thúc
                                        process.WaitForExit();

                                        // In kết quả ra màn hình để kiểm tra
                                        Console.WriteLine(output);
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Xử lý tập tin
                            Console.WriteLine($"File name: {entry.Key}");
                        }
                    }
                }
            }
        }

        public async Task<string> ExtractFileToLocalAsync(IFormFile file)
        {
            // Kiểm tra xem file có tồn tại và có dữ liệu không
            if (file == null || file.Length <= 0)
            {
                throw new Exception("File không hợp lệ");
            }
            // lấy tên file ko có đuôi
            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            // Đường dẫn tạm thời để lưu file zip
            var tempFilePath = Path.GetTempFileName() ;
            
            try
            {
                // Lưu file zip vào đường dẫn tạm thời
                using (var stream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Đường dẫn đích để giải nén tệp tin
                string extractPath = Path.Combine(Directory.GetCurrentDirectory(),
                    "Files",
                    $"{fileName}" +
                    $"_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}");

                // Giải nén tệp tin zip
                ZipFile.ExtractToDirectory(tempFilePath, extractPath);

                // Trả về link folder đã extract
                return extractPath + "\\" + fileName;
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ nếu có
                throw new Exception($"Lỗi giải nén tệp tin: {ex.Message}");
            }
            finally
            {
                // Xóa file zip tạm thời sau khi hoàn tất quá trình giải nén
                if (System.IO.File.Exists(tempFilePath))
                {
                    System.IO.File.Delete(tempFilePath);
                }
            }
        }

        public void AutomaticScoring(string filePath, List<TestCase> testCases)
        {
            foreach (var directory in Directory.GetDirectories(filePath))
            {
                // get student by studentId
                Console.WriteLine(Path.GetFileName(directory));
                
            }
        }

        public Dictionary<string, Dictionary<string, ScoreExamResultDTO>>
            AutomaticScoring(string filePath, List<Exam> exams)
        {
            // file folder "StudentSolution" which include folder Question
            string[] subDirectories = Directory.GetDirectories(filePath, "StudentSolution", SearchOption.AllDirectories);

            if (subDirectories.Length > 0)
            {
                string studentSolutionPath = subDirectories[0];
                Directory.SetCurrentDirectory(studentSolutionPath);

                Dictionary<string, Dictionary<string, ScoreExamResultDTO>> scoreExam
                    = new Dictionary<string, Dictionary<string, ScoreExamResultDTO>>();

                // loop all folder Question
                foreach (var directory in Directory.GetDirectories(Directory.GetCurrentDirectory()))
                {
                    // get question Name
                    var paperNo = Path.GetFileName(directory);

                    if (CheckPaperNoExistInExam(paperNo, exams))
                    {
                        // get question list 
                        List<Question> questions = exams.Where(e => e.PaperNo == Int32.Parse(paperNo))
                            .First().Questions.ToList();

                        BrowseAllStudentsInPaperNo(directory, scoreExam, questions, exams.First().ExamId, paperNo);
                    }
                    else
                    {
                        throw new Exception("Tại Path " + directory + " Không tìm thấy PaperNo "
                        + paperNo + " trong examId " + exams[0].ExamId);
                    }
                }

                SaveScoreExamResult(scoreExam);
                return scoreExam;
            }
            else
            {
                // Nếu không tìm thấy thư mục "StudentSolution"
                throw new Exception("Không tìm thấy thư mục 'StudentSolution'");
            }
        }

        public Dictionary<string, ScoreExamResultTestDTO>
            AutomaticScoringTest(string filePath, List<Exam> exams)
        {
            Directory.SetCurrentDirectory(filePath);
            Dictionary<string, ScoreExamResultTestDTO> scoreExam
                    = new Dictionary<string, ScoreExamResultTestDTO>();

            // loop all folder PaperNo
            foreach (var directory in Directory.GetDirectories(Directory.GetCurrentDirectory()))
            {
                // get PaperNo Name
                var paperNo = Path.GetFileName(directory);

                if (CheckPaperNoExistInExam(paperNo, exams))
                {
                    // get question list 
                    List<Question> questions = exams.Where(e => e.PaperNo == Int32.Parse(paperNo))
                        .First().Questions.ToList();

                    foreach (var ques in questions)
                    {
                        // set path to file jar
                        var nameQues = ques.QuestionName;
                        var pathQues = Path.Combine(directory, nameQues, "run", nameQues + ".jar");
                        //get list test cases
                        List<TestCase> testCases = TestCaseService.GetSingleton()
                            .GetTestCasesByQuestionId(ques.QuestionId);

                        CheckMarkQuestionTest(testCases, scoreExam, pathQues, exams[0].ExamId, paperNo, ques);

                    }
                }
                else
                {
                    throw new Exception("Tại Path " + directory + " Không tìm thấy PaperNo "
                    + paperNo + " trong examId " + exams[0].ExamId);
                }
            }

            return scoreExam;
        }


        private void SaveScoreExamResult(Dictionary<string, Dictionary<string, ScoreExamResultDTO>> scoreExam)
        {
            foreach(var paperNo in scoreExam.Keys)
            {
               foreach(var studentId in scoreExam[paperNo].Keys)
                {
                    ScoreExamResultDTO result = scoreExam[paperNo][studentId];
                    ExamResultDTO resultDTO = mapper.Map<ScoreExamResultDTO, ExamResultDTO>(result);

                    //ExamResult examResult = GetExistExamResult(result.StudentId, result.ExamId, result.PaperNo);
                    ExamResult temp = new ExamResult();
                   /* if (examResult != null)
                    {*/
                        // update

                        UpdateExamResult(resultDTO);
                    /*}
                    else
                    {
                        // add
                        temp = AddExamResult(resultDTO);
                        foreach (var gradeDetail in result.GradeDetails)
                        {
                            gradeDetail.ExamresultId = temp.ExamresultId;
                        }
                    }*/


                    foreach(var gradeDetail in result.GradeDetails)
                    {
                        GradeDetail gradeDetailTemp = GradeDetailService.GetSingleton()
                                .GetGradeDetail(gradeDetail.ExamresultId, gradeDetail.QuestionId, gradeDetail.TestcaseId);
                        GradeDetailDTO gradeDetailDTO = mapper.Map<GradeDetail, GradeDetailDTO>(gradeDetail);
                        if (gradeDetailTemp != null)
                        {

                            GradeDetailService.GetSingleton().UpdateGradeDetail(gradeDetailDTO);
                        }
                        else
                        {
                            GradeDetailService.GetSingleton().AddGradeDetail(gradeDetailDTO);
                        }
                    }

                }
            }
        }

        private void BrowseAllStudentsInPaperNo(string paperNoDirectory,
            Dictionary<string, Dictionary<string, ScoreExamResultDTO>> scoreExam,
            List<Question> questions, int examId, string paperNo)
        {
            foreach (var studentFolder in Directory.GetDirectories(paperNoDirectory))
            {
                // get student name folder
                var studentFolderName = Path.GetFileName(studentFolder);

                if (!scoreExam.ContainsKey(paperNo))
                {
                    scoreExam.Add(paperNo, new Dictionary<string, ScoreExamResultDTO>
                        {
                            { studentFolderName, null }
                        }
                    );
                }
                else
                {
                    if(!scoreExam[paperNo].ContainsKey(studentFolderName))
                    {
                        scoreExam[paperNo].Add(studentFolderName, null);
                    }
                }
                
                foreach (var ques in questions)
                {
                    // set path to file jar
                    var nameQues = ques.QuestionName;
                    var pathQues = Path.Combine(studentFolder, nameQues, "run", nameQues + ".jar");
                    //get list test cases
                    List<TestCase> testCases = TestCaseService.GetSingleton()
                        .GetTestCasesByQuestionId(ques.QuestionId);
                    
                    CheckMarkQuestion(testCases, scoreExam, pathQues, examId, paperNo, studentFolderName, ques);

                }
            }
        }

        private bool CheckPaperNoExistInExam(string paperNo, List<Exam> exams)
        {
            int paperNoInt;
            try
            {
                paperNoInt = Int32.Parse(paperNo);
            }
            catch (FormatException)
            {
                throw new Exception($"Wrong name folder format '{paperNo}'");
            }
            foreach (var each in exams)
            {
                
                if (each.PaperNo == paperNoInt)
                {
                    return true;
                }
            }
            return false;
        }

        public void CheckMarkQuestion(List<TestCase> testCases,
            Dictionary<string, Dictionary<string, ScoreExamResultDTO>> scoreExam,
            string jarFilePath, int examId, string paperNo, string studentId, Question question)
        {
            //string jarFilePath = Path.Combine(directory, "run", "example.jar"); // đường dẫn tới file .jar
            if (File.Exists(jarFilePath)) // kiểm tra file .jar có tồn tại không
            {
                foreach(var testcase in testCases)
                {
                    // Chuẩn bị các tham số cho lệnh Java
                     string[] input = testcase.Input.Split("\\n"); 

                    //TC1: alibabaubaka
                    //TC2: the fOx brOwn kc

                    // Tạo một quy trình mới để thực thi lệnh Java
                    var process = new Process();
                    process.StartInfo.FileName = "java";
                    process.StartInfo.Arguments = $"-jar \"{jarFilePath}\"";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardInput = true;
                    process.StartInfo.RedirectStandardOutput = true;

                    // Bắt đầu thực thi lệnh Java
                    process.Start();

                    // Ghi dữ liệu đầu vào vào quy trình
                    if(input.Length > 1)
                    {
                        foreach(var i in input)
                        {
                            process.StandardInput.WriteLine(i);
                        }
                    }
                    else
                    {
                        process.StandardInput.WriteLine(input[0]);
                    }
                    
                    process.StandardInput.Close();

                    // Đọc dữ liệu đầu ra từ quy trình
                    var output = process.StandardOutput.ReadToEnd();

                    // Đợi cho quy trình kết thúc
                    process.WaitForExit();

                    // In kết quả ra màn hình để kiểm tra
                    //Console.WriteLine(output);
                    CheckOutputTestCase(scoreExam, output, testcase, examId, paperNo, studentId, question);

                    
                }
            }
            else
            {
                throw new Exception("không tồn tại file jar tại run/.jar");
            }
        }


        private void CheckMarkQuestionTest(List<TestCase> testCases,
            Dictionary<string, ScoreExamResultTestDTO> scoreExam, 
            string pathQues, int examId, string paperNo, Question question)
        {
            if (File.Exists(pathQues)) // kiểm tra file .jar có tồn tại không
            {
                foreach (var testcase in testCases)
                {
                    // Chuẩn bị các tham số cho lệnh Java
                    string[] input = testcase.Input.Split("\\n");

                    //TC1: alibabaubaka
                    //TC2: the fOx brOwn kc

                    // Tạo một quy trình mới để thực thi lệnh Java
                    var process = new Process();
                    process.StartInfo.FileName = "java";
                    process.StartInfo.Arguments = $"-jar \"{pathQues}\"";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardInput = true;
                    process.StartInfo.RedirectStandardOutput = true;

                    // Bắt đầu thực thi lệnh Java
                    process.Start();

                    // Ghi dữ liệu đầu vào vào quy trình
                    if (input.Length > 1)
                    {
                        foreach (var i in input)
                        {
                            process.StandardInput.WriteLine(i);
                        }
                    }
                    else
                    {
                        process.StandardInput.WriteLine(input[0]);
                    }

                    process.StandardInput.Close();

                    // Đọc dữ liệu đầu ra từ quy trình
                    var output = process.StandardOutput.ReadToEnd();

                    // Đợi cho quy trình kết thúc
                    process.WaitForExit();

                    // In kết quả ra màn hình để kiểm tra
                    //Console.WriteLine(output);
                    CheckOutputTestCaseTest(scoreExam, output, testcase, examId, paperNo, question);


                }
            }
            else
            {
                throw new Exception("không tồn tại file jar tại run/.jar");
            }
        }

        private void CheckOutputTestCaseTest(
            Dictionary<string, ScoreExamResultTestDTO> scoreExam, 
            string output, TestCase testcase, int examId, string paperNo, Question question)
        {
            // check output vs output testcase
            output = output.Split("OUTPUT:").Last().Replace("\r", "").TrimStart('\n').TrimEnd('\n');
            int paperNoInt = Int32.Parse(paperNo);
            var outputTestCase = testcase.Output.Replace("\\n", Environment.NewLine)
                .Replace("\\t", "\t").Replace("\r", "");
            // check output
            bool testResult = output.Equals(outputTestCase);
            // check exist grade of student or not 


            //ScoreExamResultTestDTO result = ;

            if (!scoreExam.ContainsKey(paperNo))
            {
               
                ScoreExamResultTestDTO resultDTO = new ScoreExamResultTestDTO
                {
                    ExamId = examId,
                    PaperNo = paperNoInt,
                    Mark = testResult ? testcase.Mark : 0
                };



                GradeDetailTestDTO detail = new GradeDetailTestDTO
                {
                    QuestionId = question.QuestionId,
                    Output = output,
                    Testresult = testResult,
                    TestcaseId = testcase.TestcaseId,
                    Question = new Question
                    {
                        QuestionName = question.QuestionName
                    },
                    Testcase = new TestCase
                    {
                        Mark = testcase.Mark
                    }
                };

                List<GradeDetailTestDTO> gradeDetails = new List<GradeDetailTestDTO>
                {
                    detail
                };

                resultDTO.GradeDetails = gradeDetails;

                scoreExam[paperNo] = resultDTO;
            }
            else
            {
                //ExamResult examResult = GetExistExamResult(studentIdInt, examId, paperNoInt);
                ScoreExamResultTestDTO result = scoreExam[paperNo];
                GradeDetailTestDTO detail = new GradeDetailTestDTO
                {
                    QuestionId = question.QuestionId,
                    Output = output,
                    Testresult = testResult,
                    TestcaseId = testcase.TestcaseId,
                    Question = new Question
                    {
                        QuestionName = question.QuestionName
                    },
                    Testcase = new TestCase
                    {
                        Mark = testcase.Mark
                    }
                };
                if (testResult)
                    result.Mark += testcase.Mark;
                result.GradeDetails.Add(detail);
            }
        }

        private void CheckOutputTestCase(Dictionary<string, Dictionary<string, ScoreExamResultDTO>> scoreExam,
            string output, TestCase testcase
            , int examId, string paperNo, string studentId, Question question)
        {
            // check output vs output testcase
            output = output.Split("OUTPUT:").Last().Replace("\r", "").TrimStart('\n').TrimEnd('\n');
            int studentIdInt = Int32.Parse(studentId);
            int paperNoInt = Int32.Parse(paperNo);
            var outputTestCase = testcase.Output.Replace("\\n", Environment.NewLine)
                .Replace("\\t", "\t").Replace("\r", "");
            // check output
            bool testResult = output.Equals(outputTestCase);
            // check exist grade of student or not 


            ScoreExamResultDTO result = scoreExam[paperNo][studentId];

            if (result == null)
            {
                ExamResult examResultExist = GetExistExamResult(studentIdInt, examId, paperNoInt);
                ExamResult examAdd = new ExamResult();
                if (examResultExist == null)
                {
                    ExamResultDTO resultDTO = new ExamResultDTO
                    {
                        StudentId = studentIdInt,
                        ExamId = examId,
                        PaperNo = paperNoInt,
                        Mark = testResult ? testcase.Mark : 0
                    };
                    examAdd = AddExamResult(resultDTO);
                }
                

                GradeDetail detail = new GradeDetail
                {
                    ExamresultId = examResultExist == null ? examAdd.ExamresultId : examResultExist.ExamresultId,
                    QuestionId = question.QuestionId,
                    Output = output,
                    Testresult = testResult,
                    TestcaseId = testcase.TestcaseId,
                    Testcase = new TestCase
                    {
                        Mark = testcase.Mark
                    },
                    Question = new Question
                    {
                        QuestionName = question.QuestionName
                    }
                };

                List<GradeDetail> gradeDetails = new List<GradeDetail>
                {
                    detail
                };
                scoreExam[paperNo][studentId] = new ScoreExamResultDTO
                {
                    ExamresultId = examResultExist == null ? examAdd.ExamresultId : examResultExist.ExamresultId,
                    StudentId = studentIdInt,
                    ExamId = examId,
                    PaperNo = paperNoInt,
                    Mark = testResult ? testcase.Mark : 0,
                    GradeDetails = gradeDetails
                };
            }
            else
            {
                //ExamResult examResult = GetExistExamResult(studentIdInt, examId, paperNoInt);

                GradeDetail detail = new GradeDetail
                {
                    ExamresultId = result.ExamresultId,
                    QuestionId = question.QuestionId,
                    Output = output,
                    Testresult = testResult,
                    TestcaseId = testcase.TestcaseId,
                    Testcase = new TestCase
                    {
                        Mark = testcase.Mark
                    },
                    Question = new Question
                    {
                        QuestionName = question.QuestionName
                    }
                };
                if (testResult)
                    result.Mark  += testcase.Mark;
                result.GradeDetails.Add(detail);
            }

/*
            if (examResult != null)
            {
                // update
                ExamResultDTO resultDTO = new ExamResultDTO
                {
                    ExamresultId = examResult.ExamresultId,
                    StudentId = studentIdInt,
                    ExamId = examId,
                    PaperNo = paperNoInt,
                    Mark = testResult ? examResult.Mark + testcase.Mark : examResult.Mark
                };
                UpdateExamResult(resultDTO);

                GradeDetail gradeDetail = GradeDetailService.GetSingleton()
                    .GetGradeDetail(examResult.ExamresultId, questionId, testcase.TestcaseId);
                if(gradeDetail != null)
                {
                    // update
                    GradeDetailDTO detailDTO = new GradeDetailDTO
                    {
                        ExamresultId = examResult.ExamresultId,
                        QuestionId = questionId,
                        Output = output,
                        Testresult = testResult,
                        TestcaseId = testcase.TestcaseId
                    };
                    GradeDetailService.GetSingleton().UpdateGradeDetail(detailDTO);
                }
                else
                {
                    // add
                    GradeDetailDTO detailDTO = new GradeDetailDTO
                    {
                        ExamresultId = examResult.ExamresultId,
                        QuestionId = questionId,
                        Output = output,
                        Testresult = testResult,
                        TestcaseId = testcase.TestcaseId
                    };
                    GradeDetailService.GetSingleton().AddGradeDetail(detailDTO);
                }
            }
            else
            {
                // addd
                ExamResultDTO resultDTO = new ExamResultDTO
                {
                    StudentId = studentIdInt,
                    ExamId = examId,
                    PaperNo = paperNoInt,
                    Mark = testResult ? testcase.Mark : 0
                };
                ExamResult exam = AddExamResult(resultDTO);

                GradeDetailDTO detailDTO = new GradeDetailDTO
                {
                    ExamresultId = exam.ExamresultId,
                    QuestionId = questionId,
                    Output = output,
                    Testresult = testResult,
                    TestcaseId = testcase.TestcaseId
                };
                GradeDetailService.GetSingleton().AddGradeDetail(detailDTO);
            }*/

            
        }

        public Dictionary<string, ExamResultStudent> GetExamResults()
        {
            List<ExamResult> list = new List<ExamResult>();
            var query = from gd in context.GradeDetails
                        join er in context.ExamResults
                        on gd.ExamresultId equals er.ExamresultId
                        join q in context.Questions
                        on gd.QuestionId equals q.QuestionId
                        join t in context.TestCases
                        on gd.TestcaseId equals t.TestcaseId 
                        orderby er.ExamresultId
                        select new 
                        {
                            ExamResultId = er.ExamresultId,
                            StudentId = er.StudentId,
                            ExamId = er.ExamId,
                            PaperNo = er.PaperNo,
                            Mark = er.Mark,
                            QuestionName = q.QuestionName,
                            QuestionId = q.QuestionId,
                            TestCaseId = gd.TestcaseId,
                            TestResult = gd.Testresult,
                            MarkTestCase = t.Mark
                        }
                        ;
            Dictionary<string, ExamResultStudent> result = new Dictionary<string, ExamResultStudent>();
            List<GradeNote> listGradeNote = new List<GradeNote>();
            foreach(var item in query)
            {
                if (!result.ContainsKey(item.ExamResultId.ToString()))
                {
                    listGradeNote.Clear();
                    ExamResultStudent temp = new ExamResultStudent
                    {
                        StudentId = item.StudentId,
                        ExamId = item.ExamId,
                        PaperNo = item.PaperNo,
                        Mark  = item.Mark
                    };

                    listGradeNote.Add(new GradeNote
                    {
                        QuestionId = item.QuestionId,
                        QuestionName = item.QuestionName,
                        Mark = (bool)item.TestResult ? item.MarkTestCase : 0
                    });

                    result.Add(item.ExamResultId.ToString(), temp);
                }
                else
                {
                    var index = ExistQuestionInGradeNote(listGradeNote, item.QuestionId);
                    if (index == -1)
                    {
                        listGradeNote.Add(new GradeNote
                        {
                            QuestionId = item.QuestionId,
                            QuestionName = item.QuestionName,
                            Mark = (bool)item.TestResult ? item.MarkTestCase : 0
                        });
                    }
                    else
                    {
                        listGradeNote[index].Mark = (bool)item.TestResult ? 
                            listGradeNote[index].Mark + item.MarkTestCase : listGradeNote[index].Mark;
                    }
                    result[item.ExamResultId.ToString()].GradeNote = String.Join("; ", listGradeNote.Select(x => x.ToString()));
                }
                

            }
            //List<ExamResult1DTO> result = mapper.Map<List<ExamResult>, List<DTO.ExamResultDTO1.ExamResult1DTO>>(list);

            return result;
        }

        public int ExistQuestionInGradeNote(List<GradeNote> list, int quesId)
        {
            if(list.Count > 0) 
            {
                foreach (var item in list)
                {
                    if (item.QuestionId == quesId)
                    {
                        return list.IndexOf(item);
                    }
                }
                return -1;
            }
            else
            {
                return -1;
            }
            
        }

        public List<GradeDetailStudent> GetGradeDetail(int examresultId)
        {
            var query = from gd in context.GradeDetails
                        join er in context.ExamResults
                        on gd.ExamresultId equals er.ExamresultId
                        join q in context.Questions
                        on gd.QuestionId equals q.QuestionId
                        join t in context.TestCases
                        on gd.TestcaseId equals t.TestcaseId
                        where er.ExamresultId == examresultId
                        orderby q.QuestionName
                        select new
                        {
                            q.QuestionName,
                            q.QuestionId,
                            gd.TestcaseId,
                            t.Input,
                            t.Output,
                            MarkTestCase = t.Mark,
                            OutPutStudent= gd.Output,
                            gd.Testresult,
                            QuestionMark = q.Mark
                        };
                       
            List<GradeDetailStudent> gradeDetailStudent = new List<GradeDetailStudent>();
            foreach(var item in query)
            {
                var index = ExistQuestionInGradeDetailStudent(gradeDetailStudent, item.QuestionId);
                if(index == -1)
                {
                    gradeDetailStudent.Add(new GradeDetailStudent
                    {
                        QuestionId = item.QuestionId,
                        QuestionName = item.QuestionName,
                        QuestionMark= item.QuestionMark,
                        QuestionMarkStudent = (bool)item.Testresult ? item.MarkTestCase : 0,
                        TestCases = new List<TestCaseDetailStudent>
                        {
                            new TestCaseDetailStudent
                            {
                                TestCaseId = item.TestcaseId,
                                Input = item.Input,
                                Output = item.Output,
                                MarkTestCase = item.MarkTestCase,
                                OutPutStudent = item.OutPutStudent,
                                TestCaseResult = item.Testresult
                            }
                        }
                    });
                }
                else
                {
                    gradeDetailStudent[index].QuestionMarkStudent += (bool)item.Testresult 
                        ? item.MarkTestCase : gradeDetailStudent[index].QuestionMarkStudent;
                    gradeDetailStudent[index].TestCases.Add(new TestCaseDetailStudent
                    {
                        TestCaseId = item.TestcaseId,
                        Input = item.Input,
                        Output = item.Output,
                        MarkTestCase = item.MarkTestCase,
                        OutPutStudent = item.OutPutStudent,
                        TestCaseResult = item.Testresult
                    });
                }
            }
            return gradeDetailStudent;
        }

        public int ExistQuestionInGradeDetailStudent(List<GradeDetailStudent> list, int questionId)
        {
            foreach (var item in list)
            {
                if (item.QuestionId == questionId)
                {
                    return list.IndexOf(item);
                }
            }
            return -1;
        }
    }
}
