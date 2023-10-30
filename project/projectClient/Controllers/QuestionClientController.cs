using Microsoft.AspNetCore.Mvc;
using Microsoft.OData.Edm;
using Newtonsoft.Json;
using project.Models;
using projectClient.DTO;

namespace projectClient.Controllers
{
    public class QuestionClientController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            Dictionary<string, Dictionary<string, List<Question>>> data =
                new Dictionary<string, Dictionary<string, List<Question>>>();
            string link = "http://localhost:5000/api/Question";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(link))
                {
                    using (HttpContent content = res.Content)
                    {
                        string result = await content.ReadAsStringAsync();

                        data = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, List<Question>>>>(result);
                        ViewData["exams"] = data;
                    }
                }
            }
            return View();
        }

        public async Task<IActionResult> AddAsync()
        {
            List<ExamDTO> data = new List<ExamDTO>();
            string link = "http://localhost:5000/api/Exam/AllNo";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(link))
                {
                    using (HttpContent content = res.Content)
                    {
                        string result = await content.ReadAsStringAsync();

                        data = JsonConvert.DeserializeObject<List<ExamDTO>>(result);
                        ViewData["exams"] = data;
                    }
                }
            }
            return View();
        }

        public async Task<ActionResult> PostAddAsync(Question ques, string exam)
        {
            var a = exam.Split("_");
            ques.ExamId = Int16.Parse(a[0]);
            ques.PaperNo = Int16.Parse(a[1]);
            string link = "http://localhost:5000/api/Question";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PostAsJsonAsync(link, ques))
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

        public async Task<IActionResult> EditAsync(int id)
        {
            {
                Question author = new Question();
                string link = "http://localhost:5000/api/Question";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(link + "/id?id=" + id))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string result = await content.ReadAsStringAsync();
                            author = JsonConvert.DeserializeObject<Question>(result);
                            ViewData["question"] = author;
                        }
                    }
                }
            }

            {
                List<ExamDTO> data = new List<ExamDTO>();
                string link = "http://localhost:5000/api/Exam/AllNo";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(link))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string result = await content.ReadAsStringAsync();

                            data = JsonConvert.DeserializeObject<List<ExamDTO>>(result);
                            ViewData["exams"] = data;
                        }
                    }
                }
            }

            return View();
        }

        public async Task<IActionResult> PostUpdateAsync(string questionId, string questionName, string mark, string exam)
        {
            if(exam == null)
            {
                TempData["Message"] = "Select Exam - Paper No";
                return RedirectToAction("Edit", new { id = Int16.Parse(questionId) });
            }
            var a = exam.Split("_");
            if (a[0] == null || a[1] == null || a[0].Trim() == "" || a[1].Trim() == "")
            {
                TempData["Message"] = "Select Exam - Paper No";
                return RedirectToAction("Edit", new {id = Int16.Parse(questionId) });
            }


            QuestionDTO dTO = new QuestionDTO
            {
                QuestionId = Int16.Parse(questionId),
                ExamId = Int16.Parse(a[0]),
                Mark = Int16.Parse(mark),
                PaperNo = Int16.Parse(a[1]),
                QuestionName = questionName
            };

            string link = "http://localhost:5000/api/Question";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.PutAsJsonAsync(link, dTO))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        //Console.WriteLine("Updated");
                        TempData["Message"] = "Updated Success";
                    }
                    else
                    {
                        //Console.WriteLine("Fail");
                        TempData["Message"] = "Fail";
                    }
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            string link = "http://localhost:5000/api/Question";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.DeleteAsync(link + "?id=" + id))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        TempData["Message"] = "Delete success";
                    }
                    else
                    {
                        string errorMessage = await res.Content.ReadAsStringAsync();
                        TempData["Message"] = "Delete fail - " + errorMessage;
                    }
                }

            }
            return RedirectToAction("Index");
        }
    }
}
