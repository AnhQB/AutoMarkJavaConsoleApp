using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projectClient.DTO;
using projectClient.Models;

namespace projectClient.Controllers
{
    public class TestCaseController : Controller
    {
        public async Task<IActionResult> IndexAsync()
        {
            List<ExamDTO> list = new List<ExamDTO>();
            string link = "http://localhost:5000/api/Exam";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(link))
                {
                    using (HttpContent content = res.Content)
                    {
                        string result = await content.ReadAsStringAsync();

                        list = JsonConvert.DeserializeObject<List<ExamDTO>>(result);
                        ViewData["exams"] = list;
                    }
                }
            }

            List<TestCase> list1 = new List<TestCase>();
            string link1 = "http://localhost:5000/api/TestCase";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(link1))
                {
                    using (HttpContent content = res.Content)
                    {
                        string result = await content.ReadAsStringAsync();

                        list1 = JsonConvert.DeserializeObject<List<TestCase>>(result);
                        ViewData["testcases"] = list1;
                    }
                }
            }

            return View();
        }
    }
}
