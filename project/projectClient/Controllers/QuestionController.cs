using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using project.Models;
using projectClient.DTO;

namespace projectClient.Controllers
{
    public class QuestionController : Controller
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
    }
}
