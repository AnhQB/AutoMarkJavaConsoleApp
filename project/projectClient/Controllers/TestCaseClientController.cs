using Microsoft.AspNetCore.Mvc;
using Microsoft.OData.Edm;
using Newtonsoft.Json;
using project.DTO;
using project.Models;
using projectClient.DTO;
using projectClient.Models;

namespace projectClient.Controllers
{
    public class TestCaseClientController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            List<DTO.ExamDTO> list = new List<DTO.ExamDTO>();
            string link = "http://localhost:5000/api/Exam";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(link))
                {
                    using (HttpContent content = res.Content)
                    {
                        string result = await content.ReadAsStringAsync();

                        list = JsonConvert.DeserializeObject<List<DTO.ExamDTO>>(result);
                        ViewData["exams"] = list;
                    }
                }
            }

            List<project.Models.TestCase> list1 = new List<project.Models.TestCase>();
            string link1 = "http://localhost:5000/api/TestCase";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(link1))
                {
                    using (HttpContent content = res.Content)
                    {
                        string result = await content.ReadAsStringAsync();

                        list1 = JsonConvert.DeserializeObject<List<project.Models.TestCase>>(result);
                        ViewData["testcases"] = list1;
                    }
                }
            }

            return View();
        }

        public async Task<IActionResult> AddAsync(string? param)
        {
            var selectedexam = "";
            int selectedQues = -1;
            int firstExamId = 0;
            int firstPaperNo = 0;
            int questionId = 0;
            if (param != null)
            {
                //4 _ 1&{quesIdParam}
                string[] temp = param.Split('&');
                selectedexam = temp[0];
                string[] examParam = temp[0].Split('_');
                firstExamId = Int16.Parse(examParam[0].Trim());
                firstPaperNo = Int16.Parse(examParam[1].Trim());
                if (temp[1].Trim() != "") 
                {
                    questionId = Int16.Parse(temp[1].Trim());
                    selectedQues = questionId;
                }
            }
            
            {
                List<DTO.ExamDTO> data = new List<DTO.ExamDTO>();
                string link = "http://localhost:5000/api/Exam/AllNo";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(link))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string result = await content.ReadAsStringAsync();

                            data = JsonConvert.DeserializeObject<List<DTO.ExamDTO>>(result);
                            if (selectedexam == "")
                            {
                                firstExamId = data[0].ExamId;
                                firstPaperNo = data[0].PaperNo;
                            }
                            ViewData["exams"] = data;
                        }
                    }
                }
            }
            
            {
                List<project.Models.Question> data = new List<project.Models.Question>();
                string link = "http://localhost:5000/api/Question/";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(link + firstExamId + "/" + firstPaperNo))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string result = await content.ReadAsStringAsync();

                            data = JsonConvert.DeserializeObject<List<project.Models.Question>>(result);
                            if (data.Count > 0)
                            {
                                if(selectedQues == -1)
                                {
                                    questionId = data[0].QuestionId;
                                    selectedQues = questionId;
                                }
                                ViewData["questions"] = data;
                            }
                            else
                            {
                                ViewData["questions"] = null;
                            }
                            
                        }
                    }
                }
            }
            if(questionId != 0)
            {
                {
                    List<project.Models.TestCase> data = new List<project.Models.TestCase>();
                    string link = "http://localhost:5000/api/TestCase/";
                    using (HttpClient client = new HttpClient())
                    {
                        using (HttpResponseMessage res = await client.GetAsync(link + questionId))
                        {
                            using (HttpContent content = res.Content)
                            {
                                string result = await content.ReadAsStringAsync();

                                data = JsonConvert.DeserializeObject<List<project.Models.TestCase>>(result);

                                ViewData["testcases"] = data;
                            }
                        }
                    }
                }
            }
            else
            {
                ViewData["testcases"] = null;
            }

            ViewData["selectedExam"] = selectedexam;
            ViewData["selectedQues"] = selectedQues;
            
            return View();
        }

        public async Task<ActionResult> PostAddAsync(string exam, int quesId, string input, string output, int mark)
        {
            string link = "http://localhost:5000/api/TestCase";
            TestCaseDTO test = new TestCaseDTO
            {
                Input = input,
                Output = output,
                Mark = mark,
                QuestionId = quesId
            };
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PostAsJsonAsync(link, test))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Added");
                        TempData["Message"] = "added";
                    }
                    else
                    {
                        Console.WriteLine("Fail");
                        TempData["Message"] = "Fail";
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}
